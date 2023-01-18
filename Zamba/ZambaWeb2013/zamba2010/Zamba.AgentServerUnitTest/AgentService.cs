using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zamba.AgentServer.WS;
using Zamba.Core;
using System.Data;
using Zamba.Data;
using System.ServiceModel;

namespace Zamba.AgentServerUnitTest
{
    /// <summary>
    /// Se debe configurar el app.ini para apuntar a ZambaStardoc
    /// </summary>
    [TestClass]
    public class AgentServiceUT
    {
        [TestMethod]
        public void SaveEnabledLicCount_OK()
        {
            string client = "TEST";
            int licenseType = 2;
            string count = "200";

            AgentBusiness ab = new AgentBusiness();
            string result = ab.SaveEnabledLicCount(client, licenseType, count);
            ab = null;

            //Los clientes se encuentran configurados en la base de datos ZambaStardoc
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void SaveEnabledLicCount_FAIL()
        {
            string client = "TEST-fail";
            int licenseType = 2;
            string count = "200";

            AgentBusiness ab = new AgentBusiness();
            string result = ab.SaveEnabledLicCount(client, licenseType, count);
            ab = null;

            //Los clientes se encuentran configurados en la base de datos ZambaStardoc
            Assert.AreEqual(true, result.Length > 0);
        }

        [TestMethod]
        public void SaveEnabledLicCount_EXCEPTION()
        {
            string client = null;
            int licenseType = 0;
            string count = null;

            AgentBusiness ab = new AgentBusiness();
            string result = ab.SaveEnabledLicCount(client, licenseType, count);
            ab = null;

            //Los clientes se encuentran configurados en la base de datos ZambaStardoc
            Assert.AreEqual(true, result.Length > 0);
        }

        [TestMethod]
        public void SaveUCMDataSet_REAL()
        {
            AgentFactoryExt afe = new AgentFactoryExt();
            DataSet dsUcm = afe.GetUsersReport();

            AgentBusiness ab = new AgentBusiness();
            string result = ab.SaveUCM(dsUcm, "TEST", DateTime.Now, "TEST-SERVER", "TEST-DB");
            ab = null;

            if (dsUcm != null && dsUcm.Tables.Count > 0 && dsUcm.Tables[0].Rows.Count > 0)
            {
                Assert.AreEqual(true, !result.Contains("Error") && !result.Contains("La tabla se encontraba sin registros"));
            }
            else
            {
                Assert.AreEqual(true, !result.Contains("Error") && result.Contains("La tabla se encontraba sin registros"));
            }
        }

        [TestMethod]
        public void SaveILMDataSet_REAL()
        {
            AgentFactoryExt afe = new AgentFactoryExt();
            DataSet dsIlm = afe.GetILM();

            AgentBusiness ab = new AgentBusiness();
            string result = ab.SaveILM(dsIlm, "TEST");
            ab = null;

            if (dsIlm != null && dsIlm.Tables.Count > 0 && dsIlm.Tables[0].Rows.Count > 0)
            {
                Assert.AreEqual(true, !result.Contains("Error") && !result.Contains("La tabla se encontraba sin registros"));
            }
            else
            {
                Assert.AreEqual(true, !result.Contains("Error") && result.Contains("La tabla se encontraba sin registros"));
            }
        }

        [TestMethod]
        public void SaveILMDataSet_SIM()
        {
            AgentFactoryExt afe = new AgentFactoryExt();
            DataSet dsIlm = afe.GetILM();

            if (dsIlm.Tables[0].Rows.Count == 0)
            {
                dsIlm.Tables[0].Rows.Add(new object[] { DateTime.Now.ToString(), 1, 1, "User1"});
                dsIlm.Tables[0].Rows.Add(new object[] { DateTime.Now.ToString(), 2, 1, "User2" });
                dsIlm.Tables[0].Rows.Add(new object[] { DateTime.Now.ToString(), 2, 3, "User2" });
                dsIlm.Tables[0].Rows.Add(new object[] { DateTime.Now.ToString(), 3, 1, "User3" });
                dsIlm.Tables[0].Rows.Add(new object[] { DateTime.Now.ToString(), 1, 2, "User1" });
            }

            AgentBusiness ab = new AgentBusiness();
            string result = ab.SaveILM(dsIlm, "TEST");
            ab = null;

            if (dsIlm != null && dsIlm.Tables.Count > 0 && dsIlm.Tables[0].Rows.Count > 0)
            {
                Assert.AreEqual(true, !result.Contains("Error") && !result.Contains("La tabla se encontraba sin registros"));
            }
            else
            {
                Assert.AreEqual(true, !result.Contains("Error") && result.Contains("La tabla se encontraba sin registros"));
            }
        }

        [TestMethod]
        public void SaveILMDataSet_EMPTY_TABLE()
        {
            DataSet dsIlm = new DataSet();
            AgentBusiness ab = new AgentBusiness();
            string result = ab.SaveILM(dsIlm, "TEST");
            ab = null;

            Assert.AreEqual(true, !result.Contains("Error") && result.Contains("La tabla se encontraba sin registros"));
        }

        [TestMethod]
        public void SaveUCMDataSet_EMPTY_TABLE()
        {
            DataSet dsUcm = new DataSet();
            AgentBusiness ab = new AgentBusiness();
            string result = ab.SaveUCM(dsUcm, "TEST", DateTime.Now, "TEST-SERVER", "TEST-DB");
            ab = null;

            Assert.AreEqual(true, !result.Contains("Error") && result.Contains("La tabla se encontraba sin registros"));
        }

        [TestMethod]
        public void SaveILM_WS_SIM()
        {
            AgentFactoryExt afe = new AgentFactoryExt();
            DataSet dsIlm = afe.GetILM();
            string result;
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.MaxReceivedMessageSize = 2147483647;
            EndpointAddress endpoint = new EndpointAddress("http://www.zambabpm.com.ar/Zamba.ControlPanel/WS/AgentService.svc");
            if (dsIlm.Tables[0].Rows.Count == 0)
            {
                dsIlm.Tables[0].Rows.Add(new object[] { DateTime.Now.ToString(), 1, 1, "User1" });
                dsIlm.Tables[0].Rows.Add(new object[] { DateTime.Now.ToString(), 2, 1, "User2" });
                dsIlm.Tables[0].Rows.Add(new object[] { DateTime.Now.ToString(), 2, 3, "User2" });
                dsIlm.Tables[0].Rows.Add(new object[] { DateTime.Now.ToString(), 3, 1, "User3" });
                dsIlm.Tables[0].Rows.Add(new object[] { DateTime.Now.ToString(), 1, 2, "User1" });
            }

            using (AgentServiceRef.AgentServiceClient ASC = new AgentServiceRef.AgentServiceClient(binding, endpoint))
            {
                result = ASC.SaveILMDataSet(dsIlm, "TEST", DateTime.Now, "TEST", "TEST");
            }

            if (dsIlm != null && dsIlm.Tables.Count > 0 && dsIlm.Tables[0].Rows.Count > 0)
            {
                Assert.AreEqual(true, !result.Contains("Error") && !result.Contains("La tabla se encontraba sin registros"));
            }
            else
            {
                Assert.AreEqual(true, !result.Contains("Error") && result.Contains("La tabla se encontraba sin registros"));
            }
        }

        [TestMethod]
        public void SaveErrorReports_SIM()
        {
            ErrorReportBusiness erb = new ErrorReportBusiness();
            ErrorReport[] reports = erb.GetReportsToExport();
            erb = null;

            AgentBusiness ab = new AgentBusiness();
            string result = ab.SaveErrorReports(reports, "TEST");
            ab = null;

            Assert.AreEqual(true, !result.Contains("Error"));
        }

        [TestMethod]
        public void SaveErrorReports_REAL()
        {
            ErrorReportBusiness erb = new ErrorReportBusiness();
            ErrorReport[] reports = erb.GetReportsToExport();
            erb = null;

            AgentFactoryExt afe = new AgentFactoryExt();
            string result;
            BasicHttpBinding binding = new BasicHttpBinding();

            EndpointAddress endpoint = new EndpointAddress("http://www.zambabpm.com.ar/Zamba.ControlPanel/WS/AgentService.svc");

            using (AgentServiceRef.AgentServiceClient ASC = new AgentServiceRef.AgentServiceClient(binding, endpoint))
            {
                result = ASC.SaveErrorReports(reports, "TEST");
            }

            Assert.AreEqual(true, !result.Contains("Error"));
        }
    }
}
