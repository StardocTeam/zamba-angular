Imports Zamba.Data
Imports Zamba.Core
Imports System.Xml.Serialization

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Business
''' Class	 : Core.DOMail
''' -----------------------------------------------------------------------------
''' <summary>
''' Regla que permite guardar un valor (texto, zvar(...), texto inteligente) en una variable
''' </summary>
''' <remarks>
''' Hereda WFRuleParent
''' </remarks>
''' <history>
''' 	[Gaston]	16/12/2008	Created
'''  	[Marcelo]	15/09/2009	Modified - Se agrega concatenacion
''' </history>
''' -----------------------------------------------------------------------------
<RuleCategory("Tareas"), RuleDescription("Completar Variable de Nombre"), RuleHelp("Permite guardar un valor en una variable"), RuleFeatures(False)> <Serializable()> _
Public Class DoFillVar
    Inherits WFRuleParent
    Implements IDoFillVar

    Private m_variableName As String
    Private m_variableValue As String
    Private m_useConc As Boolean
    Private m_valueConc As String
    Private playRule As Zamba.WFExecution.PlayDoFillVar

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

    Public Property varName() As String Implements IDoFillVar.variableName
        Get
            Return (m_variableName)
        End Get

        Set(ByVal value As String)
            m_variableName = value
        End Set
    End Property

    Public Property varValue() As String Implements IDoFillVar.variableValue
        Get
            Return (m_variableValue)
        End Get

        Set(ByVal value As String)
            m_variableValue = value
        End Set
    End Property


    ''' <summary>
    ''' Determina si se utiliza o no la concatenacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property useConc() As Boolean Implements IDoFillVar.useConc
        Get
            Return m_useConc
        End Get
        Set(ByVal value As Boolean)
            m_useConc = value
        End Set
    End Property

    ''' <summary>
    ''' Valor que separara la concatenacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property valueConc() As String Implements IDoFillVar.concValue
        Get
            Return m_valueConc
        End Get
        Set(ByVal value As String)
            m_valueConc = value
        End Set
    End Property

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal variableName As String, ByVal variableValue As String, ByVal useConc As Boolean, ByVal valueConc As String)
        MyBase.New(Id, Name, wfstepid)

        Me.m_variableName = variableName
        Me.m_variableValue = variableValue
        Me.m_useConc = useConc
        Me.m_valueConc = valueConc
        Me.playRule = New Zamba.WFExecution.PlayDoFillVar(Me)
    End Sub

    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Return (playRule.Play(results))
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
End Class