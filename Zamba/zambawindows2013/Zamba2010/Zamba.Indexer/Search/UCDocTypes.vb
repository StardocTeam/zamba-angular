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
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PanelTop As Label
    Public Property Loading As Boolean

    <DebuggerStepThrough()> Private Sub InitializeComponent()
        ListBox1 = New ListBox()
        DSDocType = New Zamba.Core.DsDoctypeRight()
        PanelTop = New ZLabel()
        TextBox1 = New TextBox()
        PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(DSDocType, ComponentModel.ISupportInitialize).BeginInit
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit
        SuspendLayout
        '
        'ListBox1
        '
        ListBox1.BackColor = System.Drawing.Color.White
        ListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        ListBox1.DisplayMember = "DOC_TYPE_NAME"
        ListBox1.Dock = System.Windows.Forms.DockStyle.Fill
        ListBox1.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        ListBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        ListBox1.ItemHeight = 16
        ListBox1.Location = New System.Drawing.Point(0, 32)
        ListBox1.Name = "ListBox1"
        ListBox1.SelectionMode = SelectionMode.MultiExtended
        ListBox1.Size = New System.Drawing.Size(239, 232)
        ListBox1.TabIndex = 4
        '
        'DSDocType
        '
        DSDocType.DataSetName = "DsDoctypeRight"
        DSDocType.Locale = New System.Globalization.CultureInfo("en-US")
        DSDocType.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'PanelTop
        '
        PanelTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        PanelTop.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        PanelTop.ForeColor = System.Drawing.Color.White
        PanelTop.Location = New System.Drawing.Point(0, 0)
        PanelTop.Name = "PanelTop"
        PanelTop.Size = New System.Drawing.Size(239, 32)
        PanelTop.TabIndex = 8
        PanelTop.Text = " Entidades"
        PanelTop.TextAlign = ContentAlignment.MiddleLeft
        '
        'TextBox1
        '
        TextBox1.BackColor = System.Drawing.Color.White
        TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        TextBox1.Location = New System.Drawing.Point(81, 4)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New System.Drawing.Size(131, 23)
        TextBox1.TabIndex = 9
        '
        'PictureBox1
        '
        PictureBox1.BackColor = System.Drawing.Color.White
        PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        PictureBox1.Image = Global.Zamba.Controls.My.Resources.Resources.appbar_filter
        PictureBox1.Location = New System.Drawing.Point(212, 4)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New System.Drawing.Size(21, 23)
        PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 10
        PictureBox1.TabStop = false
        '
        'UCDocTypes
        '
        Controls.Add(PictureBox1)
        Controls.Add(TextBox1)
        Controls.Add(ListBox1)
        Controls.Add(PanelTop)
        Name = "UCDocTypes"
        Size = New System.Drawing.Size(239, 264)
        CType(DSDocType, ComponentModel.ISupportInitialize).EndInit
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit
        ResumeLayout(false)
        PerformLayout

    End Sub

#End Region

    Dim container As ISearchContainer
    Public Sub New(TabSearch As ISearchContainer)
        MyBase.New()
        InitializeComponent()
        Container = TabSearch
    End Sub
    Public Sub ClearDocTypes()
        Try
            ' Me.DSDocType.DocTypes.Clear()
            ListBox1.Refresh()
            TextBox1.Text = ""
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Try

            If TextBox1.Text.Trim() = String.Empty Then
                ListBox1.DataSource = DSDocType.DocTypes
            Else
                Dim dt As DataTable = DSDocType.DocTypes
                Dim dv As New DataView(dt)
                dv.RowFilter = "Doc_Type_Name like '%" & TextBox1.Text.Trim() & "%'"
                ListBox1.DataSource = dv.ToTable()
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        Try
            If Loading = False Then
                container.UCEntitesSelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
End Class
