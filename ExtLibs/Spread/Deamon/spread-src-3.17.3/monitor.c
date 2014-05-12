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


#define	status_ext

#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <errno.h>

#include "arch.h"
#include "mutex.h"

#ifdef _REENTRANT

#ifndef 	ARCH_PC_WIN95

#include        <sys/types.h>
#include        <sys/socket.h>
#include 	<pthread.h>

#else		/* ARCH_PC_WIN95 */

#include	<windows.h>
#include        <winsock.h>
#define ioctl   ioctlsocket
WSADATA		WSAData;

#endif		/* ARCH_PC_WIN95 */

#endif /* _REENTRANT */


#include "scatter.h"
#include "configuration.h"
#include "sp_events.h"
#include "status.h"
#include "net_types.h"
#include "data_link.h"
#include "alarm.h"

static	channel		SendChan;
static	sp_time		Send_partition_timeout = { 25, 0 };
static	sp_time		Send_status_timeout;
static  int             Status_active = 0;
static  int             Partition_active = 0;
static	sys_scatter	Pack_scat;
static	packet_header	Pack;
static	int16		Partition[MAX_PROCS_RING];
static	int16		Work_partition[MAX_PROCS_RING];

static	int16		Fc_buf[MAX_PROCS_RING];
static	int16		Work_fc_buf[MAX_PROCS_RING];

static	int		Status_vector[MAX_PROCS_RING];

static	int		Status_vector[MAX_PROCS_RING];

static	configuration	Cn;
static  proc            My;
static	int		My_port;
static	char		*My_name;
static	char		My_name_buf[80];
static  char		Config_file[80];

static	sys_scatter	Report_scat;
static	packet_header	Report_pack;
static  channel         Report_socket;

static	void	Print_menu();

static	void	User_command();
static	void	Define_partition();
static	void	Send_partition();
static	void	Kill_spreads();
static	void	Print_partition( int16 partition[MAX_PROCS_RING] );

static	void	Define_flow_control();
static	void	Send_flow_control();
static	void	Print_flow_control( int16 fc_buf[MAX_PROCS_RING] );

static  void	Activate_status();
static	void	Send_status_query();

static	void	Report_message();

#ifdef	_REENTRANT

#ifndef     ARCH_PC_WIN95
    static      mutex_type	Init_mutex = MUTEX_STATIC_INIT;
    static      pthread_t	Read_thread;
    static      pthread_t	Status_thread;
    static      pthread_t	Partition_thread;
    static      void            *Read_thread_routine();
    static      void            *Status_send_thread_routine();
    static      void            *Partition_send_thread_routine();
#else		/* ARCH_PC_WIN95 */
    static	mutex_type	Init_mutex = {MUTEX_STATIC_INIT};
    static	HANDLE		Read_thread;
    static	HANDLE		Status_thread;
    static	HANDLE		Partition_thread;
    static	DWORD WINAPI    Read_thread_routine( void *);
    static	DWORD WINAPI    Status_send_thread_routine( void *);
    static	DWORD WINAPI    Partition_send_thread_routine( void *);
#endif		/* ARCH_PC_WIN95 */

static	mutex_type	Status_mutex;
static  mutex_type      Partition_mutex;

#endif /* _REENTRANT */


static  void    Usage( int argc, char *argv[] );
static  void    initialize_locks(void);

