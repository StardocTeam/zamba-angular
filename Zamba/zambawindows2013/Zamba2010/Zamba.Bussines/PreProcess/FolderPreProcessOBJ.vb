Public Class FolderPreProcessOBJ
    Public id As Integer
    Public name As String
    Public Param As String
    Public order As Integer
    Public Sub New()

    End Sub
    Public Sub New(ByVal idp As Integer, ByVal nombre As String, ByVal par As String, ByVal ord As Integer)
        Me.id = idp
        Me.name = nombre
        Me.Param = par
        Me.order = ord
    End Sub
End Class