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


import spread.*;

public class recThread extends Thread implements Runnable {
	private SpreadConnection connection;
        public  boolean threadSuspended;

	public recThread(SpreadConnection aConn) {
		connection=aConn;
	}
	private void DisplayMessage(SpreadMessage msg)
	{
		try
		{
   		        System.out.println("*****************RECTHREAD Received Message************");
			if(msg.isRegular())
			{
				System.out.print("Received a ");
				if(msg.isUnreliable())
					System.out.print("UNRELIABLE");
				else if(msg.isReliable())
					System.out.print("RELIABLE");
				else if(msg.isFifo())
					System.out.print("FIFO");
				else if(msg.isCausal())
					System.out.print("CAUSAL");
				else if(msg.isAgreed())
					System.out.print("AGREED");
				else if(msg.isSafe())
					System.out.print("SAFE");
				System.out.println(" message.");
				
				System.out.println("Sent by  " + msg.getSender() + ".");
				
				System.out.println("Type is " + msg.getType() + ".");
				
				if(msg.getEndianMismatch() == true)
					System.out.println("There is an endian mismatch.");
				else
					System.out.println("There is no endian mismatch.");
				
				SpreadGroup groups[] = msg.getGroups();
				System.out.println("To " + groups.length + " groups.");
				
				byte data[] = msg.getData();
				System.out.println("The data is " + data.length + " bytes.");
				
				System.out.println("The message is: " + new String(data));
			}
			else if ( msg.isMembership() )
			{
				MembershipInfo info = msg.getMembershipInfo();
				
				if(info.isRegularMembership())
				{
					SpreadGroup groups[] = msg.getGroups();
				
					System.out.println("Received a REGULAR membership.");
					System.out.println("For group " + info.getGroup() + ".");
					System.out.println("With " + groups.length + " members.");
					System.out.println("I am member " + msg.getType() + ".");
					for(int i = 0 ; i < groups.length ; i++)
						System.out.println("  " + groups[i]);
				
					System.out.println("Group ID is " + info.getGroupID());
				
					System.out.print("Due to ");
					if(info.isCausedByJoin())
					{
						System.out.println("the JOIN of " + info.getJoined() + ".");
					}
					else if(info.isCausedByLeave())
					{
						System.out.println("the LEAVE of " + info.getLeft() + ".");
					}
					else if(info.isCausedByDisconnect())
					{
						System.out.println("the DISCONNECT of " + info.getDisconnected() + ".");
					}
					else if(info.isCausedByNetwork())
					{
						SpreadGroup stayed[] = info.getStayed();
						System.out.println("NETWORK change.");
						System.out.println("VS set has " + stayed.length + " members:");
						for(int i = 0 ; i < stayed.length ; i++)
							System.out.println("  " + stayed[i]);
					}
				}
				else if(info.isTransition())
				{
					System.out.println("Received a TRANSITIONAL membership for group " + info.getGroup());
				}
				else if(info.isSelfLeave())
				{
					System.out.println("Received a SELF-LEAVE message for group " + info.getGroup());
				}
			} else if ( msg.isReject() )
			{
			        // Received a Reject message 
				System.out.print("Received a ");
				if(msg.isUnreliable())
					System.out.print("UNRELIABLE");
				else if(msg.isReliable())
					System.out.print("RELIABLE");
				else if(msg.isFifo())
					System.out.print("FIFO");
				else if(msg.isCausal())
					System.out.print("CAUSAL");
				else if(msg.isAgreed())
					System.out.print("AGREED");
				else if(msg.isSafe())
					System.out.print("SAFE");
				System.out.println(" REJECTED message.");
				
				System.out.println("Sent by  " + msg.getSender() + ".");
				
				System.out.println("Type is " + msg.getType() + ".");
				
				if(msg.getEndianMismatch() == true)
					System.out.println("There is an endian mismatch.");
				else
					System.out.println("There is no endian mismatch.");
				
				SpreadGroup groups[] = msg.getGroups();
				System.out.println("To " + groups.length + " groups.");
				
				byte data[] = msg.getData();
				System.out.println("The data is " + data.length + " bytes.");
				
				System.out.println("The message is: " + new String(data));
			} else {
			    System.out.println("Message is of unknown type: " + msg.getServiceType() );
			}
		}
		catch(Exception e)
		{
			e.printStackTrace();
			System.exit(1);
		}
	}
	
	public void run() {
	  while(true) {
            try {
	      DisplayMessage(connection.receive());

	      if (threadSuspended) {
                synchronized(this) {
                    while (threadSuspended) {
                        wait();
		    }
                }
	      }
	    } catch(Exception e) {

	    }
	  }
        }
}
