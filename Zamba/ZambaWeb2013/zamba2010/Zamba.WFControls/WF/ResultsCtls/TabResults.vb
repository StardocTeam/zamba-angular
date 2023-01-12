Imports Zamba.Core
Imports Zamba.Viewers
Imports Zamba.AdminControls
Imports System.IO
Imports Zamba.Core.Search
Imports Zamba.Core.DocTypes.DocAsociated
Imports Zamba.Core.WF.WF
Imports System.Text

Namespace WF.ResultsCtls

    ''' <summary>
    ''' Result tab
    ''' </summary>
    ''' <History>Marcelo Created 16/12/10</History>
    ''' <remarks></remarks>
    Public Class TabResults
        Inherits System.Windows.Forms.Control
        Implements IViewerContainer
        Implements IMenuContextContainer

        Friend WithEvents SplitContainer3ResultsHorizontal As System.Windows.Forms.SplitContainer
        Public UCFusion2 As UCFusion2
        Public WithEvents TabViewers As System.Windows.Forms.TabControl
        Friend WithEvents SplitTasks As System.Windows.Forms.SplitContainer
        Friend WithEvents TabSecondaryTask As System.Windows.Forms.TabControl
        Friend WithEvents TabForo As System.Windows.Forms.TabPage
        Friend WithEvents TabDocAsociated As System.Windows.Forms.TabPage
        Friend WithEvents TabHistorialEmails As System.Windows.Forms.TabPage

        Private WithEvents extVis As ExternalVisualizer

        Private _selectedresult As Result
        Private ResultSelectedFromGrid As Boolean
        Private tabgrid As TabPage
        Private index As Int16
        Public UcForo As UcForo
        Private ucHistorialEmails As ucHistorialEmails
        Private CantPdfs As Int32
        Private splitterMoved As Boolean
        Friend WithEvents toolbar As UCToolbarResult
        Private CurrentUserId As Int64
        Public ReadOnly Property controller As Controller
        Public currentContextMenu As UCGridContextMenu

        Private _dontOpenTaskAfterAddToWF As String = String.Empty

        Public Property DontOpenTaskAfterAddToWF() As String
            Get
                Return _dontOpenTaskAfterAddToWF
            End Get
            Set(ByVal value As String)
                _dontOpenTaskAfterAddToWF = value
            End Set
        End Property

        ''' <summary>
        ''' Results tab
        ''' </summary>
        ''' <param name="_CurrentUserId">Id of the logged user</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal _CurrentUserId As Int64, Controller As Controller)
            ' This call is required by the Windows Form Designer.
            InitializeComponent()
            ' Add any initialization after the InitializeComponent() call.
            CurrentUserId = _CurrentUserId
            Me.controller = Controller
            currentContextMenu = New UCGridContextMenu(Me)

        End Sub

        Private Sub RefreshGrid(ByRef result As Result)
            UCFusion2.UpdateResult(result)
        End Sub

        Private Sub RefreshGrid()
            'Limpia la grilla.
            UCFusion2.FillResults(Nothing, Nothing)
            UCFusion2.ResetGrid()
            'Vuelve a la primer pagina.
            UCFusion2.LastPage = 0
            'Obtiene los resultados con la consulta con su conteo
            Dim dt As DataTable = ModDocuments.SearchRows(LastSearch.SQL(0), LastSearch.SQLCount(0), 0)
            'Llena la grilla con los resultados obtenidos
            UCFusion2.FillResults(dt, LastSearch)

        End Sub

        Private Sub LoadGridResults(ByVal CurrentUserID As Int64)

            If IsNothing(toolbar) OrElse toolbar.IsDisposed Then
                toolbar = New UCToolbarResult()
                toolbar.Dock = DockStyle.Top
                toolbar.AllowItemReorder = True
                toolbar.CanOverflow = True
                toolbar.ShowItemToolTips = True
                'Se cargan los botones dinámicos
                GenericRuleManager.LoadDynamicButtons(DirectCast(toolbar, ZToolBar), 0, True, ButtonPlace.GrillaResultados, 0)
                Controls.Add(toolbar)
            End If

            If IsNothing(UCFusion2) OrElse UCFusion2.IsDisposed Then
                UCFusion2 = New UCFusion2(UCFusion2.Modes.Results, CurrentUserID, Nothing, Me)
                UCFusion2.Dock = DockStyle.Fill
                UCFusion2.Text = "Árbol de Resultados"
                SplitContainer3ResultsHorizontal.Panel1.Controls.Add(UCFusion2)
                HideGrid()
                AddUCFusionEventHandlers()
            End If

            If Not IsNothing(tabgrid) AndAlso Not IsNothing(TabViewers) Then
                tabgrid.Name = "Resultados"
                If TabViewers.TabPages.Contains(tabgrid) Then
                    TabViewers.SelectTab(tabgrid)
                End If
            End If
        End Sub

        '  Public Event currentContextMenuItemClicked(Action As String, listResults As List(Of IResult), ContextMenuContainer As IMenuContextContainer)
        Private Sub currentContextMenuClick(Action As String, listResults As List(Of IResult), ContextMenuContainer As IMenuContextContainer) Implements IMenuContextContainer.currentContextMenuClick
            controller.currentContextMenuClick(Action, listResults, ContextMenuContainer)
        End Sub

        Private Sub AddUCFusionEventHandlers()
            RemoveHandler UCFusion2.ResultDoubleClick, AddressOf ShowProperResult
            RemoveHandler UCFusion2.CambiarNombre, AddressOf CambiarNombreResult
            RemoveHandler UCFusion2.ExportarAExcel, AddressOf ExportarAExcelResult
            RemoveHandler UCFusion2.GenerarListado, AddressOf GenerarListadoResult
            RemoveHandler SplitContainer3ResultsHorizontal.SplitterMoved, AddressOf SaveGridSplitterPosition
            RemoveHandler SplitContainer3ResultsHorizontal.SplitterMoving, AddressOf SaveSplitterMovementsFlag
            RemoveHandler UCFusion2._RefreshGrid, AddressOf RefreshGrid
            RemoveHandler UCFusion2.CloseResult, AddressOf CloseDocumentViewerTab
            RemoveHandler UCFusion2.ResultSelected, AddressOf ResultSelected

            AddHandler UCFusion2.CloseResult, AddressOf CloseDocumentViewerTab
            AddHandler SplitContainer3ResultsHorizontal.SplitterMoving, AddressOf SaveSplitterMovementsFlag
            AddHandler SplitContainer3ResultsHorizontal.SplitterMoved, AddressOf SaveGridSplitterPosition
            AddHandler UCFusion2.GenerarListado, AddressOf GenerarListadoResult
            AddHandler UCFusion2.ExportarAExcel, AddressOf ExportarAExcelResult
            AddHandler UCFusion2.CambiarNombre, AddressOf CambiarNombreResult
            AddHandler UCFusion2._RefreshGrid, AddressOf RefreshGrid
            AddHandler UCFusion2.ResultDoubleClick, AddressOf ShowProperResult
            AddHandler UCFusion2.ResultSelected, AddressOf ResultSelected

        End Sub



        Public Shared Event sendMail(ByVal results As Generic.List(Of IResult))
        Public Shared Event AgregarACarpeta(ByRef result As IResult)
        Public Event ShowTask(ByVal TaskId As Int64, ByVal stepId As Int64, ByVal docTypeId As Int64)
        Public Event TabInsertFormClosed(ByVal tempId As Integer)

        Public Shared Event ExportarAExcel(ByRef Result As Result)
        Private Sub ExportarAExcelResult(ByRef result As Result)
            RaiseEvent ExportarAExcel(result)
        End Sub

        Public Shared Event ExportaPdf(ByRef Result As Result)
        Private Sub ExportaPdfResult(ByRef result As Result)
            RaiseEvent ExportaPdf(result)
        End Sub

        Public Shared Event GenerarListado(ByVal Result As Result)
        Private Sub GenerarListadoResult(ByVal result As Result)
            RaiseEvent GenerarListado(result)
        End Sub


        Public Shared Event CambiarNombre(ByRef Result As Result)
        Private Sub CambiarNombreResult(ByRef Result As Result)
            RaiseEvent CambiarNombre(Result)
        End Sub

        Public Sub EliminarBusqAnt()
            Try
                LoadGridResults(CurrentUserId)
                UCFusion2.ClearSearchs()
                MessageBox.Show("Búsquedas Eliminadas")
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

#Region "FillSearchs"

        Dim LastSearch As ISearch

        Sub ShowFilesInFolder(ByVal ResultsList As DataTable, ByVal SearchName As String, ByVal Search As ISearch)
            Try
                If Search IsNot Nothing Then LastSearch = Search

                FillListView(ResultsList, SearchName, Search)

                If ResultsList.Rows.Count = 1 Then
                    UCFusion2.SelectDataRowResult(ResultsList.Rows)

                End If

                'Carga las preferencias visuales del usuario
                'LoadVisualPreferences()

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Private Sub LoadVisualPreferences()

            SplitContainer3ResultsHorizontal.SplitterDistance = UserPreferences.getValue("DocPanelHeight", UPSections.UserPreferences, CInt(SplitContainer3ResultsHorizontal.Height * 0.5))

            ChangeGridLocation(True)

        End Sub

        Private Sub ClearSearchs(ByVal o As Object, ByVal e As EventArgs)
            LoadGridResults(CurrentUserId)

            Try
                UCFusion2.ClearSearchs()
                ClearDocuments()
                UCFusion2.ClearSearchs()
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

        End Sub
#End Region

#Region "Show"

        Private Sub NewDocumentViewer(ByRef Result As Result, Optional ByVal ShowOriginal As Boolean = False)

            SplitContainer3ResultsHorizontal.Panel2Collapsed = False

            Dim IsOpened As Boolean = False
            Dim UcViewer As UCDocumentViewer2
            Dim zvc As UCDocumentViewer2

            For Each dc As TabPage In TabViewers.TabPages
                If TypeOf dc Is UCDocumentViewer2 Then
                    '[Sebastian] 09-06-2009 se agrego cast para salvar warning
                    zvc = DirectCast(dc, UCDocumentViewer2)
                    If (Not TypeOf Result Is NewResult AndAlso zvc.Result.ID = Result.ID) OrElse (TypeOf Result Is NewResult AndAlso zvc.Result.TempId = Result.TempId) Then
                        IsOpened = True
                        Exit For
                    End If
                End If
            Next

            If Not IsOpened Then
                LoadGridResults(CurrentUserId)
                'Dim UcViewer As UCDocumentViewer2
                UcViewer = New UCDocumentViewer2(Me, Result, False, False, True, False, True, False, ShowOriginal)
                If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.AddNewVersions, Result.DocTypeId) Then
                    If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.AutomaticVersion, Result.DocTypeId) Then
                        RemoveHandler UcViewer.eAutomaticNewVersion, AddressOf AddNewVersion
                        AddHandler UcViewer.eAutomaticNewVersion, AddressOf AddNewVersion
                    Else
                        RemoveHandler UcViewer.eAutomaticNewVersion, AddressOf AddNewVersion
                    End If
                Else
                    RemoveHandler UcViewer.eAutomaticNewVersion, AddressOf AddNewVersion
                End If

                UcViewer.Dock = DockStyle.Fill
                FillContentText(UcViewer)
                TabViewers.TabPages.Add(UcViewer)
                TabViewers.SelectTab(UcViewer)

                If ShowOriginal Then
                    UcViewer.VerOriginalButtonVisible = False
                    UcViewer.ShowDocument(False, False, False, False, True)
                Else
                    UcViewer.ShowDocument(Result.ISVIRTUAL, False, False, False, True)
                End If

                RemoveHandler UcViewer.LinkSelected, AddressOf UCFusion2.SelectResult
                RemoveHandler UcViewer.EventZoomLock, AddressOf EventZoomLock
                RemoveHandler UcViewer.Movido, AddressOf Movido
                RemoveHandler UcViewer.CambiarDock, AddressOf CambiarDock
                RemoveHandler UcViewer.ShowAsociatedResult, AddressOf ShowAsociatedResult
                RemoveHandler UcViewer.ShowOriginal, AddressOf Me.ShowOriginal
                RemoveHandler UcViewer.ReplaceDocument, AddressOf ReplaceDocument
                RemoveHandler UcViewer.ShowAssociatedWFbyDocId, AddressOf ShowTaskResult
                RemoveHandler UcViewer.ClearReferences, AddressOf ClearInstanceReferences
                AddHandler UcViewer.LinkSelected, AddressOf UCFusion2.SelectResult
                AddHandler UcViewer.EventZoomLock, AddressOf EventZoomLock
                AddHandler UcViewer.Movido, AddressOf Movido
                AddHandler UcViewer.CambiarDock, AddressOf CambiarDock
                AddHandler UcViewer.ReplaceDocument, AddressOf ReplaceDocument
                AddHandler UcViewer.ShowAsociatedResult, AddressOf ShowAsociatedResult
                AddHandler UcViewer.ShowOriginal, AddressOf Me.ShowOriginal
                AddHandler UcViewer.ShowAssociatedWFbyDocId, AddressOf ShowTaskResult
                AddHandler UcViewer.ClearReferences, AddressOf ClearInstanceReferences
            Else
                If Not TypeOf Result Is NewResult Then
                    If ShowOriginal Then
                        TabViewers.SelectTab(zvc)
                        zvc.VerOriginalButtonVisible = False
                        zvc.ShowDocument(False, False, False, False, True)
                    Else
                        TabViewers.SelectTab(zvc)
                        zvc.ShowDocument(True, False, False, False, True)
                    End If
                End If
            End If

        End Sub
        Private Sub ClearInstanceReferences(ByRef ucviewer As UCDocumentViewer2)
            Try
                If ucviewer IsNot Nothing Then
                    RemoveHandler ucviewer.eAutomaticNewVersion, AddressOf AddNewVersion
                    If Not IsNothing(UCFusion2) Then
                        RemoveHandler ucviewer.LinkSelected, AddressOf UCFusion2.SelectResult
                    End If
                    RemoveHandler ucviewer.EventZoomLock, AddressOf EventZoomLock
                    RemoveHandler ucviewer.Movido, AddressOf Movido
                    RemoveHandler ucviewer.CambiarDock, AddressOf CambiarDock
                    RemoveHandler ucviewer.ShowAsociatedResult, AddressOf ShowAsociatedResult
                    RemoveHandler ucviewer.ShowOriginal, AddressOf ShowOriginal
                    RemoveHandler ucviewer.ReplaceDocument, AddressOf ReplaceDocument
                    RemoveHandler ucviewer.ShowAssociatedWFbyDocId, AddressOf ShowTaskResult
                    RemoveHandler ucviewer.ClearReferences, AddressOf ClearInstanceReferences
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Reemplaza un archivo y refresca el documento
        ''' </summary>
        ''' <param name="result"></param>
        ''' <remarks></remarks>
        ''' <history>
        '''     (Pablo)	    11/01/2011	Created
        ''' 
        Dim RB As New Results_Business

        Private Sub ReplaceDocument(ByRef Result As Result)
            Try
                Dim Dialog As New System.Windows.Forms.OpenFileDialog
                Dialog.CheckFileExists = True
                Dialog.CheckPathExists = True
                Dialog.Multiselect = False
                Dialog.Title = "Reemplazo de Documentos"
                Dim DialogResult As DialogResult = Dialog.ShowDialog()
                If DialogResult = DialogResult.OK OrElse DialogResult = DialogResult.Yes Then
                    Result.File = Dialog.FileName
                    RB.ReplaceDocument(Result, Dialog.FileName, False)
                Else
                    Exit Sub
                End If
                UserBusiness.Rights.SaveAction(Result.ID, ObjectTypes.Documents, RightsType.Edit, Result.Name)

            Catch ex As IOException
                Zamba.Core.ZClass.raiseerror(ex)
                MessageBox.Show("Ocurrio un error al reemplazar el documento actual: " & ex.ToString, "Error de Reemplazo", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try

            RB = Nothing
            CloseDocumentViewerTab(Result)
            UCFusion2.UpdateResult(Result)
            ShowResult(Result)
        End Sub

        Private Sub ShowOfficeToolbars()
            For Each x As TabPage In TabViewers.TabPages
                If TypeOf (x) Is UCDocumentViewer2 Then
                    DirectCast(x, UCDocumentViewer2).ShowToolbars()
                End If
            Next
        End Sub



        ''' <summary>
        ''' [Sebastian] 03-07-09 MODIFIED se modifico para al momento de cerrar en pantalla completa 
        ''' no lance exception
        ''' </summary>
        ''' <param name="Sender"></param>
        ''' <param name="ClosedFromCross"></param>
        ''' <remarks></remarks>
        Public Sub CambiarDock(ByVal Sender As TabPage, Optional ByVal ClosedFromCross As Boolean = False, Optional ByVal IsMaximize As Boolean = False)
            Try
                If ClosedFromCross = False Then
                    If Not IsNothing(extVis) Then
                        If Sender Is Nothing Then
                            extVis.Close()
                            AdjustFullScreenTasks(False)
                        ElseIf extVis.Controls.Contains(Sender.Parent) Then
                            extVis.Close()
                            AdjustFullScreenTasks(False)
                        ElseIf IsNothing(Sender.Parent) = True Then
                            extVis.Close()
                            AdjustFullScreenTasks(False)
                        Else
                            If extVis Is Nothing Then
                                extVis = New ExternalVisualizer(DirectCast(Sender.Parent, TabControl))
                            End If
                            extVis.Show()
                            AdjustFullScreenTasks(True)
                        End If
                    Else
                        If Not IsNothing(Sender) Then
                            If extVis Is Nothing Then
                                extVis = New ExternalVisualizer(DirectCast(Sender.Parent, TabControl))
                            End If
                            extVis.Show()
                            AdjustFullScreenTasks(True)
                        End If
                    End If
                Else
                    extVis = New ExternalVisualizer(DirectCast(Sender.Parent, TabControl))
                    extVis.Show()
                    AdjustFullScreenTasks(True)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Maximiza/Minimiza todas las tareas
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub AdjustFullScreenTasks(ByVal isMaximize As Boolean)
            Try
                For Each dc As TabPage In TabViewers.TabPages
                    If TypeOf dc Is UCDocumentViewer2 Then
                        Dim zvc As UCDocumentViewer2 = DirectCast(dc, UCDocumentViewer2)
                        zvc.IsMaximize = isMaximize
                    End If
                Next
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Método que sirve para visualizar el documento
        ''' </summary>
        ''' <param name="Result">Instancia de un documento</param>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Gaston]	20/04/2009	Modified    Funcionalidad para formularios dinámicos
        ''' </history>
        Public Sub ShowResult(ByRef Result As Result)
            Try
                Try
                    If Not IsNothing(Result) AndAlso (Result.ID <> 0) Then
                        Result = Results_Business.GetResult(Result.ID, Result.DocTypeId)
                        ResultSelectedFromGrid = True
                        ResultSelected(Result)
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try

                ' Si el documento a mostrar ya se encuentra abierto se muestra...
                For Each dc As TabPage In TabViewers.TabPages
                    If TypeOf dc Is UCDocumentViewer2 Then
                        Dim zvc As UCDocumentViewer2 = DirectCast(dc, UCDocumentViewer2)
                        If (Not TypeOf Result Is NewResult AndAlso zvc.Result.ID = Result.ID) OrElse (TypeOf Result Is NewResult AndAlso zvc.Result.TempId = Result.TempId) Then
                            TabViewers.SelectTab(zvc)
                            'Exit Sub
                        End If
                    End If
                Next

                If Not IsNothing(Result.FullPath) AndAlso Not IsNothing(Result.Doc_File) AndAlso HasExension(Result.Doc_File) Then

                    NewDocumentViewer(Result)

                Else

                    If (Results_Business.HasFormsExtended(Result)) Then
                        NewDocumentViewer(Result)
                    Else
                        If Not String.IsNullOrEmpty(Result.AutoName) Then
                            Dim dynamicFormId As String = Result.AutoName.Substring(Result.AutoName.LastIndexOf("Id=") + 3).ToString()

                            If (IsNumeric(dynamicFormId)) Then

                                ' Si el formulario es un formulario dinámico y se encuentra en la correspondiente base de datos
                                If (FormBusiness.isDynamicForm(Int32.Parse(dynamicFormId), Result.DocTypeId) = True) Then
                                    ' Se visualiza el formulario dinámico
                                    NewDocumentViewer(Result)
                                Else
                                    MessageBox.Show("El Documento aun no esta ingresado en el sistema ", "Zamba")
                                End If

                            Else
                                MessageBox.Show("Ocurrió un error al intentar crear el formulario ", "Zamba")
                            End If
                        Else
                            MessageBox.Show("El Documento no tiene archivo fisico ni formulario virtual, se cancela su apertura", "Zamba")
                        End If
                    End If

                End If

                If (String.IsNullOrEmpty(Result.AutoName)) Then
                    Result.AutoName = Nothing
                End If

                If Not IsNothing(extVis) Then
                    extVis.Activate()
                End If

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

        End Sub

        Private Function HasExension(doc_File As String) As Boolean

            If doc_File.IndexOf(".") <> -1 Then
                Return True
            End If

            Return False
        End Function

        Private Sub ClearDocuments()
            For Each dc As TabPage In TabViewers.TabPages
                If TypeOf dc Is UCDocumentViewer2 Then
                    DirectCast(dc, UCDocumentViewer2).CloseDocument()
                End If
            Next
        End Sub
        Private Sub FillContentText(ByVal dc As UCDocumentViewer2)
            If Not dc.Result.Name Is Nothing AndAlso dc.Result.Name.Length > 30 Then
                dc.Text = dc.Result.Name.Substring(0, 20)
                dc.ToolTipText = dc.Result.Name
            ElseIf Not dc.Result.Name Is Nothing Then
                dc.Text = dc.Result.Name
                dc.ToolTipText = dc.Result.Name
            ElseIf Not dc.Result.AutoName Is Nothing Then
                dc.Text = dc.Result.AutoName
                dc.ToolTipText = dc.Result.AutoName
            Else
                dc.Text = dc.Result.DocType.Name
                dc.ToolTipText = dc.Result.DocType.Name
            End If
            If dc.iCount > 0 Then dc.Text += "  (" & dc.actualFrame + 1 & " de " & dc.iCount + 1 & ")"
        End Sub
        Private Sub TabViewer_TabRemoved(ByVal o As Object, ByVal e As ControlEventArgs) Handles TabViewers.ControlRemoved
            Try
                If TypeOf e.Control Is UCDocumentViewer2 Then
                    '  CloseDocument(DirectCast(e.Control, UCDocumentViewer2).Result, False)
                    Try
                        RaiseEvent TabInsertFormClosed(DirectCast(e.Control, UCDocumentViewer2).Result.TempId)
                        TabViewers.SelectTab(TabViewers.TabPages(TabViewers.TabCount - 1))
                        'Acanzar evento para eliminar nodo en el wftree
                    Catch ex As Exception

                    End Try
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
#End Region

        Public ReadOnly Property SelectedViewer() As UCDocumentViewer2
            Get
                If TypeOf TabViewers.SelectedTab Is UCDocumentViewer2 Then
                    Return DirectCast(TabViewers.SelectedTab, UCDocumentViewer2)
                Else
                    Return Nothing
                End If
            End Get
        End Property

#Region "TabViewers"

        ''' <summary>
        ''' Evento que se ejecuta cuando se cliquea sobre un tab del TabViewers
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Tomas] 07/08/2009  Modified    Se modifica la llamada al foro
        '''     [Tomas] 18/08/2009  Modified    Se corrige una exception que se presentaba al realizar una busqueda por documentos
        '''     [Sebastian] 20-10-2009 Modified Mostrarla cantidad de mensajes que hay en el foro por doc id
        ''' </history>
        Private Sub TabViewers_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TabViewers.SelectedIndexChanged

            Try

                If (TabViewers.TabPages.Count > 0) Then

                    If TabViewers.SelectedTab Is TabDocAsociated Then
                        ShowDocAsociated(LocalResult)

                    ElseIf TabViewers.SelectedTab Is TabForo Then
                        'If (Not IsNothing(selectedResult)) AndAlso selectedResult.ID <> 0 Then
                        If (Not IsNothing(_selectedresult)) AndAlso _selectedresult.ID <> 0 Then
                            Dim ArrayMensajes As New ArrayList
                            Dim ArrayRespuestas As New ArrayList
                            Dim docIDs As New Generic.List(Of Int64)
                            docIDs.Add(_selectedresult.ID)
                            'Zamba.Core.ZForoBusiness.GetAllMessages(docIDs, ArrayMensajes, ArrayRespuestas)
                            ShowForo(_selectedresult)
                            ' RaiseInfo("En esta seccion usted podra agregar comentarios sobre el documento", "Foro de Documentos", Enums.TMsg.Info)
                        End If

                    ElseIf TabViewers.SelectedTab Is TabHistorialEmails Then

                        If (Not IsNothing(_selectedresult)) AndAlso _selectedresult.ID <> 0 Then
                            ShowHistorialEmails(_selectedresult)
                            'RaiseInfo("En esta seccion usted podra consultar el historial de emails en los cuales se ha enviado este documento", "Historial de emails", Enums.TMsg.Info)
                        End If

                    ElseIf TabViewers.SelectedTab.GetType Is GetType(UCDocumentViewer2) Then
                        ResultSelectedFromGrid = False
                        Dim ViewerResult As Result = DirectCast(TabViewers.SelectedTab, UCDocumentViewer2).Result
                        UCFusion2.SelectResult(ViewerResult)

                        If Not IsNothing(UCFusion2.SelectedResults(True, UCFusion2.GetSelectedRows())) AndAlso Not IsNothing(UCFusion2.SelectedResults(0, UCFusion2.GetSelectedRows())) Then
                            ResultSelected(UCFusion2.SelectedResults(True, UCFusion2.GetSelectedRows())(0))
                        End If

                    End If

                End If

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

        End Sub


        Private Sub TabViewers_ActiveTabChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TabViewers.TabIndexChanged
            Try
                If TabViewers.TabPages.Count > 0 Then
                    If TabViewers.SelectedTab Is TabDocAsociated Then
                        ShowDocAsociated(LocalResult)
                    Else
                        ResultSelectedFromGrid = False
                        Dim ResultSeleccionado As Result = DirectCast(TabViewers.SelectedTab, UCDocumentViewer2).Result
                        ResultSelected(ResultSeleccionado)
                    End If
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
#End Region

        Public Sub ChangeGridLocation(ByVal showTabResultFirst As Boolean)

            'Si no esta dividida la pantalla.
            If SplitContainer3ResultsHorizontal.Panel1.Controls.Count = 0 Then
                SplitContainer3ResultsHorizontal.Panel1.Controls.Add(UCFusion2)
                SplitContainer3ResultsHorizontal.Panel1Collapsed = False
                TabViewers.TabPages.Remove(tabgrid)
                TabViewers.SelectedIndex = index

            Else

                HideGrid()
                If showTabResultFirst Then
                    TabViewers.SelectTab(tabgrid)
                End If

            End If

        End Sub

        Public Sub HideGrid()
            index = TabViewers.SelectedIndex

            If tabgrid Is Nothing OrElse Not TabViewers.TabPages.Contains(tabgrid) Then
                TabViewers.SuspendLayout()
                tabgrid = New ZTabPage("Resultados")
                TabViewers.TabPages.Add(tabgrid)
                tabgrid.Controls.Add(UCFusion2)
                TabViewers.ResumeLayout()
            End If

            SplitContainer3ResultsHorizontal.Panel1Collapsed = True
        End Sub


