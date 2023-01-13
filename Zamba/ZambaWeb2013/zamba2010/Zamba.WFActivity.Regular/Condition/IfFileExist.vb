Imports Zamba.Core
Imports System.IO


<RuleCategory("Secciones"), RuleDescription("Validar Archivo"), RuleHelp("Realiza una busqueda en el directorio indicado para comprobar si existe el archivo"), RuleFeatures(False)> _
Public Class IfFileExists
    Inherits WFRuleParent
    Implements IIfFileExists, IRuleIFPlay
    Public Overrides Sub Dispose()

    End Sub

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
    Private _searchPath As String
    Private _searchOption As IO.SearchOption = SearchOption.AllDirectories
    Private _textoInteligente As String

    Public Property SearchPath() As String Implements IIfFileExists.SearchPath
        Get
            Return _searchPath
        End Get
        Set(ByVal value As String)
            _searchPath = value
        End Set
    End Property
    Public Property SearchOption() As IO.SearchOption Implements IIfFileExists.SearchOption
        Get
            Return _searchOption
        End Get
        Set(ByVal value As IO.SearchOption)
            _searchOption = value
        End Set
    End Property
    Public Property TextoInteligente() As String Implements IIfFileExists.TextoInteligente
        Get
            Return _textoInteligente
        End Get
        Set(ByVal value As String)
            _textoInteligente = value
        End Set
    End Property

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal searchPath As String, ByVal searchOption As Integer, ByVal textoInteligente As String)
        MyBase.New(Id, Name, wfstepid)
        _searchPath = searchPath
        _searchOption = DirectCast(searchOption, SearchOption)
        _textoInteligente = textoInteligente
    End Sub

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayIfFileExists()
        Return playRule.Play(results, Me)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayIfFileExists()
        Return playRule.Play(results, Me)
    End Function


    ''' <summary>
    ''' Ejecuta la regla devolviendo los results que cumplan con la condicion requerida
    ''' </summary>
    ''' <param name="results">Results a validar</param>
    ''' <param name="ifType">True si quiero que devuelve los results que coincidan con la validacion, false si quiero los que no coincidan</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''        [Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    ''' </history>
    Public Function PlayIf(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of ITaskResult) Implements IRuleIFPlay.PlayIf
        Dim playRule As New Zamba.WFExecution.PlayIfFileExists()
        Return playRule.Play(results, Me, ifType)
    End Function
End Class

