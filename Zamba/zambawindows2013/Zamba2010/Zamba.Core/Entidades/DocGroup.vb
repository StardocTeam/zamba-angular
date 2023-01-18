Public Class DocGroup
    Inherits ZambaCore
    Implements IDocGroup

#Region " Atributos "
    Private _parentId As Int64
    Private _icon As Integer
    Private _isFull As Boolean
    Private _isLoaded As Boolean
#End Region

#Region " Propiedades "
    Public Property Icon() As Integer Implements IDocGroup.Icon
        Get
            Return _icon
        End Get
        Set(ByVal value As Integer)
            _icon = value
        End Set
    End Property
    Public Property ParentId() As Int64 Implements IDocGroup.ParentId
        Get
            Return _parentId
        End Get
        Set(ByVal value As Int64)
            _parentId = value
        End Set
    End Property
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
#End Region

#Region " Constructores "
    Public Sub New()
        ObjectTypeId = 3
    End Sub
#End Region

    Public Overrides Sub Dispose()

    End Sub

    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub

End Class