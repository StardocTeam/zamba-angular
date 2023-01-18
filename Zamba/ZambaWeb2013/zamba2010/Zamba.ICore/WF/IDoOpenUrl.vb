Public Interface IDoOpenUrl
    Inherits IRule

    Property Url() As String
    Property OpenMode As OpenType

End Interface

Public Enum OpenType
    NewTab = 0
    Modal = 1
    Home = 2
    NewWindow = 3
End Enum