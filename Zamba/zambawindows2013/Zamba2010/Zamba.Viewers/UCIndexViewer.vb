Imports Zamba.Core
Imports Zamba.Indexs
Imports System.Text
Imports System.Collections.Generic

Public Class UCIndexViewer
    Inherits ZControl
    Implements IDisposable


#Region " Windows Form Designer generated code "
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private isDisposed As Boolean

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If Not isDisposed Then
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If

                RaiseEvent ClearReferences(Me)

                If DesignSandBar IsNot Nothing Then
                    DesignSandBar.Dispose()
                    DesignSandBar = Nothing
                End If
                If BtnDeshacer IsNot Nothing Then
                    BtnDeshacer.Dispose()
                    BtnDeshacer = Nothing
                End If
                If BtnGuardar IsNot Nothing Then
                    BtnGuardar.Dispose()
                    BtnGuardar = Nothing
                End If
                If btnAceptar IsNot Nothing Then
                    btnAceptar.Dispose()
                    btnAceptar = Nothing
                End If
                If btnCancelar IsNot Nothing Then
                    btnCancelar.Dispose()
                    btnCancelar = Nothing
                End If
                If ButnLimpiarIndices IsNot Nothing Then
                    ButnLimpiarIndices.Dispose()
                    ButnLimpiarIndices = Nothing
                End If
                If PnlButtonContainer IsNot Nothing Then
                    PnlButtonContainer.Dispose()
                    PnlButtonContainer = Nothing
                End If
                If Panel1 IsNot Nothing Then
                    For i As Int16 = 0 To Panel1.Controls.Count - 1
                        Panel1.Controls(i).Dispose()
                    Next
                    Panel1.Controls.Clear()
                    Panel1.Dispose()
                    Panel1 = Nothing
                End If
            End If
            MyBase.Dispose(disposing)
        End If
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer
    Friend WithEvents Panel1 As ZPanel
    Friend WithEvents DesignSandBar As ZToolBar
    Friend WithEvents BtnDeshacer As ToolStripButton
    Friend WithEvents BtnGuardar As ToolStripButton
    Friend WithEvents btnAceptar As ZButton
    Friend WithEvents btnCancelar As ZButton
    Friend WithEvents ButnLimpiarIndices As ToolStripButton
    Friend WithEvents PnlButtonContainer As ZPanel
    Public Event ClearReferences(ByRef control As UCIndexViewer)

    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Panel1 = New ZPanel()
        DesignSandBar = New ZToolBar()
        PnlButtonContainer = New ZPanel()
        btnAceptar = New ZButton()
        btnCancelar = New ZButton()
        BtnDeshacer = New System.Windows.Forms.ToolStripButton()
        BtnGuardar = New System.Windows.Forms.ToolStripButton()
        ButnLimpiarIndices = New System.Windows.Forms.ToolStripButton()
        DesignSandBar.SuspendLayout()
        PnlButtonContainer.SuspendLayout()
        SuspendLayout()
        '
        'Panel1
        '
        Panel1.AutoScroll = True
        Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Panel1.Location = New System.Drawing.Point(0, 39)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(256, 310)
        Panel1.TabIndex = 0
        Panel1.BackColor = Color.White
        '
        'DesignSandBar
        '
        DesignSandBar.ImageScalingSize = New System.Drawing.Size(32, 32)
        DesignSandBar.Items.AddRange(New ToolStripItem() {BtnDeshacer, BtnGuardar, ButnLimpiarIndices})
        DesignSandBar.Location = New System.Drawing.Point(0, 0)
        DesignSandBar.Name = "DesignSandBar"
        DesignSandBar.Size = New System.Drawing.Size(256, 39)
        DesignSandBar.TabIndex = 155
        DesignSandBar.BackColor = AppBlock.ZambaUIHelpers.GetDesignBarColor
        DesignSandBar.GripStyle = ToolStripGripStyle.Hidden
        DesignSandBar.Renderer = New Zamba.AppBlock.MyStripRender()
        '
        'PnlButtonContainer
        '
        PnlButtonContainer.Controls.Add(btnAceptar)
        PnlButtonContainer.Controls.Add(btnCancelar)
        PnlButtonContainer.Dock = System.Windows.Forms.DockStyle.Bottom
        PnlButtonContainer.Location = New System.Drawing.Point(76, 0)
        PnlButtonContainer.Name = "PnlButtonContainer"
        PnlButtonContainer.Size = New System.Drawing.Size(180, 45)
        PnlButtonContainer.TabIndex = 0
        PnlButtonContainer.Visible = False
        '
        'btnAceptar
        '
        btnAceptar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        btnAceptar.Location = New System.Drawing.Point(0, 5)
        btnAceptar.Name = "btnAceptar"
        btnAceptar.Size = New System.Drawing.Size(85, 36)
        btnAceptar.TabIndex = 1
        btnAceptar.Text = "Aceptar"
        btnAceptar.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        btnCancelar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        btnCancelar.Location = New System.Drawing.Point(95, 5)
        btnCancelar.Name = "btnCancelar"
        btnCancelar.Size = New System.Drawing.Size(85, 36)
        btnCancelar.TabIndex = 0
        btnCancelar.Text = "Cancelar"
        btnCancelar.UseVisualStyleBackColor = True
        '
        'BtnDeshacer
        '
        BtnDeshacer.Image = Global.Zamba.Viewers.My.Resources.Resources.appbar_undo_curve
        BtnDeshacer.Name = "BtnDeshacer"
        BtnDeshacer.Size = New System.Drawing.Size(36, 36)
        BtnDeshacer.Tag = "DESHACER"
        BtnDeshacer.ToolTipText = "DESHACER LAS MODIFICACIONES EN LOS ATRIBUTOS"

        '
        'BtnGuardar
        '
        BtnGuardar.Image = Global.Zamba.Viewers.My.Resources.Resources.appbar_save
        BtnGuardar.Name = "BtnGuardar"
        BtnGuardar.Size = New System.Drawing.Size(36, 36)
        BtnGuardar.Tag = "GUARDAR"
        BtnGuardar.ToolTipText = "GUARDAR LAS MODIFICACIONES EN LOS ATRIBUTOS"

        '
        'ButnLimpiarIndices
        '
        ButnLimpiarIndices.Image = Global.Zamba.Viewers.My.Resources.Resources.appbar_clean
        ButnLimpiarIndices.Name = "ButnLimpiarIndices"
        ButnLimpiarIndices.Size = New System.Drawing.Size(36, 36)
        ButnLimpiarIndices.Tag = "LIMPIAR ATRIBUTOS"
        ButnLimpiarIndices.ToolTipText = "LIMPIAR ATRIBUTOS"

        '
        'UCIndexViewer
        '
        BackColor = Zamba.AppBlock.ZambaUIHelpers.GetFormsBackGroundsColor()
        CausesValidation = False
        Controls.Add(Panel1)
        Controls.Add(PnlButtonContainer)
        Controls.Add(DesignSandBar)
        Font = New Font("Tahoma", 9.75!, FontStyle.Bold)
        Location = New System.Drawing.Point(143, 0)
        Name = "UCIndexViewer"
        Size = New System.Drawing.Size(256, 394)
        DesignSandBar.ResumeLayout(False)
        DesignSandBar.PerformLayout()
        PnlButtonContainer.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()

    End Sub

