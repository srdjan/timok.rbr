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
using System.Diagnostics;
using System.Drawing; 
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Globalization;

namespace PureComponents.TreeView
{

	/// <summary>
	/// This class is being used by StringDrawUtils to draw 
	/// formatted strings.
	/// </summary>
	internal class SDUFormattedString
	{
		/// <summary>
		/// This struct can contain a position in SDUFormattedString.
		/// </summary>
		private struct Position
		{

			#region private members

			private int m_piece;
			private int m_index;
			private SDUFormattedString m_parent;

			#endregion

			#region construction

			/// <summary>
			/// Initializes a new Position struct pointing to the first character 
			/// of given parent SDUFormattedString object.
			/// </summary>
			/// <param name="parent"></param>
			public Position(SDUFormattedString parent)
			{
				m_parent=parent;
				m_piece=0;
				m_index=0;
			}
			/// <summary>
			/// Initializes a new Position struct pointing to specified position
			/// of given parent SDUFormattedString object.
			/// </summary>
			/// <param name="parent"></param>
			/// <param name="piece"></param>
			/// <param name="index"></param>
			public Position(SDUFormattedString parent, int piece, int index)
			{
				m_parent=parent;
				m_piece=piece;
				m_index=index;
			}

			/// <summary>
			/// Returns a new Position struct pointing to the first character 
			/// of given parent SDUFormattedString object.
			/// </summary>
			/// <param name="parent"></param>
			/// <returns></returns>
			public static Position Begin(SDUFormattedString parent)
			{
				Position res=new Position(parent);
				res.m_piece=0;
				res.m_index=0;
				return res;
			}

			/// <summary>
			/// Returns a new Position struct pointing behind the last character 
			/// of given parent SDUFormattedString object.
			/// </summary>
			/// <param name="parent"></param>
			/// <returns></returns>
			public static Position End(SDUFormattedString parent)
			{
				Position res=new Position(parent);
				res.m_piece=parent.Count;
				res.m_index=0;
				return res;
			}

			#endregion

			#region implementation

			/// <summary>
			/// Returns true if this Position struct points at the end 
			/// (ie. behind the last character).
			/// </summary>
			/// <returns></returns>
			public bool IsEnd()
			{
				return Piece>=Parent.Count;
			}

			/// <summary>
			/// Returns true if this Position struct points at the first character.
			/// </summary>
			/// <returns></returns>
			public bool IsBeginning()
			{
				return Piece==0 && Index==0;
			}

			/// <summary>
			/// Moves the position struct one character further.
			/// </summary>
			/// <returns></returns>
			/// <remarks>
			/// Of course this is not posible at the end of string, so in this case 
			/// an exception is thrown. (Wanting to do this means there's some bug.)
			/// </remarks>
			public Position Next()
			{
				if(IsEnd())
					throw new IndexOutOfRangeException("Calling Next() at the end of string.");
				m_index++;
				if(m_index>=Parent[m_piece].Length)
				{
					m_index=0;
					m_piece++;
				}
				return this;
			}

			/// <summary>
			/// Moves the position struct one character backward.
			/// </summary>
			/// <returns></returns>
			/// <remarks>
			/// Of course this is not posible at the beginning of string, so in this case 
			/// an exception is thrown. (Wanting to do this means there's some bug.)
			/// </remarks>
			public Position Back()
			{
				if(IsBeginning())
					throw new IndexOutOfRangeException("Calling Back() at the beginning of string.");
				m_index--;
				if(m_index<0)
				{
					m_piece--;
					m_index=Parent[Piece].Length-1;
				}
				return this;
			}

			#region Operator overrides

			public static Position operator++(Position p)
			{
				return p.Next();
			}
			public static Position operator--(Position p)
			{
				return p.Back();
			}
			public static bool operator==(Position a, Position b)
			{
				return a.Parent==b.Parent && a.Piece==b.Piece && a.Index==b.Index;
			}
			public static bool operator!=(Position a, Position b)
			{
				return !(a==b);
			}

			public static bool operator>=(Position a, Position b)
			{
				Debug.Assert(a.Parent==b.Parent);

				return a.Piece>b.Piece || (a.Piece==b.Piece && a.Index>=b.Index);
			}
			public static bool operator<=(Position a, Position b)
			{
				Debug.Assert(a.Parent==b.Parent);

				return a.Piece<b.Piece || (a.Piece==b.Piece && a.Index<=b.Index);
			}
			public static bool operator<(Position a, Position b)
			{
				Debug.Assert(a.Parent==b.Parent);

				return a.Piece<b.Piece || (a.Piece==b.Piece && a.Index<b.Index);
			}
			public static bool operator>(Position a, Position b)
			{
				Debug.Assert(a.Parent==b.Parent);

				return a.Piece>b.Piece || (a.Piece==b.Piece && a.Index>b.Index);
			}

			/// <summary>
			/// Moves the Position struct by <paramref name="ofs"/> characters forward
			/// (or -ofs backward, if ofs is negative).
			/// </summary>
			/// <param name="p"></param>
			/// <param name="ofs"></param>
			/// <returns>
			/// If moving out of the string margins would be result of this method,
			/// an exception is thrown. (Wanting to do this means there's some bug.)
			/// </returns>
			public static Position operator+(Position p, int ofs)
			{
				Position res=p;
				while(ofs<0)
				{
					if(res.Index>=-ofs)
					{
						res.m_index+=ofs;
						ofs=0;
					} 
					else
					{
						ofs+=res.m_index+1;
						res.m_index=0;
						if(res.m_piece==0)
							throw new IndexOutOfRangeException("Offset too small.");
						res.Back();
					}
				}
				while(ofs>0)
				{
					if(res.IsEnd())
						throw new IndexOutOfRangeException("Offset too big.");
					if(res.Index+ofs<res.Parent[res.Piece].Length)
					{
						res.m_index+=ofs;
						ofs=0;
					} 
					else
					{
						ofs-=res.Parent[res.Piece].Length - res.m_index;
						res.m_index=0;
						res.Next();
					}
				}
				return res;
			}
				
