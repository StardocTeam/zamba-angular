Imports Zamba.core
Imports Zamba.Data
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
    Friend WithEvents ImageList1 As ImageList
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(UCTemplatesNew))
        ImageList1 = New ImageList(components)
        SuspendLayout()
        '
        'ImageList1
        '
        ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        ImageList1.TransparentColor = System.Drawing.Color.Transparent
        ImageList1.Images.SetKeyName(0, "appbar.ie.png")
        ImageList1.Images.SetKeyName(1, "excel.png")
        ImageList1.Images.SetKeyName(2, "word.png")
        ImageList1.Images.SetKeyName(3, "ppt.png")
        ImageList1.Images.SetKeyName(4, "outlook.png")
        ImageList1.Images.SetKeyName(5, "appbar.folder.open.png")
        ImageList1.Images.SetKeyName(6, "appbar.page.search.png")
        ImageList1.Images.SetKeyName(7, "appbar.ie.png")
        ImageList1.Images.SetKeyName(8, "outlook.png")
        ImageList1.Images.SetKeyName(9, "appbar.axis.x.png")
        'Me.ImageList1.Images.SetKeyName(10, "outlook.png")
        '
        'UCTemplatesNew
        '
        AutoScroll = True
        Name = "UCTemplatesNew"
        Size = New System.Drawing.Size(353, 296)
        ResumeLayout(False)

    End Sub

