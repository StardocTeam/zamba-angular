Imports Zamba.Core.WF.WF
Imports Zamba.Core.DocTypes.DocAsociated
Imports Zamba.Core.Enumerators
Imports Zamba.Viewers
Imports Zamba.Core
Imports Zamba.Data
Imports Zamba.AdminControls
Imports System.IO
Imports Zamba.Controls.WF.UInterface.Client
Imports System.ComponentModel
Imports Zamba.IETools

Namespace WF.TasksCtls

    Public Class UCTaskViewer
        Inherits ZControl
        Implements IViewerContainer
        Implements IMenuContextContainer
        Implements IDisposable

        Dim FlagClosingTV As Boolean = False

        Public Overloads Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If Not isDisposed Then
                Try
                    If disposing Then

                        RemoveHandler chkCloseTaskAfterDistribute.CheckedChanged, AddressOf chkCloseTaskAfterDistribute_CheckedChanged
                        RemoveHandler ZClass.eHandleModuleRuleAction, AddressOf LoadModulesRuleActions
                        'RemoveHandler WP.DoWork, AddressOf RunLoadForumMessagesCount
                        If newUcForo IsNot Nothing Then RemoveHandler newUcForo.showMailForm, AddressOf sendMailFromForum
                        RemoveHandler CboStates.SelectedIndexChanged, AddressOf CboStates_SelectedIndexChanged
                        RemoveHandler TabDoc.CambiarDock, AddressOf _CambiarDock
                        RemoveHandler TabDoc.CloseOpenTask, AddressOf CloseTaskViewer
                        RemoveHandler TabDoc.ShowDocumentsAsociated, AddressOf ActivateDocAsociated
                        RemoveHandler TabDoc.ShowAsociatedResult, AddressOf ShowAsociatedResult
                        RemoveHandler TabDoc.ReplaceDocument, AddressOf ReplaceDocument
                        RemoveHandler TabDoc.ShowOriginal, AddressOf ShowOriginal
                        RemoveHandler TabDoc.ReloadAsociatedResult, AddressOf ReloadAsociatedResult
                        RemoveHandler TabDoc.RefreshTask, AddressOf RefreshTask
                        RemoveHandler TabDoc.CambiarDock, AddressOf _CambiarDock

                        If asociatedGrid IsNot Nothing Then
                            RemoveHandler asociatedGrid.ResultDoubleClick, AddressOf ShowAsociatedResult
                            RemoveHandler asociatedGrid.CambiarNombre, AddressOf _CambiarNombre
                            RemoveHandler asociatedGrid.ExportarAExcel, AddressOf _ExportarAExcel
                            RemoveHandler asociatedGrid._RefreshGrid, AddressOf refreshGrid
                        End If

                        RemoveHandler BtnClose.Click, AddressOf BtnCloseExplorerClick
                            RemoveHandler BtnFullScreen.Click, AddressOf BtnFullScreenClick
                            RemoveHandler BtnchangeVisualization.Click, AddressOf BtnchangeVisualizationClick
                            RemoveHandler BtnShowUrl.Click, AddressOf BtnShowUrlClick


                            If components IsNot Nothing Then
                                components.Dispose()
                            End If

                        'Elimina todas las referencias que mantiene el objeto
                        'RaiseEvent ClearReferences(Me)




                        If hshRulesNames IsNot Nothing Then hshRulesNames.Clear()
                            If lista IsNot Nothing Then lista.Clear()

                            If newUcForo IsNot Nothing Then
                                newUcForo.Dispose()
                                'newUcForo = Nothing
                            End If
                            If ucHistorialEmails IsNot Nothing Then
                                ucHistorialEmails.Dispose()
                            End If
                            If blnDisposeResult = True Then
                                If TaskResult IsNot Nothing Then
                                    TaskResult.Dispose()
                                    TaskResult = Nothing
                                End If
                            End If

                            If UC_Info IsNot Nothing Then
                                UC_Info.Dispose()
                            End If
                            If UC_Help IsNot Nothing Then
                                UC_Help.Dispose()
                            End If
                            If ParamWB IsNot Nothing Then
                                RemoveHandler ParamWB.DocumentCompleted, AddressOf ParamWB_DocumentCompleted
                                RemoveHandler ParamWB.HandleDestroyed, AddressOf ParamWB_HandleDestroyed
                                RemoveHandler ParamWB.Navigated, AddressOf ParamWB_Navigated
                                RemoveHandler ParamWB.Disposed, AddressOf ParamWB_Disposed
                                ParamWB.Dispose()
                                ParamWB = Nothing
                            End If
                            If WFRS IsNot Nothing Then
                                WFRS = Nothing
                                'WFRS = Nothing
                            End If
                            If BtnchangeVisualization IsNot Nothing Then
                                BtnchangeVisualization.Dispose()
                                '    'BtnchangeVisualization = Nothing
                            End If
                            If BtnClose IsNot Nothing Then
                                BtnClose.Dispose()
                                '    'BtnClose = Nothing
                            End If
                            If BtnFullScreen IsNot Nothing Then
                                BtnFullScreen.Dispose()
                                'BtnFullScreen = Nothing
                            End If
                            If BtnShowUrl IsNot Nothing Then
                                BtnShowUrl.Dispose()
                                'BtnShowUrl = Nothing
                            End If
                            If Webform IsNot Nothing Then
                                Webform.Dispose()
                                'Webform = Nothing
                            End If
                            If SplitTopOfBrowser IsNot Nothing Then
                                SplitTopOfBrowser.Panel1.Controls.Clear()
                                SplitTopOfBrowser.Panel2.Controls.Clear()
                                SplitTopOfBrowser.Dispose()
                                'SplitTopOfBrowser = Nothing
                            End If
                            If SplitButton IsNot Nothing Then
                                SplitButton.Panel1.Controls.Clear()
                                SplitButton.Panel2.Controls.Clear()
                                SplitButton.Dispose()
                                'SplitButton = Nothing
                            End If
                            If SplitCloseButton IsNot Nothing Then
                                SplitCloseButton.Panel1.Controls.Clear()
                                SplitCloseButton.Panel2.Controls.Clear()
                                SplitCloseButton.Dispose()
                                'SplitCloseButton = Nothing
                            End If

                            If TaskViewerSplit IsNot Nothing Then
                                TaskViewerSplit.Dispose()
                                'TaskViewerSplit = Nothing
                            End If

                            If Url IsNot Nothing Then
                                Url.Dispose()
                                'Url = Nothing
                            End If
                            If lblAsignedTo IsNot Nothing Then
                                lblAsignedTo.Dispose()

                            End If


                            If PanelBottom IsNot Nothing Then
                                PanelBottom.Dispose()

                            End If
                            If LnkForo IsNot Nothing Then
                                LnkForo.Dispose()

                            End If


                            If ToolBar1 IsNot Nothing Then
                                RemoveHandler ToolBar1.ItemClicked, AddressOf ToolBar1_ButtonClick
                                ToolBar1.Dispose()
                                ToolBar1 = Nothing
                            End If
                            If btnIniciar IsNot Nothing Then
                                btnIniciar.Dispose()
                                'btnIniciar = Nothing
                            End If
                            If btnDerivar IsNot Nothing Then
                                btnDerivar.Dispose()
                                'btnDerivar = Nothing
                            End If
                            If TabControl1 IsNot Nothing Then
                                RemoveHandler TabControl1.SelectedIndexChanged, AddressOf TabControl1_SelectedIndexChanged
                                TabControl1.Dispose()
                                TabControl1 = Nothing
                            End If
                            If TabHistorial IsNot Nothing Then
                                TabHistorial.Dispose()
                                'TabHistorial = Nothing
                            End If
                            If TabForo IsNot Nothing Then
                                TabForo.Dispose()
                                'TabForo = Nothing
                            End If
                            If ToolStripContainer1 IsNot Nothing Then
                                ToolStripContainer1.Dispose()
                                'ToolStripContainer1 = Nothing
                            End If
                            If btnCerrar IsNot Nothing Then
                                btnCerrar.Dispose()
                                'btnCerrar = Nothing
                            End If
                            If pctTaskInfo IsNot Nothing Then
                                RemoveHandler pctTaskInfo.Click, AddressOf pctTaskInfo_Click
                                pctTaskInfo.Dispose()
                                pctTaskInfo = Nothing
                            End If
                            If dtpFecVenc IsNot Nothing Then
                                ' RemoveHandler dtpFecVenc.ValueChanged, AddressOf dtpFecVenc_ValueChanged
                                dtpFecVenc.Dispose()
                                dtpFecVenc = Nothing
                            End If
                            If tabHistorialEmails IsNot Nothing Then
                                tabHistorialEmails.Dispose()
                                'tabHistorialEmails = Nothing
                            End If
                            If chkCloseTaskAfterDistribute IsNot Nothing Then
                                RemoveHandler chkCloseTaskAfterDistribute.CheckedChanged, AddressOf chkCloseTaskAfterDistribute_CheckedChanged
                                chkCloseTaskAfterDistribute.Dispose()
                                chkCloseTaskAfterDistribute = Nothing
                            End If
                            If lblStepName IsNot Nothing Then
                                lblStepName.Dispose()

                            End If

                            If pctWFStepHelp IsNot Nothing Then
                                RemoveHandler pctWFStepHelp.Click, AddressOf pctWFStepHelp_Click
                                pctWFStepHelp.Dispose()
                                pctWFStepHelp = Nothing
                            End If
                            If SplitTasks IsNot Nothing Then
                                SplitTasks.Panel1.Controls.Clear()
                                SplitTasks.Panel2.Controls.Clear()
                                SplitTasks.Dispose()

                            End If
                            If TabSecondaryTask IsNot Nothing Then
                                TabSecondaryTask.Dispose()

                            End If

                            If pctTaskOptions IsNot Nothing Then
                                pctTaskOptions.Dispose()
                                pctTaskOptions = Nothing
                            End If


                            If lblstatus IsNot Nothing Then
                                lblstatus.Dispose()

                            End If
                            If TabDoc IsNot Nothing Then
                                RemoveHandler TabDoc.CloseOpenTask, AddressOf CloseTaskViewer
                                RemoveHandler TabDoc.CambiarDock, AddressOf _CambiarDock
                                RemoveHandler TabDoc.ShowDocumentsAsociated, AddressOf ActivateDocAsociated
                                RemoveHandler TabDoc.ShowAsociatedResult, AddressOf ShowAsociatedResult
                                RemoveHandler TabDoc.ReplaceDocument, AddressOf ReplaceDocument
                                RemoveHandler TabDoc.ShowOriginal, AddressOf ShowOriginal
                                RemoveHandler TabDoc.ReloadAsociatedResult, AddressOf ReloadAsociatedResult
                                RemoveHandler TabDoc.RefreshTask, AddressOf RefreshTask

                                'Flag para indicarle que debe liberar de memoria el control de atributos. Esto se aplica en tareas unicamente.
                                TabDoc.DisposeIndexViewer = True
                                TabDoc.Dispose()
                                TabDoc = Nothing
                            End If
                        End If
                        MyBase.Dispose(disposing)
                    isDisposed = True
                Catch ex As Threading.ThreadAbortException
                Catch ex As Exception
                End Try
            End If
        End Sub

#Region " Código generado por el Diseñador de Windows Forms "

        'Requerido por el Diseñador de Windows Forms
        Private components As System.ComponentModel.IContainer


        Friend WithEvents PanelBottom As ZPanel
        Friend WithEvents LnkForo As System.Windows.Forms.LinkLabel

        Friend WithEvents ToolBar1 As ZToolBar
        Friend WithEvents btnIniciar As System.Windows.Forms.ToolStripButton
        Friend WithEvents btnDerivar As System.Windows.Forms.ToolStripButton
        Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
        Friend WithEvents TabDoc As UCDocumentViewer2
        Friend WithEvents TabHistorial As ZTabPage
        Friend WithEvents TabForo As ZTabPage
        Friend WithEvents ToolStripContainer1 As ToolStripContainer
        Friend WithEvents btnCerrar As System.Windows.Forms.ToolStripButton
        Friend WithEvents pctTaskInfo As System.Windows.Forms.ToolStripMenuItem

        Friend WithEvents TabAsociated As ZTabPage
        Friend WithEvents chkFinishTaskOnClose As ToolStripMenuItem
        Friend WithEvents dtpFecVenc As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents tabHistorialEmails As ZTabPage
        Friend WithEvents chkCloseTaskAfterDistribute As System.Windows.Forms.ToolStripMenuItem

        Friend WithEvents pctWFStepHelp As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents SplitTasks As System.Windows.Forms.SplitContainer
        Friend WithEvents TabSecondaryTask As System.Windows.Forms.TabControl



        Friend WithEvents btnCantUseTask As System.Windows.Forms.ToolStripButton
        Friend WithEvents lblAsignedTo As ToolStripDropDownButton
        Friend WithEvents lblStepName As System.Windows.Forms.ToolStripLabel
        Friend WithEvents lblStateName As System.Windows.Forms.ToolStripLabel
        Friend WithEvents CboStates As System.Windows.Forms.ToolStripComboBox
        Friend WithEvents lblSituation As System.Windows.Forms.ToolStripLabel
        Friend WithEvents pctTaskOptions3 As System.Windows.Forms.ToolStripButton
        Friend WithEvents pctTaskOptions As ToolStripMenuItem
        Friend WithEvents lblstepdescription As ToolStripLabel
        Friend WithEvents lblstatedescripciotn As ToolStripLabel
        Friend WithEvents lblstatus As System.Windows.Forms.ToolStripMenuItem

        <DebuggerStepThrough()> Private Sub InitializeComponent()
            Me.lblstatus = New System.Windows.Forms.ToolStripMenuItem()
            Me.pctWFStepHelp = New System.Windows.Forms.ToolStripMenuItem()
            Me.chkFinishTaskOnClose = New System.Windows.Forms.ToolStripMenuItem()
            Me.pctTaskInfo = New System.Windows.Forms.ToolStripMenuItem()
            Me.dtpFecVenc = New System.Windows.Forms.ToolStripMenuItem()
            Me.chkCloseTaskAfterDistribute = New System.Windows.Forms.ToolStripMenuItem()
            Me.ToolBar1 = New Zamba.AppBlock.ZToolBar()
            Me.btnCerrar = New System.Windows.Forms.ToolStripButton()
            Me.btnIniciar = New System.Windows.Forms.ToolStripButton()
            Me.lblAsignedTo = New System.Windows.Forms.ToolStripDropDownButton()
            Me.btnDerivar = New System.Windows.Forms.ToolStripButton()
            Me.lblSituation = New System.Windows.Forms.ToolStripLabel()
            Me.btnCantUseTask = New System.Windows.Forms.ToolStripButton()
            Me.lblStepName = New System.Windows.Forms.ToolStripLabel()
            Me.lblStateName = New System.Windows.Forms.ToolStripLabel()
            Me.CboStates = New System.Windows.Forms.ToolStripComboBox()
            Me.pctTaskOptions = New System.Windows.Forms.ToolStripMenuItem()
            Me.TabControl1 = New System.Windows.Forms.TabControl()
            Me.TabForo = New Zamba.AppBlock.ZTabPage()
            Me.TabHistorial = New Zamba.AppBlock.ZTabPage()
            Me.tabHistorialEmails = New Zamba.AppBlock.ZTabPage()
            Me.TabAsociated = New Zamba.AppBlock.ZTabPage()
            Me.PanelBottom = New Zamba.AppBlock.ZPanel()
            Me.LnkForo = New System.Windows.Forms.LinkLabel()
            Me.ToolStripContainer1 = New System.Windows.Forms.ToolStripContainer()
            Me.SplitTasks = New System.Windows.Forms.SplitContainer()
            Me.TabSecondaryTask = New System.Windows.Forms.TabControl()
            Me.lblstepdescription = New System.Windows.Forms.ToolStripLabel()
            Me.lblstatedescripciotn = New System.Windows.Forms.ToolStripLabel()
            Me.ToolBar1.SuspendLayout()
            Me.TabControl1.SuspendLayout()
            Me.PanelBottom.SuspendLayout()
            Me.ToolStripContainer1.ContentPanel.SuspendLayout()
            Me.ToolStripContainer1.TopToolStripPanel.SuspendLayout()
            Me.ToolStripContainer1.SuspendLayout()
            CType(Me.SplitTasks, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SplitTasks.Panel1.SuspendLayout()
            Me.SplitTasks.Panel2.SuspendLayout()
            Me.SplitTasks.SuspendLayout()
            Me.SuspendLayout()
            '
            'lblstatus
            '
            Me.lblstatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
            Me.lblstatus.Name = "lblstatus"
            Me.lblstatus.Size = New System.Drawing.Size(0, 0)
            Me.lblstatus.Visible = False
            '
            'pctWFStepHelp
            '
            Me.pctWFStepHelp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
            Me.pctWFStepHelp.Image = Global.Zamba.Controls.My.Resources.Resources.manual
            Me.pctWFStepHelp.Name = "pctWFStepHelp"
            Me.pctWFStepHelp.Size = New System.Drawing.Size(30, 30)
            Me.pctWFStepHelp.ToolTipText = "Ver Ayuda"
            '
            'chkFinishTaskOnClose
            '
            Me.chkFinishTaskOnClose.CheckOnClick = True
            Me.chkFinishTaskOnClose.Font = New System.Drawing.Font("Verdana", 9.75!)
            Me.chkFinishTaskOnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
            Me.chkFinishTaskOnClose.Name = "chkFinishTaskOnClose"
            Me.chkFinishTaskOnClose.Size = New System.Drawing.Size(126, 20)
            Me.chkFinishTaskOnClose.Tag = "Finalizar al Cerrar"
            Me.chkFinishTaskOnClose.Text = "Finalizar al Cerrar"
            Me.chkFinishTaskOnClose.ToolTipText = "Al cerrar la tarea, esta quedara desasignada y podra ser tomada por otra persona"
            '
            'pctTaskInfo
            '
            Me.pctTaskInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
            Me.pctTaskInfo.Image = Global.Zamba.Controls.My.Resources.Resources.note_pinned
            Me.pctTaskInfo.Name = "pctTaskInfo"
            Me.pctTaskInfo.Size = New System.Drawing.Size(30, 30)
            Me.pctTaskInfo.ToolTipText = "VER MÁS INFORMACION"
            '
            'dtpFecVenc
            '
            Me.dtpFecVenc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
            Me.dtpFecVenc.Name = "dtpFecVenc"
            Me.dtpFecVenc.Size = New System.Drawing.Size(0, 0)
            '
            'chkCloseTaskAfterDistribute
            '
            Me.chkCloseTaskAfterDistribute.CheckOnClick = True
            Me.chkCloseTaskAfterDistribute.Font = New System.Drawing.Font("Verdana", 9.75!)
            Me.chkCloseTaskAfterDistribute.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
            Me.chkCloseTaskAfterDistribute.Name = "chkCloseTaskAfterDistribute"
            Me.chkCloseTaskAfterDistribute.Size = New System.Drawing.Size(228, 20)
            Me.chkCloseTaskAfterDistribute.Text = "Cerrar tarea al cambiar de etapa"
            '
            'ToolBar1
            '
            Me.ToolBar1.AllowItemReorder = True
            Me.ToolBar1.BackColor = System.Drawing.Color.White
            Me.ToolBar1.Dock = System.Windows.Forms.DockStyle.None
            Me.ToolBar1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
            Me.ToolBar1.ImageScalingSize = New System.Drawing.Size(32, 32)
            Me.ToolBar1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnCerrar, Me.btnIniciar, Me.lblAsignedTo, Me.btnCantUseTask, Me.lblStepName, Me.lblstepdescription, Me.lblStateName, Me.lblstatedescripciotn, Me.CboStates, Me.pctTaskOptions})
            Me.ToolBar1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
            Me.ToolBar1.Location = New System.Drawing.Point(0, 0)
            Me.ToolBar1.Name = "ToolBar1"
            Me.ToolBar1.Size = New System.Drawing.Size(1086, 39)
            Me.ToolBar1.Stretch = True
            Me.ToolBar1.TabIndex = 60
            '
            'btnCerrar
            '
            Me.btnCerrar.Image = Global.Zamba.Controls.My.Resources.Resources.delete2
            Me.btnCerrar.Name = "btnCerrar"
            Me.btnCerrar.Size = New System.Drawing.Size(91, 36)
            Me.btnCerrar.Tag = "Cerrar"
            Me.btnCerrar.Text = "CERRAR"
            '
            'btnIniciar
            '
            Me.btnIniciar.Image = Global.Zamba.Controls.My.Resources.Resources.media_play_green
            Me.btnIniciar.Name = "btnIniciar"
            Me.btnIniciar.Size = New System.Drawing.Size(95, 36)
            Me.btnIniciar.Tag = "INICIAR"
            Me.btnIniciar.Text = " INICIAR"
            Me.btnIniciar.ToolTipText = "INICIAR"
            '
            'lblAsignedTo
            '
            Me.lblAsignedTo.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnDerivar, Me.lblSituation})
            Me.lblAsignedTo.Image = Global.Zamba.Controls.My.Resources.Resources.appbar_user
            Me.lblAsignedTo.Name = "lblAsignedTo"
            Me.lblAsignedTo.Size = New System.Drawing.Size(98, 36)
            Me.lblAsignedTo.Text = "Ninguno"
            '
            'btnDerivar
            '
            Me.btnDerivar.Image = Global.Zamba.Controls.My.Resources.Resources.user1_find
            Me.btnDerivar.Name = "btnDerivar"
            Me.btnDerivar.Size = New System.Drawing.Size(100, 36)
            Me.btnDerivar.Tag = "DERIVAR"
            Me.btnDerivar.Text = " DERIVAR"
            Me.btnDerivar.ToolTipText = "DERIVAR"
            '
            'lblSituation
            '
            Me.lblSituation.Name = "lblSituation"
            Me.lblSituation.Size = New System.Drawing.Size(0, 0)
            '
            'btnCantUseTask
            '
            Me.btnCantUseTask.ForeColor = System.Drawing.Color.Red
            Me.btnCantUseTask.Image = Global.Zamba.Controls.My.Resources.Resources.bullet_ball_glass_red
            Me.btnCantUseTask.Name = "btnCantUseTask"
            Me.btnCantUseTask.Size = New System.Drawing.Size(247, 36)
            Me.btnCantUseTask.Tag = "MENSAJE"
            Me.btnCantUseTask.Text = "Tarea en ejecución por otro usuario"
            Me.btnCantUseTask.Visible = False
            '
            'lblStepName
            '
            Me.lblStepName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblStepName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
            Me.lblStepName.Image = Global.Zamba.Controls.My.Resources.Resources.appbar_text_align_right
            Me.lblStepName.Name = "lblStepName"
            Me.lblStepName.Size = New System.Drawing.Size(91, 32)
            Me.lblStepName.Text = "ETAPA:"
            '
            'lblStateName
            '
            Me.lblStateName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblStateName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
            Me.lblStateName.Image = Global.Zamba.Controls.My.Resources.Resources.appbar_debug_step_into
            Me.lblStateName.Name = "lblStateName"
            Me.lblStateName.Size = New System.Drawing.Size(102, 32)
            Me.lblStateName.Tag = "LABEL_ESTADO"
            Me.lblStateName.Text = "ESTADO:"
            '
            'CboStates
            '
            Me.CboStates.Name = "CboStates"
            Me.CboStates.Size = New System.Drawing.Size(200, 23)
            Me.CboStates.Visible = False
            '
            'pctTaskOptions
            '
            Me.pctTaskOptions.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
            Me.pctTaskOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.pctTaskOptions.Image = Global.Zamba.Controls.My.Resources.Resources.appbar_cog
            Me.pctTaskOptions.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.pctTaskOptions.Name = "pctTaskOptions"
            Me.pctTaskOptions.Size = New System.Drawing.Size(44, 36)
            Me.pctTaskOptions.Text = "Opciones"
            Me.pctTaskOptions.ToolTipText = "Opciones"
            pctTaskOptions.DropDown = CreateCheckImageContextMenuStrip()
            DirectCast(pctTaskOptions.DropDown, ContextMenuStrip).ShowCheckMargin = True
            '
            'TabControl1
            '
            Me.TabControl1.Controls.Add(Me.TabForo)
            Me.TabControl1.Controls.Add(Me.TabHistorial)
            Me.TabControl1.Controls.Add(Me.tabHistorialEmails)
            Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.0!)
            Me.TabControl1.HotTrack = True
            Me.TabControl1.ItemSize = New System.Drawing.Size(150, 17)
            Me.TabControl1.Location = New System.Drawing.Point(0, 0)
            Me.TabControl1.Margin = New System.Windows.Forms.Padding(0)
            Me.TabControl1.Name = "TabControl1"
            Me.TabControl1.Padding = New System.Drawing.Point(0, 0)
            Me.TabControl1.SelectedIndex = 0
            Me.TabControl1.ShowToolTips = True
            Me.TabControl1.Size = New System.Drawing.Size(1086, 469)
            Me.TabControl1.TabIndex = 0
            '
            'TabForo
            '
            Me.TabForo.BackColor = System.Drawing.Color.White
            Me.TabForo.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TabForo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
            Me.TabForo.Location = New System.Drawing.Point(4, 21)
            Me.TabForo.Name = "TabForo"
            Me.TabForo.Padding = New System.Windows.Forms.Padding(3)
            Me.TabForo.Size = New System.Drawing.Size(1078, 444)
            Me.TabForo.TabIndex = 3
            Me.TabForo.Text = "Foro"
            '
            'TabHistorial
            '
            Me.TabHistorial.BackColor = System.Drawing.Color.White
            Me.TabHistorial.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TabHistorial.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
            Me.TabHistorial.Location = New System.Drawing.Point(4, 21)
            Me.TabHistorial.Name = "TabHistorial"
            Me.TabHistorial.Size = New System.Drawing.Size(1078, 444)
            Me.TabHistorial.TabIndex = 1
            Me.TabHistorial.Text = "Historial"
            Me.TabHistorial.UseVisualStyleBackColor = True
            '
            'tabHistorialEmails
            '
            Me.tabHistorialEmails.BackColor = System.Drawing.Color.White
            Me.tabHistorialEmails.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.tabHistorialEmails.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
            Me.tabHistorialEmails.Location = New System.Drawing.Point(4, 21)
            Me.tabHistorialEmails.Name = "tabHistorialEmails"
            Me.tabHistorialEmails.Padding = New System.Windows.Forms.Padding(3)
            Me.tabHistorialEmails.Size = New System.Drawing.Size(1078, 444)
            Me.tabHistorialEmails.TabIndex = 4
            Me.tabHistorialEmails.Text = "Historial de emails"
            '
            'TabAsociated
            '
            Me.TabAsociated.BackColor = System.Drawing.Color.White
            Me.TabAsociated.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.TabAsociated.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
            Me.TabAsociated.Location = New System.Drawing.Point(4, 21)
            Me.TabAsociated.Name = "TabAsociated"
            Me.TabAsociated.Size = New System.Drawing.Size(992, 432)
            Me.TabAsociated.TabIndex = 2
            Me.TabAsociated.Text = "Documentos Asociados"
            Me.TabAsociated.UseVisualStyleBackColor = True
            '
            'PanelBottom
            '
            Me.PanelBottom.BackColor = System.Drawing.Color.White
            Me.PanelBottom.Controls.Add(Me.LnkForo)
            Me.PanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.PanelBottom.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.PanelBottom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
            Me.PanelBottom.Location = New System.Drawing.Point(0, 469)
            Me.PanelBottom.Name = "PanelBottom"
            Me.PanelBottom.Size = New System.Drawing.Size(1086, 16)
            Me.PanelBottom.TabIndex = 62
            Me.PanelBottom.Visible = False
            '
            'LnkForo
            '
            Me.LnkForo.ActiveLinkColor = System.Drawing.Color.SlateGray
            Me.LnkForo.AutoSize = True
            Me.LnkForo.BackColor = System.Drawing.Color.Transparent
            Me.LnkForo.Cursor = System.Windows.Forms.Cursors.Default
            Me.LnkForo.DisabledLinkColor = System.Drawing.Color.White
            Me.LnkForo.Font = New System.Drawing.Font("Verdana", 7.25!, System.Drawing.FontStyle.Bold)
            Me.LnkForo.ForeColor = System.Drawing.Color.MidnightBlue
            Me.LnkForo.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            Me.LnkForo.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
            Me.LnkForo.LinkColor = System.Drawing.Color.MidnightBlue
            Me.LnkForo.Location = New System.Drawing.Point(13, 2)
            Me.LnkForo.Name = "LnkForo"
            Me.LnkForo.Size = New System.Drawing.Size(120, 12)
            Me.LnkForo.TabIndex = 57
            Me.LnkForo.TabStop = True
            Me.LnkForo.Text = "FORO DE MENSAJES"
            Me.LnkForo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
            '
            'ToolStripContainer1
            '
            '
            'ToolStripContainer1.ContentPanel
            '
            Me.ToolStripContainer1.ContentPanel.Controls.Add(Me.SplitTasks)
            Me.ToolStripContainer1.ContentPanel.Controls.Add(Me.PanelBottom)
            Me.ToolStripContainer1.ContentPanel.Size = New System.Drawing.Size(1086, 485)
            Me.ToolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ToolStripContainer1.Location = New System.Drawing.Point(0, 0)
            Me.ToolStripContainer1.Name = "ToolStripContainer1"
            Me.ToolStripContainer1.Size = New System.Drawing.Size(1086, 524)
            Me.ToolStripContainer1.TabIndex = 63
            Me.ToolStripContainer1.Text = "ToolStripContainer1"
            '
            'ToolStripContainer1.TopToolStripPanel
            '
            Me.ToolStripContainer1.TopToolStripPanel.BackColor = System.Drawing.Color.Silver
            Me.ToolStripContainer1.TopToolStripPanel.Controls.Add(Me.ToolBar1)
            '
            'SplitTasks
            '
            Me.SplitTasks.Dock = System.Windows.Forms.DockStyle.Fill
            Me.SplitTasks.Location = New System.Drawing.Point(0, 0)
            Me.SplitTasks.Name = "SplitTasks"
            '
            'SplitTasks.Panel1
            '
            Me.SplitTasks.Panel1.Controls.Add(Me.TabControl1)
            Me.SplitTasks.Panel1MinSize = 1
            '
            'SplitTasks.Panel2
            '
            Me.SplitTasks.Panel2.Controls.Add(Me.TabSecondaryTask)
            Me.SplitTasks.Panel2Collapsed = True
            Me.SplitTasks.Panel2MinSize = 0
            Me.SplitTasks.Size = New System.Drawing.Size(1086, 469)
            Me.SplitTasks.SplitterDistance = 245
            Me.SplitTasks.TabIndex = 63
            '
            'TabSecondaryTask
            '
            Me.TabSecondaryTask.Dock = System.Windows.Forms.DockStyle.Fill
            Me.TabSecondaryTask.HotTrack = True
            Me.TabSecondaryTask.Location = New System.Drawing.Point(0, 0)
            Me.TabSecondaryTask.Name = "TabSecondaryTask"
            Me.TabSecondaryTask.SelectedIndex = 0
            Me.TabSecondaryTask.Size = New System.Drawing.Size(96, 100)
            Me.TabSecondaryTask.TabIndex = 0
            '
            'lblstepdescription
            '
            Me.lblstepdescription.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
            Me.lblstepdescription.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
            Me.lblstepdescription.Margin = New System.Windows.Forms.Padding(0, 9, 0, 2)
            Me.lblstepdescription.Name = "lblstepdescription"
            Me.lblstepdescription.Size = New System.Drawing.Size(0, 0)
            '
            'lblstatedescripciotn
            '
            Me.lblstatedescripciotn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
            Me.lblstatedescripciotn.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
            Me.lblstatedescripciotn.Margin = New System.Windows.Forms.Padding(0, 9, 0, 2)
            Me.lblstatedescripciotn.Name = "lblstatedescripciotn"
            Me.lblstatedescripciotn.Size = New System.Drawing.Size(0, 0)
            '
            'UCTaskViewer
            '
            Me.Controls.Add(Me.ToolStripContainer1)
            Me.Name = "UCTaskViewer"
            Me.Size = New System.Drawing.Size(1086, 524)
            Me.ToolBar1.ResumeLayout(False)
            Me.ToolBar1.PerformLayout()
            Me.TabControl1.ResumeLayout(False)
            Me.PanelBottom.ResumeLayout(False)
            Me.PanelBottom.PerformLayout()
            Me.ToolStripContainer1.ContentPanel.ResumeLayout(False)
            Me.ToolStripContainer1.TopToolStripPanel.ResumeLayout(False)
            Me.ToolStripContainer1.TopToolStripPanel.PerformLayout()
            Me.ToolStripContainer1.ResumeLayout(False)
            Me.ToolStripContainer1.PerformLayout()
            Me.SplitTasks.Panel1.ResumeLayout(False)
            Me.SplitTasks.Panel2.ResumeLayout(False)
            CType(Me.SplitTasks, System.ComponentModel.ISupportInitialize).EndInit()
            Me.SplitTasks.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

