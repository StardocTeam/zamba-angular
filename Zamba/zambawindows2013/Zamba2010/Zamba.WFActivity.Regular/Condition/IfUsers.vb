Imports Zamba.Core
Imports System.Text

<RuleMainCategory("Usuario"), RuleCategory("Validar"), RuleSubCategory(""), RuleDescription("Validar Usuarios"), RuleHelp("Comprueba la asignaci�n de una tarea contra un determinado grupo de usuarios"), RuleFeatures(False)> _
Public Class IfUsers
    Inherits WFRuleParent
    Implements IIfUsers, IRuleIFPlay, IRuleValidate
    Private _userIdsList As New List(Of Int64)
    Private _comparator As UserComparators
    Private _strBuilder As New StringBuilder
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Dim usersIDs As String
    Private _isValid As Boolean
    Private playRule As Zamba.WFExecution.PlayIfUsers

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
    Public Property UserIdsList() As List(Of Int64) Implements IIfUsers.UserIdsList
        Get
            Return _userIdsList
        End Get
        Set(ByVal value As List(Of Int64))
            _userIdsList = value
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

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return String.Empty
        End Get
    End Property

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal usersID As String, ByVal comparator As Integer)
        MyBase.New(Id, Name, wfstepid)
        playRule = New WFExecution.PlayIfUsers(Me)
        If usersIDs <> "" Then
            For Each userID As String In usersIDs.Split("|")
                ' Dim user As IUser = UserBusiness.GetUserById(CInt(userID))
                'If Not IsNothing(user) Then
                UserIdsList.Add(userID)
                ' Else
                '  Exit For
                ' End If
            Next
        End If

        Me.Comparator = DirectCast(comparator, UserComparators)

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

    ''' <summary>
    ''' A�ade al MaskName los nombres de los usuarios seleccionados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BuildMaskName()
        If usersIDs <> "" Then
            For Each userID As Int64 In UserIdsList
                Dim user As IUser = UserBusiness.GetUserById(CInt(userID))
                If Not IsNothing(user) Then
                    _strBuilder.Append(user.Name)
                    _strBuilder.Append(", ")
                Else
                    Exit For
                End If
            Next
        End If

        _strBuilder.Remove(_strBuilder.Length - 2, 2) 'Quito la ultima coma y espacio
        _strBuilder.Append(".")
    End Sub
End Class