Public Interface IPPostProcess
    Function process(ByVal Files As ArrayList, Optional ByVal param As ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As ArrayList

    Function GetXml(Optional ByVal xml As String = Nothing) As String
    Sub SetXml(Optional ByVal xml As String = Nothing)


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Procesa un archivo
    ''' </summary>
    ''' <param name="File">Archivo a procesar</param>
    ''' <param name="param">Opcional, parametros para el preproceso</param>
    ''' <param name="xml"></param>
    ''' <param name="Test"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve la ayuda del postProcess
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	14/08/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Function GetHelp() As String
    Event PreprocessMessage(ByVal msg As String)
    Event PreprocessError(ByVal Errormsg As String)

End Interface


