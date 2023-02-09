Imports System.Data.OleDb
Imports System.IO
Imports Zamba.FileTools

Public Class PlayDoImportFromExcel


#Region "Atributos"

    Private _myrule As IDoImportFromExcel
    Private Property ignoreFirstRow As Boolean
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
        Dim VarInterReglas As New VariablesInterReglas()
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Results count " & results.Count)
            If results.Count > 0 Then

                For Each r As ITaskResult In results
                    Me.ignoreFirstRow = _myrule.ignoreFirstRow
                    Me._excelFile = Me._myrule.File
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Original del archivo: " & Me._excelFile)
                    Me._excelFile = VarInterReglas.ReconocerVariablesValuesSoloTexto(Me._excelFile)
                    Me._excelFile = TextoInteligente.ReconocerCodigo(Me._excelFile, r)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre del archivo: " & Me._excelFile)

                    Me._sheetName = Me._myrule.SheetName
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Original de ignoreFirstRowla hoja: " & Me._sheetName)
                    Me._sheetName = VarInterReglas.ReconocerVariablesValuesSoloTexto(Me._sheetName)
                    Me._sheetName = TextoInteligente.ReconocerCodigo(Me._sheetName, r)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre de la hoja: " & Me._sheetName)

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Version de Office configurada en la regla: " & Me._myrule.ExcelVersion.ToString)

                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo datos del excel.")
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Usa Spire:" & Me._myrule.UseSpireConverter.ToString())
                    If Me._myrule.UseSpireConverter Then
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Abriendo Excel con Spire.")
                        Me._ds = New DataTable()
                        Dim sp As New SpireTools

                        Me._ds = sp.GetExcelAsDataSet(Me._excelFile, _sheetName) '.Tables(0)

                        '     _ds.Columns(0).ColumnName = "Nombre del Prospecto"
                        '         _ds.Columns(1).ColumnName = "Contacto"
                        '         _ds.Columns(2).ColumnName = "Mail del Contacto"

                        If ignoreFirstRow Then
                            _ds.Rows.RemoveAt(0)
                        End If

                    Else
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Abriendo Excel de forma convencional.")
                        Me.GetExcelData()
                    End If

                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Filas obtenidas: " & Me._ds.Rows.Count)

                    Me._varName = Me._myrule.VarName
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Original de la variable a guardar: " & Me._varName)
                    Me._varName = VarInterReglas.ReconocerVariablesValuesSoloTexto(Me._varName)
                    Me._varName = TextoInteligente.ReconocerCodigo(Me._varName, r)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre de la variable a guardar: " & Me._varName)


                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Guardar copia: " & Me._myrule.SaveAs.ToString())

                    If Me._myrule.SaveAs AndAlso Me._myrule.SaveAsPath.Trim() <> String.Empty AndAlso Me._myrule.SaveAsFileName.Trim() <> String.Empty Then
                        Me._saveAsPath = Me._myrule.SaveAsPath
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre del directorio : " & Me._saveAsPath)
                        Me._saveAsPath = VarInterReglas.ReconocerVariablesValuesSoloTexto(Me._saveAsPath)
                        Me._saveAsPath = TextoInteligente.ReconocerCodigo(Me._saveAsPath, r)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre del directorio (Resuelto): " & Me._saveAsPath)

                        Me._saveAsFileName = Me._myrule.SaveAsFileName
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre del archivo : " & Me._saveAsFileName)
                        Me._saveAsFileName = VarInterReglas.ReconocerVariablesValuesSoloTexto(Me._saveAsFileName)
                        Me._saveAsFileName = TextoInteligente.ReconocerCodigo(Me._saveAsFileName, r)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre del archivo (Resuelto) : " & Me._saveAsFileName)

                        Me._saveAsPath = Me._saveAsPath.Trim()
                        Me._saveAsFileName = Me._saveAsFileName.Trim()


                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando que el path generado no contenga caracteres invalidos...")
                        For Each _chrOriginal As Char In Me._saveAsPath.ToCharArray()
                            For Each _chrItem As Char In Path.GetInvalidPathChars()
                                If _chrOriginal = _chrItem Then
                                    _chrOriginal = Convert.ToChar("-")
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Se ha encontrado un caracter invalido y se reemplazó por '-'")
                                    Exit For
                                End If
                            Next
                        Next

                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando que el nombre del archivo generado no contenga caracteres invalidos...")
                        For Each _chrFileName As Char In Me._saveAsFileName.ToCharArray()
                            For Each _chrInv As Char In Path.GetInvalidFileNameChars()
                                If _chrFileName = _chrInv Then
                                    _chrFileName = Convert.ToChar("-")
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Se ha encontrado un caracter invalido y se reemplazó por '-'")
                                    Exit For
                                End If
                            Next
                        Next


                        'ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando directorio de destino... ")
                        'If Not Directory.Exists(Me._saveAsPath) Then
                        '    ZTrace.WriteLineIf(ZTrace.IsInfo, "El directorio especificado no existe, se procede a crearlo... ")
                        '    Directory.CreateDirectory(Me._saveAsPath)
                        '    ZTrace.WriteLineIf(ZTrace.IsInfo, "Directorio creado satisfactoriamente..")

                        'Else
                        '    ZTrace.WriteLineIf(ZTrace.IsInfo, "Se ha encontrado el directorio de destino")
                        'End If

                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando extensión del archivo... ")
                        If Not Path.GetExtension(Me._saveAsFileName).ToLower().Trim() = ".xls" Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "El archivo no contiene la extensión correcta, se procede a renombrar (xls)... ")
                            Me._saveAsFileName = Path.ChangeExtension(Me._saveAsFileName.Trim(), ".xls")
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "El archivo fue renombrado satisfactoriamente")
                        Else
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "El archivo contiene la extensión correcta (xls)")

                        End If

                        Dim _strFullPath As String = Path.Combine(Me._saveAsPath, Me._saveAsFileName)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Realizando copia del archivo... ")
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Path: '" & _strFullPath & "'")
                        FileCopy(_excelFile, _strFullPath)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Copia realizada satisfactoriamente..")

                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "No se puede continuar con la copia, falta uno de los datos para generar el path.")


                    End If





                    If VariablesInterReglas.ContainsKey(Me._varName.Trim) = False Then
                        VariablesInterReglas.Add(Me._varName.Trim, Me._ds)
                    Else
                        VariablesInterReglas.Item(Me._varName.Trim) = Me._ds
                    End If

                Next


            Else

                Me._excelFile = Me._myrule.File
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Original del archivo: " & Me._excelFile)
                Me._excelFile = VarInterReglas.ReconocerVariablesValuesSoloTexto(Me._excelFile)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre del archivo: " & Me._excelFile)

                Me._sheetName = Me._myrule.SheetName
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Nombre original de la hoja: " & Me._sheetName)
                Me._sheetName = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me._sheetName)
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Nombre de la hoja: " & Me._sheetName)

                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Version de Office configurada en la regla: " & Me._myrule.ExcelVersion.ToString)

                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo datos del excel.")
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Usa Spire:" & Me._myrule.UseSpireConverter.ToString())
                If Me._myrule.UseSpireConverter Then
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Abriendo Excel con Spire.")
                    Dim sp As New SpireTools
                    _ds = sp.GetExcelAsDataSet(Me._excelFile)

                    _ds.Columns(0).ColumnName = "Nombre del Prospecto"
                    _ds.Columns(1).ColumnName = "Contacto"
                    _ds.Columns(2).ColumnName = "Mail del Contacto"

                    _ds.Rows.RemoveAt(0)
                Else
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Abriendo Excel de forma convencional.")
                    Me.GetExcelData()
                End If

                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Filas obtenidas: " & _ds.Rows.Count)

                Me._varName = Me._myrule.VarName
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Original de la variable a guardar: " & Me._varName)
                Me._varName = VarInterReglas.ReconocerVariablesValuesSoloTexto(Me._varName)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre de la variable a guardar: " & Me._varName)

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Guardar copia: " & Me._myrule.SaveAs.ToString())

                If Me._myrule.SaveAs AndAlso Me._myrule.SaveAsPath.Trim() <> String.Empty AndAlso Me._myrule.SaveAsFileName.Trim() <> String.Empty Then
                    Me._saveAsPath = Me._myrule.SaveAsPath
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre del directorio : " & Me._saveAsPath)
                    Me._saveAsPath = VarInterReglas.ReconocerVariablesValuesSoloTexto(Me._saveAsPath)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre del directorio (Resuelto): " & Me._saveAsPath)

                    Me._saveAsFileName = Me._myrule.SaveAsFileName
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre del archivo : " & Me._saveAsFileName)
                    Me._saveAsFileName = VarInterReglas.ReconocerVariablesValuesSoloTexto(Me._saveAsFileName)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre del archivo (Resuelto) : " & Me._saveAsFileName)

                    Me._saveAsPath = Me._saveAsPath.Trim()
                    Me._saveAsFileName = Me._saveAsFileName.Trim()


                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando que el path generado no contenga caracteres invalidos...")
                    For Each _chrOriginal As Char In Me._saveAsPath.ToCharArray()
                        For Each _chrItem As Char In Path.GetInvalidPathChars()
                            If _chrOriginal = _chrItem Then
                                _chrOriginal = Convert.ToChar("-")
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se ha encontrado un caracter invalido y se reemplazó por '-'")
                                Exit For
                            End If
                        Next
                    Next

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando que el nombre del archivo generado no contenga caracteres invalidos...")
                    For Each _chrFileName As Char In Me._saveAsFileName.ToCharArray()
                        For Each _chrInv As Char In Path.GetInvalidFileNameChars()
                            If _chrFileName = _chrInv Then
                                _chrFileName = Convert.ToChar("-")
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se ha encontrado un caracter invalido y se reemplazó por '-'")
                                Exit For
                            End If
                        Next
                    Next


                    'ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando directorio de destino... ")
                    'If Not Directory.Exists(Me._saveAsPath) Then
                    '    ZTrace.WriteLineIf(ZTrace.IsInfo, "El directorio especificado no existe, se procede a crearlo... ")
                    '    Directory.CreateDirectory(Me._saveAsPath)
                    '    ZTrace.WriteLineIf(ZTrace.IsInfo, "Directorio creado satisfactoriamente..")

                    'Else
                    '    ZTrace.WriteLineIf(ZTrace.IsInfo, "Se ha encontrado el directorio de destino")
                    'End If

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando extensión del archivo... ")
                    If Not Path.GetExtension(Me._saveAsFileName).ToLower().Trim() = ".xls" Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "El archivo no contiene la extensión correcta, se procede a renombrar (xls)... ")
                        Me._saveAsFileName = Path.ChangeExtension(Me._saveAsFileName.Trim(), ".xls")
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "El archivo fue renombrado satisfactoriamente")
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "El archivo contiene la extensión correcta (xls)")

                    End If

                    Dim _strFullPath As String = Path.Combine(Me._saveAsPath, Me._saveAsFileName)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Realizando copia del archivo... ")
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Path: '" & _strFullPath & "'")
                    FileCopy(_excelFile, _strFullPath)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Copia realizada satisfactoriamente..")


                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "No se puede continuar con la copia, falta uno de los datos para generar el path.")



                End If

                If VariablesInterReglas.ContainsKey(Me._varName) = False Then
                    VariablesInterReglas.Add(Me._varName, Me._ds)
                Else
                    VariablesInterReglas.Item(Me._varName) = Me._ds
                End If

            End If
            Return results
        Finally
            VarInterReglas = Nothing
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
        Dim connectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Me._excelFile.Trim & ";Extended Properties=" & Chr(34) & "Excel 12.0 Xml;HDR=YES;" & Chr(34) & ";"
        If Me._excelFile.Trim.EndsWith(".xlsx", StringComparison.InvariantCultureIgnoreCase) Then
            connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Me._excelFile.Trim & ";Extended Properties=" & Chr(34) & "Excel 12.0 Xml;HDR=YES;" & Chr(34) & ";"
        Else
            connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Me._excelFile.Trim & ";Extended Properties=" & Chr(34) & "Excel 8.0;HDR=Yes;" & Chr(34) & ";"
        End If

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

#End Region


End Class
