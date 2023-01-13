using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Zamba.Servers;
using Zamba.AppBlock;
using Zamba.Core;
using Zamba.AdminControls;
using Zamba.Membership;
using Zamba.WFActivity.Regular;

namespace Zamba.Query
{
    public partial class FrmMain : ZForm
    {
        private DataSet ds;
        private DataTable dtLog;
        private Int16 executionCounter = 1;
        private Int16 queryCounter = 1;
        const string _separatorHeader = "\r\n----------------------------------------------------------------------\r\n";
        private const string _correctResult = "Correcta";
        private const string _incorrectResult = "Fallida";
        private string logMessage;
        public string line = String.Empty;
        public int userId;
        private enum QueryResult
        {
            Ok = 0,
            Error = 1
        };


        public FrmMain(string commandLine)
        {                      
            line = commandLine;
            InitializeComponent();
            string status = string.Empty;
            DBBusiness.InitializeSystem(ObjectTypes.ModuleQuery, null, false,ref status, new ErrorReportBusiness());

            chkUseAppIni.Checked = true;
            cboServerType.DataSource = System.Enum.GetValues(typeof(DBTYPES));
            InitializeLogGridSchema();
            
        }

        public void InitializeLogGridSchema()
        {
            dtLog = new DataTable("Log");
            DataColumn dcExecutionId = new DataColumn("ExecutionId", typeof(Int16));
            DataColumn dcQueryId = new DataColumn("QueryId", typeof(Int32));
            DataColumn dcResult = new DataColumn("Result", typeof(string));
            DataColumn dcMessage = new DataColumn("Message", typeof(string));
            DataColumn dcQuery = new DataColumn("Query", typeof(string));
            dtLog.Columns.AddRange(new DataColumn[] { dcExecutionId, dcQueryId, dcResult, dcMessage, dcQuery });

            rgvLog.DataSource = dtLog;
            rgvLog.Columns[0].HeaderText = "Ejecución Nº";
            rgvLog.Columns[1].HeaderText = "Consulta Nº";
            rgvLog.Columns[2].HeaderText = "Resultado";
            rgvLog.Columns[3].HeaderText = "Mensaje";
            rgvLog.Columns[4].HeaderText = "SQL";
            rgvLog.Columns[0].IsVisible = false;
        }


