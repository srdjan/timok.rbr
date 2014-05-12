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
	public enum NodeFlagStyle
	{
		Flag = 0,		
		Exclamation = 1,
		Point = 2,
		Diamond = 3,
		Arrow = 4,
		ArrowUp = 5,
		ArrowDown = 6
	}

	/// <summary>
	/// Node Flag object implementation.
	/// </summary>
	[TypeConverter(typeof(NodeFlagConverter)), DesignTimeVisible(false)]
	[Serializable]
	public class NodeFlag
	{
		#region private members
		private Node m_Node;
		private Color m_Color = Color.Red;
		private object m_Data;
		private NodeFlagStyle m_FlagStyle = NodeFlagStyle.Flag;
		#endregion

		#region construction
		public NodeFlag()
		{			
		}

		public NodeFlag(Color color)
		{
			m_Color = color;
		}

		public NodeFlag(Color color, NodeFlagStyle flagStyle)
		{
			m_Color = color;
			m_FlagStyle = flagStyle;
		}

		public NodeFlag(Color color, object data)
		{
			m_Color = color;
			m_Data = data;
		}

		public NodeFlag(Color color, NodeFlagStyle flagStyle, object data)
		{
			m_Color = color;
			m_Data = data;
			m_FlagStyle = flagStyle;
		}

		#endregion

		#region implementation
		internal void SetNode(Node node)
		{
			m_Node = node;
		}
		#endregion

		#region properties
		[Description("The Node's flag color.")]
		[Editor(typeof (ColorUIEditor), typeof (UITypeEditor))]
		public Color Color
		{
			get
			{
				return m_Color;
			}

			set
			{
				m_Color = value;

				if (m_Node != null)
					m_Node.Invalidate();
			}
		}

		[TypeConverter(typeof(System.ComponentModel.StringConverter))]
		[Description("The value associated with the Node's Flag.")]
		public object Data
		{
			get
			{
				return m_Data;
			}

			set
			{
				m_Data = value;

				if (m_Node != null)
					m_Node.Invalidate();
			}
		}

		[Description("The style of the Node's Flag.")]
		public NodeFlagStyle FlagStyle
		{
			get
			{
				return m_FlagStyle;
			}

			set
			{
				m_FlagStyle = value;

				if (m_Node != null)
					m_Node.Invalidate();
			}
		}
		#endregion
	}

	/// <summary>
	/// Implements the conversion functions for the NodeFlagConverter
	/// </summary>
	internal sealed class NodeFlagConverter : System.ComponentModel.ExpandableObjectConverter
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
				ConstructorInfo ctor = typeof(NodeFlag).GetConstructor(new Type[] {});

				if (ctor != null) 
				{
					return new InstanceDescriptor(ctor, new object[] {}, false);
				}
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}
			
	}
}
