'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'USER CONTROL QUE SE ENCARGA DE MOSTRAR UN DOCUMENTO INDEPENDIOENTE DE LA EXTENSIÓN   '
'QUE TENGA CON LA APLICACIÓN CORRESPONDIENTE                                          '
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Imports Microsoft.Office.Interop
Imports Microsoft.Win32
Imports Zamba.AppBlock
Imports Zamba.Core
Imports Zamba.Office
Imports System.Runtime.InteropServices

Public Class WebBrowser
    Inherits ZControl


#Region " Windows Form Designer generated code "

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents AxWebBrowser1 As System.Windows.Forms.WebBrowser
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        AxWebBrowser1 = New System.Windows.Forms.WebBrowser()
        SuspendLayout()
        '
        'AxWebBrowser1
        '
        AxWebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        AxWebBrowser1.Location = New System.Drawing.Point(0, 0)
        AxWebBrowser1.Name = "AxWebBrowser1"
        AxWebBrowser1.Size = New System.Drawing.Size(560, 504)
        AxWebBrowser1.TabIndex = 0
        AxWebBrowser1.Url = New System.Uri("https://docs.google.com/document/d/1qVetyXq5IfNR_4gVkCR4GkA3_4lizL3A2WoFVfG0WUY/p" &
        "ub?embedded=true", System.UriKind.Absolute)
        '
        'WebBrowser
        '
        Controls.Add(AxWebBrowser1)
        Name = "WebBrowser"
        Size = New System.Drawing.Size(560, 504)
        ResumeLayout(False)

    End Sub

