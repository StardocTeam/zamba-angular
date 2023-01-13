Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices.Marshal
Imports System.Globalization
Imports System.IO
Imports System.Threading.Thread
Imports System.Windows.Forms
Imports System.Drawing
Imports Zamba.Core

Public Class ExcelBrowser
    Implements IDisposable

#Region "Variables privadas"

    Private disposedValue As Boolean = False        ' To detect redundant calls
    Delegate Sub eCloseDocumentDelegate()

    Private vFalse As Object = False
    Private vTrue As Object = True
    Private vMissing As Object = Nothing

    Public Event eCloseDocument()
    Public Event eDocumentSaved()
    Public Event ePrintingDocument()

    Private MsgTimer As Timer = Nothing
    Private oldWinStyle As Int32 = 0
    Private timOut As Int32 = 0
    Private longVal As Long
    Private Const toolbarHeight As Int32 = 40

    Private WithEvents pnlDoc As Panel
    Private WithEvents ExcelApp As Excel.Application = Nothing
    Private WithEvents ExcelDoc As Excel.Workbook = Nothing
    Private WithEvents ExcelBooks As Excel.Workbooks = Nothing

    Private TP As TabPage
    Private docFile As String

    Private winHandle As IntPtr = System.IntPtr.Zero
    Private hMenu As IntPtr = System.IntPtr.Zero

#End Region

#Region "Propiedades"

    Public ReadOnly Property IsDocumentEdited() As Boolean
        Get
            If ExcelApp Is Nothing OrElse ExcelDoc Is Nothing Then
                Return False
            Else
                Return Not ExcelDoc.Saved
            End If
        End Get
    End Property

#End Region

#Region "Metodos"

    Public Sub New(ByRef Tab As TabPage)

        TP = Tab
        docFile = String.Empty
        MsgTimer = New Timer

    End Sub

    Public Sub ShowDocument(ByVal filepath As String)

        Try
            If ExcelApp Is Nothing Then
                ExcelApp = New Excel.Application
            End If
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsError, "ExcelBrowser Exception: " & ex.ToString())
        End Try

        If filepath.ToLower <> docFile.ToLower Then

            pnlDoc = New Panel()
            winHandle = IntPtr.Zero
            docFile = filepath

            Try
                ExcelBooks = ExcelApp.Workbooks
                Try
                    ExcelDoc = ExcelBooks.Open(filepath.ToString())
                Catch ex As Exception
                    CurrentThread.CurrentCulture = New CultureInfo("en-US")
                    ExcelDoc = ExcelBooks.Open(filepath.ToString())
                End Try
                ExcelDoc.Activate()

                AddHandler MsgTimer.Tick, AddressOf MsgTimer_Tick
                AddHandler ExcelApp.WorkbookBeforeClose, AddressOf ExcelDoc_ExcelBeforeClose
                AddHandler ExcelApp.WorkbookBeforePrint, AddressOf WordApp_DocumentBeforePrint
                AddHandler ExcelApp.WorkbookBeforeSave, AddressOf ExcelApp_ExcelBeforeSave

                pnlDoc.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
                pnlDoc.Width = TP.Width
                pnlDoc.Height = TP.Height - toolbarHeight
                pnlDoc.Top = toolbarHeight
                pnlDoc.Left = 0
                pnlDoc.BackColor = Color.Gray
                pnlDoc.BorderStyle = BorderStyle.None
                TP.Controls.Add(pnlDoc)
                pnlDoc.BringToFront()

                ExcelDoc.Saved = True

                'Configura la visualización
                With ExcelApp
                    '.Visible = True
                    .DisplayClipboardWindow = False
                    .DisplayCommentIndicator = Excel.XlCommentDisplayMode.xlNoIndicator
                    .DisplayDocumentActionTaskPane = False
                    .DisplayDocumentInformationPanel = False
                    .DisplayExcel4Menus = False
                    .DisplayFormulaAutoComplete = True
                    .DisplayFormulaBar = True
                    .DisplayRecentFiles = False
                    .DisplayStatusBar = False
                    .ShowDevTools = False
                    .ShowMenuFloaties = False
                    .ShowStartupDialog = False
                    .ShowWindowsInTaskbar = False
                    .ShowToolTips = False
                    .Visible = True
                End With

                MsgTimer.Enabled = True
                MsgTimer.Interval = 100

            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsError, "ExcelBrowser Exception: " & ex.ToString())
            End Try

        End If

    End Sub

    Public Sub PrintDocument()

        If Not ExcelDoc Is Nothing AndAlso Not ExcelApp Is Nothing Then
            ExcelApp.Dialogs.Item(Word.WdWordDialog.wdDialogFilePrint).Show()
        End If

    End Sub

    Public Sub SaveDocument()

        If Not ExcelDoc Is Nothing Then
            RemoveHandler ExcelApp.WorkbookBeforeSave, AddressOf ExcelApp_ExcelBeforeSave
            ExcelDoc.Save()
            ExcelDoc.Saved = True
            AddHandler ExcelApp.WorkbookBeforeSave, AddressOf ExcelApp_ExcelBeforeSave
        End If

    End Sub

    Public Sub SaveDocumentAs()

        If Not ExcelDoc Is Nothing AndAlso Not ExcelApp Is Nothing Then
            RemoveHandler ExcelApp.WorkbookBeforeSave, AddressOf ExcelApp_ExcelBeforeSave
            ExcelApp.Dialogs.Item(Word.WdWordDialog.wdDialogFileSaveAs).Show()
            AddHandler ExcelApp.WorkbookBeforeSave, AddressOf ExcelApp_ExcelBeforeSave
        End If

    End Sub

