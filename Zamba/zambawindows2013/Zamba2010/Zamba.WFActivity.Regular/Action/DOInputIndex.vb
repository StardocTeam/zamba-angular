Imports Zamba.Core


<RuleMainCategory("Atributos"), RuleCategory("Completar"), RuleSubCategory(""), RuleDescription("Ingresar Atributo"), RuleHelp("solicita el ingreso del valor de un atributo"), RuleFeatures(True)> <Serializable()> _
Public Class DOInputIndex
    Inherits WFRuleParent
    Implements IDOInputIndex, IRuleValidate
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _isValid As Boolean
    Private playRule As Zamba.WFExecution.PlayDOInputIndex
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

    Private m_IndexId As Int32
    'Private m_idocTypeId as Int64
    Private m_Name As String


    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal WFStepid As Int64, ByVal p_iDocTypeId As Int64, ByVal p_iIndexId As Int64)
        MyBase.New(Id, Name, WFStepid)
        m_IndexId = p_iIndexId
        playRule = New Zamba.WFExecution.PlayDOInputIndex(Me)
    End Sub

    Public Property Index() As Int32 Implements IDOInputIndex.Index
        Get
            Return m_IndexId
        End Get
        Set(ByVal value As Int32)
            m_IndexId = value
            Try
                m_Name = IndexsBusiness.GetIndexName(m_IndexId, True)
            Catch ex As Exception
                raiseerror(ex)
            End Try
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
End Class