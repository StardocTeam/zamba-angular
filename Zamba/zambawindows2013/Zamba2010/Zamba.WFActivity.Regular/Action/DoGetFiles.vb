Imports Zamba.Core


<RuleMainCategory("Archivos y Aplicaciones"), RuleCategory("Archivos"), RuleSubCategory("Administrar"), RuleDescription("Obtener Archivos"), RuleHelp("Obtiene Archivos a partir de una ruta especifica"), RuleFeatures(False)> <Serializable()> _
Public Class DoGetFiles
    Inherits WFRuleParent
    Implements IDoGetFiles, IRuleValidate

#Region "Atributos"

    Private playRule As Zamba.WFExecution.PlayDoGetFiles

    Private _isFull As Boolean
    Private _isLoaded As Boolean
    Private _DirectoryRoute As String
    Private _VarName As String
    Private _ObtainOnlyRouteFiles As Boolean
    Private _Extensions As String
    Private _isValid As Boolean
#End Region

    Public Overrides Sub Dispose()

    End Sub

    Public Overrides Sub FullLoad()

    End Sub

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

    Public Overrides Sub Load()

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


    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal DirectoryRoute As String, ByVal ObtainOnlyRouteFiles As Boolean, ByVal VarName As String, ByVal Extensions As String)
        MyBase.New(Id, Name, wfstepId)
        _DirectoryRoute = DirectoryRoute
        _ObtainOnlyRouteFiles = ObtainOnlyRouteFiles
        _VarName = VarName
        _Extensions = Extensions
        playRule = New Zamba.WFExecution.PlayDoGetFiles(Me)
    End Sub

    Public Property DirectoryRoute() As String Implements Core.IDoGetFiles.DirectoryRoute
        Get
            Return _DirectoryRoute
        End Get
        Set(ByVal value As String)
            _DirectoryRoute = value
        End Set
    End Property
    Public Property ObtainOnlyRouteFiles() As Boolean Implements Core.IDoGetFiles.ObtainOnlyRouteFiles
        Get
            Return _ObtainOnlyRouteFiles
        End Get
        Set(ByVal value As Boolean)
            _ObtainOnlyRouteFiles = value
        End Set
    End Property
    Public Property VarName() As String Implements Core.IDoGetFiles.VarName
        Get
            Return _VarName
        End Get
        Set(ByVal value As String)
            _VarName = value
        End Set
    End Property
    Public Property Extensions() As String Implements Core.IDoGetFiles.Extensions
        Get
            Return _Extensions
        End Get
        Set(ByVal value As String)
            _Extensions = value
        End Set
    End Property

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return String.Empty
        End Get
    End Property
End Class
