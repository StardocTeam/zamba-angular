using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI.Export;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI.Localization;
using Telerik.WinControls.UI;
using Zamba.Core;
using Zamba.AppBlock;

namespace Zamba.Grid.Grid 
{
    public partial class PageSizeSettingsDialog : Form
    {
        private Label lblPosition;
        private ComboBox cboSizes;
        private RadioButton rdoVertical;
        private RadioButton rdoHorizontal;
        private Button btnOk;
        private Button btnCancel;
        private Label lblColumns;
        private CheckBox chkFitToPageWidth;
        private Label lblSize;
    
        public ExportSettings.PDF PdfExportSettings { get; set; }

        public bool FitToPageWidth
        {
            get { return chkFitToPageWidth.Checked; }
            set { chkFitToPageWidth.Checked = value; }
        }

        public PageSizeSettingsDialog()
        {
            InitializeComponent();

            PdfExportSettings = new ExportSettings.PDF();

            //Completa los tamaños de página
            cboSizes.Items.AddRange(Enum.GetNames(typeof(PaperSizes)));
            cboSizes.Text = "A4";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //Se obtiene el tamaño
            PaperSize size = new PaperSize((PaperSizes) Enum.Parse(typeof(PaperSizes), cboSizes.Text));
            if (rdoVertical.Checked)
            {
                PdfExportSettings.PageHeight = size.Alto;
                PdfExportSettings.PageWidth = size.Ancho;
            }
            else
            {
                PdfExportSettings.PageHeight = size.Ancho;
                PdfExportSettings.PageWidth = size.Alto;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// ESTA METIDO ACA Y NO EN UN DESIGNER PORQUE TUVE UN ERROR Y LO PERDI.
        /// AL VOLVER A CREAR LOS CONTROLES EN VEZ DE GENERAR EL DESIGNER METIO
        /// TODO EN ESTE METODO DENTRO DE LA CLASE.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblSize = new ZLabel();
            this.lblPosition = new ZLabel();
            this.cboSizes = new System.Windows.Forms.ComboBox();
            this.rdoVertical = new System.Windows.Forms.RadioButton();
            this.rdoHorizontal = new System.Windows.Forms.RadioButton();
            this.btnOk = new ZButton();
            this.btnCancel = new ZButton();
            this.lblColumns = new ZLabel();
            this.chkFitToPageWidth = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(12, 15);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(49, 13);
            this.lblSize.TabIndex = 0;
            this.lblSize.Text = "Tamaño:";
            // 
            // lblPosition
            // 
            this.lblPosition.AutoSize = true;
            this.lblPosition.Location = new System.Drawing.Point(12, 42);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(50, 13);
            this.lblPosition.TabIndex = 1;
            this.lblPosition.Text = "Posición:";
            // 
            // cboSizes
            // 
            this.cboSizes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSizes.FormattingEnabled = true;
            this.cboSizes.Location = new System.Drawing.Point(79, 12);
            this.cboSizes.Name = "cboSizes";
            this.cboSizes.Size = new System.Drawing.Size(201, 21);
            this.cboSizes.TabIndex = 2;
            // 
            // rdoVertical
            // 
            this.rdoVertical.AutoSize = true;
            this.rdoVertical.Checked = true;
            this.rdoVertical.Location = new System.Drawing.Point(79, 40);
            this.rdoVertical.Name = "rdoVertical";
            this.rdoVertical.Size = new System.Drawing.Size(60, 17);
            this.rdoVertical.TabIndex = 3;
            this.rdoVertical.TabStop = true;
            this.rdoVertical.Text = "Vertical";
            this.rdoVertical.UseVisualStyleBackColor = true;
            // 
            // rdoHorizontal
            // 
            this.rdoHorizontal.AutoSize = true;
            this.rdoHorizontal.Location = new System.Drawing.Point(170, 40);
            this.rdoHorizontal.Name = "rdoHorizontal";
            this.rdoHorizontal.Size = new System.Drawing.Size(72, 17);
            this.rdoHorizontal.TabIndex = 4;
            this.rdoHorizontal.TabStop = true;
            this.rdoHorizontal.Text = "Horizontal";
            this.rdoHorizontal.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(123, 131);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "&Aceptar";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(205, 131);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblColumns
            // 
            this.lblColumns.AutoSize = true;
            this.lblColumns.Location = new System.Drawing.Point(12, 68);
            this.lblColumns.Name = "lblColumns";
            this.lblColumns.Size = new System.Drawing.Size(56, 13);
            this.lblColumns.TabIndex = 7;
            this.lblColumns.Text = "Columnas:";
            // 
            // chkFitToPageWidth
            // 
            this.chkFitToPageWidth.AutoSize = true;
            this.chkFitToPageWidth.Checked = true;
            this.chkFitToPageWidth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFitToPageWidth.Location = new System.Drawing.Point(79, 68);
            this.chkFitToPageWidth.Name = "chkFitToPageWidth";
            this.chkFitToPageWidth.Size = new System.Drawing.Size(163, 17);
            this.chkFitToPageWidth.TabIndex = 8;
            this.chkFitToPageWidth.Text = "Ajustar al ancho de la página";
            this.chkFitToPageWidth.UseVisualStyleBackColor = true;
            // 
            // PageSizeSettingsDialog
            // 
            this.ClientSize = new System.Drawing.Size(292, 168);
            this.Controls.Add(this.chkFitToPageWidth);
            this.Controls.Add(this.lblColumns);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.rdoHorizontal);
            this.Controls.Add(this.rdoVertical);
            this.Controls.Add(this.cboSizes);
            this.Controls.Add(this.lblPosition);
            this.Controls.Add(this.lblSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PageSizeSettingsDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Propiedades de Hoja";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
