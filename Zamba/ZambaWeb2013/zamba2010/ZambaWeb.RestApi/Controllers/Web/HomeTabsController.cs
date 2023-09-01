using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Data;
using System.Collections;
using ZambaWeb.RestApi.Models;
using Zamba.Core;
using Zamba.Filters;
using Zamba.Core.Search;
using System;
using System.Text;
using Zamba.Services;
using Zamba;
using System.Web;
using Zamba.Core.WF.WF;
using System.Web.Http.Results;
using Newtonsoft.Json;
using ZambaWeb.RestApi.ViewModels;
using System.Net.Http;
using System.Net;
using System.Linq;
using Zamba.Core.Enumerators;
using Zamba.Framework;
using Zamba.Data;
using ZambaWeb.RestApi.Controllers.Web;
using ZambaWeb.RestApi.Controllers.Class;
using Zamba.FileTools;
using System.IO;
using Zamba.Membership;
using System.Security.Cryptography.X509Certificates;
using static ZambaWeb.RestApi.Controllers.SearchController;
using ZambaWeb.RestApi.AuthorizationRequest;

namespace ZambaWeb.RestApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/HomeTabs")]
    [RequestResponseController]
    //[Authorize]
    public class HomeTabsController : ApiController
    {


        #region Constantes

        private const string DISKGROUPIDCOLUMNNAME = "DISK_GROUP_ID";
        private const string DOC_ID_COLUMNNAME = "Doc_ID";
        private const string DOCID_COLUMNNAME = "DocId";
        private const string DOC_FILE_COLUMNNAME = "DOC_FILE";
        private const string DOC_TYPE_ID_COLUMNNAME = "DOC_TYPE_ID";
        private const string DISK_VOL_ID_COLUMNNAME = "disk_Vol_id";
        private const string DISK_VOL_PATH_COLUMNNAME = "DISK_VOL_PATH";
        private const string DO_STATE_ID_COLUMNNAME = "Do_State_ID";
        private const string DOC_TYPE_NAME_COLUMNNAME = "doc_type_name";
        private const string IMAGEN_COLUMNNAME = "Imagen";
        private const string PLATTER_ID_COLUMNNAME = "PLATTER_ID";
        private const string VOL_ID_COLUMNNAME = "VOL_ID";
        private const string OFFSET_COLUMNNAME = "OFFSET";




        private const string ICON_ID_COLUMNNAME = "ICON_ID";
        private const string SHARED_COLUMNNAME = "SHARED";
        private const string VER_PARENT_ID_COLUMNNAME = "ver_Parent_id";
        private const string VERSION_COLUMNNAME = "version";
        private const string ROOTID_COLUMNNAME = "RootId";
        private const string ORIGINAL_FILENAME_COLUMNNAME = "original_Filename";
        private const string NUMEROVERSION_COLUMNNAME = "NumeroVersion";
        private const string NUMERO_DE_VERSION_COLUMNNAME = "numero de version";
        private const string CRDATE_COLUMNNAME = "crdate";
        private const string NAME1_COLUMNNAME = "NAME1";
        private const string STEP_ID_COLUMNNAME = "Step_Id";
        private const string ICONID_COLUMNNAME = "IconId";
        private const string CHECKIN_COLUMNNAME = "CheckIn";
        private const string TASK_ID_COLUMNNAME = "Task_ID";
        private const string WFSTEPID_COLUMNNAME = "WfStepId";
        private const string ASIGNADO_COLUMNNAME = "Asignado";
        private const string STATE_COLUMNNAME = "State";
        private const string ESTADO_TAREA_COLUMNNAME = "Estado";
        private const string SITUACION_COLUMNNAME = "Situacion";
        private const string EXPIREDATE_COLUMNNAME = "ExpireDate";
        private const string VENCIMIENTO_COLUMNNAME = "Vencimiento";
        private const string NAME_COLUMNNAME = "Name";
        private const string TASK_STATE_ID_COLUMNNAME = "task_state_id";
        private const string INGRESO_COLUMNNAME = "Ingreso";
        private const string USER_ASIGNED_COLUMNNAME = "User_Asigned";
        private const string USER_ASIGNED_BY_COLUMNNAME = "User_Asigned_By";
        private const string DATE_ASIGNED_BY_COLUMNNAME = "Date_asigned_By";
        private const string REMARK_COLUMNNAME = "Remark";
        private const string TAG_COLUMNNAME = "Tag";
        private const string DOCTYPEID_COLUMNNAME = "DoctypeId";
        private const string TASKCOLOR_COLUMNNAME = "TaskColor";
        private const string VER_COLUMNNAME = "Ver";
        private const string WORK_ID_COLUMNNAME = "Work_Id";
        private const string NOMBRE_DOCUMENTO_COLUMNNAME = "Nombre Documento";
        private const string SOLICITUD_COLUMNNAME = "Solicitud";

        #endregion
        #region Variables Privadas

        private string nombreDocumento_currUserConfig;
        private string imagen_currUserConfig;
        private string ver_currUserConfig;
        private string EstadoTarea_currUserConfig;
        private string Asignado_currUserConfig;
        private string Situacion_currUserConfig;
        private string Nombre_Original_currUserConfig;
        private string TipoDocumento_currUserConfig;

        private string version_UserConfig;
        private string NroVersion_UserConfig;

        private string FechaCreacion_currUserConfig;
        private string FechaModificacion_currUserConfig;

        private Zamba.Core.ITaskResult iTaskResult;
        RightsBusiness RB = new RightsBusiness();
        UserGroupBusiness UserGroupBusiness = new UserGroupBusiness();

        #endregion



        public HomeTabsController()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("ZambaWeb.RestApi");
            }

        }


        public Zamba.Core.IUser GetUser(long? userId)
        {
            try
            {

                var user = TokenHelper.GetUser(User.Identity);

                UserBusiness UBR = new UserBusiness();

                if (userId.HasValue && userId > 0 && user == null)
                {
                    user = UBR.ValidateLogIn(userId.Value, ClientType.WebApi);
                }

                if (user == null && Request != null && Request.Headers.Count() > 0 && Request.Headers.Contains("UserId") && Request.Headers.GetValues("UserId").Count() > 0)
                {
                    Int64 UserId = Int64.Parse(Request.Headers.GetValues("UserId").FirstOrDefault());
                    if (UserId > 0)
                    {
                        user = UBR.ValidateLogIn(UserId, ClientType.WebApi);
                    }
                }

                if (user == null)
                {
                    string fullUrl = Request.Headers.GetValues("Referer").FirstOrDefault();
                    string[] urlInPieces = fullUrl.Split('&')[0].Split('/');
                    string dataItem = null;
                    foreach (string item in urlInPieces)
                    {
                        if (item.Contains("User"))
                        {
                            dataItem = item;
                        }
                    }


                    string urlPart = dataItem != null ? dataItem.Split('&')[0].Split('=')[1] : "0";

                    if (user == null && Request != null && Int64.Parse(urlPart) > 0) // && Request.Headers.Count() > 0 && Request.Headers.Contains("UserId") && Request.Headers.GetValues("UserId").Count() > 0
                    {
                        //Int64 UserId = Int64.Parse(Request.Headers.GetValues("UserId").FirstOrDefault());
                        Int64 UserIdFromUrl = Int64.Parse(urlPart);
                        if (UserIdFromUrl > 0)
                        {
                            user = UBR.ValidateLogIn(UserIdFromUrl, ClientType.WebApi);
                        }
                    }
                }


                UBR = null;

                return user;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return null;
            }
        }


        public partial class ListNews
        {
            //public Int64 Id { get; set; }
            public string Name { get; set; }
            public string crdate { get; set; }
            public string value { get; set; }

            public ListNews(string Name, string crdate, string value)
            {
                //this.Id = id;
                this.Name = Name;
                this.crdate = crdate;
                this.value = value;
            }
        }


        [HttpPost]
        [Route("GetGenericSummary")]
        [RestAPIAuthorize(isNewsPostDto = true)]
        public IHttpActionResult GetGenericSummary(NewsPostDto newsDto)
        {
            try
            {
                if (newsDto == null)
                    return BadRequest("Objeto request nulo");

                if (newsDto.UserId <= 0)
                    return BadRequest("Id de usuario debe ser mayor a cero");

                var searchType = (NewsSearchType)Enum.Parse(typeof(NewsSearchType), newsDto.SearchType.ToUpper());
                GridType SelectedgridType = GridType.News;
                List<TaskDTO> newsList = null;

                switch (newsDto.GridType)
                {
                    case "recents":
                        SelectedgridType = GridType.Recents;
                        newsList = new NewsBusiness().GetRecentTasks(newsDto.UserId);
                        break;
                    case "favorites":
                        SelectedgridType = GridType.Favorite;
                        newsList = new DocumentLabelsBusiness().GetBookmarks(newsDto.UserId);
                        break;
                    case "bookmarks":
                        SelectedgridType = GridType.Importants;
                        newsList = new DocumentLabelsBusiness().GetStars(newsDto.UserId);
                        break;
                    case "history":
                        SelectedgridType = GridType.History;
                        break;
                    case "mytasks":
                        SelectedgridType = GridType.Asigned;
                        newsList = new WFTaskBusiness().GetMyTasks(newsDto.UserId);
                        break;
                    case "lastsearch":
                        SelectedgridType = GridType.LastSearch;
                        //newsList = new WFTaskBusiness().GetLastSearch(newsDto.UserId);
                        break;
                        //case "mydash":
                        //    SelectedgridType = GridType.Asigned;
                        //    newsList = new WFTaskBusiness().GetTasksAverageTimeByStep(newsDto.UserId);
                        //    break;
                }
                newsList = newsList.OrderByDescending(x => x.Fecha).ToList();
                return Ok(newsList);

            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
                return InternalServerError(new Exception("Error al obtener el listado de novedades"));
            }
        }


        [HttpPost]
        [Route("GetNewsSummary")]
        [RestAPIAuthorize(isNewsPostDto = true)]
        public IHttpActionResult GetNewsSummary(NewsPostDto newsDto)
        {
            try
            {
                if (newsDto == null)
                    return BadRequest("Objeto request nulo");

                if (newsDto.UserId <= 0)
                    return BadRequest("Id de usuario debe ser mayor a cero");

                var searchType = (NewsSearchType)Enum.Parse(typeof(NewsSearchType), newsDto.SearchType.ToUpper());

                List<News> newsList = new NewsBusiness().GetNewsSummary(newsDto.UserId, searchType);
                return Ok(newsList);

            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
                return InternalServerError(new Exception("Error al obtener el listado de novedades"));
            }
        }

        public class NewsPostDto
        {
            public long UserId { get; set; }
            public string SearchType { get; set; }
            public string GridType { get; set; }

        }

        [HttpPost]
        [Route("SetNewsRead")]
        public IHttpActionResult SetNewsRead(SetNewsReadPostDto setReadDto)
        {
            try
            {
                if (setReadDto == null)
                    return BadRequest("Objeto request nulo");

                if (setReadDto.NewsId <= 0)
                    return BadRequest("Id de documento debe ser mayor a cero");

                new NewsBusiness().SetRead(setReadDto.NewsId);

                return Ok();
            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
                return InternalServerError(new Exception("Error al obtener el listado de novedades"));
            }
        }

        public class SetNewsReadPostDto
        {
            public long NewsId { get; set; }
        }



        [AcceptVerbs("GET", "POST")]
        [Route("GetNewsResults")]
        public List<ListNews> GetNewsResults(genericRequest paramRequest)
        {
            UserBusiness UB = new UserBusiness();
            string docid = paramRequest.Params["docid"];
            List<ListNews> NovedadesList = new List<ListNews>();
            try
            {

                StringBuilder QueryString = new StringBuilder();
                QueryString.Append("SELECT u.name Usuario, n.crdate Fecha, n.c_value Novedad FROM ZNEWS n inner join usrtable u on u.id= n.userid where docid =" + docid);

                string query = string.Format(QueryString.ToString());
                var dataSet = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, query);
                DataTable firstTable = dataSet.Tables[0];

                foreach (DataRow row in firstTable.Rows)
                {
                    NovedadesList.Add(new ListNews(row["USUARIO"].ToString(), row["FECHA"].ToString(), row["NOVEDAD"].ToString()));
                }
            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
            }
            finally { UB = null; }
            return NovedadesList;
        }

        public string validateParamRequest(string paramData)
        {
            string data = null;
            if (paramData == null)
            {
                data = string.Empty;
            }
            else
            {
                data = paramData;
            }
            return data;
        }

        public class Group
        {
            public long ID { get; set; }
            public string Name { get; set; }
        }


        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("GetUsers")]
        public List<BaseImageFileResult> GetUsers(long stepId)
        {

            try
            {
                List<BaseImageFileResult> usrList = WFBusiness.GeUsersOnlUsersByUserIdAndRightsType(stepId);
                return usrList;
            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
                return null;
            }

        }

        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("GetGroups")]
        public List<BaseImageFileResult> GetGroups(long stepId)
        {
            try
            {
                List<BaseImageFileResult> Groups = WFBusiness.GetGroupsUserGroupsIdsByStepID(stepId);
                return Groups;
            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
                return null;
            }

        }


    }


}


