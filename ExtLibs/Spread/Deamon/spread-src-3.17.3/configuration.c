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
 *  Copyright (C) 1993-2004 Spread Concepts LLC <spread@spreadconcepts.com>
 *
 *  All Rights Reserved.
 *
 * Major Contributor(s):
 * ---------------
 *    Cristina Nita-Rotaru crisn@cs.purdue.edu - group communication security.
 *    Theo Schlossnagle    jesus@omniti.com - Perl, skiplists, autoconf.
 *    Dan Schoenblum       dansch@cnds.jhu.edu - Java interface.
 *    John Schultz         jschultz@cnds.jhu.edu - contribution to process group membership.
 *
 */


/*
// example for a file:
// 4
// 3 132.27.1.0 [4803]
//	harpo	[132.28.33.22]
//	hazard
//	hal
// 4 132.28.3.0 3377
//	bih
//	binoc
// 	bbl
//	bbc
// 2 125.32.0.0 3355
// 	rb
//	rc
// 2 132.27.1.0 
//      harry
//	harmony
*/

#include "arch.h"

#ifndef	ARCH_PC_WIN95

#include <netdb.h>
#include <sys/param.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <unistd.h>

#else 	/* ARCH_PC_WIN95 */

#include <winsock.h>

#endif	/* ARCH_PC_WIN95 */

#include <stdio.h>
#include <stdlib.h>
#include <string.h> 
#include <assert.h>

#include "configuration.h"

#define ext_conf_body
#include "conf_body.h"
#undef  ext_conf_body

#include "alarm.h"
#include "memory.h"
#include "spread_params.h"

static	proc		My;

/* True means allow dangerous monitor commands like partition and flow control
 * to be handled. 
 * False means to ignore requests for those actions. THIS IS THE SAFE SETTING
 */
static  bool    EnableDangerousMonitor = FALSE;

static  port_reuse SocketPortReuse = port_reuse_auto;

static  char    *RuntimeDir = NULL;

static	char	*User = NULL;

static	char	*Group = NULL;

static  int     Link_Protocol;

