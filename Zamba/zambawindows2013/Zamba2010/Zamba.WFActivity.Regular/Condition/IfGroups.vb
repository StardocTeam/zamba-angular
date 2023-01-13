Imports Zamba.Core
Imports System.Text

<RuleMainCategory("Usuario"), RuleCategory("Validar"), RuleSubCategory(""), RuleDescription("Validar los Usuarios de Grupo"), RuleHelp("Comprueba si el usuario pertenece al grupo para tomar una desición"), RuleFeatures(False)> _
Public Class IfGroups
    Inherits WFRuleParent
    Implements IIfGroups, IRuleIFPlay, IRuleValidate
    Private _groupList As New List(Of Int64)
    Private _comparator As UserComparators
    Private _strBuilder As New StringBuilder
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _isValid As Boolean
    Private playRule As Zamba.WFExecution.PlayIfGroups
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
    Public Property groupList() As List(Of Int64) Implements IIfGroups.GroupList
        Get
            Return _groupList
        End Get
        Set(ByVal value As List(Of Int64))
            _groupList = value
        End Set
    End Property
    Public Property Comparator() As UserComparators Implements IIfGroups.Comparator
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
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal GroupsID As String, ByVal comparator As Integer)
        MyBase.New(Id, Name, wfstepid)
        If GroupsID <> "" Then
            Dim intGroupID As Int64
            For Each GroupID As String In GroupsID.Split("|")
                If Int64.TryParse(GroupID, intGroupID) Then
                    If groupList.Contains(GroupID) = False Then
                        groupList.Add(GroupID)
                    End If
                End If
            Next
        End If
        Me.Comparator = DirectCast(comparator, UserComparators)
        playRule = New WFExecution.PlayIfGroups(Me)
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
    Public Overrides ReadOnly Property MaskName() As String
        Get
            Dim strBuilder As New StringBuilder
            Return "Valido o Realizo"
            Select Case Comparator
                Case UserComparators.AssignedTo
                    _strBuilder.Append("Valida si cada documento esta asignado a ")
                    BuildMaskName()
                Case UserComparators.NotAsignedTo
                    _strBuilder.Append("Valida si cada documento NO esta asignado a ")
                    BuildMaskName()
                Case UserComparators.CurrentUser
                    _strBuilder.Append("Valida que el usuario esté entre ")
                    BuildMaskName()
                Case UserComparators.NotCurrentUser
                    _strBuilder.Append("Valida que el usuario NO esté entre ")
                    BuildMaskName()
                Case Else
                    Return ""
            End Select
            Return _strBuilder.ToString()
        End Get
    End Property
    ''' <summary>
    ''' Añade al MaskName los nombres de los usuarios seleccionados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BuildMaskName()
        For Each UserId As Int64 In groupList
            _strBuilder.Append(UserGroupBusiness.GetUserorGroupNamebyId(UserId))
            _strBuilder.Append(", ")
        Next
        _strBuilder.Remove(_strBuilder.Length - 2, 2) 'Quito la ultima coma y espacio
        _strBuilder.Append(".")
    End Sub
End Class