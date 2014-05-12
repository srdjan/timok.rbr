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

using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace PureComponents.TreeView.Internal
{
	/// <summary>
	/// The RECT structure defines the coordinates of the upper-left 
	/// and lower-right corners of a rectangle
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	[ToolboxItem(false)]	
	internal struct RECT
	{
		/// <summary>
		/// Specifies the x-coordinate of the upper-left corner of the RECT
		/// </summary>
		public int left;
			
		/// <summary>
		/// Specifies the y-coordinate of the upper-left corner of the RECT
		/// </summary>
		public int top;
			
		/// <summary>
		/// Specifies the x-coordinate of the lower-right corner of the RECT
		/// </summary>
		public int right;
			
		/// <summary>
		/// Specifies the y-coordinate of the lower-right corner of the RECT
		/// </summary>
		public int bottom;


		/// <summary>
		/// Creates a new RECT struct with the specified location and size
		/// </summary>
		/// <param name="left">The x-coordinate of the upper-left corner of the RECT</param>
		/// <param name="top">The y-coordinate of the upper-left corner of the RECT</param>
		/// <param name="right">The x-coordinate of the lower-right corner of the RECT</param>
		/// <param name="bottom">The y-coordinate of the lower-right corner of the RECT</param>
		public RECT(int left, int top, int right, int bottom)
		{
			this.left = left;
			this.top = top;
			this.right = right;
			this.bottom = bottom;
		}


		/// <summary>
		/// Creates a new RECT struct from the specified Rectangle
		/// </summary>
		/// <param name="rect">The Rectangle to create the RECT from</param>
		/// <returns>A RECT struct with the same location and size as 
		/// the specified Rectangle</returns>
		public static RECT FromRectangle(Rectangle rect)
		{
			return new RECT(rect.Left, rect.Top, rect.Right, rect.Bottom);
		}


		/// <summary>
		/// Creates a new RECT struct with the specified location and size
		/// </summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the RECT</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the RECT</param>
		/// <param name="width">The width of the RECT</param>
		/// <param name="height">The height of the RECT</param>
		/// <returns>A RECT struct with the specified location and size</returns>
		public static RECT FromXYWH(int x, int y, int width, int height)
		{
			return new RECT(x, y, x + width, y + height);
		}


		/// <summary>
		/// Returns a Rectangle with the same location and size as the RECT
		/// </summary>
		/// <returns>A Rectangle with the same location and size as the RECT</returns>
		public Rectangle ToRectangle()
		{
			return new Rectangle(this.left, this.top, this.right - this.left, this.bottom - this.top);
		}
	}
}
