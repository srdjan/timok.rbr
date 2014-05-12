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
using System.Windows.Forms; 
using System.Reflection;
using System.IO; 

namespace Pabo.Calendar
{
	
	public enum mcHeaderProperty 
	{
		Align = 0, MonthSelectors,YearSelectors, ShowMonth, Text , BackColor, Font,
		TextColor, MonthContextMenu }
	

	#region Delegates

	public delegate void HeaderPropertyEventHandler(object sender, HeaderPropertyEventArgs e);	

	#endregion

	/// <summary>
	/// Summary description for Header
	/// </summary>
	[TypeConverter(typeof(HeaderTypeConverter))]
	public class Header : IDisposable
	{
		#region private class members
		
		private bool disposed;
		private MonthCalendar m_calendar;
		private Color m_backColor;
		private Color m_textColor;
		private Font m_font;
		private bool m_monthSelector;
		private bool m_yearSelector;
		private bool m_contextMenu;
		
		private ContextMenu monthMenu = new ContextMenu();
		private Rectangle m_rect;
		private Region m_region;
		private Rectangle m_rightBtnRect;
		private Rectangle m_leftBtnRect;
		private Rectangle m_rightYearBtnRect;
		private Rectangle m_leftYearBtnRect;
		private Rectangle m_textRect;

		private ButtonState m_leftBtnState;
		private ButtonState m_rightBtnState;
		private ButtonState m_leftYearBtnState;
		private ButtonState m_rightYearBtnState;

		private string m_text;
		private mcTextAlign m_align;

		private bool m_showMonth;
				
		private Bitmap m_prevYear;
		private Bitmap m_nextYear;
		private Bitmap m_prevYearDisabled;
		private Bitmap m_nextYearDisabled;

		#endregion

		#region Eventhandlers
		
		internal event ClickEventHandler Click;
		internal event ClickEventHandler DoubleClick;
		internal event EventHandler PrevMonthButtonClick;
		internal event EventHandler NextMonthButtonClick;
		internal event EventHandler PrevYearButtonClick;
		internal event EventHandler NextYearButtonClick;
		internal event HeaderPropertyEventHandler PropertyChanged;

		#endregion

		#region Constructor

		public Header(MonthCalendar calendar)
		{
			m_calendar = calendar;
			m_backColor = Color.FromArgb(0,84,227); 
			m_textColor = Color.White;
			m_font = new Font("Microsoft Sans Serif",(float)8.25,FontStyle.Bold);
			m_showMonth = true;
			m_monthSelector = true;
			m_text = "";
			m_contextMenu = true;
			m_align = mcTextAlign.Center; 
			m_leftBtnState = ButtonState.Normal;
			m_rightBtnState = ButtonState.Normal;
			m_leftYearBtnState = ButtonState.Normal;
			m_rightYearBtnState = ButtonState.Normal;
			
			// load images
			m_prevYear = GetEmbeddedImage("prev_year.bmp");
			m_prevYear.MakeTransparent(); 
			m_prevYearDisabled = GetEmbeddedImage("prev_year_disabled.bmp");
			m_prevYearDisabled.MakeTransparent(); 
			m_nextYear = GetEmbeddedImage("next_year.bmp");
			m_nextYear.MakeTransparent(); 
			m_nextYearDisabled = GetEmbeddedImage("next_year_disabled.bmp");
			m_nextYearDisabled.MakeTransparent(); 
			
			// create monthContext menu and setup event handlers
			for(int k=0;k<12;k++)
			{
				monthMenu.MenuItems.Add(monthMenu.MenuItems.Count, 
					new MenuItem("")); 
				monthMenu.MenuItems[monthMenu.MenuItems.Count-1].Click+=new EventHandler(MonthContextMenu_Click); 
			
			}


			Setup();
	
		}

		#endregion
		
		#region Dispose
		
		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					// Remove event handlers
					for(int k=0;k<monthMenu.MenuItems.Count;k++)
					{
						monthMenu.MenuItems[k].Click-=new EventHandler(MonthContextMenu_Click); 
					}
			
					m_font.Dispose();
					m_region.Dispose(); 
			
					m_prevYear.Dispose();
					m_prevYearDisabled.Dispose();
					m_nextYear.Dispose();
					m_nextYearDisabled.Dispose();
			
					monthMenu.Dispose();

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

		#region Methods
		
