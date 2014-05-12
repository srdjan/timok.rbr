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
using System.Windows.Forms;

namespace PureComponents.TreeView.Design
{
	/// <summary>
	/// Summary description for ColorUIEditorCustomCtrl.
	/// </summary>
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[Serializable]
	internal class ColorUIEditorCustomCtrl : System.Windows.Forms.UserControl
	{
		#region private members

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#endregion

		#region construction

		public ColorUIEditorCustomCtrl()
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

		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ColorUIEditorCustomCtrl));
			// 
			// ColorUIEditorCustomCtrl
			// 
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.Name = "ColorUIEditorCustomCtrl";
			this.Size = new System.Drawing.Size(159, 145);

		}
		#endregion		

		internal event EventHandler ColorPick;		

		private void label37_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
//			Label lbl = (Label)sender;
//
//			Point p = new Point(e.X, e.Y);
//
//			p = lbl.PointToScreen(p);
//			p = this.PointToClient(p);
//
//			if (ColorPick != null && this.BackgroundImage != null)
//			{
//				ColorUIEditorPaletteCtrl.ColorPickEventArgs args = new ColorUIEditorPaletteCtrl.ColorPickEventArgs();
//				
//				if (p.X < this.BackgroundImage.Width && p.Y < this.BackgroundImage.Height && p.X > 1 && p.Y > 1)
//				{
//					args.Color = ((Bitmap)this.BackgroundImage).GetPixel(p.X, p.Y);
//
//					ColorPick(this, args);
//				}
//			}
		}

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
