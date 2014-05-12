#ifndef lint
static char const 
yyrcsid[] = "$FreeBSD: src/usr.bin/yacc/skeleton.c,v 1.28 2000/01/17 02:04:06 bde Exp $";
#endif
#include <stdlib.h>
#define YYBYACC 1
#define YYMAJOR 1
#define YYMINOR 9
#define YYLEX yylex()
#define YYEMPTY -1
#define yyclearin (yychar=(YYEMPTY))
#define yyerrok (yyerrflag=0)
#define YYRECOVERING() (yyerrflag!=0)
static int yygrowstack();
#define YYPREFIX "yy"
#line 2 "./config_parse.y"
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


#line 227 "y.tab.c"
#define YYERRCODE 256
#define SEGMENT 257
#define EVENTLOGFILE 258
#define EVENTTIMESTAMP 259
#define EVENTPRIORITY 260
#define IPADDR 261
#define NUMBER 262
#define COLON 263
#define PDEBUG 264
#define PINFO 265
#define PWARNING 266
#define PERROR 267
#define PCRITICAL 268
#define PFATAL 269
#define OPENBRACE 270
#define CLOSEBRACE 271
#define EQUALS 272
#define STRING 273
#define DEBUGFLAGS 274
#define BANG 275
#define DDEBUG 276
#define DEXIT 277
#define DPRINT 278
#define DDATA_LINK 279
#define DNETWORK 280
#define DPROTOCOL 281
#define DSESSION 282
#define DCONF 283
#define DMEMB 284
#define DFLOW_CONTROL 285
#define DSTATUS 286
#define DEVENTS 287
#define DGROUPS 288
#define DMEMORY 289
#define DSKIPLIST 290
#define DACM 291
#define DALL 292
#define DNONE 293
#define DANGEROUSMONITOR 294
#define SOCKETPORTREUSE 295
#define RUNTIMEDIR 296
#define SPUSER 297
#define SPGROUP 298
#define ALLOWEDAUTHMETHODS 299
#define REQUIREDAUTHMETHODS 300
#define ACCESSCONTROLPOLICY 301
#define SP_BOOL 302
#define SP_TRIVAL 303
#define LINKPROTOCOL 304
#define PHOP 305
#define PTCPHOP 306
#define IMONITOR 307
#define ICLIENT 308
#define IDAEMON 309
#define ROUTEMATRIX 310
#define LINKCOST 311
const short yylhs[] = {                                        -1,
    0,    1,    1,    1,    1,    5,    5,    5,    5,    5,
    5,    5,    5,    5,    5,    5,    5,    5,    5,    5,
    5,    5,    5,    6,    6,    6,    7,    7,    7,    7,
    7,    7,    3,    3,    3,    3,    3,    3,    3,    3,
    3,    3,    3,    3,    3,    3,    3,    2,    8,    8,
    9,    9,    9,    9,   11,   11,   11,   12,   12,   10,
   10,   13,    4,   14,   14,   15,   15,
};
const short yylen[] = {                                         2,
    1,    2,    2,    2,    0,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    2,    3,    0,    1,    1,    1,    1,
    1,    1,    5,    3,    3,    3,    1,    3,    3,    3,
    3,    3,    3,    3,    3,    3,    3,    5,    2,    0,
    5,    4,    2,    1,    1,    1,    1,    2,    0,    2,
    0,    2,    4,    2,    0,    1,    0,
};
const short yydefred[] = {                                      0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    1,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   65,    2,    3,    4,    0,   35,
   36,   27,   28,   29,   30,   31,   32,   34,   26,   38,
   39,   40,   41,   42,   43,   44,   45,   46,   47,    0,
    0,    0,    0,    0,   63,   66,   64,    0,    0,   48,
   49,   33,    0,    6,    7,    8,    9,   10,   11,   12,
   13,   14,   15,   16,   17,   18,   19,   20,   21,   22,
   23,   24,    0,    0,    0,    0,   25,    0,   52,   62,
   55,   56,   57,   58,   60,   51,
};
const short yydgoto[] = {                                      16,
   17,   18,   19,   20,   92,   64,   48,   62,   63,   94,
  104,   95,   96,   60,   67,
};
const short yysindex[] = {                                   -255,
 -249, -266, -239, -238, -237, -236, -235, -234, -224, -222,
 -221, -220, -219, -218, -242,    0,    0, -255, -255, -255,
 -207, -208, -170, -243, -206, -271, -199, -168, -167, -166,
 -165, -164, -163, -288,    0,    0,    0,    0, -162,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, -264,
 -261, -159, -162, -209,    0,    0,    0, -157,    0,    0,
    0,    0, -191,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0, -156, -248,    0,    0, -155,    0,    0,
    0,    0,    0,    0,    0,    0,
};
const short yyrindex[] = {                                    114,
    0,    0,    1,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  114,  114,  114,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, -154,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 -244,    0, -154,    0,    0,    0,    0, -241, -251,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, -251,    0,    0, -251,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,
};
const short yygindex[] = {                                      0,
   -4,    0,    0,    0,   45,    0,    0,   56,    0,  -85,
    0,    0,    0,    0,    0,
};
#define YYTABLESIZE 311
const short yytable[] = {                                      68,
   37,    1,    2,    3,    4,   22,   65,   98,   69,   59,
  105,   21,  100,   36,   37,   38,   58,   59,    5,   61,
   42,   43,   44,   45,   46,   47,   54,   35,   54,   53,
   50,   53,   23,   24,   25,   26,   27,   28,    6,    7,
    8,    9,   10,   11,   12,   13,   66,   29,   14,   30,
   31,   32,   33,   34,   15,   59,   59,   59,  101,  102,
  103,   72,   39,   49,   40,   73,   74,   75,   76,   77,
   78,   79,   80,   81,   82,   83,   84,   85,   86,   87,
   88,   89,   90,   91,   74,   75,   76,   77,   78,   79,
   80,   81,   82,   83,   84,   85,   86,   87,   88,   89,
   90,   91,   41,   51,   52,   53,   54,   55,   56,   57,
   61,   70,   93,    5,   99,  106,   50,   97,   71,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,   37,   37,   37,
   37,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   37,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   37,   37,   37,   37,   37,   37,
   37,   37,    0,    0,   37,    0,    0,    0,    0,    0,
   37,
};
const short yycheck[] = {                                     261,
    0,  257,  258,  259,  260,  272,  271,   93,  270,  261,
   96,  261,  261,   18,   19,   20,  305,  306,  274,  271,
  264,  265,  266,  267,  268,  269,  271,  270,  273,  271,
  302,  273,  272,  272,  272,  272,  272,  272,  294,  295,
  296,  297,  298,  299,  300,  301,  311,  272,  304,  272,
  272,  272,  272,  272,  310,  307,  308,  309,  307,  308,
  309,  271,  270,  270,  273,  275,  276,  277,  278,  279,
  280,  281,  282,  283,  284,  285,  286,  287,  288,  289,
  290,  291,  292,  293,  276,  277,  278,  279,  280,  281,
  282,  283,  284,  285,  286,  287,  288,  289,  290,  291,
  292,  293,  273,  303,  273,  273,  273,  273,  273,  273,
  273,  271,  270,    0,  271,  271,  271,   73,   63,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  257,  258,  259,
  260,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  274,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  294,  295,  296,  297,  298,  299,
  300,  301,   -1,   -1,  304,   -1,   -1,   -1,   -1,   -1,
  310,
};
#define YYFINAL 16
#ifndef YYDEBUG
#define YYDEBUG 0
#endif
#define YYMAXTOKEN 311
#if YYDEBUG
const char * const yyname[] = {
"end-of-file",0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,"SEGMENT","EVENTLOGFILE",
"EVENTTIMESTAMP","EVENTPRIORITY","IPADDR","NUMBER","COLON","PDEBUG","PINFO",
"PWARNING","PERROR","PCRITICAL","PFATAL","OPENBRACE","CLOSEBRACE","EQUALS",
"STRING","DEBUGFLAGS","BANG","DDEBUG","DEXIT","DPRINT","DDATA_LINK","DNETWORK",
"DPROTOCOL","DSESSION","DCONF","DMEMB","DFLOW_CONTROL","DSTATUS","DEVENTS",
"DGROUPS","DMEMORY","DSKIPLIST","DACM","DALL","DNONE","DANGEROUSMONITOR",
"SOCKETPORTREUSE","RUNTIMEDIR","SPUSER","SPGROUP","ALLOWEDAUTHMETHODS",
"REQUIREDAUTHMETHODS","ACCESSCONTROLPOLICY","SP_BOOL","SP_TRIVAL",
"LINKPROTOCOL","PHOP","PTCPHOP","IMONITOR","ICLIENT","IDAEMON","ROUTEMATRIX",
"LINKCOST",
};
const char * const yyrule[] = {
"$accept : Config",
"Config : ConfigStructs",
"ConfigStructs : SegmentStruct ConfigStructs",
"ConfigStructs : ParamStruct ConfigStructs",
"ConfigStructs : RouteStruct ConfigStructs",
"ConfigStructs :",
"AlarmBit : DDEBUG",
"AlarmBit : DEXIT",
"AlarmBit : DPRINT",
"AlarmBit : DDATA_LINK",
"AlarmBit : DNETWORK",
"AlarmBit : DPROTOCOL",
"AlarmBit : DSESSION",
"AlarmBit : DCONF",
"AlarmBit : DMEMB",
"AlarmBit : DFLOW_CONTROL",
"AlarmBit : DSTATUS",
"AlarmBit : DEVENTS",
"AlarmBit : DGROUPS",
"AlarmBit : DMEMORY",
"AlarmBit : DSKIPLIST",
"AlarmBit : DACM",
"AlarmBit : DALL",
"AlarmBit : DNONE",
"AlarmBitComp : AlarmBitComp AlarmBit",
"AlarmBitComp : AlarmBitComp BANG AlarmBit",
"AlarmBitComp :",
"PriorityLevel : PDEBUG",
"PriorityLevel : PINFO",
"PriorityLevel : PWARNING",
"PriorityLevel : PERROR",
"PriorityLevel : PCRITICAL",
"PriorityLevel : PFATAL",
"ParamStruct : DEBUGFLAGS EQUALS OPENBRACE AlarmBitComp CLOSEBRACE",
"ParamStruct : EVENTPRIORITY EQUALS PriorityLevel",
"ParamStruct : EVENTLOGFILE EQUALS STRING",
"ParamStruct : EVENTTIMESTAMP EQUALS STRING",
"ParamStruct : EVENTTIMESTAMP",
"ParamStruct : DANGEROUSMONITOR EQUALS SP_BOOL",
"ParamStruct : SOCKETPORTREUSE EQUALS SP_TRIVAL",
"ParamStruct : RUNTIMEDIR EQUALS STRING",
"ParamStruct : SPUSER EQUALS STRING",
"ParamStruct : SPGROUP EQUALS STRING",
"ParamStruct : ALLOWEDAUTHMETHODS EQUALS STRING",
"ParamStruct : REQUIREDAUTHMETHODS EQUALS STRING",
"ParamStruct : ACCESSCONTROLPOLICY EQUALS STRING",
"ParamStruct : LINKPROTOCOL EQUALS PHOP",
"ParamStruct : LINKPROTOCOL EQUALS PTCPHOP",
"SegmentStruct : SEGMENT IPADDR OPENBRACE Segmentparams CLOSEBRACE",
"Segmentparams : Segmentparam Segmentparams",
"Segmentparams :",
"Segmentparam : STRING IPADDR OPENBRACE Interfaceparams CLOSEBRACE",
"Segmentparam : STRING OPENBRACE Interfaceparams CLOSEBRACE",
"Segmentparam : STRING IPADDR",
"Segmentparam : STRING",
"IfType : IMONITOR",
"IfType : ICLIENT",
"IfType : IDAEMON",
"IfTypeComp : IfTypeComp IfType",
"IfTypeComp :",
"Interfaceparams : Interfaceparam Interfaceparams",
"Interfaceparams :",
"Interfaceparam : IfTypeComp IPADDR",
"RouteStruct : ROUTEMATRIX OPENBRACE Routevectors CLOSEBRACE",
"Routevectors : Routevectors Routevector",
"Routevectors :",
"Routevector : LINKCOST",
"Routevector :",
};
#endif
#ifndef YYSTYPE
typedef int YYSTYPE;
#endif
#if YYDEBUG
#include <stdio.h>
#endif
#ifdef YYSTACKSIZE
#undef YYMAXDEPTH
#define YYMAXDEPTH YYSTACKSIZE
#else
#ifdef YYMAXDEPTH
#define YYSTACKSIZE YYMAXDEPTH
#else
#define YYSTACKSIZE 10000
#define YYMAXDEPTH 10000
#endif
#endif
#define YYINITSTACKSIZE 200
int yydebug;
int yynerrs;
int yyerrflag;
int yychar;
short *yyssp;
YYSTYPE *yyvsp;
YYSTYPE yyval;
YYSTYPE yylval;
short *yyss;
short *yysslim;
YYSTYPE *yyvs;
int yystacksize;
#line 585 "./config_parse.y"
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
#line 557 "y.tab.c"
/* allocate initial stack or double stack size, up to YYMAXDEPTH */
static int yygrowstack()
{
    int newsize, i;
    short *newss;
    YYSTYPE *newvs;

    if ((newsize = yystacksize) == 0)
        newsize = YYINITSTACKSIZE;
    else if (newsize >= YYMAXDEPTH)
        return -1;
    else if ((newsize *= 2) > YYMAXDEPTH)
        newsize = YYMAXDEPTH;
    i = yyssp - yyss;
    newss = yyss ? (short *)realloc(yyss, newsize * sizeof *newss) :
      (short *)malloc(newsize * sizeof *newss);
    if (newss == NULL)
        return -1;
    yyss = newss;
    yyssp = newss + i;
    newvs = yyvs ? (YYSTYPE *)realloc(yyvs, newsize * sizeof *newvs) :
      (YYSTYPE *)malloc(newsize * sizeof *newvs);
    if (newvs == NULL)
        return -1;
    yyvs = newvs;
    yyvsp = newvs + i;
    yystacksize = newsize;
    yysslim = yyss + newsize - 1;
    return 0;
}

