''' -----------------------------------------------------------------------------
''' Project	 : Zamba.MessagesControls
''' Interface	 : Controls.IMessage
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Interfaz que debe implementar cualquier objeto del tipo Message
''' Contiene las propiedades comunes a un mail o mensaje interno.
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Interface IMailMessage

    Property MailTo() As Object
    Property De() As Object
    Property CC() As Object
    Property CCO() As Object
    Property Subject() As Object
    Property Body() As Object
    Property Attachs() As Object
    Property Confirmation() As Boolean
    Sub send()

    'Public Enum messageType As Integer
    '    LotusNotes
    '    Internal
    '    Outlook
    '    NetMail
    'End Enum


End Interface

