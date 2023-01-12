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
        Me.txtUserName.Text = message.UserName
        Me.txtCreationTime.Text = message.Fecha.ToString

        'Verifica si debe mostrar la fecha de Vencimiento para los mensajes
        If Boolean.Parse(UserPreferences.getValue("ShowExpireTimeForForumMesseges", Sections.UserPreferences, "False")) Then
            Me.txtExpireTime.Visible = True
            Me.lblExpireTime.Visible = True

            'Se carga el vencimiento.
            Me.txtExpireTime.Text = GetExpireTime(message.Fecha, message.DiasVto)
        Else
            Me.txtExpireTime.Visible = False
            Me.lblExpireTime.Visible = False
        End If

        'Verifica si debe mostrar el id del mensaje seleccionado.
        If Boolean.Parse(UserPreferences.getValue("ShowForumMessageId", Sections.UserPreferences, "False")) Then
            Me.txtMessageId.Visible = True
            Me.lblMessage.Visible = True
            Me.txtMessageId.Text = message.ID
        Else
            Me.txtMessageId.Visible = False
            Me.lblMessage.Visible = False
        End If
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
