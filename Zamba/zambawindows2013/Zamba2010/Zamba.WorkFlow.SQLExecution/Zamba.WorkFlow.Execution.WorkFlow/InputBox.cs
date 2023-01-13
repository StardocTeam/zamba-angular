using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Zamba.WorkFlow.Execution.WorkFlow
{
    public partial class InputBox : Form
    {
        public InputBox( string tituloVentana, string Titulo, string texto )
        {
            InitializeComponent();
 
            this.texto = "";
            this.lblTitulo.Text = Titulo;
            this.Text = tituloVentana;
            this.txtName.Text = texto;
            this.txtName.Visible = true;
            this.cmbItems.Visible = false;
            this.btnAceptar.Focus();
        }

        public InputBox(string tituloVentana, string Titulo, String[] items )//List<string> items )
        {
            InitializeComponent();

            this.texto = "";
            this.lblTitulo.Text = Titulo;
            this.texto = tituloVentana;            
            this.cmbItems.Items. AddRange(items);
            this.txtName.Visible = false;
            this.cmbItems.Visible = true;
            this.btnAceptar.Focus();
        }

        // Cierro el formulario sin pasar ningun valor
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            texto = "";
            selectedItem = null;
            this.Close();
        }

        private string texto;
        private string selectedItem;

        public string Texto
        {
            get {
                return texto; }
            set { texto = value; }
        }

        public string ReturnItem
        {
            get
            {
                return selectedItem;
            }
            set { selectedItem = value; }
        }

        // Cierro el formulario dejandole el valor
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (true == this.txtName.Visible)
                texto = this.txtName.Text;
            else
            {
                texto = this.cmbItems.Text;
                selectedItem = this.cmbItems.Text;
            }

            this.Close();
        }
    }
}