using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Zamba.Simulator.Model;
using Zamba.DataGenerator;
using Zamba.Core;

namespace Zamba.Simulator
{
    public partial class FrmDictionaryABM : Form
    {
        public Dictionary Dictionary;
        private DataTable dataTemp;
        private long stepId;

        public FrmDictionaryABM(Dictionary dictionary, long ruleId)
        {
            try
            {
                InitializeComponent();
                Dictionary = dictionary;
                txtDictionaryName.Text = Dictionary.Name;
                dataTemp = Dictionary.Data.Copy();
                dgv.DataSource = dataTemp;
                dgv.Show();

                stepId = WFStepBusiness.GetStepIdByRuleId(ruleId,true);
            }
            catch (Exception )
            {
                
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtDictionaryName_TextChanged(object sender, EventArgs e)
        {
            Dictionary.Name = txtDictionaryName.Text;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Dictionary.Data = dataTemp.Copy();
            this.Close();
        }

        private void btnSelectTask_Click(object sender, EventArgs e)
        {
            using (FrmTaskSelection frm = new FrmTaskSelection(stepId))
            {
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                   
                }
            }
        }

        private void btnGenerateData_Click(object sender, EventArgs e)
        {
            using (FrmGenericData frm = new FrmGenericData())
            {
                if(frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                }
            }
        }




    }
}
