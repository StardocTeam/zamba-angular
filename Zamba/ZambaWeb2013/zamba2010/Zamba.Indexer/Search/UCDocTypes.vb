Imports Zamba.AppBlock
Imports Zamba.Core

Public Class UCDocTypes
    Inherits ZControl

#Region " Código generado por el Diseñador de Windows Forms "

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
    '    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Public WithEvents ListBox1 As ListBox
    Public WithEvents DSDocType As DsDoctypeRight
    Friend WithEvents PanelTop As Zamba.AppBlock.ZColorLabel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.DSDocType = New Zamba.Core.DsDoctypeRight
        Me.PanelTop = New Zamba.AppBlock.ZColorLabel
        CType(Me.DSDocType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.BackColor = System.Drawing.Color.White
        Me.ListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBox1.DisplayMember = "DOC_TYPE_NAME"
        Me.ListBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBox1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox1.ForeColor = System.Drawing.Color.Black
        Me.ListBox1.Location = New System.Drawing.Point(0, 24)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBox1.Size = New System.Drawing.Size(200, 234)
        Me.ListBox1.TabIndex = 4
        '
        'DSDocType
        '
        Me.DSDocType.DataSetName = "DsDoctypeRight"
        Me.DSDocType.Locale = New System.Globalization.CultureInfo("en-US")
        Me.DSDocType.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'PanelTop
        '
        Me.PanelTop.BackColor = System.Drawing.Color.White
        Me.PanelTop.Color1 = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.PanelTop.Color2 = System.Drawing.Color.Navy
        Me.PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelTop.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelTop.ForeColor = System.Drawing.Color.White
        Me.PanelTop.GradientMode = System.Drawing.Drawing2D.WrapMode.TileFlipY
        Me.PanelTop.Location = New System.Drawing.Point(0, 0)
        Me.PanelTop.Name = "PanelTop"
        Me.PanelTop.Size = New System.Drawing.Size(200, 24)
        Me.PanelTop.TabIndex = 8
        Me.PanelTop.Text = " Tipos de Documento"
        Me.PanelTop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UCDocTypes
        '
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.PanelTop)
        Me.Name = "UCDocTypes"
        Me.Size = New System.Drawing.Size(200, 264)
        CType(Me.DSDocType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region
    'Declarations
    Public OldSelectedDocTypeIndex As New ArrayList

    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub
    Public Event ListBoxMouseEnter()
    Private Sub ListBox1_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox1.MouseEnter
        RaiseEvent ListBoxMouseEnter()
    End Sub

End Class
