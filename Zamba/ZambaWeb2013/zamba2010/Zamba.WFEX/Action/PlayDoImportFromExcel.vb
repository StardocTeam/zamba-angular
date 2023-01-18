Imports System.Data.OleDb
Imports System.IO
Imports Zamba.Core

Public Class PlayDoImportFromExcel


#Region "Atributos"

    Private _myrule As IDoImportFromExcel
    Private _excelFile As String
    Private _sheetName As String
    Private _varName As String
    Private _saveAsPath As String
    Private _saveAsFileName As String


    Private _ds As DataTable

#End Region

#Region "Constructores"

    Sub New(ByVal rule As IDoImportFromExcel)
        Me._myrule = rule
    End Sub

#End Region

#Region "Metodos Publicos"

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            Trace.WriteLineIf(ZTrace.IsInfo, "Results count " & results.Count)
            If results.Count > 0 Then

                For Each r As ITaskResult In results

                    Me._excelFile = Me._myrule.File
                    Trace.WriteLineIf(ZTrace.IsInfo, "Nombre original del archivo: " & Me._excelFile)
                    Me._excelFile = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._excelFile)
                    Me._excelFile = TextoInteligente.ReconocerCodigo(Me._excelFile, r)
                    Trace.WriteLineIf(ZTrace.IsInfo, "Nombre del archivo: " & Me._excelFile)

                    Me._sheetName = Me._myrule.SheetName
                    Trace.WriteLineIf(ZTrace.IsInfo, "Nombre original de la hoja: " & Me._sheetName)
                    Me._sheetName = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._sheetName)
                    Me._sheetName = TextoInteligente.ReconocerCodigo(Me._sheetName, r)
                    Trace.WriteLineIf(ZTrace.IsInfo, "Nombre de la hoja: " & Me._sheetName)

                    Trace.WriteLineIf(ZTrace.IsInfo, "Version de Office configurada en la regla: " & Me._myrule.ExcelVersion.ToString)

                    Trace.WriteLineIf(ZTrace.IsInfo, "Obteniendo datos del excel.")
                    Me.GetExcelData()
                    Trace.WriteLineIf(ZTrace.IsInfo, "Filas obtenidas: " & Me._ds.Rows.Count)

                    Me._varName = Me._myrule.VarName
                    Trace.WriteLineIf(ZTrace.IsInfo, "Nombre original de la variable a guardar: " & Me._varName)
                    Me._varName = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._varName)
                    Me._varName = TextoInteligente.ReconocerCodigo(Me._varName, r)
                    Trace.WriteLineIf(ZTrace.IsInfo, "Nombre de la variable a guardar: " & Me._varName)


                    Trace.WriteLineIf(ZTrace.IsInfo, "Guardar copia: " & Me._myrule.SaveAs.ToString())

                    If Me._myrule.SaveAs AndAlso Me._myrule.SaveAsPath.Trim() <> String.Empty AndAlso Me._myrule.SaveAsFileName.Trim() <> String.Empty Then
                        Me._saveAsPath = Me._myrule.SaveAsPath
                        Trace.WriteLineIf(ZTrace.IsInfo, "Nombre del directorio : " & Me._saveAsPath)
                        Me._saveAsPath = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._saveAsPath)
                        Me._saveAsPath = TextoInteligente.ReconocerCodigo(Me._saveAsPath, r)
                        Trace.WriteLineIf(ZTrace.IsInfo, "Nombre del directorio (Resuelto): " & Me._saveAsPath)

                        Me._saveAsFileName = Me._myrule.SaveAsFileName
                        Trace.WriteLineIf(ZTrace.IsInfo, "Nombre del archivo : " & Me._saveAsFileName)
                        Me._saveAsFileName = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._saveAsFileName)
                        Me._saveAsFileName = TextoInteligente.ReconocerCodigo(Me._saveAsFileName, r)
                        Trace.WriteLineIf(ZTrace.IsInfo, "Nombre del archivo (Resuelto) : " & Me._saveAsFileName)

                        Me._saveAsPath = Me._saveAsPath.Trim()
                        Me._saveAsFileName = Me._saveAsFileName.Trim()


                        Trace.WriteLineIf(ZTrace.IsInfo, "Verificando que el path generado no contenga caracteres invalidos...")
                        For Each _chrOriginal As Char In Me._saveAsPath.ToCharArray()
                            For Each _chrItem As Char In Path.GetInvalidPathChars()
                                If _chrOriginal = _chrItem Then
                                    _chrOriginal = Convert.ToChar("-")
                                    Trace.WriteLineIf(ZTrace.IsInfo, "Se ha encontrado un caracter invalido y se reemplazó por '-'")
                                    Exit For
                                End If
                            Next
                        Next

                        Trace.WriteLineIf(ZTrace.IsInfo, "Verificando que el nombre del archivo generado no contenga caracteres invalidos...")
                        For Each _chrFileName As Char In Me._saveAsFileName.ToCharArray()
                            For Each _chrInv As Char In Path.GetInvalidFileNameChars()
                                If _chrFileName = _chrInv Then
                                    _chrFileName = Convert.ToChar("-")
                                    Trace.WriteLineIf(ZTrace.IsInfo, "Se ha encontrado un caracter invalido y se reemplazó por '-'")
                                    Exit For
                                End If
                            Next
                        Next


                        'Trace.WriteLineIf(ZTrace.IsInfo, "Verificando directorio de destino... ")
                        'If Not Directory.Exists(Me._saveAsPath) Then
                        '    Trace.WriteLineIf(ZTrace.IsInfo, "El directorio especificado no existe, se procede a crearlo... ")
                        '    Directory.CreateDirectory(Me._saveAsPath)
                        '    Trace.WriteLineIf(ZTrace.IsInfo, "Directorio creado satisfactoriamente..")

                        'Else
                        '    Trace.WriteLineIf(ZTrace.IsInfo, "Se ha encontrado el directorio de destino")
                        'End If

                        Trace.WriteLineIf(ZTrace.IsInfo, "Verificando extensión del archivo... ")
                        If Not Path.GetExtension(Me._saveAsFileName).ToLower().Trim() = ".xls" Then
                            Trace.WriteLineIf(ZTrace.IsInfo, "El archivo no contiene la extensión correcta, se procede a renombrar (xls)... ")
                            Me._saveAsFileName = Path.ChangeExtension(Me._saveAsFileName.Trim(), ".xls")
                            Trace.WriteLineIf(ZTrace.IsInfo, "El archivo fue renombrado satisfactoriamente")
                        Else
                            Trace.WriteLineIf(ZTrace.IsInfo, "El archivo contiene la extensión correcta (xls)")

                        End If

                        Dim _strFullPath As String = Path.Combine(Me._saveAsPath, Me._saveAsFileName)
                        Trace.WriteLineIf(ZTrace.IsInfo, "Realizando copia del archivo... ")
                        Trace.WriteLineIf(ZTrace.IsInfo, "Path: '" & _strFullPath & "'")
                        FileCopy(_excelFile, _strFullPath)
                        Trace.WriteLineIf(ZTrace.IsInfo, "Copia realizada satisfactoriamente..")

                    Else
                        Trace.WriteLineIf(ZTrace.IsInfo, "No se puede continuar con la copia, falta uno de los datos para generar el path.")


                    End If





                    If VariablesInterReglas.ContainsKey(Me._varName.Trim) = False Then
                        VariablesInterReglas.Add(Me._varName.Trim, Me._ds, False)
                    Else
                        VariablesInterReglas.Item(Me._varName.Trim) = Me._ds
                    End If

                Next


            Else

                Me._excelFile = Me._myrule.File
                Trace.WriteLineIf(ZTrace.IsInfo, "Nombre original del archivo: " & Me._excelFile)
                Me._excelFile = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._excelFile)
                Trace.WriteLineIf(ZTrace.IsInfo, "Nombre del archivo: " & Me._excelFile)

                Me._sheetName = Me._myrule.SheetName
                Trace.WriteLineIf(ZTrace.IsInfo, "Nombre original de la hoja: " & Me._sheetName)
                Me._sheetName = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._sheetName)
                Trace.WriteLineIf(ZTrace.IsInfo, "Nombre de la hoja: " & Me._sheetName)

                Trace.WriteLineIf(ZTrace.IsInfo, "Version de Office configurada en la regla: " & Me._myrule.ExcelVersion.ToString)

                Trace.WriteLineIf(ZTrace.IsInfo, "Obteniendo datos del excel.")
                Me.GetExcelData()
                Trace.WriteLineIf(ZTrace.IsInfo, "Filas obtenidas: " & Me._ds.Rows.Count)

                Me._varName = Me._myrule.VarName
                Trace.WriteLineIf(ZTrace.IsInfo, "Nombre original de la variable a guardar: " & Me._varName)
                Me._varName = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._varName)
                Trace.WriteLineIf(ZTrace.IsInfo, "Nombre de la variable a guardar: " & Me._varName)

                Trace.WriteLineIf(ZTrace.IsInfo, "Guardar copia: " & Me._myrule.SaveAs.ToString())

                If Me._myrule.SaveAs AndAlso Me._myrule.SaveAsPath.Trim() <> String.Empty AndAlso Me._myrule.SaveAsFileName.Trim() <> String.Empty Then
                    Me._saveAsPath = Me._myrule.SaveAsPath
                    Trace.WriteLineIf(ZTrace.IsInfo, "Nombre del directorio : " & Me._saveAsPath)
                    Me._saveAsPath = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._saveAsPath)
                    Trace.WriteLineIf(ZTrace.IsInfo, "Nombre del directorio (Resuelto): " & Me._saveAsPath)

                    Me._saveAsFileName = Me._myrule.SaveAsFileName
                    Trace.WriteLineIf(ZTrace.IsInfo, "Nombre del archivo : " & Me._saveAsFileName)
                    Me._saveAsFileName = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._saveAsFileName)
                    Trace.WriteLineIf(ZTrace.IsInfo, "Nombre del archivo (Resuelto) : " & Me._saveAsFileName)

                    Me._saveAsPath = Me._saveAsPath.Trim()
                    Me._saveAsFileName = Me._saveAsFileName.Trim()


                    Trace.WriteLineIf(ZTrace.IsInfo, "Verificando que el path generado no contenga caracteres invalidos...")
                    For Each _chrOriginal As Char In Me._saveAsPath.ToCharArray()
                        For Each _chrItem As Char In Path.GetInvalidPathChars()
                            If _chrOriginal = _chrItem Then
                                _chrOriginal = Convert.ToChar("-")
                                Trace.WriteLineIf(ZTrace.IsInfo, "Se ha encontrado un caracter invalido y se reemplazó por '-'")
                                Exit For
                            End If
                        Next
                    Next

                    Trace.WriteLineIf(ZTrace.IsInfo, "Verificando que el nombre del archivo generado no contenga caracteres invalidos...")
                    For Each _chrFileName As Char In Me._saveAsFileName.ToCharArray()
                        For Each _chrInv As Char In Path.GetInvalidFileNameChars()
                            If _chrFileName = _chrInv Then
                                _chrFileName = Convert.ToChar("-")
                                Trace.WriteLineIf(ZTrace.IsInfo, "Se ha encontrado un caracter invalido y se reemplazó por '-'")
                                Exit For
                            End If
                        Next
                    Next


                    'Trace.WriteLineIf(ZTrace.IsInfo, "Verificando directorio de destino... ")
                    'If Not Directory.Exists(Me._saveAsPath) Then
                    '    Trace.WriteLineIf(ZTrace.IsInfo, "El directorio especificado no existe, se procede a crearlo... ")
                    '    Directory.CreateDirectory(Me._saveAsPath)
                    '    Trace.WriteLineIf(ZTrace.IsInfo, "Directorio creado satisfactoriamente..")

                    'Else
                    '    Trace.WriteLineIf(ZTrace.IsInfo, "Se ha encontrado el directorio de destino")
                    'End If

                    Trace.WriteLineIf(ZTrace.IsInfo, "Verificando extensión del archivo... ")
                    If Not Path.GetExtension(Me._saveAsFileName).ToLower().Trim() = ".xls" Then
                        Trace.WriteLineIf(ZTrace.IsInfo, "El archivo no contiene la extensión correcta, se procede a renombrar (xls)... ")
                        Me._saveAsFileName = Path.ChangeExtension(Me._saveAsFileName.Trim(), ".xls")
                        Trace.WriteLineIf(ZTrace.IsInfo, "El archivo fue renombrado satisfactoriamente")
                    Else
                        Trace.WriteLineIf(ZTrace.IsInfo, "El archivo contiene la extensión correcta (xls)")

                    End If

                    Dim _strFullPath As String = Path.Combine(Me._saveAsPath, Me._saveAsFileName)
                    Trace.WriteLineIf(ZTrace.IsInfo, "Realizando copia del archivo... ")
                    Trace.WriteLineIf(ZTrace.IsInfo, "Path: '" & _strFullPath & "'")
                    FileCopy(_excelFile, _strFullPath)
                    Trace.WriteLineIf(ZTrace.IsInfo, "Copia realizada satisfactoriamente..")


                Else
                    Trace.WriteLineIf(ZTrace.IsInfo, "No se puede continuar con la copia, falta uno de los datos para generar el path.")



                End If

                If VariablesInterReglas.ContainsKey(Me._varName) = False Then
                    VariablesInterReglas.Add(Me._varName, Me._ds, False)
                Else
                    VariablesInterReglas.Item(Me._varName) = Me._ds
                End If

            End If
            Return results
        Finally
            Me._excelFile = Nothing
            Me._sheetName = Nothing
            Me._varName = Nothing
            Me._ds = Nothing
        End Try
    End Function

#End Region

#Region "Metodos Privados"

    ''' <summary>
    ''' Obtiene los datos del excel.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <History>
    '''     [Ezequiel]    12/11/09 - Created
    ''' </history>
    Private Function GetExcelData() As DataTable
        Dim connectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Me._excelFile.Trim & ";Extended Properties=" & Chr(34) & "Excel 8.0;HDR=Yes;" & Chr(34) & ";"

        Dim strSQL As String = "SELECT * FROM [" & Me._sheetName.Trim & "$]"
        Me._ds = New DataTable()
        Using excelConnection As OleDbConnection = New OleDbConnection(connectionString)
            excelConnection.Open()
            Dim dbCommand As OleDbCommand = New OleDbCommand(strSQL, excelConnection)
            Dim dataAdapter As OleDbDataAdapter = New OleDbDataAdapter(dbCommand)
            dataAdapter.Fill(Me._ds)
        End Using
        Return Me._ds
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

#End Region


End Class
