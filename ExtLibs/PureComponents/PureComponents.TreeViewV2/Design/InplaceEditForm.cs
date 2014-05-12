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
using System.Windows.Forms;

using PureComponents.TreeView;

namespace PureComponents.TreeView.Design
{
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	internal class InplaceEditForm : System.Windows.Forms.Form
	{
		#region private members

		private System.Windows.Forms.TextBox textBox1;

		private Node m_oNode;
		private TreeViewDesigner m_oDesigner;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#endregion

		#region construction

		public InplaceEditForm(Node oNode, TreeViewDesigner oDesigner)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			m_oNode = oNode;
			m_oDesigner = oDesigner;

			textBox1.Text = m_oNode.Text;
			textBox1.SelectAll();

			textBox1.LostFocus += new EventHandler(textBox1_LostFocus);
			textBox1.Focus();
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(135, 20);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "";
			this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
			// 
			// InplaceEditForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(135, 20);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.textBox1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "InplaceEditForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "InplaceEditForm";
			this.TopMost = true;
			this.BackColorChanged += new System.EventHandler(this.InplaceEditForm_BackColorChanged);
			this.ResumeLayout(false);

		}
		#endregion

		#region implementation

		internal void FocusTextBox()
		{
			textBox1.Focus();
		}

		#endregion

		private void InplaceEditForm_BackColorChanged(object sender, System.EventArgs e)
		{
			textBox1.BackColor = this.BackColor;		
		}

		private void textBox1_LostFocus(object sender, EventArgs e)
		{
			m_oNode.Text = textBox1.Text;

			m_oDesigner.InvokeComponentChanging(TypeDescriptor.GetProperties(m_oNode)["Text"]);
			m_oDesigner.InvokeComponentChanged(TypeDescriptor.GetProperties(m_oNode)["Text"], null, null);

			this.Close();
		}

		private void textBox1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs args)
		{
			if (args.KeyChar == 13)
			{
				m_oNode.Text = textBox1.Text;

				m_oDesigner.InvokeComponentChanging(TypeDescriptor.GetProperties(m_oNode)["Text"]);
				m_oDesigner.InvokeComponentChanged(TypeDescriptor.GetProperties(m_oNode)["Text"], null, null);

				this.Close();

				return;
			}

			if (args.KeyChar == 27)
			{
				this.Close();
			}
		}		
	}
}
