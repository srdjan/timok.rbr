/*
 * Copyright © 2005, Patrik Bohman
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without modification, 
 * are permitted provided that the following conditions are met:
 *
 *    - Redistributions of source code must retain the above copyright notice, 
 *      this list of conditions and the following disclaimer.
 * 
 *    - Redistributions in binary form must reproduce the above copyright notice, 
 *      this list of conditions and the following disclaimer in the documentation 
 *      and/or other materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
 * IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, 
 * OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY 
 * OF SUCH DAMAGE.
 */

using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.Collections;
using System.Drawing.Drawing2D;   


namespace Pabo.Calendar {
	
	public enum mcMonthProperty {
		Transparency = 0, FormatTrailing, Padding, DateAlign, ShowMonthInDay,
		TextAlign, ImageAlign, DateFont, TextFont, BackgroundImage
	}
	
	public enum mcMonthColor {
		Selected = 0, SelectedBorder, SelectedText, SelectedDate, Focus, FocusBorder, FocusText,
		FocusDate,Text, Date, Background, Border, TrailingText, TrailingDate, TrailingBackColor,
		WeekendBackColor, WeekendDate, WeekendText,	DisabledBackColor, DisabledText, DisabledDate
	}

	public enum mcMonthBorderStyle {
		Normal = 0, Selected, Focus
	}

	#region Delegates

	public delegate void MonthPropertyEventHandler(object sender, MonthPropertyEventArgs e);
	public delegate void MonthColorEventHandler(object sender, MonthColorEventArgs e);
	public delegate void MonthBorderStyleEventHandler(object sender, MonthBorderStyleEventArgs e);

	#endregion
	
	
	/// <summary>
	/// Summary description for DayProperties.
	/// </summary>
	[TypeConverter(typeof(MonthTypeConverter))]
	public class Month : IDisposable {	
		#region private class members
		
		private const int NO_AREA = -2;
		
		private bool disposed;
		private MonthCalendar m_calendar;
		private Font m_dateFont;
		private Font m_textFont;
		private Rectangle m_rect;
		private Region m_region;
       	
		int m_selLeft;
		int m_selRight;
		int m_selTop;
		int m_selBottom;
		
		private bool m_newSelection;
		internal Day[] m_days;
				
		private MonthPadding m_padding;
		private TransparencyCollection m_transparency; 
		private mcItemAlign m_dateAlign;
		private mcItemAlign m_textAlign;
		private mcItemAlign m_imageAlign;
		
		private bool m_showMonth;
		private bool m_mouseDown;
		private bool m_formatTrailing;

		private Image m_backgroundImage;
		internal ArrayList m_selArea = new ArrayList(); // Collection of selected areas.

		private MonthColors m_colors;
		private MonthBorderStyles m_borderStyles;

		private DateTime m_selectedMonth;
		private bool m_selected;		
		private int m_dayInFocus;

		private float m_dayWidth;
		private float m_dayHeight;

		#endregion
		
		#region EventHandlers
		
		public event DayRenderEventHandler DayRender;
		public event DayEventHandler DayLostFocus;
		public event DayEventHandler DayGotFocus;
		public event DayClickEventHandler DayClick;
		public event DayClickEventHandler DayDoubleClick;
		public event DaySelectedEventHandler DaySelected;
		public event DaySelectedEventHandler DayDeselected;
		public event MonthPropertyEventHandler PropertyChanged;
		public event MonthColorEventHandler ColorChanged;
		public event MonthBorderStyleEventHandler BorderStyleChanged;

		#endregion
		
		#region constructor

		public Month(MonthCalendar calendar) {

			#region just to remove it from the Task List on compile
			if (m_newSelection) {
				m_newSelection = m_newSelection;
			}
			if (DayGotFocus == null) {
				DayGotFocus = DayGotFocus;
			}
			#endregion just to remove it from the Task List on compile

			m_calendar = calendar;
			m_dateFont = new Font("Microsoft Sans Serif",(float)8.25);
			m_textFont = new Font("Microsoft Sans Serif",(float)8.25);

			m_dayInFocus = -1;
			m_selArea.Clear();
			
			m_formatTrailing = true;
			m_imageAlign = mcItemAlign.TopLeft;
			m_dateAlign = mcItemAlign.Center;
			m_textAlign = mcItemAlign.BottomLeft;
						
			// we need 42 (7 * 6) days for display
			m_days = new Day[42];
			for (int i = 0;i<42;i++) {
				m_days[i] = new Day();
				m_days[i].Month = this;
				m_days[i].Calendar = m_calendar;
			}

			m_colors = new MonthColors(this); 
			m_borderStyles = new MonthBorderStyles(this); 
			m_padding = new MonthPadding(this);
			m_transparency = new TransparencyCollection(this);
		}

		#endregion

		#region Dispose
		
		protected virtual void Dispose(bool disposing) {
			if (!disposed) {
				if (disposing) {
					m_dateFont.Dispose();
					m_textFont.Dispose();
					m_region.Dispose();
					if (m_backgroundImage != null)
						m_backgroundImage.Dispose();
				}
				// shared cleanup logic
				disposed = true;
			}
		}

		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		
		#endregion
		
		#region Poperties
		
		internal bool MouseDown {
			get {
				return m_mouseDown;
			}
		}

		internal MonthCalendar Calendar {
			get {
				return m_calendar;
			}
		}

		internal DateTime SelectedMonth {
			get {
				return m_selectedMonth;
			}
			set {
				m_selectedMonth = value;
			}
		}

		internal Rectangle Rect {
			get {
				return m_rect;
			}
			set {
				m_rect = value;
				m_region = new Region(m_rect);
			}
		}

		internal float DayWidth {
			get {
				return m_dayWidth;
			}
		}

		internal float DayHeight {
			get {
				return m_dayHeight;
			}
		}

		[Browsable(true)]
		[Description("Indicates wether formatting should be applied to trailing dates.")]
		[DefaultValue(true)]
		public bool FormatTrailing {
			get {
				return m_formatTrailing;
			}
			set {
				if (m_formatTrailing!=value) {
					m_formatTrailing = value;
					if (PropertyChanged!=null)
						PropertyChanged(this,new MonthPropertyEventArgs(mcMonthProperty.FormatTrailing)); 
					Calendar.Invalidate(); 
				}
			}
		}

