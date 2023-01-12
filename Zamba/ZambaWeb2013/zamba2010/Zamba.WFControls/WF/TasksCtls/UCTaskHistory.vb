Imports Zamba.Core.WF.WF
Imports Zamba.Grid.Grid
Imports Zamba.Filters
Imports Zamba.Core

Namespace WF.TasksCtls

    Public Class UCTaskHistory
        Inherits ZControl
        Implements IGrid

#Region " Código generado por el Diseñador de Windows Forms "
        Dim CurrentUserId As Int64
        Private Sub New(ByVal CurrentUserId As Int64)
            MyBase.New()
            Me.CurrentUserId = CurrentUserId

            _fc = New FiltersComponent
            'El Diseñador de Windows Forms requiere esta llamada.
            InitializeComponent()

            'Agregar cualquier inicialización después de la llamada a InitializeComponent()

        End Sub

        'UserControl reemplaza a Dispose para limpiar la lista de componentes.
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

        'Requerido por el Diseñador de Windows Forms
        Private components As System.ComponentModel.IContainer

        'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
        'Puede modificarse utilizando el Diseñador de Windows Forms. 
        'No lo modifique con el editor de código.
        Friend WithEvents grdGeneral As GroupGrid
        'Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
        Friend WithEvents DOC_ID As System.Windows.Forms.DataGridTextBoxColumn
        Friend WithEvents CheckIn As System.Windows.Forms.DataGridTextBoxColumn
        Friend WithEvents CheckOut As System.Windows.Forms.DataGridTextBoxColumn
        Friend WithEvents Etapa As System.Windows.Forms.DataGridTextBoxColumn
        Friend WithEvents EstadoInicial As System.Windows.Forms.DataGridTextBoxColumn
        Friend WithEvents EstadoFinal As System.Windows.Forms.DataGridTextBoxColumn
        Friend WithEvents UCheckin As System.Windows.Forms.DataGridTextBoxColumn
        Friend WithEvents DOCTYPE As System.Windows.Forms.DataGridTextBoxColumn
        Friend WithEvents UCheckOut As System.Windows.Forms.DataGridTextBoxColumn
        <DebuggerStepThrough()> Private Sub InitializeComponent()
            SuspendLayout()
            '
            grdGeneral = New GroupGrid(True, CurrentUserId, Me, FilterTypes.History)
            grdGeneral.BackColor = System.Drawing.Color.White
            grdGeneral.DataSource = Nothing
            grdGeneral.Dock = System.Windows.Forms.DockStyle.Fill
            grdGeneral.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
            grdGeneral.Location = New System.Drawing.Point(0, 0)
            grdGeneral.Name = "GridView"
            grdGeneral.Size = New System.Drawing.Size(912, 306)
            grdGeneral.TabIndex = 0
            grdGeneral.WithExcel = False
            grdGeneral.AllowTelerikGridFilter = True
            grdGeneral.ShowFiltersPanel = False
            '
            'UCTaskHistory
            '
            Name = "UCTaskHistory"
            Size = New System.Drawing.Size(624, 504)
            Controls.Add(grdGeneral)
            ResumeLayout(False)

        End Sub

