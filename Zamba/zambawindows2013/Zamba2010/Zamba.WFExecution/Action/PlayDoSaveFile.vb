Imports System.IO

Public Class PlayDoSaveFile

    Private _myrule As IDOSaveFile

    Public Sub New(ByVal rule As IDOSaveFile)
        _myrule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        For Each r As ITaskResult In results

            Dim TextoToSave As String = TextoInteligente.ReconocerCodigo(_myrule.TextToSave, r)
            TextoToSave = WFRuleParent.ReconocerVariables(TextoToSave)

            Dim filePathnew As String = String.Empty

            If Not Directory.Exists(_myrule.FilePath.Trim()) Then
                Directory.CreateDirectory(_myrule.FilePath.Trim())
            End If

            filePathnew = _myrule.FilePath.Trim() + "\" + _myrule.FileName + " " + DateTime.Now.ToString("dd-MM-yy HH-mm-ss") + _myrule.FileExtension

            If File.Exists(filePathnew) Then
                File.Delete(filePathnew)
            End If

            Using sw As StreamWriter = New StreamWriter(filePathnew)
                sw.Write(TextoToSave)
            End Using

            'Seteamos una variable que guarda el path del documento local
            If VariablesInterReglas.ContainsKey(_myrule.VarFilePath) = Nothing Then
                VariablesInterReglas.Add(_myrule.VarFilePath, filePathnew, False)
            Else
                VariablesInterReglas.Item(_myrule.VarFilePath) = filePathnew
            End If

        Next

        Return results

    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class
