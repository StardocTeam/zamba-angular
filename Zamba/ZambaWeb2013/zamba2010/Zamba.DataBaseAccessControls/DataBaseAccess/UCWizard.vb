Imports Zamba.Servers
Imports Zamba.AppBlock
Imports Zamba.Core
'Imports Zamba.DBAccess

Public Class UCWizard
    Inherits System.Windows.Forms.UserControl
#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()
        '   Button1.Enabled = False
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
    Friend WithEvents btnGo As System.Windows.Forms.Button
    Friend WithEvents btnback As System.Windows.Forms.Button
    Friend WithEvents btnCancell As System.Windows.Forms.Button
    Friend WithEvents PanelControl As zbluepanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BtnEnd As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnGo = New System.Windows.Forms.Button
        Me.btnback = New System.Windows.Forms.Button
        Me.btnCancell = New System.Windows.Forms.Button
        Me.PanelControl = New zbluepanel
        Me.BtnEnd = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'btnGo
        '
        Me.btnGo.BackColor = System.Drawing.SystemColors.Control
        Me.btnGo.Enabled = False
        Me.btnGo.Location = New System.Drawing.Point(208, 280)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(88, 24)
        Me.btnGo.TabIndex = 0
        Me.btnGo.Text = "Siguiente"
        Me.btnGo.Visible = False
        '
        'btnback
        '
        Me.btnback.BackColor = System.Drawing.SystemColors.Control
        Me.btnback.Location = New System.Drawing.Point(120, 280)
        Me.btnback.Name = "btnback"
        Me.btnback.Size = New System.Drawing.Size(88, 24)
        Me.btnback.TabIndex = 1
        Me.btnback.Text = "Atrás"
        Me.btnback.Visible = False
        '
        'btnCancell
        '
        Me.btnCancell.BackColor = System.Drawing.SystemColors.Control
        Me.btnCancell.Location = New System.Drawing.Point(16, 280)
        Me.btnCancell.Name = "btnCancell"
        Me.btnCancell.Size = New System.Drawing.Size(88, 24)
        Me.btnCancell.TabIndex = 2
        Me.btnCancell.Text = "Cancelar"
        '
        'PanelControl
        '
        Me.PanelControl.Location = New System.Drawing.Point(16, 32)
        Me.PanelControl.Name = "PanelControl"
        Me.PanelControl.Size = New System.Drawing.Size(376, 232)
        Me.PanelControl.TabIndex = 3
        '
        'BtnEnd
        '
        Me.BtnEnd.BackColor = System.Drawing.SystemColors.Control
        Me.BtnEnd.Enabled = False
        Me.BtnEnd.Location = New System.Drawing.Point(304, 280)
        Me.BtnEnd.Name = "BtnEnd"
        Me.BtnEnd.Size = New System.Drawing.Size(88, 24)
        Me.BtnEnd.TabIndex = 4
        Me.BtnEnd.Text = "Finalizar"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label1.Location = New System.Drawing.Point(16, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(376, 16)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Seleccione la Tabla y las columnas que desea Obtener"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'UCWizard
        '
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BtnEnd)
        Me.Controls.Add(Me.PanelControl)
        Me.Controls.Add(Me.btnCancell)
        Me.Controls.Add(Me.btnback)
        Me.Controls.Add(Me.btnGo)
        Me.Name = "UCWizard"
        Me.Size = New System.Drawing.Size(400, 320)
        Me.ResumeLayout(False)

    End Sub

#End Region


#Region "Variables Globales"
    Dim Tabla As String
    Dim _id As Int32
    Dim UcTable As New UcTable
    Dim con1 As IConnection ' , con2 
    Dim columnasdevueltas As New ArrayList
    Dim ClaveUnica As New ArrayList
    Public key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
    Public iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
    ' Dim Valor As String 'Valor que se quiere para el where
    '  Dim DsSave As New DsPersist
    'Dim sql As String = "Select "
#End Region


    Public Property ID() As Int32
        Get
            Return _id
        End Get
        Set(ByVal Value As Int32)
            _id = Value
        End Set
    End Property

#Region "HELP"
    'Este control permite seleccionar de una base la tabla y las columnas para armar un select
    'el cual se persiste en una base de datos.
    'Se utiliza para como preproceso para los procesos de importación
#End Region

