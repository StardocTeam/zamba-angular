Imports Zamba.Core
Imports Zamba.WFExecution

<RuleMainCategory("Atributos"), RuleCategory(""), Rulesubcategory(""), RuleDescription("Limpiar cache de lista de atributo"), RuleHelp("Permite limpiar las listas almacenadas en memoria de un atributo."), RuleFeatures(True)> <Serializable()> _
Public Class DoClearAttributeListCache

    Inherits WFRuleParent
    Implements IDoClearAttributeListCache, IRuleValidate

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _attributeId As Long
    Private playRule As PlayDoClearAttributeListCache
    Private _isValid As Boolean


    Public Property AttributeId() As Long Implements IDoClearAttributeListCache.AttributeId
        Get
            Return _attributeId
        End Get
        Set(ByVal value As Long)
            _attributeId = value
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
    Public Overrides Sub Dispose()

    End Sub

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal attributeId As Long)
        MyBase.New(Id, Name, wfstepid)
        _attributeId = attributeId
        playRule = New PlayDoClearAttributeListCache(Me)
    End Sub

    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function
    Public Overrides ReadOnly Property MaskName As String
        Get
            Return "Borro Cache de Listas de Atributos"
        End Get
    End Property
End Class
