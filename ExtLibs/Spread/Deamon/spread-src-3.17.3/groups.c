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
#include <assert.h>

#include "spread_params.h"
#include "net_types.h"
#include "protocol.h"
#include "prot_body.h"
#include "sess_types.h"
#include "sess_body.h"
#include "groups.h"
#include "objects.h"
#include "memory.h"
#include "sp_events.h"
#include "status.h"
#include "alarm.h"
#include "membership.h"
#if     ( SPREAD_PROTOCOL > 3 )
#include "queues.h"
#endif
#include "skiplist.h"
#include "alarm.h"
#include "message.h"

#ifndef NULL
#define		NULL 	0
#endif	/* NULL */

#define		ESTABLISHED_MEMBER		0
#define		NEW_MEMBER			1
#define		PARTITIONED_MEMBER		2
#define		Is_established_member( status )	( status == ESTABLISHED_MEMBER )
#define		Is_new_member( status )		( status == NEW_MEMBER )
#define		Is_partitioned_member( status )	( status == PARTITIONED_MEMBER )

typedef struct dummy_groups_buf_link {
  char buf[GROUPS_BUF_SIZE];
  int  bytes;
  struct dummy_groups_buf_link *next;
} groups_buf_link;
 
struct worklist {
  char name[MAX_GROUP_NAME];
  Skiplist *groups;
};

char *printgroup(void *vgrp) {
  group *grp = (group *)vgrp;
  return grp->name;
}

static	int		Gstate;
static	configuration	Trans_memb;
static	membership_id	Trans_memb_id;
static	configuration	Reg_memb;
static	membership_id	Reg_memb_id;
static	char		Mess_buf[MAX_MESSAGE_BODY_LEN];

static  groups_buf_link *Groups_bufs;
static  int             Num_mess_gathered;
static  int             Num_daemons_gathered;
static  message_link    Gathered; /* Groups messages */

#if 0
static	group		Work[MAX_PROCS_RING+1];
#endif
static  int             Groups_control_down_queue;

static	int		G_id_is_equal( group_id g1, group_id g2 );
static  void            G_print_group_id( group_id g );
static	group		*G_get_group( char *group_name );
static	member		*G_get_member( group *grp, char *private_group_name );
static	int		G_build_memb_buf( group *grp, message_obj *msg,
					  char buf[] );
static	int		G_build_memb_vs_buf( group *grp, message_obj *msg,
					     char buf[], int32 caused );
static	message_link	*G_build_trans_mess( group *grp );

static	void		G_stamp_groups_buf( char buf[] );
static  void            G_build_groups_msg_hdr( message_obj *msg, int groups_bytes );
static	int		G_build_groups_buf( char buf[], struct skiplistnode **iter_ptr );
static	int		G_mess_to_groups( message_link *mess_link, char *name,
					  struct worklist *work );
static	int		G_smallest_group_indices( Skiplist *work, struct worklist *indices[] );
static	void		G_compute_and_notify(void);
static	void		G_print(void);
static  void            G_empty_groups_bufs(void);

static  Skiplist        GroupsList;

static  int             G_compare(void *, void *);

static  int     G_compare(void *a, void *b)
{
  /* Takes two Group records and compares them based on their keys (name) */
  /* This will work for any record type that has a char[LENGTH] as the first
     member of the struct and is the key */
  assert(a);
  assert(b);
  return strcmp((char *)a, (char *)b);
}

int     G_member_recordcompare(void *a, void *b)
{
  int compared;
  /* Takes two Member records and compares them based on their keys (name) */
  member        *am = (member *)a, *bm = (member *)b;
  assert(a);
  assert(b);
  compared = strcmp( bm->private_name, am->private_name );
  if(compared > 0)
    return -1;
  if(compared == 0)
    return 0;
  return 1;
}
int     G_member_keycompare(void *a, void *b)
{
  int compared;
  /* Takes two Member records and compares them based on their keys (name) */
  member        *bm = (member *)b;
  char          *am = (char *)a;
  assert(a);
  assert(b);
  compared = strcmp( bm->private_name, am );
  if(compared > 0)
    return -1;
  if(compared == 0) {
    return 0;
  }
  return 1;
}
/* Take two worklist pointers and compare them by the ->groups pointers */
/* We do not actually care about the order they are stored in the skiplist
 * that is why we are using meaningless pointers as keys.
 */
static int     G_work_groups_comp(void *a, void *b)
{
        struct worklist *wA, *wB;
        assert(a);
        assert(b);
        wA = (struct worklist *)a;
        wB = (struct worklist *)b;
        
        if (wA->groups < wB->groups)
                return(-1);
        if (wA->groups == wB->groups)
                return(0);
        return(1);
}
static int     G_work_groups_keycomp(void *key, void *b)
{
        struct worklist *wB;
        Skiplist *wKey;
        assert(key);
        assert(b);
        wB = (struct worklist *)b;
        wKey = (Skiplist *)key;
        if (wKey < wB->groups)
                return(-1);
        if (wKey == wB->groups)
                return(0);
        return(1);
}
#if 0
int     G_group_revproc_comp(void *a, void *b) {
  int32 aid, bid;
  struct worklist *A=(struct worklist *)a, *B=(struct worklist *)b;
  aid=A->proc_index;
  bid=B->proc_index;
  return (aid>bid)?(-1):((aid==bid)?0:1);
}
int     G_group_revproc_keycomp(void *a, void *b) {
  int32 aid, bid;
  struct worklist *B=(struct worklist *)b;
  bid=B->proc_index;
  aid = *(int32 *)a;
  return (aid>bid)?-1:((aid==bid)?0:1);
}
#endif
void	G_init()
{
        int     ret;

	Alarm( GROUPS, "G_init: \n" );

	Num_groups = 0;
	GlobalStatus.num_groups = Num_groups;

	sl_init(&GroupsList);
	/* Key's address is the same as records address and is a null
	   terminated string, so we can use the same function to compare
	   record<->record
	   key<->record */
	sl_set_compare(&GroupsList,
		       G_compare,
		       G_compare);

	/*	Groups.next = NULL;
		Groups.prev = NULL; */

        ret = Mem_init_object(GROUP, sizeof(group), 100, 0);
        if (ret < 0)
        {
                Alarm(EXIT, "G_init: Failure to Initialize GROUP memory objects\n");
        }

        ret = Mem_init_object(MEMBER, sizeof(member), 200, 0);
        if (ret < 0)
        {
                Alarm(EXIT, "G_init: Failure to Initialize MEMBER memory objects\n");
        }
        ret = Mem_init_object(GROUPS_BUF_LINK, sizeof(groups_buf_link), 1, 1);
        if( ret < 0 )
        {
                Alarm(EXIT, "G_init: Failure to Initialize GROUPS_BUF_LINK memory objects\n");
        }
#if     ( SPREAD_PROTOCOL == 3 )
        Groups_control_down_queue = 0;
#else
        Groups_control_down_queue = init_queuesess(Groups_down_qs);
#endif

        Groups_bufs          = NULL;
        Num_mess_gathered    = 0;
        Num_daemons_gathered = 0;
	Gathered.next = NULL;

	Gstate = GOP;
	GlobalStatus.gstate = Gstate;
}

