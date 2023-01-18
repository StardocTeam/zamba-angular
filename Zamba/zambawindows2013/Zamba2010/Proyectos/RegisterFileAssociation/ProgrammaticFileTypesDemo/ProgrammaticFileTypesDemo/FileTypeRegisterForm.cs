using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;

namespace ProgrammaticFileTypesDemo
{
    public partial class FileTypeRegisterForm : Form
    {
        public FileTypeRegisterForm()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            //validar
            bool response = false;
            response = ValidateInput(response);
            if (!response)
            {
                MessageBox.Show("Hay datos necesarios sin cargar", "Validaciones");
                return;
            }

            string extension = cmbExtensions.Text;

            // Create a registry key object to represent the HKEY_CLASSES_ROOT registry section
            RegistryKey rkRoot = Registry.ClassesRoot;

            // Attempt to retrieve the registry key for the .blackwasp file type
            RegistryKey rkFileType = rkRoot.OpenSubKey(extension);

            // Was the file type found?
            if (rkFileType == null)
            {
                // No, so register it
                RegistryKey rkNew;

                // Create the registry key
                rkNew = rkRoot.CreateSubKey(extension);

                // Set the unique file type name
                rkNew.SetValue("", extension.Replace(".",string.Empty));

                // Create the file type information key
                RegistryKey rkInfo = rkRoot.CreateSubKey(extension.Replace(".", string.Empty));

                // Set the default value to the file type description
                rkInfo.SetValue("", "Archivo " + extension);

                // Create the shell key to contain all verbs
                RegistryKey rkShell = rkInfo.CreateSubKey("shell");

                // Create a subkey for the "Open" verb
                RegistryKey rkOpen = rkShell.CreateSubKey("Open");

                // Set the menu name against the key
                rkOpen.SetValue("", "&Open Document");

                // Create and set the command string
                rkNew = rkOpen.CreateSubKey("command");
                rkNew.SetValue("", txtPathEjecutable.Text  + " %1");

                // Assign a default icon
                rkNew = rkInfo.CreateSubKey("DefaultIcon");
                rkNew.SetValue("", txtPathIcono.Text + ",0");

                MessageBox.Show("Asociacion a tipo de archivo creada correctamente", "Información");
            }
        }

        private bool ValidateInput(bool response)
        {
            if (
                !string.IsNullOrEmpty(cmbExtensions.Text) && cmbExtensions.Text.Contains(".")  &&
                !string.IsNullOrEmpty(txtPathEjecutable.Text) && File.Exists(txtPathEjecutable.Text) &&
                !string.IsNullOrEmpty(txtPathIcono.Text) && File.Exists(txtPathIcono.Text)
                )
            {
                response = true;
            }
            return response;
        }

        private void cmbExtensions_SelectedIndexChanged(object sender, EventArgs e)
        {
            string programFilesPath = System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            switch (cmbExtensions.Text)
            {
                case ".dwg":
                    txtPathEjecutable.Text = Path.Combine(programFilesPath,@"IGC\Free DWG Viewer\BravaFreeDWG.exe");
                    txtPathIcono.Text = Path.Combine(programFilesPath, @"IGC\Free DWG Viewer\BravaFreeDWG62.ico");
                    break;
                default:
                    break;
            }
        }


    }
}
