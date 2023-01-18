'Imports zamba.DocTypes.Factory

Imports Zamba.Core
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
    Friend WithEvents DesignSandBar As ZToolBar
    Friend WithEvents BtnDeshacer As ToolStripButton
    Friend WithEvents BtnGuardar As ToolStripButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnAceptar As ZButton
    Friend WithEvents btnCancelar As ZButton
    Friend WithEvents ButnLimpiarIndices As ToolStripButton

    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UCIndexViewerAutocompleteManual))
        Panel1 = New System.Windows.Forms.Panel
        DesignSandBar = New ZToolBar
        BtnDeshacer = New ToolStripButton
        BtnGuardar = New ToolStripButton
        ButnLimpiarIndices = New ToolStripButton
        Panel2 = New System.Windows.Forms.Panel
        btnAceptar = New ZButton
        btnCancelar = New ZButton
        Panel2.SuspendLayout()
        SuspendLayout()
        '
        'Panel1
        '
        Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Panel1.AutoScroll = True
        Panel1.Location = New System.Drawing.Point(0, 24)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(256, 324)
        Panel1.TabIndex = 0

        '
        'DesignSandBar
        '
        DesignSandBar.Items.AddRange(New ToolStripItem() {BtnDeshacer, BtnGuardar, ButnLimpiarIndices})
        DesignSandBar.Location = New System.Drawing.Point(0, 0)
        DesignSandBar.Name = "DesignSandBar"
        DesignSandBar.Size = New System.Drawing.Size(256, 24)
        DesignSandBar.TabIndex = 155
        DesignSandBar.Text = ""
        '
        'BtnDeshacer
        '
        BtnDeshacer.Image = CType(resources.GetObject("BtnDeshacer.Icon"), System.Drawing.Image)
        BtnDeshacer.Tag = "DESHACER"
        BtnDeshacer.ToolTipText = "DESHACER LAS MODIFICACIONES EN LOS ATRIBUTOS"
        '
        'BtnGuardar
        '
        BtnGuardar.Image = CType(resources.GetObject("BtnGuardar.Icon"), System.Drawing.Image)
        BtnGuardar.Tag = "GUARDAR"
        BtnGuardar.ToolTipText = "GUARDAR LAS MODIFICACIONES EN LOS ATRIBUTOS"
        '
        'ButnLimpiarIndices
        '
        ButnLimpiarIndices.Image = CType(resources.GetObject("ButnLimpiarIndices.Icon"), System.Drawing.Image)
        ButnLimpiarIndices.Tag = "LIMPIAR ATRIBUTOS"
        ButnLimpiarIndices.ToolTipText = "LIMPIAR ATRIBUTOS"
        '
        'Panel2
        '
        Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Panel2.Controls.Add(btnAceptar)
        Panel2.Controls.Add(btnCancelar)
        Panel2.Location = New System.Drawing.Point(0, 349)
        Panel2.Name = "Panel2"
        Panel2.Size = New System.Drawing.Size(256, 45)
        Panel2.TabIndex = 156
        Panel2.Visible = False

        '
        'btnAceptar
        '
        btnAceptar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        btnAceptar.Location = New System.Drawing.Point(150, 19)
        btnAceptar.Name = "btnAceptar"
        btnAceptar.Size = New System.Drawing.Size(75, 23)
        btnAceptar.TabIndex = 1
        btnAceptar.Text = "Aceptar"
        btnAceptar.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        btnCancelar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        btnCancelar.Location = New System.Drawing.Point(29, 19)
        btnCancelar.Name = "btnCancelar"
        btnCancelar.Size = New System.Drawing.Size(75, 23)
        btnCancelar.TabIndex = 0
        btnCancelar.Text = "Cancelar"
        btnCancelar.UseVisualStyleBackColor = True
        '
        'UCIndexViewerAutocompleteManual
        '
        BackColor = System.Drawing.Color.White
        BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        CausesValidation = False
        Controls.Add(Panel2)
        Controls.Add(Panel1)
        Controls.Add(DesignSandBar)
        Font = New Font("Tahoma", 9.75!, FontStyle.Bold)
        Location = New System.Drawing.Point(143, 0)
        Name = "UCIndexViewerAutocompleteManual"
        Size = New System.Drawing.Size(256, 394)
        Panel2.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

