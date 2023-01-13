Public Interface IOutLookMail
    Inherits IMailMessage, IZClass

    Property Importance() As String
    Property DeliveryReport() As String
    Property Attachments() As Generic.List(Of String)
    Property SaveOnSend() As Boolean
    Property ReturnReceipt() As Boolean
    ''' <----------------------------------------------------------------------------->
    ''' <summary>
    ''' Propiedad para armar el boton "VER EN ZAMBA"
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [Hernan]	14/06/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    ''' </----------------------------------------------------------------------------->
    Property slink() As String
    Property ReplyTo() As String
    ''' <----------------------------------------------------------------------------->
    ''' <summary>
    ''' Metodo para enviar un mail con OutLookNotes.
    ''' </summary>
    ''' <remarks>
    ''' Utiliza los objetos de OutLook, sino esta instalado OutLook, no funciona.
    ''' </remarks>
    ''' <history>
    ''' [Hernan]	15/06/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    ''' </----------------------------------------------------------------------------->
    Sub addAttach(ByVal str As String)
    Sub addAttachResult(ByVal Results As ArrayList)
    Sub addDest(ByVal dest As Object)
    Sub addDestRange(ByVal des As System.Collections.ArrayList)
End Interface