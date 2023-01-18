using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using Zamba.Core;
using Zamba;
using Newtonsoft.Json;
using Zamba.Framework;
using Zamba.Core.Cache;
using System.Collections;

namespace ZambaWeb.RestApi.Controllers.Customs
{
    public class RPController : ApiController
    {
        #region Constructor&ClassHelpers
        public RPController()
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
        #endregion


        // GET: api/RP
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/RP/11535116-12525
        public string Get(string id)
        {
            //base64            11535116-12525

            int userId = 0;
            int reportId = 0;

            var param = id.Split(char.Parse("-"));
            if (param.Length > 0)
            {
                reportId = int.Parse(param[0]);
            }
            if (param.Length > 1)
            {
                userId = int.Parse(param[1]);
            }

            //string query = "SELECT w.task_id, Q.ID,Q.PROVEEDOR,Q.MONEDA, TO_CHAR(Q.IMPORTE, '999G999G999G999D99MI')  IMPORTE ,Q.TIPO_GASTO,Q.CONCEPTO,Q.AREA, q.codigoa FROM(SELECT F.DOC_ID, F.i86 ID, F.I105 PROVEEDOR, F.I92 MONEDA, CASE WHEN F.I92 = 'PES' Then F.I109 ELSE F.I11535222 END  IMPORTE, G.DESCRIPCION TIPO_GASTO, C.DESCRIPCION CONCEPTO, a.descripcion AREA, F.I73 FECHA, f.i11535245 codigoa FROM doc_i26 f INNER JOIN slst_s126 a ON f.i126 = a.codigo INNER JOIN SLST_S110 G ON F.I110 = G.CODIGO INNER JOIN SLST_S2725 C ON F.I2725 = C.CODIGO UNION SELECT P.DOC_ID, P.i86 ID, P.I105 PROVEEDOR, P.I92 MONEDA, CASE WHEN P.I92 = 'PES' Then P.I109 ELSE P.I11535222 END  IMPORTE, G1.DESCRIPCION TIPO_GASTO, C1.DESCRIPCION CONCEPTO, a1.descripcion AREA, P.I73 FECHA, p.i11535245 codigoa FROM doc_i112 P INNER JOIN slst_s126 a1 ON P.i126 = a1.codigo INNER JOIN SLST_S110 G1 ON P.I110 = G1.CODIGO INNER JOIN SLST_S2725 C1 ON P.I2725 = C1.CODIGO) Q INNER JOIN WFDOCUMENT W ON Q.DOC_ID = W.DOC_ID AND W.doc_type_id IN(26,112) AND W.step_id = 1020003 WHERE (q.codigoa = 10053  or w.task_id in (select taskid from REQUESTACTIONTASKS where requestactionid = 10053))  ORDER BY FECHA ";
            //DataSet ds = Zamba.Servers.Server.get_Con().ExecuteDataset(System.Data.CommandType.Text, query);

            //var js = JsonConvert.SerializeObject(ds);
            //return js;


            //var user = GetUser(userId);
            //    if (user == null)
            //        throw new Exception("InvalidUser");
                try
                {
                    
                    string FormVariables = null;
                    Int64 TaskId = 0;
                    Hashtable dicFormVariables = new Hashtable();

                    List<string> AsociatedIds = new List<string>();
                //if (paramRequest.Params != null)
                //{
                //    reportId = Int32.Parse(paramRequest.Params["reportId"]);


                //    if (paramRequest.Params.ContainsKey("FormVariables"))
                //    {
                //        FormVariables = paramRequest.Params["FormVariables"];
                //        //                             TaskId = Int64.Parse(paramRequest.Params["parentTaskId"].ToString());
                //        //tomo los valores asignados desde el form en js
                //        /// Se convierte el valor en un diccionario para poder iterarlo

                //        List<itemVars> listFormVariables = JsonConvert.DeserializeObject<List<itemVars>>(FormVariables);

                //        if (listFormVariables == null)
                //        {
                //            listFormVariables = new List<itemVars>();
                //        }

                //        for (int i = 0; i < listFormVariables.Count; i++)
                //        {
                //            dicFormVariables.Add(listFormVariables[i].name, listFormVariables[i].value);
                //        }
                //    }
                //}

                          dicFormVariables.Add("x_usuario", userId);


                    Zamba.ReportBuilder.Business.ReportBuilderComponent RB = new Zamba.ReportBuilder.Business.ReportBuilderComponent();

                    var newresultss = RB.EvaluationRunWebQueryBuilder(reportId, true, dicFormVariables, null);
                    DataTable dtAsoc = newresultss.Tables[0];

                var newresults = JsonConvert.SerializeObject(dtAsoc);//, Formatting.Indented,
                    //new JsonSerializerSettings
                    //{
                    //    DateFormatString = "yyyy-MM-dd",
                    //    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    //});

                    return newresults;
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    throw new Exception("InvalidParameter");
                }
           

        }

        // POST: api/RP
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/RP/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/RP/5
        public void Delete(int id)
        {
        }
    }
}
