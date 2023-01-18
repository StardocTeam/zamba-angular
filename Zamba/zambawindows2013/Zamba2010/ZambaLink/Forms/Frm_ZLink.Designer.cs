using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ZambaLink
{
    partial class Frm_ZLink
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        
        private System.Windows.Forms.NotifyIcon notifyIcon;
        // private System.Windows.Forms.NotifyIcon nIcon;
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
        //public override Color 
        //{
        //    get { return Color.Red; }
        //}

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        /// List<ToolStripMenuItem> allItems = new List<ToolStripMenuItem>();
        bool flag = true;
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_ZLink));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.configuraciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actualizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.showLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.executeJSCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.webConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.appiniToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buscarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zambaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.personasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.versiónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.movePic = new System.Windows.Forms.PictureBox();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.movePic)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.AutoSize = false;
            this.menuStrip.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip.Enabled = false;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configuraciónToolStripMenuItem,
            this.buscarToolStripMenuItem,
            this.ayudaToolStripMenuItem});
            this.menuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(298, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // configuraciónToolStripMenuItem
            // 
            this.configuraciónToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actualizarToolStripMenuItem,
            this.toolStripSeparator1,
            this.showLogToolStripMenuItem,
            this.executeJSCodeToolStripMenuItem,
            this.webConfigToolStripMenuItem,
            this.appiniToolStripMenuItem});
            this.configuraciónToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.configuraciónToolStripMenuItem.Name = "configuraciónToolStripMenuItem";
            this.configuraciónToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.configuraciónToolStripMenuItem.Text = " &Configuración";
            // 
            // actualizarToolStripMenuItem
            // 
            this.actualizarToolStripMenuItem.Name = "actualizarToolStripMenuItem";
            this.actualizarToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.actualizarToolStripMenuItem.Text = "&Actualizar";
            this.actualizarToolStripMenuItem.Click += new System.EventHandler(this.actualizarToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(155, 6);
            // 
            // showLogToolStripMenuItem
            // 
            this.showLogToolStripMenuItem.Name = "showLogToolStripMenuItem";
            this.showLogToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.showLogToolStripMenuItem.Text = "&Show Log";
            this.showLogToolStripMenuItem.Click += new System.EventHandler(this.showLogToolStripMenuItem_Click);
            // 
            // executeJSCodeToolStripMenuItem
            // 
            this.executeJSCodeToolStripMenuItem.Name = "executeJSCodeToolStripMenuItem";
            this.executeJSCodeToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.executeJSCodeToolStripMenuItem.Text = "&Execute JS Code";
            this.executeJSCodeToolStripMenuItem.Click += new System.EventHandler(this.executeJSCodeToolStripMenuItem_Click);
            // 
            // webConfigToolStripMenuItem
            // 
            this.webConfigToolStripMenuItem.Name = "webConfigToolStripMenuItem";
            this.webConfigToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.webConfigToolStripMenuItem.Text = "&Web Config";
            this.webConfigToolStripMenuItem.Visible = false;
            this.webConfigToolStripMenuItem.Click += new System.EventHandler(this.webConfigToolStripMenuItem_Click);
            // 
            // appiniToolStripMenuItem
            // 
            this.appiniToolStripMenuItem.Name = "appiniToolStripMenuItem";
            this.appiniToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.appiniToolStripMenuItem.Text = "App.ini";
            this.appiniToolStripMenuItem.Visible = false;
            this.appiniToolStripMenuItem.Click += new System.EventHandler(this.appiniToolStripMenuItem_Click);
            // 
            // buscarToolStripMenuItem
            // 
            this.buscarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zambaToolStripMenuItem,
            this.personasToolStripMenuItem});
            this.buscarToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.buscarToolStripMenuItem.Name = "buscarToolStripMenuItem";
            this.buscarToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.buscarToolStripMenuItem.Text = "Buscar";
            this.buscarToolStripMenuItem.Click += new System.EventHandler(this.buscarToolStripMenuItem_Click);
            // 
            // zambaToolStripMenuItem
            // 
            this.zambaToolStripMenuItem.Name = "zambaToolStripMenuItem";
            this.zambaToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.zambaToolStripMenuItem.Text = "&Zamba";
            this.zambaToolStripMenuItem.Click += new System.EventHandler(this.zambaToolStripMenuItem_Click);
            // 
            // personasToolStripMenuItem
            // 
            this.personasToolStripMenuItem.Name = "personasToolStripMenuItem";
            this.personasToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.personasToolStripMenuItem.Text = "&Personas";
            this.personasToolStripMenuItem.Click += new System.EventHandler(this.personasToolStripMenuItem_Click);
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ayudaToolStripMenuItem1,
            this.versiónToolStripMenuItem});
            this.ayudaToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ayudaToolStripMenuItem.Text = "&Ayuda";
            // 
            // ayudaToolStripMenuItem1
            // 
            this.ayudaToolStripMenuItem1.Name = "ayudaToolStripMenuItem1";
            this.ayudaToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.ayudaToolStripMenuItem1.Text = "&Ayuda";
            this.ayudaToolStripMenuItem1.Click += new System.EventHandler(this.ayudaToolStripMenuItem1_Click_1);
            // 
            // versiónToolStripMenuItem
            // 
            this.versiónToolStripMenuItem.Name = "versiónToolStripMenuItem";
            this.versiónToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.versiónToolStripMenuItem.Text = "&Versión";
            // 
            // movePic
            // 
            this.movePic.BackColor = System.Drawing.Color.Transparent;
            this.movePic.Cursor = System.Windows.Forms.Cursors.NoMove2D;
            this.movePic.ErrorImage = ((System.Drawing.Image)(resources.GetObject("movePic.ErrorImage")));
            this.movePic.Image = ((System.Drawing.Image)(resources.GetObject("movePic.Image")));
            this.movePic.InitialImage = ((System.Drawing.Image)(resources.GetObject("movePic.InitialImage")));
            this.movePic.Location = new System.Drawing.Point(270, -2);
            this.movePic.Margin = new System.Windows.Forms.Padding(0);
            this.movePic.Name = "movePic";
            this.movePic.Size = new System.Drawing.Size(26, 21);
            this.movePic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.movePic.TabIndex = 2;
            this.movePic.TabStop = false;
            this.movePic.Visible = false;
            // 
            // Frm_ZLink
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            this.ClientSize = new System.Drawing.Size(298, 421);
            this.ControlBox = false;
            this.Controls.Add(this.movePic);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::Zamba.Link.Properties.Resources.ZLink;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Frm_ZLink";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zamba Link";
            this.Load += new System.EventHandler(this.Frm_ZLink_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.movePic)).EndInit();
            this.ResumeLayout(false);

        }       

        private void ArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flag = false;
        }

        private void DropDown_MouseLeave(object sender, EventArgs e)
        {          
            this.configuraciónToolStripMenuItem.HideDropDown();
            this.ayudaToolStripMenuItem.HideDropDown();            
            this.menuStrip.Height = 8;
            flag = true;
        }

        private void MenuStrip_MouseLeave(object sender, EventArgs e)
        {
            if (flag != false)
            {
                this.menuStrip.Height = 11;
            }
        }

        private void MenuStrip_MouseHover(object sender, EventArgs e)
        {
            this.menuStrip.Height = 25;
        }
        #endregion

        private SaveFileDialog saveFileDialog;
        private MenuStrip menuStrip;
        private ToolStripMenuItem configuraciónToolStripMenuItem;
        private ToolStripMenuItem actualizarToolStripMenuItem;
        private ToolStripMenuItem ayudaToolStripMenuItem;        
        private ToolStripMenuItem versiónToolStripMenuItem;        
        private ToolStripMenuItem showLogToolStripMenuItem;
        private ToolStripMenuItem executeJSCodeToolStripMenuItem;
        private PictureBox movePic;
        private ToolStripMenuItem ayudaToolStripMenuItem1;
        private ToolStripMenuItem buscarToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem webConfigToolStripMenuItem;
        private ToolStripMenuItem appiniToolStripMenuItem;
        private ToolStripMenuItem zambaToolStripMenuItem;
        private ToolStripMenuItem personasToolStripMenuItem;
    }
}