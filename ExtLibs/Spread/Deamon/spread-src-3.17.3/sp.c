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


#include "arch.h"
#include <string.h>
#include <stdio.h>
#include "mutex.h"

#ifndef	ARCH_PC_WIN95

#include <errno.h>
#include <sys/time.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h> 
#include <netinet/tcp.h> 
#include <sys/un.h>
#include <netdb.h>
#include <signal.h>
#include <sys/ioctl.h>
#include <unistd.h>

#else	/* ARCH_PC_WIN95 */

#include <winsock.h>
#define ioctl   ioctlsocket

	WSADATA		WSAData;

#endif	/* ARCH_PC_WIN95 */

#include "sp_events.h"
#include "spread_params.h"
#include "sess_types.h"
#include "scatter.h"
#include "alarm.h"
#include "sp_func.h"
#include "acm.h"

typedef	struct	dummy_sp_session {
	mailbox	mbox;
	char	private_group_name[MAX_GROUP_NAME];
        message_header  recv_saved_head;
        int     recv_message_saved;
} sp_session;

struct auth_method_info {
        char    name[MAX_AUTH_NAME];
        int     (*authenticate) (int, void *);
        void    *auth_data;
};

static  int     sp_null_authenticate(int, void *);
static  struct auth_method_info Auth_Methods[MAX_AUTH_METHODS] = { {"NULL", sp_null_authenticate, NULL} };
static  int     Num_Reg_Auth_Methods = 1;

#define	MAX_MUTEX	256
#define	MAX_MUTEX_MASK	0x000000ff

#ifdef _REENTRANT
#ifndef	ARCH_PC_WIN95
static	mutex_type	Init_mutex = MUTEX_STATIC_INIT;
#else	/* ARCH_PC_WIN95 */
static	mutex_type	Init_mutex = {MUTEX_STATIC_INIT};
#endif	/* ARCH_PC_WIN95 */

static	mutex_type	Struct_mutex;
static	mutex_type	Mbox_mutex[MAX_MUTEX][2];

#endif /* def _REENTRANT */

static	int		Num_sessions = 0;
static	sp_session	Sessions[MAX_SESSIONS];

static  sp_time         Zero_timeout = { 0, 0 };

static	void    Flip_mess( message_header *head_ptr );
static  void	SP_kill( mailbox mbox );
static	int	SP_get_session( mailbox mbox );
static	int	SP_internal_multicast( mailbox mbox, service service_type, 
				       int num_groups,
				       const char groups[][MAX_GROUP_NAME],
				       int16 mess_type,
				       const scatter *scat_mess );

/* This is a null authenticate method that does nothing */

static  int     sp_null_authenticate(int fd, void * auth_data)
{
        /* return success */
        return(1);
}
static  int     valid_auth_method(char *auth_method, char *auth_list, int auth_list_len)
{
        char *cur_p, *next_p;
        char list_str[MAX_AUTH_NAME * MAX_AUTH_METHODS];
        memcpy(list_str, auth_list, auth_list_len);

        list_str[auth_list_len] = '\0';
        cur_p = list_str;
        do {
                next_p = strchr(cur_p, ' ');
                if (next_p != NULL)
                        *next_p = '\0';
                if (!strcmp(auth_method, cur_p) )
                        return(1);
                if (next_p != NULL)
                        cur_p = next_p + 1;
                else
                        cur_p = NULL;
        } while (NULL != cur_p );
        /* didn't find the method in the list */
        return(0);
}
static  void    sp_initialize_locks(void)
{
        int ret, i;

	ret = Mutex_trylock( &Init_mutex );
	if( ret == 0 )
	{
		/* 
		 * we managed to lock the Init_mutex. This means we are the first thread
		 * to get here.
		 */

		Mutex_init( &Struct_mutex );
		for( i=0; i < MAX_MUTEX; i++ )
		{
			Mutex_init( &Mbox_mutex[i][0] );
			Mutex_init( &Mbox_mutex[i][1] );
		}
#ifdef ARCH_PC_WIN95

		ret = WSAStartup( MAKEWORD(2,0), &WSAData );
		if( ret != 0 )
			Alarm( EXIT, "sp_initialize_locks: winsock initialization error %d\n", ret );
#endif	/* ARCH_PC_WIN95 */
	}
        return;
}
/* This calls recv() with the additional features of ignoring syscall interruptions
 * caused by signals delivered to this application, and of returning in at most *time_out time.
 * When it returns the *time_out variable will be modified to have contain the value:
 * old *time_out - time spent in this function.
 *
 * If *time_out == {0,0} then the call is made blocking and will NOT timeout.
 */
static  int     recv_nointr_timeout(int s, void *buf, size_t len, int flags, sp_time *time_out)
{
        int ret, num_ready;
        fd_set rset,fixed_rset;
        sp_time start_time, temp_time, target_time, wait_time;

        if ( len == 0 )
                return(0);
        if ( E_compare_time(Zero_timeout, *time_out) < 0 )
        {
                start_time = E_get_time();
                target_time = E_add_time(start_time, *time_out);
                wait_time = *time_out;
                FD_ZERO(&fixed_rset);
                FD_SET(s, &fixed_rset);
                rset = fixed_rset;
                while( ((num_ready = select(s+1, &rset, NULL, NULL, (struct timeval *)&wait_time)) == -1) && ((sock_errno == EINTR) || (sock_errno == EAGAIN) || (sock_errno == EWOULDBLOCK)) )
                {
                        temp_time = E_get_time();
                        if (E_compare_time(temp_time, target_time) < 0 ) {
                                wait_time = E_sub_time(target_time, temp_time);
                        } else {
                                printf("recv_nointr_timeout: Timed out when interrupted\n");
                                sock_set_errno( ERR_TIMEDOUT );
                                return(-1);
                        }
                        rset = fixed_rset;
                }
                if ( !num_ready ) {
                        printf("recv_nointr_timeout: Timed out\n");
                        sock_set_errno( ERR_TIMEDOUT );
                        return(-1);
                } 
        }
        while( ((ret = recv( s, buf, len, flags)) == -1) && ((sock_errno == EINTR) || (sock_errno == EAGAIN) || (sock_errno == EWOULDBLOCK)) )
                ;
        if ( E_compare_time(Zero_timeout, *time_out) < 0 )
        {
                temp_time = E_sub_time(E_get_time(), start_time);
                *time_out = E_sub_time(*time_out, temp_time);
        }
        return(ret);
}

/* This calls connect() with the additional features of ignoring syscall interruptions
 * caused by signals delivered to this application, and of returning in at most *time_out time.
 * When it returns the *time_out variable will be modified to have contain the value:
 * old *time_out - time spent in this function.
 *
 * If *time_out == {0,0} then the call is made blocking and will NOT timeout.
 */
