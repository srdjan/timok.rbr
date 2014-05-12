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
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;   

namespace Pabo.Calendar
{
	
	public enum mcWeeknumberProperty 
	{
		BorderColor = 0, BackColor, Font, TextColor}
	
	#region Delegates
	
	public delegate void WeeknumberPropertyEventHandler(object sender, WeeknumberPropertyEventArgs e);
	public delegate void WeeknumberClickEventHandler(object sender, WeeknumberClickEventArgs e);

	#endregion

	
	/// <summary>
	/// Summary description for WeekNumber.
	/// </summary>
	[TypeConverter(typeof(WeeknumberTypeConverter))]
	public class Weeknumber : IDisposable
	{
		#region private class members
		
		private bool disposed;
		private MonthCalendar m_calendar;
		private Color m_backColor;
		private Color m_textColor;
		private Color m_borderColor;
		private Font m_font;
		private Rectangle m_rect;
		private Region m_region;

		#endregion

		#region EventHandler

		internal event WeeknumberClickEventHandler Click;
		internal WeeknumberClickEventHandler DoubleClick;
		internal event WeeknumberPropertyEventHandler PropertyChanged;

		#endregion

		#region Constructor

		public Weeknumber(MonthCalendar calendar)
		{
			m_calendar = calendar;
			m_backColor = Color.White;
			m_textColor = Color.FromArgb(0,84,227); 
			m_borderColor = Color.Black;
			m_font = new Font("Microsoft Sans Serif",(float)8.25);
			
		}

		#endregion
		
