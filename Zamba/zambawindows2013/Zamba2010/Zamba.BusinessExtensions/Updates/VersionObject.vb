Public Class VersionObject

    Private _id As String
    Public Property ID() As String
        Get
            Return _id
        End Get
        Set(ByVal value As String)
            _id = value
        End Set
    End Property

    Private _versionNumber As String
    Public Property VersionNumber() As String
        Get
            Return _versionNumber
        End Get
        Set(ByVal value As String)
            _versionNumber = value
        End Set
    End Property

    Private _filePath As String
    Public Property FilePath() As String
        Get
            Return _filePath
        End Get
        Set(ByVal value As String)
            _filePath = value
        End Set
    End Property



End Class
