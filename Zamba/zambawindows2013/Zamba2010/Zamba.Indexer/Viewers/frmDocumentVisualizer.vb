Imports Zamba.Core
Imports Zamba.Viewers
Imports Zamba.Core.DocTypes.DocAsociated

Public Class frmDocumentVisualizer
    Implements IViewerContainer

    Dim TabDocAsociated As TabPage
    Dim localResult As ITaskResult
    Dim grdDocsAsoc As UCFusion2

    ''' <summary>
    ''' Formulario que muestra un control que permite visualizar los documentos y sus asociados
    ''' </summary>
    ''' <param name="Title">Titulo del Formulario</param>
    ''' <param name="TitleTab">Titulo del Tab Principal</param>
    ''' <param name="ctrl">Control a visualizar</param>
    ''' <param name="resultOrig">Result de la tarea</param>
    ''' <param name="showAsoc">Si se desea ver la grilla de asociados</param>
    ''' <param name="showOriginal">Si se desea ver el tab con el documento original</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal Title As String, ByVal TitleTab As String, ByVal ctrl As Control, ByVal result As ITaskResult, ByVal showAsoc As Boolean, ByVal showOriginal As Boolean)
        InitializeComponent()

        Text = Title
        tpPrincipal.Text = TitleTab

        localResult = result
        'Agrego el control del usuario
        If Not IsNothing(ctrl) Then
            ctrl.Dock = DockStyle.Fill
            tpPrincipal.Controls.Add(ctrl)

            'DoMail
            If String.Compare(ctrl.GetType.Name, "ctrlLotusMessageSend") = 0 Then
                If ctrl.Controls.Item("Panel2").Controls.Item("BtnExecuteAdditionalRule").Text <> "" Or _
                    ctrl.Controls.Item("Panel2").Controls.Item("BtnExecuteRule").Text <> "" Then
                    Size = New System.Drawing.Point(650, 680)
                Else
                    Size = New System.Drawing.Point(650, 650)
                End If

            End If
        End If

        'Muestro el documento original
        If Not IsNothing(result) AndAlso showOriginal = True Then
            Dim tbOrig As UCDocumentViewer2

            If Not IsNothing(result.FullPath) Then
                tbOrig = New UCDocumentViewer2(Me, result, True, False, Nothing, False, True, False)
            Else
                tbOrig = New UCDocumentViewer2(Me, result, True, False, Nothing, False, True, True)
            End If


            tbOrig.Text = "Documento Original"
            tbPages.TabPages.Add(tbOrig)
        End If

        'Muestro la grilla de asociados
        If Not IsNothing(result) AndAlso showAsoc = True Then
            TabDocAsociated = New ZTabPage("Asociados")
            TabDocAsociated.Name = "TabDocAsociated"
            grdDocsAsoc = New UCFusion2(UCFusion2.Modes.AsociatedResults, Membership.MembershipHelper.CurrentUser.ID, localResult, Nothing)
            TabDocAsociated.Controls.Add(grdDocsAsoc)
            tbPages.TabPages.Add(TabDocAsociated)
            grdDocsAsoc.Dock = DockStyle.Fill

            RemoveHandler grdDocsAsoc.ResultDoubleClick, AddressOf ShowAsociatedResult
            AddHandler grdDocsAsoc.ResultDoubleClick, AddressOf ShowAsociatedResult
            RemoveHandler grdDocsAsoc._RefreshGrid, AddressOf refreshAsoc
            AddHandler grdDocsAsoc._RefreshGrid, AddressOf refreshAsoc
        End If
    End Sub

    ''' <summary>
    ''' Muestra el documento asociado
    ''' </summary>
    ''' <param name="AsociatedResult">Result a mostrar</param>
    ''' <remarks></remarks>
    Private Sub ShowAsociatedResult(ByRef AsociatedResult As Result)
        If Not IsNothing(AsociatedResult) Then
            Dim tbDoc As UCDocumentViewer2 = GetViewerByResult(AsociatedResult)

            If IsNothing(tbDoc) Then
                tbDoc = New UCDocumentViewer2(Me, AsociatedResult, True, False, Nothing, False, True)
                tbDoc.Text = AsociatedResult.Name

                RemoveHandler tbDoc.ShowAsociatedResult, AddressOf ShowAsociatedResult
                AddHandler tbDoc.ShowAsociatedResult, AddressOf ShowAsociatedResult

                tbPages.TabPages.Add(tbDoc)
            End If

            tbPages.SelectTab(tbDoc)
        End If
    End Sub

    ''' <summary>
    ''' Recarga la grilla de asociados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub refreshAsoc()
        If Not IsNothing(localResult) And Not IsNothing(TabDocAsociated) AndAlso TabDocAsociated.Controls.Count > 0 Then

            Dim asocResults As DataTable = DocAsociatedBusiness.getAsociatedDTResultsFromResult(localResult, 0, False, Nothing)
            Dim grdDocsAsoc As UCFusion2 = DirectCast(TabDocAsociated.Controls(0), UCFusion2)
            grdDocsAsoc.ClearSearchs()
            grdDocsAsoc.FillResults(asocResults, Nothing)

            grdDocsAsoc.Refresh()
            grdDocsAsoc.Update()
        End If
    End Sub

    ''' <summary>
    ''' Obtiene el tab si existe
    ''' </summary>
    ''' <param name="Result"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetViewerByResult(ByVal Result As Result) As UCDocumentViewer2
        For Each dc As TabPage In tbPages.TabPages
            If TypeOf dc Is UCDocumentViewer2 Then
                Dim zvc As UCDocumentViewer2 = DirectCast(dc, UCDocumentViewer2)
                If zvc.Result.ID = Result.ID Then
                    Return zvc
                End If
            End If
        Next
        Return Nothing
    End Function

    '''Se mantiene por compatibilidad
#Region "Split"
    Public Sub Split(ByVal Viewer As System.Windows.Forms.TabPage, ByVal Splited As Boolean) Implements Core.IViewerContainer.Split
        Try

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

    'Private Sub tbPages_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbPages.SelectedIndexChanged
    '    If Not IsNothing(TabDocAsociated) AndAlso tbPages.SelectedIndex = 1 Then
    '        If grdDocsAsoc.GridView.OutLookGrid.Rows.Count = 0 Then
    '            grdDocsAsoc.inicializarGrilla()
    '            refreshAsoc()
    '        End If
    '    End If
    'End Sub

    Private Sub tbPages_TabIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles tbPages.SelectedIndexChanged
        If String.Compare(tbPages.SelectedTab.Name, "TabDocAsociated") = 0 Then
            Dim asocResults As DataTable = DocAsociatedBusiness.getAsociatedDTResultsFromResult(localResult, 0, False, Nothing)
            Dim grdDocsAsoc As UCFusion2 = DirectCast(TabDocAsociated.Controls(0), UCFusion2)
            grdDocsAsoc.ClearSearchs()
            grdDocsAsoc.FillResults(asocResults, Nothing)

            grdDocsAsoc.Refresh()
            grdDocsAsoc.Update()
        End If
    End Sub
End Class