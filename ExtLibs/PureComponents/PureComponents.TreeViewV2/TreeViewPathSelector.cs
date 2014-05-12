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
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using PureComponents.TreeView.Design;
using ComponentModel_DesignerSerializationVisibility=System.ComponentModel.DesignerSerializationVisibility;

namespace PureComponents.TreeView
{
	/// <summary>
	/// TreeViewPathSelector represents selector control.
	/// </summary>
	[DefaultProperty("TreeView")]
	[DefaultEvent("")]
	[Serializable]
	[Designer(typeof(TreeViewPathSelectorDesigner), typeof(ControlDesigner))]
	[ToolboxBitmap(typeof (TreeViewPathSelector), "PureComponents.TreeViewPathSelector.bmp")]
	public class TreeViewPathSelector : Control
	{
		#region private members

		private TreeView m_TreeView;
		
		private Hashtable m_MapBtn2Node = new Hashtable();
		private Hashtable m_MapItem2Node = new Hashtable();
		
		private Point m_MousePos;
		
		private TreeViewPathSelectorStyle m_Style;
		private Rectangle m_HighlightRect;
		private HighlightRectType m_HighlightRectType;

		private ArrayList m_Nodes2Add = new ArrayList();
		private Node m_DropDownNode = null;
		private bool m_Truncated = false;
		private bool m_FirstDropDownOn = false;

		private enum HighlightRectType
		{
			Left,
			Right,
			Full
		}
		
		#endregion

		#region construction

		/// <summary>
		/// 
		/// </summary>
		public TreeViewPathSelector()
		{
			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.ResizeRedraw, true);
			
			m_Style = new TreeViewPathSelectorStyle();
			m_Style.Invalidate += new EventHandler(m_Style_Invalidate);
		}

		#endregion

		#region implementation
		
		/// <summary>
		/// Alter the size of the control when needed
		/// </summary>
		/// <param name="e"></param>
		protected override void OnResize(EventArgs e)
		{
			base.OnResize (e);
			
			// check the height
			if (Height != Font.Height + 8)
				Height = Font.Height + 8;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint (e);
			
			if (TreeView == null)
			{
				if (DesignMode == true)
				{
					e.Graphics.DrawString("No TreeView associated, set TreeView property.", Font, Brushes.Black, 3, 2);	
				}
				
				return;
			}
			
			if (DesignMode == true)
				return;
			
			// paint the dropdown control
			OnPaintNode(TreeView.SelectedNode, e);			
		}
		
		/// <summary>
		/// Paint the background
		/// </summary>
		/// <param name="pevent"></param>
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			AreaPainter.PaintRectangle(pevent.Graphics, 0, 0, Width, Height, m_Style.FillStyle, m_Style.BackColor,
			                           m_Style.FadeColor, m_Style.BorderStyle, m_Style.BorderColor);

			if (m_Style.BorderStyle == BorderStyle.Raised)
			{
				ControlPaint.DrawBorder3D(pevent.Graphics, 0, 0, Width, Height, Border3DStyle.Raised);
			}
			else if (m_Style.BorderStyle == BorderStyle.Sunken)
			{
				ControlPaint.DrawBorder3D(pevent.Graphics, 0, 0, Width, Height, Border3DStyle.Sunken);
			}
			
			if (m_HighlightRect == Rectangle.Empty)
				return;
			
