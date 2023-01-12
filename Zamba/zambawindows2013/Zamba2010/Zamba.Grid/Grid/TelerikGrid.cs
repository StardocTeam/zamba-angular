using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;
using Telerik.WinControls.UI.Localization;
using Zamba.AppBlock;
using Zamba.Core;

namespace Zamba.Grid.Grid
{
    public partial class TelerikGrid : UserControl
    {

        public List<string> HiddenColumnsToExport;

        public TelerikGrid()
        {
            InitializeComponent();
            this.ZToolBar1.Renderer = new MyRenderer();

        }

        public TelerikGrid(DataTable dt, bool shortDateFormat)
        {
            InitializeComponent();
            this.ZToolBar1.Renderer = new MyRenderer();
            this.telerikgrd.TableElement.BeginUpdate();
            this.telerikgrd.DataSource = dt;
            this.telerikgrd.TableElement.EndUpdate();

            RadGridLocalizationProvider.CurrentProvider = new SpanishRadGridLocalizationProvider();
            this.telerikgrd.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None;

            if (shortDateFormat)
            {
                foreach (var column in telerikgrd.Columns)
                {
                    if (String.Compare(column.DataType.ToString(), "System.DateTime") == 0)
                    {
                        //short date format microsoft convension
                        column.FormatString = "{0:d}";
                    }
                }
            }

            fixColumns();
            HiddenColumnsToExport = new List<string>();
        }

        public bool MultiSelect
        {
            get
            {
                return this.telerikgrd.MultiSelect;
            }
            set
            {
                this.telerikgrd.MultiSelect = value;
            }
        }

        public object DataSource
        {
            get
            {
                return this.telerikgrd.DataSource;
            }
            set
            {
                this.telerikgrd.TableElement.BeginUpdate();
                this.telerikgrd.DataSource = value;
                this.telerikgrd.TableElement.EndUpdate();
            }
        }

        public void SetColumnVisible(string name, Boolean bolVisible)
        {
            try
            {
                if (this.telerikgrd.Columns.Contains(name))
                {
                    this.telerikgrd.Columns[name].IsVisible = bolVisible;
                }
            }
            catch (Exception ex)
            {
                ZException.Log(ex);
            }
        }

        private void HideColumnsToExport()
        {
            foreach (string column in HiddenColumnsToExport)
                this.SetColumnVisible(column, false);
        }

        private void ShowColumnsAfterExport()
        {
            foreach (string column in HiddenColumnsToExport)
                this.SetColumnVisible(column, true);
        }

        public void SetColumnReadOnly(string name, Boolean IsReadOnly)
        {
            try
            {
                if (this.telerikgrd.Columns.Contains(name))
                {
                    this.telerikgrd.Columns[name].ReadOnly = IsReadOnly;
                }
            }
            catch (Exception ex)
            {
                ZException.Log(ex);
            }
        }

        public void SetColumnFontColor(int row, string column, System.Drawing.Color color)
        {
            try
            {
                this.telerikgrd.Rows[row].Cells[column].Style.CustomizeFill = true;
                this.telerikgrd.Rows[row].Cells[column].Style.ForeColor = color;
                this.telerikgrd.Rows[row].Cells[column].Style.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                ZException.Log(ex);
            }
        }

        /// <summary>
        /// Obtiene una tabla con los valores seleccionados
        /// </summary>
        public object[][] SelectedRows
        {
            get
            {
                //Declaración de la tabla indicando el número de filas seleccionadas          
                object[][] table = new object[this.telerikgrd.SelectedRows.Count][];

                //Se recorren las filas seleccionadas
                for (int row = 0; row < this.telerikgrd.SelectedRows.Count; row++)
                {
                    //Se declaran las columnas por cada fila
                    table[row] = new object[this.telerikgrd.ColumnCount];

                    //Se recorren las filas y columnas completando los valores seleccionados
                    for (int col = 0; col < this.telerikgrd.ColumnCount; col++)
                        table[row][col] = this.telerikgrd.SelectedRows[row].Cells[col].Value;
                }

                return table;
            }
        }

        public void RemoveSelectedRows()
        {
            if (this.telerikgrd.SelectedRows.Count > 0)
            {
                List<int> indexes = new List<int>();
                Int32 count = this.telerikgrd.SelectedRows.Count;

                //Agrega los índices de las filas a eliminar en orden inverso
                for (int i = this.telerikgrd.SelectedRows.Count - 1; i != -1; i--)
                {
                    indexes.Add(this.telerikgrd.SelectedRows[i].Index);
                }

                //Se eliminan en orden inverso para no generar errores
                while (indexes.Count != 0)
                {
                    this.telerikgrd.Rows.RemoveAt(indexes[0]);
                    indexes.RemoveAt(0);
                }
            }
        }

