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
using System.Drawing; 
using System.Windows.Forms;

namespace Pabo.Calendar
{	
	public enum mcDayState {Normal = 0, Focus, Selected}

	#region Delegates

	public delegate void DayClickEventHandler(object sender, DayClickEventArgs e);
	public delegate void DayEventHandler(object sender, DayEventArgs e);
	public delegate void DaySelectedEventHandler(object sender, DaySelectedEventArgs e);
	public delegate void DayDragDropEventHandler(object sender, DayDragDropEventArgs e);

	#endregion

	/// <summary>
	/// Summary description for Day.
	/// </summary>
	internal class Day : IDisposable	
	{
		#region Private class members
		
		private bool disposed;
		private Rectangle m_rect;
		private Region m_region;
		private DateTime m_date;
		private MonthCalendar m_calendar;
		private Month m_month;
		private Image m_dayImage;
		private int m_selection;
		
		private mcDayState m_state;
		
		#endregion
		
		#region constructor
		
		public Day()
		{
			m_state = mcDayState.Normal; 
			m_selection = -1;
		}	

		#endregion
	
		#region Dispose
		
		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					m_region.Dispose();
					m_dayImage.Dispose();
				
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

		#region properties
		
		internal MonthCalendar Calendar
		{
			get
			{
				return m_calendar;
			}
			set
			{	
				m_calendar = value;
			}
		}

		internal Month Month
		{
			get
			{
				return m_month;
			}
			set
			{	
				m_month = value;
			}
		}
		
		public int SelectionArea
		{
			get
			{
				return m_selection;
			}
			set
			{	
				if (value!=m_selection)
				{
					m_selection = value;
				}
			}
		}

		public int Week
		{
			get
			{
				return m_calendar.WeeknumberCallBack(m_date);
			}
		}

		public DayOfWeek Weekday
		{
			get
			{
				return m_date.DayOfWeek;
			}	
		}

		public mcDayState State
		{
			get
			{
				return m_state;
			}
			set
			{	
				if (value!=m_state)
				{
					m_state = value;
				}
			}
		}
		
		public Rectangle Rectangle
		{
			get
			{
				return m_rect;
			}
			set
			{
				if (value!=m_rect)
				{
					m_rect = value;
					m_region = new Region(m_rect); 
				}
			}
		}

		public DateTime Date
		{
			get
			{
				return m_date;
			}
			set
			{
				if (m_date!=value)
				{
					m_date = value;
				}
			}
		}
		
		#endregion
		
		#region Methods
		
		private Image GetImage(int index)
		{
			// Check that an ImageList exists and that index is valid
			if (m_month.Calendar.ImageList!=null)
			{
				if ((index>=0) && (index <m_month.Calendar.ImageList.Images.Count))
				{
					return m_month.Calendar.ImageList.Images[index]; 
				}
				else return null;
			}
			else return null;
					
		}

		private StringFormat GetStringAlignment(mcItemAlign align)
		{
			StringFormat sf = new StringFormat();
 
			switch (align)
			{
				case mcItemAlign.LeftCenter:
				{
					sf.Alignment = StringAlignment.Near;   
					sf.LineAlignment = StringAlignment.Center;
					break;
				}
				case mcItemAlign.RightCenter:
				{
					sf.Alignment = StringAlignment.Far;   
					sf.LineAlignment = StringAlignment.Center;
					break;
				}
				case mcItemAlign.TopCenter:
				{
					sf.Alignment = StringAlignment.Center;   
					sf.LineAlignment = StringAlignment.Near;
					break;
				}
				case mcItemAlign.BottomCenter:
				{
					sf.Alignment = StringAlignment.Center;   
					sf.LineAlignment = StringAlignment.Far;
					break;
				}
				case mcItemAlign.TopLeft:
				{
					sf.Alignment = StringAlignment.Near;   
					sf.LineAlignment = StringAlignment.Near;
					break;
				}
				case mcItemAlign.TopRight:
				{
					sf.Alignment = StringAlignment.Far;   
					sf.LineAlignment = StringAlignment.Near;
					break;
				}
				case mcItemAlign.Center:
				{
					sf.Alignment = StringAlignment.Center;   
					sf.LineAlignment = StringAlignment.Center;
					break;
				}
				case mcItemAlign.BottomLeft:
				{
					sf.Alignment = StringAlignment.Near;   
					sf.LineAlignment = StringAlignment.Far;
					break;
				}
				case mcItemAlign.BottomRight:
				{
					sf.Alignment = StringAlignment.Far;   
					sf.LineAlignment = StringAlignment.Far;
					break;
				}
			}
			
			return sf;
		}

