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
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using PureComponents.TreeView.Design;

namespace PureComponents.TreeView
{
	/// <summary>
	/// TreeViewPathSelector style
	/// </summary>
	[TypeConverter(typeof(TreeViewPathSelectorStyleConverter)), DesignTimeVisible(false)]	
    [ToolboxItem(false)]
	[Serializable]
	public class TreeViewPathSelectorStyle
	{
		#region private members
		
		private Color m_BackColor;
		private Color m_ForeColor;
		private Color m_BorderColor;
		private Color m_FadeColor;
		private Color m_SelectionColor;
		
		private BorderStyle m_BorderStyle;	
		private FillStyle m_FillStyle;
		private FillStyle m_FillStyleSelection;

		#endregion

		#region construction

		/// <summary>
		/// Construction
		/// </summary>
		public TreeViewPathSelectorStyle() : this(Color.White, Color.Black, Color.DimGray, Color.White,
			Color.CornflowerBlue, PureComponents.TreeView.BorderStyle.Solid, FillStyle.VerticalFading,
			FillStyle.VistaFading)
		{
		}

		/// <summary>
		/// Parametrized contruction
		/// </summary>
		/// <param name="backColor"></param>
		/// <param name="foreColor"></param>
		/// <param name="borderColor"></param>
		/// <param name="fadeColor"></param>
		/// <param name="selectionColor"></param>
		/// <param name="borderStyle"></param>
		/// <param name="fillStyle"></param>
		public TreeViewPathSelectorStyle(Color backColor, Color foreColor, Color borderColor, Color fadeColor, 
			Color selectionColor, BorderStyle borderStyle, FillStyle fillStyle, FillStyle fillStyleSelection)
		{
			m_BackColor = backColor;
			m_ForeColor = foreColor;
			m_BorderColor = borderColor;
			m_FadeColor = fadeColor;
			m_SelectionColor = selectionColor;
			m_BorderStyle = borderStyle;
			m_FillStyle = fillStyle;
			m_FillStyleSelection = fillStyleSelection;
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

		#endregion

		#region helper functions

		#endregion

		#region event handlers
		
		internal event EventHandler Invalidate;

		#endregion

		#region properties

		/// <summary>
		/// Gets or sets the back color of the TreeViewPathSelector control.
		/// </summary>
		[Description("Gets or sets the back color of the TreeViewPathSelector control.")]
		[Editor(typeof (ColorUIEditor), typeof (UITypeEditor))]
		public Color BackColor
		{
			get { return m_BackColor; }
			set
			{
				m_BackColor = value;
				
				if (Invalidate != null)
					Invalidate(this, EventArgs.Empty);
			}
		}
        private bool ShouldSerializeBackColor()
        {
            return m_BackColor != Color.White;
        }

		/// <summary>
		/// Gets or sets the fore color of the TreeViewPathSelector control.
		/// </summary>
		[Description("Gets or sets the fore color of the TreeViewPathSelector control.")]
		[Editor(typeof (ColorUIEditor), typeof (UITypeEditor))]
		public Color ForeColor
		{
			get { return m_ForeColor; }
			
			set
			{
				m_ForeColor = value;
				
				if (Invalidate != null)
					Invalidate(this, EventArgs.Empty);
			}
		}
        private bool ShouldSerializeForeColor()
        {
            return m_ForeColor != Color.Black;
        }

		/// <summary>
		/// Gets or sets the border color of the TreeViewPathSelector control.
		/// </summary>
		[Description("Gets or sets the border color of the TreeViewPathSelector control.")]
		[Editor(typeof (ColorUIEditor), typeof (UITypeEditor))]
		public Color BorderColor
		{
			get { return m_BorderColor; }
			
			set
			{
				m_BorderColor = value;
				
				if (Invalidate != null)
					Invalidate(this, EventArgs.Empty);
			}
		}
        private bool ShouldSerializeBorderColor()
        {
            return m_BorderColor != Color.DimGray;
        }

		/// <summary>
		/// Gets or sets the fade color of the TreeViewPathSelector control.
		/// </summary>
		[Description("Gets or sets the fade color of the TreeViewPathSelector control.")]
		[Editor(typeof (ColorUIEditor), typeof (UITypeEditor))]
		public Color FadeColor
		{
			get { return m_FadeColor; }
			
			set
			{
				m_FadeColor = value;
				
				if (Invalidate != null)
					Invalidate(this, EventArgs.Empty);
			}
		}
        private bool ShouldSerializeFadeColor()
        {
            return m_FadeColor != Color.White;
        }

		/// <summary>
		/// Gets or sets the selection color of the TreeViewPathSelector control.
		/// </summary>
		[Description("Gets or sets the selection color of the TreeViewPathSelector control.")]
		[Editor(typeof (ColorUIEditor), typeof (UITypeEditor))]
		public Color SelectionColor
		{
			get { return m_SelectionColor; }
			
			set
			{
				m_SelectionColor = value;
				
				if (Invalidate != null)
					Invalidate(this, EventArgs.Empty);
			}
		}
        private bool ShouldSerializeSelectionColor()
        {
            return m_SelectionColor != Color.CornflowerBlue;
        }

		/// <summary>
		/// Gets or sets the border style of the TreeViewPathSelector control.
		/// </summary>
		[Description("Gets or sets the border style of the TreeViewPathSelector control.")]
        [DefaultValue(BorderStyle.Solid)]
		public BorderStyle BorderStyle
		{
			get { return m_BorderStyle; }
			
			set
			{
				m_BorderStyle = value;
				
				if (Invalidate != null)
					Invalidate(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Gets or sets the fill style of the TreeViewPathSelector control.
		/// </summary>
		[Description("Gets or sets the fill style of the TreeViewPathSelector control.")]
        [DefaultValue(FillStyle.VerticalFading)]
		public FillStyle FillStyle
		{
			get { return m_FillStyle; }
			
			set
			{
				m_FillStyle = value;
				
				if (Invalidate != null)
					Invalidate(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Gets or sets the fill style for selection of an item in TreeViewPathSelector
		/// </summary>
		[Description("Gets or sets the fill style for selection of an item in TreeViewPathSelector.")]
        [DefaultValue(FillStyle.VistaFading)]
		public FillStyle FillStyleSelection
		{
			get { return m_FillStyleSelection; }
			
			set
			{
				m_FillStyleSelection = value;

				if (Invalidate != null)
					Invalidate(this, EventArgs.Empty);
			}
		}

		#endregion
	}
	
	/// <summary>
	/// Implements the conversion functions for the TreeViewPathSelector
	/// </summary>
	internal sealed class TreeViewPathSelectorStyleConverter : ExpandableObjectConverter
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
		public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, 
		                                 object value, Type destinationType) 
		{
			if (destinationType == null) 
			{
				throw new ArgumentNullException("null argument");
			}

			if (destinationType == typeof(InstanceDescriptor)) 
			{
				ConstructorInfo ctor = typeof(TreeViewPathSelectorStyle).GetConstructor(new Type[] {});

				if (ctor != null) 
				{
					return new InstanceDescriptor(ctor, new object[] {}, false);
				}
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}			
	}
}