        public void GroupByColumnName(string columname)
        {
            try
            {
                if (string.IsNullOrEmpty(columname) == false)
                {
                    if (this.telerikgrd.Columns.Contains(columname))
                    {
                        this.telerikgrd.EnableGrouping = true;
                        GroupDescriptor descriptor = new GroupDescriptor();
                        String[] Columnnames = columname.Split(char.Parse(","));

                        foreach (String s in Columnnames)
                            descriptor.GroupNames.Add(s, ListSortDirection.Ascending);

                        this.telerikgrd.GroupDescriptors.Add(descriptor);
                    }
                }
            }
            catch (Exception ex)
            {
                ZException.Log(ex);
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {

            ExportToExcelML expExcel;
            SaveFileDialog sfd = new SaveFileDialog();
            try
            {
                sfd.Title = "Ingrese la ruta y el nombre del archivo de Excel";
                sfd.Filter = "excel files (*.xls)|*.xls";

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.SuspendLayout();
                    HideColumnsToExport();
                    expExcel = new ExportToExcelML(telerikgrd);
                    expExcel.HiddenColumnOption = Telerik.WinControls.UI.Export.HiddenOption.DoNotExport;
                    expExcel.ExportVisualSettings = true;
                    expExcel.FileExtension = "xls";
                    expExcel.RunExport(sfd.FileName);
                    ShowColumnsAfterExport();

                    MessageBox.Show("Exportacion realizada con exito.\nPuede realizar la apertura del archivo sin inconvenientes.", "Zamba Software", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                ShowColumnsAfterExport();
                ZException.Log(ex);
                MessageBox.Show("Ha ocurrido un error en la exportacion.", "Zamba Software", MessageBoxButtons.OK);
            }
            finally
            {
                sfd.Dispose();
                sfd = null;
                expExcel = null;
                this.ResumeLayout();
            }
        }

        private void btnCSV_Click(object sender, EventArgs e)
        {
            ExportToCSV expExcel;
            SaveFileDialog sfd = new SaveFileDialog();
            try
            {
                sfd.Title = "Ingrese la ruta y el nombre del archivo de Excel";
                sfd.Filter = "excel files (*.csv)|*.csv";

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.SuspendLayout();
                    HideColumnsToExport();
                    expExcel = new ExportToCSV(telerikgrd);
                    expExcel.RunExport(sfd.FileName);
                    ShowColumnsAfterExport();
                    MessageBox.Show("Exportacion realizada con exito.\nPuede realizar la apertura del archivo sin inconvenientes.", "Zamba Software", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                ShowColumnsAfterExport();
                ZException.Log(ex);
                MessageBox.Show("Ha ocurrido un error en la exportacion.", "Zamba Software", MessageBoxButtons.OK);
            }
            finally
            {
                sfd.Dispose();
                sfd = null;
                expExcel = null;
                this.ResumeLayout();
            }
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            ExportToPDF expPDf;
            SaveFileDialog sfd = new SaveFileDialog();
            PageSizeSettingsDialog frmPdfSettings = null;

            try
            {
                sfd.Title = "Ingrese la ruta y el nombre del archivo de PDF";
                sfd.Filter = "PDF files (*.pdf)|*.pdf";

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    frmPdfSettings = new PageSizeSettingsDialog();

                    if (frmPdfSettings.ShowDialog() == DialogResult.OK)
                    {
                        this.SuspendLayout();
                        HideColumnsToExport();

                        //Propiedades de exportación de página
                        expPDf = new ExportToPDF(telerikgrd);
                        expPDf.PdfExportSettings = frmPdfSettings.PdfExportSettings;
                        expPDf.FitToPageWidth = frmPdfSettings.FitToPageWidth;
                        expPDf.ExportVisualSettings = true;
                        expPDf.RunExport(sfd.FileName);

                        ShowColumnsAfterExport();
                        MessageBox.Show("Exportacion realizada con exito.\nPuede realizar la apertura del archivo sin inconvenientes.", "Zamba Software", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowColumnsAfterExport();
                ZException.Log(ex);
                MessageBox.Show("Ha ocurrido un error en la exportacion.", "Zamba Software", MessageBoxButtons.OK);
            }
            finally
            {
                sfd.Dispose();
                sfd = null;
                expPDf = null;
                this.ResumeLayout();
            }
        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            fixColumns();
        }

        private void fixColumns()
        {
            this.telerikgrd.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None;
            foreach (var column in telerikgrd.Columns)
            {
                String maxString = string.Empty;
                foreach (var row in telerikgrd.Rows)
                {
                    if (row.Cells[column.Name].Value != null)
                    {
                        if (maxString.Length < row.Cells[column.Name].Value.ToString().Trim().Length)
                            maxString = row.Cells[column.Name].Value.ToString().Trim();

                    }
                }

                if (maxString.Length > column.HeaderText.Trim().Length)
                {
                    column.Width = (int)(this.CreateGraphics().MeasureString(maxString, this.Font).Width + 8);
                }
                else
                    column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText.Trim(), this.Font).Width + 8);
            }
        }

        private void ToolStripButton1_Click_1(object sender, EventArgs e)
        {
            if (this.telerikgrd.MasterTemplate.AutoSizeColumnsMode == GridViewAutoSizeColumnsMode.None)
            {
                this.telerikgrd.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            }
            else
                this.telerikgrd.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None;
        }

        private void telerikgrd_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            lblRegistros.Text = "Registros: " + telerikgrd.ChildRows.Count.ToString() + " de " + telerikgrd.Rows.Count.ToString();
        }

        private void telerikgrd_FilterChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            lblRegistros.Text = "Registros: " + telerikgrd.ChildRows.Count.ToString() + " de " + telerikgrd.Rows.Count.ToString();
        }

        public void addClickEventHandler()
        {
            this.telerikgrd.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.telerikgrd_CellClick);

        }

        private void telerikgrd_CellClick(object sender, GridViewCellEventArgs e)
        {
            try
            {
                if (this.telerikgrd.Columns.Contains("hddnClmn"))
                {
                    if (String.Compare(e.Value.ToString(), "Ver") == 0)
                    {
                        string ZambaLink = this.telerikgrd.SelectedRows[0].Cells["hddnClmn"].Value.ToString();

                        System.Diagnostics.Process proc = new System.Diagnostics.Process();
                        proc.EnableRaisingEvents = false;
                        proc.StartInfo.FileName = "Cliente.exe";
                        proc.StartInfo.Arguments = ZambaLink;
                        proc.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                ZException.Log(ex);
            }
        }

        class MyRenderer : ToolStripProfessionalRenderer
        {
            public MyRenderer()
            {
                RoundedEdges = false;
            }
        }

    }


}
