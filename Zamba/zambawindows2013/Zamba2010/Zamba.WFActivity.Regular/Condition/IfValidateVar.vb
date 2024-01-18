Imports Zamba.Core

<RuleMainCategory("Variables"), RuleCategory(""), RuleSubCategory(""), RuleDescription("Validar Variable"), RuleHelp("Valida variables ingresadas por el usuario"), RuleFeatures(False)> _
Public Class IfValidateVar
    Inherits WFRuleParent
    Implements IIfValidateVar, IRuleIFPlay, IRuleValidate

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


#Region "Atributos"
    Private _TxtVar As String
    Private _Operador As Comparadores
    Private _TxtValue As String
    Private _caseInsensitive As Boolean
    Private _isValid As Boolean

    Private playRule As Zamba.WFExecution.PlayIfValidateVar
#End Region


    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal _TmpVar As String, ByVal _TmpOper As Int32, ByVal _TmpValue As String, ByVal TmpCaseSensitive As Boolean)
        MyBase.New(Id, Name, wfstepid)
        CaseInsensitive = TmpCaseSensitive
        TxtVar = _TmpVar
        TxtValue = _TmpValue
        Operador = _TmpOper
        playRule = New Zamba.WFExecution.PlayIfValidateVar(Me)
    End Sub

    Public Property Operador() As Core.Comparadores Implements Core.IIfValidateVar.Operador
        Get
            Return _Operador
        End Get

        Set(ByVal value As Core.Comparadores)
            _Operador = value
        End Set

    End Property

    Public Property TxtValue() As String Implements Core.IIfValidateVar.TxtValue
        Get
            Return _TxtValue
        End Get

        Set(ByVal value As String)
            _TxtValue = value
        End Set

    End Property

    Public Property TxtVar() As String Implements Core.IIfValidateVar.TxtVar
        Get
            Return _TxtVar
        End Get

        Set(ByVal value As String)
            _TxtVar = value
        End Set

    End Property

    '[German] 04/07/2009 - Definicion del CaseSensitive
    Public Property CaseInsensitive() As Boolean Implements Core.IIfValidateVar.CaseInsensitive

        Get
            Return _caseInsensitive
        End Get
        Set(ByVal value As Boolean)
            _caseInsensitive = value
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

    ''' <summary>
    ''' Ejecuta la regla devolviendo los results que cumplan con la condicion requerida
    ''' </summary>
    ''' <param name="results">Results a validar</param>
    ''' <param name="ifType">True si quiero que devuelve los results que coincidan con la validacion, false si quiero los que no coincidan</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''      [Ezequiel] 09/06/2009 - Se elimino el Try-Catch
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

    Public Overrides Sub Dispose()

    End Sub

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides Sub Load()

    End Sub

End Class