#Region "Handler Event"
    'Private Sub CargarClave(ByVal column As String)
    '    ClaveUnica.Add(column)
    'End Sub
    Private Sub deshabilitarbotones()
        Me.btnback.Enabled = False
        Me.btnGo.Enabled = False
        Me.btnCancell.Enabled = False
        Me.BtnEnd.Enabled = False
    End Sub
    Private Sub HabilitarBoton()
        btnGo.Enabled = True
    End Sub
    Private Sub UCWizard_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim GetQuery As New UCGetQuery
        Dim s As New Server
        s.MakeConnection()
        s.dispose()
        con1 = Server.Con
        RemoveHandler UcTable.TablaName, AddressOf CargarTabla
        RemoveHandler UcTable.ColumnDevuelta, AddressOf AgregarColumnaDevueltas
        'removeHandler UcTable.ClaveUnica, AddressOf AgregarClaveunica
        RemoveHandler UcTable.habilitar, AddressOf HabilitarBoton
        RemoveHandler UcTable.Conexion, AddressOf Conectar
        RemoveHandler UcTable.LastID, AddressOf Ultimoid
        RemoveHandler GetQuery.DeshabilitarBotones, AddressOf deshabilitarbotones
        RemoveHandler UcWhere.Removercolum, AddressOf RemoveClaveUnica
        RemoveHandler UcTable.QuitarColumna, AddressOf RemoverColumnas


        AddHandler UcTable.TablaName, AddressOf CargarTabla
        AddHandler UcTable.ColumnDevuelta, AddressOf AgregarColumnaDevueltas
        'AddHandler UcTable.ClaveUnica, AddressOf AgregarClaveunica
        AddHandler UcTable.habilitar, AddressOf HabilitarBoton
        AddHandler UcTable.Conexion, AddressOf Conectar
        AddHandler UcTable.LastID, AddressOf Ultimoid
        AddHandler GetQuery.DeshabilitarBotones, AddressOf deshabilitarbotones
        RemoveHandler UcWhere.AddWhereColumn, AddressOf AgregarClaveunica
        AddHandler UcWhere.AddWhereColumn, AddressOf AgregarClaveunica
        AddHandler UcWhere.Removercolum, AddressOf RemoveClaveUnica
        AddHandler UcTable.QuitarColumna, AddressOf RemoverColumnas
        Me.PanelControl.Controls.Add(UcTable)
        Me.PanelControl.Controls.Add(GetQuery)
        UcTable.SendToBack()
    End Sub
    Private Sub Ultimoid(ByVal _id As Int32)
        Me.ID = _id
    End Sub
    Private Sub CargarTabla(ByVal nombre As String)
        Tabla = nombre
    End Sub
    Private Sub AgregarColumnaDevueltas(ByVal Column As String)
        columnasdevueltas.Add(Column)
        Me.btnGo.Visible = True
        Me.btnGo.Enabled = True
    End Sub
    Private Sub RemoverColumnas(ByVal columnname As String)
        columnasdevueltas.Remove(columnname)
    End Sub
    Private Sub AgregarClaveunica(ByVal column As String)
        BtnEnd.Enabled = True
        ClaveUnica.Add(column)
    End Sub
    Private Sub RemoveClaveUnica(ByVal columname As String)
        ClaveUnica.Remove(columname)
    End Sub
    Public Shared Event SelectString(ByVal sql As String)
#End Region

    'Pasada a Business, lista para comentar
    'Public Function MakeSelect(ByVal IDConsulta As Int32, ByVal columnasclave As ArrayList) As String
    '    ' Dim getcolumns As New ArrayList
    '    Dim sql As New System.Text.StringBuilder
    '    sql.Append("select SelectTable from ZQColumns where ID=" & IDConsulta)
    '    Try
    '        ' Dim server As New server
    '        ' server.MakeConnection()
    '        '  con1 = server.Con
    '        '  server.dispose()
    '        ' Dim Tabla As String = con1.ExecuteScalar(CommandType.Text, sql)
    '        sql.Remove(0, sql.Length)
    '        sql.Append("Select SelectColumns from ZQColumns where Id=" & IDConsulta)
    '        Dim Dscolumns As DataSet = con1.ExecuteDataset(CommandType.Text, sql.ToString)
    '        sql.Append("Select Claves from ZQKeys where ID=" & IDConsulta)
    '        Dim dsClaves As DataSet = con1.ExecuteDataset(CommandType.Text, sql.ToString)
    '        Dim i As Int32
    '        sql.Append("Select ")
    '        Try
    '            For i = 0 To Dscolumns.Tables(0).Rows.Count - 1
    '                sql.Append(Dscolumns.Tables(0).Rows(i).Item(0))
    '                sql.Append(", ")
    '            Next
    '        Catch ex As Exception
    '            Throw New Exception("La cantidad de claves no coincide con la cantidad de columnas")
    '        End Try

    '        If sql.ToString.EndsWith(", ") Then sql.Replace(sql.ToString, sql.ToString.Substring(0, sql.Length - 2))
    '        sql.Append(" from ")
    '        sql.Append("Tabla")
    '        sql.Append(" Where ")
    '        For i = 0 To dsClaves.Tables(0).Rows.Count - 1
    '            sql.Append(dsClaves.Tables(0).Rows(i).Item(0))
    '            sql.Append("=")
    '            If IsNumeric(columnasclave(i)) = False Then
    '                sql.Append("'")
    '                sql.Append(columnasclave(i))
    '                sql.Append("' and ")
    '            Else
    '                sql.Append(columnasclave(i))
    '                sql.Append(" and ")
    '            End If
    '            If sql.ToString.EndsWith(" and ") Then sql.Replace(sql.ToString, sql.ToString.Substring(0, sql.Length - 5))
    '        Next i
    '        Return sql.ToString
    '    Finally
    '        sql = Nothing
    '    End Try
    'End Function

    Public Sub Conectar(ByVal server As Server)
        Try

            ' Dim dscon As New DsConfig
            ' dscon.ReadXml(".\zdbconfig.xml")
            ' Dim server As New server
            ' server.MakeConnection(dscon.DsConfig.Rows(0).Item("DBType"), dscon.DsConfig.Rows(0).Item("Server"), dscon.DsConfig.Rows(0).Item("DataBase"), dscon.DsConfig.Rows(0).Item("User"), ZAMBA.Crypto.Encryption.DecryptString(dscon.DsConfig.Rows(0).Item("Password"), key, iv))
            ' con2 = server.Con
            server.dispose()
            server = New server
            server.MakeConnection()
            con1 = server.Con
            server.dispose()
        Catch
        End Try
    End Sub

