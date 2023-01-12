Imports Zamba.Core
Imports System.IO
Imports Zamba.Viewers

Public Class BrowserFiles
    Inherits ZControl

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

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
    Friend WithEvents FolderBrowserDialog As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents pnlOffice As System.Windows.Forms.Panel
    Friend WithEvents btnExaminarCarpetas As ZButton
    Friend WithEvents btnExaminarArchivos As ZButton
    Friend WithEvents pnlPlantilla As System.Windows.Forms.Panel
    Friend WithEvents btnExaminarScanner As ZButton
    Friend WithEvents pnlVirtualForm As System.Windows.Forms.Panel
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents PnlOfficeContainer As Panel
    Friend WithEvents btnrefresh As ZButton
    Friend WithEvents OpenFileDialog As OpenFileDialog
    Friend WithEvents btnaddtemplate As ZButton
    Friend WithEvents Panel1 As Panel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(BrowserFiles))
        FolderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog()
        ImageList1 = New ImageList(components)
        pnlPlantilla = New System.Windows.Forms.Panel()
        btnaddtemplate = New ZButton()
        btnrefresh = New ZButton()
        Label3 = New ZLabel()
        pnlVirtualForm = New System.Windows.Forms.Panel()
        Label2 = New ZLabel()
        pnlOffice = New System.Windows.Forms.Panel()
        PnlOfficeContainer = New System.Windows.Forms.Panel()
        Label1 = New ZLabel()
        Panel1 = New System.Windows.Forms.Panel()
        btnExaminarCarpetas = New ZButton()
        btnExaminarScanner = New ZButton()
        btnExaminarArchivos = New ZButton()
        OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        pnlPlantilla.SuspendLayout()
        pnlVirtualForm.SuspendLayout()
        pnlOffice.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        '
        'FolderBrowserDialog
        '
        FolderBrowserDialog.ShowNewFolderButton = False
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
        ImageList1.Images.SetKeyName(10, "")
        '
        'pnlPlantilla
        '
        pnlPlantilla.AutoScroll = True
        pnlPlantilla.BackColor = System.Drawing.Color.White
        pnlPlantilla.Controls.Add(btnaddtemplate)
        pnlPlantilla.Controls.Add(btnrefresh)
        pnlPlantilla.Controls.Add(Label3)
        pnlPlantilla.Dock = System.Windows.Forms.DockStyle.Right
        pnlPlantilla.Location = New System.Drawing.Point(673, 46)
        pnlPlantilla.Margin = New System.Windows.Forms.Padding(10)
        pnlPlantilla.Name = "pnlPlantilla"
        pnlPlantilla.Padding = New System.Windows.Forms.Padding(10)
        pnlPlantilla.Size = New System.Drawing.Size(458, 904)
        pnlPlantilla.TabIndex = 38
        '
        'btnaddtemplate
        '
        btnaddtemplate.BackColor = System.Drawing.Color.White
        btnaddtemplate.BackgroundImage = Global.Zamba.Controls.My.Resources.Resources.appbar_add
        btnaddtemplate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        btnaddtemplate.FlatStyle = FlatStyle.Flat
        btnaddtemplate.ForeColor = System.Drawing.Color.White
        btnaddtemplate.Image = Global.Zamba.Controls.My.Resources.Resources.refresh
        btnaddtemplate.ImageAlign = ContentAlignment.MiddleRight
        btnaddtemplate.Location = New System.Drawing.Point(131, 18)
        btnaddtemplate.Name = "btnaddtemplate"
        btnaddtemplate.Size = New System.Drawing.Size(41, 37)
        btnaddtemplate.TabIndex = 2
        btnaddtemplate.UseVisualStyleBackColor = False
        '
        'btnrefresh
        '
        btnrefresh.BackColor = System.Drawing.Color.White
        btnrefresh.BackgroundImage = Global.Zamba.Controls.My.Resources.Resources.refresh
        btnrefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        btnrefresh.FlatStyle = FlatStyle.Flat
        btnrefresh.ForeColor = System.Drawing.Color.White
        btnrefresh.Image = Global.Zamba.Controls.My.Resources.Resources.refresh
        btnrefresh.ImageAlign = ContentAlignment.MiddleRight
        btnrefresh.Location = New System.Drawing.Point(108, 25)
        btnrefresh.Name = "btnrefresh"
        btnrefresh.Size = New System.Drawing.Size(28, 23)
        btnrefresh.TabIndex = 1
        btnrefresh.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Label3.BackColor = System.Drawing.Color.Transparent
        Label3.Dock = System.Windows.Forms.DockStyle.Top
        Label3.FlatStyle = FlatStyle.Flat
        Label3.Font = New Font("Verdana", 12.0!)
        Label3.FontSize = 12.0!
        Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label3.Location = New System.Drawing.Point(10, 10)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(438, 53)
        Label3.TabIndex = 0
        Label3.Text = "Plantillas"
        Label3.TextAlign = ContentAlignment.MiddleLeft
        '
        'pnlVirtualForm
        '
        pnlVirtualForm.AutoScroll = True
        pnlVirtualForm.BackColor = System.Drawing.Color.White
        pnlVirtualForm.Controls.Add(Label2)
        pnlVirtualForm.Dock = System.Windows.Forms.DockStyle.Fill
        pnlVirtualForm.Location = New System.Drawing.Point(276, 46)
        pnlVirtualForm.Margin = New System.Windows.Forms.Padding(10)
        pnlVirtualForm.Name = "pnlVirtualForm"
        pnlVirtualForm.Padding = New System.Windows.Forms.Padding(10)
        pnlVirtualForm.Size = New System.Drawing.Size(397, 904)
        pnlVirtualForm.TabIndex = 44
        '
        'Label2
        '
        Label2.BackColor = System.Drawing.Color.Transparent
        Label2.Dock = System.Windows.Forms.DockStyle.Top
        Label2.FlatStyle = FlatStyle.Flat
        Label2.Font = New Font("Verdana", 12.0!)
        Label2.FontSize = 12.0!
        Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label2.Location = New System.Drawing.Point(10, 10)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(377, 53)
        Label2.TabIndex = 0
        Label2.Text = "Formularios"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        '
        'pnlOffice
        '
        pnlOffice.AutoScroll = True
        pnlOffice.BackColor = System.Drawing.Color.White
        pnlOffice.Controls.Add(PnlOfficeContainer)
        pnlOffice.Controls.Add(Label1)
        pnlOffice.Dock = System.Windows.Forms.DockStyle.Left
        pnlOffice.Location = New System.Drawing.Point(0, 46)
        pnlOffice.Margin = New System.Windows.Forms.Padding(10)
        pnlOffice.Name = "pnlOffice"
        pnlOffice.Padding = New System.Windows.Forms.Padding(10)
        pnlOffice.Size = New System.Drawing.Size(276, 904)
        pnlOffice.TabIndex = 35
        '
        'PnlOfficeContainer
        '
        PnlOfficeContainer.Dock = System.Windows.Forms.DockStyle.Fill
        PnlOfficeContainer.Location = New System.Drawing.Point(10, 63)
        PnlOfficeContainer.Name = "PnlOfficeContainer"
        PnlOfficeContainer.Size = New System.Drawing.Size(256, 831)
        PnlOfficeContainer.TabIndex = 1
        '
        'Label1
        '
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Dock = System.Windows.Forms.DockStyle.Top
        Label1.FlatStyle = FlatStyle.Flat
        Label1.Font = New Font("Verdana", 12.0!)
        Label1.FontSize = 12.0!
        Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Label1.Location = New System.Drawing.Point(10, 10)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(256, 53)
        Label1.TabIndex = 0
        Label1.Text = "Office"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        '
        'Panel1
        '
        Panel1.Controls.Add(btnExaminarCarpetas)
        Panel1.Controls.Add(btnExaminarScanner)
        Panel1.Controls.Add(btnExaminarArchivos)
        Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Panel1.Location = New System.Drawing.Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(1131, 46)
        Panel1.TabIndex = 45
        '
        'btnExaminarCarpetas
        '
        btnExaminarCarpetas.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnExaminarCarpetas.FlatStyle = FlatStyle.Flat
        btnExaminarCarpetas.ForeColor = System.Drawing.Color.White
        btnExaminarCarpetas.ImageAlign = ContentAlignment.MiddleLeft
        btnExaminarCarpetas.Location = New System.Drawing.Point(210, 7)
        btnExaminarCarpetas.Name = "btnExaminarCarpetas"
        btnExaminarCarpetas.Size = New System.Drawing.Size(140, 31)
        btnExaminarCarpetas.TabIndex = 33
        btnExaminarCarpetas.Text = "Insertar Carpeta"
        btnExaminarCarpetas.UseVisualStyleBackColor = False
        '
        'btnExaminarScanner
        '
        btnExaminarScanner.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnExaminarScanner.FlatStyle = FlatStyle.Flat
        btnExaminarScanner.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        btnExaminarScanner.ForeColor = System.Drawing.Color.White
        btnExaminarScanner.ImageAlign = ContentAlignment.MiddleLeft
        btnExaminarScanner.Location = New System.Drawing.Point(414, 7)
        btnExaminarScanner.Name = "btnExaminarScanner"
        btnExaminarScanner.Size = New System.Drawing.Size(140, 31)
        btnExaminarScanner.TabIndex = 38
        btnExaminarScanner.Text = "Escanear"
        btnExaminarScanner.UseVisualStyleBackColor = False
        '
        'btnExaminarArchivos
        '
        btnExaminarArchivos.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnExaminarArchivos.FlatStyle = FlatStyle.Flat
        btnExaminarArchivos.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        btnExaminarArchivos.ForeColor = System.Drawing.Color.White
        btnExaminarArchivos.ImageAlign = ContentAlignment.MiddleLeft
        btnExaminarArchivos.Location = New System.Drawing.Point(3, 7)
        btnExaminarArchivos.Name = "btnExaminarArchivos"
        btnExaminarArchivos.Size = New System.Drawing.Size(140, 31)
        btnExaminarArchivos.TabIndex = 30
        btnExaminarArchivos.Text = "Insertar Archivos"
        btnExaminarArchivos.UseVisualStyleBackColor = False
        '
        'OpenFileDialog
        '
        OpenFileDialog.Multiselect = True
        '
        'BrowserFiles
        '
        AutoScroll = True
        BackColor = System.Drawing.Color.WhiteSmoke
        Controls.Add(pnlVirtualForm)
        Controls.Add(pnlOffice)
        Controls.Add(pnlPlantilla)
        Controls.Add(Panel1)
        Font = New Font("Verdana", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Name = "BrowserFiles"
        Size = New System.Drawing.Size(1131, 950)
        pnlPlantilla.ResumeLayout(False)
        pnlVirtualForm.ResumeLayout(False)
        pnlOffice.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

#Region "Eventos & Declaraciones"

    Public Event AddFiles(ByVal Files As ArrayList, ByVal DocType As DocType)
    Public Event ShowResult(ByRef Result As NewResult)

    Private _ucTemplatesNew As UCTemplatesNew
    Private frmForms As UCVirtualDocumentSelector


    Private WithEvents Lnk1 As System.Windows.Forms.LinkLabel
    Private WithEvents Lnk2 As System.Windows.Forms.LinkLabel
    Private WithEvents Lnk3 As System.Windows.Forms.LinkLabel


    ''' <summary>
    ''' Propiedad que devuelve el path dependiendo si esta o no en applicationdata
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> [Ezequiel] 11/03/09 - Created </history>
    Private ReadOnly Property _pathDirectory() As String
        Get
            Return Tools.EnvironmentUtil.GetTempDir("\IndexerTemp").FullName
        End Get
    End Property
#End Region

    Private Sub BrowserFiles_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load

        _ucTemplatesNew = New UCTemplatesNew
        _ucTemplatesNew.Dock = DockStyle.Fill
        pnlPlantilla.Controls.Add(_ucTemplatesNew)
        _ucTemplatesNew.BringToFront()
        RemoveHandler _ucTemplatesNew.lnkclicked, AddressOf LnkClicked
        AddHandler _ucTemplatesNew.lnkclicked, AddressOf LnkClicked
        AddGenericTemplates()

        'Forms
        frmForms = New UCVirtualDocumentSelector
        frmForms.Dock = DockStyle.Fill
        pnlVirtualForm.Controls.Add(frmForms)
        frmForms.BringToFront()
        RemoveHandler frmForms.FormSelected, AddressOf FormSelected
        AddHandler frmForms.FormSelected, AddressOf FormSelected

        'SCANNER
        If Not UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.ModuleScan, RightsType.Use) Then
            btnExaminarScanner.Enabled = False
        End If

    End Sub

    Private Sub FormSelected(NewResult As INewResult)
        RaiseEvent ShowResult(NewResult)
    End Sub

    Private Sub LnkClicked(ByVal FullName As String)
        Dim ArrayListFiles As New ArrayList
        Dim File As New FileInfo(FullName)
        ArrayListFiles.Add(CreateNewFile(File.FullName, File.Name, File.Extension.Substring(1)))
        RaiseEvent AddFiles(ArrayListFiles, Nothing)
    End Sub


