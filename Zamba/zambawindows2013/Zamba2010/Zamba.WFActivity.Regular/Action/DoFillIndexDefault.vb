Imports Zamba.Core

''' Project	 : Zamba.Business
''' Class	 : Core.DoFillIndex
''' <summary>
''' Regla para completar Atributos
''' </summary>
''' <remarks>
''' Hereda WFRuleParent
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
<RuleMainCategory("Atributos"), RuleCategory("Autocompletar"), RuleSubCategory(""), RuleDescription("Completar Atributos con Valores Predefinidos"), RuleHelp("Permite ingresar un valor predefinido al indice de forma automatica"), RuleFeatures(False)> <Serializable()> _
Public Class DoFillIndexDefault
    Inherits WFRuleParent
    Implements IDoFillIndexDefault, IRuleValidate
    Private _indexID As Int64
    Public Overrides Sub Dispose()

    End Sub
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _isValid As Boolean
    Private playRule As Zamba.WFExecution.PlayDoFillIndexDefault
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
    Public Property IndexID() As Int64 Implements IDoFillIndexDefault.IndexID
        Get
            Return _indexID
        End Get
        Set(ByVal value As Int64)
            _indexID = value
        End Set
    End Property
    Public Property TEXTODEFAULT() As String Implements IDoFillIndexDefault.TEXTODEFAULT
        Get
            Return _TEXTODEFAULT
        End Get
        Set(ByVal value As String)
            _TEXTODEFAULT = value
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

    Private _TEXTODEFAULT As String

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal Index_Id As Int32, ByVal TEXTODEFAULT As String)
        MyBase.New(Id, Name, wfstepid)

        _indexID = Index_Id
        _TEXTODEFAULT = TEXTODEFAULT

        playRule = New Zamba.WFExecution.PlayDoFillIndexDefault(Me)
    End Sub

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