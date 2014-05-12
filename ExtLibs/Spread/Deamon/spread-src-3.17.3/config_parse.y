%{
/*
 * The Spread Toolkit.
 *     
 * The contents of this file are subject to the Spread Open-Source
 * License, Version 1.0 (the ``License''); you may not use
 * this file except in compliance with the License.  You may obtain a
 * copy of the License at:
 *
 * http://www.spread.org/license/
 *
 * or in the file ``license.txt'' found in this distribution.
 *
 * Software distributed under the License is distributed on an AS IS basis, 
 * WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License 
 * for the specific language governing rights and limitations under the 
 * License.
 *
 * The Creators of Spread are:
 *  Yair Amir, Michal Miskin-Amir, Jonathan Stanton.
 *
 *  Copyright (C) 1993-2002 Spread Concepts LLC <spread@spreadconcepts.com>
 *
 *  All Rights Reserved.
 *
 * Major Contributor(s):
 * ---------------
 *    Cristina Nita-Rotaru crisn@cnds.jhu.edu - group communication security.
 *    Theo Schlossnagle    jesus@omniti.com - Perl, skiplists, autoconf.
 *    Dan Schoenblum       dansch@cnds.jhu.edu - Java interface.
 *    John Schultz         jschultz@cnds.jhu.edu - contribution to process group membership.
 *
 */


#include "arch.h"
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#ifndef ARCH_PC_WIN95
#include <sys/types.h>
#include <netdb.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <unistd.h>
#include <sys/param.h>

#else /* ARCH_PC_WIN95 */
#include <winsock.h>
#endif /* ARCH_PC_WIN95 */

#include "alarm.h"
#include "configuration.h"
#include "skiplist.h"
#include "memory.h"
#include "objects.h"
#include "conf_body.h"
#include "acm.h"

        int     line_num, semantic_errors;
 extern char    *yytext;
 extern int     yyerror(char *str);
 extern void    yywarn(char *str);
 extern int     yylex();

 static int     num_procs = 0;
 static int     segment_procs = 0;
 static int     segments = 0;
 static int     rvec_num = 0;
 static int     procs_interfaces = 0;

 static int     authentication_configured = 0;

static char *segment2str(int seg) {
  static char ipstr[40];
  int id = Config.segments[seg].bcast_address;
  sprintf(ipstr, "%d.%d.%d.%d:%d",
  	(id & 0xff000000)>>24,
  	(id & 0xff0000)>>16,
  	(id & 0xff00)>>8,
  	(id & 0xff),
	Config.segments[seg].port);
  return ipstr;
}
static void alarm_print_proc(proc *p, int port) {
  if(port == p->port)
    Alarm(CONF, "\t%20s: %d.%d.%d.%d\n", p->name,
  	  (p->id & 0xff000000)>>24,
  	  (p->id & 0xff0000)>>16,
  	  (p->id & 0xff00)>>8,
  	  (p->id & 0xff));
  else
    Alarm(CONF, "\t%20s: %d.%d.%d.%d:%d\n", p->name,
  	  (p->id & 0xff000000)>>24,
  	  (p->id & 0xff0000)>>16,
  	  (p->id & 0xff00)>>8,
  	  (p->id & 0xff),
	  p->port);
}

static int32u name2ip(char *name) {
  int anip, i1, i2, i3, i4;
  struct hostent *host_ptr;

  host_ptr = gethostbyname(name);
  
  if ( host_ptr == 0)
    Alarm( EXIT, "Conf_init: no such host %s\n",
	   name);
  
  memcpy(&anip, host_ptr->h_addr_list[0], 
	 sizeof(int32) );
  anip = htonl( anip );
  i1= ( anip & 0xff000000 ) >> 24;
  i2= ( anip & 0x00ff0000 ) >> 16;
  i3= ( anip & 0x0000ff00 ) >>  8;
  i4=   anip & 0x000000ff;
  return ((i1<<24)|(i2<<16)|(i3<<8)|i4);
}

static  void expand_filename(char *out_string, int str_size, const char *in_string)
{
  const char *in_loc;
  char *out_loc;
  char hostn[256];
  
  for ( in_loc = in_string, out_loc = out_string; out_loc - out_string < str_size; in_loc++ )
  {
          if (*in_loc == '%' ) {
                  switch( in_loc[1] ) {
                  case 'h':
                  case 'H':
                          gethostname(hostn, sizeof(hostn) );
                          out_loc += snprintf(out_loc, str_size - (out_loc - out_string), "%s", hostn); 
                          in_loc++;
                          continue;
                  default:
                          break;
                  }

          }
          *out_loc = *in_loc;
          out_loc++;
          if (*in_loc == '\0') break;
  }
  out_string[str_size-1] = '\0';
}

static  int 	get_parsed_proc_info( char *name, proc *p )
{
	int	i;

	for ( i=0; i < num_procs; i++ )
	{
		if ( strcmp( Config_procs[i].name, name ) == 0 )
		{
			*p = Config_procs[i];
			return( i );
		}
	}
	return( -1 );
}


#define PROC_NAME_CHECK( stoken ) { \
                                            char strbuf[80]; \
                                            int ret; \
                                            proc p; \
                                            if ( strlen((stoken)) >= MAX_PROC_NAME ) { \
                                                snprintf(strbuf, 80, "Too long name(%d max): %s)\n", MAX_PROC_NAME, (stoken)); \
                                                return (yyerror(strbuf)); \
                                            } \
                                            ret = get_parsed_proc_info( stoken, &p ); \
                                            if (ret >= 0) { \
                                                snprintf(strbuf, 80, "Name not unique. name: %s equals (%s, %d.%d.%d.%d)\n", (stoken), p.name, IP1(p.id), IP2(p.id), IP3(p.id), IP4(p.id) ); \
                                                return (yyerror(strbuf)); \
                                            } \
                                         }
