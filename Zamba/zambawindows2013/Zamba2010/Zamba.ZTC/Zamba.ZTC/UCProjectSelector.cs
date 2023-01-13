using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Zamba.Core;
using Zamba.CoreExt;

namespace Zamba.ZTC
{
    public partial class UCProjectSelector : UserControl
    {
        public Int64? CurrentProjectId = null;

        public UCProjectSelector()
        {
            try
            {
                InitializeComponent();

                IOrderedQueryable<PRJ_TBL> q = from c in ControlsFactory.dbContext.PRJ_TBL
                                               orderby c.Name
                                               select c;
                IEnumerable<PRJ_TBL> Projects = q.ToList();


                radDropDownList1.DisplayMember = "Name";
                radDropDownList1.ValueMember = "Prj_ID";
                radDropDownList1.DataSource = Projects;

                Int64 ProjectId;
                Int64.TryParse(radDropDownList1.SelectedValue.ToString(), out ProjectId);
                CurrentProjectId = ProjectId;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            try
            {
                Int64 ProjectId;
                Int64.TryParse(radDropDownList1.SelectedValue.ToString(), out ProjectId);
                CurrentProjectId = ProjectId;
                ((Form)Parent).DialogResult = DialogResult.OK;
                ((Form) Parent).Close();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            try
            {
                CurrentProjectId = null;
                ((Form) Parent).DialogResult=DialogResult.Cancel;
                ((Form) Parent).Close();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
    }
}