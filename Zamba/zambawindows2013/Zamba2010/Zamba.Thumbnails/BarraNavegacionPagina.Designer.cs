
namespace Zamba
{
    namespace Thumbnails
    {
        partial class BarraNavegacionPagina
        {
            /// <summary> 
            /// Required designer variable.
            /// </summary>
            private System.ComponentModel.IContainer components = null;


            private System.Windows.Forms.LinkLabel home;
            private System.Windows.Forms.LinkLabel end;
            private System.Windows.Forms.LinkLabel back;
            private System.Windows.Forms.LinkLabel next;

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
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BarraNavegacionPagina));
                this.home = new System.Windows.Forms.LinkLabel();
                this.end = new System.Windows.Forms.LinkLabel();
                this.back = new System.Windows.Forms.LinkLabel();
                this.next = new System.Windows.Forms.LinkLabel();
                this.SuspendLayout();
                // 
                // home
                // 
                resources.ApplyResources(this.home, "home");
                this.home.BackColor = System.Drawing.Color.CornflowerBlue;
                this.home.LinkColor = System.Drawing.Color.LavenderBlush;
                this.home.Name = "home";
                this.home.TabStop = true;
                this.home.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.home_LinkClicked);
                // 
                // end
                // 
                resources.ApplyResources(this.end, "end");
                this.end.BackColor = System.Drawing.Color.CornflowerBlue;
                this.end.LinkColor = System.Drawing.Color.LavenderBlush;
                this.end.Name = "end";
                this.end.TabStop = true;
                this.end.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.end_LinkClicked);
                // 
                // back
                // 
                resources.ApplyResources(this.back, "back");
                this.back.BackColor = System.Drawing.Color.CornflowerBlue;
                this.back.LinkColor = System.Drawing.Color.LavenderBlush;
                this.back.Name = "back";
                this.back.TabStop = true;
                this.back.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.back_LinkClicked);
                // 
                // next
                // 
                resources.ApplyResources(this.next, "next");
                this.next.BackColor = System.Drawing.Color.CornflowerBlue;
                this.next.LinkColor = System.Drawing.Color.LavenderBlush;
                this.next.Name = "next";
                this.next.TabStop = true;
                this.next.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.next_LinkClicked);
                // 
                // BarraNavegacionPagina
                // 
                resources.ApplyResources(this, "$this");
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.BackColor = System.Drawing.Color.CornflowerBlue;
                this.Controls.Add(this.next);
                this.Controls.Add(this.back);
                this.Controls.Add(this.end);
                this.Controls.Add(this.home);
                this.DoubleBuffered = true;
                this.ForeColor = System.Drawing.Color.LavenderBlush;
                this.MinimumSize = new System.Drawing.Size(90, 20);
                this.Name = "BarraNavegacionPagina";
                this.ResumeLayout(false);
                this.PerformLayout();

            }

            #endregion

        }
    }
}