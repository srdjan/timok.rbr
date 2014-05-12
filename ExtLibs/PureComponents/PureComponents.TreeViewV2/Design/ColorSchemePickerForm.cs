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
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

using PureComponents.TreeView;

namespace PureComponents.TreeView.Design
{
	/// <summary>
	/// Summary description for ColorSchemePickerForm.
	/// </summary>
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	internal sealed class ColorSchemePickerForm : System.Windows.Forms.Form
	{	
		#region private members
		private IDesignerHost m_oDesignerHost;
		private TreeViewDesigner m_oComponentDesigner;
		private bool bApplied = false;

		private PureComponents.TreeView.TreeView m_oTreeView;

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RadioButton m_rbDefault;
		private System.Windows.Forms.RadioButton m_rbSky;
		private System.Windows.Forms.RadioButton m_rbOcean;
		private System.Windows.Forms.RadioButton m_rbForest;
		private System.Windows.Forms.RadioButton m_rbSunset;
		private System.Windows.Forms.RadioButton m_rbGold;
		private System.Windows.Forms.RadioButton m_rbWood;
		private System.Windows.Forms.RadioButton m_rbSilver;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.RadioButton m_rbRose;
		private PureComponents.TreeView.TreeView treeView1;
		private PureComponents.TreeView.Node Node1;
		private PureComponents.TreeView.Node Node2;
		private PureComponents.TreeView.Node Node3;
		private PureComponents.TreeView.Node Node4;
		private PureComponents.TreeView.Node Node5;
		private PureComponents.TreeView.Node Node6;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox m_chkTreeViewStyle;
		private System.Windows.Forms.CheckBox m_chkNodeStyle;
		private System.Windows.Forms.CheckBox m_chkExpandBoxStyle;
		private System.Windows.Forms.CheckBox m_chkCheckBoxStyle;
		private System.Windows.Forms.CheckBox m_chkTooltipStyle;
		private System.Windows.Forms.Label label3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		#region construction
		public ColorSchemePickerForm(TreeView oTreeView, IDesignerHost oDesignerHost, TreeViewDesigner oDesigner)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			m_oTreeView = oTreeView;
			m_oDesignerHost = oDesignerHost;
			m_oComponentDesigner = oDesigner;
			
			treeView1.Style.BorderStyle = m_oTreeView.Style.BorderStyle;		
			treeView1.Style.AutoCollapse = m_oTreeView.Style.AutoCollapse;
			treeView1.CheckBoxes = m_oTreeView.CheckBoxes;
			treeView1.Style.FlashColor = m_oTreeView.Style.FlashColor;
			treeView1.Style.HighlightSelectedPath = m_oTreeView.Style.HighlightSelectedPath;
			treeView1.Style.LineStyle = m_oTreeView.Style.LineStyle;
			treeView1.Style.ShowLines = m_oTreeView.Style.ShowLines;
			treeView1.Style.ShowPlusMinus = m_oTreeView.Style.ShowPlusMinus;
			treeView1.Style.ShowSubitemsIndicator = m_oTreeView.Style.ShowSubitemsIndicator;
			treeView1.Style.TrackNodeHover = m_oTreeView.Style.TrackNodeHover;			
			treeView1.Style.NodeStyle.SelectedFillStyle = m_oTreeView.Style.NodeStyle.SelectedFillStyle;	
	
			treeView1.Style.ApplyStyle(m_oTreeView.Style);
			treeView1.Style.NodeStyle.ApplyStyle(m_oTreeView.Style.NodeStyle);
			
			treeView1.ExpandAll();
			treeView1.Nodes[0].Nodes[0].Nodes[1].Select();
			treeView1.Refresh();			

			Node1.Tag = "color nodes";
			Node2.Tag = "color nodes";
			Node3.Tag = "color nodes";
			Node4.Tag = "color nodes";
			Node5.Tag = "color nodes";
			Node6.Tag = "color nodes";
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{			
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ColorSchemePickerForm));
			PureComponents.TreeView.ContextMenuStrings contextMenuStrings1 = new PureComponents.TreeView.ContextMenuStrings();
			PureComponents.TreeView.TreeViewStyle treeViewStyle1 = new PureComponents.TreeView.TreeViewStyle();
			PureComponents.TreeView.NodeStyle nodeStyle1 = new PureComponents.TreeView.NodeStyle();
			PureComponents.TreeView.CheckBoxStyle checkBoxStyle1 = new PureComponents.TreeView.CheckBoxStyle();
			PureComponents.TreeView.ExpandBoxStyle expandBoxStyle1 = new PureComponents.TreeView.ExpandBoxStyle();
			PureComponents.TreeView.NodeTooltipStyle nodeTooltipStyle1 = new PureComponents.TreeView.NodeTooltipStyle();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.button3 = new System.Windows.Forms.Button();
			this.m_rbDefault = new System.Windows.Forms.RadioButton();
			this.m_rbSky = new System.Windows.Forms.RadioButton();
			this.m_rbOcean = new System.Windows.Forms.RadioButton();
			this.m_rbForest = new System.Windows.Forms.RadioButton();
			this.m_rbSunset = new System.Windows.Forms.RadioButton();
			this.m_rbRose = new System.Windows.Forms.RadioButton();
			this.m_rbGold = new System.Windows.Forms.RadioButton();
			this.m_rbWood = new System.Windows.Forms.RadioButton();
			this.m_rbSilver = new System.Windows.Forms.RadioButton();
			this.treeView1 = new PureComponents.TreeView.TreeView();
			this.Node1 = new PureComponents.TreeView.Node();
			this.Node2 = new PureComponents.TreeView.Node();
			this.Node3 = new PureComponents.TreeView.Node();
			this.Node4 = new PureComponents.TreeView.Node();
			this.Node5 = new PureComponents.TreeView.Node();
			this.Node6 = new PureComponents.TreeView.Node();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.m_chkTooltipStyle = new System.Windows.Forms.CheckBox();
			this.m_chkCheckBoxStyle = new System.Windows.Forms.CheckBox();
			this.m_chkExpandBoxStyle = new System.Windows.Forms.CheckBox();
			this.m_chkNodeStyle = new System.Windows.Forms.CheckBox();
			this.m_chkTreeViewStyle = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Location = new System.Drawing.Point(216, 6);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 24);
			this.button1.TabIndex = 3;
			this.button1.Text = "OK";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button2.Location = new System.Drawing.Point(296, 6);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 24);
			this.button2.TabIndex = 3;
			this.button2.Text = "Cancel";
			// 
			// panel1
			// 
			this.panel1.BackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("panel1.BackgroundImage")));
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.label1,
																				 this.label3});
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(458, 88);
			this.panel1.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.label1.Location = new System.Drawing.Point(88, 48);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(368, 32);
			this.label1.TabIndex = 4;
			this.label1.Text = "Choose the color scheme, you can review the result immediately in the TreeView\'s " +
				"preview.";
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(238)));
			this.label3.ForeColor = System.Drawing.Color.White;
			this.label3.Location = new System.Drawing.Point(88, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(232, 24);
			this.label3.TabIndex = 11;
			this.label3.Text = "Color Scheme Picker";
			// 
			// panel2
			// 
			this.panel2.BackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("panel2.BackgroundImage")));
			this.panel2.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.button1,
																				 this.button2,
																				 this.button3});
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 306);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(458, 36);
			this.panel2.TabIndex = 5;
			// 
			// button3
			// 
			this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button3.Location = new System.Drawing.Point(376, 6);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 24);
			this.button3.TabIndex = 3;
			this.button3.Text = "Apply";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// m_rbDefault
			// 
			this.m_rbDefault.Checked = true;
			this.m_rbDefault.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.m_rbDefault.Location = new System.Drawing.Point(16, 16);
			this.m_rbDefault.Name = "m_rbDefault";
			this.m_rbDefault.Size = new System.Drawing.Size(72, 24);
			this.m_rbDefault.TabIndex = 7;
			this.m_rbDefault.TabStop = true;
			this.m_rbDefault.Text = "Default";
			this.m_rbDefault.CheckedChanged += new System.EventHandler(this.m_rbDefault_CheckedChanged);
			// 
			// m_rbSky
			// 
			this.m_rbSky.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.m_rbSky.Location = new System.Drawing.Point(16, 40);
			this.m_rbSky.Name = "m_rbSky";
			this.m_rbSky.Size = new System.Drawing.Size(72, 16);
			this.m_rbSky.TabIndex = 7;
			this.m_rbSky.Text = "Sky";
			this.m_rbSky.CheckedChanged += new System.EventHandler(this.m_rbSky_CheckedChanged);
			// 
			// m_rbOcean
			// 
			this.m_rbOcean.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.m_rbOcean.Location = new System.Drawing.Point(16, 56);
			this.m_rbOcean.Name = "m_rbOcean";
			this.m_rbOcean.Size = new System.Drawing.Size(72, 24);
			this.m_rbOcean.TabIndex = 7;
			this.m_rbOcean.Text = "Ocean";
			this.m_rbOcean.CheckedChanged += new System.EventHandler(this.m_rbOcean_CheckedChanged);
			// 
			// m_rbForest
			// 
			this.m_rbForest.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.m_rbForest.Location = new System.Drawing.Point(16, 79);
			this.m_rbForest.Name = "m_rbForest";
			this.m_rbForest.Size = new System.Drawing.Size(72, 16);
			this.m_rbForest.TabIndex = 7;
			this.m_rbForest.Text = "Forest";
			this.m_rbForest.CheckedChanged += new System.EventHandler(this.m_rbForest_CheckedChanged);
			// 
			// m_rbSunset
			// 
			this.m_rbSunset.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.m_rbSunset.Location = new System.Drawing.Point(16, 96);
			this.m_rbSunset.Name = "m_rbSunset";
			this.m_rbSunset.Size = new System.Drawing.Size(72, 24);
			this.m_rbSunset.TabIndex = 7;
			this.m_rbSunset.Text = "Sunset";
			this.m_rbSunset.CheckedChanged += new System.EventHandler(this.m_rbSunset_CheckedChanged);
			// 
			// m_rbRose
			// 
			this.m_rbRose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.m_rbRose.Location = new System.Drawing.Point(16, 112);
			this.m_rbRose.Name = "m_rbRose";
			this.m_rbRose.Size = new System.Drawing.Size(72, 32);
			this.m_rbRose.TabIndex = 7;
			this.m_rbRose.Text = "Rose";
			this.m_rbRose.CheckedChanged += new System.EventHandler(this.m_rbRose_CheckedChanged);
			// 
			// m_rbGold
			// 
			this.m_rbGold.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.m_rbGold.Location = new System.Drawing.Point(16, 136);
			this.m_rbGold.Name = "m_rbGold";
			this.m_rbGold.Size = new System.Drawing.Size(72, 24);
			this.m_rbGold.TabIndex = 7;
			this.m_rbGold.Text = "Gold";
			this.m_rbGold.CheckedChanged += new System.EventHandler(this.m_rbGold_CheckedChanged);
			// 
			// m_rbWood
			// 
			this.m_rbWood.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.m_rbWood.Location = new System.Drawing.Point(16, 152);
			this.m_rbWood.Name = "m_rbWood";
			this.m_rbWood.Size = new System.Drawing.Size(72, 32);
			this.m_rbWood.TabIndex = 7;
			this.m_rbWood.Text = "Wood";
			this.m_rbWood.CheckedChanged += new System.EventHandler(this.m_rbWood_CheckedChanged);
			// 
			// m_rbSilver
			// 
			this.m_rbSilver.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.m_rbSilver.Location = new System.Drawing.Point(16, 176);
			this.m_rbSilver.Name = "m_rbSilver";
			this.m_rbSilver.Size = new System.Drawing.Size(72, 24);
			this.m_rbSilver.TabIndex = 7;
			this.m_rbSilver.Text = "Silver";
			this.m_rbSilver.CheckedChanged += new System.EventHandler(this.m_rbSilver_CheckedChanged);
			// 
			// treeView1
			// 
			this.treeView1.AllowAdding = true;
			this.treeView1.AllowArranging = true;
			this.treeView1.AllowDeleting = true;
			this.treeView1.AllowEditing = true;
			this.treeView1.AutoDragDrop = true;
			this.treeView1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.treeView1.CheckBoxes = false;
			this.treeView1.ContextMenuArranging = false;
			this.treeView1.ContextMenuEditing = false;
			contextMenuStrings1.AddNode = "Add Node";
			contextMenuStrings1.Collapse = "Collapse";
			contextMenuStrings1.Copy = "Copy";
			contextMenuStrings1.DeleteNode = "Delete Node";
			contextMenuStrings1.EditNode = "Edit Node";
			contextMenuStrings1.Expand = "Expand";
			contextMenuStrings1.LoadXml = "Load Xml...";
			contextMenuStrings1.MoveBottom = "Move Bottom";
			contextMenuStrings1.MoveDown = "Move Down";
			contextMenuStrings1.MoveLeft = "Move Left";
			contextMenuStrings1.MoveRight = "Move Right";
			contextMenuStrings1.MoveTop = "Move Top";
			contextMenuStrings1.MoveUp = "Move Up";
			contextMenuStrings1.Paste = "Paste";
			contextMenuStrings1.SaveXml = "Save Xml...";
			this.treeView1.ContextMenuStrings = contextMenuStrings1;
			this.treeView1.ContextMenuXmlOperations = false;
			this.treeView1.ExpandOnDblClick = true;
			this.treeView1.Flags = false;
			this.treeView1.HideSelection = false;
			this.treeView1.ImageList = null;
			this.treeView1.LabelEdit = true;
			this.treeView1.Location = new System.Drawing.Point(280, 112);
			this.treeView1.Multiline = true;
			this.treeView1.Name = "treeView1";
			this.treeView1.NodeAutoNumbering = false;
			this.treeView1.Nodes.AddRange(new PureComponents.TreeView.Node[] {
																				 this.Node1});
			this.treeView1.PathSeparator = "\\";
			this.treeView1.Size = new System.Drawing.Size(152, 187);
			this.treeView1.Sorted = false;
			treeViewStyle1.AutoCollapse = false;
			treeViewStyle1.BackColor = System.Drawing.Color.White;
			treeViewStyle1.BorderColor = System.Drawing.Color.Black;
			treeViewStyle1.BorderStyle = PureComponents.TreeView.BorderStyle.Solid;
			treeViewStyle1.FadeColor = System.Drawing.Color.White;
			treeViewStyle1.FillStyle = PureComponents.TreeView.FillStyle.Flat;
			treeViewStyle1.FlashColor = System.Drawing.Color.Red;
			treeViewStyle1.HighlightSelectedPath = false;
			treeViewStyle1.LineColor = System.Drawing.Color.Gray;
			treeViewStyle1.LineStyle = PureComponents.TreeView.LineStyle.Dot;
			checkBoxStyle1.BackColor = System.Drawing.Color.White;
			checkBoxStyle1.BorderColor = System.Drawing.Color.Black;
			checkBoxStyle1.BorderStyle = PureComponents.TreeView.CheckBoxBorderStyle.Solid;
			checkBoxStyle1.CheckColor = System.Drawing.Color.Black;
			checkBoxStyle1.HoverBackColor = System.Drawing.Color.White;
			checkBoxStyle1.HoverBorderColor = System.Drawing.Color.Black;
			checkBoxStyle1.HoverCheckColor = System.Drawing.Color.Black;
			nodeStyle1.CheckBoxStyle = checkBoxStyle1;
			expandBoxStyle1.BackColor = System.Drawing.Color.SkyBlue;
			expandBoxStyle1.BorderColor = System.Drawing.Color.Black;
			expandBoxStyle1.ForeColor = System.Drawing.Color.Black;
			expandBoxStyle1.Shape = PureComponents.TreeView.ExpandBoxShape.XP;
			nodeStyle1.ExpandBoxStyle = expandBoxStyle1;
			nodeStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			nodeStyle1.ForeColor = System.Drawing.Color.Black;
			nodeStyle1.HoverBackColor = System.Drawing.Color.LightGray;
			nodeStyle1.HoverForeColor = System.Drawing.Color.Black;
			nodeStyle1.Parent = null;
			nodeStyle1.SelectedBackColor = System.Drawing.Color.FromArgb(((System.Byte)(183)), ((System.Byte)(175)), ((System.Byte)(255)));
			nodeStyle1.SelectedBorderColor = System.Drawing.Color.FromArgb(((System.Byte)(135)), ((System.Byte)(127)), ((System.Byte)(255)));
			nodeStyle1.SelectedFillStyle = PureComponents.TreeView.FillStyle.Flat;
			nodeStyle1.SelectedForeColor = System.Drawing.Color.Black;
			nodeTooltipStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
			nodeTooltipStyle1.BorderColor = System.Drawing.Color.FromArgb(((System.Byte)(170)), ((System.Byte)(142)), ((System.Byte)(125)));
			nodeTooltipStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			nodeTooltipStyle1.ForeColor = System.Drawing.Color.Black;
			nodeStyle1.TooltipStyle = nodeTooltipStyle1;
			nodeStyle1.UnderlineColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(0)), ((System.Byte)(0)));
			nodeStyle1.UnderlineStyle = PureComponents.TreeView.UnderlineStyle.Solid;
			treeViewStyle1.NodeStyle = nodeStyle1;
			treeViewStyle1.ShowLines = true;
			treeViewStyle1.ShowPlusMinus = true;
			treeViewStyle1.ShowSubitemsIndicator = false;
			treeViewStyle1.TrackNodeHover = true;
			this.treeView1.Style = treeViewStyle1;
			this.treeView1.TabIndex = 8;
			this.treeView1.TooltipPopDelay = 1500;
			// 
			// Node1
			// 
			this.Node1._TreeView = this.treeView1;
			this.Node1.CheckBoxVisible = true;
			this.Node1.Checked = false;
			this.Node1.ContextMenu = null;
			this.Node1.ContextMenuSource = PureComponents.TreeView.ContextMenuSource.None;
			this.Node1.Flag = null;
			this.Node1.FlagVisible = false;
			this.Node1.Image = null;
			this.Node1.IsEditing = false;
			this.Node1.Key = null;
			this.Node1.NodeStyle = null;
			this.Node1.NodeStyleSource = PureComponents.TreeView.NodeStyleSource.Default;
			this.Node1.Parent = null;
			this.Node1.Tag = null;
			this.Node1.Text = "Node1";
			this.Node1.Tooltip = null;
			this.Node1.TreeView = this.treeView1;
			this.Node1.Underline = false;
			this.Node1.Visible = true;
			this.Node1.YOrder = 0;
			// 
			// Node2
			// 
			this.Node2._TreeView = this.treeView1;
			this.Node2.CheckBoxVisible = true;
			this.Node2.Checked = false;
			this.Node2.ContextMenu = null;
			this.Node2.ContextMenuSource = PureComponents.TreeView.ContextMenuSource.None;
			this.Node2.Flag = null;
			this.Node2.FlagVisible = false;
			this.Node2.Image = null;
			this.Node2.IsEditing = false;
			this.Node2.Key = null;
			this.Node2.NodeStyle = null;
			this.Node2.NodeStyleSource = PureComponents.TreeView.NodeStyleSource.Default;
			this.Node2.Parent = this.Node1;
			this.Node2.Tag = null;
			this.Node2.Text = "Node2";
			this.Node2.Tooltip = null;
			this.Node2.TreeView = this.treeView1;
			this.Node2.Underline = false;
			this.Node2.Visible = true;
			this.Node2.YOrder = 0;
			// 
			// Node3
			// 
			this.Node3._TreeView = this.treeView1;
			this.Node3.CheckBoxVisible = true;
			this.Node3.Checked = false;
			this.Node3.ContextMenu = null;
			this.Node3.ContextMenuSource = PureComponents.TreeView.ContextMenuSource.None;
			this.Node3.Flag = null;
			this.Node3.FlagVisible = false;
			this.Node3.Image = null;
			this.Node3.IsEditing = false;
			this.Node3.Key = null;
			this.Node3.NodeStyle = null;
			this.Node3.NodeStyleSource = PureComponents.TreeView.NodeStyleSource.Default;
			this.Node3.Parent = this.Node2;
			this.Node3.Tag = null;
			this.Node3.Text = "Node3";
			this.Node3.Tooltip = null;
			this.Node3.TreeView = this.treeView1;
			this.Node3.Underline = false;
			this.Node3.Visible = true;
			this.Node3.YOrder = 0;
			// 
			// Node4
			// 
			this.Node4._TreeView = this.treeView1;
			this.Node4.CheckBoxVisible = true;
			this.Node4.Checked = false;
			this.Node4.ContextMenu = null;
			this.Node4.ContextMenuSource = PureComponents.TreeView.ContextMenuSource.None;
			this.Node4.Flag = null;
			this.Node4.FlagVisible = false;
			this.Node4.Image = null;
			this.Node4.IsEditing = false;
			this.Node4.Key = null;
			this.Node4.NodeStyle = null;
			this.Node4.NodeStyleSource = PureComponents.TreeView.NodeStyleSource.Default;
			this.Node4.Parent = this.Node2;
			this.Node4.Tag = null;
			this.Node4.Text = "Node4";
			this.Node4.Tooltip = null;
			this.Node4.TreeView = this.treeView1;
			this.Node4.Underline = false;
			this.Node4.Visible = true;
			this.Node4.YOrder = 1;
			// 
			// Node5
			// 
			this.Node5._TreeView = this.treeView1;
			this.Node5.CheckBoxVisible = true;
			this.Node5.Checked = false;
			this.Node5.ContextMenu = null;
			this.Node5.ContextMenuSource = PureComponents.TreeView.ContextMenuSource.None;
			this.Node5.Flag = null;
			this.Node5.FlagVisible = false;
			this.Node5.Image = null;
			this.Node5.IsEditing = false;
			this.Node5.Key = null;
			this.Node5.NodeStyle = null;
			this.Node5.NodeStyleSource = PureComponents.TreeView.NodeStyleSource.Default;
			this.Node5.Parent = this.Node2;
			this.Node5.Tag = null;
			this.Node5.Text = "Node5";
			this.Node5.Tooltip = null;
			this.Node5.TreeView = this.treeView1;
			this.Node5.Underline = false;
			this.Node5.Visible = true;
			this.Node5.YOrder = 2;
			// 
			// Node6
			// 
			this.Node6._TreeView = this.treeView1;
			this.Node6.CheckBoxVisible = true;
			this.Node6.Checked = false;
			this.Node6.ContextMenu = null;
			this.Node6.ContextMenuSource = PureComponents.TreeView.ContextMenuSource.None;
			this.Node6.Flag = null;
			this.Node6.FlagVisible = false;
			this.Node6.Image = null;
			this.Node6.IsEditing = false;
			this.Node6.Key = null;
			this.Node6.NodeStyle = null;
			this.Node6.NodeStyleSource = PureComponents.TreeView.NodeStyleSource.Default;
			this.Node6.Parent = this.Node4;
			this.Node6.Tag = null;
			this.Node6.Text = "Node6";
			this.Node6.Tooltip = null;
			this.Node6.TreeView = this.treeView1;
			this.Node6.Underline = false;
			this.Node6.Visible = true;
			this.Node6.YOrder = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.m_rbSunset,
																					this.m_rbGold,
																					this.m_rbSilver,
																					this.m_rbSky,
																					this.m_rbForest,
																					this.m_rbWood,
																					this.m_rbDefault,
																					this.m_rbOcean,
																					this.m_rbRose});
			this.groupBox1.Location = new System.Drawing.Point(8, 92);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(112, 208);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Color scheme";
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.label2.Location = new System.Drawing.Point(280, 95);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 16);
			this.label2.TabIndex = 5;
			this.label2.Text = "Preview";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.m_chkTooltipStyle,
																					this.m_chkCheckBoxStyle,
																					this.m_chkExpandBoxStyle,
																					this.m_chkNodeStyle,
																					this.m_chkTreeViewStyle});
			this.groupBox2.Location = new System.Drawing.Point(129, 92);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(143, 208);
			this.groupBox2.TabIndex = 10;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Apply to";
			// 
			// m_chkTooltipStyle
			// 
			this.m_chkTooltipStyle.Checked = true;
			this.m_chkTooltipStyle.CheckState = System.Windows.Forms.CheckState.Checked;
			this.m_chkTooltipStyle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.m_chkTooltipStyle.Location = new System.Drawing.Point(5, 100);
			this.m_chkTooltipStyle.Name = "m_chkTooltipStyle";
			this.m_chkTooltipStyle.Size = new System.Drawing.Size(130, 24);
			this.m_chkTooltipStyle.TabIndex = 15;
			this.m_chkTooltipStyle.Text = "TooltipStyle";
			// 
			// m_chkCheckBoxStyle
			// 
			this.m_chkCheckBoxStyle.Checked = true;
			this.m_chkCheckBoxStyle.CheckState = System.Windows.Forms.CheckState.Checked;
			this.m_chkCheckBoxStyle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.m_chkCheckBoxStyle.Location = new System.Drawing.Point(5, 79);
			this.m_chkCheckBoxStyle.Name = "m_chkCheckBoxStyle";
			this.m_chkCheckBoxStyle.Size = new System.Drawing.Size(130, 24);
			this.m_chkCheckBoxStyle.TabIndex = 14;
			this.m_chkCheckBoxStyle.Text = "CheckBoxStyle";
			// 
			// m_chkExpandBoxStyle
			// 
			this.m_chkExpandBoxStyle.Checked = true;
			this.m_chkExpandBoxStyle.CheckState = System.Windows.Forms.CheckState.Checked;
			this.m_chkExpandBoxStyle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.m_chkExpandBoxStyle.Location = new System.Drawing.Point(5, 59);
			this.m_chkExpandBoxStyle.Name = "m_chkExpandBoxStyle";
			this.m_chkExpandBoxStyle.Size = new System.Drawing.Size(130, 24);
			this.m_chkExpandBoxStyle.TabIndex = 13;
			this.m_chkExpandBoxStyle.Text = "ExpandBoxStyle";
			// 
			// m_chkNodeStyle
			// 
			this.m_chkNodeStyle.Checked = true;
			this.m_chkNodeStyle.CheckState = System.Windows.Forms.CheckState.Checked;
			this.m_chkNodeStyle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.m_chkNodeStyle.Location = new System.Drawing.Point(5, 40);
			this.m_chkNodeStyle.Name = "m_chkNodeStyle";
			this.m_chkNodeStyle.Size = new System.Drawing.Size(130, 24);
			this.m_chkNodeStyle.TabIndex = 12;
			this.m_chkNodeStyle.Text = "NodeStyle";
			// 
			// m_chkTreeViewStyle
			// 
			this.m_chkTreeViewStyle.Checked = true;
			this.m_chkTreeViewStyle.CheckState = System.Windows.Forms.CheckState.Checked;
			this.m_chkTreeViewStyle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.m_chkTreeViewStyle.Location = new System.Drawing.Point(5, 22);
			this.m_chkTreeViewStyle.Name = "m_chkTreeViewStyle";
			this.m_chkTreeViewStyle.Size = new System.Drawing.Size(130, 24);
			this.m_chkTreeViewStyle.TabIndex = 11;
			this.m_chkTreeViewStyle.Text = "TreeViewStyle";
			// 
			// ColorSchemePickerForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.ClientSize = new System.Drawing.Size(458, 342);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.treeView1,
																		  this.panel2,
																		  this.panel1,
																		  this.groupBox1,
																		  this.label2,
																		  this.groupBox2});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "ColorSchemePickerForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Color Scheme Picker";
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion		

		#region events handlers
		private void m_rbDefault_CheckedChanged(object sender, System.EventArgs e)
		{
			bApplied = false;
			TreeViewStyleFactory oFactory = TreeViewStyleFactory.GetInstance();

			TreeViewStyle style = oFactory.GetDefaultTreeStyle();
			if (m_chkTreeViewStyle.Checked == true)
				treeView1.Style.ApplyStyle(style);
			if (m_chkTooltipStyle.Checked == true)
				treeView1.Style.NodeStyle.TooltipStyle.ApplyStyle(style.NodeStyle.TooltipStyle);
			if (m_chkNodeStyle.Checked == true)
				treeView1.Style.NodeStyle.ApplyStyleShallow(style.NodeStyle);
			if (m_chkExpandBoxStyle.Checked == true)
				treeView1.Style.NodeStyle.ExpandBoxStyle.ApplyStyle(style.NodeStyle.ExpandBoxStyle);
			if (m_chkCheckBoxStyle.Checked == true)
				treeView1.Style.NodeStyle.CheckBoxStyle.ApplyStyle(style.NodeStyle.CheckBoxStyle);

			treeView1.Style.BorderStyle = m_oTreeView.Style.BorderStyle;		
			treeView1.Style.AutoCollapse = m_oTreeView.Style.AutoCollapse;
			treeView1.CheckBoxes = m_oTreeView.CheckBoxes;
			treeView1.Style.FlashColor = m_oTreeView.Style.FlashColor;
			treeView1.Style.HighlightSelectedPath = m_oTreeView.Style.HighlightSelectedPath;
			treeView1.Style.LineStyle = m_oTreeView.Style.LineStyle;
			treeView1.Style.ShowLines = m_oTreeView.Style.ShowLines;
			treeView1.Style.ShowPlusMinus = m_oTreeView.Style.ShowPlusMinus;
			treeView1.Style.ShowSubitemsIndicator = m_oTreeView.Style.ShowSubitemsIndicator;
			treeView1.Style.TrackNodeHover = m_oTreeView.Style.TrackNodeHover;			
			treeView1.Style.NodeStyle.SelectedFillStyle = m_oTreeView.Style.NodeStyle.SelectedFillStyle;		

			Node [] aNodes = treeView1.GetNodesByTag("color nodes");

			foreach (Node oNode in aNodes)
			{
				if (oNode.NodeStyle != null)
				{
					oNode.NodeStyle.ForeColor = treeView1.Style.NodeStyle.ForeColor;
					oNode.NodeStyle.SelectedBackColor = treeView1.Style.NodeStyle.SelectedBackColor;
					oNode.NodeStyle.SelectedForeColor = treeView1.Style.NodeStyle.SelectedForeColor;
					oNode.NodeStyle.SelectedBorderColor = treeView1.Style.NodeStyle.SelectedBorderColor;
				}
			}

			treeView1.Invalidate();
		}

		private void m_rbSky_CheckedChanged(object sender, System.EventArgs e)
		{
			bApplied = false;
			TreeViewStyleFactory oFactory = TreeViewStyleFactory.GetInstance();
			
			TreeViewStyle style = oFactory.GetSkyTreeStyle();	
			if (m_chkTreeViewStyle.Checked == true)
				treeView1.Style.ApplyStyle(style);
			if (m_chkTooltipStyle.Checked == true)
				treeView1.Style.NodeStyle.TooltipStyle.ApplyStyle(style.NodeStyle.TooltipStyle);
			if (m_chkNodeStyle.Checked == true)
				treeView1.Style.NodeStyle.ApplyStyleShallow(style.NodeStyle);
			if (m_chkExpandBoxStyle.Checked == true)
				treeView1.Style.NodeStyle.ExpandBoxStyle.ApplyStyle(style.NodeStyle.ExpandBoxStyle);
			if (m_chkCheckBoxStyle.Checked == true)
				treeView1.Style.NodeStyle.CheckBoxStyle.ApplyStyle(style.NodeStyle.CheckBoxStyle);

			treeView1.Style.BorderStyle = m_oTreeView.Style.BorderStyle;		
			treeView1.Style.AutoCollapse = m_oTreeView.Style.AutoCollapse;
			treeView1.CheckBoxes = m_oTreeView.CheckBoxes;
			treeView1.Style.FlashColor = m_oTreeView.Style.FlashColor;
			treeView1.Style.HighlightSelectedPath = m_oTreeView.Style.HighlightSelectedPath;
			treeView1.Style.LineStyle = m_oTreeView.Style.LineStyle;
			treeView1.Style.ShowLines = m_oTreeView.Style.ShowLines;
			treeView1.Style.ShowPlusMinus = m_oTreeView.Style.ShowPlusMinus;
			treeView1.Style.ShowSubitemsIndicator = m_oTreeView.Style.ShowSubitemsIndicator;
			treeView1.Style.TrackNodeHover = m_oTreeView.Style.TrackNodeHover;			
			treeView1.Style.NodeStyle.SelectedFillStyle = m_oTreeView.Style.NodeStyle.SelectedFillStyle;		

			Node [] aNodes = treeView1.GetNodesByTag("color nodes");

			foreach (Node oNode in aNodes)
			{
				if (oNode.NodeStyle != null)
				{
					oNode.NodeStyle.ForeColor = treeView1.Style.NodeStyle.ForeColor;
					oNode.NodeStyle.SelectedBackColor = treeView1.Style.NodeStyle.SelectedBackColor;
					oNode.NodeStyle.SelectedForeColor = treeView1.Style.NodeStyle.SelectedForeColor;
					oNode.NodeStyle.SelectedBorderColor = treeView1.Style.NodeStyle.SelectedBorderColor;
				}
			}

			treeView1.Invalidate();
		}

		private void m_rbOcean_CheckedChanged(object sender, System.EventArgs e)
		{
			bApplied = false;
			TreeViewStyleFactory oFactory = TreeViewStyleFactory.GetInstance();

			TreeViewStyle style = oFactory.GetOceanTreeStyle();	
			if (m_chkTreeViewStyle.Checked == true)
				treeView1.Style.ApplyStyle(style);
			if (m_chkTooltipStyle.Checked == true)
				treeView1.Style.NodeStyle.TooltipStyle.ApplyStyle(style.NodeStyle.TooltipStyle);
			if (m_chkNodeStyle.Checked == true)
				treeView1.Style.NodeStyle.ApplyStyleShallow(style.NodeStyle);
			if (m_chkExpandBoxStyle.Checked == true)
				treeView1.Style.NodeStyle.ExpandBoxStyle.ApplyStyle(style.NodeStyle.ExpandBoxStyle);
			if (m_chkCheckBoxStyle.Checked == true)
				treeView1.Style.NodeStyle.CheckBoxStyle.ApplyStyle(style.NodeStyle.CheckBoxStyle);

			treeView1.Style.BorderStyle = m_oTreeView.Style.BorderStyle;		
			treeView1.Style.AutoCollapse = m_oTreeView.Style.AutoCollapse;
			treeView1.CheckBoxes = m_oTreeView.CheckBoxes;
			treeView1.Style.FlashColor = m_oTreeView.Style.FlashColor;
			treeView1.Style.HighlightSelectedPath = m_oTreeView.Style.HighlightSelectedPath;
			treeView1.Style.LineStyle = m_oTreeView.Style.LineStyle;
			treeView1.Style.ShowLines = m_oTreeView.Style.ShowLines;
			treeView1.Style.ShowPlusMinus = m_oTreeView.Style.ShowPlusMinus;
			treeView1.Style.ShowSubitemsIndicator = m_oTreeView.Style.ShowSubitemsIndicator;
			treeView1.Style.TrackNodeHover = m_oTreeView.Style.TrackNodeHover;			
			treeView1.Style.NodeStyle.SelectedFillStyle = m_oTreeView.Style.NodeStyle.SelectedFillStyle;		

			Node [] aNodes = treeView1.GetNodesByTag("color nodes");

			foreach (Node oNode in aNodes)
			{
				if (oNode.NodeStyle != null)
				{
					oNode.NodeStyle.ForeColor = treeView1.Style.NodeStyle.ForeColor;
					oNode.NodeStyle.SelectedBackColor = treeView1.Style.NodeStyle.SelectedBackColor;
					oNode.NodeStyle.SelectedForeColor = treeView1.Style.NodeStyle.SelectedForeColor;
					oNode.NodeStyle.SelectedBorderColor = treeView1.Style.NodeStyle.SelectedBorderColor;
				}
			}

			treeView1.Invalidate();		
		}

		private void m_rbForest_CheckedChanged(object sender, System.EventArgs e)
		{
			bApplied = false;
			TreeViewStyleFactory oFactory = TreeViewStyleFactory.GetInstance();

			TreeViewStyle style = oFactory.GetForestTreeStyle();	
			if (m_chkTreeViewStyle.Checked == true)
				treeView1.Style.ApplyStyle(style);
			if (m_chkTooltipStyle.Checked == true)
				treeView1.Style.NodeStyle.TooltipStyle.ApplyStyle(style.NodeStyle.TooltipStyle);
			if (m_chkNodeStyle.Checked == true)
				treeView1.Style.NodeStyle.ApplyStyleShallow(style.NodeStyle);
			if (m_chkExpandBoxStyle.Checked == true)
				treeView1.Style.NodeStyle.ExpandBoxStyle.ApplyStyle(style.NodeStyle.ExpandBoxStyle);
			if (m_chkCheckBoxStyle.Checked == true)
				treeView1.Style.NodeStyle.CheckBoxStyle.ApplyStyle(style.NodeStyle.CheckBoxStyle);		

			treeView1.Style.BorderStyle = m_oTreeView.Style.BorderStyle;		
			treeView1.Style.AutoCollapse = m_oTreeView.Style.AutoCollapse;
			treeView1.CheckBoxes = m_oTreeView.CheckBoxes;
			treeView1.Style.FlashColor = m_oTreeView.Style.FlashColor;
			treeView1.Style.HighlightSelectedPath = m_oTreeView.Style.HighlightSelectedPath;
			treeView1.Style.LineStyle = m_oTreeView.Style.LineStyle;
			treeView1.Style.ShowLines = m_oTreeView.Style.ShowLines;
			treeView1.Style.ShowPlusMinus = m_oTreeView.Style.ShowPlusMinus;
			treeView1.Style.ShowSubitemsIndicator = m_oTreeView.Style.ShowSubitemsIndicator;
			treeView1.Style.TrackNodeHover = m_oTreeView.Style.TrackNodeHover;			
			treeView1.Style.NodeStyle.SelectedFillStyle = m_oTreeView.Style.NodeStyle.SelectedFillStyle;		

			Node [] aNodes = treeView1.GetNodesByTag("color nodes");

			foreach (Node oNode in aNodes)
			{
				if (oNode.NodeStyle != null)
				{
					oNode.NodeStyle.ForeColor = treeView1.Style.NodeStyle.ForeColor;
					oNode.NodeStyle.SelectedBackColor = treeView1.Style.NodeStyle.SelectedBackColor;
					oNode.NodeStyle.SelectedForeColor = treeView1.Style.NodeStyle.SelectedForeColor;
					oNode.NodeStyle.SelectedBorderColor = treeView1.Style.NodeStyle.SelectedBorderColor;
				}
			}

			treeView1.Invalidate();
		}

		private void m_rbSunset_CheckedChanged(object sender, System.EventArgs e)
		{
			bApplied = false;
			TreeViewStyleFactory oFactory = TreeViewStyleFactory.GetInstance();

			TreeViewStyle style = oFactory.GetSunsetTreeStyle();	
			if (m_chkTreeViewStyle.Checked == true)
				treeView1.Style.ApplyStyle(style);
			if (m_chkTooltipStyle.Checked == true)
				treeView1.Style.NodeStyle.TooltipStyle.ApplyStyle(style.NodeStyle.TooltipStyle);
			if (m_chkNodeStyle.Checked == true)
				treeView1.Style.NodeStyle.ApplyStyleShallow(style.NodeStyle);
			if (m_chkExpandBoxStyle.Checked == true)
				treeView1.Style.NodeStyle.ExpandBoxStyle.ApplyStyle(style.NodeStyle.ExpandBoxStyle);
			if (m_chkCheckBoxStyle.Checked == true)
				treeView1.Style.NodeStyle.CheckBoxStyle.ApplyStyle(style.NodeStyle.CheckBoxStyle);		

			treeView1.Style.BorderStyle = m_oTreeView.Style.BorderStyle;		
			treeView1.Style.AutoCollapse = m_oTreeView.Style.AutoCollapse;
			treeView1.CheckBoxes = m_oTreeView.CheckBoxes;
			treeView1.Style.FlashColor = m_oTreeView.Style.FlashColor;
			treeView1.Style.HighlightSelectedPath = m_oTreeView.Style.HighlightSelectedPath;
			treeView1.Style.LineStyle = m_oTreeView.Style.LineStyle;
			treeView1.Style.ShowLines = m_oTreeView.Style.ShowLines;
			treeView1.Style.ShowPlusMinus = m_oTreeView.Style.ShowPlusMinus;
			treeView1.Style.ShowSubitemsIndicator = m_oTreeView.Style.ShowSubitemsIndicator;
			treeView1.Style.TrackNodeHover = m_oTreeView.Style.TrackNodeHover;			
			treeView1.Style.NodeStyle.SelectedFillStyle = m_oTreeView.Style.NodeStyle.SelectedFillStyle;		

			Node [] aNodes = treeView1.GetNodesByTag("color nodes");

			foreach (Node oNode in aNodes)
			{
				if (oNode.NodeStyle != null)
				{
					oNode.NodeStyle.ForeColor = treeView1.Style.NodeStyle.ForeColor;
					oNode.NodeStyle.SelectedBackColor = treeView1.Style.NodeStyle.SelectedBackColor;
					oNode.NodeStyle.SelectedForeColor = treeView1.Style.NodeStyle.SelectedForeColor;
					oNode.NodeStyle.SelectedBorderColor = treeView1.Style.NodeStyle.SelectedBorderColor;
				}
			}

			treeView1.Invalidate();		
		}

		private void m_rbGold_CheckedChanged(object sender, System.EventArgs e)
		{
			bApplied = false;
			TreeViewStyleFactory oFactory = TreeViewStyleFactory.GetInstance();

			TreeViewStyle style = oFactory.GetGoldTreeStyle();	
			if (m_chkTreeViewStyle.Checked == true)
				treeView1.Style.ApplyStyle(style);
			if (m_chkTooltipStyle.Checked == true)
				treeView1.Style.NodeStyle.TooltipStyle.ApplyStyle(style.NodeStyle.TooltipStyle);
			if (m_chkNodeStyle.Checked == true)
				treeView1.Style.NodeStyle.ApplyStyleShallow(style.NodeStyle);
			if (m_chkExpandBoxStyle.Checked == true)
				treeView1.Style.NodeStyle.ExpandBoxStyle.ApplyStyle(style.NodeStyle.ExpandBoxStyle);
			if (m_chkCheckBoxStyle.Checked == true)
				treeView1.Style.NodeStyle.CheckBoxStyle.ApplyStyle(style.NodeStyle.CheckBoxStyle);			

			treeView1.Style.BorderStyle = m_oTreeView.Style.BorderStyle;		
			treeView1.Style.AutoCollapse = m_oTreeView.Style.AutoCollapse;
			treeView1.CheckBoxes = m_oTreeView.CheckBoxes;
			treeView1.Style.FlashColor = m_oTreeView.Style.FlashColor;
			treeView1.Style.HighlightSelectedPath = m_oTreeView.Style.HighlightSelectedPath;
			treeView1.Style.LineStyle = m_oTreeView.Style.LineStyle;
			treeView1.Style.ShowLines = m_oTreeView.Style.ShowLines;
			treeView1.Style.ShowPlusMinus = m_oTreeView.Style.ShowPlusMinus;
			treeView1.Style.ShowSubitemsIndicator = m_oTreeView.Style.ShowSubitemsIndicator;
			treeView1.Style.TrackNodeHover = m_oTreeView.Style.TrackNodeHover;			
			treeView1.Style.NodeStyle.SelectedFillStyle = m_oTreeView.Style.NodeStyle.SelectedFillStyle;		

			Node [] aNodes = treeView1.GetNodesByTag("color nodes");

			foreach (Node oNode in aNodes)
			{
				if (oNode.NodeStyle != null)
				{
					oNode.NodeStyle.ForeColor = treeView1.Style.NodeStyle.ForeColor;
					oNode.NodeStyle.SelectedBackColor = treeView1.Style.NodeStyle.SelectedBackColor;
					oNode.NodeStyle.SelectedForeColor = treeView1.Style.NodeStyle.SelectedForeColor;
					oNode.NodeStyle.SelectedBorderColor = treeView1.Style.NodeStyle.SelectedBorderColor;
				}
			}

			treeView1.Invalidate();		
		}

		private void m_rbWood_CheckedChanged(object sender, System.EventArgs e)
		{
			bApplied = false;
			TreeViewStyleFactory oFactory = TreeViewStyleFactory.GetInstance();

			TreeViewStyle style = oFactory.GetWoodTreeStyle();	
			if (m_chkTreeViewStyle.Checked == true)
				treeView1.Style.ApplyStyle(style);
			if (m_chkTooltipStyle.Checked == true)
				treeView1.Style.NodeStyle.TooltipStyle.ApplyStyle(style.NodeStyle.TooltipStyle);
			if (m_chkNodeStyle.Checked == true)
				treeView1.Style.NodeStyle.ApplyStyleShallow(style.NodeStyle);
			if (m_chkExpandBoxStyle.Checked == true)
				treeView1.Style.NodeStyle.ExpandBoxStyle.ApplyStyle(style.NodeStyle.ExpandBoxStyle);
			if (m_chkCheckBoxStyle.Checked == true)
				treeView1.Style.NodeStyle.CheckBoxStyle.ApplyStyle(style.NodeStyle.CheckBoxStyle);		

			treeView1.Style.BorderStyle = m_oTreeView.Style.BorderStyle;		
			treeView1.Style.AutoCollapse = m_oTreeView.Style.AutoCollapse;
			treeView1.CheckBoxes = m_oTreeView.CheckBoxes;
			treeView1.Style.FlashColor = m_oTreeView.Style.FlashColor;
			treeView1.Style.HighlightSelectedPath = m_oTreeView.Style.HighlightSelectedPath;
			treeView1.Style.LineStyle = m_oTreeView.Style.LineStyle;
			treeView1.Style.ShowLines = m_oTreeView.Style.ShowLines;
			treeView1.Style.ShowPlusMinus = m_oTreeView.Style.ShowPlusMinus;
			treeView1.Style.ShowSubitemsIndicator = m_oTreeView.Style.ShowSubitemsIndicator;
			treeView1.Style.TrackNodeHover = m_oTreeView.Style.TrackNodeHover;			
			treeView1.Style.NodeStyle.SelectedFillStyle = m_oTreeView.Style.NodeStyle.SelectedFillStyle;		

			Node [] aNodes = treeView1.GetNodesByTag("color nodes");

			foreach (Node oNode in aNodes)
			{
				if (oNode.NodeStyle != null)
				{
					oNode.NodeStyle.ForeColor = treeView1.Style.NodeStyle.ForeColor;
					oNode.NodeStyle.SelectedBackColor = treeView1.Style.NodeStyle.SelectedBackColor;
					oNode.NodeStyle.SelectedForeColor = treeView1.Style.NodeStyle.SelectedForeColor;
					oNode.NodeStyle.SelectedBorderColor = treeView1.Style.NodeStyle.SelectedBorderColor;
				}
			}

			treeView1.Invalidate();		
		}

		private void m_rbSilver_CheckedChanged(object sender, System.EventArgs e)
		{
			bApplied = false;
			TreeViewStyleFactory oFactory = TreeViewStyleFactory.GetInstance();

			TreeViewStyle style = oFactory.GetSilverTreeStyle();	
			if (m_chkTreeViewStyle.Checked == true)
				treeView1.Style.ApplyStyle(style);
			if (m_chkTooltipStyle.Checked == true)
				treeView1.Style.NodeStyle.TooltipStyle.ApplyStyle(style.NodeStyle.TooltipStyle);
			if (m_chkNodeStyle.Checked == true)
				treeView1.Style.NodeStyle.ApplyStyleShallow(style.NodeStyle);
			if (m_chkExpandBoxStyle.Checked == true)
				treeView1.Style.NodeStyle.ExpandBoxStyle.ApplyStyle(style.NodeStyle.ExpandBoxStyle);
			if (m_chkCheckBoxStyle.Checked == true)
				treeView1.Style.NodeStyle.CheckBoxStyle.ApplyStyle(style.NodeStyle.CheckBoxStyle);		

			treeView1.Style.BorderStyle = m_oTreeView.Style.BorderStyle;		
			treeView1.Style.AutoCollapse = m_oTreeView.Style.AutoCollapse;
			treeView1.CheckBoxes = m_oTreeView.CheckBoxes;
			treeView1.Style.FlashColor = m_oTreeView.Style.FlashColor;
			treeView1.Style.HighlightSelectedPath = m_oTreeView.Style.HighlightSelectedPath;
			treeView1.Style.LineStyle = m_oTreeView.Style.LineStyle;
			treeView1.Style.ShowLines = m_oTreeView.Style.ShowLines;
			treeView1.Style.ShowPlusMinus = m_oTreeView.Style.ShowPlusMinus;
			treeView1.Style.ShowSubitemsIndicator = m_oTreeView.Style.ShowSubitemsIndicator;
			treeView1.Style.TrackNodeHover = m_oTreeView.Style.TrackNodeHover;			
			treeView1.Style.NodeStyle.SelectedFillStyle = m_oTreeView.Style.NodeStyle.SelectedFillStyle;		

			Node [] aNodes = treeView1.GetNodesByTag("color nodes");

			foreach (Node oNode in aNodes)
			{
				if (oNode.NodeStyle != null)
				{
					oNode.NodeStyle.ForeColor = treeView1.Style.NodeStyle.ForeColor;
					oNode.NodeStyle.SelectedBackColor = treeView1.Style.NodeStyle.SelectedBackColor;
					oNode.NodeStyle.SelectedForeColor = treeView1.Style.NodeStyle.SelectedForeColor;
					oNode.NodeStyle.SelectedBorderColor = treeView1.Style.NodeStyle.SelectedBorderColor;
				}
			}

			treeView1.Invalidate();		
		}

		private void m_rbRose_CheckedChanged(object sender, System.EventArgs e)
		{
			bApplied = false;
			TreeViewStyleFactory oFactory = TreeViewStyleFactory.GetInstance();

			TreeViewStyle style = oFactory.GetRoseTreeStyle();	
			if (m_chkTreeViewStyle.Checked == true)
				treeView1.Style.ApplyStyle(style);
			if (m_chkTooltipStyle.Checked == true)
				treeView1.Style.NodeStyle.TooltipStyle.ApplyStyle(style.NodeStyle.TooltipStyle);
			if (m_chkNodeStyle.Checked == true)
				treeView1.Style.NodeStyle.ApplyStyleShallow(style.NodeStyle);
			if (m_chkExpandBoxStyle.Checked == true)
				treeView1.Style.NodeStyle.ExpandBoxStyle.ApplyStyle(style.NodeStyle.ExpandBoxStyle);
			if (m_chkCheckBoxStyle.Checked == true)
				treeView1.Style.NodeStyle.CheckBoxStyle.ApplyStyle(style.NodeStyle.CheckBoxStyle);		

			treeView1.Style.BorderStyle = m_oTreeView.Style.BorderStyle;		
			treeView1.Style.AutoCollapse = m_oTreeView.Style.AutoCollapse;
			treeView1.CheckBoxes = m_oTreeView.CheckBoxes;
			treeView1.Style.FlashColor = m_oTreeView.Style.FlashColor;
			treeView1.Style.HighlightSelectedPath = m_oTreeView.Style.HighlightSelectedPath;
			treeView1.Style.LineStyle = m_oTreeView.Style.LineStyle;
			treeView1.Style.ShowLines = m_oTreeView.Style.ShowLines;
			treeView1.Style.ShowPlusMinus = m_oTreeView.Style.ShowPlusMinus;
			treeView1.Style.ShowSubitemsIndicator = m_oTreeView.Style.ShowSubitemsIndicator;
			treeView1.Style.TrackNodeHover = m_oTreeView.Style.TrackNodeHover;			
			treeView1.Style.NodeStyle.SelectedFillStyle = m_oTreeView.Style.NodeStyle.SelectedFillStyle;		

			Node [] aNodes = treeView1.GetNodesByTag("color nodes");

			foreach (Node oNode in aNodes)
			{
				if (oNode.NodeStyle != null)
				{
					oNode.NodeStyle.ForeColor = treeView1.Style.NodeStyle.ForeColor;
					oNode.NodeStyle.SelectedBackColor = treeView1.Style.NodeStyle.SelectedBackColor;
					oNode.NodeStyle.SelectedForeColor = treeView1.Style.NodeStyle.SelectedForeColor;
					oNode.NodeStyle.SelectedBorderColor = treeView1.Style.NodeStyle.SelectedBorderColor;
				}
			}

			treeView1.Invalidate();		
		}
		
		private void button1_Click(object sender, System.EventArgs e)
		{
			bApplied = false;
			button3_Click(sender, e);
		}

		private void button3_Click(object sender, System.EventArgs e)
		{			
			if (bApplied == true)
				return;

			bApplied = true;

			TreeViewStyle oOldStyle = m_oTreeView.Style;			

			PropertyDescriptorCollection aProperties = TypeDescriptor.GetProperties(m_oTreeView);
			PropertyDescriptor oStyleProperty = aProperties.Find("Style", false);
						
			using (DesignerTransaction oTransaction = m_oDesignerHost.CreateTransaction("Applying TreeView style"))
			{			
				m_oComponentDesigner.InvokeComponentChanging(oStyleProperty);				

				treeView1.Style.BorderStyle = m_oTreeView.Style.BorderStyle;  				
				treeView1.Style.TrackNodeHover = m_oTreeView.Style.TrackNodeHover;
				
				treeView1.Style.AutoCollapse = m_oTreeView.Style.AutoCollapse;
				treeView1.Style.FlashColor = m_oTreeView.Style.FlashColor;
				treeView1.Style.HighlightSelectedPath = m_oTreeView.Style.HighlightSelectedPath;
				treeView1.Style.LineColor = m_oTreeView.Style.LineColor;
				treeView1.Style.LineStyle = m_oTreeView.Style.LineStyle;
				treeView1.Style.ShowLines = m_oTreeView.Style.ShowLines;
				treeView1.Style.ShowPlusMinus = m_oTreeView.Style.ShowPlusMinus;
				treeView1.Style.ShowSubitemsIndicator = m_oTreeView.Style.ShowSubitemsIndicator;

				treeView1.Style.NodeStyle.Font = m_oTreeView.Style.NodeStyle.Font;				
				treeView1.Style.NodeStyle.UnderlineColor = m_oTreeView.Style.NodeStyle.UnderlineColor;
				treeView1.Style.NodeStyle.UnderlineStyle = m_oTreeView.Style.NodeStyle.UnderlineStyle;
				treeView1.Style.NodeStyle.SelectedFillStyle = m_oTreeView.Style.NodeStyle.SelectedFillStyle;

				m_oTreeView.ApplyStyle(treeView1.Style);
				oStyleProperty.SetValue(m_oTreeView, treeView1.Style);

				m_oComponentDesigner.InvokeComponentChanged(oStyleProperty, oOldStyle, treeView1.Style);

				oTransaction.Commit();
			}
		}

		protected override void OnVisibleChanged(System.EventArgs e)
		{
			m_oTreeView.Refresh();
		}
		#endregion		
	}
}