#Region "Foro"

        ''' <summary>
        ''' Método que permite mostrar la parte de foro y colocarlo adentro del tab "Foro"
        ''' </summary>
        ''' <param name="Result"></param>
        ''' <remarks></remarks>
        ''' <history>
        ''' 	[Gaston]	04/02/2009	Modified    Cuando se ejecuta el evento showMailForm de la instancia de UcForo, se invoca al método "sendMailFromForum"
        ''' </history>
        Private Sub ShowForo(ByRef Result As Result)


            If (UcForo Is Nothing) Then

                UcForo = New UcForo(Membership.MembershipHelper.CurrentUser, Result.ID, Result.DocTypeId)

                RemoveHandler UcForo.showMailForm, AddressOf sendMailFromForum
                AddHandler UcForo.showMailForm, AddressOf sendMailFromForum

                UcForo.Dock = DockStyle.Fill
                TabForo.Controls.Add(UcForo)
                UcForo.ShowInfo(Result.ID, Result.DocTypeId)
            Else
                UcForo.ShowInfo(Result.ID, Result.DocTypeId)

            End If


        End Sub

#End Region

#Region "HistorialEmails"

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

                ucHistorialEmails = New ucHistorialEmails(Result.ID, Membership.MembershipHelper.CurrentUser.ID)

                ucHistorialEmails.Dock = DockStyle.Fill
                TabHistorialEmails.Controls.Add(ucHistorialEmails)

            Else

                ucHistorialEmails.ShowInfo(Result.ID)

            End If

        End Sub

        Private Sub refreshHistorialEmails()

            If Not ucHistorialEmails Is Nothing Then
                ucHistorialEmails.RefreshGrid()
            End If

        End Sub

