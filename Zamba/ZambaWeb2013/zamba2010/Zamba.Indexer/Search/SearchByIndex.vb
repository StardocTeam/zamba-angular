Imports Zamba.AppBlock
Imports ZAMBA.Core
Imports Zamba.data
Imports zamba
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
    Friend WithEvents Panel1 As ZBluePanel
    Friend WithEvents Panel2 As ZBluePanel
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Button1 As ZButton
    Friend WithEvents btnEliminar As Zamba.AppBlock.ZButton1
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lbltotal As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel1 = New Zamba.AppBlock.ZBluePanel
        Me.lbltotal = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.btnEliminar = New Zamba.AppBlock.ZButton1
        Me.Button1 = New Zamba.AppBlock.ZButton
        Me.Label2 = New Zamba.AppBlock.ZLabel
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.Label1 = New Zamba.AppBlock.ZLabel
        Me.Panel2 = New Zamba.AppBlock.ZBluePanel
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Color1 = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel1.Color2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Panel1.Controls.Add(Me.lbltotal)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.btnEliminar)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.TextBox1)
        Me.Panel1.Controls.Add(Me.ComboBox1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(2, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(808, 108)
        Me.Panel1.TabIndex = 0
        '
        'lbltotal
        '
        Me.lbltotal.BackColor = System.Drawing.Color.Transparent
        Me.lbltotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbltotal.Location = New System.Drawing.Point(642, 83)
        Me.lbltotal.Name = "lbltotal"
        Me.lbltotal.Size = New System.Drawing.Size(62, 17)
        Me.lbltotal.TabIndex = 7
        Me.lbltotal.Text = "0"
        Me.lbltotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lbltotal.Visible = False
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Location = New System.Drawing.Point(516, 84)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(124, 17)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Resultados: "
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label3.Visible = False
        '
        'btnEliminar
        '
        Me.btnEliminar.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnEliminar.Location = New System.Drawing.Point(611, 53)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(167, 25)
        Me.btnEliminar.TabIndex = 5
        Me.btnEliminar.Text = "Eliminar Documento"
        '
        'Button1
        '
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.None
        Me.Button1.Location = New System.Drawing.Point(512, 53)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(90, 25)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "Buscar"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(260, 29)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 23)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Valor:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.White
        Me.TextBox1.Location = New System.Drawing.Point(255, 55)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(231, 21)
        Me.TextBox1.TabIndex = 2
        '
        'ComboBox1
        '
        Me.ComboBox1.BackColor = System.Drawing.Color.White
        Me.ComboBox1.Location = New System.Drawing.Point(37, 55)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(187, 21)
        Me.ComboBox1.TabIndex = 1
        Me.ComboBox1.Text = "ComboBox1"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(39, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 23)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Indice:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel2
        '
        Me.Panel2.Color1 = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel2.Color2 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Panel2.Controls.Add(Me.ListView1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(2, 110)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(808, 373)
        Me.Panel2.TabIndex = 1
        '
        'ListView1
        '
        Me.ListView1.BackColor = System.Drawing.Color.White
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView1.FullRowSelect = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(0, 0)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(808, 373)
        Me.ListView1.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.ListView1.TabIndex = 0
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Nombre"
        Me.ColumnHeader1.Width = 300
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Tipo de Documento"
        Me.ColumnHeader2.Width = 150
        '
        'SearchByIndex
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(812, 485)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "SearchByIndex"
        Me.Text = "Busqueda de Indices"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        FillIndexs()
    End Sub
    Dim DsIndex As DSIndex
    Private Sub FillIndexs()
        DsIndex = Zamba.Core.IndexsBusiness.GetIndex()
        Me.ComboBox1.DataSource = DsIndex.DOC_INDEX
        Me.ComboBox1.DisplayMember = "INDEX_NAME"
        Me.ComboBox1.ValueMember = "INDEX_ID"
    End Sub

    Private Function SearchbyIndexs(ByVal indexId As Int32, ByVal indexType As Int32, ByVal dt As DocType, ByVal IndexData As String) As DataSet
        Return Results_Business.searchbyindexs(indexId, indexType, dt, IndexData)
    End Function

    Private Shared Function GetDocumentData(ByVal ds As DataSet, ByVal dt As DocType, ByVal i As Int32) As DataSet
        Return Results_Business.getdocumentdata(ds, dt, i)
    End Function
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            'TODO STORE : "Zsp_Workflow_100_GetIndexesByDocType"
            If Me.TextBox1.Text <> "" Then
                Try
                    Me.Cursor = Cursors.WaitCursor
                    Dim LVItem As dListViewItem
                    Dim indexId As Int32 = CInt(Me.ComboBox1.SelectedValue)
                    Dim indexType As Int32 = CInt(Me.DsIndex.DOC_INDEX(Me.ComboBox1.SelectedIndex).INDEX_TYPE)
                    Dim indexName As String = Me.DsIndex.DOC_INDEX(Me.ComboBox1.SelectedIndex).INDEX_NAME

                    Dim index(0) As Index
                    index(0) = New Index(indexName, indexId, DirectCast(indexType, Core.IndexDataType), Nothing, Nothing)
                    Dim doctypes() As DocType = Zamba.Core.ZCore.FilterDocTypes(index)

                    Dim sql As New System.Text.StringBuilder
                    'Dim sql As  System.Text.StringBuilder
                    Dim ds As DataSet
                    Me.ListView1.BeginUpdate()
                    Me.ListView1.Items.Clear()

                    Dim TotalCount As Int64
                    Me.lbltotal.Visible = True
                    Me.Label3.Visible = True
                    For Each dt As DocType In doctypes
                        Try
                            ds = SearchbyIndexs(indexId, indexType, dt, Me.TextBox1.Text)
                            TotalCount += ds.Tables(0).Rows.Count
                            Me.lbltotal.Text = TotalCount.ToString

                            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                                For i As Int32 = 0 To ds.Tables(0).Rows.Count - 1
                                    Dim ds2 As DataSet = GetDocumentData(ds, dt, i)
                                    If ds2.Tables(0).Rows.Count > 0 Then
                                        Dim name As String = Trim(ds2.Tables(0).Rows(0).Item(0).ToString)
                                        Dim doc_file As String = Trim(ds2.Tables(0).Rows(0).Item(1).ToString)
                                        Dim offset As Int32 = CInt(ds2.Tables(0).Rows(0).Item(2))
                                        Dim volid As Int32 = CInt(ds2.Tables(0).Rows(0).Item(3))
                                        Dim DISK_GROUP_ID As Int32 = CInt(ds2.Tables(0).Rows(0).Item(4))
                                        LVItem = New dListViewItem(New Result(CLng(ds.Tables(0).Rows(i).Item(0)), dt, name, doc_file, offset, VolumesBusiness.GetVolumenPathByVolId(volid), DISK_GROUP_ID))
                                        Me.ListView1.Items.Add(LVItem)
                                    End If
                                Next
                            End If
                        Catch ex As Exception
                            Zamba.Core.ZClass.raiseerror(ex)
                        End Try
                    Next
                    Me.lbltotal.Text = TotalCount.ToString
                    Me.ListView1.EndUpdate()
                Catch ex As Exception
                    zamba.core.zclass.raiseerror(ex)
                Finally
                    Me.Cursor = Cursors.Default
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
                Me.Text = result.Name
                Me.SubItems.Add(result.DocType.Name.Trim)
            Catch ex As Exception
                '             zamba.core.zclass.raiseerror(ex)
            End Try
        End Sub
    End Class
    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Try
            If Me.ListView1.SelectedItems.Count > 0 Then
                For Each li As ListViewItem In Me.ListView1.SelectedItems
                    Try
                        Dim dli As dListViewItem = DirectCast(li, dListViewItem)
                        Results_Business.Delete(dli.result, False)
                        Me.ListView1.Items.Remove(li)
                    Catch ex As Exception
                        Try
                            Me.ListView1.Items.Remove(li)
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