#End Region

#Region "Atributos y Propiedades"

        Dim newUcForo As UcForo
        Dim ucHistorialEmails As ucHistorialEmails
        Public TaskResult As TaskResult
        Dim WithEvents UC_Info As UC_Info
        Dim WithEvents UC_Help As UCWFHelp
        Private hshRulesNames As New Hashtable
        Private Delegate Sub voidDelegate()
        Private Delegate Sub LoadDelegate(ByVal o As Boolean)
        Private Delegate Sub InitControlDelegate(ByRef TaskResult As ITaskResult)
        Dim CurrentUserId As Int64
        Private isDisposed As Boolean
        Private asociatedGrid As UCFusion2
        Private _setReadOnly As Nullable(Of Boolean)
        'Variable que marca si se modifico algun valor del documento
        Private blnModified As Boolean
        ' Bandera que sirve para controlar si se ejecutan o no documentos asociados que están como una tarea
        Dim banExecuteAssociatedDocuments As Boolean
        '[Ezequiel] 20/04/09 Variable que determina si se esta ejecutando una regla en el viewer.
        Public ExecutingRule As Boolean

        Public Property SetReadOnly() As Nullable(Of Boolean)
            Get
                Return _setReadOnly
            End Get
            Set(ByVal value As Nullable(Of Boolean))
                _setReadOnly = value
            End Set
        End Property
#End Region

        Private PanelsController As WF.UInterface.Client.UCTasksPanel

