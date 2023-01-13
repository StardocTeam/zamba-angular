Public Class Restriction
    Implements IRestriction

#Region " Atributos "
    Private _id As Int32
    Private _docTypeId As Int64
    Private _value As String
    Private _indexId As Int32
    Private _name As String

#End Region

#Region " Propiedades "
    Public Property Id() As Int32 Implements IRestriction.Id
        Get
            Return _id
        End Get
        Set(ByVal value As Int32)
            _id = value
        End Set
    End Property
    Public Property DocTypeId() As Int32 Implements IRestriction.DocTypeId
        Get
            Return _docTypeId
        End Get
        Set(ByVal value As Int32)
            _docTypeId = value
        End Set
    End Property
    Public Property Value() As String Implements IRestriction.Value
        Get
            Return _value
        End Get
        Set(ByVal value As String)
            _value = value
        End Set
    End Property
    Public Property IndexId() As Int32 Implements IRestriction.IndexId
        Get
            Return _indexId
        End Get
        Set(ByVal value As Int32)
            _indexId = value
        End Set
    End Property
    Public Property Name() As String Implements IRestriction.Name
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property
#End Region

#Region " Constructores "
    Public Sub New()

    End Sub
#End Region

End Class