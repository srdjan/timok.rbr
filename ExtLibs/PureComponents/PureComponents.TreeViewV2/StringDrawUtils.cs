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
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Text;
using PureComponents.TreeView.Design;

namespace PureComponents.TreeView 
{

	#region ParagraphAlignment

	/// <summary>
	/// Paragraph alignments
	/// </summary>
	internal enum ParagraphAlignment
	{
		/// <summary>
		/// Text is aligned to left
		/// </summary>
		Left = 0, 
		/// <summary>
		/// Text is centered
		/// </summary>
		Center = 1, 
		/// <summary>
		/// Text is aligned to right
		/// </summary>
		Right = 2, 
		/// <summary>
		/// Text is fully alignment
		/// </summary>
		/// <remarks>
		/// Full alignment means that wrapped lines fill the whole 
		/// line (if possible). The other lines are left aligned.
		/// </remarks>
		Full = 3
	}

	#endregion

	#region ParagraphVerticalAlignment

	/// <summary>
	/// Vertical alignments
	/// </summary>
	internal enum ParagraphVerticalAlignment
	{
		/// <summary>
		/// Paragraph is aligned to the top of its bounding rectangle.
		/// </summary>
		Top = 0, 
		/// <summary>
		/// Paragraph is aligned to center of its bounding rectangle.
		/// </summary>
		Center = 1, 
		/// <summary>
		/// Paragraph is aligned to the bottom of its bounding rectangle.
		/// </summary>
		Bottom = 2
	}

	#endregion

	#region CharacterFormat

	/// <summary>
	/// Specifies character formatting
	/// </summary>
	internal class CharacterFormat
	{
		/// <summary>
		/// Font to be used for drawing strings.
		/// </summary>
		public Font Font;
		/// <summary>
		/// Brush to be used for drawing strings.
		/// </summary>
		public Brush Brush;
		/// <summary>
		/// Angle of string's rotation. (Clockwise)
		/// </summary>
		public float Angle;
		/// <summary>
		/// HotkeyPrefix of the string.
		/// </summary>
		public HotkeyPrefix HotkeyPrefix;
		/// <summary>
		/// If true, some special sequences can be used in the string to 
		/// change it's appearance.
		/// </summary>
		/// <remarks>
		/// The formatting sequences are:<br/>
		/// <table>
		/// <tr>
		/// <td>#CRRGGBB</td>
		/// <td>Changes color of the text. RR, GG and BB are hexadecimal values of 
		/// appropriate color channels.</td>
		/// </tr>
		/// <tr>
		/// <td>#C-</td>
		/// <td>Changes color of the text to the one before last use of #CRRGGBB command.<br/>
		/// Can be used multiple times: #CFF0000 Red #C00FF00 Green #C0000FF Blue #C- Green #C- Red</td>
		/// </tr>
		/// <tr>
		/// <td>#B+ and #B-</td>
		/// <td>Sets and unsets the bold font style.</td>
		/// </tr>
		/// <tr>
		/// <td>#I+ and #I-</td>
		/// <td>Sets and unsets the italics font style.</td>
		/// </tr>
		/// <tr>
		/// <td>#U+ and #U-</td>
		/// <td>Sets and unsets the underline font style.</td>
		/// </tr>
		/// <tr>
		/// <td>##</td>
		/// <td>Writes the # character (as it is not possible to write it by '#' character).</td>
		/// </tr>
		/// </table>
		///  
		/// </remarks>
		public bool Formatted;

		/// <summary>
		/// Instead of characters, only bounding rectangles are drawn.
		/// </summary>
		/// <remarks>
		/// This is useful when resulting image is to be masked and at the same time
		/// text is drawn by several brushes.
		/// </remarks>
		public bool FilledBounds;

		/// <summary>
		/// Color formattig marks will be ignored. (Applicable only when Formatted is true.)
		/// </summary>
		/// <remarks>
		/// This is useful when resulting image is to be used as mask and we want
		/// all the text to be drawn by same brush.
		/// </remarks>
		public bool IgnoreColorFormatting;


		#region Constructors
		/// <summary>
		/// Creates <see cref="CharacterFormat"/> object with implicit formatting.
		/// </summary>
		public CharacterFormat()
		{
			Font=new Font(FontFamily.GenericSansSerif,8.25f);
			Brush=new SolidBrush(Color.Black);
			Angle=0;
			HotkeyPrefix=HotkeyPrefix.Show;
			Formatted=false;
			FilledBounds=false;
			IgnoreColorFormatting=false;
		}
		/// <summary>
		/// Creates <see cref="CharacterFormat"/> object with specified 
		/// <see cref="Font"/> and <see cref="Color"/>.
		/// </summary>
		public CharacterFormat(Font f, Color c) : this()
		{
			Font = f;
			Brush = new SolidBrush(c);
		}

		/// <summary>
		/// Creates <see cref="CharacterFormat"/> object with specified 
		/// <see cref="Font"/> and <see cref="Brush"/>.
		/// </summary>
		public CharacterFormat(Font f, Brush br) : this()
		{
			Font = f;
			Brush = br;
		}

		/// <summary>
		/// Creates <see cref="CharacterFormat"/> object with specified 
		/// <see cref="Font"/> and <see cref="Brush"/> rotated by given angle.
		/// </summary>
		public CharacterFormat(Font f, Brush br, float a) : this()
		{
			Font = f;
			Brush = br;
			Angle = a;
		}

		/// <summary>
		/// Creates <see cref="CharacterFormat"/> object rotated by given angle.
		/// </summary>
		public CharacterFormat(float a) : this()
		{
			Angle = a;
		}

		/// <summary>
		/// Creates <see cref="CharacterFormat"/> object. If <paramref name="formatted"/> is 
		/// true, strings can containg formatting tags.
		/// </summary>
		/// <param name="formatted"></param>
		/// <remarks>
		/// See <see cref="Formatted"/> property for specification.
		/// </remarks>
		public CharacterFormat(bool formatted) : this()
		{
			Formatted = formatted;
		}

		/// <summary>
		/// Creates <see cref="CharacterFormat"/> object.
		/// </summary>
		/// <param name="f"></param>
		/// <param name="br"></param>
		/// <param name="a"></param>
		/// <param name="hp"></param>
		/// <param name="formatted"></param>
		public CharacterFormat(Font f, Brush br, float a, HotkeyPrefix hp, bool formatted)
		{
			Font = f;
			Brush = br;
			Angle = a;
			HotkeyPrefix = hp;
			Formatted = formatted;
			FilledBounds = false;
			IgnoreColorFormatting = false;
		}

