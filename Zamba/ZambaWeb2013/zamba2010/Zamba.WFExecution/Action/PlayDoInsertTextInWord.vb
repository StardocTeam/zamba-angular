Imports Zamba.FileTools
Imports System.IO
Imports System.Drawing
Imports Zamba.Membership

Public Class PlayDoInsertTextInWord

    Private _myRule As IDoInsertTextInWord
    Private _wordPath As String
    Private _replacetext As String
    Private _replaceto As String
    Private _fontConfig As Boolean
    Private _font As String
    Private _fontSize As Single
    Private _style As Int32
    Private _color As String
    Private _backcolor As String
    Private _textAsTable As Boolean

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
        Dim varInterReglas As New VariablesInterReglas()
        Dim color As Color
        Dim backColor As Color
        Dim font As Font
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Play de la DoInsertTextinWord")


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
                        Dim newfile As String = Membership.MembershipHelper.AppTempPath & "\temp\" & "document" & DateTime.Now.ToString("dd-MM-yy HH-mm-ss") & "-" & DateTime.Now.Millisecond & finfo.Extension


                        File.Copy(_wordPath, newfile)

                        Dim spireoffice As New Zamba.FileTools.SpireTools

                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Obtener texto a insertar")
                        Dim wordTextVar As Object
                        If (_myRule.Variable.ToLower().Contains("zvar")) Then
                            wordTextVar = varInterReglas.ReconocerVariablesAsObject(_myRule.Variable)
                        Else
                            wordTextVar = TextoInteligente.ReconocerCodigo(_myRule.Variable, r)
                        End If

                        _textAsTable = _myRule.textAsTable

                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Insertar texto como tabla : " & _textAsTable.ToString)

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

                            _backcolor = TextoInteligente.ReconocerCodigo(_myRule.backColor, r)
                            _backcolor = varInterReglas.ReconocerVariables(_backcolor).TrimEnd
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Color de fondo : " & _backcolor.ToString)

                            font = New Font(_font, _fontSize, getFontStyle(_style))
                            color = New Color()
                            color = Color.FromArgb(_color)
                            backColor = New Color()
                            backColor = backColor.FromArgb(_backcolor)

                        End If

                        If TypeOf (wordTextVar) Is DataSet Then
                            spireoffice.InsertTableInWord(newfile, wordTextVar.tables(0), _myRule.Section, _fontConfig,
                                                         font, color, backColor)
                        ElseIf TypeOf (wordTextVar) Is DataTable Then
                            spireoffice.InsertTableInWord(newfile, wordTextVar, _myRule.Section, _fontConfig,
                                                         font, color, backColor)
                        Else
                            spireoffice.InsertTextInWord(newfile, wordTextVar.ToString(), _myRule.Section, _textAsTable,
                                                         _fontConfig, font, color, backColor)
                        End If

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