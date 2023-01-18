using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Zamba.AppBlock;
using Zamba.Core;
using Zamba.Services;
using System.Web;
using Zamba;

namespace Views.Aysa.GEC
{
    public partial class GeneralReportStandAloneGEC : Page
    {
        private const string PAGELOCATION = "Aysa/GEC/GeneralReportStandAlone.aspx{0}";
        private  string TASKSELECTORURL = "WF/TaskSelector.ashx?DocId={0}&DocTypeId={1}&userId=" + Zamba.Membership.MembershipHelper.CurrentUser.ID.ToString();
        private IUser _user;

        UserPreferences UP = new UserPreferences();

        #region GetIconsUrl

        protected string GetFilterIcon()
        {
            return RadAjaxLoadingPanel.GetWebResourceUrl(Page, "Telerik.Web.UI.Skins.Vista.Grid.Filter.gif");
        }

        protected string GetExcelIcon()
        {
            return "../../Content/images/icons/3.png";
        }

        protected string GetCsvIcon()
        {
            return "../../Content/images/icons/8.png";
        }

        protected string GetWordIcon()
        {
            return "../../Content/images/icons/2.png";
        }

        protected string GetPdfIcon()
        {
            return "../../Content/images/icons/4.png";
        }

        protected string GetOpenTaskIcon()
        {
            return "../../Content/images/icons/30.png";
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataSource2.ConnectionString = Zamba.Servers.Server.get_Con(false, true, false).ConString;

            //Actualiza el timemout
            if (Page.IsPostBack) return;
            else
            {
                hdnIds.Value = cbEntidades.Items[0].Value;
                cbEntidades.SelectedIndex = 0;
                LoadEntityOptions();
            }

            if (Zamba.Membership.MembershipHelper.CurrentUser == null)
            {
                FormsAuthentication.RedirectToLoginPage();
                return;
            }

            try
            {
                SZOptBusiness zopt = new SZOptBusiness();

                string title = zopt.GetValue("WebViewTitle");
                zopt = null;
                Title = (string.IsNullOrEmpty(title)) ? "Zamba Software" : title + " - Zamba Software";
                _user = (IUser) Zamba.Membership.MembershipHelper.CurrentUser;
                var rights = new SRights();
                Int32 type = 0;
                if (_user.WFLic) type = 1;
                if ( _user.ConnectionId > 0)
                {
                    var UserPreferences = new UserPreferences();
                    Ucm.UpdateOrInsertActionTime(_user.ID, _user.Name, _user.puesto, _user.ConnectionId,
                                                    Int32.Parse(UP.getValue("TimeOut",
                                                                                          UPSections.UserPreferences, "30")),
                                                    type);
                    UserPreferences = null;
                }
                else
                    Response.Redirect("~/Views/Security/LogIn.aspx");
                rights = null;
            }
            catch (Exception ex)
            {
                ZException.Log(ex);
            }
        }

        private void LoadEntityOptions()
        {
            ZOptBusiness zopt = new ZOptBusiness();
            string zoptItmes = zopt.GetValue("GeneralReportItems");
            zopt = null;
            if (!string.IsNullOrEmpty(zoptItmes))
            {
                cbEntidades.Items.Clear();
                string[] splitedItems = zoptItmes.Split('|');
                cbEntidades.Items.AddRange((from item in splitedItems
                                            select new ListItem(item.Split('=')[0], item.Split('=')[1])).ToArray<ListItem>());
            }
        }

        protected void RadGrid1ItemCommand(object source, GridCommandEventArgs e)
        {
            String query = string.Empty;
            try
            {
                BindGrid(out query);

                RunCommand(e);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                ZClass.raiseerror(new Exception("Query: " + query));
            }
        }

        private void RunCommand(GridCommandEventArgs e)
        {
            if (e.CommandName == "FilterRadGrid")
            {
                RadFilter1.FireApplyCommand();
            }

            if (e.CommandName == "ExportToExcel")
            {
                ConfigureExport();
                RadGrid1.MasterTableView.ExportToExcel();
            }

            if (e.CommandName == "ExportToCSV")
            {
                ConfigureExport();
                RadGrid1.MasterTableView.ExportToCSV();
            }

            if (e.CommandName == "ExportToWord")
            {
                ConfigureExport();
                RadGrid1.MasterTableView.ExportToWord();
            }

            if (e.CommandName == "ExportToPDF")
            {
                ConfigureExport();
                RadGrid1.MasterTableView.ExportToPdf();
            }

            if (e.CommandName == "OpenTask")
            {
                OpenTask();
            }

            if (e.CommandName == RadGrid.ExportToExcelCommandName ||
                e.CommandName == RadGrid.ExportToWordCommandName ||
                e.CommandName == RadGrid.ExportToCsvCommandName)
            {
                ConfigureExport();
            }
        }

