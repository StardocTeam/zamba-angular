Public Interface IInternalMessage
    Property Destinatarios() As ArrayList
    Property Owner_User_ID() As Integer
    Property Deleted() As Boolean
    Property Id() As Int64
    Property Fecha() As Date
    Property Read() As Boolean
    Property De() As Integer
    Property UserName() As String
    Property Subject() As String
    Property Body() As String
    Property Confirmation() As Boolean
    Property AttachList() As ArrayList
    Property AttachsNames() As ArrayList
    ReadOnly Property CC() As ArrayList
    Property CCStr() As String
    ReadOnly Property CCO() As ArrayList
    Property CCOStr() As String
    Property ToStr() As String
    ReadOnly Property TOUSER() As ArrayList
    Property DeStr() As String
    ''' <----------------------------------------------------------------------------->
    ''' <summary>
    ''' Funcion que devuelve si un usuario leyo ya el correo. El usuario debe figurar entre los destinatarios del correo
    ''' </summary>
    ''' <param name="userid">Id del usuario</param>
    ''' <returns>Boolean</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    ''' </----------------------------------------------------------------------------->
    Function userRead(ByVal userid As Integer) As Boolean
    ''' <----------------------------------------------------------------------------->
    ''' <summary>
    ''' Metodo que elimina a un usuario de la lista de destinatarios
    ''' </summary>
    ''' <param name="userId">Id del usuario que se quitará</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    ''' </----------------------------------------------------------------------------->
    Sub clearDest(ByVal userId As Integer)
    ''' <----------------------------------------------------------------------------->
    ''' <summary>
    ''' Devuelve un objeto Mensaje con las propiedades de un Mensaje Reenviado para el usuario actual
    ''' </summary>
    ''' <returns>Objeto Message</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    ''' </----------------------------------------------------------------------------->
    Function getResend() As IInternalMessage
    ''' <----------------------------------------------------------------------------->
    ''' <summary>
    ''' Devuelve un objeto Mensaje con las propiedades de un Mensaje Respondido para el usuario actual
    ''' </summary>
    ''' <returns>Objeto Message</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    ''' </----------------------------------------------------------------------------->
    Function GetResponse() As IInternalMessage
    ''' <----------------------------------------------------------------------------->
    ''' <summary>
    ''' Devuelve un objeto Mensaje con las propiedades de un Mensaje Respondido a Todos para el usuario actual
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    ''' </----------------------------------------------------------------------------->
    Function getResponseAll() As IInternalMessage
    ''' <----------------------------------------------------------------------------->
    ''' <summary>
    ''' Metodo que envia el mensaje actual
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    ''' </----------------------------------------------------------------------------->
    Sub Send()
    ''' <----------------------------------------------------------------------------->
    ''' <summary>
    ''' Metodo que elimina el objeto Message actual
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    ''' </----------------------------------------------------------------------------->
    Sub Delete()
    ''' <----------------------------------------------------------------------------->
    ''' <summary>
    ''' Metodo que cambia el estado del mensaje actual a leido
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    ''' </----------------------------------------------------------------------------->
    Sub SetAsRead()
    Sub DeleteMessageRecived()
    Sub addDest(ByVal id As Integer, ByVal User_Name As String, ByVal desttype As MessageType)
    Sub addDest(ByVal dest As IDestinatario)
    ''' <----------------------------------------------------------------------------->
    ''' <summary>
    ''' Metodo que agrega un Arraylist de destinatarios
    ''' </summary>
    ''' <param name="arr">Arraylist con destinatarios que se desea agregar a la propiedad destinatarios del objeto Message actual</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    ''' </----------------------------------------------------------------------------->
    Sub AddDestRange(ByVal arr As ArrayList)
    ''' <----------------------------------------------------------------------------->
    ''' <summary>
    ''' Funcion que devuelve un objeto DisplayMessage en base el objeto Message actual
    ''' </summary>
    ''' <returns>Objeto DisplayMessage</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    ''' </----------------------------------------------------------------------------->
    Function getDispMessage() As IDisplayMessages
End Interface

