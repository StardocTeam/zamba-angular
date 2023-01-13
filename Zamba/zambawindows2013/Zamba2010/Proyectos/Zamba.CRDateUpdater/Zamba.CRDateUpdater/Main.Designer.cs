namespace Zamba.CRDateUpdater
{
    partial class Main
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
            this.btnStart = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.rdbAllDT = new System.Windows.Forms.RadioButton();
            this.rdbSelectDT = new System.Windows.Forms.RadioButton();
            this.grbType = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIds = new System.Windows.Forms.TextBox();
            this.rdbEditIDs = new System.Windows.Forms.RadioButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grbConf = new System.Windows.Forms.GroupBox();
            this.chkOnlyNull = new System.Windows.Forms.CheckBox();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnView = new System.Windows.Forms.Button();
            this.txtDocIDCol = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDateCol = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTableCol = new System.Windows.Forms.TextBox();
            this.chklstDoctypes = new System.Windows.Forms.CheckedListBox();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.grbType.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grbConf.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(122, 398);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Comenzar";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(268, 398);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Cerrar";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // rdbAllDT
            // 
            this.rdbAllDT.AutoSize = true;
            this.rdbAllDT.Checked = true;
            this.rdbAllDT.Location = new System.Drawing.Point(16, 19);
            this.rdbAllDT.Name = "rdbAllDT";
            this.rdbAllDT.Size = new System.Drawing.Size(167, 17);
            this.rdbAllDT.TabIndex = 3;
            this.rdbAllDT.TabStop = true;
            this.rdbAllDT.Text = "Todos los tipos de documento";
            this.rdbAllDT.UseVisualStyleBackColor = true;
            this.rdbAllDT.CheckedChanged += new System.EventHandler(this.rdbAllDT_CheckedChanged);
            // 
            // rdbSelectDT
            // 
            this.rdbSelectDT.AutoSize = true;
            this.rdbSelectDT.Location = new System.Drawing.Point(190, 19);
            this.rdbSelectDT.Name = "rdbSelectDT";
            this.rdbSelectDT.Size = new System.Drawing.Size(100, 17);
            this.rdbSelectDT.TabIndex = 4;
            this.rdbSelectDT.Text = "Seleccionar IDs";
            this.rdbSelectDT.UseVisualStyleBackColor = true;
            this.rdbSelectDT.CheckedChanged += new System.EventHandler(this.rdbSelectDT_CheckedChanged);
            // 
            // grbType
            // 
            this.grbType.Controls.Add(this.label4);
            this.grbType.Controls.Add(this.txtIds);
            this.grbType.Controls.Add(this.rdbEditIDs);
            this.grbType.Controls.Add(this.rdbAllDT);
            this.grbType.Controls.Add(this.rdbSelectDT);
            this.grbType.Location = new System.Drawing.Point(12, 12);
            this.grbType.Name = "grbType";
            this.grbType.Size = new System.Drawing.Size(456, 69);
            this.grbType.TabIndex = 5;
            this.grbType.TabStop = false;
            this.grbType.Text = "Tipo de actualizacion";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Ids:";
            // 
            // txtIds
            // 
            this.txtIds.Enabled = false;
            this.txtIds.Location = new System.Drawing.Point(37, 42);
            this.txtIds.Name = "txtIds";
            this.txtIds.Size = new System.Drawing.Size(398, 20);
            this.txtIds.TabIndex = 6;
            // 
            // rdbEditIDs
            // 
            this.rdbEditIDs.AutoSize = true;
            this.rdbEditIDs.Location = new System.Drawing.Point(296, 19);
            this.rdbEditIDs.Name = "rdbEditIDs";
            this.rdbEditIDs.Size = new System.Drawing.Size(145, 17);
            this.rdbEditIDs.TabIndex = 5;
            this.rdbEditIDs.Text = "Especificar ids por comas";
            this.rdbEditIDs.UseVisualStyleBackColor = true;
            this.rdbEditIDs.CheckedChanged += new System.EventHandler(this.rdbEditIDs_CheckedChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grbConf);
            this.splitContainer1.Panel1.Controls.Add(this.grbType);
            this.splitContainer1.Panel1MinSize = 160;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.chklstDoctypes);
            this.splitContainer1.Size = new System.Drawing.Size(481, 392);
            this.splitContainer1.SplitterDistance = 231;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 6;
            // 
            // grbConf
            // 
            this.grbConf.Controls.Add(this.chkOnlyNull);
            this.grbConf.Controls.Add(this.dtpHasta);
            this.grbConf.Controls.Add(this.dtpDesde);
            this.grbConf.Controls.Add(this.label6);
            this.grbConf.Controls.Add(this.label5);
            this.grbConf.Controls.Add(this.btnView);
            this.grbConf.Controls.Add(this.txtDocIDCol);
            this.grbConf.Controls.Add(this.label3);
            this.grbConf.Controls.Add(this.txtDateCol);
            this.grbConf.Controls.Add(this.label2);
            this.grbConf.Controls.Add(this.label1);
            this.grbConf.Controls.Add(this.txtTableCol);
            this.grbConf.Location = new System.Drawing.Point(12, 87);
            this.grbConf.Name = "grbConf";
            this.grbConf.Size = new System.Drawing.Size(456, 139);
            this.grbConf.TabIndex = 6;
            this.grbConf.TabStop = false;
            this.grbConf.Text = "Configuracion de tabla a obtener fechas";
            // 
            // chkOnlyNull
            // 
            this.chkOnlyNull.AutoSize = true;
            this.chkOnlyNull.Checked = true;
            this.chkOnlyNull.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOnlyNull.Location = new System.Drawing.Point(235, 77);
            this.chkOnlyNull.Name = "chkOnlyNull";
            this.chkOnlyNull.Size = new System.Drawing.Size(155, 17);
            this.chkOnlyNull.TabIndex = 15;
            this.chkOnlyNull.Text = "Solo aplicar a valores nulos";
            this.chkOnlyNull.UseVisualStyleBackColor = true;
            // 
            // dtpHasta
            // 
            this.dtpHasta.CustomFormat = "dd/MM/yyyy";
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(306, 46);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(116, 20);
            this.dtpHasta.TabIndex = 14;
            // 
            // dtpDesde
            // 
            this.dtpDesde.CustomFormat = "dd/MM/yyyy";
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(306, 18);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(116, 20);
            this.dtpDesde.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(232, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Hasta fecha:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(232, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Desde fecha:";
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(180, 107);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 6;
            this.btnView.Text = "Ver rango";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // txtDocIDCol
            // 
            this.txtDocIDCol.Location = new System.Drawing.Point(108, 75);
            this.txtDocIDCol.Name = "txtDocIDCol";
            this.txtDocIDCol.Size = new System.Drawing.Size(110, 20);
            this.txtDocIDCol.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Columna del doc id:";
            // 
            // txtDateCol
            // 
            this.txtDateCol.Location = new System.Drawing.Point(118, 45);
            this.txtDateCol.Name = "txtDateCol";
            this.txtDateCol.Size = new System.Drawing.Size(100, 20);
            this.txtDateCol.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Columna de la fecha:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nombre de la tabla:";
            // 
            // txtTableCol
            // 
            this.txtTableCol.Location = new System.Drawing.Point(108, 19);
            this.txtTableCol.Name = "txtTableCol";
            this.txtTableCol.Size = new System.Drawing.Size(110, 20);
            this.txtTableCol.TabIndex = 0;
            // 
            // chklstDoctypes
            // 
            this.chklstDoctypes.BackColor = System.Drawing.Color.Silver;
            this.chklstDoctypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chklstDoctypes.ForeColor = System.Drawing.Color.Black;
            this.chklstDoctypes.FormattingEnabled = true;
            this.chklstDoctypes.HorizontalScrollbar = true;
            this.chklstDoctypes.Location = new System.Drawing.Point(0, 0);
            this.chklstDoctypes.Name = "chklstDoctypes";
            this.chklstDoctypes.Size = new System.Drawing.Size(481, 154);
            this.chklstDoctypes.TabIndex = 1;
            this.chklstDoctypes.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chklstDoctypes_MouseUp);
            this.chklstDoctypes.KeyUp += new System.Windows.Forms.KeyEventHandler(this.chklstDoctypes_KeyUp);
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 426);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip1.Size = new System.Drawing.Size(480, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.Stretch = false;
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 448);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Actualizador de CRDate";
            this.grbType.ResumeLayout(false);
            this.grbType.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.grbConf.ResumeLayout(false);
            this.grbConf.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.RadioButton rdbAllDT;
        private System.Windows.Forms.RadioButton rdbSelectDT;
        private System.Windows.Forms.GroupBox grbType;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RadioButton rdbEditIDs;
        private System.Windows.Forms.TextBox txtIds;
        private System.Windows.Forms.CheckedListBox chklstDoctypes;
        private System.Windows.Forms.GroupBox grbConf;
        private System.Windows.Forms.TextBox txtTableCol;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDateCol;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDocIDCol;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.CheckBox chkOnlyNull;
    }
}

