namespace Zamba.Link
{
    partial class Frm_SuggestedPeople
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
            this.btnHide = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnHide
            // 
            this.btnHide.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHide.Location = new System.Drawing.Point(-1, -1);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(14, 461);
            this.btnHide.TabIndex = 0;
            this.btnHide.Text = ">";
            this.btnHide.UseVisualStyleBackColor = true;
            this.btnHide.Click += new System.EventHandler(this.button1_Click);
            // 
            // Frm_SuggestedPeople
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 531);
            this.Controls.Add(this.btnHide);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_SuggestedPeople";
            this.Text = "Tal vez conozcas...";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button btnHide;
    }
}