#Region "TEMPLATES"
    Private Sub AddGenericTemplates()

        Lnk1 = New System.Windows.Forms.LinkLabel
        Lnk2 = New System.Windows.Forms.LinkLabel
        Lnk3 = New System.Windows.Forms.LinkLabel


        Try

            Lnk1.BackColor = Color.Transparent
            Lnk1.ImageList = ImageList1
            Lnk1.ImageIndex = 2
            Lnk1.ImageAlign = Drawing.ContentAlignment.MiddleLeft
            Lnk1.TextAlign = Drawing.ContentAlignment.BottomLeft
            Lnk1.LinkBehavior = LinkBehavior.NeverUnderline
            Lnk1.Text = "       Nuevo Word"
            Lnk1.Font = Zamba.AppBlock.ZambaUIHelpers.GetFontFamily(11)
            Lnk1.SetBounds(3, (PnlOfficeContainer.Controls.Count * 35) + 3, 250, 32)
            PnlOfficeContainer.Controls.Add(Lnk1)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Try
            Lnk2.BackColor = Color.Transparent
            Lnk2.ImageList = ImageList1
            Lnk2.ImageIndex = 1
            Lnk2.ImageAlign = Drawing.ContentAlignment.MiddleLeft
            Lnk2.TextAlign = Drawing.ContentAlignment.MiddleLeft
            Lnk2.LinkBehavior = LinkBehavior.NeverUnderline
            Lnk2.Text = "       Nuevo Excel"
            Lnk2.Font = Zamba.AppBlock.ZambaUIHelpers.GetFontFamily(11)
            Lnk2.SetBounds(3, (PnlOfficeContainer.Controls.Count * 35) + 3, 250, 32)
            PnlOfficeContainer.Controls.Add(Lnk2)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Try
            Lnk3.BackColor = Color.Transparent
            Lnk3.ImageList = ImageList1
            Lnk3.ImageIndex = 3
            Lnk3.ImageAlign = Drawing.ContentAlignment.MiddleLeft
            Lnk3.TextAlign = Drawing.ContentAlignment.MiddleLeft
            Lnk3.LinkBehavior = LinkBehavior.NeverUnderline
            Lnk3.Text = "       Nuevo Powerpoint"
            Lnk3.Font = Zamba.AppBlock.ZambaUIHelpers.GetFontFamily(11)
            Lnk3.SetBounds(3, (PnlOfficeContainer.Controls.Count * 35) + 3, 250, 32)
            PnlOfficeContainer.Controls.Add(Lnk3)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try


    End Sub

