Imports ZAMBA.AppBlock
Imports zamba.core
Public Class UCTemplatesNew
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
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents BtnSearchAllTemplates As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnUpdateTemplates As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UCTemplatesNew))
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.BtnSearchAllTemplates = New System.Windows.Forms.ToolStripButton
        Me.btnUpdateTemplates = New System.Windows.Forms.ToolStripButton
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.AutoScroll = True
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(0, 25)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(353, 271)
        Me.Panel2.TabIndex = 1
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "")
        Me.ImageList1.Images.SetKeyName(1, "")
        Me.ImageList1.Images.SetKeyName(2, "")
        Me.ImageList1.Images.SetKeyName(3, "")
        Me.ImageList1.Images.SetKeyName(4, "")
        Me.ImageList1.Images.SetKeyName(5, "")
        Me.ImageList1.Images.SetKeyName(6, "")
        Me.ImageList1.Images.SetKeyName(7, "")
        Me.ImageList1.Images.SetKeyName(8, "")
        Me.ImageList1.Images.SetKeyName(9, "stream.png")
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BtnSearchAllTemplates, Me.btnUpdateTemplates})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(353, 25)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'BtnSearchAllTemplates
        '
        Me.BtnSearchAllTemplates.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.BtnSearchAllTemplates.Image = CType(resources.GetObject("BtnSearchAllTemplates.Image"), System.Drawing.Image)
        Me.BtnSearchAllTemplates.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnSearchAllTemplates.Name = "BtnSearchAllTemplates"
        Me.BtnSearchAllTemplates.Size = New System.Drawing.Size(140, 22)
        Me.BtnSearchAllTemplates.Text = "Buscar plantillas en el disco"
        '
        'btnUpdateTemplates
        '
        Me.btnUpdateTemplates.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnUpdateTemplates.Image = CType(resources.GetObject("btnUpdateTemplates.Image"), System.Drawing.Image)
        Me.btnUpdateTemplates.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnUpdateTemplates.Name = "btnUpdateTemplates"
        Me.btnUpdateTemplates.Size = New System.Drawing.Size(58, 22)
        Me.btnUpdateTemplates.Text = "Actualizar"
        '
        'UCTemplatesNew
        '
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "UCTemplatesNew"
        Me.Size = New System.Drawing.Size(353, 296)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public TemplatesCount As Int32

    Dim DsTemplates As DataSet

    Public Sub GetDBTemplates()
        Try

            Dim i As Int32

            DsTemplates = TemplatesBusiness.GetTemplates()

            For i = 0 To DsTemplates.Tables(0).Rows.Count - 1
                DelegateAddTemplate(New IO.FileInfo(DsTemplates.Tables(0).Rows(i).Item("Path").ToString.Trim), DsTemplates.Tables(0).Rows(i).Item("Name").ToString.Trim)
            Next

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Sub GetLocalDiscTemplates()
        Try
            '            Me.GetTemplates(New IO.DirectoryInfo("C:\Program Files\Microsoft Office\Templates\MMC"))
            '           Me.GetTemplates(New IO.DirectoryInfo("C:\Archivos de programa\Microsoft Office\Templates\MMC"))

            odir = New IO.DirectoryInfo(Application.StartupPath)
            '       GetoTemplates()
            Dim t1 As New Threading.Thread(AddressOf GetoTemplates)
            t1.Name = "GetTemplates"
            t1.Start()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub GetoTemplates()
        GetSubDirTemplates(odir)
    End Sub
    Private odir As IO.DirectoryInfo
    Private Sub GetSubDirTemplates(ByVal Dir As IO.DirectoryInfo)
        Try
            Dim Di As IO.DirectoryInfo
            For Each Di In Dir.GetDirectories
                GetTemplates(Di)
                GetSubDirTemplates(Di)
            Next
        Catch ex As UnauthorizedAccessException
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Sub GetTemplates(ByVal Dir As IO.DirectoryInfo)
        Try
            Dim fi As IO.FileInfo
            For Each fi In Dir.GetFiles
                If fi.Extension = ".dot" OrElse fi.Extension = ".pot" OrElse fi.Extension = ".xlt" OrElse fi.Extension = ".xoml" Then
                    DelegateAddTemplate(fi)
                End If
            Next
        Catch ex As UnauthorizedAccessException
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Delegate Sub DAddtemplate()
    Dim Fi As IO.FileInfo
    Dim TemplateName As String
    Private Sub DelegateAddTemplate(ByVal Fi As IO.FileInfo, Optional ByVal TemplateName As String = "")
        Try
            Me.Fi = Fi
            Me.TemplateName = TemplateName
            Addtemplate()
            '  Dim d1 As New DAddtemplate(AddressOf Addtemplate)
            '    Me.Invoke(d1)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub Addtemplate()
        Select Case LCase(Fi.Extension)
            Case ".dot", ".dotx", ".doc", ".docx"
                AddWordTemplate(Fi)
                TemplatesCount += +1
            Case ".pot", ".potx", ".ppt", ".pptx"
                AddPowerPointTemplate(Fi)
                TemplatesCount += +1
            Case ".xlt", ".xltx", ".xls", ".xlsx"
                AddExcelTemplate(Fi)
                TemplatesCount += +1
            Case ".html", ".htm"
                AddHtmlTemplate(Fi)
                TemplatesCount += +1
            Case ".xoml"
                AddWorkFlowTemplate(Fi)
                TemplatesCount += +1
            Case Else
        End Select
    End Sub


    Private Sub AddWordTemplate(ByVal fi As IO.FileInfo)
        Try
            Dim Lnk As New System.Windows.Forms.LinkLabel
            Lnk.BackColor = Color.Transparent
            Lnk.ImageList = Me.ImageList1
            Lnk.ImageIndex = 2
            Lnk.ImageAlign = Drawing.ContentAlignment.MiddleLeft
            Lnk.TextAlign = Drawing.ContentAlignment.MiddleLeft
            Lnk.LinkBehavior = Windows.Forms.LinkBehavior.HoverUnderline
            Lnk.Tag = fi.FullName
            If TemplateName = "" Then
                Lnk.Text = "       " & fi.Name.Trim
            Else
                Lnk.Text = "       " & TemplateName.Trim
            End If
            Dim Font As New Font("Verdana", 10)
            Lnk.Font = Font
            Lnk.SetBounds(10, (Me.Panel2.Controls.Count * 25) + 2, fi.Name.Length * 10 + 30, 25)
            Me.Invoke(New DAddLink(AddressOf AddLink), New Object() {Lnk})
            '            Me.Panel2.Controls.Add(Lnk)
            RemoveHandler Lnk.LinkClicked, AddressOf linkClicked
            RemoveHandler Lnk.LinkClicked, AddressOf linkClickedOriginalfilename
            AddHandler Lnk.LinkClicked, AddressOf linkClicked
            AddHandler Lnk.LinkClicked, AddressOf linkClickedOriginalfilename
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Delegate Sub DAddLink(ByVal Lnk As LinkLabel)

    Private Sub AddLink(ByVal Lnk As LinkLabel)
        Me.Panel2.Controls.Add(Lnk)
    End Sub
    Private Sub AddExcelTemplate(ByVal fi As IO.FileInfo)
        Try
            Dim Lnk As New System.Windows.Forms.LinkLabel
            Lnk.BackColor = Color.Transparent
            Lnk.ImageList = Me.ImageList1
            Lnk.ImageIndex = 1
            Lnk.ImageAlign = Drawing.ContentAlignment.MiddleLeft
            Lnk.TextAlign = Drawing.ContentAlignment.MiddleLeft
            Lnk.LinkBehavior = Windows.Forms.LinkBehavior.HoverUnderline
            Lnk.Tag = fi.FullName
            If TemplateName = "" Then
                Lnk.Text = "       " & fi.Name.Trim
            Else
                Lnk.Text = "       " & TemplateName.Trim
            End If
            Dim Font As New Font("Verdana", 10)
            Lnk.Font = Font
            Lnk.SetBounds(10, (Me.Panel2.Controls.Count * 25) + 2, fi.Name.Length * 10 + 30, 25)
            Me.Panel2.Controls.Add(Lnk)
            RemoveHandler Lnk.LinkClicked, AddressOf linkClicked
            RemoveHandler Lnk.LinkClicked, AddressOf linkClickedOriginalfilename
            AddHandler Lnk.LinkClicked, AddressOf linkClicked
            AddHandler Lnk.LinkClicked, AddressOf linkClickedOriginalfilename
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub AddPowerPointTemplate(ByVal fi As IO.FileInfo)
        Try
            Dim Lnk As New System.Windows.Forms.LinkLabel
            Lnk.BackColor = Color.Transparent
            Lnk.ImageList = Me.ImageList1
            Lnk.ImageIndex = 3
            Lnk.ImageAlign = Drawing.ContentAlignment.MiddleLeft
            Lnk.TextAlign = Drawing.ContentAlignment.MiddleLeft
            Lnk.LinkBehavior = Windows.Forms.LinkBehavior.HoverUnderline
            Lnk.Tag = fi.FullName
            If TemplateName = "" Then
                Lnk.Text = "       " & fi.Name.Trim
            Else
                Lnk.Text = "       " & TemplateName.Trim
            End If
            Dim Font As New Font("Verdana", 10)
            Lnk.Font = Font
            Lnk.SetBounds(10, (Me.Panel2.Controls.Count * 25) + 2, fi.Name.Length * 10 + 30, 25)
            Me.Panel2.Controls.Add(Lnk)
            RemoveHandler Lnk.LinkClicked, AddressOf linkClicked
            RemoveHandler Lnk.LinkClicked, AddressOf linkClickedOriginalfilename
            AddHandler Lnk.LinkClicked, AddressOf linkClicked
            AddHandler Lnk.LinkClicked, AddressOf linkClickedOriginalfilename
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub AddHtmlTemplate(ByVal fi As IO.FileInfo)
        Try
            Dim Lnk As New System.Windows.Forms.LinkLabel
            Lnk.BackColor = Color.Transparent
            Lnk.ImageList = Me.ImageList1
            Lnk.ImageIndex = 0
            Lnk.ImageAlign = Drawing.ContentAlignment.MiddleLeft
            Lnk.TextAlign = Drawing.ContentAlignment.MiddleLeft
            Lnk.LinkBehavior = Windows.Forms.LinkBehavior.HoverUnderline
            Lnk.Tag = fi.FullName
            If TemplateName = "" Then
                Lnk.Text = "       " & fi.Name.Trim
            Else
                Lnk.Text = "       " & TemplateName.Trim
            End If
            Dim Font As New Font("Verdana", 10)
            Lnk.Font = Font
            Lnk.SetBounds(10, (Me.Panel2.Controls.Count * 25) + 2, fi.Name.Length * 10 + 30, 25)
            Me.Panel2.Controls.Add(Lnk)
            RemoveHandler Lnk.LinkClicked, AddressOf linkClicked
            RemoveHandler Lnk.LinkClicked, AddressOf linkClickedOriginalfilename
            AddHandler Lnk.LinkClicked, AddressOf linkClicked
            AddHandler Lnk.LinkClicked, AddressOf linkClickedOriginalfilename
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Delegate Sub DAddWorkFlowTemplate(ByVal fi As IO.FileInfo)
    Private Sub AddWorkFlowTemplate(ByVal fi As IO.FileInfo)
        Dim D1 As New DAddWorkFlowTemplate(AddressOf AddWFTemplate)
        Me.Invoke(D1, New Object() {fi})
    End Sub
    Private Sub AddWFTemplate(ByVal fi As IO.FileInfo)
        Try
            Dim Lnk As New System.Windows.Forms.LinkLabel
            Lnk.BackColor = Color.Transparent
            Lnk.ImageList = Me.ImageList1
            Lnk.ImageIndex = 9
            Lnk.ImageAlign = Drawing.ContentAlignment.MiddleLeft
            Lnk.TextAlign = Drawing.ContentAlignment.MiddleLeft
            Lnk.LinkBehavior = Windows.Forms.LinkBehavior.HoverUnderline
            Lnk.Tag = fi.FullName
            If TemplateName = "" Then
                Lnk.Text = "       " & fi.Name.Trim
            Else
                Lnk.Text = "       " & TemplateName.Trim
            End If
            Dim Font As New Font("Verdana", 10)
            Lnk.Font = Font
            Lnk.SetBounds(10, (Me.Panel2.Controls.Count * 25) + 2, fi.Name.Length * 10 + 30, 25)
            Me.Panel2.Controls.Add(Lnk)
            RemoveHandler Lnk.LinkClicked, AddressOf linkClicked
            RemoveHandler Lnk.LinkClicked, AddressOf linkClickedOriginalfilename
            AddHandler Lnk.LinkClicked, AddressOf linkClicked
            AddHandler Lnk.LinkClicked, AddressOf linkClickedOriginalfilename
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Event lnkclicked(ByVal FullFileName As String)
    Private Sub linkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        Try
            Dim Dir As IO.DirectoryInfo = Tools.EnvironmentUtil.GetTempDir("\temp")
            If Dir.Exists = False Then Dir.Create()
            Dim Lnk As Windows.Forms.LinkLabel = DirectCast(sender, LinkLabel)
            Dim Fi As New IO.FileInfo(DirectCast(Lnk.Tag, String).Trim)
            If Fi.Exists Then
                Try
                    Fi.CopyTo(Dir.FullName & "\" & Fi.Name, True)
                    RaiseEvent lnkclicked(Dir.FullName & "\" & Fi.Name)
                Catch
                    Fi.CopyTo(Dir.FullName & "\" & Fi.Name & "1", True)
                    RaiseEvent lnkclicked(Dir.FullName & "\" & Fi.Name & "1")
                End Try
            Else
                MessageBox.Show("No se encontro el Template")
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Event linkClickedOriginalName(ByVal filename As String)

    Private Sub linkClickedOriginalfilename(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        Try
            Dim Lnk As Windows.Forms.LinkLabel = DirectCast(sender, LinkLabel)
            Dim Fi As New IO.FileInfo(DirectCast(Lnk.Tag, String).Trim)
            If Fi.Exists Then
                RaiseEvent linkClickedOriginalName(Fi.FullName)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Sub New()
        MyBase.New()
        Me.InitializeComponent()
    End Sub

    Private Sub UCTemplates_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.GetDBTemplates()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub BtnSearchAllTemplates_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearchAllTemplates.Click
        Try
            Dim Dir As New IO.DirectoryInfo("C:\")
            Me.GetSubDirTemplates(Dir)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se presiona el botón Actualizar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	30/04/2008	Created
    ''' </history>
    Private Sub btnUpdateTemplates_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateTemplates.Click

        Try

            Panel2.Controls.Clear()
            Me.GetDBTemplates()

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub

End Class
