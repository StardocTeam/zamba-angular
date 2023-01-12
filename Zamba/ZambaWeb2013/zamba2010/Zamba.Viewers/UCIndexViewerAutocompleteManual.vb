Imports System.Windows.Forms
'Imports zamba.DocTypes.Factory

Imports Zamba.Core
Imports Zamba.AppBlock
Imports Zamba.Indexs
Imports System.Text

'Imports Zamba.Barcodes
'Imports Zamba.Barcode

Public Class UCIndexViewerAutocompleteManual
    Inherits ZControl
#Region " Windows Form Designer generated code "





    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    '  Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    '  Friend WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    '   Friend WithEvents PicBox As System.Windows.Forms.PictureBox
    ' Friend WithEvents DsResults1 As DsResults
    '  Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    '   Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents DesignSandBar As ToolStrip
    Friend WithEvents BtnDeshacer As ToolStripButton
    Friend WithEvents BtnGuardar As ToolStripButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents ButnLimpiarIndices As ToolStripButton
    Private _useHashForCrtlIndex As Boolean
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UCIndexViewerAutocompleteManual))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.DesignSandBar = New ToolStrip
        Me.BtnDeshacer = New ToolStripButton
        Me.BtnGuardar = New ToolStripButton
        Me.ButnLimpiarIndices = New ToolStripButton
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.btnAceptar = New System.Windows.Forms.Button
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.AutoScroll = True
        Me.Panel1.Location = New System.Drawing.Point(0, 24)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(256, 324)
        Me.Panel1.TabIndex = 0
        '
        'DesignSandBar
        '
        Me.DesignSandBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BtnDeshacer, Me.BtnGuardar, Me.ButnLimpiarIndices})
        Me.DesignSandBar.Location = New System.Drawing.Point(0, 0)
        Me.DesignSandBar.Name = "DesignSandBar"
        Me.DesignSandBar.Size = New System.Drawing.Size(256, 24)
        Me.DesignSandBar.TabIndex = 155
        Me.DesignSandBar.Text = ""
        '
        'BtnDeshacer
        '
        Me.BtnDeshacer.Image = CType(resources.GetObject("BtnDeshacer.Icon"), System.Drawing.Image)
        Me.BtnDeshacer.Tag = "DESHACER"
        Me.BtnDeshacer.ToolTipText = "DESHACER LAS MODIFICACIONES EN LOS INDICES"
        '
        'BtnGuardar
        '
        Me.BtnGuardar.Image = CType(resources.GetObject("BtnGuardar.Icon"), System.Drawing.Image)
        Me.BtnGuardar.Tag = "GUARDAR"
        Me.BtnGuardar.ToolTipText = "GUARDAR LAS MODIFICACIONES EN LOS INDICES"
        '
        'ButnLimpiarIndices
        '
        Me.ButnLimpiarIndices.Image = CType(resources.GetObject("ButnLimpiarIndices.Icon"), System.Drawing.Image)
        Me.ButnLimpiarIndices.Tag = "LIMPIAR INDICES"
        Me.ButnLimpiarIndices.ToolTipText = "LIMPIAR INDICES"
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.Controls.Add(Me.btnAceptar)
        Me.Panel2.Controls.Add(Me.btnCancelar)
        Me.Panel2.Location = New System.Drawing.Point(0, 349)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(256, 45)
        Me.Panel2.TabIndex = 156
        Me.Panel2.Visible = False
        '
        'btnAceptar
        '
        Me.btnAceptar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAceptar.Location = New System.Drawing.Point(150, 19)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(75, 23)
        Me.btnAceptar.TabIndex = 1
        Me.btnAceptar.Text = "Aceptar"
        Me.btnAceptar.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancelar.Location = New System.Drawing.Point(29, 19)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(75, 23)
        Me.btnCancelar.TabIndex = 0
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'UCIndexViewerAutocompleteManual
        '
        Me.BackColor = System.Drawing.Color.White
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CausesValidation = False
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.DesignSandBar)
        Me.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Location = New System.Drawing.Point(143, 0)
        Me.Name = "UCIndexViewerAutocompleteManual"
        Me.Size = New System.Drawing.Size(256, 394)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Constructores"
    ''' <summary>
    ''' Control que muestra los indices de un documento
    ''' </summary>
    ''' <param name="offline"></param>
    ''' <param name="btnVisible"></param>
    ''' <param name="showtoolbar"></param>
    ''' <param name="UseHashForCrtlIndex"></param>
    ''' <param name="IsReIndex">Si se desea por defecto que sea reindexado</param>
    ''' <remarks></remarks>
    Public Sub New(Optional ByVal offline As Boolean = False, Optional ByVal btnVisible As Boolean = True, Optional ByVal showtoolbar As Boolean = True, Optional ByVal UseHashForCrtlIndex As Boolean = True, Optional ByVal IsReIndex As Boolean = False, Optional ByVal SaveOnAccept As Boolean = True)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Debug.Indent()
        Trace.WriteLineIf(ZTrace.IsVerbose, "Instancio el FrmViewer" & Now.ToString)
        'Add any initialization after the InitializeComponent() call
        Me.offline = offline
        Me.saveOnAccept = SaveOnAccept
        Me.isReIndex = IsReIndex
        BtnDeshacer.Visible = btnVisible
        BtnGuardar.Visible = btnVisible
       
        RemoveHandler IndexsBusiness.saveIndex, AddressOf Save
        AddHandler IndexsBusiness.saveIndex, AddressOf Save
        'RemoveHandler IndexsBusiness.CancelIndex, AddressOf CancelIndexSaved
        'AddHandler IndexsBusiness.CancelIndex, AddressOf CancelIndexSaved
        If Not showtoolbar Then
            Me.DesignSandBar.Visible = False
        End If
        Me._useHashForCrtlIndex = UseHashForCrtlIndex
    End Sub
#End Region

    Private Sub RefreshIndex(ByVal Action As ResultActions, ByRef currentResult As Result)
        If Action = ResultActions.RefreshIndexs Then
            ShowIndexs(currentResult, True)
        End If
    End Sub

#Region "Variables"
    Private offline As Boolean
    Private saveOnAccept As Boolean
    'Si el control esta en reindexado (se utiliza en indexer6000)
    Private isReIndex As Boolean
    'Dim FlagFirstTime As Boolean = True
    Dim LocalResult As Zamba.Core.Result
    'Dim FlagSameDocType As Boolean = False
    Private FlagIndexEdited As Boolean
    Private LastShowedDocId As Int64
    Private LastIsReIndex As Boolean
    Public FlagIsIndexer As Boolean
    Public Dialog_Result As DialogResult
    Private LocalIndexs As ArrayList

#End Region
#Region "Eventos tab y ENTER"
    Public Shadows Event EnterPressed()
    Private Sub Enter_KeyDown()
        RaiseEvent EnterPressed()
    End Sub
    Public Shadows Event TabPressed()
    Private Sub Tab_KeyDown()
        RaiseEvent TabPressed()
    End Sub
#End Region

    'Private IndexAnterior As String
    ''' <summary>
    ''' Metodo que g
    ''' </summary>
    ''' <param name="_Index"></param>
    ''' <remarks></remarks>
    ''' <history> [Ezequiel] 30/04/09 Modified: Se adapto el codigo para tomar mas de 1 clave al autocomplete.
    Private Sub DataChanged(ByVal _Index As Index)
        'If IndexAnterior <> Index.Data Then
        Me.FlagIndexEdited = True
        Dim LocalResultAux As IResult = Me.LocalResult
        Try
            Dim newFrmGrilla As New frmGrilla()
            If Not IsNothing(AutocompleteBCBusiness.ExecuteAutoComplete(LocalResult, _Index, newFrmGrilla)) Then
                If FlagIsIndexer = False AndAlso LocalResult.ID <> 0 Then
                    Me.Save(True)
                Else
                    If _Index.Data <> _Index.DataTemp Then
                        _Index.SetData(_Index.DataTemp)
                    End If
                End If
                Me.ShowIndexs(LocalResult)
                Me.FlagIndexEdited = True
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            If LocalResult Is Nothing Then Me.LocalResult = LocalResultAux
        End Try
        RaiseEvent IndexsChanged(LocalResult, _Index)
    End Sub
    Private Sub LimpiarIndices()
        Try
            Me.CleanIndexs()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

#Region "Muestra los indices"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Result"></param>
    ''' <param name="IfSameResultDontReload"></param>
    ''' <remarks></remarks>
    ''' <history> [Ezequiel] 29/03/2009 Modified - Owner Rights
    Public Sub ShowIndexs(ByRef Result As Zamba.Core.Result, Optional ByVal IfSameResultDontReload As Boolean = False)
        If IsNothing(Result) Then Exit Sub
        If Not IsNothing(LocalResult) AndAlso IfSameResultDontReload = True AndAlso LocalResult.ID = Result.ID Then Exit Sub

        Me.LocalResult = Result
        Me.FlagIndexEdited = False
        Me.LocalResult.FlagIndexEdited = False
        Try

            Result.DocType.RightsLoaded = False
            DocTypesBusiness.GetEditRights(Result.DocType)

            If LocalResult.isShared <> 1 AndAlso LocalResult.DocTypeId <> 0 Then


                If UserBusiness.Rights.GetUserRights(UserBusiness.CurrentUser, Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.ReIndex, LocalResult.DocTypeId) Then

                    If UserBusiness.Rights.GetUserRights(UserBusiness.CurrentUser, Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.OwnerChanges, LocalResult.DocTypeId) AndAlso UserBusiness.CurrentUser.ID = LocalResult.OwnerID AndAlso LocalResult.DocType.IsReindex = False Then
                        LocalResult.DocType.IsReindex = True
                    End If
                    If UserBusiness.Rights.GetUserRights(UserBusiness.CurrentUser, Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.OwnerChanges, LocalResult.DocTypeId) AndAlso UserBusiness.CurrentUser.ID <> LocalResult.OwnerID AndAlso LocalResult.DocType.IsReindex = True Then
                        If Not UserBusiness.Rights.DisableOwnerChanges(UserBusiness.CurrentUser, LocalResult.DocTypeId) Then
                            LocalResult.DocType.IsReindex = False
                        End If

                    End If

                End If

            ElseIf LocalResult.isShared = 1 Then
                If LocalResult.DocTypeId <> 0 Then
                    LocalResult.DocType.IsReindex = False
                End If
            End If


            If Me.LastShowedDocId = Me.LocalResult.DocType.ID AndAlso Me.LastIsReIndex = Me.LocalResult.DocType.IsReindex Then
                Me.ReloadIndexsData(Result.DocType.ID)
            Else
                Me.Panel1.Invalidate()
                ClearIndexs()
                Me.GeneracionIndices(Result.DocType.ID)
                Me.Panel1.Update()
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            Me.LastShowedDocId = Me.LocalResult.DocType.ID
            Me.LastIsReIndex = Me.LocalResult.DocType.IsReindex
            '    Me.Panel1.AutoScroll = False
            '    Me.Panel1.AutoScroll = True
        End Try
        'Me.FlagFirstTime = False
    End Sub

    Public Sub ShowEspecifiedIndexs(ByRef Result As Zamba.Core.Result, ByVal IndexsIds As ArrayList, Optional ByVal IfSameResultDontReload As Boolean = False, Optional ByVal IsReadonly As Boolean = True)

        LocalIndexs = IndexsIds
        If Not IsNothing(LocalResult) AndAlso IfSameResultDontReload = True AndAlso LocalResult.ID = Result.ID Then Exit Sub

        Me.LocalResult = Result
        Me.FlagIndexEdited = False
        Me.LocalResult.FlagIndexEdited = False
        Try
            Me.Panel1.Invalidate()
            ClearIndexs()
            Me.GeneracionIndices(Result.DocType.ID, IndexsIds, IsReadonly)
            Me.Panel1.Update()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            Me.LastShowedDocId = Me.LocalResult.DocType.ID
            '    Me.Panel1.AutoScroll = False
            '    Me.Panel1.AutoScroll = True
        End Try
        'Me.FlagFirstTime = False
    End Sub

    Public Shared Sub PreloadIndex(ByVal ind As Zamba.Core.Index, ByVal isReindex As Boolean)
        Dim ctrl As DisplayindexCtl
        If Not Zamba.Core.IndexsBusiness.LoadedControlsTable.ContainsKey(ind.ID) OrElse Zamba.Core.IndexsBusiness.LoadedControlsTable(ind.ID).IsEnabled <> isReindex Then

            ctrl = New DisplayindexCtl(ind, isReindex)

            If Zamba.Core.IndexsBusiness.LoadedControlsTable.ContainsKey(ind.ID) Then
                Zamba.Core.IndexsBusiness.LoadedControlsTable(ind.ID) = ctrl
            Else
                Zamba.Core.IndexsBusiness.LoadedControlsTable.Add(ind.ID, ctrl)
            End If
        End If
    End Sub


    ''' <summary>
    ''' Se toman los indices de cada documento y se crea un
    ''' "DisplayindexCtl" por cada uno y este se agrega
    ''' en el UCIndexViewer...
    ''' </summary>
    ''' <param name="doctypeid"></param>
    ''' <history> Marcelo    Modified    05/02/2009
    '''           Sebastian Modified 14-07-2009 Se agrego funcionalidad para que los permisos en requerido
    '''                                         del tipo de documento predominen sobre los de indice especifico.       
    ''' </history>
    ''' <remarks></remarks>
    Private Sub GeneracionIndices(ByVal doctypeid As Int64)
        Dim cur As Cursor = Me.Cursor
        Dim bNuevaInstancia As Boolean
        Dim bUseCache As Boolean

        Me.Cursor = Cursors.WaitCursor

        Try
            Dim i As Int32
            Dim IRI As Hashtable = Nothing

            Me.Panel1.SuspendLayout()

            ' usa cache de controles?
            bUseCache = Boolean.Parse(UserPreferences.getValue("UseCachedIndexControl", Sections.FormPreferences, False))

            ' Se recorren los indices del result...
            If LocalResult.Indexs.Count = 0 Then
                LocalResult.Indexs.AddRange(IndexsBusiness.GetIndexs(LocalResult.ID, LocalResult.DocTypeId))
                If LocalResult.DocType.Indexs.Count = 0 Then LocalResult.DocType.Indexs = ZCore.FilterIndex(LocalResult.DocType.ID)
            End If

            For i = LocalResult.Indexs.Count - 1 To 0 Step -1

                ' se toman los indices asociados al documento...
                Dim ind As Zamba.Core.Index
                ind = DirectCast(LocalResult.Indexs(i), Zamba.Core.Index)

                Dim ctrl As DisplayindexCtl

                Dim FlagCoinciden As Boolean = False

                'Dim Ctrl2 As DisplayindexCtl = Zamba.Core.Indexs_Factory.LoadedControlsTable(ind.ID)

                ' Dim Ctrl2 As DisplayindexCtl = Nothing

                'If IsNothing(Ctrl2) = False AndAlso LocalResult.DocType.IsReindex = Ctrl2.IsEnabled Then FlagCoinciden = True

                '------------------------------------------
                'osanchez
                'TODO: CACHE
                '------------------------------------------
                'If Not Zamba.Core.IndexsBusiness.LoadedControlsTable.ContainsKey(ind.ID) OrElse FlagCoinciden = False Then

                '    ctrl = New DisplayindexCtl(ind, LocalResult.DocType.IsReindex)

                '    RemoveHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                '    RemoveHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                '    AddHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                '    AddHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                '    RemoveHandler ctrl.DataChanged, AddressOf DataChanged
                '    AddHandler ctrl.DataChanged, AddressOf DataChanged

                '    If Not Zamba.Core.IndexsBusiness.LoadedControlsTable.ContainsKey(ind.ID) Then

                '        ' Se agrega al hashTable de la propiedad LoadedControlsTable
                '        ' el indice tomado..
                '        Zamba.Core.IndexsBusiness.LoadedControlsTable.Add(ind.ID, ctrl)
                '    End If
                'End If
                '-------------------------------
                'osanchez
                'llamada que verifica que exista el
                'control en el cache
                '-------------------------------
                If Me._useHashForCrtlIndex Then UCIndexViewer.PreloadIndex(ind, LocalResult.DocType.IsReindex)
                '-------------------------------
                'Else
                Try

                    ' [AlejandroR] 15-12-09 
                    ' Se anula el uso de cache de controles de indices ya que si se abren dos documentos del mismo 
                    ' tipo y el control esta en la cache entonces solo se puede usar solo en un documento ya que lo obtiene
                    ' por referencia, se usa cache solo si se especifico mediante userconfig

                    bNuevaInstancia = False

                    If Not bUseCache Then
                        bNuevaInstancia = True
                    Else
                        ctrl = DirectCast(Zamba.Core.IndexsBusiness.LoadedControlsTable(ind.ID), DisplayindexCtl)

                        '--------------------------------------------------------------
                        'osanchez
                        'no se deberia hacer disposed de los controles cargados en el 
                        'cache
                        '--------------------------------------------------------------
                        If ctrl.IsDisposed Then
                            bNuevaInstancia = True
                        End If
                    End If

                    If bNuevaInstancia Then

                        ctrl = New DisplayindexCtl(ind, LocalResult.DocType.IsReindex)

                        RemoveHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                        RemoveHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                        AddHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                        AddHandler ctrl.TabPressed, AddressOf Tab_KeyDown

                        If bUseCache Then
                            If Zamba.Core.IndexsBusiness.LoadedControlsTable.Contains(ind.ID) Then
                                Zamba.Core.IndexsBusiness.LoadedControlsTable(ind.ID) = ctrl
                            Else
                                ' Se agrega al hashTable de la propiedad LoadedControlsTable
                                ' el indice tomado..
                                Zamba.Core.IndexsBusiness.LoadedControlsTable.Add(ind.ID, ctrl)
                            End If
                        End If

                    End If

                    ctrl.ReloadindexData(ind)

                Catch ex As System.ObjectDisposedException
                    If LocalResult.DocType.IsReindex = True Or isReIndex = True Then
                        ctrl = New DisplayindexCtl(ind, True)
                    Else
                        ctrl = New DisplayindexCtl(ind, False)
                    End If

                    RemoveHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                    RemoveHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                    AddHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                    AddHandler ctrl.TabPressed, AddressOf Tab_KeyDown

                    If bUseCache Then
                        If Zamba.Core.IndexsBusiness.LoadedControlsTable.Contains(ind.ID) Then
                            Zamba.Core.IndexsBusiness.LoadedControlsTable(ind.ID) = ctrl
                        Else
                            ' Se agrega al hashTable de la propiedad LoadedControlsTable
                            ' el indice tomado..
                            Zamba.Core.IndexsBusiness.LoadedControlsTable.Add(ind.ID, ctrl)
                        End If
                    End If

                Catch ex As Exception
                    If LocalResult.DocType.IsReindex = True Or isReIndex = True Then
                        ctrl = New DisplayindexCtl(ind, True)
                    Else
                        ctrl = New DisplayindexCtl(ind, False)
                    End If

                    RemoveHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                    RemoveHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                    AddHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                    AddHandler ctrl.TabPressed, AddressOf Tab_KeyDown

                    If bUseCache Then
                        If Zamba.Core.IndexsBusiness.LoadedControlsTable.Contains(ind.ID) Then
                            Zamba.Core.IndexsBusiness.LoadedControlsTable(ind.ID) = ctrl
                        Else
                            ' Se agrega al hashTable de la propiedad LoadedControlsTable
                            ' el indice tomado..
                            Zamba.Core.IndexsBusiness.LoadedControlsTable.Add(ind.ID, ctrl)
                        End If
                    End If
                End Try
                'End If

                If offline = False Then
                    RemoveHandler ctrl.DataChanged, AddressOf DataChanged
                    AddHandler ctrl.DataChanged, AddressOf DataChanged
                End If

                ctrl.Dock = DockStyle.Top

                'Si el indice es referencial, no debe estar habilitado
                ctrl.Enabled = Not ind.isReference

                ' Agrega el control...
                ' Segun permiso 05-01-2008 [diego]
                If UserBusiness.Rights.GetUserRights(UserBusiness.CurrentUser, Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.ViewRightsByIndex, doctypeid) Then
                    Dim ShowIndex As Boolean = False
                    'Si no se cargaron los indices antes los cargo
                    If IsNothing(IRI) Then
                        IRI = UserBusiness.Rights.GetIndexsRights(doctypeid, UserBusiness.CurrentUser.ID, True, True)
                    End If
                    Dim IR As IndexsRightsInfo = DirectCast(IRI(ctrl.Index.ID), IndexsRightsInfo)
                    Dim dsIndexsPropertys As DataSet = DocTypesBusiness.GetIndexsProperties(doctypeid, True)

                    For Each indexid As Int64 In IRI.Keys
                        If indexid = ctrl.Index.ID Then
                            If IR.GetIndexRightValue(RightsType.IndexView) Then ShowIndex = True
                            If IR.GetIndexRightValue(RightsType.IndexEdit) = False Then
                                ctrl.Enabled = False
                                ctrl.IsEnabled = False
                            End If
                            '[Sebastian] 14-07-2009
                            For Each Index As DataRow In dsIndexsPropertys.Tables(0).Rows
                                If ctrl.Index.ID = CLng(Index("Index_Id")) AndAlso Index("MustComplete") = 1 _
                                    AndAlso ctrl.Controls(2).Text.Contains("*") = False Then

                                    ctrl.Controls(2).Text = ctrl.Controls(2).Text + " *"
                                    Exit For
                                ElseIf IR.GetIndexRightValue(RightsType.IndexRequired) = True AndAlso ctrl.Controls(2).Text.Contains("*") = False Then
                                    ctrl.Controls(2).Text = ctrl.Controls(2).Text + " *"
                                    Exit For
                                End If
                            Next
                            'If IR.GetIndexRightValue(RightsType.IndexRequired) Then
                            '    ctrl.Controls(2).Text = ctrl.Controls(2).Text + " *"
                            'End If

                            Exit For

                        End If
                    Next

                    'For Each indxRow As DataRow In dsIndexsPropertys.Tables(0).Rows
                    'If indxRow("index_id") = ctrl.Index.ID Then
                    If IR.GetIndexRightValue(RightsType.IndexView) Then ShowIndex = True
                    If IR.GetIndexRightValue(RightsType.IndexEdit) = False Then ctrl.Enabled = False
                    If ctrl.Index.Required AndAlso ctrl.Controls(2).Text.Contains("*") = False Then
                        ctrl.Controls(2).Text = ctrl.Controls(2).Text + " *"
                    End If

                    'Exit For

                    'End If
                    'Next

                    If ShowIndex Then Me.Panel1.Controls.Add(ctrl)
                Else
                    ' no se restringe su uso
                    Me.Panel1.Controls.Add(ctrl)
                End If

                If ctrl.Enabled = True Then
                    ctrl.Enabled = ind.Enabled
                End If
            Next
            Me.Panel1.ResumeLayout()
            'establesco los tabindex
            Dim tindex As Int32 = Me.Panel1.Controls.Count - 1
            For Each control As Control In Me.Panel1.Controls
                control.TabIndex = tindex
                tindex -= 1
            Next

        Catch ex As Exception
            Try
                Dim i As Integer = DirectCast(LocalResult.Parent, DocType).Indexs.Count - 1
                Me.Panel1.SuspendLayout()
                For i = DirectCast(LocalResult.Parent, DocType).Indexs.Count - 1 To 0 Step -1
                    Dim ind As Zamba.Core.Index = DirectCast(LocalResult.Parent, DocType).Indexs(i)
                    'Fijarse si tengo el indice ya cargado y no instanciar uno nuevo
                    'Cache de indices
                    Dim ctrl As DisplayindexCtl

                    Dim FlagCoinciden As Boolean = False
                    Dim Ctrl2 As DisplayindexCtl = Zamba.Core.IndexsBusiness.LoadedControlsTable(ind.ID)
                    If IsNothing(Ctrl2) = False AndAlso LocalResult.DocType.IsReindex = Ctrl2.IsEnabled Then FlagCoinciden = True

                    If Not Zamba.Core.IndexsBusiness.LoadedControlsTable.ContainsKey(ind.ID) OrElse FlagCoinciden = False Then
                        If LocalResult.DocType.IsReindex = True Or isReIndex = True Then
                            ctrl = New DisplayindexCtl(ind, True)
                        Else
                            ctrl = New DisplayindexCtl(ind, False)
                        End If
                        RemoveHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                        RemoveHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                        AddHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                        AddHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                        If bUseCache Then
                            If Not Zamba.Core.IndexsBusiness.LoadedControlsTable.ContainsKey(ind.ID) Then
                                Zamba.Core.IndexsBusiness.LoadedControlsTable.Add(ind.ID, ctrl)
                            End If
                        End If
                    Else
                        Try
                            ctrl = Zamba.Core.IndexsBusiness.LoadedControlsTable(ind.ID)
                            If ctrl.IsDisposed Then
                                If LocalResult.DocType.IsReindex = True Or isReIndex = True Then
                                    ctrl = New DisplayindexCtl(ind, True)
                                Else
                                    ctrl = New DisplayindexCtl(ind, False)
                                End If
                                RemoveHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                                RemoveHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                                AddHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                                AddHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                                If bUseCache Then
                                    If Zamba.Core.IndexsBusiness.LoadedControlsTable.Contains(ind.ID) Then
                                        Zamba.Core.IndexsBusiness.LoadedControlsTable(ind.ID) = ctrl
                                    Else
                                        Zamba.Core.IndexsBusiness.LoadedControlsTable.Add(ind.ID, ctrl)
                                    End If
                                End If
                            End If
                            ctrl.ReloadindexData(ind)
                        Catch exc As System.ObjectDisposedException
                            If LocalResult.DocType.IsReindex = True Or isReIndex = True Then
                                ctrl = New DisplayindexCtl(ind, True)
                            Else
                                ctrl = New DisplayindexCtl(ind, False)
                            End If
                            RemoveHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                            RemoveHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                            AddHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                            AddHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                            If bUseCache Then
                                If Zamba.Core.IndexsBusiness.LoadedControlsTable.Contains(ind.ID) Then
                                    Zamba.Core.IndexsBusiness.LoadedControlsTable(ind.ID) = ctrl
                                Else
                                    Zamba.Core.IndexsBusiness.LoadedControlsTable.Add(ind.ID, ctrl)
                                End If
                            End If
                        Catch exc As Exception
                            If LocalResult.DocType.IsReindex = True Or isReIndex = True Then
                                ctrl = New DisplayindexCtl(ind, True)
                            Else
                                ctrl = New DisplayindexCtl(ind, False)
                            End If
                            RemoveHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                            RemoveHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                            AddHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                            AddHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                            If bUseCache Then
                                If Zamba.Core.IndexsBusiness.LoadedControlsTable.Contains(ind.ID) Then
                                    Zamba.Core.IndexsBusiness.LoadedControlsTable(ind.ID) = ctrl
                                Else
                                    Zamba.Core.IndexsBusiness.LoadedControlsTable.Add(ind.ID, ctrl)
                                End If
                            End If
                        End Try
                    End If
                    If offline = False Then
                        RemoveHandler ctrl.DataChanged, AddressOf DataChanged
                        AddHandler ctrl.DataChanged, AddressOf DataChanged
                    End If
                    ctrl.Dock = DockStyle.Top
                    '      ctrl.Enabled = Result.IsReindex
                    ' Agrega el control...
                    ' Segun permiso 05-01-2008 [diego]
                    If UserBusiness.Rights.GetUserRights(UserBusiness.CurrentUser, Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.ViewRightsByIndex, doctypeid) Then
                        Dim IRI As Hashtable = UserBusiness.Rights.GetIndexsRights(doctypeid, UserBusiness.CurrentUser.ID, True, True)
                        If DirectCast(IRI(ctrl.Index.ID), IndexsRightsInfo).GetIndexRightValue(RightsType.IndexView) = True Then
                            For Each indexid As Int64 In IRI.Keys
                                If indexid = ctrl.Index.ID Then
                                    If DirectCast(IRI(ctrl.Index.ID), IndexsRightsInfo).GetIndexRightValue(RightsType.IndexEdit) = False Then
                                        ctrl.Enabled = False
                                        Exit For
                                    End If
                                End If
                            Next
                            Me.Panel1.Controls.Add(ctrl)
                        End If
                        If DirectCast(IRI(ctrl.Index.ID), IndexsRightsInfo).GetIndexRightValue(RightsType.IndexRequired) = True Then
                            For Each indexid As Int64 In IRI.Keys
                                If indexid = ctrl.Index.ID Then
                                    If DirectCast(IRI(ctrl.Index.ID), IndexsRightsInfo).GetIndexRightValue(RightsType.IndexEdit) = False Then
                                        DirectCast(ctrl.Controls(0), TextBox).Text = DirectCast(ctrl.Controls(0), TextBox).Text + " *"
                                        Exit For
                                    End If
                                End If
                            Next
                            Me.Panel1.Controls.Add(ctrl)
                        End If

                    Else
                        ' no se restringe su uso
                        Me.Panel1.Controls.Add(ctrl)
                    End If


                    If ctrl.Enabled = True Then
                        ctrl.Enabled = ind.Enabled
                    End If


                Next
                Me.Panel1.ResumeLayout()
                'establesco los tabindex
                Dim tindex As Int32 = Me.Panel1.Controls.Count - 1
                For Each control As Control In Me.Controls
                    control.TabIndex = tindex
                    tindex -= 1
                Next
            Catch exc As ObjectDisposedException
                Zamba.Core.ZClass.raiseerror(ex)
            Catch exc As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End Try

        Me.Cursor = cur
    End Sub

    Private Sub GeneracionIndices(ByVal doctypeid As Int64, ByVal IndexsIds As ArrayList, Optional ByVal IsReadonly As Boolean = True)
        Dim cur As Cursor = Me.Cursor
        Me.Cursor = Cursors.WaitCursor

        Try
            Dim i As Int32
            Me.Panel1.SuspendLayout()

            ' Se recorren los indices del result...
            For i = LocalResult.Indexs.Count - 1 To 0 Step -1

                ' se toman los indices asociados al documento...
                Dim ind As Zamba.Core.Index = LocalResult.Indexs(i)
                If IndexsIds.IndexOf(ind.ID.ToString) <> -1 Then
                    Dim ctrl As DisplayindexCtl
                    Dim FlagCoinciden As Boolean = False

                    '   Dim Ctrl2 As DisplayindexCtl = Zamba.Core.Indexs_Factory.LoadedControlsTable(ind.ID)

                    Dim Ctrl2 As DisplayindexCtl = Nothing

                    If IsNothing(Ctrl2) = False AndAlso LocalResult.DocType.IsReindex = Ctrl2.IsEnabled Then FlagCoinciden = True

                    If Not Zamba.Core.IndexsBusiness.LoadedControlsTable.ContainsKey(ind.ID) OrElse FlagCoinciden = False Then
                        If IsReadonly = False Then LocalResult.DocType.IsReindex = True

                        If LocalResult.DocType.IsReindex = True Or isReIndex = True Then
                            ctrl = New DisplayindexCtl(ind, True)
                        Else
                            ctrl = New DisplayindexCtl(ind, False)
                        End If

                        RemoveHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                        RemoveHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                        AddHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                        AddHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                        RemoveHandler ctrl.DataChanged, AddressOf DataChanged
                        AddHandler ctrl.DataChanged, AddressOf DataChanged

                        If Not Zamba.Core.IndexsBusiness.LoadedControlsTable.ContainsKey(ind.ID) Then

                            ' Se agrega al hashTable de la propiedad LoadedControlsTable
                            ' el indice tomado..
                            Zamba.Core.IndexsBusiness.LoadedControlsTable.Add(ind.ID, ctrl)
                        End If
                    Else
                        Try

                            ctrl = Zamba.Core.IndexsBusiness.LoadedControlsTable(ind.ID)

                            If ctrl.IsDisposed Then
                                If IsReadonly = False Then LocalResult.DocType.IsReindex = True

                                If LocalResult.DocType.IsReindex = True Or isReIndex = True Then
                                    ctrl = New DisplayindexCtl(ind, True)
                                Else
                                    ctrl = New DisplayindexCtl(ind, False)
                                End If

                                RemoveHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                                RemoveHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                                AddHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                                AddHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                                If Zamba.Core.IndexsBusiness.LoadedControlsTable.Contains(ind.ID) Then
                                    Zamba.Core.IndexsBusiness.LoadedControlsTable(ind.ID) = ctrl
                                Else
                                    ' Se agrega al hashTable de la propiedad LoadedControlsTable
                                    ' el indice tomado..
                                    Zamba.Core.IndexsBusiness.LoadedControlsTable.Add(ind.ID, ctrl)
                                End If
                            End If

                            ctrl.ReloadindexData(ind)

                        Catch ex As System.ObjectDisposedException
                            If IsReadonly = False Then LocalResult.DocType.IsReindex = True

                            If LocalResult.DocType.IsReindex = True Or isReIndex = True Then
                                ctrl = New DisplayindexCtl(ind, True)
                            Else
                                ctrl = New DisplayindexCtl(ind, False)
                            End If

                            RemoveHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                            RemoveHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                            AddHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                            AddHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                            If Zamba.Core.IndexsBusiness.LoadedControlsTable.Contains(ind.ID) Then
                                Zamba.Core.IndexsBusiness.LoadedControlsTable(ind.ID) = ctrl
                            Else
                                ' Se agrega al hashTable de la propiedad LoadedControlsTable
                                ' el indice tomado..
                                Zamba.Core.IndexsBusiness.LoadedControlsTable.Add(ind.ID, ctrl)
                            End If
                        Catch ex As Exception
                            If IsReadonly = False Then LocalResult.DocType.IsReindex = True

                            If LocalResult.DocType.IsReindex = True Or isReIndex = True Then
                                ctrl = New DisplayindexCtl(ind, True)
                            Else
                                ctrl = New DisplayindexCtl(ind, False)
                            End If

                            RemoveHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                            RemoveHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                            AddHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                            AddHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                            If Zamba.Core.IndexsBusiness.LoadedControlsTable.Contains(ind.ID) Then
                                Zamba.Core.IndexsBusiness.LoadedControlsTable(ind.ID) = ctrl
                            Else
                                ' Se agrega al hashTable de la propiedad LoadedControlsTable
                                ' el indice tomado..
                                Zamba.Core.IndexsBusiness.LoadedControlsTable.Add(ind.ID, ctrl)
                            End If
                        End Try
                    End If

                    If offline = False Then
                        RemoveHandler ctrl.DataChanged, AddressOf DataChanged
                        AddHandler ctrl.DataChanged, AddressOf DataChanged
                    End If
                    ctrl.Dock = DockStyle.Top
                    '      ctrl.Enabled = Result.IsReindex

                    If ctrl.Enabled = True Then
                        ctrl.Enabled = ind.Enabled
                    End If

                    ' Agrega el control...
                    ' Segun permiso 05-01-2008 [diego]
                    Me.Panel1.Controls.Add(ctrl)
                    If UserBusiness.Rights.GetUserRights(UserBusiness.CurrentUser, Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.ViewRightsByIndex, doctypeid) Then
                        Dim IRI As Hashtable = UserBusiness.Rights.GetIndexsRights(doctypeid, UserBusiness.CurrentUser.ID, True, True)
                        If DirectCast(IRI(ctrl.Index.ID), IndexsRightsInfo).GetIndexRightValue(RightsType.IndexView) = True Then
                            For Each indexid As Int64 In IRI.Keys
                                If indexid = ctrl.Index.ID Then
                                    If DirectCast(IRI(ctrl.Index.ID), IndexsRightsInfo).GetIndexRightValue(RightsType.IndexEdit) = False OrElse ind.Enabled = False Then
                                        ctrl.Enabled = False
                                    End If
                                    If IsReadonly = False AndAlso ind.Enabled = True Then
                                        ctrl.Enabled = True
                                    End If
                                End If
                            Next
                            Me.Panel1.Controls.Add(ctrl)
                        End If
                        If DirectCast(IRI(ctrl.Index.ID), IndexsRightsInfo).GetIndexRightValue(RightsType.IndexRequired) = True Then
                            For Each indexid As Int64 In IRI.Keys
                                If indexid = ctrl.Index.ID Then
                                    If DirectCast(IRI(ctrl.Index.ID), IndexsRightsInfo).GetIndexRightValue(RightsType.IndexEdit) = False Then
                                        DirectCast(ctrl.Controls(0), TextBox).Text = DirectCast(ctrl.Controls(0), TextBox).Text + " *"
                                        Exit For
                                    End If
                                End If
                            Next
                            Me.Panel1.Controls.Add(ctrl)
                        End If
                    Else
                        ' no se restringe su uso
                        Me.Panel1.Controls.Add(ctrl)
                    End If
                End If
            Next
            Me.Panel1.ResumeLayout()
            'establesco los tabindex
            Dim tindex As Int32 = Me.Panel1.Controls.Count - 1
            For Each control As Control In Me.Panel1.Controls
                control.TabIndex = tindex
                tindex -= 1
            Next

            'todo ver el filtro de indices en exceptions

        Catch ex As Exception

            Try
                Dim i As Integer = DirectCast(LocalResult.Parent, DocType).Indexs.Count - 1
                Me.Panel1.SuspendLayout()
                For i = DirectCast(LocalResult.Parent, DocType).Indexs.Count - 1 To 0 Step -1
                    Dim ind As Zamba.Core.Index = DirectCast(LocalResult.Parent, DocType).Indexs(i)
                    'Fijarse si tengo el indice ya cargado y no instanciar uno nuevo
                    'Cache de indices
                    Dim ctrl As DisplayindexCtl

                    Dim FlagCoinciden As Boolean = False
                    Dim Ctrl2 As DisplayindexCtl = Zamba.Core.IndexsBusiness.LoadedControlsTable(ind.ID)
                    If IsNothing(Ctrl2) = False AndAlso LocalResult.DocType.IsReindex = Ctrl2.IsEnabled Then FlagCoinciden = True

                    If Not Zamba.Core.IndexsBusiness.LoadedControlsTable.ContainsKey(ind.ID) OrElse FlagCoinciden = False Then
                        If IsReadonly = False Then LocalResult.DocType.IsReindex = True
                        If LocalResult.DocType.IsReindex = True Or isReIndex = True Then
                            ctrl = New DisplayindexCtl(ind, True)
                        Else
                            ctrl = New DisplayindexCtl(ind, False)
                        End If
                        RemoveHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                        RemoveHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                        AddHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                        AddHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                        If Not Zamba.Core.IndexsBusiness.LoadedControlsTable.ContainsKey(ind.ID) Then
                            Zamba.Core.IndexsBusiness.LoadedControlsTable.Add(ind.ID, ctrl)
                        End If
                    Else
                        Try
                            ctrl = Zamba.Core.IndexsBusiness.LoadedControlsTable(ind.ID)
                            If ctrl.IsDisposed Then
                                If IsReadonly = False Then LocalResult.DocType.IsReindex = True
                                If LocalResult.DocType.IsReindex = True Or isReIndex = True Then
                                    ctrl = New DisplayindexCtl(ind, True)
                                Else
                                    ctrl = New DisplayindexCtl(ind, False)
                                End If
                                RemoveHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                                RemoveHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                                AddHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                                AddHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                                If Zamba.Core.IndexsBusiness.LoadedControlsTable.Contains(ind.ID) Then
                                    Zamba.Core.IndexsBusiness.LoadedControlsTable(ind.ID) = ctrl
                                Else
                                    Zamba.Core.IndexsBusiness.LoadedControlsTable.Add(ind.ID, ctrl)
                                End If
                            End If
                            ctrl.ReloadindexData(ind)
                        Catch exc As System.ObjectDisposedException
                            If IsReadonly = False Then LocalResult.DocType.IsReindex = True
                            If LocalResult.DocType.IsReindex = True Or isReIndex = True Then
                                ctrl = New DisplayindexCtl(ind, True)
                            Else
                                ctrl = New DisplayindexCtl(ind, False)
                            End If
                            RemoveHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                            RemoveHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                            AddHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                            AddHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                            If Zamba.Core.IndexsBusiness.LoadedControlsTable.Contains(ind.ID) Then
                                Zamba.Core.IndexsBusiness.LoadedControlsTable(ind.ID) = ctrl
                            Else
                                Zamba.Core.IndexsBusiness.LoadedControlsTable.Add(ind.ID, ctrl)
                            End If
                        Catch exc As Exception
                            If IsReadonly = False Then LocalResult.DocType.IsReindex = True
                            If LocalResult.DocType.IsReindex = True Or isReIndex = True Then
                                ctrl = New DisplayindexCtl(ind, True)
                            Else
                                ctrl = New DisplayindexCtl(ind, False)
                            End If
                            RemoveHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                            RemoveHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                            AddHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                            AddHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                            If Zamba.Core.IndexsBusiness.LoadedControlsTable.Contains(ind.ID) Then
                                Zamba.Core.IndexsBusiness.LoadedControlsTable(ind.ID) = ctrl
                            Else
                                Zamba.Core.IndexsBusiness.LoadedControlsTable.Add(ind.ID, ctrl)
                            End If
                        End Try
                    End If

                    If offline = False Then
                        RemoveHandler ctrl.DataChanged, AddressOf DataChanged
                        AddHandler ctrl.DataChanged, AddressOf DataChanged
                    End If
                    ctrl.Dock = DockStyle.Top
                    '      ctrl.Enabled = Result.IsReindex


                    If ctrl.Enabled = True Then
                        ctrl.Enabled = ind.Enabled
                    End If


                    Me.Panel1.Controls.Add(ctrl)
                Next
                Me.Panel1.ResumeLayout()
                'establesco los tabindex
                Dim tindex As Int32 = Me.Panel1.Controls.Count - 1
                For Each control As Control In Me.Controls
                    control.TabIndex = tindex
                    tindex -= 1
                Next


            Catch exc As ObjectDisposedException
                Zamba.Core.ZClass.raiseerror(ex)
            Catch exc As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End Try

        Me.Cursor = cur
    End Sub

    'Metodo para cuando no cambia el doctype
    Public Sub ReloadIndexsData(ByVal doctypeid As Int64)
        Me.SuspendLayout()
        Try
            'Dim i As Integer
            If LocalResult.Indexs.Count = Me.Panel1.Controls.Count Then
                For Each _Index As Index In LocalResult.Indexs
                    For Each ctrl As DisplayindexCtl In Me.Panel1.Controls
                        If ctrl.Index.ID = _Index.ID Then
                            RemoveHandler ctrl.DataChanged, AddressOf DataChanged
                            ctrl.ReloadindexData(_Index)

                            'Se comenta este addHandler, ya que generaba llamadas al mismo evento varias veces
                            'todo revisar
                            'AddHandler ctrl.DataChanged, AddressOf DataChanged
                        End If
                    Next
                Next
            Else
                Me.Panel1.Invalidate()
                ClearIndexs()
                Me.GeneracionIndices(doctypeid)
                Me.Panel1.Update()
            End If
        Catch ex As System.ArgumentOutOfRangeException
            Me.Panel1.Invalidate()
            ClearIndexs()
            Me.GeneracionIndices(doctypeid)
            Me.Panel1.Update()
        End Try
        Me.ResumeLayout()
    End Sub
#End Region

#Region "Borro los datos de los indices"
    Public Sub CleanIndexs()
        Try
            For Each c As DisplayindexCtl In Me.Panel1.Controls
                'c.Index.DataTemp = ""
                c.CleanDataTemp()
            Next
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region
#Region "Borro los indices"
    Public Sub ClearIndexs()
        Try
            Me.Panel1.Controls.Clear()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Guarda los indices"
    Private Function IsValid() As Boolean
        '  Dim Indexs As New ArrayList
        For Each c As DisplayindexCtl In Me.Panel1.Controls
            If c.isValid = False Then
                Return False
                Exit For
            End If
        Next
        Return True
    End Function


    Public Function GetParent() As ZambaCore
        Return LocalResult.Parent
    End Function

    Public Function GetIndexs() As ArrayList
        Dim Indexs As New ArrayList
        Dim IndexsInvertidos As New ArrayList

        For Each c As DisplayindexCtl In Me.Panel1.Controls
            IndexsInvertidos.Add(c.Index)
        Next

        Dim i As Integer
        For i = IndexsInvertidos.Count - 1 To 0 Step -1
            Indexs.Add(IndexsInvertidos(i))
        Next

        Return Indexs
    End Function

    Public Event IndexsSaved(ByRef Result As Result)
    Public Sub FlagTrue()
        Me.FlagIndexEdited = True
    End Sub

    ''' <summary>
    ''' Guardar los datos de los indices
    ''' </summary>
    ''' <param name="NoQuestion">Preguntar si se quiere guardar</param>
    ''' <param name="reLoad">Recargar los indices</param>
    ''' <remarks>
    ''' [Sebastian] 17-06-2009 Modified Se agrego evento para actualizar los indices 
    ''' del formulario electronico asociado.
    ''' </remarks>
    ''' <history>Diego 18/6/2008 Modified
    '''  [Tomas]    03/08/2009  Modified    Se configura un parámetro nuevo del InsertDocument para que no inserte los valores
    '''                                     de búsqueda de palabras ya que lo hace en Results_Business.SaveModifiedIndexData
    '''                                     con todos los índices recargados (antes no insertaba nada porque no tenía los índices
    '''                                     recargados e iteraba buscando los índices para nada).
    ''' [Sebastian] 13/11/2009 Modified     Se comento foco en la barra de indices porque dificultaba la funcionalidad de desplaza
    '''                                     miento con las flechas de dirección. Se rehizo porque se perdieron los cambios
    ''' </history>
    Public Sub Save(ByVal NoQuestion As Boolean, Optional ByVal reLoad As Boolean = True, Optional ByVal reloadResult As Boolean = True)
        Try
            If Not IsNothing(LocalResult) AndAlso LocalResult.DocType.IsReindex Then
                '    [Sebastian 13-11-2009]
                'Me.DesignSandBar.Focus()
                If Me.FlagIndexEdited = True Then
                    If NoQuestion OrElse MessageBox.Show("¿Desea guardar los cambios realizados en los indices?", "Edicion de Documentos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        If Me.IsValid Then
                            If Results_Business.ValidateIndexDatabyRights(LocalResult) = False Then
                                MessageBox.Show("Debe Completar los indices Requeridos", "Edicion de Documentos")
                                Exit Sub
                            End If
                            ' se toman los nuevos indices de la vista...

                            '[Tomas] 02/10/2009     Se comenta esta iteración ya que no hace nada.
                            '                       Se mueve abajo y se modifica en los casos de updates.
                            '''Diego: Comento esto porque si se estan mostrando indices especificos y el result tiene todos, no serviria pisar la coleccion
                            '''LocalResult.Indexs = Me.GetIndexs
                            '''Hago esto en cambio
                            ''For Each controlI As Index In GetIndexs()
                            ''    For Each indice As Index In LocalResult.Indexs
                            ''        If controlI.ID = indice.ID Then
                            ''            indice.DataTemp = controlI.DataTemp
                            ''            Exit For
                            ''        End If
                            ''    Next
                            ''Next

                            If LocalResult.ISVIRTUAL AndAlso LocalResult.ID = 0 Then
                                'Si el documento es nuevo se inserta
                                Dim insertresult As InsertResult
                                insertresult = Results_Business.InsertDocument(LocalResult, True, False, False, True, LocalResult.ISVIRTUAL, False, False)
                                insertresult = Nothing
                            Else
                                'Si el documento ya existía se obtienen los índices modificados
                                'y se guardan unicamente estos.
                                Dim descripcion As New StringBuilder
                                descripcion.Append("Modificaciones realizadas en '" & LocalResult.Name & "': ")
                                Dim modifiedIndex As Generic.List(Of Int64) = GetModifiedIndexs(descripcion)
                                If modifiedIndex.Count <> 0 Then
                                    ' Se actualizan los nuevos indices..., si reload = false no actualizo los indices
                                    Dim Results_Business As New Results_Business
                                    Results_Business.SaveModifiedIndexData(LocalResult, True, reLoad, modifiedIndex)
                                    UserBusiness.Rights.SaveAction(LocalResult.ID, Zamba.Core.ObjectTypes.Documents, Zamba.Core.RightsType.ReIndex, descripcion.ToString)
                                End If
                                modifiedIndex = Nothing
                                descripcion = Nothing
                            End If

                            'Me.Result.HashIndexData = Me.GetIndexs

                            'Dim Indexs As New ArrayList
                            For Each c As DisplayindexCtl In Me.Panel1.Controls
                                c.Commit()
                            Next
                            Try
                                Zamba.Core.Results_Business.UpdateAutoName(LocalResult)
                            Catch ex As Exception
                                Zamba.Core.ZClass.raiseerror(ex)
                            End Try
                            RaiseEvent IndexsSaved(LocalResult)
                            '[Sebastian] 17-06-2009 Se agrego este evento para actualizar los indices del
                            'formulario
                            '[Ezequiel] Se cambio el raiseevent por el metodo de la clase zcontrol ya que el mismo invoca el evento.
                            ChangeControl(LocalResult)
                        End If
                    Else
                        Me.FlagIndexEdited = False
                        '          Dim Indexs As New ArrayList
                        For Each c As DisplayindexCtl In Me.Panel1.Controls
                            c.RollBack()
                        Next
                    End If
                ElseIf LocalResult.ISVIRTUAL AndAlso LocalResult.ID = 0 Then
                    Dim insertresult As InsertResult
                    insertresult = Results_Business.InsertDocument(LocalResult, True, False, False, True, LocalResult.ISVIRTUAL, False, False)
                End If
            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Exit Sub
        Finally
            FlagIndexEdited = False
            If Not IsNothing(Me.LocalResult) Then
                LocalResult.FlagIndexEdited = False
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Obtiene los indices modificados
    ''' </summary>
    Private Function GetModifiedIndexs(ByRef descripcion As StringBuilder) As Generic.List(Of Int64)
        Dim modifiedIndex As New Generic.List(Of Int64)
        For Each controlI As Index In GetIndexs()
            For Each indice As Index In LocalResult.Indexs
                If (controlI.ID = indice.ID) AndAlso (String.Compare(indice.Data, indice.DataTemp) <> 0) Then
                    modifiedIndex.Add(indice.ID)
                    descripcion.Append("índice '" & indice.Name & "' de '" & indice.Data & "' a '" & indice.DataTemp & "', ")
                    Exit For
                End If
            Next
        Next
        descripcion = descripcion.Remove(descripcion.Length - 2, 2)
        Return modifiedIndex
    End Function
#End Region
#Region "Eventos"
    Public Event Panel5MouseEnter()
    '    Public Event SaveIndexs(ByRef Result As Result, ByVal NoQuestion As Boolean)
    ' Public Event RemoveChanges(ByRef Result As Result)
    'Public Event LimpiarIndices()
    Public Event IndexsChanged(ByRef Result As Result, ByVal index As Index)
#End Region

    Private Sub Panel5_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel1.MouseEnter
        RaiseEvent Panel5MouseEnter()
    End Sub

    Private Sub DesignSandBar_ButtonClick(ByVal sender As System.Object, ByVal e As ToolStripItemClickedEventArgs) Handles DesignSandBar.ItemClicked
        Select Case CStr(e.ClickedItem.Tag)
            Case "DESHACER"
                Me.ShowIndexs(LocalResult, False)
            Case "GUARDAR"
                Me.Save(True)
            Case "LIMPIAR INDICES"
                Me.LimpiarIndices()
        End Select
    End Sub
    'Cuando muevo el scroll saco el foco de los textbox porque sino falla el scroll
    'ya que va siempre al textbox seleccionado
    Private Sub Panel1_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles Panel1.Scroll
        Me.Panel1.Focus()
    End Sub

   
#Region "Metodos Publicos"

    'Private Sub CancelIndexSaved()
    '    Me.ShowEspecifiedIndexs(LocalResult, LocalIndexs, False)
    'End Sub
    Public Sub ShowPanelWithAceptandCancel(Optional ByVal Show As Boolean = True)
        Panel2.Visible = True
    End Sub
#End Region


    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        If saveOnAccept Then
            Me.Save(True)
        End If

        'lanza el evento con el dialog result simulando el de un formulario
        ZClass.HandleEventDialogResult(DialogResult.OK)
        Me.ParentForm.Close()
    End Sub

    ''' <summary>
    '''     cancela la ejecución de la regla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        'lanza el evento con el dialog result simulando el de un formulario
        ZClass.HandleEventDialogResult(DialogResult.Cancel)
        Me.ParentForm.Close()
    End Sub
End Class
