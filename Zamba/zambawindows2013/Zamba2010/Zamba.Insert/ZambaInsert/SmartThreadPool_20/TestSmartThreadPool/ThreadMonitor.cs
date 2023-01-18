using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Zamba.ThreadPool;
using Zamba.ThreadPool;
using Zamba.Core;
using Zamba.ThreadPool;

namespace Zamba.ThreadPool
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblThreadInUse;
        private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown spinMaxThreads;
		private System.Windows.Forms.NumericUpDown spinMinThreads;
		private System.Windows.Forms.NumericUpDown spinInterval;
		private System.Windows.Forms.Timer timerPoll;
		private System.ComponentModel.IContainer components;
        private System.Windows.Forms.Label lblThreadsInPool;

		private System.Windows.Forms.Label lblWaitingCallbacks;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label lblWorkItemsGenerated;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label lblWorkItemsCompleted;
		private Zamba.UsageControl.UsageControl usageThreadsInPool;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox4;
		private Zamba.UsageControl.UsageHistoryControl usageHistorySTP;
		private System.Diagnostics.PerformanceCounter pcActiveThreads;
		private System.Diagnostics.PerformanceCounter pcInUseThreads;
		private System.Diagnostics.PerformanceCounter pcQueuedWorkItems;
		private System.Diagnostics.PerformanceCounter pcCompletedWorkItems;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
            InitializeComponent();
            if (Boolean.Parse(UserPreferences.getValue("ThreadWithTrace", UserPreferences.Sections.WFMonitorPreferences, true)) == true)
            {
                Trace.Listeners.Add(new TextWriterTraceListener(System.Windows.Forms.Application.StartupPath + "\\Exceptions\\Trace Thread.Pool " + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss") + ".log"));
                Trace.AutoFlush = true;
            }
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
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
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblThreadsInPool = new System.Windows.Forms.Label();
            this.spinMaxThreads = new System.Windows.Forms.NumericUpDown();
            this.spinMinThreads = new System.Windows.Forms.NumericUpDown();
            this.spinInterval = new System.Windows.Forms.NumericUpDown();
            this.lblThreadInUse = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.timerPoll = new System.Windows.Forms.Timer(this.components);
            this.lblWaitingCallbacks = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblWorkItemsGenerated = new System.Windows.Forms.Label();
            this.lblWorkItemsCompleted = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.usageThreadsInPool = new Zamba.UsageControl.UsageControl();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.usageHistorySTP = new Zamba.UsageControl.UsageHistoryControl();
            this.pcActiveThreads = new System.Diagnostics.PerformanceCounter();
            this.pcInUseThreads = new System.Diagnostics.PerformanceCounter();
            this.pcQueuedWorkItems = new System.Diagnostics.PerformanceCounter();
            this.pcCompletedWorkItems = new System.Diagnostics.PerformanceCounter();
            ((System.ComponentModel.ISupportInitialize)(this.spinMaxThreads)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinMinThreads)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinInterval)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcActiveThreads)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcInUseThreads)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcQueuedWorkItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcCompletedWorkItems)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(432, 352);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(72, 24);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(520, 352);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(72, 24);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(104, 256);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Minimum Threads";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(104, 288);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "Maximum Threads";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(334, 260);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 24);
            this.label4.TabIndex = 5;
            this.label4.Text = "Interval (Miliseconds)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "In pool (Red)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblThreadsInPool
            // 
            this.lblThreadsInPool.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThreadsInPool.Location = new System.Drawing.Point(80, 16);
            this.lblThreadsInPool.Name = "lblThreadsInPool";
            this.lblThreadsInPool.Size = new System.Drawing.Size(80, 24);
            this.lblThreadsInPool.TabIndex = 11;
            this.lblThreadsInPool.Text = "XXXXXXXXX";
            this.lblThreadsInPool.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinMaxThreads
            // 
            this.spinMaxThreads.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.spinMaxThreads.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spinMaxThreads.Location = new System.Drawing.Point(8, 288);
            this.spinMaxThreads.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinMaxThreads.Name = "spinMaxThreads";
            this.spinMaxThreads.Size = new System.Drawing.Size(88, 29);
            this.spinMaxThreads.TabIndex = 14;
            this.spinMaxThreads.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.spinMaxThreads.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.spinMaxThreads.ValueChanged += new System.EventHandler(this.spinMaxThreads_ValueChanged);
            // 
            // spinMinThreads
            // 
            this.spinMinThreads.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.spinMinThreads.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spinMinThreads.Location = new System.Drawing.Point(8, 256);
            this.spinMinThreads.Name = "spinMinThreads";
            this.spinMinThreads.Size = new System.Drawing.Size(88, 29);
            this.spinMinThreads.TabIndex = 13;
            this.spinMinThreads.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.spinMinThreads.ValueChanged += new System.EventHandler(this.spinMinThreads_ValueChanged);
            // 
            // spinInterval
            // 
            this.spinInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.spinInterval.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spinInterval.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.spinInterval.Location = new System.Drawing.Point(240, 256);
            this.spinInterval.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.spinInterval.Name = "spinInterval";
            this.spinInterval.Size = new System.Drawing.Size(88, 29);
            this.spinInterval.TabIndex = 16;
            this.spinInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.spinInterval.Value = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            // 
            // lblThreadInUse
            // 
            this.lblThreadInUse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThreadInUse.Location = new System.Drawing.Point(80, 40);
            this.lblThreadInUse.Name = "lblThreadInUse";
            this.lblThreadInUse.Size = new System.Drawing.Size(80, 24);
            this.lblThreadInUse.TabIndex = 18;
            this.lblThreadInUse.Text = "XXXXXXXXX";
            this.lblThreadInUse.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 24);
            this.label7.TabIndex = 17;
            this.label7.Text = "Used (Green)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timerPoll
            // 
            this.timerPoll.Interval = 500;
            this.timerPoll.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblWaitingCallbacks
            // 
            this.lblWaitingCallbacks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWaitingCallbacks.Location = new System.Drawing.Point(64, 16);
            this.lblWaitingCallbacks.Name = "lblWaitingCallbacks";
            this.lblWaitingCallbacks.Size = new System.Drawing.Size(80, 24);
            this.lblWaitingCallbacks.TabIndex = 22;
            this.lblWaitingCallbacks.Text = "XXXXXXXXX";
            this.lblWaitingCallbacks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(8, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 24);
            this.label9.TabIndex = 21;
            this.label9.Text = "Queued";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(8, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 24);
            this.label8.TabIndex = 25;
            this.label8.Text = "Generated";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(8, 64);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 24);
            this.label10.TabIndex = 26;
            this.label10.Text = "Completed";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblWorkItemsGenerated
            // 
            this.lblWorkItemsGenerated.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWorkItemsGenerated.Location = new System.Drawing.Point(64, 40);
            this.lblWorkItemsGenerated.Name = "lblWorkItemsGenerated";
            this.lblWorkItemsGenerated.Size = new System.Drawing.Size(80, 24);
            this.lblWorkItemsGenerated.TabIndex = 27;
            this.lblWorkItemsGenerated.Text = "XXXXXXXXX";
            this.lblWorkItemsGenerated.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblWorkItemsCompleted
            // 
            this.lblWorkItemsCompleted.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWorkItemsCompleted.Location = new System.Drawing.Point(64, 64);
            this.lblWorkItemsCompleted.Name = "lblWorkItemsCompleted";
            this.lblWorkItemsCompleted.Size = new System.Drawing.Size(80, 24);
            this.lblWorkItemsCompleted.TabIndex = 28;
            this.lblWorkItemsCompleted.Text = "XXXXXXXXX";
            this.lblWorkItemsCompleted.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.lblWaitingCallbacks);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.lblWorkItemsGenerated);
            this.groupBox2.Controls.Add(this.lblWorkItemsCompleted);
            this.groupBox2.Location = new System.Drawing.Point(8, 144);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(152, 96);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Work items";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.lblThreadInUse);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.lblThreadsInPool);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(176, 144);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(168, 72);
            this.groupBox3.TabIndex = 34;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Threads";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.usageThreadsInPool);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(80, 128);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Usage";
            // 
            // usageThreadsInPool
            // 
            this.usageThreadsInPool.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.usageThreadsInPool.BackColor = System.Drawing.Color.Black;
            this.usageThreadsInPool.Location = new System.Drawing.Point(20, 16);
            this.usageThreadsInPool.Maximum = 25;
            this.usageThreadsInPool.Name = "usageThreadsInPool";
            this.usageThreadsInPool.Size = new System.Drawing.Size(41, 104);
            this.usageThreadsInPool.TabIndex = 37;
            this.usageThreadsInPool.Value1 = 1;
            this.usageThreadsInPool.Value2 = 24;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.usageHistorySTP);
            this.groupBox4.Location = new System.Drawing.Point(104, 8);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(494, 128);
            this.groupBox4.TabIndex = 36;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Usage History";
            // 
            // usageHistorySTP
            // 
            this.usageHistorySTP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.usageHistorySTP.BackColor = System.Drawing.Color.Black;
            this.usageHistorySTP.Location = new System.Drawing.Point(8, 16);
            this.usageHistorySTP.Maximum = 100;
            this.usageHistorySTP.Name = "usageHistorySTP";
            this.usageHistorySTP.Size = new System.Drawing.Size(480, 104);
            this.usageHistorySTP.TabIndex = 0;
            // 
            // pcActiveThreads
            // 
            this.pcActiveThreads.CategoryName = "Zamba.ThreadPool";
            this.pcActiveThreads.CounterName = "Active threads";
            this.pcActiveThreads.InstanceName = "Test Zamba.ThreadPool";
            // 
            // pcInUseThreads
            // 
            this.pcInUseThreads.CategoryName = "Zamba.ThreadPool";
            this.pcInUseThreads.CounterName = "In use threads";
            this.pcInUseThreads.InstanceName = "Test Zamba.ThreadPool";
            // 
            // pcQueuedWorkItems
            // 
            this.pcQueuedWorkItems.CategoryName = "Zamba.ThreadPool";
            this.pcQueuedWorkItems.CounterName = "Work Items in queue";
            this.pcQueuedWorkItems.InstanceName = "Test Zamba.ThreadPool";
            // 
            // pcCompletedWorkItems
            // 
            this.pcCompletedWorkItems.CategoryName = "Zamba.ThreadPool";
            this.pcCompletedWorkItems.CounterName = "Work Items processed";
            this.pcCompletedWorkItems.InstanceName = "Test Zamba.ThreadPool";
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(608, 382);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.spinInterval);
            this.Controls.Add(this.spinMaxThreads);
            this.Controls.Add(this.spinMinThreads);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(616, 416);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Monitor Thread Pool";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
            ((System.ComponentModel.ISupportInitialize)(this.spinMaxThreads)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinMinThreads)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinInterval)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcActiveThreads)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcInUseThreads)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcQueuedWorkItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcCompletedWorkItems)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{

            bool runApplication = InitializePerformanceCounters();
            
            if (!runApplication)
			{
			return;
			}

			Application.Run(new Form1());
		}

		// This method is a work around for the Peformance Counter issue.
		// When the first SmartThreadPool is created with a Peformance 
		// Counter name on a machine, it creates the SmartThreadPool 
		// Peformance Counter category. In this demo I use thes Performance 
		// Counters to update the GUI. 
		// The issue is that if this demo runs for the first time on the 
		// machine, it creates the Peformance Counter category and then 
		// uses it. 
		// I don't know why, but every time the demo runs for the first
		// time on a machine, it fails to connect to the Peformance Counters,
		// because it can't find the Peformance Counter category. 
		// The work around is to check if the category exists, and if not 
		// create a SmartThreadPool instance that will create the category.
		// After that I spawn another process that runs the demo.
		// I tried the another work around and thats to check for the category
		// existance, run a second process that will create the category,
		// and then continue with the first process, but it doesn't work.
		// Thank you for reading the whole comment. If you have another way
		// to solve this issue please contact me: amibar@gmail.com.
		private static bool InitializePerformanceCounters()
		{
			if (!PerformanceCounterCategory.Exists("Zamba.ThreadPool"))
			{
                Zamba.ThreadPool.STPStartInfo stpStartInfo = new Zamba.ThreadPool.STPStartInfo();
                stpStartInfo.PerformanceCounterInstanceName = "Test Zamba.ThreadPool";

				SmartThreadPool stp = new SmartThreadPool(stpStartInfo);
				stp.Shutdown();

                if (!PerformanceCounterCategory.Exists("Zamba.ThreadPool"))
				{
                    MessageBox.Show("Failed to create Performance Counters.", "Test Zamba.ThreadPool", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}

                System.Diagnostics.Process process = new System.Diagnostics.Process();
				process.StartInfo.FileName = Application.ExecutablePath;

				try
				{
					process.Start();
				}
				catch(Exception e)
				{
					e.GetHashCode();
                    MessageBox.Show("If this is the first time you get this message,\r\nplease try to run the demo again.", "Test Zamba.ThreadPool");
				}

				return false;
			}

			return true;
		}

	

		private void btnStart_Click(object sender, System.EventArgs e)
		{
            try
            {
                UpdateControls(true);
                TPS.WorkItemsCompleted = 0;
                TPS.WorkItemsGenerated = 10;

                StartThreadService();
            }
            catch (Exception ex)
            {
                Trace.WriteLineIf(ZTrace.IsInfo,ex.ToString());
            }
        }

       ThreadPoolService TPS = new ThreadPoolService();
        private void StartThreadService()
        {
            TPS.StartThreadService(this.spinMaxThreads.Value,this.spinMinThreads.Value,this.spinInterval.Value);
        }

		private void btnStop_Click(object sender, System.EventArgs e)
		{
            TPS.StopThreadService();
            UpdateControls(false);
        }

     

		private void Form1_Load(object sender, System.EventArgs e)
		{
			UpdateControls(false);
		}

        private void UpdateControls(bool start)
        {
            if (start == false)
            {
                this.spinMaxThreads.Value = Decimal.Parse(UserPreferences.getValue("MaxThreads", UserPreferences.Sections.InsertPreferences, "10"));
                this.spinMinThreads.Value = Decimal.Parse(UserPreferences.getValue("MinThreads", UserPreferences.Sections.InsertPreferences, "1"));
                this.spinInterval.Value = Decimal.Parse(UserPreferences.getValue("Interval", UserPreferences.Sections.InsertPreferences, "10000"));
            }
            else
            {
                UserPreferences.setValue("MaxThreads", this.spinMaxThreads.Value.ToString(),UserPreferences.Sections.InsertPreferences);
                UserPreferences.setValue("MinThreads", this.spinMinThreads.Value.ToString(),UserPreferences.Sections.InsertPreferences);
                UserPreferences.setValue("Interval", this.spinInterval.Value.ToString(),UserPreferences.Sections.InsertPreferences);
            }

            TPS.Running = start;
            spinMinThreads.Enabled = !start;
            spinMaxThreads.Enabled = !start;
            btnStart.Enabled = !start;

            btnStop.Enabled = start;
            timerPoll.Enabled = start;

            lblThreadInUse.Text = "0";
            lblThreadsInPool.Text = "0";
            lblWaitingCallbacks.Text = "0";

            usageThreadsInPool.Maximum = Convert.ToInt32(spinMaxThreads.Value);
            usageThreadsInPool.Value1 = 0;
            usageThreadsInPool.Value2 = 0;
            lblWorkItemsCompleted.Text = "0";
            lblWorkItemsGenerated.Text = "0";
            usageHistorySTP.Reset();
            usageHistorySTP.Maximum = usageThreadsInPool.Maximum;
        }

		private void spinMinThreads_ValueChanged(object sender, System.EventArgs e)
		{
			if (spinMinThreads.Value > spinMaxThreads.Value)
			{
				spinMaxThreads.Value = spinMinThreads.Value;
			}
		}

		private void spinMaxThreads_ValueChanged(object sender, System.EventArgs e)
		{
			if (spinMaxThreads.Value < spinMinThreads.Value)
			{
				spinMinThreads.Value = spinMaxThreads.Value;
			}
			usageThreadsInPool.Maximum = Convert.ToInt32(spinMaxThreads.Value);
		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{
            if (null == TPS.TPSSmartThreadPool)
			{
				return;
			}

			int threadsInUse = (int)pcInUseThreads.NextValue();
			int threadsInPool = (int)pcActiveThreads.NextValue();

			lblThreadInUse.Text = threadsInUse.ToString();
			lblThreadsInPool.Text = threadsInPool.ToString();
			lblWaitingCallbacks.Text = pcQueuedWorkItems.NextValue().ToString();  //stp.WaitingCallbacks.ToString();
			usageThreadsInPool.Value1 = threadsInUse;
			usageThreadsInPool.Value2 = threadsInPool;
			lblWorkItemsCompleted.Text = pcCompletedWorkItems.NextValue().ToString();
            lblWorkItemsGenerated.Text = TPS.WorkItemsGenerated.ToString();
			usageHistorySTP.AddValues(threadsInUse, threadsInPool);
		}

	

		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
            if (null != TPS.TPSSmartThreadPool)
			{
                TPS.TPSSmartThreadPool.Shutdown();
                TPS.TPSSmartThreadPool = null;
                TPS.TPSWorkItemsGroup = null;
			}
		}
	}
}
