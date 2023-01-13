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
    public partial class FrmExportOptions : Form
    {
     
        Outlook.Application app = null;
        ZambaRemoteClass ZRC = new ZambaRemoteClass();


        public FrmExportOptions()
        {
            InitializeComponent();
        }

        public FrmExportOptions(Outlook.Application app)
        {
            InitializeComponent();
         
            this.app = app;
        }

        private void FrmExportOptions_Load(object sender, EventArgs e)
        {
            try
            {
				Boolean ChangeExportedMail = false;
				Boolean AddLinkToExportedMail = false;
				Boolean MarkExportedMail = false;

				Boolean.TryParse(UserPreferences.getValueForMachine("ChangeExportedMail",UPSections.ExportaPreferences, false),out ChangeExportedMail);
                Boolean.TryParse(UserPreferences.getValueForMachine("AddLinkToExportedMail", UPSections.ExportaPreferences, false), out AddLinkToExportedMail);
                Boolean.TryParse(UserPreferences.getValueForMachine("MarkExportedMail", UPSections.ExportaPreferences, false), out MarkExportedMail);

				this.checkBox1.Checked =ChangeExportedMail;
				this.checkBox2.Checked = AddLinkToExportedMail;
				this.checkBox3.Checked = MarkExportedMail;

				String location = UserPreferences.getValueForMachine("LinkToExportedMailLocation", UPSections.ExportaPreferences, string.Empty);
               if (location == "Top")
               {
                   this.radioButton1.Checked = true;
                   this.radioButton2.Checked = false;
               }
               else
               {
                   this.radioButton1.Checked = false;
                   this.radioButton2.Checked = true;
               }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las opciones de exportacion.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ZClass.raiseerror(ex);

            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                UserPreferences.setValueForMachine("ChangeExportedMail",this.checkBox1.Checked.ToString(), UPSections.ExportaPreferences);
                UserPreferences.setValueForMachine("AddLinkToExportedMail", this.checkBox2.Checked.ToString(), UPSections.ExportaPreferences);
                UserPreferences.setValueForMachine("MarkExportedMail", this.checkBox3.Checked.ToString(), UPSections.ExportaPreferences);
                
                if (this.radioButton1.Checked == true)
                {
                    UserPreferences.setValueForMachine("LinkToExportedMailLocation","Top", UPSections.ExportaPreferences);
                }
                else
                {
                    UserPreferences.setValueForMachine("LinkToExportedMailLocation", "Bottom", UPSections.ExportaPreferences);
                }
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar las opciones de exportacion.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ZClass.raiseerror(ex);

            }
            finally
            {
                Close();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked == true)
            {
                this.checkBox2.Checked = true;
                this.checkBox2.Enabled = false;
            }
            else
            {
                this.checkBox2.Enabled = true;
            }
        }


        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox2.Checked == true)
            {
                this.groupBox1.Enabled = true;
            }
            else
            {
                this.groupBox1.Enabled = false;
            }

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
