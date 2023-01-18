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
    Inherits IDisposable
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
    Function Execute() As Boolean
    'Nombre del paquete
    ReadOnly Property Name() As String
    'Descripcion del paquete
    ReadOnly Property Description() As String
    'Orden en que se ejecuta
    ReadOnly Property Orden() As Int64
    'Seria el ID
    ReadOnly Property Number() As EnumPaquetes
    'PaQuetes q deben correr antes
    ReadOnly Property DependenciesIDs() As Generic.List(Of Int64)
    'Devuelve true si el paquete ya fue instalado
    Property Installed() As Boolean
End Interface