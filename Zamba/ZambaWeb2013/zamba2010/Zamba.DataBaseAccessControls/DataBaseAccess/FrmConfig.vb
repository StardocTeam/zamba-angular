Imports Zamba.AppBlock
Imports Zamba.Core
'Imports Zamba.DBAccess.Core
Public Class FrmConfig
    Inherits System.Windows.Forms.UserControl

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
    Friend WithEvents Button1 As ZAMBA.AppBlock.ZButton
    Friend WithEvents TabControl1 As ztabs
    Friend WithEvents TabPage1 As ztabspage
    Friend WithEvents TabPage2 As ztabspage
    Friend WithEvents Grilla As System.Windows.Forms.DataGrid
    Friend WithEvents txt As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Button1 = New Zamba.AppBlock.ZButton
        Me.TabControl1 = New Zamba.AppBlock.ZTabs
        Me.TabPage1 = New Zamba.AppBlock.ZTabsPage
        Me.Grilla = New System.Windows.Forms.DataGrid
        Me.TabPage2 = New Zamba.AppBlock.ZTabsPage
        Me.Label1 = New System.Windows.Forms.Label
        Me.txt = New System.Windows.Forms.TextBox
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.Grilla, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.None
        Me.Button1.Location = New System.Drawing.Point(208, 168)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(72, 24)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Aceptar"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(8, 8)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(280, 152)
        Me.TabControl1.TabIndex = 4
        '
        'TabPage1
        '
        Me.TabPage1.Color1 = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TabPage1.Color2 = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TabPage1.Controls.Add(Me.Grilla)
        Me.TabPage1.GradientMode = System.Drawing.Drawing2D.WrapMode.TileFlipY
        Me.TabPage1.IncludeBackground = True
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(272, 126)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Consulta"
        '
        'Grilla
        '
        Me.Grilla.DataMember = ""
        Me.Grilla.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.Grilla.Location = New System.Drawing.Point(8, 8)
        Me.Grilla.Name = "Grilla"
        Me.Grilla.Size = New System.Drawing.Size(264, 112)
        Me.Grilla.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Color1 = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TabPage2.Color2 = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(255, Byte))
        Me.TabPage2.Controls.Add(Me.Label1)
        Me.TabPage2.Controls.Add(Me.txt)
        Me.TabPage2.GradientMode = System.Drawing.Drawing2D.WrapMode.TileFlipY
        Me.TabPage2.IncludeBackground = True
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(272, 126)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Indice del Código"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(248, 32)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Ingrese Id de campos clave, separado por comas"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt
        '
        Me.txt.BackColor = System.Drawing.Color.White
        Me.txt.Location = New System.Drawing.Point(16, 56)
        Me.txt.Name = "txt"
        Me.txt.Size = New System.Drawing.Size(240, 20)
        Me.txt.TabIndex = 0
        Me.txt.Text = ""
        '
        'FrmConfig
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(215, Byte), CType(228, Byte), CType(247, Byte))
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Button1)
        Me.Name = "FrmConfig"
        Me.Size = New System.Drawing.Size(292, 198)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.Grilla, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim ds As DataSet
    Private _Campoid, _ConsultaId As Int32
    Public Property CampoId() As Int32
        Get
            Return _Campoid
        End Get
        Set(ByVal Value As Int32)
            _Campoid = Value
        End Set
    End Property
    Public Property ConsultaID() As Int32
        Get
            Return _ConsultaId
        End Get
        Set(ByVal Value As Int32)
            _ConsultaId = Value
        End Set
    End Property
    Public Shared Event Parametros(ByVal params As String)
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim dscon As New DsConfig
        Dim row As DsConfig.DsConfigRow = dscon.DsConfig.NewDsConfigRow
        dscon.Tables(0).Rows.Clear()
        row.Campo = CInt(txt.Text)
        row.Consulta = Me.ConsultaID.ToString()
        dscon.Tables(0).Rows.Add(row)
        dscon.AcceptChanges()
        dscon.WriteXml(".\zdbconfig.xml")
        Dim cadena As String = Me.ConsultaID.ToString & "," & txt.Text 'Me.CampoId.ToString
        RaiseEvent Parametros(cadena)
        Me.Visible = False
        Me.ParentForm.Visible = False
        Me.ParentForm.Dispose()
        Me.Dispose()
    End Sub
    Private Shared Function GetQuerys() As DataSet
        Return DataBaseAccessBusiness.GetQuerys
    End Function
    Private Sub FrmConfig_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Grilla.DataSource = GetQuerys.Tables(0)
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub

    Private Sub Grilla_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grilla.Click
        Me.ConsultaID = CInt(ds.Tables(0).Rows(Grilla.CurrentCell.RowNumber).Item(0))
    End Sub

    Private Sub txt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt.TextChanged

    End Sub
End Class
