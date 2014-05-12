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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Data;
using System.Windows.Forms;
using System.Globalization;

namespace PureComponents.TreeView.Design
{
	/// <summary>
	/// The internal ImageIndex converter class
	/// </summary>
	internal class ImageIndexConverter : System.Windows.Forms.ImageIndexConverter
	{
		/// <summary>
		/// The convert from the string representation
		/// </summary>
		/// <param name="context"></param>
		/// <param name="culture"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)	
			{
				if (((string) value) != "(none)" && ((string) value) != "")
				{
					try
					{
						return Int32.Parse(((string) value));
					}
					catch
					{
						return -1;
					}
				}
				else
					return -1;
			}
			else
				return null;
		}

		/// <summary>
		/// The convert to implementation, converts from the int to string representation
		/// </summary>
		/// <param name="context"></param>
		/// <param name="culture"></param>
		/// <param name="value"></param>
		/// <param name="destinationType"></param>
		/// <returns></returns>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value is Int32)	
			{
				if (((int) value) >= 0)
					return ((int) value).ToString();
				else
					return "(none)";
			}
			else
				return null;
		}

		/// <summary>
		/// Function that iterates the set of the object, tries to find the ImageList property.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			ArrayList aImages = new ArrayList();
			ImageList oImageList = null;

			if (context.Instance != null) 
			{				
				PropertyDescriptorCollection PropertyCollection = TypeDescriptor.GetProperties(context.Instance);
				PropertyDescriptor Property;

				if ((Property = PropertyCollection.Find("ImageList", false)) != null) 
					oImageList = (ImageList)Property.GetValue(context.Instance);
								
				if (oImageList != null)
				{
					for (int nIndex = 0; nIndex < oImageList.Images.Count; nIndex ++)
						aImages.Add(nIndex);

					aImages.Add(-1);
				}
			}

			return new StandardValuesCollection(aImages);			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			if (context.Instance != null)
				return true;
			else
				return false;
		}
	}

	/// <summary>
	/// The ImageIndex editor
	/// </summary>
	internal class ImageIndexEditor : UITypeEditor
	{
		/// <summary>
		/// We will draw the item by ourselves, so we will return true here
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override bool GetPaintValueSupported(ITypeDescriptorContext context) 
		{
			return true;
		}

		/// <summary>
		/// The painting function, gets the imagelist and the image object, the image index and draws the inner part of the item
		/// in the property window
		/// </summary>
		/// <param name="pe"></param>
		public override void PaintValue(PaintValueEventArgs pe) 
		{			
			Image oImage = null;
			int nImageIndex = (int)pe.Value;
			
			ImageList oImageList = null;

			if (pe.Context.Instance != null && nImageIndex >= 0) 
			{				
				PropertyDescriptorCollection PropertyCollection = TypeDescriptor.GetProperties(pe.Context.Instance);
				PropertyDescriptor Property;

				if ((Property = PropertyCollection.Find("ImageList", false)) != null) 
					oImageList = (ImageList) Property.GetValue(pe.Context.Instance);

				if (oImageList != null && oImageList.Images.Count > nImageIndex)
					oImage = oImageList.Images[nImageIndex];
			}

			// if the image has ben found, draw it inside the item area
			if (nImageIndex >= 0 && oImage != null) 
				pe.Graphics.DrawImage(oImage, pe.Bounds);
		}
	}
}
