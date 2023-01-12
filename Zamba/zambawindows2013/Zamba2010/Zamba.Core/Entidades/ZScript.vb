''' <summary>
''' Clase que se utiliza para la ejecucion de los scripts de Zamba
''' </summary>
''' <remarks></remarks>
Public Class ZScript
    Public Sub New(ByVal id As Int64, ByVal tipo As String, ByVal name As String, ByVal descripcion As String, ByVal obligatoriedad As String, ByVal fecha As String, ByVal consulta As String) ', ByVal Dependencias As String)
        _id = id
        _name = name
        _tipo = tipo
        _descripcion = descripcion
        _obligatoriedad = obligatoriedad
        _fecha = fecha
        _consulta = consulta
        '_Dependencias = Dependencias
    End Sub

    Dim _id As Int64
    Dim _name As String
    Dim _tipo As String
    Dim _descripcion As String
    Dim _obligatoriedad As String
    Dim _fecha As String
    Dim _consulta As String
    'Dim _Dependencias As String

    ''' <summary>
    ''' Sobre escribo el tostring() para que muestre la fecha y el nombre
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function ToString() As String
        Return Id & ": " & Name & ": " & Fecha
    End Function

    Public ReadOnly Property Id As Int64
        Get
            Return _id
        End Get
    End Property

    Public ReadOnly Property Name As String
        Get
            Return _name
        End Get
    End Property

    Public ReadOnly Property Tipo As String
        Get
            Return _tipo
        End Get
    End Property

    Public ReadOnly Property Descripcion As String
        Get
            Return _descripcion
        End Get
    End Property

    Public ReadOnly Property Obligatoriedad As String
        Get
            Return _obligatoriedad
        End Get
    End Property
    Public ReadOnly Property Fecha As String
        Get
            Return _fecha
        End Get
    End Property
    Public ReadOnly Property Consulta As String
        Get
            Return _consulta
        End Get
    End Property
End Class