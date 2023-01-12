Public Interface IDOOpenTask
    Inherits IRule
    Property TaskID() As String
    Property DocID() As String
    Property UseCurrentTask() As Boolean
    Property OpenMode As Int32

End Interface
