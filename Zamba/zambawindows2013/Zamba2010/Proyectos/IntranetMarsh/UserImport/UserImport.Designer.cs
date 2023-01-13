namespace UserImport
{
    partial class UserImport
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtExcel = new System.Windows.Forms.TextBox();
            this.cmbExaminar = new System.Windows.Forms.Button();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.cmbVerificar = new System.Windows.Forms.Button();
            this.cmbImportar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lstAgregaran = new System.Windows.Forms.ListBox();
            this.lstActualizaran = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Excel origen:";
            // 
            // txtExcel
            // 
            this.txtExcel.Location = new System.Drawing.Point(12, 25);
            this.txtExcel.Name = "txtExcel";
            this.txtExcel.Size = new System.Drawing.Size(257, 20);
            this.txtExcel.TabIndex = 1;
            // 
            // cmbExaminar
            // 
            this.cmbExaminar.Location = new System.Drawing.Point(275, 25);
            this.cmbExaminar.Name = "cmbExaminar";
            this.cmbExaminar.Size = new System.Drawing.Size(68, 20);
            this.cmbExaminar.TabIndex = 2;
            this.cmbExaminar.Text = "&Examinar";
            this.cmbExaminar.UseVisualStyleBackColor = true;
            this.cmbExaminar.Click += new System.EventHandler(this.cmbExaminar_Click);
            // 
            // cmbVerificar
            // 
            this.cmbVerificar.Location = new System.Drawing.Point(12, 51);
            this.cmbVerificar.Name = "cmbVerificar";
            this.cmbVerificar.Size = new System.Drawing.Size(111, 23);
            this.cmbVerificar.TabIndex = 3;
            this.cmbVerificar.Text = "&Verificar Usuarios";
            this.cmbVerificar.UseVisualStyleBackColor = true;
            this.cmbVerificar.Click += new System.EventHandler(this.cmbVerificar_Click);
            // 
            // cmbImportar
            // 
            this.cmbImportar.Location = new System.Drawing.Point(232, 333);
            this.cmbImportar.Name = "cmbImportar";
            this.cmbImportar.Size = new System.Drawing.Size(111, 23);
            this.cmbImportar.TabIndex = 4;
            this.cmbImportar.Text = "&Importar Usuarios";
            this.cmbImportar.UseVisualStyleBackColor = true;
            this.cmbImportar.Click += new System.EventHandler(this.cmbImportar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Se agregaran:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(178, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Se actualizaran:";
            // 
            // lstAgregaran
            // 
            this.lstAgregaran.FormattingEnabled = true;
            this.lstAgregaran.Location = new System.Drawing.Point(12, 110);
            this.lstAgregaran.Name = "lstAgregaran";
            this.lstAgregaran.Size = new System.Drawing.Size(162, 212);
            this.lstAgregaran.TabIndex = 7;
            // 
            // lstActualizaran
            // 
            this.lstActualizaran.FormattingEnabled = true;
            this.lstActualizaran.Location = new System.Drawing.Point(181, 110);
            this.lstActualizaran.Name = "lstActualizaran";
            this.lstActualizaran.Size = new System.Drawing.Size(162, 212);
            this.lstActualizaran.TabIndex = 8;
            // 
            // UserImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 364);
            this.Controls.Add(this.lstActualizaran);
            this.Controls.Add(this.lstAgregaran);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbImportar);
            this.Controls.Add(this.cmbVerificar);
            this.Controls.Add(this.cmbExaminar);
            this.Controls.Add(this.txtExcel);
            this.Controls.Add(this.label1);
            this.Name = "UserImport";
            this.Text = "User Import";
            this.Load += new System.EventHandler(this.UserImport_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtExcel;
        private System.Windows.Forms.Button cmbExaminar;
        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.Button cmbVerificar;
        private System.Windows.Forms.Button cmbImportar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lstAgregaran;
        private System.Windows.Forms.ListBox lstActualizaran;
    }
}

