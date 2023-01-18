Imports Zamba.Core
Imports Zamba.Servers

<Ipreprocess.PreProcessName("Agregar Caja, Lote y Sector"), Ipreprocess.PreProcessHelp("Agrega tres campos al principio de cada linea con el Número de Caja, el Número de Lote y el Sector")> _
Public Class ippORD
    Inherits Form
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
    Friend WithEvents cmbSector As ComboBox
    Friend WithEvents txtSepearator As TextBox
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        TextBox1 = New TextBox
        Label1 = New Label
        Button1 = New Button
        txtLote = New TextBox
        Label2 = New Label
        txtSepearator = New TextBox
        Label3 = New Label
        Label4 = New Label
        cmbSector = New ComboBox
        SuspendLayout()
        '
        'TextBox1
        '
        TextBox1.Location = New System.Drawing.Point(16, 26)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New System.Drawing.Size(160, 21)
        TextBox1.TabIndex = 0
        '
        'Label1
        '
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Label1.ForeColor = System.Drawing.Color.Black
        Label1.Location = New System.Drawing.Point(16, 9)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(168, 17)
        Label1.TabIndex = 1
        Label1.Text = "Ingrese el número de caja"
        Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Button1
        '
        Button1.DialogResult = System.Windows.Forms.DialogResult.None
        Button1.Location = New System.Drawing.Point(200, 115)
        Button1.Name = "Button1"
        Button1.Size = New System.Drawing.Size(72, 26)
        Button1.TabIndex = 2
        Button1.Text = "Aceptar"
        '
        'txtLote
        '
        txtLote.Location = New System.Drawing.Point(16, 69)
        txtLote.Name = "txtLote"
        txtLote.Size = New System.Drawing.Size(160, 21)
        txtLote.TabIndex = 3
        '
        'Label2
        '
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Label2.ForeColor = System.Drawing.Color.Black
        Label2.Location = New System.Drawing.Point(16, 52)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(152, 17)
        Label2.TabIndex = 4
        Label2.Text = "Ingrese el número de lote"
        Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSepearator
        '
        txtSepearator.Location = New System.Drawing.Point(200, 26)
        txtSepearator.Name = "txtSepearator"
        txtSepearator.Size = New System.Drawing.Size(32, 21)
        txtSepearator.TabIndex = 5
        txtSepearator.Text = "|"
        '
        'Label3
        '
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Label3.ForeColor = System.Drawing.Color.Black
        Label3.Location = New System.Drawing.Point(192, 9)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(64, 17)
        Label3.TabIndex = 6
        Label3.Text = "Separador"
        Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Label4.BackColor = System.Drawing.Color.Transparent
        Label4.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Label4.ForeColor = System.Drawing.Color.Black
        Label4.Location = New System.Drawing.Point(16, 100)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(152, 17)
        Label4.TabIndex = 8
        Label4.Text = "Elija el sector correspondiente"
        Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbSector
        '
        cmbSector.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSector.FormattingEnabled = True
        cmbSector.Location = New System.Drawing.Point(16, 120)
        cmbSector.Name = "cmbSector"
        cmbSector.Size = New System.Drawing.Size(160, 21)
        cmbSector.TabIndex = 9
        '
        'ippORD
        '
        AcceptButton = Button1
        ClientSize = New System.Drawing.Size(292, 151)
        ControlBox = False
        Controls.Add(cmbSector)
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
        'Me.Name = "ippORD"
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
    Private sector As String

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Button1.Click
        If TextBox1.Text <> String.Empty AndAlso cmbSector.Text > String.Empty AndAlso txtLote.Text <> String.Empty Then
            addCajaProcess()
        End If
    End Sub

    Private Sub addCajaProcess()
        If Not IO.File.Exists(file) Then
            MessageBox.Show("El archivo de entrada no existe", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End If

        Try
            separator = txtSepearator.Text
            caja = CInt(TextBox1.Text)
            lote = txtLote.Text
            sector = cmbSector.Text
        Catch ex As Exception
            TextBox1.Text = String.Empty
            txtLote.Text = String.Empty
            cmbSector.Text = String.Empty
            MessageBox.Show("Ocurrio un Error: " & ex.ToString, "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
            RaiseEvent PreprocessError(ex.ToString)
            Exit Sub
        End Try

        Try
            AddCaja()
        Catch ex As Exception
            MessageBox.Show("Ocurrio un error al ejecutar el preproceso. " & ex.ToString, "Zamba Preprocess", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Close()
        End Try
    End Sub

    Private Sub AddCaja()
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
                strb.Append(sector)
                strb.Append(separator)
                strb.Append(str)
                sw.WriteLine(strb.ToString.Trim)
            End While
            sr.Close()
            sw.Close()
            Dim fio As New System.IO.FileInfo(dir & "\tmp.txt")
            fio.CopyTo(file, True)
            fio.Delete()
        End If
    End Sub

    Public Function process(ByVal Files As System.Collections.ArrayList, Optional ByVal param As System.Collections.ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As System.Collections.ArrayList Implements Ipreprocess.process
        Dim i As Int32

        For i = 0 To Files.Count - 1
            processFile(Files(i), param(0), xml)
        Next
        Return Files
    End Function

#Region "XML"
    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml
        'TODO:Implementar
    End Sub

    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        'TODO:Implementar
        Return String.Empty
    End Function
#End Region

    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        Me.file = File
        getSectores()
        ShowDialog()
        Return File
    End Function

    Private Sub getSectores()
        Try
            Dim sql As String
            If Server.isOracle Then
                sql = "Select * from ILST_I100"
            Else
                sql = "Select * from ILST_I71"
            End If
            Dim ds As DataSet
            ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
            cmbSector.Items.Clear()
            For Each row As DataRow In ds.Tables(0).Rows
                cmbSector.Items.Add(row.Item(1).ToString())
            Next
            cmbSector.DisplayMember = "ITEM"
        Catch
        End Try
    End Sub
    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Agrega dos campos al principio de cada linea con el Número de Caja y el Número de Lote"
    End Function
#Region "Eventos"
    Public Event PreprocessMessage1(ByVal msg As String) Implements Ipreprocess.PreprocessMessage

    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError
#End Region

    Public Shared ReadOnly Property Id() As Integer
        Get
            Return ProcessFactory.GetProcessIDByName(Name())
        End Get
    End Property

    Public Shared ReadOnly Property Name() As String
        Get
            Return "ippORD"
        End Get
    End Property
End Class
