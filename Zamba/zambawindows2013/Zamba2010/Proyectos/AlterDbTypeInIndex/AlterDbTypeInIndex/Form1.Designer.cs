namespace AlterDbTypeInIndex
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
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.btnEjecutar = new System.Windows.Forms.Button();
            this.LblMensaje = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtErrores = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtQuery
            // 
            this.txtQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQuery.Location = new System.Drawing.Point(0, 0);
            this.txtQuery.Multiline = true;
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtQuery.Size = new System.Drawing.Size(395, 347);
            this.txtQuery.TabIndex = 0;
            this.txtQuery.TextChanged += new System.EventHandler(this.txtQuery_TextChanged);
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEjecutar.Location = new System.Drawing.Point(261, 571);
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(116, 23);
            this.btnEjecutar.TabIndex = 1;
            this.btnEjecutar.Text = "Ejecutar Cambios";
            this.btnEjecutar.UseVisualStyleBackColor = true;
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // LblMensaje
            // 
            this.LblMensaje.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblMensaje.AutoSize = true;
            this.LblMensaje.Location = new System.Drawing.Point(-3, 576);
            this.LblMensaje.Name = "LblMensaje";
            this.LblMensaje.Size = new System.Drawing.Size(47, 13);
            this.LblMensaje.TabIndex = 2;
            this.LblMensaje.Text = "Mensaje";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-3, 375);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Errores:";
            // 
            // TxtErrores
            // 
            this.TxtErrores.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtErrores.Location = new System.Drawing.Point(0, 391);
            this.TxtErrores.Multiline = true;
            this.TxtErrores.Name = "TxtErrores";
            this.TxtErrores.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtErrores.Size = new System.Drawing.Size(395, 174);
            this.TxtErrores.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 594);
            this.Controls.Add(this.TxtErrores);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LblMensaje);
            this.Controls.Add(this.btnEjecutar);
            this.Controls.Add(this.txtQuery);
            this.Name = "Form1";
            this.Text = "AlterDbTypeInIndex";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtQuery;
        private System.Windows.Forms.Button btnEjecutar;
        private System.Windows.Forms.Label LblMensaje;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtErrores;
    }
}