#define PROCS_CHECK( num_procs, stoken ) { \
                                            char strbuf[80]; \
                                            if ( (num_procs) >= MAX_PROCS_RING ) { \
                                                snprintf(strbuf, 80, "%s (Too many daemons configured--%d max)\n", (stoken), MAX_PROCS_RING); \
                                                return (yyerror(strbuf)); \
                                            } \
                                         }
#define SEGMENT_CHECK( num_segments, stoken )  { \
                                            char strbuf[80]; \
                                            if ( (num_segments) >= MAX_SEGMENTS ) { \
                                                snprintf(strbuf, 80, "%s (Too many segments configured--%d max)\n", (stoken), MAX_SEGMENTS); \
                                                return( yyerror(strbuf)); \
                                            } \
                                         }
#define SEGMENT_SIZE_CHECK( num_procs, stoken )  { \
                                            char strbuf[80]; \
                                            if ( (num_procs) >= MAX_PROCS_SEGMENT ) { \
                                                snprintf(strbuf, 80, "%s (Too many daemons configured in segment--%d max)\n", (stoken), MAX_PROCS_SEGMENT); \
                                                return( yyerror(strbuf)); \
                                            } \
                                         }
#define INTERFACE_NUM_CHECK( num_ifs, stoken )  { \
                                            char strbuf[80]; \
                                            if ( (num_ifs) >= MAX_INTERFACES_PROC ) { \
                                                snprintf(strbuf, 80, "%s (Too many interfaces configured in proc--%d max)\n", (stoken), MAX_INTERFACES_PROC); \
                                                return( yyerror(strbuf)); \
                                            } \
                                         }


%}
%start Config
%token SEGMENT EVENTLOGFILE EVENTTIMESTAMP EVENTPRIORITY IPADDR NUMBER COLON
%token PDEBUG PINFO PWARNING PERROR PCRITICAL PFATAL
%token OPENBRACE CLOSEBRACE EQUALS STRING
%token DEBUGFLAGS BANG
%token DDEBUG DEXIT DPRINT DDATA_LINK DNETWORK DPROTOCOL DSESSION
%token DCONF DMEMB DFLOW_CONTROL DSTATUS DEVENTS DGROUPS DMEMORY
%token DSKIPLIST DACM DALL DNONE
%token DANGEROUSMONITOR SOCKETPORTREUSE RUNTIMEDIR SPUSER SPGROUP ALLOWEDAUTHMETHODS REQUIREDAUTHMETHODS ACCESSCONTROLPOLICY
%token SP_BOOL SP_TRIVAL LINKPROTOCOL PHOP PTCPHOP
%token IMONITOR ICLIENT IDAEMON
%token ROUTEMATRIX LINKCOST
%%
Config		:	ConfigStructs
                        {
			  Config.num_segments = segments;
			  Num_procs = num_procs;
			  Alarm(CONF, "Finished configuration file.\n");
			}


ConfigStructs	:	SegmentStruct ConfigStructs
		|	ParamStruct ConfigStructs
		|	RouteStruct ConfigStructs
		|
		;