#define YYABORT goto yyabort
#define YYREJECT goto yyabort
#define YYACCEPT goto yyaccept
#define YYERROR goto yyerrlab

#ifndef YYPARSE_PARAM
#if defined(__cplusplus) || __STDC__
#define YYPARSE_PARAM_ARG void
#define YYPARSE_PARAM_DECL
#else	/* ! ANSI-C/C++ */
#define YYPARSE_PARAM_ARG
#define YYPARSE_PARAM_DECL
#endif	/* ANSI-C/C++ */
#else	/* YYPARSE_PARAM */
#ifndef YYPARSE_PARAM_TYPE
#define YYPARSE_PARAM_TYPE void *
#endif
#if defined(__cplusplus) || __STDC__
#define YYPARSE_PARAM_ARG YYPARSE_PARAM_TYPE YYPARSE_PARAM
#define YYPARSE_PARAM_DECL
#else	/* ! ANSI-C/C++ */
#define YYPARSE_PARAM_ARG YYPARSE_PARAM
#define YYPARSE_PARAM_DECL YYPARSE_PARAM_TYPE YYPARSE_PARAM;
#endif	/* ANSI-C/C++ */
#endif	/* ! YYPARSE_PARAM */

int
yyparse (YYPARSE_PARAM_ARG)
    YYPARSE_PARAM_DECL
{
    register int yym, yyn, yystate;
#if YYDEBUG
    register const char *yys;

    if ((yys = getenv("YYDEBUG")))
    {
        yyn = *yys;
        if (yyn >= '0' && yyn <= '9')
            yydebug = yyn - '0';
    }
#endif

    yynerrs = 0;
    yyerrflag = 0;
    yychar = (-1);

    if (yyss == NULL && yygrowstack()) goto yyoverflow;
    yyssp = yyss;
    yyvsp = yyvs;
    *yyssp = yystate = 0;

yyloop:
    if ((yyn = yydefred[yystate])) goto yyreduce;
    if (yychar < 0)
    {
        if ((yychar = yylex()) < 0) yychar = 0;
#if YYDEBUG
        if (yydebug)
        {
            yys = 0;
            if (yychar <= YYMAXTOKEN) yys = yyname[yychar];
            if (!yys) yys = "illegal-symbol";
            printf("%sdebug: state %d, reading %d (%s)\n",
                    YYPREFIX, yystate, yychar, yys);
        }
#endif
    }
    if ((yyn = yysindex[yystate]) && (yyn += yychar) >= 0 &&
            yyn <= YYTABLESIZE && yycheck[yyn] == yychar)
    {
#if YYDEBUG
        if (yydebug)
            printf("%sdebug: state %d, shifting to state %d\n",
                    YYPREFIX, yystate, yytable[yyn]);
#endif
        if (yyssp >= yysslim && yygrowstack())
        {
            goto yyoverflow;
        }
        *++yyssp = yystate = yytable[yyn];
        *++yyvsp = yylval;
        yychar = (-1);
        if (yyerrflag > 0)  --yyerrflag;
        goto yyloop;
    }
    if ((yyn = yyrindex[yystate]) && (yyn += yychar) >= 0 &&
            yyn <= YYTABLESIZE && yycheck[yyn] == yychar)
    {
        yyn = yytable[yyn];
        goto yyreduce;
    }
    if (yyerrflag) goto yyinrecovery;
#if defined(lint) || defined(__GNUC__)
    goto yynewerror;
#endif
yynewerror:
    yyerror("syntax error");
#if defined(lint) || defined(__GNUC__)
    goto yyerrlab;
#endif
yyerrlab:
    ++yynerrs;
yyinrecovery:
    if (yyerrflag < 3)
    {
        yyerrflag = 3;
        for (;;)
        {
            if ((yyn = yysindex[*yyssp]) && (yyn += YYERRCODE) >= 0 &&
                    yyn <= YYTABLESIZE && yycheck[yyn] == YYERRCODE)
            {
#if YYDEBUG
                if (yydebug)
                    printf("%sdebug: state %d, error recovery shifting\
 to state %d\n", YYPREFIX, *yyssp, yytable[yyn]);
#endif
                if (yyssp >= yysslim && yygrowstack())
                {
                    goto yyoverflow;
                }
                *++yyssp = yystate = yytable[yyn];
                *++yyvsp = yylval;
                goto yyloop;
            }
            else
            {
#if YYDEBUG
                if (yydebug)
                    printf("%sdebug: error recovery discarding state %d\n",
                            YYPREFIX, *yyssp);
#endif
                if (yyssp <= yyss) goto yyabort;
                --yyssp;
                --yyvsp;
            }
        }
    }
    else
    {
        if (yychar == 0) goto yyabort;
#if YYDEBUG
        if (yydebug)
        {
            yys = 0;
            if (yychar <= YYMAXTOKEN) yys = yyname[yychar];
            if (!yys) yys = "illegal-symbol";
            printf("%sdebug: state %d, error recovery discards token %d (%s)\n",
                    YYPREFIX, yystate, yychar, yys);
        }
#endif
        yychar = (-1);
        goto yyloop;
    }
yyreduce:
#if YYDEBUG
    if (yydebug)
        printf("%sdebug: state %d, reducing by rule %d (%s)\n",
                YYPREFIX, yystate, yyn, yyrule[yyn]);
#endif
    yym = yylen[yyn];
    yyval = yyvsp[1-yym];
    switch (yyn)
    {
case 1:
#line 226 "./config_parse.y"
{
			  Config.num_segments = segments;
			  Num_procs = num_procs;
			  Alarm(CONF, "Finished configuration file.\n");
			}
break;
case 6:
#line 239 "./config_parse.y"
{ yyval = yyvsp[0]; }
break;
case 7:
#line 240 "./config_parse.y"
{ yyval = yyvsp[0]; }
break;
case 8:
#line 241 "./config_parse.y"
{ yyval = yyvsp[0]; }
break;
case 9:
#line 242 "./config_parse.y"
{ yyval = yyvsp[0]; }
break;
case 10:
#line 243 "./config_parse.y"
{ yyval = yyvsp[0]; }
break;
case 11:
#line 244 "./config_parse.y"
{ yyval = yyvsp[0]; }
break;
case 12:
#line 245 "./config_parse.y"
{ yyval = yyvsp[0]; }
break;
case 13:
#line 246 "./config_parse.y"
{ yyval = yyvsp[0]; }
break;
case 14:
#line 247 "./config_parse.y"
{ yyval = yyvsp[0]; }
break;
case 15:
#line 248 "./config_parse.y"
{ yyval = yyvsp[0]; }
break;
case 16:
#line 249 "./config_parse.y"
{ yyval = yyvsp[0]; }
break;
case 17:
#line 250 "./config_parse.y"
{ yyval = yyvsp[0]; }
break;
case 18:
#line 251 "./config_parse.y"
{ yyval = yyvsp[0]; }
break;
case 19:
#line 252 "./config_parse.y"
{ yyval = yyvsp[0]; }
break;
case 20:
#line 253 "./config_parse.y"
{ yyval = yyvsp[0]; }
break;
case 21:
#line 254 "./config_parse.y"
{ yyval = yyvsp[0]; }
break;
case 22:
#line 255 "./config_parse.y"
{ yyval = yyvsp[0]; }
break;
case 23:
#line 256 "./config_parse.y"
{ yyval = yyvsp[0]; }
break;
case 24:
#line 260 "./config_parse.y"
{
			  yyval.mask = (yyvsp[-1].mask | yyvsp[0].mask);
			}
break;
case 25:
#line 264 "./config_parse.y"
{
			  yyval.mask = (yyvsp[-2].mask & ~(yyvsp[0].mask));
			}
break;
case 26:
#line 267 "./config_parse.y"
{ yyval.mask = NONE; }
break;
case 27:
#line 269 "./config_parse.y"
{ yyval = yyvsp[0]; }
break;
case 28:
#line 270 "./config_parse.y"
{ yyval = yyvsp[0]; }
break;
case 29:
#line 271 "./config_parse.y"
{ yyval = yyvsp[0]; }
break;
case 30:
#line 272 "./config_parse.y"
{ yyval = yyvsp[0]; }
break;
case 31:
#line 273 "./config_parse.y"
{ yyval = yyvsp[0]; }
break;
case 32:
#line 274 "./config_parse.y"
{ yyval = yyvsp[0]; }
break;
case 33:
#line 278 "./config_parse.y"
{
			  if (! Alarm_get_interactive() ) {
                            Alarm_clear_types(ALL);
			    Alarm_set_types(yyvsp[-1].mask);
			    Alarm(CONF, "Set Alarm mask to: %x\n", Alarm_get_types());
                          }
			}
break;
case 34:
#line 286 "./config_parse.y"
{
                            if (! Alarm_get_interactive() ) {
                                Alarm_set_priority(yyvsp[0].number);
                            }
                        }
break;
case 35:
#line 293 "./config_parse.y"
{
			  if (! Alarm_get_interactive() ) {
                            char file_buf[MAXPATHLEN];
                            expand_filename(file_buf, MAXPATHLEN, yyvsp[0].string);
                            Alarm_set_output(file_buf);
                          }
			}
break;
case 36:
#line 301 "./config_parse.y"
{
			  if (! Alarm_get_interactive() ) {
			    Alarm_enable_timestamp(yyvsp[0].string);
                          }
			}
break;
case 37:
#line 307 "./config_parse.y"
{
			  if (! Alarm_get_interactive() ) {
			    Alarm_enable_timestamp(NULL);
                          }
			}
break;
case 38:
#line 313 "./config_parse.y"
{
			  if (! Alarm_get_interactive() ) {
                            Conf_set_dangerous_monitor_state(yyvsp[0].boolean);
                          }
			}
break;
case 39:
#line 319 "./config_parse.y"
{
                            port_reuse state;
                            if (yyvsp[0].number == 1)
                            {
                                state = port_reuse_on;
                            }
                            else if (yyvsp[0].number == 0)
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
break;
case 40:
#line 337 "./config_parse.y"
{
                            Conf_set_runtime_dir(yyvsp[0].string);
                        }
break;
case 41:
#line 341 "./config_parse.y"
{
                            Conf_set_user(yyvsp[0].string);
                        }
break;
case 42:
#line 345 "./config_parse.y"
{
                            Conf_set_group(yyvsp[0].string);
                        }
break;
case 43:
#line 349 "./config_parse.y"
{
                            char auth_list[MAX_AUTH_LIST_LEN];
                            int i, len;
                            char *c_ptr;
                            if (!authentication_configured) {
                                Acm_auth_set_disabled("NULL");
                            }
                            authentication_configured = 1;

                            strncpy(auth_list, yyvsp[0].string, MAX_AUTH_LIST_LEN);
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
break;
case 44:
#line 373 "./config_parse.y"
{
                            char auth_list[MAX_AUTH_LIST_LEN];
                            int i, len;
                            char *c_ptr;
                            if (!authentication_configured) {
                                Acm_auth_set_disabled("NULL");
                            }
                            authentication_configured = 1;

                            strncpy(auth_list, yyvsp[0].string, MAX_AUTH_LIST_LEN);
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
break;
case 45:
#line 397 "./config_parse.y"
{
                            int ret;
                            ret = Acm_acp_set_policy(yyvsp[0].string);
                            if (!ret)
                            {
                                    yyerror("Invalid Access Control Policy name. Make sure it is spelled right and any needed mocdules are loaded");
                            }
                        }
break;
case 46:
#line 406 "./config_parse.y"
{
                            Conf_set_link_protocol(HOP_PROT);
			}
break;
case 47:
#line 410 "./config_parse.y"
{
                            Conf_set_link_protocol(TCP_PROT);
			}
break;
case 48:
#line 415 "./config_parse.y"
{ int i;
                          SEGMENT_CHECK( segments, inet_ntoa(yyvsp[-3].ip.addr) );
			  Config.segments[segments].num_procs = segment_procs;
			  Config.segments[segments].port = yyvsp[-3].ip.port;
			  Config.segments[segments].bcast_address =
			    yyvsp[-3].ip.addr.s_addr;
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
break;
case 51:
#line 446 "./config_parse.y"
{ 
                          PROC_NAME_CHECK( yyvsp[-4].string );
                          PROCS_CHECK( num_procs, yyvsp[-4].string );
                          SEGMENT_CHECK( segments, yyvsp[-4].string );
                          SEGMENT_SIZE_CHECK( segment_procs, yyvsp[-4].string );
                          if (procs_interfaces == 0)
                                  yyerror("Interfaces section declared but no actual interface addresses defined\n");
                          strcpy(Config_procs[num_procs].name, yyvsp[-4].string);
                          Config_procs[num_procs].id = yyvsp[-3].ip.addr.s_addr;
 		          Config_procs[num_procs].port = yyvsp[-3].ip.port;
			  Config_procs[num_procs].seg_index = segments;
			  Config_procs[num_procs].index_in_seg = segment_procs;
                          Config_procs[num_procs].num_if = procs_interfaces;
			  Config.segments[segments].procs[segment_procs] = 
                                  &Config_procs[num_procs];
			  num_procs++;
			  segment_procs++;
                          procs_interfaces = 0;
			}
break;
case 52:
#line 466 "./config_parse.y"
{ 
                          PROC_NAME_CHECK( yyvsp[-3].string );
                          PROCS_CHECK( num_procs, yyvsp[-3].string );
                          SEGMENT_CHECK( segments, yyvsp[-3].string );
                          SEGMENT_SIZE_CHECK( segment_procs, yyvsp[-3].string );
                          if (procs_interfaces == 0)
                                  yyerror("Interfaces section declared but no actual interface addresses defined\n");
                          strcpy(Config_procs[num_procs].name, yyvsp[-3].string);
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
break;
case 53:
#line 487 "./config_parse.y"
{ 
                          PROC_NAME_CHECK( yyvsp[-1].string );
                          PROCS_CHECK( num_procs, yyvsp[-1].string );
                          SEGMENT_CHECK( segments, yyvsp[-1].string );
                          SEGMENT_SIZE_CHECK( segment_procs, yyvsp[-1].string );
                          strcpy(Config_procs[num_procs].name, yyvsp[-1].string);
                          Config_procs[num_procs].id = yyvsp[0].ip.addr.s_addr;
 		          Config_procs[num_procs].port = yyvsp[0].ip.port;
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
break;
case 54:
#line 508 "./config_parse.y"
{ 
                          PROC_NAME_CHECK( yyvsp[0].string );
                          PROCS_CHECK( num_procs, yyvsp[0].string );
                          SEGMENT_CHECK( segments, yyvsp[0].string );
                          SEGMENT_SIZE_CHECK( segment_procs, yyvsp[0].string );
                          strcpy(Config_procs[num_procs].name, yyvsp[0].string);
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
break;
case 55:
#line 531 "./config_parse.y"
{ yyval = yyvsp[0]; }
break;
case 56:
#line 532 "./config_parse.y"
{ yyval = yyvsp[0]; }
break;
case 57:
#line 533 "./config_parse.y"
{ yyval = yyvsp[0]; }
break;
case 58:
#line 537 "./config_parse.y"
{
			  yyval.mask = (yyvsp[-1].mask | yyvsp[0].mask);
			}
break;
case 59:
#line 540 "./config_parse.y"
{ yyval.mask = 0; }
break;
case 62:
#line 548 "./config_parse.y"
{ 
                          PROCS_CHECK( num_procs, yyvsp[-1].string );
                          SEGMENT_CHECK( segments, yyvsp[-1].string );
                          SEGMENT_SIZE_CHECK( segment_procs, yyvsp[-1].string );
                          INTERFACE_NUM_CHECK( procs_interfaces, yyvsp[-1].string );
                          Config_procs[num_procs].ifc[procs_interfaces].ip = yyvsp[0].ip.addr.s_addr;
                          Config_procs[num_procs].ifc[procs_interfaces].port = yyvsp[0].ip.port;
                          if (yyvsp[-1].mask == 0)
                                  Config_procs[num_procs].ifc[procs_interfaces].type = IFTYPE_ALL;
                          else 
                                  Config_procs[num_procs].ifc[procs_interfaces].type = yyvsp[-1].mask;
                          procs_interfaces++;
			}
break;
case 63:
#line 564 "./config_parse.y"
{ 
			  Alarm(CONF, "Successfully configured Routing Matrix for %d Segments with %d rows in the routing matrix\n",segments, rvec_num);
			}
break;
case 66:
#line 574 "./config_parse.y"
{ 
                                int rvec_element;
                                for (rvec_element = 0; rvec_element < segments; rvec_element++) {
                                        if (yyvsp[0].cost[rvec_element] < 0) yyerror("Wrong number of entries for routing matrix");
                                        LinkWeights[rvec_num][rvec_element] = yyvsp[0].cost[rvec_element];
                                }
                                rvec_num++;
                        }
break;
#line 1211 "y.tab.c"
    }
    yyssp -= yym;
    yystate = *yyssp;
    yyvsp -= yym;
    yym = yylhs[yyn];
    if (yystate == 0 && yym == 0)
    {
#if YYDEBUG
        if (yydebug)
            printf("%sdebug: after reduction, shifting from state 0 to\
 state %d\n", YYPREFIX, YYFINAL);
#endif
        yystate = YYFINAL;
        *++yyssp = YYFINAL;
        *++yyvsp = yyval;
        if (yychar < 0)
        {
            if ((yychar = yylex()) < 0) yychar = 0;
#if YYDEBUG
            if (yydebug)
            {
                yys = 0;
                if (yychar <= YYMAXTOKEN) yys = yyname[yychar];
                if (!yys) yys = "illegal-symbol";
                printf("%sdebug: state %d, reading %d (%s)\n",
                        YYPREFIX, YYFINAL, yychar, yys);
            }
#endif
        }
        if (yychar == 0) goto yyaccept;
        goto yyloop;
    }
    if ((yyn = yygindex[yym]) && (yyn += yystate) >= 0 &&
            yyn <= YYTABLESIZE && yycheck[yyn] == yystate)
        yystate = yytable[yyn];
    else
        yystate = yydgoto[yym];
#if YYDEBUG
    if (yydebug)
        printf("%sdebug: after reduction, shifting from state %d \
to state %d\n", YYPREFIX, *yyssp, yystate);
#endif
    if (yyssp >= yysslim && yygrowstack())
    {
        goto yyoverflow;
    }
    *++yyssp = yystate;
    *++yyvsp = yyval;
    goto yyloop;
yyoverflow:
    yyerror("yacc stack overflow");
yyabort:
    return (1);
yyaccept:
    return (0);
}
