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
using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace PureComponents.TreeView.Design
{
	/// <summary>
	/// Summary description for ColorUIEditorCtrl.
	/// </summary>
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[Serializable]
	internal class ColorUIEditorCtrl : UserControl
	{		
		#region private members

		private ColorUIEditorHexagonCtrl m_HexagonCtrl;
		private ColorUIEditorCustomCtrl m_CustomCtrl;
		private ColorUIEditorWheelCtrl m_WheelCtrl;
		private bool m_InternalValueChange = false;
		private Color m_OriginalColor;
		private IWindowsFormsEditorService m_EditorService = null;

		private TabControl tabControl1;
		private TabPage tabPage1;
		private TabPage tabPage2;
		private ListBox lstSystemColors;
		private ListBox lstColors;
		private Panel previewColorPanel;
		private System.Windows.Forms.Label label1;
		private Button btnUse;
		private TabPage tabPage4;
		private Button button1;
		private Panel originalColorPanel;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private TextBox txtValue;
		private TabPage tabPage3;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		#endregion

		#region construction

		internal ColorUIEditorCtrl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			
			AddColoursToList(lstSystemColors, typeof(SystemColors), Color.Black);
			AddNamedColors();
		
			// create the custom control
			m_CustomCtrl = new ColorUIEditorCustomCtrl();

			m_CustomCtrl.Top = 4;
			m_CustomCtrl.Left = 0;

			m_CustomCtrl.ColorPick += new EventHandler(CustomColorPick);

			// create the hexagon ctrl
			m_HexagonCtrl = new ColorUIEditorHexagonCtrl();

			m_HexagonCtrl.Top = 8;
			m_HexagonCtrl.Left = 2 + m_CustomCtrl.Width;

			m_HexagonCtrl.ColorPick += new EventHandler(CustomColorPick);

			// create the color wheel control
			m_WheelCtrl = new ColorUIEditorWheelCtrl();
			m_WheelCtrl.Dock = DockStyle.Fill;

			m_WheelCtrl.ColorChanged += new EventHandler(CustomColorPick);

			tabPage3.Controls.Add(m_WheelCtrl);			
			tabPage4.Controls.Add(m_CustomCtrl);
			tabPage4.Controls.Add(m_HexagonCtrl);
		}

		public ColorUIEditorCtrl(Color originalColor, IWindowsFormsEditorService editorService) : this()
		{
			m_EditorService = editorService;
			m_OriginalColor = originalColor;

			this.BackColor = tabPage1.BackColor;
			button1.BackColor = tabPage1.BackColor;
			btnUse.BackColor = tabPage1.BackColor;
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.lstColors = new System.Windows.Forms.ListBox();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.lstSystemColors = new System.Windows.Forms.ListBox();
			this.previewColorPanel = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.btnUse = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.originalColorPanel = new System.Windows.Forms.Panel();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.txtValue = new System.Windows.Forms.TextBox();
			this.tabControl1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(238)));
			this.tabControl1.Location = new System.Drawing.Point(1, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(336, 181);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage3
			// 
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(328, 155);
			this.tabPage3.TabIndex = 4;
			this.tabPage3.Text = "Advanced";
			// 
			// tabPage4
			// 
			this.tabPage4.BackColor = System.Drawing.SystemColors.Control;
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(328, 155);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Custom";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.lstColors);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(328, 155);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Web";
			// 
			// lstColors
			// 
			this.lstColors.BackColor = System.Drawing.Color.White;
			this.lstColors.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstColors.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.lstColors.Location = new System.Drawing.Point(0, 0);
			this.lstColors.Name = "lstColors";
			this.lstColors.Size = new System.Drawing.Size(328, 155);
			this.lstColors.TabIndex = 1;
			this.lstColors.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.lstSystemColors_MeasureItem);
			this.lstColors.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstColors_DrawItem);
			this.lstColors.SelectedIndexChanged += new System.EventHandler(this.lstColors_SelectedIndexChanged);
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.lstSystemColors);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(328, 155);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "System";
			// 
			// lstSystemColors
			// 
			this.lstSystemColors.BackColor = System.Drawing.Color.White;
			this.lstSystemColors.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstSystemColors.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.lstSystemColors.Location = new System.Drawing.Point(0, 0);
			this.lstSystemColors.Name = "lstSystemColors";
			this.lstSystemColors.Size = new System.Drawing.Size(328, 155);
			this.lstSystemColors.TabIndex = 0;
			this.lstSystemColors.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.lstSystemColors_MeasureItem);
			this.lstSystemColors.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstColors_DrawItem);
			this.lstSystemColors.SelectedIndexChanged += new System.EventHandler(this.lstSystemColors_SelectedIndexChanged);
			// 
			// previewColorPanel
			// 
			this.previewColorPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.previewColorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.previewColorPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(238)));
			this.previewColorPanel.Location = new System.Drawing.Point(108, 202);
			this.previewColorPanel.Name = "previewColorPanel";
			this.previewColorPanel.Size = new System.Drawing.Size(39, 20);
			this.previewColorPanel.TabIndex = 1;
			this.previewColorPanel.BackColorChanged += new System.EventHandler(this.previewColorPanel_BackColorChanged);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(238)));
			this.label1.Location = new System.Drawing.Point(106, 186);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(29, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "New";
			// 
			// btnUse
			// 
			this.btnUse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnUse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(238)));
			this.btnUse.Location = new System.Drawing.Point(212, 200);
			this.btnUse.Name = "btnUse";
			this.btnUse.Size = new System.Drawing.Size(53, 23);
			this.btnUse.TabIndex = 2;
			this.btnUse.Text = "Apply";
			this.btnUse.Click += new System.EventHandler(this.button1_Click);
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(238)));
			this.button1.Location = new System.Drawing.Point(272, 200);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(53, 23);
			this.button1.TabIndex = 3;
			this.button1.Text = "Cancel";
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// originalColorPanel
			// 
			this.originalColorPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.originalColorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.originalColorPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(238)));
			this.originalColorPanel.Location = new System.Drawing.Point(146, 202);
			this.originalColorPanel.Name = "originalColorPanel";
			this.originalColorPanel.Size = new System.Drawing.Size(39, 20);
			this.originalColorPanel.TabIndex = 4;
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(238)));
			this.label5.Location = new System.Drawing.Point(145, 186);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(44, 12);
			this.label5.TabIndex = 5;
			this.label5.Text = "Current";
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(238)));
			this.label6.Location = new System.Drawing.Point(5, 186);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(38, 12);
			this.label6.TabIndex = 6;
			this.label6.Text = "Value:";
			// 
			// txtValue
			// 
			this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.txtValue.BackColor = System.Drawing.Color.White;
			this.txtValue.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.txtValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(238)));
			this.txtValue.Location = new System.Drawing.Point(7, 203);
			this.txtValue.Name = "txtValue";
			this.txtValue.ReadOnly = true;
			this.txtValue.Size = new System.Drawing.Size(88, 20);
			this.txtValue.TabIndex = 7;
			this.txtValue.Text = "";
			this.txtValue.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtValue_KeyUp);
			// 
			// ColorUIEditorCtrl
			// 
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.originalColorPanel);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtValue);
			this.Controls.Add(this.previewColorPanel);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.btnUse);
			this.Name = "ColorUIEditorCtrl";
			this.Size = new System.Drawing.Size(337, 228);
			this.tabControl1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region private implementation

		private void AddColoursToList(ListBox in_listBox, Type in_colourSource, Color in_selectMe)
		{
			PropertyInfo [] pic;
			ColourAndNameListItem canli;			
			int i;

			pic = in_colourSource.GetProperties();

			foreach (PropertyInfo pi in pic)
			{
				if (pi.PropertyType == typeof(Color))
				{
					canli = new ColourAndNameListItem();
					canli.Colour = (Color)pi.GetValue(null, null);
					canli.Name = pi.Name;

					i = in_listBox.Items.Add(canli);

					if (in_selectMe.Equals(canli.Colour))
						in_listBox.SelectedIndex = i;
				}
			}																							
		}

		private void AddNamedColors()
		{
			AddColourToList(lstColors, Color.Transparent);
			AddColourToList(lstColors, Color.Black);
			AddColourToList(lstColors, Color.DimGray);
			AddColourToList(lstColors, Color.Gray);
			AddColourToList(lstColors, Color.DarkGray);
			AddColourToList(lstColors, Color.Silver);
			AddColourToList(lstColors, Color.LightGray);
			AddColourToList(lstColors, Color.Gainsboro);
			AddColourToList(lstColors, Color.WhiteSmoke);
			AddColourToList(lstColors, Color.White);

			AddColourToList(lstColors, Color.RosyBrown);
			AddColourToList(lstColors, Color.IndianRed);
			AddColourToList(lstColors, Color.Brown);
			AddColourToList(lstColors, Color.Firebrick);
			AddColourToList(lstColors, Color.LightCoral);
			AddColourToList(lstColors, Color.Maroon);
			AddColourToList(lstColors, Color.DarkRed);
			AddColourToList(lstColors, Color.Red);
			AddColourToList(lstColors, Color.Snow);
			AddColourToList(lstColors, Color.MistyRose);
			AddColourToList(lstColors, Color.Salmon);
			AddColourToList(lstColors, Color.Tomato);
			AddColourToList(lstColors, Color.DarkSalmon);
			AddColourToList(lstColors, Color.Coral);
			AddColourToList(lstColors, Color.OrangeRed);
			AddColourToList(lstColors, Color.LightSalmon);
			AddColourToList(lstColors, Color.Sienna);
			AddColourToList(lstColors, Color.SeaShell);
			AddColourToList(lstColors, Color.Chocolate);
			AddColourToList(lstColors, Color.SaddleBrown);
			AddColourToList(lstColors, Color.SandyBrown);
			AddColourToList(lstColors, Color.PeachPuff);
			AddColourToList(lstColors, Color.Peru);

			AddColourToList(lstColors, Color.Linen);
			AddColourToList(lstColors, Color.Bisque);
			AddColourToList(lstColors, Color.DarkOrange);
			AddColourToList(lstColors, Color.BurlyWood);
			AddColourToList(lstColors, Color.Tan);
			AddColourToList(lstColors, Color.AntiqueWhite);
			AddColourToList(lstColors, Color.NavajoWhite);
			AddColourToList(lstColors, Color.BlanchedAlmond);
			AddColourToList(lstColors, Color.PapayaWhip);
			AddColourToList(lstColors, Color.Moccasin);
			AddColourToList(lstColors, Color.Orange);

			AddColourToList(lstColors, Color.Wheat);
			AddColourToList(lstColors, Color.OldLace);
			AddColourToList(lstColors, Color.FloralWhite);
			AddColourToList(lstColors, Color.DarkGoldenrod);
			AddColourToList(lstColors, Color.Goldenrod);
			AddColourToList(lstColors, Color.Cornsilk);
			AddColourToList(lstColors, Color.Gold);
			AddColourToList(lstColors, Color.Khaki);
			AddColourToList(lstColors, Color.LemonChiffon);
			AddColourToList(lstColors, Color.PaleGoldenrod);
			AddColourToList(lstColors, Color.DarkKhaki);
			AddColourToList(lstColors, Color.LightGray);
			AddColourToList(lstColors, Color.Beige);
			AddColourToList(lstColors, Color.LightGoldenrodYellow);

			AddColourToList(lstColors, Color.Olive);
			AddColourToList(lstColors, Color.Yellow);
			AddColourToList(lstColors, Color.LightYellow);
			AddColourToList(lstColors, Color.Ivory);
			AddColourToList(lstColors, Color.OliveDrab);
			AddColourToList(lstColors, Color.YellowGreen);
			AddColourToList(lstColors, Color.DarkOliveGreen);
			AddColourToList(lstColors, Color.GreenYellow);
			AddColourToList(lstColors, Color.Chartreuse);
			AddColourToList(lstColors, Color.LawnGreen);
			AddColourToList(lstColors, Color.DarkSeaGreen);
			AddColourToList(lstColors, Color.ForestGreen);
			AddColourToList(lstColors, Color.LimeGreen);

			AddColourToList(lstColors, Color.LightGreen);
			AddColourToList(lstColors, Color.PaleGreen);
			AddColourToList(lstColors, Color.DarkGreen);
			AddColourToList(lstColors, Color.Green);
			AddColourToList(lstColors, Color.Lime);
			AddColourToList(lstColors, Color.Honeydew);
			AddColourToList(lstColors, Color.SeaGreen);
			AddColourToList(lstColors, Color.MediumSeaGreen);
			AddColourToList(lstColors, Color.SpringGreen);
			AddColourToList(lstColors, Color.MintCream);
			AddColourToList(lstColors, Color.MediumSpringGreen);
			AddColourToList(lstColors, Color.MediumAquamarine);
			AddColourToList(lstColors, Color.Aquamarine);
			AddColourToList(lstColors, Color.Turquoise);

			AddColourToList(lstColors, Color.LightSeaGreen);
			AddColourToList(lstColors, Color.MediumTurquoise);
			AddColourToList(lstColors, Color.DarkSlateBlue);
			AddColourToList(lstColors, Color.PaleTurquoise);
			AddColourToList(lstColors, Color.Teal);
			AddColourToList(lstColors, Color.DarkCyan);
			AddColourToList(lstColors, Color.Aqua);
			AddColourToList(lstColors, Color.Cyan);
			AddColourToList(lstColors, Color.LightCyan);
			AddColourToList(lstColors, Color.Azure);
			AddColourToList(lstColors, Color.DarkTurquoise);
			AddColourToList(lstColors, Color.CadetBlue);
			AddColourToList(lstColors, Color.PowderBlue);
			AddColourToList(lstColors, Color.LightBlue);

			AddColourToList(lstColors, Color.DeepSkyBlue);
			AddColourToList(lstColors, Color.SkyBlue);
			AddColourToList(lstColors, Color.LightSkyBlue);
			AddColourToList(lstColors, Color.SteelBlue);
			AddColourToList(lstColors, Color.AliceBlue);
			AddColourToList(lstColors, Color.DodgerBlue);
			AddColourToList(lstColors, Color.SlateGray);
			AddColourToList(lstColors, Color.LightSlateGray);
			AddColourToList(lstColors, Color.LightSteelBlue);
			AddColourToList(lstColors, Color.CornflowerBlue);
			AddColourToList(lstColors, Color.RoyalBlue);
			AddColourToList(lstColors, Color.MidnightBlue);
			AddColourToList(lstColors, Color.Lavender);
			AddColourToList(lstColors, Color.Navy);

			AddColourToList(lstColors, Color.DarkBlue);
			AddColourToList(lstColors, Color.MediumBlue);
			AddColourToList(lstColors, Color.Blue);
			AddColourToList(lstColors, Color.GhostWhite);
			AddColourToList(lstColors, Color.SlateBlue);
			AddColourToList(lstColors, Color.DarkSlateBlue);
			AddColourToList(lstColors, Color.MediumSlateBlue);
			AddColourToList(lstColors, Color.MediumPurple);
			AddColourToList(lstColors, Color.BlueViolet);
			AddColourToList(lstColors, Color.Indigo);
			AddColourToList(lstColors, Color.DarkOrchid);
			AddColourToList(lstColors, Color.DarkViolet);
			AddColourToList(lstColors, Color.MediumOrchid);

			AddColourToList(lstColors, Color.Thistle);
			AddColourToList(lstColors, Color.Plum);
			AddColourToList(lstColors, Color.Violet);
			AddColourToList(lstColors, Color.Purple);
			AddColourToList(lstColors, Color.DarkMagenta);
			AddColourToList(lstColors, Color.Magenta);
			AddColourToList(lstColors, Color.Fuchsia);
			AddColourToList(lstColors, Color.Orchid);
			AddColourToList(lstColors, Color.MediumVioletRed);
			AddColourToList(lstColors, Color.DeepPink);
			AddColourToList(lstColors, Color.HotPink);
			AddColourToList(lstColors, Color.LavenderBlush);
			AddColourToList(lstColors, Color.PaleVioletRed);
			AddColourToList(lstColors, Color.Crimson);
			AddColourToList(lstColors, Color.Pink);
			AddColourToList(lstColors, Color.LightPink);
		}

		private void AddColourToList(ListBox in_listBox, Color color)
		{
			ColourAndNameListItem canli = new ColourAndNameListItem();
			canli.Colour = color;
			canli.Name = color.Name;

			in_listBox.Items.Add(canli);
		}

		#endregion

		#region event handlers

		private void lstColors_DrawItem(object sender, DrawItemEventArgs e)
		{
			DrawItemForListBox(sender as ListBox, e);
		}

		private void CustomColorPick(object sender, EventArgs e)
		{
			this.Value = ((ColorUIEditorPaletteCtrl.ColorPickEventArgs)e).Color;

			m_WheelCtrl.Color = this.Value;
		}

		private void DrawItemForListBox(ListBox in_listBox, DrawItemEventArgs in_args)
		{
			ColourAndNameListItem canli;
			SolidBrush b = null;
			Rectangle r;
			Graphics g;
			Pen p = null;
			
			in_args.DrawBackground();

			g = in_args.Graphics;
			r = in_args.Bounds;

			canli = in_listBox.Items[in_args.Index] as ColourAndNameListItem;
			
			try
			{
				b = new SolidBrush(canli.Colour);
				p = new Pen(Color.Black);

				g.FillRectangle(b, r.Left + 2, r.Top + 2, 22, in_listBox.ItemHeight - 4);
				g.DrawRectangle(p, r.Left + 2, r.Top + 2, 22, in_listBox.ItemHeight - 4);
			}
			finally
			{
				if (b != null) 
					b.Dispose();
			
				if (p != null)
					p.Dispose();
			}

			try
			{
				if (in_args.State == DrawItemState.Selected)
				{
					b = new SolidBrush(in_listBox.BackColor);
				}
				else
				{
					b = new SolidBrush(SystemColors.ControlText);
				}

				g.DrawString(canli.Name, in_listBox.Font, b, r.Left + 26, in_args.Bounds.Top);
			}
			finally
			{
				if (b != null) 
					b.Dispose();
			}
		}

		private void lstSystemColors_MeasureItem(object sender, MeasureItemEventArgs e)
		{
			e.ItemHeight = 14;
		}

		private void lstSystemColors_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (m_InternalValueChange == true)
				return;

			m_InternalValueChange = true;

			// get the selected item and its color
			ColourAndNameListItem item = lstSystemColors.SelectedItem as ColourAndNameListItem;

			// create the HLS and set the data
			ColorManager.HLS hls = ColorManager.RGB_to_HLS(item.Colour);
