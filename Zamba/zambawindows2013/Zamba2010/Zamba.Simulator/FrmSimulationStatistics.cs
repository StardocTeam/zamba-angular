using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Zamba.Simulator
{
    public partial class FrmSimulationStatistics : Form
    {
        public FrmSimulationStatistics()
        {
            InitializeComponent();
        }

        private void FrmSimulationStatistics_Load(object sender, EventArgs e)
        {
            dgvStatistics.Rows.Add(new object[] { "12289", "Abrir carta", "102", "% 6,8", "0"});
            
            dgvStatistics.Rows.Add(new object[] { "12102", "NO", "0", "% 0", "0" });
            dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(255, 242, 242);
            dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Red;

            dgvStatistics.Rows.Add(new object[] { "12401", "Abrir carta con demandados", "1330", "% 88,6", "0" });
            
            dgvStatistics.Rows.Add(new object[] { "11917", "Mensaje al usuario", "5", "% 0,3", "0" });
            
            dgvStatistics.Rows.Add(new object[] { "12182", "Abrir poder", "55", "% 3,6", "55" });
            dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(255, 242, 242);
            dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Red;
            
            dgvStatistics.Rows.Add(new object[] { "12194", "Mensaje de poder inválido", "8", "% 0,5", "0" });
        }
    }
}
