Imports Zamba.Data
Imports Zamba.Core
Imports System.Xml.Serialization

<RuleCategory("Mensajes"), RuleDescription("Mensaje de Foro"), RuleHelp("Permite crear un nuevo mensaje de foro"), RuleFeatures(False)> <Serializable()> _
Public Class DoMessageForo
    Inherits WFRuleParent
    Implements IDoMessageForo


    Private messageBody As String
    Private messageAsunto As String



    Public Overrides Sub Dispose()

    End Sub
    Private _isLoaded As Boolean
    Private _isFull As Boolean
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
    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub


    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal WFStepId As Int64, ByVal mBody As String, ByVal mAsunto As String)
        MyBase.New(Id, Name, WFStepId)
    End Sub

    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Try
            Dim playRule As New Zamba.WFExecution.PlayDoMessageForo()
            Return playRule.Play(results, Me)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Return results
    End Function

    Public Overrides Function PlayWeb(results As List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As List(Of ITaskResult)
     
    End Function

    Public Property mAsunto() As String Implements Core.IDoMessageForo.mAsunto
        Get
            Return Me.messageAsunto
        End Get
        Set(ByVal value As String)
            Me.messageAsunto = value
        End Set
    End Property

    Public Property mBody() As String Implements Core.IDoMessageForo.mBody
        Get
            Return Me.messageBody
        End Get
        Set(ByVal value As String)
            Me.messageBody = value
        End Set
    End Property

    Public  ReadOnly Property MaskName() As String
        Get
            Return "Insertar nuevo tema en el Foro"
        End Get
    End Property
End Class