int main( int argc, char *argv[] )
{
	int	i;
#ifdef _REENTRANT
        int     ret;
#endif
	fclose(stderr);

	Alarm_set_types( NONE ); 

	Alarm( PRINT, "/===========================================================================\\\n");
	Alarm( PRINT, "| The Spread Toolkit.                                                       |\n");
	Alarm( PRINT, "| Copyright (c) 1993-2001 Spread Concepts LLC                               |\n"); 
	Alarm( PRINT, "| All rights reserved.                                                      |\n");
	Alarm( PRINT, "|                                                                           |\n");
	Alarm( PRINT, "| The Spread package is licensed under the Spread Open-Source License.      |\n");
	Alarm( PRINT, "| You may only use this software in compliance with the License.            |\n");
	Alarm( PRINT, "| A copy of the license can be found at http://www.spread.org/license       |\n");
        Alarm( PRINT, "|                                                                           |\n");
        Alarm( PRINT, "| This product uses software developed by Spread Concepts LLC for use       |\n");
        Alarm( PRINT, "| in the Spread toolkit. For more information about Spread,                 |\n");
        Alarm( PRINT, "| see http://www.spread.org                                                 |\n");
	Alarm( PRINT, "|                                                                           |\n");
	Alarm( PRINT, "| This software is distributed on an \"AS IS\" basis, WITHOUT WARRANTY OF     |\n");
	Alarm( PRINT, "| ANY KIND, either express or implied.                                      |\n");
	Alarm( PRINT, "|                                                                           |\n");
	Alarm( PRINT, "| Spread is developed at Spread Concepts LLC and the Center for Networking  |\n");
 	Alarm( PRINT, "| and Distributed Systems, The Johns Hopkins University.                    |\n");
	Alarm( PRINT, "|                                                                           |\n");
	Alarm( PRINT, "| Creators:                                                                 |\n");
	Alarm( PRINT, "|    Yair Amir             yairamir@cs.jhu.edu                              |\n");
	Alarm( PRINT, "|    Michal Miskin-Amir    michal@spread.org                                |\n");
	Alarm( PRINT, "|    Jonathan Stanton      jstanton@gwu.edu                                 |\n");
	Alarm( PRINT, "|                                                                           |\n");
	Alarm( PRINT, "| Contributors:                                                             |\n");
        Alarm( PRINT, "|    Cristina Nita-Rotaru crisn@cs.purdue.edu - GC security.                |\n");
        Alarm( PRINT, "|    Theo Schlossnagle    jesus@omniti.com - Perl, skiplists, autoconf.     |\n");
	Alarm( PRINT, "|    Dan Schoenblum   dansch@cnds.jhu.edu - Java Interface Developer.       |\n");
        Alarm( PRINT, "|    John Schultz     jschultz@cnds.jhu.edu - contribution to process group |\n");
        Alarm( PRINT, "|                                             membership.                   |\n");	
	Alarm( PRINT, "| |\n");
	Alarm( PRINT, "| Special thanks to the following for providing ideas and/or code:          |\n");
	Alarm( PRINT, "|    Ken Birman, Danny Dolev, David Shaw, Robbert VanRenesse.               |\n");
	Alarm( PRINT, "|                                                                           |\n");
	Alarm( PRINT, "| WWW:     www.spread.org     www.cnds.jhu.edu    www.spreadconcepts.com    |\n");
	Alarm( PRINT, "| Contact: spread@spread.org                                                |\n");
	Alarm( PRINT, "|                                                                           |\n");
	Alarm( PRINT, "| Version %d.%02d.%02d Built 15/October/2004                                     |\n", 
		(int)SP_MAJOR_VERSION, (int)SP_MINOR_VERSION, (int)SP_PATCH_VERSION);
	Alarm( PRINT, "\\===========================================================================/\n");

	Usage( argc, argv );
        
        Alarm_set_interactive();
	Conf_init( Config_file, My_name );
	Cn = Conf();
        My = Conf_my();

        Alarm_clear_types(ALL);
        Alarm_set_types(PRINT | EXIT );

#ifdef ARCH_PC_WIN95
        ret = WSAStartup( MAKEWORD(2,0), &WSAData );
        if( ret != 0 )
            Alarm( EXIT, "sptmonitor: main: winsock initialization error %d\n", ret );
#endif	/* ARCH_PC_WIN95 */

        initialize_locks();

	for( i=0; i < Conf_num_procs( &Cn ); i++ )
		Partition[i] = 0;

	for( i=0; i < Conf_num_procs( &Cn ); i++ )
		Status_vector[i] = 0;

	Pack_scat.elements[0].len = sizeof( packet_header );
	Pack_scat.elements[0].buf = (char *)&Pack;

	Pack.proc_id = My.id;
	Pack.seq = My_port;
	Pack.memb_id.proc_id = 15051963;

	Report_scat.num_elements = 2;
	Report_scat.elements[0].buf = (char *)&Report_pack;
	Report_scat.elements[0].len = sizeof(packet_header);
	Report_scat.elements[1].buf = (char *)&GlobalStatus;
	Report_scat.elements[1].len = sizeof(status);

        SendChan = DL_init_channel( SEND_CHANNEL , My_port, 0, 0 );

        Report_socket = DL_init_channel( RECV_CHANNEL, My_port, 0, 0 );

	E_init(); /* both reentrent and non-reentrant code uses events */

#ifndef	_REENTRANT
	E_attach_fd( 0, READ_FD, User_command, 0, NULL, LOW_PRIORITY );

        E_attach_fd( Report_socket, READ_FD, Report_message, 0, NULL, HIGH_PRIORITY );
#endif	/* _REENTRANT */

        
	Print_menu();

#ifdef	_REENTRANT

#ifndef	        ARCH_PC_WIN95
	ret = pthread_create( &Read_thread, NULL, Read_thread_routine, 0 );
	ret = pthread_create( &Status_thread, NULL, Status_send_thread_routine, 0 );
	ret = pthread_create( &Partition_thread, NULL, Partition_send_thread_routine, 0 );
#else		/* ARCH_PC_WIN95 */
	Read_thread = CreateThread( NULL, 0, Read_thread_routine, NULL, 0, &ret );
	Status_thread = CreateThread( NULL, 0, Status_send_thread_routine, NULL, 0, &ret );
	Partition_thread = CreateThread( NULL, 0, Partition_send_thread_routine, NULL, 0, &ret );
#endif		/* ARCH_PC_WIN95 */

	for(;;)
	{
		User_command();
	}

#else	/*! _REENTRANT */

	E_handle_events();

#endif	/* _REENTRANT */

	return 0;
}

