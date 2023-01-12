Public Class UCDoGenerateBarcodeInWord
    Inherits ZRuleControl

    Private _currentRule As IDoGenerateBarcodeInWord
    Private _ucTemplate As Zamba.Controls.UCTemplatesNew
    Private atributos As SortedList = New SortedList()

    ''' <summary>
    ''' Constructor: Inicia los componentes del control y setea la regla 
    ''' </summary>
    ''' <param name="CurrentRule"></param>
    ''' <history>
    '''     Javier  28/10/2010  Created
    '''</history>
    Public Sub New(ByRef CurrentRule As IDoGenerateBarcodeInWord, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(CurrentRule, _wfPanelCircuit)
        InitializeComponent()
        _currentRule = CurrentRule
        txtInputPath.Text = CurrentRule.FilePath
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
            cmbDocTypes.DisplayMember = "Doc_Type_Name"
            cmbDocTypes.ValueMember = "Doc_Type_Id"
            cmbDocTypes.DataSource = DocTypesBusiness.GetDocTypeNamesAndIds()
            If (_currentRule.docTypeId <> 0) Then
                cmbDocTypes.SelectedValue = _currentRule.docTypeId
            Else
                cmbDocTypes.SelectedIndex = 0
            End If

            Dim dt As DataTable = IndexsBusiness.GetIndexSchemaAsDataSet(cmbDocTypes.SelectedValue).Tables(0)
            Dim strIndex As String = _currentRule.Indices

            dt.Columns.Add("ValorIndice")
            While strIndex <> ""
                'Obtengo el item (// separa por items y | separa por valor)
                Dim strItem = strIndex.Split("//")(0)
                Dim id As Int32 = Int(strItem.split("|")(0))
                Dim value As String = strItem.split("|")(1)

                atributos.Add(Int64.Parse(id), value)

                strIndex = strIndex.Remove(0, strIndex.Split("//")(0).Length)
                If strIndex.Length > 0 Then
                    strIndex = strIndex.Remove(0, 2)
                End If
            End While

            If (atributos.Count = 1) Then
                Dim id As Long = atributos.GetKeyList(0)
                txtInputPath.Text = atributos(id)
            Else
                txtInputPath.Text = atributos(Int64.Parse(0))
                For Each row As DataRow In dt.Rows
                    If Not IsNothing(row("Index_Id")) Then
                        If (atributos.Contains(Int64.Parse(row("Index_Id")))) Then
                            row("ValorIndice") = atributos(Int64.Parse(row("Index_Id")))
                        End If
                    End If
                Next
            End If

            grdIndices.DataSource = dt


            Dim _ucTemplate As New Zamba.Controls.UCTemplatesNew
            _ucTemplate.Dock = DockStyle.Fill
            AddHandler _ucTemplate.linkClickedOriginalName, AddressOf SetPath
            ' _ucTemplates.lnkclicked += new UCTemplatesNew.lnkclickedEventHandler(TemplateSelected);
            Panel1.Controls.Add(_ucTemplate)
            txtTop.Text = _currentRule.Top
            txtLeft.Text = _currentRule.Left

            ChkContinueWithCurrentTasks.Checked = _currentRule.ContinueWithCurrentTasks
            ChkAutoPrint.Checked = _currentRule.AutoPrint
            ChkInsertBarcode.Checked = _currentRule.InsertBarcode

            If ChkInsertBarcode.Checked Then
                txtLeft.Enabled = True
                txtTop.Enabled = True
            Else
                txtLeft.Enabled = False
                txtTop.Enabled = False
            End If

            txtOutputPath.Text = _currentRule.DocPathVar
            CHKSavePath.Checked = _currentRule.SaveDocPathVar
            chkWithoutInsert.Checked = _currentRule.WithoutInsert

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
        txtInputPath.Text = path
    End Sub

    Private Sub cmbDocTypes_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbDocTypes.SelectedIndexChanged
        Dim dt As DataTable = IndexsBusiness.GetIndexSchemaAsDataSet(cmbDocTypes.SelectedValue).Tables(0)
        dt.Columns.Add("ValorIndice")
        grdIndices.DataSource = dt
        atributos.Clear()
        'txtIndex.Text = String.Empty
    End Sub
    ''' <summary>
    ''' Abre el dialog para seleccionar la plantilla a utilizar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>
    '''     Javier  28/10/2010  Created
    '''</history>
    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            Dim OpenFileDialog1 As New OpenFileDialog
            OpenFileDialog1.Multiselect = False
            OpenFileDialog1.Title = ("Seleccione el archivo")
            OpenFileDialog1.RestoreDirectory = True
            If OpenFileDialog1.ShowDialog() = DialogResult.OK AndAlso Not String.IsNullOrEmpty(OpenFileDialog1.FileName) Then
                txtInputPath.Text = OpenFileDialog1.FileName
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Guarda los cambios de la regla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>
    '''     Javier  28/10/2010  Created
    '''</history>
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnSave.Click
        Dim index As New System.Text.StringBuilder()
        Try
            If cmbDocTypes.Text <> "" Then
                _currentRule.docTypeId = Int64.Parse(cmbDocTypes.SelectedValue.ToString())

                index.Append(0)
                index.Append("|")
                index.Append(txtInputPath.Text)
                index.Append("//")

                For Each row As DataGridViewRow In grdIndices.Rows
                    If Not IsNothing(row.Cells("valorIndice").Value) Then
                        If Not IsDBNull(row.Cells("valorIndice").Value) Then
                            index.Append(row.Cells("Index_Id").Value)
                            index.Append("|")
                            index.Append(row.Cells("valorIndice").Value)
                            index.Append("//")
                        End If
                    End If
                Next
                _currentRule.Indices = index.ToString()
                _currentRule.FilePath = txtInputPath.Text

                _currentRule.Left = txtLeft.Text
                _currentRule.Top = txtTop.Text

                WFRulesBusiness.UpdateParamItem(Rule.ID, 0, _currentRule.docTypeId)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 1, _currentRule.Indices)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 2, _currentRule.FilePath)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 3, _currentRule.Top)
                WFRulesBusiness.UpdateParamItem(Rule.ID, 4, _currentRule.Left)
            Else
                MessageBox.Show("No se han encontrado tipos de documentos", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If


            'Se valida la correcta configuración de la regla
            If String.IsNullOrEmpty(cmbDocTypes.Text.Trim) Then
                MessageBox.Show("No se han encontrado entidades", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                cmbDocTypes.Focus()
                Exit Sub
            End If
            If CHKSavePath.Checked And String.IsNullOrEmpty(txtOutputPath.Text) Then
                MessageBox.Show("Falta completar la variable de la ruta del documento", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtOutputPath.Focus()
                Exit Sub
            End If

            _currentRule.docTypeId = Int64.Parse(cmbDocTypes.SelectedValue.ToString())


            _currentRule.FilePath = txtInputPath.Text
            _currentRule.Left = txtLeft.Text
            _currentRule.Top = txtTop.Text
            _currentRule.ContinueWithCurrentTasks = ChkContinueWithCurrentTasks.Checked
            _currentRule.AutoPrint = ChkAutoPrint.Checked
            _currentRule.InsertBarcode = ChkInsertBarcode.Checked
            _currentRule.DocPathVar = txtOutputPath.Text
            _currentRule.SaveDocPathVar = CHKSavePath.Checked
            _currentRule.WithoutInsert = chkWithoutInsert.Checked

            WFRulesBusiness.UpdateParamItem(Rule.ID, 0, _currentRule.docTypeId)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 1, _currentRule.Indices)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 2, _currentRule.FilePath)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 3, _currentRule.Top)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 4, _currentRule.Left)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 5, _currentRule.ContinueWithCurrentTasks)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 6, _currentRule.AutoPrint)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 7, _currentRule.InsertBarcode)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 8, _currentRule.SaveDocPathVar)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 9, _currentRule.DocPathVar)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 10, _currentRule.WithoutInsert)
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
    Private Sub ChkInsertBarcode_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles ChkInsertBarcode.CheckedChanged
        If ChkInsertBarcode.Checked Then
            txtLeft.Enabled = True
            txtTop.Enabled = True
        Else
            txtLeft.Enabled = False
            txtTop.Enabled = False
        End If
    End Sub

    Private Sub CHKSavePath_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles CHKSavePath.CheckedChanged
        txtOutputPath.Enabled = CHKSavePath.Checked
    End Sub

    Private Sub chkWithoutInsert_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkWithoutInsert.CheckedChanged
        If chkWithoutInsert.Checked Then CHKSavePath.Checked = True
    End Sub

    Private Sub tbRule_Click(sender As Object, e As EventArgs) Handles tbRule.Click

    End Sub
End Class
