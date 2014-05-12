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

using PureComponents.TreeView.Design;

namespace PureComponents.TreeView
{
	/// <summary>
	/// 
	/// </summary>
	internal class NodeTooltipWnd : NativeWindow
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

		protected bool m_bMouseOver = false;

		private static int m_nImageWidth = 16;
		private static int m_nShadowLength = 4;
		private static Bitmap m_oShadowCache = null;
		private static int m_nShadowCacheWidth = 0;
		private static int m_nShadowCacheHeight = 0;

		private bool m_bSupportsLayered = false;
		private Color m_oBorderColor = Color.FromArgb(0xAA, 0x8E, 0x7D);

		private NodeTooltipStyle m_Style;
		private string m_Tooltip;

		private bool m_bExitLoop = false;

		private int m_Interval = 1000;

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

		public NodeTooltipWnd()
		{
			m_bSupportsLayered = (OSFeature.Feature.GetVersionPresent(OSFeature.LayeredWindows) != null);					
		}

		#endregion

		#region native window implementation

		public void Show(int nX, int nY, NodeTooltipStyle style, string tooltip, int interval)
		{
			Show(nX, nY, style, tooltip, interval, true, true);
		}

		public void Show(int nX, int nY, NodeTooltipStyle style, string tooltip, int interval, bool animate, bool wrap)
		{
			m_Interval = interval;
			m_Style = style;
			m_Tooltip = tooltip;

			// Decide if we need layered windows
			m_bLayered = (m_bSupportsLayered);					

			CreateParams cp = new CreateParams();

			// Any old title will do as it will not be shown
			cp.Caption = "PureComponents.Node.TooltipWnd";			

			RecalcLayout(wrap);

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

			// Update the image for display, fade it up in the thread, and start the closing timer at the end of the thread function
			if (m_bLayered == true)
			{
				if (animate == true)
				{
					// Show the window without activating it (i.e. do not take focus)
					User32.ShowWindow(this.Handle, (short)ShowWindowStyles.SW_SHOWNOACTIVATE);	

					System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(this.UpdateLayeredWindow));
					thread.IsBackground = true;
					thread.Start();												
				}
				else
				{
					UpdateLayeredWindow(m_oCurrentPoint, m_oCurrentSize, 255, wrap);

					// Show the window without activating it (i.e. do not take focus)
					User32.ShowWindow(this.Handle, (short)ShowWindowStyles.SW_SHOWNOACTIVATE);

					// start the closing timer
					StartClosingTimer();

					System.Threading.Thread.Sleep(100);
				}
			}
		
			m_bExitLoop = false;

			while (!m_bExitLoop)
			{
				Application.DoEvents();
			}
		}

		protected void UpdateLayeredWindow()
		{
			int blend = 0;
			while (blend < 255)
			{
				if (blend == 254)
					blend = 255;

				UpdateLayeredWindow(m_oCurrentPoint, m_oCurrentSize, (byte)blend, true);
				blend += 3;
			}

			UpdateLayeredWindow(m_oCurrentPoint, m_oCurrentSize, 255, true);

			// start the closing timer
			StartClosingTimer();
		}

		protected void UpdateLayeredWindow(byte alpha, bool wrap)
		{
			UpdateLayeredWindow(m_oCurrentPoint, m_oCurrentSize, alpha, wrap);
		}

		protected void UpdateLayeredWindow(Point point, Size size, byte alpha, bool wrap)
		{
			// Create bitmap for drawing onto
			Bitmap memoryBitmap = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);

			using(Graphics g = Graphics.FromImage(memoryBitmap))
			{
				Rectangle area = new Rectangle(0, 0, size.Width, size.Height);
		
				// Draw the background area
				Draw(g, area, wrap);				

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

		protected void Draw(Graphics g, Rectangle rectWin, bool wrap)
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

			// paint the tooltip text
			SizeF textSize = g.MeasureString(m_Tooltip, m_Style.Font);

			if (wrap == true)
			{			
				if ((int)textSize.Width > 200)
					textSize = g.MeasureString(m_Tooltip, m_Style.Font, 200);
			}

			Brush brush = new SolidBrush(m_Style.ForeColor);
			g.DrawString(m_Tooltip, m_Style.Font, brush, new Rectangle(main.Left + 2, main.Top + 2, 
				(int)textSize.Width + 5, (int)textSize.Height + 7));
			brush.Dispose();
				
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


		private void StartClosingTimer()
		{
			System.Timers.Timer timer = new System.Timers.Timer(m_Interval);
			timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnClosingTimer);
			timer.Enabled = true;
		}

		private void OnClosingTimer(object sender, System.Timers.ElapsedEventArgs args)
		{
			System.Timers.Timer timer = sender as System.Timers.Timer;
			if (timer == null)
				return;

			timer.Enabled = false;
			timer.Dispose();

			this.Hide();
		}

		#endregion

		#region helper functions

		protected void RecalcLayout(bool wrap)
		{
			m_oBorderColor = m_Style.BorderColor;
			m_oBackColor = m_Style.BackColor;
			m_oControlLL = CalculateColor(SystemColors.Window, m_oBackColor, 220);
			m_oLBrush = new SolidBrush(CalculateColor(m_oBackColor, Color.White, 200));
			m_oEBrush = new SolidBrush(CalculateColor(m_oBackColor, Color.White, 150));
			m_oLLBrush = new SolidBrush(m_oControlLL);

			// measure the text
			Graphics gr = Graphics.FromHwnd(this.Handle);
			SizeF textSize = gr.MeasureString(m_Tooltip, m_Style.Font);

			if (wrap == true)
			{
				if ((int)textSize.Width > 200)
					textSize = gr.MeasureString(m_Tooltip, m_Style.Font, 200);

				m_oCurrentSize = new Size((int)textSize.Width + 14, (int)textSize.Height + 9);
			}
			else
			{
				m_oCurrentSize = new Size((int)textSize.Width + 14, (int)textSize.Height + 9);
			}
		}

		public void Hide()
		{					
			User32.ShowWindow(this.Handle, (short)ShowWindowStyles.SW_HIDE);
			this.ReleaseHandle();					

			m_bExitLoop = true;					
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
					base.WndProc(ref m);
					
					break;				

				default:
					base.WndProc(ref m);
					break;
			}			
		}
		
		#endregion
	}
}
