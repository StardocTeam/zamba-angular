using Zamba.AppBlock;

namespace ExportaOutlook.Forms
{
    partial class FrmMapEntityFolders
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
            this.btnCancel = new ZButton();
            this.btnOK = new ZButton();
            this.btnRemoveMap = new ZButton();
            this.dgvMaps = new System.Windows.Forms.DataGridView();
            this.btnMap = new ZButton();
            this.cmbDocTypes = new System.Windows.Forms.ComboBox();
            this.lblOutlookFolders = new ZLabel();
            this.lblDocTypes = new ZLabel();
            this.btnAddFolder = new ZButton();
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.grpExportaCfg = new System.Windows.Forms.GroupBox();
            this.label1 = new ZLabel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.chkEnableExporta = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaps)).BeginInit();
            this.grpExportaCfg.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(157)))), ((int)(((byte)(224)))));
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(654, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(157)))), ((int)(((byte)(224)))));
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(520, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 25);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "Aceptar";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnRemoveMap
            // 
            this.btnRemoveMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveMap.Location = new System.Drawing.Point(144, 192);
            this.btnRemoveMap.Name = "btnRemoveMap";
            this.btnRemoveMap.Size = new System.Drawing.Size(115, 23);
            this.btnRemoveMap.TabIndex = 22;
            this.btnRemoveMap.Text = "Remover relación";
            this.btnRemoveMap.UseVisualStyleBackColor = true;
            this.btnRemoveMap.Click += new System.EventHandler(this.btnRemoveMap_Click);
            // 
            // dgvMaps
            // 
            this.dgvMaps.AllowUserToAddRows = false;
            this.dgvMaps.AllowUserToDeleteRows = false;
            this.dgvMaps.AllowUserToResizeRows = false;
            this.dgvMaps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMaps.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvMaps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaps.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvMaps.Location = new System.Drawing.Point(9, 221);
            this.dgvMaps.MultiSelect = false;
            this.dgvMaps.Name = "dgvMaps";
            this.dgvMaps.RowHeadersVisible = false;
            this.dgvMaps.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvMaps.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMaps.ShowCellErrors = false;
            this.dgvMaps.ShowEditingIcon = false;
            this.dgvMaps.ShowRowErrors = false;
            this.dgvMaps.Size = new System.Drawing.Size(760, 150);
            this.dgvMaps.TabIndex = 21;
            // 
            // btnMap
            // 
            this.btnMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMap.Location = new System.Drawing.Point(9, 192);
            this.btnMap.Name = "btnMap";
            this.btnMap.Size = new System.Drawing.Size(115, 23);
            this.btnMap.TabIndex = 18;
            this.btnMap.Text = "Agregar relación";
            this.btnMap.UseVisualStyleBackColor = true;
            this.btnMap.Click += new System.EventHandler(this.btnMap_Click);
            // 
            // cmbDocTypes
            // 
            this.cmbDocTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDocTypes.FormattingEnabled = true;
            this.cmbDocTypes.Location = new System.Drawing.Point(9, 96);
            this.cmbDocTypes.MaxDropDownItems = 10;
            this.cmbDocTypes.Name = "cmbDocTypes";
            this.cmbDocTypes.Size = new System.Drawing.Size(725, 21);
            this.cmbDocTypes.TabIndex = 17;
            // 
            // lblOutlookFolders
            // 
            this.lblOutlookFolders.AutoSize = true;
            this.lblOutlookFolders.BackColor = System.Drawing.Color.Transparent;
            this.lblOutlookFolders.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutlookFolders.Location = new System.Drawing.Point(6, 27);
            this.lblOutlookFolders.Name = "lblOutlookFolders";
            this.lblOutlookFolders.Size = new System.Drawing.Size(110, 15);
            this.lblOutlookFolders.TabIndex = 13;
            this.lblOutlookFolders.Text = "Carpeta de outlook";
            // 
            // lblDocTypes
            // 
            this.lblDocTypes.AutoSize = true;
            this.lblDocTypes.BackColor = System.Drawing.Color.Transparent;
            this.lblDocTypes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocTypes.Location = new System.Drawing.Point(6, 76);
            this.lblDocTypes.Name = "lblDocTypes";
            this.lblDocTypes.Size = new System.Drawing.Size(49, 15);
            this.lblDocTypes.TabIndex = 14;
            this.lblDocTypes.Text = "Entidad";
            // 
            // btnAddFolder
            // 
            this.btnAddFolder.Location = new System.Drawing.Point(740, 47);
            this.btnAddFolder.Name = "btnAddFolder";
            this.btnAddFolder.Size = new System.Drawing.Size(29, 20);
            this.btnAddFolder.TabIndex = 15;
            this.btnAddFolder.Text = "...";
            this.btnAddFolder.UseVisualStyleBackColor = true;
            this.btnAddFolder.Click += new System.EventHandler(this.btnAddFolder_Click);
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Location = new System.Drawing.Point(9, 47);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.ReadOnly = true;
            this.txtFolderPath.Size = new System.Drawing.Size(725, 20);
            this.txtFolderPath.TabIndex = 16;
            // 
            // grpExportaCfg
            // 
            this.grpExportaCfg.Controls.Add(this.label1);
            this.grpExportaCfg.Controls.Add(this.textBox1);
            this.grpExportaCfg.Controls.Add(this.lblOutlookFolders);
            this.grpExportaCfg.Controls.Add(this.btnRemoveMap);
            this.grpExportaCfg.Controls.Add(this.txtFolderPath);
            this.grpExportaCfg.Controls.Add(this.dgvMaps);
            this.grpExportaCfg.Controls.Add(this.btnAddFolder);
            this.grpExportaCfg.Controls.Add(this.btnMap);
            this.grpExportaCfg.Controls.Add(this.lblDocTypes);
            this.grpExportaCfg.Controls.Add(this.cmbDocTypes);
            this.grpExportaCfg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpExportaCfg.Location = new System.Drawing.Point(0, 0);
            this.grpExportaCfg.Name = "grpExportaCfg";
            this.grpExportaCfg.Size = new System.Drawing.Size(775, 427);
            this.grpExportaCfg.TabIndex = 23;
            this.grpExportaCfg.TabStop = false;
            //this.grpExportaCfg.Enter += new System.EventHandler(this.grpExportaCfg_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 15);
            this.label1.TabIndex = 24;
            this.label1.Text = "Filtro";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.textBox1.Location = new System.Drawing.Point(9, 143);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(725, 20);
            this.textBox1.TabIndex = 23;
            this.textBox1.Text = "[MessageClass]=\'IPM.Note\' AND [Mileage] <> \'1\' AND [FlagRequest] <> \'Exportado a " +
    "Zamba\'";
            // 
            // chkEnableExporta
            // 
            this.chkEnableExporta.AutoSize = true;
            this.chkEnableExporta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableExporta.Location = new System.Drawing.Point(9, 10);
            this.chkEnableExporta.Name = "chkEnableExporta";
            this.chkEnableExporta.Size = new System.Drawing.Size(266, 19);
            this.chkEnableExporta.TabIndex = 23;
            this.chkEnableExporta.Text = "Habilitar la exportación automática de mails";
            this.chkEnableExporta.UseVisualStyleBackColor = true;
            this.chkEnableExporta.CheckedChanged += new System.EventHandler(this.chkEnableExporta_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.chkEnableExporta);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 427);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(775, 43);
            this.panel1.TabIndex = 24;
            // 
            // FrmMapEntityFolders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(775, 470);
            this.Controls.Add(this.grpExportaCfg);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "FrmMapEntityFolders";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configurar carpetas de exportación automática";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmFolderMap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaps)).EndInit();
            this.grpExportaCfg.ResumeLayout(false);
            this.grpExportaCfg.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ZButton btnCancel;
        private ZButton btnOK;
        private ZButton btnRemoveMap;
        private System.Windows.Forms.DataGridView dgvMaps;
        private ZButton btnMap;
        private System.Windows.Forms.ComboBox cmbDocTypes;
        private ZLabel lblOutlookFolders;
        private ZLabel lblDocTypes;
        private ZButton btnAddFolder;
        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.GroupBox grpExportaCfg;
        private System.Windows.Forms.CheckBox chkEnableExporta;
        private ZLabel label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
    }
}