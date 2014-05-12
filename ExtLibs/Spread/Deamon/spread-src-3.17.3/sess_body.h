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


#ifndef	INC_SESS_BODY
#define	INC_SESS_BODY

#ifndef ARCH_PC_WIN95

#include <sys/time.h>
#include <sys/types.h>

#else	/* ARCH_PC_WIN95 */

#include <winsock.h>

#endif	/* ARCH_PC_WIN95 */

#include "arch.h"
#include "protocol.h"
#include "session.h"
#include "skiplist.h"

#define		MEMB_SESSION		0x00000001
#define		OP_SESSION		0x00000010
#define         PRE_AUTH_SESSION        0x00000100

#define		Is_memb_session( status )	( status &  MEMB_SESSION )
#define		Set_memb_session( status )	( status |  MEMB_SESSION )
#define		Clear_memb_session( status )	( status & ~MEMB_SESSION )

#define		Is_op_session( status )		( status &  OP_SESSION )
#define		Set_op_session( status )	( status |  OP_SESSION )
#define		Clear_op_session( status )	( status & ~OP_SESSION ) 

#define		Is_preauth_session( status )	( status &  PRE_AUTH_SESSION )
#define		Set_preauth_session( status )	( status |  PRE_AUTH_SESSION )
#define		Clear_preauth_session( status )	( status & ~PRE_AUTH_SESSION ) 
 
typedef	struct	dummy_member {
	char			private_name[MAX_GROUP_NAME]; 
	int32			status; /* new: joined in the last trans =>1, or not =>0 */
	int32			proc_id;
        int32                   p_ind;
} member;

typedef	struct	dummy_group {
	char			name[MAX_GROUP_NAME]; 
	group_id		grp_id;
        int                     changed;  /* No longer use negative grp_id.index to flag
                                           * groups that have changed. */
	int			num_members;
        Skiplist                MembersList;
	int			num_local;
	mailbox			mbox[MAX_SESSIONS];
        route_mask              grp_mask;
} group;

#undef	ext
#ifndef ext_sess_body
#define ext extern
#else
#define ext
#endif

ext	proc		My;

ext	int		Num_groups;
ext	group		Groups;

ext	int		Num_sessions;
ext	session		Sessions[MAX_SESSIONS+1]; /* +1 for rejecting the next one */

ext	int		Session_threshold;

ext	char		Temp_buf[100000];
/*
 * ext	char		Temp_MessageGroupName_buf[MAX_GROUPS_PER_MESSAGE*MAX_GROUP_NAME];
 */
void	Sess_set_active_threshold(void);
void	Sess_write( int ses, message_link *mess_link, int *needed );
void	Sess_dispose_message( message_link *mess_link );
int	Sess_get_session( char *name );
int	Sess_get_session_index (int mbox);

#endif	/* INC_SESS_BODY */
