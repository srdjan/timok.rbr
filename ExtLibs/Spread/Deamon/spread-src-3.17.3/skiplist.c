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


#include <stdio.h>
#include <stdlib.h>
#include <assert.h>

#include "skiplist_p.h"
#include "alarm.h"

#ifndef MIN
#define MIN(a,b) ((a<b)?(a):(b))
#endif

static int get_b_rand() {
  static int ph=32; /* More bits than we will ever use */
  static unsigned long randseq;
  if(ph > 31) { /* Num bits in return of lrand48() */
    ph=0;
    randseq = get_rand();
  }
  ph++;
  return ((randseq & (1 << (ph-1))) >> (ph-1));
}

void sli_init(Skiplist *sl) {
  sl->compare = (SkiplistComparator)NULL;
  sl->comparek = (SkiplistComparator)NULL;
  sl->height=0;
  sl->preheight=0;
  sl->size=0;
  sl->top = NULL;
   sl->bottom = NULL;
  sl->index = NULL;
}

static int indexing_comp(const void *a, const void *b)
{
  void *ak = (void *) (((Skiplist *) a)->compare);
  void *bk = (void *) (((Skiplist *) b)->compare);
  assert(a);
  assert(b);
  if(ak<bk)
    return -1;
  if(ak>bk)
    return 1;
  return 0;
}
static int indexing_compk(const void *ak, const void *b)
{
  void *bk = (void *) (((Skiplist *) b)->compare);
  assert(b);
  if(ak<bk)
    return -1;
  if(ak>bk)
    return 1;
  return 0;
}

void sl_init(Skiplist *sl) {
  sli_init(sl);
  sl->index = (Skiplist *)malloc(sizeof(Skiplist));
  sli_init(sl->index);
  sl_set_compare(sl->index, indexing_comp, indexing_compk);
}

void sl_set_compare(Skiplist *sl,
		    SkiplistComparator comp,
		    SkiplistComparator compk) {
  if(sl->compare && sl->comparek) {
    sl_add_index(sl, comp, compk);
  } else {
    sl->compare = comp;
    sl->comparek = compk;
  }
}

void sl_add_index(Skiplist *sl,
		  SkiplistComparator comp,
		  SkiplistComparator compk) {
  struct skiplistnode *m;
  Skiplist *ni;
  int icount=0;
  Alarm(SKIPLIST, "Adding index to %p\n", sl);
  sl_find(sl->index, (void *)comp, &m);
  if(m) return; /* Index already there! */
  ni = (Skiplist *)malloc(sizeof(Skiplist));
  sli_init(ni);
  sl_set_compare(ni, comp, compk);
  /* Build the new index... This can be expensive! */
  m = sl_insert(sl->index, ni);
  while(m->prev) m=m->prev, icount++;
  for(m=sl_getlist(sl); m; sl_next(sl, &m)) {
    int j=icount-1;
    struct skiplistnode *nsln;
    nsln = sl_insert(ni, m->data);
    /* skip from main index down list */
    while(j>0) m=m->nextindex, j--;
    /* insert this node in the indexlist after m */
    nsln->nextindex = m->nextindex;
    if(m->nextindex) m->nextindex->previndex = nsln;
    nsln->previndex = m;
    m->nextindex = nsln;
  } 
}

struct skiplistnode *sl_getlist(Skiplist *sl) {
  if(!sl->bottom) return NULL;
  return sl->bottom->next;
}

void *sl_find(Skiplist *sl,
	      void *data,
	      struct skiplistnode **iter) {
  void *ret;
  if(!sl->compare) return 0;
  ret = sl_find_compare(sl, data, iter, sl->compare);
  return ret;
}  
void *sl_find_compare(Skiplist *sli,
		      void *data,
		      struct skiplistnode **iter,
		      SkiplistComparator comp) {
  struct skiplistnode *m = NULL;
  Skiplist *sl;
  if(comp==sli->compare || !sli->index) {
    sl = sli;
  } else {
    sl_find(sli->index, (void *)comp, &m);
    assert(m);
    sl=m->data;
  }
  sli_find_compare(sl, data, iter, sl->comparek);
  return (*iter)?((*iter)->data):(*iter);
}
int sli_find_compare(Skiplist *sl,
		     void *data,
		     struct skiplistnode **ret,
		     SkiplistComparator comp) {
  struct skiplistnode *m = NULL;
  int count=0;
  m = sl->top;
  while(m) {
    int compared = 1;
    if(m->next) compared=comp(data, m->next->data);
    if(compared == 0) {
#ifdef SL_DEBUG
      Alarm(SKIPLIST, "Looking -- found in %d steps\n", count);
#endif
      m=m->next;
      while(m->down) m=m->down;
      *ret = m;
      return count;
    }
    if((m->next == NULL) || (compared<0))
      m = m->down, count++;
    else
      m = m->next, count++;
  }
#ifdef SL_DEBUG
  Alarm(SKIPLIST, "Looking -- not found in %d steps\n", count);
#endif
  *ret = NULL;
  return count;
}
void *sl_next(Skiplist *sl, struct skiplistnode **iter) {
  if(!*iter) return NULL;
  *iter = (*iter)->next;
  return (*iter)?((*iter)->data):NULL;
}
void *sl_previous(Skiplist *sl, struct skiplistnode **iter) {
  if(!*iter) return NULL;
  *iter = (*iter)->prev;
  return (*iter)?((*iter)->data):NULL;
}
struct skiplistnode *sl_insert(Skiplist *sl,
			       void *data) {
  if(!sl->compare) return 0;
  return sl_insert_compare(sl, data, sl->compare);
}

