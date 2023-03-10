using System.Web.Http;
using System.Web.Http.Cors;
using Zamba.Core;
using System;
using System.Net.Http;
using System.Net;

using System.Collections.Generic;
using Newtonsoft.Json;
using Zamba.Framework;
using System.Linq;
using System.Data;

using System.Reflection;
using Zamba.FileTools;
using System.IO;

namespace ZambaWeb.RestApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/eInbox")]
    public class eInboxController : ApiController
    {

        public eInboxController()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("ZambaWeb.RestApi");
            }
        }


        [AcceptVerbs("GET", "POST")]
        [Route("getinbox")]
        public IHttpActionResult getinbox()
        {
            try
            {
                Assembly tt = Assembly.LoadFrom(Zamba.Membership.MembershipHelper.StartUpPath + "\\Spire\\Zamba.SpireTools.dll");
                System.Type t = tt.GetType("Zamba.SpireTools.EMail", true, true);

                ISpireEmailTools e = (ISpireEmailTools)Activator.CreateInstance(t);
                var emails = e.ReadInBox();
                return Ok(emails);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
            }
        }






        private void cleanRuleVariables_Emails()
        {
            foreach (string item in Enum.GetNames(typeof(cleanRuleVariables_ByConvention_Emails)))
            {
                if (VariablesInterReglas.ContainsKey(item))
                    VariablesInterReglas.Remove(item);
            }
        }

        enum cleanRuleVariables_ByConvention_Emails
        {
            From,
            To,
            Cc,
            ReplyTo,
            Sender,
            Date,
            Subject,
            IsRead,
            IsRecent,
            IsFlagged,
            IsAnswered,
            IsDeleted,
            IsDraft,
            SequenceNumber,
            Size,
            UniqueId,
            Attachments_Count,
            Attachments,
            Message
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

        [AcceptVerbs("GET", "POST")]
        [Route("InsertEmailsInZamba")]
        public IHttpActionResult InsertEmailsInZamba(genericRequest paramRequest)
        {
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se ha iniciado el proceso de insercion de correos.");

                //EL USUARIO LOGEADO EN LA APP DE ADMIN O EN EL SERVICIO SE DEBE ENVIAR
                IUser user = null;
                if (paramRequest != null)
                {
                    user = GetUser(paramRequest.UserId);
                    if (user == null)
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                            new HttpError(StringHelper.InvalidUser)));

                    Assembly tt = Assembly.LoadFrom(Zamba.Membership.MembershipHelper.StartUpPath + "\\Spire\\Zamba.SpireTools.dll");
                    System.Type t = tt.GetType("Zamba.SpireTools.EMail", true, true);
                    ISpireEmailTools e = (ISpireEmailTools)Activator.CreateInstance(t);

                    //GetProcessInfo
                    EmailBusiness EB = new EmailBusiness();
                    List<IDTOObjectImap> imapProcessList = new List<IDTOObjectImap>();

                    foreach (DataRow row in EB.getAllImapProcesses().Rows)
                    {
                        DTOObjectImap item = new DTOObjectImap(row);
                        imapProcessList.Add(item);
                    }

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Se ejecutara " + imapProcessList.Count + " proceso/s.");

                    e.InsertEmailsInZamba(imapProcessList, (Object)new Results_Business());

                    return Ok();
                }
                else
                {
                    ZTrace.WriteLineIf(ZTrace.IsWarning, "No hay parametros en la solicitud.");
                }

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable));
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                ZTrace.WriteLineIf(ZTrace.IsError, ex.ToString());
                throw new Exception(ex.ToString());
            }
        }





        [AcceptVerbs("GET", "POST")]
        [Route("ExecuteRuleForEmails")]
        public IHttpActionResult ExecuteRuleForEmails(genericRequest paramRequest)
        {
            try
            {
                //EL USUARIO LOGEADO EN LA APP DE ADMIN O EN EL SERVICIO SE DEBE ENVIAR
                IUser user = null;
                if (paramRequest != null)
                {
                    user = GetUser(paramRequest.UserId);
                    if (user == null)
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                            new HttpError(StringHelper.InvalidUser)));

                    //GetEmails   
                    Assembly tt = Assembly.LoadFrom(Zamba.Membership.MembershipHelper.StartUpPath + "\\Spire\\Zamba.SpireTools.dll");
                    System.Type t = tt.GetType("Zamba.SpireTools.EMail", true, true);

                    ISpireEmailTools e = (ISpireEmailTools)Activator.CreateInstance(t);
                    List<IListEmail> listEmail = e.GetEMailsFromServer(paramRequest.Params);

                    TasksController MyTaskcontroller = new TasksController();

                    foreach (ListEmail item in listEmail)
                    {
                        cleanRuleVariables_Emails();

                        VariablesInterReglas.Add("From", item.From);
                        VariablesInterReglas.Add("To", item.To);
                        VariablesInterReglas.Add("Cc", item.Cc);
                        //VariablesInterReglas.Add("ReplyTo", item.ReplyTo);
                        VariablesInterReglas.Add("Sender", item.Sender);
                        VariablesInterReglas.Add("Date", item.Date);
                        VariablesInterReglas.Add("Subject", item.Subject);

                        VariablesInterReglas.Add("IsRead", item.IsRead);
                        VariablesInterReglas.Add("IsRecent", item.IsRecent);
                        VariablesInterReglas.Add("IsFlagged", item.IsFlagged);
                        VariablesInterReglas.Add("IsAnswered", item.IsAnswered);
                        VariablesInterReglas.Add("IsDeleted", item.IsDeleted);
                        VariablesInterReglas.Add("IsDraft", item.IsDraft);
                        VariablesInterReglas.Add("SequenceNumber", item.SequenceNumber);
                        VariablesInterReglas.Add("Size", item.Size);
                        VariablesInterReglas.Add("UniqueId", item.UniqueId);

                        VariablesInterReglas.Add("Attachments_Count", item.Attachments_Count);
                        // VariablesInterReglas.Add("Attachments", item.Attachments);

                        VariablesInterReglas.Add("Message", item);

                        IHttpActionResult Obj_ExecutedRule = MyTaskcontroller.PrivEx(paramRequest);

                        // item.Attachments.Clear();
                    }


                    return Ok();
                }
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable));
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw new Exception(ex.ToString());
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
            }
        }
        [AcceptVerbs("GET", "POST")]
        [Route("GetMailBody")]
        public IHttpActionResult GetMailBody(genericRequest paramRequest)
        {
            int id = Convert.ToInt32(paramRequest.Params["ID"].ToString());
            SendMailConfig mail = MessagesBusiness.GetMessage(id);
            if (mail.OriginalDocument == null)
            {

                string cadena = File.ReadAllText(mail.OriginalDocumentFileName,System.Text.Encoding.UTF8);
                mail.Body = cadena;
            }
            else {

                mail.Body = System.Text.Encoding.UTF8.GetString(mail.OriginalDocument);

            }

            String Subject = mail.Subject;
            String CC = mail.Cc;
            String CCo = mail.Cco;
            String MailtTo = mail.MailTo;
            DateTime MailDateTime = mail.MailDateTime;
            String ResponseHTML = "";
            String TempPath = System.Configuration.ConfigurationManager.AppSettings["ZambaSofwareAppDataPath"] + "\\Temp";
            if (mail.OriginalDocumentFileName.EndsWith(".msg")){
                if (!Directory.Exists(TempPath))
                    Directory.CreateDirectory(TempPath);
                mail.OriginalDocumentFileName = "Z_MAIL_HISTORY_" + paramRequest.UserId + "_" + id.ToString() + ".msg";
                Stream writingStream = new FileStream(TempPath + "\\" + mail.OriginalDocumentFileName, System.IO.FileMode.Create);
                writingStream.Write(mail.OriginalDocument, 0, mail.OriginalDocument.Length);
                writingStream.Close();
                writingStream.Dispose();
            }
            else
            {
                mail.Body = mail.Body.Replace("<a ", "<a target='_blank' ");
            }
            return Ok(mail);
        }

        [AcceptVerbs("GET", "POST")]
        [Route("GetEmails")]
        public IHttpActionResult GetEmails(genericRequest paramRequest)
        {
            try
            {
                //EL USUARIO LOGEADO EN LA APP DE ADMIN O EN EL SERVICIO SE DEBE ENVIAR
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Iniciado obtenicion de correos para mostrar.");
                IUser user = null;
                if (paramRequest != null)
                {
                    user = GetUser(paramRequest.UserId);
                    if (user == null)
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                            new HttpError(StringHelper.InvalidUser)));

                    if (paramRequest.Params["GenericInbox"].ToString() == "true")
                    {

                        Zamba.Core.ZOptBusiness zopt = new Zamba.Core.ZOptBusiness();
                        string CasillaGenericaImap = zopt.GetValue("CasillaGenericaImap");

                        var newUser = CasillaGenericaImap + "\\" + paramRequest.Params["UserName"] + "\\" + paramRequest.Params["EMAIL"];
                        paramRequest.Params["UserName"] = newUser;

                    }

                    Assembly tt = Assembly.LoadFrom(Zamba.Membership.MembershipHelper.StartUpPath + "\\Spire\\Zamba.SpireTools.dll");
                    System.Type t = tt.GetType("Zamba.SpireTools.EMail", true, true);

                    ISpireEmailTools e = (ISpireEmailTools)Activator.CreateInstance(t);

                    List<IListEmail> listEmail = e.GetEMailsFromServer(paramRequest.Params);

                    string json = JsonConvert.SerializeObject(listEmail, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });

                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Correos obtenidos.");
                    return Ok(json);
                }
                else
                {
                    ZTrace.WriteLineIf(ZTrace.IsWarning, "No hay parametros en la solicitud.");
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError("No hay parametros disponibles")));
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.Message.ToString())));
                //throw new Exception(ex.ToString());
            }
        }



        [System.Web.Http.AcceptVerbs("POST")]
        [Route("ImportProcessImap")]
        public IHttpActionResult getListTable(genericRequest paramRequest)
        {
            if (paramRequest != null)
            {
                try
                {
                    if (paramRequest.Params != null)
                    {
                        EmailBusiness EB = new EmailBusiness();
                        DataTable result = EB.getAllImapProcesses();

                        var newresults = JsonConvert.SerializeObject(result, Formatting.Indented,
                       new JsonSerializerSettings
                       { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
                        return Ok(newresults);
                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
                }
            }
            return null;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("SaveImapProcess")]
        public IHttpActionResult SaveImapProcess(genericRequest paramRequest)
        {
            try
            {
                if (paramRequest != null)
                {
                    //var user = GetUser(paramRequest.UserId);
                    //if (user == null)
                    //    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                    //        new HttpError(StringHelper.InvalidUser)));
                    DTOObjectImap dTOObjectImap = new DTOObjectImap()
                    {
                        // SM: Panel 1
                        Id_proceso = -1,
                        Nombre_usuario = paramRequest.Params["USER_NAME"],
                        Nombre_proceso = paramRequest.Params["PROCESS_NAME"],
                        Correo_electronico = paramRequest.Params["EMAIL"],
                        Id_usuario = Convert.ToInt64(paramRequest.Params["USER_ID"]),
                        Password = paramRequest.Params["USER_PASSWORD"],
                        Is_Active = Convert.ToInt64(paramRequest.Params["IS_ACTIVE"]),
                        GenericInbox = Convert.ToInt64(paramRequest.Params["GenericInbox"]),

                        // SM: Panel 2
                        Direccion_servidor = paramRequest.Params["IP_ADDRESS"],
                        Puerto = Convert.ToInt64(paramRequest.Params["FIELD_PORT"]),
                        Protocolo = paramRequest.Params["FIELD_PROTOCOL"],
                        Filtrado = Convert.ToInt64(paramRequest.Params["HAS_FILTERS"]),

                        // SM: Panel 3
                        Carpeta = paramRequest.Params["FOLDER_NAME"],
                        CarpetaDest = paramRequest.Params["FOLDER_NAME_DEST"],
                        Filtro_campo = paramRequest.Params["FILTER_FIELD"] == null ? "" : paramRequest.Params["FILTER_FIELD"],
                        Filtro_valor = paramRequest.Params["FILTER_VALUE"] == null ? "" : paramRequest.Params["FILTER_VALUE"],
                        Filtro_recientes = Convert.ToInt64(paramRequest.Params["FILTER_RECENTS"]),
                        Filtro_noleidos = Convert.ToInt64(paramRequest.Params["FILTER_NOT_READS"]),
                        Exportar_adjunto_por_separado = Convert.ToInt64(paramRequest.Params["EXPORT_ATTACHMENTS_SEPARATELY"]),

                        // SM: Panel 4
                        Asunto = Convert.ToInt64(paramRequest.Params["SUBJECT"]),
                        Entidad = Convert.ToInt64(paramRequest.Params["ENTITY_ID"]),
                        Enviado_por = Convert.ToInt64(paramRequest.Params["SENT_BY"]),
                        Fecha = Convert.ToInt64(paramRequest.Params["FIELD_DATE"]),
                        Para = Convert.ToInt64(paramRequest.Params["FIELD_TO"])
                    };
                    // SM: Panel 4 (campos opcionales)
                    if (paramRequest.Params.ContainsKey("PROCESS_ID"))
                        dTOObjectImap.Id_proceso = Convert.ToInt64(paramRequest.Params["PROCESS_ID"]);

                    if (paramRequest.Params["CC"] != "")
                        dTOObjectImap.Cc = Convert.ToInt64(paramRequest.Params["CC"]);
                    if (paramRequest.Params["CCO"] != "")
                        dTOObjectImap.Cco = Convert.ToInt64(paramRequest.Params["CCO"]);
                    if (paramRequest.Params["FIELD_BODY"] != "")
                        dTOObjectImap.Body = Convert.ToInt64(paramRequest.Params["FIELD_BODY"]);
                    if (paramRequest.Params["Z_USER"] != "")
                        dTOObjectImap.Usuario_zamba = Convert.ToInt64(paramRequest.Params["Z_USER"]);
                    if (paramRequest.Params["CODE_MAIL"] != "")
                        dTOObjectImap.Codigo_mail = Convert.ToInt64(paramRequest.Params["CODE_MAIL"]);
                    if (paramRequest.Params["TYPE_OF_EXPORT"] != "")
                        dTOObjectImap.Tipo_exportacion = Convert.ToInt64(paramRequest.Params["TYPE_OF_EXPORT"]);

                    dTOObjectImap.Autoincremento = 1;

                    EmailBusiness EB = new EmailBusiness();
                    if (dTOObjectImap.Id_proceso != -1)
                        EB.UpdateImapProcess(dTOObjectImap);
                    else
                        EB.InsertObjectImap(dTOObjectImap);
                }
                return Ok("Se proceso correctamente");
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
            }

        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("SetProcessActiveState")]
        public IHttpActionResult SetProcessActiveState(genericRequest paramRequest)
        {
            try
            {
                if (paramRequest != null)
                {
                    if (paramRequest.Params.ContainsKey("PROCESS_ID"))
                    {
                        var isActive = Convert.ToInt64(paramRequest.Params["IS_ACTIVE"]);
                        var processId = Convert.ToInt64(paramRequest.Params["PROCESS_ID"]);

                        EmailBusiness EB = new EmailBusiness();
                        EB.SetProcessActiveState(processId, isActive);

                    }


                }
                return Ok("Se proceso correctamente");
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
            }

        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("DeleteProcessImap")]
        public IHttpActionResult DeleteProcessImap(genericRequest paramRequest)
        {
            try
            {
                if (paramRequest != null)
                {
                    if (paramRequest.Params.ContainsKey("processId"))
                    {
                        int processId = Convert.ToInt32(paramRequest.Params["processId"]);
                        EmailBusiness EB = new EmailBusiness();
                        EB.DeleteProcessImap(processId);
                    }
                }
                return Ok("Se proceso correctamente");
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
            }

        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("GetConection")]
        public IHttpActionResult GetConection(genericRequest paramRequest)
        {
            try
            {
                if (paramRequest.Params["GenericInbox"].ToString() == "true")
                {

                    Zamba.Core.ZOptBusiness zopt = new Zamba.Core.ZOptBusiness();
                    string CasillaGenericaImap = zopt.GetValue("CasillaGenericaImap");

                    var newUser = CasillaGenericaImap + "\\" + paramRequest.Params["UserName"] + "\\" + paramRequest.Params["EMAIL"];
                    paramRequest.Params["UserName"] = newUser;

                }

                Assembly tt = Assembly.LoadFrom(Zamba.Membership.MembershipHelper.StartUpPath + "\\Spire\\Zamba.SpireTools.dll");
                System.Type t = tt.GetType("Zamba.SpireTools.EMail", true, true);

                ISpireEmailTools e = (ISpireEmailTools)Activator.CreateInstance(t);
                e.ConnectToExchange(paramRequest.Params);


                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Comprobacion de conexion al Exchange exitosa.");
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new HttpError("Conexion exitosa!.")));
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsError, "[ERROR]: Fallo la conexion al Exchange:" + ex.ToString());
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.Message.ToString())));
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("GetProcess")]
        public IHttpActionResult GetProcess(genericRequest paramRequest)
        {
            try
            {
                if (paramRequest.Params != null)
                {
                    EmailBusiness EB = new EmailBusiness();
                    DataTable result = EB.getImapProcess(int.Parse(paramRequest.Params["Id"]));

                    var newresults = JsonConvert.SerializeObject(
                        result, Formatting.Indented,
                        new JsonSerializerSettings
                        {
                            PreserveReferencesHandling = PreserveReferencesHandling.Objects
                        });

                    return Ok(newresults);
                }

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new HttpError("Correos Obtenidos.")));
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
            }
        }

    }
}