static  int     connect_nointr_timeout(int s, struct sockaddr *sname, socklen_t slen, sp_time *time_out)
{
    int         ret, num_ready;
    fd_set      rset,fixed_rset,wset;
    sp_time     start_time, temp_time, target_time, wait_time;
    int         non_blocking = 0;
    int         err;
    int         on;
    int         ret_ioctl;
    sockopt_len_t   elen;

    if ( E_compare_time(Zero_timeout, *time_out) < 0 )
    {
        non_blocking = 1;
        start_time = E_get_time();
        target_time = E_add_time(start_time, *time_out);
        wait_time = *time_out;
        /* set file descriptor to non-blocking */
        on = 1;
        ret_ioctl = ioctl( s, FIONBIO, &on);
    }
    /* Handle EINTR while connecting by waiting with select until the 
     * connect completes or fails.  This is a while loop but it is never 
     * done more then once. The while is so we can use 'break' 
     */
    while( ((ret = connect( s, sname, slen ) ) == -1) 
           && ((sock_errno == EINTR) || (sock_errno == EAGAIN) || (sock_errno == EWOULDBLOCK) || (sock_errno == EINPROGRESS)) )
    {
        FD_ZERO(&fixed_rset);
        FD_SET(s, &fixed_rset);
        rset = fixed_rset;
        wset = rset;
        Alarmp( SPLOG_DEBUG, SESSION, "connect_nointr_timeout: connect in progress for socket %d, now wait in select\n", s);
        /* wait for select to timeout (num_ready == 0), give a permanent error (num_ready < 0 && sock_errno != transient). If transient error, retry after checking to make sure timeout has not expired */
        while( ((num_ready = select(s+1, &rset, &wset, NULL, (struct timeval *)&wait_time)) == -1) && ((sock_errno == EINTR) || (sock_errno == EAGAIN) || (sock_errno == EWOULDBLOCK)) )
        {
            temp_time = E_get_time();
            if (E_compare_time(temp_time, target_time) < 0 ) {
                wait_time = E_sub_time(target_time, temp_time);
            } else {
                Alarmp( SPLOG_WARNING, SESSION, "connect_nointr_timeout: connect interrupted and select wait timesout during transient error: %s\n", sock_strerror(sock_errno));
                close(s);
                sock_set_errno( ERR_TIMEDOUT );
                ret = -1;
                goto done_connect_try;
            }
            rset = fixed_rset;
            wset = rset;
        }
        if ( num_ready == 0 ) {
            /* timeout */
            close(s);
            sock_set_errno( ERR_TIMEDOUT );
            ret = -1;
            break;
        } else if ( num_ready < 0 ) 
        {
            Alarmp( SPLOG_WARNING, SESSION, "connect_nointr_timeout: connect interrupted and error in select wait: %s\n", sock_strerror(sock_errno));
            ret = -1;
            break;
        } 
        if (FD_ISSET(s, &rset) || FD_ISSET( s, &wset))
        {
            err = 0;
            elen = sizeof(err);
            if (getsockopt(s, SOL_SOCKET, SO_ERROR, (void *)&err, &elen) < 0)
            {
                ret = -1;
                break;
            }
            if (err)
            {
                sock_set_errno( err );
                ret = -1;
            } else {
                ret = 0;
            }
            break;
        } else {
            Alarmp( SPLOG_FATAL, SESSION, "connect_nointr_timeout: connect interrupted--but select does not indicate either error or connecting socket ready. Impossible condition (i.e. bug).  ret= %d: %s\n", err, sock_strerror(sock_errno));
            ret = -1;
            break;
        }
    } /* while error case for connect */
    Alarmp( SPLOG_DEBUG, SESSION, "connect_nointr_timeout: After connect, ret = %d error is:%s\n", ret, sock_strerror(sock_errno));

done_connect_try:
    if ( non_blocking )
    {
        /* set file descriptor to blocking */
        on = 0;
        ret_ioctl = ioctl( s, FIONBIO, &on);
        temp_time = E_sub_time(E_get_time(), start_time);
        *time_out = E_sub_time(*time_out, temp_time);
    }
    return(ret);
}

/* Increase socket buffer size to 200Kb if possible.
 * Used in SP_connect family when connection is established.
 */
static void set_large_socket_buffers(int s)
{
    int i, on, ret;
    sockopt_len_t onlen;

    for( i=10; i <= 200; i+=5 )
    {
        on = 1024*i;

        ret = setsockopt( s, SOL_SOCKET, SO_SNDBUF, (void *)&on, 4);
        if (ret < 0 ) break;
        
        ret = setsockopt( s, SOL_SOCKET, SO_RCVBUF, (void *)&on, 4);
        if (ret < 0 ) break;
	
        onlen = sizeof(on);
        ret= getsockopt( s, SOL_SOCKET, SO_SNDBUF, (void *)&on, &onlen );
        if( on < i*1024 ) break;
        Alarmp( SPLOG_INFO, SESSION, "set_large_socket_buffers: set sndbuf %d, ret is %d\n", on, ret );
        
        onlen = sizeof(on);
        ret= getsockopt( s, SOL_SOCKET, SO_RCVBUF, (void *)&on, &onlen );
        if( on < i*1024 ) break;
        Alarmp( SPLOG_INFO, SESSION, "set_large_socket_buffers: set rcvbuf %d, ret is %d\n", on, ret );
    }
    Alarmp( SPLOG_INFO, SESSION, "set_large_socket_buffers: set sndbuf/rcvbuf to %d\n", 1024*(i-5) );
}

/* API break 3.15.0. version is no longer a float. return 0 on error, 1 if set version number */
int	SP_version(int *major_version, int *minor_version, int *patch_version)
{
        if ( (major_version == NULL ) || 
             (minor_version == NULL ) ||
             (patch_version == NULL ) )
                return( 0 );

        *major_version = SP_MAJOR_VERSION;
        *minor_version = SP_MINOR_VERSION;
        *patch_version = SP_PATCH_VERSION;
        return( 1 );
}
/* Addition to API 3.16.0
 * Returns 0 on error, 1 if successful 
 * Registers a single authentication handler.
 */
int     SP_set_auth_method( const char *auth_name, int (*auth_function) (int, void *), void * auth_data)
{
        sp_initialize_locks();

        if (strlen(auth_name) >= MAX_AUTH_NAME)
        {
            Alarm( SESSION, "SP_set_auth_method: Name of auth method too long\n");
            return(0);
        }
        if ( NULL == auth_function )
        {
            Alarm( SESSION, "SP_set_auth_method: auth method is NULL\n");
            return(0);
        }
        Mutex_lock( &Struct_mutex );

        strncpy(Auth_Methods[0].name, auth_name, MAX_AUTH_NAME);
        Auth_Methods[0].authenticate = auth_function;
        Auth_Methods[0].auth_data = auth_data;
        Num_Reg_Auth_Methods = 1;

        Mutex_unlock( &Struct_mutex );
        return(1);
}
/* Addition to API 3.16.0
 * Returns 0 on error, 1 if successful
 * Registers the set of authentication handlers.
 */
int     SP_set_auth_methods( int num_methods, const char *auth_name[], int (*auth_function[]) (int, void *), void * auth_data[])
{
        int i;
        sp_initialize_locks();

        if (num_methods < 0 || num_methods > MAX_AUTH_METHODS)
        {
            Alarm( SESSION, "SP_set_auth_methods: Too many methods trying to be registered\n");
            return(0);
        }

        /* check validity of handlers */
        for (i=0; i< num_methods; i++)
        {
            if (strlen(auth_name[i]) >= MAX_AUTH_NAME)
            {
                Alarm( SESSION, "SP_set_auth_method: Name of auth method too long\n");
                return(0);
            }
            if ( NULL == auth_function[i] )
            {
                Alarm( SESSION, "SP_set_auth_method: auth method is NULL\n");
                return(0);
            }                
        }

        /* insert set of handlers as atomic action */
        Mutex_lock( &Struct_mutex );
        for (i=0; i< num_methods; i++)
        {

            strncpy(Auth_Methods[i].name, auth_name[i], MAX_AUTH_NAME);
            Auth_Methods[i].authenticate = auth_function[i];
            Auth_Methods[i].auth_data = auth_data[i];
        }
        Num_Reg_Auth_Methods = num_methods;
        Mutex_unlock( &Struct_mutex );
        return(1);
}

int	SP_connect( const char *spread_name, const char *private_name,
		    int priority, int group_membership, mailbox *mbox,
		    char *private_group )
{
        int ret;

        ret = SP_connect_timeout( spread_name, private_name, priority, group_membership, mbox, private_group, Zero_timeout);
        return(ret);
}

