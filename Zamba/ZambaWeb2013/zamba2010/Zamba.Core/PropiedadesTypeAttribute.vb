<AttributeUsage(AttributeTargets.Property)>
Public Class PropiedadesTypeAttribute
    Inherits Attribute
    Implements IPropiedadesTypeAttribute

#Region " Atributos "
    Protected oPropiedad As Propiedades
#End Region

#Region " Propiedades "
    Public Property Propiedad() As Propiedades Implements IPropiedadesTypeAttribute.Propiedad
        Get
            Return oPropiedad
        End Get
        Set(ByVal Value As Propiedades)
            oPropiedad = Value
        End Set
    End Property
#End Region

#Region " Constructores "
    Public Sub New(ByVal Propiedad As Propiedades)
        Me.Propiedad = Propiedad
    End Sub
#End Region

End Class


<AttributeUsage(AttributeTargets.Property)>
Public Class PropiedadesParamAttribute
    Inherits Attribute
    Implements IPropiedadesParamAttribute

    Public Property Propiedad() As String() Implements IPropiedadesParamAttribute.Parametro


#Region " Constructores "
    Public Sub New(ByVal Propiedad As String())
        Me.Propiedad = Propiedad
    End Sub

#End Region

End Class


<AttributeUsage(AttributeTargets.Property)>
Public Class PropiedadesReturnTypeAttribute
    Inherits Attribute
    Implements IPropiedadesReturnTypeAttribute

    Public Property ReturnType() As String() Implements IPropiedadesReturnTypeAttribute.ReturnType


#Region " Constructores "
    Public Sub New(ByVal ReturnType As String())
        Me.ReturnType = ReturnType
    End Sub

#End Region

End Class