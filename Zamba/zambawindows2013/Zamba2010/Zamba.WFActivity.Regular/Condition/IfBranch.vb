Imports zamba.Core

Public Class IfBranch
    Inherits WFRuleParent
    Implements IIfBranch, IRuleValidate

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _ifType As Boolean
    Private _isValid As Boolean
    Private playRule As Zamba.WFExecution.PlayIfBranch

    Public Overrides Sub Dispose()

    End Sub
    Private Sub New()
        MyBase.New(0, "", 0)
    End Sub

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal ifType As Boolean)
        MyBase.New(Id, Name, wfstepid)
        Me.ifType = ifType
        playRule = New Zamba.WFExecution.PlayIfBranch(Me)
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
    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
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

    Public Overrides Sub Load()

    End Sub
End Class