Imports Zamba.Data
'Imports Zamba.WFBusiness

Public Class UCDoRequestData
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
    Friend WithEvents CboSeleccion As ComboBox
    Friend WithEvents rdbTipoDeDocumento As System.Windows.Forms.RadioButton
    Friend WithEvents rdbIndice As System.Windows.Forms.RadioButton
    Friend WithEvents grpSolicitarDatos As GroupBox
    Friend WithEvents btnAsignIndex As ZButton
    Friend WithEvents PanelCheck As System.Windows.Forms.Panel

    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        btnSeleccionar = New ZButton()
        CboSeleccion = New ComboBox()
        Label1 = New ZLabel()
        PanelCheck = New System.Windows.Forms.Panel()
        rdbTipoDeDocumento = New System.Windows.Forms.RadioButton()
        rdbIndice = New System.Windows.Forms.RadioButton()
        grpSolicitarDatos = New GroupBox()
        btnAsignIndex = New ZButton()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        grpSolicitarDatos.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(Label1)
        tbRule.Controls.Add(btnAsignIndex)
        tbRule.Controls.Add(grpSolicitarDatos)
        tbRule.Controls.Add(PanelCheck)
        tbRule.Controls.Add(CboSeleccion)
        tbRule.Controls.Add(btnSeleccionar)
        tbRule.Size = New System.Drawing.Size(394, 432)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(402, 461)
        '
        'btnSeleccionar
        '
        btnSeleccionar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnSeleccionar.FlatStyle = FlatStyle.Flat
        btnSeleccionar.ForeColor = System.Drawing.Color.White
        btnSeleccionar.Location = New System.Drawing.Point(268, 382)
        btnSeleccionar.Name = "btnSeleccionar"
        btnSeleccionar.Size = New System.Drawing.Size(77, 34)
        btnSeleccionar.TabIndex = 13
        btnSeleccionar.Text = "Guardar"
        btnSeleccionar.UseVisualStyleBackColor = False
        '
        'CboSeleccion
        '
        CboSeleccion.DropDownStyle = ComboBoxStyle.DropDownList
        CboSeleccion.Items.AddRange(New Object() {"Aumentar", "Disminuir"})
        CboSeleccion.Location = New System.Drawing.Point(6, 93)
        CboSeleccion.Name = "CboSeleccion"
        CboSeleccion.Size = New System.Drawing.Size(233, 24)
        CboSeleccion.TabIndex = 18
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Dock = System.Windows.Forms.DockStyle.Top
        Label1.Font = New Font("Verdana", 9.75!)
        Label1.FontSize = 9.75!
        Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label1.Location = New System.Drawing.Point(3, 65)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(57, 16)
        Label1.TabIndex = 19
        Label1.Text = "Entidad"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        '
        'PanelCheck
        '
        PanelCheck.AutoScroll = True
        PanelCheck.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        PanelCheck.Location = New System.Drawing.Point(6, 142)
        PanelCheck.Name = "PanelCheck"
        PanelCheck.Size = New System.Drawing.Size(355, 234)
        PanelCheck.TabIndex = 20
        '
        'rdbTipoDeDocumento
        '
        rdbTipoDeDocumento.AutoSize = True
        rdbTipoDeDocumento.Checked = True
        rdbTipoDeDocumento.Location = New System.Drawing.Point(21, 30)
        rdbTipoDeDocumento.Name = "rdbTipoDeDocumento"
        rdbTipoDeDocumento.Size = New System.Drawing.Size(75, 20)
        rdbTipoDeDocumento.TabIndex = 21
        rdbTipoDeDocumento.TabStop = True
        rdbTipoDeDocumento.Text = "Entidad"
        rdbTipoDeDocumento.UseVisualStyleBackColor = True
        '
        'rdbIndice
        '
        rdbIndice.AutoSize = True
        rdbIndice.Location = New System.Drawing.Point(143, 30)
        rdbIndice.Name = "rdbIndice"
        rdbIndice.Size = New System.Drawing.Size(79, 20)
        rdbIndice.TabIndex = 22
        rdbIndice.TabStop = True
        rdbIndice.Text = "Atributo"
        rdbIndice.UseVisualStyleBackColor = True
        '
        'grpSolicitarDatos
        '
        grpSolicitarDatos.Controls.Add(rdbTipoDeDocumento)
        grpSolicitarDatos.Controls.Add(rdbIndice)
        grpSolicitarDatos.Dock = System.Windows.Forms.DockStyle.Top
        grpSolicitarDatos.Location = New System.Drawing.Point(3, 3)
        grpSolicitarDatos.Name = "grpSolicitarDatos"
        grpSolicitarDatos.Size = New System.Drawing.Size(388, 62)
        grpSolicitarDatos.TabIndex = 23
        grpSolicitarDatos.TabStop = False
        grpSolicitarDatos.Text = "Solicitar Datos Por"
        '
        'btnAsignIndex
        '
        btnAsignIndex.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnAsignIndex.FlatStyle = FlatStyle.Flat
        btnAsignIndex.ForeColor = System.Drawing.Color.White
        btnAsignIndex.Location = New System.Drawing.Point(268, 93)
        btnAsignIndex.Name = "btnAsignIndex"
        btnAsignIndex.Size = New System.Drawing.Size(93, 33)
        btnAsignIndex.TabIndex = 24
        btnAsignIndex.Text = "Agregar"
        btnAsignIndex.UseVisualStyleBackColor = True
        '
        'UCDoRequestData
        '
        Name = "UCDoRequestData"
        Size = New System.Drawing.Size(402, 461)
        tbRule.ResumeLayout(False)
        tbRule.PerformLayout()
        tbctrMain.ResumeLayout(False)
        grpSolicitarDatos.ResumeLayout(False)
        grpSolicitarDatos.PerformLayout()
        ResumeLayout(False)

    End Sub

