Public Class ViewTaskResult
    Implements IViewTaskResult

#Region " Atributos "
    Private _taskResult As ITaskResult
    Private _selected As Boolean
#End Region

#Region " Propiedades "
    Public Property Id() As Int64 Implements IViewTaskResult.Id
        Get
            Return _taskResult.ID
        End Get
        Set(ByVal value As Int64)
            _taskResult.ID = value
        End Set
    End Property

    Public Property ExpireDate() As Date Implements IViewTaskResult.ExpireDate
        Get
            Return _taskResult.ExpireDate
        End Get
        Set(ByVal Value As Date)
            _taskResult.ExpireDate = Value
        End Set
    End Property

    Public Property State() As String Implements IViewTaskResult.State
        Get
            Return _taskResult.State.Name
        End Get
        Set(ByVal Value As String)
            _taskResult.State.Name = Value
        End Set
    End Property

    Public Property TaskState() As TaskStates Implements IViewTaskResult.TaskState
        Get
            Return _taskResult.TaskState
        End Get
        Set(ByVal value As TaskStates)
        End Set
    End Property

    Public Property IsExpired() As Boolean Implements IViewTaskResult.IsExpired
        Get
            Return _taskResult.IsExpired
        End Get
        Set(ByVal Value As Boolean)
        End Set
    End Property

    Public Property Name() As String Implements IViewTaskResult.Name
        Get
            Return _taskResult.Name
        End Get
        Set(ByVal Value As String)
            _taskResult.Name = Value
        End Set
    End Property

    Public Property Selected() As Boolean Implements IViewTaskResult.Selected
        Get
            Return _selected
        End Get
        Set(ByVal Value As Boolean)
            _selected = Value
        End Set
    End Property

    Public Property WfStepName() As String Implements IViewTaskResult.WfStepName
        Get
            Return _taskResult.WfStep.Name
        End Get
        Set(ByVal Value As String)
            _taskResult.WfStep.Name = Value
        End Set
    End Property

    Public Property ImageURL() As String Implements IViewTaskResult.ImageURL
        Get
            Select Case _taskResult.IsExpired
                Case True
                    Return "~\Images\Expired.bmp"
                Case False
                    Return "~\Images\NotExpired.bmp"
                Case Else
                    Return String.Empty
            End Select
        End Get
        Set(ByVal Value As String)

        End Set
    End Property
#End Region

#Region " Constructores "
    Public Sub New(ByVal taskResult As TaskResult)
        _taskResult = taskResult
    End Sub
#End Region

End Class