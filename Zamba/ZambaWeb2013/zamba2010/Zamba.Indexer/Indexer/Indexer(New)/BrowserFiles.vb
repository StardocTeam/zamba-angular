Imports Zamba.Core
Imports System.IO
Imports Zamba.AppBlock
Imports zamba.Viewers
Imports Zamba.Office
Imports Zamba.Office.Outlook

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

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents OpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents FolderBrowserDialog As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    '   Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnExaminarCarpetas As Zamba.AppBlock.ZButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnExaminarArchivos As Zamba.AppBlock.ZButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnExaminarScanner As Zamba.AppBlock.ZButton
    Friend WithEvents labelScan As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lbInsertarDocumentoVirtual As System.Windows.Forms.Label
    Friend WithEvents btInsertarDocumentoVirtual As Zamba.AppBlock.ZButton
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox9 As System.Windows.Forms.PictureBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BrowserFiles))
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog
        Me.FolderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.btInsertarDocumentoVirtual = New Zamba.AppBlock.ZButton
        Me.lbInsertarDocumentoVirtual = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnExaminarScanner = New Zamba.AppBlock.ZButton
        Me.labelScan = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.PictureBox9 = New System.Windows.Forms.PictureBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.btnExaminarCarpetas = New Zamba.AppBlock.ZButton
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnExaminarArchivos = New Zamba.AppBlock.ZButton
        Me.Panel7.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.Multiselect = True
        '
        'FolderBrowserDialog
        '
        Me.FolderBrowserDialog.ShowNewFolderButton = False
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
        Me.ImageList1.Images.SetKeyName(10, "NOTEL.ICO")
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 263)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(678, 290)
        Me.Panel1.TabIndex = 38
        '
        'Panel7
        '
        Me.Panel7.AutoScroll = True
        Me.Panel7.BackColor = System.Drawing.Color.White
        Me.Panel7.Controls.Add(Me.Panel4)
        Me.Panel7.Controls.Add(Me.Panel3)
        Me.Panel7.Controls.Add(Me.Label4)
        Me.Panel7.Controls.Add(Me.btnExaminarScanner)
        Me.Panel7.Controls.Add(Me.labelScan)
        Me.Panel7.Controls.Add(Me.Label1)
        Me.Panel7.Controls.Add(Me.PictureBox9)
        Me.Panel7.Controls.Add(Me.Panel2)
        Me.Panel7.Controls.Add(Me.Label5)
        Me.Panel7.Controls.Add(Me.btnExaminarCarpetas)
        Me.Panel7.Controls.Add(Me.Label3)
        Me.Panel7.Controls.Add(Me.Label2)
        Me.Panel7.Controls.Add(Me.btnExaminarArchivos)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(0, 0)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(678, 263)
        Me.Panel7.TabIndex = 32
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.btInsertarDocumentoVirtual)
        Me.Panel4.Controls.Add(Me.lbInsertarDocumentoVirtual)
        Me.Panel4.Location = New System.Drawing.Point(340, 48)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(327, 36)
        Me.Panel4.TabIndex = 44
        '
        'btInsertarDocumentoVirtual
        '
        Me.btInsertarDocumentoVirtual.BackColor = System.Drawing.Color.SteelBlue
        Me.btInsertarDocumentoVirtual.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btInsertarDocumentoVirtual.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btInsertarDocumentoVirtual.ForeColor = System.Drawing.Color.Black
        Me.btInsertarDocumentoVirtual.Location = New System.Drawing.Point(209, 3)
        Me.btInsertarDocumentoVirtual.Name = "btInsertarDocumentoVirtual"
        Me.btInsertarDocumentoVirtual.Size = New System.Drawing.Size(115, 24)
        Me.btInsertarDocumentoVirtual.TabIndex = 42
        Me.btInsertarDocumentoVirtual.Text = "Insertar"
        '
        'lbInsertarDocumentoVirtual
        '
        Me.lbInsertarDocumentoVirtual.AutoSize = True
        Me.lbInsertarDocumentoVirtual.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.lbInsertarDocumentoVirtual.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lbInsertarDocumentoVirtual.Location = New System.Drawing.Point(3, 6)
        Me.lbInsertarDocumentoVirtual.Name = "lbInsertarDocumentoVirtual"
        Me.lbInsertarDocumentoVirtual.Size = New System.Drawing.Size(132, 16)
        Me.lbInsertarDocumentoVirtual.TabIndex = 43
        Me.lbInsertarDocumentoVirtual.Text = "Insertar Formulario"
        '
        'Panel3
        '
        Me.Panel3.AutoScroll = True
        Me.Panel3.BackColor = System.Drawing.Color.White
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Location = New System.Drawing.Point(347, 136)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(300, 96)
        Me.Panel3.TabIndex = 41
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(344, 110)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(175, 16)
        Me.Label4.TabIndex = 40
        Me.Label4.Text = "Documentos de Workflow"
        '
        'btnExaminarScanner
        '
        Me.btnExaminarScanner.BackColor = System.Drawing.Color.SteelBlue
        Me.btnExaminarScanner.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnExaminarScanner.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExaminarScanner.ForeColor = System.Drawing.Color.Black
        Me.btnExaminarScanner.Location = New System.Drawing.Point(550, 14)
        Me.btnExaminarScanner.Name = "btnExaminarScanner"
        Me.btnExaminarScanner.Size = New System.Drawing.Size(115, 24)
        Me.btnExaminarScanner.TabIndex = 38
        Me.btnExaminarScanner.Text = "Examinar"
        '
        'labelScan
        '
        Me.labelScan.AutoSize = True
        Me.labelScan.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelScan.ForeColor = System.Drawing.Color.Black
        Me.labelScan.Location = New System.Drawing.Point(344, 18)
        Me.labelScan.Name = "labelScan"
        Me.labelScan.Size = New System.Drawing.Size(154, 16)
        Me.labelScan.TabIndex = 39
        Me.labelScan.Text = "Escanear Documentos"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(0, 247)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(172, 16)
        Me.Label1.TabIndex = 37
        Me.Label1.Text = "Plantillas de Documentos"
        '
        'PictureBox9
        '
        Me.PictureBox9.BackColor = System.Drawing.Color.Black
        Me.PictureBox9.Dock = System.Windows.Forms.DockStyle.Top
        Me.PictureBox9.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox9.Name = "PictureBox9"
        Me.PictureBox9.Size = New System.Drawing.Size(678, 1)
        Me.PictureBox9.TabIndex = 36
        Me.PictureBox9.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.AutoScroll = True
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Location = New System.Drawing.Point(14, 136)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(276, 96)
        Me.Panel2.TabIndex = 35
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(10, 51)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(124, 16)
        Me.Label5.TabIndex = 34
        Me.Label5.Text = "Insertar Carpetas"
        '
        'btnExaminarCarpetas
        '
        Me.btnExaminarCarpetas.BackColor = System.Drawing.Color.SteelBlue
        Me.btnExaminarCarpetas.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnExaminarCarpetas.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExaminarCarpetas.ForeColor = System.Drawing.Color.Black
        Me.btnExaminarCarpetas.Location = New System.Drawing.Point(175, 48)
        Me.btnExaminarCarpetas.Name = "btnExaminarCarpetas"
        Me.btnExaminarCarpetas.Size = New System.Drawing.Size(115, 24)
        Me.btnExaminarCarpetas.TabIndex = 33
        Me.btnExaminarCarpetas.Text = "Examinar"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(14, 110)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(154, 16)
        Me.Label3.TabIndex = 32
        Me.Label3.Text = "Documentos de Office"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(10, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(146, 16)
        Me.Label2.TabIndex = 31
        Me.Label2.Text = "Insertar Documentos"
        '
        'btnExaminarArchivos
        '
        Me.btnExaminarArchivos.BackColor = System.Drawing.Color.SteelBlue
        Me.btnExaminarArchivos.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnExaminarArchivos.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExaminarArchivos.ForeColor = System.Drawing.Color.Black
        Me.btnExaminarArchivos.Location = New System.Drawing.Point(175, 14)
        Me.btnExaminarArchivos.Name = "btnExaminarArchivos"
        Me.btnExaminarArchivos.Size = New System.Drawing.Size(115, 24)
        Me.btnExaminarArchivos.TabIndex = 30
        Me.btnExaminarArchivos.Text = "Examinar"
        '
        'BrowserFiles
        '
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel7)
        Me.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "BrowserFiles"
        Me.Size = New System.Drawing.Size(678, 553)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Eventos & Declaraciones"

    Public Event AddFiles(ByVal Files As ArrayList, ByVal DocType As DocType)
    Public Event ShowResult(ByRef Result As NewResult)

    Private _ucTemplatesNew As UCTemplatesNew
    'Private _pathDirectory As String = Membership.MembershipHelper.StartUpPath & "\IndexerTemp"

    Private WithEvents Lnk1 As System.Windows.Forms.LinkLabel
    Private WithEvents Lnk2 As System.Windows.Forms.LinkLabel
    Private WithEvents Lnk3 As System.Windows.Forms.LinkLabel
    Private WithEvents Lnk4 As System.Windows.Forms.LinkLabel
    Private WithEvents Lnk5 As System.Windows.Forms.LinkLabel

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

    Private Sub BrowserFiles_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        _ucTemplatesNew = New UCTemplatesNew
        _ucTemplatesNew.Dock = DockStyle.Fill
        Me.Panel1.Controls.Add(_ucTemplatesNew)
        RemoveHandler _ucTemplatesNew.lnkclicked, AddressOf Me.LnkClicked
        AddHandler _ucTemplatesNew.lnkclicked, AddressOf Me.LnkClicked
        AddGenericTemplates()

        If Not UserBusiness.Rights.GetUserRights(Zamba.Core.ObjectTypes.ModuleScan, Zamba.Core.RightsType.Use) Then
            btnExaminarScanner.Enabled = False
            labelScan.Enabled = False
        End If

        Panel3.Visible = Boolean.Parse(UserPreferences.getValue("UseXoml", Sections.UserPreferences, True))
        Label4.Visible = Boolean.Parse(UserPreferences.getValue("UseXoml", Sections.UserPreferences, True))
        Me.Panel4.Visible = Boolean.Parse(UserPreferences.getValue("InsertVirtualDocuments", Sections.UserPreferences, True))

    End Sub

    Private Sub LnkClicked(ByVal FullName As String)
        Dim ArrayListFiles As New ArrayList
        Dim File As New IO.FileInfo(FullName)
        ArrayListFiles.Add(CreateNewFile(File.FullName, File.Name, File.Extension.Substring(1)))
        RaiseEvent AddFiles(ArrayListFiles, Nothing)
    End Sub