AlarmBit	:	DDEBUG { $$ = $1; }
		|	DEXIT { $$ = $1; }
		|	DPRINT { $$ = $1; }
		|	DDATA_LINK { $$ = $1; }
		|	DNETWORK { $$ = $1; }
		|	DPROTOCOL { $$ = $1; }
		|	DSESSION { $$ = $1; }
		|	DCONF { $$ = $1; }
		|	DMEMB { $$ = $1; }
		|	DFLOW_CONTROL { $$ = $1; }
		|	DSTATUS { $$ = $1; }
		|	DEVENTS { $$ = $1; }
		|	DGROUPS { $$ = $1; }
		|	DMEMORY { $$ = $1; }
		|	DSKIPLIST { $$ = $1; }
		|	DACM { $$ = $1; }
		|	DALL { $$ = $1; }
		|	DNONE { $$ = $1; }
		;

AlarmBitComp	:	AlarmBitComp AlarmBit
			{
			  $$.mask = ($1.mask | $2.mask);
			}
		|	AlarmBitComp BANG AlarmBit
			{
			  $$.mask = ($1.mask & ~($3.mask));
			}
		|	{ $$.mask = NONE; }
		;
PriorityLevel   :       PDEBUG { $$ = $1; }
                |       PINFO { $$ = $1; }
                |       PWARNING { $$ = $1; }
                |       PERROR { $$ = $1; }
                |       PCRITICAL { $$ = $1; }
                |       PFATAL { $$ = $1; }
                ;

ParamStruct	:	DEBUGFLAGS EQUALS OPENBRACE AlarmBitComp CLOSEBRACE
			{
			  if (! Alarm_get_interactive() ) {
                            Alarm_clear_types(ALL);
			    Alarm_set_types($4.mask);
			    Alarm(CONF, "Set Alarm mask to: %x\n", Alarm_get_types());
                          }
			}
                |       EVENTPRIORITY EQUALS PriorityLevel
                        {
                            if (! Alarm_get_interactive() ) {
                                Alarm_set_priority($3.number);
                            }
                        }

		|	EVENTLOGFILE EQUALS STRING
			{
			  if (! Alarm_get_interactive() ) {
                            char file_buf[MAXPATHLEN];
                            expand_filename(file_buf, MAXPATHLEN, $3.string);
                            Alarm_set_output(file_buf);
                          }
			}
		|	EVENTTIMESTAMP EQUALS STRING
			{
			  if (! Alarm_get_interactive() ) {
			    Alarm_enable_timestamp($3.string);
                          }
			}
		|	EVENTTIMESTAMP
			{
			  if (! Alarm_get_interactive() ) {
			    Alarm_enable_timestamp(NULL);
                          }
			}
		|	DANGEROUSMONITOR EQUALS SP_BOOL
			{
			  if (! Alarm_get_interactive() ) {
                            Conf_set_dangerous_monitor_state($3.boolean);
                          }
			}
                |       SOCKETPORTREUSE EQUALS SP_TRIVAL
                        {
                            port_reuse state;
                            if ($3.number == 1)
                            {
                                state = port_reuse_on;
                            }
                            else if ($3.number == 0)
                            {
                                state = port_reuse_off;
                            }
                            else
                            {
                                /* Default to AUTO. */
                                state = port_reuse_auto;
                            }
                            Conf_set_port_reuse_type(state);
                        }
                |       RUNTIMEDIR EQUALS STRING
                        {
                            Conf_set_runtime_dir($3.string);
                        }
                |       SPUSER EQUALS STRING
                        {
                            Conf_set_user($3.string);
                        }
                |       SPGROUP EQUALS STRING
                        {
                            Conf_set_group($3.string);
                        }
                |       ALLOWEDAUTHMETHODS EQUALS STRING
                        {
                            char auth_list[MAX_AUTH_LIST_LEN];
                            int i, len;
                            char *c_ptr;
                            if (!authentication_configured) {
                                Acm_auth_set_disabled("NULL");
                            }
                            authentication_configured = 1;

                            strncpy(auth_list, $3.string, MAX_AUTH_LIST_LEN);
                            len = strlen(auth_list); 
                            for (i=0; i < len; )
                            {
                                c_ptr = strchr(&auth_list[i], ' ');
                                if (c_ptr != NULL)
                                {
                                    *c_ptr = '\0';
                                }
                                Acm_auth_set_enabled(&auth_list[i]);    
                                i += strlen(&auth_list[i]);
                                i++; /* for null */
                            }
                        }
                |       REQUIREDAUTHMETHODS EQUALS STRING
                        {
                            char auth_list[MAX_AUTH_LIST_LEN];
                            int i, len;
                            char *c_ptr;
                            if (!authentication_configured) {
                                Acm_auth_set_disabled("NULL");
                            }
                            authentication_configured = 1;

                            strncpy(auth_list, $3.string, MAX_AUTH_LIST_LEN);
                            len = strlen(auth_list); 
                            for (i=0; i < len; )
                            {
                                c_ptr = strchr(&auth_list[i], ' ');
                                if (c_ptr != NULL)
                                {
                                    *c_ptr = '\0';
                                } 
                                Acm_auth_set_required(&auth_list[i]);    
                                i += strlen(&auth_list[i]);
                                i++; /* for null */
                            }
                        }
                |       ACCESSCONTROLPOLICY EQUALS STRING
                        {
                            int ret;
                            ret = Acm_acp_set_policy($3.string);
                            if (!ret)
                            {
                                    yyerror("Invalid Access Control Policy name. Make sure it is spelled right and any needed mocdules are loaded");
                            }
                        }
		|	LINKPROTOCOL EQUALS PHOP
			{
                            Conf_set_link_protocol(HOP_PROT);
			}
		|	LINKPROTOCOL EQUALS PTCPHOP
			{
                            Conf_set_link_protocol(TCP_PROT);
			}

