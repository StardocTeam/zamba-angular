using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Zamba.PreLoad
{
    public partial class frmLoadStatus : Form
    {
        public frmLoadStatus()
        {
            InitializeComponent();
            PreLoadEngine.ChangeTextEvent += new PreLoadEngine.ChangeText(this.ActualizaEstado);
            PreLoad.PreLoadEngine.CloseDialogEvent +=new PreLoadEngine.CloseDialog(this.Close);
        }


        public void ActualizaEstado(string text)
        {
            LbElemento.Text = text;      
        }
    }
}
