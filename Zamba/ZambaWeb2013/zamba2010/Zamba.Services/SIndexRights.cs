using System;
using System.Collections.Generic;
using System.Text;
using Zamba.Core;

namespace Zamba.Services
{
    public class SIndexRights : IService
    {
        #region IService Members

        public ServicesTypes ServiceType()
        {
            return ServicesTypes.IndexRights;
        }

        #endregion

        private SIndexRights() { }

        public SIndexRights(ref IUser currentUser)
        {

        }
    }
}