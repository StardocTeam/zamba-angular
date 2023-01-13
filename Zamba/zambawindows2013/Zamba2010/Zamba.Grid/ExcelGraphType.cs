using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Zamba.Grid
{
    public partial class ExcelGraphType : Form
    {
        public ExcelGraphType()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (lstGraph.SelectedItems.Count > 0)
            {
                _hasGraphic = true;
                _typeGraphic = this.lstGraph.SelectedItem.ToString();
                this.Close();
            }
            else
                MessageBox.Show("Debe seleccionar un tipo de gráfico");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            _hasGraphic = false;
            this.Close();
        }

        private Boolean _hasGraphic = false;
        public Boolean hasGraphic
        {
            get
            {
                return _hasGraphic;
            }
        }

        private String _typeGraphic;
        public String typeGraphic
        {
            get
            {
                return _typeGraphic;
            }
        }

        private void ExcelGraphType_Load(object sender, EventArgs e)
        {
            this.lstGraph.Items.Clear();
            this.lstGraph.Items.Add("LINEAS");
            this.lstGraph.Items.Add("AREA");
            this.lstGraph.Items.Add("COLUMNAS");
            this.lstGraph.Items.Add("BARRAS");
            this.lstGraph.Items.Add("TORTA");
        }
    }
}