		#region Dispose
		
		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					m_font.Dispose();
					m_region.Dispose();
				}
				// shared cleanup logic
				disposed = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);	
		}

		#endregion

		#region Properties
		
		[Description("Color used border.")]
		[DefaultValue(typeof(Color),"Black")]
		public Color BorderColor
		{
			get
			{
				return m_borderColor;
			}
			set
			{
				if (m_borderColor!=value)
				{
					m_borderColor = value;
					if (PropertyChanged!=null)
						PropertyChanged(this,new WeeknumberPropertyEventArgs(mcWeeknumberProperty.BorderColor)); 
					m_calendar.Invalidate();
				}
			}
		}

		internal Rectangle Rect
		{
			get
			{
				return m_rect;
			}
			set
			{
				m_rect = value;
				m_region = new Region(m_rect);
			}
		}

		[Description("Color used for background.")]
		[DefaultValue(typeof(Color),"White")]
		public Color BackColor
		{
			get
			{
				return m_backColor;
			}
			set
			{
				if (m_backColor!=value)
				{
					m_backColor = value;
					if (PropertyChanged!=null)
						PropertyChanged(this,new WeeknumberPropertyEventArgs(mcWeeknumberProperty.BackColor)); 
					m_calendar.Invalidate();
				}
			}
		}
		
		[Description("Font used for week numbers.")]
		[DefaultValue(typeof(Font),"Microsoft Sans Serif; 8,25pt")]
		public Font Font
		{
			get
			{
				return m_font;
			}
			set
			{
				if (m_font!=value)
				{
					m_font = value;
					m_calendar.DoLayout();
					if (PropertyChanged!=null)
						PropertyChanged(this,new WeeknumberPropertyEventArgs(mcWeeknumberProperty.Font)); 
					m_calendar.Invalidate();
				}
			}
		}
		
		[Description("Color used for text.")]
		[DefaultValue(typeof(Color),"0,84,227")]
		public Color TextColor
		{
			get
			{
				return m_textColor;
			}
			set
			{
				if (m_textColor!=value)
				{
					m_textColor = value;
					if (PropertyChanged!=null)
						PropertyChanged(this,new WeeknumberPropertyEventArgs(mcWeeknumberProperty.TextColor)); 
					m_calendar.Invalidate();
				}
			}
		}

		#endregion

		#region Methods
		
		internal void MouseMove(Point mouseLocation)
		{
			if (m_region.IsVisible(mouseLocation))
			{
				m_calendar.ActiveRegion = mcCalendarRegion.Weeknumbers;  
			}
		}

		internal void MouseClick(Point mouseLocation,MouseButtons button, mcClickMode mode)
		{
			GregorianCalendar gCalendar = new GregorianCalendar();
	
			if (m_region.IsVisible(mouseLocation))
			{
				int week = 0;
				
				int i = ((mouseLocation.Y-m_rect.Top) / (int)m_calendar.Month.DayHeight);				
				week = gCalendar.GetWeekOfYear(m_calendar.Month.m_days[i*7].Date,m_calendar.m_dateTimeFormat.CalendarWeekRule,m_calendar.m_dateTimeFormat.FirstDayOfWeek);
				if (mode == mcClickMode.Single)
				{
					if (this.Click!=null)
						this.Click(this,new WeeknumberClickEventArgs(week,button));
				}
				else
				{
					if (this.DoubleClick!=null)
						this.DoubleClick(this,new WeeknumberClickEventArgs(week,button));
				}
		
			}
		}

		internal bool IsVisible(Rectangle clip)
		{
			return m_region.IsVisible(clip); 	
		}

		internal void Draw(Graphics e)
		{
			StringFormat textFormat = new StringFormat();
			Pen linePen = new Pen(m_borderColor,1);
			Rectangle weekRect = new Rectangle();
			 
			int weeknr=0;	
			Brush weekBrush = new SolidBrush(this.BackColor);
			Brush weekTextBrush = new SolidBrush(this.TextColor); 
			int dayHeight;
			
			// Draw header
			textFormat.Alignment = StringAlignment.Center;   
			textFormat.LineAlignment = StringAlignment.Center;
			e.FillRectangle(weekBrush,m_rect);
			
			dayHeight = (int)m_calendar.Month.DayHeight; 			
			for (int i = 0;i<6;i++)
			{
				weekRect.Y = m_rect.Y + dayHeight*i;
				weekRect.Width = m_rect.Width; 
				weekRect.X =0;
				if (i==5)
					weekRect.Height = m_rect.Height - dayHeight*5;
				else
					weekRect.Height = dayHeight;
				
				weeknr = GetWeek(m_calendar.Month.m_days[i*7].Date);
				
				e.DrawString(weeknr.ToString(),this.Font,weekTextBrush,weekRect,textFormat);
					  
			}
			e.DrawLine(linePen,m_rect.Right-1,m_rect.Top,m_rect.Right-1,m_rect.Bottom); 
			// tidy up
			weekBrush.Dispose(); 
			weekTextBrush.Dispose();
			linePen.Dispose(); 
			
		}

		internal int GetWeek(DateTime dt)
		{
			int weeknr;
					
			try
			{
				// retrieve week by calling weeknumber callback
				weeknr = m_calendar.WeeknumberCallBack(dt);	
			}
			catch(Exception)
			{
				//if callback fails , call CalcWeek 
				weeknr = CalcWeek(dt);		
			}
			return weeknr;
		}

		internal int CalcWeek(DateTime dt)
		{
			int weeknr = 0;
			GregorianCalendar gCalendar = new GregorianCalendar();

			if ((m_calendar.m_dateTimeFormat.CalendarWeekRule == CalendarWeekRule.FirstFourDayWeek) &&
				(m_calendar.m_dateTimeFormat.FirstDayOfWeek == DayOfWeek.Monday))
				// Get ISO week
				weeknr = GetISO8601Weeknumber(dt); 	
			else
				// else get Microsoft week
				weeknr = gCalendar.GetWeekOfYear(dt,m_calendar.m_dateTimeFormat.CalendarWeekRule, m_calendar.m_dateTimeFormat.FirstDayOfWeek);
			
			return weeknr;
		}
	 	
		private int GetISO8601Weeknumber(DateTime date)
		{
			// Updated 2004.09.27. Cleaned the code and fixed a bug. Compared the algorithm with
			// code published here . Tested code successfully against the other algorithm 
			// for all dates in all years between 1900 and 2100.
			// Thanks to Marcus Dahlberg for pointing out the deficient logic.
			// Calculates the ISO 8601 Week Number
			// In this scenario the first day of the week is monday, 
			// and the week rule states that:
			// [...] the first calendar week of a year is the one 
			// that includes the first Thursday of that year and 
			// [...] the last calendar week of a calendar year is 
			// the week immediately preceding the first 
			// calendar week of the next year.
			// The first week of the year may thus start in the 
			// preceding year

			const int JAN = 1;
			const int DEC = 12;
			const int LASTDAYOFDEC = 31;
			const int FIRSTDAYOFJAN = 1;
			const int THURSDAY = 4;
			bool Week53Flag = false;

			// Get the day number since the beginning of the year
			int DayOfYear = date.DayOfYear;

			// Get the numeric weekday of the first day of the 
			// year (using sunday as FirstDay)
			int StartWeekDayOfYear = 
				(int)(new DateTime(date.Year, JAN, FIRSTDAYOFJAN)).DayOfWeek;
			int EndWeekDayOfYear = 
				(int)(new DateTime(date.Year, DEC, LASTDAYOFDEC)).DayOfWeek;
			
			// Compensate for the fact that we are using monday
			// as the first day of the week
				
			if (m_calendar.m_dateTimeFormat.FirstDayOfWeek!=0)  
			{
				//if( StartWeekDayOfYear == 0)
					StartWeekDayOfYear = 8 - StartWeekDayOfYear;
				//if( EndWeekDayOfYear == 0)
					EndWeekDayOfYear = 8 - EndWeekDayOfYear;
			}
			
			// Calculate the number of days in the first and last week
			int DaysInFirstWeek = 8 - (StartWeekDayOfYear  );
			int DaysInLastWeek = 8 - (EndWeekDayOfYear );

			// If the year either starts or ends on a thursday it will have a 53rd week
			if (StartWeekDayOfYear == THURSDAY || EndWeekDayOfYear == THURSDAY)
				Week53Flag = true;
		
			// We begin by calculating the number of FULL weeks between the start of the year and
			// our date. The number is rounded up, so the smallest possible value is 0.
			int FullWeeks = (int) Math.Ceiling((DayOfYear - (DaysInFirstWeek))/7.0);
    
			int WeekNumber = FullWeeks; 
     
			// If the first week of the year has at least four days, then the actual week number for our date
			// can be incremented by one.
			
			if (DaysInFirstWeek >= THURSDAY)
				WeekNumber = WeekNumber +1;
				
			// If week number is larger than week 52 (and the year doesn't either start or end on a thursday)
			// then the correct week number is 1. 
			if (WeekNumber > 52 && !Week53Flag)
				WeekNumber = 1;

			// If week number is still 0, it means that we are trying to evaluate the week number for a
			// week that belongs in the previous year (since that week has 3 days or less in our date's year).
			// We therefore make a recursive call using the last day of the previous year.
			if (WeekNumber == 0)
				WeekNumber = GetISO8601Weeknumber(
					new DateTime(date.Year-1, DEC, LASTDAYOFDEC));
			return WeekNumber;
		
		
		}

		

		#endregion

	}

	
	#region WeeknumberClickEventArgs
	
	public class WeeknumberClickEventArgs : EventArgs
	{
		#region Class Data
			
		private int m_week;
		private MouseButtons m_button;

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the DayClickEventArgs class with default settings
		/// </summary>
		public WeeknumberClickEventArgs()
		{
			m_button = MouseButtons.Left;
		}

		public WeeknumberClickEventArgs(int week, MouseButtons button)
		{
			this.m_week =week;
			this.m_button = button;
		}

		#endregion


		#region Properties

		public int Week
		{
			get
			{
				return this.m_week;
			}
		}

		public MouseButtons Button
		{
			get
			{
				return this.m_button; 
			}
		}

		#endregion
	}


	#endregion

	#region WeeknumberPropertyEventArgs
	
	public class WeeknumberPropertyEventArgs : EventArgs
	{
		#region Class Data

		/// <summary>
		/// The property that has changed
		/// </summary>
		private mcWeeknumberProperty m_property;

		#endregion

		#region Constructor

		public WeeknumberPropertyEventArgs()
		{
			m_property = 0;
		}

		public WeeknumberPropertyEventArgs(mcWeeknumberProperty property)
		{
			this.m_property = property;
		}

		#endregion


		#region Properties

		public mcWeeknumberProperty Property
		{
			get
			{
				return this.m_property;
			}
		}

		#endregion
	}


	#endregion

}
