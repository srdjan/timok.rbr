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
using System.Data;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PureComponents.TreeView.Design
{
	/// <summary>
	/// Summary description for ColorUIEditorHexagonCtrl.
	/// </summary>
	[Serializable]
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	internal class ColorUIEditorHexagonCtrl : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ColorUIEditorHexagonCtrl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();			
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

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint (e);

			GraphicsPath path = new GraphicsPath();

			path.StartFigure();

			path.AddLine(0, this.Height / 2, this.Width / 3 - 14, 0);
			path.AddLine(this.Width / 3 - 12, 0, this.Width / 3 * 2 + 14, 0);
			path.AddLine(this.Width / 3 * 2 + 14, 0, this.Width, this.Height / 2);
			path.AddLine(this.Width, this.Height / 2, this.Width / 3 * 2 + 14, this.Height);
			path.AddLine(this.Width / 3 * 2 + 14, this.Height, this.Width / 3 - 14, this.Height);
			path.AddLine(this.Width / 3 - 14, this.Height, 0, this.Height / 2);			

			path.CloseFigure();

			Region region = new Region(path);

			this.Region = region;			
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ColorUIEditorHexagonCtrl));
			// 
			// ColorUIEditorHexagonCtrl
			// 
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.Name = "ColorUIEditorHexagonCtrl";
			this.Size = new System.Drawing.Size(161, 138);

		}
		#endregion

		internal event EventHandler ColorPick;

		protected override void OnMouseUp(MouseEventArgs p)
		{
			base.OnMouseUp (p);

			if (ColorPick != null && this.BackgroundImage != null)
			{
				ColorUIEditorPaletteCtrl.ColorPickEventArgs args = new ColorUIEditorPaletteCtrl.ColorPickEventArgs();
				
				if (p.X < this.BackgroundImage.Width && p.Y < this.BackgroundImage.Height && p.X > 1 && p.Y > 1)
				{
					args.Color = ((Bitmap)this.BackgroundImage).GetPixel(p.X, p.Y);

					ColorPick(this, args);
				}
			}
		}
	}
}