		private void Setup()
		{
			
			int x = 10;
			
			if (m_yearSelector)
			{
				
				m_leftYearBtnRect = new Rectangle(x,5,20,20);
				m_rightYearBtnRect = new Rectangle(m_rect.Width-x-20,5,20,20);
				x+=20;
			}
			else
			{
				m_leftYearBtnRect = new Rectangle(0,0,0,0);
				m_rightYearBtnRect = new Rectangle(0,0,0,0);
			}
			if (m_monthSelector)
			{
				
				m_leftBtnRect = new Rectangle(x,5,20,20);
				m_rightBtnRect = new Rectangle(m_rect.Width-x-20,5,20,20);
				x+=20;
			}
			else
			{
				m_leftBtnRect = new Rectangle(0,0,0,0);
				m_rightBtnRect = new Rectangle(0,0,0,0);
			}
			m_textRect = new Rectangle(x + 2,0,m_rect.Width - (2*x)-8,m_rect.Height); 
			
		}

		private void DisplayMonthContextMenu(Point mouseLocation)
		{
	
			// Setup context menu
			string[] months = m_calendar.AllowedMonths(); 
			for(int k=0;k<months.Length;k++)
			{
				monthMenu.MenuItems[k].Text = months[k]; 

				if (k == m_calendar.ActiveMonth.Month-1)  
					monthMenu.MenuItems[k].Checked = true;
				else
					monthMenu.MenuItems[k].Checked = false;
			
			}
			//show context menu
			monthMenu.Show(m_calendar,new Point(mouseLocation.X,mouseLocation.Y));
	
		}

		internal void MouseClick(Point mouseLocation, MouseButtons button, mcClickMode mode)
		{
			Region leftBtnRgn = new Region(m_leftBtnRect);
			Region rightBtnRgn = new Region(m_rightBtnRect);
			Region leftYearBtnRgn = new Region(m_leftYearBtnRect);
			Region rightYearBtnRgn = new Region(m_rightYearBtnRect);
			MouseButtons selectButton;
			
			if (SystemInformation.MouseButtonsSwapped)
				selectButton = MouseButtons.Right;
			else
				selectButton = MouseButtons.Left;

			bool btnClick = false;
			
			if (m_region.IsVisible(mouseLocation))
			{
				if (button == selectButton)
				{
					if (m_monthSelector)
					{
						if ( (leftBtnRgn.IsVisible(mouseLocation)) &&  (m_leftBtnState!=ButtonState.Inactive) &&
							(m_leftBtnState!=ButtonState.Pushed) )
						{
							m_leftBtnState = ButtonState.Pushed;
							if (this.PrevMonthButtonClick!=null)
								this.PrevMonthButtonClick(this,new EventArgs());			
							btnClick = true;
						}
						if ( (rightBtnRgn.IsVisible(mouseLocation)) && (m_rightBtnState!=ButtonState.Inactive) &&
							(m_rightBtnState!=ButtonState.Pushed) )
						{
							m_rightBtnState = ButtonState.Pushed;
							if (this.NextMonthButtonClick!=null)
								this.NextMonthButtonClick(this,new EventArgs());
							btnClick = true;
						}
					}
					if (m_yearSelector)
					{
						if ( (leftYearBtnRgn.IsVisible(mouseLocation)) &&  (m_leftYearBtnState!=ButtonState.Inactive) &&
							(m_leftYearBtnState!=ButtonState.Pushed) )
						{
							m_leftYearBtnState = ButtonState.Pushed;
							if (this.PrevYearButtonClick!=null)
								this.PrevYearButtonClick(this,new EventArgs());			
							btnClick = true;
						}
						if ( (rightYearBtnRgn.IsVisible(mouseLocation)) && (m_rightYearBtnState!=ButtonState.Inactive) &&
							(m_rightYearBtnState!=ButtonState.Pushed) )
						{
							m_rightYearBtnState = ButtonState.Pushed;
							if (this.NextYearButtonClick!=null)
								this.NextYearButtonClick(this,new EventArgs());
							btnClick = true;
						}
					}
				}
				else
				{
					if (m_contextMenu)
					{
						DisplayMonthContextMenu(mouseLocation);
					}
				}
				
				if (mode == mcClickMode.Single)
				{
					if ((this.Click!=null) && (!btnClick))
						this.Click(this,new ClickEventArgs(button));
				}
				else
				{
					if ((this.DoubleClick!=null) && (!btnClick))
						this.DoubleClick(this,new ClickEventArgs(button));	
				}
			}
			
			leftBtnRgn.Dispose();
			rightBtnRgn.Dispose();
			leftYearBtnRgn.Dispose();
			rightYearBtnRgn.Dispose();
		}

		

