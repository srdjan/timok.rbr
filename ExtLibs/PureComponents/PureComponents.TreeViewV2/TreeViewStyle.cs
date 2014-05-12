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
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using PureComponents.TreeView.Design;

namespace PureComponents.TreeView
{
	/// <summary>
	/// The TreeStyle abstraction
	/// </summary>
	[TypeConverter(typeof(TreeViewStyleConverter)), DesignTimeVisible(false)]	
	[Serializable]
	public class TreeViewStyle
	{
		#region private members
		protected TreeView m_oTreeView = null;

		protected Color m_clrFlashColor = Color.Red;
		protected Color m_clrBackColor = Color.White;
		protected Color m_clrFadeColor = Color.White;
		protected bool m_bFullRowSelect = true;
		protected bool m_bAutoCollapse = false;
		protected bool m_bShowLines = true;		
		protected bool m_bShowPlusMinus = true;
		protected bool m_bShowSubitemsIndicator = false;
		protected bool m_bHighlightSelectedPath = true;
		protected Color m_clrLineColor = Color.Gray;	
		protected LineStyle m_oLineStyle = LineStyle.Solid;
		private bool m_bTrackNodeHover = false;

		protected NodeStyle m_oNodeStyle;

		private Color m_clrBorderColor = Color.Black;		
		private BorderStyle m_oBorderStyle = BorderStyle.Solid;		
		private FillStyle m_eFillStyle = FillStyle.Flat;

		private int m_NodeSpaceVertical = 0;
		private int m_NodeSpaceHorizontal = 0;

		#endregion

		#region construction
		/// <summary>
		/// Default construction
		/// </summary>
		public TreeViewStyle()
		{
			m_oTreeView = null;
			m_oNodeStyle = new NodeStyle();

			AutoCollapse = false;
			BackColor = System.Drawing.Color.White;
			BorderColor = System.Drawing.Color.DimGray;
			BorderStyle = PureComponents.TreeView.BorderStyle.Solid;
			FadeColor = System.Drawing.Color.Khaki;
			FillStyle = PureComponents.TreeView.FillStyle.Flat;
			FlashColor = System.Drawing.Color.Red;
			HighlightSelectedPath = true;
			LineColor = System.Drawing.Color.Gray;
			LineStyle = PureComponents.TreeView.LineStyle.Solid;
			ShowLines = true;
			ShowPlusMinus = true;
			ShowSubitemsIndicator = false;
			TrackNodeHover = true;											
		}

		/// <summary>
		/// Construction of the style using the Group object
		/// </summary>
		/// <param name="oParent">TreeView object to be associated as a parent with the style</param>
		public TreeViewStyle(TreeView oParent) : this()
		{
			m_oTreeView = oParent;			
		}
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
			if (TreeView != null)
				TreeView.Invalidate();
		}

		/// <summary>
		/// Applies the given style to the style instance
		/// </summary>
		/// <param name="oStyle">Style to apply</param>
		public void ApplyStyle(TreeViewStyle oStyle)
		{
			this.BorderColor = oStyle.BorderColor;
			this.FadeColor = oStyle.FadeColor;
			this.BorderStyle = oStyle.BorderStyle;
			this.AutoCollapse = oStyle.AutoCollapse;
			this.BackColor = oStyle.BackColor;
			this.FlashColor = oStyle.FlashColor;
			this.HighlightSelectedPath = oStyle.HighlightSelectedPath;
			this.LineColor = oStyle.LineColor;
			this.LineStyle = oStyle.LineStyle;
			this.ShowLines = oStyle.ShowLines;
			this.ShowPlusMinus = oStyle.ShowPlusMinus;
			this.ShowSubitemsIndicator = oStyle.ShowSubitemsIndicator;
			this.m_bTrackNodeHover = oStyle.TrackNodeHover;			
			this.FillStyle = oStyle.FillStyle;
		}

		#endregion

		#region helper functions
		#endregion

		#region properties

		/// <summary>
		/// Gets or sets the border color of the TreeView control.
		/// </summary>
		[Description("Gets or sets the border color of the TreeView control.")]
		[Editor(typeof (ColorUIEditor), typeof (UITypeEditor))]
		public Color BorderColor
		{
			get
			{
				return m_clrBorderColor;
			}

			set
			{
				m_clrBorderColor = value;

				Invalidate();
			}
		}
        private bool ShouldSerializeBorderColor()
        {
            return m_clrBorderColor != Color.DimGray;
        }

