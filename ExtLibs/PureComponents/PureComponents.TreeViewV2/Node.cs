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
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using System.Text;

using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;

using System.Windows.Forms;
using PureComponents.TreeView.Design;
using ComponentModel_DesignerSerializationVisibility = System.ComponentModel.DesignerSerializationVisibility;

namespace PureComponents.TreeView
{	
	/// <summary>
	/// Node represents a node object in the TreeView hierarchy
	/// </summary>
	[Serializable, DesignTimeVisible(false)]
	[ToolboxItem(false)]		
	[DefaultProperty("NodeStyle")]	
	public class Node : Component, IComparable
	{
		#region private members

		private bool m_FormattingTested = false;
		private bool m_UseFormatting = false;

		protected TreeView m_oTreeView = null;

		/// <summary>
		/// The string representation of the node item
		/// </summary>
		protected string m_sText = null;

		/// <summary>
		/// The selected switch holder
		/// </summary>
		protected bool m_bIsSelected = false;

		/// <summary>
		/// The show plus minus switch
		/// </summary>
		protected bool m_bShowPlusMinus = true;		

		/// <summary>
		/// The expanded holder switch
		/// </summary>
		protected bool m_bIsExpanded = false;

		/// <summary>
		/// The underline holder switch
		/// </summary>
		protected bool m_bIsUnderline = false;		
		
		/// <summary>
		/// The check system exclusivity
		/// </summary>
		protected bool m_bSubNodesCheckExclusive = false;

		/// <summary>
		/// The drag allowance
		/// </summary>
		protected bool m_bAllowDrag = true;

		/// <summary>
		/// The drag allowance
		/// </summary>
		protected bool m_bAllowDrop = true;

		/// <summary>
		/// The Superior object - the parent node object
		/// </summary>
		protected Node m_oParentNode = null;

		/// <summary>
		/// The style holder
		/// </summary>
		protected NodeStyle m_oNodeStyle = null;

		/// <summary>
		/// The node collection holder
		/// </summary>
		protected NodeCollection m_aSubNodes = null;

		/// <summary>
		/// The tag object associated with the node
		/// </summary>
		protected object m_oTag = null;

		/// <summary>
		/// The key object representation
		/// </summary>
		protected string m_sKey = null;	

		/// <summary>
		/// The image index holder
		/// </summary>
		protected int m_nImageIndex = -1;

		/// <summary>
		/// The Direct image object 
		/// </summary>
		protected Image m_oImage;

		/// <summary>
		/// The node's tooltip
		/// </summary>
		protected string m_sTooltip = null;

		/// <summary>
		/// The dafault style switch holder
		/// </summary>
		protected NodeStyleSource m_eNodeStyleSource = NodeStyleSource.Default;

		/// <summary>
		/// The context menu source
		/// </summary>
		protected ContextMenuSource m_eMenuSource = ContextMenuSource.Local;

		/// <summary>
		/// The contect menu object
		/// </summary>
		protected ContextMenu m_oMenu = null;

		/// <summary>
		/// The Order of the node in the collection, when adding nodes using the parent property to the collection, nodes are being added
		/// as they were created in the designer, but when the order is changed using the drag drop or the move commands, the order must
		/// be preserved. So the YOrder property serves here to store the order into the m_nCollectionOrder. Once the node is being prepared
		/// to paint, TreeView checks the order in the collection and the CollectionOrder, if it is not in the right sync then moves the node
		/// to the appropriate position.
		/// </summary>
		internal int m_nCollectionOrder = -1;

		/// <summary>
		/// The flash holder, TreeView sets this during the flashing
		/// </summary>
		internal bool Flash = false;

		internal bool DesignHighlighted = false;
		internal bool DesignSelected = false;
		internal bool InplaceEditAdded = false;

		/// <summary>
		/// The node order in the hierarchy
		/// </summary>
		internal int NodeOrder = -1;

		/// <summary>
		/// Specifies the fact that the node is moving and that the group and parent properties 
		/// being set should not invoke the collection changes
		/// </summary>
		internal bool NodeMoving = false;

		/// <summary>
		/// The width of the text in the node
		/// </summary>
		internal int TextWidth = 0;

		/// <summary>
		/// Specifies the state of the Node
		/// </summary>
		protected bool m_bChecked = false;

		/// <summary>
		/// Defines the behavior when checkboxes are set to be visible in the checkbox visible style
		/// </summary>
		protected bool m_bCheckBoxVisible = true;

		/// <summary>
		/// Node's flag
		/// </summary>
		protected NodeFlag m_oNodeFlag = null;
		protected bool m_bFlagVisible = false;

		/// <summary>
		/// Node's visibility flag
		/// </summary>
		protected bool m_bVisible = true;
		//protected bool m_bDisabled = false;

		/// <summary>
		/// Indetifies if the text on the node has been truncated
		/// </summary>
		internal bool TextTruncated = false;

		/// <summary>
		/// Panel
		/// </summary>
		private NodePanel m_Panel;

		internal int Top = 0;
		internal int Left = 0;
		internal int Width = 0;

		/// <summary>
		/// selection availability
		/// </summary>
		private bool m_CanSelect = true;

		private NodeBackgroundStyle m_BackgroundStyle;
		
		#endregion

		#region construction
		/// <summary>
		/// Basic construction
		/// </summary>
		public Node()
		{
			//m_oNodeStyle = new NodeStyle(this);
			m_BackgroundStyle = new NodeBackgroundStyle();
			m_BackgroundStyle.Invalidate += new EventHandler(m_BackgroundStyle_Invalidate);
		}
		
		/// <summary>
		/// Construction using text
		/// </summary>
		/// <param name="text"></param>
		public Node(string text) : this()
		{
			this.Text = text;
		}

		/// <summary>
		/// Cosntruction
		/// </summary>
		/// <param name="text"></param>
		/// <param name="tag"></param>
		public Node(string text, object tag) : this()
		{
			m_sText = text;
			m_oTag = tag;
		}

		#endregion

		#region helper functions

