Imports Zamba.Core
Imports System.Collections.Generic
Imports System.Text

<RuleCategory("Usuarios"), RuleDescription("Validar Usuarios"), RuleHelp("Comprueba la asignación de una tarea contra un determinado grupo de usuarios"), RuleFeatures(False)> _
Public Class IfUsers
    Inherits WFRuleParent
    Implements IIfUsers, IRuleIFPlay
    Private _userList As New Hashtable
    Private _comparator As UserComparators
    Private _strBuilder As New StringBuilder
    Private _isLoaded As Boolean
    Private _isFull As Boolean

    Dim UserBusiness As New UserBusiness

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
    Public Property UserList() As Hashtable Implements IIfUsers.UserList
        Get
            Return _userList
        End Get
        Set(ByVal value As Hashtable)
            _userList = value
        End Set
    End Property
    Public Property Comparator() As UserComparators Implements IIfUsers.Comparator
        Get
            Return _comparator
        End Get
        Set(ByVal value As UserComparators)
            _comparator = value
        End Set
    End Property
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal usersID As String, ByVal comparator As Integer)
        MyBase.New(Id, Name, wfstepid)
        If usersID <> "" Then
            For Each userID As String In usersID.Split("|")
                Dim user As IUser = UserBusiness.GetUserById(CInt(userID))
                If Not IsNothing(user) Then
                    UserList.Add(user.ID, user)
                Else
                    Exit For
                End If
            Next
        End If
        Me.Comparator = DirectCast(comparator, UserComparators)
    End Sub

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayIfUsers()
        Return playRule.Play(results, Me)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayIfUsers()
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
        Dim playRule As New Zamba.WFExecution.PlayIfUsers()
        Return playRule.Play(results, Me, ifType)
    End Function

    ''' <summary>
    ''' Añade al MaskName los nombres de los usuarios seleccionados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BuildMaskName()
        For Each User As IUser In UserList.Values
            _strBuilder.Append(User.Name)
            _strBuilder.Append(", ")
        Next
        _strBuilder.Remove(_strBuilder.Length - 2, 2) 'Quito la ultima coma y espacio
        _strBuilder.Append(".")
    End Sub
End Class