			/// <summary>
			/// Moves the Position struct by <paramref name="ofs"/> characters backward
			/// (or -ofs forward, if ofs is negative).
			/// </summary>
			/// <param name="p"></param>
			/// <param name="ofs"></param>
			/// <returns>
			/// If moving out of the string margins would be result of this method,
			/// an exception is thrown. (Wanting to do this means there's some bug.)
			/// </returns>
			public static Position operator-(Position p, int ofs)
			{
				return p+(-ofs);
			}
			public override bool Equals(object o)
			{
				return (o is Position) && (((Position)o)==this);
			}
			public override int GetHashCode()
			{
				return m_piece.GetHashCode()^m_index.GetHashCode();
			}

			/// <summary>
			/// Returns the number of characters between given Position objects.
			/// (Eg. (a+2)-a==2)
			/// </summary>
			/// <param name="a"></param>
			/// <param name="b"></param>
			/// <returns></returns>
			public static int operator-(Position a, Position b)
			{
				if(a<b)
					return -(new Interval(a, b)).Length;
				else
					return (new Interval(b, a)).Length;
			}

			#endregion


			#endregion

			#region properties

			/// <summary>
			/// Index of piece in parent's array.
			/// </summary>
			public int Piece
			{
				get { return m_piece; }
			}

			/// <summary>
			/// Index of character in piece string.
			/// </summary>
			public int Index
			{
				get { return m_index; }
			}

			/// <summary>
			/// SDUFormattedString object that this struct points into.
			/// </summary>
			public SDUFormattedString Parent
			{
				get { return m_parent; }
			}

			#endregion

		}

		/// <summary>
		/// This struct can contain an interval in SDUFormattedString.
		/// </summary>
		/// <remarks>
		/// The interval is represented as two positions - position of it's 
		/// first character (<see cref="Start"/>) and position of the first 
		/// character not contained in this interval (<see cref="End"/>).<br/>
		/// If Start==End, the interval is empty.
		/// </remarks>
		private struct Interval
		{

			#region private members

			private Position m_start;
			private Position m_end;

			#endregion

			#region construction

			/// <summary>
			/// Initializes a new Interval struct specified by gives starting and
			/// ending Position.
			/// </summary>
			/// <param name="start">First character of Interval.</param>
			/// <param name="end">Fisrt character not in interval. 
			/// ( (end-1) is the last character.)</param>
			public Interval(Position start, Position end)
			{
				Debug.Assert(start.Parent==end.Parent);

				m_start=start;
				m_end=end;
			}

			/// <summary>
			/// Returns interval containing full parent SDUFormattedString.
			/// </summary>
			/// <param name="parent"></param>
			/// <returns></returns>
			public static Interval Full(SDUFormattedString parent)
			{
				return new Interval(parent.Begin, parent.End);
			}

			#endregion

			#region implementation
			
			/// <summary>
			/// Splits interval at specified position.
			/// </summary>
			/// <param name="where">Position where to split.</param>
			/// <param name="o1">Receives interval that starts the same as original 
			/// and ends at given Position.</param>
			/// <param name="o2">Receives Interval that starts at given Position and 
			/// ends the same as original.</param>
			public void Split(Position where, out Interval o1, out Interval o2)
			{
				//as Interval and Position are structs, this is value copying
				o1=this;
				o2=this;
				o1.End=where;
				o2.Start=where;
			}


			#endregion

			#region properties

			public Position Start
			{
				get { return m_start; }
				set { m_start = value; }
			}

			/// <summary>
			/// Ending position of the interval. (End-1) is the last 
			/// character of given interval.
			/// </summary>
			public Position End
			{
				get { return m_end; }
				set { m_end = value; }
			}

			public bool Empty
			{
				get{ return Start>=End;}
			}

			public SDUFormattedString Parent
			{
				get
				{
					Debug.Assert(Start.Parent==End.Parent);
					return Start.Parent;
				}
			}

			/// <summary>
			/// Length of Interval in characters.
			/// </summary>
			public int Length
			{
				get
				{
					if(Start.Piece==End.Piece)
						return End.Index-Start.Index;

					int res = Parent[Start.Piece].Length - Start.Index;
					for (int p = Start.Piece+1; p < End.Piece; p++)
						res+=Parent[p].Length;
					res+=End.Index;
					return res;
				}
			}

			#endregion


		}
		
		#region private members

		/// <summary>
		/// Reference to StringDrawUtils object - just an acronym for
		/// StringDrawUtils.GetInstance(). (And maybe a slight speedup:-)
		/// </summary>
		static StringDrawUtils sdu;

		/// <summary>
		/// String that is used where ellipsis symbol is to be placed.
		/// </summary>
		const string ellipsisString="…";

		/// <summary>
		/// This ArrayList contains the whole string divided into 
		/// contiguous strings with the same formatting.
		/// </summary>
		ArrayList m_strings;

		/// <summary>
		/// This Arraylist contains CharacterFormat objects for strings 
		/// contained in <see cref="m_strings"/>.
		/// </summary>
		ArrayList m_formats;

		/// <summary>
		/// Contains initial format specified at the construction.
		/// </summary>
		CharacterFormat m_initialFormat;

		/// <summary>
		/// Part of full-justification magic - contains Positions of soft 
		/// line breaks.
		/// </summary>
		Hashtable m_softbreaks;

		#endregion

		#region construction

