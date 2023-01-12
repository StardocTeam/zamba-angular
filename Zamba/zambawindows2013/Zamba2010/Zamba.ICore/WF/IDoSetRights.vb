Public Interface IDoSetRights
    Inherits IRule

    ''' <summary>
    ''' Datos de los permisos codificados que provienen de la base de datos
    ''' </summary>
    ''' <value>Formato string separado por caracteres</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EncodedRights() As String

    ''' <summary>
    ''' Lista con la información para habilitar o deshabilitar permisos 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Rights() As Dictionary(Of RightsType, Boolean)

End Interface