#Region "Constructores"
        ''' <summary>
        ''' Constructor de UCTaskViewer
        ''' </summary>
        ''' <param name="TaskResult"></param>
        ''' <remarks></remarks>
        ''' <history> 
        '''     [Gaston]    28/10/2008     Modified     Llamada al método que permite ver si la etapa tiene o no permiso para ejecutar docs. asociados
        ''' </history>
        ''' 


        Private Sub currentContextMenuClick(Action As String, listResults As List(Of IResult), ContextMenuContainer As IMenuContextContainer) Implements IMenuContextContainer.currentContextMenuClick
            PanelsController.currentContextMenuClick(Action, listResults, ContextMenuContainer)
        End Sub
        Public Sub New(ByVal TaskResult As ITaskResult, ByVal CurrentUserId As Int64, Controller As UCTasksPanel)
            MyBase.New()
            InitializeComponent()
            PanelsController = Controller
            Me.CurrentUserId = CurrentUserId

            currentContextMenu = New UCGridContextMenu(Me)


            Try
                '(pablo) - permiso de Ver Documentos Asociados
                If RightsBusiness.ValidateAssociateRight(CurrentUserId, TaskResult.DocTypeId) Then
                    TabControl1.Controls.Remove(TabHistorial)
                    TabControl1.Controls.Remove(tabHistorialEmails)
                    '------------------------
                    TabControl1.Controls.Add(TabAsociated)
                    TabControl1.Controls.Add(TabHistorial)
                    TabControl1.Controls.Add(tabHistorialEmails)
                End If

                EnableFinishTaskOnClose(TaskResult)
                'Habilitacion del check que se encarga de cerrar la tarea al distribuirla
                RemoveHandler chkCloseTaskAfterDistribute.CheckedChanged, AddressOf chkCloseTaskAfterDistribute_CheckedChanged
                chkCloseTaskAfterDistribute.Checked = CBool(UserPreferences.getValue("CloseTaskAfterDistribute", UPSections.WorkFlow, "False"))
                AddHandler chkCloseTaskAfterDistribute.CheckedChanged, AddressOf chkCloseTaskAfterDistribute_CheckedChanged

                RemoveHandler ZClass.eHandleModuleRuleAction, AddressOf LoadModulesRuleActions
                AddHandler ZClass.eHandleModuleRuleAction, AddressOf LoadModulesRuleActions

                InitControl(TaskResult)

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Private Sub EnableFinishTaskOnClose(TaskResult As ITaskResult)
            'Habilitacion del check que se encarga de finalizar la tarea al cerrar
            If (TaskResult.TaskState = Global.Zamba.Core.TaskStates.Asignada OrElse TaskResult.TaskState = Global.Zamba.Core.TaskStates.Ejecucion) AndAlso TaskResult.AsignedToId = Membership.MembershipHelper.CurrentUser.ID AndAlso RightsBusiness.GetUserRights(ObjectTypes.WFSteps, RightsType.Terminar, TaskResult.StepId) Then
                chkFinishTaskOnClose.Checked = CBool(UserPreferences.getValue("CheckFinishTaskAfterClose", UPSections.WorkFlow, True))
                chkFinishTaskOnClose.Font = New Font(chkFinishTaskOnClose.Font, FontStyle.Bold)
            Else
                chkFinishTaskOnClose.Checked = False
                chkFinishTaskOnClose.Font = New Font(chkFinishTaskOnClose.Font, FontStyle.Regular)
            End If

            If RightsBusiness.GetUserRights(ObjectTypes.WFSteps, RightsType.Terminar, TaskResult.StepId) Then
                chkFinishTaskOnClose.Enabled = True
            Else
                chkFinishTaskOnClose.Enabled = False
                UserPreferences.setValue("CheckFinishTaskAfterClose", UPSections.WorkFlow, False)
            End If
        End Sub


        ' This utility method creates a ContextMenuStrip control
        ' that has four ToolStripMenuItems showing the four 
        ' possible combinations of image and check margins.
        Friend Function CreateCheckImageContextMenuStrip() As ContextMenuStrip
            Dim checkImageContextMenuStrip As New ContextMenuStrip()
            checkImageContextMenuStrip.Items.Add(dtpFecVenc)
            checkImageContextMenuStrip.Items.Add(pctTaskInfo)
            checkImageContextMenuStrip.Items.Add(lblstatus)
            checkImageContextMenuStrip.Items.Add(chkFinishTaskOnClose)
            checkImageContextMenuStrip.Items.Add(chkCloseTaskAfterDistribute)
            checkImageContextMenuStrip.Items.Add(pctWFStepHelp)
            Return checkImageContextMenuStrip
        End Function

        Private Sub InitControl(ByRef TaskResult As ITaskResult)
            Try
                SuspendLayout()
                TaskResult.EditDate = Now()
                LoadTask(TaskResult)
                DisablePropertyControls()
                IniciarTareaAlAbrir(TaskResult)

                Dim WP As New BackgroundWorker
                AddHandler WP.DoWork, AddressOf RunLoadForumMessagesCount
                WP.RunWorkerAsync(TaskResult)

                TabDoc.Select()

                'Se encarga de mostrar u ocultar el boton con la flecha que muestra opciones de la tarea
                If Boolean.Parse(UserPreferences.getValue("HideTaskOptionsArrow", UPSections.Viewer, "False")) Then
                    pctTaskOptions.Visible = False
                End If
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            Finally
                TabDoc.EnableForm(True)
                ResumeLayout()
            End Try
        End Sub

        Private Sub RunLoadForumMessagesCount(sender As Object, e As DoWorkEventArgs)
            Dim TaskResult As IResult = e.Argument
            LoadForumMessagesCount(TaskResult)

        End Sub

        Private Sub LoadForumMessagesCount(TaskResult As ITaskResult)
            If Membership.MembershipHelper.CurrentUser IsNot Nothing Then
                If RightsBusiness.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.MostrarConversaciones, TaskResult.DocTypeId) Then
                    TabForo.Text = "Foro (" & ZForoBusiness.GetAllMessagesDT(TaskResult.ID, TaskResult.DocType.ID, DirectCast(TaskResult, Zamba.Core.TaskResult).RootDocumentId).Rows.Count.ToString & " mensaje/s)"
                Else
                    TabForo.Text = "Foro (" & ZForoBusiness.GetCountAllMessages(TaskResult.ID, Membership.MembershipHelper.CurrentUser.ID).ToString & " mensaje/s)"
                End If
            End If
        End Sub
#End Region

        'Esta función emula un click en el botón 'Nuevo' de la pestaña Foro
        Private Sub RefreshForoInfo(ByVal _subject As String, ByVal _body As String, ByRef _flagSaved As Boolean)

            If Not IsNothing(newUcForo) Then
                newUcForo.btnNuevo_ClickSinEvento(_subject, _body, _flagSaved.ToString, Nothing, -1, Nothing, Nothing)
            Else
                _flagSaved = False
            End If
        End Sub
        ''' <summary>
        ''' Reemplaza un archivo y refresca la tarea
        ''' </summary>
        ''' <param name="result"></param>
        ''' <remarks></remarks>
        ''' <history>
        '''     (Pablo)	    11/01/2011	Created
        ''' </history>
        Private Sub ReplaceDocument(ByRef result As Result)
            Dim Dialog As New OpenFileDialog
            Dialog.CheckFileExists = True
            Dialog.CheckPathExists = True
            Dialog.Multiselect = False
            Dialog.Title = "Reemplazo de Documentos"

            If Dialog.ShowDialog = DialogResult.OK Then
                Try
                    'Verifica si el volumen es de tipo base de datos o si todavia no se inserto el documento
                    Dim Volume As Volume = ZCore.filterVolumes(result.Disk_Group_Id)

                    If Volume Is Nothing Then
                        Dim volListID As Int32 = VolumesFactory.GetVolumeListId(result.DocTypeId)
                        Dim DsVols As DataSet = VolumesBusiness.GetActiveDiskGroupVolumes(volListID)
                        Volume = VolumesFactory.LoadVolume(result.DocTypeId, DsVols)
                        DsVols.Dispose()
                        DsVols = Nothing
                    End If

                    Dim RB As New Results_Business

                    If Volume IsNot Nothing AndAlso Volume.Type = VolumeTypes.DataBase Then
                        'Se reemplaza el documento en Zamba
                        result.EncodedFile = FileEncode.Encode(Dialog.FileName)
                        result.File = Dialog.FileName
                        result.Doc_File = result.ID & Path.GetExtension(Dialog.FileName)
                        result.IconId = RB.GetFileIcon(result.File)
                        Dim VolumeListId As Int64 = VolumesBusiness.GetVolumeListId(result.DocTypeId)
                        Dim DsVols As DataSet = VolumesBusiness.GetActiveDiskGroupVolumes(VolumeListId)
                        Results_Factory.ReplaceDocument(result, result.Doc_File, True, DsVols)

                        'Se copia al temporal para ser abierto al refrescar la tarea
                        Dim dirInfo As DirectoryInfo = Tools.EnvironmentUtil.GetTempDir("\OfficeTemp")
                        Dim destPath As String = dirInfo.FullName & "\" & result.Doc_File
                        File.Copy(Dialog.FileName, destPath, True)
                        dirInfo = Nothing
                        Volume.Dispose()
                        Volume = Nothing
                        RB = Nothing


                    ElseIf Volume IsNot Nothing Then
                        'Se completa la ruta del documento a reemplazante
                        If String.IsNullOrEmpty(result.FullPath) Then
                            Dim filename As String = Path.GetFileName(Dialog.FileName)

                            result.File = Volume.path & "\" & result.DocTypeId & "\" & VolumesBusiness.GetOffSet(Volume) & "\" & filename
                        Else
                            result.File = Dialog.FileName
                        End If

                        'Se reemplaza el documento y se refresca la tarea completa
                        RB.ReplaceDocument(DirectCast(result, TaskResult), Dialog.FileName, True)
                        Volume.Dispose()
                        Volume = Nothing
                        RB = Nothing
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsError, "No hay volumen disponible para el reemplazo")
                        Throw New Exception("No hay volumen disponible para el reemplazo")
                    End If

                    UserBusiness.Rights.SaveAction(result.ID, ObjectTypes.Documents, RightsType.Edit, result.Name)
                    TabDoc.updateDocsAsociated()
                Catch ex As IOException
                    ZClass.raiseerror(ex)
                    MessageBox.Show("Ocurrio un error al reemplazar el documento actual: " & ex.ToString, "Error de Reemplazo", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                    MessageBox.Show("Ocurrio un error al reemplazar el documento actual: " & ex.ToString, "Error de Reemplazo", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    Dialog.Dispose()
                    Dialog = Nothing

                End Try
            End If
        End Sub






        ''' <summary>
        ''' Verifica si inicia la tarea al abrir el documento
        ''' </summary>
        Public Sub IniciarTareaAlAbrir(ByVal task As ITaskResult)
            If task.TaskState = TaskStates.Servicio Then
                ShowExecutedByOtherMessage("Servicio")
            Else
                Dim wfstep As IWFStep
                Dim users As Generic.List(Of Long)

                Try
                    Dim currentLockedUser As String
                    If WFTaskBusiness.LockTask(TaskResult.TaskId, currentLockedUser) Then
                        wfstep = WFStepBusiness.GetStepById(task.StepId, False)
                        btnCantUseTask.Visible = False

                        If wfstep.StartAtOpenDoc Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Iniciar tarea al abrir activado")
                            users = UserGroupBusiness.GetUsersIds(task.AsignedToId, True)
                            If (task.AsignedToId = Membership.MembershipHelper.CurrentUser.ID OrElse users.Contains(Membership.MembershipHelper.CurrentUser.ID)) Then
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "La tarea se encuentra en estado " & task.TaskState.ToString & " por usuario " & Membership.MembershipHelper.CurrentUser.Name)
                                'Asignada a mi o a un grupo al que pertenezco
                                btnIniciar_Click(True)
                            ElseIf task.AsignedToId <> 0 Then
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "La tarea se encuentra en estado " & task.TaskState.ToString & " por usuario " & UserGroupBusiness.GetUserorGroupNamebyId(task.AsignedToId))
                                Select Case task.TaskState
                                    Case TaskStates.Asignada
                                        If RightsBusiness.GetUserRights(ObjectTypes.WFSteps, RightsType.UnAssign, CInt(TaskResult.StepId)) Then
                                            SetAsignedTo()
                                            GetStatesOfTheButtonsRule()
                                        End If
                                    Case TaskStates.Desasignada
                                        'Nunca deberia pasar por aca, porque si esta asignada a otro usuario o grupo, deberia estar en asignada o ejecucion
                                        btnIniciar_Click(True)
                                    Case Else
                                        ShowExecutedByOtherMessage(currentLockedUser)
                                End Select
                            ElseIf task.AsignedToId = 0 Then
                                'Esta asignada a ninguno
                                btnIniciar_Click(True)
                            End If
                        Else
                            users = UserGroupBusiness.GetUsersIds(task.AsignedToId, True)
                            If task.AsignedToId <> 0 AndAlso task.AsignedToId <> Membership.MembershipHelper.CurrentUser.ID AndAlso users.Contains(Membership.MembershipHelper.CurrentUser.ID) = False Then
                                If task.TaskState = TaskStates.Ejecucion Then
                                    ShowExecutedByOtherMessage(currentLockedUser)
                                End If
                            End If
                        End If
                    Else
                        ShowExecutedByOtherMessage(currentLockedUser)
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                Finally
                    wfstep = Nothing
                    If Not IsNothing(users) Then
                        users.Clear()
                        users = Nothing
                    End If
                End Try
            End If
        End Sub

        Private Sub LoadTask(ByRef TaskResult As ITaskResult)
            Try
                Me.TaskResult = DirectCast(TaskResult, TaskResult)
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "LoadView True")
                LoadViewer(True)
                verifyIfAllowExecuteAssociatedDocuments(TaskResult.StepId)
                DocTypesBusiness.GetEditRights(DirectCast(TaskResult.DocType, DocType))
                SetWFStep()
                'Muestra o oculta las reglas segun preferencia
                GetStatesOfTheButtonsRule()
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "RefreshForm True False")
                refreshForm(True, False)
                'SetStepName(TaskResult.StepId)
                SetAsignedAndSituationLabels(TaskResult)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

        End Sub

        ''' <summary>
        ''' Oculta el botón de iniciar y muestra un mensaje informando la causa
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub ShowExecutedByOtherMessage(ByVal currentLockedUser As String)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Deshabilitando boton de Iniciar")
            btnIniciar.Visible = False
            btnCantUseTask.Text = "Tarea en Ejecucion por: " & currentLockedUser
            btnCantUseTask.Visible = True
            HideUserActions()
        End Sub


        ''' <summary>
        ''' Recarga la tarea
        ''' </summary>
        ''' <history>
        '''     Marcelo 16/03/2009  Modified
        '''     Javier  06/12/2010  Modified    GetTaskByTaskIdAndDocTypeIdAndStepId is now a instance method
        '''</history>
        ''' <remarks></remarks>
        Public Sub ReLoadTask(ByVal SelectedIndex As Int32, ByRef ActualTaskResult As ITaskResult)
            Cursor = Cursors.WaitCursor
            SuspendLayout()

            Dim formid As Int64 = TaskResult.CurrentFormID
            Dim taskid As Int64 = TaskResult.TaskId
            Dim doctypeid As Int64 = TaskResult.DocTypeId
            Dim taskbs As New WFTaskBusiness()


            If Not ActualTaskResult Is Nothing Then
                TaskResult = ActualTaskResult
            Else
                TaskResult = DirectCast(taskbs.GetTaskByTaskIdAndDocTypeIdAndStepId(taskid, doctypeid, TaskResult.StepId, 0), TaskResult)
            End If

            If IsNothing(TaskResult) Then
                Dim WFTB As New WFTaskBusiness
                TaskResult = WFTB.GetTaskByTaskIdAndDocTypeId(taskid, doctypeid, 0)
            End If

            TaskResult.CurrentFormID = formid

            verifyIfAllowExecuteAssociatedDocuments(TaskResult.StepId)
            LoadViewer(False, CShort(SelectedIndex))

            SetWFStep()

            'refresh de asociados
            refreshGrid(False)

            'Muestra o oculta las reglas segun preferencia
            GetStatesOfTheButtonsRule()
            refreshForm(True, True)
            UpdateGUITaskSituation(TaskResult)

            Cursor = Cursors.Default
            ResumeLayout()
        End Sub

        ''' <summary>
        ''' Actualiza un documento asociado abierto desde la grilla de asociados de una tarea
        ''' </summary>
        ''' <param name="Asociatedresult"></param>
        ''' <history>Created (pablo) - 28022011
        ''' </history>
        ''' <remarks></remarks>
        Public Sub ReloadAsociatedResult(ByVal Asociatedresult As Core.Result, Optional ByVal refreshTask As Boolean = False)
            SuspendLayout()
            Dim selectedIndex As Int16
            'cierro el tab y vuelvo a abrirlo
            If TabControl1.SelectedTab.Tag = Asociatedresult.ID.ToString() Then
                selectedIndex = TabControl1.TabPages.IndexOf(TabControl1.SelectedTab)
                TabControl1.Controls.Remove(TabControl1.SelectedTab)
            End If

            ShowAsociatedResult(Asociatedresult, selectedIndex)
            'Si refrtesh task viene en true es porque se llama desde la doaddasociateddocument, por ende refresco la tarea asi se ve el asociado.
            If refreshTask Then
                Me.RefreshTask(TaskResult)
            End If
            ResumeLayout()
        End Sub

        ''' <summary>
        ''' Recarga la tarea
        ''' </summary>
        ''' <history>Marcelo 16/03/2009 Modified</history>
        ''' <history>Marcelo 08/06/2009 Modified
        '''         [Sebastian] 11-06-2009 Modified se agrego cast
        ''' </history>
        ''' <remarks></remarks>
        Public Sub ReLoadTask(ByVal Task As ITaskResult)
            Cursor = Cursors.WaitCursor
            SuspendLayout()
            TaskResult = DirectCast(Task, TaskResult)
            verifyIfAllowExecuteAssociatedDocuments(TaskResult.StepId)
            LoadViewer(True)
            SetWFStep()
            'Muestra o oculta las reglas segun preferencia
            GetStatesOfTheButtonsRule()
            refreshForm(True, True)
            UpdateGUITaskSituation(Task)
            ResumeLayout()
            Cursor = Cursors.Default
        End Sub


        Dim fromRefreshTask As Boolean
        ''' <summary>
        ''' Recarga la tarea
        ''' </summary>
        ''' <param name="Task"></param>
        ''' <remarks>   Se utiliza especialmente al distribuir una tarea, no cerrarla, 
        '''             pero recargar sus elementos con el nuevo ID de etapa.
        ''' </remarks>
        ''' <history>
        '''     Tomas   30/07/2010  Created
        ''' </history>
        Public Sub RefreshTask(ByVal Task As ITaskResult)
            Cursor = Cursors.WaitCursor
            SuspendLayout()
            'Pablo. Obtengo nuevamente los datos de la base
            'Task = WFTaskBusiness.GetTaskByTaskIdAndDocTypeId(Task.TaskId, Task.DocTypeId, 0)
            TaskResult = DirectCast(Task, TaskResult)
            verifyIfAllowExecuteAssociatedDocuments(TaskResult.StepId)
            SetWFStep()
            SetAsignedAndSituationLabels(TaskResult)

            refreshForm(True, True)
            'SetStepName(Task.StepId)
            DisablePropertyControls()
            EnablePropietaryControls()

            'Muestra o oculta las reglas segun preferencia
            GetStatesOfTheButtonsRule()
            fromRefreshTask = True
            IniciarTareaAlAbrir(Task)
            ResumeLayout()
            Cursor = Cursors.Default
        End Sub

        Private Sub SetStepName(ByVal name As String)
            lblstepdescription.Text = name.Trim
        End Sub
        Private Sub SetStepName(ByVal id As Int64)
            Try
                lblstepdescription.Text = WFStepBusiness.GetStepNameById(id)
            Catch ex As Exception
                ZClass.raiseerror(ex)
                lblstepdescription.Text = String.Empty
            End Try
        End Sub

        Public Sub LoadForo()
            Try
                If Not IsNothing(Membership.MembershipHelper.CurrentUser) AndAlso Not IsNothing(TaskResult) Then
                    If (TabForo.Controls.Count = 0) Then
                        newUcForo = New UcForo(Membership.MembershipHelper.CurrentUser, TaskResult.ID, TaskResult.DocTypeId)
                        newUcForo.Dock = DockStyle.Fill
                        TabForo.Controls.Add(newUcForo)

                        RemoveHandler newUcForo.showMailForm, AddressOf sendMailFromForum
                        AddHandler newUcForo.showMailForm, AddressOf sendMailFromForum
                    End If

                    newUcForo.ShowInfo(TaskResult.ID, TaskResult.DocTypeId)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub


        ''' <summary>
        ''' Método que permite mostrar la parte de historial de emails y colocarlo adentro del tab "historial de emails"
        ''' </summary>
        ''' <param name="Result"></param>
        ''' <remarks></remarks>
        ''' <history>
        ''' 	[Alejandro]	03/12/2009 - Created
        ''' </history>
        Private Sub ShowHistorialEmails(ByRef Result As Result)

            If (ucHistorialEmails Is Nothing) Then
                If Not IsNothing(Result) Then
                    ucHistorialEmails = New ucHistorialEmails(Result.ID, CurrentUserId)
                    ucHistorialEmails.Dock = DockStyle.Fill
                    tabHistorialEmails.Controls.Add(ucHistorialEmails)
                End If
            Else
                ucHistorialEmails.ShowInfo(Result.ID)
            End If
        End Sub

        Private Sub refreshHistorialEmails()

            If Not ucHistorialEmails Is Nothing Then
                ucHistorialEmails.RefreshGrid()
            End If

        End Sub

        Private Sub sendMailFromForum(ByRef textFor As String, ByRef textSubject As String, ByRef textBody As String, ByVal idMensaje As Int32, ByVal parentId As Int64, ByVal blnAutomaticAttachLink As Boolean, ByVal blnAutomaticSend As Boolean, ByVal Attachs() As String)
            ZTrace.WriteLineIf(ZTrace.IsWarning, "Envío de mail desde foro")
            Dim SelectedResults As New Generic.List(Of IResult)
            SelectedResults.Add(TaskResult)

            Dim SelectedResultsaux As New Generic.List(Of IResult)

            For Each r As Result In SelectedResults
                Try
                    If r.ISVIRTUAL Then
                        Try
                            If Boolean.Parse(UserPreferences.getValue("PreviewFormInForum", UPSections.UserPreferences, "True")) Then
                                Dim prvfrm As New PreviewForm(r, "Adjuntar")

                                If prvfrm.ShowDialog = DialogResult.OK Then
                                    r.Html = prvfrm.frmbrowser.GetHtml

                                    r.HtmlFile = Tools.EnvironmentUtil.GetTempDir("\OfficeTemp").FullName & "\" & r.Name & "-temp" & (New Random).Next(1, 9999).ToString & ".html"

                                    Dim filePath As String = FormBusiness.GetShowAndEditForms(r.DocType.ID)(0).Path.Replace(".html", ".mht")

                                    If File.Exists(filePath) Then
                                        Try
                                            Dim writerPath As String = r.HtmlFile.Substring(0, r.HtmlFile.Length - 4) & "mht"
                                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Guardando documento MHT en: " & writerPath)
                                            Using write As New StreamWriter(writerPath)
                                                write.AutoFlush = True
                                                Dim reader As New StreamReader(filePath)
                                                Dim mhtstring As String = reader.ReadToEnd()
                                                write.Write(mhtstring.Replace("<Zamba.Html>", r.Html))
                                            End Using
                                        Catch ex As Exception
                                            ZClass.raiseerror(ex)
                                        End Try

                                        r.HtmlFile = r.HtmlFile.Substring(0, r.HtmlFile.Length - 4) & "mht"
                                    Else

                                        Try
                                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Guardando formulario en: " & r.HtmlFile)
                                            Using write As New StreamWriter(r.HtmlFile)
                                                write.AutoFlush = True
                                                write.Write(r.Html)
                                            End Using
                                        Catch ex As Exception
                                            ZClass.raiseerror(ex)
                                        End Try

                                    End If
                                End If
                            End If
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try
                    End If

                    SelectedResultsaux.Add(r)

                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            Next

            SelectedResults = SelectedResultsaux

            Dim ResulEnvio As Boolean

            Dim frmMessage As IZMessageForm
            ' Se obtiene el formulario de mail y se muestra
            frmMessage = clsMessages.getMailFormFromForum(SelectedResults, textFor, textSubject, textBody)
            Select Case frmMessage.GetType.Name.ToLower
                Case "frmlotusmessagesend"
                    DirectCast(frmMessage, frmLotusMessageSend).MailEvent = MailEvent.Forum_Tasks
                Case "frmnetmailmessagesend"
                    DirectCast(frmMessage, frmNetMailMessageSend).MailEvent = MailEvent.Forum_Tasks
            End Select

            If blnAutomaticAttachLink = True Or blnAutomaticSend = False Then
                Dim links As New ArrayList
                links.Add(True)
                links.Add("Zamba:\\taskid=" & TaskResult.TaskId)
                frmMessage.EspecificarDatos(textFor, String.Empty, String.Empty, textSubject, textBody, Attachs, Nothing, blnAutomaticSend, links, False, String.Empty, True)
            Else
                frmMessage.EspecificarDatos(textFor, String.Empty, String.Empty, textSubject, textBody, Attachs, Nothing, blnAutomaticSend, Nothing, False, String.Empty, True)
            End If

            If blnAutomaticSend = False Then
                Dim form As System.Windows.Forms.Form
                form = DirectCast(frmMessage, Form)
                form.ControlBox = True
                form.Show(Me)
            Else
                ResulEnvio = True
            End If

            ' Si se envío con éxito el mail entonces el formulario que invoco al formulario de mail se cierra
            If ResulEnvio Then
                Try
                    ZForoBusiness.InsertForumMail(SelectedResults, idMensaje, parentId, textFor)
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
                newUcForo.closeNuevoMensaje()
            End If
        End Sub

        Public Sub SetWFStep()
            Try
                If Not FlagClosing Then
                    Try
                        Dim wfstep As IWFStep = WFStepBusiness.GetStepById(TaskResult.StepId, False)

                        'Actualizo el nombre de la etapa
                        SetStepName(wfstep.Name)
                        'Cargo Estados
                        CboStates.Items.Clear()
                        For Each st As WFStepState In wfstep.States
                            CboStates.Items.Add(st)
                        Next
                        CboStates.ComboBox.DisplayMember = "Name"
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try

                    Try
                        RemoveHandler CboStates.SelectedIndexChanged, AddressOf CboStates_SelectedIndexChanged
                        If TaskResult.State.ID > 0 Then
                            For i As Int32 = 0 To CboStates.Items.Count - 1
                                CboStates.SelectedIndex = i
                                If DirectCast(CboStates.SelectedItem, Zamba.Core.WFStepState).ID = TaskResult.State.ID Then
                                    Exit For
                                End If
                            Next
                        Else
                            For i As Int32 = 0 To CboStates.Items.Count - 1
                                CboStates.SelectedIndex = i
                                If DirectCast(CboStates.SelectedItem, Zamba.Core.WFStepState).Initial Then
                                    Exit For
                                End If
                            Next
                        End If

                        AddHandler CboStates.SelectedIndexChanged, AddressOf CboStates_SelectedIndexChanged
                        lblstatedescripciotn.Text = CboStates.Text
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try


                    SetExpiredDate()

                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
        Public Sub SetAsignedAndSituationLabels(ByVal AsignedTaskResult As TaskResult)
            UpdateGUITaskSituation(TaskResult)
            UpdateGUITaskAsignedSituation(TaskResult)

        End Sub

        Public Sub SetAsignedTo()
            Try
                If Not FlagClosing Then
                    EnablePropietaryControls()
                    SetExpiredDate()
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        '------------------------------------------------------

        ''' <summary>
        ''' Método utilizado para habilitar/deshabilitar reglas
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Gaston]    23/04/2008  Created
        ''' </history>
        Public Sub GetStatesOfTheButtonsRule()
            Dim state As WFStepState
            Dim useTabEnable As Boolean
            Dim selectionvalue As RulePreferences
            Dim Dt As DataTable
            Dim Dt2 As DataTable
            Dim DtUsersAndGroup As DataTable
            Dim WFIndexAndVariableBusiness As WFIndexAndVariableBusiness
            Dim indexsAndVariables As List(Of IndexAndVariable)
            Dim lstRulesEnabled As List(Of Boolean)

            Try
                UserActionDisabledRules = New List(Of Long)

                'Obtiene el estado seleccionado del combobox
                state = DirectCast(CboStates.SelectedItem, Zamba.Core.WFStepState)
                'Variable de tipo Preferencia de regla
                selectionvalue = DirectCast(-1, RulePreferences)


                'Recorre cada regla activa en el documento
                For Each btn As ToolStripItem In ToolBar1.Items
                    Try
                        Dim idRule As Int64

                        If Int64.TryParse(btn.Tag, idRule) Then

                            useTabEnable = True
                            'Si la regla no fue procesada antes por la DoEnable
                            If TaskResult.UserRules.Contains(idRule) = True Then
                                'Lista que en la posicion 0 guarda si esta habilitada la regla o no
                                'y en la 1 si se acumula a la habilitacion de las solapas o no
                                lstRulesEnabled = DirectCast(TaskResult.UserRules(idRule), List(Of Boolean)).ToList()
                                'Si la regla esta deshabilitada, no uso los estados de los tabs
                                If lstRulesEnabled.Count > 0 AndAlso lstRulesEnabled(0) = True Then
                                    'Si no esta marcada la opcion de ejecucion conjunta con los tabs, no uso los estados
                                    If lstRulesEnabled(1) = False Then
                                        useTabEnable = False
                                    End If
                                Else
                                    useTabEnable = False
                                End If
                            End If

                            'Si utilizo los tabs (porq no uso la doenable o porq la ejecucion es conjunta)
                            If useTabEnable = True Then
                                'Obtiene el valor 
                                selectionvalue = WFBusiness.recoverItemSelectedThatCanBe_StateOrUserOrGroup(TaskResult.StepId, idRule, False)

                                'Se Evalua el valor de la variable seleccion 
                                Select Case selectionvalue
                                    'Caso de trabajo con Estados
                                    Case RulePreferences.HabilitationSelectionState
                                        'Se Obtienen los ids de estados DESHABILITADOS

                                        Dt = WFRulesBusiness.GetRuleOption(TaskResult.StepId, idRule, RuleSectionOptions.Habilitacion, RulePreferences.HabilitationTypeState, 0, True)
                                        'Por cada Id de estado se compara con el id de estado seleccionado, en cada de encontrar
                                        'Coincidencia, se deshabilita la regla
                                        If Not IsNothing(Dt) Then
                                            For Each r As DataRow In Dt.Rows
                                                If Int32.Parse(r.Item("Objvalue").ToString) = state.ID Then
                                                    btn.Visible = False
                                                    AddToDisabledRulesHash(idRule)
                                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Regla DesHabilitada")
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                        'Caso de trabajo con Usuarios o Grupos
                                    Case RulePreferences.HabilitationSelectionUser
                                        Dim ruleEnabled As Boolean = False

                                        'Se Obtienen los ids de USUARIOS DESHABILITADOS
                                        Dt = WFRulesBusiness.GetRuleOption(WFStepId, idRule, RuleSectionOptions.Habilitacion, RulePreferences.HabilitationTypeUser, 0, True)
                                        If Not IsNothing(Dt) Then

                                            'Por cada Id de usuario se compara con el id de usuario logeado, en cada de encontrar
                                            'Coincidencia, se verifican los grupos
                                            For Each r As DataRow In Dt.Rows
                                                ruleEnabled = True
                                                If Int64.Parse(r.Item("Objvalue").ToString) = Membership.MembershipHelper.CurrentUser.ID Then
                                                    ruleEnabled = False
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                        If ruleEnabled = False Then
                                            ruleEnabled = True
                                            'Se obtienen los ids de grupo del usuario y que tienen permiso en la etapa
                                            Dt = WFStepBusiness.GetStepUserGroupsIdsAsDS(WFStepBusiness.GetStepIdByRuleId(idRule, True), Membership.MembershipHelper.CurrentUser.ID)
                                            'Se Obtienen los ids de GRUPOS DESHABILITADOS
                                            Dt2 = WFRulesBusiness.GetRuleOption(TaskResult.StepId, idRule, RuleSectionOptions.Habilitacion, RulePreferences.HabilitationTypeGroup, 0, True)
                                            If Not IsNothing(Dt) AndAlso Not IsNothing(Dt2) Then

                                                'Por cada Grupo del usuario logeado y que tiene permiso la etapa, se fija si esta deshabilitado
                                                For Each rUser As DataRow In Dt.Rows
                                                    ruleEnabled = True
                                                    For Each r As DataRow In Dt2.Rows
                                                        If rUser.Item(0).ToString() = r.Item("Objvalue").ToString() Then
                                                            ruleEnabled = False
                                                            Exit For
                                                        End If
                                                    Next
                                                    'Si el grupo esta habilitado salgo asi queda habilitada la regla
                                                    If ruleEnabled = True Then
                                                        Exit For
                                                    End If
                                                Next
                                            End If
                                        End If

                                        If ruleEnabled = False Then
                                            ZTrace.WriteLineIf(ZTrace.IsInfo, "DesHabilitada")
                                            btn.Visible = False
                                            AddToDisabledRulesHash(idRule)
                                        Else
                                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Habilitada")
                                        End If
                                    Case RulePreferences.HabilitationSelectionIndexAndVariable
                                        'Se obtienen los ids de variables
                                        Dim ruleEnabled As Boolean = True
                                        WFIndexAndVariableBusiness = New WFIndexAndVariableBusiness()
                                        indexsAndVariables = WFIndexAndVariableBusiness.GetIndexAndVariableByRuleID(idRule)

                                        'Se Obtienen los ids de variables DESHABILITADOS
                                        Dt = WFRulesBusiness.GetRuleOption(TaskResult.StepId, idRule, RuleSectionOptions.Habilitacion, RulePreferences.HabilitationTypeIndexAndVariable, 0, True)

                                        If Not IsNothing(Dt) Then
                                            For Each _IndexAndVariable As IndexAndVariable In indexsAndVariables
                                                If validar(_IndexAndVariable, TaskResult, WFIndexAndVariableBusiness) = True Then
                                                    Dt.DefaultView.RowFilter = "ObjValue=" & _IndexAndVariable.ID
                                                    If Dt.DefaultView.ToTable().Rows.Count > 0 Then
                                                        ruleEnabled = False
                                                    Else
                                                        ruleEnabled = True
                                                        Exit For
                                                    End If
                                                End If
                                            Next
                                        End If
                                        If ruleEnabled = False Then
                                            ZTrace.WriteLineIf(ZTrace.IsInfo, "DesHabilitada")
                                            btn.Visible = False
                                            AddToDisabledRulesHash(idRule)
                                        Else
                                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Habilitada")
                                        End If

                                        'Caso de con Usuarios/Grupos, estados e atributos y variables
                                    Case RulePreferences.HabilitationSelectionBoth
                                        Dim ruleEnable As Boolean = True
                                        'Se Obtienen los ids de estados DESHABILITADOS
                                        Dt = WFBusiness.recoverDisableItemsBoth(idRule).Tables(0)

                                        'Filtro por estado
                                        Dt.DefaultView.RowFilter = "ObjValue='" & state.ID & "' and ObjectId in (37,38)"
                                        Dt2 = Dt.DefaultView.ToTable()
                                        If Not IsNothing(Dt) AndAlso Not IsNothing(Dt2) Then

                                            If Dt2.Rows.Count > 0 Then
                                                'Se obtienen los ids de grupo del usuario y que tienen permiso en la etapa
                                                DtUsersAndGroup = WFStepBusiness.GetStepUserGroupsIdsAsDS(WFStepBusiness.GetStepIdByRuleId(idRule, True), Membership.MembershipHelper.CurrentUser.ID)
                                                WFIndexAndVariableBusiness = New WFIndexAndVariableBusiness()
                                                indexsAndVariables = WFIndexAndVariableBusiness.GetIndexAndVariableByRuleID(idRule)

                                                For Each r As DataRow In Dt2.Rows
                                                    'Valido por grupo y usuario
                                                    If Int32.Parse(r.Item("ObjExtraData").ToString) = Membership.MembershipHelper.CurrentUser.ID Then
                                                        ruleEnable = False
                                                        For Each _IndexAndVariable As IndexAndVariable In indexsAndVariables
                                                            If validar(_IndexAndVariable, TaskResult, WFIndexAndVariableBusiness) Then
                                                                Dt.DefaultView.RowFilter = "ObjExtraData='" & _IndexAndVariable.ID & "' and ObjectId =62 and ObjValue ='" & state.ID & "'"

                                                                If Dt.DefaultView.ToTable().Rows.Count > 0 Then
                                                                    ruleEnable = False
                                                                Else
                                                                    ruleEnable = True
                                                                    Exit For
                                                                End If
                                                            End If
                                                        Next
                                                    End If

                                                    For Each rUser As DataRow In DtUsersAndGroup.Rows
                                                        If rUser.Item(0).ToString() = r.Item("ObjExtraData").ToString() Then
                                                            ruleEnable = False
                                                            For Each _IndexAndVariable As IndexAndVariable In indexsAndVariables
                                                                If validar(_IndexAndVariable, TaskResult, WFIndexAndVariableBusiness) Then
                                                                    Dt.DefaultView.RowFilter = "ObjExtraData='" & _IndexAndVariable.ID & "' and ObjectId =62 and ObjValue ='" & state.ID & "'"

                                                                    If Dt.DefaultView.ToTable().Rows.Count > 0 Then
                                                                        ruleEnable = False
                                                                    Else
                                                                        ruleEnable = True
                                                                        Exit For
                                                                    End If
                                                                End If
                                                            Next
                                                        End If
                                                    Next
                                                Next
                                            End If
                                        End If

                                        If ruleEnable = False Then
                                            ZTrace.WriteLineIf(ZTrace.IsInfo, "DesHabilitada")
                                            btn.Visible = False
                                            AddToDisabledRulesHash(idRule)
                                        Else
                                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Habilitada")
                                        End If
                                    Case Else
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Regla sin tab seleccionada")
                                End Select
                            Else
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Regla no procesada por tab " & idRule)
                            End If

                        End If
                    Finally
                        If Not IsNothing(lstRulesEnabled) Then
                            lstRulesEnabled.Clear()
                            lstRulesEnabled = Nothing
                        End If
                        If Not IsNothing(Dt) Then
                            Dt.Dispose()
                            Dt = Nothing
                        End If
                        If Not IsNothing(Dt2) Then
                            Dt2.Dispose()
                            Dt2 = Nothing
                        End If
                        If Not IsNothing(DtUsersAndGroup) Then
                            DtUsersAndGroup.Dispose()
                            DtUsersAndGroup = Nothing
                        End If
                        If Not IsNothing(WFIndexAndVariableBusiness) Then
                            WFIndexAndVariableBusiness = Nothing
                        End If
                        If Not IsNothing(indexsAndVariables) Then
                            For i As Int32 = 0 To indexsAndVariables.Count - 1
                                indexsAndVariables(i) = Nothing
                            Next
                            indexsAndVariables.Clear()
                            indexsAndVariables = Nothing
                        End If
                    End Try
                Next

            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                state = Nothing
                useTabEnable = Nothing
                selectionvalue = Nothing
                If Not IsNothing(lstRulesEnabled) Then
                    lstRulesEnabled.Clear()
                    lstRulesEnabled = Nothing
                End If
                If Not IsNothing(Dt) Then
                    Dt.Dispose()
                    Dt = Nothing
                End If
                If Not IsNothing(Dt2) Then
                    Dt2.Dispose()
                    Dt2 = Nothing
                End If
                If Not IsNothing(DtUsersAndGroup) Then
                    DtUsersAndGroup.Dispose()
                    DtUsersAndGroup = Nothing
                End If
                If Not IsNothing(WFIndexAndVariableBusiness) Then
                    WFIndexAndVariableBusiness = Nothing
                End If
                If Not IsNothing(indexsAndVariables) Then
                    For i As Int32 = 0 To indexsAndVariables.Count - 1
                        indexsAndVariables(i) = Nothing
                    Next
                    indexsAndVariables.Clear()
                    indexsAndVariables = Nothing
                End If
            End Try

            TabDoc.UserActionDisabledRules = UserActionDisabledRules

        End Sub

        Private Function validar(ByVal _IndexAndVariable As IndexAndVariable, ByVal _TaskResult As TaskResult, ByVal IndexAndVariableBusiness As WFIndexAndVariableBusiness) As Boolean
            Dim IndexAndVariableConfList As List(Of IndexAndVariableConfiguration) = IndexAndVariableBusiness.GetIndexAndVariableConfiguration(_IndexAndVariable.ID)
            Dim TextoInteligente As New Core.TextoInteligente()

            Try
                For Each IndexAndVariableConf As IndexAndVariableConfiguration In IndexAndVariableConfList
                    Dim value1 As String = IndexAndVariableConf.Name
                    If IndexAndVariableConf.Manual = "N" Then
                        For Each i As Index In _TaskResult.Indexs
                            If value1 = i.ID Then
                                value1 = i.Data
                                Exit For
                            End If
                        Next
                    Else
                        value1 = WFRuleParent.ReconocerVariablesValuesSoloTexto(value1)
                        value1 = TextoInteligente.ReconocerCodigo(value1, _TaskResult)
                    End If

                    Dim value2 As String = IndexAndVariableConf.Value
                    value2 = WFRuleParent.ReconocerVariablesValuesSoloTexto(value2)
                    value2 = TextoInteligente.ReconocerCodigo(value2, _TaskResult)

                    Dim comparator As Comparadores
                    'Le asigno el comparador al IfIndex
                    Select Case IndexAndVariableConf.Operador
                        Case "="
                            comparator = Comparadores.Igual
                        Case "<>"
                            comparator = Comparadores.Distinto
                        Case "<"
                            comparator = Comparadores.Menor
                        Case ">"
                            comparator = Comparadores.Mayor
                        Case "<="
                            comparator = Comparadores.IgualMenor
                        Case "Contiene"
                            comparator = Comparadores.Contiene
                        Case "Empieza"
                            comparator = Comparadores.Empieza
                        Case "Termina"
                            comparator = Comparadores.Termina
                        Case ">="
                            comparator = Comparadores.IgualMayor
                    End Select
                    ' para mantener la funcionalidad vieja, ingrasamos False como parametro en tmpCaseInsensitive
                    If ToolsBusiness.ValidateComp(value1, value2, comparator, False) = False Then
                        Return False
                    ElseIf _IndexAndVariable.Operador.ToLower() = "or" Then
                        Return True
                    End If
                Next
            Finally
                TextoInteligente = Nothing
            End Try

            Return True
        End Function

        Private Sub AddToDisabledRulesHash(ByVal RuleId As Long)
            If Not UserActionDisabledRules.Contains(RuleId) Then
                UserActionDisabledRules.Add(RuleId)
            End If
        End Sub




        Private Sub SetExpiredDate()
            Try
                If TaskResult.ExpireDate = #12:00:00 AM# Then
                    dtpFecVenc.Visible = False
                Else
                    dtpFecVenc.Text = TaskResult.ExpireDate
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

