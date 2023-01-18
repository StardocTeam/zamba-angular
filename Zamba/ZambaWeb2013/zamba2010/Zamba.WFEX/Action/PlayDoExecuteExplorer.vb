Imports Zamba.Data
Imports Zamba.Core
Imports Zamba.servers
Imports System.Windows.Forms

Public Class PlayDoExecuteExplorer
    ''' <summary>
    ''' Abre un WebBrowser
    ''' </summary>
    ''' <param name="results"></param>
    ''' <param name="myRule"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Pablo] 19-10-2010 Created 
    ''' </history>
    ''' 
    Private myRule As IDoExecuteExplorer
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim form As New Form
        Dim WebRoute As String = Nothing
        Dim params As New Hashtable

        WebRoute = myRule.Route
        If WebRoute.Contains("zvar") = True Then
            WebRoute = WFRuleParent.ReconocerVariablesValuesSoloTexto(WebRoute)
        End If

        If Not WebRoute.StartsWith("http://") And Not WebRoute.StartsWith("https://") Then
            WebRoute = "http://" & WebRoute
        End If

        Try
            params.Add("ruleId", myRule.RuleID)
            params.Add("paramResult", results)
            params.Add("height", myRule.Height)
            params.Add("width", myRule.Width)
            params.Add("ContinueWithRule", myRule.ContinueWithRule)
            params.Add("HorizontalVisualization", myRule.HorizontalVisualization)
            params.Add("ExecuteElseID", myRule.ExecuteElseID)
            params.Add("Route", WebRoute.Trim())
            params.Add("Habilitar", myRule.Habilitar)
            params.Add("TxtVar", myRule.Variable)
            params.Add("Operador", myRule.Operador)
            params.Add("Value", myRule.Valor)
            params.Add("NewBrowser", myRule.OpenNewWindowBrowser)

            If (myRule.Habilitar And myRule.EvaluateRuleID <> -1) Or (myRule.Habilitar And myRule.EvaluateRuleID <> 0) Then
                params.Add("EvaluateRuleID", myRule.EvaluateRuleID)
            End If

            'abrir el WebBrowser por fuera o por dentro
            params.Add("WayToOpen", myRule.BrowserStatus)
            ZambaCore.HandleModule(ResultActions.OpenBrowser, results(0), params)

        Catch ex As System.UriFormatException
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub New(ByVal rule As IDoExecuteExplorer)
        Me.myRule = rule
    End Sub
End Class

