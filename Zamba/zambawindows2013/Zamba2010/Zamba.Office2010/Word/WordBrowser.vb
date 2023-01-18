Imports Microsoft.Office.Interop
Imports System.IO
Imports System.Windows.Forms
Imports System.Drawing
Imports Zamba.Core

Public Class WordBrowser

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
    Private winHandle As IntPtr = System.IntPtr.Zero
    Private oldParent As IntPtr = System.IntPtr.Zero
    Private oldWinStyle As Int32 = 0
    Private timOut As Int32 = 0
    Private longVal As Long

    Private WithEvents pnlDoc As Control

    Private WithEvents WordApp As Word.Application = Nothing
    Private WithEvents WordDoc As Word.Document = Nothing

    Private TP As ToolStripContentPanel
    Private docFile As String

#End Region

#Region "Propiedades"

    Public ReadOnly Property IsDocumentEdited() As Boolean
        Get
            If WordApp Is Nothing OrElse WordDoc Is Nothing Then
                Return False
            Else
                Return Not WordDoc.Saved
            End If
        End Get
    End Property

#End Region

#Region "Metodos"

    Public Sub New(ByRef Tab As ToolStripContentPanel)

        TP = Tab
        docFile = String.Empty
        MsgTimer = New Timer

    End Sub

    Public Sub ShowDocument(ByVal filepath As String, ByVal isReadOnly As Boolean)
        Try
            If WordApp Is Nothing Then
                WordApp = New Word.Application
                WordApp.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone
            End If
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsError, "WordBrowser Exception: " & ex.ToString())
        End Try

        If filepath.ToLower <> docFile.ToLower Then
            If isReadOnly Then
                pnlDoc = New SplitContainer
                DirectCast(pnlDoc, SplitContainer).Orientation = Orientation.Horizontal
                Dim btnClose As New Button
                btnClose.Dock = DockStyle.Right
                btnClose.Text = "Cerrar"

                RemoveHandler btnClose.Click, AddressOf btnCloseDocumentClick
                AddHandler btnClose.Click, AddressOf btnCloseDocumentClick
                DirectCast(pnlDoc, SplitContainer).Panel1.Controls.Add(btnClose)

                Dim lblReadOnly As New Label()
                lblReadOnly.Text = "ATENCION - El documento es de solo lectura, no se guardaran los cambios realizados por el usuario"
                lblReadOnly.Font = New Font(lblReadOnly.Font, FontStyle.Bold)
                lblReadOnly.Dock = DockStyle.Fill
                lblReadOnly.BackColor = Color.LightYellow
                DirectCast(pnlDoc, SplitContainer).Panel1.BackColor = Color.LightYellow
                DirectCast(pnlDoc, SplitContainer).Panel1.Controls.Add(lblReadOnly)
                DirectCast(pnlDoc, SplitContainer).Panel1MinSize = 20
                DirectCast(pnlDoc, SplitContainer).IsSplitterFixed = True
                DirectCast(pnlDoc, SplitContainer).FixedPanel = FixedPanel.Panel1
                DirectCast(pnlDoc, SplitContainer).SplitterDistance = 20
            Else
                pnlDoc = New Panel()
            End If
            winHandle = IntPtr.Zero

            docFile = filepath

            Try
                WordDoc = New Word.Document

                WordDoc = WordApp.Documents.Open(filepath.ToString(), vFalse, vFalse, vFalse)
                WordDoc.Activate()

                AddHandler MsgTimer.Tick, AddressOf MsgTimer_Tick

                AddHandler WordApp.DocumentBeforeClose, AddressOf WordDoc_DocumentBeforeClose
                AddHandler WordApp.DocumentBeforePrint, AddressOf WordApp_DocumentBeforePrint
                AddHandler WordApp.DocumentBeforeSave, AddressOf WordApp_DocumentBeforeSave

                MsgTimer.Enabled = True
                MsgTimer.Interval = 100

                pnlDoc.Dock = DockStyle.Fill

                TP.Controls.Add(pnlDoc)

                pnlDoc.BringToFront()

                WordApp.Visible = True
                WordDoc.Saved = True

            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsError, "WordBrowser Exception: " & ex.ToString())
            End Try

        End If

    End Sub

    ''' <summary>
    ''' Cierra el documento de word al presionar el boton cerrar de la barra amarilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub btnCloseDocumentClick()
        RaiseEvent eCloseDocument()
    End Sub

    Private Sub CloseDocument()

        Dim FI As FileInfo

        Try
            FI = New FileInfo(docFile)

            If Not WordApp Is Nothing Then
                RemoveHandler WordApp.DocumentBeforeClose, AddressOf WordDoc_DocumentBeforeClose
                RemoveHandler WordApp.DocumentBeforePrint, AddressOf WordApp_DocumentBeforePrint
                RemoveHandler WordApp.DocumentBeforeSave, AddressOf WordApp_DocumentBeforeSave

                WordApp.NormalTemplate.Saved = True
            End If

            If Not WordDoc.Saved Then
                If MessageBox.Show(String.Format("¿Desea guardar los cambios efectuados al documento {0}?", FI.Name), "Zamba Software", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    WordDoc.Save()
                End If
                WordDoc.Saved = True
                WordDoc.Close()
            End If

            If Not WordApp Is Nothing Then
                WordApp.Quit()
                Threading.Thread.Sleep(1000)
            End If

        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsError, ex.ToString)
        Finally
            FI = Nothing
        End Try

    End Sub

    Public Sub PrintDocument()

        If Not WordDoc Is Nothing AndAlso Not WordApp Is Nothing Then
            WordApp.Dialogs.Item(Word.WdWordDialog.wdDialogFilePrint).Show()
        End If

    End Sub

    Public Sub SaveDocument()

        If Not WordDoc Is Nothing Then
            RemoveHandler WordApp.DocumentBeforeSave, AddressOf WordApp_DocumentBeforeSave
            WordDoc.Save()
            WordDoc.Saved = True
            AddHandler WordApp.DocumentBeforeSave, AddressOf WordApp_DocumentBeforeSave
        End If

    End Sub

    Public Sub SaveDocumentAs()

        If Not WordDoc Is Nothing AndAlso Not WordApp Is Nothing Then
            RemoveHandler WordApp.DocumentBeforeSave, AddressOf WordApp_DocumentBeforeSave
            WordApp.Dialogs.Item(Word.WdWordDialog.wdDialogFileSaveAs).Show()
            AddHandler WordApp.DocumentBeforeSave, AddressOf WordApp_DocumentBeforeSave
        End If

    End Sub