#Region "TEMPLATES"
    Private Sub AddGenericTemplates()

        Lnk1 = New System.Windows.Forms.LinkLabel
        Lnk2 = New System.Windows.Forms.LinkLabel
        Lnk3 = New System.Windows.Forms.LinkLabel
        Lnk4 = New System.Windows.Forms.LinkLabel
        Lnk5 = New System.Windows.Forms.LinkLabel

        Try
            'NUEVO DOCUMENTO DE WORD
            If Boolean.Parse(UserPreferences.getValue("InsertNewWord", Sections.UserPreferences, True)) Then

                Lnk1.BackColor = Color.Transparent
                Lnk1.ImageList = Me.ImageList1
                Lnk1.ImageIndex = 2
                Lnk1.ImageAlign = Drawing.ContentAlignment.MiddleLeft
                Lnk1.TextAlign = Drawing.ContentAlignment.MiddleLeft
                Lnk1.LinkBehavior = Windows.Forms.LinkBehavior.HoverUnderline
                Lnk1.Text = "       Documento de Word"
                Dim Font As New Font("Verdana", 10)
                Lnk1.Font = Font
                Lnk1.SetBounds(10, (Me.Panel2.Controls.Count * 25) + 2, 20 * 10 + 30, 25)
                Me.Panel2.Controls.Add(Lnk1)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Try
            'NUEVO DOCUMENTO DE EXCEL
            If Boolean.Parse(UserPreferences.getValue("InsertNewExcel", Sections.UserPreferences, True)) Then
                Lnk2.BackColor = Color.Transparent
                Lnk2.ImageList = Me.ImageList1
                Lnk2.ImageIndex = 1
                Lnk2.ImageAlign = Drawing.ContentAlignment.MiddleLeft
                Lnk2.TextAlign = Drawing.ContentAlignment.MiddleLeft
                Lnk2.LinkBehavior = Windows.Forms.LinkBehavior.HoverUnderline
                Lnk2.Text = "       Documento de Excel"
                Dim Font As New Font("Verdana", 10)
                Lnk2.Font = Font
                Lnk2.SetBounds(10, (Me.Panel2.Controls.Count * 25) + 2, 20 * 10 + 30, 25)
                Me.Panel2.Controls.Add(Lnk2)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Try
            If Boolean.Parse(UserPreferences.getValue("InsertNewPowerPoint", Sections.UserPreferences, True)) Then
                'NUEVO DOCUMENTO DE POWERPOINT
                Lnk3.BackColor = Color.Transparent
                Lnk3.ImageList = Me.ImageList1
                Lnk3.ImageIndex = 3
                Lnk3.ImageAlign = Drawing.ContentAlignment.MiddleLeft
                Lnk3.TextAlign = Drawing.ContentAlignment.MiddleLeft
                Lnk3.LinkBehavior = Windows.Forms.LinkBehavior.HoverUnderline
                Lnk3.Text = "       Documento de Powerpoint"
                Dim Font As New Font("Verdana", 10)
                Lnk3.Font = Font
                Lnk3.SetBounds(10, (Me.Panel2.Controls.Count * 25) + 2, 20 * 10 + 30, 25)
                Me.Panel2.Controls.Add(Lnk3)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Try
            'NUEVO WORKFLOW
            Lnk4.BackColor = Color.Transparent
            Lnk4.ImageList = Me.ImageList1
            Lnk4.ImageIndex = 9
            Lnk4.ImageAlign = Drawing.ContentAlignment.MiddleLeft
            Lnk4.TextAlign = Drawing.ContentAlignment.MiddleLeft
            Lnk4.LinkBehavior = Windows.Forms.LinkBehavior.HoverUnderline
            Lnk4.Text = "       Documentos de WorkFlow"
            Dim Font As New Font("Verdana", 10)
            Lnk4.Font = Font
            Lnk4.SetBounds(10, (Me.Panel3.Controls.Count * 25) + 2, 20 * 10 + 30, 25)
            Me.Panel3.Controls.Add(Lnk4)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Try
            If Boolean.Parse(UserPreferences.getValue("InsertNewEmailOutlook", Sections.UserPreferences, True)) Then
                'NUEVO OUTLOOKMAIL
                Lnk5.BackColor = Color.Transparent
                Lnk5.ImageList = Me.ImageList1
                Lnk5.ImageIndex = 10
                Lnk5.ImageAlign = Drawing.ContentAlignment.MiddleLeft
                Lnk5.TextAlign = Drawing.ContentAlignment.MiddleLeft
                Lnk5.LinkBehavior = Windows.Forms.LinkBehavior.HoverUnderline
                Lnk5.Text = "       E-mail de Outlook"
                Dim Font As New Font("Verdana", 10)
                Lnk5.Font = Font
                Lnk5.SetBounds(10, (Me.Panel2.Controls.Count * 25) + 2, 20 * 10 + 30, 25)
                Me.Panel2.Controls.Add(Lnk5)
            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub

