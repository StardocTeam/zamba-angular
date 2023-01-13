Imports Zamba.Core
Public Class FrmBulkInsert
    Inherits Zamba.AppBlock.ZForm

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
    Friend WithEvents btnFile As Zamba.AppBlock.ZButton
    Friend WithEvents btnaceptar As Zamba.AppBlock.ZButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtchar As System.Windows.Forms.TextBox
    Friend WithEvents txtfile As System.Windows.Forms.TextBox
    Friend WithEvents BtnCancelar As Zamba.AppBlock.ZButton
    Friend WithEvents lblstatus As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmBulkInsert))
        Me.txtchar = New System.Windows.Forms.TextBox
        Me.txtfile = New System.Windows.Forms.TextBox
        Me.btnFile = New Zamba.AppBlock.ZButton
        Me.btnaceptar = New Zamba.AppBlock.ZButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.BtnCancelar = New Zamba.AppBlock.ZButton
        Me.lblstatus = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'txtchar
        '
        Me.txtchar.BackColor = System.Drawing.Color.White
        Me.txtchar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtchar.Location = New System.Drawing.Point(53, 37)
        Me.txtchar.Name = "txtchar"
        Me.txtchar.Size = New System.Drawing.Size(27, 21)
        Me.txtchar.TabIndex = 0
        '
        'txtfile
        '
        Me.txtfile.BackColor = System.Drawing.Color.White
        Me.txtfile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtfile.Location = New System.Drawing.Point(53, 7)
        Me.txtfile.Name = "txtfile"
        Me.txtfile.Size = New System.Drawing.Size(264, 21)
        Me.txtfile.TabIndex = 1
        '
        'btnFile
        '
        Me.btnFile.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnFile.Location = New System.Drawing.Point(320, 7)
        Me.btnFile.Name = "btnFile"
        Me.btnFile.Size = New System.Drawing.Size(80, 21)
        Me.btnFile.TabIndex = 2
        Me.btnFile.Text = "Buscar"
        '
        'btnaceptar
        '
        Me.btnaceptar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnaceptar.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnaceptar.Location = New System.Drawing.Point(236, 134)
        Me.btnaceptar.Name = "btnaceptar"
        Me.btnaceptar.Size = New System.Drawing.Size(80, 25)
        Me.btnaceptar.TabIndex = 4
        Me.btnaceptar.Text = "Aceptar"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(7, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 17)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Archivo"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(0, 37)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 17)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Delimitador"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnCancelar
        '
        Me.BtnCancelar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnCancelar.DialogResult = System.Windows.Forms.DialogResult.None
        Me.BtnCancelar.Location = New System.Drawing.Point(323, 134)
        Me.BtnCancelar.Name = "BtnCancelar"
        Me.BtnCancelar.Size = New System.Drawing.Size(80, 25)
        Me.BtnCancelar.TabIndex = 7
        Me.BtnCancelar.Text = "Cancelar"
        '
        'lblstatus
        '
        Me.lblstatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblstatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblstatus.Location = New System.Drawing.Point(53, 67)
        Me.lblstatus.Name = "lblstatus"
        Me.lblstatus.Size = New System.Drawing.Size(347, 38)
        Me.lblstatus.TabIndex = 8
        Me.lblstatus.Text = "Inactivo"
        Me.lblstatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(7, 67)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 23)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Mensaje"
        '
        'FrmBulkInsert
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(406, 161)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblstatus)
        Me.Controls.Add(Me.BtnCancelar)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnaceptar)
        Me.Controls.Add(Me.btnFile)
        Me.Controls.Add(Me.txtfile)
        Me.Controls.Add(Me.txtchar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(492, 250)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(412, 185)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(412, 185)
        Me.Name = "FrmBulkInsert"
        Me.Text = "Inserción masiva"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region "New"
    Public Sub New(ByVal _indexid As Int32)
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()
        Me.IndexId = _indexid
        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
    End Sub
#End Region
    Public Event RefillGrid()
    Dim IndexId As Int32
    Dim t1 As Threading.Thread
    Private Sub btnFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFile.Click
        Dim dlg As New OpenFileDialog
        dlg.ShowDialog()
        txtfile.Text = dlg.FileName
    End Sub
    Private Sub btnaceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaceptar.Click
        Try
            lblstatus.Text = "Validando configuración"
            Me.Cursor = Cursors.WaitCursor
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
        cr.BulkInsertSustitucionTable(".\tempfileSLST.txt", txtchar.Text.Trim, Me.IndexId)
        cr.Dispose()
        lblstatus.Text = "Proceso Finalizado"
        Try
            IO.File.Delete(".\tempfileSLST.txt")
            RaiseEvent RefillGrid()
        Catch
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
#Region "Validación"
    Private Sub Validar()
        If txtfile.Text.Trim = "" Then
            Throw New Exception("Complete el archivo")
        End If
        If IO.File.Exists(txtfile.Text.Trim) = False Then
            Throw New Exception("El archivo no existe")
        End If
        If txtchar.Text.Trim = "" Then
            Throw New Exception("Complete el separador de campos")
        End If

        lblstatus.Text = "Validando Datos..."
        Me.ValidarDatos()
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
            campos = sr.ReadLine.Split(CType(txtchar.Text.Trim, Char))
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
            If campos(1).Trim.Length > 60 Then
                errores.Add(campos(1))
                campos(1) = campos(1).Substring(0, 59)
            End If
            If campos(1).Trim = "" Then
                sr.Close()
                sw.Close()
                Throw New Exception("No puede haber códigos sin descripción. En la linea " & i.ToString & " hay un codigo sin descripción")
            End If
            campos(1) = campos(1).Replace("'", "").Replace(Chr(34), "")
            sw.WriteLine(campos(0).Trim & txtchar.Text & campos(1)) 'lo escribo truncando la longitud
        End While
        MessageBox.Show("Se agregarán " & i.ToString & " codigos", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        If errores.Count > 0 Then
            Dim descripcion As String = ""
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
    Private Sub BtnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancelar.Click
        Try
            If Not IsNothing(t1) Then
                t1.Abort()
            End If
        Catch ex As Exception
            lblstatus.Text = "Proceso cancelado por el usuario"
            MessageBox.Show("Proceso cancelado por el usuario", "", _
                            MessageBoxButtons.OK, _
                            MessageBoxIcon.Exclamation)
        Finally
            Me.Close()
        End Try
    End Sub
End Class
