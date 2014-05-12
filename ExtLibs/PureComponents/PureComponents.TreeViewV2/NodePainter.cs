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
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing.Text;
using System.Windows.Forms;

namespace PureComponents.TreeView
{
	/// <summary>
	/// Implements the painting functionality for nodes
	/// </summary>
	internal sealed class NodePainter
	{
		#region private members
		private const int KSideBarWidth = 25;
		private const int KDefItemHeight = 25;
		private int Width;

		private SolidBrush m_oBrush = new SolidBrush(Color.Black);
		private TreeView m_oTreeView;
		
		private int m_nNodeOrder = 0;

		#endregion

		#region construction
		/// <summary>
		/// Protected construction
		/// </summary>
		public NodePainter(TreeView oTreeView)
		{
			m_oTreeView = oTreeView;					
		}
		#endregion		

		#region Node painting
		/// <summary>
		/// Paint the node in the expanded mode
		/// </summary>
		/// <param name="oNode"></param>
		/// <param name="nX"></param>
		/// <param name="nY"></param>
		/// <param name="oGraphics"></param>
		private void PaintNodeExpanded(Node oNode, Graphics oGraphics, ref int nX, ref int nY, int Width, 
			ref Hashtable m_mapSubItemToRect, ref Hashtable m_mapRectToSubItem, ref Hashtable m_mapRectToItemBox, 
			ref Hashtable m_mapItemBoxToRect, ref Hashtable m_mapRectToItemCheck, ref Hashtable m_mapItemCheckToRect, 
			ref Hashtable m_mapRectToItemFlag, ref Hashtable m_mapItemFlagToRect)
		{
			int nXAdd = 0;
			//if (oNode.GetShowPlusMinus() == false)
			//	nXAdd = 15;
			
			if (oNode.GetCheckBoxes() == true && oNode.CheckBoxVisible == true)
				nXAdd -= 18;
			
			int nodeSpace = 0;

			if (oNode.IndentLevel != 0)
				nodeSpace = m_oTreeView.Style.NodeSpaceHorizontal;

			int height = oNode.GetHeight(oGraphics);

			Rectangle oNodeRect = new Rectangle(nX - nXAdd +nodeSpace, nY, 
				Width - nX - 5 + nXAdd - nodeSpace, height);
			
			if (m_oTreeView.Style.FullRowSelect == false && oNode.Panel == null)
			{
				oNodeRect = new Rectangle(nX - nXAdd + nodeSpace, nY, 
					oNode.GetWidth(oGraphics), height);
			}

			// render the background
			if (oNode.BackgroundStyle != null && oNode.BackgroundStyle.Visible == true)
			{
				int width = oNode.GetTreeView().Width - oNodeRect.Left - nXAdd 
					- m_oTreeView.GetScrollBarWidth() - oNode.IndentLevel - 4;

				AreaPainter.PaintRectangle(oGraphics, oNodeRect.Left + 1 + nXAdd, oNodeRect.Top - 1,
					width, oNode.GetFullHeight(oGraphics) - 1, 
					oNode.BackgroundStyle.FillStyle,
					oNode.BackgroundStyle.BackColor, oNode.BackgroundStyle.FadeColor,
					BorderStyle.None, Color.White);
			}			

			oNode.NodeOrder = m_nNodeOrder;
			m_nNodeOrder ++;

			// get the order of the actual group. calculate the bottom nY of the group, if the current nY is bigger than
			// this calculated nY, then skip the drawing and donot draw the object		
			int nMaxY = oNode.TreeView.Height;
			int nGroupY = -height;

			// create the item rect and add it to maps
			try {m_mapSubItemToRect.Add(oNode, oNodeRect);}	catch{}
			try {m_mapRectToSubItem.Add(oNodeRect, oNode);}	catch{}

			oNode.Top = oNodeRect.Top + oNodeRect.Height + 1;
			oNode.Left = oNodeRect.Left;		
			oNode.Width = oNodeRect.Width - m_oTreeView.GetScrollBarWidth();

			SizeF oTextSize = oGraphics.MeasureString(oNode.GetText(), oNode.GetFont(), oNode.GetTreeView().GetDrawWidth() - nX - 8);
			if (m_oTreeView.Multiline == false)
				oTextSize = oGraphics.MeasureString(oNode.GetText(), oNode.GetFont());

			oNode.Top = nY + (int)oTextSize.Height + 2;

			if ((nY >= nGroupY && nY < nMaxY) || oNode.TreeView.IsDesignMode == true)
			{
				// draw flag
				PaintNodeFlag(oNode, oGraphics, oNodeRect, ref m_mapRectToItemFlag, ref m_mapItemFlagToRect);

				// draw selected background
				PaintNodeBackground(oNode, oGraphics, oNodeRect);

				// draw design selection
				PaintNodeBackgroundDesignSelection(oNode, oGraphics, oNodeRect);

				// draw expand/collapse boxes
				if (oNode.GetShowPlusMinus() == true)
					PaintExpandBox(oNode, oGraphics, nX + nodeSpace, nY, ref m_mapRectToItemBox, ref m_mapItemBoxToRect);

				// draw lines between nodes
				PaintNodeLines(oNode, oGraphics, nX + nodeSpace, nY, m_mapItemBoxToRect, m_mapSubItemToRect);
					
				// draw item header
				PaintNodeText(oNode, oGraphics, nX + nodeSpace, nY, ref m_mapRectToItemCheck, ref m_mapItemCheckToRect);				

				// check the design time highlight			
				if (oNode.DesignHighlighted)
				{
					if (oNode.GetCheckBoxes() == false)
					{					
						if (m_oTreeView.Style.FullRowSelect == true || oNode.Panel != null)
						{
							oGraphics.DrawRectangle(Pens.Black, oNodeRect.Left + 2, oNodeRect.Top - 1, 
								Width - oNodeRect.Left - 7 - m_oTreeView.GetScrollBarWidth(), oNodeRect.Height - 2);
						}
						else
						{
							oGraphics.DrawRectangle(Pens.Black, oNodeRect.Left + 2, oNodeRect.Top - 1, 
								(int)oNodeRect.Width, oNodeRect.Height - 2);
						}
					}
					else
					{
						if (oNode.CheckBoxVisible == true)
						{
							if (m_oTreeView.Style.FullRowSelect == true || oNode.Panel != null)
							{
								oGraphics.DrawRectangle(Pens.Black, oNodeRect.Left, oNodeRect.Top - 1, 
									Width - oNodeRect.Left - 7 - m_oTreeView.GetScrollBarWidth() + 2, oNodeRect.Height - 2);
							}
							else
							{
								oGraphics.DrawRectangle(Pens.Black, oNodeRect.Left, oNodeRect.Top - 1, 
									(int)oNodeRect.Width, oNodeRect.Height - 2);
							}
						}
					}
				}
			}	
		
			// for the nodes being drawn before the visible area, but still needed for the 
			// lines painting
			if (nY < nGroupY && oNode.TreeView.IsDesignMode == false)
			{
				// draw expand/collapse boxes
				PaintExpandBox(oNode, oGraphics, nX + nodeSpace, nY, ref m_mapRectToItemBox, ref m_mapItemBoxToRect);
			}

			// for the nodes being at the position under the drawing visible area, draw the lines
			// so it is visible that there are some other child nodes even they are not visible
			if (nY > nMaxY && oNode.TreeView.IsDesignMode == false)
			{
				// draw expand/collapse boxes
				PaintExpandBox(oNode, oGraphics, nX + nodeSpace, nY, ref m_mapRectToItemBox, ref m_mapItemBoxToRect);

				// draw lines between nodes
				PaintNodeLines(oNode, oGraphics, nX + nodeSpace, nY, m_mapItemBoxToRect, m_mapSubItemToRect);
			}

			nY += height;	
	
			nY += m_oTreeView.Style.NodeSpaceVertical;

			// draw all subitems
			if (oNode.TreeView.Sorted == true)
			{
				ArrayList aList = new ArrayList();

				foreach (Node oSubNode in oNode.Nodes)
					aList.Add(oSubNode);

				aList.Sort();

				for (int nNode = 0; nNode < aList.Count; nNode ++)
				{
					Node oSubNode = aList[nNode] as Node;

					if (oSubNode == null || oSubNode.Visible == false)
						continue;

					int nOldNX = nX;
					nX += 12;					

					nX += nodeSpace;

					PaintNode(oSubNode, oGraphics, ref nX, ref nY, Width, ref m_mapSubItemToRect, 
						ref m_mapRectToSubItem, ref m_mapRectToItemBox, ref m_mapItemBoxToRect,
						ref m_mapRectToItemCheck, ref m_mapItemCheckToRect, 
						ref m_mapRectToItemFlag, ref m_mapItemFlagToRect);

					nX = nOldNX;
				}
			}
			else			
			{
				// sort the nodes by the m_nCollectionOrder
//				SortedList aList = new SortedList();

				foreach (Node oSubNode in oNode.Nodes)
				{
					if (oSubNode.m_nCollectionOrder == -1)
						oSubNode.m_nCollectionOrder = oSubNode.Index;

//					try
//					{
//						aList.Add(oSubNode.m_nCollectionOrder, oSubNode);
//					}
//					catch
//					{
//						aList.Add(oSubNode.Index, oSubNode);
//					}
				}

//				for (int nNode = 0; nNode < aList.Count; nNode ++)
				Node [] aList = oNode.Nodes.GetNodesCollectionSorted();
				for (int nNode = 0; nNode < aList.Length; nNode ++)
				{
					//Node oSubNode = aList.GetByIndex(nNode) as Node;
					Node oSubNode = aList[nNode] as Node;

					if (oSubNode.Visible == false)
						continue;

					int nOldNX = nX;
					nX += 12;		
			
					nX += nodeSpace;

					PaintNode(oSubNode, oGraphics, ref nX, ref nY, Width, ref m_mapSubItemToRect, 
						ref m_mapRectToSubItem, ref m_mapRectToItemBox, ref m_mapItemBoxToRect,
						ref m_mapRectToItemCheck, ref m_mapItemCheckToRect, ref m_mapRectToItemFlag, ref m_mapItemFlagToRect);

					nX = nOldNX;
				}
			}
		}