int	SP_connect_timeout( const char *spread_name, const char *private_name,
		    int priority, int group_membership, mailbox *mbox,
		    char *private_group, sp_time time_out )
{
	/* struct	hostent		*host_ptr, *gethostbyname(); */
	struct	hostent		*host_ptr;
	int16			port;
	int32			host_address;
	int32			lport, i1, i2, i3, i4;
	char			*c_ptr;
	char			host_name[80];
	char			s_name[80];
	char			dummy_s[80];
	char			conn[MAX_PRIVATE_NAME+5];
        signed char                    auth_list_len;
        char                    auth_list[MAX_AUTH_NAME * MAX_AUTH_METHODS];
        char                    auth_choice[MAX_AUTH_NAME * MAX_AUTH_METHODS];
        bool                    failed;
        int                     num_auth_methods;
        struct auth_method_info auth_methods[MAX_AUTH_METHODS];
	int			p;
	int			s;
	int			ret, i;
        unsigned int            len;
	int			sp_v1, sp_v2, sp_v3;
	char			l;
	int32			on;

	struct	sockaddr_in	inet_addr;

#ifndef	ARCH_PC_WIN95

	struct	sockaddr_un	unix_addr;

#endif	/* ARCH_PC_WIN95 */

#ifndef	ARCH_PC_WIN95

	signal( SIGPIPE, SIG_IGN );

#endif	/* ARCH_PC_WIN95 */

#ifdef ENABLEDEBUG
        Alarm_set_types(SESSION | DEBUG);
        Alarm_set_priority(SPLOG_DEBUG);
#endif

        sp_initialize_locks();

	/* 
	 * There are 3 possibilities for a name:
	 * 	3333
	 *	3333@commedia.cs.jhu.edu or 3333@fault
	 *	3333@128.220.221.1
	 */

	if( spread_name == 0 || (!strcmp( spread_name, "" ) ) ) 
#ifndef ARCH_PC_WIN95
		strcpy( s_name, "4803" );
#else
		strcpy( s_name, "4803@localhost" );
#endif	/* ARCH_PC_WIN95 */

	else strcpy( s_name, spread_name );
	c_ptr = strchr( s_name, ' ');
	if( c_ptr != 0 ) return ( ILLEGAL_SPREAD );
	c_ptr = strchr( s_name, '@');

	if( c_ptr == 0 )
	{

#ifndef	ARCH_PC_WIN95
		p = AF_UNIX;
		ret = sscanf( s_name, "%d%s", &lport, dummy_s );
		if( ret != 1 ) return( ILLEGAL_SPREAD );
#else	/* ARCH_PC_WIN95 */
		return( ILLEGAL_SPREAD );
#endif	/* ARCH_PC_WIN95 */

	}else{
		p = AF_INET;
		*c_ptr = ' ';
		ret = sscanf( s_name,"%d%s", &lport, host_name );
		if( ret != 2) return( ILLEGAL_SPREAD );
		host_ptr = gethostbyname( host_name );
		if( host_ptr != 0 )
		{
			/* option 3333@commedia.cs.jhu.edu */
			memcpy( &host_address, host_ptr->h_addr, sizeof(int32) );
		}else{
			/* option 3333@128.220.221.1 */
			for(i=0; i< 3; i++)
			{
				c_ptr = strchr(host_name, '.' );
				if ( c_ptr == 0) return( ILLEGAL_SPREAD );
				*c_ptr = ' ';
			}
			ret = sscanf( host_name, "%d%d%d%d%s", &i1, &i2, &i3, &i4, dummy_s );
			if( ret != 4 ) return( ILLEGAL_SPREAD );
			host_address = ( (i1 << 24 ) | (i2 << 16 ) | (i3 << 8) | i4 );
		}
	}
	if( lport < 0 || lport >= 32*1024 ) return( ILLEGAL_SPREAD );
	port = lport;
	if( p == AF_INET )
	{
		s = socket( AF_INET, SOCK_STREAM, 0 );
		if( s < 0 )
		{
			Alarm( DEBUG, "SP_connect: unable to create mailbox %d\n", s );
			return( COULD_NOT_CONNECT );
		}

                set_large_socket_buffers(s);

                on = 1;
                ret = setsockopt( s, IPPROTO_TCP, TCP_NODELAY, (void *)&on, 4);
                if (ret < 0) 
                        Alarm(PRINT, "Setting TCP_NODELAY failed with  error: %s\n", sock_strerror(sock_errno));
                else 
                        Alarm( SESSION, "SP_connect: set TCP_NODELAY for socket %d\n", s);

		inet_addr.sin_family = AF_INET;
		inet_addr.sin_port   = htons( port );
	        memcpy( &inet_addr.sin_addr, &host_address, sizeof(int32) );
		ret = connect_nointr_timeout( s, (struct sockaddr *)&inet_addr, sizeof(inet_addr), &time_out);
	}else{

#ifndef	ARCH_PC_WIN95
		s = socket( AF_UNIX, SOCK_STREAM, 0 );
		if( s < 0 )
		{
			Alarm( DEBUG, "SP_connect: unable to create mailbox %d\n", s );
			return( COULD_NOT_CONNECT );
		}

                set_large_socket_buffers(s);

		unix_addr.sun_family = AF_UNIX;
		sprintf( unix_addr.sun_path, "/tmp/%d", port );
		ret = connect_nointr_timeout( s, (struct sockaddr *)&unix_addr, sizeof(unix_addr), &time_out);
#endif	/* !ARCH_PC_WIN95 */
	}

	if( ret < 0 )
	{
		Alarm( SESSION, "SP_connect: unable to connect mailbox %d: %s\n", s, sock_strerror(sock_errno));
		close( s );
		return( COULD_NOT_CONNECT );
	}
	/* 
	 * connect message looks like:
	 *
	 * byte - version of lib
	 * byte - subversion of lib
         * byte - patch version of lib
	 * byte - lower half byte 1/0 with or without groups, upper half byte: priority (0/1).
	 * byte - len of name
	 * len bytes - name
	 *
	 */
	conn[0] = SP_MAJOR_VERSION;
	conn[1] = SP_MINOR_VERSION;
	conn[2] = SP_PATCH_VERSION;

	if( group_membership ) conn[3] = 1; 
	else conn[3] = 0;
	if(priority < 0) priority = 0;
	if(priority > 1) priority = 1;
	conn[3] = conn[3]+16*priority;

        if (private_name == NULL)
        {
                len = 0;
        } else {
                len = strlen(private_name);
                if( len > MAX_PRIVATE_NAME ) len = MAX_PRIVATE_NAME;
                memcpy( &conn[5], private_name, len );
        }
	conn[4] = len;
	while(((ret = send( s, conn, len+5, 0 )) == -1) && ((sock_errno == EINTR) || (sock_errno == EAGAIN) || (sock_errno == EWOULDBLOCK)) )
                ;
	if( ret != len+5 )
	{
		Alarm( SESSION, "SP_connect: unable to send name %d %d: %s\n", ret, len+5, sock_strerror(sock_errno));
		close( s );
		return( CONNECTION_CLOSED );
	}
        /* Insert Access control and authentication checks here */
        auth_list_len = 0;
        ret = recv_nointr_timeout( s, &auth_list_len, 1, 0, &time_out); 
        if ( ret <= 0 )
        {
		Alarm( SESSION, "SP_connect: unable to read auth_list_len %d: %s\n", ret, sock_strerror(sock_errno));
		close( s );
		return( CONNECTION_CLOSED );
        }
        if ( auth_list_len > (MAX_AUTH_NAME * MAX_AUTH_METHODS) )
        {
		Alarm( SESSION, "SP_connect: illegal value in auth_list_len %d: %s\n", auth_list_len, sock_strerror(sock_errno));
		close( s );
		return( CONNECTION_CLOSED );
        } 
        if ( auth_list_len < 0 ) 
        {
            Alarm( SESSION, "SP_connect: connection invalid with code %d while reading auth_list_len\n", auth_list_len);
            close( s );
            return( auth_list_len );
        }
        if ( auth_list_len != 0 )
        {
                ret = recv_nointr_timeout( s, auth_list, auth_list_len, 0, &time_out);
                if ( ret <= 0 )
                {
                        Alarm( SESSION, "SP_connect: unable to read auth_list %d: %s\n", ret, sock_strerror(sock_errno));
                        close( s );
                        return( CONNECTION_CLOSED );
                }

                Alarm( SESSION, "SP_connect: DEBUG: Auth list is: %s\n", auth_list);
        } else {
                Alarm( SESSION, "SP_connect: DEBUG: Auth list is empty\n");
        }

        /* Here is where we check the list of available methods of authentication and pick one. 
         * For right now we just ignore the list and use the method the app set in SP_set_auth_method.
         * If no method was set we use the NULL method.
         * The global Auth_Methods struct needs to be protected by the Struct_mutex.
         */
        memset(auth_choice, 0, MAX_AUTH_NAME * MAX_AUTH_METHODS);
        Mutex_lock( &Struct_mutex );
        for (i=0; i< Num_Reg_Auth_Methods; i++)
        {
                auth_methods[i] = Auth_Methods[i];
                memcpy(&auth_choice[i * MAX_AUTH_NAME], Auth_Methods[i].name, MAX_AUTH_NAME);
        }
        num_auth_methods = Num_Reg_Auth_Methods;
        Mutex_unlock( &Struct_mutex );

        for (i=0; i < num_auth_methods; i++)
        {
                if ( !valid_auth_method(auth_methods[i].name, auth_list, auth_list_len) )
                {
                        Alarm( SESSION, "SP_connect: chosen authentication method is not permitted by daemon\n");
                        close( s );
                        return( REJECT_AUTH );
                }
        }
	while(((ret = send( s, auth_choice, MAX_AUTH_NAME * MAX_AUTH_METHODS, 0 )) == -1) && ((sock_errno == EINTR) || (sock_errno == EAGAIN) || (sock_errno == EWOULDBLOCK)) )
                ;
	if( ret != (MAX_AUTH_NAME * MAX_AUTH_METHODS) )
	{
		Alarm( SESSION, "SP_connect: unable to send auth_name %d %d: %s\n", ret, MAX_AUTH_NAME * MAX_AUTH_METHODS, sock_strerror(sock_errno));
		close( s );
		return( CONNECTION_CLOSED );
	}

        /* Here is where the authentication work will happen. 
         * This will be specific to the method chosen, and should be done in an
         * 'authenticate' function that is called and only returns when the process
         * is finished with either an authenticated connection or a failure. 
         * If failure (return 0) the SP_connect returns with an error REJECT_AUTH.
         * If authenticated (return 1 ), SP_connect continues with the rest of the connect protocol.
         */
        failed = FALSE;
        for (i = 0; i< num_auth_methods; i++)
        {
                ret = auth_methods[i].authenticate(s, auth_methods[i].auth_data);
                if (!ret)
                {
                        Alarm( SESSION, "SP_connect: authentication of connection failed in method %s\n", auth_methods[i].name);
                        failed = TRUE;
                }
        }
        if ( failed )
        {
                close ( s );
                return( REJECT_AUTH );
        }

	l=0;
        ret = recv_nointr_timeout( s, &l, 1, 0, &time_out);
	if( ret <= 0 )
	{
		Alarm( SESSION, "SP_connect: unable to read answer %d: %s\n", ret, sock_strerror(sock_errno));
		close( s );
		return( CONNECTION_CLOSED );
	}
	if( l != ACCEPT_SESSION )
	{
		Alarm( SESSION, "SP_connect: session rejected %d\n", l);
		close( s );
		return( l );
	}
        ret = recv_nointr_timeout( s, &l, 1, 0, &time_out);
	if( ret <= 0 )
	{
		Alarm( SESSION, "SP_connect: unable to read version %d: %s\n", ret, sock_strerror(sock_errno));
		close( s );
		return( CONNECTION_CLOSED );
	}
	sp_v1 = l;

        ret = recv_nointr_timeout( s, &l, 1, 0, &time_out);
	if( ret <= 0 )
	{
		Alarm( SESSION, "SP_connect: unable to read subversion %d: %s\n", ret, sock_strerror(sock_errno));
		close( s );
		return( CONNECTION_CLOSED );
	}
	sp_v2 = l;

        ret = recv_nointr_timeout( s, &l, 1, 0, &time_out);
	if( ret <= 0 )
	{
		Alarm( SESSION, "SP_connect: unable to read patch version %d: %s\n", ret, sock_strerror(sock_errno));
		close( s );
		return( CONNECTION_CLOSED );
	}
	sp_v3 = l;

	/* checking spread version. Should be at least 3.01 */
	if( sp_v1*10000 + sp_v2*100 + sp_v3 < 30100 )
	{
		Alarm( PRINT  , "SP_connect: old spread version %d.%d.%d not suppoted\n", 
			sp_v1, sp_v2, sp_v3 );
		close( s );
		return( REJECT_VERSION );
	}
	if( (sp_v1*10000 + sp_v2*100 + sp_v3 < 30800) && priority > 0 )
	{
		Alarm( PRINT, "SP_connect: old spread version %d.%d.%d does not support priority other than 0\n", 
			sp_v1, sp_v2, sp_v3 );
		close( s );
		return( REJECT_VERSION );
	}

        ret = recv_nointr_timeout( s, &l, 1, 0, &time_out);
	if( ret <= 0 )
	{
		Alarm( SESSION, "SP_connect: unable to read size of group %d: %s\n", ret, sock_strerror(sock_errno));
                close( s );
		return( CONNECTION_CLOSED );
	}
	len = l;
        ret = recv_nointr_timeout( s, private_group, len, 0, &time_out);
	if( ret <= 0 )
	{
		Alarm( SESSION, "SP_connect: unable to read private group %d:  %s\n", ret, sock_strerror(sock_errno));
		close( s );
		return( CONNECTION_CLOSED );
	}
	private_group[len]=0;
	Alarm( DEBUG, "SP_connect: connected with private group(%d bytes): %s\n", 
		ret, private_group );
	*mbox = s;

	Mutex_lock( &Struct_mutex );

	Sessions[Num_sessions].mbox = s;
	strcpy( Sessions[Num_sessions].private_group_name, private_group );
        Sessions[Num_sessions].recv_message_saved = 0;
	Num_sessions++;

	Mutex_unlock( &Struct_mutex );

	return( ACCEPT_SESSION );
}