		/// <summary>
		/// Initializes a new SDUFormattedString object with given string and
		/// initial CharacterFormat.<br/>
		/// Format string definition is at documentation for 
		/// <see cref="CharacterFormat.Formatted"/> member.
		/// </summary>
		/// <param name="str"></param>
		/// <param name="initFmt"></param>
		public SDUFormattedString(string str, CharacterFormat initFmt)
		{
			if(sdu==null)
				sdu=StringDrawUtils.GetInstance();

			m_strings=new ArrayList();
			m_formats=new ArrayList();
			m_softbreaks=new Hashtable();
			m_initialFormat=initFmt;

			Debug.Assert(initFmt.Angle==0);

			CharacterFormat curfmt=initFmt.ShallowCopy();
			curfmt.Angle=0;
			curfmt.Formatted=false;

			if(!initFmt.Formatted)
			{
				AddFormattedPiece(str,curfmt);
			} 
			else
			{
				Stack brushes=new Stack();

				string curstr="";
				for (int i = 0; i < str.Length; i++)
				{
					if(str[i]!='#') 
						curstr+=str[i];
					else if(str[i+1]=='#') 
					{
						curstr+='#';
						i++;
					} 
					else 
					{ 
						if(curstr.Length>0)
						{
							AddFormattedPiece(curstr,curfmt);
							curstr="";
							curfmt=curfmt.ShallowCopy();
						}
						switch(str[i+1])
						{
							case 'C':
								if(str.Length > i + 2 && str[i+2]=='-')
								{
									if(!initFmt.IgnoreColorFormatting)
									{
										if(brushes.Count==0)
                      throw new ArgumentException("Invalid format string", "str=" + str);

										curfmt.Brush=(Brush) brushes.Pop();
									}
									i+=2;
								} 
								else if(str.Length > i + 7)
								{
									string colstr=str.Substring(i+2,6);
									if(colstr.Length<6)
                    throw new ArgumentException("Invalid format string", "str=" + str);
									int colint;
									try
									{
										colint=Int32.Parse(colstr,NumberStyles.HexNumber);
									} 
									catch(FormatException)
									{
										throw new ArgumentException("Invalid format string","str=" + str);
									}
									Color clr=Color.FromArgb( (255<<24) + colint );

									if(!initFmt.IgnoreColorFormatting) 
									{
										brushes.Push(curfmt.Brush);
										curfmt.Brush=new SolidBrush(clr);
									}
									i+=7;
								}
								break;
							case 'B':
							switch(str[i+2])
							{
								case '+':
									curfmt.Font=new Font(curfmt.Font,curfmt.Font.Style|FontStyle.Bold);
									break;
								case '-':
									curfmt.Font=new Font(curfmt.Font,curfmt.Font.Style & ~FontStyle.Bold);
									break;
								default:
									throw new ArgumentException("Invalid format string","str=" + str);
							}
								i+=2;
								break;
							case 'I':
							switch(str[i+2])
							{
								case '+':
									curfmt.Font=new Font(curfmt.Font,curfmt.Font.Style|FontStyle.Italic);
									break;
								case '-':
									curfmt.Font=new Font(curfmt.Font,curfmt.Font.Style & ~FontStyle.Italic);
									break;
								default:
									throw new ArgumentException("Invalid format string","str=" + str);
							}
								i+=2;
								break;
							case 'U':
							switch(str[i+2])
							{
								case '+':
									curfmt.Font=new Font(curfmt.Font,curfmt.Font.Style|FontStyle.Underline);
									break;
								case '-':
									curfmt.Font=new Font(curfmt.Font,curfmt.Font.Style & ~FontStyle.Underline);
									break;
								default:
									throw new ArgumentException("Invalid format string","str=" + str);
							}
								i+=2;
								break;
							default:
								throw new ArgumentException("Invalid format string","str=" + str);
						}
					}
				
				}
				if(curstr.Length>0)
					AddFormattedPiece(curstr,curfmt);
			}

		}

		#endregion

		#region implementation
		
		#region Wraping lines
		/// <summary>
		/// Adds newline characters to wrap lines to fit given width.
		/// </summary>
		/// <param name="gr"></param>
		/// <param name="width"></param>
		/// <remarks>
		/// This method adds characters into SDUFormattedString
		/// so every Position and Interval structures becomes invalid.<br/>
		/// This method is called by StringDrawUtils to wrap lines to fit a rectangle.
		/// </remarks>
		public void WrapLines(Graphics gr, float width)
		{
			if(width==0)
				return;
			Interval i=Interval.Full(this);

			Position nl=IndexOf('\n',i);
			while (!nl.IsEnd())
			{
				Interval iOld, iNew;
				i.Split(nl, out iOld, out iNew);
				iNew.Start++;

				if( (!iOld.Empty) && this[iOld.End-1]=='\r' )
					iOld.End--;
				ArrayList changes=new ArrayList(8);
				WrapSingleLine(gr, width, iOld, changes);
				//Let's make iNew valid again (shift it by count of characters added)
				for (int ci = 0; ci < changes.Count; ci++)
				{
					Position change = (Position) changes[ci];
					if(iNew.End.Piece==change.Piece && iNew.End>=change)
						iNew.End++;
				}
				

				i=iNew;
				nl=IndexOf('\n',i);
			}
			WrapSingleLine(gr, width, i, null);

		}


		/// <summary>
		/// Adds newline characters into single line to make it fit given width.
		/// </summary>
		/// <param name="gr"></param>
		/// <param name="width"></param>
		/// <param name="interval"></param>
		/// <param name="changes"></param>
		/// <remarks>
		/// This function changes character numbers of SDUFormattedString
		/// so every user Position and Interval structures becomes invalid.<br/>
		/// Also as we save positions of newly added newline characters, WrapSingleLine 
		/// calls must go from left to right - to not damage previously saved positions.
		/// </remarks>
		private void WrapSingleLine(Graphics gr, float width, Interval interval, ArrayList changes)
		{
			if(width==0)
				return;
			Debug.Assert(IndexOf('\n',interval).IsEnd());

			Interval cl=new Interval(interval.Start, interval.Start); //current line
			while(cl.End<interval.End) 
			{
				//Add beginning spaces
				while(cl.End<interval.End && Char.IsWhiteSpace(this[cl.End]) )
					cl.End++;
				//Add words until the line is full
				Interval cll=cl;	//current line (longer)
				do
				{
				while( cll.End<interval.End && !Char.IsWhiteSpace(this[cll.End]) )
					cll.End++;
					if(Measure(gr,cll).Width<=width)
					{
						while(cll.End<interval.End && Char.IsWhiteSpace(this[cll.End]) )
							cll.End++;
						cl=cll;
					}
				}while (cl.End<interval.End && cll.End==cl.End);
			
				//If nothing was added, add as much single characters as possible; at least one
				if(cl.Empty)
				{
					cl.End++;
					cll=cl;
					while(cll.End<interval.End && Measure(gr,cll).Width<=width) 
					{
						cl=cll;
						cll.End++;
					} 
				}

				if(cl.End<interval.End)
				{
					//!!! Here we are adding newline character
					string str=(string)m_strings[cl.End.Piece];
					str=str.Substring(0,cl.End.Index)+"\n"+str.Substring(cl.End.Index);
					m_strings[cl.End.Piece]=str;
					//Report change position
					if(changes!=null)
						changes.Add(cl.End);
					//Register it as soft break.
					m_softbreaks.Add(cl.End, true);
				
					//let's make interval valid again
					if(interval.End.Piece==cl.End.Piece)
						interval.End++;

					//and new line starts right after newline character (now it's empty)
					cl.Start=cl.End=cl.End+1;
				}
			}

		}

