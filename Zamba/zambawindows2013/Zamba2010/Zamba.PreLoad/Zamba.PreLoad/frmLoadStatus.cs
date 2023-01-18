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
            try { 
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            PreLoadEngine.ChangeTextEvent += new PreLoadEngine.ChangeText(this.ActualizaEstado);
            PreLoad.PreLoadEngine.CloseDialogEvent += new PreLoadEngine.CloseDialog(CloseThis);
       
}
            catch (InvalidOperationException ex)
            { 
         
            }
            catch (Exception ex)
            {
         
            }
 }

        private void CloseThis()
        {
            try {
                if (this != null)
                this.Close();
            }
            catch (InvalidOperationException ex)
            {
         
            }
            catch (Exception ex)
            {
       
            }
        }
        private void ActualizaEstado(string text)
        {
            try
            {

            lblMessage.Text = text;
            lblMessage.Refresh();
        
            }
            catch (InvalidOperationException ex)
            {
         
            }
            catch (Exception ex)
            {
       
            }
    }
    }
}