struct skiplistnode *sl_insert_compare(Skiplist *sl,
				       void *data,
				       SkiplistComparator comp) {
  struct skiplistnode *m, *p, *tmp, *ret, **stack;
  int nh=1, ch, stacki;
  ret = NULL;
/*sl_print_struct(sl, "BI: ");*/
  if(!sl->top) {
    sl->height = 1;
    sl->topend = sl->bottomend = sl->top = sl->bottom = 
      (struct skiplistnode *)malloc(sizeof(struct skiplistnode));
    assert(sl->top);
    sl->top->next = sl->top->data = sl->top->prev =
	sl->top->up = sl->top->down = 
	sl->top->nextindex = sl->top->previndex = NULL;
    sl->top->sl = sl;
  }
  if(sl->preheight) {
    while(nh < sl->preheight && get_b_rand()) nh++;
  } else {
    while(nh <= sl->height && get_b_rand()) nh++;
  }
  /* Now we have the new height at which we wish to insert our new node */
  /* Let us make sure that our tree is a least that tall (grow if necessary)*/
  for(;sl->height<nh;sl->height++) {
    sl->top->up =
      (struct skiplistnode *)malloc(sizeof(struct skiplistnode));
    assert(sl->top->up);
    sl->top->up->down = sl->top;
    sl->top = sl->topend = sl->top->up;
    sl->top->prev = sl->top->next = sl->top->nextindex =
      sl->top->previndex = sl->top->up = NULL;
    sl->top->data = NULL;
    sl->top->sl = sl;
  }
  ch = sl->height;
  /* Find the node (or node after which we would insert) */
  /* Keep a stack to pop back through for insertion */
  m = sl->top;
  stack = (struct skiplistnode **)malloc(sizeof(struct skiplistnode *)*(nh));
  stacki=0;
  while(m) {
    int compared=-1;
    if(m->next) compared=comp(data, m->next->data);
    if(compared == 0) {
      free(stack);
      return 0;
    }
    if((m->next == NULL) || (compared<0)) {
            /* FIXME: This if ch<=nh test looks unnecessary. ch==nh at beginning of while(m)
             */
      if(ch<=nh) {
	/* push on stack */
	stack[stacki++] = m;
      }
      m = m->down;
      ch--;
    } else {
      m = m->next;
    }
  }
  /* Pop the stack and insert nodes */
  p = tmp = NULL;
  for(;stacki>0;stacki--) {
    m = stack[stacki-1];
    tmp = (struct skiplistnode *)malloc(sizeof(struct skiplistnode));
    tmp->next = m->next;
    if(m->next) m->next->prev=tmp;
    tmp->prev = m;
    tmp->up = NULL;
    tmp->nextindex = tmp->previndex = NULL;
    tmp->down = p;
    if(p) p->up=tmp;
    tmp->data = data;
    tmp->sl = sl;
    m->next = tmp;
    /* This sets ret to the bottom-most node we are inserting */
    if(!p) 
    {
            ret=tmp;
            sl->size++;
    }
    p = tmp;
  }
  free(stack);
  if(tmp && (tmp->prev == sl->topend)) {
    /* The last element on the top row is the new inserted one */
    sl->topend = tmp;
  }
  if(ret && (ret->prev == sl->bottomend)) {
    /* The last element on the bottom row is the new inserted one */
    sl->bottomend = ret;
  }
  if(sl->index != NULL) {
    /* this is a external insertion, we must insert into each index as well */
    struct skiplistnode *p, *ni, *li;
    li=ret;
    for(p = sl_getlist(sl->index); p; sl_next(sl->index, &p)) {
      ni = sl_insert((Skiplist *)p->data, ret->data);
      assert(ni);
      Alarm(SKIPLIST, "Adding %p to index %p\n", ret->data, p->data);
      li->nextindex = ni;
      ni->previndex = li;
      li = ni;
    }
  } 
  /* JRS: move size increment above to where node is inserted
    else {
    sl->size++;
  }
  */
/*sl_print_struct(sl, "AI: ");*/
  return ret;
}
struct skiplistnode *sl_append(Skiplist *sl, void *data) {
  return sl_insert(sl, data);
}
#if 0
struct skiplistnode *sl_append(Skiplist *sl, void *data) {
  int nh=1, ch, compared;
  struct skiplistnode *lastnode, *nodeago;
  if(sl->bottomend != sl->bottom) {
    compared=sl->compare(data, sl->bottomend->prev->data);
    /* If it doesn't belong at the end, then fail */
    if(compared<=0) return NULL;
  }
  if(sl->preheight) {
    while(nh < sl->preheight && get_b_rand()) nh++;
  } else {
    while(nh <= sl->height && get_b_rand()) nh++;
  }
  /* Now we have the new hieght at which we wish to insert our new node */
  /* Let us make sure that our tree is a least that tall (grow if necessary)*/
  lastnode = sl->bottomend;
  nodeago = NULL;