#End Region

    Private _file As String
    Private _result As Result
    Private _magViewer As Object
    Private _excelBrowser As Zamba.EmbeddedExcel.ExcelBrowser
    Private _excelcatcher As ExcelCatcher.ExcelCatcher
    Private _document As Object
    Private _showBars As Boolean = True
    Private _primeraVez As Boolean
    Private flagAutomaticNewVersion As Boolean = False


    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        Try
            InitializeComponent()
        Catch ex As Exception
            MessageBox.Show("Ocurrio un error al Visualizar el objeto " & ex.ToString, "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ZClass.raiseerror(ex)
        End Try
        'Add any initialization after the InitializeComponent() call
    End Sub

#Region "Events"
    Delegate Sub NB()
    Public Event closeBrowser()
    Public Event DocumentReady()
    Public Event WebBrowserError(ByVal Ex As Exception)
    Public Event eAutomaticNewVersion(ByVal _result As Result, ByVal newResultPath As String)
    'Private Sub AxWebBrowser2_NavigateComplete2(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_NavigateComplete2Event) Handles AxWebBrowser1.NavigateComplete2

    'End Sub
    'Private Sub AxWebBrowser2_DocumentComplete(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEvent) Handles AxWebBrowser1.DocumentComplete
    '    Try
    '        '   e.pDisp.document.all("zamba").value = "martin"
    '        'AsignValues(e)
    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Private Sub AxWebBrowser2_NewWindow2(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_NewWindow2Event) Handles AxWebBrowser1.NewWindow2
    'End Sub
    'Private Sub AxWebBrowser2_FileDownload(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_FileDownloadEvent) Handles AxWebBrowser1.FileDownload
    'End Sub

    'Private Sub AxWebBrowser1_BeforeNavigate2(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_BeforeNavigate2Event) Handles AxWebBrowser1.BeforeNavigate2
    '    MsgBox("Beforenavigate")
    'End Sub
    'Private Sub AxWebBrowser1_CausesValidationChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles AxWebBrowser1.CausesValidationChanged
    '    MsgBox("CausesValidationChanged")
    'End Sub
    'Private Sub AxWebBrowser1_ClientToHostWindow(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_ClientToHostWindowEvent) Handles AxWebBrowser1.ClientToHostWindow
    '    MsgBox("ClientToHostWindow")
    'End Sub
    'Private Sub AxWebBrowser1_CommandStateChange(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_CommandStateChangeEvent) Handles AxWebBrowser1.CommandStateChange
    '    MsgBox("CommandStateChange")
    'End Sub
    'Private Sub AxWebBrowser1_DownloadBegin(ByVal sender As Object, ByVal e As System.EventArgs) Handles AxWebBrowser1.DownloadBegin
    '    MsgBox("DownloadBegin")
    'End Sub
    'Private Sub AxWebBrowser1_DownloadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles AxWebBrowser1.DownloadComplete
    '    MsgBox("DownloadComplete")
    'End Sub
    'Private Sub AxWebBrowser1_OnMenuBar(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_OnMenuBarEvent) Handles AxWebBrowser1.OnMenuBar
    '    MsgBox("OnMenuBar")
    'End Sub
    'Private Sub AxWebBrowser1_OnStatusBar(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_OnStatusBarEvent) Handles AxWebBrowser1.OnStatusBar
    '    MsgBox("OnStatusBar")
    'End Sub
    'Private Sub AxWebBrowser1_OnToolBar(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_OnToolBarEvent) Handles AxWebBrowser1.OnToolBar
    '    MsgBox("OnToolBar")
    'End Sub
    'Private Sub AxWebBrowser1_PrintTemplateInstantiation(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_PrintTemplateInstantiationEvent) Handles AxWebBrowser1.PrintTemplateInstantiation
    '    MsgBox("PrintTemplateInstantiation")
    'End Sub
    'Private Sub AxWebBrowser1_PrintTemplateTeardown(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_PrintTemplateTeardownEvent) Handles AxWebBrowser1.PrintTemplateTeardown
    '    MsgBox("PrintTemplateTeardown")
    'End Sub
    'Private Sub AxWebBrowser1_PrivacyImpactedStateChange(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_PrivacyImpactedStateChangeEvent) Handles AxWebBrowser1.PrivacyImpactedStateChange
    '    MsgBox("PrivacyImpactedStateChange")
    'End Sub
    ''Private Sub AxWebBrowser1_ProgressChange(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_ProgressChangeEvent) Handles AxWebBrowser1.ProgressChange
    ''    MsgBox("ProgressChange")
    ''End Sub
    'Private Sub AxWebBrowser1_PropertyChange(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_PropertyChangeEvent) Handles AxWebBrowser1.PropertyChange
    '    MsgBox("PropertyChange")
    'End Sub
    'Private Sub AxWebBrowser1_UpdatePageStatus(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_UpdatePageStatusEvent) Handles AxWebBrowser1.UpdatePageStatus
    '    MsgBox("UpdatePageStatus")
    'End Sub
    'Private Sub AxWebBrowser1_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles AxWebBrowser1.Validated
    '    MsgBox("Validated")
    'End Sub
    'Private Sub AxWebBrowser1_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles AxWebBrowser1.Validating
    '    MsgBox("Validating")
    'End Sub
    'Private Sub AxWebBrowser1_WindowClosing(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_WindowClosingEvent) Handles AxWebBrowser1.WindowClosing
    '    MsgBox("WindowClosing")
    'End Sub
    'Private Sub WebForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '    MsgBox("Load")
    'End Sub
    'Private Sub AxWebBrowser1_NavigateError(ByVal sender As Object, ByVal e As AxSHDocVw.DWebBrowserEvents2_NavigateErrorEvent) Handles AxWebBrowser1.NavigateError
    '    'MsgBox("Debug: Error al Termino de Abrir Internamente")
    '    RaiseEvent WebBrowserError(New Exception("Error de Navegacion" & e.statusCode))
    'End Sub
    'Private Sub AxWebBrowser1_OnQuit(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AxWebBrowser1.OnQuit
    '    MsgBox("OnQuit")
    'End Sub
    'Public Sub SaveDocOnZamba()

    'End Sub
#End Region

    'METODO QUE MUESTRA EL DOCUMENTO
    'Dim ISReadOnly As Boolean = False
    ' Dim DocId As Int64

    Public Function TraerDoc() As Object
        If Not IsNothing(AxWebBrowser1.ActiveXInstance.Document) Then
            Return AxWebBrowser1.ActiveXInstance.Document
        End If
    End Function

    Public Sub ShowToolbars()
        If _result.IsOffice AndAlso _result.IsMAG = False Then
            Try
                If _result.IsExcel Then
                    If Not IsNothing(_excelBrowser) Then
                        _showBars = Not (_showBars)
                        _excelBrowser.ShowToolBar(_showBars)
                    End If
                ElseIf _result.IsWord Then
                    If Not IsNothing(AxWebBrowser1.ActiveXInstance.Document) Then
                        _showBars = Not (_showBars)
                        AxWebBrowser1.ActiveXInstance.Document.save()
                        AxWebBrowser1.Navigate(AxWebBrowser1.ActiveXInstance.Document.fullname)
                    End If
                ElseIf _result.IsPowerpoint Then
                    If Not IsNothing(AxWebBrowser1.ActiveXInstance.Document) Then
                        _showBars = Not (_showBars)
                        AxWebBrowser1.ActiveXInstance.Document.saved = True
                        AxWebBrowser1.Navigate(AxWebBrowser1.ActiveXInstance.Document.fullname)
                    End If
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
                RaiseEvent WebBrowserError(ex)
            End Try
        End If
    End Sub

    Public Sub ShowToolBars(ByVal status As Boolean)
        _showBars = Not status
        ShowToolbars()
    End Sub

    ' Dim Opens As Matrix.opentype
    '  Dim oMatrix As Matrix
    'Dim WithLocalCopy As Boolean = True
#Region "BROWSER AND CACHE"

    'Public Sub ShowDocument(ByVal DocFile As String)

    '    DocId = DocId
    '    Dim fi As IO.FileInfo = New IO.FileInfo(DocFile)

    '    'Try
    '    '    If IsNothing(oMatrix) Then oMatrix = New Matrix
    '    'Catch ex As Exception
    '    '   zclass.raiseerror(ex)
    '    'End Try

    '    'Try
    '    '    Opens = oMatrix.getOpenType(fi.Extension, ISReadOnly)
    '    '    'MsgBox("Debug: Modo de Apertura = " & Opens.ToString)
    '    'Catch ex As Exception
    '    '    'no esta en la Matrix
    '    '    'MsgBox("Debug: No se encontro en la Matrix, entonces asigno interno sin copia")
    '    '    '  zclass.raiseerror(ex)
    '    '    Opens = Matrix.opentype.Interno
    '    'Finally
    '    '    oMatrix.Dispose()
    '    '    oMatrix = Nothing
    '    'End Try

    '    'Try
    '    '    If Opens = Matrix.opentype.CopyInt OrElse Opens = Matrix.opentype.CopyExt OrElse Opens = Matrix.opentype.CopyOffice Then
    '    '        'Hace Copia
    '    '        'MsgBox("Debug: Se hace copia")
    '    '        WithLocalCopy = True
    '    '    Else
    '    '        'MsgBox("Debug: No se hace copia")
    '    '        WithLocalCopy = False
    '    '    End If
    '    'Catch
    '    '    WithLocalCopy = False
    '    'End Try

    '    Try

    '        'MsgBox("Debug: Comienzo la copia")
    '        Dim dir As New IO.DirectoryInfo(Application.StartupPath & "\OfficeTemp")
    '        If dir.Exists = False Then dir.Create()
    '        Try
    '            Dim FTemp As New IO.FileInfo(dir.FullName & "\" & fi.Name)
    '            fi.CopyTo(FTemp.FullName, True)
    '            FTemp.Attributes = IO.FileAttributes.Normal
    '            _file = FTemp.FullName
    '        Catch ex As Exception
    '           zclass.raiseerror(ex)
    '            _file = fi.FullName
    '        Finally
    '            dir = Nothing
    '        End Try
    '    Catch ex As Exception
    '       zclass.raiseerror(ex)
    '        _file = fi.FullName
    '    End Try
    '    fi = Nothing
    '    Try
    '        'Chequeo si se Abre adentro o afuera del Web Browser
    '        If Result.IsMAG Then
    '            OpenMag()
    '            Exit Sub
    '        End If
    '        '            Select Case Opens
    '        '               Case Matrix.opentype.Externo, Matrix.opentype.CopyExt
    '        '          NavigateExternal(False)
    '        '             Case Matrix.opentype.Interno, Matrix.opentype.CopyInt
    '        Navigate()
    '        '            Case Matrix.opentype.CopyOffice, Matrix.opentype.Office
    '        '       NavigateExternal(True)
    '        '      End Select
    '    Catch ex As Exception
    '       zclass.raiseerror(ex)
    '        RaiseEvent WebBrowserError(ex)
    '    Catch
    '        RaiseEvent WebBrowserError(New Exception("Error Desconocido"))
    '    End Try
    'End Sub

    Public Sub ShowDocument(ByVal file As String)
        Try
            Navigate(file)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            RaiseEvent WebBrowserError(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Gets directory to save data
    ''' </summary>
    ''' <param name="dire"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Ezequiel] 15/05/09 Created.
    ''' </history>
    Private Function GetTempDir(ByVal dire As String) As IO.DirectoryInfo
        Dim Dir As IO.DirectoryInfo
        Try
            Dir = New IO.DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Zamba Software" & dire)
            If Dir.Exists = False Then
                Dir.Create()
            End If
        Catch
            Dir = New IO.DirectoryInfo(System.Windows.Forms.Application.StartupPath & dire)
            If Dir.Exists = False Then
                Dir.Create()
            End If
        End Try
        Return Dir
    End Function

    ''' <summary>
    ''' [Sebastian] 22-06-2009 COMMENT Muestra los documentos en la pantalla de insersion de zamba
    ''' </summary>
    ''' <param name="result"></param>
    ''' <param name="localpath"></param>
    ''' <param name="disableTempGeneration"> Desabilita la generación de temporales en OfficeTemp, debido a que se genero el temporal en UCDocumentViewer2</param>
    ''' <remarks></remarks>
    Public Sub ShowDocument(ByRef result As Result, ByVal localPath As String)
        _result = result
        flagAutomaticNewVersion = UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.AutomaticVersion, _result.DocType.ID)
        _file = localPath

        If Not String.IsNullOrEmpty(_file) OrElse _file.IndexOf("aspx", StringComparison.CurrentCultureIgnoreCase) = -1 Then
            Dim FTemp As IO.FileInfo = Nothing
            Dim dir As IO.DirectoryInfo
            Dim errorMessage As String = String.Empty

            Try

                'Se genera la ruta temporal del documento
                dir = GetTempDir("\OfficeTemp")
                If dir.Exists = False Then dir.Create()

                FTemp = New IO.FileInfo(dir.FullName & "\" & IO.Path.GetFileName(result.FullPath))

                Try

                    'If FTemp.FullName.Contains("0.pdf") Then
                    '    System.IO.File.Delete(FTemp.FullName)
                    'End If

                    If FTemp.Name.Equals("0.pdf") Then
                        IO.File.Delete(FTemp.FullName)
                    End If

                    If Not FTemp.Exists Then
                        Results_Business.CopyFileToTemp(result, result.FullPath, FTemp.FullName)
                    End If

                    FTemp.Attributes = IO.FileAttributes.Normal
                Catch ex As IO.FileNotFoundException
                    ZClass.raiseerror(ex)
                    errorMessage = "Documento no encontrado. Verifique tener acceso a los volúmenes de Zamba y que el archivo exista."
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                    errorMessage = "Error al copiar el documento. Verifique tener acceso a los volúmenes de Zamba y que el archivo exista. Verifique tener acceso a los temporales de Zamba."
                End Try

                _file = FTemp.FullName

                If result.FullPath.ToUpper.EndsWith(".HTML") Or result.FullPath.ToUpper.EndsWith(".HTM") Then
                    Results_Business.CopySubDirAndFilesBrowser(dir.FullName, result.FullPath.Remove(result.FullPath.LastIndexOf("\")), result.FullPath.Remove(result.FullPath.LastIndexOf("\")))
                End If

            Catch ex As Exception
                ZClass.raiseerror(ex)
                errorMessage = "Error al copiar el documento. Verifique tener acceso a los volúmenes de Zamba y que el archivo exista. Verifique tener acceso a los temporales de Zamba."
            Finally
                'Libera recursos tomados
                If Not IsNothing(FTemp) Then FTemp = Nothing
                If Not IsNothing(dir) Then dir = Nothing
            End Try

            If String.IsNullOrEmpty(errorMessage) Then
                Try
                    If result.IsMAG Then
                        OpenMag()
                        Exit Sub
                    ElseIf result.IsExcel Then
                        Try
                            If Boolean.Parse(UserPreferences.getValue("ExcelPorFuera", UPSections.UserPreferences, "False")) = True Then
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Utilizando ExcelCatcher")
                                'Todo llamar al excelcatcher
                                If _excelcatcher Is Nothing Then
                                    _excelcatcher = New ExcelCatcher.ExcelCatcher(_file, Boolean.Parse(UserPreferences.getValue("ExcelTrace", UPSections.UserPreferences, "False")))
                                End If
                                _excelcatcher.openExcel()
                                RemoveHandler _excelcatcher.ExcelClosed, AddressOf CloseWebBrowserHandler
                                AddHandler _excelcatcher.ExcelClosed, AddressOf CloseWebBrowserHandler

                                Dim message As String = "El documento se ha abierto por fuera de Zamba Software." & vbCrLf &
                                    "Este comportamiento es determinado por la configuración de apertura de documentos Excel desde Zamba." & vbCrLf &
                                    "En caso de no poder visualizarlo, por favor consulte con el área de sistemas de su empresa."
                                ShowMessage(message, False)
                            Else
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Utilizando ExcelBrowser")
                                _excelBrowser = New Zamba.EmbeddedExcel.ExcelBrowser
                                _excelBrowser.Dock = DockStyle.Fill
                                Controls.Add(_excelBrowser)
                                _excelBrowser.BringToFront()
                                _excelBrowser.ShowToolBar(True)
                                _excelBrowser.ShowFile(_file)
                            End If
                        Catch
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "navigate normal (por falla de excelcatcher o excelbrowser)")
                            Navigate()
                        End Try
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "navigate normal")
                        Navigate()
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                    RaiseEvent WebBrowserError(ex)
                End Try
            Else
                AxWebBrowser1.Navigate("about:blank")

                ShowMessage(errorMessage, True)
            End If
        Else
            If Not _file.IndexOf("aspx", StringComparison.CurrentCultureIgnoreCase) = -1 Then
                Try
                    Navigate()
                Catch ex As Exception
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Navego sin copiar porque es un aspx")
                    ZClass.raiseerror(ex)
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' Muestra un mensaje de notificación al usuario en reemplazo del documento
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ShowMessage(ByVal message As String, ByVal clearControls As Boolean)
        'Crea el mensaje
        Dim lblNonPreview As New Label()
        lblNonPreview.Dock = DockStyle.Fill
        lblNonPreview.Text = message

        'Agranda la letra para ocupar mas espacio y sea mas legible
        Dim font As New Font(lblNonPreview.Font.FontFamily, 9)
        lblNonPreview.Font = font

        'Agrega el mensaje al control
        'ML Se comenta el borrado de controles y la destruccion del webbrowser, ya que si el PDF se abre por dentro, no se ve.
        'se agrega en el status change, que si la navegacion fue cancelada, ahi si se quita.

        If clearControls = True Then
            If Controls.Count > 0 Then Controls.Clear()
        End If

        Controls.Add(lblNonPreview)
        lblNonPreview.Width = Width
        lblNonPreview.Height = Height

        '       AxWebBrowser1 = Nothing
    End Sub


#End Region


    Private Sub CloseWebBrowser()
        RaiseEvent closeBrowser()
    End Sub

    Private Sub CloseWebBrowserHandler()
        Dispose()
        Dim T1 As New Threading.Thread(AddressOf CloseWebBrowser)
        T1.Start(True)
    End Sub

    Delegate Sub eCloseDocumentDelegate()

    Private Sub eCloseDocumentHandler1()
        Dim D1 As New eCloseDocumentDelegate(AddressOf CloseWebBrowser)
        Invoke(D1)
    End Sub

    Private Sub OpenMag()
        Try
            AxWebBrowser1.Visible = False
            'Dim PW As Reflection.Assembly = Reflection.Assembly.LoadFrom("Zamba.PowerWeb.dll")

            'MagViewer = Activator.CreateInstanceFrom("Zamba.PowerWeb", "UCPowerWeb")
            'todo: pruebo con el activator para evitar tener la referencia directa al proyecto powerweb que tiene dll externas que no son nuestras.
            'martin
            ' MagViewer = New UCPowerWeb
            'Modifique la manera en que levanta el reflection - MC
            Dim tt As System.Reflection.Assembly = System.Reflection.Assembly.LoadFile(Application.StartupPath & "\Zamba.PowerWeb.dll")
            Dim t As System.Type = tt.GetType("Zamba.PowerWeb.UCPowerWeb", True, True)

            _magViewer = Activator.CreateInstance(t)
            _magViewer.File = _file

            Controls.Add(_magViewer)

            _magViewer.Dock = DockStyle.Fill
            RaiseEvent DocumentReady()
        Catch ex As NullReferenceException
            ZClass.raiseerror(ex)
        Catch ex As UnauthorizedAccessException

            ZClass.raiseerror(ex)
            RaiseEvent WebBrowserError(ex)
        Catch ex As OutOfMemoryException
            ZClass.raiseerror(ex)
            RaiseEvent WebBrowserError(ex)
        Catch ex As StackOverflowException
            ZClass.raiseerror(ex)
        Catch ex As AppDomainUnloadedException
            ZClass.raiseerror(ex)
        Catch ex As ApplicationException
            ZClass.raiseerror(ex)
        Catch ex As ArgumentNullException
            ZClass.raiseerror(ex)
        Catch ex As ArgumentException
            ZClass.raiseerror(ex)
        Catch ex As ArithmeticException
            ZClass.raiseerror(ex)
        Catch ex As BadImageFormatException
            ZClass.raiseerror(ex)
        Catch ex As InvalidOperationException
            ZClass.raiseerror(ex)
        Catch ex As InvalidProgramException
            ZClass.raiseerror(ex)
        Catch ex As SystemException
            ZClass.raiseerror(ex)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            RaiseEvent WebBrowserError(ex)
        End Try
    End Sub

    Private Sub Navigate()
        Try
            If IsNothing(AxWebBrowser1) Then
                AxWebBrowser1 = New System.Windows.Forms.WebBrowser
                AxWebBrowser1.Dock = DockStyle.Fill
                AxWebBrowser1.Enabled = True
                Controls.Add(AxWebBrowser1)
            End If

            If _result.IsPDF And Boolean.Parse(UserPreferences.getValue("PDFPorFuera", UPSections.UserPreferences, False)) = True Then
                AxWebBrowser1.Navigate("about:blank")
                Dim proc As New System.Diagnostics.Process()
                proc.Start(_file)

                Dim message As String = "El documento se ha abierto por fuera de Zamba Software." & vbCrLf &
                "Este comportamiento es determinado por la configuración de su sistema operativo." & vbCrLf &
                "En caso de no poder visualizarlo, por favor consulte con el área de sistemas de su empresa."
                ShowMessage(message, True)
            Else
                AxWebBrowser1.Navigate(_file)

                If AxWebBrowser1.Document Is Nothing Then
                    Dim message As String = "El documento se ha abierto por fuera de Zamba Software." & vbCrLf &
                        "Este comportamiento es determinado por la configuración de su sistema operativo." & vbCrLf &
                        "En caso de no poder visualizarlo, por favor consulte con el área de sistemas de su empresa."
                    ShowMessage(message, False)
                End If
            End If

            _primeraVez = True
        Catch ex As System.ExecutionEngineException
            ZClass.raiseerror(ex)
            RaiseEvent WebBrowserError(ex)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            RaiseEvent WebBrowserError(ex)
        End Try
    End Sub

    Private Sub Navigate(ByVal File As String)
        Try
            If IsNothing(AxWebBrowser1) Then
                AxWebBrowser1 = New System.Windows.Forms.WebBrowser
                AxWebBrowser1.Dock = DockStyle.Fill
                AxWebBrowser1.Enabled = True
                Controls.Add(AxWebBrowser1)
            End If
            AxWebBrowser1.Navigate(File)
        Catch ex As System.ExecutionEngineException
            ZClass.raiseerror(ex)
            RaiseEvent WebBrowserError(ex)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            RaiseEvent WebBrowserError(ex)
        End Try
    End Sub
    'Private Shared Sub NavigateExternal(ByVal Office As Boolean)
    '    If Office = True Then
    '        Try
    '            'MsgBox("Debug: Abrir con office")
    '            Zoffice = New Zoffice
    '            Zoffice.OpenDocument(_file, ISReadOnly)
    '            RaiseEvent DocumentReady()
    '            'MsgBox("Debug: Termino de Abrir con Office")
    '        Catch ex As Exception
    '            'MsgBox("Debug: Error al Abrir con office")
    '            RaiseEvent WebBrowserError(ex)
    '        Catch
    '            RaiseEvent WebBrowserError(New Exception("Error desconocido"))
    '        End Try
    '    Else
    '        'MsgBox("Debug: Abrir Externamente")
    '        Shell("EXPLORER.EXE " & _file, AppWinStyle.NormalFocus, False)
    '        'MsgBox("Debug: Termino de Abrir Externamente")
    '        RaiseEvent DocumentReady()
    '    End If
    '    ZTrace.WriteLineIf(ZTrace.IsInfo, "Método no implementado")
    'End Sub

#Region "SaveDocument"
    'METODO QUE GUARDA EL DOCUMENTO
    Public Sub SaveDocument()
        Try
            If _result.IsExcel Then
                'todo marcelo ferrer: que guarde el excelbrowser el documento
            Else
                If IsNothing(_document) = False Then
                    _document.save()
                End If
            End If
        Catch ex As MissingMemberException
            'Este error se produce al abrir documentos por fuera, 
            'cuando no debería, y cerrar el tab del documento
        Catch ex As System.Runtime.InteropServices.COMException
            'No Raisear el error.
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

#End Region

#Region "Close Document"
    'Private Sub deleteOfficeDocuments()
    '    Try
    '        Dim dir As IO.DirectoryInfo = New IO.DirectoryInfo(System.Windows.Forms.Application.StartupPath & "\officetemp")
    '        If dir.Exists Then
    '            Dim files() As IO.FileInfo = dir.GetFiles()
    '            Dim l As Integer = files.Length - 1
    '            Dim i As Integer
    '            Dim fi As IO.FileInfo
    '            For i = 0 To l
    '                fi = files(i)
    '                Try
    '                    fi.Delete()
    '                Catch ex As Exception
    '                End Try
    '            Next
    '            fi = Nothing
    '            dir = Nothing
    '            files = Nothing
    '        End If
    '    Catch ex As Exception
    '       zclass.raiseerror(ex)
    '    End Try
    'End Sub
    'METODOS QUE CIERRA EL DOCUMENTO Y LIBERA MEMORIA

    Public Sub CloseOfficeDocument()
        Try
            If Not IsNothing(_document) Then
                Try
                    If _document.Saved Then
                        If flagAutomaticNewVersion Then
                            RaiseEvent eAutomaticNewVersion(_result, _file)
                        End If
                    Else
                        'Pongo la propiedad en saved sino no cierra el word
                        _document.Saved = True
                    End If
                    If Not IsNothing(_document) Then
                        _document.Close()
                    End If
                Catch exMiss As MissingMemberException
                Catch comEx As COMException
                    'No se raisea la exception, ya que la misma depende de la version de office que se este utilizando
                Catch ex2 As Exception
                End Try
            ElseIf _result.IsExcel AndAlso Boolean.Parse(UserPreferences.getValue("ExcelPorFuera", UPSections.UserPreferences, False)) Then
                'Todo llamar al excelcatcher
                If Not IsNothing(_excelcatcher) Then
                    _excelcatcher.closeExcel()
                End If
            ElseIf Not IsNothing(AxWebBrowser1) Then
                Try
                    If Not IsNothing(AxWebBrowser1.ActiveXInstance.Document) Then
                        AxWebBrowser1.ActiveXInstance.Document.close()
                    End If
                Catch exMiss As MissingMemberException
                Catch comEx As COMException
                Catch ex2 As Exception
                    ZClass.raiseerror(ex2)
                End Try
            ElseIf Controls.Count = 1 AndAlso Controls(0).GetType Is GetType(Label) Then
                DirectCast(Controls(0), Label).Dispose()
                Controls.Clear()
            End If

            If Not IsNothing(_excelBrowser) Then
                _excelBrowser.Dispose()
                _excelBrowser = Nothing
            End If
        Catch exMiss As MissingMemberException
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Cierra el WebBrowser liberando la memoria
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Tomás] - 05/05/2009 - Modified - Se modifica para cerrar documentos PDF liberando correctamente la memoria.
    ''' </history>
    Public Sub CloseWebBrowser(ByVal deleteTemp As Boolean, Optional ByVal DisableAutomaticVersion As Boolean = False)

        Try
            If Not IsNothing(_result) Then
                If _result.IsPDF And Not IsNothing(AxWebBrowser1) Then
                    If Not IsNothing(AxWebBrowser1.ActiveXInstance.Document) Then
                        Marshal.ReleaseComObject(AxWebBrowser1.ActiveXInstance.Document)
                    End If
                ElseIf _result.IsOffice OrElse _result.IsRTF Then
                    'Si es un documento Office cierro el documento con un metodo específico.
                    CloseOfficeDocument()
                ElseIf _result.IsOpenOffice Then
                    '(pablo) - guardo en el servidor los cambios realizados al documento
                    Dim OpenOfficeFileTempPath, OpenOfficeFileServerPath As String

                    OpenOfficeFileTempPath = Tools.EnvironmentUtil.GetTempDir(String.Empty).FullName & "\OfficeTemp\" & _result.Doc_File
                    OpenOfficeFileServerPath = _result.DISK_VOL_PATH.ToString & "\" & _result.DocTypeId.ToString & "\" & _result.OffSet.ToString & "\" & _result.Doc_File.ToString

                    If IO.File.Exists(OpenOfficeFileTempPath) Then
                        IO.File.Copy(OpenOfficeFileTempPath, OpenOfficeFileServerPath, True)
                    End If
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        _document = Nothing

        If Not IsNothing(AxWebBrowser1) Then
            Try
                If Not IsNothing(AxWebBrowser1.Document) Then
                    Dim D1 As New NB(AddressOf NavegarPaginaenBlanco)
                    Invoke(D1)
                End If

                'Si es un documento PDF se lo trata de manera diferente por problemas de liberación de memoria.
                If Not IsNothing(_result) AndAlso _result.IsPDF Then
                    Try
                        'Se libera el objeto COM.
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(AxWebBrowser1.ActiveXInstance)
                    Catch
                    End Try
                End If
            Catch
            End Try
            Try
                'Se libera el browser
                If Not IsNothing(AxWebBrowser1) AndAlso Not IsNothing(AxWebBrowser1.Document) Then
                    AxWebBrowser1.Dispose()
                End If
                AxWebBrowser1 = Nothing
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End If

        Dim fi As IO.FileInfo = Nothing
        Dim resBusiness As ResultBusinessExt

        Try
            If Not String.IsNullOrEmpty(_file) Then
                fi = New IO.FileInfo(_file)

                'Si el documento es editable se guarda una copia
                If _result.IsOffice AndAlso Not _result.IsPDF AndAlso Not _result.DocType.IsReadOnly Then
                    If flagAutomaticNewVersion AndAlso Not DisableAutomaticVersion AndAlso _result.IsOffice Then
                    Else
                        If fi.Exists Then
                            resBusiness = New ResultBusinessExt
                            resBusiness.UpdateDocument(_result, _file, _result.FullPath)
                        End If
                    End If
                End If

                'Se elimina la copia temporal
                If fi.Exists AndAlso deleteTemp Then fi.Delete()
            End If
        Catch ex As IO.IOException
            'Si el archivo a borrar es .pdf , el Acrobat Reader se queda colgado y no lo libera. 
            'No se atrapa la exception ya que depende de la versión del PDF.
            If Not _result.IsPDF Then
                ZClass.raiseerror(ex)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            fi = Nothing
            resBusiness = Nothing
        End Try

        Try
            If Not IsNothing(_result) AndAlso Not IsNothing(_result.IsMAG) Then
                If _result.IsMAG Then
                    _magViewer.AxPowerWeb2.Dispose()
                    _magViewer.Dispose()
                End If
            End If
        Catch ex As NullReferenceException
            ZClass.raiseerror(ex)
        Catch ex As UnauthorizedAccessException
            ZClass.raiseerror(ex)
            RaiseEvent WebBrowserError(ex)
        Catch ex As OutOfMemoryException
            ZClass.raiseerror(ex)
            RaiseEvent WebBrowserError(ex)
        Catch ex As StackOverflowException
            ZClass.raiseerror(ex)
        Catch ex As AppDomainUnloadedException
            ZClass.raiseerror(ex)
        Catch ex As ApplicationException
            ZClass.raiseerror(ex)
        Catch ex As ArgumentException
            ZClass.raiseerror(ex)
        Catch ex As ArithmeticException
            ZClass.raiseerror(ex)
        Catch ex As BadImageFormatException
            ZClass.raiseerror(ex)
        Catch ex As InvalidOperationException
            ZClass.raiseerror(ex)
        Catch ex As InvalidProgramException
            ZClass.raiseerror(ex)
        Catch ex As SystemException
            ZClass.raiseerror(ex)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            RaiseEvent WebBrowserError(ex)
        End Try
    End Sub
#End Region
    'MANEJADOR DEL EVENTO NAVIGATECOMPLETE2 QUE ES EL ENCARGADO DE PONER LOS TOOLBARS

    Private Sub AxWebBrowser1_NavigateComplete2(ByVal sender As Object, ByVal e As WebBrowserNavigatedEventArgs) Handles AxWebBrowser1.Navigated
        If _result IsNot Nothing AndAlso _result.isDisposed = False Then
            Try
                If String.Compare(e.Url.ToString, "about:blank") <> 0 Then
                    If _result.IsWord Or _result.IsPowerpoint Then
                        If Not IsNothing(AxWebBrowser1.ActiveXInstance.Document) Then
                            _document = AxWebBrowser1.ActiveXInstance.Document
                            If Not IsNothing(_document) Then
                                Try
                                    _document.Saved = False
                                Catch
                                End Try
                            End If
                            Try
                                If _showBars = False Then
                                    For Each o As Object In _document.CommandBars
                                        Try
                                            If o.visible = True Then
                                                o.Visible = False
                                            End If
                                        Catch ex As Exception
                                            ZClass.raiseerror(ex)
                                        End Try
                                    Next
                                Else
                                    _document.CommandBars("Standard").Visible = _showBars
                                    _document.CommandBars("Formatting").Visible = _showBars
                                    _document.CommandBars("Drawing").Visible = _showBars
                                End If
                                'En caso de que el visualizador de office no permita la utilizacion de las commandBars
                            Catch
                            End Try
                        End If
                    End If
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

            If _result.IsOffice AndAlso _result.IsMAG = False AndAlso e.Url.ToString <> "about:blank" Then
                Try
                    AxWebBrowser1.ScrollBarsEnabled = True
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                    RaiseEvent WebBrowserError(ex)
                End Try
            End If
            RaiseEvent DocumentReady()
        End If
    End Sub
    Private Sub AxWebBrowser1_DocumentComplete(ByVal sender As Object, ByVal e As WebBrowserDocumentCompletedEventArgs) Handles AxWebBrowser1.DocumentCompleted
        Try
            'oDocument = e.pDisp.Document

            'Nota: puede utilizar la referencia al objeto documento para
            '      automatizar el servidor de documentos.
            '           MsgBox("Archivo abierto por: " & oDocument.Application.Name)
            RaiseEvent DocumentReady()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub AxWebBrowser2_NewWindow2(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles AxWebBrowser1.NewWindow
        RaiseEvent DocumentReady()
    End Sub
    Private Sub AxWebBrowser2_FileDownload(ByVal sender As Object, ByVal e As EventArgs) Handles AxWebBrowser1.FileDownload
        RaiseEvent DocumentReady()
    End Sub
    'Public Sub PrintTxt()
    '    Try
    '        'AxWebBrowser2.QueryStatusWB(SHDocVw.OLECMDID.OLECMDID_PRINT)
    '        'If IsPrinterEnabled() Then
    '        AxWebBrowser1.ExecWB(SHDocVw.OLECMDID.OLECMDID_PRINT, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_PROMPTUSER)
    '        'End If
    '    Catch ex As Exception
    '       zclass.raiseerror(ex)
    '    End Try
    'End Sub
    'Private Function IsPrinterEnabled() As Boolean
    '    Dim response As Integer
    '    Try
    '        response = (AxWebBrowser1.QueryStatusWB(SHDocVw.OLECMDID.OLECMDID_PRINT))
    '        If response <> 0 And SHDocVw.OLECMDF.OLECMDF_ENABLED <> 0 Then
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    Catch ex As Exception
    '       zclass.raiseerror(ex)
    '    End Try

    'End Function

    Public Function OpenExternOfficeDocument() As Boolean
        Try
            'MsgBox("Debug: Abrir Externamente con Registry")
            Dim rk As RegistryKey
            Dim fi As New IO.FileInfo(_file)

            Dim prekey As String = String.Empty
            Dim posKey As String = String.Empty
            Dim exec As String = String.Empty
            Dim value As String = String.Empty

            If Not fi.Exists Then
                Throw New Exception("El archivo " & _file & " no existe")
            End If

            Select Case fi.Extension.ToLower
                Case ".doc"
                    prekey = "software\microsoft\office"
                    posKey = "\Word\InstallRoot"
                    exec = "winword.exe"
                    value = "Path"
                Case ".xls"
                    prekey = "software\microsoft\office"
                    posKey = "\Word\InstallRoot"
                    exec = "winword.exe"
                    value = "Path"
                Case ".ppt"
                    prekey = "software\microsoft\office"
                    posKey = "\Word\InstallRoot"
                    exec = "excel.exe"
                    value = "Path"
                Case ".msg"
                    prekey = "software\microsoft\office"
                    posKey = "\Word\InstallRoot"
                    exec = "outlook.exe"
                    value = "Path"
                Case ".dot"
                    prekey = "software\microsoft\office"
                    posKey = "\Word\InstallRoot"
                    exec = "powerpnt.exe"
                    value = "Path"
                Case ".pdf"
                    prekey = "software\Adobe\Acrobat Reader"
                    posKey = "\InstallPath"
                    exec = "Acrord32.exe"
                    value = String.Empty
            End Select

            Try
                rk = Registry.LocalMachine.OpenSubKey(prekey)
            Catch ex As Exception
                Throw New Exception("El programa externo no está instalado")
            End Try

            Dim names() As String
            Try
                names = rk.GetSubKeyNames()

                Dim s As String

                For Each s In names
                    Dim reg As RegistryKey
                    Try
                        reg = Registry.LocalMachine.OpenSubKey(prekey & "\" & s & posKey)
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try

                    If Not reg Is Nothing Then

                        Dim path As String = reg.GetValue(value, String.Empty)
                        If path <> String.Empty Then
                            path = path.Replace(Chr(34), String.Empty)
                            If path.EndsWith("\") = False Then
                                path = String.Concat(path, "\")
                            End If
                            Dim finf As New IO.FileInfo(path & exec)

                            If finf.Exists = True Then
                                finf = New IO.FileInfo(_file)
                                If finf.Exists = True Then
                                    Dim command As String = Chr(34) & path & exec & Chr(34) & " " & Chr(34) & _file & Chr(34)
                                    Shell(command, AppWinStyle.Hide, False)
                                    Return True
                                End If
                            End If
                        End If
                    End If
                Next
                Return False
                'MsgBox("Debug: Termino de Abrir Externamente con Registry")
            Catch ex As Exception
                'MsgBox("Debug: Error al Abrir Externamente con Registry")
                ZClass.raiseerror(ex)
                Return False
            End Try
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    'Protected Overrides Sub Finalize()
    '    CloseWebBrowser(True)
    '    If IsNothing(AxWebBrowser1) = False Then
    '        AxWebBrowser1.Dispose()
    '        AxWebBrowser1 = Nothing
    '    End If
    '    MyBase.Finalize()
    'End Sub

    'Private Sub KillAcrobatProcess()
    '    Dim Proc As Process
    '    For Each Proc In Process.GetProcessesByName("Acrord32")
    '        Proc.Kill()
    '    Next
    'End Sub



    '''Si es un documento de office usa las funciones sino lo guarda con el webbrowser
    Public Sub SaveAsDocument()
        Try
            If _result.IsExcel Then
                If Not IsNothing(_result.File) Then
                    'Zamba.Excel.Business.Excel.SaveAsExcel(Result.File)
                    ExcelInterop.SaveAsExcel(_result.File)
                Else
                    'Zamba.Excel.Business.Excel.SaveAsExcel(Result.FullPath)
                    ExcelInterop.SaveAsExcel(_result.FullPath)
                End If
            ElseIf _result.IsWord Then
                OfficeInterop.SaveAsOffice(AxWebBrowser1.ActiveXInstance.document, True)
                If Not IsNothing(_result.File) Then
                    AxWebBrowser1.Navigate(_result.File)
                Else
                    AxWebBrowser1.Navigate(_result.FullPath)
                End If
            ElseIf _result.IsPowerpoint Then
                OfficeInterop.SaveAsOffice(AxWebBrowser1.ActiveXInstance.document, False)
                If Not IsNothing(_result.File) Then
                    AxWebBrowser1.Navigate(_result.File)
                Else
                    AxWebBrowser1.Navigate(_result.FullPath)
                End If
            Else
                If IsNothing(AxWebBrowser1) = False Then
                    AxWebBrowser1.ShowSaveAsDialog()
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    '''Si es un documento de office usa las funciones sino lo imprime con el webbrowser
    Public Sub PrintDocument()
        Try
            If _result IsNot Nothing AndAlso _result.IsOffice Then
                If _result.IsExcel Then
                    If Not IsNothing(_result.File) Then
                        Zamba.Print.PrintGrilla.ImprimirExcel(_result.File, True)
                    Else
                        Zamba.Print.PrintGrilla.ImprimirExcel(_result.FullPath, True)
                    End If
                ElseIf _result.IsWord Then
                    If Not IsNothing(_result.File) Then
                        Zamba.Print.PrintGrilla.ImprimirWord(_result.File)
                    Else
                        Zamba.Print.PrintGrilla.ImprimirWord(_result.FullPath)
                    End If
                ElseIf _result.IsPowerpoint Then
                    Zamba.Print.PrintGrilla.ImprimirPP(AxWebBrowser1.ActiveXInstance.document)
                End If
            Else
                AxWebBrowser1.ShowPrintDialog()
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Navega a una pagina en blanco
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub NavegarPaginaenBlanco()
        Try
            If Not IsNothing(AxWebBrowser1) Then
                AxWebBrowser1.Navigate("about:blank")
            End If
            'Atrapo la exception por el thread
        Catch ex As Exception
        End Try
    End Sub


#Region " IDisposable Support "


    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            CloseWebBrowser(True)
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If

        MyBase.Dispose(disposing)
    End Sub
#End Region


    ''' <summary>
    ''' Abre un documento por fuera, con la aplicación por defecto de Windows para la extensión del mismo
    ''' </summary>
    ''' <param name="file">Ruta del archivo a abrir por fuera</param>
    ''' <history>
    '''     Javier  10/01/2010  Created
    ''' </history>
    Public Sub OpenExternDocument(ByVal file As String)
        Dim finf As IO.FileInfo = New IO.FileInfo(file)

        Try
            If finf.Exists = True Then

                Dim command As String

                command = Chr(34) & file & Chr(34)

                ZTrace.WriteLineIf(ZTrace.IsInfo, "OpenExternDocument - command: " & command)

                Dim proc As New System.Diagnostics.Process()
                proc.Start(command)

            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub WebBrowser_ShowError(ByVal Msg As String) Handles Me.ShowError

    End Sub

    Private Sub WebBrowser_ShowInfo(ByVal Msg As String, ByVal Title As String, ByVal Tmsg As AppBlock.Enums.TMsg, ByVal Interfaz As AppBlock.Enums.Tinterfaz) Handles Me.ShowInfo

    End Sub

    Private Sub WebBrowser_ShowWait(ByVal Estado As Boolean, ByVal Cancel As Boolean) Handles Me.ShowWait

    End Sub

    Private Sub WebBrowser_ShowWarning(ByVal Msg As String) Handles Me.ShowWarning

    End Sub

    Private Sub WebBrowser_WebBrowserError(ByVal Ex As System.Exception) Handles Me.WebBrowserError

    End Sub

    Private Sub AxWebBrowser1_StatusTextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles AxWebBrowser1.StatusTextChanged
        If DirectCast(sender, System.Windows.Forms.WebBrowser).DocumentTitle.Contains("cancel") Then
            'Deshabilita el WebBrowser y hace visible el mensaje al control

            If Not AxWebBrowser1 Is Nothing Then
                AxWebBrowser1.Parent.Controls.Remove(AxWebBrowser1)
                AxWebBrowser1 = Nothing
            End If
        End If

    End Sub
End Class
