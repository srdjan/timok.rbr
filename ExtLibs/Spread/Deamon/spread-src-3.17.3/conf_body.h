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


/* 
 *
 * $Id: conf_body.h,v 1.3 2004/03/05 00:32:46 jonathan Exp $
 */

#ifndef INC_CONF_BODY
#define INC_CONF_BODY

#include "arch.h"
#include "skiplist.h"
#include "configuration.h"
#include "spread_params.h"

int		yyparse();

#undef  ext
#ifndef ext_conf_body
#define ext extern
#else
#define ext
#endif

ext     configuration	Config;
ext     int		Num_procs;
ext     FILE		*yyin;
ext     Skiplist        ConfProcsbyname;
ext     Skiplist        ConfProcsbyid;
ext	proc           	Config_procs[MAX_PROCS_RING];
ext     int       LinkWeights[MAX_SEGMENTS][MAX_SEGMENTS];

/* For network protocols used in Spread 4 */
#define HOP_PROT        1
#define RING_PROT       2
#define TCP_PROT        3
#define MAX_PROTOCOLS   3

#define YYSTYPE YYSTYPE

#ifndef	ARCH_PC_WIN95

#include <netinet/in.h>

#else 	/* ARCH_PC_WIN95 */

#include <winsock.h>

#endif	/* ARCH_PC_WIN95 */

typedef union {
  bool boolean;
  int32 mask;
  int number;
  int cost[MAX_SEGMENTS];
  struct {
    struct in_addr addr;
    unsigned short port;
  } ip;
  char *string;
} YYSTYPE;
extern YYSTYPE yylval;
extern int yysemanticerr;

#endif /* INC_CONF_BODY */