#Region "Guardar"
    'Private Sub Persistir()
    '    Try
    '        Dim row As DsPersist.SeletTableRow = DsSave.SeletTable.NewSeletTableRow
    '        Dim i As Int16
    '        row.Tabla = Tabla
    '        DsSave.SeletTable.Rows.Add(row)
    '        For i = 0 To columnasdevueltas.Count - 1
    '            Dim row2 As DsPersist.SelectColumnsRow = DsSave.SelectColumns.NewSelectColumnsRow
    '            row2.columna = columnasdevueltas(i)
    '            DsSave.SelectColumns.Rows.Add(row2)
    '        Next
    '        For i = 0 To ClaveUnica.Count - 1
    '            Dim row3 As DsPersist.ClavesRow = DsSave.Claves.NewClavesRow
    '            row3.Clave = ClaveUnica(i)
    '            DsSave.Claves.Rows.Add(row3)
    '        Next
    '        DsSave.AcceptChanges()
    '        Dim dlg As New Windows.Forms.OpenFileDialog
    '        dlg.AddExtension = True
    '        dlg.ShowDialog()
    '        Dim f As String = dlg.FileName
    '        DsSave.WriteXml(f)
    '    Catch
    '    End Try
    'End Sub
    Private Sub PersistirDB()
        Dim sql As String
        '  Dim Id As Int32
        Dim i As Int32
        'Dim name As String = InputBox("¿Con que nombre desea guardar la consulta?", "Zamba - Guardando...")
        sql = "Insert Into ZQueryName(id,name) values(" & Me.ID & ",'" & Name & "')"
        'con1.ExecuteNonQuery(CommandType.Text, sql)
        For i = 0 To columnasdevueltas.Count - 1
            sql = "Insert into ZQColumns values(" & Me.ID & ",'" & Tabla & "','" & columnasdevueltas(i) & "')"
            con1.ExecuteNonQuery(CommandType.Text, sql)
        Next
        For i = 0 To ClaveUnica.Count - 1
            sql = "Insert into ZQKeys values(" & Me.ID & ",'" & ClaveUnica(i) & "')"
            con1.ExecuteNonQuery(CommandType.Text, sql)
        Next
        If IO.File.Exists(".\zdbconfig.xml") Then IO.File.Create(".\zdbconfig.xml")
        Dim ds As New DsConfig
        Dim row As DsConfig.DsConfigRow = ds.DsConfig.NewDsConfigRow
        Try
            row.Consulta = Me.ID
            row.Campo = 0
            ds.Tables(0).Rows.Clear()
            ds.Tables(0).Rows.Add(row)
            ds.AcceptChanges()
            ds.WriteXml(".\zdbconfig.xml")
        Catch
        End Try
        Windows.Forms.MessageBox.Show("Consulta: " & Me.ID, "Zamba Software - Tools", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
        RaiseEvent SelectString(DataBaseAccessBusiness.UCWizard.MakeSelect(Me.ID, Me.ClaveUnica))
    End Sub
#End Region
#Region "Evento Botones"
    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Me.PanelControl.Controls.Clear()
        Dim UCWhere As New UCWhere(UcTable.AllColumns)
        ' AddHandler UCWhere.AddWhereColumn, AddressOf CargarClave
        UcTable.Dispose()
        Me.PanelControl.Controls.Add(UCWhere)
        Label1.Text = "Elija la/s columna/s clave"
        Me.btnGo.Enabled = False
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEnd.Click
        Me.PersistirDB()
        Try
            Me.Visible = False
            Me.Dispose()
            Me.ParentForm.Visible = False
            Me.ParentForm.Dispose()
        Catch
        End Try
    End Sub
    Private Sub btnCancell_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancell.Click
        If Windows.Forms.MessageBox.Show("¿Esta seguro que desea cancelar?", "Zamba Software", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Me.Dispose()
        End If
    End Sub
#End Region
End Class
