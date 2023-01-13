
Imports Zamba.Core.WF.WF
Imports Zamba.Core

Namespace WF.TasksCtls
    ' Para WFTaskBusiness

    ''' -----------------------------------------------------------------------------
    ''' Project	 : Zamba.WFControls
    ''' Class	 : UCTaskRequestAction
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Control de usuario que contiene el historial de los tasks seleccionados que ejecutaron RequestAction
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    '''     [Gaston]	Aprox. 18/07/2008	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------

    Public Class UCTaskRequestAction

#Region "Constructor"
        Dim CurrentUserId As Int64
        Public Sub New(ByVal CurrentUserId As Int64)
            _fc = New Filters.FiltersComponent
            Me.InitializeComponent()
            Me.CurrentUserId = CurrentUserId
        End Sub

#End Region

#Region "Eventos"



#End Region

#Region "Métodos"

        ''' <summary>
        ''' Carga el historial de una o más tareas (RequestAction) en grdGeneral (para los taskids que ejecutaron 
        ''' alguna vez el requestAction)
        ''' </summary>
        ''' <param name="taskIds"></param>
        ''' <remarks></remarks>
        Public Sub loadRequestActionHistory(ByVal taskId As Int64)
            Try
                If (taskId = 0) Then
                    updateGrdGeneral(Nothing)
                Else
                    Dim ds As DataSet = WFTaskBusiness.getTasksRequestActionHistory(taskId)
                    If Not (IsNothing(ds)) Then

                        If (ds.Tables(0).Rows.Count > 0) Then
                            updateGrdGeneral(ds.Tables(0))
                        Else
                            updateGrdGeneral(Nothing)
                        End If

                    End If

                End If

            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try

        End Sub

        ''' <summary>
        ''' Método que sirve para actualizar el grdGeneral
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub updateGrdGeneral(ByRef table As DataTable)

            grdGeneral.DataSource = table
            'grdGeneral.OriginalDataTable = table

            If (table IsNot Nothing) Then
                grdGeneral.FixColumns()
            End If

            grdGeneral.Update()
            grdGeneral.Refresh()

        End Sub

#End Region

    End Class
End Namespace