Imports Microsoft.Office.Interop.Word

Public Class WordDocument

    Private _doc As Document
    Public ReadOnly Property Doc() As Document
        Get
            Return _doc
        End Get
    End Property

    Public Sub New()
        _doc = New Document()
    End Sub
    Public Sub New(ByVal doc As Document)
        _doc = doc
    End Sub

    Public Sub Activate()
        _doc.Activate()
    End Sub

End Class
