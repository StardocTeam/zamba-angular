Imports Zamba.Core
Imports Zamba.Data
Imports Zamba.Data.BarcodeFactory
Imports System.Windows.Forms

Public Class BarcodesBusiness
    Inherits ZClass

    Private Const NOMBRE_COLUMNA_EXISTENCIA_EN_BASE As String = "Existencia en Base"
    'Private Const NOMBRE_COLUMNA_RUTA_DOCUMENTO As String = "Ruta de Documento"
    Private Const VALOR_VERDADERO_EXISTENCIA_EN_BASE As String = "Si"
    Private Const VALOR_FALSO_EXISTENCIA_EN_BASE As String = "No"
    Private Const NOMBRE_COLUMNA_DOC_ID As String = "Nro de Documento"
    Private Const NOMBRE_COLUMNA_DOC_TYPE_ID As String = "Entidad"


    Public Overrides Sub Dispose()

    End Sub
    ''' <summary>
    ''' Devuelve una tabla con un informe de caratulas
    ''' </summary>
    ''' <returns>Informe caratulas</returns>
    Public Function getInforme() As DataSet
        Try
            Dim dsResultado As DataSet = Data.BarcodeFactory.getInforme()

            If IsNothing(dsResultado) OrElse
                0 = dsResultado.Tables.Count Then
                Return New DataSet()
            End If

            Dim tablaResultado As DataTable = dsResultado.Tables(0)

            ' Se agrega la fila "Existencia en Base" ...
            Dim columnaExistenciaEnBase As DataColumn =
             New DataColumn(NOMBRE_COLUMNA_EXISTENCIA_EN_BASE,
             Type.GetType("System.String"))
            tablaResultado.Columns.Add(columnaExistenciaEnBase)

            ' Se chequea que cada documento exista en base 
            ' y se asienta el la columna "Existencia en base"...
            For Each fila As DataRow In tablaResultado.Rows

                Dim path As String =
                Results_Factory.getPathForIdTypeIdDoc(CType(fila(NOMBRE_COLUMNA_DOC_ID), Int32),
                 CType(fila(NOMBRE_COLUMNA_DOC_TYPE_ID), Int32))

                'System.Console.WriteLine(path)
                If IO.File.Exists(path) Then
                    fila.Item(NOMBRE_COLUMNA_EXISTENCIA_EN_BASE) =
                     VALOR_VERDADERO_EXISTENCIA_EN_BASE
                Else
                    fila.Item(NOMBRE_COLUMNA_EXISTENCIA_EN_BASE) =
                     VALOR_FALSO_EXISTENCIA_EN_BASE
                End If
            Next

            ' Se quitan las columnas...
            tablaResultado.Columns.Remove(NOMBRE_COLUMNA_DOC_ID)
            tablaResultado.Columns.Remove(NOMBRE_COLUMNA_DOC_TYPE_ID)

            Return dsResultado
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return Nothing
        End Try
        Return Nothing
    End Function



    '''' -----------------------------------------------------------------------------
    '''' <summary>
    '''' Inserta la caratula en la base de Zamba
    '''' </summary>
    '''' <param name="newresult"></param>
    '''' <param name="DocTypeId"></param>
    '''' <param name="UserId"></param>
    '''' <param name="CaratulaId"></param>
    '''' <returns></returns>
    '''' <remarks>
    '''' </remarks>
    '''' <history>
    '''' 	[Hernan]	26/05/2006	Created
    '''' </history>
    '''' -----------------------------------------------------------------------------
    'Public  Function Insert(ByVal newresult As NewResult, _
    'ByVal DocTypeId As Int32, _
    'ByVal UserId As Int32, _
    'ByVal CaratulaId As Int32, _
    'ByVal DontOpenTaskAfterInsertInDoGenerateCoverPage As Boolean) As Boolean

    '    Try
    '        Dim insertresult As InsertResult
    '        insertresult = New Results_Business().Insert(newresult, False, False, False, False, True, False, False, DontOpenTaskAfterInsertInDoGenerateCoverPage)
    '        If insertresult = InsertResult.NoInsertado Then Return False
    '        'todo: falta ver cuando es reemplazo, si hay que hacer algo, si da clave unica violada
    '    Catch ex As Exception
    '        Zamba.Core.ZClass.raiseerror(ex)
    '        Return False
    '    End Try

    '    'inserto en ZBarCode
    '    Try
    '        InsertBarCode(newresult.ID, DocTypeId, UserId, CaratulaId)
    '    Catch ex As Exception
    '        Zamba.Core.ZClass.raiseerror(ex)
    '        Return False
    '    End Try
    '    Return True
    'End Function

    Public Function Insert(ByVal newresult As INewResult,
    ByVal docTypeId As Int64,
    ByVal UserId As Int64,
    ByVal CaratulaId As Int64,
    ByVal DontOpenTaskAfterInsertInDoGenerateCoverPage As Boolean) As Boolean
        Try
            Dim insertresult As InsertResult
            insertresult = New Results_Business().Insert(newresult, False, False, False, False, True, False, False, DontOpenTaskAfterInsertInDoGenerateCoverPage)
            If insertresult <> InsertResult.Insertado Then Return False
            InsertBarCodeInDB(newresult.ID, docTypeId, UserId, CaratulaId)
        Catch ex As Exception
            raiseerror(ex)
            Return False
        End Try
        Return True
    End Function

    Public Function Replicate(ByVal newresult As INewResult,
    ByVal docTypeId As Int64,
    ByVal UserId As Int64,
    ByVal CaratulaId As Int64,
    ByVal DontOpenTaskAfterInsertInDoGenerateCoverPage As Boolean) As Boolean
        Try
            Dim insertresult As InsertResult
            insertresult = New Results_Business().Insert(newresult, False, False, False, False, True, False, False, DontOpenTaskAfterInsertInDoGenerateCoverPage)
            If insertresult <> InsertResult.Insertado Then Return False
            ' InsertBarCodeInDB(newresult.ID, docTypeId, UserId, CaratulaId)
        Catch ex As Exception
            raiseerror(ex)
            Return False
        End Try
        Return True
    End Function

    Private BarcodeFactory As New BarcodeFactory
    Public Function Insert(ByVal Taskresult As ITaskResult,
    ByVal docTypeId As Int64,
    ByVal UserId As Int32,
    ByVal CaratulaId As Int32,
    ByVal DontOpenTaskAfterInsertInDoGenerateCoverPage As Boolean) As Boolean
        Try
            InsertBarCodeInDB(Taskresult.ID, docTypeId, UserId, CaratulaId)
        Catch ex As Exception
            raiseerror(ex)
            Return False
        End Try
        Return True
    End Function

    Private Sub InsertBarCodeInDB(ByVal resultid As Int64, ByVal docTypeId As Int64, ByVal UserId As Int32, ByVal CaratulaId As Int32)
        'Inserta caratula
        BarcodeFactory.InsertBarCode(resultid, docTypeId, UserId, CaratulaId)
        'Actualiza el IconId del result (60 = caratula no escaneada).
        Dim RB As New Results_Business
        RB.UpdateResultIcon(resultid, docTypeId, 60)
    End Sub


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Borra una caratula de Zamba
    ''' </summary>
    ''' <param name="CaratulaId"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub Delete(ByVal CaratulaId As Int32)
        'TODO store: SPGetDocIdAndDocType
        Dim DocId As New ArrayList
        Dim DocTypeId As New ArrayList
        Dim result As New ArrayList

        'Obtengo DocId y DocTypeId en base al CaratulaId
        Dim ds As DataSet = GetDocTypeAndDocIdByCaratulaId(CaratulaId)
        'si tiene replicas pregunto si quiere seguir eliminando
        If ds.Tables(0).Rows.Count > 1 Then
            If MessageBox.Show("La caratula tiene replicas y se eliminaran las mismas, desea continuar ?      ", "Generador de Codigo de Barras", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                Exit Sub
            End If
        End If
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            DocId.Add(ds.Tables(0).Rows(i).Item(0))
            DocTypeId.Add(ds.Tables(0).Rows(i).Item(1))
            DocId.Add(ds.Tables(0).Rows(i).Item(0))
            DocTypeId.Add(ds.Tables(0).Rows(i).Item(1))
        Next


        'obtengo results

        For i As Integer = 0 To DocId.Count - 1
            Dim result1 As Result = Results_Business.GetNewResult(DocId(i), DocTypeId(i))
            result.Add(result1)
        Next


        'borro el documento

        For i As Integer = 0 To result.Count - 1
            Results_Factory.Delete(result(i))
        Next


        'Borro en ZBarcode
        BorroEnZBarcode(CaratulaId)


    End Sub

    Public Function GetAutoCompleteIndexs(ByVal doctypeid As Int32) As DataTable
        If Cache.DocTypesAndIndexs.hsAutocompleteIndexs.ContainsKey(doctypeid) = False Then
            Dim dt As DataTable = Data.BarcodeFactory.GetAutoCompleteIndexs(doctypeid)
            Cache.DocTypesAndIndexs.hsAutocompleteIndexs.Add(doctypeid, dt)
        End If
        Return Cache.DocTypesAndIndexs.hsAutocompleteIndexs(doctypeid)
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Guarda la configuración del autocompletar en la base de datos
    ''' </summary>
    ''' <param name="doctypeid"></param>
    ''' <param name="indexid"></param>
    ''' <param name="tabla"></param>
    ''' <param name="col"></param>
    ''' <param name="esclave"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' 	[Marcelo]	22/05/2008	Modified
    '''     [Sebastián] 10/12/2008 Modified
    ''' </history>
    ''' -----------------------------------------------------------------------------


    Public Function GetLastOrden(ByVal dt As Int32) As Int16
        Return Data.BarcodeFactory.GetLastOrden(dt)
    End Function

    Public Function getIndexKey(ByVal id As Int32) As Zamba.Core.Index
        Return Data.BarcodeFactory.getIndexKey(id)
    End Function

    Public Function GetAutoIndexs(ByVal dt As Int32, ByVal IndexId As Int32) As DataSet
        Return Data.BarcodeFactory.GetAutoIndexs(dt, IndexId)
    End Function

    Public Function LoadRemarks(ByVal UserId As Integer) As ArrayList
        Return Data.BarcodeFactory.LoadRemarks(UserId)
    End Function

    Public Sub SaveRemark(ByVal UserId As Integer, ByVal remark As String)
        Data.BarcodeFactory.SaveRemark(UserId, remark)
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Construye y devuelve una consulta SQL.
    ''' </summary>
    ''' <param name="doctypeid"></param>
    ''' <param name="indexvalue"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    'Public  Function GetSql(ByVal doctypeid As Int32, ByVal indexvalue As Object) As String
    '    Return data.BarcodeFactory.GetSql(doctypeid, indexvalue)
    'End Function

    ' Codigo originalªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªª
    'Public  Function GetSentencia(ByVal DocTypeId As Int32, ByVal DataTemp As String) As DataSet
    '    Return data.BarcodeFactory.GetSentencia(DocTypeId, DataTemp)
    'End Function

    ' Modified by Gaston    ]]]]            
    Public Function GetSentencia(ByVal DocTypeId As Int32, ByVal DataTemp As ArrayList) As DataSet
        Return Data.BarcodeFactory.GetSentencia(DocTypeId, DataTemp)
    End Function

    Public Function GetDsIndexs(ByVal DocTypeId As Int32) As DataSet
        Return Data.BarcodeFactory.GetDsIndexs(DocTypeId)
    End Function


    ''' <summary>
    ''' Borra un autocompletar
    ''' </summary>
    ''' <param name="sp_DocTypeid"></param>
    ''' <param name="index"></param>
    ''' <History>
    ''' 	[Marcelo]	22/05/2008	Modified
    '''</history>
    ''' <remarks></remarks>


    ''' <summary>
    ''' Borra un autocompletar
    ''' </summary>
    ''' <param name="sp_DocTypeid"></param>
    ''' <History>
    ''' 	[Diego]	10/09/2008	Created
    '''</History>
    ''' <remarks></remarks>


    Public Overloads Function getZBARCODECOMPLETE() As DataSet
        Return BarcodeFactory.getZBARCODECOMPLETE()
    End Function
    Public Overloads Function getZBARCODECOMPLETEWithDistinctDocType() As DataSet
        Return BarcodeFactory.getZBARCODECOMPLETEWithDistinctDocType()
    End Function

    Public Sub insertZbarcodecomplete(ByVal psClave As Boolean, ByVal sp_DocTypeid As String, ByVal psIndexid As String, ByVal psTabla As String, ByVal psColumna As String, ByVal newOrden As String)
        BarcodeFactory.insertZbarcodecomplete(psClave, sp_DocTypeid, psIndexid, psTabla, psColumna, newOrden)
    End Sub


    Public Function dsFilterCaratulas(ByVal UserId As Int64) As DataTable
        Return BarcodeFactory.dsFilterCaratulas(UserId)
    End Function
    Public Function dsFilterCaratulas(ByVal UserId As Int64, ByVal fecha As DateTime) As DataTable
        Return dsFilterCaratulas(UserId, fecha, fecha)
    End Function
    Public Function dsFilterCaratulas(ByVal UserId As Int64, ByVal fechaInicial As DateTime, ByVal fechaFinal As DateTime) As DataTable
        Return BarcodeFactory.dsFilterCaratulas(UserId, fechaInicial, fechaFinal)
    End Function

    Public Function dsFilterCaratulas(ByVal fechaInicial As DateTime, ByVal fechaFinal As DateTime) As DataTable
        Return BarcodeFactory.dsFilterCaratulas(fechaInicial, fechaFinal)
    End Function

    Public Function dsAllCaratulas() As DataTable
        Return BarcodeFactory.dsAllCaratulas
    End Function

    Public Function HasSpecificIndexFilters(ByVal DocTypeId As Int32) As Boolean
        Return BarcodeFactory.HasSpecificIndexFilters(DocTypeId)
    End Function

End Class
