Imports Zamba.FileTools
Imports System.IO
Imports Zamba.Core

Public Class PlayDoCompleteTableInWord

    Private _myRule As IDoCompleteTableInWord
    Private _wordPath As String
    Private _tableIndex As Int64
    Private _pageIndex As Int64
    Private _withHeader As Boolean
    Private _varNAme As String
    Private _dt As DataTable
    Private _dtvar As Object
    Private _rowIndex As Int64

    Sub New(ByVal rule As IDoCompleteTableInWord)
        Me._myRule = rule
    End Sub

    ''' <summary>
    ''' Play de la Regla DoReplaceText
    ''' </summary>
    ''' <param name="results"></param>
    ''' <returns></returns>
    ''' <history>
    ''' </history>
    Public Function Play(ByVal results As List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            Trace.WriteLineIf(ZTrace.IsVerbose, "Play de la DoReplaceTextinWord")


            Trace.WriteLineIf(ZTrace.IsVerbose, "Obtener las variables")
            If results.Count > 0 Then
                For Each r As ITaskResult In results
                    _wordPath = String.Empty
                    Trace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & r.Name)
                    _wordPath = TextoInteligente.ReconocerCodigo(_myRule.FullPath, r)
                    _wordPath = WFRuleParent.ReconocerVariables(_wordPath).TrimEnd

                    Trace.WriteLineIf(ZTrace.IsInfo, String.Format("Ruta del documento word: {0}", _wordPath))

                    If File.Exists(_wordPath) Then
                        Dim finfo As New FileInfo(_wordPath)
                        Dim newfile As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Zamba Software\temp\document" & DateTime.Now.ToString("dd-MM-yy HH-mm-ss") & "-" & DateTime.Now.Millisecond & finfo.Extension
                        newfile = _wordPath
                        'File.Copy(_wordPath, newfile)

                        _pageIndex = TextoInteligente.ReconocerCodigo(_myRule.PageIndex, r)
                        _pageIndex = WFRuleParent.ReconocerVariables(_pageIndex).TrimEnd
                        Trace.WriteLineIf(ZTrace.IsVerbose, "Numero de pagina donde se encuentra la tabla: " & _pageIndex.ToString)


                        _tableIndex = TextoInteligente.ReconocerCodigo(_myRule.TableIndex, r)
                        _tableIndex = WFRuleParent.ReconocerVariables(_tableIndex).TrimEnd
                        Trace.WriteLineIf(ZTrace.IsVerbose, "Numero de tabla: " & _tableIndex.ToString)

                        _rowIndex = TextoInteligente.ReconocerCodigo(_myRule.RowNumber, r)
                        _rowIndex = WFRuleParent.ReconocerVariables(_rowIndex).TrimEnd
                        Trace.WriteLineIf(ZTrace.IsVerbose, "Numero de fila: " & _rowIndex.ToString)

                        _dtvar = TextoInteligente.ReconocerCodigo(_myRule.DataTable, r)
                        Me._dtvar = WFRuleParent.ObtenerValorVariableObjeto(_dtvar)
                        If TypeOf (_dtvar) Is DataTable Then
                            _dt = _dtvar
                        ElseIf TypeOf (_dtvar) Is DataSet Then
                            _dt = DirectCast(_dtvar, DataSet).Tables(0)
                        End If
                        Trace.WriteLineIf(ZTrace.IsVerbose, "Tabla obtenida.")

                        Dim spireoffice As New SpireTools

                        Trace.WriteLineIf(ZTrace.IsVerbose, "Completando tabla.")

                        spireoffice.CompleteTableInWord(newfile, _pageIndex, _tableIndex, _withHeader, _dt, Me._myRule.InTable, _rowIndex)
                        spireoffice = Nothing
                        Trace.WriteLineIf(ZTrace.IsInfo, "Se reemplazo el texto correctamente.")
                        Trace.WriteLineIf(ZTrace.IsVerbose, String.Format("Ruta del nuevo documento: {0}", newfile))

                        If VariablesInterReglas.ContainsKey(_myRule.VarName) = False Then
                            VariablesInterReglas.Add(_myRule.VarName, newfile, False)
                        Else
                            VariablesInterReglas.Item(_myRule.VarName) = newfile
                        End If
                    Else
                        Trace.WriteLineIf(ZTrace.IsInfo, String.Format("No se encuentra el archivo: {0}", _wordPath))
                        Throw New FileNotFoundException(String.Format("No se encuentra el archivo: {0}", _wordPath))
                    End If

                Next
            End If
        Finally
            _wordPath = Nothing
        End Try

        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

End Class