		#endregion

		#region Trimming
		/// <summary>
		/// Draws trimmed line with trimming specified in ParagraphFormat.Trimming.
		/// </summary>
		/// <param name="gr"></param>
		/// <param name="pnt"></param>
		/// <param name="width"></param>
		/// <param name="i"></param>
		/// <param name="pf"></param>
		private void DrawTrimmedLine(Graphics gr, PointF pnt, float width, 
			Interval i, ParagraphFormat pf)
		{
			if(i.Empty)
				return;

			bool showEllipsis;
			Interval si;//shown interval

			GetTrimmingInfo(gr, width, i, pf, out si, out showEllipsis);

			PointF pos=pnt;
			Position nl;
			Interval iOld, iNew;
			while( (nl=IndexOf('\n', si)) <si.End )
			{
				si.Split(nl, out iOld, out iNew);
				DrawSingleLineUnaligned(gr, pos, iOld);
				pos.X+=Measure(gr,iOld,true).Width - 
					2*sdu.GetMeasureStringHorizontalGap(gr, GetCharacterFormat(iOld.End).Font);

				iNew.Start++;
				si=iNew;
			}
			DrawSingleLineUnaligned(gr, pos, si);
			if(showEllipsis)
			{
				pos.X+=Measure(gr,si,true).Width - 
					2*sdu.GetMeasureStringHorizontalGap(gr, GetCharacterFormat(si.End).Font);
				if(si.End.IsEnd())
					si.End--;
				sdu.DrawString(gr, ellipsisString, pos,GetCharacterFormat(si.End));
			}
		}

		/// <summary>
		/// Sets out parameters with informations needed to trim the line correctly.<br/>
		/// </summary>
		/// <param name="gr"></param>
		/// <param name="width"></param>
		/// <param name="i"></param>
		/// <param name="pf"></param>
		/// <param name="shownInterval"></param>
		/// <param name="showEllipsis"></param>
		private void GetTrimmingInfo(Graphics gr, float width, Interval i, ParagraphFormat pf, 
			out Interval shownInterval, out bool showEllipsis)
		{
			//First we have to decide whether the ellipsis symbol will be shown
			switch(pf.Trimming)
			{
				case StringTrimming.EllipsisPath: 
				case StringTrimming.EllipsisCharacter: 
				case StringTrimming.EllipsisWord: 
					showEllipsis=true;
					break;
				case StringTrimming.Character:
				case StringTrimming.Word:
				case StringTrimming.None:
				default:
					showEllipsis=false;
					break;
			}

			
			Interval si; //"shownInterval" abbreviation
			Position nl;

			float remainingWidth;
			if(showEllipsis)
			{
				float ellipsisWidth=gr.MeasureString(ellipsisString, InitialFormat.Font).Width;
				remainingWidth=width-ellipsisWidth;
			} 
			else 
				remainingWidth=width;

			
			//And now lets compute the shown interval
			switch(pf.Trimming)
			{
				case StringTrimming.EllipsisWord: 
					si=i;
					nl=IndexOf('\n', i);
					si.End=(nl<i.End)?(nl):(i.End);

					while((!si.Empty) && Char.IsWhiteSpace(this[si.End-1]))
						si.End--;
					while( (!si.Empty) && MeasureIgnoringNewlines(gr,si).Width>remainingWidth)
					{
						while((!si.Empty) && !Char.IsWhiteSpace(this[si.End-1]))
							si.End--;
						while((!si.Empty) && Char.IsWhiteSpace(this[si.End-1]))
							si.End--;
					}

					break;
					//ellipsisPath is quite problematic and not very often used, 
					//so let's ignore it :-)
				case StringTrimming.EllipsisPath: 
				case StringTrimming.EllipsisCharacter: 
				case StringTrimming.Character:
					si=i;
					nl=IndexOf('\n', i);
					if(nl<i.End && IsWrappingNL(nl) )
					{
						Position nnl=IndexOf('\n',new Interval(nl+1, i.End));
						if(nnl<i.End)
							si.End=nnl;
						else
							si.End=i.End;
					} 
					else
						si.End=(nl<i.End)?(nl):(i.End);

					while( ( (showEllipsis &&(!si.Empty)) || si.Length>1 ) 
						&& MeasureIgnoringNewlines(gr, si).Width>remainingWidth)
						si.End--;

					break;


				case StringTrimming.Word:
					si=i;
					nl=IndexOf('\n', i);
					si.End=(nl<i.End)?(nl):(i.End);

					break;

				
				case StringTrimming.None:
				default:
					si=i;
					nl=IndexOf('\n', i);
					if(nl<i.End && IsWrappingNL(nl) )
					{
						Position nnl=IndexOf('\n',new Interval(nl+1, i.End));
						if(nnl<i.End)
							si.End=nnl;
						else
							si.End=i.End;
					}
					si.End=(nl<i.End)?(nl):(i.End);

					break;
			}

			shownInterval=si;
		}

