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
using System.Windows.Forms;

namespace PureComponents.TreeView.Design
{	
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[Serializable]
	internal class ColorUIEditorPaletteLightCtrl : Control
	{
		#region private members

		private Color m_BaseColor;		
		private bool m_MouseDown = false;
		private double m_Factor;

		#endregion

		#region construction

		public ColorUIEditorPaletteLightCtrl()
		{			
			this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);

			m_Factor = 0.014493;			
		}

		#endregion

		#region event handlers

		internal event EventHandler ColorPick;

		protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (m_MouseDown == false)
				return;

			if (ColorPick != null)
			{
				ColorUIEditorPaletteCtrl.ColorPickEventArgs args = new ColorUIEditorPaletteCtrl.ColorPickEventArgs();
				
				args.Color = ColorManager.SetBrightness(m_BaseColor, 1.0 - (double)e.Y * m_Factor / 2.0);

				ColorPick(this, args);
			}	
					
			this.Invalidate();
		}

		protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseUp(e);

			m_MouseDown = false;
		}

		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseDown(e);

			m_MouseDown = true;

			if (ColorPick != null)
			{
				ColorUIEditorPaletteCtrl.ColorPickEventArgs args = new ColorUIEditorPaletteCtrl.ColorPickEventArgs();
				
				args.Color = ColorManager.SetBrightness(m_BaseColor, 1.0 - (double)e.Y * m_Factor / 2.0);

				ColorPick(this, args);
			}	
					
			this.Invalidate();
		}

		#endregion

		#region painting

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint(e);

			// paint the color scale
			Color color = ColorManager.SetBrightness(m_BaseColor, 0.0);

			for (int step = 0; step < 69; step ++)
			{
				color = ColorManager.SetBrightness(m_BaseColor, 1.0 - m_Factor * (double)step);

				Pen pen = new Pen(color, 2);

				e.Graphics.DrawLine(pen, 0, step * 2, 10, step * 2);

				pen.Dispose();
			}

			// now find the position and draw the current placeholder
			ColorManager.HLS hls = ColorManager.RGB_to_HLS(m_BaseColor);

			int pos = this.Height - (int)(hls.L * 2.0 / m_Factor);

			e.Graphics.DrawLine(Pens.Black, 12, pos - 2, 17, pos - 2);
			e.Graphics.DrawLine(Pens.Black, 12, pos - 1, 17, pos - 1);
			e.Graphics.DrawLine(Pens.Black, 12, pos, 16, pos);
			e.Graphics.DrawLine(Pens.Black, 12, pos + 1, 17, pos + 1);
		}

		#endregion

		#region implementation

		protected override void OnCreateControl()
		{
			base.OnCreateControl ();

			// recalc factor
			double d = (double)137.0 / (double)this.Height;

			m_Factor = m_Factor * d;
		}

		#endregion

		#region properties

		public Color Color
		{
			get
			{
				return m_BaseColor;
			}

			set
			{
				m_BaseColor = value;

				this.Invalidate();
			}
		}

		#endregion
	}
}
