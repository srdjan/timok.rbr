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


#ifndef INC_NETWORK
#define INC_NETWORK

#include "arch.h"
#include "scatter.h"
#include "configuration.h"

void	Net_init();
void	Net_set_membership( configuration memb );
int	Net_bcast( sys_scatter *scat );
int     Net_queue_bcast(sys_scatter *scat);
int     Net_flush_bcast(void);
int	Net_scast( int16 seg_index, sys_scatter *scat );
int	Net_ucast( int32 proc_id, sys_scatter *scat );
int	Net_recv ( channel fd, sys_scatter *scat );
int	Net_send_token( sys_scatter *scat );
int	Net_recv_token( channel fd, sys_scatter *scat );
int	Net_ucast_token( int32 proc_id, sys_scatter *scat );
channel *Net_bcast_channel(void);
channel *Net_token_channel(void);
void    Net_num_channels(int *num_bcast, int *num_token);

#endif	/* INC_NETWORK */ 