#Region "Private Properties"
        Private Enum PropietaryTypes
            Mind
            Nobody
            Other
        End Enum
        Private ReadOnly Property PropietaryType() As PropietaryTypes
            Get
                Try
                    If TaskResult.AsignedToId = 0 Then
                        Return PropietaryTypes.Nobody
                    ElseIf TaskResult.AsignedToId = UserBusiness.Rights.CurrentUser.ID Then
                        Return PropietaryTypes.Mind
                    Else
                        Dim users As Generic.List(Of Long) = UserGroupBusiness.GetUsersIds(TaskResult.AsignedToId, True)
                        If users.Contains(Membership.MembershipHelper.CurrentUser.ID) Then
                            Return PropietaryTypes.Mind
                        Else
                            Return PropietaryTypes.Other
                        End If
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End Get
        End Property
#End Region

#Region "Private Methods"
        ''' <summary>
        ''' Habilita o deshabilita los botones basicos de la tarea
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub DisablePropertyControls()
            If isDisposed = True Then
                Exit Sub
            End If

            If Not FlagClosing Then
                btnDerivar.Visible = RightsBusiness.GetUserRights(ObjectTypes.WFSteps, RightsType.Delegates, CInt(TaskResult.StepId))

                'AsignedTo

                lblAsignedTo.Enabled = True


                dtpFecVenc.Enabled = RightsBusiness.GetUserRights(ObjectTypes.WFSteps, RightsType.ModificarVencimiento, CInt(TaskResult.StepId))


                If TaskResult.TaskState <> TaskStates.Ejecucion Then
                    CboStates.Visible = False
                    lblstatedescripciotn.Visible = True
                    lblstatedescripciotn.Text = CboStates.Text
                    lblstatedescripciotn.Enabled = True
                    dtpFecVenc.Enabled = False
                    chkFinishTaskOnClose.Enabled = False
                End If
            End If
        End Sub

        ''' <summary>
        ''' Habilitación y deshabilitación de controles
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Gaston]    01/09/2008  Modified    Llamada al método loadWfStepRules
        '''     [Gaston]    08/01/2009  Modified    Llamada al método que permite ver si la etapa tiene o no permiso para habilitar o deshabilitar el
        '''                                         combo de estados
        ''' </history>
        Private Sub EnablePropietaryControls()
            If isDisposed = True Then
                Exit Sub
            End If
            If Not FlagClosing Then
                Try
                    Dim currentLockedUser As String
                    If TaskResult.TaskState = TaskStates.Servicio OrElse Not WFTaskBusiness.LockTask(TaskResult.TaskId, currentLockedUser) Then
                        ShowExecutedByOtherMessage(currentLockedUser)
                        HideUserActions()
                    Else
                        Dim users As Generic.List(Of Long) = UserGroupBusiness.GetUsersIds(TaskResult.AsignedToId, True)
                        Dim userId As Int64 = Membership.MembershipHelper.CurrentUser.ID

                        If TaskResult.TaskState = TaskStates.Desasignada AndAlso TaskResult.AsignedToId <> 0 Then
                            TaskResult.TaskState = TaskStates.Asignada
                        ElseIf TaskResult.AsignedToId = 0 Then
                            TaskResult.TaskState = TaskStates.Desasignada
                        End If

                        If TaskResult.TaskState = TaskStates.Desasignada AndAlso UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.WFSteps, RightsType.Iniciar, CInt(TaskResult.StepId)) = True Then
                            btnIniciar.Visible = True
                        ElseIf TaskResult.TaskState = TaskStates.Asignada Then
                            If TaskResult.AsignedToId = userId OrElse
                                users.Contains(userId) OrElse
                                UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.WFSteps, RightsType.UnAssign, CInt(TaskResult.StepId)) Then
                                btnIniciar.Visible = True
                            Else
                                ShowExecutedByOtherMessage(currentLockedUser)
                            End If
                        ElseIf TaskResult.TaskState = TaskStates.Ejecucion AndAlso TaskResult.AsignedToId <> userId Then
                            ShowExecutedByOtherMessage(currentLockedUser)
                            If users.Contains(userId) Then
                                btnCantUseTask.Visible = False
                            End If
                        Else
                            btnIniciar.Visible = False
                        End If

                        'UserActions
                        If TaskResult.TaskState = TaskStates.Ejecucion Then
                            If TaskResult.AsignedToId = userId Then
                                enableUserActions(TaskResult)
                                'SI UNA TAREA SE ENCUENTRA EN EJECUCION POR OTRO USUARIO NO DEBE HABILITAR LAS ACCIONES DE USUARIO
                                'LOS PERMISOS EXISTENTES NO AFECTAN ESTA PREMISA
                            ElseIf TaskResult.AsignedToId <> 0 Then
                                Dim userGroupsIDs As List(Of Long) = UserBusiness.GetUserGroupsIdsByUserid(userId)
                                If userGroupsIDs.Contains(TaskResult.AsignedToId) Then
                                    enableUserActions(TaskResult)
                                End If
                            Else
                                HideUserActions()
                            End If
                        Else
                            HideUserActions()
                        End If

                        'Estado
                        If TaskResult.TaskState = Zamba.Core.TaskStates.Ejecucion Then

                            verifyIfAllowActivateOrNoStateComboBox(TaskResult.StepId)

                            If TaskResult.AsignedToId = userId AndAlso RightsBusiness.GetUserRights(ObjectTypes.WFSteps, RightsType.Terminar, CInt(TaskResult.StepId)) Then
                                chkFinishTaskOnClose.Checked = CBool(UserPreferences.getValue("CheckFinishTaskAfterClose", UPSections.WorkFlow, "True"))
                            Else
                                chkFinishTaskOnClose.Enabled = False
                            End If
                            chkCloseTaskAfterDistribute.Enabled = True
                        End If
                    End If

                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End If

        End Sub

        Private Sub enableUserActions(ByVal TaskResult As ITaskResult)
            Dim wfstep As IWFStep
            Try
                wfstep = WFStepBusiness.GetStepById(TaskResult.StepId, False)

                ClearUserActions()
                Dim userActionName As String = String.Empty
                'todo wf falta ver si no se modificaron las reglas y cargarlas de nuevo desde la base en el wfstep
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Reglas obtenidas en las cuales se buscan acciones de usuario: " & wfstep.DSRules.WFRules.Rows.Count)

                Dim DVDSRules As New DataView(wfstep.DSRules.WFRules)
                DVDSRules.RowFilter = "ParentType = 5"  'Rule.ParentType = TypesofRules.AccionUsuario
                For Each RuleRow As DataRow In DVDSRules.ToTable().Rows ' wfstep.Rules
                    Dim ruleid As Int64 = RuleRow("Id")
                    Dim ruleclass As String = RuleRow("Class")
                    Dim rulename As String = RuleRow("Name")

                    Dim RuleEnabled As Boolean
                    If TaskResult.UserRules.ContainsKey(ruleid) Then
                        'Lista que en la posicion 0 guarda si esta habilitada la regla o no
                        'y en la 1 si se acumula a la habilitacion de las solapas o no
                        Dim lstRulesEnabled As List(Of Boolean) = DirectCast(TaskResult.UserRules(ruleid), List(Of Boolean)).ToList()
                        If lstRulesEnabled.Count > 0 Then
                            RuleEnabled = lstRulesEnabled(0)
                        End If
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Contiene Regla en DoEnable Estado " & RuleEnabled.ToString())
                    Else
                        'Obtiene el estado
                        'RuleEnabled = WFRulesBusiness.GetRuleEstado(ruleid, True)
                        RuleEnabled = CBool(RuleRow("Enable"))
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "DoEnable en base de datos" & RuleEnabled.ToString())
                    End If
                    If RuleEnabled = True Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Accion de usuario.")
                        Dim UAB As New System.Windows.Forms.ToolStripButton
                        Dim ZIcon As New ZIconsList

                        UAB.Tag = ruleid

                        'Busca en la tabla si existe un nombre de acción de usuario para esa regla
                        Try
                            Dim WFB As New WFBusiness
                            userActionName = WFB.GetUserActionName(ruleid, wfstep.ID, rulename, True)
                            WFB = Nothing
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                            userActionName = String.Empty
                        End Try


                        'Si el nombre no existe entonces le asigna el nombre de la regla
                        If String.IsNullOrEmpty(userActionName) Then
                            userActionName = rulename
                            If String.Compare(ruleclass, "DOExecuteRule") = 0 AndAlso rulename.StartsWith("Ejecutar Regla ") Then
                                userActionName = rulename.Replace("Ejecutar Regla ", String.Empty)
                            Else
                                userActionName = rulename
                            End If
                        End If
                        UAB.Image = ZIcon.ZIconList.Images(WFRulesBusiness.GetRuleIconBasedOnClass(ruleclass, userActionName))

                        'Asigna el nombre al botón. Si este es mayor que 20 lo corta y le agrega 3 puntos
                        UAB.ToolTipText = userActionName
                        If userActionName.Length > 30 Then
                            UAB.Text = userActionName.Substring(0, 30) & "..."
                        Else
                            UAB.Text = userActionName
                        End If

                        'Guarda el nombre en un hash para luego utilizarlo cuando se llame al saveAction
                        If hshRulesNames.ContainsKey(ruleid) Then
                            hshRulesNames.Item(ruleid) = userActionName
                        Else
                            hshRulesNames.Add(ruleid, userActionName)
                        End If

                        ToolBar1.Items.Add(UAB)
                        If IsUAB(UAB) Then
                            UAB.Visible = True
                        End If
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Se cargó la acción: " & userActionName)
                    End If
                Next

                DVDSRules = Nothing
                userActionName = String.Empty
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                WFStep = Nothing
            End Try
        End Sub
#End Region

#Region "Loads"
        Public Sub SaveAllObjects()
            Try
                TabDoc.ImgViewer.SaveAllObjects()
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

#Region "Guardar atributos"
        'Private Sub SaveIndex(ByVal IndexViewer As UCIndexViewer, ByRef Result As Result, ByVal NoQuestion As Boolean)
        '    Try
        '        If Not IsNothing(result) Then
        '            If Not IsNothing(MisIndices) Then MisIndices.Save(NoQuestion)
        '        End If
        '    Catch ex As Exception
        '        zamba.core.zclass.raiseerror(ex)
        '    End Try
        'End Sub
        'Private Sub FlagTrue()
        'End Sub
        'Private Sub RemoveChanges(ByRef Result As Result)
        '    Try
        '        If Not IsNothing(result) Then
        '            MisIndices.ShowIndexs(result)
        '        End If
        '    Catch ex As Exception
        '        zamba.core.zclass.raiseerror(ex)
        '    End Try
        'End Sub
#End Region

        Public ReadOnly Property Viewer() As UCDocumentViewer2
            Get
                If Not IsNothing(TabDoc) Then
                    Return TabDoc
                Else
                    Return Nothing
                End If
            End Get
        End Property
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="seleccionarTabTarea"></param>
        ''' <param name="lastIndex"></param>
        ''' <history> Marcelo Modified 23/03/07 Se agrego un parametro, que se utiliza para seleccionar una solapa
        ''' <remarks></remarks>
        Private Sub LoadViewer(ByVal seleccionarTabTarea As Boolean, Optional ByVal tabSelect As Int16 = 0)
            ZTrace.WriteLineIf(ZTrace.IsWarning, "Abriendo el visualizador de tareas")
            Try
                If IsNothing(TabDoc) Then
                    'If TaskResult.Doc_File = "" Then WFTasksFactory.CompleteTask(TaskResult)
                    Zamba.Core.DocTypesBusiness.GetEditRights(DirectCast(TaskResult.DocType, DocType))
                    'Llamada al visualizador de documentos
                    TabDoc = New UCDocumentViewer2(Me, DirectCast(TaskResult, TaskResult), False, True, True, False, True, True)
                    TabDoc.Tag = TaskResult.ID
                    TabDoc.Text = TaskResult.Name
                    TabControl1.Controls.Add(TabDoc)
                    RemoveHandler TabDoc.CambiarDock, AddressOf _CambiarDock
                    AddHandler TabDoc.CambiarDock, AddressOf _CambiarDock


                    RemoveHandler TabDoc.CloseOpenTask, AddressOf CloseTaskViewer
                    AddHandler TabDoc.CloseOpenTask, AddressOf CloseTaskViewer
                    RemoveHandler TabDoc.ShowDocumentsAsociated, AddressOf ActivateDocAsociated
                    AddHandler TabDoc.ShowDocumentsAsociated, AddressOf ActivateDocAsociated
                    RemoveHandler TabDoc.ShowAsociatedResult, AddressOf ShowAsociatedResult
                    AddHandler TabDoc.ShowAsociatedResult, AddressOf ShowAsociatedResult


                    RemoveHandler TabDoc.ReplaceDocument, AddressOf ReplaceDocument
                    AddHandler TabDoc.ReplaceDocument, AddressOf ReplaceDocument
                    ''Solo si el control proviene de TAREAS se agregan estos handlers
                    'If String.Compare(Me.Parent.Name, "TabControl1") = 0 Then
                    RemoveHandler TabDoc.ShowOriginal, AddressOf ShowOriginal
                    AddHandler TabDoc.ShowOriginal, AddressOf ShowOriginal
                    'End If

                    ' (pablo) - 28022011
                    RemoveHandler TabDoc.ReloadAsociatedResult, AddressOf ReloadAsociatedResult
                    AddHandler TabDoc.ReloadAsociatedResult, AddressOf ReloadAsociatedResult

                    RemoveHandler TabDoc.RefreshTask, AddressOf RefreshTask
                    AddHandler TabDoc.RefreshTask, AddressOf RefreshTask

                    'Martin: Se comenta esta asignacion, no se encontro codigo que utilice el tag para obtener el resultid, si fuera necesario algo de este tipo es preferible extender mediante herencia el tab y colocarle una propiedad resultid o taskid segun corresponda
                    'Me.TabTarea.Tag = Me.Result.ID
                    If seleccionarTabTarea = True Then
                        TabControl1.SelectedTab = TabDoc
                    Else
                        TabControl1.SelectedIndex = tabSelect
                        TabDoc.ShowDocument(True, False, False, False, True)
                        TabDoc.RefreshData(TaskResult)
                    End If

                Else
                    TabDoc.Text = TaskResult.Name
                    RefreshTask(TaskResult)
                    TabDoc.RefreshData(TaskResult)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Private Delegate Sub ShowIndexsDelegate(ByVal o As Boolean)

        ''' <summary>
        ''' Muestra el documento original.
        ''' </summary>
        ''' <param name="results"></param>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Tomas] - 12/05/2009 - Created - Método modificado de NewDocumentViewer de MainPanel.
        ''' </history>
        Private Sub ShowOriginal(ByRef Result As Result)

            Dim filePath As String
            If Result.FullPath.Length > 0 AndAlso Not File.Exists(Result.FullPath) Then
                filePath = Results_Business.GetDBTempFile(Result)
            Else
                filePath = Result.FullPath
            End If


            If TaskResult.ID = Result.ID Then
                Dim IsOpened As Boolean = False
                Dim TabCounter As Int32 = 0

                'Comprueba si la pestaña se encuentra abierta
                For Each dc As TabPage In TabControl1.TabPages
                    If TypeOf dc Is UCDocumentViewer2 Then
                        Dim zvc As UCDocumentViewer2 = DirectCast(dc, UCDocumentViewer2)
                        If zvc.Result.ID = Result.ID Then
                            TabCounter = TabCounter + 1
                            If TabCounter > 1 Then
                                IsOpened = True
                                Exit For
                            End If
                        End If
                    End If
                Next

                If IsOpened = False Then
                    'Crea la pestaña correspondiente con el documento original
                    TabDoc = New UCDocumentViewer2(Me, Result, False, True, True, False, True, False, True)
                    TabDoc.Dock = DockStyle.Fill
                    TabDoc.Tag = Result.ID
                    TabDoc.SetReadOnly = SetReadOnly
                    'Selecciona la pestaña
                    TabControl1.TabPages.Add(TabDoc)
                    TabControl1.SelectTab(TabDoc)
                    TabDoc.VerOriginalButtonVisible = False
                    'Muestro el documento
                    TabDoc.ShowDocument(False, False, False, False, True)
                    RemoveHandler TabDoc.CambiarDock, AddressOf _CambiarDock
                    AddHandler TabDoc.CambiarDock, AddressOf _CambiarDock
                Else
                    'Selecciona el documento que se encuentra abierto
                    TabControl1.SelectedTab = TabDoc
                End If
            End If

        End Sub

        'Public Event CambiarDock(ByVal Sender As TabPage, ByVal ClosedFromCross As Boolean, ByVal IsMaximize As Boolean)
        Private Sub _CambiarDock(ByVal Sender As TabPage, Optional ByVal ClosedFromCross As Boolean = False, Optional ByVal IsMaximize As Boolean = False)
            PanelsController.CambiarDock(Sender, ClosedFromCross, IsMaximize)
        End Sub

        ''' <summary>
        ''' Actualiza el 
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Tomas]     01/02/2010  Created
        ''' </history>
        Public Sub RefreshAsociatedTask()
            SuspendLayout()
            Dim docViewer As UCDocumentViewer2
            Dim tipoUCDocumentViewer2 As Type = GetType(UCDocumentViewer2)

            For i As Int32 = 0 To TabControl1.TabCount - 1
                If TabControl1.TabPages(i).GetType Is tipoUCDocumentViewer2 Then
                    docViewer = DirectCast(TabControl1.TabPages(i), UCDocumentViewer2)
                    If docViewer.Result.ID = TaskResult.ID _
                       AndAlso (Not IsNothing(docViewer.Controls("pnlPanel"))) Then
                        docViewer.updateDocsAsociated()
                        docViewer = Nothing
                        Exit For
                    End If
                    docViewer = Nothing
                End If
            Next

            tipoUCDocumentViewer2 = Nothing
            docViewer = Nothing
            ResumeLayout()
        End Sub
        ''' <summary>
        ''' Método que sirve para refrescar el formulario. 
        ''' Se buscan las reglas de tipo AbrirDocumento, es decir, que se ejecutan al hacer dobre click
        ''' sobre una tarea
        ''' </summary>
        ''' <param name="results"></param>
        ''' <param name="ComeFromWF">Avisa que el formulario se abrira en tareas</param>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Gaston]	22/08/2008	Modified    Llamada al método verifyRulesDOShowForm
        '''                 01/09/2008  Modified    Llamada al método loadWfStepRules
        '''     [Pablo]	    18/10/2010	Modified    Se agrega un parametro al metodo para avisar que
        '''                                         el formulario se abrira en tareas.
        Private Sub refreshForm(ByVal ComeFromWF As Boolean, ByVal isReloading As Boolean)
            Try
                SuspendLayout()
                Dim ruleShowForm As WFRuleParent = Nothing
                Dim List As New List(Of ITaskResult)
                Dim opendocumentrules As New List(Of WFRuleParent)
                List.Add(TaskResult)

                Try
                    Dim wfstep As IWFStep = WFStepBusiness.GetStepById(TaskResult.StepId, False)
                    Dim DVDSRules As New DataView(wfstep.DSRules.WFRules)
                    DVDSRules.RowFilter = "Type = 39"
                    For Each RuleRow As DataRow In DVDSRules.ToTable().Rows ' wfstep.Rules
                        Dim Rule As IWFRuleParent = WFRulesBusiness.GetInstanceRuleById(RuleRow("Id"), wfstep.ID, True)
                        If (Rule.GetType().FullName <> "Zamba.WFActivity.Regular.DoShowForm" AndAlso Rule.ChildRules.Count = 0) Then
                            Dim WFRB As New WFRulesBusiness
                            WFRB.ExecutePrimaryRule(Rule, List, Nothing)
                            ruleShowForm = Nothing
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "ruleshowform1 = nothing")
                        Else
                            If CheckChildRules(Rule) Is Nothing Then
                                Dim WFRB As New WFRulesBusiness
                                WFRB.ExecutePrimaryRule(Rule, List, Nothing)
                                ruleShowForm = Nothing
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "ruleshowform2 = nothing")
                            Else
                                ruleShowForm = Rule
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "ruleshowform = rule")
                            End If
                        End If
                        If ruleShowForm IsNot Nothing Then
                            If Not opendocumentrules.Contains(ruleShowForm) Then
                                opendocumentrules.Add(ruleShowForm)
                            End If
                        End If
                    Next

                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try

                ZTrace.WriteLineIf(ZTrace.IsVerbose, "opendocumentrules count" & opendocumentrules.Count)
                If opendocumentrules.Count = 0 Then
                    '[pablo] ComeFromWF avisa luego que el formulario se abrirá en tareas
                    'isReloading: si el control se recarga
                    'que no se carguen los botones dinamicos
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "TabShowDocument1")
                    TabDoc.ShowDocument(True, False, False, ComeFromWF, True)
                Else
                    For Each _rule As WFRuleParent In opendocumentrules
                        If Not verifyRulesDOShowForm(List, _rule) Then
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "TabShowDocument2")
                            TabDoc.ShowDocument(True, False, False, ComeFromWF, True)
                        End If
                    Next
                    If Not IsNothing(List) AndAlso List.Count > 0 AndAlso Not IsNothing(List(0)) AndAlso DirectCast(List(0), TaskResult).CurrentFormID <= 0 Then
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "TabShowDocument3")
                        TabDoc.ShowDocument(True, False, False, ComeFromWF, True)
                    End If
                End If

                TabDoc.LoadDynamicButtons(ComeFromWF, isReloading)

            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                ResumeLayout()
            End Try
        End Sub


        ''' <summary>
        ''' Metodo el cual verifica si existe una doshowform entre las reglas.
        ''' </summary>
        ''' <param name="_rule"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        '''      [Ezequiel] - 05/11/10 - Created.
        ''' </history>
        Private Function CheckChildRules(ByVal _rule As IWFRuleParent) As IWFRuleParent
            Dim raux As WFRuleParent = Nothing
            For Each _ruleaux As IRule In _rule.ChildRules
                raux = CheckChildRules(_ruleaux)
            Next
            If _rule.GetType().FullName = "Zamba.WFActivity.Regular.DoShowForm" Then
                raux = _rule
                DirectCast(_rule, IDoShowForm).RuleParentType = TypesofRules.AbrirDocumento
            End If
            Return raux
        End Function

        ''' <summary>
        ''' Método que se encarga de verificar si existe una regla DOShowForm de tipo AbrirDocumento. Si existe, y si la regla es para visualizar 
        ''' documentos no asociados entonces se muestra el formulario configurado en la regla adentro de la solapa
        ''' </summary>
        ''' <param name="results"></param>
        ''' <param name="ruleShowForm"></param>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Gaston]	22/08/2008	Created    
        '''     [Gaston]	06/11/2008	Modified        Si la regla DoShowForm muestra forms. asociados entonces que se puedan ver adentro de la solapa
        '''     Marcelo     24/06/2009  Modified        Se cambio el nombre del metodo
        ''' </history>
        Private Function verifyRulesDOShowForm(ByRef results As System.Collections.Generic.List(Of ITaskResult), ByRef ruleShowForm As WFRuleParent) As Boolean
            Dim WFRB As New WFRulesBusiness
            Dim list As List(Of ITaskResult)

            Try
                ' Si la variable ruleShowForm no es Nothing quiere decir que existe una regla DoShowForm, por lo tanto el formulario normal se debe mostrar
                ' adentro de la solapa, sobreescribiendo al formulario que ya estaba (si es que existe) adentro de la solapa
                If Not (IsNothing(ruleShowForm)) Then
                    ' Se ejecuta la regla DoShowForm ,y devuelve un result, ese result que devuelve tiene que tener el formID que quiero mostrar, pero
                    ' ademas tiene que traer un bool que dice si la regla es para documentos asociados o no, ya que si no es entonces no tengo que colocar 
                    ' el formulario adentro de la solapa
                    list = WFRB.ExecutePrimaryRule(ruleShowForm, results, Nothing)
                    ruleShowForm.Dispose()

                    If (list.Count = 1) Then
                        ' Se hace una llamada al showDocument de tabTarea pasandole como parámetros el FormID de la regla DOShowForm, para que el 
                        ' formulario configurado en la propia regla se pueda mostrar adentro de la solapa
                        TabDoc.ShowDocumentFromDoShowForm(DirectCast(list(0), TaskResult))
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Finally
                WFRB = Nothing
                list = Nothing
            End Try
        End Function

#Region "Split"

        Public Sub Split(ByVal Viewer As TabPage, ByVal Splited As Boolean) Implements Core.IViewerContainer.Split
            Try

                If Not Splited Then
                    TabSecondaryTask.TabPages.Remove(Viewer)
                    TabControl1.TabPages.Add(Viewer)
                    TabControl1.SelectedTab = Viewer

                    If TabSecondaryTask.TabPages.Count = 0 Then
                        SplitTasks.Panel2Collapsed = True
                        SplitTasks.SplitterDistance = SplitTasks.Width
                    End If
                Else
                    TabControl1.TabPages.Remove(Viewer)
                    TabSecondaryTask.TabPages.Add(Viewer)

                    If SplitTasks.Panel2Collapsed = True Then
                        SplitTasks.Panel2Collapsed = False
                        SplitTasks.SplitterDistance = SplitTasks.Panel1.Width / 2
                    End If
                End If

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

#End Region

        Private Sub ShowHistory(ByVal ReloadHistory As Boolean, ByVal ShowOnlyIndexsHistory As Boolean)
            Try
                SuspendLayout()

                If TabHistorial.Controls.Count = 0 Or ShowOnlyIndexsHistory = True Or ReloadHistory = True Then
                    TabHistorial.SuspendLayout()
                    TabHistorial.Controls.Clear()
                    Dim UcTaskHistory As UCTaskHistory
                    If ShowOnlyIndexsHistory Then
                        UcTaskHistory = New UCTaskHistory(CInt(TaskResult.ID), CurrentUserId, ShowOnlyIndexsHistory, CInt(TaskResult.DocTypeId))
                    Else
                        UcTaskHistory = New UCTaskHistory(CInt(TaskResult.TaskId), CurrentUserId, ShowOnlyIndexsHistory, CInt(TaskResult.DocTypeId))
                    End If

                    'Creamos un split'
                    Dim scDatos As System.Windows.Forms.SplitContainer
                    scDatos = New System.Windows.Forms.SplitContainer()
                    scDatos.Orientation = Orientation.Horizontal
                    scDatos.Dock = DockStyle.Fill
                    scDatos.Panel1MinSize = 9
                    scDatos.SplitterDistance = 9
                    scDatos.BackColor = Color.White

                    'Creando los botones
                    Dim btnShowAll As ZButton
                    btnShowAll = New ZButton()
                    btnShowAll.Text = "Historial de Tarea"
                    btnShowAll.Size = New Size(100, 25)
                    btnShowAll.Location = New Point(11, 11)
                    RemoveHandler btnShowAll.Click, AddressOf btnShowAll_Click
                    AddHandler btnShowAll.Click, AddressOf btnShowAll_Click

                    Dim btnShowIndexs As ZButton
                    btnShowIndexs = New ZButton()
                    btnShowIndexs.Text = "Historial de Atributos"
                    btnShowIndexs.Size = New Size(224, 25)
                    btnShowIndexs.Location = New Point(120, 11)
                    RemoveHandler btnShowIndexs.Click, AddressOf btnShowIndexs_Click
                    AddHandler btnShowIndexs.Click, AddressOf btnShowIndexs_Click



                    'Agregar los componentes al panel 1 y 2''
                    scDatos.Panel1.Controls.Add(btnShowAll)
                    scDatos.Panel1.Controls.Add(btnShowIndexs)

                    'scDatos.Panel1.Controls.Add(Me.btnShowAll)
                    'scDatos.Panel1.Controls.Add(Me.btnShowIndexs)
                    'Me.btnShowAll.BringToFront()
                    'Me.btnShowIndexs.BringToFront()


                    scDatos.Panel2.Controls.Add(UcTaskHistory)
                    'Agregar el split al formulario
                    TabHistorial.Controls.Add(scDatos)

                    UcTaskHistory.Dock = DockStyle.Fill

                    TabHistorial.ResumeLayout()
                    RemoveHandler UcTaskHistory._RefreshGrid, AddressOf refreshGrid
                    AddHandler UcTaskHistory._RefreshGrid, AddressOf refreshGrid
                    ReloadHistory = False
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                ResumeLayout(False)
            End Try
        End Sub
#Region "grilla"

        ''' <summary>
        ''' Método que permite visualizar los documentos asociados de la tarea (si es que tiene)
        ''' </summary>
        '''<history> 
        '''Marcelo modified 20/08/2009 
        '''Marcelo Modified 11/11/2009  
        '''</history>
        Private Sub ShowDocAsociated()

            Cursor = Cursors.WaitCursor
            SuspendLayout()

            Try
                If TabAsociated.Controls.Count = 0 Then

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Iniciando visualizacion grilla de documentos asociados a tarea.")


                    'Se crea la grilla asociados
                    asociatedGrid = New UCFusion2(UCFusion2.Modes.AsociatedTasksResults, CurrentUserId, TaskResult, Me)
                    asociatedGrid.inicializarGrilla()
                    asociatedGrid.Dock = DockStyle.Fill
                    TabAsociated.Controls.Add(asociatedGrid)
                    AttachGridEventHandlers()

                    'Traigo los documentos asociados y cargo la grilla.
                    Dim asociatedResults As DataTable = DocAsociatedBusiness.getAsociatedDTResultsFromResult(TaskResult, asociatedGrid.LastPage, False, Nothing)
                    asociatedGrid.FillResults(asociatedResults, Nothing)

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Finalizada carga grilla de documentos asociados a tarea.")

                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                Cursor = Cursors.Default
                ResumeLayout()
            End Try

        End Sub

        '  Public Event currentContextMenuItemClicked(Action As String, listResults As List(Of IResult), ContextMenuContainer As IMenuContextContainer)
        '  Public Event ClosingTask(ztc As UCTaskViewer)


        Private Sub AttachGridEventHandlers()
            RemoveHandler asociatedGrid.ResultDoubleClick, AddressOf ShowAsociatedResult
            RemoveHandler asociatedGrid.CambiarNombre, AddressOf _CambiarNombre
            RemoveHandler asociatedGrid.ExportarAExcel, AddressOf _ExportarAExcel
            RemoveHandler asociatedGrid._RefreshGrid, AddressOf refreshGrid
            AddHandler asociatedGrid.ResultDoubleClick, AddressOf ShowAsociatedResult
            AddHandler asociatedGrid.CambiarNombre, AddressOf _CambiarNombre
            AddHandler asociatedGrid.ExportarAExcel, AddressOf _ExportarAExcel
            AddHandler asociatedGrid._RefreshGrid, AddressOf refreshGrid
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

        Private Sub refreshGrid(ByVal ReloadHistory As Boolean)
            '(pablo) 
            'ReloadHistory = true al presionar actualizar sobre la toolbar 
            If ReloadHistory Then
                ShowHistory(ReloadHistory, False)
            Else
                If TabAsociated.Controls.Count > 0 Then
                    Dim grdDocsAsoc As UCFusion2 = DirectCast(TabAsociated.Controls(0), UCFusion2)
                    Dim asocResults As DataTable = DocAsociatedBusiness.getAsociatedDTResultsFromResult(TaskResult, 0, False, Nothing)
                    grdDocsAsoc.inicializarGrilla()
                    grdDocsAsoc.FillResults(asocResults, Nothing)
                    grdDocsAsoc.Update()
                    grdDocsAsoc.Refresh()
                End If
            End If
        End Sub


        Private Sub _ExportarAExcel(ByRef Result As Result)
            PanelsController._ExportarAExcel(Result)
        End Sub


        Private Sub _Propiedades(ByRef Result As Result)
            PanelsController._ShowResultProperties(Result)
        End Sub


        Private Sub _CambiarNombre(ByRef Result As Result)
            PanelsController._CambiarNombre(Result)
        End Sub

