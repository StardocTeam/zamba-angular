namespace DbToXML
{
    partial class DeleteOptions
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
            this.gridSource = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkdontaskagain = new System.Windows.Forms.CheckBox();
            this.btncancel = new System.Windows.Forms.Button();
            this.btnok = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridSource
            // 
            this.gridSource.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSource.Location = new System.Drawing.Point(0, 0);
            this.gridSource.Name = "gridSource";
            this.gridSource.RowHeadersWidth = 51;
            this.gridSource.Size = new System.Drawing.Size(933, 378);
            this.gridSource.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkdontaskagain);
            this.groupBox1.Controls.Add(this.btncancel);
            this.groupBox1.Controls.Add(this.btnok);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 282);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(933, 96);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // chkdontaskagain
            // 
            this.chkdontaskagain.AutoSize = true;
            this.chkdontaskagain.Location = new System.Drawing.Point(493, 48);
            this.chkdontaskagain.Name = "chkdontaskagain";
            this.chkdontaskagain.Size = new System.Drawing.Size(224, 19);
            this.chkdontaskagain.TabIndex = 7;
            this.chkdontaskagain.Text = "Repetir respuesta para toda la tabla";
            this.chkdontaskagain.UseVisualStyleBackColor = true;
            // 
            // btncancel
            // 
            this.btncancel.Location = new System.Drawing.Point(650, 19);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(75, 23);
            this.btncancel.TabIndex = 5;
            this.btncancel.Text = "No Borrar";
            this.btncancel.UseVisualStyleBackColor = true;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // btnok
            // 
            this.btnok.Location = new System.Drawing.Point(493, 19);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(125, 23);
            this.btnok.TabIndex = 4;
            this.btnok.Text = "Borrar de Destino";
            this.btnok.UseVisualStyleBackColor = true;
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // DeleteOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(933, 378);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gridSource);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "DeleteOptions";
            this.Text = "Borrar Registro de Destino que no existe en origen";
            this.Load += new System.EventHandler(this.CompareOptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridSource;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btncancel;
        private System.Windows.Forms.Button btnok;
        private System.Windows.Forms.CheckBox chkdontaskagain;
    }
}