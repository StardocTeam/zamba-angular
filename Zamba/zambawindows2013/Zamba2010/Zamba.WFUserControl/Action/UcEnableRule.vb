'Imports Zamba.WFBusiness

'Los controles de Reglas de Accion deben heredar de ZRuleControl
Public Class UcEnableRule
    Inherits ZRuleControl

#Region "Ctor"
    'Lista con los las reglas de workflow
    Private rules As New List(Of WFRuleParent)

    'Regla que se va a configurar
    Private m_CurrentRule As IDoEnableRule

    'El New debe recibir la regla a configurar
    Public Sub New(ByVal p_CurrentRule As IDoEnableRule, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(p_CurrentRule, _wfPanelCircuit)
        InitializeComponent()
        m_CurrentRule = p_CurrentRule
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        rules.Clear()
        rules = Nothing
    End Sub
#End Region

#Region "Metodos"
    'Carga todas las reglas
    Private Sub CargarReglas()
        rules.Clear()
        Try
            rules.AddRange(WFRulesBusiness.GetRulesByStepId(DirectCast(m_CurrentRule, WFRuleParent).WFStepId, False))
        Catch ex As Exception
            Throw New ArgumentException("No se pueden recuperar las Reglas.")
        End Try
    End Sub

    'Carga las dos listas que contienen las reglas activas y no activas.
    Private Sub CargarListasDeReglas()

        lstReglasActivas.BeginUpdate()
        lstReglasNoActivas.BeginUpdate()

        lstReglasActivas.Items.Clear()
        lstReglasNoActivas.Items.Clear()
        Cursor = Cursors.WaitCursor
        RemoverHandlers()
        Try
            For Each rule As WFRuleParent In rules
                If rule.Enable = False Then
                    lstReglasNoActivas.Items.Add(rule)
                Else
                    lstReglasActivas.Items.Add(rule)
                End If
            Next
        Catch ex As Exception
            Throw New ArgumentException("No se puede Cargar la lista de Reglas.")
        Finally
            Cursor = Cursors.Arrow
            AgregarHandlers()
            lstReglasActivas.EndUpdate()
            lstReglasNoActivas.EndUpdate()
        End Try

    End Sub

    'Refresca la lista de reglas actualizando el estado de cada regla, dependiendo
    'de la lista donde este contenida.
    Private Sub RefrescaListaDeReglas()
        Dim temp_rules As New List(Of WFRuleParent)

        RemoverHandlers()

        lstReglasActivas.BeginUpdate()
        lstReglasNoActivas.BeginUpdate()

        Try
            temp_rules.AddRange(rules)
            rules.Clear()
            For Each rule As WFRuleParent In lstReglasActivas.Items
                rule.Enable = True
                rules.Add(rule)
            Next

            For Each rule As WFRuleParent In lstReglasNoActivas.Items
                rule.Enable = False
                rules.Add(rule)
            Next

        Catch ex As Exception
            'Si hay un error se procede a realizar un rollback.
            If temp_rules.Count > 0 AndAlso temp_rules.Count > rules.Count Then
                rules.Clear()
                rules.AddRange(temp_rules)
            End If
            Throw New ArgumentException("No se pueden actualizar las listas de reglas.")
        Finally
            temp_rules.Clear()
            temp_rules = Nothing
            AgregarHandlers()
            lstReglasActivas.EndUpdate()
            lstReglasNoActivas.EndUpdate()
        End Try
    End Sub
#End Region

#Region "Application"
    'Guarda los cambios de estado de las reglas.
    Private Sub GuardarEstadosDeReglas()

        Try
            Cursor = Cursors.WaitCursor
            'Actualiza la lista de reglas.
            RefrescaListaDeReglas()
            For Each rule As WFRuleParent In rules
                Try
                    WFRulesBusiness.SetRuleEstado(rule.ID, rule.Enable)
                    UserBusiness.Rights.SaveAction(rule.ID, ObjectTypes.WFRules, RightsType.Edit, "El estado de la regla " & rule.Name & "(" & rule.ID & ") se modifica a " & rule.Enable)
                Catch ex As Exception
                End Try
            Next
        Catch ex As Exception
            Throw New System.Exception(ex.Message)
        Finally
            'Vuelve a recuperar la informacion guardada, para que no se produzcan ambiguedades.
            Cursor = Cursors.Arrow
            CargarReglas()
            CargarListasDeReglas()
        End Try
    End Sub
#End Region

#Region "Eventos"
    Private Sub UcDoEnableRule_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles Me.Load
        Try
            CargarReglas()
            CargarListasDeReglas()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Zamba WorkFlow", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub cmdAplicar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles cmdAplicar.Click
        If MessageBox.Show("¿Desea guardar los cambios?", "Zamba WorkFlow", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
            Try
                GuardarEstadosDeReglas()
                MessageBox.Show("Se han guardado los cambios.", "Zamba WorkFlow", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("No se pueden guardar los cambios.", "Zamba WorkFlow", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try
        End If
    End Sub

    Private Sub SwapRuleToEnable(ByVal sender As Object, ByVal e As EventArgs)
        RemoverHandlers()
        Try
            Dim rule As WFRuleParent
            If lstReglasNoActivas.Items.Count >= 0 Then
                rule = DirectCast(lstReglasNoActivas.SelectedItem(), WFRuleParent)
                lstReglasNoActivas.Items.RemoveAt(lstReglasNoActivas.SelectedIndex())
                lstReglasActivas.Items.Add(rule)
            End If
        Catch ex As Exception
            Throw New ArgumentException("No se puede cambiar el estado de las reglas.")
        End Try
        AgregarHandlers()
    End Sub

    Private Sub SwapRuleToDisable(ByVal sender As Object, ByVal e As EventArgs)
        RemoverHandlers()
        Try
            Dim rule As WFRuleParent
            If lstReglasActivas.Items.Count >= 0 Then
                rule = DirectCast(lstReglasActivas.SelectedItem(), WFRuleParent)
                lstReglasActivas.Items.RemoveAt(lstReglasActivas.SelectedIndex)
                lstReglasNoActivas.Items.Add(rule)
            End If
        Catch ex As Exception
            Throw New ArgumentException("No se puede cambiar el estado de las reglas.")
        End Try
        AgregarHandlers()
    End Sub

    Private Sub RemoverHandlers()
        RemoveHandler lstReglasActivas.DoubleClick, AddressOf SwapRuleToDisable
        RemoveHandler lstReglasNoActivas.DoubleClick, AddressOf SwapRuleToEnable
    End Sub

    Private Sub AgregarHandlers()
        RemoverHandlers()
        AddHandler lstReglasActivas.DoubleClick, AddressOf SwapRuleToDisable
        AddHandler lstReglasNoActivas.DoubleClick, AddressOf SwapRuleToEnable
    End Sub

#End Region

End Class
