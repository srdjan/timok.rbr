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


#ifndef INC_MUTEX
#define INC_MUTEX

#ifndef _REENTRANT

#define MUTEX_STATIC_INIT 	0

#define mutex_type 		int

#define Mutex_init( mutex )    
#define Mutex_lock( mutex )    
#define Mutex_unlock( mutex ) 

static	int	Trylock_firsttime = 0;

#define Mutex_trylock( mutex )	 Trylock_firsttime; Trylock_firsttime = 1

#else	/* _REENTRANT */

#ifndef ARCH_PC_WIN95
#include <pthread.h>

#define MUTEX_STATIC_INIT 	PTHREAD_MUTEX_INITIALIZER

#define mutex_type 		pthread_mutex_t

#define Mutex_init( mutex )	pthread_mutex_init( (mutex), NULL )
#define Mutex_lock( mutex )	pthread_mutex_lock( mutex )
#define Mutex_unlock( mutex )	pthread_mutex_unlock( mutex )
#define Mutex_trylock( mutex )	pthread_mutex_trylock( mutex )

#else	/* ARCH_PC_WIN95 */

	/* ### Static init is implemented with a problem which cannot be solved */
#include <process.h>

#define MUTEX_STATIC_INIT 	0

#define mutex_type 		CRITICAL_SECTION

#define Mutex_init( mutex )	InitializeCriticalSection( mutex )
#define Mutex_lock( mutex )	EnterCriticalSection( mutex )
#define Mutex_unlock( mutex )	LeaveCriticalSection( mutex )

static	int 	Trylock_firsttime = 0;
#define Mutex_trylock( mutex )	Trylock_firsttime; Trylock_firsttime = 1

#endif /* ARCH_PC_WIN95 */


#endif /* _REENTRANT */

#endif /* INC_MUTEX */
