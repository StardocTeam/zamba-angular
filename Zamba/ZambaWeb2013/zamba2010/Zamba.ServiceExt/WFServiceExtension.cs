using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zamba.BussinesExt;
using System.Data;
using Zamba.Core;

namespace Zamba.ServiceExt
{
    public class WFServiceExtension
    {
        public static string BeginWFProcess(long processID, DataSet paramenters, IWFServiceZvarConfig[] paramConfiguration, long userID)
        { 
            return WFBussinesExtension.BeginWFProcess(processID,  paramenters,  paramConfiguration,  userID);
        }
    }
}
