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
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using PureComponents.TreeView.Design;

namespace PureComponents.TreeView
{
	/// <summary>
	/// NodePanel represents a container for controls associated with the Node.
	/// </summary>
	[DefaultEvent("")]
	[DefaultProperty("")]
	[DefaultMember("")]
	[Serializable]
	[Designer(typeof(NodePanelDesigner))]
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	public class NodePanel : Control
	{
		#region private members

		private Color m_FadeColor = Color.White;
		private Color m_BorderColor = Color.DarkGray;
		private BorderStyle m_BorderStyle = PureComponents.TreeView.BorderStyle.Solid;
		private FillStyle m_FillStyle = FillStyle.Flat;

		internal Node Node = null;

		#endregion

		#region construction

		/// <summary>
		/// Construction
		/// </summary>
		public NodePanel()
		{
			SetStyle(ControlStyles.ResizeRedraw, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			SetStyle(ControlStyles.ContainerControl, true);

			this.BackColor = Color.Transparent;
		}

		#endregion

		#region implementation

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint (e);

			AreaPainter.PaintRectangle(e.Graphics, 0, 0, Width, Height, m_FillStyle, this.BackColor, this.FadeColor, 
				this.BorderStyle, this.BorderColor);
		}

		#endregion

		#region helper functions

		#endregion

		#region event handlers

		#endregion

		#region properties

		/// <summary>
		/// Gets or sets background fade color
		/// </summary>
		[Category("Appearance")]
		[Editor(typeof (ColorUIEditor), typeof (UITypeEditor))]
		public Color FadeColor
		{
			get { return m_FadeColor; }

			set
			{
				m_FadeColor = value;

				this.Invalidate();
			}
		}
		private bool ShouldSerializeFadeColor()
		{
			return Color.White != this.FadeColor;
		}

		/// <summary>
		/// Gets or sets backgroudn color
		/// </summary>
		[Category("Appearance")]
		[Editor(typeof (ColorUIEditor), typeof (UITypeEditor))]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}

			set
			{
				base.BackColor = value;
			}
		}
		private bool ShouldSerializeBackColor()
		{
			return Color.Transparent != this.BackColor;
		}

		/// <summary>
		/// Gets or sets border color
		/// </summary>
		[Category("Appearance")]
		[Editor(typeof (ColorUIEditor), typeof (UITypeEditor))]
		public Color BorderColor
		{
			get { return m_BorderColor; }

			set
			{
				m_BorderColor = value;

				this.Invalidate();
			}
		}
		private bool ShouldSerializeBorderColor()
		{
			return Color.DarkGray != this.BorderColor;
		}

		/// <summary>
		/// Gets or sets border style
		/// </summary>
		[Category("Appearance")]
		[DefaultValue(PureComponents.TreeView.BorderStyle.Solid)]
		public BorderStyle BorderStyle
		{
			get { return m_BorderStyle; }

			set
			{
				m_BorderStyle = value;

				this.Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets fill style
		/// </summary>
		[Category("Appearance")]
		[DefaultValue(PureComponents.TreeView.FillStyle.Flat)]
		public FillStyle FillStyle
		{
			get { return m_FillStyle; }

			set
			{
				m_FillStyle = value;

				this.Invalidate();
			}
		}

		#endregion		
	}
}
