using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZambaWeb.RestApi.Controllers.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ZambaWeb.RestApiTests;
using ZambaWeb.RestApi.Controllers.Tests;
using System.Net;
using Newtonsoft.Json;

namespace ZambaWeb.RestApi.Controllers.Web.Tests
{
    [TestClass()]
    public class SignPDFControllerTests
    {

        #region Constructor&ClassHelpers
        private ConfValues.RestApi restApiConfig = null;
        private ConfValues.ValidUser user = null;

        public SignPDFControllerTests()
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

        [TestMethod()]
        public void SignSinglePDFTest()
        {
            string path = "";
            FileInfo fi = new FileInfo(path);
            if (!fi.Exists)
            {
                Assert.Fail("No existe archivo PDF para realizar el TEST");
            }

            var url = (restApiConfig.API + "SignPDF/SignSinglePDF");
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
    }
}