Imports Zamba.Core
<Ipreprocess.PreProcessName("Agregar Caja y Lote"), Ipreprocess.PreProcessHelp("Agrega dos campos al principio de cada linea con el Número de Caja y el Número de Lote")> _
Public Class ippPPNumBox
    Inherits form
    'System.Windows.Forms.Form
    Implements Ipreprocess

#Region " Código generado por el Diseñador de Windows Forms "
    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
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
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents txtLote As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents txtSepearator As TextBox
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        TextBox1 = New TextBox()
        Label1 = New System.Windows.Forms.Label()
        Button1 = New System.Windows.Forms.Button()
        txtLote = New TextBox()
        Label2 = New System.Windows.Forms.Label()
        txtSepearator = New TextBox()
        Label3 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        TextBox2 = New TextBox()
        SuspendLayout()
        '
        'TextBox1
        '
        TextBox1.Location = New System.Drawing.Point(16, 24)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New System.Drawing.Size(160, 20)
        TextBox1.TabIndex = 0
        '
        'Label1
        '
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Label1.ForeColor = System.Drawing.Color.Black
        Label1.Location = New System.Drawing.Point(16, 8)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(168, 16)
        Label1.TabIndex = 1
        Label1.Text = "Ingrese el número de caja"
        Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Button1
        '
        Button1.Location = New System.Drawing.Point(200, 64)
        Button1.Name = "Button1"
        Button1.Size = New System.Drawing.Size(72, 24)
        Button1.TabIndex = 2
        Button1.Text = "Aceptar"
        '
        'txtLote
        '
        txtLote.Location = New System.Drawing.Point(16, 64)
        txtLote.Name = "txtLote"
        txtLote.Size = New System.Drawing.Size(160, 20)
        txtLote.TabIndex = 3
        '
        'Label2
        '
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Label2.ForeColor = System.Drawing.Color.Black
        Label2.Location = New System.Drawing.Point(16, 48)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(152, 16)
        Label2.TabIndex = 4
        Label2.Text = "Ingrese el número de lote"
        Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSepearator
        '
        txtSepearator.Location = New System.Drawing.Point(200, 24)
        txtSepearator.Name = "txtSepearator"
        txtSepearator.Size = New System.Drawing.Size(32, 20)
        txtSepearator.TabIndex = 5
        txtSepearator.Text = "|"
        '
        'Label3
        '
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Label3.ForeColor = System.Drawing.Color.Black
        Label3.Location = New System.Drawing.Point(192, 8)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(64, 16)
        Label3.TabIndex = 6
        Label3.Text = "Separador"
        Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(16, 101)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(140, 13)
        Label4.TabIndex = 7
        Label4.Text = "Ingrese el nombre del Indice"
        '
        'TextBox2
        '
        TextBox2.Location = New System.Drawing.Point(16, 117)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New System.Drawing.Size(157, 20)
        TextBox2.TabIndex = 8
        '
        'ippPPNumBox
        '
        AcceptButton = Button1
        ClientSize = New System.Drawing.Size(342, 205)
        ControlBox = False
        Controls.Add(TextBox2)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(txtSepearator)
        Controls.Add(Label2)
        Controls.Add(txtLote)
        Controls.Add(TextBox1)
        Controls.Add(Button1)
        Controls.Add(Label1)
        MaximizeBox = False
        MinimizeBox = False
        Name = "ippPPNumBox"
        Padding = New System.Windows.Forms.Padding(2)
        ShowInTaskbar = False
        Text = "Preproceso de importación"
        TopMost = True
        ResumeLayout(False)
        PerformLayout()

    End Sub
#End Region

    Private file As String
    Private separator As String
    Private caja As Int32
    Private lote As String


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Button1.Click
        If TextBox1.Text <> String.Empty AndAlso txtLote.Text <> String.Empty Then
            addCajaProcess()
        End If
    End Sub

    Private Function addCajaProcess()
        If Not IO.File.Exists(file) Then
            MessageBox.Show("El archivo de entrada no existe", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End If

        Try
            separator = txtSepearator.Text
            caja = CInt(TextBox1.Text)
            lote = txtLote.Text
        Catch ex As Exception
            TextBox1.Text = String.Empty
            txtLote.Text = String.Empty
            MessageBox.Show("Ocurrio un Error: " & ex.ToString, "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
            RaiseEvent PreprocessError(ex.ToString)
            Exit Function
        End Try

        Try
            AddCaja()
        Catch ex As Exception
            MessageBox.Show("Ocurrio un error al ejecutar el preproceso. " & ex.ToString, "Zamba Preprocess", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Close()
        End Try
    End Function

    Private Function AddCaja()
        Dim fi As New System.IO.FileInfo(file)
        If fi.Exists Then
            Dim sr As New System.IO.StreamReader(fi.OpenRead)
            Dim Dir As String = Tools.EnvironmentUtil.GetTempDir("\Temp").FullName

            Dim sw As New System.IO.StreamWriter(Dir & "\tmp.txt", False)

            sw.AutoFlush = True

            While sr.Peek <> -1
                Dim str As String = sr.ReadLine
                Dim strb As New System.Text.StringBuilder

                strb.Append(caja.ToString)
                strb.Append(separator)
                strb.Append(lote)
                strb.Append(separator)
                strb.Append(str)
                sw.WriteLine(strb.ToString.Trim)
            End While
            sr.Close()
            sw.Close()
            Dim fio As New System.IO.FileInfo(Dir & "\tmp.txt")
            fio.CopyTo(file, True)
            fio.Delete()
        End If
    End Function

    Public Function process(ByVal Files As System.Collections.ArrayList, Optional ByVal param As System.Collections.ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As System.Collections.ArrayList Implements Ipreprocess.process
        Dim i As Int32

        For i = 0 To Files.Count - 1
            processFile(Files(i), param(0), xml)
        Next
        Return Files
    End Function

#Region "XML"
    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml

    End Sub

    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml

    End Function
#End Region

    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        Me.file = File
        ShowDialog()
        Return File
    End Function
    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Agrega dos campos al principio de cada linea con el Número de Caja y el Número de Lote"
    End Function
#Region "Eventos"
    Public Event PreprocessMessage1(ByVal msg As String) Implements Ipreprocess.PreprocessMessage

    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub txtLote_TextChanged(sender As Object, e As EventArgs) Handles txtLote.TextChanged

    End Sub
#End Region

End Class