		/// <summary>
		/// Paints the line from the node to its parent
		/// </summary>
		/// <param name="oNode"></param>
		/// <param name="oGraphics"></param>
		private void PaintNodeFlashLine(Node oNode, Graphics oGraphics)
		{
			if (oNode.Parent == null)
				return;

			int nNXAdd = 0;

			//if (oNode.GetShowPlusMinus() == false)
			//	nNXAdd = -1;

			Pen oPen = null;
			int nAlpha = 255;

			oPen = new Pen(Color.FromArgb(nAlpha, oNode.GetLineColor()), 1);
			oPen.DashStyle = TreeView.GetLineStyle(oNode.GetLineStyle());

			Rectangle nodeRect = m_oTreeView.GetExpandBoxRect(oNode);
			Rectangle parentRect = m_oTreeView.GetExpandBoxRect(oNode.Parent);

			int nX = nodeRect.Right + 2;
			int nY = nodeRect.Top - 2;

			oGraphics.DrawLine(oPen, nX - 19 + nNXAdd, nY + 7, nX - 14 + nNXAdd, nY + 7);
			oGraphics.DrawLine(oPen, nX - 19 + nNXAdd, parentRect.Y + 12, nX - 19 + nNXAdd, nY + 7);

			if (oNode.Parent.Parent != null)
				PaintNodeFlashLine(oNode.Parent, oGraphics);
		}

		/// <summary>
		/// Paint the node's flash lines
		/// </summary>
		/// <param name="oNode"></param>
		/// <param name="oGraphics"></param>
		internal void PaintNodeFlashLines(Node oNode, Graphics oGraphics)
		{
			if (oNode == null || oNode.Parent == null)
				return;

			if (oNode.GetShowLines() == false)
				return;

			if (oNode.Parent != null)
				PaintNodeFlashLine(oNode, oGraphics);			
		}
			
		/// <summary>
		/// Paint the node in the closed mode
		/// </summary>
		/// <param name="oNode"></param>
		/// <param name="nX"></param>
		/// <param name="nY"></param>
		/// <param name="oGraphics"></param>
		internal void PaintNode(Node oNode, Graphics oGraphics, ref int nX, ref int nY, int nWidth,
			ref Hashtable m_mapSubItemToRect, ref Hashtable m_mapRectToSubItem, ref Hashtable m_mapRectToItemBox, 
			ref Hashtable m_mapItemBoxToRect, ref Hashtable m_mapRectToItemCheck, ref Hashtable m_mapItemCheckToRect,
			ref Hashtable m_mapRectToItemFlag, ref Hashtable m_mapItemFlagToRect)
		{
			if (oNode == null)
				return;

			oNode._TreeView = m_oTreeView;
			
			if (oNode.NodeStyle != null)
			{
				oNode.NodeStyle.TreeView = this.m_oTreeView;
				oNode.NodeStyle.CheckBoxStyle.TreeView = this.m_oTreeView;
				oNode.NodeStyle.ExpandBoxStyle.TreeView = this.m_oTreeView;
			}
			
			Width = nWidth;

			if (oNode.IsExpanded == true)
			{
				PaintNodeExpanded(oNode, oGraphics, ref nX, ref nY, Width, ref m_mapSubItemToRect, ref m_mapRectToSubItem, 
					ref m_mapRectToItemBox, ref m_mapItemBoxToRect, ref m_mapRectToItemCheck, ref m_mapItemCheckToRect, 
					ref m_mapRectToItemFlag, ref m_mapItemFlagToRect);

				return;
			}			

			int nXAdd = 0;
			//if (oNode.GetShowPlusMinus() == false)
			//	nXAdd = 15;

			if (oNode.GetCheckBoxes() == true && oNode.CheckBoxVisible == true)
				nXAdd -= 18;	
		
			int nodeSpace = 0;

			if (oNode.IndentLevel != 0)
				nodeSpace = m_oTreeView.Style.NodeSpaceHorizontal;

			int height = oNode.GetHeight(oGraphics);

			Rectangle oNodeRect = new Rectangle(nX - nXAdd + nodeSpace, nY, 
				Width - nX - 5 + nXAdd - nodeSpace, height);
			
			if (m_oTreeView.Style.FullRowSelect == false && oNode.Panel == null)
			{
				oNodeRect = new Rectangle(nX - nXAdd + nodeSpace, nY, 
					oNode.GetWidth(oGraphics), height);
			}

			oNode.NodeOrder = m_nNodeOrder;
			m_nNodeOrder ++;
			
			// get the order of the actual group. calculate the bottom nY of the group, if the current nY is bigger than
			// this calculated nY, then skip the drawing and donot draw the object
			int nMaxY = oNode.TreeView.Height;
			int nGroupY = -height;

			// render the background
			if (oNode.BackgroundStyle != null && oNode.BackgroundStyle.Visible == true)
			{
				int width = oNode.GetTreeView().Width - oNodeRect.Left - nXAdd 
					- m_oTreeView.GetScrollBarWidth() - oNode.IndentLevel - 4;

				AreaPainter.PaintRectangle(oGraphics, oNodeRect.Left + 1 + nXAdd, oNodeRect.Top - 1,
					width, oNode.GetFullHeight(oGraphics) - 1, 
					oNode.BackgroundStyle.FillStyle,
					oNode.BackgroundStyle.BackColor, oNode.BackgroundStyle.FadeColor,
					BorderStyle.None, Color.White);
			}

			if (nY > nGroupY && nY < nMaxY)
			{			
				// draw flag
				PaintNodeFlag(oNode, oGraphics, oNodeRect, ref m_mapRectToItemFlag, ref m_mapItemFlagToRect);

				// render selected background
				PaintNodeBackground(oNode, oGraphics, oNodeRect);

				// draw design selection
				PaintNodeBackgroundDesignSelection(oNode, oGraphics, oNodeRect);

				// draw expand/collapse boxes
				if (oNode.GetShowPlusMinus() == true)
					PaintExpandBox(oNode, oGraphics, nX + nodeSpace, nY, ref m_mapRectToItemBox, ref m_mapItemBoxToRect);

				// draw lines between nodes
				PaintNodeLines(oNode, oGraphics, nX + nodeSpace, nY, m_mapItemBoxToRect, m_mapSubItemToRect);

				// draw item header and the subitems indicator
				PaintNodeText(oNode, oGraphics, nX + nodeSpace, nY, ref m_mapRectToItemCheck, ref m_mapItemCheckToRect);			

				// check the design time highlight			
				if (oNode.DesignHighlighted)
				{
					if (oNode.GetCheckBoxes() == false)
					{					
						if (m_oTreeView.Style.FullRowSelect == true || oNode.Panel != null)
						{
							oGraphics.DrawRectangle(Pens.Black, oNodeRect.Left + 2, oNodeRect.Top - 1, 
								Width - oNodeRect.Left - 7 - m_oTreeView.GetScrollBarWidth(), oNodeRect.Height - 2);
						}
						else
						{
							oGraphics.DrawRectangle(Pens.Black, oNodeRect.Left + 2, oNodeRect.Top - 1, 
								(int)oNodeRect.Width, oNodeRect.Height - 2);
						}
					}
					else
					{
						if (oNode.CheckBoxVisible == true)
						{
							if (m_oTreeView.Style.FullRowSelect == true || oNode.Panel != null)
							{
								oGraphics.DrawRectangle(Pens.Black, oNodeRect.Left, oNodeRect.Top - 1, 
									Width - oNodeRect.Left - 7 - m_oTreeView.GetScrollBarWidth() + 2, oNodeRect.Height - 2);
							}
							else
							{
								oGraphics.DrawRectangle(Pens.Black, oNodeRect.Left, oNodeRect.Top - 1, 
									(int)oNodeRect.Width, oNodeRect.Height - 2);
							}
						}
					}					
				}

				// create the item rect and add it to maps
				try{m_mapSubItemToRect.Add(oNode, oNodeRect);}catch{}
				try{m_mapRectToSubItem.Add(oNodeRect, oNode);}catch{}

				oNode.Top = oNodeRect.Top + oNodeRect.Height;
				oNode.Left = oNodeRect.Left;
				oNode.Width = oNodeRect.Width - m_oTreeView.GetScrollBarWidth();
			}

			// for the nodes being drawn before the visible area, but still needed for the 
			// lines painting
			if (nY <= nGroupY)
			{
				// draw expand/collapse boxes
				PaintExpandBox(oNode, oGraphics, nX + nodeSpace, nY, ref m_mapRectToItemBox, ref m_mapItemBoxToRect);
			}

			// for the nodes being at the position under the drawing visible area, draw the lines
			// so it is visible that there are some other child nodes even they are not visible
			if (nY >= nMaxY)
			{
				// draw expand/collapse boxes
				PaintExpandBox(oNode, oGraphics, nX + nodeSpace, nY, ref m_mapRectToItemBox, ref m_mapItemBoxToRect);

				// draw lines between nodes
				PaintNodeLines(oNode, oGraphics, nX + nodeSpace, nY, m_mapItemBoxToRect, m_mapSubItemToRect);
			}

			nY += height;

			nY += m_oTreeView.Style.NodeSpaceVertical;
		}
		#endregion

