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
    public class AccountControllerTest
    {
        #region Constructor&ClassHelpers
        private ConfValues.RestApi restApiConfig = null;
        private ConfValues.ValidUser user = null;
     
        public AccountControllerTest()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("ZambaWeb.RestApi");
            }
            restApiConfig = new ConfValues.RestApi();
            user = new ConfValues.ValidUser();
        }
        public string UserValidLogin()
        {
            try
            {
                String responseString = string.Empty;
                var url = (restApiConfig.API + "Account/login");
                var l = new LoginVM()
                {
                    UserName = user.userName,
                    Password = user.password,
                    ComputerNameOrIp = HttpContext.Current.Request.UserHostAddress.Replace("::1","127.0.0.1")
                };
                var json = JsonConvert.SerializeObject(l);
                byte[] arrData = Encoding.UTF8.GetBytes(json);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.UnsafeAuthenticatedConnectionSharing = true;
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
                    responseString = reader.ReadToEnd();
                    Assert.IsTrue(responseString.Length > 0);
                    responseString = (responseString.Trim('"')).TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' });
                    responseString = responseString.Replace("\\", "");

                }
                return responseString;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
                return string.Empty;
            }
        }
        #endregion

        [TestMethod()]
        public void AccountCheckTokenAPIWithZambaBusiness()
        {     
            String restApiResult = UserValidLogin();

            var select = "SELECT TOKEN FROM ZSS WHERE USERID=" + user.ID;
            string zambaBusinessResult = Zamba.Servers.Server.get_Con().ExecuteScalar(CommandType.Text, select).ToString();

            Assert.AreEqual(zambaBusinessResult, restApiResult);
        }
          
        [TestMethod()]
        public void AccountUserLoginOk()
        {
            try
            {
                string restApiResult = UserValidLogin();

                Assert.IsTrue(restApiResult.Length > 0);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]      
        public void AccountEmptyUserLogin()
        {
            try
            {
                String responseString = string.Empty;
                var url = (restApiConfig.API + "Account/login");
              
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.UnsafeAuthenticatedConnectionSharing = true;
                request.ContentType = "application/json; charset=UTF-8";
                request.ContentLength = 0;             
                var response = (HttpWebResponse)request.GetResponse();
                Assert.Fail(StringHelper.ExceptionExpected);
            }            
            catch (WebException webex)
            {
                Assert.AreEqual(HttpStatusCode.InternalServerError, ((System.Net.HttpWebResponse)webex.Response).StatusCode);            
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void AccountSameTokenIfRelogin()
        {
            try
            {
                var requestToken = UserValidLogin();
                Thread.Sleep(5000);
                var newRequestToken = UserValidLogin();
                Assert.AreEqual(requestToken, newRequestToken);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]     
        public void AccountBadUserLogin()
        {
            try
            {
                 String responseString = string.Empty;
                var url = (restApiConfig.API + "Account/login");
                var l = new LoginVM()
                {
                    UserName = "wrongUser.UserName",
                    Password = "wrongUser.password",
                    ComputerNameOrIp = "wrongUser.computerNameOrIp"
                };
                var json = JsonConvert.SerializeObject(l);
                byte[] arrData = Encoding.UTF8.GetBytes(json);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.UnsafeAuthenticatedConnectionSharing = true;
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
                Assert.AreEqual(HttpStatusCode.NotAcceptable, ((System.Net.HttpWebResponse)webex.Response).StatusCode);
                string exceptionString = TestHelper.GetExceptionString(webex);
                Assert.AreEqual(exceptionString, StringHelper.ValidUserError);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());              
            }
        } 
    }
}