		[Browsable(true)]
		[Description("Padding (Horizontal, Vertical)")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public MonthPadding Padding {
			get {
				return m_padding;
			}
			set {
				if (value != m_padding) {
					if (value != null) m_padding = value;
					SetupDays();
					Calendar.Invalidate();
				}

			}
		}

		[Browsable(true)]
		[Description("Transparency (Background, Text)")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TransparencyCollection Transparency {
			get {
				return m_transparency;
			}
			set {
				if (value != m_transparency) {
					if (value != null) m_transparency = value;
					SetupDays();
					Calendar.Invalidate();
				}

			}
		}


		[Description("Image used as background.")]
		public Image BackgroundImage {
			get {
				return m_backgroundImage;
			}
			set {
				if (m_backgroundImage != value) {
					m_backgroundImage = value;
					if (PropertyChanged != null)
						PropertyChanged(this, new MonthPropertyEventArgs(mcMonthProperty.BackgroundImage));
					m_calendar.Invalidate();
				}
			}

		}
		
		[Editor(typeof(AlignEditor),typeof(System.Drawing.Design.UITypeEditor))]
		[Description("Determines the position of the date within the day.")]
		[DefaultValue(typeof(mcItemAlign),"Center")]
		public mcItemAlign DateAlign {
			get {
				return m_dateAlign;
			}
			set {
				if (m_dateAlign!=value) {
					m_dateAlign = value;
					if (PropertyChanged!=null)
						PropertyChanged(this,new MonthPropertyEventArgs(mcMonthProperty.DateAlign)); 
					m_calendar.Invalidate();
				}
			}

		}

		[Description("Indicates wether the month should be displayed for the first and last day.")]
		[DefaultValue(false)]
		public bool ShowMonthInDay {
			get {
				return m_showMonth;
			}
			set {
				if (m_showMonth!=value) {
					m_showMonth = value;
					if (PropertyChanged!=null)
						PropertyChanged(this,new MonthPropertyEventArgs(mcMonthProperty.ShowMonthInDay)); 
					m_calendar.Invalidate();			
				}
			}
		}
		
		[Editor(typeof(AlignEditor),typeof(System.Drawing.Design.UITypeEditor))]
		[Description("Determines the position for the text within the day.")]
		[DefaultValue(typeof(mcItemAlign),"BottomLeft")]
		public mcItemAlign TextAlign {
			get {
				return m_textAlign;
			}
			set {
				if (m_textAlign!=value) {
					m_textAlign = value;
					if (PropertyChanged!=null)
						PropertyChanged(this,new MonthPropertyEventArgs(mcMonthProperty.TextAlign)); 
					m_calendar.Invalidate();
				}
			}

		}

		[Editor(typeof(AlignEditor),typeof(System.Drawing.Design.UITypeEditor))]
		[Description("Determines the position of the image within the day.")]
		[DefaultValue(typeof(mcItemAlign),"TopLeft")]
		public mcItemAlign ImageAlign {
			get {
				return m_imageAlign;
			}
			set {
				if (m_imageAlign!=value) {
					m_imageAlign = value;
					if (PropertyChanged!=null)
						PropertyChanged(this,new MonthPropertyEventArgs(mcMonthProperty.ImageAlign)); 
					m_calendar.Invalidate();
				}
			}

		}
			
		[Description("Borders used in calendar.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public MonthBorderStyles BorderStyles {
			get {
				return m_borderStyles;
			}
		}

		[Description("Colors used in calendar.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public MonthColors Colors {
			get {
				return m_colors;
			}
		}
	
		[Description("Font used for date.")]
		[DefaultValue(typeof(Font),"Microsoft Sans Serif; 8,25pt")]
		public Font DateFont {
			get {
				return m_dateFont;
			}
			set {
				if (m_dateFont!=value) {
					m_dateFont = value;
					if (PropertyChanged!=null)
						PropertyChanged(this,new MonthPropertyEventArgs(mcMonthProperty.DateFont)); 
					m_calendar.Invalidate();
				}
			}
		}

		[Description("Font used for text.")]
		[DefaultValue(typeof(Font),"Microsoft Sans Serif; 8,25pt")]
		public Font TextFont {
			get {
				return m_textFont;
			}
			set {
				if (m_textFont!=value) {
					m_textFont = value;
					if (PropertyChanged!=null)
						PropertyChanged(this,new MonthPropertyEventArgs(mcMonthProperty.TextFont)); 
					m_calendar.Invalidate();
				}
			}
		}
	
	
		#endregion

		#region Methods
		
		private string[] DaysInSelection(int sel) {
			string[] days;
			days = new string[0];
			days.Initialize();
			for (int i = 0;i<42;i++) {
				if ( (sel == m_days[i].SelectionArea) || ((sel==NO_AREA) && (m_days[i].State == mcDayState.Selected)) ) {
					days = AddDate(m_days[i].Date.ToShortDateString(),days);  
				}
			}
			return days;
		}
		
		private bool IsDateEnabled(DateTime dt) {
			DateItem[] info;
			bool enabled = true;
			info = m_calendar.GetDateInfo(dt);
			for (int i = 0;i<info.Length;i++) {
				if (info[i].Enabled == false) {
					enabled = false;
					break;
				}
			}
			return enabled;
		}

		private int SelectionDayCount(int sel) {	
			int nr = 0;
			for (int i = 0;i<42;i++) {
				if (sel == m_days[i].SelectionArea)
					nr++;
			}
			return nr;
		}

		private void RemoveDay(int day, bool raiseEvent) {
			string[] d;
			
			// retrieve the days area
			int sel = m_days[day].SelectionArea;
			d = DaysInSelection(sel);
			
			SelectionArea area = (SelectionArea)m_selArea[sel];
			
			// reset the area
			area.Begin = -1;
			area.End = -1;
					
			// reset the day
			m_days[day].State = mcDayState.Normal;
			m_days[day].SelectionArea = -1; 
			
			if (raiseEvent) {
				// Raise event
				if (this.DayDeselected!=null)
					this.DayDeselected(this,new DaySelectedEventArgs(d));
			}
				
			for (int i = 0;i<42;i++) {
				// We dont want to add the day we are removing
				if (i!=day) {
					// Check if day belong to the same area as the day we are removing
					if (m_days[i].SelectionArea == sel) {
						// Create new selected day
						m_days[i].State = mcDayState.Normal;
						NewSelectedDay(i);
					}
				}
			}
			m_newSelection = true;
			
		}
	
		internal void NewSelectedDay(int day) {
			NewSelectedArea(day,day);
		}
				
		internal void NewSelectedRange(int from, int to) {
			if ((m_days[from].Rectangle.Bottom == m_days[to].Rectangle.Bottom )) {
				// dates are in the same week , treat as area
				NewSelectedArea(from,to);	
			}
			else {
				// days are not in same week , select individually
				if (m_calendar.SelectionMode==mcSelectionMode.MultiExtended) {
					for (int i = from;i<=to;i++) {
						if (m_days[i].State !=mcDayState.Selected) 
							NewSelectedDay(i);
					}
				}
			}
		}

		internal void Remove(int day) {
			if (m_days[day].State == mcDayState.Selected) {
				if (SelectionDayCount(m_days[day].SelectionArea) ==1) {
					RemoveSelection(false,m_days[day].SelectionArea);
				}
				else {
					RemoveDay(day,false);
				}
			}
		}

		internal void NewSelectedArea(int topLeft, int bottomRight) {	
			
			if ((!m_calendar.ExtendedKey) || (m_calendar.SelectionMode<mcSelectionMode.MultiExtended)) {
				// clear selection and start over
				RemoveSelection(true);
				m_selArea.Clear();
			}
			else if ((m_calendar.SelectionMode==mcSelectionMode.MultiExtended)) {
				// Add new area
				//m_selIndex++;
			}
			
			m_selArea.Add(new SelectionArea(topLeft,bottomRight,this));
		
			// Mark area as selected
			MarkAreaAsSelected(topLeft,bottomRight,m_selArea.Count-1);
			
			m_selected = true;
			m_newSelection = false;
		}

		internal void DeselectRange(int from, int to) {
			string[] dates = new string[0];
			
			// if MultiExtended , press CTRL to enable extended select
			if (m_calendar.SelectionMode==mcSelectionMode.MultiExtended)
				m_calendar.ExtendedKey = true; 
			
			for (int i = from;i<=to;i++) {
				if (m_days[i].State==mcDayState.Selected) {
					Remove(i);
					dates = AddDate(m_days[i].Date.ToShortDateString(),dates); 
				}
			}
						
			// raise dayselected event
			if ((this.DayDeselected!=null) && (dates.Length>0))
				this.DayDeselected(this,new DaySelectedEventArgs(dates));
			
			m_calendar.ExtendedKey = false;
		}
		
		internal void DeselectArea(int topLeft, int bottomRight) {
			ArrayList days;
			string[] dates = new string[0];
			int index;

			days = DaysInArea(topLeft,bottomRight);

			// if MultiExtended , press CTRL to enable extended select
			if (m_calendar.SelectionMode==mcSelectionMode.MultiExtended)
				m_calendar.ExtendedKey = true; 
			
			for (int i = 0;i<days.Count;i++) {
				index = (int)days[i];
				if (m_days[index].State==mcDayState.Selected) {
					Remove(index);
					dates = AddDate(m_days[index].Date.ToShortDateString(),dates); 
				}
			}
						
			// raise dayselected event
			if ((this.DayDeselected!=null) && (dates.Length>0))
				this.DayDeselected(this,new DaySelectedEventArgs(dates));
			
			m_calendar.ExtendedKey = false;
		}
			
		internal void SelectArea(int topLeft, int bottomRight) {
			// if MultiExtended , press CTRL to enable extended select
			if (m_calendar.SelectionMode==mcSelectionMode.MultiExtended)
				m_calendar.ExtendedKey = true; 
				
			NewSelectedArea(topLeft,bottomRight);
			
			// raise dayselected event
			if (this.DaySelected!=null)
				this.DaySelected(this,new DaySelectedEventArgs(DaysInSelection(NO_AREA)));
			
			m_calendar.ExtendedKey = false;
		}
		
		internal void SelectRange(int from, int to) {
			string[] selBefore;
			string[] selAfter;
			
			// if MultiExtended , press CTRL to enable extended select
			if (m_calendar.SelectionMode==mcSelectionMode.MultiExtended)
				m_calendar.ExtendedKey = true; 
		
			selBefore = DaysInSelection(NO_AREA);
			NewSelectedRange(from,to);
			selAfter = DaysInSelection(NO_AREA);
			
			if (selAfter.Length>selBefore.Length) {
				// raise dayselected event
				if (this.DaySelected!=null)
					this.DaySelected(this,new DaySelectedEventArgs(selAfter));
			}

			// Release CTRL key
			m_calendar.ExtendedKey = false; 
		}
		
		internal void DoubleClick(Point mouseLocation, MouseButtons button) {
			Region monthRgn = new Region(m_rect);

			if (monthRgn.IsVisible(mouseLocation)) {
				for (int i = 0;i<42;i++) {
					if (m_days[i].HitTest(mouseLocation)) {
						if (this.DayDoubleClick!=null)
							this.DayDoubleClick(this,new DayClickEventArgs(m_days[i].Date.ToShortDateString() ,button));
					}
				}
			}
		}
		
		internal void MouseUp() {
			m_mouseDown = false;
			string[] days;
			days = DaysInSelection(NO_AREA);
			if ((days.Length>0) && (m_selected)) {
				Array.Sort(days);
				if (this.DaySelected!=null)
					this.DaySelected(this,new DaySelectedEventArgs(days));
				m_selected = false;
			}
		}

		internal void Click(Point mouseLocation,MouseButtons button) {
			bool dayEnabled = true;
			
			if (m_region.IsVisible(mouseLocation) && (m_calendar.SelectionMode>mcSelectionMode.None) ) {
				for (int i = 0;i<42;i++) {
					if (m_days[i].HitTest(mouseLocation)) {
						m_dayInFocus = i;
						// Check if proper button is used
						if ((button.ToString() == m_calendar.SelectButton.ToString()) || 
							(m_calendar.SelectButton == mcSelectButton.Any)) {
							dayEnabled = IsDateEnabled(m_days[i].Date);
 
							if ( ((m_calendar.SelectTrailingDates) || (SelectedMonth.Month  == m_days[i].Date.Month)) &&
								((m_calendar.MinDate<=m_days[i].Date) && (m_calendar.MaxDate >= m_days[i].Date)) && (dayEnabled) ) {
								// If day is already selected and number of days in selection = 1
								// or selectionMode = MultiExtended, toggle to focus
								if (m_days[i].State == mcDayState.Selected) {
									if (SelectionDayCount(m_days[i].SelectionArea) ==1) {
										RemoveSelection(true,m_days[i].SelectionArea);
										m_days[i].State = mcDayState.Focus;
									}
									else if ((m_calendar.SelectionMode == mcSelectionMode.MultiExtended)) {
										if (m_calendar.ExtendedKey) {
											RemoveDay(i,true);
											m_days[i].State = mcDayState.Focus;
										}
										else {
											NewSelectedDay(i);
										}
									}
									else {
										NewSelectedDay(i);
									}
									
								}
								else {
									NewSelectedDay(i);
								}
									
								m_mouseDown = true;
								m_calendar.Invalidate();
							}
						}
						//either way ceate DayClick event
						if (this.DayClick!=null)
							this.DayClick(this,new DayClickEventArgs(m_days[i].Date.ToShortDateString(),button));
						break;
					}
				}
			}
		}

		private string[] AddDate(string dt, string[] old) {
			int l =  old.Length;
			int i;
			bool exist = false;
			string[] n = new string[l+1];
			n.Initialize(); 
			for (i = 0;i<l;i++) {
				n[i] = old[i];
				if (old[i]==dt) {
					exist = true;
					break;
				}
			}
			n[i] = dt;
			if (!exist)
				// if already selected return new array
				return n;
			else
				// else return old
				return old;
		}

		internal void MouseMove (Point mouseLocation) {
//						bool dayEnabled = true;
//			
//						// is mouse pointer inside month region
//						if (m_region.IsVisible(mouseLocation))
//						{
//							m_calendar.ActiveRegion = mcCalendarRegion.Month;  
//							// Check which day has focus
//							for (int i = 0;i<42;i++)
//							{
//								if (m_days[i].HitTest(mouseLocation))
//								{
//									dayEnabled = IsDateEnabled(m_days[i].Date);
//			 						
//									// check if its a new day
//									if (m_dayInFocus!=i)
//									{
//										if (!m_mouseDown) 
//										{
//											if ( ((m_calendar.SelectTrailingDates) || (SelectedMonth.Month  == m_days[i].Date.Month)) && (dayEnabled))
//												
//											{
//												if (m_days[i].State != mcDayState.Selected)
//													m_days[i].State = mcDayState.Focus;
//												if ((m_dayInFocus!= -1) && (m_days[m_dayInFocus].State != mcDayState.Selected)) 
//													m_days[m_dayInFocus].State = mcDayState.Normal;
//										
//												// raise events
//												if ((DayLostFocus!=null) && (m_dayInFocus!=-1))
//													DayLostFocus(this,new DayEventArgs(m_days[m_dayInFocus].Date.ToShortDateString()));	
//												if (DayGotFocus!=null)
//													DayGotFocus(this,new DayEventArgs(m_days[i].Date.ToShortDateString()));	
//											}
//											else
//											{
//												if ((m_dayInFocus!= -1) && (m_days[m_dayInFocus].State != mcDayState.Selected)) 
//													m_days[m_dayInFocus].State = mcDayState.Normal;
//											}
//											
//											if (m_calendar.ShowFocus)
//												m_calendar.Invalidate(m_rect);
//											m_dayInFocus = i;
//										}
//										else if (m_calendar.SelectionMode >= mcSelectionMode.MultiSimple) 
//										{
//											m_dayInFocus = i;
//											if ( ((m_calendar.SelectTrailingDates) || (SelectedMonth.Month  == m_days[i].Date.Month)) &&
//												((m_calendar.MinDate<=m_days[i].Date) && (m_calendar.MaxDate >= m_days[i].Date)) && (dayEnabled) )      
//											{
//																		
//												if (m_newSelection)
//												{
//													NewSelectedDay(i);
//													m_newSelection = false;
//												}
//												else
//												{
//													SelectionArea area = (SelectionArea)m_selArea[m_selArea.Count-1];
//													area.End = i; 
//												}
//												
//												m_selected = true;
//												RemoveSelection(false);									
//												// loop through number of selections
//												for (int y = 0;y<m_selArea.Count;y++) 
//												{
//													SelectionArea area = (SelectionArea)m_selArea[y];
//													if ((area.Begin!=-1) && (area.End!=-1)) 
//														MarkAreaAsSelected(area.Begin,area.End,y);	
//												}
//												// Force repaint of calendar
//												m_calendar.Invalidate(m_rect); 
//												
//											}
//										}
//			
//									}
//									break;
//								}
//							}
//						}
//						else
//						{
//							RemoveFocus();
//						}
			
		}
			
		internal string DateInFocus() {
			return m_days[m_dayInFocus].Date.ToShortDateString(); 
		}

		internal int GetDay(Point mouseLocation) {
			int day = -1;
			for (int i = 0;i<42;i++)
				if (m_days[i].HitTest(mouseLocation))
					day = i;
			return day;
		}

		internal void RemoveFocus() {
			
			if ((DayLostFocus!=null) && (m_dayInFocus!=-1))
				DayLostFocus(this,new DayEventArgs(m_days[m_dayInFocus].Date.ToShortDateString()));	
			
			m_dayInFocus = -1;
			for (int i = 0;i<42;i++)
				if (m_days[i].State != mcDayState.Selected)
					m_days[i].State = mcDayState.Normal; 
		}
		
		private ArrayList DaysInArea(int topLeft,int bottomRight) {
			ArrayList days = new ArrayList();
		
			// Get Coordinates for selection rectangle
			m_selRight = System.Math.Max(m_days[bottomRight].Rectangle.Right,m_days[topLeft].Rectangle.Right); 
			m_selLeft = System.Math.Min(m_days[bottomRight].Rectangle.Left,m_days[topLeft].Rectangle.Left);
			m_selTop = System.Math.Min(m_days[bottomRight].Rectangle.Top,m_days[topLeft].Rectangle.Top); 
			m_selBottom = System.Math.Max(m_days[bottomRight].Rectangle.Bottom,m_days[topLeft].Rectangle.Bottom); 	
				
			for (int t = 0;t<42;t++) {
				if ((m_days[t].Rectangle.Left >= m_selLeft) &&
					(m_days[t].Rectangle.Right <= m_selRight) &&
					(m_days[t].Rectangle.Top >= m_selTop) &&
					(m_days[t].Rectangle.Bottom <= m_selBottom)) {
					days.Add(t);
				}
			}  
			return days;
		}

		private void MarkAreaAsSelected(int topLeft,int bottomRight, int area) {
			ArrayList days;
			int index = 0;
			
			SelectionArea a = (SelectionArea)m_selArea[area];

			a.Begin = topLeft;
			a.End = bottomRight;
			
			days = DaysInArea(topLeft,bottomRight);
			for (int i = 0;i<days.Count;i++) {
				index = (int)days[i];
				if ( (m_calendar.SelectTrailingDates) || (SelectedMonth.Month  == m_days[index].Date.Month) &&
					(m_days[index].State != mcDayState.Selected) ) {
					m_days[index].State = mcDayState.Selected;
					m_days[index].SelectionArea = area; 
				}
			}
		}

		internal void RemoveSelection(bool raiseEvent, int sel) {
			string[] days;
			
			// Get selected days
			days = DaysInSelection(sel); 
					
			for (int i = 0;i<42;i++) {
				// Reset all days or days within a selection to "Normal"
				if ( (m_days[i].SelectionArea == sel) || (sel == NO_AREA) ) {
					m_days[i].State = mcDayState.Normal;
					m_days[i].SelectionArea = -1;
				}
			}

			// if a selection is specified , "reset" its start and stop day
			if ((sel!=NO_AREA)) {
				SelectionArea area = (SelectionArea)m_selArea[sel];
				area.Begin = -1;
				area.End = -1;
				// Make sure moving the mouse creates a new selection
				m_newSelection = true;
			}
							
			//raise deselect event
			if (raiseEvent) {
				if (days.Length !=0) {
					Array.Sort(days);
					if (this.DayDeselected!=null)
						this.DayDeselected(this,new DaySelectedEventArgs(days));
				}
				
				// reset arrays and index
				if (sel==NO_AREA)
					m_selArea.Clear(); 
				
			}
		}
		
		internal void RemoveSelection(bool raiseEvent) {
			RemoveSelection(raiseEvent,NO_AREA);	
		}

		internal void Setup() {
			int startPos=0;
			DateTime currentDate;
			string[] weekdays;
			string lblDay;
			int i = 0;

			weekdays = m_calendar.Weekdays.GetWeekDays();  
			   
			if (m_calendar.Weekdays.Format == mcDayFormat.Short)   
				lblDay = m_calendar.m_dateTimeFormat.GetAbbreviatedDayName(m_selectedMonth.DayOfWeek);
			else
				lblDay = m_calendar.m_dateTimeFormat.GetDayName(m_selectedMonth.DayOfWeek);
		
			for (i = 0;i<weekdays.Length;i++) {
				if (weekdays[i] == lblDay)
					break;
   
			}
			startPos = i;
			if (startPos == 0) startPos = 7;

			currentDate = m_selectedMonth;
			for (i = startPos;i<42;i++) {
				m_days[i].Date = currentDate;
				currentDate = currentDate.AddDays(1); 
			}
			currentDate = m_selectedMonth;
			for (i= startPos;i>=0;i--) {
				m_days[i].Date = currentDate;
				currentDate = currentDate.AddDays(-1); 
			}
		}

		internal bool IsVisible(Rectangle clip) {
			return m_region.IsVisible(clip); 	
		}
		
		
		internal void Draw(Graphics e) {
			int today = -1;
			string[] selectedDays;
			Rectangle todayRect;

			Brush bgBrush = new SolidBrush(Colors.Background);    
			Brush selBrush = new SolidBrush(Color.FromArgb(125,Colors.SelectedBackground));   
			Brush focusBrush = new SolidBrush(Color.FromArgb(125,Colors.FocusBackground));
			Pen todayPen = new Pen(Color.FromArgb(150,Calendar.TodayColor),2);
			
			try {
				if (BackgroundImage != null)
					e.DrawImage(BackgroundImage, Rect);  
				else
					e.FillRectangle(bgBrush,m_rect);
				// Draw days
				for (int i = 0;i<42;i++) {
					// Create new graphics object
					Graphics d = m_calendar.CreateGraphics();
					// Create bitmap..
					Bitmap bmp = new Bitmap(m_days[i].Rectangle.Width,m_days[i].Rectangle.Height,d);
					// link graphics object to bitmap
					d = Graphics.FromImage(bmp);
					DayRenderEventArgs args = new DayRenderEventArgs(d,m_days[i].Rectangle,m_days[i].Date, m_days[i].State);  
					DayRender(this,args); 
					if (!args.OwnerDraw)
						// day is not user drawn
						m_days[i].Draw(e);
					else
						// Draw user rendered day
						e.DrawImage(bmp,m_days[i].Rectangle);
				
					// Check if day has focus and if focus should be drawn
					if ((m_days[i].State == mcDayState.Focus) && (m_calendar.ShowFocus)) {
						e.FillRectangle(focusBrush,m_days[i].Rectangle);
						ControlPaint.DrawBorder(e,m_days[i].Rectangle,Colors.FocusBorder,BorderStyles.Focus);
					}
	
					if ((m_days[i].Date == DateTime.Today) && (!args.OwnerDraw))
						today = i;
								
					d.Dispose();
					bmp.Dispose(); 
				}
			
				// check if date is "today" and if it should be marked..
				if ( (m_calendar.ShowToday) && (today !=-1) && 
					((m_calendar.ShowTrailingDates) || (m_days[today].Date.Month == m_calendar.ActiveMonth.Month)) ) {
					SizeF radius = e.MeasureString(m_days[today].Date.Day.ToString(),DateFont);
					todayRect = m_days[today].Rectangle;
					// m_days[today].Rectangle.Location; 
					switch (DateAlign) {
						case mcItemAlign.LeftCenter: {
							todayRect.X = todayRect.Left - 3;
							todayRect.Y = todayRect.Y + ((todayRect.Height / 2) -((int)radius.Height/2) - 3);
							break;
						}
						case mcItemAlign.RightCenter: {	
							todayRect.X = m_days[today].Rectangle.Right - (int)radius.Width - 3;
							todayRect.Y = todayRect.Y + ((todayRect.Height / 2) -((int)radius.Height/2) - 3);
							break;
						}
						case mcItemAlign.TopCenter: {
							todayRect.Y = m_days[today].Rectangle.Top - 3;
							todayRect.X = todayRect.X + ((todayRect.Width / 2) -((int)radius.Width/2) - 3);
							break;
						}
						case mcItemAlign.BottomCenter: {
							todayRect.Y = todayRect.Bottom - (int)radius.Height - 3;
							todayRect.X = todayRect.X + ((todayRect.Width / 2) -((int)radius.Width/2) - 3);
							break;
						}

						case mcItemAlign.TopLeft: {
							todayRect.X = todayRect.Left - 3;
							todayRect.Y = todayRect.Top - 3;
							break;
						}
						case mcItemAlign.TopRight: {
							todayRect.X = m_days[today].Rectangle.Right - (int)radius.Width - 3;
							todayRect.Y = m_days[today].Rectangle.Top - 3;
							break;
						}
						case mcItemAlign.BottomLeft: {
							todayRect.X = todayRect.Left - 3;
							todayRect.Y = todayRect.Bottom - (int)radius.Height - 3;
							break;
						}
						case mcItemAlign.BottomRight: {
							todayRect.X = todayRect.Right - (int)radius.Width - 3;
							todayRect.Y = todayRect.Bottom - (int)radius.Height - 3;
							break;
						}
						case mcItemAlign.Center: {
							todayRect.X = todayRect.X + ((todayRect.Width / 2) -((int)radius.Width/2) - 3);
							todayRect.Y = todayRect.Y + ((todayRect.Height / 2) -((int)radius.Height/2) - 3);
							break;
						}
					}

					todayRect.Width = (int)radius.Width +6;
					todayRect.Height = (int)radius.Height +6;
						
					if (todayRect.Width > m_days[today].Rectangle.Width)
						todayRect.Width = m_days[today].Rectangle.Width;
					if (todayRect.Height > m_days[today].Rectangle.Height)
						todayRect.Height = m_days[today].Rectangle.Height;
					
					if (todayRect.Left < (m_days[today].Rectangle.Left-3))
						todayRect.X = m_days[today].Rectangle.Left-3;
					if (todayRect.Top < (m_days[today].Rectangle.Top-3))
						todayRect.Y = m_days[today].Rectangle.Top-3;
					
					e.DrawEllipse(todayPen,todayRect); 
				}

				// Check if a selection exist
				selectedDays = DaysInSelection(NO_AREA);
				if (selectedDays.Length>0) {
					// Check how many selection areas there are
					if (m_selArea.Count<=1) {
						for (int i = 0;i<m_selArea.Count;i++)	{
							SelectionArea area = (SelectionArea)m_selArea[i];
							if ((area.Begin!=-1) && (area.End !=-1)) {
								// Get Coordinates for selection rectangle
								m_selRight = System.Math.Max(m_days[area.End].Rectangle.Right,m_days[area.Begin].Rectangle.Right); 
								m_selLeft = System.Math.Min(m_days[area.End].Rectangle.Left,m_days[area.Begin].Rectangle.Left);
								m_selTop = System.Math.Min(m_days[area.End].Rectangle.Top,m_days[area.Begin].Rectangle.Top); 
								m_selBottom = System.Math.Max(m_days[area.End].Rectangle.Bottom,m_days[area.Begin].Rectangle.Bottom); 	
				
								// Draw selection
								Rectangle selRect = new Rectangle(m_selLeft,m_selTop,m_selRight-m_selLeft,m_selBottom-m_selTop);
								e.FillRectangle(selBrush,selRect); 
								ControlPaint.DrawBorder(e,selRect,Colors.SelectedBorder,BorderStyles.Selected);  	
							}
						}
					}
						// Multiple selection areas, we dont use border so we 
						// draw each day individually to not overlap regions
					else	{
						for (int i =0;i<42;i++) {
							if ((m_days[i].State==mcDayState.Selected) && (m_days[i].SelectionArea!=-1)) {
								e.FillRectangle(selBrush,m_days[i].Rectangle);
							}
						}
					}
				}
			}
			catch (Exception)	{	}
		
			bgBrush.Dispose();
			selBrush.Dispose();
			todayPen.Dispose();
			focusBrush.Dispose();
		}

		internal void SetupDays() {
			int row = 0;
			int col = 0;
			int index;
			
			Rectangle dayRect = new Rectangle(); 
			
			m_dayHeight = (float)((m_rect.Height - (m_padding.Vertical*7))  / 6);
			m_dayWidth =  (float)((m_rect.Width - (m_padding.Horizontal*8)) / 7);
			
			// setup rectangles for days
			row = 0;
			index = 0;
						
			for (int i = 0;i<6;i++) {  // rows
				col = 0;
				for (int j = 0;j<7;j++) {  // colums
					dayRect.X = (int)(m_dayWidth * col)+(col+1)*m_padding.Horizontal+ m_rect.Left;
					dayRect.Y = (int)(m_dayHeight *row)+(row+1)*m_padding.Vertical + m_rect.Top;
					if (j ==6)
						dayRect.Width = m_rect.Width - (int)(m_padding.Horizontal*8) - (int)(m_dayWidth*6)-1;
					else
						dayRect.Width = (int)m_dayWidth;
					if ( i==5)
						dayRect.Height = m_rect.Height - (int)(m_padding.Vertical*7) - (int)(m_dayHeight*5)-1;
					else
						dayRect.Height = (int)m_dayHeight;
									
					m_days[index].Rectangle = dayRect;
					index++;
					col++;
				}
				row++;
			}
		}

		#endregion

		#region  MonthColors

		[TypeConverter(typeof(ColorsTypeConverter))]
			public class MonthColors {
			private Color m_selected;
			private Color m_selectedBorder;
			private Color m_selectedText;
			private Color m_selectedDate;
			private Color m_focus;
			private Color m_focusBorder;
			private Color m_focusText;
			private Color m_focusDate;
			private Color m_text;
			private Color m_date;
			private Color m_normalBg;
			private Color m_border;
			private Color m_trailingText;
			private Color m_trailingDate;
			private Color m_trailing;
			private Color m_weekendBg;
			private Color m_weekendDate;
			private Color m_weekendText;
			private Color m_disabledBg;
			private Color m_disabledText;
			private Color m_disabledDate;

			private Month m_month;
			
			public MonthColors(Month month) {
				m_month = month;
				// Default values
				m_selected = Color.FromArgb(193,210,238);
				m_selectedBorder = Color.FromArgb(49,106,197); 
				m_selectedText = Color.Black;
				m_selectedDate = Color.Black;
				m_focus = Color.FromArgb(224,232,246); 
				m_focusBorder = Color.FromArgb(152,180,226); 
				m_focusText = Color.Black;
				m_focusDate = Color.Black;
				m_normalBg = Color.White;
				m_trailing = Color.White;
				m_border = Color.Black;
				m_text = Color.Black;
				m_date = Color.Black;
				m_trailingText = Color.LightGray; 
				m_trailingDate = Color.LightGray;
				m_weekendBg = Color.White;
				m_weekendDate = Color.Black;
				m_weekendText = Color.Black;
				m_disabledBg = Color.FromArgb(233, 233, 233);  
				m_disabledText = Color.LightGray;
				m_disabledDate = Color.LightGray;
			}
					
			[Description("Color used for disabled background.")]
			[DefaultValue(typeof(Color),"233, 233, 233")]
			public Color DisabledBackground {
				get {
					return m_disabledBg;
				}
				set {
					if (m_disabledBg != value) {
						m_disabledBg = value;
						if (m_month.ColorChanged!=null)
							m_month.ColorChanged(this,new MonthColorEventArgs(mcMonthColor.DisabledBackColor));  
						m_month.m_calendar.Invalidate();
					}
				}
			}

			[Description("Color used for disabled text.")]
			[DefaultValue(typeof(Color),"LightGray")]
			public Color DisabledText {
				get {
					return m_disabledText;
				}
				set {
					if (m_disabledText != value) {
						m_disabledText = value;
						if (m_month.ColorChanged!=null)
							m_month.ColorChanged(this,new MonthColorEventArgs(mcMonthColor.DisabledText));  
						m_month.m_calendar.Invalidate();
					}
				}
			}

			[Description("Color used for disabled date.")]
			[DefaultValue(typeof(Color),"LightGray")]
			public Color DisabledDate {
				get {
					return m_disabledDate;
				}
				set {
					if (m_disabledDate != value) {
						m_disabledDate = value;
						if (m_month.ColorChanged!=null)
							m_month.ColorChanged(this,new MonthColorEventArgs(mcMonthColor.DisabledDate));  
						m_month.m_calendar.Invalidate();
					}
				}
			}

			[Description("Color used as weekend background.")]
			[DefaultValue(typeof(Color),"White")]
			public Color WeekendBackground {
				get {
					return m_weekendBg;
				}
				set {
					if (m_weekendBg != value) {
						m_weekendBg = value;
						if (m_month.ColorChanged!=null)
							m_month.ColorChanged(this,new MonthColorEventArgs(mcMonthColor.WeekendBackColor));  
						m_month.m_calendar.Invalidate();
					}
				}
			}

			[Description("Color used as weekend background.")]
			[DefaultValue(typeof(Color),"Black")]
			public Color WeekendText {
				get {
					return m_weekendText;
				}
				set {
					if (m_weekendText != value) {
						m_weekendText = value;
						if (m_month.ColorChanged!=null)
							m_month.ColorChanged(this,new MonthColorEventArgs(mcMonthColor.WeekendText));  
						m_month.m_calendar.Invalidate();
					}
				}
			}

			[Description("Color used as weekend date.")]
			[DefaultValue(typeof(Color),"Black")]
			public Color WeekendDate {
				get {
					return m_weekendDate;
				}
				set {
					if (m_weekendDate != value) {
						m_weekendDate = value;
						if (m_month.ColorChanged!=null)
							m_month.ColorChanged(this,new MonthColorEventArgs(mcMonthColor.WeekendDate));  
						m_month.m_calendar.Invalidate();
					}
				}
			}

			[Description("Backgrund color for trailing dates.")]
			[DefaultValue(typeof(Color),"White")]
			public Color TrailingBackground {
				get {
					return m_trailing;
				}
				set {
					if (m_trailing != value) {
						m_trailing = value;
						if (m_month.ColorChanged!=null)
							m_month.ColorChanged(this,new MonthColorEventArgs(mcMonthColor.TrailingBackColor));  
						m_month.m_calendar.Invalidate();
					}
				}
			}

			[Description("Color used for text.")]
			[DefaultValue(typeof(Color),"Black")]
			public Color Text {
				get {
					return m_text;
				}
				set {
					if (m_text != value) {
						m_text = value;
						if (m_month.ColorChanged!=null)
							m_month.ColorChanged(this,new MonthColorEventArgs(mcMonthColor.Text));  
						m_month.m_calendar.Invalidate();
					}
				}
			}

			[Description("Color used for date.")]
			[DefaultValue(typeof(Color),"Black")]
			public Color Date {
				get {
					return m_date;
				}
				set {
					if (m_date != value) {
						m_date = value;
						if (m_month.ColorChanged!=null)
							m_month.ColorChanged(this,new MonthColorEventArgs(mcMonthColor.Date));  
						m_month.m_calendar.Invalidate();
					}
				}
			}

			[Description("Color used for trailing text.")]
			[DefaultValue(typeof(Color),"LightGray")]
			public Color TrailingText {
				get {
					return m_trailingText;
				}
				set {
					if (m_trailingText!=value) {
						m_trailingText = value;
						if (m_month.ColorChanged!=null)
							m_month.ColorChanged(this,new MonthColorEventArgs(mcMonthColor.TrailingText));  
						m_month.m_calendar.Invalidate();
					}
				}
			}

			[Description("Color used for trailing date.")]
			[DefaultValue(typeof(Color),"LightGray")]
			public Color TrailingDate {
				get {
					return m_trailingDate;
				}
				set {
					if (m_trailingDate!=value) {
						m_trailingDate = value;
						if (m_month.ColorChanged!=null)
							m_month.ColorChanged(this,new MonthColorEventArgs(mcMonthColor.TrailingDate));  
						m_month.m_calendar.Invalidate();
					}
				}
			}

			[Description("Background color when day is not selected or has focus.")]
			[DefaultValue(typeof(Color),"White")]
			public Color Background {
				get {
					return m_normalBg;
				}
				set {
					if (m_normalBg!=value) {
						m_normalBg = value;
						if (m_month.ColorChanged!=null)
							m_month.ColorChanged(this,new MonthColorEventArgs(mcMonthColor.Background));  
						m_month.m_calendar.Invalidate();
					}
				}
			}

			[Description("Border color when day is not selected or has focus.")]
			[DefaultValue(typeof(Color),"Black")]
			public Color Border {
				get {
					return m_border;
				}
				set {
					if (m_border!=value) {
						m_border = value;
						if (m_month.ColorChanged!=null)
							m_month.ColorChanged(this,new MonthColorEventArgs(mcMonthColor.Border));  
						m_month.m_calendar.Invalidate();
					}
				}
			}
			
	
			[Description("Background color when day is selected.")]
			[DefaultValue(typeof(Color),"193,210,238")]
			public Color SelectedBackground {
				get {
					return m_selected;
				}
				set {
					if (m_selected!=value) {
						m_selected = value;
						if (m_month.ColorChanged!=null)
							m_month.ColorChanged(this,new MonthColorEventArgs(mcMonthColor.Selected));  
						m_month.m_calendar.Invalidate();
					}
					
				}
			}
		
			[Description("Border color when day is selected.")]
			[DefaultValue(typeof(Color),"49,106,197")]
			public Color SelectedBorder {
				get {
					return m_selectedBorder;
				}
				set {
					if (m_selectedBorder!=value) {
						m_selectedBorder = value;
						if (m_month.ColorChanged!=null)
							m_month.ColorChanged(this,new MonthColorEventArgs(mcMonthColor.SelectedBorder));  
						m_month.m_calendar.Invalidate();
					}
				}
			}

			[Description("Text color when day is selected.")]
			[DefaultValue(typeof(Color),"Color.Black")]
			public Color SelectedText {
				get {
					return m_selectedText;
				}
				set {
					if (m_selectedText!=value) {
						m_selectedText = value;
						if (m_month.ColorChanged!=null)
							m_month.ColorChanged(this,new MonthColorEventArgs(mcMonthColor.SelectedText));  
						m_month.m_calendar.Invalidate();
					}
				}
			}

			[Description("Date color when day is selected.")]
			[DefaultValue(typeof(Color),"Color.Black")]
			public Color SelectedDate {
				get {
					return m_selectedDate;
				}
				set {
					if (m_selectedDate!=value) {
						m_selectedDate = value;
						if (m_month.ColorChanged!=null)
							m_month.ColorChanged(this,new MonthColorEventArgs(mcMonthColor.SelectedDate));  
						m_month.m_calendar.Invalidate();
					}
				}
			}
		
			[Description("Background color when day has focus.")]
			[DefaultValue(typeof(Color),"224,232,246")]
			public Color FocusBackground {
				get {
					return m_focus;
				}
				set {
					if (m_focus!=value) {
						m_focus = value;
						if (m_month.ColorChanged!=null)
							m_month.ColorChanged(this,new MonthColorEventArgs(mcMonthColor.Focus));  
						m_month.m_calendar.Invalidate();
					}
				}
			}
		
			[Description("Border color when day has focus.")]
			[DefaultValue(typeof(Color),"152,180,226")]
			public Color FocusBorder {
				get {
					return m_focusBorder;
				}
				set {
					if (m_focusBorder!=value) {
						m_focusBorder = value;
						if (m_month.ColorChanged!=null)
							m_month.ColorChanged(this,new MonthColorEventArgs(mcMonthColor.FocusBorder));  
						m_month.m_calendar.Invalidate();
					}
				}
			}

			[Description("Text color when day has focus.")]
			[DefaultValue(typeof(Color),"Color.Black")]
			public Color FocusText {
				get {
					return m_focusText;
				}
				set {
					if (m_focusText!=value) {
						m_focusText = value;
						if (m_month.ColorChanged!=null)
							m_month.ColorChanged(this,new MonthColorEventArgs(mcMonthColor.FocusText));  
						m_month.m_calendar.Invalidate();
					}
				}
			}

			[Description("Date color when day has focus.")]
			[DefaultValue(typeof(Color),"Color.Black")]
			public Color FocusDate {
				get {
					return m_focusDate;
				}
				set {
					if (m_focusDate!=value) {
						m_focusDate = value;
						if (m_month.ColorChanged!=null)
							m_month.ColorChanged(this,new MonthColorEventArgs(mcMonthColor.FocusDate));  
						m_month.m_calendar.Invalidate();
					}
				}
			}
		
		}
		
		#endregion

		#region  MonthBorderStyles
	

		[TypeConverter(typeof(BorderStylesTypeConverter))]
			public class MonthBorderStyles {
			private Month m_month;
			
			private ButtonBorderStyle m_borderStyle;
			private ButtonBorderStyle m_focusBorderStyle;
			private ButtonBorderStyle m_selectedBorderStyle;

			public MonthBorderStyles(Month month) {
				m_month = month;
				m_borderStyle = ButtonBorderStyle.None;
				m_focusBorderStyle = ButtonBorderStyle.Solid;
				m_selectedBorderStyle = ButtonBorderStyle.Solid;
			}
				
			[Description("Border style when item has no focus.")]
			[DefaultValue(typeof(ButtonBorderStyle),"None")]
			public ButtonBorderStyle Normal {
				get {
					return m_borderStyle;
				}
				set {
					if (m_borderStyle!=value) {
						m_borderStyle = value;
						if (m_month.BorderStyleChanged!=null)
							m_month.BorderStyleChanged(this,new MonthBorderStyleEventArgs(mcMonthBorderStyle.Normal));  
						m_month.m_calendar.Invalidate();
					}
				}
				
			}
			
			[Description("Border style when item has focus.")]
			[DefaultValue(typeof(ButtonBorderStyle),"Solid")]
			public ButtonBorderStyle Focus {
				get {
					return m_focusBorderStyle;
				}
				set {
					if (m_focusBorderStyle!=value) {
						m_focusBorderStyle = value;
						if (m_month.BorderStyleChanged!=null)
							m_month.BorderStyleChanged(this,new MonthBorderStyleEventArgs(mcMonthBorderStyle.Focus));  
						m_month.m_calendar.Invalidate();
					}
				}
				
			}
			
			[Description("Border style when item is selected.")]
			[DefaultValue(typeof(ButtonBorderStyle),"Solid")]
			public ButtonBorderStyle Selected {
				get {
					return m_selectedBorderStyle;
				}
				set {
					if (m_selectedBorderStyle!=value) {
						m_selectedBorderStyle = value;
						if (m_month.BorderStyleChanged!=null)
							m_month.BorderStyleChanged(this,new MonthBorderStyleEventArgs(mcMonthBorderStyle.Selected));  
						m_month.m_calendar.Invalidate(); 
					}
				}
	
			}
		
		}
		

		#endregion

		#region  MonthPadding

		[TypeConverter(typeof(MonthPaddingTypeConverter))]		
			public class MonthPadding {
			private Month m_month;
			private int m_horizontal;
			private int m_vertical;
			
			public MonthPadding(Month month) {
				// set the control to which the collection belong
				m_month = month;
				// Default values
				m_horizontal = 2;
				m_vertical = 2;
			}
			
			[RefreshProperties(System.ComponentModel.RefreshProperties.All)]
			[Description("Horizontal padding.")]
			[DefaultValue(2)]
			public int Horizontal {
				get {
					return m_horizontal;
				}
				set {
					if (m_horizontal!=value) {
						m_horizontal = value;
						if (m_month!=null) {
							// padding has changed , force DoLayout
							m_month.SetupDays();
							m_month.Calendar.Invalidate();  
							if (m_month.PropertyChanged!=null)
								m_month.PropertyChanged(this,new MonthPropertyEventArgs(mcMonthProperty.Padding));  
				
						}
					}
				}
			}
			
			[RefreshProperties(System.ComponentModel.RefreshProperties.All)]
			[Description("Vertical padding.")]
			[DefaultValue(2)]
			public int Vertical {
				get {	
					return m_vertical;
				}
				set {
					if (m_vertical!=value) {
						m_vertical = value;
						if (m_month!=null) {						
							m_month.SetupDays();
							m_month.Calendar.Invalidate();  
							if (m_month.PropertyChanged!=null)
								m_month.PropertyChanged(this,new MonthPropertyEventArgs(mcMonthProperty.Padding));  
						}
					}
				}
			}

		}
		
		#endregion


		#region  TransparencyCollection

		[TypeConverter(typeof(TransparencyTypeConverter))]
			public class TransparencyCollection {
			private Month m_month;
			private int m_background;
			private int m_text;

			public TransparencyCollection(Month month) {
				// set the control to which the collection belong
				m_month = month;
				// Default values
				m_background = 175;
				m_text = 175;
			}

			[RefreshProperties(System.ComponentModel.RefreshProperties.All)]
			[Description("Transparency used for background.")]
			[DefaultValue(175)]
			public int Background {
				get {
					return m_background;
				}
				set {
					if (m_background != value) {
						m_background = value;
						if (m_month != null) {
							// padding has changed , force DoLayout
							m_month.SetupDays();
							m_month.Calendar.Invalidate();
							if (m_month.PropertyChanged != null)
								m_month.PropertyChanged(this, new MonthPropertyEventArgs(mcMonthProperty.Transparency));

						}
					}
				}
			}

			[RefreshProperties(System.ComponentModel.RefreshProperties.All)]
			[Description("Transparency used for text.")]
			[DefaultValue(175)]
			public int Text {
				get {
					return m_text;
				}
				set {
					if (m_text != value) {
						m_text = value;
						if (m_month != null) {
							m_month.SetupDays();
							m_month.Calendar.Invalidate();
							if (m_month.PropertyChanged != null)
								m_month.PropertyChanged(this, new MonthPropertyEventArgs(mcMonthProperty.Transparency));
						}
					}
				}
			}

		}

		#endregion
		
		#region MonthPaddingTypeConverter

		public class MonthPaddingTypeConverter : ExpandableObjectConverter {
			        	
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) {
				if(sourceType == typeof(string))
					return true;
				return base.CanConvertFrom (context, sourceType);
			}

			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) {
				if(destinationType == typeof(string))
					return true;
				return base.CanConvertTo (context, destinationType);
			}

			public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value) {
				
				if(value.GetType() == typeof(string)) {
					// Parse property string
					string[] ss = value.ToString().Split(new char[] {';'}, 2);
					if (ss.Length==2) {
						// Create new PaddingCollection
						MonthPadding item = new MonthPadding((Month)context.Instance); 
						// Set properties
						item.Horizontal = int.Parse(ss[0]);
						item.Vertical = int.Parse(ss[1]); 
						return item;				
					}
				}
				return base.ConvertFrom (context, culture, value);
			}

			public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType) {
									
				MonthPadding dest = value as Month.MonthPadding;
				if (destinationType == typeof(string) && (dest !=null) ) {
					// create property string
					return dest.Horizontal.ToString()+"; "+dest.Vertical.ToString();
				}
				return base.ConvertTo (context, culture, value, destinationType);
			}

		}