#End Region

#Region "Funciones y llamadas a apis"

    Private Function FindWindowByApi(ByVal WindowCaption As String) As System.IntPtr

        If winHandle = IntPtr.Zero Then
            ZTrace.WriteLineIf(ZTrace.IsError, "Comienza a buscar la ventana ..." & Date.Now.ToString())

            winHandle = WindowsApi.Usr32Api.FindWindow(WindowsApi.ClassNames.Outlook, WindowCaption)

            If winHandle = IntPtr.Zero Then
                winHandle = WindowsApi.Usr32Api.FindWindow(WindowsApi.ClassNames.Word, WindowCaption)
            End If

            If winHandle = IntPtr.Zero Then
                WindowCaption = WordApp.ActiveWindow.Caption & " - Word"
                winHandle = WindowsApi.Usr32Api.FindWindow(WindowsApi.ClassNames.Word, WindowCaption)
            End If

            ZTrace.WriteLineIf(ZTrace.IsVerbose, winHandle.ToString() & " = FindWindow ( rctrl_renwnd32 , '" & WindowCaption & "' ) " & Date.Now.ToString())

            If winHandle <> IntPtr.Zero Then
                ZTrace.WriteLineIf(ZTrace.IsError, "Se encontro Handle ( " & winHandle.ToString() & " ) el Titulo es '" & WindowCaption & "' " & Date.Now.ToString())
            End If

        End If

        Return winHandle

    End Function

    Private Sub SetMsgPanelAttributes()

        If winHandle <> IntPtr.Zero Then

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

            'ZTrace.WriteLineIf(ZTrace.IsVerbose, " Se cambiaron atributos de ventana (Botones de cerrar, minimizar y demas) " & Date.Now.ToString())

        End If

    End Sub

    Private Sub DocumentPanelSetSize() Handles pnlDoc.Resize
        Dim ret As IntPtr
        Dim width As Int32 = TP.Width
        Dim height As Int32 = TP.Height

        If winHandle <> IntPtr.Zero Then
            ret = WindowsApi.Usr32Api.SetWindowPos(winHandle, 1, 0, 0, width, height, 0)
            ' ret = WindowsApi.Usr32Api.ShowWindow(winHandle, WindowsApi.Usr32Api.SW_MAXIMIZE)
            WindowsApi.Usr32Api.SetActiveWindow(winHandle)
        End If

        SetMsgPanelAttributes()
    End Sub

