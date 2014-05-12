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
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using PureComponents.TreeView.Design;
using PureComponents.TreeView.Internal;
using Timer = System.Timers.Timer;

namespace PureComponents.TreeView
{
	/// <summary>
	/// TreeView control implementation, see www.purecomponents.com
	/// </summary>
	[Designer(typeof (TreeViewDesigner))]
	[ToolboxBitmap(typeof (TreeView), "PureComponents.TreeView.bmp")]
	[DefaultEvent("")]
	[DefaultMember("")]
    [DefaultProperty("Style")]
	public class TreeView : Control, ISupportInitialize
	{
		#region private members

		private bool m_Initialized = false;

		private bool m_Control = false;
		private bool m_Shift = false;

		/// <summary>
		/// This is another focus type, allowing to distinguish the focus functionality
		/// </summary>
		internal bool m_bUserFocus = false;

		/// <summary>
		/// The designer properties
		/// </summary>
		internal IDesignerHost DesignerHost = null;

		internal bool IsDesignMode = false;

		/// <summary>
		/// Fire override
		/// </summary>
		internal bool CanFireNodeEvent = true;

		/// <summary>
		/// Map that holds the keys for all nodes
		/// </summary>
		private Hashtable m_mapNodeKey = new Hashtable();

		/// <summary>
		/// Optimization
		/// </summary>
		internal bool CancelDraw = false;

		/// <summary>
		/// The drag drop operation helpers
		/// </summary>
		private bool m_bDragDropNode = false;

		private Node m_oDragNode = null;

		/// <summary>
		/// The rectangle to subitem mapper object, and vice versa
		/// </summary>
		internal Hashtable m_mapSubItemToRect = new Hashtable();

		internal Hashtable m_mapRectToSubItem = new Hashtable();

		/// <summary>
		/// The rectangle to subitem box mappr object, and vice versa
		/// </summary>
		internal Hashtable m_mapRectToItemBox = new Hashtable();

		internal Hashtable m_mapItemBoxToRect = new Hashtable();

		/// <summary>
		/// The rectangle to subitem check box mapper object and vice versa
		/// </summary>
		internal Hashtable m_mapRectToItemCheck = new Hashtable();

		internal Hashtable m_mapItemCheckToRect = new Hashtable();

		/// <summary>
		/// The rectangle to subitem flag mapper object and vice versa
		/// </summary>
		internal Hashtable m_mapRectToItemFlag = new Hashtable();

		internal Hashtable m_mapItemFlagToRect = new Hashtable();

		/// <summary>
		/// The scrollbar object
		/// </summary>
		internal SystemVScrollBar m_oScrollBar = new SystemVScrollBar();
		internal SystemHScrollBar m_oHScrollBar = new SystemHScrollBar();

		/// <summary>
		/// The selected node holder and the designtime selected node
		/// </summary>		
		private Node m_oSelectedNode = null;

		private ArrayList m_oSelectedNodes = new ArrayList();
		private Node m_oFocusNode = null;
		private SelectionMode m_oSelectionMode = SelectionMode.Single;

		internal Node EditingNode = null;
		internal Node FlashingNode = null;
		internal Node HighlightedNode = null;
		internal Node MouseOverNode = null;

		internal NodeDragDrop DragDropNode = null;
		private DragDropPopup m_oDragDropPop = null;
		private Node m_oTimerExpandNode = null;

		/// <summary>
		/// The worker thread for the flash function
		/// </summary>
		private Thread m_oFlashThread = null;

		/// <summary>
		/// The mouse click point
		/// </summary>
		private Point m_oMouseClickPoint = Point.Empty;

		private MouseButtons m_oButtonClicked;

		/// <summary>
		/// The focus information holder for the path highlighting
		/// </summary>
		internal bool m_bFocus = false;

		/// <summary>
		/// Specifies if the mouse is in the drag drop mode
		/// </summary>				
		private Node m_oDragOverNode = null;

		private Thread m_oDragScrollThread;
		private bool m_bDragScrollDownRunning = false;
		private bool m_bDragScrollUpRunning = false;

		internal int m_nScroll = 0;
		internal int m_nHScroll = 0;
		internal int m_LastNX = 0;

		/// <summary>
		/// The arraylist holding all added nodes. No node can be added more than once.
		/// </summary>
		internal ArrayList NodePool = new ArrayList();
		internal ArrayList NodePanelPool = new ArrayList();

		/// <summary>
		/// Inplace editing
		/// </summary>
		private EventHandler m_OnInplaceEditLostFocus = null;

		private KeyPressEventHandler m_OnInplaceEditKeyPress = null;
		private TextBox m_textBox = new TextBox();

		/// <summary>
		/// Tooltip
		/// </summary>
		private NodeTooltipWnd m_Tooltip = new NodeTooltipWnd();

		internal Node TooltipNode = null;
		private System.Timers.Timer m_TooltipTimer = null;

		/// <summary>
		/// The copy node object for the context menu operations
		/// </summary>
		private Node m_ContextMenuEditingCopyNode = null;

		#endregion

		#region property members

		/// <summary>
		/// The nodes collection
		/// </summary>
		protected NodeCollection m_aNodes = null;

		/// <summary>
		/// The path separator holder
		/// </summary>
		private string m_sPathSeparator = "\\";

		/// <summary>
		/// The TreeView style holder
		/// </summary>
		private TreeViewStyle m_oStyle = null;

		protected ImageList m_aImages;

		/// <summary>
		/// The sorted property holder
		/// </summary>
		internal bool m_bSorted = false;

		/// <summary>
		/// identifies if the labels are to be edited when F2 is being pressed
		/// </summary>
		private bool m_bLabelEdit = true;

		/// <summary>
		/// The multiline property switch
		/// </summary>
		private bool m_bMultiline = true;

		/// <summary>
		/// The checkbox visibility property
		/// </summary>
		protected bool m_bCheckBoxes = false;

		/// <summary>
		/// The flags visibility property
		/// </summary>
		protected bool m_bFlags = false;

		/// <summary>
		/// The autonumbering feature for nodes
		/// </summary>
		protected bool m_bNodeAutoNumbering = false;

		/// <summary>
		/// The hide selection functionality
		/// </summary>
		protected bool m_bHideSelection = false;

		/// <summary>
		/// Context menu changers
		/// </summary>
		protected bool m_ContextMenuEditing = false;

		protected bool m_ContextMenuArranging = false;
		protected bool m_ContextMenuXmlOperations = false;
		private MethodInfo m_OnPopupMenu = null;

		/// <summary>
		/// The auto drag drop behaviour
		/// </summary>
		protected bool m_AutoDragDrop = true;

		/// <summary>
		/// Interval of the node's tooltip
		/// </summary>
		protected int m_TooltipPopDelay = 1500;

		/// <summary>
		/// The tooltips switch
		/// </summary>
		protected bool m_Tooltips = true;

		/// <summary>
		/// Defines the behaviour on dblclick
		/// </summary>
		protected bool m_bExandOnDblClick = true;

		/// <summary>
		/// Inplace edit settings
		/// </summary>
		protected bool m_bAllowEditing = true;

		protected bool m_bAllowAdding = true;
		protected bool m_bAllowDeleting = true;
		protected bool m_bAllowArranging = true;

		protected ContextMenuStrings m_oContextMenuString = new ContextMenuStrings();

		// databinding 
		protected Hashtable m_NodesValueMember;
		protected DataTable m_DataSource;
		protected string m_DisplayMember;
		protected string m_ValueMember;
		protected string m_ParentMember;
		protected object m_RootParentValue = DBNull.Value;

		#endregion

		#region construction

		/// <summary>
		/// Construction
		/// </summary>
		public TreeView()
		{
			// initialization
			Width = 280;
			Height = 240;
			Left = 2;
			Top = 2;

			m_NodesValueMember = new Hashtable();

			// optimize scrolling - no flicker
			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			SetStyle(ControlStyles.ResizeRedraw, true);

			m_aNodes = new NodeCollection(this);
			m_oStyle = new TreeViewStyle(this);

			m_oScrollBar.Scroll += new ScrollEventHandler(OnScrollItems);
			m_oScrollBar.MouseEnter += new EventHandler(OnScrollMouseEnter);
			m_oScrollBar.MouseLeave += new EventHandler(OnScrollMouseLeave);
			m_oHScrollBar.Scroll += new ScrollEventHandler(OnScrollItemsHorizontal);
			m_oHScrollBar.MouseEnter += new EventHandler(OnScrollMouseEnter);
			m_oHScrollBar.MouseLeave += new EventHandler(OnScrollMouseLeave);

			this.BackColor = Color.White;

			m_OnInplaceEditLostFocus = new EventHandler(OnInplaceEditLostFocus);
			m_OnInplaceEditKeyPress = new KeyPressEventHandler(OnInplaceEditKeyPress);

			m_textBox.Visible = false;

			MethodInfo[] methods = typeof (ContextMenu).GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.NonPublic);

			foreach (MethodInfo method in methods)
			{
				if (method.Name == "OnPopup")
				{
					m_OnPopupMenu = method;
					break;
				}
			}			
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			if (disposing == false)
				return;

			if (m_Tooltip != null)
			{
				m_Tooltip.Hide();
				//m_Tooltip.DestroyHandle();
			}
		}

		#endregion

		#region implementation

		/// <summary>
		/// Saves the treeview content to the specified Binary file
		/// </summary>
		public void SaveBinary(string path)
		{
			TreeViewDataAccess.SaveTreeViewData(this, path);
		}

		/// <summary>
		/// Saves the treeview content to the specified Binary stream
		/// </summary>
		public void SaveBinary(Stream stream)
		{
			TreeViewDataAccess.SaveTreeViewData(this, stream);
		}

		/// <summary>
		/// Saves the treeview content to the specified Binary file
		/// </summary>
		public void SaveBinary()
		{
			try
			{
				SaveFileDialog saveDialog = new SaveFileDialog();

				saveDialog.FileName = "TreeViewContent.dat";
				saveDialog.InitialDirectory = Assembly.GetEntryAssembly().Location;
				saveDialog.Filter = "dat files (*.dat)|*.dat";
				saveDialog.FilterIndex = 0;
				saveDialog.RestoreDirectory = true;

				if (saveDialog.ShowDialog() == DialogResult.OK)
					SaveBinary(saveDialog.FileName);
			}
			catch
			{
			}
		}