static  void    initialize_locks(void)
{
        int ret;

	ret = Mutex_trylock( &Init_mutex );
	if( ret == 0 )
	{
		/* 
		 * we managed to lock the Init_mutex. This means we are the first thread
		 * to get here.
		 */

		Mutex_init( &Status_mutex );
		Mutex_init( &Partition_mutex );
        }
        return;
}

#ifdef	_REENTRANT

#ifndef 	ARCH_PC_WIN95
static	void	*Read_thread_routine()
#else		/* ARCH_PC_WIN95 */
static	DWORD WINAPI    Read_thread_routine( void *dummy)
#endif		/* ARCH_PC_WIN95 */
{
	for(;;)
	{
            Report_message( Report_socket, 0, NULL);
	}
	return( 0 );
}

#endif	/* _REENTRANT */

static	void	User_command()
{
	char	command[80];
	int	i;

	if( fgets( command,70,stdin ) == NULL )
	{
		printf("Bye.\n");
		exit( 0 );
	}

	switch( command[0] )
	{
		case '0':
			Activate_status();

			printf("\n");
			printf("Monitor> ");
			fflush(stdout);

			break;

		case '1':
			Define_partition();
			Print_partition( Work_partition );

			break;

		case '2':
			for( i=0; i < Conf_num_procs( &Cn ); i++ )
				Partition[i] = Work_partition[i];
                        Mutex_lock( &Partition_mutex );
                        Partition_active = 1;
                        Mutex_unlock( &Partition_mutex );
#ifndef _REENTRANT
			Send_partition();
#endif

			printf("\n");
			printf("Monitor> ");
			fflush(stdout);

			break;

		case '3':
			Print_partition( Partition );

			break;

		case '4':
			for( i=0; i < Conf_num_procs( &Cn ); i++ )
			{
				Partition[i] = 0;
				Work_partition[i] = 0;
			}
                        Mutex_lock( &Partition_mutex );
                        Partition_active = 0;
                        Mutex_unlock( &Partition_mutex );

			Send_partition();
#ifndef _REENTRANT
			E_dequeue( Send_partition, 0, NULL );
#endif
			printf("\n");
			printf("Monitor> ");
			fflush(stdout);

			break;

		case '5':
			Define_flow_control();
			Print_flow_control( Work_fc_buf );

			break;

		case '6':
			for( i=0; i < Conf_num_procs( &Cn )+1; i++ )
				Fc_buf[i] = Work_fc_buf[i];
			Send_flow_control();

			printf("\n");
			printf("Monitor> ");
			fflush(stdout);

			break;

		case '7':
			Print_flow_control( Fc_buf );

			break;

		case '8':
			Kill_spreads();

			printf("\n");
			printf("Monitor> ");
			fflush(stdout);

			break;

		case '9':
		case 'q':
			printf("Bye.\n");
			exit( 0 );

			break;

		default:
			printf("\nUnknown commnad\n");
			Print_menu();

			break;
	}
}

