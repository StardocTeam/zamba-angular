using System.Data;
using Zamba.Core;

namespace Zamba.Services
{
    internal class SHistory : IService
    {
        #region Singleton
        private static SHistory _history = null;

        private SHistory()
        {
        }

        public static IService GetInstance()
        {
            if (_history == null)
                _history = new SHistory();

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