        protected String FormatStrQuery()
        {
            if (!string.IsNullOrEmpty(hdnIds.Value))
            {
                LoadHiddenZoptColumns();
                string[] ids = hdnIds.Value.Split(Char.Parse(","));

                string query = "Select ";

                String firstDocTable = string.Empty;
                String fromQuery = String.Empty;

                var indexs = new Dictionary<long, string>();

                foreach (String id in ids)
                {
                    if (id == string.Empty) continue;

                    Int64 eId = Int64.Parse(id);
                    String docTable = "Doc_I" + id;

                    if (firstDocTable == String.Empty) firstDocTable = docTable;

                    var whereSlts = new StringBuilder();

                    DataSet ds = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset(CommandType.Text,
                                                                                                 "Select INDEX_R_DOC_TYPE.INDEX_ID, DOC_INDEX.INDEX_NAME, DOC_INDEX.DROPDOWN from index_r_doc_type INNER JOIN DOC_INDEX on index_r_doc_type.index_id = doc_index.index_id where doc_type_id = " +
                                                                                                 id);

                    IEnumerable<DataRow> rows = from DataRow r in ds.Tables[0].Rows
                                  where !indexs.ContainsKey(Int64.Parse(r["INDEX_ID"].ToString()))
                                  select r;

                    //Si el doctype es "documentos gec" o "documentos,manuales y procedimientos"
                    if (eId == 11080 || eId == 11059)
                    {
                        query += "step.name [Etapa],state.name [Estado], ";
                        whereSlts.AppendLine(" inner join wfdocument wfdoc on wfdoc.doc_id = " + docTable + ".doc_id and doc_type_id =" + eId);
                        whereSlts.AppendLine(" inner join wfstep step on step.step_id = wfdoc.step_id");
                        whereSlts.AppendLine(" inner join wfstepstates state on state.doc_state_id = wfdoc.do_state_id");
                    }

                    if (eId == 11080 || eId == 11059 || eId == 11062)
                    {
                        query += " I11279 [Nombre Documentacion], ";
                    }

                    foreach (DataRow r in rows.Where(r => Int64.Parse(r["INDEX_ID"].ToString()) != 1229 &&
                        !_hiddenColumns.Contains(Int64.Parse(r["INDEX_ID"].ToString()))))
                    {
                        indexs.Add(Int64.Parse(r["INDEX_ID"].ToString()), r["INDEX_NAME"].ToString().Trim());

                        //Si el dropdown es 2 o 4 es porque son SLST
                        if (r["DROPDOWN"].ToString().Trim() == "2" || r["DROPDOWN"].ToString().Trim() == "4")
                        {
                            query += docTable + ".I" + r["INDEX_ID"] + " as [Codigo " +
                                     r["INDEX_NAME"].ToString().Trim() + "],";
                            query += "SLST_S" + r["INDEX_ID"] + ".Descripcion as [" +
                                     r["INDEX_NAME"].ToString().Trim() + "],";

                            whereSlts.Append(" left join SLST_S");
                            whereSlts.Append(r["INDEX_ID"]);
                            whereSlts.Append(" on convert(varchar,SLST_S");
                            whereSlts.Append(r["INDEX_ID"]);
                            whereSlts.Append(".codigo)=");
                            whereSlts.Append(docTable);
                            whereSlts.Append(".I" + r["INDEX_ID"]);
                        }
                        else
                        {
                            query += docTable + ".I" + r["INDEX_ID"] + " as [" +
                                     r["INDEX_NAME"].ToString().Trim() + "],";
                        }
                    }

                    if (fromQuery.Contains("from"))
                    {
                        fromQuery += " left join " + docTable;
                        fromQuery += " on " + docTable + ".i11292 = " + firstDocTable + ".i11292" + whereSlts;
                    }
                    else
                    {
                        fromQuery += " from " + docTable + whereSlts;
                    }
                }

                query = query.Remove(query.Length - 1);

                if (ids.Length > 0)
                    return query + ", doc_i" + ids[0] + ".doc_id as IDZamba " + fromQuery;
                else
                    return string.Empty;
            }
            return string.Empty;
        }

