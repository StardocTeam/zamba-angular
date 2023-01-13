Imports Zamba.Core

<RuleMainCategory("Tareas"), RuleCategory(""), RuleSubCategory(""), RuleDescription("Eliminar Tarea"), RuleHelp("Permite borrar una tarea de un Work Flow o en su totalidad"), RuleFeatures(False)> <Serializable()> _
Public Class DoDelete
    Inherits WFRuleParent
    Implements IDoDelete, IRuleValidate
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDODELETE
    Private _isValid As Boolean
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

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal TipoBorrado As Int32, ByVal KeepFile As Boolean)
        MyBase.New(Id, Name, wfstepid) ', ListofRules.DoDelete)
        Me.TipoBorrado = TipoBorrado
        DeleteFile = KeepFile 'no dar vuelta el booleano
        playRule = New WFExecution.PlayDODELETE(Me)
    End Sub

    Private Borrado As Borrados
    Private _DeleteFile As Boolean

    'Public Overloads Overrides Function Play(ByVal Results As SortedList) As System.Collections.SortedList
    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)

        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function

    Public Property TipoBorrado() As Borrados Implements IDoDelete.TipoBorrado
        Get
            Return Borrado
        End Get
        Set(ByVal Value As Borrados)
            Borrado = Value
        End Set
    End Property
    Public Property DeleteFile() As Boolean Implements IDoDelete.DeleteFile
        Get
            Return _DeleteFile
        End Get
        Set(ByVal Value As Boolean)
            _DeleteFile = Value
        End Set
    End Property

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return "Borro la tarea"
        End Get
    End Property
End Class




