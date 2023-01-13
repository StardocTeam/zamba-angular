Imports Zamba.Core.DocTypes.DocAsociated
Imports Zamba.Core.Search
Imports Zamba.Core
Imports System.IO
Imports Zamba.AdminControls
Imports Zamba.Viewers.WindowsApi.Usr32Api
Imports Zamba.Viewers.WindowsApi.ClassNames
Imports System.Text.RegularExpressions
Imports Microsoft.Win32
Imports System.Collections.Generic
Imports Zamba.Office
Imports System.Security.Permissions
Imports Zamba.Core.WF.WF
Imports Zamba.Core.Enumerators
Imports System.Security.Cryptography
Imports Zamba.Membership
Imports System.ComponentModel
Imports Zamba.Print

Public Class UCDocumentViewer2
    Inherits TabPage
    Implements IDisposable


#Region " Windows Form Designer generated code "
    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If Not isDisposed Then
            Try
                If disposing Then
                    If components IsNot Nothing Then
                        components.Dispose()
                    End If

                    ' Apunto la referencia a null, asi no mata el objeto al que apunta.
                    'Me.Result.Dispose()
                    _result = Nothing

                    'NOTA: no hacer dispose del objeto result ni del control de atributos ya que se encuentra referenciado.
                    RaiseEvent ClearReferences(Me)

                    'Controles del winform
                    If LayoutButton IsNot Nothing Then
                        LayoutButton.Dispose()
                        'LayoutButton = Nothing
                    End If
                    If txtNumPag IsNot Nothing Then
                        txtNumPag.Dispose()
                        'txtNumPag = Nothing
                    End If
                    If DropDownMenuItem1 IsNot Nothing Then
                        DropDownMenuItem1.Dispose()
                        'DropDownMenuItem1 = Nothing
                    End If
                    If NETRON16 IsNot Nothing Then
                        NETRON16.Dispose()
                        'NETRON16 = Nothing
                    End If
                    If MenuButtonItem7 IsNot Nothing Then
                        MenuButtonItem7.Dispose()
                        'MenuButtonItem7 = Nothing
                    End If
                    If lblPagin IsNot Nothing Then
                        lblPagin.Dispose()
                        'lblPagin = Nothing
                    End If
                    If ButtonItem1 IsNot Nothing Then
                        ButtonItem1.Dispose()
                        'ButtonItem1 = Nothing
                    End If
                    If ButtonItem2 IsNot Nothing Then
                        ButtonItem2.Dispose()
                        'ButtonItem2 = Nothing
                    End If
                    If ButtonItem3 IsNot Nothing Then
                        ButtonItem3.Dispose()
                        'ButtonItem3 = Nothing
                    End If
                    If ButtonItem4 IsNot Nothing Then
                        ButtonItem4.Dispose()
                        'ButtonItem4 = Nothing
                    End If
                    If ButtonItem5 IsNot Nothing Then
                        ButtonItem5.Dispose()
                        'ButtonItem5 = Nothing
                    End If
                    If ButtonItem6 IsNot Nothing Then
                        ButtonItem6.Dispose()
                        'ButtonItem6 = Nothing
                    End If
                    If ButtonItem7 IsNot Nothing Then
                        ButtonItem7.Dispose()
                        ' ButtonItem7 = Nothing
                    End If
                    If ButtonItem8 IsNot Nothing Then
                        ButtonItem8.Dispose()
                        'ButtonItem8 = Nothing
                    End If
                    If ButtonItem9 IsNot Nothing Then
                        ButtonItem9.Dispose()
                        'ButtonItem9 = Nothing
                    End If
                    If ButtonItem10 IsNot Nothing Then
                        ButtonItem10.Dispose()
                        'ButtonItem10 = Nothing
                    End If
                    If btnReplace IsNot Nothing Then
                        btnReplace.Dispose()
                        'btnReplace = Nothing
                    End If
                    If ButtonItem12 IsNot Nothing Then
                        ButtonItem12.Dispose()
                        'ButtonItem12 = Nothing
                    End If
                    If ButtonItem13 IsNot Nothing Then
                        ButtonItem13.Dispose()
                        'ButtonItem13 = Nothing
                    End If
                    If ButtonItem14 IsNot Nothing Then
                        ButtonItem14.Dispose()
                        'ButtonItem14 = Nothing
                    End If
                    If ButtonItem15 IsNot Nothing Then
                        ButtonItem15.Dispose()
                        'ButtonItem15 = Nothing
                    End If
                    If ButtonItem16 IsNot Nothing Then
                        ButtonItem16.Dispose()
                        'ButtonItem16 = Nothing
                    End If
                    If ButtonItem17 IsNot Nothing Then
                        ButtonItem17.Dispose()
                        'ButtonItem17 = Nothing
                    End If
                    If ButtonItem18 IsNot Nothing Then
                        ButtonItem18.Dispose()
                        'ButtonItem18 = Nothing
                    End If
                    If ButtonItem19 IsNot Nothing Then
                        ButtonItem19.Dispose()
                        'ButtonItem19 = Nothing
                    End If
                    If ButtonItem20 IsNot Nothing Then
                        ButtonItem20.Dispose()
                        'ButtonItem20 = Nothing
                    End If
                    If Buttonclose IsNot Nothing Then
                        Buttonclose.Dispose()
                        'Buttonclose = Nothing
                    End If
                    If btnClose IsNot Nothing Then
                        btnClose.Dispose()
                        'btnClose = Nothing
                    End If
                    If ButtonDocumental IsNot Nothing Then
                        ButtonDocumental.Dispose()
                        'ButtonDocumental = Nothing
                    End If
                    If BtnReplaceDocument IsNot Nothing Then
                        BtnReplaceDocument.Dispose()
                        'BtnReplaceDocument = Nothing
                    End If
                    If InfoView IsNot Nothing Then
                        InfoView.Dispose()
                        'InfoView = Nothing
                    End If
                    If DesignSandBar IsNot Nothing Then
                        RemoveHandler DesignSandBar.ItemClicked, AddressOf DesignSandBar_ButtonClick
                        DesignSandBar.Dispose()
                        DesignSandBar = Nothing
                    End If
                    If BPrint IsNot Nothing Then
                        BPrint.Dispose()
                        BPrint = Nothing
                    End If
                    If btnPrint IsNot Nothing Then
                        btnPrint.Dispose()
                        btnPrint = Nothing
                    End If
                    If btnPrintPreview IsNot Nothing Then
                        btnPrintPreview.Dispose()
                        btnPrintPreview = Nothing
                    End If
                    If btnFullScreen IsNot Nothing Then
                        btnFullScreen.Dispose()
                        btnFullScreen = Nothing
                    End If
                    If btnShow IsNot Nothing Then
                        btnShow.Dispose()
                        btnShow = Nothing
                    End If
                    If btnOriginalFile IsNot Nothing Then
                        btnOriginalFile.Dispose()
                        btnOriginalFile = Nothing
                    End If
                    If ToolStripContainer1 IsNot Nothing Then
                        ToolStripContainer1.TopToolStripPanel.Controls.Clear()
                        ToolStripContainer1.Dispose()
                        ToolStripContainer1 = Nothing
                    End If
                    If ToolStripSeparator7 IsNot Nothing Then
                        ToolStripSeparator7.Dispose()
                        ToolStripSeparator7 = Nothing
                    End If
                    If ToolStripSeparator8 IsNot Nothing Then
                        ToolStripSeparator8.Dispose()
                        ToolStripSeparator8 = Nothing
                    End If
                    'If ToolStripSeparator9 IsNot Nothing Then
                    '    ToolStripSeparator9.Dispose()
                    '    ToolStripSeparator9 = Nothing
                    'End If
                    If ToolStripSeparator12 IsNot Nothing Then
                        ToolStripSeparator12.Dispose()
                        ToolStripSeparator12 = Nothing
                    End If
                    If ToolStripSeparator13 IsNot Nothing Then
                        ToolStripSeparator13.Dispose()
                        ToolStripSeparator13 = Nothing
                    End If
                    If separateDocumentLabels IsNot Nothing Then
                        separateDocumentLabels.Dispose()
                        separateDocumentLabels = Nothing
                    End If
                    If btnRefresh IsNot Nothing Then
                        btnRefresh.Dispose()
                        btnRefresh = Nothing
                    End If
                    If btnAdjuntarEmail IsNot Nothing Then
                        btnAdjuntarEmail.Dispose()
                        btnAdjuntarEmail = Nothing
                    End If
                    If btnSaveAs IsNot Nothing Then
                        btnSaveAs.Dispose()
                        btnSaveAs = Nothing
                    End If
                    'If btnAdjuntarMensaje IsNot Nothing Then
                    '    btnAdjuntarMensaje.Dispose()
                    '    btnAdjuntarMensaje = Nothing
                    'End If
                    If btnAgregarDocumentoCarpeta IsNot Nothing Then
                        btnAgregarDocumentoCarpeta.Dispose()
                        btnAgregarDocumentoCarpeta = Nothing
                    End If
                    If btnVerVersionesDelDocumento IsNot Nothing Then
                        btnVerVersionesDelDocumento.Dispose()
                        'btnVerVersionesDelDocumento = Nothing
                    End If
                    If btnAgregarUnaNuevaVersionDelDocuemto IsNot Nothing Then
                        btnAgregarUnaNuevaVersionDelDocuemto.Dispose()
                        btnAgregarUnaNuevaVersionDelDocuemto = Nothing
                    End If
                    If btnGotoWorkflow IsNot Nothing Then
                        btnGotoWorkflow.Dispose()
                        btnGotoWorkflow = Nothing
                    End If
                    If btnChangePosition IsNot Nothing Then
                        btnChangePosition.Dispose()
                        btnChangePosition = Nothing
                    End If
                    'If btnGoToWF IsNot Nothing Then
                    '    btnGoToWF.Dispose()
                    '    btnGoToWF = Nothing
                    'End If
                    'If btnGotoWfdb IsNot Nothing Then
                    '    btnGotoWfdb.Dispose()
                    '    btnGotoWfdb = Nothing
                    'End If
                    If btnGotoHelp IsNot Nothing Then
                        btnGotoHelp.Dispose()
                        btnGotoHelp = Nothing
                    End If
                    If btnFlagAsFavorite IsNot Nothing Then
                        btnFlagAsFavorite.Dispose()
                        btnFlagAsFavorite = Nothing
                    End If
                    If btnFlagAsImportant IsNot Nothing Then
                        btnFlagAsImportant.Dispose()
                        btnFlagAsImportant = Nothing
                    End If
                    If ToolBar1 IsNot Nothing Then
                        RemoveHandler ToolBar1.ItemClicked, AddressOf ToolBar11_ButtonClick
                        ToolBar1.Dispose()
                        ToolBar1 = Nothing
                    End If

                    'Controles u objetos no agregados directamente en el winform
                    'Si el documento se encontraba en tareas, hago dispose del control de atributos.
                    If _disposeIndexViewer AndAlso _ucIndexs IsNot Nothing Then
                        _ucIndexs.Dispose()
                        _ucIndexs = Nothing
                    End If
                    If ImgViewer IsNot Nothing AndAlso ImgViewer.IsDisposed = False Then
                        ImgViewer.Dispose()
                        'ImgViewer = Nothing
                    End If
                    If FormBrowser IsNot Nothing AndAlso FormBrowser.IsDisposed = False Then
                        RemoveHandler FormBrowser.LinkSelected, AddressOf FormBrowser_LinkSelected
                        RemoveHandler FormBrowser.ResultModified, AddressOf RefreshIndexs
                        RemoveHandler FormBrowser.ShowAsociatedResult, AddressOf ThrowShowAsociatedResult
                        RemoveHandler FormBrowser.showYellowPanel, AddressOf showYellowPanel
                        RemoveHandler FormBrowser.SaveDocumentVirtualForm, AddressOf SaveDocumentVirtualForm
                        RemoveHandler FormBrowser.RefreshAfterF5, AddressOf updateDocsAsociated
                        RemoveHandler FormBrowser.FormCloseTab, AddressOf CloseDocument
                        RemoveHandler FormBrowser.RefreshTask, AddressOf fbRefreshTask
                        RemoveHandler FormBrowser.ShowOriginal, AddressOf fbShowOriginal
                        RemoveHandler FormBrowser.ReloadAsociatedResult, AddressOf fbReloadAsociatedResult
                        FormBrowser.Dispose()
                        FormBrowser = Nothing
                    End If
                    If OffWB IsNot Nothing AndAlso OffWB.IsDisposed = False Then
                        RemoveHandler OffWB.eAutomaticNewVersion, AddressOf AutomaticNewVersionHandler
                        OffWB.Dispose()
                        'OffWB = Nothing
                    End If
                    If MsgWB IsNot Nothing Then
                        MsgWB.Controls.Clear()
                        MsgWB.Dispose()
                        'MsgWB = Nothing
                    End If
                    If rtfViewer IsNot Nothing AndAlso rtfViewer.IsDisposed = False Then
                        rtfViewer.Dispose()
                        'rtfViewer = Nothing
                    End If
                    If Prev IsNot Nothing AndAlso Prev.IsDisposed = False Then
                        Prev.Controls.Clear()
                        Prev.Dispose()
                        'Prev = Nothing
                    End If
                    If ParentTabControl IsNot Nothing AndAlso ParentTabControl.IsDisposed = False Then
                        ParentTabControl.Controls.Clear()
                        ParentTabControl.Dispose()
                        'ParentTabControl = Nothing
                    End If
                    If RotatedImg IsNot Nothing Then
                        RotatedImg.Dispose()
                        'RotatedImg = Nothing
                    End If
                    If bt IsNot Nothing Then
                        bt.Dispose()
                        'bt = Nothing
                    End If
                    If bt2 IsNot Nothing Then
                        bt2.Dispose()
                        'bt2 = Nothing
                    End If
                    If Txt IsNot Nothing AndAlso Txt.IsDisposed = False Then
                        Txt.Controls.Clear()
                        Txt.Dispose()
                        'Txt = Nothing
                    End If
                    If Txt2 IsNot Nothing AndAlso Txt2.IsDisposed = False Then
                        Txt2.Controls.Clear()
                        Txt2.Dispose()
                        'Txt2 = Nothing
                    End If
                    If WFDesigner IsNot Nothing AndAlso WFDesigner.IsDisposed = False Then
                        WFDesigner.Controls.Clear()
                        WFDesigner.Dispose()
                        'WFDesigner = Nothing
                    End If
                    If ZControl_PanelLinks IsNot Nothing AndAlso ZControl_PanelLinks.IsDisposed = False Then
                        ZControl_PanelLinks.Controls.Clear()
                        ZControl_PanelLinks.Dispose()
                        'ZControl_PanelLinks = Nothing
                    End If
                    If P IsNot Nothing AndAlso P.IsDisposed = False Then
                        P.Controls.Clear()
                        P.Dispose()
                        'P = Nothing
                    End If
                    If ExcelOffWB IsNot Nothing Then
                        ExcelOffWB.Dispose()
                        ExcelOffWB = Nothing
                    End If
                    If WordOffWB IsNot Nothing Then
                        WordOffWB.Dispose()
                        WordOffWB = Nothing
                    End If
                    If PdfBrowser IsNot Nothing AndAlso PdfBrowser.IsDisposed = False Then
                        PdfBrowser.Dispose()
                        PdfBrowser = Nothing
                    End If

                    'For i As Int32 = 0 To Me.Controls.Count - 1
                    '    If Me.Controls(i) IsNot Nothing AndAlso TypeOf (Me.Controls(i)) Is IDisposable Then
                    '        Me.Controls(i).Dispose()
                    '    End If
                    'Next
                End If
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
            Try
                DisposeNotas()
                disposeRotation()
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
            Try
                MyBase.Dispose(disposing)
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
            isDisposed = True
        End If
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    Friend WithEvents LayoutButton As ToolStripButton
    Friend WithEvents txtNumPag As ToolStripComboBox
    Friend WithEvents DropDownMenuItem1 As ToolStripDropDownButton
    Friend WithEvents NETRON16 As ToolStripMenuItem
    Friend WithEvents MenuButtonItem7 As ToolStripMenuItem
    Friend WithEvents lblPagin As ToolStripLabel
    Friend WithEvents ToolBar1 As ZToolBar
    Friend WithEvents ButtonItem1 As ToolStripButton
    Friend WithEvents ButtonItem2 As ToolStripButton
    Friend WithEvents ButtonItem3 As ToolStripButton
    Friend WithEvents ButtonItem4 As ToolStripButton
    Friend WithEvents ButtonItem5 As ToolStripButton
    Friend WithEvents ButtonItem6 As ToolStripButton
    Friend WithEvents ButtonItem7 As ToolStripButton
    Friend WithEvents ButtonItem8 As ToolStripButton
    Friend WithEvents ButtonItem9 As ToolStripButton
    Friend WithEvents ButtonItem10 As ToolStripButton
    Friend WithEvents btnReplace As ToolStripButton
    Friend WithEvents ButtonItem12 As ToolStripMenuItem
    Friend WithEvents ButtonItem13 As ToolStripMenuItem
    Friend WithEvents ButtonItem14 As ToolStripMenuItem
    Friend WithEvents ButtonItem15 As ToolStripButton
    Friend WithEvents ButtonItem16 As ToolStripButton
    Friend WithEvents ButtonItem17 As ToolStripButton
    Friend WithEvents ButtonItem18 As ToolStripButton
    Friend WithEvents ButtonItem19 As ToolStripButton
    Friend WithEvents ButtonItem20 As ToolStripButton
    Friend WithEvents Buttonclose As ToolStripButton
    Friend WithEvents btnClose As ToolStripButton
    Friend WithEvents ButtonDocumental As ToolStripButton
    Friend WithEvents BtnReplaceDocument As ToolStripButton
    Friend WithEvents InfoView As ToolStripLabel
    Friend WithEvents DesignSandBar As ZToolBar
    Friend WithEvents btnPrint As ToolStripButton
    Friend WithEvents btnFullScreen As ToolStripButton
    Friend WithEvents btnShow As ToolStripButton
    Friend WithEvents btnOriginalFile As ToolStripButton
    Friend WithEvents BPrint As ToolStripButton
    Friend WithEvents ToolStripContainer1 As ToolStripContainer
    Public WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    'Public WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents ToolStripSeparator13 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents ToolStripSeparator12 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents separateClose As System.Windows.Forms.ToolStripSeparator
    Public WithEvents separateDocumentLabels As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnRefresh As ToolStripButton
    'Friend WithEvents BItemGoToSearch As ToolStripButton
    Public WithEvents btnAdjuntarEmail As System.Windows.Forms.ToolStripButton
    Public WithEvents btnSaveAs As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnFlagAsImportant As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnFlagAsFavorite As System.Windows.Forms.ToolStripButton
    'Public WithEvents btnAdjuntarMensaje As System.Windows.Forms.ToolStripButton
    Public WithEvents btnAgregarDocumentoCarpeta As System.Windows.Forms.ToolStripButton
    Public WithEvents btnVerVersionesDelDocumento As System.Windows.Forms.ToolStripButton
    Public WithEvents btnAgregarUnaNuevaVersionDelDocuemto As System.Windows.Forms.ToolStripButton
    Public WithEvents btnGotoWorkflow As System.Windows.Forms.ToolStripButton
    Public WithEvents btnChangePosition As System.Windows.Forms.ToolStripButton
    'Friend WithEvents btnGoToWF As System.Windows.Forms.ToolStripButton
    ' Friend WithEvents btnGotoWfdb As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPrintPreview As System.Windows.Forms.ToolStripButton
    Public WithEvents btnGotoHelp As System.Windows.Forms.ToolStripButton
    Public WithEvents btnDecryptDocument As System.Windows.Forms.ToolStripButton
    Public WithEvents lblEncryptFile As System.Windows.Forms.ToolStripLabel
    Public WithEvents btnOpenDocument As System.Windows.Forms.ToolStripButton

    <DebuggerStepThrough()> Private Sub InitializeComponent()
        ButtonDocumental = New System.Windows.Forms.ToolStripButton()
        Buttonclose = New System.Windows.Forms.ToolStripButton()
        btnClose = New System.Windows.Forms.ToolStripButton()
        LayoutButton = New System.Windows.Forms.ToolStripButton()
        btnReplace = New System.Windows.Forms.ToolStripButton()
        DropDownMenuItem1 = New ToolStripDropDownButton()
        ButtonItem12 = New System.Windows.Forms.ToolStripMenuItem()
        ButtonItem13 = New System.Windows.Forms.ToolStripMenuItem()
        ButtonItem14 = New System.Windows.Forms.ToolStripMenuItem()
        NETRON16 = New System.Windows.Forms.ToolStripMenuItem()
        MenuButtonItem7 = New System.Windows.Forms.ToolStripMenuItem()
        txtNumPag = New System.Windows.Forms.ToolStripComboBox()
        ButtonItem20 = New System.Windows.Forms.ToolStripButton()
        lblPagin = New System.Windows.Forms.ToolStripLabel()
        ButtonItem18 = New System.Windows.Forms.ToolStripButton()
        ButtonItem19 = New System.Windows.Forms.ToolStripButton()
        DesignSandBar = New ZToolBar()
        separateClose = New System.Windows.Forms.ToolStripSeparator()
        btnFlagAsFavorite = New System.Windows.Forms.ToolStripButton()
        btnFlagAsImportant = New System.Windows.Forms.ToolStripButton()
        separateDocumentLabels = New System.Windows.Forms.ToolStripSeparator()
        BPrint = New System.Windows.Forms.ToolStripButton()
        BtnReplaceDocument = New System.Windows.Forms.ToolStripButton()
        ButtonItem1 = New System.Windows.Forms.ToolStripButton()
        ButtonItem2 = New System.Windows.Forms.ToolStripButton()
        ButtonItem3 = New System.Windows.Forms.ToolStripButton()
        ButtonItem4 = New System.Windows.Forms.ToolStripButton()
        ButtonItem6 = New System.Windows.Forms.ToolStripButton()
        ButtonItem7 = New System.Windows.Forms.ToolStripButton()
        ButtonItem8 = New System.Windows.Forms.ToolStripButton()
        ButtonItem9 = New System.Windows.Forms.ToolStripButton()
        ButtonItem10 = New System.Windows.Forms.ToolStripButton()
        ButtonItem16 = New System.Windows.Forms.ToolStripButton()
        ButtonItem17 = New System.Windows.Forms.ToolStripButton()
        'Me.btnGotoWfdb = New System.Windows.Forms.ToolStripButton()
        ButtonItem5 = New System.Windows.Forms.ToolStripButton()
        btnSaveAs = New System.Windows.Forms.ToolStripButton()
        ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        btnAdjuntarEmail = New System.Windows.Forms.ToolStripButton()
        ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        btnAgregarDocumentoCarpeta = New System.Windows.Forms.ToolStripButton()
        ToolStripSeparator13 = New System.Windows.Forms.ToolStripSeparator()
        btnVerVersionesDelDocumento = New System.Windows.Forms.ToolStripButton()
        btnAgregarUnaNuevaVersionDelDocuemto = New System.Windows.Forms.ToolStripButton()
        ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
        btnGotoWorkflow = New System.Windows.Forms.ToolStripButton()
        btnChangePosition = New System.Windows.Forms.ToolStripButton()
        ButtonItem15 = New System.Windows.Forms.ToolStripButton()
        ToolBar1 = New ZToolBar()
        btnPrint = New System.Windows.Forms.ToolStripButton()
        btnPrintPreview = New System.Windows.Forms.ToolStripButton()
        btnFullScreen = New System.Windows.Forms.ToolStripButton()
        btnOriginalFile = New System.Windows.Forms.ToolStripButton()
        btnShow = New System.Windows.Forms.ToolStripButton()
        btnRefresh = New System.Windows.Forms.ToolStripButton()
        'Me.btnGoToWF = New System.Windows.Forms.ToolStripButton()
        InfoView = New System.Windows.Forms.ToolStripLabel()
        lblEncryptFile = New System.Windows.Forms.ToolStripLabel()
        btnDecryptDocument = New System.Windows.Forms.ToolStripButton()
        ToolStripContainer1 = New ToolStripContainer()
        btnGotoHelp = New System.Windows.Forms.ToolStripButton()
        btnOpenDocument = New System.Windows.Forms.ToolStripButton()
        DesignSandBar.SuspendLayout()
        ToolBar1.SuspendLayout()
        ToolStripContainer1.TopToolStripPanel.SuspendLayout()
        ToolStripContainer1.SuspendLayout()
        SuspendLayout()
        '
        'btnOpenDocument
        '
        btnOpenDocument.Name = "btnOpenDocument"
        btnOpenDocument.Size = New System.Drawing.Size(59, 22)
        btnOpenDocument.Tag = "OpenDocument"
        btnOpenDocument.Text = "Abrir Documento"
        btnOpenDocument.ToolTipText = "Abrir Documento"
        btnOpenDocument.Visible = True
        RemoveHandler btnOpenDocument.Click, AddressOf btnOpenDocument_click
        AddHandler btnOpenDocument.Click, AddressOf btnOpenDocument_click
        '
        'ButtonDocumental
        '
        ButtonDocumental.Name = "ButtonDocumental"
        ButtonDocumental.Size = New System.Drawing.Size(111, 22)
        ButtonDocumental.Tag = "VerDocumental"
        ButtonDocumental.Text = "Ver en Resultados"
        ButtonDocumental.ToolTipText = "Ver en Resultados"
        '
        'Buttonclose
        '
        Buttonclose.Image = Global.Zamba.Viewers.My.Resources.delete2
        Buttonclose.Name = "Buttonclose"
        Buttonclose.Size = New System.Drawing.Size(59, 22)
        Buttonclose.Tag = "Cerrar"
        Buttonclose.Text = "Cerrar"
        Buttonclose.ToolTipText = "Cerrar"
        '
        'btnClose
        '
        btnClose.Image = Global.Zamba.Viewers.My.Resources.delete2
        btnClose.Name = "btnClose"
        btnClose.Size = New System.Drawing.Size(65, 22)
        btnClose.Tag = "Cerrar"
        btnClose.Text = "Cerrar"
        btnClose.ToolTipText = "Cerrar"
        '
        'LayoutButton
        '
        LayoutButton.DisplayStyle = ToolStripItemDisplayStyle.Image
        LayoutButton.Image = Global.Zamba.Viewers.My.Resources.appbar_layout_expand_right_variant
        LayoutButton.Name = "LayoutButton"
        LayoutButton.Size = New System.Drawing.Size(23, 32)
        LayoutButton.Tag = "DIVIDIR"
        LayoutButton.Text = " Dividir"
        LayoutButton.ToolTipText = "Dividir Pantalla"
        '
        'btnReplace
        '
        btnReplace.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnReplace.Image = Global.Zamba.Viewers.My.Resources.appbar_page_edit
        btnReplace.Name = "btnReplace"
        btnReplace.Size = New System.Drawing.Size(23, 32)
        btnReplace.Tag = "BtnReplace"
        btnReplace.ToolTipText = "Cambiar Documento"
        btnReplace.Text = "Cambiar Documento"
        '
        'DropDownMenuItem1
        '
        DropDownMenuItem1.DisplayStyle = ToolStripItemDisplayStyle.Image
        DropDownMenuItem1.DropDownItems.AddRange(New ToolStripItem() {ButtonItem12, ButtonItem13, ButtonItem14, NETRON16, MenuButtonItem7})
        DropDownMenuItem1.Image = Global.Zamba.Viewers.My.Resources.Resources.note_add
        DropDownMenuItem1.Name = "DropDownMenuItem1"
        DropDownMenuItem1.Size = New System.Drawing.Size(29, 22)
        DropDownMenuItem1.Text = "NOTAS"
        '
        'ButtonItem12
        '
        ButtonItem12.Name = "ButtonItem12"
        ButtonItem12.Size = New System.Drawing.Size(218, 22)
        ButtonItem12.Tag = "BtnNota"
        ButtonItem12.Text = "MODO NOTA"
        '
        'ButtonItem13
        '
        ButtonItem13.Name = "ButtonItem13"
        ButtonItem13.Size = New System.Drawing.Size(218, 22)
        ButtonItem13.Tag = "BtnFirma"
        ButtonItem13.Text = "MODO FIRMA DE USUARIO"
        '
        'ButtonItem14
        '
        ButtonItem14.Name = "ButtonItem14"
        ButtonItem14.Size = New System.Drawing.Size(218, 22)
        ButtonItem14.Tag = "BtnOCR"
        ButtonItem14.Text = "MODO OCR"
        '
        'NETRON16
        '
        NETRON16.Name = "NETRON16"
        NETRON16.Size = New System.Drawing.Size(218, 22)
        NETRON16.Tag = "BtnNetron"
        NETRON16.Text = "MODO RECTANGULO"
        '
        'MenuButtonItem7
        '
        MenuButtonItem7.Enabled = False
        MenuButtonItem7.Name = "MenuButtonItem7"
        MenuButtonItem7.Size = New System.Drawing.Size(218, 22)
        MenuButtonItem7.Text = "NUEVO RECTANGULO"
        '
        'txtNumPag
        '
        txtNumPag.Name = "txtNumPag"
        txtNumPag.Size = New System.Drawing.Size(121, 25)
        '
        'ButtonItem20
        '
        ButtonItem20.DisplayStyle = ToolStripItemDisplayStyle.Image
        ButtonItem20.Image = Global.Zamba.Viewers.My.Resources.Resources.bullet_triangle_blue
        ButtonItem20.Name = "ButtonItem20"
        ButtonItem20.Size = New System.Drawing.Size(23, 32)
        ButtonItem20.Tag = "IR"
        ButtonItem20.Text = "IR"
        ButtonItem20.ToolTipText = "IR A LA PAGINA ESPECIFICADA"
        '
        'lblPagin
        '
        lblPagin.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        lblPagin.Name = "lblPagin"
        lblPagin.Size = New System.Drawing.Size(41, 22)
        lblPagin.Text = "0 DE 0"
        '
        'ButtonItem18
        '
        ButtonItem18.DisplayStyle = ToolStripItemDisplayStyle.Image
        ButtonItem18.Image = Global.Zamba.Viewers.My.Resources.Resources.arrow_right_blue
        ButtonItem18.Name = "ButtonItem18"
        ButtonItem18.Size = New System.Drawing.Size(23, 32)
        ButtonItem18.Tag = "BtnNext"
        ButtonItem18.ToolTipText = "IR A PAGINA SIGUIENTE"
        '
        'ButtonItem19
        '
        ButtonItem19.DisplayStyle = ToolStripItemDisplayStyle.Image
        ButtonItem19.Image = Global.Zamba.Viewers.My.Resources.Resources.media_end
        ButtonItem19.Name = "ButtonItem19"
        ButtonItem19.Size = New System.Drawing.Size(23, 32)
        ButtonItem19.Tag = "btnlastpage"
        ButtonItem19.ToolTipText = "IR A ULTIMA PAGINA"
        '
        'DesignSandBar
        '
        DesignSandBar.BackColor = System.Drawing.Color.Silver
        DesignSandBar.Dock = System.Windows.Forms.DockStyle.None
        DesignSandBar.Font = New Font("Verdana", 7.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        DesignSandBar.Items.AddRange(New ToolStripItem() {Buttonclose, BPrint, BtnReplaceDocument, ButtonItem1, ButtonDocumental, ButtonItem2, ButtonItem3, ButtonItem4, ButtonItem6, ButtonItem7, ButtonItem8, ButtonItem9, ButtonItem10, ButtonItem16, ButtonItem17, txtNumPag, ButtonItem20, lblPagin, ButtonItem18, ButtonItem19, DropDownMenuItem1})
        DesignSandBar.Location = New System.Drawing.Point(3, 0)
        DesignSandBar.Name = "DesignSandBar"
        DesignSandBar.Size = New System.Drawing.Size(758, 25)
        DesignSandBar.TabIndex = 1
        DesignSandBar.GripStyle = ToolStripGripStyle.Hidden
        DesignSandBar.Renderer = New Zamba.AppBlock.MyStripRender()
        '
        'separateClose
        '
        separateClose.Name = "separateClose"
        separateClose.Size = New System.Drawing.Size(6, 25)
        '
        'btnFlagAsFavorite
        '
        btnFlagAsFavorite.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnFlagAsFavorite.Image = Global.Zamba.Viewers.My.Resources.appbar_cards_heart
        btnFlagAsFavorite.ImageTransparentColor = System.Drawing.Color.Magenta
        btnFlagAsFavorite.Name = "btnFlagAsFavorite"
        btnFlagAsFavorite.Size = New System.Drawing.Size(32, 32)
        btnFlagAsFavorite.Tag = "FAVORITO"
        btnFlagAsFavorite.ToolTipText = "Marcar como Favorito"
        btnFlagAsFavorite.Visible = False
        btnFlagAsFavorite.Height = 32

        '
        'btnFlagAsImportant
        '
        btnFlagAsImportant.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnFlagAsImportant.Image = Global.Zamba.Viewers.My.Resources.appbar_location
        btnFlagAsImportant.ImageTransparentColor = System.Drawing.Color.Magenta
        btnFlagAsImportant.Name = "btnFlagAsImportant"
        btnFlagAsImportant.Size = New System.Drawing.Size(23, 32)
        btnFlagAsImportant.Tag = "IMPORTANTE"
        btnFlagAsImportant.ToolTipText = "Marcar como Importante"
        btnFlagAsImportant.Visible = False
        '
        'separateDocumentLabels
        '
        separateDocumentLabels.Name = "separateDocumentLabels"
        separateDocumentLabels.Size = New System.Drawing.Size(6, 25)
        '
        'BPrint
        '
        BPrint.DisplayStyle = ToolStripItemDisplayStyle.Image
        BPrint.Image = Global.Zamba.Viewers.My.Resources.Resources.printer
        BPrint.Name = "BPrint"
        BPrint.Size = New System.Drawing.Size(23, 32)
        BPrint.Tag = "IMPRIMIR"
        BPrint.Text = "Imprimir"
        BPrint.ToolTipText = "Imprimir"
        '
        'BtnReplaceDocument
        '
        BtnReplaceDocument.DisplayStyle = ToolStripItemDisplayStyle.Image
        BtnReplaceDocument.Image = Global.Zamba.Viewers.My.Resources.appbar_page_edit
        BtnReplaceDocument.Name = "BtnReplaceDocument"
        BtnReplaceDocument.Size = New System.Drawing.Size(23, 32)
        BtnReplaceDocument.Tag = "BtnReplace"
        BtnReplaceDocument.ToolTipText = "Cambiar Documento"
        BtnReplaceDocument.Text = "Cambiar documento"

        '
        'ButtonItem1
        '
        ButtonItem1.DisplayStyle = ToolStripItemDisplayStyle.Image
        ButtonItem1.Image = Global.Zamba.Viewers.My.Resources.Resources.presentation
        ButtonItem1.Name = "ButtonItem1"
        ButtonItem1.Size = New System.Drawing.Size(23, 32)
        ButtonItem1.Tag = "btnPreview"
        ButtonItem1.ToolTipText = "ABRIR IMAGEN EN VENTANA NUEVA"
        '
        'ButtonItem2
        '
        ButtonItem2.DisplayStyle = ToolStripItemDisplayStyle.Image
        ButtonItem2.Image = Global.Zamba.Viewers.My.Resources.Resources.zoom_out
        ButtonItem2.Name = "ButtonItem2"
        ButtonItem2.Size = New System.Drawing.Size(23, 32)
        ButtonItem2.Tag = "Minus"
        ButtonItem2.ToolTipText = "DISMINUIR ZOOM"
        '
        'ButtonItem3
        '
        ButtonItem3.DisplayStyle = ToolStripItemDisplayStyle.Image
        ButtonItem3.Image = Global.Zamba.Viewers.My.Resources.Resources.zoom_in
        ButtonItem3.Name = "ButtonItem3"
        ButtonItem3.Size = New System.Drawing.Size(23, 32)
        ButtonItem3.Tag = "Plus"
        ButtonItem3.ToolTipText = "AUMENTAR ZOOM"
        '
        'ButtonItem4
        '
        ButtonItem4.DisplayStyle = ToolStripItemDisplayStyle.Image
        ButtonItem4.Image = Global.Zamba.Viewers.My.Resources.Resources.lock_view
        ButtonItem4.Name = "ButtonItem4"
        ButtonItem4.Size = New System.Drawing.Size(23, 32)
        ButtonItem4.Tag = "BtnLockZoom"
        ButtonItem4.ToolTipText = "BLOQUEAR ZOOM"
        '
        'ButtonItem6
        '
        ButtonItem6.DisplayStyle = ToolStripItemDisplayStyle.Image
        ButtonItem6.Image = Global.Zamba.Viewers.My.Resources.Resources.undo
        ButtonItem6.Name = "ButtonItem6"
        ButtonItem6.Size = New System.Drawing.Size(23, 32)
        ButtonItem6.Tag = "Rotate_90"
        ButtonItem6.ToolTipText = "ROTAR A LA IZQUIERDA"
        '
        'ButtonItem7
        '
        ButtonItem7.DisplayStyle = ToolStripItemDisplayStyle.Image
        ButtonItem7.Image = Global.Zamba.Viewers.My.Resources.Resources.redo
        ButtonItem7.Name = "ButtonItem7"
        ButtonItem7.Size = New System.Drawing.Size(23, 32)
        ButtonItem7.Tag = "Rotate90"
        ButtonItem7.ToolTipText = "ROTAR A LA DERECHA"
        '
        'ButtonItem8
        '
        ButtonItem8.DisplayStyle = ToolStripItemDisplayStyle.Image
        ButtonItem8.Image = Global.Zamba.Viewers.My.Resources.Resources.layout_center
        ButtonItem8.Name = "ButtonItem8"
        ButtonItem8.Size = New System.Drawing.Size(23, 32)
        ButtonItem8.Tag = "btnTamNormal"
        ButtonItem8.ToolTipText = "TAMAÑO ORIGINAL"
        '
        'ButtonItem9
        '
        ButtonItem9.DisplayStyle = ToolStripItemDisplayStyle.Image
        ButtonItem9.Image = Global.Zamba.Viewers.My.Resources.Resources.layout_horizontal
        ButtonItem9.Name = "ButtonItem9"
        ButtonItem9.Size = New System.Drawing.Size(23, 32)
        ButtonItem9.Tag = "BtnStrech"
        ButtonItem9.ToolTipText = "AJUSTAR ANCHO"
        '
        'ButtonItem10
        '
        ButtonItem10.DisplayStyle = ToolStripItemDisplayStyle.Image
        ButtonItem10.Image = Global.Zamba.Viewers.My.Resources.Resources.layout_vertical
        ButtonItem10.Name = "ButtonItem10"
        ButtonItem10.Size = New System.Drawing.Size(23, 32)
        ButtonItem10.Tag = "btnAltoPantalla"
        ButtonItem10.ToolTipText = "AJUSTAR ALTO"
        '
        'ButtonItem16
        '
        ButtonItem16.DisplayStyle = ToolStripItemDisplayStyle.Image
        ButtonItem16.Image = Global.Zamba.Viewers.My.Resources.Resources.media_beginning
        ButtonItem16.Name = "ButtonItem16"
        ButtonItem16.Size = New System.Drawing.Size(23, 32)
        ButtonItem16.Tag = "btnfirstpage"
        ButtonItem16.ToolTipText = "IR A PRIMER PAGINA"
        '
        'ButtonItem17
        '
        ButtonItem17.DisplayStyle = ToolStripItemDisplayStyle.Image
        ButtonItem17.Image = Global.Zamba.Viewers.My.Resources.Resources.arrow_left_blue1
        ButtonItem17.Name = "ButtonItem17"
        ButtonItem17.Size = New System.Drawing.Size(23, 32)
        ButtonItem17.Tag = "btnBefore"
        ButtonItem17.ToolTipText = "IR A PAGINA ANTERIOR"
        '
        'btnGotoWfdb
        '
        'Me.btnGotoWfdb.DisplayStyle = ToolStripItemDisplayStyle.Image
        'Me.btnGotoWfdb.ForeColor = System.Drawing.Color.Transparent
        'Me.btnGotoWfdb.Image = Global.Zamba.Viewers.My.Resources.Resources.btnGotoWorkflow_Image
        'Me.btnGotoWfdb.ImageTransparentColor = System.Drawing.Color.Magenta
        'Me.btnGotoWfdb.Name = "btnGotoWfdb"
        'Me.btnGotoWfdb.Size = New System.Drawing.Size(23, 20)
        'Me.btnGotoWfdb.Tag = "GOTOWF"
        'Me.btnGotoWfdb.Text = "ToolStripButton1"
        'Me.btnGotoWfdb.ToolTipText = "VER DOCUMENTO EN TAREAS"
        '
        'ButtonItem5
        '
        ButtonItem5.Name = "ButtonItem5"
        ButtonItem5.Size = New System.Drawing.Size(23, 4)
        '
        'btnSaveAs
        '
        btnSaveAs.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnSaveAs.Image = Global.Zamba.Viewers.My.Resources.appbar_save
        btnSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta
        btnSaveAs.Name = "btnSaveAs"
        btnSaveAs.Size = New System.Drawing.Size(23, 20)
        btnSaveAs.Tag = "GUARDARDOCUMENTOCOMO"
        btnSaveAs.Text = "Guardar Como"
        btnSaveAs.ToolTipText = "Guardar Como"
        '
        'ToolStripSeparator7
        '
        ToolStripSeparator7.Name = "ToolStripSeparator7"
        ToolStripSeparator7.Size = New System.Drawing.Size(6, 25)
        '
        'btnAdjuntarEmail
        '
        btnAdjuntarEmail.Font = Zamba.AppBlock.ZambaUIHelpers.GetFontFamily()
        btnAdjuntarEmail.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        btnAdjuntarEmail.Image = Global.Zamba.Viewers.My.Resources.appbar_email_hardedge
        btnAdjuntarEmail.ImageTransparentColor = System.Drawing.Color.Magenta
        btnAdjuntarEmail.Name = "btnAdjuntarEmail"
        btnAdjuntarEmail.Size = New System.Drawing.Size(69, 20)
        btnAdjuntarEmail.Tag = "EMAIL"
        btnAdjuntarEmail.Text = "Envio Mail"
        btnAdjuntarEmail.ToolTipText = "Enviar por mail"
        '
        'ToolStripSeparator8
        '
        ToolStripSeparator8.Name = "ToolStripSeparator8"
        ToolStripSeparator8.Size = New System.Drawing.Size(6, 25)
        '
        'btnAgregarDocumentoCarpeta
        '
        btnAgregarDocumentoCarpeta.Font = Zamba.AppBlock.ZambaUIHelpers.GetFontFamily()
        btnAgregarDocumentoCarpeta.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        btnAgregarDocumentoCarpeta.Image = Global.Zamba.Viewers.My.Resources.appbar_page_add
        btnAgregarDocumentoCarpeta.ImageTransparentColor = System.Drawing.Color.Magenta
        btnAgregarDocumentoCarpeta.Name = "btnAgregarDocumentoCarpeta"
        btnAgregarDocumentoCarpeta.Size = New System.Drawing.Size(115, 20)
        btnAgregarDocumentoCarpeta.Tag = "AGREGARACARPETA"
        btnAgregarDocumentoCarpeta.Text = "Incorporar documento"
        btnAgregarDocumentoCarpeta.ToolTipText = "Incorporar nuevo documento a la carpeta"
        '
        'ToolStripSeparator13
        '
        ToolStripSeparator13.Name = "ToolStripSeparator13"
        ToolStripSeparator13.Size = New System.Drawing.Size(6, 105)
        '
        'btnVerVersionesDelDocumento
        '
        btnVerVersionesDelDocumento.Font = Zamba.AppBlock.ZambaUIHelpers.GetFontFamily()
        btnVerVersionesDelDocumento.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        btnVerVersionesDelDocumento.Image = Global.Zamba.Viewers.My.Resources.Resources.blue_documents
        btnVerVersionesDelDocumento.ImageTransparentColor = System.Drawing.Color.Magenta
        btnVerVersionesDelDocumento.Name = "btnVerVersionesDelDocumento"
        btnVerVersionesDelDocumento.Size = New System.Drawing.Size(65, 20)
        btnVerVersionesDelDocumento.Tag = "VERSIONESDELDOCUMENTO"
        btnVerVersionesDelDocumento.Text = "Versiones"
        btnVerVersionesDelDocumento.ToolTipText = "Ver versiones del documento"
        '
        'btnAgregarUnaNuevaVersionDelDocuemto
        '
        btnAgregarUnaNuevaVersionDelDocuemto.Font = Zamba.AppBlock.ZambaUIHelpers.GetFontFamily()
        btnAgregarUnaNuevaVersionDelDocuemto.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        btnAgregarUnaNuevaVersionDelDocuemto.Image = Global.Zamba.Viewers.My.Resources.Resources.blue_document__plus
        btnAgregarUnaNuevaVersionDelDocuemto.ImageTransparentColor = System.Drawing.Color.Magenta
        btnAgregarUnaNuevaVersionDelDocuemto.Name = "btnAgregarUnaNuevaVersionDelDocuemto"
        btnAgregarUnaNuevaVersionDelDocuemto.Size = New System.Drawing.Size(86, 20)
        btnAgregarUnaNuevaVersionDelDocuemto.Tag = "AGREGARNUEVAVERSION"
        btnAgregarUnaNuevaVersionDelDocuemto.Text = "Nueva version"
        btnAgregarUnaNuevaVersionDelDocuemto.ToolTipText = "Agregar nueva version del documento"
        '
        'ToolStripSeparator12
        '
        ToolStripSeparator12.Name = "ToolStripSeparator12"
        ToolStripSeparator12.Size = New System.Drawing.Size(6, 105)
        '
        'btnGotoWorkflow
        '
        btnGotoWorkflow.Font = Zamba.AppBlock.ZambaUIHelpers.GetFontFamily()
        btnGotoWorkflow.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        btnGotoWorkflow.Image = Global.Zamba.Viewers.My.Resources.Resources.gears_run
        btnGotoWorkflow.ImageTransparentColor = System.Drawing.Color.Magenta
        btnGotoWorkflow.Name = "btnGotoWorkflow"
        btnGotoWorkflow.Size = New System.Drawing.Size(114, 20)
        btnGotoWorkflow.Tag = "IRAWORKFLOW"
        btnGotoWorkflow.Text = "Trabajar con la Tarea"
        btnGotoWorkflow.ToolTipText = "Trabajar con la Tarea"
        '
        'btnChangePosition
        '
        btnChangePosition.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnChangePosition.Font = Zamba.AppBlock.ZambaUIHelpers.GetFontFamily()
        btnChangePosition.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        btnChangePosition.Image = Global.Zamba.Viewers.My.Resources.appbar_layout_expand_right_variant
        btnChangePosition.ImageTransparentColor = System.Drawing.Color.Magenta
        btnChangePosition.Name = "btnChangePosition"
        btnChangePosition.Size = New System.Drawing.Size(23, 20)
        btnChangePosition.Tag = "CHANGEPOSITION"
        btnChangePosition.ToolTipText = "Mostrar Listado solo o Listado y Documentos"
        '
        'ButtonItem15
        '
        ButtonItem15.Name = "ButtonItem15"
        ButtonItem15.Size = New System.Drawing.Size(23, 23)
        '
        'ToolBar1
        '
        ToolBar1.AllowItemReorder = True
        ToolBar1.BackColor = Color.White
        ToolBar1.ImageScalingSize = New System.Drawing.Size(32, 32)
        ToolBar1.Dock = System.Windows.Forms.DockStyle.None
        ToolBar1.Font = AppBlock.ZambaUIHelpers.GetFontFamily
        ToolBar1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        ToolBar1.Items.AddRange(New ToolStripItem() {btnOpenDocument, btnClose, separateClose, btnPrint, btnPrintPreview, btnFullScreen, LayoutButton, btnOriginalFile, btnShow, btnRefresh, separateDocumentLabels, btnFlagAsFavorite, btnFlagAsImportant, InfoView, lblEncryptFile, btnDecryptDocument, btnReplace})
        ToolBar1.Location = New System.Drawing.Point(0, 26)
        ToolBar1.Name = "ToolBar1"
        ToolBar1.Size = New System.Drawing.Size(594, 32)
        ToolBar1.TabIndex = 169
        ToolBar1.Visible = False
        ToolBar1.GripStyle = ToolStripGripStyle.Hidden
        ToolBar1.Renderer = New Zamba.AppBlock.MyStripRender()
        ToolBar1.Height = 32
        ToolBar1.AutoSize = True
        ToolBar1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow
        ToolBar1.Stretch = True
        '
        'btnPrint
        '
        btnPrint.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnPrint.Image = Global.Zamba.Viewers.My.Resources.appbar_printer_text
        btnPrint.Name = "btnPrint"
        btnPrint.Size = New System.Drawing.Size(23, 32)
        btnPrint.Tag = "IMPRIMIR"
        btnPrint.Text = " Imprimir"
        btnPrint.ToolTipText = "Imprimir documento"
        '
        'btnPrintPreview
        '
        btnPrintPreview.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnPrintPreview.Image = Global.Zamba.Viewers.My.Resources.appbar_page_search
        btnPrintPreview.ImageTransparentColor = System.Drawing.Color.White
        btnPrintPreview.Name = "btnPrintPreview"
        btnPrintPreview.Size = New System.Drawing.Size(23, 32)
        btnPrintPreview.Tag = "PREVISUALIZAR"
        btnPrintPreview.Text = "Previsualizar"
        btnPrintPreview.ToolTipText = "Imprimir formulario "
        '
        'btnFullScreen
        '
        btnFullScreen.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnFullScreen.Image = Global.Zamba.Viewers.My.Resources.appbar_fullscreen
        btnFullScreen.Name = "btnFullScreen"
        btnFullScreen.Size = New System.Drawing.Size(23, 32)
        btnFullScreen.Tag = "PANTALLACOMPLETA"
        btnFullScreen.Text = " Pantalla Completa"
        btnFullScreen.ToolTipText = "Pantalla Completa"
        '
        'btnOriginalFile
        '
        btnOriginalFile.Image = Global.Zamba.Viewers.My.Resources.appbar_page_text
        btnOriginalFile.Name = "btnOriginalFile"
        btnOriginalFile.Size = New System.Drawing.Size(95, 22)
        btnOriginalFile.Tag = "VERORIGINAL"
        btnOriginalFile.Text = "Ver Adjunto"
        btnOriginalFile.ToolTipText = "Ver el Adjunto"
        btnOriginalFile.Visible = False
        '
        'btnShow
        '
        btnShow.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnShow.Image = Global.Zamba.Viewers.My.Resources.Resources.Settings
        btnShow.Name = "btnShow"
        btnShow.Size = New System.Drawing.Size(23, 32)
        btnShow.Tag = "MOSTRAR"
        btnShow.Text = "Mostrar Barras de Herramientas de Office"
        btnShow.ToolTipText = "Mostrar Barras de Herramientas de Office"
        btnShow.Visible = False
        '
        'btnRefresh
        '
        btnRefresh.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnRefresh.Image = Global.Zamba.Viewers.My.Resources.appbar_refresh
        btnRefresh.Name = "btnRefresh"
        btnRefresh.Size = New System.Drawing.Size(23, 32)
        btnRefresh.Text = "Actualizar"
        btnRefresh.ToolTipText = "Actualizar Datos"
        '
        'btnGoToWF
        ''
        'Me.btnGoToWF.DisplayStyle = ToolStripItemDisplayStyle.Image
        'Me.btnGoToWF.Image = Global.Zamba.Viewers.My.Resources.Resources.btnGotoWorkflow_Image
        'Me.btnGoToWF.ImageTransparentColor = System.Drawing.Color.Transparent
        'Me.btnGoToWF.Name = "btnGoToWF"
        'Me.btnGoToWF.Size = New System.Drawing.Size(23, 32)
        'Me.btnGoToWF.Tag = "GOTOWF"
        'Me.btnGoToWF.Text = "Tareas"
        'Me.btnGoToWF.ToolTipText = "VER DOCUMENTOS EN TAREAS"
        '
        'InfoView
        '
        InfoView.Name = "InfoView"
        InfoView.Size = New System.Drawing.Size(0, 22)
        '
        'lblEncryptFile
        '
        lblEncryptFile.DisplayStyle = ToolStripItemDisplayStyle.Text
        lblEncryptFile.Font = Zamba.AppBlock.ZambaUIHelpers.GetFontFamily()
        lblEncryptFile.ForeColor = System.Drawing.Color.Blue
        lblEncryptFile.Name = "lblEncryptFile"
        lblEncryptFile.Size = New System.Drawing.Size(97, 22)
        lblEncryptFile.Tag = "lblEnc"
        lblEncryptFile.Text = "Documento encriptado"
        lblEncryptFile.Visible = False
        '
        'btnDecryptDocument
        '
        btnDecryptDocument.DisplayStyle = ToolStripItemDisplayStyle.Text
        btnDecryptDocument.Font = Zamba.AppBlock.ZambaUIHelpers.GetFontFamily()
        btnDecryptDocument.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        btnDecryptDocument.Name = "btnDecryptDocument"
        btnDecryptDocument.Size = New System.Drawing.Size(60, 22)
        btnDecryptDocument.Tag = "decrypt"
        btnDecryptDocument.Text = "Desencriptar"
        btnDecryptDocument.ToolTipText = "Desencripta un documento, ingresando su clave de desencriptacion"
        btnDecryptDocument.Visible = False
        '
        'ToolStripContainer1
        '
        '
        'ToolStripContainer1.ContentPanel
        '
        ToolStripContainer1.ContentPanel.Size = New System.Drawing.Size(761, 384)
        ToolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        ToolStripContainer1.Location = New System.Drawing.Point(0, 0)
        ToolStripContainer1.Name = "ToolStripContainer1"
        ToolStripContainer1.Size = New System.Drawing.Size(761, 409)
        ToolStripContainer1.TabIndex = 63
        ToolStripContainer1.Text = "ToolStripContainer1"
        '
        'ToolStripContainer1.TopToolStripPanel
        '
        ToolStripContainer1.TopToolStripPanel.BackColor = System.Drawing.Color.Silver
        ToolStripContainer1.TopToolStripPanel.Controls.Add(DesignSandBar)
        ToolStripContainer1.TopToolStripPanel.Controls.Add(ToolBar1)
        '
        'btnGotoHelp
        '
        btnGotoHelp.Name = "btnGotoHelp"
        btnGotoHelp.Size = New System.Drawing.Size(23, 23)
        '
        'UCDocumentViewer2
        '
        AllowDrop = True
        BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        CausesValidation = False
        Controls.Add(ToolStripContainer1)
        Font = New Font("Tahoma", 7.0!)
        Location = New System.Drawing.Point(143, 0)
        Size = New System.Drawing.Size(761, 409)
        DesignSandBar.ResumeLayout(False)
        DesignSandBar.PerformLayout()
        ToolBar1.ResumeLayout(False)
        ToolBar1.PerformLayout()
        ToolStripContainer1.TopToolStripPanel.ResumeLayout(False)
        ToolStripContainer1.TopToolStripPanel.PerformLayout()
        ToolStripContainer1.ResumeLayout(False)
        ToolStripContainer1.PerformLayout()
        ResumeLayout(False)

    End Sub

#End Region

#Region "Atributos y eventos"
    Private Const MESSAGE_ONLY_READ As String = "Documento de solo Lectura"

    'OBJETOS PESADOS
    Public ParentTabControl As TabControl
    Public WithEvents ImgViewer As UCImgViewer = Nothing
    Public WithEvents FormBrowser As FormBrowser = Nothing
    Private WithEvents OffWB As Zamba.Browser.WebBrowser = Nothing
    Private WithEvents PdfBrowser As PdfDocumentViewerFix = Nothing
    Private WithEvents MsgWB As Panel = Nothing
    Private WithEvents WordOffWB As WordBrowser = Nothing
    Private WithEvents ExcelOffWB As ExcelBrowser = Nothing
    Private WithEvents Prev As FrmPreviewGold
    Private myOutlook As Office.Outlook.OutlookInterop = Nothing
    Private RotatedImg As Image
    Private _result As Result
    Private bt As ToolBarButton
    Private bt2 As ToolBarButton
    Private Txt As UcGoToPage
    Private Txt2 As UcGoToPage2_2
    Private WFDesigner As Zamba.WorkFlow.Designer.WorkFlowDesignerControl
    Private oFDimension As System.Drawing.Imaging.FrameDimension
    Private _notes As New ArrayList
    Private ZControl_Panels As New Hashtable
    Private ZControl_PanelLinks As Panel
    Private P As Panel
    Private _ucIndexs As IZControl
    Private rtfViewer As RichTextBox

    'DELEGADOS
    Private Delegate Sub CloseMailItemDelegate(ByRef res As Zamba.Core.Result)
    'Se encarga de cerrar el documento.
    Delegate Sub DelegateClose()

    'VARIABLES DE TIPO BASE
    Public iCount As Integer
    Public actualFrame As Integer
    Public parentName As String
    Private _replaceButtonVisibility As Boolean
    Private _file As String
    Private flagAutomaticNewVersion As Boolean
    Private ClonatedImg As Boolean
    Private _previewMode As Boolean
    Private IsNotesPushed As Boolean
    Private isDisposed As Boolean
    Private _disposeIndexViewer As Boolean
    Private ServerImagesPath As String
    Private _rotation As Int32
    Private Zoom As Decimal = 100
    Private ZoomLock As Boolean = False
    Private ZoomScale As Int32 = 8
    Private MaxZoom As Decimal = 1000
    Private MinZoom As Decimal = 10
    Private IsSignPushed As Boolean
    Private ValorPixelesTop As Int32
    Private ValorPixelesLeft As Int32
    Private IsIndexer As Boolean = False
    Private _isMaximize As Boolean = False
    Private _Estado As Int32 = 0
    Private MsgTimer As Timer
    Private oldWinStyle As Int32 = 0
    Private proc As System.Diagnostics.Process = Nothing
    Private timOut As Int32 = 0
    Private longCount As Int32 = 0
    Private itemId As Int32 = 0
    Private OutOfZamba As Boolean = False
    Private _disableInputControls As Boolean = False
    Private isAsocTask As Boolean
    Private formIdFromDoShowForm As Int64
    Private loadDocFromDoShowForm As Boolean
    Private _showToolBar As Boolean
    Private _isWF As Boolean
    Private docOpenedDate As Date = Nothing
    Private _userActionDisabledRules As New List(Of Long)
    Private _setReadOnly As Nullable(Of Boolean)

    'EVENTOS
    Public Event eAutomaticNewVersion(ByRef _result As Result, ByVal newResultPath As String)
    Public Event EventZoomLock(ByVal Estado As Boolean)
    Public Event Movido(ByVal Top As Int32, ByVal Left As Int32)
    Public Event ShowOriginal(ByRef result As Result)
    Public Event ShowAsociatedResult(ByRef res As Result)
    Public Event LinkSelected(ByVal Result As Result)
    Public Event ShowDocumentsAsociated(ByVal Id As Integer)
    Public Event MostrarForo(ByRef Result As Result)
    Public Event CambiarDock(ByVal Sender As TabPage, ByVal ClosedFromCross As Boolean, ByVal IsMaximize As Boolean)
    Public Event ShowAssociatedWFbyDocId(ByVal ID As Int64, ByVal DocTypeId As Int64)
    Public Event ReplaceDocument(ByRef result As IResult)
    Public Event CloseOpenTask(ByVal DispoSeResult As Boolean)
    Public Event RefreshAsocTask()
    Public Event ClearReferences(ByRef ucviewer As UCDocumentViewer2)

    'PUNTEROS
    Private winHandle As IntPtr = System.IntPtr.Zero
    Private oldParent As IntPtr = System.IntPtr.Zero
    Private hMenu As IntPtr = System.IntPtr.Zero

    Enum Estados
        Ninguno = 0
        OCR = 1
        Netron = 2
        Nota = 3
        Firma = 4
    End Enum
    Enum Posiciones
        Arriba
        Abajo
        Derecha
        Izquierda
    End Enum

#End Region

#Region "Constructores"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Result"></param>
    ''' <param name="FlagShowDocument"></param>
    ''' <param name="WFMode"></param>
    ''' <param name="isWF">Muestra o no el boton para ir a documental</param>
    ''' <remarks></remarks>
    Public Sub New(ByRef Container As IViewerContainer,
                   ByRef Result As Result,
                   ByVal FlagShowDocument As Boolean,
                   ByVal isWF As Boolean,
                   Optional ByVal ShowToolBar As Boolean = True,
                   Optional ByVal DisableInputControls As Boolean = False,
                   Optional ByVal useVirtual As Boolean = True,
                   Optional ByVal disposeIndexViewer As Boolean = False,
                   Optional ByVal isAttachDocument As Boolean = False)
        MyBase.New()
        InitializeComponent()
        _ucIndexs = Nothing
        ViewerContainer = Container
        Control.CheckForIllegalCrossThreadCalls = False
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Instancio el UCDocumentViewer2")
        _disableInputControls = DisableInputControls
        _result = Result
        Text = _result.Name
        _showToolBar = ShowToolBar
        _isWF = isWF
        _disposeIndexViewer = disposeIndexViewer

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Muestro el Documento ")
        If FlagShowDocument Then
            ShowDocument(useVirtual, False, False, False, True)
        End If

        Try
            ' Si no esta instalado office 2003...
            If Not Zamba.Tools.EnvironmentUtil.getOfficePlatform =
            Tools.EnvironmentUtil.OfficeVersions.Office2003 Then
                ButtonItem14.Enabled = False
            End If
        Catch ex As Exception
            ButtonItem14.Enabled = False
        End Try

        'Botones que se utilizan en Tareas para buscar el documento en la grilla de Resultado - MC
        Try
            ButtonDocumental.Visible = isWF
            'Me.btnGoToWF.Visible = Not isWF
            'Me.btnGotoWfdb.Visible = Not isWF

            'Si no es wf, o si se encuentra en tareas pero se abre un documento, se habilitan los cerrar
            If Not isWF OrElse (isWF AndAlso Not (TypeOf (_result) Is TaskResult)) OrElse isAttachDocument Then
                'Este caso se da cuando se abre un documento sin tarea dentro de los asociados de una tarea
                Buttonclose.Visible = True
                btnClose.Visible = True
                separateClose.Visible = True
            Else
                Buttonclose.Visible = False
                btnClose.Visible = False
                separateClose.Visible = False
            End If

            '[Sebastian 02-12-2009] se realizo esto para no mostrar o si la barra en la solapa documento de la opcion de ver original en 
            'la regla do show form
            ToolBar1.Visible = ShowToolBar
            LoadResultRights(Result)

            flagAutomaticNewVersion = UserBusiness.Rights.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.AutomaticVersion, _result.DocTypeId)

            Dim _newsBusiness As New NewsBusiness()
            _newsBusiness.SetRead(Membership.MembershipHelper.CurrentUser, Result.DocTypeId, Result.ID)
            _newsBusiness = Nothing
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Sobrecarga para trabajar con NEWRESULT
    ''' </summary>
    ''' <param name="newresult"></param>
    ''' <param name="flagshowdocument"></param>
    ''' <param name="wfmode"></param>
    ''' <remarks>
    ''' Se usa en el indexer6000 para mostrar la imagen una vez insertado
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	04/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub New(ByVal newresult As NewResult, ByVal flagshowdocument As Boolean)
        'Para usar un NewResult
        MyBase.New()
        InitializeComponent()
        _ucIndexs = Nothing
        Control.CheckForIllegalCrossThreadCalls = False
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Instancio el UCDocumentViewer2 con result")
        _result = newresult
        IsIndexer = True

        'Se ocultan las etiquetas del doc
        btnFlagAsFavorite.Visible = False
        btnFlagAsImportant.Visible = False
        separateDocumentLabels.Visible = False

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Mostrar doc la primera vez: " & flagshowdocument)
        If flagshowdocument Then
            ShowDocument(True, False, False, False, True)
        End If

        'Si el documento es virtual 
        Try
            If Not Zamba.Tools.EnvironmentUtil.getOfficePlatform = Tools.EnvironmentUtil.OfficeVersions.Office2003 Then
                ButtonItem14.Enabled = False
            End If
        Catch ex As Exception
            ButtonItem14.Enabled = False
        End Try

        flagAutomaticNewVersion = UserBusiness.Rights.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.AutomaticVersion, _result.DocTypeId)

        Dim _newsBusiness As New NewsBusiness()
        _newsBusiness.SetRead(Membership.MembershipHelper.CurrentUser, Result.DocTypeId, Result.ID)
        _newsBusiness = Nothing
    End Sub
