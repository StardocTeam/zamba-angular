Public Class PublishState
    Inherits Result
    Implements IPublishState

    Private _StateId As Integer
    Private _StateName As String

    Public Property StateId() As Integer Implements IPublishState.StateId
        Get
            Return _StateId
        End Get
        Set(ByVal value As Integer)
            _StateId = value
        End Set
    End Property

    Public Property StateName() As String Implements IPublishState.StateName
        Get
            Return _StateName
        End Get
        Set(ByVal value As String)
            _StateName = value
        End Set
    End Property
End Class
