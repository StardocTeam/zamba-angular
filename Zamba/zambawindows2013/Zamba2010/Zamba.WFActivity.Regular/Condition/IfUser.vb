Imports Zamba.Core

<RuleMainCategory("Usuario"), RuleCategory("Validar"), RuleSubCategory(""), RuleDescription("Validar Usuario"), RuleHelp("Comprueba el usuario actual para poder tomar una determinada desici�n"), RuleFeatures(False)> _
Public Class IfUser
    Inherits WFRuleParent
    Implements IIfUser, IRuleIFPlay, IRuleValidate
    Private _userid As Int64
    Private _comparator As Comparators
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _isValid As Boolean
    Private playRule As Zamba.WFExecution.PlayIfUser
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
    Public Property UserId() As Int64 Implements IIfUser.UserId
        Get
            Return _userid
        End Get
        Set(ByVal value As Int64)
            _userid = value
        End Set
    End Property
    Public Property Comparator() As UserComparators Implements IIfUser.Comparator
        Get
            Return _comparator
        End Get
        Set(ByVal value As UserComparators)
            _comparator = value
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

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal userId As Int64, ByVal comp As Comparators)
        MyBase.New(Id, Name, wfstepid)
        playRule = New WFExecution.PlayIfUser(Me)
        'Dim users As SortedList = WFUserBusiness.GetUsersByStepID(wfstepid)

        'If userId <> 0 AndAlso users.ContainsKey(userId) Then User = users(userId)
        _userid = userId
        Comparator = comp
    End Sub

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
End Class