#End Region

#Region "Propiedades"
    Public Property DisposeIndexViewer() As Boolean
        Get
            Return _disposeIndexViewer
        End Get
        Set(ByVal value As Boolean)
            _disposeIndexViewer = value
        End Set
    End Property
    Public Property IsMaximize() As Boolean
        Get
            Return _isMaximize
        End Get
        Set(ByVal value As Boolean)
            _isMaximize = value
            If (Not btnFullScreen Is Nothing) Then
                If value = True Then
                    btnFullScreen.Text = "Restaurar Pantalla"
                    btnFullScreen.ToolTipText = "Restaurar Pantalla"
                Else
                    btnFullScreen.Text = "Pantalla Completa"
                    btnFullScreen.ToolTipText = "Pantalla Completa"
                End If
            End If
        End Set
    End Property
    Public Property VerOriginalButtonVisible() As Boolean
        Get
            Return btnOriginalFile.Visible
        End Get
        Set(ByVal value As Boolean)
            btnOriginalFile.Visible = value
        End Set
    End Property
    Public Property PrintDocButtonVisible() As Boolean
        Get
            Return btnPrint.Visible
        End Get
        Set(ByVal value As Boolean)
            btnPrint.Visible = value
            btnPrintPreview.Visible = value
        End Set
    End Property
    Public Property PrintImgButtonVisible() As Boolean
        Get
            Return BPrint.Visible
        End Get
        Set(ByVal value As Boolean)
            BPrint.Visible = value
        End Set
    End Property
    Public Property GuardarComoButtonVisible() As Boolean
        Get
            Return btnSaveAs.Visible
        End Get
        Set(ByVal value As Boolean)
            btnSaveAs.Visible = value
        End Set
    End Property
    Public Property Estado() As Int32
        Get
            Return _Estado
        End Get
        Set(ByVal Value As Int32)
            _Estado = Value
            'Me.ImgViewer.Estado = Value
            ImgViewer.PicBox2.State = DirectCast(Value, Zamba.Shapes.NetronLight.GraphControl.States)
            Select Case Value
                Case Estados.Firma
                    ButtonItem12.Checked = False
                    ButtonItem13.Checked = True
                    ButtonItem14.Checked = False
                    NETRON16.Checked = False
                    ImgViewer.PicBox2.ContextMenu = ImgViewer.ContextMenu1
                    ImgViewer.cropX = 0
                    ImgViewer.cropY = 0
                    ImgViewer.cropWidth = 0
                    ImgViewer.cropHeight = 0
                    MenuButtonItem7.Enabled = False
                Case Estados.Netron
                    ButtonItem12.Checked = False
                    ButtonItem13.Checked = False
                    ButtonItem14.Checked = False
                    NETRON16.Checked = True
                    ImgViewer.PicBox2.ContextMenu = DirectCast(ImgViewer.MenuNetron, ContextMenu)
                    ImgViewer.cropX = 0
                    ImgViewer.cropY = 0
                    ImgViewer.cropWidth = 0
                    ImgViewer.cropHeight = 0
                    MenuButtonItem7.Enabled = True
                Case Estados.Ninguno
                    ButtonItem12.Checked = False
                    ButtonItem13.Checked = False
                    ButtonItem14.Checked = False
                    NETRON16.Checked = False
                    ImgViewer.PicBox2.ContextMenu = Nothing
                    ImgViewer.cropX = 0
                    ImgViewer.cropY = 0
                    ImgViewer.cropWidth = 0
                    ImgViewer.cropHeight = 0
                    MenuButtonItem7.Enabled = False
                Case Estados.Nota
                    ButtonItem12.Checked = True
                    ButtonItem13.Checked = False
                    ButtonItem14.Checked = False
                    NETRON16.Checked = False
                    ImgViewer.PicBox2.ContextMenu = ImgViewer.ContextMenu1
                    ImgViewer.cropX = 0
                    ImgViewer.cropY = 0
                    ImgViewer.cropWidth = 0
                    ImgViewer.cropHeight = 0
                    MenuButtonItem7.Enabled = False
                Case Estados.OCR
                    ButtonItem12.Checked = False
                    ButtonItem13.Checked = False
                    ButtonItem14.Checked = True
                    NETRON16.Checked = False
                    ImgViewer.PicBox2.ContextMenu = Nothing
                    MenuButtonItem7.Enabled = False
            End Select
        End Set
    End Property
    Private WriteOnly Property Rotation() As Int32
        Set(ByVal Value As Int32)
            Select Case Value
                Case 1
                    _rotation += 1
                    If _rotation = 4 Then _rotation = 0
                Case -1
                    _rotation += -1
                    If _rotation = -4 Then _rotation = 0
                Case 0
                    _rotation = 0
            End Select
            'es multitiff y lo volvi a su posicion original
            If _rotation = 0 Then
                If iCount > 1 Then
                    ReloadImage()
                    ClonatedImg = False
                End If
            End If
        End Set
    End Property
    Public ReadOnly Property Result() As IResult
        Get
            Return _result
        End Get
    End Property
    Private Property Notes() As ArrayList
        Get
            Return _notes
        End Get
        Set(ByVal Value As ArrayList)
            _notes = Value
        End Set
    End Property
    Public Property UserActionDisabledRules() As List(Of Long)
        Get
            Return _userActionDisabledRules
        End Get
        Set(ByVal value As List(Of Long))
            _userActionDisabledRules = value
            If Not FormBrowser Is Nothing Then
                FormBrowser.UserActionDisabledRules = UserActionDisabledRules
            End If
        End Set
    End Property
    Public Property SetReadOnly() As Nullable(Of Boolean)
        Get
            Return _setReadOnly
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            _setReadOnly = value
        End Set
    End Property