#End Region

    Private Function CreateNewFile(ByVal Original As String, ByVal Nombre As String, ByVal Extension As String) As String
        Dim File As New FileInfo(Original)
        If Not File.Exists Then File.Create()
        Dim Number As Int32 = 0
        Dim NuevoFile As FileInfo
        Try
            NuevoFile = New FileInfo(FileBusiness.GetUniqueFileName(_pathDirectory & "\", Nombre, "." & Extension))
            File.CopyTo(NuevoFile.FullName, False)
            Dim Path As String = NuevoFile.FullName
            Return Path
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            File = Nothing
            NuevoFile = Nothing
        End Try
    End Function


#Region "Abrir Carpetas"
    ''' <summary>
    ''' ExaminarCarpetas Event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>
    '''     Javier  30/12/2010  Modified
    ''' </history>
    Private Sub btnExaminarCarpetas_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnExaminarCarpetas.Click
        BrowseFolders()
    End Sub
#End Region

#Region "Abrir Archivos"
    ''' <summary>
    '''  ExaminarArchivos Event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>
    '''     Javier  30/12/2010  Modified    
    ''' </history>
    Private Sub btnExaminarArchivos_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnExaminarArchivos.Click
        BrowseFiles()
    End Sub
#End Region

#Region "NUEVO WORD"
    Private Sub Lnk1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Lnk1.Click
        NuevoWord(Nothing)
    End Sub
    Public Sub NuevoWord(ByVal doctype As DocType)
        Try
            Dim ArrayListFiles As New ArrayList
            ArrayListFiles.Add(CreateNewFile(Application.StartupPath & "\New.docx", "Word", "docx"))
            RaiseEvent AddFiles(ArrayListFiles, doctype)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region


#Region "Nuevo EXCEL"
    Private Sub Lnk2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Lnk2.Click
        NuevoExcel(Nothing)
    End Sub

    Public Sub NuevoExcel(ByVal doctype As DocType)
        Try
            Dim ArrayListFiles As New ArrayList
            Dim originalFileName As String = Path.Combine(Application.StartupPath, "New.xlsx")
            Dim newFileName As String = CreateNewFile(originalFileName, "Excel", "xlsx")
            ArrayListFiles.Add(newFileName)
            RaiseEvent AddFiles(ArrayListFiles, doctype)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "NUEVO POWERPOINT"
    Private Sub Lnk3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Lnk3.Click
        NuevoPowerpoint(Nothing)
    End Sub

    Public Sub NuevoPowerpoint(ByVal doctype As DocType)
        Try
            Dim ArrayListFiles As New ArrayList
            ArrayListFiles.Add(CreateNewFile(Application.StartupPath & "\New.pptx", "Powerpoint", "pptx"))
            RaiseEvent AddFiles(ArrayListFiles, doctype)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Insertar Imagenes Desde UnScaner..."


    ''' <summary>
    ''' ExaminarScanner Event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>
    '''     Javier  30/12/2010  Modified    
    ''' </history>
    Private Sub btnExaminarScanner_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnExaminarScanner.Click
        ScanDocuments()
    End Sub

    Private Function getFileName(ByVal fileName As String) As String
        Return fileName.Substring(0, fileName.LastIndexOf("."))
    End Function

    Private Function getNextFileName(ByVal file As System.IO.FileInfo,
    ByVal count As Int16) As String
        If lastIFileCount > 0 Then
            Return getFileName(file.Name) &
            lastIFileCount.ToString() &
            file.Extension
        Else
            Return file.Name
        End If
    End Function


    Private Shared lastIFileCount As Int16 = 0


    Private Sub IncrementeLastFileCount()
        lastIFileCount = CShort(lastIFileCount + 1)
    End Sub



    ''' Se Agrega las imagenes a la lista de inserción
    Private Sub endScan()
        Try
            Dim lista As New ArrayList()
            Dim pathDestino As String
            Dim directorio As New DirectoryInfo(Application.StartupPath &
            "\scanTemp")

            For Each item As System.IO.FileInfo In directorio.GetFiles()
                Try
                    ' Se arma el path destino...
                    pathDestino = Tools.EnvironmentUtil.GetTempDir("\IndexerTemp").FullName &
                     "\" &
                     getNextFileName(item, lastIFileCount)


                    ' Si la imagen ya existe se borra ....
                    If File.Exists(pathDestino) Then
                        File.Delete(pathDestino)
                    End If

                    lista.Add(pathDestino)

                    ' Se copia la nueva imagen...
                    File.Copy(item.FullName, pathDestino)

                Catch ex As Exception
                End Try

                ' Se borra la imagen del path origen...
                File.Delete(item.FullName)
            Next

            If lista.Count > 0 Then
                ' Se agregan las imagenes 
                ' escaneadas en la vista de inserción...
                RaiseEvent AddFiles(lista, Nothing)


                ' incrementa el numero de veces
                ' que se ejecuta este metodo
                ' para asignar nombre unico a 
                ' cada imagen...
                IncrementeLastFileCount()
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub


#End Region


    ''' <summary>
    '''     Show a window to select a document to insert in Zamba
    ''' </summary>
    ''' <history>
    '''     Javier  30/12/2010  Created     Extracted from event handler
    '''</history>
    Private Sub BrowseFiles()
        Try
            If DialogResult.OK = OpenFileDialog.ShowDialog() Then
                If OpenFileDialog.FileNames.Length = 0 Then Exit Sub

                Dim ArrayFiles As Array = OpenFileDialog.FileNames
                Dim ArrayListFiles As New ArrayList

                Dim TempDirectory As New IO.DirectoryInfo(_pathDirectory)
                If TempDirectory.Exists = False Then TempDirectory.Create()

                For Each File As String In ArrayFiles
                    Dim SourceFile As New FileInfo(File)
                    Dim NuevoFile As String = FileBusiness.GetUniqueFileName(TempDirectory.FullName & "\", SourceFile.Name)

                    SourceFile.CopyTo(NuevoFile, True)

                    ArrayListFiles.Add(NuevoFile)
                    'Libera los recursos
                    SourceFile = Nothing
                    NuevoFile = Nothing
                Next
                RaiseEvent AddFiles(ArrayListFiles, Nothing)
                'Libero recursos
                TempDirectory = Nothing
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    '''     Show a window to select a folder with documents to insert in Zamba
    ''' </summary>
    ''' <history>
    '''     Javier  30/12/2010  Created     Extracted from event handler
    '''</history>
    Private Sub BrowseFolders()
        FolderBrowserDialog.ShowDialog()
        If FolderBrowserDialog.SelectedPath.ToString.Trim = "" Then Exit Sub
        Dim Folder As New IO.DirectoryInfo(FolderBrowserDialog.SelectedPath)

        Dim ArrayListFiles As New ArrayList
        If Folder.GetFiles.Length = 0 Then Exit Sub

        Dim TempDirectory As New IO.DirectoryInfo(_pathDirectory)
        If TempDirectory.Exists = False Then TempDirectory.Create()

        For Each File As FileInfo In Folder.GetFiles
            Dim NuevoFile As String = FileBusiness.GetUniqueFileName(TempDirectory.FullName & "\", File.Name)
            Try
                File.CopyTo(NuevoFile)
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
            ArrayListFiles.Add(NuevoFile)
        Next
        RaiseEvent AddFiles(ArrayListFiles, Nothing)
    End Sub

    ''' <summary>
    '''     Show a window to scan, from a scan device, a document to insert in Zamba
    ''' </summary>
    ''' <history>
    '''     Javier  30/12/2010  Created     Extracted from event handler
    '''</history>
    Private Sub ScanDocuments()
        Try
            Shell(Application.StartupPath &
            "\Zamba.ScanGUI.exe",
            AppWinStyle.NormalFocus,
            True,
            -1)

            endScan()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub


    ''' <summary>
    '''     Execute directly one of a four ways to insert a Document, used for DoAddAsociatedDocuments
    '''     
    ''' </summary>
    ''' <history>
    '''     Javier  30/12/2010  Created     
    '''</history>
    Public Sub OpenOptionToInsert(ByVal opt As Int32)
        Select Case opt
            Case 0
                BrowseFiles()
            Case 1
                BrowseFolders()
            Case 2
                ScanDocuments()
        End Select
    End Sub

    Private Sub pnlPlantilla_Paint(sender As Object, e As PaintEventArgs) Handles pnlPlantilla.Paint

    End Sub

    Private Sub lblPlantilla_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Try
            _ucTemplatesNew.Controls.Clear()
            _ucTemplatesNew.GetDBTemplates()
            pnlPlantilla.Refresh()

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Event ShowTemplatesAdmin()
    Private Sub btnaddtemplate_Click(sender As Object, e As EventArgs) Handles btnaddtemplate.Click
        RaiseEvent ShowTemplatesAdmin()
    End Sub
End Class



