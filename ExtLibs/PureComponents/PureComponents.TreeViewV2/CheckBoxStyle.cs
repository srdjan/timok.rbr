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
	/// The style class for the style definition of the Node's checkbox
	/// </summary>
	[TypeConverter(typeof(CheckBoxStyleConverter)), DesignTimeVisible(false)]
	[Serializable]
	public class CheckBoxStyle
	{
		#region private members
						
    	private CheckBoxBorderStyle m_BorderStyle = CheckBoxBorderStyle.XP;
		private Color m_BackColor = Color.White;
		private Color m_BorderColor = Color.Navy;
		private Color m_CheckColor = Color.LimeGreen;
		private Color m_HoverBackColor = Color.DarkOrange;
		private Color m_HoverBorderColor = Color.Navy;
		private Color m_HoverCheckColor = Color.LimeGreen;

		private Node m_Parent = null;
		internal TreeView TreeView = null;
		#endregion

		#region construction
		public CheckBoxStyle() {}

		public CheckBoxStyle(Node parent) : this()
		{
			m_Parent = parent;
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

		internal void Invalidate()
		{
			if (m_Parent != null)
				m_Parent.Invalidate();

			if (TreeView != null)
				TreeView.Invalidate();
		}

		public void ApplyStyle(CheckBoxStyle oStyle)
		{
			this.BorderColor = oStyle.BorderColor;
			this.BackColor = oStyle.BackColor;
			this.BorderStyle = oStyle.BorderStyle;
			this.CheckColor = oStyle.CheckColor;
			this.HoverBackColor = oStyle.HoverBackColor;
			this.HoverBorderColor = oStyle.HoverBorderColor;
			this.HoverCheckColor = oStyle.HoverCheckColor;			
		}
		#endregion

		#region properties
		/// <summary>
		/// Defines the style of the check box
		/// </summary>
		[Category("Appearance")] 
		[Description("Defines the style of the check box.")]
		[DefaultValue(CheckBoxBorderStyle.XP)]
		public CheckBoxBorderStyle BorderStyle
		{
			get
			{
				return m_BorderStyle;
			}

			set
			{
				m_BorderStyle = value;

				Invalidate();
			}
		}

		/// <summary>
		/// Defines the back color of the check box
		/// </summary>
		[Category("Appearance")] 
		[Description("Defines the back color of the check box.")]		
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
            return m_BackColor != Color.White;
        }

		/// <summary>
		/// Defines the color of the check box
		/// </summary>
		[Category("Appearance")] 
		[Description("Defines the color of the check box.")]		
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
            return m_BorderColor != Color.Navy;
        }

		/// <summary>
		/// Defines the color of the check
		/// </summary>
		[Category("Appearance")] 
		[Description("Defines the color of the check.")]		
		[Editor(typeof (ColorUIEditor), typeof (UITypeEditor))]
		public Color CheckColor
		{
			get
			{
				return m_CheckColor;
			}

			set
			{
				m_CheckColor = value;

				Invalidate();
			}
		}
        private bool ShouldSerializeCheckColor()
        {
            return m_CheckColor != Color.LimeGreen;
        }

		/// <summary>
		/// Defines the color of the check box when mouse is over the node
		/// </summary>
		[Category("Appearance")] 
		[Description("Defines the color of the check box when mouse is over the node.")]		
		[Editor(typeof (ColorUIEditor), typeof (UITypeEditor))]
		public Color HoverBorderColor
		{
			get
			{
				return m_HoverBorderColor;
			}

			set
			{
				m_HoverBorderColor = value;

				Invalidate();
			}
		}
        private bool ShouldSerializeHoverBorderColor()
        {
            return m_HoverBorderColor != Color.Navy;
        }

		/// <summary>
		/// Defines the hover back color of the check box
		/// </summary>
		[Category("Appearance")] 
		[Description("Defines the back color of the check box.")]		
		[Editor(typeof (ColorUIEditor), typeof (UITypeEditor))]
		public Color HoverBackColor
		{
			get
			{
				return m_HoverBackColor;
			}

			set
			{
				m_HoverBackColor = value;

				Invalidate();
			}
		}
        private bool ShouldSerializeHoverBackColor()
        {
            return m_HoverBackColor != Color.DarkOrange;
        }

		/// <summary>
		/// Defines the color of the check when mouse is over the node
		/// </summary>
		[Category("Appearance")] 
		[Description("Defines the color of the check when mouse is over the node.")]		
		[Editor(typeof (ColorUIEditor), typeof (UITypeEditor))]
		public Color HoverCheckColor
		{
			get
			{
				return m_HoverCheckColor;
			}

			set
			{
				m_HoverCheckColor = value;

				Invalidate();
			}
		}
        private bool ShouldSerializeHoverCheckColor()
        {
            return m_HoverCheckColor != Color.LimeGreen;
        }
		
		#endregion
	}

    /// <summary>
    /// Border style for checkbox area of a Node
    /// </summary>
    public enum CheckBoxBorderStyle
    {
        /// <summary>
        /// None border
        /// </summary>
        None = 0,

        /// <summary>
        /// Solid border
        /// </summary>
        Solid = 1,

        /// <summary>
        /// Dotted border
        /// </summary>
        Dot = 2,

        /// <summary>
        /// XP like border
        /// </summary>
        XP = 3
    }

	/// <summary>
	/// Implements the conversion functions for the CheckBoxStyle
	/// </summary>
	internal sealed class CheckBoxStyleConverter : System.ComponentModel.ExpandableObjectConverter
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
				ConstructorInfo ctor = typeof(CheckBoxStyle).GetConstructor(new Type[] {});

				if (ctor != null) 
				{
					return new InstanceDescriptor(ctor, new object[] {}, false);
				}
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}
			
	}

}