int		Conf_init( char *file_name, char *my_name )
{
        struct hostent  *host_ptr;
	char	machine_name[256];
	char	ip[16];
	int	i,j;
        unsigned int name_len;
        char    configfile_location[MAXPATHLEN];
#if 0
        int     s;
	char	line[132];
	char	buf[132];
	int	full;
	char	optional_id[20];
	int	iret;
	char	*ret;
	int	p;
	int32	i1,i2,i3,i4;
#endif
	Num_procs = 0;
	/* init Config, Config_procs from file
	   init My from host
	 */
        configfile_location[0] = '\0';
        strcat(configfile_location, SPREAD_ETCDIR);
        strcat(configfile_location, "/spread.conf");

	if (NULL != (yyin = fopen(file_name,"r")) )
                Alarm( PRINT, "Conf_init: using file: %s\n", file_name);
	if (yyin == NULL) 
		if (NULL != (yyin = fopen("./spread.conf", "r")) )
                        Alarm( PRINT, "Conf_init: using file: ./spread.conf\n");
	if (yyin == NULL)
		if (NULL != (yyin = fopen(configfile_location, "r")) )
                        Alarm( PRINT, "Conf_init: using file: %s\n", configfile_location);
	if (yyin == NULL)
		Alarm( EXIT, "Conf_init: error opening config file %s\n",
			file_name);

	yyparse();
#if 0
	do{
		ret = fgets(line,132,fp);
		if (ret == NULL) 
			Alarm( EXIT, "Conf_init: no number of segments \n");
		full = sscanf( line, "%s", buf );
	}while( line[0] == '#' || full <= 0 ); 

	sscanf(line,"%d",&Config.num_segments);
	Alarm( CONF, "Conf_init: %d segments\n", Config.num_segments );
	for ( s=0; s < Config.num_segments; s++ )
	{
		do{
			ret = fgets(line,132,fp);
			if (ret == NULL) 
				Alarm( EXIT, "Conf_init: no segment data line \n");
			full = sscanf( line, "%s", buf );
		}while( line[0] == '#' || full <= 0 ); 

		for(i=0; i< 3; i++)
		{
			ret = strchr(line, '.' );
			if ( ret == NULL)
				Alarm( EXIT, "Conf_init: error in bcast addr\n");
			*ret = ' ';
		}
		iret = sscanf(line,"%d%d%d%d%d%hd",
			    	&Config.segments[s].num_procs,
				&i1,&i2,&i3,&i4,
			    	&Config.segments[s].port);
		if( iret == 5 ) Config.segments[s].port = 4803;
		else if( iret < 6 ) 
			Alarm( EXIT, "Conf_init: not a valid segment line: %s\n",
					line);

		Alarm( CONF, "Conf_init: segment %d: with %d procs, (%d.%d.%d.%d, %hd)\n",
			    s, Config.segments[s].num_procs,
			    i1,i2,i3,i4,
			    Config.segments[s].port );

		Config.segments[s].bcast_address = 
			( (i1 << 24 ) | (i2 << 16) | (i3 << 8) | i4 );

		for ( p=0; p < Config.segments[s].num_procs; p++ )
		{
		    do{
			ret = fgets(line,132,fp);
			if (ret == NULL) 
				Alarm( EXIT, "Conf_init: no proc line\n");
			full = sscanf( line, "%s", buf );
		    }while( line[0] == '#' || full <= 0 ); 

		    /* %19 is MAX_PROC_NAME-1 for the null */

		    iret = sscanf(line,"%19s%s",Config_procs[Num_procs].name,
					optional_id);
		    if( iret == 1 )
		    {
			host_ptr = gethostbyname(Config_procs[Num_procs].name);

			if ( host_ptr == 0)
				Alarm( EXIT, "Conf_init: no such host %s\n",
					Config_procs[Num_procs].name);

        		memcpy(&Config_procs[Num_procs].id, host_ptr->h_addr_list[0], 
			       sizeof(int32) );
			Config_procs[Num_procs].id = 
				htonl( Config_procs[Num_procs].id );
			i1= ( Config_procs[Num_procs].id & 0xff000000 ) >> 24;
			i2= ( Config_procs[Num_procs].id & 0x00ff0000 ) >> 16;
			i3= ( Config_procs[Num_procs].id & 0x0000ff00 ) >>  8;
			i4= Config_procs[Num_procs].id & 0x000000ff;
		    }else if( iret == 2 ){
			for(i=0; i< 3; i++)
			{
			    ret = strchr(optional_id, '.' );
			    if ( ret == NULL)
			        Alarm( EXIT, 
	"Conf_init: error in id or not a valid host line: %s\n",line);
			    *ret = ' ';
			}
			sscanf(optional_id,"%d%d%d%d",&i1,&i2,&i3,&i4);
			Config_procs[Num_procs].id = 
				( (i1 << 24 ) | (i2 << 16) | (i3 << 8) | i4 );
		    }else Alarm( EXIT, "Conf_init: not a valid host line: %s\n",
					line);

 		    Config_procs[Num_procs].port = Config.segments[s].port;
 		    Config_procs[Num_procs].seg_index = s;
 		    Config_procs[Num_procs].index_in_seg = p;
		    Config.segments[s].proc_ids[p] = 
				Config_procs[Num_procs].id;

		    Alarm( CONF, "Conf_init: \t\t%-20s\t%d.%d.%d.%d\n",
				Config_procs[Num_procs].name, i1,i2,i3,i4 );

		    Num_procs++;
		}
	}
#endif

        fclose(yyin);

        /* Test for localhost segemnt defined with other non-localhost segments.
         * That is an invalid configuration 
         */
        if ( Config.num_segments > 1 ) {
            int found_localhost = 0;
            int found_nonlocal = 0;
            for ( i=0; i < Config.num_segments; i++) {
                if ( ((Config.segments[i].bcast_address & 0xff000000) >> 24) == 127 ) {
                    found_localhost = 1;
                } else {
                    found_nonlocal = 1;
                }
            }
            if (found_nonlocal && found_localhost) {
                /* Both localhost and non-localhost segments exist. This is a non-functional config.*/
                Alarmp( SPLOG_PRINT, PRINT, "Conf_init: Invalid configuration:\n");
                Conf_print( &Config );
                Alarmp( SPLOG_PRINT, PRINT, "\n");
                Alarmp( SPLOG_FATAL, CONF, "Conf_init: Localhost segments can not be used along with regular network address segments.\nMost likely you need to remove or comment out the \nSpread_Segment 127.0.0.255 {...}\n section of your configuration file.\n");
            }
        }

	if( my_name == NULL ){
		gethostname(machine_name,sizeof(machine_name)); 
		host_ptr = gethostbyname(machine_name);
		if( host_ptr == 0 )
			Alarm( EXIT, "Conf_init: could not get my ip address (my name is %s)\n",
				machine_name );
                if (host_ptr->h_addrtype != AF_INET)
                        Alarm(EXIT, "Conf_init: Sorry, cannot handle addr types other than IPv4\n");
                if (host_ptr->h_length != 4)
                        Alarm(EXIT, "Conf_init: Bad IPv4 address length\n");
	
		i = -1;	/* in case host_ptr->h_length == 0 */
                for (j = 0; host_ptr->h_addr_list[j] != NULL; j++) {
                        memcpy(&My.id, host_ptr->h_addr_list[j], sizeof(struct in_addr));
			My.id = ntohl( My.id );
			i = Conf_proc_by_id( My.id, &My );
			if( i >= 0 ) break;
                }
		if( i < 0 ) Alarm( EXIT,
			"Conf_init: My proc id (%d.%d.%d.%d) is not in configuration\n", IP1(My.id),IP2(My.id),IP3(My.id),IP4(My.id) );

	}else if( ! strcmp( my_name, "Monitor" ) ){
		gethostname(machine_name,sizeof(machine_name)); 
		host_ptr = gethostbyname(machine_name);

		if( host_ptr == 0 )
			Alarm( EXIT, "Conf_init: no such monitor host %s\n",
				machine_name );

        	memcpy(&My.id, host_ptr->h_addr_list[0], 
			sizeof(int32) );
		My.id = ntohl( My.id );

		name_len = strlen( machine_name );
		if( name_len > sizeof(My.name) ) name_len = sizeof(My.name);
		memcpy(My.name, machine_name, name_len );
		Alarm( CONF, "Conf_init: My name: %s, id: %d\n",
			My.name, My.id );
		return( 1 );
	}else{
		name_len = strlen( my_name );
		if( name_len > sizeof(My.name) ) name_len = sizeof(My.name);
		memcpy(My.name, my_name, name_len );
		i = Conf_proc_by_name( My.name, &My );
		if( i < 0  ) Alarm( EXIT,
				"Conf_init: My proc %s is not in configuration \n",
				My.name);

	}

	Conf_id_to_str( My.id, ip );
	Alarm( CONF, "Conf_init: My name: %s, id: %s, port: %hd\n",
		My.name, ip, My.port );

	return( 0 );
}