//			txtH.Value = (int)(hls.H * 100.0);
//			txtL.Value = (int)(hls.L * 100.0);
//			txtS.Value = (int)(hls.S * 100.0);

			// set the preview as well
			previewColorPanel.BackColor = item.Colour;

//			txtRed.Value = previewColorPanel.BackColor.R;
//			txtGreen.Value = previewColorPanel.BackColor.G;
//			txtBlue.Value = previewColorPanel.BackColor.B;		
			
			txtValue.Text = "#" + previewColorPanel.BackColor.R.ToString("X2") + previewColorPanel.BackColor.G.ToString("X2") + previewColorPanel.BackColor.B.ToString("X2");			

			m_InternalValueChange = false;
		}

		private void lstColors_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (m_InternalValueChange == true)
				return;

			m_InternalValueChange = true;

			// get the selected item and its color
			ColourAndNameListItem item = lstColors.SelectedItem as ColourAndNameListItem;

			// create the HLS and set the data
			ColorManager.HLS hls = ColorManager.RGB_to_HLS(item.Colour);
			
//			txtH.Value = (int)(hls.H * 100.0);
//			txtL.Value = (int)(hls.L * 100.0);
//			txtS.Value = (int)(hls.S * 100.0);

			// set the preview as well
			previewColorPanel.BackColor = item.Colour;

