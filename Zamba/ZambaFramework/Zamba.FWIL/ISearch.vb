Imports Zamba.Searchs

Public Interface ISearch
    Property Doctypes As Generic.List(Of IDocType)
    Property Indexs As Generic.List(Of IIndex)
    Property CaseSensitive() As Boolean
    Property UseVersion() As Boolean
    Property ShowIndexOnGrid() As Boolean
    Property Textsearch() As String
    Property ParentName() As String
    Property Name() As String
    Property MaxResults() As Int32
    Property UserId() As Int64
    Property WorkflowId() As Int64
    Property StepId() As Int64
    Property StepStateId() As Int64
    Property TaskStateId() As Int64
    Property SQL() As List(Of String)
    Property SQLCount() As List(Of String)
    Property SearchType As SearchTypes
    Property EntitiesEnabledForQuickSearch As List(Of IEntityEnabledForQuickSearch)
    Sub SetOrderBy(ByVal orderString As String)
    Sub SetGroupBy(ByVal orderString As String)
    Sub AddIndex(index As IIndex)
    Sub AddDocType(docType As IDocType)
    Property OrderBy As String

    Property Filters As List(Of ikendoFilter)
End Interface