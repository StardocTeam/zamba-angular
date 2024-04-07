Public Class PostProcessBusiness
    Public Shared Function GetPreference(ByVal _preference As PostProcessPreferences) As String
        Dim file As String
        If IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Zamba Software\PostProcessConfig.ini") Then
            file = (New System.IO.FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Zamba Software\PostProcessConfig.ini")).FullName
        Else
            file = (New System.IO.FileInfo(System.Windows.Forms.Application.StartupPath & "\PostProcessConfig.ini")).FullName
        End If

        Dim filetext As New System.IO.StreamReader(file)

        Dim text As String = filetext.ReadToEnd
        Dim value As String = String.Empty

        For Each Item As String In text.Split(Chr(13))
            If Trim(Item.ToUpper).Contains(_preference.ToString.ToUpper) Then
                value = Item.Split("=")(1)
                Exit For
            End If
        Next
        Return value

    End Function
End Class