		internal void MouseUp()
		{
			// if mouse button is released no button should be pushed
			if (m_leftBtnState!=ButtonState.Inactive) m_leftBtnState = ButtonState.Normal;
			if (m_rightBtnState!=ButtonState.Inactive) m_rightBtnState = ButtonState.Normal;
			if (m_leftYearBtnState!=ButtonState.Inactive) m_leftYearBtnState = ButtonState.Normal;
			if (m_rightYearBtnState!=ButtonState.Inactive) m_rightYearBtnState = ButtonState.Normal;
			
			m_calendar.Invalidate();	
		}

		internal void MouseMove(Point mouseLocation)
		{
			Region leftBtnRgn = new Region(m_leftBtnRect);
			Region rightBtnRgn = new Region(m_rightBtnRect);
			Region leftYearBtnRgn = new Region(m_leftYearBtnRect);
			Region rightYearBtnRgn = new Region(m_rightYearBtnRect);

			if (m_monthSelector)
			{
				// If not within left scroll button, make sure its not pushed
				if (!leftBtnRgn.IsVisible(mouseLocation))
					if (m_leftBtnState!=ButtonState.Inactive) m_leftBtnState = ButtonState.Normal;
						
				// If not within right scroll button, make sure its not pushed
				if (!rightBtnRgn.IsVisible(mouseLocation))
					if (m_rightBtnState!=ButtonState.Inactive) m_rightBtnState = ButtonState.Normal;
			}
			if (m_yearSelector)
			{
				// If not within left scroll button, make sure its not pushed
				if (!leftYearBtnRgn.IsVisible(mouseLocation))
					if (m_leftYearBtnState!=ButtonState.Inactive) m_leftYearBtnState = ButtonState.Normal;
						
				// If not within right scroll button, make sure its not pushed
				if (!rightYearBtnRgn.IsVisible(mouseLocation))
					if (m_rightYearBtnState!=ButtonState.Inactive) m_rightYearBtnState = ButtonState.Normal;
			}

			if (m_region.IsVisible(mouseLocation))
				m_calendar.ActiveRegion = mcCalendarRegion.Header;  
			
			
			leftBtnRgn.Dispose();
			rightBtnRgn.Dispose();
			leftYearBtnRgn.Dispose();
			rightYearBtnRgn.Dispose();
		}

		internal bool IsVisible(Rectangle clip)
		{
			return m_region.IsVisible(clip); 	
		}

		private Bitmap GetEmbeddedImage(string name)
		{
			Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
			Stream myStream = myAssembly.GetManifestResourceStream("Pabo.Calendar.Images."+name);
			Bitmap image = new Bitmap(myStream);
			return image;
		}
		