        IEnumerable<long> _hiddenColumns;
        private void LoadHiddenZoptColumns()
        {
            ZOptBusiness zopt = new ZOptBusiness();
            string hiddenColumns = zopt.GetValue("GeneralReportHiddenColumns");
            zopt = null;
            if (!string.IsNullOrEmpty(hiddenColumns))
            {
                _hiddenColumns = from item in hiddenColumns.Split('|')
                                 select long.Parse(item);
            }
        }

        protected void Header1SkinChanged(object sender, SkinChangedEventArgs e)
        {
            //Required for dynamic skin changing
            RadGrid1.Rebind();
        }

        protected void RadToolBar1ButtonClick(object sender, RadToolBarEventArgs e)
        {
            //
        }

        protected void RadGrid1NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            String query = string.Empty;
            try
            {
                BindGrid(out query);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                ZClass.raiseerror(new Exception("Query: " + query));
            }
        }

        protected void RadGrid1Load(object sender, EventArgs e)
        {
            String query = string.Empty;
            try
            {
                BindGrid(out query);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                ZClass.raiseerror(new Exception("Query: " + query));
            }
        }

        protected void SetIds(int id)
        {
            if (!string.IsNullOrEmpty(hdnIds.Value))
            {
                String idsString = hdnIds.Value;
                if (idsString.Contains(id.ToString(CultureInfo.InvariantCulture)))
                {
                    hdnIds.Value = idsString.Replace(id.ToString(CultureInfo.InvariantCulture), String.Empty);
                    hdnIds.Value = hdnIds.Value.Replace(", ,", ",");
                    hdnIds.Value = hdnIds.Value.Replace(",,", ",");
                }
                else
                {
                    hdnIds.Value = idsString + "," + id;
                }
            }
            else
            {
                hdnIds.Value = id.ToString(CultureInfo.InvariantCulture);
            }
        }

        protected void BtnApplyEntitiesClick(object sender, EventArgs e)
        {
            String query = string.Empty;
            try
            {
                BindGrid(out query);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                ZClass.raiseerror(new Exception("Query: " + query));
            }
        }

        private void BindGrid(out string query)
        {
            query = FormatStrQuery();
            //Javier: Si es necesario aplicar restricciones, se ejecutará el select acá.

            //string whereQuery = GetWhereForRegion(_userID);

            //if (!(string.IsNullOrEmpty(Query) || Query.Contains("where")) && !string.IsNullOrEmpty(whereQuery))
            //{
            //    Query += " where " + whereQuery;
            //}

            SqlDataSource2.SelectCommand = query;

            SqlDataSource2.DataBind();
            RadGrid1.Rebind();
        }

        protected void RadFilter1PreRender(object sender, EventArgs e)
        {
            var menu = RadFilter1.FindControl("rfContextMenu") as RadContextMenu;
            if (menu == null) return;
            menu.DefaultGroupSettings.Height = Unit.Pixel(400);
            menu.EnableAutoScroll = true;
        }

        public void ConfigureExport()
        {
            RadGrid1.ExportSettings.ExportOnlyData = CheckBox1.Checked;
            RadGrid1.ExportSettings.IgnorePaging = CheckBox2.Checked;
            RadGrid1.ExportSettings.OpenInNewWindow = CheckBox3.Checked;
        }

