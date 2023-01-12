using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zamba.Link
{
    public partial class ZLink_Help : Form
    {
        public ZLink_Help()
        {
            InitializeComponent();
        }

        private void ZLink_Help_Load(object sender, EventArgs e)
        {
            //object filename = System.AppDomain.CurrentDomain.BaseDirectory + @"Zamba Link.docx";
            object filename = ZOptBusiness.GetValue("ZambaLinkHelp");
            Microsoft.Office.Interop.Word.Application AC = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document doc = new Microsoft.Office.Interop.Word.Document();
            object readOnly = false;
            object isVisible = true;
            object missing = System.Reflection.Missing.Value;
            try
            {
                doc = AC.Documents.Open(ref filename, ref missing, ref readOnly, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref isVisible);
                doc.Content.Select();
                doc.Content.Copy();
                richTextBox1.Paste();
                richTextBox1.SelectionStart = 0;
                richTextBox1.ScrollToCaret();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
                this.Close();
            }
            finally
            {
                doc.Close(ref missing, ref missing, ref missing);
            }
        }
    }
}
