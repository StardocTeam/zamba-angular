Imports Zamba.Core

''' <summary>
''' Clase encargada de mostrar información adicional de un mensaje de foro.
''' </summary>
''' <remarks></remarks>
''' <history>
'''     Tomas   19/05/2010  Created
''' </history>
Public Class UcForumDetails

    ''' <summary>
    ''' Carga la información necesaria del mensaje.
    ''' </summary>
    ''' <param name="message">Mensaje de foro seleccionado</param>
    ''' <remarks></remarks>
    Public Sub FillDetails(ByVal message As MensajeForo)
        txtUserName.Text = message.UserName
        txtCreationTime.Text = message.Fecha.ToString   

    End Sub


    ''' <summary>
    ''' Obtiene la fecha de vencimiento
    ''' </summary>
    ''' <param name="fechaCreacion">Fecha de creación del mensaje</param>
    ''' <param name="diasVto">Días de vencimiento</param>
    ''' <returns>Fecha de vencimiento (String)</returns>
    ''' <remarks></remarks>
    Private Function GetExpireTime(ByVal fechaCreacion As DateTime, ByVal diasVto As Int32) As String
        'Devuelve la fecha de creación + los días del vencimiento.
        If diasVto = 0 Then
            Return "-"
        Else
            Return fechaCreacion.AddDays(diasVto).ToString()
        End If
    End Function
End Class