void	G_handle_reg_memb( configuration reg_memb, membership_id reg_memb_id )
{
	group		*grp, *nextgroup;
	member		*mbr, *nextmember;
	struct skiplistnode *giter, *iter, *passed_iter;
        groups_buf_link     *grps_buf_link;
	message_link	*mess_link;
	down_link	*down_ptr;
        message_obj     *msg;
        message_header  *head_ptr;
	int		num_bytes;
	int		needed;
	int		ses;
	int		i;


	Alarm( GROUPS, "G_handle_reg_memb:  with (%d.%d.%d.%d, %d) id\n", 
               IP1(reg_memb_id.proc_id),IP2(reg_memb_id.proc_id),IP3(reg_memb_id.proc_id),
               IP4(reg_memb_id.proc_id), reg_memb_id.time );
        
	switch( Gstate )
	{
	    case GOP:
		Alarm( EXIT, "G_handle_reg_memb in GOP\n");

		break;

	    case GTRANS:
		/* 
		 * Save reg_memb and reg_memb_id
		 * if previous Trans_memb is equal to reg_memb then:
		 * 	for every changed group
		 *		eliminate partitioned members
		 * 		set Grp_id to (reg_memb_id, 1)
		 *		notify local members of regular membership
		 *	Shift to GOP
		 * else
		 *	for every changed group
		 *		eliminate partitioned members
		 *		set Grp_id to (reg_memb_id, -1)
		 *	Replace protocol queue, raise event thershold
		 *	Build groups message -- only local members
		 *	Send groups message
		 *	Shift to GGATHER
		 */
		Alarm( GROUPS, "G_handle_reg_memb in GTRANS\n");

		Reg_memb = reg_memb;
		Reg_memb_id = reg_memb_id;

		if( Conf_num_procs( &Trans_memb ) == Conf_num_procs( &Reg_memb ) )
		{
		  giter = sl_getlist( &GroupsList );
		  grp = (giter)?(group *)giter->data:NULL;
		  for( ; grp != NULL ; grp = nextgroup )
		    {
                        nextgroup = sl_next( &GroupsList, &giter );
			if( grp->changed )
			{
			    /* The group has changed */
			    /* eliminating partitioned members */
                            iter = sl_getlist( &grp->MembersList );
                            mbr = (iter)?(member *)iter->data:NULL;
                            for( ; mbr != NULL ; mbr = nextmember )
                            {
                                nextmember = sl_next( &grp->MembersList, &iter );
                                if( Is_partitioned_member( mbr->status ) )
				{
					/* discard this member - proc no longer in membership */
                                        sl_remove ( &grp->MembersList,mbr->private_name, dispose);
                                        grp->num_members--;
				}
			    }
                            if( grp->num_members == 0 )
			    {
				/* discard this empty group */
                                sl_destruct ( &grp->MembersList, dispose);
                                sl_remove (  &GroupsList, grp->name, dispose);
				Num_groups--;
				GlobalStatus.num_groups = Num_groups;
			    }else{
                                Alarm( GROUPS, "G_handle_reg_memb: skipping state transfer for group %s.\n", grp->name );
                                Alarm( DEBUG,  "G_handle_reg_memb: changing group_id from: " );
                                G_print_group_id( grp->grp_id );
				grp->grp_id.memb_id = Reg_memb_id;
				grp->grp_id.index   = 1;
                                grp->changed        = 0;
                                Alarm( DEBUG, " to: " );
                                G_print_group_id( grp->grp_id );
                                Alarm( DEBUG, "\n" );

				if( grp->num_local > 0 ){
                                        /* send members regular membership */
                                        msg = Message_new_message();
                                        num_bytes = G_build_memb_vs_buf( grp, msg, Mess_buf, CAUSED_BY_NETWORK );
                                        
                                        /* create the mess_link */
                                        mess_link = new( MESSAGE_LINK );
                                        Message_Buffer_to_Message_Fragments( msg, Mess_buf, num_bytes );
                                        mess_link->mess = msg;
                                        Obj_Inc_Refcount(mess_link->mess);
                                        
                                        /* notify local members */
                                        needed = 0;
                                        for( i=0; i < grp->num_local; i++ )
                                        {
                                                ses = Sess_get_session_index ( grp->mbox[i] );
                                                if( Is_memb_session( Sessions[ ses ].status ) )
                                                        Sess_write( ses, mess_link, &needed );
                                        }
                                        Message_Dec_Refcount(msg);
                                        if( !needed ) Sess_dispose_message( mess_link );
                                }
			    }
			}
		    }
		    Gstate = GOP;
		    GlobalStatus.gstate = Gstate;
		}else{
		/*
		 * else
		 *	for every changed group
		 *		eliminate partitioned members
		 *		set Grp_id to (reg_memb_id, -1)
		 *	Replace protocol queue, raise event thershold
		 *	build groups message -- only local members
		 *	Send groups message
		 *	Shift to GGATHER
		 */
		    giter = sl_getlist( &GroupsList );
		    grp = (giter)?(group *)giter->data:NULL;
		    for( ; grp != NULL ; grp = nextgroup )
		    {
                        nextgroup = sl_next( &GroupsList, &giter );
			if( grp->changed )
			{
			    /* The group has changed */
			    /* eliminating partitioned members */
                            iter = sl_getlist( &grp->MembersList );
                            mbr = (iter)?(member *)iter->data:NULL;
                            for( ; mbr != NULL ; mbr = nextmember )
                            {
                                nextmember = sl_next( &grp->MembersList, &iter );
				if( Is_partitioned_member( mbr->status ) )
				{
					/* discard this member - proc no longer in membership */
                                        sl_remove ( &grp->MembersList,mbr->private_name, dispose);
					grp->num_members--;
				}
			    }
			    if( grp->num_members == 0 )
			    {
				/* discard this empty group */
                                sl_destruct ( &grp->MembersList, dispose);
                                sl_remove (  &GroupsList, grp->name, dispose);
				Num_groups--;
				GlobalStatus.num_groups = Num_groups;
                            }
			}
		    }
		    /* raise events threshold */
		    Session_threshold = MEDIUM_PRIORITY;
		    Sess_set_active_threshold();

                    /* Replace down queue */
                    Prot_set_down_queue( GROUPS_DOWNQUEUE );

		    /* build and send Groups message */
                    /* Nowadays, we can send multiple groups messages.  No group has
                     * data in more than one.  As an optimization, only the last message is
                     * AGREED, and all previous ones are RELIABLE.  G_handle_groups uses this
                     * knowledge when parsing Groups messages. */
                    passed_iter = NULL; 
                    do { 
                            msg = Message_new_message();
                            grps_buf_link = new( GROUPS_BUF_LINK );
                            grps_buf_link->next = Groups_bufs;
                            Groups_bufs = grps_buf_link;
                            grps_buf_link->bytes = G_build_groups_buf(grps_buf_link->buf, &passed_iter);
                            G_build_groups_msg_hdr( msg, grps_buf_link->bytes );
                            head_ptr = Message_get_message_header(msg);
                            if( passed_iter )
                                    head_ptr->type |= RELIABLE_MESS;
                            else
                                    head_ptr->type |= AGREED_MESS;
                            Message_Buffer_to_Message_Fragments( msg, grps_buf_link->buf, grps_buf_link->bytes );

                            down_ptr = Prot_Create_Down_Link(msg, Message_get_packet_type(head_ptr->type), 0, 0);
                            down_ptr->mess = msg; 
                            Obj_Inc_Refcount(down_ptr->mess);
                            /* Use control queue--not normal session queues */
                            Prot_new_message( down_ptr, Groups_control_down_queue );
                            Message_Dec_Refcount(msg);
                            Alarm( GROUPS, "G_handle_reg_memb: (%8s) GROUPS message sent in GTRANS with %d bytes\n",
                                   (passed_iter) ? "RELIABLE" : "AGREED", grps_buf_link->bytes );
                    } while( passed_iter != NULL );

		    Gstate = GGATHER;
		    GlobalStatus.gstate = Gstate;
		}
		break;

	    case GGATHER:
		Alarm( EXIT, "G_handle_reg_memb in GGATHER\n");

		break;

	    case GGT:
		/*
		 * Save reg_memb and reg_memb_id
		 * Clear all retained Groups messages
		 * Stamp own Groups message with current membership id
		 * Send group message
		 * Shift to GGATHER
		 */
		Alarm( GROUPS, "G_handle_reg_memb in GGT\n");

		Reg_memb = reg_memb;
		Reg_memb_id = reg_memb_id;

		/* Clear retained Groups messages in Gathered */
		for( i=0; i < Num_mess_gathered; i++ )
		{
			mess_link = Gathered.next;
			Gathered.next = mess_link->next;
			Sess_dispose_message( mess_link );
		}
                Num_mess_gathered    = 0;
                Num_daemons_gathered = 0;

                for( grps_buf_link = Groups_bufs; grps_buf_link; grps_buf_link = grps_buf_link->next )
                {
                        /*  Stamp own Groups message in buffer with current membership id */
                        G_stamp_groups_buf( grps_buf_link->buf );

                        /* send Groups message */
                        msg = Message_new_message();
                        G_build_groups_msg_hdr( msg, grps_buf_link->bytes );
                        head_ptr = Message_get_message_header(msg);
                        if( grps_buf_link->next )
                                head_ptr->type |= RELIABLE_MESS;
                        else
                                head_ptr->type |= AGREED_MESS;
                        Message_Buffer_to_Message_Fragments( msg, grps_buf_link->buf, grps_buf_link->bytes );
               
                        down_ptr = Prot_Create_Down_Link(msg, Message_get_packet_type(head_ptr->type), 0, 0);
                        down_ptr->mess = msg;
                        Obj_Inc_Refcount(down_ptr->mess);
                        /* Use control queue--not normal session queues */
                        Prot_new_message( down_ptr, Groups_control_down_queue );
                        Message_Dec_Refcount(msg);
                        Alarm( GROUPS, "G_handle_reg_memb: (%8s) GROUPS message sent in GGT with %d bytes\n",
                               (grps_buf_link->next) ? "RELIABLE" : "AGREED", grps_buf_link->bytes );
                }
        
		Gstate = GGATHER;
		GlobalStatus.gstate = Gstate;

		break;
	}
}

void	G_handle_trans_memb( configuration trans_memb, membership_id trans_memb_id )
{
	group		*grp, *nextgroup;
	member		*mbr, *nextmember;
	struct skiplistnode *giter, *iter;
	int		group_changed;
	message_link	*mess_link;
	int		needed;
	int		ses;
	int		i;

	Alarm( GROUPS, "G_handle_trans_memb: \n" );

	switch( Gstate )
	{
	    case GOP:
		/* 
		 * Save transitional membership
		 * For every group that has members that are not in the trans_memb do:
		 *	mark group members that are not in trans_memb as partitioned.
		 * 	notify local members with an empty transitional group mess.
		 * 	mark group as changed (index = -1)
		 * Shift to GTRANS
		 */
		Alarm( GROUPS, "G_handle_trans_memb in GOP\n");

		Trans_memb    = trans_memb;
                Trans_memb_id = trans_memb_id;
                Alarm( GROUPS, "G_handle_trans_memb: Received trans memb id of:"
                       " {proc_id: %d "
                       " time:    %d}\n", Trans_memb_id.proc_id, Trans_memb_id.time );

		giter = sl_getlist( &GroupsList );
		grp = (giter)?(group *)giter->data:NULL;
		for( ; grp != NULL ; grp = nextgroup )
		  {
                      nextgroup = sl_next( &GroupsList, &giter );
		      group_changed = 0;
		      iter = sl_getlist( &grp->MembersList );
		      mbr = (iter)?(member *)iter->data:NULL;
		        for( ; mbr != NULL ; mbr = nextmember )
		        {
                                nextmember = sl_next( &grp->MembersList, &iter );
				if( Conf_id_in_conf( &Trans_memb, mbr->proc_id ) == -1 )
				{
					/* mark this member as partitioned - proc no longer in membership */
					mbr->status = PARTITIONED_MEMBER;
					group_changed = 1;
				}
                        }
			if( group_changed ) 
                        {
				if( grp->num_local > 0 )
				{
					/* send members transitional membership */
                                        mess_link = G_build_trans_mess( grp );
					needed = 0;
					for( i=0; i < grp->num_local; i++ )
					{
						ses = Sess_get_session_index ( grp->mbox[i] );
						if( Is_memb_session( Sessions[ ses ].status ) )
							Sess_write( ses, mess_link, &needed );
					}
					if( !needed ) Sess_dispose_message( mess_link );
				}
                                Alarm( DEBUG, "G_handle_trans_memb: changed group %s in GOP, change"
                                       " group id from: ", grp->name );
                                G_print_group_id( grp->grp_id );
                                grp->grp_id.memb_id = trans_memb_id;
                                grp->grp_id.index   = 1;  /* Not technically needed, but not bad, either. */
                                grp->changed        = 1;
                                Alarm( DEBUG, " to: " );
                                G_print_group_id( grp->grp_id );
                                Alarm( DEBUG, "\n" );
			}
		}

		Gstate = GTRANS;
		GlobalStatus.gstate = Gstate;

		break;

	    case GTRANS:
		Alarm( EXIT, "G_handle_trans_memb in GTRANS\n");

		break;

	    case GGATHER:
		/* 
		 * Save transitional membership
		 * For every group that has members that are not in the
		 *  trans_memb do:
		 *	discard group members that are not in trans_memb
		 * 	if group is changed then mark it as changed (index = -1) (it might be already changed, but its ok).
		 * Shift to GGT
		 *
		 *	Note: there is no need to notify local members with a transitional group mess
		 *	      becuase no message will come between the trans group memb and the next reg group memb.
		 *	Note: this cascading deletes of members that are not in transitional membership actually
		 *	      opens the door for implementation of the ERSADS97 algorithm.
		 */
		Alarm( GROUPS, "G_handle_trans_memb in GGATHER\n");

		Trans_memb    = trans_memb;
                Trans_memb_id = trans_memb_id; /* Need this because we deliver the transitional again if we complete
                                                * the state exchange during GGT. */
                Alarm( GROUPS, "G_handle_trans_memb: Received trans memb id of:"
                       " {proc_id: %d "
                       " time:    %d}\n", Trans_memb_id.proc_id, Trans_memb_id.time );

		giter = sl_getlist( &GroupsList );
		grp = (giter)?(group *)giter->data:NULL;
		for( ; grp != NULL ; grp = nextgroup )
                {
                        nextgroup = sl_next( &GroupsList, &giter );
			group_changed = 0;
			iter = sl_getlist( &grp->MembersList );
			mbr = (iter)?(member *)iter->data:NULL;
		        for( ; mbr != NULL ; mbr = nextmember )
			{
                                nextmember = sl_next( &grp->MembersList, &iter );
				if( Conf_id_in_conf( &Trans_memb, mbr->proc_id ) == -1 )
				{
				/* discard this member - proc no longer in membership */
                                    sl_remove ( &grp->MembersList,mbr->private_name, dispose);
				    grp->num_members--;
				  
                                    group_changed = 1;
				}
			}
			if( grp->num_members == 0 )
			{
				/* discard this empty group */
                                sl_destruct ( &grp->MembersList, dispose);
				sl_remove ( &GroupsList, grp->name, dispose);
				Num_groups--;
				GlobalStatus.num_groups = Num_groups;

			}else if( group_changed ) {
                                grp->changed = 1;
			}
		}

		Gstate = GGT;
		GlobalStatus.gstate = Gstate;

		break;

	    case GGT:
		Alarm( EXIT, "G_handle_trans_memb in GGT\n");

		break;
	}
}