		/// <summary>
		/// Gets or sets the border style of the TreeView control.
		/// </summary>
		[Description("Gets or sets the border style of the TreeView control.")]
        [DefaultValue(BorderStyle.Solid)]
		public BorderStyle BorderStyle
		{
			get
			{
				return m_oBorderStyle;
			}

			set
			{
				m_oBorderStyle = value;

				Invalidate();
			}
		}	

		/// <summary>
		/// The TreeView object holder
		/// </summary>
		internal TreeView TreeView
		{
			get
			{
				return m_oTreeView;
			}

			set
			{
				m_oTreeView = value;

				m_oNodeStyle.TreeView = TreeView;
				m_oNodeStyle.CheckBoxStyle.TreeView = TreeView;
				m_oNodeStyle.ExpandBoxStyle.TreeView = TreeView;
				m_oNodeStyle.TooltipStyle.TreeView = TreeView;
			}
		}

		/// <summary>
		/// Gets or sets the fillstyle of the TreeView control.
		/// </summary>
		[Description("Gets or sets the fillstyle color of the TreeView control.")]
        [DefaultValue(FillStyle.Flat)]
		public FillStyle FillStyle
		{
			get
			{
				return m_eFillStyle;
			}

			set
			{
				m_eFillStyle = value;

				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the back color of the TreeView control.
		/// </summary>
		[Description("Gets or sets the back color of the TreeView control.")]
		[Editor(typeof (ColorUIEditor), typeof (UITypeEditor))]
		public Color BackColor
		{
			get
			{
				return m_clrBackColor;
			}

			set
			{
				m_clrBackColor = value;

				Invalidate();
			}
		}
        private bool ShouldSerializeBackColor()
        {
            return m_clrBackColor != Color.White;
        }

		/// <summary>
		/// Gets or sets the back color of the TreeView control.
		/// </summary>
		[Description("Gets or sets the back color of the TreeView control for fading effect.")]
		[Editor(typeof (ColorUIEditor), typeof (UITypeEditor))]
		public Color FadeColor
		{
			get
			{
				return m_clrFadeColor;
			}

			set
			{
				m_clrFadeColor = value;

				Invalidate();
			}
		}
        private bool ShouldSerializeFadeColor()
        {
            return m_clrFadeColor != Color.Khaki;
        }

		/// <summary>
		/// Gets or sets a value indicating what color is used to draw lines between nodes in the TreeView control.
		/// </summary>
		[Description("Gets or sets a value indicating what color is used to draw lines between nodes in the TreeView control.")]
		[Editor(typeof (ColorUIEditor), typeof (UITypeEditor))]
		public Color LineColor
		{
			get
			{	
				return m_clrLineColor;
			}

			set
			{
				m_clrLineColor = value;

				Invalidate();
			}
		}
        private bool ShouldSerializeLineColor()
        {
            return m_clrLineColor != Color.Gray;
        }

		/// <summary>
		/// Gets or sets the back color of the TreeView control.
		/// </summary>
		[Description("Gets or sets the back color of the TreeView control.")]
		[Editor(typeof (ColorUIEditor), typeof (UITypeEditor))]
		public Color FlashColor
		{
			get
			{
				return m_clrFlashColor;
			}

			set
			{
				m_clrFlashColor = value;

				Invalidate();
			}
		}
        private bool ShouldSerializeFlashColor()
        {
            return m_clrFlashColor != Color.Red;
        }

		/// <summary>
		/// Gets or sets a value indicating how lines are drawn between nodes in the TreeView control.
		/// </summary>
		[Description("Gets or sets a value indicating how lines are drawn between nodes in the TreeView control.")]
        [DefaultValue(LineStyle.Solid)]
		public LineStyle LineStyle
		{
			get
			{
				return m_oLineStyle;
			}	

			set
			{
				m_oLineStyle = value;

				Invalidate();
			}
		}

		/// <summary>
		/// Specifies the behaviour when the node is selected.
		/// </summary>
		[Description("Specifies the behaviour when the node is selected.")]
        [DefaultValue(false)]
		public bool AutoCollapse
		{
			get
			{
				return m_bAutoCollapse;
			}

			set
			{
				m_bAutoCollapse = value;

				Invalidate();
			}
		}

		/// <summary>
		/// Specifies whether the full node row should be selected when node is selected
		/// </summary>
		[Description("Specifies whether the full node row should be selected when node is selected.")]
        [DefaultValue(true)]
		public bool FullRowSelect
		{
			get
			{
				return m_bFullRowSelect;
			}

			set
			{
				m_bFullRowSelect = value;

				Invalidate();
			}
		}

		/// <summary>
		/// Specifies the behaviour when the mouse leaves TreeView object
		/// </summary>
		[Description("Specifies the behaviour when the mouse leaves TreeView object.")]
        [DefaultValue(true)]
		public bool HighlightSelectedPath
		{
			get
			{
				return m_bHighlightSelectedPath;
			}

			set
			{
				m_bHighlightSelectedPath = value;

				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether lines are drawn between nodes in the TreeView control.
		/// </summary>
		[Description("Gets or sets a value indicating whether lines are drawn between nodes in the TreeView control.")]
        [DefaultValue(true)]
		public bool ShowLines
		{
			get
			{
				return m_bShowLines;
			}

			set
			{
				m_bShowLines = value;

				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether plus-sign (+) and minus-sign (-) buttons are displayed next to nodes that contain child nodes.
		/// </summary>
		[Description("Gets or sets a value indicating whether plus-sign (+) and minus-sign (-) buttons are displayed next to nodes that contain child nodes.")]
        [DefaultValue(true)]
		public bool ShowPlusMinus
		{
			get
			{
				return m_bShowPlusMinus;
			}

			set
			{
				m_bShowPlusMinus = value;

				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether arrow-sign is displayed next to nodes that contain child nodes.
		/// </summary>
		[Description("Gets or sets a value indicating whether arrow-sign is displayed next to nodes that contain child nodes.")]
        [DefaultValue(false)]
		public bool ShowSubitemsIndicator
		{
			get
			{
				return m_bShowSubitemsIndicator;
			}

			set
			{
				m_bShowSubitemsIndicator = value;

				Invalidate();
			}
		}

		/// <summary>
		/// The additional horizontal spacing
		/// </summary>
		[Description("The additional horizontal spacing.")]
        [DefaultValue(0)]
		public int NodeSpaceHorizontal
		{
			get
			{
				return m_NodeSpaceHorizontal;
			}

			set
			{
				if (value < 0)
					return;

				m_NodeSpaceHorizontal = value;

				Invalidate();
			}
		}

		/// <summary>
		/// The additional vertical spacing
		/// </summary>
		[Description("The additional vertical spacing.")]
        [DefaultValue(0)]
		public int NodeSpaceVertical
		{
			get
			{
				return m_NodeSpaceVertical;
			}

			set
			{
				if (value < 0)
					return;

				m_NodeSpaceVertical = value;

				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the node style instance
		/// </summary>
		[Description(" Gets or sets the node style instance.")]
		public NodeStyle NodeStyle
		{
			get
			{
                return m_oNodeStyle; 
			}

			set
			{
				m_oNodeStyle = value;

				Invalidate();
			}
		}
		
		/// <summary>
		/// Specifies the behaviour of the mouse move
		/// </summary>
		[Description("Specifies the behaviour of the mouse move.")]
        [DefaultValue(true)]
		public bool TrackNodeHover
		{
			get
			{
				return m_bTrackNodeHover;
			}

			set
			{
				m_bTrackNodeHover = value;

				Invalidate();
			}
		}

		#endregion
	}

	/// <summary>
	/// Implements the conversion functions for the TreeViewStyle
	/// </summary>
	internal sealed class TreeViewStyleConverter : System.ComponentModel.ExpandableObjectConverter
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
				ConstructorInfo ctor = typeof(TreeViewStyle).GetConstructor(new Type[] {});

				if (ctor != null) 
				{
					return new InstanceDescriptor(ctor, new object[] {}, false);
				}
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}			
	}
}