#End Region

        ''' <summary>
        ''' Método que verifica si la etapa tiene permiso para ejecutar documentos asociados que se encuentren en la propia etapa (en caso de que
        ''' el documento asociado este como tarea) 
        ''' </summary>
        ''' <param name="wfstep"></param>
        ''' <remarks></remarks>
        ''' <history> 
        '''     [Gaston]    09/10/2008     Created
        ''' </history>
        Private Sub verifyIfAllowExecuteAssociatedDocuments(ByRef wfstepid As Int64)

            ' Se verifica el estado del checkbox "permitir ejecutar documentos asociados" para
            If (RightsBusiness.GetUserRights(ObjectTypes.WFSteps, RightsType.AllowExecuteAssociatedDocuments, CInt(wfstepid)) = True) Then
                banExecuteAssociatedDocuments = True
            Else
                banExecuteAssociatedDocuments = False
            End If

        End Sub

        ''' <summary>
        ''' Método que verifica si la etapa tiene permiso para habilitar o no el combo de estados
        ''' </summary>
        ''' <param name="wfstepid"></param>
        ''' <remarks></remarks>
        ''' <history> 
        '''     [Gaston]    08/01/2009     Created
        ''' </history>
        Private Sub verifyIfAllowActivateOrNoStateComboBox(ByRef wfstepid As Int64)

            If (RightsBusiness.GetUserRights(ObjectTypes.WFSteps, RightsType.AllowStateComboBox, CInt(wfstepid)) = True) Then
                AllowChangeState = True
            Else
                AllowChangeState = False
            End If

        End Sub



        ''' <summary>
        ''' Método que sirve para visualizar un documento asociado, ya sea como una tarea (si es que está y la etapa tiene permiso) o como formulario
        ''' </summary>
        ''' <param name="asociatedResult"> Documento asociado sobre el que se hizo click </param>
        ''' <remarks></remarks>
        ''' <history> 
        '''     [Gaston]    15/10/2008     Modified
        '''     [Gaston]    20/10/2008     Modified     Si el documento asociado no es una tarea, entonces se visualiza
        ''' </history>
        Private Sub ShowAsociatedResult(ByRef asociatedResult As Result, Optional ByVal selectedIndex As Int16 = -1)
            Try
                If asociatedResult IsNot Nothing Then
                    Results_Business.aCompleteDocument(asociatedResult)
                End If
                ' Si la etapa tiene permiso para ejecutar documentos asociados
                If banExecuteAssociatedDocuments Then
                    ' Busco si el documento asociado es una tarea
                    Dim TaskIDs As Dictionary(Of Int64, Int64) = WFTaskBusiness.GetTaskAndStepIDsByDocId(asociatedResult.ID)
                    Dim dt As DataTable = Nothing

                    ' Si es una tarea, se ejecuta el documento asociado como una tarea, de lo contrario se visualiza
                    If TaskIDs.Count > 0 Then
                        For Each key As Int64 In TaskIDs.Keys
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Buscando permisos del asociado sobre la etapa")
                            dt = WFStepBusiness.GetStepUserGroupsIdsAsDS(TaskIDs(key), CurrentUserId)
                            If dt.Rows.Count > 0 Then
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "El asociado tiene permisos sobre la etapa para abrirse como tarea")
                                OpenDocAsociatedTask(key, TaskIDs(key), asociatedResult.DocTypeId)
                            Else
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "El asociado no tiene permisos sobre la etapa y se abrira como documento")
                                visualizeDocAsociated(asociatedResult, selectedIndex)
                            End If
                        Next

                        If dt IsNot Nothing Then
                            dt.Dispose()
                            dt = Nothing
                        End If
                        TaskIDs.Clear()
                    Else
                        visualizeDocAsociated(asociatedResult, selectedIndex)
                    End If

                    TaskIDs = Nothing
                Else
                    visualizeDocAsociated(asociatedResult, selectedIndex)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Método que sirve para visualizar un documento asociado, ya sea como una tarea (si es que está y la etapa tiene permiso) o como formulario
        ''' </summary>
        ''' <param name="AsociatedResult"> Documento asociado sobre el que se hizo click </param>
        ''' <remarks></remarks>
        ''' <history> 
        '''     [Tomas]     01/02/2010  Modified    Se agrego un handler para refrescar la tarea original del asociado.
        ''' </history>
        Private Sub visualizeDocAsociated(ByRef AsociatedResult As Result, Optional ByVal SelectedIndex As Int16 = 0)

            If (IsNothing(LookOpenedContent(AsociatedResult))) Then

                Zamba.Core.DocTypesBusiness.GetEditRights(DirectCast(AsociatedResult.DocType, DocType))
                Dim UcViewer As New UCDocumentViewer2(Me, AsociatedResult, False, True)

                'Permite refrescar la tarea al cerrar el documento
                RemoveHandler UcViewer.RefreshAsocTask, AddressOf RefreshAsociatedTask
                AddHandler UcViewer.RefreshAsocTask, AddressOf RefreshAsociatedTask

                UcViewer.Name = AsociatedResult.Name
                UcViewer.Tag = AsociatedResult.ID


                If SelectedIndex > 0 Then
                    TabControl1.TabPages.Insert(SelectedIndex, UcViewer)
                Else
                    TabControl1.TabPages.Insert(TabControl1.TabPages.Count, UcViewer)
                End If


                UcViewer.ShowDocument(True, True, False, False, True)

                TabControl1.SelectedTab = UcViewer

            End If
        End Sub


        ''' <summary>
        ''' Método que sirve para ejecutar un documento asociado que esta como tarea 
        ''' </summary>
        ''' <param name="Task"></param>
        ''' <remarks></remarks>
        ''' <history> 
        '''     [Gaston]    15/10/2008     Created
        ''' </history>
        Private Sub OpenDocAsociatedTask(ByVal TaskId As Int64, ByVal StepID As Int64, ByVal docTypeId As Int64)
            PanelsController.ShowTaskViewer(TaskId, StepID, docTypeId)
        End Sub

        ''' <summary>
        ''' Método que sirve para visualizar el tab del documento asociado (en caso de que exista)
        ''' </summary>
        ''' <param name="AsociatedResult"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history> 
        '''     [Gaston]    10/10/2008     Modified     Si el tab existe se visualiza el tab. Se compara el tag del tab con el id del doc. asociado
        ''' </history>
        Private Function LookOpenedContent(ByVal AsociatedResult As Result) As TabPage

            Try

                Dim T As TabPage = Nothing

                For Each tab As TabPage In TabControl1.TabPages

                    If Not (IsNothing(tab.Tag)) Then

                        If (tab.Tag.ToString = AsociatedResult.ID.ToString()) Then
                            T = tab
                            Exit For
                        End If

                    End If

                Next

                'Dim T As TabPage = Me.TabControl1.TabPages.Item(AsociatedResult.ID.ToString)

                If Not IsNothing(T) Then
                    TabControl1.SelectedTab = T
                    Return T
                End If

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

            Return Nothing

        End Function

        Private Sub ActivateDocAsociated(ByVal Id As Integer)

            TabControl1.SelectedTab = TabAsociated

        End Sub
