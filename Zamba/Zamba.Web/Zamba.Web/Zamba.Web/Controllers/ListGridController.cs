using System.Collections.Generic;
using System.Web.Http;
//using System.Web.Http.Cors;
using System.Data;
using System.Collections;
using ZambaWeb.RestApi.Models;
using Zamba.Core;
using Zamba.Filters;
using Zamba.Core.Search;
using System;
using System.Web.Http.Routing;
//using AttributeRouting;
//using AttributeRouting.Web;
//using AttributeRouting.Web.Http;
//using AttributeRouting.Web.Mvc;
using System.Net.Http;
using System.Net;
using Zamba;
using Newtonsoft.Json;
using Zamba.Core.Searchs;
using Nelibur.ObjectMapper;
using Zamba.Web.Helpers;
using System.Linq;
using Zamba.Services;
using static System.Web.HttpContext;
using System.Web.Configuration;
using System.Web;
using System.Web.Script.Serialization;

namespace ZambaWeb.RestApi.Controllers
{
    // [EnableCors(origins: "*", headers: "*", methods: "*")]


    public class ListGridController : ApiController
    {
        private const string DOC_TYPE_NAME_COLUMNNAME = "doc_type_name";
        private const string DOC_TYPE_ID_COLUMNNAME = "DOC_TYPE_ID";
        long _gridTotalCount;
        

        public int StepId
        {
            get
            {
                if (Session["StepId"] != null)
                    return int.Parse(Session["StepId"].ToString());
                else
                    return -1;
            }
        }


        private void SetLicenceConsume()
        {
            try
            {
                var user = Zamba.Membership.MembershipHelper.CurrentUser;
                SRights rights = new SRights();
                Int32 type = 0;

                if (user.WFLic)
                {
                    type = 1;
                }
                else
                {
                    SUserPreferences SUserPreferences = new SUserPreferences();
                    Ucm.changeLicDocToLicWF(user.ConnectionId, user.ID, user.Name, user.puesto, Int16.Parse(SUserPreferences.getValue("TimeOut", Sections.UserPreferences, "30")), 1);
                    SUserPreferences = null;
                    Zamba.Membership.MembershipHelper.CurrentUser.WFLic = true;
                    user.WFLic = true;
                    type = 1;
                }

                if (user.ConnectionId > 0)
                {
                    SUserPreferences SUserPreferences = new SUserPreferences();
                    //Ucm.UpdateOrInsertActionTime(user.ID, user.Name, user.puesto, user.ConnectionId, Int32.Parse(SUserPreferences.getValue("TimeOut", Sections.UserPreferences, "30")), type);
                    SUserPreferences = null;
                }

            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
            }
        }


        //ApplicationDBEntities ctx;
        public ListGridController()
        {
            // ctx = new ApplicationDBEntities();
        }

        // Get all orders
        [Route("GetListGrid")]
        //public  string GetListGrid()
        //{
        //    //var table = GetDataTable(WFId,StepId);
        //    JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        //    List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
        //    Dictionary<string, object> childRow;
        //    foreach (DataRow row in table.Rows)
        //    {
        //        childRow = new Dictionary<string, object>();
        //        foreach (DataColumn col in table.Columns)
        //        {
        //            childRow.Add(col.ColumnName, row[col]);
        //        }
        //        parentRow.Add(childRow);
        //    }
        //    return jsSerializer.Serialize(parentRow);
        //}

