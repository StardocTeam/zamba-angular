Public Interface ISearch
    Property RaiseResults() As Boolean
    Property Doctypes() As IDocType()

    Property TextSearchInAllIndexs() As String
    Property blnSearchInAllDocsType() As Boolean

    Property NotesSearch() As String

    Property Textsearch() As String
    Property Indexs() As IIndex()
    Property ParentName() As String
End Interface