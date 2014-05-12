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
	/// NodeBackgroundStyle represents object holding information about node's background.
	/// </summary>
	[TypeConverter(typeof(NodeBackgroundStyleConverter)), DesignTimeVisible(false)]
	[Serializable]
	public class NodeBackgroundStyle
	{
		#region private members

		private Color m_BackColor;
		private Color m_FadeColor;
		private FillStyle m_FillStyle;

		private bool m_Visible;

		#endregion

		#region construction

		/// <summary>
		/// Construction
		/// </summary>
		public NodeBackgroundStyle()
		{
			m_BackColor = Color.FromArgb(255, 192, 128);
			m_FadeColor = Color.FromArgb(255, 216, 197);
			m_FillStyle = FillStyle.Flat;
			m_Visible = false;
		}

		#endregion

		#region implementation

		/// <summary>
		/// 
		/// </summary>
		internal event EventHandler Invalidate;

		/// <summary>
		/// Overriden.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Empty;
		}

		#endregion

		#region properties

		/// <summary>
		/// Gets or sets the back color.
		/// </summary>
		[Description("Gets or sets the back color.")]
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
			return m_BackColor != Color.FromArgb(255, 192, 128);
		}

		/// <summary>
		/// Gets or sets the fade color.
		/// </summary>
		[Description("Gets or sets the fade color.")]
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
			return m_FadeColor != Color.FromArgb(255, 216, 197);
		}		

		/// <summary>
		/// Gets or sets the fill style.
		/// </summary>
		[DefaultValue(FillStyle.Flat)]
		[Description("Gets or sets the fill style.")]
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
		/// Gets or sets whether the Node's background is visible.
		/// </summary>
		[DefaultValue(false)]
		[Description("Gets or sets whether the Node's background is visible.")]
		public bool Visible
		{
			get { return m_Visible; }

			set
			{
				m_Visible = value;

				if (Invalidate != null)
					Invalidate(this, EventArgs.Empty);
			}
		}

		#endregion		
	}

	/// <summary>
	/// 
	/// </summary>
	internal sealed class NodeBackgroundStyleConverter : System.ComponentModel.ExpandableObjectConverter
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
				ConstructorInfo ctor = typeof(NodeBackgroundStyle).GetConstructor(new Type[] {});

				if (ctor != null) 
				{
					return new InstanceDescriptor(ctor, new object[] {}, false);
				}
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}
			
	}
}
