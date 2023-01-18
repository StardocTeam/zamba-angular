Imports Zamba.Data
Imports System.ComponentModel
Imports System.Text
Imports System.IO

Public Class DataGeneratorBusiness
    Implements IDisposable

    Private PATH_NAMES As String
    Private PATH_LASTNAMES As String
    Private PATH_COMPANIES As String
    Private PATH_ADRESSES As String
    Private PATH_USERS As String
    Private PATH_XML As String
    Private dicGenerators As New Dictionary(Of DataGeneratorCategory, List(Of String))
    Private dataGen As New DataGenerator

    Private Enum MergeType
        Multiply = 0
        FillMainOnly = 1
    End Enum

    ''' <summary>
    ''' Instancia un objeto que provee funcionalidad para la generación de datos aleatorios
    ''' </summary>
    ''' <param name="applicationStartupPath">Ruta del ejecutable. En el directorio del mismo deberá 
    ''' existir una carpeta llamada "Listas" con todos los archivos txt de diccionarios.</param>
    ''' <remarks></remarks>
    Public Sub New(applicationStartupPath As String)
        PATH_NAMES = applicationStartupPath & "\Listas\Nombres.txt"
        PATH_LASTNAMES = applicationStartupPath & "\Listas\Apellidos.txt"
        PATH_COMPANIES = applicationStartupPath & "\Listas\Empresas.txt"
        PATH_ADRESSES = applicationStartupPath & "\Listas\Direcciones.txt"
        PATH_USERS = applicationStartupPath & "\Listas\Usuarios.txt"
        PATH_XML = applicationStartupPath & "\LastGenerationConfig.xml"
    End Sub

    ''' <summary>
    ''' Genera datos aleatorios a partir de un archivo de diccionario y actualiza los registros de una tabla especificada
    ''' </summary>
    ''' <param name="dataGeneratorHelpers"></param>
    ''' <param name="bgwDataGeneration"></param>
    ''' <remarks></remarks>
    Public Sub GenerateGenericData(ByVal table As DataGeneratorHelper, _
                                  bgwDataGeneration As BackgroundWorker)
        Dim dt As DataTable = Nothing
        Dim lstCategories As List(Of String) = Nothing
        Dim lstColumns As New List(Of String)
        Dim lstValues As New List(Of String)
        Dim query As String
        Dim listCount As Int32
        Dim i As Int32 = 0
        Dim dtRowsCount As Int32 = 0

        Try
            'Obtiene los datos a modificar
            dt = dataGen.GetIds(table.TableName, table.ColumnKey)

            'Total de filas por columnas a modificar equivale al total de updates a realizar en la tabla
            dtRowsCount = dt.Rows.Count

            'Se verifica la existencia de datos a modificar
            If dtRowsCount > 0 Then
                'Se identifica la tabla modificada
                bgwDataGeneration.ReportProgress(0, table.TableName)

                'Actualiza la tabla destino con datos del diccionario de manera aleatoria
                For Each dr As DataRow In dt.Rows
                    'Verifica si el proceso debe ser cancelado
                    If bgwDataGeneration.CancellationPending Then
                        Exit Sub
                    End If

                    'Se regeneran los valores aleatorios
                    For Each column As DataGeneratorHelper.ColumnHelper In table.Columns
                        'Se carga el diccionario de datos
                        lstCategories = LoadDictionary(column.Category)
                        lstValues.Add(lstCategories(Math.Ceiling(Rnd() * lstCategories.Count - 1)))
                    Next

                    dataGen.UpdateRow(table.TableName, _
                                        table.Columns, _
                                        lstValues, _
                                        table.ColumnKey, _
                                        dr(0).ToString)

                    'Aumenta la barra de progreso
                    If bgwDataGeneration IsNot Nothing Then
                        i += 1
                        bgwDataGeneration.ReportProgress((i / dtRowsCount) * 100)
                    End If

                    lstValues.Clear()
                Next
            End If

            Throw New ZambaEx("Registros modificados de manera exitosa")
        Finally
            If dt IsNot Nothing Then
                dt.Dispose()
                dt = Nothing
            End If
            lstValues = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Genera datos aleatorios para las entidades seleccionadas
    ''' </summary>
    ''' <param name="dataGeneratorHelpers"></param>
    ''' <param name="bgwDataGeneration"></param>
    ''' <remarks></remarks>
    Public Sub GenerateEntityData(ByVal dataGeneratorHelpers As List(Of DataGeneratorHelper), _
                                  bgwDataGeneration As BackgroundWorker)
        Dim dt As DataTable = Nothing
        Dim lstValues As List(Of String) = Nothing
        Dim query As String
        Dim listCount As Int32
        Dim i As Int32
        Dim dtRowsCount As Int32 = 0

        Try
            'Se recorren todas las tablas a modificar
            For Each table As DataGeneratorHelper In dataGeneratorHelpers
                'Obtiene los datos a modificar
                dt = dataGen.GetIds(table.TableName, table.ColumnKey)

                'Total de filas por columnas a modificar equivale al total de updates a realizar en la tabla
                'TODO: MODIFICAR EL CODIGO PARA EJECUTAR SOLO 1 UPDATE POR ROW Y NO UN UPDATE POR ROW Y POR COLUMNA
                dtRowsCount = dt.Rows.Count * table.Columns.Count

                'Se verifica la existencia de datos a modificar
                If dtRowsCount > 0 Then
                    'Se identifica la tabla modificada
                    bgwDataGeneration.ReportProgress(0, table.TableName)

                    'Se recorre cada columna a modificar
                    For Each column As DataGeneratorHelper.ColumnHelper In table.Columns
                        'Se carga el diccionario de datos
                        lstValues = LoadDictionary(column.Category)
                        listCount = lstValues.Count

                        'Verifica que el diccionario tenga datos
                        If listCount > 0 Then
                            i = 0

                            'Actualiza la tabla destino con datos del diccionario de manera aleatoria
                            For Each dr As DataRow In dt.Rows
                                'Verifica si el proceso debe ser cancelado
                                If bgwDataGeneration.CancellationPending Then
                                    Exit Sub
                                End If

                                'Verifica que el registro no sea uno filtrado
                                If Not column.Filter.Contains(dr(0)) Then
                                    dataGen.UpdateRow(table.TableName, _
                                                      column.ColumnName, _
                                                      lstValues(Math.Ceiling(Rnd() * listCount - 1)), _
                                                      table.ColumnKey, _
                                                      dr(0).ToString)
                                End If

                                'Aumenta la barra de progreso
                                If bgwDataGeneration IsNot Nothing Then
                                    i += 1
                                    bgwDataGeneration.ReportProgress((i / dtRowsCount) * 100)
                                End If
                            Next
                        End If
                    Next
                End If
            Next

            Throw New ZambaEx("Registros modificados de manera exitosa")
        Finally
            If dt IsNot Nothing Then
                dt.Dispose()
                dt = Nothing
            End If
            lstValues = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Carga el diccionario de datos en una lista
    ''' </summary>
    ''' <param name="selectedGeneration">Tipo de dato a generar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function LoadDictionary(ByVal selectedGeneration As DataGeneratorCategory) As List(Of String)
        'Verifica si el tipo de dato no fue cargado
        If Not dicGenerators.ContainsKey(selectedGeneration) Then
            Dim mainList As List(Of String) = Nothing
            Dim tempList As List(Of String) = Nothing

            'Selección del tipo de generación de datos
            Select Case selectedGeneration
                Case DataGeneratorCategory.Name
                    mainList = LoadFileData(PATH_NAMES)

                Case DataGeneratorCategory.LastName
                    mainList = LoadFileData(PATH_LASTNAMES)

                Case DataGeneratorCategory.User
                    mainList = LoadFileData(PATH_USERS)

                Case DataGeneratorCategory.Company
                    mainList = LoadFileData(PATH_COMPANIES)

                Case DataGeneratorCategory.Address
                    mainList = LoadFileData(PATH_ADRESSES)

                Case DataGeneratorCategory.NameAndLastName
                    mainList = LoadDictionary(DataGeneratorCategory.Name).ToList()
                    tempList = LoadDictionary(DataGeneratorCategory.LastName).ToList()
                    mainList = MergeLists(mainList, tempList, MergeType.Multiply)

                Case DataGeneratorCategory.LastNameAndName
                    mainList = LoadDictionary(DataGeneratorCategory.LastName).ToList()
                    tempList = LoadDictionary(DataGeneratorCategory.Name).ToList()
                    mainList = MergeLists(mainList, tempList, MergeType.Multiply)

                Case DataGeneratorCategory.AddressAndNumber
                    mainList = LoadDictionary(DataGeneratorCategory.Address).ToList()
                    tempList = LoadDictionary(DataGeneratorCategory.Number).ToList()
                    mainList = MergeLists(mainList, tempList, MergeType.FillMainOnly)

                Case DataGeneratorCategory.Mail
                    mainList = LoadDictionary(DataGeneratorCategory.NameAndLastName).ToList()
                    For i As Int32 = 0 To mainList.Count - 1
                        mainList(i) = mainList(i).Replace(" ", ".") & "@mail.com"
                    Next

                Case DataGeneratorCategory.DNI
                    'Este tipo dato debe mejorarse permitiendo configurar sus límites
                    Dim rand As New Random(Now.Millisecond)
                    mainList = New List(Of String)
                    For i As Int32 = 0 To 99
                        mainList.Add(rand.Next(10000000, 40000000))
                    Next

                Case DataGeneratorCategory.Patent
                    mainList = New List(Of String)
                    For i As Int32 = 0 To 999
                        mainList.Add("ABC" & i.ToString("000"))
                    Next

                Case DataGeneratorCategory.Phone
                    'Este tipo dato debe mejorarse permitiendo configurar su formato y números
                    mainList = New List(Of String)
                    For i As Int32 = 0 To 9999
                        mainList.Add("5218" & i.ToString("0000"))
                    Next

                Case DataGeneratorCategory.Number
                    'Este tipo dato debe mejorarse permitiendo configurar sus límites
                    mainList = New List(Of String)
                    For i As Int32 = 0 To 9999
                        mainList.Add(i.ToString)
                    Next

                Case DataGeneratorCategory.CUIT
                    'Este tipo dato debe mejorarse permitiendo configurar sus límites
                    mainList = LoadDictionary(DataGeneratorCategory.DNI).ToList()
                    For i As Int32 = 0 To mainList.Count - 1
                        mainList(i) = "30" & mainList(i) & "4"
                    Next

                Case DataGeneratorCategory.Patent123
                    mainList = New List(Of String)
                    For i As Int32 = 0 To 999
                        mainList.Add(i.ToString("000"))
                    Next

                Case DataGeneratorCategory.PatentABC
                    Dim rand As New Random(DateTime.Now.Millisecond)
                    Dim abc As String
                    mainList = New List(Of String)
                    For i As Int32 = 0 To 100
                        abc = (Convert.ToChar(rand.Next(97, 122))).ToString & _
                                     (Convert.ToChar(rand.Next(97, 122))).ToString & _
                                     (Convert.ToChar(rand.Next(97, 122))).ToString
                        mainList.Add(abc)
                    Next

            End Select

            'Se agrega al diccionario para no volver a realizar su carga
            dicGenerators.Add(selectedGeneration, mainList)
        End If

        Return dicGenerators(selectedGeneration)
    End Function

    ''' <summary>
    ''' Carga el contenido de un archivo en una lista
    ''' </summary>
    ''' <param name="path">Ruta del archivo diccionario</param>
    ''' <returns>List(Of String)</returns>
    ''' <remarks></remarks>
    Private Function LoadFileData(ByVal path As String) As List(Of String)
        Dim list As New List(Of String)

        Using fl As New System.IO.StreamReader(path)
            While (fl.EndOfStream() = False)
                list.Add(fl.ReadLine())
            End While
        End Using

        Return list
    End Function

    ''' <summary>
    ''' Obtiene una tabla con los datos de los atributos en uso
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAttributes() As DataTable
        'Se obtienen todos los atributos de Zamba
        Dim dtAttributes As DataTable = IndexsBusiness.GetIndexsDsIdsAndNames().Tables(0)

        'Se agrega la columna categoría y filtros
        Dim colCategory As New DataColumn("category", GetType(String))
        dtAttributes.Columns.Add(colCategory)
        Dim colFilter As New DataColumn("filter", GetType(String))
        dtAttributes.Columns.Add(colFilter)

        'Elimina los atributos en desuso
        Dim validAttributeIds As List(Of Int64) = dataGen.GetLinkedDocIAttributes()
        Dim i As Int32 = 0
        While i < dtAttributes.Rows.Count
            If validAttributeIds.Contains(dtAttributes.Rows(i)("Index_Id")) Then
                i += 1
            Else
                dtAttributes.Rows.RemoveAt(i)
            End If
        End While
        validAttributeIds.Clear()
        validAttributeIds = Nothing

        'Verifica que existan atributos a completar
        If dtAttributes IsNot Nothing AndAlso dtAttributes.Rows.Count > 0 Then
            Dim dsTemp As DataSet = Nothing
            Dim dtTemp As DataTable = Nothing
            Try
                'Verifica si existe un trabajo anterior realizado a cargar
                If File.Exists(PATH_XML) Then
                    'Carga los datos del archivo en un dataset temporal
                    dsTemp = New DataSet
                    dsTemp.ReadXml(PATH_XML)
                    dtTemp = dsTemp.Tables(0)

                    'Luego se copian los datos al datatable que será de fuente para el datagridview
                    'La copia no se hace de manera directa porque cabe la posibilidad de que los
                    'datos del archivo no se encuentren actualizados
                    For i = 0 To dtAttributes.Rows.Count - 1
                        For j As Int32 = 0 To dsTemp.Tables(0).Rows.Count - 1
                            If dtAttributes.Rows(i)("Index_Id") = dtTemp.Rows(j)("Index_Id") Then
                                dtAttributes.Rows(i)("category") = dtTemp.Rows(j)("category")
                                dtAttributes.Rows(i)("filter") = dtTemp.Rows(j)("filter")
                            End If
                        Next
                    Next
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                If dtTemp IsNot Nothing Then
                    dtTemp.Dispose()
                    dtTemp = Nothing
                End If
                If dsTemp IsNot Nothing Then
                    dsTemp.Dispose()
                    dsTemp = Nothing
                End If
            End Try
        End If

        Return dtAttributes
    End Function

    ''' <summary>
    ''' Obtiene todas las columnas de una tabla
    ''' </summary>
    ''' <param name="table">Tabla</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetColumns(ByVal table As String) As DataTable
        Dim dtTables As DataTable = dataGen.GetAllTableColumns(table)

        'Se agrega la columna categoría y filtros
        Dim colCategory As New DataColumn("category", GetType(String))
        dtTables.Columns.Add(colCategory)
        'Dim colFilter As New DataColumn("filter", GetType(String))
        'dtTables.Columns.Add(colFilter)

        Return dtTables
    End Function

    ''' <summary>
    ''' Hace la union entre dos listados
    ''' </summary>
    ''' <param name="mainList"></param>
    ''' <param name="tempList"></param>
    ''' <param name="mergeType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MergeLists(mainList As List(Of String), _
                                        tempList As List(Of String), _
                                        mergeType As MergeType) As List(Of String)
        If mergeType = DataGeneratorBusiness.MergeType.Multiply Then
            Dim mergedList As New List(Of String)
            For i As Int32 = 0 To mainList.Count - 1
                For j As Int32 = 0 To tempList.Count - 1
                    mergedList.Add(mainList(i) & " " & tempList(j))
                Next
            Next
            Return mergedList
        Else
            Dim rand As New Random(Now.Millisecond)
            Dim tempMaxIndex As Int32 = tempList.Count - 1
            For i As Int32 = 0 To mainList.Count - 1
                mainList(i) &= " " & tempList(rand.Next(0, tempMaxIndex))
            Next
            Return mainList
        End If
    End Function

    ''' <summary>
    ''' Obtiene un listado con las categorías de los datos a generar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDataCategories() As String()
        'Se crea un listado con las categorias del enumerador mas un espacio en blanco
        Dim categories() As String = [Enum].GetNames(GetType(DataGeneratorCategory))
        Dim source() As String = New String(categories.Length) {}
        source(0) = String.Empty
        For i As Int32 = 1 To source.Length - 1
            source(i) = GetCategoryDescription(i - 1)
        Next
        Return source
    End Function

    ''' <summary>
    ''' Obtiene la descripción de cada item del enumerador DataGenerationCategory
    ''' </summary>
    ''' <param name="category">Enumerador DataGenerationCategory</param>
    ''' <returns>Descripcion</returns>
    ''' <remarks></remarks>
    Private Function GetCategoryDescription(category As DataGeneratorCategory) As String
        Select Case category
            Case DataGeneratorCategory.Name
                Return "Nombre"
            Case DataGeneratorCategory.LastName
                Return "Apellido"
            Case DataGeneratorCategory.NameAndLastName
                Return "Nombre y Apellido"
            Case DataGeneratorCategory.LastNameAndName
                Return "Apellido y Nombre"
            Case DataGeneratorCategory.Company
                Return "Empresa"
            Case DataGeneratorCategory.Mail
                Return "Correo"
            Case DataGeneratorCategory.Phone
                Return "Teléfono"
            Case DataGeneratorCategory.DNI
                Return "DNI"
            Case DataGeneratorCategory.Number
                Return "Número"
            Case DataGeneratorCategory.Address
                Return "Dirección"
            Case DataGeneratorCategory.AddressAndNumber
                Return "Dirección y número"
            Case DataGeneratorCategory.Patent
                Return "Patente"
            Case DataGeneratorCategory.CUIT
                Return "CUIT"
            Case DataGeneratorCategory.User
                Return "Usuario"
            Case DataGeneratorCategory.Patent123
                Return "Nº de patente"
            Case DataGeneratorCategory.PatentABC
                Return "Letras de patente"
        End Select
    End Function

    ''' <summary>
    ''' Obtiene el enumerador DataGeneratorCategory a partir de su descripcion
    ''' </summary>
    ''' <param name="category">Descripción de DataGeneratorCategory</param>
    ''' <returns>Enumerador</returns>
    ''' <remarks></remarks>
    Public Function GetCategoryEnumerator(categoryDescription As String) As DataGeneratorCategory
        Select Case categoryDescription
            Case "Nombre"
                Return DataGeneratorCategory.Name
            Case "Apellido"
                Return DataGeneratorCategory.LastName
            Case "Nombre y Apellido"
                Return DataGeneratorCategory.NameAndLastName
            Case "Apellido y Nombre"
                Return DataGeneratorCategory.LastNameAndName
            Case "Empresa"
                Return DataGeneratorCategory.Company
            Case "Correo"
                Return DataGeneratorCategory.Mail
            Case "Teléfono"
                Return DataGeneratorCategory.Phone
            Case "DNI"
                Return DataGeneratorCategory.DNI
            Case "Número"
                Return DataGeneratorCategory.Number
            Case "Dirección"
                Return DataGeneratorCategory.Address
            Case "Dirección y número"
                Return DataGeneratorCategory.AddressAndNumber
            Case "Patente"
                Return DataGeneratorCategory.Patent
            Case "CUIT"
                Return DataGeneratorCategory.CUIT
            Case "Usuario"
                Return DataGeneratorCategory.User
            Case "Nº de patente"
                Return DataGeneratorCategory.Patent123
            Case "Letras de patente"
                Return DataGeneratorCategory.PatentABC
        End Select
    End Function

    ''' <summary>
    ''' Obtiene una previsualización de la tabla deseada
    ''' </summary>
    ''' <param name="table">Tabla a previsualizar</param>
    ''' <param name="filterTop100">True para obtener los primeros 100 registros</param>
    ''' <returns>Datatable</returns>
    ''' <remarks></remarks>
    Public Function GetTablePreview(ByVal table As String, _
                                    ByVal filterTop100 As Boolean, _
                                    Optional ByVal lstAttributes As List(Of Int64) = Nothing) As DataTable

        'Si es una entidad se busca obtener unicamente las columnas seleccionadas
        If table.ToUpper.StartsWith("DOC_I") AndAlso lstAttributes IsNot Nothing Then
            Dim docTypeId As String = table.Remove(0, 5)
            Dim dtAttributes As DataTable = dataGen.GetLinkedDocIAttributes(docTypeId, lstAttributes)
            Dim sbAttributes As New StringBuilder

            For i As Int32 = 0 To dtAttributes.Rows.Count - 1
                sbAttributes.Append(dtAttributes(i)(0))
                sbAttributes.Append(",")
            Next
            sbAttributes = sbAttributes.Remove(sbAttributes.Length - 1, 1)

            Return dataGen.GetTablePreview(table, filterTop100, sbAttributes.ToString)
        Else
            Return dataGen.GetTablePreview(table, filterTop100, String.Empty)
        End If
    End Function

    ''' <summary>
    ''' Obtiene una tabla con los nombres de las tablas a modificar
    ''' </summary>
    ''' <param name="lstAttributes">Lista de atributos seleccionados para modificar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTablesNames(ByVal lstAttributes As List(Of Int64)) As Object()
        Dim dtInfo As DataTable
        Dim i As Int32
        Dim indexId As Int64

        'Obtiene el id y nombre de las tablas SLST e ILST
        dtInfo = dataGen.GetLinkedSlstAndIlst(lstAttributes)

        'Se remueven los ids de los atributos slst e ilst
        For i = 0 To dtInfo.Rows.Count - 1
            indexId = CLng(dtInfo(i)("ID"))
            If lstAttributes.Contains(indexId) Then
                lstAttributes.Remove(indexId)
            End If
        Next

        'Se elimina la columna ID
        dtInfo.Columns.Remove("ID")

        'Verifica la existencia de atributos que no sean slst o ilst
        If lstAttributes.Count > 0 Then
            'Obtiene el id y nombre de las tablas DOC que contengan atributos que no sean SLST ni ILST
            dtInfo.Merge(dataGen.GetLinkedDocI(lstAttributes))
        End If

        'Convierte el datatable en un Object()
        Dim results() As Object
        results.Resize(results, dtInfo.Rows.Count)
        For i = 0 To dtInfo.Rows.Count - 1
            results(i) = dtInfo(i)(0).ToString
        Next

        Return results
    End Function

    ''' <summary>
    ''' Guarda en un XML las ultimas modificaciones hechas de configuracion de atributos
    ''' </summary>
    ''' <param name="dtAttributes"></param>
    ''' <remarks></remarks>
    Public Sub SaveChanges(ByVal dtAttributes As DataTable)
        Try
            'Se guarda la configuración realizada
            dtAttributes.DataSet.WriteXml(PATH_XML, XmlWriteMode.IgnoreSchema)
        Catch ex As Exception
            ZClass.raiseerror(New Exception("El archivo de configuración no pudo ser guardado", ex))
        End Try
    End Sub

    ''' <summary>
    ''' Obtiene todos los nombres de las tablas de la base de datos en forma de lista
    ''' </summary>
    ''' <returns>Lista de nombres de las tablas</returns>
    ''' <remarks></remarks>
    Public Function GetAllTableNamesList() As List(Of String)
        Dim lstTables As New List(Of String)
        Dim dtTables As DataTable = dataGen.GetAllTableNames

        For i As Int32 = 0 To dtTables.Rows.Count - 1
            lstTables.Add(dtTables.Rows(i)(0).ToString)
        Next

        dtTables.Dispose()
        dtTables = Nothing

        Return lstTables
    End Function


#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.

        End If
        disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