		/// <summary>
		/// Returns size of given line if it's drawn as trimmed.
		/// </summary>
		/// <param name="gr"></param>
		/// <param name="width"></param>
		/// <param name="i"></param>
		/// <param name="pf"></param>
		/// <returns></returns>
		private SizeF MeasureTrimmedLine(Graphics gr, float width, Interval i, ParagraphFormat pf)
		{
			bool showEllipsis;
			Interval shown;
			GetTrimmingInfo(gr, width, i, pf, out shown, out showEllipsis);
			SizeF res=MeasureIgnoringNewlines(gr, shown);
			if(showEllipsis)
			{
				float ellipsisWidth=gr.MeasureString(ellipsisString, InitialFormat.Font).Width;
				res.Width+=ellipsisWidth;
			}
			return res;
		}

		#endregion

		#region Drawing

		/// <summary>
		/// Draws string into specified rectangle using given ParagraphFormat.
		/// </summary>
		/// <param name="gr"></param>
		/// <param name="rect"></param>
		/// <param name="pf"></param>
		/// <remarks>
		/// This is the 'main' method of the class. This one is called by StringDrawUtils
		/// to draw formatted string.<br/>
		/// As this class (and consequently this method) isn't to be published, there are some limitations:<br/>
		/// Angle of InitialFormat (specified at construction) has to be zero.<br/>
		/// BackgroundBrush
		/// </remarks>
		public void DrawStringInRectangle(Graphics gr, RectangleF rect, ParagraphFormat pf)
		{
			Debug.Assert(InitialFormat.Angle==0);
			Debug.Assert(pf.BackgroundBrush==null);


			if(pf.VerticalAlignment!=ParagraphVerticalAlignment.Top)
			{
				int lines=Math.Min(GetMaxVisibleLineCount(gr, rect.Height, pf), 
					CountOf(Interval.Full(this), '\n')+1);
				float lineHeight=sdu.GetLineHeight(gr,InitialFormat.Font);
				float totalHeight=(lineHeight*lines)+
					sdu.GetMeasureStringVerticalGap(gr, InitialFormat.Font);

				switch(pf.VerticalAlignment)
				{
					case ParagraphVerticalAlignment.Bottom:
						rect.Offset(0, rect.Height-totalHeight);
						break;
					case ParagraphVerticalAlignment.Center:
						rect.Offset(0, (rect.Height-totalHeight)/2);
						break;
				}
			}

			
			Region oldClip=gr.Clip.Clone();

			gr.IntersectClip(rect);

			DrawStringLineByLine(gr, rect, pf);

			gr.Clip=oldClip;

		}

		/// <summary>
		/// This one draws string into given rectangle with even some more limitations 
		/// than DrawStringInRectangle.<br/>
		/// It should not be called from anywhere else than DrawStringInRectangle.
		/// </summary>
		/// <param name="gr"></param>
		/// <param name="rect"></param>
		/// <param name="pf"></param>
		private void DrawStringLineByLine(Graphics gr, RectangleF rect, ParagraphFormat pf)
		{
			Interval i=Interval.Full(this);
			if(i.Empty)
				return;

			PointF pnt=rect.Location;
			float lineHeight=sdu.GetLineHeight(gr,InitialFormat.Font);

			int maxLineCount=GetMaxVisibleLineCount(gr, rect.Size.Height,pf);
			int lineIndex=0;

			Position nl=IndexOf('\n',i);
			while (!nl.IsEnd() && lineIndex<maxLineCount-1)
			{
				Interval iOld, iNew;
				i.Split(nl, out iOld, out iNew);
				iNew.Start++;

				if( (!iOld.Empty) && this[iOld.End-1]=='\r' )
					iOld.End--;

				DrawSingleLineAligned(gr,pnt,rect.Width,iOld,pf, false);
				pnt.Y+=lineHeight;

				i=iNew;
				nl=IndexOf('\n',i);

				lineIndex++;
			}
			if(nl.IsEnd())
				DrawSingleLineAligned(gr,pnt,rect.Width,i,pf, false);
			else
				DrawSingleLineAligned(gr,pnt,rect.Width,i,pf, true);
		}

		

		/// <summary>
		/// Draws left aligned single line with its top-left point at <paramref name="pnt"/>.
		/// </summary>
		/// <param name="gr"></param>
		/// <param name="pnt"></param>
		/// <param name="i"></param>
		private void DrawSingleLineUnaligned(Graphics gr, PointF pnt, Interval i)
		{
			
			if(i.Start.IsEnd() || i.Empty)
				return;

			CharacterFormat cf;

			if(i.Start.Piece==i.End.Piece)
			{
				cf = GetCharacterFormat(i.Start).ShallowCopy();
				cf.Formatted=false;
				sdu.DrawString(gr,this[i.End.Piece].Substring(i.Start.Index,i.End.Index-i.Start.Index),pnt,cf);
				return;
			}

			
			float horizBorder=sdu.GetMeasureStringHorizontalGap(gr, InitialFormat.Font);

			cf = GetCharacterFormat(i.Start).ShallowCopy();
			cf.Formatted=false;
			sdu.DrawString(gr,this[i.Start.Piece].Substring(i.Start.Index),pnt,cf);
			pnt.X+=Measure(gr, 
				new Interval(i.Start, BeginningOfPiece(i.Start.Piece+1)), true).Width-2*horizBorder;
			

			for (int p = i.Start.Piece+1; p < i.End.Piece; p++)
			{
				cf = GetCharacterFormat(p).ShallowCopy();
				cf.Formatted=false;
				sdu.DrawString(gr,this[p],pnt,cf);
				pnt.X+=Measure(gr, 
					new Interval(BeginningOfPiece(p), BeginningOfPiece(p+1)), true).Width-2*horizBorder;
			}
			if(i.End.Index>0)
			{
				cf = GetCharacterFormat(i.End).ShallowCopy();
				cf.Formatted=false;
				sdu.DrawString(gr,this[i.End.Piece].Substring(0,i.End.Index),pnt,cf);
			}
			
		}
		#endregion

