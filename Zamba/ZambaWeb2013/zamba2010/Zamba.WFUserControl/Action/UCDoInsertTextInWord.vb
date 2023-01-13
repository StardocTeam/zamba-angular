Public Class UCDoInsertTextInWord
    Inherits ZRuleControl


#Region "Metodos Heredados"
    'Constructor por defecto
    Dim CurrentRule As IDoInsertTextInWord
    Dim Color As Color
    Dim backColor As Color
    Dim style As Int32

    Public Sub New(ByRef DoInsertTextRule As IDoInsertTextInWord, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(DoInsertTextRule, _wfPanelCircuit)
        Try
            CurrentRule = DoInsertTextRule
            InitializeComponent()
            txtText.Text = CurrentRule.WordPath
            txtSaveOnVar.Text = CurrentRule.NewPath
            txtVariable.Text = CurrentRule.Variable
            txtSection.Text = CurrentRule.Section
            chkSaveOriginal.Checked = CurrentRule.SaveOriginalPath
            chkFontConfig.Checked = CurrentRule.FontConfig
            chkTextAsTable.Checked = CurrentRule.textAsTable
            lblFontSelected.Text = CurrentRule.Font
            lblSizeSelected.Text = CurrentRule.FontSize.ToString()

            style = CurrentRule.Style
            lblStyleSelected.Text = getFontStyle(style).ToString()

            Color = New Color()
            If Not String.IsNullOrEmpty(CurrentRule.Color) Then Color = Color.FromArgb(CurrentRule.Color)
            lblColorSelected.Text = Color.ToString

            backColor = New Color()
            If Not String.IsNullOrEmpty(CurrentRule.backColor) Then backColor = backColor.FromArgb(CurrentRule.backColor)
            lblBackColorSelected.Text = backColor.ToString
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

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnSave.Click
        Try
            If Not String.IsNullOrEmpty(txtSaveOnVar.Text) Then
                CurrentRule.WordPath = txtText.Text
                CurrentRule.NewPath = txtSaveOnVar.Text
                CurrentRule.Variable = txtVariable.Text
                CurrentRule.Section = txtSection.Text
                CurrentRule.SaveOriginalPath = chkSaveOriginal.Checked
                CurrentRule.FontConfig = chkFontConfig.Checked
                CurrentRule.Font = lblFontSelected.Text
                CurrentRule.FontSize = Decimal.Parse(lblSizeSelected.Text)
                CurrentRule.Style = style
                CurrentRule.Color = Color.ToArgb
                CurrentRule.backColor = backColor.ToArgb
                CurrentRule.SaveOriginalPath = chkSaveOriginal.Checked
                CurrentRule.textAsTable = chkTextAsTable.Checked

                WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.WordPath)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.Variable)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 2, CurrentRule.NewPath)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 3, CurrentRule.Section)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 4, CurrentRule.SaveOriginalPath)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 5, CurrentRule.FontConfig)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 6, CurrentRule.Font)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 7, CurrentRule.FontSize)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 8, CurrentRule.Style)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 9, CurrentRule.Color)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 10, CurrentRule.backColor)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 11, CurrentRule.textAsTable)



                UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
            Else
                MessageBox.Show("Debe ingresar un nombre al resultado", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub btnFont_Click(sender As System.Object, e As EventArgs) Handles btnFont.Click
        Try
            If FontDialog1.ShowDialog() = DialogResult.OK Then
                lblFontSelected.Text = FontDialog1.Font.Name.ToString()
                lblSizeSelected.Text = FontDialog1.Font.Size.ToString()
                style = FontDialog1.Font.Style
                lblStyleSelected.Text = getFontStyle(style).ToString()
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try


    End Sub

    Private Function getFontStyle(fStyle As Integer) As System.Drawing.FontStyle
        Select Case fStyle
            Case 1
                Return FontStyle.Bold
            Case 2
                Return FontStyle.Italic
            Case 0
                Return FontStyle.Regular
            Case 8
                Return FontStyle.Strikeout
            Case 4
                Return FontStyle.Underline
        End Select
    End Function


    Private Sub btnFontColor_Click(sender As System.Object, e As EventArgs) Handles btnFontColor.Click
        Try
            If ColorDialog1.ShowDialog() = DialogResult.OK Then
                Color = ColorDialog1.Color
                lblColorSelected.Text = Color.ToString
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Sub btnBackColor_Click(sender As System.Object, e As EventArgs) Handles btnBackColor.Click
        Try
            If ColorDialog2.ShowDialog() = DialogResult.OK Then
                backColor = ColorDialog2.Color
                lblBackColorSelected.Text = backColor.ToString
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub chkFontConfig_CheckedChanged(sender As System.Object, e As EventArgs) Handles chkFontConfig.CheckedChanged
        grpFontConfig.Enabled = chkFontConfig.Checked
    End Sub
End Class
