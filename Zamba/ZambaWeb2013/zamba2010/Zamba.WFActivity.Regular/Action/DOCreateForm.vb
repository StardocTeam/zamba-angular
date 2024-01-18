Imports Zamba.Data
Imports Zamba.Core
Imports System.Xml.Serialization

<RuleCategory("Formularios"), RuleDescription("Crear Formulario"), RuleHelp("Permite crear un nuevo formulario, a partir de un tipo de docuemnto asociado a un formulario electronico"), RuleFeatures(False)> <Serializable()> _
Public Class DOCreateForm
    Inherits WFRuleParent
    Implements IDOCreateForm

#Region "Atributos"

    Private _isLoaded As Boolean
    Private _isFull As Boolean

    Public Event FormCreated(ByRef Result As IResult) Implements IDOCreateForm.FormCreated


    'Id del Entidad Virtual a crear
    Private m_iDocTypeIdVirtual As Int32
    Private m_bAddToWf As Boolean
    Private m_hashTable As String

#End Region

#Region "Constructor"

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal WFStepid As Int64, ByVal p_iDocTypeIdVirtual As Int32, ByVal p_bAddToWf As Boolean, ByVal p_hashTable As String)

        MyBase.New(Id, Name, WFStepid)
        Me.m_iDocTypeIdVirtual = p_iDocTypeIdVirtual
        Me.m_bAddToWf = p_bAddToWf
        Me.m_hashTable = p_hashTable

    End Sub

#End Region

#Region "Propiedades"

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

    Public Property DocTypeIdVirtual() As Int32 Implements IDOCreateForm.DocTypeIdVirtual
        Get
            Return m_iDocTypeIdVirtual
        End Get
        Set(ByVal Value As Int32)
            m_iDocTypeIdVirtual = Value
        End Set
    End Property

    Public Property AddToWf() As Boolean Implements IDOCreateForm.AddToWf
        Get
            Return m_bAddToWf
        End Get
        Set(ByVal value As Boolean)
            m_bAddToWf = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para contener la variable interregla
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	18/07/2008	Created
    ''' </history>
    Public Property HashTable() As String Implements IDOCreateForm.HashTable
        Get
            Return (m_hashTable)
        End Get
        Set(ByVal value As String)
            m_hashTable = value
        End Set
    End Property

#End Region

#Region "M�todos"

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides Sub Load()

    End Sub

    Public Overrides Sub Dispose()

    End Sub

    'Public Overloads Overrides Function Play(ByVal Results As SortedList) As System.Collections.SortedList
    'Dim list As SortedList
    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDOCreateForm()
        Return playRule.Play(results, Me)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New WFExecution.PlayDOCreateForm()
        Return playRule.Play(results, Me)
    End Function
#End Region

End Class