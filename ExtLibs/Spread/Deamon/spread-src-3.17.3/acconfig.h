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

/* $Id: acconfig.h,v 1.3 2004/03/05 00:32:46 jonathan Exp $ */

#ifndef _CONFIG_H
#define _CONFIG_H

/* Comments for config.h.in file */
/* Generated automatically from acconfig.h by autoheader. */
/* Please make your changes there */

@TOP@

/* Define if your system's struct sockaddr_un has a sun_len member */
#undef HAVE_SUN_LEN_IN_SOCKADDR_UN

/* Define if you system's inet_ntoa is busted (e.g. Irix gcc issue) */
#undef BROKEN_INET_NTOA

/* Define if your system defines sys_errlist[] */
#undef HAVE_SYS_ERRLIST

/* Define if your system defines sys_nerr */
#undef HAVE_SYS_NERR

/* Define if your snprintf is busted */
#undef BROKEN_SNPRINTF

#undef HAVE_CYGWIN

/* Define if you are on NeXT */
#undef HAVE_NEXT

/* Define if you want to install preformatted manpages.*/
#undef MANTYPE

/* struct timeval */
#undef HAVE_STRUCT_TIMEVAL

/* Define if libc defines __progname */
#undef HAVE___PROGNAME

/* Define if your libraries define daemon() */
#undef HAVE_DAEMON

/* Defined if in_systm.h needs to be included with netinet/ip.h (HPUX - <sigh/>) */
#undef NEED_IN_SYSTM_H

/* Data types */
#undef ARCH_PC_WIN95
#undef ARCH_SCATTER_NONE
#undef ARCH_SCATTER_CONTROL
#undef ARCH_SCATTER_ACCRIGHTS
#undef HAVE_SOCKOPT_LEN_T
#undef HAVE_STRUCT_TIMEZONE
#undef HAVE_U_INT
#undef HAVE_INTXX_T
#undef HAVE_U_INTXX_T
#undef HAVE_UINTXX_T
#undef HAVE_INT64_T
#undef HAVE_U_INT64_T
#undef HAVE_SOCKLEN_T
#undef HAVE_SIZE_T
#undef HAVE_SSIZE_T
#undef HAVE_CLOCK_T
#undef HAVE_MODE_T
#undef HAVE_PID_T
#undef HAVE_SA_FAMILY_T
#undef HAVE_STRUCT_SOCKADDR_STORAGE
#undef HAVE_STRUCT_ADDRINFO
#undef HAVE_STRUCT_IN6_ADDR
#undef HAVE_STRUCT_SOCKADDR_IN6

/* Fields in struct sockaddr_storage */
#undef HAVE_SS_FAMILY_IN_SS
#undef HAVE___SS_FAMILY_IN_SS

/* Specify location of spread.pid */
#undef _PATH_SPREAD_PIDDIR

/* Specify location of spread.conf and other configuration files */
#undef SPREAD_ETCDIR

@BOTTOM@

/* ******************* Shouldn't need to edit below this line ************** */

#include "defines.h"

#endif /* _CONFIG_H */
