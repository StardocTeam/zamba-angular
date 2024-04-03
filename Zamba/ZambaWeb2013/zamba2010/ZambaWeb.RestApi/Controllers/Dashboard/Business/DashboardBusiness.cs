using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;
using Zamba.Core;
using static ZambaWeb.RestApi.Controllers.AfipController;

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

    class Vacation
    {
        public string Id { get; set; }
        public string Names { get; set; }
        public string Cuil { get; set; }
        public string CuitCompany { get; set; }
        public string EmployeeNumber { get; set; }
        public string LengthOfService { get; set; }
        public string PeriodDays { get; set; }
        public string PendingDays { get; set; }
        public string TotalDays { get; set; }
        public DateTime VacationFromOption1 { get; set; }
        public DateTime VacationToOption1 { get; set; }
        public string RequestedDaysOption1 { get; set; }
        public string AuthorizeOption1 { get; set; }
        public DateTime VacationFromOption2 { get; set; }
        public DateTime VacationToOption2 { get; set; }
        public string RequestedDaysOption2 { get; set; }
        public string AuthorizeOption2 { get; set; }
        public string AuthorizedBy { get; set; }
        public string Comments { get; set; }
        public string RequestedPeriod { get; set; }
        public string ZambaUserId { get; set; }

        public Vacation() { }

        public static List<Vacation> MapDataTableToList(DataRowCollection DT)
        {
            List<Vacation> List = new List<Vacation>();

            for (int i = 0; i < DT.Count; i++)
            {
                Vacation rs = new Vacation();
                rs.Id = DT[i]["ID"].ToString();
                rs.Names = DT[i]["Nombres"].ToString();
                rs.Cuil = DT[i]["CUIL"].ToString();
                //rs.CuitCompany = DT[i]["CUIT Empresa"].ToString();
                //rs.EmployeeNumber = DT[i]["Nro Legajo"].ToString();
                //rs.LengthOfService = DT[i]["Antiguedad Laboral"].ToString();
                rs.PeriodDays = DT[i]["Dias del Periodo"].ToString();
                rs.PendingDays = DT[i]["Dias Pendientes"].ToString();
                rs.TotalDays = DT[i]["Dias Totales"].ToString();

                rs.VacationFromOption1 = (DateTime)DT[i]["Vacaciones Desde opcion 1"];
                rs.VacationToOption1 = (DateTime)DT[i]["Vacaciones Hasta opcion 1"];
                rs.RequestedDaysOption1 = DT[i]["Dias Solicitados opcion 1"].ToString();
                rs.AuthorizeOption1 = DT[i]["Autorizar Opcion 1"].ToString();


                rs.VacationFromOption2 = (DateTime)DT[i]["Vacaciones Desde opcion 2"];
                rs.VacationToOption2 = (DateTime)DT[i]["Vacaciones Hasta opcion 2"];
                rs.RequestedDaysOption2 = DT[i]["Dias Solicitados opcion 2"].ToString();
                rs.AuthorizeOption2 = DT[i]["Autorizar Opcion 2"].ToString();

                rs.AuthorizedBy = DT[i]["Autorizado por"].ToString();
                //rs.Comments = DT[i]["Observaciones"].ToString();
                rs.RequestedPeriod = DT[i]["Periodo Solicitado"].ToString();
                rs.ZambaUserId = DT[i]["User_Asigned"].ToString();

                List.Add(rs);
            }

            return List;
        }
    }

    class VacationDTO
    {
        public string AuthorizeOption { get; set; }
        public DateTime VacationFromOption { get; set; }
        public DateTime VacationToOption { get; set; }
        public string RequestedDaysOption { get; set; }
        public string TotalDays { get; set; }
    }
}