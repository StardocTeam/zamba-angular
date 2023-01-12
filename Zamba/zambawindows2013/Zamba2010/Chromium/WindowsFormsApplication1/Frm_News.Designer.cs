namespace Zamba.ZChromium
{
    partial class Frm_News
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
            this.lnkLbl = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lnkLbl
            // 
            this.lnkLbl.BackColor = System.Drawing.Color.Transparent;
            this.lnkLbl.Location = new System.Drawing.Point(-2, -4);
            this.lnkLbl.Name = "lnkLbl";
            this.lnkLbl.Size = new System.Drawing.Size(478, 16);
            this.lnkLbl.TabIndex = 0;
            this.lnkLbl.TabStop = true;
            this.lnkLbl.Text = "Ver noticia completa";
            this.lnkLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Frm_News
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(468, 304);
            this.Controls.Add(this.lnkLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_News";
            this.ShowIcon = false;
            this.Text = "Zamba News";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.LinkLabel lnkLbl;
    }
}