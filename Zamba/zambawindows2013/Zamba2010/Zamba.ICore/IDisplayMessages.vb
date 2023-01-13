Public Interface IDisplayMessages
    ReadOnly Property Usuario() As String
    ReadOnly Property Asunto() As String
    ReadOnly Property Fecha() As String
    ReadOnly Property De() As String
    ReadOnly Property Para() As String
    ReadOnly Property ConCopia() As String
    ''' <----------------------------------------------------------------------------->
    ''' <summary>
    ''' Compara el objeto Message actual con un objeto message pasado por parametro
    ''' </summary>
    ''' <param name="msge">Objeto Message que se desea comparar con el objeto Message actual</param>
    ''' <returns>
    ''' True si son los mismos
    ''' </returns>
    ''' <remarks>
    ''' Solo se comparan los IDs de los mensajes
    ''' </remarks>
    ''' <history>
    ''' [Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    ''' </----------------------------------------------------------------------------->
    Function Compare(ByVal msge As IInternalMessage) As Boolean
End Interface