static	void	Print_menu()
{
	printf("\n");
	printf("=============\n");
	printf("Monitor Menu:\n");
	printf("-------------\n");
	printf("\t0. Activate/Deactivate Status {all, none, Proc, CR}\n");
	printf("\n");
	printf("\t1. Define Partition\n");
	printf("\t2. Send   Partition\n");
	printf("\t3. Review Partition\n");
	printf("\t4. Cancel Partition Effects\n");
	printf("\n");
	printf("\t5. Define Flow Control\n");
	printf("\t6. Send   Flow Control\n");
	printf("\t7. Review Flow Control\n");
	printf("\n");
	printf("\t8. Terminate Spread Daemons {all, none, Proc, CR}\n");
	printf("\n");
	printf("\t9. Exit\n");
	printf("\n");
	printf("Monitor> ");
	fflush(stdout);
}

static	void	Print_partition( int16 partition[MAX_PROCS_RING] )
{
	int32	proc_id;
	proc	p;
	int	proc_index;
	int	i,j;

	printf("\n");
	printf("=============\n");
	printf("Partition Map:\n");
	printf("-------------\n");

	printf("\n");
	for( i=0; i < Cn.num_segments ; i++ )
	{
	    for( j=0; j < Cn.segments[i].num_procs; j++ )
	    {
		proc_id = Cn.segments[i].procs[j]->id;
		proc_index = Conf_proc_by_id( proc_id, &p );
		printf("\t%s\t%d\n", p.name, partition[proc_index] );
	    }
	    printf("\n");
	}
	printf("\n");
	printf("Monitor> ");
	fflush(stdout);
}

static	void	Define_partition()
{
	int32	proc_id;
	proc	p;
	int	proc_index;
	char	str[80];
	int	legal,ret,temp;
	int	i,j;

	printf("\n");
	printf("=============\n");
	printf("Define Partition\n");
	printf("-------------\n");

	printf("\n");
	for( i=0; i < Cn.num_segments ; i++ )
	{
	    for( j=0; j < Cn.segments[i].num_procs; j++ )
	    {
		proc_id = Cn.segments[i].procs[j]->id;
		proc_index = Conf_proc_by_id( proc_id, &p );
		for( legal=0; !legal; )
		{
		    printf("\t%s\t", p.name);

		    if( fgets( str, 70, stdin ) == NULL )
		    {
			printf("Bye.\n");
			exit(0);
		    }
		    ret = sscanf(str, "%d", &temp );
		    Work_partition[proc_index] = temp;
		    if( ret > 0 ) legal = 1;
		    else printf("Please enter a number\n");
		}
	    }
	    printf("\n");
	}
}

static	void	Send_partition()
{
	int32	proc_id;
	proc	p;
	int	proc_index;
	int	i,j;

	Pack.type    = PARTITION_TYPE;
	Pack.type    = Set_endian( Pack.type );
	Pack.data_len= sizeof( Partition );;

	Pack_scat.num_elements    = 2;
	Pack_scat.elements[1].len = sizeof( Partition );
	Pack_scat.elements[1].buf = (char *)&Partition;

	Alarm( PRINT  , "Monitor: send partition\n");
	for( i=0; i < Cn.num_segments ; i++ )
	{
	    for( j=0; j < Cn.segments[i].num_procs; j++ )
	    {
		proc_id = Cn.segments[i].procs[j]->id;
		proc_index = Conf_proc_by_id( proc_id, &p );
		DL_send( SendChan, p.id, p.port, &Pack_scat );
		DL_send( SendChan, p.id, p.port, &Pack_scat );
	    }
	}
#ifndef _REENTRANT
	E_queue( Send_partition, 0, NULL, Send_partition_timeout );
#endif
}

#ifdef	_REENTRANT

#ifndef 	ARCH_PC_WIN95
static	void	*Partition_send_thread_routine()
#else		/* ARCH_PC_WIN95 */
static	DWORD WINAPI    Partition_send_thread_routine( void *dummy)
#endif		/* ARCH_PC_WIN95 */
{
    sp_time onesecond_time = { 1, 0};
    sp_time send_interval;
    int active_p;

    for(;;)
    {
        Mutex_lock( &Partition_mutex );
        active_p = Partition_active;
        send_interval = Send_partition_timeout;
        Mutex_unlock( &Partition_mutex );
        if (active_p) {
            Send_partition();

            E_delay(send_interval); 
        } else {
            E_delay(onesecond_time);
        }
    }
    return( 0 );
}