		internal void Draw(Graphics e)
		{
											
			StringFormat dateAlign = new StringFormat();
			StringFormat textAlign = new StringFormat();
           	Font boldFont = new Font(m_month.DateFont.Name,m_month.DateFont.Size,m_month.DateFont.Style | FontStyle.Bold);
            Color bgColor = m_month.Colors.Background;
            Color textColor = m_month.Colors.Text;
            Color dateColor = m_month.Colors.Date;
            Brush dateBrush = new SolidBrush(dateColor);
            Brush textBrush = new SolidBrush(textColor);
            Brush bgBrush = new SolidBrush(bgColor);   
		
            
            string dateString;
			Rectangle imageRect = new Rectangle(); 
			string text = "";
			bool drawDay = false;
			bool enabled = true;
			
			int i = -1;

			bool boldedDate = false;
 
			DateItem[] info;
			m_dayImage = null;
	
			dateAlign = GetStringAlignment(m_month.DateAlign); 
			textAlign = GetStringAlignment(m_month.TextAlign);							
			
			if ((m_month.SelectedMonth.Month == m_date.Month) || (m_month.Calendar.ShowTrailingDates))
				drawDay = true;
			
			if ((m_date.DayOfWeek == DayOfWeek.Saturday) || (m_date.DayOfWeek == DayOfWeek.Sunday))
			{
			    bgColor = m_month.Colors.WeekendBackground;
				dateColor= m_month.Colors.WeekendDate;
				textColor= m_month.Colors.WeekendText;
			}			
			
			if (m_month.SelectedMonth.Month  != m_date.Month)
			{
				bgColor =  m_month.Colors.TrailingBackground; 
				dateColor = m_month.Colors.TrailingDate; 
				textColor = m_month.Colors.TrailingText; 
			}
				
			// Check if formatting should be applied
			if ((m_month.FormatTrailing) || (m_month.SelectedMonth.Month  == m_date.Month)) 
			{
				// check of there is formatting for this day
				info = m_calendar.GetDateInfo(this.Date);
				if (info.Length > 0)
					i = 0;
				// go through the available dateitems
				while ((i<info.Length) && (drawDay))
				{
					if (info.Length>0)
					{
						DateItem dateInfo = info[i];
				
						if (dateInfo.BackColor!=Color.Empty)  
							bgColor = dateInfo.BackColor;
						if (dateInfo.DateColor!=Color.Empty)  
							dateColor = dateInfo.DateColor;
						if (dateInfo.TextColor!=Color.Empty)  
							textColor = dateInfo.TextColor;
						text = dateInfo.Text; 
				
						if (dateInfo.Weekend)
						{
							bgColor = m_month.Colors.WeekendBackground;	
							dateColor = m_month.Colors.WeekendDate;
							textColor = m_month.Colors.WeekendText;
						}
						boldedDate = dateInfo.BoldedDate; 
						enabled = dateInfo.Enabled;
						if (!dateInfo.Enabled)
						{
							bgColor = m_month.Colors.DisabledBackground;	
							dateColor = m_month.Colors.DisabledDate;
							textColor = m_month.Colors.DisabledText;
						}
 						
						m_dayImage = dateInfo.Image;  	
									
						if (m_dayImage!=null)
							imageRect = ImageRect(m_month.ImageAlign);	
					}

					if (m_state == mcDayState.Selected)
					{
						dateColor = m_month.Colors.SelectedDate; 
						textColor = m_month.Colors.SelectedText;
					}
					if ((m_state == mcDayState.Focus) && (m_month.Calendar.ShowFocus))  
					{
						dateColor = m_month.Colors.FocusDate; 
						textColor = m_month.Colors.FocusText;
					}

                    if (bgColor != Color.Transparent)
                    {
                        bgBrush = new SolidBrush(Color.FromArgb(m_month.Transparency.Background, bgColor));
                        e.FillRectangle(bgBrush, m_rect);
                    }
					ControlPaint.DrawBorder(e,m_rect, m_month.Colors.Border,m_month.BorderStyles.Normal);
					if (m_dayImage!=null)
					{
						if (enabled)
							e.DrawImageUnscaled(m_dayImage,imageRect);
						else
							ControlPaint.DrawImageDisabled(e,m_dayImage,imageRect.X,imageRect.Y,m_month.Colors.DisabledBackground);   
					}
            						
					// Check if we should append month name to date
					if ((m_month.ShowMonthInDay) &&
						((m_date.AddDays(-1).Month != m_date.Month) ||
						(m_date.AddDays(1).Month != m_date.Month)))							
						dateString = m_date.Day.ToString()+" "+m_calendar.m_dateTimeFormat.GetMonthName(m_date.Month);  
					else
						dateString = m_date.Day.ToString();

                    if (dateColor != Color.Transparent)
                    {
                        dateBrush = new SolidBrush(Color.FromArgb(m_month.Transparency.Text, dateColor));

                        // Should date be bolded ?
                        if (!boldedDate)
                            e.DrawString(dateString, m_month.DateFont, dateBrush, m_rect, dateAlign);
                        else
                            e.DrawString(dateString, boldFont, dateBrush, m_rect, dateAlign);
                    }
                    if ((text.Length > 0) && (textColor != Color.Transparent))
                    {
                        textBrush = new SolidBrush(Color.FromArgb(m_month.Transparency.Text, textColor));
                        e.DrawString(text, m_month.TextFont, textBrush, m_rect, textAlign);
                    }
					i++;	
				}
			} 
								
			dateBrush.Dispose();
			bgBrush.Dispose();
			textBrush.Dispose();
			boldFont.Dispose();
			dateAlign.Dispose();
			textAlign.Dispose();
			
		}