#End Region

#Region "Funciones y llamadas a apis"

    Private Function FindWindowByApi(ByVal WindowCaption As String) As System.IntPtr

        If winHandle = IntPtr.Zero Then
            ZTrace.WriteLineIf(ZTrace.IsError, "Comienza a buscar la ventana ..." & Date.Now.ToString())

            'TODO: A PARTIR DE 2002 EXISTE ExcelApp.Hwnd. VERIFICAR  
            'SI CON ESTO ALCANZA Y SE AHORRA LA LLAMADA A LAS APIS.
            winHandle = WindowsApi.Usr32Api.FindWindow(WindowsApi.ClassNames.Outlook, WindowCaption)
            If winHandle = IntPtr.Zero Then
                winHandle = WindowsApi.Usr32Api.FindWindow(WindowsApi.ClassNames.Word, WindowCaption)
                If winHandle = IntPtr.Zero Then
                    winHandle = ExcelApp.Hwnd
                End If
            End If

            ZTrace.WriteLineIf(ZTrace.IsVerbose,winHandle.ToString() & " = FindWindow ( rctrl_renwnd32 , '" & WindowCaption & "' ) " & Date.Now.ToString())

        End If

        Return winHandle

    End Function

    Private Sub SetMsgPanelAttributes()

        If winHandle <> IntPtr.Zero Then

            'hMenu = WindowsApi.Usr32Api.GetSystemMenu(winHandle, 0)
            'WindowsApi.Usr32Api.DeleteMenu(hMenu, WindowsApi.Usr32Api.SC_MINIMIZE, WindowsApi.Usr32Api.MF_BYCOMMAND)
            'WindowsApi.Usr32Api.DeleteMenu(hMenu, WindowsApi.Usr32Api.SC_MAXIMIZE, WindowsApi.Usr32Api.MF_BYCOMMAND)
            'WindowsApi.Usr32Api.DeleteMenu(hMenu, WindowsApi.Usr32Api.SC_SIZE, WindowsApi.Usr32Api.MF_BYCOMMAND)
            'WindowsApi.Usr32Api.DeleteMenu(hMenu, WindowsApi.Usr32Api.SC_MOVE, WindowsApi.Usr32Api.MF_BYCOMMAND)
            'WindowsApi.Usr32Api.DeleteMenu(hMenu, WindowsApi.Usr32Api.SC_RESTORE, WindowsApi.Usr32Api.MF_BYCOMMAND)
            'WindowsApi.Usr32Api.DeleteMenu(hMenu, WindowsApi.Usr32Api.SC_NEXTWINDOW, WindowsApi.Usr32Api.MF_BYCOMMAND)
            'WindowsApi.Usr32Api.DeleteMenu(hMenu, WindowsApi.Usr32Api.SC_CLOSE, WindowsApi.Usr32Api.MF_BYCOMMAND)
            'WindowsApi.Usr32Api.DeleteMenu(hMenu, 0, WindowsApi.Usr32Api.MF_BYPOSITION)
            'WindowsApi.Usr32Api.DrawMenuBar(hMenu)
            'Dim winStyle As Int32 = WindowsApi.Usr32Api.GetWindowLong(winHandle, WindowsApi.Usr32Api.GWL_STYLE)
            'oldWinStyle = winStyle
            'winStyle = winStyle And Not WindowsApi.Usr32Api.WS_MAXIMIZEBOX
            'winStyle = winStyle And Not WindowsApi.Usr32Api.WS_MINIMIZEBOX
            'winStyle = winStyle And Not WindowsApi.Usr32Api.WS_SYSMENU
            'winStyle = winStyle And Not WindowsApi.Usr32Api.WS_MAXIMIZE
            'winStyle = winStyle And Not WindowsApi.Usr32Api.WS_CAPTION
            'winStyle = winStyle Or WindowsApi.Usr32Api.WS_CHILD
            'WindowsApi.Usr32Api.SetWindowLong(winHandle, WindowsApi.Usr32Api.GWL_STYLE, winStyle)


            Dim val As IntPtr
            val = WindowsApi.Usr32Api.GetSystemMenu(winHandle, 0)
            WindowsApi.Usr32Api.DeleteMenu(val, WindowsApi.Usr32Api.SC_MINIMIZE, WindowsApi.Usr32Api.MF_BYCOMMAND)
            WindowsApi.Usr32Api.DeleteMenu(val, WindowsApi.Usr32Api.SC_MAXIMIZE, WindowsApi.Usr32Api.MF_BYCOMMAND)
            WindowsApi.Usr32Api.DeleteMenu(val, WindowsApi.Usr32Api.SC_SIZE, WindowsApi.Usr32Api.MF_BYCOMMAND)
            WindowsApi.Usr32Api.DeleteMenu(val, WindowsApi.Usr32Api.SC_MOVE, WindowsApi.Usr32Api.MF_BYCOMMAND)
            WindowsApi.Usr32Api.DeleteMenu(val, WindowsApi.Usr32Api.SC_RESTORE, WindowsApi.Usr32Api.MF_BYCOMMAND)
            WindowsApi.Usr32Api.DeleteMenu(val, WindowsApi.Usr32Api.SC_NEXTWINDOW, WindowsApi.Usr32Api.MF_BYCOMMAND)
            WindowsApi.Usr32Api.DeleteMenu(val, WindowsApi.Usr32Api.SC_CLOSE, WindowsApi.Usr32Api.MF_BYCOMMAND)
            WindowsApi.Usr32Api.DeleteMenu(val, 0, WindowsApi.Usr32Api.MF_BYPOSITION)
            Dim winStyle As Int32 = WindowsApi.Usr32Api.GetWindowLong(winHandle, WindowsApi.Usr32Api.GWL_STYLE)
            oldWinStyle = winStyle
            winStyle = winStyle And Not WindowsApi.Usr32Api.WS_MAXIMIZEBOX
            winStyle = winStyle And Not WindowsApi.Usr32Api.WS_MINIMIZEBOX
            winStyle = winStyle And Not WindowsApi.Usr32Api.WS_SYSMENU
            winStyle = winStyle And Not WindowsApi.Usr32Api.WS_MAXIMIZE
            winStyle = winStyle And Not WindowsApi.Usr32Api.WM_SYSCOMMAND
            winStyle = winStyle And Not WindowsApi.Usr32Api.WM_RBUTTONDBLCLK
            winStyle = winStyle And Not WindowsApi.Usr32Api.WM_COMMAND
            'winStyle = winStyle And Not WindowsApi.Usr32Api.WS_CAPTION
            'winStyle = winStyle Or WindowsApi.Usr32Api.WS_CHILD
            WindowsApi.Usr32Api.SetWindowLong(winHandle, WindowsApi.Usr32Api.GWL_STYLE, winStyle)


        End If

    End Sub

    Private Sub DocumentPanelSetSize() Handles pnlDoc.Resize
        Dim ret As IntPtr
        Dim width As Int32 = TP.Width
        Dim height As Int32 = TP.Height - toolbarHeight

        If winHandle <> IntPtr.Zero Then
            ret = WindowsApi.Usr32Api.SetWindowPos(winHandle, 1, 0, 0, width, height, 0)
            ' ret = WindowsApi.Usr32Api.ShowWindow(winHandle, WindowsApi.Usr32Api.SW_MAXIMIZE)
            WindowsApi.Usr32Api.SetActiveWindow(winHandle)
        End If

        SetMsgPanelAttributes()
    End Sub