void	G_handle_join( char *private_group_name, char *group_name )
{
	group		*grp, *new_grp;
	member		*mbr, *new_mbr;
	int		needed;
	char		*num_vs_ptr; /* num members in virtual-synchrony/failure-atomicity set */
	int		num_bytes;
	char		proc_name[MAX_PROC_NAME];
	char		private_name[MAX_PRIVATE_NAME+1];
	int		new_p_ind, p_ind1;
	proc		new_p, p1;
	int		ses;
	mailbox		new_mbox;
	message_link	*mess_link;
	message_header	*head_ptr;
        message_obj     *msg, *joiner_msg;
	char		*vs_ptr;     /* the virtual synchrony set */
	int		i;
	int32u          temp;

	Alarm( GROUPS, "G_handle_join: %s joins group %s\n", private_group_name, group_name );

	switch( Gstate )
	{
	    case GOP:
	    case GTRANS:

		if (Gstate == GOP) Alarm( GROUPS, "G_handle_join in GOP\n");
		if (Gstate == GTRANS) Alarm( GROUPS, "G_handle_join in GTRANS\n");

		/*
		 * if already in group then ignore
		 * if the group is unchanged and new member is coming from alive daemon then: 
		 *    Insert to group as established
		 *    Increment Grp_id
		 *    Notify all local members of a regular membership caused by join
		 * else if group is changed and coming from alive daemon then 
		 *    Insert to group as new
		 *    Increment Grp_id
		 *    if there are local members then
		 *	build a membership with all members, and vs set with all established members
		 *	notify all local established members with that membership (caused by network)
                 *	if new member is local
		 *	    notify new member with membership and self vs set (caused by network)
		 *      notify all local members a transitional membership
		 *    mark new member as established
		 * else (if new member is coming from a partitioned daemon then)
		 *    Insert to group as partitioned 
                 *    Increment Grp_id, and mark group as changed if not already done
		 *    if there are local members then
		 *      build a membership with all members and vs set with all established members
		 *      notify all local members with that membership (caused by network)
		 *      notify all local members with transitional membership
		 *
		 * Note: remember that when delivering a regular message while in GTRANS, you should use the
		 * mbox list of the group. You should still be cautious when delivering memberships to take
		 * care of the fact that the new guy gets a different treatment.
		 */
		G_private_to_names( private_group_name, private_name, proc_name );

		new_p_ind = Conf_proc_by_name( proc_name, &new_p );
		if( new_p_ind < 0 )
		{
			Alarm( PRINT, "G_handle_join: illegal proc_name %s in private_group %s \n",
				proc_name, private_group_name );
			return;
		}
		grp = G_get_group( group_name );
		if( grp == NULL )
		{
			new_grp = new( GROUP );
			memset( new_grp->name, 0, MAX_GROUP_NAME );
			strcpy( new_grp->name, group_name );
			sl_init( &new_grp->MembersList );
			sl_set_compare( &new_grp->MembersList,
					G_member_recordcompare,
					G_member_keycompare);
                        if( Gstate == GOP) {
                                new_grp->changed        = 0;
                                new_grp->grp_id.memb_id = Reg_memb_id;
                        } else { /* Gtrans */
                                new_grp->changed        = 1;
                                new_grp->grp_id.memb_id = Trans_memb_id;
                        }
                        new_grp->grp_id.index = 0; /* 0 because we will definitely increment it */
                        Alarm( DEBUG, "G_handle_join: New group added with group id: " );
                        G_print_group_id( new_grp->grp_id );
                        Alarm( DEBUG, "\n" );

			new_grp->num_members = 0;
			new_grp->num_local = 0;

			sl_insert( &GroupsList, new_grp );
			Num_groups++; /*sl need this?*/
			GlobalStatus.num_groups = Num_groups;
			grp = new_grp;
                }
		mbr = G_get_member( grp, private_group_name );
		if( mbr != NULL )
		{
			Alarm( PRINT, "G_handle_join: %s is already in group %s\n",
				private_group_name, group_name );
			return;
		}
		/* Add a new member as ESTABLISHED (might change later on depending on the situation */
		new_mbr = new( MEMBER );
		memset( new_mbr->private_name, 0, MAX_GROUP_NAME );
		strcpy( new_mbr->private_name, private_group_name );
		new_mbr->proc_id = new_p.id;
		new_mbr->status = ESTABLISHED_MEMBER;
		new_mbr->p_ind = new_p_ind;

		sl_insert( &grp->MembersList, new_mbr );
		grp->num_members++;

		/* if member is local then add mbox */
		if( new_mbr->proc_id == My.id )
		{
			ses = Sess_get_session( private_name );
			if( ses < 0 ) Alarm( EXIT, "G_handle_join: local session does not exist\n");
			grp->mbox[ grp->num_local ] = Sessions[ ses ].mbox;
			grp->num_local++;
			new_mbox = Sessions[ ses ].mbox;
		}else new_mbox = -1;

                /* This is the meat */
		if( Gstate == GOP || ( Conf_id_in_conf( &Trans_memb, new_p.id ) != -1 ) )
		{
			/* new member is coming from alive daemon */
			if( !grp->changed )
			{
				/* group is unchanged */
				/* Increment group id */
				grp->grp_id.index++;

				/* Notify local members */
				if( grp->num_local > 0 )
				{
                                        msg = Message_new_message();
					num_bytes = G_build_memb_buf( grp, msg, Mess_buf );
					head_ptr = Message_get_message_header(msg);

					head_ptr->type |= CAUSED_BY_JOIN ;

					/* notify all local members */
					num_vs_ptr = &Mess_buf[ num_bytes ];
					num_bytes += sizeof( int32 );
                                        temp = 1;
                                        memcpy( num_vs_ptr, &temp, sizeof( int32 ) ); /* *num_vs_ptr = 1; */

					vs_ptr = (char *)&Mess_buf[ num_bytes ];
					memcpy( vs_ptr, new_mbr->private_name, MAX_GROUP_NAME );
					num_bytes += MAX_GROUP_NAME;

					head_ptr->data_len += ( sizeof(int32) + MAX_GROUP_NAME );

					mess_link = new( MESSAGE_LINK );
                                        Message_Buffer_to_Message_Fragments( msg, Mess_buf, num_bytes );
                                        mess_link->mess = msg;
                                        Obj_Inc_Refcount(mess_link->mess);
					
					needed = 0;
					for( i=0; i < grp->num_local; i++ )
					{
						ses = Sess_get_session_index ( grp->mbox[i] );
						if( Is_memb_session( Sessions[ ses ].status ) )
							Sess_write( ses, mess_link, &needed );
					}
                                        if ( !needed ) Sess_dispose_message( mess_link );
                                        Message_Dec_Refcount(msg);
				}
			}else{
				/* group is changed */
				/* mark new member as new */
				new_mbr->status = NEW_MEMBER;

				/* Increment group id */
				grp->grp_id.index++;

				if( grp->num_local > 0 )
				{
					/* build a membership with all members, and vs set with all established members */
                                        msg = Message_new_message();
					num_bytes = G_build_memb_vs_buf( grp, msg, Mess_buf, CAUSED_BY_NETWORK );

					/* notify all non-new local members with that membership (caused by network) */
                                        mess_link = new( MESSAGE_LINK );
                                        Message_Buffer_to_Message_Fragments( msg, Mess_buf, num_bytes );
                                        mess_link->mess = msg;
                                        Obj_Inc_Refcount(mess_link->mess);

					needed = 0;
					for( i=0; i < grp->num_local; i++ )
					{
						/* if new member is local we do not notify it here */
						if( grp->mbox[i] == new_mbox ) continue;

						ses = Sess_get_session_index ( grp->mbox[i] );
						if( Is_memb_session( Sessions[ ses ].status ) )
							Sess_write( ses, mess_link, &needed );
					}
					if ( !needed ) Sess_dispose_message( mess_link );
                                        Message_Dec_Refcount(msg);

					/* notify new member if local */
					if( new_mbox != -1 )
					{
						/* build a membership with all members */
                                                joiner_msg = Message_new_message();
						num_bytes = G_build_memb_buf( grp, joiner_msg, Mess_buf );
						head_ptr = Message_get_message_header(joiner_msg);
						head_ptr->type |= CAUSED_BY_NETWORK ;
						/* build a self vs set */
						num_vs_ptr = &Mess_buf[ num_bytes ];
						num_bytes += sizeof( int32 );
                                                temp = 1;
                                                memcpy( num_vs_ptr, &temp, sizeof( int32 ) ); /* *num_vs_ptr = 1; */
						vs_ptr = (char *)&Mess_buf[ num_bytes ];
						memcpy( vs_ptr, new_mbr->private_name, MAX_GROUP_NAME );
						num_bytes += MAX_GROUP_NAME;
						head_ptr->data_len += ( sizeof(int32) + MAX_GROUP_NAME );
						mess_link = new( MESSAGE_LINK );
                                                Message_Buffer_to_Message_Fragments( joiner_msg, Mess_buf, num_bytes );
                                                mess_link->mess = joiner_msg;
                                                Obj_Inc_Refcount(mess_link->mess);

						needed = 0;
						ses = Sess_get_session_index ( new_mbox );
						if( Is_memb_session( Sessions[ ses ].status ) )
							Sess_write( ses, mess_link, &needed );
						if ( !needed ) Sess_dispose_message( mess_link );
                                                Message_Dec_Refcount(joiner_msg);
					}
		 			/* notify all local members a transitional membership */
					mess_link = G_build_trans_mess( grp );
					needed = 0;
					for( i=0; i < grp->num_local; i++ )
					{
						ses = Sess_get_session_index ( grp->mbox[i] );
						if( Is_memb_session( Sessions[ ses ].status ) )
							Sess_write( ses, mess_link, &needed );
					}
					if( !needed ) Sess_dispose_message( mess_link );
				}
				/* Mark new member as established */
				new_mbr->status = ESTABLISHED_MEMBER;
			}
		}else{
			/* coming from a partitioned daemon */
			/* mark new member as partitioned member */
			new_mbr->status = PARTITIONED_MEMBER;
			/* 
			 * (marking group as changed  - it might be already )
			 */
			if( !grp->changed ) grp->changed = 1;
			grp->grp_id.index++; 
			if( grp->num_local > 0 )
                        {
				/* build a membership with all members, and vs set with all non-partitioned members */
                                msg = Message_new_message();
				num_bytes = G_build_memb_vs_buf( grp, msg, Mess_buf, CAUSED_BY_NETWORK );

				/* notify all local members with that membership (caused by network) */
				mess_link = new( MESSAGE_LINK );
                                Message_Buffer_to_Message_Fragments( msg, Mess_buf, num_bytes );
                                mess_link->mess = msg;
                                Obj_Inc_Refcount(mess_link->mess);

				needed = 0;
				for( i=0; i < grp->num_local; i++ )
				{
					ses = Sess_get_session_index ( grp->mbox[i] );
					if( Is_memb_session( Sessions[ ses ].status ) )
						Sess_write( ses, mess_link, &needed );
				}
				if ( !needed ) Sess_dispose_message( mess_link );
                                Message_Dec_Refcount(msg);

		 		/* notify all local members a transitional membership */
				mess_link = G_build_trans_mess( grp );

				needed = 0;
				for( i=0; i < grp->num_local; i++ )
				{
					ses = Sess_get_session_index ( grp->mbox[i] );
					if( Is_memb_session( Sessions[ ses ].status ) )
						Sess_write( ses, mess_link, &needed );
				}
				if( !needed ) Sess_dispose_message( mess_link );
                        }
                }
		/* Compute the mask */
		for(i=0; i<4; i++)
		{
		    grp->grp_mask[i] = 0;
		}
		{
		  struct skiplistnode *iter;
		  member *memp;
		  /*
		    for( mbr= &grp->members; mbr->next != NULL; mbr=mbr->next )
		    {
		    p_ind1 = Conf_proc_by_id( mbr->next->proc_id, &p1 ); */
		  for( iter = sl_getlist( &grp->MembersList ),
			 memp=(member *)iter->data;
		       iter != NULL;
		       memp = (member *)sl_next( &grp->MembersList, &iter )) {
		    p_ind1 = Conf_proc_by_id( memp->proc_id, &p1 );
		    temp = 1;
		    for(i=0; i<p1.seg_index%32; i++)
		      {
			temp *= 2;
		      }
		    grp->grp_mask[p1.seg_index/32] |= temp;
		  }
		}
		Alarm(GROUPS, "G_handle_join: Mask for group %s set to %x %x %x %x\n", 
		      grp->name, grp->grp_mask[3], grp->grp_mask[2], grp->grp_mask[1], grp->grp_mask[0]);

		break;

	    case GGATHER:
		Alarm( EXIT, "G_handle_join in GGATHER\n");

		break;

	    case GGT:
		Alarm( EXIT, "G_handle_join in GGT\n");

		break;
	}
}

