Imports Zamba.Tools
Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim D As New ShortcutHandler
            D.ValidateShortcutInFolder(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\", "C:\P\F\Cliente.exe", "ZC", Nothing, Nothing)

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
End Class
