Imports Zamba.Servers
Imports System.Windows.Forms
'Imports Zamba.Data
Imports Zamba.core

Public Class UCCopyQuery
    Inherits Zamba.AppBlock.ZControl

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents CboDoctype As System.Windows.Forms.ComboBox
    Friend WithEvents Grilla As System.Windows.Forms.DataGrid
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GRILLASENTENCE As System.Windows.Forms.DataGrid
    Friend WithEvents ButtonSENTENCE As System.Windows.Forms.Button
    Friend WithEvents CboDoctypeSENTENCE As System.Windows.Forms.ComboBox
    Friend WithEvents borrSentencia As System.Windows.Forms.Button
    Friend WithEvents borrUpdate As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Grilla = New System.Windows.Forms.DataGrid
        Me.Button1 = New System.Windows.Forms.Button
        Me.CboDoctype = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.GRILLASENTENCE = New System.Windows.Forms.DataGrid
        Me.ButtonSENTENCE = New System.Windows.Forms.Button
        Me.CboDoctypeSENTENCE = New System.Windows.Forms.ComboBox
        Me.borrSentencia = New System.Windows.Forms.Button
        Me.borrUpdate = New System.Windows.Forms.Button
        CType(Me.Grilla, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GRILLASENTENCE, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Grilla
        '
        Me.Grilla.AlternatingBackColor = System.Drawing.Color.Lavender
        Me.Grilla.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Grilla.BackgroundColor = System.Drawing.Color.LightGray
        Me.Grilla.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Grilla.CaptionBackColor = System.Drawing.Color.LightSteelBlue
        Me.Grilla.CaptionFont = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.Grilla.CaptionForeColor = System.Drawing.Color.MidnightBlue
        Me.Grilla.DataMember = ""
        Me.Grilla.FlatMode = True
        Me.Grilla.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.Grilla.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Grilla.GridLineColor = System.Drawing.Color.Gainsboro
        Me.Grilla.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None
        Me.Grilla.HeaderBackColor = System.Drawing.Color.MidnightBlue
        Me.Grilla.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.Grilla.HeaderForeColor = System.Drawing.Color.WhiteSmoke
        Me.Grilla.LinkColor = System.Drawing.Color.Teal
        Me.Grilla.Location = New System.Drawing.Point(16, 32)
        Me.Grilla.Name = "Grilla"
        Me.Grilla.ParentRowsBackColor = System.Drawing.Color.Gainsboro
        Me.Grilla.ParentRowsForeColor = System.Drawing.Color.MidnightBlue
        Me.Grilla.SelectionBackColor = System.Drawing.Color.CadetBlue
        Me.Grilla.SelectionForeColor = System.Drawing.Color.WhiteSmoke
        Me.Grilla.Size = New System.Drawing.Size(576, 120)
        Me.Grilla.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(408, 192)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(152, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Agregar a Dual Control"
        '
        'CboDoctype
        '
        Me.CboDoctype.Location = New System.Drawing.Point(408, 160)
        Me.CboDoctype.Name = "CboDoctype"
        Me.CboDoctype.Size = New System.Drawing.Size(152, 21)
        Me.CboDoctype.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(24, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(216, 16)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Actualizacion"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(24, 248)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(152, 16)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Sentencia"
        '
        'GRILLASENTENCE
        '
        Me.GRILLASENTENCE.AlternatingBackColor = System.Drawing.Color.Lavender
        Me.GRILLASENTENCE.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GRILLASENTENCE.BackgroundColor = System.Drawing.Color.LightGray
        Me.GRILLASENTENCE.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.GRILLASENTENCE.CaptionBackColor = System.Drawing.Color.LightSteelBlue
        Me.GRILLASENTENCE.CaptionFont = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.GRILLASENTENCE.CaptionForeColor = System.Drawing.Color.MidnightBlue
        Me.GRILLASENTENCE.DataMember = ""
        Me.GRILLASENTENCE.FlatMode = True
        Me.GRILLASENTENCE.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.GRILLASENTENCE.ForeColor = System.Drawing.Color.MidnightBlue
        Me.GRILLASENTENCE.GridLineColor = System.Drawing.Color.Gainsboro
        Me.GRILLASENTENCE.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None
        Me.GRILLASENTENCE.HeaderBackColor = System.Drawing.Color.MidnightBlue
        Me.GRILLASENTENCE.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.GRILLASENTENCE.HeaderForeColor = System.Drawing.Color.WhiteSmoke
        Me.GRILLASENTENCE.LinkColor = System.Drawing.Color.Teal
        Me.GRILLASENTENCE.Location = New System.Drawing.Point(16, 272)
        Me.GRILLASENTENCE.Name = "GRILLASENTENCE"
        Me.GRILLASENTENCE.ParentRowsBackColor = System.Drawing.Color.Gainsboro
        Me.GRILLASENTENCE.ParentRowsForeColor = System.Drawing.Color.MidnightBlue
        Me.GRILLASENTENCE.SelectionBackColor = System.Drawing.Color.CadetBlue
        Me.GRILLASENTENCE.SelectionForeColor = System.Drawing.Color.WhiteSmoke
        Me.GRILLASENTENCE.Size = New System.Drawing.Size(576, 120)
        Me.GRILLASENTENCE.TabIndex = 5
        '
        'ButtonSENTENCE
        '
        Me.ButtonSENTENCE.Location = New System.Drawing.Point(400, 432)
        Me.ButtonSENTENCE.Name = "ButtonSENTENCE"
        Me.ButtonSENTENCE.Size = New System.Drawing.Size(152, 23)
        Me.ButtonSENTENCE.TabIndex = 6
        Me.ButtonSENTENCE.Text = "Agregar a Dual Control"
        '
        'CboDoctypeSENTENCE
        '
        Me.CboDoctypeSENTENCE.Location = New System.Drawing.Point(400, 400)
        Me.CboDoctypeSENTENCE.Name = "CboDoctypeSENTENCE"
        Me.CboDoctypeSENTENCE.Size = New System.Drawing.Size(152, 21)
        Me.CboDoctypeSENTENCE.TabIndex = 7
        '
        'borrSentencia
        '
        Me.borrSentencia.Location = New System.Drawing.Point(24, 400)
        Me.borrSentencia.Name = "borrSentencia"
        Me.borrSentencia.Size = New System.Drawing.Size(75, 24)
        Me.borrSentencia.TabIndex = 8
        Me.borrSentencia.Text = "Borrar"
        '
        'borrUpdate
        '
        Me.borrUpdate.Location = New System.Drawing.Point(32, 160)
        Me.borrUpdate.Name = "borrUpdate"
        Me.borrUpdate.Size = New System.Drawing.Size(75, 24)
        Me.borrUpdate.TabIndex = 9
        Me.borrUpdate.Text = "Borrar"
        '
        'UCCopyQuery
        '
        Me.Controls.Add(Me.borrUpdate)
        Me.Controls.Add(Me.borrSentencia)
        Me.Controls.Add(Me.CboDoctypeSENTENCE)
        Me.Controls.Add(Me.ButtonSENTENCE)
        Me.Controls.Add(Me.GRILLASENTENCE)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CboDoctype)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Grilla)
        Me.Name = "UCCopyQuery"
        Me.Size = New System.Drawing.Size(656, 480)
        CType(Me.Grilla, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GRILLASENTENCE, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    ''' Para AMRO Bank
    ''' 
    Dim NewqueryId As Int32
    Dim OK As Boolean = True

    Private Sub UCCopyQuery_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            LoadUpdates()
        Catch ex As Exception
        End Try
        Try
            LoadSentence()
        Catch ex As Exception
        End Try
        Try
            Dim sql As String = "Select Doc_type_id, Doc_type_name from doc_type order by doc_type_name"
            Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
            CboDoctype.DataSource = ds.Tables(0)
            Me.CboDoctypeSENTENCE().DataSource = ds.Tables(0)
            CboDoctype.ValueMember = "Doc_type_id"
            Me.CboDoctypeSENTENCE.ValueMember = "Doc_type_id"
            CboDoctype.DisplayMember = "Doc_type_name"
            Me.CboDoctypeSENTENCE.DisplayMember = "Doc_type_name"
            CboDoctype.Refresh()
            Me.CboDoctypeSENTENCE.Refresh()
        Catch ex As Exception
        End Try

    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If Not IsNothing(Me.Grilla.Item(Me.Grilla.CurrentRowIndex, 0)) AndAlso Me.Grilla.Item(Me.Grilla.CurrentRowIndex, 0) <> 0 AndAlso Me.CboDoctype.SelectedValue <> 0 Then
                Me.GenerarNuevaConsulta(Convert.ToInt32(Me.Grilla.Item(Me.Grilla.CurrentRowIndex, 0)), Convert.ToInt32(Me.CboDoctype.SelectedValue))
                If OK Then MessageBox.Show("CONSULTA CREADA", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Seleccione una consulta de la grilla y el documento que desea agregar del combo", "ZAMBA")
            End If
        Catch ex As Exception
        End Try
        Try
            LoadUpdates()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ButtonSENTENCE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSENTENCE.Click
        Try
            If Not IsNothing(Me.GRILLASENTENCE.Item(Me.GRILLASENTENCE.CurrentRowIndex, 0)) AndAlso Me.GRILLASENTENCE.Item(Me.GRILLASENTENCE.CurrentRowIndex, 0) <> 0 AndAlso Me.CboDoctypeSENTENCE.SelectedValue <> 0 Then
                Me.GenerarNuevaSentencia(Convert.ToInt32(Me.GRILLASENTENCE.Item(Me.GRILLASENTENCE.CurrentRowIndex, 0)), Convert.ToInt32(Me.CboDoctypeSENTENCE.SelectedValue))
                If OK Then MessageBox.Show("CONSULTA CREADA", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Seleccione una consulta de la grilla y el documento que desea agregar del combo", "ZAMBA")
            End If
        Catch ex As Exception
        End Try
        Try
            LoadSentence()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GenerarNuevaConsulta(ByVal idquery As Int32, ByVal docTypeId As Int64)
        Dim strinsert As String
        Dim QueryName As String = InputBox("Complete el nombre para la nueva consulta", "Zamba")
        If QueryName.Trim = "" Then
            OK = False
            Exit Sub
        End If
        Dim sql As String = "Select servername, usuario, clave, servertype,DB,Querytype from ZQueryname where id=" & idquery
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
        Dim i As Int32
        'ZQueryname
        NewqueryId = CoreBusiness.GetNewID(Zamba.Core.IdTypes.ZQUERY)

        strinsert = "Insert into ZQueryname(id,nombre,servername,usuario,clave,servertype,db,querytype) values(" & NewqueryId.ToString() & ",'" & QueryName.ToString() & "','" & ds.Tables(0).Rows(0).Item(0).ToString() & "','" & ds.Tables(0).Rows(0).Item(1).ToString() & "','" & ds.Tables(0).Rows(0).Item(2).ToString() & "'," & ds.Tables(0).Rows(0).Item(3).ToString() & ",'" & ds.Tables(0).Rows(0).Item(4).ToString() & "','" & ds.Tables(0).Rows(0).Item(5).ToString() & "')"
        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
        'ZQColumns
        Try
            sql = "Select Selecttable, Selectcolumns, Value from ZQColumns where id=" & idquery
            ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
            For i = 0 To ds.Tables(0).Rows.Count - 1
                sql = "Insert into Zqcolumns(id,Selecttable, Selectcolumns, Value) values(" & NewqueryId.ToString() & ",'DOC_I" & doctypeid.ToString() & "','" & ds.Tables(0).Rows(i).Item(1).ToString() & "','=''Aprobado''')"
                Server.Con.ExecuteNonQuery(CommandType.Text, sql)
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        'ZQKeys
        sql = "Select claves,Value from zqkeys where id=" & idquery
        ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
        For i = 0 To ds.Tables(0).Rows.Count - 1
            sql = "insert into ZQKeys(id,claves,Value,Selecttable) values (" & NewqueryId.ToString() & ",'" & ds.Tables(0).Rows(i).Item(0).ToString() & "','=''Pendiente''','DOC_I" & doctypeid.ToString() & "')"
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        Next
    End Sub

    Private Sub GenerarNuevaSentencia(ByVal idquery As Int32, ByVal docTypeId As Int64)
        Dim strinsert As String
        Dim QueryName As String = InputBox("Complete el nombre para la nueva consulta", "Zamba")
        If QueryName.Trim = "" Then
            OK = False
            Exit Sub
        End If
        Dim sql As String = "Select servername, usuario, clave, servertype,DB,Querytype from ZQueryname where id=" & idquery
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
        Dim i As Int32
        'ZQueryname
        NewqueryId = CoreBusiness.GetNewID(Zamba.Core.IdTypes.ZQUERY) + 1
        strinsert = "Insert into ZQueryname(id,nombre,servername,usuario,clave,servertype,db,querytype) values(" & NewqueryId.ToString() & ",'" & QueryName.ToString() & "','" & ds.Tables(0).Rows(0).Item(0).ToString() & "','" & ds.Tables(0).Rows(0).Item(1).ToString() & "','" & ds.Tables(0).Rows(0).Item(2).ToString() & "'," & ds.Tables(0).Rows(0).Item(3).ToString() & ",'" & ds.Tables(0).Rows(0).Item(4).ToString() & "','" & ds.Tables(0).Rows(0).Item(5).ToString() & "')"
        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
        'ZQSENTENCE
        Try
            sql = "Select id,Value,type from ZQSENTENCE where id=" & idquery
            ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
            Dim newValue As String = "SELECT COUNT(I7) AS Cantidad, I7 FROM DOC_I" & doctypeid & " GROUP BY I7 ORDER BY 2"
            sql = "Insert into ZQSENTENCE(id,Value, type) values(" & NewqueryId.ToString() & ",'" & newValue & "','" & ds.Tables(0).Rows(i).Item(2).ToString() & "')"
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Try
            Dim sql2 As String = "delete from zqueryname where id not in (select id from zqsentence) and querytype='Sentence'"
            Server.Con.ExecuteNonQuery(CommandType.Text, sql2)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadUpdates()
        Try
            Dim ds As DataSet
            Dim strUpdate As String = "select id,nombre from zqueryname where querytype='Update' order by id"
            ds = Server.Con.ExecuteDataset(CommandType.Text, strUpdate)
            Me.Grilla.DataSource = Nothing
            Me.Grilla.DataSource = ds.Tables(0)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub LoadSentence()
        Try
            Dim ds As DataSet
            Dim strUpdate As String = "select id,nombre from zqueryname where querytype='Sentence' order by id"
            ds = Server.Con.ExecuteDataset(CommandType.Text, strUpdate)
            Me.GRILLASENTENCE.DataSource = Nothing
            Me.GRILLASENTENCE.DataSource = ds.Tables(0)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub borrUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles borrUpdate.Click
        Try
            Dim sql As String
            sql = "Delete From ZQueryName where id=" & Me.Grilla.Item(Me.Grilla.CurrentRowIndex, 0).ToString()
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
            sql = "Delete From ZQColumns where id=" & Me.Grilla.Item(Me.Grilla.CurrentRowIndex, 0).ToString()
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
            sql = "Delete From ZQKeys where id=" & Me.Grilla.Item(Me.Grilla.CurrentRowIndex, 0).ToString()
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
            LoadUpdates()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub borrSentencia_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles borrSentencia.Click
        Try
            Dim sql As String
            sql = "Delete From ZQueryName where id=" & Me.GRILLASENTENCE.Item(Me.GRILLASENTENCE.CurrentRowIndex, 0).ToString()
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
            sql = "Delete From ZQSentence where id=" & Me.GRILLASENTENCE.Item(Me.GRILLASENTENCE.CurrentRowIndex, 0).ToString
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
            LoadSentence()
        Catch ex As Exception
        End Try
    End Sub
End Class