		#region painting helpers - Node
		/// <summary>
		/// Paint the background of the node
		/// </summary>
		/// <param name="oNode"></param>
		/// <param name="oNodeRect"></param>
		private void PaintNodeBackground(Node oNode, Graphics oGraphics, Rectangle oNodeRect)
		{
			// if the node is selected node, then draw the selection
			if (oNode.IsSelected == true && m_oTreeView.HideSelection == false)
			{								
				int nScrollWidth = 2;
				int nAdd = 0;

				if (oNode.GetCheckBoxes() == true && oNode.CheckBoxVisible == true)
				{
					//if (oNode.GetShowPlusMinus() == false)
					//	nAdd = -2;
					//else
						nAdd = -1;
				}
				
				nScrollWidth += m_oTreeView.GetScrollBarWidth();

				m_oBrush.Color = oNode.GetSelectedBorderColor();

				// if full row select
				if (m_oTreeView.Style.FullRowSelect == true || oNode.Panel != null)
				{
					oGraphics.FillRectangle(m_oBrush, oNodeRect.X + 1 + nAdd, oNodeRect.Y - 1, 
						Width - (oNodeRect.X + 3) - nScrollWidth - nAdd, oNodeRect.Height - 1);

					m_oBrush.Color = oNode.GetSelectedBackColor();

					if (oNode.GetSelectedFillStyle() == FillStyle.Flat)
					{
						oGraphics.FillRectangle(m_oBrush, oNodeRect.X + 2 + nAdd, oNodeRect.Y, 
							Width - (oNodeRect.X + 5) - nScrollWidth - nAdd, oNodeRect.Height - 3);
					}
					else 
					if (oNode.GetSelectedFillStyle() == FillStyle.HorizontalFading)
					{
						AreaPainter.PaintRectangle(oGraphics, oNodeRect.X + 2 + nAdd, oNodeRect.Y, 
							Width - (oNodeRect.X + 5) - nScrollWidth - nAdd, oNodeRect.Height - 3,
							FillStyle.HorizontalFading, m_oBrush.Color, Color.White, BorderStyle.None, 
							Color.Transparent);
					}
					else
					if (oNode.GetSelectedFillStyle() == FillStyle.VerticalFading)
					{
						AreaPainter.PaintRectangle(oGraphics, oNodeRect.X + 2 + nAdd, oNodeRect.Y, 
							Width - (oNodeRect.X + 5) - nScrollWidth - nAdd, oNodeRect.Height - 3,
							FillStyle.VerticalFading, m_oBrush.Color, Color.White, BorderStyle.None, 
							Color.Transparent);
					}
					else
					if (oNode.GetSelectedFillStyle() == FillStyle.DiagonalBackward)
					{
						AreaPainter.PaintRectangle(oGraphics, oNodeRect.X + 2 + nAdd, oNodeRect.Y, 
							Width - (oNodeRect.X + 5) - nScrollWidth - nAdd, oNodeRect.Height - 3,
							FillStyle.DiagonalBackward, m_oBrush.Color, Color.White, BorderStyle.None, 
							Color.Transparent);
					}
					else
					if (oNode.GetSelectedFillStyle() == FillStyle.DiagonalForward)
					{
						AreaPainter.PaintRectangle(oGraphics, oNodeRect.X + 2 + nAdd, oNodeRect.Y, 
							Width - (oNodeRect.X + 5) - nScrollWidth - nAdd, oNodeRect.Height - 3,
							FillStyle.DiagonalForward, m_oBrush.Color, Color.White, BorderStyle.None, 
							Color.Transparent);
					}
					else
					if (oNode.GetSelectedFillStyle() == FillStyle.VistaFading)
					{
						AreaPainter.PaintRectangle(oGraphics, oNodeRect.X + 2 + nAdd, oNodeRect.Y, 
							Width - (oNodeRect.X + 5) - nScrollWidth - nAdd, oNodeRect.Height - 3,
							FillStyle.VistaFading, m_oBrush.Color, Color.White, BorderStyle.None, 
							Color.Transparent);
					}
					else
					if (oNode.GetSelectedFillStyle() == FillStyle.VerticalCentreFading)
					{
						AreaPainter.PaintRectangle(oGraphics, oNodeRect.X + 2 + nAdd, oNodeRect.Y, 
							Width - (oNodeRect.X + 5) - nScrollWidth - nAdd, oNodeRect.Height - 3,
							FillStyle.VerticalCentreFading, m_oBrush.Color, Color.White, BorderStyle.None, 
							Color.Transparent);
					}
				}
				else
				{
					oGraphics.FillRectangle(m_oBrush, oNodeRect.X + 1 + nAdd, oNodeRect.Y - 1, 
						(int)oNodeRect.Width, oNodeRect.Height - 1);

					m_oBrush.Color = oNode.GetSelectedBackColor();

					if (oNode.GetSelectedFillStyle() == FillStyle.Flat)
					{
						oGraphics.FillRectangle(m_oBrush, oNodeRect.X + 2 + nAdd, oNodeRect.Y, 
							(int)oNodeRect.Width - 2, oNodeRect.Height - 3);
					}
					else
					if (oNode.GetSelectedFillStyle() == FillStyle.HorizontalFading)
					{
						AreaPainter.PaintRectangle(oGraphics,  oNodeRect.X + 2 + nAdd, oNodeRect.Y, 
							(int)oNodeRect.Width - 2, oNodeRect.Height - 3,
							FillStyle.HorizontalFading, m_oBrush.Color, Color.White, BorderStyle.None, 
							Color.Transparent);
					}
					else
					if (oNode.GetSelectedFillStyle() == FillStyle.VerticalFading)
					{
						AreaPainter.PaintRectangle(oGraphics,  oNodeRect.X + 2 + nAdd, oNodeRect.Y, 
							(int)oNodeRect.Width - 2, oNodeRect.Height - 3,
							FillStyle.VerticalFading, m_oBrush.Color, Color.White, BorderStyle.None, 
							Color.Transparent);
					}
					else
					if (oNode.GetSelectedFillStyle() == FillStyle.DiagonalBackward)
					{
						AreaPainter.PaintRectangle(oGraphics,  oNodeRect.X + 2 + nAdd, oNodeRect.Y, 
							(int)oNodeRect.Width - 2, oNodeRect.Height - 3,
							FillStyle.DiagonalBackward, m_oBrush.Color, Color.White, BorderStyle.None, 
							Color.Transparent);
					}
					else
					if (oNode.GetSelectedFillStyle() == FillStyle.DiagonalForward)
					{
						AreaPainter.PaintRectangle(oGraphics,  oNodeRect.X + 2 + nAdd, oNodeRect.Y, 
							(int)oNodeRect.Width - 2, oNodeRect.Height - 3,
							FillStyle.DiagonalForward, m_oBrush.Color, Color.White, BorderStyle.None, 
							Color.Transparent);
					}
					else
					if (oNode.GetSelectedFillStyle() == FillStyle.VistaFading)
					{
						AreaPainter.PaintRectangle(oGraphics,  oNodeRect.X + 2 + nAdd, oNodeRect.Y, 
							(int)oNodeRect.Width - 2, oNodeRect.Height - 3,
							FillStyle.VistaFading, m_oBrush.Color, Color.White, BorderStyle.None, 
							Color.Transparent);
					}
					else
					if (oNode.GetSelectedFillStyle() == FillStyle.VerticalCentreFading)
					{
						AreaPainter.PaintRectangle(oGraphics,  oNodeRect.X + 2 + nAdd, oNodeRect.Y, 
							(int)oNodeRect.Width - 2, oNodeRect.Height - 3,
							FillStyle.VerticalCentreFading, m_oBrush.Color, Color.White, BorderStyle.None, 
							Color.Transparent);
					}
				}
			}

			// draw the focus rectangle
			if (oNode.IsSelected == true && oNode.TreeView.m_bUserFocus == true && oNode.TreeView.FocusNode == oNode)
			{
				int nScrollWidth = 2;
				int nAdd = 0;

				nScrollWidth += m_oTreeView.GetScrollBarWidth();

				if (oNode.GetCheckBoxes() == true && oNode.CheckBoxVisible == true)
				{
					//if (oNode.GetShowPlusMinus() == false)
					//	nAdd = -2;
					//else
						nAdd = -1;
				}

				Pen oRectPen = new Pen(Color.Black, 1);
				oRectPen.DashStyle = DashStyle.Dot;

				if (m_oTreeView.Style.FullRowSelect == true || oNode.Panel != null)
				{
					oGraphics.DrawRectangle(oRectPen, oNodeRect.X + 2 + nAdd, oNodeRect.Y, 
						Width - (oNodeRect.X + 6) - nScrollWidth - nAdd, oNodeRect.Height - 4);	
				}
				else
				{
					oGraphics.DrawRectangle(oRectPen, oNodeRect.X + 2 + nAdd, oNodeRect.Y, 
						(int)oNodeRect.Width - 3, oNodeRect.Height - 4);	
				}

				oRectPen.Dispose();
			}

			// if not selected
			if (oNode.IsSelected == false)
			{			
				// when draging the mouse over nodes, test if the node is being highlighted
				if (oNode == oNode.TreeView.HighlightedNode)
				{
					int nScrollWidth = 2;
				
					nScrollWidth += m_oTreeView.GetScrollBarWidth();

					m_oBrush.Color = oNode.GetHighlightedBackColor();

					if (m_oTreeView.Style.FullRowSelect == true || oNode.Panel != null)
					{
						oGraphics.FillRectangle(m_oBrush, oNodeRect.X + 1, oNodeRect.Y - 1, 
							Width - (oNodeRect.X + 3) - nScrollWidth, oNodeRect.Height - 1);
					}
					else
					{
						oGraphics.FillRectangle(m_oBrush, oNodeRect.X + 1, oNodeRect.Y - 1, 
							(int)oNodeRect.Width, oNodeRect.Height - 1);
					}
				}
			}
		}

