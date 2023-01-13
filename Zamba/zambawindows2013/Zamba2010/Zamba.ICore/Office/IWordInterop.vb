
Public Interface IWordInterop
    Sub Activate(ByVal word As Object)
    Function Print(ByVal Doc As String, ByVal PrinterName As String) As Boolean
    Function PrintToDefaultPrinter(ByVal Doc As String) As Boolean

    ''' <summary>
    '''     Imprime un documento de word a traves de la API de Microsoft y con el worddocument por referencia
    ''' </summary>
    ''' <param name="Doc"></param>
    ''' <param name="app"></param>
    ''' <param name="PrinterName"></param>
    ''' <returns></returns>
    ''' <history>
    '''     Javier  28/10/2010  Created
    '''</history>
    Function PrintWithWord(ByVal Doc As Object, ByVal app As Object, Optional ByVal PrinterName As String = "") As Boolean

    ''' <summary>
    '''     Devuelve todo el texto de un documento de Word
    ''' </summary>
    ''' <param name="worddoc"></param>
    ''' <returns></returns>
    ''' <history>
    '''     Javier  02/11/2010  Created
    '''</history>
    Function GetAllText(ByVal docobj As Object) As String
End Interface

