using ExportaOutlook.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using Zamba.ExportaOutlook.RServices;
using Outlook = Microsoft.Office.Interop.Outlook;
using Zamba.Core;
using Zamba.Services.Remoting;
using Zamba;

namespace ExportaOutlook.Forms
{
    public partial class FrmExportedFolder : Form
    {
     
        Outlook.Application app = null;
        ZambaRemoteClass ZRC = new ZambaRemoteClass();


        public FrmExportedFolder()
        {
            InitializeComponent();
        }

        public FrmExportedFolder( Outlook.Application app)
        {
            InitializeComponent();
         
            this.app = app;
        }

        private void FrmExportedFolder_Load(object sender, EventArgs e)
        {
            try
            {
                txtFolder.Text = UserPreferences.getValueForMachine("ExportedMailsFolderPath",UPSections.ExportaPreferences, string.Empty);
                txtID.Text = UserPreferences.getValueForMachine("ExportedMailsFolderEntryId", UPSections.ExportaPreferences, string.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la carpeta de mails exportados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ZTrace.WriteLineIf(ZTrace.IsError, "Error al cargar la carpeta de mails exportados: " + ex.ToString());
                ZClass.raiseerror(ex);

            }
        }

        private void btnPickFolder_Click(object sender, EventArgs e)
        {
            FolderInfo fiExported = MailTool.SelectFolder(app);
            if (fiExported != null)
            {
                txtFolder.Text = fiExported.Path;
                txtID.Text = fiExported.EntryId;
                fiExported = null;
            }
        }

        private void btnRemoveFolder_Click(object sender, EventArgs e)
        {
            txtFolder.Text = string.Empty;
            txtID.Text = string.Empty;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
               UserPreferences.setValueForMachine("ExportedMailsFolderPath", txtFolder.Text,UPSections.ExportaPreferences);
                UserPreferences.setValueForMachine("ExportedMailsFolderEntryId", txtID.Text,UPSections.ExportaPreferences);
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la carpeta de mails exportados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ZTrace.WriteLineIf(ZTrace.IsError, "Error al guardar la carpeta de mails exportados: " + ex.ToString());
                ZClass.raiseerror(ex);

            }
            finally
            {
                Close();
            }
        }
    }
}
