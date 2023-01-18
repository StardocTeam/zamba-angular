Imports Zamba.Core
Imports Zamba.Data
Imports System.IO
Imports Zamba.Tools
Imports Zamba.Core.WF.WF

Public Class PlayDoSaveFile

    Private _myrule As IDOSaveFile

    Public Sub New(ByVal rule As IDOSaveFile)
        Me._myrule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        For Each r As ITaskResult In results

            Dim TextoToSave As String = Zamba.Core.TextoInteligente.ReconocerCodigo(_myrule.TextToSave, r)
            TextoToSave = WFRuleParent.ReconocerVariables(TextoToSave)

            Dim filePathnew As String = String.Empty

            If Not IO.Directory.Exists(Me._myrule.FilePath.Trim()) Then
                IO.Directory.CreateDirectory(Me._myrule.FilePath.Trim())
            End If

            filePathnew = Me._myrule.FilePath.Trim() + "\" + Me._myrule.FileName + " " + DateTime.Now.ToString("dd-MM-yy HH-mm-ss") + Me._myrule.FileExtension

            If IO.File.Exists(filePathnew) Then
                IO.File.Delete(filePathnew)
            End If

            Using sw As StreamWriter = New StreamWriter(filePathnew)
                sw.Write(TextoToSave)
            End Using

            'Seteamos una variable que guarda el path del documento local
            If VariablesInterReglas.ContainsKey(Me._myrule.VarFilePath) = Nothing Then
                VariablesInterReglas.Add(Me._myrule.VarFilePath, filePathnew, False)
            Else
                VariablesInterReglas.Item(Me._myrule.VarFilePath) = filePathnew
            End If

        Next

        Return results

    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class
