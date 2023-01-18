namespace Zamba.SetupExportaOutLook
{
    partial class ZambaSetupExportaOutLook
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.txZamba = new System.Windows.Forms.TextBox();
            this.btZamba = new System.Windows.Forms.Button();
            this.btAplicar = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Instalación de Zamba";
            // 
            // txZamba
            // 
            this.txZamba.Location = new System.Drawing.Point(134, 27);
            this.txZamba.Name = "txZamba";
            this.txZamba.Size = new System.Drawing.Size(237, 20);
            this.txZamba.TabIndex = 3;
            // 
            // btZamba
            // 
            this.btZamba.Location = new System.Drawing.Point(377, 27);
            this.btZamba.Name = "btZamba";
            this.btZamba.Size = new System.Drawing.Size(25, 20);
            this.btZamba.TabIndex = 1;
            this.btZamba.Text = "...";
            this.btZamba.UseVisualStyleBackColor = true;
            this.btZamba.Click += new System.EventHandler(this.btZamba_Click);
            // 
            // btAplicar
            // 
            this.btAplicar.Location = new System.Drawing.Point(178, 78);
            this.btAplicar.Name = "btAplicar";
            this.btAplicar.Size = new System.Drawing.Size(75, 23);
            this.btAplicar.TabIndex = 4;
            this.btAplicar.Text = "Aplicar";
            this.btAplicar.UseVisualStyleBackColor = true;
            this.btAplicar.Click += new System.EventHandler(this.btAplicar_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ZambaSetupExportaOutLook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 116);
            this.Controls.Add(this.btAplicar);
            this.Controls.Add(this.txZamba);
            this.Controls.Add(this.btZamba);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ZambaSetupExportaOutLook";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zamba.SetupExportaOutLook";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox txZamba;
        private System.Windows.Forms.Button btZamba;
        private System.Windows.Forms.Button btAplicar;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}

