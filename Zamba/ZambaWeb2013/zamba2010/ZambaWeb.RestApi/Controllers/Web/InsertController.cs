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
using Newtonsoft.Json;
using System.Linq;
using Zamba.Core.Searchs;
using Zamba;
using System.Net.Http;
using Nelibur.ObjectMapper;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security;
using System.Globalization;
using System.Net;
using Zamba.Services;
using System.Web;
using ZambaWeb.RestApi.ViewModels;
using Zamba.Framework;
using Zamba.Membership;

namespace ZambaWeb.RestApi.Controllers
{
    [RoutePrefix("api/Insert")]
    public class InsertController : ApiController
    {
        #region Constructor&ClassHelpers
        public InsertController()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("ZambaWeb.RestApi");
            }
        }
        private Zamba.Core.IUser GetUser()
        {
            var user = TokenHelper.GetUser(User.Identity);
            return user;
        }
        #endregion

        [System.Web.Http.AcceptVerbs("POST")]
        [Route("Insert")]
        ///Adaptacion de views/insert.aspx.cs/InsertDoc
        public IHttpActionResult InsertDoc(InsertParamVM param)
        {
            try
            {
                var user = GetUser();
                if (user == null) return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidUser)));
                if (param.DocTypeId == 0 || param.Filenames.Count == 0) return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(StringHelper.BadInsertParameter)));

                Zamba.Core.ZOptBusiness zopt = new Zamba.Core.ZOptBusiness();
                List<IIndex> indices = new List<IIndex>();
                //Indice viene como objecto porque viene vacio si es 'Index'
                param.Indexs.ForEach(x => indices.Add((IIndex)JsonConvert.DeserializeObject<Zamba.Core.Index>(x.ToString())));

                List<IIndex> Indexs = new List<IIndex>();
                var insertVM = new List<InsertVM>();
                foreach (IIndex ind in indices)
                {
                    Indexs.Add(ind);
                    if ((ind.DropDown == IndexAdditionalType.AutoSustitución || ind.DropDown == IndexAdditionalType.AutoSustituciónJerarquico) &&
                               !string.IsNullOrEmpty(ind.Data) &&
                               string.IsNullOrEmpty(ind.dataDescription))
                    {
                        ind.dataDescription = new AutoSubstitutionBusiness().getDescription(ind.Data, ind.ID);
                    }
                }
                InsertResult res = InsertResult.NoInsertado;
                SResult sResult = new SResult();

                string doctypeidsexc = zopt.GetValue("DTIDShowDocAfterInsert");
                bool opendoc = false;
                Zamba.Core.UserPreferences UP = new Zamba.Core.UserPreferences();
                bool showDocAfterInsert = bool.Parse(UP.getValue("ShowDocAfterInsert", UPSections.InsertPreferences, "true", Zamba.Membership.MembershipHelper.CurrentUser.ID));

                foreach (string filename in param.Filenames)
                {
                    var insertResult = new InsertVM();
                    INewResult newresult = new SResult().GetNewNewResult(param.DocTypeId);
                    res = sResult.Insert(ref newresult, filename, param.DocTypeId, Indexs, MembershipHelper.CurrentUser.ID);
                    if (res == InsertResult.Insertado)
                    {
                        if (newresult.Disk_Group_Id > 0 &&
                            VolumesBusiness.GetVolumeType(newresult.Disk_Group_Id) != (int)VolumeType.DataBase)
                        {
                            //Guarda el documento en el volumen utilizando un webservice
                            string useWebService = zopt.GetValue("UseWebService");
                            string wsResultsUrl = ZOptBusiness.GetValueOrDefault("WSResultsUrl", "http://localhost/Zambaweb.wsservices/results.asmx");
                            if (!String.IsNullOrEmpty(useWebService) && bool.Parse(useWebService) && !String.IsNullOrEmpty(wsResultsUrl))
                            {
                                sResult.CopyBlobToVolumeWS(newresult.ID, newresult.DocTypeId);
                            }
                        }
                        //Codigo de ejecucion de reglas de entrada de la nueva tarea
                        STasks stasks = new STasks();
                        ITaskResult Task = stasks.GetTaskByDocId(newresult.ID);
                        if (!string.IsNullOrEmpty(doctypeidsexc))
                        {
                            foreach (string dtid in doctypeidsexc.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                if (string.Compare(dtid.Trim(), newresult.DocTypeId.ToString()) == 0)
                                    if (Task.ISVIRTUAL) opendoc = true; else opendoc = false;
                            }
                        }
                        insertResult.OpenDoc = showDocAfterInsert || opendoc;
                        insertResult.Success = true;
                        insertResult.NewResult.Id = newresult.ID;
                        insertResult.NewResult.Name = newresult.Name;
                        insertResult.NewResult.DocTypeId = newresult.DocTypeId;
                    }
                    else if (res == InsertResult.ErrorIndicesIncompletos || res == InsertResult.ErrorIndicesInvalidos)
                    {
                        insertResult.ErrorMessage = "Verificar indices incompletos y/o invalidos";
                    }
                    else if (res == InsertResult.NoInsertado)
                    {
                        insertResult.ErrorMessage += "Se produjo un error al insertar el documento: " + filename;
                    }
                    insertVM.Add(insertResult);
                }
                var js = JsonConvert.SerializeObject(insertVM);
                return Ok(js);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
            }
        }

        [System.Web.Http.AcceptVerbs("POST")]
        [Route("GetUserRightToInsert")]
        ///Adaptacion de views/insert.aspx.cs/InsertDoc
        public bool GetUserRightToInsert(long userid)
        {
            RightsBusiness RiB = new RightsBusiness();


            bool HasRightToInsert = RiB.GetUserRights(userid, ObjectTypes.InsertWeb, RightsType.View, -1);

            return HasRightToInsert;

        }

        [System.Web.Http.AcceptVerbs("POST")]
        [Route("ExecuteAutoComplete")]
        ///Adaptacion de views/insert.aspx.cs/InsertDoc
        public IHttpActionResult ExecuteAutoComplete(genericRequest request)
        {
            try
            {

                Int64 EntityId = Int64.Parse(request.Params["entityId"]);
                Int64 attributeId = Int64.Parse(request.Params["attributeId"]);
                string attributeValue = request.Params["attributeValue"];

                ZTrace.WriteLineIf(ZTrace.IsInfo, string.Format("Obteniendo autocompletar para Entidad: {0}, atributo {1} valor {2}", EntityId, attributeId, attributeValue));


                Hashtable haskindexs = AutocompleteBCBusiness.ExecuteAutoComplete(EntityId, attributeId, attributeValue);


                var js = JsonConvert.SerializeObject(haskindexs);
                return Ok(js);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
            }
        }

    }
}

