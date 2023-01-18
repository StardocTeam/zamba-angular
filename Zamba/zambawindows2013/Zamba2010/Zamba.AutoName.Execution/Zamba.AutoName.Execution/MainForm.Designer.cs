namespace Zamba.AutoName.Execution
{
    partial class MainForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.btnAplicar = new Zamba.AppBlock.ZLinkLabel();
            this.LblInformation = new Zamba.AppBlock.ZSubTitleLabel();
            this.lstDocTypes = new System.Windows.Forms.ListBox();
            this.txtAutoName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Documentos disponibles:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 320);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "AutoName configurado:";
            // 
            // btnAplicar
            // 
            this.btnAplicar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(213)))), ((int)(((byte)(217)))));
            this.btnAplicar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnAplicar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAplicar.ForeColor = System.Drawing.Color.White;
            this.btnAplicar.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.btnAplicar.LinkColor = System.Drawing.Color.SteelBlue;
            this.btnAplicar.Location = new System.Drawing.Point(8, 375);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(280, 24);
            this.btnAplicar.TabIndex = 83;
            this.btnAplicar.TabStop = true;
            this.btnAplicar.Text = "Aplicar esta regla a los documentos existentes";
            this.btnAplicar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            // 
            // LblInformation
            // 
            this.LblInformation.BackColor = System.Drawing.Color.Silver;
            this.LblInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblInformation.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblInformation.Font = new System.Drawing.Font("Arial", 8.75F);
            this.LblInformation.ForeColor = System.Drawing.Color.White;
            this.LblInformation.Location = new System.Drawing.Point(2, 2);
            this.LblInformation.Name = "LblInformation";
            this.LblInformation.Size = new System.Drawing.Size(549, 37);
            this.LblInformation.TabIndex = 84;
            this.LblInformation.Text = "Seleccione el tipo de documento deseado para el cual se aplicara el proceso de Au" +
                "toName";
            this.LblInformation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lstDocTypes
            // 
            this.lstDocTypes.BackColor = System.Drawing.Color.White;
            this.lstDocTypes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstDocTypes.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDocTypes.ItemHeight = 16;
            this.lstDocTypes.Location = new System.Drawing.Point(8, 69);
            this.lstDocTypes.Name = "lstDocTypes";
            this.lstDocTypes.Size = new System.Drawing.Size(534, 242);
            this.lstDocTypes.TabIndex = 85;
            this.lstDocTypes.SelectedIndexChanged += new System.EventHandler(this.lstDocTypes_SelectedIndexChanged);
            // 
            // txtAutoName
            // 
            this.txtAutoName.BackColor = System.Drawing.Color.White;
            this.txtAutoName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAutoName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAutoName.Location = new System.Drawing.Point(8, 336);
            this.txtAutoName.MaxLength = 255;
            this.txtAutoName.Multiline = true;
            this.txtAutoName.Name = "txtAutoName";
            this.txtAutoName.ReadOnly = true;
            this.txtAutoName.Size = new System.Drawing.Size(534, 25);
            this.txtAutoName.TabIndex = 86;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 409);
            this.Controls.Add(this.txtAutoName);
            this.Controls.Add(this.lstDocTypes);
            this.Controls.Add(this.LblInformation);
            this.Controls.Add(this.btnAplicar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AutoName";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        internal Zamba.AppBlock.ZLinkLabel btnAplicar;
        internal Zamba.AppBlock.ZSubTitleLabel LblInformation;
        internal System.Windows.Forms.ListBox lstDocTypes;
        internal System.Windows.Forms.TextBox txtAutoName;
    }
}

