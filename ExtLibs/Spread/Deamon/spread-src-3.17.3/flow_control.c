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


#include "flow_control.h"
#include "configuration.h"
#include "membership.h"
#include "prot_body.h"
#include "status.h"
#include "alarm.h"

static	int16	Window;
static	int16	Personal_window;
static	configuration	Cn;

void	FC_init( )
{
	Window = 60;
	Personal_window = 15;

	Cn = Conf();

	GlobalStatus.window = Window;
	GlobalStatus.personal_window = Personal_window;
}

void	FC_new_configuration( )
{
	Last_num_retrans = 0;
	Last_num_sent    = 0;
}

int	FC_allowed( int flow_control, int num_retrans )
{
	int	allowed;

	if( Memb_state() == EVS ) return( 0 );
	if( Highest_seq > (Aru + MAX_SEQ_GAP) ) return( 0 );
	allowed = Window + Personal_window - flow_control;
	if (allowed < 0) allowed = 0;
	if (allowed > Window) allowed = Window;
	if (allowed > Personal_window) allowed = Personal_window;

	return(allowed);
}

void	FC_handle_message( sys_scatter *scat )
{

	int16		*cur_fc_buf;
	packet_header	*pack_ptr;
	proc		dummy_proc;
	int		my_index;
	int16		temp_window,temp_personal_window;

	pack_ptr = (packet_header *)scat->elements[0].buf;
        if ( ! Conf_get_dangerous_monitor_state() ) {
                Alarm( FLOW_CONTROL, "FC_handle_message: Request to change flow control from (%d.%d.%d.%d) denied. Monitor in safe mode\n", IP1(pack_ptr->proc_id), IP2(pack_ptr->proc_id), IP3(pack_ptr->proc_id), IP4(pack_ptr->proc_id) );
                return;
        }

	if( ! ( pack_ptr->memb_id.proc_id == 15051963 && Conf_id_in_conf( &Cn, pack_ptr->proc_id ) != -1 ) )
	{
		Alarm( FLOW_CONTROL, 
			"FC_handle_message: Illegal monitor request\n");
		return;
	}
	cur_fc_buf = (int16 *)scat->elements[1].buf;

	my_index = Conf_proc_by_id( Conf_my().id, &dummy_proc );

	if( Same_endian( pack_ptr->type ) ) {
		temp_window = cur_fc_buf[Conf_num_procs( &Cn )];
		temp_personal_window = cur_fc_buf[my_index];
	}else{
		temp_window = Flip_int16( cur_fc_buf[Conf_num_procs( &Cn )] );
		temp_personal_window = Flip_int16( cur_fc_buf[my_index] );
	}
	if( temp_window != -1 ) Window = temp_window;
	if( temp_personal_window != -1 ) Personal_window = temp_personal_window;
	GlobalStatus.window = Window;
	GlobalStatus.personal_window = Personal_window;
	Alarm( FLOW_CONTROL, 
		"FC_handle_message: Got monitor mess, Window %d Personal %d\n", 
			Window, Personal_window );
}