			// determine the rectangle area type
			if (m_HighlightRectType == HighlightRectType.Left)
			{
				AreaPainter.PaintRectangle(pevent.Graphics, m_HighlightRect.Left, 0, m_HighlightRect.Width - 13,
					Height, m_Style.FillStyleSelection, m_Style.SelectionColor, Color.White, BorderStyle.None, Color.White);
				AreaPainter.PaintRectangle(pevent.Graphics, m_HighlightRect.Right - 13, 0, 13,
					Height, m_Style.FillStyleSelection, Color.LightGray, Color.White, BorderStyle.None, Color.White);
				
				// paint the border
				Pen pen = new Pen(ColorManager.ModifyBrightness(m_Style.SelectionColor, 0.8f));
				pevent.Graphics.DrawLine(pen, m_HighlightRect.Left, 0, m_HighlightRect.Right - 13, 0);
				pevent.Graphics.DrawLine(pen, m_HighlightRect.Left, 0, m_HighlightRect.Left, Height);
				pevent.Graphics.DrawLine(pen, m_HighlightRect.Left, Height - 1, m_HighlightRect.Right - 13, Height - 1);
				pen.Color = ColorManager.ModifyBrightness(Color.LightGray, 0.8f);
				pevent.Graphics.DrawLine(pen, m_HighlightRect.Right, 0, m_HighlightRect.Right - 13, 0);
				pevent.Graphics.DrawLine(pen, m_HighlightRect.Right, 0, m_HighlightRect.Right, Height);
				pevent.Graphics.DrawLine(pen, m_HighlightRect.Right, Height - 1, m_HighlightRect.Right - 13, Height - 1);
				pen.Dispose();
			}
			else if (m_HighlightRectType == HighlightRectType.Right)
			{
				AreaPainter.PaintRectangle(pevent.Graphics, m_HighlightRect.Left, 0, m_HighlightRect.Width - 13,
					Height, m_Style.FillStyleSelection, Color.LightGray, Color.White, BorderStyle.None, Color.White);
				AreaPainter.PaintRectangle(pevent.Graphics, m_HighlightRect.Right - 13, 0, 13,
					Height, m_Style.FillStyleSelection, m_Style.SelectionColor, Color.White, BorderStyle.None, Color.White);
				
				// paint the border
				Pen pen = new Pen(ColorManager.ModifyBrightness(Color.LightGray, 0.8f));
				pevent.Graphics.DrawLine(pen, m_HighlightRect.Left, 0, m_HighlightRect.Right - 13, 0);
				pevent.Graphics.DrawLine(pen, m_HighlightRect.Left, 0, m_HighlightRect.Left, Height);
				pevent.Graphics.DrawLine(pen, m_HighlightRect.Left, Height - 1, m_HighlightRect.Right - 13, Height - 1);
				pen.Color = ColorManager.ModifyBrightness(m_Style.SelectionColor, 0.8f);
				pevent.Graphics.DrawLine(pen, m_HighlightRect.Right, 0, m_HighlightRect.Right - 13, 0);
				pevent.Graphics.DrawLine(pen, m_HighlightRect.Right, 0, m_HighlightRect.Right, Height);
				pevent.Graphics.DrawLine(pen, m_HighlightRect.Right, Height - 1, m_HighlightRect.Right - 13, Height - 1);
				pen.Dispose();
			}	
			else
			{
				AreaPainter.PaintRectangle(pevent.Graphics, m_HighlightRect.Left, 0, m_HighlightRect.Width,
					Height, m_Style.FillStyleSelection, m_Style.SelectionColor, Color.White, BorderStyle.Solid, 
					ColorManager.ModifyBrightness(m_Style.SelectionColor, 0.8f));
			}
		}			

		/// <summary>
		/// 
		/// </summary>
		/// <param name="node"></param>
		/// <param name="e"></param>
		private void OnPaintNode(Node node, PaintEventArgs e)
		{
			m_MapBtn2Node.Clear();
			m_Nodes2Add.Clear();

			int xPos = 3;
			
			if (node == null)
			{
				OnPaintDropDown(node, xPos + 1, e);
				
				return;
			}
			
			// for each node's parent paint the node text and dropdown for it
			ArrayList parents = new ArrayList();
			
			Node parent = node;
			
			while (parent != null)
			{
				parents.Insert(0, parent);
				
				parent = parent.Parent;
			}

			// paint the first node
			OnPaintDropDown(null, xPos + 1, e);
			xPos += 14;

			// first measure all nodes
			foreach (Node node1 in parents)
			{
				SizeF size = e.Graphics.MeasureString(node1.Text, Font);
				
				xPos += (int) size.Width + 14;
			}

			// if larger than the are for painting then 
			bool repaint = false;
			if (xPos > this.Width)
			{
				if (m_Truncated == false)
					repaint = true;

				m_Truncated = true;

				xPos = Width - 4;

				// paint from last node
				int nParentStart = parents.Count - 1;
				for (int nParent = parents.Count - 1; nParent >= 0; nParent --)
				{
					Node node1 = parents[nParent] as Node;

					// paint the node here
					SizeF size = e.Graphics.MeasureString(node1.Text, Font);
				
					xPos = xPos - (int) size.Width - 14;

					if (xPos <= 14)
						break;					

					nParentStart = nParent;
				}
			
				// clear nodes
				m_Nodes2Add.Clear();

				// nParentStart defines the start, remember the nodes and add them to the list
				for (int nParent = 0; nParent < nParentStart; nParent ++)
				{
					Node node1 = parents[nParent] as Node;

					m_Nodes2Add.Add(node1);
				}
	
				xPos = 17;
				for (int nParent = nParentStart; nParent < parents.Count; nParent ++)
				{
					Node node1 = parents[nParent] as Node;

					OnPaintDropDown(node1, xPos, e);

					SizeF size = e.Graphics.MeasureString(node1.Text, Font);
				
					xPos += (int) size.Width + 14;
				}
			}
			else
			{
				if (m_Truncated == true)
					repaint = true;

				m_Truncated = false;
 
				xPos = 17;

				foreach (Node node1 in parents)
				{
					OnPaintDropDown(node1, xPos, e);
				
					SizeF size = e.Graphics.MeasureString(node1.Text, Font);
				
					xPos += (int) size.Width + 14;
				}
			}
			
			if (repaint)
				this.Invalidate();
		}
		
		/// <summary>
		/// Paint drop down for the specified name
		/// </summary>
		/// <param name="node"></param>
		/// <param name="pos"></param>
		/// <param name="e"></param>
		private void OnPaintDropDown(Node node, int pos, PaintEventArgs e)
		{
			if (node == null)
			{
				if (m_Truncated == false)
				{
					if (m_FirstDropDownOn == true)
						DrawComboButton(e.Graphics, pos - 1);
					else
						DrawComboButton2(e.Graphics, pos - 1);
				}
				else
				{
					DrawFirstButton(e.Graphics, pos + 2, 7);
					DrawFirstButton(e.Graphics, pos + 6, 7);
				}
				
				m_MapBtn2Node.Add(new Rectangle(0, 0, 16, Height), null);
				
				return;
			}
			
			// draw node's text and then the dropdown
			SizeF size = e.Graphics.MeasureString(node.Text, Font);
			Brush brush = new SolidBrush(m_Style.ForeColor);
			e.Graphics.DrawString(node.Text, Font, brush, pos, 4);
			brush.Dispose();
			
			if (node.Nodes.Count == 0)
			{
				m_MapBtn2Node.Add(new Rectangle(pos, 0, (int)size.Width + 4, Height), node);
				
				return;
			}					
			
			// paint the dropdown
			if (node == m_DropDownNode)
				DrawComboButton(e.Graphics, pos + (int)size.Width + 1);
			else
				DrawComboButton2(e.Graphics, pos + (int)size.Width + 1);
			
			// remember the location and the node associated with it, so on click we can show the dropdown
			m_MapBtn2Node.Add(new Rectangle(pos, 0, (int)size.Width + 13, Height), node);
		}
		
		/// <summary>
		/// 
		/// </summary>
		private void DrawFirstButton(Graphics graphics, int pos, int top)
		{
			top = Height / 2 - 2;

			Pen pen = new Pen(m_Style.ForeColor);

			graphics.DrawLine(pen, pos, top, pos + 2, top);
			graphics.DrawLine(pen, pos - 1, top + 1, pos + 1, top + 1);
			graphics.DrawLine(pen, pos - 2, top + 2, pos + 0, top + 2);
			graphics.DrawLine(pen, pos - 1, top + 3, pos + 1, top + 3);
			graphics.DrawLine(pen, pos, top + 4, pos + 2, top + 4);

			pen.Dispose();
		}
		
		/// <summary>
		/// 
		/// </summary>
		private void DrawComboButton(Graphics graphics, int left)
		{
			Pen pen = new Pen(m_Style.ForeColor);
			
			Rectangle rect = new Rectangle(left, 0, 12, Height);

			graphics.DrawLine(pen, left + rect.Width - rect.Width / 2 - 4, 
				rect.Height / 2 - 2, 
				left + rect.Width - rect.Width / 2 + 2, 
				rect.Height / 2 - 2);
			graphics.DrawLine(pen, left + rect.Width - rect.Width / 2 - 3, 
				rect.Height / 2 - 1, 
				left + rect.Width - rect.Width / 2 + 1, 
				rect.Height / 2 - 1);
			graphics.DrawLine(pen, left + rect.Width - rect.Width / 2 - 2, 
				rect.Height / 2 - 0, 
				left + rect.Width - rect.Width / 2, 
				rect.Height / 2 - 0);
			graphics.DrawLine(pen, left + rect.Width - rect.Width / 2 - 1, 
				rect.Height / 2 - 0, 
				left + rect.Width - rect.Width / 2 - 1, 
				rect.Height / 2 + 1);

			pen.Dispose();
		}

		/// <summary>
		/// 
		/// </summary>
		private void DrawComboButton2(Graphics graphics, int left)
		{
			Pen pen = new Pen(m_Style.ForeColor);
			
			int top = Height / 2;

			left += 5;

			graphics.DrawLine(pen, left - 1, top - 3, left - 1, top + 3); 
			graphics.DrawLine(pen, left, top - 2, left, top + 2); 
			graphics.DrawLine(pen, left + 1, top - 1, left + 1, top + 1); 
			graphics.DrawLine(pen, left + 1, top, left + 2, top); 

			pen.Dispose();
		}

		#endregion

		#region helper functions

		#endregion

		#region event handlers
		
		/// <summary>
		/// repaint
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_TreeView_NodeSelectionChange(object sender, System.EventArgs e)
		{
			Invalidate();
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown (e);					
		}
		
		protected override void OnMouseUp(MouseEventArgs e)
		{
			Point p = new Point(e.X,  e.Y);

			ProccesMouseClick(p);						
				
			base.OnMouseUp (e);					
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave (e);
			
			m_HighlightRect = Rectangle.Empty;
			
			Invalidate();
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		internal void ProccesMouseClick(Point p)
		{
			m_MousePos = p;
			
			// show the dropdown menu if clicked at the pseudo button
			foreach (Rectangle rectangle in m_MapBtn2Node.Keys)
			{
				if (rectangle.Contains(m_MousePos) == false)
					continue;
				
				// get the node and show the menu with subnodes of the node
				Node node = m_MapBtn2Node[rectangle] as Node;
				
				if (node == null)
				{
					// create the menu and show it
					ContextMenu menu1 = new ContextMenu();
				
					m_MapItem2Node.Clear();

					// add nodes that have been omitted
					bool separator = false;
					foreach (Node nodes2Add in m_Nodes2Add)
					{
						separator = true;

						MenuItem item = menu1.MenuItems.Add(nodes2Add.Text);
					
						// create map between item and the node associated with it
						m_MapItem2Node.Add(item, nodes2Add);
					
						item.Click += new EventHandler(item_Click);
					}

					// add item separator
					if (separator == true)
						menu1.MenuItems.Add("-");

					foreach (Node node1 in TreeView.Nodes)
					{
						MenuItem item = menu1.MenuItems.Add(node1.Text);
					
						// create map between item and the node associated with it
						m_MapItem2Node.Add(item, node1);
					
						item.Click += new EventHandler(item_Click);
					}

					m_FirstDropDownOn = true;
					this.Refresh();
								
					menu1.Disposed += new EventHandler(menu_Disposed);
					menu1.Show(this, m_MousePos);	
				
					return;
				}
				
				// create the menu and show it
				ContextMenu menu = new ContextMenu();
				
				m_MapItem2Node.Clear();
				
				if (m_HighlightRectType == HighlightRectType.Left || m_HighlightRectType == HighlightRectType.Full)
				{
					// just select the node
					TreeView.SelectedNode = node;					
				}
				else if (m_HighlightRectType == HighlightRectType.Right)
				{
					foreach (Node node1 in node.Nodes)
					{
						MenuItem item = menu.MenuItems.Add(node1.Text);
					
						// create map between item and the node associated with it
						m_MapItem2Node.Add(item, node1);
					
						item.Click += new EventHandler(item_Click);
					}

					// set the node as the drop node and refresh
					m_FirstDropDownOn = false;
					m_DropDownNode = node;
					Refresh();
					
					menu.Show(this, new Point(rectangle.Right - 12, Height));
					menu.Disposed += new EventHandler(menu_Disposed);					
				}				
				
				return;
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove (e);
			
			Point pt = new Point(e.X, e.Y);
			
			// check the area and paint the background of it when needed
			foreach (Rectangle rectangle in m_MapBtn2Node.Keys)
			{
				if (rectangle.Contains(pt) == false)
					continue;
				
				// get the node and show the menu with subnodes of the node
				Node node = m_MapBtn2Node[rectangle] as Node;
				
				if (node == null)
				{
					m_HighlightRect = rectangle;
					m_HighlightRectType = HighlightRectType.Full;
					
					Invalidate();
					
					return;
				}
				
				m_HighlightRect = rectangle;
				
				if (node.Nodes.Count != 0)
				{
					// check the exact mouse position and determine if the area is text area or 
					// drop down button area, then it will be painted differently.
					if (pt.X < rectangle.Right - 12)
						m_HighlightRectType = HighlightRectType.Left;
					else
						m_HighlightRectType = HighlightRectType.Right;
				}
				else
					m_HighlightRectType = HighlightRectType.Full;
				
				Invalidate();
				
				return;
			}
			
			m_HighlightRect = Rectangle.Empty;
			Invalidate();
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void item_Click(object sender, EventArgs e)
		{
			MenuItem item = sender as MenuItem;
			
			Node node = m_MapItem2Node[item] as Node;
			
			m_TreeView.SelectedNode = node;
			m_DropDownNode = null;
			m_FirstDropDownOn = false;

			this.Refresh();
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_Style_Invalidate(object sender, EventArgs e)
		{
			Invalidate();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menu_Disposed(object sender, EventArgs e)
		{
			m_FirstDropDownOn = false;
			m_DropDownNode = null;

			Refresh();
		}

		#endregion

		#region properties

		/// <summary>
		/// Gets or sets the TreeView associated with the TreeViewPathSelector
		/// </summary>
        [Category("Data")]
		public TreeView TreeView
		{
			get { return m_TreeView; }

			set
			{
				if (m_TreeView != null)
					m_TreeView.NodeSelectionChange -= new System.EventHandler(m_TreeView_NodeSelectionChange);
				
				m_TreeView = value;
				
				if (m_TreeView != null)
					m_TreeView.NodeSelectionChange += new System.EventHandler(m_TreeView_NodeSelectionChange);
				
				this.Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the style of the TreeViewPathSelector.
		/// </summary>
		[Description("Gets or sets the style of the TreeViewPathSelector.")]
		[Category("Appearance")]
		public TreeViewPathSelectorStyle Style
		{
			get { return m_Style; }
			
			set
			{
				if (m_Style != null)
					m_Style.Invalidate -= new EventHandler(m_Style_Invalidate);
				
				m_Style = value;
				
				if (m_Style != null)
					m_Style.Invalidate += new EventHandler(m_Style_Invalidate);
			}
		}
		
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}
		
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				base.BackgroundImage = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}
		
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override RightToLeft RightToLeft
		{
			get
			{
				return base.RightToLeft;
			}
			set
			{
				base.RightToLeft = value;
			}
		}

		#endregion				
	}
}
