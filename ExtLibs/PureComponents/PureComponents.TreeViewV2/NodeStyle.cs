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
using System.Drawing.Design;
using System.Reflection;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using PureComponents.TreeView.Design;

namespace PureComponents.TreeView
{
	/// <summary>
	/// Wrapps the functionality of the Node style
	/// </summary>
	[TypeConverter(typeof(NodeStyleConverter)), DesignTimeVisible(false)]
	[Serializable]
	public class NodeStyle
	{
		#region private members
        			
		private Color m_clrForeColor = Color.Black;
		private Color m_clrUnderlineColor = Color.Red;
		private UnderlineStyle m_oUnderlineStyle = UnderlineStyle.Tilde;
		private Color m_clrSelectedForeColor = Color.Black;
		private Color m_clrSelectedBackColor = Color.CornflowerBlue;
		private Color m_clrSelectedBorderColor = Color.RoyalBlue;
		private Color m_clrHoverBackColor = Color.Moccasin;
		private Color m_clrHoverForeColor = Color.Black;
		private Font m_fntFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular);		
		private FillStyle m_eSelectedFillStyle = FillStyle.VerticalFading;

		private CheckBoxStyle m_oCheckBoxStyle = null;
		private ExpandBoxStyle m_oExpandBoxStyle = null;
		private NodeTooltipStyle m_oTooltipStyle = null;

		private Node m_oParent;
		internal TreeView TreeView = null;
		#endregion

		#region construction
		/// <summary>
		/// Default construction
		/// </summary>
		public NodeStyle()
		{
			m_oParent = null;

			m_oCheckBoxStyle = new CheckBoxStyle(null);
			m_oExpandBoxStyle = new ExpandBoxStyle(null);
			m_oTooltipStyle = new NodeTooltipStyle(null);
		}

		/// <summary>
		/// Initializes the parent node object
		/// </summary>
		/// <param name="oParent">Node object to be associated with the style</param>
		public NodeStyle(Node oParent)
		{
			m_oParent = oParent;

			m_oCheckBoxStyle = new CheckBoxStyle(oParent);
			m_oExpandBoxStyle = new ExpandBoxStyle(oParent);
			m_oTooltipStyle = new NodeTooltipStyle(oParent);
		}
		#endregion

		#region helper functions
		#endregion

		#region implementation

