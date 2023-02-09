Imports Zamba.FileTools
Imports System.IO
Imports Zamba.Membership

Public Class PlayDoReplaceTextInWord

    Private ReadOnly _myRule As IDoReplaceTextInWord
    Private _wordPath As String
    Private _replacetext As String
    Private _replaceto As String

    Sub New(ByVal rule As IDoReplaceTextInWord)
        _myRule = rule
    End Sub

    ''' <summary>
    ''' Play de la Regla DoReplaceText
    ''' </summary>
    ''' <param name="results"></param>
    ''' <returns></returns>
    ''' <history>
    ''' </history>
    Public Function Play(ByVal results As List(Of ITaskResult)) As List(Of ITaskResult)
        Dim varInterReglas As New VariablesInterReglas()
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Play de la DoReplaceTextinWord")


            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obtener las variables")
            If results.Count > 0 Then
                For Each r As ITaskResult In results
                    _wordPath = String.Empty
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & r.Name)
                    _wordPath = TextoInteligente.ReconocerCodigo(_myRule.WordPath, r)
                    _wordPath = varInterReglas.ReconocerVariables(_wordPath).TrimEnd

                    ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Ruta del documento word: {0}", _wordPath))

                    If File.Exists(_wordPath) Then
                        Dim finfo As New FileInfo(_wordPath)
                        Dim TempDirectory As String = Membership.MembershipHelper.AppTempPath & "\temp\"
                        If IO.Directory.Exists(TempDirectory) = False Then
                            IO.Directory.CreateDirectory(TempDirectory)
                        End If
                        Dim newfile As String = Membership.MembershipHelper.AppTempPath & "\temp\" & "document" & DateTime.Now.ToString("dd-MM-yy HH-mm-ss") & "-" & DateTime.Now.Millisecond & finfo.Extension
                        File.Copy(_wordPath, newfile)

                        _replacetext = String.Empty
                        _replaceto = String.Empty
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad de campos a reemplazar: " & _myRule.ReplaceFields.Split("§").Length)
                        Dim spireoffice As New SpireTools
                        For Each replacefield As String In _myRule.ReplaceFields.Split(New String() {"§"}, StringSplitOptions.RemoveEmptyEntries)
                            _replacetext = replacefield.Split("¶")(0)
                            _replaceto = replacefield.Split("¶")(1)
                            _replacetext = TextoInteligente.ReconocerCodigo(_replacetext, r)
                            If _replacetext = "  " Then
                                _replacetext = Convert.ToChar(32)
                            Else
                                _replacetext = varInterReglas.ReconocerVariables(_replacetext).TrimEnd
                            End If

                            _replaceto = TextoInteligente.ReconocerCodigo(_replaceto, r)
                            If varInterReglas.ReconocerVariables(_replaceto) = " " Then
                                _replaceto = Convert.ToChar(32)
                            Else
                                _replaceto = varInterReglas.ReconocerVariables(_replaceto).TrimEnd
                            End If
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Reemplazando " & Chr(34) & _replacetext & Chr(34) & " por " & Chr(34) & _replaceto & Chr(34))

                            spireoffice.ReplaceInWord(newfile, _replacetext, _replaceto, _myRule.CaseSensitive, True)
                        Next
                        spireoffice = Nothing
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Se reemplazo el texto correctamente.")

                        If Membership.MembershipHelper.ClientType = ClientType.Web AndAlso Not Me._myRule.SaveOriginalPath Then
                            Dim fnewinfo As New FileInfo(newfile)
                            Dim dirpath As New DirectoryInfo(Zamba.Membership.MembershipHelper.AppTempPath)
                            Dim ZOPTB As New ZOptBusiness
                            Dim weblink As String = MembershipHelper.AppUrl

                            newfile = weblink & "/" & dirpath.Name & "/" & fnewinfo.Directory.Name & "/" & fnewinfo.Name
                            ZOPTB = Nothing
                        End If

                        ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Ruta del nuevo documento: {0}", newfile))
                        If VariablesInterReglas.ContainsKey(_myRule.NewPath) = False Then
                            VariablesInterReglas.Add(_myRule.NewPath, newfile)
                        Else
                            VariablesInterReglas.Item(_myRule.NewPath) = newfile
                        End If
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("No se encuentra el archivo: {0}", _wordPath))
                        Throw New FileNotFoundException(String.Format("No se encuentra el archivo: {0}", _wordPath))
                    End If

                Next
            End If
        Finally
            varInterReglas = Nothing
            _wordPath = Nothing
            _replacetext = Nothing
            _replaceto = Nothing
        End Try

        Return results
    End Function

End Class
