namespace Stardoc.HtmlEditor
{
    internal partial class HtmlControlsList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HtmlControlsList));
            this.lbDocTypesList = new System.Windows.Forms.Label();
            this.cmbDocTypes = new System.Windows.Forms.ComboBox();
            this.lvControls = new System.Windows.Forms.ListView();
            this.cmsControls = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.insertarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBoxesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.radioButtonsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iglThumbnails = new System.Windows.Forms.ImageList(this.components);
            this.cmsControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbDocTypesList
            // 
            this.lbDocTypesList.AutoSize = true;
            this.lbDocTypesList.Location = new System.Drawing.Point(12, 9);
            this.lbDocTypesList.Name = "lbDocTypesList";
            this.lbDocTypesList.Size = new System.Drawing.Size(161, 13);
            this.lbDocTypesList.TabIndex = 0;
            this.lbDocTypesList.Text = "Listado de Tipos de Documento:";
            // 
            // cmbDocTypes
            // 
            this.cmbDocTypes.FormattingEnabled = true;
            this.cmbDocTypes.Location = new System.Drawing.Point(179, 6);
            this.cmbDocTypes.Name = "cmbDocTypes";
            this.cmbDocTypes.Size = new System.Drawing.Size(121, 21);
            this.cmbDocTypes.TabIndex = 1;
            this.cmbDocTypes.SelectedIndexChanged += new System.EventHandler(this.cmbDocTypes_SelectedIndexChanged);
            // 
            // lvControls
            // 
            this.lvControls.AllowDrop = true;
            this.lvControls.ContextMenuStrip = this.cmsControls;
            this.lvControls.LargeImageList = this.iglThumbnails;
            this.lvControls.Location = new System.Drawing.Point(12, 33);
            this.lvControls.MultiSelect = false;
            this.lvControls.Name = "lvControls";
            this.lvControls.ShowItemToolTips = true;
            this.lvControls.Size = new System.Drawing.Size(288, 388);
            this.lvControls.SmallImageList = this.iglThumbnails;
            this.lvControls.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvControls.TabIndex = 2;
            this.lvControls.UseCompatibleStateImageBehavior = false;
            this.lvControls.View = System.Windows.Forms.View.SmallIcon;
            this.lvControls.SelectedIndexChanged += new System.EventHandler(this.lvControls_SelectedIndexChanged);
            this.lvControls.DoubleClick += new System.EventHandler(this.lvControls_DoubleClick);
            // 
            // cmsControls
            // 
            this.cmsControls.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertarToolStripMenuItem,
            this.editarToolStripMenuItem,
            this.eliminarToolStripMenuItem});
            this.cmsControls.Name = "cmsControls";
            this.cmsControls.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cmsControls.ShowImageMargin = false;
            this.cmsControls.Size = new System.Drawing.Size(95, 70);
            this.cmsControls.Opening += new System.ComponentModel.CancelEventHandler(this.cmsControls_Opening);
            // 
            // insertarToolStripMenuItem
            // 
            this.insertarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.comboBoxesToolStripMenuItem,
            this.radioButtonsToolStripMenuItem,
            this.selectToolStripMenuItem,
            this.textAreaToolStripMenuItem,
            this.imageToolStripMenuItem});
            this.insertarToolStripMenuItem.Name = "insertarToolStripMenuItem";
            this.insertarToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.insertarToolStripMenuItem.Text = "Insertar";
            // 
            // comboBoxesToolStripMenuItem
            // 
            this.comboBoxesToolStripMenuItem.Name = "comboBoxesToolStripMenuItem";
            this.comboBoxesToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.comboBoxesToolStripMenuItem.Text = "CheckBox";
            this.comboBoxesToolStripMenuItem.Click += new System.EventHandler(this.comboBoxesToolStripMenuItem_Click);
            // 
            // radioButtonsToolStripMenuItem
            // 
            this.radioButtonsToolStripMenuItem.Name = "radioButtonsToolStripMenuItem";
            this.radioButtonsToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.radioButtonsToolStripMenuItem.Text = "RadioButton";
            this.radioButtonsToolStripMenuItem.Click += new System.EventHandler(this.radioButtonsToolStripMenuItem_Click);
            // 
            // selectToolStripMenuItem
            // 
            this.selectToolStripMenuItem.Name = "selectToolStripMenuItem";
            this.selectToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.selectToolStripMenuItem.Text = "Select";
            this.selectToolStripMenuItem.Click += new System.EventHandler(this.selectToolStripMenuItem_Click);
            // 
            // textAreaToolStripMenuItem
            // 
            this.textAreaToolStripMenuItem.Name = "textAreaToolStripMenuItem";
            this.textAreaToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.textAreaToolStripMenuItem.Text = "TextArea";
            this.textAreaToolStripMenuItem.Click += new System.EventHandler(this.textAreaToolStripMenuItem_Click);
            // 
            // imageToolStripMenuItem
            // 
            this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            this.imageToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.imageToolStripMenuItem.Text = "Image";
            this.imageToolStripMenuItem.Click += new System.EventHandler(this.imageToolStripMenuItem_Click);
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.editarToolStripMenuItem.Text = "Editar";
            this.editarToolStripMenuItem.Click += new System.EventHandler(this.editarToolStripMenuItem_Click);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // iglThumbnails
            // 
            this.iglThumbnails.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iglThumbnails.ImageStream")));
            this.iglThumbnails.TransparentColor = System.Drawing.Color.Transparent;
            this.iglThumbnails.Images.SetKeyName(0, "ListViewCheckBox.bmp");
            // 
            // HtmlControlsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvControls);
            this.Controls.Add(this.cmbDocTypes);
            this.Controls.Add(this.lbDocTypesList);
            this.Name = "HtmlControlsList";
            this.Size = new System.Drawing.Size(314, 435);
            this.Load += new System.EventHandler(this.HtmlControlsList_Load);
            this.cmsControls.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbDocTypesList;
        private System.Windows.Forms.ComboBox cmbDocTypes;
        private System.Windows.Forms.ImageList iglThumbnails;
        private System.Windows.Forms.ContextMenuStrip cmsControls;
        private System.Windows.Forms.ToolStripMenuItem insertarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem comboBoxesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem radioButtonsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem textAreaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        public System.Windows.Forms.ListView lvControls;
    }
}