		internal void Draw(Graphics e)
		{
			StringFormat textFormat = new StringFormat();
			Brush textBrush = new SolidBrush(TextColor);
			Brush bgBrush = new SolidBrush(BackColor);
			string minMonth;
			string maxMonth;
			string currentMonth;
				

			string month;
			textFormat.LineAlignment = StringAlignment.Center;
			switch (m_align)
			{
				case mcTextAlign.Center:
				{
					textFormat.Alignment = StringAlignment.Center;
					break;
				}
				case mcTextAlign.Left:
				{
					textFormat.Alignment = StringAlignment.Near;
					break;
				}
				case mcTextAlign.Right:
				{
					textFormat.Alignment = StringAlignment.Far;
					break;
				}
			}
			
			e.FillRectangle(bgBrush,m_rect);			

			if (m_monthSelector)
			{
				currentMonth = m_calendar.Month.SelectedMonth.Year.ToString()+"-"+m_calendar.Month.SelectedMonth.Month.ToString();
								
				minMonth = m_calendar.MinDate.Year.ToString()+"-"+m_calendar.MinDate.Month.ToString();
				maxMonth = m_calendar.MaxDate.Year.ToString()+"-"+m_calendar.MaxDate.Month.ToString();
				
				if ((minMonth == currentMonth) && (m_leftBtnState != ButtonState.Pushed))
					m_leftBtnState = ButtonState.Inactive;
				else if (m_leftBtnState != ButtonState.Pushed)
					m_leftBtnState = ButtonState.Normal;

				if ((maxMonth == currentMonth) && (m_rightBtnState != ButtonState.Pushed))
					m_rightBtnState = ButtonState.Inactive;
				else if (m_rightBtnState != ButtonState.Pushed)
					m_rightBtnState = ButtonState.Normal;
							
			}
			if (m_yearSelector)
			{
				currentMonth = m_calendar.Month.SelectedMonth.Year.ToString()+"-"+m_calendar.Month.SelectedMonth.Month.ToString()+"-01";
				
				DateTime currentDate = DateTime.Parse(currentMonth);
				int days = DateTime.DaysInMonth(m_calendar.MinDate.Year,m_calendar.MinDate.Month); 
				DateTime minDate = DateTime.Parse(m_calendar.MinDate.Year.ToString()+"-"+m_calendar.MinDate.Month.ToString()+"-"+days.ToString());
				days = DateTime.DaysInMonth(m_calendar.MaxDate.Year,m_calendar.MaxDate.Month); 
				DateTime maxDate = DateTime.Parse(m_calendar.MaxDate.Year.ToString()+"-"+m_calendar.MaxDate.Month.ToString()+"-"+days.ToString());
				
				if ( (DateTime.Compare(currentDate.AddYears(-1),minDate)<0) && (m_leftYearBtnState != ButtonState.Pushed))  
					m_leftYearBtnState = ButtonState.Inactive;
				else if (m_leftYearBtnState != ButtonState.Pushed)
					m_leftYearBtnState = ButtonState.Normal;

				if ( (DateTime.Compare(currentDate.AddYears(1),maxDate)>0) && (m_rightYearBtnState != ButtonState.Pushed))  
					m_rightYearBtnState = ButtonState.Inactive;
				else if (m_rightYearBtnState != ButtonState.Pushed)
					m_rightYearBtnState = ButtonState.Normal;
			}

			
			if (m_calendar.Enabled) {
				if (m_monthSelector) {
					ControlPaint.DrawScrollButton(e,m_leftBtnRect,ScrollButton.Left,m_leftBtnState);
					ControlPaint.DrawScrollButton(e,m_rightBtnRect,ScrollButton.Right,m_rightBtnState);
				}
				if (m_yearSelector) {
					int corr = 0;
					ControlPaint.DrawButton(e,m_leftYearBtnRect,m_leftYearBtnState);
					if (m_leftYearBtnState==ButtonState.Pushed)
						corr = 1;
					if (m_leftYearBtnState!=ButtonState.Inactive) 
						e.DrawImage(m_prevYear,new Point(m_leftYearBtnRect.Left,m_leftYearBtnRect.Top+2+corr));  
					else
						e.DrawImage(m_prevYearDisabled,new Point(m_leftYearBtnRect.Left,m_leftYearBtnRect.Top+2));  
					
					ControlPaint.DrawButton(e,m_rightYearBtnRect,m_rightYearBtnState);
					corr = 0;
					
					if (m_rightYearBtnState==ButtonState.Pushed)
						corr = 1;
					if (m_rightYearBtnState!=ButtonState.Inactive) 
						e.DrawImage(m_nextYear,new Point(m_rightYearBtnRect.Left+3,m_rightYearBtnRect.Top+2+corr));  
					else
						e.DrawImage(m_nextYearDisabled,new Point(m_rightYearBtnRect.Left+3,m_rightYearBtnRect.Top+2));  
						
				}
				     
			}
			else {
				//IGOR:
//				ControlPaint.DrawScrollButton(e,m_leftBtnRect,ScrollButton.Left,ButtonState.Inactive);
//				ControlPaint.DrawScrollButton(e,m_rightBtnRect,ScrollButton.Right,ButtonState.Inactive);
//				ControlPaint.DrawButton(e,m_leftYearBtnRect,ButtonState.Inactive);
//				ControlPaint.DrawButton(e,m_rightYearBtnRect,ButtonState.Inactive);		
				if (m_monthSelector) {
					ControlPaint.DrawScrollButton(e,m_leftBtnRect,ScrollButton.Left,ButtonState.Inactive);
					ControlPaint.DrawScrollButton(e,m_rightBtnRect,ScrollButton.Right,ButtonState.Inactive);
				}
				if (m_yearSelector) {
					ControlPaint.DrawButton(e,m_leftYearBtnRect,ButtonState.Inactive);
					ControlPaint.DrawButton(e,m_rightYearBtnRect,ButtonState.Inactive);		
				}
				//IGOR: end
			}
		
			
			month = m_calendar.m_dateTimeFormat.GetMonthName(m_calendar.Month.SelectedMonth.Month)+" "+m_calendar.Month.SelectedMonth.Year.ToString();  
			if (ShowMonth)
				e.DrawString(month,Font,textBrush,m_textRect,textFormat); 
			else
				e.DrawString(m_text,Font,textBrush,m_textRect,textFormat);
						
			textBrush.Dispose();
			bgBrush.Dispose();
		}

