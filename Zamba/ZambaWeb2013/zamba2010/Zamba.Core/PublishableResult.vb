Public Class PublishableResult
    Inherits Result
    Implements Ipublishable

#Region "Atributos"
    Private _publishId As Int32
    Private _publishDate As Date
    Private _publisher As IUser = Nothing
    Private _docId As Int64
#End Region

#Region "Propiedades"
    Public Property PublishId() As Int32 Implements Ipublishable.PublishId
        Get
            Return _publishId
        End Get
        Set(ByVal value As Int32)
            _publishId = value
        End Set
    End Property
    Public Property PublishDate() As Date Implements Ipublishable.PublishDate
        Get
            Return _publishDate
        End Get
        Set(ByVal value As Date)
            _publishDate = value
        End Set
    End Property
    Public Property Publisher() As IUser Implements Ipublishable.Publisher
        Get
            If IsNothing(_publisher) Then CallForceLoad(Me)
            If IsNothing(_publisher) Then _publisher = New User()

            Return _publisher
        End Get
        Set(ByVal value As IUser)
            _publisher = value
        End Set
    End Property
    Public Property DocId() As Int64 Implements Ipublishable.DocId
        Get
            Return _docId
        End Get
        Set(ByVal value As Int64)
            _docId = value
        End Set
    End Property
#End Region
#Region "Contructor"
    Public Sub New()

    End Sub
#End Region

    Public Overrides Sub Load()

    End Sub
    Public Overrides Sub FullLoad()

    End Sub
End Class