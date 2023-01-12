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

namespace Zamba.AgentServer.WS
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUCMService" in both code and config file together.
    [ServiceContract]
    public interface IUCMService
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
                    UriTemplate = "details/{client}/{year}/{month}/{day}/{hour}")]
        UCMDetail[] Details(string Client, string Year, string Month, string Day, string Hour);
  }




}
