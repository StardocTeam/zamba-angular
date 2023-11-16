Imports Zamba.Core

<RuleMainCategory("Tareas"), RuleCategory(""), RuleSubCategory(""), RuleDescription("Cambiar Estado"), RuleHelp("Permite modificar el estado de una tarea al ejecutarse la regla"), RuleFeatures(False)> <Serializable()> _
Public Class DoChangeState
    Inherits WFRuleParent
    Implements IDoChangeState, IRuleValidate
    Public Overrides Sub Dispose()

    End Sub
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDoChangeState
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

    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal p_iStateId As Int32, ByVal p_iStepId As Int32)
        MyBase.New(Id, Name, wfstepId) ', ListofRules.DoChangeState)
        m_iStateId = p_iStateId
        m_iStepId = p_iStepId
        playRule = New Zamba.WFExecution.PlayDoChangeState(Me)
    End Sub
    '--ITEMS--
    'StateId=0

    'Properties
    Private m_iStateId As Int32
    Private m_iStepId As Int32

    Public ReadOnly Property StateName() As String Implements IDoChangeState.StateName
        Get
            Return Zamba.Data.WFStepStatesFactory.GetStateNameByStateId(m_iStateId)
        End Get
    End Property

    Public Property StepId() As Int32 Implements IDoChangeState.StepId
        Get
            Return m_iStepId
        End Get
        Set(ByVal value As Int32)
            m_iStepId = value
        End Set
    End Property

    Public Property StateId() As Int32 Implements IDoChangeState.StateId
        Get
            Return m_iStateId
        End Get
        Set(ByVal value As Int32)
            m_iStateId = value
        End Set
    End Property

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

    Public Overrides ReadOnly Property MaskName() As String
        Get
            If m_iStepId = 0 And m_iStateId = 0 Then
                Return "Cambio Estado"
            Else
                Return "Cambio Estado a " & StateName
            End If
        End Get
    End Property
End Class