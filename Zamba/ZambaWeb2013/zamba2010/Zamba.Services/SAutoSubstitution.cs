using System;
using Zamba.Core;

namespace Zamba.Services
{
    public class SAutoSubstitution:IService
    {
        #region IService Members

        public ServicesTypes ServiceType()
        {
            return ServicesTypes.AutoSubstitution;
        }

        #endregion

       

    }
}