#End Region

    Private _dontOpenTaskAfterAddToWF As String = String.Empty

    Public Property DontOpenTaskAfterAddToWF() As String
        Get
            Return _dontOpenTaskAfterAddToWF
        End Get
        Set(ByVal value As String)
            _dontOpenTaskAfterAddToWF = value
        End Set
    End Property

#Region "Constructores"
    ''' <summary>
    ''' Control que muestra los atributos de un documento
    ''' </summary>
    ''' <param name="btnVisible">Show Undo Button</param>
    ''' <param name="showtoolbar"></param>
    ''' <param name="UseHashForCrtlIndex"></param>
    ''' <param name="IsReIndex">Si se desea por defecto que sea reindexado</param>
    ''' <remarks></remarks>
    Public Sub New(Optional ByVal btnVisible As Boolean = True, Optional ByVal showtoolbar As Boolean = True, Optional ByVal IsReIndex As Boolean = False)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()


        ZTrace.WriteLineIf(ZTrace.IsInfo, "Instancio el UCIndexViewer")
        Me.isReIndex = IsReIndex
        BtnDeshacer.Visible = btnVisible
        BtnGuardar.Visible = btnVisible

        If BtnGuardar.Visible Then
            If Me.ParentForm IsNot Nothing Then
                Me.ParentForm.AcceptButton = Me.BtnGuardar
                Me.ParentForm.CancelButton = Me.BtnDeshacer
            End If
        Else
            If Me.ParentForm IsNot Nothing Then
                Me.ParentForm.AcceptButton = Me.btnAceptar
                Me.ParentForm.CancelButton = Me.btnCancelar
            End If
        End If

        If Not showtoolbar Then
            DesignSandBar.Visible = False
        End If

    End Sub
#End Region

    Public Sub AsignButtonsToParentForm()
        Try
            If BtnGuardar.Visible Then
                If Me.ParentForm IsNot Nothing Then
                    Me.ParentForm.AcceptButton = Me.BtnGuardar
                    Me.ParentForm.CancelButton = Me.BtnDeshacer
                End If
            Else
                If Me.ParentForm IsNot Nothing Then
                    Me.ParentForm.AcceptButton = Me.btnAceptar
                    Me.ParentForm.CancelButton = Me.btnCancelar
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub RefreshIndex(ByVal Action As ResultActions, ByRef currentResult As Result, IsInserting As Boolean)
        If Action = ResultActions.RefreshIndexs Then
            ShowIndexs(currentResult.ID, currentResult.DocTypeId, IsInserting)
        End If
    End Sub

#Region "Variables"
    'Private offline As Boolean
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
    Private LocalIndexs As List(Of Int64)

    Private Const _INDEXUPDATE1 As String = "Atributo '"
    Private Const _INDEXUPDATE2 As String = "' de '"
    Private Const _INDEXUPDATE3 As String = "' a '"
    Private Const _INDEXUPDATE4 As String = "', "
#End Region
#Region "Eventos tab y ENTER"
    Public Event IndexsSaved(ByRef Result As Result)
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
        'If _Index.DataTemp.Length > 0 Then
        FlagIndexEdited = True



        Try
            Dim newFrmGrilla As New frmGrilla()
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo autocompletar para el atributo: " & _Index.Name)
            Dim haskindexs As Hashtable = AutocompleteBCBusiness.ExecuteAutoComplete(LocalResult, _Index, newFrmGrilla, False)
            If Not IsNothing(haskindexs) AndAlso haskindexs.Count > 0 Then
                For Each ind As IIndex In haskindexs.Values
                    For Each ctrl As DisplayindexCtl In Panel1.Controls
                        If ctrl.Index.ID = ind.ID Then
                            ctrl.RefreshDataTemp()
                        End If
                    Next
                Next
            End If
            'If Not IsNothing(AutocompleteBCBusiness.ExecuteAutoComplete(LocalResult, _Index, newFrmGrilla, False)) Then
            '    If Not FlagIsIndexer AndAlso LocalResult.ID <> 0 Then
            '        Me.Save(True)
            '    Else
            '        If String.Compare(_Index.Data, _Index.DataTemp) <> 0 Then
            '            _Index.SetData(_Index.DataTemp)
            '        End If
            '    End If
            '    Me.ShowIndexs(LocalResult)
            '    Me.FlagIndexEdited = True
            'End If
        Catch ex As Exception
            ZClass.raiseerror(ex)

        End Try
        RaiseEvent IndexsChanged(LocalResult, _Index)
        'End If
    End Sub
    Private Sub LimpiarIndices()
        Try
            CleanIndexs()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