#End Region

#Region "Show"
    ''' <summary>
    ''' Muestra el documento en el WebBrowser
    ''' </summary>
    ''' <param name="useVirtual">Habilitar formularios virtuales</param>
    ''' <remarks></remarks>
    Public Sub ShowDocument(ByVal useVirtual As Boolean, ByVal _isAsocTask As Boolean, ByVal InsertingDoc As Boolean, ByVal ComeFromWF As Boolean, ByVal ShowIndexsPanel As Boolean)
        Try

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Entrando a la visualizacion del documento")
            ShowImage(useVirtual, InsertingDoc, ComeFromWF)

            'Especifica si el documento proviene de una tarea asociada.
            isAsocTask = _isAsocTask

            UserBusiness.Rights.SaveAction(Result.ID, ObjectTypes.Documents, RightsType.View, Result.Name)

            'Si es virtual el menu de atributos no se despliega            
            '---------------------------------------------------
            'osanchez - 300409
            '---------------------------------------------------
            'Mauro - Agrego validacion ya que mostraba al insertar documento
            '--------------------------------------------------- 
            If Result.ISVIRTUAL Then
                ShowIndexs(ShowIndexsPanel)
            Else 'Es un documento, se muestran, excepto si estoy insertando
                ShowIndexs(Not InsertingDoc)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Sub LoadDynamicButtons(ByVal isTask As Boolean, ByVal reload As Boolean)
        If reload Then
            'Se remueven los botones dinámicos previamente creados
            If Not ToolBar1 Is Nothing AndAlso ToolBar1.Items.Count > 0 Then
                For i As Int32 = ToolBar1.Items.Count - 1 To 0 Step -1
                    If Not ToolBar1.Items(i) Is Nothing AndAlso ToolBar1.Items(i).Tag = "DynamicButton" Then
                        ToolBar1.Items.RemoveAt(i)
                    End If
                Next
            End If
        End If
        If Not ToolBar1 Is Nothing AndAlso ToolBar1.Items.Count > 0 Then

            'obtengo la ultima posicion ocupada por la toolbar
            Dim LastIndexPosition As Int32 = ToolBar1.Items.IndexOf((ToolBar1.Items(ToolBar1.Items.Count - 1)))

            'Se cargan los botones dinámicos
            If isTask Then
                GenericRuleManager.LoadDynamicButtons(ToolBar1, LastIndexPosition, True, ButtonPlace.DocumentToolbar_tasks, Result.ID, ButtonType.Rule, DirectCast(Result, TaskResult).WorkId)
            Else
                GenericRuleManager.LoadDynamicButtons(ToolBar1, LastIndexPosition, True, ButtonPlace.DocumentToolbar_Results, Result.ID)
            End If
        End If

    End Sub


    ''' <summary>
    ''' Método que le pasa al result actual el formulario que se tendra que mostrar (de la regla) adentro de la solapa 
    ''' </summary>
    ''' <param name="formIDOfTheRule"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	22/08/2008	Created    
    '''     [Gaston]	06/11/2008	Modified    Si la regla DoShowForm muestra forms. asociados entonces que se puedan ver adentro de la solapa
    ''' </history>
    Private Sub ShowDocumentSinceRuleDOShowForm(ByVal formIDOfTheRule As Int64)

        Result.CurrentFormID = formIDOfTheRule

        UserBusiness.Rights.SaveAction(Result.ID, ObjectTypes.Documents, RightsType.View, Result.Name)

        'Si es virtual el menu de atributos no se despliega            
        '---------------------------------------------------
        'osanchez - 300409
        '---------------------------------------------------
        If Result.ISVIRTUAL Then
            ShowIndexs(True)
        Else
            ShowIndexs(True)
        End If

        If (VariablesInterReglas.ContainsKey("AsociatedDocIdForPreview") = False) Then
            ShowFormOfDOShowForm()
        Else
            ShowAssociatedFormOfDOShowForm()
        End If

    End Sub

    ''' <summary>
    ''' Método que le pasa al result actual el formulario que se tendra que mostrar (de la regla) adentro de la solapa 
    ''' </summary>
    ''' <param name="formId">Id del formulario a cargar</param>
    ''' <history>
    '''     [Tomas] 10/02/2010  Created     Se crea para hacer que cuando se realice un refresh sobre 
    '''                                     un formulario mostrado por una doShowForm se ejecuten los 
    '''                                     métodos adecuados y no los genéricos que hacen cargar el 
    '''                                     formulario original.
    ''' </history>
    Public Sub ShowDocumentFromDoShowForm(ByVal TaskResult As TaskResult)
        _result = TaskResult
        loadDocFromDoShowForm = True
        formIdFromDoShowForm = TaskResult.CurrentFormID
        ShowDocumentSinceRuleDOShowForm(formIdFromDoShowForm)
    End Sub

    ''' <summary>
    ''' Método que muestra el formulario especificado en la regla DOShowForm (mostrar datos de documento asociado desactivado)
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	22/08/2008	Created    
    ''' </history>
    Private Sub ShowFormOfDOShowForm()

        Try

            If (Result.CurrentFormID > 0) Then

                Dim Form As ZwebForm = FormBusiness.GetForm(Result.CurrentFormID)
                If Form Is Nothing Then
                    Throw New Exception(String.Format("El Form con Id: {0} no se ha podido obtener", Result.CurrentFormID))
                End If
                LoadFormBrowser()
                DesignSandBar.Visible = False
                ToolBar1.Visible = True
                btnRefresh.Visible = True

                If (Form.Type <> FormTypes.Edit) Then
                    Form.Type = FormTypes.Show
                End If

                EnableToolBarButtons()
                FormBrowser.ShowDocument(Result, Form)
                showOriginalButton(Result)

                Exit Sub
            Else
                EnableToolBarButtons()
            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Exit Sub
        End Try

    End Sub
    Private Sub EnableToolBarButtons()
        If Result.CurrentFormID > 0 Then
            ToolBar1.Items.AddRange(New ToolStripItem() {btnSaveAs, ToolStripSeparator7, btnAdjuntarEmail, ToolStripSeparator8, btnAgregarDocumentoCarpeta, ToolStripSeparator13, btnVerVersionesDelDocumento, btnAgregarUnaNuevaVersionDelDocuemto, ToolStripSeparator12, btnGotoWorkflow, btnChangePosition})
        Else
            ToolBar1.Items.AddRange(New ToolStripItem() {btnSaveAs, ToolStripSeparator7, btnAdjuntarEmail, ToolStripSeparator8, btnAgregarDocumentoCarpeta, ToolStripSeparator13, btnVerVersionesDelDocumento, btnAgregarUnaNuevaVersionDelDocuemto, ToolStripSeparator12, btnGotoWorkflow, btnChangePosition})
        End If
    End Sub

    ''' <summary>
    ''' Método que muestra el formulario especificado en la regla DOShowForm (mostrar datos de documento asociado activado)
    ''' </summary>
    ''' <param name="varDocId">variable para buscar el doc Id</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	06/11/2008	Created    
    ''' </history>
    Private Sub ShowAssociatedFormOfDOShowForm()

        Dim ResultsAsociated As ArrayList
        Dim AsociatedResult As Result = Nothing

        ' Se obtienen los documentos asociados
        ResultsAsociated = DocAsociatedBusiness.getAsociatedResultsFromResult(Result)

        ' Se recorren los documentos asociados para buscar el que coincide con el form configurado en la regla
        For Each asociatedR As Result In ResultsAsociated

            ' Si la variable doc_id no está vacía
            If (VariablesInterReglas.ContainsKey("AsociatedDocIdForPreview") = True) Then

                ' Se busca el valor que contiene la variable doc_id (id del new result que se creo en el play de la regla DOCreateForm) 
                Dim id_VarDocId As Long = CType(VariablesInterReglas.Item("AsociatedDocIdForPreview"), Long)

                If (asociatedR.ID = id_VarDocId) Then
                    ' Se obtiene el formID
                    asociatedR.CurrentFormID = FormBusiness.getDTFormId(CType(asociatedR.DocTypeId, Integer))
                    AsociatedResult = asociatedR
                    Exit For
                End If

            End If

        Next

        Try

            Dim Forms As List(Of ZwebForm) = FormBusiness.GetAllForms(AsociatedResult.DocTypeId, True)

            If Not ((IsNothing(Forms)) AndAlso (Forms.Count > 0)) Then

                If Not (IsNothing(AsociatedResult)) Then

                    For Each F As ZwebForm In Forms

                        If (F.ID = AsociatedResult.CurrentFormID) Then
                            LoadFormBrowser()
                            FormBrowser.ShowDocument(AsociatedResult, F)
                            Exit For
                        End If

                    Next

                End If

            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Public Function ValidateForm() As Boolean
        If FormBrowser Is Nothing Then
            Return False
        Else
            Return FormBrowser.validateForm
        End If
    End Function

    Private _documentEncryptionState As ZEncpritonStates = ZEncpritonStates.None
    Private _useVirtual As Boolean
    Private _insertedDoc As Boolean
    Private _comeToWF As Boolean

    ''' <summary>
    ''' Muestra la imagen del documento
    ''' </summary>
    ''' <param name="useVirtual">Si se desea habilitar o no los formularios virtuales</param>
    ''' <param name="InsertingDoc">Determina cuando el formulario debe visualizarse desde insercion</param>
    ''' <param name="ComeFromWF">Determina cuando el formulario debe visualizarse desde tareas</param>
    ''' <history>
    ''' Marcelo modified 05/02/2009
    ''' [Ezequiel] 29/03/2009 Modified - Owner Rights
    '''     [Gaston]	20/04/2009	Modified    Funcionalidad para formularios dinámicos
    '''     [Tomas]     11/05/2009  Modified    Si el formulario es virtual el botón 'Ver Original' no es mostrado.
    '''     [Javier]    01/10/2010  Modified    Se comenta código que genera formulario dinamico ya que esto generaba error en tipos
    '''                                         de documentos que no tienen formulario asociado, el error estaba en el if de IsNullOrEmpty(Result.AutoName)
    '''     [Pablo]    17/10/2010  Modified     Se agregan dos parametros mas para determinar desde donde se tiene que mostrar el formulario
    '''                                         que se debe visualizar, si es desde insercion, desde busqueda o desde tareas.
    '''     [Tomas]     06/07/2011  Modified    Se corrige la visibilidad del botón Imprimir.
    '''</history>
    ''' <remarks></remarks>
    Private Sub ShowImage(Optional ByVal useVirtual As Boolean = True, Optional ByVal InsertingDoc As Boolean = False, Optional ByVal ComeFromWF As Boolean = False)

        If IsNothing(Result) Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "No hay result para la tarea" & Now.ToString)
            Exit Sub
        End If

        Try
            CheckEncryptedFile()
            _useVirtual = False
            _insertedDoc = False
            _comeToWF = False
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Try
            If Not String.IsNullOrEmpty(Result.FullPath) Then
                If Result.FullPath.IndexOf("aspx", StringComparison.CurrentCultureIgnoreCase) <> -1 Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Muestro el Documento aspx " & Now.ToString)
                    ShowOfficeDocument(Result, _setReadOnly)
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        'Si es office muestro la barra de herramientas
        Try
            'If Not btnShow Is Nothing Then
            '    If Result.IsOffice Then
            '        Me.btnShow.Visible = True
            '    Else
            '        Me.btnShow.Visible = False
            '    End If
            'End If
            If Not btnSaveAs Is Nothing Then
                'Si es virtual no se guarda
                If Result.ISVIRTUAL Then
                    btnSaveAs.Visible = False
                Else
                    btnSaveAs.Visible = True
                End If
            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            ShowLabelError(ex.Message)
            Exit Sub
        End Try

        'Busco WebForms
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Busco los Forms ")

            If useVirtual Then
                If Result.CurrentFormID > 0 Then
                    Dim Form As ZwebForm = FormBusiness.GetForm(Result.CurrentFormID)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Cargo el form browser ")
                    LoadFormBrowser()
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Levanto el form browser ")
                    DesignSandBar.Visible = False
                    ToolBar1.Visible = True

                    If TypeOf (Result) Is NewResult Then
                        btnRefresh.Visible = False
                        btnAdjuntarEmail.Visible = False
                        btnAgregarDocumentoCarpeta.Visible = False
                        btnAgregarUnaNuevaVersionDelDocuemto.Visible = False
                        'Me.btnGoToWF.Visible = False
                        'Me.btnGotoWfdb.Visible = False
                        btnReplace.Visible = False
                        BtnReplaceDocument.Visible = False
                        btnVerVersionesDelDocumento.Visible = False
                        btnPrint.Visible = False
                        btnPrintPreview.Visible = False
                        'Me.ToolStripSeparator9.Visible = False
                        ToolStripSeparator8.Visible = False
                        ToolStripSeparator7.Visible = False
                        ToolStripSeparator13.Visible = False
                        ToolStripSeparator12.Visible = False
                        btnChangePosition.Visible = False
                        btnFlagAsFavorite.Visible = False
                        btnFlagAsImportant.Visible = False
                        separateDocumentLabels.Visible = False
                    Else
                        btnRefresh.Visible = True
                    End If

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Muestro el Formulario ")
                    EnableToolBarButtons()
                    FormBrowser.ShowDocument(Result, Form)

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Cargue el Formulario ")
                    showOriginalButton(Result)

                    Exit Sub
                Else
                    '[Sebastian 11-05-09] se salvo el warning en Result.DocTypeId de long to int
                    Dim Forms As List(Of ZwebForm)

                    '[Pablo 12/10/2010] determina si vengo de insercion o de busqueda
                    If InsertingDoc = True Then
                        Forms = FormBusiness.GetInsertFormsByDocTypeId(Int32.Parse(Result.DocTypeId.ToString))
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Cargo el form browser ")
                        LoadFormBrowser()
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Muestro el Formulario ")
                        If Not IsNothing(Forms) AndAlso Forms.Count > 0 AndAlso FormBrowser.ShowInsertForm(Result, Forms) = True Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Levanto el form browser ")
                            DesignSandBar.Visible = False
                            ToolBar1.Visible = True
                            btnRefresh.Visible = True
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Cargue el Formulario ")
                            If Not IsNothing(Forms) Then
                                If Forms.Count > 0 Then
                                    Result.CurrentFormID = Forms(0).ID
                                End If
                            End If
                            EnableToolBarButtons()
                            showOriginalButton(Result)
                            Exit Sub
                        End If
                    Else
                        If RightsBusiness.GetUserRights(ObjectTypes.DocTypes, RightsType.Edit, Result.DocTypeId) Then
                            Forms = FormBusiness.GetForms(Result.DocTypeId, FormTypes.Edit, True)
                            If (Forms Is Nothing OrElse Forms.Count = 0) Then
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "No hay formularios de edicion.")
                                Forms = FormBusiness.GetForms(Result.DocTypeId, FormTypes.Show, True)
                            End If
                        Else
                            Forms = FormBusiness.GetForms(Result.DocTypeId, FormTypes.Show, True)
                            If (Forms Is Nothing OrElse Forms.Count = 0) Then
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "No hay formularios de visualizacion.")
                            End If
                        End If

                        If Not IsNothing(Forms) AndAlso Forms.Count > 0 Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Cargo el form browser ")
                            LoadFormBrowser()
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Levanto el form browser ")
                            DesignSandBar.Visible = False
                            ToolBar1.Visible = True
                            btnRefresh.Visible = True
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Muestro el Formulario ")
                            '[Pablo 12/10/2010] determina si vengo de busqueda o de tareas
                            FormBrowser.ShowDocument(Result, Forms, ComeFromWF)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Cargue el Formulario ")
                            If Not IsNothing(Forms) Then
                                If Forms.Count > 0 Then
                                    Result.CurrentFormID = Forms(0).ID
                                End If
                            End If
                            EnableToolBarButtons()
                            showOriginalButton(Result)
                            Exit Sub
                        Else
                            If Result.ISVIRTUAL Then
                                NotifyIfIsBarcode()
                                Exit Sub
                            End If
                        End If
                    End If
                End If

            End If

            'Me.btnRefresh.Visible = False
        Catch ex As Exception
            MsgBox("No se pudo encontrar el formulario correspondiente", MsgBoxStyle.Exclamation, "Zamba Software")
            Zamba.Core.ZClass.raiseerror(ex)
            btnRefresh.Visible = False
        End Try

        NotifyIfIsBarcode()

        If Result.IsImage Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "El documento es imagen")
            Try


                ZTrace.WriteLineIf(ZTrace.IsInfo, "Muestro la Imagen " & Now.ToString)
                ShowGrafics(InsertingDoc)


            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        ElseIf Result.IsXoml Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "El documento es Xoml")
            ToolBar1.Visible = True
            DesignSandBar.Visible = False
            ShowWorkFlowDesigner(Result)
        Else
            Try
                If Not ToolBar1 Is Nothing Then
                    If Not Result.ISVIRTUAL Then 'AndAlso Result.IsOffice2 Then
                        ToolBar1.Visible = True
                        DesignSandBar.Visible = False
                        ToolBar1.SendToBack()
                        EnableToolBarButtons()
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "El documento virtual")
                        ToolBar1.Visible = True
                        DesignSandBar.Visible = False
                    End If
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Muestro el Documento de Office " & Now.ToString)
                End If

                If Result.IsMsg Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "El documento es outlook")
                    ShowMsgDocument(Result)
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "El documento es office")
                    ShowOfficeDocument(Result, _setReadOnly, False)
                End If
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End If

        '-------------------------------------------------------------------------------------
        'nota:  Por alguna razón al depurar, los controles de la barra mantienen el visible
        '       en false, pero al ejecutar el código se asigna correctamente.
        '-------------------------------------------------------------------------------------
        'Visibilidad del botón imprimir para los diferentes casos que se puedan dar.
        'Por permisos o configuración
        Dim print As Boolean = True
        If RightsBusiness.GetUserRights(ObjectTypes.Documents, RightsType.Print) Then
            If Result.IsPDF OrElse Result.IsPowerpoint Then
                print = False
            End If
        Else
            print = False
        End If

        If Not btnPrint Is Nothing Then btnPrint.Visible = print
        If Not btnPrintPreview Is Nothing Then btnPrintPreview.Visible = print
        If Not BPrint Is Nothing Then BPrint.Visible = print
        '-------------------------------------------------------------------------------------
    End Sub

    Private Sub NotifyIfIsBarcode()
        Dim file = DirectCast(Result, IResult).FileName
        Dim re As Result = New Result()
        'Si es una caratula no digitalizada
        If file Is Nothing Then 'Result.CurrentFormID = -1 AndAlso (Result.FullPath Is Nothing OrElse re.HasExtension(file))
            Dim bardoceId As String = BarcodesBusiness.GetBarCodeByDocTypeIdAndDocId(Result.ID, Result.DocTypeId)
            If Not String.IsNullOrEmpty(bardoceId) AndAlso bardoceId > 0 Then
                MessageBox.Show("Esta abriendo la caratula con ID número " & bardoceId & ", la cual aun no ha sido digitalizada.")
            End If
        End If
    End Sub



    ''' <summary>
    ''' Show original button if the result has fullpath and the doctype a form asociated
    ''' </summary>
    ''' <history>Marcelo Created 26/11/09</history>
    ''' <param name="_result"></param>
    ''' <remarks></remarks>
    Private Sub showOriginalButton(ByVal _result As Result)
        If String.IsNullOrEmpty(_result.FullPath) = False Then
            btnOriginalFile.Visible = True
        Else
            btnOriginalFile.Visible = False
        End If

        If btnOriginalFile.Visible Then
            btnOpenDocument.Visible = True
        Else
            btnOpenDocument.Visible = False
        End If
    End Sub

    Private Sub ShowIndexs(ByVal visible As Boolean)
        Try
            If visible OrElse UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.EditAttributesInForms, Result.DocTypeId) Then
                If _ucIndexs Is Nothing Then
                    _ucIndexs = New UCIndexViewer()
                End If
                ZControl_Show(_ucIndexs, UCDocumentViewer2.Posiciones.Izquierda, "Atributos", False)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub RefreshIndexs(ByVal Result As IResult)
        If Not IsNothing(_ucIndexs) Then
            DirectCast(_ucIndexs, UCIndexViewer).ShowIndexs(Result.ID, Result.DocTypeId, False)
        End If
    End Sub

#Region "Formularios Web"

    ''' <summary>
    ''' Carga el formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadFormBrowser()
        If isDisposed = False Then
            If Not _ucIndexs Is Nothing Then
                FormBrowser = New FormBrowser(_ucIndexs)
            Else
                FormBrowser = New FormBrowser
            End If

            '[AlejandroR] 28/12/09 - Created
            FormBrowser.DisableInputControls = _disableInputControls

            RemoveHandler FormBrowser.ResultModified, AddressOf RefreshIndexs
            AddHandler FormBrowser.ResultModified, AddressOf RefreshIndexs
            RemoveHandler FormBrowser.ShowAsociatedResult, AddressOf ThrowShowAsociatedResult
            AddHandler FormBrowser.ShowAsociatedResult, AddressOf ThrowShowAsociatedResult

            'Eventos de la barra amarilla de actualizacion.
            RemoveHandler FormBrowser.showYellowPanel, AddressOf showYellowPanel
            AddHandler FormBrowser.showYellowPanel, AddressOf showYellowPanel

            '[Alejandro] 20/11/09 - Created
            RemoveHandler FormBrowser.SaveDocumentVirtualForm, AddressOf SaveDocumentVirtualForm
            AddHandler FormBrowser.SaveDocumentVirtualForm, AddressOf SaveDocumentVirtualForm

            '[Tomas]    10/02/2010  Created
            RemoveHandler FormBrowser.RefreshAfterF5, AddressOf updateDocsAsociated
            AddHandler FormBrowser.RefreshAfterF5, AddressOf updateDocsAsociated

            RemoveHandler FormBrowser.FormCloseTab, AddressOf CloseDocument
            AddHandler FormBrowser.FormCloseTab, AddressOf CloseDocument


            '[AlejandroR] 28/04/2011 - Created
            RemoveHandler FormBrowser.RefreshTask, AddressOf fbRefreshTask
            AddHandler FormBrowser.RefreshTask, AddressOf fbRefreshTask

            '[Marcelo] 06/11/2012 - Created
            RemoveHandler FormBrowser.ShowOriginal, AddressOf fbShowOriginal
            AddHandler FormBrowser.ShowOriginal, AddressOf fbShowOriginal

            '[AlejandroR] 28/04/2011 - Created
            RemoveHandler FormBrowser.ReloadAsociatedResult, AddressOf fbReloadAsociatedResult
            AddHandler FormBrowser.ReloadAsociatedResult, AddressOf fbReloadAsociatedResult

            FormBrowser.Dock = DockStyle.Fill

            If Not IsNothing(ToolStripContainer1) AndAlso Not IsNothing(ToolStripContainer1.ContentPanel) Then
                ToolStripContainer1.ContentPanel.Controls.Add(FormBrowser)
            End If

            FormBrowser.BringToFront()
        End If
    End Sub

    '[AlejandroR] 20/11/09 - Created
    Public Event SaveDocumentWithVirtualForms()

    '[AlejandroR] 20/11/09 - Created
    Private Sub SaveDocumentVirtualForm()
        RaiseEvent SaveDocumentWithVirtualForms()
    End Sub

    '[AlejandroR] 28/04/2011 - Created
    Private Sub fbRefreshTask(ByVal Task As ITaskResult)
        RaiseEvent RefreshTask(Task)
    End Sub

    ''' <summary>
    ''' Abre el documento original
    ''' </summary>
    ''' <param name="result"></param>
    ''' <history>[Marcelo] 06/11/2012 - Created</history>
    ''' <remarks></remarks>
    Private Sub fbShowOriginal(ByVal result As IResult)
        RaiseEvent ShowOriginal(result)
    End Sub

    '[AlejandroR] 28/04/2011 - Created
    Private Sub fbReloadAsociatedResult(ByVal AsociatedResult As Core.Result)
        RaiseEvent ReloadAsociatedResult(Result)
    End Sub

    Public Function GetHtml() As String
        Try
            If IsNothing(FormBrowser) = False Then
                Return FormBrowser.GetHtml()
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return String.Empty
        End Try
        Return String.Empty
    End Function

    Public Function SaveHtmlFile() As Boolean

        Result.Html = GetHtml()
        Result.HtmlFile = Tools.EnvironmentUtil.GetTempDir("\OfficeTemp").FullName & "\" & Zamba.Core.FileBusiness.RemoveInvalidFileChars(Result.Name) & "-temp" & (New Random).Next(1, 9999).ToString & ".html"

        Try
            If File.Exists(Result.HtmlFile) Then
                File.Delete(Result.HtmlFile)
            End If
        Catch
        End Try
        Dim form As ZwebForm = FormBusiness.GetShowAndEditForms(Int32.Parse(Result.DocTypeId.ToString))(0)
        If File.Exists(form.Path.Replace(".html", ".mht")) Then
            Try
                Using write As New StreamWriter(Result.HtmlFile.Substring(0, Result.HtmlFile.Length - 4) & "mht")
                    write.AutoFlush = True
                    Dim reader As New StreamReader(form.Path.Replace(".html", ".mht"))
                    Dim mhtstring As String = reader.ReadToEnd()
                    write.Write(mhtstring.Replace("<Zamba.Html>", Result.Html))
                End Using
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Try
                File.Delete(Result.HtmlFile)
            Catch ex As Exception

            End Try

            Result.HtmlFile = Result.HtmlFile.Substring(0, Result.HtmlFile.Length - 4) & "mht"
        Else

            Try
                Using write As New StreamWriter(Result.HtmlFile)
                    write.AutoFlush = True
                    write.Write(Result.Html)
                End Using
            Catch ex As Exception
                Dim ex2 As New Exception(ex.Message & " - Ruta del archivo: " & Result.HtmlFile, ex)
                ZClass.raiseerror(ex2)
            End Try

        End If
    End Function

    Private Sub ThrowShowAsociatedResult(ByVal res As Result)
        RaiseEvent ShowAsociatedResult(res)
    End Sub

    ''' <summary>
    ''' Método que muestra una etiqueta amarilla cuando el usuario presiona el botón Ver de algún documento asociado ubicado en el formulario virtual 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Gaston]    11/12/2008  Created 
    ''' </history>
    Private Sub showYellowPanel()

        ' Si no se encuentra en la colección de controles de UCDocumentViewer2 el panel
        If (IsNothing(ToolStripContainer1.ContentPanel.Controls("pnlPanel"))) Then

            Dim lblLabel As New Label
            lblLabel.AutoSize = True
            lblLabel.Location = New Point(5, 10)
            lblLabel.Name = "lblMessage"
            lblLabel.Size = New Size(195, 13)
            lblLabel.TabIndex = 0
            lblLabel.Text = "El Documento ha sido actualizado. Presione actualizar para volver a cargar"
            lblLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                              Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            lblLabel.Font = New Font("Microsoft Sans Serif", 11.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))

            Dim btnButton As New Button
            ' Se asocia al evento click del botón "Actualizar" el método updateDocsAsociated. Cuando el usuario presiona dicho botón se ejecutará
            ' el correspondiente método
            AddHandler btnButton.Click, AddressOf updateDocsAsociated
            btnButton.Location = New Point(730, 5)
            btnButton.Name = "btnUpdate"
            btnButton.Size = New Size(75, 23)
            btnButton.TabIndex = 1
            btnButton.Text = "Actualizar"
            btnButton.UseVisualStyleBackColor = True

            Dim btnClose As New Button
            ' Se asocia al evento click del botón "Actualizar" el método updateDocsAsociated. Cuando el usuario presiona dicho botón se ejecutará
            ' el correspondiente método
            AddHandler btnClose.Click, AddressOf hideYellowPanel
            btnClose.Location = New Point(830, 5)
            btnClose.Name = "btnClose"
            btnClose.Size = New Size(75, 23)
            btnClose.TabIndex = 1
            btnClose.Text = "Cerrar"
            btnClose.UseVisualStyleBackColor = True

            Dim pnlPanel As New Panel
            pnlPanel.BackColor = Color.Beige
            ' La etiqueta y el botón se agregan al panel
            pnlPanel.Controls.Add(btnButton)
            pnlPanel.Controls.Add(btnClose)
            pnlPanel.Controls.Add(lblLabel)
            pnlPanel.Dock = DockStyle.Top
            pnlPanel.ForeColor = Color.FromArgb(76, 76, 76)
            pnlPanel.Location = New Point(3, 3)
            pnlPanel.Name = "pnlPanel"
            pnlPanel.Size = New Size(305, 33)
            pnlPanel.TabIndex = 0

            ' El panel se agrega a la colección de controles de UCDocumentViewer2
            ToolStripContainer1.ContentPanel.Controls.Add(pnlPanel)

        End If

    End Sub

    Private Sub hideYellowPanel()
        If ToolStripContainer1 IsNot Nothing AndAlso ToolStripContainer1.ContentPanel IsNot Nothing AndAlso
        ToolStripContainer1.ContentPanel.Controls.Count > 0 AndAlso ToolStripContainer1.ContentPanel.Controls.Find("pnlPanel", True).Length > 0 Then

            ToolStripContainer1.ContentPanel.Controls.Remove(ToolStripContainer1.ContentPanel.Controls.Find("pnlPanel", True)(0))

        End If
    End Sub

    ''' <summary>
    ''' Método que refresca el tab. Se borran sus controles y se vuelven a crear, incluyendo al FormBrowser donde se vuelven a cargar los 
    ''' documentos asociados
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Gaston]    11/12/2008  Created
    '''     [Tomas]     10/02/2010  Modified    Se diferencia el refresh de los fomrularios comunes
    '''                                         y los mostrados por la doShowForm.
    ''' </history>
    Public Sub updateDocsAsociated()
        SuspendLayout()
        Dim WFTB As New WFTaskBusiness
        Try
            '(pablo) 27022011
            If Result.GetType.FullName.ToString = "Zamba.Core.Result" Then
                RaiseEvent ReloadAsociatedResult(Result)
                RefreshData(Nothing)
            Else
                Dim task As TaskResult = WFtb.GetTaskByTaskIdAndDocTypeId(DirectCast(Result, TaskResult).TaskId, Result.DocTypeId, 0)
                _result = task
                RaiseEvent RefreshTask(task)
                RefreshData(task)
            End If

            hideYellowPanel()
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
        Finally
            WFTB = Nothing
            ResumeLayout(False)
        End Try

    End Sub

    Public Sub EnableForm(ByVal Enable As Boolean)
        Try
            If Not IsNothing(FormBrowser) Then
                FormBrowser.Enabled = Enable
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub

    'Private Function HasExtension(ByVal forms() As ZwebForm) As Boolean
    '    If Result.ISVIRTUAL = False Then
    '        Try
    '            Dim file As New FileInfo(Result.FullPath)
    '            Dim iExtensions As Int32
    '            If Not IsNothing(file) Then
    '                For Each f As ZwebForm In forms
    '                    iExtensions = f.Extensions.Count - 1
    '                    If iExtensions > -1 Then
    '                        For i As Int32 = 0 To iExtensions
    '                            Try
    '                                '[sebastian ]09-06-2009 se agrego cast para salvar warning
    '                                Dim etype As WebFormsExtensions = DirectCast(f.Extensions(i), WebFormsExtensions)
    '                                If String.Compare("." & etype.ToString, file.Extension.ToLower) = 0 Then
    '                                    Return True
    '                                End If
    '                            Catch ex As Exception
    '                                Zamba.Core.ZClass.raiseerror(ex)
    '                            End Try
    '                        Next
    '                    End If
    '                Next
    '            End If
    '            Return False
    '        Catch ex As Exception
    '            Zamba.Core.ZClass.raiseerror(ex)
    '            Return False
    '        End Try
    '    Else
    '        Return True
    '    End If
    'End Function


    Private Sub FormBrowser_LinkSelected(ByVal Result As Result) Handles FormBrowser.LinkSelected
        RaiseEvent LinkSelected(Result)
    End Sub