#End Region

    Private Function CreateNewFile(ByVal Original As String, ByVal Nombre As String, ByVal Extension As String) As String
        Dim File As New IO.FileInfo(Original)
        If File.Exists = False Then File.Create()

        'Dim FlagExists As Boolean = True
        Dim Number As Int32 = 0
        Dim NuevoFile As IO.FileInfo
        Try
            NuevoFile = New IO.FileInfo(FileBusiness.GetUniqueFileName(_pathDirectory & "\", Nombre, "." & Extension))
            'Try

            '    Do Until FlagExists = False

            '    If NuevoFile.Exists = True Then
            '        Number += 1
            '    Else
            '        FlagExists = False
            '    End If
            'Loop
            'Catch ex As Exception
            '    Zamba.Core.ZClass.raiseerror(ex)
            'End Try
            'Try
            File.CopyTo(NuevoFile.FullName, False)
            Dim Path As String = NuevoFile.FullName
            Return Path
            'Catch ex As Exception
            '    Zamba.Core.ZClass.raiseerror(ex)
            'End Try
        Catch ex As Exception
        Finally
            File = Nothing
            NuevoFile = Nothing
        End Try

        Return String.Empty
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
    Private Sub btnExaminarCarpetas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExaminarCarpetas.Click
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
    Private Sub btnExaminarArchivos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExaminarArchivos.Click
        BrowseFiles()
    End Sub
#End Region

#Region "NUEVO WORD"
    Private Sub Lnk1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Lnk1.Click
        NuevoWord(Nothing)
    End Sub
    Public Sub NuevoWord(ByVal doctype As DocType)
        Try
            Dim ArrayListFiles As New ArrayList
            ArrayListFiles.Add(CreateNewFile(Membership.MembershipHelper.StartUpPath & "\New.doc", "Word....", "doc"))
            RaiseEvent AddFiles(ArrayListFiles, doctype)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "NUEVO OUTLOOKMAIL"
    Private Sub Lnk5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Lnk5.Click
        NuevoOutlookMail(Nothing)
    End Sub
    Public Sub NuevoOutlookMail(ByVal doctype As DocType)
        Try
            Dim NuevoFile As IO.FileInfo = New IO.FileInfo(FileBusiness.GetUniqueFileName(_pathDirectory & "\", "OutlookMail...", ".msg"))

            If SharedOutlook.GetOutlook().GetNewMailItem(NuevoFile.FullName) Then
                Dim ArrayListFiles As New ArrayList
                ArrayListFiles.Add(NuevoFile.FullName)
                RaiseEvent AddFiles(ArrayListFiles, doctype)
            End If


        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub


#End Region

#Region "Nuevo EXCEL"
    Private Sub Lnk2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Lnk2.Click
        NuevoExcel(Nothing)
    End Sub

    Public Sub NuevoExcel(ByVal doctype As DocType)
        Try
            Dim ArrayListFiles As New ArrayList
            ArrayListFiles.Add(CreateNewFile(Membership.MembershipHelper.StartUpPath & "\New.xls", "Excel....", "xls"))
            RaiseEvent AddFiles(ArrayListFiles, doctype)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "NUEVO POWERPOINT"
    Private Sub Lnk3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Lnk3.Click
        NuevoPowerpoint(Nothing)
    End Sub

    Public Sub NuevoPowerpoint(ByVal doctype As DocType)
        Try
            Dim ArrayListFiles As New ArrayList
            ArrayListFiles.Add(CreateNewFile(Membership.MembershipHelper.StartUpPath & "\New.ppt", "Powerpoint....", "ppt"))
            RaiseEvent AddFiles(ArrayListFiles, doctype)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "NUEVO WORKFLOW"
    Private Sub Lnk4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Lnk4.Click
        NuevoWorkFlow()
    End Sub

    Public Sub NuevoWorkFlow()
        Try
            Dim ArrayListFiles As New ArrayList
            ArrayListFiles.Add(CreateNewFile(Membership.MembershipHelper.StartUpPath & "\New.xoml", "Workflow....", "xoml"))
            RaiseEvent AddFiles(ArrayListFiles, Nothing)
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
    Private Sub btnExaminarScanner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExaminarScanner.Click
        ScanDocuments()
    End Sub

    Private Function getFileName(ByVal fileName As String) As String
        Return fileName.Substring(0, fileName.LastIndexOf("."))
    End Function

    Private Function getNextFileName(ByVal file As System.IO.FileInfo, _
    ByVal count As Int16) As String
        If lastIFileCount > 0 Then
            Return Me.getFileName(file.Name) & _
            lastIFileCount.ToString() & _
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
            Dim directorio As New DirectoryInfo(Membership.MembershipHelper.StartUpPath & _
            "\scanTemp")

            For Each item As System.IO.FileInfo In directorio.GetFiles()
                Try
                    ' Se arma el path destino...
                    pathDestino = Tools.EnvironmentUtil.GetTempDir("\IndexerTemp").FullName & _
                     "\" & _
                     Me.getNextFileName(item, lastIFileCount)


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
                Me.IncrementeLastFileCount()
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub


#End Region

    ''' <summary>
    ''' InsertarDocumentoVirtual Event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>
    '''     Javier  30/12/2010  Modified    
    ''' </history>
    Private Sub btInsertarDocumentoVirtual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btInsertarDocumentoVirtual.Click
        InsertForms()
    End Sub

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
                    Dim SourceFile As New IO.FileInfo(File)
                    Dim NuevoFile As String = FileBusiness.GetUniqueFileName(TempDirectory.FullName & "\", SourceFile.Name)
                    Try
                        SourceFile.CopyTo(NuevoFile, True)
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
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

        For Each File As IO.FileInfo In Folder.GetFiles
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
            Shell(Membership.MembershipHelper.StartUpPath & _
            "\Zamba.ScanGUI.exe", _
            AppWinStyle.NormalFocus, _
            True, _
            -1)

            Me.endScan()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    '''     Show a window to select a Form to insert in Zamba
    ''' </summary>
    ''' <history>
    '''     Javier  30/12/2010  Created    Extracted from event handler
    '''</history>
    Private Sub InsertForms()

        Try
            Dim frm As New VirtualDocumentSelector
            frm.ShowDialog()

            Select Case frm.DialogResult

                Case DialogResult.OK, DialogResult.Yes
                    Dim Result As NewResult = frm.GetInsertedDocument()
                    '     Result.HandleModule(ResultActions.MostrarResult, Result, Nothing)

                    '[Ezequiel] Se comento la que sigue abajo ya que a causa de la misma cuando insertaba un formulario los
                    'lo insertaba 2 veces y si estaba asosiado con un WF hacia lo mismo.

                    RaiseEvent ShowResult(Result)
                Case Else
                    Exit Sub
            End Select
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
                Me.BrowseFiles()
            Case 1
                Me.BrowseFolders()
            Case 2
                Me.ScanDocuments()
            Case 3
                Me.InsertForms()
        End Select
    End Sub
End Class