		#region Aligned drawing

		/// <summary>
		/// Draws single line into rectangle located at <paramref name="pnt"/> with specified width.
		/// ParagraphAlignemnt is considered here.
		/// </summary>
		/// <param name="gr"></param>
		/// <param name="pnt"></param>
		/// <param name="width"></param>
		/// <param name="i"></param>
		/// <param name="pf"></param>
		/// <param name="trimmedLine"></param>
		private void DrawSingleLineAligned(Graphics gr, PointF pnt, float width, 
			Interval i, ParagraphFormat pf, bool trimmedLine)
		{
			float lineWidth;
			switch(pf.Alignment)
			{
				case ParagraphAlignment.Left:
					if(trimmedLine)
						DrawTrimmedLine(gr, pnt, width,  i, pf);
					else
						DrawSingleLineUnaligned(gr, pnt, i);
					break;
				case ParagraphAlignment.Right:
					if(trimmedLine)
					{
						lineWidth=MeasureTrimmedLine(gr, width, i, pf).Width;
						DrawTrimmedLine(gr,new PointF(pnt.X+width-lineWidth,pnt.Y), width, i, pf);
					} 
					else 
					{
						lineWidth=Measure(gr, i, false).Width;
						DrawSingleLineUnaligned(gr,new PointF(pnt.X+width-lineWidth,pnt.Y), i);
					}
					break;
				case ParagraphAlignment.Center:
					if(trimmedLine)
					{
						lineWidth=MeasureTrimmedLine(gr, width, i, pf).Width;
						DrawTrimmedLine(gr,new PointF(pnt.X+(width-lineWidth)/2,pnt.Y), width, i, pf);
					} 
					else 
					{
						lineWidth=Measure(gr, i, false).Width;
						DrawSingleLineUnaligned(gr, new PointF(pnt.X+(width-lineWidth)/2,pnt.Y), i);
					}
					break;
				case ParagraphAlignment.Full:
					if(trimmedLine)
						DrawTrimmedLine(gr, pnt, width,  i, pf);
					else if(IsWrapedLine(i))
						DrawSingleLineFullyAligned(gr, pnt, width, i);
					else
						DrawSingleLineUnaligned(gr, pnt, i);
					break;
			}
		}

		/// <summary>
		/// Draws fully aligned single line with specified width.
		/// </summary>
		/// <param name="gr"></param>
		/// <param name="pnt"></param>
		/// <param name="width"></param>
		/// <param name="i"></param>
		private void DrawSingleLineFullyAligned(Graphics gr, PointF pnt, float width, Interval i)
		{

			while(i.End>i.Start && Char.IsWhiteSpace(this[i.End-1]))
				i.End--;
			if(i.Empty)
				return;
			Interval[] words=SplitIntoWords(i);
			float[] widths=new float[words.Length];
			float totalwidth=0;
			for (int j = 0; j < words.Length; j++)
			{
				widths[j]=Measure(gr, words[j], false).Width;
				totalwidth+=widths[j];
			}
			int spaceCnt=0;
			for (Position pos = i.Start; pos < i.End; pos++)
				if(Char.IsWhiteSpace(this[pos]))
					spaceCnt++;
			
			if(spaceCnt==0)
				DrawSingleLineUnaligned(gr, pnt, i);
			else 
			{
				float spaceOfs=(width-totalwidth)/spaceCnt;
				PointF pos=pnt;
				Position prev=i.Start;
				for (int j = 0; j < words.Length; j++)
				{
					pos.X+=spaceOfs*(words[j].Start-prev);
					DrawSingleLineUnaligned(gr, pos, words[j]);
					pos.X+=widths[j];
					prev=words[j].End;
				}
			}
		}
		#endregion

		#region Measuring

		public float GetTotalHeight(Graphics gr)
		{
			float lineHeight=sdu.GetLineHeight(gr,InitialFormat.Font);
			return (lineHeight*GetLineCount())+
				sdu.GetMeasureStringVerticalGap(gr, InitialFormat.Font);
		}

		public int GetLineCount()
		{
			return CountOf(Interval.Full(this), '\n')+1;
		}


		/// <summary>
		/// Measures part of the text. This part of text must all have the same formatting
		/// (ie. must be stored in the same item of m_strings).
		/// </summary>
		/// <param name="gr"></param>
		/// <param name="piece"></param>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="trailingSpaces"></param>
		/// <returns></returns>
		private SizeF MeasurePiece(Graphics gr, int piece, int start, int end, bool trailingSpaces)
		{
			if(piece>=Count)
			{
				return new SizeF(0,sdu.GetLineHeight(gr,InitialFormat.Font));
			}

			if(end>this[piece].Length)
				end=this[piece].Length;

			if(start>=end)
			{
				return new SizeF(0,sdu.GetLineHeight(gr,InitialFormat.Font));
			}

			ParagraphFormat pf = new ParagraphFormat(ParagraphAlignment.Left, ParagraphVerticalAlignment.Top,
				false,true, StringTrimming.None, null);
			CharacterFormat cf=GetCharacterFormat(piece);
			
			string str=this[piece].Substring(start, end-start);


			SizeF size=sdu.MeasureStringExactly(gr,str,cf, pf, trailingSpaces);

			return size;
		}

		/// <summary>
		/// Measures single line of text (ie. that given interval mustn't contain 
		/// newline character).
		/// </summary>
		/// <param name="gr"></param>
		/// <param name="i"></param>
		/// <param name="trailingSpaces"></param>
		/// <returns></returns>
		private SizeF MeasureSingleLine(Graphics gr, Interval i, bool trailingSpaces)
		{
			Debug.Assert(IndexOf('\n',i).IsEnd());

			float horizBorder=sdu.GetMeasureStringHorizontalGap(gr, InitialFormat.Font);
			
			SizeF sz;
			if(i.Start.Piece==i.End.Piece) 
			{
				sz=MeasurePiece(gr,i.Start.Piece,i.Start.Index, i.End.Index, trailingSpaces);
				sz.Width+=2*horizBorder;
				return sz;
			}

			sz=MeasurePiece(gr,i.Start.Piece,i.Start.Index, true);
			float width=sz.Width;
			float height=sz.Height;
			for (int p = i.Start.Piece+1; p < i.End.Piece; p++)
			{
				sz=MeasurePiece(gr,p, true);
				width+=sz.Width;
				height=Math.Max(height, sz.Height);
			}
			if(i.End.Index>0)
			{
				sz=MeasurePiece(gr,i.End.Piece,0,i.End.Index, trailingSpaces);
				width+=sz.Width;
				height=Math.Max(height, sz.Height);
			}
			width+=2*horizBorder;
			return new SizeF(width, height);
		}

