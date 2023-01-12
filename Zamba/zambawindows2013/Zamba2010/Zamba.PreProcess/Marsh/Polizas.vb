
Imports Zamba.Core
Imports System.IO
Imports Zamba.Servers

<Ipreprocess.PreProcessName("Agregar Caja, Lote, Sector y Tipo de Orden"), Ipreprocess.PreProcessHelp("Agrega cuatro campos al principio de cada linea con el Número de Caja, el Número de Lote, el Sector y el Tipo de Orden")> _
Public Class ippPolizas
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
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbOrden As ComboBox
    Friend WithEvents txtSepearator As TextBox
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        TextBox1 = New TextBox
        Label1 = New System.Windows.Forms.Label
        Button1 = New System.Windows.Forms.Button
        txtLote = New TextBox
        Label2 = New System.Windows.Forms.Label
        txtSepearator = New TextBox
        Label3 = New System.Windows.Forms.Label
        Label4 = New System.Windows.Forms.Label
        cmbSector = New ComboBox
        Label5 = New System.Windows.Forms.Label
        cmbOrden = New ComboBox
        SuspendLayout()
        '
        'TextBox1
        '
        TextBox1.Location = New System.Drawing.Point(16, 26)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New System.Drawing.Size(160, 20)
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
        Button1.Location = New System.Drawing.Point(200, 170)
        Button1.Name = "Button1"
        Button1.Size = New System.Drawing.Size(72, 26)
        Button1.TabIndex = 2
        Button1.Text = "Aceptar"
        '
        'txtLote
        '
        txtLote.Location = New System.Drawing.Point(16, 69)
        txtLote.Name = "txtLote"
        txtLote.Size = New System.Drawing.Size(160, 20)
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
        txtSepearator.Size = New System.Drawing.Size(32, 20)
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
        Label4.Location = New System.Drawing.Point(16, 105)
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
        cmbSector.Location = New System.Drawing.Point(16, 125)
        cmbSector.Name = "cmbSector"
        cmbSector.Size = New System.Drawing.Size(160, 21)
        cmbSector.TabIndex = 9
        '
        'Label5
        '
        Label5.BackColor = System.Drawing.Color.Transparent
        Label5.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Label5.ForeColor = System.Drawing.Color.Black
        Label5.Location = New System.Drawing.Point(16, 155)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(152, 17)
        Label5.TabIndex = 11
        Label5.Text = "Elija el tipo de orden"
        Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbOrden
        '
        cmbOrden.DropDownStyle = ComboBoxStyle.DropDownList
        cmbOrden.FormattingEnabled = True
        cmbOrden.Location = New System.Drawing.Point(16, 175)
        cmbOrden.Name = "cmbOrden"
        cmbOrden.Size = New System.Drawing.Size(160, 21)
        cmbOrden.TabIndex = 12
        '
        'ippPolizas
        '
        AcceptButton = Button1
        ClientSize = New System.Drawing.Size(292, 217)
        ControlBox = False
        Controls.Add(cmbOrden)
        Controls.Add(Label5)
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
    Private orden As String

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Button1.Click
        If TextBox1.Text <> String.Empty AndAlso IsNumeric(TextBox1.Text) AndAlso cmbSector.Text > String.Empty AndAlso txtLote.Text <> String.Empty AndAlso cmbOrden.Text > String.Empty Then
            addCajaProcess()
        End If
    End Sub

    ''' <summary>
    ''' Obtiene los valores y los guarda en las variables locales
    ''' </summary>
    ''' <remarks></remarks>
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
            orden = cmbOrden.Text
        Catch ex As Exception
            TextBox1.Text = String.Empty
            txtLote.Text = String.Empty
            cmbSector.Text = String.Empty
            cmbOrden.Text = String.Empty
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

    ''' <summary>
    ''' Guarda los valores en el txt modificado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddCaja()
        Dim fi As FileInfo = Nothing
        Dim sr As StreamReader = Nothing
        Dim sw As StreamWriter = Nothing
        Try
            fi = New FileInfo(file)
            If fi.Exists Then
                sr = New System.IO.StreamReader(fi.OpenRead)
                Dim Dir As String = Tools.EnvironmentUtil.GetTempDir("\Temp").FullName

                sw = New System.IO.StreamWriter(Dir & "\tmp.txt", False)

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
                    strb.Append(orden)
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
        Catch
            If Not IsNothing(fi) Then
                fi = Nothing
            End If
            If Not IsNothing(sr) Then
                sr.Dispose()
                sr = Nothing
            End If
            If Not IsNothing(sw) Then
                sw.Dispose()
                sw = Nothing
            End If
            GC.Collect()
        End Try
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

    ''' <summary>
    ''' Procesar el archivo
    ''' </summary>
    ''' <param name="File">Path del Archivo</param>
    ''' <param name="param"></param>
    ''' <param name="xml"></param>
    ''' <param name="Test"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        Me.file = File
        getSectores()
        getOrdenes()
        ShowDialog()
        Return File
    End Function

    ''' <summary>
    ''' Obtiene los sectores y los carga en el combo
    ''' </summary>
    ''' <remarks></remarks>
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

    ''' <summary>
    ''' Obtiene las ordenes y las carga en el combo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub getOrdenes()
        Try
            Dim sql As String
            If Server.isOracle Then
                sql = "Select * from ILST_I86"
            Else
                sql = "Select * from ILST_I71"
            End If
            Dim ds As DataSet
            ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
            cmbOrden.Items.Clear()
            For Each row As DataRow In ds.Tables(0).Rows
                cmbOrden.Items.Add(row.Item(1).ToString())
            Next
            cmbOrden.DisplayMember = "ITEM"
        Catch
        End Try
    End Sub
    ''' <summary>
    ''' La descripcion del preproceso
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Agrega cuatro campos al principio de cada linea con Número de Caja, Número de Lote, Sector y Tipo de Orden"
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

    ''' <summary>
    ''' Nombre del Preproceso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property Name() As String
        Get
            Return "ippPolizas"
        End Get
    End Property
End Class
