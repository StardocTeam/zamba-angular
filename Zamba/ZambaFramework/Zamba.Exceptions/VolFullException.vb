Public Class VolFullException
    Inherits ZException
    Public Sub New(ByVal ex As Exception, ByVal Reason As String)
        MyBase.New()
    End Sub
End Class
