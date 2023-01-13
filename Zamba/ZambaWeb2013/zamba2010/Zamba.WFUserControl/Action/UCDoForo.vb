Public Class UCDoForo
    Inherits Zamba.WFUserControl.ZRuleControl

    Dim CurrentRule As IDoForo

    Private Event OpenMissedRule(ByVal workflowId As Int64, ByVal ruleId As Int64)
    Private Delegate Sub ChangeCursorDelegate(ByVal cur As Cursor)

    Private Sub ChangeCursor(ByVal cur As Cursor)
        Try
            Cursor = cur
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shadows ReadOnly Property MyRule() As IDoForo
        Get
            Return DirectCast(Rule, IDoForo)
        End Get
    End Property

    Public Sub New(ByRef _CurrentRule As IDoForo, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(_CurrentRule, _wfPanelCircuit)
        InitializeComponent()
        CurrentRule = _CurrentRule
        txtIdMensaje.Text = CurrentRule.IdMensaje
        Try
            RemoveHandler OpenMissedRule, AddressOf _wfPanelCircuit.OpenMissedRule
            AddHandler OpenMissedRule, AddressOf _wfPanelCircuit.OpenMissedRule
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        ExecuteRuleConfiguration_load()
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click

        If ValidarCampos() Then

            CurrentRule.Body = txtBody.Text
            CurrentRule.Subject = txtAsunto.Text
            CurrentRule.IdMensaje = txtIdMensaje.Text
            CurrentRule.Participantes = txtParticipantes.Text
            CurrentRule.Automatic = chkautomatic.Checked

            WFRulesBusiness.UpdateParamItem(CurrentRule, 0, CurrentRule.Subject)
            WFRulesBusiness.UpdateParamItem(CurrentRule, 1, CurrentRule.Body)
            WFRulesBusiness.UpdateParamItem(CurrentRule, 2, CurrentRule.IdMensaje)
            WFRulesBusiness.UpdateParamItem(CurrentRule, 3, CurrentRule.Participantes)
            WFRulesBusiness.UpdateParamItem(CurrentRule, 4, CurrentRule.Automatic)


            'pablo - funcionalidad de ejecucion de regla 07032012
            If CboForumRules.SelectedValue <> Nothing Then
                MyRule.ExecuteRuleID = Int32.Parse(CboForumRules.SelectedValue)
            Else
                MyRule.ExecuteRuleID = -1
            End If

            CurrentRule.BtnName = txtbtnName.Text

            WFRulesBusiness.UpdateParamItem(CurrentRule, 5, CurrentRule.ExecuteRuleID)
            WFRulesBusiness.UpdateParamItem(CurrentRule, 6, CurrentRule.BtnName)

            UserBusiness.Rights.SaveAction(CurrentRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & CurrentRule.Name & "(" & CurrentRule.ID & ")")
        End If

    End Sub
    Private Sub ExecuteRuleConfiguration_load()
        Try
            'cargo el combo de reglas
            Dim dt As DataTable = WFRulesBusiness.GetDoExecuteRules().Tables(0)
            CboForumRules.DataSource = dt
            CboForumRules.DisplayMember = dt.Columns(0).ColumnName 'name
            CboForumRules.ValueMember = dt.Columns(1).ColumnName   'ID
            CboForumRules.SelectedValue = MyRule.ExecuteRuleID

            txtbtnName.Text = CurrentRule.BtnName
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    'Realiza las validaciones de los campos de texto
    Private Function ValidarCampos() As Boolean

        'Si el campo Asunto está vacío retorna Falso
        If String.IsNullOrEmpty(txtAsunto.Text) Then
            MessageBox.Show("Falta completar el campo Asunto.", "Atención", MessageBoxButtons.OK)
            Return False

            'Si el campo Mensaje está vacío retorna Falso
        ElseIf String.IsNullOrEmpty(txtBody.Text) Then
            MessageBox.Show("Falta completar el campo Texto.", "Atención", MessageBoxButtons.OK)
            Return False

            'Si todo es correcto retorna True
        Else
            Return True
        End If

    End Function


    Private Sub UCDoForo_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        'Si la regla no viene vacía en la propiedad Subject, lo carga en el Control
        If Not String.IsNullOrEmpty(CurrentRule.Subject) Then
            txtAsunto.Text = CurrentRule.Subject
        End If
        'Si la regla no viene vacía en la propiedad Body, lo carga en el Control
        If Not String.IsNullOrEmpty(CurrentRule.Body) Then
            txtBody.Text = CurrentRule.Body
        End If
        'Si la regla no viene vacía en la propiedad idmensaje, lo carga en el Control
        If Not String.IsNullOrEmpty(CurrentRule.IdMensaje) Then
            txtIdMensaje.Text = CurrentRule.IdMensaje
        End If
        If Not String.IsNullOrEmpty(CurrentRule.Participantes) Then
            txtParticipantes.Text = CurrentRule.Participantes
        End If
        chkautomatic.Checked = CurrentRule.Automatic
    End Sub

    Private Sub BtnCleanRuleValues_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnCleanRuleValues.Click
        CboForumRules.SelectedValue = -1
        txtbtnName.Text = String.Empty
    End Sub

    Private Sub btnForoRules_Click(sender As System.Object, e As EventArgs) Handles btnForoRules.Click
        If Not IsNothing(CboForumRules.SelectedValue) Then
            Dim wfbe As New WFBusinessExt
            Try
                Invoke(New ChangeCursorDelegate(AddressOf ChangeCursor), New Object() {Cursors.WaitCursor})
                Dim ruleId As Int64 = Int64.Parse(CboForumRules.SelectedValue)
                RaiseEvent OpenMissedRule(wfbe.GetWorkflowIdByRule(ruleId), ruleId)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                wfbe = Nothing
                Invoke(New ChangeCursorDelegate(AddressOf ChangeCursor), New Object() {Cursors.Default})
            End Try
        End If
    End Sub
End Class