		/// <summary>
		/// Paint the background in the design mode
		/// </summary>
		private void PaintNodeBackgroundDesignSelection(Node oNode, Graphics oGraphics, Rectangle oNodeRect)
		{
			if (oNode.DesignSelected == true)
			{
				int nScrollWidth = 2;

				nScrollWidth += m_oTreeView.GetScrollBarWidth();

				Pen oPen = new Pen(Color.Black, 1);

				if (oNode.GetCheckBoxes() == true && oNode.CheckBoxVisible == true)
				{				
					if (m_oTreeView.Style.FullRowSelect || oNode.Panel != null)
					{
						oGraphics.DrawRectangle(oPen, oNodeRect.X - 1, oNodeRect.Y - 2, 
							Width - (oNodeRect.X + 3) - nScrollWidth, oNodeRect.Height);

						oPen.DashStyle = DashStyle.Dot;
						oGraphics.DrawRectangle(oPen, oNodeRect.X, oNodeRect.Y - 1, 
							Width - (oNodeRect.X + 3) - nScrollWidth, oNodeRect.Height - 2);
					}
					else
					{
						oGraphics.DrawRectangle(oPen, oNodeRect.X - 1, oNodeRect.Y - 2, 
							(int)oNodeRect.Width, oNodeRect.Height);

						oPen.DashStyle = DashStyle.Dot;
						oGraphics.DrawRectangle(oPen, oNodeRect.X, oNodeRect.Y - 1, 
							(int)oNodeRect.Width - 2, oNodeRect.Height - 2);
					}
				}
				else
				{
					if (m_oTreeView.Style.FullRowSelect == true || oNode.Panel != null)
					{
						oGraphics.DrawRectangle(oPen, oNodeRect.X + 1, oNodeRect.Y - 2, 
							Width - (oNodeRect.X + 3) - nScrollWidth, oNodeRect.Height);

						oPen.DashStyle = DashStyle.Dot;
						oGraphics.DrawRectangle(oPen, oNodeRect.X + 2, oNodeRect.Y - 1, 
							Width - (oNodeRect.X + 3) - nScrollWidth, oNodeRect.Height - 2);
					}
					else
					{
						oGraphics.DrawRectangle(oPen, oNodeRect.X + 1, oNodeRect.Y - 2, 
							(int)oNodeRect.Width, oNodeRect.Height);

						oPen.DashStyle = DashStyle.Dot;
						oGraphics.DrawRectangle(oPen, oNodeRect.X + 2, oNodeRect.Y - 1, 
							(int)oNodeRect.Width - 2, oNodeRect.Height - 2);
					}
				}

				oPen.Dispose();
			}
		}

		/// <summary>
		/// Paint the node's lines
		/// </summary>		
		private void PaintNodeLines(Node oNode, Graphics oGraphics, int nX, int nY, Hashtable mapItemBoxToRect, Hashtable m_mapSubItemToRect)
		{
			if (oNode.GetShowLines() == true && oNode.Parent != null)
			{
				int nAlpha = oNode.TreeView.GetNodeAlpha(oNode);
				int nNXAdd = 0;

				//if (oNode.GetShowPlusMinus() == false)
				//	nNXAdd = -1;

				if (oNode.GetTreeView().Style.ShowPlusMinus && oNode.ShowPlusMinus == false)
					nNXAdd = 0;

				if (oNode.IndentLevel != 0)
					nNXAdd -= m_oTreeView.Style.NodeSpaceHorizontal;

				Pen oPen = null;
				if (oNode.Flash == true)
					nAlpha = 255;

				oPen = new Pen(Color.FromArgb(nAlpha, oNode.GetLineColor()), 1);
				oPen.DashStyle = TreeView.GetLineStyle(oNode.GetLineStyle());

				if (oNode.m_nCollectionOrder != 0 && oNode.Flash == false && oNode.IsInSelectedPath() == false)
				{
					Node oPrevNode = null;
					
					foreach (Node oTestNode in oNode.Collection)
					{
						if (oTestNode.m_nCollectionOrder == oNode.m_nCollectionOrder - 1)
						{
							oPrevNode = oTestNode;
							break;
						}
					}

					if (oPrevNode != null)
					{
						// prev node rect
						object oRect = mapItemBoxToRect[oPrevNode];

						if (oNode.GetTreeView().Sorted == false)
							oRect = mapItemBoxToRect[oNode.Parent];

						if (oRect != null)
						{
							Rectangle oParentRect = (Rectangle)oRect;

							oGraphics.DrawLine(oPen, nX - 19 + nNXAdd, nY + 7, nX - 14 + nNXAdd + m_oTreeView.Style.NodeSpaceHorizontal, nY + 7);

							if (oNode.GetTreeView().Sorted == true)
								oParentRect.Y -= 6;

							if ((oNode.IsInSelectedPath() && oNode.GetHighlightSelectedPath() == true) || oNode.IsLastInCollection() || oNode.GetTreeView().Sorted == true)
								oGraphics.DrawLine(oPen, nX - 19 + nNXAdd, oParentRect.Y + 13, nX - 19 + nNXAdd, nY + 7);
						}
					}
					else
					{
						// parent rect
						object oRect = mapItemBoxToRect[oNode.Parent];

						if (oRect != null)
						{
							Rectangle oParentRect = (Rectangle)oRect;

							oGraphics.DrawLine(oPen, nX - 19 + nNXAdd, nY + 7, nX - 14 + nNXAdd + m_oTreeView.Style.NodeSpaceHorizontal, nY + 7);

							if ((oNode.IsInSelectedPath() && oNode.GetHighlightSelectedPath() == true) || oNode.IsLastInCollection() || oNode.GetTreeView().Sorted == true)
								oGraphics.DrawLine(oPen, nX - 19 + nNXAdd, oParentRect.Y + 13, nX - 19 + nNXAdd, nY + 7);
						}
					}
				}
				else
				{
					// parent rect
					object oRect = mapItemBoxToRect[oNode.Parent];

					if (oRect != null)
					{
						Rectangle oParentRect = (Rectangle)oRect;

						oGraphics.DrawLine(oPen, nX - 19 + nNXAdd, nY + 7, nX - 14 + nNXAdd + m_oTreeView.Style.NodeSpaceHorizontal, nY + 7);

						if (oNode.GetShowPlusMinus() == false)
						{					
							oRect = m_mapSubItemToRect[oNode.Parent];

							if (oRect == null)
							{
								if ((oNode.IsInSelectedPath() && oNode.GetHighlightSelectedPath() == true) || oNode.IsLastInCollection() || oNode.GetTreeView().Sorted == true)
									oGraphics.DrawLine(oPen, nX - 19 + nNXAdd, oParentRect.Y + 13, nX - 19 + nNXAdd, nY + 7);
							}
							else
							{			
								oParentRect =  (Rectangle)oRect;	
								if ((oNode.IsInSelectedPath() && oNode.GetHighlightSelectedPath() == true) || oNode.IsLastInCollection() || oNode.GetTreeView().Sorted == true)
									oGraphics.DrawLine(oPen, nX - 19 + nNXAdd, oParentRect.Y + 13, nX - 19 + nNXAdd, nY + 7);
							}
						}
						else
						{
							if ((oNode.IsInSelectedPath() && oNode.GetHighlightSelectedPath() == true) || oNode.IsLastInCollection() || oNode.GetTreeView().Sorted == true)
								oGraphics.DrawLine(oPen, nX - 19 + nNXAdd, oParentRect.Y + 13, nX - 19 + nNXAdd, nY + 7);						
						}
					}
				}

				oPen.Dispose();
			}
		}

