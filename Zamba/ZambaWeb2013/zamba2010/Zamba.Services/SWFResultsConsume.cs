using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zamba.Core;

namespace Zamba.Services
{
    public class SWFResultsConsume : IService
    {
        public ServicesTypes ServiceType()
        {
            return ServicesTypes.WFResultsConsume;
        }

        public static void ClearOptions()
        {
            WSResultsBusiness.ClearOptions();
        }
    }
}
