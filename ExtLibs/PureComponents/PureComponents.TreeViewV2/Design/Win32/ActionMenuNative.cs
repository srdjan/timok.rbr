#region Copyright (c) PureComponents, All Rights Reserved

/* ---------------------------------------------------------------------*
*                           PureComponents                              *
*              Copyright (c) All Rights reserved                        *
*                                                                       *
*                                                                       *
* This file and its contents are protected by Czech and                 *
* International copyright laws.  Unauthorized reproduction and/or       *
* distribution of all or any portion of the code contained herein       *
* is strictly prohibited and will result in severe civil and criminal   *
* penalties.  Any violations of this copyright will be prosecuted       *
* to the fullest extent possible under law.                             *
*                                                                       *
* THE SOURCE CODE CONTAINED HEREIN AND IN RELATED FILES IS PROVIDED     *
* TO THE REGISTERED DEVELOPER FOR THE PURPOSES OF EDUCATION AND         *
* TROUBLESHOOTING. UNDER NO CIRCUMSTANCES MAY ANY PORTION OF THE SOURCE *
* CODE BE DISTRIBUTED, DISCLOSED OR OTHERWISE MADE AVAILABLE TO ANY     *
* THIRD PARTY WITHOUT THE EXPRESS WRITTEN CONSENT OF PURECOMPONENYS     *
*                                                                       *
* UNDER NO CIRCUMSTANCES MAY THE SOURCE CODE BE USED IN WHOLE OR IN     *
* PART, AS THE BASIS FOR CREATING A PRODUCT THAT PROVIDES THE SAME,     *
* SIMILAR, SUBSTANTIALLY SIMILAR OR THE SAME, FUNCTIONALITY AS ANY      *
* PURECOMPONENTS PRODUCT.                                               *
*                                                                       *
* THE REGISTERED DEVELOPER ACKNOWLEDGES THAT THIS SOURCE CODE           *
* CONTAINS VALUABLE AND PROPRIETARY TRADE SECRETS OF PURECOMPONENTS.    *
* THE REGISTERED DEVELOPER AGREES TO EXPEND EVERY EFFORT TO             *
* INSURE ITS CONFIDENTIALITY.                                           *
*                                                                       *
* THE END USER LICENSE AGREEMENT (EULA) ACCOMPANYING THE PRODUCT        *
* PERMITS THE REGISTERED DEVELOPER TO REDISTRIBUTE THE PRODUCT IN       *
* EXECUTABLE FORM ONLY IN SUPPORT OF APPLICATIONS WRITTEN USING         *
* THE PRODUCT.  IT DOES NOT PROVIDE ANY RIGHTS REGARDING THE            *
* SOURCE CODE CONTAINED HEREIN.                                         *
*                                                                       *
* THIS COPYRIGHT NOTICE MAY NOT BE REMOVED FROM THIS FILE.              *
* --------------------------------------------------------------------- *
*/

#endregion Copyright (c) PureComponents, All Rights Reserved

