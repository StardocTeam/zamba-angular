using Zamba.AppBlock;

namespace Zamba.Shapes.Views
{
    partial class UcExportControl
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
            this.chkHome = new System.Windows.Forms.CheckBox();
            this.chkActors = new System.Windows.Forms.CheckBox();
            this.chkWorkflows = new System.Windows.Forms.CheckBox();
            this.chkReports = new System.Windows.Forms.CheckBox();
            this.lbltitle = new ZLabel();
            this.btnExport = new ZButton();
            this.chkInterfaces = new System.Windows.Forms.CheckBox();
            this.btnLoadWFs = new ZButton();
            this.chkEntities = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkHome
            // 
            this.chkHome.AutoSize = true;
            this.chkHome.Location = new System.Drawing.Point(12, 37);
            this.chkHome.Name = "chkHome";
            this.chkHome.Size = new System.Drawing.Size(15, 14);
            this.chkHome.TabIndex = 0;
            this.chkHome.UseVisualStyleBackColor = true;
            // 
            // chkActors
            // 
            this.chkActors.AutoSize = true;
            this.chkActors.Location = new System.Drawing.Point(12, 60);
            this.chkActors.Name = "chkActors";
            this.chkActors.Size = new System.Drawing.Size(15, 14);
            this.chkActors.TabIndex = 1;
            this.chkActors.UseVisualStyleBackColor = true;
            // 
            // chkWorkflows
            // 
            this.chkWorkflows.AutoSize = true;
            this.chkWorkflows.Location = new System.Drawing.Point(12, 80);
            this.chkWorkflows.Name = "chkWorkflows";
            this.chkWorkflows.Size = new System.Drawing.Size(15, 14);
            this.chkWorkflows.TabIndex = 2;
            this.chkWorkflows.UseVisualStyleBackColor = true;
            // 
            // chkReports
            // 
            this.chkReports.AutoSize = true;
            this.chkReports.Location = new System.Drawing.Point(12, 150);
            this.chkReports.Name = "chkReports";
            this.chkReports.Size = new System.Drawing.Size(15, 14);
            this.chkReports.TabIndex = 3;
            this.chkReports.UseVisualStyleBackColor = true;
            // 
            // lbltitle
            // 
            this.lbltitle.AutoSize = true;
            this.lbltitle.Location = new System.Drawing.Point(9, 9);
            this.lbltitle.Name = "lbltitle";
            this.lbltitle.Size = new System.Drawing.Size(169, 13);
            this.lbltitle.TabIndex = 4;
            this.lbltitle.Text = "Seleccione los graficos a exportar:";
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(275, 185);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(78, 23);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "Exportar";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // chkInterfaces
            // 
            this.chkInterfaces.AutoSize = true;
            this.chkInterfaces.Location = new System.Drawing.Point(12, 127);
            this.chkInterfaces.Name = "chkInterfaces";
            this.chkInterfaces.Size = new System.Drawing.Size(15, 14);
            this.chkInterfaces.TabIndex = 6;
            this.chkInterfaces.UseVisualStyleBackColor = true;
            // 
            // btnLoadWFs
            // 
            this.btnLoadWFs.Location = new System.Drawing.Point(30, 99);
            this.btnLoadWFs.Name = "btnLoadWFs";
            this.btnLoadWFs.Size = new System.Drawing.Size(100, 21);
            this.btnLoadWFs.TabIndex = 8;
            this.btnLoadWFs.Text = "Cargar Workflows";
            this.btnLoadWFs.UseVisualStyleBackColor = true;
            this.btnLoadWFs.Click += new System.EventHandler(this.btnLoadWFs_Click);
            // 
            // chkEntities
            // 
            this.chkEntities.AutoSize = true;
            this.chkEntities.Location = new System.Drawing.Point(12, 170);
            this.chkEntities.Name = "chkEntities";
            this.chkEntities.Size = new System.Drawing.Size(15, 14);
            this.chkEntities.TabIndex = 9;
            this.chkEntities.UseVisualStyleBackColor = true;
            // 
            // UcExportControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(365, 220);
            this.Controls.Add(this.chkEntities);
            this.Controls.Add(this.btnLoadWFs);
            this.Controls.Add(this.chkInterfaces);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.lbltitle);
            this.Controls.Add(this.chkReports);
            this.Controls.Add(this.chkWorkflows);
            this.Controls.Add(this.chkActors);
            this.Controls.Add(this.chkHome);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "UcExportControl";
            this.Text = "Zamba Software - Informe de Graficos";
            this.Load += new System.EventHandler(this.UcExportControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkHome;
        private System.Windows.Forms.CheckBox chkActors;
        private System.Windows.Forms.CheckBox chkWorkflows;
        private System.Windows.Forms.CheckBox chkReports;
        private ZLabel lbltitle;
        private ZButton btnExport;
        private System.Windows.Forms.CheckBox chkInterfaces;
        private ZButton btnLoadWFs;
        private System.Windows.Forms.CheckBox chkEntities;
    }
}