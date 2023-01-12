Public Interface IZMessageForm
    '[Ezequiel] 04/08/09 Se agrego parametro de automail
    '[Ezequiel] 08/05/09 Se agrego parametro de Links
    Sub EspecificarDatos(ByVal ToStr As String, ByVal CC As String, ByVal CCo As String, ByVal Subject As String, ByVal Body As String, ByVal paths() As String, Optional ByVal AssociatedDocuments As List(Of IResult) = Nothing, Optional ByVal Automatic As Boolean = False, Optional ByVal GotLinks As ArrayList = Nothing, Optional ByVal EmbedImages As Boolean = False, Optional ByRef mailpath As String = "", Optional ByVal IsWF As Boolean = False)
    '[Alejandro] 09/12/09 Metodo para especificar los ids del documento para guardar el historial desde las reglas DOMail y DOAutoMail
    Sub EspecificarDatosDoc(ByVal DocId As Int64, ByVal docTypeId As Int64, ByVal StepID As Int64, ByVal MailEvent As MailEvent)
End Interface
