Imports Zamba.Servers

Partial Public Class UCMFactoryExt
    Inherits UcmFactory


    ''' <summary>
    ''' Método que verifica si el usuario todavía sigue o no en la tabla UCM
    ''' </summary>
    ''' <param name="UserId">Id de Usuario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Martin]	18/06/2008	Created
    ''' [German] 16/07/2012 Se corrige funcionalidad multisession.Ahora un usuario puede
    '''                     ingresar al cliente si dejo una instancia anterior abierta.
    ''' </history>
    Public Function verifyIfPCNameInUCMMoreThanOnce(ByVal UserId As Int64) As Boolean
        Dim value As Integer = Server.Con.ExecuteScalar(CommandType.Text, "SELECT count(1) FROM UCM Where WINPC not like '%" & My.Computer.Name.ToString & "%' AND USER_ID = " & UserId)

        If (value > 0) Then
            Return False
        Else
            Return True
        End If
    End Function
End Class
