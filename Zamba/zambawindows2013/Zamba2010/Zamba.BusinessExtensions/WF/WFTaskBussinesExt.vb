
Imports Zamba.Filters

Namespace WF

    Public Class WfTaskBussinesExt
        Implements IGrid

#Region "Atributos"
        Private _fc As New FiltersComponent
        Private _lastPage As Integer

        Public Property Exporting As Boolean Implements IGrid.Exporting
            Get

            End Get
            Set(value As Boolean)

            End Set
        End Property

        Public Property ExportSize As Integer Implements IGrid.ExportSize
            Get

            End Get
            Set(value As Integer)

            End Set
        End Property
#End Region
#Region "propiedades"
        Public Property FC() As IFiltersComponent Implements IGrid.Fc
            Get
                Return _fc
            End Get
            Set(ByVal value As IFiltersComponent)
                _fc = value
            End Set
        End Property

        Public Property FiltersChanged As Boolean Implements IFilter.FiltersChanged
            Get

            End Get
            Set(value As Boolean)

            End Set
        End Property

        Public Property LastPage() As Integer Implements IGrid.LastPage
            Get
                Return _lastPage
            End Get
            Set(ByVal value As Integer)
                _lastPage = value
            End Set
        End Property

        Public Property PageSize As Integer Implements IGrid.PageSize
            Get

            End Get
            Set(value As Integer)

            End Set
        End Property

        Public Property SaveSearch As Boolean Implements IGrid.SaveSearch
            Get
            End Get
            Set(value As Boolean)
            End Set
        End Property

        Public Property SortChanged As Boolean Implements IOrder.SortChanged

        Public Property FontSizeChanged As Boolean Implements IGrid.FontSizeChanged

        Public Sub AddOrderComponent(orderString As String) Implements IOrder.AddOrderComponent

        End Sub
        Public Sub AddGroupByComponent(v As String) Implements IGrid.AddGroupByComponent

        End Sub
        Public Sub ShowTaskOfDT() Implements IGrid.ShowTaskOfDT

        End Sub


#End Region
        Public Function GetTaskCount(ByVal stepId As Int64, ByVal withGridRights As Boolean, ByVal currentUserId As Int64) As Int64
            Dim dt As DataTable = WFStepBusiness.GetDocTypesByWfStepAsDT(stepId, True)
            Dim count As Int64 = 0
            Dim wftb As New WF.WFTaskBusiness
            For Each r As DataRow In dt.Rows
                'Verifica que no sea el valor "0 - Todas las entidades"
                If r(0) <> 0 Then
                    Dim DTcount As DataTable = wftb.GetTasksByStepandDocTypeId(stepId, r(0), withGridRights, currentUserId, Nothing, LastPage, 0, SearchType.WFStepCount, String.Empty)
                    If Not IsNothing(DTcount) AndAlso DTcount.Rows.Count > 0 Then
                        count = count + DTcount.Rows(0)(0)
                    End If
                End If
            Next
            wftb = Nothing
            Return count
        End Function
        Public Function GetWFTaskCount(ByVal WorkFlowId As Int64, stepid As Int64, ByVal withGridRights As Boolean, ByVal currentUserId As Int64) As Int64
            Dim dt As DataTable = WFStepBusiness.GetDocTypesByWfStepAsDT(stepid, True)
            Dim count As Int64 = 0
            Dim wftb As New WF.WFTaskBusiness
            For Each r As DataRow In dt.Rows
                'Verifica que no sea el valor "0 - Todas las entidades"
                If r(0) <> 0 Then
                    Dim DTcount As DataTable = wftb.GetTasksByWFandDocTypeId(WorkFlowId, stepid, r(0), withGridRights, currentUserId, Nothing, LastPage, 0, SearchType.WFStepCount, Nothing)
                    If Not DTcount Is Nothing AndAlso DTcount.Rows.Count > 0 Then
                        count = count + DTcount.Rows(0)(0)
                    End If
                End If
            Next
            wftb = Nothing
            Return count
        End Function

    End Class
End Namespace