		#endregion

		#region Properties
	
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
				Setup();
			}
		}
		
		[Description("Determines if the month selection menu should be displayed when right clicking the header.")]
		[DefaultValue(true)]
		public bool MonthContextMenu
		{
			get
			{
				return m_contextMenu;
			}
			set
			{
				if (m_contextMenu!=value)
				{
					m_contextMenu = value;
					if (PropertyChanged!=null)
						PropertyChanged(this,new HeaderPropertyEventArgs(mcHeaderProperty.MonthContextMenu)); 
					m_calendar.Invalidate();
				}
			}
		}

		[Description("Determines the position for the text.")]
		[DefaultValue(typeof(mcTextAlign),"Center")]
		public mcTextAlign Align
		{
			get
			{
				return m_align;
			}
			set
			{
				if (m_align!=value)
				{
					m_align = value;
					if (PropertyChanged!=null)
						PropertyChanged(this,new HeaderPropertyEventArgs(mcHeaderProperty.Align)); 
					m_calendar.Invalidate();
				}
			}
		}
		
		[Description("Determines wether the month selector buttons should be displayed.")]
		[DefaultValue(true)]
		public bool MonthSelectors
		{
			get
			{
				return m_monthSelector;
			}
			set
			{
				if (m_monthSelector!=value)
				{
					m_monthSelector = value;
					
					Setup();
					
					if (PropertyChanged!=null)
						PropertyChanged(this,new HeaderPropertyEventArgs(mcHeaderProperty.MonthSelectors)); 
					m_calendar.Invalidate();
				}
			}
		}

		[Description("Determines wether the year selector buttons should be displayed.")]
		[DefaultValue(false)]
		public bool YearSelectors
		{
			get
			{
				return m_yearSelector;
			}
			set
			{
				if (m_yearSelector!=value)
				{
					m_yearSelector = value;
					
					Setup();
					
					if (PropertyChanged!=null)
						PropertyChanged(this,new HeaderPropertyEventArgs(mcHeaderProperty.YearSelectors)); 
					m_calendar.Invalidate();
				}
			}
		}

		[Description("Determines wether the current month should be displayed.")]
		[DefaultValue(true)]
		public bool ShowMonth
		{
			get
			{
				return m_showMonth;
			}
			set
			{
				if (m_showMonth!=value)
				{
					m_showMonth = value;
					if (PropertyChanged!=null)
						PropertyChanged(this,new HeaderPropertyEventArgs(mcHeaderProperty.ShowMonth)); 
					m_calendar.Invalidate();
				}
			}
		}

		[Description("Text to be displayed in the header.")]
		[DefaultValue("")]
		public string Text
		{
			get
			{
				return m_text;
			}
			set
			{
				if (m_text!=value)
				{
					m_text = value;
					if (PropertyChanged!=null)
						PropertyChanged(this,new HeaderPropertyEventArgs(mcHeaderProperty.Text)); 
					m_calendar.Invalidate();
				}
			}
		}

		[Description("Color used for background.")]
		[DefaultValue(typeof(Color),"0,84,227")]
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
						PropertyChanged(this,new HeaderPropertyEventArgs(mcHeaderProperty.BackColor)); 
					m_calendar.Invalidate();
				}
			}
		}

		[Description("Font used for header.")]
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
						PropertyChanged(this,new HeaderPropertyEventArgs(mcHeaderProperty.Font)); 
					m_calendar.Invalidate();
				}
			}
		}
		
		[Description("Color used for text.")]
		[DefaultValue(typeof(Color),"Black")]
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
						PropertyChanged(this,new HeaderPropertyEventArgs(mcHeaderProperty.TextColor)); 
					m_calendar.Invalidate();
				}
			}
		}

		#endregion

		#region Events
		
		private void MonthContextMenu_Click(object sender, EventArgs e)
		{
			MenuItem item = (MenuItem)sender;
			m_calendar.ActiveMonth.Month = item.Index+1;  
		}


		#endregion

	}

	
	#region HeaderPropertyEventArgs
	
	public class HeaderPropertyEventArgs : EventArgs
	{
		#region Class Data

		/// <summary>
		/// The property that has changed
		/// </summary>
		private mcHeaderProperty m_property;

		#endregion

		#region Constructor

		public HeaderPropertyEventArgs()
		{
			m_property = 0;
		}

		public HeaderPropertyEventArgs(mcHeaderProperty property)
		{
			this.m_property = property;
		}

		#endregion


		#region Properties

		public mcHeaderProperty Property
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
