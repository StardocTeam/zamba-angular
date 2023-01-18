Public Class ZFeed
    Implements IZFeed
    Private _id As Long
    Private _name As String
    Private _createDate As Date
    Private _feedType As FeedTypes
    Private _readed As Boolean
    Private _text As String

    Public Property ID As Long Implements ICore.ID
        Get
            Return _id
        End Get
        Set(ByVal value As Long)
            _id = value
        End Set
    End Property

    Public Property Name As String Implements ICore.Name
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Public Property CreateDate As Date Implements IZFeed.CreateDate
        Get
            Return _createDate
        End Get
        Set(ByVal value As Date)
            _createDate = value
        End Set
    End Property

    Public Property FeedType As FeedTypes Implements IZFeed.FeedType
        Get
            Return _feedType
        End Get
        Set(ByVal value As FeedTypes)
            _feedType = value
        End Set
    End Property

    Public Property Readed As Boolean Implements IZFeed.Readed
        Get
            Return _readed
        End Get
        Set(ByVal value As Boolean)
            _readed = value
        End Set
    End Property

    Public Property Text As String Implements IZFeed.Text
        Get
            Return _text
        End Get
        Set(ByVal value As String)
            _text = value
        End Set
    End Property

    Public Sub New()

    End Sub

    Public Sub New(ByVal pId As Long, ByVal pName As String, ByVal pCreateDate As Date, ByVal pFeedType As FeedTypes, ByVal pReaded As Boolean, ByVal pText As String)
        _id = pId
        _name = pName
        _createDate = pCreateDate
        _feedType = pFeedType
        _readed = pReaded
        _text = pText
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            _createDate = Nothing
            _feedType = Nothing
            _text = String.Empty
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
