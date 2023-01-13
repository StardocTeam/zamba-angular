namespace Zamba
{
    namespace EmbeddedExcel
    {
        partial class ExcelBrowser
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
            //    this.splitContainer1 = new System.Windows.Forms.SplitContainer();
                this.excelWrapper1 = new Zamba.EmbeddedExcel.ExcelWrapper();
              //  this.splitContainer1.Panel2.SuspendLayout();
               // this.splitContainer1.SuspendLayout();
                this.SuspendLayout();
                // 
                // splitContainer1
                // 
              //  this.splitContainer1.BackColor = System.Drawing.Color.White;
              //  this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
              //  this.splitContainer1.Location = new System.Drawing.Point(0, 0);
              //  this.splitContainer1.Name = "splitContainer1";
              //  this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
                // 
                // splitContainer1.Panel1
                // 
              //  this.splitContainer1.Panel1.BackColor = System.Drawing.Color.White;
                // 
                // splitContainer1.Panel2
                // 
                this.Controls.Add(this.excelWrapper1);
               // this.splitContainer1.Size = new System.Drawing.Size(638, 494);
              //  this.splitContainer1.SplitterDistance = 175;
              //  this.splitContainer1.TabIndex = 1;
                // 
                // excelWrapper1
                // 
                this.excelWrapper1.Dock = System.Windows.Forms.DockStyle.Fill;
                this.excelWrapper1.Location = new System.Drawing.Point(0, 0);
                this.excelWrapper1.Name = "excelWrapper1";
                this.excelWrapper1.Size = new System.Drawing.Size(638, 315);
                this.excelWrapper1.TabIndex = 0;
                this.excelWrapper1.ToolBarVisible = false;
                // 
                // Form1
                // 
               // this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
               // this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.BackColor = System.Drawing.Color.White;
                this.ClientSize = new System.Drawing.Size(638, 494);
              //  this.Controls.Add(this.splitContainer1);
                this.Name = "Form1";
                this.Text = "Zamba Browser";
              //  this.splitContainer1.Panel2.ResumeLayout(false);
              //  this.splitContainer1.ResumeLayout(false);
                this.ResumeLayout(false);

            }

            #endregion

             private Zamba.EmbeddedExcel.ExcelWrapper excelWrapper1;
            //private System.Windows.Forms.SplitContainer splitContainer1;
        }
    }
}

