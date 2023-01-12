Public interface IWordBrowser
    Event eCloseDocument()
    Event eDocumentSaved()
    Event ePrintingDocument()
    ReadOnly Property IsDocumentEdited() As Boolean
    Sub ShowDocument(ByVal filepath As String, ByVal isReadOnly As Boolean)
    Sub PrintDocument()
    Sub SaveDocument()
    Sub SaveDocumentAs()
    Property TP As TabPage
end interface