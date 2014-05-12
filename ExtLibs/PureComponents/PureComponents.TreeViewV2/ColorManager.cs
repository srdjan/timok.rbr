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
using System.Drawing;

namespace PureComponents.TreeView
{
	[Serializable]
	internal class ColorManager 
	{ 
		#region internal class

		[Serializable]
		internal class HLS 
		{ 
			public HLS() 
			{ 
				m_H=0; 
				m_S=0; 
				m_L=0; 
			} 

			private double m_H; 
			private double m_S; 
			private double m_L; 

			public double H 
			{ 
				get
				{
					return m_H;
				} 

				set 
				{ 
					m_H = value; 

					m_H = m_H > 1 ? 1 : m_H < 0 ? 0 : m_H; 
				} 
			} 

			public double S 
			{ 
				get
				{
					return m_S;
				} 

				set 
				{ 
					m_S = value; 

					m_S = m_S > 1 ? 1 : m_S < 0 ? 0 : m_S; 
				} 
			} 

			public double L 
			{ 
				get
				{
					return m_L;
				} 

				set 
				{ 
					m_L = value; 

					m_L = m_L > 1 ? 1 : m_L < 0 ? 0 : m_L;
				} 
			} 
		}
 
		#endregion

		#region construction

		public ColorManager() 
		{ 

		} 

		#endregion

		/// <summary> 
		/// Sets the absolute brightness of a colour 
		/// </summary> 
		public static Color SetBrightness(Color c, double brightness) 
		{ 
			HLS hls = RGB_to_HLS(c); 

			hls.L = brightness; 

			return HLS_to_RGB(hls); 
		} 
       

		/// <summary> 
		/// Modifies an existing brightness level 
		/// </summary> 
		/// <remarks> 
		/// To reduce brightness use a number smaller than 1. To increase brightness use a number larger tnan 1 
		/// </remarks> 
		public static Color ModifyBrightness(Color c, double brightness) 
		{ 
			HLS hls = RGB_to_HLS(c); 

			hls.L *= brightness; 

			return HLS_to_RGB(hls); 
		} 

		/// <summary> 
		/// Sets the absolute saturation level 
		/// </summary> 
		/// <remarks>Accepted values 0-1</remarks> 
		public static Color SetSaturation(Color c, double Saturation) 
		{ 
			HLS hls = RGB_to_HLS(c); 

			hls.S = Saturation; 

			return HLS_to_RGB(hls); 
		} 

        /// <summary> 
		/// Modifies an existing Saturation level 
		/// </summary> 
		/// <remarks> 
		/// To reduce Saturation use a number smaller than 1. To increase Saturation use a number larger tnan 1 
		/// </remarks> 
		public static Color ModifySaturation(Color c, double Saturation) 
		{ 
			HLS hls = RGB_to_HLS(c); 

			hls.S *= Saturation; 

			return HLS_to_RGB(hls); 
		} 

  		/// <summary> 
		/// Sets the absolute Hue level 
		/// </summary> 
		/// <remarks>Accepted values 0-1</remarks> 
		public static  Color SetHue(Color c, double Hue) 
		{ 
			HLS hls = RGB_to_HLS(c); 

			hls.H = Hue; 

			return HLS_to_RGB(hls); 
		} 

       	/// <summary> 
		/// Modifies an existing Hue level 
		/// </summary> 
		/// <remarks> 
		/// To reduce Hue use a number smaller than 1. To increase Hue use a number larger tnan 1 
		/// </remarks> 
		public static Color ModifyHue(Color c, double Hue) 
		{ 
			HLS hls = RGB_to_HLS(c); 

			hls.H *= Hue; 

			return HLS_to_RGB(hls); 
		} 

		/// <summary> 
		/// Converts a colour from HLS to RGB 
		/// </summary> 
		/// <remarks>Adapted from the algoritm in Foley and Van-Dam</remarks> 
		public static Color HLS_to_RGB(HLS hls) 
		{ 
			double r = 0, g = 0, b = 0; 
			double temp1,temp2; 

			if (hls.L == 0) 
			{ 
				r = g = b = 0; 
			} 
			else 
			{ 
				if (hls.S==0) 
				{ 
					r = g = b = hls.L; 
				} 
				else 
				{ 
					temp2 = ((hls.L <= 0.5) ? hls.L * (1.0 + hls.S) : hls.L + hls.S - (hls.L * hls.S)); 
					temp1 = 2.0 * hls.L - temp2; 

					double[] t3 = new double[]{hls.H + 1.0 / 3.0, hls.H, hls.H - 1.0 / 3.0}; 
					double[] clr=new double[]{0, 0, 0}; 

					for (int i = 0; i < 3; i++) 
					{ 
						if (t3[i] < 0) 
							t3[i] += 1.0; 

						if (t3[i] > 1) 
							t3[i] -= 1.0; 

						if (6.0*t3[i] < 1.0) 
							clr[i] = temp1 + (temp2 - temp1) * t3[i] * 6.0; 
						else if (2.0 * t3[i] < 1.0) 
							clr[i] = temp2; 
						else if(3.0 * t3[i] < 2.0) 
							clr[i] = (temp1 + (temp2 - temp1) * ((2.0 / 3.0) - t3[i]) * 6.0); 
						else 
							clr[i] = temp1; 
					} 

					r = clr[0]; 
					g = clr[1]; 
					b = clr[2]; 
				} 
			} 

			return Color.FromArgb((int)(255 * r), (int)(255 * g), (int)(255 * b)); 
		} 

		/// <summary> 
		/// Converts RGB to HLS 
		/// </summary> 
		public static HLS RGB_to_HLS (Color c) 
		{ 
			HLS hls = new HLS(); 

			hls.H = c.GetHue() / 360.0;
			hls.L = c.GetBrightness(); 
			hls.S = c.GetSaturation(); 

			return hls; 
		} 
	}  
}
