Imports System.Drawing
Imports ZAMBA.Servers
Imports Zamba.Core
Imports System.IO

<Ipreprocess.PreProcessName("Cobranzas"), Ipreprocess.PreProcessHelp("Agrega los campos Nro de Caja, Nro de Lote, Sociedad, Sucursal, Tipo de Asiento, moneda en el orden especificado")> _
Public Class ippCobranzas
    Inherits Form
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
    Friend WithEvents ZLabel3 As Label
    Friend WithEvents ZLabel2 As Label
    Friend WithEvents ZLabel1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtSeparator As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtbatch As TextBox
    Friend WithEvents txtbox As TextBox
    Friend WithEvents btnaceptar As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents ZLabel4 As Label
    Friend WithEvents cbomoneda As ComboBox
    Friend WithEvents cbosoc As ComboBox
    Friend WithEvents cbosuc As ComboBox
    Friend WithEvents cboasiento As ComboBox
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ippCobranzas))
        ZLabel3 = New System.Windows.Forms.Label
        ZLabel2 = New System.Windows.Forms.Label
        ZLabel1 = New System.Windows.Forms.Label
        Label3 = New System.Windows.Forms.Label
        txtSeparator = New TextBox
        Label2 = New System.Windows.Forms.Label
        txtbatch = New TextBox
        txtbox = New TextBox
        btnaceptar = New System.Windows.Forms.Button
        Label1 = New System.Windows.Forms.Label
        cbomoneda = New ComboBox
        ZLabel4 = New System.Windows.Forms.Label
        cbosoc = New ComboBox
        cbosuc = New ComboBox
        cboasiento = New ComboBox
        SuspendLayout()
        '
        'ZLabel3
        '
        ZLabel3.BackColor = System.Drawing.Color.Transparent
        ZLabel3.ForeColor = System.Drawing.Color.Black
        ZLabel3.Location = New System.Drawing.Point(12, 88)
        ZLabel3.Name = "ZLabel3"
        ZLabel3.Size = New System.Drawing.Size(152, 16)
        ZLabel3.TabIndex = 32
        ZLabel3.Text = "Ingrese el Tipo de Asiento"
        ZLabel3.TextAlign = ContentAlignment.MiddleLeft
        '
        'ZLabel2
        '
        ZLabel2.BackColor = System.Drawing.Color.Transparent
        ZLabel2.ForeColor = System.Drawing.Color.Black
        ZLabel2.Location = New System.Drawing.Point(12, 61)
        ZLabel2.Name = "ZLabel2"
        ZLabel2.Size = New System.Drawing.Size(152, 16)
        ZLabel2.TabIndex = 30
        ZLabel2.Text = "Ingrese Moneda"
        ZLabel2.TextAlign = ContentAlignment.MiddleLeft
        '
        'ZLabel1
        '
        ZLabel1.BackColor = System.Drawing.Color.Transparent
        ZLabel1.ForeColor = System.Drawing.Color.Black
        ZLabel1.Location = New System.Drawing.Point(12, 5)
        ZLabel1.Name = "ZLabel1"
        ZLabel1.Size = New System.Drawing.Size(152, 16)
        ZLabel1.TabIndex = 28
        ZLabel1.Text = "Ingrese el Sociedad"
        ZLabel1.TextAlign = ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.ForeColor = System.Drawing.Color.Black
        Label3.Location = New System.Drawing.Point(12, 191)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(64, 16)
        Label3.TabIndex = 26
        Label3.Text = "Separador"
        Label3.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtSeparator
        '
        txtSeparator.Location = New System.Drawing.Point(82, 190)
        txtSeparator.Name = "txtSeparator"
        txtSeparator.Size = New System.Drawing.Size(32, 20)
        txtSeparator.TabIndex = 25
        txtSeparator.Text = "|"
        '
        'Label2
        '
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.ForeColor = System.Drawing.Color.Black
        Label2.Location = New System.Drawing.Point(12, 125)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(152, 16)
        Label2.TabIndex = 24
        Label2.Text = "Ingrese el número de lote"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        '
        'txtbatch
        '
        txtbatch.Location = New System.Drawing.Point(196, 125)
        txtbatch.Name = "txtbatch"
        txtbatch.Size = New System.Drawing.Size(160, 20)
        txtbatch.TabIndex = 5
        '
        'txtbox
        '
        txtbox.Location = New System.Drawing.Point(196, 154)
        txtbox.Name = "txtbox"
        txtbox.Size = New System.Drawing.Size(160, 20)
        txtbox.TabIndex = 4
        '
        'btnaceptar
        '
        btnaceptar.Location = New System.Drawing.Point(133, 230)
        btnaceptar.Name = "btnaceptar"
        btnaceptar.Size = New System.Drawing.Size(72, 24)
        btnaceptar.TabIndex = 22
        btnaceptar.Text = "Aceptar"
        '
        'Label1
        '
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.ForeColor = System.Drawing.Color.Black
        Label1.Location = New System.Drawing.Point(12, 154)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(168, 16)
        Label1.TabIndex = 21
        Label1.Text = "Ingrese el número de caja"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        '
        'cbomoneda
        '
        cbomoneda.Location = New System.Drawing.Point(196, 61)
        cbomoneda.Name = "cbomoneda"
        cbomoneda.Size = New System.Drawing.Size(160, 21)
        cbomoneda.TabIndex = 2
        '
        'ZLabel4
        '
        ZLabel4.BackColor = System.Drawing.Color.Transparent
        ZLabel4.ForeColor = System.Drawing.Color.Black
        ZLabel4.Location = New System.Drawing.Point(12, 33)
        ZLabel4.Name = "ZLabel4"
        ZLabel4.Size = New System.Drawing.Size(152, 16)
        ZLabel4.TabIndex = 34
        ZLabel4.Text = "Ingrese Sucursal"
        ZLabel4.TextAlign = ContentAlignment.MiddleLeft
        '
        'cbosoc
        '
        cbosoc.Location = New System.Drawing.Point(196, 4)
        cbosoc.Name = "cbosoc"
        cbosoc.Size = New System.Drawing.Size(160, 21)
        cbosoc.TabIndex = 0
        '
        'cbosuc
        '
        cbosuc.Location = New System.Drawing.Point(196, 33)
        cbosuc.Name = "cbosuc"
        cbosuc.Size = New System.Drawing.Size(160, 21)
        cbosuc.TabIndex = 1
        '
        'cboasiento
        '
        cboasiento.Location = New System.Drawing.Point(196, 88)
        cboasiento.Name = "cboasiento"
        cboasiento.Size = New System.Drawing.Size(160, 21)
        cboasiento.TabIndex = 3
        '
        'Cobranzas
        '
        AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        ClientSize = New System.Drawing.Size(376, 266)
        Controls.Add(cboasiento)
        Controls.Add(cbosuc)
        Controls.Add(cbosoc)
        Controls.Add(ZLabel4)
        Controls.Add(cbomoneda)
        Controls.Add(ZLabel3)
        Controls.Add(ZLabel2)
        Controls.Add(ZLabel1)
        Controls.Add(Label3)
        Controls.Add(txtSeparator)
        Controls.Add(Label2)
        Controls.Add(txtbatch)
        Controls.Add(txtbox)
        Controls.Add(btnaceptar)
        Controls.Add(Label1)
        Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        MaximizeBox = False
        Name = "Cobranzas"
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Text = "Cobranzas"
        ResumeLayout(False)
        PerformLayout()

    End Sub