#Region "Muestra los atributos"


    Public Sub ShowIndexsForInsert(ByVal result As Result, ByVal IsInserting As Boolean)
        LocalResult = result
        ShowIndexs(LocalResult.ID, LocalResult.DocTypeId, IsInserting, False)
    End Sub


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Result"></param>
    ''' <param name="IfSameResultDontReload"></param>
    ''' <remarks></remarks>
    ''' <history> [Ezequiel] 29/03/2009 Modified - Owner Rights
    Public Sub ShowIndexs(ByVal resultid As Int64, ByVal entityid As Int64, ByVal IsInserting As Boolean, Optional ReloadResult As Object = True)

        If ReloadResult Then
            LocalResult = Results_Business.GetResult(resultid, entityid)
        End If

        If IsNothing(LocalResult) Then
            Exit Sub
        End If

        FlagIndexEdited = False
        LocalResult.FlagIndexEdited = False

        Try

            LocalResult.DocType.RightsLoaded = False
            DocTypesBusiness.GetEditRights(LocalResult.DocType)

            ZTrace.WriteLineIf(ZTrace.IsVerbose, " LocalResult.isShared = " & LocalResult.isShared.ToString)
            ZTrace.WriteLineIf(ZTrace.IsVerbose, " LocalResult.DocTypeId = " & LocalResult.DocTypeId)
            ZTrace.WriteLineIf(ZTrace.IsVerbose, " LocalResult.isShared <> 1 = " & (LocalResult.isShared <> 1).ToString)

            If LocalResult.isShared <> 1 AndAlso LocalResult.DocTypeId <> 0 Then

                Dim ReIndexRight As Boolean = UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.ReIndex, LocalResult.DocTypeId)
                'Tengo Permiso para Editar?
                ZTrace.WriteLineIf(ZTrace.IsVerbose, " ReIndexRight = " & ReIndexRight.ToString)

                If ReIndexRight Then

                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "El Doctype.IsReindex esta en " & LocalResult.DocType.IsReindex.ToString())
                    LocalResult.DocType.IsReindex = True

                    'Esta restringido la edicion solo al creador y soy el creador
                    If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.OwnerChanges, LocalResult.DocTypeId) AndAlso Membership.MembershipHelper.CurrentUser.ID = LocalResult.OwnerID Then
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Esta restringido la edicion solo al creador y soy el creador y el Doctype.IsReindex esta en " & LocalResult.DocType.IsReindex.ToString())

                        LocalResult.DocType.IsReindex = True
                    End If

                    'Esta restringido la edicion solo al creador y NO soy el creador
                    If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.OwnerChanges, LocalResult.DocTypeId) AndAlso Membership.MembershipHelper.CurrentUser.ID <> LocalResult.OwnerID AndAlso LocalResult.DocType.IsReindex = True Then
                        If Not UserBusiness.Rights.DisableOwnerChanges(Membership.MembershipHelper.CurrentUser, LocalResult.DocTypeId) Then
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Esta restringido la edicion solo al creador y NO soy el creador y el Doctype.IsReindex esta en " & LocalResult.DocType.IsReindex.ToString())
                            LocalResult.DocType.IsReindex = False
                        End If

                    End If

                End If

            ElseIf LocalResult.isShared = 1 Then
                If LocalResult.DocTypeId <> 0 Then
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, " LocalResult.isShared = 1 ")
                    LocalResult.DocType.IsReindex = False
                End If
            End If


            If LastShowedDocId = LocalResult.DocTypeId AndAlso LastIsReIndex = LocalResult.DocType.IsReindex Then
                ReloadIndexsData(LocalResult.DocTypeId, IsInserting)
            Else

                ClearIndexs()

                GeneracionIndices(LocalResult.DocTypeId, IsInserting)
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            LastShowedDocId = LocalResult.DocTypeId
            LastIsReIndex = LocalResult.DocType.IsReindex
        End Try
    End Sub
    Public Sub ShowEspecifiedIndexs(ByVal ResultId As Int64, ByVal EntityId As Int64, ByVal IndexsIds As List(Of Int64), ByVal IsReadonly As Boolean)
        LocalResult = Results_Business.GetResult(ResultId, EntityId)

        If IsNothing(LocalResult) Then Exit Sub

        FlagIndexEdited = False
        LocalResult.FlagIndexEdited = False
        Try
            ClearIndexs()
            GeneracionIndices(LocalResult.DocTypeId, IndexsIds, IsReadonly)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            LastShowedDocId = LocalResult.DocType.ID
            'Me.Panel1.ResumeLayout
        End Try
    End Sub


    Public Sub ShowEspecifiedIndexsExporta(ByRef Result As Zamba.Core.Result, ByVal IndexsIds As List(Of Int64), Optional ByVal IfSameResultDontReload As Boolean = False, Optional ByVal IsReadonly As Boolean = True)
        LocalIndexs = IndexsIds
        If Not IsNothing(LocalResult) AndAlso IfSameResultDontReload = True AndAlso LocalResult.ID = Result.ID Then Exit Sub

        LocalResult = Result
        FlagIndexEdited = False
        LocalResult.FlagIndexEdited = False
        Try
            ClearIndexs()
            GeneracionIndices(Result.DocTypeId, IndexsIds, IsReadonly)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            LastShowedDocId = LocalResult.DocType.ID
            'Me.Panel1.ResumeLayout
        End Try
    End Sub


    Private Sub Item_Changed(ByVal IndexID As Long, ByVal NewValue As String)
        ItemChangedForAsociated(IndexID, NewValue)
    End Sub

    Private Sub ItemChangedForAsociated(ByVal indexId As Long, ByVal newValue As String)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "IndexViewer - ha cambiado el indice:  " & indexId & " valor: " & newValue)

        'Se itera por los controles de indices(contenidos en panel1) para obtener los hijos del indice que cambio
        'y asignarle el valor que ha cambiado.
        For Each e As DisplayindexCtl In Panel1.Controls
            If e.Index.HierarchicalParentID = indexId Then

                e.ParentIndexData = newValue
            End If
        Next
    End Sub

    ''' <summary>
    ''' Se toman los atributos de cada documento y se crea un
    ''' "DisplayindexCtl" por cada uno y este se agrega
    ''' en el UCIndexViewer...
    ''' </summary>
    ''' <param name="doctypeid"></param>
    ''' <history> Marcelo    Modified    05/02/2009
    '''           Sebastian Modified 14-07-2009 Se agrego funcionalidad para que los permisos en requerido
    '''                                         de la entidad predominen sobre los de indice especifico.       
    ''' </history>
    ''' <remarks></remarks>
    Private Sub GeneracionIndices(ByVal doctypeid As Int64, ByVal IsInserting As Boolean)



        If Disposing = False And isDisposed = False Then
            Dim cur As Cursor = Cursor
            Cursor = Cursors.WaitCursor
            Try
                Dim i As Int32
                Dim IRI As Hashtable = Nothing



                ' Se recorren los atributos del result...
                If LocalResult.Indexs.Count = 0 Then
                    LocalResult.Indexs.AddRange(IndexsBusiness.GetIndexs(LocalResult.ID, LocalResult.DocTypeId))
                    If LocalResult.DocType.Indexs.Count = 0 Then LocalResult.DocType.Indexs = ZCore.FilterIndex(LocalResult.DocType.ID)
                End If

                For i = LocalResult.Indexs.Count - 1 To 0 Step -1

                    ' se toman los atributos asociados al documento...
                    Dim ind As Zamba.Core.Index
                    ind = DirectCast(LocalResult.Indexs(i), Index)

                    Dim ctrl As DisplayindexCtl

                    Dim FlagCoinciden As Boolean = False

                    Try
                        ctrl = New DisplayindexCtl(ind, LocalResult.DocType.IsReindex)

                        RemoveHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                        RemoveHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                        AddHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                        AddHandler ctrl.TabPressed, AddressOf Tab_KeyDown

                        RemoveHandler ctrl.ItemChanged, AddressOf Item_Changed
                        AddHandler ctrl.ItemChanged, AddressOf Item_Changed
                        RemoveHandler ctrl.ClearReferences, AddressOf ClearInstanceReferences
                        AddHandler ctrl.ClearReferences, AddressOf ClearInstanceReferences


                        ctrl.ReloadindexData(ind)

                        'Si el indice tiene padre, se itera por los indices y se agrega la data
                        If ind.HierarchicalParentID > 0 Then
                            ctrl.ParentIndexData = GetLocalIndex(ind.HierarchicalParentID).Data
                        End If
                    Catch ex As Exception
                        If LocalResult.DocType.IsReindex = True OrElse isReIndex = True OrElse IsInserting Then
                            ctrl = New DisplayindexCtl(ind, True)
                        Else
                            ctrl = New DisplayindexCtl(ind, False)
                        End If

                        RemoveHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                        RemoveHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                        AddHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                        AddHandler ctrl.TabPressed, AddressOf Tab_KeyDown


                    End Try

                    RemoveHandler ctrl.DataChanged, AddressOf DataChanged
                    AddHandler ctrl.DataChanged, AddressOf DataChanged
                    ctrl.Dock = DockStyle.Top

                    'Si el atributo es referencial, no debe estar habilitado
                    ctrl.Enabled = Not ind.isReference
                    ' Agrega el control...
                    ' Segun permiso 05-01-2008 [diego]
                    If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.ViewRightsByIndex, doctypeid) Then
                        Dim ShowIndex As Boolean = False

                        IRI = UserBusiness.Rights.GetIndexsRights(doctypeid, Membership.MembershipHelper.CurrentUser.ID, True, True)
                        Dim IR As IndexsRightsInfo = DirectCast(IRI(ctrl.Index.ID), IndexsRightsInfo)

                        Dim dsIndexsPropertys As DataSet = DocTypesBusiness.GetIndexsProperties(doctypeid)

                        If IR.GetIndexRightValue(RightsType.IndexView) Then
                            ShowIndex = True

                            If IR.GetIndexRightValue(RightsType.IndexEdit) = False AndAlso IsInserting = False Then
                                ctrl.Enabled = False
                                ctrl.IsEnabled = False
                            End If
                            '[Sebastian] 14-07-2009

                            For Each Index As DataRow In dsIndexsPropertys.Tables(0).Rows
                                If ctrl.Index.ID = CLng(Index("Index_Id")) AndAlso Index("MustComplete") = 1 _
                                    AndAlso ctrl.lblIndexName.Text.Contains("*") = False Then

                                    ctrl.lblIndexName.Text = ctrl.lblIndexName.Text + " *"
                                    Exit For
                                ElseIf IR.GetIndexRightValue(RightsType.IndexRequired) = True AndAlso ctrl.lblIndexName.Text.Contains("*") = False Then
                                    ctrl.lblIndexName.Text = ctrl.lblIndexName.Text + " *"
                                    Exit For
                                End If
                            Next

                        End If


                        If ShowIndex Then
                            Panel1.Controls.Add(ctrl)
                        Else
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Indice Oculto: " & ctrl.Index.Name)
                        End If
                    Else
                        Panel1.Controls.Add(ctrl)
                    End If

                    If ctrl.Index.Required AndAlso ctrl.lblIndexName.Text.Contains("*") = False Then
                        ctrl.lblIndexName.Text = ctrl.lblIndexName.Text + " *"
                    End If

                    If ctrl.Enabled = True Then
                        ctrl.Enabled = ind.Enabled
                    End If
                Next

                'establesco los tabindex
                Dim tindex As Int32 = Panel1.Controls.Count - 1
                For Each control As Control In Panel1.Controls
                    control.TabIndex = tindex
                    tindex -= 1
                Next


            Catch ex As Exception
                ZClass.raiseerror(ex)
                Panel1.Controls.Clear()
            End Try
            Cursor = cur
        End If
    End Sub

    Private Sub ClearInstanceReferences(ByRef ctrl As DisplayindexCtl)
        RemoveHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
        RemoveHandler ctrl.TabPressed, AddressOf Tab_KeyDown
        RemoveHandler ctrl.ItemChanged, AddressOf Item_Changed
        RemoveHandler ctrl.ItemChanged, AddressOf ItemChangedForAsociated
        RemoveHandler ctrl.ClearReferences, AddressOf ClearInstanceReferences
        RemoveHandler ctrl.DataChanged, AddressOf DataChanged
    End Sub

    Private Sub GeneracionIndices(ByVal doctypeid As Int64, ByVal IndexsIds As List(Of Int64), Optional ByVal IsReadonly As Boolean = True)
        Dim cur As Cursor = Cursor

        Cursor = Cursors.WaitCursor
        Panel1.SuspendLayout()

        Try
            Dim i As Int32

            Dim IndexCtrlList As New List(Of DisplayindexCtl)
            ' Se recorren los atributos del result...
            For i = LocalResult.Indexs.Count - 1 To 0 Step -1

                ' se toman los atributos asociados al documento...
                Dim ind As Zamba.Core.Index = LocalResult.Indexs(i)
                If IndexsIds.IndexOf(ind.ID) <> -1 Then
                    Dim ctrl As DisplayindexCtl
                    Dim FlagCoinciden As Boolean = False
                    Dim Ctrl2 As DisplayindexCtl = Nothing

                    If Not IsReadonly Then LocalResult.DocType.IsReindex = True

                    If LocalResult.DocType.IsReindex = True Or isReIndex = True Then
                        ctrl = New DisplayindexCtl(ind, True)
                    Else
                        ctrl = New DisplayindexCtl(ind, False)
                    End If

                    RemoveHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                    RemoveHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                    AddHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                    AddHandler ctrl.TabPressed, AddressOf Tab_KeyDown

                    'Si el dindice es jerarquico sumarle el evento de cambio al combobox
                    If ind.DropDown = IndexAdditionalType.DropDown OrElse
                        ind.DropDown = IndexAdditionalType.DropDownJerarquico OrElse
                        ind.DropDown = IndexAdditionalType.AutoSustitución OrElse
                        ind.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then

                        RemoveHandler ctrl.ItemChanged, AddressOf ItemChangedForAsociated
                        AddHandler ctrl.ItemChanged, AddressOf ItemChangedForAsociated
                    End If

                    'Se agrega la data del indice padre
                    If ind.HierarchicalParentID > 0 Then
                        ctrl.ParentIndexData = GetLocalIndex(ind.HierarchicalParentID).Data
                    End If

                    RemoveHandler ctrl.DataChanged, AddressOf DataChanged
                    AddHandler ctrl.DataChanged, AddressOf DataChanged

                    ctrl.Dock = DockStyle.Top

                    If ctrl.Enabled = True Then
                        ctrl.Enabled = ind.Enabled
                    End If

                    ' Agrega el control...
                    ' Segun permiso 05-01-2008 [diego]
                    '  Me.Panel1.Controls.Add(ctrl)
                    If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.ViewRightsByIndex, doctypeid) Then
                        Dim IRI As Hashtable = UserBusiness.Rights.GetIndexsRights(doctypeid, Membership.MembershipHelper.CurrentUser.ID, True, True)
                        Dim IR As IndexsRightsInfo = DirectCast(IRI(ctrl.Index.ID), IndexsRightsInfo)

                        If IR.GetIndexRightValue(RightsType.IndexView) = True Then
                            If IR.GetIndexRightValue(RightsType.IndexEdit) = False OrElse ind.Enabled = False Then
                                ctrl.Enabled = False
                            End If
                            If IsReadonly = False AndAlso ind.Enabled = True Then
                                ctrl.Enabled = True
                            End If

                            If IR.GetIndexRightValue(RightsType.IndexRequired) = True AndAlso ctrl.lblIndexName.Text.Contains("*") = False Then
                                ctrl.lblIndexName.Text = ctrl.lblIndexName.Text + " *"
                            Else
                                Dim dsIndexsPropertys As DataSet = DocTypesBusiness.GetIndexsProperties(doctypeid)
                                For Each Index As DataRow In dsIndexsPropertys.Tables(0).Rows
                                    If ctrl.Index.ID = CLng(Index("Index_Id")) AndAlso Index("MustComplete") = 1 _
                                        AndAlso ctrl.lblIndexName.Text.Contains("*") = False Then
                                        ctrl.lblIndexName.Text = ctrl.lblIndexName.Text + " *"
                                        Exit For
                                    End If
                                Next
                            End If

                            IndexCtrlList.Add(ctrl)
                        Else
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Indice Oculto: " & ctrl.Index.Name)
                        End If

                    Else
                        ' no se restringe su uso
                        IndexCtrlList.Add(ctrl)
                    End If
                    If ctrl.Index.Required AndAlso ctrl.lblIndexName.Text.Contains("*") = False Then
                        ctrl.lblIndexName.Text = ctrl.lblIndexName.Text + " *"
                    End If
                End If
            Next

            Panel1.Controls.AddRange(IndexCtrlList.ToArray())
            'establesco los tabindex
            Dim tindex As Int32 = Panel1.Controls.Count - 1
            For Each control As Control In Panel1.Controls
                control.TabIndex = tindex
                tindex -= 1
            Next

            'todo ver el filtro de atributos en exceptions

        Catch ex As System.ComponentModel.Win32Exception
            Cache.CacheBusiness.ClearAllCache()
            Zamba.Core.ZCore.Cleardata()
            ZClass.raiseerror(ex)
        Catch ex As Exception
            Try
                Dim i As Integer = DirectCast(LocalResult.Parent, DocType).Indexs.Count - 1
                For i = DirectCast(LocalResult.Parent, DocType).Indexs.Count - 1 To 0 Step -1
                    Dim ind As Zamba.Core.Index = DirectCast(LocalResult.Parent, DocType).Indexs(i)

                    Dim ctrl As DisplayindexCtl

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
                    ctrl.Dock = DockStyle.Top

                    If ctrl.Enabled = True Then
                        ctrl.Enabled = ind.Enabled
                    End If

                    Panel1.Controls.Add(ctrl)
                Next
                'establesco los tabindex
                Dim tindex As Int32 = Panel1.Controls.Count - 1
                For Each control As Control In Controls
                    control.TabIndex = tindex
                    tindex -= 1
                Next
            Catch exc As ObjectDisposedException
                Zamba.Core.ZClass.raiseerror(ex)
            Catch exc As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try

        Finally
            Panel1.ResumeLayout(True)
            Cursor = cur
        End Try

    End Sub

    'Metodo para cuando no cambia el doctype
    Private Sub ReloadIndexsData(ByVal doctypeid As Int64, ByVal IsInserting As Boolean)

        If Disposing = False And isDisposed = False Then
            Try
                If Not IsNothing(LocalResult) Then
                    If LocalResult.Indexs.Count = Panel1.Controls.Count Then
                        For Each _Index As Index In LocalResult.Indexs
                            For Each ctrl As DisplayindexCtl In Panel1.Controls
                                If ctrl.Index.ID = _Index.ID Then
                                    RemoveHandler ctrl.DataChanged, AddressOf DataChanged
                                    ctrl.ReloadindexData(_Index)
                                Else
                                    If ctrl.Index.HierarchicalParentID = _Index.ID Then
                                        ctrl.ParentIndexData = _Index.Data
                                    End If
                                End If
                            Next
                        Next
                    Else
                        ClearIndexs()
                        GeneracionIndices(doctypeid, IsInserting)
                    End If
                End If
            Catch ex As System.ArgumentOutOfRangeException
                ClearIndexs()
                GeneracionIndices(doctypeid, IsInserting)
            End Try
        End If
    End Sub
