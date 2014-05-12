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
using System.Drawing;

namespace PureComponents.TreeView
{
	internal enum NodeDropMode
	{
		DropUnder,
		DropInfront,
		DropAfter,
		DropLeft,
		DropRight
	}

	/// <summary>
	/// Implements the drag drop for nodes, hods the necessary dragdrop information
	/// and allows drawing of the drag drop
	/// </summary>
	internal class NodeDragDrop
	{
		#region private members
		private Node m_oDragNode;
		private Node m_oDropNode;
		private NodeDropMode m_eDropMode;
		#endregion

		#region construction
		/// <summary>
		/// Construction of the drag drop mode object
		/// </summary>
		/// <param name="oDragNode"></param>
		/// <param name="oDropNode"></param>
		/// <param name="eDropNode"></param>
		public NodeDragDrop(Node oDragNode, Node oDropNode, NodeDropMode eDropNode)
		{
			m_oDragNode = oDragNode;
			m_oDropNode = oDropNode;
			m_eDropMode = eDropNode;
		}
		#endregion

		#region implementation
		internal void Paint(TreeView oTreeView, Graphics oGraphics)
		{			
			if (m_eDropMode == NodeDropMode.DropInfront)
			{
				Rectangle oRect = oTreeView.GetNodeRect(m_oDropNode);

				int nRight = oRect.Right;

				if (oTreeView.Controls.Contains(oTreeView.m_oScrollBar) == true)
					nRight -= oTreeView.m_oScrollBar.Width;

				// dra the gray line
				oGraphics.DrawLine(Pens.Gray, 3, oRect.Top - 3, nRight - 3, oRect.Top - 3);				
				oGraphics.DrawLine(Pens.Gray, nRight - 3, oRect.Top - 6, nRight - 3, oRect.Top);
				oGraphics.DrawLine(Pens.Gray, nRight - 4, oRect.Top - 5, nRight - 4, oRect.Top - 1);
				oGraphics.DrawLine(Pens.Gray, nRight - 5, oRect.Top - 4, nRight - 5, oRect.Top - 2);

				// draw the box
				oGraphics.DrawLine(Pens.Black, oRect.Left - 11, oRect.Top - 4, oRect.Left - 6, oRect.Top - 4);
				oGraphics.DrawLine(Pens.Black, oRect.Left - 11, oRect.Top - 3, oRect.Left - 6, oRect.Top - 3);
				oGraphics.DrawLine(Pens.Black, oRect.Left - 11, oRect.Top - 2, oRect.Left - 6, oRect.Top - 2);							

				// draw the move right arrow				
				oGraphics.DrawLine(Pens.Gray, oRect.Left - 4, oRect.Top - 5, oRect.Left - 4, oRect.Top - 1);
				oGraphics.DrawLine(Pens.Gray, oRect.Left - 3, oRect.Top - 4, oRect.Left - 3, oRect.Top - 2);							
			}

			if (m_eDropMode == NodeDropMode.DropAfter)
			{
				Rectangle oRect = oTreeView.GetNodeRect(m_oDropNode);

				int nRight = oRect.Right;

				if (oTreeView.Controls.Contains(oTreeView.m_oScrollBar) == true)
					nRight -= oTreeView.m_oScrollBar.Width;

				oGraphics.DrawLine(Pens.Gray, 3, oRect.Bottom - 3, nRight - 3, oRect.Bottom - 3);
				oGraphics.DrawLine(Pens.Gray, nRight - 3, oRect.Bottom - 6, nRight - 3, oRect.Bottom);
				oGraphics.DrawLine(Pens.Gray, nRight - 4, oRect.Bottom - 5, nRight - 4, oRect.Bottom - 1);
				oGraphics.DrawLine(Pens.Gray, nRight - 5, oRect.Bottom - 4, nRight - 5, oRect.Bottom - 2);

				oGraphics.DrawLine(Pens.Black, oRect.Left - 11, oRect.Bottom - 4, oRect.Left - 6, oRect.Bottom - 4);
				oGraphics.DrawLine(Pens.Black, oRect.Left - 11, oRect.Bottom - 3, oRect.Left - 6, oRect.Bottom - 3);
				oGraphics.DrawLine(Pens.Black, oRect.Left - 11, oRect.Bottom - 2, oRect.Left - 6, oRect.Bottom - 2);

				// draw the move right arrow				
				oGraphics.DrawLine(Pens.Gray, oRect.Left - 4, oRect.Bottom - 5, oRect.Left - 4, oRect.Bottom - 1);
				oGraphics.DrawLine(Pens.Gray, oRect.Left - 3, oRect.Bottom - 4, oRect.Left - 3, oRect.Bottom - 2);							
			}			

			if (m_eDropMode == NodeDropMode.DropUnder)
			{
				Rectangle oRect = oTreeView.GetNodeRect(m_oDropNode);

				int nRight = oRect.Right;

				if (oTreeView.Controls.Contains(oTreeView.m_oScrollBar) == true)
					nRight -= oTreeView.m_oScrollBar.Width;

				oGraphics.DrawLine(Pens.Gray, 3, oRect.Bottom - 3, nRight - 3, oRect.Bottom - 3);
				oGraphics.DrawLine(Pens.Gray, nRight - 3, oRect.Bottom - 6, nRight - 3, oRect.Bottom);
				oGraphics.DrawLine(Pens.Gray, nRight - 4, oRect.Bottom - 5, nRight - 4, oRect.Bottom - 1);
				oGraphics.DrawLine(Pens.Gray, nRight - 5, oRect.Bottom - 4, nRight - 5, oRect.Bottom - 2);

				oGraphics.DrawLine(Pens.Black, oRect.Left + 4, oRect.Bottom - 4, oRect.Left + 9, oRect.Bottom - 4);
				oGraphics.DrawLine(Pens.Black, oRect.Left + 4, oRect.Bottom - 3, oRect.Left + 9, oRect.Bottom - 3);
				oGraphics.DrawLine(Pens.Black, oRect.Left + 4, oRect.Bottom - 2, oRect.Left + 9, oRect.Bottom - 2);

				// draw the move right arrow				
				oGraphics.DrawLine(Pens.Gray, oRect.Left + 1, oRect.Bottom - 4, oRect.Left + 1, oRect.Bottom - 2);							
				oGraphics.DrawLine(Pens.Gray, oRect.Left + 2, oRect.Bottom - 5, oRect.Left + 2, oRect.Bottom - 1);				
			}
		}
		#endregion

		#region properties
		internal Node DragNode
		{
			get
			{
				return m_oDragNode;
			}
		}

		internal Node DropNode
		{
			get
			{
				return m_oDropNode;
			}
		}

		internal NodeDropMode NodeDropMode
		{
			get
			{
				return m_eDropMode;
			}
		}
		#endregion
	}
}