using System;
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Drawing.Text;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace PureComponents.TreeView.Design
{
	/// <summary>
	/// The action menu item object
	/// </summary>
	internal class ActionMenuItem
	{
		public string Text;
		public bool Selected = false;
		public ActionMenuGroup MenuGroup;
	}

	/// <summary>
	/// The action menu group object
	/// </summary>
	internal class ActionMenuGroup
	{
		public ArrayList Items = new ArrayList();

		public string Title;
		public bool Expanded = false;		
	}
	
	internal class TreeViewSelector : NativeWindow
	{
		private Size m_oCurrentSize;	
		private Point m_oCurrentPoint;
		private Point m_oScreenPos;
		private bool m_exitLoop = false;	

		internal bool Visible = false;
		internal ISelectionService SelectionService;
		internal TreeView TreeView;

		private Image m_SelectorImage;

		private static Image LoadImage(string imageName)
		{			
			Stream strm = Type.GetType("PureComponents.TreeView.TreeView").Assembly.GetManifestResourceStream
				("PureComponents.TreeView.Design." + imageName);
			
			Image ic = null;
			if(strm != null)
			{
				ic = System.Drawing.Image.FromStream(strm);
				strm.Close();
			}

			return ic;
		}

		protected void RecalcLayout()
		{
			m_oCurrentSize = new Size(13, 13);
		}

		protected void UpdateLayeredWindow()
		{
			UpdateLayeredWindow(m_oCurrentPoint, m_oCurrentSize, 255);
		}

		protected void UpdateLayeredWindow(byte alpha)
		{
			UpdateLayeredWindow(m_oCurrentPoint, m_oCurrentSize, alpha);
		}

		protected void UpdateLayeredWindow(Point point, Size size, byte alpha)
		{
			Graphics g = Graphics.FromHwnd(this.Handle);
			g.DrawImage(m_SelectorImage, 0, 0);
			
			g.Dispose();
		}

		protected POINT MousePositionToClient(POINT point)
		{
			POINT localPoint;
			localPoint.x = point.x;
			localPoint.y = point.y;
			User32.ScreenToClient(this.Handle, ref localPoint);

			return localPoint;
		}

		protected POINT MousePositionToScreen(POINT point)
		{
			POINT localPoint;
			localPoint.x = point.x;
			localPoint.y = point.y;

			User32.ClientToScreen(this.Handle, ref localPoint);

			return localPoint;
		}

		protected POINT MousePositionToScreen(MSG msg)
		{
			POINT screenPos;
			screenPos.x = (short)((uint)msg.lParam & 0x0000FFFFU);
			screenPos.y = (short)(((uint)msg.lParam & 0xFFFF0000U) >> 16);

			// Convert the mouse position to screen coordinates, 
			// but not for non-client messages
			if ((msg.message != (int)Msgs.WM_NCLBUTTONUP) &&
				(msg.message != (int)Msgs.WM_NCMBUTTONUP) &&
				(msg.message != (int)Msgs.WM_NCRBUTTONUP) &&
				(msg.message != (int)Msgs.WM_NCXBUTTONUP) &&
				(msg.message != (int)Msgs.WM_NCLBUTTONDOWN) &&
				(msg.message != (int)Msgs.WM_NCMBUTTONDOWN) &&
				(msg.message != (int)Msgs.WM_NCRBUTTONDOWN) &&
				(msg.message != (int)Msgs.WM_NCXBUTTONDOWN))	
			{
				// Convert the mouse position to screen coordinates
				User32.ClientToScreen(msg.hwnd, ref screenPos);
			}

			return screenPos;
		}

		protected POINT MousePositionToScreen(Message msg)
		{
			POINT screenPos;
			screenPos.x = (short)((uint)msg.LParam & 0x0000FFFFU);
			screenPos.y = (short)(((uint)msg.LParam & 0xFFFF0000U) >> 16);

			// Convert the mouse position to screen coordinates, 
			// but not for non-client messages
			if ((msg.Msg != (int)Msgs.WM_NCLBUTTONUP) &&
				(msg.Msg != (int)Msgs.WM_NCMBUTTONUP) &&
				(msg.Msg != (int)Msgs.WM_NCRBUTTONUP) &&
				(msg.Msg != (int)Msgs.WM_NCXBUTTONUP) &&
				(msg.Msg != (int)Msgs.WM_NCLBUTTONDOWN) &&
				(msg.Msg != (int)Msgs.WM_NCMBUTTONDOWN) &&
				(msg.Msg != (int)Msgs.WM_NCRBUTTONDOWN) &&
				(msg.Msg != (int)Msgs.WM_NCXBUTTONDOWN))	
			{
				// Convert the mouse position to screen coordinates
				User32.ClientToScreen(msg.HWnd, ref screenPos);
			}

			return screenPos;
		}

		public void Show(int nX, int nY)
		{
			Visible = true;
			m_SelectorImage = LoadImage("Selector.gif");

			// Decide if we need layered windows			
			CreateParams cp = new CreateParams();
	
			// Any old title will do as it will not be shown
			cp.Caption = "PureComponents.TreeViewSelector";

			RecalcLayout();
		
			// get the max screen size. if the nX + Width or nY + Height is bigger than the area, them move it elsewhere and recalculate the
			// position
			Screen oScreen = Screen.FromHandle(this.Handle);
			if (nX + m_oCurrentSize.Width > oScreen.Bounds.Width)
				nX = oScreen.Bounds.Width - m_oCurrentSize.Width;
	
			if (nY + m_oCurrentSize.Height > oScreen.Bounds.Height)
				nY = oScreen.Bounds.Height - m_oCurrentSize.Height;
	
			// set the position
			m_oScreenPos = new Point(nX, nY);
	
			Size winSize = m_oCurrentSize;
			Point screenPos = m_oScreenPos;						

			// Define the screen position/size			
			cp.X = nX;
			cp.Y = nY;
			cp.Height = winSize.Height;
			cp.Width = winSize.Width;
	
			// As a top-level window it has no parent
			cp.Parent = IntPtr.Zero;
				
			// Appear as a top-level window
			cp.Style = unchecked((int)(uint)WindowStyles.WS_POPUP);
				
			// Set styles so that it does not have a caption bar and is above all other 
			// windows in the ZOrder, i.e. TOPMOST
			cp.ExStyle = (int)WindowExStyles.WS_EX_TOPMOST + 
				(int)WindowExStyles.WS_EX_TOOLWINDOW;
	
			// Create the actual window
			if (this.Handle == IntPtr.Zero)
				this.CreateHandle(cp);
	
			// Remember the correct screen drawing details
			m_oCurrentSize = winSize;
			m_oCurrentPoint = screenPos;
			// Show the window without activating it (i.e. do not take focus)
			User32.ShowWindow(this.Handle, (short)ShowWindowStyles.SW_SHOWNOACTIVATE);	

			UpdateLayeredWindow();
		
			MSG msg = new MSG();	
			m_exitLoop = false;
	
			while (!m_exitLoop)
			{
				// Suspend thread until a windows message has arrived
				if (User32.WaitMessage())
				{
					// Take a peek at the message details without removing from queue
					while(!m_exitLoop && User32.PeekMessage(ref msg, 0, 0, 0, (int)PeekMessageFlags.PM_NOREMOVE))
					{
						// test if the mouse up is in the area of the menu
						int localWidth = m_oCurrentSize.Width;
						int localHeight = m_oCurrentSize.Height;						
						
						bool eatMessage = false;
						
						#region mouse move
						if (msg.message == (int)Msgs.WM_MOUSEMOVE)
						{
							POINT screenPosMsg = MousePositionToScreen(msg);
							
							// Is the POINT inside the Popup window rectangle
							if ((screenPosMsg.x >= m_oCurrentPoint.X) && (screenPosMsg.x <= (m_oCurrentPoint.X + localWidth)) &&
								(screenPosMsg.y >= m_oCurrentPoint.Y) && (screenPosMsg.y <= (m_oCurrentPoint.Y + localHeight)))
							{
								eatMessage = true;

								//OnWM_MOUSEMOVE(screenPosMsg.x, screenPosMsg.y);
							}
							else
							{
								if (User32.GetMessage(ref msg, 0, 0, 0))
								{
									User32.TranslateMessage(ref msg);
									User32.DispatchMessage(ref msg);
								}
							}
						}
						#endregion

						#region left button up
						if (msg.message == (int)Msgs.WM_LBUTTONUP)
						{
							POINT screenPosMsg = MousePositionToScreen(msg);
							
							// Is the POINT inside the Popup window rectangle
							if ((screenPosMsg.x >= m_oCurrentPoint.X) && (screenPosMsg.x <= (m_oCurrentPoint.X + localWidth)) &&
								(screenPosMsg.y >= m_oCurrentPoint.Y) && (screenPosMsg.y <= (m_oCurrentPoint.Y + localHeight)))
							{
								eatMessage = true;

								Hide();

								POINT localPoint = MousePositionToClient(screenPosMsg); 

								//if (SelectionService != null)
								//	SelectionService.SetSelectedComponents(new Component [] {TreeView}, SelectionTypes.Replace);
							}
							else
							{
								//Hide();

								//m_exitLoop = true;								
							}
						}						
						#endregion												

						if (eatMessage == true)
						{
							MSG eat = new MSG();
							User32.GetMessage(ref eat, 0, 0, 0);

							eatMessage = false;
						}
						else						
						{
							if (User32.GetMessage(ref msg, 0, 0, 0))
							{
								User32.TranslateMessage(ref msg);
								User32.DispatchMessage(ref msg);
							}
						}
					}
				}
			}
		}

		public void Hide()
		{
			m_exitLoop = true;
			Visible = false;	

			User32.ShowWindow(this.Handle, (short)ShowWindowStyles.SW_HIDE);					
		}

		protected override void WndProc(ref Message m)
		{
			switch(m.Msg)
			{					
				case (int)Msgs.WM_ACTIVATEAPP:
					Hide();
					base.WndProc(ref m);
					break;

				default:
					base.WndProc(ref m);
					break;
			}			
		}
	}

	/// <summary>
	/// Summary description for ActionMenuNative.
	/// </summary>
	internal class ActionMenuNative : NativeWindow
	{
		#region private members
		private bool m_bLayered;
		private Color m_oBackColor;
		private Color m_oControlLL;
		private Size m_oCurrentSize;	
		private Point m_oScreenPos;
		private Point m_oCurrentPoint;
		private SolidBrush m_oLBrush;
		private SolidBrush m_oEBrush;
		private SolidBrush m_oLLBrush;
		private Point m_oLastMousePos;

		protected bool m_bMouseOver = false;

		private static int m_nImageWidth = 16;
		private static int m_nShadowLength = 4;
		private static Bitmap m_oShadowCache = null;
		private static int m_nShadowCacheWidth = 0;
		private static int m_nShadowCacheHeight = 0;

		private bool m_bSupportsLayered = false;

		private Color m_oBorderColor = Color.FromArgb(0xAA, 0x8E, 0x7D);		
		private Color m_oSelectColor = Color.Azure;
		private string m_sTitle = "Context Menu";

		private int m_nWidth = 150;
		private Point m_oLastMouseDown;		

		private ArrayList m_aGroups = new ArrayList();
		private Hashtable m_mapGroupBox2Rect = new Hashtable();
		private Hashtable m_mapRect2GroupBox = new Hashtable();
		private Hashtable m_mapItem2Rect = new Hashtable();
		private Hashtable m_mapRect2Item = new Hashtable();

		private ActionMenuItem m_oSelectedItem = null;
		private ActionMenuGroup m_oHighlightedGroup = null;
		private bool m_bExpandCollapseStrafeSelect = false;	
		private bool m_bDrag = false;	
		#endregion

		#region properties
		[
		Category("ActionMenu"),
		Description("Font of the context menu"),
		]
		public int Width
		{
			set
			{
				m_nWidth = value;				
			}			
		}

		[
		Category("ActionMenu"),
		Description("Font of the context menu"),
		]
		public Font Font
		{
			get
			{
				return new Font("Microsoft Sans Serif", (float)8.25);
			}			
		}

		[
		Category("ActionMenu"),
		Description("Title of the context menu"),
		]
		public string Title
		{
			get
			{
				return m_sTitle;
			}

			set
			{
				m_sTitle = value;
			}
		}

		[
		Category("ActionMenu"),
		Description("The color of the border"),
		]
		public Color BorderColor
		{
			get
			{
				return m_oBorderColor;
			}

			set
			{
				m_oBorderColor = value;
			}
		}

		[
		Category("ActionMenu"),
		Description("The color of the selected item"),
		]
		public Color SelectColor
		{
			get
			{
				return m_oSelectColor;
			}

			set
			{
				m_oSelectColor = value;
			}
		}
		#endregion		

		#region helper structs
		private enum PaintItem
		{
			BT = 0,	BL = 1, BB = 2, BR = 3, IGT = 4, IGL = 5, IGB = 6, IGR = 7,	TGL	= 8, TGR = 9,
			SMGL = 10, SMW = 11, SBGR = 12, SPH	= 13, SPW = 14, SG = 15, SW = 16, SH = 17, EWG = 18,
			EHG	= 19, ERG = 20,	ER = 21
		}		
		
		private static readonly int[,] m_aPosition = { 
														{2, 1, 0, 1, 2, 3, 3, 5, 4, 4, 2, 6, 5, 5, 1, 10, 4, 4, 2, 2, 0, 0},
														{1, 0, 1, 2, 2, 1, 3, 4, 3, 3, 2, 8, 5, 5, 5, 10, 0, 0, 2, 2, 2, 5}	
													 };
		#endregion

		#region construction
		public ActionMenuNative()
		{			
			m_bSupportsLayered = (OSFeature.Feature.GetVersionPresent(OSFeature.LayeredWindows) != null);

			m_oBackColor = Color.White;
			m_oControlLL = CalculateColor(SystemColors.Window, m_oBackColor, 220);
			m_oLBrush = new SolidBrush(CalculateColor(m_oBackColor, Color.White, 200));
			m_oEBrush = new SolidBrush(CalculateColor(m_oBackColor, Color.White, 150));
			m_oLLBrush = new SolidBrush(m_oControlLL);			
		}
		#endregion

		#region implementation
		public ActionMenuGroup AddMenuGroup(string sGroup)
		{
			ActionMenuGroup oGroup = new ActionMenuGroup();
			oGroup.Title = sGroup;

			m_aGroups.Add(oGroup);

			RecalcLayout();

			Invalidate();

			return oGroup;
		}

		public void RemoveMenuGroup(ActionMenuGroup oGroup)
		{
			m_aGroups.Remove(oGroup);

			RecalcLayout();

			Invalidate();
		}

		public void ClearMenuGroups()
		{			
			m_aGroups.Clear();
		}		

		public ActionMenuItem AddMenuItem(ActionMenuGroup oGroup, string sItem)
		{
			ActionMenuItem oItem = new ActionMenuItem();

			oItem.Text = sItem;
			oItem.MenuGroup = oGroup;
			oGroup.Items.Add(oItem);

			return oItem;
		}
		
		internal bool AllGroupsExpanded()
		{
			foreach (ActionMenuGroup oGroup in m_aGroups)
				if (oGroup.Expanded == false)
					return false;

			return true;
		}
		#endregion			

		#region defined events
		internal delegate void ItemClickEventHandler(ActionMenuItem oItem);
		internal event ItemClickEventHandler ItemClick;
		protected virtual void OnItemClick(ActionMenuItem oItem) { if (ItemClick != null) ItemClick(oItem); }
		#endregion

		#region native window implementation
		public void Show(int nX, int nY)
		{
			// Decide if we need layered windows
			m_bLayered = (m_bSupportsLayered);					

			CreateParams cp = new CreateParams();

			// Any old title will do as it will not be shown
			cp.Caption = "PureComponents.ActionMenu";

			RecalcLayout();

			// get the max screen size. if the nX + Width or nY + Height is bigger than the area, them move it elsewhere and recalculate the
			// position
			Screen oScreen = Screen.FromHandle(this.Handle);

			if (nX + m_oCurrentSize.Width > oScreen.Bounds.Width)
				nX = oScreen.Bounds.Width - m_oCurrentSize.Width;

			if (nY + m_oCurrentSize.Height > oScreen.Bounds.Height)
				nY = oScreen.Bounds.Height - m_oCurrentSize.Height;

			// set the position
			m_oScreenPos = new Point(nX, nY);

			Size winSize = m_oCurrentSize;
			Point screenPos = m_oScreenPos;
			
			// Define the screen position/size			
			cp.X = nX;
			cp.Y = nY;
			cp.Height = winSize.Height;
			cp.Width = winSize.Width;

			// As a top-level window it has no parent
			cp.Parent = IntPtr.Zero;
			
			// Appear as a top-level window
			cp.Style = unchecked((int)(uint)WindowStyles.WS_POPUP);
			
			// Set styles so that it does not have a caption bar and is above all other 
			// windows in the ZOrder, i.e. TOPMOST
			cp.ExStyle = (int)WindowExStyles.WS_EX_TOPMOST + 
				(int)WindowExStyles.WS_EX_TOOLWINDOW;

			// OS specific style
			if (m_bLayered)
			{
				// If not on NT then we are going to use alpha blending on the shadow border
				// and so we need to specify the layered window style so the OS can handle it
				cp.ExStyle += (int)WindowExStyles.WS_EX_LAYERED;
			}

			// Create the actual window
			if (this.Handle == IntPtr.Zero)
				this.CreateHandle(cp);

			// Remember the correct screen drawing details
			m_oCurrentSize = winSize;
			m_oCurrentPoint = screenPos;

			// Update the image for display
			if (m_bLayered)
				UpdateLayeredWindow();
				
			// Show the window without activating it (i.e. do not take focus)
			User32.ShowWindow(this.Handle, (short)ShowWindowStyles.SW_SHOWNOACTIVATE);	
		
			MSG msg = new MSG();

			bool _exitLoop = false;

//			POINT lastMousePoint;

			while(!_exitLoop)
			{
				// Suspend thread until a windows message has arrived
				if (User32.WaitMessage())
				{
					// Take a peek at the message details without removing from queue
					while(!_exitLoop && User32.PeekMessage(ref msg, 0, 0, 0, (int)PeekMessageFlags.PM_NOREMOVE))
					{
						// test if the mouse up is in the area of the menu
						int localWidth = m_oCurrentSize.Width - m_aPosition[0, (int)PaintItem.SW];
						int localHeight = m_oCurrentSize.Height - m_aPosition[0, (int)PaintItem.SH];						
						
						bool eatMessage = false;
						
						#region mouse move
						if (msg.message == (int)Msgs.WM_MOUSEMOVE)
						{
							POINT screenPosMsg = MousePositionToScreen(msg);
							
							// Is the POINT inside the Popup window rectangle
							if ((screenPosMsg.x >= m_oCurrentPoint.X) && (screenPosMsg.x <= (m_oCurrentPoint.X + localWidth)) &&
								(screenPosMsg.y >= m_oCurrentPoint.Y) && (screenPosMsg.y <= (m_oCurrentPoint.Y + localHeight)))
							{
								eatMessage = true;

								OnWM_MOUSEMOVE(screenPosMsg.x, screenPosMsg.y);

								if (m_oHighlightedGroup != null)
									User32.SetCursor(User32.LoadCursor(IntPtr.Zero, (uint)Cursors.IDC_HAND));
								else
									User32.SetCursor(User32.LoadCursor(IntPtr.Zero, (uint)Cursors.IDC_ARROW));								
							}
							else
							{
								if (m_bDrag == true)
								{
									OnWM_MOUSEMOVE(screenPosMsg.x, screenPosMsg.y);
								}
								else
								{
									// clear selection and redraw the window
									if (m_oSelectedItem != null)
									{
										m_oSelectedItem.Selected = false;
										m_oSelectedItem = null;

										RecalcLayout();
										Invalidate();				
									}

									if (m_oHighlightedGroup != null)
									{
										m_oHighlightedGroup = null;

										RecalcLayout();
										Invalidate();
									}		
	
									if (m_bExpandCollapseStrafeSelect == true)
									{
										m_bExpandCollapseStrafeSelect = false;

										RecalcLayout();
										Invalidate();
									}

									if (User32.GetMessage(ref msg, 0, 0, 0))
									{
										User32.TranslateMessage(ref msg);
										User32.DispatchMessage(ref msg);
									}
								}
							}
						}
						#endregion

						#region left button up
						if (msg.message == (int)Msgs.WM_LBUTTONUP)
						{
							POINT screenPosMsg = MousePositionToScreen(msg);
							m_bDrag = false;
							
							// Is the POINT inside the Popup window rectangle
							if ((screenPosMsg.x >= m_oCurrentPoint.X) && (screenPosMsg.x <= (m_oCurrentPoint.X + localWidth)) &&
								(screenPosMsg.y >= m_oCurrentPoint.Y) && (screenPosMsg.y <= (m_oCurrentPoint.Y + localHeight)))
							{
								eatMessage = true;

								POINT localPoint = MousePositionToClient(screenPosMsg); 

								if (OnWM_LBUTTONUP(localPoint.x, localPoint.y) == true)
								{
									Hide();

									return;
								}								
							}
							else
							{
								Hide();

								_exitLoop = true;

								if (User32.GetMessage(ref msg, 0, 0, 0))
								{
									User32.TranslateMessage(ref msg);
									User32.DispatchMessage(ref msg);
								}
							}
						}						
						#endregion						

						#region mouse click events
						if ((msg.message == (int)Msgs.WM_MBUTTONDOWN) ||
							(msg.message == (int)Msgs.WM_RBUTTONDOWN) ||
							(msg.message == (int)Msgs.WM_XBUTTONDOWN) ||
							(msg.message == (int)Msgs.WM_NCLBUTTONDOWN) ||
							(msg.message == (int)Msgs.WM_NCMBUTTONDOWN) ||
							(msg.message == (int)Msgs.WM_NCRBUTTONDOWN) ||
							(msg.message == (int)Msgs.WM_NCXBUTTONDOWN))
						{
							POINT screenPosMsg = MousePositionToScreen(msg);

							// Is the POINT inside the Popup window rectangle
							if ((screenPosMsg.x >= m_oCurrentPoint.X) && (screenPosMsg.x <= (m_oCurrentPoint.X + localWidth)) &&
								(screenPosMsg.y >= m_oCurrentPoint.Y) && (screenPosMsg.y <= (m_oCurrentPoint.Y + localHeight)))
							{
								eatMessage = true;

								// if it is the left mouse then check the drag
								if (msg.message != (int)Msgs.WM_RBUTTONDOWN && msg.message != (int)Msgs.WM_MBUTTONDOWN)
								{								
									// check if it is in the header, if yes, start the dragging mode
									if ((screenPosMsg.x >= m_oCurrentPoint.X) && (screenPosMsg.x <= (m_oCurrentPoint.X + localWidth)) &&
										(screenPosMsg.y >= m_oCurrentPoint.Y) && (screenPosMsg.y <= (m_oCurrentPoint.Y + 10)))
									{
										m_bDrag = true;
									}
									else
										m_bDrag = false;
								}		
							}
							else
							{
								m_bDrag = false;
								Hide();

								_exitLoop = true;

								if (User32.GetMessage(ref msg, 0, 0, 0))
								{
									User32.TranslateMessage(ref msg);
									User32.DispatchMessage(ref msg);
								}
							}
						}
						#endregion

						#region key press
						if (msg.message == (int)Msgs.WM_KEYDOWN)
						{
							switch((int)msg.wParam)
							{
								case (int)VirtualKeys.VK_ESCAPE:
									Hide();
									return;									
							}
						}
						#endregion

						if (eatMessage == true)
						{
							MSG eat = new MSG();
							User32.GetMessage(ref eat, 0, 0, 0);

							eatMessage = false;
						}
						else						
						{
							if (User32.GetMessage(ref msg, 0, 0, 0))
							{
								User32.TranslateMessage(ref msg);
								User32.DispatchMessage(ref msg);
							}
						}
					}
				}
			}
		}

		protected void UpdateLayeredWindow()
		{
			m_mapGroupBox2Rect.Clear();
			m_mapItem2Rect.Clear();
			m_mapRect2GroupBox.Clear();
			m_mapRect2Item.Clear();

			UpdateLayeredWindow(m_oCurrentPoint, m_oCurrentSize, 255);
		}

		protected void UpdateLayeredWindow(byte alpha)
		{
			UpdateLayeredWindow(m_oCurrentPoint, m_oCurrentSize, alpha);
		}

		protected void UpdateLayeredWindow(Point point, Size size, byte alpha)
		{
			// Create bitmap for drawing onto
			Bitmap memoryBitmap = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);

			using(Graphics g = Graphics.FromImage(memoryBitmap))
			{
				Rectangle area = new Rectangle(0, 0, size.Width, size.Height);
		
				// Draw the background area
				DrawBackground(g, area);

				// Draw the actual menu items
				DrawAllCommands(g);

				// Get hold of the screen DC
				IntPtr hDC = User32.GetDC(IntPtr.Zero);
				
				// Create a memory based DC compatible with the screen DC
				IntPtr memoryDC = Gdi32.CreateCompatibleDC(hDC);
				
				// Get access to the bitmap handle contained in the Bitmap object
				IntPtr hBitmap = memoryBitmap.GetHbitmap(Color.FromArgb(0));

				// Select this bitmap for updating the window presentation
				IntPtr oldBitmap = Gdi32.SelectObject(memoryDC, hBitmap);

				// New window size
				SIZE ulwsize;
				ulwsize.cx = size.Width;
				ulwsize.cy = size.Height;

				// New window position
				POINT topPos;
				topPos.x = point.X;
				topPos.y = point.Y;

				// Offset into memory bitmap is always zero
				POINT pointSource;
				pointSource.x = 0;
				pointSource.y = 0;

				// We want to make the entire bitmap opaque 
				BLENDFUNCTION blend = new BLENDFUNCTION();
				blend.BlendOp = (byte)AlphaFlags.AC_SRC_OVER;
				blend.BlendFlags = 0;
				blend.SourceConstantAlpha = alpha;
				blend.AlphaFormat = (byte)AlphaFlags.AC_SRC_ALPHA;

				// Tell operating system to use our bitmap for painting
				User32.UpdateLayeredWindow(Handle, hDC, ref topPos, ref ulwsize, 
					memoryDC, ref pointSource, 0, ref blend, 
					(int)UpdateLayeredWindowsFlags.ULW_ALPHA);

				// Put back the old bitmap handle
				Gdi32.SelectObject(memoryDC, oldBitmap);

				// Cleanup resources
				User32.ReleaseDC(IntPtr.Zero, hDC);
				Gdi32.DeleteObject(hBitmap);
				Gdi32.DeleteDC(memoryDC);
			}
		}
		#endregion

		#region drawing functions
		protected void RecalcLayout()
		{
			int nHeight = 23;

			// paint groups
			foreach (ActionMenuGroup oGroup in m_aGroups)
				nHeight += CalcGroupHeight(oGroup);

			m_oCurrentSize = new Size(m_nWidth, nHeight);
		}
	
		protected int CalcGroupHeight(ActionMenuGroup oGroup)
		{
			if (oGroup.Expanded == false)
				return 23;

			int nHeight = 28;

			foreach (ActionMenuItem oItem in oGroup.Items)
			{
				if (oItem.Text == "-")
					nHeight += 3;
				else
					nHeight += 20;
			}

			return nHeight;
		}

		protected void Invalidate()
		{
			UpdateLayeredWindow();
		}

		protected void DrawAllCommands(Graphics oGraphics)
		{
			int nY = 0;

			Rectangle main = new Rectangle(0, 0, 
				m_oCurrentSize.Width - 1 - m_aPosition[(int)0, (int)PaintItem.SW], 
				m_oCurrentSize.Height - 1 - m_aPosition[(int)0, (int)PaintItem.SH]);

			// paint the title
			SolidBrush oBrush = new SolidBrush(m_oBorderColor);
			Font oFont = new Font(this.Font.FontFamily.Name, this.Font.Size, FontStyle.Bold);			
			oGraphics.FillRectangle(oBrush, 0, 0, main.Width, 18);

			Pen oPen = new Pen(m_oSelectColor, 1);
			oGraphics.DrawRectangle(oPen, main.Width - 15, 3, 11, 10);				
			oGraphics.DrawLine(oPen, main.Width - 13, 5, main.Width - 12, 5);
			oGraphics.DrawLine(oPen, main.Width - 12, 6, main.Width - 11, 6);								
			oGraphics.DrawLine(oPen, main.Width - 7, 5, main.Width - 6, 5);
			oGraphics.DrawLine(oPen, main.Width - 8, 6, main.Width - 7, 6);
				
			oGraphics.DrawLine(oPen, main.Width - 11, 7, main.Width - 8, 7);
			oGraphics.DrawLine(oPen, main.Width - 10, 8, main.Width - 9, 8);
				
			oGraphics.DrawLine(oPen, main.Width - 11, 9, main.Width - 8, 9);
			oGraphics.DrawLine(oPen, main.Width - 13, 11, main.Width - 12, 11);
			oGraphics.DrawLine(oPen, main.Width - 12, 10, main.Width - 11, 10);								
			oGraphics.DrawLine(oPen, main.Width - 7, 11, main.Width - 6, 11);
			oGraphics.DrawLine(oPen, main.Width - 8, 10, main.Width - 7, 10);
				
			oBrush.Color = m_oSelectColor;		
			oGraphics.DrawString(m_sTitle, oFont, oBrush, 3, 2);
			oBrush.Dispose();

			nY = 23;

			// paint groups
			foreach (ActionMenuGroup oGroup in m_aGroups)
			{
				if (oGroup.Expanded == true)
				{
					PaintGroupExpanded(oGroup, oGraphics, ref nY);
					continue;
				}

				PaintGroupClosed(oGroup, oGraphics, ref nY);
			}
		}

		private void PaintGroupClosed(ActionMenuGroup oGroup, Graphics oGraphics, ref int nY)
		{
			Brush oBrush;

			Rectangle main = new Rectangle(0, 0, 
				m_oCurrentSize.Width - 1 - m_aPosition[(int)0, (int)PaintItem.SW], 
				m_oCurrentSize.Height - 1 - m_aPosition[(int)0, (int)PaintItem.SH]);
			
			if (m_oHighlightedGroup == oGroup)
				oBrush = new SolidBrush(Color.FromArgb(0x8A, 0x6E, 0x5D));
			else
				oBrush = new SolidBrush(m_oBorderColor);

			oGraphics.FillRectangle(oBrush, 2, nY, main.Width - 3, 18);
			oGraphics.DrawString(oGroup.Title, Font, Brushes.White, 5, nY + 2);
			oBrush.Dispose();

			Pen oPen = new Pen(m_oSelectColor, 1);
			oGraphics.DrawLine(oPen, main.Width - 15, nY + 5, main.Width - 14, nY + 5);
			oGraphics.DrawLine(oPen, main.Width - 14, nY + 6, main.Width - 13, nY + 6);
			oGraphics.DrawLine(oPen, main.Width - 13, nY + 7, main.Width - 12, nY + 7);
			oGraphics.DrawLine(oPen, main.Width - 12, nY + 8, main.Width - 11, nY + 8);
			oGraphics.DrawLine(oPen, main.Width - 11, nY + 7, main.Width - 10, nY + 7);
			oGraphics.DrawLine(oPen, main.Width - 10, nY + 6, main.Width - 9, nY + 6);
			oGraphics.DrawLine(oPen, main.Width - 9, nY + 5, main.Width - 8, nY + 5);

			oGraphics.DrawLine(oPen, main.Width - 15, nY + 8, main.Width - 14, nY + 8);
			oGraphics.DrawLine(oPen, main.Width - 14, nY + 9, main.Width - 13, nY + 9);
			oGraphics.DrawLine(oPen, main.Width - 13, nY + 10, main.Width - 12, nY + 10);
			oGraphics.DrawLine(oPen, main.Width - 12, nY + 11, main.Width - 11, nY + 11);
			oGraphics.DrawLine(oPen, main.Width - 11, nY + 10, main.Width - 10, nY + 10);
			oGraphics.DrawLine(oPen, main.Width - 10, nY + 9, main.Width - 9, nY + 9);
			oGraphics.DrawLine(oPen, main.Width - 9, nY + 8, main.Width - 8, nY + 8);
			oPen.Dispose();

			Rectangle oRect = new Rectangle(1, nY, main.Width - 2, 18);
			m_mapGroupBox2Rect.Add(oGroup, oRect);
			m_mapRect2GroupBox.Add(oRect, oGroup);

			nY += 23;
		}

		private void PaintGroupExpanded(ActionMenuGroup oGroup, Graphics oGraphics, ref int nY)
		{
			Brush oBrush;

			Rectangle main = new Rectangle(0, 0, 
				m_oCurrentSize.Width - 1 - m_aPosition[(int)0, (int)PaintItem.SW], 
				m_oCurrentSize.Height - 1 - m_aPosition[(int)0, (int)PaintItem.SH]);
			
			if (m_oHighlightedGroup == oGroup)
				oBrush = new SolidBrush(Color.FromArgb(0x8A, 0x6E, 0x5D));
			else
				oBrush = new SolidBrush(m_oBorderColor);
			oGraphics.FillRectangle(oBrush, 2, nY, main.Width - 3, 18);
			oGraphics.DrawString(oGroup.Title, Font, Brushes.White, 5, nY + 2);
			oBrush.Dispose();

			Pen oPen = new Pen(m_oSelectColor, 1);
			oGraphics.DrawLine(oPen, main.Width - 12, nY + 5, main.Width - 11, nY + 5);
			oGraphics.DrawLine(oPen, main.Width - 13, nY + 6, main.Width - 12, nY + 6);
			oGraphics.DrawLine(oPen, main.Width - 11, nY + 6, main.Width - 10, nY + 6);
			oGraphics.DrawLine(oPen, main.Width - 14, nY + 7, main.Width - 13, nY + 7);
			oGraphics.DrawLine(oPen, main.Width - 10, nY + 7, main.Width - 9, nY + 7);
			oGraphics.DrawLine(oPen, main.Width - 15, nY + 8, main.Width - 14, nY + 8);
			oGraphics.DrawLine(oPen, main.Width - 9, nY + 8, main.Width - 8, nY + 8);

			oGraphics.DrawLine(oPen, main.Width - 12, nY + 8, main.Width - 11, nY + 8);
			oGraphics.DrawLine(oPen, main.Width - 13, nY + 9, main.Width - 12, nY + 9);
			oGraphics.DrawLine(oPen, main.Width - 11, nY + 9, main.Width - 10, nY + 9);
			oGraphics.DrawLine(oPen, main.Width - 14, nY + 10, main.Width - 13, nY + 10);
			oGraphics.DrawLine(oPen, main.Width - 10, nY + 10, main.Width - 9, nY + 10);
			oGraphics.DrawLine(oPen, main.Width - 15, nY + 11, main.Width - 14, nY + 11);
			oGraphics.DrawLine(oPen, main.Width - 9, nY + 11, main.Width - 8, nY + 11);
			oPen.Dispose();

			Rectangle oRect = new Rectangle(3, nY, main.Width - 6, 18);
			m_mapGroupBox2Rect.Add(oGroup, oRect);
			m_mapRect2GroupBox.Add(oRect, oGroup);

			nY += 5;

			foreach (ActionMenuItem oItem in oGroup.Items)
			{
				if (oItem.Selected == true)
				{	
					SolidBrush oInnerBrush = new SolidBrush(Color.FromArgb(0x80, 0x80, 0x80));
					oGraphics.FillRectangle(oInnerBrush, 2, nY + 17, main.Width - 3, 20);
					oInnerBrush.Color = Color.FromArgb(0xBF, 0xBF, 0xFF);					
					oGraphics.FillRectangle(oInnerBrush, 3, nY + 18, main.Width - 5, 18);
					oInnerBrush.Dispose();
				}

				if (oItem.Text == "-")
				{
					oPen = new Pen(m_oBorderColor);
					oPen.DashStyle = DashStyle.Dot;
					oGraphics.DrawLine(oPen, 5, nY + 18, main.Width - 5, nY + 18);
					oPen.Dispose();
					nY += 3;
				}
				else
				{
					oGraphics.DrawString(oItem.Text, Font, Brushes.Black, 5, nY + 20);

					oRect = new Rectangle(10, nY + 17, main.Width - 20, 20);
					m_mapItem2Rect.Add(oItem, oRect);
					m_mapRect2Item.Add(oRect, oItem);

					nY += 20;				
				}
			}

			nY += 23;
		}

		protected void DrawBackground(Graphics g, Rectangle rectWin)
		{ 
			Rectangle main = new Rectangle(0, 0, 
				rectWin.Width - 1 - m_aPosition[(int)0, (int)PaintItem.SW], 
				rectWin.Height - 1 - m_aPosition[(int)0, (int)PaintItem.SH]);
						
			// Calculate some common values
			int imageColWidth = m_aPosition[(int)0, (int)PaintItem.IGL] + 
				m_nImageWidth + 
				m_aPosition[(int)0, (int)PaintItem.IGR];

			int xStart = m_aPosition[(int)0, (int)PaintItem.BL];
			int yStart = m_aPosition[(int)0, (int)PaintItem.BT];
			int yHeight = main.Height - yStart - m_aPosition[(int)0, (int)PaintItem.BB] - 1;

			// Paint the main area background
			g.FillRectangle(m_oLLBrush, main);

			// Draw single line border around the main area
			using (Pen mainBorder = new Pen(m_oBorderColor))
			{
				g.DrawRectangle(mainBorder, main);
			}

			// Draw the first image column
			Rectangle imageRect = new Rectangle(xStart, yStart, imageColWidth, yHeight);

			g.FillRectangle(Brushes.White, imageRect);
			g.FillRectangle(m_oLBrush, imageRect);
				
			// Draw shadow around borders
			int rightLeft = main.Right + 1;
			int rightTop = main.Top + m_aPosition[(int)0, (int)PaintItem.SH];
			int rightBottom = main.Bottom + 1;
			int leftLeft = main.Left + m_aPosition[(int)0, (int)PaintItem.SW];

			DrawShadowHorizontal(g, leftLeft, rightBottom, rightLeft, m_aPosition[(int)0, (int)PaintItem.SH]);

			DrawShadowVertical(g, rightLeft, rightTop, m_aPosition[(int)0, (int)PaintItem.SW], rightBottom - rightTop - 1);								
		}

		protected void DrawShadowVertical(Graphics g, int left, int top, int width, int height)
		{
			if (m_bLayered)
			{
				Color extraColor = Color.FromArgb(64, 0, 0, 0);
				Color darkColor = Color.FromArgb(48, 0, 0, 0);
				Color lightColor = Color.FromArgb(0, 0, 0, 0);
            
				// Enough room for top and bottom shades?
				if (height >= m_nShadowLength)
				{
					using (LinearGradientBrush topBrush = new LinearGradientBrush(new Point(left - m_nShadowLength, top + m_nShadowLength), 
							  new Point(left + m_nShadowLength, top),
							  extraColor, lightColor))
					{
						// Draw top shade
						g.FillRectangle(topBrush, left, top, m_nShadowLength, m_nShadowLength);
                        
						top += m_nShadowLength;
						height -= m_nShadowLength;
					}
				}
               
				using (LinearGradientBrush middleBrush = new LinearGradientBrush(new Point(left, 0), 
						  new Point(left + width, 0),
						  darkColor, lightColor))
				{                                                                               
					// Draw middle shade
					g.FillRectangle(middleBrush, left, top, width, height + 1);
				}
			}
			else
			{
				using(SolidBrush shadowBrush = new SolidBrush(ControlPaint.Dark(m_oBackColor)))
					g.FillRectangle(shadowBrush, left, top, width, height + 1);
			}
		}

		protected void DrawShadowHorizontal(Graphics g, int left, int top, int width, int height)
		{
			if (m_bLayered)
			{
				Color extraColor = Color.FromArgb(64, 0, 0, 0);
				Color darkColor = Color.FromArgb(48, 0, 0, 0);
				Color lightColor = Color.FromArgb(0, 0, 0, 0);

				// Do we need to draw the right shadow?
				//if (op != Shadow.Left)
				{
					if (width >= m_nShadowLength)
					{
						try
						{
							g.DrawImageUnscaled(GetShadowCache(m_nShadowLength, height), left + width - m_nShadowLength, top);
						}
						catch
						{
							//	just to be on the safe side...
						}
						width -= m_nShadowLength;
					}
				}

				// Draw the remaining middle
				using (LinearGradientBrush middleBrush = new LinearGradientBrush(new Point(9999, top), 
						  new Point(9999, top + height),
						  darkColor, lightColor))
				{                                                                               
					// Draw middle shade
					g.FillRectangle(middleBrush, left, top, width, height);
				}
			}
			else
			{
				using(SolidBrush shadowBrush = new SolidBrush(ControlPaint.Dark(m_oBackColor)))
					g.FillRectangle(shadowBrush, left, top, width, height);
			}
		}
		#endregion

		#region helper functions

		public void Hide()
		{
			User32.ShowWindow(this.Handle, (short)ShowWindowStyles.SW_HIDE);
		}

		protected POINT MousePositionToClient(POINT point)
		{
			POINT localPoint;
			localPoint.x = point.x;
			localPoint.y = point.y;
			User32.ScreenToClient(this.Handle, ref localPoint);

			return localPoint;
		}

		protected POINT MousePositionToScreen(POINT point)
		{
			POINT localPoint;
			localPoint.x = point.x;
			localPoint.y = point.y;

			User32.ClientToScreen(this.Handle, ref localPoint);

			return localPoint;
		}

		protected POINT MousePositionToScreen(MSG msg)
		{
			POINT screenPos;
			screenPos.x = (short)((uint)msg.lParam & 0x0000FFFFU);
			screenPos.y = (short)(((uint)msg.lParam & 0xFFFF0000U) >> 16);

			// Convert the mouse position to screen coordinates, 
			// but not for non-client messages
			if ((msg.message != (int)Msgs.WM_NCLBUTTONUP) &&
				(msg.message != (int)Msgs.WM_NCMBUTTONUP) &&
				(msg.message != (int)Msgs.WM_NCRBUTTONUP) &&
				(msg.message != (int)Msgs.WM_NCXBUTTONUP) &&
				(msg.message != (int)Msgs.WM_NCLBUTTONDOWN) &&
				(msg.message != (int)Msgs.WM_NCMBUTTONDOWN) &&
				(msg.message != (int)Msgs.WM_NCRBUTTONDOWN) &&
				(msg.message != (int)Msgs.WM_NCXBUTTONDOWN))	
			{
				// Convert the mouse position to screen coordinates
				User32.ClientToScreen(msg.hwnd, ref screenPos);
			}

			return screenPos;
		}

		protected POINT MousePositionToScreen(Message msg)
		{
			POINT screenPos;
			screenPos.x = (short)((uint)msg.LParam & 0x0000FFFFU);
			screenPos.y = (short)(((uint)msg.LParam & 0xFFFF0000U) >> 16);

			// Convert the mouse position to screen coordinates, 
			// but not for non-client messages
			if ((msg.Msg != (int)Msgs.WM_NCLBUTTONUP) &&
				(msg.Msg != (int)Msgs.WM_NCMBUTTONUP) &&
				(msg.Msg != (int)Msgs.WM_NCRBUTTONUP) &&
				(msg.Msg != (int)Msgs.WM_NCXBUTTONUP) &&
				(msg.Msg != (int)Msgs.WM_NCLBUTTONDOWN) &&
				(msg.Msg != (int)Msgs.WM_NCMBUTTONDOWN) &&
				(msg.Msg != (int)Msgs.WM_NCRBUTTONDOWN) &&
				(msg.Msg != (int)Msgs.WM_NCXBUTTONDOWN))	
			{
				// Convert the mouse position to screen coordinates
				User32.ClientToScreen(msg.HWnd, ref screenPos);
			}

			return screenPos;
		}

		protected static Bitmap GetShadowCache(int width, int height)
		{
			// Do we already have a cached bitmap of the correct size?
			if ((m_nShadowCacheWidth == width) && (m_nShadowCacheHeight == height) && (m_oShadowCache != null))
				return m_oShadowCache;

			// Dispose of any previously cached bitmap	
			if (m_oShadowCache != null)
				m_oShadowCache.Dispose();
	
			// Create our new bitmap with 32bpp so we have an alpha channel
			Bitmap image = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            
			// We want direct access to the bits so we can change values
			BitmapData data = image.LockBits(new System.Drawing.Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
				
			unsafe
			{
				// Direct pointer to first line
				uint* pixptr = (uint*)(data.Scan0);
					
				// For each row
				for (int y = 0; y < height; y++)
				{
					int offset = data.Stride * y / 4;
                    
					// Fade each line as we go down
					int alphay = 64 * (height - y) / (height + 1);
						
					// For each column pixel
					for (int x = 0; x < width; x++)
					{
						// Fade each pixel as we go across
						int alphax = alphay * (width - x) / (width + 1);
						pixptr[offset+x] = (uint)alphax << 24;
					}
				}
			}
	
			image.UnlockBits(data);

			// Cache values for next time around	
			m_oShadowCache = image;
			m_nShadowCacheWidth = width;
			m_nShadowCacheHeight = height;
	
			return m_oShadowCache;
		}

		protected Color CalculateColor(Color front, Color back, int alpha)
		{
			// Use alpha blending to brigthen the colors but don't use it
			// directly. Instead derive an opaque color that we can use.
			Color frontColor = Color.FromArgb(255, front);
			Color backColor = Color.FromArgb(255, back);

			float frontRed = frontColor.R;
			float frontGreen = frontColor.G;
			float frontBlue = frontColor.B;
			float backRed = backColor.R;
			float backGreen = backColor.G;
			float backBlue = backColor.B;

			float fRed = (frontRed * alpha / 255) + backRed * ((float)(255-alpha)/255);
			float fGreen = (frontGreen * alpha / 255) + backGreen * ((float)(255-alpha)/255);
			float fBlue = (frontBlue * alpha / 255) + backBlue * ((float)(255-alpha)/255);

			byte newRed = (byte)fRed;
			byte newGreen = (byte)fGreen;
			byte newBlue = (byte)fBlue;

			return  Color.FromArgb(255, newRed, newGreen, newBlue);
		}
		#endregion

		#region event handling
		protected override void WndProc(ref Message m)
		{
			// Want to notice when the window is maximized
			switch(m.Msg)
			{					
				case (int)Msgs.WM_LBUTTONDOWN:
					m_oLastMouseDown = new Point(m.LParam.ToInt32());

					POINT scrP1 = new POINT();
					scrP1.x = m_oLastMouseDown.X;
					scrP1.y = m_oLastMouseDown.Y;
					
					scrP1 = MousePositionToScreen(scrP1);

					if ((scrP1.x >= m_oCurrentPoint.X) && (scrP1.x <= (m_oCurrentPoint.X + m_nWidth)) &&
						(scrP1.y >= m_oCurrentPoint.Y) && (scrP1.y <= (m_oCurrentPoint.Y + 20)))
					{
						m_bDrag = true;
					}
					
					break;

				case (int)Msgs.WM_LBUTTONUP:
					OnWM_LBUTTONUP();
					break;

				case (int)Msgs.WM_MOUSEACTIVATE:
					OnWM_MOUSEACTIVATE(ref m);
					break;

				case (int)Msgs.WM_PAINT:
					OnWM_PAINT(ref m);
					break;

				case (int)Msgs.WM_SETCURSOR:
					OnWM_SETCURSOR();
					break;

				case (int)Msgs.WM_NCHITTEST:
					if (!OnWM_NCHITTEST(ref m))
						base.WndProc(ref m);
					break;

				case (int)Msgs.WM_ACTIVATEAPP:
					Hide();
					base.WndProc(ref m);
					break;

				default:
					base.WndProc(ref m);
					break;
			}			
		}

		protected bool OnWM_LBUTTONUP(int nX, int nY)
		{
			m_oLastMouseDown = new Point(nX, nY);
			return OnWM_LBUTTONUP();
		}

		protected bool OnWM_LBUTTONUP()
		{
			try
			{				
				int localWidth = m_oCurrentSize.Width - m_aPosition[0, (int)PaintItem.SW];										

				Rectangle oCloseRect = new Rectangle(localWidth - 15, 3, 11, 10);
			
				if (oCloseRect.Contains(m_oLastMouseDown) == true)
				{
					Cursor.Current = System.Windows.Forms.Cursors.Arrow;

					Hide();

					return true;
				}

				foreach (Rectangle oRect in m_mapRect2GroupBox.Keys)
				{
					if (oRect.Contains(m_oLastMouseDown) == true)
					{
						ActionMenuGroup oGroup = m_mapRect2GroupBox[oRect] as ActionMenuGroup;
						oGroup.Expanded = !oGroup.Expanded;
											
						if (m_oSelectedItem != null)
						{
							m_oSelectedItem.Selected = false;
							m_oSelectedItem = null;
						}

						RecalcLayout();

						// get the max screen size. if the nX + Width or nY + Height is bigger than the area, 
						// them move it elsewhere and recalculate the position
						Screen oScreen = Screen.FromHandle(this.Handle);

						if (m_oCurrentPoint.X + m_oCurrentSize.Width > oScreen.Bounds.Width)
							m_oCurrentPoint.X = oScreen.Bounds.Width - m_oCurrentSize.Width;

						if (m_oCurrentPoint.Y + m_oCurrentSize.Height > oScreen.Bounds.Height)
							m_oCurrentPoint.Y = oScreen.Bounds.Height - m_oCurrentSize.Height;

						Invalidate();

						Cursor.Current = System.Windows.Forms.Cursors.Hand;

						return false;
					}
				}

				if (m_oSelectedItem != null)
				{
					Rectangle oRect = (Rectangle)m_mapItem2Rect[m_oSelectedItem];

					if (oRect.Contains(m_oLastMouseDown) == true)
					{
						this.OnItemClick(m_oSelectedItem);			

						Hide();

						return true;
					}
				}					
			}
			catch (Exception exp)
			{
                exp.ToString();
			}

			return false;
		}

		protected void OnWM_MOUSEMOVE(int xPos, int yPos)
		{
			// Convert from screen to client coordinates
			xPos -= m_oCurrentPoint.X;
			yPos -= m_oCurrentPoint.Y;		
	
			Point pos = new Point(xPos, yPos);

			// Yes, we know the mouse is over window
			m_bMouseOver = true;

			if (m_bDrag == true)
			{
				m_oCurrentPoint = new Point(m_oCurrentPoint.X - (m_oLastMousePos.X - xPos), m_oCurrentPoint.Y - (m_oLastMousePos.Y - yPos));

				// get the max screen size. if the nX + Width or nY + Height is bigger than the area, 
				// them move it elsewhere and recalculate the position
				Screen oScreen = Screen.FromHandle(this.Handle);

				if (m_oCurrentPoint.X + m_oCurrentSize.Width > oScreen.Bounds.Width)
					m_oCurrentPoint.X = oScreen.Bounds.Width - m_oCurrentSize.Width;

				if (m_oCurrentPoint.Y + m_oCurrentSize.Height > oScreen.Bounds.Height)
					m_oCurrentPoint.Y = oScreen.Bounds.Height - m_oCurrentSize.Height;

				RecalcLayout();
				Invalidate();

				return;
			}			
			
			int localWidth = m_oCurrentSize.Width - m_aPosition[0, (int)PaintItem.SW];			

			// Has mouse position really changed since last time?
			if (m_oLastMousePos != pos)
			{
				foreach (Rectangle oRect in m_mapRect2GroupBox.Keys)
				{
					if (oRect.Contains(pos) == true)
					{										
						ActionMenuGroup oGroup = m_mapRect2GroupBox[oRect] as ActionMenuGroup;

						if (m_oHighlightedGroup != oGroup)
						{
							m_oHighlightedGroup = oGroup;

							RecalcLayout();
							Invalidate();

							if (Cursor.Current != System.Windows.Forms.Cursors.Hand)
								Cursor.Current = System.Windows.Forms.Cursors.Hand;
						}

						return;
					}
				}

				foreach (Rectangle oRect in m_mapRect2Item.Keys)
				{
					if (oRect.Contains(pos) == true)
					{
						ActionMenuItem oItem = m_mapRect2Item[oRect] as ActionMenuItem;

						if (oItem.Selected == true)
							return;
					
						if (m_oSelectedItem != null)
							m_oSelectedItem.Selected = false;

						oItem.Selected = true;
						m_oSelectedItem = oItem;
						m_oHighlightedGroup = null;

						RecalcLayout();
						Invalidate();

						return;
					}
				}

				Rectangle oCloseRect = new Rectangle(localWidth - 15, 3, 11, 10);
				
				if (oCloseRect.Contains(pos) == true)
				{
					Cursor.Current = System.Windows.Forms.Cursors.Hand;

					return;
				}

				if (m_oSelectedItem != null)
				{
					m_oSelectedItem.Selected = false;
					m_oSelectedItem = null;

					RecalcLayout();
					Invalidate();				
				}

				if (m_oHighlightedGroup != null)
				{
					m_oHighlightedGroup = null;

					RecalcLayout();
					Invalidate();
				}		
	
				if (m_bExpandCollapseStrafeSelect == true)
				{
					m_bExpandCollapseStrafeSelect = false;

					RecalcLayout();
					Invalidate();
				}						
			}

			// Remember for next time around
			m_oLastMousePos = pos;
		}

		protected void OnWM_MOUSELEAVE()
		{
			// Reset flag so that next mouse move start monitor for mouse leave message
			m_bMouseOver = false;

			// No point having a last mouse position
			m_oLastMousePos = new Point(-1,-1);
		}		

		protected void OnWM_MOUSEACTIVATE(ref Message m)
		{
			// Do not allow the mouse down to activate the window, but eat 
			// the message as we still want the mouse down for processing
			m.Result = (IntPtr)MouseActivateFlags.MA_NOACTIVATE;
		}

		protected void OnWM_SETCURSOR()
		{						
		}			

		protected void OnWM_PAINT(ref Message m)
		{
			PAINTSTRUCT ps = new PAINTSTRUCT();

			// Have to call BeginPaint whenever processing a WM_PAINT message
			IntPtr hDC = User32.BeginPaint(m.HWnd, ref ps);

			RECT rectRaw = new RECT();

			// Grab the screen rectangle of the window
			User32.GetWindowRect(this.Handle, ref rectRaw);

			// Convert to a client size rectangle
			Rectangle rectWin = new Rectangle(0, 0, 
				rectRaw.right - rectRaw.left, 
				rectRaw.bottom - rectRaw.top);

			// Create a graphics object for drawing
			using (Graphics g = Graphics.FromHdc(hDC))
			{
				// Create bitmap for drawing onto
				Bitmap memoryBitmap = new Bitmap(rectWin.Width, rectWin.Height);

				using(Graphics h = Graphics.FromImage(memoryBitmap))
				{
					// Draw the background area
					DrawBackground(h, rectWin);

					// Draw the actual menu items
					DrawAllCommands(h);
				}

				// Blit bitmap onto the screen
				g.DrawImageUnscaled(memoryBitmap, 0, 0);
			}

			// Don't forget to end the paint operation!
			User32.EndPaint(m.HWnd, ref ps);
		}

		protected bool OnWM_NCHITTEST(ref Message m)
		{
			// Get mouse coordinates
			POINT screenPos;
			screenPos.x = (short)((uint)m.LParam & 0x0000FFFFU);
			screenPos.y = (short)(((uint)m.LParam & 0xFFFF0000U) >> 16);

			POINT popupPos;
			popupPos.x = m_oCurrentSize.Width - m_aPosition[0, (int)PaintItem.SW];
			popupPos.y = m_oCurrentSize.Height - m_aPosition[0, (int)PaintItem.SH];

			// Convert the mouse position to screen coordinates
			User32.ClientToScreen(this.Handle, ref popupPos);

			// Is the mouse in the shadow areas?
			if ((screenPos.x > popupPos.x) ||
				(screenPos.y > popupPos.y))
			{
				// Allow actions to occur to window beneath us
				m.Result = (IntPtr)HitTest.HTTRANSPARENT;

				return true;
			}

			return false;
		}
		#endregion
	}
}
