Public Interface IWFService
    Inherits IZClass

    Property DateStarted() As String
    Property DateReStarted() As String
    Property DateStoped() As String
    Property DateLastRefresh() As Date
    'Property WFlows() As ArrayList
    Property WFIds() As List(Of Int64)
    Property RefreshRate() As Double
    Property ServiceType() As ClientType

End Interface