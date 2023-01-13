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
using Zamba.Core;

namespace ZambaWeb.RestApi.Controllers.Tests
{
    [TestClass()]
    public class TemplatesControllerTest
    {
        #region Constructor&ClassHelpers
        private ConfValues.RestApi restApiConfig = null;
        private ConfValues.ValidUser user = null;

        public TemplatesControllerTest()
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


        #region TemplatesGetTemplates
        [TestMethod()]
        public void TemplatesGetTemplatesOk()
        {
            try
            {
                var url = (restApiConfig.API + "Templates/GetTemplates");
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
                //Trae Templates
                webResponse = (webResponse.Trim('"'));
                webResponse = webResponse.Replace("\\", "");
                dynamic dynObj = JsonConvert.DeserializeObject<dynamic>(webResponse);
                var firstTemplate = dynObj[0].Name;
                Assert.IsTrue(firstTemplate.GetType().Name == "JValue");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void TemplatesGetTemplatesWithOutToken()
        {
            try
            {
                var url = (restApiConfig.API + "Templates/GetTemplates");             
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
    }
}