#End Region

    Dim file As String


    ''' <summary>
    ''' Valida todos los campos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnaceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnaceptar.Click
        Try
            If txtSeparator.Text.Trim = "" Then
                MessageBox.Show("Ingrese un separador", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            If txtbatch.Text.Trim = "" Then
                If MessageBox.Show("El Nro de Lote está vacio, ¿desea continuar?", "Zamba", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.No Then
                    Exit Sub
                End If
            End If
            If txtbox.Text.Trim = "" Then
                If MessageBox.Show("El Nro de Caja está vacio, ¿desea continuar?", "Zamba", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.No Then
                    txtbox.Text = "0"
                    Exit Sub
                End If
            End If
            If cbosoc.Text.Trim = "" Then
                If MessageBox.Show("El campo sociedad está vacio, ¿desea continuar?", "Zamba", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.No Then
                    Exit Sub
                End If
            End If
            If cbosuc.Text.Trim = "" Then
                If MessageBox.Show("El campo sucursal está vacio, ¿desea continuar?", "Zamba", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.No Then
                    Exit Sub
                End If
            End If
            If cbomoneda.Text.Trim = "" Then
                If MessageBox.Show("El campo moneda está vacio, ¿desea continuar?", "Zamba", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.No Then
                    Exit Sub
                End If
            End If
            If cboasiento.Text.Trim = "" Then
                If MessageBox.Show("El campo Tipo de asiento está vacio, ¿desea continuar?", "Zamba", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.No Then
                    Exit Sub
                End If
            End If
            'Si se validaron todos los valores, lo guarda en el .txt
            AddParams(txtbox.Text.Trim, txtbatch.Text.Trim, cbosoc.Text.Trim, cbosuc.Text, cboasiento.Text, cbomoneda.Text, txtSeparator.Text.Trim)
        Catch ex As Exception
            MessageBox.Show("Ocurrio un error al ejecutar el preproceso. " & ex.ToString, "Zamba Preprocess", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Guardo los valores en el txt
    ''' </summary>
    ''' <param name="caja">Valor de la caja</param>
    ''' <param name="lote">Valor del lote</param>
    ''' <param name="sociedad">Valor de la Sociedad</param>
    ''' <param name="sucursal">Valor de la Sucursal</param>
    ''' <param name="tasiento"></param>
    ''' <param name="moneda">Valor de la moneda</param>
    ''' <param name="separador"></param>
    ''' <remarks></remarks>
    Private Sub AddParams(ByVal caja As String, ByVal lote As String, ByVal sociedad As String, ByVal sucursal As String, ByVal tasiento As String, ByVal moneda As String, ByVal separador As Char)
        Dim fi As FileInfo = Nothing
        Dim fio As FileInfo = Nothing
        Dim sr As StreamReader = Nothing
        Dim sw As StreamWriter = Nothing

        Try
            fi = New FileInfo(file)
            If fi.Exists Then
                sr = New System.IO.StreamReader(fi.OpenRead)
                Dim Dir As String = Tools.EnvironmentUtil.GetTempDir("\Temp").FullName

                sw = New System.IO.StreamWriter(Dir & "\tmp.txt", False)

                sw.AutoFlush = True

                'Por cada linea del txt le agrego los valores obtenidos de los controles
                While sr.Peek <> -1
                    Dim str As String = sr.ReadLine
                    Dim strb As New System.Text.StringBuilder

                    strb.Append(caja)
                    strb.Append(separador)
                    strb.Append(lote)
                    strb.Append(separador)
                    strb.Append(sociedad)
                    strb.Append(separador)
                    strb.Append(sucursal)
                    strb.Append(separador)
                    strb.Append(tasiento)
                    strb.Append(separador)
                    strb.Append(moneda)
                    strb.Append(separador)
                    strb.Append(str)
                    sw.WriteLine(strb.ToString().Trim())

                    strb = Nothing
                End While

                sr.Close()
                sw.Close()
                'Copio el archivo

                fio = New FileInfo(dir & "\tmp.txt")
                fio.CopyTo(file, True)
                fio.Delete()
            End If
        Catch ex As Exception
            MessageBox.Show("Ocurrio un error al ejecutar el preproceso. " & ex.ToString, "Zamba Preprocess", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(fi) Then
                fi = Nothing
            End If
            If Not IsNothing(fio) Then
                fio = Nothing
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
            Close()
        End Try
    End Sub

#Region "XML"
    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        Return ""
    End Function
    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml

    End Sub
#End Region
#Region "HELP"
    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Agrega los campos Nro de Caja, Nro de Lote, Sociedad, Sucursal, Tipo de Asiento, moneda en el orden especificado"
    End Function
#End Region
#Region "Eventos"
    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError
    Public Event PreprocessMessage1(ByVal msg As String) Implements Ipreprocess.PreprocessMessage
#End Region

    Public Function process(ByVal Files As System.Collections.ArrayList, Optional ByVal param As System.Collections.ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As System.Collections.ArrayList Implements Ipreprocess.process
        Dim i As Int32

        For i = 0 To Files.Count - 1
            processFile(Files(i), param(0), xml)
        Next
        Return Files
    End Function
    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        Me.file = File
        ShowDialog()
    End Function


    Private Sub Cobranzas_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        FillCombos()
    End Sub
    ''' <summary>
    ''' LLena los combos con los valores de la base
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillCombos()
        Dim ds As DataSet
        Dim sql As String
        Dim i As Int32
        Try
            sql = "Select Item from ILST_I58 order by Item"

            ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
            'Cargo la sociedad
            For Each row As DataRow In ds.Tables(0).Rows
                cbosoc.Items.Add(row.Item(0).ToString())
            Next
            cbosoc.DisplayMember = "ITEM"
            'Cargo las sucursales
            sql = "Select Item from ILST_I79 order by Item"
            ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
            'Cargo la sociedad
            For Each row As DataRow In ds.Tables(0).Rows
                cbosuc.Items.Add(row.Item(0).ToString())
            Next
            cbosuc.DisplayMember = "ITEM"
            'Cargo los tipos de asientos
            sql = "Select Item from ILST_I81 order by Item"
            ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
            For Each row As DataRow In ds.Tables(0).Rows
                cboasiento.Items.Add(row.Item(0).ToString())
            Next
            cboasiento.DisplayMember = "ITEM"
        Catch ex As Exception
            MessageBox.Show("Ocurrio un error al ejecutar el preproceso. " & ex.ToString, "Zamba Preprocess", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Try
            'Cargo monedas
            sql = "Select Item from ILST_I80 order by Item"
            ds = Server.Con.ExecuteDataset(Server.Con.ConString, sql)
            For i = 0 To ds.Tables(0).Rows.Count - 1
                cbomoneda.Items.Add(ds.Tables(0).Rows(i).Item(0))
            Next
        Catch ex As Exception
            cbomoneda.Items.Add("Dolares")
            cbomoneda.Items.Add("Dolares / Otras Monedas")
            cbomoneda.Items.Add("Euros")
            cbomoneda.Items.Add("Pesos")
            cbomoneda.Items.Add("Otras Monedas")
        End Try
    End Sub
End Class
