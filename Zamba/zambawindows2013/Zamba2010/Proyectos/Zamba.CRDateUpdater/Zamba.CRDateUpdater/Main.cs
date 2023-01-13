using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Zamba.Core;
using Zamba.Data;

namespace Zamba.CRDateUpdater
{
    public partial class Main : Form
    {

        #region Constructor

        public Main()
        {
            InitializeComponent();
            this.rdbSelectDT.Checked = true;
            this.loadDocTypes();
        }

        #endregion

        #region Metodos privados

        /// <summary>
        /// Metodo que carga los tipos de documentos en la grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        /// [Ezequiel] 15/10/2009 - Created
        /// </history>
        private void loadDocTypes()
        {
            try
            {
                DataTable dtaux = new DataTable();
                dtaux.Columns.Add("Agregar", typeof(bool));
                DataSet dsdoctypes = Servers.Server.get_Con(false, true, false).ExecuteDataset(CommandType.Text, "select doc_type_id, doc_type_name from doc_type order by doc_type_name asc");
                dtaux.Merge(dsdoctypes.Tables[0]);
                this.chklstDoctypes.DataSource = dtaux;
                this.chklstDoctypes.DisplayMember = "doc_type_name";
                this.chklstDoctypes.ValueMember = "doc_type_id";
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Metodo que se llama al seleccionar el radio de actualizar todos los tipos de documento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        /// [Ezequiel] 15/10/2009 - Created
        /// </history>
        private void rdbAllDT_CheckedChanged(object sender, EventArgs e)
        {
            //[Ezequiel] - Dependiendo del radio seleccionado oculto o no el panel de los tipos de documentos.
            try
            {
                if (this.rdbAllDT.Checked)
                {
                    this.Size = new Size(488, 314);
                    this.splitContainer1.Panel2Collapsed = true;
                    this.splitContainer1.Size = new Size(481, 227);
                    this.btnStart.Location = new Point(122, 232);
                    this.btnClose.Location = new Point(268, 232);
                    this.btnClose.BringToFront();
                    this.btnStart.BringToFront();
                    this.txtIds.Enabled = false;
                    this.label4.Enabled = false;
                    this.txtIds.Text = "";
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }


        /// <summary>
        /// Metodo que se llama al seleccionar el radio de seleccionar tipo de documento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        /// [Ezequiel] 15/10/2009 - Created
        /// </history>
        private void rdbSelectDT_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.rdbSelectDT.Checked)
                {
                    this.Size = new Size(488, 520);
                    this.splitContainer1.Panel2Collapsed = false;
                    this.splitContainer1.Size = new Size(481, 433);
                    this.btnStart.Location = new Point(122, 438);
                    this.btnClose.Location = new Point(268, 438);
                    this.btnClose.BringToFront();
                    this.btnStart.BringToFront();
                    this.txtIds.Enabled = false;
                    this.label4.Enabled = false;
                    this.txtIds.Text = "";
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Metodo que se llama al seleccionar el radio de especificar ids
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        /// [Ezequiel] 15/10/2009 - Created
        /// </history>
        private void rdbEditIDs_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.rdbEditIDs.Checked)
                {
                    this.Size = new Size(488, 314);
                    this.splitContainer1.Panel2Collapsed = true;
                    this.splitContainer1.Size = new Size(481, 227);
                    this.btnStart.Location = new Point(122, 232);
                    this.btnClose.Location = new Point(268, 232);
                    this.btnClose.BringToFront();
                    this.btnStart.BringToFront();
                    this.txtIds.Enabled = true;
                    this.label4.Enabled = true;
                    this.txtIds.Text = "";
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Metodo el cual muestra los id de los tipos de documento
        /// que fueron seleccionados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chklstDoctypes_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                this.txtIds.Text = string.Empty;
                string idlist = string.Empty;
                foreach (DataRowView item in chklstDoctypes.CheckedItems)
                {
                    idlist += ", " + item.Row["doc_type_id"];
                }
                if (!string.IsNullOrEmpty(idlist))
                    this.txtIds.Text = idlist.Substring(1).Trim();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Metodo el cual muestra los id de los tipos de documento
        /// que fueron seleccionados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chklstDoctypes_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                this.txtIds.Text = string.Empty;
                string idlist = string.Empty;
                foreach (DataRowView item in chklstDoctypes.CheckedItems)
                {
                    idlist += ", " + item.Row["doc_type_id"];
                }
                if (!string.IsNullOrEmpty(idlist))
                    this.txtIds.Text = idlist.Substring(1).Trim();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Metodo el cual inicia la ejecucion del actualizador
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        /// [Ezequiel] - 15/10/09 - Created
        /// </history>
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtDateCol.Text) || string.IsNullOrEmpty(this.txtDocIDCol.Text) || string.IsNullOrEmpty(this.txtTableCol.Text) || string.IsNullOrEmpty(this.dtpDesde.Text) || string.IsNullOrEmpty(this.dtpHasta.Text))
                    MessageBox.Show("Debe completar los campos de configuracion de tabla", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (this.rdbAllDT.Checked == false && string.IsNullOrEmpty(this.txtIds.Text))
                    MessageBox.Show("Debe especificar al menos un tipo de documento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    if (this.rdbAllDT.Checked != true)
                        this.startUpdate(this.txtIds.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));

                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }


        /// <summary>
        /// Metodo que cierra la aplicacion.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        /// [Ezequiel] - 15/10/09 - Created
        /// </history>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Metodo el cual muestra los datos a los cuales se hace referencia para completar el crdate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        /// [Ezequiel] Created.
        /// </history>
        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtDateCol.Text) || string.IsNullOrEmpty(this.txtDocIDCol.Text) || string.IsNullOrEmpty(this.txtTableCol.Text) || string.IsNullOrEmpty(this.dtpDesde.Text) || string.IsNullOrEmpty(this.dtpHasta.Text))
                    MessageBox.Show("Debe completar los campos de configuracion de tabla", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    DataSet dsview = Servers.Server.get_Con(false, true, false).ExecuteDataset(CommandType.Text, this.getDateForDocID());
                    Preview frmPreview = new Preview(dsview.Tables[0]);
                    frmPreview.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }


