Public Class UCDoReplaceText
    Inherits ZRuleControl


#Region "Metodos Heredados"
    'Constructor por defecto
    Dim CurrentRule As IDoReplaceText
    Public Sub New(ByRef DoReplaceTextRule As IDoReplaceText, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(DoReplaceTextRule, _wfPanelCircuit)
        Try
            CurrentRule = DoReplaceTextRule
            InitializeComponent()
            If CurrentRule.IsFile Then
                chkFile.Checked = True
            Else
                chkVar.Checked = True
            End If
            txtText.Text = CurrentRule.Text
            txtSaveOnVar.Text = CurrentRule.SaveTextAs
            For Each field As String In CurrentRule.ReplaceFields.Split(New String() {"§"}, StringSplitOptions.RemoveEmptyEntries)
                lstReplaceFields.Items.Add(field)
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub
#End Region


    Private Sub chkVar_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkVar.CheckedChanged, chkFile.CheckedChanged
        Try
            lblTextToReplace.Visible = chkVar.Checked
            lblReadFile.Visible = chkFile.Checked
            btnFile.Visible = chkFile.Checked
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub btnFile_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnFile.Click
        Try
            Dim Dlg As New OpenFileDialog()
            Dlg.DefaultExt = "*.*"
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
                CurrentRule.Text = txtText.Text
                CurrentRule.IsFile = chkFile.Checked
                CurrentRule.SaveTextAs = txtSaveOnVar.Text
                CurrentRule.ReplaceFields = ""
                For Each field As String In lstReplaceFields.Items
                    If Not String.IsNullOrEmpty(field) Then CurrentRule.ReplaceFields += field & "§"
                Next
                If CurrentRule.ReplaceFields.Length = 0 OrElse lstReplaceFields.Items.Count = 0 Then
                    CurrentRule.ReplaceFields = ""
                Else
                    CurrentRule.ReplaceFields = CurrentRule.ReplaceFields.Substring(0, CurrentRule.ReplaceFields.Length - 1)
                End If
                WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.Text)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.ReplaceFields)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 2, CurrentRule.SaveTextAs)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 3, CurrentRule.IsFile)
                UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
            Else
                MessageBox.Show("Debe ingresar un nombre al resultado", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
End Class