void	G_handle_leave( char *private_group_name, char *group_name )
{

	char		proc_name[MAX_PROC_NAME];
	char		private_name[MAX_PRIVATE_NAME+1];
	char		departing_private_group_name[MAX_GROUP_NAME];
	int		p_ind, p_ind1;
	proc		p, p1;
	group		*grp;
	member		*mbr;
	char		*num_vs_ptr; /* num members in vs set */
	char		*vs_ptr;     /* the virtual synchrony set */
	message_link	*mess_link;
	message_header	*head_ptr;
        message_obj     *msg;
	int		num_bytes;
	int		needed;
	int		ses;
	int		i, j;
	int32u          temp;

	Alarm( GROUPS, "G_handle_leave: %s leaves group %s\n", private_group_name, group_name );

	switch( Gstate )
	{
	    case GOP:
	    case GTRANS:

		if (Gstate == GOP) Alarm( GROUPS, "G_handle_leave in GOP\n");
		if (Gstate == GTRANS) Alarm( GROUPS, "G_handle_leave in GTRANS\n");

		/*
		 * if not already in group then ignore
		 * if this member is local, notify it and extract its mbox
		 * Extract this member from group
		 * if the group is unchanged (in GOP all groups are unchanged) then: 
		 *    Increment Grp_id
		 *    Notify all local members of a regular membership caused by leave
		 */
		G_private_to_names( private_group_name, private_name, proc_name );
		p_ind = Conf_proc_by_name( proc_name, &p );
		if( p_ind < 0 )
		{
			Alarm( PRINT, "G_handle_leave: illegal proc_name %s in private_group %s \n",
				proc_name, private_group_name );
			return;
		}
		grp = G_get_group( group_name );
		if( grp == NULL ) 
		{
			Alarm( PRINT, "G_handle_leave: group %s does not exist\n",
				group_name );
			return;
		}
		mbr = G_get_member( grp, private_group_name );
		if( mbr == NULL )
		{
			Alarm( PRINT, "G_handle_leave: member %s does not exist in group %s\n",
				private_group_name, group_name );
			return;
		}

		if( p.id == My.id )
		{
			/* notify this local member and extract its mbox from group */
                        msg = Message_new_message();
			head_ptr = Message_get_message_header(msg);
			head_ptr->type = CAUSED_BY_LEAVE;
			head_ptr->type = Set_endian( head_ptr->type );
			head_ptr->hint = Set_endian( 0 );
			memcpy( head_ptr->private_group_name, grp->name, MAX_GROUP_NAME );
			head_ptr->num_groups = 0;
			head_ptr->data_len = 0;

			/* create the mess_link */
			mess_link = new( MESSAGE_LINK );
                        /* NOTE: Mess_buf contents are NOT used here. We only examine "0" bytes of it
                         * We just need a valid pointer here to prevent faults */
                        Message_Buffer_to_Message_Fragments( msg, Mess_buf, 0);
			mess_link->mess = msg;
                        Obj_Inc_Refcount(mess_link->mess);
			/* notify member */
			needed = 0;
			ses = Sess_get_session( private_name );
			if( Is_memb_session( Sessions[ ses ].status ) )
				Sess_write( ses, mess_link, &needed );
			if( !needed ) Sess_dispose_message( mess_link );

			/* extract this mbox */
			for( i=0, j=0; i < grp->num_local; i++,j++ )
			{
				if( grp->mbox[i] == Sessions[ses].mbox ) j--;
				else grp->mbox[j] = grp->mbox[i];
			}
			grp->num_local--;
                        Message_Dec_Refcount(msg);
		}

		/* extract this member from group */
		memcpy( departing_private_group_name, mbr->private_name, MAX_GROUP_NAME );
		sl_remove( &grp->MembersList, mbr->private_name, dispose );
		grp->num_members--;
		if( grp->num_members == 0 )
		{
			/* discard this empty group */
                        sl_destruct ( &grp->MembersList, dispose);
		        sl_remove( &GroupsList, grp->name, dispose );
			Num_groups--;
			GlobalStatus.num_groups = Num_groups;
			return;
		}
		
		if( grp->changed )
		{
			if( Gstate != GTRANS ) Alarm( EXIT, "G_handle_leave: changed group in GOP\n");
			/*
			 * If the group is changed (in GTRANS) then there is no need
			 * to increment group id or to notify the local members.
			 * They will get a group membership after the state transfer
			 * terminates.
			 */
			return;
		}

		/* Increment group id */
		grp->grp_id.index++; 

		if( grp->num_local > 0 )
		{
			/* notify all local members */
                        msg = Message_new_message();
			num_bytes = G_build_memb_buf( grp, msg, Mess_buf );
			head_ptr = Message_get_message_header(msg);
			head_ptr->type |= CAUSED_BY_LEAVE ;

			/* notify all local members */
			num_vs_ptr = &Mess_buf[ num_bytes ];
			num_bytes += sizeof( int32 );
                        temp = 1;
                        memcpy( num_vs_ptr, &temp, sizeof( int32 ) ); /* *num_vs_ptr = 1; */

			vs_ptr = (char *)&Mess_buf[ num_bytes ];
		        memcpy( vs_ptr, departing_private_group_name, MAX_GROUP_NAME );
			num_bytes += MAX_GROUP_NAME;

			head_ptr->data_len += ( sizeof(int32) + MAX_GROUP_NAME );

			mess_link = new( MESSAGE_LINK );
                        Message_Buffer_to_Message_Fragments( msg, Mess_buf, num_bytes );
			mess_link->mess = msg;
                        Obj_Inc_Refcount(mess_link->mess);
			needed = 0;
			for( i=0; i < grp->num_local; i++ )
			{
				ses = Sess_get_session_index ( grp->mbox[i] );
				if( Is_memb_session( Sessions[ ses ].status ) )
					Sess_write( ses, mess_link, &needed );
			}
			if ( !needed ) Sess_dispose_message( mess_link );
                        Message_Dec_Refcount(msg);
		}
		/* Compute the mask */
		for(i=0; i<4; i++)
		{
		    grp->grp_mask[i] = 0;
		}
		{
		  struct skiplistnode *iter;
		  member *memp;
		  for( iter = sl_getlist( &grp->MembersList ),
			 memp=(member *)iter->data;
		       iter != NULL;
		       memp = (member *)sl_next( &grp->MembersList, &iter )) {
		    p_ind1 = Conf_proc_by_id( memp->proc_id, &p1 );
		    temp = 1;
		    for(i=0; i<p1.seg_index%32; i++)
		      {
			temp *= 2;
		      }
		    grp->grp_mask[p1.seg_index/32] |= temp;
		  }
		}

		Alarm(GROUPS, "G_handle_leave: Mask for group %s set to %x %x %x %x\n", 
		      grp->name, grp->grp_mask[3], grp->grp_mask[2], grp->grp_mask[1], grp->grp_mask[0]);
		break;

	    case GGATHER:
		Alarm( EXIT, "G_handle_leave in GGATHER\n");

		break;

	    case GGT:
		Alarm( EXIT, "G_handle_leave in GGT\n");

		break;
	}
}

