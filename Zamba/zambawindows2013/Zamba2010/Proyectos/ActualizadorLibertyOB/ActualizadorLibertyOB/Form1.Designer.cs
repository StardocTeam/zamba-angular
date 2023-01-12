namespace ActualizadorLibertyOB
{
    partial class Form1
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
            this.dialogoexaminar = new System.Windows.Forms.OpenFileDialog();
            this.RutaTxt = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.fecharecepcion = new System.Windows.Forms.DateTimePicker();
            this.fechadigitalizacion = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.botonactualizar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dialogoexaminar
            // 
            this.dialogoexaminar.FileName = "openFileDialog1";
            this.dialogoexaminar.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // RutaTxt
            // 
            this.RutaTxt.Location = new System.Drawing.Point(41, 27);
            this.RutaTxt.Name = "RutaTxt";
            this.RutaTxt.Size = new System.Drawing.Size(289, 20);
            this.RutaTxt.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(336, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Examinar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // fecharecepcion
            // 
            this.fecharecepcion.Location = new System.Drawing.Point(44, 110);
            this.fecharecepcion.Name = "fecharecepcion";
            this.fecharecepcion.Size = new System.Drawing.Size(200, 20);
            this.fecharecepcion.TabIndex = 4;
            // 
            // fechadigitalizacion
            // 
            this.fechadigitalizacion.Location = new System.Drawing.Point(41, 198);
            this.fechadigitalizacion.Name = "fechadigitalizacion";
            this.fechadigitalizacion.Size = new System.Drawing.Size(200, 20);
            this.fechadigitalizacion.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Ubicacion del Archivo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Fecha Recepción";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Fecha Digitalización";
            // 
            // botonactualizar
            // 
            this.botonactualizar.Location = new System.Drawing.Point(336, 227);
            this.botonactualizar.Name = "botonactualizar";
            this.botonactualizar.Size = new System.Drawing.Size(75, 23);
            this.botonactualizar.TabIndex = 9;
            this.botonactualizar.Text = "Actualizar";
            this.botonactualizar.UseVisualStyleBackColor = true;
            this.botonactualizar.Click += new System.EventHandler(this.botonactualizar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 262);
            this.Controls.Add(this.botonactualizar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fechadigitalizacion);
            this.Controls.Add(this.fecharecepcion);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.RutaTxt);
            this.Name = "Form1";
            this.Text = "Actualizador de Archivos O.B";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog dialogoexaminar;
        private System.Windows.Forms.TextBox RutaTxt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker fecharecepcion;
        private System.Windows.Forms.DateTimePicker fechadigitalizacion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button botonactualizar;

    }
}

