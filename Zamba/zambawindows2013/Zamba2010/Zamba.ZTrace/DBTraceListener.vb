Public Class DBTraceListener
    Inherits TraceListener


    Public Property _DBWriter As IDBWriter

    Public Sub New(DBWriter As IDBWriter)
        MyBase.New()
        _DBWriter = DBWriter
    End Sub

    Public Overrides Sub Write(traceDto As Object, Category As String)
    End Sub

    Public Overrides Sub WriteLine(traceDto As Object, Category As String)
        If _DBWriter IsNot Nothing Then _DBWriter.Write(traceDto)
    End Sub


    Public Overrides Sub flush()
    End Sub

    Public Overrides Sub Write(message As String)
    End Sub

    Public Overrides Sub WriteLine(message As String)
    End Sub
End Class
