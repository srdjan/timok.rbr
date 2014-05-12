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
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PureComponents.TreeView.Internal
{
	/// <summary>
	/// CustomScrollBarBase
	/// </summary>
	[ToolboxItem(false)]	
	[DesignTimeVisible(false)]	
	internal abstract class CustomScrollBarBase : ScrollBar
	{
		#region private members

		/// <summary>
		/// 
		/// </summary>
		private const int SBM_SETSCROLLINFO = 0x00E9;

		/// <summary>
		/// 
		/// </summary>
		protected ScrollBarArea m_mouseArea;

		/// <summary>
		/// 
		/// </summary>
		protected ScrollBarArea m_mouseDownArea;

		/// <summary>
		/// 
		/// </summary>
		protected Point m_mouseDownPoint;

		/// <summary>
		/// 
		/// </summary>
		protected MouseButtons m_mouseButton;

		/// <summary>
		/// 
		/// </summary>
		protected int m_mouseDownOriginalValue;

		/// <summary>
		/// 
		/// </summary>
		protected Timer m_timer;

		#endregion

		#region construction

		/// <summary>
		/// Construction.
		/// </summary>
		public CustomScrollBarBase()
		{
			m_mouseArea = ScrollBarArea.Outside;
			m_mouseButton = MouseButtons.None;
			m_timer = new Timer();

			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.UserPaint, true);
			//SetStyle(ControlStyles.Opaque, true);
			SetStyle(ControlStyles.UserMouse, true);
		}

		#endregion

		#region implementation

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// 
		/// </summary>
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.ClassName = null;
				return cp;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="m"></param>
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			if (m.Msg == SBM_SETSCROLLINFO)
				Invalidate();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="keyData"></param>
		/// <returns></returns>
		protected override bool IsInputKey(Keys keyData)
		{
			switch (keyData)
			{
				case Keys.Up:
				case Keys.Left:
				case Keys.Down:
				case Keys.Right:
				case Keys.PageUp:
				case Keys.PageDown:
				case Keys.Home:
				case Keys.End:
					return true;
			}
			return base.IsInputKey(keyData);
		}

		/// <summary>
		/// 
		/// </summary>
		protected virtual void ForceRedraw()
		{
			this.Refresh();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gr"></param>
		/// <param name="rect"></param>
		/// <param name="d"></param>
		/// <param name="state"></param>
		protected abstract void DrawArrow(Graphics gr, Rectangle rect, Direction d, ElementState state);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gr"></param>
		/// <param name="rect"></param>
		/// <param name="o"></param>
		/// <param name="state"></param>
		protected abstract void DrawThumb(Graphics gr, Rectangle rect, Orientation o, ElementState state);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gr"></param>
		/// <param name="rect"></param>
		/// <param name="d"></param>
		/// <param name="state"></param>
		protected abstract void DrawInside(Graphics gr, Rectangle rect, Direction d, ElementState state);

		#endregion

		#region helper methods

		/// <summary>
		/// 
		/// </summary>
		/// <param name="val"></param>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		private static int Constraint(int val, int min, int max)
		{
			if (val > max)
				return max;
			if (val < min)
				return min;
			return val;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="r"></param>
		/// <returns></returns>
		protected Rectangle Transp(Rectangle r)
		{
			return new Rectangle(r.Y, r.X, r.Height, r.Width);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="r"></param>
		/// <returns></returns>
		protected virtual Rectangle ModifyRect(Rectangle r)
		{
			if (IsHorizontal)
				return Transp(r);
			else
				return r;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pnt"></param>
		/// <returns></returns>
		protected virtual int FirstCoord(Point pnt)
		{
			if (IsHorizontal)
				return pnt.X;
			else
				return pnt.Y;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		protected virtual int GetDistance(Point p)
		{
			int xdist, ydist;
			if (p.X < 0)
				xdist = -p.X;
			else
				xdist = Math.Max(0, p.X - Width);
			if (p.Y < 0)
				ydist = -p.Y;
			else
				ydist = Math.Max(0, p.Y - Height);
			return Math.Max(xdist, ydist);
		}

		#region Interop

		/// <summary>
		/// 
		/// </summary>
		/// <param name="nIndex"></param>
		/// <returns></returns>
		[DllImport("user32.dll")]
		protected static extern int GetSystemMetrics(int nIndex);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="uiAction"></param>
		/// <param name="uiParam"></param>
		/// <param name="pvParam"></param>
		/// <param name="fWinIni"></param>
		/// <returns></returns>
		[DllImport("user32.dll")]
		protected static extern bool SystemParametersInfoA(int uiAction, int uiParam, ref Int32 pvParam, int fWinIni);

		private const int SPI_GETKEYBOARDSPEED = 0x000A;
		private const int SPI_GETKEYBOARDDELAY = 0x0016;

		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected virtual int GetFirstDelay() //in ms
		{
			Int32 kbddl = -1;
			bool res = SystemParametersInfoA(SPI_GETKEYBOARDDELAY, 0, ref kbddl, 0);
			
			Debug.Assert(res && kbddl >= 0 && kbddl <= 3);

			if (!(res && kbddl >= 0 && kbddl <= 3))
				return 500;

			return (kbddl + 1) * 250;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected virtual int GetNextDelays() //in ms
		{
			Int32 kbdsp = -1;
			bool res = SystemParametersInfoA(SPI_GETKEYBOARDSPEED, 0, ref kbdsp, 0);

			Debug.Assert(res && kbdsp >= 0 && kbdsp <= 31);
			
			if (!(res && kbdsp >= 0 && kbdsp <= 31))
				return 50;
			
			return (int) (1000.0 / (2.5 + (kbdsp) * 27.5 / 31));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="t"></param>
		protected virtual void ModifyDelay(Timer t)
		{
			int nd = GetNextDelays();
			if (t.Interval != nd)
				t.Interval = nd;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		protected virtual ScrollBarArea HitTest(Point p)
		{
			if (TopLeftArrowRect.Contains(p))
				return ScrollBarArea.TopLeftArrow;
			if (TopLeftInnerRect.Contains(p))
				return ScrollBarArea.TopLeftInside;
			if (ThumbRect.Contains(p))
				return ScrollBarArea.Thumb;
			if (BottomRightInnerRect.Contains(p))
				return ScrollBarArea.BottomRightInside;
			if (BottomRightArrowRect.Contains(p))
				return ScrollBarArea.BottomRightArrow;
			return ScrollBarArea.Outside;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="elem"></param>
		/// <returns></returns>
		protected virtual ElementState GetElementState(ScrollBarArea elem)
		{
			if (!Enabled)
				return ElementState.Disabled;
			if (m_mouseButton == MouseButtons.Left)
			{
				if (m_mouseDownArea == elem)
				{
					if (elem == ScrollBarArea.Thumb)
					{
						if (m_mouseArea == elem)
							return ElementState.Pushed;
						else
							return ElementState.Normal;
					}
					else
						return ElementState.Pushed;
				}
				else
					return ElementState.Normal;

			}
			else if (m_mouseArea == elem)
			{
				return ElementState.Hover;
			}
			else
				return ElementState.Normal;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="elem"></param>
		/// <returns></returns>
		protected bool IsRepeatableElement(ScrollBarArea elem)
		{
			switch (elem)
			{
				case ScrollBarArea.BottomRightArrow:
				case ScrollBarArea.BottomRightInside:
				case ScrollBarArea.TopLeftArrow:
				case ScrollBarArea.TopLeftInside:
					return true;
				default:
					return false;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="elem"></param>
		/// <returns></returns>
		protected EventHandler GetElementHandler(ScrollBarArea elem)
		{
			switch (elem)
			{
				case ScrollBarArea.BottomRightArrow:
					return new EventHandler(BottomRightArrowPress);
				case ScrollBarArea.BottomRightInside:
					return new EventHandler(BottomRightInsidePress);
				case ScrollBarArea.TopLeftArrow:
					return new EventHandler(TopLeftArrowPress);
				case ScrollBarArea.TopLeftInside:
					return new EventHandler(TopLeftInsidePress);
				default:
					return null;
			}
		}

		#endregion

		#region properties

		/// <summary>
		/// 
		/// </summary>
		protected abstract bool IsHorizontal { get; }

		/// <summary>
		/// 
		/// </summary>
		protected override Size DefaultSize
		{
			get
			{
				if (IsHorizontal)
					return new Size(100, SystemInformation.HorizontalScrollBarHeight);
				else
					return new Size(SystemInformation.VerticalScrollBarWidth, 100);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		protected int RealMaximum
		{
			get { return Maximum - LargeChange + 1; }
		}

		/// <summary>
		/// 
		/// </summary>
		protected virtual int MaxThumbDistance
		{
			get { return 100; }
		}

		#region Sizes

		/// <summary>
		/// 
		/// </summary>
		protected virtual int FirstDim
		{
			get
			{
				if (IsHorizontal)
					return Width;
				else
					return Height;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		protected virtual int SecDim
		{
			get
			{
				if (IsHorizontal)
					return Height;
				else
					return Width;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		protected virtual int DefaultArrowSize
		{
			get
			{
				if (IsHorizontal)
					return SystemInformation.HorizontalScrollBarArrowWidth;
				else
					return SystemInformation.VerticalScrollBarArrowHeight;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		protected virtual int MinThumbSize
		{
			get { return 8; }
		}

		/// <summary>
		/// 
		/// </summary>
		protected int ThumbSize
		{
			get
			{
				if (Minimum >= RealMaximum)
					return -1;
				if (InnerSize < MinThumbSize + 1)
					return -1;
				int res = LargeChange * InnerSize / (Maximum - Minimum);
				return Math.Max(res, MinThumbSize);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		protected int InnerSize
		{
			get
			{
				if (FirstDim <= DefaultArrowSize * 2)
					return 0;
				else
					return FirstDim - DefaultArrowSize * 2;
			}
		}

		#endregion

		/// <summary>
		/// 
		/// </summary>
		protected int ThumbPos
		{
			get
			{
				if (ThumbSize <= 0)
					return -1;
				int sp = (InnerSize - ThumbSize);
				int res = sp * (Value - Minimum) / (RealMaximum - Minimum);
				
				return Math.Min(sp, res);
			}
		}

		#region Rectangles

		/// <summary>
		/// 
		/// </summary>
		protected Rectangle TopLeftArrowRect
		{
			get
			{
				if (FirstDim <= 3)
					return Rectangle.Empty;
				if (FirstDim < DefaultArrowSize * 2)
					return ModifyRect(new Rectangle(0, 0, SecDim, FirstDim / 2));
				else
					return ModifyRect(new Rectangle(0, 0, SecDim, DefaultArrowSize));
			}
		}

		/// <summary>
		/// 
		/// </summary>
		protected Rectangle BottomRightArrowRect
		{
			get
			{
				if (FirstDim <= 3)
					return Rectangle.Empty;
				if (FirstDim < DefaultArrowSize * 2)
					return ModifyRect(new Rectangle(0, FirstDim / 2, SecDim, FirstDim / 2 + FirstDim % 2));
				else
					return ModifyRect(
						new Rectangle(0, FirstDim - DefaultArrowSize, SecDim, DefaultArrowSize));
			}
		}

		/// <summary>
		/// 
		/// </summary>
		protected Rectangle InnerRect
		{
			get
			{
				if (InnerSize > 0)
					return ModifyRect(new Rectangle(0, DefaultArrowSize, SecDim, InnerSize));
				else
					return Rectangle.Empty;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		protected Rectangle ThumbRect
		{
			get
			{
				int tp = ThumbPos;
				if (tp >= 0)
					return ModifyRect(new Rectangle(0, DefaultArrowSize + tp, SecDim, ThumbSize));
				else
					return Rectangle.Empty;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		protected Rectangle TopLeftInnerRect
		{
			get
			{
				int tp = ThumbPos;
				if (tp <= 0)
					return Rectangle.Empty;
				return ModifyRect(new Rectangle(0, DefaultArrowSize, SecDim, tp));
			}
		}

		/// <summary>
		/// 
		/// </summary>
		protected Rectangle BottomRightInnerRect
		{
			get
			{
				int ins = InnerSize;
				if (ins <= 0)
					return Rectangle.Empty;
				int tp = ThumbPos;
				int ts = ThumbSize;
				if (tp <= 0)
					return ModifyRect(new Rectangle(0, DefaultArrowSize, SecDim, ins));
				int ofs = DefaultArrowSize + tp + ts;

				return ModifyRect(new Rectangle(0, ofs, SecDim, FirstDim - ofs - DefaultArrowSize));
			}
		}

		#endregion

		#endregion

		#region events

		#region Property changes

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnValueChanged(EventArgs e)
		{
			base.OnValueChanged(e);
			if (Value == Minimum)
				OnScroll(new ScrollEventArgs(ScrollEventType.First, Value));
			else if (Value >= RealMaximum)
				OnScroll(new ScrollEventArgs(ScrollEventType.Last, Value));
			Invalidate(InnerRect);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnResize(EventArgs e)
		{
			Invalidate();
		}

		#endregion

		#region Painting

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pe"></param>
		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);

			pe.Graphics.FillRectangle(Brushes.White, 0, 0, Width, Height);

			Rectangle rct;

			rct = TopLeftInnerRect;
			if (!rct.IsEmpty)
				DrawInside(pe.Graphics, rct,
				           IsHorizontal ? Direction.Left : Direction.Up,
				           GetElementState(ScrollBarArea.TopLeftInside));
			rct = BottomRightInnerRect;
			if (!rct.IsEmpty)
				DrawInside(pe.Graphics, rct,
				           IsHorizontal ? Direction.Right : Direction.Down,
				           GetElementState(ScrollBarArea.BottomRightInside));

			rct = ThumbRect;
			if (!rct.IsEmpty)
				DrawThumb(pe.Graphics, rct,
				          IsHorizontal ? Orientation.Horizontal : Orientation.Vertical,
				          GetElementState(ScrollBarArea.Thumb));

			rct = TopLeftArrowRect;
			if (!rct.IsEmpty)
				DrawArrow(pe.Graphics, rct,
				          IsHorizontal ? Direction.Left : Direction.Up,
				          GetElementState(ScrollBarArea.TopLeftArrow));
			rct = BottomRightArrowRect;
			if (!rct.IsEmpty)
				DrawArrow(pe.Graphics, rct,
				          IsHorizontal ? Direction.Right : Direction.Down,
				          GetElementState(ScrollBarArea.BottomRightArrow));
		}

		#endregion

		#region Mouse

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.Button == MouseButtons.Left)
			{
				m_mouseButton = e.Button;
				m_mouseArea = HitTest(new Point(e.X, e.Y));
				m_mouseDownPoint = new Point(e.X, e.Y);
				m_mouseDownArea = m_mouseArea;
				m_mouseDownOriginalValue = Value;

				if (IsRepeatableElement(m_mouseDownArea))
					OnRepeatableElementMouseDown(m_mouseDownArea);
			}
			Invalidate();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (e.Button == MouseButtons.Left)
			{
				if (IsRepeatableElement(m_mouseDownArea) && m_mouseArea == m_mouseDownArea)
					OnRepeatableElementMouseUp(m_mouseDownArea);

				if (m_mouseDownArea == ScrollBarArea.Thumb)
				{
					OnScroll(new ScrollEventArgs(ScrollEventType.ThumbPosition, Value));
					OnScroll(new ScrollEventArgs(ScrollEventType.EndScroll, Value));
				}

				m_mouseButton = MouseButtons.None;
				m_mouseArea = HitTest(new Point(e.X, e.Y));
				m_mouseDownPoint = Point.Empty;
				m_mouseDownArea = ScrollBarArea.Outside;
				Debug.Assert(!m_timer.Enabled);
			}
			Invalidate();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (m_mouseButton != MouseButtons.Left)
			{
				ScrollBarArea newArea = HitTest(new Point(e.X, e.Y));
				if (newArea != m_mouseArea)
				{
					m_mouseArea = newArea;
					Invalidate();
				}
			}
			else if (m_mouseDownArea == ScrollBarArea.Thumb)
			{
				Point pnt = new Point(e.X, e.Y);
				if (GetDistance(pnt) > MaxThumbDistance)
				{
					m_mouseArea = ScrollBarArea.Outside;
					Value = m_mouseDownOriginalValue;
					OnScroll(new ScrollEventArgs(ScrollEventType.ThumbTrack, Value));
				}
				else
				{
					m_mouseArea = ScrollBarArea.Thumb;
					int delta = FirstCoord(pnt) - FirstCoord(m_mouseDownPoint);
					int deltaVal = delta * (RealMaximum - Minimum) / (InnerSize - ThumbSize);
					Value = Constraint(m_mouseDownOriginalValue + deltaVal,
					                   Minimum, RealMaximum);
					ForceRedraw();
					OnScroll(new ScrollEventArgs(ScrollEventType.ThumbTrack, Value));
				}
			}
			else if (IsRepeatableElement(m_mouseDownArea))
			{
				ScrollBarArea newArea = HitTest(new Point(e.X, e.Y));
				if (newArea != m_mouseArea)
				{
					if (m_mouseArea == m_mouseDownArea && newArea != m_mouseDownArea)
						OnRepeatableElementMouseUp(m_mouseDownArea);
					ScrollBarArea oldArea = m_mouseArea;
					m_mouseArea = newArea;
					if (oldArea != m_mouseDownArea && m_mouseArea == m_mouseDownArea)
						OnRepeatableElementMouseDown(m_mouseDownArea);
					Invalidate();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			m_mouseArea = ScrollBarArea.Outside;
			Invalidate();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);
			int scrollLines = SystemInformation.MouseWheelScrollLines;
			if (e.Delta < 0)
			{
				Value = Math.Min(Value + SmallChange * scrollLines, RealMaximum);
				OnScroll(new ScrollEventArgs(ScrollEventType.SmallIncrement, Value));
			}
			else if (e.Delta > 0)
			{
				Value = Math.Max(Value - SmallChange * scrollLines, Minimum);
				OnScroll(new ScrollEventArgs(ScrollEventType.SmallDecrement, Value));
			}
			ForceRedraw();
		}

		#endregion

		#region Keys

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			switch (e.KeyCode)
			{
				case Keys.Up:
				case Keys.Left:
					TopLeftArrowPress(null, new EventArgs());
					return;
				case Keys.Down:
				case Keys.Right:
					BottomRightArrowPress(null, new EventArgs());
					return;
				case Keys.PageUp:
					TopLeftInsidePress(null, new EventArgs());
					return;
				case Keys.PageDown:
					BottomRightInsidePress(null, new EventArgs());
					return;
				case Keys.Home:
					Value = Minimum;
					return;
				case Keys.End:
					Value = RealMaximum;
					return;
			}
		}

		#endregion

		#region Elements

		/// <summary>
		/// 
		/// </summary>
		/// <param name="o"></param>
		/// <param name="e"></param>
		protected virtual void TopLeftArrowPress(object o, EventArgs e)
		{
			Value = Constraint(Value - SmallChange, Minimum, RealMaximum);
			ForceRedraw();
			OnScroll(new ScrollEventArgs(ScrollEventType.SmallDecrement, Value));
			if (o != null)
				ModifyDelay((Timer) o);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="o"></param>
		/// <param name="e"></param>
		protected virtual void BottomRightArrowPress(object o, EventArgs e)
		{
			Value = Constraint(Value + SmallChange, Minimum, RealMaximum);
			ForceRedraw();
			OnScroll(new ScrollEventArgs(ScrollEventType.SmallIncrement, Value));
			if (o != null)
				ModifyDelay((Timer) o);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="o"></param>
		/// <param name="e"></param>
		protected virtual void TopLeftInsidePress(object o, EventArgs e)
		{
			Value = Constraint(Value - LargeChange, Minimum, RealMaximum);
			ForceRedraw();
			OnScroll(new ScrollEventArgs(ScrollEventType.LargeDecrement, Value));
			if (o != null)
				ModifyDelay((Timer) o);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="o"></param>
		/// <param name="e"></param>
		protected virtual void BottomRightInsidePress(object o, EventArgs e)
		{
			Value = Constraint(Value + LargeChange, Minimum, RealMaximum);
			ForceRedraw();
			OnScroll(new ScrollEventArgs(ScrollEventType.LargeIncrement, Value));
			if (o != null)
				ModifyDelay((Timer) o);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="el"></param>
		protected virtual void OnRepeatableElementMouseDown(ScrollBarArea el)
		{
			Debug.Assert(!m_timer.Enabled);
			EventHandler eh = GetElementHandler(el);
			eh.Invoke(null, new EventArgs());
			m_timer.Tick += eh;
			m_timer.Interval = GetFirstDelay();
			m_timer.Start();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="el"></param>
		protected virtual void OnRepeatableElementMouseUp(ScrollBarArea el)
		{
			Debug.Assert(m_timer.Enabled);
			EventHandler eh = GetElementHandler(el);
			m_timer.Tick -= eh;
			m_timer.Stop();
			OnScroll(new ScrollEventArgs(ScrollEventType.EndScroll, Value));
		}

		#endregion

		#endregion

		#region internal enums

		/// <summary>
		/// 
		/// </summary>
		internal enum ScrollBarArea
		{
			/// <summary>
			/// 
			/// </summary>
			TopLeftArrow,

			/// <summary>
			/// 
			/// </summary>
			TopLeftInside,

			/// <summary>
			/// 
			/// </summary>
			Thumb,

			/// <summary>
			/// 
			/// </summary>
			BottomRightInside,

			/// <summary>
			/// 
			/// </summary>
			BottomRightArrow,

			/// <summary>
			/// 
			/// </summary>
			Outside
		}

		/// <summary>
		/// 
		/// </summary>
		internal enum Direction
		{
			/// <summary>
			/// 
			/// </summary>
			Up,

			/// <summary>
			/// 
			/// </summary>
			Down,

			/// <summary>
			/// 
			/// </summary>
			Left,

			/// <summary>
			/// 
			/// </summary>
			Right
		}

		/// <summary>
		/// 
		/// </summary>
		internal enum Orientation
		{
			/// <summary>
			/// 
			/// </summary>
			Vertical,

			/// <summary>
			/// 
			/// </summary>
			Horizontal
		}

		/// <summary>
		/// 
		/// </summary>
		internal enum ElementState
		{
			/// <summary>
			/// 
			/// </summary>
			Normal,

			/// <summary>
			/// 
			/// </summary>
			Disabled,

			/// <summary>
			/// 
			/// </summary>
			Hover,

			/// <summary>
			/// 
			/// </summary>
			Pushed
		}

		#endregion
	}
}