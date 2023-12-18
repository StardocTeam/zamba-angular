Imports Zamba.Core
Imports Zamba.Data

<RuleCategory("Tareas"), RuleDescription("Validar Entidad"), RuleHelp("Permite realizar una validadci�n del entidad para tomar una desici�n determinada"), RuleFeatures(False)> _
Public Class IfDocumentType
    Inherits WFRuleParent
    Implements IIfDocumentType, IRuleIFPlay
    Private _isLoaded As Boolean
    Private _isFull As Boolean
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

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal DocTypeId As Int32, ByVal Comp As Comparators)
        MyBase.New(Id, Name, wfstepid)
        Me._DocTypeId = DocTypeId
        Me._Comp = Comp
        Me.playRule = New Zamba.WFExecution.PlayIfDocumentType(Me)
    End Sub
    Public Property DocTypeId() As Int32 Implements IIfDocumentType.DocTypeId
        Get
            Return _DocTypeId
        End Get
        Set(ByVal value As Int32)
            _DocTypeId = value
        End Set
    End Property
    Private _DocTypeId As Int32
    Public Property Comp() As Comparators Implements IIfDocumentType.Comp
        Get
            Return _Comp
        End Get
        Set(ByVal value As Comparators)
            _Comp = value
        End Set
    End Property
    Private _Comp As Comparators

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
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
End Class


'Inherits WFRuleParent
'Private Sub New()
'    MyBase.New(0, "", Nothing)
'End Sub

'Public Sub New(ByVal Id As Int64, ByVal Name As String, byref wfstep As WFStep)
'    MyBase.New(Id, Name, WFStep)
'End Sub

'Public Overloads Overrides Function Play(ByVal Results As SortedList) As SortedList
'    Dim NewSortedList As New SortedList
'    Try
'        For Each r As TaskResult In results.Values
'            NewSortedList.Add(r.Id, r)
'        Next
'    Catch ex As Exception
'       zamba.core.zclass.raiseerror(ex)
'    End Try
'    Return NewSortedList
'End Function

'Public Overrides ReadOnly Property MaskName() As String
'    Get
'        Try
'            Return "Valido o Realizo"
'        Catch ex As Exception
'           zamba.core.zclass.raiseerror(ex)
'        End Try
'    End Get
'End Property
'End Class