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
 *  Copyright (C) 1993-2001 Spread Concepts LLC <spread@spreadconcepts.com>
 *
 *  All Rights Reserved.
 *
 * Major Contributor(s):
 * ---------------
 *    Dan Schoenblum   dansch@cnds.jhu.edu - Java Interface Developer.
 *    John Schultz     jschultz@cnds.jhu.edu - contribution to process group membership.
 *    Theo Schlossnagle theos@cnds.jhu.edu - Perl library and Skiplists.
 *
 */

using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Threading;

namespace Timok.SpreadLib 
{
	public class SpreadMessage {
		public const int UNRELIABLE_MESS        = 0x00000001;
		public const int RELIABLE_MESS          = 0x00000002;
		public const int FIFO_MESS              = 0x00000004;
		public const int CAUSAL_MESS            = 0x00000008;
		public const int AGREED_MESS            = 0x00000010;
		public const int SAFE_MESS              = 0x00000020;
		public const int REGULAR_MESS           = 0x0000003f;
		public const int SELF_DISCARD           = 0x00000040;
		public const int REG_MEMB_MESS          = 0x00001000;
		public const int TRANSITION_MESS        = 0x00002000;
		public const int CAUSED_BY_JOIN         = 0x00000100;
		public const int CAUSED_BY_LEAVE        = 0x00000200;
		public const int CAUSED_BY_DISCONNECT   = 0x00000400;
		public const int CAUSED_BY_NETWORK      = 0x00000800;
		public const int MEMBERSHIP_MESS        = 0x00003f00;

		public const int REJECT_MESS			   = 0x00400000;

		public const int JOIN_MESS            = 0x00010000;
		public const int LEAVE_MESS           = 0x00020000;
		public const int KILL_MESS            = 0x00040000;
		public const int GROUPS_MESS          = 0x00080000;
		private const int CONTENT_DATA           = 1;
		private const int CONTENT_OBJECT         = 2;
		private const int CONTENT_DIGEST         = 3;

		public int ServiceType = RELIABLE_MESS;
		protected int content = CONTENT_DATA;
		public short Type;
		public bool EndianMismatch;
		public byte[] Data = new byte[0]; 
		internal ArrayList groups = new ArrayList();
		internal MembershipInfo membershipInfo;

		public MembershipInfo MembershipInfo { get { return membershipInfo;	} }
		public SpreadGroup[] Groups { get {	return groups.ToArray(typeof(SpreadGroup)) as SpreadGroup[]; } }
		public SpreadGroup Sender;
		public void AddGroup(SpreadGroup group) {
			groups.Add(group);
		}
		public void AddGroup(string group) {
			SpreadGroup spreadGroup = new SpreadGroup(null, group);
			AddGroup(spreadGroup);
		}
		public bool IsRegular {	get {	return ( ((ServiceType & REGULAR_MESS) != 0) && ( (ServiceType & REJECT_MESS) == 0) );	}	}
		public bool IsReject { get { return ((ServiceType & REJECT_MESS) != 0); } }
		public bool IsMembership { get { return ( ((ServiceType & MEMBERSHIP_MESS) != 0) && ((ServiceType & REJECT_MESS) == 0) );	}	}
		public bool IsUnreliable {
			get {	return ((ServiceType & UNRELIABLE_MESS) != 0); }
			set {
				if (value) {
					ServiceType &= ~REGULAR_MESS;
					ServiceType |= UNRELIABLE_MESS;
				} else IsReliable = true;
			}
		}
		public bool IsReliable {
			get {	return ((ServiceType & RELIABLE_MESS) != 0); }
			set {
				ServiceType &= ~REGULAR_MESS;
				ServiceType |= RELIABLE_MESS;
			}
		}
		public bool IsFifo {
			get {	return ((ServiceType & FIFO_MESS) != 0); }
			set {
				if (value) {
					ServiceType &= ~REGULAR_MESS;
					ServiceType |= FIFO_MESS;
				} else IsReliable = true;
			}
		}
		public bool IsCausal {
			get {	return ((ServiceType & CAUSAL_MESS) != 0); }
			set {
				if (value) {
					ServiceType &= ~REGULAR_MESS;
					ServiceType |= CAUSAL_MESS;
				} else IsReliable = true;
			}
		}
		public bool IsAgreed {
			get {	return ((ServiceType & AGREED_MESS) != 0); }
			set {
				if (value) {
					ServiceType &= ~REGULAR_MESS;
					ServiceType |= AGREED_MESS;
				} else IsReliable = true;
			}
		}
		public bool IsSafe {
			get { return ((ServiceType & SAFE_MESS) != 0); }
			set {
				if (value) {
					ServiceType &= ~REGULAR_MESS;
					ServiceType |= SAFE_MESS;
				} else IsReliable = true;
			}
		}
		public bool IsSelfDiscard {
			get { return ((ServiceType & SELF_DISCARD) != 0); }
			set {
				if (value)
					ServiceType |= SELF_DISCARD;
				else
					ServiceType &= ~SELF_DISCARD;
			}
		}
	}
}