		#endregion

		/// <summary>
		/// Returns a shallow copy of object.
		/// </summary>
		/// <returns></returns>
		/// <remarks>
		/// Shallow copy means that member objects are not being copied, just
		/// references to them.
		/// </remarks>
		public CharacterFormat ShallowCopy()
		{
			return (CharacterFormat) MemberwiseClone();
		}

		/// <summary>
		/// Determines whether the specified Object is equal to the current Object.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			if (!(obj is CharacterFormat))
				return false;
			CharacterFormat c = (CharacterFormat) obj;
			return Font.Equals(c.Font) &&
				(Brush.Equals(c.Brush) ||
					((Brush is SolidBrush && c.Brush is SolidBrush) &&
						(((SolidBrush) Brush).Color == ((SolidBrush) c.Brush).Color))) &&
				Angle == c.Angle &&
				HotkeyPrefix == c.HotkeyPrefix &&
				Formatted == c.Formatted &&
				FilledBounds == c.FilledBounds &&
				IgnoreColorFormatting == c.IgnoreColorFormatting;
		}

		/// <summary>
		/// Serves as a hash function for a particular type, suitable 
		/// for use in hashing algorithms and data tructures like a hash table.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return Font.GetHashCode() ^ Brush.GetHashCode() ^ Angle.GetHashCode() ^
				HotkeyPrefix.GetHashCode() ^ Formatted.GetHashCode()*37 ^
				FilledBounds.GetHashCode()*113 ^ IgnoreColorFormatting.GetHashCode()*217;
		}

		/// <summary>
		/// Color of actual brush. If brush is not solid, <see cref="System.Drawing.Color.Empty"/>
		/// is returned.
		/// </summary>
		public Color Color
		{
			get
			{
				if (Brush is SolidBrush)
					return ((SolidBrush) Brush).Color;
				else
					return Color.Empty;
			}
		}
	}

	#endregion

	#region ParagraphFormat

	/// <summary>
	/// Specifies paragraph formatting
	/// </summary>
	internal class ParagraphFormat
	{
		/// <summary>
		/// Alignment of the paragraph.
		/// </summary>
		public ParagraphAlignment Alignment;

		/// <summary>
		/// Vertical alignment of the paragraph.
		/// </summary>
		public ParagraphVerticalAlignment VerticalAlignment;

		/// <summary>
		/// Specifies wherther given string is multiline. Also if true, word 
		/// wrapping is performed.
		/// </summary>
		public bool MultiLine;

		/// <summary>
		/// If true, partially clipped lines are also shown.
		/// </summary>
		public bool ShowIncompleteLines;

		/// <summary>
		/// Specifies the way of text trimming...
		/// </summary>
		public StringTrimming Trimming;

		/// <summary>
		/// Specifies brush used to draw text background. If null, text background
		/// is transparent.
		/// </summary>
		public Brush BackgroundBrush;

		#region Constructors

		/// <summary>
		/// Creates <see cref="ParagraphFormat"/> object with implicit formatting.
		/// </summary>
		public ParagraphFormat()
		{
			Alignment = ParagraphAlignment.Left;
			VerticalAlignment = ParagraphVerticalAlignment.Top;
			MultiLine = true;
			ShowIncompleteLines = true;
			Trimming = StringTrimming.EllipsisCharacter;
			BackgroundBrush = null;
		}

		/// <summary>
		/// Creates <see cref="ParagraphFormat"/> object.
		/// </summary>
		public ParagraphFormat(ParagraphAlignment pa, ParagraphVerticalAlignment pva,
		                       bool ml, bool sil, StringTrimming tr, Brush bg)
		{
			Alignment = pa;
			VerticalAlignment = pva;
			MultiLine = ml;
			ShowIncompleteLines = sil;
			Trimming = tr;
			BackgroundBrush = bg;
		}

		#endregion

		/// <summary>
		/// Returns a shallow copy of object.
		/// </summary>
		/// <returns></returns>
		/// <remarks>
		/// Shallow copy means that member objects are not being copied, just
		/// references to them.
		/// </remarks>
		public ParagraphFormat ShallowCopy()
		{
			return (ParagraphFormat) MemberwiseClone();
		}
	}

	#endregion

	/// <summary>
	/// Methods for advanced string drawing.
	/// </summary>
	internal class StringDrawUtils
	{
		#region Singletonism implementation

		static StringDrawUtils instance;

		/// <summary>
		/// Returns instance of StringDrawUtils object.
		/// </summary>
		/// <returns></returns>
		/// <remarks>As StringDrawUtils is a singleton, always the same instance is returned.</remarks>
		public static StringDrawUtils GetInstance()
		{
			if (instance == null)
				instance = new StringDrawUtils();
			return instance;
		}

		private StringDrawUtils()
		{
			dummyImg = new Bitmap(1, 1);
			dummyGraphics = Graphics.FromImage(dummyImg);
		}

		#endregion

		#region private members

		private Image dummyImg;

		/// <summary>
		/// We sometimes need a <see cref="Graphics"/> object when we don't have
		/// one. So here we have one.
		/// </summary>
		private Graphics dummyGraphics;

		#endregion

		#region implementation

		#region Measurement

		/// <summary>
		/// Returns line height of given font.
		/// </summary>
		/// <param name="gr">Graphics object used to measure line height.</param>
		/// <param name="f">Font</param>
		/// <returns>Line height of given font.</returns>
		/// <remarks>
		/// Returned value is not exactly what gr.MeasureString gives. It's
		/// height of a line without the vertival gap that gr.MeasureString adds.
		/// </remarks>
		public float GetLineHeight(Graphics gr, Font f)
		{
			return f.GetHeight(gr);
		}

		/// <summary>
		/// Returns height of the text when wrapping to given <paramref name="width"/> is performed.
		/// </summary>
		/// <param name="gr">Graphics object used for measurement.</param>
		/// <param name="str">String to be measured.</param>
		/// <param name="cf"><see cref="CharacterFormat"/> of given string.</param>
		/// <param name="width">Maximum width of resulting text.</param>
		/// <returns>Returns height of the text when wrapping to given <paramref name="width"/> 
		/// is performed.</returns>
		public float GetWrappedHeight(Graphics gr, string str, CharacterFormat cf, float width)
		{
			CharacterFormat cf2=cf.ShallowCopy();
			cf2.Angle=0;
			SDUFormattedString fmts = new SDUFormattedString(str, cf);
			fmts.WrapLines(gr, width);
			return fmts.GetTotalHeight(gr);

		}

		/// <summary>
		/// Returns (more or less) exact size of given string.
		/// </summary>
		/// <param name="gr">Graphics where the size should be measured.</param>
		/// <param name="str">String to be measured.</param>
		/// <param name="cf">CharacterFormat of the string.</param>
		/// <param name="pf">ParagraphFormat of the string.</param>
		/// <param name="includeTrailingSpaces">Specifies whether trailing 
		/// spaces should be included into measurement.</param>
		/// <returns>Returns size of given text at specified conditions.</returns>
		/// <remarks>
		/// Returned value is not affected by any gaps and inaccurancies which appear
		/// while using Graphics.MeasureString as we use Graphics.MeasureCharacterRanges.
		/// </remarks>
		public SizeF MeasureStringExactly(Graphics gr, string str, CharacterFormat cf, ParagraphFormat pf,
		                                  bool includeTrailingSpaces)
		{
			StringFormat sf = GetStringFormat(cf, pf);

			if (includeTrailingSpaces)
				sf.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;


			sf.SetMeasurableCharacterRanges(new CharacterRange[] {new CharacterRange(0, str.Length)});
			sf.FormatFlags |= StringFormatFlags.NoWrap;

			SizeF bound = gr.MeasureString(str, (cf).Font, 0, sf);
			bound.Width += 10;
			bound.Height += 10;
			RectangleF rect = new RectangleF(new PointF(0, 0), bound);

			//RectangleF rect=new RectangleF(0,0,0,0);

			bool useOriginalGraphics = gr.DpiX != dummyGraphics.DpiX ||
				gr.DpiY != dummyGraphics.DpiY ||
				gr.PageScale != dummyGraphics.PageScale ||
				gr.PageUnit != dummyGraphics.PageUnit;

			Region[] rgn;

			if (useOriginalGraphics)
				rgn = gr.MeasureCharacterRanges(str, cf.Font, rect, sf);
			else
				rgn = dummyGraphics.MeasureCharacterRanges(str, cf.Font, rect, sf);

			sf.Dispose();

			SizeF size;

			if (useOriginalGraphics)
				size = rgn[0].GetBounds(gr).Size;
			else
				size = rgn[0].GetBounds(dummyGraphics).Size;

			rgn[0].Dispose();

			return size;
		}

		/// <summary>
		/// Returns size of vertical gap that's included in size returned by
		/// Graphics.MeasureString besides actual height of the given text.
		/// </summary>
		public float GetMeasureStringVerticalGap(Graphics gr, Font f)
		{
			float olh = gr.MeasureString(" ", f).Height; //one line's height
			float tlh = gr.MeasureString(" \n ", f).Height; //two lines' height
			float borders = 2*olh - tlh;
			return borders/2;
		}

		/// <summary>
		/// Returns size of vertical gap that's included in size returned by
		/// Graphics.MeasureString besides actual height of the given text.
		/// </summary>
		public float GetMeasureStringHorizontalGap(Graphics gr, Font f)
		{
			float ocw = gr.MeasureString(" ", f).Width; //one character's width
			float tcw = gr.MeasureString("  ", f).Width; //two characters' width
			float borders = 2*ocw - tcw;
			return borders/2;
		}

		#endregion

		#region Formatted strings

		/// <summary>
		/// Converts string with formatting marks into ordinary string.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		/// <remarks>
		/// The conversion consists of removing formatting tags and replacing 
		/// "##" sequences with single #.
		/// </remarks>
		public string GetTextFromFormattedString(string str)
		{
      //TODO: IGOR BEGIN - Disable the new "Rich Text Formatting", 
      //otherwise we cannot use "#" in the Note's Text !!! 
      return str;
      //IGOR END

			if (str == null)
				return null;

			StringBuilder res=new StringBuilder();

			for (int i = 0; i < str.Length; i++)
			{
				if(str[i]!='#') 
				{
					res.Append(str[i]);
				}
				else if(str[i+1]=='#') 
				{
					res.Append('#');
					i++;
				} 
				else 
				{ 
					switch(str[i+1])
					{
						case 'C':
							if(str.Length > i+2 && str[i+2]=='-')
								i+=2;
							else if(str.Length > i+7)
								i+=7;
							break;
						case 'B':
						case 'I':
						case 'U':
							i+=2;
							break;
						default:
							throw new ArgumentException("Invalid format string","str=" + str);
					}
				}
			}
			return res.ToString();
		}

		#endregion

		/// <summary>
		/// Draws string with given formating and width at specified point.
		/// </summary>
		/// <param name="gr"><see cref="Graphics"/> object to draw into.</param>
		/// <param name="str"><see cref="string"/> to be drawn.</param>
		/// <param name="width">Width that string will be wrapped to.</param>
		/// <param name="ofs">Top-left point of the drawn string.</param>
		/// <param name="fmtCharacter"><see cref="CharacterFormat"/> used to draw the <see cref="string"/>. </param>
		/// <param name="fmtParagraph"><see cref="ParagraphFormat"/> used to draw the <see cref="string"/>. </param>
		public void DrawWrappedString(Graphics gr, string str, PointF ofs, float width,
			CharacterFormat fmtCharacter, ParagraphFormat fmtParagraph)
		{
			float height=GetWrappedHeight(gr, str, fmtCharacter, width);
			ParagraphFormat pf2=fmtParagraph.ShallowCopy();
			pf2.MultiLine=true;
			pf2.ShowIncompleteLines=true;
			pf2.VerticalAlignment=ParagraphVerticalAlignment.Top;
			DrawStringInRectangle(gr, str, new RectangleF(ofs, 
				new SizeF(width, height)),fmtCharacter, fmtParagraph);
		}

		/// <summary>
		/// Draws string with given formating into given <paramref name="rectangle"/>.
		/// </summary>
		/// <param name="gr"><see cref="Graphics"/> object to draw into.</param>
		/// <param name="str"><see cref="string"/> to be drawn.</param>
		/// <param name="rectangle"><see cref="Rectangle"/> that string is drawn into.</param>
		/// <param name="fmtCharacter"><see cref="CharacterFormat"/> used to draw the <see cref="string"/>. </param>
		/// <param name="fmtParagraph"><see cref="ParagraphFormat"/> used to draw the <see cref="string"/>. </param>
		public void DrawStringInRectangle(Graphics gr, string str, RectangleF rectangle,
		                                  CharacterFormat fmtCharacter, ParagraphFormat fmtParagraph)
		{
			Matrix trOld = gr.Transform.Clone();
			gr.TranslateTransform(rectangle.X, rectangle.Y);
			gr.RotateTransform(fmtCharacter.Angle);
			gr.TranslateTransform(-rectangle.X, -rectangle.Y);

			if (fmtParagraph.BackgroundBrush != null)
				gr.FillRectangle(fmtParagraph.BackgroundBrush, rectangle);

			CharacterFormat cf = fmtCharacter.ShallowCopy();
			cf.Angle = 0;
			ParagraphFormat pf = fmtParagraph.ShallowCopy();
			pf.BackgroundBrush = null;

			if (fmtCharacter.Formatted)
			{
				SDUFormattedString fmts = new SDUFormattedString(str, cf);
				if (pf.MultiLine)
					fmts.WrapLines(gr, rectangle.Width);
				fmts.DrawStringInRectangle(gr, rectangle, pf);
			}
			else if (fmtCharacter.FilledBounds)
				gr.FillRectangle(fmtCharacter.Brush, rectangle);
			else
			{
				if (fmtParagraph.Alignment == ParagraphAlignment.Full && fmtParagraph.MultiLine && rectangle.Width > 0)
				{
					DrawFullAlignedStringInRectangle(gr, str, rectangle, cf, fmtParagraph);
				}
				else
				{
					StringFormat sf = GetStringFormat(fmtCharacter, fmtParagraph);

					gr.DrawString(str, fmtCharacter.Font, fmtCharacter.Brush, rectangle, sf);

				}
			}

			gr.Transform = trOld;

		}

		/// <summary>
		/// Draws string with given character format and alignment at given <see cref="Point"/>.
		/// </summary>
		/// <param name="gr"><see cref="Graphics"/> object to draw into.</param>
		/// <param name="str"><see cref="string"/> to be drawn.</param>
		/// <param name="pnt"><see cref="Point"/> where the string will be drawn.</param>
		/// <param name="fmt"><see cref="CharacterFormat"/> used to draw the <see cref="string"/>. </param>
		/// <param name="align">Specifies whether the <paramref name="pnt"/> is at left, right 
		/// or in the center of the string. (<see cref="ParagraphAlignment.Full"/> means the same
		/// as <see cref="ParagraphAlignment.Left"/>.)</param>
		public void DrawString(Graphics gr, string str, PointF pnt,
		                       CharacterFormat fmt, ParagraphAlignment align)
		{
			StringFormat sf = (StringFormat) StringFormat.GenericDefault.Clone();
			sf.Alignment = ParAl2StrAl(align);
			sf.HotkeyPrefix = fmt.HotkeyPrefix;

			Matrix trOld = null;
			if (fmt.Angle != 0)
			{
				trOld = gr.Transform.Clone();
				gr.TranslateTransform(pnt.X, pnt.Y);
				gr.RotateTransform(fmt.Angle);
				gr.TranslateTransform(-pnt.X, -pnt.Y);
			}

			if (fmt.Formatted)
			{
				CharacterFormat cf = fmt.ShallowCopy();
				cf.Angle = 0;
				SDUFormattedString fs = new SDUFormattedString(str, cf);
				SizeF sz = fs.Measure(gr, true);
				sz.Height += GetMeasureStringVerticalGap(gr, cf.Font);

				PointF ofs = pnt;
				switch (align)
				{
					case ParagraphAlignment.Right:
						ofs.X -= sz.Width;
						break;
					case ParagraphAlignment.Center:
						ofs.X -= sz.Width/2;
						break;
				}

				ParagraphFormat pf = new ParagraphFormat();
				pf.MultiLine = false;
				pf.Alignment = align;
				fs.DrawStringInRectangle(gr, new RectangleF(ofs, sz), pf);


			}
			else if (fmt.FilledBounds)
			{
				ParagraphFormat pf = new ParagraphFormat();
				//pf.Alignment=align;
				SizeF sz = MeasureStringExactly(gr, str, fmt, pf, false);

				sz.Width += GetMeasureStringHorizontalGap(gr, fmt.Font);
				pnt.X += GetMeasureStringHorizontalGap(gr, fmt.Font)/2;
				pnt.Y += GetMeasureStringVerticalGap(gr, fmt.Font);

				gr.FillRectangle(fmt.Brush, new RectangleF(pnt, sz));
			}
			else
				gr.DrawString(str, fmt.Font, fmt.Brush, pnt, sf);

			if (trOld != null)
				gr.Transform = trOld;
		}

		/// <summary>
		/// Draws string with given character format at given <see cref="Point"/>.
		/// </summary>
		/// <param name="gr"><see cref="Graphics"/> object to draw into.</param>
		/// <param name="str"><see cref="string"/> to be drawn.</param>
		/// <param name="pnt">Top-left <see cref="Point"/> of the drawn string.</param>
		/// <param name="fmt"><see cref="CharacterFormat"/> used to draw the <see cref="string"/>. </param>
		public void DrawString(Graphics gr, string str, PointF pnt,
		                       CharacterFormat fmt)
		{
			DrawString(gr, str, pnt, fmt, ParagraphAlignment.Left);
		}

		/// <summary>
		/// Draws string with given formating and width at specified point.
		/// </summary>
		/// <param name="img"><see cref="Image"/> object to draw into.</param>
		/// <param name="str"><see cref="string"/> to be drawn.</param>
		/// <param name="width">Width that string will be wrapped to.</param>
		/// <param name="ofs">Top-left point of the drawn string.</param>
		/// <param name="fmtCharacter"><see cref="CharacterFormat"/> used to draw the <see cref="string"/>. </param>
		/// <param name="fmtParagraph"><see cref="ParagraphFormat"/> used to draw the <see cref="string"/>. </param>
		public void DrawWrappedString(Image img, string str, PointF ofs, float width,
			CharacterFormat fmtCharacter, ParagraphFormat fmtParagraph)
		{
			Graphics gr = Graphics.FromImage(img);
			DrawWrappedString(gr, str, ofs, width, fmtCharacter, fmtParagraph);
			gr.Dispose();
		}

		/// <summary>
		/// Draws string with given formating into given rectangle.
		/// </summary>
		/// <param name="img"><see cref="Image"/> object to draw into.</param>
		/// <param name="str"><see cref="string"/> to be drawn.</param>
		/// <param name="rect"><see cref="Rectangle"/> that string is drawn into.</param>
		/// <param name="fmtCharacter"><see cref="CharacterFormat"/> used to draw the <paramref name="str"/>. </param>
		/// <param name="fmtParagraph"><see cref="ParagraphFormat"/> used to draw the <paramref name="str"/>. </param>
		/// <remarks>There's a problem while drawing strings using ClearType on transparent 
		/// background. Resulting text is going to look thick and jagged. If this applies to you, use
		/// rather Image DrawString(string, CharacterFormat) or 
		/// Image DrawStringInRectangle(string, SizeF, CharacterFormat, ParagraphFormat) where the 
		/// problem is corrected (at cost of a slight performance loss).</remarks>
		public void DrawStringInRectangle(Image img, string str, RectangleF rect,
		                                  CharacterFormat fmtCharacter, ParagraphFormat fmtParagraph)
		{
			Graphics gr = Graphics.FromImage(img);
			DrawStringInRectangle(gr, str, rect, fmtCharacter, fmtParagraph);
			gr.Dispose();
		}

		/// <summary>
		/// Draws string with given character format and alignment at given <see cref="Point"/>.
		/// </summary>
		/// <param name="img"><see cref="Image"/> object to draw into.</param>
		/// <param name="str"><see cref="string"/> to be drawn.</param>
		/// <param name="pnt"><see cref="Point"/> where the string will be drawn.</param>
		/// <param name="fmt"><see cref="CharacterFormat"/> used to draw the <see cref="string"/>. </param>
		/// <param name="align">Specifies whether the <paramref name="pnt"/> is at left, right 
		/// or in the center of the string. (<see cref="ParagraphAlignment.Full"/> means the same
		/// as <see cref="ParagraphAlignment.Left"/>.)</param>
		/// <remarks>There's a problem while drawing strings using ClearType on transparent 
		/// background. Resulting text is going to look thick and jagged. If this applies to you, use
		/// rather Image DrawString(string, CharacterFormat) or 
		/// Image DrawStringInRectangle(string, SizeF, CharacterFormat, ParagraphFormat) where the 
		/// problem is corrected (at cost of a slight performance loss).</remarks>
		public void DrawString(Image img, string str, PointF pnt,
		                       CharacterFormat fmt, ParagraphAlignment align)
		{
			Graphics gr = Graphics.FromImage(img);
			DrawString(gr, str, pnt, fmt, align);
			gr.Dispose();
		}

		/// <summary>
		/// Draws string with given character format at given <paramref name="pnt"/>.
		/// </summary>
		/// <param name="img"><see cref="Image"/> object to draw into.</param>
		/// <param name="str"><see cref="string"/> to be drawn.</param>
		/// <param name="pnt">Top-left <see cref="Point"/> of the drawn string.</param>
		/// <param name="fmt"><see cref="CharacterFormat"/> used to draw the <see cref="string"/>. </param>
		/// <remarks>There's a problem while drawing strings using ClearType on transparent 
		/// background. Resulting text is going to look thick and jagged. If this applies to you, use
		/// rather Image DrawString(string, CharacterFormat) or 
		/// Image DrawStringInRectangle(string, SizeF, CharacterFormat, ParagraphFormat) where the 
		/// problem is corrected (at cost of a slight performance loss).</remarks>
		public void DrawString(Image img, string str, PointF pnt,
		                       CharacterFormat fmt)
		{
			Graphics gr = Graphics.FromImage(img);
			DrawString(gr, str, pnt, fmt);
			gr.Dispose();
		}

		/// <summary>
		/// Returns <see cref="Image"/> object with <paramref name="str"/> drawn.
		/// </summary>
		/// <param name="str"><see cref="string"/> to be drawn.</param>
		/// <param name="fmt"><see cref="CharacterFormat"/> used to draw the <paramref name="str"/>. </param>
		/// <returns><see cref="Image"/> with given <paramref name="str"/> drawn. The image is sized
		/// just to fit the string.</returns>
		public Image DrawString(string str, CharacterFormat fmt)
		{
			/*Size size=Size.Ceiling(dummyGraphics.MeasureString(str, fmt.Font));
			Image res=new Bitmap(size.Width, size.Height);
			DrawString(res, str, new Point(0,0), fmt);
			return res;*/
			ParagraphFormat pf = new ParagraphFormat(ParagraphAlignment.Left, ParagraphVerticalAlignment.Top,
			                                         false, true, StringTrimming.None, null);
			SizeF sz = MeasureStringExactly(dummyGraphics, str, fmt, new ParagraphFormat(), true);
			sz.Width += 2*GetMeasureStringVerticalGap(dummyGraphics, fmt.Font);
			sz.Height += 2*GetMeasureStringHorizontalGap(dummyGraphics, fmt.Font);

			if ((fmt.Font.Style & FontStyle.Italic) != 0)
				sz.Width += 4;
			else
				sz.Width += 2;


			return DrawStringInRectangle(str, sz, fmt, pf);
		}

		/// <summary>
		/// Returns Image with <paramref name="str"/> drawn wrapped to given <paramref name="width"/>.
		/// </summary>
		/// <param name="str"><see cref="string"/> to be drawn.</param>
		/// <param name="width">Width that string will be wrapped to.</param>
		/// <param name="fmtCharacter"><see cref="CharacterFormat"/> used to draw the <see cref="string"/>. </param>
		/// <param name="fmtParagraph"><see cref="ParagraphFormat"/> used to draw the <see cref="string"/>. </param>
		/// <returns><see cref="Image"/> with given <paramref name="str"/> drawn.</returns>
		public Image DrawWrappedString(string str, float width,
		                               CharacterFormat fmtCharacter, ParagraphFormat fmtParagraph)
		{
			float height = GetWrappedHeight(DummyGraphics, str, fmtCharacter, width);
			ParagraphFormat pf2 = fmtParagraph.ShallowCopy();
			pf2.MultiLine = true;
			pf2.ShowIncompleteLines = true;
			pf2.VerticalAlignment = ParagraphVerticalAlignment.Top;
			return DrawStringInRectangle(str, new SizeF(width, height), fmtCharacter, fmtParagraph);
		}


		/// <summary>
		/// Returns Image with <paramref name="str"/> drawn into rectangle of given size.
		/// </summary>
		/// <param name="str"><see cref="string"/> to be drawn.</param>
		/// <param name="size">Size of rectangle string will be drawn into and also of the 
		/// returned image.</param>
		/// <param name="fmtCharacter"><see cref="CharacterFormat"/> used to draw the <paramref name="str"/>. </param>
		/// <param name="fmtParagraph"><see cref="ParagraphFormat"/> used to draw the <paramref name="str"/>. </param>
		/// <param name="topLeftOffset">Out parameter which receives top-left point of the rotated string. </param>
		/// <returns><see cref="Image"/> with given <paramref name="str"/> drawn.</returns>
		public Image DrawStringInRectangle(string str, SizeF size,
		                                   CharacterFormat fmtCharacter, ParagraphFormat fmtParagraph,
		                                   out PointF topLeftOffset)
		{
			//Prepare mask
			CharacterFormat mcf = fmtCharacter.ShallowCopy();
			ParagraphFormat mpf = fmtParagraph.ShallowCopy();
			mcf.Brush = new SolidBrush(Color.Black);
			mpf.BackgroundBrush = new SolidBrush(Color.White);
			mcf.IgnoreColorFormatting = true;
			Bitmap bMask = (Bitmap) DrawSolidBackgroundStringInRectangle(str, size, mcf,
			                                                             mpf, out topLeftOffset);

			//Prepare foreground
			CharacterFormat fgcf = fmtCharacter.ShallowCopy();
			ParagraphFormat fgpf = fmtParagraph.ShallowCopy();
			fgcf.FilledBounds = true;
			fgpf.BackgroundBrush = null;

			Bitmap bFg = (Bitmap) DrawSolidBackgroundStringInRectangle(str, size, fgcf,
			                                                           fgpf, out topLeftOffset);

			//And blend it together
			CopyIntensityAsAlpha(bMask, bFg);

			bMask.Dispose();

			//Using background
			if (fmtParagraph.BackgroundBrush != null)
			{
				Bitmap bRes = new Bitmap(bFg.Width, bFg.Height);
				Graphics rsgr = Graphics.FromImage(bRes); //resulting bitmap's graphics


				if (fmtCharacter.Angle == 0)
					rsgr.FillRectangle(fmtParagraph.BackgroundBrush, 0, 0, size.Width, size.Height);
				else
				{
					Matrix trOld = rsgr.Transform.Clone();
					rsgr.TranslateTransform(topLeftOffset.X, topLeftOffset.Y);
					rsgr.RotateTransform(fmtCharacter.Angle);

					rsgr.FillRectangle(fmtParagraph.BackgroundBrush, 0, 0, size.Width, size.Height);

					rsgr.Transform = trOld;
				}

				rsgr.DrawImage(bFg, 0, 0);
				rsgr.Dispose();
				bFg.Dispose();
				return bRes;
			}
			else
			{
				return bFg;
			}
		}


		/// <summary>
		/// Returns Image with <paramref name="str"/> drawn into rectangle of given size.
		/// </summary>
		/// <param name="str"><see cref="string"/> to be drawn.</param>
		/// <param name="size">Size of rectangle string will be drawn into and also of the 
		/// returned image.</param>
		/// <param name="fmtCharacter"><see cref="CharacterFormat"/> used to draw the <paramref name="str"/>. </param>
		/// <param name="fmtParagraph"><see cref="ParagraphFormat"/> used to draw the <paramref name="str"/>. </param>
		/// <returns><see cref="Image"/> with given <paramref name="str"/> drawn.</returns>
		public Image DrawStringInRectangle(string str, SizeF size,
		                                   CharacterFormat fmtCharacter, ParagraphFormat fmtParagraph)
		{
			PointF tmp;
			return DrawStringInRectangle(str, size, fmtCharacter, fmtParagraph, out tmp);
		}

		#endregion

		#region helper functions

		#region Conversion routines

		/// <summary>
		/// Converts <see cref="ParagraphAlignment"/> value into <see cref="StringAlignment"/>.
		/// </summary>
		/// <param name="par"><see cref="ParagraphAlignment"/> value to be converted.</param>
		/// <returns>Resulting <see cref="StringAlignment"/> value.</returns>
		private StringAlignment ParAl2StrAl(ParagraphAlignment par)
		{
			//As profiller showed this point to be a bottleneck, we have performed some optimalizations...
			return (StringAlignment) ((int) par%3);
			//Original unoptimized code
			/*
			switch(par)
			{
				case ParagraphAlignment.Left:
					return StringAlignment.Near;
				case ParagraphAlignment.Full:
					return StringAlignment.Near;
				case ParagraphAlignment.Right:
					return StringAlignment.Far;
				case ParagraphAlignment.Center:
					return StringAlignment.Center;
			}
			System.Diagnostics.Debug.Assert(false);
			return StringAlignment.Near;
			*/
		}

		/// <summary>
		/// Converts <see cref="ParagraphVerticalAlignment"/> value into <see cref="StringAlignment"/>.
		/// </summary>
		/// <param name="pva"><see cref="ParagraphVerticalAlignment"/> value to be converted.</param>
		/// <returns>Resulting <see cref="StringAlignment"/> value.</returns>
		private StringAlignment ParVertAl2StrAl(ParagraphVerticalAlignment pva)
		{
			//As profiller showed this point to be a bottleneck, we have performed some optimalizations...
			return (StringAlignment) pva;
			//Original unoptimized code
			/*
			switch(pva)
			{
				case ParagraphVerticalAlignment.Top:
					return StringAlignment.Near;
				case ParagraphVerticalAlignment.Bottom:
					return StringAlignment.Far;
				case ParagraphVerticalAlignment.Center:
					return StringAlignment.Center;
			}
			System.Diagnostics.Debug.Assert(false);
			return StringAlignment.Near;
			*/
		}

		/// <summary>
		/// Returns <see cref="StringFormat"/> object that fits best to given <see cref="CharacterFormat"/>
		/// and <see cref="ParagraphFormat"/> objects.
		/// </summary>
		/// <param name="fmtCharacter"><see cref="CharacterFormat"/> object</param>
		/// <param name="fmtParagraph"><see cref="ParagraphFormat"/> object</param>
		/// <returns><see cref="StringFormat"/> object</returns>
		internal StringFormat GetStringFormat(CharacterFormat fmtCharacter, ParagraphFormat fmtParagraph)
		{
			StringFormat sf = (StringFormat) StringFormat.GenericDefault.Clone();
			sf.Alignment = ParAl2StrAl(fmtParagraph.Alignment);
			sf.LineAlignment = ParVertAl2StrAl(fmtParagraph.VerticalAlignment);
			sf.HotkeyPrefix = fmtCharacter.HotkeyPrefix;
			sf.Trimming = fmtParagraph.Trimming;
			if (!fmtParagraph.ShowIncompleteLines)
				sf.FormatFlags |= StringFormatFlags.LineLimit;
			if (!fmtParagraph.MultiLine)
				sf.FormatFlags |= StringFormatFlags.NoWrap;
			return sf;
		}

		#endregion

		#region Rotated text in images

		/// <summary>
		/// Returns an image with given string drawn. As in this method is not present the
		/// ClearType bug correction, it's only to be used with solid background brush.<br/>
		/// Merit of this method is that it can properly handle rotated text.
		/// </summary>
		private Image DrawSolidBackgroundStringInRectangle(string str, SizeF size,
		                                                   CharacterFormat fmtCharacter, ParagraphFormat fmtParagraph, out PointF topLeftOffset)
		{
			float angle = fmtCharacter.Angle%360;
			float nAngle = (float) ((angle%90)*Math.PI/180); //normalized angle in RADIANS!
			PointF nOfs;
			SizeF nSize;
			if (nAngle == 0 || fmtCharacter.Angle == 0)
			{
				nOfs = new PointF(0, 0);
				nSize = size;
			}
			else
			{
				nOfs = new PointF((float) Math.Sin(nAngle)*size.Height, 0);
				nSize = new SizeF(
					(float) (Math.Cos(nAngle)*size.Width + Math.Sin(nAngle)*size.Height),
					(float) (Math.Sin(nAngle)*size.Width + Math.Cos(nAngle)*size.Height));
			}

			PointF rOfs; //Rotated top-left point
			SizeF rSize; //Rotated size
			if (angle < 90f)
			{
				rSize = nSize;
				rOfs = nOfs;
			}
			else if (angle < 180f)
			{
				rSize = new SizeF(nSize.Height, nSize.Width); //yes, width=height and height=width
				rOfs = new PointF(rSize.Width, nOfs.X);
			}
			else if (angle < 270f)
			{
				rSize = nSize;
				rOfs = new PointF(rSize.Width - nOfs.X, rSize.Height);
			}
			else
			{
				rSize = new SizeF(nSize.Height, nSize.Width); //yes, width=height and height=width
				rOfs = new PointF(0, rSize.Height - nOfs.X);
			}

			topLeftOffset = rOfs;

			Bitmap res = new Bitmap(Size.Ceiling(rSize).Width, Size.Ceiling(rSize).Height);

			Graphics gr = Graphics.FromImage(res);
			DrawStringInRectangle(gr, str, new RectangleF(rOfs, size), fmtCharacter, fmtParagraph);
			gr.Dispose();
			return res;
		}

		#endregion

		#region ClearType Bug correction

		/// <summary>
		/// For every pixel of dest bitmap takes color intensity at src bitmap and computes
		/// alpha value at dest.
		/// </summary>
		/// <param name="src">Bitmap used as source of dest's alpha canal.</param>
		/// <param name="dest">Destination bitmap.</param>
		/// <remarks>
		/// <para>Sizes of src and dest must be equal.</para>
		/// <para>White color results in transparent pixel and black in solid.</para>
		/// The formula is<br/>
		/// <code>
		/// if(sa==255)
		///		da = 255 - (sr+sg+sb)/3;
		/// else	
		///		da=0;
		/// </code>
		/// </remarks>
		private void CopyIntensityAsAlpha(Bitmap src, Bitmap dest)
		{
			Debug.Assert(src.Size == dest.Size);
			BitmapData srcd = src.LockBits(new Rectangle(new Point(0, 0), src.Size),
			                               ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
			BitmapData destd = dest.LockBits(new Rectangle(new Point(0, 0), dest.Size),
			                                 ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

			try
			{
				unsafe
				{
					int pixelSize = 4;
					int width = srcd.Width;
					int height = srcd.Height;

					byte* sBase = (byte*) srcd.Scan0;
					int sOfs = srcd.Stride - srcd.Width*pixelSize;

					byte* dBase = (byte*) destd.Scan0;
					int dOfs = srcd.Stride - srcd.Width*pixelSize;

					byte* sPixel = sBase;
					byte* dPixel = dBase;

					for (int y = 0; y < height; y++)
					{
						for (int x = 0; x < width; x++)
						{
							int a = *(sPixel + 3);
							int r = *(sPixel + 2);
							int g = *(sPixel + 1);
							int b = *(sPixel + 0);

							int fa;


							if (a == 255)
								fa = 255 - (r + g + b)/3;
							else //we don't care about source pixels which arent 100% visible
								fa = 0;

							int oa = *(dPixel + 3);

							*(dPixel + 3) = (byte) ((oa*fa) >> 8);

							sPixel += pixelSize;
							dPixel += pixelSize;
						}
						sPixel += sOfs;
						dPixel += dOfs;

					}
				}
			}
			finally
			{
				src.UnlockBits(srcd);
				dest.UnlockBits(destd);
			}
		}

		#endregion

		#region Full alignment routines

		/// <summary>
		/// Draws one full aligned line of text (ie. spaces are expanded to fill 
		/// exactly the given <paramref name="width"/>.
		/// </summary>
		/// <param name="gr"><see cref="Graphics"/> object to draw into.</param>
		/// <param name="line"><see cref="string"/> that is to be drawn.</param>
		/// <param name="pnt">Top-left <see cref="Point"/> of the line.</param>
		/// <param name="width">Width of the line.</param>
		/// <param name="cf"><see cref="CharacterFormat"/> used to draw the line. <br/>
		/// </param>
		/// <remarks>As this method isn't to be exposed as public, there are 
		/// several limitations:<br/>
		/// <see cref="CharacterFormat.Angle"/> has to be zero.<br/>
		/// Given string has to fit into given width. <br/>
		/// If not satisfied, results might be not as expected.</remarks>
		private void DrawFullAlignedLine(Graphics gr, string line, PointF pnt, float width,
		                                 CharacterFormat cf)
		{
			if (line.Length == 0)
				return;
			int whtcnt = 0; //count of white spaces
			for (int i = 0; i < line.Length; i++)
				if (Char.IsWhiteSpace(line, i))
					whtcnt++;

			if (whtcnt == 0)
				DrawString(gr, line, pnt, cf);
			else
			{
				float lnowhtwidth = 0; //width of line without white spaces
				int fwc = 0; //first word character
				for (int i = 0; i < line.Length; i++)
				{
					if (Char.IsWhiteSpace(line, i))
					{
						if (i > 0 && !Char.IsWhiteSpace(line, i - 1))
							lnowhtwidth += gr.MeasureString(line.Substring(fwc, i - fwc), cf.Font).Width;
					}
					else if (i > 0 && Char.IsWhiteSpace(line, i - 1))
						fwc = i;
				}
				if (!Char.IsWhiteSpace(line, line.Length - 1))
					lnowhtwidth += gr.MeasureString(line.Substring(fwc), cf.Font).Width;


				float whtxofs = (width - lnowhtwidth)/whtcnt;
				float xofs = pnt.X;
				fwc = 0;
				for (int i = 0; i < line.Length; i++)
				{
					if (Char.IsWhiteSpace(line, i))
					{
						if (i > 0 && !Char.IsWhiteSpace(line, i - 1))
						{
							string word = line.Substring(fwc, i - fwc);
							DrawString(gr, word, new PointF(xofs, pnt.Y), cf);
							xofs += gr.MeasureString(word, cf.Font).Width;
						}
						xofs += whtxofs;

					}
					else if (i > 0 && Char.IsWhiteSpace(line, i - 1))
						fwc = i;
				}
				if (!Char.IsWhiteSpace(line, line.Length - 1))
					DrawString(gr, line.Substring(fwc), new PointF(xofs, pnt.Y), cf);
			}
		}

		/// <summary>
		/// Draws full aligned string into given rectangle.
		/// </summary>
		/// <param name="gr"><see cref="Graphics"/> object to draw into.</param>
		/// <param name="str"><see cref="string"/> to be drawn.</param>
		/// <param name="rect"><see cref="Rectangle"/> that string is drawn into.</param>
		/// <param name="cf"><see cref="CharacterFormat"/> used to draw the <paramref name="str"/>. </param>
		/// <param name="pf"><see cref="ParagraphFormat"/> used to draw the <paramref name="str"/>. </param>
		/// <remarks>As this method isn't to be exposed as public, there are 
		/// several limitations:<br/>
		/// <see cref="Rectangle.Width"/> has to be non-zero.<br/>
		/// <see cref="CharacterFormat.Angle"/> has to be zero.<br/>
		/// <see cref="ParagraphFormat.MultiLine"/> has to be false.<br/>
		/// <see cref="ParagraphFormat.Alignment"/> has to be <see cref="ParagraphAlignment.Full"/>.<br/>
		/// <see cref="ParagraphFormat.BackgroundBrush"/> has to be null.<br/>
		/// If not satisfied, results might be not as expected.</remarks>
		private void DrawFullAlignedStringInRectangle(Graphics gr, string str, RectangleF rect,
		                                              CharacterFormat cf, ParagraphFormat pf)
		{
			Region oldClip = gr.Clip.Clone();
			gr.IntersectClip(rect);

			float yofs = rect.Y;

			StringFormat sfLeft; //StringFormat for drawing left-aligned lines
			{
				ParagraphFormat pf2 = pf.ShallowCopy();
				pf2.Alignment = ParagraphAlignment.Left;
				pf2.VerticalAlignment = ParagraphVerticalAlignment.Top;
				pf2.MultiLine = false;
				pf2.ShowIncompleteLines = true;
				sfLeft = GetStringFormat(cf, pf2);
			}


			//lineHeight & borders retrieving
			float lineHeight = GetLineHeight(gr, cf.Font);

			int lineCount;

			//lineCount retrieving
			{
				StringFormat sf = GetStringFormat(cf, pf);
				int charfit;
				gr.MeasureString(str, cf.Font, new SizeF(rect.Width, rect.Height), sf,
				                 out charfit, out lineCount);
			}


			//VerticalAlignment business
			switch (pf.VerticalAlignment)
			{
				case ParagraphVerticalAlignment.Bottom:
					yofs += rect.Height - lineHeight*lineCount;
					break;
				case ParagraphVerticalAlignment.Center:
					yofs += (rect.Height - lineHeight*lineCount)/2;
					break;
			}


			int flc = 0; //first line character
			int llc = flc; //last line character (actually the one after last)
			int lineIndex = 0;
			while (lineIndex < lineCount - 1 && flc < str.Length)
			{
				lineIndex++;

				llc = flc;

				//add spaces at the beginneing
				while (llc < str.Length && Char.IsWhiteSpace(str, llc))
					llc++;

				//add words until the line is full
				int pllc = llc; //potential last line character
				while (llc == pllc && llc < str.Length)
				{
					pllc++;
					while (pllc < str.Length && !Char.IsWhiteSpace(str, pllc))
						pllc++;
					if (gr.MeasureString(str.Substring(flc, pllc - flc), cf.Font).Width < rect.Width)
						llc = pllc;
				}

				//If nothing was added before, add single characters (rect.Width is too small)
				if (llc == flc)
					do
					{ //always at least one character per line
						llc++;
					} while (llc < str.Length && gr.MeasureString(str.Substring(flc, llc - flc + 1), cf.Font).Width < rect.Width);

				string line = str.Substring(flc, llc - flc);

				//Newline fotmatting
				int nlindex = line.IndexOf('\n');
				if (nlindex >= 0)
				{
					//Newline may be just "\n" or "\r\n"
					if (nlindex > 1 && line[nlindex - 1] == '\r')
						line = line.Substring(0, nlindex - 1);
					else
						line = line.Substring(0, nlindex);

					DrawString(gr, line, new PointF(rect.X, yofs), cf);

					flc += nlindex + 1;
				}
				else
				{
					DrawFullAlignedLine(gr, line, new PointF(rect.X, yofs), rect.Width, cf);

					flc = llc;
					//skip spaces at the end of line (just like Graphics.DrawString does)
					while (flc < str.Length && Char.IsWhiteSpace(str, flc))
						flc++;
				}

				yofs += lineHeight;

			}

			//And here comes the last line - it's always drawn left-aligned
			//(also, we have to get rid of newlines or they will sometimes show
			//in the bottom)

			gr.DrawString(str.Substring(flc), cf.Font, cf.Brush,
			              new RectangleF(rect.X, yofs, rect.Width, lineHeight), sfLeft);

			//Set back the original clipping region
			gr.Clip = oldClip;

		}

		#endregion

		#endregion

		#region properties

		/// <summary>
		/// Graphics object to be used for string measurement and similar tasks.
		/// </summary>
		public Graphics DummyGraphics
		{
			get { return dummyGraphics; }
		}

		#endregion
	}
}

