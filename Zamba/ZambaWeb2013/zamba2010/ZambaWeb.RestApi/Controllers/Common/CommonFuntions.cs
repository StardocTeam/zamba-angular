using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Data;
using ZambaWeb.RestApi.Models;
using Zamba.Core;
using Zamba.Core.Search;
using System;
using Newtonsoft.Json;
using System.Linq;
using Zamba.Core.Searchs;
using Zamba;
using System.Net.Http;
using Nelibur.ObjectMapper;
using ZambaWeb.RestApi.ViewModels;
using System.Net;
using Zamba.Core.WF.WF;
using Zamba.Framework;
using Zamba.Data;
using Zamba.Services;
using Zamba.Core.Access;
using System.Globalization;
using Zamba.ReportBuilder;
using System.Collections;
using Zamba.Membership;
using System.Web.Script.Serialization;
using Zamba.FileTools;
using System.Text;


namespace ZambaWeb.RestApi.Controllers.Common
{
    public class CommonFuntions : ApiController
    {
        public bool IsNumeric(string value)
        {
            decimal testDe;
            double testDo;
            int testI;

            if (decimal.TryParse(value, out testDe) || double.TryParse(value, out testDo) || int.TryParse(value, out testI))
                return true;

            return false;
        }

        public IResult GetResultFromParamRequest(genericRequest paramRequest)
        {
            Int64 resultId = 0;
            Int64 entityId = 0;
            if (paramRequest.Params != null && paramRequest.Params.ContainsKey("resultId"))
            {
                resultId = Int64.Parse(paramRequest.Params["resultId"].ToString());
                entityId = Int64.Parse(paramRequest.Params["entityId"].ToString());
            }
            Results_Business RB = new Results_Business();
            IResult result = RB.GetResult(resultId, entityId, false);
            return result;
        }

        private const string STR_SLSTPLAIN_SEPARATOR = "-";
        public IIndex SetIndexData(string indexValue, Int64 indexid, List<IIndex> Indexs)
        {
            int toSplitIndex;

            foreach (IIndex currIndex in Indexs)
            {
                if (currIndex.ID == indexid)
                {
                    currIndex.Data = indexValue.Trim();
                    currIndex.DataTemp = currIndex.Data;

                    if (currIndex.DropDown != IndexAdditionalType.AutoSustitución && currIndex.DropDown != IndexAdditionalType.AutoSustituciónJerarquico)
                    {
                        if (currIndex.Type == IndexDataType.Si_No)
                        {
                            if (string.Compare(indexValue, "on") == 0)
                            {
                                currIndex.Data = "1";
                                currIndex.DataTemp = "1";
                            }
                            else
                            {
                                currIndex.Data = "0";
                                currIndex.DataTemp = "0";
                            }
                        }
                    }
                    else
                    {
                        toSplitIndex = indexValue.IndexOf(STR_SLSTPLAIN_SEPARATOR);
                        if (toSplitIndex > -1)
                        {
                            currIndex.Data = indexValue.Split(char.Parse(STR_SLSTPLAIN_SEPARATOR))[0];
                            currIndex.DataTemp = currIndex.Data;
                            currIndex.dataDescription = indexValue.Split(char.Parse(STR_SLSTPLAIN_SEPARATOR))[1];
                            currIndex.dataDescriptionTemp = indexValue.Split(char.Parse(STR_SLSTPLAIN_SEPARATOR))[1];
                        }
                        else
                        {
                            currIndex.Data = indexValue.Trim();
                            currIndex.DataTemp = indexValue.Trim();
                            currIndex.dataDescription = new AutoSubstitutionBusiness().getDescription(indexValue.Trim(), currIndex.ID);
                            currIndex.dataDescriptionTemp = currIndex.dataDescription;
                        }

                        currIndex.dataDescriptionTemp = currIndex.dataDescription;
                    }
                    return currIndex;
                }
            }
            return null;
        }

        public Zamba.Core.IUser GetUser(long? userId)
        {
            try
            {

                var user = ZambaWeb.RestApi.Controllers.TokenHelper.GetUser(User.Identity);

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

        public string validateParam(string paramData, genericRequest paramRequest)
        {
            string data = null;
            if (paramRequest.Params.ContainsKey(paramData))
            {
                var isnull = paramRequest.Params[paramData] == null;
                data = isnull ? string.Empty : paramRequest.Params[paramData];
            }
            else
            {
                data = string.Empty;
            }
            return data;
        }

        public string GetBase64Photo(long userId)
        {
            //Obtengo la imagen en base 64.
            string B64Photo = string.Empty;
            if (Zamba.Core.Cache.UsersAndGroups.hsUserPhotos.ContainsKey(userId) == false)
            {
                UserBusiness usrB = new UserBusiness();
                string userPhotoPath = usrB.GetUserPhotoPathById(userId);
                Zamba.FileTools.Base64 base64 = new Zamba.FileTools.Base64();
                B64Photo = base64.PathToBase64(userPhotoPath);
                Zamba.Core.Cache.UsersAndGroups.hsUserPhotos.Add(userId, B64Photo);
            }
            else
                B64Photo = Zamba.Core.Cache.UsersAndGroups.hsUserPhotos[userId].ToString();
            return B64Photo;
        }

        public string ConvertStringtoBinary(String data)
        {
            string result = "0x" + String.Join("",
            data.ToCharArray().
            Select(n => (Byte)n).ToArray().
            Select(n => n.ToString("X2")).
            ToArray());

            return result;
        }
    }
}