Imports System.Data.OleDb
Imports System.IO
Imports System.Windows.Forms
Imports Zamba.FileTools


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
        _myrule = rule
    End Sub

#End Region

#Region "Metodos Publicos"

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Results count " & results.Count)
            If results.Count > 0 Then

                For Each r As ITaskResult In results

                    GetVarOfRule(r)

                    GetTableOfExcel()


                    If _myrule.SaveAs AndAlso Not String.IsNullOrEmpty(_myrule.SaveAsPath.Trim()) AndAlso Not String.IsNullOrEmpty(_myrule.SaveAsFileName.Trim()) Then

                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Guardar copia: " & _myrule.SaveAs.ToString())

                        GetVarToSaveFile(r)

                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando que el path generado no contenga caracteres invalidos...")

                        CopyFile()
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "No se puede continuar con la copia, falta uno de los datos para generar el path.")
                    End If

                    If VariablesInterReglas.ContainsKey(_varName.Trim) = False Then
                        VariablesInterReglas.Add(_varName.Trim, _ds, False)
                    Else
                        VariablesInterReglas.Item(_varName.Trim) = _ds
                    End If

                Next
            End If

            Return results
        Finally
            _excelFile = Nothing
            _sheetName = Nothing
            _varName = Nothing
            _ds = Nothing
        End Try
    End Function

    Private Sub CopyFile()
        For Each _chrOriginal As Char In _saveAsPath.ToCharArray()
            For Each _chrItem As Char In Path.GetInvalidPathChars()
                If _chrOriginal = _chrItem Then
                    _chrOriginal = Convert.ToChar("-")
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Se ha encontrado un caracter invalido y se reemplazó por '-'")
                    Exit For
                End If
            Next
        Next

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando que el nombre del archivo generado no contenga caracteres invalidos...")
        For Each _chrFileName As Char In _saveAsFileName.ToCharArray()
            For Each _chrInv As Char In Path.GetInvalidFileNameChars()
                If _chrFileName = _chrInv Then
                    _chrFileName = Convert.ToChar("-")
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Se ha encontrado un caracter invalido y se reemplazó por '-'")
                    Exit For
                End If
            Next
        Next

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando extensión del archivo... ")
        If Not Path.GetExtension(_saveAsFileName).ToLower().Trim() = ".xls" Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "El archivo no contiene la extensión correcta, se procede a renombrar (xls)... ")
            _saveAsFileName = Path.ChangeExtension(_saveAsFileName.Trim(), ".xls")
            ZTrace.WriteLineIf(ZTrace.IsInfo, "El archivo fue renombrado satisfactoriamente")
        Else
            ZTrace.WriteLineIf(ZTrace.IsInfo, "El archivo contiene la extensión correcta (xls)")

        End If

        Dim _strFullPath As String = Path.Combine(_saveAsPath, _saveAsFileName)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Realizando copia del archivo... ")
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Path: '" & _strFullPath & "'")
        FileCopy(_excelFile, _strFullPath)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Copia realizada satisfactoriamente..")
    End Sub

    Private Sub GetVarToSaveFile(r As ITaskResult)
        _saveAsPath = _myrule.SaveAsPath
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre del directorio : " & _saveAsPath)
        _saveAsPath = WFRuleParent.ReconocerVariablesValuesSoloTexto(_saveAsPath)
        _saveAsPath = TextoInteligente.ReconocerCodigo(_saveAsPath, r)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre del directorio (Resuelto): " & _saveAsPath)

        _saveAsFileName = _myrule.SaveAsFileName
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre del archivo : " & _saveAsFileName)
        _saveAsFileName = WFRuleParent.ReconocerVariablesValuesSoloTexto(_saveAsFileName)
        _saveAsFileName = TextoInteligente.ReconocerCodigo(_saveAsFileName, r)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre del archivo (Resuelto) : " & _saveAsFileName)

        _saveAsPath = _saveAsPath.Trim()
        _saveAsFileName = _saveAsFileName.Trim()
    End Sub

    Private Sub GetTableOfExcel()
        If _myrule.UseSpireConverter Then
            Trace.WriteLineIf(ZTrace.IsVerbose, "Abriendo Excel con Spire.")
            _ds = New DataTable()
            Dim sp As New SpireTools
            Try
                _ds = sp.GetExcelAsDataSet(_excelFile, _sheetName)
            Catch ex As Exception
                If ex.Message.ToLower.Contains("the process cannot access the file") AndAlso ex.Message.ToLower.Contains("because it is being used by another process") Then
                    Throw New Exception("Archivo abierto por fuera, por favor cierrelo para continuar.")
                Else
                    Throw ex
                End If
            End Try
            _ds.MinimumCapacity = _ds.Rows.Count
        Else
            Trace.WriteLineIf(ZTrace.IsVerbose, "Abriendo Excel de forma convencional.")
            GetExcelData()
        End If

        Dim dRow As New DataTable()
        Dim List As System.Data.DataRow
        Dim removeRowIndex As DataRow
        Dim RowCounter As Integer = 0
        Dim index As Integer = 0
        Dim rowIndex As DataRow = Nothing

        Dim dtt As New DataTableTools
        'dtt.RemoveEmptyRows(_ds)

        Trace.WriteLineIf(ZTrace.IsVerbose, "Filas obtenidas: " & _ds.Rows.Count)
    End Sub

    Private Sub GetVarOfRule(r As ITaskResult)
        _excelFile = _myrule.File
        _sheetName = _myrule.SheetName
        _varName = _myrule.VarName

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre original de la variable a guardar: " & _varName)
        _varName = WFRuleParent.ReconocerVariablesValuesSoloTexto(_varName)
        _varName = TextoInteligente.ReconocerCodigo(_varName, r)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre de la variable a guardar: " & _varName)

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre original del archivo: " & _excelFile)
        _excelFile = WFRuleParent.ReconocerVariablesValuesSoloTexto(_excelFile)
        _excelFile = TextoInteligente.ReconocerCodigo(_excelFile, r)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre del archivo: " & _excelFile)

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre original de la hoja: " & _sheetName)
        _sheetName = WFRuleParent.ReconocerVariablesValuesSoloTexto(_sheetName)
        _sheetName = TextoInteligente.ReconocerCodigo(_sheetName, r)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre de la hoja: " & _sheetName)

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Version de Office configurada en la regla: " & _myrule.ExcelVersion.ToString)

        Trace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo datos del excel.")
        Trace.WriteLineIf(ZTrace.IsVerbose, "Usa Spire:" & _myrule.UseSpireConverter.ToString())
    End Sub

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
        Dim fileExc As New FileInfo(_excelFile)
        Dim connectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & _excelFile.Trim & ";Extended Properties=" & Chr(34) & "Excel 12.0 Xml;HDR=YES;" & Chr(34) & ";"
        If _excelFile.Trim.EndsWith(".xlsx", StringComparison.InvariantCultureIgnoreCase) Then
            connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & _excelFile.Trim & ";Extended Properties=" & Chr(34) & "Excel 12.0 Xml;HDR=YES;" & Chr(34) & ";"
        Else
            connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & _excelFile.Trim & ";Extended Properties=" & Chr(34) & "Excel 8.0;HDR=Yes;" & Chr(34) & ";"
        End If

        Dim strSQL As String = "SELECT * FROM [" & _sheetName.Trim & "$]"
        _ds = New DataTable()
        Using excelConnection As OleDbConnection = New OleDbConnection(connectionString)
            excelConnection.Open()
            Dim dbCommand As OleDbCommand = New OleDbCommand(strSQL, excelConnection)
            Dim dataAdapter As OleDbDataAdapter = New OleDbDataAdapter(dbCommand)
            dataAdapter.Fill(_ds)
        End Using
        Return _ds
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

#End Region


End Class
