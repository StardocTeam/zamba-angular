using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Zamba;
using Zamba.Core;
using Zamba.Data;
using Zamba.Framework;

namespace ZambaWeb.RestApi.Controllers.Barcode
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BarcodeController : ApiController
    {

        [AcceptVerbs("GET", "POST")]
        [Route("api/barcode/GenerateBarcode")]
        public IHttpActionResult GenerateBarcode(genericRequest paramRequest)
        {

            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Generate Barcode");

            foreach (string item in Enum.GetNames(typeof(EVariablesInterReglas_Convencion)))
            {
                if (VariablesInterReglas.ContainsKey(item))
                    VariablesInterReglas.Remove(item);
            }

            try
            {
                //AccountController AC = new AccountController();
                //AC.LoginById((int)paramRequest.UserId);

                //UserBusiness UBR = new UserBusiness();
                // IUser user = UBR.ValidateLogIn(paramRequest.UserId, ClientType.WebApi); 

                //if (user == null)
                //    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                //        new HttpError(StringHelper.InvalidUser)));

                if (paramRequest.Params.ContainsKey("entityId") && !string.IsNullOrEmpty(paramRequest.Params["entityId"]) &&
                paramRequest.Params.ContainsKey("indexs") && !string.IsNullOrEmpty(paramRequest.Params["indexs"]))
                {

                    Int64 entityId = Int64.Parse(paramRequest.Params["entityId"].ToString());
                    string data = paramRequest.Params["indexs"].ToString();
                    data = data.Trim().TrimStart(Char.Parse("{"));
                    data = data.Trim().Replace("\"Table\"", "");
                    data = data.Trim().TrimStart(Char.Parse(":"));
                    data = data.Trim().TrimEnd(Char.Parse("}"));
                    DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(data.Trim());
                    Results_Business RB = new Results_Business();

                    INewResult nr = RB.GetNewResult(entityId, string.Empty);
                    nr.Indexs = ZCore.GetInstance().FilterIndex(entityId);

                    foreach (DataRow r in dataTable.Rows)
                    {
                        try
                        {
                                    IIndex I = ((IResult)nr).get_GetIndexById(Int64.Parse(r["Id"].ToString()));
                                    if (I != null)
                                    {
                                        I.DataTemp = r["value"].ToString();
                                    }
                        }
                        catch (Exception ex)
                        {
                            ZClass.raiseerror(ex);
                        }

                    }

                    BarcodesBusiness BCB = new BarcodesBusiness();
                    Int64 BarcodeId = CoreData.GetNewID(IdTypes.Caratulas);
                    Int64 DocId = CoreData.GetNewID(IdTypes.DOCID);
                    nr.ID = DocId;
                    BCB.Insert(nr, entityId, paramRequest.UserId, BarcodeId, true);

                    return Ok(BarcodeId);

                }
                else
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidParameter)));
                }

            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
            }

        }


        [AcceptVerbs("GET", "POST")]
        [Route("api/barcode/replicateBarcode")]
        public IHttpActionResult replicateBarcode(genericRequest paramRequest)
        {

            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Generate Barcode");

            foreach (string item in Enum.GetNames(typeof(EVariablesInterReglas_Convencion)))
            {
                if (VariablesInterReglas.ContainsKey(item))
                    VariablesInterReglas.Remove(item);
            }

            try
            {
                //AccountController AC = new AccountController();
                //AC.LoginById(paramRequest.UserId);

                //UserBusiness UBR = new UserBusiness();
                // IUser user = UBR.ValidateLogIn(paramRequest.UserId, ClientType.WebApi); 

                //if (user == null)
                //    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                //        new HttpError(StringHelper.InvalidUser)));

                if (paramRequest.Params.ContainsKey("entityId") && !string.IsNullOrEmpty(paramRequest.Params["entityId"]) &&
                paramRequest.Params.ContainsKey("indexs") && !string.IsNullOrEmpty(paramRequest.Params["indexs"]))
                {

                    Int64 entityId = Int64.Parse(paramRequest.Params["entityId"].ToString());
                    Int64 barcodeId = Int64.Parse(paramRequest.Params["barcodeId"].ToString());

                    string data = paramRequest.Params["indexs"].ToString();
                    data = data.Trim().TrimStart(Char.Parse("{"));
                    data = data.Trim().Replace("\"Table\"", "");
                    data = data.Trim().TrimStart(Char.Parse(":"));
                    data = data.Trim().TrimEnd(Char.Parse("}"));
                    DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(data.Trim());
                    Results_Business RB = new Results_Business();

                    INewResult nr = RB.GetNewResult(entityId, string.Empty);
                    nr.Indexs = ZCore.GetInstance().FilterIndex(entityId);

                    foreach (DataRow r in dataTable.Rows)
                    {
                        try
                        {
                            IIndex I = ((IResult)nr).get_GetIndexById(Int64.Parse(r["Id"].ToString()));
                            if (I != null)
                            {
                                I.DataTemp = r["value"].ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                            ZClass.raiseerror(ex);
                        }

                    }

                    BarcodesBusiness BCB = new BarcodesBusiness();
                  //  Int64 BarcodeId = CoreData.GetNewID(IdTypes.Caratulas);
                    Int64 DocId = CoreData.GetNewID(IdTypes.DOCID);
                    nr.ID = DocId;
                    BCB.Insert(nr, entityId, paramRequest.UserId, barcodeId, true);

                    return Ok(barcodeId);

                }
                else
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidParameter)));
                }

            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
            }

        }




        [AcceptVerbs("GET", "POST")]
        [Route("api/barcode/GetHistory")]
        public IHttpActionResult GetHistory(genericRequest paramRequest)
        {

            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Hisotrial de Barcode");

            try
            {
                //AccountController AC = new AccountController();
                //AC.LoginById(paramRequest.UserId);

                BarcodesBusiness BCB = new BarcodesBusiness();
                DataTable DT = BCB.dsFilterCaratulas(paramRequest.UserId);

                var jsonDD = JsonConvert.SerializeObject(DT);
                return Ok(jsonDD);
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
            }

        }

    }


}
