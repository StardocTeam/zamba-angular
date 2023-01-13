Imports Zamba.Core
Imports System.Collections.Generic
Imports System.Text

<RuleCategory("Usuarios"), RuleDescription("Validar los Usuarios de Grupo"), RuleHelp("Comprueba si el usuario pertenece al grupo para tomar una desición"), RuleFeatures(False)> _
Public Class IfGroups
    Inherits WFRuleParent
    Implements IIfGroups, IRuleIFPlay
    Private _groupList As New List(Of Int64)
    Private _comparator As UserComparators
    Private _strBuilder As New StringBuilder
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
    End Sub

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayIfGroups()
        Return playRule.Play(results, Me)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayIfGroups()
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
    '''      [Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    ''' </history>
    Public Function PlayIf(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of ITaskResult) Implements IRuleIFPlay.PlayIf
        Dim playRule As New Zamba.WFExecution.PlayIfGroups()
        Return playRule.Play(results, Me, ifType)
    End Function

End Class