#End Region

#Region "Eventos"

    Private Sub ExcelDoc_ExcelBeforeClose(ByVal Doc As Microsoft.Office.Interop.Excel.Workbook, ByRef Cancel As Boolean)
        Cancel = True
        Dispose()
        Dim T1 As New Threading.Thread(AddressOf eCloseDocumentHandler2)
        T1.Start()
    End Sub

    Private Sub ExcelApp_ExcelBeforeSave(ByVal Doc As Microsoft.Office.Interop.Excel.Workbook, ByVal SaveAsUI As Boolean, ByRef Cancel As Boolean) Handles ExcelApp.WorkbookBeforeSave
        If Doc.Saved Then
            RaiseEvent eDocumentSaved()
        End If
    End Sub

    Private Sub eCloseDocumentHandler1()
        Dim D1 As New eCloseDocumentDelegate(AddressOf eCloseDocumentHandler2)
        TP.Invoke(D1)
    End Sub

    Private Sub eCloseDocumentHandler2()
        RaiseEvent eCloseDocument()
    End Sub

    Private Sub WordApp_DocumentBeforePrint(ByVal Doc As Microsoft.Office.Interop.Excel.Workbook, ByRef Cancel As Boolean)
        RaiseEvent ePrintingDocument()
    End Sub


    Private Sub MsgTimer_Tick()

        Dim x As IntPtr

        Dim threadId As IntPtr = IntPtr.Zero
        Dim pId As IntPtr = IntPtr.Zero
        Dim actualParent As IntPtr = IntPtr.Zero
        Dim Caption As String

        If Not ExcelApp Is Nothing Then

            Try

                Caption = "Microsoft Excel - " & ExcelApp.ActiveWindow.Caption

                ZTrace.WriteLineIf(ZTrace.IsError, "Buscando ventana: " & Caption)

                winHandle = FindWindowByApi(Caption)


                If winHandle = IntPtr.Zero Then
                    ZTrace.WriteLineIf(ZTrace.IsError, "NO Se encontro Handle de la ventana " & Date.Now.ToString())
                End If

                If winHandle <> IntPtr.Zero AndAlso actualParent <> pnlDoc.Handle Then

                    x = WindowsApi.Usr32Api.SetParent(winHandle, pnlDoc.Handle)

                    actualParent = WindowsApi.Usr32Api.GetParent(winHandle)

                    DocumentPanelSetSize()

                    MsgTimer.Enabled = False

                Else

                    If winHandle <> IntPtr.Zero Then
                        WindowsApi.Usr32Api.SetActiveWindow(winHandle)
                        MsgTimer.Enabled = False
                        SetMsgPanelAttributes()
                    Else
                        If timOut >= 300 Then
                            ZTrace.WriteLineIf(ZTrace.IsError, " TIMEOUT : NO SE ENCONTRO VENTANA " & Date.Now.ToString())
                            MsgTimer.Enabled = False
                        Else
                            timOut = timOut + 1
                            ZTrace.WriteLineIf(ZTrace.IsError, " TIME COUNT = " & timOut.ToString() & " " & Date.Now.ToString())
                        End If
                    End If

                End If

            Catch ex As ObjectDisposedException
                ZTrace.WriteLineIf(ZTrace.IsError, "ExcelBrowser Exception: " & ex.ToString())
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsError, "ExcelBrowser Exception: " & ex.ToString())
            End Try

            ZTrace.WriteLineIf(ZTrace.IsError, " -- End sub de MsgTimer_Tick -- " & Date.Now.ToString())

        End If

    End Sub