		/// <summary>
		/// Measures text contained in given interval.
		/// </summary>
		/// <param name="gr"></param>
		/// <param name="interval"></param>
		/// <param name="trailingSpaces"></param>
		/// <returns></returns>
		private SizeF Measure(Graphics gr, Interval interval, bool trailingSpaces)
		{
			SizeF res=new SizeF(0,0);
			Interval i=interval;
			Position nl=IndexOf('\n',i);

			//as there's no font-changing formatting command, heights of all lines are
			//the same
			float lineHeight=sdu.GetLineHeight(gr, InitialFormat.Font);

			SizeF lsize;

			while (!nl.IsEnd())
			{
				Interval iOld, iNew;
				i.Split(nl, out iOld, out iNew);
				iNew.Start++;

				if( (!iOld.Empty) && this[iOld.End-1]=='\r' )
					iOld.End--;
				lsize=MeasureSingleLine(gr, iOld, trailingSpaces);
				res.Width=Math.Max(res.Width, lsize.Width);
				res.Height+=lineHeight;

				i=iNew;
				nl=IndexOf('\n',i);
			}
			lsize=MeasureSingleLine(gr, i, trailingSpaces);
			res.Width=Math.Max(res.Width, lsize.Width);
			res.Height+=lineHeight;

			return res;
		}

		/// <summary>
		/// Measures text contained in given interval.
		/// </summary>
		/// <param name="gr"></param>
		/// <param name="interval"></param>
		/// <returns></returns>
		private SizeF Measure(Graphics gr, Interval interval)
		{
			return Measure(gr, interval, true);
		}

		/// <summary>
		/// Measures part of the text. This part of text must all have the same formatting
		/// (ie. must be stored in the same item of m_strings).
		/// </summary>
		/// <param name="gr"></param>
		/// <param name="piece"></param>
		/// <param name="start"></param>
		/// <param name="trailingSpaces"></param>
		/// <returns></returns>
		private SizeF MeasurePiece(Graphics gr, int piece, int start, bool trailingSpaces)
		{
			return MeasurePiece(gr,piece,start, this[piece].Length, trailingSpaces);
		}
		/// <summary>
		/// Measures part of the text. This part of text must all have the same formatting
		/// (ie. must be stored in the same item of m_strings).
		/// </summary>
		/// <param name="gr"></param>
		/// <param name="piece"></param>
		/// <param name="trailingSpaces"></param>
		/// <returns></returns>
		private SizeF MeasurePiece(Graphics gr, int piece, bool trailingSpaces)
		{
			return MeasurePiece(gr,piece,0, this[piece].Length,trailingSpaces);
		}

		/// <summary>
		/// Measures text contained in given interval.
		/// </summary>
		/// <param name="gr"></param>
		/// <param name="trailingSpaces"></param>
		/// <returns></returns>
		public SizeF Measure(Graphics gr, bool trailingSpaces)
		{
			return Measure(gr, Interval.Full(this), trailingSpaces);
		}

		/// <summary>
		/// Measures text contained in given interval. Newline characters 
		/// are ignored and so it's measured as a long single line.
		/// </summary>
		/// <param name="gr"></param>
		/// <param name="i"></param>
		/// <returns></returns>
		private SizeF MeasureIgnoringNewlines(Graphics gr, Interval i)
		{
			SizeF size=new SizeF(0, 0);

			SizeF msz;	//Measured size

			Position nl;
			Interval iOld, iNew;
			while( (nl=IndexOf('\n', i)) <i.End )
			{
				i.Split(nl, out iOld, out iNew);

				msz=Measure(gr,iOld,true);
				size.Width+=msz.Width;
				size.Height=Math.Max(size.Height, msz.Height);

				iNew.Start++;
				i=iNew;
			}
			msz=Measure(gr,i,true);
			size.Width+=msz.Width;
			size.Height=Math.Max(size.Height, msz.Height);

			return size;
		}

		#endregion

		#endregion

		#region helper functions

		/// <summary>
		/// Adds string with given format to the end of this formatted string.
		/// </summary>
		/// <param name="str"></param>
		/// <param name="fmt"></param>
		private void AddFormattedPiece(string str, CharacterFormat fmt)
		{
			//No reason for storing empty pieces (and lots against it)
			if(str.Length==0)
				return;
			//Rotation is matter of global format, piece formats doesn't implement
			//it (so it angle to be zero)
			Debug.Assert(fmt.Angle==0);
			m_strings.Add(str);
			m_formats.Add(fmt);
		}

		/// <summary>
		/// Returns Position struct pointing at the beginning of given piece.
		/// </summary>
		/// <param name="i"></param>
		/// <returns></returns>
		private Position BeginningOfPiece(int i)
		{
			return new Position(this,i,0);
		}