#End Region


#Region "Thumbnails"








#End Region

#Region "Controler"


        ''' <summary>
        ''' Método que se ejecuta cuando se selecciona un result
        ''' </summary>
        ''' <param name="Result"></param>
        ''' <remarks></remarks>
        ''' <history>
        ''' 	[Gaston]	01/08/2008	Modified    Se agrego una llamada al método showDocRelateds
        ''' </history>
        Public Sub ResultSelected(ByRef Result As Result)
            LocalResult = Result
            Try
                LoadResultRights(Result)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Try
                '[Sebastian 09-06-2009] Se corrigio cast porque estaba mal hecho
                DocTypesBusiness.GetEditRights(DirectCast(Result.DocType, DocType))
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try


            Try
                'Si el tab del foro se encuentra seleccionado, entonces carga los datos del documento seleccionado.
                'La única excepción sería la primer vez que carga la grilla. Esto se debe a que el primer tab seleccionado
                'es el del foro y al encontrarse seleccionado carga el foro, pero luego selecciona el del propio
                'documento y esto no vuelve a suceder.
                If (Not IsNothing(LocalResult) AndAlso LocalResult.ID <> 0) AndAlso (TabViewers.SelectedTab Is TabForo) Then
                    ShowForo(LocalResult)
                End If

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

        End Sub

        Private Sub LoadResultRights(ByVal Result As Result)
            If (toolbar IsNot Nothing) Then
                'GUARDAR
                If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.Documents, RightsType.Saveas) = True Then
                    toolbar.btnSaveAs.Visible = True
                    toolbar.ToolStripSeparator7.Visible = True
                Else
                    toolbar.btnSaveAs.Visible = False
                    toolbar.ToolStripSeparator7.Visible = False
                End If

                'IMPRIMIR
                If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.Documents, RightsType.Print) = True Then
                    toolbar.btnImprimirImagenesIndices.Visible = True
                Else
                    toolbar.btnImprimirImagenesIndices.Visible = False
                End If


                If Result.IsPDF Then
                    'todo: hay que ver porque con el boton de imprimir de zamba el PDF sale en blanco, desde windows no pasa.
                    toolbar.btnImprimirImagenesIndices.Visible = False
                End If

                'ENVIAR POR EMAIL
                If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.Documents, RightsType.EnviarPorMail) = True Then
                    toolbar.btnAdjuntarEmail.Visible = True
                    toolbar.ToolStripSeparator8.Visible = True
                Else
                    toolbar.btnAdjuntarEmail.Visible = False
                    toolbar.ToolStripSeparator8.Visible = False
                End If


                'VERSIONES
                If UCFusion2.useVersion Then
                    toolbar.btnAgregarUnaNuevaVersionDelDocuemto.Visible = UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.AddNewVersions, Result.DocTypeId)
                    toolbar.btnVerVersionesDelDocumento.Visible = UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.ViewVersions, Result.DocTypeId)
                Else
                    toolbar.btnAgregarUnaNuevaVersionDelDocuemto.Visible = False
                    toolbar.btnVerVersionesDelDocumento.Visible = False
                End If

                'Me.toolbar.btnAgregarUnaNuevaVersionDelDocuemto.Visible = False
                'Me.toolbar.btnVerVersionesDelDocumento.Visible = False
                ''VERSIONES
                'Me.UCFusion2.useVersion = UserBusiness.Rights.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.ModuleVersions, RightsType.Use)
                'Me.toolbar.ToolStripSeparator12.Visible = Me.UCFusion2.useVersion

            End If
        End Sub

        Public Sub SaveAllObjects()
            Try
                If Not IsNothing(SelectedViewer) Then SelectedViewer.ImgViewer.SaveAllObjects()
            Catch ex As Exception
            End Try
        End Sub


        Public Function hasDocumentsOpened() As Boolean
            Dim documentOpen As Boolean = False
            For Each tab As TabPage In TabViewers.TabPages
                If TypeOf tab Is UCDocumentViewer2 Then
                    documentOpen = True
                    Exit For
                End If
            Next
            Return documentOpen
        End Function

        Public Function CloseActiveDocumentViewerTab() As Boolean
            Try
                If TabViewers.TabCount > 0 Then
                    If TypeOf TabViewers.SelectedTab Is UCDocumentViewer2 Then
                        DirectCast(TabViewers.SelectedTab, UCDocumentViewer2).CloseDocument()
                        Return True
                    Else
                        For Each tab As TabPage In TabViewers.TabPages
                            If TypeOf tab Is UCDocumentViewer2 Then
                                TabViewers.SelectTab(tab)
                                DirectCast(tab, UCDocumentViewer2).CloseDocument()
                                Return True
                                Exit For
                            End If
                        Next
                    End If
                Else
                    Return False
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return False
            End Try
        End Function

        Public Sub CloseDocumentViewerTab(ByRef Result As Result)
            Try
                For Each dc As TabPage In TabViewers.TabPages
                    If TypeOf dc Is UCDocumentViewer2 Then
                        If DirectCast(dc, UCDocumentViewer2).Result.ID = Result.ID Then
                            DirectCast(dc, UCDocumentViewer2).CloseDocument()
                        End If
                    End If
                Next
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Public Sub CloseAllDocumentViewerTabs()
            Try
                For Each dc As TabPage In TabViewers.TabPages
                    If TypeOf dc Is UCDocumentViewer2 Then
                        DirectCast(dc, UCDocumentViewer2).CloseDocument()
                    End If
                Next
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

