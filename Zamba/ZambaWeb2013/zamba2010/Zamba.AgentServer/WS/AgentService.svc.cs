using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.ServiceModel.Web;
using System.Web;
using System.ServiceModel.Activation;
using Zamba.Data;
using Zamba.Core;
using System.Web.Security;

namespace Zamba.AgentServer.WS
{

    public class AgentService : IAgentService
    {

        public AgentService() {

            if (Zamba.Servers.Server.ConInitialized == false) {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.Web");
                Zamba.Core.UserPreferences.LoadAllMachineConfigValues();
                //Zamba.Membership.MembershipHelper.OptionalAppTempPath = Zamba.Core.UserPreferences.getValue("AppTempPath", Zamba.Sections.UserPreferences, String.Empty);

            }

        }
        


        String IAgentService.SaveUCMDataSet(DataSet dsUcm, String client, DateTime dsDate, String server, String dataBase)
        {
            AgentBusiness ab = new AgentBusiness();
            string result = ab.SaveUCM(dsUcm, client, dsDate, server, dataBase);
            ab = null;
            return result;
        }

        String IAgentService.SaveILMDataSet(DataSet dsIlm, String client, DateTime dsDate, String server, String dataBase)
        {
            AgentBusiness ab = new AgentBusiness();
            string result = ab.SaveILM(dsIlm, client);
            ab = null;
            return result;
        }

        String IAgentService.SaveEnabledLicCount(string client, int licenseType, string count)
        {
            AgentBusiness ab = new AgentBusiness();
            string result = ab.SaveEnabledLicCount(client, licenseType, count);
            ab = null;
            return result;
        }

        String IAgentService.SaveErrorReports(ErrorReport[] reports, string client)
        {
            AgentBusiness ab = new AgentBusiness();
            string result = ab.SaveErrorReports(reports, client);
            ab = null;
            return result;
        }
    }
}