#End Region

#Region "State"
        ''' <summary>
        ''' [Sebastian] 17-09-09 MODIFIED It's Log in when user change the state
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub CboStates_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs)
            Dim State As IWFStepState
            Try
                blnModified = True
                'update the change state
                State = DirectCast(DirectCast(DirectCast(sender, System.Windows.Forms.ToolStripComboBox).SelectedItem, System.Object), Zamba.Core.WFStepState)
                WFTaskBusiness.ChangeState(TaskResult, State)
                'Vuelvo a esconder el combo y mostrar el label
                ToogleComboStates(False)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

#End Region

#Region "MoreInfo"

        Private Sub InstanceMoreInfo()
            Try
                If UC_Info Is Nothing OrElse UC_Info.IsDisposed OrElse Not Controls.Contains(UC_Info) Then
                    UC_Info = New UC_Info(TaskResult)
                    Controls.Add(UC_Info)
                    UC_Info.BringToFront()
                    UC_Info.Focus()
                    'Me.Controls.Remove(UC_Info)
                Else
                    UC_Info.CloseControl()
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

#End Region

#Region "Indexs"
        'Private Sub LnkIndexs_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LnkIndexs.LinkClicked
        '    InstanceIndexs()
        'End Sub
        'Dim WithEvents ucIndexs As ucIndexs
        'Private Sub InstanceIndexs()
        '    Try
        '        ucIndexs = New ucIndexs(Result)
        '        Dim p As New Point
        '        p.X = Me.LnkIndexs.Location.X + Me.LnkIndexs.Width - ucIndexs.Width
        '        p.Y = Me.PanelBottom.Location.Y - ucIndexs.Height
        '        ucIndexs.Location = p
        '        Me.Controls.Add(ucIndexs)
        '        ucIndexs.BringToFront()
        '        ucIndexs.Focus()
        '    Catch ex As Exception
        '        zamba.core.zclass.raiseerror(ex)
        '    End Try
        'End Sub
#End Region

#Region "CommonActions"

#Region "Iniciar"

        ''' <summary>
        ''' Botón que se ejecuta al hacer click sobre el botón Iniciar
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Gaston]    01/09/2008  Modified    Llamada al método loadWfStepRules
        ''' </history>
        Private Sub btnIniciar_Click(ByVal btnIniciarClicked As Boolean)
            Dim users As Generic.List(Of Long)
            Dim dt As DataTable
            Dim Results As List(Of Core.ITaskResult)
            Dim WFRulesBusiness As WFRulesBusiness

            Try
                blnModified = True
                users = UserGroupBusiness.GetUsersIds(TaskResult.AsignedToId, True)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se inicia la tarea con ID " & TaskResult.ID)
                'Si la tarea no esta asignada, esta asignada al usuario o asignada a algun grupo del usuario o tengo el permiso de desasignar
                If TaskResult.AsignedToId = 0 OrElse TaskResult.AsignedToId = Membership.MembershipHelper.CurrentUser.ID OrElse users.Contains(Membership.MembershipHelper.CurrentUser.ID) OrElse (TaskResult.TaskState = Zamba.Core.TaskStates.Asignada AndAlso UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.WFSteps, RightsType.UnAssign, CInt(TaskResult.StepId)) = True) Then
                    Dim HasToAsign As Boolean
                    If TaskResult.AsignedToId = 0 Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "- La tarea esta desasignada")
                        HasToAsign = True
                    ElseIf users.Contains(Membership.MembershipHelper.CurrentUser.ID) Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "- Se asigna la tarea a " & Membership.MembershipHelper.CurrentUser.Name)
                        dt = WFStepBusiness.getTypesOfPermit(TaskResult.StepId, False, TypesofPermits.DontAsignTaskAsignedToGroup)
                        If dt.Rows.Count > 0 Then
                            If dt.Rows(0)(2) = False Then
                                HasToAsign = True
                            End If
                        Else
                            HasToAsign = True
                        End If
                    ElseIf TaskResult.AsignedToId = Membership.MembershipHelper.CurrentUser.ID Then
                    ElseIf (TaskResult.TaskState = Zamba.Core.TaskStates.Asignada AndAlso UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.WFSteps, RightsType.UnAssign, CInt(TaskResult.StepId)) = True) Then
                        HasToAsign = True
                    End If

                    Results = New System.Collections.Generic.List(Of Core.ITaskResult)
                    Results.Add(TaskResult)

                    If HasToAsign Then
                        WFTaskBusiness.Asign(DirectCast(TaskResult, TaskResult), Membership.MembershipHelper.CurrentUser.ID, Membership.MembershipHelper.CurrentUser.ID, True, False)
                        Try
                            Dim wfstep As IWFStep = WFStepBusiness.GetStepById(TaskResult.StepId, False)
                            Dim DVDSRules As New DataView(wfstep.DSRules.WFRules)
                            DVDSRules.RowFilter = "Type = 34"
                            For Each RuleRow As DataRow In DVDSRules.ToTable().Rows ' wfstep.Rules
                                Dim Rule As IWFRuleParent = WFRulesBusiness.GetInstanceRuleById(RuleRow("Id"), wfstep.ID, True)
                                Dim WFRB As New WFRulesBusiness
                                WFRB.ExecutePrimaryRule(Rule, Results, Nothing)
                                WFRB = Nothing
                            Next
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try
                    End If

                    'La coleccion de tareas se pasa por referencia
                    TaskResult.TaskState = TaskStates.Ejecucion
                    SetAsignedAndSituationLabels(TaskResult)
                    EnableFinishTaskOnClose(TaskResult)
                    WFRulesBusiness = New WFRulesBusiness
                    WFRulesBusiness.ExecuteStartRules(Results)

                    SetAsignedTo()
                    GetStatesOfTheButtonsRule()
                    '  Se agrega boolean en el refreshtask , y se coloca en el iniciar la validacion ya que llama al logueo de la accion iniciar al momento de refrescar la tarea y al presionar el boton iniciar. JB
                    If Not fromRefreshTask Then
                        WFTaskBusiness.Iniciar(DirectCast(TaskResult, TaskResult))
                    End If

                Else

                    SetAsignedAndSituationLabels(TaskResult)
                    'Tomas: se valida que el usuario no sea el que genero la tarea (wi:6753)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando que el 'asignado por' sea diferente del usuario actual")
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Asignado por: " & TaskResult.AsignedById & vbCrLf & "Asignado a: " & TaskResult.AsignedToId)
                    If TaskResult.AsignedToId <> Membership.MembershipHelper.CurrentUser.ID AndAlso Not btnIniciarClicked Then
                        ZTrace.WriteLineIf(ZTrace.IsWarning, "El usuario no tiene permiso para iniciar la tarea o la misma esta siendo utilizada por otro usuario")
                        MessageBox.Show("El usuario no tiene permiso para iniciar la tarea o la misma esta siendo utilizada por otro usuario")
                    End If
                    Exit Sub
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                If Not IsNothing(users) Then
                    users.Clear()
                    users = Nothing
                End If
                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If
                If Not IsNothing(Results) Then
                    Results.Clear()
                    Results = Nothing
                End If
                If Not IsNothing(WFRulesBusiness) Then
                    WFRulesBusiness = Nothing
                End If
            End Try
        End Sub

        Private Sub UpdateGUITaskAsignedSituation(ByVal AsignedTaskResult As TaskResult)
            'Si la tarea esta asignada a algun usuario, va en negrita
            If AsignedTaskResult.AsignedToId <> 0 Then
                If AsignedTaskResult.TaskState = TaskStates.Desasignada Then
                    AsignedTaskResult.TaskState = TaskStates.Asignada
                    If Not IsNothing(TaskResult) Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Se marca la tarea como asignada")
                        TaskResult.TaskState = TaskStates.Asignada
                        WFTaskBusiness.Asign(DirectCast(TaskResult, ITaskResult), TaskResult.AsignedToId, TaskResult.AsignedById, False, False)
                    End If
                    UpdateGUITaskSituation(AsignedTaskResult)
                End If
                lblAsignedTo.Text = UserGroupBusiness.GetUserorGroupNamebyId(AsignedTaskResult.AsignedToId)
                lblAsignedTo.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
            Else
                lblAsignedTo.Text = "[Ninguno]"
                lblAsignedTo.Font = New Font("Tahoma", 8.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
            End If
        End Sub

        Private Sub UpdateGUITaskSituation(ByVal AsignedTaskResult As TaskResult)
            lblSituation.Text = AsignedTaskResult.TaskState.ToString
        End Sub



#End Region

#Region "Finalizar"
        Private Sub btnTerminar_Click()
            Try
                Dim asignedId As Int64 = TaskResult.AsignedToId
                If chkFinishTaskOnClose.Checked AndAlso RightsBusiness.GetUserRights(ObjectTypes.WFSteps, RightsType.Terminar, CInt(TaskResult.StepId)) Then
                    TaskResult.TaskState = TaskStates.Desasignada
                    TaskResult.UsuarioAsignadoId = 0
                    TaskResult.AsignedToId = 0
                    WFTaskBusiness.Finalizar(TaskResult, asignedId)
                Else
                    TaskResult.TaskState = TaskStates.Asignada
                    WFTaskBusiness.Finalizar(TaskResult, asignedId)
                End If

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

#End Region

#Region "Derivar"
        ''' <summary>
        ''' Deriva la tarea
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub btnDerivar_Click() Handles btnDerivar.Click

            If (TaskResult.TaskState = TaskStates.Ejecucion AndAlso TaskResult.TaskState = TaskStates.Servicio) AndAlso
                 TaskResult.AsignedToId <> Membership.MembershipHelper.CurrentUser.ID Then
                If MessageBox.Show("La tarea esta en ejecucion por otro usuario, desea derivarla de todas formas?", "Atencion", MessageBoxButtons.YesNo) = DialogResult.No Then
                    Exit Sub
                End If
            End If

            Dim f As New Form
            Dim ucAssign As UCAsignar
            Try
                Dim currentLockedUser As String
                If WFTaskBusiness.LockTask(TaskResult.TaskId, currentLockedUser) Then
                    ucAssign = New UCAsignar(TaskResult, UCAsignar.AsignTypes.Asignar)

                    f.DialogResult = DialogResult.Cancel
                    f.FormBorderStyle = FormBorderStyle.Fixed3D
                    f.Text = "Derivar"
                    f.MaximizeBox = False
                    f.StartPosition = FormStartPosition.CenterScreen
                    f.Controls.Add(ucAssign)
                    ucAssign.Dock = DockStyle.Fill
                    f.Size = New Drawing.Size(320, 260)

                    If f.ShowDialog() = DialogResult.OK Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Derivando tarea")
                        'todo agregar validacion que se haya realizado una derivacion
                        Try

                            Dim Results As New System.Collections.Generic.List(Of Core.ITaskResult)
                            Results.Add(TaskResult)
                            Dim wfstep As IWFStep = WFStepBusiness.GetStepById(TaskResult.StepId, False)
                            Dim DVDSRules As New DataView(wfstep.DSRules.WFRules)
                            DVDSRules.RowFilter = "Type = 33"
                            For Each RuleRow As DataRow In DVDSRules.ToTable().Rows ' wfstep.Rules
                                Dim Rule As IWFRuleParent = WFRulesBusiness.GetInstanceRuleById(RuleRow("Id"), wfstep.ID, True)
                                Dim WFRB As New WFRulesBusiness
                                WFRB.ExecutePrimaryRule(Rule, Results, Nothing)
                                WFRB = Nothing
                            Next

                            Results.Clear()
                            Results = Nothing
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try
                        blnModified = True
                        CloseTaskViewer(True)
                    End If
                Else
                    ShowExecutedByOtherMessage(currentLockedUser)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally

                If f IsNot Nothing Then
                    f.Dispose()
                    f = Nothing
                End If
                If Not IsNothing(ucAssign) Then
                    ucAssign.Dispose()
                    ucAssign = Nothing
                End If
            End Try

        End Sub



#End Region


#Region "Cerrar"
        Private FlagClosing As Boolean
        Private blnDisposeResult As Boolean = True
        Public Function CloseTaskViewer(ByVal blnDisposeRes As Boolean) As Boolean
            FlagClosingTV = True
            SuspendLayout()
            blnDisposeResult = blnDisposeRes
            Try
                If Not IsNothing(TabDoc) Then
                    If Not IsNothing(TabControl1) Then
                        'Remover este evento evita que se cargue el contenido de alguna solapa al querer cerrar la tarea
                        RemoveHandler TabControl1.SelectedIndexChanged, AddressOf TabControl1_SelectedIndexChanged
                    End If
                    TabDoc.CloseDocument()
                End If

                If CloseTab(True) Then
                    If Not IsNothing(TabControl1) Then
                        'Remover este evento evita que se cargue el contenido de alguna solapa al querer cerrar la tarea
                        RemoveHandler TabControl1.SelectedIndexChanged, AddressOf TabControl1_SelectedIndexChanged
                    End If

                    '  Dispose()
                    'Remueve elementos tomados en la memoria. Esta comprobado que funciona, por ende NO BORRAR.
                    GC.Collect()
                End If

                ' RaiseEvent ClosingTask(Me)
                PanelsController.ClosingTaskInTaskDetails(Me)
                Return True
            Finally
                blnDisposeResult = True
                ResumeLayout()
            End Try
        End Function
        ''' <summary>
        ''' Metodo que se ejecuta para cerrar la tarea
        ''' </summary>
        ''' <param name="refreshGrid"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        '''     Javier  25/01/2011  Modified    Se agrega llamada para cerrar los tabs abiertos de asociados
        Private Function CloseTab(Optional ByVal refreshGrid As Boolean = False) As Boolean
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Realizando el cerrado de la tarea")


            Cursor = Cursors.WaitCursor
            SuspendLayout()
            Dim blnClose As Boolean
            Try
                If Viewer.ValidateForm Then
                    MessageBox.Show("!Atencion! No ha guardado los cambios realizados", "Atencion", MessageBoxButtons.OK)
                    Exit Function
                End If
                'Si se cierra el explorador de la DoExecuteExplorer, limpio todas las variables
                If CloseDoExplorerBrowser() Then
                    CerrarDocsAsociados()

                    If Not FlagClosing Then
                        Dim IsLastTab As Boolean = False
                        FlagClosing = True

                        If TaskResult IsNot Nothing Then
                            WFTaskBusiness.UnLockTask(TaskResult.TaskId)

                            Dim users As Generic.List(Of Long) = UserGroupBusiness.GetUsersIds(TaskResult.AsignedToId, True)
                            If TaskResult.AsignedToId = Membership.MembershipHelper.CurrentUser.ID OrElse users.Contains(Membership.MembershipHelper.CurrentUser.ID) Then
                                btnTerminar_Click()
                            End If
                            users = Nothing

                            RemoveHandler TabControl1.SelectedIndexChanged, AddressOf TabControl1_SelectedIndexChanged
                            If blnModified = True Then
                                PanelsController.CloseTaskViewer(TaskResult, refreshGrid)
                            End If
                        End If

                        '(pablo) - Valido si es la ultima tarea a cerrar en modo pantalla
                        ' completa, si lo es, cierro el tab y vuelvo al modo normal       
                        If Parent IsNot Nothing AndAlso Parent.Parent IsNot Nothing Then
                            Dim TP As TabPage = DirectCast(Parent, TabPage)
                            Dim TC As TabControl = DirectCast(Parent.Parent, TabControl)

                            If TC.Controls.Count = 1 Then
                                IsLastTab = True
                            Else
                                Dim Index As Integer

                                Index = TC.SelectedIndex()
                                If Index = 0 Then
                                    TC.SelectedTab = TC.TabPages.Item(Index + 1)
                                Else
                                    TC.SelectedTab = TC.TabPages.Item(Index - 1)
                                End If

                                TC.SelectedTab.Show()
                            End If

                            TC.TabPages.Remove(TP)
                            TP = Nothing
                        End If

                        If IsLastTab Then
                            PanelsController.CloseFullScreen(False)
                        End If

                    End If
                    Dispose()
                    Return True
                Else
                    blnClose = False
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                Cursor = Cursors.Default
            End Try

            Return blnClose
            'todo: implementar el execute close y verificar que este el execute open
        End Function

        ''' <summary>
        ''' Cierra todos los document viewers de asociados abiertos
        ''' </summary>
        ''' <history>
        '''     Javier  25/01/2011  Created
        ''' </history>
        Public Sub CerrarDocsAsociados()
            Dim tipoUCDocumentViewer2 As Type = GetType(UCDocumentViewer2)
            Dim docviewer As UCDocumentViewer2

            If TabControl1 IsNot Nothing Then
                For i As Int32 = TabControl1.TabPages.Count - 1 To 0 Step -1
                    If TabControl1.TabPages(i).GetType Is tipoUCDocumentViewer2 Then
                        docviewer = DirectCast(TabControl1.TabPages(i), UCDocumentViewer2)
                        If docviewer.Tag <> TabDoc.Tag Then
                            docviewer.CloseDocument()
                        End If
                        docviewer.Dispose()
                        docviewer = Nothing
                    Else
                        Exit For
                    End If
                Next
            End If
        End Sub

#End Region



#End Region

