
Public Class IndexLink
    Implements IIndexLink
#Region " Atributos "
    Private _id As Integer 'Id del Vinculo
    Private _name As String = String.Empty  ' Nombre dado por el usuario
    Private _description As String = String.Empty  'Descricion del vinculo 
    Private _links As New ArrayList() 'Lista de entidades e atributos que estan vinculados
#End Region

#Region " Propiedades "
    Public Property Name() As String Implements IIndexLink.Name
        Get
            Return _name
        End Get
        Set(ByVal Value As String)
            _name = Value
        End Set
    End Property
    Public Property Id() As Integer Implements IIndexLink.Id
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property
    Public Property Description() As String Implements IIndexLink.Description
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property
    Public Property Links() As ArrayList Implements IIndexLink.Links
        Get
            Return _links
        End Get
        Set(ByVal value As ArrayList)
            _links = value
        End Set
    End Property

#End Region

#Region " Constructores "

    'Constructor Para recuperar de la base de datos
    Public Sub New(ByVal id As Integer, ByVal Name As String, ByVal Description As String, ByVal Links As ArrayList)
        _id = id
        _Name = Name
        _description = Description
        'cargo los Links de la base
        'Carga la info asignada a un vinculo 
        _links = Links
    End Sub

    'Constructor para crear un link nuevo 
    'Ya queda persistido en la base para obtener un ID
    Public Sub New(ByVal Name As String)
        _name = Name
    End Sub

#End Region

End Class
