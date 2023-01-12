Imports Zamba.Core.DocTypes.DocAsociated
Imports Zamba.Core.Search
Imports Zamba.AppBlock
Imports Zamba.Core
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports System.Diagnostics
Imports Zamba.AdminControls
Imports Zamba.Viewers.WindowsApi.Usr32Api
Imports System.Text.RegularExpressions
Imports Microsoft.Win32
Imports System.Collections.Generic

Public Class UCDocumentViewer2
    Inherits TabPage
    Implements IDisposable

#Region " Windows Form Designer generated code "
    '[Ezequiel] 08/04/09
    Public Event GetIndexsEvent(ByRef indexs As ArrayList)

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If components IsNot Nothing Then components.Dispose()
                If ImgViewer IsNot Nothing Then ImgViewer.Dispose()
                If FormBrowser IsNot Nothing Then FormBrowser.Dispose()
                If OffWB IsNot Nothing Then OffWB.Dispose()
                If MsgWB IsNot Nothing Then MsgWB.Dispose()
                If Prev IsNot Nothing Then Prev.Dispose()
                If ParentTabControl IsNot Nothing Then ParentTabControl.Dispose()
                If RotatedImg IsNot Nothing Then RotatedImg.Dispose()
                If _result IsNot Nothing Then _result.Dispose()
                If bt IsNot Nothing Then bt.Dispose()
                If bt2 IsNot Nothing Then bt2.Dispose()
                If Txt IsNot Nothing Then Txt.Dispose()
                If Txt2 IsNot Nothing Then Txt2.Dispose()
                If WFDesigner IsNot Nothing Then WFDesigner.Dispose()
                If ZControl_PanelLinks IsNot Nothing Then ZControl_PanelLinks.Dispose()
                If P IsNot Nothing Then P.Dispose()
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Try
            Me.DisposeNotas()
            Me.disposeRotation()
            If Me.Result IsNot Nothing Then
                Me.Result.DisposePicture()
                Me.Result.Dispose()
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    '  Friend WithEvents ctxArchivo As System.Windows.Forms.ContextMenu
    ' Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    ' Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    '  Friend WithEvents ImageList1 As System.Windows.Forms.ImageList

    Friend WithEvents ButtonItem1 As ToolStripButton
    Friend WithEvents ButtonDocumental As ToolStripButton
    Friend WithEvents Buttonclose As ToolStripButton
    Friend WithEvents Buttonclose2 As ToolStripButton
    Friend WithEvents ButtonItem2 As ToolStripButton
    Friend WithEvents LayoutButton As ToolStripButton


    Friend WithEvents ButtonItem3 As ToolStripButton
    Friend WithEvents ButtonItem4 As ToolStripButton
    Friend WithEvents ButtonItem6 As ToolStripButton
    Friend WithEvents ButtonItem7 As ToolStripButton
    Friend WithEvents ButtonItem8 As ToolStripButton
    Friend WithEvents ButtonItem9 As ToolStripButton
    Friend WithEvents ButtonItem10 As ToolStripButton
    Friend WithEvents ButtonItem11 As ToolStripButton
    Friend WithEvents BtnReplaceDocument As ToolStripButton
    Friend WithEvents ButtonItem16 As ToolStripButton
    Friend WithEvents ButtonItem17 As ToolStripButton
    Friend WithEvents ButtonItem18 As ToolStripButton
    Friend WithEvents ButtonItem19 As ToolStripButton
    Friend WithEvents ButtonItem20 As ToolStripButton
    '    Friend WithEvents txtNumPag2 As System.Windows.Forms.TextBox
    Friend WithEvents txtNumPag As ToolStripComboBox
    Friend WithEvents DropDownMenuItem1 As ToolStripDropDownButton
    Friend WithEvents ButtonItem12 As ToolStripMenuItem
    Friend WithEvents ButtonItem13 As ToolStripMenuItem
    Friend WithEvents ButtonItem14 As ToolStripMenuItem
    Friend WithEvents NETRON16 As ToolStripMenuItem
    Friend WithEvents MenuButtonItem7 As ToolStripMenuItem
    Friend WithEvents lblPagin As ToolStripLabel
    Friend WithEvents ButtonItem15 As ToolStripButton
    Friend WithEvents ToolBar1 As ToolStrip
    'Friend WithEvents BItem1 As ToolStripButton
    Friend WithEvents BItem2 As ToolStripButton
    Friend WithEvents BItem3 As ToolStripButton
    Friend WithEvents BPrint As ToolStripButton
    Friend WithEvents InfoView As ToolStripLabel
    Friend WithEvents DesignSandBar As ToolStrip
    Friend WithEvents BtnRefresh As ToolStripButton
    Friend WithEvents ButtonItem5 As ToolStripButton
    Friend WithEvents BItem4 As ToolStripButton
    Friend WithEvents BItem5 As ToolStripButton
    Friend WithEvents BItemGoToSearch As ToolStripButton
    Friend WithEvents ToolStripContainer1 As ToolStripContainer

    Public WithEvents btnSaveAs As System.Windows.Forms.ToolStripButton
    Public WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents btnAdjuntarEmail As System.Windows.Forms.ToolStripButton
    Public WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents btnAdjuntarMensaje As System.Windows.Forms.ToolStripButton
    Public WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents btnAgregarDocumentoCarpeta As System.Windows.Forms.ToolStripButton
    Public WithEvents ToolStripSeparator13 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents btnVerVersionesDelDocumento As System.Windows.Forms.ToolStripButton
    Public WithEvents btnAgregarUnaNuevaVersionDelDocuemto As System.Windows.Forms.ToolStripButton
    Public WithEvents ToolStripSeparator12 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents btnGotoWorkflow As System.Windows.Forms.ToolStripButton
    Public WithEvents btnChangePosition As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnGoToWF As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnGotoWfdb As System.Windows.Forms.ToolStripButton
    Public WithEvents btnGotoHelp As System.Windows.Forms.ToolStripButton

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ButtonDocumental = New System.Windows.Forms.ToolStripButton
        Me.Buttonclose = New System.Windows.Forms.ToolStripButton
        Me.Buttonclose2 = New System.Windows.Forms.ToolStripButton
        Me.LayoutButton = New System.Windows.Forms.ToolStripButton
        Me.ButtonItem11 = New System.Windows.Forms.ToolStripButton
        Me.DropDownMenuItem1 = New System.Windows.Forms.ToolStripDropDownButton
        Me.ButtonItem12 = New System.Windows.Forms.ToolStripMenuItem
        Me.ButtonItem13 = New System.Windows.Forms.ToolStripMenuItem
        Me.ButtonItem14 = New System.Windows.Forms.ToolStripMenuItem
        Me.NETRON16 = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuButtonItem7 = New System.Windows.Forms.ToolStripMenuItem
        Me.txtNumPag = New System.Windows.Forms.ToolStripComboBox
        Me.ButtonItem20 = New System.Windows.Forms.ToolStripButton
        Me.lblPagin = New System.Windows.Forms.ToolStripLabel
        Me.ButtonItem18 = New System.Windows.Forms.ToolStripButton
        Me.ButtonItem19 = New System.Windows.Forms.ToolStripButton
        Me.DesignSandBar = New System.Windows.Forms.ToolStrip
        Me.BPrint = New System.Windows.Forms.ToolStripButton
        Me.BtnReplaceDocument = New System.Windows.Forms.ToolStripButton
        Me.ButtonItem1 = New System.Windows.Forms.ToolStripButton
        Me.ButtonItem2 = New System.Windows.Forms.ToolStripButton
        Me.ButtonItem3 = New System.Windows.Forms.ToolStripButton
        Me.ButtonItem4 = New System.Windows.Forms.ToolStripButton
        Me.ButtonItem6 = New System.Windows.Forms.ToolStripButton
        Me.ButtonItem7 = New System.Windows.Forms.ToolStripButton
        Me.ButtonItem8 = New System.Windows.Forms.ToolStripButton
        Me.ButtonItem9 = New System.Windows.Forms.ToolStripButton
        Me.ButtonItem10 = New System.Windows.Forms.ToolStripButton
        Me.ButtonItem16 = New System.Windows.Forms.ToolStripButton
        Me.ButtonItem17 = New System.Windows.Forms.ToolStripButton
        Me.ButtonItem5 = New System.Windows.Forms.ToolStripButton
        Me.btnSaveAs = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator
        Me.btnAdjuntarEmail = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator
        Me.btnAdjuntarMensaje = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator
        Me.btnAgregarDocumentoCarpeta = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator13 = New System.Windows.Forms.ToolStripSeparator
        Me.btnVerVersionesDelDocumento = New System.Windows.Forms.ToolStripButton
        Me.btnAgregarUnaNuevaVersionDelDocuemto = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator
        Me.btnGotoWorkflow = New System.Windows.Forms.ToolStripButton
        Me.btnChangePosition = New System.Windows.Forms.ToolStripButton
        Me.ButtonItem15 = New System.Windows.Forms.ToolStripButton
        Me.ToolBar1 = New System.Windows.Forms.ToolStrip
        Me.BItem2 = New System.Windows.Forms.ToolStripButton
        Me.BItem3 = New System.Windows.Forms.ToolStripButton
        Me.BItem5 = New System.Windows.Forms.ToolStripButton
        Me.BItemGoToSearch = New System.Windows.Forms.ToolStripButton
        Me.BItem4 = New System.Windows.Forms.ToolStripButton
        Me.InfoView = New System.Windows.Forms.ToolStripLabel
        Me.BtnRefresh = New System.Windows.Forms.ToolStripButton
        Me.ToolStripContainer1 = New System.Windows.Forms.ToolStripContainer
        Me.btnGotoHelp = New System.Windows.Forms.ToolStripButton
        Me.BtnGoToWF = New System.Windows.Forms.ToolStripButton
        Me.btnGotoWfdb = New System.Windows.Forms.ToolStripButton
        Me.DesignSandBar.SuspendLayout()
        Me.ToolBar1.SuspendLayout()
        Me.ToolStripContainer1.TopToolStripPanel.SuspendLayout()
        Me.ToolStripContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonDocumental
        '
        Me.ButtonDocumental.Name = "ButtonDocumental"
        Me.ButtonDocumental.Size = New System.Drawing.Size(111, 22)
        Me.ButtonDocumental.Tag = "VerDocumental"
        Me.ButtonDocumental.Text = "Ver en Resultados"
        Me.ButtonDocumental.ToolTipText = "Ver en Resultados"
        '
        'Buttonclose
        '
        Me.Buttonclose.Image = Global.Zamba.Viewers.My.Resources.Resources._Exit
        Me.Buttonclose.Name = "Buttonclose"
        Me.Buttonclose.Size = New System.Drawing.Size(59, 22)
        Me.Buttonclose.Tag = "Cerrar"
        Me.Buttonclose.Text = "Cerrar"
        Me.Buttonclose.ToolTipText = "Cerrar"
        '
        'Buttonclose2
        '
        Me.Buttonclose2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Buttonclose2.Image = Global.Zamba.Viewers.My.Resources.Resources._Exit
        Me.Buttonclose2.Name = "Buttonclose2"
        Me.Buttonclose2.Size = New System.Drawing.Size(23, 22)
        Me.Buttonclose2.Tag = "Cerrar"
        Me.Buttonclose2.Text = "Cerrar"
        Me.Buttonclose2.ToolTipText = "Cerrar"
        '
        'LayoutButton
        '
        Me.LayoutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.LayoutButton.Image = Global.Zamba.Viewers.My.Resources.Resources.window_split_hor
        Me.LayoutButton.Name = "LayoutButton"
        Me.LayoutButton.Size = New System.Drawing.Size(23, 22)
        Me.LayoutButton.Tag = "DIVIDIR"
        Me.LayoutButton.Text = " Dividir"
        Me.LayoutButton.ToolTipText = "Dividir Pantalla"
        '
        'ButtonItem11
        '
        Me.ButtonItem11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonItem11.Image = Global.Zamba.Viewers.My.Resources.Resources.replace2
        Me.ButtonItem11.Name = "ButtonItem11"
        Me.ButtonItem11.Size = New System.Drawing.Size(23, 22)
        Me.ButtonItem11.Tag = "BtnReplace"
        Me.ButtonItem11.ToolTipText = "REEMPLAZAR DOCUMENTO"
        '
        'DropDownMenuItem1
        '
        Me.DropDownMenuItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.DropDownMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ButtonItem12, Me.ButtonItem13, Me.ButtonItem14, Me.NETRON16, Me.MenuButtonItem7})
        Me.DropDownMenuItem1.Image = Global.Zamba.Viewers.My.Resources.Resources.note_add
        Me.DropDownMenuItem1.Name = "DropDownMenuItem1"
        Me.DropDownMenuItem1.Size = New System.Drawing.Size(29, 22)
        Me.DropDownMenuItem1.Text = "NOTAS"
        '
        'ButtonItem12
        '
        Me.ButtonItem12.Name = "ButtonItem12"
        Me.ButtonItem12.Size = New System.Drawing.Size(228, 22)
        Me.ButtonItem12.Tag = "BtnNota"
        Me.ButtonItem12.Text = "MODO NOTA"
        '
        'ButtonItem13
        '
        Me.ButtonItem13.Name = "ButtonItem13"
        Me.ButtonItem13.Size = New System.Drawing.Size(228, 22)
        Me.ButtonItem13.Tag = "BtnFirma"
        Me.ButtonItem13.Text = "MODO FIRMA DE USUARIO"
        '
        'ButtonItem14
        '
        Me.ButtonItem14.Name = "ButtonItem14"
        Me.ButtonItem14.Size = New System.Drawing.Size(228, 22)
        Me.ButtonItem14.Tag = "BtnOCR"
        Me.ButtonItem14.Text = "MODO OCR"
        '
        'NETRON16
        '
        Me.NETRON16.Name = "NETRON16"
        Me.NETRON16.Size = New System.Drawing.Size(228, 22)
        Me.NETRON16.Tag = "BtnNetron"
        Me.NETRON16.Text = "MODO RECTANGULO"
        '
        'MenuButtonItem7
        '
        Me.MenuButtonItem7.Enabled = False
        Me.MenuButtonItem7.Name = "MenuButtonItem7"
        Me.MenuButtonItem7.Size = New System.Drawing.Size(228, 22)
        Me.MenuButtonItem7.Text = "NUEVO RECTANGULO"
        '
        'txtNumPag
        '
        Me.txtNumPag.Name = "txtNumPag"
        Me.txtNumPag.Size = New System.Drawing.Size(121, 25)
        Me.txtNumPag.Tag = ""
        '
        'ButtonItem20
        '
        Me.ButtonItem20.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonItem20.Image = Global.Zamba.Viewers.My.Resources.Resources.bullet_triangle_blue
        Me.ButtonItem20.Name = "ButtonItem20"
        Me.ButtonItem20.Size = New System.Drawing.Size(23, 22)
        Me.ButtonItem20.Tag = "IR"
        Me.ButtonItem20.Text = "IR"
        Me.ButtonItem20.ToolTipText = "IR A LA PAGINA ESPECIFICADA"
        '
        'lblPagin
        '
        Me.lblPagin.ForeColor = System.Drawing.Color.Black
        Me.lblPagin.Name = "lblPagin"
        Me.lblPagin.Size = New System.Drawing.Size(41, 22)
        Me.lblPagin.Text = "0 DE 0"
        '
        'ButtonItem18
        '
        Me.ButtonItem18.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonItem18.Image = Global.Zamba.Viewers.My.Resources.Resources.arrow_right_blue
        Me.ButtonItem18.Name = "ButtonItem18"
        Me.ButtonItem18.Size = New System.Drawing.Size(23, 22)
        Me.ButtonItem18.Tag = "BtnNext"
        Me.ButtonItem18.ToolTipText = "IR A PAGINA SIGUIENTE"
        '
        'ButtonItem19
        '
        Me.ButtonItem19.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonItem19.Image = Global.Zamba.Viewers.My.Resources.Resources.media_end
        Me.ButtonItem19.Name = "ButtonItem19"
        Me.ButtonItem19.Size = New System.Drawing.Size(23, 22)
        Me.ButtonItem19.Tag = "btnlastpage"
        Me.ButtonItem19.ToolTipText = "IR A ULTIMA PAGINA"
        '
        'DesignSandBar
        '
        Me.DesignSandBar.BackColor = System.Drawing.Color.Silver
        Me.DesignSandBar.Dock = System.Windows.Forms.DockStyle.None
        Me.DesignSandBar.Font = New System.Drawing.Font("Verdana", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DesignSandBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BPrint, Me.BtnReplaceDocument, Me.ButtonItem1, Me.ButtonDocumental, Me.ButtonItem2, Me.ButtonItem3, Me.ButtonItem4, Me.ButtonItem6, Me.ButtonItem7, Me.ButtonItem8, Me.ButtonItem9, Me.ButtonItem10, Me.ButtonItem16, Me.ButtonItem17, Me.txtNumPag, Me.ButtonItem20, Me.lblPagin, Me.ButtonItem18, Me.ButtonItem19, Me.DropDownMenuItem1, Me.btnGotoWfdb, Me.Buttonclose, Me.ButtonItem5})
        Me.DesignSandBar.Location = New System.Drawing.Point(3, 0)
        Me.DesignSandBar.Name = "DesignSandBar"
        Me.DesignSandBar.Size = New System.Drawing.Size(758, 25)
        Me.DesignSandBar.TabIndex = 1
        '
        'BPrint
        '
        Me.BPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BPrint.Image = Global.Zamba.Viewers.My.Resources.Resources.printer
        Me.BPrint.Name = "BPrint"
        Me.BPrint.Size = New System.Drawing.Size(23, 22)
        Me.BPrint.Tag = "IMPRIMIR"
        Me.BPrint.Text = "Imprimir"
        Me.BPrint.ToolTipText = "Imprimir"
        '
        'BtnReplaceDocument
        '
        Me.BtnReplaceDocument.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BtnReplaceDocument.Image = Global.Zamba.Viewers.My.Resources.Resources.replace2
        Me.BtnReplaceDocument.Name = "BtnReplaceDocument"
        Me.BtnReplaceDocument.Size = New System.Drawing.Size(23, 22)
        Me.BtnReplaceDocument.Tag = "BtnReplace"
        Me.BtnReplaceDocument.ToolTipText = "REEMPLAZAR DOCUMENTO"
        '
        'ButtonItem1
        '
        Me.ButtonItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonItem1.Image = Global.Zamba.Viewers.My.Resources.Resources.presentation
        Me.ButtonItem1.Name = "ButtonItem1"
        Me.ButtonItem1.Size = New System.Drawing.Size(23, 22)
        Me.ButtonItem1.Tag = "btnPreview"
        Me.ButtonItem1.ToolTipText = "ABRIR IMAGEN EN VENTANA NUEVA"
        '
        'ButtonItem2
        '
        Me.ButtonItem2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonItem2.Image = Global.Zamba.Viewers.My.Resources.Resources.zoom_out
        Me.ButtonItem2.Name = "ButtonItem2"
        Me.ButtonItem2.Size = New System.Drawing.Size(23, 22)
        Me.ButtonItem2.Tag = "Minus"
        Me.ButtonItem2.ToolTipText = "DISMINUIR ZOOM"
        '
        'ButtonItem3
        '
        Me.ButtonItem3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonItem3.Image = Global.Zamba.Viewers.My.Resources.Resources.zoom_in
        Me.ButtonItem3.Name = "ButtonItem3"
        Me.ButtonItem3.Size = New System.Drawing.Size(23, 22)
        Me.ButtonItem3.Tag = "Plus"
        Me.ButtonItem3.ToolTipText = "AUMENTAR ZOOM"
        '
        'ButtonItem4
        '
        Me.ButtonItem4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonItem4.Image = Global.Zamba.Viewers.My.Resources.Resources.lock_view
        Me.ButtonItem4.Name = "ButtonItem4"
        Me.ButtonItem4.Size = New System.Drawing.Size(23, 22)
        Me.ButtonItem4.Tag = "BtnLockZoom"
        Me.ButtonItem4.ToolTipText = "BLOQUEAR ZOOM"
        '
        'ButtonItem6
        '
        Me.ButtonItem6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonItem6.Image = Global.Zamba.Viewers.My.Resources.Resources.undo
        Me.ButtonItem6.Name = "ButtonItem6"
        Me.ButtonItem6.Size = New System.Drawing.Size(23, 22)
        Me.ButtonItem6.Tag = "Rotate_90"
        Me.ButtonItem6.ToolTipText = "ROTAR A LA IZQUIERDA"
        '
        'ButtonItem7
        '
        Me.ButtonItem7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonItem7.Image = Global.Zamba.Viewers.My.Resources.Resources.redo
        Me.ButtonItem7.Name = "ButtonItem7"
        Me.ButtonItem7.Size = New System.Drawing.Size(23, 22)
        Me.ButtonItem7.Tag = "Rotate90"
        Me.ButtonItem7.ToolTipText = "ROTAR A LA DERECHA"
        '
        'ButtonItem8
        '
        Me.ButtonItem8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonItem8.Image = Global.Zamba.Viewers.My.Resources.Resources.layout_center
        Me.ButtonItem8.Name = "ButtonItem8"
        Me.ButtonItem8.Size = New System.Drawing.Size(23, 22)
        Me.ButtonItem8.Tag = "btnTamNormal"
        Me.ButtonItem8.ToolTipText = "TAMAÑO ORIGINAL"
        '
        'ButtonItem9
        '
        Me.ButtonItem9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonItem9.Image = Global.Zamba.Viewers.My.Resources.Resources.layout_horizontal
        Me.ButtonItem9.Name = "ButtonItem9"
        Me.ButtonItem9.Size = New System.Drawing.Size(23, 22)
        Me.ButtonItem9.Tag = "BtnStrech"
        Me.ButtonItem9.ToolTipText = "AJUSTAR ANCHO"
        '
        'ButtonItem10
        '
        Me.ButtonItem10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonItem10.Image = Global.Zamba.Viewers.My.Resources.Resources.layout_vertical
        Me.ButtonItem10.Name = "ButtonItem10"
        Me.ButtonItem10.Size = New System.Drawing.Size(23, 22)
        Me.ButtonItem10.Tag = "btnAltoPantalla"
        Me.ButtonItem10.ToolTipText = "AJUSTAR ALTO"
        '
        'ButtonItem16
        '
        Me.ButtonItem16.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonItem16.Image = Global.Zamba.Viewers.My.Resources.Resources.media_beginning
        Me.ButtonItem16.Name = "ButtonItem16"
        Me.ButtonItem16.Size = New System.Drawing.Size(23, 22)
        Me.ButtonItem16.Tag = "btnfirstpage"
        Me.ButtonItem16.ToolTipText = "IR A PRIMER PAGINA"
        '
        'ButtonItem17
        '
        Me.ButtonItem17.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonItem17.Image = Global.Zamba.Viewers.My.Resources.Resources.arrow_left_blue1
        Me.ButtonItem17.Name = "ButtonItem17"
        Me.ButtonItem17.Size = New System.Drawing.Size(23, 22)
        Me.ButtonItem17.Tag = "btnBefore"
        Me.ButtonItem17.ToolTipText = "IR A PAGINA ANTERIOR"
        '
        'ButtonItem5
        '
        Me.ButtonItem5.Name = "ButtonItem5"
        Me.ButtonItem5.Size = New System.Drawing.Size(23, 4)
        '
        'btnSaveAs
        '
        Me.btnSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnSaveAs.Image = Global.Zamba.Viewers.My.Resources.Resources.disk_blue
        Me.btnSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSaveAs.Name = "btnSaveAs"
        Me.btnSaveAs.Size = New System.Drawing.Size(23, 20)
        Me.btnSaveAs.Tag = "GUARDARDOCUMENTOCOMO"
        Me.btnSaveAs.Text = "Guardar Como"
        Me.btnSaveAs.ToolTipText = "Guardar Como"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 25)
        '
        'btnAdjuntarEmail
        '
        Me.btnAdjuntarEmail.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdjuntarEmail.ForeColor = System.Drawing.Color.Black
        Me.btnAdjuntarEmail.Image = Global.Zamba.Viewers.My.Resources.Resources.mail_attachment
        Me.btnAdjuntarEmail.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAdjuntarEmail.Name = "btnAdjuntarEmail"
        Me.btnAdjuntarEmail.Size = New System.Drawing.Size(69, 20)
        Me.btnAdjuntarEmail.Tag = "EMAIL"
        Me.btnAdjuntarEmail.Text = "Envio Mail"
        Me.btnAdjuntarEmail.ToolTipText = "Enviar por mail"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 25)
        '
        'btnAdjuntarMensaje
        '
        Me.btnAdjuntarMensaje.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnAdjuntarMensaje.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdjuntarMensaje.ForeColor = System.Drawing.Color.Black
        Me.btnAdjuntarMensaje.Image = Global.Zamba.Viewers.My.Resources.Resources.mail_server
        Me.btnAdjuntarMensaje.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAdjuntarMensaje.Name = "btnAdjuntarMensaje"
        Me.btnAdjuntarMensaje.Size = New System.Drawing.Size(23, 20)
        Me.btnAdjuntarMensaje.Tag = "MENSAJE"
        Me.btnAdjuntarMensaje.Text = "Envio mensaje"
        Me.btnAdjuntarMensaje.ToolTipText = "Enviar por mensaje interno"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(6, 25)
        '
        'btnAgregarDocumentoCarpeta
        '
        Me.btnAgregarDocumentoCarpeta.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAgregarDocumentoCarpeta.ForeColor = System.Drawing.Color.Black
        Me.btnAgregarDocumentoCarpeta.Image = Global.Zamba.Viewers.My.Resources.Resources.folder_add
        Me.btnAgregarDocumentoCarpeta.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAgregarDocumentoCarpeta.Name = "btnAgregarDocumentoCarpeta"
        Me.btnAgregarDocumentoCarpeta.Size = New System.Drawing.Size(115, 20)
        Me.btnAgregarDocumentoCarpeta.Tag = "AGREGARACARPETA"
        Me.btnAgregarDocumentoCarpeta.Text = "Incorporar documento"
        Me.btnAgregarDocumentoCarpeta.ToolTipText = "Incorporar nuevo documento a la carpeta"
        '
        'ToolStripSeparator13
        '
        Me.ToolStripSeparator13.Name = "ToolStripSeparator13"
        Me.ToolStripSeparator13.Size = New System.Drawing.Size(6, 105)
        '
        'btnVerVersionesDelDocumento
        '
        Me.btnVerVersionesDelDocumento.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnVerVersionesDelDocumento.ForeColor = System.Drawing.Color.Black
        Me.btnVerVersionesDelDocumento.Image = Global.Zamba.Viewers.My.Resources.Resources.blue_documents
        Me.btnVerVersionesDelDocumento.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnVerVersionesDelDocumento.Name = "btnVerVersionesDelDocumento"
        Me.btnVerVersionesDelDocumento.Size = New System.Drawing.Size(65, 20)
        Me.btnVerVersionesDelDocumento.Tag = "VERSIONESDELDOCUMENTO"
        Me.btnVerVersionesDelDocumento.Text = "Versiones"
        Me.btnVerVersionesDelDocumento.ToolTipText = "Ver versiones del documento"
        '
        'btnAgregarUnaNuevaVersionDelDocuemto
        '
        Me.btnAgregarUnaNuevaVersionDelDocuemto.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAgregarUnaNuevaVersionDelDocuemto.ForeColor = System.Drawing.Color.Black
        Me.btnAgregarUnaNuevaVersionDelDocuemto.Image = Global.Zamba.Viewers.My.Resources.Resources.blue_document__plus
        Me.btnAgregarUnaNuevaVersionDelDocuemto.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAgregarUnaNuevaVersionDelDocuemto.Name = "btnAgregarUnaNuevaVersionDelDocuemto"
        Me.btnAgregarUnaNuevaVersionDelDocuemto.Size = New System.Drawing.Size(86, 20)
        Me.btnAgregarUnaNuevaVersionDelDocuemto.Tag = "AGREGARNUEVAVERSION"
        Me.btnAgregarUnaNuevaVersionDelDocuemto.Text = "Nueva version"
        Me.btnAgregarUnaNuevaVersionDelDocuemto.ToolTipText = "Agregar nueva version del documento"
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        Me.ToolStripSeparator12.Size = New System.Drawing.Size(6, 105)
        '
        'btnGotoWorkflow
        '
        Me.btnGotoWorkflow.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGotoWorkflow.ForeColor = System.Drawing.Color.Black
        Me.btnGotoWorkflow.Image = Global.Zamba.Viewers.My.Resources.Resources.gears_run
        Me.btnGotoWorkflow.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnGotoWorkflow.Name = "btnGotoWorkflow"
        Me.btnGotoWorkflow.Size = New System.Drawing.Size(114, 20)
        Me.btnGotoWorkflow.Tag = "IRAWORKFLOW"
        Me.btnGotoWorkflow.Text = "Trabajar con la Tarea"
        Me.btnGotoWorkflow.ToolTipText = "Trabajar con la Tarea"
        '
        'btnChangePosition
        '
        Me.btnChangePosition.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnChangePosition.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnChangePosition.ForeColor = System.Drawing.Color.Black
        Me.btnChangePosition.Image = Global.Zamba.Viewers.My.Resources.Resources.window_split_ver
        Me.btnChangePosition.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnChangePosition.Name = "btnChangePosition"
        Me.btnChangePosition.Size = New System.Drawing.Size(23, 20)
        Me.btnChangePosition.Tag = "CHANGEPOSITION"
        Me.btnChangePosition.ToolTipText = "Mostrar Listado solo o Listado y Documentos"
        '
        'ButtonItem15
        '
        Me.ButtonItem15.Name = "ButtonItem15"
        Me.ButtonItem15.Size = New System.Drawing.Size(23, 23)
        '
        'ToolBar1
        '
        Me.ToolBar1.AllowItemReorder = True
        Me.ToolBar1.BackColor = System.Drawing.Color.Silver
        Me.ToolBar1.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolBar1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolBar1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BItem2, Me.ButtonItem11, Me.BItem3, Me.LayoutButton, Me.BItem5, Me.BItemGoToSearch, Me.BItem4, Me.InfoView, Me.BtnRefresh, Me.BtnGoToWF, Me.Buttonclose2})
        Me.ToolBar1.Location = New System.Drawing.Point(0, 26)
        Me.ToolBar1.Name = "ToolBar1"
        Me.ToolBar1.Size = New System.Drawing.Size(300, 25)
        Me.ToolBar1.TabIndex = 169
        Me.ToolBar1.Visible = False
        '
        'BItem2
        '
        Me.BItem2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BItem2.Image = Global.Zamba.Viewers.My.Resources.Resources.printer
        Me.BItem2.Name = "BItem2"
        Me.BItem2.Size = New System.Drawing.Size(23, 22)
        Me.BItem2.Tag = "IMPRIMIR"
        Me.BItem2.Text = " Imprimir"
        Me.BItem2.ToolTipText = "Imprimir"
        '
        'BItem3
        '
        Me.BItem3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BItem3.Image = Global.Zamba.Viewers.My.Resources.Resources.presentation
        Me.BItem3.Name = "BItem3"
        Me.BItem3.Size = New System.Drawing.Size(23, 22)
        Me.BItem3.Tag = "PANTALLACOMPLETA"
        Me.BItem3.Text = " Pantalla Completa"
        Me.BItem3.ToolTipText = "Pantalla Completa"
        '
        'BItem5
        '
        Me.BItem5.Image = Global.Zamba.Viewers.My.Resources.Resources.document_attachment
        Me.BItem5.Name = "BItem5"
        Me.BItem5.Size = New System.Drawing.Size(95, 22)
        Me.BItem5.Tag = "VERORIGINAL"
        Me.BItem5.Text = "Ver Adjunto"
        Me.BItem5.ToolTipText = "Ver el Adjunto"
        Me.BItem5.Visible = False
        '
        'BItemGoToSearch
        '
        Me.BItemGoToSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BItemGoToSearch.Image = Global.Zamba.Viewers.My.Resources.Resources.Search
        Me.BItemGoToSearch.Name = "BItemGoToSearch"
        Me.BItemGoToSearch.Size = New System.Drawing.Size(23, 22)
        Me.BItemGoToSearch.Tag = "VERDOCUMENTAL"
        Me.BItemGoToSearch.Text = "Ver en Busqueda"
        Me.BItemGoToSearch.ToolTipText = "Ver en Busqueda"
        Me.BItemGoToSearch.Visible = False
        '
        'BItem4
        '
        Me.BItem4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BItem4.Image = Global.Zamba.Viewers.My.Resources.Resources.Settings
        Me.BItem4.Name = "BItem4"
        Me.BItem4.Size = New System.Drawing.Size(23, 22)
        Me.BItem4.Tag = "MOSTRAR"
        Me.BItem4.Text = "Barras de Herramientas"
        Me.BItem4.ToolTipText = "Mostrar Barras de Herramientas de Office"
        '
        'InfoView
        '
        Me.InfoView.Name = "InfoView"
        Me.InfoView.Size = New System.Drawing.Size(11, 22)
        Me.InfoView.Text = " "
        '
        'BtnRefresh
        '
        Me.BtnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BtnRefresh.Image = Global.Zamba.Viewers.My.Resources.Resources.refresh
        Me.BtnRefresh.Name = "BtnRefresh"
        Me.BtnRefresh.Size = New System.Drawing.Size(23, 22)
        Me.BtnRefresh.Text = "Actualizar"
        Me.BtnRefresh.ToolTipText = "Actualizar Datos"
        '
        'ToolStripContainer1
        '
        '
        'ToolStripContainer1.ContentPanel
        '
        Me.ToolStripContainer1.ContentPanel.Size = New System.Drawing.Size(761, 384)
        Me.ToolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStripContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripContainer1.Name = "ToolStripContainer1"
        Me.ToolStripContainer1.Size = New System.Drawing.Size(761, 409)
        Me.ToolStripContainer1.TabIndex = 63
        Me.ToolStripContainer1.Text = "ToolStripContainer1"
        '
        'ToolStripContainer1.TopToolStripPanel
        '
        Me.ToolStripContainer1.TopToolStripPanel.BackColor = System.Drawing.Color.Silver
        Me.ToolStripContainer1.TopToolStripPanel.Controls.Add(Me.DesignSandBar)
        Me.ToolStripContainer1.TopToolStripPanel.Controls.Add(Me.ToolBar1)
        '
        'btnGotoHelp
        '
        Me.btnGotoHelp.Name = "btnGotoHelp"
        Me.btnGotoHelp.Size = New System.Drawing.Size(23, 23)
        '
        'BtnGoToWF
        '
        Me.BtnGoToWF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BtnGoToWF.Image = Global.Zamba.Viewers.My.Resources.Resources.btnGotoWorkflow_Image
        Me.BtnGoToWF.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.BtnGoToWF.Name = "BtnGoToWF"
        Me.BtnGoToWF.Size = New System.Drawing.Size(23, 22)
        Me.BtnGoToWF.Tag = "GOTOWF"
        Me.BtnGoToWF.Text = "Tareas"
        Me.BtnGoToWF.ToolTipText = "VER DOCUMENTOS EN TAREAS"
        '
        'btnGotoWfdb
        '
        Me.btnGotoWfdb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnGotoWfdb.ForeColor = System.Drawing.Color.Transparent
        Me.btnGotoWfdb.Image = Global.Zamba.Viewers.My.Resources.Resources.btnGotoWorkflow_Image
        Me.btnGotoWfdb.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnGotoWfdb.Name = "btnGotoWfdb"
        Me.btnGotoWfdb.Size = New System.Drawing.Size(23, 23)
        Me.btnGotoWfdb.Tag = "GOTOWF"
        Me.btnGotoWfdb.Text = "ToolStripButton1"
        Me.btnGotoWfdb.ToolTipText = "VER DOCUMENTO EN TAREAS"
        '
        'UCDocumentViewer2
        '
        Me.AllowDrop = True
        Me.BackColor = System.Drawing.Color.White
        Me.CausesValidation = False
        Me.Controls.Add(Me.ToolStripContainer1)
        Me.Font = New System.Drawing.Font("Tahoma", 7.0!)
        Me.Location = New System.Drawing.Point(143, 0)
        Me.Size = New System.Drawing.Size(761, 409)
        Me.DesignSandBar.ResumeLayout(False)
        Me.DesignSandBar.PerformLayout()
        Me.ToolBar1.ResumeLayout(False)
        Me.ToolBar1.PerformLayout()
        Me.ToolStripContainer1.TopToolStripPanel.ResumeLayout(False)
        Me.ToolStripContainer1.TopToolStripPanel.PerformLayout()
        Me.ToolStripContainer1.ResumeLayout(False)
        Me.ToolStripContainer1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Atributos y eventos"
    Private Const MESSAGE_ONLY_READ As String = "Info: ¡¡Documento de solo Lectura!!"

    'OBJETOS PESADOS
    Public WithEvents ImgViewer As UCImgViewer
    Private WithEvents FormBrowser As FormBrowser
    Private WithEvents OffWB As Zamba.Browser.WebBrowser
    Private WithEvents MsgWB As Panel
    Private WithEvents Prev As FrmPreviewGold
    Dim myOutlook As Office.Outlook.OutlookInterop = Nothing
    Public ParentTabControl As TabControl
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

    'DELEGADOS
    Private Delegate Sub CloseMailItemDelegate(ByRef res As Zamba.Core.Result)
    'Se encarga de cerrar el documento.
    Delegate Sub DelegateClose()

    'VARIABLES DE TIPO BASE
    Public iCount As Integer
    Public actualFrame As Integer
    Public parentName As String
    Private ClonatedImg As Boolean
    Private _previewMode As Boolean
    Private IsNotesPushed As Boolean
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
    Private showPrint As Boolean = True
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

    'EVENTOS
    Public Event eAutomaticNewVersion(ByRef _result As Result, ByVal newResultPath As String)
    Public Event EventZoomLock(ByVal Estado As Boolean)
    Public Event Movido(ByVal Top As Int32, ByVal Left As Int32)
    Public Event Ver_Original(ByRef result As Result)
    Public Event ShowAsociatedResult(ByRef res As Result)
    Public Event LinkSelected(ByVal Result As Result)
    Public Event ShowDocumentsAsociated(ByVal Id As Integer)
    Public Event MostrarForo(ByRef Result As Result)
    Public Event CambiarDock(ByVal Sender As TabPage, ByVal closeFromCross As Boolean, ByVal isMaximize As Boolean)
    Public Event ShowAssociatedWFbyDocId(ByVal ID As Int64, ByVal DocTypeId As Int64)
    Public Event ReplaceDocument(ByRef result As IResult)
    Public Event CloseOpenTask()
    Public Event RefreshAsocTask()

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
    Public Sub New(ByRef Container As IViewerContainer, ByRef Result As Result, ByVal FlagShowDocument As Boolean, ByVal isWF As Boolean, Optional ByRef ucindex As IZControl = Nothing, Optional ByVal ShowToolBar As Boolean = True, Optional ByVal DisableInputControls As Boolean = False, Optional ByVal useVirtual As Boolean = True)
        MyBase.New()
        InitializeComponent()
        Me.ViewerContainer = Container
        Control.CheckForIllegalCrossThreadCalls = False
        Trace.WriteLineIf(ZTrace.IsVerbose, "Instancio el FrmViewer " & Now.ToString)
        Me._disableInputControls = DisableInputControls
        Me._result = Result
        Me.Text = Me._result.Name
        Me._ucIndexs = ucindex
        Me._showToolBar = ShowToolBar
        Me._isWF = isWF

        If False = UserBusiness.Rights.GetUserRights(Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.Edit, Result.DocType.ID) Then
            Me.InfoView.Text = MESSAGE_ONLY_READ
            Me.ButtonItem11.Visible = False
        Else
            Me.InfoView.Text = System.String.Empty
            'si tengo permisos de edicion muestro el boton de reemplazar documento
            Me.ButtonItem11.Visible = True
        End If
        Trace.WriteLineIf(ZTrace.IsVerbose, "Muestro el Documento " & Now.ToString)
        If FlagShowDocument Then
            ShowDocument(useVirtual, False, False, False)
        End If

        Try
            ' Si no esta instalado office 2003...
            If Not Zamba.Tools.EnvironmentUtil.getOfficePlatform = _
            Tools.EnvironmentUtil.OfficeVersions.Office2003 Or Boolean.Parse(UserPreferences.getValue("UseOcr", Sections.UserPreferences, True)) = False Then
                Me.ButtonItem14.Enabled = False
            End If
        Catch ex As Exception
            Me.ButtonItem14.Enabled = False
        End Try

        'Botones que se utilizan en Tareas para buscar el documento en la grilla de Resultado - MC
        Me.BItemGoToSearch.Visible = isWF
        Me.ButtonDocumental.Visible = isWF
        Me.Buttonclose.Visible = Not isWF
        Me.Buttonclose2.Visible = Not isWF
        Me.BtnGoToWF.Visible = Not isWF
        Me.btnGotoWfdb.Visible = Not isWF
        '[Sebastian 02-12-2009] se realizo esto para no mostrar o si la barra en la solapa documento de la opcion de ver original en 
        'la regla do show form
        Me.ToolBar1.Visible = ShowToolBar
        LoadResultRights(Result)
    End Sub
    Private ParentToolbar As ToolStrip

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
        Control.CheckForIllegalCrossThreadCalls = False
        Trace.WriteLineIf(ZTrace.IsVerbose, "Instancio el FrmViewer con result" & Now.ToString)
        Me._result = newresult
        Me.IsIndexer = True

        Trace.WriteLineIf(ZTrace.IsVerbose, "Mostrar doc la primera vez: " & flagshowdocument)
        If flagshowdocument Then
            ShowDocument()
        End If

        'Si el documento es virtual 
        Try
            If Not Zamba.Tools.EnvironmentUtil.getOfficePlatform = Tools.EnvironmentUtil.OfficeVersions.Office2003 Or Boolean.Parse(UserPreferences.getValue("UseOcr", Sections.UserPreferences, True)) = False Then
                Me.ButtonItem14.Enabled = False
            End If
        Catch ex As Exception
            Me.ButtonItem14.Enabled = False
        End Try
    End Sub
