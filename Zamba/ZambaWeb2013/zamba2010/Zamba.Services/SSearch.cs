namespace Zamba.Services
{
    public class SSearch : IService
    {
        #region Singleton
        private static SSearch _search = null;

        private SSearch()
        {
        }

        public static IService GetInstance()
        {
            if (_search == null)
                _search = new SSearch();

            return _search;
        }
        #endregion
        #region IService Members
        ServicesTypes IService.ServiceType()
        {
            return ServicesTypes.Search;
        }
        #endregion
    }
}