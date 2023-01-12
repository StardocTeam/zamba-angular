Imports Zamba.Data

Public Class UCDoExport
    Inherits ZRuleControl

#Region " Código generado por el Diseñador de Windows Forms "


    'Form reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend Shadows WithEvents btnSeleccionar As ZButton
    Friend Shadows WithEvents Label1 As ZLabel
    Friend WithEvents btnAddCustomColumn As ZButton
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents txtSeparator As TextBox
    Friend WithEvents lblSeparator As ZLabel
    Friend WithEvents lblExportTo As ZLabel
    Friend WithEvents txtFullPath As TextBox
    Friend WithEvents btnExaminar As ZButton
    Friend WithEvents txtDocumentName As TextBox
    Friend WithEvents lblDocumentName As ZLabel
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents txtCustomItem As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lstIndexs As System.Windows.Forms.Panel
    Friend WithEvents txtResultLine As TextBox
    Friend WithEvents chkVersionsDocuments As System.Windows.Forms.CheckBox
    Friend WithEvents CboSeleccion As ComboBox

    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        btnSeleccionar = New ZButton()
        CboSeleccion = New ComboBox()
        Label1 = New ZLabel()
        btnAddCustomColumn = New ZButton()
        Label2 = New ZLabel()
        txtSeparator = New TextBox()
        lblSeparator = New ZLabel()
        lblExportTo = New ZLabel()
        txtFullPath = New TextBox()
        btnExaminar = New ZButton()
        txtDocumentName = New TextBox()
        lblDocumentName = New ZLabel()
        FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        txtCustomItem = New Zamba.AppBlock.TextoInteligenteTextBox()
        lstIndexs = New System.Windows.Forms.Panel()
        txtResultLine = New TextBox()
        chkVersionsDocuments = New System.Windows.Forms.CheckBox()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(chkVersionsDocuments)
        tbRule.Controls.Add(txtResultLine)
        tbRule.Controls.Add(lstIndexs)
        tbRule.Controls.Add(txtCustomItem)
        tbRule.Controls.Add(lblDocumentName)
        tbRule.Controls.Add(txtDocumentName)
        tbRule.Controls.Add(btnExaminar)
        tbRule.Controls.Add(txtFullPath)
        tbRule.Controls.Add(lblExportTo)
        tbRule.Controls.Add(lblSeparator)
        tbRule.Controls.Add(txtSeparator)
        tbRule.Controls.Add(Label2)
        tbRule.Controls.Add(btnAddCustomColumn)
        tbRule.Controls.Add(Label1)
        tbRule.Controls.Add(CboSeleccion)
        tbRule.Controls.Add(btnSeleccionar)
        tbRule.Size = New System.Drawing.Size(592, 436)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(600, 465)
        '
        'btnSeleccionar
        '
        btnSeleccionar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnSeleccionar.FlatStyle = FlatStyle.Flat
        btnSeleccionar.ForeColor = System.Drawing.Color.White
        btnSeleccionar.Location = New System.Drawing.Point(407, 389)
        btnSeleccionar.Name = "btnSeleccionar"
        btnSeleccionar.Size = New System.Drawing.Size(91, 27)
        btnSeleccionar.TabIndex = 13
        btnSeleccionar.Text = "Guardar"
        btnSeleccionar.UseVisualStyleBackColor = False
        '
        'CboSeleccion
        '
        CboSeleccion.DropDownStyle = ComboBoxStyle.DropDownList
        CboSeleccion.Items.AddRange(New Object() {"Aumentar", "Disminuir"})
        CboSeleccion.Location = New System.Drawing.Point(8, 30)
        CboSeleccion.Name = "CboSeleccion"
        CboSeleccion.Size = New System.Drawing.Size(211, 24)
        CboSeleccion.TabIndex = 18
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label1.Location = New System.Drawing.Point(5, 13)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(57, 16)
        Label1.TabIndex = 19
        Label1.Text = "Entidad"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        '
        'btnAddCustomColumn
        '
        btnAddCustomColumn.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnAddCustomColumn.FlatStyle = FlatStyle.Flat
        btnAddCustomColumn.ForeColor = System.Drawing.Color.White
        btnAddCustomColumn.Location = New System.Drawing.Point(129, 282)
        btnAddCustomColumn.Name = "btnAddCustomColumn"
        btnAddCustomColumn.Size = New System.Drawing.Size(36, 23)
        btnAddCustomColumn.TabIndex = 21
        btnAddCustomColumn.Text = ">>"
        btnAddCustomColumn.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label2.Location = New System.Drawing.Point(10, 234)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(148, 16)
        Label2.TabIndex = 22
        Label2.Text = "Agregar item definido"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtSeparator
        '
        txtSeparator.Location = New System.Drawing.Point(265, 30)
        txtSeparator.MaxLength = 1
        txtSeparator.Name = "txtSeparator"
        txtSeparator.Size = New System.Drawing.Size(45, 23)
        txtSeparator.TabIndex = 25
        '
        'lblSeparator
        '
        lblSeparator.AutoSize = True
        lblSeparator.BackColor = System.Drawing.Color.Transparent
        lblSeparator.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblSeparator.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblSeparator.Location = New System.Drawing.Point(261, 11)
        lblSeparator.Name = "lblSeparator"
        lblSeparator.Size = New System.Drawing.Size(75, 16)
        lblSeparator.TabIndex = 26
        lblSeparator.Text = "Separador"
        lblSeparator.TextAlign = ContentAlignment.MiddleLeft
        '
        'lblExportTo
        '
        lblExportTo.AutoSize = True
        lblExportTo.BackColor = System.Drawing.Color.Transparent
        lblExportTo.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblExportTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblExportTo.Location = New System.Drawing.Point(182, 234)
        lblExportTo.Name = "lblExportTo"
        lblExportTo.Size = New System.Drawing.Size(83, 16)
        lblExportTo.TabIndex = 27
        lblExportTo.Text = "Exportar A:"
        lblExportTo.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtFullPath
        '
        txtFullPath.Location = New System.Drawing.Point(185, 255)
        txtFullPath.Name = "txtFullPath"
        txtFullPath.ReadOnly = True
        txtFullPath.Size = New System.Drawing.Size(216, 23)
        txtFullPath.TabIndex = 28
        '
        'btnExaminar
        '
        btnExaminar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnExaminar.FlatStyle = FlatStyle.Flat
        btnExaminar.ForeColor = System.Drawing.Color.White
        btnExaminar.Location = New System.Drawing.Point(407, 253)
        btnExaminar.Name = "btnExaminar"
        btnExaminar.Size = New System.Drawing.Size(91, 25)
        btnExaminar.TabIndex = 29
        btnExaminar.Text = "Examinar..."
        btnExaminar.UseVisualStyleBackColor = True
        '
        'txtDocumentName
        '
        txtDocumentName.Location = New System.Drawing.Point(253, 309)
        txtDocumentName.MaxLength = 60
        txtDocumentName.Name = "txtDocumentName"
        txtDocumentName.Size = New System.Drawing.Size(229, 23)
        txtDocumentName.TabIndex = 30
        '
        'lblDocumentName
        '
        lblDocumentName.AutoSize = True
        lblDocumentName.BackColor = System.Drawing.Color.Transparent
        lblDocumentName.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lblDocumentName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lblDocumentName.Location = New System.Drawing.Point(10, 312)
        lblDocumentName.Name = "lblDocumentName"
        lblDocumentName.Size = New System.Drawing.Size(237, 16)
        lblDocumentName.TabIndex = 31
        lblDocumentName.Text = "Nombre del Documento Exportado:"
        lblDocumentName.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtCustomItem
        '
        txtCustomItem.Location = New System.Drawing.Point(13, 255)
        txtCustomItem.Name = "txtCustomItem"
        txtCustomItem.Size = New System.Drawing.Size(152, 21)
        txtCustomItem.TabIndex = 32
        txtCustomItem.Text = ""
        '
        'lstIndexs
        '
        lstIndexs.AutoScroll = True
        lstIndexs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        lstIndexs.Location = New System.Drawing.Point(8, 57)
        lstIndexs.Name = "lstIndexs"
        lstIndexs.Size = New System.Drawing.Size(250, 170)
        lstIndexs.TabIndex = 34
        '
        'txtResultLine
        '
        txtResultLine.Location = New System.Drawing.Point(264, 57)
        txtResultLine.Multiline = True
        txtResultLine.Name = "txtResultLine"
        txtResultLine.Size = New System.Drawing.Size(234, 170)
        txtResultLine.TabIndex = 35
        '
        'chkVersionsDocuments
        '
        chkVersionsDocuments.AutoSize = True
        chkVersionsDocuments.BackColor = System.Drawing.Color.Transparent
        chkVersionsDocuments.FlatStyle = FlatStyle.Flat
        chkVersionsDocuments.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        chkVersionsDocuments.Location = New System.Drawing.Point(13, 347)
        chkVersionsDocuments.Name = "chkVersionsDocuments"
        chkVersionsDocuments.Size = New System.Drawing.Size(275, 20)
        chkVersionsDocuments.TabIndex = 36
        chkVersionsDocuments.Text = "Versionar Los Documentos Exportados"
        chkVersionsDocuments.UseVisualStyleBackColor = False
        '
        'UCDoExport
        '
        Name = "UCDoExport"
        Size = New System.Drawing.Size(600, 465)
        tbRule.ResumeLayout(False)
        tbRule.PerformLayout()
        tbctrMain.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