int	SP_disconnect( mailbox mbox )
{
	int		ses;
	int		ret;
	char		send_group[MAX_GROUP_NAME];
	scatter		send_scat;

	Mutex_lock( &Struct_mutex );

	ses = SP_get_session( mbox );

	if( ses < 0 )
	{
		Mutex_unlock( &Struct_mutex );
		return( ILLEGAL_SESSION );
	}
	strcpy(send_group, Sessions[ses].private_group_name );

	Mutex_unlock( &Struct_mutex );

	send_scat.num_elements = 0;

	ret = SP_internal_multicast( mbox, KILL_MESS, 1, (const char (*)[MAX_GROUP_NAME])send_group, 0, &send_scat );

	SP_kill( mbox );

	ret = 0;

	return( ret );
}

int	SP_join( mailbox mbox, const char *group )
{
	int		ret;
	char		send_group[MAX_GROUP_NAME];
	scatter		send_scat;
	unsigned int	len;
	int		i;

	len = strlen( group );
	if ( len == 0 ) return( ILLEGAL_GROUP );
        if ( len >= MAX_GROUP_NAME ) return( ILLEGAL_GROUP );
	for( i=0; i < len; i++ )
		if( group[i] < 36 || group[i] > 126 ) return( ILLEGAL_GROUP );

	send_group[MAX_GROUP_NAME-1]=0;
	strncpy(send_group, group, MAX_GROUP_NAME-1);
	send_scat.num_elements = 0;

	ret = SP_internal_multicast( mbox, JOIN_MESS, 1, (const char (*)[MAX_GROUP_NAME])send_group, 0, &send_scat );
	return( ret );
}

