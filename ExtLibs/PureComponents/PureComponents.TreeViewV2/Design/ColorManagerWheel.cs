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

namespace PureComponents.TreeView.Design
{
	/// <summary>
	/// 
	/// </summary>
	internal class ColorManagerWheel 
	{
		/// <summary>
		/// 
		/// </summary>
		internal struct RGB
		{
			public int Red;
			public int Green;
			public int Blue;

			public RGB(int R, int G, int B) 
			{
				Red = R;
				Green = G;
				Blue = B;
			}

			public override string  ToString() 
			{
				return String.Format("({0}, {1}, {2})", Red, Green, Blue);
			}
		} 

		/// <summary>
		/// 
		/// </summary>
		internal struct HSV
		{
			// All values are between 0 and 255.
			public int Hue;
			public int Saturation;
			public int value;

			public HSV(int H, int S, int V) 
			{
				Hue = H;
				Saturation = S;
				value = V;
			}

			public override string  ToString() 
			{
				return String.Format("({0}, {1}, {2})", Hue, Saturation, value);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="H"></param>
		/// <param name="S"></param>
		/// <param name="V"></param>
		/// <returns></returns>
		public static RGB HSVtoRGB( int H, int S, int V) 
		{
			// H, S, and V must all be between 0 and 255.
			return HSVtoRGB(new HSV(H, S, V));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="hsv"></param>
		/// <returns></returns>
		public static Color HSVtoColor(HSV hsv) 
		{
			RGB RGB = HSVtoRGB(hsv);
			return Color.FromArgb(RGB.Red, RGB.Green, RGB.Blue);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="H"></param>
		/// <param name="S"></param>
		/// <param name="V"></param>
		/// <returns></returns>
		public static Color HSVtoColor( int H,  int S,  int V) 
		{
			return HSVtoColor(new HSV(H, S, V));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="HSV"></param>
		/// <returns></returns>
		public static RGB HSVtoRGB(HSV HSV) 
		{
			double h;
			double s;
			double v;

			double r = 0;
			double g = 0;
			double b = 0;

			// Scale Hue to be between 0 and 360. Saturation
			// and value scale to be between 0 and 1.
			h = ((double) HSV.Hue / 255 * 360) % 360;
			s = (double) HSV.Saturation / 255;
			v = (double) HSV.value / 255;

			if ( s == 0 ) 
			{
				// If s is 0, all colors are the same.
				// This is some flavor of gray.
				r = v;
				g = v;
				b = v;
			} 
			else 
			{
				double p;
				double q;
				double t;

				double fractionalSector;
				int sectorNumber;
				double sectorPos;

				// The color wheel consists of 6 sectors.
				// Figure out which sector you//re in.
				sectorPos = h / 60;
				sectorNumber = (int)(Math.Floor(sectorPos));

				// get the fractional part of the sector.
				// That is, how many degrees into the sector
				// are you?
				fractionalSector = sectorPos - sectorNumber;

				// Calculate values for the three axes
				// of the color. 
				p = v * (1 - s);
				q = v * (1 - (s * fractionalSector));
				t = v * (1 - (s * (1 - fractionalSector)));

				// Assign the fractional colors to r, g, and b
				// based on the sector the angle is in.
				switch (sectorNumber) 
				{
					case 0:
						r = v;
						g = t;
						b = p;
						break;

					case 1:
						r = q;
						g = v;
						b = p;
						break;

					case 2:
						r = p;
						g = v;
						b = t;
						break;

					case 3:
						r = p;
						g = q;
						b = v;
						break;

					case 4:
						r = t;
						g = p;
						b = v;
						break;

					case 5:
						r = v;
						g = p;
						b = q;
						break;
				}
			}

			// return an RGB structure, with values scaled
			// to be between 0 and 255.
			r = (int)(r * 255);
			g = (int)(g * 255);
			b = (int)(b * 255);

			if (r > 255)
				r = 255;

			if (g > 255)
				g = 255;

			if (b > 255)
				b = 255;

			if (r < 0)
				r = 0;

			if (g < 0)
				g = 0;

			if (b < 0)
				b = 0;

			return new RGB((int)r, (int)g, (int)b);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="RGB"></param>
		/// <returns></returns>
		public static HSV RGBtoHSV( RGB RGB) 
		{
			double min;
			double max;
			double delta;

			double r = (double) RGB.Red / 255;
			double g = (double) RGB.Green / 255;
			double b = (double) RGB.Blue / 255;

			double h;
			double s;
			double v;

			min = Math.Min(Math.Min(r, g), b);
			max = Math.Max(Math.Max(r, g), b);
			v = max;
			delta = max - min;
			if ( max == 0 || delta == 0 ) 
			{
				// R, G, and B must be 0, or all the same.
				// In this case, S is 0, and H is undefined.
				// Using H = 0 is as good as any...
				s = 0;
				h = 0;
			} 
			else 
			{
				s = delta / max;
				if ( r == max ) 
				{
					// Between Yellow and Magenta
					h = (g - b) / delta;
				} 
				else if ( g == max ) 
				{
					// Between Cyan and Yellow
					h = 2 + (b - r) / delta;
				} 
				else 
				{
					// Between Magenta and Cyan
					h = 4 + (r - g) / delta;
				}

			}

			h *= 60;
			if ( h < 0 ) 
			{
				h += 360;
			}

			// Scale to the requirements of this 
			// application. All values are between 0 and 255.
			return new HSV((int)(h / 360 * 255), (int)(s * 255), (int)(v * 255));
		}
	}
}

