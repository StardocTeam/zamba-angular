using System.Data;
using Zamba.Core;

namespace Zamba.Services
{
    internal class History : IService
    {
        #region Singleton
        private static History _history = null;

        private History()
        {
        }

        public static IService GetInstance()
        {
            if (_history == null)
                _history = new History();

            return _history;
        }
        #endregion
        #region IService Members
        ServicesTypes IService.ServiceType()
        {
            return ServicesTypes.History;
        }
        #endregion
        public static DataSet GetHistory(IResult result)
        {
            // TODO :
            return null;
        }
    }
}