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
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PureComponents.TreeView.Internal
{
	/// <summary>
	/// A class that contains methods for drawing Windows XP themed Control parts
	/// </summary>
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	internal abstract class ThemeFactory
	{
		#region Constructor

		/// <summary>
		/// Initializes a new instance of the ThemeManager class with default settings
		/// </summary>
		protected ThemeFactory()
		{
		}

		#endregion

		#region Methods

		#region Painting

		#region ScrollBar

		/// <summary>
		/// 
		/// </summary>
		/// <param name="g"></param>
		/// <param name="sbRect"></param>
		/// <param name="state"></param>
		/// <param name="part"></param>
		public static void DrawScrollBar(Graphics g, Rectangle sbRect,
		                                 ScrollBarStates state, ScrollBarParts part)
		{
			ThemeFactory.DrawScrollBar(g, sbRect, sbRect, state, part);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="g"></param>
		/// <param name="sbRect"></param>
		/// <param name="clipRect"></param>
		/// <param name="state"></param>
		/// <param name="part"></param>
		public static void DrawScrollBar(Graphics g, Rectangle sbRect, Rectangle clipRect,
			ScrollBarStates state, ScrollBarParts part)
		{
			if (g == null || sbRect.Width <= 0 || sbRect.Height <= 0 ||
				clipRect.Width <= 0 || clipRect.Height <= 0)
			{
				return;
			}

			ThemeFactory.DrawThemeBackground(g, "SCROLLBAR", (int) part, (int) state, sbRect, clipRect);
		}

		#endregion

		#region Theme Background

		/// <summary>
		/// Draws the background image defined by the visual style for the specified control part
		/// </summary>
		/// <param name="g">The Graphics to draw on</param>
		/// <param name="windowClass">The class of the part to draw</param>
		/// <param name="part">The part to draw</param>
		/// <param name="partState">The state of the part to draw</param>
		/// <param name="drawRect">The Rectangle in which the part is drawn</param>
		public static void DrawThemeBackground(Graphics g, string windowClass, int part, int partState, Rectangle drawRect)
		{
			ThemeFactory.DrawThemeBackground(g, windowClass, part, partState, drawRect, drawRect);
		}


		/// <summary>
		/// Draws the background image defined by the visual style for the specified control part
		/// </summary>
		/// <param name="g">The Graphics to draw on</param>
		/// <param name="windowClass">The class of the part to draw</param>
		/// <param name="part">The part to draw</param>
		/// <param name="partState">The state of the part to draw</param>
		/// <param name="drawRect">The Rectangle in which the part is drawn</param>
		/// <param name="clipRect">The Rectangle that represents the clipping area for the part</param>
		public static void DrawThemeBackground(Graphics g, string windowClass, int part, int partState, 
			Rectangle drawRect, Rectangle clipRect)
		{
			if (g == null || drawRect.Width <= 0 || drawRect.Height <= 0 || clipRect.Width <= 0 || clipRect.Height <= 0)
			{
				return;
			}

			// open theme data
			IntPtr hTheme = IntPtr.Zero;
			hTheme = NativeMethods.OpenThemeData(hTheme, windowClass);

			// make sure we have a valid handle
			if (hTheme != IntPtr.Zero)
			{
				// get a graphics object the UxTheme can draw into
				IntPtr hdc = g.GetHdc();

				// get the draw and clipping rectangles
				RECT dRect = RECT.FromRectangle(drawRect);
				RECT cRect = RECT.FromRectangle(clipRect);

				// draw the themed background
				NativeMethods.DrawThemeBackground(hTheme, hdc, part, partState, ref dRect, ref cRect);

				// clean up resources
				g.ReleaseHdc(hdc);
			}

			// close the theme handle
			NativeMethods.CloseThemeData(hTheme);
		}

		#endregion

		#endregion

		#endregion

		#region Properties

		/// <summary>
		/// Gets whether Visual Styles are supported by the system
		/// </summary>
		public static bool VisualStylesSupported
		{
			get { return OSFeature.Feature.IsPresent(OSFeature.Themes); }
		}


		/// <summary>
		/// Gets whether Visual Styles are enabled for the application
		/// </summary>
		public static bool VisualStylesEnabled
		{
			get
			{
				if (VisualStylesSupported)
				{
					// are themes enabled
					if (NativeMethods.IsThemeActive() && NativeMethods.IsAppThemed())
					{
						return true;//GetComctlVersion().Major >= 6;
					}
				}

				return false;
			}
		}


		/// <summary>
		/// Returns a Version object that contains information about the verion 
		/// of the CommonControls that the application is using
		/// </summary>
		/// <returns>A Version object that contains information about the verion 
		/// of the CommonControls that the application is using</returns>
		private static Version GetComctlVersion()
		{
			DLLVERSIONINFO comctlVersion = new DLLVERSIONINFO();
			comctlVersion.cbSize = Marshal.SizeOf(typeof (DLLVERSIONINFO));

			if (NativeMethods.DllGetVersion(ref comctlVersion) == 0)
			{
				return new Version(comctlVersion.dwMajorVersion, comctlVersion.dwMinorVersion, comctlVersion.dwBuildNumber);
			}

			return new Version();
		}

		#endregion
	}
}