int	SP_leave( mailbox mbox, const char *group )
{
	int		ret;
	char		send_group[MAX_GROUP_NAME];
	scatter		send_scat;
	unsigned int	len;
	int		i;

	len = strlen( group );
	if ( len == 0 ) return( ILLEGAL_GROUP );
	if ( len >= MAX_GROUP_NAME ) return( ILLEGAL_GROUP );
	for( i=0; i < len; i++ )
		if( group[i] < 36 || group[i] > 126 ) return( ILLEGAL_GROUP );

	send_group[MAX_GROUP_NAME-1]=0;
	strncpy(send_group, group, MAX_GROUP_NAME-1);
	send_scat.num_elements = 0;

	ret = SP_internal_multicast( mbox, LEAVE_MESS, 1, (const char (*)[MAX_GROUP_NAME])send_group, 0, &send_scat );
	return( ret );
}

int	SP_multicast( mailbox mbox, service service_type, 
		      const char *group,
		      int16 mess_type, int mess_len, const char *mess )
{
	int		ret;
	char		send_group[MAX_GROUP_NAME];
	scatter		send_scat;

	send_group[MAX_GROUP_NAME-1]=0;
	strncpy(send_group, group, MAX_GROUP_NAME-1);

	send_scat.num_elements = 1;
	send_scat.elements[0].len = mess_len;
	/* might be good to create a const_scatter type */
	send_scat.elements[0].buf = (char *)mess;

	ret = SP_multigroup_scat_multicast( mbox, service_type, 1, (const char (*)[MAX_GROUP_NAME])send_group, mess_type, &send_scat );
	return( ret );
}

int	SP_scat_multicast( mailbox mbox, service service_type, 
			   const char *group,
			   int16 mess_type, const scatter *scat_mess )
{
	int		ret;
	char		send_group[MAX_GROUP_NAME];

	send_group[MAX_GROUP_NAME-1]=0;
	strncpy(send_group, group, MAX_GROUP_NAME-1);

	ret = SP_multigroup_scat_multicast( mbox, service_type, 1, (const char (*)[MAX_GROUP_NAME])send_group, mess_type, scat_mess );
	return( ret );

}

int	SP_multigroup_multicast( mailbox mbox, service service_type, 
			   	 int num_groups,
				 const char groups[][MAX_GROUP_NAME],
		      		 int16 mess_type, int mess_len,
				 const char *mess )
{
	int		ret;
	scatter		send_scat;

	send_scat.num_elements = 1;
	send_scat.elements[0].len = mess_len;
	send_scat.elements[0].buf = (char *)mess;

	ret = SP_multigroup_scat_multicast( mbox, service_type, num_groups, groups, mess_type, &send_scat );
	return( ret );
}

int	SP_multigroup_scat_multicast( mailbox mbox, service service_type, 
			   	      int num_groups,
				      const char groups[][MAX_GROUP_NAME],
			   	      int16 mess_type,
				      const scatter *scat_mess )
{
	int		ret;

	if( !Is_regular_mess( service_type ) ) return( ILLEGAL_SERVICE );

	ret = SP_internal_multicast( mbox, service_type, num_groups, groups, mess_type, scat_mess );
	return( ret );
}

static	int	SP_internal_multicast( mailbox mbox, service service_type, 
				       int num_groups,
				       const char groups[][MAX_GROUP_NAME],
				       int16 mess_type,
				       const scatter *scat_mess )
{

	char		head_buf[10000]; 
	message_header	*head_ptr;
	char		*group_ptr;
	int		mess_len, len;
	int		ses;
	int		i;
	int		ret;

        /* zero head_buf to avoid information leakage */
        memset( head_buf, 0, sizeof(message_header) + MAX_GROUP_NAME*num_groups );

	Mutex_lock( &Struct_mutex );

	ses = SP_get_session( mbox );
	if( ses < 0 ){
		Mutex_unlock( &Struct_mutex );
		return( ILLEGAL_SESSION );
	}

	head_ptr = (message_header *)head_buf;
	group_ptr = &head_buf[ sizeof(message_header) ];

	/* enter the private_group_name of this mbox */
	strcpy( head_ptr->private_group_name, Sessions[ses].private_group_name );

	Mutex_unlock( &Struct_mutex );

	for( i=0, mess_len=0; i < scat_mess->num_elements; i++ )
	{
		if( scat_mess->elements[i].len < 0 ) return ( ILLEGAL_MESSAGE );
		mess_len += scat_mess->elements[i].len;
	}

        if ( (mess_len + num_groups * MAX_GROUP_NAME) > MAX_MESSAGE_BODY_LEN )
        {
                /* Message contents + groups is too large */
                return( MESSAGE_TOO_LONG );
        }

	head_ptr->type = service_type;
	head_ptr->type = Set_endian( head_ptr->type );

	head_ptr->hint = (mess_type << 8) & 0x00ffff00;
	head_ptr->hint = Set_endian( head_ptr->hint );

	head_ptr->num_groups = num_groups;
	head_ptr->data_len = mess_len;
	memcpy( group_ptr, groups, MAX_GROUP_NAME * num_groups );

	Mutex_lock( &Mbox_mutex[mbox&MAX_MUTEX_MASK][0] );
	while(((ret=send( mbox, head_buf, sizeof(message_header)+MAX_GROUP_NAME*num_groups, 0 )) == -1) 
              && ((sock_errno == EINTR) || (sock_errno == EAGAIN) || (sock_errno == EWOULDBLOCK)) )
                ;
	if( ret <=0 )
	{
		Alarm( SESSION, "SP_internal_multicast: error %d sending header and groups on mailbox %d: %s \n", ret, mbox, sock_strerror(sock_errno));
		Mutex_unlock( &Mbox_mutex[mbox&MAX_MUTEX_MASK][0] );
		SP_kill( mbox );
		return( CONNECTION_CLOSED );
	}
	for( len=0, i=0; i < scat_mess->num_elements; len+=ret, i++ )
	{
		while(((ret=send( mbox, scat_mess->elements[i].buf, scat_mess->elements[i].len, 0 )) == -1)
                      && ((sock_errno == EINTR) || (sock_errno == EAGAIN) || (sock_errno == EWOULDBLOCK)) )
                        ;
		if( ret < 0 )
		{
			Alarm( SESSION, "SP_internal_multicast: error %d sending message data on mailbox %d: %s \n", ret, mbox, sock_strerror(sock_errno));
			Mutex_unlock( &Mbox_mutex[mbox&MAX_MUTEX_MASK][0] );
			SP_kill( mbox );
			return( CONNECTION_CLOSED );
		}
	}
	Mutex_unlock( &Mbox_mutex[mbox&MAX_MUTEX_MASK][0] );
	return( len );
}

int	SP_receive( mailbox mbox, service *service_type, char sender[MAX_GROUP_NAME],
		    int max_groups, int *num_groups, char groups[][MAX_GROUP_NAME],
		    int16 *mess_type, int *endian_mismatch,
		    int max_mess_len, char *mess )
{
	int		ret;
	scatter		recv_scat;

	recv_scat.num_elements = 1;
	recv_scat.elements[0].len = max_mess_len;
	recv_scat.elements[0].buf = mess;

	ret = SP_scat_receive( mbox, service_type, sender, max_groups, num_groups, groups, 
				mess_type, endian_mismatch, &recv_scat );
	return( ret );
}