		/// <summary>
		/// Gets count of all subnodes in the hierarchy
		/// </summary>
		/// <returns>Number of nodes</returns>
		public int GetNodeCount()
		{
			int nCount = Nodes.Count;

			foreach (Node oNode in Nodes)
				nCount += oNode.GetNodeCount();

			return nCount;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="g"></param>
		/// <returns></returns>
		internal int GetFullHeight(Graphics g)
		{
			int height = this.GetHeight(g) + GetTreeView().Style.NodeSpaceVertical;

			if (this.IsExpanded == true)
			{
				foreach (Node node in this.Nodes)
					height += node.GetFullHeight(node, g);
			}

			return height;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="g"></param>
		/// <returns></returns>
		internal int GetFullHeight(Node node, Graphics g)
		{
			int height = node.GetHeight(g) + GetTreeView().Style.NodeSpaceVertical;

			if (this.IsExpanded == true)
			{
				foreach (Node subNode in node.Nodes)
					height += subNode.GetFullHeight(subNode, g);
			}

			return height;
		}

		#endregion

		#region internal helper functions
		internal void SetNodeTreeViewReference(TreeView oView)
		{
			SetNodeTreeViewReference(oView, false);
		}

		internal void SetNodeTreeViewReference(TreeView oView, bool deep)
		{
			m_oTreeView = oView;

			if (m_oNodeFlag != null)
				m_oNodeFlag.SetNode(this);

			if (deep == true)
			{
				foreach (Node node in Nodes)
					node.SetNodeTreeViewReference(oView, deep);
			}
		}

		/// <summary>
		/// Gets the treeview core object the Node belongs to
		/// </summary>
		/// <returns>TreeView object</returns>
		internal TreeView GetTreeView()
		{
			if (m_oTreeView == null)
			{
				Node oParent = this.Parent;

				while (oParent != null)
				{
					if (oParent.TreeView != null)
					{
						m_oTreeView = oParent.TreeView;
						break;
					}

					if (oParent != null)
						oParent = oParent.Parent;
				}
			}

			return m_oTreeView;
		}

		/// <summary>
		/// Copies the one node to another
		/// </summary>
		/// <param name="oNode"></param>
		internal void Copy(Node oNode)
		{
			this._TreeView = oNode.GetTreeView();
			this.ContextMenu = oNode.ContextMenu;
			this.ContextMenuSource = oNode.ContextMenuSource;
			this.Image = oNode.Image;
			this.ImageIndex = oNode.ImageIndex;			
			this.Text = oNode.Text;
			this.Checked = oNode.Checked;
			this.CheckBoxVisible = oNode.CheckBoxVisible;

			if (oNode.Flag != null)
			{
				this.Flag = new NodeFlag();
				this.Flag.Color = oNode.Flag.Color;
				this.Flag.Data = oNode.Flag.Data;
				this.Flag.FlagStyle = oNode.Flag.FlagStyle;
				this.Flag.SetNode(this);
			}
			
			this.FlagVisible = oNode.FlagVisible;
			this.NodeStyleSource = oNode.NodeStyleSource;

			if (oNode.NodeStyle != null)
			{
				this.NodeStyle = new NodeStyle();
				this.NodeStyle.ApplyStyle(oNode.NodeStyle);
			}

			this.Tag = oNode.Tag;
			this.Tooltip = oNode.Tooltip;
			this.Underline = oNode.Underline;
		}

		internal bool IsLastInCollection()
		{
			Node [] sortedCollection = this.Collection.GetNodesCollectionSorted();

			if (sortedCollection[sortedCollection.Length - 1] == this)
				return true;

			return false;
		}

		internal string GetAutoNumberText()
		{
			string parentNumber = String.Empty;

			if (this.Parent != null)
				parentNumber = this.Parent.GetAutoNumberText();

			if (parentNumber != String.Empty)
				return parentNumber + "." + ((int)(this.Index + 1)).ToString();

			return ((int)(this.Index + 1)).ToString();
		}

		/// <summary>
		/// The node text getter
		/// </summary>
		/// <returns></returns>
		internal string GetText()
		{
			if (this.GetTreeView() == null)
			{
				if (this.UseFormatting == true)
					return this.FormattedText;

				return this.Text;
			}

			if (this.GetTreeView().NodeAutoNumbering == false)
			{
				if (this.UseFormatting == true)
					return this.FormattedText;

				return this.Text;			
			}
			
			if (this.UseFormatting == true)
				return GetAutoNumberText() + "  " + this.FormattedText;

			return GetAutoNumberText() + "  " + this.Text;
		}

		/// <summary>
		/// Gets the information wether the checkboxes should be painted
		/// </summary>
		/// <returns></returns>
		internal bool GetCheckBoxes()
		{
			return TreeView.CheckBoxes;
		}

		/// <summary>
		/// The context menu getter
		/// </summary>
		/// <returns>Context menu asociated with the node</returns>
		internal ContextMenu GetContextMenu()
		{
			if (ContextMenuSource == ContextMenuSource.None)
				return null;

			if (ContextMenuSource == ContextMenuSource.Local)
				return this.ContextMenu;			

			Node oNode = this;

			while (oNode.Parent != null)
			{
				if (oNode.ContextMenu != null)
					return oNode.ContextMenu;

				oNode = oNode.Parent;
			}

			if (oNode != null && oNode.ContextMenu != null)
				return oNode.ContextMenu;

			if (this.GetTreeView() == null)
				return null;

			return this.GetTreeView().ContextMenu;
		}

		/// <summary>
		/// Gets the node order for the lowest node in the node's hierarchy
		/// </summary>
		/// <returns>Max node order</returns>
		internal int GetMaxSubNodeOrder()
		{
			Node oNode = this;

			while (oNode.Nodes.Count > 0)
				oNode = oNode.Nodes[oNode.Nodes.Count - 1];

			return oNode.NodeOrder;
		}

		/// <summary>
		/// Function recalcs the order of all nodes. when moveing noved around it is important to preserve the correct topdown node order
		/// </summary>
		internal void RecalcNodeOrder(ref int Order)
		{
			// set the order here and increase it
			this.NodeOrder = Order;
			Order ++;

			// if node is not expanded, then skip the process
			if (this.IsExpanded == false)
				return;

			// get the selected Group and recalc the node order
			Node [] aNodes = this.Nodes.GetNodesCollectionSorted();

			foreach (Node oNode in aNodes)
				oNode.RecalcNodeOrder(ref Order);
		}		

		/// <summary>
		/// Fits the inner panel
		/// </summary>
		internal void FitPanel()
		{
			if (this.Panel == null)
				return;

			TreeView trv = GetTreeView();
			
			if (this.IsExpanded == false)
			{	
				if (trv.Controls.Contains(this.Panel) == true)
					trv.Controls.Remove(this.Panel);

				return;
			}
		
			// also check that all parents are expanded as well
			Node parent = this.Parent;
			while (parent != null)
			{
				if (parent.IsExpanded == false)
				{
					if (trv.Controls.Contains(this.Panel) == true)
						trv.Controls.Remove(this.Panel);

					return;
				}

				parent = parent.Parent;
			}
	
			if (this.Panel.Top != this.Top)
				this.Panel.Top = this.Top;

			if (this.Panel.Left != this.Left + 3)
				this.Panel.Left = this.Left + 3;

			if (this.Panel.Width != this.Width - 5)
				this.Panel.Width = this.Width - 5;						

			if (trv.Controls.Contains(this.Panel) == false)
				trv.Controls.Add(this.Panel);
		}

		internal void ClearNodeOrder()
		{
			NodeOrder = -1;

			foreach (Node oNode in Nodes)
				oNode.ClearNodeOrder();
		}

		/// <summary>
		/// Gets the node by the order specified
		/// </summary>
		/// <param name="nOrder">Order to check</param>
		/// <returns>Node object if found, null otherwise</returns>
		internal Node GetNodeByOrder(int nOrder)
		{
			foreach (Node oNode in Nodes)
			{
				if (oNode.NodeOrder == nOrder)
					return oNode;

				Node oSubNode = oNode.GetNodeByOrder(nOrder);

				if (oSubNode != null)
					return oSubNode;
			}

			return null;
		}		

		internal void SetChecked(bool checkedState)
		{
			m_bChecked = checkedState;

			this.Invalidate();
		}

		/// <summary>
		/// Calls the proper events when needed
		/// </summary>
		internal void ToggleChecked()
		{
			TreeView view = GetTreeView();

			if (view != null)
			{			
				CancelEventArgs cancelArgs = new CancelEventArgs(false);

				view.InvokeBeforeNodeCheck(this, cancelArgs);

				Invalidate();

				if (cancelArgs.Cancel == true)
					return;
			}

			m_bChecked = !m_bChecked;

			if (view != null)
				view.InvokeAfterNodeCheck(this);

			// now test the check box exclusivity flag
			if (this.Parent != null && this.Parent.SubNodesCheckExclusive == true)
			{
				// uncheck all siblings
				foreach (Node node in this.Parent.Nodes)
				{
					if (node != this)
						node.SetChecked(false);
				}
			}
		}

		/// <summary>
		/// Gets the array of nodes in the Node object by their Tag object.
		/// </summary>
		/// <param name="oTag">Tag object to find the nodes</param>
		/// <returns>Array of Node objects that have the Tag specified</returns>
		public Node [] GetNodesByTag(object oTag)
		{
			ArrayList aNodes = new ArrayList();			

			foreach (Node oNode in Nodes)
			{
				Node [] aGroupNodes = oNode.GetNodesByTag(oTag);

				if (aGroupNodes != null && aGroupNodes.Length > 0)
					aNodes.AddRange(aGroupNodes);
			}

			if (this.Tag == null && oTag == null)
				aNodes.Add(this);
			else
			{			
				if (this.Tag != null && this.Tag.Equals(oTag))
					aNodes.Add(this);
			}

			Node [] aData = new Node[aNodes.Count];
			aNodes.CopyTo(aData);

			return aData;
		}

		/// <summary>
		/// Gets the information if the node is in the selected path
		/// </summary>
		/// <returns>Status of the operation</returns>
		internal bool IsInSelectedPath()
		{
			if (m_bIsSelected == true)
				return true;

			foreach (Node oNode in Nodes)
			{
				if (oNode.IsInSelectedPath() == true)
					return true;
			}

			return false;
		}

		/// <summary>
		/// Performs the iteration and searches for the selected node
		/// </summary>
		/// <returns></returns>
		internal Node GetSelectedNode()
		{
			if (m_bIsSelected == true)
				return this;

			Node oSelNode = null;

			foreach (Node oNode in Nodes)
			{
				oSelNode = oNode.GetSelectedNode();

				if (oSelNode != null)
					return oSelNode;
			}

			return null;
		}

		/// <summary>
		/// Expands the path to the parent Node
		/// </summary>
		internal void ExpandParent()
		{
			Node oParent = this.Parent;

			while (oParent != null)
			{
				if (oParent.IsExpanded == false)
				{
					oParent.Expand();

					Redraw();
				}

				oParent = oParent.Parent;
			}
		}

		/// <summary>
		/// Gets the NodeStyle for the node based on the style source
		/// </summary>
		/// <returns>NodeStyle instance ued to draw the Node</returns>
		public NodeStyle GetNodeStyle()
		{
			if (this.NodeStyleSource == NodeStyleSource.Default)
				return GetTreeView() == null ? new NodeStyle(): GetTreeView().Style.NodeStyle;

			if (this.NodeStyleSource == NodeStyleSource.Parent)
			{
				if (this.Parent != null)
					return this.Parent.GetNodeStyle();

				return GetTreeView() == null ? new NodeStyle(): GetTreeView().Style.NodeStyle;
			}

			if (this.NodeStyle == null)
				return GetTreeView() == null ? new NodeStyle(): GetTreeView().Style.NodeStyle;

			return this.NodeStyle;
		}

		/// <summary>
		/// Gets the NodeTooltipStyle 
		/// </summary>
		/// <returns></returns>
		public NodeTooltipStyle GetTooltipStyle()
		{
			return GetNodeStyle().TooltipStyle;
		}

		/// <summary>
		/// Gets the back color of the node which is highlighted
		/// </summary>
		/// <returns></returns>
		internal Color GetHighlightedBackColor()
		{
			return GetNodeStyle().HoverBackColor;
		}

		/// <summary>
		/// Gets the fore color of the node which is highlighted
		/// </summary>
		/// <returns></returns>
		internal Color GetHighlightedForeColor()
		{
			return GetNodeStyle().HoverForeColor;
		}

		/// <summary>
		/// Gets the back color of the selected node
		/// </summary>
		/// <returns></returns>
		internal Color GetSelectedBackColor()
		{
			return GetNodeStyle().SelectedBackColor;
		}

		/// <summary>
		/// Gets the border color of the selected node
		/// </summary>
		/// <returns>Color object</returns>
		internal Color GetSelectedBorderColor()
		{
			return GetNodeStyle().SelectedBorderColor;
		}

		/// <summary>
		/// Gets the node font
		/// </summary>
		/// <returns>Font class</returns>
		internal Font GetFont()
		{
			return GetNodeStyle().Font;
		}
		
		/// <summary>
		/// Gets the node text color
		/// </summary>
		/// <returns>Color class</returns>
		internal Color GetForeColor()		
		{
			if (Flash == true)
				return GetFlashColor();

			if (IsSelected == true && GetTreeView().HideSelection == false)
				return GetSelectedForeColor();

			return GetNodeStyle().ForeColor;
		}

		/// <summary>
		/// Gets the node's fill background style while being selected
		/// </summary>
		/// <returns>Fiil style of the selected Node</returns>
		internal FillStyle GetSelectedFillStyle()
		{
			return GetNodeStyle().SelectedFillStyle;
		}

		/// <summary>
		/// Gets the node text color for the selected state
		/// </summary>
		/// <returns>Color class</returns>
		internal Color GetSelectedForeColor()		
		{
			if (Flash == true)
				return GetFlashColor();

			return GetNodeStyle().SelectedForeColor;
		}
		
		/// <summary>
		/// Gets the node line color
		/// </summary>
		/// <returns>Color class</returns>
		internal Color GetLineColor()
		{			
			if (Flash == true)
				return GetFlashColor();

			return GetTreeView().Style.LineColor;
		}

		/// <summary>
		/// The color of the node's box
		/// </summary>		
		internal Color GetExpandBoxForeColor()
		{
			return GetNodeStyle().ExpandBoxStyle.ForeColor;
		}

		/// <summary>
		/// The back color of the node's box
		/// </summary>		
		internal Color GetExpandBoxBackColor()
		{
			return GetNodeStyle().ExpandBoxStyle.BackColor;
		}

		/// <summary>
		/// The color of the node's box
		/// </summary>		
		internal Color GetExpandBoxBorderColor()
		{
			return GetNodeStyle().ExpandBoxStyle.BorderColor;
		}

		/// <summary>
		/// The shape of the node's box
		/// </summary>		
		internal ExpandBoxShape GetExpandBoxShape()
		{
			return GetNodeStyle().ExpandBoxStyle.Shape;
		}

		/// <summary>
		/// Gets the color of the node's check background
		/// </summary>
		/// <returns></returns>
		internal Color GetCheckBackColor()
		{
			if (this == GetTreeView().HighlightedNode)
				return GetNodeStyle().CheckBoxStyle.HoverBackColor;

			return GetNodeStyle().CheckBoxStyle.BackColor;
		}

		/// <summary>
		/// Gets the color of the node's check border
		/// </summary>
		/// <returns></returns>
		internal Color GetCheckBorderColor()
		{
			if (this == GetTreeView().HighlightedNode)
				return GetNodeStyle().CheckBoxStyle.HoverBorderColor;

			return GetNodeStyle().CheckBoxStyle.BorderColor;
		}

		/// <summary>
		/// Gets the color of the node's check check
		/// </summary>
		/// <returns></returns>
		internal Color GetCheckCheckColor()
		{
			if (this == GetTreeView().HighlightedNode)
				return GetNodeStyle().CheckBoxStyle.HoverCheckColor;

			return GetNodeStyle().CheckBoxStyle.CheckColor;
		}		

		/// <summary>
		/// Gets the style of the node's check border
		/// </summary>
		/// <returns></returns>
		internal CheckBoxBorderStyle GetCheckBorderStyle()
		{
			return GetNodeStyle().CheckBoxStyle.BorderStyle;
		}

		/// <summary>
		/// Gets the node line style
		/// </summary>
		/// <returns>LineStyle class</returns>
		internal LineStyle GetLineStyle()
		{			
			if (Flash == true)
				return LineStyle.Solid;			

			return GetTreeView().Style.LineStyle;		
		}


		/// <summary>
		/// Gets the node underline style
		/// </summary>
		/// <returns>UnderlineStyle class</returns>
		internal UnderlineStyle GetUnderlineStyle()
		{
			return GetNodeStyle().UnderlineStyle;
		}
		
		/// <summary>
		/// Gets the node underline style
		/// </summary>
		/// <returns>UnderlineStyle class</returns>
		internal bool GetHighlightSelectedPath()
		{						
			return GetTreeView().Style.HighlightSelectedPath;
		}		

		/// <summary>
		/// Gets the node underline color
		/// </summary>
		/// <returns>Color class</returns>
		internal Color GetUnderlineColor()
		{
			return GetNodeStyle().UnderlineColor;			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		internal Pen GetUnderlineBackColor()
		{
			Pen pen = null;
			
			if (this.BackgroundStyle.Visible == true)
				pen = new Pen(this.BackgroundStyle.BackColor, 1);
			else
			{
				// locate if some parent does not have the background selected
				Node parent = this.Parent;

				while (parent != null)
				{
					if (parent.BackgroundStyle.Visible == true)
					{
						pen = new Pen(parent.BackgroundStyle.BackColor, 1);

						break;
					}

					parent = parent.Parent;
				}
			}

			if (pen != null)
				return pen;

			pen = new Pen(GetTreeView().GetTreeBackColor(), 1);

			return pen;
		}

		/// <summary>
		/// Gets the node flash color
		/// </summary>
		/// <returns>Color class</returns>
		internal Color GetFlashColor()
		{
			return GetTreeView().Style.FlashColor;
		}

		/// <summary>
		/// Gets the information if the brother nodes should be collapsed
		/// </summary>
		/// <returns>AutoCollapse information</returns>
		internal bool GetAutoCollapse()
		{
			return GetTreeView().Style.AutoCollapse;
		}

		/// <summary>
		/// Gets the node plus minus switch
		/// </summary>
		/// <returns>Color class</returns>
		internal bool GetShowPlusMinus()
		{
			return GetTreeView().Style.ShowPlusMinus && ShowPlusMinus;
		}

		/// <summary>
		/// Gets the node subitems indicator switch
		/// </summary>
		/// <returns>Color class</returns>
		internal bool GetShowSubitemsIndicator()
		{
			return GetTreeView().Style.ShowSubitemsIndicator;
		}

		/// <summary>
		/// Gets the node lines indicator switch
		/// </summary>
		/// <returns>Color class</returns>
		internal bool GetShowLines()
		{
			return GetTreeView().Style.ShowLines;
		}

		/// <summary>
		/// Gets the width of the node
		/// </summary>
		/// <param name="oGraphics"></param>
		/// <returns></returns>
		internal int GetWidth(Graphics oGraphics)
		{
			if (this.Text == null || this.Text.Length == 0)
				return 18;

			int nAdd = -1;

			if (GetTreeView().Style.ShowPlusMinus == true)
				nAdd += 15;

			if (GetTreeView().CheckBoxes == true)
				nAdd += 20;					
	
			if (GetTreeView().Flags == true)
				nAdd += 13;		
	
			if (GetTreeView().Flags == false && GetTreeView().CheckBoxes == false)
				nAdd += 3;

			if (GetTreeView().Flags == true && GetTreeView().CheckBoxes == true)
				nAdd -= 3;

			if (GetTreeView().Flags == true && GetTreeView().Style.ShowPlusMinus == true && GetTreeView().CheckBoxes == false)
				nAdd += 2;

			if (GetTreeView().Flags == true && GetTreeView().Style.ShowPlusMinus == true && GetTreeView().CheckBoxes == true)
				nAdd += 2;

			if (GetTreeView().Flags == false && GetTreeView().Style.ShowPlusMinus == true && GetTreeView().CheckBoxes == true)
				nAdd += 2;

			if (GetTreeView().Flags == false && GetTreeView().Style.ShowPlusMinus == true && GetTreeView().CheckBoxes == false)
				nAdd += 2;

			if (GetTreeView().Flags == false && GetTreeView().Style.ShowPlusMinus == false && GetTreeView().CheckBoxes == false)
				nAdd += 2;

			if (GetTreeView().Flags == true && GetTreeView().Style.ShowPlusMinus == false && GetTreeView().CheckBoxes == true)
				nAdd += 2;

			if (GetTreeView().Flags == false && GetTreeView().Style.ShowPlusMinus == false && GetTreeView().CheckBoxes == true)
				nAdd += 2;

			if (GetTreeView().Flags == true && GetTreeView().Style.ShowPlusMinus == false && GetTreeView().CheckBoxes == false)
				nAdd += 2;

			int nImageSize = 0;
			if (this.Image != null)
			{
				nAdd += this.Image.Width + 2;
				nImageSize = this.Image.Width + 2;
			}

			if (this.ImageIndex != -1)
			{
				nAdd += this.ImageList.ImageSize.Width + 2;
				nImageSize = this.ImageList.ImageSize.Width + 2;
			}

			// get the size of the string
			SizeF size;
			
			if (this.GetTreeView().Multiline == true)
				size = oGraphics.MeasureString(this.GetText(), this.GetFont(), 
					GetTreeView().GetDrawWidth() - 14 * (this.IndentLevel + 1) - nAdd - (this.IndentLevel * GetTreeView().Style.NodeSpaceHorizontal));
			else
				size = oGraphics.MeasureString(this.GetText(), this.GetFont());

			// return the size as int
			return (int)size.Width + 7 + nImageSize;
		}

		/// <summary>
		/// Gets the height of the node
		/// </summary>
		/// <returns>Node height</returns>
		internal int GetHeight(Graphics oGraphics)
		{
			if (this.Text == null || this.Text.Length == 0)
				return 18;

			int nAdd = -1;

			if (GetTreeView().Style.ShowPlusMinus == true)
				nAdd += 15;

			if (GetTreeView().CheckBoxes == true)
				nAdd += 20;					
	
			if (GetTreeView().Flags == true)
				nAdd += 13;		
	
			if (GetTreeView().Flags == false && GetTreeView().CheckBoxes == false)
				nAdd += 3;

			if (GetTreeView().Flags == true && GetTreeView().CheckBoxes == true)
				nAdd -= 3;

			if (GetTreeView().Flags == true && GetTreeView().Style.ShowPlusMinus == true && GetTreeView().CheckBoxes == false)
				nAdd += 2;

			if (GetTreeView().Flags == true && GetTreeView().Style.ShowPlusMinus == true && GetTreeView().CheckBoxes == true)
				nAdd += 2;

			if (GetTreeView().Flags == false && GetTreeView().Style.ShowPlusMinus == true && GetTreeView().CheckBoxes == true)
				nAdd += 2;

			if (GetTreeView().Flags == false && GetTreeView().Style.ShowPlusMinus == true && GetTreeView().CheckBoxes == false)
				nAdd += 2;

			if (GetTreeView().Flags == false && GetTreeView().Style.ShowPlusMinus == false && GetTreeView().CheckBoxes == false)
				nAdd += 2;

			if (GetTreeView().Flags == true && GetTreeView().Style.ShowPlusMinus == false && GetTreeView().CheckBoxes == true)
				nAdd += 2;

			if (GetTreeView().Flags == false && GetTreeView().Style.ShowPlusMinus == false && GetTreeView().CheckBoxes == true)
				nAdd += 2;

			if (GetTreeView().Flags == true && GetTreeView().Style.ShowPlusMinus == false && GetTreeView().CheckBoxes == false)
				nAdd += 2;

			if (this.Image != null)
			{
				nAdd += this.Image.Width + 2;
			}

			if (this.ImageIndex != -1)
			{
				nAdd += this.ImageList.ImageSize.Width + 2;
			}

			// get the size of the string
			SizeF size;
			
			if (this.GetTreeView().Multiline == true)
				size = oGraphics.MeasureString(this.UnFormattedText, this.GetFont(), 
					GetTreeView().GetDrawWidth() - 14 * (this.IndentLevel + 1) - nAdd - (this.IndentLevel * GetTreeView().Style.NodeSpaceHorizontal));
			else
				size = oGraphics.MeasureString(this.UnFormattedText, this.GetFont());

			if (this.Panel != null && this.IsExpanded == true)
				size.Height += this.Panel.Height + 2;

			// return the size as int
			int height = (int)size.Height + 4;

			if (height % 2 != 0)
				height ++;

			return height;
		}

		/// <summary>
		/// Gets the rectangle of the box area
		/// </summary>
		/// <returns></returns>
		internal Rectangle GetBoxRect()
		{
			return m_oTreeView.GetExpandBoxRect(this);
		}

		/// <summary>
		/// Checks if the oNode is not some child of the current node
		/// </summary>
		/// <param name="oNode">Node to check</param>
		/// <returns>Status of the operation</returns>
		internal bool IsSomeParent(Node oNode)
		{
			while (oNode.Parent != null)
			{
				if (oNode.Parent == this)
					return true;

				oNode = oNode.Parent;
			}

			return false;
		}

		/// <summary>
		/// Sets the parent for the node
		/// </summary>
		/// <param name="oNode"></param>
		internal void SetParent(Node oNode)
		{
			m_oParentNode = oNode;
		}

		/// <summary>
		/// Clears the selection flag
		/// </summary>
		internal void ClearDesignHighlight()
		{
			// set the node's highlight to false
			DesignHighlighted = false;

			foreach (Node oSubNode in Nodes)
				oSubNode.ClearDesignHighlight();

			// redraw the object
			Invalidate();
		}

		/// <summary>
		/// Clears the selection flag
		/// </summary>
		internal void ClearDesignSelection()
		{
			// set the node's selection to false
			DesignSelected = false;

			foreach (Node oSubNode in Nodes)
				oSubNode.ClearDesignSelection();

			// redraw the object
			Invalidate();
		}

		/// <summary>
		/// Removes the separator text from the Group or Node Text
		/// </summary>
		/// <param name="sNewSeparator"></param>
		internal void UpdateNodeGroupTextSeparator(string sNewSeparator)
		{
			if (m_sText != null && sNewSeparator != null && sNewSeparator.Length > 0)
				m_sText = m_sText.Replace(sNewSeparator, "");

			foreach (Node oNode in Nodes)
				oNode.UpdateNodeGroupTextSeparator(sNewSeparator);
		}
		#endregion

		#region painting
		/// <summary>
		/// Draws the object in the associate rectangle
		/// </summary>
		internal void Invalidate()
		{
			if (GetTreeView() != null)
				GetTreeView().Invalidate();
		}

		/// <summary>
		/// Draws the object in the associate rectangle
		/// </summary>
		internal void Redraw()
		{
			if (GetTreeView() != null)
				GetTreeView().Refresh();
		}
		#endregion	

		#region implementation
		/// <summary>
		/// Compares the object to the other Node object
		/// </summary>
		/// <param name="obj">Node to be compared with</param>
		/// <returns>Status of the operation</returns>
		public int CompareTo(object obj)
		{
			Node oNode = obj as Node;

			if (oNode == null)
				return 0;

			string sThisText = this.Text == null ? "" : this.Text;
			string sNodeText = oNode.Text == null ? "" : oNode.Text;

			return String.Compare(sThisText.ToLower(), sNodeText.ToLower());
		}

		/// <summary>
		/// Creates the new subnode and adds the node to the node collection
		/// </summary>
		/// <returns></returns>
		public Node CreateSubNode()
		{
			Node oNode = new Node();
			oNode.SetParent(this);

			Nodes.Add(oNode);

			return oNode;
		}		

		/// <summary>
		/// Removes the node from its current collection and forces the object to be redrawn
		/// </summary>
		public void Remove()
		{
			if (this.Collection.Count > 0 && this.Collection.Contains(this) == true)
			{
				this.Collection.Remove(this);

				// set each subnode's key to null
				foreach (Node node in this.Nodes)
					node.ClearKeys();

				Invalidate();
			}
		}

		internal void ClearKeys()
		{
			this.Key = null;

			foreach (Node node in this.Nodes)
			{
				node.Key = null;

				node.ClearKeys();
			}
		}

		internal void SelectInternal(bool value)
		{
			m_bIsSelected = value;
		}

		/// <summary>
		/// Selects the node
		/// </summary>
		public void Select()
		{
			//if (m_bDisabled == true)
			//	return;

			if (m_bIsSelected == true)
			{
				// expand path to the parent nodes
				ExpandParent();

				// scroll to the node
				GetTreeView().SetNodeScrollVisible(this);
				
				// redraws the object
				Redraw();

				return;
			}

			CancelEventArgs eArgs = new CancelEventArgs(false);

			// fire the before node selection
			GetTreeView().InvokeBeforeNodeSelect(this, eArgs);

			// clears all selection for all nodes in the group
			if (eArgs.Cancel == false)
			{
				GetTreeView().ClearNodeSelection();								
			}			

			// expand all in the path up in the level
			ExpandParent();

			// make the item selected
			if (eArgs.Cancel == false)
			{
				this.m_bIsSelected = true;
				this.GetTreeView().AddSelectedNode(this);				
			}

			// redraws the object
			Invalidate();

			// fire the after node selected
			if (eArgs.Cancel == false)
				GetTreeView().InvokeAfterNodeSelect(this);	
			
			// check that there is not a scrollbar visible, if yes, then 
			// check the Node is in the visible are, if not, then set the scrollbar
			// at the appropriate position
			GetTreeView().SetNodeScrollVisible(this);
		}

		/// <summary>
		/// Selects the node
		/// </summary>
		internal void Select(bool control, bool shift)
		{
			if (GetTreeView().IsDesignMode == false && CanSelect == false)
				return;

			if (m_bIsSelected == true)
			{
				if (GetTreeView().SelectionMode == PureComponents.TreeView.SelectionMode.Single)
				{
					// expand path to the parent nodes
					ExpandParent();

					// scroll to the node
					GetTreeView().SetNodeScrollVisible(this);
				}
				else if (GetTreeView().SelectionMode == PureComponents.TreeView.SelectionMode.Multiple)
				{
					m_bIsSelected = false;

					this.GetTreeView().RemoveSelectedNode(this);					
				}
				else if (GetTreeView().SelectionMode == PureComponents.TreeView.SelectionMode.MultipleExtended)
				{	
					if (control == true)
					{
						m_bIsSelected = false;

						this.GetTreeView().RemoveSelectedNode(this);						
					}
					else if (shift == true)
					{
						m_bIsSelected = false;

						this.GetTreeView().RemoveSelectedNode(this);
					}
					else
					{
						this.GetTreeView().SetSelectedNode(this);

						// expand path to the parent nodes
						ExpandParent();

						// scroll to the node
						GetTreeView().SetNodeScrollVisible(this);						
					}
				}
				
				// redraws the object
				Redraw();

				return;
			}

			CancelEventArgs eArgs = new CancelEventArgs(false);

			// fire the before node selection
			GetTreeView().InvokeBeforeNodeSelect(this, eArgs);

			// clears all selection for all nodes in the group
			if (eArgs.Cancel == false)
			{
				if (GetTreeView().SelectionMode == PureComponents.TreeView.SelectionMode.Single)
				{
					GetTreeView().ClearNodeSelection();
				}
				else if (GetTreeView().SelectionMode == PureComponents.TreeView.SelectionMode.Multiple)
				{
				}
				else if (GetTreeView().SelectionMode == PureComponents.TreeView.SelectionMode.MultipleExtended)
				{
					if (control == true)
					{
					}
					else if (shift == true)
					{
						// for every node between selected node and the clicked node, alter the selection
						int startOrder = 0;

						if (GetTreeView().SelectedNode != null)
							startOrder = GetTreeView().SelectedNode.NodeOrder;

						int endOrder = this.NodeOrder;

						if (startOrder <= endOrder)
						{									
							for (int order = startOrder; order <= endOrder; order++)
							{
								Node node = GetTreeView().GetNodeByOrder(order);

								if (node == null)
									continue;

								node.TreeView = GetTreeView();
								node.SelectInternal(true);	
								GetTreeView().AddSelectedNode(node);																						
							}
						}
						else
						{
							for (int order = endOrder; order <= startOrder; order++)
							{
								Node node = GetTreeView().GetNodeByOrder(order);

								if (node == null)
									continue;

								node.TreeView = GetTreeView();
								node.SelectInternal(true);	
								GetTreeView().AddSelectedNode(node);
							}
						}
									
						GetTreeView().Invalidate();
					}
					else
					{
						GetTreeView().ClearNodeSelection();
					}
				}				
			}			

			// expand all in the path up in the level
			ExpandParent();

			// make the item selected
			if (eArgs.Cancel == false)
			{
				this.m_bIsSelected = true;
				this.GetTreeView().SetSelectedNode(this);
				this.GetTreeView().AddSelectedNode(this);				
			}

			// redraws the object
			Invalidate();

			// fire the after node selected
			if (eArgs.Cancel == false)
				GetTreeView().InvokeAfterNodeSelect(this);	
			
			// check that there is not a scrollbar visible, if yes, then 
			// check the Node is in the visible are, if not, then set the scrollbar
			// at the appropriate position
			GetTreeView().SetNodeScrollVisible(this);
		}

		/// <summary>
		/// Clears the selection flag for the node and its subnodes
		/// </summary>
		public void ClearSelection()
		{
			TreeView view = this.GetTreeView();			

			if (m_bIsSelected == true && view != null)
				view.SetSelectedNode(null);

			// set the node's selection to false
			m_bIsSelected = false;

			foreach (Node oSubNode in Nodes)
				oSubNode.ClearSelection();

			// redraw the object
			if (view != null && view.CancelDraw == false)
				Invalidate();
		}				

		/// <summary>
		/// Expands the node
		/// </summary>
		public void Expand()
		{
			if (m_bIsExpanded == true)
			{
				FitPanel();

				return;
			}

			CancelEventArgs eArgs = new CancelEventArgs(false);

			// fire the before expand
			GetTreeView().InvokeBeforeNodeExpand(this, eArgs);			

			// expand the node
			if (eArgs.Cancel == false)
			{
				// if close all on click, then close all at the same level
				if (this.GetAutoCollapse() == true)
					foreach (Node oCollapseNode in this.Collection)
						if (oCollapseNode.IsExpanded == true)
							oCollapseNode.Collapse();

				m_bIsExpanded = true;
			}

			Invalidate();

			// fire the after expand
			if (eArgs.Cancel == false)
				GetTreeView().InvokeAfterNodeExpand(this);			

			m_bIsExpanded = true;	
		
			FitPanel();
		}

		/// <summary>
		/// Collapses the node
		/// </summary>
		public void Collapse()		
		{
			if (this.Panel != null)
				GetTreeView().Controls.Remove(this.Panel);

			if (m_bIsExpanded == false)
				return;

			CancelEventArgs eArgs = new CancelEventArgs(false);

			// fire the before collapse 
			GetTreeView().InvokeBeforeNodeCollapse(this, eArgs);

			// collapse the node
			if (eArgs.Cancel == false)
				m_bIsExpanded = false;

			// fire the after collapse
			if (eArgs.Cancel == false)
				GetTreeView().InvokeAfterNodeCollapse(this);			

			Invalidate();
		}

		/// <summary>
		/// Expands the node
		/// </summary>
		public void ExpandAll()
		{
			if (m_bIsExpanded == false)
			{
				CancelEventArgs eArgs = new CancelEventArgs(false);

				// fire the before expand
				GetTreeView().InvokeBeforeNodeExpand(this, eArgs);			

				// expand the node
				if (eArgs.Cancel == false)
				{
					// if close all on click, then close all at the same level
					if (this.GetAutoCollapse() == true)
						foreach (Node oCollapseNode in this.Collection)
							if (oCollapseNode.IsExpanded == true)
								oCollapseNode.Collapse();

					m_bIsExpanded = true;
				}

				Invalidate();

				// fire the after expand
				if (eArgs.Cancel == false)
					GetTreeView().InvokeAfterNodeExpand(this);			

				m_bIsExpanded = true;			
			}

			foreach (Node oNode in Nodes)
				oNode.ExpandAll();

			Invalidate();
		}

		/// <summary>
		/// Collapses the node
		/// </summary>
		public void CollapseAll()
		{
			if (m_bIsExpanded == true)
			{
				CancelEventArgs eArgs = new CancelEventArgs(false);

				// fire the before collapse 
				GetTreeView().InvokeBeforeNodeCollapse(this, eArgs);

				// collapse the node
				if (eArgs.Cancel == false)
					m_bIsExpanded = false;

				// fire the after collapse
				if (eArgs.Cancel == false)
					GetTreeView().InvokeAfterNodeCollapse(this);			

				Invalidate();
			}

			foreach (Node oNode in Nodes)
				oNode.CollapseAll();

			Invalidate();
		}			

		/// <summary>
		/// Applies the given style to the node instance
		/// </summary>		
		/// <param name="oNodeStyle">NodeStyle to apply to node and all subnodes</param>
		public void ApplyStyle(NodeStyle oNodeStyle)
		{
			if (NodeStyle != null && m_eNodeStyleSource == NodeStyleSource.Local)
				NodeStyle.ApplyStyle(oNodeStyle);

			foreach (Node oNode in Nodes)
				oNode.ApplyStyle(oNodeStyle);
		}

		/// <summary>
		/// Applies the given style to the node instance, ignoring the style source if the bForceApply is set to True.
		/// </summary>		
		/// <param name="oNodeStyle">NodeStyle to apply to node and all subnodes</param>
		/// <param name="bForceApply">Forces to apply the style</param>
		public void ApplyStyle(NodeStyle oNodeStyle, bool bForceApply)
		{
			if (m_eNodeStyleSource != NodeStyleSource.Local && bForceApply == false)
				return;

			if (NodeStyle != null)
				NodeStyle.ApplyStyle(oNodeStyle);

			foreach (Node oNode in Nodes)
				oNode.ApplyStyle(oNodeStyle, bForceApply);
		}

		/// <summary>
		/// Moves the Node up in the collection
		/// </summary>
		public void MoveUp()
		{
			if (this.NodeOrder == 0)
				return;

			// stop the reordering events
			this.TreeView.CanFireNodeEvent = false;
			this.NodeMoving = true;

			Node oSelNode = GetTreeView().SelectedNode;

			// get the previous node
			int nPrevPosition = this.Index;
			Node oPrevNode = this.TreeView.GetNodeByOrder(this.NodeOrder - 1);

			// if we have the previous node, gets its index and the parent and move the
			// node infront of the previous node
			if (oPrevNode == null)
				return;

			// fire the node before pos changed
			this.TreeView.InvokeBeforeNodePositionChange(this);

			// remember the tag and the key
			object tag = this.Tag;
			string key = this.Key;

			// remove itself from the local collection
			this.Collection.Remove(this);

			// add the node to the previous node collection
			oPrevNode.Collection.Insert(oPrevNode.Index, this);			

			// update the collection orders
			this.m_nCollectionOrder = this.Index;
			oPrevNode.m_nCollectionOrder = oPrevNode.Index;

			// update the parent accordingly
			this.Parent = oPrevNode.Parent;						

			if (this.Parent != null)
			{
				foreach (Node oSubNode in Parent.Collection)
					oSubNode.m_nCollectionOrder = oSubNode.Index;
			}

			foreach (Node oSubNode in this.Collection)
				oSubNode.m_nCollectionOrder = oSubNode.Index;

			this.TreeView.CanFireNodeEvent = true;
			this.NodeMoving = false;	
			GetTreeView().SetSelectedNode(oSelNode);
			GetTreeView().SetNodeScrollVisible(this);

			// set the key and tag back
			this.Tag = tag;
			this.Key = key;
		
			this.Redraw();

			// fire the node pos changed
			this.TreeView.InvokeAfterNodePositionChange(this);
		}

		/// <summary>
		/// Moves the Node down in the collection
		/// </summary>
		public void MoveDown()
		{
			// get the next node
			Node oNextNode = this.TreeView.GetNodeByOrder(this.NodeOrder + 1);

			// if we have the next node, gets its index and the parent and move the
			// node after the next node
			if (oNextNode == null)
				return;

			// remember the tag and the key
			object tag = this.Tag;
			string key = this.Key;

			Node oSelNode = GetTreeView().SelectedNode;
			
			// stop the reordering events
			this.TreeView.CanFireNodeEvent = false;
			this.NodeMoving = true;

			int nPrevPosition = this.Index;

			if (this.Nodes.Count == 0 || this.IsExpanded == false)
			{
				// fire the node pos changed
				this.TreeView.InvokeBeforeNodePositionChange(this);

				// remove itself from the local collection
				this.Collection.Remove(this);

				// add the node to the subnode collection if needed and exists
				if (oNextNode.IsExpanded && oNextNode.Nodes.Count > 0)
				{
					// insert at the first position
					oNextNode.Nodes.Insert(0, this);

					// update the parent accordingly
					this.Parent = oNextNode;
				}
				else
				{
					// add the node to the previous node collection
					oNextNode.Collection.Insert(oNextNode.Index + 1, this);

					// update the parent accordingly
					this.Parent = oNextNode.Parent;
				}
			}
			else
			{
				int nIndex = this.Index + 1;

				if (nIndex > this.Collection.Count - 1)
				{
					// find the order of last node under this hierarchy and place the node under it
					int nOrder = this.GetMaxSubNodeOrder();

					// get the next node
					Node oNextParentNode = this.TreeView.GetNodeByOrder(nOrder + 1);

					// add the node to the subnode collection if needed and exists
					if (oNextParentNode != null)
					{
						// fire the node pos changed
						this.TreeView.InvokeBeforeNodePositionChange(this);

						// remove itself from the local collection
						this.Collection.Remove(this);

						if (this.Index == this.Collection.Count)
						{
							this.Collection.Add(this);
						}
						else
						{						
							if (oNextParentNode.Nodes.Count > 0)
							{
								// insert at the first position
								oNextParentNode.Nodes.Insert(0, this);

								// update the parent accordingly
								this.Parent = oNextParentNode;
							}
							else
							{
								// add the node to the previous node collection
								oNextParentNode.Collection.Insert(oNextParentNode.Index + 1, this);

								// update the parent accordingly
								this.Parent = oNextParentNode.Parent;
							}

						}
					}
					else
					{
						// set the key and tag back
						this.Tag = tag;
						this.Key = key;

						this.TreeView.CanFireNodeEvent = true;
						this.NodeMoving = false;
						GetTreeView().SetSelectedNode(oSelNode);
						GetTreeView().SetNodeScrollVisible(this);

						return;
					}
				}
				else
				{
					// fire the node pos changed
					this.TreeView.InvokeBeforeNodePositionChange(this);

					// remove itself from the local collection
					this.Collection.Remove(this);

					// get the node at the index, if it has subnodes, add it to the position
					Node oNextParentNode = this.Collection[nIndex - 1];

					if (oNextParentNode != null && oNextParentNode.IsExpanded && oNextParentNode.Nodes.Count > 0)
					{
						oNextParentNode.Nodes.Insert(0, this);
						this.Parent = oNextParentNode;
					}
					else
						this.Collection.Insert(nIndex, this);
				}				
			}		

			foreach (Node oSubNode in this.Collection)
				oSubNode.m_nCollectionOrder = oSubNode.Index;
			
			foreach (Node oSubNode in oNextNode.Collection)
				oSubNode.m_nCollectionOrder = oSubNode.Index;

			this.TreeView.CanFireNodeEvent = true;
			this.NodeMoving = false;	
			GetTreeView().SetSelectedNode(oSelNode);
			GetTreeView().SetNodeScrollVisible(this);

			// set the key and tag back
			this.Tag = tag;
			this.Key = key;
		
			this.Redraw();

			// fire the node pos changed
			this.TreeView.InvokeAfterNodePositionChange(this);
		}

		/// <summary>
		/// Moves the Node left 
		/// </summary>
		public void MoveLeft()
		{
			if (this.Parent == null)
				return;

			// remember the tag and the key
			object tag = this.Tag;
			string key = this.Key;

			Node oSelNode = GetTreeView().SelectedNode;

			this.TreeView.CanFireNodeEvent = false;
			this.NodeMoving = true;

			// get the index of the parent in the parent's collection
			int nParentIndex = this.Parent.Index;

			int nPrevPosition = this.Index;

			// fire the node pos changed
			this.TreeView.InvokeBeforeNodePositionChange(this);
			
			// remove the current node from its collection
			this.Collection.Remove(this);

			// add it to the parents collection
			this.Parent.Collection.Insert(nParentIndex + 1, this);	
		
			if (this.Parent != null)
			{
				foreach (Node oSubNode in Parent.Collection)
					oSubNode.m_nCollectionOrder = oSubNode.Index;
			}

			foreach (Node oSubNode in this.Collection)
				oSubNode.m_nCollectionOrder = oSubNode.Index;

			this.TreeView.CanFireNodeEvent = true;
			this.NodeMoving = false;
			GetTreeView().SetSelectedNode(oSelNode);
			GetTreeView().SetNodeScrollVisible(this);

			// set the key and tag back
			this.Tag = tag;
			this.Key = key;

			this.Invalidate();

			// fire the node pos changed
			this.TreeView.InvokeAfterNodePositionChange(this);
		}

		/// <summary>
		/// Moves the Node right
		/// </summary>
		public void MoveRight()
		{					
			// get the index of the previous node
			if (this.Index == 0)
				return;

			// remember the tag and the key
			object tag = this.Tag;
			string key = this.Key;

			Node oSelNode = GetTreeView().SelectedNode;

			this.TreeView.CanFireNodeEvent = false;
			this.NodeMoving = true;

			Node oNode;

			int nPrevPosition = this.Index;

			if (this.Parent == null)
				oNode = this.TreeView.Nodes[this.Index - 1];
			else
				oNode = this.Parent.Nodes[this.Index - 1];

			// fire the node pos changed
			this.TreeView.InvokeBeforeNodePositionChange(this);

			this.Collection.Remove(this);
			
			oNode.Nodes.Add(this);
			oNode.Expand();

			foreach (Node oSubNode in this.Collection)
				oSubNode.m_nCollectionOrder = oSubNode.Index;

			foreach (Node oSubNode in oNode.Collection)
				oSubNode.m_nCollectionOrder = oSubNode.Index;

			foreach (Node oSubNode in oNode.Nodes)
				oSubNode.m_nCollectionOrder = oSubNode.Index;

			this.TreeView.CanFireNodeEvent = true;
			this.NodeMoving = false;
			GetTreeView().SetSelectedNode(oSelNode);
			GetTreeView().SetNodeScrollVisible(this);

			// set the key and tag back
			this.Tag = tag;
			this.Key = key;

			this.Invalidate();

			// fire the node pos changed
			this.TreeView.InvokeAfterNodePositionChange(this);
		}

		/// <summary>
		/// Moves the Node to the top
		/// </summary>
		public void MoveTop()
		{
			if (this.Index == 0)
				return;

			// remember the tag and the key
			object tag = this.Tag;
			string key = this.Key;

			Node oSelNode = GetTreeView().SelectedNode;

			this.TreeView.CanFireNodeEvent = false;
			this.NodeMoving = true;

			int nPrevPosition = this.Index;
			
			// fire the node pos changed
			this.TreeView.InvokeBeforeNodePositionChange(this);

			this.Collection.Remove(this);
			this.Collection.Insert(0, this);
			
			foreach (Node oSubNode in Collection)
				oSubNode.m_nCollectionOrder = oSubNode.Index;			

			this.TreeView.CanFireNodeEvent = true;	
			this.NodeMoving = false;		
			GetTreeView().SetSelectedNode(oSelNode);
			GetTreeView().SetNodeScrollVisible(this);

			// set the key and tag back
			this.Tag = tag;
			this.Key = key;

			this.Invalidate();

			// fire the node pos changed
			this.TreeView.InvokeAfterNodePositionChange(this);
		}

		/// <summary>
		/// Moves the Node to the bottom
		/// </summary>
		public void MoveBottom()
		{
			if (this.Index == this.Collection.Count - 1)
				return;

			// remember the tag and the key
			object tag = this.Tag;
			string key = this.Key;

			Node oSelNode = GetTreeView().SelectedNode;

			this.TreeView.CanFireNodeEvent = false;	
			this.NodeMoving = true;

			int nPrevPosition = this.Index;

			// fire the node pos changed
			this.TreeView.InvokeBeforeNodePositionChange(this);

			this.Collection.Remove(this);
			this.Collection.Add(this);

			foreach (Node oSubNode in Collection)
				oSubNode.m_nCollectionOrder = oSubNode.Index;			

			this.TreeView.CanFireNodeEvent = true;	
			this.NodeMoving = true;
			GetTreeView().SetNodeScrollVisible(this);

			// set the key and tag back
			this.Tag = tag;
			this.Key = key;
			
			this.Invalidate();

			// fire the node pos changed
			this.TreeView.InvokeAfterNodePositionChange(this);
		}

		/// <summary>
		/// Initiates the editing of the tree node label.
		/// </summary>
		public void BeginEdit()
		{
			if (GetTreeView().LabelEdit == false)
				throw new Exception("Cannot start node edit mode, TreeView's LabelEdit property is False.");

			EnsureVisible();

			GetTreeView().StartInplaceEdit(this);
		}

		/// <summary>
		/// Ends the editing of the tree node label.
		/// </summary>
		/// <param name="cancel">true if the editing of the tree node label text was canceled without being saved; otherwise, false.</param>
		public void EndEdit(bool cancel)
		{
			if (GetTreeView().EditingNode != this)
				return;

			GetTreeView().OnInplaceEditLostFocus(cancel, EventArgs.Empty);
		}

		/// <summary>
		/// Ensures that the tree node is visible, expanding tree nodes and scrolling the tree view control as necessary.
		/// </summary>
		public void EnsureVisible()
		{
			PureComponents.TreeView.TreeView view = GetTreeView();

			view.ExpandPath(this);
			view.SetNodeScrollVisible(this);
		}

		#endregion

		#region designer support implementation
		/// <summary>
		/// Destroys the component
		/// </summary>
		/// <param name="oDesignerHost">The designer instance interface</param>
		internal void DestroyComponent(IDesignerHost oDesignerHost)
		{
			// destroy subnodes
			foreach (Node oSubNode in Nodes.ToNodeArray())			
				oSubNode.DestroyComponent(oDesignerHost);	
		
			if (this.Panel != null)
			{
				if (GetTreeView() != null)
					GetTreeView().Controls.Remove(this.Panel);

				oDesignerHost.DestroyComponent(this.Panel);

				this.Panel = null;
			}

			// destroys the component
			oDesignerHost.DestroyComponent(this);			
		}

		/// <summary>
		/// Destroys the component
		/// </summary>
		/// <param name="oDesignerHost">The designer instance interface</param>
		internal void DestroyComponentNoRemove(IDesignerHost oDesignerHost)
		{
			// destroy subnodes
			Node [] aNodes = Nodes.ToNodeArray();

			foreach (Node oNode in aNodes)			
				oNode.DestroyComponentNoRemove(oDesignerHost);			

			// destroys the component
			oDesignerHost.DestroyComponent(this);			
		}
		#endregion

		#region delegates and events

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_BackgroundStyle_Invalidate(object sender, EventArgs e)
		{
			if (GetTreeView() != null)
				GetTreeView().Invalidate();
		}

		#endregion

		#region properties

		#region nodes manipulation
		/// <summary>
		/// The parent node object accessor, allows to get the parent node and to set the new one
		/// </summary>
		[Browsable(false)]
		[DefaultValue(null)]
		public Node Parent
		{
			get
			{
				return m_oParentNode;
			}

			set
			{
				if (value != null && value == m_oParentNode)
					return;

				if (value == this)
					throw new Exception("Cannot set parent to itself.");

				if (value != null && value.Parent == this)
					throw new Exception("Cannot create cross parent reference.");

				try
				{				
					if (NodeMoving == false)
					{
						if (m_oParentNode != null)
							try{m_oParentNode.Nodes.Remove(this);}
							catch{}
						else
							try{m_oTreeView.Nodes.Remove(this);}
							catch{}
					}

					// set the new parent
					m_oParentNode = value;

					if (NodeMoving == false)
					{
						if (m_oParentNode != null)
						{
							if (m_oParentNode._TreeView == null && m_oTreeView != null)
								m_oParentNode._TreeView = m_oTreeView;

							m_oParentNode.Nodes.Add(this);
						}
						else
							m_oTreeView.Nodes.Add(this);					
					}
				}
				catch (StackOverflowException oStackException)
				{
					throw new Exception("Cannot create cross parent reference.", oStackException);
				}

				if (NodeMoving == false)
					Invalidate();				
			}
		}

		/// <summary>
		/// Specifies the depth of the hierachy of the node
		/// </summary>
		[Browsable(false)]
		public int IndentLevel
		{
			get
			{
				Node oNode = this;

				int nDepth = 0;

				while (oNode.Parent != null)
				{
					nDepth ++;

					oNode = oNode.Parent;		
				}

				return nDepth;
			}
		}

		/// <summary>
		/// Gets the previous node in the nodes collection
		/// </summary>
		[Browsable(false)]
		public Node PrevNode
		{
			get
			{
				if (Collection == null)
					return null;

				int nIndex = Index;

				nIndex --;

				if (nIndex < 0)
					return null;

				return Collection[nIndex];
			}
		}

		/// <summary>
		/// Gets the next node in the nodes collection
		/// </summary>
		[Browsable(false)]
		public Node NextNode
		{
			get
			{
				if (Collection == null)
					return null;

				int nIndex = Index;

				nIndex ++;

				if (nIndex > Collection.Count - 1)
					return null;

				return Collection[nIndex];
			}
		}
		/// <summary>
		/// Gets the first node in the nodes collection
		/// </summary>
		[Browsable(false)]
		public Node FirstNode
		{
			get
			{
				if (Collection == null)
					return null;

				return Collection[0];
			}
		}

		/// <summary>
		/// Gets the last node in the nodes collection
		/// </summary>
		[Browsable(false)]
		public Node LastNode
		{
			get
			{
				if (Collection == null)
					return null;

				return Collection[Collection.Count - 1];
			}
		}

		/// <summary>
		/// Gets the top node in the nodes collection
		/// </summary>
		[Browsable(false)]
		public Node TopNode
		{
			get
			{
				if (Collection == null)
					return null;

				Node oParentNode = this;

				while (oParentNode.Parent != null)
					oParentNode = oParentNode.Parent;

				return oParentNode;
			}
		}		

		/// <summary>
		/// The TreeView object control of the node
		/// </summary>
		[Browsable(false)]
		public PureComponents.TreeView.TreeView TreeView
		{
			get
			{
				return m_oTreeView;
			}

			set
			{
				m_oTreeView = value;

				Invalidate();
			}
		}

		/// <summary>
		/// Gets the collection of Node objects assigned to the current tree node.
		/// </summary>				
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public NodeCollection Nodes
		{
			get
			{
				if (m_aSubNodes == null)
					m_aSubNodes = new NodeCollection(this);
		
				return m_aSubNodes;
			}
		}

		/// <summary>
		/// Returns the index of the node in the collection the node is part of
		/// </summary>
		[Browsable(false)]		
		public int Index
		{
			get
			{
				if (Collection == null)
					return -1;				

				return Collection.IndexOf(this);
			}			
		}

		/// <summary>
		/// Returns the index of the node in the collection the node is part of, do not modify this, for internal purposes only
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int YOrder
		{
			get
			{
				if (Collection == null)
					return -1;

				if (m_nCollectionOrder != Index)
					return m_nCollectionOrder;

				return Index;
			}		
	
			set
			{
				m_nCollectionOrder = value;
			}
		}

		/// <summary>
		/// Returns the index of the node in the collection the node is part of
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[CLSCompliant(false)]
		public TreeView _TreeView
		{
			get
			{
				return m_oTreeView;
			}

			set
			{
				m_oTreeView = value;				
			}
		}

		/// <summary>
		/// Gets the collection the node is part of
		/// </summary>
		[Browsable(false)]
		public NodeCollection Collection
		{
			get
			{
				if (m_oTreeView == null)
					return null;

				if (m_oParentNode == null)
					return m_oTreeView.Nodes;

				return m_oParentNode.Nodes;
			}
		}
		#endregion

		#region style and UI properties

		//		/// <summary>
		//		/// Specifies the disabled flag style of the node
		//		/// </summary>		
		//		public bool Disabled
		//		{
		//			get
		//			{
		//				return m_bDisabled;
		//			}
		//
		//			set
		//			{
		//				m_bDisabled = value;
		//
		//				Invalidate();
		//			}
		//		}

		/// <summary>
		/// Specifies the visibility flag of the node
		/// </summary>
		[Category("Appearance")] 
		[Browsable(false)]
		[DefaultValue(true)]
		public bool Visible
		{
			get
			{
				return m_bVisible;
			}

			set
			{
				m_bVisible = value;

				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether plus-sign (+) and minus-sign (-) buttons are displayed next to node that contains child nodes.
		/// </summary>
		[Category("Appearance")] 
		[Description("Gets or sets a value indicating whether plus-sign (+) and minus-sign (-) buttons are displayed next to node that contains child nodes.")]
		[DefaultValue(true)]
		public bool ShowPlusMinus
		{
			get
			{
				return m_bShowPlusMinus;
			}

			set
			{
				m_bShowPlusMinus = value;

				Invalidate();
			}
		}

		/// <summary>
		/// Style holder of the node object
		/// </summary>		
		[Category("Appearance")] 
		[Description("Defines the Style being used to paint the node.")]
		[DefaultValue(null)]
		public NodeStyle NodeStyle
		{
			get
			{
				return m_oNodeStyle;
			}

			set
			{
				m_eNodeStyleSource = NodeStyleSource.Local;

				m_oNodeStyle = value;

				if (m_oNodeStyle != null)
				{				
					m_oNodeStyle.Parent = this;					
				}

				Invalidate();
			}
		}

		private void ResetNodeStyle()
		{
			m_oNodeStyle = null;
			m_eNodeStyleSource = PureComponents.TreeView.NodeStyleSource.Default;

			Invalidate();
		}

		/// <summary>
		/// Defines whether checkbox is shown in the Node area
		/// </summary>		
		[Category("Appearance")] 
		[Description("Defines whether checkbox is shown in the Node area.")]
		[DefaultValue(true)]
		public bool CheckBoxVisible
		{
			get
			{
				return m_bCheckBoxVisible;
			}

			set
			{
				m_bCheckBoxVisible = value;
 
				Invalidate();
			}
		}		

		/// <summary>
		/// Defines the flag for the node
		/// </summary>		
		[Category("Appearance")] 
		[Description("Defines the flag for the node.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(null)]
		public NodeFlag Flag
		{
			get
			{
				return m_oNodeFlag;
			}

			set
			{
				m_oNodeFlag = value;

				Invalidate();
			}
		}

		/// <summary>
		/// Defines whether flag is shown in the Node area
		/// </summary>		
		[Category("Appearance")] 
		[Description("Defines whether flag is shown in the Node area.")]
		[DefaultValue(false)]
		public bool FlagVisible
		{
			get
			{
				return m_bFlagVisible;
			}

			set
			{
				if (m_bFlagVisible == false && value == true && m_oNodeFlag == null)
				{
					m_oNodeFlag = new NodeFlag();
					m_oNodeFlag.SetNode(this);
				}

				m_bFlagVisible = value;

				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the text displayed in the label of the tree node.
		/// </summary>
		[Category("Appearance")] 
		[Description("String representing the Node.")]
		public string Text
		{
			get
			{
        if (GetTreeView() != null && GetTreeView().IsDesignMode == false)
          return StringDrawUtils.GetInstance().GetTextFromFormattedString(m_sText);

				return m_sText;
			}

			set
			{
				m_FormattingTested = false;

				if (value != null && GetTreeView() != null)
				{
					m_sText = value.Replace(GetTreeView().PathSeparator, "");
					GetTreeView().m_LastNX = 0;
				}
				else
					m_sText = value;

				Invalidate();
			}
		}

		internal string UnFormattedText
		{
			get
			{
				return StringDrawUtils.GetInstance().GetTextFromFormattedString(m_sText);
			}
		}

		/// <summary>
		/// Gets the text directly
		/// </summary>
		[Browsable(false)]
		public string FormattedText
		{
			get
			{
				return m_sText;
			}			
		}

		/// <summary>
		/// Gets or sets the text displayed in the label of the tree node.
		/// </summary>
		[Category("Appearance")] 
		[Description("String representing the Node's tooltip.")]
		[DefaultValue(null)]
		public string Tooltip
		{
			get
			{
				return m_sTooltip;
			}

			set
			{
				m_sTooltip = value;

				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the string that contains Key data about the Node.
		/// </summary>
		[Description("String Key that identifies the Node uniquely.")]
		[DefaultValue(null)]
		public string Key
		{
			get
			{
				return m_sKey;
			}

			set
			{	
				if (value == null)
				{
					if (GetTreeView() != null)
						GetTreeView().ClearNodeKey(m_sKey);
				}
				else
				{				
					if (GetTreeView() != null)
						GetTreeView().SetNodeKey(this, value);
				}

				m_sKey = value;
			}
		}

		/// <summary>
		/// Gets the TreeView URI of the Node
		/// </summary>
		[Browsable(false)]
		public string FullPath
		{
			get
			{
				StringBuilder oBuilder = new StringBuilder();
				oBuilder.Insert(0, this.Text);

				Node oNode = this.Parent;
				while (oNode != null)
				{
					oBuilder.Insert(0, oNode.Text + GetTreeView().PathSeparator);
					oNode = oNode.Parent;
				}

				oBuilder.Insert(0, GetTreeView().PathSeparator);

				return oBuilder.ToString();
			}
		}

		/// <summary>
		/// The context menu source
		/// </summary>
		[Description("Defines the source of the ContextMenu object.")]
		[DefaultValue(PureComponents.TreeView.ContextMenuSource.Parent)]		
		public ContextMenuSource ContextMenuSource
		{
			get
			{
				return m_eMenuSource;
			}

			set
			{
				m_eMenuSource = value;
			}
		}

		/// <summary>
		/// The context menu associated with the component
		/// </summary>
		[Description("Direct ContextMenu accessor.")]
		[DefaultValue(null)]
		public ContextMenu ContextMenu
		{
			get
			{
				return m_oMenu;
			}

			set
			{
				m_oMenu = value;
			}
		}

		/// <summary>
		/// Gets or sets the object that contains data about the Node.
		/// </summary>
		[TypeConverter(typeof(System.ComponentModel.StringConverter))]
		[Description("The Tag value associated with the Node.")]
		[DefaultValue(null)]
		public object Tag
		{
			get
			{
				return m_oTag;
			}

			set
			{
				m_oTag = value;
			}
		}

		/// <summary>
		/// Style holder of the node object
		/// </summary>		
		[Category("Appearance")] 
		[Description("Gets or sets a value indicating whether the tree node is in a checked state.")]
		[DefaultValue(false)]
		public bool Checked
		{
			get
			{
				return m_bChecked;
			}

			set
			{				
				if (m_bChecked == value)
					return;

				ToggleChecked();

				Invalidate();
			}
		}		

		/// <summary>
		/// Gets a value indicating whether the node is in the selected state.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsSelected
		{
			get
			{
				return m_bIsSelected;
			}

			set
			{
				if (value == true && m_bIsSelected == false)
					Select();

				if (value == false)
				{
					GetTreeView().SetSelectedNode(null);
					this.Invalidate();
				}

				m_bIsSelected = value;
			}
		}

		/// <summary>
		/// Gets a value indicating whether the node is in the expanded state.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsExpanded
		{
			get
			{
				return m_bIsExpanded;
			}

			set
			{
				if (value == true)
					Expand();

				if (value == false)
					Collapse();

				m_bIsExpanded = value;
			}
		}

		/// <summary>
		/// Gets/Sets the value indicating whether the node is underlined
		/// </summary>		
		[Category("Appearance")] 
		[Description("Defines whether the Node should be underlined.")]
		[DefaultValue(false)]
		public bool Underline
		{
			get
			{
				return m_bIsUnderline;
			}

			set
			{
				m_bIsUnderline = value;

				Invalidate();
			}
		}

		/// <summary>
		/// The image list for this item. Used by UI editor
		/// </summary>
		[Browsable(false)]		
		public ImageList ImageList
		{
			get { return this.TreeView.ImageList; }			
		}
		
		/// <summary>
		/// Gets or sets the image list index value of the image on the left side of group.
		/// </summary>	
		[DefaultValue(-1)]				
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[TypeConverter(typeof(PureComponents.TreeView.Design.ImageIndexConverter))]
		[Editor(typeof(PureComponents.TreeView.Design.ImageIndexEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Category("Appearance")] 
		[Description("When NodeImageList in Group is set, you can choose the image from the List here.")]
		public int ImageIndex
		{
			get
			{
				return m_nImageIndex;
			}

			set
			{
				m_nImageIndex = value;

				Invalidate();
			}
		}

		/// <summary>
		/// The nodes's icon
		/// </summary>
		[Category("Appearance")] 
		[Description("Direct image accessor.")]
		[DefaultValue(null)]
		public Image Image
		{
			get
			{
				return m_oImage;
			}

			set
			{
				m_oImage = value;

				Invalidate();
			}
		}

		private void ResetImage()
		{
			m_oImage = null;

			Invalidate();
		}

		/// <summary>
		/// Specifies the way of using styles for the node
		/// </summary>
		[Category("Appearance")] 
		[Description("Defines the source of the style for the Node.")]
		[DefaultValue(NodeStyleSource.Default)]
		public NodeStyleSource NodeStyleSource
		{
			get
			{
				return m_eNodeStyleSource;
			}

			set
			{
				m_eNodeStyleSource = value;

				if (NodeStyle == null && value == NodeStyleSource.Local)
				{
					m_oNodeStyle = new NodeStyle(this);

					if (this.Parent != null)
					{
						m_oNodeStyle.ApplyStyle(this.Parent.GetNodeStyle());
					}
					else
					{
						if (m_oTreeView != null && m_oNodeStyle != null)
							m_oNodeStyle.ApplyStyle(m_oTreeView.Style.NodeStyle);																
					}
				}

				Invalidate();
			}
		}

		#endregion				

		/// <summary>
		/// The Sub nodes check exclusive flag
		/// </summary>
		[Category("Behavior")] 
		[Description("The Sub nodes check exclusive flag.")]
		[DefaultValue(false)]
		public bool SubNodesCheckExclusive
		{
			get
			{
				return m_bSubNodesCheckExclusive;
			}

			set
			{
				m_bSubNodesCheckExclusive = value;

				this.Invalidate();
			}
		}

		/// <summary>
		/// The drag allowance
		/// </summary>
		[Category("Behavior")] 
		[Description("The drag allowance.")]
		[DefaultValue(true)]
		public bool AllowDrag
		{
			get
			{
				return m_bAllowDrag;
			}

			set
			{
				m_bAllowDrag = value;
			}
		}

		/// <summary>
		/// The drop allowance
		/// </summary>		
		[Category("Behavior")] 
		[Description("The drop allowance.")]
		[DefaultValue(true)]
		public bool AllowDrop
		{
			get
			{
				return m_bAllowDrop;
			}

			set
			{
				m_bAllowDrop = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the node is in an editable state.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsEditing
		{
			get
			{
				if (GetTreeView() == null)
					return false;

				return (GetTreeView().EditingNode == this);
			}

			set
			{
				if (GetTreeView() == null)
					return;

				if (value == true)
				{
					Node editingNode = GetTreeView().EditingNode;

					// end editing of existing editing node
					if (editingNode != null && editingNode != this)
						editingNode.EndEdit(false);

					if (editingNode == this)
						return;

					this.BeginEdit();
				}
				else
				{
					if (GetTreeView().EditingNode == this)
						this.EndEdit(false);
				}
			}
		}

		/// <summary>
		/// Use formatting functionality
		/// </summary>
		internal bool UseFormatting
		{
			get
			{
				if (m_FormattingTested == true)
					return m_UseFormatting;

				m_FormattingTested = true;

				// check for formatting characters
				if (this.FormattedText == string.Empty || this.FormattedText == null)
					return m_UseFormatting = false;

				if (this.FormattedText.IndexOf("#C") != -1 || this.FormattedText.IndexOf("#B") != -1 || 
					this.FormattedText.IndexOf("#U") != -1 || this.FormattedText.IndexOf("#I") != -1)
					return m_UseFormatting = true;

				return m_UseFormatting = false;
			}
		}

		/// <summary>
		/// Associated panel object
		/// </summary>
		[DefaultValue(null)]
		public NodePanel Panel
		{
			get { return m_Panel; }

			set
			{
				if (m_Panel != null)
					GetTreeView().Controls.Remove(m_Panel);

				m_Panel = value;

				if (m_Panel != null)
					m_Panel.Node = this;

				this.Invalidate();

				if (m_Panel != null)
				{
					// set the node to the collection of panel nodes
					if (GetTreeView().NodePanelPool.Contains(this) == false)
						GetTreeView().NodePanelPool.Add(this);
				}
				else
					GetTreeView().NodePanelPool.Remove(this);
			}
		}

		/// <summary>
		/// Defines whether the Node can be selected at runtime
		/// </summary>
		[DefaultValue(true)]
		[Category("Behavior")] 
		[Description("Defines whether the Node can be selected at runtime.")]
		public bool CanSelect
		{			
			get { return m_CanSelect; }
			
			set
			{
				m_CanSelect = value;
			}
		}

		/// <summary>
		/// Gets or sets the background style.
		/// </summary>
		[Description("Gets or sets the background style.")]
		[Category("Appearance")]
		public NodeBackgroundStyle BackgroundStyle
		{
			get { return m_BackgroundStyle; }

			set
			{
				if (m_BackgroundStyle != null)
					m_BackgroundStyle.Invalidate -= new EventHandler(m_BackgroundStyle_Invalidate);

				m_BackgroundStyle = value;

				if (m_BackgroundStyle != null)
					m_BackgroundStyle.Invalidate += new EventHandler(m_BackgroundStyle_Invalidate);
			}
		}

		#endregion		

		#region properties databinding

		private object _value;

		/// <summary>
		/// Value of the node in the databinding.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object Value
		{
			get
			{
				return _value;
			}
			set
			{
				_value = value;
			}
		}

		private object _parentValue;

		/// <summary>
		/// Value of the node's parent in the databinding.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object ParentValue
		{
			get
			{
				return _parentValue;
			}
			set
			{
				_parentValue = value;
			}
		}

		#endregion		
	}	
}