		/// <summary>
		/// Loads the treeview content from the specified Binary file and prompts for the file name
		/// </summary>
		public void LoadBinary()
		{
			try
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();

				openFileDialog.FileName = "TreeViewContent.dat";
				openFileDialog.InitialDirectory = Assembly.GetEntryAssembly().Location;
				openFileDialog.Filter = "dat files (*.dat)|*.dat";
				openFileDialog.FilterIndex = 0;
				openFileDialog.RestoreDirectory = true;

				if (openFileDialog.ShowDialog() == DialogResult.OK)
					LoadBinary(openFileDialog.FileName);
			}
			catch (Exception e)
			{
				Trace.WriteLine(e.ToString());
			}
		}

		/// <summary>
		/// Load the treeview content from the specified Binary file
		/// </summary>
		public void LoadBinary(string path)
		{
			TreeViewDataAccess.LoadTreeViewData(this, path);
		}

		/// <summary>
		/// Load the treeview content from the specified Binary stream
		/// </summary>
		public void LoadBinary(Stream stream)
		{
			TreeViewDataAccess.LoadTreeViewData(this, stream);
		}

		/// <summary>
		/// Saves the treeview content to the specified XML file, default encoding is Unicode and prompts for the file name
		/// </summary>
		public void SaveXml()
		{
			try
			{
				SaveFileDialog saveDialog = new SaveFileDialog();

				saveDialog.FileName = "TreeViewContent.xml";
				saveDialog.InitialDirectory = Assembly.GetEntryAssembly().Location;
				saveDialog.Filter = "xml files (*.xml)|*.xml";
				saveDialog.FilterIndex = 0;
				saveDialog.RestoreDirectory = true;

				if (saveDialog.ShowDialog() == DialogResult.OK)
					SaveXml(saveDialog.FileName);
			}
			catch
			{
			}
		}

		/// <summary>
		/// Loads the treeview content from the specified XML file and prompts for the file name
		/// </summary>
		public void LoadXml()
		{
			try
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();

				openFileDialog.FileName = "TreeViewContent.xml";
				openFileDialog.InitialDirectory = Assembly.GetEntryAssembly().Location;
				openFileDialog.Filter = "xml files (*.xml)|*.xml";
				openFileDialog.FilterIndex = 0;
				openFileDialog.RestoreDirectory = true;

				if (openFileDialog.ShowDialog() == DialogResult.OK)
					LoadXml(openFileDialog.FileName);
			}
			catch (Exception e)
			{
				Trace.WriteLine(e.ToString());
			}
		}

		/// <summary>
		/// Saves the treeview content to the specified XML file, default encoding is Unicode
		/// </summary>
		/// <param name="fileName">File name of the file where to save the treeview XML representation</param>
		public void SaveXml(string fileName)
		{
			this.SaveXml(fileName, ASCIIEncoding.Unicode);
		}

		/// <summary>
		/// Saves the treeview content to the specified XML file
		/// </summary>
		/// <param name="fileName">File name of the file where to save the treeview XML representation</param>
		/// <param name="encoding">Text encoding used to encode the content</param>
		public void SaveXml(string fileName, Encoding encoding)
		{
			TreeViewXML xml = new TreeViewXML(this);

			xml.SaveToXML(fileName, encoding);
		}

		/// <summary>
		/// Saves the treeview content to the specified XML stream, default encoding is Unicode
		/// </summary>
		/// <param name="stream">Stream where to save the TreeView XML content </param>
		public void SaveXml(Stream stream)
		{
			this.SaveXml(stream, ASCIIEncoding.Unicode);
		}

		/// <summary>
		/// Saves the treeview content to the specified XML stream
		/// </summary>
		/// <param name="stream">Stream where to save the TreeView XML content </param>
		/// <param name="encoding">Text encoding used to encode the content</param>
		public void SaveXml(Stream stream, Encoding encoding)
		{
			TreeViewXML xml = new TreeViewXML(this);

			xml.SaveToXML(stream, encoding);
		}

		/// <summary>
		/// Loads the treeview content from the specified XML file
		/// </summary>
		public void LoadXml(string fileName)
		{
			TreeViewXML xml = new TreeViewXML(this);

			xml.LoadFromXML(fileName);
		}

		/// <summary>
		/// Loads the treeview content from the specified XML stream
		/// </summary>
		public void LoadXml(Stream stream)
		{
			TreeViewXML xml = new TreeViewXML(this);

			xml.LoadFromXML(stream);
		}

		/// <summary>
		/// Applies the given style to the actual TreeView style
		/// </summary>		
		public void ApplyStyle(TreeViewStyle oStyle)
		{
			this.Style.BorderStyle = oStyle.BorderStyle;
			this.Style.TrackNodeHover = oStyle.TrackNodeHover;

			this.Style.AutoCollapse = oStyle.AutoCollapse;
			this.Style.FlashColor = oStyle.FlashColor;
			this.Style.HighlightSelectedPath = oStyle.HighlightSelectedPath;
			this.Style.LineColor = oStyle.LineColor;
			this.Style.LineStyle = oStyle.LineStyle;
			this.Style.ShowLines = oStyle.ShowLines;
			this.Style.ShowPlusMinus = oStyle.ShowPlusMinus;
			this.Style.ShowSubitemsIndicator = oStyle.ShowSubitemsIndicator;
		}

		/// <summary>
		/// Clears all selection
		/// </summary>
		internal void ClearAllSelection()
		{
			m_oSelectedNode = null;

			foreach (Node oNode in Nodes)
				oNode.ClearSelection();
		}

		/// <summary>
		/// Clears TreeView content, nodes and keys
		/// </summary>
		public void Clear()
		{
			ClearNodeKeys();

			ClearNodeSelection();

			this.Nodes.Clear();

			m_NodesValueMember = new Hashtable();
		}

		/// <summary>
		/// Clears all selection
		/// </summary>
		public void ClearNodeSelection()
		{
			ClearAllSelection();

			ClearSelectedNodes();
		}

		/// <summary>
		/// Clears keys for all nodes
		/// </summary>
		public void ClearNodeKeys()
		{
			ArrayList nodeList = new ArrayList();

			foreach (Node node in m_mapNodeKey.Values)
				nodeList.Add(node);

			// clear keys map
			m_mapNodeKey.Clear();

			// remove all node keys
			foreach (Node node in nodeList)
				node.Key = null;
		}

		/// <summary>
		/// Adds the given node to the list of selected nodes
		/// </summary>
		/// <param name="node"></param>
		public void AddSelectedNode(Node node)
		{
			if (SelectionMode == SelectionMode.Single)
				return;

			node.SelectInternal(true);

			if (m_oSelectedNodes.Contains(node) == false)
				m_oSelectedNodes.Add(node);

			this.Invalidate();

			this.InvokeNodeSelectionChange();
		}

		/// <summary>
		/// Removes the given node from the list of selected nodes
		/// </summary>
		/// <param name="node"></param>
		public void RemoveSelectedNode(Node node)
		{
			node.SelectInternal(false);

			if (m_oSelectedNodes.Contains(node) == true)
				m_oSelectedNodes.Remove(node);

			this.Invalidate();

			this.InvokeNodeSelectionChange();
		}

		/// <summary>
		/// Clears the list of selected nodes
		/// </summary>
		public void ClearSelectedNodes()
		{
			foreach (Node node in m_oSelectedNodes)
				node.SelectInternal(false);

			m_oSelectedNodes.Clear();

			m_oSelectedNode = null;

			this.Invalidate();

			this.InvokeNodeSelectionChange();
		}

		/// <summary>
		/// Expands the path to the given node
		/// </summary>
		/// <param name="oNode">Node to which expand the path</param>
		public void ExpandPath(Node oNode)
		{
			ArrayList aParents = new ArrayList();

			while (oNode.Parent != null)
			{
				aParents.Insert(0, oNode.Parent);

				oNode = oNode.Parent;
			}

			foreach (Node oParent in aParents)
				oParent.Expand();
		}

		/// <summary>
		/// Expands all nodes
		/// </summary>
		public void ExpandAll()
		{
			foreach (Node oNode in Nodes)
				oNode.ExpandAll();
		}

		/// <summary>
		/// Expands all nodes
		/// </summary>
		public void CollapseAll()
		{
			foreach (Node oNode in Nodes)
				oNode.CollapseAll();
		}

		/// <summary>
		/// Flashes the actual selected node and the path to the node. Doeas not expand the path
		/// </summary>
		public void Flash()
		{
			if (SelectedNode == null)
				return;

			Node oFlashNode = FlashingNode;

			while (oFlashNode != null)
			{
				oFlashNode.Flash = false;
				oFlashNode = oFlashNode.Parent;
			}

			Invalidate();

			FlashingNode = SelectedNode;

			Flash(FlashingNode);
		}

		/// <summary>
		/// Flashes the given node and the path to the node, does not expand the path
		/// </summary>
		public void Flash(Node oNode)
		{
			Node oFlashNode = FlashingNode;

			while (oFlashNode != null)
			{
				oFlashNode.Flash = false;
				oFlashNode = oFlashNode.Parent;
			}

			Invalidate();

			FlashingNode = oNode;

			Flash(FlashingNode, false);
		}

		/// <summary>
		/// Flashes the given node and the given path to the node
		/// </summary>
		/// <param name="oNode">Node to flash</param>
		/// <param name="bExpand">Switch specifying to expand the node</param>
		public void Flash(Node oNode, bool bExpand)
		{
			SetNodeScrollVisible(oNode);

			Node oFlashNode = FlashingNode;

			while (oFlashNode != null)
			{
				oFlashNode.Flash = false;
				oFlashNode = oFlashNode.Parent;
			}

			Invalidate();

			FlashingNode = oNode;

			if (bExpand == true)
				ExpandPath(oNode);

			if (m_oFlashThread != null)
				m_oFlashThread.Abort();

			m_oFlashThread = new Thread(new ThreadStart(this.OnFlashImpl));
			m_oFlashThread.Start();
		}

		/// <summary>
		/// Flashes the actual selected node and the path to the node
		/// </summary>
		/// <param name="bExpand">Switch specifying to expand the node</param>
		public void Flash(bool bExpand)
		{
			if (SelectedNode == null)
				return;

			Node oFlashNode = FlashingNode;

			while (oFlashNode != null)
			{
				oFlashNode.Flash = false;
				oFlashNode = oFlashNode.Parent;
			}

			Invalidate();

			FlashingNode = SelectedNode;

			Flash(FlashingNode, bExpand);
		}

		/// <summary>
		/// Implementation of the flashing mechanism
		/// </summary>
		protected void OnFlashImpl()
		{
			bool bFlash = false;

			for (int nStep = 0; nStep < 17; nStep ++)
			{
				Node oNode = FlashingNode;

				while (oNode != null)
				{
					oNode.Flash = bFlash;
					oNode = oNode.Parent;
				}

				Invalidate();

				Thread.Sleep(150);

				bFlash = !bFlash;
			}

			FlashingNode = null;

			Invalidate();
		}

		/// <summary>
		/// Retrieves the tree node that is at the specified location.
		/// </summary>
		/// <param name="oPoint">The Point to evaluate and retrieve the node from.</param>
		/// <returns>The Node at the specified point, in tree view coordinates.</return>		
		public Node GetNodeAt(Point oPoint)
		{
			return GetSubObjectAtPoint(oPoint) as Node;
		}

		/// <summary>
		/// Retrieves the tree node that is at the specified location.
		/// </summary>
		/// <param name="x">X coordinate of the at point</param>
		/// <param name="y">Y coordinate of the at point</param>
		/// <returns>The Node at the specified point, in tree view coordinates.</return>		
		public Node GetNodeAt(int x, int y)
		{
			return GetSubObjectAtPoint(new Point(x, y)) as Node;
		}

		/// <summary>
		/// Adds the node by the specified path, if the path does not exist, it will create all needed objects
		/// </summary>
		/// <param name="sPath">Path to add</param>
		public Node AddNodeByPath(string sPath)
		{
			return AddNodeByPath(sPath, true);
		}

		/// <summary>
		/// Adds the node by the specified path, if the path does not exist, it will create all needed objects
		/// </summary>
		/// <param name="sPath">Path to add</param>
		/// <param name="sNodeKey">Node key to be associated with the created node</param>
		public Node AddNodeByPath(string sPath, string sNodeKey)
		{
			if (GetNodeByKey(sNodeKey) != null)
				throw new Exception("Node with the specified key [" + sNodeKey + "] already exists. Node cannot be added.");

			Node bStatus = AddNodeByPath(sPath, true);

			if (bStatus == null)
				return bStatus;

			bStatus.Key = sNodeKey;

			return bStatus;
		}

		/// <summary>
		/// Adds the node by the specified path
		/// </summary>
		/// <param name="sPath">Path to add</param>
		/// <param name="bCreateStructure">Specifies wheter to create the structure</param>
		public Node AddNodeByPath(string sPath, bool bCreateStructure)
		{
			// parse the node path for nodes
			string[] aNodes = GetNodesFromPath(sPath);

			Node oParent = null;
			NodeCollection oCollection = this.Nodes;

			if (bCreateStructure == true)
			{
				foreach (string sNode in aNodes)
				{
					if (sNode == "")
						continue;

					Node oNode = GetCollectionNode(oCollection, sNode);

					// create the node
					if (oNode == null && oParent == null)
					{
						oNode = new Node();
						oNode.Text = sNode;
						oNode.SetNodeTreeViewReference(this);
						oCollection.Add(oNode);
					}

					if (oNode == null && oParent != null)
					{
						oNode = new Node();
						oNode.SetNodeTreeViewReference(this);
						oNode.Text = sNode;
						oNode.SetParent(oParent);
						oCollection.Add(oNode);
					}

					oCollection = oNode.Nodes;
					oParent = oNode;
				}
			}
			else
			{
				Node oNode = null;

				for (int nNode = 0; nNode < aNodes.Length - 1; nNode ++)
				{
					string sNode = aNodes[nNode];

					oNode = GetCollectionNode(oCollection, sNode);

					if (oNode == null)
						return null;

					oCollection = oNode.Nodes;
				}

				if (oNode == null)
					return null;

				Node oSubNode = oNode.CreateSubNode();
				oSubNode.SetNodeTreeViewReference(this);
				oSubNode.Text = aNodes[aNodes.Length - 1];
				oSubNode.Parent = oNode;

				return oNode;
			}

			return oParent;
		}

		/// <summary>
		/// Gets the node at the specified path
		/// </summary>
		/// <param name="sPath">Path to check</param>
		/// <returns>Node object if found, null (nothing) otherwise</returns>
		public Node GetNodeByPath(string sPath)
		{
			if (sPath == null)
				return null;

			// parse the node path for nodes
			string[] aNodes = GetNodesFromPath(sPath);

			NodeCollection oCollection = this.Nodes;

			Node oNode = null;

			foreach (string sNode in aNodes)
			{
				if (sNode == string.Empty)
					continue;

				oNode = GetCollectionNode(oCollection, sNode);

				if (oNode == null)
					return null;

				oCollection = oNode.Nodes;
			}

			return oNode;
		}

		/// <summary>
		/// Gets the Node by the specified Key, if there is no Node under the key, returns nothing.
		/// </summary>
		/// <param name="sKey">Key to find the node</param>
		/// <returns>Node object if found, null (nothing) otherwise</returns>
		public Node GetNodeByKey(string sKey)
		{
			if (m_mapNodeKey.ContainsKey(sKey) == false)
				return null;

			return m_mapNodeKey[sKey] as Node;
		}

		/// <summary>
		/// Gets the array of nodes in the whole TreeView object by their Tag object.
		/// </summary>
		/// <param name="oTag">Tag object to find the nodes</param>
		/// <returns>Array of Node objects that have the Tag specified</returns>
		public Node[] GetNodesByTag(object oTag)
		{
			ArrayList aNodes = new ArrayList();

			foreach (Node oNode in Nodes)
			{
				if (oNode.Tag == null && oTag == null)
					aNodes.Add(oNode);
				else
				{
					if (oNode.Tag != null && oNode.Tag.Equals(oTag))
						aNodes.Add(oNode);
				}

				Node[] aGroupNodes = oNode.GetNodesByTag(oTag);

				if (aGroupNodes != null && aGroupNodes.Length > 0)
					aNodes.AddRange(aGroupNodes);
			}

			Node[] aData = new Node[aNodes.Count];
			aNodes.CopyTo(aData);

			return aData;
		}

		#endregion

		#region implementation databound

		#region Load

		/// <summary>
		/// Loads the tree for databinding
		/// </summary>
		private void LoadTree()
		{
			if (this.m_DataSource != null && this.m_DisplayMember != null && this.m_ValueMember != null && this.m_ParentMember != null)
			{
				Clear();

				ArrayList nodes = new ArrayList();

				foreach (DataRow dr in this.m_DataSource.Rows)
				{
					Node node = new Node(dr[this.m_DisplayMember].ToString());
					
					node.Value = dr[this.m_ValueMember];
					node.ParentValue = dr[this.m_ParentMember];

					m_NodesValueMember.Add(node.Value, node);
					nodes.Add(node);
				}

				foreach (Node node in nodes)
				{
					if (node.ParentValue == m_RootParentValue)
					{
						//the node is a Root, add it to the root collection
						this.Nodes.Add(node);

						node.NodeOrder = -1;
						node.YOrder = this.Nodes.IndexOf(node);
					}
					else
					{
						//look for the parent
						Node parent = (Node) m_NodesValueMember[node.ParentValue];

						//add it to the nodes collection of the parent node
						parent.Nodes.Add(node);

						node.NodeOrder = -1;
						node.YOrder = parent.Nodes.IndexOf(node);
					}
				}
			}
		}

		#endregion

		#region Row Changed & Row Deleted

		/// <summary>
		/// Handles datachagned at row
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void value_RowChanged(object sender, DataRowChangeEventArgs e)
		{
			if (e.Action == DataRowAction.Add)
			{
				if (e.Row[this.m_ValueMember] != DBNull.Value)
				{
					Node node = new Node(e.Row[this.DisplayMember].ToString());
					node.Value = e.Row[this.m_ValueMember];
					node.ParentValue = e.Row[this.m_ParentMember];
					m_NodesValueMember.Add(node.Value, node);
					if (node.ParentValue == m_RootParentValue)
					{
						//Its a root
						this.Nodes.Add(node);
					}
					else if (m_NodesValueMember[node.ParentValue] != null)
					{
						//The parent exist
						Node parent = (Node) m_NodesValueMember[node.ParentValue];

						parent.Nodes.Add(node);

						parent.Expand();
					}
				}
			}
			else if (e.Action == DataRowAction.Change)
			{
				Node node = (Node) m_NodesValueMember[e.Row[this.m_ValueMember]];
				object actualParent = e.Row[this.m_ParentMember].ToString();

				//Change parenthood
				if (actualParent.ToString() != node.ParentValue.ToString())
				{
					if (node.ParentValue != m_RootParentValue)
					{
						Node oldParent = (Node) m_NodesValueMember[node.ParentValue];

						if (oldParent != null)
						{
							oldParent.Nodes.Remove(node);
						}
					}
					else
					{
						//Remove it from the root nodes
						this.Nodes.Remove(node);
					}

					node.ParentValue = e.Row[this.m_ParentMember];

					if (node.ParentValue != m_RootParentValue)
					{
						Node newParent = (Node) m_NodesValueMember[node.ParentValue];
						if (newParent != null) //if exist
						{
							newParent.Nodes.Add(node);

							newParent.Expand();
						}
					}
					else
					{
						this.Nodes.Add(node);
					}
				}
				//Change the text
				node.Text = e.Row[this.DisplayMember].ToString();
			}
		}

		/// <summary>
		/// Handles row deleting at datasource
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void value_RowDeleting(object sender, DataRowChangeEventArgs e)
		{
			Node node = (Node) m_NodesValueMember[e.Row[this.m_ValueMember]];

			m_NodesValueMember.Remove(node.Value);
			
			if (node.TreeView != null)
			{
				node.Remove();
			}
		}

		#endregion

		#endregion

		#region internal helper functions

		/// <summary>
		/// Performs the copy of the treeview
		/// </summary>		
		internal void Copy(TreeView treeView)
		{
			this.AllowDrop = treeView.AllowDrop;
			this.Anchor = treeView.Anchor;
			this.BackColor = treeView.BackColor;
			this.BackgroundImage = treeView.BackgroundImage;
			this.BindingContext = treeView.BindingContext;
			this.Capture = treeView.Capture;
			this.CheckBoxes = treeView.CheckBoxes;
			this.CausesValidation = treeView.CausesValidation;
			this.ContextMenu = treeView.ContextMenu;
			this.ContextMenuArranging = treeView.ContextMenuArranging;
			this.ContextMenuEditing = treeView.ContextMenuEditing;
			this.Cursor = treeView.Cursor;
			this.Style.ApplyStyle(treeView.Style);
			this.Style.NodeStyle.ApplyStyle(treeView.Style.NodeStyle);
			this.Enabled = treeView.Enabled;
			this.Font = treeView.Font;
			this.ForeColor = treeView.ForeColor;
			this.Flags = treeView.Flags;
			this.Height = treeView.Height;
			this.ImeMode = treeView.ImeMode;
			this.Multiline = treeView.Multiline;
			this.ImageList = treeView.ImageList;
			this.Parent = treeView.Parent;
			this.PathSeparator = treeView.PathSeparator;
			this.Sorted = treeView.Sorted;
			this.TabStop = treeView.TabStop;
			this.Visible = treeView.Visible;
			this.Width = treeView.Width;
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

		private int CalcItemHeight(Graphics oGraphics)
		{
			int nHeight = 6;

			foreach (Node oNode in Nodes)
				nHeight += CalcItemHeight(oNode, oGraphics);

			return nHeight;
		}

		private int CalcItemWidth(Graphics oGraphics)
		{
			return m_LastNX;
		}

		/// <summary>
		/// Calc the drawing area height of the node specified
		/// </summary>
		/// <param name="oNode"></param>
		/// <returns></returns>
		private int CalcItemHeight(Node oNode, Graphics oGraphics)
		{
			int nHeight = oNode.GetHeight(oGraphics) + this.Style.NodeSpaceVertical;

			if (oNode.IsExpanded == false)
				return nHeight;

			if (oNode.Nodes.Count == 0)
				return nHeight;

			foreach (Node oSubNode in oNode.Nodes)
				nHeight += CalcItemHeight(oSubNode, oGraphics);

			return nHeight;
		}

		/// <summary>
		/// Get the border style as the dash style
		/// </summary>		
		internal DashStyle GetBorderStyle(BorderStyle oBorderStyle)
		{
			if (oBorderStyle == BorderStyle.Dot)
				return DashStyle.Dot;

			if (oBorderStyle == BorderStyle.None)
				return DashStyle.Solid;

			if (oBorderStyle == BorderStyle.Solid)
				return DashStyle.Solid;

			return DashStyle.Dot;
		}

		/// <summary>
		/// Get the line style as the dash style
		/// </summary>		
		internal static DashStyle GetLineStyle(LineStyle oLineStyle)
		{
			if (oLineStyle == LineStyle.Dot)
				return DashStyle.Dot;

			if (oLineStyle == LineStyle.Solid)
				return DashStyle.Solid;

			return DashStyle.Dot;
		}

		/// <summary>
		/// Gets the color alpha part depending on the type of rendering and the node
		/// </summary>
		/// <param name="oNode">Node to get the alpha</param>
		/// <returns>Alpha part of the color</returns>
		internal int GetNodeAlpha(Node oNode)
		{
			int nAlpha = 255;

			if (oNode.GetHighlightSelectedPath() == false)
				return nAlpha;

			if (IsDesignMode == true)
				return nAlpha;

			if (m_bFocus == false)
			{
				if (oNode.IsInSelectedPath() == false)
					return nAlpha = 100;

				if (SelectedNode == null)
					nAlpha = 100;
			}

			return nAlpha;
		}

		/// <summary>
		/// Gets the rectangle of the node
		/// </summary>
		/// <param name="oNode">Node whose box rect to get</param>
		/// <returns>Box rectangle</returns>
		internal Rectangle GetExpandBoxRect(Node oNode)
		{
			if (oNode == null)
				return Rectangle.Empty;

			object oRect = m_mapItemBoxToRect[oNode];

			if (oRect == null)
				return Rectangle.Empty;

			return (Rectangle) oRect;
		}

		internal Node GetNodeByItemBox(Rectangle rect)
		{
			return m_mapRectToItemBox[rect] as Node;
		}

		/// <summary>
		/// Gets the rectangle of the node
		/// </summary>
		internal Rectangle GetExpandBoxRect(Point p)
		{
			foreach (Rectangle rect in m_mapRectToItemBox.Keys)
			{
				if (rect.Contains(p) == true)
					return rect;
			}

			return Rectangle.Empty;
		}

		internal bool IsExpandBox(Point p)
		{
			foreach (Rectangle rect in m_mapRectToItemBox.Keys)
			{
				if (rect.Contains(p) == true)
					return true;
			}

			return false;
		}

		internal Rectangle GetNodeCheckBoxRect(Node oNode)
		{
			if (oNode == null)
				return Rectangle.Empty;

			object oRect = m_mapItemCheckToRect[oNode];

			if (oRect == null)
				return Rectangle.Empty;

			return (Rectangle) oRect;
		}

		internal Rectangle GetNodeCheckBoxRect(Point p)
		{
			foreach (Rectangle rect in m_mapRectToItemCheck.Keys)
			{
				if (rect.Contains(p) == true)
					return rect;
			}

			return Rectangle.Empty;
		}

		internal Node GetNodeByItemCheckBox(Rectangle rect)
		{
			return m_mapRectToItemCheck[rect] as Node;
		}

		internal bool IsNodeCheckBox(Point p)
		{
			foreach (Rectangle rect in m_mapRectToItemCheck.Keys)
			{
				if (rect.Contains(p) == true)
					return true;
			}

			return false;
		}

		internal Rectangle GetNodeFlagRect(Node oNode)
		{
			if (oNode == null)
				return Rectangle.Empty;

			object oRect = m_mapItemFlagToRect[oNode];

			if (oRect == null)
				return Rectangle.Empty;

			return (Rectangle) oRect;
		}

		internal Rectangle GetNodeFlagRect(Point p)
		{
			foreach (Rectangle rect in m_mapRectToItemFlag.Keys)
			{
				if (rect.Contains(p) == true)
					return rect;
			}

			return Rectangle.Empty;
		}

		internal Node GetNodeByItemFlag(Rectangle rect)
		{
			return m_mapRectToItemFlag[rect] as Node;
		}

		internal bool IsNodeFlag(Point p)
		{
			foreach (Rectangle rect in m_mapRectToItemFlag.Keys)
			{
				if (rect.Contains(p) == true)
					return true;
			}

			return false;
		}

		/// <summary>
		/// Retrievse information of the node is in the TreeView
		/// </summary>
		/// <param name="oNode"></param>
		/// <returns></returns>
		internal bool ContainsNode(Node oNode)
		{
			return NodePool.Contains(oNode);
		}

		/// <summary>
		/// The direct SelectedNode setter
		/// </summary>
		/// <param name="oNode">Node object to be set as selected</param>
		internal void SetSelectedNode(Node oNode)
		{
			m_oSelectedNode = oNode;
		}

		/// <summary>
		/// Sets the scrolling in the position so the Node is being visible
		/// </summary>
		/// <param name="oNode">Node to be set as visible</param>
		internal void SetNodeScrollVisible(Node oNode)
		{
			SetNodeScrollVisible(oNode, false);
		}

		internal void SetNodeScrollVisible(Node oNode, bool bIgnoreDesignMode)
		{
			// if no scrollbar there is nothing to set
			if (Controls.Contains(m_oScrollBar) == false)
				return;

			if (IsDesignMode == true && bIgnoreDesignMode == false)
				return;

			Refresh();

			Rectangle oRect;

			try
			{
				oRect = (Rectangle) m_mapSubItemToRect[oNode];
			}
			catch
			{
				try
				{
					oRect = (Rectangle) m_mapItemBoxToRect[oNode];
				}
				catch
				{
					return;
				}
			}

			try
			{
				int nScroll = m_nScroll;

				if (oRect.Bottom >= this.Height || oRect.Top < 0)
				{
					if (oRect.Bottom >= this.Height)
						nScroll = m_nScroll - (oRect.Bottom - this.Height);

					if (oRect.Top < 0)
						nScroll = m_nScroll - oRect.Top;

					m_nScroll = nScroll;

					if (m_nScroll >= -6)
						m_nScroll = 0;
				}
			}
			catch
			{
			}

			Refresh();
		}

		/// <summary>
		/// Sets the key for the specified node object
		/// </summary>
		/// <param name="oNode"></param>
		/// <param name="sKey"></param>
		internal void SetNodeKey(Node oNode, string sKey)
		{
			if (sKey == null)
				return;

			if (m_mapNodeKey.ContainsKey(sKey) == true)
			{
				Node oExistingNode = m_mapNodeKey[sKey] as Node;

				if (oNode != oExistingNode)
					throw new Exception("Key '" + sKey + "' is already in the NodeKey dictionary, key cannot be added.");
				else
					return;
			}

			m_mapNodeKey.Add(sKey, oNode);
		}

		/// Clears the Nodes's key
		/// </summary>
		/// <param name="sKey">Node whose key to be cleared</param>
		internal void ClearNodeKey(string sKey)
		{
			if (sKey == null)
				return;

			if (m_mapNodeKey.ContainsKey(sKey) == false)
				return;

			m_mapNodeKey.Remove(sKey);
		}

		/// <summary>
		/// Gets the node by its name
		/// </summary>
		/// <param name="oCollection">Collection to check</param>
		/// <param name="sNode">Node text to check</param>
		/// <returns>Node object if found, null otherwise</returns>
		private Node GetCollectionNode(NodeCollection oCollection, string sNode)
		{
			foreach (Node oNode in oCollection)
				if (oNode.Text == sNode)
					return oNode;

			return null;
		}

		/// <summary>
		/// Parses the path for nodes
		/// </summary>
		/// <param name="sPath">Path to parse</param>
		/// <returns>Node array representing the structure</returns>
		private string[] GetNodesFromPath(string sPath)
		{
			ArrayList aNodes = new ArrayList();
			int nPos = 0;

			while (nPos != -1)
			{
				nPos = sPath.IndexOf(PathSeparator);

				string sNode;

				if (nPos == -1)
					sNode = sPath;
				else
					sNode = sPath.Substring(0, nPos);

				aNodes.Add(sNode);

				if (nPos == -1)
					break;

				sPath = sPath.Substring(nPos + PathSeparator.Length);
			}

			string[] aData = new string[aNodes.Count];
			aNodes.CopyTo(aData);

			return aData;
		}

		/// <summary>
		/// Gets the TreeView's subobject at the given point 
		/// </summary>
		/// <param name="oPoint">Point to be tested</param>
		/// <returns>Component found, null otherwise</returns>
		internal Component GetSubObjectAtPoint(Point oPoint)
		{
			foreach (Rectangle oRect in m_mapRectToSubItem.Keys)
			{
				if (oRect.Contains(oPoint) == true)
				{
					return m_mapRectToSubItem[oRect] as Node;
				}
			}

			return null;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="oNode"></param>
		/// <returns></returns>
		internal Rectangle GetNodeRect(Node oNode)
		{
			if (oNode == null)
				return Rectangle.Empty;

			if (m_mapSubItemToRect.ContainsKey(oNode) == false)
				return Rectangle.Empty;

			return (Rectangle) m_mapSubItemToRect[oNode];
		}

		/// <summary>
		/// Gets the width for the drawing
		/// </summary>
		/// <returns></returns>
		internal int GetDrawWidth()
		{
			int nWidth = this.Width;

			if (Controls.Contains(m_oScrollBar) == true)
				nWidth -= m_oScrollBar.Width;

			return this.Width;
		}

		/// <summary>
		/// Clears the hiver status
		/// </summary>
		internal void ClearHover()
		{
			HighlightedNode = null;

			Invalidate();
		}

		/// <summary>
		/// Clears the highlight of all objects
		/// </summary>
		internal void ClearDesignHighlight()
		{
			foreach (Node oNode in Nodes)
				oNode.ClearDesignHighlight();
		}

		/// <summary>
		/// Clears the highlight of all objects
		/// </summary>
		internal void ClearDesignSelection()
		{
			foreach (Node oNode in Nodes)
				oNode.ClearDesignSelection();
		}

		/// <summary>
		/// Gets the width of scrollbar
		/// </summary>
		internal int GetScrollBarWidth()
		{
			if (Controls.Contains(m_oScrollBar) == true)
				return m_oScrollBar.Width;

			return 0;
		}

		/// <summary>
		/// Starts the timer for the tooltip
		/// </summary>
		private void StartTooltipTimer()
		{
			// check for tooltips
			if (Tooltips == false)
				return;

			if (m_TooltipTimer != null)
			{
				m_TooltipTimer.Enabled = false;
				m_TooltipTimer.Dispose();
			}

			if (m_Tooltip != null)
			{
				m_Tooltip.Hide();
				m_Tooltip.DestroyHandle();
			}

			if (TooltipNode.TextTruncated == true)
				m_TooltipTimer = new System.Timers.Timer(600);
			else
				m_TooltipTimer = new System.Timers.Timer(800);

			m_TooltipTimer.Elapsed += new ElapsedEventHandler(this.OnTooltipTimer);
			m_TooltipTimer.Enabled = true;
		}

		/// <summary>
		/// Invoke delegate
		/// </summary>
		private delegate Point PointDelegate(Point p);

		/// <summary>
		/// The timer event
		/// </summary>
		private void OnTooltipTimer(object sender, ElapsedEventArgs args)
		{
			// kill the timer
			Timer timer = sender as Timer;
			if (timer == null)
				return;

			m_TooltipTimer.Enabled = false;
			m_TooltipTimer.Dispose();

			if (TooltipNode == null)
				return;

			// get the current mouse position, check the tooltip node and position, if the node is the same 
			// and position is not different too much, then show the tooltip exit otherwise
			Point cursorPos = Cursor.Position;
			object obj = Invoke(new PointDelegate(this.PointToClient), new object[] {cursorPos});
			Node node = this.GetSubObjectAtPoint((Point) obj) as Node;

			if (node != TooltipNode)
			{
				//TooltipNode = null;
				return;
			}

			// if the node is the tooltip node then continue
			string tooltipText = string.Empty;

			if (node.TextTruncated == true)
				tooltipText = node.GetText();

			if (node.Tooltip != null && node.Tooltip != string.Empty)
				tooltipText = node.Tooltip;

			if (tooltipText != string.Empty)
			{
				// show tooltip				
				m_Tooltip = new NodeTooltipWnd();

				// show it in the async call
				if (node.TextTruncated == false)
					m_Tooltip.Show(cursorPos.X + 20, cursorPos.Y, node.GetTooltipStyle(), tooltipText, this.TooltipPopDelay);
				else
				{
					Rectangle nodeRect = this.GetNodeRect(node);

					Point tooltipLoc = (Point) Invoke(new PointDelegate(this.PointToScreen), new object[] {nodeRect.Location});

					m_Tooltip.Show(tooltipLoc.X - 1, tooltipLoc.Y - 2, node.GetTooltipStyle(), tooltipText, this.TooltipPopDelay, false, false);
				}
			}
		}

       	/// <summary>
		/// Based on the treeview settings adds new commands of the menu object
		/// </summary>
		/// <param name="contextMenu"></param>
		private void ContextMenuExtension(ContextMenu contextMenu)
		{
			if (this.ContextMenuEditing == true)
			{
				if ((AllowAdding == true || AllowDeleting == true || LabelEdit == true || AllowEditing == true)
					&& contextMenu.MenuItems.Count > 0)
                    contextMenu.MenuItems.Add("-");

				if (AllowAdding == true)
                    contextMenu.MenuItems.Add(m_oContextMenuString.AddNode, new EventHandler(OnContextMenuEditingAddNode));
                if (AllowDeleting == true)
                    contextMenu.MenuItems.Add(m_oContextMenuString.DeleteNode, new EventHandler(OnContextMenuEditingDeleteNode));
                if (LabelEdit == true)
                    contextMenu.MenuItems.Add(m_oContextMenuString.EditNode, new EventHandler(OnContextMenuEditingEditNode));
                if (AllowEditing == true)
                    contextMenu.MenuItems.Add(m_oContextMenuString.Copy, new EventHandler(OnContextMenuEditingCopy));
                if (AllowEditing == true)
                    contextMenu.MenuItems.Add(m_oContextMenuString.Paste, new EventHandler(OnContextMenuEditingPaste));
			}

			if (this.ContextMenuArranging == true)
			{
				ContextMenu contextMenuArranging = new ContextMenu();

                if (contextMenu.MenuItems.Count > 0)
                    contextMenuArranging.MenuItems.Add("-");

                contextMenuArranging.MenuItems.Add(m_oContextMenuString.Expand, new EventHandler(OnContextMenuArrangingExpand));
                contextMenuArranging.MenuItems.Add(m_oContextMenuString.Collapse, new EventHandler(OnContextMenuArrangingCollapse));

				if (AllowArranging == true)
				{
                    contextMenuArranging.MenuItems.Add(m_oContextMenuString.MoveTop, new EventHandler(OnContextMenuArrangingMoveTop));
                    contextMenuArranging.MenuItems.Add(m_oContextMenuString.MoveBottom, new EventHandler(OnContextMenuArrangingMoveBottom));
                    contextMenuArranging.MenuItems.Add(m_oContextMenuString.MoveUp, new EventHandler(OnContextMenuArrangingMoveUp));
                    contextMenuArranging.MenuItems.Add(m_oContextMenuString.MoveDown, new EventHandler(OnContextMenuArrangingMoveDown));
                    contextMenuArranging.MenuItems.Add(m_oContextMenuString.MoveLeft, new EventHandler(OnContextMenuArrangingMoveLeft));
                    contextMenuArranging.MenuItems.Add(m_oContextMenuString.MoveRight, new EventHandler(OnContextMenuArrangingMoveRight));
				}

				contextMenu.MergeMenu(contextMenuArranging);
			}

			if (this.ContextMenuXmlOperations == true)
			{
				ContextMenu contextMenuXmlOperations = new ContextMenu();

                if (contextMenu.MenuItems.Count > 0)
                    contextMenuXmlOperations.MenuItems.Add("-");

                contextMenuXmlOperations.MenuItems.Add(m_oContextMenuString.SaveXml, new EventHandler(OnContextMenuXmlOperationsSave));
                contextMenuXmlOperations.MenuItems.Add(m_oContextMenuString.LoadXml, new EventHandler(OnContextMenuXmlOperationsLoad));

				contextMenu.MergeMenu(contextMenuXmlOperations);
			}
		}

		/// <summary>
		/// Based on the treeview settings adds new commands of the menu object
		/// </summary>
		/// <param name="contextMenu"></param>
		private void ContextMenuExtensionTreeView(ContextMenu contextMenu)
		{
			if (this.ContextMenuEditing == true)
			{
                ContextMenu contextMenuEditing = new ContextMenu();

                if ((AllowAdding == true || AllowEditing == true) && contextMenu.MenuItems.Count > 0)
                    contextMenuEditing.MenuItems.Add("-");

				if (AllowAdding == true)
                    contextMenuEditing.MenuItems.Add(m_oContextMenuString.AddNode, new EventHandler(OnContextMenuEditingAddNode));
				if (AllowEditing == true)
                    contextMenuEditing.MenuItems.Add(m_oContextMenuString.Paste, new EventHandler(OnContextMenuEditingPaste));

				contextMenu.MergeMenu(contextMenuEditing);
			}

			if (this.ContextMenuArranging == true)
			{
                ContextMenu contextMenuArranging = new ContextMenu();

                if (contextMenu.MenuItems.Count > 0)
                    contextMenuArranging.MenuItems.Add("-");

                contextMenuArranging.MenuItems.Add(m_oContextMenuString.Expand, new EventHandler(OnContextMenuArrangingExpand));
                contextMenuArranging.MenuItems.Add(m_oContextMenuString.Collapse, new EventHandler(OnContextMenuArrangingCollapse));

				contextMenu.MergeMenu(contextMenuArranging);
			}

			if (this.ContextMenuXmlOperations == true)
			{
                ContextMenu contextMenuXmlOperations = new ContextMenu();

                if (contextMenu.MenuItems.Count > 0)
                    contextMenuXmlOperations.MenuItems.Add("-");

                contextMenuXmlOperations.MenuItems.Add(m_oContextMenuString.SaveXml, new EventHandler(OnContextMenuXmlOperationsSave));
                contextMenuXmlOperations.MenuItems.Add(m_oContextMenuString.LoadXml, new EventHandler(OnContextMenuXmlOperationsLoad));

				contextMenu.MergeMenu(contextMenuXmlOperations);
			}
		}

		/// <summary>
		/// Starts the inplace editing session for the given node
		/// </summary>
		/// <param name="node"></param>
		internal void StartInplaceEdit(Node node)
		{
			if (LabelEdit == false)
				return;

			this.Refresh();

			node.IsSelected = true;
			this.EditingNode = node;

			Rectangle rect = this.GetNodeRect(node);
			m_textBox.Multiline = true;
			m_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			m_textBox.BackColor = Color.White;//node.GetSelectedBackColor();
			m_textBox.Text = node.Text;
			m_textBox.Left = rect.X;
			m_textBox.Top = rect.Y - 1;
			m_textBox.Width = rect.Width + 3 - GetScrollBarWidth();
			m_textBox.Height = rect.Height + 1;
			m_textBox.LostFocus += m_OnInplaceEditLostFocus;
			m_textBox.KeyPress += m_OnInplaceEditKeyPress;
			m_textBox.CreateControl();
			m_textBox.Parent = this;

			m_textBox.Visible = true;
			m_textBox.Focus();
		}

		private Node CloneNode(Node oNode)
		{
			return CloneNode(oNode, true);
		}

		private Node CloneNode(Node oNode, bool bExpand)
		{
			Node oCloneNode = new Node();

			oCloneNode.NodeMoving = true;
			oCloneNode.Parent = null;
			oCloneNode.Copy(oNode);
			oCloneNode._TreeView = oNode._TreeView;

			if (bExpand == true)
				oCloneNode.Expand();

			foreach (Node oSubNode in oNode.Nodes.ToNodeArray())
			{
				Node oSubNodeClone = CloneNode(oSubNode, bExpand);

				oSubNodeClone.Parent = oCloneNode;
			}

			oCloneNode.NodeMoving = false;

			return oCloneNode;
		}

		#endregion       

		#region ISupportInitialize members

        /// <summary>
        /// Initialziation begin.
        /// </summary>
		public void BeginInit()
		{
			m_Initialized = false;
		}

        /// <summary>
        /// Initialization end.
        /// </summary>
		public void EndInit()
		{
			m_Initialized = true;

			this.Controls.Clear();
		}

		#endregion

		#region delegates and events

		#region node events

		#region node expand, collapse, select events

		/// <summary>
		/// After node expand event handler
		/// </summary>
		public delegate void AfterNodeExpandEventHandler(Node oNode);

		/// <summary>
		/// After node expand event definition
		/// </summary>
		public event AfterNodeExpandEventHandler AfterNodeExpand;

		/// <summary>
		/// After node expand virtual function
		/// </summary>		
		protected virtual void OnAfterNodeExpand(Node oNode)
		{
			if (AfterNodeExpand != null) AfterNodeExpand(oNode);
		}

		internal void InvokeAfterNodeExpand(Node oNode)
		{
			OnAfterNodeExpand(oNode);
		}

		/// <summary>
		/// After node checked event handler
		/// </summary>
		public delegate void AfterNodeCheckEventHandler(Node oNode);

		/// <summary>
		/// After node checked event definition
		/// </summary>
		public event AfterNodeCheckEventHandler AfterNodeCheck;

		/// <summary>
		/// After node checked virtual function
		/// </summary>		
		protected virtual void OnAfterNodeCheck(Node oNode)
		{
			if (AfterNodeCheck != null) AfterNodeCheck(oNode);
		}

		internal void InvokeAfterNodeCheck(Node oNode)
		{
			OnAfterNodeCheck(oNode);
		}

		/// <summary>
		/// After node collapse event handler
		/// </summary>
		public delegate void AfterNodeCollapseEventHandler(Node oNode);

		/// <summary>
		/// After node collapse event definition
		/// </summary>
		public event AfterNodeCollapseEventHandler AfterNodeCollapse;

		/// <summary>
		/// After node collapse virtual function
		/// </summary>		
		protected virtual void OnAfterNodeCollapse(Node oNode)
		{
			if (AfterNodeCollapse != null) AfterNodeCollapse(oNode);
		}

		internal void InvokeAfterNodeCollapse(Node oNode)
		{
			OnAfterNodeCollapse(oNode);
		}

		/// <summary>
		/// After node select event handler
		/// </summary>
		public delegate void AfterNodeSelectEventHandler(Node oNode);

		/// <summary>
		/// After node select event definition
		/// </summary>
		public event AfterNodeSelectEventHandler AfterNodeSelect;

		/// <summary>
		/// After node select virtual function
		/// </summary>		
		protected virtual void OnAfterNodeSelect(Node oNode)
		{
			if (AfterNodeSelect != null) AfterNodeSelect(oNode);
		}

		internal void InvokeAfterNodeSelect(Node oNode)
		{
			OnAfterNodeSelect(oNode);
		}

		/// <summary>
		/// Fired when multiple selection is switched on and the selection has changed
		/// </summary>
		public event EventHandler NodeSelectionChange;

		protected virtual void OnNodeSelectionChange(object sender, EventArgs args)
		{
			if (NodeSelectionChange != null) NodeSelectionChange(sender, args);
		}

		internal void InvokeNodeSelectionChange()
		{
			OnNodeSelectionChange(this, EventArgs.Empty);
		}

		/// <summary>
		/// Before node expand event handler
		/// </summary>
		public delegate void BeforeNodeExpandEventHandler(Node oNode, CancelEventArgs e);

		/// <summary>
		/// Before node expand event definition
		/// </summary>
		public event BeforeNodeExpandEventHandler BeforeNodeExpand;

		/// <summary>
		/// Before node expand virtual function
		/// </summary>		
		protected virtual void OnBeforeNodeExpand(Node oNode, CancelEventArgs e)
		{
			if (BeforeNodeExpand != null) BeforeNodeExpand(oNode, e);
		}

		internal void InvokeBeforeNodeExpand(Node oNode, CancelEventArgs e)
		{
			OnBeforeNodeExpand(oNode, e);
		}

		/// <summary>
		/// Before node check event handler
		/// </summary>
		public delegate void BeforeNodeCheckEventHandler(Node oNode, CancelEventArgs e);

		/// <summary>
		/// Before node check event definition
		/// </summary>
		public event BeforeNodeCheckEventHandler BeforeNodeCheck;

		/// <summary>
		/// Before node check virtual function
		/// </summary>		
		protected virtual void OnBeforeNodeCheck(Node oNode, CancelEventArgs e)
		{
			if (BeforeNodeCheck != null) BeforeNodeCheck(oNode, e);
		}

		internal void InvokeBeforeNodeCheck(Node oNode, CancelEventArgs e)
		{
			OnBeforeNodeCheck(oNode, e);
		}

		/// <summary>
		/// Before node collapse event handler
		/// </summary>
		public delegate void BeforeNodeCollapseEventHandler(Node oNode, CancelEventArgs e);

		/// <summary>
		/// Before node collapse event definition
		/// </summary>
		public event BeforeNodeCollapseEventHandler BeforeNodeCollapse;

		/// <summary>
		/// Before node collapse virtual function
		/// </summary>		
		protected virtual void OnBeforeNodeCollapse(Node oNode, CancelEventArgs e)
		{
			if (BeforeNodeCollapse != null) BeforeNodeCollapse(oNode, e);
		}

		internal void InvokeBeforeNodeCollapse(Node oNode, CancelEventArgs e)
		{
			OnBeforeNodeCollapse(oNode, e);
		}

		/// <summary>
		/// Before node select event handler
		/// </summary>
		public delegate void BeforeNodeSelectEventHandler(Node oNode, CancelEventArgs e);

		/// <summary>
		/// Before node select event definition
		/// </summary>
		public event BeforeNodeSelectEventHandler BeforeNodeSelect;

		/// <summary>
		/// Before node select virtual function
		/// </summary>		
		protected virtual void OnBeforeNodeSelect(Node oNode, CancelEventArgs e)
		{
			if (BeforeNodeSelect != null) BeforeNodeSelect(oNode, e);
		}

		internal void InvokeBeforeNodeSelect(Node oNode, CancelEventArgs e)
		{
			OnBeforeNodeSelect(oNode, e);
		}

		/// <summary>
		/// After node text edit event handler
		/// </summary>
		public delegate void AfterLabelEditEventHandler(Node oNode);

		/// <summary>
		/// After node text edit event definition
		/// </summary>
		public event AfterLabelEditEventHandler AfterLabelEdit;

		/// <summary>
		/// After node text edit  virtual function
		/// </summary>		
		protected virtual void OnAfterLabelEdit(Node oNode)
		{
			if (AfterLabelEdit != null) AfterLabelEdit(oNode);
		}

		internal void InvokeAfterLabelEdit(Node oNode)
		{
			OnAfterLabelEdit(oNode);
		}

		/// <summary>
		/// Before node TextEdit event handler
		/// </summary>
		public delegate void BeforeLabelEditEventHandler(Node oNode, CancelEventArgs e);

		/// <summary>
		/// Before node TextEdit event definition
		/// </summary>
		public event BeforeLabelEditEventHandler BeforeLabelEdit;

		/// <summary>
		/// Before node TextEdit virtual function
		/// </summary>		
		protected virtual void OnBeforeLabelEdit(Node oNode, CancelEventArgs e)
		{
			if (BeforeLabelEdit != null) BeforeLabelEdit(oNode, e);
		}

		internal void InvokeBeforeLabelEdit(Node oNode, CancelEventArgs e)
		{
			OnBeforeLabelEdit(oNode, e);
		}

		#endregion

		#region node added, removed events

		/// <summary>
		/// Node position changed delegate
		/// </summary>
		public delegate void BeforeNodePositionChangeEventHandler(Node oNode);

		/// <summary>
		/// Node position changed event definition
		/// </summary>
		public event BeforeNodePositionChangeEventHandler BeforeNodePositionChange;

		/// <summary>
		/// Node position changed virtual function
		/// </summary>
		/// <param name="oNode"></param>
		protected virtual void OnBeforeNodePositionChange(Node oNode)
		{
			if (BeforeNodePositionChange != null) BeforeNodePositionChange(oNode);
		}

		/// <summary>
		/// Node position changed invoke handler
		/// </summary>
		/// <param name="oNode"></param>
		internal void InvokeBeforeNodePositionChange(Node oNode)
		{
			OnBeforeNodePositionChange(oNode);
		}

		/// <summary>
		/// Node position changed delegate
		/// </summary>
		public delegate void AfterNodePositionChangeEventHandler(Node oNode);

		/// <summary>
		/// Node position changed event definition
		/// </summary>
		public event AfterNodePositionChangeEventHandler AfterNodePositionChange;

		/// <summary>
		/// Node position changed virtual function
		/// </summary>
		/// <param name="oNode"></param>
		protected virtual void OnAfterNodePositionChange(Node oNode)
		{
			if (AfterNodePositionChange != null) AfterNodePositionChange(oNode);
		}

		/// <summary>
		/// Node position changed invoke handler
		/// </summary>
		/// <param name="oNode"></param>
		internal void InvokeAfterNodePositionChange(Node oNode)
		{
			OnAfterNodePositionChange(oNode);
		}

		/// <summary>
		/// Node added delegate
		/// </summary>
		public delegate void NodeAddedEventHandler(Node oNode);

		/// <summary>
		/// Node Removed event definition
		/// </summary>
		public event NodeAddedEventHandler NodeAdded;

		/// <summary>
		/// Node Added virtual function
		/// </summary>
		/// <param name="oNode"></param>
		protected virtual void OnNodeAdded(Node oNode)
		{
			if (NodeAdded != null) NodeAdded(oNode);
		}

		/// <summary>
		/// Node Added invoke handler
		/// </summary>
		/// <param name="oNode"></param>
		internal void InvokeNodeAdded(Node oNode)
		{
			if (CanFireNodeEvent == false) return;
			if (oNode != null && m_Initialized == true) OnNodeAdded(oNode);
		}

		/// <summary>
		/// Node removed delegate
		/// </summary>
		public delegate void NodeRemovedEventHandler(Node oNode);

		/// <summary>
		/// Node Removed event definition
		/// </summary>
		public event NodeRemovedEventHandler NodeRemoved;

		/// <summary>
		/// Node Removed virtual function
		/// </summary>
		/// <param name="oNode"></param>
		protected virtual void OnNodeRemoved(Node oNode)
		{
			if (NodeRemoved != null) NodeRemoved(oNode);
		}

		/// <summary>
		/// Node Removed invoke handler
		/// </summary>
		/// <param name="oNode"></param>
		internal void InvokeNodeRemoved(Node oNode)
		{
			if (CanFireNodeEvent == false) return;
			if (oNode != null && m_Initialized == true) OnNodeRemoved(oNode);

			if (oNode != null && oNode.Panel != null)
				this.Controls.Remove(oNode.Panel);
		}

		#endregion

		#region node mouse events

		/// <summary>
		/// Node MouseClick delegate
		/// </summary>
		public delegate void NodeMouseClickEventHandler(EventArgs e, Node oNode);

		/// <summary>
		/// Node MouseClick event definition
		/// </summary>
		public event NodeMouseClickEventHandler NodeMouseClick;

		/// <summary>
		/// Node MouseClick virtual function
		/// </summary>
		/// <param name="oNode"></param>
		protected virtual void OnNodeMouseClick(EventArgs e, Node oNode)
		{
			if (NodeMouseClick != null) NodeMouseClick(e, oNode);
		}

		/// <summary>
		/// Node MouseClick invoke handler
		/// </summary>
		/// <param name="oNode"></param>
		internal void InvokeNodeMouseClick(EventArgs e, Node oNode)
		{
			OnNodeMouseClick(e, oNode);
		}

		/// <summary>
		/// Node MouseDblClick delegate
		/// </summary>
		public delegate void NodeMouseDblClickEventHandler(EventArgs e, Node oNode);

		/// <summary>
		/// Node MouseDblClick event definition
		/// </summary>
		public event NodeMouseDblClickEventHandler NodeMouseDblClick;

		/// <summary>
		/// Node MouseDblClick virtual function
		/// </summary>
		/// <param name="oNode"></param>
		protected virtual void OnNodeMouseDblClick(EventArgs e, Node oNode)
		{
			if (NodeMouseDblClick != null) NodeMouseDblClick(e, oNode);
		}

		/// <summary>
		/// Node MouseDblClick invoke handler
		/// </summary>
		/// <param name="oNode"></param>
		internal void InvokeNodeMouseDblClick(EventArgs e, Node oNode)
		{
			OnNodeMouseDblClick(e, oNode);
		}

		/// <summary>
		/// Node MouseDown delegate
		/// </summary>
		public delegate void NodeMouseDownEventHandler(MouseEventArgs e, Node oNode);

		/// <summary>
		/// Node MouseDown event definition
		/// </summary>
		public event NodeMouseDownEventHandler NodeMouseDown;

		/// <summary>
		/// Node MouseDown virtual function
		/// </summary>
		/// <param name="oNode"></param>
		protected virtual void OnNodeMouseDown(MouseEventArgs e, Node oNode)
		{
			if (NodeMouseDown != null) NodeMouseDown(e, oNode);
		}

		/// <summary>
		/// Node MouseDown invoke handler
		/// </summary>
		/// <param name="oNode"></param>
		internal void InvokeNodeMouseDown(MouseEventArgs e, Node oNode)
		{
			OnNodeMouseDown(e, oNode);
		}

		/// <summary>
		/// Node MouseUp delegate
		/// </summary>
		public delegate void NodeMouseUpEventHandler(MouseEventArgs e, Node oNode);

		/// <summary>
		/// Node MouseUp event definition
		/// </summary>
		public event NodeMouseUpEventHandler NodeMouseUp;

		/// <summary>
		/// Node MouseUp virtual function
		/// </summary>
		/// <param name="oNode"></param>
		protected virtual void OnNodeMouseUp(MouseEventArgs e, Node oNode)
		{
			if (NodeMouseUp != null) NodeMouseUp(e, oNode);
		}

		/// <summary>
		/// Node MouseUp invoke handler
		/// </summary>
		/// <param name="oNode"></param>
		internal void InvokeNodeMouseUp(MouseEventArgs e, Node oNode)
		{
			OnNodeMouseUp(e, oNode);
		}

		/// <summary>
		/// Node MouseMove delegate
		/// </summary>
		public delegate void NodeMouseMoveEventHandler(MouseEventArgs e, Node oNode);

		/// <summary>
		/// Node MouseMove event definition
		/// </summary>
		public event NodeMouseMoveEventHandler NodeMouseMove;

		/// <summary>
		/// Node MouseMove virtual function
		/// </summary>
		/// <param name="oNode"></param>
		protected virtual void OnNodeMouseMove(MouseEventArgs e, Node oNode)
		{
			if (NodeMouseMove != null) NodeMouseMove(e, oNode);
		}

		/// <summary>
		/// Node MouseMove invoke handler
		/// </summary>
		/// <param name="oNode"></param>
		internal void InvokeNodeMouseMove(MouseEventArgs e, Node oNode)
		{
			OnNodeMouseMove(e, oNode);
		}

		/// <summary>
		/// Node Mouse enter delegate
		/// </summary>
		public delegate void NodeMouseEnterEventHandler(Node oNode);

		/// <summary>
		/// Node Mouse enter event definition
		/// </summary>
		public event NodeMouseEnterEventHandler NodeMouseEnter;

		/// <summary>
		/// Node Mouse enter virtual function
		/// </summary>
		/// <param name="oNode"></param>
		protected virtual void OnNodeMouseEnter(Node oNode)
		{
			if (NodeMouseEnter != null) NodeMouseEnter(oNode);
		}

		/// <summary>
		/// Node Mouse enter invoke handler
		/// </summary>
		/// <param name="oNode"></param>
		internal void InvokeNodeMouseEnter(Node oNode)
		{
			this.TooltipNode = oNode;
			StartTooltipTimer();
			OnNodeMouseEnter(oNode);
		}

		/// <summary>
		/// Node Mouse Leave delegate
		/// </summary>
		public delegate void NodeMouseLeaveEventHandler(Node oNode);

		/// <summary>
		/// Node Mouse Leave event definition
		/// </summary>
		public event NodeMouseLeaveEventHandler NodeMouseLeave;

		/// <summary>
		/// Node Mouse Leave virtual function
		/// </summary>
		/// <param name="oNode"></param>
		protected virtual void OnNodeMouseLeave(Node oNode)
		{
			if (NodeMouseLeave != null) NodeMouseLeave(oNode);
		}

		/// <summary>
		/// Node Mouse Leave invoke handler
		/// </summary>
		/// <param name="oNode"></param>
		internal void InvokeNodeMouseLeave(Node oNode)
		{
			m_Tooltip.Hide(); /*m_Tooltip.DestroyHandle(); */
			OnNodeMouseLeave(oNode);
		}

		#endregion

		#region node flag events

		/// <summary>
		/// NodeFlag MouseClick event definition
		/// </summary>
		public event NodeMouseClickEventHandler NodeFlagMouseClick;

		/// <summary>
		/// NodeFlag MouseClick virtual function
		/// </summary>
		/// <param name="oNode"></param>
		protected virtual void OnNodeFlagMouseClick(EventArgs e, Node oNode)
		{
			if (NodeFlagMouseClick != null) NodeFlagMouseClick(e, oNode);
		}

		/// <summary>
		/// NodeFlag MouseClick invoke handler
		/// </summary>
		/// <param name="oNode"></param>
		internal void InvokeNodeFlagMouseClick(EventArgs e, Node oNode)
		{
			OnNodeFlagMouseClick(e, oNode);
		}

		/// <summary>
		/// NodeFlag MouseDblClick event definition
		/// </summary>
		public event NodeMouseDblClickEventHandler NodeFlagMouseDblClick;

		/// <summary>
		/// NodeFlag MouseDblClick virtual function
		/// </summary>
		/// <param name="oNode"></param>
		protected virtual void OnNodeFlagMouseDblClick(EventArgs e, Node oNode)
		{
			if (NodeFlagMouseDblClick != null) NodeFlagMouseDblClick(e, oNode);
		}

		/// <summary>
		/// NodeFlag MouseDblClick invoke handler
		/// </summary>
		/// <param name="oNode"></param>
		internal void InvokeNodeFlagMouseDblClick(EventArgs e, Node oNode)
		{
			OnNodeFlagMouseDblClick(e, oNode);
		}

		/// <summary>
		/// NodeFlag MouseDown event definition
		/// </summary>
		public event NodeMouseDownEventHandler NodeFlagMouseDown;

		/// <summary>
		/// NodeFlag MouseDown virtual function
		/// </summary>
		/// <param name="oNode"></param>
		protected virtual void OnNodeFlagMouseDown(MouseEventArgs e, Node oNode)
		{
			if (NodeFlagMouseDown != null) NodeFlagMouseDown(e, oNode);
		}

		/// <summary>
		/// NodeFlag MouseDown invoke handler
		/// </summary>
		/// <param name="oNode"></param>
		internal void InvokeNodeFlagMouseDown(MouseEventArgs e, Node oNode)
		{
			OnNodeFlagMouseDown(e, oNode);
		}

		/// <summary>
		/// NodeFlag MouseUp event definition
		/// </summary>
		public event NodeMouseUpEventHandler NodeFlagMouseUp;

		/// <summary>
		/// NodeFlag MouseUp virtual function
		/// </summary>
		/// <param name="oNode"></param>
		protected virtual void OnNodeFlagMouseUp(MouseEventArgs e, Node oNode)
		{
			if (NodeFlagMouseUp != null) NodeFlagMouseUp(e, oNode);
		}

		/// <summary>
		/// NodeFlag MouseUp invoke handler
		/// </summary>
		/// <param name="oNode"></param>
		internal void InvokeNodeFlagMouseUp(MouseEventArgs e, Node oNode)
		{
			OnNodeFlagMouseUp(e, oNode);
		}

		#endregion

		#region node drag drop events

		/// <summary>
		/// Node DragDrop delegate
		/// </summary>
		public delegate void NodeDragDropEventHandler(DragEventArgs e, Node oNode);

		/// <summary>
		/// Node DragDrop event definition
		/// </summary>
		public event NodeDragDropEventHandler NodeDragDrop;

		/// <summary>
		/// Node DragDrop virtual function
		/// </summary>
		/// <param name="oNode"></param>
		protected virtual void OnNodeDragDrop(DragEventArgs e, Node oNode)
		{
			if (NodeDragDrop != null) NodeDragDrop(e, oNode);
		}

		/// <summary>
		/// Node DragDrop invoke handler
		/// </summary>
		/// <param name="oNode"></param>
		internal void InvokeNodeDragDrop(DragEventArgs e, Node oNode)
		{
			OnNodeDragDrop(e, oNode);
		}

		/// <summary>
		/// Node DragOver delegate
		/// </summary>
		public delegate void NodeDragOverEventHandler(DragEventArgs e, Node oNode);

		/// <summary>
		/// Node DragOver event definition
		/// </summary>
		public event NodeDragOverEventHandler NodeDragOver;

		/// <summary>
		/// Node DragOver virtual function
		/// </summary>
		/// <param name="oNode"></param>
		protected virtual void OnNodeDragOver(DragEventArgs e, Node oNode)
		{
			if (NodeDragOver != null) NodeDragOver(e, oNode);
		}

		/// <summary>
		/// Node DragOver invoke handler
		/// </summary>
		/// <param name="oNode"></param>
		internal void InvokeNodeDragOver(DragEventArgs e, Node oNode)
		{
			OnNodeDragOver(e, oNode);
		}

		/// <summary>
		/// Node DragEnter delegate
		/// </summary>
		public delegate void NodeDragEnterEventHandler(DragEventArgs e, Node oNode);

		/// <summary>
		/// Node DragEnter event definition
		/// </summary>
		public event NodeDragEnterEventHandler NodeDragEnter;

		/// <summary>
		/// Node DragEnter virtual function
		/// </summary>
		/// <param name="oNode"></param>
		protected virtual void OnNodeDragEnter(DragEventArgs e, Node oNode)
		{
			if (NodeDragEnter != null) NodeDragEnter(e, oNode);
		}

		/// <summary>
		/// Node DragEnter invoke handler
		/// </summary>
		/// <param name="oNode"></param>
		internal void InvokeNodeDragEnter(DragEventArgs e, Node oNode)
		{
			OnNodeDragEnter(e, oNode);
		}

		/// <summary>
		/// Node DragLeave delegate
		/// </summary>
		public delegate void NodeDragLeaveEventHandler(DragEventArgs e, Node oNode);

		/// <summary>
		/// Node DragLeave event definition
		/// </summary>
		public event NodeDragLeaveEventHandler NodeDragLeave;

		/// <summary>
		/// Node DragLeave virtual function
		/// </summary>
		/// <param name="oNode"></param>
		protected virtual void OnNodeDragLeave(DragEventArgs e, Node oNode)
		{
			if (NodeDragLeave != null) NodeDragLeave(e, oNode);
		}

		/// <summary>
		/// Node DragLeave invoke handler
		/// </summary>
		/// <param name="oNode"></param>
		internal void InvokeNodeDragLeave(DragEventArgs e, Node oNode)
		{
			OnNodeDragLeave(e, oNode);
		}

		#endregion

		#endregion

		#endregion

		#region painting

		private void PaintBorder(Graphics oGraphics)
		{
			if (this.Style.BorderStyle == BorderStyle.None)
				return;

			if (this.Style.BorderStyle == BorderStyle.Solid || this.Style.BorderStyle == BorderStyle.Dot)
			{
				Pen oPen = new Pen(this.Style.BorderColor);
				oPen.DashStyle = GetBorderStyle(this.Style.BorderStyle);
				oGraphics.DrawRectangle(oPen, 0, 0, Width - 1, Height - 1);
				oPen.Dispose();
			}

			if (this.Style.BorderStyle == BorderStyle.Sunken)
			{
				ControlPaint.DrawBorder3D(oGraphics, 0, 0, Width, Height, Border3DStyle.Sunken);
//				Pen oPen = new Pen(Color.FromArgb(0x80, 0x80, 0x80));				
//				oGraphics.DrawLine(oPen, 0, 0, Width - 1, 0);
//				oGraphics.DrawLine(oPen, 0, 0, 0, Height - 1);
//				
//				oPen.Color = Color.FromArgb(0x71, 0x6f, 0x64);
//				oGraphics.DrawLine(oPen, 1, 1, Width - 1, 1);
//				oGraphics.DrawLine(oPen, 1, 1, 1, Height - 1);
//
//				oPen.Color = Color.FromArgb(0xeb, 0xeb, 0xeb);				
//				oGraphics.DrawLine(oPen, Width - 2, 1, Width - 2, Height - 2);
//				oGraphics.DrawLine(oPen, 1, Height - 2, Width - 2, Height - 2);
//				
//				oPen.Color = Color.FromArgb(0xD4, 0xD0, 0xC8);
//				oGraphics.DrawLine(oPen, Width - 1, 0, Width - 1, Height - 1);
//				oGraphics.DrawLine(oPen, 0, Height - 1, Width - 1, Height - 1);
//
//				oPen.Dispose();
			}

			if (this.Style.BorderStyle == BorderStyle.Raised)
			{
				ControlPaint.DrawBorder3D(oGraphics, 0, 0, Width, Height, Border3DStyle.Raised);

//				Pen oPen = new Pen(Color.White);				
//				oGraphics.DrawLine(oPen, 0, 0, Width - 1, 0);
//				oGraphics.DrawLine(oPen, 0, 0, 0, Height - 1);
//				
//				oPen.Color = this.Style.BorderColor;
//				oGraphics.DrawLine(oPen, 1, 1, Width - 1, 1);
//				oGraphics.DrawLine(oPen, 1, 1, 1, Height - 1);
//
//				oPen.Color = Color.White;				
//				oGraphics.DrawLine(oPen, Width - 2, 1, Width - 2, Height - 2);
//				oGraphics.DrawLine(oPen, 1, Height - 2, Width - 2, Height - 2);
//				
//				oPen.Color = Color.FromArgb(196, this.Style.BorderColor);
//				oGraphics.DrawLine(oPen, Width - 1, 0, Width - 1, Height - 1);
//				oGraphics.DrawLine(oPen, 0, Height - 1, Width - 1, Height - 1);
//
//				oPen.Dispose();
			}
		}

		private void PaintNodes(Graphics oGraphics)
		{
			m_mapItemBoxToRect.Clear();
			m_mapItemCheckToRect.Clear();
			m_mapItemFlagToRect.Clear();
			m_mapRectToItemBox.Clear();
			m_mapRectToItemCheck.Clear();
			m_mapRectToItemFlag.Clear();
			m_mapRectToSubItem.Clear();
			m_mapSubItemToRect.Clear();

			if (IsDesignMode == true && Nodes.Count == 0)
			{
				m_nScroll = 0;
				m_nHScroll = 0;

				Controls.Remove(m_oScrollBar);
				Controls.Remove(m_oHScrollBar);

				string s = "Please, use right click mouse to add nodes.";
				SizeF size = oGraphics.MeasureString(s, Font);

				oGraphics.DrawString(s, Font, Brushes.Gray, new Rectangle(1, (int) ((double) Height / 2.0) -
					(int) (size.Height / 2.0), Width - 1, (int) (Height / 2.0)));

				return;
			}

			NodePainter painter = new NodePainter(this);

			int nX = 18 + m_nHScroll;
			int nY = 6 + m_nScroll;

			if (Flags == true)
				nX += 10;

			if (this.Style.ShowPlusMinus == false)
				nX -= 15;

			// draw all subitems
			if (Sorted == true)
			{
				ArrayList aList = new ArrayList();

				foreach (Node oSubNode in Nodes)
					aList.Add(oSubNode);

				aList.Sort();

				for (int nNode = 0; nNode < aList.Count; nNode ++)
				{
					Node oSubNode = aList[nNode] as Node;

					if (oSubNode == null || oSubNode.Visible == false)
						continue;

					painter.PaintNode(oSubNode, oGraphics, ref nX, ref nY, Width, ref m_mapSubItemToRect,
					                  ref m_mapRectToSubItem, ref m_mapRectToItemBox, ref m_mapItemBoxToRect,
					                  ref m_mapRectToItemCheck, ref m_mapItemCheckToRect,
					                  ref m_mapRectToItemFlag, ref m_mapItemFlagToRect);					
				}
			}
			else
			{
				// sort the nodes by the m_nCollectionOrder
//				SortedList aList = new SortedList();
//
//				foreach (Node oSubNode in Nodes)
//				{
//					if (oSubNode.m_nCollectionOrder == -1)
//						oSubNode.m_nCollectionOrder = oSubNode.Index;
//
//					try
//					{
//						aList.Add(oSubNode.m_nCollectionOrder, oSubNode);
//					}
//					catch
//					{
//						try
//						{
//							aList.Add(oSubNode.Index, oSubNode);
//						}
//						catch
//						{
//							aList.Add(aList.Count + 1, oSubNode);
//						}
//					}
//				}	

				foreach (Node oSubNode in Nodes)
				{
					if (oSubNode.m_nCollectionOrder == -1)
						oSubNode.m_nCollectionOrder = oSubNode.Index;
				}

				Node[] nodes = this.Nodes.GetNodesCollectionSorted();
				for (int nNode = 0; nNode < nodes.Length; nNode ++)
				{
					Node oSubNode = nodes[nNode] as Node;

					if (oSubNode.Visible == false)
						continue;

					painter.PaintNode(oSubNode, oGraphics, ref nX, ref nY, Width, ref m_mapSubItemToRect,
					                  ref m_mapRectToSubItem, ref m_mapRectToItemBox, ref m_mapItemBoxToRect,
					                  ref m_mapRectToItemCheck, ref m_mapItemCheckToRect, ref m_mapRectToItemFlag, ref m_mapItemFlagToRect);					
				}
			}

			// paint the flashing lines properly with the flash solid line
			painter.PaintNodeFlashLines(this.FlashingNode, oGraphics);

			// groups are all drawn. calc the selected group items height and if it is bigger than than its drawing area
			// add the scrollbar to the group object

			#region scrollbar check vertical

			if (Nodes.Count == 0)
			{
				m_nScroll = 0;
				m_nHScroll = 0;

				Controls.Remove(m_oScrollBar);
				Controls.Remove(m_oHScrollBar);

				return;
			}

			// the group height
			int nGroupHeight = CalcItemHeight(oGraphics);

			// calc the drawing height here
			int nDrawingHeight = this.Height - 2;

			// the group height
			int nGroupWidth = CalcItemWidth(oGraphics) + 15;

			// calc the drawing width here
			int nDrawingWidth = this.GetDrawWidth();

			if (nGroupWidth > nDrawingWidth || nGroupWidth > nDrawingWidth - m_oScrollBar.Width)
				nDrawingHeight -= m_oHScrollBar.Height;

			if (nGroupHeight > nDrawingHeight)
			{
				if (Controls.Contains(m_oScrollBar) == false)
				{
					Controls.Add(m_oScrollBar);
				}

				// draw the scroller
				if (Style.BorderStyle == BorderStyle.Raised || Style.BorderStyle == BorderStyle.Sunken)
				{
					m_oScrollBar.Left = Width - m_oScrollBar.Width - 2;
					m_oScrollBar.Top = 2;
					m_oScrollBar.Height = nDrawingHeight - 2;
				}
				else
				{
					m_oScrollBar.Left = Width - m_oScrollBar.Width - 1;
					m_oScrollBar.Top = 1;
					m_oScrollBar.Height = nDrawingHeight;
				}

				// set the scroll bars maximum, it is the rest between the height and the drawing area						
				m_oScrollBar.Maximum = nGroupHeight;

				if (nDrawingHeight > 0)
				{
					if (-m_nScroll < m_oScrollBar.Minimum)
						m_nScroll = -m_oScrollBar.Minimum;

					if (-m_nScroll > m_oScrollBar.Maximum)
						m_nScroll = -m_oScrollBar.Maximum;

					m_oScrollBar.Value = -m_nScroll;

					m_oScrollBar.LargeChange = nDrawingHeight;
					m_oScrollBar.SmallChange = (int) ((float) m_oScrollBar.LargeChange / 5.0);
				}
			}
			else
			{
				if (Controls.Contains(m_oScrollBar) == true)
				{
					m_nScroll = 0;
					Controls.Remove(m_oScrollBar);
				}
			}

			#endregion

			#region scrollbar check horizontal

			if (Nodes.Count == 0)
			{
				m_nHScroll = 0;
				Controls.Remove(m_oHScrollBar);

				return;
			}

			if (Controls.Contains(m_oScrollBar))
				nDrawingWidth -= m_oScrollBar.Width;

			if (Multiline == false)
			{
				if (nGroupWidth > nDrawingWidth)
				{
					if (Controls.Contains(m_oHScrollBar) == false)
					{
						Controls.Add(m_oHScrollBar);
					}

					// draw the scroller
					if (Style.BorderStyle == BorderStyle.Raised || Style.BorderStyle == BorderStyle.Sunken)
					{
						m_oHScrollBar.Top = this.Height - m_oHScrollBar.Height - 2;
						m_oHScrollBar.Left = 2;
						m_oHScrollBar.Width = nDrawingWidth - 4;
					}
					else if (Style.BorderStyle != BorderStyle.None)
					{
						m_oHScrollBar.Top = this.Height - m_oHScrollBar.Height - 1;
						m_oHScrollBar.Left = 1;
						m_oHScrollBar.Width = nDrawingWidth - 2;
					}
					else
					{
						m_oHScrollBar.Top = this.Height - m_oHScrollBar.Height;
						m_oHScrollBar.Left = 0;
						m_oHScrollBar.Width = nDrawingWidth - 2;
					}

					// set the scroll bars maximum, it is the rest between the height and the drawing area						
					m_oHScrollBar.Maximum = nGroupWidth;

					if (nDrawingWidth > 0)
					{
						if (-m_nHScroll < m_oHScrollBar.Minimum)
							m_nHScroll = -m_oHScrollBar.Minimum;

						if (-m_nHScroll > m_oHScrollBar.Maximum)
							m_nHScroll = -m_oHScrollBar.Maximum;

						m_oHScrollBar.Value = -m_nHScroll;

						m_oHScrollBar.LargeChange = nDrawingWidth;
						m_oHScrollBar.SmallChange = (int) ((float) m_oHScrollBar.LargeChange / 5.0);
					}
				}
				else
				{
					if (Controls.Contains(m_oHScrollBar) == true)
					{
						m_nHScroll = 0;
						Controls.Remove(m_oHScrollBar);
					}
				}
			}

			#endregion

			// last check, if we have both scrollbars we have to paint the rectangle in the right bottom corner			
			if (Controls.Contains(m_oHScrollBar) == true && Controls.Contains(m_oScrollBar) == true)
			{
				Brush brush = new SolidBrush(SystemColors.Control);
				oGraphics.FillRectangle(brush, m_oScrollBar.Left, m_oHScrollBar.Top, m_oScrollBar.Width, m_oHScrollBar.Height);
				brush.Dispose();
			}

			// fit panels
			foreach (Node node in NodePanelPool)
				node.FitPanel();
		}

		private void PaintDropLines(Graphics oGraphics)
		{
			if (DragDropNode != null)
				DragDropNode.Paint(this, oGraphics);
		}

		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			if (this.BackColor != this.Style.BackColor)
				this.BackColor = this.Style.BackColor;

			base.OnPaintBackground(pevent);

			if (this.Style.BackColor != Color.Transparent)
			{
				if (this.Style.FillStyle == FillStyle.Flat)
				{
					Brush brush = new SolidBrush(this.GetTreeBackColor());
					pevent.Graphics.FillRectangle(brush, 0, 0, Width, Height);
					brush.Dispose();
					brush = null;
				}

				if (this.Style.FillStyle == FillStyle.VerticalFading)
				{
					LinearGradientBrush oBrush = new LinearGradientBrush
						(
						new Point(0, 0),
						new Point(0, Height),
						this.GetTreeBackFadeColor(),
						this.GetTreeBackColor()
						);

					pevent.Graphics.FillRectangle(oBrush, 0, 0, Width, Height);
					oBrush.Dispose();
					oBrush = null;
				}

				if (this.Style.FillStyle == FillStyle.HorizontalFading)
				{
					LinearGradientBrush oBrush = new LinearGradientBrush
						(
						new Point(Width, Height),
						new Point(0, Height),
						this.GetTreeBackColor(),
						this.GetTreeBackFadeColor()
						);

					pevent.Graphics.FillRectangle(oBrush, 0, 0, Width, Height);
					oBrush.Dispose();
					oBrush = null;
				}

				if (this.Style.FillStyle == FillStyle.DiagonalBackward)
				{
					LinearGradientBrush oBrush = new LinearGradientBrush
						(
						new Rectangle(0, 0, Width, Height),
						this.GetTreeBackFadeColor(),
						this.GetTreeBackColor(),
						LinearGradientMode.BackwardDiagonal
						);

					pevent.Graphics.FillRectangle(oBrush, 0, 0, Width, Height);
					oBrush.Dispose();
					oBrush = null;
				}

				if (this.Style.FillStyle == FillStyle.DiagonalForward)
				{
					LinearGradientBrush oBrush = new LinearGradientBrush
						(
						new Rectangle(0, 0, Width, Height),
						this.GetTreeBackFadeColor(),
						this.GetTreeBackColor(),
						LinearGradientMode.ForwardDiagonal
						);

					pevent.Graphics.FillRectangle(oBrush, 0, 0, Width, Height);
					oBrush.Dispose();
					oBrush = null;
				}

				if (this.Style.FillStyle == FillStyle.VistaFading)
				{
					AreaPainter.PaintRectangle(pevent.Graphics, 0, 0, Width, Height, FillStyle.VistaFading,
					                           this.GetTreeBackColor(), this.GetTreeBackFadeColor(), BorderStyle.None, Color.Transparent);
				}

				if (this.Style.FillStyle == FillStyle.VerticalCentreFading)
				{
					AreaPainter.PaintRectangle(pevent.Graphics, 0, 0, Width, Height, FillStyle.VerticalCentreFading,
					                           this.GetTreeBackColor(), this.GetTreeBackFadeColor(), BorderStyle.None, Color.Transparent);
				}
			}
		}

		/// <summary>
		/// The internal painting function
		/// </summary>		
		internal void PerformPaiting(Graphics g)
		{
			// clear all maps first
			m_mapRectToSubItem.Clear();
			m_mapSubItemToRect.Clear();
			m_mapRectToItemBox.Clear();
			m_mapItemBoxToRect.Clear();
			m_mapItemCheckToRect.Clear();
			m_mapRectToItemCheck.Clear();
			m_mapItemFlagToRect.Clear();
			m_mapRectToItemFlag.Clear();

			Style.TreeView = this;

			PrepareNodePainting();

			PaintNodes(g);

			PaintBorder(g);

			PaintDropLines(g);

			if (Controls.Contains(m_oScrollBar) == true)
				m_oScrollBar.Refresh();
			if (Controls.Contains(m_oHScrollBar) == true)
				m_oHScrollBar.Refresh();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			try
			{
				base.OnPaint(pe);

				PerformPaiting(pe.Graphics);
			}
			catch (Exception e)
			{
#if DEBUG
//				MessageBox.Show(e.ToString());
#endif	

				Trace.WriteLine(e.ToString());
				throw e;
			}
		}

		#endregion

		#region painting helpers

		internal Color GetTreeBackColor()
		{
			return Style.BackColor;
		}

		internal Color GetTreeBackFadeColor()
		{
			return Style.FadeColor;
		}

		private void PrepareNodePainting()
		{
			foreach (Node oNode in Nodes)
				oNode.ClearNodeOrder();

			int nOrder = 0;

			foreach (Node oNode in Nodes)
				oNode.RecalcNodeOrder(ref nOrder);
		}

		#endregion

		#region event handlers

		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
					// WM_CONTEXTMENU, just hide it, and do not process, we do this in mousedown
				case 0x007B:
					break;
				default:
					base.WndProc(ref m);
					break;
			}
		}

		#region keyboard events

		/// <summary>
		/// Processes the command keys
		/// </summary>
		/// <param name="msg">Message to process</param>
		/// <param name="keyData">Key data of the message</param>
		/// <returns>Status of the operation of the key was handled</returns>
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (m_textBox.Visible == true && m_textBox.Focused == true)
				return base.ProcessCmdKey(ref msg, keyData);
			;

			Refresh();

			// handle the Up key and the Down key
			if (keyData == Keys.Up || keyData == Keys.Down)
			{
				Node oSelNode = SelectedNode;

				if (oSelNode == null)
				{
					if (keyData == Keys.Up)
					{
						oSelNode = Nodes[Nodes.Count - 1];

						// if the node is opened, find the highest node order
						oSelNode = this.GetNodeByOrder(oSelNode.GetMaxSubNodeOrder());
					}
					else
						oSelNode = Nodes[0];

					oSelNode.Select(m_Control, m_Shift);

					oSelNode.Invalidate();

					return true;
				}

				int nOrder = oSelNode.NodeOrder;

				if (keyData == Keys.Up)
					nOrder -= 1;
				else
					nOrder += 1;

				// test the first node
				if (nOrder < 0)
					return true;

				Node oNewSelNode = this.GetNodeByOrder(nOrder);

				if (oNewSelNode == null)
					return true;

				oNewSelNode.Select(m_Control, m_Shift);

				oNewSelNode.Invalidate();

				return true;
			}

			// handle the the Left key
			if (keyData == Keys.Left)
			{
				Node oSelNode = SelectedNode;

				if (oSelNode == null)
					return true;

				if (oSelNode.IsExpanded == false || oSelNode.Nodes.Count == 0)
					return true;

				oSelNode.Collapse();

				oSelNode.Invalidate();

				return true;
			}

			// handle the the Left key
			if (keyData == Keys.Right)
			{
				Node oSelNode = SelectedNode;

				if (oSelNode == null)
					return true;

				if (oSelNode.IsExpanded == true || (oSelNode.Nodes.Count == 0 && oSelNode.Panel == null))
					return true;

				oSelNode.Expand();

				oSelNode.Invalidate();

				return true;
			}

			if (keyData == Keys.Space)
			{
				if (SelectedNode != null)
					SelectedNode.ToggleChecked();

				return true;
			}

			if (keyData == Keys.PageDown || keyData == Keys.PageUp)
			{
				// check the scrolling and update the scrolling
				if (this.Controls.Contains(m_oScrollBar) == false)
					return base.ProcessCmdKey(ref msg, keyData);

				if (keyData == Keys.PageDown)
					m_nScroll -= m_oScrollBar.LargeChange;
				else
					m_nScroll += m_oScrollBar.LargeChange;

				if (m_nScroll < -CalcItemHeight(Graphics.FromHwnd(this.Handle)) + this.Height)
					m_nScroll = -CalcItemHeight(Graphics.FromHwnd(this.Handle)) + this.Height;

				if (m_nScroll > 0)
					m_nScroll = 0;

				Invalidate();

				return true;
			}

			// inplace editing
			if (LabelEdit == true)
			{
				if (keyData == Keys.F2 && this.SelectedNode != null && m_bLabelEdit == true)
				{
					CancelEventArgs args = new CancelEventArgs(false);
					InvokeBeforeLabelEdit(this.SelectedNode, args);

					if (args.Cancel == true)
						return base.ProcessCmdKey(ref msg, keyData);
					;

					StartInplaceEdit(this.SelectedNode);

					return true;
				}
			}

			return false;
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);

			m_Control = e.Control;
			m_Shift = e.Shift;
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);

			// set the control flag
			m_Control = e.Control;
			m_Shift = e.Shift;

			// the copy to clipboard action
			if (AllowEditing == true)
			{
				if (e.Control == true && e.KeyCode == Keys.C)
				{
					if (this.SelectedNode != null)
						OnContextMenuEditingCopy(null, EventArgs.Empty);
				}
			}

			// the paste from clipboard action
			if (AllowEditing == true)
			{
				if (e.Control == true && e.KeyCode == Keys.V)
				{
					OnContextMenuEditingPaste(null, EventArgs.Empty);
				}
			}

			// delete the node
			if (AllowDeleting == true)
			{
				if (e.KeyCode == Keys.Delete)
				{
					if (this.SelectedNode != null)
						OnContextMenuEditingDeleteNode(null, EventArgs.Empty);
				}
			}

			// insert the node
			if (AllowAdding == true)
			{
				if (e.KeyCode == Keys.Insert)
				{
					OnContextMenuEditingAddNode(null, EventArgs.Empty);
				}
			}

			// the move left function
			if (AllowArranging == true)
			{
				if (e.Control == true && e.KeyCode == Keys.Left)
				{
					if (this.SelectedNode != null)
						this.SelectedNode.MoveLeft();
				}
			}

			// the move right function
			if (AllowArranging == true)
			{
				if (e.Control == true && e.KeyCode == Keys.Right)
				{
					if (this.SelectedNode != null)
						this.SelectedNode.MoveRight();
				}
			}

			// the move down function
			if (AllowArranging == true)
			{
				if (e.Control == true && e.KeyCode == Keys.Down)
				{
					if (this.SelectedNode != null)
						this.SelectedNode.MoveDown();
				}
			}

			// the move up function
			if (AllowArranging == true)
			{
				if (e.Control == true && e.KeyCode == Keys.Up)
				{
					if (this.SelectedNode != null)
						this.SelectedNode.MoveUp();
				}
			}

			// the move bottom function
			if (AllowArranging == true)
			{
				if (e.Control == true && e.KeyCode == Keys.PageDown)
				{
					if (this.SelectedNode != null)
						this.SelectedNode.MoveBottom();
				}
			}

			// the move top function
			if (AllowArranging == true)
			{
				if (e.Control == true && e.KeyCode == Keys.PageUp)
				{
					if (this.SelectedNode != null)
						this.SelectedNode.MoveTop();
				}
			}

			// scroll up function
			if (e.Alt == true && e.KeyCode == Keys.Up)
			{
				Invalidate();

				int nScrollIndex = -m_oScrollBar.Value;

				nScrollIndex += m_oScrollBar.SmallChange;

				if (nScrollIndex <= -(m_oScrollBar.Maximum - Height + m_oScrollBar.SmallChange))
				{
					nScrollIndex = -(m_oScrollBar.Maximum - Height + m_oScrollBar.SmallChange);
					return;
				}

				if (nScrollIndex > 6)
				{
					nScrollIndex = 6;
					return;
				}

				m_nScroll = nScrollIndex;

				Invalidate();
			}

			// scroll down function
			if (e.Alt == true && e.KeyCode == Keys.Down)
			{
				Invalidate();

				int nScrollIndex = -m_oScrollBar.Value;

				nScrollIndex -= m_oScrollBar.SmallChange;

				if (nScrollIndex <= -(m_oScrollBar.Maximum - Height + m_oScrollBar.SmallChange))
				{
					nScrollIndex = -(m_oScrollBar.Maximum - Height + m_oScrollBar.SmallChange);
					return;
				}

				if (nScrollIndex > 6)
				{
					nScrollIndex = 6;
					return;
				}

				m_nScroll = nScrollIndex;

				Invalidate();
			}

			// scroll left
			if (e.Alt == true && e.KeyCode == Keys.Left)
			{
				Invalidate();

				int nScrollIndex = -m_oHScrollBar.Value;

				nScrollIndex += m_oHScrollBar.SmallChange;

				if (nScrollIndex <= -(m_oHScrollBar.Maximum - Width + m_oHScrollBar.SmallChange))
				{
					nScrollIndex = -(m_oHScrollBar.Maximum - Width + m_oHScrollBar.SmallChange);
				}

				if (nScrollIndex > 0)
				{
					nScrollIndex = 0;
				}

				m_nHScroll = nScrollIndex;

				Invalidate();
			}

			// scroll right
			if (e.Alt == true && e.KeyCode == Keys.Right)
			{
				Invalidate();

				int nScrollIndex = -m_oHScrollBar.Value;

				nScrollIndex -= m_oHScrollBar.SmallChange;

				if (nScrollIndex <= -(m_oHScrollBar.Maximum - Width + m_oHScrollBar.SmallChange))
				{
					nScrollIndex = -(m_oHScrollBar.Maximum - Width + m_oHScrollBar.SmallChange);
				}

				if (nScrollIndex > 0)
				{
					nScrollIndex = 0;
				}

				m_nHScroll = nScrollIndex;

				Invalidate();
			}
		}

		#endregion

		#region inplace editing

		internal void OnInplaceEditLostFocus(object sender, EventArgs args)
		{
			this.EditingNode = null;

			m_textBox.LostFocus -= m_OnInplaceEditLostFocus;
			m_textBox.KeyPress -= m_OnInplaceEditKeyPress;

			m_textBox.Hide();

			if (this.SelectedNode != null)
			{
				if (sender is Boolean)
				{
					bool cancel = (bool) sender;

					if (cancel == false)
						this.SelectedNode.Text = m_textBox.Text;
				}
				else
					this.SelectedNode.Text = m_textBox.Text;
			}

			this.Focus();

			if (this.SelectedNode != null)
				InvokeAfterLabelEdit(this.SelectedNode);
		}

		private void OnInplaceEditKeyPress(object sender, KeyPressEventArgs args)
		{
			if (args.KeyChar == 13)
			{
				OnInplaceEditLostFocus(null, EventArgs.Empty);
				return;
			}

			if (args.KeyChar == 27)
			{
				if (this.EditingNode.InplaceEditAdded == true)
				{
					Node nextSelectNode = this.EditingNode.Parent;
					this.EditingNode.Remove();

					if (nextSelectNode != null)
						nextSelectNode.Select();
				}

				this.EditingNode = null;

				m_textBox.LostFocus -= m_OnInplaceEditLostFocus;
				m_textBox.KeyPress -= m_OnInplaceEditKeyPress;

				m_textBox.Hide();

				this.Focus();
			}
		}

		#endregion

		#region mouse events

		/// <summary>
		/// Onclick event handler
		/// </summary>
		/// <param name="e">Event to be handled</param>
		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);

			if (m_oButtonClicked == MouseButtons.Right)
				return;

			// check the place, get the object, its popup menu and show it
			Component oComponent = this.GetSubObjectAtPoint(m_oMouseClickPoint);

			#region test the node rects

			if (oComponent is Node)
			{
				Node oNode = oComponent as Node;

				InvokeNodeMouseClick(e, oNode);

				return;
			}

			#endregion

			// check box
			if (IsNodeCheckBox(m_oMouseClickPoint) == true)
			{
				Node oNode = GetNodeByItemCheckBox(GetNodeCheckBoxRect(m_oMouseClickPoint));

				if (oNode.Parent != null && oNode.Parent.SubNodesCheckExclusive == true)
				{
					if (oNode.Checked == false)
						oNode.ToggleChecked();
				}
				else
				{
					oNode.ToggleChecked();
				}
			}

			// flag object
			if (IsNodeFlag(m_oMouseClickPoint) == true)
			{
				Node oNode = GetNodeByItemFlag(GetNodeFlagRect(m_oMouseClickPoint));
				InvokeNodeFlagMouseClick(e, oNode);
			}

			#region collapse.expand box test

			if (this.Style.ShowPlusMinus == true)
			{
				foreach (Rectangle r in m_mapItemBoxToRect.Values)
				{
					if (r.Contains(m_oMouseClickPoint.X, m_oMouseClickPoint.Y) == false)
						continue;

					Node oNode = m_mapRectToItemBox[r] as Node;

					if (oNode.Nodes.Count > 0 || oNode.Panel != null)
					{
						if (oNode.IsExpanded == true)
							oNode.Collapse();
						else
							oNode.Expand();
					}

					// draw the node
					oNode.Invalidate();

					// clear the mouse point;
					m_oMouseClickPoint.X = -1;
					m_oMouseClickPoint.Y = -1;
					m_oButtonClicked = MouseButtons.None;

					return;
				}
			}

			#endregion									

			// clear the mouse point;
			m_oMouseClickPoint.X = -1;
			m_oMouseClickPoint.Y = -1;
			m_oButtonClicked = MouseButtons.None;

			// redraw the control
			Invalidate();
		}

		protected override void OnDoubleClick(EventArgs e)
		{
			base.OnDoubleClick(e);

			#region collapse expand test

			foreach (Rectangle r in m_mapRectToSubItem.Keys)
			{
				if (r.Contains(m_oMouseClickPoint.X, m_oMouseClickPoint.Y) == false)
					continue;

				Node oNode = m_mapRectToSubItem[r] as Node;

				// invoke the dbl click event
				InvokeNodeMouseDblClick(e, oNode);

				// test the default dblclick behavior setting and expand/collapse nodes if allowed
				if (this.ExpandOnDblClick == true)
				{
					if (oNode.IsExpanded == true && (oNode.Nodes.Count > 0 || oNode.Panel != null))
						oNode.Collapse();
					else if (oNode.IsExpanded == false && (oNode.Nodes.Count > 0 || oNode.Panel != null))
						oNode.Expand();
				}

				// draw the node
				oNode.Invalidate();

				// clear the mouse point;
				m_oMouseClickPoint.X = -1;
				m_oMouseClickPoint.Y = -1;
				m_oButtonClicked = MouseButtons.None;

				return;
			}

			#endregion

			// flag object
			if (IsNodeFlag(m_oMouseClickPoint) == true)
			{
				Node oNode = GetNodeByItemFlag(GetNodeFlagRect(m_oMouseClickPoint));
				InvokeNodeFlagMouseDblClick(e, oNode);
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (Focused == false)
				this.Focus();

			// clear the mouse data holders
			m_oMouseClickPoint.X = e.X;
			m_oMouseClickPoint.Y = e.Y;
			m_oButtonClicked = e.Button;

			// check if have not clicked the right corner when scrollbars are in place
			if (Controls.Contains(m_oHScrollBar) && Controls.Contains(m_oScrollBar))
			{
				if (m_oMouseClickPoint.X > m_oHScrollBar.Right && m_oMouseClickPoint.Y > m_oScrollBar.Bottom)
				{
					// redraw the control
					Invalidate();

					return;
				}
			}

			// check the place, get the object, its popup menu and show it
			Component oComponent = this.GetSubObjectAtPoint(m_oMouseClickPoint);

			if (oComponent is Node)
				InvokeNodeMouseDown(e, oComponent as Node);

			// flag object
			if (this.Flags == true && IsNodeFlag(m_oMouseClickPoint) == true)
			{
				Node oNode = GetNodeByItemFlag(GetNodeFlagRect(m_oMouseClickPoint));
				InvokeNodeFlagMouseDown(e, oNode);
			}

			#region node select

			if (oComponent is Node)
			{
				// get the node
				Node oNode = oComponent as Node;

				// make the node selected
				oNode.Select(m_Control, m_Shift);

				// set the focus node
				m_oFocusNode = oNode;

				// redraw the node
				oNode.Invalidate();
			}

			#endregion

			if (m_oButtonClicked == MouseButtons.Right)
			{
				if (oComponent is Node)
				{
					Node oNode = oComponent as Node;

					ContextMenu oMenu = oNode.GetContextMenu();

					if (oMenu == null && this.ContextMenuArranging == false && this.ContextMenuEditing == false
						&& this.ContextMenuXmlOperations == false)
						return;

					if (oMenu == null && (this.ContextMenuArranging == true || this.ContextMenuEditing == true
						|| this.ContextMenuXmlOperations == true))
                        oMenu = new ContextMenu();

                    ContextMenu contextMenu = new ContextMenu();
					try
					{
						m_OnPopupMenu.Invoke(oMenu, new object[] {EventArgs.Empty});
					}
					catch
					{
					}

                    contextMenu.MergeMenu(oMenu);

                    // check the associated menu switches, if we have several menu alterations specified, use them to add
					// proper menu items which are associated with menu commands that have predefined handlers
					ContextMenuExtension(contextMenu);

                    contextMenu.Show(this, m_oMouseClickPoint);
                    
					return;
				}

				this.SelectedNode = null;

				// context menu associated with the treeview and being called
                ContextMenu oContMenu = this.ContextMenu;

				if (oContMenu == null && (this.ContextMenuArranging == true || this.ContextMenuEditing == true
					|| this.ContextMenuXmlOperations == true))
                    oContMenu = new ContextMenu();

				if (oContMenu != null)
				{
                    ContextMenu contextMenu = new ContextMenu();
					try
					{
						m_OnPopupMenu.Invoke(oContMenu, new object[] {EventArgs.Empty});
					}
					catch
					{
					}

                    contextMenu.MergeMenu(oContMenu);

                    // check the associated menu switches, if we have several menu alterations specified, use them to add
					// proper menu items which are associated with menu commands that have predefined handlers
					ContextMenuExtensionTreeView(contextMenu);

                    contextMenu.Show(this, m_oMouseClickPoint);

					return;
				}
			}

			base.OnMouseDown(e);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			if (m_oDragDropPop != null)
			{
				m_oDragDropPop.Hide();
				m_oDragDropPop.Dispose();
			}

			// check the place, get the object, its popup menu and show it
			Component oComponent = this.GetSubObjectAtPoint(new Point(e.X, e.Y));

			if (oComponent is Node)
				InvokeNodeMouseUp(e, oComponent as Node);

			// flag object
			if (this.Flags == true && IsNodeFlag(new Point(e.X, e.Y)) == true)
			{
				Node oNode = GetNodeByItemFlag(GetNodeFlagRect(new Point(e.X, e.Y)));
				InvokeNodeFlagMouseUp(e, oNode);
			}

			#region droping at node

			Node oDropNode = oComponent as Node;
			string key = null;

			if (m_oDragNode != null)
				key = m_oDragNode.Key;

			if (oDropNode != null && oDropNode.AllowDrop == true)
			{
				// perform the drop of the node
				if (m_bDragDropNode == true && oDropNode != null && oDropNode != m_oDragNode)
				{
					Node oCopyNode = m_oDragNode;

					// check where we are dropping, we can test the TreeView's nodedrop variables							
					if (DragDropNode != null
						&& DragDropNode.NodeDropMode == NodeDropMode.DropUnder)
					{
						// drop at the node
						// add the node to the new parent holder							
						oCopyNode.TreeView = oDropNode.TreeView;

						oCopyNode.NodeMoving = true;

						if (oCopyNode.IsSomeParent(oDropNode) == false)
						{
							InvokeBeforeNodePositionChange(oCopyNode);
							m_oDragNode.Remove();
							oCopyNode.SetParent(oDropNode);
							oDropNode.Nodes.Insert(0, oCopyNode);
							InvokeAfterNodePositionChange(oCopyNode);

							oDropNode.IsExpanded = true;
						}

						oCopyNode.NodeMoving = false;

						foreach (Node oSubNode in oCopyNode.Nodes)
							oSubNode.m_nCollectionOrder = oSubNode.YOrder;
					}
					else if (DragDropNode != null
						&& DragDropNode.NodeDropMode == NodeDropMode.DropAfter)
					{
						// we are droping the node behind the drop node selected
						Node oNode = DragDropNode.DropNode;

						if (oNode.Collection.Count - 1 == oNode.Index)
						{
							// add the node to the new parent holder							
							oCopyNode.TreeView = oNode.TreeView;
							oCopyNode.NodeMoving = true;

							if (oCopyNode.IsSomeParent(oNode) == false)
							{
								InvokeBeforeNodePositionChange(oCopyNode);
								m_oDragNode.Remove();
								oCopyNode.SetParent(oNode.Parent);
								oNode.Collection.Add(oCopyNode);
								InvokeAfterNodePositionChange(oCopyNode);

								oDropNode.IsExpanded = true;
							}

							oCopyNode.NodeMoving = false;
						}
						else
						{
							// add the node to the new parent holder							
							oCopyNode.TreeView = oNode.TreeView;
							oCopyNode.NodeMoving = true;

							if (oCopyNode.IsSomeParent(oNode) == false)
							{
								InvokeBeforeNodePositionChange(oCopyNode);
								m_oDragNode.Remove();
								oCopyNode.SetParent(oNode.Parent);
								oNode.Collection.Insert(oNode.Index + 1, oCopyNode);
								InvokeAfterNodePositionChange(oCopyNode);

								oDropNode.IsExpanded = true;
							}

							oCopyNode.NodeMoving = false;
						}
					}
					else if (DragDropNode != null
						&& DragDropNode.NodeDropMode == NodeDropMode.DropInfront)
					{
						// we are droping the node infront the drop node selected
						Node oNode = DragDropNode.DropNode;

						// add the node to the new parent holder							
						oCopyNode.TreeView = oNode.TreeView;
						oCopyNode.NodeMoving = true;

						if (oCopyNode.IsSomeParent(oNode) == false)
						{
							InvokeBeforeNodePositionChange(oCopyNode);
							m_oDragNode.Remove();
							oCopyNode.SetParent(oNode.Parent);
							oNode.Collection.Insert(oNode.Index, oCopyNode);
							InvokeAfterNodePositionChange(oCopyNode);

							oDropNode.IsExpanded = true;
						}

						oCopyNode.NodeMoving = false;
					}

					foreach (Node oCollectionNode in oCopyNode.Collection)
						oCollectionNode.m_nCollectionOrder = oCollectionNode.Index;
				}
			}

			if (m_oDragNode != null)
			{
				ClearNodeKey(key);
				m_oDragNode.Key = key;
			}

			#endregion	

			m_oButtonClicked = MouseButtons.None;
			m_bDragDropNode = false;
			DragDropNode = null;
			m_oDragNode = null;

			Invalidate();
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

//			if (m_bMouseMoveLocked == true)
//				return;

			Point p = new Point(e.X, e.Y);
			Node oMouseMoveNode = GetSubObjectAtPoint(p) as Node;

			// get the mouse down info and the coordinates, if we are starting to move mouse over the clicked node
			// then start the drag drop operation
			if (AutoDragDrop == true)
			{
				if (m_oButtonClicked == MouseButtons.Left && m_oDragNode == null && oMouseMoveNode != null && m_bDragDropNode == false)
				{
					if (oMouseMoveNode.AllowDrag == true)
					{
						m_bDragDropNode = true;
						m_oDragNode = oMouseMoveNode;

						if (m_oDragNode != null)
						{
							m_oDragDropPop = new DragDropPopup(m_oDragNode);
							m_oDragDropPop.Show();

							this.Focus();
						}
					}
				}
			}

			#region dragdrop node drop test

			if (m_bDragDropNode == true && m_oDragDropPop != null)
			{
				m_oDragDropPop.CanDrop = false;
				m_oDragDropPop.Left = this.PointToScreen(p).X + 10;
				m_oDragDropPop.Top = this.PointToScreen(p).Y + 10;

				// get the drop object and check if we can drop it here
				// try to find the group or node object and set the object to the property window
				Node oCanDropNode = this.GetSubObjectAtPoint(p) as Node;

				if (oCanDropNode != null)
				{
					if (oCanDropNode.AllowDrop == true)
					{
						if (m_oDragNode != null && m_oDragNode.IsSomeParent(oCanDropNode) == false)
							m_oDragDropPop.CanDrop = true;
					}
					else
						m_oDragDropPop.CanDrop = false;
				}

				if (oCanDropNode != null && oCanDropNode.IsExpanded == false)
				{
					// start the expand timer 
					m_oTimerExpandNode = oCanDropNode;

					System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
					timer.Interval = 800;
					timer.Tick += new EventHandler(this.ExpandTimerTick);
					timer.Enabled = true;
				}

				m_oDragDropPop.Refresh();
			}

			#endregion

			#region dragdrop autoscroll

			if (Controls.Contains(m_oScrollBar) == true && m_oDragNode != null)
			{
				if (p.Y < 10 && m_bDragScrollDownRunning == false)
				{
					if (m_oDragScrollThread != null)
						try
						{
							m_oDragScrollThread.Abort();
						}
						catch
						{
						}

					m_oDragScrollThread = new Thread(new ThreadStart(this.OnDragScrollDown));
					m_oDragScrollThread.Start();
				}
				else if (p.Y > this.Height - 10 && m_bDragScrollUpRunning == false)
				{
					if (m_oDragScrollThread != null)
						try
						{
							m_oDragScrollThread.Abort();
						}
						catch
						{
						}

					m_oDragScrollThread = new Thread(new ThreadStart(this.OnDragScrollUp));
					m_oDragScrollThread.Start();
				}
				else
				{
					// if the mouse is outside the scrolling area and the scrolling thread is running stop the scrolling
					if (m_oDragScrollThread != null)
						try
						{
							m_oDragScrollThread.Abort();
						}
						catch
						{
						}

					m_bDragScrollDownRunning = false;
					m_bDragScrollUpRunning = false;
				}
			}

			#endregion

			#region Node dragdrop structure creation and dragdrop check

			if (AutoDragDrop == true)
			{
				if (oMouseMoveNode != null)
				{
					if (m_oDragNode != null && oMouseMoveNode != m_oDragNode)
					{
						// we are not dragging, just hightlight the node
						if (m_bDragDropNode == true)
						{
							// we are in the dragging mode, get the node's rectangle and do some more calculations and
							// operations here
							Rectangle oRect = GetNodeRect(oMouseMoveNode);

							// test the area where we are with the actual point of drag. 
							if (p.Y >= oRect.Top && p.Y < oRect.Top + ((float) oRect.Height / 2.0))
							{
								if (p.X > m_oMouseClickPoint.X + 3)
									// we are in the upper part of the node, but in the right side, drop under
									DragDropNode = new NodeDragDrop(m_oDragNode, oMouseMoveNode, NodeDropMode.DropUnder);
								else
									// we are in the upper part of the node
									DragDropNode = new NodeDragDrop(m_oDragNode, oMouseMoveNode, NodeDropMode.DropInfront);
							}
							else if (p.Y >= oRect.Top + ((float) oRect.Height / 2.0) && p.Y <= oRect.Bottom)
							{
								// we are in the lower part of the node
								if (p.X > m_oMouseClickPoint.X + 3)
									// test the X coordinate, if we are in the right from the pop, then behave in that way
									DragDropNode = new NodeDragDrop(m_oDragNode, oMouseMoveNode, NodeDropMode.DropUnder);
								else
									// test the X coordinate, if we are in the right from the pop, then behave in that way
									DragDropNode = new NodeDragDrop(m_oDragNode, oMouseMoveNode, NodeDropMode.DropAfter);
							}
						}
					}
				}
			}

			#endregion	

			HighlightedNode = null;

			#region search for the node, if node found, highlight it

			if (this.Style.TrackNodeHover == true)
			{
				foreach (Rectangle r in m_mapRectToSubItem.Keys)
				{
					if (r.Contains(e.X, e.Y) == false)
						continue;

					Node oNode = m_mapRectToSubItem[r] as Node;

					if (oNode == null)
					{
						base.OnMouseMove(e);

						return;
					}

					if (this.HighlightedNode == oNode)
					{
						base.OnMouseMove(e);

						InvokeNodeMouseMove(e, oNode);

						return;
					}

					this.HighlightedNode = oNode;

					Invalidate();

					if (this.MouseOverNode != null && this.MouseOverNode != oNode)
						InvokeNodeMouseLeave(MouseOverNode);

					if (this.MouseOverNode != oNode)
					{
						this.MouseOverNode = oNode;
						InvokeNodeMouseEnter(MouseOverNode);
					}

					base.OnMouseMove(e);

					InvokeNodeMouseMove(e, oNode);

					return;
				}
			}
			else
			{
				foreach (Rectangle r in m_mapRectToSubItem.Keys)
				{
					if (r.Contains(e.X, e.Y) == false)
						continue;

					Node oNode = m_mapRectToSubItem[r] as Node;

					if (oNode == null)
					{
						base.OnMouseMove(e);

						return;
					}

					if (this.MouseOverNode != null && this.MouseOverNode != oNode)
						InvokeNodeMouseLeave(MouseOverNode);

					if (this.MouseOverNode != oNode)
					{
						this.MouseOverNode = oNode;
						InvokeNodeMouseEnter(MouseOverNode);
					}

					base.OnMouseMove(e);

					InvokeNodeMouseMove(e, oNode);

					Invalidate();

					return;
				}
			}

			#endregion

			bool bInvalidate = false;

			HighlightedNode = null;

			if (MouseOverNode != null)
			{
				InvokeNodeMouseLeave(MouseOverNode);

				bInvalidate = true;
			}

			MouseOverNode = null;

			if (bInvalidate == true)
				Invalidate();
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);

			if (m_oDragDropPop != null)
			{
				m_oDragDropPop.Hide();
				m_oDragDropPop.Dispose();
			}

			ClearDesignHighlight();

			ClearHover();

			m_bFocus = false;

			if (MouseOverNode != null)
				InvokeNodeMouseLeave(MouseOverNode);

			MouseOverNode = null;
			HighlightedNode = null;
			m_oDragOverNode = null;
			m_bDragDropNode = false;
			m_oDragNode = null;
			DragDropNode = null;

			Invalidate();
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);

			m_bFocus = true;

			Invalidate();
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);

			Invalidate();
		}

		#endregion				

		#region drag drop events

		private void OnDragScrollDown()
		{
			while (true)
			{
				Invalidate();
				int nScrollIndex = -m_oScrollBar.Value;

				nScrollIndex += m_oScrollBar.SmallChange;

				if (nScrollIndex <= -(m_oScrollBar.Maximum - Height + m_oScrollBar.SmallChange))
				{
					nScrollIndex = -(m_oScrollBar.Maximum - Height + m_oScrollBar.SmallChange);
					return;
				}

				if (nScrollIndex > 6)
				{
					nScrollIndex = 6;
					return;
				}

				m_nScroll = nScrollIndex;
				Invalidate();

				Thread.Sleep(150);
			}
		}

		private void OnDragScrollUp()
		{
			while (true)
			{
				Invalidate();
				int nScrollIndex = -m_oScrollBar.Value;

				nScrollIndex -= m_oScrollBar.SmallChange;

				if (nScrollIndex <= -(m_oScrollBar.Maximum - Height + m_oScrollBar.SmallChange))
				{
					nScrollIndex = -(m_oScrollBar.Maximum - Height + m_oScrollBar.SmallChange);
					return;
				}

				if (nScrollIndex > 6)
				{
					nScrollIndex = 6;
					return;
				}

				m_nScroll = nScrollIndex;
				Invalidate();

				Thread.Sleep(150);
			}
		}

		protected override void OnDragDrop(DragEventArgs drgevent)
		{
			// check the place, get the object
			Component oComponent = this.GetSubObjectAtPoint(this.PointToClient(new Point(drgevent.X, drgevent.Y)));

			if (oComponent is Node)
				InvokeNodeDragDrop(drgevent, oComponent as Node);

			m_oDragOverNode = null;

			Invalidate();

			base.OnDragDrop(drgevent);
		}

		protected override void OnDragOver(DragEventArgs drgevent)
		{
			// check the place, get the object
			Point p = this.PointToClient(new Point(drgevent.X, drgevent.Y));
			Component oComponent = this.GetSubObjectAtPoint(p);

			if (oComponent is Node)
			{
				if (m_oDragOverNode != oComponent)
				{
					if (m_oDragOverNode != null)
						InvokeNodeDragLeave(drgevent, m_oDragOverNode);

					InvokeNodeDragEnter(drgevent, oComponent as Node);
				}

				InvokeNodeDragOver(drgevent, oComponent as Node);

				m_oDragOverNode = oComponent as Node;

				if (this.Style.TrackNodeHover == true)
					HighlightedNode = m_oDragOverNode;

				Invalidate();
			}

			if (oComponent == null)
			{
				if (m_oDragOverNode != null)
					InvokeNodeDragLeave(drgevent, m_oDragOverNode);

				m_oDragOverNode = null;
				HighlightedNode = null;
				Invalidate();
			}

			#region dragdrop autoscroll

			if (Controls.Contains(m_oScrollBar) == true)
			{
				if (p.Y < 10 && m_bDragScrollDownRunning == false)
				{
					if (m_oDragScrollThread != null)
						try
						{
							m_oDragScrollThread.Abort();
						}
						catch
						{
						}

					m_oDragScrollThread = new Thread(new ThreadStart(this.OnDragScrollDown));
					m_oDragScrollThread.Start();
				}
				else if (p.Y > this.Height - 10 && m_bDragScrollUpRunning == false)
				{
					if (m_oDragScrollThread != null)
						try
						{
							m_oDragScrollThread.Abort();
						}
						catch
						{
						}

					m_oDragScrollThread = new Thread(new ThreadStart(this.OnDragScrollUp));
					m_oDragScrollThread.Start();
				}
				else
				{
					// if the mouse is outside the scrolling area and the scrolling thread is running stop the scrolling
					if (m_oDragScrollThread != null)
						try
						{
							m_oDragScrollThread.Abort();
						}
						catch
						{
						}

					m_bDragScrollDownRunning = false;
					m_bDragScrollUpRunning = false;
				}
			}

			#endregion

			base.OnDragOver(drgevent);
		}

		protected override void OnDragEnter(DragEventArgs drgevent)
		{
			base.OnDragEnter(drgevent);
		}

		protected override void OnDragLeave(EventArgs e)
		{
			base.OnDragLeave(e);

			if (m_oDragOverNode != null)
				InvokeNodeDragLeave(null, m_oDragOverNode);

			m_oDragOverNode = null;
			MouseOverNode = null;
			HighlightedNode = null;

			Invalidate();
		}

		#endregion

		#region other event handlers

		protected override void OnParentBackColorChanged(EventArgs e)
		{
			this.BackColor = Parent.BackColor;
		}

		#endregion

		#region local event handlers

		private void ExpandTimerTick(object sender, EventArgs e)
		{
			System.Windows.Forms.Timer timer = sender as System.Windows.Forms.Timer;
			timer.Enabled = false;
			timer.Dispose();

			// get the current node over node
			Node mouseOverNode = this.GetSubObjectAtPoint(this.PointToClient(Cursor.Position)) as Node;

			if (mouseOverNode == null || m_oTimerExpandNode == null)
				return;

			if (mouseOverNode == m_oTimerExpandNode)
				m_oTimerExpandNode.IsExpanded = true;

			this.Invalidate();
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			if (e.Delta < 0)
				m_nScroll -= m_oScrollBar.SmallChange;
			else
				m_nScroll += m_oScrollBar.SmallChange;

			if (m_nScroll < -CalcItemHeight(Graphics.FromHwnd(this.Handle)) + this.Height)
				m_nScroll = -CalcItemHeight(Graphics.FromHwnd(this.Handle)) + this.Height;

			if (m_nScroll > 0)
				m_nScroll = 0;

			Invalidate();
		}

		private void OnScrollItems(object sender, ScrollEventArgs e)
		{
			if (m_textBox != null && m_textBox.Visible == true)
				OnInplaceEditLostFocus(null, EventArgs.Empty);

			m_nScroll = -e.NewValue;

			Invalidate();
		}

		private void OnScrollItemsHorizontal(object sender, ScrollEventArgs e)
		{
			if (m_textBox != null && m_textBox.Visible == true)
				OnInplaceEditLostFocus(null, EventArgs.Empty);

			m_nHScroll = -e.NewValue;

			Invalidate();
		}

		private void OnScrollMouseEnter(object sender, EventArgs e)
		{
			m_bFocus = true;

			Invalidate();
		}

		private void OnScrollMouseLeave(object sender, EventArgs e)
		{
			m_bFocus = false;

			Invalidate();
		}

		#endregion																		

		#region other events

		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);

			m_bUserFocus = true;

			Invalidate();
		}

		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);

			m_bUserFocus = false;

			Invalidate();
		}

		#endregion				

		#region local context menu event handlers

		private void OnContextMenuArrangingExpand(object sender, EventArgs args)
		{
			if (this.SelectedNode != null)
				this.SelectedNode.ExpandAll();
			else
				this.ExpandAll();
		}

		private void OnContextMenuArrangingCollapse(object sender, EventArgs args)
		{
			if (this.SelectedNode != null)
				this.SelectedNode.CollapseAll();
			else
				this.CollapseAll();
		}

		private void OnContextMenuArrangingMoveTop(object sender, EventArgs args)
		{
			this.SelectedNode.MoveTop();
		}

		private void OnContextMenuArrangingMoveBottom(object sender, EventArgs args)
		{
			this.SelectedNode.MoveBottom();
		}

		private void OnContextMenuArrangingMoveUp(object sender, EventArgs args)
		{
			this.SelectedNode.MoveUp();
		}

		private void OnContextMenuArrangingMoveDown(object sender, EventArgs args)
		{
			this.SelectedNode.MoveDown();
		}

		private void OnContextMenuArrangingMoveLeft(object sender, EventArgs args)
		{
			this.SelectedNode.MoveLeft();
		}

		private void OnContextMenuArrangingMoveRight(object sender, EventArgs args)
		{
			this.SelectedNode.MoveRight();
		}

		private void OnContextMenuEditingAddNode(object sender, EventArgs args)
		{
			if (this.SelectedNode != null)
			{
				Node subNode = this.SelectedNode.CreateSubNode();
				subNode._TreeView = this;
				subNode.Copy(this.SelectedNode);
				subNode.Text = "new node";
				subNode.InplaceEditAdded = true;

				this.SelectedNode.Expand();

				StartInplaceEdit(subNode);
			}
			else
			{
				Node subNode = new Node();
				subNode._TreeView = this;
				subNode.Text = "new node";
				subNode.ContextMenuSource = ContextMenuSource.Parent;
				subNode.InplaceEditAdded = true;

				this.Nodes.Add(subNode);

				StartInplaceEdit(subNode);
			}
		}

		private void OnContextMenuEditingDeleteNode(object sender, EventArgs args)
		{
			this.SelectedNode.Remove();
		}

		private void OnContextMenuEditingEditNode(object sender, EventArgs args)
		{
			try
			{
				this.SelectedNode.BeginEdit();
			}
			catch
			{
			}
		}

		private void OnContextMenuEditingCopy(object sender, EventArgs args)
		{
			m_ContextMenuEditingCopyNode = this.SelectedNode;
		}

		private void OnContextMenuEditingPaste(object sender, EventArgs args)
		{
			if (m_ContextMenuEditingCopyNode == null)
				return;

			if (this.SelectedNode != null)
			{
				this.SelectedNode.Nodes.Add(CloneNode(m_ContextMenuEditingCopyNode));
				this.SelectedNode.ExpandAll();
			}
			else
			{
				Node node = CloneNode(m_ContextMenuEditingCopyNode);
				node._TreeView = this;
				this.Nodes.Add(node);
				node.ExpandAll();
			}
		}

		private void OnContextMenuXmlOperationsSave(object sender, EventArgs args)
		{
			this.SaveXml();
		}

		private void OnContextMenuXmlOperationsLoad(object sender, EventArgs args)
		{
			this.LoadXml();
		}

		#endregion

		#endregion

		#region properties

		/// <summary>
		/// Specifies menu strings for extended context menu
		/// </summary>
		[Category("Behavior")]
		[Description("Specifies menu strings for extended context menu.")]
		public ContextMenuStrings ContextMenuStrings
		{
			get { return m_oContextMenuString; }

			set { m_oContextMenuString = value; }
		}

		/// <summary>
		/// Specifies if inplace edit mode is allowed
		/// </summary>
		[Category("Behavior")]
		[Description("Specifies if inplace edit mode is allowed.")]
        [DefaultValue(true)]
		public bool AllowEditing
		{
			get { return m_bAllowEditing; }

			set { m_bAllowEditing = value; }
		}

		/// <summary>
		/// Specifies if inplace node adding mode is allowed
		/// </summary>
		[Category("Behavior")]
		[Description("Specifies if inplace node adding mode is allowed.")]
        [DefaultValue(true)]
		public bool AllowAdding
		{
			get { return m_bAllowAdding; }

			set { m_bAllowAdding = value; }
		}

		/// <summary>
		/// Specifies if inplace node delete mode is allowed
		/// </summary>
		[Category("Behavior")]
		[Description("Specifies if inplace node delete mode is allowed.")]
        [DefaultValue(true)]
		public bool AllowDeleting
		{
			get { return m_bAllowDeleting; }

			set { m_bAllowDeleting = value; }
		}

		/// <summary>
		/// Specifies if inplace node arranging mode is allowed
		/// </summary>
		[Category("Behavior")]
		[Description("Specifies if inplace node arranging mode is allowed.")]
        [DefaultValue(true)]
		public bool AllowArranging
		{
			get { return m_bAllowArranging; }

			set { m_bAllowArranging = value; }
		}

		/// <summary>
        /// Defines the default behavior on dblclick event.
		/// </summary>
		[Category("Behavior")]
		[Description("Defines the default behavior on dblclick event.")]
        [DefaultValue(true)]
		public bool ExpandOnDblClick
		{
			get { return m_bExandOnDblClick; }

			set { m_bExandOnDblClick = value; }
		}

		/// <summary>
		/// Defines the interval of the node's tooltip
		/// </summary>
		[Category("Behavior")]
		[Description("Defines the interval of the node's tooltip.")]
        [DefaultValue(1500)]
		public int TooltipPopDelay
		{
			get { return m_TooltipPopDelay; }

			set
			{
				if (value < 1000)
					m_TooltipPopDelay = 1000;
				else
					m_TooltipPopDelay = value;
			}
		}

		/// <summary>
		/// Defines whether tooltips are used for truncated nodes
		/// </summary>
		[Category("Behavior")]
		[Description("Defines whether tooltips are used for truncated nodes.")]
        [DefaultValue(true)]
		public bool Tooltips
		{
			get { return m_Tooltips; }

			set { m_Tooltips = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether nodes are being drawn with selected background strape.
		/// </summary>
		[Category("Behavior")]
		[Description("Gets or sets a value indicating whether nodes are being drawn with selected background strape.")]
        [DefaultValue(false)]
		public bool HideSelection
		{
			get { return m_bHideSelection; }

			set
			{
				m_bHideSelection = value;

				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether nodes are being autonumbered.
		/// </summary>
		[Category("Behavior")]
		[Description("Gets or sets a value indicating whether nodes are being autonumbered.")]
        [DefaultValue(false)]
		public bool NodeAutoNumbering
		{
			get { return m_bNodeAutoNumbering; }

			set
			{
				m_bNodeAutoNumbering = value;

				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether flags are displayed next to the tree nodes in the tree view control.
		/// </summary>
		[Category("Behavior")]
		[Description("Gets or sets a value indicating whether flags are displayed next to the tree nodes in the tree view control.")]
        [DefaultValue(false)]
		public bool Flags
		{
			get { return m_bFlags; }

			set
			{
				m_bFlags = value;

				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether check boxes are displayed next to the tree nodes in the tree view control.
		/// </summary>
		[Category("Behavior")]
		[Description("Gets or sets a value indicating whether check boxes are displayed next to the tree nodes in the tree view control.")]
        [DefaultValue(false)]
		public bool CheckBoxes
		{
			get { return m_bCheckBoxes; }

			set
			{
				m_bCheckBoxes = value;

				Invalidate();
			}
		}

		/// <summary>
		/// The Multiline property defines how the long text nodes are displayed.
		/// </summary>
		[Category("Behavior")]
		[Description("The Multiline property defines how the long text nodes are displayed.")]
        [DefaultValue(true)]
		public bool Multiline
		{
			get { return m_bMultiline; }

			set
			{
				m_bMultiline = value;

				this.Refresh();

				if (m_bMultiline == true)
				{
					m_nHScroll = 0;
					this.Controls.Remove(m_oHScrollBar);

					this.Refresh();
				}
			}
		}

		/// <summary>
		/// Gets or sets the delimiter string that the tree node path uses.
		/// </summary>
		[Category("Behavior")]
		[Description("Character or string defining the separator for url-like path to Node, default is \\.")]
        [DefaultValue("\\")]
		public string PathSeparator
		{
			get
			{
				if (m_sPathSeparator == null || m_sPathSeparator.Length == 0)
					m_sPathSeparator = "\\";

				return m_sPathSeparator;
			}

			set
			{
//				UpdateNodeGroupTextSeparator(value);

				m_sPathSeparator = value;

				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the behaviour when F2 is being pressed
		/// </summary>
		[Category("Behavior")]
		[Description("Gets or sets the behaviour when F2 is being pressed.")]
        [DefaultValue(true)]
		public bool LabelEdit
		{
			get { return m_bLabelEdit; }

			set { m_bLabelEdit = value; }
		}

		/// <summary>
		/// Gets or sets the selection mode
		/// </summary>				
		[Category("Behavior")]
		[Description("Gets or sets the selection mode.")]
        [DefaultValue(SelectionMode.Single)]
		public SelectionMode SelectionMode
		{
			get { return m_oSelectionMode; }

			set { m_oSelectionMode = value; }
		}

		/// <summary>
		/// Gets or sets the focus node
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]		
		public Node FocusNode
		{
			get { return m_oFocusNode; }

			set
			{
				m_oFocusNode = value;

				this.Invalidate();
			}
		}

		/// <summary>
		/// Listr of selected nodes, allows adding of nodes to selection and removing as well.		
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]		
		public Node[] SelectedNodes
		{
			get
			{
				Node[] nodes = new Node[m_oSelectedNodes.Count];

				m_oSelectedNodes.CopyTo(nodes);

				return nodes;
			}
		}

		/// <summary>
		/// Gets or sets the selected node
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Node SelectedNode
		{
			get
			{
				if (m_oSelectedNode == null)
				{
					foreach (Node oNode in this.Nodes)
					{
						m_oSelectedNode = oNode.GetSelectedNode();

						if (m_oSelectedNode != null)
							return m_oSelectedNode;
					}
				}

				return m_oSelectedNode;
			}

			set
			{
				if (value == null)
				{
					ClearAllSelection();
					ClearNodeSelection();
					return;
				}

				if (value != null && value.NodeMoving == false)
				{
					this.ClearAllSelection();

					// make the node selected
					value.Select();

					// redraw the node
					value.Invalidate();
				}
			}
		}

		/// <summary>
		/// Gets the collection of Node objects assigned to the current tree node.
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		public NodeCollection Nodes
		{
			get { return m_aNodes; }
		}

		/// <summary>
		/// Style of the TreeView object
		/// </summary>
		[Category("Appearance")]
		[Description("The Style object defines the visual attributes for TreeView painting functions.")]
		public TreeViewStyle Style
		{
			get { return m_oStyle; }

			set
			{
				m_oStyle = value;
				this.Style.NodeStyle.TreeView = this;
				this.Style.NodeStyle.CheckBoxStyle.TreeView = this;

				Invalidate();
			}
		}

		/// <summary>
		/// Gets ImageList for all nodes in the TreeView
		/// </summary>
		[Category("Appearance")]
		[Description("Defines the image list for subsequent nodes.")]
        [DefaultValue(null)]
		public ImageList ImageList
		{
			get { return m_aImages; }

			set
			{
				m_aImages = value;

				if (CancelDraw == false)
					Invalidate();
			}
		}

		/// <summary>
		/// Specifies if the nodes are being sorted
		/// </summary>
		[Category("Behavior")]
		[Description("When True Node objects will be sorted (caseinsensitive) in their collections when being painted.")]
        [DefaultValue(false)]
		public bool Sorted
		{
			get { return m_bSorted; }

			set
			{
				m_bSorted = value;

				if (m_bSorted == true)
				{
					ArrayList aList = new ArrayList();

					foreach (Node oNode in Nodes)
						aList.Add(oNode);

					aList.Sort();

					NodeCollection oCollection = new NodeCollection(this);

					for (int nNode = 0; nNode < aList.Count; nNode ++)
					{
						Node oNode = aList[nNode] as Node;

						if (oNode == null)
							continue;

						CanFireNodeEvent = false;
						oNode.Remove();
						oCollection.Add(oNode);
						CanFireNodeEvent = true;
					}

					this.m_aNodes = oCollection;
				}

				Invalidate();
			}
		}

		/// <summary>
		/// Specifies if node context menu is being modified with new items for node editing
		/// </summary>
		[Category("Behavior")]
		[Description("Specifies if node context menu is being modified with new items for node editing.")]
        [DefaultValue(false)]
		public bool ContextMenuEditing
		{
			get { return m_ContextMenuEditing; }

			set { m_ContextMenuEditing = value; }
		}

		/// <summary>
		/// Specifies if node context menu is being modified with new items for node arranging
		/// </summary>
		[Category("Behavior")]
		[Description("Specifies if node context menu is being modified with new items for node arranging.")]
        [DefaultValue(false)]
		public bool ContextMenuArranging
		{
			get { return m_ContextMenuArranging; }

			set { m_ContextMenuArranging = value; }
		}

		/// <summary>
		/// Specifies if node context menu is being modified with new items for xml operations
		/// </summary>
		[Category("Behavior")]
		[Description("Specifies if node context menu is being modified with new items for xml operations.")]
        [DefaultValue(false)]
		public bool ContextMenuXmlOperations
		{
			get { return m_ContextMenuXmlOperations; }

			set { m_ContextMenuXmlOperations = value; }
		}

		/// <summary>
		/// Specifies if the default drag drop operations are available at runtime
		/// </summary>
		[Category("Behavior")]
		[Description("Specifies if the default drag drop operations are available at runtime.")]
        [DefaultValue(true)]
		public bool AutoDragDrop
		{
			get { return m_AutoDragDrop; }

			set { m_AutoDragDrop = value; }
		}

		/// <summary>
		/// Gets or sets backcolor
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		new public Color BackColor
		{
			get { return base.BackColor; }

			set { base.BackColor = value; }
		}        

        /// <summary>
        /// Context menu associated with the treeview
        /// </summary>
        [Browsable(true)]
        new public ContextMenu ContextMenu
        {
            get
            {
                return base.ContextMenu;
            }

            set
            {
                base.ContextMenu = value;
            }
        }

		#endregion												

		#region databound properties

		/// <summary>
		/// DataSource bound to the TreeView
		/// </summary>
		[Bindable(true)]
		[Category("Data")]
		[DefaultValue(null)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design")]
		[Description("DataSource bound to the TreeView")]
		public DataTable DataSource
		{
			get { return m_DataSource; }
			set
			{
				//If the it's a change of datasource 
				if (this.m_DataSource != null)
				{
					//unsubscribe events
					this.m_DataSource.RowDeleting -= new DataRowChangeEventHandler(value_RowDeleting);
					this.m_DataSource.RowChanged -= new DataRowChangeEventHandler(value_RowChanged);

				}
				if (value == null)
				{
					Clear();
					this.m_DataSource = null;
				}
				else
				{
					//subscribe to datatable events
					value.RowDeleting += new DataRowChangeEventHandler(value_RowDeleting);
					value.RowChanged += new DataRowChangeEventHandler(value_RowChanged);


					this.m_DataSource = value;
					LoadTree();
				}
			}
		}		

		/// <summary>
		/// Gets or sets a string that specifies the property (column name) of the data source whose contents you want to display.
		/// </summary>
		[Bindable(true)]
		[Category("Data")]
		[Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
		[RefreshProperties(RefreshProperties.All)]
        [DefaultValue(null)]
		public string DisplayMember
		{
			get { return m_DisplayMember; }
			set
			{
				if (m_DisplayMember != value)
				{
					m_DisplayMember = value;

					LoadTree();
				}
			}
		}		

		/// <summary>
		/// Gets or sets a string that specifies the property (column name) of the data source whose contents is used to specify an value (Id) for created nodes.
		/// </summary>
		[Bindable(true)]
		[Category("Data")]
		[Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
		[RefreshProperties(RefreshProperties.All)]
		[Description("Memder that defines what column has the value for the node")]
        [DefaultValue(null)]
		public string ValueMember
		{
			get { return m_ValueMember; }
			set
			{
				if (m_ValueMember != value)
				{
					m_ValueMember = value;
					LoadTree();
				}
			}
		}		

		/// <summary>
		/// Gets or sets a string that specifies the property (column name) of the data source whose contents is used to categorize created nodes into structure, the parent Id.
		/// </summary>
		[Bindable(true)]
		[Category("Data")]
		[Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
		[RefreshProperties(RefreshProperties.All)]
		[Description("Member defining which column has the structure parent information")]
        [DefaultValue(null)]
		public string ParentMember
		{
			get { return m_ParentMember; }
			set
			{
				if (m_ParentMember != value)
				{
					m_ParentMember = value;
					LoadTree();
				}
			}
		}		

		/// <summary>
		/// Root value for all binded nodes
		/// </summary>
		[Browsable(false)]
        [DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object RootParentValue
		{
			get { return m_RootParentValue; }
			set { m_RootParentValue = value; }
		}

		/// <summary>
		/// Selected value for the node
		/// </summary>
		[Browsable(false)]
		[DefaultValue(null)]
		public object SelectedValue
		{
			get
			{
				if (this.SelectedNode != null)
				{
					return (this.SelectedNode).Value;
				}
				else
				{
					return null;
				}
			}

			set
			{
				if (m_NodesValueMember != null && value != null)
				{
					this.SelectedNode = (Node) m_NodesValueMember[value];
				}
			}
		}

		#endregion
	}
}