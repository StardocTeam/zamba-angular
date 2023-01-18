Imports Zamba.Core
Imports System.Xml.Serialization

<RuleCategory("Formularios"), RuleDescription("Generar Formulario Dinámico"), RuleHelp("Crea un formulario dinámico a travez de un Entidad"), RuleFeatures(False)> <Serializable()> _
Public Class DoGenerateDinamicForm

    Inherits WFRuleParent
    Implements IDoGenerateDinamicForm

#Region "ATRIBUTOS"

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _DocType As Int64
    Private _FormId As Integer
    Private _variable As String
    Private _columnName As String
    Private _maskname As String
    Private _name As String

#End Region

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal WFStepid As Int64, ByVal variable As String, ByVal doctype As Int64, ByVal FrmName As String, ByVal FormId As Integer, ByVal columnName As String)
        MyBase.New(Id, Name, WFStepid)

        Me.DocType = doctype
        Me.variable = variable
        Me.Name = FrmName
        Me.FormId = FormId
        Me.ColumnName = columnName
    End Sub

#Region "PROPIEDADES"
    Public Property ColumnName() As String Implements IDoGenerateDinamicForm.ColumnName
        Get
            Return _columnName
        End Get
        Set(ByVal value As String)
            _columnName = value
        End Set
    End Property

    Public Property variable() As String Implements IDoGenerateDinamicForm.Variable
        Get
            Return _variable
        End Get
        Set(ByVal value As String)
            _variable = value
        End Set
    End Property

    Public Property Name() As String Implements IDoGenerateDinamicForm.Name
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Public Property FormId() As Integer Implements IDoGenerateDinamicForm.FormId
        Get
            Return _DocType
        End Get
        Set(ByVal value As Integer)
            _DocType = value
        End Set
    End Property

    Public Property DocType() As Int64 Implements IDoGenerateDinamicForm.DocType
        Get
            Return _DocType
        End Get
        Set(ByVal value As Int64)
            _DocType = value
        End Set
    End Property

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

#End Region

#Region "METODOS"

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides Sub Load()

    End Sub

    Public Overrides Sub Dispose()

    End Sub

    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoGenerateDinamicForm
        Return playRule.Play(results, Me)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoGenerateDinamicForm
        Return playRule.Play(results, Me)
    End Function
#End Region

End Class
