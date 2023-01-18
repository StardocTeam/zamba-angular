namespace ProgrammaticFileTypesDemo
{
    partial class FileTypeRegisterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileTypeRegisterForm));
            this.RegisterButton = new System.Windows.Forms.Button();
            this.txtPathEjecutable = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPathIcono = new System.Windows.Forms.TextBox();
            this.cmbExtensions = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // RegisterButton
            // 
            this.RegisterButton.Location = new System.Drawing.Point(228, 99);
            this.RegisterButton.Name = "RegisterButton";
            this.RegisterButton.Size = new System.Drawing.Size(76, 25);
            this.RegisterButton.TabIndex = 0;
            this.RegisterButton.Text = "Registrar";
            this.RegisterButton.UseVisualStyleBackColor = true;
            this.RegisterButton.Click += new System.EventHandler(this.RegisterButton_Click);
            // 
            // txtPathEjecutable
            // 
            this.txtPathEjecutable.Location = new System.Drawing.Point(162, 38);
            this.txtPathEjecutable.Name = "txtPathEjecutable";
            this.txtPathEjecutable.Size = new System.Drawing.Size(142, 20);
            this.txtPathEjecutable.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Extension (con punto)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Path de Ejecutable Asociado";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Icono de Ejecutable Asociado";
            // 
            // txtPathIcono
            // 
            this.txtPathIcono.Location = new System.Drawing.Point(162, 63);
            this.txtPathIcono.Name = "txtPathIcono";
            this.txtPathIcono.Size = new System.Drawing.Size(142, 20);
            this.txtPathIcono.TabIndex = 5;
            // 
            // cmbExtensions
            // 
            this.cmbExtensions.FormattingEnabled = true;
            this.cmbExtensions.Items.AddRange(new object[] {
            ".dwg"});
            this.cmbExtensions.Location = new System.Drawing.Point(162, 11);
            this.cmbExtensions.Name = "cmbExtensions";
            this.cmbExtensions.Size = new System.Drawing.Size(83, 21);
            this.cmbExtensions.TabIndex = 7;
            this.cmbExtensions.SelectedIndexChanged += new System.EventHandler(this.cmbExtensions_SelectedIndexChanged);
            // 
            // FileTypeRegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 142);
            this.Controls.Add(this.cmbExtensions);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPathIcono);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPathEjecutable);
            this.Controls.Add(this.RegisterButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FileTypeRegisterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registro de Tipos de Archivos";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RegisterButton;
        private System.Windows.Forms.TextBox txtPathEjecutable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPathIcono;
        private System.Windows.Forms.ComboBox cmbExtensions;
    }
}