int	SP_scat_receive( mailbox mbox, service *service_type, char sender[MAX_GROUP_NAME],
			 int max_groups, int *num_groups, char groups[][MAX_GROUP_NAME],
			 int16 *mess_type, int *endian_mismatch,
			 scatter *scat_mess )
{

static	char		dummy_buf[10240];
        int             This_session_message_saved;
        int             drop_semantics;
	message_header	mess_head;
	message_header	*head_ptr;
	char		*buf_ptr;
        int32           temp_mess_type;
	int		len, remain, ret;
	int		max_mess_len;
	int		short_buffer;
	int		short_groups;
	int		to_read;
	int		scat_index, byte_index;
	int		ses;
	char		This_session_private_group[MAX_GROUP_NAME];
	int		i;
        int32           old_type;

        /* I must acquire the lock for this mbox before the Struct_mutex lock because
         * I must be sure ONLY one thread is in recv for this mbox, EVEN for 
         * this initial 'get the session and session state' operation.
         * Otherwise one thread enters this and gets the state and sees no saved message
         * then grabs the mbox mutex and discoveres buffer too short and so regrabs the
         * Struct_mutex and adds the saved header, but during this time another thread
         * has entered recv for the same mbox and already grabbed the struct_mutex and also
         * read that no saved mesage exists and is now waiting for the mbox mutex.
         * When it the first thread returns and releases the mbox mutex, the second thread will
         * grab it and enter--but it will think there is NO saved messaage when in reality
         * there IS one. This will cause MANY PROBLEMS :-)
         *
         * NOTE: locking and unlocking the Struct_mutex multiple times during this is OK
         * BECAUSE struct_Mutex only locks non-blocking operations that are guaranteed to complete
         * quickly and never take additional locks.
         */
	Mutex_lock( &Mbox_mutex[mbox & MAX_MUTEX_MASK][1] );

	Mutex_lock( &Struct_mutex );
	/* verify mbox */
	ses = SP_get_session( mbox );
	if( ses < 0 ){
		Mutex_unlock( &Struct_mutex );
                Mutex_unlock( &Mbox_mutex[mbox & MAX_MUTEX_MASK][1] );
		return( ILLEGAL_SESSION );
	}
	strcpy( This_session_private_group, Sessions[ses].private_group_name );

        if (Sessions[ses].recv_message_saved) {
                memcpy(&mess_head, &(Sessions[ses].recv_saved_head), sizeof(message_header) );
                This_session_message_saved = 1;
        } else {
                This_session_message_saved = 0;
        }

	Mutex_unlock( &Struct_mutex );
        
	head_ptr = (message_header *)&mess_head;
	buf_ptr = (char *)&mess_head;

        drop_semantics = Is_drop_recv(*service_type);

        if (!This_session_message_saved) {
                /* read up to size of message_header */
                for( len=0, remain = sizeof(message_header); remain > 0;  len += ret, remain -= ret )
                {
                        while(((ret = recv( mbox, &buf_ptr[len], remain, 0 )) == -1 )
                              && ((sock_errno == EINTR) || (sock_errno == EAGAIN) || (sock_errno == EWOULDBLOCK)) )
                                ;
                        if( ret <=0 )
                        {
                                Alarm( SESSION, "SP_scat_receive: failed receiving header on session %d (ret: %d len: %d): %s\n", mbox, ret, len, sock_strerror(sock_errno) );
                                Mutex_unlock( &Mbox_mutex[mbox & MAX_MUTEX_MASK][1] );

                                SP_kill( mbox );
                                return( CONNECTION_CLOSED );
                        }
                }

                /* Fliping message header to my form if needed */
                if( !Same_endian( head_ptr->type ) ) 
                {
                        Flip_mess( head_ptr );
                }
        }
        /* Validate user's scatter */
	for( max_mess_len = 0, i=0; i < scat_mess->num_elements; i++ ) {
                if ( scat_mess->elements[i].len < 0 )   {
                        if ( !drop_semantics && !This_session_message_saved) {
                                Mutex_lock( &Struct_mutex );
                                ses = SP_get_session( mbox );
                                if( ses < 0 ){
                                        Mutex_unlock( &Struct_mutex );
                                        Mutex_unlock( &Mbox_mutex[mbox & MAX_MUTEX_MASK][1] );
                                        return( ILLEGAL_SESSION );
                                }
                                memcpy(&(Sessions[ses].recv_saved_head), &mess_head, sizeof(message_header) );
                                Sessions[ses].recv_message_saved = 1;
                                Mutex_unlock( &Struct_mutex );
                        }
                        return( ILLEGAL_MESSAGE );
                }
		max_mess_len += scat_mess->elements[i].len;
        }
        /* Validate num_groups and data_len */
        if (head_ptr->num_groups < 0) {
            /* reject this message since it has an impossible (negative) num_groups
             * This is likely to be caused by a malicious attack or memory corruption
             */
            return( ILLEGAL_MESSAGE );
        }
        if (head_ptr->data_len < 0) {
            /* reject this message since it has an impossible (negative) data_len
             * This is likely to be caused by a malicious attack or memory corruption
             */
            return( ILLEGAL_MESSAGE );
        }

        /* Check if sufficient buffer space for groups and data */
        if (!drop_semantics) {
                if ( (head_ptr->num_groups > max_groups) || (head_ptr->data_len > max_mess_len) ) {
                        if (!This_session_message_saved) {
                                Mutex_lock( &Struct_mutex );
                                ses = SP_get_session( mbox );
                                if( ses < 0 ){
                                        Mutex_unlock( &Struct_mutex );
                                        Mutex_unlock( &Mbox_mutex[mbox & MAX_MUTEX_MASK][1] );
                                        return( ILLEGAL_SESSION );
                                }
                                memcpy(&(Sessions[ses].recv_saved_head), &mess_head, sizeof(message_header) );
                                Sessions[ses].recv_message_saved = 1;
                                Mutex_unlock( &Struct_mutex );
                        }
                        /* When *_TOO_SHORT error will be returned, provide caller with all available information:
                         * service_type
                         * sender
                         * mess_type
                         * 
                         * The num_groups field and endian_mismatch field are used to specify the required
                         * size of the groups array and message body array in order to fit the current message
                         * so, they do NOT have their usual meaning. 
                         * If number of groups in the message is > max_groups then the number of required groups 
                         *   is returned as a negative value in the num_groups field.
                         * If the size of the message is > max_mess_len, then the required size in bytes is 
                         *   returned as a negative value in the endian_mismatch field.
                         */
                        if ( Is_regular_mess( head_ptr->type ) || Is_reject_mess( head_ptr->type ) )
                        {
                                temp_mess_type = head_ptr->hint;
                                if ( !Same_endian( head_ptr->hint ) ) {
                                        temp_mess_type = Flip_int32( temp_mess_type );
                                }
                                temp_mess_type = Clear_endian( temp_mess_type );
                                *mess_type = ( temp_mess_type >> 8 ) & 0x0000ffff;
                        }
                        else 
                                *mess_type = 0;
                        *service_type = Clear_endian( head_ptr->type );
                        if (head_ptr->num_groups > max_groups)
                                *num_groups = -(head_ptr->num_groups);
                        else    
                                *num_groups = 0;
                        if (head_ptr->data_len > max_mess_len)
                                *endian_mismatch = -(head_ptr->data_len);
                        else
                                *endian_mismatch = 0;

                        /* Return sender field to caller */
                        strncpy( sender, head_ptr->private_group_name, MAX_GROUP_NAME );

                        Mutex_unlock( &Mbox_mutex[mbox & MAX_MUTEX_MASK][1] );
                        if (*num_groups)
                                return( GROUPS_TOO_SHORT );
                        else
                                return( BUFFER_TOO_SHORT );
                }
        }
	/* Compute mess_type and endian_mismatch from hint */
	if( Is_regular_mess( head_ptr->type ) || Is_reject_mess( head_ptr->type)  )
	{
		if( !Same_endian( head_ptr->hint ) )
		{
			head_ptr->hint = Flip_int32( head_ptr->hint );
			*endian_mismatch = 1;
		}else{
			*endian_mismatch = 0;
		}
                head_ptr->hint = Clear_endian( head_ptr->hint );
                head_ptr->hint = ( head_ptr->hint >> 8 ) & 0x0000ffff;
                *mess_type = head_ptr->hint;
	}else{
		*mess_type = -1; /* marks the index (0..n-1) of the member in the group */
		*endian_mismatch = 0;
	}

	strncpy( sender, head_ptr->private_group_name, MAX_GROUP_NAME );
        
        /* if a reject message read the extra old_type field first, and merge with head_ptr->type */
        if ( Is_reject_mess( head_ptr->type ) )
        {
                remain = 4;
                buf_ptr = (char *)&old_type;
                for( len=0; remain > 0; len += ret, remain -= ret )
                {
                        while(((ret = recv( mbox, &buf_ptr[len], remain, 0 )) == -1 ) && ((sock_errno == EINTR) || (sock_errno == EAGAIN) || (sock_errno == EWOULDBLOCK)) )
                                ;
                        if( ret <=0 )
                        {
                                Alarm( SESSION, "SP_scat_receive: failed receiving old_type for reject on session %d, ret is %d: %s\n", mbox, ret, sock_strerror(sock_errno));
                                Mutex_unlock( &Mbox_mutex[mbox & MAX_MUTEX_MASK][1] );
                                SP_kill( mbox );
                                return( CONNECTION_CLOSED );
                        }
                }
                /* endian flip it */
                if ( !Same_endian( head_ptr->type ) )
                        old_type = Flip_int32(old_type);
        }

	/* read the destination groups */
	buf_ptr = (char *)groups;

	remain = head_ptr->num_groups * MAX_GROUP_NAME;
	short_groups=0;
	if( head_ptr->num_groups > max_groups )
	{
		/* groups too short */
		remain = max_groups * MAX_GROUP_NAME;
		short_groups = 1;
	}

	for( len=0; remain > 0; len += ret, remain -= ret )
	{
		while(((ret = recv( mbox, &buf_ptr[len], remain, 0 )) == -1 ) && ((sock_errno == EINTR) || (sock_errno == EAGAIN) || (sock_errno == EWOULDBLOCK)) )
                        ;
		if( ret <=0 )
		{
			Alarm( SESSION, "SP_scat_receive: failed receiving groups on session %d, ret is %d: %s\n", mbox, ret, sock_strerror(sock_errno));
			Mutex_unlock( &Mbox_mutex[mbox & MAX_MUTEX_MASK][1] );

			SP_kill( mbox );
			return( CONNECTION_CLOSED );
		}
	}

	if( short_groups )
	{
		for( remain = (head_ptr->num_groups - max_groups) * MAX_GROUP_NAME; 
		     remain > 0; remain -= ret ) 
		{
			to_read = remain;
			if( to_read > sizeof( dummy_buf ) ) to_read = sizeof( dummy_buf );
			while(((ret = recv( mbox, dummy_buf, to_read, 0 )) == -1 ) && ((sock_errno == EINTR) || (sock_errno == EAGAIN) || (sock_errno == EWOULDBLOCK)) )
                                ;
			if( ret <=0 )
			{
				Alarm( SESSION, "SP_scat_receive: failed receiving groups overflow on session %d, ret is %d: %s\n", 
                                       mbox, ret, sock_strerror(sock_errno) );
				Mutex_unlock( &Mbox_mutex[mbox & MAX_MUTEX_MASK][1] );
				SP_kill( mbox );
				return( CONNECTION_CLOSED );
			}
		}
		*num_groups = -head_ptr->num_groups; /* !!!! */
	}else	*num_groups = head_ptr->num_groups;

	/* read the rest of the message */
	remain = head_ptr->data_len;
	short_buffer=0;
	if( head_ptr->data_len > max_mess_len )
	{
		/* buffer too short */
		remain = max_mess_len;
		short_buffer = 1;
	}

	ret = 0;
	/* 
	 * pay attention that if head_ptr->data_len is smaller than max_mess_len we need to
	 * change scat, do recvmsg, and restore scat, and then check ret.
         * ret = recvmsg( mbox, &msg, 0 ); 
         * if( ret <=0 )
         * {
         *      Alarm( SESSION, "SP_scat_receive: failed receiving message on session %d\n", mbox );
	 *
	 *	Mutex_unlock( &Mbox_mutex[mbox & MAX_MUTEX_MASK][1] );
	 *
         *      SP_kill( mbox );
         *      return;
         * }
	 */

	/* calculate scat_index and byte_index based on ret and scat_mess */
	for( byte_index=ret, scat_index=0; scat_index < scat_mess->num_elements; scat_index++ )
	{
		if( scat_mess->elements[scat_index].len > byte_index ) break;
		byte_index -= scat_mess->elements[scat_index].len;
	}

	remain -= ret;
	for( len=ret; remain > 0; len += ret, remain -= ret )
	{
		to_read = scat_mess->elements[scat_index].len - byte_index;
		if( to_read > remain ) to_read = remain;
		while(((ret = recv( mbox, &scat_mess->elements[scat_index].buf[byte_index], to_read, 0 )) == -1 )
                      && ((sock_errno == EINTR) || (sock_errno == EAGAIN) || (sock_errno == EWOULDBLOCK)) )
                        ;
		if( ret <=0 )
		{
			Alarm( SESSION, "SP_scat_receive: failed receiving message on session %d, ret is %d: %s\n", 
                               mbox, ret, sock_strerror(sock_errno) );
			Mutex_unlock( &Mbox_mutex[mbox & MAX_MUTEX_MASK][1] );
			SP_kill( mbox );
			return( CONNECTION_CLOSED );
		}else if( ret == to_read ){
			byte_index = 0;
			scat_index++;
		}else{
			byte_index += ret;
		}
	}

	if( Is_reg_memb_mess( head_ptr->type ) && !short_groups )
	{
		/* calculate my index in group */
		for( i=0; i < head_ptr->num_groups; i++ )
		{
			if( !strcmp( groups[i], This_session_private_group ) )
			{
				*mess_type = i;
				break;
			}
		}
	}

 	if( Is_reg_memb_mess( head_ptr->type ) && !Same_endian( head_ptr->type ) )
	{
		int	 	flip_size;
		group_id	*gid_ptr;
		int32		*num_vs_ptr;
		int		bytes_to_copy, bytes_index;
		char		groups_buf[10240];

		/* 
		 * flip membership message:
		 * group_id and number of member ins vs_set 
		 * so - acctually 4 int32.
		 */
		flip_size = sizeof( group_id ) + sizeof( int32 );
		if( flip_size > max_mess_len ) flip_size = max_mess_len;
		for( bytes_index=0, i=0 ; bytes_index < flip_size ; i++, bytes_index += bytes_to_copy )
		{
			bytes_to_copy = flip_size - bytes_index;
			if( bytes_to_copy > scat_mess->elements[i].len )
				bytes_to_copy = scat_mess->elements[i].len;
			memcpy( &groups_buf[bytes_index], scat_mess->elements[i].buf, bytes_to_copy );
		}
		gid_ptr    = (group_id *)&groups_buf[0];
		num_vs_ptr = (int32 *)&groups_buf[sizeof(group_id)];
		gid_ptr->memb_id.proc_id = Flip_int32( gid_ptr->memb_id.proc_id );
		gid_ptr->memb_id.time    = Flip_int32( gid_ptr->memb_id.time );
		gid_ptr->index           = Flip_int32( gid_ptr->index );
		*num_vs_ptr 	         = Flip_int32( *num_vs_ptr );
		for( bytes_index=0, i=0 ; bytes_index < flip_size ; i++, bytes_index += bytes_to_copy )
		{
			bytes_to_copy = flip_size - bytes_index;
			if( bytes_to_copy > scat_mess->elements[i].len )
				bytes_to_copy = scat_mess->elements[i].len;
			memcpy( scat_mess->elements[i].buf, &groups_buf[bytes_index], bytes_to_copy );
		}

	}
        if ( Is_reject_mess( head_ptr->type ) )
        {
                /* set type to be old type + reject */
                head_ptr->type = old_type | REJECT_MESS;
        }
	*service_type = Clear_endian( head_ptr->type );

	if( short_buffer )
	{
		for( remain = head_ptr->data_len - max_mess_len; remain > 0; remain -= ret ) 
		{
			to_read = remain;
			if( to_read > sizeof( dummy_buf ) ) to_read = sizeof( dummy_buf );
			while(((ret = recv( mbox, dummy_buf, to_read, 0 )) == -1 ) && ((sock_errno == EINTR) || (sock_errno == EAGAIN) || (sock_errno == EWOULDBLOCK)) )
                                ;
			if( ret <=0 )
			{
				Alarm( SESSION, "SP_scat_receive: failed receiving overflow on session %d, ret is %d: %s\n", 
                                       mbox, ret, sock_strerror(sock_errno) );
				Mutex_unlock( &Mbox_mutex[mbox & MAX_MUTEX_MASK][1] );
				SP_kill( mbox );
				return( CONNECTION_CLOSED );
			}
		}
		Mutex_unlock( &Mbox_mutex[mbox & MAX_MUTEX_MASK][1] );
		return( BUFFER_TOO_SHORT );
	}
        /* Successful receive so clear saved_message info if any */
        if (This_session_message_saved) {
                Mutex_lock( &Struct_mutex );
                ses = SP_get_session( mbox );
                if( ses < 0 ){
                        Mutex_unlock( &Struct_mutex );
                        Mutex_unlock( &Mbox_mutex[mbox & MAX_MUTEX_MASK][1] );
                        return( ILLEGAL_SESSION );
                }
                memset(&(Sessions[ses].recv_saved_head), 0, sizeof(message_header) );
                Sessions[ses].recv_message_saved = 0;
                Mutex_unlock( &Struct_mutex );
        }

	Mutex_unlock( &Mbox_mutex[mbox & MAX_MUTEX_MASK][1] );
	return( head_ptr->data_len );
}

