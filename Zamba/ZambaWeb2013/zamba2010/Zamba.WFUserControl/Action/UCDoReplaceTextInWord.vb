Public Class UCDoReplaceTextInWord
    Inherits ZRuleControl


#Region "Metodos Heredados"
    'Constructor por defecto
    Dim CurrentRule As IDoReplaceTextInWord
    Public Sub New(ByRef DoReplaceTextRule As IDoReplaceTextInWord, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(DoReplaceTextRule, _wfPanelCircuit)
        Try
            CurrentRule = DoReplaceTextRule
            InitializeComponent()
            txtText.Text = CurrentRule.WordPath
            txtSaveOnVar.Text = CurrentRule.NewPath
            chkCaseSensitive.Checked = CurrentRule.CaseSensitive
            chkSaveOriginal.Checked = CurrentRule.SaveOriginalPath
            For Each field As String In CurrentRule.ReplaceFields.Split(New String() {"§"}, StringSplitOptions.RemoveEmptyEntries)
                lstReplaceFields.Items.Add(field)
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub
#End Region




    Private Sub btnFile_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnFile.Click
        Try
            Dim Dlg As New OpenFileDialog()
            Dlg.DefaultExt = "*.doc;*.docx"
            Dlg.ShowDialog()
            txtText.Text = Dlg.FileName
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAdd.Click
        Try
            If String.IsNullOrEmpty(txtTextReplace.Text) Then
                MessageBox.Show("Debe completarse el texto a reemplazar", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                If Not lstReplaceFields.Items.Contains(txtTextReplace.Text & "¶" & txtReaplceTo.Text) Then
                    lstReplaceFields.Items.Add(txtTextReplace.Text & "¶" & txtReaplceTo.Text)
                Else
                    MessageBox.Show("Ya existe en la lista", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnRemove.Click
        Try
            If Not lstReplaceFields.SelectedItem Is Nothing Then
                lstReplaceFields.Items.Remove(lstReplaceFields.SelectedItem)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnSave.Click
        Try
            If Not String.IsNullOrEmpty(txtSaveOnVar.Text) Then
                CurrentRule.WordPath = txtText.Text
                CurrentRule.NewPath = txtSaveOnVar.Text
                CurrentRule.ReplaceFields = ""
                CurrentRule.CaseSensitive = chkCaseSensitive.Checked
                CurrentRule.SaveOriginalPath = chkSaveOriginal.Checked
                For Each field As String In lstReplaceFields.Items
                    If Not String.IsNullOrEmpty(field) Then CurrentRule.ReplaceFields += field & "§"
                Next
                If CurrentRule.ReplaceFields.Length = 0 OrElse lstReplaceFields.Items.Count = 0 Then
                    CurrentRule.ReplaceFields = ""
                Else
                    CurrentRule.ReplaceFields = CurrentRule.ReplaceFields.Substring(0, CurrentRule.ReplaceFields.Length - 1)
                End If
                WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.WordPath)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.ReplaceFields)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 2, CurrentRule.NewPath)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 3, CurrentRule.CaseSensitive)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 4, CurrentRule.SaveOriginalPath)
                UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
            Else
                MessageBox.Show("Debe ingresar un nombre al resultado", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
End Class
