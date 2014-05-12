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
using System.Reflection;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;

namespace PureComponents.TreeView
{
	/// <summary>
	/// ContextMenuStrings class holding strings for context menu extensions
	/// </summary>
	[TypeConverter(typeof(ContextMenuStringsConverter)), DesignTimeVisible(false)]
	[Serializable]
	public class ContextMenuStrings
	{
		#region private members

		private string m_Collapse = "Collapse";
		private string m_Expand = "Expand";
		private string m_MoveTop = "Move Top";
		private string m_MoveBottom = "Move Bottom";
		private string m_MoveLeft = "Move Left";
		private string m_MoveRight = "Move Right";
		private string m_MoveUp = "Move Up";
		private string m_MoveDown = "Move Down";
		private string m_AddNode = "Add Node";
		private string m_EditNode = "Edit Node";
		private string m_DeleteNode = "Delete Node";
		private string m_Copy = "Copy";
		private string m_Paste = "Paste";
		private string m_SaveXml = "Save Xml...";
		private string m_LoadXml = "Load Xml...";

		#endregion

		#region construction

		public ContextMenuStrings()
		{		
		}

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
        /// Gets or sets the collapse text.
        /// </summary>
        [Description("Gets or sets the collapse text.")]
        [DefaultValue("Collapse")]
		public string Collapse
		{
			get
			{
				return m_Collapse;
			}

			set
			{
				m_Collapse = value;
			}
		}

        /// <summary>
        /// Gets or sets the expand text.
        /// </summary>
        [Description("Gets or sets the expand text.")]
        [DefaultValue("Expand")]
		public string Expand
		{
			get
			{
				return m_Expand;
			}

			set
			{
				m_Expand = value;
			}
		}

        /// <summary>
        /// Gets or sets the move to top text.
        /// </summary>
        [Description("Gets or sets the move to top text.")]
        [DefaultValue("Move Top")]
		public string MoveTop
		{
			get
			{
				return m_MoveTop;
			}

			set
			{
				m_MoveTop = value;
			}
		}

        /// <summary>
        /// Gets or sets the move bottom text.
        /// </summary>
        [Description("Gets or sets the move bottom text.")]
        [DefaultValue("Move Bottom")]
		public string MoveBottom
		{
			get
			{
				return m_MoveBottom;
			}

			set
			{
				m_MoveBottom = value;
			}
		}

        /// <summary>
        /// Gets or sets the move left text.
        /// </summary>
        [Description("Gets or sets the move left text.")]
        [DefaultValue("Move Left")]
		public string MoveLeft
		{
			get
			{
				return m_MoveLeft;
			}

			set
			{
				m_MoveLeft = value;
			}
		}

        /// <summary>
        /// Gets or sets the move right text.
        /// </summary>
        [Description("Gets or sets the move right text.")]
        [DefaultValue("Move Right")]
		public string MoveRight
		{
			get
			{
				return m_MoveRight;
			}

			set
			{
				m_MoveRight = value;
			}
		}

        /// <summary>
        /// Gets or sets the move up text.
        /// </summary>
        [Description("Gets or sets the move up text.")]
        [DefaultValue("Move Up")]
		public string MoveUp
		{
			get
			{
				return m_MoveUp;
			}

			set
			{
				m_MoveUp = value;
			}
		}

        /// <summary>
        /// Gets or sets the move down text.
        /// </summary>
        [Description("Gets or sets the move down text.")]
        [DefaultValue("Move Down")]
		public string MoveDown
		{
			get
			{
				return m_MoveDown;
			}

			set
			{
				m_MoveDown = value;
			}
		}

        /// <summary>
        /// Gets or sets the add node text.
        /// </summary>
        [Description("Gets or sets the add node text.")]
        [DefaultValue("Add Node")]
		public string AddNode
		{
			get
			{
				return m_AddNode;
			}

			set
			{
				m_AddNode = value;
			}
		}

        /// <summary>
        /// Gets or sets the edit node text.
        /// </summary>
        [Description("Gets or sets the edit node text.")]
        [DefaultValue("Edit Node")]
		public string EditNode
		{
			get
			{
				return m_EditNode;
			}

			set
			{
				m_EditNode = value;
			}
		}

        /// <summary>
        /// Gets or sets the delete node text.
        /// </summary>
        [Description("Gets or sets the delete node text.")]
        [DefaultValue("Delete Node")]
		public string DeleteNode
		{
			get
			{
				return m_DeleteNode;
			}

			set
			{
				m_DeleteNode = value;
			}
		}

        /// <summary>
        /// Gets or sets the copy text.
        /// </summary>
        [Description("Gets or sets the copy text.")]
        [DefaultValue("Copy")]
		public string Copy
		{
			get
			{
				return m_Copy;
			}

			set
			{
				m_Copy = value;
			}
		}

        /// <summary>
        /// Gets or sets the paste text.
        /// </summary>
        [Description("Gets or sets the paste text.")]
        [DefaultValue("Paste")]
		public string Paste
		{
			get
			{
				return m_Paste;
			}

			set
			{
				m_Paste = value;
			} 
		}

        /// <summary>
        /// Gets or sets the save xml text.
        /// </summary>
        [Description("Gets or sets the save xml text.")]
        [DefaultValue("Save Xml...")]
		public string SaveXml
		{
			get
			{
				return m_SaveXml;
			}

			set
			{
				m_SaveXml = value;
			}
		}

        /// <summary>
        /// Gets or sets the load xml text.
        /// </summary>
        [Description("Gets or sets the load xml text.")]
        [DefaultValue("Load Xml...")]
		public string LoadXml
		{
			get
			{
				return m_LoadXml;
			}

			set
			{
				m_LoadXml = value;
			}
		}

		#endregion
	}

	/// <summary>
	/// Implements the conversion functions for the CheckBoxStyle
	/// </summary>
	internal sealed class ContextMenuStringsConverter : System.ComponentModel.ExpandableObjectConverter
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
				ConstructorInfo ctor = typeof(ContextMenuStrings).GetConstructor(new Type[] {});

				if (ctor != null) 
				{
					return new InstanceDescriptor(ctor, new object[] {}, false);
				}
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}
			
	}
}
