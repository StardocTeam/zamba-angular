namespace ZItoDOC_ID
{
    partial class frmZItoDOCIDProcess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmZItoDOCIDProcess));
            this.btnBeginProcess = new System.Windows.Forms.Button();
            this.btnCancelProcess = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.lstErrors = new System.Windows.Forms.ListBox();
            this.btnSaveError = new System.Windows.Forms.Button();
            this.lblProgreso = new System.Windows.Forms.Label();
            this.lnkErrors = new System.Windows.Forms.LinkLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbUpdate = new System.Windows.Forms.TabPage();
            this.tbUpdated = new System.Windows.Forms.TabPage();
            this.lstUpdatedItems = new System.Windows.Forms.ListBox();
            this.btnSaveUpdates = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tbUpdate.SuspendLayout();
            this.tbUpdated.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBeginProcess
            // 
            this.btnBeginProcess.BackColor = System.Drawing.Color.Honeydew;
            this.btnBeginProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBeginProcess.ForeColor = System.Drawing.Color.Black;
            this.btnBeginProcess.Location = new System.Drawing.Point(55, 120);
            this.btnBeginProcess.Name = "btnBeginProcess";
            this.btnBeginProcess.Size = new System.Drawing.Size(166, 26);
            this.btnBeginProcess.TabIndex = 0;
            this.btnBeginProcess.Text = "COMENZAR";
            this.btnBeginProcess.UseVisualStyleBackColor = false;
            this.btnBeginProcess.Click += new System.EventHandler(this.btnBeginProcess_Click);
            // 
            // btnCancelProcess
            // 
            this.btnCancelProcess.BackColor = System.Drawing.Color.Honeydew;
            this.btnCancelProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelProcess.ForeColor = System.Drawing.Color.Black;
            this.btnCancelProcess.Location = new System.Drawing.Point(435, 120);
            this.btnCancelProcess.Name = "btnCancelProcess";
            this.btnCancelProcess.Size = new System.Drawing.Size(166, 26);
            this.btnCancelProcess.TabIndex = 1;
            this.btnCancelProcess.Text = "CANCELAR";
            this.btnCancelProcess.UseVisualStyleBackColor = false;
            this.btnCancelProcess.Click += new System.EventHandler(this.btnCancelProcess_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.SystemColors.Control;
            this.progressBar1.Location = new System.Drawing.Point(6, 45);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(644, 42);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Honeydew;
            this.label1.Location = new System.Drawing.Point(6, 194);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Listado de Errores";
            // 
            // lstErrors
            // 
            this.lstErrors.BackColor = System.Drawing.Color.GhostWhite;
            this.lstErrors.FormattingEnabled = true;
            this.lstErrors.Location = new System.Drawing.Point(9, 210);
            this.lstErrors.Name = "lstErrors";
            this.lstErrors.Size = new System.Drawing.Size(613, 199);
            this.lstErrors.TabIndex = 6;
            // 
            // btnSaveError
            // 
            this.btnSaveError.BackColor = System.Drawing.Color.Honeydew;
            this.btnSaveError.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveError.ForeColor = System.Drawing.Color.Black;
            this.btnSaveError.Location = new System.Drawing.Point(627, 210);
            this.btnSaveError.Name = "btnSaveError";
            this.btnSaveError.Size = new System.Drawing.Size(22, 199);
            this.btnSaveError.TabIndex = 8;
            this.btnSaveError.Tag = "Le permite guardar el listado de errores en la PC";
            this.btnSaveError.Text = "Guardar";
            this.btnSaveError.UseCompatibleTextRendering = true;
            this.btnSaveError.UseVisualStyleBackColor = false;
            this.btnSaveError.Click += new System.EventHandler(this.btnSaveError_Click);
            // 
            // lblProgreso
            // 
            this.lblProgreso.AutoSize = true;
            this.lblProgreso.BackColor = System.Drawing.Color.Honeydew;
            this.lblProgreso.Location = new System.Drawing.Point(491, 90);
            this.lblProgreso.Name = "lblProgreso";
            this.lblProgreso.Size = new System.Drawing.Size(49, 13);
            this.lblProgreso.TabIndex = 9;
            this.lblProgreso.Text = "Progreso";
            // 
            // lnkErrors
            // 
            this.lnkErrors.AutoSize = true;
            this.lnkErrors.BackColor = System.Drawing.Color.Honeydew;
            this.lnkErrors.Location = new System.Drawing.Point(6, 166);
            this.lnkErrors.Name = "lnkErrors";
            this.lnkErrors.Size = new System.Drawing.Size(55, 13);
            this.lnkErrors.TabIndex = 10;
            this.lnkErrors.TabStop = true;
            this.lnkErrors.Text = "Errores >>";
            this.lnkErrors.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkErrors_LinkClicked);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbUpdate);
            this.tabControl1.Controls.Add(this.tbUpdated);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(671, 469);
            this.tabControl1.TabIndex = 11;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tbUpdate
            // 
            this.tbUpdate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbUpdate.BackgroundImage")));
            this.tbUpdate.Controls.Add(this.progressBar1);
            this.tbUpdate.Controls.Add(this.lnkErrors);
            this.tbUpdate.Controls.Add(this.btnSaveError);
            this.tbUpdate.Controls.Add(this.btnBeginProcess);
            this.tbUpdate.Controls.Add(this.label1);
            this.tbUpdate.Controls.Add(this.lblProgreso);
            this.tbUpdate.Controls.Add(this.lstErrors);
            this.tbUpdate.Controls.Add(this.btnCancelProcess);
            this.tbUpdate.Location = new System.Drawing.Point(4, 22);
            this.tbUpdate.Name = "tbUpdate";
            this.tbUpdate.Padding = new System.Windows.Forms.Padding(3);
            this.tbUpdate.Size = new System.Drawing.Size(663, 443);
            this.tbUpdate.TabIndex = 0;
            this.tbUpdate.Text = "Actualización";
            this.tbUpdate.UseVisualStyleBackColor = true;
            // 
            // tbUpdated
            // 
            this.tbUpdated.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tbUpdated.BackgroundImage")));
            this.tbUpdated.Controls.Add(this.btnSaveUpdates);
            this.tbUpdated.Controls.Add(this.lstUpdatedItems);
            this.tbUpdated.Location = new System.Drawing.Point(4, 22);
            this.tbUpdated.Name = "tbUpdated";
            this.tbUpdated.Padding = new System.Windows.Forms.Padding(3);
            this.tbUpdated.Size = new System.Drawing.Size(663, 443);
            this.tbUpdated.TabIndex = 1;
            this.tbUpdated.Text = "Actualizados";
            this.tbUpdated.UseVisualStyleBackColor = true;
            // 
            // lstUpdatedItems
            // 
            this.lstUpdatedItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstUpdatedItems.FormattingEnabled = true;
            this.lstUpdatedItems.Location = new System.Drawing.Point(6, 20);
            this.lstUpdatedItems.Name = "lstUpdatedItems";
            this.lstUpdatedItems.Size = new System.Drawing.Size(651, 368);
            this.lstUpdatedItems.TabIndex = 0;
            // 
            // btnSaveUpdates
            // 
            this.btnSaveUpdates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveUpdates.BackColor = System.Drawing.Color.Honeydew;
            this.btnSaveUpdates.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveUpdates.Location = new System.Drawing.Point(547, 403);
            this.btnSaveUpdates.Name = "btnSaveUpdates";
            this.btnSaveUpdates.Size = new System.Drawing.Size(75, 23);
            this.btnSaveUpdates.TabIndex = 1;
            this.btnSaveUpdates.Text = "Guardar";
            this.btnSaveUpdates.UseVisualStyleBackColor = false;
            // 
            // frmZItoDOCIDProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(671, 469);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmZItoDOCIDProcess";
            this.Opacity = 0.95;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZI to DOC ID";
            this.Load += new System.EventHandler(this.frmZItoDOCIDProcess_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmZItoDOCIDProcess_FormClosed);
            this.tabControl1.ResumeLayout(false);
            this.tbUpdate.ResumeLayout(false);
            this.tbUpdate.PerformLayout();
            this.tbUpdated.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBeginProcess;
        private System.Windows.Forms.Button btnCancelProcess;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstErrors;
        private System.Windows.Forms.Button btnSaveError;
        private System.Windows.Forms.Label lblProgreso;
        private System.Windows.Forms.LinkLabel lnkErrors;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbUpdate;
        private System.Windows.Forms.TabPage tbUpdated;
        private System.Windows.Forms.Button btnSaveUpdates;
        private System.Windows.Forms.ListBox lstUpdatedItems;
    }
}

