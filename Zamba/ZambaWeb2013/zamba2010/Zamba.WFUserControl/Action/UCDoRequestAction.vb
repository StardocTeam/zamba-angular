Imports System.Text

''' <summary>
''' UserControl que se encarga de configurar la regla DoRequestAction
''' </summary>
''' <remarks></remarks>
Public Class UCDoRequestAction
    Inherits ZRuleControl

    ''' <summary>
    ''' Clave donde se encuentra el valor que donde va a ser mandado los mails. La clave es usada tanto en el UserConfig como en la tabla Zopt.
    ''' </summary>
    ''' <remarks>EJ: "www.stardoc.com.ar/requestaction.aspx"</remarks>
    Private Const KEY_SERVER_LOCATION As String = "RequestActionServerLocation"

#Region "Atributos"
    'Como el checkedListbox no contiene un tag , no puedo guardar los ids de reglas y usuarios 
    'ahi , por eso uso listas privadas 
    Private _userList As List(Of Int64) = Nothing
    Private _ruleList As List(Of Int64) = Nothing
#End Region

    Private Shadows ReadOnly Property MyRule() As IDoRequestAction
        Get
            Return DirectCast(Rule, IDoRequestAction)
        End Get
    End Property

    ''' <summary>
    ''' Valida si la informacion ingresada en el formulario es valida.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsFormValid() As Boolean
        Dim IsValid As Boolean = True

        If String.IsNullOrEmpty(tbServerLocation.Text) Then
            MessageBox.Show("Debe ingresar un valor en el campo URL server.")
            IsValid = False
        End If

        Return IsValid
    End Function

