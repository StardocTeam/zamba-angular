Public Interface IFiltersComponent
    Function GetFiltersString(ByVal filters As IEnumerable(Of IFilterElem)) As String
    Function GetLastUsedFilters(ByVal docTypeId As Int64, ByVal currentUserId As Int64, ByVal IsTask As Boolean) As List(Of IFilterElem)
    Function GetDocumentFiltersCount(ByVal docTypeId As Int64, ByVal IsTask As Boolean) As Int32
    Function SetNewFilter(ByVal indexId As Int64, ByVal filterName As [String], ByVal dataType As IndexDataType, ByVal currentUserId As Int64, ByVal compareOperator As [String], ByVal valueString As [String], _
    ByVal docTypeId As Int64, ByVal save As [Boolean], ByVal description As [String], ByVal additionalType As IndexAdditionalType, ByVal FilterType As [String], ByVal IsTask As [Boolean]) As IFilterElem
    Sub SetEnabledFilter(ByVal fe As IFilterElem, ByVal IsTask As [Boolean])
    Sub RemoveFilter(ByVal fe As IFilterElem, ByVal IsTask As [Boolean])
    'Sub ClearFilters(ByVal docTypeId As Int64, ByVal userId As Int64, ByVal IsTask As [Boolean])
    Sub ClearFilters(ByVal docTypeId As Int64, ByVal userId As Int64, ByVal IsTask As Boolean, ByVal dataTable As DataTable, ByVal RemoveFilterRight As Boolean)

    Sub AddFiltersElements(ByVal filtersElements As List(Of IFilterElem))

End Interface