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

namespace Timok.SpreadLib {
	public class MembershipInfo {
		// The message's serviceType.
		private int serviceType;
	
		// The group.
		private SpreadGroup group;
	
		// The group's id.
		private GroupID groupID;
	
		// The group's members.
		private SpreadGroup[] members;
	
		// The group(s) that joined/left/disconeccted/stayed.
		private SpreadGroup[] groups;
	
		// Constructor.
		internal MembershipInfo(SpreadConnection connection,SpreadMessage message, bool daemonEndianMismatch) {		
			// Set local variables.
			this.serviceType = message.ServiceType;
			this.group = message.Sender;
			this.members = message.Groups;
		
			// Is this a regular membership message.
			if(IsRegularMembership) {
				// Extract the groupID.
				int dataIndex = 0;
				int[] id = new int[3];
				id[0] = SpreadConnection.toInt(message.Data, dataIndex);
				dataIndex += 4;
				id[1] = SpreadConnection.toInt(message.Data, dataIndex);
				dataIndex += 4;
				id[2] = SpreadConnection.toInt(message.Data, dataIndex);
				dataIndex += 4;
				// Endian-flip?
				if(daemonEndianMismatch) {
					id[0] = SpreadConnection.flip(id[0]);
					id[1] = SpreadConnection.flip(id[1]);
					id[2] = SpreadConnection.flip(id[2]);
				}
			
				// Create the group ID.
				this.groupID = new GroupID(id[0], id[1], id[2]);
			
				// Get the number of groups.
				int numGroups = SpreadConnection.toInt(message.Data, dataIndex);
				if(daemonEndianMismatch) {
					numGroups = SpreadConnection.flip(numGroups);
				}
				dataIndex += 4;
			
				// Get the groups.
				this.groups = new SpreadGroup[numGroups];
				for(int i = 0 ; i < numGroups ; i++) {
					this.groups[i] = connection.toGroup(message.Data, dataIndex);
					dataIndex += SpreadConnection.MAX_GROUP_NAME;
				}
			}
		}
	
		// Checking the service-type.
		/**
			 * Check if this is a regular membership message.
			 * If true, {@link MembershipInfo#getGroup()}, {@link MembershipInfo#getGroupID()},
			 * and {@link MembershipInfo#getMembers()} are valid.
			 * One of {@link MembershipInfo#getJoined()}, {@link MembershipInfo#getLeft()}, 
			 * {@link MembershipInfo#getDisconnected()}, or {@link MembershipInfo#getStayed()} are valid
			 * depending on the type of message.
			 * 
			 * @return  true if this is a regular membership message
			 * @see  MembershipInfo#getGroup()
			 * @see  MembershipInfo#getGroupID()
			 * @see  MembershipInfo#getMembers()
			 * @see  MembershipInfo#getJoined()
			 * @see  MembershipInfo#getLeft()
			 * @see  MembershipInfo#getDisconnected()
			 * @see  MembershipInfo#getStayed()
			 */
		public bool IsRegularMembership {
			get {
				return ((serviceType & SpreadMessage.REG_MEMB_MESS) != 0);
			}
		}
	
		/**
			 * Check if this is a transition message.
			 * If true, {@link MembershipInfo#getGroup()} is the only valid get*() function.
			 * 
			 * @return  true if this is a transition message
			 * @see  MembershipInfo#getGroup()
			 */
		public bool IsTransition {
			get {
				return ((serviceType & SpreadMessage.TRANSITION_MESS) != 0);
			}
		}
	
		/**
			 * Check if this message was caused by a join.
			 * If true, {@link MembershipInfo#getJoined()} can be used to get the group member who joined.
			 * 
			 * @return  true if this message was caused by a join
			 * @see  MembershipInfo#getJoined()
			 */
		public bool IsCausedByJoin {
			get {
				return ((serviceType & SpreadMessage.CAUSED_BY_JOIN) != 0);
			}
		}
	
		/**
			 * Check if this message was caused by a leave.
			 * If true, {@link MembershipInfo#getLeft()} can be used to get the group member who left.
			 * 
			 * @return  true if this message was caused by a leave
			 * @see  MembershipInfo#getLeft()
			 */
		public bool IsCausedByLeave {
			get {
				return ((serviceType & SpreadMessage.CAUSED_BY_LEAVE) != 0);
			}
		}
	
		/**
			 * Check if this message was caused by a disconnect.
			 * If true, {@link MembershipInfo#getDisconnected()} can be used to get the group member who left.
			 * 
			 * @return  true if this message was caused by a disconnect
			 * @see  MembershipInfo#getDisconnected()
			 */
		public bool IsCausedByDisconnect {
			get {
				return ((serviceType & SpreadMessage.CAUSED_BY_DISCONNECT) != 0);
			}
		}
	
