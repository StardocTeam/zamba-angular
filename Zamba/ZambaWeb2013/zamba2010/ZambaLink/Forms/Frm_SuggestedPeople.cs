using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZambaLink;

namespace Zamba.Link
{
    public partial class Frm_SuggestedPeople : Form
    {
        public Frm_SuggestedPeople()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {     
            SuggestedPeople.FadeOut(Frm_ZLink.spForm);            
        }
    }
}
