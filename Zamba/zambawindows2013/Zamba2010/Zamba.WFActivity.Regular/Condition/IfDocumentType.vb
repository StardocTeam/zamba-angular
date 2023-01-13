Imports Zamba.Core

<RuleMainCategory("Tareas"), RuleCategory(""), RuleSubCategory(""), RuleDescription("Validar Entidad"), RuleHelp("Permite realizar una validación de la entidad para tomar una desición determinada"), RuleFeatures(False)> _
Public Class IfDocumentType
    Inherits WFRuleParent
    Implements IIfDocumentType, IRuleIFPlay, IRuleValidate
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _isValid As Boolean

    Private playRule As Zamba.WFExecution.PlayIfDocumentType
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
    Private Sub New()
        MyBase.New(0, "", 0)
    End Sub

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal DocTypeId As Int64, ByVal Comp As Comparators)
        MyBase.New(Id, Name, wfstepid)
        _DocTypeId = DocTypeId
        _Comp = Comp
        playRule = New Zamba.WFExecution.PlayIfDocumentType(Me)
    End Sub
    Public Property DocTypeId() As Int64 Implements IIfDocumentType.DocTypeId
        Get
            Return _DocTypeId
        End Get
        Set(ByVal value As Int64)
            _DocTypeId = value
        End Set
    End Property
    Private _DocTypeId As Int64
    Public Property Comp() As Comparators Implements IIfDocumentType.Comp
        Get
            Return _Comp
        End Get
        Set(ByVal value As Comparators)
            _Comp = value
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

    Private _Comp As Comparators

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)
        Return playRule.Play(results)
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
        Return playRule.Play(results, ifType)
    End Function

    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function
End Class