//			txtRed.Value = previewColorPanel.BackColor.R;
//			txtGreen.Value = previewColorPanel.BackColor.G;
//			txtBlue.Value = previewColorPanel.BackColor.B;			

			txtValue.Text = "#" + previewColorPanel.BackColor.R.ToString("X2") + previewColorPanel.BackColor.G.ToString("X2") + previewColorPanel.BackColor.B.ToString("X2");			

			m_InternalValueChange = false;
		}
		
		internal event EventHandler OKClick;
		internal event EventHandler CancelClick;

		private void button1_Click(object sender, EventArgs e)
		{			
			if (m_EditorService != null)
				m_EditorService.CloseDropDown();

			if (OKClick != null)
				OKClick(this, EventArgs.Empty);
		}

		private void button1_Click_1(object sender, EventArgs e)
		{
			previewColorPanel.BackColor = m_OriginalColor;

			if (m_EditorService != null)
				m_EditorService.CloseDropDown();

			if (CancelClick != null)
				CancelClick(this, EventArgs.Empty);
		}
		
		private void radioButton2_Click(object sender, EventArgs e)
		{
			// HLS model selected
//			txtH.Visible = true;
//			txtL.Visible = true;
//			txtS.Visible = true;
//			lblHue.Visible = true;
//			lblLight.Visible = true;
//			lblSaturation.Visible = true;
//			
//			txtRed.Visible = false;
//			txtGreen.Visible = false;
//			txtBlue.Visible = false;
//			lblRed.Visible = false;
//			lblGreen.Visible = false;			
//			lblBlue.Visible = false;
		}

		private void radioButton1_Click(object sender, EventArgs e)
		{
			// RGB model selected
//			txtH.Visible = false;
//			txtL.Visible = false;
//			txtS.Visible = false;
//			lblHue.Visible = false;
//			lblLight.Visible = false;
//			lblSaturation.Visible = false;
//			
//			txtRed.Visible = true;
//			txtGreen.Visible = true;
//			txtBlue.Visible = true;
//			lblRed.Visible = true;
//			lblGreen.Visible = true;			
//			lblBlue.Visible = true;
		}

		private void txtBlue_ValueChanged(object sender, EventArgs e)
		{
			if (m_InternalValueChange == true)
				return;			

			m_InternalValueChange = true;

//			Color color = Color.FromArgb((int)txtRed.Value, (int)txtGreen.Value, (int)txtBlue.Value);
//
//			// update the hls and the preview color			
//			ColorManager.HLS hls = ColorManager.RGB_to_HLS(color);
//			
//			txtH.Value = (int)(hls.H * 100.0);
//			txtL.Value = (int)(hls.L * 100.0);
//			txtS.Value = (int)(hls.S * 100.0);			
//
//			// set the preview as well
//			previewColorPanel.BackColor = color;

			txtValue.Text = "#" + previewColorPanel.BackColor.R.ToString("X2") + previewColorPanel.BackColor.G.ToString("X2") + previewColorPanel.BackColor.B.ToString("X2");			

			m_InternalValueChange = false;
		}

		private void previewColorPanel_BackColorChanged(object sender, EventArgs e)
		{
			if (m_WheelCtrl == null)
				return;

			m_WheelCtrl.Color = this.Value;
		}		

		private void txtHSL_ValueChanged(object sender, EventArgs e)
		{
			if (m_InternalValueChange == true)
				return;

			m_InternalValueChange = true;

			// get the hls value and set it to the preview
			ColorManager.HLS hls = new ColorManager.HLS();

//			hls.H = (double)txtH.Value / 100.0;
//			hls.L = (double)txtL.Value / 100.0;
//			hls.S = (double)txtS.Value / 100.0;

			Color color = ColorManager.HLS_to_RGB(hls);

			previewColorPanel.BackColor = color;

//			txtRed.Value = previewColorPanel.BackColor.R;
//			txtGreen.Value = previewColorPanel.BackColor.G;
//			txtBlue.Value = previewColorPanel.BackColor.B;		

			txtValue.Text = "#" + previewColorPanel.BackColor.R.ToString("X2") + previewColorPanel.BackColor.G.ToString("X2") + previewColorPanel.BackColor.B.ToString("X2");			

			m_InternalValueChange = false;
		}

		/// <summary>
		/// Key handling for the textbox
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtValue_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			// handle the CTRL+V key stroke to enter the clipboard color data
			if (e.Control == true && e.KeyCode == Keys.V)
			{
				IDataObject obj = Clipboard.GetDataObject();
			
				if (obj.GetDataPresent(DataFormats.Text))
				{
					string colorText = obj.GetData(DataFormats.Text) as string;

					// check if it is in the correct format
					if (colorText.Length == 7)
					{
						string testStr = "ABCDEF0123456789";

						char[] arrayStr = colorText.ToCharArray();
						if (arrayStr[0] == '#' && testStr.IndexOf(arrayStr[1]) != -1 && 
							testStr.IndexOf(arrayStr[2]) != -1 && testStr.IndexOf(arrayStr[3]) != -1 && 
							testStr.IndexOf(arrayStr[4]) != -1 && testStr.IndexOf(arrayStr[5]) != -1 && 
							testStr.IndexOf(arrayStr[6]) != -1)
						{
							// now parse the color
							string r = "0x" + arrayStr[1].ToString() + arrayStr[2].ToString();
							string g = "0x" + arrayStr[3].ToString() + arrayStr[4].ToString();
							string b = "0x" + arrayStr[5].ToString() + arrayStr[6].ToString();

							try
							{
								Color clr = Color.FromArgb(Convert.ToInt32(r, 16), Convert.ToInt32(g, 16), Convert.ToInt32(b, 16));

								Value = clr;
								//m_WheelCtrl.Color = clr;
								m_WheelCtrl.RefreshEditors();
								m_WheelCtrl.Invalidate();

								e.Handled = true;
							}
							catch
							{
								
							}
						}
					}
				}
			}
		}

		#endregion
		
		#region properties

		public Color OriginalValue
		{
			set
			{
				m_OriginalColor = value;
				originalColorPanel.BackColor = value;
			}
		}
		
		public Color Value
		{
			get
			{
				return previewColorPanel.BackColor;
			}

			set
			{
				// set the preview panel color
				previewColorPanel.BackColor = value;
				originalColorPanel.BackColor = m_OriginalColor;

				// create the HLS and set the data
				ColorManager.HLS hls = ColorManager.RGB_to_HLS(value);
			
				// set the HLS tracks
//				txtH.Value = (int)(hls.H * 100.0);
//				txtL.Value = (int)(hls.L * 100.0);
//				txtS.Value = (int)(hls.S * 100.0);
//
//				txtRed.Value = value.R;
//				txtGreen.Value = value.G;
//				txtBlue.Value = value.B;

				txtValue.Text = "#" + previewColorPanel.BackColor.R.ToString("X2") + previewColorPanel.BackColor.G.ToString("X2") + previewColorPanel.BackColor.B.ToString("X2");			
			}
		}

		#endregion		
	}

	[Serializable]
	internal class ColourAndNameListItem
	{

		public Color Colour;
		public string Name;

		public override string ToString()
		{
			return this.Name;
		}
	}
}