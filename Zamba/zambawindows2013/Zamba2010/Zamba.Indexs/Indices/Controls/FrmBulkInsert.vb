Imports Zamba.Core
Public Class FrmBulkInsert
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
    Friend WithEvents btnFile As ZButton
    Friend WithEvents btnaceptar As ZButton
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents txtchar As TextBox
    Friend WithEvents txtfile As TextBox
    Friend WithEvents BtnCancelar As ZButton
    Friend WithEvents lblstatus As ZLabel
    Friend WithEvents Label3 As ZLabel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(FrmBulkInsert))
        txtchar = New TextBox()
        txtfile = New TextBox()
        btnFile = New ZButton()
        btnaceptar = New ZButton()
        Label1 = New ZLabel()
        Label2 = New ZLabel()
        BtnCancelar = New ZButton()
        lblstatus = New ZLabel()
        Label3 = New ZLabel()
        SuspendLayout()
        '
        'txtchar
        '
        txtchar.BackColor = Color.White
        txtchar.BorderStyle = BorderStyle.FixedSingle
        txtchar.Location = New Point(151, 39)
        txtchar.Name = "txtchar"
        txtchar.Size = New Size(38, 23)
        txtchar.TabIndex = 0
        '
        'txtfile
        '
        txtfile.BackColor = Color.White
        txtfile.BorderStyle = BorderStyle.FixedSingle
        txtfile.Location = New Point(151, 10)
        txtfile.Name = "txtfile"
        txtfile.Size = New Size(472, 23)
        txtfile.TabIndex = 1
        '
        'btnFile
        '
        btnFile.BackColor = Color.FromArgb(0, 157, 224)
        btnFile.FlatStyle = FlatStyle.Flat
        btnFile.ForeColor = Color.White
        btnFile.Location = New Point(631, 9)
        btnFile.Name = "btnFile"
        btnFile.Size = New Size(112, 24)
        btnFile.TabIndex = 2
        btnFile.Text = "Buscar"
        btnFile.UseVisualStyleBackColor = False
        '
        'btnaceptar
        '
        btnaceptar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnaceptar.BackColor = Color.FromArgb(0, 157, 224)
        btnaceptar.FlatStyle = FlatStyle.Flat
        btnaceptar.ForeColor = Color.White
        btnaceptar.Location = New Point(271, 150)
        btnaceptar.Name = "btnaceptar"
        btnaceptar.Size = New Size(112, 29)
        btnaceptar.TabIndex = 4
        btnaceptar.Text = "Aceptar"
        btnaceptar.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Label1.BackColor = Color.White
        Label1.Font = New Font("Verdana", 9.75!)
        Label1.FontSize = 9.75!
        Label1.ForeColor = Color.FromArgb(76, 76, 76)
        Label1.Location = New Point(10, 11)
        Label1.Name = "Label1"
        Label1.Size = New Size(135, 19)
        Label1.TabIndex = 5
        Label1.Text = "Archivo"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Label2.BackColor = Color.White
        Label2.Font = New Font("Verdana", 9.75!)
        Label2.FontSize = 9.75!
        Label2.ForeColor = Color.FromArgb(76, 76, 76)
        Label2.Location = New Point(10, 39)
        Label2.Name = "Label2"
        Label2.Size = New Size(135, 20)
        Label2.TabIndex = 6
        Label2.Text = "Delimitador"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        '
        'BtnCancelar
        '
        BtnCancelar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        BtnCancelar.BackColor = Color.FromArgb(0, 157, 224)
        BtnCancelar.FlatStyle = FlatStyle.Flat
        BtnCancelar.ForeColor = Color.White
        BtnCancelar.Location = New Point(389, 150)
        BtnCancelar.Name = "BtnCancelar"
        BtnCancelar.Size = New Size(112, 29)
        BtnCancelar.TabIndex = 7
        BtnCancelar.Text = "Cancelar"
        BtnCancelar.UseVisualStyleBackColor = False
        '
        'lblstatus
        '
        lblstatus.BackColor = Color.Gainsboro
        lblstatus.Font = New Font("Verdana", 9.75!)
        lblstatus.FontSize = 9.75!
        lblstatus.ForeColor = Color.FromArgb(76, 76, 76)
        lblstatus.Location = New Point(151, 68)
        lblstatus.Name = "lblstatus"
        lblstatus.Size = New Size(472, 60)
        lblstatus.TabIndex = 8
        lblstatus.Text = "Inactivo"
        lblstatus.TextAlign = ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Label3.BackColor = Color.Transparent
        Label3.Font = New Font("Verdana", 9.75!)
        Label3.FontSize = 9.75!
        Label3.ForeColor = Color.FromArgb(76, 76, 76)
        Label3.Location = New Point(10, 68)
        Label3.Name = "Label3"
        Label3.Size = New Size(132, 26)
        Label3.TabIndex = 9
        Label3.Text = "Mensaje"
        Label3.TextAlign = ContentAlignment.MiddleLeft
        '
        'FrmBulkInsert
        '
        AutoScaleBaseSize = New Size(7, 16)
        BackColor = Color.White
        ClientSize = New Size(755, 184)
        Controls.Add(Label3)
        Controls.Add(lblstatus)
        Controls.Add(BtnCancelar)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(btnaceptar)
        Controls.Add(btnFile)
        Controls.Add(txtfile)
        Controls.Add(txtchar)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(0, 100)
        Name = "FrmBulkInsert"
        StartPosition = FormStartPosition.CenterParent
        Text = "Inserción masiva"
        ShowInTaskbar = False
        ResumeLayout(False)
        PerformLayout()

    End Sub

