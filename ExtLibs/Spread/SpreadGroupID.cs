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
	/**
	 * A GroupID represents a particular group membership view at a particular time in the history of the group.
	 * A GroupID can be retrieved from a membership message using {@link MembershipInfo#getGroupID()}.
	 * To check if two GroupID's represent the same group, use {@link GroupID#equals(Object)}:
	 * <p><blockquote><pre>
	 * if(thisID.equals(thatID) == true)
	 *     System.out.println("Equal group ID's.");
	 * </pre></blockquote><p>
	 */
	public class GroupID {
		// The group ID.
		private int[] id = new int[3];
	
		// Constructor.
		internal GroupID(int id0, int id1, int id2) {
			// Setup the ID.
			////////////////
			id[0] = id0;
			id[1] = id1;
			id[2] = id2;
		}
	
		// Returns true if the object represents the same group ID.
		/**
		 * Returns true if the two GroupID's represent the same group membership view at the same point in time
		 * in the history of the group.
		 * 
		 * @param  object  a GroupID to compare against
		 * @return  true if object is a GroupID object and the two GroupID's are the same
		 */
		public override bool Equals(Object o) {
			// Check if it's the correct class.
			if(o == null || GetType() != o.GetType()) {
				return false;
			}
		
			// Check if the ID's are the same.
			GroupID other = (GroupID)o;
			int[] otherID = other.ID;
			for(int i = 0 ; i < 3 ; i++) {
				if(id[i] != otherID[i]) {
					// The ID's are different.
					return false;
				}
			}
		
			// No differences, the ID's are the same.
			return true;
		}
	
		// Get the ID.
		internal int[] ID {
			get {
				return id;
			}
		}

		// Returns the hash code of the group ID.
		/**
		* Returns the hash code of the group ID.
		*
		* @return int the hash code
		*/
		public override int GetHashCode() {
			return id.GetHashCode();
		}

		// Converts the group ID to a string.
		/**
		 * Converts the GroupID to a string.
		 * 
		 * @return  the string form of this GroupID
		 */
		public override string ToString() {
			return id[0] + " " + id[1] + " " + id[2];
		}
	}

}