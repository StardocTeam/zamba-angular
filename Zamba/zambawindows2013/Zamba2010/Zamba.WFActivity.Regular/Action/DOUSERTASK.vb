Imports Zamba.Core
Imports System.Xml.Serialization

<RuleMainCategory("Workflow"), RuleCategory("Reglas"), RuleSubCategory(""), RuleDescription("Ejecutar Regla de Usuario"), RuleHelp("Permite ejecutar una regla realizada por un usuario"), RuleFeatures(True)> <Serializable()> _
Public Class DOUSERTASK
    Inherits WFRuleParent
    Implements IDOUSERTASK, IRuleValidate

    Private playRule As Zamba.WFExecution.PlayDOUSERTASK

    Public Overrides Sub Dispose()

    End Sub
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _isValid As Boolean
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
    Public Property IsValid As Boolean Implements IRuleValidate.isValid
        Get
            Return _isValid
        End Get
        Set(ByVal value As Boolean)
            _isValid = value
        End Set
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


    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64)
        MyBase.New(Id, Name, wfstepId)
        Me.playRule = New WFExecution.PlayDOUSERTASK(Me)
    End Sub

    'Public Overloads Overrides Function Play(ByVal Results As SortedList) As SortedList
    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)

        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function
End Class