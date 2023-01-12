Public Class Log

    Public Shared filename As String = "\logger.log"
    Public Shared Header As String
    Private Shared Finfo As IO.FileInfo
    Private Shared isOpen As Boolean
    Private Shared Writer As IO.StreamWriter

    Public Shared Sub WriteLine(ByVal str As String)
        If isOpen = False Then
            Open()
        End If
        Writer.WriteLine(str)
    End Sub

    Public Shared Sub WriteLines(ByVal str As String)
        If isOpen = False Then
            Open()
        End If
        Dim strs() As String = str.Split("|")
        Dim i As Integer

        For i = 0 To strs.Length - 1
            Writer.WriteLine(strs(i))
        Next
    End Sub

    Private Shared Sub Open()
        If isOpen = False Then
            'Dim di As New IO.DirectoryInfo(Application.StartupPath + "\LOGS")
            'If di.Exists = False Then
            '    di.Create()
            'End If
            Finfo = New IO.FileInfo(filename)
            Writer = New IO.StreamWriter(Finfo.Create)
            Writer.AutoFlush = True
            WriteHeader()
            isOpen = True
        End If
    End Sub
    Public Shared Sub Open(ByVal fname As String, ByVal Head As String)
        If isOpen = False Then
            filename = fname
            Header = Head
            Open()
        End If
    End Sub
    Private Shared Sub WriteHeader()
        Dim str() As String = Header.Split("|")
        Dim i As Int32
        For i = 0 To str.Length - 1
            Writer.WriteLine(str(i))
        Next
    End Sub
    Public Shared Sub Close()
        WriteLines("||Programa Finalizado a las: " & Date.Now.ToString("HH:mm:ss"))
        Writer.Close()
    End Sub

End Class
