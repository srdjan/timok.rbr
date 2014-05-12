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
	public class SpreadGroup {
		// The group's name.
		private String name;
	
		// The connection this group exists on.
		private SpreadConnection connection;
	
		// Package constructor.
		internal SpreadGroup(SpreadConnection connection, String name) {
			// Store member variables.
			this.connection = connection;
			this.name = name;
		}
		public SpreadGroup() {
			// There is no connection yet.
			connection = null;
		
			// There is no name.
			name = null;
		}
		public void Join(SpreadConnection c, string groupName) {
			// Check if this group has already been joined.
			if(this.connection != null) {
				throw new SpreadException("Already joined.");
			}

			// Set member variables.
			this.connection = c;
			this.name = groupName;
		
			// Error check the name.
			byte[] bytes;
			try {
				bytes = System.Text.Encoding.ASCII.GetBytes(name);
			}
			catch(Exception e) {
				throw new SpreadException("Encoding not supported: "+e);
			}
			for(int i = 0 ; i < bytes.Length ; i++) {
				// Make sure the byte (character) is within the valid range.
				if((bytes[i] < 36) || (bytes[i] > 126)) {
					throw new SpreadException("Illegal character in group name.");
				}
			}
		
			// Get a new message.
			SpreadMessage joinMessage = new SpreadMessage();
		
			// Set the group we're sending to.
			joinMessage.AddGroup(name);
		
			// Set the service type.
			joinMessage.ServiceType = SpreadMessage.JOIN_MESS;
		
			// Send the message.
			connection.Multicast(joinMessage);
		}
		public void Leave(){
			// Check if we can leave.
			if(connection == null) {
				throw new SpreadException("No group to leave.");
			}
		
			// Get a new message.
			SpreadMessage leaveMessage = new SpreadMessage();
		
			// Set the group we're sending to.
			leaveMessage.AddGroup(name);
		
			// Set the service type.
			leaveMessage.ServiceType = SpreadMessage.LEAVE_MESS;
		
			// Send the message.
			connection.Multicast(leaveMessage);
		
			// No longer connected.
			connection = null;
		}

		public override string ToString() {
			return name;
		}
	}
}