void	G_handle_kill( char *private_group_name )
{
	char		proc_name[MAX_PROC_NAME];
	char		private_name[MAX_PRIVATE_NAME+1];
	char		departing_private_group_name[MAX_GROUP_NAME];
	int		p_ind, p_ind1;
	proc		p, p1;
	group		*grp, *nextgroup;
	member		*mbr;
	char		*num_vs_ptr; /* num members in vs set */
	char		*vs_ptr;     /* the virtual synchrony set */
	message_link	*mess_link;
	message_header	*head_ptr;
        message_obj     *msg;
	int		num_bytes;
	int		needed;
	int		ses = -1;       /* Fool compiler */
	int		i, j;
	int32u          temp;
	struct skiplistnode *giter;

	Alarm( GROUPS, "G_handle_kill: %s is killed\n", private_group_name );

	switch( Gstate )
	{
	    case GOP:
	    case GTRANS:

		if (Gstate == GOP) Alarm( GROUPS, "G_handle_kill in GOP\n");
		if (Gstate == GTRANS) Alarm( GROUPS, "G_handle_kill in GTRANS\n");

		/*
		 * for every group this guy is a member of
		 *    Extract this member from group
		 *    if the group is unchanged (in GOP all groups are unchanged) then: 
		 *        Increment Grp_id
		 *        Notify all local members of a regular membership caused by disconnet
		 */
		G_private_to_names( private_group_name, private_name, proc_name );
		p_ind = Conf_proc_by_name( proc_name, &p );
		if( p_ind < 0 )
		{
			Alarm( PRINT, "G_handle_kill: illegal proc_name %s in private_group %s \n",
				proc_name, private_group_name );
			return;
		}

		if( p.id == My.id ) ses = Sess_get_session( private_name );
	       
		giter = sl_getlist( &GroupsList );
		grp = (giter)?(group *)giter->data:NULL;
		for( ; grp != NULL ; grp = nextgroup)
		{
		  /* This is confusing... get the nextgroup so that if we
		     choose to remove it it doesn't screw up the iterator.
		     Then next time through use this "next" value */
                        nextgroup = sl_next( &GroupsList, &giter );
			mbr = G_get_member( grp, private_group_name );
			if( mbr == NULL ) continue; /* no such member in that group */

			/* Extract this member from group */
			if( p.id == My.id )
			{
				/* extract the mbox if local member */
				for( i=0, j=0; i < grp->num_local; i++, j++ )
				{
					if( grp->mbox[i] == Sessions[ses].mbox ) j--;
					else grp->mbox[j] = grp->mbox[i];
				}
				grp->num_local--;
			}
			memcpy( departing_private_group_name, mbr->private_name, MAX_GROUP_NAME );
			sl_remove( &grp->MembersList, mbr->private_name, dispose );
			grp->num_members--;
			if( grp->num_members == 0 )
			{
                                sl_destruct ( &grp->MembersList, dispose);
                                sl_remove( &GroupsList, grp->name, dispose );
				Num_groups--;
				GlobalStatus.num_groups = Num_groups;
				continue;
			}
		
			if( grp->changed )
			{
			    if( Gstate != GTRANS ) Alarm( EXIT, "G_handle_kill: changed group in GOP\n");
			    /*
			     * If the group is changed (in GTRANS) then there is no need
			     * to increment group id or to notify the local members.
			     * They will get a group membership after the state transfer
			     * terminates.
			     */
			    continue;
			}

			/* Increment group id */
			grp->grp_id.index++; 

		        /* Compute the mask */
		        for(i=0; i<4; i++)
		        {
		            grp->grp_mask[i] = 0;
		        }
			{
			  struct skiplistnode *iter;
			  member *memp;
			  for( iter = sl_getlist( &grp->MembersList ),
				 memp=(member *)iter->data;
			       iter != NULL;
			       memp = (member *)sl_next( &grp->MembersList, &iter )) {
			    p_ind1 = Conf_proc_by_id( memp->proc_id, &p1 );
		            temp = 1;
		            for(i=0; i<p1.seg_index%32; i++)
			      {
			        temp *= 2;
			      }
		            grp->grp_mask[p1.seg_index/32] |= temp;
			  }
			}

  		        Alarm(GROUPS, "G_handle_kill: Mask for group %s set to %x %x %x %x\n", 
		              grp->name, grp->grp_mask[3], grp->grp_mask[2], grp->grp_mask[1], grp->grp_mask[0]);

			if( grp->num_local > 0 )
			{
				/* notify all local members */
                                msg = Message_new_message();
				num_bytes = G_build_memb_buf( grp, msg, Mess_buf );
				head_ptr = Message_get_message_header(msg);

				head_ptr->type |= CAUSED_BY_DISCONNECT ;

				num_vs_ptr = &Mess_buf[ num_bytes ];
				num_bytes += sizeof( int32 );
                                temp = 1;
                                memcpy( num_vs_ptr, &temp, sizeof( int32 ) ); /* *num_vs_ptr = 1; */

				vs_ptr = (char *)&Mess_buf[ num_bytes ];
				memcpy( vs_ptr, departing_private_group_name, MAX_GROUP_NAME );
				num_bytes += MAX_GROUP_NAME;

				head_ptr->data_len += ( sizeof(int32) + MAX_GROUP_NAME );

				mess_link = new( MESSAGE_LINK );
                                Message_Buffer_to_Message_Fragments( msg, Mess_buf, num_bytes );
				mess_link->mess = msg;
                                Obj_Inc_Refcount(mess_link->mess);
				needed = 0;
				for( i=0; i < grp->num_local; i++ )
				{
                                        int temp_ses;

			 		temp_ses = Sess_get_session_index ( grp->mbox[i] );
					if( Is_memb_session( Sessions[ temp_ses ].status ) )
						Sess_write( temp_ses, mess_link, &needed );
				}
				if ( !needed ) Sess_dispose_message( mess_link );
                                Message_Dec_Refcount(msg);
			}
		}
		break;

	    case GGATHER:
		Alarm( EXIT, "G_handle_kill in GGATHER\n");

		break;

	    case GGT:
		Alarm( EXIT, "G_handle_kill in GGT\n");

		break;
	}
}

void	G_handle_groups( message_link *mess_link )
{
	char		*memb_id_ptr;
	membership_id	temp_memb_id;
        message_obj     *msg;
	message_header	*head_ptr;

	Alarm( GROUPS, "G_handle_groups: \n" );

	switch( Gstate )
	{
	    case GOP:

		Alarm( EXIT, "G_handle_groups in GOP\n");

		break;

	    case GTRANS:

		Alarm( EXIT, "G_handle_groups in GTRANS\n");

		break;

	    case GGATHER:
	    case GGT:

		if (Gstate == GGATHER) Alarm( GROUPS, "G_handle_groups in GGATHER\n");
		if (Gstate == GGT) Alarm( GROUPS, "G_handle_groups in GGT\n");

		msg = mess_link->mess;
                Obj_Inc_Refcount(msg);
		head_ptr = Message_get_message_header(msg);
		memb_id_ptr = Message_get_first_data_ptr(msg);
		memcpy( &temp_memb_id, memb_id_ptr, sizeof( membership_id ) );
		if( !Same_endian( head_ptr->type ) )
		{
			/* Flip membership id */
			temp_memb_id.proc_id = Flip_int32( temp_memb_id.proc_id );
			temp_memb_id.time    = Flip_int32( temp_memb_id.time    );
		}
		if( ! Memb_is_equal( temp_memb_id, Reg_memb_id ) )
		{
                        Alarm( GROUPS, 
                               "G_handle_groups: GROUPS message received from bad memb id proc %d, time %d, daemon %s.\n",
                               temp_memb_id.proc_id, temp_memb_id.time, head_ptr->private_group_name );
			Sess_dispose_message( mess_link );
                        Message_Dec_Refcount(msg);
			return;
		}

		mess_link->next = Gathered.next;
		Gathered.next = mess_link;
		Num_mess_gathered++;
                /* The last Groups message a daemon sends is AGREED. */
                if( Is_agreed_mess( head_ptr->type ) ) Num_daemons_gathered++;
                Alarm( GROUPS, "G_handle_groups: GROUPS message received from %s - msgs %d, daemons %d\n", 
                       head_ptr->private_group_name, Num_mess_gathered, Num_daemons_gathered );
		if( Num_daemons_gathered != Conf_num_procs( &Reg_memb ) )
                {
                        Message_Dec_Refcount(msg);
                        return;
                }
                Alarm( GROUPS, "G_handle_groups: Last GROUPS message received - msgs %d, daemons %d\n",
                       Num_mess_gathered, Num_daemons_gathered );
		/* Replace protocol queue */
		Prot_set_down_queue( NORMAL_DOWNQUEUE );

		/* lower events threshold */
		Session_threshold = LOW_PRIORITY;
		Sess_set_active_threshold();

		/* 
		 * Compute new groups membership and notify members of
		 * groups that have changed 
		 */
		G_compute_and_notify();

		if( Gstate == GGATHER )
		{
			Gstate = GOP;
			GlobalStatus.gstate = Gstate;
		}else{
			Gstate = GOP;
                        /* We do want to deliver a transitional signal to any
                         * groups that are going to get a CAUSED_BY_NETWORK
                         * after our Reg_memb is delivered. */
			G_handle_trans_memb( Trans_memb, Trans_memb_id );
		}
                
                Message_Dec_Refcount(msg);
		break;
	}
}

