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
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[Serializable]
	internal class ColorUIEditorPaletteCtrl : System.Windows.Forms.UserControl
	{
		#region private members

		private Point m_lastPoint = Point.Empty;
		private bool m_MouseDown = false;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#endregion

		#region construction

		public ColorUIEditorPaletteCtrl()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ColorUIEditorPaletteCtrl));
			// 
			// ColorUIEditorPaletteCtrl
			// 
			this.BackgroundImage = ((System.Drawing.Bitmap)(resources.GetObject("$this.BackgroundImage")));
			this.Name = "ColorUIEditorPaletteCtrl";
			this.Size = new System.Drawing.Size(175, 137);

		}
		#endregion

		#region events

		internal event EventHandler ColorPick;

		[Serializable]
		internal class ColorPickEventArgs : EventArgs
		{
			public Color Color;
		}

		#endregion		
		
		#region implementation

		protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
		{
			m_MouseDown = false;
		}
		
		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseDown(e);

			m_MouseDown = true;

			if (ColorPick != null && this.BackgroundImage != null)
			{
				ColorPickEventArgs args = new ColorPickEventArgs();
				
				if (e.X < this.BackgroundImage.Width && e.Y < this.BackgroundImage.Height && e.X > 1 && e.Y > 1)
				{
					args.Color = ((Bitmap)this.BackgroundImage).GetPixel(e.X, e.Y);

					ColorPick(this, args);
				}
			}	
		
			// remember the last point and do the painting
			m_lastPoint = new Point(e.X, e.Y);

			this.Invalidate();
		}

		protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (m_MouseDown == false)
				return;

			if (ColorPick != null && this.BackgroundImage != null)
			{
				ColorPickEventArgs args = new ColorPickEventArgs();
				
				if (e.X < this.BackgroundImage.Width && e.Y < this.BackgroundImage.Height && e.X > 1 && e.Y > 1)
				{
					args.Color = ((Bitmap)this.BackgroundImage).GetPixel(e.X, e.Y);

					ColorPick(this, args);
				}
			}	

			// remember the last point and do the painting
			m_lastPoint = new Point(e.X, e.Y);

			this.Invalidate();
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint(e);

			// paint the target area
			e.Graphics.DrawLine(Pens.White, m_lastPoint.X - 5, m_lastPoint.Y, m_lastPoint.X - 1, m_lastPoint.Y);
			e.Graphics.DrawLine(Pens.White, m_lastPoint.X + 5, m_lastPoint.Y, m_lastPoint.X + 1, m_lastPoint.Y);
			e.Graphics.DrawLine(Pens.White, m_lastPoint.X, m_lastPoint.Y - 5, m_lastPoint.X, m_lastPoint.Y - 1);
			e.Graphics.DrawLine(Pens.White, m_lastPoint.X, m_lastPoint.Y + 5, m_lastPoint.X, m_lastPoint.Y + 1);
		}		

		#endregion		
	}
}
