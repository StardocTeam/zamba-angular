Public Interface IDoExportHTMLToPDF
    Inherits IRule

    Property Content() As String
    Property CanEditable() As Boolean
    Property ReturnFileName() As String
End Interface