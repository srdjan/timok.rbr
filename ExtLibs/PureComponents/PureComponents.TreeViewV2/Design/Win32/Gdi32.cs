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
using System.Runtime.InteropServices;

namespace PureComponents.TreeView.Design
{
    internal class Gdi32
    {
        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        internal static extern int CombineRgn(IntPtr dest, IntPtr src1, IntPtr src2, int flags);

        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        internal static extern IntPtr CreateRectRgnIndirect(ref RECT rect); 

        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        internal static extern int GetClipBox(IntPtr hDC, ref RECT rectBox); 

        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        internal static extern int SelectClipRgn(IntPtr hDC, IntPtr hRgn); 

        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        internal static extern IntPtr CreateBrushIndirect(ref LOGBRUSH brush); 

        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        internal static extern bool PatBlt(IntPtr hDC, int x, int y, int width, int height, uint flags); 

        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        internal static extern IntPtr DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        internal static extern bool DeleteDC(IntPtr hDC);

        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        internal static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        internal static extern IntPtr CreateCompatibleDC(IntPtr hDC);
    }
}