#Region "UserActions"

        Private _userActionDisabledRules As New List(Of Long)

        Private Property UserActionDisabledRules() As List(Of Long)
            Get
                Return _userActionDisabledRules
            End Get
            Set(ByVal value As List(Of Long))
                _userActionDisabledRules = value
            End Set
        End Property

        Private Shadows Property Name As String Implements IViewerContainer.Name
            Get
                Return MyBase.Name
            End Get
            Set(value As String)
                MyBase.Name = value
            End Set
        End Property

        Private Property currentContextMenu As UCGridContextMenu

        Private Sub ClearUserActions()
            Try
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Limpiando acciones de usuario")
                ToolBar1.Invalidate()
                Dim Btn2Remove As New ArrayList
                For Each B As ToolStripItem In ToolBar1.Items
                    If IsUAB(B) Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Quitando visibilidad " & B.Text)
                        B.Visible = False
                        Btn2Remove.Add(B)
                    End If
                Next
                For i As Int32 = 0 To Btn2Remove.Count - 1
                    ToolBar1.Items.Remove(DirectCast(Btn2Remove(i), ToolStripItem))
                Next
                ToolBar1.Update()

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Private Sub HideUserActions()
            Try
                For Each B As ToolStripItem In ToolBar1.Items
                    If IsUAB(B) Then
                        B.Visible = False
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Ocultando visibilidad " & B.Text)
                    End If

                Next
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Private Class UserActionButton
            Inherits ZButton
            Public Rule As WFRuleParent
            Sub New(ByRef rule As WFRuleParent, ByVal i As Integer)

                Me.Rule = rule
                Font = New Font("Verdana", 8, FontStyle.Regular)
                Size = New System.Drawing.Size(74, 38)
                If i = 0 Then
                    Location = New System.Drawing.Point(0, 4)
                Else
                    Location = New System.Drawing.Point(i * 77, 4)
                End If
                TabIndex = 0
                Text = rule.Name.ToUpper
                'If Rule.Name.Length < 30 Then
                '    Me.Text = Rule.Name
                'Else
                '    Me.Text = Rule.Name.Substring(0, 30) & "..."
                'End If
                Dim T As New ToolTip
                T.SetToolTip(Me, rule.Name.ToUpper)

            End Sub
        End Class