		/// <summary>
		/// Overriden.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Empty;
		}

		/// <summary>
		/// Invalidates the style and forces the parent object to redraw
		/// </summary>
		internal void Invalidate()
		{
			if (m_oParent != null)
			{
				m_oParent.Invalidate();
				return;
			}

			if (TreeView != null)
				TreeView.Invalidate();
		}

		/// <summary>
		/// Applies the given style to the style instance
		/// </summary>
		/// <param name="oStyle">Style to apply</param>
		public void ApplyStyle(NodeStyle oStyle)
		{			
			this.Font = new Font(oStyle.Font, oStyle.Font.Style);
			this.CheckBoxStyle = new CheckBoxStyle(m_oParent);
			this.CheckBoxStyle.ApplyStyle(oStyle.CheckBoxStyle);
			this.ExpandBoxStyle = new ExpandBoxStyle(m_oParent);
			this.ExpandBoxStyle.ApplyStyle(oStyle.ExpandBoxStyle);
			this.ForeColor = oStyle.ForeColor;
			this.HoverBackColor = oStyle.HoverBackColor;
			this.HoverForeColor = oStyle.HoverForeColor;			
			this.SelectedForeColor = oStyle.SelectedForeColor;			
			this.SelectedBackColor = oStyle.SelectedBackColor;
			this.SelectedBorderColor = oStyle.SelectedBorderColor;			
			this.SelectedFillStyle = oStyle.SelectedFillStyle;
			this.SelectedForeColor = oStyle.SelectedForeColor;
			this.TooltipStyle = new NodeTooltipStyle(m_oParent);
			this.TooltipStyle.ApplyStyle(oStyle.TooltipStyle);
			this.UnderlineColor = oStyle.UnderlineColor;
			this.UnderlineStyle = oStyle.UnderlineStyle;
		}

		/// <summary>
		/// Applies the given style to the style instance
		/// </summary>
		/// <param name="oStyle">Style to apply</param>
		internal void ApplyStyleShallow(NodeStyle oStyle)
		{			
			this.Font = new Font(oStyle.Font, oStyle.Font.Style);
			this.ForeColor = oStyle.ForeColor;
			this.HoverBackColor = oStyle.HoverBackColor;
			this.HoverForeColor = oStyle.HoverForeColor;			
			this.SelectedForeColor = oStyle.SelectedForeColor;			
			this.SelectedBackColor = oStyle.SelectedBackColor;
			this.SelectedBorderColor = oStyle.SelectedBorderColor;			
			this.SelectedFillStyle = oStyle.SelectedFillStyle;
			this.SelectedForeColor = oStyle.SelectedForeColor;
			this.UnderlineColor = oStyle.UnderlineColor;
			this.UnderlineStyle = oStyle.UnderlineStyle;
		}
		#endregion

		#region properties
		/// <summary>
		/// The fore color of the selected TreeView Node
		/// </summary>
		[Description("The fore color of the selected TreeView Node.")]
		[Editor(typeof (ColorUIEditor), typeof (UITypeEditor))]
		public Color SelectedForeColor
		{			
			get
			{
				return m_clrSelectedForeColor;
			}

			set
			{
				m_clrSelectedForeColor = value;

				Invalidate();
			}
		}
        private bool ShouldSerializeSelectedForeColor()
        {
            return m_clrSelectedForeColor != Color.Black;
        }

		/// <summary>
		/// The back color of the selected TreeView Node
		/// </summary>
		[Description("The back color of the selected TreeView Node.")]
		[Editor(typeof (ColorUIEditor), typeof (UITypeEditor))]
		public Color SelectedBackColor
		{			
			get
			{
				return m_clrSelectedBackColor;
			}

			set
			{
				m_clrSelectedBackColor = value;

				Invalidate();
			}
		}
        private bool ShouldSerializeSelectedBackColor()
        {
            return m_clrSelectedBackColor != Color.CornflowerBlue;
        }

		/// <summary>
		/// The border color of the selected TreeView Node
		/// </summary>
		[Description("The border color of the selected TreeView Node.")]
		[Editor(typeof (ColorUIEditor), typeof (UITypeEditor))]
		public Color SelectedBorderColor
		{			
			get
			{
				return m_clrSelectedBorderColor;
			}

			set
			{
				m_clrSelectedBorderColor = value;

				Invalidate();
			}
		}
        private bool ShouldSerializeSelectedBorderColor()
        {
            return m_clrSelectedBorderColor != Color.RoyalBlue;
        }

		/// <summary>
		/// The FillStyle of the background of the node while being selected
		/// </summary>
		[Description("The FillStyle of the background of the node while being selected.")]
        [DefaultValue(FillStyle.VerticalFading)]
		public FillStyle SelectedFillStyle
		{
			get
			{	
				return m_eSelectedFillStyle;
			}

			set
			{
				m_eSelectedFillStyle = value;

				Invalidate();
			}
		}

		/// <summary>
		/// The fore color of the TreeView Node
		/// </summary>
		[Description("The fore color of the TreeView Node.")]
		[Editor(typeof (ColorUIEditor), typeof (UITypeEditor))]
		public Color ForeColor
		{
			get
			{
				return m_clrForeColor;
			}

			set
			{
				m_clrForeColor = value;

				Invalidate();
			}
		}
        private bool ShouldSerializeForeColor()
        {
            return m_clrForeColor != Color.Black;
        }

		/// <summary>
		/// Back color of the highlighted node
		/// </summary>
		[Description("Back color of the highlighted node.")]
		[Editor(typeof (ColorUIEditor), typeof (UITypeEditor))]
		public Color HoverBackColor
		{
			get
			{
				return m_clrHoverBackColor;
			}

			set
			{
				m_clrHoverBackColor = value;
			}
		}
        private bool ShouldSerializeHoverBackColor()
        {
            return m_clrHoverBackColor != Color.Moccasin;
        }

		/// <summary>
		/// Fore color of the highlighted node
		/// </summary>
		[Description("Fore color of the highlighted node.")]
		[Editor(typeof (ColorUIEditor), typeof (UITypeEditor))]
		public Color HoverForeColor
		{
			get
			{
				return m_clrHoverForeColor;
			}

			set
			{
				m_clrHoverForeColor = value;
			}
		}
        private bool ShouldSerializeHoverForeColor()
        {
            return m_clrHoverForeColor != Color.Black;
        }

		/// <summary>
		/// The underline color of the TreeView Node
		/// </summary>
		[Description("The underline color of the TreeView Node.")]
		[Editor(typeof (ColorUIEditor), typeof (UITypeEditor))]
		public Color UnderlineColor
		{
			get
			{
				return m_clrUnderlineColor;
			}

			set
			{
				m_clrUnderlineColor = value;

				Invalidate();
			}
		}
        private bool ShouldSerializeUnderlineColor()
        {
            return m_clrUnderlineColor != Color.Red;
        }

		/// <summary>
		/// The underline style of the TreeView Node
		/// </summary>
		[Description("The underline style of the TreeView Node.")]
        [DefaultValue(UnderlineStyle.Tilde)]
		public UnderlineStyle UnderlineStyle
		{
			get
			{
				return m_oUnderlineStyle;
			}

			set
			{
				m_oUnderlineStyle = value;

				Invalidate();
			}
		}

		/// <summary>
		/// The checkbox style of the TreeView Node
		/// </summary>
		[Description("The checkbox style of the TreeView Node.")]
		public CheckBoxStyle CheckBoxStyle
		{
			get
			{
				return m_oCheckBoxStyle;
			}

			set
			{
				m_oCheckBoxStyle = value;

				Invalidate();
			}
		}

		/// <summary>
		/// The style of the TreeView Node's tooltip
		/// </summary>
		[Description("The style of the TreeView Node's tooltip.")]
		public NodeTooltipStyle TooltipStyle
		{
			get
			{
				return m_oTooltipStyle;
			}

			set
			{
				m_oTooltipStyle = value;
				Invalidate();
			}
		}

		/// <summary>
		/// The style of the TreeView Node's collapse/expand box
		/// </summary>
		[Description("The style of the TreeView Node's collapse/expand box.")]
		public ExpandBoxStyle ExpandBoxStyle
		{
			get
			{
				return m_oExpandBoxStyle;
			}

			set
			{
				m_oExpandBoxStyle = value;

				Invalidate();
			}
		}

		/// <summary>
		/// The font of the TreeView Node
		/// </summary>
		[Description("The font of the TreeView Node.")]
		public Font Font
		{
			get
			{
				return m_fntFont;
			}

			set
			{
				m_fntFont = value;

				Invalidate();
			}
		}		
		
		/// <summary>
		/// The parent object accessor
		/// </summary>
		[Browsable(false)]
        [DefaultValue(null)]
		public Node Parent
		{
			get
			{
				return m_oParent;
			}

			set
			{
				m_oParent = value;				
			}
		}
		#endregion
	}

	/// <summary>
	/// Implements the conversion functions for the NodeStyle
	/// </summary>
	internal sealed class NodeStyleConverter : System.ComponentModel.ExpandableObjectConverter
	{			
		/// <summary>
		/// Returns true if the object can convert to that type.
		/// </summary>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) 
		{
			if (destinationType == typeof(InstanceDescriptor)) 
			{
				return true;
			}
				
			return base.CanConvertTo(context, destinationType);
		}

		/// <summary>
		/// Converts the object to the requested type.
		/// </summary>
		public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType) 
		{
			if (destinationType == null) 
			{
				throw new ArgumentNullException("null argument");
			}

			if (destinationType == typeof(InstanceDescriptor)) 
			{
				ConstructorInfo ctor = typeof(NodeStyle).GetConstructor(new Type[] {});

				if (ctor != null) 
				{
					return new InstanceDescriptor(ctor, new object[] {}, false);
				}
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}
			
	}
}
