Imports Zamba.Core


<RuleCategory("Datos"), RuleDescription("Consumir Servicio REST"), RuleHelp("Permite consumir servicios REST desde la url pasada por parametro, el resultado del procesamiento será guardado en la variable seleccionada por el usuario."), RuleFeatures(True)> <Serializable()> _
Public Class DoConsumeRestApi
    Inherits WFRuleParent
    Implements IDoConsumeRestApi

    Public Property ResultVar As String Implements IDoConsumeRestApi.ResultVar
    Public Property Url As String Implements IDoConsumeRestApi.Url
    Public Property JsonMessage As String Implements IDoConsumeRestApi.JsonMessage
    Public Property Method As String Implements IDoConsumeRestApi.Method
    Public playRule As Zamba.WFExecution.PlayDoConsumeRestApi

#Region "Miembros de interfaces"
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Public Overloads Overrides Function PlayTest() As Boolean
        Try
            Return playRule.PlayTest()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function
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

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return String.Empty
        End Get
    End Property

    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
    Public Overrides Sub Dispose()

    End Sub
#End Region

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal url As String, ByVal resultVar As String, ByVal JsonMessage As String, ByVal Method As String)
        MyBase.New(Id, Name, wfstepid)

        Me.ResultVar = resultVar
        Me.Url = url
        Me.JsonMessage = JsonMessage
        Me.Method = Method
        playRule = New Zamba.WFExecution.PlayDoConsumeRestApi(Me)
    End Sub

    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)
        Try
            Return playRule.Play(results)
        Catch ex As Exception

        End Try

    End Function
End Class