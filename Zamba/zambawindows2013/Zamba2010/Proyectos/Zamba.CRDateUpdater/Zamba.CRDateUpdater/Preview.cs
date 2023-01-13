using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Zamba.CRDateUpdater
{
    public partial class Preview : Form
    {
        public Preview(DataTable dt)
        {
            InitializeComponent();
            this.dgvListDate.DataSource = dt;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
