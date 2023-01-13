Imports Zamba.FileTools
Imports System.IO
Imports System.Drawing
Imports Zamba.Membership

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
    Private _fontConfig As Boolean
    Private _font As String
    Private _fontSize As Single
    Private _style As Int32
    Private _color As String
    Private _backcolor As String


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
        Dim varInterReglas As New VariablesInterReglas()
        Dim color As Color
        Dim backColor As Color
        Dim font As Font
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Play de la DoCompleteTableInWord")


            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obtener las variables")
            If results.Count > 0 Then
                For Each r As ITaskResult In results
                    _wordPath = String.Empty
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & r.Name)
                    _wordPath = TextoInteligente.ReconocerCodigo(_myRule.FullPath, r)
                    _wordPath = varInterReglas.ReconocerVariables(_wordPath).TrimEnd

                    ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Ruta del documento word: {0}", _wordPath))

                    If File.Exists(_wordPath) Then
                        Dim finfo As New FileInfo(_wordPath)
                        Dim newfile As String = Membership.MembershipHelper.AppTempPath & "\temp\" & "document" & DateTime.Now.ToString("dd-MM-yy HH-mm-ss") & "-" & DateTime.Now.Millisecond & finfo.Extension
                        File.Copy(_wordPath, newfile)


                        _pageIndex = TextoInteligente.ReconocerCodigo(_myRule.PageIndex, r)
                        _pageIndex = varInterReglas.ReconocerVariables(_pageIndex).TrimEnd
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Numero de pagina donde se encuentra la tabla: " & _pageIndex.ToString)


                        _tableIndex = TextoInteligente.ReconocerCodigo(_myRule.TableIndex, r)
                        _tableIndex = varInterReglas.ReconocerVariables(_tableIndex).TrimEnd
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Numero de tabla: " & _tableIndex.ToString)

                        _rowIndex = TextoInteligente.ReconocerCodigo(_myRule.RowNumber, r)
                        _rowIndex = varInterReglas.ReconocerVariables(_rowIndex).TrimEnd
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Numero de fila: " & _rowIndex.ToString)

                        _dtvar = TextoInteligente.ReconocerCodigo(_myRule.DataTable, r)
                        Me._dtvar = WFRuleParent.ObtenerValorVariableObjeto(_dtvar)
                        If TypeOf (_dtvar) Is DataTable Then
                            _dt = _dtvar
                        ElseIf TypeOf (_dtvar) Is DataSet Then
                            _dt = DirectCast(_dtvar, DataSet).Tables(0)
                        End If
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Tabla obtenida.")

                        _fontConfig = _myRule.FontConfig

                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Fuente Personalizada : " & _fontConfig.ToString)

                        If _fontConfig Then
                            _font = TextoInteligente.ReconocerCodigo(_myRule.Font, r)
                            _font = varInterReglas.ReconocerVariables(_font).TrimEnd
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Fuente : " & _font.ToString)

                            _fontSize = TextoInteligente.ReconocerCodigo(_myRule.FontSize, r)
                            _fontSize = varInterReglas.ReconocerVariables(_fontSize).TrimEnd
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Tamaño de la fuente : " & _fontSize.ToString)

                            _style = TextoInteligente.ReconocerCodigo(_myRule.Style, r)
                            _style = varInterReglas.ReconocerVariables(_style).TrimEnd
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Estilo : " & _style.ToString)

                            _color = TextoInteligente.ReconocerCodigo(_myRule.Color, r)
                            _color = varInterReglas.ReconocerVariables(_color).TrimEnd
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Color de la fuente : " & _color.ToString)

                            _backcolor = TextoInteligente.ReconocerCodigo(_myRule.BackColor, r)
                            _backcolor = varInterReglas.ReconocerVariables(_backcolor).TrimEnd
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Color de fondo : " & _backcolor.ToString)


                            font = New Font(_font, _fontSize, getFontStyle(_style))
                            color = New Color()
                            color = Color.FromArgb(_color)
                            backColor = New Color()
                            backColor = backColor.FromArgb(_backcolor)

                        End If

                        Dim spireoffice As New Zamba.FileTools.SpireTools

                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Completando tabla.")



                        spireoffice.CompleteTableInWord(newfile, _pageIndex, _tableIndex, _withHeader, _dt, Me._myRule.InTable, _rowIndex, _fontConfig, font, color, backColor)
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
                        If VariablesInterReglas.ContainsKey(_myRule.VarName) = False Then
                            VariablesInterReglas.Add(_myRule.VarName, newfile)
                        Else
                            VariablesInterReglas.Item(_myRule.VarName) = newfile
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
        End Try

        Return results
    End Function

    Private Function getFontStyle(fStyle As Integer) As System.Drawing.FontStyle
        Select Case fStyle
            Case 1
                Return FontStyle.Bold
            Case 2
                Return FontStyle.Italic
            Case 0
                Return FontStyle.Regular
            Case 8
                Return FontStyle.Strikeout
            Case 4
                Return FontStyle.Underline
        End Select
    End Function

End Class
