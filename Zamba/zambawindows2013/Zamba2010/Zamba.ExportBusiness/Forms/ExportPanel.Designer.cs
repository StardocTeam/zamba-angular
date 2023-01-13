using Zamba.AppBlock;

namespace OutlookPanel
{
    partial class ExportPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportPanel));
            this.lblToExportDesc = new ZLabel();
            this.lblExportedDesc = new ZLabel();
            this.lblToExport = new ZLabel();
            this.lblExported = new ZLabel();
            this.dgvMailsToExport = new System.Windows.Forms.DataGridView();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.pnlExport = new System.Windows.Forms.Panel();
            this.lblErrorDesc = new ZLabel();
            this.lblError = new ZLabel();
            this.btnShowBar = new ZButton();
            this.btnHidePanel = new ZButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pctTitle = new System.Windows.Forms.PictureBox();
            this.pctBackground = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMailsToExport)).BeginInit();
            this.pnlTitle.SuspendLayout();
            this.pnlExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctBackground)).BeginInit();
            this.SuspendLayout();
            // 
            // lblToExportDesc
            // 
            this.lblToExportDesc.AutoSize = true;
            this.lblToExportDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToExportDesc.Location = new System.Drawing.Point(6, 8);
            this.lblToExportDesc.Name = "lblToExportDesc";
            this.lblToExportDesc.Size = new System.Drawing.Size(69, 15);
            this.lblToExportDesc.TabIndex = 9;
            this.lblToExportDesc.Text = "Pendientes";
            // 
            // lblExportedDesc
            // 
            this.lblExportedDesc.AutoSize = true;
            this.lblExportedDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExportedDesc.Location = new System.Drawing.Point(6, 24);
            this.lblExportedDesc.Name = "lblExportedDesc";
            this.lblExportedDesc.Size = new System.Drawing.Size(69, 15);
            this.lblExportedDesc.TabIndex = 10;
            this.lblExportedDesc.Text = "Exportados";
            // 
            // lblToExport
            // 
            this.lblToExport.AutoSize = true;
            this.lblToExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToExport.Location = new System.Drawing.Point(76, 8);
            this.lblToExport.Name = "lblToExport";
            this.lblToExport.Size = new System.Drawing.Size(14, 15);
            this.lblToExport.TabIndex = 11;
            this.lblToExport.Text = "0";
            // 
            // lblExported
            // 
            this.lblExported.AutoSize = true;
            this.lblExported.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExported.Location = new System.Drawing.Point(76, 25);
            this.lblExported.Name = "lblExported";
            this.lblExported.Size = new System.Drawing.Size(14, 15);
            this.lblExported.TabIndex = 12;
            this.lblExported.Text = "0";
            // 
            // dgvMailsToExport
            // 
            this.dgvMailsToExport.AllowUserToAddRows = false;
            this.dgvMailsToExport.AllowUserToDeleteRows = false;
            this.dgvMailsToExport.AllowUserToResizeColumns = false;
            this.dgvMailsToExport.AllowUserToResizeRows = false;
            this.dgvMailsToExport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMailsToExport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMailsToExport.BackgroundColor = System.Drawing.Color.White;
            this.dgvMailsToExport.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = Zamba.AppBlock.ZambaUIHelpers.GetFontsColor();
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMailsToExport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMailsToExport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = Zamba.AppBlock.ZambaUIHelpers.GetFontsColor();
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = Zamba.AppBlock.ZambaUIHelpers.GetFontsColor();
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMailsToExport.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMailsToExport.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvMailsToExport.EnableHeadersVisualStyles = false;
            this.dgvMailsToExport.Location = new System.Drawing.Point(0, 63);
            this.dgvMailsToExport.Name = "dgvMailsToExport";
            this.dgvMailsToExport.ReadOnly = true;
            this.dgvMailsToExport.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = Zamba.AppBlock.ZambaUIHelpers.GetFontsColor();
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMailsToExport.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvMailsToExport.RowHeadersVisible = false;
            this.dgvMailsToExport.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvMailsToExport.ShowCellErrors = false;
            this.dgvMailsToExport.ShowEditingIcon = false;
            this.dgvMailsToExport.ShowRowErrors = false;
            this.dgvMailsToExport.Size = new System.Drawing.Size(210, 285);
            this.dgvMailsToExport.TabIndex = 14;
            // 
            // pnlTitle
            // 
            this.pnlTitle.BackColor = System.Drawing.Color.MidnightBlue;
            this.pnlTitle.Controls.Add(this.pictureBox1);
            this.pnlTitle.Controls.Add(this.pctTitle);
            this.pnlTitle.Controls.Add(this.pctBackground);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(15, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(210, 26);
            this.pnlTitle.TabIndex = 19;
            // 
            // pnlExport
            // 
            this.pnlExport.Controls.Add(this.lblErrorDesc);
            this.pnlExport.Controls.Add(this.lblError);
            this.pnlExport.Controls.Add(this.btnHidePanel);
            this.pnlExport.Controls.Add(this.lblToExportDesc);
            this.pnlExport.Controls.Add(this.lblExportedDesc);
            this.pnlExport.Controls.Add(this.lblToExport);
            this.pnlExport.Controls.Add(this.dgvMailsToExport);
            this.pnlExport.Controls.Add(this.lblExported);
            this.pnlExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlExport.Location = new System.Drawing.Point(15, 26);
            this.pnlExport.Name = "pnlExport";
            this.pnlExport.Size = new System.Drawing.Size(210, 347);
            this.pnlExport.TabIndex = 21;
            // 
            // lblErrorDesc
            // 
            this.lblErrorDesc.AutoSize = true;
            this.lblErrorDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorDesc.Location = new System.Drawing.Point(6, 40);
            this.lblErrorDesc.Name = "lblErrorDesc";
            this.lblErrorDesc.Size = new System.Drawing.Size(58, 15);
            this.lblErrorDesc.TabIndex = 16;
            this.lblErrorDesc.Text = "Con error";
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.Location = new System.Drawing.Point(76, 41);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(14, 15);
            this.lblError.TabIndex = 17;
            this.lblError.Text = "0";
            // 
            // btnShowBar
            // 
            this.btnShowBar.AutoSize = true;
            this.btnShowBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnShowBar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShowBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnShowBar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowBar.Location = new System.Drawing.Point(0, 0);
            this.btnShowBar.Name = "btnShowBar";
            this.btnShowBar.Size = new System.Drawing.Size(15, 373);
            this.btnShowBar.TabIndex = 20;
            this.btnShowBar.UseVisualStyleBackColor = true;
            this.btnShowBar.Visible = false;
            this.btnShowBar.Click += new System.EventHandler(this.btnShowBar_Click);
            // 
            // btnHidePanel
            // 
            this.btnHidePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHidePanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHidePanel.ForeColor = System.Drawing.Color.White;
            this.btnHidePanel.Image = global::Zamba.ExportBusiness.Properties.Resources.right_round_48;
            this.btnHidePanel.Location = new System.Drawing.Point(145, 8);
            this.btnHidePanel.Name = "btnHidePanel";
            this.btnHidePanel.Size = new System.Drawing.Size(56, 49);
            this.btnHidePanel.TabIndex = 15;
            this.btnHidePanel.UseVisualStyleBackColor = true;
            this.btnHidePanel.Click += new System.EventHandler(this.btnHidePanel_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::Zamba.ExportBusiness.Properties.Resources.favicon_16x163;
            this.pictureBox1.Location = new System.Drawing.Point(0, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pctTitle
            // 
            this.pctTitle.Image = ((System.Drawing.Image)(resources.GetObject("pctTitle.Image")));
            this.pctTitle.Location = new System.Drawing.Point(27, 1);
            this.pctTitle.Name = "pctTitle";
            this.pctTitle.Size = new System.Drawing.Size(180, 23);
            this.pctTitle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pctTitle.TabIndex = 18;
            this.pctTitle.TabStop = false;
            // 
            // pctBackground
            // 
            this.pctBackground.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pctBackground.Image = ((System.Drawing.Image)(resources.GetObject("pctBackground.Image")));
            this.pctBackground.Location = new System.Drawing.Point(-13, 1);
            this.pctBackground.Name = "pctBackground";
            this.pctBackground.Size = new System.Drawing.Size(221, 23);
            this.pctBackground.TabIndex = 20;
            this.pctBackground.TabStop = false;
            // 
            // ExportPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlExport);
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.btnShowBar);
            this.Name = "ExportPanel";
            this.Size = new System.Drawing.Size(225, 373);
            this.Load += new System.EventHandler(this.DockedControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMailsToExport)).EndInit();
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.pnlExport.ResumeLayout(false);
            this.pnlExport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctBackground)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZLabel lblToExportDesc;
        private ZLabel lblExportedDesc;
        private ZLabel lblToExport;
        private ZLabel lblExported;
        private System.Windows.Forms.DataGridView dgvMailsToExport;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.PictureBox pctTitle;
        private System.Windows.Forms.PictureBox pctBackground;
        internal ZButton btnShowBar;
        private System.Windows.Forms.Panel pnlExport;
        private ZButton btnHidePanel;
        private ZLabel lblErrorDesc;
        private ZLabel lblError;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}