#End Region


#Region "Results"

        Public Sub FillListView(ByVal ResultsList As DataTable, ByVal Searchname As String, ByVal Search As ISearch)
            Try

                If Search IsNot Nothing Then LastSearch = Search
                LoadGridResults(CurrentUserId)
                UCFusion2.DisableFilters()
                UCFusion2.FillResults(ResultsList, Search)
                If ResultsList.Rows.Count = 0 AndAlso UCFusion2.FilterCount > 0 Then
                    UCFusion2.AddMessageRow("Existen filtros que pueden estar ocultando los documentos solicitados")
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Muestra el result en la grilla de busqueda
        ''' </summary>
        ''' <param name="ResultsList"></param>
        ''' <param name="Searchname"></param>
        ''' <remarks></remarks>
        Public Sub FillListView(ByVal ResultsList As Result, ByVal Searchname As String)
            Try
                LoadGridResults(CurrentUserId)
                UCFusion2.AddResult(ResultsList)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
        Delegate Sub DFillListView()

#End Region

#Region "Print"

        ''' <summary>
        ''' Método encargado de abrir el formulario de impresión
        ''' </summary>
        ''' <param name="results"></param>
        ''' <param name="OnlyIndexs"></param>
        ''' <remarks></remarks>
        ''' <history>
        ''' 	[Gaston]	16/05/2008	Modified
        ''' 	[Marcelo]	03/02/2009	Modified
        ''' </history>
        Public Sub Imprimir(ByVal results As List(Of IResult), ByVal loadAction As Zamba.Print.LoadAction)
            Try
                If (results IsNot Nothing) Then

                    'Dim r As Object = results
                    Dim resultsToPrint As New List(Of IPrintable)
                    For Each r As IResult In results
                        resultsToPrint.Add(TryCast(r, IPrintable))
                    Next
                    ' Se muestra el formulario de impresión
                    Dim Zp As New Zamba.Print.frmchooseprintmode(resultsToPrint, loadAction)
                    'Dim Zp As New Zamba.Print.frmchooseprintmode(TryCast(r, List(Of IPrintable)), loadAction)
                    'Evento que imprime el formulario virtual
                    AddHandler Zp.PrintVirtual, AddressOf PrintVirtualDocument
                    ' Si el cliente presiono el botón Imprimir que aparece en el formulario de impresión
                    If (Zp.ShowDialog = DialogResult.OK) Then
                        ' Se actualiza el U_TIME del usuario en UCM y se registra la acción (de que presiono el botón Imprimir) en la tabla USER_HST antes
                        ' de que aparezca el formulario de impresión
                        UserBusiness.Rights.SaveAction(CurrentUserId, ObjectTypes.Documents, RightsType.Print, "Se imprimio formulario")
                    End If
                    RemoveHandler Zp.PrintVirtual, AddressOf PrintVirtualDocument
                    Zp.Dispose()
                    Zp = Nothing
                End If
                'End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
        Private Sub PrintVirtualDocument(ByVal r As IPrintable)
            Dim UcViewer As UCDocumentViewer2 = Nothing
            Try
                UcViewer = GetViewerByResult(DirectCast(r, Result))
                If IsNothing(UcViewer) Then
                    UcViewer = New UCDocumentViewer2(Me, DirectCast(r, Result), False, False)
                    TabViewers.TabPages.Add(UcViewer)
                    TabViewers.SelectTab(UcViewer)
                    UcViewer.ShowDocument(True, False, False, False, True)
                    RemoveHandler UcViewer.LinkSelected, AddressOf UCFusion2.SelectResult
                    RemoveHandler UcViewer.EventZoomLock, AddressOf EventZoomLock
                    RemoveHandler UcViewer.Movido, AddressOf Movido
                    RemoveHandler UcViewer.CambiarDock, AddressOf CambiarDock
                    RemoveHandler UcViewer.ShowAsociatedResult, AddressOf ShowAsociatedResult
                    RemoveHandler UcViewer.ClearReferences, AddressOf ClearInstanceReferences
                    AddHandler UcViewer.LinkSelected, AddressOf UCFusion2.SelectResult
                    AddHandler UcViewer.EventZoomLock, AddressOf EventZoomLock
                    AddHandler UcViewer.Movido, AddressOf Movido
                    AddHandler UcViewer.CambiarDock, AddressOf CambiarDock
                    AddHandler UcViewer.ShowAsociatedResult, AddressOf ShowAsociatedResult
                    AddHandler UcViewer.ClearReferences, AddressOf ClearInstanceReferences
                    'todo: ver porque no llama a imprimir despues de mostrarlo.
                Else
                    UcViewer.PrintDocumentWB()
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Imprime el formulario virtual
        ''' </summary>
        ''' <param name="r"></param>
        ''' <remarks></remarks>
        Private Sub PrintVirtualDocument(ByVal r As Result)
            Dim UcViewer As UCDocumentViewer2 = Nothing
            Try
                UcViewer = GetViewerByResult(r)
                If IsNothing(UcViewer) Then
                    UcViewer = New UCDocumentViewer2(Me, r, False, False)
                    TabViewers.TabPages.Add(UcViewer)
                    TabViewers.SelectTab(UcViewer)
                    UcViewer.ShowDocument(True, False, False, False, True)
                    RemoveHandler UcViewer.LinkSelected, AddressOf UCFusion2.SelectResult
                    RemoveHandler UcViewer.EventZoomLock, AddressOf EventZoomLock
                    RemoveHandler UcViewer.Movido, AddressOf Movido
                    RemoveHandler UcViewer.CambiarDock, AddressOf CambiarDock
                    RemoveHandler UcViewer.ShowAsociatedResult, AddressOf ShowAsociatedResult
                    RemoveHandler UcViewer.ClearReferences, AddressOf ClearInstanceReferences
                    AddHandler UcViewer.LinkSelected, AddressOf UCFusion2.SelectResult
                    AddHandler UcViewer.EventZoomLock, AddressOf EventZoomLock
                    AddHandler UcViewer.Movido, AddressOf Movido
                    AddHandler UcViewer.CambiarDock, AddressOf CambiarDock
                    AddHandler UcViewer.ShowAsociatedResult, AddressOf ShowAsociatedResult
                    AddHandler UcViewer.ClearReferences, AddressOf ClearInstanceReferences
                    'todo: ver porque no llama a imprimir despues de mostrarlo.
                Else
                    UcViewer.PrintDocumentWB()
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub


#End Region

#Region "Ver Documentos Asociados"
        'Private Shared Function GetDocumentAsociatedCount(ByRef Result As Result) As Int16
        '    Return DocAsociatedBusiness.GetDocumentAsociatedCount(result)
        'End Function
        Private Sub VerDocumentosAsociados(ByRef Result As Result)
            'todo store: SPGetCount_DocTypeID1
            Try

                BuscarDocAsoc(Result)

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

        End Sub
        ''' <summary>
        ''' Busca los documentos asociados
        ''' </summary>
        ''' <history>
        ''' [Sebastian] 23-11-2009 Seleccion de asociado a cargar en la grilla de búsqueda
        ''' </history>
        ''' <param name="Result"></param>
        ''' <history>   Marcelo Modified 20/08/2009</history>
        ''' <remarks></remarks>
        Private Sub BuscarDocAsoc(ByRef Result As Result)
            Try
                Dim Results As DataTable = DocAsociatedBusiness.getAsociatedDTResultsFromResult(Result, 0, False, Nothing)
                If Results.Rows.Count > 0 Then
                    UCFusion2.FillResults(Results, LastSearch)
                Else
                    MessageBox.Show("No existen asociaciones para este documento", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

#End Region


#Region "Versioning"

        ''' <summary>
        ''' Muestra las versiones de un result
        ''' </summary>
        ''' <param name="result"></param>
        ''' <history>   Marcelo Modified 20/08/2009</history>
        ''' <remarks></remarks>
        Public Sub ShowVersions(ByRef Result As Result)
            Try
                Dim frm As New frmVersionedDetails(Result)
                RemoveHandler frm.ShowVersion, AddressOf ShowResult
                AddHandler frm.ShowVersion, AddressOf ShowResult
                RemoveHandler frm.PublishVersion, AddressOf ShowVersionComment
                AddHandler frm.PublishVersion, AddressOf ShowVersionComment
                frm.ShowDialog()
                'Me.UCFusion2.HaveVersions = True

                'Dim Results As DataTable = ModDocuments.SearchParentVersions(Result)

                'If IsNothing(Results) OrElse Results.Rows.Count = 0 Then
                '    MessageBox.Show("NO SE ENCONTRARON VERSIONES DEL DOCUMENTO")
                'Else
                '    UCFusion2.gridsort = False
                '    Me.UCFusion2.ClearSearchs()
                '    UCFusion2.FillResults(Results, Nothing)
                'End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Añade un nuevo número de versión al result, Sobrecarga creada para version automatica
        ''' debido a que en este caso se utiliza el archivo local en ves de el del servidor
        ''' </summary>
        ''' <param name="result"></param>
        ''' <remarks></remarks>
        Public Sub AddNewVersion(ByRef _Result As Result)
            Try
                If _Result.HasVersion = 1 Then
                    If Not UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.AddFromVersions, _Result.DocType.ID) Then
                        MessageBox.Show("No tiene los permisos necesarios para crear una nueva version de un documento Versionado", "Zamba", MessageBoxButtons.OK)
                        Exit Sub
                    End If
                End If

                If _Result.IsOffice OrElse _Result.IsXoml Then

                    '<DIE> subi este codigo aca para que si el archivo se encuentra abierto y modificado
                    'y se quiere hacer una nueva version, se guarde, se cierre, se suba al servidor
                    ' y luego se cree la nueva version en base a la modificada en el servidor
                    Dim Viewer As UCDocumentViewer2
                    Viewer = GetViewerByResult(_Result)
                    If IsNothing(Viewer) = False Then
                        If Not IsNothing(SelectedViewer) Then
                            RemoveHandler SelectedViewer.eAutomaticNewVersion, AddressOf AddNewVersion
                        End If
                        Viewer.SaveDocument()
                        'todo: Actualmente no esta cerrando el documento padre si se encuentra abierto
                        Viewer.CloseDocument(_Result)
                    End If
                    '</DIE>

                    Dim frmComment As New frmGenerateNewVersion(_Result)
                    frmComment.ShowDialog()
                    If frmComment.DialogResult = DialogResult.OK Then

                        Dim newVersion As Result = frmComment.GetNewResult
                        Dim tempFilePath As String = _Result.File
                        ShowResult(newVersion)
                    Else
                        Exit Sub
                    End If
                Else
                    MessageBox.Show("Nueva Versión está disponible para documentos de Office solamente.")
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Añade un nuevo número de versión al result.
        ''' Sobrecarga utilizada para el WebBrowser.
        ''' </summary>
        ''' <param name="result"></param>
        ''' <remarks></remarks>
        Public Sub AddNewVersion(ByRef _Result As Result, ByVal TempResultPath As String)
            'sobrecarga creada para version automatica, ya que necesita el path del archivo local 
            ' en ves de la del servidor
            Try
                If _Result.HasVersion = 1 Then
                    If Not UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.AddFromVersions, _Result.DocType.ID) Then
                        MessageBox.Show("No tiene los permisos necesarios para crear una nueva version de un documento Versionado", "Zamba", MessageBoxButtons.OK)
                        Exit Sub
                    End If
                End If

                If _Result.IsOffice OrElse _Result.IsXoml Then

                    '<DIE> subi este codigo aca para que si el archivo se encuentra abierto y modificado
                    'y se quiere hacer una nueva version, se guarde, se cierre, se eleve al servidor
                    ' y luego se cree la nueva version en base a la modificada en el servidor
                    'Dim Viewer As UCDocumentViewer2
                    'Viewer = GetViewerByResult(_Result)
                    'If IsNothing(Viewer) = False Then
                    'RemoveHandler SelectedViewer.eAutomaticNewVersion, AddressOf AddNewVersion
                    'Viewer.SaveDocument()
                    'todo: Actualmente no esta cerrando el documento padre si se encuentra abierto
                    'Viewer.CloseDocument(_Result, True)
                    'End If
                    '</DIE>

                    Dim frmComment As New frmGenerateNewVersion(_Result, TempResultPath)
                    frmComment.ShowDialog()
                    If frmComment.DialogResult = DialogResult.OK Then

                        Dim newVersion As Result = frmComment.GetNewResult

                        Dim tempFilePath As String = _Result.File
                        'Dim newVersion As Result = Results_Business.InsertNewVersion(_Result, frmComment.Comment)


                        'borra el padre de la grilla
                        UCFusion2.AddResult(newVersion)
                        'abro el documento nuevo en el webbroser
                        ShowResult(newVersion)
                        ShowVersions(newVersion)
                        UCFusion2.SelectResult(newVersion, True)
                        ResultSelected(newVersion)
                    Else
                        'Results_Business.Delete(newVersion)
                        Exit Sub
                    End If
                Else
                    MessageBox.Show("Nueva Versión está disponible para documentos de Office solamente.")
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Private Sub ShowVersionComment(ByVal _result As Result)

            Dim Result As Result = Nothing

            If _result Is Nothing Then
                Result = LocalResult
            Else
                Result = _result
            End If

            Dim collection As New System.Collections.Generic.List(Of String)
            Dim comment As String
            Dim commentdate As String

            comment = Results_Business.GetVersionComment(Result.ID)
            commentdate = Results_Business.GetVersionCommentDate(Result.ID)
            If String.IsNullOrEmpty(commentdate) Then
                commentdate = "No se registran datos de este documento"
                comment = String.Empty
            End If

            Dim PublishResult As New PublishableResult
            PublishResult = Results_Business.PublishableResult(Result)
            Dim showversion As New frmPublishVersion(PublishResult, Result, comment, commentdate)

            showversion.ShowDialog()
            RefreshGrid()
        End Sub