		/// <summary>
		/// Paint the Node's flag
		/// </summary>
		private void PaintNodeFlag(Node oNode, Graphics oGraphics, Rectangle oNodeRect, 
			ref Hashtable m_mapRectToItemFlag, ref Hashtable m_mapItemFlagToRect)
		{
			int x = 2 + m_oTreeView.m_nHScroll;

			Rectangle rect = new Rectangle(x, oNodeRect.Top + 2, 12, 12);

			if (m_mapItemFlagToRect.Contains(oNode))
				m_mapItemFlagToRect.Remove(oNode);
			if (m_mapRectToItemFlag.Contains(rect))
				m_mapRectToItemFlag.Remove(rect);
			m_mapItemFlagToRect.Add(oNode, rect);
			m_mapRectToItemFlag.Add(rect, oNode);

			if (oNode.FlagVisible == false || oNode.Flag == null || oNode.TreeView.Flags == false)
				return;

			int nAlpha = oNode.TreeView.GetNodeAlpha(oNode);
			if (oNode.Flash == true)
				nAlpha = 255;			

			#region flag
			if (oNode.Flag.FlagStyle == NodeFlagStyle.Flag)
			{			
				Pen pen = new Pen(nAlpha == 255 ? oNode.Flag.Color : Color.FromArgb(128, oNode.Flag.Color));

				pen.Color = Color.FromArgb(nAlpha == 255 ? 128 : 48, pen.Color);
				oGraphics.DrawLine(pen, new Point(x + 3, oNodeRect.Top + 4), new Point(x + 7, oNodeRect.Top + 4));
				oGraphics.DrawLine(pen, new Point(x + 4, oNodeRect.Top + 5), new Point(x + 7, oNodeRect.Top + 5));
				oGraphics.DrawLine(pen, new Point(x + 4, oNodeRect.Top + 6), new Point(x + 7, oNodeRect.Top + 6));			
				oGraphics.DrawLine(pen, new Point(x + 5, oNodeRect.Top + 7), new Point(x + 5, oNodeRect.Top + 8));

				pen.Color = nAlpha == 255 ? oNode.Flag.Color : Color.FromArgb(92, oNode.Flag.Color);
				oGraphics.DrawLine(pen, new Point(x + 2, oNodeRect.Top + 3), new Point(x + 6, oNodeRect.Top + 12));
				oGraphics.DrawLine(pen, new Point(x + 2, oNodeRect.Top + 3), new Point(x + 3, oNodeRect.Top + 3));
				oGraphics.DrawLine(pen, new Point(x + 4, oNodeRect.Top + 4), new Point(x + 5, oNodeRect.Top + 4));
				oGraphics.DrawLine(pen, new Point(x + 6, oNodeRect.Top + 3), new Point(x + 7, oNodeRect.Top + 3));
				oGraphics.DrawLine(pen, new Point(x + 7, oNodeRect.Top + 4), new Point(x + 11, oNodeRect.Top + 4));
				oGraphics.DrawLine(pen, new Point(x + 7, oNodeRect.Top + 5), new Point(x + 10, oNodeRect.Top + 5));
				oGraphics.DrawLine(pen, new Point(x + 8, oNodeRect.Top + 6), new Point(x + 9, oNodeRect.Top + 6));
				oGraphics.DrawLine(pen, new Point(x + 7, oNodeRect.Top + 7), new Point(x + 8, oNodeRect.Top + 7));
				oGraphics.DrawLine(pen, new Point(x + 6, oNodeRect.Top + 6), new Point(x + 6, oNodeRect.Top + 8));
				oGraphics.DrawLine(pen, new Point(x + 5, oNodeRect.Top + 6), new Point(x + 5, oNodeRect.Top + 5));			

				pen.Dispose();
				pen = null;
			}
			#endregion

			#region arrow
			if (oNode.Flag.FlagStyle == NodeFlagStyle.Arrow)
			{			
				Pen pen = new Pen(nAlpha == 255 ? oNode.Flag.Color : Color.FromArgb(128, oNode.Flag.Color));
				
				oGraphics.FillPolygon(new SolidBrush(Color.FromArgb(nAlpha == 255 ? 148 : 64, oNode.Flag.Color)), 
					new Point [] { 
									 new Point(x + 7, oNodeRect.Top + 2), new Point(x + 7, oNodeRect.Top + 5),
									 new Point(x + 7, oNodeRect.Top + 5), new Point(x + 2, oNodeRect.Top + 5),
									 new Point(x + 2, oNodeRect.Top + 5), new Point(x + 2, oNodeRect.Top + 9),
									 new Point(x + 2, oNodeRect.Top + 9), new Point(x + 7, oNodeRect.Top + 9),
									 new Point(x + 7, oNodeRect.Top + 9), new Point(x + 7, oNodeRect.Top + 12),
									 new Point(x + 7, oNodeRect.Top + 12), new Point(x + 12, oNodeRect.Top + 7),
									 new Point(x + 12, oNodeRect.Top + 7), new Point(x + 7, oNodeRect.Top + 2)
								 }
					);
				
				oGraphics.DrawLine(pen, new Point(x + 7, oNodeRect.Top + 2), new Point(x + 7, oNodeRect.Top + 5));
				oGraphics.DrawLine(pen, new Point(x + 7, oNodeRect.Top + 5), new Point(x + 2, oNodeRect.Top + 5));				
				oGraphics.DrawLine(pen, new Point(x + 2, oNodeRect.Top + 5), new Point(x + 2, oNodeRect.Top + 9));
				oGraphics.DrawLine(pen, new Point(x + 2, oNodeRect.Top + 9), new Point(x + 7, oNodeRect.Top + 9));
				oGraphics.DrawLine(pen, new Point(x + 7, oNodeRect.Top + 9), new Point(x + 7, oNodeRect.Top + 12));
				oGraphics.DrawLine(pen, new Point(x + 7, oNodeRect.Top + 12), new Point(x + 12, oNodeRect.Top + 7));
				oGraphics.DrawLine(pen, new Point(x + 12, oNodeRect.Top + 7), new Point(x + 7, oNodeRect.Top + 2));

				pen.Dispose();
				pen = null;
			}
			#endregion

			#region exclamation
			if (oNode.Flag.FlagStyle == NodeFlagStyle.Exclamation)
			{			
				Pen pen = new Pen(nAlpha == 255 ? oNode.Flag.Color : Color.FromArgb(128, oNode.Flag.Color));
								
				oGraphics.FillRectangle(new SolidBrush(Color.FromArgb(nAlpha == 255 ? 164 : 82, oNode.Flag.Color)), x + 5, oNodeRect.Top + 2, 4, 4);
				pen.Color = Color.FromArgb(nAlpha == 255 ? 164 : 82, oNode.Flag.Color);
				oGraphics.DrawLine(pen, new Point(x + 6, oNodeRect.Top + 6), new Point(x + 6, oNodeRect.Top + 9));
				pen.Color = nAlpha == 255 ? oNode.Flag.Color : Color.FromArgb(128, oNode.Flag.Color);
				oGraphics.DrawLine(pen, new Point(x + 5, oNodeRect.Top + 1), new Point(x + 7, oNodeRect.Top + 1));
				oGraphics.DrawLine(pen, new Point(x + 4, oNodeRect.Top + 2), new Point(x + 4, oNodeRect.Top + 5));
				oGraphics.DrawLine(pen, new Point(x + 8, oNodeRect.Top + 2), new Point(x + 8, oNodeRect.Top + 5));
				oGraphics.DrawLine(pen, new Point(x + 5, oNodeRect.Top + 6), new Point(x + 5, oNodeRect.Top + 9));
				oGraphics.DrawLine(pen, new Point(x + 7, oNodeRect.Top + 6), new Point(x + 7, oNodeRect.Top + 9));
				oGraphics.DrawLine(pen, new Point(x + 6, oNodeRect.Top + 10), new Point(x + 7, oNodeRect.Top + 9));

				pen.Color = Color.FromArgb(nAlpha == 255 ? 164 : 82, oNode.Flag.Color);
				oGraphics.DrawLine(pen, new Point(x + 6, oNodeRect.Top + 12), new Point(x + 6, oNodeRect.Top + 13));
				pen.Color = nAlpha == 255 ? oNode.Flag.Color : Color.FromArgb(128, oNode.Flag.Color);
				oGraphics.DrawLine(pen, new Point(x + 6, oNodeRect.Top + 12), new Point(x + 7, oNodeRect.Top + 13));
				oGraphics.DrawLine(pen, new Point(x + 5, oNodeRect.Top + 13), new Point(x + 6, oNodeRect.Top + 14));

				pen.Dispose();
				pen = null;
			}
			#endregion

			#region point
			if (oNode.Flag.FlagStyle == NodeFlagStyle.Point)
			{			
				Pen pen = new Pen(nAlpha == 255 ? oNode.Flag.Color : Color.FromArgb(128, oNode.Flag.Color));
								
				oGraphics.FillPolygon(new SolidBrush(Color.FromArgb(nAlpha == 255 ? 148 : 74, oNode.Flag.Color)), 
					new Point [] { 
									 new Point(x + 4, oNodeRect.Top + 3), new Point(x + 8, oNodeRect.Top + 3),
									 new Point(x + 10, oNodeRect.Top + 5), new Point(x + 10, oNodeRect.Top + 9),
									 new Point(x + 8, oNodeRect.Top + 11), new Point(x + 4, oNodeRect.Top + 11),
									 new Point(x + 2, oNodeRect.Top + 9), new Point(x + 2, oNodeRect.Top + 5)
								 }
					);

				oGraphics.DrawPolygon(pen, 
					new Point [] { 
									 new Point(x + 4, oNodeRect.Top + 3), new Point(x + 8, oNodeRect.Top + 3),
									 new Point(x + 10, oNodeRect.Top + 5), new Point(x + 10, oNodeRect.Top + 9),
									 new Point(x + 8, oNodeRect.Top + 11), new Point(x + 4, oNodeRect.Top + 11),
									 new Point(x + 2, oNodeRect.Top + 9), new Point(x + 2, oNodeRect.Top + 5)
								 }
					);

				pen.Dispose();
				pen = null;
			}
			#endregion

			#region diamond
			if (oNode.Flag.FlagStyle == NodeFlagStyle.Diamond)
			{			
				Pen pen = new Pen(nAlpha == 255 ? oNode.Flag.Color : Color.FromArgb(128, oNode.Flag.Color));
								
				oGraphics.FillPolygon(new SolidBrush(Color.FromArgb(nAlpha == 255 ? 148 : 74, oNode.Flag.Color)), 
					new Point [] { 
									 new Point(x + 2, oNodeRect.Top + 7), new Point(x + 6, oNodeRect.Top + 3),
									 new Point(x + 10, oNodeRect.Top + 7), new Point(x + 6, oNodeRect.Top + 11)
								 }
					);

				oGraphics.DrawPolygon(pen, 
					new Point [] { 
									 new Point(x + 2, oNodeRect.Top + 7), new Point(x + 6, oNodeRect.Top + 3),
									 new Point(x + 10, oNodeRect.Top + 7), new Point(x + 6, oNodeRect.Top + 11)
								 }
					);

				pen.Dispose();
				pen = null;
			}
			#endregion

			#region arrow down
			if (oNode.Flag.FlagStyle == NodeFlagStyle.ArrowDown)
			{			
				Pen pen = new Pen(nAlpha == 255 ? oNode.Flag.Color : Color.FromArgb(128, oNode.Flag.Color));

				pen.Color = Color.FromArgb(nAlpha == 255 ? 128 : 48, pen.Color);
				oGraphics.DrawLine(pen, new Point(x + 7, oNodeRect.Top + 3), new Point(x + 7, oNodeRect.Top + 11));
				oGraphics.DrawLine(pen, new Point(x + 6, oNodeRect.Top + 6), new Point(x + 6, oNodeRect.Top + 9));
				oGraphics.DrawLine(pen, new Point(x + 8, oNodeRect.Top + 6), new Point(x + 8, oNodeRect.Top + 9));							

				pen.Color = nAlpha == 255 ? oNode.Flag.Color : Color.FromArgb(92, oNode.Flag.Color);
				oGraphics.DrawLine(pen, new Point(x + 6, oNodeRect.Top + 2), new Point(x + 6, oNodeRect.Top + 5));
				oGraphics.DrawLine(pen, new Point(x + 6, oNodeRect.Top + 2), new Point(x + 8, oNodeRect.Top + 2));
				oGraphics.DrawLine(pen, new Point(x + 8, oNodeRect.Top + 2), new Point(x + 8, oNodeRect.Top + 5));

				oGraphics.DrawLine(pen, new Point(x + 4, oNodeRect.Top + 6), new Point(x + 5, oNodeRect.Top + 6));
				oGraphics.DrawLine(pen, new Point(x + 9, oNodeRect.Top + 6), new Point(x + 10, oNodeRect.Top + 6));

				oGraphics.DrawLine(pen, new Point(x + 5, oNodeRect.Top + 6), new Point(x + 5, oNodeRect.Top + 8));
				oGraphics.DrawLine(pen, new Point(x + 9, oNodeRect.Top + 6), new Point(x + 9, oNodeRect.Top + 8));

				oGraphics.DrawLine(pen, new Point(x + 6, oNodeRect.Top + 9), new Point(x + 6, oNodeRect.Top + 10));
				oGraphics.DrawLine(pen, new Point(x + 8, oNodeRect.Top + 9), new Point(x + 8, oNodeRect.Top + 10));

				oGraphics.DrawLine(pen, new Point(x + 6, oNodeRect.Top + 10), new Point(x + 7, oNodeRect.Top + 11));

				pen.Dispose();
				pen = null;
			}
			#endregion

			#region arrow up
			if (oNode.Flag.FlagStyle == NodeFlagStyle.ArrowUp)
			{			
				Pen pen = new Pen(nAlpha == 255 ? oNode.Flag.Color : Color.FromArgb(128, oNode.Flag.Color));

				pen.Color = Color.FromArgb(nAlpha == 255 ? 128 : 48, pen.Color);
				oGraphics.DrawLine(pen, new Point(x + 7, oNodeRect.Top + 11), new Point(x + 7, oNodeRect.Top + 3));
				oGraphics.DrawLine(pen, new Point(x + 6, oNodeRect.Top + 8), new Point(x + 6, oNodeRect.Top + 5));
				oGraphics.DrawLine(pen, new Point(x + 8, oNodeRect.Top + 8), new Point(x + 8, oNodeRect.Top + 5));							

				pen.Color = nAlpha == 255 ? oNode.Flag.Color : Color.FromArgb(92, oNode.Flag.Color);
				oGraphics.DrawLine(pen, new Point(x + 6, oNodeRect.Top + 12), new Point(x + 6, oNodeRect.Top + 9));
				oGraphics.DrawLine(pen, new Point(x + 6, oNodeRect.Top + 12), new Point(x + 8, oNodeRect.Top + 12));
				oGraphics.DrawLine(pen, new Point(x + 8, oNodeRect.Top + 12), new Point(x + 8, oNodeRect.Top + 9));

				oGraphics.DrawLine(pen, new Point(x + 4, oNodeRect.Top + 8), new Point(x + 5, oNodeRect.Top + 8));
				oGraphics.DrawLine(pen, new Point(x + 9, oNodeRect.Top + 8), new Point(x + 10, oNodeRect.Top + 8));

				oGraphics.DrawLine(pen, new Point(x + 5, oNodeRect.Top + 8), new Point(x + 5, oNodeRect.Top + 6));
				oGraphics.DrawLine(pen, new Point(x + 9, oNodeRect.Top + 8), new Point(x + 9, oNodeRect.Top + 6));

				oGraphics.DrawLine(pen, new Point(x + 6, oNodeRect.Top + 5), new Point(x + 6, oNodeRect.Top + 4));
				oGraphics.DrawLine(pen, new Point(x + 8, oNodeRect.Top + 5), new Point(x + 8, oNodeRect.Top + 4));

				oGraphics.DrawLine(pen, new Point(x + 6, oNodeRect.Top + 4), new Point(x + 7, oNodeRect.Top + 3));

				pen.Dispose();
				pen = null;
			}
			#endregion

		}