#End Region

#Region "Propiedades"
    Public Property IsMaximize() As Boolean
        Get
            Return _isMaximize
        End Get
        Set(ByVal value As Boolean)
            _isMaximize = value
            If value = True Then
                BItem3.Text = "Restaurar Pantalla"
                BItem3.ToolTipText = "Restaurar Pantalla"
            Else
                BItem3.Text = "Pantalla Completa"
                BItem3.ToolTipText = "Pantalla Completa"
            End If
        End Set
    End Property
    Public Property VerOriginalButtonVisible() As Boolean
        Get
            Return Me.BItem5.Visible
        End Get
        Set(ByVal value As Boolean)
            Me.BItem5.Visible = value
        End Set
    End Property
    Public Property PrintDocButtonVisible() As Boolean
        Get
            Return Me.BItem2.Visible
        End Get
        Set(ByVal value As Boolean)
            Me.BItem2.Visible = value
        End Set
    End Property
    Public Property PrintImgButtonVisible() As Boolean
        Get
            Return Me.BPrint.Visible
        End Get
        Set(ByVal value As Boolean)
            Me.BPrint.Visible = value
        End Set
    End Property
    Public Property GuardarComoButtonVisible() As Boolean
        Get
            Return Me.btnSaveAs.Visible
        End Get
        Set(ByVal value As Boolean)
            Me.btnSaveAs.Visible = value
        End Set
    End Property
    Public Property Estado() As Int32
        Get
            Return _Estado
        End Get
        Set(ByVal Value As Int32)
            _Estado = Value
            'Me.ImgViewer.Estado = Value
            Me.ImgViewer.PicBox2.State = DirectCast(Value, Zamba.Shapes.NetronLight.GraphControl.States)
            Select Case Value
                Case Estados.Firma
                    Me.ButtonItem12.Checked = False
                    Me.ButtonItem13.Checked = True
                    Me.ButtonItem14.Checked = False
                    Me.NETRON16.Checked = False
                    Me.ImgViewer.PicBox2.ContextMenu = Me.ImgViewer.ContextMenu1
                    Me.ImgViewer.cropX = 0
                    Me.ImgViewer.cropY = 0
                    Me.ImgViewer.cropWidth = 0
                    Me.ImgViewer.cropHeight = 0
                    Me.MenuButtonItem7.Enabled = False
                Case Estados.Netron
                    Me.ButtonItem12.Checked = False
                    Me.ButtonItem13.Checked = False
                    Me.ButtonItem14.Checked = False
                    Me.NETRON16.Checked = True
                    Me.ImgViewer.PicBox2.ContextMenu = DirectCast(Me.ImgViewer.MenuNetron, ContextMenu)
                    Me.ImgViewer.cropX = 0
                    Me.ImgViewer.cropY = 0
                    Me.ImgViewer.cropWidth = 0
                    Me.ImgViewer.cropHeight = 0
                    Me.MenuButtonItem7.Enabled = True
                Case Estados.Ninguno
                    Me.ButtonItem12.Checked = False
                    Me.ButtonItem13.Checked = False
                    Me.ButtonItem14.Checked = False
                    Me.NETRON16.Checked = False
                    Me.ImgViewer.PicBox2.ContextMenu = Nothing
                    Me.ImgViewer.cropX = 0
                    Me.ImgViewer.cropY = 0
                    Me.ImgViewer.cropWidth = 0
                    Me.ImgViewer.cropHeight = 0
                    Me.MenuButtonItem7.Enabled = False
                Case Estados.Nota
                    Me.ButtonItem12.Checked = True
                    Me.ButtonItem13.Checked = False
                    Me.ButtonItem14.Checked = False
                    Me.NETRON16.Checked = False
                    Me.ImgViewer.PicBox2.ContextMenu = Me.ImgViewer.ContextMenu1
                    Me.ImgViewer.cropX = 0
                    Me.ImgViewer.cropY = 0
                    Me.ImgViewer.cropWidth = 0
                    Me.ImgViewer.cropHeight = 0
                    Me.MenuButtonItem7.Enabled = False
                Case Estados.OCR
                    Me.ButtonItem12.Checked = False
                    Me.ButtonItem13.Checked = False
                    Me.ButtonItem14.Checked = True
                    Me.NETRON16.Checked = False
                    Me.ImgViewer.PicBox2.ContextMenu = Nothing
                    Me.MenuButtonItem7.Enabled = False
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
#End Region

    Private _userActionDisabledRules As New List(Of Long)

    Public Property UserActionDisabledRules() As List(Of Long)
        Get
            Return _userActionDisabledRules
        End Get
        Set(ByVal value As List(Of Long))
            _userActionDisabledRules = value
            If Not FormBrowser Is Nothing Then
                FormBrowser.UserActionDisabledRules = Me.UserActionDisabledRules
            End If
        End Set
    End Property