		/// <summary>
		/// Returns position of specified character within given interval.
		/// </summary>
		/// <param name="ch"></param>
		/// <param name="i"></param>
		/// <returns></returns>
		private Position IndexOf(char ch, Interval i)
		{
			if(i.Start.IsEnd() || i.Empty)
				return this.End;

			int ind;

			if(i.Start.Piece==i.End.Piece)
			{
				ind=this[i.Start.Piece].IndexOf(ch, i.Start.Index, i.End.Index-i.Start.Index);
				if(ind>=0)
					return new Position(this,i.Start.Piece, ind);
				else
					return this.End;
			}

			ind=this[i.Start.Piece].IndexOf(ch,i.Start.Index);
			if( ind>=0 )
				return new Position(this,i.Start.Piece,ind);
			for (int p = i.Start.Piece+1; p < i.End.Piece; p++)
			{
				string str = this[p];
				ind=str.IndexOf(ch);
				if( ind>=0 )
					return new Position(this,p,ind);
			}
			if(i.End.Index>0)
			{
				ind=this[i.End.Piece].IndexOf(ch,0,i.End.Index);
				if(ind>=0)
					return new Position(this,i.End.Piece,ind);
			}
			return this.End;
		}

		/// <summary>
		/// Returns string containing unformatted text of specified interval.
		/// </summary>
		/// <param name="i"></param>
		/// <returns></returns>
		private string GetIntervalText(Interval i)
		{
			if(i.Start.IsEnd() || i.Empty)
				return "";

			if(i.Start.Piece==i.End.Piece)
				return this[i.End.Piece].Substring(i.Start.Index,i.End.Index-i.Start.Index);

			string res=this[i.Start.Piece].Substring(i.Start.Index);
			

			for (int p = i.Start.Piece+1; p < i.End.Piece; p++)
				res+=this[p];
			if(i.End.Index>0)
				res+=this[i.End.Piece].Substring(0,i.End.Index);
			return res;
			
		}

		/// <summary>
		/// Returns array of intervals. Each of these intervals contain one of words in
		/// given <paramref name="i"/>. Intervals are ordered successive.
		/// </summary>
		/// <param name="i"></param>
		/// <returns></returns>
		private Interval[] SplitIntoWords(Interval i)
		{
			ArrayList al=new ArrayList();
			Position wstart=i.Start;
			Interval w=new Interval(i.Start, i.Start);;

			while(w.End<i.End) 
			{
				wstart=w.End;
				while(wstart<i.End && Char.IsWhiteSpace(this[wstart]))
					wstart++;
				
				if(wstart==i.End)
					break;
			
				w=new Interval(wstart, wstart);
				while(w.End<i.End && !Char.IsWhiteSpace(this[w.End]))
					w.End++;
				al.Add(w);
			}
			return (Interval[]) al.ToArray(typeof (Interval));
		}

		/// <summary>
		/// True if character at given position is 'soft newline character' 
		/// (ie. newline character added during word wrapping).
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		private bool IsWrappingNL(Position pos)
		{
			return m_softbreaks[pos]!=null;
		}

		/// <summary>
		/// True if specified interval ends with 'soft newline character'
		/// (ie. newline character added during word wrapping).
		/// </summary>
		/// <param name="i"></param>
		/// <returns></returns>
		private bool IsWrapedLine(Interval i)
		{
			if(i.End.IsEnd())
				return false;
			Position nl=i.End;
			if(this[nl]=='\r' && !(nl+1).IsEnd())
				nl++;
			if(this[nl]!='\n')
				return false;
			return IsWrappingNL(nl);
		}

		/// <summary>
		/// Returns maximum visible line count based on give height and 
		/// line height of used font.
		/// </summary>
		/// <param name="gr"></param>
		/// <param name="height"></param>
		/// <param name="pf"></param>
		/// <returns></returns>
		private int GetMaxVisibleLineCount(Graphics gr, float height, ParagraphFormat pf)
		{
			float res=height/sdu.GetLineHeight(gr,InitialFormat.Font);
			if(pf.ShowIncompleteLines)
				return (int)Math.Ceiling(res);
			else
				return (int)Math.Floor(res);
		}

		/// <summary>
		/// Returns count of occurrencies of given character in specified interval.
		/// </summary>
		/// <param name="i"></param>
		/// <param name="c"></param>
		/// <returns></returns>
		private int CountOf(Interval i, char c)
		{
			int cnt=0;
			Interval tmp, iNew;
			Position pos;
			while( (pos=IndexOf(c,i)) < i.End )
			{
				cnt++;
				i.Split(pos, out tmp, out iNew);
				iNew.Start++;
				i=iNew;
			}
			return cnt;
		}

		/// <summary>
		/// Returns CharactedFormat of character at given position.
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		private CharacterFormat GetCharacterFormat(Position pos)
		{
			return (CharacterFormat)m_formats[pos.Piece];
		}
		/// <summary>
		/// Returns character format of specified piece of text.
		/// </summary>
		/// <param name="piece"></param>
		/// <returns></returns>
		private CharacterFormat GetCharacterFormat(int piece)
		{
			return (CharacterFormat)m_formats[piece];
		}

		/// <summary>
		/// Converts object to unformatted string.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return GetIntervalText(Interval.Full(this));
		}

		#endregion

		#region properties

		/// <summary>
		/// Contiguous piece of text with same formatting.
		/// </summary>
		private string this[int i]
		{
			get
			{
				return (string)m_strings[i];
			}
		}
		/// <summary>
		/// Character at given position.
		/// </summary>
		private char this[Position p]
		{
			get
			{
				return this[p.Piece][p.Index];
			}
		}

		/// <summary>
		/// Count of pieces with different formatting.
		/// </summary>
		private int Count
		{
			get
			{
				return m_strings.Count;
			}
		}
		/// <summary>
		/// Number of characters in this string.
		/// </summary>
		public int Length
		{
			get
			{
				int len=0;
				for (int i = 0; i < m_strings.Count; i++)
				{
					string str = (string) m_strings[i];
					len+=str.Length;
				}
				return len;
			}
		}
		/// <summary>
		/// Initial format specified at construction.
		/// </summary>
		public CharacterFormat InitialFormat
		{
			get
			{
				return m_initialFormat;
			}
		}

		/// <summary>
		/// Position of first character of the object.
		/// </summary>
		private Position Begin
		{
			get
			{
				return Position.Begin(this);
			}
		}
		/// <summary>
		/// Position of the end of object. (One character after the last character.)
		/// </summary>
		private Position End
		{
			get
			{
				return Position.End(this);
			}
		}

		#endregion

		
	}

}
