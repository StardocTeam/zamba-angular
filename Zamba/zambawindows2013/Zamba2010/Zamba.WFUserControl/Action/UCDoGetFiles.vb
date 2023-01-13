Public Class UCDoGetFiles

    Private CurrentRule As IDoGetFiles
    ''' <summary>
    ''' Constructor: Inicia los componentes del control y setea la regla 
    ''' </summary>
    ''' <param name="CurrentRule"></param>
    ''' <history>
    '''     Pablo  28/10/2010  Created
    '''</history>
    Public Sub New(ByRef rule As IDoGetFiles, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(rule, _wfPanelCircuit)
        CurrentRule = rule
        InitializeComponent()
        UCDoGetFiles_Load()
    End Sub
    '''' <summary>
    '''' Asigna en el load los valores de la regla a los controles
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <history>
    ''''     Pablo  28/10/2010  Created
    ''''</history>
    Private Sub UCDoGetFiles_Load()
        Try
            txtDirectory.Text = CurrentRule.DirectoryRoute
            txtVarName.Text = CurrentRule.VarName
            ChkObtainAllFiles.Checked = CurrentRule.ObtainOnlyRouteFiles

            setExtensions()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ChkObtainOnlyRouteFiles_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles ChkObtainAllFiles.CheckedChanged
        If ChkObtainAllFiles.Checked Then
            CurrentRule.ObtainOnlyRouteFiles = True
            ChkObtainAllFiles.Checked = True
        Else
            CurrentRule.ObtainOnlyRouteFiles = False
            ChkObtainAllFiles.Checked = False
        End If
    End Sub

    Private Sub btnInputExm_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnInputExm.Click
        Try
            Dim Dlg As New FolderBrowserDialog
            Dlg.ShowDialog()
            txtDirectory.Text = Dlg.SelectedPath
            CurrentRule.DirectoryRoute = txtDirectory.Text





        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub BtnSaveFile_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnSaveFile.Click
        Dim extensions, FilteredExtensions As String
        Dim t As Int16
        CurrentRule.VarName = txtVarName.Text
        CurrentRule.DirectoryRoute = txtDirectory.Text.Trim

        For Each Item As String In lstExtension.SelectedItems
            extensions = extensions + Item.ToString + ","
        Next
        If Not extensions Is Nothing Then
            FilteredExtensions = extensions.Remove(extensions.Length - 1)
        End If

        WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.DirectoryRoute)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.ObtainOnlyRouteFiles)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 2, CurrentRule.VarName)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 3, FilteredExtensions)
        UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
    End Sub
    Private Sub setExtensions()
        lstExtension.SelectionMode = SelectionMode.MultiSimple
        lstExtension.Items.Add("Todos los Archivos (*.*)")
        lstExtension.Items.Add("Archivos de Texto (*.TXT)")
        lstExtension.Items.Add("Archivos de Texto (*.LOG)")
        lstExtension.Items.Add("Outlook E-Mail (*.MSG)")
        lstExtension.Items.Add("Archivos PDF (*.PDF)")
        lstExtension.Items.Add("Imagen (*.JPG)")
        lstExtension.Items.Add("Imagen (*.BMP)")
        lstExtension.Items.Add("Imagen (*.GIF)")
        lstExtension.Items.Add("Imagen (*.TIF)")
        lstExtension.Items.Add("Imagen (*.TIFF)")
        lstExtension.Items.Add("Imagen (*.PCX)")
        lstExtension.Items.Add("Office Word 2003 (*.DOC)")
        lstExtension.Items.Add("Office Exel 2003 (*.XLS)")
        lstExtension.Items.Add("Office PowerPoint 2003 (*.PPT)")
        lstExtension.Items.Add("Office Access (*.ACCDB)")
        lstExtension.Items.Add("Office Word 2007 (*.DOCX)")
        lstExtension.Items.Add("Office Exel 2007 (*.XLSX)")
        lstExtension.Items.Add("Archivo Web (*.HTM)")
        lstExtension.Items.Add("Archivo Web (*.HTML)")
        lstExtension.Items.Add("Archivo Comprimido (*.ZIP)")
        lstExtension.Items.Add("Archivo Comprimido (*.RAR)")
    End Sub
End Class

