using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Zamba.SetupLibrary;

namespace Zamba.SetupExportaOutLook
{
    public partial class ZambaSetupExportaOutLook : Form
    {
        private const string ERROR_VACIO = "Debe ingresar una ruta";
        private const string ERROR_RUTA_VALIDA = "La ruta no es valida";
        private const string MSG_EXITO = "El proceso finalizo con exito.";
        private const string MSG_ERROR = "Se ha producido un error: {0}";
        private const string TITLE_MSG = "Setup ExportaOutlook";
        private const string MSG_QUESTION_APLICAR = "¿Esta seguro?";

        public ZambaSetupExportaOutLook()
        {
            InitializeComponent();
            btZamba.Click += new EventHandler(ClearError);                       
        }

        private void btZamba_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {                
                txZamba.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btAplicar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(MSG_QUESTION_APLICAR, TITLE_MSG, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Enabled = false;
                AplicarSeguridad();
                this.Enabled = true;   
            }            
        }

        private void ClearError(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        /// <summary>
        /// Valida que los datos ingresados 
        /// sean validados y llama los metodos
        /// RemoveSecurityZamba y SetSecurityZamba 
        /// </summary>
        /// <remarks>
        /// osanchez - 12/05/2009 version inicial
        /// </remarks>
        private void AplicarSeguridad()
        {
            bool isValid = true;            
            if(txZamba.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txZamba, ERROR_VACIO);
                isValid = false;
            }            

            if (isValid)
            {
                if (!Directory.Exists(txZamba.Text))
                {
                    errorProvider1.SetError(txZamba, ERROR_RUTA_VALIDA);
                    isValid = false;
                }                                  
            }
            if (isValid)
            {
                try
                {
                    HelperSecurity hs = new HelperSecurity(txZamba.Text);
                    hs.UpdateDependenciesZamba();

                    MessageBox.Show(MSG_EXITO, TITLE_MSG, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(MSG_ERROR,ex.Message), TITLE_MSG, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
      
    }
}
