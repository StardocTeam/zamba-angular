using System;
using System.Windows.Forms;
using Zamba.AppBlock;

namespace Zamba.ZTC
{
    partial class UCTestCaseDescription
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

                try
                {
                    if (sbMod != null)
                    {
                        sbMod = null;
                    }

                    if (CT != null)
                    {
                        CT = null;
                    }

                    if (User != null)
                    {
                        User.Dispose();
                        User = null;
                    }

                    if (contextMenu != null)
                    {
                        contextMenu.Dispose();
                        contextMenu = null;
                    }
                }
                catch (Exception ex)
                {
                    Zamba.Core.ZClass.raiseerror(ex);
                }
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCTestCaseDescription));
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewComboBoxColumn gridViewComboBoxColumn1 = new Telerik.WinControls.UI.GridViewComboBoxColumn();
            this.radSplitContainer1 = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
            this.lblModifiedBy = new ZLabel();
            this.lblModified = new ZLabel();
            this.lblversion = new ZLabel();
            this.label7 = new ZLabel();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label3 = new ZLabel();
            this.lblmodifieddate = new ZLabel();
            this.label5 = new ZLabel();
            this.lblCreateDate = new ZLabel();
            this.label4 = new ZLabel();
            this.lblAuthor = new ZLabel();
            this.label2 = new ZLabel();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label1 = new ZLabel();
            this.splitPanel2 = new Telerik.WinControls.UI.SplitPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ZToolBar1 = new ZToolBar();
            this.ToolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cmbExportTypes = new System.Windows.Forms.ToolStripComboBox();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.radGridView1 = new Telerik.WinControls.UI.RadGridView();
            this.splitPanel3 = new Telerik.WinControls.UI.SplitPanel();
            this.btnnewversion = new ZButton();
            this.btnEdit = new ZButton();
            this.btnExecute = new ZButton();
            this.btncancel = new ZButton();
            this.btnsave = new ZButton();
            this.toolTipMod = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).BeginInit();
            this.radSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            this.splitPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).BeginInit();
            this.splitPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.ZToolBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel3)).BeginInit();
            this.splitPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // radSplitContainer1
            // 
            this.radSplitContainer1.Controls.Add(this.splitPanel1);
            this.radSplitContainer1.Controls.Add(this.splitPanel2);
            this.radSplitContainer1.Controls.Add(this.splitPanel3);
            this.radSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radSplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.radSplitContainer1.Name = "radSplitContainer1";
            this.radSplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // 
            // 
            this.radSplitContainer1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.radSplitContainer1.Size = new System.Drawing.Size(660, 523);
            this.radSplitContainer1.TabIndex = 0;
            this.radSplitContainer1.TabStop = false;
            this.radSplitContainer1.Text = "radSplitContainer1";
            // 
            // splitPanel1
            // 
            this.splitPanel1.BackColor = System.Drawing.Color.White;
            this.splitPanel1.Controls.Add(this.lblModifiedBy);
            this.splitPanel1.Controls.Add(this.lblModified);
            this.splitPanel1.Controls.Add(this.lblversion);
            this.splitPanel1.Controls.Add(this.label7);
            this.splitPanel1.Controls.Add(this.txtDescription);
            this.splitPanel1.Controls.Add(this.label3);
            this.splitPanel1.Controls.Add(this.lblmodifieddate);
            this.splitPanel1.Controls.Add(this.label5);
            this.splitPanel1.Controls.Add(this.lblCreateDate);
            this.splitPanel1.Controls.Add(this.label4);
            this.splitPanel1.Controls.Add(this.lblAuthor);
            this.splitPanel1.Controls.Add(this.label2);
            this.splitPanel1.Controls.Add(this.txtTitle);
            this.splitPanel1.Controls.Add(this.label1);
            this.splitPanel1.Location = new System.Drawing.Point(0, 0);
            this.splitPanel1.Name = "splitPanel1";
            // 
            // 
            // 
            this.splitPanel1.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 660, 132);
            this.splitPanel1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel1.Size = new System.Drawing.Size(660, 155);
            this.splitPanel1.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, -0.07801419F);
            this.splitPanel1.SizeInfo.MaximumSize = new System.Drawing.Size(0, 132);
            this.splitPanel1.SizeInfo.MinimumSize = new System.Drawing.Size(25, 155);
            this.splitPanel1.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, -41);
            this.splitPanel1.TabIndex = 0;
            this.splitPanel1.TabStop = false;
            this.splitPanel1.Text = "z";
            // 
            // lblModifiedBy
            // 
            this.lblModifiedBy.AutoSize = true;
            this.lblModifiedBy.Location = new System.Drawing.Point(109, 67);
            this.lblModifiedBy.Name = "lblModifiedBy";
            this.lblModifiedBy.Size = new System.Drawing.Size(61, 13);
            this.lblModifiedBy.TabIndex = 13;
            this.lblModifiedBy.Text = "Sin Definir";
            // 
            // lblModified
            // 
            this.lblModified.AutoSize = true;
            this.lblModified.Location = new System.Drawing.Point(14, 67);
            this.lblModified.Name = "lblModified";
            this.lblModified.Size = new System.Drawing.Size(89, 13);
            this.lblModified.TabIndex = 12;
            this.lblModified.Text = "Modificado Por:";
            // 
            // lblversion
            // 
            this.lblversion.AutoSize = true;
            this.lblversion.Location = new System.Drawing.Point(555, 17);
            this.lblversion.Name = "lblversion";
            this.lblversion.Size = new System.Drawing.Size(13, 13);
            this.lblversion.TabIndex = 11;
            this.lblversion.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(500, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Version:";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(17, 109);
            this.txtDescription.MaxLength = 10000;
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(624, 43);
            this.txtDescription.TabIndex = 9;
            this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Descripcion:";
            // 
            // lblmodifieddate
            // 
            this.lblmodifieddate.AutoSize = true;
            this.lblmodifieddate.Location = new System.Drawing.Point(366, 67);
            this.lblmodifieddate.Name = "lblmodifieddate";
            this.lblmodifieddate.Size = new System.Drawing.Size(61, 13);
            this.lblmodifieddate.TabIndex = 7;
            this.lblmodifieddate.Text = "Sin Definir";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(291, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Modificado:";
            // 
            // lblCreateDate
            // 
            this.lblCreateDate.AutoSize = true;
            this.lblCreateDate.Location = new System.Drawing.Point(366, 45);
            this.lblCreateDate.Name = "lblCreateDate";
            this.lblCreateDate.Size = new System.Drawing.Size(61, 13);
            this.lblCreateDate.TabIndex = 5;
            this.lblCreateDate.Text = "Sin Definir";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(313, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Creado:";
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Location = new System.Drawing.Point(60, 45);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(61, 13);
            this.lblAuthor.TabIndex = 3;
            this.lblAuthor.Text = "Sin Definir";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Autor:";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(60, 14);
            this.txtTitle.MaxLength = 500;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(434, 20);
            this.txtTitle.TabIndex = 1;
            this.txtTitle.TextChanged += new System.EventHandler(this.txtTitle_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Titulo:";
            // 
            // splitPanel2
            // 
            this.splitPanel2.BackColor = System.Drawing.Color.White;
            this.splitPanel2.Controls.Add(this.splitContainer1);
            this.splitPanel2.Location = new System.Drawing.Point(0, 158);
            this.splitPanel2.Name = "splitPanel2";
            // 
            // 
            // 
            this.splitPanel2.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel2.Size = new System.Drawing.Size(660, 313);
            this.splitPanel2.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, 0.31657F);
            this.splitPanel2.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, 164);
            this.splitPanel2.TabIndex = 1;
            this.splitPanel2.TabStop = false;
            this.splitPanel2.Text = "splitPanel2";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ZToolBar1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.radGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(660, 313);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.TabIndex = 1;
            // 
            // ZToolBar1
            // 
            this.ZToolBar1.Items.AddRange(new ToolStripItem[] {
            this.ToolStripLabel1,
            this.cmbExportTypes,
            this.btnExport});
            this.ZToolBar1.Location = new System.Drawing.Point(0, 0);
            this.ZToolBar1.Name = "ZToolBar1";
            this.ZToolBar1.Size = new System.Drawing.Size(660, 25);
            this.ZToolBar1.TabIndex = 0;
            this.ZToolBar1.Text = "TestCaseBar";
            // 
            // ToolStripLabel1
            // 
            this.ToolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.ToolStripLabel1.Name = "ToolStripLabel1";
            this.ToolStripLabel1.Size = new System.Drawing.Size(67, 22);
            this.ToolStripLabel1.Text = "Exportar a:";
            // 
            // cmbExportTypes
            // 
            this.cmbExportTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExportTypes.Name = "cmbExportTypes";
            this.cmbExportTypes.Size = new System.Drawing.Size(80, 25);
            // 
            // btnExport
            // 
            this.btnExport.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(23, 22);
            this.btnExport.Text = "Exportar";
            this.btnExport.ToolTipText = "Exportar";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // radGridView1
            // 
            this.radGridView1.AutoScroll = true;
            this.radGridView1.AutoSizeRows = true;
            this.radGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGridView1.Location = new System.Drawing.Point(0, 0);
            // 
            // radGridView1
            // 
            this.radGridView1.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom;
            this.radGridView1.MasterTemplate.AllowColumnReorder = false;
            this.radGridView1.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            gridViewTextBoxColumn1.FieldName = "StepId";
            gridViewTextBoxColumn1.FormatString = "";
            gridViewTextBoxColumn1.HeaderText = "Id";
            gridViewTextBoxColumn1.Name = "StepId";
            gridViewTextBoxColumn1.ReadOnly = true;
            gridViewTextBoxColumn1.Width = 31;
            gridViewTextBoxColumn2.FieldName = "StepDescription";
            gridViewTextBoxColumn2.FormatString = "";
            gridViewTextBoxColumn2.HeaderText = "Descripcion";
            gridViewTextBoxColumn2.Name = "StepDescription";
            gridViewTextBoxColumn2.Width = 259;
            gridViewTextBoxColumn3.FieldName = "StepObservation";
            gridViewTextBoxColumn3.FormatString = "";
            gridViewTextBoxColumn3.HeaderText = "Observaciones";
            gridViewTextBoxColumn3.Name = "StepObservation";
            gridViewTextBoxColumn3.Width = 259;
            gridViewComboBoxColumn1.DisplayMember = null;
            gridViewComboBoxColumn1.FieldName = "StepTypeId";
            gridViewComboBoxColumn1.FormatString = "";
            gridViewComboBoxColumn1.HeaderText = "Tipo";
            gridViewComboBoxColumn1.Name = "StepTypeId";
            gridViewComboBoxColumn1.ValueMember = null;
            gridViewComboBoxColumn1.Width = 94;
            this.radGridView1.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewComboBoxColumn1});
            this.radGridView1.MasterTemplate.EnableAlternatingRowColor = true;
            this.radGridView1.MasterTemplate.EnableGrouping = false;
            this.radGridView1.MasterTemplate.EnableSorting = false;
            this.radGridView1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect;
            this.radGridView1.Name = "radGridView1";
            this.radGridView1.Size = new System.Drawing.Size(660, 284);
            this.radGridView1.TabIndex = 1;
            this.radGridView1.Text = "radGridView1";
            this.radGridView1.DefaultValuesNeeded += new Telerik.WinControls.UI.GridViewRowEventHandler(this.radGridView1_DefaultValuesNeeded_1);
            this.radGridView1.ToolTipTextNeeded += new Telerik.WinControls.ToolTipTextNeededEventHandler(this.radGridView1_ToolTipTextNeeded);
            // 
            // splitPanel3
            // 
            this.splitPanel3.BackColor = System.Drawing.Color.White;
            this.splitPanel3.Controls.Add(this.btnnewversion);
            this.splitPanel3.Controls.Add(this.btnEdit);
            this.splitPanel3.Controls.Add(this.btnExecute);
            this.splitPanel3.Controls.Add(this.btncancel);
            this.splitPanel3.Controls.Add(this.btnsave);
            this.splitPanel3.Location = new System.Drawing.Point(0, 474);
            this.splitPanel3.Name = "splitPanel3";
            // 
            // 
            // 
            this.splitPanel3.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel3.Size = new System.Drawing.Size(660, 49);
            this.splitPanel3.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, -0.2385558F);
            this.splitPanel3.SizeInfo.MaximumSize = new System.Drawing.Size(0, 49);
            this.splitPanel3.SizeInfo.MinimumSize = new System.Drawing.Size(25, 49);
            this.splitPanel3.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, -123);
            this.splitPanel3.TabIndex = 2;
            this.splitPanel3.TabStop = false;
            this.splitPanel3.Text = "splitPanel3";
            // 
            // btnnewversion
            // 
            this.btnnewversion.Location = new System.Drawing.Point(254, 13);
            this.btnnewversion.Name = "btnnewversion";
            this.btnnewversion.Size = new System.Drawing.Size(106, 23);
            this.btnnewversion.TabIndex = 0;
            this.btnnewversion.Text = "Nueva Version";
            this.btnnewversion.Click += new System.EventHandler(this.btnnewversion_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(156, 13);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "Editar";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(17, 13);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(75, 23);
            this.btnExecute.TabIndex = 2;
            this.btnExecute.Text = "Ejecutar";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btncancel
            // 
            this.btncancel.Location = new System.Drawing.Point(474, 13);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(75, 23);
            this.btncancel.TabIndex = 1;
            this.btncancel.Text = "Cancelar";
            this.btncancel.UseVisualStyleBackColor = true;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // btnsave
            // 
            this.btnsave.Location = new System.Drawing.Point(393, 13);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(75, 23);
            this.btnsave.TabIndex = 0;
            this.btnsave.Text = "Guardar";
            this.btnsave.UseVisualStyleBackColor = true;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // UCTestCaseDescription
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radSplitContainer1);
            this.Name = "UCTestCaseDescription";
            this.Size = new System.Drawing.Size(660, 523);
            this.Load += new System.EventHandler(this.UCTestCaseDescription_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).EndInit();
            this.radSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            this.splitPanel1.ResumeLayout(false);
            this.splitPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).EndInit();
            this.splitPanel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ZToolBar1.ResumeLayout(false);
            this.ZToolBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel3)).EndInit();
            this.splitPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadSplitContainer radSplitContainer1;
        private Telerik.WinControls.UI.SplitPanel splitPanel1;
        private System.Windows.Forms.TextBox txtDescription;
        private ZLabel label3;
        private ZLabel lblmodifieddate;
        private ZLabel label5;
        private ZLabel lblCreateDate;
        private ZLabel label4;
        private ZLabel lblAuthor;
        private ZLabel label2;
        private System.Windows.Forms.TextBox txtTitle;
        private ZLabel label1;
        private Telerik.WinControls.UI.SplitPanel splitPanel2;
        private Telerik.WinControls.UI.SplitPanel splitPanel3;
        private ZButton btncancel;
        private ZButton btnsave;
        private ZButton btnExecute;
        private ZButton btnEdit;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Telerik.WinControls.UI.RadGridView radGridView1;
        private ZToolBar ZToolBar1;
        private System.Windows.Forms.ToolStripLabel ToolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cmbExportTypes;
        private System.Windows.Forms.ToolStripButton btnExport;
        private ZButton btnnewversion;
        private ZLabel lblversion;
        private ZLabel label7;
        private ZLabel lblModifiedBy;
        private ZLabel lblModified;
        private System.Windows.Forms.ToolTip toolTipMod;
    }
}
