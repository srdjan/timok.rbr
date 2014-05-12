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
using System.Windows.Forms.Design;

namespace PureComponents.TreeView.Design
{	
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	internal class ColorUIEditor : UITypeEditor
	{
		public ColorUIEditor()
		{			
		}

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) 
		{
			if (context != null) 
				return UITypeEditorEditStyle.DropDown;

			return base.GetEditStyle(context);
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) 
		{
			if ((context != null) && (provider != null)) 
			{
				// Access the Property Browser's UI display service
				IWindowsFormsEditorService editorService =
					(IWindowsFormsEditorService)
					provider.GetService(typeof(IWindowsFormsEditorService));
   
				if (editorService != null) 
				{
					// Create an instance of the UI editor control
					//ColorUIEditorCtrl dropDownEditor =
					//	new ColorUIEditorCtrl(editorService);
					ColorUIEditorCtrl dropDownEditor =
						new ColorUIEditorCtrl((Color)value, editorService);
   
					//// Pass the UI editor control the current property value
					dropDownEditor.Value = (Color)value;
   
					// Display the UI editor control
					editorService.DropDownControl(dropDownEditor);
   
					value = dropDownEditor.Value;

					// Return the new property value from the UI editor control
					return value;
				}
			}

			return base.EditValue(context, provider, value);
		}

		public override bool GetPaintValueSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override void PaintValue(PaintValueEventArgs e)
		{
			Color color = (Color)e.Value;

			Brush brush = new SolidBrush(color);

			e.Graphics.FillRectangle(brush, e.Bounds);

			brush.Dispose();
		}
	}
}
