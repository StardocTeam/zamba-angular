'Imports Zamba.Data
'Imports Zamba.Data.MessagesFactory
Imports Zamba.data
''' -----------------------------------------------------------------------------
''' Project	 : ZMessages
''' Class	 : Messages.AutoMailDBFacade
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase para trabajar con los "AutomaticsMails" existentes
''' </summary>
''' <remarks>
''' Implementa el patron FACADE
''' </remarks>
''' <history>
''' 	[Gonzalo]	30/03/2005	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class AutoMailDBFacade

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un Arraylist con los "automails" guardados en la base de datos
    ''' </summary>
    ''' <returns>ArrayList</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' 	[Gaston]	07/08/2008	Modified
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetAutoMailList() As ArrayList
        Dim _automaillist As New ArrayList
        Dim CurrentAutoMail As AutoMail

        Dim DS As DataSet = messagesfactory.GetAutoMailList()

        For Each ir As DataRow In DS.Tables(0).Rows

            CurrentAutoMail = New AutoMail

            Try
                CurrentAutoMail.CC = ir.Item("CC").ToString()
            Catch ex As Exception
                CurrentAutoMail.CC = String.Empty
            End Try
            Try
                CurrentAutoMail.CCO = ir.Item("CCO").ToString()
            Catch ex As Exception
                CurrentAutoMail.CCO = String.Empty
            End Try
            Try
                CurrentAutoMail.MailTo = ir.Item("MailTO").ToString()
            Catch ex As Exception
                CurrentAutoMail.MailTo = String.Empty
            End Try
            Try
                CurrentAutoMail.Name = ir.Item("Name").ToString()
            Catch ex As Exception
                CurrentAutoMail.Name = String.Empty
            End Try
            Try
                CurrentAutoMail.Confirmation = Boolean.Parse(ir.Item("Confirmation").ToString)
            Catch ex As Exception
                CurrentAutoMail.Confirmation = False
            End Try
            Try
                If String.IsNullOrEmpty(ir.Item("PathImages").ToString()) = False Then
                    CurrentAutoMail.PathImages.AddRange(ir.Item("PathImages").ToString().Split(";"))
                Else
                    CurrentAutoMail.PathImages.Clear()
                End If
            Catch ex As Exception
                CurrentAutoMail.PathImages.Clear()
            End Try
            Try
                If String.IsNullOrEmpty(ir.Item("PathFiles").ToString()) = False Then
                    CurrentAutoMail.AttachmentsPaths.AddRange(ir.Item("PathFiles").ToString().Split(";"))
                Else
                    CurrentAutoMail.AttachmentsPaths.Clear()
                End If
            Catch ex As Exception
                CurrentAutoMail.AttachmentsPaths.Clear()
            End Try
            Try
                CurrentAutoMail.ID = Int32.Parse(ir.Item("ID").ToString())
            Catch ex As Exception
                CurrentAutoMail.ID = 0
            End Try
            Try
                'A éste ítem se le agrega un Trim ya que en la tabla se guarda como char
                'y en este tipo los caracteres faltantes para completar el campo se autogeneran
                CurrentAutoMail.Body = ir.Item("Body").ToString().Trim()
            Catch ex As Exception
                CurrentAutoMail.Body = String.Empty
            End Try
            Try
                CurrentAutoMail.Subject = ir.Item("Subject").ToString()
            Catch ex As Exception
                CurrentAutoMail.Body = String.Empty
            End Try

            'CurrentAutoMail.AttachmentsPaths = MessagesBusiness.GetAutomailAttachments(CurrentAutoMail.ID)

            _automaillist.Add(CurrentAutoMail)

        Next
        Return _automaillist
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Guarda las modificaciones realizadas a un Automail en la base de datos
    ''' </summary>
    ''' <param name="automail">Objeto Automail que se desea guardar</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' 	[Marcelo]	22/05/2008	Modified
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub Save(ByVal automail As AutoMail)

        messagesfactory.saveautomail(automail)

        'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
        UserBusiness.Rights.SaveAction(automail.ID, ObjectTypes.RulePreference, RightsType.Edit, "Se ha modificado el automail: " & automail.Name & "(" & automail.ID & ")")
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Crea un objeto "AutoMail"
    ''' </summary>
    ''' <param name="Name">Nombre con que se conocera el "Automail"</param>
    ''' <returns>Objeto Automail</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetNew(ByVal name As String) As AutoMail
        Try
            Dim NewAutomail As New AutoMail
            NewAutomail.From = Membership.MembershipHelper.CurrentUser.eMail.Mail
            NewAutomail.Name = name
            NewAutomail.ID = Zamba.Core.CoreBusiness.GetNewID(IdTypes.AutoMail)
            Insert(NewAutomail)

            Return NewAutomail
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Persiste un objeto "AutoMail" en la base de datos
    ''' </summary>
    ''' <param name="am">Objeto "AutoMail" que se desea persistir</param>
    ''' <remarks>
    ''' Realiza un Insert, si el objeto ya existe utilizar la funcion SAVE
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' 	[Marcelo]	22/05/2008	Modified
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub Insert(ByVal am As AutoMail)
        MessagesFactory.InsertAutoMail(am)

        'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
        UserBusiness.Rights.SaveAction(am.ID, ObjectTypes.WFRules, RightsType.insert, "Se ha creado un nuevo automail: " & am.Name & "(" & am.ID & ")")
    End Sub


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Elimina un objeto AutoMail de la base de datos
    ''' </summary>
    ''' <param name="Am">Objeto que se desea eliminar</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' 	[Marcelo]	22/05/2008	Modified
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub Remove(ByVal Am As AutoMail)
        MessagesFactory.RemoveAutomail(Am.ID)

        'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
        UserBusiness.Rights.SaveAction(Am.ID, ObjectTypes.WFRules, RightsType.Delete, "Se ha eliminado el automail: " & Am.Name & "(" & Am.ID & ")")
    End Sub

    'Public Shared Sub SendAutoMail(ByVal automail As AutoMail)
    '    Dim CurrentAutoMail As IMailMessage = Zamba.Core.MessagesBusiness.GetMessage(Membership.MembershipHelper.CurrentUser.eMail.Type) 'Servers.Server.MailType)
    '    CurrentAutoMail.Attachs = automail._Attach
    '    CurrentAutoMail.Body = automail.Body
    '    CurrentAutoMail.CC = automail.CC
    '    CurrentAutoMail.CCO = automail.CCO
    '    CurrentAutoMail.De = automail.From
    '    CurrentAutoMail.MailTo = automail.MailTo
    '    CurrentAutoMail.Subject = automail.Subject
    '    CurrentAutoMail.send()
    'End Sub

    'Public Shared Sub SendAutoMail2(ByVal automail As AutoMail, ByRef Result As Result, ByVal AddDocument As Boolean, ByVal AddLink As Boolean, ByVal AddIndexs As Boolean)
    '    Dim m As IMessage = MessageFactory.GetMessage(RightFactory.CurrentUser.eMail.Type) 'Servers.Server.MailType)

    '    If m.Confirmation Then
    '        If MessageBox.Show("¿DESEA ENVIAR EL AUTOMAIL " & automail.Subject.ToString.ToUpper & "?", "", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub
    '    End If

    '    If IsNothing(automail._Attach) Then
    '        m.Attachs = New ArrayList
    '    Else
    '        m.Attachs = automail._Attach
    '    End If


    '    'TODO: NO ESTA ARMADA LA PROPIEDAD ATTACHS POR LO QUE QUEDA SIEMPRE EN NOTHING
    '    If AddDocument Then m.Attachs.add(Result)

    '    m.Body = Zamba.Core.TextoInteligente.ReconocerCodigo(automail.Body.Trim)

    '    If AddIndexs Then
    '        Dim TextoIndices As String = "Atributos: " & vbNewLine & vbNewLine
    '        For Each I As Index In Result.Indexs
    '            Dim Dato As String = I.Data
    '            If Dato = String.Empty Then Dato = I.Data2
    '            If Dato = String.Empty Then Dato = I.dataDescription
    '            If Dato = String.Empty Then Dato = I.dataDescription2
    '            TextoIndices += I.Name.Trim.ToUpper & ": " & Dato & vbNewLine
    '        Next
    '        m.Body += vbNewLine & vbNewLine & TextoIndices
    '    End If

    '    If AddLink Then m.Body = m.Body & vbNewLine & "Link:" & vbNewLine & Zamba.Core.Results_Factory.GetLinkFromResult(Result)

    '    m.CC = automail.CC
    '    m.CCO = automail.CCO
    '    m.De = automail.From
    '    m.MailTo = automail.MailTo
    '    m.Subject = automail.Subject
    '    m.send()
    'End Sub
End Class