#End Region
        Private _data As DataTable
        Private _showOnlyIndexsHistory As Boolean
        Dim TaskId As Int64
        Private DocTypeID As Long
        Public Event _RefreshGrid(ByVal ReloadHistory As Boolean)

        Public Sub New(ByVal TaskId As Integer, ByVal CurrentUserId As Int64, ByVal ShowOnlyIndexsHistory As Boolean, ByVal docTypeId As Long)
            Me.New(CurrentUserId)
            Me.TaskId = TaskId
            Me.DocTypeID = docTypeId
            _showOnlyIndexsHistory = ShowOnlyIndexsHistory

            If ShowOnlyIndexsHistory Then
                LoadHistory(TaskId, docTypeId)
            End If
            RemoveHandler grdGeneral.OnRefreshClick, AddressOf refreshGridMethod
            AddHandler grdGeneral.OnRefreshClick, AddressOf refreshGridMethod
        End Sub
        Private Sub refreshGridMethod()
            RaiseEvent _RefreshGrid(True)
        End Sub

        ''' <summary>
        ''' Carga el historial de una tarea en grdGeneral
        ''' </summary>
        ''' <param name="resultId"></param>
        ''' <remarks></remarks>
        Public Sub LoadHistory(ByVal TaskId As Int64, ByVal docTypeId As Int64)
            Try
                LastPage = 0 ' Por ahora la pongo a cero para que borre la grilla.
                grdGeneral.DataSource = Nothing
                grdGeneral.NewGrid.Columns.Clear()
                grdGeneral.NewGrid.DataSource = Nothing
                Dim ds As DataSet
                If _showOnlyIndexsHistory Then
                    ds = WFTaskBusiness.GetOnlyIndexsHistory(TaskId)
                Else
                    ds = WFTaskBusiness.GetTaskHistory(TaskId, docTypeId, Fc)
                End If

                grdGeneral.AlwaysFit = True
                grdGeneral.showRefreshButton = True
                ds.Tables(0).MinimumCapacity = ds.Tables(0).Rows.Count
                grdGeneral.DataSource = ds.Tables(0)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se recargaran los filtros.")
                grdGeneral.ReloadLsvFilters()
                grdGeneral.FixColumns()
                If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                    _data = ds.Tables(0)
                End If
                'grdGeneral.Update()
                'grdGeneral.Refresh()
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End Sub

        ''' <summary>
        ''' [Sebastian] 18-09-09 Show date in short or large format
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub UCTaskHistory_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            Try
                '[Sebastian] this option show in the grid the date in short or larg format
                grdGeneral.ShortDateFormat = Boolean.Parse(UserPreferences.getValue("GridWFHistorialShortDateFormat", UPSections.UserPreferences, "False"))
                grdGeneral.AlwaysFit = True
                grdGeneral.UseZamba = True
                grdGeneral.WithExcel = True
                grdGeneral.AllowTelerikGridFilter = True
                LoadHistory(TaskId, DocTypeID)
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End Sub

        'Private Sub tsbActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '    Try
        '        LoadHistory(TaskId)
        '    Catch ex As Exception
        '        Zamba.Core.ZClass.raiseerror(ex)
        '    End Try
        'End Sub


        'Private Sub ZToolBar1_ItemClicked(ByVal sender As System.Object, ByVal e As ToolStripItemClickedEventArgs)

        'End Sub

        Private _fc As IFiltersComponent

        Public Property Fc() As IFiltersComponent Implements IGrid.Fc
            Get
                Return _fc
            End Get
            Set(ByVal value As IFiltersComponent)
                _fc = value
            End Set
        End Property
        Private _lastPage As Integer


        Public Property LastPage() As Integer Implements IGrid.LastPage
            Get
                Return _lastPage
            End Get
            Set(ByVal value As Integer)
                _lastPage = value
            End Set
        End Property

        Public Property PageSize As Integer Implements IGrid.PageSize


        Public Property Exporting As Boolean Implements IGrid.Exporting


        Public Property ExportSize As Integer Implements IGrid.ExportSize

        Public Property SaveSearch As Boolean Implements IGrid.SaveSearch
            Get
            End Get
            Set(value As Boolean)
            End Set
        End Property

        Public Property SortChanged As Boolean Implements IOrder.SortChanged

        Private _filtersChanged As Boolean

        Public Property FiltersChanged As Boolean Implements IFilter.FiltersChanged
            Get
                Return _filtersChanged
            End Get
            Set(value As Boolean)
                _filtersChanged = value
            End Set
        End Property

        Public Property FontSizeChanged As Boolean Implements IGrid.FontSizeChanged

        Private Sub ShowTaskOfDT() Implements IGrid.ShowTaskOfDT
            If FiltersChanged Then
                RaiseEvent _RefreshGrid(True)
            ElseIf FontSizeChanged Then
                grdGeneral.DataSource = _data
            End If
        End Sub

        Public Sub AddOrderComponent(orderString As String) Implements IOrder.AddOrderComponent

        End Sub
        Public Sub AddGroupByComponent(GroupByString As String) Implements IGrid.AddGroupByComponent

        End Sub
    End Class
End Namespace