Imports Zamba.Core
'Imports Zamba.WFBusiness
Imports System.Text
Imports System.Collections.Generic
Imports system.windows.Forms
Imports Zamba.Controls
Imports Zamba.AdminControls

'09/08/07 Modificado por: Alejandro 
Partial Public Class UCDOInternalMessage
    Inherits ZRuleControl
#Region "Propiedades y Atributos"
    Private _currentRule As IDOInternalMessage
    Private Const BODY_LENGTH As Int32 = 2000
    Private Const SUBJECT_LENGTH As Int16 = 200
    Public Event MensajeEnviado()

    Public Property CurrentRule() As IDOInternalMessage
        Get
            Return _currentRule
        End Get
        Set(ByVal value As IDOInternalMessage)
            _currentRule = value
        End Set
    End Property
#End Region

#Region "Constructor"
    Public Sub New(ByRef rule As IDOInternalMessage, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(rule, _wfPanelCircuit)
        Me.InitializeComponent()
        Me.CurrentRule = rule
    End Sub
#End Region

#Region "Eventos"
    Private Sub btEnviar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btGuardar.Click
        Try
            If ValidateData() Then
                SaveParamItemsAndValues()
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub tbPara_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbPara.Click
        GetUsers()
    End Sub
    Private Sub tbCC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbCC.Click
        GetUsers()
    End Sub
    Private Sub tbCCO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbCCO.Click
        GetUsers()
    End Sub
    Private Sub UCDOSendInternalMsg_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadValues()
    End Sub
#End Region

#Region "Métodos"

    Private Sub GetUsers()
        Dim users As InternalUserSelector
        Try
            users = New InternalUserSelector(CurrentRule.Msg.Destinatarios)
            If users.ShowDialog() = DialogResult.OK Then
                AddUsersToTextBox()
            End If
            users.Dispose()
        Catch ex As Exception
            MessageBox.Show("No se pudo abrir la lista de selección de usuarios " & ex.ToString, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AddUsersToTextBox()
        'tbPara.Text = ""
        'tbCC.Text = ""
        'tbCCO.Text = ""
        Try
            For Each destinatario As IDestinatario In CurrentRule.Msg.Destinatarios
                Select Case destinatario.Type
                    Case MessageType.MailCC
                        If tbCC.Text.Length = 0 Then
                            tbCC.Text &= destinatario.UserName
                        Else
                            tbCC.Text &= ";" & destinatario.UserName
                        End If
                    Case MessageType.MailCCO
                        If tbCCO.Text.Length = 0 Then
                            tbCCO.Text &= destinatario.UserName
                        Else
                            tbCCO.Text &= ";" & destinatario.UserName
                        End If
                    Case MessageType.MailTo
                        If tbPara.Text.Length = 0 Then
                            tbPara.Text &= destinatario.UserName
                        Else
                            tbPara.Text &= ";" & destinatario.UserName
                        End If
                End Select
            Next
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Saves the input data in the database
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveParamItemsAndValues()

        If Me.tbPara.Text.Length > 0 Then

            Dim _toStr As String = String.Empty
            Dim ToStrArray() As String = Me.tbPara.Text.Split(";")

            For Each s As String In ToStrArray

                Dim user As iuser

                Try
                    user = UserBusiness.GetUserByName(s)

                    If _toStr = String.Empty Then
                        _toStr = user.ID
                    Else
                        _toStr = _toStr & ";" & user.ID
                    End If

                Catch ex As Exception
                    zamba.core.zclass.raiseerror(ex)
                End Try

            Next

            Me.CurrentRule.ToStr = _toStr

        Else
            Me.CurrentRule.ToStr = String.Empty
        End If

        If Me.tbCC.Text.Length > 0 Then

            Dim _CCStr As String = String.Empty
            Dim CCStrArray() As String = Me.tbCC.Text.Split(";")

            For Each s As String In CCStrArray

                Dim user As iuser

                Try
                    user = UserBusiness.GetUserByName(s)

                    If _CCStr = String.Empty Then
                        _CCStr = user.ID
                    Else
                        _CCStr = _CCStr & ";" & user.ID
                    End If

                Catch ex As Exception
                    zamba.core.zclass.raiseerror(ex)
                End Try

            Next

            Me.CurrentRule.CCStr = _CCStr
        Else
            Me.CurrentRule.CCStr = String.Empty
        End If

        If Me.tbCCO.Text.Length > 0 Then

            Dim _CCOStr As String = String.Empty
            Dim CCOStrArray() As String = Me.tbCCO.Text.Split(";")

            For Each s As String In CCOStrArray

                Dim user As iuser

                Try
                    user = UserBusiness.GetUserByName(s)

                    If _CCOStr = String.Empty Then
                        _CCOStr = user.ID
                    Else
                        _CCOStr = _CCOStr & ";" & user.ID
                    End If

                Catch ex As Exception
                    zamba.core.zclass.raiseerror(ex)
                End Try

            Next

            Me.CurrentRule.CCOStr = _CCOStr
        Else
            Me.CurrentRule.CCOStr = String.Empty
        End If

        'Guardo los valores en la regla
        Me.CurrentRule.Msg.Subject = Me.tbAsunto.Text
        Me.CurrentRule.Msg.Body = Me.tbBody.Text
        Me.CurrentRule.OneDocPerMail = Me.chbMail.Checked

        'Guardo los valores en los parámetros
        WFRulesBusiness.UpdateParamItem(MyRule, 0, Me.CurrentRule.ToStr)
        WFRulesBusiness.UpdateParamItem(MyRule, 1, Me.CurrentRule.CCStr)
        WFRulesBusiness.UpdateParamItem(MyRule, 2, Me.CurrentRule.CCOStr)
        WFRulesBusiness.UpdateParamItem(MyRule, 3, Me.CurrentRule.Msg.Subject)
        WFRulesBusiness.UpdateParamItem(MyRule, 4, Me.CurrentRule.Msg.Body)
        WFRulesBusiness.UpdateParamItem(MyRule, 5, Me.CurrentRule.OneDocPerMail)
        UserBusiness.Rights.SaveAction(MyRule.ID, ObjectTypes.WFRules, Zamba.Core.RightsType.Edit, "Se editaron los datos de la regla " & MyRule.Name & "(" & MyRule.ID & ")")
    End Sub
    ''' <summary>
    ''' Loads the input data from the rule
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadValues()

        If Not IsNothing(CurrentRule) Then
            chbMail.Checked = CurrentRule.OneDocPerMail
        End If
        If Not IsNothing(CurrentRule.Msg) Then
            tbAsunto.Text = CurrentRule.Msg.Subject
            tbBody.Text = CurrentRule.Msg.Body
        End If

        If Me.CurrentRule.ToStr.Length > 0 Then

            Dim _toStr As String = String.Empty
            Dim ToStrArray() As String = Me.CurrentRule.ToStr.Split(";")

            For Each s As String In ToStrArray

                Dim user As iuser

                Try
                    user = UserBusiness.GetUserById(CInt(s))

                    If _toStr = String.Empty Then
                        _toStr = user.Name
                    Else
                        _toStr = _toStr & ";" & user.Name
                    End If

                Catch ex As Exception
                    zamba.core.zclass.raiseerror(ex)
                End Try

            Next

            Me.tbPara.Text = _toStr

        End If

        If Me.CurrentRule.CCStr.Length > 0 Then

            Dim _CCStr As String = String.Empty
            Dim ToStrArray() As String = Me.CurrentRule.CCStr.Split(";")

            For Each s As String In ToStrArray

                Dim user As iuser

                Try
                    user = UserBusiness.GetUserById(CInt(s))

                    If _CCStr = String.Empty Then
                        _CCStr = user.Name
                    Else
                        _CCStr = _CCStr & ";" & user.Name
                    End If

                Catch ex As Exception
                    zamba.core.zclass.raiseerror(ex)
                End Try

            Next

            Me.tbCC.Text = _CCStr

        End If

        If Me.CurrentRule.CCOStr.Length > 0 Then

            Dim _CCOStr As String = String.Empty
            Dim ToStrArray() As String = Me.CurrentRule.CCOStr.Split(";")

            For Each s As String In ToStrArray

                Dim user As iuser

                Try
                    user = UserBusiness.GetUserById(CInt(s))

                    If _CCOStr = String.Empty Then
                        _CCOStr = user.Name
                    Else
                        _CCOStr = _CCOStr & ";" & user.Name
                    End If

                Catch ex As Exception
                    zamba.core.zclass.raiseerror(ex)
                End Try

            Next

            Me.tbCCO.Text = _CCOStr

        End If



    End Sub
    ''' <summary>
    ''' Validates the input data 
    ''' </summary>
    ''' <returns>True if it is valid , false if it isnt</returns>
    ''' <remarks></remarks>
    Private Function ValidateData() As Boolean

        If tbAsunto.Text.Length > SUBJECT_LENGTH Then
            MessageBox.Show("El asunto de este mensaje es demasiado largo", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If

        If tbAsunto.Text.Length = 0 Then
            If MessageBox.Show("Este mensaje no tiene asunto. ¿Desea guardarlo igual?", "Zamba", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                CurrentRule.Msg.Subject = tbAsunto.Text
            Else
                Return False
            End If
        End If

        If tbBody.Text.Length > BODY_LENGTH Then
            MessageBox.Show("El mensaje es demasiado largo.", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If

        If tbBody.Text.Length = 0 Then
            If MessageBox.Show("Este mensaje no tiene cuerpo. ¿Desea guardarlo igual?", "Zamba", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                CurrentRule.Msg.Body = ""
            Else
                Return False
            End If
        End If

        Return True
    End Function

#End Region

    'Comentado por: Alejandro 09/08/07
    ''CurrentRule.Msg.ToUser.Clear()
    '' CurrentRule.Msg.ToStr = ""
    'If tbPara.Text.Length > 0 Then
    '    Dim userNames As String() = tbPara.Text.Split(";")
    '    For Each str As String In userNames
    '        Dim user as iuser
    '        Try
    '            user = UserBusiness.GetUserByName(str)
    '        Catch ex As Exception
    '            zamba.core.zclass.raiseerror(New Exception("No se encontro el usuario " & str))
    '            Exit For
    '        End Try
    '        Try
    '            CurrentRule.Msg.TOUSER.Add(user)
    '            CurrentRule.Msg.ToStr &= user.Name & " "
    '        Catch ex As Exception
    '        End Try
    '    Next
    'End If

    ''CurrentRule.Msg.CC.Clear()
    ''CurrentRule.Msg.CCStr = ""
    'If tbCC.Text.Length > 0 Then
    '    Dim userNames As String() = tbCC.Text.Split(";")
    '    For Each str As String In userNames
    '        Dim user as iuser
    '        Try
    '            user = UserBusiness.GetUserByName(str)
    '        Catch ex As Exception
    '            zamba.core.zclass.raiseerror(New Exception("No se encontro el usuario " & str))
    '            Exit For
    '        End Try
    '        Try
    '            CurrentRule.Msg.CC.Add(user)
    '            CurrentRule.Msg.CCStr &= user.Name & " "
    '        Catch ex As Exception
    '        End Try
    '    Next
    'End If

    'CurrentRule.Msg.CCO.Clear()
    'CurrentRule.Msg.CCOStr = ""
    'If tbCCO.Text.Length > 0 Then
    '    Dim userNames As String() = tbCCO.Text.Split(";")
    '    For Each str As String In userNames
    '        Dim user as iuser
    '        Try
    '            user = UserBusiness.GetUserByName(str)
    '        Catch ex As Exception
    '            zamba.core.zclass.raiseerror(New Exception("No se encontro el usuario " & str))
    '            Exit For
    '        End Try
    '        Try
    '            CurrentRule.Msg.CCO.Add(user)
    '            CurrentRule.Msg.CCOStr &= user.Name & " "
    '        Catch ex As Exception
    '        End Try
    '    Next
    'End If
    'CurrentRule.Msg.Body = tbBody.Text
    'CurrentRule.OneDocPerMail = chbMail.Checked
    'If Not IsNothing(CurrentRule) Then
    '    chbMail.Checked = CurrentRule.OneDocPerMail
    'End If
    'If Not IsNothing(CurrentRule.Msg) Then
    '    tbAsunto.Text = CurrentRule.Msg.Subject
    '    tbBody.Text = CurrentRule.Msg.Body
    'End If

    'If Not IsNothing(CurrentRule.Msg.CC) Then
    '    Dim strCC As New StringBuilder
    '    For Each value As Object In CurrentRule.Msg.CC
    '        strCC.Append(DirectCast(value, Zamba.Controls.Destinatario).UserName)
    '        strCC.Append(";")
    '    Next
    '    Dim strUsers As String = strCC.ToString()
    '    strCC = Nothing
    '    If strUsers.Length > 0 Then strUsers = strUsers.Substring(0, strUsers.Length - 1)
    '    tbCC.Text = strUsers
    'End If

    'If Not IsNothing(CurrentRule.Msg.CCO) Then
    '    Dim strCCO As New StringBuilder
    '    For Each value As Object In CurrentRule.Msg.CCO

    '        strCCO.Append(DirectCast(value, Zamba.Controls.Destinatario).UserName)
    '        strCCO.Append(";")

    '    Next
    '    Dim strUsers As String = strCCO.ToString()
    '    strCCO = Nothing
    '    If strUsers.Length > 0 Then strUsers = strUsers.Substring(0, strUsers.Length - 1)
    '    tbCCO.Text = strUsers
    'End If

    'If Not IsNothing(CurrentRule.Msg.TOUSER) Then
    '    Dim strTo As New StringBuilder
    '    For Each value As Object In CurrentRule.Msg.TOUSER
    '        strTo.Append(DirectCast(value, Zamba.Controls.Destinatario).UserName)
    '        strTo.Append(";")
    '    Next
    '    Dim strUsers As String = strTo.ToString()
    '    strTo = Nothing
    '    If strUsers.Length > 0 Then strUsers = strUsers.Substring(0, strUsers.Length - 1)
    '    tbPara.Text = strUsers
    'End If

    'Dim toBuilder As New StringBuilder
    'For Each value As Object In CurrentRule.Msg.TOUSER
    '    Dim Id As Int32 = DirectCast(value, Zamba.Controls.Destinatario).UserID
    '    If Id = 0 Then Exit For
    '    toBuilder.Append(Id)
    '    toBuilder.Append(";")
    '    value = Nothing
    'Next
    'Dim toStr As String = toBuilder.ToString()
    'If toStr.Length > 0 Then toStr = toStr.Substring(0, toStr.Length - 1)
    'toBuilder = Nothing

    'Dim ccBuilder As New StringBuilder
    'For Each value As Object In CurrentRule.Msg.CC
    '    Dim Id As Int32 = DirectCast(value, Zamba.Controls.Destinatario).UserID
    '    If Id = 0 Then Exit For
    '    ccBuilder.Append(Id)
    '    ccBuilder.Append(";")
    '    value = Nothing
    'Next
    'Dim cc As String = ccBuilder.ToString()
    'If cc.Length > 0 Then cc = cc.Substring(0, cc.Length - 1) 'Le saco el ultimo ;
    'ccBuilder = Nothing

    'Dim ccoBuilder As New StringBuilder
    'For Each value As Object In CurrentRule.Msg.CCO
    '    Dim Id As Int32 = DirectCast(value, Zamba.Controls.Destinatario).UserID
    '    If Id = 0 Then Exit For
    '    ccoBuilder.Append(Id)
    '    ccoBuilder.Append(";")
    '    value = Nothing
    'Next
    'Dim cco As String = ccoBuilder.ToString()
    'If cco.Length > 0 Then cco = cco.Substring(0, cco.Length - 1) 'Le saco el ultimo ;
    'ccoBuilder = Nothing

End Class
