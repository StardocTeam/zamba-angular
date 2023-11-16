﻿Public Interface IFiltersComponent
    Function GetLastUsedFilters(ByVal docTypeId As Int64, ByVal currentUserId As Int64, ByVal filterType As FilterTypes) As List(Of IFilterElem)
    Function GetDocumentFiltersCount(ByVal docTypeId As Int64, ByVal filterType As FilterTypes) As Int32
    Function SetNewFilter(ByVal indexId As Int64, ByVal filterName As String, ByVal dataType As IndexDataType, ByVal currentUserId As Int64, ByVal compareOperator As String, ByVal valueString As String, ByVal docTypeId As Int64, ByVal save As Boolean, ByVal description As String, ByVal additionalType As IndexAdditionalType, ByVal FilterType As String, ByVal FType As FilterTypes) As IFilterElem
    Sub SaveFilterInDatabase(ByVal fe As IFilterElem)
    Sub SetEnabledFilter(ByVal fe As IFilterElem, ByVal filterType As FilterTypes)
    Sub RemoveFilter(ByVal fe As IFilterElem, ByVal filterType As FilterTypes)
    Sub ClearFilters(ByVal docTypeId As Int64, ByVal userId As Int64, ByVal filterType As FilterTypes, ByVal dataTable As DataTable, ByVal RemoveFilterRightz As Boolean)
End Interface