#endif	/* _REENTRANT */

static	void	Print_flow_control( int16 fc_buf[MAX_PROCS_RING] )
{
	int32	proc_id;
	proc	p;
	int	proc_index;
	int	i,j;

	printf("\n");
	printf("========================\n");
	printf("Flow Control Parameters:\n");
	printf("------------------------\n");
	printf("\n");
	printf("Window size:  %d\n",fc_buf[ Conf_num_procs( &Cn )]);
	printf("\n");
	for( i=0; i < Cn.num_segments ; i++ )
	{
	    for( j=0; j < Cn.segments[i].num_procs; j++ )
	    {
		proc_id = Cn.segments[i].procs[j]->id;
		proc_index = Conf_proc_by_id( proc_id, &p );
		printf("\t%s\t%d\n", p.name, fc_buf[proc_index] );
	    }
	    printf("\n");
	}
	printf("\n");
	printf("Monitor> ");
	fflush(stdout);
}

static	void	Define_flow_control()
{
	int32	proc_id;
	proc	p;
	int	proc_index;
	char	str[80];
	int	legal,ret,temp;
	int	i,j;

	printf("\n");
	printf("===================\n");
	printf("Define Flow Control\n");
	printf("-------------------\n");

	printf("\n");
	for( legal=0; !legal; )
	{
	    printf("    Window size: ");

	    if( fgets( str,70,stdin ) == NULL )
	    {
		printf("Bye.\n");
		exit(0);
	    }
	    ret = sscanf(str, "%d", &temp );
	    Work_fc_buf[Conf_num_procs( &Cn )] = temp;
	    if( ret > 0 ) legal = 1;
	    else if( ret == -1 ){
		legal = 1;
		Work_fc_buf[Conf_num_procs( &Cn )] = -1;
	    }else printf("Please enter a number\n");
	}
	printf("\n");
	for( i=0; i < Cn.num_segments ; i++ )
	{
	    for( j=0; j < Cn.segments[i].num_procs; j++ )
	    {
		proc_id = Cn.segments[i].procs[j]->id;
		proc_index = Conf_proc_by_id( proc_id, &p );
		for( legal=0; !legal; )
		{
		    printf("\t%s\t", p.name);

		    if( fgets( str, 70, stdin ) == NULL )
		    {
			printf("Bye.\n");
			exit(0);
		    }
		    ret = sscanf(str, "%d", &temp);
		    Work_fc_buf[proc_index] = temp;
		    if( ret > 0 ) legal = 1;
		    else if( ret == -1 ){
			legal = 1;
			Work_fc_buf[proc_index] = -1;
		    }else printf("Please enter a number\n");
		}
	    }
	    printf("\n");
	}
}

static	void	Send_flow_control()
{
	int32	proc_id;
	proc	p;
	int	proc_index;
	int	i,j;

	Pack.type    = FC_TYPE;
	Pack.type    = Set_endian( Pack.type );
	Pack.data_len= sizeof( Fc_buf );;

	Pack_scat.num_elements    = 2;
	Pack_scat.elements[1].len = sizeof( Fc_buf );
	Pack_scat.elements[1].buf = (char *)&Fc_buf;

	Alarm( PRINT  , "Monitor: send flow control params\n");
	for( i=0; i < Cn.num_segments ; i++ )
	{
	    for( j=0; j < Cn.segments[i].num_procs; j++ )
	    {
		proc_id = Cn.segments[i].procs[j]->id;
		proc_index = Conf_proc_by_id( proc_id, &p );
		DL_send( SendChan, p.id, p.port, &Pack_scat );
		DL_send( SendChan, p.id, p.port, &Pack_scat );
	    }
	}
}