#End Region

#Region "BirdMegaView"

        ' Dim ShowingBirdMegaView As Boolean
        Dim ZoomLock As Boolean
        Dim NewTop As Int32
        Dim NewLeft As Int32

        Private Sub EventZoomLock(ByVal Estado As Boolean)
            ZoomLock = Not Estado
        End Sub
        Private Sub Movido(ByVal Top As Int32, ByVal Left As Int32)
            NewTop = Top
            NewLeft = Left
        End Sub

        Private Sub ShowOriginal(ByRef Result As Result)
            NewDocumentViewer(Result, True)
        End Sub
#End Region


        Private Sub AddItem(ByVal Item As Object)
            Try
                Dim Result As Result = DirectCast(Item, Result)
                ResultSelected(Result)
                ShowResult(Result)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Método que invoca el formulario de mail (configurado en el administrador) cuando se presiona el botón "Guardar/Notificar" del form. de Foro
        ''' </summary>
        ''' <param name="textFor">Usuarios o grupos a los que se les va enviar el mensaje</param>
        ''' <param name="textSubject">Asunto del mensaje</param>
        ''' <param name="textBody">Cuerpo del mensaje</param>
        ''' <remarks></remarks>
        ''' <history>
        ''' 	[Gaston]	04/02/2009	Created   
        '''     [Ezequiel]  21/04/09 Modified - Se agrego opcion de adjuntar o no formularios virtuales.
        ''' </history>
        Private Sub sendMailFromForum(ByRef textFor As String, ByRef textSubject As String, ByRef textBody As String, ByVal idMensaje As Int32, ByVal parentId As Int64, ByVal blnAutomaticAttachLink As Boolean, ByVal blnAutomaticSend As Boolean, ByVal attachPaths() As String)

            Dim SelectedResults As New Generic.List(Of IResult)
            SelectedResults.AddRange(UCFusion2.SelectedResults(False, UCFusion2.GetSelectedRows()))

            Dim SelectedResultsaux As New Generic.List(Of IResult)

            Try
                ' [AlejandroR] - 05/03/2010 - Created
                ' Se chequea antes de enviar el mail que se tenga configurada y con acceso
                ' la ruta para el historial de emails
                MessagesBusiness.CheckHistoryExportPath()
            Catch ex As Exception
                UcForo.closeNuevoMensaje()
                MessageBox.Show(ex.Message, "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ZClass.raiseerror(ex)
                Exit Sub
            End Try

            For Each r As Result In SelectedResults
                Try
                    If r.ISVIRTUAL Then
                        Try
                            If Boolean.Parse(UserPreferences.getValue("PreviewFormInForum", UPSections.UserPreferences, "True")) Then
                                Dim prvfrm As New PreviewForm(r, "Adjuntar")

                                If prvfrm.ShowDialog = DialogResult.OK Then
                                    r.Html = prvfrm.frmbrowser.GetHtml

                                    r.HtmlFile = Tools.EnvironmentUtil.GetTempDir("\OfficeTemp").FullName & "\" & r.Name & "-temp" & (New Random).Next(1, 9999).ToString & ".html"
                                    Dim form As ZwebForm = FormBusiness.GetShowAndEditForms(r.DocType.ID)(0)
                                    If File.Exists(form.Path.Replace(".html", ".mht")) Then
                                        Try
                                            Using write As New StreamWriter(r.HtmlFile.Substring(0, r.HtmlFile.Length - 4) & "mht")
                                                write.AutoFlush = True
                                                Dim reader As New StreamReader(form.Path.Replace(".html", ".mht"))
                                                Dim mhtstring As String = reader.ReadToEnd()
                                                write.Write(mhtstring.Replace("<Zamba.Html>", r.Html))
                                            End Using
                                        Catch ex As Exception
                                            ZClass.raiseerror(ex)
                                        End Try

                                        r.HtmlFile = r.HtmlFile.Substring(0, r.HtmlFile.Length - 4) & "mht"
                                    Else
                                        Try
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

            'If Membership.MembershipHelper.CurrentUser.eMail.Type = MailTypes.OutLookMail Then
            'Dim frmMessage As OutlookMessageForm

            'frmMessage = Zamba.AdminControls.clsMessages.getOutlookMailForm(SelectedResults, textFor, textSubject, textBody)
            'ResulEnvio = frmMessage.ShowDialog()

            'Else
            Dim frmMessage As IZMessageForm
            ' Se obtiene el formulario de mail y se muestra 

            frmMessage = clsMessages.getMailFormFromForum(SelectedResults, textFor, textSubject, textBody)
            If blnAutomaticAttachLink = True Then
                Dim links As New ArrayList
                links.Add(True)
                frmMessage.EspecificarDatos(textFor, String.Empty, String.Empty, textSubject, textBody, Nothing, Nothing, blnAutomaticSend, links)
            Else
                frmMessage.EspecificarDatos(textFor, String.Empty, String.Empty, textSubject, textBody, attachPaths, Nothing, blnAutomaticSend)
            End If
            'End If
            If blnAutomaticSend = False Then
                'se cambia el envio modal por NO modal
                'ResulEnvio = DirectCast(frmMessage, Form).ShowDialog()

                Select Case frmMessage.GetType.Name.ToLower
                    Case "frmlotusmessagesend"
                        DirectCast(frmMessage, frmLotusMessageSend).MailEvent = MailEvent.Forum_Search
                    Case "frmnetmailmessagesend"
                        DirectCast(frmMessage, frmNetMailMessageSend).MailEvent = MailEvent.Forum_Search
                End Select

                Dim form As System.Windows.Forms.Form
                form = DirectCast(frmMessage, Form)

                form.ControlBox = True
                form.Show(Me)
            Else
                ResulEnvio = True
            End If

            UcForo.closeNuevoMensaje()

        End Sub
#Region "EnviarZip"
        Public Sub EnviarZip(ByRef allResults As Generic.List(Of IResult))

            Try
                If Not RightsBusiness.GetUserRights(ObjectTypes.Documents, RightsType.Saveas) And
                    Not RightsBusiness.GetUserRights(ObjectTypes.Documents, RightsType.EnviarPorMail) Then

                    MessageBox.Show("Usted no tiene permiso para guardar los Documentos.", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Se intento guardar documento pero el usuario no cuenta con los permisos.")
                    Exit Sub

                End If
                ' Se chequea antes de enviar el mail que se tenga configurada y con acceso
                ' la ruta para el historial de emails
                MessagesBusiness.CheckHistoryExportPath()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ZClass.raiseerror(ex)
                Exit Sub
            End Try

            Try
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Comienza el proceso para generar y enviar el zip")

                Dim queryUser As New GetPasswordZip()
                Dim zip As New Ionic.Zip.ZipFile
                Dim arrayDocs As New ArrayList()
                Dim pathToZip As String
                Dim bodyMail As New StringBuilder()



                For Each Result As Result In allResults

                    If Result Is Nothing OrElse (String.IsNullOrEmpty(Result.FullPath) AndAlso String.IsNullOrEmpty(Result.Doc_File)) Then
                        If Result.ISVIRTUAL Then
                            MessageBox.Show("Alguno de las tareas seleccionadas no tiene documento adjunto, por favor revisar los elementos seleccionados.", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "La tarea no posee documento.")
                            Exit Sub
                        Else
                            MessageBox.Show("El documento no esta accesible, consulte al administrador del sistema.", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                            ZTrace.WriteLineIf(ZTrace.IsError, "El documento no se encuentra, verificar que exista en el volumen.")
                            Exit Sub
                        End If
                    End If

                    If Not IsNothing(Result.FullPath) Then
                        Dim file As FileInfo = GetNameAndPathToFile(Result)
                        If file.Exists Then
                            zip.AddFile(file.FullName, "")
                        End If
                        bodyMail.Append(file.Name & "; ")
                    End If
                Next

                bodyMail.Append("Documentos adjuntos dentro del comprimido;")
                queryUser.ControlBox = False
                queryUser.ShowDialog()

                If IsNothing(queryUser.CancelPass.Tag) Then

                    For index = 0 To zip.Count - 1
                        If Not String.IsNullOrEmpty(queryUser.BoxPass.Text) Then zip.Item(index).Password = queryUser.BoxPass.Text
                    Next

                    If Not String.IsNullOrEmpty(queryUser.NameFile.Text) Then
                        pathToZip = Membership.MembershipHelper.AppTempPath & "\temp\" & queryUser.NameFile.Text & ".zip"
                        zip.Comment = queryUser.NameFile.Text & ".zip"
                    Else
                        pathToZip = Membership.MembershipHelper.AppTempPath & "\temp\" & allResults(0).Name & ".zip"
                        zip.Comment = allResults(0).Name & ".zip"
                    End If

                    zip.Save(pathToZip)

                    If UserBusiness.Rights.CurrentUser.eMail.Type = MailTypes.OutLookMail Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Pido el Form de Envio del Mail")

                        arrayDocs.Add(zip.Name)

                        Using frmMessage As OutlookMessageForm = Zamba.AdminControls.clsMessages.getOutlookMailForm(Nothing, arrayDocs, False, False, False)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Muestro el Form de Envio del Mail con Modal en False")
                            frmMessage.EspecificarDatos(Nothing, Nothing, Nothing, "Documentos adjuntos " & zip.Comment, bodyMail.ToString(), Nothing, Nothing, False, Nothing, False)
                            frmMessage.ShowDialog(True)
                        End Using
                    ElseIf UserBusiness.Rights.CurrentUser.eMail.Type = 0 Then
                        MessageBox.Show("Usted no tiene configurada la cuenta de correo.", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                    End If

                    File.Delete(pathToZip)
                End If


            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
#End Region


#Region "GuardarComo"
        Private Sub GuardarComo(ByRef allResults As List(Of IResult))
            Try
                If Not RightsBusiness.GetUserRights(ObjectTypes.Documents, RightsType.Saveas) Then
                    MessageBox.Show("Usted no tiene permiso para guardar los Documentos.", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Se intento guardar documento pero el usuario no cuenta con los permisos.")
                    Exit Sub
                End If

                If allResults.Count > 1 Then
                    Dim folderSave As New FolderBrowserDialog
                    folderSave.SelectedPath = Environment.SpecialFolder.MyDocuments
                    folderSave.ShowNewFolderButton = True
                    If (folderSave.ShowDialog() = DialogResult.OK) Then
                        Dim pathSaveFiles As String = folderSave.SelectedPath
                        For Each Result As Result In allResults
                            Dim Resultfile As FileInfo = GetNameAndPathToFile(Result)
                            If Resultfile IsNot Nothing AndAlso Resultfile.Exists Then
                                Dim namePath As String = Path.Combine(pathSaveFiles, Tools.FileBusiness.FormatFileName(Result.Name))
                                namePath = Path.ChangeExtension(namePath, Resultfile.Extension)
                                Resultfile.CopyTo(FileBusiness.GetUniqueFileName(namePath), True)
                            End If
                        Next
                    End If
                ElseIf allResults.Count = 1 Then
                    Dim folderSave As New SaveFileDialog
                    folderSave.InitialDirectory = Environment.SpecialFolder.MyDocuments
                    folderSave.FileName = Zamba.Tools.FileBusiness.FormatFileName(allResults(0).Name)
                    If (folderSave.ShowDialog() = DialogResult.OK) Then
                        For Each Result As Result In allResults
                            Dim file As FileInfo = GetNameAndPathToFile(Result)
                            If file IsNot Nothing AndAlso file.Exists Then
                                Dim namePath As String = Path.ChangeExtension(folderSave.FileName, file.Extension)
                                file.CopyTo(FileBusiness.GetUniqueFileName(namePath), True)
                            End If
                        Next
                    End If
                Else
                    MessageBox.Show("Tenes que seleccionar al menos un registro para guardar.", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Private Function GetNameAndPathToFile(ByVal Result As Result) As FileInfo

            'Results_Business.CompleteDocument(Result)

            If String.IsNullOrEmpty(Result.FullPath) OrElse String.IsNullOrEmpty(Result.Doc_File) Then

                If Result.ISVIRTUAL Then
                    MessageBox.Show("La tarea no posee un documento para guardar.", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "La tarea no posee documento.")
                    Exit Function
                Else
                    MessageBox.Show("El documento no esta accesible, consulte al administrador del sistema.", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                    ZTrace.WriteLineIf(ZTrace.IsError, "El documento no se encuentra, verificar que exista en el volumen.")
                    Exit Function

                End If
            End If

            Try
                Dim namefile As String
                If Result.FullPath.Length > 0 AndAlso Not File.Exists(Result.FullPath) Then
                    namefile = Results_Business.GetDBTempFile(Result)
                Else
                    namefile = Result.FullPath
                End If

                Dim filePath As FileInfo

                If Not String.IsNullOrEmpty(namefile) Then
                    filePath = New FileInfo(namefile)
                End If

                If Result.ISVIRTUAL Then

                    If Not IsNothing(SelectedViewer) Then
                        Result.Html = SelectedViewer.GetHtml()
                    End If
                    Result.HtmlFile = Tools.EnvironmentUtil.GetTempDir("\OfficeTemp").FullName & "\" & Result.Name & "-temp" & (New Random).Next(1, 9999).ToString & ".html"
                    Try
                        If File.Exists(Result.HtmlFile) Then
                            File.Delete(Result.HtmlFile)
                        End If
                    Catch ex As Exception
                    End Try

                    Dim htmlFile As String = Result.HtmlFile
                    htmlFile = htmlFile.Substring(0, htmlFile.IndexOf(Result.Name))
                    htmlFile = Path.Combine(htmlFile, Tools.FileBusiness.FormatFileName(Result.Name))
                    htmlFile = Path.ChangeExtension(htmlFile, ".html")
                    'htmlFile &= Result.Name.Replace(":", " ").Replace("<", " ").Replace(">", " ").Replace("*", " ").Replace("|", " ").Replace("""", " ").Replace("/", " ").Replace("\", " ").Replace("?", " ").Replace("  ", " ").Replace("  ", " ") & ".html"
                    'htmlFile = Result.HtmlFile.Substring(0, Result.HtmlFile.Replace(":", " ").Replace("<", " ").Replace(">", " ").Replace("*", " ").Replace("|", " ").Replace("""", " ").Replace("/", " ").Replace("\", " ").Replace("?", " ").Length - 4) & "mht"
                    Try
                        Using write As New StreamWriter(htmlFile)
                            write.AutoFlush = True
                            Dim reader As New StreamReader(FormBusiness.GetShowAndEditForms(Result.DocTypeId)(0).Path)
                            Dim mhtstring As String = reader.ReadToEnd()
                            write.Write(mhtstring.Replace("<Zamba.Html>", Result.Html))
                        End Using
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try

                    Try
                        File.Delete(Tools.FileBusiness.FormatFileName(Result.HtmlFile))
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try

                    'Result.HtmlFile = Tools.FileBusiness.FormatFileName(Result.HtmlFile)
                    'filePath = New FileInfo(Tools.FileBusiness.FormatFileName(Result.HtmlFile))
                    filePath = New FileInfo(htmlFile)

                End If

                Return filePath
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Function
#End Region
#Region "Convert to PDF"

        Private Sub ConvertToPdf(ByVal ResultArray() As Result)

            Dim ResultsImages As New Generic.List(Of Result)
            Dim ResultsWord As New Generic.List(Of Result)

            For Each CurrentResult As Result In ResultArray
                If CurrentResult.IsImage Or Not IsNothing(CurrentResult.Picture) Then
                    ResultsImages.Add(CurrentResult)
                End If
                If CurrentResult.IsWord Then
                    ResultsWord.Add(CurrentResult)
                End If
            Next

            CantPdfs = 0

            If ResultsImages.Count > 0 Then

                Dim frmSelectDirectory As New FolderBrowserDialog
                frmSelectDirectory.Description = "Carpeta donde se exportarán los PDFs"
                frmSelectDirectory.ShowDialog()

                Dim PdfFolderPath As String = frmSelectDirectory.SelectedPath

                If String.IsNullOrEmpty(PdfFolderPath) Then
                    MessageBox.Show("Debe seleccionar una carpeta donde guardar los PDFs generados", "Exportar a PDF", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    For Each CurrentResult As Result In ResultsImages
                        Results_Business.ConvertToPdfFile(CurrentResult, PdfFolderPath, CantPdfs)
                    Next
                End If

            End If

            If ResultsWord.Count > 0 Then

                Dim PDFPrinter As String

                'Obtener nombre de impresora virtual
                PDFPrinter = ZOptBusiness.GetValue("PDFPrinter")

                If String.IsNullOrEmpty(PDFPrinter) Then

                    MessageBox.Show("No se ha configurado la impresora a utilizar para generar los PDFs", "Exportar a PDF", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else

                    Cursor = Cursors.WaitCursor

                    Application.DoEvents()

                    For Each CurrentResult As Result In ResultsWord
                        If Results_Business.ConvertDocToPdfFile(CurrentResult, PDFPrinter) Then
                            CantPdfs += 1
                        End If
                    Next

                    ShowOfficeToolbars()

                    Cursor = Cursors.Default
                End If
            End If

            If CantPdfs = 1 Then
                MessageBox.Show("El documento se exporto a PDF exitosamente", "Exportar a PDF", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf CantPdfs > 1 Then
                MessageBox.Show("Se exportaron " & CantPdfs.ToString & " documentos a PDF de manera exitosa", "Exportar a PDF", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        End Sub

#End Region

        Private Function GetViewerByResult(ByRef Result As Result) As UCDocumentViewer2
            For Each dc As TabPage In TabViewers.TabPages
                If TypeOf dc Is UCDocumentViewer2 Then
                    Dim zvc As UCDocumentViewer2 = DirectCast(dc, UCDocumentViewer2)
                    If zvc.Result.ID = Result.ID Then
                        Return zvc
                    End If
                End If
            Next
            Return Nothing
        End Function

        ''' <summary>
        ''' [sebastian] 09-06-2009 se agrego cast para salvar warning
        ''' </summary>
        ''' <param name="Result"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetTaskViewerByResult(ByRef Result As Result) As UCDocumentViewer2
            For Each dc As TabPage In TabViewers.TabPages
                If TypeOf dc Is UCDocumentViewer2 Then
                    Dim zvc As UCDocumentViewer2 = DirectCast(dc, UCDocumentViewer2)
                    If zvc.Result.ID = Result.ID Then
                        Return zvc
                    End If
                End If
            Next
            Return Nothing
        End Function

#Region "TAB Document Asociated"
        ''' <summary>
        ''' Mostrar los documentos asociados
        ''' </summary>
        ''' <param name="Result"></param>
        ''' <history>   Marcelo Modified 20/08/2009</history>
        ''' <remarks></remarks>
        Private Sub ShowDocAsociated(ByRef Result As Result)
            Try
                Dim asocResults As DataTable = DocAsociatedBusiness.getAsociatedDTResultsFromResult(Result, 0, False, Nothing)
                If (asocResults IsNot Nothing) Then
                    If TabDocAsociated.Controls.Count = 0 Then

                        Dim grdDocsAsoc As New UCFusion2(UCFusion2.Modes.AsociatedResults, CurrentUserId, Result, Me)


                        'todo diego, sacarle el item insertar relacion del  contextmenu
                        grdDocsAsoc.Dock = DockStyle.Fill
                        grdDocsAsoc.inicializarGrilla()
                        TabDocAsociated.Controls.Add(grdDocsAsoc)
                        ' ivan - 11/12/15.  Agregue la siguiente linea para setear esta propiedad y que no me pida recargar la grilla si no hace falta.
                        asocResults.MinimumCapacity = asocResults.Rows.Count
                        grdDocsAsoc.FillResults(asocResults, Nothing)
                        grdDocsAsoc.Update()
                        grdDocsAsoc.Refresh()
                        RemoveHandler grdDocsAsoc.ResultDoubleClick, AddressOf ShowAsociatedResult
                        AddHandler grdDocsAsoc.ResultDoubleClick, AddressOf ShowAsociatedResult
                        RemoveHandler grdDocsAsoc._RefreshGrid, AddressOf refreshAsoc
                        AddHandler grdDocsAsoc._RefreshGrid, AddressOf refreshAsoc
                    Else
                        Dim grdDocsAsoc As UCFusion2 = DirectCast(TabDocAsociated.Controls(0), UCFusion2)
                        grdDocsAsoc.ClearSearchs()
                        grdDocsAsoc.FillResults(asocResults, Nothing)
                    End If
                End If

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Private Sub refreshAsoc()
            If Not IsNothing(LocalResult) And Not IsNothing(TabDocAsociated) AndAlso TabDocAsociated.Controls.Count > 0 Then
                Dim asocResults As DataTable = DocAsociatedBusiness.getAsociatedDTResultsFromResult(LocalResult, 0, False, Nothing)
                Dim grdDocsAsoc As UCFusion2 = DirectCast(TabDocAsociated.Controls(0), UCFusion2)
                grdDocsAsoc.ClearSearchs()
                grdDocsAsoc.FillResults(asocResults, Nothing)
            End If
        End Sub
        ''' <summary>
        ''' [Sebastian ] 09-06-2009 Modified se agrego cast para salvar warnings
        ''' </summary>
        ''' <param name="AsociatedResult"></param>
        ''' <remarks></remarks>
        Private Sub ShowAsociatedResult(ByRef AsociatedResult As Result)
            Try
                Dim Viewer As UCDocumentViewer2 = GetViewerByResult(AsociatedResult)

                If IsNothing(Viewer) Then

                    DocTypesBusiness.GetEditRights(DirectCast(AsociatedResult.DocType, DocType))
                    Dim UcViewer As New UCDocumentViewer2(Me, AsociatedResult, False, False)
                    RemoveHandler UcViewer.ShowAsociatedResult, AddressOf ShowAsociatedResult
                    AddHandler UcViewer.ShowAsociatedResult, AddressOf ShowAsociatedResult

                    RemoveHandler UcViewer.ShowOriginal, AddressOf ShowOriginal
                    AddHandler UcViewer.ShowOriginal, AddressOf ShowOriginal


                    RemoveHandler UcViewer.CambiarDock, AddressOf CambiarDock
                    AddHandler UcViewer.CambiarDock, AddressOf CambiarDock

                    RemoveHandler UcViewer.ShowAssociatedWFbyDocId, AddressOf ShowTaskResult
                    AddHandler UcViewer.ShowAssociatedWFbyDocId, AddressOf ShowTaskResult

                    UcViewer.Name = AsociatedResult.Name
                    UcViewer.Tag = AsociatedResult.ID
                    FillContentText(UcViewer)

                    TabViewers.TabPages.Add(UcViewer)
                    UcViewer.ShowDocument(True, False, False, False, True)
                    TabViewers.SelectedTab = UcViewer
                Else
                    TabViewers.SelectTab(Viewer)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Friend Sub ShowTabOfInsertedForm(IDOfInsertedForm As Integer)

            'For Each tab As TabPage In Me.TabViewers.TabPages

            '    If True Then
            '    End If

            'Next


            TabViewers.SelectTab(IDOfInsertedForm)
        End Sub

        Public Sub ShowGridTab()
            TabViewers.SelectTab(tabgrid)
        End Sub