        // Get all orders
        [Route("Orders")]
        public List<OrderManager> GetOrders()
        {

        [Route("GetListGrid/{filter}/{value}")]
        public string GetListGridByCustFilter(string filter, string value)
        {
            var table = new DataTable();
            var t = GetDataTable();
            if (filter != string.Empty && filter != "-")
            {
             var rows = from myRow in t.AsEnumerable()
               where myRow[filter].ToString().Contains(value)
               select myRow;              
                if (rows.Any())
                    table = rows.CopyToDataTable();
            }
            else { 
             // t.DefaultView.RowFilter = value;
                table = t;
            }

        // Get Orders based on Criteria
        [Route("Orders/{filter}/{value}")]
        public List<OrderManager> GetOrdersByCustName(string filter, string value)
        {
            List<OrderManager> Res = new List<OrderManager>();
            //var a = GetDataTable();
            //switch (filter)
            //{
            //    case "CustomerName":
            //        Res = (from c in ctx.OrderManagers
            //               where c.CustomerName.StartsWith(value)
            //               select c).ToList();
            //        break;
            //    case "MobileNo":
            //        Res = (from c in ctx.OrderManagers
            //               where c.CustomerMobileNo.StartsWith(value)
            //               select c).ToList();
            //        break;
            //    case "SalesAgentName":
            //        Res = (from c in ctx.OrderManagers
            //               where c.SalesAgentName.StartsWith(value)
            //               select c).ToList();
            //        break;
            //}
            return Res;
        }
        public class OrderManager
        {
            public int OrderId { get; set; }
            public string CustomerName { get; set; }
            public string CustomerMobileNo { get; set; }
        }

        public int PageItemCount
        {
            get
            {
                return Int16.Parse(WebConfigurationManager.AppSettings["PageSize"]);
            }
        }

        public  DataTable GetDataTable(ref Int32 WFId, ref Int32 StepId)
        {
            // cmbDocType.Enabled = false;
            //pnlFilters.Visible = false;

            LoadDocTypes(StepId);

            STasks sTasks = new STasks();
            //ArrayList dtSelected = getDocTypeSelected();
            //_gridTotalCount = 0;

            //Cargamos la tabla de resultados y con ella la cantidad de los mismos harcoded 1 cambiar
            //            DataTable dt = sTasks.getTasksAsDT(StepId, dtSelected, 1, 1, Filter, ref _gridTotalCount);
            ArrayList a = new ArrayList();
            a.Add(1028);
            //_gridTotalCount = 0;
            //long first = 1070;
            DataTable dt = sTasks.getTasksAsDT(StepId, a, 1, 50, null, ref _gridTotalCount);
            //var addd = _gridTotalCount;

            //ArrayList a = new ArrayList();
            //a.Add((long)11074);
            //_gridTotalCount = 0;
            //long first = 11102;
            //DataTable dt = sTasks.getTasksAsDT(first, a, 1, 50, null, ref _gridTotalCount);
            //var addd = _gridTotalCount;
           // HttpContext.Current.Session["User"] = "algo";
           //var a=(IUser)Current.Session["User"];
           // System.Web.UI.Page page = (System.Web.UI.Page)HttpContext.Current.Handler;
           // var aaaa=page.Session["User"];

            DataTable table = new DataTable();
            table.Columns.Add("Dosage", typeof(int));
            table.Columns.Add("Drug", typeof(string));
            table.Columns.Add("Patient", typeof(string));
            table.Columns.Add("Date", typeof(DateTime));

            // Here we add five DataRows.
            table.Rows.Add(25, "Indocin", "David", DateTime.Now);
            table.Rows.Add(50, "Enebrel", "Sam", DateTime.Now);
            table.Rows.Add(10, "Hydralazine", "Christoff", DateTime.Now);
            table.Rows.Add(21, "Combivent", "Janet", DateTime.Now);
            table.Rows.Add(100, "Dilantin", "Melanie", DateTime.Now);
            return dt;   
        }
        public IFiltersComponent Filter
        {
            get
            {
                //if (Current.Session["FC"] != null)
                //    return (IFiltersComponent)Current.Session["FC"];
                //else
                return null;
            }
        }
        private void LoadDocTypes(long stepid)
        {
            sDocType sDocType = new sDocType();
            HttpContext.Current.Session["User"] = "algo";
            DataTable dt = sDocType.GetDocTypesByWfStepAsDT(stepid, ((IUser)Current.Session["User"]).ID);

            //cmbDocType.DataTextField = DOC_TYPE_NAME_COLUMNNAME;
            //cmbDocType.DataValueField = DOC_TYPE_ID_COLUMNNAME;
            //cmbDocType.DataSource = dt;
            //cmbDocType.DataBind();

            ZOptBusiness zopt = new ZOptBusiness();

            if (zopt.GetValue("WebViewDocTypesInTaskGrid") != null)
            {
                if (bool.Parse(zopt.GetValue("WebViewDocTypesInTaskGrid")) && dt.Rows.Count > 1)
                {
                    // cmbDocType.Enabled = true;
                    //pnlFilters.Visible = true;

                    if ((decimal)dt.Rows[dt.Rows.Count - 1][DOC_TYPE_ID_COLUMNNAME] != 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr[DOC_TYPE_ID_COLUMNNAME] = 0;
                        dr[DOC_TYPE_NAME_COLUMNNAME] = "Todas las tareas";
                        dt.Rows.Add(dr);
                    }
                }
                else
                {
                    // cmbDocType.Enabled = false;
                    //pnlFilters.Visible = false;
                }
            }
            else
            {
                //  cmbDocType.Enabled = false;
                //pnlFilters.Visible = false;
            }
            zopt = null;
        }
        public int StepId
        {
            get
            {
                if (HttpContext.Current.Session["StepId"] != null)
                    return int.Parse(HttpContext.Current.Session["StepId"].ToString());
                else
                    return -1;
            }
        }

        /// <summary>
        /// Etapa anterior
        /// </summary>
        public int PrevStepId
        {
            get
            {
                if (HttpContext.Current.Session["PrevStepId"] != null)
                    return int.Parse(HttpContext.Current.Session["PrevStepId"].ToString());
                else
                    return -1;
            }
            set
            {
                HttpContext.Current.Session["PrevStepId"] = value;
            }
        }

        /// <summary>
        /// Filtros utilizados
        /// </summary>
        //public IFiltersComponent Filter
        //{
        //    get
        //    {
        //        if (Session["FC"] != null)
        //            return (IFiltersComponent)Session["FC"];
        //        else
        //            return null;
        //    }
        //}

        private ArrayList getDocTypeSelected()
        {
            ArrayList docTypeIds = new ArrayList();

            //if (cmbDocType.SelectedValue != null && !string.IsNullOrEmpty(cmbDocType.SelectedValue.ToString()))
            //{
            //    if (long.Parse(cmbDocType.SelectedValue.ToString()) == 0)
            //    {
            //        foreach (ListItem item in cmbDocType.Items)
            //        {
            //            if (long.Parse(item.Value) > 0)
            //            {
            //                docTypeIds.Add(long.Parse(item.Value));
            //            }
            //        }
            //    }
            //    else
            //    {
            //        docTypeIds.Add(long.Parse(cmbDocType.SelectedValue.ToString()));
            //    }
            //}

            Current.Session["docTypeIds"] = docTypeIds;

            return docTypeIds;
        }

    }

}
