using Zamba.AppBlock;

namespace ExportaOutlook.Forms
{
    partial class FrmExportedFolder
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
            this.btnPickFolder = new Zamba.AppBlock.ZButton();
            this.btnRemoveFolder = new Zamba.AppBlock.ZButton();
            this.btnOK = new Zamba.AppBlock.ZButton();
            this.btnCancel = new Zamba.AppBlock.ZButton();
            this.lblFolder = new Zamba.AppBlock.ZLabel();
            this.lblID = new Zamba.AppBlock.ZLabel();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnPickFolder
            // 
            this.btnPickFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(157)))), ((int)(((byte)(224)))));
            this.btnPickFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPickFolder.ForeColor = System.Drawing.Color.White;
            this.btnPickFolder.Location = new System.Drawing.Point(12, 12);
            this.btnPickFolder.Name = "btnPickFolder";
            this.btnPickFolder.Size = new System.Drawing.Size(150, 27);
            this.btnPickFolder.TabIndex = 0;
            this.btnPickFolder.Text = "Seleccionar carpeta";
            this.btnPickFolder.UseVisualStyleBackColor = true;
            this.btnPickFolder.Click += new System.EventHandler(this.btnPickFolder_Click);
            // 
            // btnRemoveFolder
            // 
            this.btnRemoveFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(157)))), ((int)(((byte)(224)))));
            this.btnRemoveFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveFolder.ForeColor = System.Drawing.Color.White;
            this.btnRemoveFolder.Location = new System.Drawing.Point(168, 12);
            this.btnRemoveFolder.Name = "btnRemoveFolder";
            this.btnRemoveFolder.Size = new System.Drawing.Size(150, 27);
            this.btnRemoveFolder.TabIndex = 1;
            this.btnRemoveFolder.Text = "Quitar relación";
            this.btnRemoveFolder.UseVisualStyleBackColor = true;
            this.btnRemoveFolder.Click += new System.EventHandler(this.btnRemoveFolder_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(157)))), ((int)(((byte)(224)))));
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(387, 131);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 25);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "Aceptar";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(157)))), ((int)(((byte)(224)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(473, 131);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.BackColor = System.Drawing.Color.Transparent;
            this.lblFolder.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFolder.FontSize = 9.75F;
            this.lblFolder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.lblFolder.Location = new System.Drawing.Point(12, 65);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(159, 16);
            this.lblFolder.TabIndex = 11;
            this.lblFolder.Text = "Carpeta de exportados";
            this.lblFolder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.BackColor = System.Drawing.Color.Transparent;
            this.lblID.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.FontSize = 9.75F;
            this.lblID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.lblID.Location = new System.Drawing.Point(12, 92);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(99, 16);
            this.lblID.TabIndex = 12;
            this.lblID.Text = "ID de carpeta";
            this.lblID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFolder
            // 
            this.txtFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolder.Location = new System.Drawing.Point(168, 62);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.ReadOnly = true;
            this.txtFolder.Size = new System.Drawing.Size(385, 21);
            this.txtFolder.TabIndex = 13;
            // 
            // txtID
            // 
            this.txtID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtID.Location = new System.Drawing.Point(168, 89);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(385, 21);
            this.txtID.TabIndex = 14;
            // 
            // FrmExportedFolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 168);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.lblFolder);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRemoveFolder);
            this.Controls.Add(this.btnPickFolder);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmExportedFolder";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seleccione una carpeta para almacenar mails exportados";
            this.Load += new System.EventHandler(this.FrmExportedFolder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZButton btnPickFolder;
        private ZButton btnRemoveFolder;
        private ZButton btnOK;
        private ZButton btnCancel;
        private ZLabel lblFolder;
        private ZLabel lblID;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.TextBox txtID;
    }
}