#End Region

#Region "Eventos"

    Private Sub WordDoc_DocumentBeforeClose(ByVal Doc As Microsoft.Office.Interop.Word.Document, ByRef Cancel As Boolean)
        Cancel = True
        Dispose()
        Dim T1 As New Threading.Thread(AddressOf eCloseDocumentHandler2)
        T1.Start()
    End Sub

    Private Sub eCloseDocumentHandler1()
        Dim D1 As New eCloseDocumentDelegate(AddressOf eCloseDocumentHandler2)
        TP.Invoke(D1)
    End Sub

    Private Sub eCloseDocumentHandler2()
        RaiseEvent eCloseDocument()
    End Sub

    Private Sub WordApp_DocumentBeforePrint(ByVal Doc As Microsoft.Office.Interop.Word.Document, ByRef Cancel As Boolean)
        RaiseEvent ePrintingDocument()
    End Sub

    Private Sub WordApp_DocumentBeforeSave(ByVal Doc As Microsoft.Office.Interop.Word.Document, ByRef SaveAsUI As Boolean, ByRef Cancel As Boolean) Handles WordApp.DocumentBeforeSave
        If Doc.Saved Then
            RaiseEvent eDocumentSaved()
        End If
    End Sub

    Private Sub MsgTimer_Tick()


        Dim threadId As IntPtr = IntPtr.Zero
        Dim pId As IntPtr = IntPtr.Zero
        Dim actualParent As IntPtr = IntPtr.Zero
        Dim Caption As String

        If Not WordApp Is Nothing Then

            Try

                Caption = WordApp.ActiveWindow.Caption & " - Microsoft Word"

                ZTrace.WriteLineIf(ZTrace.IsError, "Buscando ventana: " & Caption)

                winHandle = FindWindowByApi(Caption)

                If winHandle = IntPtr.Zero Then
                    ZTrace.WriteLineIf(ZTrace.IsError, "NO Se encontro Handle de la ventana " & Date.Now.ToString())
                End If

                If TypeOf (pnlDoc) Is SplitContainer Then
                    setParent(actualParent, DirectCast(pnlDoc, SplitContainer).Panel2)
                Else
                    setParent(actualParent, pnlDoc)
                End If
            Catch ex As ObjectDisposedException
                ZTrace.WriteLineIf(ZTrace.IsError, "WordBrowser Exception: " & ex.ToString())
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsError, "WordBrowser Exception: " & ex.ToString())
            End Try

            ZTrace.WriteLineIf(ZTrace.IsError, " -- End sub de MsgTimer_Tick -- " & Date.Now.ToString())

        End If

    End Sub

    Private Sub setParent(ByVal actualParent As IntPtr, ByVal ctrl As Control)
        If winHandle <> IntPtr.Zero AndAlso actualParent <> ctrl.Handle Then
            oldParent = WindowsApi.Usr32Api.GetParent(winHandle)
            Dim x As IntPtr = WindowsApi.Usr32Api.SetParent(winHandle, ctrl.Handle)
            actualParent = WindowsApi.Usr32Api.GetParent(winHandle)

            DocumentPanelSetSize()
            SetMsgPanelAttributes()

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
    End Sub

#End Region

#Region " IDisposable Support "

    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                CloseDocument()
            End If

            If MsgTimer IsNot Nothing Then
                RemoveHandler MsgTimer.Tick, AddressOf MsgTimer_Tick
                MsgTimer.Stop()
                MsgTimer.Dispose()
                MsgTimer = Nothing
            End If

            Try
                If Not WordDoc Is Nothing Then
                    WordDoc = Nothing
                End If
            Catch ex As Exception
            End Try

            Try
                If Not WordApp Is Nothing Then
                    WordApp = Nothing
                End If
            Catch ex As Exception
            End Try

            MsgTimer = Nothing

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