static	void	Activate_status()
{
	proc	p;
	int	proc_index;
	char	str[80];
	int	legal,ret;
	int	i;
	int	end;

	printf("\n");
	printf("=============\n");
	printf("Activate Status\n");
	printf("-------------\n");

	printf("\n");

	end = 0;
	while( !end )
	{
		for( legal=0; !legal; )
		{
		    printf("\tEnter Proc Name: ");

		    if( fgets( str, 70, stdin ) == NULL )
		    {
			printf("Bye.\n");
			exit(0);
		    }
		    ret = sscanf(str, "%s", p.name );
		    if( ret > 0  || str[0] == '\n' ) legal = 1;
		    else printf("Please enter a legal proc name, none, or all\n");
		}
		if( str[0] == '\n' ){
			end = 1;
		}else if( !strcmp( p.name, "all" ) ){
			for( i=0; i < Conf_num_procs( &Cn ); i++ )
				Status_vector[i] = 1;
		}else if( !strcmp( p.name, "none" ) ){
			for( i=0; i < Conf_num_procs( &Cn ); i++ )
				Status_vector[i] = 0;
		}else{
			proc_index = Conf_proc_by_name( p.name, &p );
			if( proc_index != -1 ){
				Status_vector[proc_index] = 1;
			}else printf("Please! enter a legal proc name, none, or all\n");
		}
	}
#ifndef _REENTRANT
	E_dequeue( Send_status_query, 0, NULL );
#endif
        Mutex_lock( &Status_mutex );
        Status_active = 0;
	for( i=0; i < Conf_num_procs( &Cn ); i++ )
	{
		if( Status_vector[i] )
		{
                    Status_active = 1;
			break;
		}
	}
        Mutex_unlock( &Status_mutex );
#ifndef _REENTRANT
        if (Status_active)
            Send_status_query();
#endif
}

static	void 	Send_status_query()
{

	int32	proc_id;
	proc	p;
	int	proc_index;
	int	i,j;

	Pack.type    = STATUS_TYPE;
	Pack.type    = Set_endian( Pack.type );
	Pack.data_len= 0;

	Pack_scat.num_elements    = 1;

	Alarm( PRINT  , "Monitor: send status query\n");
	for( i=0; i < Cn.num_segments ; i++ )
	{
	    for( j=0; j < Cn.segments[i].num_procs; j++ )
	    {
		proc_id = Cn.segments[i].procs[j]->id;
		proc_index = Conf_proc_by_id( proc_id, &p );
		if( Status_vector[proc_index] )
		{
			DL_send( SendChan, p.id, p.port, &Pack_scat );
		}
	    }
	}
#ifndef _REENTRANT
	E_queue( Send_status_query, 0, NULL, Send_status_timeout );
#endif
}

#ifdef	_REENTRANT

#ifndef 	ARCH_PC_WIN95
static	void	*Status_send_thread_routine()
#else		/* ARCH_PC_WIN95 */
static	DWORD WINAPI    Status_send_thread_routine( void *dummy)
#endif		/* ARCH_PC_WIN95 */
{
    sp_time onesecond_time = { 1, 0};
    sp_time send_interval;
    int active_p;

    for(;;)
    {
        Mutex_lock( &Status_mutex );
        active_p = Status_active;
        send_interval = Send_status_timeout;
        Mutex_unlock( &Status_mutex );
        if (active_p) {
            Send_status_query();

            E_delay(send_interval); 
        } else {
            E_delay(onesecond_time);
        }
    }
    return( 0 );
}

#endif	/* _REENTRANT */