#Region "Show"
    ''' <summary>
    ''' Muestra el documento en el WebBrowser
    ''' </summary>
    ''' <param name="useVirtual">Habilitar formularios virtuales</param>
    ''' <remarks></remarks>
    Public Sub ShowDocument(Optional ByVal useVirtual As Boolean = True, Optional ByVal _isAsocTask As Boolean = False, Optional ByVal InsertedDoc As Boolean = False, Optional ByVal ComeFromWF As Boolean = False)
        Try
            Trace.WriteLineIf(ZTrace.IsVerbose, "Entrando a la visualizacion del documento")
            Me.ShowImage(useVirtual, InsertedDoc, ComeFromWF)

            'Especifica si el documento proviene de una tarea asociada.
            isAsocTask = _isAsocTask

            UserBusiness.Rights.SaveAction(Result.ID, Zamba.Core.ObjectTypes.Documents, Zamba.Core.RightsType.View, Result.Name)
            NewsBusiness.SetRead(Result.DocTypeId, Result.ID)

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
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

        If (VariablesInterReglas.ContainsKey("AsociatedDocIdForPreview") = False) Then
            Me.ShowFormOfDOShowForm()
        Else
            Me.ShowAssociatedFormOfDOShowForm()
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
    Public Sub ShowDocumentFromDoShowForm(ByVal formId As Int64)
        loadDocFromDoShowForm = True
        formIdFromDoShowForm = formId
        ShowDocumentSinceRuleDOShowForm(formId)
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
                LoadFormBrowser()
                Me.DesignSandBar.Visible = False
                Me.ToolBar1.Visible = True
                Me.BtnRefresh.Visible = True

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
            Me.ToolBar1.Items.AddRange(New ToolStripItem() {Me.btnSaveAs, Me.ToolStripSeparator7, Me.btnAdjuntarEmail, Me.ToolStripSeparator8, Me.btnAdjuntarMensaje, Me.ToolStripSeparator9, Me.btnAgregarDocumentoCarpeta, Me.ToolStripSeparator13, Me.btnVerVersionesDelDocumento, Me.btnAgregarUnaNuevaVersionDelDocuemto, Me.ToolStripSeparator12, Me.btnGotoWorkflow, Me.btnChangePosition})
        Else
            Me.DesignSandBar.Items.AddRange(New ToolStripItem() {Me.btnSaveAs, Me.ToolStripSeparator7, Me.btnAdjuntarEmail, Me.ToolStripSeparator8, Me.btnAdjuntarMensaje, Me.ToolStripSeparator9, Me.btnAgregarDocumentoCarpeta, Me.ToolStripSeparator13, Me.btnVerVersionesDelDocumento, Me.btnAgregarUnaNuevaVersionDelDocuemto, Me.ToolStripSeparator12, Me.btnGotoWorkflow, Me.btnChangePosition})
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
        ResultsAsociated = DocAsociatedBusiness.getAsociatedResultsFromResult(Result, Int32.Parse(UserPreferences.getValue("CantidadFilas", Sections.UserPreferences, 100)))

        ' Se recorren los documentos asociados para buscar el que coincide con el form configurado en la regla
        For Each asociatedR As Result In ResultsAsociated

            ' Si la variable doc_id no está vacía
            If (VariablesInterReglas.ContainsKey("AsociatedDocIdForPreview") = True) Then

                ' Se busca el valor que contiene la variable doc_id (id del new result que se creo en el play de la regla DOCreateForm) 
                Dim id_VarDocId As Long = CType(VariablesInterReglas.Item("AsociatedDocIdForPreview"), Long)

                If (asociatedR.ID = id_VarDocId) Then
                    ' Se obtiene el formID
                    asociatedR.CurrentFormID = DocAsociatedBusiness.getAsociatedFormId(CType(asociatedR.DocType.ID, Integer))
                    AsociatedResult = asociatedR
                    Exit For
                End If

            End If

        Next

        Try

            Dim Forms() As ZwebForm = FormBusiness.GetAllForms(CType(AsociatedResult.DocType.ID, Integer))

            If Not ((IsNothing(Forms)) AndAlso (Forms.Length > 0)) Then

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

    ''' <summary>
    ''' Muestra la imagen del documento
    ''' </summary>
    ''' <param name="useVirtual">Si se desea habilitar o no los formularios virtuales</param>
    ''' <param name="InsertedDoc">Determina cuando el formulario debe visualizarse desde insercion</param>
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
    '''</history>
    ''' <remarks></remarks>
    Private Sub ShowImage(Optional ByVal useVirtual As Boolean = True, Optional ByVal InsertedDoc As Boolean = False, Optional ByVal ComeFromWF As Boolean = False)
        ''Try
        ''    Trace.WriteLineIf(ZTrace.IsVerbose, "Abro el Documento en la Base " & Now.ToString)
        ''    If Results_Business.OpenDocument(Result, UserBusiness.Rights.CurrentUser.ID) = True Then
        ''        Result.DocType.IsReadOnly = True
        ''    End If
        ''    'End If
        ''Catch ex As Exception
        ''    Zamba.Core.ZClass.raiseerror(ex)
        ''    ShowLabelError(ex.Message)
        ''    Exit Sub
        ''End Try

        Try
            If Not IsNothing(Result) Then
                If String.IsNullOrEmpty(Result.FullPath) = False Then
                    If Result.FullPath.IndexOf("aspx") <> -1 Then
                        Trace.WriteLineIf(ZTrace.IsVerbose, "Muestro el Documento aspx " & Now.ToString)
                        Me.ShowOfficeDocument(Result)
                    End If
                End If
            End If
        Catch ex As Exception
            raiseerror(ex)
        End Try
        'Si es office muestro la barra de herramientas
        Try
            If Result.IsOffice Then
                Me.BItem4.Visible = True
            Else
                Me.BItem4.Visible = False
            End If
            'Si es virtual no se guarda
            If Result.ISVIRTUAL Then
                Me.btnSaveAs.Visible = False
            Else
                Me.btnSaveAs.Visible = True
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            ShowLabelError(ex.Message)
            Exit Sub
        End Try
        'Busco WebForms
        Try
            Trace.WriteLineIf(ZTrace.IsVerbose, "Busco los Forms " & Now.ToString)

            If useVirtual = True Then
                If Result.CurrentFormID > 0 Then

                    'Dim Forms() As ZwebForm = FormBusiness.GetShowAndEditForms(CType(Result.ID, Int32))
                    'Dim Form As ZwebForm = Nothing
                    'For Each WebForm As ZwebForm In Forms
                    '    If Form.Type = FormTypes.Show Then
                    '        Form = WebForm
                    '    End If
                    'Next

                    Dim Form As ZwebForm = FormBusiness.GetForm(Result.CurrentFormID)
                    Trace.WriteLineIf(ZTrace.IsVerbose, "Cargo el form browser " & Now.ToString)
                    LoadFormBrowser()
                    Trace.WriteLineIf(ZTrace.IsVerbose, "Levanto el form browser " & Now.ToString)
                    Me.DesignSandBar.Visible = False
                    Me.ToolBar1.Visible = True
                    Me.BtnRefresh.Visible = True
                    Trace.WriteLineIf(ZTrace.IsVerbose, "Muestro el Formulario " & Now.ToString)
                    'If Form.Type <> FormTypes.Edit Then
                    '    Form.Type = FormTypes.Show
                    'End If
                    EnableToolBarButtons()
                    FormBrowser.ShowDocument(Result, Form)

                    Trace.WriteLineIf(ZTrace.IsVerbose, "Cargue el Formulario " & Now.ToString)
                    showOriginalButton(Result)
                    Exit Sub
                Else
                    '[Sebastian 11-05-09] se salvo el warning en result.doctype.id de long to int
                    Dim Forms() As ZwebForm
                    '[Pablo 12/10/2010] determina si vengo de insercion o de busqueda
                    If InsertedDoc = True Then
                        Forms = FormBusiness.GetInsertFormsByDocTypeId(Int32.Parse(Result.DocTypeId.ToString))
                        Trace.WriteLineIf(ZTrace.IsVerbose, "Cargo el form browser " & Now.ToString)
                        LoadFormBrowser()
                        Trace.WriteLineIf(ZTrace.IsVerbose, "Muestro el Formulario " & Now.ToString)
                        If Not IsNothing(Forms) AndAlso Forms.Length > 0 AndAlso FormBrowser.ShowInsertForm(Result, Forms) = True Then
                            Trace.WriteLineIf(ZTrace.IsVerbose, "Levanto el form browser " & Now.ToString)
                            Me.DesignSandBar.Visible = False
                            Me.ToolBar1.Visible = True
                            Me.BtnRefresh.Visible = True
                            Trace.WriteLineIf(ZTrace.IsVerbose, "Cargue el Formulario " & Now.ToString)
                            If Not IsNothing(Forms) Then
                                If Forms.Length > 0 Then
                                    Result.CurrentFormID = Forms(0).ID
                                End If
                            End If
                            EnableToolBarButtons()
                            showOriginalButton(Result)
                            Exit Sub
                        End If
                    Else
                        Forms = FormBusiness.GetShowAndEditForms(Int32.Parse(Result.DocTypeId.ToString))
                        If Not IsNothing(Forms) AndAlso Forms.Length > 0 Then
                            Trace.WriteLineIf(ZTrace.IsVerbose, "Cargo el form browser " & Now.ToString)
                            LoadFormBrowser()
                            Trace.WriteLineIf(ZTrace.IsVerbose, "Levanto el form browser " & Now.ToString)
                            Me.DesignSandBar.Visible = False
                            Me.ToolBar1.Visible = True
                            Me.BtnRefresh.Visible = True
                            Trace.WriteLineIf(ZTrace.IsVerbose, "Muestro el Formulario " & Now.ToString)
                            '[Pablo 12/10/2010] determina si vengo de busqueda o de tareas
                            FormBrowser.ShowDocument(Result, Forms, ComeFromWF)
                            Trace.WriteLineIf(ZTrace.IsVerbose, "Cargue el Formulario " & Now.ToString)
                            If Not IsNothing(Forms) Then
                                If Forms.Length > 0 Then
                                    Result.CurrentFormID = Forms(0).ID
                                End If
                            End If
                            EnableToolBarButtons()
                            showOriginalButton(Result)
                            Exit Sub
                        End If
                    End If
                    'LoadFormBrowser()
                    'If Not IsNothing(Forms) AndAlso Forms.Length > 0 AndAlso FormBrowser.ShowInsertForm(Result, Forms) = True Then
                    '    Trace.WriteLineIf(ZTrace.IsVerbose, "Cargo el form browser " & Now.ToString)
                    '    'LoadFormBrowser()
                    '    Trace.WriteLineIf(ZTrace.IsVerbose, "Levanto el form browser " & Now.ToString)
                    '    Me.DesignSandBar.Visible = False
                    '    Me.ToolBar1.Visible = True
                    '    Me.BtnRefresh.Visible = True
                    '    Trace.WriteLineIf(ZTrace.IsVerbose, "Muestro el Formulario " & Now.ToString)
                    '    'FormBrowser.ShowDocument(Result, Forms)
                    '    Trace.WriteLineIf(ZTrace.IsVerbose, "Cargue el Formulario " & Now.ToString)
                    '    If Not IsNothing(Forms) Then
                    '        If Forms.Length > 0 Then
                    '            Result.CurrentFormID = Forms(0).ID
                    '        End If
                    '    End If
                    '    showOriginalButton(Result)
                    '    Exit Sub
                    '    ' De lo contrario, es un formulario dinámico
                    'Else

                    'If Not String.IsNullOrEmpty(Result.AutoName) Then
                    '    Dim dynamicForm As New ZwebForm()

                    '    ' Se recupera de la propiedad Autoname del Result el nombre y id del formulario dinámico
                    '    Dim name As String = Result.AutoName.Substring(Result.AutoName.IndexOf("Name=") + 5)
                    '    dynamicForm.Name = name.Remove(name.IndexOf("Id=") - 1)
                    '    dynamicForm.ID = Int32.Parse((name.Substring(name.IndexOf("Id=") + 3)).ToString)
                    '    dynamicForm.Type = FormTypes.Show

                    '    Trace.WriteLineIf(ZTrace.IsVerbose, "Cargo el form browser " & Now.ToString)
                    '    LoadFormBrowser()
                    '    Trace.WriteLineIf(ZTrace.IsVerbose, "Levanto el form browser " & Now.ToString)
                    '    Me.DesignSandBar.Visible = False
                    '    Me.ToolBar1.Visible = True
                    '    Me.BtnRefresh.Visible = True
                    '    Trace.WriteLineIf(ZTrace.IsVerbose, "Muestro el Formulario " & Now.ToString)
                    '    FormBrowser.ShowDocument(Result, dynamicForm)
                    '    Trace.WriteLineIf(ZTrace.IsVerbose, "Cargue el Formulario " & Now.ToString)
                    '    showOriginalButton(Result)
                    '    Exit Sub
                    'End If
                    'End If
                End If
            Else
                Trace.WriteLineIf(ZTrace.IsVerbose, "Mostrar formulario electrónico deshabilitado")
            End If

            Me.BtnRefresh.Visible = False
        Catch ex As Exception
            MsgBox("No se pudo encontrar el formulario correspondiente", MsgBoxStyle.Exclamation, "Zamba Software")
            Zamba.Core.ZClass.raiseerror(ex)
            Me.BtnRefresh.Visible = False
        End Try

        If Result.IsImage Then
            Trace.WriteLineIf(ZTrace.IsVerbose, "El documento es imagen")
            Try
                Me.DesignSandBar.Visible = True
                Me.ToolBar1.Visible = False
                Trace.WriteLineIf(ZTrace.IsVerbose, "Muestro la Imagen " & Now.ToString)
                ShowGrafics()
                'Ruta de servidor de imagenes
                ServerImagesPath = IconsBusiness.GetServerImagesPath()
                ShowNotes()
                EnableToolBarButtons()
                'If Notes.Count > 0 Then
                '    RecorrerNotes(ImgViewer.PicBox.Height, ImgViewer.PicBox.Width)
                'End If
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        ElseIf Result.IsXoml Then
            Trace.WriteLineIf(ZTrace.IsVerbose, "El documento es Xoml")
            Me.ToolBar1.Visible = False
            Me.DesignSandBar.Visible = False
            Me.ShowWorkFlowDesigner(Result)
        Else
            Try
                If Result.ISVIRTUAL = False Then 'AndAlso Result.IsOffice2 Then
                    Me.ToolBar1.Visible = True
                    Me.DesignSandBar.Visible = False
                    Me.ToolBar1.SendToBack()
                    EnableToolBarButtons()
                Else
                    Trace.WriteLineIf(ZTrace.IsVerbose, "El documento virtual")
                    Me.ToolBar1.Visible = False
                    Me.DesignSandBar.Visible = False
                End If
                Trace.WriteLineIf(ZTrace.IsVerbose, "Muestro el Documento de Office " & Now.ToString)

                If Result.IsMsg Then

                    Trace.WriteLineIf(ZTrace.IsVerbose, "El documento es outlook")
                    Me.ShowMsgDocument(Result)
                Else
                    Trace.WriteLineIf(ZTrace.IsVerbose, "El documento es office")
                    Me.ShowOfficeDocument(Result)
                End If
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End If

        'Visibilidad del botón imprimir para los diferentes casos que se puedan dar.
        'Por permisos o configuración
        Me.BItem2.Visible = Boolean.Parse(Zamba.Core.UserPreferences.getValue("showPrintButtonInBrowser", Sections.UserPreferences, True))
        If Not RightsBusiness.GetUserRights(ObjectTypes.Documents, RightsType.Print) Then
            Me.BItem2.Visible = False
        End If
        'Por tipo de archivo
        Me.BItem2.Visible = Not (Result.IsPowerpoint)
        Me.BItem2.Visible = Not (Result.IsPDF)

        showPrint = Me.BItem2.Visible
        '[Tomas] 13/05/2009 - Se comenta la linea de abajo porque no deja visualizar el botón imprimir 
        '                     en el caso de que se quiera ver el documento original.
        'If showPrint = True Then BItem2.Visible = IsIndexer
        'El botón imprimir de imagen es lo mismo.
        Me.BPrint.Visible = Me.BItem2.Visible
    End Sub

    ''' <summary>
    ''' Show original button if the result has fullpath and the doctype a form asociated
    ''' </summary>
    ''' <history>Marcelo Created 26/11/09</history>
    ''' <param name="_result"></param>
    ''' <remarks></remarks>
    Private Sub showOriginalButton(ByVal _result As Result)
        If String.IsNullOrEmpty(_result.FullPath) = False Then
            Me.BItem5.Visible = True
        End If
    End Sub

