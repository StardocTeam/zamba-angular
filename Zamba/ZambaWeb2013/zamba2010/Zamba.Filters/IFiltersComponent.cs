using System;
using System.Collections.Generic;
using Zamba.Core;

namespace Zamba.Filters
{
    public interface IFiltersComponent
    {
        List<FilterElem> GetLastUsedFilters(Int64 docTypeId, Int64 currentUserId, bool IsTask);
        Int32 GetDocumentFiltersCount(Int64 docTypeId, bool IsTask);
        FilterElem SetNewFilter(Int64 indexId, String filterName, IndexDataType dataType, Int64 currentUserId,
                                                String compareOperator, String valueString, Int64 docTypeId, Boolean save, String description, IndexAdditionalType additionalType, String FilterType, Boolean IsTask);
        void SaveFilterInDatabase(IFilterElem fe);
        void SetEnabledFilter(IFilterElem fe, Boolean IsTask);
        void RemoveFilter(IFilterElem fe, Boolean IsTask);
        void ClearFilters(Int64 docTypeId, Int64 userId, Boolean IsTask, System.Data.DataTable dataTable, Boolean RemoveFilterRightz);
    }
}