        /// <summary>
        /// Arma el string en base a los datos de configuracion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        /// [Ezequiel] Created.
        /// </history>
        private string getDateForDocID()
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("select fechas Fecha, min(doc_id) \"Doc id minimo\", max(doc_id) \"Doc id maximo\" from (select to_date(");
                sb.Append(this.txtDateCol.Text);
                sb.Append(",'dd/mm/yyyy') fechas, ");
                sb.Append(this.txtDocIDCol.Text);
                sb.Append(" doc_id from ");
                sb.Append(this.txtTableCol.Text);
                sb.Append(" where to_date(to_char(");
                sb.Append(this.txtDateCol.Text);
                sb.Append(",'dd/mm/yyyy'),'dd/mm/yyyy') >=  to_date('");
                sb.Append(this.dtpDesde.Text);
                sb.Append("','dd/mm/yyyy') AND to_date(to_char(");
                sb.Append(this.txtDateCol.Text);
                sb.Append(",'dd/mm/yyyy'),'dd/mm/yyyy') <=  to_date('");
                sb.Append(this.dtpHasta.Text);
                sb.Append("','dd/mm/yyyy') AND ");
                sb.Append(this.txtDocIDCol.Text);
                sb.Append(" is not null) group by fechas");
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Actualiza los tipos de documento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        /// [Ezequiel] Created.
        /// </history>
        private void startUpdate(string[] doctypes)
        {
            Int64 rowcount = 0;
            Transaction tran = new Transaction();
            try
            {
                this.toolStripProgressBar1.Maximum = doctypes.Length;
                this.toolStripProgressBar1.Value = 0;
                DataSet dsview = Servers.Server.get_Con(false, true, false).ExecuteDataset(CommandType.Text, this.getDateForDocID());
                List<string> querys = makeQueryUpdate(dsview.Tables[0]);
                foreach (string dtid in doctypes)
                {
                    this.toolStripProgressBar1.Value++;
                    this.toolStripStatusLabel1.Text = "Actualizando " + this.toolStripProgressBar1.Value.ToString() + " de " + this.toolStripProgressBar1.Maximum.ToString() + " tipos de documento...";
                    Application.DoEvents();
                    foreach (string query in querys)
                    {
                        rowcount += tran.Con.ExecuteNonQuery(tran.Transaction, CommandType.Text, "UPDATE doc_i" + dtid.Trim() + " " + query);
                    }
                }
                tran.Commit();
                this.toolStripStatusLabel1.Text = "Proceso terminado. " + rowcount + " Filas Afectadas en la Consulta";
                MessageBox.Show("El proceso se ejecuto con exito", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                MessageBox.Show("Ocurrio un error en la ejecucion del actualizardor. Se hace un rollback de los datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tran.Rollback();
            }
        }

        /// <summary>
        /// Metodo el cual arma la lista de los rango de fecha a actualizar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        /// [Ezequiel] Created.
        /// </history>
        private List<string> makeQueryUpdate(DataTable dt)
        {
            List<string> querys = new List<string>();
            try
            {
                for (Int32 i = 0; i < dt.Rows.Count; i++)
                {
                    if (i + 1 == dt.Rows.Count)
                        querys.Add(" SET crdate = to_date('" + dt.Rows[i][0].ToString().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0] + "','dd/mm/yyyy') WHERE doc_id >= " + dt.Rows[i][1].ToString() + " AND doc_id <= " + dt.Rows[i][2].ToString() + (this.chkOnlyNull.Checked == true ? " AND crdate is null" : ""));
                    else
                        querys.Add(" SET crdate = to_date('" + dt.Rows[i][0].ToString().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0] + "','dd/mm/yyyy') WHERE doc_id >= " + dt.Rows[i][1].ToString() + " AND doc_id < " + dt.Rows[i + 1][1].ToString() + (this.chkOnlyNull.Checked == true ? " AND crdate is null" : ""));
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return querys;
        }

        #endregion

    }
}