static	void		G_compute_and_notify()
{
	group		*grp, *new_grp, *orig_grp;
	member		*mbr;
	int		changed;
	int		ret;
	int		vs_bytes;
	char		*num_vs_ptr; /* num members in virtual-synchrony/failure-atomicity set */
        int32           num_vs;
	int		num_exist;
	struct worklist *indices[MAX_PROCS_RING];
	int		num_bytes;
	message_link	*mess_link;
	message_header	*head_ptr;
        message_obj     *msg;
	int		needed;
	char		proc_name[MAX_PROC_NAME];
	char		private_name[MAX_PRIVATE_NAME+1];
	int		ses;
	int		i;
	Skiplist        work;
	
	Alarm( GROUPS, "G_compute_and_notify:\n");
	/* Compute groups structure in Work from gathered messages and clear messages */

	sl_init(&work);
	sl_set_compare(&work, G_work_groups_comp, G_work_groups_keycomp);

	for( i=0; i < Num_mess_gathered; i++ )
	{
	        struct worklist *tp;
		tp = (struct worklist *)Mem_alloc(sizeof(struct worklist));
		tp->groups=NULL;
		mess_link = Gathered.next;
		Gathered.next = mess_link->next;
		ret = G_mess_to_groups( mess_link, tp->name, tp );
                if( ret < 0 )
                        Alarm( EXIT, "G_compute_and_notify: G_mess_to_groups errored %d\n",
                               ret );
                Sess_dispose_message( mess_link );
		if ( !sl_insert(&work, tp) )
                {
                        Alarm(EXIT, "G_compute_and_notify: Failed to insert worklist (%s) into work\n", tp->name);
                }
	}
	/* 
	 * for every sorted group name:
	 * 	Join the member lists to one list in Groups with a vs set.
	 *	If the group has changed (*) 
	 *		Set new gid 
	 *		notify all local members: non-new get vs set, new get self.
	 *		cancel new mark.
	 *	dispose of this group is all of Work.
	 *
	 * Note: group has changed unless all of this hold:
	 *	- everyone has the same gid
	 *	- gid is not changed (-1)
	 */
	
	for( num_exist = G_smallest_group_indices( &work, indices ) ;
	     num_exist > 0 ; 
	     num_exist = G_smallest_group_indices( &work, indices ) )
        {
	    struct skiplistnode *top_iter;
	    group *this_group;
	    /* prepare vs set */
	    vs_bytes = 0;
	    num_vs_ptr = &Temp_buf[0];
	    vs_bytes+= sizeof( int32 );
            num_vs = 0;
	    
	    changed = 0;
	    orig_grp = NULL;
	    assert( NULL != (this_group = (group *)(sl_getlist(indices[0]->groups)->data)) );
            orig_grp = sl_find( &GroupsList, this_group->name, &top_iter);
	    	    
	    if( orig_grp == NULL )
            {
			new_grp = new( GROUP );
			memset( new_grp->name, 0, MAX_GROUP_NAME );
			strcpy( new_grp->name, this_group->name );
	
			new_grp->grp_id = this_group->grp_id;

			new_grp->num_members = 0;
			sl_init( &new_grp->MembersList );
			sl_set_compare( &new_grp->MembersList,
					G_member_recordcompare,
					G_member_keycompare);
			new_grp->num_local = 0;

			sl_insert( &GroupsList, new_grp );
			Num_groups++;
			GlobalStatus.num_groups = Num_groups;
			orig_grp = new_grp;
            }else{
		  /* free members but keep local mbox */
		  sl_remove_all( &orig_grp->MembersList, dispose );
		  orig_grp->num_members = 0;
            }

            for( i=0 ; i < num_exist; i++ )
            {
		  group *currentgroup;
		  currentgroup =
		    (group *)sl_getlist(indices[i]->groups)->data;
		  if( G_id_is_equal( orig_grp->grp_id, currentgroup->grp_id ) )
                  {
			  struct skiplistnode *iter;
			  Skiplist *currentmembers;
			  currentmembers = &currentgroup->MembersList;
			  iter = sl_getlist(currentmembers);
                          assert(iter != NULL); /* memberlist in Groups message should never be empty */
			  for( mbr = iter->data;
			       mbr != NULL;
			       mbr = sl_next(currentmembers, &iter))
                          {
                                        /* add this non-new member to vs */
                                        memcpy( &Temp_buf[vs_bytes], mbr->private_name, MAX_GROUP_NAME );
                                        vs_bytes += MAX_GROUP_NAME;
                                        num_vs++;	
                          }
                  }else{
			  /* not the same grp_id */
                          changed = 1;
                  }
                  /* in any way, mbr points here to the last member */
                  /* chain these members */

		  sl_concat(&orig_grp->MembersList,
			    &currentgroup->MembersList);
		  orig_grp->num_members = orig_grp->MembersList.size;

			/* free this Work group */
                  sl_destruct(&currentgroup->MembersList, dispose);
		  sl_remove(indices[i]->groups, currentgroup, dispose);
            }

            memcpy( num_vs_ptr, &num_vs, sizeof( int32 ) ); /* *num_vs_ptr = current count; */

            /* now our orig_grp is almost updated */
            grp = orig_grp;
            
            if( grp->changed ) changed = 1;
            
            if( !changed ) continue;

            /* the group has changed */
            Alarm( GROUPS, "G_compute_and_notify: completed group %s.\n", grp->name );
            Alarm( DEBUG,  "G_compute_and_notify: changing group id from: " );
            G_print_group_id( grp->grp_id );
            grp->grp_id.memb_id = Reg_memb_id;
            grp->grp_id.index   = 1;
            grp->changed        = 0;
            Alarm( DEBUG, " to: " );
            G_print_group_id( grp->grp_id );
            Alarm( DEBUG, "\n" );
            
            if( grp->num_local > 0 )
            {
                    struct skiplistnode *iter;
                    msg = Message_new_message();
                    num_bytes = G_build_memb_buf( grp, msg, Mess_buf );
                    head_ptr = Message_get_message_header(msg);
                    
                    head_ptr->type |= CAUSED_BY_NETWORK ;
                    
                    /* notify non-new local members */
                    memcpy( &Mess_buf[num_bytes], Temp_buf, vs_bytes );
                    head_ptr->data_len += vs_bytes;
                    
                    mess_link = new( MESSAGE_LINK );
                    Message_Buffer_to_Message_Fragments( msg, Mess_buf, num_bytes + vs_bytes);
                    mess_link->mess = msg;
                    Obj_Inc_Refcount(mess_link->mess);
                    needed = 0;
                    iter = sl_getlist(&grp->MembersList);
                    for( mbr = iter->data;
			     mbr != NULL;
			     mbr = sl_next(&grp->MembersList, &iter))
                    {
                            if( Is_new_member( mbr->status ) ) continue;
                            if( mbr->proc_id != My.id ) continue;
                            
                            G_private_to_names( mbr->private_name, private_name, proc_name );
                            ses = Sess_get_session( private_name );
                            if( ses < 0 ) Alarm( EXIT, "G_compute_and_notify: no session for %s\n", private_name);
                            
                            if( Is_memb_session( Sessions[ ses ].status ) )
                                    Sess_write( ses, mess_link, &needed );
                    }
                    if( !needed ) Sess_dispose_message( mess_link );
                    Message_Dec_Refcount(msg);
            }
	}
        Num_mess_gathered    = 0;
        Num_daemons_gathered = 0;

        /* We're going back to GOP... destroy our groups messages. */
        G_empty_groups_bufs();

        /* Finish freeing the memory in our worklists */
	{
		struct worklist     *worklist;
		struct skiplistnode *iter;
                
		iter = sl_getlist(&work);       
		worklist = (iter)?iter->data:NULL;      
                
		while( worklist != NULL ) {     
			assert( worklist->groups->size == 0 );  
			dispose( worklist->groups );    
			worklist = sl_next(&work, &iter);       
		}       
	}       
        
        sl_destruct( &work, dispose );
        
	G_print();
}

static	int		G_smallest_group_indices( Skiplist *work, struct worklist *indices[] )
{
	/* 
	 * this function searches the Work structure for the smallest
	 * alphabetically ordered group name. It stores  
	 * all of the occurences of that group in the indices array, 
	 * and returns the number of occurences.
	 */
	int	num_exist;
	int	cmp;
	struct worklist *worklist;
	Skiplist *groups;
	struct skiplistnode *iter;
	
	iter = sl_getlist(work);
	worklist = (iter)?iter->data:NULL;
	num_exist = 0;
	if(!worklist) {
                return 0;
	}
        /* set indices[0] to first worklist with any groups */
        do {
                if ( worklist->groups->size == 0 )
                {
                        worklist = sl_next(work, &iter);
                } else {
                        indices[0] = worklist;
                        num_exist = 1;
                        break;
                }
        } while ( worklist != NULL );

	if(!worklist) {
                /* All worklist groups are empty (no daemons have any alive groups) */
                return 0;
	}
        
        worklist = sl_next( work, &iter );
        /* Check rest of worklists for any with earlier groups or the same first group as indices[0] */
        while ( worklist != NULL )
	{
                group *first, *current;
                
                groups = worklist->groups;
		if( groups->size == 0 ) 
                {
                        worklist = sl_next(work, &iter);
                        continue;
                }
		first = (group *)(sl_getlist(indices[0]->groups)->data);
		current = (group *)(sl_getlist(groups)->data);
		cmp = strcmp( first->name, current->name );
		if( cmp == 0 )
                {
                        indices[num_exist] = worklist;
                        num_exist++;
                }else if( cmp > 0 ){
                        indices[0] = worklist;
                        num_exist = 1;
                }
		worklist = sl_next(work, &iter);
	}
	return( num_exist );
}