#Region "Formularios Web"

    Private Sub LoadFormBrowser()

        If Not Me._ucIndexs Is Nothing Then
            FormBrowser = New FormBrowser(Me._ucIndexs)
        Else
            FormBrowser = New FormBrowser
        End If

        '[AlejandroR] 28/12/09 - Created
        FormBrowser.DisableInputControls = _disableInputControls

        RemoveHandler FormBrowser.RefreshIndexs, AddressOf RefreshIndexFromFormBrowser
        AddHandler FormBrowser.RefreshIndexs, AddressOf RefreshIndexFromFormBrowser
        RemoveHandler FormBrowser.ShowAsociatedResult, AddressOf ThrowShowAsociatedResult
        AddHandler FormBrowser.ShowAsociatedResult, AddressOf ThrowShowAsociatedResult

        'Eventos de la barra amarilla de actualizacion.
        RemoveHandler FormBrowser.showYellowPanel, AddressOf showYellowPanel
        AddHandler FormBrowser.showYellowPanel, AddressOf showYellowPanel

        'RemoveHandler ExternalVisualizer.ClosedByFormCross, AddressOf ClosedFromExternalVisualizaer
        'AddHandler ExternalVisualizer.ClosedByFormCross, AddressOf ClosedFromExternalVisualizaer

        '[Ezequiel] 08/04/09 - Created
        RemoveHandler FormBrowser.GetIndexsEvent, AddressOf Me.RefreshIndexFromBrowser
        AddHandler FormBrowser.GetIndexsEvent, AddressOf Me.RefreshIndexFromBrowser

        '[Alejandro] 20/11/09 - Created
        RemoveHandler FormBrowser.SaveDocumentVirtualForm, AddressOf Me.SaveDocumentVirtualForm
        AddHandler FormBrowser.SaveDocumentVirtualForm, AddressOf Me.SaveDocumentVirtualForm

        '[Tomas]    10/02/2010  Created
        RemoveHandler FormBrowser.RefreshAfterF5, AddressOf updateDocsAsociated
        AddHandler FormBrowser.RefreshAfterF5, AddressOf updateDocsAsociated

        RemoveHandler FormBrowser.FormCloseTab, AddressOf CloseDocument
        AddHandler FormBrowser.FormCloseTab, AddressOf CloseDocument

        '[AlejandroR] 28/04/2011 - Created
        RemoveHandler FormBrowser.RefreshTask, AddressOf fbRefreshTask
        AddHandler FormBrowser.RefreshTask, AddressOf fbRefreshTask

        '[AlejandroR] 28/04/2011 - Created
        RemoveHandler FormBrowser.ReloadAsociatedResult, AddressOf fbReloadAsociatedResult
        AddHandler FormBrowser.ReloadAsociatedResult, AddressOf fbReloadAsociatedResult

        FormBrowser.Dock = DockStyle.Fill

        'If Me._showToolBar AndAlso Not Me.ViewerContainer.ToolBar Is Nothing AndAlso Me._isWF Then
        '    Try
        '        ParentToolbar = Me.ViewerContainer.ToolBar
        '        Me.ToolStripContainer1.TopToolStripPanel.Controls.Add(ParentToolbar)
        '    Catch ex As Exception
        '        raiseerror(ex)
        '    End Try

        'End If

        Me.ToolStripContainer1.ContentPanel.Controls.Add(FormBrowser)

        Me.FormBrowser.BringToFront()

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
            raiseerror(ex)
            Return String.Empty
        End Try
        Return String.Empty
    End Function

    Public Function SaveHtmlFile() As Boolean

        Me.Result.Html = Me.GetHtml()
        Me.Result.HtmlFile = Tools.EnvironmentUtil.GetTempDir("\temp").FullName & "\" & Result.Name & "-temp" & (New Random).Next(1, 9999).ToString & ".html"

        Try
            If File.Exists(Me.Result.HtmlFile) Then
                File.Delete(Me.Result.HtmlFile)
            End If
        Catch ex As Exception

        End Try

        If File.Exists(FormBusiness.GetShowAndEditForms(Int32.Parse(Result.DocType.ID.ToString))(0).Path.Replace(".html", ".mht")) Then
            Try
                Using write As New StreamWriter(Result.HtmlFile.Substring(0, Result.HtmlFile.Length - 4) & "mht")
                    write.AutoFlush = True
                    Dim reader As New StreamReader(FormBusiness.GetShowAndEditForms(Int32.Parse(Result.DocType.ID.ToString))(0).Path.Replace(".html", ".mht"))
                    Dim mhtstring As String = reader.ReadToEnd()
                    write.Write(mhtstring.Replace("<Zamba.Html>", Result.Html))
                End Using
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Try
                File.Delete(Me.Result.HtmlFile)
            Catch ex As Exception

            End Try

            Me.Result.HtmlFile = Me.Result.HtmlFile.Substring(0, Me.Result.HtmlFile.Length - 4) & "mht"
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
        If (IsNothing(Me.ToolStripContainer1.ContentPanel.Controls("pnlPanel"))) Then

            Dim lblLabel As New Label
            lblLabel.AutoSize = True
            lblLabel.Location = New Point(5, 10)
            lblLabel.Name = "lblMessage"
            lblLabel.Size = New Size(195, 13)
            lblLabel.TabIndex = 0
            lblLabel.Text = "El Documento ha sido actualizado. Presione actualizar para volver a cargar"
            lblLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                              Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            lblLabel.Font = New Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

            Dim btnButton As New Button
            ' Se asocia al evento click del botón "Actualizar" el método updateDocsAsociated. Cuando el usuario presiona dicho botón se ejecutará
            ' el correspondiente método
            AddHandler btnButton.Click, AddressOf updateDocsAsociated
            btnButton.Location = New Point(830, 5)
            btnButton.Name = "btnUpdate"
            btnButton.Size = New Size(75, 23)
            btnButton.TabIndex = 1
            btnButton.Text = "Actualizar"
            btnButton.UseVisualStyleBackColor = True

            Dim pnlPanel As New Panel
            pnlPanel.BackColor = Color.Beige
            ' La etiqueta y el botón se agregan al panel
            pnlPanel.Controls.Add(btnButton)
            pnlPanel.Controls.Add(lblLabel)
            pnlPanel.Dock = DockStyle.Top
            pnlPanel.ForeColor = Color.Black
            pnlPanel.Location = New Point(3, 3)
            pnlPanel.Name = "pnlPanel"
            pnlPanel.Size = New Size(305, 33)
            pnlPanel.TabIndex = 0

            ' El panel se agrega a la colección de controles de UCDocumentViewer2
            Me.ToolStripContainer1.ContentPanel.Controls.Add(pnlPanel)

        End If

    End Sub

    Private Sub hideYellowPanel()
        Trace.WriteLineIf(ZTrace.IsVerbose, "Cantidad de controles en ContentPanel: " & Me.ToolStripContainer1.ContentPanel.Controls.Count)

        Me.ToolStripContainer1.ContentPanel.Controls.Remove(Me.ToolStripContainer1.ContentPanel.Controls.Find("pnlPanel", True)(0))
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
        '(pablo) - se comentan estas lineas ya que no hace falta
        'redibujar el control para refrescar el formulario.
        'Me.ToolStripContainer1.ContentPanel.Controls.Clear()
        'InitializeComponent()

        'Me._result = Results_Business.GetRefreshedResult(Me.Result)
        Try
            If loadDocFromDoShowForm Then
                ShowDocumentSinceRuleDOShowForm(formIdFromDoShowForm)
            Else
                Me.ShowImage()
            End If

            hideYellowPanel()
        Catch ex As Exception
            Trace.WriteLineIf(ZTrace.IsVerbose, ex.ToString)
        End Try

    End Sub

    Public Sub EnableForm(ByVal Enable As Boolean)
        Try
            If Not IsNothing(FormBrowser) Then
                Me.FormBrowser.Enabled = Enable
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub
    Private Sub RefreshIndexFromFormBrowser(ByVal Result As IResult)
        DirectCast(_ucIndexs, UCIndexViewer).ShowIndexs(Result, False)

    End Sub
    Private Function HasExtension(ByVal forms() As ZwebForm) As Boolean
        If Result.ISVIRTUAL = False Then
            Try
                Dim file As New FileInfo(Result.FullPath)
                Dim iExtensions As Int32
                If Not IsNothing(file) Then
                    For Each f As ZwebForm In forms
                        iExtensions = f.Extensions.Count - 1
                        If iExtensions > -1 Then
                            For i As Int32 = 0 To iExtensions
                                Try
                                    '[sebastian ]09-06-2009 se agrego cast para salvar warning
                                    Dim etype As WebFormsExtensions = DirectCast(f.Extensions(i), WebFormsExtensions)
                                    If String.Compare("." & etype.ToString, file.Extension.ToLower) = 0 Then
                                        Return True
                                    End If
                                Catch ex As Exception
                                    Zamba.Core.ZClass.raiseerror(ex)
                                End Try
                            Next
                        End If
                    Next
                End If
                Return False
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
                Return False
            End Try
        Else
            Return True
        End If
    End Function


    Private Sub FormBrowser_LinkSelected(ByVal Result As Result) Handles FormBrowser.LinkSelected
        RaiseEvent LinkSelected(Result)
    End Sub
#End Region

