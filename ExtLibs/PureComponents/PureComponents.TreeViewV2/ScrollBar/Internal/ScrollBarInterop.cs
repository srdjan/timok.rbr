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
using System.Runtime.InteropServices;

namespace PureComponents.TreeView.Internal
{
	/// <summary>
	/// Receives dynamic-link library (DLL)-specific version information. 
	/// It is used with the DllGetVersion function
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	[ToolboxItem(false)]
	internal struct DLLVERSIONINFO
	{
		/// <summary>
		/// Size of the structure, in bytes. This member must be filled 
		/// in before calling the function
		/// </summary>
		public int cbSize;

		/// <summary>
		/// Major version of the DLL. If the DLL's version is 4.0.950, 
		/// this value will be 4
		/// </summary>
		public int dwMajorVersion;

		/// <summary>
		/// Minor version of the DLL. If the DLL's version is 4.0.950, 
		/// this value will be 0
		/// </summary>
		public int dwMinorVersion;

		/// <summary>
		/// Build number of the DLL. If the DLL's version is 4.0.950, 
		/// this value will be 950
		/// </summary>
		public int dwBuildNumber;

		/// <summary>
		/// Identifies the platform for which the DLL was built
		/// </summary>
		public int dwPlatformID;
	}

	/// <summary>
	/// A class that contains methods for P/Invoking the Win32 API
	/// </summary>
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	internal abstract class NativeMethods
	{
		/// <summary>
		/// Implemented by many of the Microsoft® Windows® Shell dynamic-link libraries 
		/// (DLLs) to allow applications to obtain DLL-specific version information
		/// </summary>
		/// <param name="pdvi">Pointer to a DLLVERSIONINFO structure that receives the 
		/// version information. The cbSize member must be filled in before calling 
		/// the function</param>
		/// <returns>Returns NOERROR if successful, or an OLE-defined error value otherwise</returns>
		[DllImport("Comctl32.dll", CharSet=CharSet.Auto, SetLastError=true)] 
		internal static extern int DllGetVersion(ref DLLVERSIONINFO pdvi);


		/// <summary>
		/// Tests if a visual style for the current application is active
		/// </summary>
		/// <returns>TRUE if a visual style is enabled, and windows with 
		/// visual styles applied should call OpenThemeData to start using 
		/// theme drawing services, FALSE otherwise</returns>
		[DllImport("UxTheme.dll", CharSet=CharSet.Auto, SetLastError=true)]
		internal static extern bool IsThemeActive();


		/// <summary>
		/// Reports whether the current application's user interface 
		/// displays using visual styles
		/// </summary>
		/// <returns>TRUE if the application has a visual style applied,
		/// FALSE otherwise</returns>
		[DllImport("UxTheme.dll", CharSet=CharSet.Auto, SetLastError=true)]
		internal static extern bool IsAppThemed();


		/// <summary>
		/// Opens the theme data for a window and its associated class
		/// </summary>
		/// <param name="hwnd">Handle of the window for which theme data 
		/// is required</param>
		/// <param name="pszClassList">Pointer to a string that contains 
		/// a semicolon-separated list of classes</param>
		/// <returns>OpenThemeData tries to match each class, one at a 
		/// time, to a class data section in the active theme. If a match 
		/// is found, an associated HTHEME handle is returned. If no match 
		/// is found NULL is returned</returns>
		[DllImport("UxTheme.dll", CharSet=CharSet.Auto, SetLastError=true)]
		internal static extern IntPtr OpenThemeData(IntPtr hwnd, [MarshalAs(UnmanagedType.LPTStr)] string pszClassList);


		/// <summary>
		/// Closes the theme data handle
		/// </summary>
		/// <param name="hTheme">Handle to a window's specified theme data. 
		/// Use OpenThemeData to create an HTHEME</param>
		/// <returns>Returns S_OK if successful, or an error value otherwise</returns>
		[DllImport("UxTheme.dll", CharSet=CharSet.Auto, SetLastError=true)]
		internal static extern int CloseThemeData(IntPtr hTheme);


		/// <summary>
		/// Draws the background image defined by the visual style for the 
		/// specified control part
		/// </summary>
		/// <param name="hTheme">Handle to a window's specified theme data. 
		/// Use OpenThemeData to create an HTHEME</param>
		/// <param name="hdc">Handle to a device context (HDC) used for 
		/// drawing the theme-defined background image</param>
		/// <param name="iPartId">Value of type int that specifies the part 
		/// to draw</param>
		/// <param name="iStateId">Value of type int that specifies the state 
		/// of the part to draw</param>
		/// <param name="pRect">Pointer to a RECT structure that contains the 
		/// rectangle, in logical coordinates, in which the background image 
		/// is drawn</param>
		/// <param name="pClipRect">Pointer to a RECT structure that contains 
		/// a clipping rectangle. This parameter may be set to NULL</param>
		/// <returns>Returns S_OK if successful, or an error value otherwise</returns>
		[DllImport("UxTheme.dll", CharSet=CharSet.Auto, SetLastError=true)]
		internal static extern int DrawThemeBackground(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, ref RECT pRect, ref RECT pClipRect);
	}
}
