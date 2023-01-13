Imports Zamba.Core
''Imports Zamba.WFBusiness
''' <summary>
'''  Genera un Id y completa un indice determinado.
''' </summary>
''' <remarks></remarks>
<RuleMainCategory("Atributos"), RuleCategory(""), RuleSubCategory(""), RuleDescription("Generar Id"), RuleHelp("Genera un nuevo id e ingresa el nuevo valor del indice en la entidad seleccionada"), RuleFeatures(False)> <Serializable()> _
Public Class DoGetNewId
    Inherits WFRuleParent
    Implements IDoGetNewId
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDoGetNewId
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
    Private m_iDocTypeId As Int64
    Private m_iIndexId As Int64
    Public Overrides Sub Dispose()

    End Sub
#Region "Constructor"
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal p_iDocTypeId As Int64, ByVal p_iIndexId As Int64)
        MyBase.New(Id, Name, wfstepid)
        m_iDocTypeId = p_iDocTypeId
        m_iIndexId = p_iIndexId
        playRule = New WFExecution.PlayDoGetNewId(Me)
    End Sub
#End Region

    Public Property DocTypeId() As Int64 Implements IDoGetNewId.DocTypeId
        Get
            Return m_iDocTypeId
        End Get
        Set(ByVal value As Int64)
            m_iDocTypeId = value
        End Set
    End Property

    Public Property IndexId() As Int64 Implements IDoGetNewId.IndexId
        Get
            Return m_iIndexId
        End Get
        Set(ByVal value As Int64)
            m_iIndexId = value
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
