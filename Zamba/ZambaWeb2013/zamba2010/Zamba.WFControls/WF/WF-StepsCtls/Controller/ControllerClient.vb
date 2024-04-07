Imports Zamba.Controls.WF.TasksCtls
Imports Zamba.Controls.WF.UInterface.Client
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

#Region " C�digo generado por el Dise�ador de Windows Forms "

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.


    'Requerido por el Dise�ador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Dise�ador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Dise�ador de Windows Forms. 
    'No lo modifique con el editor de c�digo.
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
    Public ReadOnly Property UCTasksPanel As UCTasksPanel

    Public Sub New(ByVal CurrentUserId As Int64, UCTasksPanel As UCTasksPanel)
        MyBase.New()
        Me.CurrentUserId = CurrentUserId
        Me.UCTasksPanel = UCTasksPanel
        InitializeComponent()
        LoadGrids()
    End Sub

#Region "Grid"

    Public WithEvents UCTaskGrid As UCTaskGrid

    Private Sub LoadGrids()

        Try
            tabResults = New WF.ResultsCtls.TabResults(CurrentUserId, Me)


            tabResults.Dock = DockStyle.Fill

            Controls.Add(tabResults)

            UCTaskGrid = New UCTaskGrid(CurrentUserId, Me)


            UCTaskGrid.Dock = DockStyle.Fill

            Controls.Add(UCTaskGrid)
            UCTaskGrid.BringToFront()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub

    ' Public Event currentContextMenuItemClicked(Action As String, listResults As List(Of IResult), ContextMenuContainer As IMenuContextContainer)
    Public Sub currentContextMenuClick(Action As String, listResults As List(Of IResult), ContextMenuContainer As IMenuContextContainer)
        UCTasksPanel.currentContextMenuClick(Action, listResults, ContextMenuContainer)
    End Sub

#End Region

#Region "Viewer"


    Private Delegate Sub LoadHistoryDelegate(ByVal TaskId As Int64)
    Private Delegate Sub TaskHistoryLoadHistoryDelegate(ByVal TaskId As Int64)
    Private Delegate Sub showResultsOfRequestActionDelegate(ByRef TaskId As Int64)

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