#End Region

#Region " IDisposable Support "

    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not disposedValue Then

            Dim FI As FileInfo = Nothing

            Try
                MsgTimer = Nothing
                vFalse = Nothing
                vTrue = Nothing
                vMissing = Nothing
                winHandle = System.IntPtr.Zero

                If ExcelApp IsNot Nothing Then
                    RemoveHandler ExcelApp.WorkbookBeforeClose, AddressOf ExcelDoc_ExcelBeforeClose
                    RemoveHandler ExcelApp.WorkbookBeforePrint, AddressOf WordApp_DocumentBeforePrint
                    RemoveHandler ExcelApp.WorkbookBeforeSave, AddressOf ExcelApp_ExcelBeforeSave
                End If

                'Se guardan los cambios no salvados
                If Not ExcelDoc.Saved Then
                    FI = New FileInfo(docFile)
                    If MessageBox.Show(String.Format("¿Desea guardar los cambios efectuados al documento {0}?", FI.Name), "Zamba Software", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        ExcelDoc.Save()
                    End If
                End If

                'Se libera el documento
                Try
                    ExcelDoc.Close()
                Catch
                End Try
                ReleaseComObject(ExcelDoc)
                ExcelDoc = Nothing

                'Se libera el temporal
                If ExcelBooks IsNot Nothing Then
                    Try
                        ExcelBooks.Close()
                    Catch
                    End Try
                    ReleaseComObject(ExcelBooks)
                    ExcelBooks = Nothing
                End If

                'Si no queda ningun workbook abierto cierro tambien el excel
                If Not IsNothing(ExcelApp) Then
                    If ExcelApp.Workbooks.Count = 0 Then
                        WindowsApi.Usr32Api.SetParent(ExcelApp.Hwnd, IntPtr.Zero)
                        ExcelApp.Quit()
                        ReleaseComObject(ExcelApp)
                        'ExcelApp = Nothing
                    End If
                End If

                If pnlDoc IsNot Nothing Then
                    pnlDoc.Controls.Clear()
                    pnlDoc.Dispose()
                    pnlDoc = Nothing
                End If

            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsError, ex.ToString)
            Finally
                FI = Nothing
            End Try

        End If
        disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class
