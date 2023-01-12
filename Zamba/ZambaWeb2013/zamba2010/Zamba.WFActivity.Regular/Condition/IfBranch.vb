Imports zamba.Core

Public Class IfBranch
    Inherits WFRuleParent
    Implements IIfBranch

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _ifType As Boolean
    Private playRule As Zamba.WFExecution.PlayIfBranch

    Public Overrides Sub Dispose()

    End Sub
    Private Sub New()
        MyBase.New(0, "", 0)
    End Sub

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal ifType As Boolean)
        MyBase.New(Id, Name, wfstepid)
        Me.ifType = ifType
        Me.playRule = New Zamba.WFExecution.PlayIfBranch(Me)
    End Sub
    ''' <summary>
    ''' Ejecuta la regla
    ''' </summary>
    ''' <param name="results"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''       [Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    ''' </history>
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
    Public Property ifType() As Boolean Implements IIfBranch.ifType
        Get
            Return _ifType
        End Get
        Set(ByVal value As Boolean)
            _ifType = value
        End Set
    End Property

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides ReadOnly Property IsFull() As Boolean
        Get

        End Get
    End Property

    Public Overrides ReadOnly Property IsLoaded() As Boolean
        Get

        End Get
    End Property

    Public Overrides Sub Load()

    End Sub
End Class