int	SP_poll( mailbox mbox )
{
	int		num_bytes;
	int		ses;
	int		ret;

	Mutex_lock( &Struct_mutex );

	/* verify mbox */
	ses = SP_get_session( mbox );

	Mutex_unlock( &Struct_mutex );

	if( ses < 0 ) return( ILLEGAL_SESSION );

	ret = ioctl( mbox, FIONREAD, &num_bytes);
	if( ret < 0 ) return( ILLEGAL_SESSION );
	return( num_bytes );

}

int     SP_equal_group_ids( group_id g1, group_id g2 )
{
	if( g1.memb_id.proc_id == g2.memb_id.proc_id && g1.memb_id.time == g2.memb_id.time && g1.index == g2.index ) return( 1 );
	else return( 0 );
}

int SP_get_gid_offset_memb_mess() 
{
    return 0;
}

int SP_get_num_vs_offset_memb_mess() 
{
    return sizeof(group_id);
}

int SP_get_vs_set_offset_memb_mess() 
{
    return sizeof(group_id) + sizeof(int32);
}

int	SP_query_groups( mailbox mbox, int max_groups, char *groups[MAX_GROUP_NAME] )
{
	return( -1 );
}

int	SP_query_members( mailbox mbox, char *group, int max_members, char *members[MAX_GROUP_NAME] )
{
	return( -1 );
}

