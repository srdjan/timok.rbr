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


#ifndef	INC_SPREAD_PARAMS
#define INC_SPREAD_PARAMS

#define		SP_MAJOR_VERSION	3
#define         SP_MINOR_VERSION        17
#define         SP_PATCH_VERSION        3
#define         SPREAD_PROTOCOL         3

#define		DEFAULT_SPREAD_PORT	4803

#ifndef SP_RUNTIME_DIR
#define         SP_RUNTIME_DIR          "/var/run/spread"
#endif
#ifndef SP_GROUP
#define         SP_GROUP                "spread"
#endif
#ifndef SP_USER
#define         SP_USER                 "spread"
#endif

#define		MAX_PROC_NAME		 20     /* including the null, so actually max 19, look for it if changed */

#define		MAX_PROCS_SEGMENT	128
#define		MAX_SEGMENTS		 20
#define		MAX_PROCS_RING		128
#define         MAX_INTERFACES_PROC      10

#define         MAX_REPS                 25
#define         MAX_FORM_REPS            20

#define		MAX_PACKETS_IN_STRUCT 	8192
#define		PACKET_MASK		0x00001fff

#define		MAX_SEQ_GAP		1600	/* used in flow control to limit difference between highest_seq and aru */

#define		WATER_MARK		500	/* used to limit incoming user messages */

#define		MAX_PRIVATE_NAME	 10     /* not including the null, look for it if changed */

#define		MAX_GROUP_NAME		 (1+MAX_PRIVATE_NAME+1+MAX_PROC_NAME)
					/* #private_name#proc_name  including the null */
#include        "sp_events.h"
#define		MAX_SESSIONS		( ( MAX_FD_EVENTS-5 ) / 2 ) /* reserves 2 for each connection */

#define		MAX_SESSION_MESSAGES	1000
#define         MAX_GROUPS_PER_MESSAGE  100     /* Each multicast can't send to more groups then this */

#endif /* INC_SPREAD_PARAMS */