		/// <summary>
		/// Paint the box nex to the node
		/// </summary>
		private void PaintExpandBox(Node oNode, Graphics oGraphics, int nX, int nY, 
			ref Hashtable m_mapRectToItemBox, ref Hashtable m_mapItemBoxToRect)
		{
			int nAlpha = oNode.TreeView.GetNodeAlpha(oNode);
			Rectangle rect = new Rectangle(nX - 12, nY + 2, 10, 10);
		
			if (oNode.GetShowPlusMinus() == true)
			{
				if (oNode.Nodes.Count > 0 || oNode.Panel != null)
				{
					if (oNode.IsExpanded == false)
					{
						// draw the plus in the box										
						Pen oPen = null;		
			
						if (oNode.Flash == true)
							nAlpha = 255;

						Rectangle boxRect = new Rectangle(rect.Left + 1, rect.Top + 1, rect.Width - 2, rect.Height - 2);

						// if XP line style, then fade the box and paint in black the inner part
						if (oNode.GetExpandBoxShape() == ExpandBoxShape.XP)
						{
							LinearGradientBrush oBrush = new LinearGradientBrush
							(
								new Rectangle(boxRect.Left, boxRect.Top, boxRect.Width, boxRect.Height),
								Color.White,
								oNode.GetExpandBoxBackColor(),
								LinearGradientMode.Vertical						
							);  

							oGraphics.FillRectangle(oBrush, new Rectangle(boxRect.Left + 1, boxRect.Top + 1, boxRect.Width - 1, boxRect.Height - 1));						

							oPen = new Pen(Color.FromArgb(nAlpha, oNode.GetExpandBoxBorderColor()), 1);
							oGraphics.DrawLine(oPen, boxRect.Left + 1, boxRect.Bottom, boxRect.Right - 1, boxRect.Bottom);
							oGraphics.DrawLine(oPen, boxRect.Right, boxRect.Top + 1, boxRect.Right, boxRect.Bottom - 1);
							oPen.Color = Color.FromArgb(128, oPen.Color);
							oGraphics.DrawLine(oPen, boxRect.Left + 1, boxRect.Top, boxRect.Right - 1, boxRect.Top);
							oGraphics.DrawLine(oPen, boxRect.Left, boxRect.Top + 1, boxRect.Left, boxRect.Bottom - 1);
						}
						else 
						{
							oPen = new Pen(Color.FromArgb(nAlpha, oNode.GetExpandBoxBorderColor()), 1);												
							oGraphics.DrawRectangle(oPen, boxRect);

							if (oNode.GetExpandBoxShape() == ExpandBoxShape.Flat)
							{
								SolidBrush oBrush = new SolidBrush(oNode.GetExpandBoxBackColor());
								oGraphics.FillRectangle(oBrush, new Rectangle(boxRect.Left + 1, boxRect.Top + 1, boxRect.Width - 1, boxRect.Height - 1));						
							}
						}

						oPen.Color = Color.FromArgb(nAlpha, oNode.GetExpandBoxForeColor());
						oGraphics.DrawLine(oPen, nX - 9, nY + 7, nX - 5, nY + 7);
						oGraphics.DrawLine(oPen, nX - 7, nY + 9, nX - 7, nY + 5);
						oPen.Dispose();
					}
					else
					{
						// draw the minus in the box
						Pen oPen = null;

						if (oNode.Flash == true)
							nAlpha = 255;			
			
						Rectangle boxRect = new Rectangle(rect.Left + 1, rect.Top + 1, rect.Width - 2, rect.Height - 2);

						// if XP line style, then fade the box and paint in black the inner part
						if (oNode.GetExpandBoxShape() == ExpandBoxShape.XP)
						{
							LinearGradientBrush oBrush = new LinearGradientBrush
							(
								new Rectangle(boxRect.Left, boxRect.Top, boxRect.Width, boxRect.Height),
								Color.White,
								oNode.GetExpandBoxBackColor(),
								LinearGradientMode.Vertical						
							);  

							oGraphics.FillRectangle(oBrush, new Rectangle(boxRect.Left + 1, boxRect.Top + 1, boxRect.Width - 1, boxRect.Height - 1));							

							oPen = new Pen(Color.FromArgb(nAlpha, oNode.GetExpandBoxBorderColor()), 1);
							oGraphics.DrawLine(oPen, boxRect.Left + 1, boxRect.Bottom, boxRect.Right - 1, boxRect.Bottom);
							oGraphics.DrawLine(oPen, boxRect.Right, boxRect.Top + 1, boxRect.Right, boxRect.Bottom - 1);
							oPen.Color = Color.FromArgb(128, oPen.Color);
							oGraphics.DrawLine(oPen, boxRect.Left + 1, boxRect.Top, boxRect.Right - 1, boxRect.Top);
							oGraphics.DrawLine(oPen, boxRect.Left, boxRect.Top + 1, boxRect.Left, boxRect.Bottom - 1);
						}
						else 
						{
							oPen = new Pen(Color.FromArgb(nAlpha, oNode.GetExpandBoxBorderColor()), 1);												
							oGraphics.DrawRectangle(oPen, boxRect);

							if (oNode.GetExpandBoxShape() == ExpandBoxShape.Flat)
							{
								SolidBrush oBrush = new SolidBrush(oNode.GetExpandBoxBackColor());
								oGraphics.FillRectangle(oBrush, new Rectangle(boxRect.Left + 1, boxRect.Top + 1, boxRect.Width - 1, boxRect.Height - 1));						
							}
						}

						oPen.Color = Color.FromArgb(nAlpha, oNode.GetExpandBoxForeColor());
						oGraphics.DrawLine(oPen, nX - 8, nY + 7, nX - 6, nY + 7);
						oPen.Dispose();
					}					
				}
				else
				{
					// draw the dot in the box
					Pen oPen = null;

					if (oNode.Flash == true)
						nAlpha = 255;			
		
					Rectangle boxRect = new Rectangle(rect.Left + 1, rect.Top + 1, rect.Width - 2, rect.Height - 2);

					// if XP line style, then fade the box and paint in black the inner part
					if (oNode.GetExpandBoxShape() == ExpandBoxShape.XP)
					{
						LinearGradientBrush oBrush = new LinearGradientBrush
						(
							new Rectangle(boxRect.Left, boxRect.Top, boxRect.Width, boxRect.Height),
							Color.White,
							oNode.GetExpandBoxBackColor(),
							LinearGradientMode.Vertical						
						);  

						oGraphics.FillRectangle(oBrush, new Rectangle(boxRect.Left + 1, boxRect.Top + 1, boxRect.Width - 1, boxRect.Height - 1));						

						oPen = new Pen(Color.FromArgb(nAlpha, oNode.GetExpandBoxBorderColor()), 1);
						oGraphics.DrawLine(oPen, boxRect.Left + 1, boxRect.Bottom, boxRect.Right - 1, boxRect.Bottom);
						oGraphics.DrawLine(oPen, boxRect.Right, boxRect.Top + 1, boxRect.Right, boxRect.Bottom - 1);
						oPen.Color = Color.FromArgb(128, oPen.Color);
						oGraphics.DrawLine(oPen, boxRect.Left + 1, boxRect.Top, boxRect.Right - 1, boxRect.Top);
						oGraphics.DrawLine(oPen, boxRect.Left, boxRect.Top + 1, boxRect.Left, boxRect.Bottom - 1);
					}
					else 
					{
						oPen = new Pen(Color.FromArgb(nAlpha, oNode.GetExpandBoxBorderColor()), 1);											
						oGraphics.DrawRectangle(oPen, boxRect);

						if (oNode.GetExpandBoxShape() == ExpandBoxShape.Flat)
						{
							SolidBrush oBrush = new SolidBrush(oNode.GetExpandBoxBackColor());
							oGraphics.FillRectangle(oBrush, new Rectangle(boxRect.Left + 1, boxRect.Top + 1, boxRect.Width - 1, boxRect.Height - 1));						
						}
					}

					oPen.Color = Color.FromArgb(nAlpha, oNode.GetExpandBoxForeColor());
					oGraphics.DrawLine(oPen, nX - 7, nY + 7, nX - 7, nY + 8);
					oPen.Dispose();
				}
			}

			// now mask the rect into the rect holders. it is being used by the mouse handler to change statuses
			try{m_mapRectToItemBox.Add(rect, oNode);}catch{}
			try{m_mapItemBoxToRect.Add(oNode, rect);}catch{}			
		}

