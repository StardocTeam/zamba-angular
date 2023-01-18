using System;
using System.Collections.Generic;
using System.Text;
using Zamba.Core;

namespace Zamba.Services
{
    public class SZOptBusiness : IService 
    {

        
        private SSteps Steps;

        public SZOptBusiness()
        {
            this.Steps = new SSteps();
        }

        public ServicesTypes ServiceType()
        {
            return ServicesTypes.ZOptBusiness;

        }
        public string GetValue(string key) 
        {
            ZOptBusiness zopt = new ZOptBusiness();
            return zopt.GetValue(key);
            zopt = null;
        }

        public void Insert(string key, string value)
        {
            ZOptBusiness.Insert(key, value);
        }
    }
}
