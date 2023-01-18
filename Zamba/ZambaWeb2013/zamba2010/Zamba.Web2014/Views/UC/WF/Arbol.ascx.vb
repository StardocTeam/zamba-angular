Imports Zamba.Core

Partial Public Class Arbol
    Inherits System.Web.UI.UserControl
    Implements IArbol

    ''' <summary>
    ''' Una vez finalizado el refresco del arbol de wf se eleva este evento para actualizar la grilla
    ''' </summary>
    ''' <param name="StepId"></param>
    ''' <remarks></remarks>
    Public Event WFTreeRefreshed(ByVal StepId As Int32)
    Public Event WFTreeIsEmpty()
    Public Event SelectedNodeChanged(ByVal WFId As Int32, ByVal StepId As Int32, ByVal DocTypeId As Int32)
    Private Presenter As Presenters.Arbol

    Public Sub Arbol()
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not IsNothing(Session("User")) Then
            Presenter = New Presenters.Arbol(Session("UserId"), Me, Session("User"))
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsNothing(Session("User")) Then
            btnInsertar.Visible = UserBusiness.Rights.GetUserRights(Zamba.Core.ObjectTypes.InsertWeb, Zamba.Core.RightsType.View)
            If btnInsertar.Visible Then
                Presenter.FillCmbFormTypes()
            End If
        End If

        Dim script As String = "$(document).ready(function() { FixWfTree(); AddStepCountHandler(); });"
        ScriptManager.RegisterClientScriptBlock(Me, Me.[GetType](), "WfTreeScripts", script, True)
    End Sub

    Public Sub Refresh()
        Try
            Dim LastWFStepUsed As String = UserPreferences.getValue("UltimoWFStepUtilizado", Sections.WorkFlow, "")
            Dim WFId As Int32
            Dim stepID As Int32

            If String.IsNullOrEmpty(LastWFStepUsed) = False Then
                Presenter.getWorkflowAndStepSelected(WFId, LastWFStepUsed)
                SelectStepNode(LastWFStepUsed)

                If ArbolProcesos.Nodes.Count > 0 Then
                    Dim nodeselected As Boolean = False
                    For Each nodeMain As TreeNode In ArbolProcesos.Nodes(0).ChildNodes

                        If nodeMain.Value = LastWFStepUsed Then
                            nodeMain.Select()
                            RaiseEvent SelectedNodeChanged(WFId, LastWFStepUsed, 0)
                            Exit For
                        End If

                        For Each NodeChild As TreeNode In nodeMain.ChildNodes
                            If NodeChild.Value = LastWFStepUsed Then
                                nodeMain.Expand()
                                NodeChild.Select()
                                RaiseEvent SelectedNodeChanged(WFId, LastWFStepUsed, 0)
                                nodeselected = True
                                Exit For
                            End If
                        Next
                        If nodeselected Then Exit For
                    Next



                End If
            Else
                Presenter.getWorkflowAndStepSelected(WFId, stepID)
                RaiseEvent SelectedNodeChanged(WFId, stepID, 0)
            End If
        Catch ex As Exception
            ZCore.raiseerror(ex)
        End Try
    End Sub

    Private Sub RefreshTree()
        Try
            Presenter.FillWF()

            Dim LastWFStepUsed As String = UserPreferences.getValue("UltimoWFStepUtilizado", Sections.WorkFlow, "")
            Dim WFId As Int32

            Presenter.getWorkflowAndStepSelected(WFId, LastWFStepUsed)

            If ArbolProcesos.Nodes.Count > 0 Then
                SelectStepNode(LastWFStepUsed)
                RaiseEvent WFTreeRefreshed(LastWFStepUsed)
            End If
        Catch ex As Exception
            ZCore.raiseerror(ex)
        End Try
    End Sub

    Private Sub SelectStepNode(ByVal stepId As Int64)
        ArbolProcesos.Nodes(0).Expand()

        For Each nodeMain As TreeNode In ArbolProcesos.Nodes(0).ChildNodes

            If nodeMain.Value = stepId Then
                nodeMain.Select()
            End If

            For Each NodeChild As TreeNode In nodeMain.ChildNodes
                If NodeChild.Value = stepId Then
                    nodeMain.Expand()
                    NodeChild.Select()
                End If
            Next
        Next
    End Sub

    Public Sub FillWF()
        Try
            Presenter.FillWF()

            'Si el arbol no posee nodos, se dispara el evento para que no muestre el control
            If ArbolProcesos.Nodes.Count = 0 Then
                RaiseEvent WFTreeIsEmpty()
                Return
            End If

            If IsNothing(ArbolProcesos.SelectedNode) Then
                Refresh()
            End If
        Catch ex As Exception
            ZCore.raiseerror(ex)
        End Try
    End Sub

    Public Property WFTreeView() As TreeView Implements IArbol.WFTreeView
        Get
            Return Me.ArbolProcesos
        End Get
        Set(ByVal value As TreeView)
            Me.ArbolProcesos = value
        End Set
    End Property

    'Protected Sub btncontraer_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnContraer.Click
    '    Presenter.CollapseTreeView()
    'End Sub

    Public Property CmbFormType() As System.Web.UI.WebControls.DropDownList Implements IArbol.CmbFormType
        Get
            Return Me.ddltipodeform
        End Get
        Set(ByVal value As System.Web.UI.WebControls.DropDownList)
            Me.ddltipodeform = value
        End Set
    End Property

    Protected Sub BtnRefresh_Click(ByVal sender As Object, ByVal e As EventArgs)
        RefreshTree()
    End Sub

    Protected Sub ArbolProcesos_SelectedNodeChanged(sender As Object, e As EventArgs)
        Dim wfId, stepId As Int32
        Presenter.getWorkflowAndStepSelected(wfId, stepId)
        SelectStepNode(stepId)

        RaiseEvent SelectedNodeChanged(wfId, stepId, 0)
        RaiseEvent WFTreeRefreshed(stepId)
    End Sub
End Class
