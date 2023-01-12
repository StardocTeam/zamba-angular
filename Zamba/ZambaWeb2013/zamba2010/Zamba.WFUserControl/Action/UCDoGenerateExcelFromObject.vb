Public Class UCDoGenerateExcelFromObject
    Inherits ZRuleControl


#Region "Metodos Heredados"
    'Constructor por defecto
    Dim CurrentRule As IDoGenerateExcelFromObject
    Public Sub New(ByRef DoGenerateExcelFromObject As IDoGenerateExcelFromObject, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(DoGenerateExcelFromObject, _wfPanelCircuit)
        Try
            CurrentRule = DoGenerateExcelFromObject
            InitializeComponent()
            txtDsName.Text = CurrentRule.DataSetName
            txtExcelName.Text = CurrentRule.ExcelNAme
            chkAddColNom.Checked = CurrentRule.AddColName
            Dim enumtype As Type = GetType(ExcelExportTypes)
            For Each x As ExcelExportTypes In [Enum].GetValues(enumtype)
                lstFormatTypes.Items.Add(x.ToString)
            Next
            If CurrentRule.ExportType.ToString.CompareTo("Otro") = 0 Then
                lstFormatTypes.Items.Add(CurrentRule.OtherFormattype)
            End If
            Dim strslcv As String = CurrentRule.OtherFormattype
            If String.IsNullOrEmpty(strslcv) Then
                strslcv = "Otro"
            End If
            If Not String.IsNullOrEmpty(CurrentRule.Path) Then
                txtpath.Text = CurrentRule.Path
            End If
            lstFormatTypes.SelectedItem = lstFormatTypes.Items(lstFormatTypes.Items.IndexOf(CurrentRule.ExportType.ToString))
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub
#End Region

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnSave.Click
        Try
            CurrentRule.AddColName = chkAddColNom.Checked
            CurrentRule.DataSetName = txtDsName.Text
            CurrentRule.ExcelNAme = txtExcelName.Text
            Dim SavedEnum As ExcelExportTypes
            If [Enum].IsDefined(GetType(ExcelExportTypes), lstFormatTypes.SelectedItem.ToString) = True Then
                SavedEnum = [Enum].Parse(GetType(ExcelExportTypes), lstFormatTypes.SelectedItem.ToString)
            End If
            CurrentRule.ExportType = SavedEnum
            'CurrentRule.ExportType = IIf([Enum].IsDefined(GetType(ExcelExportTypes), Me.lstFormatTypes.SelectedItem.ToString) = True, SavedEnum, ExcelExportTypes.Otro)
            'If Me.lstFormatTypes.SelectedItem.ToString.CompareTo("Otro") = 0 Then
            '    CurrentRule.OtherFormattype = InputBox("Formato del archivo:", "Ingrese Formato").ToString
            'ElseIf [Enum].IsDefined(GetType(ExcelExportTypes), Me.lstFormatTypes.SelectedItem.ToString) = True Then
            '    CurrentRule.OtherFormattype = ""
            'Else
            '    CurrentRule.OtherFormattype = Me.lstFormatTypes.SelectedItem.ToString
            'End If
            CurrentRule.Path = txtpath.Text

            WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.ExcelNAme)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.DataSetName)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 2, CurrentRule.AddColName)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 3, CurrentRule.ExportType)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 4, CurrentRule.OtherFormattype)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 5, CurrentRule.Path)
            UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ZButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ZButton.Click
        Try
            Dim Dlg As New FolderBrowserDialog
            Dlg.ShowDialog()
            txtpath.Text = Dlg.SelectedPath
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
End Class
