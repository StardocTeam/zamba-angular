namespace Zamba.Diagrams.UserControls
{
    partial class UCDiagrams
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TabManager = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.TabManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabManager.Location = new System.Drawing.Point(0, 0);
            this.TabManager.Name = "tabControl1";
            this.TabManager.SelectedIndex = 0;
            this.TabManager.Size = new System.Drawing.Size(150, 150);
            this.TabManager.TabIndex = 0;
            // 
            // UCDiagrams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TabManager);
            this.Name = "UCDiagrams";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabManager;

    }
}