		/// <summary>
		/// Paint the node text
		/// </summary>
		/// <param name="oNode"></param>
		/// <param name="oGraphics"></param>
		/// <param name="nX"></param>
		/// <param name="nY"></param>		
		private void PaintNodeText(Node oNode, Graphics oGraphics, int nX, int nY, 
			ref Hashtable m_mapRectToItemCheck, ref Hashtable m_mapItemCheckToRect)
		{
			int nAlpha = oNode.TreeView.GetNodeAlpha(oNode);
			Font oFont = oNode.GetFont();

			if (oNode.Flash == true)
				nAlpha = 255;

			//if (oNode.GetShowPlusMinus() == false)
			//	nX -= 15;
			
			SizeF oTextSize = oGraphics.MeasureString(oNode.GetText(), oNode.GetFont(), oNode.GetTreeView().GetDrawWidth() - nX - 8);
			if (m_oTreeView.Multiline == false)
				oTextSize = oGraphics.MeasureString(oNode.GetText(), oFont);

			oNode.Top = nY + (int)oTextSize.Height + 2;

			int nTextWidth = (int)oTextSize.Width;
			oNode.TextWidth = nTextWidth;

			if (oTextSize.Height == 0)
			{
				oTextSize.Height = oNode.GetFont().Height;
			}
			
			// draw check boxes
			if (oNode.GetCheckBoxes() == true && oNode.CheckBoxVisible == true)
			{												
				Pen oPen = null;		
			
				if (oNode.Flash == true)
					nAlpha = 255;

				#region draw check background
				if (oNode.GetCheckBackColor() != Color.Transparent)
				{
					if (oNode.Parent == null || oNode.Parent.SubNodesCheckExclusive == false)
					{
						Rectangle rectCheckBack = new Rectangle(nX + 4, nY + 2, 11, 11);
						Color backColor = Color.FromArgb(nAlpha, oNode.GetCheckBackColor());

						Brush brush = new SolidBrush(backColor);

						if (oNode.GetCheckBorderStyle() == CheckBoxBorderStyle.XP)
						{					
							Color fadeColor = oNode.GetCheckBackColor();
							if (fadeColor == Color.White)
								fadeColor = Color.LightGray;

							brush = new LinearGradientBrush
								(
								rectCheckBack,
								Color.FromArgb(nAlpha == 255 ? 255 : 48, fadeColor),
								Color.White,
								LinearGradientMode.ForwardDiagonal						
								);  
						}

						oGraphics.FillRectangle(brush, rectCheckBack);
						brush.Dispose();
						brush = null;
					}
					else
					{
						Rectangle rectCheckBack = new Rectangle(nX + 4, nY + 2, 11, 11);
						Color backColor = Color.FromArgb(nAlpha, oNode.GetCheckBackColor());

						Brush brush = new SolidBrush(backColor);

						if (oNode.GetCheckBorderStyle() == CheckBoxBorderStyle.XP)
						{					
							Color fadeColor = oNode.GetCheckBackColor();
							if (fadeColor == Color.White)
								fadeColor = Color.LightGray;

							brush = new LinearGradientBrush
								(
								rectCheckBack,
								Color.FromArgb(nAlpha == 255 ? 255 : 48, fadeColor),
								Color.White,
								LinearGradientMode.ForwardDiagonal						
								);  
						}

						GraphicsPath path = new GraphicsPath();
						path.AddEllipse(nX + 4, nY + 2, 10, 10);
						Region region = new Region(path);
						
						oGraphics.Clip = region;
						oGraphics.FillRectangle(brush, rectCheckBack);
						oGraphics.Clip = new Region(new Rectangle(0, 0, m_oTreeView.Width, m_oTreeView.Height));

						brush.Dispose();
						brush = null;
					}
				}
				#endregion

				oPen = new Pen(Color.FromArgb(nAlpha, oNode.GetCheckBorderColor()), 1);																	
				Rectangle rectCheck = new Rectangle(nX + 3, nY + 1, 12, 12);

				#region draw check rectangle

				if (oNode.Parent == null || oNode.Parent.SubNodesCheckExclusive == false)
				{
					if (oNode.GetCheckBorderStyle() == CheckBoxBorderStyle.Solid || oNode.GetCheckBorderStyle() == CheckBoxBorderStyle.XP)
						oGraphics.DrawRectangle(oPen, rectCheck);

					if (oNode.GetCheckBorderStyle() == CheckBoxBorderStyle.Dot)
					{
						oPen.DashStyle = DashStyle.Dot;
						oGraphics.DrawRectangle(oPen, rectCheck);
					}
				}
				else
				{
					if (oNode.GetCheckBorderStyle() == CheckBoxBorderStyle.Dot)					
						oPen.DashStyle = DashStyle.Dot;

					if (oNode.GetCheckBorderStyle() == CheckBoxBorderStyle.Solid || oNode.GetCheckBorderStyle() == CheckBoxBorderStyle.XP
						|| oNode.GetCheckBorderStyle() == CheckBoxBorderStyle.Dot)
						oGraphics.DrawEllipse(oPen, nX + 4, nY + 2, 10, 10);
				}

				oPen.DashStyle = DashStyle.Solid;
				#endregion

				m_mapItemCheckToRect.Add(oNode, rectCheck);
				m_mapRectToItemCheck.Add(rectCheck, oNode);

				if (oNode.Checked == true)
				{
					if (oNode.Parent == null || oNode.Parent.SubNodesCheckExclusive == false)
					{
						oPen.Color = Color.FromArgb(nAlpha, oNode.GetCheckCheckColor());
					
						oGraphics.DrawLine(oPen, nX + 6, nY + 6, nX + 6, nY + 8);
						oGraphics.DrawLine(oPen, nX + 7, nY + 7, nX + 7, nY + 9);
						oGraphics.DrawLine(oPen, nX + 8, nY + 8, nX + 8, nY + 10);
						oGraphics.DrawLine(oPen, nX + 9, nY + 7, nX + 9, nY + 9);
						oGraphics.DrawLine(oPen, nX + 10, nY + 6, nX + 10, nY + 8);
						oGraphics.DrawLine(oPen, nX + 11, nY + 5, nX + 11, nY + 7);
						oGraphics.DrawLine(oPen, nX + 12, nY + 4, nX + 12, nY + 6);
					}
					else
					{
						oPen.Color = Color.FromArgb(nAlpha, oNode.GetCheckCheckColor());

						oGraphics.DrawEllipse(oPen, nX + 6, nY + 4, 6, 6);
						oGraphics.DrawEllipse(oPen, nX + 7, nY + 5, 4, 4);
						oGraphics.DrawEllipse(oPen, nX + 8, nY + 6, 2, 2);						
						oGraphics.DrawEllipse(oPen, nX + 9, nY + 7, 1, 1);
						oGraphics.DrawRectangle(oPen, nX + 8, nY + 6, 2, 2);
					}
				}

				oPen.Dispose();

				nX += 17;
			}

			// paint the picture first if needed then draw the text
			if (oNode.Image != null)
			{								
				oGraphics.DrawImage(oNode.Image, 
					nX + 4, 
					nY + (int)(oTextSize.Height / 2.0) - (int)((float)oNode.Image.Height / 2.0) + 1,
					oNode.Image.Width, oNode.Image.Height);

				nX += oNode.Image.Width + 2;
			}
			else
			{
				if (oNode.ImageIndex != -1 && oNode.TreeView.ImageList != null 
					&& oNode.ImageIndex < oNode.TreeView.ImageList.Images.Count)
				{					
					oNode.TreeView.ImageList.Draw(oGraphics, nX + 2, 
						nY + (int)(oTextSize.Height / 2.0) - (int)((float)oNode.TreeView.ImageList.ImageSize.Height / 2.0), 
						oNode.TreeView.ImageList.ImageSize.Width,
						oNode.TreeView.ImageList.ImageSize.Height,
						oNode.ImageIndex);

					nX += oNode.TreeView.ImageList.ImageSize.Width;
				}
			}

			// get the right side of the text. if it is more far away than the width of the TreeView, truncate the text
			string sText = oNode.GetText();

			// render the text
			SolidBrush oTextBrush = new SolidBrush(Color.FromArgb(nAlpha, oNode.GetForeColor()));						

			// when draging the mouse over nodes, test if the node is being highlighted
			if (oNode == oNode.TreeView.HighlightedNode)
				oTextBrush.Color = oNode.GetHighlightedForeColor();

			Rectangle textRect = new Rectangle(nX + 2, nY, oNode.GetTreeView().GetDrawWidth() - nX - 15, 
				(int)oNode.GetTreeView().Height);

      SizeF textSize = oGraphics.MeasureString(StringDrawUtils.GetInstance().GetTextFromFormattedString(sText), oFont,
        oNode.GetTreeView().GetDrawWidth() - nX - 15);			

			// clear the truncated flag
			oNode.TextTruncated = false;
 
			if (m_oTreeView.Multiline)
			{
				if (oNode.UseFormatting == true)
				{
					CharacterFormat chrFormat = new CharacterFormat(
						oFont, 
						oTextBrush, 
						0, 
						HotkeyPrefix.None, 
						true);

					ParagraphFormat paraFormat = new ParagraphFormat(
						ParagraphAlignment.Left, 
						ParagraphVerticalAlignment.Top, 
						true, 
						true, 
						StringTrimming.None, 
						Brushes.Transparent);

					StringDrawUtils.GetInstance().DrawStringInRectangle(oGraphics, sText, textRect, 
						chrFormat, paraFormat);
				}
				else
					oGraphics.DrawString(sText, oFont, oTextBrush, textRect);
			}
			else
			{
				sText = oNode.GetText();

        oTextSize = oGraphics.MeasureString(StringDrawUtils.GetInstance().GetTextFromFormattedString(sText), oFont);

        nTextWidth = (int)oTextSize.Width;

				if (nX + 2 + nTextWidth > m_oTreeView.m_LastNX)
					m_oTreeView.m_LastNX = nX + 2 + nTextWidth;

				if (nX + nTextWidth > oNode.GetTreeView().GetDrawWidth() - 15)
				{
          sText = StringDrawUtils.GetInstance().GetTextFromFormattedString(sText);

					float fChar = oTextSize.Width / float.Parse(sText.Length.ToString());

					int nChar = (int)((float)(nX + nTextWidth - oNode.GetTreeView().GetDrawWidth() + 15) / fChar) + 4;

					if (nChar < 0)
						nChar = 0;

					int nLength = sText.Length - nChar;
					if (nLength < 0)
						nLength = 0;

					sText = sText.Substring(0, nLength) + "...";
					oNode.TextTruncated = true;
				}
				
				// draw string (text) of the node in the proper system, based on the information whether it has formatting or not
				if (oNode.UseFormatting == true)
				{
					CharacterFormat chrFormat = new CharacterFormat(
						oFont, 
						oTextBrush, 
						0, 
						HotkeyPrefix.None, 
						true);

					StringDrawUtils.GetInstance().DrawString(oGraphics, sText,  new PointF(nX + 2, nY), 
						chrFormat, ParagraphAlignment.Left);
				}
				else
					oGraphics.DrawString(sText, oFont, oTextBrush, nX + 2, nY);				
			}

      if (m_oTreeView.Multiline == false) {
        textSize = oGraphics.MeasureString(StringDrawUtils.GetInstance().GetTextFromFormattedString(sText), oFont);
      }

			oTextBrush.Dispose();			

			// create the expand icon if specified
			if (oNode.GetShowSubitemsIndicator() == true && oNode.Nodes.Count > 0 && oNode.IsExpanded == false)
			{	
				int nIconX = (int)textRect.Left + (int)textSize.Width + 4;

				Pen oIndicatorPen = new Pen(Color.FromArgb(nAlpha, oNode.GetForeColor()), 1);
				oGraphics.DrawLine(oIndicatorPen, nIconX, nY + textSize.Height - 4, nIconX + 3, nY + textSize.Height - 4);
				oGraphics.DrawLine(oIndicatorPen, nIconX + 1, nY + textSize.Height - 5, nIconX + 3, nY + textSize.Height - 5);
				oGraphics.DrawLine(oIndicatorPen, nIconX + 2, nY + textSize.Height - 6, nIconX + 3, nY + textSize.Height - 6);
				oGraphics.DrawLine(oIndicatorPen, nIconX + 3, nY + textSize.Height - 6, nIconX + 3, nY + textSize.Height - 7);
				oIndicatorPen.Dispose();
			}

			if (oNode.Underline == true)
				PaintNodeUnderline(oNode, sText, oGraphics, nX, nY);
		}

