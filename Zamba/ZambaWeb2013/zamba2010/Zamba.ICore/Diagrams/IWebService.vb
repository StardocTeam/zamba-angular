Public Interface IWebService
    Inherits ICore

    Property Description() As String
    Property Server() As IServer
    Property Databases() As List(Of IDataBase)
    Property Path() As String
    Property WebMethods() As List(Of String)

End Interface
