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
    public class TasksControllerTest
    {

        WFTasksFactory WTF = new WFTasksFactory();

        #region Constructor&ClassHelpers
        private ConfValues.RestApi restApiConfig = null;
        private ConfValues.ValidUser user = null;

        public TasksControllerTest()
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


        #region TasksLoadTree
        [TestMethod()]
        public void TasksLoadTreeOk()
        {
            try
            {
                var url = (restApiConfig.API + "tasks/loadtree");
                var token = Login();

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.UnsafeAuthenticatedConnectionSharing = true;
                request.Headers["Authorization"] = "Bearer " + token;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                var webResponse = reader.ReadToEnd();
                //Trae resultados
                Assert.IsTrue(webResponse.Length > 0);
                //Trae WF con valores
                webResponse = (webResponse.Trim('"'));
                webResponse = webResponse.Replace("\\", "");
                dynamic dynObj = JsonConvert.DeserializeObject<dynamic>(webResponse);
                var firstWF = dynObj[0].WfName;
                Assert.IsTrue(firstWF.GetType().Name == "JValue");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void TasksLoadTreeWithOutToken()
        {
            try
            {
                var url = (restApiConfig.API + "tasks/loadtree");
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

        #region TasksLoadTasks
        [TestMethod()]
        public void TasksLoadTasksOk()
        {
            try
            {
                var token = Login();
                var url = restApiConfig.API + "Tasks/LoadTasks";

                long stepId = 0;
                var docTypeIds = new List<long>();

                #region Obtencion de parametros aleatorios
                var ds = DocTypesFactory.GetDocTypesIdsAndNames();
                var dataRows = ds.Tables[0].Rows;
                DocTypesBusiness DTB = new DocTypesBusiness();
                foreach (DataRow dr in dataRows)
                {
                    var docTypeId = long.Parse(dr["DOC_TYPE_ID"].ToString());
                    var wfIds = DTB.GetDocTypeWfIds((int)docTypeId, true).Cast<string>()
                            .ToList();
                    foreach (string wfId in wfIds)
                    {
                        var steps = WFStepBusiness.GetStepsIdAndNamebyWorkflowId(Convert.ToInt32(wfId));
                        foreach (var step in steps)
                        {
                            var dt = WTF.GetTasksByStep(step.Key);
                            if (dt.Rows.Count > 0)
                            {
                                stepId = step.Key;
                                docTypeIds.Add(docTypeId);
                                break;
                            }
                        }
                        if (stepId > 0) break;
                    }
                    if (stepId > 0) break;
                }
                DTB = null;

                if (stepId == 0)
                    Assert.Fail("No se encontraron tareas");
                int lastPage = 1;
                int pageSize = 50;
                var filtersElem = new List<FilterElem>();
                var param = new LoadTasksParamVM()
                {
                    StepId = stepId,
                    DocTypeIds = docTypeIds,
                    LastPage = lastPage,
                    PageSize = pageSize,
                    FiltersElem = filtersElem
                };
                #endregion

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
                    if (dynObj.Data.ToString() == "[]")
                    {
                        //Usuario logueado no tiene acceso a ver las tareas, se ejecuta WFTaskBussines.GetTasksByStepandDocTypeId con userId
                    }
                    else
                    {
                        var docTypeIdResult = dynObj.Data[0].DoctypeId.ToString();
                        Assert.IsTrue(docTypeIdResult == param.DocTypeIds.FirstOrDefault().ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void TasksLoadTasksWithoutParams()
        {
            try
            {
                var token = Login();
                var url = restApiConfig.API + "Tasks/LoadTasks"; 
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json; charset=UTF-8";
                request.UnsafeAuthenticatedConnectionSharing = true;
                request.Headers["Authorization"] = "Bearer " + token;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                StreamReader reader = new StreamReader(response.GetResponseStream());
                var webResponse = reader.ReadToEnd();
                var clearResponse = webResponse.Replace("\\", "").Replace("\"", "");
                Assert.Fail("Debe dar error en 'request.GetResponse'");
            }
            catch (WebException webex)
            {
                Assert.AreEqual(HttpStatusCode.LengthRequired, ((System.Net.HttpWebResponse)webex.Response).StatusCode);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }
        #endregion

        #region TasksGetTask
        [TestMethod()]
        public void TasksGetTaskOk()
        {
            try
            {
                var token = Login();
                var url = restApiConfig.API + "Tasks/GetTask";
                long taskId = 0;

                #region Obtencion de parametros aleatorios
                var ds = DocTypesFactory.GetDocTypesIdsAndNames();
                var dataRows = ds.Tables[0].Rows;
                DocTypesBusiness DTB = new DocTypesBusiness();
                foreach (DataRow dr in dataRows)
                {
                    var docType = long.Parse(dr["DOC_TYPE_ID"].ToString());
                    var wfIds = DTB.GetDocTypeWfIds((int)docType, true).Cast<string>()
                            .ToList();
                    foreach (string wfId in wfIds)
                    {
                        var steps = WFStepBusiness.GetStepsIdAndNamebyWorkflowId(Convert.ToInt32(wfId));
                        foreach (var step in steps)
                        {
                            var dt = WTF.GetTasksByStep(step.Key);
                            if (dt.Rows.Count > 0)
                            {
                                taskId = Convert.ToInt64(dt.Rows[0]["Task_ID"]);
                                break;
                            }
                        }
                        if (taskId > 0) break;
                    }
                    if (taskId > 0) break;
                }
                DTB = null;

                if (taskId == 0)
                    Assert.Fail("No se encontraron tareas");
                #endregion

                url += "?taskId=" + taskId;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json; charset=UTF-8";
                request.UnsafeAuthenticatedConnectionSharing = true;
                request.Headers["Authorization"] = "Bearer " + token;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                StreamReader reader = new StreamReader(response.GetResponseStream());
                var webResponse = reader.ReadToEnd();
                Assert.IsTrue(webResponse.Length > 0);
                var clearResponse = webResponse.Replace("\\", "").Replace("\"", "");
                Assert.IsTrue(clearResponse.Contains("TaskId:" + taskId));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void TasksGetTaskWithOutParam()
        {
            try
            {
                var token = Login();
                var url = restApiConfig.API + "Tasks/GetTask";
                url += "?taskId=0";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json; charset=UTF-8";
                request.UnsafeAuthenticatedConnectionSharing = true;
                request.Headers["Authorization"] = "Bearer " + token;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                StreamReader reader = new StreamReader(response.GetResponseStream());
                var webResponse = reader.ReadToEnd();
                Assert.Fail(StringHelper.ExceptionExpected);
            }
            catch (WebException webex)
            {
                Assert.AreEqual(HttpStatusCode.BadRequest, ((System.Net.HttpWebResponse)webex.Response).StatusCode);
                var stringEx = TestHelper.GetExceptionString(webex);
                Assert.AreEqual(stringEx, StringHelper.TaskIdExpected);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }
        #endregion

        #region TasksGetResult
        [TestMethod()]
        public void TasksGetResultOk()
        {
            try
            {
                var token = Login();
                var url = restApiConfig.API + "Tasks/GetResult";
                long docTypeId = 0;
                long docId = 0;

                #region Obtencion de parametros aleatorios
                var ds = DocTypesFactory.GetDocTypesIdsAndNames();
                var dataRows = ds.Tables[0].Rows;
                DocTypesBusiness DTB = new DocTypesBusiness();

                foreach (DataRow dr in dataRows)
                {
                    var docType = long.Parse(dr["DOC_TYPE_ID"].ToString());
                    var wfIds = DTB.GetDocTypeWfIds((int)docType, true).Cast<string>()
                            .ToList();
                    foreach (string wfId in wfIds)
                    {
                        var steps = WFStepBusiness.GetStepsIdAndNamebyWorkflowId(Convert.ToInt32(wfId));
                        foreach (var step in steps)
                        {
                            var dt = WTF.GetTasksByStep(step.Key);
                            if (dt.Rows.Count > 0)
                            {
                                docId = Convert.ToInt64(dt.Rows[0]["DOC_ID"]);
                                docTypeId = docType;
                                break;
                            }
                        }
                        if (docTypeId > 0) break;
                    }
                    if (docTypeId > 0) break;
                }

                DTB = null;

                if (docTypeId == 0)
                    Assert.Fail("No se encontraron tareas");
                #endregion

                url += "?docId=" + docId + "&docTypeId=" + docTypeId;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json; charset=UTF-8";
                request.UnsafeAuthenticatedConnectionSharing = true;
                request.Headers["Authorization"] = "Bearer " + token;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                StreamReader reader = new StreamReader(response.GetResponseStream());
                var webResponse = reader.ReadToEnd();
                Assert.IsTrue(webResponse.Length > 0);
                var clearResponse = webResponse.Replace("\\", "").Replace("\"", "");
                Assert.IsTrue(clearResponse.Contains("DocTypeId:" + docTypeId));
                Assert.IsTrue(clearResponse.Contains("ID:" + docId));

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void TasksGetResultWithOutParam()
        {
            try
            {
                var token = Login();
                var url = restApiConfig.API + "Tasks/GetResult";
                url += "?docId=0&docTypeId=0";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json; charset=UTF-8";
                request.UnsafeAuthenticatedConnectionSharing = true;
                request.Headers["Authorization"] = "Bearer " + token;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                StreamReader reader = new StreamReader(response.GetResponseStream());
                var webResponse = reader.ReadToEnd();
                Assert.Fail("Debe dar error al ejecutar la llamada");
            }
            catch (WebException webex)
            {
                Assert.AreEqual(HttpStatusCode.BadRequest, ((System.Net.HttpWebResponse)webex.Response).StatusCode);
                var stringEx = TestHelper.GetExceptionString(webex);
                Assert.AreEqual(stringEx, "No se ingreso docId-docTypeId");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }
        #endregion

    }
}