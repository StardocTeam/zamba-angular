Public Class UCDoCompleteTableInWord
    Inherits ZRuleControl

#Region "Metodos Heredados"
    'Constructor por defecto
    Dim CurrentRule As IDoCompleteTableInWord
    Dim Color As Color
    Dim backColor As Color
    Dim style As Int32

    Public Sub New(ByRef rule As IDoCompleteTableInWord, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(rule, _wfPanelCircuit)
        Try

            CurrentRule = rule
            InitializeComponent()
            txtFile.Text = CurrentRule.FullPath
            txtNumber.Text = CurrentRule.PageIndex
            txtTableNumber.Text = CurrentRule.TableIndex
            chkWithHeader.Checked = CurrentRule.WithHeader
            txtVarName.Text = CurrentRule.VarName
            txtDataTable.Text = CurrentRule.DataTable
            txtRowNumber.Text = CurrentRule.RowNumber
            chkInTable.Checked = CurrentRule.InTable
            chkFontConfig.Checked = CurrentRule.FontConfig
            lblFontSelected.Text = CurrentRule.Font
            lblSizeSelected.Text = CurrentRule.FontSize.ToString()
            style = CurrentRule.Style
            lblStyleSelected.Text = getFontStyle(style).ToString()
            Color = New Color()
            Color = Color.FromArgb(CurrentRule.Color)
            lblColorSelected.Text = Color.ToString
            backColor = New Color()
            backColor = backColor.FromArgb(CurrentRule.BackColor)
            lblBackColorSelected.Text = backColor.ToString
            chkSaveOriginal.Checked = CurrentRule.SaveOriginalPath


            FontDialog1.Font = New Font(CurrentRule.Font, CurrentRule.FontSize, getFontStyle(CurrentRule.Style))

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Sub btnFile_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnFile.Click
        Try
            Dim Dlg As New OpenFileDialog()
            Dlg.DefaultExt = "*.doc;*.docx"
            Dlg.ShowDialog()
            txtFile.Text = Dlg.FileName
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnZambaSave.Click
        Try
            If Not String.IsNullOrEmpty(txtVarName.Text) Then
                CurrentRule.FullPath = txtFile.Text
                CurrentRule.PageIndex = txtNumber.Text
                CurrentRule.TableIndex = txtTableNumber.Text
                CurrentRule.VarName = txtVarName.Text
                CurrentRule.WithHeader = chkWithHeader.Checked
                CurrentRule.DataTable = txtDataTable.Text
                CurrentRule.InTable = chkInTable.Checked
                CurrentRule.RowNumber = txtRowNumber.Text
                CurrentRule.FontConfig = chkFontConfig.Checked
                CurrentRule.Font = lblFontSelected.Text
                If Not Decimal.Parse(lblSizeSelected.Text) = 0 Then
                    CurrentRule.FontSize = Decimal.Parse(lblSizeSelected.Text)
                Else CurrentRule.FontSize = 8
                End If
                CurrentRule.Style = style
                CurrentRule.Color = Color.ToArgb
                CurrentRule.BackColor = backColor.ToArgb
                CurrentRule.SaveOriginalPath = chkSaveOriginal.Checked
                WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.TableIndex)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.PageIndex)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 2, CurrentRule.FullPath)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 3, CurrentRule.VarName)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 4, CurrentRule.WithHeader)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 5, CurrentRule.DataTable)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 6, CurrentRule.InTable)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 7, CurrentRule.RowNumber)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 8, CurrentRule.FontConfig)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 9, CurrentRule.Font)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 10, CurrentRule.FontSize)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 11, CurrentRule.Style)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 12, CurrentRule.Color)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 13, CurrentRule.BackColor)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 14, CurrentRule.SaveOriginalPath)

                UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
                MessageBox.Show("Se han guardado los cambios correctamente", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Else
                MessageBox.Show("Debe ingresar un nombre al resultado", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

#End Region

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

    Private Sub chkFontConfig_CheckedChanged(sender As System.Object, e As EventArgs) Handles chkFontConfig.CheckedChanged
        grpFontConfig.Enabled = chkFontConfig.Checked
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

End Class
