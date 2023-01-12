Public Class UCIfDesign
    Inherits ZRuleControl

    Dim CurrentRule As IIfDesign
    Private Event updatePanelCircuit(ByVal docId As Int64)
    Public Sub New(ByRef IfDesign As IIfDesign, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(IfDesign, _wfPanelCircuit)
        InitializeComponent()
        Try
            CurrentRule = IfDesign
            txtHelp.Text = CurrentRule.Help
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Contructor para realizar inyeccion de código. Se le esta pasando "wf panel circuit"
    ''' </summary>
    ''' <param name="IfDesign">Regla</param>
    ''' <param name="_wfPanelCircuit">Árbol de Work Flow</param>
    ''' <remarks></remarks>
    Public Sub New(ByRef IfDesign As IIfDesign, ByRef _wfPanelCircuit As IIfWFPanelCircuit)
        MyBase.New(IfDesign, _wfPanelCircuit)
        InitializeComponent()
        Try
            CurrentRule = IfDesign
            txtHelp.Text = CurrentRule.Help
            RemoveHandler updatePanelCircuit, AddressOf _wfPanelCircuit.UpdateRuleTypeIfDesign
            AddHandler updatePanelCircuit, AddressOf _wfPanelCircuit.UpdateRuleTypeIfDesign
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Guarda la descripción de lo que debe hacer la regla.
    ''' </summary>
    ''' <param name="sender">Botón</param>
    ''' <param name="e">Acción</param>
    ''' <remarks></remarks>
    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnSave.Click
        Try
            If txtHelp.TextLength > 4000 Then
                MessageBox.Show("El texto de diseño no puede superar los 4000 caracteres", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                CurrentRule.Help = txtHelp.Text
                WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.Help)
                UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Convierte a la regla que corresponde. La que se eligio del listado de reglas.
    ''' </summary>
    ''' <param name="sender">Botón</param>
    ''' <param name="e">Acción</param>
    ''' <remarks></remarks>
    Private Sub btnConvertir_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnConvertir.Click
        Dim frmRule As New UCRules(True)

        frmRule.ShowDialog()
        Dim className As String = frmRule.ClassName()

        If Not String.IsNullOrEmpty(className) Then
            WFRulesBusiness.updateClass(Rule.ID, className)

            WFBusiness.SetRulesPreferences(Rule.ID, RuleSectionOptions.Configuracion, RulePreferences.RuleHelp, 0, txtHelp.Text)

            Rule.Description = txtHelp.Text

            RaiseEvent updatePanelCircuit(Rule.ID)
        End If
    End Sub
End Class