        private void btn_EjecutarQuery_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtQuery.Text))
            {
                MessageBox.Show("Ingrese la Consulta a realizar", "Completar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                this.Tabs.TabPages.Clear();
                if (!string.IsNullOrEmpty(this.txtQuery.SelectedText))
                    this.ExecuteScript(this.txtQuery.SelectedText);
                else
                    this.ExecuteScript(txtQuery.Text);
            }
            txtQuery.Focus();
        }

        private void btnCleanQuery_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea limpiar el cuadro de resultados?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                txtQuery.Clear();
            }
        }

        private void btnCleanLog_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea limpiar el cuadro de la consulta?", String.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                txtMensajes.Clear();
                dtLog.Rows.Clear();
            }
        }

        /// <summary>
        /// Metodo que ejecuta la consulta
        /// </summary>
        /// <param name="script"></param>
        /// <history>
        /// [Ezequiel] - 10/09/10 - Unifique el metodo de ejecutar las consultas asi es mas facil tratar el codigo.
        /// </history>
        private void ExecuteScript(string script)
        {
            queryCounter = 1;

            rgvLog.BeginUpdate();

            try
            {
                Boolean UseSplitters = false;
                if (UseSplitters)
                {
                    script = script.Replace("';'", "'§'");
                }

                String[] separator = {"\r", "\n GO", "\nGO", "\r\n GO", "\r\nGO" };

                String[] queries = script.Split(separator, StringSplitOptions.RemoveEmptyEntries);


                AddMessageHeader();
                Application.DoEvents();

                foreach (String query in queries)
                {
                    try
                    {
                        StringBuilder sql = new StringBuilder();
                        sql.Append(query.Replace("'§'", "';'"));
                        if (chkUseAppIni.Checked == true)
                        {
                            if (query.ToLower().Trim().StartsWith("select") == true || query.ToLower().Trim().StartsWith("exec"))
                            {
                                //[Ezequiel] - 07/10/09 - Si el servidor es de oracle y la consulta comienza
                                //                        con exec entonces ejecuto un procedimiento     
                                if (Server.isOracle && query.ToLower().Trim().StartsWith("exec "))
                                {
                                    //[Ezequiel] - Guardo el nombre del store
                                    string spname = query.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1];
                                    //[Ezequiel] - Guardo los parametros.
                                    string param = query.Replace(query.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0] + " " + query.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1], "");
                                    //[Ezequiel] - Creo los arrays donde se van a guardar los parametros y valores
                                    ArrayList parNames = new ArrayList();
                                    ArrayList parValues = new ArrayList();

                                    //[Ezequiel] - Recorro la lista de parametros para cargar los datos en los arraylist
                                    while (param != string.Empty)
                                    {
                                        Boolean split = false;
                                        Int32 pos = 0;
                                        string par = string.Empty;
                                        //[Ezequiel] - Bucle que separa el nombre del parametro del valor
                                        while (!split)
                                        {
                                            //[Ezequiel] - Obtengo la posicion de la primera coma y la guardo en pos,
                                            // en el caso de que pos sea diferente de 0 es porque el valor tenia dentro una coma
                                            // entonces en la proxima iteracion a esa posicion le sumo 1 asi toma la segunda coma
                                            pos = param.IndexOf(",", (pos == 0 ? pos : pos + 1));
                                            //[Ezequiel] - Si la posicion es -1 es porque era el ultimo parametro.
                                            if (pos == -1)
                                                par = param;
                                            else
                                                par = param.Substring(0, pos);
                                            //[Ezequiel] - Si el parametro termina con comilla simple es porque esta completo
                                            // caso contrario habia una coma dentro del valor y sigue la iteracion
                                            if (par.Trim().EndsWith("'"))
                                            {
                                                //[Ezequiel] - Guardo el nombre del parametro
                                                string parname = par.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0];
                                                parNames.Add(parname.Trim());
                                                //[Ezequiel] - Guardo el valor del parametro
                                                parValues.Add(par.Substring(par.IndexOf(parname) + parname.Length).Trim().Replace("'", String.Empty));
                                                //[Ezequiel] - Si la longitud del parametro es igual a la lista de parametros
                                                // es porque ya no hay mas nada por recorrer
                                                if (param.Length == par.Length)
                                                    param = String.Empty;
                                                else
                                                    param = param.Substring(par.Length + 1);
                                                //[Ezequiel] - Pongo la bandera en true para que recorra el siguiente parametro.
                                                split = true;
                                            }
                                        }
                                    }

                                    //[Ezequiel] - Genero el array para los tipos de parametros
                                    string[] parTypes = new string[parNames.ToArray().Length];
                                    for (Int32 i = parNames.ToArray().Length - 1; i > -1; i--)
                                    {
                                        parNames[i] = parNames[i].ToString().Trim();
                                        parValues[i] = parValues[i].ToString().Trim();
                                        if (parNames[i].ToString().ToLower().CompareTo("io_cursor") == 0)
                                            parTypes[i] = "2";
                                        else
                                            parTypes[i] = "13";
                                    }
                                    ds = Server.get_Con().ExecuteDataset(spname, parNames.ToArray(), parTypes, parValues.ToArray());
                                }
                                else
                                    ds = Server.get_Con().ExecuteDataset(CommandType.Text, sql.ToString());

                                for (int i = 0; i < ds.Tables.Count; i++)
                                    this.addQuery(ds.Tables[i], query);

                                logMessage = "Filas obtenidas: " + ds.Tables[0].Rows.Count.ToString();
                            }
                            else if (query.ToLower().Trim().StartsWith("update") == true || query.ToLower().Trim().StartsWith("delete") == true || query.ToLower().Trim().StartsWith("insert") == true)
                            {
                                if (ds != null)
                                {
                                    this.Tabs.TabPages.Clear();
                                }
                                Object result = Server.get_Con().ExecuteNonQuery(CommandType.Text, sql.ToString().Replace("\r", String.Empty));
                                if (result != null)
                                {
                                    logMessage = result.ToString() + " Filas Afectadas en la Consulta";
                                }
                            }
                            else
                            {
                                //[Ezequiel] - 07/10/09 - Si el servidor es de oracle y la consulta comienza
                                //                        con exec entonces ejecuto un procedimiento     
                                if (Server.isOracle && query.ToLower().Trim().StartsWith("exec "))
                                {
                                    //[Ezequiel] - Guardo el nombre del store
                                    string spname = query.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1];
                                    //[Ezequiel] - Guardo los parametros.
                                    string param = query.Replace(query.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0] + " " + query.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1], "");
                                    //[Ezequiel] - Creo los arrays donde se van a guardar los parametros y valores
                                    ArrayList parNames = new ArrayList();
                                    ArrayList parValues = new ArrayList();

                                    //[Ezequiel] - Recorro la lista de parametros para cargar los datos en los arraylist
                                    while (param != string.Empty)
                                    {
                                        Boolean split = false;
                                        Int32 pos = 0;
                                        string par = string.Empty;
                                        //[Ezequiel] - Bucle que separa el nombre del parametro del valor
                                        while (!split)
                                        {
                                            //[Ezequiel] - Obtengo la posicion de la primera coma y la guardo en pos,
                                            // en el caso de que pos sea diferente de 0 es porque el valor tenia dentro una coma
                                            // entonces en la proxima iteracion a esa posicion le sumo 1 asi toma la segunda coma
                                            pos = param.IndexOf(",", (pos == 0 ? pos : pos + 1));
                                            //[Ezequiel] - Si la posicion es -1 es porque era el ultimo parametro.
                                            if (pos == -1)
                                                par = param;
                                            else
                                                par = param.Substring(0, pos);
                                            //[Ezequiel] - Si el parametro termina con comilla simple es porque esta completo
                                            // caso contrario habia una coma dentro del valor y sigue la iteracion
                                            if (par.Trim().EndsWith("'"))
                                            {
                                                //[Ezequiel] - Guardo el nombre del parametro
                                                string parname = par.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0];
                                                parNames.Add(parname.Trim());
                                                //[Ezequiel] - Guardo el valor del parametro
                                                parValues.Add(par.Substring(par.IndexOf(parname) + parname.Length).Trim().Replace("'", String.Empty));
                                                //[Ezequiel] - Si la longitud del parametro es igual a la lista de parametros
                                                // es porque ya no hay mas nada por recorrer
                                                if (param.Length == par.Length)
                                                    param = String.Empty;
                                                else
                                                    param = param.Substring(par.Length + 1);
                                                //[Ezequiel] - Pongo la bandera en true para que recorra el siguiente parametro.
                                                split = true;
                                            }
                                        }
                                    }

                                    //[Ezequiel] - Genero el array para los tipos de parametros
                                    string[] parTypes = new string[parNames.ToArray().Length];
                                    for (Int32 i = parNames.ToArray().Length - 1; i > -1; i--)
                                    {
                                        parNames[i] = parNames[i].ToString().Trim();
                                        parValues[i] = parValues[i].ToString().Trim();
                                        if (parNames[i].ToString().ToLower().CompareTo("io_cursor") == 0)
                                            parTypes[i] = "2";
                                        else
                                            parTypes[i] = "13";
                                    }
                                    ds = Server.get_Con().ExecuteDataset(spname, parNames.ToArray(), parTypes, parValues.ToArray());
                                }
                                else
                                    ds = Server.get_Con().ExecuteDataset(CommandType.Text, sql.ToString());

                                for (int i = 0; i < ds.Tables.Count; i++)
                                    this.addQuery(ds.Tables[i], query);

                                logMessage = "Filas obtenidas: " + ds.Tables[0].Rows.Count.ToString();

                            }
                        }
                        else
                        {
                            DBTYPES servertype = (DBTYPES)Enum.Parse(typeof(DBTYPES), cboServerType.Text, false);
                            if (servertype == DBTYPES.ODBC)
                                txtServerName.Text = cmbODBC.Text;
                            if (query.ToLower().Trim().StartsWith("select") == true || query.ToLower().Trim().StartsWith("exec"))
                            {
                                txtMensajes.Visible = false;
                                //[Ezequiel] - 07/10/09 - Si el servidor es de oracle y la consulta comienza
                                //                        con exec entonces ejecuto un procedimiento     
                                if ((cboServerType.SelectedIndex == 3 || cboServerType.SelectedIndex == 2) && query.ToLower().Trim().StartsWith("exec "))
                                {
                                    //[Ezequiel] - Guardo el nombre del store
                                    string spname = query.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1];
                                    //[Ezequiel] - Guardo los parametros.
                                    string param = query.Replace(query.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0] + " " + query.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1], "");
                                    //[Ezequiel] - Creo los arrays donde se van a guardar los parametros y valores
                                    ArrayList parNames = new ArrayList();
                                    ArrayList parValues = new ArrayList();

                                    //[Ezequiel] - Recorro la lista de parametros para cargar los datos en los arraylist
                                    while (param != string.Empty)
                                    {
                                        Boolean split = false;
                                        Int32 pos = 0;
                                        string par = string.Empty;
                                        //[Ezequiel] - Bucle que separa el nombre del parametro del valor
                                        while (!split)
                                        {
                                            //[Ezequiel] - Obtengo la posicion de la primera coma y la guardo en pos,
                                            // en el caso de que pos sea diferente de 0 es porque el valor tenia dentro una coma
                                            // entonces en la proxima iteracion a esa posicion le sumo 1 asi toma la segunda coma
                                            pos = param.IndexOf(",", (pos == 0 ? pos : pos + 1));
                                            //[Ezequiel] - Si la posicion es -1 es porque era el ultimo parametro.
                                            if (pos == -1)
                                                par = param;
                                            else
                                                par = param.Substring(0, pos);
                                            //[Ezequiel] - Si el parametro termina con comilla simple es porque esta completo
                                            // caso contrario habia una coma dentro del valor y sigue la iteracion
                                            if (par.Trim().EndsWith("'"))
                                            {
                                                //[Ezequiel] - Guardo el nombre del parametro
                                                string parname = par.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0];
                                                parNames.Add(parname.Trim());
                                                //[Ezequiel] - Guardo el valor del parametro
                                                parValues.Add(par.Substring(par.IndexOf(parname) + parname.Length).Trim().Replace("'", String.Empty));
                                                //[Ezequiel] - Si la longitud del parametro es igual a la lista de parametros
                                                // es porque ya no hay mas nada por recorrer
                                                if (param.Length == par.Length)
                                                    param = String.Empty;
                                                else
                                                    param = param.Substring(par.Length + 1);
                                                //[Ezequiel] - Pongo la bandera en true para que recorra el siguiente parametro.
                                                split = true;
                                            }
                                        }

                                    }

                                    //[Ezequiel] - Genero el array para los tipos de parametros
                                    string[] parTypes = new string[parNames.ToArray().Length];
                                    for (Int32 i = parNames.ToArray().Length - 1; i > -1; i--)
                                    {
                                        parNames[i] = parNames[i].ToString().Trim();
                                        parValues[i] = parValues[i].ToString().Trim();
                                        if (parNames[i].ToString().ToLower().CompareTo("io_cursor") == 0)
                                            parTypes[i] = "2";
                                        else
                                            parTypes[i] = "13";
                                    }
                                    ds = Server.get_Con(servertype, txtServerName.Text, txtDatabase.Text, txtUser.Text, txtPassword.Text, false, true).ExecuteDataset(spname, parNames.ToArray(), parTypes, parValues.ToArray());
                                }
                                else
                                    ds = Server.get_Con(servertype, txtServerName.Text, txtDatabase.Text, txtUser.Text, txtPassword.Text, false, true).ExecuteDataset(CommandType.Text, sql.ToString());
                                this.addQuery(ds.Tables[0], query);
                                logMessage = "Filas obtenidas: " + ds.Tables[0].Rows.Count.ToString();
                            }
                            else
                            {
                                if (ds != null)
                                {
                                    this.Tabs.TabPages.Clear();
                                }
                                Object result = Server.get_Con(servertype, txtServerName.Text, txtDatabase.Text, txtUser.Text, txtPassword.Text, false, false).ExecuteNonQuery(CommandType.Text, sql.ToString().Replace("\r", String.Empty));
                                if (result != null)
                                {
                                    logMessage = result.ToString() + " Filas Afectadas en la Consulta";
                                }
                            }
                        }

                        AddMessage(executionCounter, queryCounter, QueryResult.Ok, logMessage, query);
                    }
                    catch (Exception ex2)
                    {
                        AddMessage(executionCounter, queryCounter, QueryResult.Error, ex2.Message, query);
                        if (!chkContinueIfError.Checked) break;
                    }
                    finally
                    {
                        queryCounter++;
                    }
                }
            }
            catch (Exception ex)
            {
                AddMessage(0, 0, QueryResult.Error, ex.Message, string.Empty);
            }
            finally
            {
                executionCounter++;
                rgvLog.EndUpdate();
                if (tabOptions.SelectedIndex < 2) tabOptions.SelectedIndex = 2;
            }
        }

        private void AddMessageHeader()
        {
            string message = "- Ejecución Nº " + executionCounter.ToString() + ". " + DateTime.Now.ToString();
            txtMensajes.Text = message + _separatorHeader + txtMensajes.Text;
            txtMensajes.SelectionStart = txtMensajes.Text.Length;
            txtMensajes.SelectionLength = 0;
        }

        private void AddMessage(Int16 executionCounter, Int32 queryCounter, QueryResult result, string message, string sql)
        {
            //Log de texto
            txtMensajes.Text = message + "\r\n" + txtMensajes.Text;

            //Log de grilla
            dtLog.Rows.Add(new object[]
                               {
                                   executionCounter,
                                   queryCounter,
                                   result == QueryResult.Ok ? _correctResult : _incorrectResult,
                                   message,
                                   sql.Replace("\n", String.Empty).Replace("\r", String.Empty)
                               });
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == ((int)Keys.F5))
                btn_EjecutarQuery_Click(sender, e);
        }

        /// <summary>
        /// Metodo que agrega un tabpage con el resultado de la consulta.
        /// </summary>
        /// <param name="dt">Resultado de la consulta</param>
        /// <param name="sql">Titulo del tabpage</param>
        /// <history>
        /// [Ezequiel] 09/10/09 - Created
        /// </history>
        private void addQuery(DataTable dt, string sql)
        {
            //[Ezequiel] - Creo un nuevo tabpage
            TabPage page = new ZTabPage((sql.Length > 30 ? sql.Trim().Replace("\n", "").Replace("\r", "").Substring(0, 27) + "..." : sql.Trim().Replace("\n", "").Replace("\r", "")));
            page.ToolTipText = sql;
            //[Ezequiel] - creo el datagridview y se lo agrego al tabpage
            Grid.Grid.TelerikGrid dataGridView1 = new Grid.Grid.TelerikGrid(dt, false);
            //dataGridView1.DataError += DataGridView1_DataError;
            //dataGridView1.BackgroundColor = System.Drawing.Color.Silver;
            dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            //dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView1.Location = new System.Drawing.Point(2, 151);
            dataGridView1.Size = new System.Drawing.Size(679, 164);
            //dataGridView1.DataSource = dt;
            //[Ezequiel] - Agrego los controles
            page.Controls.Add(dataGridView1);
            this.Tabs.TabPages.Add(page);
        }

        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void chkUseAppIni_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseAppIni.Checked)
            {
                txtServerName.Enabled = false;
                cboServerType.Enabled = false;
                cmbODBC.Enabled = false;
                txtDatabase.Enabled = false;
                txtUser.Enabled = false;
                txtPassword.Enabled = false;
            }
            else
            {
                txtServerName.Enabled = true;
                cboServerType.Enabled = true;
                cmbODBC.Enabled = true;
                txtDatabase.Enabled = true;
                txtUser.Enabled = true;
                txtPassword.Enabled = true;
            }
        }

        private void cboServerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbODBC.SelectedIndex = -1;
            if (cboServerType.SelectedIndex == 6)
            {
                cmbODBC.Visible = true;
                GetSystemDataSourceNames();
            }
            else
                cmbODBC.Visible = false;
        }

        //<summary>
        // Gets all System data source names for the local machine.
        //</summary>
        public void GetSystemDataSourceNames()
        {
            //'get system dsn's
            Microsoft.Win32.RegistryKey reg = (Microsoft.Win32.Registry.LocalMachine).OpenSubKey("Software");
            if (reg != null)
            {
                reg = reg.OpenSubKey("ODBC");
                if (reg != null)
                {
                    reg = reg.OpenSubKey("ODBC.INI");
                    if (reg != null)
                    {
                        reg = reg.OpenSubKey("ODBC Data Sources");
                        if (reg != null)
                        {
                            //'Get all DSN entries defined in DSN_LOC_IN_REGISTRY.
                            foreach (string sName in reg.GetValueNames())
                                cmbODBC.Items.Add(sName);
                        }
                        try
                        {
                            reg.Close();
                            //' ignore this exception if we couldn't close
                        }
                        catch
                        { }
                    }
                }
            }
        }

        private void cmbODBC_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDatabase.Text = cmbODBC.Text;
            txtServerName.Text = cmbODBC.Text;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
           
            if (!String.IsNullOrEmpty(line))
            {
                line = line.ToUpper();
                //TRATA DE OBTENER EL USERID MANDADO COMO PARAMETRO
                if (line.Contains("USERID="))
                {
                    userId = int.Parse((line.Split(Char.Parse("="))[1]).Split(Char.Parse(" "))[0]);
                    //Remuevo los caracteres que no sirven para los parámetros.
                    line = line.Replace("USERID=" + userId.ToString(), "");
                    line = line.Trim();
                    if (!UserBusiness.Rights.ValidateLogIn(userId, ClientType.Desktop).Equals(null))
                    {
                        int timeout = int.Parse(UserPreferences.getValue("TimeOut", UPSections.UserPreferences, "20"));
                        string winusername = UserGroupBusiness.GetUserorGroupNamebyId(userId);
                        UcmServices.Login(timeout, "Query", userId, winusername, Environment.MachineName, 0, ServiceTypes.Report);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo validar el usuario de Zamba, se cerrara la aplicacion", "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Application.Exit();
                    }
                }
            }
            else
            {
                System.Reflection.Assembly assem = new RulesInstance().GetWFActivityRegularAssembly();
                this.Enabled = false;
                Login login = new Login(false, false, true, ObjectTypes.ModuleQuery, string.Empty, string.Empty, string.Empty, assem);
                try
                {
                    if (login.IsDisposed == false) login.ShowDialog();
                    if (login.DialogResult != System.Windows.Forms.DialogResult.OK)
                    {
                        Application.Exit();
                    }
                    else
                    {
                        Int64 userId = Membership.MembershipHelper.CurrentUser.ID;
                        if (this.Enabled == false) this.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    MessageBox.Show("Ocurrio un Error en el Sistema, al mostrar el dialogo de Usuario " + ex.ToString(), "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }      
            this.Text = this.Text + "(" + Application.ProductVersion + ")";
            string Name = ZOptBusiness.GetValue("ClientTitle");
            if (!String.IsNullOrEmpty(Name))
            {
                this.Text = this.Text + Name;
            }
            if (Membership.MembershipHelper.CurrentUser != null)
            {
                this.Text = this.Text + " - [" + Membership.MembershipHelper.CurrentUser.Name + "]";
            }
         }           
        


        private void rgvLog_GroupByChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            if (!rgvLog.Columns[0].IsVisible) rgvLog.Columns[0].IsVisible = true;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void txtQuery_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