static	int		G_id_is_equal( group_id g1, group_id g2 )
{
	if( g1.index == g2.index && Memb_is_equal( g1.memb_id, g2.memb_id ) )
		return( 1 );
	else	return( 0 );
}

static	group		*G_get_group( char *group_name )
{
	struct skiplistnode *iter;

	return sl_find( &GroupsList, group_name, &iter );
}

static	member		*G_get_member( group *grp, char *private_group_name )
{
	struct skiplistnode *iter;

	return sl_find( &grp->MembersList, private_group_name, &iter );
}

static	message_link	*G_build_trans_mess( group *grp )
{
	/* 
	 * This routine builds a ready-to-be-sent transitional message signal 
	 * to the members of the process group grp 
	 */

	message_link	*mess_link;
	scatter	        *scat;
	message_header	*head_ptr;
	char		*gid_ptr;

	mess_link = new( MESSAGE_LINK );
	mess_link->mess = Message_create_message(TRANSITION_MESS, grp->name);

	scat = Message_get_data_scatter(mess_link->mess);
	scat->elements[0].len = Message_get_data_header_size() +
				sizeof( group_id );
	head_ptr = Message_get_message_header(mess_link->mess);
	gid_ptr = Message_get_first_data_ptr(mess_link->mess );

	head_ptr->data_len = sizeof( group_id );
	memcpy( gid_ptr, &grp->grp_id, sizeof(group_id) );

	return( mess_link );
}

static	int		G_build_memb_buf( group *grp, message_obj *msg, char buf[])
{
	int		num_bytes;
	message_header	*head_ptr;
	char		*gid_ptr;
	member		*mbr;
	struct skiplistnode *iter;
	char		*memb_ptr;


	head_ptr = Message_get_message_header(msg);
	head_ptr->type = REG_MEMB_MESS;
	head_ptr->type = Set_endian( head_ptr->type );
	head_ptr->hint = Set_endian( 0 );
	memcpy( head_ptr->private_group_name, grp->name, MAX_GROUP_NAME );
	head_ptr->num_groups = grp->num_members;
	head_ptr->data_len = sizeof( group_id );

        num_bytes = 0;
	iter = sl_getlist( &grp->MembersList );
	mbr = (iter)?(member *)iter->data:NULL;
	for( ; mbr != NULL ; mbr = sl_next( &grp->MembersList, &iter ) )
	{
		memb_ptr = (char *)&buf[num_bytes];
		num_bytes += MAX_GROUP_NAME;
		memcpy( memb_ptr, mbr->private_name, MAX_GROUP_NAME );
	}

	gid_ptr = &buf[num_bytes];
	num_bytes += sizeof( group_id );
	memcpy( gid_ptr, &grp->grp_id, sizeof(group_id) );

	return( num_bytes );
}


static	int		G_build_memb_vs_buf( group *grp, message_obj *msg, char buf[], int32 caused )
{
/* 
 * This routine builds the memb buffer message, including a virtual synchrony
 * (failure atomicity) part with a set which contains only the established members
 * in the group membership.
 * 
 * Note that in leave and disconnect we provide the member that left or
 * got disconnected in the vs_set. Therefore, caused will always be CAUSED_BY_NETWORK.
 */

	int		num_bytes;
	message_header	*head_ptr;
	char		*num_vs_ptr; /* num members in virtual-synchrony/failure-atomicity set */
	struct skiplistnode *iter;
        member          *mbr;
	char		*membs_ptr;
        int32           num_vs;

	num_bytes = G_build_memb_buf( grp, msg, buf);
	head_ptr = Message_get_message_header(msg);

	head_ptr->type = head_ptr->type | caused;

	num_vs_ptr = &buf[num_bytes];
	num_bytes += sizeof( int32 );
        head_ptr->data_len += sizeof( int32 );
        num_vs = 0;

        iter = sl_getlist( &grp->MembersList );
	mbr = (iter)?(member *)iter->data:NULL;
	for( ; mbr != NULL ; mbr = sl_next( &grp->MembersList, &iter ) )
        {
		if( Is_established_member( mbr->status ) )
		{
			membs_ptr = (char *)&buf[num_bytes];
			memcpy( membs_ptr, mbr->private_name, MAX_GROUP_NAME );
			num_vs++ ;
			num_bytes += MAX_GROUP_NAME;
			head_ptr->data_len += MAX_GROUP_NAME;
		}
	}
        memcpy( num_vs_ptr, &num_vs, sizeof( int32 ) ); /* *num_vs_ptr = total count; */

	return( num_bytes );
}

static	void		G_stamp_groups_buf( char buf[] )
{
	char	*memb_id_ptr;
	memb_id_ptr = buf;
	memcpy( memb_id_ptr, &Reg_memb_id, sizeof( membership_id ) );
}

/* This function used to be called G_refresh_groups_msg. */
static  void            G_build_groups_msg_hdr( message_obj *msg, int groups_bytes )
{
	message_header	*head_ptr;

	head_ptr = Message_get_message_header(msg);
	head_ptr->type = GROUPS_MESS;
	head_ptr->type = Set_endian( head_ptr->type );
	head_ptr->hint = Set_endian( 0 );
	memset(head_ptr->private_group_name, 0, MAX_GROUP_NAME);
	strcpy( head_ptr->private_group_name, My.name );
	head_ptr->num_groups = 0;
	head_ptr->data_len = groups_bytes;
}

/* This function guarantees that each group's data appears in only one buffer in
 * a sequence, and that the sorted order is preserved from the GroupsList. */
static	int		G_build_groups_buf(char buf[], struct skiplistnode **iter_ptr)
{
	int		num_bytes;
	char		*memb_id_ptr;
	group		*grp;
	char		*gid_ptr;
	member		*mbr;
	struct skiplistnode *giter, *iter;
	char		*num_memb_ptr;
        int16           num_memb;
	char		*memb_ptr;
        int     size_for_this_group;

	num_bytes = 0;

	memb_id_ptr = &buf[num_bytes];
	num_bytes += sizeof( membership_id );
	memcpy( memb_id_ptr, &Reg_memb_id, sizeof( membership_id ) );

        giter = (*iter_ptr) ? (*iter_ptr) : (sl_getlist( &GroupsList ));

	grp = (giter)?(group *)giter->data:NULL;
	for( ; grp != NULL ; grp = sl_next( &GroupsList, &giter ) )
	{
		if( grp->num_local == 0 ) continue;
		
                size_for_this_group = MAX_GROUP_NAME + sizeof(group_id) + sizeof(int16) +
                        (grp->num_local * MAX_GROUP_NAME);
                /* This requires that the number of local group members be limited. */
                if( size_for_this_group > GROUPS_BUF_SIZE - num_bytes )  break;

		memcpy( &buf[num_bytes], grp->name, MAX_GROUP_NAME );
		num_bytes += MAX_GROUP_NAME;
		
		gid_ptr = &buf[num_bytes];
		num_bytes += sizeof( group_id );
		memcpy( gid_ptr, &grp->grp_id, sizeof(group_id) );

		num_memb_ptr = &buf[num_bytes];
		num_bytes += sizeof( int16 );
		num_memb  = 0;

		iter = sl_getlist( &grp->MembersList );
		mbr = (iter)?(member *)iter->data:NULL;
		for( ; mbr != NULL ; mbr = sl_next( &grp->MembersList, &iter ) )
		{
			/* collect local members */
			if( mbr->proc_id != My.id ) continue;
			memb_ptr = (char *)&buf[num_bytes];
			num_bytes += MAX_GROUP_NAME;
			memcpy( memb_ptr, mbr->private_name, MAX_GROUP_NAME );
			num_memb++;
		}
                memcpy(num_memb_ptr, &num_memb, sizeof( int16 ) );
                if( num_memb != grp->num_local )
                        Alarm( EXIT, "G_build_groups_buf: group %s has %d %d members\n",
                               grp->name, num_memb, grp->num_local );
                
        }
        *iter_ptr = giter; 
	return( num_bytes );
}

