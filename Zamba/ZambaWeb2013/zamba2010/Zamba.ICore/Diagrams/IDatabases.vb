Public Interface IDataBase
    Inherits ICore

    Property Server() As IServer
    Property User() As String
    Property Password() As String
    Property WinAuthorization() As Boolean

End Interface
