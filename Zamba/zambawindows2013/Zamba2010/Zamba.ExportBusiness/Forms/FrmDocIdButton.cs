using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
//using Zamba.ExportaOutlook.RServices;
using Zamba.Core;
using Zamba.Services.Remoting;
namespace ExportaOutlook.Forms
{
    public partial class FrmDocIdButton : Form
    {

        ZambaRemoteClass ZRC = new ZambaRemoteClass();


        public string AttributeId 
        { 
            get
            {
                if (cmbAttributes.SelectedValue != null)
                    return cmbAttributes.SelectedValue.ToString();
                else
                    return String.Empty;
            }
        }

        public string ZoptOptionName { get{return "OverrideDocIdWithAttributeValue";} }

        public FrmDocIdButton()
        {
            InitializeComponent();
        }

        private void FrmDocIdButton_Load(object sender, EventArgs e)
        {
            try
            {
                //Completa el combo de atributos
                Dictionary<long, string> indexs = ZRC.GetAllIndexsIdsAndNames();
                cmbAttributes.DataSource = indexs.ToList();
                cmbAttributes.ValueMember = "Key";
                cmbAttributes.DisplayMember = "Value";

                //Selecciona el valor guardado
                Int64 indexId;
                if (Int64.TryParse(ZRC.GetZoptValue(ZoptOptionName), out indexId))
                {
                    cmbAttributes.SelectedValue = indexId;
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);

                MessageBox.Show("Ha ocurrido un error al cargar la configuración de sobreescritura de DOCID", "Zamba Software", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
