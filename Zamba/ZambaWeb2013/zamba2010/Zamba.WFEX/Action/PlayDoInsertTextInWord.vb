Imports Zamba.FileTools
Imports System.IO
Imports Zamba.AdminControls
Imports Zamba.Core

Public Class PlayDoInsertTextInWord

    Private _myRule As IDoInsertTextInWord
    Private _wordPath As String
    Private _replacetext As String
    Private _replaceto As String

    Sub New(ByVal rule As IDoInsertTextInWord)
        Me._myRule = rule
    End Sub

    ''' <summary>
    ''' Play de la Regla InsertTextInWord
    ''' </summary>
    ''' <param name="results"></param>
    ''' <returns></returns>
    ''' <history>
    ''' </history>
    Public Function Play(ByVal results As List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            Trace.WriteLineIf(ZTrace.IsInfo, "Play de la DoInsertTextinWord")


            Trace.WriteLineIf(ZTrace.IsInfo, "Obtener las variables")
            If results.Count > 0 Then
                For Each r As ITaskResult In results
                    _wordPath = String.Empty
                    _wordPath = TextoInteligente.ReconocerCodigo(_myRule.WordPath, r)
                    _wordPath = WFRuleParent.ReconocerVariables(_wordPath).TrimEnd

                    Trace.WriteLineIf(ZTrace.IsInfo, String.Format("Ruta del documento : {0}", _wordPath))

                    If File.Exists(_wordPath) Then
                        Dim finfo As New FileInfo(_wordPath)
                        Dim newfile As String = Membership.MembershipHelper.AppTempPath & "\" & "document" & DateTime.Now.ToString("dd-MM-yy HH-mm-ss") & finfo.Extension
                        File.Copy(_wordPath, newfile)



                        Trace.WriteLineIf(ZTrace.IsInfo, "Obtener texto a insertar")
                        Dim wordTextVar As Object
                        If (_myRule.Variable.ToLower().Contains("zvar")) Then
                            wordTextVar = WFRuleParent.ReconocerVariablesAsObject(_myRule.Variable)
                        Else
                            wordTextVar = TextoInteligente.ReconocerCodigo(_myRule.Variable, r)
                        End If

                        If Path.GetExtension(newfile).ToLower = ".rtf" Then
                            Trace.WriteLineIf(ZTrace.IsInfo, "El documento es .rtf")
                            Dim rtf As New FrmRtf()
                            rtf.InsertHeaderIntoRtf(newfile, wordTextVar.ToString(), False, Nothing)
                            rtf.Dispose()
                            rtf = Nothing
                        Else
                            Trace.WriteLineIf(ZTrace.IsInfo, "El documento es word")
                            Dim spireoffice As New SpireTools
                            If TypeOf (wordTextVar) Is DataSet Then
                                spireoffice.InsertTableInWord(newfile, wordTextVar.tables(0), _myRule.Section)
                            ElseIf TypeOf (wordTextVar) Is DataTable Then
                                spireoffice.InsertTableInWord(newfile, wordTextVar, _myRule.Section)
                            Else
                                spireoffice.InsertTextInWord(newfile, wordTextVar.ToString(), _myRule.Section)
                            End If
                            spireoffice = Nothing
                        End If

                        Trace.WriteLineIf(ZTrace.IsInfo, "Se reemplazo el texto correctamente.")

                        If Membership.MembershipHelper.ClientType = ClientType.Web Then
                            Dim fnewinfo As New FileInfo(newfile)
                            newfile = ZOptBusiness.GetValue("WebViewPath").ToString() & "/" & fnewinfo.Directory.Name & "/" & fnewinfo.Name
                        End If

                        Trace.WriteLineIf(ZTrace.IsInfo, String.Format("Ruta del nuevo documento: {0}", newfile))
                        If VariablesInterReglas.ContainsKey(_myRule.NewPath) = False Then
                            VariablesInterReglas.Add(_myRule.NewPath, newfile, False)
                        Else
                            VariablesInterReglas.Item(_myRule.NewPath) = newfile
                        End If
                    Else
                        Trace.WriteLineIf(ZTrace.IsInfo, String.Format("No se encuentra el archivo: {0}", _wordPath))
                        Throw New FileNotFoundException(String.Format("No se encuentra el archivo: {0}", _wordPath))
                    End If

                Next
            End If
        Finally
            _wordPath = Nothing
            _replacetext = Nothing
            _replaceto = Nothing
        End Try

        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

End Class
