using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Zamba.AdminControls;
using Zamba.Core;
using Zamba.AppBlock;

namespace Zamba.AutoName.Execution
{
    public partial class MainForm : ZForm
    {
        delegate void dvalidatetimeout();
        ExternalTimer timer;
        DSDOCTYPE ds;
        string AutoName;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                ExternalLogin login = new ExternalLogin();

                if (!login.validate(string.Empty, string.Empty))
                {
                    Application.Exit();
                }
                else
                {
                    timer = new ExternalTimer();

                    timer.connectionTimeOut += new ExternalTimer.connectionTimeOutEventHandler(relogin);

                    timer.InitializeConnectionTimer();

                    if (!Zamba.Core.Users.Rights.GetUserRights(ObjectTypes.DocTypes, RightsType.Admin, -1))
                    {
                        MessageBox.Show("No tiene permiso para ejecutar AutoName sobre los documentos", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        Application.Exit();
                    }

                    FillDocTypes();
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void relogin()
        {
            try
            {
                Zamba.Core.Users.User.CurrentUser = null;

                dvalidatetimeout d = new dvalidatetimeout(ValidateTimeOut);
                this.Invoke(d);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void ValidateTimeOut()
        {
            ExternalLogin login;

            try
            {
                login = new ExternalLogin();
                if (login.validateTimeOut() == false)
                    Application.Exit();
            }
            finally
            {
                login = null;
            }
        }

        private void FillDocTypes()
        {
            try
            {
                ds = DocTypesBusiness.GetDocTypes();

                ds.Clear();
                ds = DocTypesBusiness.GetDocTypesDsDocType();

                lstDocTypes.DisplayMember = "DOC_TYPE_NAME";
                lstDocTypes.ValueMember = "DOC_TYPE_ID";
                lstDocTypes.DataSource = ds.DOC_TYPE;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (timer != null)
            {
                timer.stopTimeOut();
                timer = null;
            }
        }

        private void lstDocTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView row = (System.Data.DataRowView)(lstDocTypes.SelectedItem);

            txtAutoName.Text = AutoName = row.Row["autoname"].ToString().Trim();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            Int32 DocTypeId = Int32.Parse(lstDocTypes.SelectedValue.ToString());
            string DocTypeName = lstDocTypes.Text.ToString().Trim();

            Autoname_Business.ExecuteUpdate(DocTypeId, DocTypeName, AutoName);
        }
    }
}