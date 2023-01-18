namespace Zamba.Services
{
    public class Search : IService
    {
        #region Singleton
        private static Search _search = null;

        private Search()
        {
        }

        public static IService GetInstance()
        {
            if (_search == null)
                _search = new Search();

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