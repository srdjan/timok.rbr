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
	/// ExpandBoxStyle class representation
	/// </summary>
	[TypeConverter(typeof(ExpandBoxStyleConverter)), DesignTimeVisible(false)]	
	[Serializable]
	public class ExpandBoxStyle
	{
		#region private members

		private Color m_clrBorderColor = Color.Black;
		private Color m_clrForeColor = Color.Black;
		private Color m_clrBackColor = Color.SkyBlue;
		private ExpandBoxShape m_oShape = ExpandBoxShape.XP;

		private Node m_oParent = null;
		internal TreeView TreeView = null;

		#endregion

		#region construction

		public ExpandBoxStyle()
		{			
		}

		public ExpandBoxStyle(Node oParent)
		{			
			m_oParent = oParent;
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
		/// Applies the given style to the style instance
		/// </summary>
		/// <param name="oStyle">Style to apply</param>
		public void ApplyStyle(ExpandBoxStyle oStyle)
		{
			this.BackColor = oStyle.BackColor;
			this.BorderColor = oStyle.BorderColor;
			this.ForeColor = oStyle.ForeColor;
			this.Shape = oStyle.Shape;
		}

		/// <summary>
		/// Sets the parent of the style
		/// </summary>		
		internal void SetParent(Node oParent)
		{	
			m_oParent = oParent;
		}

		/// <summary>
		/// Invalidates the style
		/// </summary>
		internal void Invalidate()
		{
			if (m_oParent != null)
				m_oParent.Invalidate();

			if (TreeView != null)
				TreeView.Invalidate();
		}

		#endregion

		#region properties

		/// <summary>
		/// Defines the shape type of the node's box
		/// </summary>
		[Category("Appearance")] 
		[Description("Defines the shape type of the node's box.")]	
        [DefaultValue(ExpandBoxShape.XP)]
		public ExpandBoxShape Shape
		{
			get
			{
				return m_oShape;
			}

			set
			{
				m_oShape = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Defines the border color of the node's box
		/// </summary>
		[Category("Appearance")] 
		[Description("Defines the border color of the node's box.")]	
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
            return m_clrBorderColor != Color.Black;
        }

		/// <summary>
		/// Defines the line color of the node's box
		/// </summary>
		[Category("Appearance")] 
		[Description("Defines the line/forecolor of the node's box.")]	
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
		/// Defines the back color of the node's box
		/// </summary>
		[Category("Appearance")] 
		[Description("Defines the back color of the node's box.")]		
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
            return m_clrBackColor != Color.SkyBlue;
        }

		#endregion
	}

    /// <summary>
    /// Node box shape definition
    /// </summary>
    public enum ExpandBoxShape
    {
        /// <summary>
        /// XP like shape
        /// </summary>
        XP = 0,

        /// <summary>
        /// Flat shape
        /// </summary>
        Flat = 1,

        /// <summary>
        /// Transparent shape
        /// </summary>
        Transparent = 2
    }

	/// <summary>
	/// Implements the conversion functions for the ExpandBoxStyle
	/// </summary>
	internal sealed class ExpandBoxStyleConverter : System.ComponentModel.ExpandableObjectConverter
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
				ConstructorInfo ctor = typeof(ExpandBoxStyle).GetConstructor(new Type[] {});

				if (ctor != null) 
				{
					return new InstanceDescriptor(ctor, new object[] {}, false);
				}
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}			
	}
}
