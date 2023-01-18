Public Class UCDoAdminFiles
    Inherits ZRuleControl

    Dim CurrentRule As IDoAdminFiles

    Public Sub New(ByRef CurrentRule As IDoAdminFiles, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(CurrentRule, _wfPanelCircuit)
        InitializeComponent()
        Me.CurrentRule = CurrentRule
        LoadParams()
    End Sub

    Private Sub UCAdminFiles_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        'Posiciona los controles correctamente
        Dim ancho As Int32 = tbRule.Width - 20
        grpErrors.Width = ancho - grpErrors.Left
        txtErrorVar.Width = grpErrors.Width - 20
        txtSourceVar.Width = ancho
        txtTargetPath.Width = ancho
        btnSaveChanges.Left = ancho - btnSaveChanges.Width
        lblHelp1.Left = ancho - lblHelp1.Width
        lblHelp2.Left = ancho - lblHelp2.Width
        lblHelp3.Left = grpErrors.Width - lblHelp3.Width - 10
    End Sub

    Private Sub LoadParams()
        'Rutas o variables
        txtErrorVar.Text = CurrentRule.ErrorVar
        txtSourceVar.Text = CurrentRule.SourceVar
        txtTargetPath.Text = CurrentRule.TargetPath

        'Accion a ejecutar
        Select Case CurrentRule.Action
            Case FileActions.Move
                rdoMove.Checked = True
                grpOverwrite.Enabled = True
            Case FileActions.Delete
                rdoDelete.Checked = True
            Case Else
                'Caso en que sea copiar y por defecto
                rdoCopy.Checked = True
                grpOverwrite.Enabled = True
        End Select

        'En un futuro se pueden agregar mas tipos de datos de salida
        Select Case CurrentRule.OutputDataType
            Case FWDataTypes.ListOfString
                rdoAsList.Checked = True
            Case Else
                'Caso en que se guarde como cadena y por defecto
                rdoAsString.Checked = True
        End Select

        'Eliminar desde variable de origen o destino
        rdoUseVars.Checked = CurrentRule.DeleteVarFiles
        rdoUseTargetPath.Checked = Not CurrentRule.DeleteVarFiles

        'Sobreescribir archivos
        chkOverwrite.Checked = CurrentRule.Overwrite
        chkWorkWithFiles.Checked = CurrentRule.WorkWithFiles
    End Sub

    Private Sub btnSaveChanges_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnSaveChanges.Click

        CurrentRule.ErrorVar = txtErrorVar.Text.Trim
        CurrentRule.SourceVar = txtSourceVar.Text.Trim
        CurrentRule.TargetPath = txtTargetPath.Text.Trim

        If rdoCopy.Checked Then
            CurrentRule.Action = FileActions.Copy
        ElseIf rdoMove.Checked Then
            CurrentRule.Action = FileActions.Move
        Else
            CurrentRule.Action = FileActions.Delete
        End If

        If rdoAsString.Checked Then
            CurrentRule.OutputDataType = FWDataTypes.Cadena
        Else
            CurrentRule.OutputDataType = FWDataTypes.ListOfString
        End If

        CurrentRule.DeleteVarFiles = rdoUseVars.Checked
        CurrentRule.Overwrite = chkOverwrite.Checked
        CurrentRule.WorkWithFiles = chkWorkWithFiles.Checked

        WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.SourceVar)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.Action)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 2, CurrentRule.TargetPath)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 3, CurrentRule.ErrorVar)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 4, CurrentRule.OutputDataType)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 5, CurrentRule.DeleteVarFiles)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 6, CurrentRule.Overwrite)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 7, CurrentRule.WorkWithFiles)
        UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
    End Sub

    Private Sub rdoDelete_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rdoDelete.CheckedChanged
        grpDeleteOptions.Enabled = rdoDelete.Checked
        grpOverwrite.Enabled = Not rdoDelete.Checked
    End Sub

End Class
