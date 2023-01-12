Imports Zamba.Core
<Ipreprocess.PreProcessName("Completar impuestos"), Ipreprocess.PreProcessHelp("Agrega Nro de Caja, Lote,Sociedad,Impuesto y Concepto en el orden descripto.")> _
Public Class ippImpuestos
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
    Friend WithEvents Label3 As label
    Friend WithEvents Label2 As label
    Friend WithEvents Label1 As label
    Friend WithEvents ZLabel1 As label
    Friend WithEvents ZLabel2 As label
    Friend WithEvents ZLabel3 As label
    Friend WithEvents txtbatch As TextBox
    Friend WithEvents txtbox As TextBox
    Friend WithEvents btnaceptar As button
    Friend WithEvents txtSeparator As TextBox
    Friend WithEvents cbotax As ComboBox
    Friend WithEvents cboConcept As ComboBox
    Friend WithEvents cbosociedad As ComboBox
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(ippImpuestos))
        Label3 = New label
        txtSeparator = New TextBox
        Label2 = New label
        txtbatch = New TextBox
        txtbox = New TextBox
        btnaceptar = New button
        Label1 = New label
        ZLabel1 = New label
        ZLabel2 = New label
        ZLabel3 = New label
        cbotax = New ComboBox
        cboConcept = New ComboBox
        cbosociedad = New ComboBox
        SuspendLayout()
        '
        'Label3
        '
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Label3.ForeColor = System.Drawing.Color.Black
        Label3.Location = New System.Drawing.Point(192, 9)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(64, 17)
        Label3.TabIndex = 13
        Label3.Text = "Separador"
        Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSeparator
        '
        txtSeparator.Location = New System.Drawing.Point(200, 26)
        txtSeparator.Name = "txtSeparator"
        txtSeparator.Size = New System.Drawing.Size(32, 21)
        txtSeparator.TabIndex = 1
        txtSeparator.Text = "|"
        '
        'Label2
        '
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Label2.ForeColor = System.Drawing.Color.Black
        Label2.Location = New System.Drawing.Point(8, 52)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(152, 17)
        Label2.TabIndex = 11
        Label2.Text = "Ingrese el número de lote"
        Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtbatch
        '
        txtbatch.Location = New System.Drawing.Point(8, 69)
        txtbatch.Name = "txtbatch"
        txtbatch.Size = New System.Drawing.Size(160, 21)
        txtbatch.TabIndex = 2
        txtbatch.Text = ""
        '
        'txtbox
        '
        txtbox.Location = New System.Drawing.Point(8, 26)
        txtbox.Name = "txtbox"
        txtbox.Size = New System.Drawing.Size(160, 21)
        txtbox.TabIndex = 0
        txtbox.Text = ""
        '
        'btnaceptar
        '
        btnaceptar.DialogResult = System.Windows.Forms.DialogResult.None
        btnaceptar.Location = New System.Drawing.Point(192, 215)
        btnaceptar.Name = "btnaceptar"
        btnaceptar.Size = New System.Drawing.Size(72, 26)
        btnaceptar.TabIndex = 6
        btnaceptar.Text = "Aceptar"
        '
        'Label1
        '
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Label1.ForeColor = System.Drawing.Color.Black
        Label1.Location = New System.Drawing.Point(8, 9)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(168, 17)
        Label1.TabIndex = 8
        Label1.Text = "Ingrese el número de caja"
        Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ZLabel1
        '
        ZLabel1.BackColor = System.Drawing.Color.Transparent
        ZLabel1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        ZLabel1.ForeColor = System.Drawing.Color.Black
        ZLabel1.Location = New System.Drawing.Point(8, 103)
        ZLabel1.Name = "ZLabel1"
        ZLabel1.Size = New System.Drawing.Size(152, 18)
        ZLabel1.TabIndex = 15
        ZLabel1.Text = "Ingrese la Sociedad"
        ZLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ZLabel2
        '
        ZLabel2.BackColor = System.Drawing.Color.Transparent
        ZLabel2.Font = New System.Drawing.Font("Tahoma", 8.0!)
        ZLabel2.ForeColor = System.Drawing.Color.Black
        ZLabel2.Location = New System.Drawing.Point(8, 146)
        ZLabel2.Name = "ZLabel2"
        ZLabel2.Size = New System.Drawing.Size(152, 18)
        ZLabel2.TabIndex = 17
        ZLabel2.Text = "Ingrese Impuesto"
        ZLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ZLabel3
        '
        ZLabel3.BackColor = System.Drawing.Color.Transparent
        ZLabel3.Font = New System.Drawing.Font("Tahoma", 8.0!)
        ZLabel3.ForeColor = System.Drawing.Color.Black
        ZLabel3.Location = New System.Drawing.Point(8, 198)
        ZLabel3.Name = "ZLabel3"
        ZLabel3.Size = New System.Drawing.Size(168, 17)
        ZLabel3.TabIndex = 19
        ZLabel3.Text = "Ingrese el Concepto de Impuesto"
        ZLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbotax
        '
        cbotax.Location = New System.Drawing.Point(8, 168)
        cbotax.Name = "cbotax"
        cbotax.Size = New System.Drawing.Size(160, 21)
        cbotax.TabIndex = 4
        '
        'cboConcept
        '
        cboConcept.Location = New System.Drawing.Point(8, 216)
        cboConcept.Name = "cboConcept"
        cboConcept.Size = New System.Drawing.Size(160, 21)
        cboConcept.TabIndex = 5
        '
        'cbosociedad
        '
        cbosociedad.Location = New System.Drawing.Point(8, 122)
        cbosociedad.Name = "cbosociedad"
        cbosociedad.Size = New System.Drawing.Size(160, 21)
        cbosociedad.TabIndex = 3
        '
        'ippImpuestos
        '
        AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        ClientSize = New System.Drawing.Size(272, 264)
        Controls.Add(cbosociedad)
        Controls.Add(cboConcept)
        Controls.Add(cbotax)
        Controls.Add(ZLabel3)
        Controls.Add(txtSeparator)
        Controls.Add(txtbatch)
        Controls.Add(txtbox)
        Controls.Add(ZLabel2)
        Controls.Add(ZLabel1)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(btnaceptar)
        Controls.Add(Label1)
        DockPadding.All = 2
        Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Name = "ippImpuestos"
        Text = "Impuestos"
        ResumeLayout(False)

    End Sub

