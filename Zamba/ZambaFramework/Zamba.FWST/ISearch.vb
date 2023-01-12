Public Interface ISearch
    Property RaiseResults() As Boolean
    Property DoctypesIds() As Int64()
    Property DocGroupId() As Long

    Property TextSearchInAllIndexs() As String
    Property blnSearchInAllDocsType() As Boolean

    Property NotesSearch() As String
    Property Textsearch() As String

    Property Indexs() As IIndex()
    Property ParentName() As String
    Property AdvanceSearch() As Boolean
    Property AdvanceSearchFilter() As String
End Interface