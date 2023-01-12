Public Class Folder


    Public Sub New()
    End Sub


    Public Sub New(PATH As String, NOMBRE As String, ID As Int64, NOMBREMAQUINA As String, SERVICE As Boolean, USER_ID As Int64, TIMER As Int64)
        Me.Path = PATH
        Me.Nombre = NOMBRE
        Me.ID = ID
        Me.NOMBREMAQUINA = NOMBREMAQUINA
        Me.Service = SERVICE
        Me.User_Id = USER_ID
        Me.Timer = TIMER
    End Sub

    Public Property ID As Decimal
    Public Property Service As Boolean
    Public Property Nombre As String
    Public Property Path As String
    Public Property Timer As Integer
    Public Property User_Id As Integer
    Public Property NOMBREMAQUINA As String
End Class