		/**
			 * Check if this message was caused by a network partition.
			 * If true, {@link MembershipInfo#getStayed()} can be used to get the members who are still in the group.
			 * 
			 * @return  true if this message was caused by a network partition
			 * @see  MembershipInfo#getStayed()
			 */
		public bool IsCausedByNetwork {
			get {
				return ((serviceType & SpreadMessage.CAUSED_BY_NETWORK) != 0);
			}
		}
	
		/**
			 * Check if this is a self-leave message.
			 * If true, {@link MembershipInfo#getGroup()} is the only valid get*() function.
			 * 
			 * @return  true if this is a self-leave message
			 * @see  MembershipInfo#getGroup()
			 */
		public bool IsSelfLeave {
			get {
				return ((IsCausedByLeave == true) && (IsRegularMembership == false));
			}
		}
	
		// Get the group this message came from.
		/**
			 * Gets a SpreadGroup object representing the group that caused this message.
			 * 
			 * @return  the group that caused this message
			 */
		public SpreadGroup Group {
			get {
				return group;
			}
		}
	
		// Get the group's ID.
		/**
			 * Gets the GroupID for this group membership at this point in time.
			 * This is only valid if {@link MembershipInfo#isRegularMembership()} is true.
			 * If it is not true, null is returned.
			 * 
			 * @return  the GroupID for this group memebership at this point in time
			 */
		public GroupID GroupID {
			get {
				return groupID;
			}
		}
	
		// Get the group's members.
		/**
			 * Gets the private groups for all the members in the new group membership.
			 * This list will be in the same order everywhere it is received.
			 * This is only valid if {@link MembershipInfo#isRegularMembership()} is true.
			 * If it is not true, null is returned.
			 * 
			 * @return  the private groups for everyone in the new group membership
			 */
		public SpreadGroup[] Members {	
			get {
				// Check for a non-regular memberhsip message.
				if(IsRegularMembership == false) {
					return null;
				}
				return members;
			}
		}
	
		// Get the private group of the member who joined.
		/**
			 * Gets the private group of the member who joined.
			 * This is only valid if both {@link MembershipInfo#isRegularMembership()} and
			 * {@link MembershipInfo#isCausedByJoin()} are true.
			 * If either or both are false, null is returned.
			 * 
			 * @return  the private group of the member who joined
			 * @see  MembershipInfo#isCausedByJoin()
			 */
		public SpreadGroup Joined {
			get {
				if((IsRegularMembership) && (IsCausedByJoin))
					return groups[0];
				return null;
			}
		}
	
		// Get the private group of the member who left.
		/**
			 * Gets the private group of the member who left.
			 * This is only valid if both {@link MembershipInfo#isRegularMembership()} and
			 * ( {@link MembershipInfo#isCausedByLeave()} or {@link MembershipInfo#isCausedByDisconnect()} ) are true.
			 * If either or both are false, null is returned.
			 * 
			 * @return  the private group of the member who left
			 * @see  MembershipInfo#isCausedByLeave() MembershipInfo#isCausedByDisconnect()
			 */
		public SpreadGroup Left {
			get {
				if((IsRegularMembership) && 
					( IsCausedByLeave || IsCausedByDisconnect ) )
					return groups[0];
				return null;
			}
		}
	
		// Get the private group of the member who disconnected.
		/**
			 * Gets the private group of the member who disconnected.
			 * This is only valid if both {@link MembershipInfo#isRegularMembership()} and
			 * {@link MembershipInfo#isCausedByDisconnect()} are true.
			 * If either or both are false, null is returned.
			 * 
			 * @return  the private group of the member who disconnected
			 * @see  MembershipInfo#isCausedByDisconnect()
			 */
		public SpreadGroup Disconnected {
			get {
				if((IsRegularMembership) && (IsCausedByDisconnect))
					return groups[0];
				return null;
			}
		}
	
		// Get the private groups of the member who were not partitioned.
		/**
			 * Gets the private groups of the members who were not partitioned.
			 * This is only valid if both {@link MembershipInfo#isRegularMembership()} and
			 * {@link MembershipInfo#isCausedByNetwork()} are true.
			 * If either or both are false, null is returned.
			 * 
			 * @return  the private groups of the members who were not partitioned
			 * @see  MembershipInfo#isCausedByNetwork()
			 */
		public SpreadGroup[] Stayed {
			get {
				if((IsRegularMembership) && (IsCausedByNetwork))
					return groups;
				return null;
			}
		}
	}
}