#End Region

    Public TemplatesCount As Int32

    Dim DsTemplates As DataSet

    Public Sub GetDBTemplates()
        Try

            Dim i As Int32

            DsTemplates = TemplatesBusiness.GetTemplates()
            For i = 0 To DsTemplates.Tables(0).Rows.Count - 1
                DelegateAddTemplate(New IO.FileInfo(DsTemplates.Tables(0).Rows(i).Item("Path").ToString.Trim), DsTemplates.Tables(0).Rows(i).Item("Name").ToString.Trim, Int64.Parse(DsTemplates.Tables(0).Rows(i).Item("id").ToString))
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
            Dim existTempl As Boolean = false
            For Each fi In Dir.GetFiles
                If fi.Extension = ".dot" OrElse fi.Extension = ".pot" OrElse fi.Extension = ".xlt" OrElse fi.Extension = ".xoml" Then
                    DelegateAddTemplate(fi)
                    existTempl = true
                End If

            Next
            If existTempl then

                TemplatesFactory.SaveTemplates(DsTemplates)
                DsTemplates.Clear()
            End if
        Catch ex As UnauthorizedAccessException
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Delegate Sub DAddtemplate()
    Dim Fi As IO.FileInfo
    Dim TemplateName As String
    Dim TemplateExtension As String
    Private Sub DelegateAddTemplate(ByVal Fi As IO.FileInfo, Optional ByVal TemplateName As String = "", Optional ByVal idTemplate As Integer = 0)
        Try
            Me.Fi = Fi

            If idTemplate = 0 Then
                idTemplate = TemplatesBusiness.GetIdByName(TemplateName)
            End If

            If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.Templates, RightsType.Use, idTemplate) Then

                'Batta
                'Esta condicion se realiza por si es una plantilla nueva para cargar a la base o solamente para cargarla
                'a la grilla
                'Si viene "" es para cargarla a la grilla, hay algunos archivos que en el TemplateName no tiene la extension,
                'por lo que se fija si tiene o no la extension y si no le pasa
                'Si viene el nombre del template es para agregarla a la base

                If Not TemplateName Is "" Then
                    If TemplateName.LastIndexOf(".") < 0 Then
                        TemplateExtension = Fi.FullName.ToString
                    Else
                        TemplateExtension = TemplateName
                    End If
                    Me.TemplateName = TemplateName
                Else
                    TemplateExtension = Fi.Name.ToString
                End If
                Addtemplate()
            End If

            '  Dim d1 As New DAddtemplate(AddressOf Addtemplate)
            '    Me.Invoke(d1)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub Addtemplate()

        'Batta
        'Busca el indice de la ultima aparicion del . del template para identificar la extension
        Dim indexOfExtension As Int32 = TemplateExtension.LastIndexOf(".")
        Select Case LCase(TemplateExtension.Substring(indexOfExtension))
            Case ".dot", ".dotx", ".doc", ".docx"
                AddWordTemplate(Fi)
                TemplatesCount = 1
            Case ".pot", ".potx", ".ppt", ".pptx"
                AddPowerPointTemplate(Fi)
                TemplatesCount = 2
            Case ".xlt", ".xltx", ".xls", ".xlsx"
                AddExcelTemplate(Fi)
                TemplatesCount = 3
            Case ".html", ".htm"
                AddHtmlTemplate(Fi)
                TemplatesCount = 4
            Case ".xoml"
                AddWorkFlowTemplate(Fi)
                TemplatesCount = 5
            Case Else
        End Select
        ' Se crea una fila con el esquema de la tabla contenida en el DsTemplates
        Dim row As DataRow = DsTemplates.Tables(0).NewRow
        ' Se genera un nuevo id para el Template y se lo coloca en la celda Id de la fila
        row("Id") = FactoryTemplates.GetNewTemplateId()
        ' Se colocan los datos ingresados en la fila en base a lo ingresado en la ventana
        row("Name") = Fi.Name
        row("Path") = Fi.DirectoryName
        'row("Description") = Fi.
        row("Type") = TemplatesCount
        DsTemplates.Tables(0).Rows.Add(row)
        'TemplatesFactory.SaveTemplates(DsTemplates)

    End Sub


    Private Sub AddWordTemplate(ByVal fi As IO.FileInfo)
        Try
            Dim Lnk As New System.Windows.Forms.LinkLabel
            Lnk.BackColor = Color.Transparent
            Lnk.ImageList = ImageList1
            Lnk.ImageIndex = 2
            Lnk.ImageAlign = Drawing.ContentAlignment.MiddleLeft
            Lnk.TextAlign = Drawing.ContentAlignment.MiddleLeft
            Lnk.LinkBehavior = LinkBehavior.HoverUnderline
            Lnk.Tag = fi.FullName
            If TemplateName = "" Then
                Lnk.Text = "       " & fi.Name.Trim
            Else
                Lnk.Text = "       " & TemplateName.Trim
            End If
            Dim Font As New Font("Verdana", 10)
            Lnk.Font = Font
            Lnk.SetBounds(10, (Controls.Count * 25) + 2, fi.Name.Length * 10 + 30, 25)
            Invoke(New DAddLink(AddressOf AddLink), New Object() {Lnk})
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
        Controls.Add(Lnk)
    End Sub
    Private Sub AddExcelTemplate(ByVal fi As IO.FileInfo)
        Try
            Dim Lnk As New System.Windows.Forms.LinkLabel
            Lnk.BackColor = Color.Transparent
            Lnk.ImageList = ImageList1
            Lnk.ImageIndex = 1
            Lnk.ImageAlign = Drawing.ContentAlignment.MiddleLeft
            Lnk.TextAlign = Drawing.ContentAlignment.MiddleLeft
            Lnk.LinkBehavior = LinkBehavior.HoverUnderline
            Lnk.Tag = fi.FullName
            If TemplateName = "" Then
                Lnk.Text = "       " & fi.Name.Trim
            Else
                Lnk.Text = "       " & TemplateName.Trim
            End If
            Dim Font As New Font("Verdana", 10)
            Lnk.Font = Font
            Lnk.SetBounds(10, (Controls.Count * 25) + 2, fi.Name.Length * 10 + 30, 25)
            Controls.Add(Lnk)
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
            Lnk.ImageList = ImageList1
            Lnk.ImageIndex = 3
            Lnk.ImageAlign = Drawing.ContentAlignment.MiddleLeft
            Lnk.TextAlign = Drawing.ContentAlignment.MiddleLeft
            Lnk.LinkBehavior = LinkBehavior.HoverUnderline
            Lnk.Tag = fi.FullName
            If TemplateName = "" Then
                Lnk.Text = "       " & fi.Name.Trim
            Else
                Lnk.Text = "       " & TemplateName.Trim
            End If
            Dim Font As New Font("Verdana", 10)
            Lnk.Font = Font
            Lnk.SetBounds(10, (Controls.Count * 25) + 2, fi.Name.Length * 10 + 30, 25)
            Controls.Add(Lnk)
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
            Lnk.ImageList = ImageList1
            Lnk.ImageIndex = 0
            Lnk.ImageAlign = Drawing.ContentAlignment.MiddleLeft
            Lnk.TextAlign = Drawing.ContentAlignment.MiddleLeft
            Lnk.LinkBehavior = LinkBehavior.HoverUnderline
            Lnk.Tag = fi.FullName
            If TemplateName = "" Then
                Lnk.Text = "       " & fi.Name.Trim
            Else
                Lnk.Text = "       " & TemplateName.Trim
            End If
            Dim Font As New Font("Verdana", 10)
            Lnk.Font = Font
            Lnk.SetBounds(10, (Controls.Count * 25) + 2, fi.Name.Length * 10 + 30, 25)
            Controls.Add(Lnk)
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
        Invoke(D1, New Object() {fi})
    End Sub
    Private Sub AddWFTemplate(ByVal fi As IO.FileInfo)
        Try
            Dim Lnk As New System.Windows.Forms.LinkLabel
            Lnk.BackColor = Color.Transparent
            Lnk.ImageList = ImageList1
            Lnk.ImageIndex = 9
            Lnk.ImageAlign = Drawing.ContentAlignment.MiddleLeft
            Lnk.TextAlign = Drawing.ContentAlignment.MiddleLeft
            Lnk.LinkBehavior = LinkBehavior.HoverUnderline
            Lnk.Tag = fi.FullName
            If TemplateName = "" Then
                Lnk.Text = "       " & fi.Name.Trim
            Else
                Lnk.Text = "       " & TemplateName.Trim
            End If
            Dim Font As New Font("Verdana", 10)
            Lnk.Font = Font
            Lnk.SetBounds(10, (Controls.Count * 25) + 2, fi.Name.Length * 10 + 30, 25)
            Controls.Add(Lnk)
            RemoveHandler Lnk.LinkClicked, AddressOf linkClicked
            RemoveHandler Lnk.LinkClicked, AddressOf linkClickedOriginalfilename
            AddHandler Lnk.LinkClicked, AddressOf linkClicked
            AddHandler Lnk.LinkClicked, AddressOf linkClickedOriginalfilename
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Event lnkclicked(ByVal FullFileName As String)
    Private Sub linkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs)
        Try
            Dim Dir As IO.DirectoryInfo = Tools.EnvironmentUtil.GetTempDir("\temp")
            If Dir.Exists = False Then Dir.Create()
            Dim Lnk As LinkLabel = DirectCast(sender, LinkLabel)
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

    Private Sub linkClickedOriginalfilename(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs)
        Try
            Dim Lnk As LinkLabel = DirectCast(sender, LinkLabel)
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
        InitializeComponent()
    End Sub

    Private Sub UCTemplates_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        Try
            GetDBTemplates()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub




End Class