#Region "Fields"
    Private paramrule As IDoExport
    Private Const MARGINTOP As Int32 = 5
    Private Const CONTROLS_SEPARATION As Int32 = 20
    Private controlsId As Integer
    Private LoadingParameters As Boolean
    Dim sortedLst As SortedList
#End Region

    Public Sub New(ByRef _rule As IDoExport, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(_rule, _wfPanelCircuit)
        InitializeComponent()
        paramrule = _rule
        txtSeparator.Text = "|"
        LoadAllDocTypes()
        LoadParameters()

    End Sub

#Region "Metodos"
    ''' <summary>
    ''' Carga El combo con los doctypes
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Diego]	18/02/2008	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub LoadAllDocTypes()
        Try
            Dim DsDocTypes As DataSet
            DsDocTypes = DocTypesFactory.GetDocTypesDsDocType()

            RemoveHandler CboSeleccion.SelectedIndexChanged, AddressOf CboSeleccion_SelectedIndexChanged
            CboSeleccion.DataSource = DsDocTypes.Tables("DOC_TYPE")
            CboSeleccion.DisplayMember = "DOC_TYPE_NAME"
            CboSeleccion.ValueMember = "DOC_TYPE_ID"
            AddHandler CboSeleccion.SelectedIndexChanged, AddressOf CboSeleccion_SelectedIndexChanged

            LoadIndexs()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Carga Los Atributos
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Diego]	18/02/2008	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub LoadIndexs()
        Try
            controlsId = 1
            lstIndexs.Controls.Clear()
            Dim dsIndexs As DataSet = IndexsBusiness.GetIndexSchemaAsDataSet(CInt(CboSeleccion.SelectedValue))
            Dim Indexs As New Generic.List(Of Int64)

            For index As Integer = 1 To dsIndexs.Tables(0).Rows.Count
                Indexs.Add(index)
            Next

            Dim IndexsNames As New List(Of String)

            If dsIndexs.Tables(0).Rows.Count > 0 Then
                For Each r As DataRow In dsIndexs.Tables(0).Rows

                    Dim Chk As New CheckBox
                    Chk.Text = r.Item("Index_Name").ToString.Trim

                    IndexsNames.Add(r.Item("Index_Name").ToString.Trim)

                    Chk.FlatStyle = FlatStyle.Flat
                    Chk.Height = 18
                    Chk.Checked = True
                    Chk.Width = 160
                    Chk.Left = 8
                    Chk.Top = GetTopPosition()

                    Dim cmb As New ComboBox

                    For Each _Item As Int32 In GetItemsArray()
                        cmb.Items.Add(_Item)
                    Next
                    cmb.DropDownStyle = ComboBoxStyle.DropDownList
                    cmb.SelectedItem = controlsId
                    cmb.Text = controlsId
                    'Le agrege el seleccionado al TAG para tener un Estado previo al ordenar
                    cmb.Tag = controlsId
                    cmb.Left = 185
                    cmb.Width = 40
                    cmb.Top = GetTopPosition()


                    AddHandler cmb.SelectedIndexChanged, AddressOf CmbOrder_SelectedIndexChanged
                    AddHandler Chk.CheckedChanged, AddressOf chkSelectedIndex_CheckedChanged

                    lstIndexs.Controls.Add(Chk)
                    lstIndexs.Controls.Add(cmb)


                    controlsId += 1
                Next

            End If

            SetLine(IndexsNames)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Function GetItemsArray(Optional ByVal AddRemoveItems As Boolean = False) As Generic.List(Of Int32)
        Dim list As New Generic.List(Of Int32)

        If AddRemoveItems = False Then

            Dim dsIndexs As DataSet = IndexsBusiness.GetIndexSchemaAsDataSet(CInt(CboSeleccion.SelectedValue))
            Dim count As Int32 = dsIndexs.Tables(0).Rows.Count

            For Each Item As String In paramrule.SortString.Split("|")
                If String.IsNullOrEmpty(Item) = False Then
                    If Trim(Item).StartsWith("<<") Then
                        count += 1
                    End If
                End If
            Next
            If count > 0 Then
                For index As Integer = 0 To count - 1
                    list.Add(index + 1)
                Next
            End If


        Else
            For Each c As Control In lstIndexs.Controls
                If TypeOf c Is ComboBox Then
                    For index As Integer = 0 To DirectCast(c, ComboBox).Items.Count - 1
                        list.Add(index + 1)
                    Next
                    Exit For
                End If
            Next
        End If

        Return list
    End Function

    Private Function GetTopPosition() As Int32
        Dim count As Int32 = lstIndexs.Controls.Count

        'If count = 0 Then
        '    Return MARGINTOP
        'ElseIf count Mod 2 = 0 Then
        '    Return MARGINTOP + ((count / 2) * CONTROLS_SEPARATION)
        'Else
        '    Return MARGINTOP + (((count + 1) / 2) * CONTROLS_SEPARATION)
        'End If
        Return ((count * CONTROLS_SEPARATION) + MARGINTOP) / 2
    End Function

    ''' <summary>
    ''' Crea La linea Resultante para el archivo a Exportar
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Diego]	18/02/2008	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub SetLine(Optional ByVal IndexsNames As List(Of String) = Nothing)
        Try
            sortedLst = New SortedList

            txtResultLine.Text = String.Empty
            Dim Line As New System.Text.StringBuilder

            If Not IsNothing(IndexsNames) Then

                Dim _top As Int32
                For Each chk As Control In lstIndexs.Controls
                    If TypeOf chk Is CheckBox Then
                        _top = chk.Top

                        'If DirectCast(chk, CheckBox).Checked Then Line.Append(DirectCast(chk, CheckBox).Text & " " & txtSeparator.Text & " ")

                        For Each cmb As Control In lstIndexs.Controls
                            If TypeOf cmb Is ComboBox AndAlso cmb.Top = _top Then

                                If sortedLst.ContainsKey(Int16.Parse(cmb.Text)) = False Then
                                    sortedLst.Add(Int16.Parse(cmb.Text), DirectCast(chk, CheckBox).Text)
                                End If
                                Exit For
                            End If
                        Next
                    End If
                Next
                For Each Item As String In IndexsNames
                    Line.Append(Item & " " & txtSeparator.Text & " ")
                Next
            Else
                Dim _top As Int32
                For Each chk As Control In lstIndexs.Controls
                    If TypeOf chk Is CheckBox Then
                        If DirectCast(chk, CheckBox).Checked Then
                            _top = chk.Top

                            For Each cmb As Control In lstIndexs.Controls
                                If TypeOf cmb Is ComboBox AndAlso cmb.Top = _top Then
                                    If Not String.IsNullOrEmpty(cmb.Text) Then
                                        If sortedLst.ContainsKey(Int16.Parse(cmb.Text)) = False Then
                                            sortedLst.Add(Int16.Parse(cmb.Text), DirectCast(chk, CheckBox).Text)
                                        End If
                                        Exit For
                                    End If
                                End If
                            Next


                        End If
                    End If
                Next

                For Each Item As String In sortedLst.Values
                    Line.Append(Item & " " & txtSeparator.Text & " ")
                Next
            End If

            txtResultLine.Text = Line.ToString
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub DisableControls()
        For Each c As Control In lstIndexs.Controls
            'deshabilito y le saco el check a los controles
            If TypeOf c Is CheckBox Then
                Dim chk As CheckBox = DirectCast(c, CheckBox)
                RemoveHandler chk.CheckedChanged, AddressOf chkSelectedIndex_CheckedChanged
                chk.Checked = False
                AddHandler chk.CheckedChanged, AddressOf chkSelectedIndex_CheckedChanged
            Else
                c.Enabled = False
                DirectCast(c, ComboBox).Text = String.Empty
                DirectCast(c, ComboBox).Tag = String.Empty
                DirectCast(c, ComboBox).SelectedItem = String.Empty
            End If
        Next
    End Sub

    ''' <summary>
    ''' Setea los Parametro en el control
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Diego]	18/02/2008	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub LoadParameters()
        Try
            LoadingParameters = True
            If paramrule.DoctypeId > 0 Then
                CboSeleccion.SelectedValue = paramrule.DoctypeId
            End If
            If String.IsNullOrEmpty(paramrule.separator) = False Then txtSeparator.Text = paramrule.separator
            If String.IsNullOrEmpty(paramrule.documentName) = False Then txtDocumentName.Text = paramrule.documentName
            If String.IsNullOrEmpty(paramrule.documentPath) = False Then txtFullPath.Text = paramrule.documentPath
            RemoveHandler chkVersionsDocuments.CheckedChanged, AddressOf chkVersionsDocuments_CheckedChanged
            If String.IsNullOrEmpty(paramrule.VersionsExportedDocuments) = False Then chkVersionsDocuments.Checked = paramrule.VersionsExportedDocuments
            AddHandler chkVersionsDocuments.CheckedChanged, AddressOf chkVersionsDocuments_CheckedChanged



            If String.IsNullOrEmpty(paramrule.resultLine) = False Then
                Dim indexsNames As String() = paramrule.resultLine.Split(paramrule.separator.ToCharArray)

                DisableControls()
                'Habilito los que correspondan segun Parametro
                For Each _Index As String In indexsNames
                    If String.IsNullOrEmpty(Trim(_Index)) = False Then

                        If Not Trim(_Index).StartsWith("<<") Then
                            For Each chk As Control In lstIndexs.Controls

                                If TypeOf chk Is CheckBox AndAlso chk.Text.Trim = _Index.Trim Then
                                    Dim chktemp As CheckBox = DirectCast(chk, CheckBox)
                                    RemoveHandler chktemp.CheckedChanged, AddressOf chkSelectedIndex_CheckedChanged
                                    chktemp.Checked = True
                                    AddHandler chktemp.CheckedChanged, AddressOf chkSelectedIndex_CheckedChanged
                                    Dim _top As Int32 = chk.Top

                                    For Each cmb As Control In lstIndexs.Controls
                                        If TypeOf cmb Is ComboBox AndAlso cmb.Top = _top Then
                                            Dim cmbtemp As ComboBox = DirectCast(cmb, ComboBox)
                                            cmbtemp.Enabled = True

                                            ' 
                                            For index As Integer = 0 To paramrule.SortString.Split("|").Length() - 1
                                                If paramrule.SortString.Split("|")(index) = chk.Text Then
                                                    'Do
                                                    '    index += 1
                                                    '    If ValidateOrderExistance(index) = False Then
                                                    RemoveHandler cmbtemp.SelectedIndexChanged, AddressOf CmbOrder_SelectedIndexChanged
                                                    cmbtemp.Text = index + 1
                                                    cmbtemp.Tag = index + 1
                                                    cmbtemp.SelectedItem = index + 1
                                                    AddHandler cmbtemp.SelectedIndexChanged, AddressOf CmbOrder_SelectedIndexChanged
                                                    '    Exit Do
                                                    'End If
                                                    '    Loop Until ValidateOrderExistance(index) = False


                                                    Exit For
                                                End If
                                            Next

                                            'Todo: faltaria el orden
                                            Exit For
                                        End If
                                    Next
                                    Exit For
                                End If
                            Next
                        Else
                            ' Es Texto Inteligente

                            Dim Chk As New CheckBox
                            Chk.Text = Trim(_Index)
                            Chk.Width = 160
                            Chk.FlatStyle = FlatStyle.Flat
                            Chk.Checked = True
                            Chk.Left = 8
                            Chk.Top = GetTopPosition()


                            Dim cmb As New ComboBox
                            For Each _Item As Int32 In GetItemsArray()
                                cmb.Items.Add(_Item)
                            Next
                            cmb.Left = 185
                            cmb.Width = 40
                            cmb.Top = GetTopPosition()
                            cmb.DropDownStyle = ComboBoxStyle.DropDownList

                            For i As Integer = 0 To paramrule.SortString.Split("|").Length() - 1
                                If Trim(paramrule.SortString.Split("|")(i)) = Trim(Chk.Text) Then
                                    'Do
                                    '    i += 1
                                    '    If ValidateOrderExistance(i) = False Then
                                    cmb.Text = i + 1
                                    cmb.Tag = i + 1
                                    cmb.SelectedItem = i + 1
                                    '    Exit Do
                                    'End If
                                    '    Loop Until ValidateOrderExistance(i) = False

                                    Exit For
                                End If
                            Next

                            AddHandler cmb.SelectedIndexChanged, AddressOf CmbOrder_SelectedIndexChanged
                            AddHandler Chk.CheckedChanged, AddressOf chkSelectedIndex_CheckedChanged

                            lstIndexs.Controls.Add(Chk)
                            lstIndexs.Controls.Add(cmb)
                            controlsId += 1

                        End If

                    End If
                Next
            End If

            'Pueden quedar combos disabled y que no se le asigno el orden

            Dim top As Int32
            Dim _combobox As ComboBox
            For Each chk As Control In lstIndexs.Controls
                If TypeOf chk Is CheckBox AndAlso DirectCast(chk, CheckBox).Checked = False Then
                    top = chk.Top
                    For Each cmb As Control In lstIndexs.Controls
                        If TypeOf cmb Is ComboBox AndAlso cmb.Top = top Then
                            _combobox = DirectCast(cmb, ComboBox)
                            For i As Integer = 0 To paramrule.SortString.Split("|").Length() - 1
                                If Trim(paramrule.SortString.Split("|")(i)) = Trim(chk.Text) Then
                                    _combobox.Text = i + 1
                                    _combobox.Tag = i + 1
                                    _combobox.SelectedItem = i + 1
                                    'Exit For
                                End If
                            Next
                        End If
                        'Exit For
                    Next
                End If
            Next



            SetLine()


            LoadingParameters = False
        Catch ex As Exception
            LoadingParameters = False
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Function ValidateOrderExistance(ByVal index As Int32) As Boolean
        For Each c As Control In lstIndexs.Controls
            If TypeOf c Is ComboBox Then
                Dim cmb As ComboBox = DirectCast(c, ComboBox)
                If Int32.Parse(cmb.Text) = index Then Return True
            End If
        Next
        Return False
    End Function

