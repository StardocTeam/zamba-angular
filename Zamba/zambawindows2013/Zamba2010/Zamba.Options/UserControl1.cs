using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Zamba.Options
{
    public partial class UserControl1 : UserControl
    {
        Zamba.CoreExt.ZOPTEntities model = null;
        public UserControl1()
        {
            InitializeComponent();
            model = new CoreExt.ZOPTEntities();
            this.zoptDataGridView.DataSource = model.Zopt.OrderBy(p => p.Item);
            this.zoptDataGridView.Refresh();
        }

        private void zoptBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            model.SaveChanges();
        }
    }
}
