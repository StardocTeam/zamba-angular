Public Class UCDoSaveFile
    Inherits ZRuleControl

    Private _currentRuledosave As IDOSaveFile
    Private _ucTemplatedosave As Zamba.Controls.UCTemplatesNew
    'Private indicesdosave As SortedList = New SortedList()
    Dim rutaTemp As String

    ''' <summary>
    ''' Constructor: Inicia los componentes del control y setea la regla 
    ''' </summary>
    ''' <param name="CurrentRule"></param>
    ''' <history>
    '''     Javier  28/10/2010  Created
    '''</history>
    Public Sub New(ByRef CurrentRule As IDOSaveFile, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(CurrentRule, _wfPanelCircuit)
        InitializeComponent()
        _currentRuledosave = CurrentRule
        txtPathVariable.Text = _currentRuledosave.VarFilePath
    End Sub

    ''' <summary>
    ''' Asigna en el load los valores de la regla a los controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>
    '''     Javier  28/10/2010  Created
    '''</history>
    Private Sub UCDOGenerateTaskResult_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            'bolini = True

            Dim _ucTemplate As New Zamba.Controls.UCTemplatesNew
            _ucTemplate.Dock = DockStyle.Fill
            AddHandler _ucTemplate.linkClickedOriginalName, AddressOf SetPath
            ' _ucTemplates.lnkclicked += new UCTemplatesNew.lnkclickedEventHandler(TemplateSelected);
            'Panel.Controls.Add(_ucTemplate)

            txtFilePath.Text = _currentRuledosave.FilePath
            txtTextToSave.Text = _currentRuledosave.TextToSave
            txtFileExtension.Text = _currentRuledosave.FileExtension
            txtPathVariable.Text = _currentRuledosave.VarFilePath
            TxtFileName.Text = _currentRuledosave.FileName
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Coloca el path
    ''' </summary>
    ''' <param name="path"></param>
    ''' <history>
    '''     Javier  28/10/2010  Created
    '''</history>
    Private Sub SetPath(ByVal path As String)
        txtPathVariable.Text = path
    End Sub


    ''' <summary>
    ''' Guarda los cambios de la regla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>
    '''     Javier  28/10/2010  Created
    '''</history>
    Private Sub BtnSaveFile_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnSaveFile.Click
        Dim index As New System.Text.StringBuilder()
        Try
            'Try

            '    rutaTemp = DateTime.Now.ToString("dd-MM-yy HH-mm-ss")
            '    Dim file As New IO.FileInfo(rutaTemp)


            '    Dim saveFileDialog As New SaveFileDialog

            '    With saveFileDialog
            '        .DefaultExt = "txt"
            '        .FileName = rutaTemp
            '        .Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            '        .FilterIndex = 1
            '        .OverwritePrompt = True
            '        .Title = "Guardar Archivo"
            '    End With

            '    If saveFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK AndAlso Not String.IsNullOrEmpty(saveFileDialog.FileName) Then
            '        Dim filePath As String
            '        filePath = System.IO.Path.Combine( _
            '            Me.txtFilePath.Text, _
            '            saveFileDialog.FileName)
            '        Me.txtFilePath.Text = saveFileDialog.FileName
            '        My.Computer.FileSystem.WriteAllText(filePath, Me.txtTextToSave.Text, False)

            '    End If
            'Catch ex As Exception
            '    Zamba.Core.ZClass.raiseerror(ex)
            'End Try

            _currentRuledosave.FilePath = txtFilePath.Text
            _currentRuledosave.FileExtension = txtFileExtension.Text
            _currentRuledosave.TextToSave = txtTextToSave.Text
            _currentRuledosave.VarFilePath = txtPathVariable.Text
            _currentRuledosave.FileName = TxtFileName.Text

            WFRulesBusiness.UpdateParamItem(Rule.ID, 0, _currentRuledosave.FilePath)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 1, _currentRuledosave.FileExtension)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 2, _currentRuledosave.TextToSave)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 3, _currentRuledosave.VarFilePath)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 4, _currentRuledosave.FileName)
            UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")

        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            index = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Habilita o deshabilita la configuración de la posición del código de barras
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>
    '''     Javier  28/10/2010  Created
    '''</history>
    'Private Sub ChkInsertBarcode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    'If Me.ChkInsertBarcode.Checked Then
    '    '    Me.txtLeft.Enabled = True
    '    '    Me.txtTop.Enabled = True
    '    'Else
    '    '    Me.txtLeft.Enabled = False
    '    '    Me.txtTop.Enabled = False
    '    'End If
    'End Sub


End Class
