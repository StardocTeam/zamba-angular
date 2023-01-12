Public Class ZCoreView
    Inherits ZBaseCore

    Private Sub New()
    End Sub
    Public Sub New(ByVal Id As Int64, ByVal Name As String)
        Me.ID = Id
        Me.Name = Name
    End Sub
    Public Overrides Sub Dispose()

    End Sub

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides ReadOnly Property IsFull() As Boolean
        Get

        End Get
    End Property

    Public Overrides ReadOnly Property IsLoaded() As Boolean
        Get

        End Get
    End Property

    Public Overrides Sub Load()

    End Sub
End Class