SegmentStruct	:    SEGMENT IPADDR OPENBRACE Segmentparams CLOSEBRACE
                        { int i;
                          SEGMENT_CHECK( segments, inet_ntoa($2.ip.addr) );
			  Config.segments[segments].num_procs = segment_procs;
			  Config.segments[segments].port = $2.ip.port;
			  Config.segments[segments].bcast_address =
			    $2.ip.addr.s_addr;
			  if(Config.segments[segments].port == 0)
			    Config.segments[segments].port = DEFAULT_SPREAD_PORT;
			  Alarm(CONF, "Successfully configured Segment %d [%s] with %d procs:\n",
				segments,
				segment2str(segments),
				segment_procs);
			  for(i=(num_procs - segment_procs); i<num_procs; i++) {
                                  /* This '1' is to keep each proc with the same port as the segment.*/
			    if( 1 || Config_procs[i].port==0)  {
			      Config_procs[i].port=
				Config.segments[segments].port;
			    }
			    alarm_print_proc(&Config_procs[i],
			    	Config.segments[segments].port);
			  }
			  segments++;
			  segment_procs = 0;
			}
		;

Segmentparams	:	Segmentparam Segmentparams
		|
		;

Segmentparam	:	STRING IPADDR OPENBRACE Interfaceparams CLOSEBRACE
                        { 
                          PROC_NAME_CHECK( $1.string );
                          PROCS_CHECK( num_procs, $1.string );
                          SEGMENT_CHECK( segments, $1.string );
                          SEGMENT_SIZE_CHECK( segment_procs, $1.string );
                          if (procs_interfaces == 0)
                                  yyerror("Interfaces section declared but no actual interface addresses defined\n");
                          strcpy(Config_procs[num_procs].name, $1.string);
                          Config_procs[num_procs].id = $2.ip.addr.s_addr;
 		          Config_procs[num_procs].port = $2.ip.port;
			  Config_procs[num_procs].seg_index = segments;
			  Config_procs[num_procs].index_in_seg = segment_procs;
                          Config_procs[num_procs].num_if = procs_interfaces;
			  Config.segments[segments].procs[segment_procs] = 
                                  &Config_procs[num_procs];
			  num_procs++;
			  segment_procs++;
                          procs_interfaces = 0;
			}
		|	STRING OPENBRACE Interfaceparams CLOSEBRACE
			{ 
                          PROC_NAME_CHECK( $1.string );
                          PROCS_CHECK( num_procs, $1.string );
                          SEGMENT_CHECK( segments, $1.string );
                          SEGMENT_SIZE_CHECK( segment_procs, $1.string );
                          if (procs_interfaces == 0)
                                  yyerror("Interfaces section declared but no actual interface addresses defined\n");
                          strcpy(Config_procs[num_procs].name, $1.string);
                          Config_procs[num_procs].id =
			    name2ip(Config_procs[num_procs].name);
 		          Config_procs[num_procs].port = 0;
			  Config_procs[num_procs].seg_index = segments;
			  Config_procs[num_procs].index_in_seg = segment_procs;
                          Config_procs[num_procs].num_if = procs_interfaces;
			  Config.segments[segments].procs[segment_procs] = 
                                  &Config_procs[num_procs];
			  num_procs++;
			  segment_procs++;
                          procs_interfaces = 0;
			}
                |       STRING IPADDR
			{ 
                          PROC_NAME_CHECK( $1.string );
                          PROCS_CHECK( num_procs, $1.string );
                          SEGMENT_CHECK( segments, $1.string );
                          SEGMENT_SIZE_CHECK( segment_procs, $1.string );
                          strcpy(Config_procs[num_procs].name, $1.string);
                          Config_procs[num_procs].id = $2.ip.addr.s_addr;
 		          Config_procs[num_procs].port = $2.ip.port;
			  Config_procs[num_procs].seg_index = segments;
			  Config_procs[num_procs].index_in_seg = segment_procs;
                          Config_procs[num_procs].num_if = 1;
                          Config_procs[num_procs].ifc[0].ip = Config_procs[num_procs].id;
                          Config_procs[num_procs].ifc[0].port = Config_procs[num_procs].port;
                          Config_procs[num_procs].ifc[0].type = IFTYPE_ALL | IFTYPE_ANY;
			  Config.segments[segments].procs[segment_procs] = 
                                  &Config_procs[num_procs];
			  num_procs++;
			  segment_procs++;
                          procs_interfaces = 0;
			}
		|	STRING
			{ 
                          PROC_NAME_CHECK( $1.string );
                          PROCS_CHECK( num_procs, $1.string );
                          SEGMENT_CHECK( segments, $1.string );
                          SEGMENT_SIZE_CHECK( segment_procs, $1.string );
                          strcpy(Config_procs[num_procs].name, $1.string);
                          Config_procs[num_procs].id =
			    name2ip(Config_procs[num_procs].name);
 		          Config_procs[num_procs].port = 0;
			  Config_procs[num_procs].seg_index = segments;
			  Config_procs[num_procs].index_in_seg = segment_procs;
                          Config_procs[num_procs].num_if = 1;
                          Config_procs[num_procs].ifc[0].ip = Config_procs[num_procs].id;
                          Config_procs[num_procs].ifc[0].port = Config_procs[num_procs].port;
                          Config_procs[num_procs].ifc[0].type = IFTYPE_ALL | IFTYPE_ANY;
			  Config.segments[segments].procs[segment_procs] = 
                                  &Config_procs[num_procs];
			  num_procs++;
			  segment_procs++;
                          procs_interfaces = 0;
			}
		;

