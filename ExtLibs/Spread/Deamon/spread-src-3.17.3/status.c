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


#define status_ext
#include <stdio.h>

#include "arch.h"
#include "spread_params.h"
#include "scatter.h"
#include "net_types.h"
#include "data_link.h"
#include "configuration.h"
#include "status.h"
#include "sp_events.h"
#include "alarm.h"

static	sp_time		Start_time;
static	channel		Report_channel;
static	sys_scatter	Report_scat;
static	packet_header	Pack;
static	configuration	Cn;


void	Stat_init()
{
	int16	dummy_port = 0;

	Start_time = E_get_time();

	Report_channel = DL_init_channel( SEND_CHANNEL, dummy_port, 0, Conf_my().id );

	Report_scat.num_elements = 2;
	Report_scat.elements[0].len = sizeof( packet_header );
	Report_scat.elements[0].buf = (char *)&Pack;
	Report_scat.elements[1].len = sizeof( status );
	Report_scat.elements[1].buf = (char *)&GlobalStatus;

	Pack.type = STATUS_TYPE;
	Pack.type = Set_endian( Pack.type );
	Pack.data_len = sizeof( status );
	Pack.proc_id = Conf_my().id;

	Cn = Conf();

	GlobalStatus.major_version = SP_MAJOR_VERSION;
	GlobalStatus.minor_version = SP_MINOR_VERSION;
	GlobalStatus.patch_version = SP_PATCH_VERSION;

	Alarm( STATUS, "Stat_init: went ok\n" );

}

void	Stat_handle_message( sys_scatter *scat )
{
	sp_time		now;
	sp_time		delta;
	packet_header	*pack_ptr;
	proc		p;
	int		ret;

	pack_ptr = (packet_header *)scat->elements[0].buf;
	if( ! ( pack_ptr->memb_id.proc_id == 15051963 && Conf_id_in_conf( &Cn, pack_ptr->proc_id ) != -1 ) )
	{
		Alarm( STATUS, "Stat_handle_message: Illegal monitor request\n");
		return;
	}

	now   = E_get_time();
	delta = E_sub_time( now, Start_time );
	GlobalStatus.sec = delta.sec;

	DL_send( Report_channel, pack_ptr->proc_id, pack_ptr->seq, &Report_scat );
	ret = Conf_proc_by_id( pack_ptr->proc_id, &p );
	if( ret < 0 )
		Alarm( STATUS, 
			"Stat_handle_message: sent status to Monitor at %d\n",
			pack_ptr->proc_id );
	else 	Alarm( STATUS, 
			"Stat_handle_message: sent status to Monitor at %s\n",
			p.name );
}