		/// <summary>
		/// Paints the underline for the specified node
		/// </summary>
		/// <param name="oNode"></param>
		/// <param name="oGraphics"></param>
		/// <param name="nX"></param>
		/// <param name="nY"></param>
		private void PaintNodeUnderline(Node oNode, string sText, Graphics oGraphics, int nX, int nY)
		{
			UnderlineStyle oStyle = oNode.GetUnderlineStyle();
			Pen oPen = new Pen(oNode.GetUnderlineColor(), 1);

      SizeF oSize = oGraphics.MeasureString(StringDrawUtils.GetInstance().GetTextFromFormattedString(sText),
        oNode.GetFont(), oNode.GetTreeView().GetDrawWidth() - nX - 8);
      
      oSize.Width += 2;

			if (oStyle == UnderlineStyle.Tilde)
			{
				Pen oClearPen = null;
				
				if (oNode.IsSelected == false)
				{
					if (oNode.BackgroundStyle.Visible == false)
						oClearPen = oNode.GetUnderlineBackColor();
					else
						oClearPen = new Pen(oNode.BackgroundStyle.BackColor, 1);
				}
				else
					oClearPen = new Pen(oNode.GetSelectedBackColor(), 1);
				
				int nStep = 0;
				
				while (nX + 4 + nStep < nX + oSize.Width)
				{
					oGraphics.DrawLine(oPen, nX + 4 + nStep, nY + oSize.Height - 2, nX + 5 + nStep, nY + oSize.Height - 2);
					oGraphics.DrawLine(oClearPen, nX + 5 + nStep, nY + oSize.Height - 2, nX + 6 + nStep, nY + oSize.Height - 2);
					nStep += 4;
				}

				nStep = 0;
				while (nX + 4 + nStep < nX + oSize.Width)
				{
					oGraphics.DrawLine(oPen, nX + 5 + nStep, nY + oSize.Height - 1, nX + 6 + nStep, nY + oSize.Height - 1);
					oGraphics.DrawLine(oClearPen, nX + 6 + nStep, nY + oSize.Height - 1, nX + 7 + nStep, nY + oSize.Height - 1);
					nStep += 2;
				}

				nStep = 0;
				while (nX + 4 + nStep < nX + oSize.Width)
				{
					oGraphics.DrawLine(oPen, nX + 6 + nStep, nY + oSize.Height, nX + 7 + nStep, nY + oSize.Height);
					oGraphics.DrawLine(oClearPen, nX + 7 + nStep, nY + oSize.Height, nX + 8 + nStep, nY + oSize.Height);
					nStep += 4;
				}

				oClearPen.Dispose();
			}
			else
			{
				if (oStyle == UnderlineStyle.Dash)
					oPen.DashStyle = DashStyle.Dash;

				if (oStyle == UnderlineStyle.Dot)
					oPen.DashStyle = DashStyle.Dot;				

				oGraphics.DrawLine(oPen, nX + 4, nY + oSize.Height - 2, nX + oSize.Width, nY + oSize.Height - 2);
			}

			oPen.Dispose();
		}
		#endregion
	}
}
