'Imports Zamba.WFBusiness
Imports System.Text
Imports zamba.Data

Public Class UCDoAddAsociatedDocument
    Inherits ZRuleControl
#Region "Variables"

    Dim ds As New DataSet
    Dim _docTypeID As Long
#End Region

#Region "Constructor"

    ''' <summary>
    ''' Constructor de la uc de la regla
    ''' </summary>
    ''' <history>
    ''' [Sebastian] 23-10-2009 MODIFIED se corrigio la visualizacion de los nombres de los tipos de documento poruqe no se veian
    ''' [Javier]    30-12-2010 MODIFIED OpenDefaultScreen and DefaultScreenId params added
    ''' </history>
    ''' <param name="rule"></param>
    ''' <remarks></remarks>
    Public Sub New(ByRef rule As IDoAddAsociatedDocument, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(rule, _wfPanelCircuit)
        InitializeComponent()

        ds = TemplatesBusiness.GetTemplates()
        lstTemplates.DataSource = ds.Tables(0)
        lstTemplates.DisplayMember = "Name"
        lstTemplates.ValueMember = "Id"
        Try
            cboTiposDeDocumento.DataSource = DocTypesFactory.GetDocTypesDataSet.Tables(0)
            '[Sebastian 23-10-2009] Se corrigio la carga del combo porque no mostraba los
            'nombres de los tipos de documento.
            cboTiposDeDocumento.DisplayMember = "DOC_TYPE_NAME"
            cboTiposDeDocumento.ValueMember = "DOC_TYPE_ID"


            If Not IsNothing(MyRule.AsociatedDocType.ID) Then
                'For Each CurrentDocType As DocType In cboTiposDeDocumento.Items
                For Each CurrentDocType As DataRowView In cboTiposDeDocumento.Items
                    If MyRule.AsociatedDocType.ID = CurrentDocType("doc_type_id") Then
                        cboTiposDeDocumento.SelectedItem = CurrentDocType
                        Exit For
                    End If
                Next
            End If
            Select Case MyRule.SelectionId
                Case IDoAddAsociatedDocument.Selection.Ninguno
                    rdbNinguno.Checked = True
                Case IDoAddAsociatedDocument.Selection.Template
                    If MyRule.TemplateId <> 0 Then
                        rdbTemplate.Checked = True
                        'todo diego: seleccionar template
                        lstTemplates.SelectedValue = MyRule.TemplateId
                    End If
                Case IDoAddAsociatedDocument.Selection.Documento
                    rdbDocumento.Checked = True
                    Select Case MyRule.Typeid
                        Case 1
                            rdbword.Checked = True
                        Case 2
                            rdbexcel.Checked = True
                        Case 3
                            rdbpowerpoint.Checked = True
                    End Select
            End Select
            If MyRule.OpenDefaultScreen Then
                ChkOpenByDefault.Checked = True
                EnableRdbOpenByDefault()
            Else
                ChkOpenByDefault.Checked = False
                DisableRdbOpenByDefault()
            End If
            Select Case MyRule.DefaultScreenId
                Case IDoAddAsociatedDocument.DefaultScreenSelection.InsertDocument
                    rdbInsertDocuments.Checked = True
                Case IDoAddAsociatedDocument.DefaultScreenSelection.InsertFolder
                    rdbInsertFolder.Checked = True
                Case IDoAddAsociatedDocument.DefaultScreenSelection.InsertForms
                    rdbInsertForms.Checked = True
                Case IDoAddAsociatedDocument.DefaultScreenSelection.ScanDocuments
                    rdbScanDocuments.Checked = True
                Case Else
                    rdbInsertDocuments.Checked = True
            End Select

            chkDontOpenIfAsociated.Checked = rule.DontOpenTaskIfIsAsociatedToWF

            AddAutomaticVariables()

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

#End Region

#Region "Eventos"
    ''' <summary>
    ''' User control load event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>
    '''     Javier 30/12/2010   Modified    OpenDefaultScreen and DefaultScreenId params added
    ''' </history>
    Private Sub UCAddAsociatedDocuments_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles Me.Load
        ds = TemplatesBusiness.GetTemplates()
        lstTemplates.DataSource = ds.Tables(0)
        lstTemplates.DisplayMember = "Name"
        lstTemplates.ValueMember = "Id"
        Try
            cboTiposDeDocumento.DataSource = DocTypesFactory.GetDocTypesDataSet.Tables(0)
            cboTiposDeDocumento.DisplayMember = "DOC_TYPE_NAME"
            cboTiposDeDocumento.ValueMember = "DOC_TYPE_ID"
            RemoveHandler cboTiposDeDocumento.SelectedIndexChanged, AddressOf DocTypeChanged
            AddHandler cboTiposDeDocumento.SelectedIndexChanged, AddressOf DocTypeChanged
            If Not IsNothing(MyRule.AsociatedDocType.ID) Then
                'For Each CurrentDocType As DocType In cboTiposDeDocumento.Items
                For Each CurrentDocType As DataRowView In cboTiposDeDocumento.Items
                    If MyRule.AsociatedDocType.ID = CurrentDocType("DOC_TYPE_ID") Then
                        cboTiposDeDocumento.SelectedItem = CurrentDocType
                        Exit For
                    End If
                Next
            End If
            Select Case MyRule.SelectionId
                Case IDoAddAsociatedDocument.Selection.Ninguno
                    rdbNinguno.Checked = True
                Case IDoAddAsociatedDocument.Selection.Template
                    If MyRule.TemplateId <> 0 Then
                        rdbTemplate.Checked = True
                        'todo diego: seleccionar template
                        lstTemplates.SelectedValue = MyRule.TemplateId
                    End If
                Case IDoAddAsociatedDocument.Selection.Documento
                    rdbDocumento.Checked = True
                    Select Case MyRule.Typeid
                        Case 1
                            rdbword.Checked = True
                        Case 2
                            rdbexcel.Checked = True
                        Case 3
                            rdbpowerpoint.Checked = True
                    End Select
            End Select
            If MyRule.OpenDefaultScreen Then
                ChkOpenByDefault.Checked = True
                EnableRdbOpenByDefault()
            Else
                ChkOpenByDefault.Checked = False
                DisableRdbOpenByDefault()
            End If
            Select Case MyRule.DefaultScreenId
                Case IDoAddAsociatedDocument.DefaultScreenSelection.InsertDocument
                    rdbInsertDocuments.Checked = True
                Case IDoAddAsociatedDocument.DefaultScreenSelection.InsertFolder
                    rdbInsertFolder.Checked = True
                Case IDoAddAsociatedDocument.DefaultScreenSelection.InsertForms
                    rdbInsertForms.Checked = True
                Case IDoAddAsociatedDocument.DefaultScreenSelection.ScanDocuments
                    rdbScanDocuments.Checked = True
                Case Else
                    rdbInsertDocuments.Checked = True
            End Select

            chkSpecificConfig.Checked = MyRule.HaveSpecificAttributes

            'Si hay configuracion de atributos especificos, se carga el datasource
            If MyRule.HaveSpecificAttributes Then
                gvAttribute.DataSource = GetAttributesSource(_docTypeID)
                gvAttribute.Columns("DONOTCOMPLETE").HeaderText = "No Autocompletar"
            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Saves rules params
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>
    '''     Javier  30/12/2010  Modified    OpenDefaultScreen and DefaultScreenId params added
    ''' </history>
    Private Sub btGuardar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnSave.Click
        Dim selectedFormat As String = String.Empty

        If Not IsNothing(cboTiposDeDocumento.SelectedItem) Then
            MyRule.SelectionId = IDoAddAsociatedDocument.Selection.Ninguno
            WFRulesBusiness.UpdateParamItem(MyRule, 3, MyRule.SelectionId)
            MyRule.TemplateId = 0
            MyRule.Typeid = 0
            'MyRule.AsociatedDocType.ID = DirectCast(cboTiposDeDocumento.SelectedValue, DocType).ID
            MyRule.AsociatedDocType.ID = Int64.Parse(cboTiposDeDocumento.SelectedValue)
            WFRulesBusiness.UpdateParamItem(MyRule, 0, MyRule.AsociatedDocType.ID)
            If rdbTemplate.Checked Then
                MyRule.SelectionId = IDoAddAsociatedDocument.Selection.Template
                WFRulesBusiness.UpdateParamItem(MyRule, 3, MyRule.SelectionId)
                MyRule.Typeid = 0
                MyRule.TemplateId = CType(lstTemplates.SelectedValue, Integer)
                WFRulesBusiness.UpdateParamItem(MyRule, 1, MyRule.TemplateId)
            End If
            If rdbDocumento.Checked Then
                MyRule.SelectionId = IDoAddAsociatedDocument.Selection.Documento
                WFRulesBusiness.UpdateParamItem(MyRule, 3, MyRule.SelectionId)
                If rdbword.Checked Then
                    MyRule.Typeid = 1
                    WFRulesBusiness.UpdateParamItem(MyRule, 2, MyRule.Typeid)
                ElseIf rdbexcel.Checked Then
                    MyRule.Typeid = 2
                    WFRulesBusiness.UpdateParamItem(MyRule, 2, MyRule.Typeid)
                Else
                    MyRule.Typeid = 3
                    WFRulesBusiness.UpdateParamItem(MyRule, 2, MyRule.Typeid)
                End If
            End If
            If rdbNinguno.Checked Then
                MyRule.OpenDefaultScreen = ChkOpenByDefault.Checked
                WFRulesBusiness.UpdateParamItem(MyRule, 4, MyRule.OpenDefaultScreen)

                If rdbInsertDocuments.Checked Then
                    MyRule.DefaultScreenId = IDoAddAsociatedDocument.DefaultScreenSelection.InsertDocument
                    WFRulesBusiness.UpdateParamItem(MyRule, 5, MyRule.DefaultScreenId)
                End If
                If rdbInsertFolder.Checked Then
                    MyRule.DefaultScreenId = IDoAddAsociatedDocument.DefaultScreenSelection.InsertFolder
                    WFRulesBusiness.UpdateParamItem(MyRule, 5, MyRule.DefaultScreenId)
                End If
                If rdbInsertForms.Checked Then
                    MyRule.DefaultScreenId = IDoAddAsociatedDocument.DefaultScreenSelection.InsertForms
                    WFRulesBusiness.UpdateParamItem(MyRule, 5, MyRule.DefaultScreenId)
                End If
                If rdbScanDocuments.Checked Then
                    MyRule.DefaultScreenId = IDoAddAsociatedDocument.DefaultScreenSelection.ScanDocuments
                    WFRulesBusiness.UpdateParamItem(MyRule, 5, MyRule.DefaultScreenId)
                End If
            End If

            MyRule.DontOpenTaskIfIsAsociatedToWF = chkDontOpenIfAsociated.Checked
            WFRulesBusiness.UpdateParamItem(MyRule, 6, MyRule.DontOpenTaskIfIsAsociatedToWF)

            MyRule.HaveSpecificAttributes = chkSpecificConfig.Checked
            MyRule.SpecificAttrubutes = FormatSpecificConfiguration()
            WFRulesBusiness.UpdateParamItem(Rule.ID, 7, MyRule.HaveSpecificAttributes)
            WFRulesBusiness.UpdateParamItem(Rule.ID, 8, MyRule.SpecificAttrubutes)

            lblOk.Text = "Las modificaciones se han guardado correctamente"
            UserBusiness.Rights.SaveAction(MyRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & MyRule.Name & "(" & MyRule.ID & ")")

            AddAutomaticVariables()

        End If
    End Sub

    Private Sub RdbNinguno_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rdbNinguno.CheckedChanged
        If rdbNinguno.Checked = True Then
            clearpanel()
            pnlOptions.Enabled = True
            NoneSelected()
        End If

    End Sub

    Private Sub RdbTemplate_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rdbTemplate.CheckedChanged
        If rdbTemplate.Checked = True Then
            clearpanel()
            pnlOptions.Enabled = True
            LoadTemplates()
            HasBeenModified = True
        End If
    End Sub

    Private Sub RdbDocumento_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rdbDocumento.CheckedChanged
        If rdbDocumento.Checked = True Then
            clearpanel()
            pnlOptions.Enabled = True
            AddFormatDocument()
            HasBeenModified = True
        End If
    End Sub

    Private Sub ChkOpenByDefault_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles ChkOpenByDefault.CheckedChanged
        If ChkOpenByDefault.Checked = True Then
            EnableRdbOpenByDefault()
        Else
            DisableRdbOpenByDefault()
        End If
    End Sub

    Private Sub chkSpecificConfig_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkSpecificConfig.CheckedChanged
        If chkSpecificConfig.Checked Then
            gvAttribute.Enabled = True
            gvAttribute.Visible = True
            gvAttribute.DataSource = GetAttributesSource(_docTypeID)
            gvAttribute.Columns("DONOTCOMPLETE").HeaderText = "No Autocompletar"
        Else
            gvAttribute.Enabled = False
            gvAttribute.Visible = False
        End If
        HasBeenModified = True
    End Sub

    Private Sub DocTypeChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles cboTiposDeDocumento.SelectedIndexChanged
        If cboTiposDeDocumento.SelectedIndex > -1 Then
            Dim dtID As Int64
            If Int64.TryParse(cboTiposDeDocumento.SelectedValue.ToString(), dtID) Then
                'Cuando cambia el tipo de documetno, actualizo la grilla
                _docTypeID = dtID

                If chkSpecificConfig.Checked Then
                    gvAttribute.DataSource = GetAttributesSource(_docTypeID)
                    gvAttribute.Columns("DONOTCOMPLETE").HeaderText = "No Autocompletar"
                    gvAttribute.Enabled = True
                    gvAttribute.Visible = True
                Else
                    gvAttribute.Enabled = False
                    gvAttribute.Visible = False
                End If
            End If
        End If
    End Sub
#End Region

#Region "Metodos"
    Private Sub clearpanel()
        Dim panelcontrols As New Generic.List(Of Control)
        If pnlOptions.Controls.Count > 0 Then
            For Each c As Control In pnlOptions.Controls
                panelcontrols.Add(c)
            Next
            For Each c As Control In panelcontrols
                pnlOptions.Controls.Remove(c)
            Next
        End If
        panelcontrols = Nothing
    End Sub

    Private Sub LoadTemplates()
        lstTemplates.Dock = DockStyle.Fill
        pnlOptions.Controls.Add(lstTemplates)
    End Sub

    Private Sub AddFormatDocument()
        rdbword.Text = "Documento de Word"
        rdbword.Checked = True
        rdbexcel.Text = "Documento de Excel"
        rdbpowerpoint.Text = "Documento de Power Point"
        Dim o As control() = {rdbword, rdbexcel, rdbpowerpoint}
        grpboxDocument.Controls.AddRange(o)
        grpboxDocument.Dock = DockStyle.Fill
        pnlOptions.Controls.Add(grpboxDocument)
        rdbword.Dock = DockStyle.Top
        rdbexcel.Dock = DockStyle.Top
        rdbpowerpoint.Dock = DockStyle.Top
    End Sub

    Private Sub NoneSelected()
        pnlOptions.Controls.Add(grpboxNone)
    End Sub

    Public Sub EnableRdbOpenByDefault()
        rdbInsertDocuments.Enabled = True
        rdbInsertFolder.Enabled = True
        rdbInsertForms.Enabled = True
        rdbScanDocuments.Enabled = True
    End Sub

    Public Sub DisableRdbOpenByDefault()
        rdbInsertDocuments.Enabled = False
        rdbInsertFolder.Enabled = False
        rdbInsertForms.Enabled = False
        rdbScanDocuments.Enabled = False
    End Sub

    ''' <summary>
    ''' Carga en un datatable en base a la los atributos de la entidad y a la configuracion de la regla
    ''' </summary>
    ''' <param name="docTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetAttributesSource(ByVal docTypeId As Long) As DataTable
        Dim dt As DataTable = IndexsBusiness.GetIndexSchemaAsDataSet(docTypeId).Tables(0)
        If Not dt.Columns.Contains("ValorIndice") Then
            dt.Columns.Add("ValorIndice")
        End If

        If dt.Columns.Contains("Index_Type") Then
            dt.Columns.Remove(dt.Columns("Index_Type"))
        End If

        If dt.Columns.Contains("Index_Len") Then
            dt.Columns.Remove(dt.Columns("Index_Len"))
        End If

        If dt.Columns.Contains("DropDown") Then
            dt.Columns.Remove(dt.Columns("DropDown"))
        End If

        If dt.Columns.Contains("IsReferenced") Then
            dt.Columns.Remove(dt.Columns("IsReferenced"))
        End If

        If dt.Columns.Contains("Orden") Then
            dt.Columns.Remove(dt.Columns("Orden"))
        End If

        If dt.Columns.Contains("IndicePadre") Then
            dt.Columns.Remove(dt.Columns("IndicePadre"))
        End If

        If dt.Columns.Contains("IndiceHijo") Then
            dt.Columns.Remove(dt.Columns("IndiceHijo"))
        End If

        If dt.Columns.Contains("DataTableName") Then
            dt.Columns.Remove(dt.Columns("DataTableName"))
        End If

        Dim col As New DataColumn("DONOTCOMPLETE")
        col.DataType = GetType(Boolean)
        col.DefaultValue = False
        col.Caption = "No Autocompletar"

        If Not dt.Columns.Contains("DONOTCOMPLETE") Then
            dt.Columns.Add(col)
        End If

        dt.Columns("DONOTCOMPLETE").Caption = "No Autocompletar"

        If Not String.IsNullOrEmpty(MyRule.SpecificAttrubutes) AndAlso MyRule.AsociatedDocType.ID = cboTiposDeDocumento.SelectedValue Then

            dt = CompleteAttributesValues(dt)
        End If

        Return dt
    End Function

    ''' <summary>
    ''' Completa los valores del datatable con los atributos
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CompleteAttributesValues(ByVal dt As DataTable) As DataTable
        Dim strIndex As String = MyRule.SpecificAttrubutes.Replace("//", "§")
        Dim value As String
        Dim id As Int64
        Dim strItem As String
        Dim indices As New Hashtable

        'En este while carga un hash con los indices y su configuracion(valor|completar/nocompletar)
        While Not String.IsNullOrEmpty(strIndex)
            'Obtengo el item (// separa por items y | separa por valor y no completar)

            strItem = strIndex.Split("§")(0)
            id = Int(strItem.Split("|")(0))
            value = strItem.Substring(strItem.IndexOf("|", StringComparison.Ordinal) + 1)

            indices.Add(Int64.Parse(id), value)

            strIndex = strIndex.Remove(0, strIndex.Split("§")(0).Length)
            If strIndex.Length > 0 Then
                strIndex = strIndex.Remove(0, 1)
            End If
        End While

        Dim rowCount As Integer = dt.Rows.Count
        Dim currRow As DataRow
        For i As Integer = 0 To rowCount - 1
            currRow = dt.Rows(i)
            If Not IsNothing(currRow("Index_Id")) Then
                If (indices.Contains(Int64.Parse(currRow("Index_Id")))) Then

                    'Obtiene el valor y el check para no autocompletar (si es que existe)
                    value = indices(Int64.Parse(currRow("Index_Id")))

                    'Obtiene la posición del último "|" que separa la configuracion del check
                    id = value.LastIndexOf("|", StringComparison.Ordinal)

                    'Verifica que exista
                    If id > -1 Then
                        'Obtiene el autocompletado
                        strItem = value.Substring(id + 1)

                        'Verifica si debe dejar el valor que tiene y no autocompletarlo
                        If String.Compare(strItem, "[no_completar]") = 0 Then
                            currRow("ValorIndice") = value.Substring(0, id)
                            currRow("DONOTCOMPLETE") = True
                        Else
                            currRow("ValorIndice") = value
                            currRow("DONOTCOMPLETE") = False
                        End If
                    Else
                        'En caso de no encontrar un valor para no autocompletar
                        currRow("ValorIndice") = value
                        currRow("DONOTCOMPLETE") = False
                    End If

                End If
            End If
        Next

        Return dt
    End Function

    ''' <summary>
    ''' Formatea la configuracion hecha en la grilla a un string
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FormatSpecificConfiguration() As String
        Dim config As New StringBuilder()

        Dim valor As Boolean
        Dim completar As Boolean

        For Each row As DataGridViewRow In gvAttribute.Rows
            'Se guardan las condiciones
            valor = (Not IsNothing(row.Cells("valorIndice").Value) AndAlso Not IsDBNull(row.Cells("valorIndice").Value))
            completar = row.Cells("DONOTCOMPLETE").Value

            'Verifica que se deba completar algún dato del Atributo
            If (chkSpecificConfig.Checked) Then

                'Verifica si debe completar información del valor
                If valor Then
                    config.Append(row.Cells("Index_Id").Value)
                    config.Append("|")
                    config.Append(row.Cells("valorIndice").Value)
                Else
                    config.Append(row.Cells("Index_Id").Value)
                    config.Append("|")
                End If

                'Verifica si debe completar informacion de 
                If completar Then
                    config.Append("|")
                    config.Append("[no_completar]")
                End If

                config.Append("//")
            End If
        Next

        Return config.ToString()
    End Function
#End Region

#Region "Propiedades"
    Public Shadows ReadOnly Property MyRule() As IDoAddAsociatedDocument
        Get
            Return DirectCast(Rule, IDoAddAsociatedDocument)
        End Get
    End Property
#End Region

    Private Sub AddAutomaticVariables()
        Try
            lstautomaticvariables.Items.Clear()
            lstautomaticvariables.Items.Add("zvar(NuevoDocumento.Id)")
            lstautomaticvariables.Items.Add("zvar(NuevoDocumento.EntityId)")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub pnlOptions_Paint(sender As Object, e As PaintEventArgs) Handles pnlOptions.Paint

    End Sub

    Private Sub chkDontOpenIfAsociated_CheckedChanged(sender As Object, e As EventArgs) Handles chkDontOpenIfAsociated.CheckedChanged
        HasBeenModified = True
    End Sub
End Class