IfType  	:	IMONITOR { $$ = $1; }
		|	ICLIENT { $$ = $1; }
		|	IDAEMON { $$ = $1; }
		;

IfTypeComp	:       IfTypeComp IfType
			{
			  $$.mask = ($1.mask | $2.mask);
			}
		|	{ $$.mask = 0; }
		;

Interfaceparams	:	Interfaceparam Interfaceparams
		|
		;

Interfaceparam	:	IfTypeComp IPADDR
			{ 
                          PROCS_CHECK( num_procs, $1.string );
                          SEGMENT_CHECK( segments, $1.string );
                          SEGMENT_SIZE_CHECK( segment_procs, $1.string );
                          INTERFACE_NUM_CHECK( procs_interfaces, $1.string );
                          Config_procs[num_procs].ifc[procs_interfaces].ip = $2.ip.addr.s_addr;
                          Config_procs[num_procs].ifc[procs_interfaces].port = $2.ip.port;
                          if ($1.mask == 0)
                                  Config_procs[num_procs].ifc[procs_interfaces].type = IFTYPE_ALL;
                          else 
                                  Config_procs[num_procs].ifc[procs_interfaces].type = $1.mask;
                          procs_interfaces++;
			}
		;

RouteStruct	:    ROUTEMATRIX OPENBRACE Routevectors CLOSEBRACE
                        { 
			  Alarm(CONF, "Successfully configured Routing Matrix for %d Segments with %d rows in the routing matrix\n",segments, rvec_num);
			}
		;

Routevectors	:	Routevectors Routevector
		|
		;

Routevector     :       LINKCOST
                        { 
                                int rvec_element;
                                for (rvec_element = 0; rvec_element < segments; rvec_element++) {
                                        if ($1.cost[rvec_element] < 0) yyerror("Wrong number of entries for routing matrix");
                                        LinkWeights[rvec_num][rvec_element] = $1.cost[rvec_element];
                                }
                                rvec_num++;
                        }
                |       
                ;
%%
void yywarn(char *str) {
        fprintf(stderr, "-------Parse Warning-----------\n");
        fprintf(stderr, "Parser warning on or before line %d\n", line_num);
        fprintf(stderr, "Error type; %s\n", str);
        fprintf(stderr, "Offending token: %s\n", yytext);
}
int yyerror(char *str) {
  fprintf(stderr, "-------------------------------------------\n");
  fprintf(stderr, "Parser error on or before line %d\n", line_num);
  fprintf(stderr, "Error type; %s\n", str);
  fprintf(stderr, "Offending token: %s\n", yytext);
  exit(1);
}
