Imports Zamba.Data
Imports Zamba.Core
Imports System.Xml.Serialization

''' <summary>
''' Esta regla compara un listado por un atributo de cada elemento y guarda en una variable los elementos que cumplan con ese valor
''' </summary>
''' <history>
''' Marcelo Created 14/09/2009
''' </history>
''' <remarks></remarks>
<RuleCategory("Datos"), RuleDescription("Comparar Listado de Datos"), RuleHelp("Permite filtrar listado de Datos por valor dado por el usuario, guardando el listado filtrado en una variable"), RuleFeatures(False)> <Serializable()> _
Public Class DoCompare
    Inherits WFRuleParent
    Implements IDoCompare
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
    Private Sub New()
        MyBase.New(0, "", 0)
    End Sub
    Public Overrides Sub Dispose()

    End Sub

    Private _UseAsocDoc As Boolean
    Private _IdAsoc As String
    Private _idDocTypeAsoc As Int64
    Private _valueList As String
    Private _valueComp As String
    Private _Comp As String
    Private _valueFilter As String
    Private _variableName As String

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfStepId As Int64, ByVal UseAsocDoc As Boolean, ByVal IdAsoc As String, ByVal idDocTypeAsoc As Int64, ByVal valueList As String, ByVal valueComp As String, ByVal Comp As String, ByVal valueFilter As String, ByVal variableName As String)
        MyBase.New(Id, Name, wfStepId)
        Me.UseAsocDoc = UseAsocDoc
        Me.IdAsoc = IdAsoc
        Me.idDocTypeAsoc = idDocTypeAsoc
        Me.valueList = valueList
        Me.valueComp = valueComp
        Me.Comp = Comp
        Me.valueFilter = valueFilter
        Me.variableName = variableName
    End Sub

    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoCompare()
        Return playRule.Play(results, Me)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New WFExecution.PlayDoCompare()
        Return playRule.Play(results, Me)
    End Function

    Property UseAsocDoc() As Boolean Implements IDoCompare.UseAsocDoc
        Get
            Return _UseAsocDoc
        End Get
        Set(ByVal value As Boolean)
            _UseAsocDoc = value
        End Set
    End Property
    Property IdAsoc() As String Implements IDoCompare.IdAsoc
        Get
            Return _IdAsoc
        End Get
        Set(ByVal value As String)
            _IdAsoc = value
        End Set
    End Property
    Property idDocTypeAsoc() As Int64 Implements IDoCompare.idDocTypeAsoc
        Get
            Return _idDocTypeAsoc
        End Get
        Set(ByVal value As Int64)
            _idDocTypeAsoc = value
        End Set
    End Property
    Property valueList() As String Implements IDoCompare.valueList
        Get
            Return _valueList
        End Get
        Set(ByVal value As String)
            _valueList = value
        End Set
    End Property
    Property valueComp() As String Implements IDoCompare.valueComp
        Get
            Return _valueComp
        End Get
        Set(ByVal value As String)
            _valueComp = value
        End Set
    End Property
    Property Comp() As String Implements IDoCompare.Comp
        Get
            Return _Comp
        End Get
        Set(ByVal value As String)
            _Comp = value
        End Set
    End Property
    Property valueFilter() As String Implements IDoCompare.valueFilter
        Get
            Return _valueFilter
        End Get
        Set(ByVal value As String)
            _valueFilter = value
        End Set
    End Property
    Property variableName() As String Implements IDoCompare.variableName
        Get
            Return _variableName
        End Get
        Set(ByVal value As String)
            _variableName = value
        End Set
    End Property
End Class