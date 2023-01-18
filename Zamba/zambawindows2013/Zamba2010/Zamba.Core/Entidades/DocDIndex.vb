
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' CLASE QUE CONTIENE LOS DATOS DE LOS ATRIBUTOS QUE TIENE ASIGNADO la entidad    '
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Public Class DocDIndex
    Implements IDocDIndex
#Region " Atributos "
    Private _id As Integer
    Private _name As String = String.Empty
#End Region

#Region " Propiedades "
    Public Property Id() As Integer Implements IDocDIndex.Id
        Get
            Return _id
        End Get
        Set(ByVal Value As Integer)
            _id = Value
        End Set
    End Property

    Public Property Name() As String Implements IDocDIndex.Name
        Get
            Return _name
        End Get
        Set(ByVal Value As String)
            _name = Value
        End Set
    End Property
#End Region

#Region " Constructores "
    Public Sub New(ByVal index_id As Integer, ByVal index_name As String)
        _id = index_id
        _name = index_name
    End Sub
#End Region

End Class