#Region "Eventos"
    Private Sub clsRules_ItemCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles clsRules.ItemCheck
        Try
            Dim CurrentRuleId As Int64 = _ruleList(e.Index)
            If e.NewValue = CheckState.Checked Then
                MyRule.RuleIds.Add(CurrentRuleId)
            Else
                MyRule.RuleIds.Remove(CurrentRuleId)
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub clsUsers_ItemCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles clsUsers.ItemCheck
        Try
            Dim CurrentUserId As Int64 = _userList(e.Index)
            If e.NewValue = CheckState.Checked Then
                MyRule.UserIds.Add(CurrentUserId)
            Else
                MyRule.UserIds.Remove(CurrentUserId)
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se presiona sobre el checkbox "Enviar al usuario asignado"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	30/10/2008	Created
    ''' </history>
    Private Sub chkSendTheUserAssigned_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkSendTheUserAssigned.CheckedChanged

        If (chkSendTheUserAssigned.Checked = True) Then
            clsUsers.Enabled = False
        Else
            clsUsers.Enabled = True
        End If

    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se presiona el botón Guardar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	30/07/2008	Modified
    '''     [Gaston]	30/10/2008	Modified    Guardado o no del checkbox "Enviar al usuario asignado"
    ''' </history>
    Private Sub btSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btSave.Click
        If IsFormValid() Then
            Try
                WFRulesBusiness.UpdateParamItem(MyRule.ID, 0, Membership.MembershipHelper.CurrentUser.ID)

                Dim ParameterBuilder As New StringBuilder()

                For Each CurrentRuleId As Int64 In MyRule.RuleIds
                    ParameterBuilder.Append(CurrentRuleId.ToString())
                    ParameterBuilder.Append(";")
                Next

                If (ParameterBuilder.Length > 0) Then
                    ParameterBuilder.Remove(ParameterBuilder.Length - 1, 1)
                End If
                WFRulesBusiness.UpdateParamItem(MyRule.ID, 1, ParameterBuilder.ToString())

                ParameterBuilder.Remove(0, ParameterBuilder.Length)

                ' Si el checkbox "Enviar al usuario asignado" no está activado entonces se guardan en la base de datos los usuarios que se hayan 
                ' cliqueado en la lista de usuarios, de lo contrario se guardara "userAssigned" que indicará que el mail se enviara al usuario
                ' que está asignado a la tarea sobre la que se quiere ejecutar la regla
                If (chkSendTheUserAssigned.Checked = False) Then

                    ' Si la colección UsersIds contiene -1 quiere decir que cuando se presiono sobre la regla, el checkbox "Enviar al usuario asignado"
                    ' estaba activado, por lo tanto hay que eliminar el -1 de la colección que identificaba que el correspondiente checkbox estaba
                    ' activado
                    If (MyRule.UserIds.Contains(-1)) Then
                        MyRule.UserIds.Remove(-1)
                    End If

                    For Each CurrentUserId As Int64 In MyRule.UserIds
                        ParameterBuilder.Append(CurrentUserId.ToString())
                        ParameterBuilder.Append(";")
                    Next

                    If (ParameterBuilder.Length > 0) Then
                        ParameterBuilder.Remove(ParameterBuilder.Length - 1, 1)
                    End If

                    WFRulesBusiness.UpdateParamItem(MyRule.ID, 2, ParameterBuilder.ToString())

                    ParameterBuilder.Remove(0, ParameterBuilder.Length)

                Else

                    MyRule.UserIds.Clear()
                    MyRule.UserIds.Add(-1)
                    WFRulesBusiness.UpdateParamItem(MyRule.ID, 2, "userAssigned")
                End If

                ParameterBuilder = Nothing
                MyRule.Subject = txtSubject.Text

                MyRule.NotificationMessage = txtMessage.Text
                WFRulesBusiness.UpdateParamItem(MyRule.ID, 3, MyRule.NotificationMessage)

                MyRule.ServerLocation = tbServerLocation.Text
                WFRulesBusiness.UpdateParamItem(MyRule.ID, 4, MyRule.ServerLocation)

                MyRule.LinkMail = txtLinkMail.Text

                WFRulesBusiness.UpdateParamItem(MyRule.ID, 5, MyRule.Name)
                WFRulesBusiness.UpdateParamItem(MyRule.ID, 6, MyRule.Subject)

                WFRulesBusiness.UpdateParamItem(MyRule.ID, 7, MyRule.LinkMail)
                UserBusiness.Rights.SaveAction(MyRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & MyRule.Name & "(" & MyRule.ID & ")")
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End If
    End Sub

#End Region

#Region "Constructores"

    ''' <summary>
    ''' Constructor del usercontrol de la regla RequestAction
    ''' </summary>
    ''' <param name="rule"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	30/10/2008	Modified    
    ''' </history>
    Public Sub New(ByVal rule As IDoRequestAction, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(rule, _wfPanelCircuit)
        InitializeComponent()

        _userList = New List(Of Int64)
        _ruleList = New List(Of Int64)

        If String.IsNullOrEmpty(rule.ServerLocation) Then
            rule.ServerLocation = UserPreferences.getValue(KEY_SERVER_LOCATION, UPSections.UserPreferences, String.Empty)

            If String.IsNullOrEmpty(rule.ServerLocation) Then
                rule.ServerLocation = ZOptBusiness.GetValue(KEY_SERVER_LOCATION)
            End If

        End If

        tbServerLocation.Text = rule.ServerLocation

        Dim CurrentStep As WFStep = WFStepBusiness.GetStepById(MyRule.WFStepId)
        'WFRulesBusiness.FillRules(CurrentStep, False)
        Dim rules As DsRules.WFRulesDataTable = CurrentStep.DsRules.WFRules ' WFRulesBusiness.GetRulesByStep(MyRule.WFStepId)
        '        rules.Sort(New RulesComparer())

        Try
            For Each CurrentRule As DsRules.WFRulesRow In rules
                If CurrentRule.ParentId = 0 AndAlso Not CurrentRule._Class.ToUpper.StartsWith("IF") Then

                    If MyRule.RuleIds.Contains(CurrentRule.Id) Then
                        clsRules.Items.Add(CurrentRule.Name, True)
                    Else
                        clsRules.Items.Add(CurrentRule.Name, False)
                    End If

                    _ruleList.Add(CurrentRule.Id)
                End If
            Next

            clsUsers.Items.Clear()
            _userList.Clear()

            Dim CurrentUser As IUser = Nothing
            Dim Users As ArrayList = UserBusiness.GetUsersArrayList(True)
            Users.Sort(New UsersComparer())

            For Each CurrentItem As Object In Users
                If TypeOf CurrentItem Is IUser Then
                    CurrentUser = DirectCast(CurrentItem, IUser)

                    If MyRule.UserIds.Contains(CurrentUser.ID) Then
                        clsUsers.Items.Add(CurrentUser.Nombres + " " + CurrentUser.Apellidos, True)
                    Else
                        clsUsers.Items.Add(CurrentUser.Nombres + " " + CurrentUser.Apellidos, False)
                    End If

                    _userList.Add(CurrentUser.ID)
                End If
            Next

            ' Si UsersIds contiene -1 entonces se activa el checkbox "Enviar al usuario asignado" y se desactiva la lista de usuarios
            If (MyRule.UserIds.Contains(-1)) Then
                chkSendTheUserAssigned.Checked = True
                clsUsers.Enabled = False
            Else
                chkSendTheUserAssigned.Checked = False
                clsUsers.Enabled = True
            End If

            CurrentUser.Dispose()
            CurrentUser = Nothing

            'Dim message() As String = rule.NotificationMessage.Split("þ")
            txtSubject.Text = rule.Subject
            txtMessage.Text = rule.NotificationMessage
            'txtMessage.Text = rule.NotificationMessage

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub
#End Region

    Private Class UsersComparer
        Implements IComparer


        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
            Return New CaseInsensitiveComparer().Compare(DirectCast(x, User).Nombres + DirectCast(x, User).Apellidos, DirectCast(y, User).Nombres + DirectCast(y, User).Apellidos)
        End Function

    End Class

    Private Class RulesComparer
        Implements IComparer(Of IWFRuleParent)

        Public Function Compare(ByVal x As IWFRuleParent, ByVal y As IWFRuleParent) As Integer Implements IComparer(Of Core.IWFRuleParent).Compare
            Return New CaseInsensitiveComparer().Compare(x.Name, y.Name)
        End Function

    End Class

End Class