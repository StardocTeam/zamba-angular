Public Interface IDoSignPDF
    Inherits IRule
    'PDF Document
    Property FullPath() As String
    Property FileName() As String
    'PDF Metadata
    Property Author() As String
    Property Title() As String
    Property Subject() As String
    Property Keywords() As String
    Property Creator() As String
    Property Producer() As String
    'Certificate
    Property Certificate() As String
    Property Password() As String
    'Signature
    Property Reason() As String
    Property Contact() As String
    Property Location() As String
    Property WritePDF() As Boolean
End Interface