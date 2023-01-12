using System.Windows.Forms;
using Zamba.AppBlock;

namespace Zamba.Grid.Grid
{
    partial class TelerikGrid
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TelerikGrid));
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.ZToolBar1 = new ZToolBar();
            this.lblRegistros = new System.Windows.Forms.ToolStripLabel();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportExcel = new System.Windows.Forms.ToolStripButton();
            this.btnCSV = new System.Windows.Forms.ToolStripButton();
            this.btnPDF = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFit = new System.Windows.Forms.ToolStripButton();
            this.btnFitScreen = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.telerikgrd = new Telerik.WinControls.UI.RadGridView();
            this.ZToolBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.telerikgrd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.telerikgrd.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // ZToolBar1
            // 
            this.ZToolBar1.BackColor = System.Drawing.Color.White;
            this.ZToolBar1.GripStyle = ToolStripGripStyle.Hidden;
            this.ZToolBar1.Items.AddRange(new ToolStripItem[] {
            this.lblRegistros,
            this.ToolStripSeparator1,
            this.btnExportExcel,
            this.btnCSV,
            this.btnPDF,
            this.ToolStripSeparator2,
            this.btnFit,
            this.btnFitScreen,
            this.ToolStripSeparator3});
            this.ZToolBar1.Location = new System.Drawing.Point(0, 0);
            this.ZToolBar1.Name = "ZToolBar1";
            this.ZToolBar1.Size = new System.Drawing.Size(564, 25);
            this.ZToolBar1.TabIndex = 2;
            this.ZToolBar1.Text = "ZToolBar1";
            // 
            // lblRegistros
            // 
            this.lblRegistros.Name = "lblRegistros";
            this.lblRegistros.Size = new System.Drawing.Size(67, 22);
            this.lblRegistros.Text = "Registros: 0";
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnExportExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportExcel.Image")));
            this.btnExportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(23, 22);
            this.btnExportExcel.Text = "Exportar a Excel (Office 2003 o superior)";
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnCSV
            // 
            this.btnCSV.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnCSV.Image = ((System.Drawing.Image)(resources.GetObject("btnCSV.Image")));
            this.btnCSV.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCSV.Name = "btnCSV";
            this.btnCSV.Size = new System.Drawing.Size(23, 22);
            this.btnCSV.Text = "Exporta a CSV";
            this.btnCSV.Click += new System.EventHandler(this.btnCSV_Click);
            // 
            // btnPDF
            // 
            this.btnPDF.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnPDF.Image = ((System.Drawing.Image)(resources.GetObject("btnPDF.Image")));
            this.btnPDF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPDF.Name = "btnPDF";
            this.btnPDF.Size = new System.Drawing.Size(23, 22);
            this.btnPDF.Text = "Exportar a PDF";
            this.btnPDF.Click += new System.EventHandler(this.btnPDF_Click);
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnFit
            // 
            this.btnFit.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnFit.Image = ((System.Drawing.Image)(resources.GetObject("btnFit.Image")));
            this.btnFit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFit.Name = "btnFit";
            this.btnFit.Size = new System.Drawing.Size(23, 22);
            this.btnFit.Text = "Ajustar el ancho de la columna";
            this.btnFit.Click += new System.EventHandler(this.ToolStripButton1_Click);
            // 
            // btnFitScreen
            // 
            this.btnFitScreen.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnFitScreen.Image = ((System.Drawing.Image)(resources.GetObject("btnFitScreen.Image")));
            this.btnFitScreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFitScreen.Name = "btnFitScreen";
            this.btnFitScreen.Size = new System.Drawing.Size(23, 22);
            this.btnFitScreen.Text = "Ajustar a Pantalla";
            this.btnFitScreen.Click += new System.EventHandler(this.ToolStripButton1_Click_1);
            // 
            // ToolStripSeparator3
            // 
            this.ToolStripSeparator3.Name = "ToolStripSeparator3";
            this.ToolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // telerikgrd
            // 
            this.telerikgrd.AutoSizeRows = true;
            this.telerikgrd.BackColor = System.Drawing.Color.White;
            this.telerikgrd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.telerikgrd.ForeColor = System.Drawing.Color.Black;
            this.telerikgrd.Location = new System.Drawing.Point(0, 25);
            // 
            // 
            // 
            this.telerikgrd.MasterTemplate.AllowAddNewRow = false;
            this.telerikgrd.MasterTemplate.AllowCellContextMenu = false;
            this.telerikgrd.MasterTemplate.AllowDeleteRow = false;
            this.telerikgrd.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.telerikgrd.MasterTemplate.DataSource = ((object)(resources.GetObject("telerikgrd.MasterTemplate.DataSource")));
            this.telerikgrd.MasterTemplate.EnableAlternatingRowColor = true;
            this.telerikgrd.MasterTemplate.EnableFiltering = true;
            this.telerikgrd.MasterTemplate.ShowRowHeaderColumn = false;
            this.telerikgrd.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.telerikgrd.Name = "telerikgrd";
            this.telerikgrd.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.None;
            // 
            // 
            // 
            this.telerikgrd.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 25, 240, 150);
            this.telerikgrd.Size = new System.Drawing.Size(564, 520);
            this.telerikgrd.TabIndex = 5;
            this.telerikgrd.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.telerikgrd_DataBindingComplete);
            this.telerikgrd.FilterChanged += new Telerik.WinControls.UI.GridViewCollectionChangedEventHandler(this.telerikgrd_FilterChanged);
            // 
            // TelerikGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.telerikgrd);
            this.Controls.Add(this.ZToolBar1);
            this.Name = "TelerikGrid";
            this.Size = new System.Drawing.Size(564, 545);
            this.ZToolBar1.ResumeLayout(false);
            this.ZToolBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.telerikgrd.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.telerikgrd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZToolBar ZToolBar1;
        private System.Windows.Forms.ToolStripButton btnExportExcel;
        private Telerik.WinControls.UI.RadGridView telerikgrd;
        private System.Windows.Forms.ToolStripButton btnFit;
        private System.Windows.Forms.ToolStripButton btnFitScreen;
        private System.Windows.Forms.ToolStripButton btnCSV;
        private System.Windows.Forms.ToolStripLabel lblRegistros;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnPDF;
    }
}
