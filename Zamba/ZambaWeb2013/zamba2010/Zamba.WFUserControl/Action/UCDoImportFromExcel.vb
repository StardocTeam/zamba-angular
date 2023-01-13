Imports System.Data.OleDb

Public Class UCDoImportFromExcel
    Inherits ZRuleControl


#Region "Atributos"

    Private _currentRule As IDoImportFromExcel
    Private UseSpireConverter As IDoImportFromExcel
#End Region

#Region "Constructor"

    Sub New(ByRef currentrule As IDoImportFromExcel, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(currentrule, _wfPanelCircuit)
        InitializeComponent()
        _currentRule = currentrule
        loadValues()
    End Sub

#End Region


#Region "Metodos Privados"

    ''' <summary>
    ''' Carga los valores a la interfaz
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub loadValues()
        Try

            txtFilePath.Text = _currentRule.File
            txtsheetName.Text = _currentRule.SheetName
            Dim enumtype As Type = GetType(OfficeVersion)
            txtVarName.Text = _currentRule.VarName
            chkSaveCopyAs.Checked = _currentRule.SaveAs
            txtDirPath.Text = _currentRule.SaveAsPath
            txtSaveFileName.Text = _currentRule.SaveAsFileName
            chkUseSpire.Checked = _currentRule.UseSpireConverter

            For Each x As ExcelExportTypes In [Enum].GetValues(enumtype)
                cmbVersion.Items.Add([Enum].GetName(enumtype, x))
            Next
            Dim VersionSelectedItem As Int32 = cmbVersion.Items.IndexOf(_currentRule.ExcelVersion.ToString)
            If VersionSelectedItem <> -1 Then
                cmbVersion.SelectedItem = cmbVersion.Items(VersionSelectedItem)
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

#End Region

    ''' <summary>
    ''' Visualiza los datos del excel.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <History>
    '''     [Ezequiel]    12/11/09 - Created
    ''' </history>
    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnPreview.Click
        Try

            grdTableCols.DataSource = GetExcelData

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub


    ''' <summary>
    ''' Muestra el dialogo de seleccion de archivo excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <History>
    '''     [Ezequiel]    12/11/09 - Created
    ''' </history>
    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnBrowse.Click
        Try
            Dim Dlg As New OpenFileDialog
            Dlg.DefaultExt = "*.xls"
            Dlg.ShowDialog()
            txtFilePath.Text = Dlg.FileName
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Obtiene los datos del excel.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <History>
    '''     [Ezequiel]    12/11/09 - Created
    ''' </history>
    Private Function GetExcelData() As DataTable
        Dim connectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & txtFilePath.Text & ";Extended Properties=" & Chr(34) & "Excel 8.0;HDR=Yes;" & Chr(34) & ";"

        Dim strSQL As String = "SELECT * FROM [" & txtsheetName.Text.Trim & "$]"
        Dim dTable As New DataTable()
        Using excelConnection As OleDbConnection = New OleDbConnection(connectionString)
            excelConnection.Open()
            Dim dbCommand As OleDbCommand = New OleDbCommand(strSQL, excelConnection)
            Dim dataAdapter As OleDbDataAdapter = New OleDbDataAdapter(dbCommand)
            dataAdapter.Fill(dTable)
        End Using
        Return dTable
    End Function


    ''' <summary>
    ''' Guarda los parametros de la regla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <History>
    '''     [Ezequiel]    12/11/09 - Created
    ''' </history>
    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnSave.Click
        Try
            _currentRule.ExcelVersion = [Enum].Parse(GetType(OfficeVersion), cmbVersion.SelectedItem.ToString)
            _currentRule.File = txtFilePath.Text
            _currentRule.SheetName = txtsheetName.Text
            _currentRule.VarName = txtVarName.Text
            _currentRule.UseSpireConverter = chkUseSpire.Checked

            If chkSaveCopyAs.Checked AndAlso (txtDirPath.Text.Trim() = String.Empty OrElse txtSaveFileName.Text.Trim() = String.Empty) Then
                MsgBox("Los datos están incompletos", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "Error al guardar...")
            Else
                WFRulesBusiness.UpdateParamItem(Rule.ID, 0, _currentRule.File)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 1, _currentRule.ExcelVersion)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 2, _currentRule.SheetName)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 3, _currentRule.VarName)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 7, _currentRule.UseSpireConverter)


                _currentRule.SaveAs = chkSaveCopyAs.Checked
                _currentRule.SaveAsPath = txtDirPath.Text
                _currentRule.SaveAsFileName = txtSaveFileName.Text

                WFRulesBusiness.UpdateParamItem(Rule.ID, 4, _currentRule.SaveAs)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 5, _currentRule.SaveAsPath)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 6, _currentRule.SaveAsFileName)
                UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkSaveCopyAs.CheckedChanged
        If chkSaveCopyAs.Checked Then
            txtSaveFileName.Enabled = True
            txtDirPath.Enabled = True
        Else
            txtSaveFileName.Enabled = False
            txtDirPath.Enabled = False
        End If
    End Sub

    Private Sub btnBrowse2_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnBrowse2.Click
        Try
            Dim Dlg As FolderBrowserDialog = New FolderBrowserDialog()
            If Dlg.ShowDialog() = DialogResult.OK Then
                txtDirPath.Text = Dlg.SelectedPath
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

End Class