#End Region

    Private this As IDoRequestData
    Private flag As Boolean = False
    Dim UbicacionTop As Int32 = 5
    Public Sub New(ByRef this As IDoRequestData, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(this, _wfPanelCircuit)
        InitializeComponent()
        btnAsignIndex.Visible = False
        flag = True
        Try
            Me.this = this
            If Me.this.DocTypeId > -1 Then
                RemoveHandler rdbIndice.CheckedChanged, AddressOf rdbIndice_CheckedChanged
                RemoveHandler rdbTipoDeDocumento.CheckedChanged, AddressOf rdbTipoDeDocumento_CheckedChanged
                rdbTipoDeDocumento.Checked = True
                AddHandler rdbIndice.CheckedChanged, AddressOf rdbIndice_CheckedChanged
                AddHandler rdbTipoDeDocumento.CheckedChanged, AddressOf rdbTipoDeDocumento_CheckedChanged
                LoadAllDocTypes()
            Else
                Label1.Text = "Atributo"
                btnAsignIndex.Visible = True
                RemoveHandler rdbIndice.CheckedChanged, AddressOf rdbIndice_CheckedChanged
                RemoveHandler rdbTipoDeDocumento.CheckedChanged, AddressOf rdbTipoDeDocumento_CheckedChanged
                rdbIndice.Checked = True
                AddHandler rdbIndice.CheckedChanged, AddressOf rdbIndice_CheckedChanged
                AddHandler rdbTipoDeDocumento.CheckedChanged, AddressOf rdbTipoDeDocumento_CheckedChanged
                LoadAllIndexs()
            End If
            HasBeenModified = False
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub



#Region "DocTypes / Indexs"
    Dim DsDocTypes As DataSet
    Private Sub LoadAllDocTypes()
        Try
            DsDocTypes = DocTypesFactory.GetDocTypesDsDocType()
            If DsDocTypes.Tables("DOC_TYPE").Rows.Count <= 0 Then
                MessageBox.Show("Error 013: No hay definidos Entidades para realizar la indexacion o no tiene Permisos para crear Documentos", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show("Error: 014: Ocurrio un Error al cargar los Entidades para la Indexación ", "Zamba Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ZClass.raiseerror(ex)
        End Try

        Try
            RemoveHandler CboSeleccion.SelectedIndexChanged, AddressOf SelectedIndexChanged
            CboSeleccion.BeginUpdate()
            CboSeleccion.DataSource = DsDocTypes.Tables("DOC_TYPE")
            CboSeleccion.DisplayMember = "DOC_TYPE_NAME"
            CboSeleccion.ValueMember = "DOC_TYPE_ID"
            CboSeleccion.EndUpdate()
            AddHandler CboSeleccion.SelectedIndexChanged, AddressOf SelectedIndexChanged
            SelectDocType()
        Catch ex As Exception
            MessageBox.Show("Error: 014: Ocurrio un Error al cargar los Entidades ", "Zamba Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub SelectDocType()

        If this.DocTypeId = 0 Then
            CboSeleccion.SelectedIndex = 0
        Else
            RemoveHandler CboSeleccion.SelectedIndexChanged, AddressOf SelectedIndexChanged
            CboSeleccion.SelectedValue = this.DocTypeId
            AddHandler CboSeleccion.SelectedIndexChanged, AddressOf SelectedIndexChanged
        End If

        SelectedIndexChanged(CboSeleccion, New EventArgs)
    End Sub
    Private Sub LoadAllIndexs()
        Try
            Dim dsIndexs As DataSet = IndexsBusiness.GetIndex()
            If Not IsNothing(dsIndexs.Tables(0)) AndAlso dsIndexs.Tables(0).Rows.Count > 0 Then
                RemoveHandler CboSeleccion.SelectedIndexChanged, AddressOf SelectedIndexChanged
                CboSeleccion.DataSource = dsIndexs.Tables(0)
                CboSeleccion.DisplayMember = "Index_Name"
                CboSeleccion.ValueMember = "Index_Id"
                AddHandler CboSeleccion.SelectedIndexChanged, AddressOf SelectedIndexChanged
                If this.DocTypeId = -1 AndAlso this.ArrayIds.Count > 0 Then
                    For Each IndexId As Int64 In this.ArrayIds
                        Dim Chk As New CheckBox
                        Chk.Text = IndexsBusiness.GetIndexNameById(IndexId)
                        Chk.Tag = IndexId
                        Chk.FlatStyle = FlatStyle.Flat
                        Chk.Height = 18
                        Chk.Checked = True
                        Chk.Width = 205
                        Chk.Left = 15
                        Chk.Top = Top
                        Top += 20
                        PanelCheck.Controls.Add(Chk)
                        UbicacionTop += 20
                    Next
                End If
            Else
                CboSeleccion.DataSource = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show("Error: 014: Ocurrio un Error al cargar los atributos ", "Zamba Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs)
        Try

            If IsNothing(CboSeleccion.SelectedValue) Then Exit Sub
            If rdbTipoDeDocumento.Checked Then
                Dim ArrayIndices As List(Of IIndex) = IndexsBussinesExt.getAllIndexesByDocTypeID(CboSeleccion.SelectedValue)

                PanelCheck.Controls.Clear()
                Dim Top As Int32 = 5

                For Each Index As Zamba.Core.Index In ArrayIndices
                    Dim Chk As New CheckBox
                    Chk.Text = Index.Name
                    Chk.Tag = Index.ID
                    Chk.FlatStyle = FlatStyle.Flat
                    Chk.Height = 18
                    Chk.Width = 205
                    Chk.Left = 15
                    Chk.Top = Top
                    Top += 20

                    PanelCheck.Controls.Add(Chk)
                Next

                If IsNothing(this.ArrayIds) = False AndAlso this.ArrayIds.Count > 0 Then
                    '  Dim i As Int32
                    For Each Chk As CheckBox In PanelCheck.Controls
                        If this.ArrayIds.Contains(Chk.Tag) Then Chk.Checked = True
                    Next
                End If
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

#End Region


    Public Shadows ReadOnly Property MyRule() As IDoRequestData
        Get
            Return DirectCast(Rule, IDoRequestData)
        End Get
    End Property

#Region "Eventos"


    Private Sub btnSeleccionar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnSeleccionar.Click
        Try
            If Not IsNothing(CboSeleccion.SelectedValue) Then
                If rdbTipoDeDocumento.Checked Then
                    this.DocTypeId = CboSeleccion.SelectedValue
                Else
                    this.DocTypeId = -1
                End If

                Dim ArrayIds As New List(Of Int64)
                For Each Chk As CheckBox In PanelCheck.Controls
                    If Chk.Checked = True Then ArrayIds.Add(Chk.Tag)
                Next
                this.ArrayIds = ArrayIds
                WFRulesBusiness.UpdateParamItem(this, 0, this.DocTypeId)
                WFRulesBusiness.UpdateParamItem(this, 1, this.JoinIds)
                UserBusiness.Rights.SaveAction(this.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & this.Name & "(" & this.ID & ")")
            End If
            HasBeenModified = False
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub rdbTipoDeDocumento_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rdbTipoDeDocumento.CheckedChanged
        If flag = True Then
            btnAsignIndex.Visible = False
            Label1.Text = "Entidad"
            PanelCheck.Controls.Clear()
            LoadAllDocTypes()
        End If
    End Sub

    Private Sub rdbIndice_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rdbIndice.CheckedChanged
        btnAsignIndex.Visible = True
        Label1.Text = "Atributo"
        UbicacionTop = 5
        PanelCheck.Controls.Clear()
        LoadAllIndexs()
    End Sub

    Private Sub btnAsignIndex_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAsignIndex.Click
        If Not IsNothing(CboSeleccion.SelectedValue) Then

            Dim Chk As New CheckBox
            Dim flagRepeated As Boolean = False
            Chk.Text = Trim(CboSeleccion.Text)
            Chk.Tag = CboSeleccion.SelectedValue

            Chk.Top = UbicacionTop
            Chk.FlatStyle = FlatStyle.Flat
            Chk.Height = 18
            Chk.Width = 205

            Chk.Left = 15
            Chk.Checked = True
            For Each c As Control In PanelCheck.Controls
                If c.Tag = Chk.Tag Then
                    flagRepeated = True
                End If
            Next
            If flagRepeated = False Then
                PanelCheck.Controls.Add(Chk)
                UbicacionTop += 20
            Else
                MessageBox.Show("El Atributo ya  se encuentra agregado", "Zamba", MessageBoxButtons.OK)
            End If
        End If
        HasBeenModified = True
    End Sub

    Private Sub PanelCheck_Paint(sender As Object, e As PaintEventArgs) Handles PanelCheck.Paint
        HasBeenModified = True
    End Sub

    Private Sub CboSeleccion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboSeleccion.SelectedIndexChanged

    End Sub

#End Region

End Class
