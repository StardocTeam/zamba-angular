using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using Zamba.Core;

namespace ZambaWeb.RestApi.Controllers.Dashboard.Business
{
    public class DashboardBusiness

    {
    }
    public class TaskDTORRHH : TaskDTO
    {
        public TaskDTORRHH(string Tarea, long Task_id, long doc_id, long DOC_TYPE_ID, DateTime Fecha, string Etapa, string Asignado, DateTime Ingreso, DateTime Vencimiento)
            : base(Tarea, Task_id, doc_id, DOC_TYPE_ID, Fecha, Etapa, Asignado, Ingreso, Vencimiento)
        {

        }
        public TaskDTORRHH(string Tarea, long Task_id, long doc_id, long DOC_TYPE_ID, DateTime Fecha, string Etapa, string Asignado, DateTime Ingreso, DateTime Vencimiento, string url)
  : base(Tarea, Task_id, doc_id, DOC_TYPE_ID, Fecha, Etapa, Asignado, Ingreso, Vencimiento)
        {
            this.url = url;
        }
        public string url { get; set; }
    }
    public class DashBoardTools
    {
        public string GetZambaWebDomain(HttpRequestMessage Request)
        {
            return Request.RequestUri.Scheme + "://" + Request.RequestUri.Authority + System.Configuration.ConfigurationManager.AppSettings["ThisDomain"];
        }
        public string GetURLTask(TaskDTO Task, HttpRequestMessage request, long userid)
        {
            string url = "";
            if (Task.Task_id > 0)
            {
                url = GetZambaWebDomain(request) + "/views/WF/TaskViewer.aspx?" +
                    "DocType=" + Task.DOC_TYPE_ID
                    + "&docid=" + Task.doc_id
                    + "&taskid=" + Task.Task_id
                    + "&mode=s" + "&s=0"
                    + "&userId=" + userid.ToString();
            }
            else
            {
                url = GetZambaWebDomain(request) + "/views/search/docviewer.aspx?" +
                    "DocType=" + Task.DOC_TYPE_ID
                    + "&docid=" + Task.doc_id
                    + "&mode=s" + "&s=0"
                    + "&userId=" + userid.ToString();
            }
            return url;
        }
    }

    public class app
    {
        public string name { get; set; }
        public string description { get; set; }
    }

    public class User
    {
        public string name { get; set; }
        public string avatar { get; set; }
        public string email { get; set; }
    }

    public class MenuItem
    {
        public string text { get; set; }
        public string i18n { get; set; }
        public bool group { get; set; }
        public bool hideInBreadcrumb { get; set; }
        public List<MenuItem> children { get; set; } = new List<MenuItem>();
        public string icon { get; set; }
        public string link { get; set; }
    }

    public class menu
    {
        public List<MenuItem> items { get; set; }
    }

    public class rootObject
    {
        public app app { get; set; }
        public User user { get; set; }
        public menu menu { get; set; }
    }

    public class CarouselConfigDTO
    {
        public string DotPosition { get; set; }
        public int EnableSwipe { get; set; }
        public int AutoPlaySpeed { get; set; }
        public int AutoPlay { get; set; }
        public int Loop { get; set; }
    }
}