using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using Zamba.Core;

namespace Zamba.AgentServer.WS
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAgentService" in both code and config file together.
    [ServiceContract]
    public interface IAgentService
    {
        [OperationContract]
        String SaveUCMDataSet(DataSet UCMDS, String Client, DateTime DsDate, String Server, String DataBase);

        [OperationContract]
        String SaveILMDataSet(DataSet ILMDS, String Client, DateTime DsDate, String Server, String DataBase);

        [OperationContract]
        String SaveEnabledLicCount(String Client, Int32 LicenseType, String Count);

        [OperationContract]
        String SaveErrorReports(ErrorReport[] reports, String client);
    }
}



