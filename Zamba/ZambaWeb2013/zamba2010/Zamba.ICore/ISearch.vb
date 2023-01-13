Public Interface ISearch
    Property Doctypes As Generic.List(Of IDocType)
    Property Indexs As Generic.List(Of IIndex)
    Property RaiseResults() As Boolean
    Property CaseSensitive() As Boolean
    Property UseVersion() As Boolean
    Property ShowIndexOnGrid() As Boolean
    Property TextSearchInAllIndexs() As String
    Property blnSearchInAllDocsType() As Boolean
    Property NotesSearch() As String

    Property Textsearch() As String
    Property ParentName() As String
    Property MaxResults() As Int32
    Property UserId() As Int64
    Property WorkflowId() As Int64
    Property StepId() As Int64
    Property StepStateId() As Int64
    Property TaskStateId() As Int64

    Sub AddIndex(index As IIndex)
    Sub AddDocType(docType As IDocType)
End Interface