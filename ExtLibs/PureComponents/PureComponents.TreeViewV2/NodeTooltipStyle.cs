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
	/// NodeTooltipStyle implementation
	/// </summary>
	[TypeConverter(typeof(NodeTootipStyleConverter)), DesignTimeVisible(false)]	
	[Serializable]
	public class NodeTooltipStyle
	{
		#region private members

		private Color m_BorderColor = Color.RosyBrown;
		private Color m_BackColor = Color.WhiteSmoke;
		private Color m_ForeColor = Color.Black;
		private Font m_Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular);		

		private Node m_oParent = null;
		internal TreeView TreeView = null;

		#endregion

		#region construction

		public NodeTooltipStyle()
		{			
		}

		public NodeTooltipStyle(Node oParent)
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
		public void ApplyStyle(NodeTooltipStyle oStyle)
		{
			this.BackColor = oStyle.BackColor;
			this.BorderColor = oStyle.BorderColor;
			this.Font = oStyle.Font;
			this.ForeColor = oStyle.ForeColor;			
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
		/// Defines the font of the node's tooltip
		/// </summary>
		[Category("Appearance")] 
		[Description("Defines the font of the node's tooltip.")]	
		public Font Font
		{
			get
			{
				return m_Font;
			}

			set
			{
				m_Font = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Defines the fore color of the node's tooltip
		/// </summary>
		[Category("Appearance")] 
		[Description("Defines the fore color of the node's tooltip.")]	
		[Editor(typeof (ColorUIEditor), typeof (UITypeEditor))]
		public Color ForeColor
		{
			get
			{
				return m_ForeColor;
			}

			set
			{
				m_ForeColor = value;
				Invalidate();
			}
		}
        private bool ShouldSerializeForeColor()
        {
            return m_ForeColor != Color.Black;
        }

		/// <summary>
		/// Defines the back color of the node's tooltip
		/// </summary>
		[Category("Appearance")] 
		[Description("Defines the back color of the node's tooltip.")]	
		[Editor(typeof (ColorUIEditor), typeof (UITypeEditor))]
		public Color BackColor
		{
			get
			{
				return m_BackColor;
			}

			set
			{
				m_BackColor = value;
				Invalidate();
			}
		}
        private bool ShouldSerializeBackColor()
        {
            return m_BackColor != Color.WhiteSmoke;
        }

		/// <summary>
		/// Defines the border color of the node's tooltip
		/// </summary>
		[Category("Appearance")] 
		[Description("Defines the border color of the node's tooltip.")]	
		[Editor(typeof (ColorUIEditor), typeof (UITypeEditor))]
		public Color BorderColor
		{
			get
			{
				return m_BorderColor;
			}

			set
			{
				m_BorderColor = value;
				Invalidate();
			}
		}
        private bool ShouldSerializeBorderColor()
        {
            return m_BorderColor != Color.RosyBrown;
        }

		#endregion
	}

	/// <summary>
	/// Implements the conversion functions for the NodeTootipStyle
	/// </summary>
	internal sealed class NodeTootipStyleConverter : System.ComponentModel.ExpandableObjectConverter
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
				ConstructorInfo ctor = typeof(NodeTooltipStyle).GetConstructor(new Type[] {});

				if (ctor != null) 
				{
					return new InstanceDescriptor(ctor, new object[] {}, false);
				}
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}			
	}
}