static  void	SP_kill( mailbox mbox )
{
	int	ses;
	int	i;

	Mutex_lock( &Struct_mutex );

	/* get mbox out of the data structures */
	ses = SP_get_session( mbox );
	if( ses < 0 ){ 
		Alarm( SESSION, "SP_kill: killing non existent session for mailbox %d (might be ok in a threaded case)\n",mbox );
		Mutex_unlock( &Struct_mutex );
		return;
	}

	close(mbox);
	for( i=ses+1; i < Num_sessions; i++ )
		memcpy( &Sessions[i-1], &Sessions[i], sizeof(sp_session) );
	Num_sessions--;

	Mutex_unlock( &Struct_mutex );
}

static	int	SP_get_session( mailbox mbox )
{
	int ses;

	for( ses=0; ses < Num_sessions; ses++ )
	{
		if( Sessions[ses].mbox == mbox ) return( ses );
	}
	return( -1 );
}
void	SP_error( int error )
{
	switch( error )
	{
		case ILLEGAL_SPREAD:
			Alarm( PRINT, "SP_error: (%d) Illegal spread was provided\n", error );
			break;
		case COULD_NOT_CONNECT:
			Alarm( PRINT, "SP_error: (%d) Could not connect. Is Spread running?\n", error );
			break;
		case REJECT_QUOTA:
			Alarm( PRINT, "SP_error: (%d) Connection rejected, to many users\n", error );
			break;
		case REJECT_NO_NAME:
			Alarm( PRINT, "SP_error: (%d) Connection rejected, no name was supplied\n", error );
			break;
		case REJECT_ILLEGAL_NAME:
			Alarm( PRINT, "SP_error: (%d) Connection rejected, illegal name\n", error );
			break;
		case REJECT_NOT_UNIQUE:
			Alarm( PRINT, "SP_error: (%d) Connection rejected, name not unique\n", error );
			break;
		case REJECT_VERSION:
			Alarm( PRINT, "SP_error: (%d) Connection rejected, library does not fit daemon\n", error );
			break;
		case CONNECTION_CLOSED:
			Alarm( PRINT, "SP_error: (%d) Connection closed by spread\n", error );
			break;
		case REJECT_AUTH:
			Alarm( PRINT, "SP_error: (%d) Connection rejected, authentication failed\n", error );
			break;
		case ILLEGAL_SESSION:
			Alarm( PRINT, "SP_error: (%d) Illegal session was supplied\n", error );
			break;
		case ILLEGAL_SERVICE:
			Alarm( PRINT, "SP_error: (%d) Illegal service request\n", error );
			break;
		case ILLEGAL_MESSAGE:
			Alarm( PRINT, "SP_error: (%d) Illegal message\n", error );
			break;
		case ILLEGAL_GROUP:
			Alarm( PRINT, "SP_error: (%d) Illegal group\n", error );
			break;
		case BUFFER_TOO_SHORT:
			Alarm( PRINT, "SP_error: (%d) The supplied buffer was too short\n", error );
			break;
		case GROUPS_TOO_SHORT:
			Alarm( PRINT, "SP_error: (%d) The supplied groups list was too short\n", error );
			break;
		case MESSAGE_TOO_LONG:
			Alarm( PRINT, "SP_error: (%d) The message body + group names was too large to fit in a message\n", error );
			break;
		default:
			Alarm( PRINT, "SP_error: (%d) unrecognized error\n", error );
	}
}

static	void    Flip_mess( message_header *head_ptr )
{
	head_ptr->type     	= Flip_int32( head_ptr->type );
	head_ptr->num_groups	= Flip_int32( head_ptr->num_groups );
	head_ptr->data_len 	= Flip_int32( head_ptr->data_len );
}
