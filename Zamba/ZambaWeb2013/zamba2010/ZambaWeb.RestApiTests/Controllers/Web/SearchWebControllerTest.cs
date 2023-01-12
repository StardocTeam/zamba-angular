using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZambaWeb.RestApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Web.Http;
using ZambaWeb.RestApiTests;
using Zamba.Data;
using Zamba.Servers;
using Zamba.Membership;
using System.Web.Script.Serialization;
using System.IO;
using System.Web;
using Zamba.Core.Access;
using System.Data;
using System.Runtime.Serialization.Json;
using System.Threading;
using Newtonsoft.Json;
using System.Net.Security;
using Zamba.Services;
using Zamba.Core;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using System.Dynamic;
using Microsoft.CSharp;
using ZambaWeb.RestApiTests.Helpers;
using Zamba.Framework;

namespace ZambaWeb.RestApi.Controllers.Tests
{
    [TestClass()]
    public class SearchWebControllerTest
    {
        Results_Factory RF = new Results_Factory();

        #region Constructor&ClassHelpers
        private ConfValues.RestApi restApiConfig = null;
        private ConfValues.ValidUser user = null;

        public SearchWebControllerTest()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("ZambaWeb.RestApi");
            }
            restApiConfig = new ConfValues.RestApi();
            user = new ConfValues.ValidUser();
        }

        private string Login()
        {   
            var act = new AccountControllerTest();
            var token = act.UserValidLogin();
            return token;
        }
        #endregion


        #region SearchWebGetTree
        [TestMethod()]
        public void SearchWebGetTreeOk()
        {
            try
            {
                var url = (restApiConfig.API + "searchweb/gettree");
                var token = Login();

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.UnsafeAuthenticatedConnectionSharing = true;
                request.Headers["Authorization"] = "Bearer " + token;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                var webResponse = reader.ReadToEnd();

                Assert.IsTrue(webResponse.Length > 0);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void SearchWebGetTreeWithOutToken()
        {
            try
            {
                var url = (restApiConfig.API + "searchweb/gettree");
                var token = Login();

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.UnsafeAuthenticatedConnectionSharing = true;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                var webResponse = reader.ReadToEnd();

                Assert.Fail();
            }
            catch (WebException webex)
            {
                Assert.AreEqual(HttpStatusCode.NotFound, ((System.Net.HttpWebResponse)webex.Response).StatusCode);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }
        #endregion

        #region SearchWebGetIndex
        [TestMethod()]
        public void SearchWebGetIndexOk()
        {
            try
            {
                var token = Login();
                var url = restApiConfig.API + "searchweb/getindexs";

                var ds = DocTypesFactory.GetDocTypesIdsAndNames();
                var dataRows = ds.Tables[0].Rows;
                var docTypeIds = new List<long>();
                foreach (DataRow dr in dataRows)
                {
                    var docTypeId = long.Parse(dr["DOC_TYPE_ID"].ToString());
                    docTypeIds.Add(docTypeId);
                    if (docTypeIds.Count == 2) break;
                }

                url += "?indexs=" + JsonConvert.SerializeObject(docTypeIds);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json; charset=UTF-8";
                request.UnsafeAuthenticatedConnectionSharing = true;
                request.Headers["Authorization"] = "Bearer " + token;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                StreamReader reader = new StreamReader(response.GetResponseStream());
                var webResponse = reader.ReadToEnd();
                var clearResponse = webResponse.Replace("\\", "").Replace("\"", "");
                Assert.IsTrue(webResponse.Length > 0);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void SearchWebGetIndexWithoutParams()
        {
            try
            {
                var token = Login();
                var url = restApiConfig.API + "searchweb/getindexs";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json; charset=UTF-8";
                request.UnsafeAuthenticatedConnectionSharing = true;
                request.Headers["Authorization"] = "Bearer " + token;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                StreamReader reader = new StreamReader(response.GetResponseStream());
                var webResponse = reader.ReadToEnd();
                var clearResponse = webResponse.Replace("\\", "").Replace("\"", "");
                Assert.Fail(StringHelper.ExceptionExpected);
            }
            catch (WebException webex)
            {
                Assert.AreEqual(HttpStatusCode.BadRequest, ((System.Net.HttpWebResponse)webex.Response).StatusCode);
                string exceptionString = TestHelper.GetExceptionString(webex);
                Assert.AreEqual(exceptionString, StringHelper.IndexExpected);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }
        #endregion

        #region SearchWebGetResult
        [TestMethod()]
        public void SearchWebGetResultOk()
        {
            IndexsBusiness IB = new IndexsBusiness();

            try
            {
                var url = (restApiConfig.API + "searchweb/getresults");
                var token = Login();

                var sw = new SearchDto();
                sw.UserId = user.ID;

                #region DocType&Index aleatorios
                var ds = DocTypesFactory.GetDocTypesIdsAndNames();
                var dataRows = ds.Tables[0].Rows;
                foreach (DataRow dr in dataRows)
                {
                    var docTypeId = long.Parse(dr["DOC_TYPE_ID"].ToString());
                    var indexs = IB.GetIndexsSchemaAsListOfDT(docTypeId);
                    var docCount = RF.GetDocumentsCount((int)docTypeId);
                    if (docCount > 0)
                    {
                        var r = new Random();
                        var dsIndex = RF.GetDocumentsIndexByRowNum(docTypeId, r.Next(1, (int)docCount));
                        var rowIndex = dsIndex.Rows[0];
                        foreach (Zamba.IIndex index in indexs)
                        {
                            if (rowIndex.Table.Columns.Contains("i" + index.ID) && !string.IsNullOrEmpty(rowIndex["i" + index.ID].ToString()))
                            {
                                index.Data = index.Data2 = index.DataTemp = index.DataTemp2 = rowIndex["i" + index.ID].ToString();
                                var listIndex = new List<object>();
                                indexs.ForEach(x => listIndex.Add((object)x));
                                sw.Indexs = listIndex;
                                var docTypeList = new List<long>();
                                docTypeList.Add(docTypeId);
                                sw.DoctypesIds = docTypeList;
                                break;
                            }
                        }
                        break;
                    }
                }
                #endregion

                var json = JsonConvert.SerializeObject(sw);
                byte[] arrData = Encoding.UTF8.GetBytes(json);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.UnsafeAuthenticatedConnectionSharing = true;
                request.Headers["Authorization"] = "Bearer " + token;
                request.ContentType = "application/json; charset=UTF-8";

                request.ContentLength = arrData.Length;
                using (var dataStream = request.GetRequestStream())
                {
                    dataStream.Write(arrData, 0, arrData.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    String responseString = reader.ReadToEnd();
                    Assert.IsTrue(responseString.Length > 0);

                    responseString = (responseString.Trim('"')).TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' });
                    responseString = responseString.Replace("\\", "");

                    dynamic dynObj = JsonConvert.DeserializeObject<dynamic>(responseString);
                    Assert.IsTrue((dynObj.DOC_TYPE_ID).ToString() == sw.DoctypesIds.FirstOrDefault().ToString());
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
            finally { IB = null; }
        }

        [TestMethod()]
        public void SearchWebGetResultWithOutParams()
        {
            try
            {
                var url = (restApiConfig.API + "searchweb/getresults");
                var token = Login();
                var sw = new SearchDto();
                var json = JsonConvert.SerializeObject(sw);
                byte[] arrData = Encoding.UTF8.GetBytes(json);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.UnsafeAuthenticatedConnectionSharing = true;
                request.Headers["Authorization"] = "Bearer " + token;
                request.ContentType = "application/json; charset=UTF-8";

                request.ContentLength = arrData.Length;
                using (var dataStream = request.GetRequestStream())
                {
                    dataStream.Write(arrData, 0, arrData.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();
                Assert.Fail(StringHelper.ExceptionExpected);
            }
            catch (WebException webex)
            {
                Assert.AreEqual(HttpStatusCode.InternalServerError, ((System.Net.HttpWebResponse)webex.Response).StatusCode);
                string exceptionString = TestHelper.GetExceptionString(webex);
                Assert.AreEqual(exceptionString, StringHelper.IndexExpected);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }
        #endregion
    }
}