#End Region

#Region "Borro los datos de los atributos"
    Public Sub CleanIndexs()
        Try
            For Each c As DisplayindexCtl In Panel1.Controls
                'c.Index.DataTemp = ""
                c.CleanDataTemp()
            Next
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region
#Region "Borro los atributos"
    Public Sub ClearIndexs()
        Try
            If Disposing = False Then
                Panel1.Controls.Clear()
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Guarda los atributos"
    Public Function IsValid() As Boolean
        Dim valid As Boolean = True

        For Each c As DisplayindexCtl In Panel1.Controls
            If Not c.isValid Then
                valid = False
                Exit For
            End If
        Next

        Return valid
    End Function




    Public Function GetIndexs() As List(Of IIndex)
        Dim Indexs As New List(Of IIndex)
        Dim IndexsInvertidos As New List(Of IIndex)

        For Each c As DisplayindexCtl In Panel1.Controls
            IndexsInvertidos.Add(c.Index)
        Next

        Dim i As Integer
        For i = IndexsInvertidos.Count - 1 To 0 Step -1
            Indexs.Add(IndexsInvertidos(i))
        Next

        IndexsInvertidos.Clear()
        IndexsInvertidos = Nothing
        Return Indexs
    End Function

    Public Function GetIndexCount() As Int32
        Return Panel1.Controls.Count()
    End Function

    Public Function GetIndexIds() As ArrayList
        Dim ids As New ArrayList
        Dim idsInvertidos As New ArrayList

        For Each c As DisplayindexCtl In Panel1.Controls
            idsInvertidos.Add(c.Index.ID)
        Next

        Dim i As Integer
        For i = idsInvertidos.Count - 1 To 0 Step -1
            ids.Add(idsInvertidos(i))
        Next

        idsInvertidos.Clear()
        idsInvertidos = Nothing
        Return ids
    End Function


    Public Sub FlagTrue()
        FlagIndexEdited = True
    End Sub



    ''' <summary>
    ''' Guardar los datos de los atributos
    ''' </summary>
    ''' <param name="NoQuestion">Preguntar si se quiere guardar</param>
    ''' <param name="reLoad">Recargar los atributos</param>
    ''' <remarks>
    ''' [Sebastian] 17-06-2009 Modified Se agrego evento para actualizar los atributos 
    ''' del formulario electronico asociado.
    ''' </remarks>
    ''' <history>
    '''  Diego      18/6/2008   Modified
    '''  [Tomas]    03/08/2009  Modified    Se configura un parámetro nuevo del InsertDocument para que no inserte los valores
    '''                                     de búsqueda de palabras ya que lo hace en Results_Business.SaveModifiedIndexData
    '''                                     con todos los atributos recargados (antes no insertaba nada porque no tenía los atributos
    '''                                     recargados e iteraba buscando los atributos para nada).
    ''' [Sebastian] 13/11/2009  Modified    Se comento foco en la barra de atributos porque dificultaba la funcionalidad de desplaza
    '''                                     miento con las flechas de dirección. Se rehizo porque se perdieron los cambios
    ''' [Tomas]     26/01/2010  Modified    SE DESCOMENTA EL FOCUS YA QUE ESTE SIRVE PARA FORZAR AL INDICE QUE PIERDA EL FOCO
    '''                                     Y ASI EJECUTAR EL EVENTO QUE SE ENCARGA DE DETECTAR SI HUBO UNA MODIFICACION.
    ''' </history>
    Private Sub Save(ByVal NoQuestion As Boolean, Optional ByVal reLoad As Boolean = True, Optional ByVal reloadResult As Boolean = True)
        Dim cur As Cursor = Cursor
        Cursor = Cursors.WaitCursor

        Try
            If Not IsNothing(LocalResult) AndAlso LocalResult.DocType.IsReindex Then

                ' [Tomas]   SE DESCOMENTA EL FOCUS YA QUE ESTE SIRVE PARA FORZAR AL INDICE QUE PIERDA EL FOCO
                '           Y ASI EJECUTAR EL EVENTO QUE SE ENCARGA DE DETECTAR SI HUBO UNA MODIFICACION.
                '    [Sebastian 13-11-2009]
                DesignSandBar.Focus()
                EnableControls(False)

                If FlagIndexEdited = True Then
                    Cursor = Cursors.WaitCursor
                    Dim question As String
                    If TypeOf (LocalResult) Is NewResult Then
                        question = "¿Desea guardar el documento pendiente de inserción?"
                    Else
                        question = "¿Desea guardar los cambios realizados en los atributos?"
                    End If
                    If NoQuestion OrElse MessageBox.Show(question, "Zamba Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        If IsValid() Then
                            If Results_Business.ValidateIndexDatabyRights(LocalResult) = False Then
                                MessageBox.Show("Debe Completar los atributos Requeridos", "Edicion de Documentos")
                                Exit Sub
                            End If
                            ' se toman los nuevos atributos de la vista...

                            '[Tomas] 02/10/2009     Se comenta esta iteración ya que no hace nada.
                            '                       Se mueve abajo y se modifica en los casos de updates.
                            '''Diego: Comento esto porque si se estan mostrando atributos especificos y el result tiene todos, no serviria pisar la coleccion
                            '''LocalResult.Indexs = Me.GetIndexs
                            '''Hago esto en cambio
                            'For Each controlI As Index In GetIndexs()
                            '    For Each indice As Index In LocalResult.Indexs
                            '        If controlI.ID = indice.ID Then
                            '            indice.DataTemp = controlI.DataTemp
                            '            Exit For
                            '        End If
                            '    Next
                            'Next

                            If LocalResult.ISVIRTUAL AndAlso LocalResult.ID = 0 Then
                                'Si el documento es nuevo se inserta
                                Dim insertresult As InsertResult

                                Dim RefreshWFAfterInsert As Boolean = True

                                If Not String.IsNullOrEmpty(DontOpenTaskAfterAddToWF) Then
                                    RefreshWFAfterInsert = Not Boolean.Parse(DontOpenTaskAfterAddToWF)
                                End If

                                insertresult = Results_Business.InsertDocument(LocalResult, True, False, False, True, LocalResult.ISVIRTUAL, False, False, RefreshWFAfterInsert)
                                insertresult = Nothing
                            Else
                                'Si el documento ya existía se obtienen los atributos modificados
                                'y se guardan unicamente estos.

                                Dim descripcion As New StringBuilder
                                descripcion.Append("Modificaciones realizadas en '" & LocalResult.Name & "': ")
                                Dim modifiedIndex As Generic.List(Of Int64) = GetModifiedIndexs(descripcion)
                                If modifiedIndex.Count <> 0 Then
                                    ' Se actualizan los nuevos atributos..., si reload = false no actualizo los atributos
                                    Dim rstBuss As New Results_Business()
                                    rstBuss.SaveModifiedIndexData(LocalResult, True, reLoad, modifiedIndex)
                                    rstBuss = Nothing
                                    UserBusiness.Rights.SaveAction(LocalResult.ID, ObjectTypes.Documents, RightsType.ReIndex, descripcion.ToString)
                                    modifiedIndex.Clear()
                                End If
                                modifiedIndex = Nothing
                                descripcion = Nothing
                            End If

                            'Me.Result.HashIndexData = Me.GetIndexs

                            'Dim Indexs As New ArrayList
                            For Each c As DisplayindexCtl In Panel1.Controls
                                c.Commit()
                            Next


                            '[Sebastian] 17-06-2009 Se agrego este evento para actualizar los atributos del
                            'formulario
                            '[Ezequiel] Se cambio el raiseevent por el metodo de la clase zcontrol ya que el mismo invoca el evento.
                            ChangeControl(LocalResult)
                        End If
                    Else
                        FlagIndexEdited = False
                        '          Dim Indexs As New ArrayList
                        For Each c As DisplayindexCtl In Panel1.Controls
                            c.RollBack()
                        Next
                    End If
                    Cursor = Cursors.Arrow
                ElseIf LocalResult.ISVIRTUAL AndAlso LocalResult.ID = 0 Then
                    If MessageBox.Show("¿Desea insertar el documento """ + LocalResult.Name + """?", "Documento a insertar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Cursor = Cursors.WaitCursor
                        Dim insertresult As InsertResult
                        insertresult = Results_Business.InsertDocument(LocalResult, True, False, False, True, LocalResult.ISVIRTUAL, False, False)
                        Cursor = Cursors.Arrow
                    End If
                End If
            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Exit Sub
        Finally
            FlagIndexEdited = False
            If Not IsNothing(LocalResult) Then
                LocalResult.FlagIndexEdited = False
            End If
            EnableControls(True)
            Cursor = cur
        End Try
    End Sub

    Private Sub EnableControls(ByVal enable As Boolean)
        btnAceptar.Enabled = enable
        btnCancelar.Enabled = enable
        BtnDeshacer.Enabled = enable
        BtnGuardar.Enabled = enable
        ButnLimpiarIndices.Enabled = enable
        Application.DoEvents()
    End Sub

    ''' <summary>
    ''' Obtiene los atributos modificados y guarda en un StringBuilder las modificaciones a realizar.
    ''' </summary>
    Private Function GetModifiedIndexs(ByRef descripcion As StringBuilder) As Generic.List(Of Int64)
        Dim modifiedIndex As New Generic.List(Of Int64)
        Dim indexIds As ArrayList = GetIndexIds()
        Dim i, j As Int32


        If Panel1.Controls.Count > 0 Then
            For i = 0 To Panel1.Controls.Count - 1
                If String.Compare(DirectCast(Panel1.Controls(i), DisplayindexCtl).Index.Data, DirectCast(Panel1.Controls(i), DisplayindexCtl).Index.DataTemp) <> 0 Then
                    modifiedIndex.Add(DirectCast(Panel1.Controls(i), DisplayindexCtl).Index.ID)
                    descripcion.Append(_INDEXUPDATE1 & DirectCast(Panel1.Controls(i), DisplayindexCtl).Index.Name & _INDEXUPDATE2 & DirectCast(Panel1.Controls(i), DisplayindexCtl).Index.Data & _INDEXUPDATE3 & DirectCast(Panel1.Controls(i), DisplayindexCtl).Index.DataTemp & _INDEXUPDATE4)
                    'Se comenta ya que insertaba en el historial por atributos 2 veces lo mismo JB
                    'descripcion.Append(_INDEXUPDATE1 & DirectCast(Panel1.Controls(i), DisplayindexCtl).Index.Name & _INDEXUPDATE2 & DirectCast(Panel1.Controls(i), DisplayindexCtl).Index.dataDescription & _INDEXUPDATE3 & DirectCast(Panel1.Controls(i), DisplayindexCtl).Index.dataDescriptionTemp & _INDEXUPDATE4)
                End If
            Next
        Else
            For i = 0 To indexIds.Count - 1
                For j = 0 To LocalResult.Indexs.Count - 1
                    If (indexIds(i) = LocalResult.Indexs(j).ID) Then
                        If String.Compare(LocalResult.Indexs(j).Data, LocalResult.Indexs(i).DataTemp) <> 0 Then
                            modifiedIndex.Add(indexIds(i))
                            descripcion.Append(_INDEXUPDATE1 & LocalResult.Indexs(j).Name & _INDEXUPDATE2 & LocalResult.Indexs(j).Data & _INDEXUPDATE3 & LocalResult.Indexs(j).DataTemp & _INDEXUPDATE4)
                            descripcion.Append(_INDEXUPDATE1 & LocalResult.Indexs(j).Name & _INDEXUPDATE2 & LocalResult.Indexs(j).dataDescription & _INDEXUPDATE3 & LocalResult.Indexs(j).dataDescriptionTemp & _INDEXUPDATE4)
                            Exit For
                        End If
                    End If
                Next
            Next
        End If
        indexIds.Clear()
        indexIds = Nothing
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

    Private Sub Panel5_MouseEnter(ByVal sender As System.Object, ByVal e As EventArgs) Handles Panel1.MouseEnter
        RaiseEvent Panel5MouseEnter()
    End Sub

    Private Sub DesignSandBar_ButtonClick(ByVal sender As System.Object, ByVal e As ToolStripItemClickedEventArgs) Handles DesignSandBar.ItemClicked
        Select Case CStr(e.ClickedItem.Tag)
            Case "DESHACER"
                'ShowIndexs(True)
            Case "GUARDAR"
                Save(True)
            Case "LIMPIAR ATRIBUTOS"
                LimpiarIndices()
        End Select
    End Sub
    'Cuando muevo el scroll saco el foco de los textbox porque sino falla el scroll
    'ya que va siempre al textbox seleccionado
    Private Sub Panel1_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles Panel1.Scroll
        Panel1.Focus()
        ' Me.CloseControl()
    End Sub
    Private Function GetLocalIndex(ByVal indexId As Integer) As IIndex

        Return (From ind In LocalResult.Indexs.OfType(Of IIndex)()
                Where ind.ID = indexId
                Select ind).Single()
    End Function


#Region "ShowDialog"
    Public Property WithDialogPanel() As Boolean
        Get
            Return PnlButtonContainer.Visible
        End Get
        Set(ByVal value As Boolean)
            PnlButtonContainer.Visible = value
        End Set
    End Property

    Public Property ResultId As Long

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
        Try
            If IsValid() Then
                Save(True)
            End If
        Finally
            ParentForm.DialogResult = DialogResult.OK
            ParentForm.Close()
        End Try
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnCancelar.Click


        Dim IndexsIds As List(Of Int64) = IndexsBusiness.GetIndexsIdsByDocTypeId(LocalResult.DocTypeId)
        Try
            Try
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "BtnCancelar Click UCINdexViewer")
                ShowEspecifiedIndexs(LocalResult.ID, LocalResult.DocTypeId, IndexsIds, False)
            Finally
                ParentForm.DialogResult = DialogResult.Cancel
                ParentForm.Close()
                ZClass.HandleEventDialogResult(DialogResult.Cancel)
            End Try
        Catch ex As System.ComponentModel.Win32Exception
            ZClass.raiseerror(ex)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

End Class