#End Region

#Region "New"
    Public Sub New(ByVal _indexid As Int32)
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()
        IndexId = _indexid
        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
    End Sub
#End Region
    Public Event RefillGrid()
    Dim IndexId As Int32
    Dim t1 As Threading.Thread
    Private Sub btnFile_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnFile.Click
        Dim dlg As New OpenFileDialog
        dlg.ShowDialog()
        txtfile.Text = dlg.FileName
    End Sub
    Private Sub btnaceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnaceptar.Click
        Try
            lblstatus.Text = "Validando configuración"
            Cursor = Cursors.WaitCursor
            Validar()
            lblstatus.Text = "Procesando..."
            t1 = New Threading.Thread(AddressOf ProcessBulkFile)
            t1.Start()
        Catch ex As Exception
            lblstatus.Text = ex.Message
            'MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub ProcessBulkFile()
        Dim cr As New CreateTablesBusiness
        cr.BulkInsertSustitucionTable(".\tempfileSLST.txt", txtchar.Text.Trim, IndexId)
        cr.Dispose()
        Try
            IO.File.Delete(".\tempfileSLST.txt")
            RaiseEvent RefillGrid()
        Catch

        End Try
    End Sub
#Region "Validación"
    Private Sub Validar()
        If txtfile.Text.Trim = String.Empty Then
            Throw New Exception("Complete el archivo")
        End If
        If IO.File.Exists(txtfile.Text.Trim) = False Then
            Throw New Exception("El archivo no existe")
        End If
        If txtchar.Text.Trim = String.Empty Then
            Throw New Exception("Complete el separador de campos")
        End If

        lblstatus.Text = "Validando Datos..."
        ValidarDatos()
        lblstatus.Text = "Validación correcta"

    End Sub
    Private Sub ValidarDatos()
        Dim sr As New IO.StreamReader(txtfile.Text.Trim)
        Dim sw As New IO.StreamWriter(".\tempfileSLST.txt", False)
        Dim i As Int64
        Dim campos() As String
        Dim errores As New ArrayList
        While sr.Peek <> -1
            i += 1
            campos = sr.ReadLine.Split(txtchar.Text.Trim)
            If campos.Length <> 2 Then
                sw.Close()
                sr.Close()
                Throw New Exception("Debe haber solo dos campos, código y descripción, en la linea " & i.ToString & " hay " & campos.Length & " campo/s")
            End If
            If Not IsNumeric(campos(0).Trim) Then
                sr.Close()
                sw.Close()
                Throw New Exception("El primer campo debe ser numerico. En la linea " & i.ToString & " el primer campo NO es numerico")
            End If
            If campos(1).Trim.Length > 250 Then
                errores.Add(campos(1))
                campos(1) = campos(1).Substring(0, 249)
            End If
            If campos(1).Trim = String.Empty Then
                sr.Close()
                sw.Close()
                Throw New Exception("No puede haber códigos sin descripción. En la linea " & i.ToString & " hay un codigo sin descripción")
            End If
            campos(1) = campos(1).Replace("'", String.Empty).Replace(Chr(34), String.Empty)
            sw.WriteLine(campos(0).Trim & txtchar.Text & campos(1)) 'lo escribo truncando la longitud
        End While
        MessageBox.Show("Se agregarán " & i.ToString & " codigos", String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
        If errores.Count > 0 Then
            Dim descripcion As String = String.Empty
            Dim j As Int32
            For j = 0 To errores.Count - 1
                descripcion &= errores(j).ToString() & ", "
            Next
            descripcion = descripcion.Trim.TrimEnd(Char.Parse(","))
            MessageBox.Show("Las siguientes descripciones se truncarán a 60 caracteres. " & descripcion)
        End If
        sr.Close()
        sw.Close()
    End Sub

#End Region
    Private Sub BtnCancelar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnCancelar.Click
        Try
            If Not IsNothing(t1) Then
                t1.Abort()
            End If
        Catch ex As Exception
            lblstatus.Text = "Proceso cancelado por el usuario"
            MessageBox.Show("Proceso cancelado por el usuario", String.Empty,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation)
        Finally
            Close()
        End Try
    End Sub
End Class