  if(!lastnode) return sl_insert(sl, data);

  for(;sl->height<nh;sl->height++) {
    assert(sl->top);
    sl->top->up =
      (struct skiplistnode *)malloc(sizeof(struct skiplistnode));
    sl->top->up->down = sl->top;
    sl->top = sl->topend = sl->top->up;
    sl->top->prev = sl->top->next = sl->top->nextindex =
      sl->top->previndex = NULL;
    sl->top->data = NULL;
    sl->top->sl = sl;
  }
  ch = sl->height;
  while(nh) {
    struct skiplistnode *anode;
    anode =
      (struct skiplistnode *)malloc(sizeof(struct skiplistnode));
    anode->next = lastnode;
    anode->prev = lastnode->prev;
    anode->up = NULL;
    anode->down = nodeago;
    /* If this the bottom, we are appending, so bottomend should change */
    if(!nodeago) sl->bottomend = anode;
    if(lastnode->prev) {
      if(lastnode == sl->bottom)
	sl->bottom = anode;
      else if (lastnode == sl->top)
	sl->top = anode;
    }
    nodeago = anode;
    lastnode = lastnode->up;
    nh--;
  }
  sl->size++;
  return(nodeago);
}
#endif
Skiplist *sl_concat(Skiplist *sl1, Skiplist *sl2) {
  /* Check integrity! */
  Skiplist temp;
  struct skiplistnode *b2;
  if(sl1->bottomend == NULL || sl1->bottomend->prev == NULL) {
    sl_remove_all(sl1, free);
    temp = *sl1;
    *sl1 = *sl2;
    *sl2 = temp;
    /* swap them so that sl2 can be freed normally upon return. */
    return sl1;
  }
  if(sl2->bottom == NULL || sl2->bottom->next == NULL) {
    sl_remove_all(sl2, free);
    return sl1;
  }
  b2 = sl_getlist(sl2);
  while(b2) {
    sl_insert(sl1, b2->data);
    sl_next(sl2, &b2);
  }
  sl_remove_all(sl2, NULL);
  return sl1;
}
#if 0
Skiplist *sl_concat(Skiplist *sl1, Skiplist *sl2) {
  /* Check integrity! */
  int compared, eheight;
  Skiplist temp;
  struct skiplistnode *lbottom, *lbottomend, *b1, *e1, *b2, *e2;
  if(sl1->bottomend == NULL || sl1->bottomend->prev == NULL) {
    sl_remove_all(sl1, free);
    temp = *sl1;
    *sl1 = *sl2;
    *sl2 = temp;
    /* swap them so that sl2 can be freed normally upon return. */
    return sl1;
  }
  if(sl2->bottom == NULL || sl2->bottom->next == NULL) {
    sl_remove_all(sl2, free);
    return sl1;
  }
  compared = sl1->compare(sl1->bottomend->prev->data, sl2->bottom->data);
  /* If it doesn't belong at the end, then fail */
  if(compared<=0) return NULL;
  
  /* OK now append sl2 onto sl1 */
  lbottom = lbottomend = NULL;
  eheight = MIN(sl1->height, sl2->height);
  b1 = sl1->bottom; e1 = sl1->bottomend;
  b2 = sl2->bottom; e2 = sl2->bottomend;
  while(eheight) {    
    e1->prev->next = b2;
    b2->prev = e1->prev->next;
    e2->prev->next = e1;
    e1->prev = e2->prev;
    e2->prev = NULL;
    b2 = e2;
    b1->down = lbottom;
    e1->down = lbottomend;
    if(lbottom) lbottom->up = b1;
    if(lbottomend) lbottomend->up = e1;
    
    lbottom = b1;
    lbottomend = e1;
  }
  /* Take the top of the longer one (if it is sl2) and make it sl1's */
  if(sl2->height > sl1->height) {
    b1->up = b2->up;
    e1->up = e2->up;
    b1->up->down = b1;
    e1->up->down = e1;
    sl1->height = sl2->height;
    sl1->top = sl2->top;
    sl1->topend = sl2->topend;
  }

  /* move the top pointer to here if it isn't there already */
  sl2->top = sl2->topend = b2;
  sl2->top->up = NULL; /* If it isn't already */
  sl1->size += sl2->size;
  sl_remove_all(sl2, free);
  return sl1;
}
#endif
int sl_remove(Skiplist *sl,
	      void *data, FreeFunc myfree) {
  if(!sl->compare) return 0;
  return sl_remove_compare(sl, data, myfree, sl->comparek);
}
void sl_print_struct(Skiplist *sl, char *prefix, char *(*printdata)(void *)) {
  struct skiplistnode *p, *q;
  Alarm(SKIPLIST, "Skiplist Structure (height: %d)\n", sl->height);
  p = sl->bottom;
  while(p) {
    q = p;
    Alarm(SKIPLIST, prefix);
    while(q) {
      Alarm(SKIPLIST, "%6s ", printdata(q->data));
      q=q->up;
    }
    Alarm(SKIPLIST, "\n");
    p=p->next;
  }
}
int sli_remove(Skiplist *sl, struct skiplistnode *m, FreeFunc myfree) {
  struct skiplistnode *p;
  if(!m) return 0;
  if(m->nextindex) sli_remove(m->nextindex->sl, m->nextindex, NULL);
  else sl->size--;
  /*sl_print_struct(sl, "BR:");*/
  while(m->up) m=m->up;
  while(m) {
    p=m;
    p->prev->next = p->next; /* take me out of the list */
    if(p->next) p->next->prev = p->prev; /* take me out of the list */
    m=m->down;
    /* This only frees the actual data in the bottom one */
    if(!m && myfree && p->data) myfree(p->data);
    free(p);
  }
  while(sl->top && sl->top->next == NULL) {
    /* While the row is empty and we are not on the bottom row */
    p = sl->top;
    sl->top = sl->top->down; /* Move top down one */
    if(sl->top) sl->top->up = NULL;      /* Make it think its the top */
    free(p);
    sl->height--;
  }
  if(!sl->top) sl->bottom = NULL;
  assert(sl->height>=0);
  /*sl_print_struct(sl, "AR: ");*/
  return sl->height;
}
int sl_remove_compare(Skiplist *sli,
		      void *data,
		      FreeFunc myfree, SkiplistComparator comp) {
  struct skiplistnode *m;
  Skiplist *sl;
  if(comp==sli->comparek || !sli->index) {
    sl = sli;
  } else {
    sl_find(sli->index, (void *)comp, &m);
    assert(m);
    sl=m->data;
  }
  sli_find_compare(sl, data, &m, comp);
  if (!m) return( 0 );
  while(m->previndex) m=m->previndex;
  return sli_remove(sl, m, myfree);
}
void sl_remove_all(Skiplist *sl, FreeFunc myfree) {
  /* This must remove even the place holder nodes (bottom though top)
     because we specify in the API that one can free the Skiplist after
     making this call without memory leaks */
  struct skiplistnode *m, *p, *u;
  m=sl->bottom;
  while(m) {
    p = m->next;
    if(myfree && p && p->data) myfree(p->data);
    while(m) {
      u = m->up;
      free(m);
      m=u;
    }
    m = p;
  }
  sl->top = sl->bottom = NULL;
  sl->height = 0;
  sl->size = 0;
}
void sli_destruct_free(Skiplist *sl, FreeFunc myfree) {
  sl_remove_all(sl, NULL);
  free(sl);
}
void sl_destruct(Skiplist *sl, FreeFunc myfree) {
  if(sl->index) {
    sl_remove_all(sl->index, (FreeFunc)sli_destruct_free);
    free(sl->index);
  }
  sl_remove_all(sl, myfree);
}

