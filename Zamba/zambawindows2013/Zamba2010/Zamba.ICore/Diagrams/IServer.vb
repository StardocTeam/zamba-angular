Public Interface IServer
    Inherits ICore

    Property IP() As String
    Property Databases() As List(Of IDataBase)

End Interface
