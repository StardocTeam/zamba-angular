Imports Zamba.Data
'Imports Zamba.WFBusiness
'Imports zamba.DocTypes.Factory

Public Class UCDOGetDocAsoc
    Inherits ZRuleControl

#Region " Código generado por el Diseñador de Windows Forms "


    Public Sub New(ByRef GetDocAsoc As IDOGetDocAsoc, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(GetDocAsoc, _wfPanelCircuit)
        InitializeComponent()

    End Sub

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
    Friend WithEvents Label2 As ZLabel
    Friend Shadows WithEvents Label1 As ZLabel
    Friend WithEvents txtVariable As TextBox
    Friend WithEvents lstDocTypes As System.Windows.Forms.ListView
    Friend WithEvents chkContinuarConResultadoObtenido As System.Windows.Forms.CheckBox
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader

    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        btnSeleccionar = New ZButton()
        Label2 = New ZLabel()
        txtVariable = New TextBox()
        Label1 = New ZLabel()
        lstDocTypes = New System.Windows.Forms.ListView()
        ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        chkContinuarConResultadoObtenido = New System.Windows.Forms.CheckBox()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(chkContinuarConResultadoObtenido)
        tbRule.Controls.Add(lstDocTypes)
        tbRule.Controls.Add(Label1)
        tbRule.Controls.Add(txtVariable)
        tbRule.Controls.Add(Label2)
        tbRule.Controls.Add(btnSeleccionar)
        tbRule.Size = New System.Drawing.Size(403, 468)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(411, 497)
        '
        'btnSeleccionar
        '
        btnSeleccionar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnSeleccionar.FlatStyle = FlatStyle.Flat
        btnSeleccionar.ForeColor = System.Drawing.Color.White
        btnSeleccionar.Location = New System.Drawing.Point(278, 351)
        btnSeleccionar.Name = "btnSeleccionar"
        btnSeleccionar.Size = New System.Drawing.Size(87, 28)
        btnSeleccionar.TabIndex = 14
        btnSeleccionar.Text = "Guardar"
        btnSeleccionar.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label2.Location = New System.Drawing.Point(24, 24)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(149, 16)
        Label2.TabIndex = 31
        Label2.Text = "Tipos de Documento:"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtVariable
        '
        txtVariable.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        txtVariable.Location = New System.Drawing.Point(27, 259)
        txtVariable.Name = "txtVariable"
        txtVariable.Size = New System.Drawing.Size(347, 23)
        txtVariable.TabIndex = 34
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label1.Location = New System.Drawing.Point(24, 240)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(65, 16)
        Label1.TabIndex = 35
        Label1.Text = "Variable:"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        '
        'lstDocTypes
        '
        lstDocTypes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        lstDocTypes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        lstDocTypes.CheckBoxes = True
        lstDocTypes.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {ColumnHeader1})
        lstDocTypes.Location = New System.Drawing.Point(24, 43)
        lstDocTypes.Name = "lstDocTypes"
        lstDocTypes.Size = New System.Drawing.Size(350, 176)
        lstDocTypes.TabIndex = 37
        lstDocTypes.UseCompatibleStateImageBehavior = False
        lstDocTypes.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        ColumnHeader1.Text = "TIPOS DE DOCUMENTO"
        ColumnHeader1.Width = 294
        '
        'chkContinuarConResultadoObtenido
        '
        chkContinuarConResultadoObtenido.AutoSize = True
        chkContinuarConResultadoObtenido.Location = New System.Drawing.Point(27, 302)
        chkContinuarConResultadoObtenido.Name = "chkContinuarConResultadoObtenido"
        chkContinuarConResultadoObtenido.Size = New System.Drawing.Size(347, 20)
        chkContinuarConResultadoObtenido.TabIndex = 38
        chkContinuarConResultadoObtenido.Text = "Continuar la ejecucion con el resultado obtenido"
        chkContinuarConResultadoObtenido.UseVisualStyleBackColor = True
        '
        'UCDOGetDocAsoc
        '
        Name = "UCDOGetDocAsoc"
        Size = New System.Drawing.Size(411, 497)
        tbRule.ResumeLayout(False)
        tbRule.PerformLayout()
        tbctrMain.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnSeleccionar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnSeleccionar.Click
        Try
            Dim T As String = String.Empty
            For Each I As ListViewItem In lstDocTypes.Items
                If I.Checked = True Then T += I.Tag & "*"
            Next
            MyRule.tiposDeDocumento = T
            MyRule.Variable = txtVariable.Text
            MyRule.ContinuarConResultadoObtenido = chkContinuarConResultadoObtenido.Checked
            WFRulesBusiness.UpdateParamItem(MyRule, 0, T)
            WFRulesBusiness.UpdateParamItem(MyRule, 1, MyRule.Variable)
            WFRulesBusiness.UpdateParamItem(MyRule, 2, MyRule.ContinuarConResultadoObtenido)
            UserBusiness.Rights.SaveAction(MyRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & MyRule.Name & "(" & MyRule.ID & ")")
        Catch ex As Exception
            Dim exn As New Exception("Error en UCDOGetDocAsoc.btnSeleccionar_Click(), excepción: " & ex.ToString)
            Zamba.Core.ZClass.raiseerror(exn)
            MessageBox.Show("Excepción al guardar: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub UCDOGetDocAsoc_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        txtVariable.Text = MyRule.Variable
        chkContinuarConResultadoObtenido.Checked = MyRule.ContinuarConResultadoObtenido
        LoadAllDocTypes()
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
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Try
            For Each R As DataRow In DsDocTypes.Tables("DOC_TYPE").Rows
                Dim N As New ListViewItem
                N.Tag = R.Item(9)
                N.Text = R.Item(0)
                lstDocTypes.Items.Add(N)
            Next
            SelectDocType()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SelectDocType()

        ' [AlejandroR] 04/01/10 
        ' El indexof falla cuando un id esta contenido dentro de otro
        ' (por ej: si selecciono 1645 y en la lista tengo el 64 pone como seleccionado a los dos
        '
        'For i As Int32 = 0 To Me.lstDocTypes.Items.Count - 1
        '    Try
        '        Dim L As ListViewItem = Me.lstDocTypes.Items(i)
        '        If Me.MyRule.tiposDeDocumento.IndexOf(L.Tag) > -1 Then
        '            L.Checked = True
        '        Else
        '            L.Checked = False
        '        End If
        '    Catch ex As Exception
        '        Zamba.Core.ZClass.raiseerror(ex)
        '    End Try
        'Next

        ' [AlejandroR] 04/01/10 - Created
        ' Se corrige error al marcar como seleccionados los documentos en la lista
        Dim docTypes As New List(Of String)

        For Each docType As String In MyRule.tiposDeDocumento.Split("*")
            If Not String.IsNullOrEmpty(docType) Then
                docTypes.Add(docType)
            End If
        Next

        For Each L As ListViewItem In lstDocTypes.Items
            Try
                If docTypes.Contains(L.Tag) Then
                    L.Checked = True
                Else
                    L.Checked = False
                End If
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        Next

    End Sub

#End Region

    Public Shadows ReadOnly Property MyRule() As IDOGetDocAsoc
        Get
            Return DirectCast(Rule, IDOGetDocAsoc)
        End Get
    End Property
End Class
