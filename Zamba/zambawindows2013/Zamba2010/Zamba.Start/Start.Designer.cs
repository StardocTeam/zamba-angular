using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;
using Zamba.AppBlock;

namespace Zamba.Start
{
    partial class Start
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Start));
            this.lblmessage = new Zamba.AppBlock.ZLabel();
            this.btnclose = new Zamba.AppBlock.ZButton();
            this.lblcount = new Zamba.AppBlock.ZLabel();
            this.button1 = new Zamba.AppBlock.ZButton();
            this.panelBtns = new Telerik.WinControls.UI.RadPanel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnTools = new Zamba.AppBlock.ZButton();
            this.radContextMenu1 = new Telerik.WinControls.UI.RadContextMenu(this.components);
            this.MenuRegister = new Telerik.WinControls.UI.RadMenuItem();
            this.MenuEnableOutlookExport = new Telerik.WinControls.UI.RadMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.lblcurrentversion = new Zamba.AppBlock.ZLabel();
            ((System.ComponentModel.ISupportInitialize)(this.panelBtns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblmessage
            // 
            this.lblmessage.AutoSize = true;
            this.lblmessage.BackColor = System.Drawing.Color.Transparent;
            this.lblmessage.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.lblmessage.FontSize = 9.75F;
            this.lblmessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.lblmessage.Location = new System.Drawing.Point(395, 377);
            this.lblmessage.Name = "lblmessage";
            this.lblmessage.Size = new System.Drawing.Size(0, 16);
            this.lblmessage.TabIndex = 1;
            this.lblmessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(157)))), ((int)(((byte)(224)))));
            this.btnclose.BackgroundImage = global::Zamba.Properties.Resources.appbar_close;
            this.btnclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnclose.FlatAppearance.BorderSize = 0;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(709, 12);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(34, 27);
            this.btnclose.TabIndex = 5;
            this.btnclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblcount
            // 
            this.lblcount.AutoSize = true;
            this.lblcount.BackColor = System.Drawing.Color.Transparent;
            this.lblcount.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.lblcount.FontSize = 9.75F;
            this.lblcount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.lblcount.Location = new System.Drawing.Point(401, 377);
            this.lblcount.Name = "lblcount";
            this.lblcount.Size = new System.Drawing.Size(0, 16);
            this.lblcount.TabIndex = 6;
            this.lblcount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(157)))), ((int)(((byte)(224)))));
            this.button1.BackgroundImage = global::Zamba.Properties.Resources.appbar_minus;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(660, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(34, 27);
            this.button1.TabIndex = 8;
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // panelBtns
            // 
            this.panelBtns.AutoSize = true;
            this.panelBtns.BackColor = System.Drawing.Color.Transparent;
            this.panelBtns.ForeColor = System.Drawing.Color.White;
            this.panelBtns.Location = new System.Drawing.Point(50, 120);
            this.panelBtns.Name = "panelBtns";
            // 
            // 
            // 
            this.panelBtns.RootElement.ControlBounds = new System.Drawing.Rectangle(50, 120, 0, 0);
            this.panelBtns.Size = new System.Drawing.Size(0, 0);
            this.panelBtns.TabIndex = 9;
            this.panelBtns.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.White;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Image = global::Zamba.Properties.Resources.pwbyzamba;
            this.pictureBox2.Location = new System.Drawing.Point(12, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(253, 76);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // btnTools
            // 
            this.btnTools.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(157)))), ((int)(((byte)(224)))));
            this.btnTools.BackgroundImage = global::Zamba.Properties.Resources.appbar_tools;
            this.btnTools.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTools.FlatAppearance.BorderSize = 0;
            this.btnTools.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTools.ForeColor = System.Drawing.Color.White;
            this.btnTools.Location = new System.Drawing.Point(709, 371);
            this.btnTools.Name = "btnTools";
            this.btnTools.Size = new System.Drawing.Size(34, 27);
            this.btnTools.TabIndex = 11;
            this.btnTools.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnTools.UseVisualStyleBackColor = false;
            this.btnTools.Click += new System.EventHandler(this.button2_Click);
            // 
            // radContextMenu1
            // 
            this.radContextMenu1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.MenuRegister,
            this.MenuEnableOutlookExport});
            // 
            // MenuRegister
            // 
            this.MenuRegister.Name = "MenuRegister";
            this.MenuRegister.Text = "Registrar Zamba";
            this.MenuRegister.Click += new System.EventHandler(this.MenuRegister_Click);
            // 
            // MenuEnableOutlookExport
            // 
            this.MenuEnableOutlookExport.Name = "MenuEnableOutlookExport";
            this.MenuEnableOutlookExport.Text = "Habilitar Exportacion";
            this.MenuEnableOutlookExport.Click += new System.EventHandler(this.MenuEnableOutlookExport_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Zamba Start";
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // lblcurrentversion
            // 
            this.lblcurrentversion.AutoSize = true;
            this.lblcurrentversion.BackColor = System.Drawing.Color.Transparent;
            this.lblcurrentversion.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.lblcurrentversion.FontSize = 9.75F;
            this.lblcurrentversion.ForeColor = System.Drawing.Color.White;
            this.lblcurrentversion.Location = new System.Drawing.Point(395, 23);
            this.lblcurrentversion.Name = "lblcurrentversion";
            this.lblcurrentversion.Size = new System.Drawing.Size(0, 16);
            this.lblcurrentversion.TabIndex = 12;
            this.lblcurrentversion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Start
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(94)))), ((int)(((byte)(130)))));
            this.BackgroundImage = global::Zamba.Properties.Resources.custom_blue_wallpaper;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(755, 410);
            this.Controls.Add(this.lblcurrentversion);
            this.Controls.Add(this.btnTools);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panelBtns);
            this.Controls.Add(this.lblcount);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.lblmessage);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Start";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.Start_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Start_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Start_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Start_MouseUp);
            this.Resize += new System.EventHandler(this.Start_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.panelBtns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void MenuEnableOutlookExport_Click(object sender, EventArgs e)
        {
            try
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(Path.Combine(Application.StartupPath, "app.ini"));
           System.IO.DirectoryInfo di = new DirectoryInfo(Membership.MembershipHelper.AppTempPath);
                if (di.Exists == false)
                { di.Create(); }
                fi.CopyTo(Path.Combine(di.FullName, "app.ini"), true);
            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
                System.Windows.Forms.MessageBox.Show("No se pudo copiar el app.ini de la Exportacion, contacte al administrador del sistema", "Zamba Exportacion", System.Windows.Forms.MessageBoxButtons.OK);
            }
            try
            {

                String ExportSetup = ZOptBusiness.GetValueOrDefault("ExportSetupLocation", "\\\\arbue11as06v\\zamba\\imagenes\\Setup Zamba Software\\Zamba Exporta Automatica\\Zamba.ExportaOutlook2010Direct.vsto");
                String Arguments = "/s";
                System.Diagnostics.ProcessStartInfo PS = new System.Diagnostics.ProcessStartInfo(ExportSetup, Arguments);
                PS.UseShellExecute = true;
                PS.WindowStyle = ProcessWindowStyle.Normal;
                System.Diagnostics.Process.Start(PS);
            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
                System.Windows.Forms.MessageBox.Show("No se pudo habilitar la Exportacion, contacte al administrador del sistema", "Zamba Exportacion", System.Windows.Forms.MessageBoxButtons.OK);
            }

        }

        #endregion
        private ZLabel lblmessage;
    
        private ZButton btnclose;
        private ZLabel lblcount;     
        private ZButton button1;
        private Telerik.WinControls.UI.RadPanel panelBtns;
        private System.Windows.Forms.PictureBox pictureBox2;
        private ZButton btnTools;
        private Telerik.WinControls.UI.RadContextMenu radContextMenu1;
        private Telerik.WinControls.UI.RadMenuItem MenuRegister;
        private Telerik.WinControls.UI.RadMenuItem MenuEnableOutlookExport;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private ZLabel lblcurrentversion;
    }
}

