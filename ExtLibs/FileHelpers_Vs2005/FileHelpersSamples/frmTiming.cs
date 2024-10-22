using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using FileHelpers;

namespace FileHelpersSamples
{
	/// <summary>
	/// Summary description for frmSamples.
	/// </summary>
	public class frmTimming : frmFather
	{
		private Button button1;
		private Button cmdCreateFile;
		private NumericUpDown txtRecords;
		private Label label2;
		private Label label1;
		private Label lblSize;
		private Button cmdRun;
		private Label lblTime;
		private Label label4;
		private Label label3;
		private Label lblTimeAsync;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public frmTimming()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.cmdCreateFile = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.cmdRun = new System.Windows.Forms.Button();
			this.txtRecords = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.lblSize = new System.Windows.Forms.Label();
			this.lblTime = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lblTimeAsync = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.txtRecords)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox2
			// 
			this.pictureBox2.Location = new System.Drawing.Point(328, 0);
			this.pictureBox2.Name = "pictureBox2";
			// 
			// pictureBox3
			// 
			this.pictureBox3.Location = new System.Drawing.Point(368, 8);
			this.pictureBox3.Name = "pictureBox3";
			// 
			// cmdCreateFile
			// 
			this.cmdCreateFile.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.cmdCreateFile.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdCreateFile.ForeColor = System.Drawing.Color.White;
			this.cmdCreateFile.Location = new System.Drawing.Point(288, 72);
			this.cmdCreateFile.Name = "cmdCreateFile";
			this.cmdCreateFile.Size = new System.Drawing.Size(168, 32);
			this.cmdCreateFile.TabIndex = 4;
			this.cmdCreateFile.Text = "Create Temp File ->";
			this.cmdCreateFile.Click += new System.EventHandler(this.cmdCreateFile_Click);
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.Navy;
			this.button1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button1.ForeColor = System.Drawing.Color.White;
			this.button1.Location = new System.Drawing.Point(128, 232);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(192, 32);
			this.button1.TabIndex = 6;
			this.button1.Text = "Exit";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// cmdRun
			// 
			this.cmdRun.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.cmdRun.Enabled = false;
			this.cmdRun.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmdRun.ForeColor = System.Drawing.Color.White;
			this.cmdRun.Location = new System.Drawing.Point(288, 160);
			this.cmdRun.Name = "cmdRun";
			this.cmdRun.Size = new System.Drawing.Size(168, 32);
			this.cmdRun.TabIndex = 7;
			this.cmdRun.Text = "Run Test ->";
			this.cmdRun.Click += new System.EventHandler(this.cmdRun_Click);
			// 
			// txtRecords
			// 
			this.txtRecords.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtRecords.Increment = new System.Decimal(new int[] {
																		 1000,
																		 0,
																		 0,
																		 0});
			this.txtRecords.Location = new System.Drawing.Point(184, 77);
			this.txtRecords.Maximum = new System.Decimal(new int[] {
																	   10000000,
																	   0,
																	   0,
																	   0});
			this.txtRecords.Name = "txtRecords";
			this.txtRecords.Size = new System.Drawing.Size(88, 23);
			this.txtRecords.TabIndex = 8;
			this.txtRecords.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtRecords.Value = new System.Decimal(new int[] {
																	 10000,
																	 0,
																	 0,
																	 0});
			this.txtRecords.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRecords_KeyDown);
			this.txtRecords.ValueChanged += new System.EventHandler(this.txtRecords_ValueChanged);
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.White;
			this.label2.Location = new System.Drawing.Point(16, 80);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120, 16);
			this.label2.TabIndex = 9;
			this.label2.Text = "Number of records";
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(16, 112);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 16);
			this.label1.TabIndex = 10;
			this.label1.Text = "File Size:";
			// 
			// lblSize
			// 
			this.lblSize.BackColor = System.Drawing.Color.Transparent;
			this.lblSize.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblSize.ForeColor = System.Drawing.Color.White;
			this.lblSize.Location = new System.Drawing.Point(152, 112);
			this.lblSize.Name = "lblSize";
			this.lblSize.Size = new System.Drawing.Size(120, 16);
			this.lblSize.TabIndex = 11;
			this.lblSize.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblTime
			// 
			this.lblTime.BackColor = System.Drawing.Color.Transparent;
			this.lblTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblTime.ForeColor = System.Drawing.Color.White;
			this.lblTime.Location = new System.Drawing.Point(136, 168);
			this.lblTime.Name = "lblTime";
			this.lblTime.Size = new System.Drawing.Size(136, 16);
			this.lblTime.TabIndex = 13;
			this.lblTime.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.ForeColor = System.Drawing.Color.White;
			this.label4.Location = new System.Drawing.Point(8, 168);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(96, 16);
			this.label4.TabIndex = 12;
			this.label4.Text = "Process Time:";
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.White;
			this.label3.Location = new System.Drawing.Point(8, 192);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(136, 16);
			this.label3.TabIndex = 14;
			this.label3.Text = "Async Process Time:";
			// 
			// lblTimeAsync
			// 
			this.lblTimeAsync.BackColor = System.Drawing.Color.Transparent;
			this.lblTimeAsync.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblTimeAsync.ForeColor = System.Drawing.Color.White;
			this.lblTimeAsync.Location = new System.Drawing.Point(128, 192);
			this.lblTimeAsync.Name = "lblTimeAsync";
			this.lblTimeAsync.Size = new System.Drawing.Size(144, 16);
			this.lblTimeAsync.TabIndex = 15;
			this.lblTimeAsync.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// frmTimming
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(466, 304);
			this.Controls.Add(this.lblTimeAsync);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lblTime);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.lblSize);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtRecords);
			this.Controls.Add(this.cmdRun);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.cmdCreateFile);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "frmTimming";
			this.Text = "FileHelpers Library - Time And Stress Tests";
			this.Load += new System.EventHandler(this.frmTimming_Load);
			this.Closed += new System.EventHandler(this.frmTimming_Closed);
			this.Controls.SetChildIndex(this.pictureBox2, 0);
			this.Controls.SetChildIndex(this.pictureBox3, 0);
			this.Controls.SetChildIndex(this.cmdCreateFile, 0);
			this.Controls.SetChildIndex(this.button1, 0);
			this.Controls.SetChildIndex(this.cmdRun, 0);
			this.Controls.SetChildIndex(this.txtRecords, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.lblSize, 0);
			this.Controls.SetChildIndex(this.label4, 0);
			this.Controls.SetChildIndex(this.lblTime, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.Controls.SetChildIndex(this.lblTimeAsync, 0);
			((System.ComponentModel.ISupportInitialize)(this.txtRecords)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private void button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void cmdCreateFile_Click(object sender, EventArgs e)
		{
			cmdCreateFile.Enabled = false;
			cmdRun.Enabled = false;
			this.Cursor = Cursors.WaitCursor;

			CreateDelimitedFile("tempFile.tmp", (int) txtRecords.Value);
			this.Cursor = Cursors.Default;
			cmdCreateFile.Enabled = true;
			cmdRun.Enabled = true;
		}

		void CreateDelimitedFile(string fileName, int records)
		{
			StreamWriter fs = new StreamWriter(fileName, false);

			for (int i = 0; i < (records/10); i++)
			{
				fs.Write(mTestData);
			}

			fs.Close();

			FileInfo info = new FileInfo(fileName);
			lblSize.Text = (info.Length/1024).ToString() + " Kb";
		}

		private string mTestData;
		private string mTestData2;

		private void frmTimming_Load(object sender, EventArgs e)
		{
			mTestData = "10248|VINET|5|04071996|01081996|16071996|3|32.38" + Environment.NewLine +
				"10249|TOMSP|6|05071996|16081996|10071996|1|11.61" + Environment.NewLine +
				"10250|HANAR|4|08071996|05081996|12071996|2|65.83" + Environment.NewLine +
				"10251|VICTE|3|08071996|05081996|15071996|1|41.34" + Environment.NewLine +
				"10252|SUPRD|4|09071996|06081996|11071996|2|51.3" + Environment.NewLine +
				"10253|HANAR|3|10071996|24071996|16071996|2|58.17" + Environment.NewLine +
				"10254|CHOPS|5|11071996|08081996|23071996|2|22.98" + Environment.NewLine +
				"10255|RICSU|9|12071996|09081996|15071996|3|148.33" + Environment.NewLine +
				"10256|WELLI|3|15071996|12081996|17071996|2|13.97" + Environment.NewLine +
				"10257|HILAA|4|16071996|13081996|22071996|3|81.91" + Environment.NewLine;

			mTestData2 = "10248|VINET|5|3|32.38" + Environment.NewLine +
				"10249|TOMSP|6|1|11.61" + Environment.NewLine +
				"10250|HANAR|4|2|65.83" + Environment.NewLine +
				"10251|VICTE|3|1|41.34" + Environment.NewLine +
				"10252|SUPRD|4|2|51.3" + Environment.NewLine +
				"10253|HANAR|3|2|58.17" + Environment.NewLine +
				"10254|CHOPS|5|2|22.98" + Environment.NewLine +
				"10255|RICSU|9|3|148.33" + Environment.NewLine +
				"10256|WELLI|3|2|13.97" + Environment.NewLine +
				"10257|HILAA|4|3|81.91" + Environment.NewLine;

		}

		private void cmdRun_Click(object sender, EventArgs e)
		{
			cmdCreateFile.Enabled = false;
			cmdRun.Enabled = false;
			this.Cursor = Cursors.WaitCursor;
			lblTimeAsync.Text = "";
			lblTime.Text = "";
			Application.DoEvents();

			RunTest();
			RunTestAsync();

			this.Cursor = Cursors.Default;
			cmdCreateFile.Enabled = true;
			cmdRun.Enabled = true;

		}

		private void RunTest()
		{
			long start = DateTime.Now.Ticks;

			FileHelperEngine engine = new FileHelperEngine(typeof (OrdersVerticalBar));

			engine.ReadFile("tempFile.tmp");

			long end = DateTime.Now.Ticks;

			TimeSpan span = new TimeSpan(end - start);
			lblTime.Text = Math.Round(span.TotalSeconds, 4).ToString() + " sec.";
			Application.DoEvents();
		}

		private void RunTestAsync()
		{
			long start = DateTime.Now.Ticks;

			FileHelperAsyncEngine engine = new FileHelperAsyncEngine(typeof (OrdersVerticalBar));

			engine.BeginReadFile("tempFile.tmp");

			while (engine.ReadNext() != null)
			{
			}

			long end = DateTime.Now.Ticks;

			TimeSpan span = new TimeSpan(end - start);
			lblTimeAsync.Text = Math.Round(span.TotalSeconds, 4).ToString() + " sec.";
			Application.DoEvents();
		}

		private void frmTimming_Closed(object sender, EventArgs e)
		{
			if (File.Exists("tempFile.tmp"))
				File.Delete("tempFile.tmp");
		}

		private void txtRecords_ValueChanged(object sender, EventArgs e)
		{
			ResetButtons();
		}

		private void ResetButtons()
		{
			cmdRun.Enabled = false;
			lblSize.Text = "";
			lblTimeAsync.Text = "";
			lblTime.Text = "";
		}

		private void txtRecords_KeyDown(object sender, KeyEventArgs e)
		{
			ResetButtons();
		}

	}
}