Imports Zamba.Core
Imports Zamba.Services
Imports System.Collections.Generic
Imports Zamba.Core.Enumerators

Partial Public Class TaskDetail
    Inherits System.Web.UI.UserControl

    Private _ITaskResult As Zamba.Core.ITaskResult
    Private TaskId As Integer
    Private _userid As Long
    Private _user As Zamba.Core.IUser

#Region "Properties"

    Public Property TaskResult() As ITaskResult
        Get
            Return _ITaskResult
        End Get
        Set(ByVal value As ITaskResult)
            _ITaskResult = value
        End Set
    End Property

    ''' <summary>
    ''' Evento que ejecuta reglas
    ''' </summary>
    ''' <param name="ruleId">ID de la regla a ejecutar</param>
    ''' <param name="results">Tareas a ejecutar</param>
    ''' <remarks></remarks>
    Public Event ExecuteRule(ByVal ruleId As Int64, ByVal results As List(Of Zamba.Core.ITaskResult))

    Private Sub ThrowExecuteRule(ByVal ruleId As Int64, ByVal results As List(Of Zamba.Core.ITaskResult))
        RaiseEvent ExecuteRule(ruleId, results)
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.Session("UserId") Is Nothing = False AndAlso Not (TaskResult Is Nothing) Then
            Page.Title = TaskResult.Name

            _userid = Long.Parse(Session("UserId"))
            _user = Session("User")



            TaskId = CInt(Request("TaskId"))
            Me.HiddenTaskId.Value = TaskId

            Me.HiddenDocID.Value = TaskResult.ID

            If (IsPostBack = False) Then
                ExecuteOpenRules()
            Else
                If String.IsNullOrEmpty(Me.HiddenCurrentFormID.Value) = False Then
                    TaskResult.CurrentFormID = Int64.Parse(Me.HiddenCurrentFormID.Value)
                End If
            End If

            If TaskResult.CurrentFormID > 0 OrElse HasForms(TaskResult.DocTypeId) Then
                Me.HiddenCurrentFormID.Value = TaskResult.CurrentFormID

                Dim viewer As Views_UC_Viewers_FormBrowser
                viewer = LoadControl("../Viewers/FormBrowser.ascx")
                viewer.IsShowing = True
                viewer.TaskResult = TaskResult

                RemoveHandler viewer.ExecuteRule, AddressOf ThrowExecuteRule
                AddHandler viewer.ExecuteRule, AddressOf ThrowExecuteRule

                pnlViewer.Controls.Add(viewer)
            Else
                Dim viewer As Views_UC_Viewers_DocViewer

                viewer = LoadControl("../Viewers/DocViewer.ascx")
                viewer.Result = TaskResult
                pnlViewer.Controls.Add(viewer)
            End If

            Dim ZOptBusines As Zamba.Services.SZOptBusiness = New Zamba.Services.SZOptBusiness()
            Page.Title = DirectCast(ZOptBusines.GetValue("WebViewTitle"), String) + " - " + TaskResult.Name
        End If
    End Sub

    Public Function HasForms(ByVal docTypeId As Int64) As Boolean
        Dim sForms As New SForms()
        Dim result As Boolean = sForms.HasForms(docTypeId)
        sForms = Nothing

        Return result
    End Function

    ''' <summary>
    ''' Ejecuta las reglas de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ExecuteOpenRules()
        Try
            Dim List As New List(Of ITaskResult)()
            Dim formId As Int64 = TaskResult.CurrentFormID
            TaskResult.CurrentFormID = 0
            List.Add(TaskResult)

            Try
                Dim sRules As New SRules()

                Dim rules As List(Of IWFRuleParent) = sRules.GetCompleteHashTableRulesByStep(TaskResult.StepId)

                If Not IsNothing(rules) Then
                    For Each rule As IWFRuleParent In rules
                        If (rule.RuleType = TypesofRules.AbrirDocumento) Then
                            ThrowExecuteRule(rule.ID, List)
                        End If
                    Next
                End If

            Catch ex As Exception
                Zamba.AppBlock.ZException.Log(ex)
            End Try

            'Verifica si una DoShowForm no completo el formulario a mostrar
            If (TaskResult.CurrentFormID = 0) Then
                TaskResult.CurrentFormID = formId
            End If
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
    End Sub
End Class