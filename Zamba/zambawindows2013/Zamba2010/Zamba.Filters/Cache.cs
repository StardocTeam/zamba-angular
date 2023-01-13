using System.Collections;

namespace Zamba.Filters
{
    class Cache
    {
        /// <summary>
        /// Contiene los filtros de un entidad. LA key es docTypeId + "-" + IsTask
        /// </summary>
        public static Hashtable HsFilters = new Hashtable();
    }
}
