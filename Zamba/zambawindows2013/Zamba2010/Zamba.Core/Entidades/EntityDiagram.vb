Public Class EntityDiagram
    Inherits ZambaCore
    Implements IEntityDiagram

#Region " Atributos "
    Private _ObjectTypeID As Integer
    Private _IconId As Integer
    Private _isFull As Boolean
    Private _isLoaded As Boolean
   
#End Region
#Region " Propiedades "
    Public Property ObjectTypeId As Integer Implements IEntityDiagram.ObjectTypeId
        Get
            Return _ObjectTypeID
        End Get
        Set(value As Integer)
            _ObjectTypeID = value
        End Set
    End Property

    Public Property IconId As Integer Implements IEntityDiagram.IconId
        Get
            Return _IconId
        End Get
        Set(value As Integer)
            _IconId = value
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
    Public Sub New(Entity As IDocType)
        ID = Entity.ID
        Name = Entity.Name
        ObjecttypeId = Entity.ObjecttypeId
        IconId = Entity.IconId
    End Sub
    Public Sub New()
    End Sub

#End Region


    Public Overrides Sub Dispose()

    End Sub

    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
End Class
