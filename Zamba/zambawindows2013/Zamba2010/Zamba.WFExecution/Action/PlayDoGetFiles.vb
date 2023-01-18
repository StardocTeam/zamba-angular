Imports System.IO

Public Class PlayDoGetFiles

    Private _myrule As IDoGetFiles
    Private indexvalue As String


    Public Sub New(ByVal rule As IDoGetFiles)
        _myrule = rule

    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim newResults As New System.Collections.Generic.List(Of Core.ITaskResult)
        Dim files As Array
        Dim ExtensionFile As Array
        Dim t As Int16 = 1
        Dim filePathnew As String = String.Empty
        Dim FilteredFiles() As String
        Try
            'reconociendo texto inteligente
            Dim filepath As String = TextoInteligente.ReconocerCodigo(_myrule.DirectoryRoute, results(0)).Trim
            'reconociendo Zvar
            If _myrule.DirectoryRoute.ToLower.Contains("zvar") = True Then
                filepath = WFRuleParent.ReconocerVariables(filepath).Trim
            End If

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Directorio: " & filepath)

            If _myrule.ObtainOnlyRouteFiles Then
                files = GetFilesRecursive(filepath).ToArray()
            Else
                files = Directory.GetFiles(filepath)
            End If


            If Not _myrule.Extensions.Contains("*.*") Then
                ExtensionFile = _myrule.Extensions.Split(",")
                For Each fi As String In files
                    For Each extFi As String In ExtensionFile
                        If fi.Substring(fi.Length - 4).ToLower = extFi.Remove(extFi.Length - 1).Substring(extFi.Length - 5).ToLower Then
                            Array.Resize(FilteredFiles, t)
                            FilteredFiles.SetValue(fi, t - 1)
                            t = t + 1
                        End If
                    Next
                Next

                files = Nothing
                files = FilteredFiles
            End If


            'Seteamos una variable que guarda el path del documento local
            If VariablesInterReglas.ContainsKey(_myrule.VarName) = Nothing Then
                VariablesInterReglas.Add(_myrule.VarName, files, False)
            Else
                VariablesInterReglas.Item(_myrule.VarName) = files
            End If
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre Variable: " & _myrule.VarName)


        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

        Return results
    End Function

    Public Shared Function GetFilesRecursive(ByVal initial As String) As List(Of String)
        ' This list stores the results.
        Dim result As New List(Of String)

        ' This stack stores the directories to process.
        Dim stack As New Stack(Of String)

        ' Add the initial directory
        stack.Push(initial)

        ' Continue processing for each stacked directory
        Do While (stack.Count > 0)
            ' Get top directory string
            Dim dir As String = stack.Pop
            Try
                ' Add all immediate file paths
                result.AddRange(Directory.GetFiles(dir, "*.*"))

                ' Loop through all subdirectories and add them to the stack.
                Dim directoryName As String
                For Each directoryName In Directory.GetDirectories(dir)
                    stack.Push(directoryName)
                Next

            Catch ex As Exception
            End Try
        Loop

        ' Return the list
        Return result
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function


End Class