#End Region

#Region "Split"


        Public Sub Split(ByVal Viewer As System.Windows.Forms.TabPage, ByVal Splited As Boolean) Implements IViewerContainer.Split
            Try
                If Not Splited Then
                    TabSecondaryTask.TabPages.Remove(Viewer)
                    TabViewers.TabPages.Add(Viewer)
                    TabViewers.SelectedTab = Viewer

                    If TabSecondaryTask.TabPages.Count = 0 Then
                        SplitTasks.Panel2Collapsed = True
                        SplitTasks.SplitterDistance = SplitTasks.Width
                    End If
                Else
                    TabViewers.TabPages.Remove(Viewer)
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
        Private Shadows Property Name As String Implements IViewerContainer.Name
            Get
                Return MyBase.Name
            End Get
            Set(value As String)
                MyBase.Name = value
            End Set
        End Property
#End Region

        Private Sub SaveGridSplitterPosition(ByVal sender As Object, ByVal e As System.Windows.Forms.SplitterEventArgs)
            If splitterMoved Then
                UserPreferences.setValue("DocPanelHeight", e.SplitY, UPSections.UserPreferences)
                splitterMoved = False
            End If
        End Sub
        Private Sub SaveSplitterMovementsFlag(ByVal sender As Object, ByVal e As System.Windows.Forms.SplitterCancelEventArgs)
            splitterMoved = True
        End Sub

        ''' <summary>
        ''' Se va a insertar un documento estableciendo una relacion con el documento que lo invoco
        ''' </summary>
        ''' <param name="result"></param>
        ''' <param name="relationid"></param>
        ''' <remarks></remarks>
        ''' <history>Diego 31-7-2008 Created</history>
        Public Sub AddRelationedResult(ByVal result As Result, ByVal relationid As Int32)
            Try
                'Dim d1 As New MainPanel.DLoadIndexerWithDocRelation(AddressOf InsertarCarpetaLoadIndexerWithRelationedDocument)
                'Me.Invoke(d1, result, relationid)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
#Region "Utilizado por el Patron Observador"
        Dim iplugin As Form2
        Private Sub editarPag(ByRef R As Result)
            Cursor = Cursors.WaitCursor
            Try
                If IsNothing(iplugin) Then
                    iplugin = New Form2
                    iplugin.Show()
                    AddHandler iplugin.Save, AddressOf Save
                    AddHandler iplugin.CloseDocument, AddressOf Close
                    '    AddHandler iplugin.Update, AddressOf Update
                End If

                If Not IsNothing(R) Then
                    iplugin.initialize(R)
                    iplugin.play()
                End If

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Cursor = Cursors.Default
        End Sub


        Public Sub Save(ByRef r As Object)
            Dim path As String = DirectCast(r, Result).FullPath
            File.Copy(path, path, True)
        End Sub
        'Public Sub Update(ByVal r As Object)
        '    Dim path As String = DirectCast(r, Result).FullPath
        '    System.IO.File.Copy(path, path, True)
        'End Sub
        Public Shadows Sub Close(ByRef o As Object)
            Try
                iplugin.Close()
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
#End Region


        Public Property LocalResult() As Result
            Get
                Return _selectedresult
            End Get
            Set(ByVal Value As Result)
                _selectedresult = Value
            End Set
        End Property

        ''' <remarks></remarks>
        ''' <history>
        ''' </history>
        '''[pablo] sobrecarga del metodo para que pueda ser utilizado cuando se seleccionan varios documentos desde la regla DoForo
        '''
        Private Sub sendMailFromForum(ByVal _taskResults As Generic.List(Of ITaskResult), ByRef textFor As String, ByRef textSubject As String, ByRef textBody As String, ByVal AutomaticLink As Boolean, ByVal AutomaticSend As Boolean, ByVal attachPaths() As String)
            Dim SelectedResults As New Generic.List(Of IResult)
            Dim SelectedResultsaux As New Generic.List(Of IResult)

            For Each r As TaskResult In _taskResults
                SelectedResults.Add(r)
            Next

            Try
                ' [AlejandroR] - 05/03/2010 - Created
                ' Se chequea antes de enviar el mail que se tenga configurada y con acceso
                ' la ruta para el historial de emails
                MessagesBusiness.CheckHistoryExportPath()
            Catch ex As Exception
                UcForo.closeNuevoMensaje()
                MessageBox.Show(ex.Message, "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ZClass.raiseerror(ex)
                Exit Sub
            End Try

            For Each r As Result In SelectedResults
                Try
                    If r.ISVIRTUAL Then
                        Try
                            If Boolean.Parse(UserPreferences.getValue("PreviewFormInForum", UPSections.UserPreferences, "True")) Then
                                Dim prvfrm As New PreviewForm(r, "Adjuntar")

                                If prvfrm.ShowDialog = DialogResult.OK Then
                                    r.Html = prvfrm.frmbrowser.GetHtml

                                    r.HtmlFile = Tools.EnvironmentUtil.GetTempDir("\OfficeTemp").FullName & "\" & r.Name & "-temp" & (New Random).Next(1, 9999).ToString & ".html"
                                    Dim form As ZwebForm = FormBusiness.GetShowAndEditForms(r.DocType.ID)(0)
                                    If File.Exists(form.Path.Replace(".html", ".mht")) Then
                                        Try
                                            Using write As New StreamWriter(r.HtmlFile.Substring(0, r.HtmlFile.Length - 4) & "mht")
                                                write.AutoFlush = True
                                                Dim reader As New StreamReader(form.Path.Replace(".html", ".mht"))
                                                Dim mhtstring As String = reader.ReadToEnd()
                                                write.Write(mhtstring.Replace("<Zamba.Html>", r.Html))
                                            End Using
                                        Catch ex As Exception
                                            ZClass.raiseerror(ex)
                                        End Try

                                        r.HtmlFile = r.HtmlFile.Substring(0, r.HtmlFile.Length - 4) & "mht"
                                    Else
                                        Try
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

            'If Membership.MembershipHelper.CurrentUser.eMail.Type = MailTypes.OutLookMail Then
            'Dim frmMessage As OutlookMessageForm

            'frmMessage = Zamba.AdminControls.clsMessages.getOutlookMailForm(SelectedResults, textFor, textSubject, textBody)
            'ResulEnvio = frmMessage.ShowDialog()

            'Else
            Dim frmMessage As IZMessageForm
            ' Se obtiene el formulario de mail y se muestra 
            frmMessage = clsMessages.getMailFormFromForum(SelectedResults, textFor, textSubject, textBody)
            If AutomaticLink = True Then
                Dim links As New ArrayList
                links.Add(True)
                frmMessage.EspecificarDatos(textFor, String.Empty, String.Empty, textSubject, textBody, Nothing, Nothing, AutomaticSend, links)
            Else
                frmMessage.EspecificarDatos(textFor, String.Empty, String.Empty, textSubject, textBody, Nothing, Nothing, AutomaticSend)
            End If
            'End If
            If AutomaticSend = False Then
                ResulEnvio = DirectCast(frmMessage, Form).ShowDialog()
            Else
                ResulEnvio = True
            End If

        End Sub

#Region "MainToolBar"

        Private Sub toolbar_Click(ByVal sender As Object, ByVal e As ToolStripItemClickedEventArgs) Handles toolbar.ItemClicked

            Dim WFTB As New WFTaskBusiness
            Try
                Select Case CStr(e.ClickedItem.Tag)
                    Case "EMAIL"
                        RaiseEvent sendMail(UCFusion2.SelectedResultsList)
                        refreshHistorialEmails()
                    Case "IMPRIMIR"
                        Imprimir(UCFusion2.SelectedResults(False, UCFusion2.GetSelectedRows()), Print.LoadAction.ShowForm)
                    Case "PREVISUALIZAR"
                        Imprimir(UCFusion2.SelectedResults(False, UCFusion2.GetSelectedRows()), Print.LoadAction.ShowPreview)
                    'Case "HISTORIAL"
                    '    _Historial()
                    Case "DOCUMENTOSASOCIADOS"
                        VerDocumentosAsociados(UCFusion2.SelectedResults(True, UCFusion2.GetSelectedRows())(0))
                    Case "VERSIONESDELDOCUMENTO"
                        ShowVersions(UCFusion2.SelectedResults(True, UCFusion2.GetSelectedRows())(0))
                    Case "AGREGARNUEVAVERSION"
                        AddNewVersion(UCFusion2.SelectedResults(True, UCFusion2.GetSelectedRows())(0))
                    '  Case "MOVERCOPIAR"
                    '_MoverCopiarDocumento()
                    Case "CLOSETABS"
                        CloseAllDocumentViewerTabs()
                    Case "AGREGARACARPETA"
                        RaiseEvent AgregarACarpeta(UCFusion2.SelectedResults(0, UCFusion2.GetSelectedRows())(0))
                    Case "GUARDARDOCUMENTOCOMO"
                        Try
                            GuardarComo(UCFusion2.SelectedResults(False, UCFusion2.GetSelectedRows()))
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try
                    Case "IRAWORKFLOW"
                        Try
                            If (UCFusion2.SelectedResults(False, UCFusion2.GetSelectedRows()).Count > 0) Then
                                Dim Result As IResult = UCFusion2.SelectedResults(True, UCFusion2.GetSelectedRows())(0)
                                Dim Task As ITaskResult = WFTB.GetTaskByDocId(Result.ID)
                                If Not IsNothing(Task) Then
                                    If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.WFSteps, RightsType.Use, Task.StepId) = True Then
                                        RaiseEvent ShowTask(Task.TaskId, Task.StepId, Task.DocTypeId)
                                    Else
                                        MessageBox.Show("No tiene permiso de visualizar la etapa en la que se encuentra el documento", "Atencion", MessageBoxButtons.OK)
                                    End If
                                Else
                                    MessageBox.Show("El documento no se encuentra en ningun Workflow", "Atencion", MessageBoxButtons.OK)
                                End If
                            End If
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try
                    Case "CHANGEPOSITION"
                        ChangeGridLocation(True)
                    Case "VERFORO"
                        If UCFusion2.SelectedResults(False, UCFusion2.GetSelectedRows()).Count > 0 Then
                            TabViewers.SelectTab(TabForo)
                        End If
                    Case "ENVIARZIP"
                        Try
                            EnviarZip(UCFusion2.SelectedResultsList)
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try
                    Case "SIZEUP"
                        UCFusion2.SetFontSizeUp()
                    Case "SIZEDOWN"
                        UCFusion2.SetFontSizeDown()
                End Select
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                WFTB = Nothing
            End Try
        End Sub