		#endregion

		#region TransparencyTypeConverter

		public class TransparencyTypeConverter : ExpandableObjectConverter {

			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) {
				if (sourceType == typeof(string))
					return true;
				return base.CanConvertFrom(context, sourceType);
			}

			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) {
				if (destinationType == typeof(string))
					return true;
				return base.CanConvertTo(context, destinationType);
			}

			public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value) {

				if (value.GetType() == typeof(string)) {
					// Parse property string
					string[] ss = value.ToString().Split(new char[] { ';' }, 2);
					if (ss.Length == 2) {
						// Create new PaddingCollection
						TransparencyCollection item = new TransparencyCollection((Month)context.Instance);
						// Set properties
						item.Background = int.Parse(ss[0]);
						item.Text = int.Parse(ss[1]);
                                                
						if (item.Text > 255)
							item.Text = 255;
						if (item.Text < 0)
							item.Text = 0;
						if (item.Background > 255)
							item.Background = 255;
						if (item.Background < 0)
							item.Background = 0;

						return item;
					}
				}
				return base.ConvertFrom(context, culture, value);
			}

			public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType) {

				TransparencyCollection dest = value as Month.TransparencyCollection;
				if (destinationType == typeof(string) && (dest != null)) {
					// create property string
					if (dest.Text > 255)
						dest.Text = 255;
					if (dest.Text < 0)
						dest.Text = 0;
					if (dest.Background > 255)
						dest.Background = 255;
					if (dest.Background < 0)
						dest.Background = 0;

					return dest.Background.ToString() + "; " + dest.Text.ToString();
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}

		}


		#endregion
		
		
		#region ColorsTypeConverter
	
		public class ColorsTypeConverter : ExpandableObjectConverter {
			public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType) {
				return ""; 
			}

		}

		#endregion

		#region BorderStylesTypeConverter
	
		public class BorderStylesTypeConverter : ExpandableObjectConverter {
			public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType) {
				return ""; 
			}

		}

		#endregion
	}

	#region MonthPropertyEventArgs
	
	public class MonthPropertyEventArgs : EventArgs {
		#region Class Data

		/// <summary>
		/// The property that has changed
		/// </summary>
		private mcMonthProperty m_property;

		#endregion

		#region Constructor

		public MonthPropertyEventArgs() {
			m_property = 0;
		}

		public MonthPropertyEventArgs(mcMonthProperty property) {
			this.m_property = property;
		}

		#endregion


		#region Properties

		public mcMonthProperty Property {
			get {
				return this.m_property;
			}
		}

		#endregion
	}


	#endregion
	
	#region MonthColorEventArgs
	
	public class MonthColorEventArgs : EventArgs {
		#region Class Data

		/// <summary>
		/// The property that has changed
		/// </summary>
		private mcMonthColor m_color;

		#endregion

		#region Constructor

		public MonthColorEventArgs() {
			m_color = 0;
		}

		public MonthColorEventArgs(mcMonthColor color) {
			this.m_color = color;
		}

		#endregion


		#region Properties

		public mcMonthColor Color {
			get {
				return this.m_color;
			}
		}

		#endregion
	}


	#endregion
	
	#region MonthBorderStyleEventArgs
	
	public class MonthBorderStyleEventArgs : EventArgs {
		#region Class Data

		/// <summary>
		/// The property that has changed
		/// </summary>
		private mcMonthBorderStyle m_borderStyle;

		#endregion

		#region Constructor

		public MonthBorderStyleEventArgs() {
			m_borderStyle = 0;
		}

		public MonthBorderStyleEventArgs(mcMonthBorderStyle style) {
			this.m_borderStyle = style;
		}

		#endregion


		#region Properties

		public mcMonthBorderStyle BorderStyle {
			get {
				return this.m_borderStyle;
			}
		}

		#endregion
	}


	#endregion


}
