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


#ifndef _SKIPLIST_P_H
#define _SKIPLIST_P_H

/* This is a skiplist implementation to be used for abstract structures
   within the Spread multicast and group communication toolkit

   This portion written by -- Theo Schlossnagle <jesus@cnds.jhu.eu>
*/

/* This is the function type that must be implemented per object type
   that is used in a skiplist for comparisons to maintain order */
typedef int (*SkiplistComparator)(void *, void *);
typedef void (*FreeFunc)(void *);

struct skiplistnode;

typedef struct _iskiplist {
  SkiplistComparator compare;
  SkiplistComparator comparek;
  int height;
  int preheight;
  int size;
  struct skiplistnode *top;
  struct skiplistnode *bottom;
  /* These two are needed for appending */
  struct skiplistnode *topend;
  struct skiplistnode *bottomend;
  struct _iskiplist *index;
} Skiplist;

struct skiplistnode {
  void *data;
  struct skiplistnode *next;
  struct skiplistnode *prev;
  struct skiplistnode *down;
  struct skiplistnode *up;
  struct skiplistnode *previndex;
  struct skiplistnode *nextindex;
  Skiplist *sl;
};


void sl_init(Skiplist *sl);
void sl_set_compare(Skiplist *sl, SkiplistComparator,
		    SkiplistComparator);
void sl_add_index(Skiplist *sl, SkiplistComparator,
		  SkiplistComparator);
struct skiplistnode *sl_getlist(Skiplist *sl);
void *sl_find_compare(Skiplist *sl, void *data, struct skiplistnode **iter,
		      SkiplistComparator func);
void *sl_find(Skiplist *sl, void *data, struct skiplistnode **iter);
void *sl_next(Skiplist *sl, struct skiplistnode **);
void *sl_previous(Skiplist *sl, struct skiplistnode **);

struct skiplistnode *sl_insert_compare(Skiplist *sl,
				       void *data, SkiplistComparator comp);
struct skiplistnode *sl_insert(Skiplist *sl, void *data);
int sl_remove_compare(Skiplist *sl, void *data,
		      FreeFunc myfree, SkiplistComparator comp);
int sl_remove(Skiplist *sl, void *data, FreeFunc myfree);
int sli_remove(Skiplist *sl, struct skiplistnode *m, FreeFunc myfree);
void sl_remove_all(Skiplist *sl, FreeFunc myfree);

int sli_find_compare(Skiplist *sl,
		    void *data,
		    struct skiplistnode **ret,
		    SkiplistComparator comp);

#endif