        public int GetDocIdByIndexValue(int valor)
        {
            try
            {
                DataSet ds = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset(CommandType.Text,
                                                                                             "SELECT doc_id FROM Doc_I11080 WHERE I11292 = " +
                                                                                             valor);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return int.Parse(ds.Tables[0].Rows[0]["doc_id"].ToString());
                }
                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        protected void OpenTask()
        {
            //TO-DO: llamar al opener, y validar que sea el correcto.
            try
            {
                if (string.IsNullOrEmpty(hdnIdDocument.Value))
                    return;

                int docId = int.Parse(hdnIdDocument.Value);

                String script = String.Empty;

                if (docId != 0)
                {
                    string docName = new SResult().GetResultName(docId, Int32.Parse(cbEntidades.SelectedValue));
                    Uri currUrl = HttpContext.Current.Request.Url;

                    string urlToOpen = currUrl.ToString().Replace(string.Format(PAGELOCATION,currUrl.Query), string.Format(TASKSELECTORURL ,docId, cbEntidades.SelectedValue));

                    script = "OpenTaskInOpener('" + urlToOpen + "'," + docId + ",'" + docName + "');";
                }
                else
                {
                    script = "alert('Aun no existe documentacion para el ID de proyecto seleccionado');";
                }

                Page.ClientScript.RegisterStartupScript(typeof(Page), "DoOpenTaskScript", script, true);
                hdnIdDocument.Value = string.Empty;
            }
            catch
            {
            }
        }

        protected void Button1Click(object sender, EventArgs e)
        {
            OpenTask();
        }

        protected void Button2Click(object sender, EventArgs e)
        {
            ConfigureExport();
            RadGrid1.MasterTableView.ExportToExcel();
        }

        protected void Button3Click(object sender, EventArgs e)
        {
            ConfigureExport();
            RadGrid1.MasterTableView.ExportToCSV();
        }

        protected void Button4Click(object sender, EventArgs e)
        {
            ConfigureExport();
            RadGrid1.MasterTableView.ExportToWord();
        }

        protected void Button5Click(object sender, EventArgs e)
        {
            ConfigureExport();
            RadGrid1.MasterTableView.ExportToPdf();
        }

        private string GetWhereForRegion(long usrId)
        {
            var sbQuery = new StringBuilder();
            sbQuery.Append("select g.name as GroupName ");
            sbQuery.Append("from USR_R_GROUP usrG ");
            sbQuery.Append("left outer join USRGROUP G on G.id = usrG.GROUPID ");
            sbQuery.Append("where Usrid = ");
            sbQuery.Append(usrId.ToString(CultureInfo.InvariantCulture));

            DataSet ds = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset(CommandType.Text,
                                                                                         sbQuery.ToString());

            if (ds.Tables.Count > 0)
            {
                DataTable dtRegiones = ds.Tables[0];

                var regions = new List<string>();

                foreach (DataRow row in dtRegiones.Rows)
                {
                    if (row["GroupName"] != null)
                    {
                        string groupName = row["GroupName"].ToString();

                        switch (groupName)
                        {
                            case "DRCF":
                                regions.Add("Doc_I1027.I1181 = 1");
                                break;
                            case "DRN":
                                regions.Add("Doc_I1027.I1181 = 2");
                                break;
                            case "DRO":
                                regions.Add("Doc_I1027.I1181 = 5");
                                break;
                            case "DRSE":
                                regions.Add("Doc_I1027.I1181 = 3");
                                break;
                            case "DRSO":
                                regions.Add("Doc_I1027.I1181 = 4");
                                break;
                        }
                    }
                }
                sbQuery = new StringBuilder();
                if (regions.Count > 1)
                {
                    sbQuery.Append("(");
                    foreach (string item in regions)
                    {
                        sbQuery.Append(item);
                        sbQuery.Append(" OR ");
                    }
                    if (sbQuery.ToString().Trim().EndsWith("OR"))
                    {
                        sbQuery.Remove(sbQuery.ToString().LastIndexOf("OR ", StringComparison.Ordinal), 3);
                    }

                    sbQuery.Append(")");
                }
                else
                {
                    if (regions.Count > 0)
                    {
                        sbQuery.Append(regions[0]);
                    }
                }
            }

            return sbQuery.ToString();
        }

        #region EntityChecks

        protected void Chk11059CheckedChanged(object sender, EventArgs e)
        {
            SetIds(11059);
        }

        protected void Chk11062CheckedChanged(object sender, EventArgs e)
        {
            SetIds(11062);
        }

        protected void Chk11079CheckedChanged(object sender, EventArgs e)
        {
            SetIds(11079);
        }

        protected void Chk11080CheckedChanged(object sender, EventArgs e)
        {
            SetIds(11080);
        }

        #endregion

        protected void cbEntidadesChange(object sender, EventArgs e)
        {
            hdnIds.Value = cbEntidades.SelectedValue;
        }
    }
} 