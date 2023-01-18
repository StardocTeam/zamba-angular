Public Class UCDoGenerateCoverPage
    Inherits ZRuleControl

    Public Sub New(ByRef myrule As IDoGenerateCoverPage, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(myrule, _wfPanelCircuit)
        InitializeComponent()
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnSave.Click
        Try
            Dim selectedFormat As String = String.Empty

            If Not IsNothing(cboTiposDeDocumento.SelectedItem) OrElse chkUseCurrentTask.Checked Then

                MyRule.UseCurrentTask = chkUseCurrentTask.Checked
                WFRulesBusiness.UpdateParamItem(MyRule.ID, 9, MyRule.UseCurrentTask)

                MyRule.Note = txtnote.Text
                WFRulesBusiness.UpdateParamItem(MyRule.ID, 2, MyRule.Note)

                MyRule.DocTypeId = DirectCast(cboTiposDeDocumento.SelectedItem, DocType).ID
                WFRulesBusiness.UpdateParamItem(MyRule.ID, 0, MyRule.DocTypeId)

                MyRule.PrintIndexs = chkprintindexs.Checked
                WFRulesBusiness.UpdateParamItem(MyRule.ID, 1, MyRule.PrintIndexs)

                MyRule.SetPrinter = chkSetPrinter.Checked
                WFRulesBusiness.UpdateParamItem(MyRule.ID, 3, MyRule.SetPrinter)

                If Not IsNumeric(txtCopies.Text) OrElse txtCopies.Text.Equals(String.Empty) Then
                    txtCopies.Text = 0
                End If

                MyRule.Copies = txtCopies.Text
                WFRulesBusiness.UpdateParamItem(MyRule.ID, 4, MyRule.Copies)

                MyRule.continueWithGeneratedDocument = chkContinueWithGeneratedDocument.Checked
                WFRulesBusiness.UpdateParamItem(MyRule.ID, 5, MyRule.continueWithGeneratedDocument)

                MyRule.DontOpenTaskAfterInsert = Not chkDontOpenTaskAfterInsert.Checked
                WFRulesBusiness.UpdateParamItem(MyRule.ID, 6, MyRule.DontOpenTaskAfterInsert)

                MyRule.UseTemplate = chkUseTemplate.Checked
                WFRulesBusiness.UpdateParamItem(MyRule.ID, 7, MyRule.UseTemplate)

                MyRule.TemplatePath = txtTemplatePath.Text
                WFRulesBusiness.UpdateParamItem(MyRule.ID, 8, MyRule.TemplatePath)

                If txtTemplateWidth.Text.Equals(String.Empty) OrElse Not txtTemplateWidth.Enabled Then
                    MyRule.templateWidth = 0
                Else
                    MyRule.templateWidth = txtTemplateWidth.Text
                End If
                WFRulesBusiness.UpdateParamItem(MyRule.ID, 11, MyRule.templateWidth)

                If txtTemplateHeigth.Text.Equals(String.Empty) OrElse Not txtTemplateHeigth.Enabled Then
                    MyRule.templateHeight = 0
                Else
                    MyRule.templateHeight = txtTemplateHeigth.Text
                End If
                WFRulesBusiness.UpdateParamItem(MyRule.ID, 12, MyRule.templateHeight)

                If IsNumeric(txtCopiesCount.Text) OrElse txtCopiesCount.Text.Equals(String.Empty) Then
                    If txtCopiesCount.Text.Equals(String.Empty) OrElse Int32.Parse(txtCopiesCount.Text) <= 0 Then
                        txtCopiesCount.Text = 1
                    End If
                End If

                MyRule.CopiesCount = txtCopiesCount.Text
                WFRulesBusiness.UpdateParamItem(MyRule.ID, 10, MyRule.CopiesCount)

                UserBusiness.Rights.SaveAction(MyRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & MyRule.Name & "(" & MyRule.ID & ")")
            Else
                MsgBox("Debe seleccionar un entidad o utilizar la tarea actual")
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub


#Region "Eventos"
    Private Sub UCAddAsociatedDocuments_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles Me.Load, MyBase.Load
        Try
            cboTiposDeDocumento.DataSource = DocTypesBusiness.GetDocTypesArrayList
            cboTiposDeDocumento.DisplayMember = "Name"
            If Not MyRule.DocTypeId = 0 Then
                For Each CurrentDocType As DocType In cboTiposDeDocumento.Items
                    If MyRule.DocTypeId = CurrentDocType.ID Then
                        cboTiposDeDocumento.SelectedItem = CurrentDocType
                        Exit For
                    End If
                Next
            End If
            chkUseCurrentTask.Checked = MyRule.UseCurrentTask
            chkDontOpenTaskAfterInsert.Checked = Not MyRule.DontOpenTaskAfterInsert
            chkprintindexs.Checked = MyRule.PrintIndexs
            txtnote.Text = MyRule.Note

            txtCopies.Text = MyRule.Copies
            If IsNumeric(txtCopies.Text) OrElse txtCopies.Text.Equals(String.Empty) Then
                If txtCopies.Text.Equals(String.Empty) OrElse Int32.Parse(txtCopies.Text) <= 0 Then
                    txtCopies.Text = 0
                End If
            End If

            chkSetPrinter.Checked = MyRule.SetPrinter
            chkContinueWithGeneratedDocument.Checked = MyRule.continueWithGeneratedDocument
            chkUseTemplate.Checked = MyRule.UseTemplate
            txtTemplatePath.Text = MyRule.TemplatePath

            txtTemplateWidth.Text = MyRule.templateWidth
            txtTemplateHeigth.Text = MyRule.templateHeight

            txtCopiesCount.Text = MyRule.CopiesCount
            If IsNumeric(txtCopiesCount.Text) OrElse txtCopiesCount.Text.Equals(String.Empty) Then
                If txtCopiesCount.Text.Equals(String.Empty) OrElse Int32.Parse(txtCopiesCount.Text) <= 0 Then
                    txtCopiesCount.Text = 1
                End If
            End If

            If chkUseTemplate.Checked Then
                txtTemplatePath.Enabled = True
                btnBrowse.Enabled = True
                txtTemplateWidth.Enabled = True
                txtTemplateHeigth.Enabled = True
            Else
                txtTemplatePath.Enabled = False
                btnBrowse.Enabled = False
                txtTemplateWidth.Enabled = False
                txtTemplateHeigth.Enabled = False
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

#End Region

#Region "Propiedades"
    Public Shadows ReadOnly Property MyRule() As IDoGenerateCoverPage
        Get
            Return DirectCast(Rule, IDoGenerateCoverPage)
        End Get
    End Property

#End Region

    Private Sub chkUseTemplate_CheckedChanged(sender As Object, e As EventArgs) Handles chkUseTemplate.CheckedChanged
        Try
            If chkUseTemplate.Checked Then
                txtTemplatePath.Enabled = True
                btnBrowse.Enabled = True
                txtTemplateWidth.Enabled = True
                txtTemplateHeigth.Enabled = True
            Else
                txtTemplatePath.Enabled = False
                btnBrowse.Enabled = False
                txtTemplateWidth.Enabled = False
                txtTemplateHeigth.Enabled = False
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Try
            Dim ofd As New OpenFileDialog()
            If ofd.ShowDialog() = DialogResult.OK Then
                txtTemplatePath.Text = ofd.FileName
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ZLabel1_Click(sender As Object, e As EventArgs) Handles ZLabel1.Click

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub chkUseCurrentTask_CheckedChanged(sender As Object, e As EventArgs) Handles chkUseCurrentTask.CheckedChanged
        If chkUseCurrentTask.Checked Then
            cboTiposDeDocumento.Enabled = False
        Else
            cboTiposDeDocumento.Enabled = True
        End If
    End Sub
End Class