#End Region

#Region "Eventos"

    Private Sub btnSeleccionar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnSeleccionar.Click
        Try

            If Not String.IsNullOrEmpty(txtDocumentName.Text) AndAlso Not String.IsNullOrEmpty(txtFullPath.Text) _
             AndAlso Not String.IsNullOrEmpty(txtResultLine.Text) AndAlso Not String.IsNullOrEmpty(txtSeparator.Text) Then
                Dim OrderString As String = String.Empty
                Dim sortStr As New SortedList

                Dim top As Int32
                For Each chk As Control In lstIndexs.Controls
                    If TypeOf chk Is CheckBox Then
                        top = chk.Top
                        For Each cmb As Control In lstIndexs.Controls
                            If TypeOf cmb Is ComboBox AndAlso cmb.Top = top Then
                                sortStr.Add(Int16.Parse(cmb.Text), chk.Text)
                                Exit For
                            End If
                        Next
                    End If
                Next


                For Each Item As String In sortStr.Values
                    OrderString += Trim(Item) & "|"
                Next

                OrderString = OrderString.Remove(OrderString.Length - 1, 1)

                paramrule.documentPath = txtFullPath.Text
                paramrule.documentName = txtDocumentName.Text
                paramrule.resultLine = txtResultLine.Text
                paramrule.separator = txtSeparator.Text
                paramrule.DoctypeId = Int64.Parse(CboSeleccion.SelectedValue)
                paramrule.SortString = OrderString.ToString
                paramrule.VersionsExportedDocuments = chkVersionsDocuments.Checked

                WFRulesBusiness.UpdateParamItem(paramrule.ID, 0, paramrule.DoctypeId)
                WFRulesBusiness.UpdateParamItem(paramrule.ID, 1, paramrule.separator)
                WFRulesBusiness.UpdateParamItem(paramrule.ID, 2, paramrule.resultLine)
                WFRulesBusiness.UpdateParamItem(paramrule.ID, 3, paramrule.documentName)
                WFRulesBusiness.UpdateParamItem(paramrule.ID, 4, paramrule.documentPath)
                WFRulesBusiness.UpdateParamItem(paramrule.ID, 5, paramrule.SortString)
                If chkVersionsDocuments.Checked Then
                    WFRulesBusiness.UpdateParamItem(paramrule.ID, 6, 1)
                Else
                    WFRulesBusiness.UpdateParamItem(paramrule.ID, 6, 0)
                End If
                UserBusiness.Rights.SaveAction(paramrule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & paramrule.Name & "(" & paramrule.ID & ")")

            Else
                MessageBox.Show("Complete la ubicacion del archivo a exportar, su nombre, Y defina Atributos a exportar", "Zamba", MessageBoxButtons.OK)
            End If


        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Protected Sub CmbOrder_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs)
        Dim cmbSender As ComboBox = DirectCast(sender, ComboBox)
        For Each c As Control In lstIndexs.Controls
            If TypeOf c Is ComboBox Then
                Dim cmb As ComboBox = DirectCast(c, ComboBox)
                If cmb.Text = cmbSender.Text AndAlso cmb.Text = cmb.Tag Then
                    RemoveHandler cmb.SelectedIndexChanged, AddressOf CmbOrder_SelectedIndexChanged
                    cmb.Text = cmbSender.Tag
                    cmb.Tag = cmb.Text
                    cmbSender.Tag = cmbSender.Text
                    AddHandler cmb.SelectedIndexChanged, AddressOf CmbOrder_SelectedIndexChanged
                End If
            End If
        Next
        SetLine()

    End Sub

    Protected Sub chkSelectedIndex_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs)
        Dim chkSelected As CheckBox = DirectCast(sender, CheckBox)
        Dim _Top As Int32 = chkSelected.Top

        For Each c As Control In lstIndexs.Controls
            If TypeOf c Is ComboBox AndAlso DirectCast(c, ComboBox).Top = _Top Then
                DirectCast(c, ComboBox).Enabled = chkSelected.Checked
                Exit For
            End If
        Next

        SetLine()
    End Sub

    Private Function GetOrder() As Int32
        Dim col As Generic.List(Of Int32) = GetItemsArray()
        Dim valid As Boolean = False
        Dim ValidItem As Int32

        For Each Item As Int32 In col

            For Each c As Control In lstIndexs.Controls
                If TypeOf c Is ComboBox Then
                    If DirectCast(c, ComboBox).Text = Item Then
                        valid = False
                        Exit For
                    Else
                        valid = True
                        ValidItem = Item
                    End If
                End If
            Next
            If valid Then
                Return ValidItem
            End If
        Next
        Return 0
    End Function

    Private Sub CboSeleccion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles CboSeleccion.SelectedIndexChanged
        If LoadingParameters = False Then
            paramrule.resultLine = String.Empty
            paramrule.SortString = String.Empty
        End If
        LoadIndexs()
    End Sub


    Private Sub btnAddCustomColumn_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAddCustomColumn.Click

        If Not String.IsNullOrEmpty(txtCustomItem.Text) Then

            Dim cmb As New ComboBox
            For Each _Item As Int32 In GetItemsArray(True)
                cmb.Items.Add(_Item)
            Next
            cmb.Items.Add(cmb.Items(cmb.Items.Count - 1) + 1)

            cmb.Text = controlsId
            cmb.SelectedItem = controlsId
            cmb.Left = 185
            cmb.Width = 40
            cmb.Tag = controlsId
            cmb.DropDownStyle = ComboBoxStyle.DropDownList
            cmb.Top = GetTopPosition()

            Dim Chk As New CheckBox
            Chk.Text = txtCustomItem.Text
            Chk.FlatStyle = FlatStyle.Flat
            Chk.Checked = True
            Chk.Left = 8
            Chk.Top = GetTopPosition()

            'Suma un nuevo elemento a los items del combo
            For Each c As Control In lstIndexs.Controls
                If TypeOf c Is ComboBox Then
                    If Not DirectCast(c, ComboBox).Items.Contains(cmb.Items(cmb.Items.Count - 1)) Then
                        DirectCast(c, ComboBox).Items.Add(cmb.Items(cmb.Items.Count - 1))
                    End If

                End If
            Next

            AddHandler cmb.SelectedIndexChanged, AddressOf CmbOrder_SelectedIndexChanged
            AddHandler Chk.CheckedChanged, AddressOf chkSelectedIndex_CheckedChanged

            lstIndexs.Controls.Add(Chk)
            lstIndexs.Controls.Add(cmb)

            controlsId += 1

            SetLine()
            txtCustomItem.Text = String.Empty
        Else
            MessageBox.Show("Complete Un dato para Insertar", "Zamba", MessageBoxButtons.OK)
        End If
    End Sub

    Private Sub btnExaminar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnExaminar.Click
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            txtFullPath.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub chkVersionsDocuments_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkVersionsDocuments.CheckedChanged
        If chkVersionsDocuments.Checked Then
            MessageBox.Show("Se crearán documentos de textos como cantidad de Documentos ejecuten la regla", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Se creará un unico Documento de texto que contenerá la información de todos los documentos que ejecuten la regla", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
#End Region

End Class
