
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Text;
using Zamba.Core;
using System.Xml;
using System.Data.SqlClient;
using System.Diagnostics;
using Zamba;
using Zamba.Tools;
using Zamba.Servers;
using System.Collections;
using System.Linq;
using Telerik.WinControls.UI;
using System.IO;
using Zamba.Data;

namespace DbToXML
{

    public partial class DBToXML : Form
    {
        private const string _comillaDoble = "\"";
        private const string _espacio = " ";
        private const string _usuario = "user";
        private const string _coma = ",";
        private const string _punto = ".";
        private const string _nulo = "NULL";
        private const string _dosPuntos = ":";
        private const string _guion = "-";
        private const string _true = "true";
        private const string _false = "false";
        private const string _uno = "1";
        private const string _cero = "0";
        private const string _comillaSimple = "'";
        private const string _comillaSimple2 = "''";
        private const string _cierraParentesis = ")";
        private Dictionary<String, TableStructure> _tablesList = null;
        private Dictionary<String, TableStructure> _tablesDList = null;
        private string tablefile = Application.StartupPath + "\\tables.xml";
        private string resultsfile = string.Empty;

        public DBToXML()
        {
            try
            {
                InitializeComponent();
                string status = string.Empty;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);

            }
        }

        private void DBToXML_Load(object sender, EventArgs e)
        {
            try
            {
                LoadConnFiles();
                LoadTables(tablefile);
                this.lblXmlTablePath.Text = tablefile;
                this.lblXmlResultsPath.Text = resultsfile;
                ZTrace.SetLevel(1, "Zamba.DBToXML");
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);

            }
        }

        string temppath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Zamba Software");
        private void LoadConnFiles()
        {
            try
            {

                if (new FileInfo(Path.Combine(temppath, "LastConSourceFile.txt")).Exists)
                {
                    StreamReader sws = new StreamReader(Path.Combine(temppath, "LastConSourceFile.txt"));
                    fs = sws.ReadLine();
                    sws.Close();
                    sws.Dispose();
                    LoadSourceConn();
                }

                if (new FileInfo(Path.Combine(temppath, "LastConDestinyFile.txt")).Exists)
                {
                    StreamReader swd = new StreamReader(Path.Combine(temppath, "LastConDestinyFile.txt"));
                    fd = swd.ReadLine();
                    swd.Close();
                    swd.Dispose();
                    LoadDestConn();
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

        }

        private void LoadTables(String TablesFile)
        {
            string table = string.Empty;
            string filter = string.Empty;
            string keys = string.Empty;
            XmlTextReader textReader = null;

            try
            {
                _tablesList = new Dictionary<string, TableStructure>();
                _tablesDList = new Dictionary<string, TableStructure>();
                textReader = new XmlTextReader(TablesFile);

                if (textReader.Read())
                {
                    while (textReader.Read())
                    {
                        textReader.MoveToElement();

                        try
                        {

                            if (textReader.NodeType == XmlNodeType.Element)
                            {
                                if (textReader.Name == "name")
                                    table = textReader.ReadElementString();

                                if (textReader.Name == "filter")
                                    filter = textReader.ReadElementString();
                                if (filter == "N/A") filter = "";

                                if (textReader.Name == "enabled")
                                {
                                    string val = textReader.ReadElementString();
                                    if (val == "1" || val == "-1")
                                    {
                                        enabled = true;
                                    }
                                }

                                if (textReader.Name == "preserve")
                                {
                                    preserve = textReader.ReadElementString();
                                }

                                if (textReader.Name == "keys")
                                    keys = textReader.ReadElementString();
                                if (keys == "N/A") keys = "";

                                if (textReader.Name == "unique")
                                {
                                    unique = textReader.ReadElementString();
                                    if (unique == "N/A") unique = "";
                                }

                                if (textReader.Name == "dependencies")
                                {

                                    dependencies = textReader.ReadElementString();
                                    if (dependencies == "N/A") dependencies = "";

                                    TableStructure te = new TableStructure(table, filter, enabled, preserve, keys, unique, dependencies, ignorecompare, notupdate, notinsert);
                                    if (_tablesList.ContainsKey(table) == false)
                                    {
                                        _tablesList.Add(table, te);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Se ha detectado una tabla duplicada en el XML: " + table);
                                    }
                                    if (_tablesDList.ContainsKey(table) == false)
                                    {
                                        _tablesDList.Add(table, te);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Se ha detectado una tabla duplicada en el XML: " + table);
                                    }

                                }


                                if (textReader.Name == "ignorecompare")
                                {
                                    ignorecompare = textReader.ReadElementString();
                                    if (ignorecompare == "N/A") ignorecompare = "";
                                }

                                if (textReader.Name == "notupdate")
                                {
                                    notupdate = textReader.ReadElementString();
                                    if (notupdate == "N/A") notupdate = "";
                                }

                                if (textReader.Name == "notinsert")
                                {
                                    notinsert = textReader.ReadElementString();
                                    if (notinsert == "N/A") notinsert = "";
                                }

                            }

                        }
                        catch (Exception ex)
                        {
                            ZClass.raiseerror(ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                MessageBox.Show(ex.ToString(), "Error al leer configuracion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (textReader != null)
                {
                    try
                    {
                        textReader.Close();
                    }
                    catch (Exception)
                    {
                    }
                    textReader = null;
                }
            }

            try
            {
                this.webBrowser1.Navigate(TablesFile);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            try
            {
                this.RadGridView1.DataSource = _tablesList.Values;

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Guarda los valores en el XML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbSave_Click(object sender, EventArgs e)
        {
            try
            {

                DataSet XmlDs = new DataSet("Valores de la Base de datos");
                DataSet TempDs = null;
                String Query = null;
                Int32 topcount;
                prgBar.Value = 0;
                prgBar.Maximum = _tablesList.Count;

                foreach (KeyValuePair<String, TableStructure> Item in _tablesList)
                {
                    if (Item.Value.enabled)
                    {
                        prgBar.Value += 1;
                        topcount = 0;
                        if (this.chkRowTop.Checked && Int32.TryParse(this.txtRowTop.Text.Trim(), out topcount) && topcount > 0)
                        {
                            if (conSource.isOracle)
                            {
                                Query = "SELECT * FROM " + Item.Key;
                                if (!string.IsNullOrEmpty(Item.Value.filter))
                                    Query += " WHERE " + Item.Value.filter + " AND ROWNUM <= " + topcount.ToString();
                                else
                                    Query += " WHERE ROWNUM <= " + topcount.ToString();
                            }
                            else if (conSource.isODBC)
                            {
                                Query = "SELECT * FROM " + Item.Key;
                                if (!string.IsNullOrEmpty(Item.Value.filter))
                                    Query += " WHERE " + Item.Value.filter;
                                Query += " FETCH FIRST " + topcount.ToString() + " ROWS ONLY";
                            }
                            else
                            {
                                Query = "SELECT TOP " + topcount + " * FROM " + Item.Key;
                                if (!string.IsNullOrEmpty(Item.Value.filter))
                                    Query += " WHERE " + Item.Value.filter;
                            }
                        }
                        else
                        {
                            Query = "SELECT * FROM " + Item.Key;

                            if (!string.IsNullOrEmpty(Item.Value.filter))
                            {
                                Query += " WHERE " + Item.Value.filter;
                            }
                        }



                        try
                        {
                            TempDs = conSource.ExecuteDataset(CommandType.Text, Query);

                            if (null != TempDs && TempDs.Tables.Count > 0 && TempDs.Tables[0].Rows.Count > 0)
                            {
                                TempDs.Tables[0].TableName = Item.Key;
                                TempDs.Tables[0].Columns.Add("Script", typeof(String));

                                //Arreglo de Columnas diferentes entre sql y oracle C_
                                if (conDestiny.isOracle == false && conSource.isOracle == true)
                                {
                                    foreach (DataColumn c in TempDs.Tables[0].Columns)
                                    {
                                        if (c.ColumnName.ToLower().StartsWith("c_"))
                                        {
                                            //ESTO ES UN PROBLEMA!
                                            c.ColumnName = c.ColumnName.Substring(2, c.ColumnName.Length - 2);
                                        }
                                    }
                                }
                                TempDs.AcceptChanges();

                                try
                                {
                                    String Script = string.Empty;
                                    if (Item.Key == "DOC_INDEX")
                                    {
                                        foreach (DataRow r in TempDs.Tables[0].Rows)
                                        {
                                            Int64 IndexId = Int64.Parse(r["INDEX_ID"].ToString());
                                            IndexAdditionalType IndexDropdown = (IndexAdditionalType)int.Parse(r["DROPDOWN"].ToString());
                                            Object ScriptO = null;

                                            if (IndexDropdown == IndexAdditionalType.AutoSustitución || IndexDropdown == IndexAdditionalType.AutoSustituciónJerarquico)
                                            {
                                                ScriptO = conSource.ExecuteScalar(CommandType.Text, string.Format("select text from dba_views where LOWER(owner) = '{0}'and view_name = 'SLST_S{1}'", Server.dbOwner.ToLower(), IndexId));
                                            }
                                            if (IndexDropdown == IndexAdditionalType.DropDown || IndexDropdown == IndexAdditionalType.DropDownJerarquico)
                                            {
                                                ScriptO = conSource.ExecuteScalar(CommandType.Text, string.Format("select text from dba_views where LOWER(owner)  = '{0}'and view_name = 'SLST_S{1}'", Server.dbOwner.ToLower(), IndexId));
                                            }
                                            if (ScriptO != null)
                                                r["Script"] = ScriptO.ToString();
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ZClass.raiseerror(ex);
                                }
                                TempDs.AcceptChanges();
                                XmlDs.Tables.Add(TempDs.Tables[0].Copy());


                            }
                        }
                        catch (Exception ex)
                        {
                            ZClass.raiseerror(ex);
                            //  MessageBox.Show("Se produjo un error al exportar la tabla: " + Item.Key);
                        }
                    }

                }

                XmlDs.WriteXml(resultsfile, XmlWriteMode.WriteSchema);

                try
                {
                    this.webBrowser2.Navigate(resultsfile);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }

                MessageBox.Show("Proceso Terminado");


            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

        }

        /// <summary>
        /// Carga los valores en la base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btLoad_Click(object sender, EventArgs e)
        {
            ProcessParsedXML(false);
        }

        private void ProcessParsedXML(Boolean IsTest)
        {
            try
            {

                StreamWriter SW = new StreamWriter(Path.Combine(temppath, "SQLOut" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + ".txt"));

                if (conDestiny == null || conDestiny.CN == null)
                    conDestiny = Zamba.Servers.Server.get_Con(apDestiny.SERVERTYPE, apDestiny.SERVER, apDestiny.DB, apDestiny.USER, apDestiny.PASSWORD, true, false);

                Transaction transaction = new Transaction(conDestiny);

                try
                {

                    DataSet XmlDs = new DataSet();
                    DataSet BeforeDs = new DataSet();
                    DataSet AfterDs = new DataSet();
                    String SelectQuery;
                    String InsertQuery;
                    // String UpdateQuery;
                    String DeleteQuery;

                    StringBuilder QueryBuilderInsert;
                    StringBuilder QueryBuilderSelect;
                    //  StringBuilder QueryBuilderUpdate;
                    //StringBuilder QueryBuilderDelete;

                    DateTime TryValue;
                    int i;
                    object CurrentValue;
                    string strCurrentValue;
                    IDbDataParameter[] blobData = null;
                    int blobCount = 0;
                    List<int> blobPositions = new List<int>();

                    string blobParam;

                    try
                    {
                        ZTrace.WriteLineIf(ZTrace.IsError, "Leyendo XML: " + resultsfile);
                        this.listView1.Items.Add("Leyendo XML: " + resultsfile);

                        XmlDs.ReadXml(resultsfile);

                        prgBar.Value = 0;
                        prgBar.Maximum = XmlDs.Tables.Count;

                        List<Int64> NewEntities = new List<Int64>();
                        List<NewIndex> NewIndexsInEntities = new List<NewIndex>();
                        List<NewIndex> NewIndexs = new List<NewIndex>();


                        foreach (DataTable CurrentTable in XmlDs.Tables)
                        {
                            ZTrace.WriteLineIf(ZTrace.IsError, "Leyendo XML Tabla: " + CurrentTable.TableName);
                            this.listView1.Items.Add("Leyendo XML Tabla: " + CurrentTable.TableName);
                            Application.DoEvents();

                            prgBar.Value += 1;
                            VerifyTable(conDestiny, CurrentTable);


                            if (IsTest == false) DisableConstrainsOfTable(conDestiny, CurrentTable.TableName);

                            String IndentityDisabledString = GetDisableIdentityColumnsString(CurrentTable.TableName);
                            String IndentityEnableString = GetReEnableIdentityColumnsString(CurrentTable.TableName);


                            InsertQuery = BuildTableQueryInsert(CurrentTable);
                            SelectQuery = BuildTableQuerySelect(CurrentTable);
                            //  UpdateQuery = BuildTableQueryUpdate(CurrentTable, null);
                            //DeleteQuery = BuildTableQueryDelete(CurrentTable);

                            QueryBuilderInsert = new StringBuilder();
                            QueryBuilderSelect = new StringBuilder();
                            //QueryBuilderUpdate = new StringBuilder();
                            //QueryBuilderDelete = new StringBuilder();

                            //Se verifica la existencia de archivos digitales (blobs)
                            foreach (DataColumn col in CurrentTable.Columns)
                            {
                                //Arreglo de Columnas diferentes entre sql y oracle C_
                                if (conDestiny.isOracle == false && conSource.isOracle == true)
                                {
                                    if (col.ColumnName.ToLower().StartsWith("c_"))
                                    {
                                        //ESTO ES UN PROBLEMA!
                                        col.ColumnName = col.ColumnName.Substring(2, col.ColumnName.Length - 2);
                                    }

                                    CurrentTable.AcceptChanges();
                                }

                                if (col.DataType == typeof(Byte[]))
                                {
                                    if (conDestiny.isOracle)
                                    {
                                        blobCount++;
                                        blobPositions.Add(col.Ordinal);
                                    }
                                    else
                                    {
                                        blobCount++;
                                        blobPositions.Add(col.Ordinal);
                                    }
                                }
                            }

                            if (blobCount > 0)
                            {
                                blobData = new IDbDataParameter[blobCount];
                                blobCount = 0;
                            }

                            foreach (DataRow r1 in CurrentTable.Rows)
                            {
                                QueryBuilderInsert.Remove(0, QueryBuilderInsert.Length);
                                QueryBuilderInsert.Append(IndentityDisabledString);
                                QueryBuilderInsert.Append(InsertQuery);

                                QueryBuilderSelect.Remove(0, QueryBuilderSelect.Length);
                                QueryBuilderSelect.Append(SelectQuery);

                                //QueryBuilderUpdate.Remove(0, QueryBuilderUpdate.Length);
                                // QueryBuilderUpdate.Append(UpdateQuery);

                                //QueryBuilderDelete.Remove(0, QueryBuilderDelete.Length);
                                //QueryBuilderDelete.Append(DeleteQuery);

                                for (i = 0; i < r1.ItemArray.Length; i++)
                                {
                                    String CurrentColumnName = r1.Table.Columns[i].ToString();
                                    if (CurrentColumnName != "Script" && CurrentColumnName != "Compare")
                                    {
                                        CurrentValue = r1.ItemArray[i];
                                        strCurrentValue = CurrentValue.ToString();

                                        if (blobPositions.Contains(i))
                                        {//Verifica si es un blob
                                            blobParam = "@blob" + i.ToString();
                                            QueryBuilderInsert.Append(blobParam);
                                            SqlParameter pDocFile = default(SqlParameter);

                                            //TODO: FALTA LA PARTE DE ORACLE
                                            pDocFile = new SqlParameter(blobParam, SqlDbType.VarBinary, ((byte[])CurrentValue).Length, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, CurrentValue);

                                            blobData[blobCount] = pDocFile;
                                            blobCount++;
                                        }
                                        else if ((strCurrentValue.Contains(_guion) || strCurrentValue.Contains("/")) && strCurrentValue.Contains(_dosPuntos) && DateTime.TryParse(strCurrentValue, out TryValue))
                                        {//Verifica si es una fecha
                                            QueryBuilderInsert.Append(conDestiny.ConvertDateTime(TryValue));
                                            QueryBuilderSelect.Replace("{" + i.ToString() + "}", conDestiny.ConvertDateTime(TryValue));
                                            //      QueryBuilderUpdate.Append(conDestiny.ConvertDateTime(TryValue));
                                            //QueryBuilderDelete.Append(conDestiny.ConvertDateTime(TryValue));
                                        }
                                        else if (chkUseBool.Checked && String.Compare(strCurrentValue, _true, true) == 0)
                                        {//Verifica si es un booleano True
                                            QueryBuilderInsert.Append(_uno);
                                            QueryBuilderSelect.Replace("{" + i.ToString() + "}", _uno);
                                            //QueryBuilderUpdate.Append(_uno);
                                            //QueryBuilderDelete.Append(_uno);
                                        }
                                        else if (chkUseBool.Checked && String.Compare(strCurrentValue, _false, true) == 0)
                                        {//Verifica si es un booleano False
                                            QueryBuilderInsert.Append(_cero);
                                            QueryBuilderSelect.Replace("{" + i.ToString() + "}", _cero);
                                            //QueryBuilderUpdate.Append(_cero);
                                            //QueryBuilderDelete.Append(_cero);
                                        }
                                        else if (CurrentValue is DBNull && conDestiny.isOracle == false)
                                        {//Verifica si es nulo
                                            QueryBuilderInsert.Append(_nulo);
                                            QueryBuilderSelect.Replace("{" + i.ToString() + "}", _nulo);
                                            //QueryBuilderUpdate.Append(_nulo);
                                            //QueryBuilderDelete.Append(_nulo);
                                        }
                                        else
                                        {//Se agrega el valor sin modificación
                                            strCurrentValue = strCurrentValue.Replace(_comillaSimple, _comillaSimple2);
                                            QueryBuilderInsert.Append(_comillaSimple);
                                            QueryBuilderInsert.Append(strCurrentValue);
                                            QueryBuilderInsert.Append(_comillaSimple);

                                            // QueryBuilderSelect.Append(_comillaSimple);
                                            if (strCurrentValue == string.Empty)
                                            {
                                                //quitar esta columna del select
                                                QueryBuilderSelect.Replace(" = {" + i.ToString() + "}", " is null ");
                                            }
                                            else
                                            {
                                                QueryBuilderSelect.Replace("{" + i.ToString() + "}", _comillaSimple + strCurrentValue + _comillaSimple);
                                            }//      QueryBuilderSelect.Append(_comillaSimple);

                                            //QueryBuilderUpdate.Append(_comillaSimple);
                                            //QueryBuilderUpdate.Append(strCurrentValue);
                                            //QueryBuilderUpdate.Append(_comillaSimple);

                                            //QueryBuilderDelete.Append(_comillaSimple);
                                            //QueryBuilderDelete.Append(strCurrentValue);
                                            //QueryBuilderDelete.Append(_comillaSimple);
                                        }

                                        QueryBuilderInsert.Append(_coma);
                                        //                                QueryBuilderUpdate.Append(_coma);
                                        //QueryBuilderDelete.Append(_coma);
                                    }
                                }

                                //Se remueve la coma que queda
                                QueryBuilderInsert.Remove(QueryBuilderInsert.Length - 1, 1);
                                QueryBuilderInsert.Append(_cierraParentesis);
                                QueryBuilderInsert.Append(IndentityEnableString);

                                QueryBuilderSelect.Append(IndentityEnableString);

                                //QueryBuilderUpdate.Remove(QueryBuilderInsert.Length - 1, 1);
                                //QueryBuilderUpdate.Append(_cierraParentesis);
                                //QueryBuilderUpdate.Append(IndentityEnableString);

                                //QueryBuilderDelete.Remove(QueryBuilderInsert.Length - 1, 1);
                                //QueryBuilderDelete.Append(_cierraParentesis);
                                //QueryBuilderDelete.Append(IndentityEnableString);

                                try
                                {
                                    if (blobCount > 0)
                                    {
                                        // ZTrace.WriteLineIf(ZTrace.IsError, "Armo Query Insert Tabla con BLOB: " + CurrentTable.TableName + " - " + QueryBuilderInsert.ToString());
                                        this.listView1.Items.Add(QueryBuilderInsert.ToString());
                                        Application.DoEvents();

                                        SW.WriteLine(QueryBuilderInsert + ";");
                                        if (IsTest == false) conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, QueryBuilderInsert.ToString(), blobData);
                                        blobCount = 0;
                                    }
                                    else
                                    {

                                        Int32 count = Int32.Parse(conDestiny.ExecuteScalar(transaction.Transaction, CommandType.Text, QueryBuilderSelect.ToString()).ToString());

                                        if (count == 0)
                                        {
                                            //  ZTrace.WriteLineIf(ZTrace.IsError, "Armo Query Insert Tabla: " + CurrentTable.TableName + " - " + QueryBuilderInsert.ToString());
                                            this.listView1.Items.Add(QueryBuilderInsert.ToString());
                                            this.listView1.Items[this.listView1.Items.Count - 1].EnsureVisible();
                                            if (QueryBuilderInsert.ToString().Contains("Error"))
                                                this.listView1.Items[this.listView1.Items.Count - 1].ForeColor = System.Drawing.Color.Red;
                                            Application.DoEvents();
                                            SW.WriteLine(QueryBuilderInsert + ";");
                                            if (IsTest == false) conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, QueryBuilderInsert.ToString());

                                            if (CurrentTable.TableName == "DOC_TYPE")
                                            {
                                                NewEntities.Add(Int64.Parse(r1["DOC_TYPE_ID"].ToString()));
                                            }

                                            if (CurrentTable.TableName == "INDEX_R_DOC_TYPE")
                                            {
                                                NewIndexsInEntities.Add(new NewIndex(Int64.Parse(r1["INDEX_ID"].ToString()), Int64.Parse(r1["DOC_TYPE_ID"].ToString())));
                                            }

                                            if (CurrentTable.TableName == "DOC_INDEX")
                                            {
                                                NewIndexs.Add(new NewIndex(Int64.Parse(r1["INDEX_ID"].ToString()), string.Empty, Int32.Parse(r1["Index_Len"].ToString()), (IndexDataType)Int32.Parse(r1["Index_Type"].ToString()), (IndexAdditionalType)Int32.Parse(r1["DropDown"].ToString())));
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    if (ex.Message.Contains("Primary") || ex.Message.Contains("unique"))
                                    {
                                        this.listView1.Items.Add(ex.Message + ": " + QueryBuilderInsert.ToString());
                                        //                                    conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, QueryBuilderUpdate.ToString());
                                    }
                                    else if (ex.Message.Contains("parent key not found"))
                                    {
                                        this.listView1.Items.Add(ex.Message + ": " + QueryBuilderInsert.ToString());
                                    }
                                    else
                                    {
                                        ZClass.raiseerror(ex);
                                        this.listView1.Items.Add(ex.Message);
                                        Application.DoEvents();
                                    }
                                }

                                QueryBuilderInsert.Remove(0, QueryBuilderInsert.Length);
                            }

                            blobPositions.Clear();


                        }


                        if (IsTest)
                            transaction.Rollback();
                        else
                        {
                            transaction.Commit();
                        }
                        #region PostScripts



                        foreach (Int64 EntityId in NewEntities)
                        {
                            Zamba.Servers.Server.CreateTables().AddDocsTables(EntityId);
                        }

                        foreach (NewIndex NI in NewIndexsInEntities)
                        {
                            IIndex Index = Zamba.Core.IndexsBusiness.GetIndexById(NI.IndexId, string.Empty);
                            NI.IndexType = Index.Type;
                            NI.IndexLen = Index.Len;

                            if (!IndexsBusiness.getReferenceStatus(NI.DocTypeId, NI.IndexId))
                            {
                                if (DocTypesBusiness.CheckColumn(NI.DocTypeId, NI.IndexId, NI.IndexType, NI.IndexLen) == false)
                                {
                                    DocTypesBusiness.AddColumn(NI.DocTypeId, NI.IndexId, NI.IndexType, NI.IndexLen);
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Se agrega el indice Id: {0} en  DocTypeId: {1}.", NI.IndexId, NI.DocTypeId));
                                }
                                else
                                {
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("El indice Id: {0} existe en la entidad DocTypeId: {1}.", NI.IndexId, NI.DocTypeId));
                                }

                            }
                            else
                            {
                                ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("El indice Id: {0} DocTypeId: {1} es referencial.", NI.IndexId, NI.DocTypeId));
                            }
                        }

                        foreach (NewIndex NI in NewIndexs)
                        {
                            if (NI.DropDown == IndexAdditionalType.AutoSustitución || NI.DropDown == IndexAdditionalType.AutoSustituciónJerarquico)
                            {
                                if (NI.Script.Length > 0)
                                {
                                    IndexsBusiness.addindexsust(NI.IndexId, NI.VistaoTabla, NI.ColumnaCodigo, NI.ColumnaDescripcion, NI.Script);
                                }
                                else
                                {
                                    IndexsBusiness.createsustituciontable(NI.IndexId, NI.IndexLen, (IndexDataType)Enum.Parse(typeof(IndexDataType), NI.IndexType.ToString()));
                                }
                            }
                            else if (NI.DropDown == IndexAdditionalType.DropDown || NI.DropDown == IndexAdditionalType.DropDownJerarquico)
                            {
                                if (NI.Script.Length > 0)
                                {
                                    IndexsBusiness.addindexlist(NI.IndexId, NI.VistaoTabla, NI.ColumnaCodigo, NI.ColumnaDescripcion, NI.Script);
                                }
                                else
                                {
                                    IndexsBusiness.addindexlist(NI.IndexId, NI.IndexLen);
                                }
                            }
                            else
                            {
                            }

                            //TODO: Falta agregar la relacion de las DOC_I a las listas
                        }



                        if (chkAfterScripts.Checked == true)
                        {
                            AfterDs.ReadXml(Application.StartupPath + "\\AfterScripts.xml");

                            foreach (DataRow dr in AfterDs.Tables[0].Rows)
                            {
                                ZScript script = new ZScript(Int64.Parse(dr[0].ToString()), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
                                SW.WriteLine(script.Consulta + ";");
                                if (IsTest == false) UpdaterBusiness.ExecuteQuery(conDestiny, script.Consulta);
                            }

                            AfterDs.Clear();
                            AfterDs.Dispose();
                            AfterDs = null;
                        }
                        #endregion



                        MessageBox.Show("Proceso Finalizado");
                    }
                    catch (Exception ex)
                    {
                        ZClass.raiseerror(ex);
                        transaction.Rollback();
                        this.listView1.Items.Add(ex.Message);
                        Application.DoEvents();
                    }
                }
                catch (Exception ex)
                {

                    ZClass.raiseerror(ex);
                    transaction.Rollback();
                    this.listView1.Items.Add(ex.Message);
                    Application.DoEvents();
                }
                finally
                {
                    if (transaction != null)
                        transaction.Dispose();

                    SW.Flush();
                    SW.Close();
                    SW.Dispose();

                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Verifica la existencia de la tabla encontrada en ParsedXML sobre la conexión actual.
        /// Si la tabla no existe, busca el esquema guardado en el xml y la crea.
        /// </summary>
        /// <param name="CurrentTable"></param>
        /// <remarks>Solo funciona para SQL Server</remarks>
        private void VerifyTable(IConnection con, DataTable CurrentTable)
        {
            if (con.isOracle == false)
            {
                var connection = (System.Data.SqlClient.SqlConnection)con.CN;
                if (connection != null) connection.Open();
                DbToXML.TablesDBSchemaHelper th = new TablesDBSchemaHelper(connection);
                if (!th.TableExist(CurrentTable))
                    th.CreateFromDataTable(CurrentTable);
            }
        }

        private void DisableConstrainsOfTable(IConnection con, String TableName)
        {
            try
            {
                if (con.isOracle == false)
                {
                    con.ExecuteNonQuery(CommandType.Text, String.Format("ALTER TABLE {0} NOCHECK CONSTRAINT ALL", TableName));
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void ReEnableConstrainsOfTable(IConnection con, String TableName)
        {
            try
            {
                if (con.isOracle == false)
                {
                    con.ExecuteNonQuery(CommandType.Text, String.Format("ALTER TABLE {0} WITH NOCHECK CHECK CONSTRAINT ALL", TableName));
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private String GetDisableIdentityColumnsString(String TableName)
        {
            try
            {

                if (conDestiny.isOracle == false)
                {
                    Int32 count = Int32.Parse(conDestiny.ExecuteScalar(CommandType.Text, String.Format("select count(1) from INFORMATION_SCHEMA.COLUMNS where TABLE_name = '{0}' and COLUMNPROPERTY(object_id(TABLE_NAME), COLUMN_NAME, 'IsIdentity') = 1 ", TableName)).ToString());
                    if (count > 0)
                    {
                        //                    Server.get_Con().ExecuteNonQuery(CommandType.Text, String.Format("SET IDENTITY_INSERT {0} ON", TableName));
                        //    return true;

                        return String.Format("  SET IDENTITY_INSERT {0} ON  ", TableName);
                    }
                    return string.Empty;

                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return string.Empty;
            }
        }

        private String GetReEnableIdentityColumnsString(String TableName)
        {
            try
            {
                if (conDestiny.isOracle == false)
                {
                    Int32 count = Int32.Parse(conDestiny.ExecuteScalar(CommandType.Text, String.Format("select count(1) from INFORMATION_SCHEMA.COLUMNS where TABLE_name = '{0}' and COLUMNPROPERTY(object_id(TABLE_NAME), COLUMN_NAME, 'IsIdentity') = 1 ", TableName)).ToString());
                    if (count > 0)
                    {
                        ///    Server.get_Con().ExecuteNonQuery(CommandType.Text, String.Format("SET IDENTITY_INSERT {0} OFF", TableName));
                        return String.Format("  SET IDENTITY_INSERT {0} OFF  ", TableName);
                    }
                    return string.Empty;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return string.Empty;
            }
        }

        /// <summary>
        /// Construye el query para insertar los registros leidos desde el XML en la base de datos que se encuentre conectado
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private static String BuildTableQueryInsert(DataTable table)
        {
            var sbQuery = new StringBuilder();

            sbQuery.Append("INSERT INTO ");
            sbQuery.Append(table.TableName);
            sbQuery.Append("(");



            foreach (DataColumn currentColumn in table.Columns)
            {
                if (currentColumn.ColumnName != "Script" && currentColumn.ColumnName != "Compare")
                {
                    if (currentColumn.ColumnName.Contains(_espacio) || string.Compare(currentColumn.ColumnName.ToLower(), _usuario) == 0)
                    {
                        sbQuery.Append(_comillaDoble);
                        sbQuery.Append(currentColumn.ColumnName);
                        sbQuery.Append(_comillaDoble);
                    }
                    else
                        sbQuery.Append(currentColumn.ColumnName);

                    sbQuery.Append(_coma);
                }
            }



            //saco la ultima que quedo
            sbQuery.Remove(sbQuery.Length - 1, 1);
            sbQuery.Append(") VALUES(");

            return sbQuery.ToString();
        }

        private static String BuildTableQuerySelect(DataTable table)
        {
            var sbQuery = new StringBuilder();

            sbQuery.Append("select count(1) from ");
            sbQuery.Append(table.TableName);
            sbQuery.Append(" where ");

            Int32 i = 0;
            foreach (DataColumn currentColumn in table.Columns)
            {


                if (currentColumn.ColumnName != "Script" && currentColumn.ColumnName != "Compare")
                {
                    if (currentColumn.ColumnName.Contains(_espacio) || string.Compare(currentColumn.ColumnName.ToLower(), _usuario) == 0)
                    {
                        sbQuery.Append(_comillaDoble);
                        sbQuery.Append(currentColumn.ColumnName);
                        sbQuery.Append(_comillaDoble);
                    }
                    else
                    {
                        sbQuery.Append(currentColumn.ColumnName);
                    }

                    sbQuery.Append(" = ");

                    if (currentColumn.DataType.ToString() == "String") sbQuery.Append(" '");
                    sbQuery.Append("{");
                    sbQuery.Append(i.ToString());
                    sbQuery.Append("}");
                    if (currentColumn.DataType.ToString() == "String") sbQuery.Append("' ");

                    sbQuery.Append(" and ");

                    i++;
                }
            }

            //saco la ultima que quedo
            sbQuery.Remove(sbQuery.Length - 4, 4);
            return sbQuery.ToString();
        }


        private static String BuildTableQueryUpdate(DataTable table, TableStructure Item)
        {
            var sbQuery = new StringBuilder();
            var sbQueryW = new StringBuilder();
            var sbQueryC = new StringBuilder();

            sbQuery.Append("UPDATE ");
            sbQuery.Append(table.TableName);
            sbQuery.Append(" set ");

            sbQueryW.Append(" where ");

            Int32 i = 0;
            foreach (DataColumn currentColumn in table.Columns)
            {


                if (currentColumn.ColumnName != "Script" && currentColumn.ColumnName != "Compare")
                {

                    if (currentColumn.ColumnName.Contains(_espacio) || string.Compare(currentColumn.ColumnName.ToLower(), _usuario) == 0)
                    {
                        sbQuery.Append(_comillaDoble);
                        sbQuery.Append(currentColumn.ColumnName);
                        sbQuery.Append(_comillaDoble);

                        if (Item.keys.ToLower().Contains(currentColumn.ColumnName.ToLower()))
                        {
                            sbQueryC.Append(_comillaDoble);
                            sbQueryC.Append(currentColumn.ColumnName);
                            sbQueryC.Append(_comillaDoble);
                        }
                    }
                    else
                    {
                        sbQuery.Append(currentColumn.ColumnName);

                        if (Item.keys.ToLower().Contains(currentColumn.ColumnName.ToLower()))
                        {
                            sbQueryC.Append(currentColumn.ColumnName);
                        }
                    }

                    sbQuery.Append(" = ");

                    if (Item.keys.ToLower().Contains(currentColumn.ColumnName.ToLower()))
                    {
                        sbQueryC.Append(" = ");
                    }

                    if (currentColumn.DataType.ToString() == "String") sbQuery.Append(" '");
                    sbQuery.Append("{");
                    sbQuery.Append(i);
                    sbQuery.Append("}");
                    if (currentColumn.DataType.ToString() == "String") sbQuery.Append("' ");

                    sbQuery.Append(_coma);

                    if (Item.keys.ToLower().Contains(currentColumn.ColumnName.ToLower()))
                    {

                        if (currentColumn.DataType.ToString() == "String") sbQueryC.Append(" '");
                        sbQueryC.Append("{");
                        sbQueryC.Append(i);
                        sbQueryC.Append("}");
                        if (currentColumn.DataType.ToString() == "String") sbQueryC.Append("' ");
                        sbQueryC.Append(" and ");
                    }
                    i++;
                }
            }

            //saco la ultima que quedo
            sbQuery.Remove(sbQuery.Length - 1, 1);
            if (sbQueryC.Length > 3) sbQueryC.Remove(sbQueryC.Length - 4, 4);

            sbQuery.Append(sbQueryW.ToString());
            sbQuery.Append(sbQueryC.ToString());

            return sbQuery.ToString();
        }

        private static String BuildTableQueryDelete(DataRow r2, TableStructure value)
        {
            var sbQuery = new StringBuilder();
            var sbQueryW = new StringBuilder();

            sbQuery.Append("delete ");
            sbQuery.Append(value.name);

            sbQueryW.Append(" where ");

            Int32 i = 0;
            foreach (DataColumn currentColumn in r2.Table.Columns)
            {
                if (currentColumn.ColumnName != "Script" && currentColumn.ColumnName != "Compare")
                {
                    if (value.keys.ToLower().Contains(currentColumn.ColumnName.ToLower()))
                    {
                        if (currentColumn.ColumnName.Contains(_espacio) || string.Compare(currentColumn.ColumnName.ToLower(), _usuario) == 0)
                        {

                            sbQueryW.Append(_comillaDoble);
                            sbQueryW.Append(currentColumn.ColumnName);
                            sbQueryW.Append(_comillaDoble);
                        }
                        else
                        {
                            sbQueryW.Append(currentColumn.ColumnName);
                        }

                        sbQueryW.Append(" = ");


                        if (currentColumn.DataType.ToString() == "String") sbQueryW.Append(" '");
                        sbQueryW.Append(r2[currentColumn.ColumnName].ToString());
                        if (currentColumn.DataType.ToString() == "String") sbQueryW.Append("' ");
                        sbQueryW.Append(" and ");
                        i++;
                    }
                }
            }

            //saco la ultima que quedo
            if (sbQueryW.Length > 3) sbQueryW.Remove(sbQueryW.Length - 4, 4);

            sbQuery.Append(sbQueryW.ToString());

            return sbQuery.ToString();
        }
        /// <summary>
        /// Habilita o deshabilita el campo de texto para fijar el tope de filas a obtener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.txtRowTop.Enabled = this.chkRowTop.Checked;
        }

        private string TablesXML;
        /// <summary>
        /// Botón que permite cargar un archivo XML para configurar las tablas a guardar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                this.XmlFileDialog.Filter = "Archivos xml (*.xml)|*.xml";
                this.XmlFileDialog.DefaultExt = ".xml";
                this.XmlFileDialog.FileName = "tables.xml";
                this.XmlFileDialog.CheckFileExists = true;
                if (this.XmlFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.lblXmlTablePath.Text = this.XmlFileDialog.FileName;
                    TablesXML = new System.IO.FileInfo(this.XmlFileDialog.FileName).Name;
                    LoadTables(this.XmlFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {

                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Boton que permite cargar una ruta de archivo XML para guardar la información obtenida de las tablas seleccionadas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                this.XmlFileDialog.Filter = "Archivos xml (*.xml)|*.xml";
                this.XmlFileDialog.DefaultExt = ".xml";
                this.XmlFileDialog.FileName = "ParsedXml.xml";
                this.XmlFileDialog.CheckFileExists = false;
                if (this.XmlFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.resultsfile = this.XmlFileDialog.FileName;
                    this.lblXmlResultsPath.Text = resultsfile;
                }

                try
                {
                    this.webBrowser2.Navigate(resultsfile);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }
            }
            catch (Exception ex)
            {

                ZClass.raiseerror(ex);
            }
        }

        private void prgBar_Click(object sender, EventArgs e)
        {

        }

        private void zPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        IConnection conSource;
        IConnection conDestiny;
        private void zButton1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fd1 = new OpenFileDialog();
                if (fd1.ShowDialog() == DialogResult.OK)
                {
                    fs = fd1.FileName;
                    LoadSourceConn();

                    StreamWriter sw = new StreamWriter(Path.Combine(temppath, "LastConSourceFile.txt"));
                    sw.WriteLine(fs + ";");
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
            }
            catch (Exception)
            {

            }

        }

        private void LoadSourceConn()
        {
            ApplicationConfig ap = new ApplicationConfig(fs);
            conSource = Zamba.Servers.Server.get_Con(ap.SERVERTYPE, ap.SERVER, ap.DB, ap.USER, ap.PASSWORD, true, false);

            var sbConnection = new StringBuilder();
            sbConnection.Append("Datos de Conexion Origen");
            sbConnection.Append("Servidor: ");
            sbConnection.AppendLine(ap.SERVER);
            sbConnection.Append("Base de datos: ");
            sbConnection.AppendLine(ap.DB);
            sbConnection.Append("Usuario: ");
            sbConnection.Append(ap.USER);
            txtConnection.Text = sbConnection.ToString();
            sbConnection = null;
            ZTrace.WriteLineIf(ZTrace.IsError, "Conectando con Origen: " + txtConnection.Text);
        }

        ApplicationConfig apDestiny = null;
        string fd = string.Empty;
        string fs = string.Empty;

        public string unique { get; private set; }
        public string preserve { get; private set; }
        public Boolean enabled { get; private set; }
        public string dependencies { get; private set; }

        public string ignorecompare { get; private set; }
        public string notupdate { get; private set; }
        public string notinsert { get; private set; }

        private void zButton2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fd1 = new OpenFileDialog();
                if (fd1.ShowDialog() == DialogResult.OK)
                {
                    fd = fd1.FileName;
                    LoadDestConn();

                    StreamWriter sw = new StreamWriter(Path.Combine(temppath, "LastConDestinyFile.txt"));
                    sw.WriteLine(fd + ";");
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
            }
            catch (Exception)
            {

            }


        }

        private void LoadDestConn()
        {
            apDestiny = new ApplicationConfig(fd);
            ConnectToDestiny(fd);
        }

        private void ConnectToDestiny(string FileName)
        {
            conDestiny = Zamba.Servers.Server.get_Con(apDestiny.SERVERTYPE, apDestiny.SERVER, apDestiny.DB, apDestiny.USER, apDestiny.PASSWORD, true, false);

            var sbConnection = new StringBuilder();
            sbConnection.Append("Datos de Conexion Origen");
            sbConnection.Append("Servidor: ");
            sbConnection.AppendLine(apDestiny.SERVER);
            sbConnection.Append("Base de datos: ");
            sbConnection.AppendLine(apDestiny.DB);
            sbConnection.Append("Usuario: ");
            sbConnection.Append(apDestiny.USER);
            txtConnection2.Text = sbConnection.ToString();
            sbConnection = null;
            ZTrace.WriteLineIf(ZTrace.IsError, "Conectando con Origen: " + txtConnection2.Text);
            Zamba.Servers.Server.AppConfig = this.apDestiny;

            Zamba.Servers.Server.ConInitialized = false;
            Zamba.Servers.Server.ConInitializing = true;

            Zamba.Servers.Server _server = new Zamba.Servers.Server(FileName);
            if (Zamba.Servers.Server.ServerType != DBTYPES.SinDefinir)
            {
                _server.MakeConnection();
                _server.InitializeConnection(UserPreferences.getValue("DateConfig", UPSections.UserPreferences, "CONVERT(DATETIME, '{0}-{1}-{2} 00:00:00', 102)"), UserPreferences.getValue("DateTimeConfig", UPSections.UserPreferences, "CONVERT(DATETIME, '{0}-{1}-{2} {3}:{4}:{5}', 102)"));
                _server = null;
                Zamba.Servers.Server.ConInitializing = false;
                Zamba.Servers.Server.ConInitialized = true;
            }
        }

        private void zButton3_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessParsedXML(true);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

        }

        private void zButton4_Click(object sender, EventArgs e)
        {
            Boolean IsTest = this.chktest.Checked;
            StreamWriter SW = new StreamWriter(Path.Combine(temppath, "SQLOut" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + ".txt"));
            try
            {
                DataSet XmlDs = new DataSet("Valores de la Base de datos");
                String Query = null;
                Dictionary<Int32, string> Querys = new Dictionary<int, string>();
                Int32 i = 0;

                foreach (KeyValuePair<String, TableStructure> Item in _tablesDList)
                {
                    Query = "delete " + Item.Key;
                    if (!string.IsNullOrEmpty(Item.Value.filter))
                    {
                        Query += " WHERE " + Item.Value.filter;
                    }
                    Querys.Add(i, Query);
                    i++;
                }

                for (i = Querys.Count - 1; i >= 0; i--)
                {
                    try
                    {
                        this.listView1.Items.Add("Borrando Tabla " + Querys[i]);
                        Application.DoEvents();
                        SW.WriteLine(Querys[i] + ";");
                        if (IsTest == false) conDestiny.ExecuteNonQuery(CommandType.Text, Querys[i]);
                    }
                    catch (Exception ex)
                    {
                        this.listView1.Items.Add("Error al borrar Tabla " + Querys[i] + ex.Message);
                        ZClass.raiseerror(new Exception("Error al borrar Tabla " + Querys[i] + ex.ToString()));
                        Application.DoEvents();
                    }
                }

                MessageBox.Show("Proceso Terminado");


            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            finally
            {
                SW.Flush();
                SW.Close();
                SW.Dispose();
            }
        }

        public enum AnalizeResults
        {
            Update,
            ReCreateOrigin,
            ReCreateDestiny,
            Equals,
            DontInsert,
            KeepBoth,
            UpdateDestiny,
            UpdateSource,
            Insert,
            DeleteOrigin,
            DontDelete
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            LoadSourceConn();
            LoadDestConn();
            String OutName = "SQLOut" + conSource.CN.Database + " " + conDestiny.CN.Database + " - " + TablesXML.Replace(".xml", "") + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + ".txt";
            MessageBox.Show("Se guardara el resultado en :" + Path.Combine(temppath, OutName));
            StreamWriter SW = new StreamWriter(Path.Combine(temppath, OutName));

            if (conDestiny == null || conDestiny.CN == null)
                conDestiny = Zamba.Servers.Server.get_Con(apDestiny.SERVERTYPE, apDestiny.SERVER, apDestiny.DB, apDestiny.USER, apDestiny.PASSWORD, true, false);

            Transaction transaction = new Transaction(conDestiny);
            Boolean DontAskAgainpartialCommit = false;
            Boolean DontAskAgainForDeleteDestiny = false;

            Boolean IsTest = this.chktest.Checked;
            try
            {
                DataSet TempSourceDS = null;
                DataSet TempDestinyDs = null;
                String Query = null;

                prgBar.Value = 0;
                prgBar.Maximum = _tablesList.Count;

                this.RadGridView2.DataSource = _tablesList.Values;
                //this.RadGridView2.DataSource = null;
                //this.RadGridView2.DataSource = _tablesList.Values;


                DataSet BeforeDs = new DataSet();
                DataSet AfterDs = new DataSet();
                String SelectQuery;
                String InsertQuery;
                String UpdateQuery;


                StringBuilder QueryBuilderInsert;
                StringBuilder QueryBuilderSelect;
                StringBuilder QueryBuilderUpdate;


                DateTime TryValue;
                int i;
                object CurrentValue;
                string strCurrentValue;
                IDbDataParameter[] blobData = null;
                int blobCount = 0;
                List<int> blobPositions = new List<int>();

                string blobParam;


                try
                {
                    List<Int64> NewEntities = new List<Int64>();
                    List<NewIndex> NewIndexsInEntities = new List<NewIndex>();
                    List<NewIndex> NewIndexs = new List<NewIndex>();

                    if (resultsfile.Length > 0)
                    {
                        if (TempSourceDS == null)
                        {
                            TempSourceDS = new DataSet();
                        }
                        TempSourceDS.ReadXml(resultsfile);
                    }

                    foreach (KeyValuePair<String, TableStructure> Item in _tablesList.Reverse())
                    {
                        try
                        {

                            if (Item.Value.enabled)
                            {

                                if (IsTest == false) DisableConstrainsOfTable(conDestiny, Item.Key);

                                String IndentityDisabledString = GetDisableIdentityColumnsString(Item.Key);
                                String IndentityEnableString = GetReEnableIdentityColumnsString(Item.Key);

                                if (Item.Value.preserve == "Delete First")
                                {
                                    DialogResult answer = MessageBox.Show(string.Format("Se borraran los datos de la tabla: {0}, borrar?", Item.Key), "Borrado previo", MessageBoxButtons.YesNoCancel);
                                    if (answer == DialogResult.Yes)
                                    {
                                        SW.WriteLine("delete from " + Item.Key + ";");
                                        if (IsTest == false) conDestiny.ExecuteNonQuery(CommandType.Text, "delete from " + Item.Key);
                                    }
                                    if (answer == DialogResult.Cancel) return;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ZClass.raiseerror(ex);
                        }
                    }

                    foreach (KeyValuePair<String, TableStructure> Item in _tablesList)
                    {


                        if (Item.Value.enabled)
                        {
                            this.RadGridView2.CurrentRow = this.RadGridView2.Rows[prgBar.Value];
                            prgBar.Value += 1;


                            Query = "SELECT * FROM " + Item.Key;

                            if (!string.IsNullOrEmpty(Item.Value.filter.Trim()))
                            {
                                Query += " WHERE " + Item.Value.filter.Trim();
                            }

                            try
                            {
                                if (resultsfile.Length == 0)
                                {
                                    TempSourceDS = conSource.ExecuteDataset(CommandType.Text, Query);
                                    if (null != TempSourceDS && TempSourceDS.Tables.Count > 0 && TempSourceDS.Tables[0].Rows.Count > 0)
                                    {
                                        TempSourceDS.Tables[0].TableName = Item.Key;
                                    }
                                }



                                TempDestinyDs = conDestiny.ExecuteDataset(transaction.Transaction, CommandType.Text, Query);

                                if (null != TempSourceDS && TempSourceDS.Tables.Count > 0 && TempSourceDS.Tables[Item.Key] != null && TempSourceDS.Tables[Item.Key].Rows.Count > 0)
                                {
                                StartAgain:
                                    //Arreglo de Columnas diferentes entre sql y oracle C_
                                    if (conDestiny.isOracle == false && conSource.isOracle == true)
                                    {
                                        foreach (DataColumn currentColumn in TempSourceDS.Tables[Item.Key].Columns)
                                        {

                                            if (currentColumn.ColumnName.ToLower().StartsWith("c_"))
                                            {
                                                //ESTO ES UN PROBLEMA!
                                                var newColumnName = currentColumn.ColumnName.Substring(2, currentColumn.ColumnName.Length - 2);
                                                if (TempSourceDS.Tables[Item.Key].Columns.Contains(newColumnName))

                                                {
                                                    TempSourceDS.Tables[Item.Key].Columns.Remove(newColumnName);
                                                    currentColumn.ColumnName = newColumnName;
                                                    TempSourceDS.Tables[Item.Key].AcceptChanges();
                                                    goto StartAgain;
                                                }
                                                else
                                                {
                                                    currentColumn.ColumnName = newColumnName;

                                                }

                                            }
                                        }
                                    }


                                    TempSourceDS.Tables[Item.Key].Columns.Add("Compare", typeof(String));
                                    TempSourceDS.AcceptChanges();

                                    DataTable TempSourceDT = TempSourceDS.Tables[Item.Key];

                                    TempDestinyDs.Tables[0].TableName = Item.Key;
                                    TempDestinyDs.Tables[0].Columns.Add("Compare", typeof(String));
                                    TempDestinyDs.AcceptChanges();

                                    this.RadGridView3.DataSource = null;
                                    this.RadGridView3.DataSource = TempSourceDT;


                                    if (TempDestinyDs == null)
                                    {
                                        VerifyTable(conDestiny, TempSourceDT);
                                        GenerateDestinyTable(Item.Value);
                                    }


                                    InsertQuery = BuildTableQueryInsert(TempSourceDT);
                                    SelectQuery = BuildTableQuerySelect(TempSourceDT);
                                    UpdateQuery = BuildTableQueryUpdate(TempSourceDT, Item.Value);
                                    //DeleteQuery = BuildTableQueryDelete(CurrentTable);

                                    QueryBuilderInsert = new StringBuilder();
                                    QueryBuilderSelect = new StringBuilder();
                                    QueryBuilderUpdate = new StringBuilder();
                                    //QueryBuilderDelete = new StringBuilder();

                                    //Se verifica la existencia de archivos digitales (blobs)
                                    foreach (DataColumn col in TempSourceDT.Columns)
                                    {
                                        //Arreglo de Columnas diferentes entre sql y oracle C_
                                        if (conDestiny.isOracle == false && conSource.isOracle == true)
                                        {
                                            if (col.ColumnName.ToLower().StartsWith("c_"))
                                            {
                                                //ESTO ES UN PROBLEMA!
                                                col.ColumnName = col.ColumnName.Substring(2, col.ColumnName.Length - 2);
                                            }

                                            TempSourceDT.AcceptChanges();
                                        }


                                        if (col.DataType == typeof(Byte[]))
                                        {
                                            if (conDestiny.isOracle)
                                            {
                                                blobCount++;
                                                blobPositions.Add(col.Ordinal);
                                            }
                                            else
                                            {
                                                blobCount++;
                                                blobPositions.Add(col.Ordinal);
                                            }
                                        }
                                    }

                                    if (blobCount > 0)
                                    {
                                        blobData = new IDbDataParameter[blobCount];
                                        blobCount = 0;
                                    }


                                    this.progressBar1.Value = 0;
                                    progressBar1.Maximum = TempSourceDT.Rows.Count;

                                    foreach (DataRow r1 in TempSourceDT.Rows)
                                    {
                                        this.RadGridView3.CurrentRow = this.RadGridView3.Rows[progressBar1.Value];
                                        this.progressBar1.Value += 1;


                                        Boolean FoundOrigin = false;
                                        Boolean FoundDestiny = false;
                                        AnalizeResults AnalizeResult;

                                        //if (CheckEveryRow == true)
                                        //{
                                        //    MessageBox.Show(r1.ItemArray)
                                        //}

                                        if (TempDestinyDs == null)
                                        {
                                            BuildTraceInfo(r1, null, Item.Value);
                                            r1["Compare"] = "Insert";
                                            Item.Value.Inserts++;
                                        }
                                        else
                                        {
                                            string CompareUnique = CompareUniquesString(r1, Item.Value);
                                            CompareUnique = (CompareUnique.Length > 0) ? " or (" + CompareUnique + ")" : string.Empty;
                                            DataRow[] TempDestinyDT = TempDestinyDs.Tables[0].Select("(Compare is null or Compare = '' or Compare = 'FoundDestiny') and (( " + CompareKeysString(r1, Item.Value) + " ) " + CompareUnique + ")");

                                            this.RadGridView4.DataSource = null;
                                            //   if (TempDestinyDT.Length > 0)
                                            //         this.RadGridView4.DataSource = TempDestinyDT.CopyToDataTable();


                                            this.progressBar2.Value = 0;
                                            progressBar2.Maximum = TempDestinyDT.Length;

                                            AnalizeResult = AnalizeResults.DontInsert;
                                            if (TempDestinyDT.Length > 0)
                                            {
                                                foreach (DataRow r2 in TempDestinyDT)
                                                {
                                                    //         this.RadGridView4.CurrentRow = this.RadGridView4.Rows[progressBar2.Value];
                                                    this.progressBar2.Value += 1;


                                                    if (r2["Compare"].ToString() == string.Empty || r2["Compare"].ToString() == "FoundDestiny")
                                                    {
                                                        if (CompareKeys(r1, r2, Item.Value) || CompareUniques(r1, r2, Item.Value))
                                                        {
                                                            FoundOrigin = true;

                                                            AnalizeResult = AnalizeRow(r1, r2, Item.Value);

                                                            if (AnalizeResult == AnalizeResults.Update)
                                                            {
                                                                BuildTraceInfo(r1, null, Item.Value);
                                                                r1["Compare"] = "Update";
                                                                r2["Compare"] = "Update";
                                                                Item.Value.Updates++;
                                                            }
                                                            else if (AnalizeResult == AnalizeResults.Equals)
                                                            {
                                                                r1["Compare"] = "Equals";
                                                                r2["Compare"] = "Equals";
                                                                Item.Value.Equals++;
                                                                //  r1.Delete();
                                                                // r2.Delete();

                                                            }
                                                            else if (AnalizeResult == AnalizeResults.ReCreateOrigin)
                                                            {
                                                                BuildTraceInfo(r1, null, Item.Value);
                                                                r1["Compare"] = "ReCreateOrigin";
                                                                r2["Compare"] = "Keep";
                                                                Item.Value.ReCreateOrigin++;

                                                                GenerateNewIdOnOrigin(r1, r2, Item.Value);
                                                            }
                                                            else if (AnalizeResult == AnalizeResults.ReCreateDestiny)
                                                            {
                                                                BuildTraceInfo(r1, null, Item.Value);
                                                                r1["Compare"] = "Insert";
                                                                r2["Compare"] = "ReCreateDestiny";
                                                                Item.Value.ReCreateDestiny++;
                                                                GenerateNewIdOnDestiny(r1, r2, Item.Value);
                                                            }
                                                            else if (AnalizeResult == AnalizeResults.KeepBoth)
                                                            {
                                                                BuildTraceInfo(r1, null, Item.Value);
                                                                r1["Compare"] = "Insert";
                                                                r2["Compare"] = "Keep";
                                                                Item.Value.InsertNew++;
                                                            }

                                                            break;
                                                        }


                                                        if (FoundOrigin == false || r2["Compare"].ToString() == string.Empty)
                                                        {
                                                            foreach (DataRow r3 in TempSourceDT.Rows)
                                                            {
                                                                if (r3["Compare"].ToString() != string.Empty || CompareKeys(r3, r2, Item.Value))
                                                                {
                                                                    FoundDestiny = true;
                                                                    r2["Compare"] = "FoundDestiny";

                                                                    break;
                                                                }

                                                            }
                                                            if (FoundDestiny == false)
                                                            {
                                                                BuildTraceInfo(r1, null, Item.Value);
                                                                r2["Compare"] = "Only Destiny";
                                                                Item.Value.OnlyDestiny++;

                                                                AnalizeResults ConflictResult = AnalizeResults.KeepBoth;

                                                                if (DontAskAgainDestiny.Contains(Item.Key))
                                                                {
                                                                    ConflictResult = LastResultOnlyDestiny;
                                                                }
                                                                else
                                                                {
                                                                    if (checkBox1.Checked == true)
                                                                    {
                                                                        ConflictResult = AnalizeResults.DontInsert;
                                                                    }
                                                                    else
                                                                    {
                                                                        KeepDestinyOptions co = new KeepDestinyOptions(r2, Item.Value);
                                                                        if (co.ShowDialog() == DialogResult.OK)
                                                                        {
                                                                            if (co.DontAskAgain)
                                                                            {
                                                                                DontAskAgainDestiny.Add(Item.Key);
                                                                                LastResultOnlyDestiny = co.ConflictResult;
                                                                                ConflictResult = LastResultOnlyDestiny;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                        }
                                                                    }
                                                                }

                                                                if (ConflictResult == AnalizeResults.DeleteOrigin)
                                                                {
                                                                    string deletequery = BuildTableQueryDelete(r2, Item.Value);
                                                                    SW.WriteLine(deletequery + ";");
                                                                    if (IsTest == false) conDestiny.ExecuteNonQuery(CommandType.Text, deletequery);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }

                                            }
                                            else
                                            {
                                                BuildTraceInfo(r1, null, Item.Value);
                                                r1["Compare"] = "Insert";
                                                Item.Value.Inserts++;
                                            }
                                            TempSourceDT.AcceptChanges();
                                            TempDestinyDs.AcceptChanges();
                                            this.RadGridView2.MasterTemplate.Refresh(null);

                                            GridTableElement tableElement2 = this.RadGridView2.CurrentView as GridTableElement;
                                            GridViewRowInfo row2 = this.RadGridView2.CurrentRow;

                                            if (tableElement2 != null && row2 != null)
                                            {
                                                tableElement2.ScrollToRow(row2);
                                            }

                                            GridTableElement tableElement3 = this.RadGridView3.CurrentView as GridTableElement;
                                            GridViewRowInfo row3 = this.RadGridView3.CurrentRow;

                                            if (tableElement3 != null && row3 != null)
                                            {
                                                tableElement3.ScrollToRow(row3);
                                            }

                                            //GridTableElement tableElement4 = this.RadGridView4.CurrentView as GridTableElement;
                                            //GridViewRowInfo row4 = this.RadGridView4.CurrentRow;

                                            //if (tableElement4 != null && row4 != null)
                                            //{
                                            //    tableElement4.ScrollToRow(row4);
                                            //}

                                            //this.RadGridView2.DataSource = null;
                                            //this.RadGridView3.DataSource = null;
                                            //this.RadGridView3.DataSource = TempSourceDs.Tables[0];
                                            //this.RadGridView4.DataSource = TempDestinyDs.Tables[0];
                                            //this.RadGridView2.DataSource = null;
                                            //this.RadGridView2.DataSource = _tablesList.Values;
                                            Application.DoEvents();


                                            if (r1["Compare"].ToString() == "Insert")
                                            {

                                                QueryBuilderInsert.Remove(0, QueryBuilderInsert.Length);
                                                // QueryBuilderInsert.Append(IndentityDisabledString);
                                                QueryBuilderInsert.Append(InsertQuery);

                                                QueryBuilderSelect.Remove(0, QueryBuilderSelect.Length);
                                                QueryBuilderSelect.Append(SelectQuery);

                                                //QueryBuilderUpdate.Remove(0, QueryBuilderUpdate.Length);
                                                //QueryBuilderUpdate.Append(UpdateQuery);

                                                //QueryBuilderDelete.Remove(0, QueryBuilderDelete.Length);
                                                //QueryBuilderDelete.Append(DeleteQuery);

                                                for (i = 0; i < r1.ItemArray.Length; i++)
                                                {
                                                    String CurrentColumnName = r1.Table.Columns[i].ToString();
                                                    if (CurrentColumnName != "Script" && CurrentColumnName != "Compare")
                                                    {
                                                        CurrentValue = r1.ItemArray[i];
                                                        strCurrentValue = CurrentValue.ToString();

                                                        if (blobPositions.Contains(i))
                                                        {//Verifica si es un blob
                                                            blobParam = "@blob" + i.ToString();
                                                            QueryBuilderInsert.Append(blobParam);
                                                            SqlParameter pDocFile = default(SqlParameter);

                                                            //TODO: FALTA LA PARTE DE ORACLE
                                                            pDocFile = new SqlParameter(blobParam, SqlDbType.VarBinary, ((byte[])CurrentValue).Length, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, CurrentValue);

                                                            blobData[blobCount] = pDocFile;
                                                            blobCount++;
                                                        }
                                                        else if ((strCurrentValue.Contains(_guion) || strCurrentValue.Contains("/")) && strCurrentValue.Contains(_dosPuntos) && DateTime.TryParse(strCurrentValue, out TryValue))
                                                        {//Verifica si es una fecha
                                                            QueryBuilderInsert.Append(conDestiny.ConvertDateTime(TryValue));
                                                            QueryBuilderSelect.Replace("{" + i.ToString() + "}", conDestiny.ConvertDateTime(TryValue));
                                                            //      QueryBuilderUpdate.Append(conDestiny.ConvertDateTime(TryValue));
                                                            //QueryBuilderDelete.Append(conDestiny.ConvertDateTime(TryValue));
                                                        }
                                                        else if (chkUseBool.Checked && String.Compare(strCurrentValue, _true, true) == 0)
                                                        {//Verifica si es un booleano True
                                                            QueryBuilderInsert.Append(_uno);
                                                            QueryBuilderSelect.Replace("{" + i.ToString() + "}", _uno);
                                                            //QueryBuilderUpdate.Append(_uno);
                                                            //QueryBuilderDelete.Append(_uno);
                                                        }
                                                        else if (chkUseBool.Checked && String.Compare(strCurrentValue, _false, true) == 0)
                                                        {//Verifica si es un booleano False
                                                            QueryBuilderInsert.Append(_cero);
                                                            QueryBuilderSelect.Replace("{" + i.ToString() + "}", _cero);
                                                            //QueryBuilderUpdate.Append(_cero);
                                                            //QueryBuilderDelete.Append(_cero);
                                                        }
                                                        else if (CurrentValue is DBNull && conDestiny.isOracle == false)
                                                        {//Verifica si es nulo
                                                            QueryBuilderInsert.Append(_nulo);
                                                            QueryBuilderSelect.Replace("{" + i.ToString() + "}", _nulo);
                                                            //QueryBuilderUpdate.Append(_nulo);
                                                            //QueryBuilderDelete.Append(_nulo);
                                                        }
                                                        else
                                                        {//Se agrega el valor sin modificación
                                                            strCurrentValue = strCurrentValue.Replace(_comillaSimple, _comillaSimple2);
                                                            QueryBuilderInsert.Append(_comillaSimple);
                                                            QueryBuilderInsert.Append(strCurrentValue);
                                                            QueryBuilderInsert.Append(_comillaSimple);

                                                            // QueryBuilderSelect.Append(_comillaSimple);
                                                            if (strCurrentValue == string.Empty)
                                                            {
                                                                //quitar esta columna del select
                                                                QueryBuilderSelect.Replace(" = {" + i.ToString() + "}", " is null ");
                                                            }
                                                            else
                                                            {
                                                                QueryBuilderSelect.Replace("{" + i.ToString() + "}", _comillaSimple + strCurrentValue + _comillaSimple);
                                                            }//      QueryBuilderSelect.Append(_comillaSimple);

                                                            //QueryBuilderUpdate.Append(_comillaSimple);
                                                            //QueryBuilderUpdate.Append(strCurrentValue);
                                                            //QueryBuilderUpdate.Append(_comillaSimple);

                                                            //QueryBuilderDelete.Append(_comillaSimple);
                                                            //QueryBuilderDelete.Append(strCurrentValue);
                                                            //QueryBuilderDelete.Append(_comillaSimple);
                                                        }

                                                        QueryBuilderInsert.Append(_coma);
                                                        //                                QueryBuilderUpdate.Append(_coma);
                                                        //QueryBuilderDelete.Append(_coma);
                                                    }
                                                }

                                                //Se remueve la coma que queda
                                                QueryBuilderInsert.Remove(QueryBuilderInsert.Length - 1, 1);
                                                QueryBuilderInsert.Append(_cierraParentesis);
                                                // QueryBuilderInsert.Append(IndentityEnableString);

                                                // QueryBuilderSelect.Append(IndentityEnableString);

                                                //QueryBuilderUpdate.Remove(QueryBuilderInsert.Length - 1, 1);
                                                //QueryBuilderUpdate.Append(_cierraParentesis);
                                                //QueryBuilderUpdate.Append(IndentityEnableString);

                                                //QueryBuilderDelete.Remove(QueryBuilderInsert.Length - 1, 1);
                                                //QueryBuilderDelete.Append(_cierraParentesis);
                                                //QueryBuilderDelete.Append(IndentityEnableString);

                                                try
                                                {
                                                    if (blobCount > 0)
                                                    {
                                                        //  ZTrace.WriteLineIf(ZTrace.IsError, "Armo Query Insert Tabla con BLOB: " + TempSourceDT.TableName + " - " + QueryBuilderInsert.ToString());
                                                        this.listView1.Items.Add(QueryBuilderInsert.ToString());
                                                        Application.DoEvents();

                                                        SW.WriteLine(QueryBuilderInsert.ToString() + ";");
                                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, QueryBuilderInsert.ToString());
                                                        if (IsTest == false && chkInserts.Checked) conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, QueryBuilderInsert.ToString(), blobData);
                                                        blobCount = 0;
                                                    }
                                                    else
                                                    {
                                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, QueryBuilderSelect.ToString());
                                                        Int32 count = Int32.Parse(conDestiny.ExecuteScalar(transaction.Transaction, CommandType.Text, QueryBuilderSelect.ToString()).ToString());

                                                        if (count == 0)
                                                        {
                                                            //  ZTrace.WriteLineIf(ZTrace.IsError, "Armo Query Insert Tabla: " + TempSourceDT.TableName + " - " + QueryBuilderInsert.ToString());
                                                            this.listView1.Items.Add(QueryBuilderInsert.ToString());
                                                            this.listView1.Items[this.listView1.Items.Count - 1].EnsureVisible();
                                                            if (QueryBuilderInsert.ToString().Contains("Error"))
                                                                this.listView1.Items[this.listView1.Items.Count - 1].ForeColor = System.Drawing.Color.Red;
                                                            Application.DoEvents();
                                                            SW.WriteLine(QueryBuilderInsert.ToString() + ";");
                                                            ZTrace.WriteLineIf(ZTrace.IsVerbose, QueryBuilderInsert.ToString());

                                                            if (IsTest == false && chkInserts.Checked) conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, QueryBuilderInsert.ToString());

                                                            if (TempSourceDT.TableName == "DOC_TYPE")
                                                            {
                                                                NewEntities.Add(Int64.Parse(r1["DOC_TYPE_ID"].ToString()));
                                                            }

                                                            if (TempSourceDT.TableName == "INDEX_R_DOC_TYPE")
                                                            {
                                                                NewIndexsInEntities.Add(new NewIndex(Int64.Parse(r1["INDEX_ID"].ToString()), Int64.Parse(r1["DOC_TYPE_ID"].ToString())));
                                                            }

                                                            if (TempSourceDT.TableName == "DOC_INDEX")
                                                            {
                                                                try
                                                                {
                                                                    NewIndexs.Add(new NewIndex(Int64.Parse(r1["INDEX_ID"].ToString()), r1["Script"].ToString(), Int32.Parse(r1["Index_Len"].ToString()), (IndexDataType)Int32.Parse(r1["Index_Type"].ToString()), (IndexAdditionalType)Int32.Parse(r1["DropDown"].ToString())));
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    ZClass.raiseerror(ex);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    if (ex.Message.Contains("Primary") || ex.Message.Contains("unique"))
                                                    {
                                                        this.listView1.Items.Add(ex.Message + ": " + QueryBuilderInsert.ToString());
                                                        //                                    conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, QueryBuilderUpdate.ToString());
                                                    }
                                                    else if (ex.Message.Contains("parent key not found"))
                                                    {
                                                        this.listView1.Items.Add(ex.Message + ": " + QueryBuilderInsert.ToString());
                                                    }
                                                    else
                                                    {
                                                        ZClass.raiseerror(ex);
                                                        this.listView1.Items.Add(ex.Message);
                                                        Application.DoEvents();
                                                    }
                                                }

                                                QueryBuilderInsert.Remove(0, QueryBuilderInsert.Length);

                                            }
                                            else if (r1["Compare"].ToString() == "Update")
                                            {

                                                QueryBuilderUpdate.Remove(0, QueryBuilderUpdate.Length);
                                                QueryBuilderUpdate.Append(UpdateQuery);

                                                for (i = 0; i < r1.ItemArray.Length; i++)
                                                {
                                                    String CurrentColumnName = r1.Table.Columns[i].ToString();
                                                    if (CurrentColumnName != "Script" && CurrentColumnName != "Compare")
                                                    {
                                                        CurrentValue = r1.ItemArray[i];
                                                        strCurrentValue = CurrentValue.ToString();

                                                        if (blobPositions.Contains(i))
                                                        {//Verifica si es un blob
                                                            blobParam = "@blob" + i.ToString();
                                                            QueryBuilderUpdate.Append(blobParam);
                                                            SqlParameter pDocFile = default(SqlParameter);

                                                            //TODO: FALTA LA PARTE DE ORACLE
                                                            pDocFile = new SqlParameter(blobParam, SqlDbType.VarBinary, ((byte[])CurrentValue).Length, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, CurrentValue);

                                                            blobData[blobCount] = pDocFile;
                                                            blobCount++;
                                                        }
                                                        else if ((strCurrentValue.Contains(_guion) || strCurrentValue.Contains("/")) && strCurrentValue.Contains(_dosPuntos) && DateTime.TryParse(strCurrentValue, out TryValue))
                                                        {//Verifica si es una fecha
                                                            //QueryBuilderUpdate.Append(conDestiny.ConvertDateTime(TryValue));
                                                            QueryBuilderUpdate.Replace("{" + i.ToString() + "}", conDestiny.ConvertDateTime(TryValue));
                                                            //      QueryBuilderUpdate.Append(conDestiny.ConvertDateTime(TryValue));
                                                            //QueryBuilderDelete.Append(conDestiny.ConvertDateTime(TryValue));
                                                        }
                                                        else if (chkUseBool.Checked && String.Compare(strCurrentValue, _true, true) == 0)
                                                        {//Verifica si es un booleano True
                                                            //QueryBuilderUpdate.Append(_uno);
                                                            QueryBuilderUpdate.Replace("{" + i.ToString() + "}", _uno);
                                                            //QueryBuilderUpdate.Append(_uno);
                                                            //QueryBuilderDelete.Append(_uno);
                                                        }
                                                        else if (chkUseBool.Checked && String.Compare(strCurrentValue, _false, true) == 0)
                                                        {//Verifica si es un booleano False
                                                            //QueryBuilderUpdate.Append(_cero);
                                                            QueryBuilderUpdate.Replace("{" + i.ToString() + "}", _cero);
                                                            //QueryBuilderUpdate.Append(_cero);
                                                            //QueryBuilderDelete.Append(_cero);
                                                        }
                                                        else if (CurrentValue is DBNull && conDestiny.isOracle == false)
                                                        {//Verifica si es nulo
                                                            //QueryBuilderUpdate.Append(_nulo);
                                                            QueryBuilderUpdate.Replace("{" + i.ToString() + "}", _nulo);
                                                            //QueryBuilderUpdate.Append(_nulo);
                                                            //QueryBuilderDelete.Append(_nulo);
                                                        }
                                                        else
                                                        {//Se agrega el valor sin modificación
                                                            strCurrentValue = strCurrentValue.Replace(_comillaSimple, _comillaSimple2);
                                                            // QueryBuilderUpdate.Append(_comillaSimple);
                                                            //QueryBuilderUpdate.Append(strCurrentValue);
                                                            //QueryBuilderUpdate.Append(_comillaSimple);

                                                            // QueryBuilderSelect.Append(_comillaSimple);
                                                            if (strCurrentValue == string.Empty)
                                                            {
                                                                //quitar esta columna del select
                                                                QueryBuilderUpdate.Replace("{" + i.ToString() + "}", " null ");
                                                            }
                                                            else
                                                            {
                                                                QueryBuilderUpdate.Replace("{" + i.ToString() + "}", _comillaSimple + strCurrentValue + _comillaSimple);
                                                            }//      QueryBuilderSelect.Append(_comillaSimple);

                                                            //QueryBuilderUpdate.Append(_comillaSimple);
                                                            //QueryBuilderUpdate.Append(strCurrentValue);
                                                            //QueryBuilderUpdate.Append(_comillaSimple);

                                                            //QueryBuilderDelete.Append(_comillaSimple);
                                                            //QueryBuilderDelete.Append(strCurrentValue);
                                                            //QueryBuilderDelete.Append(_comillaSimple);
                                                        }

                                                        //QueryBuilderUpdate.Append(_coma);
                                                        //                                QueryBuilderUpdate.Append(_coma);
                                                        //QueryBuilderDelete.Append(_coma);
                                                    }
                                                }

                                                //Se remueve la coma que queda
                                                //QueryBuilderUpdate.Remove(QueryBuilderUpdate.Length - 1, 1);
                                                //QueryBuilderUpdate.Append(_cierraParentesis);
                                                // QueryBuilderUpdate.Append(IndentityEnableString);

                                                // QueryBuilderSelect.Append(IndentityEnableString);

                                                //QueryBuilderUpdate.Remove(QueryBuilderUpdate.Length - 1, 1);
                                                //QueryBuilderUpdate.Append(_cierraParentesis);
                                                //QueryBuilderUpdate.Append(IndentityEnableString);

                                                //QueryBuilderDelete.Remove(QueryBuilderUpdate.Length - 1, 1);
                                                //QueryBuilderDelete.Append(_cierraParentesis);
                                                //QueryBuilderDelete.Append(IndentityEnableString);

                                                try
                                                {
                                                    if (blobCount > 0)
                                                    {
                                                        // ZTrace.WriteLineIf(ZTrace.IsError, "Armo Query Insert Tabla con BLOB: " + TempSourceDT.TableName + " - " + QueryBuilderUpdate.ToString());
                                                        this.listView1.Items.Add(QueryBuilderUpdate.ToString());
                                                        Application.DoEvents();

                                                        SW.WriteLine(QueryBuilderUpdate.ToString() + ";");
                                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, QueryBuilderUpdate.ToString());
                                                        if (IsTest == false && chkUpdates.Checked) conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, QueryBuilderUpdate.ToString(), blobData);
                                                        blobCount = 0;
                                                    }
                                                    else
                                                    {

                                                        // ZTrace.WriteLineIf(ZTrace.IsError, "Armo Query Insert Tabla: " + TempSourceDT.TableName + " - " + QueryBuilderUpdate.ToString());
                                                        this.listView1.Items.Add(QueryBuilderUpdate.ToString());
                                                        this.listView1.Items[this.listView1.Items.Count - 1].EnsureVisible();
                                                        if (QueryBuilderUpdate.ToString().Contains("Error"))
                                                            this.listView1.Items[this.listView1.Items.Count - 1].ForeColor = System.Drawing.Color.Red;
                                                        Application.DoEvents();
                                                        SW.WriteLine(QueryBuilderUpdate.ToString() + ";");
                                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, QueryBuilderUpdate.ToString());
                                                        if (IsTest == false && chkUpdates.Checked) conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, QueryBuilderUpdate.ToString());

                                                        if (TempSourceDT.TableName == "DOC_TYPE")
                                                        {
                                                            //NewEntities.Add(Int64.Parse(r1["DOC_TYPE_ID"].ToString()));
                                                        }

                                                        if (TempSourceDT.TableName == "INDEX_R_DOC_TYPE")
                                                        {
                                                            //NewIndexsInEntities.Add(new NewIndex(Int64.Parse(r1["INDEX_ID"].ToString()), Int64.Parse(r1["DOC_TYPE_ID"].ToString())));
                                                        }

                                                        if (TempSourceDT.TableName == "DOC_INDEX")
                                                        {
                                                            //NewIndexs.Add(new NewIndex(Int64.Parse(r1["INDEX_ID"].ToString()), r1["Script"].ToString(), Int32.Parse(r1["Index_Len"].ToString()), (IndexDataType)Int32.Parse(r1["Index_Type"].ToString()), (IndexAdditionalType)Int32.Parse(r1["DropDown"].ToString())));
                                                        }

                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    if (ex.Message.Contains("Primary") || ex.Message.Contains("unique"))
                                                    {
                                                        this.listView1.Items.Add(ex.Message + ": " + QueryBuilderUpdate.ToString());
                                                        //                                    conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, QueryBuilderUpdate.ToString());
                                                    }
                                                    else if (ex.Message.Contains("parent key not found"))
                                                    {
                                                        this.listView1.Items.Add(ex.Message + ": " + QueryBuilderUpdate.ToString());
                                                    }
                                                    else
                                                    {
                                                        ZClass.raiseerror(ex);
                                                        this.listView1.Items.Add(ex.Message);
                                                        Application.DoEvents();
                                                    }
                                                }

                                                QueryBuilderUpdate.Remove(0, QueryBuilderUpdate.Length);

                                            }
                                            else
                                            {
                                                //proceso no definido
                                            }

                                            if (TempDestinyDT != null)
                                            {
                                                TempDestinyDT = null;
                                            }

                                        }
                                    }







                                    //------------------------------------------------------------------------------------------------------------------------------------
                                    //Procesamiento de Filas de Destino que no existen en Origen


                                    DataRow[] TempDestinyToAnalize = TempDestinyDs.Tables[0].Select("(Compare is null or Compare = '' or Compare = 'FoundDestiny')");

                                    if (DontAskAgainForDeleteDestiny == false && (CheckEveryRow == true && checkBox1.Checked == false))
                                    {
                                        foreach (DataRow r2 in TempDestinyToAnalize)
                                        {
                                            AnalizeResults AnalizeResult;

                                            string CompareUnique = CompareUniquesString(r2, Item.Value);
                                            CompareUnique = (CompareUnique.Length > 0) ? " or (" + CompareUnique + ")" : string.Empty;
                                            DataRow[] TempSourceDTCheck = TempSourceDS.Tables[0].Select("(( " + CompareKeysString(r2, Item.Value) + " ) " + CompareUnique + ")");


                                            AnalizeResult = AnalizeResults.DontInsert;
                                            if (TempSourceDTCheck.Length == 0)
                                            {
                                                AnalizeResult = AnalizeRowToDelete(r2, Item.Value);

                                                if (AnalizeResult == AnalizeResults.DeleteOrigin)
                                                {
                                                    r2["Compare"] = "Delete";
                                                    Item.Value.Deletes++;
                                                }

                                                TempDestinyDs.AcceptChanges();

                                                this.RadGridView2.MasterTemplate.Refresh(null);

                                                GridTableElement tableElement2 = this.RadGridView2.CurrentView as GridTableElement;
                                                GridViewRowInfo row2 = this.RadGridView2.CurrentRow;

                                                if (tableElement2 != null && row2 != null)
                                                {
                                                    tableElement2.ScrollToRow(row2);
                                                }

                                                GridTableElement tableElement3 = this.RadGridView3.CurrentView as GridTableElement;
                                                GridViewRowInfo row3 = this.RadGridView3.CurrentRow;

                                                if (tableElement3 != null && row3 != null)
                                                {
                                                    tableElement3.ScrollToRow(row3);
                                                }

                                                GridTableElement tableElement4 = this.RadGridView4.CurrentView as GridTableElement;
                                                GridViewRowInfo row4 = this.RadGridView4.CurrentRow;

                                                if (tableElement4 != null && row4 != null)
                                                {
                                                    tableElement4.ScrollToRow(row4);
                                                }

                                                Application.DoEvents();




                                                if (r2["Compare"].ToString() == "Delete")
                                                {
                                                    StringBuilder QueryBuilderDelete = new StringBuilder();
                                                    string DeleteQuery = BuildTableQueryDelete(r2, Item.Value);
                                                    QueryBuilderDelete.Append(DeleteQuery);

                                                    this.listView1.Items.Add(QueryBuilderDelete.ToString());
                                                    this.listView1.Items[this.listView1.Items.Count - 1].EnsureVisible();
                                                    if (QueryBuilderDelete.ToString().Contains("Error"))
                                                        this.listView1.Items[this.listView1.Items.Count - 1].ForeColor = System.Drawing.Color.Red;
                                                    Application.DoEvents();
                                                    SW.WriteLine(QueryBuilderDelete.ToString() + ";");
                                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, QueryBuilderDelete.ToString());
                                                    if (IsTest == false) conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, QueryBuilderDelete.ToString());

                                                }
                                            }
                                        }


                                    }






                                }
                            }
                            catch (Exception ex)
                            {
                                ZClass.raiseerror(ex);

                                if (ex.Message.Contains("table or view does not exist"))
                                {
                                    if (MessageBox.Show("La Tabla : " + Item.Key + " no existe, desea crearla?", "Tabla no existe", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                    {
                                        if (Item.Value.Script.Length > 0)
                                        {

                                        }
                                    }

                                }
                                //  MessageBox.Show("Se produjo un error al exportar la tabla: " + Item.Key);
                            }

                            if (IsTest == false)
                            {
                                if (DontAskAgainpartialCommit == false && checkBox1.Checked == false)
                                {
                                    DialogResult dr = MessageBox.Show("Desea aplicar commit para las tablas pendientes o hacerlo al final o cancelar el commit?", "Commit Parcial", MessageBoxButtons.YesNoCancel);
                                    if (dr == DialogResult.Yes)
                                    {
                                        transaction.Commit();
                                        transaction = new Transaction(conDestiny);

                                    }
                                    else if (dr == DialogResult.No)
                                    {
                                        DontAskAgainpartialCommit = true;
                                    }
                                    else
                                    {
                                        transaction.Rollback();
                                        transaction = new Transaction(conDestiny);
                                    }
                                }
                            }
                        }

                    }


                    try
                    {

                        if (IsTest)
                            transaction.Rollback();
                        else
                        {
                            transaction.Commit();

                            #region PostScripts



                            foreach (Int64 EntityId in NewEntities)
                            {
                                Zamba.Servers.Server.CreateTables().AddDocsTables(EntityId);
                            }

                            foreach (NewIndex NI in NewIndexsInEntities)
                            {
                                IIndex Index = Zamba.Core.IndexsBusiness.GetIndexById(NI.IndexId, string.Empty);
                                NI.IndexType = Index.Type;
                                NI.IndexLen = Index.Len;
                                if (!IndexsBusiness.getReferenceStatus(NI.DocTypeId, NI.IndexId))
                                {
                                    if (DocTypesBusiness.CheckColumn(NI.DocTypeId, NI.IndexId, NI.IndexType, NI.IndexLen) == false)
                                    {
                                        DocTypesBusiness.AddColumn(NI.DocTypeId, NI.IndexId, NI.IndexType, NI.IndexLen);
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Se agrega el indice Id: {0} en  DocTypeId: {1}.", NI.IndexId, NI.DocTypeId));
                                    }
                                    else
                                    {
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("El indice Id: {0} existe en la entidad DocTypeId: {1}.", NI.IndexId, NI.DocTypeId));
                                    }

                                }
                                else
                                {
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("El indice Id: {0} DocTypeId: {1} es referencial.", NI.IndexId, NI.DocTypeId));
                                }

                            }

                            foreach (NewIndex NI in NewIndexs)
                            {
                                if (NI.DropDown == IndexAdditionalType.AutoSustitución || NI.DropDown == IndexAdditionalType.AutoSustituciónJerarquico)
                                {
                                    if (NI.Script.Length > 0)
                                    {
                                        IndexsBusiness.addindexsust(NI.IndexId, NI.VistaoTabla, NI.ColumnaCodigo, NI.ColumnaDescripcion, NI.Script);
                                    }
                                    else
                                    {
                                        IndexsBusiness.createsustituciontable(NI.IndexId, NI.IndexLen, (IndexDataType)Enum.Parse(typeof(IndexDataType), NI.IndexType.ToString()));
                                    }
                                }
                                else if (NI.DropDown == IndexAdditionalType.DropDown || NI.DropDown == IndexAdditionalType.DropDownJerarquico)
                                {
                                    if (NI.Script.Length > 0)
                                    {
                                        IndexsBusiness.addindexlist(NI.IndexId, NI.VistaoTabla, NI.ColumnaCodigo, NI.ColumnaDescripcion, NI.Script);
                                    }
                                    else
                                    {
                                        IndexsBusiness.addindexlist(NI.IndexId, NI.IndexLen);
                                    }
                                }
                                else
                                {
                                }

                                //TODO: Falta agregar la relacion de las DOC_I a las listas
                            }



                            if (chkAfterScripts.Checked == true)
                            {
                                AfterDs.ReadXml(Application.StartupPath + "\\AfterScripts.xml");

                                foreach (DataRow dr in AfterDs.Tables[0].Rows)
                                {
                                    ZScript script = new ZScript(Int64.Parse(dr[0].ToString()), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
                                    SW.WriteLine(script.Consulta.ToString() + ";");
                                    if (IsTest == false) UpdaterBusiness.ExecuteQuery(conDestiny, script.Consulta);
                                }

                                AfterDs.Clear();
                                AfterDs.Dispose();
                                AfterDs = null;
                            }
                            #endregion

                        }

                    }
                    catch (Exception ex)
                    {
                        ZClass.raiseerror(ex);
                    }
                    MessageBox.Show("Proceso Finalizado");


                }
                catch (Exception ex)
                {
                    try
                    {
                        ZClass.raiseerror(ex);
                        transaction.Rollback();
                        this.listView1.Items.Add(ex.Message);
                        Application.DoEvents();
                    }
                    catch (Exception ex1)
                    {
                        ZClass.raiseerror(ex1);
                    }
                }
            }
            catch (Exception ex)
            {
                try
                {
                    ZClass.raiseerror(ex);
                    transaction.Rollback();
                    this.listView1.Items.Add(ex.Message);
                    Application.DoEvents();
                }
                catch (Exception ex2)
                {
                    ZClass.raiseerror(ex2);
                }
            }
            finally
            {
                if (transaction != null)
                    transaction.Dispose();

                SW.Flush();
                SW.Close();
                SW.Dispose();
            }
        }

        private string BuildTraceInfo(DataRow r1, DataRow r2, TableStructure value)
        {
            try
            {
                switch (value.name)
                {
                    case "WFWorkflow":
                        break;
                    case "Zwfopt":
                        break;
                    case "WFStep":
                        break;
                    case "WFStepOpt":
                        break;
                    case "WFStepStates":
                        break;
                    case "WF_CONF_OPT":
                        break;
                    case "WFCONTEXTS":
                        break;
                    case "WFTask_States":
                        break;
                    case "WFRules":
                        break;
                    case "WFRuleParamItems":
                        break;
                    case "ZRuleOptBase":
                        break;
                    case "DISK_GROUP":
                        break;
                    case "DISK_VOLUME":
                        break;
                    case "DISK_GROUP_R_DISK_VOLUME":
                        break;
                    case "USRTABLE":
                        break;
                    case "ZUSERCONFIG":
                        //r1
                        string infoSelect1 = "/* ORIGEN: " + string.Format("select '--Usuario-Grupo: ' || g.ID || ' ' || g.name || ' Item: ' || o.c_name || ' valor: ' || o.c_value from zuserconfig o inner join zuser_or_group g on g.id = o.userid where o.userid = {0} and lower(o.c_name) = '{1}' ", r1["c_userid"], r1["c_name"]);
                        if (conSource.isOracle == false)
                        {
                            infoSelect1 = infoSelect1.Replace("c_", "");
                            infoSelect1 = infoSelect1.Replace("lower", "to_lower");
                            infoSelect1 = infoSelect1.Replace("||", "+");
                        }
                        string info1 = conSource.ExecuteScalar(CommandType.Text, infoSelect1).ToString();

                        info1 = info1 + " */";
                        //r2
                        string info2 = " /*";
                        if (r2 != null)
                        {
                            string infoSelect2 = string.Format("select '--Usuario-Grupo: ' || g.ID || ' ' || g.name || ' Item: ' || o.c_name || ' valor: ' || o.c_value from zuserconfig o inner join zuser_or_group g on g.id = o.userid where o.userid = {0} and lower(o.c_name) = '{1}' ", r2["c_userid"], r2["c_name"]);
                            if (conSource.isOracle == false)
                            {
                                infoSelect2 = infoSelect1.Replace("c_", "");
                                infoSelect2 = infoSelect1.Replace("lower", "to_lower");
                                infoSelect2 = infoSelect1.Replace("||", "+");
                            }
                            info2 = conDestiny.ExecuteScalar(CommandType.Text, infoSelect2).ToString();
                        }
                        return info1 + info2 + " */";
                    case "USRGROUP":
                        break;
                    case "group_r_group":
                        break;
                    case "USR_R_GROUP":
                        break;
                    case "DOC_TYPE_GROUP":
                        break;
                    case "DOC_TYPE":
                        break;
                    case "DOC_TYPE_R_DOC_TYPE_GROUP":
                        break;
                    case "DOC_TYPE_R_DOC_TYPE":
                        break;
                    case "DOCTYPES_ASSOCIATED":
                        break;
                    case "DOC_RESTRICTIONS":
                        break;
                    case "DOC_RESTRICTION_R_USER":
                        break;
                    case "DOC_INDEX":
                        break;
                    case "INDEX_R_DOC_TYPE":
                        break;
                    case "USR_RIGHTS":
                        break;
                    case "ZVIR":
                        break;
                    case "WFIndexAndVariableConfig":
                        break;
                    case "WFIndexAndVariable_r_WFRules":
                        break;
                    case "WFIndexAndVariable":
                        break;
                    case "WF_DT":
                        break;
                    case "ZFrms":
                        break;
                    case "zFrmDynamicForms":
                        break;
                    case "ZFRMSBLOB":
                        break;
                    case "ZFrms_Conditions":
                        break;
                    case "ZFRMSREQUIREMENTS":
                        break;
                    case "zfrms_AdditionalFiles":
                        break;
                    case "ZFrms_Actions":
                        break;
                    case "ZFrmIndexsDesc":
                        break;
                    case "ZFrmDesc":
                        break;
                    case "WF_Frms":
                        break;
                    case "Ztype_Zfrms":
                        break;
                    case "Zext_zfrms":
                        break;
                    case "USR_R_FORM":
                        break;
                    case "ZSecOption":
                        break;
                    case "ZSecciones":
                        break;
                    case "ZTempl":
                        break;
                    case "ZFILTERS":
                        break;
                    case "ZBUTTONSPLACE":
                        break;
                    case "ZBUTTONSTYPE":
                        break;
                    case "ZButtons":
                        break;
                    case "ZBUTTONSANDGROUPS":
                        break;
                    case "ZBarcodeComplete":
                        break;
                    case "ReportBuilder":
                        break;
                    case "Reporte_General":
                        break;
                    case "ReportDecodeValues":
                        break;
                    case "reportbuilder_linkOption":
                        break;
                    case "ReportBuilder_ChartOption":
                        break;
                    case "OUT_MAP_FOLDER":
                        break;
                    case "OUT_MAP_DOC_TYPE":
                        break;
                    case "OUT_MAP_ATTACH_IDS":
                        break;
                    case "NOTESINDEX_R_DOC_INDEX":
                        break;
                    case "IPJOB_LIST":
                        break;
                    case "IP_TGRP":
                        break;
                    case "IP_TYPE":
                        break;
                    case "IPJOB_R_IPJOB_LIST":
                        break;
                    case "IP_FOLDER":
                        break;
                    case "IP_PREPROCESS":
                        break;
                    case "IP_INDEX":
                        break;
                    case "IP_FOLDERCONF":
                        break;
                    case "IP_FOLDERBACKUP":
                        break;
                    case "IP_FOLDER_PREPROCESS":
                        break;
                }


                return string.Empty;
            }
            catch (Exception ex)
            {

                ZClass.raiseerror(ex);
                return string.Empty;
            }
        }

        private void ProcessDOCBList(KeyValuePair<string, TableStructure> item)
        {
            throw new NotImplementedException();
        }

        private void ProcessDOCIList(KeyValuePair<string, TableStructure> item)
        {
            throw new NotImplementedException();
        }

        private void ProcessDOCTList(KeyValuePair<string, TableStructure> item)
        {
            throw new NotImplementedException();
        }

        private void ProcessSearchList(KeyValuePair<string, TableStructure> item)
        {
            throw new NotImplementedException();
        }

        private void ProcessSustList(KeyValuePair<string, TableStructure> item)
        {
            throw new NotImplementedException();
        }

        private bool CompareKeysAndUniques(DataRow r1, DataRow r2, TableStructure Value)
        {
            Boolean IsEquals = true;
            try
            {
                if (Value.keys.Length > 0)
                {
                    foreach (string key in Value.keys.Split(char.Parse(",")))
                    {
                        if (r1[key.Trim()].ToString() != r2[key.Trim()].ToString())
                        {
                            IsEquals = false;
                        }

                    }
                }
                if (Value.uniques.Length > 0)
                {
                    foreach (string unique in Value.uniques.Split(char.Parse(",")))
                    {
                        if (Value.ignorecompare == null || Value.ignorecompare.Contains(unique) == false)
                        {
                            if (r1[unique.Trim()].ToString() != r2[unique.Trim()].ToString())
                            {
                                IsEquals = false;
                            }
                        }
                    }
                }
                if (Value.uniques.Length == 0 && Value.keys.Length == 0)
                { return false; }

                return IsEquals;
            }
            catch (Exception)
            {

                throw;
            }

        }


        private string CompareKeysString(DataRow r1, TableStructure Value)
        {
            StringBuilder SB = new StringBuilder();
            try
            {
                foreach (string key in Value.keys.Split(char.Parse(",")))
                {
                    if (key.Trim().Length > 0)
                    {
                        SB.Append(key);
                        SB.Append(" = ");
                        if (r1.Table.Columns[key.Trim()].DataType.Name == "String")
                        {
                            SB.Append("'");
                        }
                        SB.Append(r1[key.Trim()].ToString());
                        if (r1.Table.Columns[key.Trim()].DataType.Name == "String")
                        {
                            SB.Append("'");
                        }
                        SB.Append(" and ");
                    }
                }
                if (SB.Length > 3)
                {
                    SB.Remove(SB.Length - 4, 4);
                    return SB.ToString();
                }
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        private string CompareUniquesString(DataRow r1, TableStructure Value)
        {
            StringBuilder SB = new StringBuilder();
            try
            {
                foreach (string unique in Value.uniques.Split(char.Parse(",")))
                {
                    if (unique.Trim().Length > 0)
                    {
                        if (Value.ignorecompare == null || Value.ignorecompare.Contains(unique) == false)
                        {
                            SB.Append(unique);
                            SB.Append(" = ");
                            if (r1.Table.Columns[unique.Trim()].DataType.Name == "String")
                            {
                                SB.Append("'");
                            }
                            SB.Append(r1[unique.Trim()].ToString());
                            if (r1.Table.Columns[unique.Trim()].DataType.Name == "String")
                            {
                                SB.Append("'");
                            }
                            SB.Append(" and ");
                        }
                    }
                }
                if (SB.Length > 3)
                {
                    SB.Remove(SB.Length - 4, 4);
                    return SB.ToString();
                }
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        private bool CompareKeys(DataRow r1, DataRow r2, TableStructure Value)
        {
            Boolean IsEquals = true;
            try
            {
                if (Value.keys.Length > 0)
                {
                    foreach (string key in Value.keys.Split(char.Parse(",")))
                    {
                        if (r1[key.Trim()].ToString() != r2[key.Trim()].ToString())
                        {
                            IsEquals = false;
                            break;
                        }

                    }
                }
                else
                {
                    return false;
                }

                return IsEquals;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        private bool CompareAllColumns(DataRow r1, DataRow r2, TableStructure Value)
        {
            Boolean IsEquals = true;
            try
            {
                foreach (DataColumn column in r1.Table.Columns)
                {

                    if (column.ColumnName != "Script" && column.ColumnName != "Compare")
                    {
                        if (Value.ignorecompare == null || Value.ignorecompare.Contains(column.ColumnName) == false)
                        {
                            if (r2.Table.Columns.Contains(column.ColumnName))
                            {
                                if (r1[column.ColumnName].ToString() != r2[column.ColumnName].ToString())
                                {
                                    IsEquals = false;
                                }
                            }
                            else
                            {
                                //ver que hacemos con la columna faltante
                                IsEquals = false;

                            }
                        }
                    }
                }

                foreach (DataColumn column in r2.Table.Columns)
                {
                    if (column.ColumnName != "Script" && column.ColumnName != "Compare")
                    {
                        if (Value.ignorecompare == null || Value.ignorecompare.Contains(column.ColumnName) == false)
                        {
                            if (r1.Table.Columns.Contains(column.ColumnName))
                            {
                                if (r1[column.ColumnName].ToString() != r2[column.ColumnName].ToString())
                                {
                                    IsEquals = false;
                                }
                            }
                            else
                            {
                                //ver que hacemos con la columna faltante
                                IsEquals = false;
                            }
                        }
                    }
                }
                return IsEquals;
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        private bool CompareUniques(DataRow r1, DataRow r2, TableStructure Value)
        {
            Boolean IsEquals = true;
            try
            {
                if (Value.uniques.Length > 0)
                {
                    foreach (string unique in Value.uniques.Split(char.Parse(",")))
                    {
                        if (Value.ignorecompare == null || Value.ignorecompare.Contains(unique) == false)
                        {
                            if (r1[unique.Trim()].ToString() != r2[unique.Trim()].ToString())
                            {
                                IsEquals = false;
                            }
                        }
                    }
                }
                else
                {
                    return false;
                }
                return IsEquals;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        private void GenerateDestinyTable(TableStructure value)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void GenerateNewIdOnDestiny(DataRow r1, DataRow r2, TableStructure value)
        {
            switch (value.name)
            {
                case "WFWorkflow":

                    break;
                case "Zwfopt":
                    break;
                case "WFStep":
                    break;
                case "WFStepOpt":
                    break;
                case "WFStepStates":
                    break;
                case "WF_CONF_OPT":
                    break;
                case "WFCONTEXTS":
                    break;
                case "WFTask_States":
                    break;
                case "WFRules":
                    break;
                case "WFRuleParamItems":
                    break;
                case "ZRuleOptBase":
                    break;
                case "DISK_GROUP":
                    break;
                case "DISK_VOLUME":
                    break;
                case "DISK_GROUP_R_DISK_VOLUME":
                    break;
                case "USRTABLE":
                    break;
                case "USRGROUP":
                    break;
                case "group_r_group":
                    break;
                case "USR_R_GROUP":
                    break;
                case "DOC_TYPE_GROUP":
                    break;
                case "DOC_TYPE":
                    break;
                case "DOC_TYPE_R_DOC_TYPE_GROUP":
                    break;
                case "DOC_TYPE_R_DOC_TYPE":
                    break;
                case "DOCTYPES_ASSOCIATED":
                    break;
                case "DOC_RESTRICTIONS":
                    break;
                case "DOC_RESTRICTION_R_USER":
                    break;
                case "DOC_INDEX":
                    break;
                case "INDEX_R_DOC_TYPE":
                    break;
                case "USR_RIGHTS":
                    break;
                case "ZVIR":
                    break;
                case "WFIndexAndVariableConfig":
                    break;
                case "WFIndexAndVariable_r_WFRules":
                    break;
                case "WFIndexAndVariable":
                    break;
                case "WF_DT":
                    break;
                case "ZFrms":
                    break;
                case "zFrmDynamicForms":
                    break;
                case "ZFRMSBLOB":
                    break;
                case "ZFrms_Conditions":
                    break;
                case "ZFRMSREQUIREMENTS":
                    break;
                case "zfrms_AdditionalFiles":
                    break;
                case "ZFrms_Actions":
                    break;
                case "ZFrmIndexsDesc":
                    break;
                case "ZFrmDesc":
                    break;
                case "WF_Frms":
                    break;
                case "Ztype_Zfrms":
                    break;
                case "Zext_zfrms":
                    break;
                case "USR_R_FORM":
                    break;
                case "ZSecOption":
                    break;
                case "ZSecciones":
                    break;
                case "ZTempl":
                    break;
                case "ZFILTERS":
                    break;
                case "ZBUTTONSPLACE":
                    break;
                case "ZBUTTONSTYPE":
                    break;
                case "ZButtons":
                    break;
                case "ZBUTTONSANDGROUPS":
                    break;
                case "ZBarcodeComplete":
                    break;
                case "ReportBuilder":
                    break;
                case "Reporte_General":
                    break;
                case "ReportDecodeValues":
                    break;
                case "reportbuilder_linkOption":
                    break;
                case "ReportBuilder_ChartOption":
                    break;
                case "OUT_MAP_FOLDER":
                    break;
                case "OUT_MAP_DOC_TYPE":
                    break;
                case "OUT_MAP_ATTACH_IDS":
                    break;
                case "NOTESINDEX_R_DOC_INDEX":
                    break;
                case "IPJOB_LIST":
                    break;
                case "IP_TGRP":
                    break;
                case "IP_TYPE":
                    break;
                case "IPJOB_R_IPJOB_LIST":
                    break;
                case "IP_FOLDER":
                    break;
                case "IP_PREPROCESS":
                    break;
                case "IP_INDEX":
                    break;
                case "IP_FOLDERCONF":
                    break;
                case "IP_FOLDERBACKUP":
                    break;
                case "IP_FOLDER_PREPROCESS":
                    break;
            }
        }

        private void GenerateNewIdOnOrigin(DataRow r1, DataRow r2, TableStructure value)
        {
            switch (value.name)
            {

                case "WFWorkflow":
                    Int64 NewId = CoreBusiness.GetNewID(IdTypes.WF);
                    string query1 = string.Format("update wfworkflow set work_id = {0} where work_id = {1}", NewId, r1["Work_Id"].ToString());
                    string query2 = string.Format("update wfstep set work_id = {0} where work_id = {1}", NewId, r1["Work_Id"].ToString());
                    string query3 = string.Format("update wfdocument set work_id = {0} where work_id = {1}", NewId, r1["Work_Id"].ToString());
                    conSource.ExecuteNonQuery(CommandType.Text, query1);
                    conSource.ExecuteNonQuery(CommandType.Text, query2);
                    conSource.ExecuteNonQuery(CommandType.Text, query3);
                    r1["Work_Id"] = NewId;
                    r1.Table.AcceptChanges();
                    break;
                case "Zwfopt":
                    break;
                case "WFStep":
                    Int64 NewId2 = CoreBusiness.GetNewID(IdTypes.WFSTEP);
                    string query21 = string.Format("update WFSTEP set step_id = {0} where step_id = {1}", NewId2, r1["step_id"].ToString());
                    string query22 = string.Format("update WFRULES set step_id = {0} where step_id = {1}", NewId2, r1["step_id"].ToString());
                    string query23 = string.Format("update WFRULESHST set step_id = {0} where step_id = {1}", NewId2, r1["step_id"].ToString());
                    string query24 = string.Format("update WF_FRMS set step_id = {0} where step_id = {1}", NewId2, r1["step_id"].ToString());
                    string query25 = string.Format("update WFDOCUMENT set step_id = {0} where step_id = {1}", NewId2, r1["step_id"].ToString());
                    string query26 = string.Format("update WFSTEPSTATES set step_id = {0} where step_id = {1}", NewId2, r1["step_id"].ToString());
                    string query27 = string.Format("update wfruleparamitems set c_value  = {0} where rule_id in (select rule_id from wfrules r inner join wfruleparamitems p on r.id = p.rule_id where c_value = '{1}' and class = 'DoDistribuir' and item = 0) and item = 0", NewId2, r1["step_id"].ToString());
                    string query28 = string.Format("update wfstepopt set stepid = {0} where stepid = {1}", NewId2, r1["step_id"].ToString());
                    conSource.ExecuteNonQuery(CommandType.Text, query21);
                    conSource.ExecuteNonQuery(CommandType.Text, query22);
                    conSource.ExecuteNonQuery(CommandType.Text, query23);
                    conSource.ExecuteNonQuery(CommandType.Text, query24);
                    conSource.ExecuteNonQuery(CommandType.Text, query25);
                    conSource.ExecuteNonQuery(CommandType.Text, query26);
                    conSource.ExecuteNonQuery(CommandType.Text, query27);
                    conSource.ExecuteNonQuery(CommandType.Text, query28);
                    r1["step_id"] = NewId2;
                    r1.Table.AcceptChanges();
                    break;
                case "WFStepOpt":
                    break;
                case "WFStepStates":
                    break;
                case "WF_CONF_OPT":
                    break;
                case "WFCONTEXTS":
                    break;
                case "WFTask_States":
                    break;
                case "WFRules":
                    break;
                case "WFRuleParamItems":
                    break;
                case "ZRuleOptBase":
                    break;
                case "DISK_GROUP":
                    break;
                case "DISK_VOLUME":
                    break;
                case "DISK_GROUP_R_DISK_VOLUME":
                    break;
                case "USRTABLE":
                    break;
                case "USRGROUP":
                    Zamba.Core.IUserGroup UG = UserGroupBusiness.GetNewGroup(r1["Name"].ToString());
                    //foreach (string Dependency in value.dependencies.Split(char.Parse(",")))
                    //{
                    //    switch (Dependency)
                    //    {
                    //        case "":
                    //        break;

                    //    }
                    //}

                    conSource.ExecuteNonQuery(CommandType.Text, string.Format("update user_r_user_group set user_group_id = {0} where user_group_id = {1}", UG.ID, r1["ID"].ToString()));

                    break;
                case "group_r_group":
                    break;
                case "USR_R_GROUP":
                    break;
                case "DOC_TYPE_GROUP":
                    break;
                case "DOC_TYPE":
                    break;
                case "DOC_TYPE_R_DOC_TYPE_GROUP":
                    break;
                case "DOC_TYPE_R_DOC_TYPE":
                    break;
                case "DOCTYPES_ASSOCIATED":
                    break;
                case "DOC_RESTRICTIONS":
                    break;
                case "DOC_RESTRICTION_R_USER":
                    break;
                case "DOC_INDEX":
                    //mismo id, diferente nombre
                    Int64 NewIdDI = CoreBusiness.GetNewID(IdTypes.DOCINDEXID);
                    string queryDI1 = string.Format("update doc_index set index_id ={0} where index_id = {1}", NewIdDI, r1["index_id"].ToString());
                    string queryDI2 = string.Format("update index_R_DOC_TYPE set index_id ={0} where index_id = {1}", NewIdDI, r1["index_id"].ToString());
                    string queryDI3 = string.Format("update ZIR set indexid ={0} where indexid = {1}", NewIdDI, r1["index_id"].ToString());
                    string queryDI4 = string.Format("update wfruleparamitems set c_value = replace (c_value,''//{1}'',''//{0}'') where  class = ''DOGenerateTaskResult'' and item =1 and c_value like ''%//{1}%''", NewIdDI, r1["index_id"].ToString());
                    string queryDI5 = string.Format("update wfruleparamitems set c_value = replace (c_value,''{1}|'',''{0}|'') where  class = ''IfIndex'' and item =0 and c_value like ''%{1}|%''", NewIdDI, r1["index_id"].ToString());
                    string queryDI6 = string.Format("update wfruleparamitems set c_value = replace (c_value,''{1}|'',''{0}|'') where  class = ''DoFillIndex'' and item =0 and c_value like ''%{1}|%''", NewIdDI, r1["index_id"].ToString());
                    string queryDI7 = string.Format("update wfruleparamitems set c_value = replace (c_value,''{1}'',''{0}'') where  class = ''IfIndex'' and item =0 and c_value = ''{1}''", NewIdDI, r1["index_id"].ToString());
                    string queryDI8 = string.Format("update wfruleparamitems set c_value = replace (c_value,''{1}'',''{0}'') where  class = ''DoFillIndex'' and item =0 and c_value = ''{1}''", NewIdDI, r1["index_id"].ToString());

                    conSource.ExecuteNonQuery(CommandType.Text, queryDI1);
                    conSource.ExecuteNonQuery(CommandType.Text, queryDI2);
                    conSource.ExecuteNonQuery(CommandType.Text, queryDI3);
                    conSource.ExecuteNonQuery(CommandType.Text, queryDI4);
                    conSource.ExecuteNonQuery(CommandType.Text, queryDI5);
                    conSource.ExecuteNonQuery(CommandType.Text, queryDI6);
                    conSource.ExecuteNonQuery(CommandType.Text, queryDI7);
                    conSource.ExecuteNonQuery(CommandType.Text, queryDI8);
                    r1["index_id"] = NewIdDI;
                    r1.Table.AcceptChanges();
                    break;
                case "INDEX_R_DOC_TYPE":
                    break;
                case "USR_RIGHTS":
                    break;
                case "ZVIR":
                    break;
                case "WFIndexAndVariableConfig":
                    break;
                case "WFIndexAndVariable_r_WFRules":
                    break;
                case "WFIndexAndVariable":
                    break;
                case "WF_DT":
                    break;
                case "ZFrms":
                    break;
                case "zFrmDynamicForms":
                    break;
                case "ZFRMSBLOB":
                    break;
                case "ZFrms_Conditions":
                    break;
                case "ZFRMSREQUIREMENTS":
                    break;
                case "zfrms_AdditionalFiles":
                    break;
                case "ZFrms_Actions":
                    break;
                case "ZFrmIndexsDesc":
                    break;
                case "ZFrmDesc":
                    break;
                case "Ztype_Zfrms":
                    break;
                case "USR_R_FORM":
                    break;
                case "ZSecOption":
                    break;
                case "ZSecciones":
                    break;
                case "ZTempl":
                    break;
                case "ZFILTERS":
                    break;
                case "ZBUTTONSPLACE":
                    break;
                case "ZBUTTONSTYPE":
                    break;
                case "ZButtons":
                    break;
                case "ZBUTTONSANDGROUPS":
                    break;
                case "ZBarcodeComplete":
                    break;
                case "ReportBuilder":
                    break;
                case "Reporte_General":
                    break;
                case "ReportDecodeValues":
                    break;
                case "reportbuilder_linkOption":
                    break;
                case "ReportBuilder_ChartOption":
                    break;
                case "OUT_MAP_FOLDER":
                    break;
                case "OUT_MAP_DOC_TYPE":
                    break;
                case "OUT_MAP_ATTACH_IDS":
                    break;
                case "NOTESINDEX_R_DOC_INDEX":
                    break;
                case "IPJOB_LIST":
                    break;
                case "IP_TGRP":
                    break;
                case "IP_TYPE":
                    break;
                case "IPJOB_R_IPJOB_LIST":
                    break;
                case "IP_FOLDER":
                    break;
                case "IP_PREPROCESS":
                    break;
                case "IP_INDEX":
                    break;
                case "IP_FOLDERCONF":
                    break;
                case "IP_FOLDERBACKUP":
                    break;
                case "IP_FOLDER_PREPROCESS":
                    break;
            }
        }

        public List<String> DontAskAgain { get; private set; } = new List<string>();
        public AnalizeResults LastResult { get; private set; }
        public List<String> DontAskAgainDestiny { get; private set; } = new List<string>();
        public List<String> DontAskAgainToDelete { get; private set; } = new List<string>();
        public AnalizeResults LastResultOnlyDestiny { get; private set; }
        public bool CheckEveryRow { get; private set; }

        private AnalizeResults AnalizeRow(DataRow r1, DataRow r2, TableStructure value)
        {
            if (CompareKeys(r1, r2, value))
            {
                //La clave es la misma
                if (value.uniques.Length == 0 || CompareUniques(r1, r2, value))
                {
                    if (CompareAllColumns(r1, r2, value))
                    {
                        //faltaria validar todos los campos para ver si es equals y entonces no se intenta insertar ni update
                        return AnalizeResults.Equals;
                    }
                    else
                    {
                        //La clave es la misma y el unique es el mismo
                        return AnalizeResults.Update;
                    }
                }
                else
                {
                    //La clave es la misma, pero no se corresponde al mismo item o el mismo cambio de nombre.
                    if (checkBox1.Checked == true) return AnalizeResults.DontInsert;

                    if (DontAskAgain.Contains(value.name)) return LastResult;

                    CompareOptions co = new CompareOptions(r1, r2, value);
                    if (co.ShowDialog() == DialogResult.OK)
                    {
                        if (co.DontAskAgain)
                        {
                            DontAskAgain.Add(value.name);
                            LastResult = co.ConflictResult;
                        }
                        return co.ConflictResult;
                    }
                    else
                    {
                        //ver que hacer si cancela la decision
                        return AnalizeResults.DontInsert;
                    }
                }
            }
            else
            {

                //La clave NO es la misma
                if (value.uniques != null && value.uniques != string.Empty && CompareUniques(r1, r2, value))
                {
                    if (DontAskAgain.Contains(value.name)) return LastResult;

                    if (checkBox1.Checked == true) return AnalizeResults.DontInsert;

                    //La clave NO es la misma y el unique es el mismo
                    CompareOptions co = new CompareOptions(r1, r2, value);
                    if (co.ShowDialog() == DialogResult.OK)
                    {
                        if (co.DontAskAgain)
                        {
                            DontAskAgain.Add(value.name);
                            LastResult = co.ConflictResult;
                        }
                        return co.ConflictResult;
                    }
                    else
                    {
                        //ver que hacer si cancela la decision
                        return AnalizeResults.DontInsert;
                    }
                }
                else
                {
                    //La clave NO es la misma, pero no se corresponde al mismo item o el mismo cambio de nombre.
                    //Esto seria un error
                    return AnalizeResults.DontInsert;
                }
            }

        }


        private AnalizeResults AnalizeRowToDelete(DataRow r, TableStructure value)
        {
            //La clave es la misma, pero no se corresponde al mismo item o el mismo cambio de nombre.

            if (DontAskAgainToDelete.Contains(value.name)) return LastResult;

            DeleteOptions co = new DeleteOptions(r, value);
            if (co.ShowDialog() == DialogResult.OK)
            {
                if (co.DontAskAgain)
                {
                    DontAskAgainToDelete.Add(value.name);
                    LastResult = co.ConflictResult;
                }
                return co.ConflictResult;
            }
            else
            {
                //ver que hacer si cancela la decision
                if (co.DontAskAgain)
                {
                    DontAskAgainToDelete.Add(value.name);
                    LastResult = co.ConflictResult;
                }
                return AnalizeResults.DontDelete;
            }

        }

        DataSet dsentities = null;

        private void zButton5_Click_1(object sender, EventArgs e)
        {
            try
            {
                dsentities = conSource.ExecuteDataset(CommandType.Text, "Select doc_type_name, doc_type_id from doc_type order by doc_type_name");

                this.RadGridView5.DataSource = dsentities;
                GridViewCheckBoxColumn checkBoxColumn = new GridViewCheckBoxColumn();
                checkBoxColumn.DataType = typeof(int);
                checkBoxColumn.Name = "Enabled";
                checkBoxColumn.FieldName = "Enabled";
                checkBoxColumn.HeaderText = "Habilitado";
                RadGridView5.MasterTemplate.Columns.Add(checkBoxColumn);

                checkBoxColumn.EnableHeaderCheckBox = true;
                checkBoxColumn.EditMode = EditMode.OnValueChange;

                RadGridView5.ValueChanged += radGridView1_ValueChanged;
                RadGridView5.HeaderCellToggleStateChanged += radGridView1_HeaderCellToggleStateChanged;
            }
            catch (Exception ex)
            {

                ZClass.raiseerror(ex);
            }

        }


        void radGridView1_ValueChanged(object sender, EventArgs e)
        {
            if (this.RadGridView5.ActiveEditor is RadCheckBoxEditor)
            {
                Console.WriteLine(this.RadGridView5.CurrentCell.RowIndex);
                Console.WriteLine(this.RadGridView5.ActiveEditor.Value);
            }
        }

        void radGridView1_HeaderCellToggleStateChanged(object sender, GridViewHeaderCellEventArgs e)
        {
            Console.WriteLine(e.Column.Name);
            Console.WriteLine(e.State);
        }

        private void zButton6_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRowInfo r in RadGridView5.Rows)
                {
                    if (Boolean.Parse(r.Cells["Enabled"].Value.ToString()) == true)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void zButton7_Click(object sender, EventArgs e)
        {
            Boolean IsTest = this.chktest.Checked;
            StreamWriter SW = new StreamWriter(Path.Combine(temppath, "SQLOut" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + ".txt"));
            try
            {
                string dbOwner = Zamba.Servers.Server.dbOwner.ToUpper();

                //                string queryR = "select distinct index_id from index_r_doc_type order by doc_type_id, index_id";
                string queryI = "select index_id, index_name, index_type, index_len from doc_index order by index_id";

                DataSet IndexsDS = conDestiny.ExecuteDataset(CommandType.Text, queryI);

                foreach (DataRow r in IndexsDS.Tables[0].Rows)
                {
                    Int64 IndexId = Int64.Parse(r["index_id"].ToString());

                    IIndex currentIndex = Zamba.Core.IndexsBussinesExt.getIndex(IndexId, true);

                    string query = string.Format(@"select COLUMN_NAME, TABLE_NAME, DATA_TYPE, DATA_LENGTH, DATA_PRECISION from all_tab_columns where column_name = 'I{0}' and owner = '{1}' and TABLE_NAME like 'DOC\_%' escape '\' and TABLE_NAME not like '%BK%'", IndexId, dbOwner);

                    DataSet ColumnsDS = conDestiny.ExecuteDataset(CommandType.Text, query);

                    bool Match = true;
                    string sDATATYPE = string.Empty;
                    string sDATALENGTH = string.Empty;
                    string sDATAPRECISION = string.Empty;

                    foreach (DataRow rc in ColumnsDS.Tables[0].Rows)
                    {

                        string TABLE_NAME = rc["TABLE_NAME"].ToString();

                        string DATATYPE = rc["DATA_TYPE"].ToString();
                        string DATALENGTH = rc["DATA_LENGTH"].ToString();
                        string DATAPRECISION = rc["DATA_PRECISION"].ToString();

                        if ((string.Empty != sDATATYPE) && (DATATYPE != sDATATYPE || DATALENGTH != sDATALENGTH || DATAPRECISION != sDATAPRECISION))
                        {
                            Match = false;
                            break;
                        }

                        sDATATYPE = rc["DATA_TYPE"].ToString();
                        sDATALENGTH = rc["DATA_LENGTH"].ToString();
                        sDATAPRECISION = rc["DATA_PRECISION"].ToString();

                    }

                    if (Match == false)
                    {
                        CompareSchema co = new CompareSchema(ColumnsDS.Tables[0], currentIndex);
                        if (co.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {

                                DataRow c = co.SelectedResult;
                                sDATATYPE = c["DATA_TYPE"].ToString();
                                sDATALENGTH = c["DATA_LENGTH"].ToString();
                                sDATAPRECISION = c["DATA_PRECISION"].ToString();

                                foreach (DataRow rc in ColumnsDS.Tables[0].Rows)
                                {
                                    string TABLE_NAME = rc["TABLE_NAME"].ToString();

                                    string DATATYPE = rc["DATA_TYPE"].ToString();
                                    string DATALENGTH = rc["DATA_LENGTH"].ToString();
                                    string DATAPRECISION = rc["DATA_PRECISION"].ToString();

                                    if (DATATYPE != sDATATYPE || DATALENGTH != sDATALENGTH || DATAPRECISION != sDATAPRECISION)
                                    {
                                        string change1 = string.Format("alter table {0} add tempi{1} {2}({3})", TABLE_NAME, IndexId, sDATATYPE, sDATALENGTH, sDATAPRECISION);
                                        SW.WriteLine(change1.ToString() + ";");
                                        if (IsTest == false) conDestiny.ExecuteNonQuery(CommandType.Text, change1);

                                        string change2 = string.Format("update {0} set tempi{1} = i{1}", TABLE_NAME, IndexId);
                                        SW.WriteLine(change2.ToString() + ";");
                                        if (IsTest == false) conDestiny.ExecuteNonQuery(CommandType.Text, change2);

                                        string change3 = string.Format("alter table {0} drop column i{1}", TABLE_NAME, IndexId);
                                        SW.WriteLine(change3.ToString() + ";");
                                        if (IsTest == false) conDestiny.ExecuteNonQuery(CommandType.Text, change3);

                                        string change4 = string.Format("alter table {0} rename column tempi{1} to i{1}", TABLE_NAME, IndexId);
                                        SW.WriteLine(change4.ToString() + ";");
                                        if (IsTest == false) conDestiny.ExecuteNonQuery(CommandType.Text, change4);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ZClass.raiseerror(ex);
                                MessageBox.Show("Error al aplicar el cambio. " + ex.Message);
                            }

                        }
                        else
                        {
                            //ver que hacer si cancela la decision
                        }
                    }

                }

                MessageBox.Show("Proceso Finalizado");


            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            finally
            {
                SW.Flush();
                SW.Close();
                SW.Dispose();
            }
        }

        private void zButton8_Click(object sender, EventArgs e)
        {
            Boolean IsTest = this.chktest.Checked;
            StreamWriter SW = new StreamWriter(Path.Combine(temppath, "SQLOut" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + ".txt"));
            try
            {
                string dbOwner = Zamba.Servers.Server.dbOwner;

                string queryIs = "select index_id, index_name, index_type, index_len from doc_index order by index_id";

                DataSet IndexsDSs = conSource.ExecuteDataset(CommandType.Text, queryIs);

                string queryIdd = "select index_id, index_name, index_type, index_len from doc_index order by index_id";

                DataSet IndexsDSd = conDestiny.ExecuteDataset(CommandType.Text, queryIdd);


                foreach (DataRow rs in IndexsDSs.Tables[0].Rows)
                {
                    Int64 IndexIds = Int64.Parse(rs["index_id"].ToString());
                    string IndexNames = rs["index_name"].ToString();

                    foreach (DataRow rd in IndexsDSd.Tables[0].Rows)
                    {
                        Int64 IndexIdd = Int64.Parse(rd["index_id"].ToString());
                        string IndexNamed = rd["index_name"].ToString();

                        if (IndexIds == IndexIdd)
                        {
                            //Mismo ID
                            if (IndexNames == IndexNamed)
                            {
                                //Mismo Name
                                //No hay conflicto
                            }
                            else
                            {
                                //Hay conflicto de nombres
                                IIndex currentIndexs = Zamba.Core.IndexsBussinesExt.getIndex(IndexIds, true);
                                IIndex currentIndexd = Zamba.Core.IndexsBussinesExt.getIndex(IndexIdd, true);
                                CompareAttributes ca = new CompareAttributes(rs, rd, currentIndexs);

                                if (ca.ShowDialog() == DialogResult.OK)
                                {
                                    if (ca.ConflictResult == AnalizeResults.ReCreateDestiny)
                                    {
                                        Int64 newAttributeId = Zamba.Core.CoreBusiness.GetNewID(IdTypes.DOCINDEXID);

                                        string querydocindex = "select max(index_id) maxid from doc_index";

                                        object maxidD = conDestiny.ExecuteScalar(CommandType.Text, querydocindex);
                                        object maxidS = conSource.ExecuteScalar(CommandType.Text, querydocindex);

                                        if (Int64.Parse(maxidD.ToString()) > newAttributeId || Int64.Parse(maxidS.ToString()) > newAttributeId)
                                        {
                                            newAttributeId = (Int64.Parse(maxidD.ToString()) > Int64.Parse(maxidS.ToString()) ? Int64.Parse(maxidD.ToString()) + 1 : Int64.Parse(maxidS.ToString()) + 1);
                                            Zamba.Core.CoreBusiness.SetNewID(IdTypes.DOCINDEXID, newAttributeId);

                                        }

                                        querydocindex = string.Format("update doc_index set index_id = {0} where index_id = {1}", newAttributeId, IndexIdd);

                                        conDestiny.ExecuteNonQuery(CommandType.Text, querydocindex);

                                        querydocindex = string.Format("update index_r_doc_type set index_id = {0} where index_id = {1}", newAttributeId, IndexIdd);

                                        conDestiny.ExecuteNonQuery(CommandType.Text, querydocindex);

                                        string queryR = string.Format("select distinct doc_type_id from index_r_doc_type where index_id = {0}", IndexIdd);

                                        DataSet dsR = conDestiny.ExecuteDataset(CommandType.Text, queryR);

                                        foreach (DataRow r in dsR.Tables[0].Rows)
                                        {
                                            string change4 = string.Format("alter table DOC_I{0} rename column i{1} to i{2}", r["doc_type_id"].ToString(), IndexIdd, newAttributeId);
                                            SW.WriteLine(change4.ToString() + ";");
                                            if (IsTest == false) conDestiny.ExecuteNonQuery(CommandType.Text, change4);
                                        }

                                        if (currentIndexd.DropDown == IndexAdditionalType.DropDown || currentIndexd.DropDown == IndexAdditionalType.DropDownJerarquico)
                                        {
                                            string change4 = string.Format("rename table ILST_I{0} to ILST_I{1}", IndexIdd, newAttributeId);
                                            SW.WriteLine(change4.ToString() + ";");
                                            if (IsTest == false) conDestiny.ExecuteNonQuery(CommandType.Text, change4);

                                        }
                                        if (currentIndexd.DropDown == IndexAdditionalType.AutoSustitución || currentIndexd.DropDown == IndexAdditionalType.AutoSustituciónJerarquico)
                                        {
                                            string change4 = string.Format("rename table ILST_I{0} to ILST_I{1}", IndexIdd, newAttributeId);
                                            SW.WriteLine(change4.ToString() + ";");
                                            if (IsTest == false) conDestiny.ExecuteNonQuery(CommandType.Text, change4);

                                        }

                                        string changezvir = string.Format("update zvir set indexid = {0} where indexid = {1}", newAttributeId, IndexIdd);
                                        conDestiny.ExecuteNonQuery(CommandType.Text, changezvir);

                                    }
                                    else if (ca.ConflictResult == AnalizeResults.ReCreateOrigin)
                                    {
                                        Int64 newAttributeId = Zamba.Core.CoreBusiness.GetNewID(IdTypes.DOCINDEXID);

                                        string querydocindex = "select max(index_id) maxid from doc_index";

                                        object maxidD = conDestiny.ExecuteScalar(CommandType.Text, querydocindex);
                                        object maxidS = conSource.ExecuteScalar(CommandType.Text, querydocindex);

                                        if (Int64.Parse(maxidD.ToString()) > newAttributeId || Int64.Parse(maxidS.ToString()) > newAttributeId)
                                        {
                                            newAttributeId = (Int64.Parse(maxidD.ToString()) > Int64.Parse(maxidS.ToString()) ? Int64.Parse(maxidD.ToString()) + 1 : Int64.Parse(maxidS.ToString()) + 1);
                                            Zamba.Core.CoreBusiness.SetNewID(IdTypes.DOCINDEXID, newAttributeId);

                                        }

                                        querydocindex = string.Format("update doc_index set index_id = {0} where index_id = {1}", newAttributeId, IndexIds);

                                        conSource.ExecuteDataset(CommandType.Text, querydocindex);

                                        string queryR = string.Format("select distinct doc_type_id from index_r_doc_type where index_id = {0}", IndexIds);

                                        DataSet dsR = conSource.ExecuteDataset(CommandType.Text, queryR);

                                        foreach (DataRow r in dsR.Tables[0].Rows)
                                        {
                                            try
                                            {

                                                string change4 = string.Format("alter table DOC_I{0} rename column i{1} to i{2}", r["doc_type_id"].ToString(), IndexIds, newAttributeId);
                                                conSource.ExecuteNonQuery(CommandType.Text, change4);
                                            }
                                            catch (Exception)
                                            {
                                            }
                                        }

                                        if (currentIndexs.DropDown == IndexAdditionalType.DropDown || currentIndexs.DropDown == IndexAdditionalType.DropDownJerarquico)
                                        {
                                            try
                                            {
                                                string change4 = string.Format("rename table SLST_S{0} to SLST_S{1}", IndexIds, newAttributeId);
                                                conSource.ExecuteNonQuery(CommandType.Text, change4);

                                            }
                                            catch (Exception)
                                            {
                                            }

                                        }
                                        if (currentIndexs.DropDown == IndexAdditionalType.AutoSustitución || currentIndexs.DropDown == IndexAdditionalType.AutoSustituciónJerarquico)
                                        {
                                            try
                                            {
                                                string change4 = string.Format("rename table SLST_S{0} to SLST_S{1}", IndexIds, newAttributeId);
                                                conSource.ExecuteNonQuery(CommandType.Text, change4);

                                            }
                                            catch (Exception)
                                            {
                                            }

                                        }

                                        string changezvir = string.Format("update zvir set indexid = {0} where indexid = {1}", newAttributeId, IndexIds);
                                        conSource.ExecuteNonQuery(CommandType.Text, changezvir);


                                        string queryDI2 = string.Format("update index_R_DOC_TYPE set index_id ={0} where index_id = {1}", newAttributeId, IndexIds);
                                        string queryDI3 = string.Format("update ZIR set indexid ={0} where indexid = {1}", newAttributeId, IndexIds);
                                        string queryDI4 = string.Format("update wfruleparamitems set c_value = replace (c_value,'//{1}','//{0}') where   item =1 and c_value like '%//{1}%' and rule_id in (select id from wfrules where class = 'DOGenerateTaskResult')", newAttributeId, IndexIds);
                                        string queryDI5 = string.Format("update wfruleparamitems set c_value = replace (c_value,'{1}|','{0}|') where   item =0 and c_value like '%{1}|%'  and rule_id in (select id from wfrules where class = 'IfIndex')", newAttributeId, IndexIds);
                                        string queryDI6 = string.Format("update wfruleparamitems set c_value = replace (c_value,'{1}|','{0}|') where   item =0 and c_value like '%{1}|%'  and rule_id in (select id from wfrules where class = 'DoFillIndex')", newAttributeId, IndexIds);
                                        string queryDI7 = string.Format("update wfruleparamitems set c_value = replace (c_value,'{1}','{0}') where   item =0 and c_value = '{1}'  and rule_id in (select id from wfrules where class = 'IfIndex')", newAttributeId, IndexIds);
                                        string queryDI8 = string.Format("update wfruleparamitems set c_value = replace (c_value,'{1}','{0}') where   item =0 and c_value = '{1}'  and rule_id in (select id from wfrules where class = 'DoFillIndex')", newAttributeId, IndexIds);

                                        conSource.ExecuteNonQuery(CommandType.Text, queryDI2);
                                        conSource.ExecuteNonQuery(CommandType.Text, queryDI3);
                                        conSource.ExecuteNonQuery(CommandType.Text, queryDI4);
                                        conSource.ExecuteNonQuery(CommandType.Text, queryDI5);
                                        conSource.ExecuteNonQuery(CommandType.Text, queryDI6);
                                        conSource.ExecuteNonQuery(CommandType.Text, queryDI7);
                                        conSource.ExecuteNonQuery(CommandType.Text, queryDI8);





                                    }
                                    else if (ca.ConflictResult == AnalizeResults.UpdateDestiny)
                                    {
                                        string querydocindex = string.Format("update doc_index set index_name = '{0}' where index_id = {1}", IndexNames, IndexIdd);
                                        conDestiny.ExecuteNonQuery(CommandType.Text, querydocindex);
                                    }
                                    else if (ca.ConflictResult == AnalizeResults.UpdateSource)
                                    {
                                        //mismo nombre, distinto ID
                                        string querydocindex = string.Format("update doc_index set index_name = '{0}' where index_id = {1}", IndexNamed, IndexIds);
                                        conSource.ExecuteNonQuery(CommandType.Text, querydocindex);
                                    }



                                }
                                else
                                {
                                }

                            }


                        }
                        else
                        {

                        }

                    }
                }

                MessageBox.Show("Proceso Finalizado");
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            finally
            {
                SW.Flush();
                SW.Close();
                SW.Dispose();
            }
        }

        private void zButton9_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dse = conDestiny.ExecuteDataset(CommandType.Text, "select doc_type_id from doc_type");



                foreach (DataRow r in dse.Tables[0].Rows)
                {
                    Zamba.Servers.Server.CreateTables().AddDocsTables(Int64.Parse(r["Doc_Type_Id"].ToString()));
                }


                DataSet dsei = conDestiny.ExecuteDataset(CommandType.Text, "select r.doc_type_id , r.index_id, i.index_type from index_r_doc_type r inner join doc_index i on i.index_id = r.index_id");


                foreach (DataRow r in dsei.Tables[0].Rows)
                {
                    IIndex Index = Zamba.Core.IndexsBusiness.GetIndexById(Int64.Parse(r["Index_Id"].ToString()), string.Empty);
                    if (!IndexsBusiness.getReferenceStatus(Int64.Parse(r["Doc_Type_Id"].ToString()), Index.ID))
                    {
                        if (DocTypesBusiness.CheckColumn(Int64.Parse(r["Doc_Type_Id"].ToString()), Index.ID, Index.Type, Index.Len) == false)
                        {
                            DocTypesBusiness.AddColumn(Int64.Parse(r["Doc_Type_Id"].ToString()), Index.ID, Index.Type, Index.Len);
                            ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Se agrega el indice Id: {0} en  DocTypeId: {1}.", Index.ID, Int64.Parse(r["Doc_Type_Id"].ToString())));
                        }
                        else
                        {
                            ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("El indice Id: {0} existe en la entidad DocTypeId: {1}.", Index.ID, Int64.Parse(r["Doc_Type_Id"].ToString())));
                        }

                    }
                    else
                    {
                        ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("El indice Id: {0} DocTypeId: {1} es referencial.", Index.Type, Int64.Parse(r["Doc_Type_Id"].ToString())));
                    }
                }

                DataSet dsi = conDestiny.ExecuteDataset(CommandType.Text, "select  index_id, dropdown from doc_index ");

                foreach (DataRow r in dsi.Tables[0].Rows)
                {
                    IIndex I = Zamba.Core.IndexsBusiness.GetIndexById(Int64.Parse(r["Index_Id"].ToString()), string.Empty);

                    if (I.DropDown == IndexAdditionalType.AutoSustitución || I.DropDown == IndexAdditionalType.AutoSustituciónJerarquico)
                    {
                        //if (I.Script.Length > 0)
                        //{
                        //    //IndexsBusiness.addindexsust(I.ID, I.VistaoTabla, I.ColumnaCodigo, I.ColumnaDescripcion, I.Script);
                        //}
                        //else
                        //{
                        IndexsBusiness.createsustituciontable(I.ID, I.Len, (IndexDataType)Enum.Parse(typeof(IndexDataType), I.Type.ToString()));
                        //}
                    }
                    else if (I.DropDown == IndexAdditionalType.DropDown || I.DropDown == IndexAdditionalType.DropDownJerarquico)
                    {
                        //if (I.Script.Length > 0)
                        //{
                        //    IndexsBusiness.addindexlist(I.IndexId, I.VistaoTabla, I.ColumnaCodigo, I.ColumnaDescripcion, I.Script);
                        //}
                        //else
                        //{
                        IndexsBusiness.addindexlist(I.ID, I.Len);
                        //}
                    }
                    else
                    {
                    }

                    //TODO: Falta agregar la relacion de las DOC_I a las listas
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void zButton10_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void zButton11_Click(object sender, EventArgs e)
        {
            Boolean IsTest = this.chktest.Checked;
            StreamWriter SW = new StreamWriter(Path.Combine(temppath, "SQLOut" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + ".txt"));
            try
            {
                DataSet dse = conDestiny.ExecuteDataset(CommandType.Text, "select u.id userid, g.id groupid from usrtable u inner join usrgroup g on u.id = g.id");

                foreach (DataRow r in dse.Tables[0].Rows)
                {
                    SW.WriteLine("delete from zss where userid = " + r["userid"].ToString() + ";");
                    if (IsTest == false) conDestiny.ExecuteNonQuery(CommandType.Text, "delete from zss where userid = " + r["userid"].ToString());
                    SW.WriteLine("delete from ucm where user_id = " + r["userid"].ToString() + ";");
                    if (IsTest == false) conDestiny.ExecuteNonQuery(CommandType.Text, "delete from ucm where user_id = " + r["userid"].ToString());

                    SW.WriteLine("delete from usrtable where id = " + r["userid"].ToString() + ";");
                    if (IsTest == false) conDestiny.ExecuteNonQuery(CommandType.Text, "delete from usrtable where id = " + r["userid"].ToString());
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            finally
            {

                SW.Flush();
                SW.Close();
                SW.Dispose();
            }
        }

        private void zButton12_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void zButton13_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "";
                DataSet TempDs = conSource.ExecuteDataset(CommandType.Text, query);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            CheckEveryRow = this.chkdeletes.Checked;
        }

        private void BuscarConflictos(object sender, EventArgs e)
        {
            if (conDestiny == null || conDestiny.CN == null)
                conDestiny = Zamba.Servers.Server.get_Con(apDestiny.SERVERTYPE, apDestiny.SERVER, apDestiny.DB, apDestiny.USER, apDestiny.PASSWORD, true, false);


            Transaction transaction = new Transaction(conDestiny);

            Boolean IsTest = true;
            StreamWriter SW = new StreamWriter(Path.Combine(temppath, "SQLOut" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + ".txt"));
            try
            {
                DataSet TempSourceDS = null;
                DataSet TempDestinyDs = null;
                String Query = null;

                prgBar.Value = 0;
                prgBar.Maximum = _tablesList.Count;

                this.RadGridView2.DataSource = _tablesList.Values;
                //this.RadGridView2.DataSource = null;
                //this.RadGridView2.DataSource = _tablesList.Values;


                DataSet BeforeDs = new DataSet();
                DataSet AfterDs = new DataSet();
                String SelectQuery;
                String InsertQuery;
                String UpdateQuery;


                StringBuilder QueryBuilderInsert;
                StringBuilder QueryBuilderSelect;
                StringBuilder QueryBuilderUpdate;


                DateTime TryValue;
                int i;
                object CurrentValue;
                string strCurrentValue;
                IDbDataParameter[] blobData = null;
                int blobCount = 0;
                List<int> blobPositions = new List<int>();

                string blobParam;


                try
                {
                    List<Int64> NewEntities = new List<Int64>();
                    List<NewIndex> NewIndexsInEntities = new List<NewIndex>();
                    List<NewIndex> NewIndexs = new List<NewIndex>();

                    if (resultsfile.Length > 0)
                    {
                        if (TempSourceDS == null)
                        {
                            TempSourceDS = new DataSet();
                        }
                        TempSourceDS.ReadXml(resultsfile);
                    }


                    foreach (KeyValuePair<String, TableStructure> Item in _tablesList)
                    {
                        if (Item.Value.enabled)
                        {
                            this.RadGridView2.CurrentRow = this.RadGridView2.Rows[prgBar.Value];
                            prgBar.Value += 1;
                            Query = "SELECT * FROM " + Item.Key;

                            if (!string.IsNullOrEmpty(Item.Value.filter))
                            {
                                Query += " WHERE " + Item.Value.filter;
                            }

                            try
                            {
                                if (resultsfile.Length == 0)
                                {
                                    TempSourceDS = conSource.ExecuteDataset(CommandType.Text, Query);
                                    if (null != TempSourceDS && TempSourceDS.Tables.Count > 0 && TempSourceDS.Tables[0].Rows.Count > 0)
                                    {
                                        TempSourceDS.Tables[0].TableName = Item.Key;
                                    }
                                }



                                TempDestinyDs = conDestiny.ExecuteDataset(CommandType.Text, Query);

                                if (null != TempSourceDS && TempSourceDS.Tables.Count > 0 && TempSourceDS.Tables[Item.Key] != null && TempSourceDS.Tables[Item.Key].Rows.Count > 0)
                                {
                                    TempSourceDS.Tables[Item.Key].Columns.Add("Compare", typeof(String));
                                    TempSourceDS.AcceptChanges();

                                    DataTable TempSourceDT = TempSourceDS.Tables[Item.Key];

                                    TempDestinyDs.Tables[0].TableName = Item.Key;
                                    TempDestinyDs.Tables[0].Columns.Add("Compare", typeof(String));
                                    TempDestinyDs.AcceptChanges();

                                    this.RadGridView3.DataSource = null;
                                    this.RadGridView3.DataSource = TempSourceDT;

                                    if (TempDestinyDs == null)
                                    {
                                        VerifyTable(conDestiny, TempSourceDT);
                                        GenerateDestinyTable(Item.Value);
                                    }

                                    InsertQuery = BuildTableQueryInsert(TempSourceDT);
                                    SelectQuery = BuildTableQuerySelect(TempSourceDT);
                                    UpdateQuery = BuildTableQueryUpdate(TempSourceDT, Item.Value);
                                    //DeleteQuery = BuildTableQueryDelete(CurrentTable);

                                    QueryBuilderInsert = new StringBuilder();
                                    QueryBuilderSelect = new StringBuilder();
                                    QueryBuilderUpdate = new StringBuilder();
                                    //QueryBuilderDelete = new StringBuilder();

                                    //Se verifica la existencia de archivos digitales (blobs)
                                    foreach (DataColumn col in TempSourceDT.Columns)
                                    {
                                        //Arreglo de Columnas diferentes entre sql y oracle C_
                                        if (conDestiny.isOracle == false && conSource.isOracle == true)
                                        {
                                            if (col.ColumnName.ToLower().StartsWith("c_"))
                                            {
                                                //ESTO ES UN PROBLEMA!
                                                col.ColumnName = col.ColumnName.Substring(2, col.ColumnName.Length - 2);
                                            }

                                            TempSourceDT.AcceptChanges();
                                        }

                                        if (col.DataType == typeof(Byte[]))
                                        {
                                            if (conDestiny.isOracle)
                                            {
                                                blobCount++;
                                                blobPositions.Add(col.Ordinal);
                                            }
                                            else
                                            {
                                                blobCount++;
                                                blobPositions.Add(col.Ordinal);
                                            }
                                        }
                                    }

                                    if (blobCount > 0)
                                    {
                                        blobData = new IDbDataParameter[blobCount];
                                        blobCount = 0;
                                    }





                                    this.progressBar1.Value = 0;
                                    progressBar1.Maximum = TempSourceDT.Rows.Count;

                                    foreach (DataRow r1 in TempSourceDT.Rows)
                                    {
                                        this.RadGridView3.CurrentRow = this.RadGridView3.Rows[progressBar1.Value];
                                        this.progressBar1.Value += 1;


                                        Boolean FoundOrigin = false;
                                        Boolean FoundDestiny = false;
                                        AnalizeResults AnalizeResult;


                                        if (TempDestinyDs == null)
                                        {
                                            r1["Compare"] = "Insert";
                                            Item.Value.Inserts++;
                                        }
                                        else
                                        {
                                            string CompareUnique = CompareUniquesString(r1, Item.Value);
                                            CompareUnique = (CompareUnique.Length > 0) ? " or (" + CompareUnique + ")" : string.Empty;
                                            DataRow[] TempDestinyDT = TempDestinyDs.Tables[0].Select("(Compare is null or Compare = '' or Compare = 'FoundDestiny') and (( " + CompareKeysString(r1, Item.Value) + " ) " + CompareUnique + ")");

                                            this.RadGridView4.DataSource = null;
                                            if (TempDestinyDT.Length > 0)
                                                this.RadGridView4.DataSource = TempDestinyDT.CopyToDataTable();


                                            this.progressBar2.Value = 0;
                                            progressBar2.Maximum = TempDestinyDT.Length;

                                            AnalizeResult = AnalizeResults.DontInsert;
                                            if (TempDestinyDT.Length > 0)
                                            {
                                                foreach (DataRow r2 in TempDestinyDT)
                                                {
                                                    this.RadGridView4.CurrentRow = this.RadGridView4.Rows[progressBar2.Value];
                                                    this.progressBar2.Value += 1;


                                                    if (r2["Compare"].ToString() == string.Empty || r2["Compare"].ToString() == "FoundDestiny")
                                                    {
                                                        if (CompareKeys(r1, r2, Item.Value) || CompareUniques(r1, r2, Item.Value))
                                                        {
                                                            FoundOrigin = true;

                                                            AnalizeResult = AnalizeRow(r1, r2, Item.Value);

                                                            if (AnalizeResult == AnalizeResults.Update)
                                                            {
                                                                r1["Compare"] = "Update";
                                                                r2["Compare"] = "Update";
                                                                Item.Value.Updates++;
                                                            }
                                                            else if (AnalizeResult == AnalizeResults.Equals)
                                                            {
                                                                r1["Compare"] = "Equals";
                                                                r2["Compare"] = "Equals";
                                                                Item.Value.Equals++;
                                                            }
                                                            else if (AnalizeResult == AnalizeResults.ReCreateOrigin)
                                                            {
                                                                r1["Compare"] = "ReCreateOrigin";
                                                                r2["Compare"] = "Keep";
                                                                Item.Value.ReCreateOrigin++;

                                                                GenerateNewIdOnOrigin(r1, r2, Item.Value);
                                                            }
                                                            else if (AnalizeResult == AnalizeResults.ReCreateDestiny)
                                                            {
                                                                r1["Compare"] = "Insert";
                                                                r2["Compare"] = "ReCreateDestiny";
                                                                Item.Value.ReCreateDestiny++;
                                                                GenerateNewIdOnDestiny(r1, r2, Item.Value);
                                                            }
                                                            else if (AnalizeResult == AnalizeResults.KeepBoth)
                                                            {
                                                                r1["Compare"] = "Insert";
                                                                r2["Compare"] = "Keep";
                                                                Item.Value.InsertNew++;
                                                            }

                                                            break;
                                                        }


                                                        if (FoundOrigin == false || r2["Compare"].ToString() == string.Empty)
                                                        {
                                                            foreach (DataRow r3 in TempSourceDT.Rows)
                                                            {
                                                                if (r3["Compare"].ToString() != string.Empty || CompareKeys(r3, r2, Item.Value))
                                                                {
                                                                    FoundDestiny = true;
                                                                    r2["Compare"] = "FoundDestiny";

                                                                    break;
                                                                }

                                                            }
                                                            if (FoundDestiny == false)
                                                            {
                                                                r2["Compare"] = "Only Destiny";
                                                                Item.Value.OnlyDestiny++;

                                                                AnalizeResults ConflictResult = AnalizeResults.KeepBoth;

                                                                if (DontAskAgainDestiny.Contains(Item.Key))
                                                                {
                                                                    ConflictResult = LastResultOnlyDestiny;
                                                                }
                                                                else
                                                                {
                                                                    if (checkBox1.Checked == true)
                                                                    {
                                                                        ConflictResult = AnalizeResults.DontInsert;
                                                                    }
                                                                    else
                                                                    {
                                                                        KeepDestinyOptions co = new KeepDestinyOptions(r2, Item.Value);
                                                                        if (co.ShowDialog() == DialogResult.OK)
                                                                        {
                                                                            if (co.DontAskAgain)
                                                                            {
                                                                                DontAskAgainDestiny.Add(Item.Key);
                                                                                LastResultOnlyDestiny = co.ConflictResult;
                                                                                ConflictResult = LastResultOnlyDestiny;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                        }
                                                                    }
                                                                }

                                                                if (ConflictResult == AnalizeResults.DeleteOrigin)
                                                                {
                                                                    string deletequery = BuildTableQueryDelete(r2, Item.Value);
                                                                    SW.WriteLine(deletequery + ";");
                                                                    if (IsTest == false) conDestiny.ExecuteNonQuery(CommandType.Text, deletequery);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }

                                            }
                                            else
                                            {
                                                r1["Compare"] = "Insert";
                                                Item.Value.Inserts++;
                                            }
                                            TempSourceDT.AcceptChanges();
                                            TempDestinyDs.AcceptChanges();
                                            this.RadGridView2.MasterTemplate.Refresh(null);

                                            GridTableElement tableElement2 = this.RadGridView2.CurrentView as GridTableElement;
                                            GridViewRowInfo row2 = this.RadGridView2.CurrentRow;

                                            if (tableElement2 != null && row2 != null)
                                            {
                                                tableElement2.ScrollToRow(row2);
                                            }

                                            GridTableElement tableElement3 = this.RadGridView3.CurrentView as GridTableElement;
                                            GridViewRowInfo row3 = this.RadGridView3.CurrentRow;

                                            if (tableElement3 != null && row3 != null)
                                            {
                                                tableElement3.ScrollToRow(row3);
                                            }

                                            GridTableElement tableElement4 = this.RadGridView4.CurrentView as GridTableElement;
                                            GridViewRowInfo row4 = this.RadGridView4.CurrentRow;

                                            if (tableElement4 != null && row4 != null)
                                            {
                                                tableElement4.ScrollToRow(row4);
                                            }

                                            //this.RadGridView2.DataSource = null;
                                            //this.RadGridView3.DataSource = null;
                                            //this.RadGridView3.DataSource = TempSourceDs.Tables[0];
                                            //this.RadGridView4.DataSource = TempDestinyDs.Tables[0];
                                            //this.RadGridView2.DataSource = null;
                                            //this.RadGridView2.DataSource = _tablesList.Values;
                                            Application.DoEvents();


                                            if (r1["Compare"].ToString() == "Insert")
                                            {

                                                QueryBuilderInsert.Remove(0, QueryBuilderInsert.Length);
                                                // QueryBuilderInsert.Append(IndentityDisabledString);
                                                QueryBuilderInsert.Append(InsertQuery);

                                                QueryBuilderSelect.Remove(0, QueryBuilderSelect.Length);
                                                QueryBuilderSelect.Append(SelectQuery);

                                                //QueryBuilderUpdate.Remove(0, QueryBuilderUpdate.Length);
                                                //QueryBuilderUpdate.Append(UpdateQuery);

                                                //QueryBuilderDelete.Remove(0, QueryBuilderDelete.Length);
                                                //QueryBuilderDelete.Append(DeleteQuery);

                                                for (i = 0; i < r1.ItemArray.Length; i++)
                                                {
                                                    String CurrentColumnName = r1.Table.Columns[i].ToString();
                                                    if (CurrentColumnName != "Script" && CurrentColumnName != "Compare")
                                                    {
                                                        CurrentValue = r1.ItemArray[i];
                                                        strCurrentValue = CurrentValue.ToString();

                                                        if (blobPositions.Contains(i))
                                                        {//Verifica si es un blob
                                                            blobParam = "@blob" + i.ToString();
                                                            QueryBuilderInsert.Append(blobParam);
                                                            SqlParameter pDocFile = default(SqlParameter);

                                                            //TODO: FALTA LA PARTE DE ORACLE
                                                            pDocFile = new SqlParameter(blobParam, SqlDbType.VarBinary, ((byte[])CurrentValue).Length, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, CurrentValue);

                                                            blobData[blobCount] = pDocFile;
                                                            blobCount++;
                                                        }
                                                        else if ((strCurrentValue.Contains(_guion) || strCurrentValue.Contains("/")) && strCurrentValue.Contains(_dosPuntos) && DateTime.TryParse(strCurrentValue, out TryValue))
                                                        {//Verifica si es una fecha
                                                            QueryBuilderInsert.Append(conDestiny.ConvertDateTime(TryValue));
                                                            QueryBuilderSelect.Replace("{" + i.ToString() + "}", conDestiny.ConvertDateTime(TryValue));
                                                            //      QueryBuilderUpdate.Append(conDestiny.ConvertDateTime(TryValue));
                                                            //QueryBuilderDelete.Append(conDestiny.ConvertDateTime(TryValue));
                                                        }
                                                        else if (chkUseBool.Checked && String.Compare(strCurrentValue, _true, true) == 0)
                                                        {//Verifica si es un booleano True
                                                            QueryBuilderInsert.Append(_uno);
                                                            QueryBuilderSelect.Replace("{" + i.ToString() + "}", _uno);
                                                            //QueryBuilderUpdate.Append(_uno);
                                                            //QueryBuilderDelete.Append(_uno);
                                                        }
                                                        else if (chkUseBool.Checked && String.Compare(strCurrentValue, _false, true) == 0)
                                                        {//Verifica si es un booleano False
                                                            QueryBuilderInsert.Append(_cero);
                                                            QueryBuilderSelect.Replace("{" + i.ToString() + "}", _cero);
                                                            //QueryBuilderUpdate.Append(_cero);
                                                            //QueryBuilderDelete.Append(_cero);
                                                        }
                                                        else if (CurrentValue is DBNull && conDestiny.isOracle == false)
                                                        {//Verifica si es nulo
                                                            QueryBuilderInsert.Append(_nulo);
                                                            QueryBuilderSelect.Replace("{" + i.ToString() + "}", _nulo);
                                                            //QueryBuilderUpdate.Append(_nulo);
                                                            //QueryBuilderDelete.Append(_nulo);
                                                        }
                                                        else
                                                        {//Se agrega el valor sin modificación
                                                            strCurrentValue = strCurrentValue.Replace(_comillaSimple, _comillaSimple2);
                                                            QueryBuilderInsert.Append(_comillaSimple);
                                                            QueryBuilderInsert.Append(strCurrentValue);
                                                            QueryBuilderInsert.Append(_comillaSimple);

                                                            // QueryBuilderSelect.Append(_comillaSimple);
                                                            if (strCurrentValue == string.Empty)
                                                            {
                                                                //quitar esta columna del select
                                                                QueryBuilderSelect.Replace(" = {" + i.ToString() + "}", " is null ");
                                                            }
                                                            else
                                                            {
                                                                QueryBuilderSelect.Replace("{" + i.ToString() + "}", _comillaSimple + strCurrentValue + _comillaSimple);
                                                            }//      QueryBuilderSelect.Append(_comillaSimple);

                                                            //QueryBuilderUpdate.Append(_comillaSimple);
                                                            //QueryBuilderUpdate.Append(strCurrentValue);
                                                            //QueryBuilderUpdate.Append(_comillaSimple);

                                                            //QueryBuilderDelete.Append(_comillaSimple);
                                                            //QueryBuilderDelete.Append(strCurrentValue);
                                                            //QueryBuilderDelete.Append(_comillaSimple);
                                                        }

                                                        QueryBuilderInsert.Append(_coma);
                                                        //                                QueryBuilderUpdate.Append(_coma);
                                                        //QueryBuilderDelete.Append(_coma);
                                                    }
                                                }

                                                //Se remueve la coma que queda
                                                QueryBuilderInsert.Remove(QueryBuilderInsert.Length - 1, 1);
                                                QueryBuilderInsert.Append(_cierraParentesis);
                                                // QueryBuilderInsert.Append(IndentityEnableString);

                                                // QueryBuilderSelect.Append(IndentityEnableString);

                                                //QueryBuilderUpdate.Remove(QueryBuilderInsert.Length - 1, 1);
                                                //QueryBuilderUpdate.Append(_cierraParentesis);
                                                //QueryBuilderUpdate.Append(IndentityEnableString);

                                                //QueryBuilderDelete.Remove(QueryBuilderInsert.Length - 1, 1);
                                                //QueryBuilderDelete.Append(_cierraParentesis);
                                                //QueryBuilderDelete.Append(IndentityEnableString);

                                                try
                                                {
                                                    if (blobCount > 0)
                                                    {
                                                        //  ZTrace.WriteLineIf(ZTrace.IsError, "Armo Query Insert Tabla con BLOB: " + TempSourceDT.TableName + " - " + QueryBuilderInsert.ToString());
                                                        this.listView1.Items.Add(QueryBuilderInsert.ToString());
                                                        Application.DoEvents();

                                                        SW.WriteLine(QueryBuilderInsert.ToString() + ";");
                                                        if (IsTest == false) conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, QueryBuilderInsert.ToString(), blobData);
                                                        blobCount = 0;
                                                    }
                                                    else
                                                    {

                                                        Int32 count = Int32.Parse(conDestiny.ExecuteScalar(transaction.Transaction, CommandType.Text, QueryBuilderSelect.ToString()).ToString());

                                                        if (count == 0)
                                                        {
                                                            // ZTrace.WriteLineIf(ZTrace.IsError, "Armo Query Insert Tabla: " + TempSourceDT.TableName + " - " + QueryBuilderInsert.ToString());
                                                            this.listView1.Items.Add(QueryBuilderInsert.ToString());
                                                            this.listView1.Items[this.listView1.Items.Count - 1].EnsureVisible();
                                                            if (QueryBuilderInsert.ToString().Contains("Error"))
                                                                this.listView1.Items[this.listView1.Items.Count - 1].ForeColor = System.Drawing.Color.Red;
                                                            Application.DoEvents();
                                                            SW.WriteLine(QueryBuilderInsert.ToString() + ";");
                                                            if (IsTest == false) conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, QueryBuilderInsert.ToString());

                                                            if (TempSourceDT.TableName == "DOC_TYPE")
                                                            {
                                                                NewEntities.Add(Int64.Parse(r1["DOC_TYPE_ID"].ToString()));
                                                            }

                                                            if (TempSourceDT.TableName == "INDEX_R_DOC_TYPE")
                                                            {
                                                                NewIndexsInEntities.Add(new NewIndex(Int64.Parse(r1["INDEX_ID"].ToString()), Int64.Parse(r1["DOC_TYPE_ID"].ToString())));
                                                            }

                                                            if (TempSourceDT.TableName == "DOC_INDEX")
                                                            {
                                                                NewIndexs.Add(new NewIndex(Int64.Parse(r1["INDEX_ID"].ToString()), string.Empty, Int32.Parse(r1["Index_Len"].ToString()), (IndexDataType)Int32.Parse(r1["Index_Type"].ToString()), (IndexAdditionalType)Int32.Parse(r1["DropDown"].ToString())));
                                                            }
                                                        }
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    if (ex.Message.Contains("Primary") || ex.Message.Contains("unique"))
                                                    {
                                                        this.listView1.Items.Add(ex.Message + ": " + QueryBuilderInsert.ToString());
                                                        //                                    conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, QueryBuilderUpdate.ToString());
                                                    }
                                                    else if (ex.Message.Contains("parent key not found"))
                                                    {
                                                        this.listView1.Items.Add(ex.Message + ": " + QueryBuilderInsert.ToString());
                                                    }
                                                    else
                                                    {
                                                        ZClass.raiseerror(ex);
                                                        this.listView1.Items.Add(ex.Message);
                                                        Application.DoEvents();
                                                    }
                                                }

                                                QueryBuilderInsert.Remove(0, QueryBuilderInsert.Length);

                                            }
                                            else if (r1["Compare"].ToString() == "Update")
                                            {

                                                QueryBuilderUpdate.Remove(0, QueryBuilderUpdate.Length);
                                                QueryBuilderUpdate.Append(UpdateQuery);

                                                for (i = 0; i < r1.ItemArray.Length; i++)
                                                {
                                                    String CurrentColumnName = r1.Table.Columns[i].ToString();
                                                    if (CurrentColumnName != "Script" && CurrentColumnName != "Compare")
                                                    {
                                                        CurrentValue = r1.ItemArray[i];
                                                        strCurrentValue = CurrentValue.ToString();

                                                        if (blobPositions.Contains(i))
                                                        {//Verifica si es un blob
                                                            blobParam = "@blob" + i.ToString();
                                                            QueryBuilderUpdate.Append(blobParam);
                                                            SqlParameter pDocFile = default(SqlParameter);

                                                            //TODO: FALTA LA PARTE DE ORACLE
                                                            pDocFile = new SqlParameter(blobParam, SqlDbType.VarBinary, ((byte[])CurrentValue).Length, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, CurrentValue);

                                                            blobData[blobCount] = pDocFile;
                                                            blobCount++;
                                                        }
                                                        else if ((strCurrentValue.Contains(_guion) || strCurrentValue.Contains("/")) && strCurrentValue.Contains(_dosPuntos) && DateTime.TryParse(strCurrentValue, out TryValue))
                                                        {//Verifica si es una fecha
                                                            //QueryBuilderUpdate.Append(conDestiny.ConvertDateTime(TryValue));
                                                            QueryBuilderUpdate.Replace("{" + i.ToString() + "}", conDestiny.ConvertDateTime(TryValue));
                                                            //      QueryBuilderUpdate.Append(conDestiny.ConvertDateTime(TryValue));
                                                            //QueryBuilderDelete.Append(conDestiny.ConvertDateTime(TryValue));
                                                        }
                                                        else if (chkUseBool.Checked && String.Compare(strCurrentValue, _true, true) == 0)
                                                        {//Verifica si es un booleano True
                                                            //QueryBuilderUpdate.Append(_uno);
                                                            QueryBuilderUpdate.Replace("{" + i.ToString() + "}", _uno);
                                                            //QueryBuilderUpdate.Append(_uno);
                                                            //QueryBuilderDelete.Append(_uno);
                                                        }
                                                        else if (chkUseBool.Checked && String.Compare(strCurrentValue, _false, true) == 0)
                                                        {//Verifica si es un booleano False
                                                            //QueryBuilderUpdate.Append(_cero);
                                                            QueryBuilderUpdate.Replace("{" + i.ToString() + "}", _cero);
                                                            //QueryBuilderUpdate.Append(_cero);
                                                            //QueryBuilderDelete.Append(_cero);
                                                        }
                                                        else if (CurrentValue is DBNull && conDestiny.isOracle == false)
                                                        {//Verifica si es nulo
                                                            //QueryBuilderUpdate.Append(_nulo);
                                                            QueryBuilderUpdate.Replace("{" + i.ToString() + "}", _nulo);
                                                            //QueryBuilderUpdate.Append(_nulo);
                                                            //QueryBuilderDelete.Append(_nulo);
                                                        }
                                                        else
                                                        {//Se agrega el valor sin modificación
                                                            strCurrentValue = strCurrentValue.Replace(_comillaSimple, _comillaSimple2);
                                                            // QueryBuilderUpdate.Append(_comillaSimple);
                                                            //QueryBuilderUpdate.Append(strCurrentValue);
                                                            //QueryBuilderUpdate.Append(_comillaSimple);

                                                            // QueryBuilderSelect.Append(_comillaSimple);
                                                            if (strCurrentValue == string.Empty)
                                                            {
                                                                //quitar esta columna del select
                                                                QueryBuilderUpdate.Replace("{" + i.ToString() + "}", " null ");
                                                            }
                                                            else
                                                            {
                                                                QueryBuilderUpdate.Replace("{" + i.ToString() + "}", _comillaSimple + strCurrentValue + _comillaSimple);
                                                            }//      QueryBuilderSelect.Append(_comillaSimple);

                                                            //QueryBuilderUpdate.Append(_comillaSimple);
                                                            //QueryBuilderUpdate.Append(strCurrentValue);
                                                            //QueryBuilderUpdate.Append(_comillaSimple);

                                                            //QueryBuilderDelete.Append(_comillaSimple);
                                                            //QueryBuilderDelete.Append(strCurrentValue);
                                                            //QueryBuilderDelete.Append(_comillaSimple);
                                                        }

                                                        //QueryBuilderUpdate.Append(_coma);
                                                        //                                QueryBuilderUpdate.Append(_coma);
                                                        //QueryBuilderDelete.Append(_coma);
                                                    }
                                                }

                                                //Se remueve la coma que queda
                                                //QueryBuilderUpdate.Remove(QueryBuilderUpdate.Length - 1, 1);
                                                //QueryBuilderUpdate.Append(_cierraParentesis);
                                                // QueryBuilderUpdate.Append(IndentityEnableString);

                                                // QueryBuilderSelect.Append(IndentityEnableString);

                                                //QueryBuilderUpdate.Remove(QueryBuilderUpdate.Length - 1, 1);
                                                //QueryBuilderUpdate.Append(_cierraParentesis);
                                                //QueryBuilderUpdate.Append(IndentityEnableString);

                                                //QueryBuilderDelete.Remove(QueryBuilderUpdate.Length - 1, 1);
                                                //QueryBuilderDelete.Append(_cierraParentesis);
                                                //QueryBuilderDelete.Append(IndentityEnableString);

                                                try
                                                {
                                                    if (blobCount > 0)
                                                    {
                                                        //  ZTrace.WriteLineIf(ZTrace.IsError, "Armo Query Insert Tabla con BLOB: " + TempSourceDT.TableName + " - " + QueryBuilderUpdate.ToString());
                                                        this.listView1.Items.Add(QueryBuilderUpdate.ToString());
                                                        Application.DoEvents();

                                                        SW.WriteLine(QueryBuilderUpdate.ToString() + ";");
                                                        if (IsTest == false) conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, QueryBuilderUpdate.ToString(), blobData);
                                                        blobCount = 0;
                                                    }
                                                    else
                                                    {

                                                        //  ZTrace.WriteLineIf(ZTrace.IsError, "Armo Query Insert Tabla: " + TempSourceDT.TableName + " - " + QueryBuilderUpdate.ToString());
                                                        this.listView1.Items.Add(QueryBuilderUpdate.ToString());
                                                        this.listView1.Items[this.listView1.Items.Count - 1].EnsureVisible();
                                                        if (QueryBuilderUpdate.ToString().Contains("Error"))
                                                            this.listView1.Items[this.listView1.Items.Count - 1].ForeColor = System.Drawing.Color.Red;
                                                        Application.DoEvents();
                                                        SW.WriteLine(QueryBuilderUpdate.ToString() + ";");
                                                        if (IsTest == false) conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, QueryBuilderUpdate.ToString());

                                                        if (TempSourceDT.TableName == "DOC_TYPE")
                                                        {
                                                            //NewEntities.Add(Int64.Parse(r1["DOC_TYPE_ID"].ToString()));
                                                        }

                                                        if (TempSourceDT.TableName == "INDEX_R_DOC_TYPE")
                                                        {
                                                            //NewIndexsInEntities.Add(new NewIndex(Int64.Parse(r1["INDEX_ID"].ToString()), Int64.Parse(r1["DOC_TYPE_ID"].ToString())));
                                                        }

                                                        if (TempSourceDT.TableName == "DOC_INDEX")
                                                        {
                                                            //NewIndexs.Add(new NewIndex(Int64.Parse(r1["INDEX_ID"].ToString()), r1["Script"].ToString(), Int32.Parse(r1["Index_Len"].ToString()), (IndexDataType)Int32.Parse(r1["Index_Type"].ToString()), (IndexAdditionalType)Int32.Parse(r1["DropDown"].ToString())));
                                                        }

                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    if (ex.Message.Contains("Primary") || ex.Message.Contains("unique"))
                                                    {
                                                        this.listView1.Items.Add(ex.Message + ": " + QueryBuilderUpdate.ToString());
                                                        //                                    conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, QueryBuilderUpdate.ToString());
                                                    }
                                                    else if (ex.Message.Contains("parent key not found"))
                                                    {
                                                        this.listView1.Items.Add(ex.Message + ": " + QueryBuilderUpdate.ToString());
                                                    }
                                                    else
                                                    {
                                                        ZClass.raiseerror(ex);
                                                        this.listView1.Items.Add(ex.Message);
                                                        Application.DoEvents();
                                                    }
                                                }

                                                QueryBuilderUpdate.Remove(0, QueryBuilderUpdate.Length);

                                            }
                                            else
                                            {
                                                //proceso no definido
                                            }



                                        }
                                    }


                                }
                            }
                            catch (Exception ex)
                            {
                                ZClass.raiseerror(ex);
                                if (ex.Message.Contains("table or view does not exist"))
                                {
                                    if (MessageBox.Show("La Tabla : " + Item.Key + " no existe, desea crearla?", "Tabla no existe", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                    {
                                        if (Item.Value.Script.Length > 0)
                                        {

                                        }
                                    }

                                }
                                //  MessageBox.Show("Se produjo un error al exportar la tabla: " + Item.Key);
                            }


                            if (this.chktest.Checked == false)
                            {
                                // DialogResult dr = MessageBox.Show("Desea aplicar commit para las tablas pendientes o hacerlo al final o cancelar el commit?", "Commit Parcial", MessageBoxButtons.YesNoCancel);
                                //if (dr == DialogResult.Yes)
                                //{
                                transaction.Commit();
                                transaction = new Transaction(conDestiny);

                                //}
                                //else if (dr == DialogResult.No)
                                //{
                                //}
                                //else
                                //{
                                //    transaction.Rollback();
                                //    transaction = new Transaction(conDestiny);
                                //}
                            }
                            else
                            {
                                transaction.Rollback();
                                transaction = new Transaction(conDestiny);
                            }
                        }

                    }




                    if (IsTest)
                        transaction.Rollback();
                    else
                    {
                        DialogResult drfinal = MessageBox.Show("Finalizo el Proceso, Desea Realizar Commit de todo el proceso o cancelar?", "Commit Final", MessageBoxButtons.YesNo);
                        if (drfinal == DialogResult.Yes)
                        {
                            transaction.Commit();
                            #region PostScripts

                            foreach (Int64 EntityId in NewEntities)
                            {
                                Zamba.Servers.Server.CreateTables().AddDocsTables(EntityId);
                            }

                            foreach (NewIndex NI in NewIndexsInEntities)
                            {
                                IIndex Index = Zamba.Core.IndexsBusiness.GetIndexById(NI.IndexId, string.Empty);
                                NI.IndexType = Index.Type;
                                NI.IndexLen = Index.Len;
                                if (!IndexsBusiness.getReferenceStatus(NI.DocTypeId, NI.IndexId))
                                {
                                    if (DocTypesBusiness.CheckColumn(NI.DocTypeId, NI.IndexId, NI.IndexType, NI.IndexLen) == false)
                                    {
                                        DocTypesBusiness.AddColumn(NI.DocTypeId, NI.IndexId, NI.IndexType, NI.IndexLen);
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Se agrega el indice Id: {0} en  DocTypeId: {1}.", NI.IndexId, NI.DocTypeId));
                                    }
                                    else
                                    {
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("El indice Id: {0} existe en la entidad DocTypeId: {1}.", NI.IndexId, NI.DocTypeId));
                                    }

                                }
                                else
                                {
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("El indice Id: {0} DocTypeId: {1} es referencial.", NI.IndexId, NI.DocTypeId));
                                }

                            }

                            foreach (NewIndex NI in NewIndexs)
                            {
                                if (NI.DropDown == IndexAdditionalType.AutoSustitución || NI.DropDown == IndexAdditionalType.AutoSustituciónJerarquico)
                                {
                                    if (NI.Script.Length > 0)
                                    {
                                        IndexsBusiness.addindexsust(NI.IndexId, NI.VistaoTabla, NI.ColumnaCodigo, NI.ColumnaDescripcion, NI.Script);
                                    }
                                    else
                                    {
                                        IndexsBusiness.createsustituciontable(NI.IndexId, NI.IndexLen, (IndexDataType)Enum.Parse(typeof(IndexDataType), NI.IndexType.ToString()));
                                    }
                                }
                                else if (NI.DropDown == IndexAdditionalType.DropDown || NI.DropDown == IndexAdditionalType.DropDownJerarquico)
                                {
                                    if (NI.Script.Length > 0)
                                    {
                                        IndexsBusiness.addindexlist(NI.IndexId, NI.VistaoTabla, NI.ColumnaCodigo, NI.ColumnaDescripcion, NI.Script);
                                    }
                                    else
                                    {
                                        IndexsBusiness.addindexlist(NI.IndexId, NI.IndexLen);
                                    }
                                }
                                else
                                {
                                }

                                //TODO: Falta agregar la relacion de las DOC_I a las listas
                            }



                            if (chkAfterScripts.Checked == true)
                            {
                                AfterDs.ReadXml(Application.StartupPath + "\\AfterScripts.xml");

                                foreach (DataRow dr in AfterDs.Tables[0].Rows)
                                {
                                    ZScript script = new ZScript(Int64.Parse(dr[0].ToString()), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
                                    SW.WriteLine(script.Consulta + ";");
                                    if (IsTest == false) UpdaterBusiness.ExecuteQuery(conDestiny, script.Consulta);
                                }

                                AfterDs.Clear();
                                AfterDs.Dispose();
                                AfterDs = null;
                            }
                            #endregion

                        }
                        else
                        {
                            transaction.Rollback();
                        }



                    }

                    MessageBox.Show("Proceso Finalizado");


                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    transaction.Rollback();
                    this.listView1.Items.Add(ex.Message);
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {

                ZClass.raiseerror(ex);
                transaction.Rollback();
                this.listView1.Items.Add(ex.Message);
                Application.DoEvents();
            }
            finally
            {
                if (transaction != null)
                    transaction.Dispose();

                SW.Flush();
                SW.Close();
                SW.Dispose();
            }
        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void zButton14_Click(object sender, EventArgs e)
        {
            Boolean IsTest = this.chktest.Checked;
            StreamWriter SW = new StreamWriter(Path.Combine(temppath, "SQLOut" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + ".txt"));
            try
            {
                if (this.txtoldid.Text.Length > 0)
                {
                    Int64 newAttributeId = 0;
                    Int64 oldAttributeId = Int64.Parse(this.txtoldid.Text);
                    IIndex currentIndexd = IndexsBussinesExt.getIndex(oldAttributeId, true);

                    if (this.txtnewid.Text.Length > 0)
                    {
                        newAttributeId = Int64.Parse(this.txtnewid.Text);
                    }
                    else
                    {
                        newAttributeId = Zamba.Core.CoreBusiness.GetNewID(IdTypes.DOCINDEXID);
                        string querymaxdocindex = "select max(index_id) maxid from doc_index";

                        object maxidD = conDestiny.ExecuteScalar(CommandType.Text, querymaxdocindex);
                        object maxidS = conDestiny.ExecuteScalar(CommandType.Text, querymaxdocindex);

                        if (Int64.Parse(maxidD.ToString()) > newAttributeId || Int64.Parse(maxidS.ToString()) > newAttributeId)
                        {
                            newAttributeId = (Int64.Parse(maxidD.ToString()) > Int64.Parse(maxidS.ToString()) ? Int64.Parse(maxidD.ToString()) + 1 : Int64.Parse(maxidS.ToString()) + 1);
                            Zamba.Core.CoreBusiness.SetNewID(IdTypes.DOCINDEXID, newAttributeId);

                        }

                    }

                    try
                    {
                        string querydocindex = string.Format("update doc_index set index_id = {0} where index_id = {1}", newAttributeId, oldAttributeId);

                        conDestiny.ExecuteNonQuery(CommandType.Text, querydocindex);
                    }
                    catch (Exception) { }
                    try
                    {

                        string queryR = string.Format("select distinct doc_type_id from index_r_doc_type where index_id = {0}", oldAttributeId);

                        DataSet dsR = conDestiny.ExecuteDataset(CommandType.Text, queryR);

                        foreach (DataRow r in dsR.Tables[0].Rows)
                        {
                            string change4 = string.Format("alter table DOC_I{0} rename column i{1} to i{2}", r["doc_type_id"].ToString(), oldAttributeId, newAttributeId);
                            SW.WriteLine(change4 + ";");
                            if (IsTest == false) conDestiny.ExecuteNonQuery(CommandType.Text, change4);
                        }

                        if (currentIndexd.DropDown == IndexAdditionalType.DropDown || currentIndexd.DropDown == IndexAdditionalType.DropDownJerarquico)
                        {
                            string change4 = string.Format("rename table ILST_I{0} to ILST_I{1}", oldAttributeId, newAttributeId);
                            SW.WriteLine(change4 + ";");
                            if (IsTest == false) conDestiny.ExecuteNonQuery(CommandType.Text, change4);

                        }
                        if (currentIndexd.DropDown == IndexAdditionalType.AutoSustitución || currentIndexd.DropDown == IndexAdditionalType.AutoSustituciónJerarquico)
                        {
                            string change4 = string.Format("rename table ILST_I{0} to ILST_I{1}", oldAttributeId, newAttributeId);
                            SW.WriteLine(change4 + ";");
                            if (IsTest == false) conDestiny.ExecuteNonQuery(CommandType.Text, change4);

                        }
                    }
                    catch (Exception) { }
                    try
                    {
                        string querydocindex2 = string.Format("update index_r_doc_type set index_id = {0} where index_id = {1} and doc_type_id not in (select doc_type_id from  index_r_doc_type where index_id = {0})", newAttributeId, oldAttributeId);

                        conDestiny.ExecuteNonQuery(CommandType.Text, querydocindex2);
                    }
                    catch (Exception) { }
                    try
                    {

                        string changezvir = string.Format("update zvir set indexid = {0} where indexid = {1}", newAttributeId, oldAttributeId);
                        SW.WriteLine(changezvir + ";");
                        if (IsTest == false) conDestiny.ExecuteNonQuery(CommandType.Text, changezvir);
                    }
                    catch (Exception) { }

                    try
                    {
                        string queryDI3 = string.Format("update ZIR set indexid ={0} where indexid = {1}", newAttributeId, oldAttributeId);
                        string queryDI4 = string.Format("update wfruleparamitems set c_value = replace (c_value,'//{1}|','//{0}|') where   item =1 and c_value like '%//{1}|%' and rule_id in (select id from wfrules where class = 'DOGenerateTaskResult')", newAttributeId, oldAttributeId);
                        string queryDI5 = string.Format("update wfruleparamitems set c_value = replace (c_value,'|{1}|','|{0}|') where   item =0 and c_value like '%|{1}|%'  and rule_id in (select id from wfrules where class = 'IfIndex')", newAttributeId, oldAttributeId);
                        string queryDI6 = string.Format("update wfruleparamitems set c_value = replace (c_value,'|{1}|','|{0}|') where   item =0 and c_value like '%|{1}|%'  and rule_id in (select id from wfrules where class = 'DoFillIndex')", newAttributeId, oldAttributeId);
                        string queryDI7 = string.Format("update wfruleparamitems set c_value = replace (c_value,'{1}','{0}') where   item =0 and c_value = '{1}'  and rule_id in (select id from wfrules where class = 'IfIndex')", newAttributeId, oldAttributeId);
                        string queryDI8 = string.Format("update wfruleparamitems set c_value = replace (c_value,'{1}','{0}') where   item =0 and c_value = '{1}'  and rule_id in (select id from wfrules where class = 'DoFillIndex')", newAttributeId, oldAttributeId);
                        string queryDI9 = string.Format("update wfruleparamitems set c_value = replace (c_value,'i{1} ','i{0} ') where    c_value like '%i{1} %'", newAttributeId, oldAttributeId);
                        string queryDI10 = string.Format("update wfruleparamitems set c_value = replace (c_value,'I{1} ','I{0} ') where   c_value like '%I{1} %'", newAttributeId, oldAttributeId);
                        string queryDI11 = string.Format("update wfruleparamitems set c_value = replace (c_value,'I{1},','I{0},') where   c_value like '%I{1},%'", newAttributeId, oldAttributeId);
                        string queryDI12 = string.Format("update wfruleparamitems set c_value = replace (c_value,'I{1}=','I{0}=') where   c_value like '%I{1}=%'", newAttributeId, oldAttributeId);
                        string queryDI13 = string.Format("update wfruleparamitems set c_value = replace (c_value,'i{1},','i{0},') where   c_value like '%i{1},%'", newAttributeId, oldAttributeId);
                        string queryDI14 = string.Format("update wfruleparamitems set c_value = replace (c_value,'i{1}=','i{0}=') where   c_value like '%i{1}=%'", newAttributeId, oldAttributeId);
                        try
                        {
                            SW.WriteLine(queryDI3 + ";");
                            if (IsTest == false) conDestiny.ExecuteNonQuery(CommandType.Text, queryDI3);
                        }
                        catch (Exception) { }
                        try
                        {
                            SW.WriteLine(queryDI4 + ";");
                            if (IsTest == false) conDestiny.ExecuteNonQuery(CommandType.Text, queryDI4);
                        }
                        catch (Exception) { }
                        try
                        {
                            SW.WriteLine(queryDI5 + ";");
                            if (IsTest == false) conDestiny.ExecuteNonQuery(CommandType.Text, queryDI5);
                        }
                        catch (Exception) { }
                        try
                        {
                            SW.WriteLine(queryDI6 + ";");
                            if (IsTest == false) conDestiny.ExecuteNonQuery(CommandType.Text, queryDI6);
                        }
                        catch (Exception) { }
                        try
                        {
                            SW.WriteLine(queryDI7 + ";");
                            if (IsTest == false) conDestiny.ExecuteNonQuery(CommandType.Text, queryDI7);
                        }
                        catch (Exception) { }
                        try
                        {
                            SW.WriteLine(queryDI8 + ";");
                            if (IsTest == false) conDestiny.ExecuteNonQuery(CommandType.Text, queryDI8);
                        }
                        catch (Exception) { }
                        try
                        {
                            SW.WriteLine(queryDI9 + ";");
                            if (IsTest == false) conDestiny.ExecuteNonQuery(CommandType.Text, queryDI9);
                        }
                        catch (Exception) { }
                        try
                        {
                            SW.WriteLine(queryDI10 + ";");
                            if (IsTest == false) conDestiny.ExecuteNonQuery(CommandType.Text, queryDI10);
                        }
                        catch (Exception) { }
                        try
                        {
                            SW.WriteLine(queryDI11 + ";");
                            if (IsTest == false) conDestiny.ExecuteNonQuery(CommandType.Text, queryDI11);
                        }
                        catch (Exception) { }
                        try
                        {
                            SW.WriteLine(queryDI12 + ";");
                            if (IsTest == false) conDestiny.ExecuteNonQuery(CommandType.Text, queryDI12);
                        }
                        catch (Exception) { }
                        try
                        {
                            SW.WriteLine(queryDI13 + ";");
                            if (IsTest == false) conDestiny.ExecuteNonQuery(CommandType.Text, queryDI13);
                        }
                        catch (Exception) { }
                        try
                        {
                            SW.WriteLine(queryDI14 + ";");
                            if (IsTest == false) conDestiny.ExecuteNonQuery(CommandType.Text, queryDI14);
                        }
                        catch (Exception) { }
                    }
                    catch (Exception) { }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                SW.Flush();
                SW.Close();
                SW.Dispose();

            }
        }

        private void Chktest_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void ZButton15_Click(object sender, EventArgs e)
        {
            try
            {

                string script = this.txtnewid.Text.Trim();
                //                IDataReader dr = conSource.ExecuteReader(CommandType.Text, "select 'update wfdocument set step_id = ' || step_id || ', do_state_id = ' || do_state_id || ' where doc_id = ' || doc_id || ' and doc_type_id = ' || doc_type_id || '  and step_id = 1020010 ' from wfdocument where doc_type_id  in (18,26,112,2523,2528,2544,10122,1220023,2524) order by doc_id desc");
                // IDataReader dr = conSource.ExecuteReader(CommandType.Text, "select 'update wfdocument set step_id = ' || a.stepid || ', do_state_id = ' || s.doc_state_id || ' where doc_id = ' || a.doc_id || ' and doc_type_id = ' || a.doctypeid from wfstephst a inner join (select doc_id, max(to_char(fecha, 'dd/MM/YYYY HH:mm:ss')) fecha  from wfstephst group by doc_id) b on a.doc_id = b.doc_Id and b.fecha = to_char(a.fecha, 'dd/MM/YYYY HH:mm:ss') inner join wfstepstates s on s.name = a.state order by a.doc_id desc");
                IDataReader dr = conSource.ExecuteReader(CommandType.Text, script);

                Int64 i = 0;
                while (dr.Read())
                {

                    string q = dr.GetString(0);

                    conDestiny.ExecuteNonQuery(CommandType.Text, q);
                    i++;
                    this.txtoldid.Text = i.ToString();
                    Application.DoEvents();
                }
                MessageBox.Show("Ready");
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void BtnImplementFullWfs_Click(object sender, EventArgs e)
        {
            try
            {
                StreamWriter SW = new StreamWriter(Path.Combine(temppath, "SQLOut-WFFull" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + ".txt"));

                if (conDestiny == null || conDestiny.CN == null)
                    conDestiny = Zamba.Servers.Server.get_Con(apDestiny.SERVERTYPE, apDestiny.SERVER, apDestiny.DB, apDestiny.USER, apDestiny.PASSWORD, true, false);

                Transaction transaction = new Transaction(conDestiny);



                string WFs = this.wfs.Text;
                string DataBaseLink = this.txtdatabaselink.Text;

                if (WFs != string.Empty)
                {
                    if (chkRules.Checked)
                    {
                        conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, string.Format("create table bkpwfrules{0} as select * from wfrules", DateTime.Now.ToString("yyyy-MM-dd HH-mm")));
                        conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, string.Format("create table bkpzruleoptbase{0} as select * from zruleoptbase", DateTime.Now.ToString("yyyy-MM-dd HH-mm")));
                        conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, string.Format("create table bkpwfruleparamitems{0} as select * from wfruleparamitems", DateTime.Now.ToString("yyyy-MM-dd HH-mm")));
                    }
                    if (chkSteps.Checked)
                    {
                        conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, string.Format("create table bkWFstep{0} as select * from WFstep", DateTime.Now.ToString("yyyy-MM-dd HH-mm")));
                        conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, string.Format("create table bkWFstepstates{0} as select * from WFstepstates", DateTime.Now.ToString("yyyy-MM-dd HH-mm")));

                    }
                    if (chkWF.Checked)
                    {
                        conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, string.Format("create table bkWF_DT{0} as select * from WF_DT", DateTime.Now.ToString("yyyy-MM-dd HH-mm")));
                        conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, string.Format("create table bkWFworkflow{0} as select * from WFworkflow", DateTime.Now.ToString("yyyy-MM-dd HH-mm")));

                    }

                    if (chkRules.Checked)
                    {
                        conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, string.Format("delete wfruleparamitems where rule_id in (select id from wfrules where step_id in (select step_id from wfstep where work_id in (select work_id from wfworkflow where work_id in ({0}))))", WFs));
                        conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, string.Format("delete zruleoptbase where ruleid in (select id from wfrules where step_id in (select step_id from wfstep where work_id in (select work_id from wfworkflow where work_id in ({0}))))", WFs));
                        conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, string.Format("delete wfrules where step_id in (select step_id from wfstep where work_id in ( select work_id from wfworkflow where work_id in ({0})))", WFs));
                    }
                    if (chkSteps.Checked)
                    {
                        conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, string.Format("delete WFstepstates where step_id in (select step_id from wfstep where work_id in ( select work_id from wfworkflow where work_id in ({0})))", WFs));
                        conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, string.Format("delete WFstep where work_id in ( select work_id from wfworkflow where work_id in ({0}))", WFs));
                    }
                    if (chkWF.Checked)
                    {
                        conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, string.Format("delete WF_DT where work_id in ( select work_id from wfworkflow where work_id in ({0}))", WFs));
                        conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, string.Format("delete WFworkflow where work_id in ( select work_id from wfworkflow where work_id in ({0}))", WFs));
                    }



                    if (chkRules.Checked)
                    {
                        conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, string.Format("insert into wfrules select * from wfrules{1} where step_id in (select step_id from wfstep where work_id in ( select work_id from wfworkflow where work_id in ({0})))", WFs, DataBaseLink));
                        conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, string.Format("insert into zruleoptbase select * from zruleoptbase{1} where ruleid in (select id from wfrules where step_id in (select step_id from wfstep where work_id in ( select work_id from wfworkflow where work_id in ({0}))))", WFs, DataBaseLink));
                        conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, string.Format("insert into wfruleparamitems select * from wfruleparamitems{1} where rule_id in (select id from wfrules where step_id in (select step_id from wfstep where work_id in ( select work_id from wfworkflow where work_id in ({0}))))", WFs, DataBaseLink));


                    }
                    if (chkSteps.Checked)
                    {
                        conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, string.Format("insert into WFstep select * from WFstep{1} where step_id in (select step_id from wfstep where work_id in ( select work_id from wfworkflow where work_id in ({0})))", WFs, DataBaseLink));
                        conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, string.Format("insert into WF_DT select * from WF_DT{1} where step_id in (select step_id from wfstep where work_id in ( select work_id from wfworkflow where work_id in ({0})))", WFs, DataBaseLink));
                    }
                    if (chkWF.Checked)
                    {
                        conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, string.Format("insert into WFworkflow select * from WFworkflow{1} where work_id in ({0})", WFs, DataBaseLink));
                        conDestiny.ExecuteNonQuery(transaction.Transaction, CommandType.Text, string.Format("insert into WF_DT select * from WF_DT{1} where work_id in ( select work_id from wfworkflow where work_id in ({0}))", WFs, DataBaseLink));
                    }



                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void zButton16_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fi = new OpenFileDialog();
                fi.ShowDialog();
                string file = fi.FileName;
                StreamReader r = new StreamReader(file);

                string lastORG = "";
                Int32 count = 1;
                while (r.Peek() > -1)
                {
                    try
                    {

                        string line = r.ReadLine();
                        string orden = line.Split(char.Parse("|"))[0];

                        if (lastORG.Length == 0 || lastORG != orden)
                        {
                            count = 1;
                        }

                        string origen = line.Split(char.Parse("|"))[1];
                        string destino = line.Split(char.Parse("|"))[2] + count.ToString();

                        if (new FileInfo(destino).Exists)
                        {
                            count++;
                            destino = line.Split(char.Parse("|"))[2] + count.ToString();
                        }
                        else
                        {

                        }
                        File.Copy(origen, destino);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void zButton17_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fi = new OpenFileDialog();
                fi.ShowDialog();
                string file = fi.FileName;
                StreamReader r = new StreamReader(file);

                while (r.Peek() > -1)
                {
                    try
                    {
                        string line = r.ReadLine();

                        string origen = line;
                        string destino = line.Replace(@"\\10.6.110.127\c$\ZambaWeb", @"\\10.6.110.127\c$\ZambaWebUpdate");

                        if (new FileInfo(destino).Directory.Exists == false)
                        {
                            new FileInfo(destino).Directory.Create();
                        }

                        File.Copy(origen, destino);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }

                r.Close();
                r.Dispose();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
    }


    public class TableStructure
    {

        public TableStructure(string Table, string Filter, Boolean Enable, string Preserve, string Keys, string Unique, string Dependencies, string Ignorecompare, string Notupdate, string Notinsert)
        {
            this.name = Table;
            this.filter = Filter;
            this.keys = Keys;
            this.enabled = Enable;
            this.preserve = Preserve;
            this.uniques = Unique;
            this.dependencies = Dependencies;
            this.ignorecompare = Ignorecompare;
            this.notupdate = Notupdate;
            this.notinsert = Notinsert;


        }

        public string name { get; set; }
        public string filter { get; set; }
        public bool enabled { get; set; }
        public string preserve { get; private set; }
        public string keys { get; set; }
        public string uniques { get; internal set; }
        public string dependencies { get; private set; }

        public string ignorecompare { get; private set; }
        public string notupdate { get; private set; }
        public string notinsert { get; private set; }

        public string Script { get; internal set; }

        public string Compare { get; internal set; }
        public Int64 Equals { get; internal set; }
        public Int64 Inserts { get; internal set; }
        public Int64 Deletes { get; internal set; }
        public Int64 Updates { get; internal set; }
        public Int64 InsertNew { get; internal set; }
        public Int64 OnlyDestiny { get; internal set; }
        public Int64 ReCreateDestiny { get; internal set; }
        public Int64 ReCreateOrigin { get; internal set; }
    }


    internal class NewIndex
    {


        public NewIndex(long indexId, long docTypeId)
        {
            this.IndexId = indexId;
            this.DocTypeId = docTypeId;
        }

        public NewIndex(long indexId, string script, Int32 indexLen, IndexDataType indexType, IndexAdditionalType dropDown)
        {
            this.IndexId = indexId;
            this.Script = script;
            this.IndexLen = indexLen;
            this.IndexType = IndexType;
            this.DropDown = dropDown;

        }

        public Int64 DocTypeId { get; internal set; }
        public Int64 IndexId { get; internal set; }
        public IndexDataType IndexType { get; internal set; }
        public Int32 IndexLen { get; internal set; }

        public string VistaoTabla { get; internal set; }
        public string ColumnaCodigo { get; internal set; }
        public string ColumnaDescripcion { get; internal set; }
        public string Script { get; internal set; }
        public IndexAdditionalType DropDown { get; internal set; }
    }
}