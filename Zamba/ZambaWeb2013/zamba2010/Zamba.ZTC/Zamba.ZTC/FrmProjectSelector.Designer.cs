using System.Drawing;
using System.Windows.Forms;

namespace Zamba.ZTC
{
    partial class FrmProjectSelector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProjectSelector));
            this.SuspendLayout();
            // 
            // FrmProjectSelector
            // 
            this.Ucp = new UCProjectSelector();
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmProjectSelector";
            this.ResumeLayout(false);
            this.Size = new Size(331, 180);
            this.AutoSize = false;
            this.Text = "Selección de proyectos";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.Ucp.Dock = DockStyle.Fill;
            this.Controls.Add(Ucp);
            

        }

        #endregion
    }
}