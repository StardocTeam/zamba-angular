Imports ZAMBA.Core
Imports Zamba.data
Public Class SearchByIndex
    Inherits ZForm

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
    Friend WithEvents Panel1 As ZPanel
    Friend WithEvents Panel2 As ZPanel
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Button1 As ZButton
    Friend WithEvents btnEliminar As ZButton
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents lbltotal As ZLabel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Panel1 = New ZPanel
        lbltotal = New ZLabel
        Label3 = New ZLabel
        btnEliminar = New ZButton
        Button1 = New ZButton
        Label2 = New ZLabel
        TextBox1 = New TextBox
        ComboBox1 = New ComboBox
        Label1 = New ZLabel
        Panel2 = New ZPanel
        ListView1 = New System.Windows.Forms.ListView
        ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        '
        'Panel1
        '
        Panel1.Controls.Add(lbltotal)
        Panel1.Controls.Add(Label3)
        Panel1.Controls.Add(btnEliminar)
        Panel1.Controls.Add(Button1)
        Panel1.Controls.Add(Label2)
        Panel1.Controls.Add(TextBox1)
        Panel1.Controls.Add(ComboBox1)
        Panel1.Controls.Add(Label1)
        Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Panel1.Location = New System.Drawing.Point(2, 2)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(808, 108)
        Panel1.TabIndex = 0
        '
        'lbltotal
        '
        lbltotal.BackColor = System.Drawing.Color.Transparent
        lbltotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        lbltotal.Location = New System.Drawing.Point(642, 83)
        lbltotal.Name = "lbltotal"
        lbltotal.Size = New System.Drawing.Size(62, 17)
        lbltotal.TabIndex = 7
        lbltotal.Text = "0"
        lbltotal.TextAlign = ContentAlignment.MiddleCenter
        lbltotal.Visible = False
        '
        'Label3
        '
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Label3.Location = New System.Drawing.Point(516, 84)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(124, 17)
        Label3.TabIndex = 6
        Label3.Text = "Resultados: "
        Label3.TextAlign = ContentAlignment.MiddleCenter
        Label3.Visible = False
        '
        'btnEliminar
        '
        btnEliminar.DialogResult = System.Windows.Forms.DialogResult.None
        btnEliminar.Location = New System.Drawing.Point(611, 53)
        btnEliminar.Name = "btnEliminar"
        btnEliminar.Size = New System.Drawing.Size(167, 25)
        btnEliminar.TabIndex = 5
        btnEliminar.Text = "Eliminar Documento"
        '
        'Button1
        '
        Button1.DialogResult = System.Windows.Forms.DialogResult.None
        Button1.Location = New System.Drawing.Point(512, 53)
        Button1.Name = "Button1"
        Button1.Size = New System.Drawing.Size(90, 25)
        Button1.TabIndex = 4
        Button1.Text = "Buscar"
        '
        'Label2
        '
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Font = New Font("Tahoma", 8.0!)
        Label2.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Label2.Location = New System.Drawing.Point(260, 29)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(100, 23)
        Label2.TabIndex = 3
        Label2.Text = "Valor:"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        '
        'TextBox1
        '
        TextBox1.BackColor = System.Drawing.Color.White
        TextBox1.Location = New System.Drawing.Point(255, 55)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New System.Drawing.Size(231, 21)
        TextBox1.TabIndex = 2
        '
        'ComboBox1
        '
        ComboBox1.BackColor = System.Drawing.Color.White
        ComboBox1.Location = New System.Drawing.Point(37, 55)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New System.Drawing.Size(187, 21)
        ComboBox1.TabIndex = 1
        ComboBox1.Text = "ComboBox1"
        '
        'Label1
        '
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Font = New Font("Tahoma", 8.0!)
        Label1.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Label1.Location = New System.Drawing.Point(39, 29)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(100, 23)
        Label1.TabIndex = 0
        Label1.Text = "Atributo:"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        '
        'Panel2
        '
        Panel2.Controls.Add(ListView1)
        Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Panel2.Location = New System.Drawing.Point(2, 110)
        Panel2.Name = "Panel2"
        Panel2.Size = New System.Drawing.Size(808, 373)
        Panel2.TabIndex = 1
        '
        'ListView1
        '
        ListView1.BackColor = System.Drawing.Color.White
        ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {ColumnHeader1, ColumnHeader2})
        ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        ListView1.FullRowSelect = True
        ListView1.HideSelection = False
        ListView1.Location = New System.Drawing.Point(0, 0)
        ListView1.Name = "ListView1"
        ListView1.Size = New System.Drawing.Size(808, 373)
        ListView1.Sorting = System.Windows.Forms.SortOrder.Ascending
        ListView1.TabIndex = 0
        ListView1.UseCompatibleStateImageBehavior = False
        ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        ColumnHeader1.Text = "Nombre"
        ColumnHeader1.Width = 300
        '
        'ColumnHeader2
        '
        ColumnHeader2.Text = "Entidad"
        ColumnHeader2.Width = 150
        '
        'SearchByIndex
        '
        AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        ClientSize = New System.Drawing.Size(812, 485)
        Controls.Add(Panel2)
        Controls.Add(Panel1)
        Name = "SearchByIndex"
        Text = "Busqueda de Atributos"
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel2.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        FillIndexs()
    End Sub
    Dim DsIndex As DataSet
    Private Sub FillIndexs()
        DsIndex = Zamba.Core.IndexsBusiness.GetIndex()
        ComboBox1.DataSource = DsIndex.Tables("DOC_INDEX")
        ComboBox1.DisplayMember = "INDEX_NAME"
        ComboBox1.ValueMember = "INDEX_ID"
    End Sub

    Private Function SearchbyIndexs(ByVal indexId As Int32, ByVal indexType As Int32, ByVal dt As DocType, ByVal IndexData As String) As DataSet
        Return Results_Business.searchbyindexs(indexId, indexType, dt, IndexData)
    End Function

    Private Shared Function GetDocumentData(ByVal ds As DataSet, ByVal dt As DocType, ByVal i As Int32) As DataSet
        Return Results_Business.getdocumentdata(ds, dt, i)
    End Function
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        Try
            'TODO STORE : "Zsp_Workflow_100_GetIndexesByDocType"
            If TextBox1.Text <> "" Then
                Try
                    Cursor = Cursors.WaitCursor
                    Dim LVItem As dListViewItem
                    Dim indexId As Int32 = CInt(ComboBox1.SelectedValue)
                    Dim indexType As Int32 = CInt(DsIndex.Tables("DOC_INDEX")(ComboBox1.SelectedIndex)("INDEX_TYPE"))
                    Dim indexName As String = DsIndex.Tables("DOC_INDEX")(ComboBox1.SelectedIndex)("INDEX_NAME")

                    Dim index(0) As Index
                    index(0) = New Index(indexName, indexId, DirectCast(indexType, IndexDataType), Nothing, Nothing)
                    Dim doctypes() As DocType = Zamba.Core.ZCore.FilterDocTypes(index)

                    Dim sql As New System.Text.StringBuilder
                    'Dim sql As  System.Text.StringBuilder
                    Dim ds As DataSet
                    ListView1.BeginUpdate()
                    ListView1.Items.Clear()

                    Dim TotalCount As Int64
                    lbltotal.Visible = True
                    Label3.Visible = True
                    For Each dt As DocType In doctypes
                        Try
                            ds = SearchbyIndexs(indexId, indexType, dt, TextBox1.Text)
                            TotalCount += ds.Tables(0).Rows.Count
                            lbltotal.Text = TotalCount.ToString

                            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                                For i As Int32 = 0 To ds.Tables(0).Rows.Count - 1
                                    Dim ds2 As DataSet = GetDocumentData(ds, dt, i)
                                    If ds2.Tables(0).Rows.Count > 0 Then
                                        Dim name As String = Trim(ds2.Tables(0).Rows(0).Item(0).ToString)
                                        Dim doc_file As String = Trim(ds2.Tables(0).Rows(0).Item(1).ToString)
                                        Dim offset As Int32 = CInt(ds2.Tables(0).Rows(0).Item(2))
                                        Dim volid As Int32 = CInt(ds2.Tables(0).Rows(0).Item(3))
                                        Dim DISK_GROUP_ID As Int32 = CInt(ds2.Tables(0).Rows(0).Item(4))
                                        LVItem = New dListViewItem(New Result(CLng(ds.Tables(0).Rows(i).Item(0)), dt, name, doc_file, offset, VolumesFactory.GetVolumenPathByVolId(volid), DISK_GROUP_ID))
                                        ListView1.Items.Add(LVItem)
                                    End If
                                Next
                            End If
                        Catch ex As Exception
                            Zamba.Core.ZClass.raiseerror(ex)
                        End Try
                    Next
                    lbltotal.Text = TotalCount.ToString
                    ListView1.EndUpdate()
                Catch ex As Exception
                    zamba.core.zclass.raiseerror(ex)
                Finally
                    Cursor = Cursors.Default
                End Try
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Class dListViewItem
        Inherits ListViewItem
        Public result As Result
        Sub New(ByRef Result As Result)
            Try
                Me.result = result
                Text = result.Name
                SubItems.Add(result.DocType.Name.Trim)
            Catch ex As Exception
                '             zamba.core.zclass.raiseerror(ex)
            End Try
        End Sub
    End Class
    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnEliminar.Click
        Try
            If ListView1.SelectedItems.Count > 0 Then
                For Each li As ListViewItem In ListView1.SelectedItems
                    Try
                        Dim dli As dListViewItem = DirectCast(li, dListViewItem)
                        Results_Business.Delete(dli.result, False)
                        ListView1.Items.Remove(li)
                    Catch ex As Exception
                        Try
                            ListView1.Items.Remove(li)
                        Catch exc As Exception
                            zamba.core.zclass.raiseerror(exc)
                        End Try
                        zamba.core.zclass.raiseerror(ex)
                    End Try
                Next
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
End Class
