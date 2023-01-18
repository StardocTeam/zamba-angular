'Imports Zamba.WFBusiness
Imports System.Text

'Los controles de Reglas de Accion deben heredar de ZRuleControl
Public Class UcDoEnableRule
    Inherits ZRuleControl

#Region "Atributos"
    Private m_sReglasSeleccionada_index As String
    Private m_sReglasSeleccionadaId As String = String.Empty
    Private lstReglasSeleccionadasID As New Generic.List(Of Int64)
    'Si el valor de m_bEstado es true, significa que la regla esta activa.
    Private m_bEstado As Boolean
    Private m_bOnlyForTask As Boolean
    Private m_bJoinExecution As Boolean
    Private m_bViewAllSteps As Boolean
    'Private flagChangeView As Boolean = False
#End Region

#Region "Propiedades"
    Public Shadows ReadOnly Property MyRule() As IDoEnableRule
        Get
            Return DirectCast(Rule, IDoEnableRule)
        End Get
    End Property
#End Region

#Region "Constructor"

    'El New debe recibir la regla a configurar
    Public Sub New(ByRef p_CurrentRule As IDoEnableRule, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(p_CurrentRule, _wfPanelCircuit)
        InitializeComponent()

        m_bEstado = MyRule.RuleEstado
        m_bOnlyForTask = MyRule.OnlyForTask
        m_bJoinExecution = MyRule.RuleEjecucion
        m_sReglasSeleccionadaId = MyRule.SelectedRulesIDs
        m_bViewAllSteps = MyRule.ViewAllSteps


        AddHandler btnAplicar.Click, AddressOf cmdAplicar_Click
        Try
            InicializarValores()
            CargarReglas()
            MostrarReglasSeleccionadas(False)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub CargarReglas()
        Try
            Cursor = Cursors.WaitCursor
            If rdbEtapaActual.Checked Then
                Dim tmpDsRules As DsRules
                tmpDsRules = WFRulesBusiness.GetRulesDSByStepID(MyRule.WFStepId, False)

                If Not tmpDsRules.WFRules Is Nothing Then
                    Dim ruleData As DataTable = New DataTable()
                    Dim Rows, t As Int16
                    ruleData.Columns.Add("Name&Id")
                    ruleData.Columns.Add("Id")

                    Rows = DirectCast(tmpDsRules.WFRules, Zamba.Core.DsRules.WFRulesDataTable).Count
                    clbReglasActivas.DataSource = ruleData
                    For t = 0 To Rows - 1
                        ruleData.Rows.Add(tmpDsRules.WFRules.Rows(t).Item(1).ToString + " - (" + tmpDsRules.WFRules.Rows(t).Item(0).ToString + ")")
                        ruleData.Rows(t).Item("Id") = tmpDsRules.WFRules.Rows(t).Item(0).ToString
                    Next

                    clbReglasActivas.DisplayMember = ruleData.Columns(0).ColumnName
                    clbReglasActivas.ValueMember = ruleData.Columns(1).ColumnName
                Else
                    Throw New ArgumentException("No se pueden cargar las reglas.")
                End If
            Else
                Dim tmpDs As DataSet
                Dim wfID As Int64 = WFBusiness.GetWorkflowIdByStepId(MyRule.WFStepId)
                tmpDs = WFRulesBusiness.GetRulesByWorkFlowIDAsDataSet(wfID)
                If Not IsNothing(tmpDs) AndAlso tmpDs.Tables.Count > 0 AndAlso tmpDs.Tables(0).Rows.Count > 0 Then
                    Using tmpDs
                        clbReglasActivas.DataSource = tmpDs.Tables(0)
                        clbReglasActivas.DisplayMember = "ruleFullName"
                        clbReglasActivas.ValueMember = "ruleId"
                    End Using
                Else
                    Throw New ArgumentException("No se pueden cargar las reglas.")
                End If
            End If
        Catch ex As Exception
            Throw New System.Exception(ex.Message)
        Finally
            Cursor = Cursors.Arrow
        End Try
    End Sub

    Private Sub InicializarValores()
        RemoveHandler rdbEtapaActual.CheckedChanged, AddressOf rdbEtapaActual_CheckedChanged
        RemoveHandler rdbTodoElWorkFlow.CheckedChanged, AddressOf rdbTodoElWorkFlow_CheckedChanged
        Try
            If Not String.IsNullOrEmpty(m_sReglasSeleccionadaId) Then

                Dim auxString() As String = m_sReglasSeleccionadaId.Split(Char.Parse(","))
                If IsNothing(lstReglasSeleccionadasID) Then lstReglasSeleccionadasID = New Generic.List(Of Int64)
                For Each s As String In auxString
                    lstReglasSeleccionadasID.Add(Int64.Parse(s))
                Next

                rdbActivarReglas.Checked = m_bEstado
                rdbDesactivarReglas.Checked = Not m_bEstado
                chkDesactivarTareaActual.Checked = m_bOnlyForTask
                ChkEjecutarConTabs.Checked = m_bJoinExecution
                rdbEtapaActual.Checked = Not m_bViewAllSteps
                rdbTodoElWorkFlow.Checked = m_bViewAllSteps

            Else
                rdbActivarReglas.Checked = True
                rdbDesactivarReglas.Checked = False
                rdbEtapaActual.Checked = True
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        AddHandler rdbEtapaActual.CheckedChanged, AddressOf rdbEtapaActual_CheckedChanged
        AddHandler rdbTodoElWorkFlow.CheckedChanged, AddressOf rdbTodoElWorkFlow_CheckedChanged
    End Sub

    Private Sub MostrarReglasSeleccionadas(ByVal bChangeView As Boolean)
        Dim i As Int32 = 0
        Dim selectedRulesIndices As New Generic.List(Of Int32)
        For Each idReglaDesactivada As Int64 In lstReglasSeleccionadasID
            For Each reglaActiva As DataRowView In clbReglasActivas.Items
                'If bChangeView Then
                '    flagChangeView = Not flagChangeView
                '    If Not flagChangeView Then
                '        If Me.rdbEtapaActual.Checked Then
                '            If reglaActiva.Item("Id").ToString() = idReglaDesactivada.ToString() Then
                '                selectedRulesIndices.Add(i)
                '            End If
                '        Else
                '            If reglaActiva.Item("RuleId").ToString() = idReglaDesactivada.ToString() Then
                '                selectedRulesIndices.Add(i)
                '            End If
                '        End If
                '    Else
                '        If Not Me.rdbEtapaActual.Checked Then
                '            If reglaActiva.Item("Id").ToString() = idReglaDesactivada.ToString() Then
                '                selectedRulesIndices.Add(i)
                '            End If
                '        Else
                '            If reglaActiva.Item("RuleId").ToString() = idReglaDesactivada.ToString() Then
                '                selectedRulesIndices.Add(i)
                '            End If
                '        End If
                '    End If
                'Else
                If rdbEtapaActual.Checked Then
                    If reglaActiva.Item("Id").ToString() = idReglaDesactivada.ToString() Then
                        selectedRulesIndices.Add(i)
                    End If
                Else
                    If reglaActiva.Item("RuleId").ToString() = idReglaDesactivada.ToString() Then
                        selectedRulesIndices.Add(i)
                    End If
                End If
                'End If
                i = i + 1
            Next
            i = 0
        Next

        For Each ruleIndex As Int32 In selectedRulesIndices
            clbReglasActivas.SetItemChecked(ruleIndex, True)
        Next

    End Sub

    'Guarda los cambios de estado de las reglas.
    Private Sub GuardarEstadosDeReglas()
        Cursor = Cursors.WaitCursor
        'Guarda el estado para poder realiza un rollback en caso de error.
        Dim idSeleccionados As New StringBuilder()
        Try
            If clbReglasActivas.SelectedIndex > -1 Then

                For Each selectedRuleID As DataRowView In clbReglasActivas.CheckedItems
                    If rdbEtapaActual.Checked Then
                        idSeleccionados.Append(selectedRuleID.Item("Id").ToString())
                        Me.TextBox1.Text = Me.TextBox1.Text & selectedRuleID.Item("Id").ToString()
                    Else
                        idSeleccionados.Append(selectedRuleID.Item("RuleId").ToString())
                        Me.TextBox1.Text = Me.TextBox1.Text & selectedRuleID.Item("RuleId").ToString()
                    End If
                    If clbReglasActivas.CheckedItems.Count > 1 Then
                        idSeleccionados.Append(",")
                        Me.TextBox1.Text = Me.TextBox1.Text & ","
                    End If
                Next
                If clbReglasActivas.CheckedItems.Count > 1 Then
                    idSeleccionados.Remove(idSeleccionados.Length - 1, 1)
                    Me.TextBox1.Text = Me.TextBox1.Text.Remove(Me.TextBox1.Text.Length - 1, 1)
                End If

                m_bEstado = rdbActivarReglas.Checked
                m_bOnlyForTask = chkDesactivarTareaActual.Checked
                m_bJoinExecution = ChkEjecutarConTabs.Checked
                m_bViewAllSteps = rdbTodoElWorkFlow.Checked
                MyRule.SelectedRulesIDs = idSeleccionados.ToString()
                MyRule.SelectedRulesIDs = Me.TextBox1.Text.ToString()

                MyRule.RuleEstado = m_bEstado
                MyRule.OnlyForTask = m_bOnlyForTask
                MyRule.ViewAllSteps = m_bViewAllSteps
                MyRule.RuleEjecucion = m_bJoinExecution
                WFRulesBusiness.UpdateParamItem(MyRule.ID, 0, idSeleccionados.ToString())
                WFRulesBusiness.UpdateParamItem(MyRule.ID, 1, Convert.ToInt32(m_bEstado))
                WFRulesBusiness.UpdateParamItem(MyRule.ID, 2, Convert.ToInt32(m_bOnlyForTask))
                WFRulesBusiness.UpdateParamItem(MyRule.ID, 3, Convert.ToInt32(m_bViewAllSteps))
                WFRulesBusiness.UpdateParamItem(MyRule.ID, 4, Convert.ToInt32(m_bJoinExecution))
                UserBusiness.Rights.SaveAction(MyRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & MyRule.Name & "(" & MyRule.ID & ")")
            End If
        Catch ex As Exception
            Throw New System.Exception(ex.Message)
        Finally
            'Vuelve a recuperar la informacion guardada, para que no se produzcan ambiguedades.
            Cursor = Cursors.Arrow
        End Try
    End Sub

#End Region

#Region "Eventos"

    Private Sub btnRefrescar_Click(ByVal sender As System.Object, ByVal e As EventArgs)
        CargarReglas()
    End Sub

    Private Sub cmdAplicar_Click(ByVal sender As System.Object, ByVal e As EventArgs)
        RemoveHandler btnAplicar.Click, AddressOf cmdAplicar_Click

        If MessageBox.Show("¿Desea guardar los cambios?", "Zamba WorkFlow", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
            Try
                GuardarEstadosDeReglas()
                MessageBox.Show("Se han guardado los cambios.", "Zamba WorkFlow", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("No se pueden guardar los cambios.", "Zamba WorkFlow", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ZClass.raiseerror(ex)
            End Try
        End If

        RemoveHandler btnAplicar.Click, AddressOf cmdAplicar_Click
        AddHandler btnAplicar.Click, AddressOf cmdAplicar_Click
    End Sub

    Private Sub rdbEtapaActual_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rdbEtapaActual.CheckedChanged
        CargarReglas()
        MostrarReglasSeleccionadas(True)
    End Sub

    Private Sub rdbTodoElWorkFlow_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rdbTodoElWorkFlow.CheckedChanged
        CargarReglas()
        MostrarReglasSeleccionadas(True)
    End Sub



#End Region

End Class