#Region "PictureBox"

    Private Sub LoadPicBox()
        RollBackReplace(Result)
        ImgViewer = New UCImgViewer
        ImgViewer.Dock = DockStyle.Fill
        Me.ToolStripContainer1.ContentPanel.Controls.Add(ImgViewer)
        ImgViewer.SearchInDb(Result)

        'RemoveHandler ImgViewer.PicBox.DoubleClick, AddressOf PictureBox1_DoubleClick
        'AddHandler ImgViewer.PicBox.DoubleClick, AddressOf PictureBox1_DoubleClick
        btnnotasHandlers()
        Me.ImgViewer.BringToFront()
    End Sub
    Private Sub ShowGrafics()
        Try
            LoadPicBox()

            'Permise de notas
            Try
                Me.DropDownMenuItem1.Enabled = UserBusiness.Rights.GetUserRights(Zamba.Core.ObjectTypes.Notas, Zamba.Core.RightsType.Create)
            Catch ex As Exception
                Me.DropDownMenuItem1.Enabled = False
                Zamba.Core.ZClass.raiseerror(ex)
            End Try

            'Permise de firma
            Try
                Me.ButtonItem13.Enabled = UserBusiness.Rights.GetUserRights(Zamba.Core.ObjectTypes.ModuleSignature, Zamba.Core.RightsType.Use)
            Catch ex As Exception
                ButtonItem13.Enabled = False
                Zamba.Core.ZClass.raiseerror(ex)
            End Try

            Try
                Me.BtnReplaceDocument.Visible = UserBusiness.Rights.GetUserRights(Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.Edit, Result.DocType.ID)
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
                    'Me.ImgViewer.btnfirstpage.Visible = True
                    'Me.ImgViewer.btnBefore.Visible = True
                    'Me.ImgViewer.BtnNext.Visible = True
                    'Me.ImgViewer.btnlastpage.Visible = True
                    Try
                        txtNumPag.Items.Clear()
                        For A As Int32 = 1 To iCount + 1
                            txtNumPag.Items.Add(A)
                        Next
                        lblPagin.Text = "1 DE " & iCount + 1
                        lblPagin.Visible = True

                        '  DropDownMenuItem1.Text = "NOTAS"
                        ButtonItem16.Visible = True
                        ButtonItem17.Visible = True
                        ButtonItem18.Visible = True
                        ButtonItem19.Visible = True

                        'Me.ImgViewer.Panel2.Width = 721
                        txtNumPag.Visible = True
                        ButtonItem20.Visible = True
                        lblPagin.Text = "1 DE " & iCount + 1
                        lblPagin.Visible = True
                    Catch ex As Exception
                    End Try
                    Result.Picture.Image.SelectActiveFrame(oFDimension, actualFrame)
                    LoadGoToPage()
                Else
                    'Me.ImgViewer.btnfirstpage.Visible = False
                    'Me.ImgViewer.btnBefore.Visible = False
                    'Me.ImgViewer.BtnNext.Visible = False
                    'Me.ImgViewer.btnlastpage.Visible = False
                    Try
                        'DropDownMenuItem1.Text = "NOTAS"
                        ButtonItem16.Visible = False
                        ButtonItem17.Visible = False
                        ButtonItem18.Visible = False
                        ButtonItem19.Visible = False
                        'Me.ImgViewer.Panel2.Width = 577
                        txtNumPag.Visible = False
                        ButtonItem20.Visible = False
                        lblPagin.Text = "0 DE 0"
                        lblPagin.Visible = False
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
            '[sebastian ]09-06-2009 se agrego cast para salvar el warning
            Dim res As Long = CLng(Result.Picture.Resolution)

            If Boolean.Parse(UserPreferences.getValue("LockZoom", Sections.UserPreferences, True)) Then
                Try
                    Dim fr As New IO.StreamReader(Membership.MembershipHelper.StartUpPath & "\TempZoom.txt")
                    Dim str As String
                    str = fr.ReadLine
                    res = Long.Parse(str)
                    str = fr.ReadLine
                    size.Width = Integer.Parse(str)
                    str = fr.ReadLine
                    size.Height = Integer.Parse(str)

                Catch ex As Exception
                    Trace.WriteLineIf(ZTrace.IsVerbose, ex.ToString)
                End Try
            End If

            Try
                'ImgViewer.PicBox.Size = size
                Result.Picture.Resolution = res
                'ImgViewer.PicBox.Image = Result.Picture.Image
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

            Select Case Int32.Parse(UserPreferences.getValue("InitialSize", Sections.UserPreferences, 1))
                Case UserPreferences.InitialSizes.Height
                    If Result.IsImage Then AltoPantalla()
                Case UserPreferences.InitialSizes.Width
                    If Result.IsImage Then AnchoPantalla()
            End Select

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
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
        End Try
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
        Me.ToolStripContainer1.ContentPanel.Controls.Add(OffWB)
        Me.OffWB.BringToFront()
    End Sub
    Private Sub LoadMsgBrowser()
        MsgWB = New Panel
        MsgTimer = New Timer()
        winHandle = IntPtr.Zero

        AddHandler MsgTimer.Tick, AddressOf MsgTimer_Tick
        MsgTimer.Interval = 1000
        MsgTimer.Enabled = False

        'RemoveHandler OffWB.WebBrowserError, AddressOf WebBrowserError
        'AddHandler OffWB.WebBrowserError, AddressOf WebBrowserError
        'RemoveHandler OffWB.closeBrowser, AddressOf CloseDocument
        'AddHandler OffWB.closeBrowser, AddressOf CloseDocument



        MsgWB.Dock = DockStyle.Fill
        Me.ToolStripContainer1.ContentPanel.Controls.Add(MsgWB)
        Me.MsgWB.BringToFront()
    End Sub

    Private Sub MsgTimer_Tick()

        If Me.MsgWB IsNot Nothing Then
            Dim x As IntPtr
            Dim threadId As IntPtr = IntPtr.Zero
            Dim pId As IntPtr = IntPtr.Zero
            Dim actualParent As IntPtr = IntPtr.Zero

            FindWindowByApi(myOutlook.ActiveInspectorCaption)

            If winHandle = IntPtr.Zero Then
                Trace.WriteLineIf(ZTrace.IsVerbose, "NO Se encontro Handle de la ventana " & Date.Now.ToString())
            End If
            Try
                If winHandle <> IntPtr.Zero AndAlso Not OutOfZamba AndAlso actualParent <> Me.MsgWB.Handle Then
                    If Boolean.Parse(UserPreferences.getValue("ShowMsgControlBox", Sections.ExportaPreferences, "False")) = True Then
                        SetMsgPanelAttributes()
                    End If

                    oldParent = WindowsApi.Usr32Api.GetParent(winHandle)
                    Trace.WriteLineIf(ZTrace.IsVerbose, " Obtiene el Parent : " & oldParent.ToString() & " - " & Date.Now.ToString())

                    x = WindowsApi.Usr32Api.SetParent(winHandle, Me.MsgWB.Handle)
                    Trace.WriteLineIf(ZTrace.IsVerbose, " Setea al Panel(" & Me.MsgWB.Handle.ToString() & ") como parent de la ventana " & Date.Now.ToString())

                    MsgPanelSetSize()

                    actualParent = WindowsApi.Usr32Api.GetParent(winHandle)
                    Trace.WriteLineIf(ZTrace.IsVerbose, " Obtiene el Parent ACTUAL : " & actualParent.ToString() & " - " & Date.Now.ToString())

                    Trace.WriteLineIf(ZTrace.IsVerbose, " Se ha finalizado con los seteos para la visualización " & Date.Now.ToString())

                    Me.MsgTimer.Enabled = False

                Else

                    If OutOfZamba AndAlso winHandle <> IntPtr.Zero Then
                        WindowsApi.Usr32Api.SetActiveWindow(winHandle)
                        Me.MsgTimer.Enabled = False
                    Else
                        If timOut >= 300 Then
                            Trace.WriteLineIf(ZTrace.IsVerbose, " TIMEOUT : NO SE ENCONTRO VENTANA " & Date.Now.ToString())
                            Me.MsgTimer.Enabled = False
                        Else
                            timOut = timOut + 1
                            Trace.WriteLineIf(ZTrace.IsVerbose, " TIME COUNT = " & timOut.ToString() & " " & Date.Now.ToString())
                        End If
                    End If
                End If
            Catch ex As ObjectDisposedException
                raiseerror(ex)
            Catch ex As Exception
                raiseerror(ex)
            End Try

            Trace.WriteLineIf(ZTrace.IsVerbose, " -- End sub de MsgTimer_Tick -- " & Date.Now.ToString())

        Else
            Trace.WriteLineIf(ZTrace.IsVerbose, "El panel de WebBrowser no se encontraba instanciado." & Date.Now.ToString())
            Trace.WriteLineIf(ZTrace.IsVerbose, " -- End sub de MsgTimer_Tick -- " & Date.Now.ToString())
            Me.MsgTimer.Enabled = False
        End If
        MsgPanelSetSize()
    End Sub

    Private Function FindWindowByApi(ByVal WindowCaption As String) As System.IntPtr

        If winHandle = IntPtr.Zero Then
            Trace.WriteLineIf(ZTrace.IsVerbose, "Comienza a buscar la ventana ..." & Date.Now.ToString())
            winHandle = WindowsApi.Usr32Api.FindWindow(WindowsApi.ClassNames.rctrl_renwnd32, WindowCaption)
            If winHandle = IntPtr.Zero Then winHandle = WindowsApi.Usr32Api.FindWindow(WindowsApi.ClassNames.OpusApp, WindowCaption)
            Trace.WriteLineIf(ZTrace.IsVerbose, "Se encontro Handle ( " & winHandle.ToString() & " ) el Titulo es '" & WindowCaption & "' " & Date.Now.ToString())
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
            WindowsApi.Usr32Api.GetSystemMenu(winHandle, 1)
            WindowsApi.Usr32Api.SetWindowLong(winHandle, WindowsApi.Usr32Api.GWL_STYLE, oldWinStyle)
            Trace.WriteLineIf(ZTrace.IsVerbose, " Se Restauraron atributos de ventana (Botones de cerrar, minimizar y demas) " & Date.Now.ToString())
        End If


    End Sub

    Private Sub MsgPanelSetSize()
        Dim ret As IntPtr
        Dim msgTop As Integer
        If Integer.TryParse(UserPreferences.getValue("MsgViewerTopPosition", Sections.ExportaPreferences, "0"), msgTop) = False Then
            msgTop = 0
        End If

        Dim width As Int32 = Me.MsgWB.Width
        Dim height As Int32 = Me.MsgWB.Height + msgTop


        If winHandle <> IntPtr.Zero AndAlso OutOfZamba = False Then
            ret = WindowsApi.Usr32Api.SetWindowPos(winHandle, 1, 0, -msgTop, width, height, 0)
            ret = WindowsApi.Usr32Api.ShowWindow(winHandle, WindowsApi.Usr32Api.SW_MAXIMIZE)
            Trace.WriteLineIf(ZTrace.IsVerbose, " RESIZE , DOCK " & Date.Now.ToShortTimeString())
            WindowsApi.Usr32Api.SetActiveWindow(winHandle)
        End If
    End Sub

    Private Sub ShowOfficeDocument(ByRef Result As Result)
        Try
            If UserBusiness.Rights.GetUserRights(Zamba.Core.ObjectTypes.ModuleElectronicDoc, Zamba.Core.RightsType.Use) = False Then
                Dim Proceso As System.Diagnostics.Process = New System.Diagnostics.Process
                'Proceso.Start (Result.fullpath)
                Diagnostics.Process.Start(Result.FullPath)
                MessageBox.Show("El Módulo de Documentos Electrónicos no se encuentra habilitado. Consulte con su administrador del sistema.  " & "Este Módulo le permitirá editar sus archivos desde la aplicación integrando las funcionalidades del sistema", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            If UserBusiness.Rights.GetUserRights(Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.Edit, Result.DocType.ID) Then
                If Result.HasVersion = 1 AndAlso UserBusiness.Rights.GetUserRights(Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.EditVersions, Result.DocType.ID) Then
                    Result.DocType.IsReadOnly = False
                Else
                    If Result.HasVersion = 0 Then
                        Result.DocType.IsReadOnly = False
                    Else
                        MessageBox.Show("No tiene los permisos necesarios para Editar un archivo versionado. Se abrirá en modo Solo Lectura", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Result.DocType.IsReadOnly = True
                    End If

                End If

            Else
                Result.DocType.IsReadOnly = True
            End If
            If CInt(Result.isShared) <> 1 And Result.DocTypeId <> 0 Then
                If UserBusiness.Rights.GetUserRights(UserBusiness.CurrentUser, Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.OwnerChanges, CInt(Result.DocTypeId)) AndAlso UserBusiness.CurrentUser.ID = Result.OwnerID AndAlso Result.DocType.IsReadOnly = True Then
                    Result.DocType.IsReadOnly = False
                End If
                If UserBusiness.Rights.GetUserRights(UserBusiness.CurrentUser, Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.OwnerChanges, CInt(Result.DocTypeId)) AndAlso UserBusiness.CurrentUser.ID <> Result.OwnerID AndAlso Result.DocType.IsReadOnly = False Then
                    If Not UserBusiness.Rights.DisableOwnerChanges(UserBusiness.CurrentUser, Result.DocTypeId) Then
                        Result.DocType.IsReadOnly = True
                    End If
                End If
            ElseIf CInt(Result.isShared) = 1 Then
                If Result.DocTypeId <> 0 Then
                    Result.DocType.IsReadOnly = True
                End If
            End If
            LoadWebBrowser()

            OffWB.ShowDocument(Result)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

        ''Guardo en la variable showprint si el usuario ve la impresion
        'Me.BItem2.Visible = Zamba.Core.UserPreferences.showPrintButtonInBrowser
        'Me.BItem2.Visible = Not (Result.IsPowerpoint)
        'If Not RightsBusiness.GetUserRights(IUser.ObjectTypes.Documents, IUser.RightsType.Print) Then
        '    Me.BItem2.Visible = False
        'End If
        'showPrint = Me.BItem2.Visible
        'If showPrint = True Then BItem2.Visible = IsIndexer
        'Me.BPrint.Visible = Me.BItem2.Visible

        If CInt(Result.isShared) = 1 Then
            Me.btnSaveAs.Enabled = False
        Else
            If Not RightsBusiness.GetUserRights(Zamba.Core.ObjectTypes.Documents, Zamba.Core.RightsType.Saveas) Then
                Me.btnSaveAs.Visible = False
            End If

            If RightsBusiness.GetUserRights(Zamba.Core.ObjectTypes.Documents, Zamba.Core.RightsType.Saveas) AndAlso UserBusiness.CurrentUser.ID = Result.OwnerID AndAlso Me.btnSaveAs.Enabled = False Then
                Me.btnSaveAs.Enabled = True
            End If
            If RightsBusiness.GetUserRights(Zamba.Core.ObjectTypes.Documents, Zamba.Core.RightsType.Saveas) AndAlso UserBusiness.CurrentUser.ID <> Result.OwnerID AndAlso Me.btnSaveAs.Enabled = True Then
                Me.btnSaveAs.Enabled = False
            End If

        End If

    End Sub


    ''' <summary>
    ''' Muestra el mensaje del documento
    ''' </summary>
    ''' <param name="Result"></param>
    ''' <remarks></remarks>
    Private Sub ShowMsgDocument(ByRef Result As Result)
        Try
            Trace.WriteLineIf(ZTrace.IsVerbose, "Entrando al show message")
            If CInt(Result.isShared) <> 1 And Result.DocTypeId <> 0 Then
                If UserBusiness.Rights.GetUserRights(UserBusiness.CurrentUser, Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.OwnerChanges, CInt(Result.DocTypeId)) AndAlso UserBusiness.CurrentUser.ID = Result.OwnerID AndAlso Result.DocType.IsReadOnly = True Then
                    Result.DocType.IsReadOnly = False
                End If
                If UserBusiness.Rights.GetUserRights(UserBusiness.CurrentUser, Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.OwnerChanges, CInt(Result.DocTypeId)) AndAlso UserBusiness.CurrentUser.ID <> Result.OwnerID AndAlso Result.DocType.IsReadOnly = False Then
                    If Not UserBusiness.Rights.DisableOwnerChanges(UserBusiness.CurrentUser, Result.DocTypeId) Then
                        Result.DocType.IsReadOnly = True
                    End If
                End If
            ElseIf CInt(Result.isShared) = 1 Then
                If Result.DocTypeId <> 0 Then
                    Result.DocType.IsReadOnly = True
                End If
            End If


            Select Case CheckOutlookVersionInstalled()
                Case 9
                    'parche para 2000
                    Trace.WriteLine("Versión de outlook en office 2000")
                    OpenMsgOffice2000(Result)

                Case Else
                    Trace.WriteLineIf(ZTrace.IsVerbose, "Cargando el browser de outlook")
                    LoadMsgBrowser()

                    Trace.WriteLineIf(ZTrace.IsVerbose, "Cargando el mensaje al hook")

                    HookOutlookWindow(MsgWB, Result)
            End Select


        Catch ex As Exception
            Trace.WriteLine(ex.ToString())
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

        'todo: ver si se puede deshabilitar los permisos en el control de outlook
        'If CInt(Result.isShared) = 1 Then
        '    Me.BItem1.Enabled = False
        'Else
        '    If Not RightsBusiness.GetUserRights(Zamba.Core.ObjectTypes.Documents, Zamba.Core.RightsType.Saveas) Then
        '        Me.BItem1.Visible = False
        '    End If

        '    If RightsBusiness.GetUserRights(Zamba.Core.ObjectTypes.Documents, Zamba.Core.RightsType.Saveas) AndAlso UserBusiness.CurrentUser.ID = Result.OwnerID AndAlso Me.BItem1.Enabled = False Then
        '        Me.BItem1.Enabled = True
        '    End If
        '    If RightsBusiness.GetUserRights(Zamba.Core.ObjectTypes.Documents, Zamba.Core.RightsType.Saveas) AndAlso UserBusiness.CurrentUser.ID <> Result.OwnerID AndAlso Me.BItem1.Enabled = True Then
        '        Me.BItem1.Enabled = False
        '    End If

        'End If

    End Sub

    Private Sub CloseMailItemEventHandler()

        Trace.WriteLineIf(ZTrace.IsVerbose, "UCDocumentViewer2 - CloseMailItemEventHandler")

        If Not IsNothing(myOutlook) Then

            Trace.WriteLineIf(ZTrace.IsVerbose, "Calling CloseDocument(Result)")
            Dim th As Threading.Thread = New Threading.Thread(New Threading.ParameterizedThreadStart(AddressOf CloseDocument))
            th.Start(Me.Result)
            Trace.WriteLineIf(ZTrace.IsVerbose, "Waiting 500 miliseconds")
            Threading.Thread.CurrentThread.Sleep(500)

        End If

    End Sub

    ''' <summary>
    ''' Ingresa el archivo de mensaje a la ventana
    ''' </summary>
    ''' <param name="msgwb"></param>
    ''' <param name="Result"></param>
    ''' <remarks></remarks>
    Private Sub HookOutlookWindow(ByVal msgwb As Panel, ByVal Result As IResult)

        winHandle = IntPtr.Zero
        Trace.WriteLineIf(ZTrace.IsVerbose, " ---------------MsgDocumentViewerTrace--------------- " & Date.Now.ToString())
        Trace.WriteLineIf(ZTrace.IsVerbose, " ---------------------------------------------------- ")
        OutOfZamba = Boolean.Parse(UserPreferences.getValue("OpenMsgOutOfZamba", Sections.ExportaPreferences, "False"))
        Trace.WriteLineIf(ZTrace.IsVerbose, " Abrir fuera de Zamba = " & OutOfZamba.ToString())
        Trace.WriteLineIf(ZTrace.IsVerbose, "Creando archivo temporal...")
        Trace.WriteLineIf(ZTrace.IsVerbose, "Result.FullPath: " & Result.FullPath)
        Dim Dir As System.IO.DirectoryInfo = GetTempDir("\OfficeTemp")
        Dim strPathLocal As String = Dir.FullName & Result.FullPath.Remove(0, Result.FullPath.LastIndexOf("\"))
        strPathLocal = Path.Combine(Path.GetDirectoryName(strPathLocal), Path.GetFileNameWithoutExtension(strPathLocal) & "_" & DateTime.Now.ToString("HHmmss") & Path.GetExtension(strPathLocal))

        Try
            File.Copy(Result.FullPath, strPathLocal, True)
        Catch ex As Exception
            raiseerror(ex)
        End Try

        Trace.WriteLineIf(ZTrace.IsVerbose, " Se creo Archivo temporal de " & strPathLocal & " " & Date.Now.ToString())

        myOutlook = Office.Outlook.SharedOutlook.GetOutlook()
        AddHandler myOutlook.CloseMailItemEvent, AddressOf CloseMailItemEventHandler

        Dim winState As FormWindowState
        If OutOfZamba Then
            winState = FormWindowState.Maximized
        Else
            winState = FormWindowState.Minimized
        End If

        Try
            myOutlook.OpenMailItem(strPathLocal, False, winState, Not OutOfZamba)
        Catch ex As System.Runtime.InteropServices.COMException
            If ex.Message.Contains("A dialog box is open. Close it and try again") Or ex.Message.Contains("Hay un cuadro de diálogo abierto. Ciérrelo y vuelva a intentarlo") Then
                MessageBox.Show("Error al abrir el archivo. Hay un cuadro de diálogo abierto en Outook. Ciérrelo y vuelva a intentarlo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Throw ex
            Else
                MessageBox.Show("Error al abrir el archivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Throw ex
            End If
        Catch ex As FileNotFoundException
            MessageBox.Show("No se ha encontrado el archivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        Catch ex As Exception
            MessageBox.Show("Error al abrir el archivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        End Try


        If Not OutOfZamba Then

            Me.MsgTimer.Enabled = True
            Trace.WriteLineIf(ZTrace.IsVerbose, "Timer ON")

        End If

        ''Try
        ''    If Not String.IsNullOrEmpty(Result.FullPath) Then
        ''        Trace.WriteLineIf(ZTrace.IsVerbose, " ---------------MsgDocumentViewerTrace--------------- " & Date.Now.ToString())
        ''        Trace.WriteLineIf(ZTrace.IsVerbose, " ---------------------------------------------------- ")
        ''        OutOfZamba = Boolean.Parse(UserPreferences.getValue("OpenMsgOutOfZamba", Sections.ExportaPreferences, "False"))
        ''        Trace.WriteLineIf(ZTrace.IsVerbose, " Abrir fuera de Zamba = " & OutOfZamba.ToString())

        ''        FindWindowByApi()
        ''        If winHandle <> IntPtr.Zero Then
        ''            CloseMsgDocument(True)

        ''        End If

        ''        dir = GetTempDir("\OfficeTemp")
        ''        Dim strPathLocal As String = dir.FullName & Result.FullPath.Remove(0, Result.FullPath.LastIndexOf("\"))
        ''        strPathLocal = Path.Combine(Path.GetDirectoryName(strPathLocal), Path.GetFileNameWithoutExtension(strPathLocal) & "_" & DateTime.Now.ToString("HHmmss") & Path.GetExtension(strPathLocal))

        ''        Try
        ''            File.Copy(Result.FullPath, strPathLocal, True)
        ''        Catch ex As Exception
        ''            raiseerror(ex)
        ''        End Try

        ''        Trace.WriteLineIf(ZTrace.IsVerbose, " Se creo Archivo temporal de " & strPathLocal & " " & Date.Now.ToString())

        ''        If Not IsNothing(proc) Then
        ''            closeProc()
        ''        End If
        ''        proc = New System.Diagnostics.Process()
        ''        AddHandler proc.Exited, AddressOf closeProc
        ''        proc.StartInfo.UseShellExecute = True
        ''        proc.StartInfo.FileName = strPathLocal
        ''        proc.StartInfo.WindowStyle = ProcessWindowStyle.Normal
        ''        proc.Start()
        ''        Trace.WriteLineIf(ZTrace.IsVerbose, "Process.Start()-- OK -- " & Date.Now.ToString())

        ''        If OutOfZamba AndAlso Not Result.ISVIRTUAL Then
        ''            Me.Close()
        ''            Trace.WriteLine("Se cierra UCDocumentViewer2")
        ''        Else

        ''            Me.MsgTimer.Enabled = True
        ''            Trace.WriteLineIf(ZTrace.IsVerbose, "Timer ON")

        ''        End If

        ''    Else
        ''        Trace.WriteLineIf(ZTrace.IsVerbose, "No se encontro el archivo")
        ''    End If
        ''Finally
        ''    dir = Nothing
        ''End Try
    End Sub
    Private Function closeProc()
        Try
            If Not IsNothing(proc) Then
                proc.Kill()
                proc.Dispose()
            End If
        Catch ex As Exception
            raiseerror(ex)
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
            Dir = New IO.DirectoryInfo(Membership.MembershipHelper.StartUpPath & dire)
            If Dir.Exists = False Then
                Dir.Create()
            End If
        End Try
        Return Dir
    End Function
    Private Sub WebBrowserError(ByVal Exception As Exception)
        Try
            Me.ClearUCBrowser()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "WF Designer"
    Private Sub ShowWorkFlowDesigner(ByRef Result As Result)
        Try
            If UserBusiness.Rights.GetUserRights(Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.Edit, Result.DocType.ID) Then
                Result.DocType.IsReadOnly = False
            Else
                Result.DocType.IsReadOnly = True
            End If
            LoadWorkFlowDesigner(Not (Result.DocType.IsReadOnly), Result.FullPath)
            If Not RightsBusiness.GetUserRights(Zamba.Core.ObjectTypes.Documents, Zamba.Core.RightsType.Print) Then
                Me.BItem2.Visible = False
            End If
            If Not RightsBusiness.GetUserRights(Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.Saveas, CInt(Result.DocType.ID)) Then
                Me.btnSaveAs.Visible = False
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
            Me.ToolStripContainer1.ContentPanel.Controls.Add(WFDesigner)
            Me.WFDesigner.BringToFront()
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
        Dim l As New Label
        l.Text = ex
        l.Dock = System.Windows.Forms.DockStyle.Fill
        l.BackColor = System.Drawing.Color.Transparent
        l.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        l.ForeColor = System.Drawing.Color.DarkBlue
        l.Location = New System.Drawing.Point(0, 0)
        l.Name = "Label1"
        l.Size = New System.Drawing.Size(648, 334)
        l.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolStripContainer1.ContentPanel.Controls.Add(l)
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
            If UserBusiness.Rights.GetUserRights(Zamba.Core.ObjectTypes.Notas, Zamba.Core.RightsType.View) Then
                Notes = NotesControlFactory.GetNotes(CInt(Result.ID), ServerImagesPath)
                For Each note As BaseNote In Notes
                    Me.ImgViewer.Controls.Add(note)
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
    Private Sub Img_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If IsNotesPushed OrElse Me.IsSignPushed Then
                Me.Cursor = Cursors.Cross
            Else
                Me.Cursor = Cursors.Default
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub Img_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If IsNotesPushed OrElse Me.IsSignPushed Then
                Me.Cursor = Cursors.Cross
            Else
                Me.Cursor = Cursors.Default
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
    Private Sub Img_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim mouseButton As System.Windows.Forms.MouseEventArgs = DirectCast(e, System.Windows.Forms.MouseEventArgs)
            If mouseButton.Button = Windows.Forms.MouseButtons.Left Then
                If Me.Estado = Estados.Nota Then
                    If UserBusiness.Rights.GetUserRights(Zamba.Core.ObjectTypes.Notas, Zamba.Core.RightsType.Create) Then
                        Dim NotePosition As New Drawing.Point
                        NotePosition.X = System.Windows.Forms.Cursor.Position.X
                        NotePosition.Y = System.Windows.Forms.Cursor.Position.Y
                        Dim Note As BaseNote = NotesControlFactory.NewNote(CInt(UserBusiness.Rights.CurrentUser.ID), UserBusiness.Rights.CurrentUser.Name, UserBusiness.Rights.CurrentUser.Apellidos, CInt(Result.ID), NotePosition, CBool(0))
                        Notes.Add(Note)

                        If _previewMode Then
                            NotePosition.Y -= 45
                            '                        note.Location = panelMain.Parent.PointToClient(NotePosition)
                            Note.Location = Me.PointToClient(NotePosition)
                        Else
                            NotePosition.Y -= 45
                            Note.Location = Me.Parent.PointToClient(NotePosition)
                        End If

                        Me.ImgViewer.Controls.Add(Note)
                        Note.BringToFront()
                        Me.Estado = Estados.Ninguno
                        Exit Sub
                        'Me.ImgViewer.BtnNota.Pushed = False
                    End If
                End If


                If Me.Estado = Estados.Firma Then
                    If UserBusiness.Rights.GetUserRights(Zamba.Core.ObjectTypes.ModuleSignature, Zamba.Core.RightsType.Use) Then

                        'valido que no halla otra firma del mismo usuario
                        For Each n As BaseNote In Notes
                            If n.GetType.Name = "Sign" AndAlso n.UserID = UserBusiness.Rights.CurrentUser.ID Then
                                MessageBox.Show("Usted ya posee imagen de firma en este documento", "Zamba Firma", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Me.Estado = Estados.Ninguno
                                'Me.ImgViewer.BtnFirma.Pushed = False
                                Exit Sub
                            End If
                        Next

                        'valido que tenga la firma actualizada
                        Dim firma As String = UserBusiness.GetUserSignById(CInt(UserBusiness.Rights.CurrentUser.ID))
                        If firma = "" Then
                            MessageBox.Show("Usted no posee imagen de firma", "Zamba Firma", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Me.Estado = Estados.Ninguno
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
                        If pw.ShowDialog() = Windows.Forms.DialogResult.OK Then
                            '[sebastian ] 09-06-2009 se agrego cast para salvar warning
                            Note = NotesControlFactory.NewNote(CInt(UserBusiness.Rights.CurrentUser.ID), UserBusiness.Rights.CurrentUser.Name, UserBusiness.Rights.CurrentUser.Apellidos, CInt(Result.ID), NotePosition, CBool(1), SignPath)
                            Notes.Add(Note)

                            If _previewMode Then
                                NotePosition.Y -= 45
                                Note.Location = Me.PointToClient(NotePosition)
                            Else
                                NotePosition.Y -= 45
                                Note.Location = Me.Parent.PointToClient(NotePosition)
                            End If

                            Me.ImgViewer.Controls.Add(Note)
                            RemoveHandler DirectCast(Note, Sign).DestroySign, AddressOf RemoveSign
                            AddHandler DirectCast(Note, Sign).DestroySign, AddressOf RemoveSign
                            Note.BringToFront()
                            Me.Estado = Estados.Ninguno
                            'Me.ImgViewer.BtnFirma.Pushed = False
                        Else
                            MessageBox.Show("La clave ingresada no es correcta", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.Estado = Estados.Ninguno
                            'Me.ImgViewer.BtnFirma.Pushed = False
                            Exit Sub
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Me.Estado = Estados.Ninguno
            'Me.ImgViewer.BtnFirma.Pushed = False
            'Me.ImgViewer.BtnNota.Pushed = False
        End Try
    End Sub
#End Region

#Region "ImgPreview"

    Private Sub PictureBox1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
            Me.ToolStripContainer1.ContentPanel.Controls.Add(Prev.Controls(0))
            _previewMode = False
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub FillParentFormText()
        If _previewMode Then
            Me.Text = Result.Name
            If iCount > 0 Then
                txtNumPag.Items.Clear()
                For A As Int32 = 1 To iCount + 1
                    txtNumPag.Items.Add(A)
                Next
                Me.Text += "  (" & actualFrame + 1 & " de " & iCount + 1 & ")"
            Else
                txtNumPag.Items.Clear()
            End If
        Else
            txtNumPag.Items.Clear()
            For A As Int32 = 1 To iCount + 1
                txtNumPag.Items.Add(A)
            Next
            If Result.Name.Length < 20 Then
                Me.Parent.Parent.Text = Result.Name
            Else
                Me.Parent.Parent.Text = "..." & Result.Name.Substring(Result.Name.Length - 20, 20)
            End If
            Me.Parent.Parent.Text += "  (" & actualFrame + 1 & " de " & iCount + 1 & ")"
        End If
    End Sub
#End Region

#Region "Clear"
    '    Public Sub clear2()
    '        Me.ClearUCBrowser()
    '        Me.ClearUCPicBox()
    '        Me.ClearFormBrowser()
    '        Me.ClearNotes()
    '    End Sub
    Public Sub ClearUCBrowser()
        Try
            If IsNothing(OffWB) = False Then
                Try
                    'todo: ver de no destruir el webbrowser y reusarlo
                    Me.OffWB.CloseWebBrowser(True)
                    '                Me.OffWB.Visible = False
                    '               Me.OffWB = Nothing
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                End Try
            ElseIf Not IsNothing(Me.FormBrowser) Then
                FormBrowser.CloseWebBrowser()
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    '    Private Sub ClearUCPicBox()
    '        Try
    '            If IsNothing(ImgViewer.PicBox) = False Then
    '                ImgViewer.PicBox.Image = ImgViewer.ImageList1.Images(21)
    '                ImgViewer.PicBox.SizeMode = PictureBoxSizeMode.AutoSize
    '                'PicBox.Visible = False
    '            End If
    '        Catch ex As Exception
    '           zamba.core.zclass.raiseerror(ex)
    '        End Try
    '    End Sub
    'Private Sub ClearFormBrowser()
    '    Try
    '        If IsNothing(Me.FormBrowser) = False Then
    '            Me.FormBrowser.CloseWebBrowser()
    '        End If
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '    End Try
    'End Sub
    'Private Sub ClearNotes()
    '    Try
    '        If IsNothing(Notes) = False Then
    '            For Each note As Note In Notes
    '                Me.ToolStripContainer1.ContentPanel.Controls.Remove(note)
    '                '           Notes.Remove(note)
    '                note = Nothing
    '            Next
    '            Notes.Clear()
    '        End If
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '    End Try
    'End Sub
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
                    Me.ReloadImage()
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
                    Me.Rotate(Rot)
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
                    Me.ReloadImage()
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
                    Me.Rotate(Rot)
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
                    Me.ReloadImage()
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
                    Me.Rotate(Rot)
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
                        Me.ReloadImage()
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
                        Me.Rotate(Rot)
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
                        Me.ReloadImage()
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
                        Me.Rotate(Rot)
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
            Me.ScrollControlIntoView(Me.ToolStripContainer1.ContentPanel.Controls(0))
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
            '        Dim fileTemp As New FileInfo(Membership.MembershipHelper.StartUpPath & "\TempZoom.txt")
            '        If fileTemp.Exists Then fileTemp.Delete()
            '        fileTemp = Nothing
            '        Dim fw As New IO.StreamWriter(Membership.MembershipHelper.StartUpPath & "\TempZoom.txt")
            '        fw.WriteLine(Result.Picture.Resolution)
            '        fw.WriteLine(Result.Picture.Size.Width)
            '        fw.WriteLine(Result.Picture.Size.Height)
            '        fw.Close()
            '        fw = Nothing
            '    Catch ex As Exception
            '        Trace.WriteLineIf(ZTrace.IsVerbose,ex.ToString)
            '    End Try
            'End If
            'Aumento la escala del picbox2
            Me.ImgViewer.PicBox2.zoomWidth += CSng(0.5)
            Me.ImgViewer.PicBox2.zoomHeight += CSng(0.5)
            Me.ImgViewer.Refresh()
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
            'If Result.Picture.Resolution > MinZoom Then
            '    If Result.Picture.Resolution - ZoomScale < MinZoom Then
            '        Result.Picture.Resolution = MinZoom
            '    Else
            '        Result.Picture.Resolution += -ZoomScale
            '    End If
            '    Dim Size As System.Drawing.Size = ZPicture.adjustImage(Result.Picture.Resolution, Result.Picture.Size.Height, Result.Picture.ResV, Result.Picture.Size.Width, Result.Picture.ResH)
            '    ImgViewer.PicBox.Size = Size
            '    Result.Picture.Size = Size
            '    Result.Picture.ResH = Result.Picture.Resolution
            '    Result.Picture.ResV = Result.Picture.Resolution
            '    ImgViewer.PicBox.Refresh()
            '    Me.Zoom = Result.Picture.Resolution
            '    'guardo el zoom en un temp
            '    Try
            '        Dim fileTemp As New FileInfo(Membership.MembershipHelper.StartUpPath & "\TempZoom.txt")
            '        If fileTemp.Exists Then fileTemp.Delete()
            '        fileTemp = Nothing
            '        Dim fw As New IO.StreamWriter(Membership.MembershipHelper.StartUpPath & "\TempZoom.txt")
            '        fw.WriteLine(Result.Picture.Resolution)
            '        fw.WriteLine(Result.Picture.Size.Width)
            '        fw.WriteLine(Result.Picture.Size.Height)
            '        fw.Close()
            '        fw = Nothing
            '    Catch ex As Exception
            '        MessageBox.Show(ex.ToString)
            '    End Try
            'End If
            'Me.ImgViewer.ZoomNetron()
            'Disminuyo la escala del picbox2
            If Me.ImgViewer.PicBox2.zoomWidth > 0.5 Then
                Me.ImgViewer.PicBox2.zoomWidth -= CSng(0.5)
            End If
            If Me.ImgViewer.PicBox2.zoomHeight > 0.5 Then
                Me.ImgViewer.PicBox2.zoomHeight -= CSng(0.5)
            End If
            Me.ImgViewer.Refresh()
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

            Me.ImgViewer.PicBox2.zoomWidth = 1
            Me.ImgViewer.PicBox2.zoomHeight = 1
            Me.ImgViewer.Refresh()
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
            If Me.ImgViewer.PicBox2.isTif = True Then
                Me.ImgViewer.PicBox2.zoomWidth = CSng(Me.ImgViewer.PicBox2.Width / Me.ImgViewer.PicBox2.Image.Width * 2)
                Me.ImgViewer.PicBox2.zoomHeight = Me.ImgViewer.PicBox2.zoomWidth
                Me.ImgViewer.Refresh()
            Else
                Me.ImgViewer.PicBox2.zoomWidth = CSng(Me.ImgViewer.PicBox2.Width / Me.ImgViewer.PicBox2.Image.Width)
                Me.ImgViewer.PicBox2.zoomHeight = Me.ImgViewer.PicBox2.zoomWidth
                Me.ImgViewer.Refresh()
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
            If Me.ImgViewer.PicBox2.isTif = True Then
                Me.ImgViewer.PicBox2.zoomHeight = CSng(Me.ImgViewer.PicBox2.Height / Me.ImgViewer.PicBox2.Image.Height * 2)
                Me.ImgViewer.PicBox2.zoomWidth = Me.ImgViewer.PicBox2.zoomHeight
                Me.ImgViewer.Refresh()
            Else
                Me.ImgViewer.PicBox2.zoomHeight = CSng(Me.ImgViewer.PicBox2.Height / Me.ImgViewer.PicBox2.Image.Height)
                Me.ImgViewer.PicBox2.zoomWidth = Me.ImgViewer.PicBox2.zoomHeight
                Me.ImgViewer.Refresh()
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#End Region

#Region "DELETE Result"
    Private Sub MenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
                Me.RefreshImage(Result)
                ImgViewer.Refresh()
            Else
                Exit Sub
            End If
            UserBusiness.Rights.SaveAction(Result.ID, Zamba.Core.ObjectTypes.Documents, Zamba.Core.RightsType.Edit, Result.Name)
        Catch ex As System.IO.IOException
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
            '[sebastian + 09-06-2009 se agrego cast
            Dim res As Long = CLng(Result.Picture.Resolution)

            If Boolean.Parse(UserPreferences.getValue("LockZoom", Sections.UserPreferences, True)) Then
                Try
                    Dim fr As New IO.StreamReader(Membership.MembershipHelper.StartUpPath & "\TempZoom.txt")
                    Dim str As String
                    str = fr.ReadLine
                    res = Long.Parse(str)
                    str = fr.ReadLine
                    size.Width = Integer.Parse(str)
                    str = fr.ReadLine
                    size.Height = Integer.Parse(str)

                Catch ex As Exception
                    Trace.WriteLineIf(ZTrace.IsVerbose, ex.ToString)
                End Try
            End If

            Try
                'ImgViewer.PicBox.Size = size
                Result.Picture.Resolution = res
                'ImgViewer.PicBox.Image = result.Picture.Image
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
    '    If RightFactory.GetUserRights(Zamba.Core.ObjectTypes.Documents, Zamba.Core.RightsType.Print) Then
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
    '        RightFactory.SaveAction(Result.Id, Zamba.Core.ObjectTypes.Documents, Zamba.Core.RightsType.Print, Result.Name & " Hojas Impresas: " & Me.PagesCount)
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
    Private Sub closeOfficeDocument()
        Me.OffWB.CloseOfficeDocument()
    End Sub

    Public Sub ShowToolbars()
        Me.OffWB.ShowToolbars(True)
    End Sub
#End Region

#Region "CloseDocument"

    Public Sub CloseDocument(ByVal Deletetemp As Boolean, Optional ByVal DisableAutomaticVersion As Boolean = False)
        Try
            'If IsNothing(Result) = False Then
            '    Results_Business.CloseDocument(Result.ID, CInt(UserBusiness.Rights.CurrentUser.ID))
            'End If
            If Result.IsOffice Then
                If Not IsNothing(Me.OffWB) Then
                    Me.OffWB.CloseWebBrowser(Deletetemp, DisableAutomaticVersion)
                Else
                    If Not IsNothing(Me.FormBrowser) Then
                        Me.FormBrowser.CloseWebBrowser()
                    End If
                End If
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub



#End Region

#Region "DeleteDocument"
    'private Sub DeleteDocument()
    'If ClsRights.Right(Result.DocTypeId,(Zamba.Core.ObjectTypes.DocTypes,Zamba.Core.RightsType.Delete) Then
    'If MsgBox("El Documento seleccionado se eliminará, ¿Está seguro que desea continuar?", MsgBoxStyle.YesNo, "Zamba Cliente") = MsgBoxResult.Yes Then
    '  Result.DeleteFlag = True
    '  Dim DocumentId As Int32 = Result.Id
    '  Dim DocTypeId As Int32 = Result.DocTypeId
    '  Dim FullName As String = Result.FullFileName
    '  Dim VolId As Int32 = Result.VolumeId
    '  Try
    '	 NotesFactory.DeleteNotes(Result.Id)
    '  Catch ex As Exception
    '	zamba.core.zclass.raiseerror(ex)
    '  Catch
    '  End Try
    '  Try
    '	 Me.CloseDocument()
    '  Catch ex As Exception
    '	zamba.core.zclass.raiseerror(ex)
    '  Catch
    '  End Try
    '  Try
    '	 results_factory.Delete(DocTypeId, DocumentId, FullName, VolId)
    '	 '                    results_factory.Delete(Result)
    '  Catch ex As Exception
    '	zamba.core.zclass.raiseerror(ex)
    '  End Try
    '  Try
    '	 RightFactory.SaveAction(Result.Id,(Zamba.Core.ObjectTypes.Documents,Zamba.Core.RightsType.Delete, Result.Name)
    '  Catch ex As Exception
    '	zamba.core.zclass.raiseerror(ex)
    '  End Try

    '  MsgBox("Documento eliminado con éxito", MsgBoxStyle.Exclamation, "Zamba")
    'End If
    'Else
    'MsgBox("Usted no tiene permisos para eliminar este documento", MsgBoxStyle.Critical, "Zamba Advertencia")
    'End If
    'End Sub
#End Region

#Region "PrintDocument"
    Public Sub PrintDocumentWB()
        'If Result.IsOffice Then
        Dim results(0) As Zamba.Core.IPrintable
        If Result.ISVIRTUAL = True Then
            If Not IsNothing(Me.FormBrowser) Then Me.FormBrowser.Print()
        Else
            results(0) = Result
            Dim Zp As New Zamba.Print.frmchooseprintmode(results, False)
            Zp.ShowDialog()
        End If
        'If Not IsNothing(Me.OffWB) Then Me.OffWB.PrintDocument()

        'End If
    End Sub
#End Region

#Region "SaveDocument"
    Public Sub SaveDocument()
        If Me.Result.CurrentFormID <= 0 Then
            If Not IsNothing(Me.OffWB) Then Me.OffWB.SaveDocument()
            If Not IsNothing(Me.FormBrowser) Then Me.FormBrowser.SaveDocument()
        End If
    End Sub
    Private Sub SaveAs()
        If Not IsNothing(Me.OffWB) Then Me.OffWB.SaveAsDocument()
        If Not IsNothing(Me.FormBrowser) Then Me.FormBrowser.SaveDocument()
    End Sub
    'Private Sub MenuItem1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItem1.Click
    '    SaveAs()
    '    Me.SaveDocument()
    'End Sub
#End Region

#Region "Foro"

    Private Sub btnMostrarForo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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



    'Variable que va a contener el nombre de la clase que contiene a UCDocumentViewer2
    Public Sub ClosedFromExternalVisualizaer(ByVal min As Boolean)
        If min = True Then
            IsMaximize = True
        Else
            IsMaximize = False
        End If
    End Sub

    'Public Event eSendMail(ByVal Task As ITaskResult)
    ''' <summary>
    ''' [sebastian 31-03-2009] modified It was modified to send virtuals forms by mail
    ''' </summary>
    ''' <param name="results"></param>
    ''' <remarks></remarks>

    Public Sub SendMail(ByVal Task As ITaskResult) 'FORM QUE PERMITE MANDAR UN MAIL
        If Task.ISVIRTUAL Then
            Me.SaveHtmlFile()
        End If
        If Not Result Is Nothing Then ZClass.HandleModule(ResultActions.EnvioDeMail, Result, New Hashtable)

        'RaiseEvent eSendMail(Task)
    End Sub

    Private Sub EnviarMessage(ByVal results() As Result)
        Try
            If results.Length = 0 Then
                MessageBox.Show("DEBE SELECCIONAR DOCUMENTO")
                Exit Sub
            End If
            'todoÑ poner permiso de mensaje
            If RightsBusiness.GetUserRights(ObjectTypes.Documents, RightsType.EnviarPorMail) Then

                Dim Mail As New Zamba.AdminControls.frmInternalMessageSend(CInt(UserBusiness.CurrentUser.ID), results)
                Mail.WindowState = FormWindowState.Normal
                Mail.ShowInTaskbar = True
                Mail.Show()
                'RemoveHandler Mail.MensajeEnviado, AddressOf BuscarNuevosEmails
                'AddHandler Mail.MensajeEnviado, AddressOf BuscarNuevosEmails
                Mail.Focus()
            Else
                MessageBox.Show("No tiene permisos suficientes para enviar el documento por mensajeria", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

            'Me.BuscarNuevosEmails()

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    'Public Sub BuscarNuevosEmails()
    '    Try

    '        Dim ArrayMensajes As ArrayList = GNotifierFactory.GetMensajes(UserBusiness.CurrentUser.ID)
    '        Dim Texto As String = String.Empty
    '        Dim Cantidad As Int32

    '        If Not IsNothing(ArrayMensajes) Then
    '            If ArrayMensajes.Count = 1 Then
    '                Texto = "TIENE 1 MENSAJE NUEVO EN SU BANDEJA DE MENSAJES"
    '            ElseIf ArrayMensajes.Count > 1 Then
    '                Texto = "TIENE " & Cantidad & " MENSAJES NUEVOS EN SU BANDEJA DE MENSAJES"
    '            End If
    '            If Cantidad > 0 Then
    '                RaiseInfo(Texto, "", Enums.TMsg.NO, Enums.Tinterfaz.WINFORM)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        ZClass.raiseerror(ex)
    '    End Try
    'End Sub

    Public Sub AgregarACarpeta(ByVal Result As Result)

        If RightsBusiness.GetUserRights(ObjectTypes.ModuleInsert, RightsType.Use) = False Then
            MessageBox.Show("Ud No tiene permiso para utilizar este módulo", "Zamba Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        Try
            Result.DocType.IsReindex = True
            ZClass.HandleModule(ResultActions.InsertarCarpetaLoadIndexerDelegado, Result)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    'Public Event AsocDocEvent(ByRef Result As Result)

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

    Public Sub Imprimir(ByVal results() As Result, ByVal OnlyIndexs As Boolean)
        Try
            If (results IsNot Nothing) Then
                Dim Zp As New Zamba.Print.frmchooseprintmode(results, OnlyIndexs)
                If (Zp.ShowDialog = Windows.Forms.DialogResult.OK) Then
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
            SaveFileDialog1.InitialDirectory = Membership.MembershipHelper.StartUpPath & "\"

            Dim str As String
            If (Not String.IsNullOrEmpty(_result.OriginalName)) AndAlso (Not IsNumeric(IO.Path.GetFileNameWithoutExtension(_result.OriginalName))) Then
                str = Path.GetFileName(_result.OriginalName)
            Else
                str = _result.Name
            End If
            'frm.SelectedResult.Name()

            Dim file As IO.FileInfo
            If Not Result.ISVIRTUAL Then
                file = New IO.FileInfo(Result.FullPath())
            Else
                'Dim strHtml As String
                'strHtml = Viewer.GetHtml()

                'Dim SW As New System.IO.StreamWriter(Membership.MembershipHelper.StartUpPath & "\temp\" & TaskResult.Name & ".html")
                'SW.AutoFlush = True
                'SW.Write(strHtml)
                'SW.Close()

                'file = New IO.FileInfo(Membership.MembershipHelper.StartUpPath & "\temp\" & TaskResult.Name & ".html")


                Result.Html = GetHtml()
                Result.HtmlFile = Tools.EnvironmentUtil.GetTempDir("\temp").FullName & "\" & Result.Name & "-temp" & (New Random).Next(1, 9999).ToString & ".html"

                Try
                    If System.IO.File.Exists(Result.HtmlFile) Then
                        System.IO.File.Delete(Result.HtmlFile)
                    End If
                Catch ex As Exception

                End Try

                Try
                    Using write As New StreamWriter(Result.HtmlFile.Substring(0, Result.HtmlFile.Length - 4) & "mht")
                        write.AutoFlush = True
                        Dim reader As New StreamReader(FormBusiness.GetShowAndEditForms(CInt(Result.DocType.ID))(0).Path.Replace(".html", ".mht"))
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

                file = New IO.FileInfo(Result.HtmlFile)

            End If

            ' frm.SelectedResult.FullPath)
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
            If (Not String.IsNullOrEmpty(_result.OriginalName)) AndAlso (Not IsNumeric(IO.Path.GetFileNameWithoutExtension(_result.OriginalName))) Then
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


        'GUARDAR
        If Boolean.Parse(UserPreferences.getValue("ShowSaveButton", Sections.UserPreferences, True)) = True AndAlso UserBusiness.Rights.GetUserRights(ObjectTypes.Documents, RightsType.Saveas) = True Then
            Me.btnSaveAs.Visible = True
            Me.ToolStripSeparator7.Visible = True
        Else
            Me.btnSaveAs.Visible = False
            Me.ToolStripSeparator7.Visible = False
        End If

        'ENVIAR POR EMAIL
        If Boolean.Parse(UserPreferences.getValue("ShowSendByMailButton", Sections.UserPreferences, True)) = True AndAlso UserBusiness.Rights.GetUserRights(ObjectTypes.Documents, RightsType.EnviarPorMail) = True Then
            Me.btnAdjuntarEmail.Visible = True
            Me.ToolStripSeparator8.Visible = True
        Else
            Me.btnAdjuntarEmail.Visible = False
            Me.ToolStripSeparator8.Visible = False
        End If

        ''IMPRIMIR
        'If Boolean.Parse(UserPreferences.getValue("ShowToolBarPrintButton", Sections.UserPreferences, True)) = True AndAlso UserBusiness.Rights.GetUserRights(ObjectTypes.Documents, RightsType.Print) = True Then
        '    Me.btnImprimirImagenesIndices.Visible = True
        'Else
        '    Me.btnImprimirImagenesIndices.Visible = False
        'End If




        'MENSAJES
        If Boolean.Parse(UserPreferences.getValue("ShowInternalMessagesButton", Sections.UserPreferences, True)) = True AndAlso UserBusiness.Rights.GetUserRights(ObjectTypes.Documents, RightsType.Print) = True Then
            Me.btnAdjuntarMensaje.Visible = True
            Me.ToolStripSeparator9.Visible = True
        Else
            Me.btnAdjuntarMensaje.Visible = False
            Me.ToolStripSeparator9.Visible = False
        End If


        'INCORPORAR DOCUMENTO
        If Boolean.Parse(UserPreferences.getValue("ShowAddFolderButton", Sections.UserPreferences, True)) = True Then
            Me.btnAgregarDocumentoCarpeta.Visible = True
        Else
            Me.btnAgregarDocumentoCarpeta.Visible = False
        End If

        If Result.IsPDF Then
            'todo: hay que ver porque con el boton de imprimir de zamba el PDF sale en blanco, desde windows no pasa.
            Me.BItem2.Visible = False
        End If

        Me.btnGotoWorkflow.Visible = False
        Me.btnVerVersionesDelDocumento.Visible = False
        Me.btnChangePosition.Visible = False
        Me.btnAgregarUnaNuevaVersionDelDocuemto.Visible = False
        Me.ToolStripSeparator13.Visible = False
        Me.ToolStripSeparator12.Visible = False

        'Asociados
        If UserBusiness.Rights.GetUserRights(Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.ShowAsociatedTab, Result.DocType.ID) Then
            'Me.ta.Visible = True
        Else
            'Me..Visible = False
        End If




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
                    Me.SaveAs()
                Case "IMPRIMIR"
                    Me.PrintDocumentWB()
                Case "PANTALLACOMPLETA"
                    IsMaximize = Not (IsMaximize)
                    RaiseEvent CambiarDock(Me, False, Not (IsMaximize))
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
                    If Not IsNothing(Me.OffWB) Then Me.OffWB.ShowToolbars()
                Case "Cerrar"
                    CloseTab()
                Case "VERDOCUMENTAL"
                    'LLama al metodo para mostrarlo en la grilla de resultados
                    Dim docId(0) As String
                    docId.SetValue(Me.Result.ID.ToString(), 0)
                    Zamba.Core.Search.ModDocuments.DoSearch(Me.Result.DocType.ID, docId)
                Case "VERORIGINAL"
                    'Al hacer click en ver original muestra el documento asociado
                    RaiseEvent Ver_Original(Result)

                Case "EMAIL"
                    Me.SendMail(Me.Result)

                Case "MENSAJE"
                    Dim results() As Result = {Me.Result}
                    Me.EnviarMessage(results)
                Case "IMPRIMIR"
                    Dim results() As Result = {Me.Result}
                    Imprimir(results, False)
                Case "VERSIONESDELDOCUMENTO"
                    Me.ShowVersionComment(Me.Result)
                Case "GUARDARDOCUMENTOCOMO"
                    Try
                        Me.GuardarComo(DirectCast(Me.Result, TaskResult))
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                Case "AGREGARACARPETA"
                    If Me.Result.GetType.ToString = "Zamba.Core.TaskResult" Then
                        Me.AgregarACarpeta(DirectCast(Me.Result, TaskResult))
                    Else
                        Me.AgregarACarpeta(Me.Result)
                    End If
                Case "BtnReplace"
                    Try
                        RaiseEvent replaceDocument(Result)
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                Case "GOTOWF"
                    Try
                        RaiseEvent ShowAssociatedWFbyDocId(Convert.ToInt64(Me.Result.ID.ToString()), Me.Result.DocType.ID)
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
            End Select
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub CloseTab()
        Trace.WriteLineIf(ZTrace.IsVerbose, "'Cerrar' Button was pressed")

        If Not (Me.Parent.Parent.GetType Is GetType(Zamba.Viewers.ExternalVisualizer)) _
        AndAlso String.Compare(Me.Parent.Parent.Parent.Parent.Name, "UCTaskViewer") = 0 _
        AndAlso GetTaskTabsCount() = 1 Then
            RaiseEvent CloseOpenTask()
        Else
            'Si la tarea proviene de una asociada se refresca dicha tarea
            If isAsocTask Then RaiseEvent RefreshAsocTask()
            CloseDocument(Me.Result)
            If IsMaximize = True Then
                RaiseEvent CambiarDock(Me, False, IsMaximize)
            End If
        End If
    End Sub
    Private Function GetTaskTabsCount() As Int32
        Dim count As Int32 = 0
        'For Each taskTab As TabPage In DirectCast(Me.Parent, TabControl).TabPages
        'Next
        Dim parentTab As TabControl = DirectCast(Me.Parent, TabControl)
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
                WindowsApi.Usr32Api.ShowWindow(winHandle, WindowsApi.Usr32Api.SW_HIDE)
                System.Threading.Thread.Sleep(250)
                WindowsApi.Usr32Api.SetParent(winHandle, Me.oldParent)
                'System.Threading.Thread.Sleep(250)
            End If

            'WindowsApi.Usr32Api.SendMessage(winHandle, WindowsApi.Usr32Api.WM_CLOSE, 0, 0)
            winHandle = IntPtr.Zero
            'System.Threading.Thread.Sleep(1000)
            Trace.WriteLineIf(ZTrace.IsVerbose, " Se cerro la ventana")
            Trace.WriteLineIf(ZTrace.IsVerbose, " -------------MsgDocumentViewer CERRADO-------------- " & Date.Now.ToString())
            Trace.WriteLineIf(ZTrace.IsVerbose, " ---------------------------------------------------- ")
        End If

    End Sub

    Public Sub HideButtons()
        Me.GuardarComoButtonVisible = False
        Me.PrintImgButtonVisible = False
        Me.PrintDocButtonVisible = False
    End Sub
    Public Function traerDoc() As Object
        Return Me.OffWB.TraerDoc
    End Function
    Public Sub CloseDocument(ByRef Result As Result)
        Try
            Trace.WriteLineIf(ZTrace.IsVerbose, "Calling ClearUCBrowser()")
            ClearUCBrowser()
        Catch
        End Try

        Dim parentTab As TabControl = DirectCast(Me.Parent, TabControl)
        Try
            Trace.WriteLineIf(ZTrace.IsVerbose, "Removing Tab")

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

            Me.Dispose()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
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
            DirectCast(Sender, Windows.Forms.Panel).Controls(0).Top = -1
            DirectCast(Sender, Windows.Forms.Panel).Controls(0).Left = DirectCast(Sender, Windows.Forms.Panel).Width - DirectCast(Sender, Windows.Forms.Panel).Controls(0).Width + 1
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
                Me.ToolStripContainer1.ContentPanel.Controls.Add(ZControl_PanelLinks)
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
            RemoveHandler L.Click, AddressOf MostrarPos
            AddHandler L.Click, AddressOf MostrarPos
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
    Private Sub MostrarPos(ByVal sender As Object, ByVal e As EventArgs)
        Try
            DirectCast(DirectCast(sender, LinkLabel).Tag, Panel).Width = 250
            RaiseEvent MostrarPosEvent()
            'ZControl_Splitter.Visible = True
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Sub ZControl_Show(ByVal MyZControl As ZControl, ByVal Posicion As Posiciones, ByVal Name As String, ByVal Visible As Boolean)
        Try
            If IsNothing(MyZControl) Then Exit Sub
            '[Ezequiel] - Si el tipo de zcontrol es UnIndexViewer entonces los igualo a la variable local
            If TypeOf MyZControl Is UCIndexViewer Then
                Me._ucIndexs = MyZControl
                '[Ezequiel] - En caso del que formbrowser ya este instanciado le le paso el control por el metodo AssociateIndexViewer
                If Not Me.FormBrowser Is Nothing Then Me.FormBrowser.AssociateIndexViewer(Me._ucIndexs)
            End If

            If Boolean.Parse(UserPreferences.getValue("ShowIndexLinkUnderTask", Sections.UserPreferences, "False") = True) Then
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
                MyZControl.Dock = DockStyle.Fill
                P.Controls.Add(MyZControl)
                P.Controls.Add(B)
                Me.ZControl_Panels.Add(Name, P)
                RemoveHandler P.SizeChanged, AddressOf ZControl_PanelSizeChanged
                AddHandler P.SizeChanged, AddressOf ZControl_PanelSizeChanged

                Dim ZControl_Splitter As New Splitter
                ZControl_Splitter.BackColor = Color.Blue

                If Not String.IsNullOrEmpty(Name) Then ShowZControl_PanelLinks(P, Name)

                Select Case Posicion
                    Case Posiciones.Abajo
                        P.Dock = DockStyle.Bottom
                        Me.ToolStripContainer1.ContentPanel.Controls.Add(P)
                        P.BringToFront()
                        P.Focus()
                        ZControl_Splitter.Dock = DockStyle.Bottom
                        ZControl_Splitter.Height = 2
                        Me.ToolStripContainer1.ContentPanel.Controls.Add(DirectCast(ZControl_Splitter, Windows.Forms.Control))
                        ZControl_Splitter.BringToFront()
                        Me.BringToFront()
                    Case Posiciones.Arriba
                        P.Dock = DockStyle.Top
                        Me.ToolStripContainer1.ContentPanel.Controls.Add(P)
                        P.BringToFront()
                        P.Focus()
                        ZControl_Splitter.Dock = DockStyle.Top
                        ZControl_Splitter.Height = 2
                        Me.ToolStripContainer1.ContentPanel.Controls.Add(DirectCast(ZControl_Splitter, System.Windows.Forms.Control))
                        ZControl_Splitter.BringToFront()
                        Me.BringToFront()
                    Case Posiciones.Derecha
                        P.Dock = DockStyle.Right
                        Me.ToolStripContainer1.ContentPanel.Controls.Add(P)
                        P.BringToFront()
                        P.Focus()
                        ZControl_Splitter.Dock = DockStyle.Right
                        ZControl_Splitter.Width = 2
                        Me.ToolStripContainer1.ContentPanel.Controls.Add(DirectCast(ZControl_Splitter, Windows.Forms.Control))
                        ZControl_Splitter.BringToFront()
                        Me.BringToFront()
                    Case Posiciones.Izquierda
                        P.Dock = DockStyle.Left
                        Me.ToolStripContainer1.ContentPanel.Controls.Add(P)
                        P.BringToFront()
                        P.Focus()
                        ZControl_Splitter.Dock = DockStyle.Left
                        ZControl_Splitter.Width = 2
                        Me.ToolStripContainer1.ContentPanel.Controls.Add(DirectCast(ZControl_Splitter, Windows.Forms.Control))
                        ZControl_Splitter.BringToFront()
                        Me.BringToFront()
                End Select

                B.Top = -1
                B.Left = P.Width - B.Width + 1
                B.BringToFront()
                If Visible = False Then ZControl_Close(B, New EventArgs)
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
            'Dim Hold As Integer
            'Dim Wold As Integer
            'Try
            '    Hold = ImgViewer.PicBox.Height
            '    Wold = ImgViewer.PicBox.Width
            'Catch
            'End Try
            Select Case CStr(e.ClickedItem.Tag)
                Case "IR"
                    If IsNumeric(Me.txtNumPag.Text) = True Then
                        GotoPage(CInt(Val(Me.txtNumPag.Text.Trim)))
                    Else
                        Me.txtNumPag.Text = String.Empty
                    End If
                Case "Plus"
                    If Result.IsImage Then
                        Try
                            ' If IsNothing(BirdMegaView) = False Then BirdMegaView.Dispose()
                            If ZoomLock = False Then
                                ZoomPlus()
                            End If
                            'Me.RecorrerNotes(Hold, Wold)
                        Catch ex As Exception
                            Zamba.Core.ZClass.raiseerror(ex)
                        End Try
                    End If
                Case "Minus"
                    If Result.IsImage Then
                        Try
                            ' If IsNothing(BirdMegaView) = False Then BirdMegaView.Dispose()
                            If ZoomLock = False Then
                                ZoomMinus()
                            End If
                            'Me.RecorrerNotes(Hold, Wold)
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
                            Me.AnchoPantalla()
                            'Me.RecorrerNotes(Hold, Wold)
                        Catch ex As Exception
                            Zamba.Core.ZClass.raiseerror(ex)
                        End Try
                    End If
                Case "btnAltoPantalla"
                    If Result.IsImage Then
                        Try
                            Me.AltoPantalla()
                            'Me.RecorrerNotes(Hold, Wold)
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
                    Me.Estado = Estados.OCR
                Case "BtnNetron"
                    Me.Estado = Estados.Netron
                    ImgViewer.MenuAddNetron_Click(Me, New EventArgs)
                Case "BtnNota"
                    If Result.IsImage Then
                        Try
                            Me.Estado = Estados.Nota
                            'If IsNotesPushed Then
                            '    IsNotesPushed = 0
                            '    ImgViewer.BtnNota.Image = ImgViewer.PictureBox1.Image
                            '    'Me.ImgViewer.BtnNota.Pushed = 0
                            'Else
                            '    IsNotesPushed = 1
                            '    ImgViewer.BtnNota.Image = ImgViewer.PictureBox2.Image
                            '    'Me.ImgViewer.BtnNota.Pushed = 1
                            'End If
                        Catch ex As Exception
                            Zamba.Core.ZClass.raiseerror(ex)
                        End Try
                    End If
                Case "BtnFirma"
                    If Result.IsImage Then
                        Try
                            If Not String.IsNullOrEmpty(UserBusiness.Rights.CurrentUser.firma) Then

                                Me.Estado = Estados.Firma

                                'If IsSignPushed Then
                                '    IsSignPushed = 0
                                '    'Me.ImgViewer.BtnFirma.Pushed = 0
                                '    ImgViewer.BtnFirma.Image = ImgViewer.PictureBox3.Image
                                'Else
                                '    IsSignPushed = 1
                                '    ImgViewer.BtnFirma.Image = ImgViewer.PictureBox4.Image
                                '    'Me.ImgViewer.BtnFirma.Pushed = 1
                                'End If
                            Else
                                Me.Estado = Estados.Ninguno
                                MessageBox.Show("Usted no posee imagen de firma", "Zamba Firma", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            End If
                        Catch ex As Exception
                            Zamba.Core.ZClass.raiseerror(ex)
                        End Try
                    End If
                Case "BtnLockZoom"
                    If Result.IsImage Then
                        Try
                            If ZoomLock Then
                                Me.ZoomLock = False
                                'Me.ImgViewer.BtnLockZoom.Pushed = True
                                RaiseEvent EventZoomLock(False)
                            Else
                                Me.ZoomLock = True
                                'Me.ImgViewer.BtnLockZoom.Pushed = False
                                RaiseEvent EventZoomLock(True)
                            End If
                        Catch ex As Exception
                            Zamba.Core.ZClass.raiseerror(ex)
                        End Try
                    End If
                Case "btnfirstpage"
                    Me.FirstPage()
                Case "btnBefore"
                    Me.PreviusPage()
                Case "BtnNext"
                    Me.NextPage()
                Case "btnlastpage"
                    Me.LastPage()
                Case "IMPRIMIR"
                    Me.PrintDocumentWB()
                Case "btnPreview"
                    '[Tomas] Se comenta porque no debería ocultarse o mostrarse el botón al maximizar
                    'If showPrint = True And IsIndexer = False Then
                    '    Me.BPrint.Visible = Not (Me.BPrint.Visible)
                    'End If
                    IsMaximize = Not (IsMaximize)
                    RaiseEvent CambiarDock(Me, False, IsMaximize)
                    'Me.SubCambiarDock()
                    'Case "BtnHistory"
                    '    Try
                    '        ShowDocumentHistory(Result)
                    '    Catch ex As Exception
                    '       zamba.core.zclass.raiseerror(ex)
                    '    End Try

                Case "BtnReplace"
                    Try
                        RaiseEvent ReplaceDocument(Result)
                    Catch ex As Exception
                        Zamba.Core.ZClass.raiseerror(ex)
                    End Try
                    '    Case "BtnBirdView"
                    '       ShowBirdMegaView()
                Case "BtnNetron"
                    Me.ImgViewer.MenuAddNetron_Click(Me, New EventArgs)
                Case "Cerrar"
                    If String.Compare(Me.Parent.Parent.Parent.Parent.Name, "UCTaskViewer") = 0 _
                    AndAlso GetTaskTabsCount() = 1 Then
                        RaiseEvent CloseOpenTask()
                    Else
                        CloseImage()
                    End If

                    ' RaiseEvent formclose(Me)
                Case "btnPreview"
                Case "VerDocumental"
                    'LLama al metodo para mostrarlo en la grilla de resultados
                    Dim docId(0) As String
                    docId.SetValue(Me.Result.ID.ToString(), 0)
                    Zamba.Core.Search.ModDocuments.DoSearch(Me.Result.DocType.ID, docId)





                Case "EMAIL"
                    Me.SendMail(Me.Result)

                Case "MENSAJE"
                    Dim results() As Result = {Me.Result}
                    Me.EnviarMessage(results)
                Case "IMPRIMIR"
                    Dim results() As Result = {Me.Result}
                    Imprimir(results, False)
                Case "VERSIONESDELDOCUMENTO"
                    Me.ShowVersionComment(Me.Result)
                Case "GUARDARDOCUMENTOCOMO"
                    Try
                        Me.GuardarComo(DirectCast(Me.Result, TaskResult))
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                Case "AGREGARACARPETA"
                    Me.AgregarACarpeta(DirectCast(Me.Result, TaskResult))
                Case "GOTOWF"
                    Try
                        RaiseEvent ShowAssociatedWFbyDocId(Convert.ToInt64(Me.Result.ID.ToString()), Me.Result.DocType.ID)
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
            End Select

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub


    Private Sub CloseImage()
        Dim parentTab As TabControl
        Dim fakeTabPage As TabPage

        Try
            parentTab = DirectCast(Me.Parent, TabControl)

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

    Public Sub CloseDocument()
        Try
            ClearUCBrowser()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        'Try
        '    '            If CleanIndex Then UCIndexViewer.ClearIndexs()
        'Catch
        'End Try
        'Try
        '    If Not IsNothing(Result) Then
        '        Results_Business.CloseDocument(Result.ID, CInt(UserBusiness.Rights.CurrentUser.ID))
        '    End If
        'Catch
        'End Try

        Try
            'Se aplica delegados para no generar errores al trabajar con excel catcher.
            Me.Invoke(New DelegateClose(AddressOf Close))
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
        DirectCast(Me.Parent, TabControl).TabPages.Remove(Me)
        MyBase.Dispose()
        Me.Dispose()
    End Sub

    Private Sub MenuButtonItem7_Activate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuButtonItem7.Click
        ImgViewer.MenuAddNetron_Click(Me, New EventArgs)
    End Sub

    Private Sub ButtonItem12_Activate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonItem12.Click
        Me.Estado = 3
    End Sub
    Private Sub ButtonItem13_Activate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonItem13.Click
        Me.Estado = 4
    End Sub
    Private Sub ButtonItem14_Activate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonItem14.Click
        Me.Estado = 1
    End Sub
    Private Sub NETRON16_Activate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NETRON16.Click
        Me.Estado = 2
    End Sub
    Public Event RefreshTask(ByVal Task As ITaskResult)
    Public Event ReloadAsociatedResult(ByVal AsociatedResult As Core.Result)
    Private Sub BtnRefresh_Activate_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        '(pablo) 27022011
        If Result.GetType.FullName.ToString = "Zamba.Core.Result" Then
            RaiseEvent ReloadAsociatedResult(Result)
        Else
            RaiseEvent RefreshTask(Result)
        End If
        RefreshData()
    End Sub
    Public Sub RefreshData()
        Try
            If IsNothing(Me.FormBrowser) = False Then
                Me.FormBrowser.RefreshData(Result)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Lanza evento para refrescar indices del formbrowser
    ''' </summary>
    ''' <param name="indexs"></param>
    ''' <remarks></remarks>
    ''' <history> [Ezequiel] 08/04/09 - Created
    Private Sub RefreshIndexFromBrowser(ByRef indexs As ArrayList)
        RaiseEvent GetIndexsEvent(indexs)
    End Sub

    Private Sub MsgWB_HandleDestroyed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MsgWB.HandleDestroyed

        If Result.IsMsg Then
            CloseMsgDocument()
        End If
    End Sub

    Private Sub MsgWB_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MsgWB.SizeChanged
        MsgPanelSetSize()
    End Sub

    Private Sub UCDocumentViewer2_HandleDestroyed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.HandleDestroyed
        Try
            If Not IsNothing(myOutlook) Then
                If Not myOutlook.closeFromControlbox Then
                    myOutlook.DisposingParent = True
                    myOutlook.CloseMailItem()
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

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

    '        Trace.WriteLineIf(ZTrace.IsVerbose, " Se cambiaron atributos de ventana (Botones de cerrar, minimizar y demas) " & Date.Now.ToString())
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
                Trace.WriteLine("Versión de outlook en office 97")
                Return 8
            End If
            If Regex.IsMatch(stXLVersion, "9") Then
                Trace.WriteLine("Versión de outlook en office 2000")
                Return 9
            End If
            If Regex.IsMatch(stXLVersion, "10") Then
                Trace.WriteLine("Versión de outlook en office 2002")
                Return 10
            End If
            If Regex.IsMatch(stXLVersion, "11") Then
                Trace.WriteLine("Versión de outlook en office 2003")
                Return 11
            End If
            If Regex.IsMatch(stXLVersion, "12") Then
                Trace.WriteLine("Versión de outlook en office 2007")
                Return 12
            End If
            If Regex.IsMatch(stXLVersion, "13") Then
                Trace.WriteLine("Versión de outlook en office 2010")
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
            'MsgBox("Debug: Abrir Externamente con Registry")
            Trace.WriteLine("OpenExternOfficeDocument - Documento office abierto por fuera")

            Dim rk As RegistryKey
            Dim fi As New IO.FileInfo(file)

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
                            Dim finf As New IO.FileInfo(path & exec)

                            If finf.Exists = True Then
                                finf = New IO.FileInfo(file)
                                If finf.Exists = True Then

                                    Dim command As String
                                    If fi.Extension.ToLower() = ".msg" Then
                                        'El msg se abre con la ruta directa ya que sino lo abre como adjunto de un msg nuevo
                                        command = Chr(34) & file & Chr(34)
                                    Else
                                        command = Chr(34) & path & exec & Chr(34) & " " & Chr(34) & file & Chr(34)
                                    End If
                                    Trace.WriteLine("command: " & command)

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
            raiseerror(ex)
        End Try

        Trace.WriteLineIf(ZTrace.IsVerbose, " Se creo Archivo temporal de " & strPathLocal & " " & Date.Now.ToString())

        Me.OpenExternOfficeDocument(strPathLocal)

    End Function

End Class
