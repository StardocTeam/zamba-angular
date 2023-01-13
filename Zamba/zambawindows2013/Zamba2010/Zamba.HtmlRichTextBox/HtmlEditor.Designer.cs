namespace Zamba.HtmlRichTextBox
{
    partial class HtmlEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HtmlEditor));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnBold = new System.Windows.Forms.ToolStripButton();
            this.btnItalic = new System.Windows.Forms.ToolStripButton();
            this.btUnderline = new System.Windows.Forms.ToolStripButton();
            this.btnStrikeOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnColor = new System.Windows.Forms.ToolStripButton();
            this.cboFontType = new System.Windows.Forms.ToolStripDropDownButton();
            this.cboFontSize = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCopy = new System.Windows.Forms.ToolStripButton();
            this.btnPaste = new System.Windows.Forms.ToolStripButton();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.richTextBox1 = new HtmlRichText.HtmlRichTextBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnBold,
            this.btnItalic,
            this.btUnderline,
            this.btnStrikeOut,
            this.btnColor,
            this.toolStripSeparator2,
            this.cboFontType,
            this.cboFontSize,
            this.toolStripSeparator3,
            this.btnCopy,
            this.btnPaste});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(455, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnBold
            // 
            this.btnBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBold.Image = global::Zamba.HtmlRichTextBox.Properties.Resources.tb4_bold;
            this.btnBold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBold.Name = "btnBold";
            this.btnBold.Size = new System.Drawing.Size(23, 22);
            this.btnBold.Text = "Negrita";
            this.btnBold.Click += new System.EventHandler(this.btnBold_Click);
            // 
            // btnItalic
            // 
            this.btnItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnItalic.Image = global::Zamba.HtmlRichTextBox.Properties.Resources.tb5_italic;
            this.btnItalic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnItalic.Name = "btnItalic";
            this.btnItalic.Size = new System.Drawing.Size(23, 22);
            this.btnItalic.Text = "Cursiva";
            this.btnItalic.Click += new System.EventHandler(this.btnItalic_Click);
            // 
            // btUnderline
            // 
            this.btUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btUnderline.Image = global::Zamba.HtmlRichTextBox.Properties.Resources.tb6_underline;
            this.btUnderline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btUnderline.Name = "btUnderline";
            this.btUnderline.Size = new System.Drawing.Size(23, 22);
            this.btUnderline.Text = "Subrayado";
            this.btUnderline.Click += new System.EventHandler(this.btUnderline_Click);
            // 
            // btnStrikeOut
            // 
            this.btnStrikeOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStrikeOut.Image = global::Zamba.HtmlRichTextBox.Properties.Resources.tb7_strikeout;
            this.btnStrikeOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStrikeOut.Name = "btnStrikeOut";
            this.btnStrikeOut.Size = new System.Drawing.Size(23, 22);
            this.btnStrikeOut.Text = "Tachado";
            this.btnStrikeOut.Click += new System.EventHandler(this.btnStrikeOut_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnColor
            // 
            this.btnColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnColor.Image = global::Zamba.HtmlRichTextBox.Properties.Resources.tb10_color;
            this.btnColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(23, 22);
            this.btnColor.Text = "Color";
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // cboFontType
            // 
            this.cboFontType.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.cboFontType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cboFontType.Name = "cboFontType";
            this.cboFontType.Size = new System.Drawing.Size(44, 22);
            this.cboFontType.Text = "Tipo";
            // 
            // cboFontSize
            // 
            this.cboFontSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.cboFontSize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cboFontSize.Name = "cboFontSize";
            this.cboFontSize.Size = new System.Drawing.Size(64, 22);
            this.cboFontSize.Text = "Tamaño";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCopy
            // 
            this.btnCopy.AutoToolTip = false;
            this.btnCopy.BackColor = System.Drawing.Color.Silver;
            this.btnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopy.Margin = new System.Windows.Forms.Padding(0, 3, 1, 4);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(52, 18);
            this.btnCopy.Text = "COPIAR";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnPaste
            // 
            this.btnPaste.BackColor = System.Drawing.Color.Silver;
            this.btnPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnPaste.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPaste.Image = ((System.Drawing.Image)(resources.GetObject("btnPaste.Image")));
            this.btnPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPaste.Margin = new System.Windows.Forms.Padding(1, 3, 0, 4);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(47, 18);
            this.btnPaste.Text = "PEGAR";
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(0, 28);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(455, 312);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // HtmlEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "HtmlEditor";
            this.Size = new System.Drawing.Size(455, 340);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnBold;
        public HtmlRichText.HtmlRichTextBox richTextBox1;
        private System.Windows.Forms.ToolStripButton btnItalic;
        private System.Windows.Forms.ToolStripButton btUnderline;
        private System.Windows.Forms.ToolStripButton btnStrikeOut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnColor;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStripButton btnCopy;
        private System.Windows.Forms.ToolStripButton btnPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripDropDownButton cboFontType;
        private System.Windows.Forms.ToolStripDropDownButton cboFontSize;
    }
}