		private Rectangle ImageRect(mcItemAlign align)
		{
			Rectangle imageRect = new Rectangle(0,0,m_rect.Width,m_rect.Height);
 
			switch (align)
			{
				
				case mcItemAlign.LeftCenter:
				{
					imageRect.X = m_rect.X + 2;
					imageRect.Y = m_rect.Top +((m_rect.Height/2) - (m_dayImage.Height/2));
					break;
				}
				case mcItemAlign.RightCenter:
				{
					imageRect.X = m_rect.Right - 2 - m_dayImage.Width;
					imageRect.Y = m_rect.Top +((m_rect.Height/2) - (m_dayImage.Height/2));
					break;
				}
				case mcItemAlign.TopCenter:
				{
					imageRect.X = m_rect.X +((m_rect.Width/2) - (m_dayImage.Width/2));
					imageRect.Y = m_rect.Y + 2;
					break;
				}
				case mcItemAlign.BottomCenter:
				{
					imageRect.X = m_rect.X +((m_rect.Width/2) - (m_dayImage.Width/2));
					imageRect.Y = m_rect.Bottom -2 - m_dayImage.Height;
					break;
				}
				
				case mcItemAlign.TopLeft:
				{
					imageRect.X = m_rect.X + 2;
					imageRect.Y = m_rect.Y + 2;
					break;
				}
				case mcItemAlign.TopRight:
				{
					imageRect.X = m_rect.Right - 2 - m_dayImage.Width;
					imageRect.Y = m_rect.Y + 2;
					break;
				}
				case mcItemAlign.Center:
				{
					imageRect.X = m_rect.X +((m_rect.Width/2) - (m_dayImage.Width/2));
					imageRect.Y = m_rect.Top +((m_rect.Height/2) - (m_dayImage.Height/2));
					break;
				}
				case mcItemAlign.BottomLeft:
				{
					imageRect.X = m_rect.X + 2;
					imageRect.Y = m_rect.Bottom -2 - m_dayImage.Height;
					break;
				}
				case mcItemAlign.BottomRight:
				{
					imageRect.X = m_rect.Right - 2 - m_dayImage.Width;
					imageRect.Y = m_rect.Bottom -2 - m_dayImage.Height;	
					break;
				}
			}
			return imageRect;
		}

		internal bool HitTest(Point p)
		{
			if (m_region.IsVisible(p))
				return true;
			else
				return false;
		}

		#endregion
		
	}

	#region DayEventArgs
	
	public class DayEventArgs : EventArgs
	{
		#region Class Data
		
		string m_date;
		
		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the DayClickEventArgs class with default settings
		/// </summary>
		public DayEventArgs()
		{
			m_date = DateTime.Today.ToShortDateString();
		}

		public DayEventArgs(string date)
		{
			this.m_date = date;
		}

		#endregion


		#region Properties

		public string Date
		{
			get
			{
				return this.m_date; 
			}
		}

		#endregion
	}


	#endregion

	#region DayClickEventArgs
	
	public class DayClickEventArgs : EventArgs
	{
		#region Class Data
			
		private string m_date;
		private MouseButtons m_button;

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the DayClickEventArgs class with default settings
		/// </summary>
		public DayClickEventArgs()
		{
			m_date = DateTime.Today.ToShortDateString();
			m_button = MouseButtons.Left;
		}

		public DayClickEventArgs(string date, MouseButtons button)
		{
			this.m_date =date;
			this.m_button = button;
		}

		#endregion


		#region Properties

		public string Date
		{
			get
			{
				return this.m_date;
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

	#region DaySelectedEventArgs
	
	public class DaySelectedEventArgs : EventArgs
	{
		#region Class Data
		
		private string[] m_days;
		
		#endregion

		#region Constructor
	

		public DaySelectedEventArgs(string[] days)
		{
			this.m_days = days;
		}

		#endregion


		#region Properties

		public string[] Days
		{
			get
			{
				return this.m_days; 
			}
		}

		#endregion
	}


	#endregion

	#region DayDragDropEventArgs
	
	public class DayDragDropEventArgs : EventArgs
	{

		#region Class Data
			
		private string m_date;
		private int m_keyState;
		private IDataObject m_data;

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the DayClickEventArgs class with default settings
		/// </summary>
		public DayDragDropEventArgs(IDataObject data, int keystate , string date)
		{
			m_date = date;
			m_data = data;
			m_keyState = keystate;	
		}
		
		#endregion


		#region Properties

		public string Date
		{
			get
			{
				return this.m_date;
			}
		}

		public int KeyState
		{
			get
			{
				return this.m_keyState;
			}
		}

		public IDataObject Data
		{
			get
			{
				return this.m_data;
			}
		}


		#endregion


	}


	#endregion
}
