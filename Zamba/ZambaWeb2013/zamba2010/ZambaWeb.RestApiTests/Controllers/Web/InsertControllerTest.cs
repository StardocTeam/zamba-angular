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
using Microsoft.CSharp;
using ZambaWeb.RestApiTests.Helpers;
using Zamba.Filters;
using ZambaWeb.RestApi.ViewModels;
using Zamba.Core;
using Zamba.Framework;

namespace ZambaWeb.RestApi.Controllers.Tests
{
    [TestClass()]
    public class InsertControllerTest
    {
        Results_Factory RF = new Results_Factory();
        #region Constructor&ClassHelpers
        private ConfValues.RestApi restApiConfig = null;
        private ConfValues.ValidUser user = null;

        public InsertControllerTest()
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
        private string GetTxtFilePath()
        {
            string myTempFile = string.Empty;
            try
            {
                myTempFile = Path.Combine(Path.GetTempPath(), "InsertDocText.txt");
                using (StreamWriter sw = new StreamWriter(myTempFile))
                {
                    sw.WriteLine("InsertDocText");
                }
            }
            catch (Exception ex)
            {

            }
            return myTempFile;
        }

        #endregion

        #region Insert
        [TestMethod()]
        public void InsertOk()
        {
            IndexsBusiness IB = new IndexsBusiness();
            try
            {
                var url = (restApiConfig.API + "Insert/Insert");
                var token = Login();
                var param = new InsertParamVM();
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
                                param.Indexs = listIndex;
                                param.DocTypeId = docTypeId;
                                break;
                            }
                        }
                        break;
                    }
                }
                #endregion
                var fileNames = new List<string>();
                fileNames.Add(GetTxtFilePath());
                param.Filenames = fileNames;

                var json = JsonConvert.SerializeObject(param);
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
                    //Insertado correctamente
                    Assert.IsTrue(dynObj.Success.ToString().ToLower() == "true");
                    Assert.IsTrue(dynObj.NewResult.DocTypeId.ToString() == param.DocTypeId.ToString());
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
            finally { IB = null; }
        }
           
        [TestMethod()]
        public void InsertWithOutParamenter()
        {
            try
            {
                var url = (restApiConfig.API + "Insert/Insert");
                var token = Login();
                var param = new InsertParamVM();
         
                var json = JsonConvert.SerializeObject(param);
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
                Assert.AreEqual(HttpStatusCode.BadRequest, ((System.Net.HttpWebResponse)webex.Response).StatusCode);
                string exceptionString = TestHelper.GetExceptionString(webex);
                Assert.AreEqual(exceptionString, StringHelper.BadInsertParameter);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }
        #endregion             
    }
}