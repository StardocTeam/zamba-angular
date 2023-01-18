Imports Zamba.Core

<RuleMainCategory("Zamba"), RuleCategory("Formularios"), RuleSubCategory(""), RuleDescription("Crear Formulario"), RuleHelp("Permite crear un nuevo formulario, a partir de un tipo de docuemnto asociado a un formulario electronico"), RuleFeatures(False)> <Serializable()> _
Public Class DOCreateForm
    Inherits WFRuleParent
    Implements IDOCreateForm, IRuleValidate

#Region "Atributos"

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _isValid As Boolean

    Public Event FormCreated(ByRef Result As IResult) Implements IDOCreateForm.FormCreated


    'Id de la entidad Virtual a crear
    Private m_iDocTypeIdVirtual As Int32
    Private m_bAddToWf As Boolean
    Private m_hashTable As String
    Private playRule As Zamba.WFExecution.PlayDOCreateForm

#End Region

#Region "Constructor"

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal WFStepid As Int64, ByVal p_iDocTypeIdVirtual As Int32, ByVal p_bAddToWf As Boolean, ByVal p_hashTable As String)

        MyBase.New(Id, Name, WFStepid)
        m_iDocTypeIdVirtual = p_iDocTypeIdVirtual
        m_bAddToWf = p_bAddToWf
        m_hashTable = p_hashTable
        playRule = New WFExecution.PlayDOCreateForm(Me)

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
#End Region

#Region "Métodos"

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides Sub Load()

    End Sub

    Public Overrides Sub Dispose()

    End Sub

    'Public Overloads Overrides Function Play(ByVal Results As SortedList) As System.Collections.SortedList
    'Dim list As SortedList
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

#End Region

End Class