#End Region

        Private Sub ToolBar1_ButtonClick(ByVal sender As System.Object, ByVal e As ToolStripItemClickedEventArgs) Handles ToolBar1.ItemClicked
            Try
                If Viewer.ValidateForm Then
                    MessageBox.Show("!Atencion! No ha guardado los cambios realizados.", "Atencion", MessageBoxButtons.OK)
                    Exit Sub
                Else
                    If e.ClickedItem.Tag IsNot Nothing Then
                        Select Case e.ClickedItem.Tag.ToString.ToUpper
                            Case "LABEL_ESTADO"
                                ToogleComboStates(True)
                            Case "INICIAR"
                                '[AlejandroR] 19/05/2011 - WI 6672
                                'Si dos usuarios estan viendo la misma grilla de tareas, uno de ellos abre una y pasa a ejecucion pero para
                                'el segundo usuario sigue desasignada y le permite abrirla e iniciarla, por eso antes de iniciar se 
                                'vuelve a traer la MISMA tarea para actualizar el estado y volver a chequear si se permiete o no iniciar

                                'se guardan las reglas que se deshabilitan por doenable y se recuperan despues de obtener la tarea de la base
                                Dim hsUserRules As Hashtable = TaskResult.UserRules
                                TaskResult = New WFTaskBusiness().GetTaskByTaskIdAndDocTypeIdAndStepId(TaskResult.TaskId, TaskResult.DocTypeId, TaskResult.StepId, 0)
                                TaskResult.UserRules = hsUserRules.Clone()
                                Dim currentLockedUser As String
                                If WFTaskBusiness.LockTask(TaskResult.TaskId, currentLockedUser) Then
                                    btnIniciar_Click(False)
                                End If
                            Case "DERIVAR"
                                btnDerivar_Click()
                            Case "CERRAR"
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Usuario presiono cerrar tarea")
                                CloseTaskViewer(True)
                            '[Ezequiel] 07/05/09 - Se agrego la funcionalidad de la opcion quitar que se encuentra en permisos por etapas
                            Case "MENSAJE"
                            Case Else
                                Try
                                    blnModified = True
                                    ExecutingRule = True
                                    Dim Results As New System.Collections.Generic.List(Of Core.ITaskResult)
                                    Results.Add(TaskResult)
                                    Cursor.Current = Cursors.WaitCursor
                                    ToolBar1.Enabled = False
                                    Dim WFRB As New WFRulesBusiness
                                    'Se loguea la accion de usuario
                                    Dim ruleid As Int64 = e.ClickedItem.Tag

                                    Dim currentLockedUser As String
                                    If WFTaskBusiness.LockTask(TaskResult.TaskId, currentLockedUser) Then
                                        WFTaskBusiness.LogUserAction(TaskResult.TaskId, TaskResult.Name, TaskResult.DocTypeId, TaskResult.DocType.Name, TaskResult.StepId, TaskResult.State.Name, TaskResult.WorkId, hshRulesNames(ruleid).ToString)
                                        Dim Rule As IWFRuleParent = WFRulesBusiness.GetInstanceRuleById(ruleid, True)
                                        Results = WFRB.ExecutePrimaryRule(Rule, Results, Nothing)
                                        WFRB = Nothing
                                        If Not IsNothing(Results) Then
                                            Results.Clear()
                                            Results = Nothing
                                        End If
                                    Else
                                        ShowExecutedByOtherMessage(currentLockedUser)
                                    End If


                                Catch ex As Exception
                                    ZClass.raiseerror(ex)

                                    MessageBox.Show(ex.Message, "Error en la ejecución", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Finally
                                    Cursor.Current = Cursors.AppStarting
                                    If isDisposed = False Then
                                        ToolBar1.Enabled = True
                                        ExecutingRule = False
                                    End If
                                End Try
                        End Select
                    End If
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Metodo para intercambiar visualizacion entre el label y el combo de estados.
        ''' </summary>
        ''' <param name="showCombo">Para saber si muestra el combo o muestra el label.</param>
        Private Sub ToogleComboStates(showCombo As Boolean)
            If AllowChangeState Then
                If showCombo Then
                    lblstatedescripciotn.Visible = False
                    CboStates.Visible = True
                    CboStates.Enabled = True
                Else
                    CboStates.Visible = False
                    lblstatedescripciotn.Text = CboStates.Text
                    lblstatedescripciotn.Enabled = True
                    lblstatedescripciotn.Visible = True
                End If
            End If
        End Sub

        Private Sub UCTaskViewer_CloseCtrl() Handles Me.CloseCtrl
            If ToolBar1 IsNot Nothing Then
                ToolBar1.Dispose()
            End If
        End Sub

        Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles TabControl1.SelectedIndexChanged
            If FlagClosingTV = False Then
                Select Case DirectCast(sender, TabControl).SelectedTab.Text.ToUpper
                    Case "DOCUMENTOS ASOCIADOS"
                        ShowDocAsociated()
                    Case "HISTORIAL"
                        ShowHistory(False, False)
                    Case "FORO"
                        LoadForo()
                    Case "HISTORIAL DE EMAILS"
                        If (Not IsNothing(TaskResult)) AndAlso TaskResult.ID <> 0 Then
                            ShowHistorialEmails(TaskResult)
                        End If
                    Case Else
                        If DirectCast(sender, TabControl).SelectedTab.Text.ToUpper.Contains("FORO") Then
                            LoadForo()
                        End If
                End Select
            End If
        End Sub

        Private Sub pctTaskInfo_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles pctTaskInfo.Click
            Try
                InstanceMoreInfo()
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Muestra la solapa de foro
        ''' </summary>
        ''' [Sebastian] 02-11-2009 MODIFIED se cambio la seleccion de la solapa foro por el nombre de la solapa.
        ''' <history>
        ''' </history>
        ''' <remarks></remarks>
        Private Sub ShowForo()
            'Selecciono el tab de foro
            '[Sebastian] Se comento porque debido al cambio que se hizo para ocultar la solapa de foro
            ' cuando se hacia clic sobre el boton de foro te enviaba a otra solapa
            'Me.TabControl1.SelectTab(2)
            If TabControl1.TabPages.Contains(TabForo) = True Then
                TabControl1.SelectTab(TabForo)
            End If
        End Sub



        ''' <summary>
        ''' Guarda la fecha de vencimiento
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        ''' <history>
        ''' [Ezequiel] 06/05/09 - Created.
        'Private Sub dtpFecVenc_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFecVenc.ValueChanged
        '    Try
        '        WFTaskBusiness.ChangeExpireDate(Me.TaskResult, dtpFecVenc.Value)
        '    Catch ex As Exception
        '        ZClass.raiseerror(ex)
        '    End Try
        'End Sub

        Private Sub chkCloseTaskAfterDistribute_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkCloseTaskAfterDistribute.CheckedChanged
            UserPreferences.setValue("CloseTaskAfterDistribute", chkCloseTaskAfterDistribute.Checked.ToString, UPSections.WorkFlow)
        End Sub

        Private Sub pctWFStepHelp_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles pctWFStepHelp.Click
            If Not IsNothing(TaskResult) Then
                Try
                    If UC_Help Is Nothing OrElse UC_Help.IsDisposed OrElse Not Controls.Contains(UC_Help) Then
                        Dim wfstep As IWFStep = WFStepBusiness.GetStepById(TaskResult.StepId, False)
                        UC_Help = New UCWFHelp(wfstep.Name, wfstep.Help)
                        Controls.Add(UC_Help)
                        UC_Help.BringToFront()
                        UC_Help.Focus()
                    Else
                        UC_Help.CloseControl()
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End If
        End Sub

        Private Function IsUAB(ByRef b As ToolStripItem)
            If IsNumeric(b.Tag) Then
                Return True
            Else
                Return False
            End If
        End Function

#Region "Interaccion con Reglas"
        '[Pablo] 04/11/2010 creada para almacenar el ruleId de la regla PlayDoExecuteExplorer
        Private RuleId As Int64
        '[Pablo] 04/11/2010 creada para almacenar el listado de results de la regla PlayDoExecuteExplorer
        Private ParamResult As System.Collections.Generic.List(Of Core.ITaskResult)
        '[Pablo] 04/11/2010 creada para almacenar el WebBrowser de la regla PlayDoExecuteExplorer
        Private WithEvents ParamWB As WebBrowser
        '[Pablo] 04/11/2010 creada para almacenar el alto de la regla PlayDoExecuteExplorer
        Private ParamHeight As Int16
        '[Pablo] 04/11/2010 creada para almacenar el ancho de la regla PlayDoExecuteExplorer
        Private ParamWidth As Int16
        '[Pablo] 04/11/2010 creada para determinar si la regla se ejecuta por fuera o por dentro
        Private WayToOpen As Boolean
        '[Pablo] 04/11/2010 creada para determinar si la regla debe continuar o no
        Private ContinueWithRule As Boolean
        '[Pablo] 04/11/2010 creada para determinar si la regla debe continuar o no
        Private UCWidth As Int16
        '[Pablo] 04/11/2010 creada para determinar si la regla debe continuar o no

        '[Pablo] 04/11/2010 creada para determinar si la regla debe cambiar la  visualizacion
        Private changeVisuals As Boolean
        '[Pablo] 04/11/2010 guarda el alto del split container
        '[Pablo] 04/11/2010 determina en que posicion cerre el browser
        Private ItsHorizontalVisualization As Boolean
        '[Pablo] 04/11/2010 determina si el primer combo esta activo
        Private EvaluateRuleID As Int64
        '[Pablo] 04/11/2010 guardo el ID del combo CBOElse
        Private ExecuteElseID As Int32
        '[Pablo] 04/11/2010 determina si la seccion de validacion de condicion de cierre esta activada
        Private habilitar As Boolean
        Private CloseUrlTextbox As Boolean
        Private lista As List(Of ITaskResult)
        Private WFRS As WFRulesBusiness
        Private WFStepId As Int64
        Private rulevalue, TxtVar, Operador, Value As String
        Private BtnchangeVisualization As New Button
        Private BtnClose As New Button
        Private BtnFullScreen As New Button
        Private BtnShowUrl As New Button
        Private Webform As New Form
        Private SplitTopOfBrowser As New System.Windows.Forms.SplitContainer
        Private SplitButton As New System.Windows.Forms.SplitContainer
        Private SplitCloseButton As System.Windows.Forms.SplitContainer
        Private TaskViewerSplit As New System.Windows.Forms.SplitContainer
        Private Url As TextBox

        ''' <summary> 
        ''' Activa el SplitContainer para visualizar un WebBrowser 
        ''' </summary> 
        ''' <remarks></remarks> 
        ''' <history>   [Pablo] 13/10/2010 Created</history> 
        Public Sub OpenBrowser(ByVal params As Hashtable)
            Cursor = Cursors.WaitCursor

            SuspendLayout()

            'Dim OnClearIECacheKeepSpecifiedFiles As String = UserPreferences.getValue("OnClearIECacheKeepSpecifiedFiles", Sections.FormPreferences, "True") 
            'Dim CachePreserveList As String = UserPreferences.getValue("ClearIECacheList", Sections.FormPreferences, String.Empty) 
            'Dim Files As List(Of String) = New List(Of String) 

            'If Not String.IsNullOrEmpty(CachePreserveList) Then 
            '    For Each f As String In CachePreserveList.Split(",") 
            '        Files.Add(f.Trim()) 
            '    Next 
            'End If 

            'If Files.Count = 0 Then 
            '    Zamba.IETools.IECache.ClearCacheFilesFull() 
            'Else 
            '    If OnClearIECacheKeepSpecifiedFiles = "True" Then 
            '        Zamba.IETools.IECache.ClearCacheFilesExceptSelected(Files) 
            '    Else 
            '        Zamba.IETools.IECache.ClearCacheFilesOnlySelected(Files) 
            '    End If 
            'End If 

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Limpiando Cache")
            IECache.ClearCache()
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Caché limpiada")

            RuleId = Int32.Parse(params.Item("ruleId").ToString())
            ParamResult = params.Item("paramResult")

            ParamHeight = params.Item("height")
            ParamWidth = params.Item("width")
            WayToOpen = params.Item("WayToOpen")
            ContinueWithRule = params.Item("ContinueWithRule")
            UCWidth = params.Item("UCWidth")
            params.Item("BrowserCloser") = False
            changeVisuals = False
            EvaluateRuleID = params.Item("EvaluateRuleID")
            ExecuteElseID = params.Item("ExecuteElseID")
            habilitar = params.Item("Habilitar")
            TxtVar = params.Item("TxtVar")
            Operador = params.Item("Operador")
            Value = params.Item("Value")

            Try

                If Not ParamWB Is Nothing Then
                    ParamWB.Dispose()
                    ParamWB = Nothing
                End If

                ParamWB = New WebBrowser
                ParamWB.Dock = DockStyle.Fill

                SplitTopOfBrowser.Panel1.Controls.Clear()
                SplitTopOfBrowser.Panel2.Controls.Clear()
                SplitButton.Panel1.Controls.Clear()
                SplitButton.Panel2.Controls.Clear()
                BtnchangeVisualization.Controls.Clear()
                BtnClose.Controls.Clear()
                BtnFullScreen.Controls.Clear()
                TaskViewerSplit.Panel2.Controls.Clear()

                'creacion de botones 
                SplitTopOfBrowser = New SplitContainer
                SplitButton = New SplitContainer
                SplitCloseButton = New SplitContainer
                BtnClose = New Button
                BtnFullScreen = New Button
                BtnchangeVisualization = New Button
                BtnShowUrl = New Button

                BtnClose.Text = "Cerrar Explorador"
                BtnClose.Dock = DockStyle.Fill
                BtnFullScreen.Text = "Pantalla Completa"
                BtnFullScreen.Dock = DockStyle.Fill
                BtnchangeVisualization.Text = "Cambiar Orientacion"
                BtnchangeVisualization.Dock = DockStyle.Fill
                BtnShowUrl.BackgroundImage = Global.Zamba.Controls.My.Resources.Resources.showInfo
                BtnShowUrl.BackgroundImageLayout = ImageLayout.Stretch
                BtnShowUrl.Dock = DockStyle.Fill


                Controls.Clear()
                Controls.Add(TaskViewerSplit)
                TaskViewerSplit.Orientation = Orientation.Horizontal
                TaskViewerSplit.Dock = DockStyle.Fill
                'Configuración del splitPanel 

                SplitCloseButton.Panel1.Controls.Add(BtnClose)
                SplitCloseButton.Panel2.Controls.Add(BtnShowUrl)
                SplitTopOfBrowser.Panel1.Controls.Add(BtnFullScreen)
                SplitButton.Panel1.Controls.Add(BtnchangeVisualization)
                SplitButton.Panel2.Controls.Add(SplitCloseButton)
                SplitTopOfBrowser.Panel2.Controls.Add(SplitButton)

                SplitButton.Dock = DockStyle.Top
                SplitButton.IsSplitterFixed = True
                SplitButton.Size = New System.Drawing.Point(params.Item("width"), 20)
                SplitButton.SplitterDistance = SplitButton.Width / 2

                SplitCloseButton.Dock = DockStyle.Top
                SplitCloseButton.IsSplitterFixed = True
                SplitCloseButton.Size = New System.Drawing.Point(params.Item("width"), 20)
                SplitCloseButton.Panel1MinSize = 0
                SplitCloseButton.Panel2MinSize = 0
                SplitCloseButton.SplitterDistance = 40

                SplitTopOfBrowser.Orientation = Orientation.Vertical
                SplitTopOfBrowser.IsSplitterFixed = True
                SplitTopOfBrowser.Size = New System.Drawing.Point(params.Item("width"), 20)
                SplitTopOfBrowser.Name = "SplitTopOfBrowser"
                SplitTopOfBrowser.Dock = DockStyle.Top

                'seteo el alto 
                If CInt(params.Item("height")) <> 0 Then
                    TaskViewerSplit.SplitterDistance = (TaskViewerSplit.Height * (100 - params.Item("height"))) / 100
                Else
                    TaskViewerSplit.SplitterDistance = (TaskViewerSplit.Height / 2) - 100
                End If

                RemoveHandler BtnClose.Click, AddressOf BtnCloseExplorerClick
                AddHandler BtnClose.Click, AddressOf BtnCloseExplorerClick
                RemoveHandler BtnFullScreen.Click, AddressOf BtnFullScreenClick
                AddHandler BtnFullScreen.Click, AddressOf BtnFullScreenClick
                RemoveHandler BtnchangeVisualization.Click, AddressOf BtnchangeVisualizationClick
                AddHandler BtnchangeVisualization.Click, AddressOf BtnchangeVisualizationClick
                RemoveHandler BtnShowUrl.Click, AddressOf BtnShowUrlClick
                AddHandler BtnShowUrl.Click, AddressOf BtnShowUrlClick

                If params.Item("WayToOpen") Then
                    'el browser se encuentra fuera de zamba 
                    Webform.Controls.Clear()
                    Webform = New Form
                    BtnFullScreen.Text = "Restaurar"

                    'Configuración del webBrowser 
                    Webform.Name = "Zamba - Web Browser"
                    Webform.Text = "Zamba - Web Browser"
                    'remuevo el boton cambiar visualizacion 
                    SplitTopOfBrowser.Panel2.Controls.Clear()
                    SplitTopOfBrowser.Panel2.Controls.Add(BtnClose)
                    SplitTopOfBrowser.SplitterDistance = SplitTopOfBrowser.Width / 2

                    Webform.Controls.Add(SplitTopOfBrowser)
                    Webform.Controls.Add(ParamWB)
                    Webform.WindowState = FormWindowState.Maximized
                    Webform.BringToFront()
                    TaskViewerSplit.Panel2Collapsed = True
                    TaskViewerSplit.Panel2.Controls.Clear()
                    TaskViewerSplit.Panel1.Controls.Add(ToolStripContainer1)
                    TaskViewerSplit.Panel1.Show()
                    Webform.Controls.Add(SplitTopOfBrowser)

                    BtnFullScreen.Text = "Restaurar"
                    Webform.Show()
                Else
                    'el browser se encuentra dentro de zamba 
                    ParamWB.BringToFront()
                    TaskViewerSplit.Panel2.Show()
                    TaskViewerSplit.Panel2Collapsed = False
                    TaskViewerSplit.IsSplitterFixed = False
                    'valido si la opcion de visualizacion de Orientacion Horizontal esta activada 
                    If params.Item("HorizontalVisualization") Then
                        Dim result As Zamba.Core.Result
                        TaskViewerSplit.Orientation = Orientation.Vertical
                        TaskViewerSplit.Panel1Collapsed = False
                        TaskViewerSplit.Panel1.Controls.Add(SplitTopOfBrowser)
                        TaskViewerSplit.Panel1.Controls.Add(ParamWB)
                        TaskViewerSplit.Panel2.Controls.Add(ToolStripContainer1)

                        TaskViewerSplit.SplitterDistance = ((TaskViewerSplit.Width * (ParamWidth)) / 100)
                        params.Add("ItsHorizontalVisualization", ItsHorizontalVisualization)
                        params.Add("TaskViewerSplit", TaskViewerSplit)
                        changeVisuals = True
                        ItsHorizontalVisualization = True
                    Else
                        TaskViewerSplit.Panel2.Controls.Add(ParamWB)
                        TaskViewerSplit.Panel2.Controls.Add(SplitTopOfBrowser)
                        TaskViewerSplit.Panel1.Controls.Add(ToolStripContainer1)
                        TaskViewerSplit.Orientation = Orientation.Horizontal
                        ItsHorizontalVisualization = False
                        TaskViewerSplit.Panel1Collapsed = False
                    End If
                End If

                Webform.BringToFront()
                ParamWB.BringToFront()

                If Not String.IsNullOrEmpty(UserPreferences.getValue("EnableJsStartSessionFunction", UPSections.WorkFlow, String.Empty)) Then
                    ParamWB.Navigate(UserPreferences.getValue("EnableJsStartSessionFunction", UPSections.WorkFlow, String.Empty))
                End If

                ZTrace.WriteLineIf(ZTrace.IsInfo, "El explorador de la regla se ha cargado con éxito")
                ParamWB.Navigate(New Uri(params.Item("Route")))

                'If Not IsNothing(ParamWB.Document) Then 
                '    If Not IsNothing(ParamWB.Document.Window) Then 
                '        RemoveHandler ParamWB.Document.Window.Unload, AddressOf WebBrowserClose 
                '        AddHandler ParamWB.Document.Window.Unload, AddressOf WebBrowserClose 
                '    Else 
                '        ZTrace.WriteLineIf(ZTrace.IsInfo, "window no instanciada") 
                '    End If 
                'Else 
                '    ZTrace.WriteLineIf(ZTrace.IsInfo, "document no instanciado") 
                'End If 
                CommonFunctions.VariablesxTarea("ExplorerIsOpen") = True
                ZTrace.WriteLineIf(ZTrace.IsInfo, "La regla se ejecutó con exito")
            Catch ex As System.Runtime.InteropServices.COMException
                ParamWB = Nothing
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            Finally
                ResumeLayout()
                Cursor = Cursors.Default
            End Try
        End Sub


        ''' <summary>
        ''' Metodo que se ejecuta al apretar el boton de cerrar de la DoExecuteExplorer
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub BtnCloseExplorerClick(ByVal sender As Object, ByVal e As EventArgs)
            CloseDoExplorerBrowser()
        End Sub
        Private Function CloseDoExplorerBrowser() As Boolean
            Try
                SuspendLayout()
                'Si se cierra el explorador de la DoExecuteExplorer, limpio todas las variables
                If closeExplorer() = True Then
                    RuleId = 0
                    ParamResult = Nothing
                    If Not String.IsNullOrEmpty(UserPreferences.getValue("EnableJsEndSessionFunction", UPSections.WorkFlow, "")) Then
                        ParamWB.Navigate(UserPreferences.getValue("EnableJsEndSessionFunction", UPSections.WorkFlow, ""))
                    End If

                    ParamHeight = 0
                    ParamWidth = 0
                    WayToOpen = Nothing
                    ContinueWithRule = False
                    UCWidth = 0
                    changeVisuals = False
                    EvaluateRuleID = 0
                    ExecuteElseID = 0
                    habilitar = False
                    TxtVar = String.Empty
                    Operador = String.Empty
                    Value = String.Empty
                    If Not ParamWB Is Nothing Then ParamWB.Dispose()
                    ParamWB = Nothing
                    CommonFunctions.VariablesxTarea("ExplorerIsOpen") = False
                    Return True
                Else
                    Return False
                End If
            Catch ex As System.Runtime.InteropServices.COMException
                CommonFunctions.VariablesxTarea("ExplorerIsOpen") = False
                ParamWB = Nothing
            Catch ex As Exception
                CommonFunctions.VariablesxTarea("ExplorerIsOpen") = False
                ParamWB = Nothing
                ZClass.raiseerror(ex)
            Finally
                ResumeLayout()
            End Try
        End Function
        ''' <summary>
        ''' Metodo que se ejecuta para cerrar el explorador de la DOExecuteExplorer
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function closeExplorer() As Boolean
            If RuleId > 0 Then
                Dim params As New Hashtable
                Dim result As Zamba.Core.Result
                Dim close As Boolean = True
                params.Add("paramResult", ParamResult)
                params.Add("ExecuteElseID", ExecuteElseID)
                params.Add("ContinueWithRule", ContinueWithRule)
                params.Add("ruleId", RuleId)
                params.Add("TaskViewerSplit", TaskViewerSplit)
                params.Add("changeVisuals", True)

                TaskViewerSplit.Panel1.Controls.Clear()
                TaskViewerSplit.Panel2.Controls.Clear()

                lista = New List(Of ITaskResult)
                WFStepId = WFStepBusiness.GetStepIdByRuleId(EvaluateRuleID, True)
                WFRS = New WFRulesBusiness
                Try
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Cerrando el explorador de la regla DOExecuteExplorer")
                    If habilitar Then
                        If EvaluateRuleID <> -1 Then
                            lista = WFRS.ExecuteRule(EvaluateRuleID, WFStepId, ParamResult, True, Nothing)
                        Else
                            lista = params.Item("paramResult")
                        End If

                        If Zamba.Core.WF.WF.WFTaskBusiness.ValidateVar(lista(0), TxtVar, Operador, Value) = True Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado de la Validacion de Cierre: Verdadero")
                            params.Add("CloseBrowser", True)
                            RuleId = 0
                        Else
                            close = False
                            params.Add("CloseBrowser", False)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado de la Validacion de Cierre: Falso")
                        End If
                    End If

                    If params.Contains("CloseBrowser") = False Then
                        params.Add("CloseBrowser", True)
                    End If

                    If EvaluateRuleID > 0 Then
                        If ExecuteElseID > 0 Then
                            If ItsHorizontalVisualization Then
                                changeVisuals = True
                            End If

                            If params.Item("CloseBrowser") Then
                                TaskViewerSplit.Panel2Collapsed = True
                                Webform.Close()
                                Webform.Dispose()
                                TaskViewerSplit.Panel1.Controls.Add(ToolStripContainer1)
                                TaskViewerSplit.Panel2.Controls.Add(SplitTopOfBrowser)
                            Else
                                If WayToOpen Then
                                    ZambaCore.HandleModule(ResultActions.CloseBrowser, result, params)
                                    Return close
                                End If
                                If ItsHorizontalVisualization Then
                                    If params.Item("CloseBrowser") Then
                                        TaskViewerSplit.Panel1Collapsed = True
                                        TaskViewerSplit.Panel2.Controls.Add(ToolStripContainer1)
                                        changeVisuals = False
                                    Else
                                        TaskViewerSplit.Orientation = Orientation.Vertical
                                        TaskViewerSplit.Panel2.Controls.Add(ToolStripContainer1)
                                        TaskViewerSplit.Panel1.Controls.Add(SplitTopOfBrowser)
                                        TaskViewerSplit.Panel1.Controls.Add(ParamWB)
                                    End If
                                Else
                                    If params.Item("CloseBrowser") Then
                                        TaskViewerSplit.Panel2Collapsed = True
                                    Else
                                        TaskViewerSplit.Panel1.Controls.Add(ToolStripContainer1)
                                        TaskViewerSplit.Panel2.Controls.Add(SplitTopOfBrowser)
                                        TaskViewerSplit.Panel2.Controls.Add(ParamWB)
                                    End If
                                End If
                            End If
                            ZambaCore.HandleModule(ResultActions.CloseBrowser, result, params)
                            Return close
                        End If
                        params.Add("BrowserClosed", True)
                        params.Add("UCWidth", UCWidth)
                        'valido en que modo de visualizacion se cierra el Browser
                        If ItsHorizontalVisualization Then
                            TaskViewerSplit.Orientation = Orientation.Vertical
                            TaskViewerSplit.Panel1.Controls.Clear()
                            TaskViewerSplit.Panel1.Controls.Add(ToolStripContainer1)
                            Webform.Close()
                            Webform.Dispose()
                            ZambaCore.HandleModule(ResultActions.CloseBrowser, result, params)
                        Else
                            If WayToOpen Then
                                Webform.Close()
                                Webform.Dispose()
                                ZambaCore.HandleModule(ResultActions.CloseBrowser, result, params)
                            Else
                                TaskViewerSplit.Panel2.Controls.Clear()
                                TaskViewerSplit.Panel2.Controls.Add(SplitTopOfBrowser)
                                TaskViewerSplit.Panel2.Controls.Add(ParamWB)
                                TaskViewerSplit.Panel1.Controls.Add(ToolStripContainer1)
                                TaskViewerSplit.Panel1.Show()
                                ZambaCore.HandleModule(ResultActions.CloseBrowser, result, params)
                            End If
                        End If
                    Else
                        TaskViewerSplit.Panel2.Controls.Clear()
                        TaskViewerSplit.Panel1.Controls.Add(ToolStripContainer1)
                        TaskViewerSplit.Panel1.Show()
                        TaskViewerSplit.Panel2Collapsed = True
                        'true: fullscreen mode
                        'false: inside Zamba
                        If WayToOpen Then
                            Webform.Close()
                        End If
                        ZambaCore.HandleModule(ResultActions.CloseBrowser, result, params)
                    End If
                    ParamWB.Navigate("'about:blank")

                    If Not IsNothing(ParamWB) Then
                        ParamWB.Dispose()
                    End If

                    ParamWB = Nothing

                    Return close
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                    Return True
                End Try
            Else
                TaskViewerSplit.Panel2Collapsed = True
                Webform.Close()
                Webform.Dispose()
                TaskViewerSplit.Panel1.Controls.Add(ToolStripContainer1)
                TaskViewerSplit.Panel2.Controls.Add(SplitTopOfBrowser)
                TaskViewerSplit.Panel2.Controls.Add(ParamWB)
                Return True
            End If
        End Function
        Private Sub BtnFullScreenClick(ByVal sender As Object, ByVal e As EventArgs)






            Dim point As New System.Drawing.Point(ParamWidth, ParamHeight)

            Try
                SplitTopOfBrowser.Panel1.Controls.Clear()
                SplitTopOfBrowser.Panel2.Controls.Clear()
                TaskViewerSplit.Panel1.Controls.Clear()
                TaskViewerSplit.Panel2.Controls.Clear()
                If WayToOpen Then
                    Webform.Controls.Clear()
                    Webform.ControlBox = False
                    SplitButton.Panel1.Controls.Add(BtnchangeVisualization)

                    SplitTopOfBrowser.Panel2.Controls.Add(SplitButton)
                    SplitTopOfBrowser.Panel1.Controls.Add(BtnFullScreen)
                    SplitButton.SplitterDistance = SplitButton.Width / 2
                    SplitTopOfBrowser.SplitterDistance = SplitTopOfBrowser.Width / 3
                    SplitCloseButton.Dock = DockStyle.Top
                    SplitCloseButton.IsSplitterFixed = True
                    SplitCloseButton.Panel1MinSize = 0
                    SplitCloseButton.Panel2MinSize = 0

                    SplitCloseButton.Panel1.Controls.Add(BtnClose)
                    SplitButton.Panel2.Controls.Add(SplitCloseButton)
                    SplitTopOfBrowser.Panel2.Controls.Add(SplitButton)


                    SplitTopOfBrowser.IsSplitterFixed = True
                    SplitTopOfBrowser.Size = New System.Drawing.Point(ParamWidth, 20)
                    SplitTopOfBrowser.Orientation = Orientation.Vertical

                    If ItsHorizontalVisualization Then
                        TaskViewerSplit.Panel1.Controls.Add(SplitTopOfBrowser)
                        TaskViewerSplit.Panel1.Controls.Add(ParamWB)
                        TaskViewerSplit.Panel2.Controls.Add(ToolStripContainer1)
                        If changeVisuals = False Then
                            TaskViewerSplit.Orientation = Orientation.Horizontal
                            TaskViewerSplit.SplitterDistance = (TaskViewerSplit.Width * ParamWidth) / 100
                        End If
                    Else
                        TaskViewerSplit.Panel2.Controls.Add(SplitTopOfBrowser)
                        TaskViewerSplit.Panel2.Controls.Add(ParamWB)
                        TaskViewerSplit.Panel1.Controls.Add(ToolStripContainer1)
                        TaskViewerSplit.SplitterDistance = (TaskViewerSplit.Height * (100 - ParamHeight)) / 100
                    End If

                    SplitTopOfBrowser.Dock = DockStyle.Top
                    BtnFullScreen.Text = "Pantalla Completa"
                    ParamWB.BringToFront()
                    TaskViewerSplit.Panel2.Show()
                    TaskViewerSplit.Panel2Collapsed = False
                    TaskViewerSplit.IsSplitterFixed = False
                    ParamWB.BringToFront()
                    Webform.BringToFront()
                    Webform.Close()
                    WayToOpen = False
                Else
                    Webform = New Form
                    Webform.ControlBox = False
                    Webform.Name = "Zamba - Web Browser"
                    Webform.Text = "Zamba - Web Browser"
                    SplitTopOfBrowser.Panel1.Controls.Add(BtnFullScreen)
                    SplitTopOfBrowser.Panel2.Controls.Add(BtnClose)
                    SplitTopOfBrowser.SplitterDistance = SplitTopOfBrowser.Width / 2

                    Webform.Controls.Add(SplitTopOfBrowser)
                    Webform.Size = point
                    BtnFullScreen.Text = "Restaurar"
                    TaskViewerSplit.Panel2Collapsed = True
                    Webform.WindowState = FormWindowState.Maximized
                    TaskViewerSplit.Panel1.Controls.Add(ToolStripContainer1)
                    Webform.Controls.Add(ParamWB)
                    ParamWB.BringToFront()
                    Webform.BringToFront()
                    Webform.Show()

                    WayToOpen = True
                End If

            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End Sub
        Private Sub BtnchangeVisualizationClick(ByVal sender As Object, ByVal e As EventArgs)
            Dim params As New Hashtable

            params.Add("TaskViewerSplit", TaskViewerSplit)

            BtnchangeVisualization.Controls.Clear()
            BtnClose.Controls.Clear()
            BtnFullScreen.Controls.Clear()
            TaskViewerSplit.Panel1.Controls.Clear()
            TaskViewerSplit.Panel2.Controls.Clear()
            Try
                If changeVisuals Then
                    TaskViewerSplit.Orientation = Orientation.Horizontal
                    TaskViewerSplit.IsSplitterFixed = False
                    ItsHorizontalVisualization = False
                    TaskViewerSplit.Panel2.Controls.Add(SplitTopOfBrowser)
                    TaskViewerSplit.Panel2.Controls.Add(ParamWB)
                    TaskViewerSplit.Panel1.Controls.Add(ToolStripContainer1)

                    TaskViewerSplit.SplitterDistance = (TaskViewerSplit.Height * (100 - ParamHeight)) / 100
                    params.Add("ItsHorizontalVisualization", ItsHorizontalVisualization)
                    params.Add("changeVisuals", changeVisuals)
                    changeVisuals = False
                Else
                    TaskViewerSplit.Panel1.Controls.Add(SplitTopOfBrowser)
                    TaskViewerSplit.Panel1.Controls.Add(ParamWB)
                    TaskViewerSplit.Panel2.Controls.Add(ToolStripContainer1)
                    TaskViewerSplit.Orientation = Orientation.Vertical
                    TaskViewerSplit.SplitterDistance = ((TaskViewerSplit.Width * (ParamWidth)) / 100)
                    ItsHorizontalVisualization = True
                    params.Add("ItsHorizontalVisualization", ItsHorizontalVisualization)
                    changeVisuals = True
                End If

                ParamWB.BringToFront()
                Webform.BringToFront()
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End Sub
        Private Sub BtnShowUrlClick(ByVal sender As Object, ByVal e As EventArgs)
            Try

                If Not ParamWB Is Nothing AndAlso Not ParamWB.Url Is Nothing Then
                    If CloseUrlTextbox Then
                        ParamWB.Controls.Remove(Url)
                        Url.Dispose()
                        CloseUrlTextbox = False
                    Else

                        Url = New TextBox
                        Url.Text = ParamWB.Url.ToString
                        Url.Visible = True
                        Url.Name = "Url"
                        Url.Size = New System.Drawing.Size(157, 20)
                        Url.TabIndex = 0
                        Url.Dock = DockStyle.Top
                        ParamWB.Controls.Add(Url)
                        Url.Show()
                        Url.BringToFront()
                        CloseUrlTextbox = True
                    End If
                End If
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End Sub
        ''' <summary>
        ''' muestra los todos los registros de log de la tarea
        ''' </summary>
        ''' <param name="currentResult"></param>
        ''' <remarks></remarks>
        ''' <history¨Pablo 07-09-2011 [Created]</history>
        Private Sub btnShowAll_Click(ByVal sender As System.Object, ByVal e As EventArgs)
            ShowHistory(True, False)
        End Sub
        ''' <summary>
        ''' muestra los logs de modificacion de atributos sobre un usuario
        ''' </summary>
        ''' <param name="currentResult"></param>
        ''' <remarks></remarks>
        ''' <history¨Pablo 07-09-2011 [Created]</history>
        Private Sub btnShowIndexs_Click(ByVal sender As System.Object, ByVal e As EventArgs)
            ShowHistory(True, True)
        End Sub

        Private Sub ParamWB_Disposed(ByVal sender As Object, ByVal e As EventArgs) Handles ParamWB.Disposed
            ZTrace.WriteLineIf(ZTrace.IsInfo, "ParamWB_Disposed: system.EventArgs no contiene URL para este evento")
        End Sub

        'Logueo del evento documentCompleted
        Private docCompleted As Boolean
        Private Sub ParamWB_DocumentCompleted(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles ParamWB.DocumentCompleted
            If docCompleted = False Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Evento ParamWB_DocumentCompleted - Url: " & e.Url.ToString)
                docCompleted = True
            End If
        End Sub

        Private Sub ParamWB_HandleDestroyed(ByVal sender As Object, ByVal e As EventArgs) Handles ParamWB.HandleDestroyed
            ZTrace.WriteLineIf(ZTrace.IsInfo, "ParamWB_HandleDestroyed no contiene URL para este evento")
        End Sub

        'Logueo del evento documentNavigated
        Private docNavigated As Boolean
        Private AllowChangeState As Boolean

        Private Sub ParamWB_Navigated(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserNavigatedEventArgs) Handles ParamWB.Navigated
            Try
                If docNavigated = False Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Evento ParamWB_Navigated - Url: " & e.Url.ToString & vbCrLf & "Cerrando el explorador de la regla")
                    docNavigated = True
                End If

                If IsNothing(ParamWB.Url) Then
                    CloseDoExplorerBrowser()
                ElseIf String.Compare("about:blank", e.Url.ToString.Trim) = 0 Then
                    CloseDoExplorerBrowser()
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
#End Region




        ''' <summary>
        ''' Metodo generico creado para Handlear acciones sobre una tarea o result
        ''' </summary>
        ''' <param name="action">Enum conteniendo la accion realizada</param>
        ''' <param name="currentResult"></param>
        ''' <remarks></remarks>
        ''' <history>Marcelo 30-03-2009 [Created]</history>
        Private Sub LoadModulesRuleActions(ByVal action As ResultActions, ByRef _Results As List(Of ITaskResult), ByVal Params As Hashtable)
            Try
                If action <> ResultActions.UpdateUserAsigned Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando acciones sobre el visualizador de tareas por la acción: " & action.ToString())
                End If

                Select Case action
                    Case ResultActions.ExecuteRule
                        If _Results IsNot Nothing AndAlso Params IsNot Nothing Then
                            Dim RuleId As Long
                            Dim StepId As Long

                            If Params.Contains("RuleId") Then
                                RuleId = Params("RuleId")
                            End If
                            If Params.Contains("StepId") Then
                                StepId = Params("StepId")
                            End If

                            If StepId = 0 Then
                                StepId = WFRulesBusiness.GetWFStepIdbyRuleID(RuleId)
                            End If

                            Dim wf As New WFRulesBusiness
                            wf.ExecuteRule(RuleId, StepId, _Results, True, Nothing)
                        Else
                            ZTrace.WriteLineIf(ZTrace.IsWarning, "ERROR: Existen parámetros nulos y no es posible continuar.")
                        End If

                    Case ResultActions.SetReadOnly
                        If _Results IsNot Nothing AndAlso _Results.Count > 0 AndAlso Params IsNot Nothing AndAlso Params.ContainsKey("value") Then
                            Dim value As Boolean = CBool(Params("value"))

                            'Configura las marcas de solo lectura sobre los results en memoria existentes
                            _setReadOnly = value
                            If TaskResult IsNot Nothing AndAlso TaskResult.DocType IsNot Nothing Then TaskResult.DocType.IsReadOnly = value
                            If TabDoc IsNot Nothing Then
                                TabDoc.SetReadOnly = value
                                If TabDoc.Result IsNot Nothing Then TabDoc.Result.DocType.IsReadOnly = value
                            End If
                        Else
                            ZTrace.WriteLineIf(ZTrace.IsWarning, "ERROR: Existen parámetros nulos y no es posible continuar.")
                        End If

                    Case ResultActions.SetReindex
                        If _Results IsNot Nothing AndAlso _Results.Count > 0 AndAlso Params IsNot Nothing AndAlso Params.ContainsKey("value") Then
                            Dim value As Boolean = Params("value")
                            TaskResult.DocType.IsReindex = value
                            TabDoc.Result.DocType.IsReindex = value
                        Else
                            ZTrace.WriteLineIf(ZTrace.IsWarning, "ERROR: Existen parámetros nulos y no es posible continuar.")
                        End If

                    Case ResultActions.HideReplaceDocument
                        If _Results IsNot Nothing AndAlso _Results.Count > 0 AndAlso Params IsNot Nothing AndAlso Params.ContainsKey("value") Then
                            TabDoc.ChangeReplaceButtonVisibility(Params("value"))
                        Else
                            ZTrace.WriteLineIf(ZTrace.IsWarning, "ERROR: Existen parámetros nulos y no es posible continuar.")
                        End If
                End Select

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Private Sub chkTakeTask3_Click(sender As Object, e As EventArgs) Handles chkFinishTaskOnClose.Click
            UserPreferences.setValue("CheckFinishTaskAfterClose", UPSections.WorkFlow, chkFinishTaskOnClose.Checked)
            If chkFinishTaskOnClose.Checked Then
                chkFinishTaskOnClose.Font = New Font(chkFinishTaskOnClose.Font, FontStyle.Bold)
                UserPreferences.setValue("CheckFinishTaskAfterClose", "True", UPSections.WorkFlow)
            Else
                chkFinishTaskOnClose.Font = New Font(chkFinishTaskOnClose.Font, FontStyle.Regular)
                UserPreferences.setValue("CheckFinishTaskAfterClose", "False", UPSections.WorkFlow)
            End If
        End Sub

        Private Sub chkCloseTaskAfterDistribute_Click(sender As Object, e As EventArgs) Handles chkCloseTaskAfterDistribute.Click
            If chkCloseTaskAfterDistribute.Checked Then
                chkCloseTaskAfterDistribute.Font = New Font(chkCloseTaskAfterDistribute.Font, FontStyle.Bold)
            Else
                chkCloseTaskAfterDistribute.Font = New Font(chkCloseTaskAfterDistribute.Font, FontStyle.Regular)
            End If
        End Sub

        Public Function GetSelectedResults() As List(Of IResult) Implements IMenuContextContainer.GetSelectedResults
            Return New List(Of IResult)(TaskResult)
        End Function

        Public Sub RefreshResults() Implements IMenuContextContainer.RefreshResults
            Try
                RefreshTask(TaskResult)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
    End Class
End Namespace
