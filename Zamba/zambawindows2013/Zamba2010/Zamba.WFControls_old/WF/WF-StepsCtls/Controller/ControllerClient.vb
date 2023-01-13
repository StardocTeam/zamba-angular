Imports Zamba.Controls.WF.TasksCtls
Imports Zamba.Core

Public Class Controller
    Inherits ZControl
    Implements IDisposable

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then components.Dispose()
            If UCTaskGrid IsNot Nothing Then UCTaskGrid.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

#Region " Código generado por el Diseñador de Windows Forms "

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.


    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Public WithEvents tabResults As WF.ResultsCtls.TabResults
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        SuspendLayout()

        '
        'Controller
        '
        BackColor = System.Drawing.Color.White
        Name = "Controller"
        Size = New System.Drawing.Size(568, 464)
        ResumeLayout(False)

    End Sub

#End Region

    Public Shared selectedTaskIds As New List(Of Integer)


    Dim CurrentUserId As Int64
    Public Sub New(ByVal CurrentUserId As Int64)
        MyBase.New()
        Me.CurrentUserId = CurrentUserId
        InitializeComponent()
        LoadGrids()
    End Sub

#Region "Grid"

    Public WithEvents UCTaskGrid As UCTaskGrid

    Private Sub LoadGrids()

        Try
            tabResults = New WF.ResultsCtls.TabResults(CurrentUserId)
            RemoveHandler tabResults.currentContextMenu.ItemClicked, AddressOf currentContextMenuClick
            AddHandler tabResults.currentContextMenu.ItemClicked, AddressOf currentContextMenuClick
            RemoveHandler tabResults.currentContextMenuItemClicked, AddressOf currentContextMenuClick
            AddHandler tabResults.currentContextMenuItemClicked, AddressOf currentContextMenuClick

            tabResults.Dock = DockStyle.Fill

            Controls.Add(tabResults)

            UCTaskGrid = New UCTaskGrid(CurrentUserId)
            RemoveHandler UCTaskGrid.currentContextMenu.ItemClicked, AddressOf currentContextMenuClick
            AddHandler UCTaskGrid.currentContextMenu.ItemClicked, AddressOf currentContextMenuClick

            UCTaskGrid.Dock = DockStyle.Fill

            Controls.Add(UCTaskGrid)
            UCTaskGrid.BringToFront()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub

    Public Event currentContextMenuItemClicked(Action As String, listResults As List(Of IResult), ContextMenuContainer As IMenuContextContainer)
    Private Sub currentContextMenuClick(Action As String, listResults As List(Of IResult), ContextMenuContainer As IMenuContextContainer)
        RaiseEvent currentContextMenuItemClicked(Action, listResults, ContextMenuContainer)
    End Sub

#End Region

#Region "Viewer"


    Private Delegate Sub LoadHistoryDelegate(ByVal TaskId As Int64)
    Private Delegate Sub TaskHistoryLoadHistoryDelegate(ByVal TaskId As Int64)
    Private Delegate Sub showResultsOfRequestActionDelegate(ByRef TaskId As Int64)

    ' [AlejandroR] 21/12/09 - Created
    Private _SelectedTask As TaskResult

#End Region

#Region "InertButton"
    Public Event ShowDocuments()
    Private Sub InertButton2_Click(ByVal sender As System.Object, ByVal e As EventArgs)
        RaiseEvent ShowDocuments()
    End Sub

    Public Sub ShowResultsGrid()
        UCTaskGrid.Visible = False
        tabResults.Visible = True
    End Sub

    Public Sub ShowTaskGrid()
        tabResults.Visible = False
        UCTaskGrid.Visible = True
    End Sub

    Public Function GetResultsSelected() As Generic.List(Of IResult)
        Try
            If tabResults.Visible Then
                Return tabResults.UCFusion2.SelectedResultsList()
            Else
                Return UCTaskGrid.SelectedResultsList()
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Function

#End Region

End Class
