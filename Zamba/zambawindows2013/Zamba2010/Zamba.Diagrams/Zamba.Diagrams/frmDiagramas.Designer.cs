namespace Zamba.Diagrams
{
    partial class FrmDiagramas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDiagramas));
            this.radCommandBar1 = new Telerik.WinControls.UI.RadCommandBar();
            this.commandBarRowElement1 = new Telerik.WinControls.UI.CommandBarRowElement();
            this.commandBarStripElement1 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.btnExport = new Telerik.WinControls.UI.CommandBarButton();
            this.commandBarStripElement2 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.Refresh = new Telerik.WinControls.UI.CommandBarLabel();
            this.commandBarRowElement2 = new Telerik.WinControls.UI.CommandBarRowElement();
            this.commandBarRowElement3 = new Telerik.WinControls.UI.CommandBarRowElement();
            ((System.ComponentModel.ISupportInitialize)(this.radCommandBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // radCommandBar1
            // 
            this.radCommandBar1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.radCommandBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radCommandBar1.Location = new System.Drawing.Point(0, 0);
            this.radCommandBar1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.radCommandBar1.Name = "radCommandBar1";
            // 
            // 
            // 
            this.radCommandBar1.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 50, 50);
            this.radCommandBar1.Rows.AddRange(new Telerik.WinControls.UI.CommandBarRowElement[] {
            this.commandBarRowElement1});
            this.radCommandBar1.Size = new System.Drawing.Size(1972, 106);
            this.radCommandBar1.TabIndex = 0;
            this.radCommandBar1.Text = "Refresh";
            // 
            // commandBarRowElement1
            // 
            this.commandBarRowElement1.DisplayName = null;
            this.commandBarRowElement1.MinSize = new System.Drawing.Size(25, 25);
            this.commandBarRowElement1.Strips.AddRange(new Telerik.WinControls.UI.CommandBarStripElement[] {
            this.commandBarStripElement1,
            this.commandBarStripElement2});
            // 
            // commandBarStripElement1
            // 
            this.commandBarStripElement1.DisplayName = "commandBarStripElement1";
            // 
            // 
            // 
            this.commandBarStripElement1.Grip.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarStripElement1.Grip.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarStripElement1.Items.AddRange(new Telerik.WinControls.UI.RadCommandBarBaseItem[] {
            this.btnExport});
            this.commandBarStripElement1.Name = "commandBarStripElement1";
            // 
            // 
            // 
            this.commandBarStripElement1.OverflowButton.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarStripElement1.OverflowButton.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarStripElement1.Text = "";
            // 
            // btnExport
            // 
            this.btnExport.AccessibleDescription = "commandBarButton1";
            this.btnExport.AccessibleName = "commandBarButton1";
            this.btnExport.DisplayName = "Exportar";
            this.btnExport.DrawText = true;
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.Name = "btnExport";
            this.btnExport.Text = "Exportar";
            this.btnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExport.TextWrap = true;
            this.btnExport.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            this.btnExport.Click += new System.EventHandler(this.commandBarButton1_Click);
            // 
            // commandBarStripElement2
            // 
            this.commandBarStripElement2.DisplayName = "commandBarStripElement2";
            // 
            // 
            // 
            this.commandBarStripElement2.Grip.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarStripElement2.Grip.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarStripElement2.Items.AddRange(new Telerik.WinControls.UI.RadCommandBarBaseItem[] {
            this.Refresh});
            this.commandBarStripElement2.Name = "commandBarStripElement2";
            // 
            // 
            // 
            this.commandBarStripElement2.OverflowButton.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarStripElement2.OverflowButton.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarStripElement2.Text = "";
            // 
            // Refresh
            // 
            this.Refresh.DisplayName = "commandBarLabel1";
            this.Refresh.Name = "Refresh";
            this.Refresh.Text = "Actualizar";
            this.Refresh.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            this.Refresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // commandBarRowElement2
            // 
            this.commandBarRowElement2.DisplayName = null;
            this.commandBarRowElement2.MinSize = new System.Drawing.Size(25, 25);
            // 
            // commandBarRowElement3
            // 
            this.commandBarRowElement3.DisplayName = null;
            this.commandBarRowElement3.MinSize = new System.Drawing.Size(25, 25);
            // 
            // FrmDiagramas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1972, 877);
            this.Controls.Add(this.radCommandBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "FrmDiagramas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Diagramas";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmDiagramas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radCommandBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadCommandBar radCommandBar1;
        private Telerik.WinControls.UI.CommandBarRowElement commandBarRowElement1;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement1;
        private Telerik.WinControls.UI.CommandBarButton btnExport;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement2;
        private new Telerik.WinControls.UI.CommandBarLabel Refresh;
        private Telerik.WinControls.UI.CommandBarRowElement commandBarRowElement2;
        private Telerik.WinControls.UI.CommandBarRowElement commandBarRowElement3;
    }
}