#End Region

        Public Sub ShowProperResult(ByRef result As Result)
            If TypeOf result Is ITaskResult Then
                ShowTaskResult(result.ID, result.DocTypeId)
            Else
                ShowResult(result)
            End If

        End Sub

        Private Sub ShowTaskResult(ByVal docId As Int64, ByVal docTypeId As Int64)
            Dim WFTB As New WFTaskBusiness
            Dim Task As ITaskResult = WFTB.GetTaskByDocId(docId)
            If Not IsNothing(Task) Then
                If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.WFSteps, RightsType.Use, Task.StepId) = True Then
                    RaiseEvent ShowTask(Task.TaskId, Task.StepId, Task.DocTypeId)
                Else
                    MessageBox.Show("No tiene permiso de visualizar la etapa en la que se encuentra el documento", "Atencion", MessageBoxButtons.OK)
                End If
            Else
                MessageBox.Show("El documento no se encuentra en ningun Workflow", "Atencion", MessageBoxButtons.OK)
            End If
            WFTB = Nothing
        End Sub

        Public Function GetSelectedResults() As List(Of IResult) Implements IMenuContextContainer.GetSelectedResults
            Return UCFusion2.SelectedResultsList()
        End Function

        Public Sub RefreshResults() Implements IMenuContextContainer.RefreshResults

        End Sub
    End Class
End Namespace