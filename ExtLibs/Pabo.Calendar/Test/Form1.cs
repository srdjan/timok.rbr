using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;

using Pabo.Calendar;

namespace Test 
{
	public class Form1 : System.Windows.Forms.Form {
		private Pabo.Calendar.MonthCalendar monthCalendar;
		private System.ComponentModel.Container components = null;

		public Form1() {
			InitializeComponent();
		}

		private void Form1_Load(object sender, System.EventArgs e) {
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.monthCalendar = new Pabo.Calendar.MonthCalendar();
			this.SuspendLayout();
			// 
			// monthCalendar
			// 
			this.monthCalendar.ActiveMonth.Month = 2;
			this.monthCalendar.ActiveMonth.Year = 2006;
			this.monthCalendar.Culture = new System.Globalization.CultureInfo("en-US");
			this.monthCalendar.Footer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.monthCalendar.Header.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.monthCalendar.Header.TextColor = System.Drawing.Color.White;
			this.monthCalendar.ImageList = null;
			this.monthCalendar.Location = new System.Drawing.Point(88, 24);
			this.monthCalendar.MaxDate = new System.DateTime(2016, 2, 21, 12, 6, 57, 390);
			this.monthCalendar.MinDate = new System.DateTime(1996, 2, 21, 12, 6, 57, 390);
			this.monthCalendar.Month.BackgroundImage = null;
			this.monthCalendar.Month.Colors.FocusBackground = System.Drawing.Color.White;
			this.monthCalendar.Month.Colors.FocusDate = System.Drawing.Color.Black;
			this.monthCalendar.Month.Colors.FocusText = System.Drawing.Color.Black;
			this.monthCalendar.Month.Colors.SelectedDate = System.Drawing.Color.Black;
			this.monthCalendar.Month.Colors.SelectedText = System.Drawing.Color.Black;
			this.monthCalendar.Month.DateFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.monthCalendar.Month.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.monthCalendar.Name = "monthCalendar";
			this.monthCalendar.SelectionMode = Pabo.Calendar.mcSelectionMode.MultiExtended;
			this.monthCalendar.Size = new System.Drawing.Size(368, 224);
			this.monthCalendar.TabIndex = 0;
			this.monthCalendar.Weekdays.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.monthCalendar.Weeknumbers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
			this.ClientSize = new System.Drawing.Size(608, 296);
			this.Controls.Add(this.monthCalendar);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			Application.Run(new Form1());
		}
	}
}
