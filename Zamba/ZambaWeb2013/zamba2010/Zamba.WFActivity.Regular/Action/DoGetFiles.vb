Imports Zamba.Data
Imports Zamba.Core
Imports System.Xml.Serialization


<RuleCategory("Secciones"), RuleDescription("Obtener Archivos"), RuleHelp("Obtiene Archivos a partir de una ruta especifica"), RuleFeatures(False)> <Serializable()> _
Public Class DoGetFiles
    Inherits WFRuleParent
    Implements IDoGetFiles

#Region "Atributos"

    Private playRule As Zamba.WFExecution.PlayDoGetFiles

    Private _isFull As Boolean
    Private _isLoaded As Boolean
    Private _DirectoryRoute As String
    Private _VarName As String
    Private _ObtainOnlyRouteFiles As Boolean
    Private _Extensions As String
#End Region

    Public Overrides Sub Dispose()

    End Sub

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides ReadOnly Property IsFull() As Boolean
        Get
            Return _isFull
        End Get
    End Property

    Public Overrides ReadOnly Property IsLoaded() As Boolean
        Get
            Return _isLoaded
        End Get
    End Property

    Public Overrides Sub Load()

    End Sub
    Public Overrides Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return playRule.Play(results)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal DirectoryRoute As String, ByVal ObtainOnlyRouteFiles As Boolean, ByVal VarName As String, ByVal Extensions As String)
        MyBase.New(Id, Name, wfstepId)
        Me._DirectoryRoute = DirectoryRoute
        Me._ObtainOnlyRouteFiles = ObtainOnlyRouteFiles
        Me._VarName = VarName
        Me._Extensions = Extensions
        Me.playRule = New Zamba.WFExecution.PlayDoGetFiles(Me)
    End Sub

    Public Property DirectoryRoute() As String Implements Core.IDoGetFiles.DirectoryRoute
        Get
            Return Me._DirectoryRoute
        End Get
        Set(ByVal value As String)
            Me._DirectoryRoute = value
        End Set
    End Property
    Public Property ObtainOnlyRouteFiles() As Boolean Implements Core.IDoGetFiles.ObtainOnlyRouteFiles
        Get
            Return Me._ObtainOnlyRouteFiles
        End Get
        Set(ByVal value As Boolean)
            Me._ObtainOnlyRouteFiles = value
        End Set
    End Property
    Public Property VarName() As String Implements Core.IDoGetFiles.VarName
        Get
            Return Me._VarName
        End Get
        Set(ByVal value As String)
            Me._VarName = value
        End Set
    End Property
    Public Property Extensions() As String Implements Core.IDoGetFiles.Extensions
        Get
            Return Me._Extensions
        End Get
        Set(ByVal value As String)
            Me._Extensions = value
        End Set
    End Property
End Class
