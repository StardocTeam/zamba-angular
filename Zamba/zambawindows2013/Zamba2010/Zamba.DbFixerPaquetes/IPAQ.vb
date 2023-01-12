''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Paquetes
''' Interface	 : Paquetes.IPAQ
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Interfaz que deben implementar todos los paquetes de actualización
''' </summary>
''' <remarks>
''' Todas las propiedades deben ser completadas para cada paquete creado
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' 	[Marcelo]	16/12/2007	Modified
''' </history>
''' -----------------------------------------------------------------------------
Public Interface IPAQ

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Ejecuta el paquete
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' 	[Marcelo]	16/12/2007	Modified
    ''' </history>
    ''' -----------------------------------------------------------------------------
    'Ejecucion del paquete
    Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean
    'Nombre del paquete
    ReadOnly Property name() As String
    'Descripcion del paquete
    ReadOnly Property description() As String
    'Orden en que se ejecuta
    ReadOnly Property orden() As Int64
    'Seria el ID
    ReadOnly Property number() As EnumPaquetes
    'PaQuetes q deben correr antes
    'ReadOnly Property dependenciesIDs() As Generic.List(Of Int64)
    'Devuelve true si el paquete ya fue instalado
    Property installed() As Boolean
End Interface