configuration	Conf()
{
	return Config;
}

proc	Conf_my()
{
	return	My;
}

void    Conf_set_link_protocol(int protocol)
{
        if (protocol < 0 || protocol >= MAX_PROTOCOLS) {
                Alarm(PRINT, "Conf_set_link_protocol: Illegal protocol type %d\n", protocol);
                return;
        }
        Link_Protocol = protocol;
}

int     Conf_get_link_protocol(void)
{
        return(Link_Protocol);
}


int	Conf_proc_by_id( int32u id, proc *p )
{
	int	i,j;

	for ( i=0; i < Num_procs; i++ )
	{
                for ( j=0; j < Config_procs[i].num_if; j++)
                {
                        if ( Config_procs[i].ifc[j].ip == id )
                        {
                                *p =  Config_procs[i] ;
                                return( i );
                        }
                }
	}
	return( -1 );
}

int 	Conf_proc_by_name( char *name, proc *p )
{
	int	i;

	for ( i=0; i < Num_procs; i++ )
	{
		if ( strcmp( Config_procs[i].name, name ) == 0 )
		{
			*p = Config_procs[i];
			return( i );
		}
	}
	return( -1 );
}

int	Conf_id_in_seg( segment *seg, int32u id )
{
	int 	i,j;

	for ( j=0; j < seg->num_procs; j++ )
	{
                for ( i=0; i < seg->procs[j]->num_if; i++)
                {
                        if ( seg->procs[j]->ifc[i].ip == id )
                                return( j );
                }
	}
	return( -1 );
}
static  int     Conf_proc_ref_by_id( int32u id, proc **p )
{
	int	i,j;

	for ( i=0; i < Num_procs; i++ )
	{
                for ( j=0; j < Config_procs[i].num_if; j++)
                {
                        if ( Config_procs[i].ifc[j].ip == id )
                        {
                                *p = &Config_procs[i];
                                return( i );
                        }
                }
	}
	return( -1 );
}

