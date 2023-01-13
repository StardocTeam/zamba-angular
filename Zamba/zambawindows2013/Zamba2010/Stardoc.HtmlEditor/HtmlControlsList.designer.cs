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
            this.lvControls = new System.Windows.Forms.ListView();
            this.chType = new System.Windows.Forms.ColumnHeader();
            this.chName = new System.Windows.Forms.ColumnHeader();
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
            this.lbDocTypeName = new System.Windows.Forms.Label();
            this.pnlIndexes = new System.Windows.Forms.Panel();
            this.btRefreshIndexes = new System.Windows.Forms.Button();
            this.cmsControls.SuspendLayout();
            this.pnlIndexes.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvControls
            // 
            this.lvControls.AllowDrop = true;
            this.lvControls.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chType,
            this.chName});
            this.lvControls.ContextMenuStrip = this.cmsControls;
            this.lvControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvControls.LargeImageList = this.iglThumbnails;
            this.lvControls.Location = new System.Drawing.Point(0, 0);
            this.lvControls.MultiSelect = false;
            this.lvControls.Name = "lvControls";
            this.lvControls.ShowItemToolTips = true;
            this.lvControls.Size = new System.Drawing.Size(240, 391);
            this.lvControls.SmallImageList = this.iglThumbnails;
            this.lvControls.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvControls.TabIndex = 2;
            this.lvControls.UseCompatibleStateImageBehavior = false;
            this.lvControls.View = System.Windows.Forms.View.Details;
            this.lvControls.SelectedIndexChanged += new System.EventHandler(this.lvControls_SelectedIndexChanged);
            this.lvControls.DoubleClick += new System.EventHandler(this.lvControls_DoubleClick);
            // 
            // chType
            // 
            this.chType.Text = "Tipo";
            this.chType.Width = 80;
            // 
            // chName
            // 
            this.chName.Text = "Nombre";
            this.chName.Width = 155;
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
            this.cmsControls.Size = new System.Drawing.Size(100, 70);
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
            this.insertarToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.insertarToolStripMenuItem.Text = "Insertar";
            // 
            // comboBoxesToolStripMenuItem
            // 
            this.comboBoxesToolStripMenuItem.Name = "comboBoxesToolStripMenuItem";
            this.comboBoxesToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.comboBoxesToolStripMenuItem.Text = "CheckBox";
            this.comboBoxesToolStripMenuItem.Click += new System.EventHandler(this.comboBoxesToolStripMenuItem_Click);
            // 
            // radioButtonsToolStripMenuItem
            // 
            this.radioButtonsToolStripMenuItem.Name = "radioButtonsToolStripMenuItem";
            this.radioButtonsToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.radioButtonsToolStripMenuItem.Text = "RadioButton";
            this.radioButtonsToolStripMenuItem.Click += new System.EventHandler(this.radioButtonsToolStripMenuItem_Click);
            // 
            // selectToolStripMenuItem
            // 
            this.selectToolStripMenuItem.Name = "selectToolStripMenuItem";
            this.selectToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.selectToolStripMenuItem.Text = "Select";
            this.selectToolStripMenuItem.Click += new System.EventHandler(this.selectToolStripMenuItem_Click);
            // 
            // textAreaToolStripMenuItem
            // 
            this.textAreaToolStripMenuItem.Name = "textAreaToolStripMenuItem";
            this.textAreaToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.textAreaToolStripMenuItem.Text = "TextArea";
            this.textAreaToolStripMenuItem.Click += new System.EventHandler(this.textAreaToolStripMenuItem_Click);
            // 
            // imageToolStripMenuItem
            // 
            this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            this.imageToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.imageToolStripMenuItem.Text = "Image";
            this.imageToolStripMenuItem.Click += new System.EventHandler(this.imageToolStripMenuItem_Click);
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.editarToolStripMenuItem.Text = "Editar";
            this.editarToolStripMenuItem.Click += new System.EventHandler(this.editarToolStripMenuItem_Click);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // iglThumbnails
            // 
            this.iglThumbnails.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iglThumbnails.ImageStream")));
            this.iglThumbnails.TransparentColor = System.Drawing.Color.Transparent;
            this.iglThumbnails.Images.SetKeyName(0, "checkbox.png");
            this.iglThumbnails.Images.SetKeyName(1, "textbox.gif");
            this.iglThumbnails.Images.SetKeyName(2, "text_area.gif");
            this.iglThumbnails.Images.SetKeyName(3, "combobox.gif");
            this.iglThumbnails.Images.SetKeyName(4, "radiobutton.PNG");
            // 
            // lbDocTypeName
            // 
            this.lbDocTypeName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbDocTypeName.AutoSize = true;
            this.lbDocTypeName.Location = new System.Drawing.Point(3, 0);
            this.lbDocTypeName.Name = "lbDocTypeName";
            this.lbDocTypeName.Size = new System.Drawing.Size(35, 13);
            this.lbDocTypeName.TabIndex = 3;
            this.lbDocTypeName.Text = "label1";
            // 
            // pnlIndexes
            // 
            this.pnlIndexes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlIndexes.Controls.Add(this.lvControls);
            this.pnlIndexes.Location = new System.Drawing.Point(3, 16);
            this.pnlIndexes.Name = "pnlIndexes";
            this.pnlIndexes.Size = new System.Drawing.Size(240, 391);
            this.pnlIndexes.TabIndex = 4;
            // 
            // btRefreshIndexes
            // 
            this.btRefreshIndexes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btRefreshIndexes.Location = new System.Drawing.Point(168, 409);
            this.btRefreshIndexes.Name = "btRefreshIndexes";
            this.btRefreshIndexes.Size = new System.Drawing.Size(75, 23);
            this.btRefreshIndexes.TabIndex = 3;
            this.btRefreshIndexes.Text = "Recargar";
            this.btRefreshIndexes.UseVisualStyleBackColor = true;
            this.btRefreshIndexes.Click += new System.EventHandler(this.btRefreshIndexes_Click);
            // 
            // HtmlControlsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btRefreshIndexes);
            this.Controls.Add(this.pnlIndexes);
            this.Controls.Add(this.lbDocTypeName);
            this.Name = "HtmlControlsList";
            this.Size = new System.Drawing.Size(246, 435);
            this.Load += new System.EventHandler(this.HtmlControlsList_Load);
            this.cmsControls.ResumeLayout(false);
            this.pnlIndexes.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
        private System.Windows.Forms.Label lbDocTypeName;
        private System.Windows.Forms.Panel pnlIndexes;
        private System.Windows.Forms.ColumnHeader chType;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.Button btRefreshIndexes;
    }
}