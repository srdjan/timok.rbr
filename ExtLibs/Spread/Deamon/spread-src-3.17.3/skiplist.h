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


#ifndef _SKIPLIST_H
#define _SKIPLIST_H

/* This is a skiplist implementation to be used for abstract structures
   within the Spread multicast and group communication toolkit

   This portion written by -- Theo Schlossnagle <jesus@cnds.jhu.eu>
*/

/* This is the function type that must be implemented per object type
   that is used in a skiplist for comparisons to maintain order */
typedef int (*SkiplistComparator)(void *, void *);
typedef void (*FreeFunc)(void *);

struct skiplistnode {
  void *data;
  void *private_use[7];
};

typedef struct {
  SkiplistComparator compare;
  SkiplistComparator comparek;
  const int height;
  int preheight;
  const int size;
  void *private_use[5];
} Skiplist;

/* set up the internals for this skiplist */
void sl_init(Skiplist *sl);

/* set a compare function to be used by the skiplist */
/* Pass a skiplist in, a comparator for (record, record) and a 
   comparator for (key, record) in the order */

void sl_set_compare(Skiplist *, SkiplistComparator,
		    SkiplistComparator);
void sl_add_index(Skiplist *sl, SkiplistComparator,
		  SkiplistComparator);

/* Returns a pointer to the first node in the list
   DO NOT EDIT THIS LIST!  It is maintained internally and should not
   be toyed with */
struct skiplistnode *sl_getlist(Skiplist *sl);

/* Find returns NULL on failure and a point to the data on success */
/* sl is the skiplist in which you are looking
   data the key you are looking for
   iter is an iterator that is filled out with the found node
     it is then passed into next and previous to iterate through the list */
void *sl_find(Skiplist *sl, void *data, struct skiplistnode **iter);
void *sl_find_compare(Skiplist *sl, void *data, struct skiplistnode **iter,
		      SkiplistComparator comp);
void *sl_next(Skiplist *sl, struct skiplistnode **iter);
void *sl_previous(Skiplist *sl, struct skiplistnode **iter);

/* Insert returns 0 on failure and a pointer to the skiplistnode on success */
/* sl is the skiplist in which you are inserting
   data is a record (not necessarily the key) */
struct skiplistnode *sl_insert(Skiplist *sl, void *data);

/* Append and concatenate are special.. They are used to effeciently create
   a skiplist from presorted data. You MUST be sure that there are not
   multiple indices and that the comparator is the same.
   Both return NULL on error and the skiplistnode inserted and the new
   Skiplist on success, respectively */
struct skiplistnode *sl_append(Skiplist *sl, void *data);
/* The return value will match dest on success and appending will be empty and
   thus freeable without leakage */
Skiplist *sl_concat(Skiplist *dest, Skiplist *appending);

/* Remove returns 0 on failure and the current tree height on success */ 
/* sl is the skiplist from which you are removing
   data is the key for the node you wish to remove */
int sl_remove(Skiplist *sl, void *data, FreeFunc myfree);
int sl_remove_compare(Skiplist *sl, void *data, FreeFunc myfree,
		      SkiplistComparator comp);

/* removes all nodes in a skiplist, the list can still be used */
/* sl is the skiplist from which you are removing */
void sl_remove_all(Skiplist *sl, FreeFunc myfree);

/* removes all nodes in a skiplist, you can free the skiplist itself
   without memory leaks after calling this function.  After calling
   this function, the list cannot be safely used, it must be freed */
void sl_destruct(Skiplist *sl, FreeFunc myfree);


#endif