#End Region
#Region "XML"

    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        Return ""
    End Function
    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml

    End Sub

#End Region
#Region "Eventos"
    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError
    Public Event PreprocessMessage1(ByVal msg As String) Implements Ipreprocess.PreprocessMessage

    Private Sub Impuestos_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Try

            Dim dtImpuesto As DataTable = IndexsBusiness.GetTable(70).Tables(0) 'Cargo el indice Impuesto
            cbotax.DataSource = dtImpuesto
            cbotax.DisplayMember = dtImpuesto.Columns(1).ColumnName
            cbotax.ValueMember = dtImpuesto.Columns(0).ColumnName

            Dim dtConcepto As DataTable = IndexsBusiness.GetTable(71).Tables(0) 'Cargo el indice Concepto
            cboConcept.DataSource = dtConcepto
            cboConcept.DisplayMember = dtConcepto.Columns(1).ColumnName
            cboConcept.ValueMember = dtConcepto.Columns(0).ColumnName

            Dim dtSociedad As DataTable = IndexsBusiness.GetTable(58).Tables(0) 'Cargo el indice Sociedad
            cbosociedad.DataSource = dtSociedad
            cbosociedad.DisplayMember = dtSociedad.Columns(1).ColumnName
            cbosociedad.ValueMember = dtSociedad.Columns(0).ColumnName

        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
#End Region

    Dim file As String

    Private Sub btnaceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnaceptar.Click
        Dim Concepto, lote, caja, Impuesto, Sociedad As String
        Dim separador As String

        separador = txtSeparator.Text.Trim
        If separador = "" Then
            If MessageBox.Show("El Separador está vacio", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) Then
                Exit Sub
            End If
        End If
        lote = txtbatch.Text.Trim
        If lote = "" Then
            If MessageBox.Show("El Nro de Lote está vacio, ¿desea continuar?", "Zamba", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.No Then
                Exit Sub
            End If
        End If
        Impuesto = cbotax.Text.Trim
        If Impuesto = "" Then
            If MessageBox.Show("El campo Impuesto está vacio, ¿desea continuar?", "Zamba", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.No Then
                Exit Sub
            End If
        End If
        caja = txtbox.Text.Trim
        If caja = "" Then
            If MessageBox.Show("El campo Caja está vacio, ¿desea continuar?", "Zamba", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.No Then
                Exit Sub
            End If
        End If
        Concepto = cboConcept.Text.Trim
        If Concepto = "" Then
            If MessageBox.Show("El campo concepto está vacio, ¿desea continuar?", "Zamba", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.No Then
                Exit Sub
            End If
        End If
        Sociedad = cbosociedad.Text.Trim
        If Sociedad = "" Then
            If MessageBox.Show("El campo sociedad está vacio, ¿desea continuar?", "Zamba", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.No Then
                Exit Sub
            End If
        End If
        addparams(Concepto, lote, caja, Impuesto, separador, Sociedad)

    End Sub

    Private Sub addparams(ByVal concepto As String, ByVal lote As String, ByVal caja As String, ByVal impuesto As String, ByVal separador As String, ByVal sociedad As String)
        Dim fileInfo As New IO.FileInfo(file)
        Try
            If fileInfo.Exists Then
                Dim sr As New IO.StreamReader(fileInfo.OpenRead)
                Dim Dir As String = Tools.EnvironmentUtil.GetTempDir("\Temp").FullName

                Dim sw As New IO.StreamWriter(Dir & "\tmp.txt", False)
                sw.AutoFlush = True
                While sr.Peek <> -1
                    Dim str As String = sr.ReadLine
                    Dim strb As New System.Text.StringBuilder
                    strb.Append(caja.ToString)
                    strb.Append(separador)
                    strb.Append(lote)
                    strb.Append(separador)
                    strb.Append(sociedad)
                    strb.Append(separador)
                    strb.Append(impuesto)
                    strb.Append(separador)
                    strb.Append(concepto)
                    strb.Append(separador)
                    strb.Append(str)
                    sw.WriteLine(strb.ToString.Trim)
                End While
                sr.Close()
                sw.Close()
                Dim fio As New System.IO.FileInfo(dir & "\tmp.txt")
                fio.CopyTo(file, True)
                fio.Delete()
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        Finally
            Close()
        End Try
    End Sub

    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Agrega Nro de Caja, Lote,Sociedad,Impuesto y Concepto en el orden descripto."
    End Function

    Public Function process(ByVal Files As System.Collections.ArrayList, Optional ByVal param As Collections.ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As System.Collections.ArrayList Implements Ipreprocess.process
        Dim i As Int32

        For i = 0 To Files.Count - 1
            processFile(Files(i), param(0), xml)
        Next
        Return Files
    End Function

    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        Me.file = File
        ShowDialog()
        Return File
    End Function

End Class