int     Conf_append_id_to_seg( segment *seg, int32u id)
{
        proc *p;
        if (Conf_proc_ref_by_id(id, &p) != -1)
        {
                seg->procs[seg->num_procs] = p;
                seg->num_procs++;
                return( 0 );
        } 
        return( -1 );
}
int	Conf_id_in_conf( configuration *config, int32u id )
{
	int 	i;

	for ( i=0; i < config->num_segments; i++ )
                if ( Conf_id_in_seg(&(config->segments[i]), id) >= 0 )
                        return( i );
	return( -1 );
}

int	Conf_num_procs( configuration *config )
{
	int 	i,ret;

	ret = 0;
	for ( i=0; i < config->num_segments; i++ )
		ret += config->segments[i].num_procs;

	return( ret );
}

int32u	Conf_leader( configuration *config )
{
        int i;

        for( i=0; i < config->num_segments; i++ )
        {
                if( config->segments[i].num_procs > 0 )
                        return( config->segments[i].procs[0]->id );
        }
        Alarm( EXIT, "Conf_leader: Empty configuration %c",Conf_print(config));
	return( -1 );
}

int32u	Conf_last( configuration *config )
{
        int i,j;

        for( i = config->num_segments-1; i >= 0; i-- )
        {
                if( config->segments[i].num_procs > 0 )
		{
			j = config->segments[i].num_procs-1;
                        return( config->segments[i].procs[j]->id );
		}
        }
        Alarm( EXIT, "Conf_last: Empty configuration %c",Conf_print(config));
	return( -1 );
}

int32u	Conf_seg_leader( configuration *config, int16 seg_index )
{
	if( config->segments[seg_index].num_procs > 0 )
	{
		return( config->segments[seg_index].procs[0]->id );
	}
        Alarm( EXIT, "Conf_seg_leader: Empty segment %d in Conf %c",
		seg_index, Conf_print(config));
	return( -1 );
}

int32u	Conf_seg_last( configuration *config, int16 seg_index )
{
	int	j;

	if( config->segments[seg_index].num_procs > 0 )
	{
		j = config->segments[seg_index].num_procs-1;
		return( config->segments[seg_index].procs[j]->id );
	}
        Alarm( EXIT, "Conf_seg_leader: Empty segment %d in Conf %c",
		seg_index, Conf_print(config));
        return(-1);
}

int	Conf_num_procs_in_seg( configuration *config, int16 seg_index )
{
	return( config->segments[seg_index].num_procs );
}

void	Conf_id_to_str( int32u id, char *str )
{
	int32u	i1,i2,i3,i4;

	i1 = (id & 0xff000000) >> 24;
	i2 = (id & 0x00ff0000) >> 16;
	i3 = (id & 0x0000ff00) >> 8;
	i4 = (id & 0x000000ff);
	sprintf( str, "%u.%u.%u.%u", i1, i2, i3, i4 );
}

