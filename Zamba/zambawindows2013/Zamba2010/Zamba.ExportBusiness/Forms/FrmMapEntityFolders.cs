using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using OutlookPanel;
//using Zamba.ExportaOutlook.RServices;
//using Zamba.Services.RemoteEntities;
//using Zamba.Services.RemoteInterfaces;
using Zamba.Core;
using Office = Microsoft.Office.Core;

using Outlook = Microsoft.Office.Interop.Outlook;
using System.Collections;
using ExportaOutlook.Helper;
using Zamba.Services.RemoteEntities;
using Zamba.Services.RemoteInterfaces;
using Zamba.Services.Remoting;
using Zamba;

namespace ExportaOutlook.Forms
{
    public partial class FrmMapEntityFolders : Form
    {

        ZambaRemoteClass ZRC = new ZambaRemoteClass();


        Outlook.Application app = null;
     
        FolderInfo fiMap;
        Int32 colEntryId, colDocTypeId, colDocTypeName, colFolderName, colAttachId;
        List<IFolderMap> lstMaps = null;

        public bool IsAutomatedExport { get { return chkEnableExporta.Checked;} }

        #region Constructores
        public FrmMapEntityFolders()
        {
            InitializeComponent();
        }
        public FrmMapEntityFolders( Outlook.Application app, bool automatedExport)
        {
            InitializeComponent();
            
          
            this.app = app;
            chkEnableExporta.Checked = automatedExport;
            grpExportaCfg.Enabled = automatedExport;
            
            ZTrace.WriteLineIf(ZTrace.IsError, "AutomatedExport " + automatedExport.ToString());

        }
        #endregion

        private void frmFolderMap_Load(object sender, EventArgs e)
        {
            try
            {
                //Se cargan los tipos de documento en el combo.
                DataTable dtDocTypes = ZRC.GetDocTypeNamesAndIds();
                cmbDocTypes.DataSource = dtDocTypes;
                cmbDocTypes.ValueMember = dtDocTypes.Columns[0].ColumnName;
                cmbDocTypes.DisplayMember = dtDocTypes.Columns[1].ColumnName;

                chkEnableExporta.Checked =  Boolean.Parse(UserPreferences.getValueForMachine("AutomatedExport", UPSections.ExportaPreferences, false, false));
                ZTrace.WriteLineIf(ZTrace.IsError, "AutomatedExport " + chkEnableExporta.Checked.ToString());

                this.textBox1.Text = UserPreferences.getValueForMachine("FilterFolder", UPSections.ExportaPreferences, "[MessageClass]='IPM.Note' AND [Mileage] <> '1' AND [FlagRequest] <> 'Exportado a Zamba'");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los tipos de documento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ZTrace.WriteLineIf(ZTrace.IsError, "Error al cargar los tipos de documento.\n" + ex.ToString());
                ZClass.raiseerror(ex);

                this.Close();
            }

            try
            {
                //Se cargan los mapeos existentes.
                lstMaps = ZRC.GetFolderDocTypeMaps(false);
                dgvMaps.DataSource = lstMaps;
                colDocTypeId = dgvMaps.Columns["DocTypeId"].Index;
                colDocTypeName = dgvMaps.Columns["DocTypeName"].Index;
                colEntryId = dgvMaps.Columns["FolderEntryId"].Index;
                colFolderName = dgvMaps.Columns["FolderName"].Index;
                colAttachId = dgvMaps.Columns["AttachDocTypeId"].Index;
                FormatColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las relaciones existentes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ZTrace.WriteLineIf(ZTrace.IsError, "Error al cargar las relaciones.\n" + ex.ToString());
                ZClass.raiseerror(ex);

                this.Close();
            }
        }

        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            fiMap = MailTool.SelectFolder(app);
            if(fiMap != null)
            { 
                txtFolderPath.Text = fiMap.Path;
            }
        }

        private void btnMap_Click(object sender, EventArgs e)
        {
            if (txtFolderPath.Text.Length == 0)
            {
                MessageBox.Show("Seleccione una carpeta de outlook");
            }
            else
            {
                if (cmbDocTypes.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione la entidad asociada");
                }
                else
                {
                    try
                    {
                        IFolderMap folderMap = new FolderMap(fiMap.EntryId,
                            fiMap.Path,
                            Int64.Parse(cmbDocTypes.SelectedValue.ToString()),
                            cmbDocTypes.Text.Trim(),
                            0);

                        lstMaps.Add(folderMap);
                        dgvMaps.DataSource = null;
                        dgvMaps.Rows.Clear();
                        dgvMaps.Columns.Clear();
                        dgvMaps.DataSource = lstMaps;
                        FormatColumns();
                    }
                    catch (Exception ex)
                    {
                        ZTrace.WriteLineIf(ZTrace.IsError, ex.Message);
                        MessageBox.Show("Error al relacionar la entidad con la carpeta de outlook.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ZClass.raiseerror(ex);

                    }
                }
            }
        }

        private void btnRemoveMap_Click(object sender, EventArgs e)
        {
            if (dgvMaps.SelectedRows.Count > 0)
            {
                try
                {
                    lstMaps.RemoveAt(dgvMaps.SelectedRows[0].Index);
                    dgvMaps.DataSource = null;
                    dgvMaps.Rows.Clear();
                    dgvMaps.Columns.Clear();
                    dgvMaps.DataSource = lstMaps;
                    FormatColumns();
                }
                catch (Exception ex)
                {
                    ZTrace.WriteLineIf(ZTrace.IsError,ex.Message);
                    MessageBox.Show("Error al quitar la relacion entre la entidad y la carpeta de outlook.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ZClass.raiseerror(ex);
                }

            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {

                ZTrace.WriteLineIf(ZTrace.IsError, "AutomatedExport " + IsAutomatedExport.ToString());

                UserPreferences.setValueForMachine("AutomatedExport", IsAutomatedExport.ToString(), UPSections.ExportaPreferences);
                ZTrace.WriteLineIf(ZTrace.IsError, "AutomatedExport " + IsAutomatedExport.ToString());

                ZRC.MapFolder(lstMaps);
                //string filter = this.textBox1.Text.Replace("'","''");
                //UserPreferences.setValueForMachine("FilterFolder", filter, UPSections.ExportaPreferences);

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsError,ex.Message);
                MessageBox.Show("Ha ocurrido un error al guardar las opciones.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ZClass.raiseerror(ex);
            }
            finally
            {
                this.Close();
            }
        }

        private void grpExportaCfg_Enter(object sender, EventArgs e)
        {

        }

        private void FormatColumns()
        {
            dgvMaps.Columns[colEntryId].Visible = false;
            dgvMaps.Columns[colDocTypeId].Visible = false;
            dgvMaps.Columns[colFolderName].HeaderText = "Carpeta de Outlook";
            dgvMaps.Columns[colDocTypeName].HeaderText = "Tipo de documento";
            dgvMaps.Columns[colAttachId].Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtAttachDocTypeId_KeyPress(object sender, KeyPressEventArgs e)
        {
            int num;
            if (!Int32.TryParse(e.KeyChar.ToString(), out num))
                e.Handled = true;
        }

        private void chkEnableExporta_CheckedChanged(object sender, EventArgs e)
        {
            grpExportaCfg.Enabled = chkEnableExporta.Checked;
            
        }
    }
}
