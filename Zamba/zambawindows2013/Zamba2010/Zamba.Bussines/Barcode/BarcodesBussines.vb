Imports Zamba.Data
Imports Zamba.Data.BarcodeFactory

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
    Public Shared Function getInforme() As DataSet
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

    Public Shared Function GetBarCodeByDocTypeIdAndDocId(docId As Integer, docTypeID As Integer) As Integer

        Dim barcodeId As Integer = BarcodeFactory.GetBarcodeIdByDocIdAndDocTypeID(docId, docTypeID)

        Return barcodeId
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Inserta la caratula en la base de Zamba
    ''' </summary>
    ''' <param name="newresult"></param>
    ''' <param name="DocTypeId"></param>
    ''' <param name="UserId"></param>
    ''' <param name="CaratulaId"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function Insert(ByVal newresult As NewResult,
    ByVal docTypeId As Int64,
    ByVal UserId As Int32,
    ByVal CaratulaId As Int32,
    ByVal DontOpenTaskAfterInsertInDoGenerateCoverPage As Boolean) As Boolean

        'Inserto un newresult
        '  Dim docId As Long
        Try
            Dim insertresult As InsertResult
            insertresult = Results_Business.InsertDocument(newresult, False, False, False, False, True, False, False, DontOpenTaskAfterInsertInDoGenerateCoverPage)
            '   docId = newresult.Id
            If insertresult <> Core.InsertResult.Insertado Then Return False

            InsertBarCode(newresult.ID, docTypeId, UserId, CaratulaId)

            'todo: falta ver cuando es reemplazo, si hay que hacer algo, si da clave unica violada
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return False
        End Try

        Return True
    End Function




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
    Public Shared Sub Delete(ByVal CaratulaId As Int32)
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

    Public Shared Function GetAutoCompleteIndexs(ByVal docTypeId As Int64) As DataTable
        If Cache.DocTypesAndIndexs.hsAutocompleteIndexs.ContainsKey(docTypeId) = False Then
            Dim dt As DataTable = Data.BarcodeFactory.GetAutoCompleteIndexs(docTypeId)
            Cache.DocTypesAndIndexs.hsAutocompleteIndexs.Add(docTypeId, dt)
        End If
        Return Cache.DocTypesAndIndexs.hsAutocompleteIndexs(docTypeId)
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
    Public Shared Sub Insertar(ByVal docTypeId As Int64, ByVal indexid As Int32, ByVal tabla As String, ByVal col As String, ByVal esclave As Boolean, ByVal indexGroup As Boolean, ByVal FilterByIndex As Boolean, Optional ByVal conditions As String = "=", Optional ByVal Where_condition As String = " ")

        Data.BarcodeFactory.Insertar(docTypeId, indexid, tabla, col, esclave, indexGroup, FilterByIndex, conditions, Where_condition)
        'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
        UserBusiness.Rights.SaveAction(docTypeId, ObjectTypes.DocTypes, RightsType.Edit, "Se creó un Autocompletar para la entidad " & docTypeId & ", Atributo " & indexid & ", tabla " & tabla & ", columna " & col)

    End Sub

    Public Shared Function GetLastOrden(ByVal dt As Int32) As Int16
        Return Data.BarcodeFactory.GetLastOrden(dt)
    End Function

    Public Shared Function getIndexKey(ByVal id As Int32) As Zamba.Core.Index
        Return Data.BarcodeFactory.getIndexKey(id)
    End Function

    Public Shared Function GetAutoIndexs(ByVal dt As Int32, ByVal IndexId As Int32) As DataSet
        Return Data.BarcodeFactory.GetAutoIndexs(dt, IndexId)
    End Function

    Public Shared Function LoadRemarks(ByVal UserId As Integer) As ArrayList
        Return Data.BarcodeFactory.LoadRemarks(UserId)
    End Function

    Public Shared Sub SaveRemark(ByVal UserId As Integer, ByVal remark As String)
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
    'Public Shared Function GetSql(ByVal docTypeId as Int64, ByVal indexvalue As Object) As String
    '    Return data.BarcodeFactory.GetSql(doctypeid, indexvalue)
    'End Function

    ' Codigo originalªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªªª
    'Public Shared Function GetSentencia(ByVal docTypeId as Int64, ByVal DataTemp As String) As DataSet
    '    Return data.BarcodeFactory.GetSentencia(DocTypeId, DataTemp)
    'End Function

    ' Modified by Gaston    ]]]]            
    Public Shared Function GetSentencia(ByVal docTypeId As Int64, ByVal DataTemp As ArrayList) As DataSet
        Return Data.BarcodeFactory.GetSentencia(docTypeId, DataTemp)
    End Function

    Public Shared Function GetDsIndexs(ByVal docTypeId As Int64) As DataSet
        Return Data.BarcodeFactory.GetDsIndexs(docTypeId)
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
    Public Overloads Shared Sub deleteZbarcodecomplete(ByVal sp_DocTypeid As Int64, ByVal indexId As Int64)
        BarcodeFactory.deleteZbarcodecomplete(sp_DocTypeid, indexId)

        'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
        UserBusiness.Rights.SaveAction(sp_DocTypeid, ObjectTypes.DocTypes, RightsType.Edit, "Se eliminó un Autocompletar cuyo id de entidad es " & sp_DocTypeid & " y su Atributo es " & indexId)
    End Sub

    ''' <summary>
    ''' Borra un autocompletar
    ''' </summary>
    ''' <param name="sp_DocTypeid"></param>
    ''' <History>
    ''' 	[Diego]	10/09/2008	Created
    '''</History>
    ''' <remarks></remarks>
    Public Overloads Shared Sub deleteZbarcodecomplete(ByVal sp_DocTypeid As Int64)
        BarcodeFactory.deleteZbarcodecomplete(sp_DocTypeid)

        'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
        UserBusiness.Rights.SaveAction(sp_DocTypeid, ObjectTypes.DocTypes, RightsType.Edit, "Se eliminó un Autocompletar cuyo id de entidad es " & sp_DocTypeid)
    End Sub
    Public Overloads Shared Sub deleteZbarcodecomplete(ByVal sp_DocTypeid As String)
        BarcodeFactory.deleteZbarcodecomplete(sp_DocTypeid)
    End Sub
    Public Overloads Shared Sub deleteZbarcodecomplete()
        BarcodeFactory.deleteZbarcodecomplete()
    End Sub

    Public Overloads Shared Function getZBARCODECOMPLETE() As DataSet
        Return BarcodeFactory.getZBARCODECOMPLETE()
    End Function
    Public Overloads Shared Function getZBARCODECOMPLETEWithDistinctDocType() As DataSet
        Return BarcodeFactory.getZBARCODECOMPLETEWithDistinctDocType()
    End Function

    Public Shared Sub insertZbarcodecomplete(ByVal psClave As Boolean, ByVal sp_DocTypeid As String, ByVal psIndexid As String, ByVal psTabla As String, ByVal psColumna As String, ByVal newOrden As String)
        BarcodeFactory.insertZbarcodecomplete(psClave, sp_DocTypeid, psIndexid, psTabla, psColumna, newOrden)
    End Sub


    Public Shared Function dsFilterCaratulas(ByVal UserId As Integer) As DataTable
        Return BarcodeFactory.dsFilterCaratulas(UserId)
    End Function
    Public Shared Function dsFilterCaratulas(ByVal UserId As Integer, ByVal fecha As DateTime) As DataTable
        Return dsFilterCaratulas(UserId, fecha, fecha)
    End Function
    Public Shared Function dsFilterCaratulas(ByVal UserId As Integer, ByVal fechaInicial As DateTime, ByVal fechaFinal As DateTime) As DataTable
        Return BarcodeFactory.dsFilterCaratulas(UserId, fechaInicial, fechaFinal)
    End Function

    Public Shared Function dsFilterCaratulas(ByVal fechaInicial As DateTime, ByVal fechaFinal As DateTime) As DataTable
        Return BarcodeFactory.dsFilterCaratulas(fechaInicial, fechaFinal)
    End Function

    Public Shared Function dsAllCaratulas() As DataTable
        Return BarcodeFactory.dsAllCaratulas
    End Function

    Public Shared Function HasSpecificIndexFilters(ByVal docTypeId As Int64) As Boolean
        Return BarcodeFactory.HasSpecificIndexFilters(docTypeId)
    End Function



End Class
