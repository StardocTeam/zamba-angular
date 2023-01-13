Imports Zamba.Data
Imports zamba.Core
'Imports Zamba.WFBusiness
Imports System.Xml.Serialization

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Business
''' Class	 : Core.DoAsign
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Regla para asignar una tarea a un usuario
''' </summary>
''' <remarks>
''' Hereda de WFRuleParent
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
<RuleCategory("Usuarios"), RuleDescription("Asignar a Usuario"), RuleHelp("Permite seleccionar a que usuario o grupo se le va a asignar la tarea."), RuleFeatures(False)> <Serializable()> _
Public Class DoAsign
    Inherits WFRuleParent
    Implements IDoAsign
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDoAsign
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
    Public Overrides Sub Dispose()

    End Sub
    ' Constructor
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfStepID As Int64, ByVal UserId As Int32, ByVal AlternateUser As String)
        MyBase.New(Id, Name, wfStepID)
        Me._UserId = UserId
        Me._AlternateUser = AlternateUser
        Me.playRule = New Zamba.WFExecution.PlayDoAsign(Me)
    End Sub
    'Items
    '0=UserId

    'Properties
    'Public Property RuleWStep() As IWFStep Implements IDoAsign.WFStep
    '    Get
    '        Return Me.WFStep
    '    End Get
    '    Set(ByVal value As IWFStep)
    '        Me.WFStep = value
    '    End Set
    'End Property

    Public Property UserId() As Int32 Implements IDoAsign.UserId
        Get
            Return _UserId
        End Get
        Set(ByVal value As Int32)
            _UserId = value
        End Set
    End Property
    Private _UserId As Int32

    Public Property AlternateUser() As String Implements IDoAsign.AlternateUser
        Get
            Return _AlternateUser
        End Get
        Set(ByVal value As String)
            _AlternateUser = value
        End Set
    End Property

    Private _AlternateUser As String

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
End Class
