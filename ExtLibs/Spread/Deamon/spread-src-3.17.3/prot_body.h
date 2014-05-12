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


#ifndef	INC_PROT_BODY
#define	INC_PROT_BODY

#include "configuration.h"
#include "spread_params.h"
#include "net_types.h"
#include "sp_events.h"
#include "protocol.h"


typedef	struct	dummy_packet_info {
	packet_header	*head;
	packet_body	*body;
	int		exist;
	int		proc_index;
} packet_info;

typedef	struct	dummy_up_queue {
	int		exist;
	scatter 	*mess;
} up_queue;

#undef	ext
#ifndef	ext_prot_body
#define	ext	extern
#else
#define ext
#endif

ext	down_queue	*Down_queue_ptr;
ext	up_queue	Up_queue[MAX_PROCS_RING+1];

ext	packet_info	Packets[MAX_PACKETS_IN_STRUCT];

ext	int32		Aru;
ext	int32		My_aru;
ext	int32		Highest_seq;
ext	int32		Highest_fifo_seq;
ext	int32		Last_discarded;
ext	int32		Last_delivered;
ext	int32		Last_seq;
ext	token_header	*Last_token;

ext	int		Transitional;
ext	configuration	Trans_membership;
ext	configuration	Commit_membership;
ext	configuration	Reg_membership;

ext	int		Last_num_retrans;
ext	int		Last_num_sent;

ext	sp_time		Token_timeout;
ext	sp_time		Hurry_timeout;

ext	sp_time		Alive_timeout;
ext	sp_time		Join_timeout;
ext	sp_time		Rep_timeout;
ext	sp_time		Seg_timeout;
ext	sp_time		Gather_timeout;
ext	sp_time		Form_timeout;
ext	sp_time		Lookup_timeout;
ext	int		Wide_network;

void	Prot_token_hurry();
void	Discard_packets();

#endif	/* INC_PROT_BODY */
