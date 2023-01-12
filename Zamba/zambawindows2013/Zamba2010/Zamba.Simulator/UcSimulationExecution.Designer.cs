using Zamba.AppBlock;

namespace Zamba.Simulator
{
    partial class UcSimulationExecution
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.lblSimulation = new ZLabel();
            this.simulationsRepeater = new Microsoft.VisualBasic.PowerPacks.DataRepeater();
            this.imgState = new System.Windows.Forms.PictureBox();
            this.label4 = new ZLabel();
            this.lblDictionary = new ZLabel();
            this.label2 = new ZLabel();
            this.lblProcess = new ZLabel();
            this.lblResult = new ZLabel();
            this.btnPause = new ZButton();
            this.btnStop = new ZButton();
            this.btnPlay = new ZButton();
            this.btnClose = new ZButton();
            this.txtHistory = new System.Windows.Forms.TextBox();
            this.btnDebug = new ZButton();
          UcSimulationExecution.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.simulationsRepeater.ItemTemplate.SuspendLayout();
            this.simulationsRepeater.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgState)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSimulation
            // 
            this.lblSimulation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSimulation.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSimulation.Location = new System.Drawing.Point(174, 12);
            this.lblSimulation.Name = "lblSimulation";
            this.lblSimulation.Size = new System.Drawing.Size(592, 31);
            this.lblSimulation.TabIndex = 21;
            this.lblSimulation.Text = "Simulación:";
            // 
            // simulationsRepeater
            // 
            this.simulationsRepeater.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            // 
            // simulationsRepeater.ItemTemplate
            // 
            this.simulationsRepeater.ItemTemplate.Controls.Add(this.imgState);
            this.simulationsRepeater.ItemTemplate.Controls.Add(this.label4);
            this.simulationsRepeater.ItemTemplate.Controls.Add(this.lblDictionary);
            this.simulationsRepeater.ItemTemplate.Controls.Add(this.label2);
            this.simulationsRepeater.ItemTemplate.Controls.Add(this.lblProcess);
            this.simulationsRepeater.ItemTemplate.Size = new System.Drawing.Size(326, 69);
            this.simulationsRepeater.Location = new System.Drawing.Point(12, 56);
            this.simulationsRepeater.Name = "simulationsRepeater";
            this.simulationsRepeater.Size = new System.Drawing.Size(334, 390);
            this.simulationsRepeater.TabIndex = 22;
            this.simulationsRepeater.VirtualMode = true;
            this.simulationsRepeater.ItemValueNeeded += new Microsoft.VisualBasic.PowerPacks.DataRepeaterItemValueEventHandler(this.simulationsRepeater_ItemValueNeeded);
            // 
            // imgState
            // 
            this.imgState.BackgroundImage = global::Zamba.Simulator.Properties.Resources.pause_64;
            this.imgState.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imgState.Location = new System.Drawing.Point(3, 18);
            this.imgState.Name = "imgState";
            this.imgState.Size = new System.Drawing.Size(32, 32);
            this.imgState.TabIndex = 31;
            this.imgState.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(41, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 16);
            this.label4.TabIndex = 30;
            this.label4.Text = "Diccionario:";
            // 
            // lblDictionary
            // 
            this.lblDictionary.AutoSize = true;
            this.lblDictionary.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDictionary.Location = new System.Drawing.Point(41, 48);
            this.lblDictionary.Name = "lblDictionary";
            this.lblDictionary.Size = new System.Drawing.Size(102, 16);
            this.lblDictionary.TabIndex = 29;
            this.lblDictionary.Text = "[DICCIONARIO]";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(41, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 16);
            this.label2.TabIndex = 28;
            this.label2.Text = "Proceso:";
            // 
            // lblProcess
            // 
            this.lblProcess.AutoSize = true;
            this.lblProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcess.Location = new System.Drawing.Point(41, 16);
            this.lblProcess.Name = "lblProcess";
            this.lblProcess.Size = new System.Drawing.Size(82, 16);
            this.lblProcess.TabIndex = 0;
            this.lblProcess.Text = "[PROCESO]";
            // 
            // lblResult
            // 
            this.lblResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult.Location = new System.Drawing.Point(353, 421);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(535, 25);
            this.lblResult.TabIndex = 34;
            this.lblResult.Text = "Resultado:";
            // 
            // btnPause
            // 
            this.btnPause.BackgroundImage = global::Zamba.Simulator.Properties.Resources.pause_64;
            this.btnPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPause.Enabled = false;
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPause.ForeColor = System.Drawing.Color.Transparent;
            this.btnPause.Location = new System.Drawing.Point(50, 10);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(32, 32);
            this.btnPause.TabIndex = 37;
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackgroundImage = global::Zamba.Simulator.Properties.Resources.stop_64;
            this.btnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnStop.Enabled = false;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.ForeColor = System.Drawing.Color.Transparent;
            this.btnStop.Location = new System.Drawing.Point(88, 10);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(32, 32);
            this.btnStop.TabIndex = 36;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.BackgroundImage = global::Zamba.Simulator.Properties.Resources.play_64;
            this.btnPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.ForeColor = System.Drawing.Color.Transparent;
            this.btnPlay.Location = new System.Drawing.Point(12, 10);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(32, 32);
            this.btnPlay.TabIndex = 35;
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackgroundImage = global::Zamba.Simulator.Properties.Resources.close_window_64;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.Transparent;
            this.btnClose.Location = new System.Drawing.Point(772, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(30, 30);
            this.btnClose.TabIndex = 16;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtHistory
            // 
            this.txtHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHistory.Location = new System.Drawing.Point(358, 56);
            this.txtHistory.Multiline = true;
            this.txtHistory.Name = "txtHistory";
            this.txtHistory.Size = new System.Drawing.Size(444, 362);
            this.txtHistory.TabIndex = 23;
            // 
            // btnDebug
            // 
            this.btnDebug.BackgroundImage = global::Zamba.Simulator.Properties.Resources.bug_64;
            this.btnDebug.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDebug.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDebug.ForeColor = System.Drawing.Color.Transparent;
            this.btnDebug.Location = new System.Drawing.Point(126, 10);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(32, 32);
            this.btnDebug.TabIndex = 38;
            this.btnDebug.UseVisualStyleBackColor = true;
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // bgWorker
            // 
            UcSimulationExecution.bgWorker.WorkerReportsProgress = true;
            UcSimulationExecution.bgWorker.WorkerSupportsCancellation = true;
            UcSimulationExecution.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            UcSimulationExecution.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
            UcSimulationExecution.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // UcSimulationExecution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnDebug);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.txtHistory);
            this.Controls.Add(this.simulationsRepeater);
            this.Controls.Add(this.lblSimulation);
            this.Controls.Add(this.btnClose);
            this.Name = "UcSimulationExecution";
            this.Size = new System.Drawing.Size(814, 458);
            this.Load += new System.EventHandler(this.FrmSimulationExecution_Load);
            this.simulationsRepeater.ItemTemplate.ResumeLayout(false);
            this.simulationsRepeater.ItemTemplate.PerformLayout();
            this.simulationsRepeater.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgState)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZButton btnClose;
        private ZLabel lblSimulation;
        private Microsoft.VisualBasic.PowerPacks.DataRepeater simulationsRepeater;
        private ZLabel lblProcess;
        private ZLabel label4;
        private ZLabel lblDictionary;
        private ZLabel label2;
        private System.Windows.Forms.PictureBox imgState;
        private ZLabel lblResult;
        private ZButton btnPlay;
        private ZButton btnStop;
        private ZButton btnPause;
        private System.Windows.Forms.TextBox txtHistory;
        private ZButton btnDebug;
        public static System.ComponentModel.BackgroundWorker bgWorker;
    }
}