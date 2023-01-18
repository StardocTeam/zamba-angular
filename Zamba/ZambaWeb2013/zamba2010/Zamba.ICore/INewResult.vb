Public Interface INewResult
    Inherits IResult

    Property DsVols() As DataSet
    Property State() As States
    Property FlagCopyVerify() As Boolean
    Property Ready() As Boolean
    Property NewFile() As String
    Property Volume() As IVolume
    Property VolumeListId() As Int32
    Property Comment() As String
    Property EncodedFile() As Byte()
    ''' <----------------------------------------------------------------------------->
    ''' <summary>
    ''' Obtiene la cantidad de atributos de un tipo determidado que contiene
    ''' </summary>
    ''' <param name="indextypeId">Tipo de indice enumerado</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    ''' </----------------------------------------------------------------------------->
    Function GetIndex(ByVal indextypeId As Integer) As Integer
    ''' <----------------------------------------------------------------------------->
    ''' <summary>
    ''' Devuelve la extensión del archivo
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    ''' </----------------------------------------------------------------------------->
    ReadOnly Property Extension() As String
    ReadOnly Property FileName() As String
    Property FileLength() As Decimal
    ''' <----------------------------------------------------------------------------->
    ''' <summary>
    ''' Serializa el objeto NewResult en un Archivo
    ''' </summary>
    ''' <param name="filename">Nombre del archivo donde se va a serializar el objeto NewResult</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    ''' </----------------------------------------------------------------------------->
    Sub FileSerialize(ByVal filename As String)
End Interface