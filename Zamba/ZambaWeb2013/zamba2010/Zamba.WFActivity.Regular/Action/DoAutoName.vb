Imports Zamba.Data
Imports Zamba.Core
Imports System.Xml.Serialization


<RuleCategory("Datos"), RuleDescription("Ejecuta el autoname para la tarea existente o para una nueva"), RuleHelp("Ejecuta el autoname para la tarea existente o para una nueva"), RuleFeatures(False)> <Serializable()> _
Public Class DoAutoName
    Inherits WFRuleParent
    Implements IDoAutoName


#Region "Atributos"
    Private playrule As Zamba.WFExecution.PlayDoAutoName
    Private _updateMultiple As Boolean
    Private _docTypeIds As String
    Private _variabledocid As String
    Private _variabledoctypeid As String
    Private _seleccion As String
    Private _nombreColumna As String
#End Region

    Public Overrides Sub Dispose()

    End Sub

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

    Public Overrides Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return playrule.Play(results)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    Public Sub New(ByVal Id As Int64, _
                   ByVal Name As String, _
                   ByVal wfstepId As Int64, _
                   ByVal Seleccion As String, _
                   ByVal variabledocid As String, _
                   ByVal variabledoctypeid As String, _
                   ByVal variablenombrecolumna As String, _
                   ByVal updateMultiple As Boolean, _
                   ByVal docTypeIds As String)

        MyBase.New(Id, Name, wfstepId)

        Me._seleccion = Seleccion
        Me._variabledocid = variabledocid
        Me._variabledoctypeid = variabledoctypeid
        Me._nombreColumna = variablenombrecolumna
        Me._updateMultiple = updateMultiple
        Me._docTypeIds = docTypeIds

        Me.playrule = New Zamba.WFExecution.PlayDoAutoName(Me)


    End Sub


    Public Property Seleccion() As String Implements Core.IDoAutoName.Seleccion
        Get
            Return Me._seleccion.ToString()
        End Get
        Set(ByVal value As String)
            Me._seleccion = value
        End Set
    End Property

    Public Property variabledocid() As String Implements Core.IDoAutoName.variabledocid
        Get
            Return Me._variabledocid.ToString()
        End Get
        Set(ByVal value As String)
            Me._variabledocid = value
        End Set


    End Property


    Public Property variabledoctypeid() As String Implements Core.IDoAutoName.variabledoctypeid
        Get
            Return Me._variabledoctypeid.ToString()
        End Get
        Set(ByVal value As String)
            Me._variabledoctypeid = value
        End Set


    End Property

    Public Property variableconmrecolumna() As String Implements Core.IDoAutoName.nombreColumna
        Get
            Return Me._nombreColumna.ToString()
        End Get
        Set(ByVal value As String)
            Me._nombreColumna = value
        End Set


    End Property

    Public Property UpdateMultiple() As Boolean Implements Core.IDoAutoName.updateMultiple
        Get
            Return _updateMultiple
        End Get
        Set(ByVal value As Boolean)
            _updateMultiple = value
        End Set
    End Property

    Public Property DocTypeIds() As String Implements Core.IDoAutoName.docTypeIds
        Get
            Return _docTypeIds
        End Get
        Set(ByVal value As String)
            _docTypeIds = value
        End Set
    End Property

End Class