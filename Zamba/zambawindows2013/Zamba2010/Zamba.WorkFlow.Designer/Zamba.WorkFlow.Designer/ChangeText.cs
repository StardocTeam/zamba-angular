using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Zamba.AppBlock;

namespace Zamba.WorkFlow.Designer
{
    public partial class ChangeText : ZForm
    {
        public ChangeText(string OldName)
        {
            InitializeComponent();
            this.textBox1.Text = OldName;
        }

        public string name = "";
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            name = this.textBox1.Text;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            name = "";
            this.Close();
        }
    }
}