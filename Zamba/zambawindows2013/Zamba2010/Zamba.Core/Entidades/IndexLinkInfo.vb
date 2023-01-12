
Public Class IndexLinkInfo
    Implements IIndexLinkInfo

    Private _Name As String = String.Empty
    Private _id As Integer
    Private _data As String = String.empty
    Private _doctype As Integer
    Private _index As Integer
    Private _flag As IndexLinkFlag
    Private _indexLink As IIndexLink

    Public Property Name() As String Implements IIndexLinkInfo.Name
        Get
            Return _Name
        End Get
        Set(ByVal Value As String)
            _Name = Value
        End Set
    End Property
    Public Property IndexLink() As IIndexLink Implements IIndexLinkInfo.IndexLink1
        Get
            Return _indexLink
        End Get
        Set(ByVal value As IIndexLink)
            _indexLink = value
        End Set
    End Property
    Public Property Flag() As IndexLinkFlag Implements IIndexLinkInfo.Flag1
        Get
            Return _flag
        End Get
        Set(ByVal value As IndexLinkFlag)
            _flag = value
        End Set
    End Property
    Public Property Index() As Integer Implements IIndexLinkInfo.Index1
        Get
            Return _index
        End Get
        Set(ByVal value As Integer)
            _index = value
        End Set
    End Property
    Public Property Doctype() As Integer Implements IIndexLinkInfo.Doctype1
        Get
            Return _doctype
        End Get
        Set(ByVal value As Integer)
            _doctype = value
        End Set
    End Property
    Public Property Data() As String Implements IIndexLinkInfo.Data1
        Get
            Return _data
        End Get
        Set(ByVal value As String)
            _data = value
        End Set
    End Property
    Public Property Id() As Integer Implements IIndexLinkInfo.Id1
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Public Sub New(ByVal indexLink As IndexLink, ByVal id As Integer, ByVal data As String, ByVal flag As IndexLinkFlag, ByVal doctype As Integer, ByVal docindex As Integer, ByVal name As String)
        _id = id
        _data = data
        _flag = flag
        _doctype = doctype
        _index = docindex
        _Name = name
        _indexLink = indexLink
    End Sub

End Class