static	void	Kill_spreads()
{
	int16	Kill_partition[MAX_PROCS_RING];
	proc	p;
	int	proc_index;
	int32	proc_id;
	char	str[80];
	int	legal, ret;
	int	i, j;
	int	end;

	for( i=0; i < MAX_PROCS_RING; i++ )
		Kill_partition[i] = 0;

        end = 0;
        while( !end )
        {
                for( legal=0; !legal; )
                {
                    printf("\tEnter Proc Name to Terminate: ");

                    if( fgets( str, 70, stdin ) == NULL )
                    {
                        printf("Bye.\n");
                        exit(0);
                    }
                    ret = sscanf(str, "%s", p.name );
                    if( ret > 0  || str[0] == '\n' ) legal = 1;
                    else printf("Please enter a legal proc name, none, or all\n");
                }
                if( str[0] == '\n' ){
                        end = 1;
                }else if( !strcmp( p.name, "all" ) ){
                        for( i=0; i < Conf_num_procs( &Cn ); i++ )
                                Kill_partition[i] = -1;
                }else if( !strcmp( p.name, "none" ) ){
                        for( i=0; i < Conf_num_procs( &Cn ); i++ )
                                Kill_partition[i] = 0;
                }else{
                        proc_index = Conf_proc_by_name( p.name, &p );
                        if( proc_index != -1 ){
                                Kill_partition[proc_index] = -1;
                        }else printf("Please! enter a legal proc name, none, or all\n");
                }
        }
	for( i=0; i < Conf_num_procs( &Cn ); i++ )
	{
		if( Kill_partition[i] != -1 ) Kill_partition[i] = Partition[i];
	}
	Pack.type    = PARTITION_TYPE;
	Pack.type    = Set_endian( Pack.type );
	Pack.data_len= sizeof( Kill_partition );;

	Pack_scat.num_elements    = 2;
	Pack_scat.elements[1].len = sizeof( Kill_partition );
	Pack_scat.elements[1].buf = (char *)&Kill_partition;

	for( i=0; i < Cn.num_segments ; i++ )
	{
	    for( j=0; j < Cn.segments[i].num_procs; j++ )
	    {
		proc_id = Cn.segments[i].procs[j]->id;
		proc_index = Conf_proc_by_id( proc_id, &p );
		if( Kill_partition[proc_index] == -1 )
		{
			Alarm( PRINT  , "Monitor: Terminating %s\n", p.name );
			DL_send( SendChan, p.id, p.port, &Pack_scat );
			DL_send( SendChan, p.id, p.port, &Pack_scat );
		}
	    }
	}
}

static	void	Report_message(mailbox fd, int dummy, void *dummy_p)
{
	proc	p;
	proc	leader_p;
	int	ret;
	int	ret1,ret2;
static	int32	last_mes;
static	int32	last_aru;
static	int32	last_sec;

	last_mes = GlobalStatus.message_delivered;
	last_aru = GlobalStatus.aru;
	last_sec = GlobalStatus.sec;

	ret = DL_recv( fd, &Report_scat );
	if( ret <= 0 ) {
            Alarm( DEBUG, "Report_messsage: DL_recv failed with ret %d, errno %d\n", ret, sock_errno);
            return;
        }

	if( !Same_endian( Report_pack.type ) )
	{
		GlobalStatus.sec		= Flip_int32( GlobalStatus.sec );
		GlobalStatus.state		= Flip_int32( GlobalStatus.state );
		GlobalStatus.gstate		= Flip_int32( GlobalStatus.gstate );
		GlobalStatus.packet_sent	= Flip_int32( GlobalStatus.packet_sent );
		GlobalStatus.packet_recv	= Flip_int32( GlobalStatus.packet_recv );
		GlobalStatus.packet_delivered   = Flip_int32( GlobalStatus.packet_delivered );
		GlobalStatus.retrans		= Flip_int32( GlobalStatus.retrans );
		GlobalStatus.u_retrans	        = Flip_int32( GlobalStatus.u_retrans );
		GlobalStatus.s_retrans	        = Flip_int32( GlobalStatus.s_retrans );
		GlobalStatus.b_retrans	        = Flip_int32( GlobalStatus.b_retrans );
		GlobalStatus.aru		= Flip_int32( GlobalStatus.aru );
		GlobalStatus.my_aru		= Flip_int32( GlobalStatus.my_aru );
		GlobalStatus.highest_seq	= Flip_int32( GlobalStatus.highest_seq );
		GlobalStatus.token_hurry	= Flip_int32( GlobalStatus.token_hurry );
		GlobalStatus.token_rounds	= Flip_int32( GlobalStatus.token_rounds );
		GlobalStatus.my_id		= Flip_int32( GlobalStatus.my_id );
		GlobalStatus.leader_id	        = Flip_int32( GlobalStatus.leader_id );
		GlobalStatus.message_delivered  = Flip_int32( GlobalStatus.message_delivered );
		GlobalStatus.membership_changes = Flip_int16( GlobalStatus.membership_changes);
		GlobalStatus.num_procs	        = Flip_int16( GlobalStatus.num_procs );
		GlobalStatus.num_segments	= Flip_int16( GlobalStatus.num_segments );
		GlobalStatus.window		= Flip_int16( GlobalStatus.window );
		GlobalStatus.personal_window	= Flip_int16( GlobalStatus.personal_window );
		GlobalStatus.num_sessions	= Flip_int16( GlobalStatus.num_sessions );
		GlobalStatus.num_groups	        = Flip_int16( GlobalStatus.num_groups );
		GlobalStatus.major_version		= Flip_int16( GlobalStatus.major_version );
		GlobalStatus.minor_version		= Flip_int16( GlobalStatus.minor_version );
		GlobalStatus.patch_version		= Flip_int16( GlobalStatus.patch_version );
	}
	printf("\n============================\n");
	ret1 = Conf_proc_by_id( GlobalStatus.my_id, &p );
	ret2 = Conf_proc_by_id( GlobalStatus.leader_id, &leader_p );
	if( ret1 < 0 )
	{
		printf("Report_message: Skiping illegal status \n");
		printf("==================================\n");
		return;
	}
	printf("Status at %s V%2d.%02d.%2d (state %d, gstate %d) after %d seconds :\n",p.name, 
               GlobalStatus.major_version,GlobalStatus.minor_version,GlobalStatus.patch_version, 
               GlobalStatus.state, GlobalStatus.gstate, GlobalStatus.sec);
	if( ret2 < 0 )
	     printf("Membership  :  %d  procs in %d segments, leader is %d\n",
			GlobalStatus.num_procs,GlobalStatus.num_segments,GlobalStatus.leader_id);
	else printf("Membership  :  %d  procs in %d segments, leader is %s\n",
		GlobalStatus.num_procs,GlobalStatus.num_segments,leader_p.name);

	printf("rounds   : %7d\ttok_hurry : %7d\tmemb change: %7d\n",GlobalStatus.token_rounds,GlobalStatus.token_hurry,GlobalStatus.membership_changes);
	printf("sent pack: %7d\trecv pack : %7d\tretrans    : %7d\n",GlobalStatus.packet_sent,GlobalStatus.packet_recv,GlobalStatus.retrans);
	printf("u retrans: %7d\ts retrans : %7d\tb retrans  : %7d\n",GlobalStatus.u_retrans,GlobalStatus.s_retrans,GlobalStatus.b_retrans);
	printf("My_aru   : %7d\tAru       : %7d\tHighest seq: %7d\n",GlobalStatus.my_aru,GlobalStatus.aru, GlobalStatus.highest_seq);
	printf("Sessions : %7d\tGroups    : %7d\tWindow     : %7d\n",GlobalStatus.num_sessions,GlobalStatus.num_groups,GlobalStatus.window);
	printf("Deliver M: %7d\tDeliver Pk: %7d\tPers Window: %7d\n",GlobalStatus.message_delivered,GlobalStatus.packet_delivered,GlobalStatus.personal_window);
	printf("Delta Mes: %7d\tDelta Pack: %7d\tDelta sec  : %7d\n",
			GlobalStatus.message_delivered - last_mes, GlobalStatus.aru - last_aru, GlobalStatus.sec - last_sec );
	printf("==================================\n");

	printf("\n");
	printf("Monitor> ");
	fflush(stdout);

}