static	int		G_mess_to_groups( message_link *mess_link, char *name, struct worklist *work )
{
	/* the function returns 0 for success or -1 if an error occured */

        message_obj     *msg;
        scatter         *scat;
	message_header	*head_ptr;
	proc		p;
	int		num_bytes, total_bytes;
	group		*grp;
	char		*gid_ptr;
	member		*mbr;
	char		*num_memb_ptr;
        int16           num_memb;
	int		i;

	total_bytes = 0;
	msg = mess_link->mess;
        scat = Message_get_data_scatter(msg);
	for( i=0; i < scat->num_elements ; i++ )
	{
		memcpy( &Temp_buf[total_bytes], scat->elements[i].buf, scat->elements[i].len );
		total_bytes += scat->elements[i].len;
	}

	num_bytes = 0;
	head_ptr = Message_get_message_header(msg);
        num_bytes += Message_get_data_header_size();
	if (0 > Conf_proc_by_name( head_ptr->private_group_name , &p ) )
        {
                Alarm( PRINT, "G_mess_to_groups: Groups message from someone (%s) not in conf\n", head_ptr->private_group_name);
                return( -1 );
        }
	work->groups = (Skiplist *)Mem_alloc(sizeof(Skiplist));
	sl_init(work->groups);
	sl_set_compare(work->groups,
		       G_compare,
		       G_compare);
	
	memcpy( name, head_ptr->private_group_name, MAX_GROUP_NAME );

	num_bytes += sizeof( membership_id );

	for( ; num_bytes < total_bytes; )
	{
		/* creating a new group */
		grp = new( GROUP );

		memcpy( grp->name, &Temp_buf[num_bytes], MAX_GROUP_NAME );
		num_bytes += MAX_GROUP_NAME;

		sl_append( work->groups, grp );
		sl_init( &grp->MembersList );
		sl_set_compare( &grp->MembersList,
				G_member_recordcompare,
				G_member_keycompare);

		gid_ptr = &Temp_buf[num_bytes];
		num_bytes += sizeof( group_id );
		memcpy( &grp->grp_id, gid_ptr, sizeof(group_id) );

		num_memb_ptr = &Temp_buf[num_bytes];
		num_bytes += sizeof( int16 );
                memcpy( &num_memb, num_memb_ptr, sizeof( int16 ) );

		if( !Same_endian( head_ptr->type ) )
		{
			/* Flip group id */
			grp->grp_id.memb_id.proc_id = Flip_int32( grp->grp_id.memb_id.proc_id );
			grp->grp_id.memb_id.time    = Flip_int32( grp->grp_id.memb_id.time );
			grp->grp_id.index    	    = Flip_int32( grp->grp_id.index );

			/* flip other parts of the message */
			num_memb  = Flip_int16( num_memb  );
		}
		/* creating members */
		for( i=0; i < num_memb; i++ )
		{
			mbr = new( MEMBER );
			
			mbr->proc_id = p.id;
			mbr->status = ESTABLISHED_MEMBER;
			memcpy( mbr->private_name, &Temp_buf[num_bytes], MAX_GROUP_NAME );
			num_bytes += MAX_GROUP_NAME;

			sl_append( &grp->MembersList, mbr );
		}

		grp->num_members = num_memb;
                memcpy( num_memb_ptr, &num_memb, sizeof( int16 ) );
	}
	return( 0 );
}

int	G_analize_groups( int num_groups, char target_groups[][MAX_GROUP_NAME], int target_sessions[] )
{
static	mailbox mbox[MAX_SESSIONS];
static	mailbox	current[MAX_SESSIONS];
static	mailbox *current_ptr;
	int	num_mbox;
	int	num_mbox_pre;
	int	num_current;
	group	*grp;
	member 	*mbr;
	struct skiplistnode *iter;
	char	proc_name[MAX_PROC_NAME];
	char	private_name[MAX_PRIVATE_NAME+1];
	int	found;
	int	ses;
	int	ret;
	int	i, j, k;
	
	/* get the mbox */
	num_mbox = 0;
	for( i=0; i < num_groups; i++ )
	{
		if( target_groups[i][0] == '#' )
		{
			/* private group */
			ret = G_private_to_names( target_groups[i], private_name, proc_name );

			/* Illegal group */
			if( ret < 0 ) continue;

			/* this private group is not local */
			if( strcmp( My.name, proc_name ) != 0 ) continue;

			ses = Sess_get_session( private_name );

			/* we have no such session */
			if( ses < 0 ) continue;

			current[0] = Sessions[ ses ].mbox;
			current_ptr = current;
			num_current = 1;
		}else{
			/* regular group */
			grp = G_get_group( target_groups[i] );
			if( grp == NULL ) continue;
			if( Gstate == GOP )
			{
				current_ptr = grp->mbox;
				num_current = grp->num_local;
			}else if( Gstate == GTRANS ){
				current_ptr = current;
				num_current = 0;
				iter = sl_getlist( &grp->MembersList );
				mbr = (iter)?(member *)iter->data:NULL;
				for( ; mbr != NULL ;
				     mbr = sl_next( &grp->MembersList, &iter ) )
				{
					if( mbr->proc_id == My.id && !Is_new_member( mbr->status ) )
					{
						G_private_to_names( mbr->private_name, private_name, proc_name );
						ses = Sess_get_session( private_name );
						if( ses < 0 ) Alarm( EXIT, 
							"G_analize_groups: ses is %d private_name is %s\n", 
							ses, private_name );
						current[ num_current ] = Sessions[ ses ].mbox;
						num_current++;
					}
				}
			}else {
                                num_current = 0; /* fool compiler warnings */
                                Alarm( EXIT, "G_analize_groups: Gstate is %d\n", Gstate );
                        }
		}
		num_mbox_pre = num_mbox;
		for( j=0; j < num_current; j++ )
		{
			found = 0;
			for( k=0; k < num_mbox_pre; k++ )
			{
				if( mbox[k] == current_ptr[j] )
				{
					found = 1;
					break;
				}
			}
			if( !found )
			{
				mbox[num_mbox] = current_ptr[j];
				num_mbox++;
			}
		}
	}

	/* convert mbox to sessions */
	for( i=0; i < num_mbox; i++ ) target_sessions[i] = Sess_get_session_index ( mbox[ i ] );
	return( num_mbox );
}


void G_set_mask( int num_groups, char target_groups[][MAX_GROUP_NAME], int32u *grp_mask )
{
	group	*grp;
	char	proc_name[MAX_PROC_NAME];
	char	private_name[MAX_PRIVATE_NAME+1];
	int	ret;
	int	i, j;
	proc    p;
	int32u  temp;


	for(i=0; i<4; i++)
        {
	   grp_mask[i] = 0;
	}

	for( i=0; i < num_groups; i++ )
	{
		if( target_groups[i][0] == '#' )
		{
			/* private group */
			ret = G_private_to_names( target_groups[i], private_name, proc_name );

			/* Illegal group */
			if( ret < 0 ) continue;

		        Conf_proc_by_name( proc_name, &p ); 
		        temp = 1;
		        for(j=0; j<p.seg_index%32; j++)
		        {
			    temp *= 2;
		        }
		        grp_mask[p.seg_index/32] |= temp;

		}else{
			/* regular group */
			grp = G_get_group( target_groups[i] );
			if( grp == NULL )
			{
			    p = Conf_my();
		            temp = 1;
		            for(j=0; j<p.seg_index%32; j++)
		            {
			        temp *= 2;
		            }
		            grp_mask[p.seg_index/32] |= temp;
			}
			else if(( Gstate == GOP )||(Gstate == GTRANS))
			{
		            for(j=0; j<4; j++)
		            {
			        grp_mask[j] |= grp->grp_mask[j]; 
		            }
			    p = Conf_my();
		            temp = 1;
		            for(j=0; j<p.seg_index%32; j++)
		            {
			        temp *= 2;
		            }
		            grp_mask[p.seg_index/32] |= temp;

			}else Alarm( EXIT, "G_set_mask: Gstate is %d\n", Gstate );
		}
	}
}



int	G_private_to_names( char *private_group_name, char *private_name, char *proc_name )
{
	char	name[MAX_GROUP_NAME];
	char	*pn, *prvn;
	unsigned int	priv_name_len, proc_name_len;
	int	i,legal_private_name;

        memcpy(name, private_group_name, MAX_GROUP_NAME );
        proc_name_len = 0; /* gcc not smart enough to detect that proc_name_len is always initialized when used */

        pn = strchr(&name[1], '#');
        if (pn != NULL)
        {
                pn[0] = '\0';
                proc_name_len = strlen( &(pn[1]));
        }
        priv_name_len = strlen( &(name[1]));
        if ( (pn == NULL) || (name[0] != '#' ) || 
             ( priv_name_len > MAX_PRIVATE_NAME) ||
             ( priv_name_len < 1 ) ||
             ( proc_name_len >= MAX_PROC_NAME ) ||
             ( proc_name_len < 1 ) )
        {
                Alarm( GROUPS, "G_private_to_names: Illegal private_group_name (priv, proc)\n");
                return( ILLEGAL_GROUP );
        }
        /* start strings at actual beginning */
        pn++;
        pn[proc_name_len] = '\0';
        prvn = &name[1];
        legal_private_name = 1;
        for( i=0; i < priv_name_len; i++ )
                if( prvn[i] <= '#' ||
                    prvn[i] >  '~' ) 
                {
                        legal_private_name = 0;
                        prvn[i] = '.';
                }
        for( i=0; i < proc_name_len; i++ )
                if( pn[i] <= '#' ||
                    pn[i] >  '~' ) 
                {
                        legal_private_name = 0;
                        pn[i] = '.';
                }
        if( !legal_private_name )
        {
                Alarm( GROUPS, "G_private_to_names: Illegal private_group_name characters (%s) (%s)\n", prvn, pn );
                return( ILLEGAL_GROUP );
        }
        /* copy name components including null termination */
        memcpy( private_name, prvn, priv_name_len + 1 );
        memcpy( proc_name, pn, proc_name_len + 1 );
	return( 1 );
}

static	void	G_print()
{
	group	*grp;
	member	*mbr;
	struct skiplistnode *giter, *iter;
	int	i, j;

	printf("++++++++++++++++++++++\n");
	printf("Num of groups: %d\n", Num_groups );
	giter = sl_getlist( &GroupsList );
	grp = (giter)?(group *)giter->data:NULL;
	for( i=0; grp != NULL ; i++, grp = sl_next( &GroupsList, &giter ) )
	{
		printf("[%d] group %s with %d members:\n", i+1, grp->name, grp->num_members );
		iter = sl_getlist( &grp->MembersList );
		mbr = (iter)?(member *)iter->data:NULL;
		for( j=0; mbr != NULL ;
		     j++, mbr = sl_next( &grp->MembersList, &iter ) )
		{
			printf("\t[%d] %s\n", j+1, mbr->private_name );
		}
		printf("----------------------\n");
	}
}

static  void  G_empty_groups_bufs()
{
        groups_buf_link *next;

        for( ; Groups_bufs;  Groups_bufs = next )
        {
                next = Groups_bufs->next;
                dispose( Groups_bufs );
        }
        return;
}

int     G_get_num_local( char *group_name )
{
        group *grp = G_get_group( group_name );
        if( grp == NULL ) return 0;
        return grp->num_local;
}

static  void  G_print_group_id( group_id g )
{
        Alarm( DEBUG, "{Proc ID: %d, Time: %d, Index: %d}",
               g.memb_id.proc_id, g.memb_id.time, g.index );
}
