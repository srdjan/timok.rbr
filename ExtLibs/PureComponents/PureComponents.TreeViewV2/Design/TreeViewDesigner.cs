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
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace PureComponents.TreeView.Design
{
	/// <summary>
	/// 
	/// </summary>	
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	internal class TreeViewDesigner : System.Windows.Forms.Design.ParentControlDesigner
	{
		#region private members - WM_ definitions

		private const int WM_MOUSEMOVE = 0x0200;
		private const int WM_MOUSELEAVE = 0x02A3;
		private const int WM_MOUSEWHEEL = 0x020A;
		private const int WM_LBUTTONDOWN = 0x0201;
		private const int WM_LBUTTONUP = 0x0202;
		private const int WM_LBUTTONDBLCLK = 0x0203;
		private const int MK_SHIFT = 0x0004;
		private const int MK_CONTROL = 0x0008;
		private const int MK_LBUTTON = 0x0001;
		private const int WM_SIZE = 0x0005;
		private const int WM_MOVE = 0x0003;
		private const int WM_PAINT = 0x000F;

		#endregion

		#region private members

		private static bool MenuAdded = false;

		/// <summary>
		/// The designed TreeView object holder
		/// </summary>
		private TreeView m_oTreeView = null;

		private TreeViewSelector m_oSelector = new TreeViewSelector();

		/// <summary>
		/// The VS.NET property window holder
		/// </summary>
		private ISelectionService m_oSelectionService;

		/// <summary>
		/// The VS.NET Serialization service
		/// </summary>
		private IDesignerSerializationService m_oDesignerSerializationService;

		/// <summary>
		/// The VS.NET Toolbox service
		/// </summary>
		private IToolboxService m_oToolboxService;

		/// <summary>
		/// The UI service
		/// </summary>
		private IUIService m_oUIService;

		/// <summary>
		/// The VS.NET changed service
		/// </summary>
		private IComponentChangeService m_oComponentChangeService;

		private ComponentEventHandler m_oComponentAddedHandler;
		private ComponentEventHandler m_oComponentRemovingHandler;
		private EventHandler m_oNodeParentChanged;
		private bool m_bCanRemoveGrupNode = true;
		private bool m_bSkipSelectionChange = false;

		/// <summary>
		/// The VS.NET menu service
		/// </summary>
		private IMenuCommandService m_oMenuService;

		private Point m_oContextMenuPoint;

		private ActionMenuNative m_oActionMenuNode;
		private ActionMenuNative m_oActionMenuTreeView;

		private MenuCommand m_oOldCmdCopy;
		private MenuCommand m_oNewCmdCopy;
		private MenuCommand m_oOldCmdPaste;
		private MenuCommand m_oNewCmdPaste;
		private MenuCommand m_oOldCmdCut;
		private MenuCommand m_oOldBringFront;
		private MenuCommand m_oOldSendBack;
		private MenuCommand m_oOldAlignGrid;
		private MenuCommand m_oOldLockControls;
		private MenuCommand m_oOldDelete;

		/// <summary>
		/// The VS.NET designer
		/// </summary>
		private IDesignerHost m_oDesignerHost;

		/// <summary>
		/// The clipboard support holder
		/// </summary>
		internal static DataObject DataObject = null;

		/// <summary>
		/// ArrayList of node components to remove when adding to new form
		/// </summary>
		internal ArrayList NodeComponentToDestroy = new ArrayList();

		/// <summary>
		/// The designer verbs collection
		/// </summary>
		private DesignerVerbCollection m_aVerbs = new DesignerVerbCollection();

		private DesignerVerb m_oVerbAddNode;
		private DesignerVerb m_oVerbDeleteNode;
		private DesignerVerb m_oVerbColorScheme;
		private DesignerVerb m_oVerbNodeColorScheme;

		/// <summary>
		/// The color picker form
		/// </summary>
		private ColorSchemePickerForm m_oColorPickerForm = null;

		/// <summary>
		/// The node selected
		/// </summary>		
		private Node m_oSelectedNode = null;

		private Node m_oHighlightedNode = null;

		/// <summary>
		/// Drag drop support
		/// </summary>
		private bool m_bIsDragging = false;

		private bool m_bMouseDown = false;
		private Point m_oMouseDown = Point.Empty;
		private Node m_oDragNode = null;
		private Node m_oTimerExpandNode = null;
		private DragDropPopup m_oDragDropPop;
		private System.Threading.Thread m_oDragScrollThread;

		/// <summary>
		/// The first time flag
		/// </summary>
		private bool m_bFirstTime = true;

		/// <summary>
		/// The last cursor holder
		/// </summary>
		private Cursor m_oLastCursor = System.Windows.Forms.Cursors.Arrow;

		private const string REGKEY = "Software\\PureComponents\\Components\\TreeViewV2\\Designer";

		/// <summary>
		/// Tooltip
		/// </summary>
		private NodeTooltipWnd m_Tooltip = new NodeTooltipWnd();

		internal Node TooltipNode = null;
		private System.Timers.Timer m_TooltipTimer = null;

		private InplaceEditForm m_InplaceEdit;

		#endregion		

		#region construction

		/// <summary>
		/// The construction
		/// </summary>
		public TreeViewDesigner()
		{
			m_oVerbAddNode = new DesignerVerb("Add node", new EventHandler(this.OnAddNode));
			m_oVerbDeleteNode = new DesignerVerb("Delete node", new EventHandler(this.OnDeleteNode));
			m_oVerbColorScheme = new DesignerVerb("TreeView color scheme", new EventHandler(this.OnColorScheme));
			m_oVerbNodeColorScheme = new DesignerVerb("Apply color scheme", new EventHandler(this.OnApplyColorScheme));

			m_aVerbs.Add(m_oVerbAddNode);
			m_aVerbs.Add(m_oVerbDeleteNode);
			m_aVerbs.Add(m_oVerbColorScheme);
		}

		#endregion

		#region overriden properties

		/// <summary>
		/// Returns the list of objects associated
		/// </summary>
		public override System.Collections.ICollection AssociatedComponents
		{
			get
			{
				System.Collections.ArrayList aComponents = new System.Collections.ArrayList();

				foreach (Node oNode in m_oTreeView.Nodes)
				{
					aComponents.Add(oNode);
				}

				return aComponents;
			}
		}

		#endregion

		#region overriden functions

		/// <summary>
		/// We override Dispose here.  This designer listens to some events from outside services
		/// and we must disconnect these event listeners when we're disposed.  Otherwise, we might
		/// never GC, and worse, our designer may receive events after it has been destroyed.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				try
				{
					m_oMenuService.RemoveCommand(m_oNewCmdCopy);
				}
				catch
				{
				}
				try
				{
					m_oMenuService.RemoveCommand(m_oNewCmdPaste);
				}
				catch
				{
				}
				try
				{
					m_oMenuService.AddCommand(m_oOldCmdCopy);
				}
				catch
				{
				}
				try
				{
					m_oMenuService.AddCommand(m_oOldCmdPaste);
				}
				catch
				{
				}

				m_oComponentChangeService.ComponentAdded -= m_oComponentAddedHandler;
				m_oComponentChangeService.ComponentRemoving -= m_oComponentRemovingHandler;

				if (m_Tooltip != null)
				{
					m_Tooltip.Hide();
					m_Tooltip.DestroyHandle();
				}
			}

			base.Dispose(disposing);
		}

		/// <summary>
		/// Called by the framework when the designer is being initialized with the designed control
		/// </summary>
		/// <param name="component"></param>
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);

			if (Control is TreeView)
			{
				try
				{
					m_oSelectionService = (ISelectionService) GetService(typeof (ISelectionService));
				}
				catch
				{
				}
				try
				{
					m_oSelectionService.SelectionChanged += new EventHandler(this.OnSelectionServiceChanged);
				}
				catch
				{
				}
				try
				{
					m_oDesignerHost = (IDesignerHost) GetService(typeof (IDesignerHost));
				}
				catch
				{
				}
				try
				{
					m_oMenuService = (IMenuCommandService) GetService(typeof (IMenuCommandService));
				}
				catch
				{
				}
				try
				{
					m_oDesignerSerializationService = (IDesignerSerializationService) GetService(typeof (IDesignerSerializationService));
				}
				catch
				{
				}
				try
				{
					m_oToolboxService = (IToolboxService) GetService(typeof (IToolboxService));
				}
				catch
				{
				}
				try
				{
					m_oUIService = (IUIService) GetService(typeof (IUIService));
				}
				catch
				{
				}
				try
				{
					m_oComponentChangeService = (IComponentChangeService) GetService(typeof (IComponentChangeService));
				}
				catch
				{
				}

				m_oTreeView = (TreeView) Control;
				m_oTreeView.m_bFocus = true;
				m_oTreeView.ClearAllSelection();
				m_oTreeView.DesignerHost = m_oDesignerHost;
				m_oTreeView.IsDesignMode = true;

				if (m_bFirstTime == true)
				{
					OnComponentCreated(m_oTreeView);
					m_bFirstTime = false;
				}

				try
				{
					m_oComponentAddedHandler = new ComponentEventHandler(this.OnComponentAdded);
				}
				catch
				{
				}
				try
				{
					m_oComponentRemovingHandler = new ComponentEventHandler(this.OnComponentRemoving);
				}
				catch
				{
				}
				try
				{
					m_oComponentChangeService.ComponentAdded += m_oComponentAddedHandler;
				}
				catch
				{
				}
				try
				{
					m_oComponentChangeService.ComponentRemoving += m_oComponentRemovingHandler;
				}
				catch
				{
				}
				try
				{
					m_oNodeParentChanged = new EventHandler(this.OnNodeParentChanged);
				}
				catch
				{
				}

				try
				{
					m_oOldCmdCopy = m_oMenuService.FindCommand(StandardCommands.Copy);
				}
				catch
				{
				}
				try
				{
					m_oOldCmdPaste = m_oMenuService.FindCommand(StandardCommands.Paste);
				}
				catch
				{
				}
				try
				{
					m_oOldCmdCut = m_oMenuService.FindCommand(StandardCommands.Cut);
				}
				catch
				{
				}
				try
				{
					m_oOldBringFront = m_oMenuService.FindCommand(StandardCommands.BringToFront);
				}
				catch
				{
				}
				try
				{
					m_oOldSendBack = m_oMenuService.FindCommand(StandardCommands.SendToBack);
				}
				catch
				{
				}
				try
				{
					m_oOldAlignGrid = m_oMenuService.FindCommand(StandardCommands.AlignToGrid);
				}
				catch
				{
				}
				try
				{
					m_oOldLockControls = m_oMenuService.FindCommand(StandardCommands.LockControls);
				}
				catch
				{
				}
				try
				{
					m_oOldDelete = m_oMenuService.FindCommand(StandardCommands.Delete);
				}
				catch
				{
				}

				try
				{
					m_oNewCmdCopy = new MenuCommand(new EventHandler(this.OnMenuCopy), StandardCommands.Copy);
				}
				catch
				{
				}
				try
				{
					m_oNewCmdPaste = new MenuCommand(new EventHandler(this.OnMenuPaste), StandardCommands.Paste);
				}
				catch
				{
				}

				if (TreeViewDesigner.MenuAdded == false)
				{
					try
					{
						m_oMenuService.RemoveCommand(m_oOldCmdCopy);
					}
					catch
					{
					}
					try
					{
						m_oMenuService.RemoveCommand(m_oOldCmdPaste);
					}
					catch
					{
					}
					try
					{
						m_oMenuService.AddCommand(m_oNewCmdCopy);
					}
					catch
					{
					}
					try
					{
						m_oMenuService.AddCommand(m_oNewCmdPaste);
					}
					catch
					{
					}

					TreeViewDesigner.MenuAdded = true;
				}

				m_oTreeView.Invalidate();

				#region action menus

				#region node menu

				m_oActionMenuNode = new ActionMenuNative();
				m_oActionMenuNode.Width = 170;
				m_oActionMenuNode.Title = "Node Action Menu";

				ActionMenuGroup oMenuGroup = m_oActionMenuNode.AddMenuGroup("Editing");
				oMenuGroup.Expanded = true;
				m_oActionMenuNode.AddMenuItem(oMenuGroup, "Add Node");
				m_oActionMenuNode.AddMenuItem(oMenuGroup, "Delete Node");
				m_oActionMenuNode.AddMenuItem(oMenuGroup, "Add Panel");
				m_oActionMenuNode.AddMenuItem(oMenuGroup, "-");
				m_oActionMenuNode.AddMenuItem(oMenuGroup, "Clear Content");
				m_oActionMenuNode.AddMenuItem(oMenuGroup, "Delete TreeView");
				m_oActionMenuNode.AddMenuItem(oMenuGroup, "-");
				m_oActionMenuNode.AddMenuItem(oMenuGroup, "Copy");
				m_oActionMenuNode.AddMenuItem(oMenuGroup, "Paste");
				m_oActionMenuNode.AddMenuItem(oMenuGroup, "-");
				m_oActionMenuNode.AddMenuItem(oMenuGroup, "Properties");
				oMenuGroup = m_oActionMenuNode.AddMenuGroup("Arranging");
				m_oActionMenuNode.AddMenuItem(oMenuGroup, "Expand");
				m_oActionMenuNode.AddMenuItem(oMenuGroup, "Collapse");
				m_oActionMenuNode.AddMenuItem(oMenuGroup, "Move Top");
				m_oActionMenuNode.AddMenuItem(oMenuGroup, "Move Bottom");
				m_oActionMenuNode.AddMenuItem(oMenuGroup, "Move Up");
				m_oActionMenuNode.AddMenuItem(oMenuGroup, "Move Down");
				m_oActionMenuNode.AddMenuItem(oMenuGroup, "Move Left");
				m_oActionMenuNode.AddMenuItem(oMenuGroup, "Move Right");
				oMenuGroup = m_oActionMenuNode.AddMenuGroup("Color Schemes");
				m_oActionMenuNode.AddMenuItem(oMenuGroup, "Default");
				m_oActionMenuNode.AddMenuItem(oMenuGroup, "Forest");
				m_oActionMenuNode.AddMenuItem(oMenuGroup, "Gold");
				m_oActionMenuNode.AddMenuItem(oMenuGroup, "Ocean");
				m_oActionMenuNode.AddMenuItem(oMenuGroup, "Rose");
				m_oActionMenuNode.AddMenuItem(oMenuGroup, "Silver");
				m_oActionMenuNode.AddMenuItem(oMenuGroup, "Sky");
				m_oActionMenuNode.AddMenuItem(oMenuGroup, "Sunset");
				m_oActionMenuNode.AddMenuItem(oMenuGroup, "Wood");

				m_oActionMenuNode.ItemClick += new ActionMenuNative.ItemClickEventHandler(this.OnActionMenuNodeItemClicked);

				#endregion			

				#region TreeView menu

				m_oActionMenuTreeView = new ActionMenuNative();
				m_oActionMenuTreeView.Width = 170;
				m_oActionMenuTreeView.Title = "TreeView Action Menu";

				oMenuGroup = m_oActionMenuTreeView.AddMenuGroup("Editing");
				oMenuGroup.Expanded = true;
				m_oActionMenuTreeView.AddMenuItem(oMenuGroup, "Add Node");
				m_oActionMenuTreeView.AddMenuItem(oMenuGroup, "Color Scheme Picker...");
				m_oActionMenuTreeView.AddMenuItem(oMenuGroup, "-");
				m_oActionMenuTreeView.AddMenuItem(oMenuGroup, "Clear Content");
				m_oActionMenuTreeView.AddMenuItem(oMenuGroup, "Delete TreeView");
				m_oActionMenuTreeView.AddMenuItem(oMenuGroup, "-");
				m_oActionMenuTreeView.AddMenuItem(oMenuGroup, "Copy");
				m_oActionMenuTreeView.AddMenuItem(oMenuGroup, "Paste");
				m_oActionMenuTreeView.AddMenuItem(oMenuGroup, "-");
				m_oActionMenuTreeView.AddMenuItem(oMenuGroup, "Properties");				
				oMenuGroup = m_oActionMenuTreeView.AddMenuGroup("Arranging");
				m_oActionMenuTreeView.AddMenuItem(oMenuGroup, "Expand All");
				m_oActionMenuTreeView.AddMenuItem(oMenuGroup, "Collapse All");
				oMenuGroup = m_oActionMenuTreeView.AddMenuGroup("Layout");
				m_oActionMenuTreeView.AddMenuItem(oMenuGroup, "Bring to Front");
				m_oActionMenuTreeView.AddMenuItem(oMenuGroup, "Send to Back");
				m_oActionMenuTreeView.AddMenuItem(oMenuGroup, "Align to Grid");
				m_oActionMenuTreeView.AddMenuItem(oMenuGroup, "Lock Controls");

				m_oActionMenuTreeView.ItemClick += new ActionMenuNative.ItemClickEventHandler(this.OnActionMenuTreeViewItemClicked);

				#endregion

				#endregion

				// enable the drag drop operations
				m_oTreeView.AllowDrop = true;
				this.EnableDragDrop(true);

				m_oTreeView.CollapseAll();
				m_oSelector.SelectionService = m_oSelectionService;
				m_oSelector.TreeView = m_oTreeView;
			}
		}

		/// <summary>
		/// Called by the framework when the component is being pasted from the clipboard
		/// </summary>		
		public override void InitializeNonDefault()
		{
			try
			{
				TreeView oTreeView = m_oTreeView;

				if ((TreeViewDesigner.DataObject != null &&
					TreeViewDesigner.DataObject.GetDataPresent("PureComponents.TreeView.Design.TreeViewData"))
					|| Clipboard.GetDataObject().GetDataPresent(DataFormats.Serializable))
				{
					base.InitializeNonDefault();

					ICollection objects;

					if (TreeViewDesigner.DataObject != null &&
						TreeViewDesigner.DataObject.GetDataPresent("PureComponents.TreeView.Design.TreeViewData"))
					{
						objects = m_oDesignerSerializationService.Deserialize(
							TreeViewDesigner.DataObject.GetData("PureComponents.TreeView.Design.TreeViewData"));
					}
					else
					{
						objects = m_oDesignerSerializationService.Deserialize(
							Clipboard.GetDataObject().GetData("PureComponents.TreeView.Design.TreeViewData"));
					}

					TreeView oBaseTreeView = null;

					foreach (TreeView oNav in objects)
					{
						oBaseTreeView = oNav;
						break;
					}

					oTreeView.Copy(oBaseTreeView);

					foreach (Node node in oTreeView.Nodes.ToNodeArray())
						NodeComponentToDestroy.Add(node);

					foreach (Node oNode in oBaseTreeView.Nodes)
					{
						Node node = CloneNode(oNode);
						oTreeView.Nodes.Add(node);
					}

					// add the TreeView to the form				
					Control oBaseControl = m_oDesignerHost.RootComponent as Control;
					oBaseControl.Controls.Add(oTreeView);

					// make the new component selected
					m_bSkipSelectionChange = true;
					m_oSelectionService.SetSelectedComponents(new TreeView[] {oTreeView}, SelectionTypes.Replace);
					m_bSkipSelectionChange = false;

					oTreeView.Invalidate();

					return;
				}

				foreach (Node oNode in oTreeView.Nodes.ToNodeArray())
				{
					Node node = CloneNode(oNode);
					oTreeView.Nodes.Add(node);

					NodeComponentToDestroy.Add(oNode);
				}

				m_oTreeView.Invalidate();
			}
			catch (Exception e)
			{
				System.Console.WriteLine(e.ToString());
			}
		}

		/// <summary>
		/// Called by the framework when the context menu is being shown
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		protected override void OnContextMenu(int x, int y)
		{
			try
			{
				m_oTreeView.DragDropNode = null;
				m_bMouseDown = false;
				m_bIsDragging = false;
				m_oDragNode = null;

				if (m_oDragDropPop != null)
				{
					m_oDragDropPop.Close();
					m_oDragDropPop.Dispose();
					m_oDragDropPop = null;
				}

				m_oTreeView.ClearDesignHighlight();
				m_oTreeView.Invalidate();

				Component oComponent = m_oTreeView.GetSubObjectAtPoint(m_oTreeView.PointToClient(new Point(x, y)));
				m_oContextMenuPoint = new Point(x, y);

				if (oComponent is Node)
				{
					m_oTreeView.ClearNodeSelection();
					m_oTreeView.ClearDesignSelection();
					m_oTreeView.ClearDesignHighlight();

					Node oNode = oComponent as Node;
					oNode.TreeView = m_oTreeView;
					oNode.DesignSelected = true;
					oNode.Select();

					m_oSelectedNode = oNode;

					m_bSkipSelectionChange = true;
					m_oSelectionService.SetSelectedComponents(new Component[] {oComponent}, SelectionTypes.Replace);
					m_bSkipSelectionChange = false;

					m_oTreeView.ClearDesignHighlight();
					m_oTreeView.Invalidate();

					m_oActionMenuTreeView.Hide();
					m_oActionMenuNode.Show(x - 21, y);

					return;
				}

				m_oTreeView.ClearNodeSelection();
				m_oTreeView.ClearDesignSelection();
				m_oTreeView.ClearDesignHighlight();

				m_oSelectedNode = null;

				m_bSkipSelectionChange = true;
				m_oSelectionService.SetSelectedComponents(new Component[] {m_oTreeView}, SelectionTypes.Replace);
				m_bSkipSelectionChange = false;

				m_oTreeView.ClearDesignHighlight();
				m_oTreeView.Invalidate();

				m_oActionMenuNode.Hide();
				m_oActionMenuTreeView.Show(x - 21, y);
			}
			catch (Exception ex)
			{
				System.Console.Out.WriteLine(ex.ToString());
			}
		}

		protected override void PreFilterProperties(System.Collections.IDictionary properties)
		{
			string[] propsToRemove =
				{
					"BackgroundImage",
					"Cursor",
					"Font",
					"ForeColor",
					"RightToLeft",
					"Text",
					"ImeMode",
					"CausesValidation"
				};

			//Remove all properties that have to do with layout
			foreach (string prop in propsToRemove)
			{
				if (properties.Contains(prop))
					properties.Remove(prop);
			}

			base.PreFilterProperties(properties);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="control"></param>
		/// <returns></returns>
		public override bool CanParent(Control control)
		{
			if (control is NodePanel)
				return true;

			return false;
		}

		#endregion

		#region WndProc

		/// <summary>
		/// Internal wnd proc fnc
		/// </summary>
		/// <param name="m"></param>
		internal void InvokeWndProc(ref Message m)
		{
			WndProc(ref m);
		}

		/// <summary>
		/// Overrides the default message/event handler to perform our design time magic editing
		/// </summary>
		/// <param name="m"></param>
		protected override void WndProc(ref Message m)
		{
			// if the message is not or TreeView, just skip it
			if (m.HWnd != m_oTreeView.Handle)
			{
				// but if it is a TreeView's scrollbar, let the control handle the event
				if (m.HWnd == m_oTreeView.m_oScrollBar.Handle)
				{
					DefWndProc(ref m);

					return;
				}

				base.WndProc(ref m);

				return;
			}

			try
			{
				Point p = new Point(m.LParam.ToInt32());
				bool bControl = ((m.WParam.ToInt32() & MK_CONTROL) != 0);
				bool bShift = ((m.WParam.ToInt32() & MK_SHIFT) != 0);
				bool lButton = ((m.WParam.ToInt32() & MK_LBUTTON) != 0);

				if (m.Msg == WM_MOUSEWHEEL)
				{
					base.WndProc(ref m);
				}

				switch (m.Msg)
				{
					case WM_MOVE:
					case WM_SIZE:
						base.WndProc(ref m);

						break;

					case WM_LBUTTONDBLCLK:
						if (m_oSelectedNode == null)
							break;

						if (m_oSelectedNode.IsExpanded == true)
						{
							// inplace editing
							m_InplaceEdit = new InplaceEditForm(m_oSelectedNode, this);

							Rectangle nodeRect = m_oTreeView.GetNodeRect(m_oSelectedNode);
							m_InplaceEdit.BackColor = m_oSelectedNode.GetSelectedBackColor();
							m_InplaceEdit.Location = m_oTreeView.Parent.PointToScreen(m_oTreeView.Location);
							m_InplaceEdit.Left += nodeRect.Left + 1;
							m_InplaceEdit.Top += nodeRect.Top - 3;
							m_InplaceEdit.Width = nodeRect.Width + 2;
							m_InplaceEdit.Height = nodeRect.Height + 3;

							m_InplaceEdit.Show();
							m_InplaceEdit.FocusTextBox();

							break;
						}

						m_oSelectedNode.IsExpanded = !m_oSelectedNode.IsExpanded;
						break;

					case WM_LBUTTONDOWN:
						Cursor.Current = System.Windows.Forms.Cursors.Arrow;

						#region Lbutton down

						m_bMouseDown = true;
						m_oMouseDown = p;

						m_oTreeView.ClearDesignHighlight();
						m_oSelectedNode = null;

						// check if have not clicked the right corner when scrollbars are in place
						if (m_oTreeView.Controls.Contains(m_oTreeView.m_oHScrollBar)
							&& m_oTreeView.Controls.Contains(m_oTreeView.m_oScrollBar))
						{
							if (p.X > m_oTreeView.m_oHScrollBar.Right && p.Y > m_oTreeView.m_oScrollBar.Bottom)
							{
								m_oTreeView.ClearAllSelection();
								m_oTreeView.ClearDesignSelection();
								m_oTreeView.ClearDesignHighlight();

								m_bSkipSelectionChange = true;
								m_oSelectionService.SetSelectedComponents(new Component[] {m_oTreeView}, SelectionTypes.Replace);
								m_bSkipSelectionChange = false;

								break;
							}
						}

						// try to find the group or node object and set the object to the property window
						Component oComponent = m_oTreeView.GetSubObjectAtPoint(p);

						if (oComponent != null)
						{
							if (oComponent is Node)
							{
								Node oNode = oComponent as Node;

								m_oTreeView.ClearDesignSelection();

								if (bControl == false && bShift == false)
								{
									m_oTreeView.ClearDesignSelection();
									m_oTreeView.ClearDesignHighlight();
									m_oTreeView.ClearAllSelection();
								}

								if (bShift == true)
								{
									ArrayList selection = new ArrayList();

									// for every node between selected node and the clicked node, alter the selection
									int startOrder = 0;

									if (m_oTreeView.SelectedNode != null)
										startOrder = m_oTreeView.SelectedNode.NodeOrder;

									int endOrder = oNode.NodeOrder;

									if (startOrder <= endOrder)
									{
										for (int order = startOrder; order <= endOrder; order++)
										{
											Node node = m_oTreeView.GetNodeByOrder(order);

											if (node == null)
												continue;

											node.TreeView = m_oTreeView;
											node.SelectInternal(true);

											selection.Add(node);
										}
									}
									else
									{
										for (int order = endOrder; order <= startOrder; order++)
										{
											Node node = m_oTreeView.GetNodeByOrder(order);

											if (node == null)
												continue;

											node.TreeView = m_oTreeView;
											node.SelectInternal(true);

											selection.Add(node);
										}
									}

									m_bSkipSelectionChange = true;

									m_oSelectionService.SetSelectedComponents(selection, SelectionTypes.Replace);

									m_bSkipSelectionChange = false;

									oNode.DesignSelected = true;

									m_oTreeView.Invalidate();
								}
								else
								{
									if (oNode.IsSelected == true)
									{
										oNode.TreeView = m_oTreeView;
										oNode.IsSelected = false;
										oNode.DesignSelected = false;

										m_bSkipSelectionChange = true;

										if (bControl == true)
											m_oSelectionService.SetSelectedComponents(new Component[] {oNode}, SelectionTypes.Auto);

										m_oTreeView.Invalidate();
										m_bSkipSelectionChange = false;
									}
									else
									{
										oNode.TreeView = m_oTreeView;
										oNode.SelectInternal(true);

										oNode.DesignSelected = true;
										m_oDragNode = oNode;

										m_oSelectedNode = oNode;
										m_bSkipSelectionChange = true;

										if (bControl == false)
											m_oSelectionService.SetSelectedComponents(new Component[] {oNode}, SelectionTypes.Replace);
										else
											m_oSelectionService.SetSelectedComponents(new Component[] {oNode}, SelectionTypes.Auto);

										m_oTreeView.Invalidate();
										m_bSkipSelectionChange = false;
									}
								}
							}

							break;
						}

						if (m_oTreeView.Style.ShowPlusMinus == true && m_oTreeView.IsExpandBox(p) == true)
						{
							Node oNode = m_oTreeView.GetNodeByItemBox(m_oTreeView.GetExpandBoxRect(p));

							if (oNode != null)
								oNode.IsExpanded = !oNode.IsExpanded;

							try
							{
								RaiseComponentChanging(TypeDescriptor.GetProperties(oNode)["IsExpanded"]);
							}
							catch
							{
							}
							try
							{
								RaiseComponentChanged(TypeDescriptor.GetProperties(oNode)["IsExpanded"], null, null);
							}
							catch
							{
							}

							break;
						}

						if (m_oTreeView.IsNodeCheckBox(p) == true)
						{
							Node oNode = m_oTreeView.GetNodeByItemCheckBox(m_oTreeView.GetNodeCheckBoxRect(p));
							oNode.ToggleChecked();

							try
							{
								RaiseComponentChanging(TypeDescriptor.GetProperties(oNode)["Checked"]);
							}
							catch
							{
							}
							try
							{
								RaiseComponentChanged(TypeDescriptor.GetProperties(oNode)["Checked"], null, null);
							}
							catch
							{
							}

							break;
						}

						m_oTreeView.ClearAllSelection();
						m_oTreeView.ClearDesignSelection();
						m_oTreeView.ClearDesignHighlight();

						m_bSkipSelectionChange = true;
						m_oSelectionService.SetSelectedComponents(new Component[] {m_oTreeView}, SelectionTypes.Replace);
						m_bSkipSelectionChange = false;

						#endregion

						break;

					case WM_MOUSEMOVE:

						#region mouse move

						if (m_oSelector.Visible == false)
						{
							POINT screenPoint = new POINT();
							screenPoint.x = m_oTreeView.Location.X - 8;
							screenPoint.y = m_oTreeView.Location.Y - 8;
							User32.ClientToScreen(m_oTreeView.Parent.Handle, ref screenPoint);
//							m_oSelector.Show(screenPoint.x, screenPoint.y);
						}

						// try to find the group or node object and set the object to the property window
						oComponent = m_oTreeView.GetSubObjectAtPoint(p);
						Cursor.Current = System.Windows.Forms.Cursors.Arrow;

						// if we are dragging and the button is not pressed, then skip the dragging
						if (m_bIsDragging == true && lButton == false)
						{
							m_oTreeView.DragDropNode = null;
							m_bMouseDown = false;
							m_bIsDragging = false;
							m_oDragNode = null;

							if (m_oDragDropPop != null)
							{
								m_oDragDropPop.Close();
								m_oDragDropPop.Dispose();
								m_oDragDropPop = null;
							}

							m_oTreeView.ClearDesignHighlight();
							m_oTreeView.Invalidate();

							//Cursor.Current = m_oLastCursor;							

							break;
						}

						// Test the dragging at the border area
						if (p.X <= 4 || p.Y <= 4 || p.X >= m_oTreeView.Width - 8 || p.Y >= m_oTreeView.Height - 3)
						{
							m_oTreeView.DragDropNode = null;
							m_bMouseDown = false;
							m_bIsDragging = false;
							m_oDragNode = null;

							if (m_oDragDropPop != null)
							{
								m_oDragDropPop.Close();
								m_oDragDropPop.Dispose();
								m_oDragDropPop = null;
							}

							m_oTreeView.ClearDesignHighlight();
							m_oTreeView.Invalidate();

							//Cursor.Current = m_oLastCursor;							

							break;
						}

						if (oComponent != null || m_oTreeView.IsExpandBox(p) == true || m_oTreeView.IsNodeCheckBox(p))
						{
							m_oLastCursor = Cursor.Current;
							Cursor.Current = System.Windows.Forms.Cursors.Arrow;
						}
						//else
						//Cursor.Current = m_oLastCursor;

						// start the dragging of the mouse is moving and the mouse button is still at its down state

						#region dragdrop start test

						if (m_bIsDragging == false && m_bMouseDown == true && (oComponent is Node) && m_oMouseDown != p
							&& (m_oDragDropPop == null || m_oDragDropPop.Visible == false))
						{
							if (Math.Abs(m_oMouseDown.X - p.X) > 3 || Math.Abs(m_oMouseDown.Y - p.Y) > 3)
							{
								m_bIsDragging = true;
								m_oDragNode = oComponent as Node;

								if (m_oDragNode != null)
								{
									m_oDragDropPop = new DragDropPopup(m_oDragNode);

									m_oDragDropPop.Show();
									m_oTreeView.Focus();
								}
							}
						}

						#endregion

						// test the moving if the moving is at some drop node allow the drop and if it is at something
						// not allowed, then draw the dragdrop node control in red background

						#region dragdrop node drop test

						if (m_bIsDragging == true && m_bMouseDown == true && m_oMouseDown != p && m_oDragDropPop != null)
						{
							m_oDragDropPop.CanDrop = false;
							m_oDragDropPop.IsCopy = bControl;
							m_oDragDropPop.Left = m_oTreeView.PointToScreen(p).X + 10;
							m_oDragDropPop.Top = m_oTreeView.PointToScreen(p).Y + 10;

							// get the drop object and check if we can drop it here
							// try to find the group or node object and set the object to the property window
							Node oCanDropNode = m_oTreeView.GetSubObjectAtPoint(p) as Node;

							if (oCanDropNode != null)
							{
								if (m_oDragNode != null && m_oDragNode.IsSomeParent(oCanDropNode) == false)
									m_oDragDropPop.CanDrop = true;
							}

							if (oCanDropNode != null && oCanDropNode.IsExpanded == false)
							{
								// start the expand timer 
								m_oTimerExpandNode = oCanDropNode;

								Timer timer = new Timer();
								timer.Interval = 800;
								timer.Tick += new EventHandler(this.ExpandTimerTick);
								timer.Enabled = true;
							}

							m_oDragDropPop.Refresh();
						}

						#endregion

						// test the dragdrop while moving, if the scroller is used in the nodes area, then start the scrolling thread
						// and if the scrolling thread is running and the mouse has left the autoscroll area, then stop the scrolling

						#region dragdrop nodes autoscroll

						if (m_bIsDragging == true && m_oTreeView.Controls.Contains(m_oTreeView.m_oScrollBar) == true
							&& m_oDragNode != null)
						{
							int nAdd = 10;
							if (m_oTreeView.Controls.Contains(m_oTreeView.m_oHScrollBar))
								nAdd += m_oTreeView.m_oHScrollBar.Height;

							if (p.Y < 10)
							{
								if (m_oDragScrollThread != null)
									try
									{
										m_oDragScrollThread.Abort();
									}
									catch
									{
									}

								m_oDragScrollThread = new System.Threading.Thread(new System.Threading.ThreadStart(this.OnDragScrollDown));
								m_oDragScrollThread.Start();
							}
							else if (p.Y > m_oTreeView.Height - nAdd)
							{
								if (m_oDragScrollThread != null)
									try
									{
										m_oDragScrollThread.Abort();
									}
									catch
									{
									}

								m_oDragScrollThread = new System.Threading.Thread(new System.Threading.ThreadStart(this.OnDragScrollUp));
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
							}
						}

						#endregion

						// create the highlighting while mouse draging
						if (oComponent != null)
						{
							m_oTreeView.ClearDesignHighlight();

							// check for the node, if we are moving above the node, check if we are not in the dragging mode

							#region Node dragdrop structure creation and dragdrop check

							if (oComponent is Node && m_oDragNode != null)
							{
								Node oNode = oComponent as Node;

								if (oNode == m_oDragNode)
									break;

								// we are not dragging, just hightlight the node
								if (m_bIsDragging == false)
								{
									oNode.DesignHighlighted = true;
									m_oTreeView.Invalidate();
								}
								else
								{
									// we are in the dragging mode, get the node's rectangle and do some more calculations and
									// operations here
									Rectangle oRect = m_oTreeView.GetNodeRect(oNode);

									// test the area where we are with the actual point of drag. 
									if (p.Y >= oRect.Top && p.Y < oRect.Top + ((float) oRect.Height/2.0))
									{
										if (p.X > m_oMouseDown.X + 3)
											// we are in the upper part of the node, but in the right side, drop under
											m_oTreeView.DragDropNode = new NodeDragDrop(m_oDragNode, oNode, NodeDropMode.DropUnder);
										else
											// we are in the upper part of the node
											m_oTreeView.DragDropNode = new NodeDragDrop(m_oDragNode, oNode, NodeDropMode.DropInfront);
									}
									else if (p.Y >= oRect.Top + ((float) oRect.Height/2.0) && p.Y <= oRect.Bottom)
									{
										// we are in the lower part of the node
										if (p.X > m_oMouseDown.X + 3)
											// test the X coordinate, if we are in the right from the pop, then behave in that way
											m_oTreeView.DragDropNode = new NodeDragDrop(m_oDragNode, oNode, NodeDropMode.DropUnder);
										else
											// test the X coordinate, if we are in the right from the pop, then behave in that way
											m_oTreeView.DragDropNode = new NodeDragDrop(m_oDragNode, oNode, NodeDropMode.DropAfter);
									}
								}
							}

							#endregion						

							// node design highlight check
							if (oComponent is Node && m_oDragNode == null)
							{
								Node oNode = oComponent as Node;

								oNode.DesignHighlighted = true;

								m_oTreeView.Invalidate();

								if (oNode != m_oHighlightedNode)
								{
									OnNodeMouseLeave(m_oHighlightedNode);
									OnNodeMouseEnter(oNode);

									m_oHighlightedNode = oNode;
								}
							}

							break;
						}

						OnNodeMouseLeave(m_oHighlightedNode);
						m_oHighlightedNode = null;

						m_oTreeView.ClearDesignHighlight();
						m_oTreeView.Invalidate();

						#endregion						

						break;

					case WM_LBUTTONUP:
						Cursor.Current = System.Windows.Forms.Cursors.Arrow;

						#region button up

						// stop the drag drop scrolling thread
						if (m_oDragScrollThread != null)
							try
							{
								m_oDragScrollThread.Abort();
							}
							catch
							{
							}

						if (m_oDragNode == null || m_bIsDragging == false)
						{
							m_bMouseDown = false;
							m_bIsDragging = false;
							m_oDragNode = null;

							if (m_oDragDropPop != null)
							{
								m_oDragDropPop.Close();
								m_oDragDropPop.Dispose();
								m_oDragDropPop = null;
							}

							m_oTreeView.DragDropNode = null;

							m_oTreeView.Invalidate();

							break;
						}

						if (m_bIsDragging == true && m_oDragDropPop.CanDrop == false)
						{
							m_bMouseDown = false;
							m_bIsDragging = false;
							m_oDragNode = null;

							m_oTreeView.DragDropNode = null;

							m_oDragDropPop.Close();
							m_oDragDropPop.Dispose();
							m_oDragDropPop = null;

							m_oTreeView.Invalidate();

							break;
						}

						// try to find the group or node object and set the object to the property window
						Node oDropNode = m_oTreeView.GetSubObjectAtPoint(p) as Node;

						// test the drop target if we are not droping at the same place
						if (oDropNode != null && m_oDragNode != null && oDropNode == m_oDragNode)
						{
							m_bMouseDown = false;
							m_bIsDragging = false;

							if (m_oDragDropPop != null)
							{
								m_oDragDropPop.Close();
								m_oDragDropPop.Dispose();
								m_oDragDropPop = null;
							}

							m_oTreeView.DragDropNode = null;

							m_oTreeView.Invalidate();

							break;
						}

						#region droping at node

						// perform the drop of the node
						if (oDropNode != null)
						{
							// remove the node from its parent collection
							if (bControl == false)
								m_oDragNode.Collection.Remove(m_oDragNode);

							Node oCopyNode = m_oDragNode;

							if (bControl == true)
								oCopyNode = CloneNode(m_oDragNode);

							// check where we are dropping, we can test the TreeView's nodedrop variables							
							if (m_oTreeView.DragDropNode != null
								&& m_oTreeView.DragDropNode.NodeDropMode == NodeDropMode.DropUnder)
							{
								// drop at the node
								// add the node to the new parent holder							
								oCopyNode.TreeView = oDropNode.TreeView;
								oCopyNode.SetParent(oDropNode);
								oCopyNode.YOrder = 0;
								oDropNode.Nodes.Insert(0, oCopyNode);

								foreach (Node oSubNode in oCopyNode.Nodes)
									oSubNode.m_nCollectionOrder = oSubNode.YOrder;
							}
							else if (m_oTreeView.DragDropNode != null
								&& m_oTreeView.DragDropNode.NodeDropMode == NodeDropMode.DropAfter)
							{
								// we are droping the node behind the drop node selected
								Node oNode = m_oTreeView.DragDropNode.DropNode;

								if (oNode.Collection.Count - 1 == oNode.Index)
								{
									// add the node to the new parent holder							
									oCopyNode.TreeView = oNode.TreeView;
									oCopyNode.NodeMoving = true;
									oCopyNode.Parent = oNode.Parent;
									oNode.Collection.Add(oCopyNode);
									oCopyNode.NodeMoving = false;
								}
								else
								{
									// add the node to the new parent holder							
									oCopyNode.TreeView = oNode.TreeView;
									oCopyNode.NodeMoving = true;
									oCopyNode.Parent = oNode.Parent;
									oNode.Collection.Insert(oNode.Index + 1, oCopyNode);
									oCopyNode.NodeMoving = false;
								}
							}
							else if (m_oTreeView.DragDropNode != null
								&& m_oTreeView.DragDropNode.NodeDropMode == NodeDropMode.DropInfront)
							{
								// we are droping the node infront the drop node selected
								Node oNode = m_oTreeView.DragDropNode.DropNode;

								// add the node to the new parent holder							
								oCopyNode.TreeView = oNode.TreeView;
								oCopyNode.NodeMoving = true;
								oCopyNode.Parent = oNode.Parent;
								oNode.Collection.Insert(oNode.Index, oCopyNode);
								oCopyNode.NodeMoving = false;
							}

							foreach (Node oCollectionNode in oCopyNode.Collection)
								oCollectionNode.m_nCollectionOrder = oCollectionNode.Index;

							// call the update of the properties
							try
							{
								RaiseComponentChanging(TypeDescriptor.GetProperties(oCopyNode)["Parent"]);
							}
							catch
							{
							}
							try
							{
								RaiseComponentChanged(TypeDescriptor.GetProperties(oCopyNode)["Parent"], null, null);
							}
							catch
							{
							}
							try
							{
								RaiseComponentChanging(TypeDescriptor.GetProperties(oDropNode)["Nodes"]);
							}
							catch
							{
							}
							try
							{
								RaiseComponentChanged(TypeDescriptor.GetProperties(oDropNode)["Nodes"], null, null);
							}
							catch
							{
							}
							try
							{
								RaiseComponentChanging(TypeDescriptor.GetProperties(typeof (Node))["NodeStyle"]);
							}
							catch
							{
							}
							try
							{
								RaiseComponentChanged(TypeDescriptor.GetProperties(typeof (Node))["NodeStyle"], null, null);
							}
							catch
							{
							}
							try
							{
								RaiseComponentChanging(TypeDescriptor.GetProperties(typeof (Node))["NodeStyleSource"]);
							}
							catch
							{
							}
							try
							{
								RaiseComponentChanged(TypeDescriptor.GetProperties(typeof (Node))["NodeStyleSource"], null, null);
							}
							catch
							{
							}
							try
							{
								RaiseComponentChanging(TypeDescriptor.GetProperties(typeof (Node))["Text"]);
							}
							catch
							{
							}
							try
							{
								RaiseComponentChanged(TypeDescriptor.GetProperties(typeof (Node))["Text"], null, null);
							}
							catch
							{
							}
							try
							{
								RaiseComponentChanging(TypeDescriptor.GetProperties(typeof (Node))["ContextMenu"]);
							}
							catch
							{
							}
							try
							{
								RaiseComponentChanged(TypeDescriptor.GetProperties(typeof (Node))["ContextMenu"], null, null);
							}
							catch
							{
							}
							try
							{
								RaiseComponentChanging(TypeDescriptor.GetProperties(typeof (Node))["ContextMenuSource"]);
							}
							catch
							{
							}
							try
							{
								RaiseComponentChanged(TypeDescriptor.GetProperties(typeof (Node))["ContextMenuSource"], null, null);
							}
							catch
							{
							}
							try
							{
								RaiseComponentChanging(TypeDescriptor.GetProperties(typeof (Node))["Image"]);
							}
							catch
							{
							}
							try
							{
								RaiseComponentChanged(TypeDescriptor.GetProperties(typeof (Node))["Image"], null, null);
							}
							catch
							{
							}
							try
							{
								RaiseComponentChanging(TypeDescriptor.GetProperties(typeof (Node))["Imageindex"]);
							}
							catch
							{
							}
							try
							{
								RaiseComponentChanged(TypeDescriptor.GetProperties(typeof (Node))["ImageIndex"], null, null);
							}
							catch
							{
							}

							//oDropNode.ExpandAll();
						}

						#endregion					

						// clear the dragdrop properties
						m_bMouseDown = false;
						m_bIsDragging = false;
						m_oDragNode = null;

						m_oTreeView.DragDropNode = null;

						m_oTreeView.Invalidate();

						if (m_oDragDropPop != null)
						{
							m_oDragDropPop.Close();
							m_oDragDropPop.Dispose();
							m_oDragDropPop = null;
						}

						#endregion

						break;

					case WM_MOUSELEAVE:
						m_oTreeView.DragDropNode = null;

						m_bMouseDown = false;
						m_bIsDragging = false;
						m_oDragNode = null;

						if (m_oDragDropPop != null)
						{
							m_oDragDropPop.Close();
							m_oDragDropPop.Dispose();
							m_oDragDropPop = null;
						}

						m_oTreeView.ClearDesignHighlight();
						m_oTreeView.Invalidate();

						OnNodeMouseLeave(m_oHighlightedNode);
						m_oHighlightedNode = null;

						//m_oSelector.Hide();

						break;

					case WM_PAINT:
						// when there are some node to be destroyed from the clipboard action, then do it
						// we have to do it here beacuse there is not an event that we can handle once the 
						// clipboard action is finished
						foreach (Node node in NodeComponentToDestroy.ToArray())
						{
							node.DestroyComponent(m_oDesignerHost);
							node.Remove();
						}
						NodeComponentToDestroy.Clear();

						base.WndProc(ref m);

						break;

					default:
						base.WndProc(ref m);
						break;
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString());
			}
		}

		#endregion

		#region event handlers

		#region selection events

		/// <summary>
		/// Called by the framewrok when the selection is changed
		/// </summary>
		protected void OnSelectionServiceChanged(object sender, EventArgs args)
		{
			if (m_bSkipSelectionChange == true)
				return;

			ICollection aComponents = m_oSelectionService.GetSelectedComponents();

			if (aComponents.Count > 0 && FindAndSelectComponent(aComponents) == true)
			{
				m_oTreeView.SetNodeScrollVisible(m_oSelectedNode, true);

				return;
			}

			if (m_oSelectedNode != null)
			{
				if (m_oSelectionService.GetComponentSelected(m_oSelectedNode) == false
					&& m_oSelectionService.GetComponentSelected(m_oTreeView) == false)
				{
					m_oSelectedNode = null;
					m_oTreeView.ClearDesignSelection();
					m_oTreeView.ClearDesignHighlight();
					m_oTreeView.Invalidate();
				}
				else
				{
					m_oSelectedNode.DesignSelected = true;
					m_oTreeView.Invalidate();
				}
			}
		}

		#endregion

		#region node&group component changed, component removed events, component created

		private void OnComponentCreated(TreeView oTreeViewCmp)
		{
			try
			{
				Control oBaseControl = m_oDesignerHost.RootComponent as Control;
				oTreeViewCmp.BackColor = oBaseControl.BackColor;

				// now check the REG_KEY for information if we should show the info screen

				#region info screen registry getter

				try
				{
					Microsoft.Win32.RegistryKey oKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(REGKEY);

					if (oKey == null)
					{
						Microsoft.Win32.RegistryKey oSoftware = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software", true);

						oKey = oSoftware.OpenSubKey("PureComponents", true);

						if (oKey == null)
						{
							oKey = oSoftware.CreateSubKey("PureComponents");
							oKey = oKey.CreateSubKey("Components");
							oKey = oKey.CreateSubKey("TreeViewV2");
							oKey = oKey.CreateSubKey("Designer");
						}
						else
						{
							Microsoft.Win32.RegistryKey oComponents = oKey.OpenSubKey("Components", true);

							if (oComponents == null)
							{
								oKey = oKey.CreateSubKey("Components");
								oKey = oKey.CreateSubKey("TreeViewV2");
								oKey = oKey.CreateSubKey("Designer");
							}
							else
							{
								Microsoft.Win32.RegistryKey oTreeView = oComponents.OpenSubKey("TreeViewV2", true);

								if (oTreeView == null)
								{
									oKey = oComponents.CreateSubKey("TreeViewV2");
									oKey = oKey.CreateSubKey("Designer");
								}
								else
								{
									oKey = oTreeView.OpenSubKey("Designer", true);

									if (oKey == null)
										oKey = oTreeView.CreateSubKey("Designer");
								}
							}
						}
					}

					oKey.Close();
				}
				catch
				{
				}

				#endregion				
			}
			catch
			{
			}
		}

		private void OnComponentAdded(object sender, ComponentEventArgs e)
		{
			Node oNode = e.Component as Node;

			if (oNode != null)
			{
				PropertyDescriptor oParentProperty = TypeDescriptor.GetProperties(oNode).Find("Parent", false);
				oParentProperty.AddValueChanged(oNode, m_oNodeParentChanged);
			}
		}

		private void OnNodeParentChanged(object sender, EventArgs e)
		{
			Node oNode = sender as Node;

			if (oNode == null)
				return;

			if (oNode.Parent != null)
			{
				if (oNode.Parent.Nodes.Contains(oNode) == false)
					oNode.Parent.Nodes.Add(oNode);

				if (oNode.TreeView == null)
					oNode.TreeView = oNode.Parent.TreeView;
			}
			else
			{
				if (oNode.TreeView.Nodes.Contains(oNode) == false)
					oNode.TreeView.Nodes.Add(oNode);
			}

			//oNode.Expand();
			//oNode.ExpandAll();

			PropertyDescriptor oParentProperty = TypeDescriptor.GetProperties(oNode).Find("Parent", false);
			oParentProperty.RemoveValueChanged(oNode, m_oNodeParentChanged);
		}

		private void OnComponentRemoving(object sender, ComponentEventArgs e)
		{
			Node oNode = e.Component as Node;
			TreeView oTreeView = e.Component as TreeView;
			NodePanel panel = e.Component as NodePanel;

			if (panel != null)
			{
				if (panel.Node != null)
					panel.Node.Panel = null;

				return;
			}

			if (oNode != null && oNode.Collection.Contains(oNode) == true)
			{
				if (m_bCanRemoveGrupNode == true)
					oNode.Collection.Remove(oNode);

				foreach (Node oSubNode in oNode.Nodes.ToNodeArray())
					oSubNode.DestroyComponent(m_oDesignerHost);

				// destroy the associate Panel as well
				if (oNode.Panel != null)
				{
					m_oTreeView.Controls.Remove(oNode.Panel);

					m_oDesignerHost.DestroyComponent(oNode.Panel);

					oNode.Panel = null;
				}

				m_oTreeView.Invalidate();

				return;
			}

			try
			{
				if (oTreeView != null && oTreeView.Nodes != null)
				{
					m_bCanRemoveGrupNode = false;
					Node[] aNodes = oTreeView.Nodes.ToNodeArray();

					foreach (Node oSubNode in aNodes)
						oSubNode.DestroyComponentNoRemove(m_oDesignerHost);

					m_bCanRemoveGrupNode = true;
				}
			}
			catch (Exception ex)
			{
				string s = ex.ToString();
			}
		}

		#endregion

		#region node&group events		

		/// <summary>
		/// Called when the user selects Delete group menu item
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void OnAddNode(object sender, EventArgs e)
		{
			using (DesignerTransaction oTransaction = m_oDesignerHost.CreateTransaction("Adding new Node"))
			{
				Node oSelNode = m_oSelectedNode;

				Node oNode = CreateNodeComponent();
				//oNode.Expand();

				m_oTreeView.ClearNodeSelection();
				m_oTreeView.ClearDesignSelection();
				m_oTreeView.ClearDesignHighlight();

				oNode.Select();
				oNode.DesignSelected = true;

				m_oSelectedNode = oNode;
				m_bSkipSelectionChange = true;
				m_oSelectionService.SetSelectedComponents(new Component[] {oNode}, SelectionTypes.Replace);
				m_bSkipSelectionChange = false;

				if (oNode.Parent != null)
					oNode.Parent.Nodes.Add(oNode);
				else
					oNode.TreeView.Nodes.Add(oNode);

				oTransaction.Commit();
			}
		}

		/// <summary>
		/// Add the new panel to the actual selected node
		/// </summary>
		/// <param name="o"></param>
		/// <param name="empty"></param>
		private void OnAddPanel(object o, EventArgs empty)
		{
			using (DesignerTransaction oTransaction = m_oDesignerHost.CreateTransaction("Adding Node Panel"))
			{
				Node oSelNode = m_oSelectedNode;

				NodePanel panel = CreatePanelComponent();
				panel.Left = oSelNode.Left;
				panel.Top = oSelNode.Top;

				m_bSkipSelectionChange = true;
				m_oSelectionService.SetSelectedComponents(new Component[] {panel}, SelectionTypes.Replace);
				m_bSkipSelectionChange = false;

				oSelNode.Expand();
				oSelNode.Panel = panel;

				oSelNode.FitPanel();

				oTransaction.Commit();
			}
		}

		/// <summary>
		/// Called when the user selects Delete group menu item
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void OnDeleteNode(object sender, EventArgs e)
		{
			Node oSelNode = m_oSelectedNode;

			if (oSelNode == null)
				return;

			// call the designer and delete the component
			oSelNode.DestroyComponent(m_oDesignerHost);

			// redraw the component
			m_oTreeView.Invalidate();

			m_oSelectedNode = null;
		}

		/// <summary>
		/// Clears all nodes within treeview
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void OnDeleteNodes(object sender, EventArgs e)
		{
			foreach (Node node in m_oTreeView.Nodes.ToNodeArray())
				node.DestroyComponent(m_oDesignerHost);

			m_oTreeView.Invalidate();
			m_oSelectedNode = null;
		}

		/// <summary>
		/// Called by the framework when user selects the TreeView color scheme
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void OnColorScheme(object sender, EventArgs e)
		{
			if (m_oColorPickerForm == null || m_oColorPickerForm.Visible == false)
			{
				m_oColorPickerForm = new ColorSchemePickerForm(m_oTreeView, m_oDesignerHost, this);

				try
				{
					m_oActionMenuTreeView.Hide();
					m_oColorPickerForm.ShowDialog();
				}
				catch (Exception ex)
				{
					Console.Out.WriteLine(ex.ToString());
				}
			}
		}

		/// <summary>
		/// Called by the framework when user selects the Apply color scheme
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void OnApplyColorScheme(object sender, EventArgs e)
		{
		}

		/// <summary>
		/// Called by the framework when the apply color scheme object is being choosen
		/// </summary>
		/// <param name="sItem">Item clicked</param>
		protected void OnActionMenuNodeItemClicked(ActionMenuItem oItem)
		{
			TreeViewStyleFactory oFactory = TreeViewStyleFactory.GetInstance();
			TreeViewStyle oStyle = null;

			#region menu group section color scheme

			if (oItem.MenuGroup.Title == "Color Schemes")
			{
				m_oActionMenuNode.Hide();

				switch (oItem.Text)
				{
					case "Default":
						oStyle = oFactory.GetDefaultTreeStyle();
						break;
					case "Forest":
						oStyle = oFactory.GetForestTreeStyle();
						break;
					case "Gold":
						oStyle = oFactory.GetGoldTreeStyle();
						break;
					case "Ocean":
						oStyle = oFactory.GetOceanTreeStyle();
						break;
					case "Rose":
						oStyle = oFactory.GetRoseTreeStyle();
						break;
					case "Silver":
						oStyle = oFactory.GetSilverTreeStyle();
						break;
					case "Sky":
						oStyle = oFactory.GetSkyTreeStyle();
						break;
					case "Sunset":
						oStyle = oFactory.GetSunsetTreeStyle();
						break;
					case "Wood":
						oStyle = oFactory.GetWoodTreeStyle();
						break;
					default:
						oStyle = oFactory.GetDefaultTreeStyle();
						break;
				}

				string sTransaction = "Applying style";

				if (m_oSelectedNode != null)
					sTransaction = "Applying style to Node [" + m_oSelectedNode.GetText() + "]";

				using (DesignerTransaction oTransaction = m_oDesignerHost.CreateTransaction(sTransaction))
				{
					// check the selected component and apply the style
					if (m_oSelectedNode != null)
					{
						NodeStyle oOldStyle = m_oSelectedNode.GetNodeStyle();
						NodeStyleSource eOldStyleSource = m_oSelectedNode.NodeStyleSource;

						PropertyDescriptorCollection aProperties = TypeDescriptor.GetProperties(m_oSelectedNode);
						PropertyDescriptor oNodeStyleProperty = aProperties.Find("NodeStyle", false);
						PropertyDescriptor oNodeStyleSourceProperty = aProperties.Find("NodeStyleSource", false);

						try
						{
							RaiseComponentChanging(oNodeStyleProperty);
						}
						catch
						{
						}
						try
						{
							RaiseComponentChanging(oNodeStyleSourceProperty);
						}
						catch
						{
						}

						if (m_oSelectedNode.NodeStyle != null)
							oStyle.NodeStyle.SelectedFillStyle = m_oSelectedNode.NodeStyle.SelectedFillStyle;
						else
							oStyle.NodeStyle.SelectedFillStyle = m_oTreeView.Style.NodeStyle.SelectedFillStyle;

						oNodeStyleSourceProperty.SetValue(m_oSelectedNode, NodeStyleSource.Local);
						oNodeStyleProperty.SetValue(m_oSelectedNode, oStyle.NodeStyle);

						try
						{
							RaiseComponentChanged(oNodeStyleProperty, oOldStyle, m_oSelectedNode.NodeStyle);
						}
						catch
						{
						}
						try
						{
							RaiseComponentChanged(oNodeStyleSourceProperty, eOldStyleSource, m_oSelectedNode.NodeStyleSource);
						}
						catch
						{
						}
					}

					oTransaction.Commit();
				}
			}

			#endregion

			#region menu group section arranging

			if (oItem.MenuGroup.Title == "Arranging")
			{
				PropertyDescriptorCollection aProperties = TypeDescriptor.GetProperties(m_oSelectedNode);
				PropertyDescriptor oParentProperty = aProperties.Find("Parent", false);

				try
				{
					RaiseComponentChanging(oParentProperty);
				}
				catch
				{
				}

				switch (oItem.Text)
				{
					case "Move Top":
						m_oActionMenuNode.Hide();
						m_oSelectedNode.MoveTop();
						break;
					case "Move Bottom":
						m_oActionMenuNode.Hide();
						m_oSelectedNode.MoveBottom();
						break;
					case "Move Down":
						m_oActionMenuNode.Hide();
						m_oSelectedNode.MoveDown();
						break;
					case "Move Up":
						m_oActionMenuNode.Hide();
						m_oSelectedNode.MoveUp();
						break;
					case "Move Left":
						m_oActionMenuNode.Hide();
						m_oSelectedNode.MoveLeft();
						break;
					case "Move Right":
						m_oActionMenuNode.Hide();
						m_oSelectedNode.MoveRight();
						break;
					case "Expand":
						m_oActionMenuNode.Hide();
						m_oSelectedNode.ExpandAll();
						break;
					case "Collapse":
						m_oActionMenuNode.Hide();
						m_oSelectedNode.CollapseAll();
						break;
				}

				try
				{
					RaiseComponentChanged(oParentProperty, null, m_oTreeView.Nodes);
				}
				catch
				{
				}
			}

			#endregion

			#region menu group section editing

			if (oItem.MenuGroup.Title == "Editing")
			{
				switch (oItem.Text)
				{
					case "Add Node":
						m_oActionMenuNode.Hide();
						OnAddNode(null, EventArgs.Empty);
						break;
					case "Add Panel":
						m_oActionMenuNode.Hide();
						OnAddPanel(null, EventArgs.Empty);
						break;
					case "Delete Node":
						m_oActionMenuNode.Hide();
						OnDeleteNode(null, EventArgs.Empty);
						break;
					case "Delete TreeView":
						m_oActionMenuNode.Hide();
						// select tree view
						m_oSelectionService.SetSelectedComponents(new Component[] {m_oTreeView});
						m_oOldDelete.Invoke();
						break;
					case "Clear Content":
						m_oActionMenuNode.Hide();

						if (MessageBox.Show(null, "Are you sure you want to delete all nodes?", "TreeView Designer",
						                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
							break;

						OnDeleteNodes(this, EventArgs.Empty);

						break;
					case "Properties":
						m_oActionMenuNode.Hide();
						m_oUIService.ShowToolWindow(System.ComponentModel.Design.StandardToolWindows.PropertyBrowser);
						break;
					case "Copy":
						m_oActionMenuNode.Hide();
						OnMenuCopy(this, EventArgs.Empty);
						break;
					case "Paste":
						m_oActionMenuNode.Hide();
						OnMenuPaste(this, EventArgs.Empty);
						break;
				}
			}

			#endregion			

			m_oActionMenuNode.Hide();
		}

		/// <summary>
		/// Called by the framework when the apply color scheme object is being choosen
		/// </summary>
		/// <param name="sItem">Item clicked</param>
		protected void OnActionMenuTreeViewItemClicked(ActionMenuItem oItem)
		{
			#region menu group section editing

			if (oItem.MenuGroup.Title == "Editing")
			{
				switch (oItem.Text)
				{
					case "Add Node":
						OnAddNode(null, EventArgs.Empty);

						break;
					case "Color Scheme Picker...":
						m_oActionMenuTreeView.Hide();
						OnColorScheme(null, EventArgs.Empty);
						break;
					case "Properties":
						m_oActionMenuTreeView.Hide();
						m_oUIService.ShowToolWindow(System.ComponentModel.Design.StandardToolWindows.PropertyBrowser);
						break;					
					case "Copy":
						m_oActionMenuTreeView.Hide();
						OnMenuCopy(this, EventArgs.Empty);
						break;
					case "Paste":
						m_oActionMenuTreeView.Hide();
						OnMenuPaste(this, EventArgs.Empty);
						break;
					case "Delete TreeView":
						m_oActionMenuTreeView.Hide();
						m_oOldDelete.Invoke();
						break;
					case "Clear Content":
						m_oActionMenuTreeView.Hide();

						if (MessageBox.Show(null, "Are you sure you want to delete all nodes?", "TreeView Designer",
						                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
							break;

						OnDeleteNodes(this, EventArgs.Empty);

						break;
				}
			}

			#endregion

			#region menu group section layout

			if (oItem.MenuGroup.Title == "Layout")
			{
				switch (oItem.Text)
				{
					case "Bring to Front":
						m_oActionMenuTreeView.Hide();
						m_oOldBringFront.Invoke();
						break;
					case "Send to Back":
						m_oActionMenuTreeView.Hide();
						m_oOldSendBack.Invoke();
						break;
					case "Align to Grid":
						m_oActionMenuTreeView.Hide();
						m_oOldAlignGrid.Invoke();
						break;
					case "Lock Controls":
						m_oActionMenuTreeView.Hide();
						m_oOldLockControls.Invoke();
						break;
				}
			}

			#endregion

			#region menu group section arranging

			if (oItem.MenuGroup.Title == "Arranging")
			{
				switch (oItem.Text)
				{
					case "Expand All":
						m_oTreeView.ExpandAll();
						break;

					case "Collapse All":
						m_oTreeView.CollapseAll();
						break;
				}
			}

			#endregion

			m_oActionMenuTreeView.Hide();
		}

		#endregion

		#region inplace editing		

		#endregion

		#endregion

		#region helper functions

		private void OnNodeMouseLeave(Node node)
		{
			m_Tooltip.Hide();
			m_Tooltip.DestroyHandle();
		}

		private void OnNodeMouseEnter(Node oNode)
		{
			this.TooltipNode = oNode;
			StartTooltipTimer();
		}

		/// <summary>
		/// Starts the timer for the tooltip
		/// </summary>
		private void StartTooltipTimer()
		{
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
				m_TooltipTimer = new System.Timers.Timer(1800);
			else
				m_TooltipTimer = new System.Timers.Timer(1800);

			m_TooltipTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTooltipTimer);
			m_TooltipTimer.Enabled = true;
		}

		/// <summary>
		/// The timer event
		/// </summary>
		private void OnTooltipTimer(object sender, System.Timers.ElapsedEventArgs args)
		{
			// kill the timer
			System.Timers.Timer timer = sender as System.Timers.Timer;
			if (timer == null)
				return;

			m_TooltipTimer.Enabled = false;
			m_TooltipTimer.Dispose();

			if (TooltipNode == null)
				return;

			// get the current mouse position, check the tooltip node and position, if the node is the same 
			// and position is not different too much, then show the tooltip exit otherwise
			Point cursorPos = System.Windows.Forms.Cursor.Position;
			Node node = m_oTreeView.GetSubObjectAtPoint(m_oTreeView.PointToClient(cursorPos)) as Node;

			if (node != TooltipNode)
				return;

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
					m_Tooltip.Show(cursorPos.X + 20, cursorPos.Y, node.GetTooltipStyle(), tooltipText, node.GetTreeView().TooltipPopDelay);
				else
				{
					Rectangle nodeRect = m_oTreeView.GetNodeRect(node);
					Point tooltipLoc = m_oTreeView.PointToScreen(nodeRect.Location);

					m_Tooltip.Show(tooltipLoc.X - 1, tooltipLoc.Y - 2, node.GetTooltipStyle(), tooltipText,
					               node.GetTreeView().TooltipPopDelay, false, false);
				}
			}
		}

		private void ExpandTimerTick(object sender, EventArgs e)
		{
			Timer timer = sender as Timer;
			timer.Enabled = false;
			timer.Dispose();

			// get the current node over node
			Node mouseOverNode = m_oTreeView.GetSubObjectAtPoint(m_oTreeView.PointToClient(System.Windows.Forms.Cursor.Position)) as Node;

			if (mouseOverNode == null || m_oTimerExpandNode == null)
				return;

			if (mouseOverNode == m_oTimerExpandNode)
				m_oTimerExpandNode.IsExpanded = true;

			m_oTreeView.Invalidate();
		}

		internal void InvokeComponentChanging(MemberDescriptor member)
		{
			try
			{
				RaiseComponentChanging(member);
			}
			catch
			{
			}
		}

		internal void InvokeComponentChanged(MemberDescriptor member, object oldValue, object newValue)
		{
			try
			{
				RaiseComponentChanged(member, oldValue, newValue);
			}
			catch
			{
			}
		}

		private void OnDragScrollDown()
		{
			while (true)
			{
				m_oTreeView.Invalidate();
				int nScrollIndex = -m_oTreeView.m_oScrollBar.Value;

				nScrollIndex += m_oTreeView.m_oScrollBar.SmallChange;

				if (nScrollIndex <= -(m_oTreeView.m_oScrollBar.Maximum - m_oTreeView.Height + m_oTreeView.m_oScrollBar.SmallChange))
				{
					nScrollIndex = -(m_oTreeView.m_oScrollBar.Maximum - m_oTreeView.Height + m_oTreeView.m_oScrollBar.SmallChange);
					return;
				}

				if (nScrollIndex > 6)
				{
					nScrollIndex = 6;
					return;
				}

				m_oTreeView.m_nScroll = nScrollIndex;
				m_oTreeView.Invalidate();

				System.Threading.Thread.Sleep(150);
			}
		}

		private void OnDragScrollUp()
		{
			while (true)
			{
				m_oTreeView.Invalidate();
				int nScrollIndex = -m_oTreeView.m_oScrollBar.Value;

				nScrollIndex -= m_oTreeView.m_oScrollBar.SmallChange;

				if (nScrollIndex <= -(m_oTreeView.m_oScrollBar.Maximum - m_oTreeView.Height + m_oTreeView.m_oScrollBar.SmallChange))
				{
					nScrollIndex = -(m_oTreeView.m_oScrollBar.Maximum - m_oTreeView.Height + m_oTreeView.m_oScrollBar.SmallChange);
					return;
				}

				if (nScrollIndex > 6)
				{
					nScrollIndex = 6;
					return;
				}

				m_oTreeView.m_nScroll = nScrollIndex;
				m_oTreeView.Invalidate();

				System.Threading.Thread.Sleep(150);
			}
		}

		protected TreeView CreateTreeViewComponent()
		{
			string sName = GenerateNewTreeViewName();

			TreeView oTreeView = m_oDesignerHost.CreateComponent(typeof (TreeView), sName) as TreeView;

			return oTreeView;
		}

		protected Node CreateNodeComponent()
		{
			string sName = GenerateNewNodeName();

			Node oNode = m_oDesignerHost.CreateComponent(typeof (Node), sName) as Node;

			oNode.TreeView = m_oTreeView;
			oNode.SetParent(m_oSelectedNode);
			oNode.Text = sName;

			return oNode;
		}

		protected NodePanel CreatePanelComponent()
		{
			string sName = GenerateNewName("NodePanel");

			NodePanel panel = m_oDesignerHost.CreateComponent(typeof (NodePanel), sName) as NodePanel;

			panel.Height = 50;

			return panel;
		}

		protected Node CreateNodeComponentBulk()
		{
			string sName = GenerateNewNodeName();

			Node oNode = m_oDesignerHost.CreateComponent(typeof (Node), sName) as Node;

			oNode.Text = sName;
			oNode.TreeView = m_oTreeView;

			return oNode;
		}

		protected string GenerateNewTreeViewName()
		{
			return GenerateNewName("TreeView");
		}

		protected string GenerateNewNodeName()
		{
			return GenerateNewName("Node");
		}

		protected string GenerateNewName(string sTmpl)
		{
			int curNum = 1;
			bool bDuplicateName;

			IReferenceService refsvc = GetService(typeof (IReferenceService)) as IReferenceService;
			string curTry;

			do
			{
				curTry = sTmpl + curNum;
				ComponentCollection comps = m_oDesignerHost.Container.Components;
				bDuplicateName = false;

				foreach (IComponent comp in comps)
				{
					string name = refsvc.GetName(comp);

					if (name.ToLower() == curTry.ToLower())
						bDuplicateName |= true;
				}

				curNum++;
			} while (bDuplicateName);

			return curTry;
		}

		protected Node CloneNode(Node oNode)
		{
			return CloneNode(oNode, true);
		}

		protected Node CloneNode(Node oNode, bool bExpand)
		{
			Node oCloneNode = CreateNodeComponent();

			oCloneNode.NodeMoving = true;
			oCloneNode.Parent = null;
			oCloneNode.Copy(oNode);

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

		protected void CopyTreeViewNode(TreeNode oTreeNode)
		{
			Node oNode = CreateNodeComponent();

			oNode.Text = oTreeNode.Text;
			oNode.Tag = oTreeNode.Tag;
			oNode.Checked = oTreeNode.Checked;

			foreach (TreeNode oSubNode in oTreeNode.Nodes)
				CopyTreeViewNode(oNode, oSubNode);

			m_oTreeView.Nodes.Add(oNode);
		}

		protected void CopyTreeViewNode(Node oNode, TreeNode oTreeNode)
		{
			Node oSubNode = CreateNodeComponent();

			oSubNode.Text = oTreeNode.Text;
			oSubNode.Tag = oTreeNode.Tag;

			foreach (TreeNode oSubTreeNode in oTreeNode.Nodes)
				CopyTreeViewNode(oSubNode, oSubTreeNode);

			oNode.Nodes.Add(oSubNode);
		}

		/// <summary>
		/// Builds TreeView out from the string data
		/// </summary>
		/// <param name="sData"></param>
		protected void BuildFromString(string sData)
		{
			string[] aData = sData.Split("\n\r".ToCharArray());

			Node oLastNode = m_oSelectedNode;

			int nLastSpaces = 0;

			Hashtable mapNode2Spaces = new Hashtable();

			foreach (string sToken in aData)
			{
				if (sToken.Length == 0)
					continue;

				// get number of spaces from the beginning of the string
				char[] aTokenData = sToken.ToCharArray();

				int nSpaces = 0;

				for (int nChar = 0; nChar < aTokenData.Length; nChar ++)
				{
					if (aTokenData[nChar] == '\t' || aTokenData[nChar] == ' ')
						nSpaces ++;
					else
						break;
				}

				// create node
				Node oNode = CreateNodeComponentBulk();
				oNode.Text = sToken.Substring(nSpaces);

				if (oLastNode != null)
				{
					if (nSpaces > nLastSpaces)
					{
						oNode.TreeView = m_oTreeView;

						oLastNode.Nodes.Add(oNode);

						oNode.Parent = oLastNode;
					}
					else
					{
						oNode.TreeView = m_oTreeView;

						// find the parent with indent just smaller than nLevel
						Node oParent = oLastNode.Parent;

						if (m_oSelectedNode != null)
							oParent = m_oSelectedNode;

						if (nSpaces != nLastSpaces)
						{
							bool bStop = false;

							while (oParent != null && bStop == false)
							{
								if (mapNode2Spaces.ContainsKey(oParent) == false)
								{
									bStop = true;
									continue;
								}

								int nParentSpace = (int) mapNode2Spaces[oParent];

								if (nParentSpace <= nSpaces)
								{
									bStop = true;
									oParent = oParent.Parent;
								}
								else
									oParent = oParent.Parent;
							}
						}

						if (oParent != null)
							oParent.Nodes.Add(oNode);
						else
							m_oTreeView.Nodes.Add(oNode);

						oNode.Parent = oParent;
					}

					oNode.ExpandAll();

					oLastNode = oNode;
					nLastSpaces = nSpaces;
					mapNode2Spaces.Add(oNode, nSpaces);
				}
				else
				{
					m_oTreeView.Nodes.Add(oNode);

					oLastNode = oNode;
					nLastSpaces = nSpaces;
					mapNode2Spaces.Add(oNode, nSpaces);

					oNode.ExpandAll();
				}
			}
		}

		protected bool FindAndSelectComponent(ICollection aComponents)
		{
			m_oTreeView.ClearDesignHighlight();
			m_oTreeView.ClearDesignSelection();

			foreach (Component oComponent in aComponents)
			{
				if (oComponent is Node)
				{
					Node oNode = oComponent as Node;

					m_oTreeView.ClearAllSelection();
					m_oTreeView.ClearDesignHighlight();
					m_oTreeView.ClearDesignSelection();

					oNode.Select();
					oNode.DesignSelected = true;

					m_oSelectedNode = oNode;

					m_oTreeView.Invalidate();

					return true;
				}
			}

			return false;
		}

		#endregion								

		#region overriden clipboard support

		/// <summary>
		/// Command handler for the Copy command.  This is called when the user copies the
		/// current selection to the clipboard.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnMenuCopy(object sender, EventArgs e)
		{
			try
			{
				// get the selection service, and test what kind of object is being selected. If it 
				// is the TreeView, serialize the whole TreeView, if it is the node or the group
				// then serialize this respectfully and store it under the appropriate reference to the
				// serialized object holder
				object serializedData = null;
				DataObject data = null;

				ICollection aComponentsSelected = m_oSelectionService.GetSelectedComponents();
				if (aComponentsSelected.Count == 0)
				{
					TreeViewDesigner.DataObject = null;
					return;
				}

				object[] aData = new object[aComponentsSelected.Count];
				aComponentsSelected.CopyTo(aData, 0);
				Component oSelectedComponent = aData[0] as Component;

				if (oSelectedComponent is TreeView)
				{
					serializedData = m_oDesignerSerializationService.Serialize(new TreeView[] {oSelectedComponent as TreeView});
					data = new DataObject("PureComponents.TreeView.Design.TreeViewData", serializedData);

					TreeViewDesigner.DataObject = data;
					Clipboard.SetDataObject(data, false);
				}
				else if (oSelectedComponent is Node)
				{
					serializedData = m_oDesignerSerializationService.Serialize(new Node[] {oSelectedComponent as Node});
					data = new DataObject("PureComponents.TreeView.Design.NodeData", serializedData);

					TreeViewDesigner.DataObject = data;
					Clipboard.SetDataObject(data, false);
				}
				else
				{
					TreeViewDesigner.DataObject = null;
				}
			}
			catch
			{
			}

			m_oOldCmdCopy.Invoke();
		}

		/// <summary>
		/// Command handler for the Paste command.  This is called when the user copies the
		/// current selection from the clipboard.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnMenuPaste(object sender, EventArgs e)
		{
			IDataObject data = Clipboard.GetDataObject();

			// If the data is text, then set the text of the clipboard as the inner created
			if (data.GetDataPresent(DataFormats.Text))
			{
				object oData = data.GetData(DataFormats.Text);
				string sData = oData.ToString();

				this.BuildFromString(sData);
			}
			else
			{
				// test the data for the node and group existency, because we have to handle it here in a different way using the
				// clone methods and special handling			
				if (TreeViewDesigner.DataObject != null &&
					TreeViewDesigner.DataObject.GetDataPresent("PureComponents.TreeView.Design.NodeData"))
				{
					ICollection aComponents = m_oSelectionService.GetSelectedComponents();
					bool treeviewSelected = false;

					if (aComponents.Count > 0)
					{
						foreach (Component component in aComponents)
						{
							if (component is PureComponents.TreeView.TreeView)
							{
								treeviewSelected = true;
								break;
							}
						}
					}

					// test for the node or group being selected
					if (m_oSelectedNode == null && treeviewSelected == false)
					{
						// show the message informing that it is not possible to paste data like this here
						MessageBox.Show(m_oTreeView, "Cannot paste data here, please select Node object first and try pasting data again.",
						                "TreeView Designer - Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

						return;
					}

					ICollection objects = m_oDesignerSerializationService.Deserialize(
						TreeViewDesigner.DataObject.GetData("PureComponents.TreeView.Design.NodeData"));

					// get the serialized node from the clipboard
					object[] aData = new object[1];
					objects.CopyTo(aData, 0);
					Node oNode = aData[0] as Node;

					// now clone the node
					Node oCloneNode = CloneNode(oNode, false);
					oCloneNode.ExpandAll();

					// set the parent of the node
					if (m_oSelectedNode != null)
						oCloneNode.Parent = m_oSelectedNode;
					else
						m_oTreeView.Nodes.Add(oCloneNode);

					return;
				}

				if (TreeViewDesigner.DataObject != null &&
					TreeViewDesigner.DataObject.GetDataPresent("PureComponents.TreeView.Design.TreeViewData"))
				{
					ICollection objects = m_oDesignerSerializationService.Deserialize(
						TreeViewDesigner.DataObject.GetData("PureComponents.TreeView.Design.TreeViewData"));

					TreeView oBaseTreeView = null;
					TreeView oTreeView = CreateTreeViewComponent();

					foreach (TreeView oTree in objects)
					{
						oBaseTreeView = oTree;
						break;
					}

					oTreeView.Copy(oBaseTreeView);

					foreach (Node oNode in oBaseTreeView.Nodes)
					{
						Node oCloneNode = CloneNode(oNode);
						oCloneNode.Copy(oNode);
						oCloneNode.SetNodeTreeViewReference(oTreeView, true);

						oTreeView.Nodes.Add(oCloneNode);
					}

					// add the TreeView to the form				
					Control oBaseControl = m_oDesignerHost.RootComponent as Control;
					oBaseControl.Controls.Add(oTreeView);

					// apply the style
					oTreeView.Style.ApplyStyle(oBaseTreeView.Style);
					oTreeView.Style.ApplyStyle(oBaseTreeView.Style);
					oTreeView.Style.NodeStyle.ApplyStyle(oBaseTreeView.Style.NodeStyle);

					// make the new component selected
					m_bSkipSelectionChange = true;
					m_oSelectionService.SetSelectedComponents(new TreeView[] {oTreeView}, SelectionTypes.Replace);
					m_bSkipSelectionChange = false;

					oTreeView.Invalidate();

					return;
				}

				m_oOldCmdPaste.Invoke();
			}
		}

		#endregion

		#region drag drop support

		protected override void OnDragEnter(System.Windows.Forms.DragEventArgs de)
		{
			base.OnDragEnter(de);

			de.Effect = DragDropEffects.Copy;
		}

		protected override void OnDragDrop(System.Windows.Forms.DragEventArgs de)
		{
			base.OnDragDrop(de);

			string[] aFormat = de.Data.GetFormats();

			foreach (string sFormat in aFormat)
			{
				object o = de.Data.GetData(sFormat);

				if (o != null)
				{
					ICollection oCollection = m_oDesignerSerializationService.Deserialize(o);

					try
					{
						foreach (object oControl in oCollection)
						{
							if (oControl is System.Windows.Forms.TreeView)
							{
								System.Windows.Forms.TreeView oView = oControl as System.Windows.Forms.TreeView;

								foreach (TreeNode oTreeNode in oView.Nodes)
									CopyTreeViewNode(oTreeNode);

								// set the checkboxes style
								m_oTreeView.CheckBoxes = oView.CheckBoxes;

								// notify about the component change
								try
								{
									RaiseComponentChanging(TypeDescriptor.GetProperties(typeof (Node))["Parent"]);
								}
								catch
								{
								}
								try
								{
									RaiseComponentChanged(TypeDescriptor.GetProperties(typeof (Node))["Parent"], null, null);
								}
								catch
								{
								}
							}
						}
					}
					catch (Exception ex)
					{
						System.Console.Out.WriteLine(ex.ToString());
					}

					break;
				}
			}

			// only TreeView objects can be added
//			if (bTreeViewAdded == false)
//				MessageBox.Show("You can only drop classes that derive from System.Windows.Forms.TreeView.", "TreeView Designer", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		#endregion

		protected override bool DrawGrid
		{
			get { return false; }

			set { base.DrawGrid = value; }
		}
	}

	#region DragDropPopup subcontrol

	/// <summary>
	/// The popup window implementation
	/// </summary>	
	internal sealed class DragDropPopup : System.Windows.Forms.Form
	{
		#region private members

		/// <summary>
		/// The drag node handler
		/// </summary>
		private Node m_oNode;

		internal bool CanDrop = false;
		internal bool IsCopy = false;

		private Hashtable m_mapItemBoxToRect = new Hashtable();
		private Hashtable m_mapRectToItemBox = new Hashtable();
		private Hashtable m_mapRectToSubItem = new Hashtable();
		private Hashtable m_mapSubItemToRect = new Hashtable();
		private Hashtable a = new Hashtable();
		private Hashtable b = new Hashtable();
		private Hashtable c = new Hashtable();
		private Hashtable d = new Hashtable();

		#endregion

		#region construction

		internal DragDropPopup(Node oNode)
		{
			m_oNode = oNode;

			// optimize scrolling - no flicker
			SetStyle(ControlStyles.DoubleBuffer, true);

			this.ControlBox = false;
			this.ShowInTaskbar = false;
			this.TopMost = true;
			this.FormBorderStyle = FormBorderStyle.None;

			this.Width = oNode.TreeView.Width;
			this.Height = 0;
			this.Opacity = 0.4;
		}

		#endregion

		#region painting

		/// <summary>
		/// The painting implementation
		/// </summary>
		/// <param name="e">Paint arguments</param>
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			if (CanDrop == false)
				this.BackColor = Color.Red;
			else
				this.BackColor = Color.White;

			if (m_oNode != null)
			{
				NodePainter oPainter = new NodePainter(m_oNode.TreeView);

				int nX = 15;
				int nY = 5;

				m_mapItemBoxToRect.Clear();
				m_mapRectToItemBox.Clear();
				m_mapRectToSubItem.Clear();
				m_mapSubItemToRect.Clear();
				a.Clear();
				b.Clear();
				c.Clear();
				d.Clear();

				oPainter.PaintNode(m_oNode, e.Graphics, ref nX, ref nY, m_oNode.TreeView.Width,
				                   ref m_mapSubItemToRect, ref m_mapRectToSubItem,
				                   ref m_mapRectToItemBox, ref m_mapItemBoxToRect,
				                   ref a, ref b, ref c, ref d);

				int nHeight = 0;
				foreach (Rectangle oRect in m_mapRectToSubItem.Keys)
					if (oRect.Top + oRect.Height > nHeight) nHeight = oRect.Top + oRect.Height;

				if (nHeight > 350)
					nHeight = 350;

				this.Height = nHeight;

				if (IsCopy == true)
				{
					e.Graphics.DrawLine(Pens.Black, 2, 5, 9, 5);
					e.Graphics.DrawLine(Pens.Black, 2, 6, 9, 6);
					e.Graphics.DrawLine(Pens.Black, 5, 2, 5, 9);
					e.Graphics.DrawLine(Pens.Black, 6, 2, 6, 9);
				}
			}
		}

		#endregion		

		#region event handlers

		protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = true;

			Visible = false;
		}

		#endregion								
	}

	#endregion	

	#region custom code dom serializer

	internal class NodeCodeDomSerializer : CodeDomSerializer
	{
		public override object Deserialize(IDesignerSerializationManager manager, object codeObject)
		{
			CodeDomSerializer baseClassSerializer = (CodeDomSerializer) manager.
				GetSerializer(typeof (Node).BaseType, typeof (CodeDomSerializer));

			return baseClassSerializer.Deserialize(manager, codeObject);
		}

		public override object Serialize(IDesignerSerializationManager manager, object value)
		{
			//bool bNodesExpessionFound = false;

			CodeDomSerializer baseClassSerializer = (CodeDomSerializer) manager.
				GetSerializer(typeof (Node).BaseType, typeof (CodeDomSerializer));

			object codeObject = baseClassSerializer.Serialize(manager, value);

			/*
			// type the value as node
			Node oNode = value as Node;
 
			// check the code statement collection for the nodes entry existency
			if (codeObject is CodeStatementCollection && oNode.Nodes.Count > 0) 
			{
				CodeStatementCollection statements = (CodeStatementCollection)codeObject;
	
				foreach (CodeStatement oStatement in statements)
				{
					if (oStatement is CodeAssignStatement)
					{
						// check that the CodeAssignStatement is the Nodes statement
						CodeAssignStatement oAssignStatement = oStatement as CodeAssignStatement;
						
						if (oAssignStatement.Left is CodePropertyReferenceExpression)
						{
							CodePropertyReferenceExpression oRefExpression = oAssignStatement.Left as CodePropertyReferenceExpression;

							if (oRefExpression.PropertyName == "Nodes")								
							{
								bNodesExpessionFound = true;
								break;
							}
						}
					}
				}

				if (bNodesExpessionFound == false)
				{
					CodeExpressionCollection aExpresion = new CodeExpressionCollection();

					foreach (Node oSubNode in oNode.Nodes)
					{
						aExpresion.Add(new CodeVariableReferenceExpression("aaa"));
					}
					
					CodeExpression [] aNodes = new CodeExpression[aExpresion.Count];
					aExpresion.CopyTo(aNodes, 0);

					// we have to add the nodes reference expression here
					CodeAssignStatement oNodesAssign = new CodeAssignStatement
					(
						new CodePropertyReferenceExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "Nodes"), "Nodes"),
						new CodeArrayCreateExpression("Node", aNodes)
					);

					statements.Add(oNodesAssign);					
				}				
			}*/

			return codeObject;
		}
	}

	#endregion
}