static	void	Usage(int argc, char *argv[])
{
	My_name = 0; /* NULL */
	My_port = 6543; 

	Send_status_timeout.sec = 10;
	Send_status_timeout.usec= 0;

	strcpy( Config_file, "spread.conf" );

	while( --argc > 0 )
	{
		argv++;

		if( !strncmp( *argv, "-p", 2 ) )
		{
			sscanf(argv[1], "%d", &My_port );

			argc--; argv++;

                }else if( !strncmp( *argv, "-n", 2 ) ) {
			if( strlen( argv[1] ) > MAX_PROC_NAME-1 ) /* -1 for the null */
				Alarm( EXIT, "Usage: proc name %s too long\n",
					argv[1] );

			memcpy( My_name_buf, argv[1], strlen( argv[1] ) );
			My_name = My_name_buf;

			argc--; argv++;

		}else if( !strncmp( *argv, "-t", 2 ) ){
			sscanf( argv[1], "%ld", &Send_status_timeout.sec );

			argc--; argv++;

		}else if( !strncmp( *argv, "-c", 2 ) ){
			strcpy( Config_file, argv[1] );

			argc--; argv++;
		}else{
			Alarm( EXIT, "Usage: spmonitor\n%s\n%s\n%s\n%s\n",
				"\t[-p <port number>]: specify port number",
				"\t[-n <proc name>]  : force computer name",
				"\t[-t <status timeout>]: specify number of seconds between status queries",
				"\t[-c <file name>]  : specify configuration file" );
		}
	}
}