#Region "Constructores"
    ''' <summary>
    ''' Control que muestra los atributos de un documento
    ''' </summary>
    ''' <param name="offline"></param>
    ''' <param name="btnVisible"></param>
    ''' <param name="showtoolbar"></param>
    ''' <param name="UseHashForCrtlIndex"></param>
    ''' <param name="IsReIndex">Si se desea por defecto que sea reindexado</param>
    ''' <remarks></remarks>
    Public Sub New(Optional ByVal btnVisible As Boolean = True, Optional ByVal showtoolbar As Boolean = True, Optional ByVal IsReIndex As Boolean = False, Optional ByVal SaveOnAccept As Boolean = True)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Instancio el UCIndexViewerAutocompleteManual")
        Me.saveOnAccept = SaveOnAccept
        Me.isReIndex = IsReIndex
        BtnDeshacer.Visible = btnVisible
        BtnGuardar.Visible = btnVisible

        If Not showtoolbar Then
            DesignSandBar.Visible = False
        End If

    End Sub
#End Region

    Private Sub RefreshIndex(ByVal Action As ResultActions, ByRef currentResult As Result)
        If Action = ResultActions.RefreshIndexs Then
            ShowIndexs(currentResult, True)
        End If
    End Sub

#Region "Variables"
    'Private offline As Boolean
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
        FlagIndexEdited = True
        Dim LocalResultAux As IResult = LocalResult
        Try
            Dim newFrmGrilla As New frmGrilla()
            If Not IsNothing(AutocompleteBCBusiness.ExecuteAutoComplete(LocalResult, _Index, newFrmGrilla, True)) Then
                If FlagIsIndexer = False AndAlso LocalResult.ID <> 0 Then
                    Save(True)
                Else
                    If _Index.Data <> _Index.DataTemp Then
                        _Index.SetData(_Index.DataTemp)
                    End If
                End If
                ShowIndexs(LocalResult)
                FlagIndexEdited = True
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            If LocalResult Is Nothing Then LocalResult = LocalResultAux
        End Try
        RaiseEvent IndexsChanged(LocalResult, _Index)
    End Sub
    Private Sub LimpiarIndices()
        Try
            CleanIndexs()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

#Region "Muestra los atributos"

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

        LocalResult = Result
        FlagIndexEdited = False
        LocalResult.FlagIndexEdited = False
        Try

            Result.DocType.RightsLoaded = False
            DocTypesBusiness.GetEditRights(Result.DocType)

            If LocalResult.isShared <> 1 AndAlso LocalResult.DocTypeId <> 0 Then


                If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.ReIndex, LocalResult.DocTypeId) Then

                    If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.OwnerChanges, LocalResult.DocTypeId) AndAlso Membership.MembershipHelper.CurrentUser.ID = LocalResult.OwnerID AndAlso LocalResult.DocType.IsReindex = False Then
                        LocalResult.DocType.IsReindex = True
                    End If
                    If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.OwnerChanges, LocalResult.DocTypeId) AndAlso Membership.MembershipHelper.CurrentUser.ID <> LocalResult.OwnerID AndAlso LocalResult.DocType.IsReindex = True Then
                        If Not UserBusiness.Rights.DisableOwnerChanges(Membership.MembershipHelper.CurrentUser, LocalResult.DocTypeId) Then
                            LocalResult.DocType.IsReindex = False
                        End If

                    End If

                End If

            ElseIf LocalResult.isShared = 1 Then
                If LocalResult.DocTypeId <> 0 Then
                    LocalResult.DocType.IsReindex = False
                End If
            End If


            If LastShowedDocId = LocalResult.DocType.ID AndAlso LastIsReIndex = LocalResult.DocType.IsReindex Then
                ReloadIndexsData(Result.DocTypeId)
            Else
                ClearIndexs()
                GeneracionIndices(Result.DocTypeId)
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            LastShowedDocId = LocalResult.DocType.ID
            LastIsReIndex = LocalResult.DocType.IsReindex
            '    Me.Panel1.AutoScroll = False
            '    Me.Panel1.AutoScroll = True
        End Try
        'Me.FlagFirstTime = False
    End Sub

    Public Sub ShowEspecifiedIndexs(ByRef Result As Zamba.Core.Result, ByVal IndexsIds As ArrayList, Optional ByVal IfSameResultDontReload As Boolean = False, Optional ByVal IsReadonly As Boolean = True)

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
        End Try
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
    Private Sub GeneracionIndices(ByVal doctypeid As Int64)
        Dim cur As Cursor = Cursor
        Cursor = Cursors.WaitCursor

        Try
            Dim i As Int32
            Dim IRI As Hashtable = Nothing

            Panel1.SuspendLayout()

            ' Se recorren los atributos del result...
            If LocalResult.Indexs.Count = 0 Then
                LocalResult.Indexs.AddRange(IndexsBusiness.GetIndexs(LocalResult.ID, LocalResult.DocTypeId))
                If LocalResult.DocType.Indexs.Count = 0 Then LocalResult.DocType.Indexs = ZCore.FilterIndex(LocalResult.DocType.ID)
            End If

            For i = LocalResult.Indexs.Count - 1 To 0 Step -1

                ' se toman los atributos asociados al documento...
                Dim ind As Zamba.Core.Index
                ind = DirectCast(LocalResult.Indexs(i), Zamba.Core.Index)

                Dim ctrl As DisplayindexCtl

                Dim FlagCoinciden As Boolean = False


                Try
                    ctrl = New DisplayindexCtl(ind, LocalResult.DocType.IsReindex)

                    RemoveHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                    RemoveHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                    AddHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                    AddHandler ctrl.TabPressed, AddressOf Tab_KeyDown

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
                End Try

                ctrl.Dock = DockStyle.Top

                'Si el atributo es referencial, no debe estar habilitado
                ctrl.Enabled = Not ind.isReference

                ' Agrega el control...
                ' Segun permiso 05-01-2008 [diego]
                If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.ViewRightsByIndex, doctypeid) Then
                    Dim ShowIndex As Boolean = False
                    'Si no se cargaron los atributos antes los cargo
                    If IsNothing(IRI) Then
                        IRI = UserBusiness.Rights.GetIndexsRights(doctypeid, Membership.MembershipHelper.CurrentUser.ID, True, True)
                    End If
                    Dim IR As IndexsRightsInfo = DirectCast(IRI(ctrl.Index.ID), IndexsRightsInfo)
                    Dim dsIndexsPropertys As DataSet = DocTypesBusiness.GetIndexsProperties(doctypeid)

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
                            Exit For

                        End If
                    Next
                    If IR.GetIndexRightValue(RightsType.IndexView) Then ShowIndex = True
                    If IR.GetIndexRightValue(RightsType.IndexEdit) = False Then ctrl.Enabled = False
                    If ctrl.Index.Required AndAlso ctrl.Controls(2).Text.Contains("*") = False Then
                        ctrl.Controls(2).Text = ctrl.Controls(2).Text + " *"
                    End If

                    If ShowIndex Then Panel1.Controls.Add(ctrl)
                Else
                    ' no se restringe su uso
                    Panel1.Controls.Add(ctrl)
                End If

                If ctrl.Enabled = True Then
                    ctrl.Enabled = ind.Enabled
                End If
            Next
            Panel1.ResumeLayout()
            'establesco los tabindex
            Dim tindex As Int32 = Panel1.Controls.Count - 1
            For Each control As Control In Panel1.Controls
                control.TabIndex = tindex
                tindex -= 1
            Next

        Catch ex As Exception
            Try
                Dim i As Integer = DirectCast(LocalResult.Parent, DocType).Indexs.Count - 1
                Panel1.SuspendLayout()
                For i = DirectCast(LocalResult.Parent, DocType).Indexs.Count - 1 To 0 Step -1
                    Dim ind As Zamba.Core.Index = DirectCast(LocalResult.Parent, DocType).Indexs(i)
                    Dim ctrl As DisplayindexCtl


                    If LocalResult.DocType.IsReindex = True Or isReIndex = True Then
                        ctrl = New DisplayindexCtl(ind, True)
                    Else
                        ctrl = New DisplayindexCtl(ind, False)
                    End If
                    RemoveHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                    RemoveHandler ctrl.TabPressed, AddressOf Tab_KeyDown
                    AddHandler ctrl.EnterPressed, AddressOf Enter_KeyDown
                    AddHandler ctrl.TabPressed, AddressOf Tab_KeyDown


                    ctrl.Dock = DockStyle.Top
                    ' Agrega el control...
                    ' Segun permiso 05-01-2008 [diego]
                    If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.ViewRightsByIndex, doctypeid) Then
                        Dim IRI As Hashtable = UserBusiness.Rights.GetIndexsRights(doctypeid, Membership.MembershipHelper.CurrentUser.ID, True, True)
                        If DirectCast(IRI(ctrl.Index.ID), IndexsRightsInfo).GetIndexRightValue(RightsType.IndexView) = True Then
                            For Each indexid As Int64 In IRI.Keys
                                If indexid = ctrl.Index.ID Then
                                    If DirectCast(IRI(ctrl.Index.ID), IndexsRightsInfo).GetIndexRightValue(RightsType.IndexEdit) = False Then
                                        ctrl.Enabled = False
                                        Exit For
                                    End If
                                End If
                            Next
                            Panel1.Controls.Add(ctrl)
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
                            Panel1.Controls.Add(ctrl)
                        End If

                    Else
                        ' no se restringe su uso
                        Panel1.Controls.Add(ctrl)
                    End If


                    If ctrl.Enabled = True Then
                        ctrl.Enabled = ind.Enabled
                    End If


                Next
                Panel1.ResumeLayout()
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
        End Try

        Cursor = cur
    End Sub

    Private Sub GeneracionIndices(ByVal doctypeid As Int64, ByVal IndexsIds As ArrayList, Optional ByVal IsReadonly As Boolean = True)
        Dim cur As Cursor = Cursor
        Cursor = Cursors.WaitCursor

        Try
            Dim i As Int32
            Panel1.SuspendLayout()

            ' Se recorren los atributos del result...
            For i = LocalResult.Indexs.Count - 1 To 0 Step -1

                ' se toman los atributos asociados al documento...
                Dim ind As Zamba.Core.Index = LocalResult.Indexs(i)
                If IndexsIds.IndexOf(ind.ID.ToString) <> -1 Then
                    Dim ctrl As DisplayindexCtl
                    Dim FlagCoinciden As Boolean = False

                    Dim Ctrl2 As DisplayindexCtl = Nothing

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

                    ' Agrega el control...
                    ' Segun permiso 05-01-2008 [diego]
                    Panel1.Controls.Add(ctrl)
                    If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.ViewRightsByIndex, doctypeid) Then
                        Dim IRI As Hashtable = UserBusiness.Rights.GetIndexsRights(doctypeid, Membership.MembershipHelper.CurrentUser.ID, True, True)
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
                            Panel1.Controls.Add(ctrl)
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
                            Panel1.Controls.Add(ctrl)
                        End If
                    Else
                        ' no se restringe su uso
                        Panel1.Controls.Add(ctrl)
                    End If
                End If
            Next
            Panel1.ResumeLayout()
            'establesco los tabindex
            Dim tindex As Int32 = Panel1.Controls.Count - 1
            For Each control As Control In Panel1.Controls
                control.TabIndex = tindex
                tindex -= 1
            Next

            'todo ver el filtro de atributos en exceptions

        Catch ex As Exception

            Try
                Dim i As Integer = DirectCast(LocalResult.Parent, DocType).Indexs.Count - 1
                Panel1.SuspendLayout()
                For i = DirectCast(LocalResult.Parent, DocType).Indexs.Count - 1 To 0 Step -1
                    Dim ind As Zamba.Core.Index = DirectCast(LocalResult.Parent, DocType).Indexs(i)
                    'Fijarse si tengo el atributo ya cargado y no instanciar uno nuevo
                    'Cache de atributos
                    Dim ctrl As DisplayindexCtl

                    Dim FlagCoinciden As Boolean = False
                    Dim Ctrl2 As DisplayindexCtl


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

                    ctrl.Dock = DockStyle.Top

                    If ctrl.Enabled = True Then
                        ctrl.Enabled = ind.Enabled
                    End If

                    Panel1.Controls.Add(ctrl)
                Next
                Panel1.ResumeLayout()
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
        End Try

        Cursor = cur
    End Sub

    'Metodo para cuando no cambia el doctype
    Public Sub ReloadIndexsData(ByVal doctypeid As Int64)
        Try
            'Dim i As Integer
            If LocalResult.Indexs.Count = Panel1.Controls.Count Then
                For Each _Index As Index In LocalResult.Indexs
                    For Each ctrl As DisplayindexCtl In Panel1.Controls
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
                ClearIndexs()
                GeneracionIndices(doctypeid)
            End If
        Catch ex As System.ArgumentOutOfRangeException
            ClearIndexs()
            GeneracionIndices(doctypeid)
        End Try
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
            Panel1.Controls.Clear()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Guarda los atributos"
    Private Function IsValid() As Boolean
        '  Dim Indexs As New ArrayList
        For Each c As DisplayindexCtl In Panel1.Controls
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

        For Each c As DisplayindexCtl In Panel1.Controls
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
    ''' <history>Diego 18/6/2008 Modified
    '''  [Tomas]    03/08/2009  Modified    Se configura un parámetro nuevo del InsertDocument para que no inserte los valores
    '''                                     de búsqueda de palabras ya que lo hace en Results_Business.SaveModifiedIndexData
    '''                                     con todos los atributos recargados (antes no insertaba nada porque no tenía los atributos
    '''                                     recargados e iteraba buscando los atributos para nada).
    ''' [Sebastian] 13/11/2009 Modified     Se comento foco en la barra de atributos porque dificultaba la funcionalidad de desplaza
    '''                                     miento con las flechas de dirección. Se rehizo porque se perdieron los cambios
    ''' </history>
    Public Sub Save(ByVal NoQuestion As Boolean, Optional ByVal reLoad As Boolean = True, Optional ByVal reloadResult As Boolean = True)
        Dim RB As New Results_Business
        Try
            If Not IsNothing(LocalResult) AndAlso LocalResult.DocType.IsReindex Then
                '    [Sebastian 13-11-2009]
                'Me.DesignSandBar.Focus()
                If FlagIndexEdited = True Then
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
                                End If
                                modifiedIndex = Nothing
                                descripcion = Nothing
                            End If

                            'Me.Result.HashIndexData = Me.GetIndexs

                            'Dim Indexs As New ArrayList
                            For Each c As DisplayindexCtl In Panel1.Controls
                                c.Commit()
                            Next
                            Try
                                RB.UpdateAutoName(LocalResult)
                            Catch ex As Exception
                                Zamba.Core.ZClass.raiseerror(ex)
                            End Try
                            RaiseEvent IndexsSaved(LocalResult)
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
            If Not IsNothing(LocalResult) Then
                LocalResult.FlagIndexEdited = False
            End If
            RB = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Obtiene los atributos modificados
    ''' </summary>
    Private Function GetModifiedIndexs(ByRef descripcion As StringBuilder) As Generic.List(Of Int64)
        Dim modifiedIndex As New Generic.List(Of Int64)
        For Each controlI As Index In GetIndexs()
            For Each indice As Index In LocalResult.Indexs
                If (controlI.ID = indice.ID) AndAlso (String.Compare(indice.Data, indice.DataTemp) <> 0) Then
                    modifiedIndex.Add(indice.ID)
                    descripcion.Append("Atributo '" & indice.Name & "' de '" & indice.Data & "' a '" & indice.DataTemp & "', ")
                    descripcion.Append("Atributo '" & indice.Name & "' de '" & indice.dataDescription & "' a '" & indice.dataDescriptionTemp & "', ")
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

    Private Sub Panel5_MouseEnter(ByVal sender As System.Object, ByVal e As EventArgs) Handles Panel1.MouseEnter
        RaiseEvent Panel5MouseEnter()
    End Sub

    Private Sub DesignSandBar_ButtonClick(ByVal sender As System.Object, ByVal e As ToolStripItemClickedEventArgs) Handles DesignSandBar.ItemClicked
        Select Case CStr(e.ClickedItem.Tag)
            Case "DESHACER"
                ShowIndexs(LocalResult, False)
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
    End Sub


#Region "Metodos Publicos"

    'Private Sub CancelIndexSaved()
    '    Me.ShowEspecifiedIndexs(LocalResult, LocalIndexs, False)
    'End Sub
    Public Sub ShowPanelWithAceptandCancel(Optional ByVal Show As Boolean = True)
        Panel2.Visible = True
    End Sub
#End Region


    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click
        If saveOnAccept Then
            Save(True)
        End If

        'lanza el evento con el dialog result simulando el de un formulario
        ZClass.HandleEventDialogResult(DialogResult.OK)
        ParentForm.Close()
    End Sub

    ''' <summary>
    '''     cancela la ejecución de la regla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnCancelar.Click
        'lanza el evento con el dialog result simulando el de un formulario
        ZClass.HandleEventDialogResult(DialogResult.Cancel)
        ParentForm.Close()
    End Sub
End Class