char	Conf_print(configuration *config)
{
	int 	s,p,ret;
	char	ip[16];
	proc	pr;

	Alarm( PRINT, "--------------------\n" );
	Alarm( PRINT, "Configuration at %s is:\n", My.name );
	Alarm( PRINT, "Num Segments %d\n",config->num_segments );
	for ( s=0; s < config->num_segments; s++ )
	{
		Conf_id_to_str( config->segments[s].bcast_address, ip );
		Alarm( PRINT, "\t%d\t%-16s  %hd\n",
			config->segments[s].num_procs, ip,
			config->segments[s].port );
		for( p=0; p < config->segments[s].num_procs; p++)
		{
			ret = Conf_proc_by_id( config->segments[s].procs[p]->id,
					&pr );	
			Conf_id_to_str( pr.id, ip );
			Alarm( PRINT, "\t\t%-20s\t%-16s\n", pr.name, ip );
		}
	}
	Alarm( PRINT, "====================" );
	return( '\n' );
}

bool    Conf_get_dangerous_monitor_state(void)
{
        return(EnableDangerousMonitor);
}

void    Conf_set_dangerous_monitor_state(bool new_state)
{
        if (new_state == FALSE) {
                Alarm(PRINT, "disabling Dangerous Monitor Commands!\n");
        } else if (new_state == TRUE) {
                Alarm(PRINT, "ENABLING Dangerous Monitor Commands! Make sure Spread network is secured\n");
        } else {
                /* invalid setting */
                return;
        }
        EnableDangerousMonitor = new_state;
}

port_reuse Conf_get_port_reuse_type(void)
{
        return(SocketPortReuse);
}

void    Conf_set_port_reuse_type(port_reuse state)
{
        switch (state)
        {
        case port_reuse_auto:
                Alarm(PRINT, "Setting SO_REUSEADDR to auto\n");
                break;
        case port_reuse_on:
                Alarm(PRINT, "Setting SO_REUSEADDR to always on -- make sure Spread daemon host is secured!\n");
                break;
        case port_reuse_off:
                Alarm(PRINT, "Setting SO_REUSEADDR to always off\n");
                break;
        default:
                /* Inavlid type -- ignored */
                return;
        }
        SocketPortReuse = state;
}

static void set_param_if_valid(char **param, char *value, char *description, unsigned int max_value_len)
{
        if (value != NULL && *value != '\0')
        {
                unsigned int len = strlen(value);
                char *old_value = *param;
                char *buf;
                if (len > max_value_len)
                {
                    Alarm(EXIT, "set_param_if_valid: value string too long\n");
                }
                buf = Mem_alloc(len + 1);
                if (buf == NULL)
                {
                        Alarm(EXIT, "set_param_if_valid: Out of memory\n");
                }
                strncpy(buf, value, len);
                buf[len] = '\0';

                *param = buf;
                if (old_value != NULL)
                {
                    dispose(old_value);
                }
                Alarm(PRINT, "Set %s to '%s'\n", description, value);
        }
        else
        {
                Alarm(DEBUG, "Ignored invalid %s\n", description);
        }
}

char    *Conf_get_runtime_dir(void)
{
        return (RuntimeDir != NULL ? RuntimeDir : SP_RUNTIME_DIR);
}

void    Conf_set_runtime_dir(char *dir)
{
        set_param_if_valid(&RuntimeDir, dir, "runtime directory", MAXPATHLEN);
}

char    *Conf_get_user(void)
{
        return (User != NULL ? User : SP_USER);
}

void    Conf_set_user(char *user)
{
        set_param_if_valid(&User, user, "user name", 32);
}

char    *Conf_get_group(void)
{
        return (Group != NULL ? Group : SP_GROUP);
}

void    Conf_set_group(char *group)
{
        set_param_if_valid(&Group, group, "group name", 32);
}
