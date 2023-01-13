Public Interface IProcess
    Property ID() As Int32
    Property Name() As String
    Property Path() As String
    Property Caracter() As String
    Property DocType() As IDocType
    Property Index() As ArrayList
    Property IndexOrder() As ArrayList
    Property Move() As Boolean
    Property Verify() As Boolean
    Property FlagAceptBlankData() As Boolean
    Property FlagBackUp() As Boolean
    Property Replace() As Boolean
    Property CheckBatch() As Boolean
    Property FlagDelSourceFile() As Boolean
    Property FlagSourceVariable() As Boolean
    Property FlagMultipleFiles() As Boolean
    Property MultipleCaracter() As String
    Property CreateFolder() As Boolean
    Property IP_GROUP() As Int32
    Property preProcessNames() As ArrayList
    Property BackUpPath() As String
    Property UserId() As Int32
    Property type() As ProcessTypes
    Property AskConfirmations() As Boolean
    Property DsProcessIndex() As DsIpIndex
    Property History() As IProcessHistory
End Interface