#End Region

#Region "PictureBox"

    Private Sub LoadPicBox()
        ImgViewer = New UCImgViewer

        'RollBackReplace(Result)

        ImgViewer.Dock = DockStyle.Fill
        ToolStripContainer1.ContentPanel.Controls.Add(ImgViewer)
        '//  ImgViewer.SearchInDb(Result)

        'RemoveHandler ImgViewer.PicBox.DoubleClick, AddressOf PictureBox1_DoubleClick
        'AddHandler ImgViewer.PicBox.DoubleClick, AddressOf PictureBox1_DoubleClick
        btnnotasHandlers()
        ImgViewer.BringToFront()

        'Si el documento esta desencriptado o por desencritpar habilito los botones de la barra, sino los deshabilito
        If _documentEncryptionState = ZEncpritonStates.Decrypted OrElse _documentEncryptionState = ZEncpritonStates.ToDecrypt Then
            For Each item As ToolStripItem In DesignSandBar.Items
                If String.Compare(item.Name, "lblEncryptFile") <> 0 AndAlso String.Compare(item.Name, "btnDecryptDocument") <> 0 AndAlso String.Compare(item.Name, "Buttonclose") <> 0 Then
                    item.Enabled = True
                End If
            Next
        Else
            For Each item As ToolStripItem In DesignSandBar.Items
                If String.Compare(item.Name, "lblEncryptFile") <> 0 AndAlso String.Compare(item.Name, "btnDecryptDocument") <> 0 AndAlso String.Compare(item.Name, "Buttonclose") <> 0 Then
                    item.Enabled = False
                End If
            Next
        End If
    End Sub
    Private Sub ShowGrafics(IsInserting As Boolean)
        Try
            If IsInserting Then
                _file = _result.File
            End If

            'Si se pudo crear el temporal
            If IsInserting OrElse (CreateTempFileToShow(_result) AndAlso Not String.IsNullOrEmpty(_file)) Then

                If Path.GetExtension(_file).ToLower().Contains("pdf") Then
                    _result.Doc_File = _result.Doc_File.Replace(Path.GetExtension(_result.RealFullPath), ".pdf")
                    If _result.OriginalName IsNot Nothing Then
                        _result.OriginalName = _result.OriginalName.Replace(Path.GetExtension(_result.FullPath), ".pdf")
                    End If
                    ToolBar1.Visible = True
                        DesignSandBar.Visible = False
                        ToolBar1.SendToBack()
                        EnableToolBarButtons()
                        ShowOfficeDocument(_result, True, False)
                        Exit Sub
                    End If
                    DesignSandBar.Visible = True
                ToolBar1.Visible = False

                DesignSandBar.Items.Add(lblEncryptFile)
                DesignSandBar.Items.Add(btnDecryptDocument)
                'Ruta de servidor de imagenes
                ServerImagesPath = IconsBusiness.GetServerImagesPath()
                ShowNotes()
                EnableToolBarButtons()
                LoadPicBox()

                'Instancio el picture en base al temporal
                _result.Picture = New ZPicture(_file)

                'Permiso de notas
                Try
                    DropDownMenuItem1.Enabled = UserBusiness.Rights.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.Notas, RightsType.Create)
                Catch ex As Exception
                    DropDownMenuItem1.Enabled = False
                    Zamba.Core.ZClass.raiseerror(ex)
                End Try

                Try
                    If UserBusiness.Rights.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.Edit, Result.DocTypeId) Then
                        BtnReplaceDocument.Visible = True
                    Else
                        BtnReplaceDocument.Visible = False
                    End If
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                End Try

                'Mutitiff
                Try
                    iCount = 0
                    actualFrame = 0
                    oFDimension = New System.Drawing.Imaging.FrameDimension(Result.Picture.Image.FrameDimensionsList(actualFrame))
                    iCount = Result.Picture.Image.GetFrameCount(oFDimension) - 1
                    If iCount > 0 Then
                        Try
                            txtNumPag.Items.Clear()
                            For A As Int32 = 1 To iCount + 1
                                txtNumPag.Items.Add(A)
                            Next
                            lblPagin.Text = "1 DE " & iCount + 1
                            lblPagin.Visible = True

                            ButtonItem16.Visible = True
                            ButtonItem17.Visible = True
                            ButtonItem18.Visible = True
                            ButtonItem19.Visible = True

                            txtNumPag.Visible = True
                            ButtonItem20.Visible = True
                            lblPagin.Text = "1 DE " & iCount + 1
                            lblPagin.Visible = True
                        Catch ex As Exception
                        End Try
                        Result.Picture.Image.SelectActiveFrame(oFDimension, actualFrame)
                        LoadGoToPage()
                    Else
                        Try
                            HidePageButtons()
                        Catch ex As Exception
                            Zamba.Core.ZClass.raiseerror(ex)
                        End Try
                        oFDimension = Nothing
                    End If
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                End Try

                'Ajuste de imagen inicial
                Dim size As New Drawing.Size
                size = Result.Picture.Size
                '[sebastian ]09-06-2009 se agrego cast para salvar el warning
                Dim res As Long = CLng(Result.Picture.Resolution)

                Try
                    Result.Picture.Resolution = res
                    If Result.IsTif Then
                        ImgViewer.PicBox2.isTif = True
                    End If

                    ImgViewer.PicBox2.Image = Result.Picture.Image
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                End Try

                Try
                    _rotation = 0
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                End Try

                Select Case Int32.Parse(UserPreferences.getValue("InitialSize", UPSections.UserPreferences, 1))
                    Case UserPreferences.InitialSizes.Height
                        If Result.IsImage Then AltoPantalla()
                    Case UserPreferences.InitialSizes.Width
                        If Result.IsImage Then AnchoPantalla()
                End Select

            Else

                MessageBox.Show("El documento no esta accesible, consulte al administrador del sistema.", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                ZTrace.WriteLineIf(ZTrace.IsError, "El documento no se encuentra, verificar que exista en el volumen.")
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            HidePageButtons()
        End Try
    End Sub

    Private Sub HidePageButtons()
        '  DropDownMenuItem1.Text = "NOTAS"
        ButtonItem16.Visible = False
        ButtonItem17.Visible = False
        ButtonItem18.Visible = False
        ButtonItem19.Visible = False
        'Me.ImgViewer.Panel2.Width = 577
        txtNumPag.Visible = False
        ButtonItem20.Visible = False
        lblPagin.Text = "0 DE 0"
        lblPagin.Visible = False
    End Sub

    Private Sub LoadGoToPage()
        Try
            bt = New ToolBarButton
            bt2 = New ToolBarButton
            Txt = New UcGoToPage
            Txt2 = New UcGoToPage2_2
            'TODO PATOTOOLBAR
            'AddHandler ImgViewer.ToolBar1.Resize, AddressOf AlignTxt
            'AddHandler Txt2.BtnGoPage.Click, AddressOf Me.GotoPage
            'Me.ImgViewer.ToolBar1.Controls.Add(Txt)
            'Me.ImgViewer.ToolBar1.Controls.Add(Txt2)
            'Me.ImgViewer.ToolBar1.Buttons.Add(bt)
            'Me.ImgViewer.ToolBar1.Buttons.Add(bt2)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub AlignTxt(ByVal sender As Object, ByVal e As EventArgs)
        Txt.SetBounds(bt.Rectangle.X, bt.Rectangle.Y, bt.Rectangle.Width, bt.Rectangle.Height)
        Txt2.SetBounds(bt2.Rectangle.X, bt2.Rectangle.Y, bt2.Rectangle.Width, bt2.Rectangle.Height)
    End Sub
#End Region

#Region "OFFWB"

    Private Sub AutomaticNewVersionHandler(ByVal _result As Result, ByVal newResultPath As String) Handles OffWB.eAutomaticNewVersion
        RaiseEvent eAutomaticNewVersion(_result, newResultPath)
    End Sub

    Private Sub LoadWebBrowser()
        OffWB = New Zamba.Browser.WebBrowser
        RemoveHandler OffWB.WebBrowserError, AddressOf WebBrowserError
        AddHandler OffWB.WebBrowserError, AddressOf WebBrowserError
        RemoveHandler OffWB.closeBrowser, AddressOf CloseDocument
        AddHandler OffWB.closeBrowser, AddressOf CloseDocument
        OffWB.Dock = DockStyle.Fill
        ToolStripContainer1.ContentPanel.Controls.Add(OffWB)
        OffWB.BringToFront()
    End Sub

    Private Sub LoadMsgBrowser()
        'se debe generar un solo panel
        If IsNothing(MsgWB) Then
            MsgWB = New Panel

            'NOTA: AL AGREGAR ESTE PANEL, EL HOOK SE ROMPE Y NO PUEDE SER MOSTRADO
            'AddMessageLabel("El documento se ha abierto por fuera de Zamba Software." & vbCrLf & _
            '            "En caso de no poder visualizarlo, por favor consulte con el área de sistemas de su empresa.", _
            '            MsgWB.Controls)

            MsgTimer = New Timer()
            winHandle = IntPtr.Zero
            RemoveHandler MsgTimer.Tick, AddressOf MsgTimer_Tick
            AddHandler MsgTimer.Tick, AddressOf MsgTimer_Tick
            MsgTimer.Interval = 100
            MsgTimer.Enabled = False

            MsgWB.Dock = DockStyle.Fill
            ToolStripContainer1.ContentPanel.Controls.Add(MsgWB)
            MsgWB.BringToFront()

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Cargando el mensaje al hook")
            HookOutlookWindow(MsgWB, Result)
        End If

    End Sub

    ''' <summary>
    ''' Carga el visualizador de documentos PDF de Spire
    ''' </summary>
    ''' <param name="filePath">Ruta del archivo PDF a cargar</param>
    ''' <remarks></remarks>
    Private Sub LoadPdfBrowser(Optional ByVal filePath As String = "")
        If filePath.Length > 0 Then
            If File.Exists(filePath) Then
                PdfBrowser = New PdfDocumentViewerFix()
                PdfBrowser.Dock = DockStyle.Fill

                ToolStripContainer1.ContentPanel.Controls.Add(PdfBrowser)
                PdfBrowser.BringToFront()

                PdfBrowser.LoadFromFile(filePath)
                'PdfBrowser.Fix()
            Else
                Throw New FileNotFoundException("No se ha encontrado el archivo especificado", filePath)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Agrega un label con un mensaje a una colección de controles
    ''' </summary>
    ''' <param name="message"></param>
    ''' <param name="controls"></param>
    ''' <remarks>Se utiliza para notificar al usuario al ocurrir diferentes situaciones sobre la visualización del mismo.
    ''' Por ejemplo al ocurrir un error en la apertura, al visualizarlo por fuera, etc...
    ''' </remarks>
    Private Sub AddMessageLabel(ByVal message As String, ByRef controls As ControlCollection)
        'Crea el mensaje
        Dim lblNonPreview As New Label()
        lblNonPreview.Dock = DockStyle.Fill
        lblNonPreview.Text = message

        'Agranda la letra para ocupar mas espacio y sea mas legible
        Dim font As New Font(lblNonPreview.Font.FontFamily, 9)
        lblNonPreview.Font = font

        'Agrega el mensaje al control
        If controls.Count > 0 Then controls.Clear()
        controls.Add(lblNonPreview)
        lblNonPreview.Width = Width
        lblNonPreview.Height = Height
    End Sub

    Private Sub LoadWordBrowser()
        BPrint.Visible = False
        btnSaveAs.Visible = False
        ToolBar1.Visible = True
        ToolBar1.Dock = DockStyle.Top

        WordOffWB = New WordBrowser(ToolStripContainer1.ContentPanel)

        RemoveHandler WordOffWB.eCloseDocument, AddressOf CloseDocument
        AddHandler WordOffWB.eCloseDocument, AddressOf CloseDocument

    End Sub

    Private Sub LoadExcelBrowser()
        BPrint.Visible = False
        btnSaveAs.Visible = False
        ToolBar1.Visible = True
        ToolBar1.Dock = DockStyle.Top

        ExcelOffWB = New ExcelBrowser(Me)
    End Sub

    Private Sub MsgTimer_Tick()

        If MsgTimer.Enabled = True AndAlso MsgWB IsNot Nothing Then
            MsgTimer.Enabled = False
            Dim x As IntPtr
            Dim threadId As IntPtr = IntPtr.Zero
            Dim pId As IntPtr = IntPtr.Zero
            Dim actualParent As IntPtr = IntPtr.Zero
            If winHandle = IntPtr.Zero Then FindWindowByApi(myOutlook.ActiveInspectorCaption)

            If winHandle = IntPtr.Zero Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "NO Se encontro Handle de la ventana " & Date.Now.ToString())
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se encontro Handle ( " & winHandle.ToString() & " ) el Titulo es '" & myOutlook.ActiveInspectorCaption & "' " & Date.Now.ToString())
            End If
            Try
                If (MsgWB Is Nothing OrElse MsgWB.IsDisposed) Then
                    MsgTimer.Enabled = False
                    Exit Sub
                End If

                If (winHandle <> IntPtr.Zero AndAlso Not OutOfZamba AndAlso actualParent <> MsgWB.Handle) Then
                    If Boolean.Parse(UserPreferences.getValue("ShowMsgControlBox", UPSections.ExportaPreferences, "False")) = True Then
                        SetMsgPanelAttributes()
                    End If

                    oldParent = GetParent(winHandle)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, " Obtiene el Parent : " & oldParent.ToString() & " - " & Date.Now.ToString())

                    x = SetParent(winHandle, MsgWB.Handle)

                    ZTrace.WriteLineIf(ZTrace.IsInfo, " Setea al Panel(" & MsgWB.Handle.ToString() & ") como parent de la ventana " & Date.Now.ToString())

                    System.Threading.Thread.Sleep(500)
                    MsgPanelSetSize()

                    actualParent = GetParent(winHandle)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, " Obtiene el Parent ACTUAL : " & actualParent.ToString() & " - " & Date.Now.ToString())

                    ZTrace.WriteLineIf(ZTrace.IsInfo, " Se ha finalizado con los seteos para la visualización " & Date.Now.ToString())

                    MsgTimer.Enabled = False
                    MsgTimer.Stop()
                Else

                    If OutOfZamba AndAlso winHandle <> IntPtr.Zero Then
                        SetActiveWindow(winHandle)
                        MsgTimer.Enabled = False
                    Else
                        If timOut >= 300 Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, " TIMEOUT : NO SE ENCONTRO VENTANA " & Date.Now.ToString())
                            MsgTimer.Enabled = False
                        Else
                            timOut = timOut + 1
                            ZTrace.WriteLineIf(ZTrace.IsInfo, " TIME COUNT = " & timOut.ToString() & " " & Date.Now.ToString())
                        End If
                    End If
                End If
            Catch ex As ObjectDisposedException
                ZClass.raiseerror(ex)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

            ZTrace.WriteLineIf(ZTrace.IsInfo, " -- End sub de MsgTimer_Tick -- " & Date.Now.ToString())

        Else
            ZTrace.WriteLineIf(ZTrace.IsInfo, "El panel de WebBrowser no se encontraba instanciado." & Date.Now.ToString())
            ZTrace.WriteLineIf(ZTrace.IsInfo, " -- End sub de MsgTimer_Tick -- " & Date.Now.ToString())
            MsgTimer.Enabled = False
        End If
        MsgPanelSetSize()
    End Sub

    Private Function FindWindowByApi(ByVal WindowCaption As String) As System.IntPtr
        If winHandle = IntPtr.Zero Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Comienza a buscar la ventana ..." & Date.Now.ToString())
            winHandle = FindWindow(Outlook, WindowCaption)
            If winHandle = IntPtr.Zero Then winHandle = FindWindow(Word, WindowCaption)
        End If
        Return winHandle
    End Function

    Private Sub SetMsgPanelAttributes()

        If winHandle <> IntPtr.Zero Then

            hMenu = GetSystemMenu(winHandle, 0)
            DeleteMenu(hMenu, SC_MINIMIZE, MF_BYCOMMAND)
            DeleteMenu(hMenu, SC_MAXIMIZE, MF_BYCOMMAND)
            DeleteMenu(hMenu, SC_SIZE, MF_BYCOMMAND)
            DeleteMenu(hMenu, SC_MOVE, MF_BYCOMMAND)
            DeleteMenu(hMenu, SC_RESTORE, MF_BYCOMMAND)
            DeleteMenu(hMenu, SC_NEXTWINDOW, MF_BYCOMMAND)
            DeleteMenu(hMenu, SC_CLOSE, MF_BYCOMMAND)
            DeleteMenu(hMenu, 0, MF_BYPOSITION)
            DrawMenuBar(hMenu)
            Dim winStyle As Int32 = GetWindowLong(winHandle, GWL_STYLE)
            oldWinStyle = winStyle
            winStyle = winStyle And Not WS_MAXIMIZEBOX
            winStyle = winStyle And Not WS_MINIMIZEBOX
            winStyle = winStyle And Not WS_SYSMENU
            winStyle = winStyle And Not WS_MAXIMIZE
            winStyle = winStyle And Not WS_CAPTION
            winStyle = winStyle Or WS_CHILD
            SetWindowLong(winHandle, GWL_STYLE, winStyle)

        End If
    End Sub

    Private Sub RestoreMsgPanelAttributes()

        If winHandle <> IntPtr.Zero Then
            GetSystemMenu(winHandle, 1)
            SetWindowLong(winHandle, GWL_STYLE, oldWinStyle)
            ZTrace.WriteLineIf(ZTrace.IsInfo, " Se Restauraron atributos de ventana (Botones de cerrar, minimizar y demas) " & Date.Now.ToString())
        End If


    End Sub

    Private Sub MsgPanelSetSize()
        Dim ret As IntPtr
        Dim msgTop As Integer
        If Integer.TryParse(UserPreferences.getValue("MsgViewerTopPosition", UPSections.ExportaPreferences, "0"), msgTop) = False Then
            msgTop = 0
        End If

        Dim width As Int32 = MsgWB.Width
        Dim height As Int32 = MsgWB.Height + msgTop


        If winHandle <> IntPtr.Zero AndAlso OutOfZamba = False Then
            ret = SetWindowPos(winHandle, 1, 0, -msgTop, width, height, 0)
            ret = ShowWindow(winHandle, SW_MAXIMIZE)
            ZTrace.WriteLineIf(ZTrace.IsInfo, " RESIZE , DOCK " & Date.Now.ToShortTimeString())
            SetActiveWindow(winHandle)
        End If
    End Sub

    ''' <summary>
    ''' Muestra un documento office
    ''' </summary>
    ''' <param name="Result">Documento office a visualizar</param>
    ''' <param name="setReadOnly">Opción que habilita o deshabilita la opción de solo lectura mediante la regla DoSetRights. 
    '''                             Tiene prioridad ante cualquier otro permiso u opción de edición</param>
    ''' <remarks></remarks>
    Private Sub ShowOfficeDocument(ByRef Result As Result, ByVal setReadOnly As Nullable(Of Boolean), Optional ByVal CreateTempFile As Boolean = True)
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Ingreso en ShowOfficeDocument de DocumentViewer2 " & Date.Now.ToShortTimeString())

            If CInt(Result.isShared) = 1 Then
                btnSaveAs.Enabled = False
            Else
                If Not RightsBusiness.GetUserRights(ObjectTypes.Documents, RightsType.Saveas) Then
                    btnSaveAs.Visible = False
                End If
                If RightsBusiness.GetUserRights(ObjectTypes.Documents, RightsType.Saveas) AndAlso Membership.MembershipHelper.CurrentUser.ID = Result.OwnerID AndAlso btnSaveAs.Enabled = False Then
                    btnSaveAs.Enabled = True
                End If
                If RightsBusiness.GetUserRights(ObjectTypes.Documents, RightsType.Saveas) AndAlso Membership.MembershipHelper.CurrentUser.ID <> Result.OwnerID AndAlso btnSaveAs.Enabled = True Then
                    btnSaveAs.Enabled = False
                End If
            End If

            If UserBusiness.Rights.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.ModuleElectronicDoc, RightsType.Use) = False Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "El Módulo de Documentos Electrónicos no se encuentra habilitado " & Date.Now.ToShortTimeString())
                Dim Proceso As System.Diagnostics.Process = New System.Diagnostics.Process
                Diagnostics.Process.Start(Result.FullPath)
                Exit Sub
            End If

            If setReadOnly Is Nothing Then
                If UserBusiness.Rights.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.Edit, Result.DocTypeId) Then
                    If Result.HasVersion = 1 AndAlso UserBusiness.Rights.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.EditVersions, Result.DocTypeId) Then
                        Result.DocType.IsReadOnly = False
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "IsReadOnly se pone en False x tener permiso edicion sobre la version de Entidad" & Date.Now.ToShortTimeString())
                    Else
                        If Result.HasVersion = 0 Then
                            Result.DocType.IsReadOnly = False
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "IsReadOnly se pone en False x tener permiso edicion sobre la Entidad" & Date.Now.ToShortTimeString())
                        Else
                            MessageBox.Show("No tiene los permisos necesarios para Editar un archivo versionado. Se abrirá en modo Solo Lectura", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Result.DocType.IsReadOnly = True
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "IsReadOnly se pone en True no tener permiso de edicion sobre la entidad o sobre la edicion del versionado de la entidad" & Date.Now.ToShortTimeString())
                        End If

                    End If

                Else
                    Result.DocType.IsReadOnly = True
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "IsReadOnly se pone en True x falta permiso edicion sobre la Entidad" & Date.Now.ToShortTimeString())
                End If
                If CInt(Result.isShared) <> 1 And Result.DocTypeId <> 0 Then
                    If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.OwnerChanges, Result.DocTypeId) AndAlso Membership.MembershipHelper.CurrentUser.ID = Result.OwnerID AndAlso Result.DocType.IsReadOnly = True Then
                        Result.DocType.IsReadOnly = False
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "IsReadOnly se pone en False por ser el creador del documento y esta marcado el permiso de modificacion por el creador " & Date.Now.ToShortTimeString())
                    End If
                    If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.OwnerChanges, Result.DocTypeId) AndAlso Membership.MembershipHelper.CurrentUser.ID <> Result.OwnerID AndAlso Result.DocType.IsReadOnly = False Then
                        If Not UserBusiness.Rights.DisableOwnerChanges(Membership.MembershipHelper.CurrentUser, Result.DocTypeId) Then
                            Result.DocType.IsReadOnly = True
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "IsReadOnly se pone en True porque no es el creador del documento y esta marcada la opcion solo modifica el creador" & Date.Now.ToShortTimeString())
                        End If
                    End If
                ElseIf CInt(Result.isShared) = 1 Then
                    If Result.DocTypeId <> 0 Then
                        Result.DocType.IsReadOnly = True
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "IsReadOnly se pone en True porque isShared = 1 " & Date.Now.ToShortTimeString())
                    End If
                End If

                'ML: Wi:8929 Se verifica si se esta abriendo en resultados o tarea, si es resultado y la opcion de no edicion esta habilitada
                'No permite guardar los cambios en los documentos.
                If _isWF = False Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "IsWF esta false" & Date.Now.ToShortTimeString())
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "IsWF esta true" & Date.Now.ToShortTimeString())
                    If TypeOf (Result) Is TaskResult Then
                        Dim DisableEditByStepRight As Boolean = UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.WFSteps, RightsType.Edit, DirectCast(Result, ITaskResult).StepId)
                        If DisableEditByStepRight Then
                            Result.DocType.IsReadOnly = True
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Tiene el Permiso de Deshabilitar Edicion por Etapa, se pone ReadOnly en True porque es WF" & Date.Now.ToShortTimeString())
                        Else
                            Result.DocType.IsReadOnly = False
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "NO Tiene el Permiso de Deshabilitar Edicion por Etapa, se pone ReadOnly en False porque es WF" & Date.Now.ToShortTimeString())
                        End If
                    End If
                End If
            Else
                Result.DocType.IsReadOnly = setReadOnly
            End If

            If CreateTempFile = False Then
                If Result.Disk_Group_Id = -1 OrElse _file Is Nothing Then
                    _file = Result.RealFullPath
                End If
            End If
            'Si se pudo crear el temporal sigo con la visualizacion
            If CreateTempFile = False OrElse CreateTempFileToShow(Result) Then
                Dim extension As String = Path.GetExtension(_file)
                Dim modifyOpening As Boolean

                docOpenedDate = Now

                If Result.IsWord Then
                    modifyOpening = Boolean.Parse(UserPreferences.getValue("UseWordBrowser", UPSections.UserPreferences, "False"))
                    If modifyOpening Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Utilizando WordBrowser (visualizador embebido) para visualizar el archivo con extensión " & extension)
                        LoadWordBrowser()
                        WordOffWB.ShowDocument(_file, Result.DocType.IsReadOnly)

                        Exit Sub
                    End If

                    modifyOpening = Boolean.Parse(UserPreferences.getValue("WordPorFuera", UPSections.UserPreferences, "False"))
                    If modifyOpening Then
                        System.Diagnostics.Process.Start(_file)
                        AddMessageLabel("El documento se ha abierto por fuera de Zamba Software." & vbCrLf &
                                            "Este comportamiento es determinado por la configuración de apertura de documentos Word desde Zamba." & vbCrLf &
                                            "En caso de no poder visualizarlo, por favor consulte con el área de sistemas de su empresa.",
                                            ToolStripContainer1.ContentPanel.Controls)
                        Exit Sub
                    End If
                End If

                If Result.IsExcel Then
                    modifyOpening = Boolean.Parse(UserPreferences.getValue("UseExcelBrowser", UPSections.UserPreferences, "False"))
                    If modifyOpening Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Utilizando  ExcelBrowser (visualizador embebido) para visualizar el archivo con extensión " & extension)
                        LoadExcelBrowser()
                        ExcelOffWB.ShowDocument(_file)
                        Exit Sub
                    End If

                    modifyOpening = Boolean.Parse(UserPreferences.getValue("ExcelPorFuera", UPSections.UserPreferences, "False"))
                    If modifyOpening Then
                        System.Diagnostics.Process.Start(_file)
                        AddMessageLabel("El documento se ha abierto por fuera de Zamba Software." & vbCrLf &
                                            "Este comportamiento es determinado por la configuración de apertura de documentos Excel desde Zamba." & vbCrLf &
                                            "En caso de no poder visualizarlo, por favor consulte con el área de sistemas de su empresa.",
                                            ToolStripContainer1.ContentPanel.Controls)
                        Exit Sub
                    End If
                End If

                If Result.IsRTF Then
                    modifyOpening = Boolean.Parse(UserPreferences.getValue("UseRtfBrowser", UPSections.UserPreferences, "False"))
                    If modifyOpening Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Utilizando  RtfBrowser (visualizador embebido) para visualizar el archivo con extensión " & extension)
                        ShowRtf()
                        Exit Sub
                    End If
                End If

                If Result.IsPDF Then
                    modifyOpening = Boolean.Parse(UserPreferences.getValue("UsePdfBrowser", UPSections.UserPreferences, "True"))
                    If modifyOpening Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Utilizando  PdfBrowser (visualizador embebido) para visualizar el archivo con extensión " & extension)
                        LoadPdfBrowser(_file)
                        Exit Sub
                    End If

                    modifyOpening = Boolean.Parse(UserPreferences.getValue("PdfPorFuera", UPSections.UserPreferences, "False"))
                    If modifyOpening Then
                        Dim ps As New System.Diagnostics.ProcessStartInfo(_file)
                        ps.UseShellExecute = True
                        ps.WindowStyle = ProcessWindowStyle.Normal
                        System.Diagnostics.Process.Start(ps)
                        AddMessageLabel("El documento se ha abierto por fuera de Zamba Software." & vbCrLf &
                                            "Este comportamiento es determinado por la configuración de apertura de documentos PDF desde Zamba." & vbCrLf &
                                            "En caso de no poder visualizarlo, por favor consulte con el área de sistemas de su empresa.",
                                            ToolStripContainer1.ContentPanel.Controls)
                        Exit Sub
                    End If
                End If
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Utilizando WebBrowser (navegador web) para visualizar el archivo con extensión " & extension)

                LoadWebBrowser()
                OffWB.ShowDocument(Result, _file)
            Else

                MessageBox.Show("El documento no esta accesible, consulte al administrador del sistema.", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                ZTrace.WriteLineIf(ZTrace.IsError, "El documento no se encuentra, verificar que exista en el volumen.")
            End If
        Catch ex As FileNotFoundException
            Zamba.Core.ZClass.raiseerror(ex)
            AddMessageLabel("No se ha podido encontrar el documento solicitado. Consulte con el área de sistemas que el mismo exista y tenga los permisos necesarios para acceder a estos.",
                            ToolStripContainer1.ContentPanel.Controls)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Visualiza documentos RTF
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ShowRtf()
        rtfViewer = New RichTextBox()

        'Se encarga de bloquear la escritura sobre el richtextbox, sin
        'cambiar el color como lo hace la propiedad ReadOnly=True
        RemoveHandler rtfViewer.KeyDown, AddressOf rtfViewer_KeyDown
        AddHandler rtfViewer.KeyDown, AddressOf rtfViewer_KeyDown

        rtfViewer.Dock = DockStyle.Fill
        ToolStripContainer1.ContentPanel.Controls.Add(rtfViewer)
        rtfViewer.BringToFront()

        Try
            rtfViewer.LoadFile(_file, RichTextBoxStreamType.RichText)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            rtfViewer.Controls.Clear()
            AddMessageLabel("Ha ocurrido un error al abrir el documento. Es posible que el documento se encuentre abierto por otra aplicación.",
                            rtfViewer.Controls)
        End Try

    End Sub

    Private Sub rtfViewer_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        e.SuppressKeyPress = True
    End Sub

    ''' <summary>
    ''' Muestra el mensaje del documento
    ''' </summary>
    ''' <param name="Result"></param>
    ''' <remarks></remarks>
    Private Sub ShowMsgDocument(ByRef Result As Result)
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Entrando al show message")
            If CInt(Result.isShared) <> 1 And Result.DocTypeId <> 0 Then
                If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.OwnerChanges, Result.DocTypeId) AndAlso Membership.MembershipHelper.CurrentUser.ID = Result.OwnerID AndAlso Result.DocType.IsReadOnly = True Then
                    Result.DocType.IsReadOnly = False
                End If
                If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.OwnerChanges, Result.DocTypeId) AndAlso Membership.MembershipHelper.CurrentUser.ID <> Result.OwnerID AndAlso Result.DocType.IsReadOnly = False Then
                    If Not UserBusiness.Rights.DisableOwnerChanges(Membership.MembershipHelper.CurrentUser, Result.DocTypeId) Then
                        Result.DocType.IsReadOnly = True
                    End If
                End If
            ElseIf CInt(Result.isShared) = 1 Then
                If Result.DocTypeId <> 0 Then
                    Result.DocType.IsReadOnly = True
                End If
            End If

            If Boolean.Parse(UserPreferences.getValue("OpenMsgWithProcessStart", UPSections.UserPreferences, "False")) Then
                'Copia local del msg
                Dim tempFile As String = MembershipHelper.AppTempPath & "\OfficeTemp\" & Result.Doc_File
                Results_Business.CopyFileToTemp(Result, Result.FullPath, tempFile)
                'Se abre el msg sin hacer el hook
                System.Diagnostics.Process.Start(tempFile)

                AddMessageLabel("El documento se ha abierto por fuera de Zamba Software." & vbCrLf &
                                "Este comportamiento es determinado por la configuración de apertura de mails de Outlook desde Zamba." & vbCrLf &
                                "En caso de no poder visualizarlo, por favor consulte con el área de sistemas de su empresa.",
                                ToolStripContainer1.ContentPanel.Controls)
            Else
                Select Case CheckOutlookVersionInstalled()
                    Case 9
                        'parche para 2000
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Versión de outlook en office 2000")
                        OpenMsgOffice2000(Result)

                    Case Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Cargando el browser de outlook")
                        LoadMsgBrowser()

                End Select

            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub



    Private Sub CloseMailItemEventHandler()
        Try

            ZTrace.WriteLineIf(ZTrace.IsInfo, "UCDocumentViewer2 - CloseMailItemEventHandler")
            If Not IsNothing(myOutlook) AndAlso Me.isDisposed = False Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Calling CloseDocument()")
                Invoke(New dCloseTab(AddressOf CloseTab))
            End If
        Catch ex As InvalidOperationException

        End Try
    End Sub
    Delegate Sub dCloseTab()

    ''' <summary>
    ''' Ingresa el archivo de mensaje a la ventana
    ''' </summary>
    ''' <param name="msgwb"></param>
    ''' <param name="Result"></param>
    ''' <remarks></remarks>
    <SecurityPermission(SecurityAction.Assert)>
    Private Sub HookOutlookWindow(ByVal msgwb As Panel, ByVal Result As IResult)

        winHandle = IntPtr.Zero
        ZTrace.WriteLineIf(ZTrace.IsInfo, " ---------------MsgDocumentViewerTrace--------------- " & Date.Now.ToString())
        ZTrace.WriteLineIf(ZTrace.IsInfo, " ---------------------------------------------------- ")
        OutOfZamba = Boolean.Parse(UserPreferences.getValue("OpenMsgOutOfZamba", UPSections.ExportaPreferences, "False"))
        ZTrace.WriteLineIf(ZTrace.IsInfo, " Abrir fuera de Zamba = " & OutOfZamba.ToString())
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Creando archivo temporal...")
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Result.FullPath: " & Result.FullPath)
        Dim Dir As System.IO.DirectoryInfo = GetTempDir("\OfficeTemp")
        Dim strPathLocal As String = FileBusiness.GetUniqueFileName(Dir.FullName, Result.FullPath.Remove(0, Result.FullPath.LastIndexOf("\")))
        ' strPathLocal = Path.Combine(Path.GetDirectoryName(strPathLocal), Path.GetFileNameWithoutExtension(strPathLocal) & Path.GetExtension(strPathLocal))

        Try
            'Si no existe lo copia
            If Not String.IsNullOrEmpty(strPathLocal) Then
                If Not File.Exists(strPathLocal) Then
                    Results_Business.CopyFileToTemp(Result, Result.FullPath, strPathLocal)
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        ZTrace.WriteLineIf(ZTrace.IsInfo, " Se creo Archivo temporal de " & strPathLocal & " " & Date.Now.ToString())

        myOutlook = Office.Outlook.SharedOutlook.GetOutlook()
        RemoveHandler myOutlook.CloseMailItemEvent, AddressOf CloseMailItemEventHandler
        AddHandler myOutlook.CloseMailItemEvent, AddressOf CloseMailItemEventHandler

        Dim winState As FormWindowState
        If OutOfZamba Then
            winState = FormWindowState.Maximized
        Else
            winState = FormWindowState.Minimized
        End If

        Try
            winHandle = myOutlook.OpenMailItem(strPathLocal, False, winState, True)
        Catch ex As System.Runtime.InteropServices.COMException
            If ex.Message.Contains("A dialog box is open. Close it and try again") Or ex.Message.Contains("Hay un cuadro de diálogo abierto. Ciérrelo y vuelva a intentarlo") Then
                MessageBox.Show("Hay un cuadro de diálogo abierto en Outook. Ciérrelo y vuelva a intentarlo", "Error al abrir el archivo", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Throw ex
            Else
                MessageBox.Show("Error al abrir el archivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Throw ex
            End If
        Catch ex As FileNotFoundException
            MessageBox.Show("No se ha encontrado el archivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        Catch ex As NotSupportedException
            MessageBox.Show(ex.Message, "Versión de Office incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        Catch ex As Exception
            MessageBox.Show("Error al abrir el archivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        End Try

        If Not OutOfZamba Then
            MsgTimer.Enabled = True
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Timer ON")
        End If
    End Sub
    Private Function closeProc()
        Try
            If Not IsNothing(proc) Then
                proc.Kill()
                proc.Dispose()
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        proc = Nothing
    End Function

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
    Private Sub WebBrowserError(ByVal Exception As Exception)
        Try
            ClearUCBrowser()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "WF Designer"
    Private Sub ShowWorkFlowDesigner(ByRef Result As Result)
        Try
            If UserBusiness.Rights.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.Edit, Result.DocTypeId) Then
                Result.DocType.IsReadOnly = False
            Else
                Result.DocType.IsReadOnly = True
            End If
            LoadWorkFlowDesigner(Not (Result.DocType.IsReadOnly), Result.FullPath)
            If Not RightsBusiness.GetUserRights(ObjectTypes.Documents, RightsType.Print) Then
                btnPrint.Visible = False
                btnPrintPreview.Visible = False
            End If
            If Not RightsBusiness.GetUserRights(ObjectTypes.DocTypes, RightsType.Saveas, CInt(Result.DocTypeId)) Then
                btnSaveAs.Visible = False
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub LoadWorkFlowDesigner(ByVal Enabled As Boolean, ByVal Path As String)
        Try
            'Si esto da error instalar Framework 3.0 y extensiones de workflow
            'NO COMENTAR;
            WFDesigner = New Zamba.WorkFlow.Designer.WorkFlowDesignerControl(Path, Enabled)
            WFDesigner.Dock = DockStyle.Fill
            ToolStripContainer1.ContentPanel.Controls.Add(WFDesigner)
            WFDesigner.BringToFront()
            'Try
            '    RemoveHandler WFDesigner.ShowDocAsociated, AddressOf ShowDocAsociated
            '    AddHandler WFDesigner.ShowDocAsociated, AddressOf ShowDocAsociated
            'Catch ex As Exception

            'End Try
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub ShowDocAsociated(ByVal Id As Integer)
        Try
            RaiseEvent ShowDocumentsAsociated(Id)
        Catch ex As Exception
        End Try
    End Sub
#End Region
#Region "FileNotOpen"
    Private Sub ShowLabelError(ByVal ex As String)
        If Not ToolStripContainer1 Is Nothing AndAlso Not ToolStripContainer1.ContentPanel Is Nothing Then
            Dim l As New Label
            l.Text = ex
            l.Dock = System.Windows.Forms.DockStyle.Fill
            l.BackColor = System.Drawing.Color.Transparent
            l.Font = New Font("Verdana", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
            l.ForeColor = System.Drawing.Color.DarkBlue
            l.Location = New System.Drawing.Point(0, 0)
            l.Name = "Label1"
            l.Size = New System.Drawing.Size(648, 334)
            l.TextAlign = ContentAlignment.MiddleCenter

            ToolStripContainer1.ContentPanel.Controls.Add(l)
        End If

    End Sub
#End Region

#End Region

#Region "Notas"

    'Private Property Alto() As Int32
    '    Get
    '        Return Alto
    '    End Get
    '    Set(ByVal Value As Int32)
    '        Alto = Value
    '    End Set
    'End Property
    'Private Property Ancho() As Int32
    '    Get
    '        Return Ancho
    '    End Get
    '    Set(ByVal Value As Int32)
    '        Ancho = Value
    '    End Set
    'End Property

    Private Sub DisposeNotas()
        Try
            If Not (_notes Is Nothing) Then
                _notes = Nothing
            End If
            If Not (oFDimension Is Nothing) Then
                oFDimension = Nothing
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub RemoveSign(ByVal s As Sign)
        Try
            Notes.Remove(s)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    ''' <summary>
    ''' [sebastian] 09-06-2009 Modified Se agrego cast para salvar warning
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ShowNotes()
        Try
            If UserBusiness.Rights.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.Notas, RightsType.View) Then
                Notes = NotesControlFactory.GetNotes(CInt(Result.ID), ServerImagesPath)
                For Each note As BaseNote In Notes
                    ImgViewer.Controls.Add(note)
                    If TypeOf (note) Is Sign Then
                        RemoveHandler DirectCast(note, Sign).DestroySign, AddressOf RemoveSign
                        AddHandler DirectCast(note, Sign).DestroySign, AddressOf RemoveSign
                    End If
                    note.BringToFront()
                    note.Edited = False
                Next
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Notas & Sign"
    Private Sub btnnotasHandlers()
        RemoveHandler ImgViewer.MouseEnter, AddressOf Img_MouseEnter
        RemoveHandler ImgViewer.MouseLeave, AddressOf Img_MouseLeave
        'RemoveHandler ImgViewer.PicBox.MouseEnter, AddressOf Img_MouseEnter
        'RemoveHandler ImgViewer.PicBox.MouseLeave, AddressOf Img_MouseLeave
        RemoveHandler ImgViewer.PicBox2.Click, AddressOf Img_Click
        'RemoveHandler ImgViewer.PicBox.Click, AddressOf Img_Click
        AddHandler ImgViewer.MouseEnter, AddressOf Img_MouseEnter
        AddHandler ImgViewer.MouseLeave, AddressOf Img_MouseLeave
        'AddHandler ImgViewer.PicBox.MouseEnter, AddressOf Img_MouseEnter
        'AddHandler ImgViewer.PicBox.MouseLeave, AddressOf Img_MouseLeave
        AddHandler ImgViewer.PicBox2.Click, AddressOf Img_Click
        'AddHandler ImgViewer.PicBox.Click, AddressOf Img_Click
    End Sub
    Private Sub Img_MouseEnter(ByVal sender As System.Object, ByVal e As EventArgs)
        Try
            If IsNotesPushed OrElse IsSignPushed Then
                Cursor = Cursors.Cross
            Else
                Cursor = Cursors.Default
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub Img_MouseLeave(ByVal sender As System.Object, ByVal e As EventArgs)
        Try
            If IsNotesPushed OrElse IsSignPushed Then
                Cursor = Cursors.Cross
            Else
                Cursor = Cursors.Default
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    'Dim _Estado As Int32

    'Public Property Estado()
    '    Get
    '        Return _Estado
    '    End Get
    '    Set(ByVal Value)
    '        _Estado = Value
    '        Me.ImgViewer.Estado = Value
    '    End Set
    'End Property


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' EVENTO QUE CAPTURA EL CLICK EN EL PICTUREBOX Y SE FIJA SI HAY QUE AGREGAR UNA NOTA
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>NOTAS</remarks>
    ''' -----------------------------------------------------------------------------
    Private Sub Img_Click(ByVal sender As System.Object, ByVal e As EventArgs)
        Try
            Dim mouseButton As System.Windows.Forms.MouseEventArgs = DirectCast(e, System.Windows.Forms.MouseEventArgs)
            If mouseButton.Button = MouseButtons.Left Then
                If Estado = Estados.Nota Then
                    If UserBusiness.Rights.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.Notas, RightsType.Create) Then
                        Dim NotePosition As New Drawing.Point
                        NotePosition.X = System.Windows.Forms.Cursor.Position.X
                        NotePosition.Y = System.Windows.Forms.Cursor.Position.Y
                        Dim Note As BaseNote = NotesControlFactory.NewNote(CInt(UserBusiness.Rights.CurrentUser.ID), UserBusiness.Rights.CurrentUser.Name, UserBusiness.Rights.CurrentUser.Apellidos, CInt(Result.ID), NotePosition, CBool(0))
                        Notes.Add(Note)

                        If _previewMode Then
                            NotePosition.Y -= 45
                            '                        note.Location = panelMain.Parent.PointToClient(NotePosition)
                            Note.Location = PointToClient(NotePosition)
                        Else
                            NotePosition.Y -= 45
                            Note.Location = Parent.PointToClient(NotePosition)
                        End If

                        ImgViewer.Controls.Add(Note)
                        Note.BringToFront()
                        Estado = Estados.Ninguno
                        Exit Sub
                        'Me.ImgViewer.BtnNota.Pushed = False
                    End If
                End If


                If Estado = Estados.Firma Then

                    'valido que no halla otra firma del mismo usuario
                    For Each n As BaseNote In Notes
                        If n.GetType.Name = "Sign" AndAlso n.UserID = UserBusiness.Rights.CurrentUser.ID Then
                            MessageBox.Show("Usted ya posee imagen de firma en este documento", "Zamba Firma", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Estado = Estados.Ninguno
                            'Me.ImgViewer.BtnFirma.Pushed = False
                            Exit Sub
                        End If
                    Next

                    'valido que tenga la firma actualizada
                    Dim firma As String = UserBusiness.GetUserSignById(CInt(UserBusiness.Rights.CurrentUser.ID))
                    If firma = "" Then
                        MessageBox.Show("Usted no posee imagen de firma", "Zamba Firma", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Estado = Estados.Ninguno
                        'Me.ImgViewer.BtnFirma.Pushed = False
                        Exit Sub
                    End If

                    Dim NotePosition As New Drawing.Point
                    NotePosition.X = System.Windows.Forms.Cursor.Position.X
                    NotePosition.Y = System.Windows.Forms.Cursor.Position.Y


                    Dim SignPath As String = ""
                    Try
                        SignPath = ServerImagesPath & "\" & firma
                    Catch ex As Exception
                        Zamba.Core.ZClass.raiseerror(ex)
                    End Try

                    Dim Note As BaseNote
                    Dim pw As New FrmAskPassword(UserBusiness.Rights.CurrentUser.Password)
                    If pw.ShowDialog() = DialogResult.OK Then
                        '[sebastian ] 09-06-2009 se agrego cast para salvar warning
                        Note = NotesControlFactory.NewNote(CInt(UserBusiness.Rights.CurrentUser.ID), UserBusiness.Rights.CurrentUser.Name, UserBusiness.Rights.CurrentUser.Apellidos, CInt(Result.ID), NotePosition, CBool(1), SignPath)
                        Notes.Add(Note)

                        If _previewMode Then
                            NotePosition.Y -= 45
                            Note.Location = PointToClient(NotePosition)
                        Else
                            NotePosition.Y -= 45
                            Note.Location = Parent.PointToClient(NotePosition)
                        End If

                        ImgViewer.Controls.Add(Note)
                        RemoveHandler DirectCast(Note, Sign).DestroySign, AddressOf RemoveSign
                        AddHandler DirectCast(Note, Sign).DestroySign, AddressOf RemoveSign
                        Note.BringToFront()
                        Estado = Estados.Ninguno

                    Else
                        MessageBox.Show("La clave ingresada no es correcta", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Estado = Estados.Ninguno

                        Exit Sub
                    End If

                End If
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Estado = Estados.Ninguno
        End Try
    End Sub
#End Region

#Region "ImgPreview"

    Private Sub PictureBox1_DoubleClick(ByVal sender As System.Object, ByVal e As EventArgs)
        LoadPreView()
    End Sub
    Private Sub LoadPreView()
        Try
            Prev = New FrmPreviewGold
            Prev.Controls.Add(Me)
            'RemoveHandler ImgViewer.PicBox.DoubleClick, AddressOf PictureBox1_DoubleClick
            '    ImgViewer.btnPreview.Visible = False
            'TamanoNormal()
            _previewMode = True
            FillParentFormText()
            Prev.ShowDialog()
            ReloadImage()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub Prev_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Prev.Closing
        Try
            ToolStripContainer1.ContentPanel.Controls.Add(Prev.Controls(0))
            _previewMode = False
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub FillParentFormText()
        If _previewMode Then
            Text = Result.Name
            If iCount > 0 Then
                txtNumPag.Items.Clear()
                For A As Int32 = 1 To iCount + 1
                    txtNumPag.Items.Add(A)
                Next
                Text += "  (" & actualFrame + 1 & " de " & iCount + 1 & ")"
            Else
                txtNumPag.Items.Clear()
            End If
        Else
            txtNumPag.Items.Clear()
            For A As Int32 = 1 To iCount + 1
                txtNumPag.Items.Add(A)
            Next
            If Result.Name.Length < 20 Then
                Parent.Parent.Text = Result.Name
            Else
                Parent.Parent.Text = "..." & Result.Name.Substring(Result.Name.Length - 20, 20)
            End If
            Parent.Parent.Text += "  (" & actualFrame + 1 & " de " & iCount + 1 & ")"
        End If
    End Sub
#End Region

#Region "Clear"

    Public Sub ClearUCBrowser()
        Try
            If OffWB IsNot Nothing AndAlso OffWB.IsDisposed = False Then
                OffWB.Dispose()
                OffWB = Nothing
            ElseIf WordOffWB IsNot Nothing Then
                WordOffWB.Dispose()
                WordOffWB = Nothing
                GC.Collect()
            ElseIf ExcelOffWB IsNot Nothing Then
                ExcelOffWB.Dispose()
                ExcelOffWB = Nothing
                GC.Collect()
            ElseIf rtfViewer IsNot Nothing AndAlso rtfViewer.IsDisposed = False Then
                rtfViewer.Dispose()
                rtfViewer = Nothing
            ElseIf PdfBrowser IsNot Nothing AndAlso PdfBrowser.IsDisposed = False Then
                PdfBrowser.CloseDocument()
            ElseIf MsgWB IsNot Nothing AndAlso myOutlook IsNot Nothing Then
                CloseMsgDocument()
                myOutlook.DisposingParent = True
                myOutlook.CloseMailItem(myOutlook.closeFromControlbox)
            ElseIf Not IsNothing(FormBrowser) Then
                FormBrowser.CloseWebBrowser()
            End If

            If Result.IsEditable Then
                SaveModifiedDocument()
            End If

            'Si esta instaciada la imagen, libero recursos
            If ImgViewer IsNot Nothing AndAlso _result.IsImage Then
                _result.Picture = Nothing
                If ImgViewer IsNot Nothing AndAlso ImgViewer.PicBox2 IsNot Nothing AndAlso ImgViewer.IsDisposed = False Then
                    ImgViewer.Dispose()
                    ImgViewer = Nothing
                End If

            End If

            If flagAutomaticNewVersion Then
                RaiseEvent eAutomaticNewVersion(_result, _file)
            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    'Variable que va a contener el nombre de la clase que contiene a UCDocumentViewer2
    Public Sub ClosedFromExternalVisualizaer(ByVal min As Boolean)
        If min = True Then
            IsMaximize = True
        Else
            IsMaximize = False
        End If
    End Sub

#End Region

#Region "MultiTiff"
    Private Sub NextPage()
        If Result.IsImage Then
            Try
                If actualFrame < iCount Then
                    actualFrame = actualFrame + 1
                Else
                    actualFrame = iCount
                    Exit Sub
                End If
                lblPagin.Text = actualFrame + 1 & " DE  " & iCount + 1
                If ClonatedImg = True Then
                    ReloadImage()
                End If
                Result.Picture.Image.SelectActiveFrame(oFDimension, actualFrame)
                Result.Picture.AdjustImageRes()
                'ImgViewer.PicBox.Image.SelectActiveFrame(oFDimension, actualFrame)
                ImgViewer.PicBox2.Image.SelectActiveFrame(oFDimension, actualFrame)
                ImgViewer.PicBox2.Refresh()
                FillParentFormText()

                'RaiseEvent MultiTiffPageChanged(Me)
                If ClonatedImg = True Then
                    'h = ImgViewer.PicBox.Height
                    'w = ImgViewer.PicBox.Width
                    'ImgViewer.PicBox.Height = w
                    'ImgViewer.PicBox.Width = h
                    Dim Rot As Int32 = _rotation
                    _rotation = 0
                    Rotate(Rot)
                End If
                RestoreScroll()
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End If
    End Sub
    Private Sub PreviusPage()
        If Result.IsImage Then
            Try
                If actualFrame > 0 Then
                    actualFrame = actualFrame - 1
                Else
                    actualFrame = 0
                    Exit Sub
                End If
                lblPagin.Text = actualFrame + 1 & " DE  " & iCount + 1
                If ClonatedImg = True Then
                    ReloadImage()
                End If
                Result.Picture.Image.SelectActiveFrame(oFDimension, actualFrame)
                Result.Picture.AdjustImageRes()
                'ImgViewer.PicBox.Image.SelectActiveFrame(oFDimension, actualFrame)
                'ImgViewer.PicBox.Refresh()
                ImgViewer.PicBox2.Image.SelectActiveFrame(oFDimension, actualFrame)
                ImgViewer.Refresh()
                FillParentFormText()
                'RaiseEvent MultiTiffPageChanged(Me)
                If ClonatedImg = True Then
                    'h = ImgViewer.PicBox.Height
                    'w = ImgViewer.PicBox.Width
                    'ImgViewer.PicBox.Height = w
                    'ImgViewer.PicBox.Width = h
                    Dim Rot As Int32 = _rotation
                    _rotation = 0
                    Rotate(Rot)
                End If
                RestoreScroll()
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End If
    End Sub

    'Public Sub ShowBirdMegaView()
    '    Try
    '        If IsNothing(BirdMegaView) = False Then BirdMegaView.Dispose()

    '        BirdMegaView = New BirdMegaView(Me.ImgViewer.PicBox.Image)
    '        RemoveHandler BirdMegaView.MeMovi, AddressOf MeMovi
    '        AddHandler BirdMegaView.MeMovi, AddressOf MeMovi

    '        RemoveHandler BirdMegaView.FuiMovido, AddressOf FuiMovido
    '        AddHandler BirdMegaView.FuiMovido, AddressOf FuiMovido

    '        RemoveHandler BirdMegaView.MeCerre, AddressOf MeCerre
    '        AddHandler BirdMegaView.MeCerre, AddressOf MeCerre
    '        Try
    '            BirdMegaView.Top = Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Height - BirdMegaView.Height - 30
    '            BirdMegaView.Left = Me.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Width - BirdMegaView.Width - 30
    '        Catch ex As Exception
    '           zamba.core.zclass.raiseerror(ex)
    '        End Try

    '        RaiseEvent ShowingMegaView(True)

    '        BirdMegaView.Show()
    '        BirdMegaView.Focus()
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Private Sub GotoPage(ByVal Page As Int32)
        Try
            'Dim Page As Int32
            Try
                'Page = CInt(Txt.TextBox1.Text)
                Page -= 1
            Catch ex As Exception
                Page = -1
            End Try
            Try
                If Page < 0 Then
                    txtNumPag.Text = ""
                    Exit Sub
                End If
            Catch ex As Exception
            End Try
            txtNumPag.Text = ""

            If Page <> actualFrame And Page <= iCount And Page >= 0 Then
                actualFrame = Page
                If ClonatedImg = True Then
                    ReloadImage()
                End If
                Result.Picture.Image.SelectActiveFrame(oFDimension, actualFrame)
                Result.Picture.AdjustImageRes()
                'ImgViewer.PicBox.Image.SelectActiveFrame(oFDimension, actualFrame)
                'ImgViewer.PicBox.Refresh()
                ImgViewer.PicBox2.Image.SelectActiveFrame(oFDimension, actualFrame)
                ImgViewer.Refresh()
                FillParentFormText()
                'RaiseEvent MultiTiffPageChanged(Me)
                If ClonatedImg = True Then
                    'h = ImgViewer.PicBox.Height
                    'w = ImgViewer.PicBox.Width
                    'ImgViewer.PicBox.Height = w
                    'ImgViewer.PicBox.Width = h
                    Dim Rot As Int32 = _rotation
                    _rotation = 0
                    Rotate(Rot)
                End If
            End If
            RestoreScroll()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub FirstPage()
        If Result.IsImage Then
            Try
                If actualFrame <> 0 Then
                    actualFrame = 0
                    lblPagin.Text = "1 DE  " & iCount + 1
                    If ClonatedImg = True Then
                        ReloadImage()
                    End If
                    Result.Picture.Image.SelectActiveFrame(oFDimension, actualFrame)
                    Result.Picture.AdjustImageRes()
                    'ImgViewer.PicBox.Image.SelectActiveFrame(oFDimension, actualFrame)
                    '               ImgViewer.PicBox.Refresh()
                    ImgViewer.PicBox2.Image.SelectActiveFrame(oFDimension, actualFrame)
                    ImgViewer.Refresh()
                    FillParentFormText()
                    If ClonatedImg = True Then
                        'h = ImgViewer.PicBox.Height
                        'w = ImgViewer.PicBox.Width
                        'ImgViewer.PicBox.Height = w
                        'ImgViewer.PicBox.Width = h
                        Dim Rot As Int32 = _rotation
                        _rotation = 0
                        Rotate(Rot)
                    End If
                End If
                RestoreScroll()
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End If
    End Sub
    Private Sub LastPage()
        If Result.IsImage Then
            Try
                If actualFrame <> iCount Then
                    actualFrame = iCount
                    lblPagin.Text = iCount + 1 & " DE  " & iCount + 1
                    If ClonatedImg = True Then
                        ReloadImage()
                    End If
                    Result.Picture.Image.SelectActiveFrame(oFDimension, actualFrame)
                    Result.Picture.AdjustImageRes()
                    'ImgViewer.PicBox.Image.SelectActiveFrame(oFDimension, actualFrame)
                    '               ImgViewer.PicBox.Refresh()
                    ImgViewer.PicBox2.Image.SelectActiveFrame(oFDimension, actualFrame)
                    ImgViewer.Refresh()
                    FillParentFormText()
                    If ClonatedImg = True Then
                        'h = ImgViewer.PicBox.Height
                        'w = ImgViewer.PicBox.Width
                        'ImgViewer.PicBox.Height = w
                        'ImgViewer.PicBox.Width = h
                        Dim Rot As Int32 = _rotation
                        _rotation = 0
                        Rotate(Rot)
                    End If
                End If
                RestoreScroll()
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End If
    End Sub
    Private Sub RestoreScroll()
        Try
            ScrollControlIntoView(ToolStripContainer1.ContentPanel.Controls(0))
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Visualizador"

#Region "Rotation"
    Private Sub Rotate(ByVal Value As Int32)
        'Dim Hold As Integer = ImgViewer.PicBox.Height
        'Dim Wold As Integer = ImgViewer.PicBox.Width
        If iCount > 1 Then
            'RotatedImg = ImgViewer.PicBox.Image.Clone
            'ImgViewer.PicBox.Image = RotatedImg
            'ImgViewer.PicBox.Image.SelectActiveFrame(oFDimension, actualFrame)
            '[sebastian] 09-06-2009 se agrego cast para salvar warning
            RotatedImg = DirectCast(ImgViewer.PicBox2.Image.Clone, System.Drawing.Image)
            ImgViewer.PicBox2.Image = RotatedImg
            ImgViewer.PicBox2.Image.SelectActiveFrame(oFDimension, actualFrame)
            ClonatedImg = True
        End If
        If Value = 1 Then
            Try
                Dim h As Integer
                Dim w As Integer
                'h = ImgViewer.PicBox.Height
                'w = ImgViewer.PicBox.Width
                h = ImgViewer.PicBox2.Image.Height
                w = ImgViewer.PicBox2.Image.Width

                'ImgViewer.PicBox.Image.RotateFlip(RotateFlipType.Rotate270FlipNone)
                ImgViewer.PicBox2.Image.RotateFlip(RotateFlipType.Rotate270FlipNone)
                'ImgViewer.PicBox.Height = w
                'ImgViewer.PicBox.Width = h
                Result.Picture.SizeHeight = w
                Result.Picture.SizeWidth = h
                Rotation = 1
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        Else
            Try
                Dim H As Decimal
                Dim W As Decimal
                'H = ImgViewer.PicBox.Height
                'W = ImgViewer.PicBox.Width
                H = ImgViewer.PicBox2.Image.Height
                W = ImgViewer.PicBox2.Image.Width

                'ImgViewer.PicBox.Image.RotateFlip(RotateFlipType.Rotate90FlipNone)
                'ImgViewer.PicBox.Height = W
                'ImgViewer.PicBox.Width = H
                ImgViewer.PicBox2.Image.RotateFlip(RotateFlipType.Rotate90FlipNone)
                Result.Picture.SizeHeight = CInt(W)
                Result.Picture.SizeWidth = CInt(H)
                Rotation = -1
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End If
        ImgViewer.RotateNetron()
        ImgViewer.Refresh()
    End Sub
    Private Sub ReloadImage()
        Try
            ImgViewer.PicBox2.Image = Result.Picture.Image
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub disposeRotation()
        Try
            If Not (RotatedImg Is Nothing) Then
                RotatedImg.Dispose()
                RotatedImg = Nothing
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub
#End Region

#Region "Zoom"

    'CAPTURO LA TECLA +, - Y SUPR PARA HACER ZOOM + Y - Y PARA ELIMINAR UN DOCUMENTO
    Private Sub ListView1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            'Dim Hold As Integer = ImgViewer.PicBox.Height
            'Dim Wold As Integer = ImgViewer.PicBox.Width
            Select Case (e.KeyCode)
                Case Keys.Add
                    If Result.IsImage Then
                        ZoomPlus()
                        'Me.RecorrerNotes(Hold, Wold)
                    End If
                Case Keys.Subtract
                    If Result.IsImage Then
                        ZoomMinus()
                        'Me.RecorrerNotes(Hold, Wold)
                    End If
                Case Keys.Delete
                    '	 DeleteDocument()
            End Select
            If e.KeyCode = Keys.Up Then
            End If
            If e.KeyCode = Keys.Down Then
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    'TODO Falta ver que el min y el max del zoom sean logicos segun la resolucion de la imagen
    ' Private sizeLock As System.Drawing.Size

    'METODO QUE REALIZA UN ZOOM +
    Private Function ZoomPlus() As Boolean
        Try
            'If Result.Picture.Resolution < MaxZoom Then
            '    If Result.Picture.Resolution + ZoomScale > MaxZoom Then
            '        Result.Picture.Resolution = MaxZoom
            '    Else
            '        Result.Picture.Resolution += +ZoomScale
            '    End If

            '    Dim Size As System.Drawing.Size = ZPicture.adjustImage(Result.Picture.Resolution, Result.Picture.Size.Height, Result.Picture.ResV, Result.Picture.Size.Width, Result.Picture.ResH)
            '    ImgViewer.PicBox.Size = Size
            '    Result.Picture.Size = Size
            '    Result.Picture.ResH = Result.Picture.Resolution
            '    Result.Picture.ResV = Result.Picture.Resolution
            '    ImgViewer.PicBox.Refresh()
            '    Me.Zoom = Result.Picture.Resolution
            '    'TODO FEDE: Guardar en un temp los datos del zoom
            '    Try
            '        Dim fileTemp As New FileInfo(Application.StartupPath & "\TempZoom.txt")
            '        If fileTemp.Exists Then fileTemp.Delete()
            '        fileTemp = Nothing
            '        Dim fw As New IO.StreamWriter(Application.StartupPath & "\TempZoom.txt")
            '        fw.WriteLine(Result.Picture.Resolution)
            '        fw.WriteLine(Result.Picture.Size.Width)
            '        fw.WriteLine(Result.Picture.Size.Height)
            '        fw.Close()
            '        fw = Nothing
            '    Catch ex As Exception
            '        ZTrace.WriteLineIf(ZTrace.IsInfo,ex.ToString)
            '    End Try
            'End If
            'Aumento la escala del picbox2
            ImgViewer.PicBox2.zoomWidth += CSng(0.5)
            ImgViewer.PicBox2.zoomHeight += CSng(0.5)
            ImgViewer.Refresh()
            'Me.ImgViewer.ZoomNetronPlus()
        Catch ex As System.OutOfMemoryException
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Function
    'METODO QUE REALIZA UN ZOOM -
    Private Function ZoomMinus() As Boolean
        Try

            If ImgViewer.PicBox2.zoomWidth > 0.5 Then
                ImgViewer.PicBox2.zoomWidth -= CSng(0.5)
            End If
            If ImgViewer.PicBox2.zoomHeight > 0.5 Then
                ImgViewer.PicBox2.zoomHeight -= CSng(0.5)
            End If
            ImgViewer.Refresh()
            'Me.ImgViewer.ZoomNetronMinus()
        Catch ex As System.OutOfMemoryException
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Function 'METODO QUE PERMITE VOLVER AL TAMAÑO NORMAL LA IMAGEN
    'Pongo en uno la escala del picbox2
    Private Sub TamanoNormal()
        Try
            'Result.Picture.Resolution = 100
            ''Dim Size As System.Drawing.Size
            'size = ZPicture.adjustImage(Result.Picture.Resolution, Result.Picture.Size.Height, Result.Picture.ResV, Result.Picture.Size.Width, Result.Picture.ResH)
            '         ImgViewer.PicBox.Size = size
            'Result.Picture.Size = size
            'Result.Picture.ResH = Result.Picture.Resolution
            'Result.Picture.ResV = Result.Picture.Resolution
            ''ImgViewer.PicBox.Refresh()
            'Me.Zoom = Result.Picture.Resolution
            'Me.ImgViewer.ZoomNetron()

            ImgViewer.PicBox2.zoomWidth = 1
            ImgViewer.PicBox2.zoomHeight = 1
            ImgViewer.Refresh()
            'Me.ImgViewer.ZoomNetronNormal()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    'Adapta el ancho de la imagen
    Public Sub AnchoPantalla()
        Try
            '  Dim Size As System.Drawing.Size
            'Result.Picture.Resolution = ZPicture.AdjustImageToScreenWidth(ImgViewer.Width - 18, Result.Picture.Resolution, Result.Picture.Size.Width)
            'size = ZPicture.adjustImage(Result.Picture.Resolution, Result.Picture.Size.Height, Result.Picture.ResV, Result.Picture.Size.Width, Result.Picture.ResH)
            'ImgViewer.PicBox.Size = size
            'Result.Picture.Size = size
            'Result.Picture.ResH = Result.Picture.Resolution
            'Result.Picture.ResV = Result.Picture.Resolution
            ''ImgViewer.PicBox.Refresh()
            'Me.Zoom = Result.Picture.Resolution
            'Me.ImgViewer.ZoomNetron()
            '[sebastian ] 09-06-2009 se agregaron cast para salvar warning
            If ImgViewer.PicBox2.isTif = True Then
                ImgViewer.PicBox2.zoomWidth = CSng(ImgViewer.PicBox2.Width / ImgViewer.PicBox2.Image.Width * 2)
                ImgViewer.PicBox2.zoomHeight = ImgViewer.PicBox2.zoomWidth
                ImgViewer.Refresh()
            Else
                ImgViewer.PicBox2.zoomWidth = CSng(ImgViewer.PicBox2.Width / ImgViewer.PicBox2.Image.Width)
                ImgViewer.PicBox2.zoomHeight = ImgViewer.PicBox2.zoomWidth
                ImgViewer.Refresh()
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    'Adapta el alto de la imagen
    Public Sub AltoPantalla()
        Try
            'Dim Size As System.Drawing.Size
            'Result.Picture.Resolution = ZPicture.AdjustImageToScreenHeight(ImgViewer.Height - 4, Result.Picture.Resolution, Result.Picture.Size.Height)
            'size = ZPicture.adjustImage(Result.Picture.Resolution, Result.Picture.Size.Height, Result.Picture.ResV, Result.Picture.Size.Width, Result.Picture.ResH)
            'ImgViewer.PicBox.Size = size
            'Result.Picture.Size = size
            'Result.Picture.ResH = Result.Picture.Resolution
            'Result.Picture.ResV = Result.Picture.Resolution
            ''ImgViewer.PicBox.Refresh()
            'Me.Zoom = Result.Picture.Resolution
            'Me.ImgViewer.ZoomNetron()
            If ImgViewer.PicBox2.isTif = True Then
                ImgViewer.PicBox2.zoomHeight = CSng(ImgViewer.PicBox2.Height / ImgViewer.PicBox2.Image.Height * 2)
                ImgViewer.PicBox2.zoomWidth = ImgViewer.PicBox2.zoomHeight
                ImgViewer.Refresh()
            Else
                ImgViewer.PicBox2.zoomHeight = CSng(ImgViewer.PicBox2.Height / ImgViewer.PicBox2.Image.Height)
                ImgViewer.PicBox2.zoomWidth = ImgViewer.PicBox2.zoomHeight
                ImgViewer.Refresh()
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#End Region

#Region "DELETE Result"
    Private Sub MenuItem4_Click(ByVal sender As System.Object, ByVal e As EventArgs)
        Try
            '		DeleteDocument()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

    Private Sub FuiMovido(ByVal Top As Int32, ByVal Left As Int32)
        RaiseEvent Movido(Top, Left)
    End Sub

#Region "Toolbar"

    'Private Sub SubCambiarDock()
    '    Select Case EstadoPantalla
    '        Case EstadosPantalla.Contraido
    '            Me.EstadoPantalla = EstadosPantalla.Expandido
    '            RaiseEvent CambiarDock(Me, True)
    '        Case EstadosPantalla.Expandido
    '            Me.EstadoPantalla = EstadosPantalla.Contraido
    '            RaiseEvent CambiarDock(Me, False)
    '    End Select
    'End Sub

    Private Sub MeCerre()
        'Me.ImgViewer.PicBox.Top = 0
        'Me.ImgViewer.PicBox.Left = 0
        ' RaiseEvent ShowingMegaView(False)
    End Sub

    Private Sub MeMovi(ByVal DiferenciaTop As Int32, ByVal DiferenciaLeft As Int32)
        Try
            'ValorPixelesLeft = Me.ImgViewer.PicBox.Width / 128
            'ValorPixelesTop = Me.ImgViewer.PicBox.Height / 182

            'Me.ImgViewer.PicBox.Top = Me.ImgViewer.PicBox.Top - DiferenciaTop * ValorPixelesTop
            'Me.ImgViewer.PicBox.Left = Me.ImgViewer.PicBox.Left - DiferenciaLeft * ValorPixelesLeft
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    'Public Sub StateBirdMegaView(ByVal ZoomLock As Boolean, ByVal ShowingBirdMegaView As Boolean, ByVal Top As Int32, ByVal Left As Int32)
    '    If ShowingBirdMegaView = True Then
    '        Me.ShowBirdMegaView()
    '    End If
    '    If ZoomLock = True Then
    '        Me.ZoomLock = False
    '        'Me.ImgViewer.BtnLockZoom.Pushed = True

    '        If ShowingBirdMegaView Then
    '            Me.BirdMegaView.ChangeLeft(Left)
    '            Me.BirdMegaView.ChangeTop(Top)
    '        End If
    '        RaiseEvent EventZoomLock(False)
    '    End If

    'End Sub

#End Region

#Region "ToolBar Actions"
    ''' <summary>
    ''' Modifica la visibilidad de los botones de reemplazar el documento
    ''' </summary>
    ''' <param name="visible"></param>
    ''' <remarks></remarks>
    Public Sub ChangeReplaceButtonVisibility(ByVal visible As Boolean)
        btnReplace.Visible = visible
        BtnReplaceDocument.Visible = visible
    End Sub

    Private Sub ReplDocument(ByRef Result As Result)
        Dim Dialog As New System.Windows.Forms.OpenFileDialog
        Try
            Dialog.CheckFileExists = True
            Dialog.CheckPathExists = True
            Dialog.Multiselect = False
            Dialog.Title = "Reemplazo de Documentos"
            Dialog.ValidateNames = True
            Dialog.Filter = "Archivos de imagen  (*.BMP;*.JPG;*.GIF;*.TIF;*.TIFF;*.PCX)|*.BMP;*.JPG;*.GIF;*.TIF;*.TIFF;*.PCX"
            Dim DialogResult As DialogResult = Dialog.ShowDialog()
            If DialogResult = DialogResult.OK OrElse DialogResult = DialogResult.Yes Then
                If Not Results_Business.IsImage(Dialog.FileName.Substring(Dialog.FileName.Length - 3, 3)) Then Exit Sub
                'ImgViewer.PicBox.Image = Nothing
                ImgViewer.PicBox2.Image = Nothing
                Result.Picture.Image = Nothing
                Results_Business.ReplaceFile(Result, Dialog.FileName)
                RefreshImage(Result)
                ImgViewer.Refresh()
            Else
                Exit Sub
            End If
            UserBusiness.Rights.SaveAction(Result.ID, ObjectTypes.Documents, RightsType.Edit, Result.Name)
        Catch ex As IOException
            Zamba.Core.ZClass.raiseerror(ex)
            MessageBox.Show("Ocurrio un error al reemplazar el documento actual: " & ex.ToString, "Error de Reemplazo", MessageBoxButtons.OK, MessageBoxIcon.Error)
            RollBackReplace(Result)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            RollBackReplace(Result)
        End Try
    End Sub
    Private Sub RefreshImage(ByRef Result As Result)
        Try
            Result.Picture.Image = Image.FromFile(Result.FullPath)

            'Mutitiff
            Try
                iCount = 0
                actualFrame = 0
                oFDimension = New System.Drawing.Imaging.FrameDimension(Result.Picture.Image.FrameDimensionsList(actualFrame))
                iCount = Result.Picture.Image.GetFrameCount(oFDimension) - 1
                If iCount > 0 Then
                    Try
                        ' DropDownMenuItem1.Text = "NOTAS"
                        ButtonItem16.Visible = True
                        ButtonItem17.Visible = True
                        ButtonItem18.Visible = True
                        ButtonItem19.Visible = True
                        'Me.ImgViewer.Panel2.Width = 721
                        txtNumPag.Visible = False
                        ButtonItem20.Visible = False
                        lblPagin.Text = "0 DE 0"
                        lblPagin.Visible = False
                    Catch ex As Exception
                        Zamba.Core.ZClass.raiseerror(ex)
                    End Try
                    Result.Picture.Image.SelectActiveFrame(oFDimension, actualFrame)
                    LoadGoToPage()
                Else
                    Try
                        '   DropDownMenuItem1.Text = "NOTAS"
                        ButtonItem16.Visible = False
                        ButtonItem17.Visible = False
                        ButtonItem18.Visible = False
                        ButtonItem19.Visible = False
                        'Me.ImgViewer.Panel2.Width = 577
                        txtNumPag.Visible = False
                        ButtonItem20.Visible = False
                        lblPagin.Text = "0 DE 0"
                        lblPagin.Visible = False
                    Catch ex As Exception
                        Zamba.Core.ZClass.raiseerror(ex)
                    End Try
                    oFDimension = Nothing
                End If
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try

            'Ajuste de imagen inicial
            Dim size As New Drawing.Size
            size = Result.Picture.Size

            Try
                ImgViewer.PicBox2.Image = Result.Picture.Image
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try

            Try
                _rotation = 0
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub RollBackReplace(ByRef Result As Result)
        Try
            Result.Picture.Image = Image.FromFile(Result.FullPath)
            'ImgViewer.PicBox.Image = result.Picture.Image
            ImgViewer.PicBox2.Image = Result.Picture.Image
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub


    'Public Sub SDH()
    '    If IsNothing(Me.Result) = False Then ShowDocumentHistory(Me.Result)
    'End Sub


    'Public Sub PrintDocument()
    '    If IsNothing(Result) = True Then Exit Sub
    '    If RightFactory.GetUserRights(ObjectTypes.Documents, Zamba.Core.RightsType.Print) Then
    '        If Result.IsImage Then
    '            Dim Printer As New ZPrinter
    '            RemoveHandler Printer.Printed, AddressOf PrintedPages
    '            AddHandler Printer.Printed, AddressOf PrintedPages
    '            If Zamba.Tools.EnvironmentUtil.getWindowsVersion = Tools.EnvironmentUtil.Windows.WindowsXp Then
    '                Printer.printWia(Result)
    '            Else
    '                Printer.PrintDocument(Result, ZPrinting.PrintConfig, ZPrinting.PrintConfig.DefaultPageSettings)
    '            End If
    '        Else
    '            If Results_Factory.GetExtensionId(Result.FullPath) = Results_Factory.Extensiones.TXT Then
    '                PrintTXT()
    '            Else
    '                EnviarKeys()
    '            End If
    '        End If
    '        RightFactory.SaveAction(Result.Id, ObjectTypes.Documents, Zamba.Core.RightsType.Print, Result.Name & " Hojas Impresas: " & Me.PagesCount)
    '    End If
    'End Sub
    'Dim PagesCount As Int32
    'Private Sub PrintedPages(ByVal count As Int64)
    '    PagesCount = count
    'End Sub



    'Private Shared Sub EnviarKeys()
    '    SendKeys.Send("^(p)")
    'End Sub
    'Public Sub PrintTXT()
    '    Try
    '        Dim owb As WebBrowser = Me.ToolStripContainer1.ContentPanel.Controls(0)
    '        owb.PrintTxt()
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '    End Try
    'End Sub
#End Region

#Region "OfficeDocuments"
    Public Sub ShowToolbars()
        OffWB.ShowToolBars(True)
    End Sub
#End Region



#Region "PrintDocument"
    Public Sub PrintDocumentWB()
        Try
            Dim results As List(Of IPrintable)

            Dim fullPath As String = String.Empty
            If Not IsNothing(Result.FullPath) Then
                fullPath = Result.FullPath.ToString()
            End If

            If Result.ISVIRTUAL OrElse Results_Business.HasForms(Result) OrElse fullPath.EndsWith(".html") Then
                If IsNothing(FormBrowser) Then
                    FormBrowser = New FormBrowser()
                End If
                FormBrowser.Print(Result.FullPath)
                results = New List(Of IPrintable)
                results.Add(Result)
                Dim Zp As New frmchooseprintmode(results, LoadAction.ShowForm)
                Zp.ShowDialog()
            Else
                results = New List(Of IPrintable)
                results.Add(Result)
                Dim Zp As New frmchooseprintmode(results, LoadAction.ShowForm)
                Zp.ShowDialog()
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Sub PrintPreviewDocument()
        Try
            Dim results As New List(Of IPrintable)
            results.Add(Result)
            Using frmPrinter As New frmchooseprintmode(results, LoadAction.ShowPreview)
                RemoveHandler frmPrinter.PrintVirtual, AddressOf PrintVirtualDocument
                AddHandler frmPrinter.PrintVirtual, AddressOf PrintVirtualDocument
                frmPrinter.ShowDialog()
                RemoveHandler frmPrinter.PrintVirtual, AddressOf PrintVirtualDocument
            End Using
        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("Ha ocurrido un error inesperado", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub PrintVirtualDocument(ByVal r As IResult)
        Try
            PrintDocumentWB()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

#End Region

#Region "SaveDocument"
    Public Sub SaveDocument()
        If Result.CurrentFormID <= 0 Then
            If Not IsNothing(OffWB) Then OffWB.SaveDocument()
            If Not IsNothing(FormBrowser) Then FormBrowser.SaveDocument()
            If Not IsNothing(ExcelOffWB) Then SaveModifiedDocument()
            If Not IsNothing(WordOffWB) Then SaveModifiedDocument()
        End If
    End Sub


    ''' <summary>
    ''' Copia el documento al servidor si el mismo fue modificado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveModifiedDocument()
        Dim fi As FileInfo = Nothing
        Dim fa As FileInfo = Nothing

        Try
            If Not String.IsNullOrEmpty(_file) Then
                fi = New FileInfo(_file)

                If fi.LastWriteTime > docOpenedDate Then
                    Dim docWasSaved As Boolean = False

                    If _result.DocType.IsReadOnly = False Then
                        If flagAutomaticNewVersion = True AndAlso _result.IsOffice = True Then
                        Else
                            If _result.Disk_Group_Id <> 0 Then
                                Dim ds As DataSet = VolumesBusiness.GetActiveDiskGroupVolumes(_result.Disk_Group_Id)

                                If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 AndAlso ZCore.filterVolumes(ds.Tables(0).Rows(0)("DISK_VOLUME_ID")).Type = VolumeTypes.DataBase Then
                                    _result.EncodedFile = FileEncode.Encode(_file)
                                    Results_Business.UpdateDOCB(_result)
                                    docWasSaved = True
                                Else
                                    fa = New FileInfo(_result.FullPath)
                                    ' en caso de que no use version automatica y se deba guardar
                                    fa.Attributes = IO.FileAttributes.Archive
                                    If fi.Exists Then
                                        Dim resb As New ResultBusinessExt()
                                        'Si el documento está encriptado, entonces lo encripto y lo copio, sino lo copio normal
                                        If resb.IsDocumentEncrypted(_result.ID, _result.DocTypeId) Then
                                            resb.EncryptAndCopy(_result, _file)
                                            docWasSaved = True
                                        Else
                                            fi.CopyTo(_result.FullPath, True)
                                            docWasSaved = True
                                        End If
                                    End If
                                End If
                            End If

                        End If
                    End If

                    If docWasSaved Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo si el documento es una tarea")
                        Dim TaskResult As TaskResult = WFTaskBusiness.GetTaskByDocIdAndWorkFlowId(Result.ID, 0)
                        If Not IsNothing(TaskResult) Then
                            Dim WFTaskBusiness As New WFTaskBusiness()
                            WFTaskBusiness.ExecuteEventRules(TaskResult, True, TypesofRules.GuardarDocumento)
                            WFTaskBusiness = Nothing
                            TaskResult.Dispose()
                            TaskResult = Nothing
                        End If
                    End If
                End If
            End If
        Catch ex As IO.IOException
            'Esta exception no se esta tirando porque actualmente la tira siempre que se abre un word
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            fa = Nothing
            fi = Nothing
        End Try
    End Sub
    Private Sub SaveAs()
        If Not IsNothing(OffWB) Then OffWB.SaveAsDocument()
        If Not IsNothing(FormBrowser) Then FormBrowser.SaveDocument()
        'If Me.WordOffWB isnot Nothing then Me.WordOffWB.
    End Sub
    'Private Sub MenuItem1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItem1.Click
    '    SaveAs()
    '    Me.SaveDocument()
    'End Sub
#End Region

#Region "Foro"

    Private Sub btnMostrarForo_Click(ByVal sender As System.Object, ByVal e As EventArgs)
        RaiseEvent MostrarForo(Result)
    End Sub
#End Region

#Region "Control Injection Container"
    Private ViewerContainer As IViewerContainer

#End Region

#Region "Split"
    Private IsSplited As Boolean
    Private Sub SplitDocumentViewer()
        ViewerContainer.Split(Me, IsSplited)
    End Sub

#End Region





    'Public Event eSendMail(ByVal Task As ITaskResult)
    ''' <summary>
    ''' [sebastian 31-03-2009] modified It was modified to send virtuals forms by mail
    ''' </summary>
    ''' <param name="results"></param>
    ''' <remarks></remarks>

    Public Sub SendMail(ByVal Task As Result) 'FORM QUE PERMITE MANDAR UN MAIL
        If Task.ISVIRTUAL Then
            SaveHtmlFile()
        End If
        If Not Result Is Nothing Then ZClass.HandleModule(ResultActions.EnvioDeMail, Result, New Hashtable)
    End Sub


    Public Sub AgregarACarpeta(ByVal Result As Result)
        Try
            Result.DocType.IsReindex = True
            ZClass.HandleModule(ResultActions.InsertarCarpetaLoadIndexerDelegado, Result)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ShowVersionComment(ByVal result As Result)
        Dim collection As New System.Collections.Generic.List(Of String)
        Dim comment As String
        Dim commentdate As String

        comment = Results_Business.GetVersionComment(result.ID)
        commentdate = Results_Business.GetVersionCommentDate(result.ID)
        If String.IsNullOrEmpty(commentdate) Then
            commentdate = "No se registran datos de este documento"
            comment = String.Empty
        End If

        Dim PublishResult As New PublishableResult
        PublishResult = Results_Business.PublishableResult(result)
        Dim showversion As New frmShowVersionComment(PublishResult, result, comment, commentdate)
        showversion.ShowDialog()
    End Sub

    Public Sub Imprimir(ByVal results As List(Of IResult), ByVal loadAction As Print.LoadAction)
        Try
            If (results IsNot Nothing) AndAlso results.Count > 0 Then
                Dim r As Object = results
                Dim Zp As New Zamba.Print.frmchooseprintmode(TryCast(r, List(Of IPrintable)), loadAction)
                If (Zp.ShowDialog = DialogResult.OK) Then
                    UserBusiness.Rights.SaveAction(results(0).ID, ObjectTypes.Documents, RightsType.Print, "Se imprimio tarea con id: " & results(0).ID & "desde la grilla")
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub GuardarComo(ByRef _result As Result)
        Try
            If RightsBusiness.GetUserRights(ObjectTypes.Documents, RightsType.Saveas) = False Then
                MessageBox.Show("Usted no tiene permiso para guardar los Documentos", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                Exit Sub
            End If
            Dim SaveFileDialog1 As New SaveFileDialog
            SaveFileDialog1.InitialDirectory = Application.StartupPath & "\"

            Dim str As String
            If (Not String.IsNullOrEmpty(_result.OriginalName)) AndAlso (Not IsNumeric(Path.GetFileNameWithoutExtension(_result.OriginalName))) Then
                str = Path.GetFileName(_result.OriginalName)
            Else
                str = _result.Name
            End If
            'frm.SelectedResult.Name()

            Dim file As FileInfo
            If Not Result.ISVIRTUAL Then
                file = New FileInfo(Result.FullPath())
            Else
                Result.Html = GetHtml()
                Result.HtmlFile = Tools.EnvironmentUtil.GetTempDir("\OfficeTemp").FullName & "\" & Result.Name & "-temp" & (New Random).Next(1, 9999).ToString & ".html"

                Try
                    If System.IO.File.Exists(Result.HtmlFile) Then
                        System.IO.File.Delete(Result.HtmlFile)
                    End If
                Catch ex As Exception

                End Try

                Try
                    Using write As New StreamWriter(Result.HtmlFile.Substring(0, Result.HtmlFile.Length - 4) & "mht")
                        write.AutoFlush = True
                        Dim reader As New StreamReader(FormBusiness.GetShowAndEditForms(Result.DocTypeId)(0).Path.Replace(".html", ".mht"))
                        Dim mhtstring As String = reader.ReadToEnd()
                        write.Write(mhtstring.Replace("<Zamba.Html>", Result.Html))
                    End Using
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
                Try
                    System.IO.File.Delete(Result.HtmlFile)
                Catch ex As Exception

                End Try

                Result.HtmlFile = Result.HtmlFile.Substring(0, Result.HtmlFile.Length - 4) & "mht"

                file = New FileInfo(Result.HtmlFile)

            End If

            'reemplazo los caracteres invalidos
            str = str.Replace("\", Chr(32))
            str = str.Replace("/", Chr(32))
            str = str.Replace(Chr(58), Chr(32)) ' :
            str = str.Replace("*", Chr(32))
            str = str.Replace("?", Chr(32))
            str = str.Replace(Chr(34), Chr(32)) ' "
            str = str.Replace("<", Chr(32))
            str = str.Replace(">", Chr(32))
            str = str.Replace("|", Chr(32))

            Dim ext As String = file.Extension
            If (Not String.IsNullOrEmpty(_result.OriginalName)) AndAlso (Not IsNumeric(Path.GetFileNameWithoutExtension(_result.OriginalName))) Then
                SaveFileDialog1.FileName = str
            Else
                SaveFileDialog1.FileName = str + ext
            End If

            SaveFileDialog1.Filter = "Todos los archivos(*" & ext & ")|*" & ext
            SaveFileDialog1.FilterIndex = 1
            SaveFileDialog1.ValidateNames = True
            If SaveFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                file.CopyTo(SaveFileDialog1.FileName, True)
                ' Se actualiza el U_TIME del usuario en la tabla UCM (fecha y hora en que se realizo la última acción), así como guardar en la
                ' tabla USER_HST la acción realizada por el usuario. En este caso, se registra la acción tras guardar el documento seleccionado
                ' en la máquina local del usuario
                UserBusiness.Rights.SaveAction(_result.ID, ObjectTypes.Documents, RightsType.Saveas, "Se guardo documento desde la grilla: " & _result.Name)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub LoadResultRights(ByVal Result As Result)
        If ToolBar1 IsNot Nothing Then
            'GUARDAR
            If UserBusiness.Rights.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.Documents, RightsType.Saveas) = True Then
                btnSaveAs.Visible = True
                ToolStripSeparator7.Visible = True
            Else
                btnSaveAs.Visible = False
                ToolStripSeparator7.Visible = False
            End If

            'ENVIAR POR EMAIL
            If UserBusiness.Rights.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.RemoveSendMailInTasks, Result.DocTypeId) = False AndAlso UserBusiness.Rights.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.Documents, RightsType.EnviarPorMail) = True Then
                btnAdjuntarEmail.Visible = True
                ToolStripSeparator8.Visible = True
            Else
                btnAdjuntarEmail.Visible = False
                ToolStripSeparator8.Visible = False
            End If


            'REEMPLAZAR DOCUMENTO
            If UserBusiness.Rights.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.Edit, Result.DocTypeId) Then
                InfoView.Text = String.Empty
                btnReplace.Visible = True
                BtnReplaceDocument.Visible = True
            Else
                If Not Result.ISVIRTUAL Then
                    'De no ser un formulario, muestra un mensaje de documento de solo lectura
                    InfoView.Text = MESSAGE_ONLY_READ
                End If
                btnReplace.Visible = False
                BtnReplaceDocument.Visible = False
            End If


            If Result.IsPDF Then
                'todo: hay que ver porque con el boton de imprimir de zamba el PDF sale en blanco, desde windows no pasa.
                btnPrint.Visible = False
                btnPrintPreview.Visible = False
            End If

            btnGotoWorkflow.Visible = False
            btnVerVersionesDelDocumento.Visible = False
            btnChangePosition.Visible = False
            btnAgregarUnaNuevaVersionDelDocuemto.Visible = False
            ToolStripSeparator13.Visible = False
            ToolStripSeparator12.Visible = False

            'Visibilidad de la marca de favoritos o importancia
            btnFlagAsFavorite.Visible = True
            btnFlagAsImportant.Visible = True
            separateDocumentLabels.Visible = True

            Dim WP As New BackgroundWorker()
            AddHandler WP.DoWork, AddressOf WPDoWork
            WP.RunWorkerAsync(Result)
        End If
    End Sub

    Private Sub WPDoWork(sender As Object, e As DoWorkEventArgs)
        Dim currentResult As IResult = e.Argument
        FillResultsLabels(currentResult)
    End Sub

    Private Sub FillResultsLabels(ByRef Result As IResult)
        DocumentLabelsBusiness.FillResultLabels(Result)
        ChangeFavoriteButtonImage()
        ChangeImportanceButtonImage()

    End Sub

    ''' <summary>
    ''' Se encarga de manejar los eventos click de la toolbar de documentos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    '''<history> [Marcelo]  02/05/2008  Modified</history>
    Private Overloads Sub ToolBar11_ButtonClick(ByVal sender As System.Object, ByVal e As ToolStripItemClickedEventArgs) Handles ToolBar1.ItemClicked
        Try
            Select Case CStr(e.ClickedItem.Tag)
                Case "GUARDAR"
                    SaveAs()
                Case "IMPRIMIR"
                    PrintDocumentWB()
                Case "PREVISUALIZAR"
                    PrintPreviewDocument()
                Case "PANTALLACOMPLETA"
                    IsMaximize = Not (IsMaximize)
                    If IsMaximize Then
                        RaiseEvent CambiarDock(Me, False, Not IsMaximize)
                    End If
                Case "DIVIDIR"
                    If IsSplited = False Then
                        e.ClickedItem.Text = "Restaurar"
                        e.ClickedItem.ToolTipText = "Restaurar"
                    Else
                        e.ClickedItem.Text = "Dividir"
                        e.ClickedItem.ToolTipText = "Dividir"
                    End If
                    IsSplited = Not (IsSplited)
                    SplitDocumentViewer()
                Case "MOSTRAR"
                    If Not IsNothing(OffWB) Then OffWB.ShowToolbars()
                Case "Cerrar"
                    CloseDocument()
                Case "VERDOCUMENTAL"
                    'LLama al metodo para mostrarlo en la grilla de resultados
                    Dim docId(0) As String
                    docId.SetValue(Result.ID.ToString(), 0)
                    ModDocuments.DoSearch(Result.DocTypeId, docId, False)
                Case "VERORIGINAL"
                    'Al hacer click en ver original muestra el documento asociado
                    RaiseEvent ShowOriginal(Result)
                Case "EMAIL"
                    SendMail(Result)
                Case "VERSIONESDELDOCUMENTO"
                    ShowVersionComment(Result)
                Case "GUARDARDOCUMENTOCOMO"
                    Try
                        GuardarComo(DirectCast(Result, TaskResult))
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                Case "AGREGARACARPETA"
                    If Result.GetType.ToString = "Zamba.Core.TaskResult" Then
                        AgregarACarpeta(DirectCast(Result, TaskResult))
                    Else
                        AgregarACarpeta(Result)
                    End If
                Case "BtnReplace"
                    Try
                        RaiseEvent ReplaceDocument(Result)
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                Case "GOTOWF"
                    Try
                        RaiseEvent ShowAssociatedWFbyDocId(Convert.ToInt64(Result.ID.ToString()), Result.DocTypeId)
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                Case "FAVORITO"
                    UpdateFavoriteFlag()
                Case "IMPORTANTE"
                    UpdateImportantFlag()
                Case "decrypt"
                    DecryptRequested()

            End Select
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub UpdateImportantFlag()
        'Modifica la importancia del result
        Result.IsImportant = Not Result.IsImportant

        Try
            'Actualiza el documento
            If Result IsNot Nothing Then
                DocumentLabelsBusiness.UpdateImportanceLabel(Result)
            End If

            ChangeImportanceButtonImage()
        Catch ex As Exception
            ZClass.raiseerror(ex)

            'Vuelve atras el cambio
            Result.IsImportant = Not Result.IsImportant

            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub UpdateFavoriteFlag()
        'Modifica la marca de favorito del result
        Result.IsFavorite = Not Result.IsFavorite

        Try
            'Actualiza el documento
            If Result IsNot Nothing Then
                DocumentLabelsBusiness.UpdateFavoriteLabel(Result)
            End If

            ChangeFavoriteButtonImage()
        Catch ex As Exception
            ZClass.raiseerror(ex)

            'Vuelve atras el cambio
            Result.IsFavorite = Not Result.IsFavorite

            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ChangeFavoriteButtonImage()
        Try
            If ToolBar1 IsNot Nothing AndAlso ToolBar1.IsDisposed = False AndAlso btnFlagAsFavorite IsNot Nothing AndAlso btnFlagAsImportant IsNot Nothing Then
                If Result.IsFavorite Then
                    btnFlagAsFavorite.Image = Global.Zamba.Viewers.My.Resources.appbar_cards_heart
                    btnFlagAsImportant.ToolTipText = "Quitar marca de Favorito"
                Else
                    btnFlagAsFavorite.Image = Global.Zamba.Viewers.My.Resources.appbar_heart_outline
                    btnFlagAsImportant.ToolTipText = "Marcar como Favorito"
                End If
            End If
        Catch ex As Exception
            'ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ChangeImportanceButtonImage()
        Try
            If ToolBar1 IsNot Nothing Then
                If Result.IsImportant Then
                    btnFlagAsImportant.Image = Global.Zamba.Viewers.My.Resources.appbar_location
                    btnFlagAsImportant.ToolTipText = "Quitar marca de Importante"
                Else
                    btnFlagAsImportant.Image = Global.Zamba.Viewers.My.Resources.appbar_location_delete
                    btnFlagAsImportant.ToolTipText = "Marcar como Importante"
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub CloseTab()
        ZTrace.WriteLineIf(ZTrace.IsInfo, "'Cerrar' Button was pressed")

        If Not (Parent.Parent.GetType Is GetType(Zamba.Viewers.ExternalVisualizer)) _
        AndAlso String.Compare(ViewerContainer.Name, "UCTaskViewer") = 0 _
        AndAlso GetTaskTabsCount() = 1 Then
            RaiseEvent CloseOpenTask(True)
        Else
            'Si la tarea proviene de una asociada se refresca dicha tarea
            If isAsocTask Then RaiseEvent RefreshAsocTask()
            CloseDocument(Result)
            If IsMaximize Then
                RaiseEvent CambiarDock(Me, False, True)
            End If
        End If
    End Sub
    Private Function GetTaskTabsCount() As Int32
        Dim count As Int32 = 0
        'For Each taskTab As TabPage In DirectCast(Me.Parent, TabControl).TabPages
        'Next
        Dim parentTab As TabControl = DirectCast(Parent, TabControl)
        Dim tipoUCDocumentViewer2 As Type = GetType(UCDocumentViewer2)
        For i As Int32 = 0 To parentTab.TabCount - 1
            If parentTab.TabPages(i).GetType Is tipoUCDocumentViewer2 Then
                count += 1
            End If
        Next
        parentTab = Nothing
        tipoUCDocumentViewer2 = Nothing
        Return count
    End Function


    Public Sub CloseMsgDocument(Optional ByVal isOpenned As Boolean = False)

        If winHandle <> System.IntPtr.Zero Then

            If OutOfZamba = False AndAlso isOpenned = False Then
                RestoreMsgPanelAttributes()
                ShowWindow(winHandle, SW_HIDE)
                System.Threading.Thread.Sleep(250)
                SetParent(winHandle, oldParent)
                'System.Threading.Thread.Sleep(250)
            End If

            'WindowsApi.Usr32Api.SendMessage(winHandle, WindowsApi.Usr32Api.WM_CLOSE, 0, 0)
            winHandle = IntPtr.Zero
            'System.Threading.Thread.Sleep(1000)
            ZTrace.WriteLineIf(ZTrace.IsInfo, " Se cerro la ventana")
            ZTrace.WriteLineIf(ZTrace.IsInfo, " -------------MsgDocumentViewer CERRADO-------------- " & Date.Now.ToString())
            ZTrace.WriteLineIf(ZTrace.IsInfo, " ---------------------------------------------------- ")
        End If

    End Sub

    Public Sub HideButtons()
        GuardarComoButtonVisible = False
        PrintImgButtonVisible = False
        PrintDocButtonVisible = False
    End Sub
    Public Function traerDoc() As Object
        Return OffWB.TraerDoc
    End Function
    Public Sub CloseDocument(ByVal Result As Object)
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Calling ClearUCBrowser()")
            ClearUCBrowser()
        Catch
        End Try

        Dim parentTab As TabControl = DirectCast(Parent, TabControl)
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Removing Tab")
            Dim index As Integer = parentTab.SelectedIndex()
            'Si la solapa seleccionada es la última, se reordena la anteúltima 
            'solapa al final. Si esto no se hace me devuelve a la solapa de
            'foro automáticamente.
            'If parentTab.SelectedIndex = parentTab.TabCount - 1 Then
            '    'Dim fakeTabPage As TabPage = parentTab.TabPages(parentTab.TabCount - 1)
            '    'parentTab.SelectTab(index - 1)
            '    'parentTab.TabPages.Remove(fakeTabPage)
            '    'parentTab.TabPages.Add(fakeTabPage)
            '    parentTab.TabPages.Remove(Me)
            '    'fakeTabPage = Nothing
            '    parentTab.SelectTab(index - 1)
            'Else
            '    parentTab.TabPages.Remove(Me)
            '    parentTab.SelectTab(index - 1)
            '    'parentTab.SelectTab(parentTab.TabCount - 1)
            'End If
            parentTab.TabPages.Remove(Me)
            parentTab.SelectTab(index - 1)
            Dispose()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            'NO APLICAR DISPOSE SOBRE ESTE OBJETO
            parentTab = Nothing
        End Try
    End Sub

    Private Sub ZControl_Close(ByVal Sender As Object, ByVal e As EventArgs)
        Try
            If String.Compare(Sender.GetType().Name, "Button") = 0 Then
                If Not DirectCast(Sender, Button).Parent Is Nothing Then
                    DirectCast(Sender, Button).Parent.Width = 0
                End If
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub ZControl_PanelSizeChanged(ByVal Sender As Object, ByVal e As EventArgs)
        Try
            DirectCast(Sender, Panel).Controls(0).Top = -1
            DirectCast(Sender, Panel).Controls(0).Left = DirectCast(Sender, Panel).Width - DirectCast(Sender, Panel).Controls(0).Width + 1
        Catch ex As Exception
        End Try
    End Sub
    Private Sub ShowZControl_PanelLinks(ByVal Panel As Panel, ByVal Name As String)
        Try
            If IsNothing(ZControl_PanelLinks) Then
                ZControl_PanelLinks = New Panel
                ZControl_PanelLinks.BackColor = Color.White
                ZControl_PanelLinks.Dock = DockStyle.Bottom
                ZControl_PanelLinks.Height = 25
                ZControl_PanelLinks.BorderStyle = BorderStyle.FixedSingle
                ToolStripContainer1.ContentPanel.Controls.Add(ZControl_PanelLinks)
                '                ZControl_PanelLinks.BringToFront()
                ZControl_PanelLinks.Focus()
            End If
            Dim MaxLeft As Int32 = 0
            For Each Control As Control In ZControl_PanelLinks.Controls
                If Control.Left + Control.Width > MaxLeft Then MaxLeft = Control.Left + Control.Width
            Next
            MaxLeft += 20
            Dim L As New LinkLabel
            L.Text = Name.ToUpper
            L.ForeColor = Color.Blue
            RemoveHandler L.Click, AddressOf ShowIndexPanel
            AddHandler L.Click, AddressOf ShowIndexPanel
            L.AutoSize = True
            L.Top = 5
            L.Left = MaxLeft
            L.Tag = Panel
            ZControl_PanelLinks.Controls.Add(L)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Event MostrarPosEvent()

    ''' <summary>
    ''' [Sebastian] 17-06-2009 Modified se realizo cast para salvar warning
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ShowIndexPanel(ByVal sender As Object, ByVal e As EventArgs)
        Try

            DirectCast(_ucIndexs, UCIndexViewer).ShowIndexs(Result.ID, Result.DocTypeId, False)
            DirectCast(DirectCast(sender, LinkLabel).Tag, Panel).Width = 400
            RaiseEvent MostrarPosEvent()
            'ZControl_Splitter.Visible = True
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub ZControl_Show(ByVal MyZControl As ZControl, ByVal Posicion As Posiciones, ByVal Name As String, ByVal Visible As Boolean)
        Try
            If IsNothing(MyZControl) Then
                _ucIndexs = Nothing
                Exit Sub
            End If
            '[Ezequiel] - Si el tipo de zcontrol es UnIndexViewer entonces los igualo a la variable local
            If TypeOf MyZControl Is UCIndexViewer Then
                _ucIndexs = MyZControl
                '[Ezequiel] - En caso del que formbrowser ya este instanciado le paso el control por el metodo AssociateIndexViewer
                If Not FormBrowser Is Nothing Then
                    FormBrowser.AssociateIndexViewer(_ucIndexs)
                End If

            End If

            If Visible OrElse UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.EditAttributesInForms, Result.DocTypeId) Then

                If Not ZControl_Panels.ContainsKey(Name) Then
                    Dim B As New Button
                    B.FlatStyle = FlatStyle.Flat
                    B.BackColor = Color.White
                    B.Font = New Font(B.Font.FontFamily, 7, FontStyle.Regular, GraphicsUnit.Pixel)
                    B.Text = "X"
                    B.Height = 20
                    B.Width = 25
                    RemoveHandler B.Click, AddressOf ZControl_Close
                    AddHandler B.Click, AddressOf ZControl_Close

                    P = New Panel
                    P.Width = 250
                    P.BorderStyle = BorderStyle.FixedSingle
                    P.Controls.Add(MyZControl)
                    MyZControl.Dock = DockStyle.Fill
                    P.Controls.Add(B)
                    ZControl_Panels.Add(Name, P)

                    RemoveHandler P.SizeChanged, AddressOf ZControl_PanelSizeChanged
                    AddHandler P.SizeChanged, AddressOf ZControl_PanelSizeChanged


                    Dim ZControl_Splitter As New Splitter
                    ZControl_Splitter.BackColor = Color.Gray

                    If Not String.IsNullOrEmpty(Name) Then ShowZControl_PanelLinks(P, Name)

                    Dim ViewerControl As UserControl
                    If ToolStripContainer1.ContentPanel.Controls(0).GetType.Name = "UserControl" OrElse ToolStripContainer1.ContentPanel.Controls(0).GetType.Name = "WebBrowser" Then
                        ViewerControl = ToolStripContainer1.ContentPanel.Controls(0)
                    End If

                    Select Case Posicion
                        Case Posiciones.Abajo
                            P.Dock = DockStyle.Bottom
                            ToolStripContainer1.ContentPanel.Controls.Add(P)
                            P.BringToFront()
                            P.Focus()
                            ZControl_Splitter.Dock = DockStyle.Bottom
                            ZControl_Splitter.Height = 2
                            ToolStripContainer1.ContentPanel.Controls.Add(DirectCast(ZControl_Splitter, Control))
                            ZControl_Splitter.BringToFront()
                            BringToFront()
                            ToolStripContainer1.ContentPanel.Controls(0).BringToFront()
                        Case Posiciones.Arriba
                            P.Dock = DockStyle.Top
                            ToolStripContainer1.ContentPanel.Controls.Add(P)
                            P.BringToFront()
                            P.Focus()
                            ZControl_Splitter.Dock = DockStyle.Top
                            ZControl_Splitter.Height = 2
                            ToolStripContainer1.ContentPanel.Controls.Add(DirectCast(ZControl_Splitter, System.Windows.Forms.Control))
                            ZControl_Splitter.BringToFront()
                            BringToFront()
                            ToolStripContainer1.ContentPanel.Controls(0).BringToFront()
                        Case Posiciones.Derecha
                            P.Dock = DockStyle.Right
                            ToolStripContainer1.ContentPanel.Controls.Add(P)
                            P.BringToFront()
                            P.Focus()
                            ZControl_Splitter.Dock = DockStyle.Right
                            ZControl_Splitter.Width = 2
                            ToolStripContainer1.ContentPanel.Controls.Add(DirectCast(ZControl_Splitter, Control))
                            ZControl_Splitter.BringToFront()
                            BringToFront()
                            ToolStripContainer1.ContentPanel.Controls(0).BringToFront()
                        Case Posiciones.Izquierda
                            P.Dock = DockStyle.Left
                            ToolStripContainer1.ContentPanel.Controls.Add(P)
                            P.BringToFront()
                            P.Focus()
                            ZControl_Splitter.Dock = DockStyle.Left
                            ZControl_Splitter.Width = 2
                            ToolStripContainer1.ContentPanel.Controls.Add(DirectCast(ZControl_Splitter, Control))
                            ZControl_Splitter.BringToFront()
                            BringToFront()
                            ToolStripContainer1.ContentPanel.Controls(0).BringToFront()
                    End Select

                    B.Top = -1
                    B.Left = P.Width - B.Width + 1
                    B.BringToFront()
                    If Not ViewerControl Is Nothing Then ViewerControl.BringToFront()
                    If Visible = False Then ZControl_Close(B, New EventArgs)

                    'ivan 26/01/2016 - Vuelvo a traer FormBrowser al frente porque sino se muestra solapado por los indices.
                    If Not IsNothing(FormBrowser) Then
                        FormBrowser.BringToFront()
                    End If
                End If

            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Se encarga de la ejecucion de los botones de la barra de imagenes
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Marcelo]	02/05/2008	Modified
    ''' </history>
    Private Sub DesignSandBar_ButtonClick(ByVal Sender As Object, ByVal e As ToolStripItemClickedEventArgs) Handles DesignSandBar.ItemClicked
        Try
            Select Case CStr(e.ClickedItem.Tag)
                Case "IR"
                    If IsNumeric(txtNumPag.Text) = True Then
                        GotoPage(CInt(Val(txtNumPag.Text.Trim)))
                    Else
                        txtNumPag.Text = String.Empty
                    End If
                Case "Plus"
                    If Result.IsImage Then
                        Try
                            If ZoomLock = False Then
                                ZoomPlus()
                            End If
                        Catch ex As Exception
                            Zamba.Core.ZClass.raiseerror(ex)
                        End Try
                    End If
                Case "Minus"
                    If Result.IsImage Then
                        Try
                            If ZoomLock = False Then
                                ZoomMinus()
                            End If
                        Catch ex As Exception
                            Zamba.Core.ZClass.raiseerror(ex)
                        End Try
                    End If
                Case "btnTamNormal"
                    If Result.IsImage Then
                        TamanoNormal()
                    End If

                Case "BtnStrech"
                    If Result.IsImage Then
                        Try
                            AnchoPantalla()
                        Catch ex As Exception
                            Zamba.Core.ZClass.raiseerror(ex)
                        End Try
                    End If
                Case "btnAltoPantalla"
                    If Result.IsImage Then
                        Try
                            AltoPantalla()
                        Catch ex As Exception
                            Zamba.Core.ZClass.raiseerror(ex)
                        End Try
                    End If
                Case "Rotate_90"
                    If Result.IsImage Then
                        Rotate(1)
                    End If
                Case "Rotate90"
                    If Result.IsImage Then
                        Rotate(-1)
                    End If
                Case "BtnOCR"
                    Estado = Estados.OCR
                Case "BtnNetron"
                    Estado = Estados.Netron
                    ImgViewer.MenuAddNetron_Click(Me, New EventArgs)
                Case "BtnNota"
                    If Result.IsImage Then
                        Estado = Estados.Nota
                    End If
                Case "BtnFirma"
                    If Result.IsImage Then
                        If Not String.IsNullOrEmpty(UserBusiness.Rights.CurrentUser.firma) Then
                            Estado = Estados.Firma
                        Else
                            Estado = Estados.Ninguno
                            MessageBox.Show("Usted no posee imagen de firma", "Zamba Firma", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If
                    End If
                Case "BtnLockZoom"
                    If Result.IsImage Then
                        Try
                            If ZoomLock Then
                                ZoomLock = False
                                RaiseEvent EventZoomLock(False)
                            Else
                                ZoomLock = True
                                RaiseEvent EventZoomLock(True)
                            End If
                        Catch ex As Exception
                            Zamba.Core.ZClass.raiseerror(ex)
                        End Try
                    End If
                Case "btnfirstpage"
                    FirstPage()
                Case "btnBefore"
                    PreviusPage()
                Case "BtnNext"
                    NextPage()
                Case "btnlastpage"
                    LastPage()
                Case "IMPRIMIR"
                    PrintDocumentWB()
                Case "PREVISUALIZAR"

                Case "btnPreview"
                    '[Tomas] Se comenta porque no debería ocultarse o mostrarse el botón al maximizar
                    'If showPrint = True And IsIndexer = False Then
                    '    Me.BPrint.Visible = Not (Me.BPrint.Visible)
                    'End If
                    IsMaximize = Not (IsMaximize)
                    RaiseEvent CambiarDock(Me, False, Not IsMaximize)
                Case "BtnReplace"
                    Try
                        RaiseEvent ReplaceDocument(Result)
                    Catch ex As Exception
                        Zamba.Core.ZClass.raiseerror(ex)
                    End Try
                Case "BtnNetron"
                    ImgViewer.MenuAddNetron_Click(Me, New EventArgs)
                Case "Cerrar"
                    CloseDocument()
                Case "btnPreview"
                Case "VerDocumental"
                    'LLama al metodo para mostrarlo en la grilla de resultados
                    Dim docId(0) As String
                    docId.SetValue(Result.ID.ToString(), 0)
                    ModDocuments.DoSearch(Result.DocTypeId, docId, False)
                Case "EMAIL"
                    SendMail(Result)

                Case "IMPRIMIR"
                    Dim results As List(Of IResult)
                    results.Add(Result)
                    Imprimir(results, False)
                Case "VERSIONESDELDOCUMENTO"
                    ShowVersionComment(Result)
                Case "GUARDARDOCUMENTOCOMO"
                    GuardarComo(Result)
                Case "AGREGARACARPETA"
                    AgregarACarpeta(Result)
                Case "GOTOWF"
                    Try
                        RaiseEvent ShowAssociatedWFbyDocId(Convert.ToInt64(Result.ID.ToString()), Result.DocTypeId)
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                Case "decrypt"
                    DecryptRequested()

            End Select
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub


    Private Sub CloseImage()
        Dim parentTab As TabControl
        Dim fakeTabPage As TabPage

        Try
            parentTab = DirectCast(Parent, TabControl)

            'Si la solapa seleccionada es la última, se reordena la anteúltima 
            'solapa al final. Si esto no se hace me devuelve a la solapa de
            'foro automáticamente.
            If parentTab.SelectedIndex = parentTab.TabCount - 1 Then
                fakeTabPage = parentTab.TabPages(parentTab.TabCount - 2)
                parentTab.TabPages.Remove(fakeTabPage)
                parentTab.TabPages.Add(fakeTabPage)
            End If
            CloseDocument()
        Finally
            If Not IsNothing(fakeTabPage) Then fakeTabPage = Nothing
            parentTab = Nothing
        End Try
    End Sub



    Public Sub CloseDocument(Optional ByVal removeTab As Boolean = False)
        Try
            ClearUCBrowser()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Try
            'Se aplica delegados para no generar errores al trabajar con excel catcher.
            If removeTab Then
                Invoke(New DelegateClose(AddressOf RemoveTabPage))
            Else
                Invoke(New DelegateClose(AddressOf Close))
            End If
        Catch
        End Try
    End Sub
    ''' <summary>
    ''' Remueve el tab y hace dispose del objeto.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Tomas] 29/10/2009  Modified    Código extraido de CloseDocument() para aplicar delegados.
    ''' </history>
    Public Sub Close()
        Try
            Dim tempParent As TabControl = Parent
            Dim tabPosition As Int32

            If tempParent IsNot Nothing Then

                tabPosition = tempParent.TabPages.IndexOf(Me)

                Try
                    tempParent.Visible = False
                    tempParent.SuspendLayout()
                    tempParent.TabPages.Remove(Me)
                    If tabPosition = tempParent.TabPages.Count AndAlso tabPosition <> 0 Then
                        tempParent.SelectTab(tabPosition - 1)
                    End If
                Catch ex As Exception
                    Throw
                Finally
                    tempParent.ResumeLayout()
                    tempParent.Visible = True
                    tempParent = Nothing
                End Try

                RaiseEvent CloseOpenTask(True)

            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub


    ''' <summary>
    ''' Crea el archivo temporal que va a ser visualizado trayendolo desde el servidor
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [AlejandroR] 11/08/2010  Created
    ''' </history>
    Private Function CreateTempFileToShow(ByVal ResulGt As Result) As Boolean
        If Result IsNot Nothing AndAlso Not String.IsNullOrEmpty(Result.FullPath) AndAlso Result.FullPath.IndexOf("aspx", StringComparison.CurrentCultureIgnoreCase) = -1 Then
            Dim FTemp As FileInfo = Nothing
            Dim dir As IO.DirectoryInfo

            Try
                dir = GetTempDir("\OfficeTemp")
                If dir.Exists = False Then dir.Create()
                Dim name As String = Result.ID.ToString()

                If _documentEncryptionState = ZEncpritonStates.ToDecrypt Then
                    Dim resB As New ResultBusinessExt()

                    Dim isError As Boolean = False
                    Try
                        _file = resB.CopyAndDecrypt(Result, InputBox("Ingrese la clave de desencriptacion", "Zamba Software"))
                        If String.IsNullOrEmpty(_file) Then
                            isError = True
                        End If
                    Catch ex As CryptographicException
                        ZClass.raiseerror(ex)
                        isError = True
                    Catch ex2 As Exception
                        ZClass.raiseerror(ex2)
                    End Try

                    If isError Then
                        MessageBox.Show("Clave incorrecta", "Zamba Software")
                        _documentEncryptionState = ZEncpritonStates.Encrypted
                        CheckEncryptedFile()
                        Return False
                    Else
                        Return True
                    End If
                Else
                    If _documentEncryptionState <> ZEncpritonStates.Encrypted Then
                        name = Path.GetFileName(Result.FullPath)
                        FTemp = New FileInfo(Zamba.Core.FileBusiness.GetUniqueFileName(dir.FullName, name))
                        name = Nothing
                        _file = FTemp.FullName

                        Try
                            If FTemp.Exists Then
                                FTemp.Delete()
                            End If
                        Catch ex As Exception
                            If ex.Message.Contains("used by another process") = False Then
                                ZClass.raiseerror(ex)
                            End If
                        End Try

                        _file = Results_Business.CopyFileToTemp(Result, Result.RealFullPath, FTemp.FullName)

                        'Result.Doc_File = FTemp.FullName

                        If Result.FullPath.ToUpper.EndsWith(".HTML") OrElse Result.FullPath.ToUpper.EndsWith(".HTM") Then
                            Results_Business.CopySubDirAndFilesBrowser(dir.FullName, Result.FullPath.Remove(Result.FullPath.LastIndexOf("\")), Result.FullPath.Remove(Result.FullPath.LastIndexOf("\")))
                        End If

                        Return True
                    End If
                End If

                Return False
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                'Libera recursos tomados
                If Not IsNothing(FTemp) Then FTemp = Nothing
                If Not IsNothing(dir) Then dir = Nothing
            End Try
        End If
    End Function

    ''' <summary>
    ''' Elimina los archivos temporales y realiza la copia al servidor
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [AlejandroR] 11/08/2010  Created
    ''' </history>
    Private Sub CopyAndCleanFiles(ByVal DisableAutomaticVersion As Boolean)
        If Not IsNothing(_file) Then
            Dim fi As FileInfo = Nothing
            Dim fa As FileInfo = Nothing

            Try
                fi = New FileInfo(_file)

                'if _result.isoffice() andalso not _result.ispdf() then
                If _result.IsEditable Then
                    If _result.DocType.IsReadOnly = False Then
                        If flagAutomaticNewVersion AndAlso _result.IsOffice AndAlso Not DisableAutomaticVersion Then
                        Else
                            fa = New FileInfo(_result.FullPath)
                            ' en caso de que no use version automatica y se deba guardar
                            fa.Attributes = IO.FileAttributes.Archive
                            If fi.Exists Then
                                fi.CopyTo(_result.FullPath, True)
                            End If
                        End If
                    End If
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
                fa = Nothing
                fi = Nothing
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Remueve el tab y hace dispose del objeto.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Tomas] 29/10/2009  Modified    Código extraido de CloseDocument() para aplicar delegados.
    ''' </history>
    Public Sub RemoveTabPage()
        Dim parentTab As TabControl = DirectCast(Parent, TabControl)
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Removing Tab")

            If Not (Parent.Parent.GetType Is GetType(Zamba.Viewers.ExternalVisualizer)) _
                AndAlso String.Compare(Parent.Parent.Parent.Parent.Name, "UCTaskViewer") = 0 _
                AndAlso GetTaskTabsCount() = 1 Then

                'Si la solapa seleccionada es la última, se reordena la anteúltima 
                'solapa al final. Si esto no se hace me devuelve a la solapa de
                'foro automáticamente.
                If parentTab.SelectedIndex = parentTab.TabCount - 1 Then
                    Dim fakeTabPage As TabPage = parentTab.TabPages(parentTab.TabCount - 2)
                    parentTab.TabPages.Remove(fakeTabPage)
                    parentTab.TabPages.Add(fakeTabPage)
                    parentTab.SelectTab(parentTab.TabCount - 1)
                    parentTab.TabPages.Remove(Me)
                    fakeTabPage = Nothing
                Else
                    parentTab.TabPages.Remove(Me)
                    parentTab.SelectTab(parentTab.TabCount - 1)
                End If

                RaiseEvent CloseOpenTask(True)

            Else

                'Si la tarea proviene de una asociada se refresca dicha tarea
                If isAsocTask Then RaiseEvent RefreshAsocTask()

                If IsMaximize Then
                    RaiseEvent CambiarDock(Me, False, True)
                End If

                'Si la solapa seleccionada es la última, se reordena la anteúltima 
                'solapa al final. Si esto no se hace me devuelve a la solapa de
                'foro automáticamente.
                If parentTab.SelectedIndex = parentTab.TabCount - 1 Then
                    Dim fakeTabPage As TabPage

                    If parentTab.TabCount > 1 Then
                        fakeTabPage = parentTab.TabPages(parentTab.TabCount - 2)
                    Else
                        fakeTabPage = parentTab.TabPages(parentTab.TabCount - 1)
                    End If

                    parentTab.TabPages.Remove(fakeTabPage)
                    parentTab.TabPages.Add(fakeTabPage)
                    parentTab.SelectTab(parentTab.TabCount - 1)
                    parentTab.TabPages.Remove(Me)
                    fakeTabPage = Nothing
                Else
                    parentTab.TabPages.Remove(Me)
                    parentTab.SelectTab(parentTab.TabCount - 1)
                End If

                RaiseEvent CloseOpenTask(True)

            End If
            Dispose()

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            'NO APLICAR DISPOSE SOBRE ESTE OBJETO
            parentTab = Nothing
        End Try

    End Sub

    Private Sub MenuButtonItem7_Activate(ByVal sender As System.Object, ByVal e As EventArgs) Handles MenuButtonItem7.Click
        ImgViewer.MenuAddNetron_Click(Me, New EventArgs)
    End Sub

    Private Sub ButtonItem12_Activate(ByVal sender As System.Object, ByVal e As EventArgs) Handles ButtonItem12.Click
        Estado = 3
    End Sub
    Private Sub ButtonItem13_Activate(ByVal sender As System.Object, ByVal e As EventArgs) Handles ButtonItem13.Click
        Estado = 4
    End Sub
    Private Sub ButtonItem14_Activate(ByVal sender As System.Object, ByVal e As EventArgs) Handles ButtonItem14.Click
        Estado = 1
    End Sub
    Private Sub NETRON16_Activate(ByVal sender As System.Object, ByVal e As EventArgs) Handles NETRON16.Click
        Estado = 2
    End Sub

    Public Event RefreshTask(ByVal Task As ITaskResult)
    Public Event ReloadAsociatedResult(ByVal AsociatedResult As Core.Result)
    Private Sub BtnRefresh_Activate_1(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnRefresh.Click
        Dim WFTB As New WFTaskBusiness
        Try
            '(pablo) 27022011
            If Result.GetType.FullName.ToString = "Zamba.Core.Result" Then
                RaiseEvent ReloadAsociatedResult(Result)
                RefreshData(Nothing)
            Else
                Dim task As TaskResult
                If Result.GetType() Is GetType(TaskResult) Then
                    task = WFTB.GetTaskByTaskIdAndDocTypeId(DirectCast(Result, TaskResult).TaskId, Result.DocTypeId, 0)
                Else
                    task = WFTB.GetTaskByDocId(Result.ID)
                End If
                If task IsNot Nothing Then
                    _result = task
                    RaiseEvent RefreshTask(task)
                    RefreshData(task)
                ElseIf Not Result.ISVIRTUAL Then
                    ShowDocument(False, False, False, False, False)
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            WFTB = Nothing
        End Try
    End Sub
    Public Sub RefreshData(ByVal newResult As ITaskResult)
        Try
            If IsNothing(newResult) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Actualizando tarea en documento actual")
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Actualizando tarea con nuevo documento")
                _result = newResult
            End If

            If Not IsNothing(FormBrowser) Then
                FormBrowser.RefreshData(Result)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub MsgWB_HandleDestroyed(ByVal sender As Object, ByVal e As EventArgs) Handles MsgWB.HandleDestroyed

        If Result IsNot Nothing AndAlso Result.IsMsg Then
            CloseMsgDocument()
        End If
    End Sub

    Private Sub MsgWB_SizeChanged(ByVal sender As Object, ByVal e As EventArgs) Handles MsgWB.SizeChanged
        MsgPanelSetSize()
    End Sub

    'Private Sub UCDocumentViewer2_HandleDestroyed(ByVal sender As Object, ByVal e As EventArgs) Handles Me.HandleDestroyed
    '    Try
    '        If Not IsNothing(myOutlook) Then

    '            myOutlook.DisposingParent = True
    '            myOutlook.CloseMailItem(myOutlook.closeFromControlbox)

    '        End If
    '    Catch ex As Exception
    '        ZClass.raiseerror(ex)
    '    End Try
    'End Sub

    'Private Sub SetMsgPanelSpecialAttributes()
    '    If winHandle <> IntPtr.Zero Then

    '        hMenu = GetSystemMenu(winHandle, 0)
    '        If hMenu <> IntPtr.Zero Then
    '            DeleteMenu(hMenu, SC_MINIMIZE, MF_BYCOMMAND)
    '            DeleteMenu(hMenu, SC_MAXIMIZE, MF_BYCOMMAND)
    '            DeleteMenu(hMenu, SC_SIZE, MF_BYCOMMAND)
    '            DeleteMenu(hMenu, SC_MOVE, MF_BYCOMMAND)
    '            DeleteMenu(hMenu, SC_RESTORE, MF_BYCOMMAND)
    '            DeleteMenu(hMenu, SC_NEXTWINDOW, MF_BYCOMMAND)
    '            DeleteMenu(hMenu, SC_CLOSE, MF_BYCOMMAND)
    '            DeleteMenu(hMenu, 0, MF_BYPOSITION)

    '            DeleteMenu(hMenu, 6, MF_BYPOSITION)

    '        End If

    '        Dim winStyle As Int32 = GetWindowLong(winHandle, GWL_STYLE)
    '        oldWinStyle = winStyle
    '        winStyle = winStyle And Not WS_MINIMIZEBOX
    '        winStyle = winStyle And Not WS_MAXIMIZEBOX
    '        SetWindowLong(winHandle, GWL_STYLE, winStyle)

    '        ZTrace.WriteLineIf(ZTrace.IsInfo, " Se cambiaron atributos de ventana (Botones de cerrar, minimizar y demas) Then " & Date.Now.ToString())
    '    End If
    'End Sub

    Private Function CheckOutlookVersionInstalled() As Integer

        'The subkey's string value we check is like 
        'Excel.Application.<version>, i e Excel.Application.10 

        'The subkey we are interested of is located under the 
        'HKEY_CLASSES_ROOT class.
        Const stXL_SUBKEY As String = "\Outlook.Application\CurVer"

        Dim rkVersionKey As RegistryKey = Nothing
        Dim stVersion As String = String.Empty
        Dim stXLVersion As String = String.Empty

        'A very simple regular expression where:
        '[8-9] means look for the numbers 8 and 9
        'and start in the end of the expression.
        Dim stRegExpr As String = "[8-9]$"

        'If we need to make sure that for instance Excel 2003 (11) or
        'later is installed then the above expression can be modified
        'to:
        'Dim stRegExpr As String = "[8-9]$|[1]0$"

        Dim iVersion As Integer = Nothing

        Try
            'Here we try to open the subkey.
            rkVersionKey = Registry.ClassesRoot.OpenSubKey(stXL_SUBKEY, False)

            'If it does not exist it means that Excel is not installed at all.
            If rkVersionKey Is Nothing Then
                iVersion = 0
                Return iVersion
            End If

            'OK, Excel is installed let's find out which version is available.
            stXLVersion = CStr(rkVersionKey.GetValue(stVersion))

            'Here we match the retrieved value with our created regular
            'expression.

            If Regex.IsMatch(stXLVersion, "8") Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Versión de outlook en office 97")
                Return 8
            End If
            If Regex.IsMatch(stXLVersion, "9") Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Versión de outlook en office 2000")
                Return 9
            End If
            If Regex.IsMatch(stXLVersion, "10") Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Versión de outlook en office 2002")
                Return 10
            End If
            If Regex.IsMatch(stXLVersion, "11") Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Versión de outlook en office 2003")
                Return 11
            End If
            If Regex.IsMatch(stXLVersion, "12") Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Versión de outlook en office 2007")
                Return 12
            End If
            If Regex.IsMatch(stXLVersion, "13") Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Versión de outlook en office 2010")
                Return 12
            End If


            Return 0

        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        Finally
            If Not rkVersionKey Is Nothing Then
                rkVersionKey.Close()
            End If
        End Try

    End Function

    ''' <summary>
    ''' Abre un documento de Office por fuera
    ''' </summary>
    ''' <param name="file"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function OpenExternOfficeDocument(ByVal file As String) As Boolean
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "OpenExternOfficeDocument - Documento office abierto por fuera")

            Dim rk As RegistryKey
            Dim fi As New FileInfo(file)

            Dim prekey As String = String.Empty
            Dim posKey As String = String.Empty
            Dim exec As String = String.Empty
            Dim value As String = String.Empty

            If Not fi.Exists Then
                Throw New Exception("El archivo " & file & " no existe")
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
                ZClass.raiseerror(ex)
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
                            Dim finf As New FileInfo(path & exec)

                            If finf.Exists = True Then
                                finf = New FileInfo(file)
                                If finf.Exists = True Then

                                    Dim command As String
                                    If fi.Extension.ToLower() = ".msg" Then
                                        'El msg se abre con la ruta directa ya que sino lo abre como adjunto de un msg nuevo
                                        command = Chr(34) & file & Chr(34)
                                    Else
                                        command = Chr(34) & path & exec & Chr(34) & " " & Chr(34) & file & Chr(34)
                                    End If
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "command: " & command)

                                    Dim proc As New System.Diagnostics.Process()
                                    proc.Start(command)

                                    'Shell(Command, AppWinStyle.Hide, False)
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

    ''' <summary>
    '''     Abre un msg para outlook 2000
    ''' </summary>
    ''' <param name="result"></param>
    ''' <returns></returns>
    ''' <history>
    '''     Javier  22/12/2010  Created
    ''' </history>
    Private Function OpenMsgOffice2000(ByVal result As Result)
        Dim Dir As System.IO.DirectoryInfo = GetTempDir("\OfficeTemp")
        Dim strPathLocal As String = Dir.FullName & result.FullPath.Remove(0, result.FullPath.LastIndexOf("\"))
        strPathLocal = Path.Combine(Path.GetDirectoryName(strPathLocal), Path.GetFileNameWithoutExtension(strPathLocal) & "_" & DateTime.Now.ToString("HHmmss") & Path.GetExtension(strPathLocal))

        Try
            File.Copy(result.FullPath, strPathLocal, True)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        ZTrace.WriteLineIf(ZTrace.IsInfo, " Se creo Archivo temporal de " & strPathLocal & " " & Date.Now.ToString())

        OpenExternOfficeDocument(strPathLocal)

    End Function

    ''' <summary>
    ''' Agrega label que informa al usuario que la previsualización de documentos se encuentra desactivada
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ShowNonPreviewMessage()
        'Ocultar las toolbars

        'Crea el mensaje
        Dim lblNonPreview As New Label()
        lblNonPreview.Dock = DockStyle.Fill
        lblNonPreview.Text = "La previsualización de documentos a insertar ha sido desactivada." & vbCrLf &
            "Para volver a activar dicha opción debe acceder a:" & vbCrLf &
            "    'OPCIONES > USUARIO > PREFERENCIAS > INSERCIÓN' y marcar la casilla 'Previsualizar documento'" & vbCrLf &
            "En caso de no poder acceder a las preferencias de usuario consulte con el área de sistemas."

        'Agranda la letra para ocupar mas espacio y sea mas legible
        Dim font As New Font(lblNonPreview.Font.FontFamily, 9)
        lblNonPreview.Font = font

        'Agrega el mensaje al control
        If Controls.Count > 0 Then Controls.Clear()
        Controls.Add(lblNonPreview)
        lblNonPreview.Width = Width
        lblNonPreview.Height = Height
    End Sub

    ''' <summary>
    ''' Revisa el estado del result y en base a eso activa o no los botones pertinentes
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckEncryptedFile()
        Dim resb As New ResultBusinessExt
        If resb.IsDocumentEncrypted(Result.ID, Result.DocTypeId) Then
            If _documentEncryptionState = ZEncpritonStates.Decrypted OrElse _documentEncryptionState = ZEncpritonStates.ToDecrypt Then
                lblEncryptFile.Visible = False
                btnDecryptDocument.Visible = False
            Else
                lblEncryptFile.Visible = True
                btnDecryptDocument.Visible = True
                _documentEncryptionState = ZEncpritonStates.Encrypted
            End If
        Else
            lblEncryptFile.Visible = False
            btnDecryptDocument.Visible = False
            _documentEncryptionState = ZEncpritonStates.Decrypted
        End If
    End Sub

    ''' <summary>
    ''' Setea el estado para desencriptar y llama al mostrar documento para hacer la desencriptacion.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DecryptRequested()
        _documentEncryptionState = ZEncpritonStates.ToDecrypt
        ShowImage(_useVirtual, _insertedDoc, _comeToWF)
    End Sub



    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub btnOpenDocument_click(sender As Object, e As EventArgs)
        ShowDocument(False, False, False, False, True)
    End Sub

End Class

