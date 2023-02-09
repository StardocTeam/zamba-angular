Imports Zamba
Imports Zamba.Data
Imports Zamba.Core
Imports System.Collections
Imports System.Collections.Generic
Imports System.Text
Imports Zamba.Servers

Public Class DocTypesBusiness
    Private indexs As Hashtable = New Hashtable
    Dim DTF As New DocTypesFactory
    Public Function GetDocCountFromADocType(ByVal Doctype As Int64) As Int32
        Return DTF.GetDocCountFromADocType(Doctype)
    End Function


    Public Function GetDocTypeIdByIndexId(ByVal indexID As Int64) As Generic.List(Of Int64)
        Dim tmpDocTypes As New Generic.List(Of Int64)
        Dim tmpDS As DataSet
        Try
            tmpDS = DTF.GetDocTypeIdByIndexId(indexID)

            If Not IsNothing(tmpDS) AndAlso Not IsNothing(tmpDS.Tables(0)) AndAlso tmpDS.Tables(0).Rows.Count > 0 Then
                For Each r As DataRow In tmpDS.Tables(0).Rows
                    tmpDocTypes.Add(Convert.ToInt64(r("DOC_TYPE_ID")))
                Next
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return tmpDocTypes

    End Function
    Public Function GetDocTypeIdByIndexId(ByVal indexID As Int64, ByVal userId As Int64) As Generic.List(Of Int64)

        Dim tmpDocTypes As New Generic.List(Of Int64)
        Dim tmpDS As DataSet

        Try
            'Comente lo siguiente para qeu me compile, Diego
            tmpDS = DTF.GetDocTypeIdByIndexId(indexID, userId)

            If Not IsNothing(tmpDS) AndAlso Not IsNothing(tmpDS.Tables(0)) AndAlso tmpDS.Tables(0).Rows.Count > 0 Then
                For Each r As DataRow In tmpDS.Tables(0).Rows
                    tmpDocTypes.Add(Convert.ToInt64(r("DOC_TYPE_ID")))
                Next
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return tmpDocTypes

    End Function





    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Esto borra el contenido de las tablas Doc_I, Doc_T, pero falta borrar las imagenes (archivos fisicos)
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub BorrarDocumentos(ByVal Id As Int64)
        DTF.BorrarDocumentos(Id)
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Elimina la relación entre un Doc_type y un Doc_type_group
    ''' </summary>
    ''' <param name="doctypeid"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	22/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub Remove_DocType_FromAll_DocTypesGroup(ByVal doctypeid As Int64)
        DTF.Remove_DocType_FromAll_DocTypesGroup(doctypeid)
    End Sub

    Public Function CheckTempFiles(ByVal Id As Int64) As Int32
        Return DTF.CheckTempFiles(Id)
    End Function







    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un Arraylist con con todos los objetos DocType de Zamba
    ''' </summary>
    ''' <param name="bolAddBlank">En true si se quiere agregar un DocType en blanco al arraylist</param>
    ''' <returns>Arraylist</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' 	[Marcelo]	13/08/2009	Modified
    ''' </history>
    Public Function GetDocTypesArrayList(Optional ByVal bolAddBlank As Boolean = False) As ArrayList
        Dim dsDocType1 As DataSet = DTF.GetDocTypesArrayList

        Dim DocTypes As New ArrayList
        Dim i As Int32

        If bolAddBlank Then
            DocTypes.Add(New DocType(0, "Ninguno", 0))
        End If

        For i = 0 To dsDocType1.Tables("DOC_TYPE").Rows.Count - 1
            Dim DocType As New DocType(Convert.ToInt64(dsDocType1.Tables("DOC_TYPE")(i)("DOC_TYPE_ID")), dsDocType1.Tables("DOC_TYPE")(i)("DOC_TYPE_NAME"), Convert.ToInt32(dsDocType1.Tables("DOC_TYPE")(i)("FILE_FORMAT_ID")), Convert.ToInt32(dsDocType1.Tables("DOC_TYPE")(i)("DISK_GROUP_ID")), Convert.ToInt32(dsDocType1.Tables("DOC_TYPE")(i)("THUMBNAILS")), Convert.ToInt32(dsDocType1.Tables("DOC_TYPE")(i)("ICON_ID")), Convert.ToInt32(dsDocType1.Tables("DOC_TYPE")(i)("OBJECT_TYPE_ID")), dsDocType1.Tables("DOC_TYPE")(i)("AUTONAME"), dsDocType1.Tables("DOC_TYPE")(i)("AUTONAME"), 0)
            DocTypes.Add(DocType)
        Next

        dsDocType1 = Nothing
        Return DocTypes
    End Function

    ''' <summary>
    ''' Obtiene un DOC_TYPE en base al ID
    ''' </summary>
    ''' <param name="DocTypeId"></param>
    ''' <returns></returns>
    ''' <history>
    ''' 	[Hernan]	22/05/2006	Created
    ''' 	[Marcelo]	07/08/2009	Modified - Agrego llamada a la cache
    ''' 	[Marcelo]	07/08/2009	Modified - Agrego salteo de cache
    ''' </history>
    ''' <remarks></remarks>
    Public Function GetDocType(ByVal DocTypeId As Int64) As DocType
        Try


            Dim core As ZCore = ZCore.GetInstance()
            Dim _cache = Cache.DocTypes.GetInstance()
            Dim IB As New IndexsBusiness

            If _cache.hsDocTypes.ContainsKey(DocTypeId) = False Then

                Dim docType As IDocType
                If Not _cache.hsBaseDocTypesWithoutIndexs.ContainsKey(DocTypeId) Then
                    docType = DTF.GetDocType(DocTypeId)
                    SyncLock _cache.hsBaseDocTypesWithoutIndexs.SyncRoot
                        If Not _cache.hsBaseDocTypesWithoutIndexs.ContainsKey(DocTypeId) Then
                            _cache.hsBaseDocTypesWithoutIndexs.Add(DocTypeId, docType)
                        End If
                    End SyncLock
                End If

                docType = _cache.hsBaseDocTypesWithoutIndexs.Item(DocTypeId)
                If docType Is Nothing Then
                    docType = DTF.GetDocType(DocTypeId)
                    If _cache.hsBaseDocTypesWithoutIndexs.ContainsKey(DocTypeId) Then
                        _cache.hsBaseDocTypesWithoutIndexs(DocTypeId) = docType
                    End If
                End If

                If docType Is Nothing Then Return Nothing
                docType.Indexs = core.FilterIndex(DocTypeId)
                core = Nothing

                If _cache.hsDocTypes.ContainsKey(DocTypeId) Then
                    _cache.hsDocTypes(DocTypeId) = docType
                Else
                    'Este try catch se pone asi ya que el problema es por el  , cuando se utilize el otro branch no hace falta
                    Try
                        _cache.hsDocTypes.Add(DocTypeId, docType)
                    Catch
                    End Try
                End If
            End If

            Return DirectCast(_cache.hsDocTypes(DocTypeId).Clone, IDocType)
        Catch ex As Exception
            ZClass.raiseerror(ex)

            Return Nothing
        End Try
    End Function



    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un doctype en base al nombre
    ''' </summary>
    ''' <param name="DocTypeName"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	22/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetDocType(ByVal DocTypeName As String) As DocType

        If Cache.DocTypesAndIndexs.hsDocTypesByName.ContainsKey(DocTypeName) = False Then
            Cache.DocTypesAndIndexs.hsDocTypesByName(DocTypeName) = DTF.GetDocType(DocTypeName)
        End If
        Return Cache.DocTypesAndIndexs.hsDocTypesByName(DocTypeName)
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un Dataset Tipeado con todos los datos de los Entidades existentes en Zamba
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetDocTypes() As DataSet
        Return DTF.GetDocTypes
    End Function




    'Public   Function GetDocTypes(ByVal UserId As Int32) As DSDocType
    'Dim DsDocType As New DsDocType

    'if Server.IsOracle then
    'Dim dstemp As DataSet
    ''Dim parNames() As String = {"GlobalUserId", "io_cursor"}
    'Dim parTypes() = {13, 13, 5}
    'Dim parValues() = {UserId, 2}
    'dstemp = Server.Con.ExecuteDataset("Z_USERDOCTYPES_PKG.Z_USERDOCTYPES", parValues)
    'dstemp.Tables(0).TableName = "DocTypes"
    'DsDocType.Merge(dstemp)
    'Else
    'Dim dstemp As DataSet
    'Dim parameters() = {UserId}
    'dstemp = Server.Con.ExecuteDataset("Z_USERDOCTYPES", parameters)
    'dstemp.Tables(0).TableName = "DocTypes"
    'DsDocType.Merge(dstemp)
    'End If
    'End Function

    'Public   Function GetDocTypesChilds() As DataSet
    '    Return DTF.GetDocTypesChilds
    'End Function
    Public Function GetIndexText(ByVal DocTypeId As Int32) As Int32
        Return DTF.GetIndexText(DocTypeId)
    End Function

    Private AccesingObjectDTNames As New Object

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene el nombre de un Doc_Type en base al ID
    ''' </summary>
    ''' <param name="DocTypeId"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	22/05/2006	Created
    '''     [Ezequiel]  10/09/09    Modified - Se agrego la opcion de cache la cual llama al autonombre del hash del entidad.
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetDocTypeName(ByVal DocTypeId As Int64) As String
        Return GetDocType(DocTypeId).Name
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Arraylist con los DOC_TYPE_NAMES
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	22/05/2006	Created
    ''' 	[Marcelo]	05/03/2006	Modified(Add if on the catch)
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetDocTypeNames() As ArrayList
        Return DTF.GetDocTypeNames
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Arraylist con los DOC_TYPE_NAMES_ID
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Marcelo]	28/02/2008	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetDocTypeNamesAndIds() As DataTable
        Return DTF.GetDocTypeNamesAndIds()
    End Function

#Region "Add"
    Public Function AddDocType(ByVal DocType As DocType) As Integer
        Return DTF.AddDocType(DocType)
    End Function
#End Region

#Region "Delete"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Elimina un DOC_Type. Recibe como parametro un objeto Doc_type
    ''' </summary>
    ''' <param name="DocType">Objeto doc_type que se desea eliminar</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	22/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub DelDocType(ByVal DocType As DocType)
        DTF.DelDocType(DocType)
    End Sub
#End Region


    Public Function DocTypeIsDuplicated(ByVal DocTypeName As String) As Boolean
        Return DTF.DocTypeIsDuplicated(DocTypeName)
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve la cantidad de valores que tiene cargado un indice  
    ''' </summary>
    ''' <param name="Doctypeid">ID del Tipo de documento</param>
    ''' <param name="Indexid">ID del Index</param>
    ''' <returns>Cantidad</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/06/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function IndexCountInDocuments(ByVal Doctypeid As Int64, ByVal Indexid As Int64) As Int64
        Try
            Return DTF.IndexCountInDocuments(Doctypeid, Indexid)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            'Si no pudo acceder a la columna, que devuelva 0
            Return 0
        End Try
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve la cantidad de documentos, en base al ID
    ''' </summary>
    ''' <param name="DocTypeId"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	22/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function DocumentsCount(ByVal DocTypeId As Int64) As Int32
        Return DTF.DocumentsCount(DocTypeId)
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Convierte un Dataset en un Arraylist, en base a una columna
    ''' </summary>
    ''' <param name="Ds"></param>
    ''' <param name="ColumnId"></param>
    '''     ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	22/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function ConvertDatasettoArraylist(ByVal Ds As DataSet, ByVal ColumnId As Int32) As ArrayList
        Return DTF.ConvertDatasettoArraylist(Ds, ColumnId)
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Crea un Entidad en base a otro ya existente.
    ''' </summary>
    ''' <param name="DocIDOrigen">ID del entidad original</param>
    ''' <param name="DocNameDestino">Nombre del nuevo entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Lo genera en el mismo volumen que el original
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function CopyDoc(ByVal DocIDOrigen As Int64, ByVal DocNameDestino As String) As Integer
        Return DTF.CopyDoc(DocIDOrigen, DocNameDestino)
    End Function

    'Public   Function GetIndexSchema(ByVal docTypeId As Int32) As DataSet
    '    Return DTF.GetIndexSchema(docTypeId)
    'End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' obtiene el ID de un entidad en base al nombre del mismo
    ''' </summary>
    ''' <param name="DocTypeName">Nombre del Doc_type del cual se desea averiguar el ID</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetDocTypeIdByName(ByVal DocTypeName As String) As Int32
        Return DTF.GetDocTypeIdByName(DocTypeName)
    End Function

#Region "LinkIndexs"
    Public Function IndexIsLinked(ByVal doctypeid As Int32, ByVal indexid As Int32) As Boolean
        Return DTF.IndexIsLinked(doctypeid, indexid)
    End Function


    Public Sub UpdateSomeRowsCascade(ByVal doctypeIdParent As Int32, ByVal indexParentId As Int32, ByVal Value As Object, ByVal whereindexId As Int32, ByVal wherevalue As Object)
        DTF.UpdateSomeRowsCascade(doctypeIdParent, indexParentId, Value, whereindexId, wherevalue)
    End Sub

#End Region

#Region "WF"



    Public Function GetAllWFDocTypesByWFIdOnlyForAdmin(ByVal WfId As Int64) As DataSet

        Return DTF.GetAllWFDocTypes(WfId)
    End Function

    Public Function GetDocTypeWfIds(ByVal DocTypeId As Int32, Optional ByVal WithCache As Boolean = True) As ArrayList
        If Not WithCache Then Return DTF.GetDocTypeWfIds(DocTypeId)
        If Not Cache.DocTypesAndIndexs.hsWFDocTypes.ContainsKey(DocTypeId) Then
            SyncLock (Cache.DocTypesAndIndexs.hsWFDocTypes)
                Dim dtswf As ArrayList = DTF.GetDocTypeWfIds(DocTypeId)
                If Not Cache.DocTypesAndIndexs.hsWFDocTypes.ContainsKey(DocTypeId) Then
                    Cache.DocTypesAndIndexs.hsWFDocTypes.Add(DocTypeId, dtswf)
                End If
            End SyncLock
        End If
        Return DirectCast(Cache.DocTypesAndIndexs.hsWFDocTypes.Item(DocTypeId), ArrayList)
    End Function

    Public Function GetDocTypeWfIds(ByVal DocTypeId As Int32, ByVal t As Transaction, Optional ByVal WithCache As Boolean = True) As ArrayList
        If Not WithCache Then Return DTF.GetDocTypeWfIds(DocTypeId, t)
        If Not Cache.DocTypesAndIndexs.hsWFDocTypes.ContainsKey(DocTypeId) Then
            SyncLock (Cache.DocTypesAndIndexs.hsWFDocTypes)
                If Not Cache.DocTypesAndIndexs.hsWFDocTypes.ContainsKey(DocTypeId) Then
                    Cache.DocTypesAndIndexs.hsWFDocTypes.Add(DocTypeId, DTF.GetDocTypeWfIds(DocTypeId, t))
                End If
            End SyncLock
        End If
        Return DirectCast(Cache.DocTypesAndIndexs.hsWFDocTypes.Item(DocTypeId), ArrayList)
    End Function

    Public Function GetDocTypeWorkFlowByWfId(ByVal WfId As Int32) As DataSet
        Return DTF.GetDocTypeWorkFlowByWfId(WfId)
    End Function

    Public Sub AsignDocType2Wf(ByVal WFID As Int64, ByVal DocTypeId As Int32)
        DTF.AsignDocType2Wf(WFID, DocTypeId)
    End Sub
    Public Sub RemoveDocTypefromWf(ByVal WFID As Int32, ByVal DocTypeId As Int32)
        DTF.RemoveDocTypefromWf(WFID, DocTypeId)
    End Sub
    Public Sub RemoveAllAsociationsByDT(ByVal DocTypeId As Int32)
        DTF.RemoveAllAsociationsByDT(DocTypeId)
    End Sub
#End Region

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Checkea para todos los Doc_Types que existan las respectivas tablas
    ''' DOC_I,DOC_T y DOC_D
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub CheckDockTypeTables()
        DTF.CheckDockTypeTables()
    End Sub

#Region "Eventos"
    Public Event CheckTable(ByVal str As String)
#End Region

#Region "AutoNombre"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve el Autonombre
    ''' </summary>
    ''' <param name="AutoNameCode"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Martin]	22/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Overloads Function GetAutoName(ByVal AutoNameCode As String, ByVal DocTypeName As String, ByVal CreateDate As Date, ByVal EditDate As Date, ByVal Indexs As List(Of IIndex)) As String
        Dim DTF As New DocTypesFactory
        Try
            Return DTF.GetAutoName(AutoNameCode, DocTypeName, CreateDate, EditDate, Indexs)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return DocTypeName
            DTF = Nothing
        End Try
    End Function
    Public Function GetAutoNameCode(ByVal AutoNameText As String, ByVal IndexTable As DataTable) As String
        Try
            Return DTF.GetAutoNameCode(AutoNameText, IndexTable)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return AutoNameText
        End Try
    End Function
    Public Function GetAutoNameText(ByVal AutoNameCode As String, ByVal IndexTable As DataTable) As String
        Try
            Return DTF.GetAutoNameText(AutoNameCode, IndexTable)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return AutoNameCode
        End Try
    End Function

#End Region
    ''' <summary>
    '''  Obtiene todos los indices de los documentos
    ''' </summary>
    ''' <returns>Dataset</returns>
    ''' <remarks></remarks>
    'Public   Function GetAllIndexs() As DataSet
    '    Return DTF.GetAllIndexs
    'End Function
    ''' <summary>
    ''' Cambia el orden de un indice asociado en la base de datos
    ''' </summary>
    ''' <param name="docTypeID">El ID del entidad al que pertenecen los indices</param>
    ''' <param name="selectedIndexID">El id del indice seleccionado </param>
    ''' <param name="selectedIndexOrder">El numero de orden del indice seleccionado</param>
    ''' <param name="modifiedIndexId">El id del indice que se va a modificar</param>
    ''' <param name="modifiedIndexOrder">El orden del indice que se va a modificar</param>
    ''' <remarks></remarks>
    Public Sub ChangeIndexOrder(ByVal docTypeID As Integer, ByVal selectedIndexID As Integer, ByVal selectedIndexOrder As Integer, ByVal modifiedIndexId As Integer, ByVal modifiedIndexOrder As Integer)
        DTF.ChangeIndexOrder(docTypeID, selectedIndexID, selectedIndexOrder, modifiedIndexId, modifiedIndexOrder)
    End Sub










    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Elimina las Tablas byvaleridas al Doc_type_ID
    ''' </summary>
    ''' <param name="DocType"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub DeleteTables(ByVal DocType As DocType)
        DTF.DeleteTables(DocType)
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Elimina los permisos asignados a un DOCTYPE.
    ''' Debe usarse al eliminar un Doc_type
    ''' </summary>
    ''' <param name="doctype"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub DeleteRights(ByVal doctype As DocType)
        DTF.DeleteRights(doctype)
    End Sub
    Public Sub RemoveIndex(ByVal Doctype As DocType, ByVal IndexId As Int64)
        DTF.RemoveIndex(Doctype, IndexId)
    End Sub
    Public Sub Removecolumn(ByVal doctype As DocType, ByVal indexidarray As ArrayList)
        DTF.Removecolumn(doctype, indexidarray)
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Establece un indice como obligatorio para un doc_type
    ''' </summary>
    ''' <param name="DocTypeId">Id del Tipo de documento</param>
    ''' <param name="indexid">Indice que se desea establecer como obligatorio</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub SetRequiredIndex(ByVal DocTypeId As Int32, ByVal indexid As Int32)
        DTF.SetRequiredIndex(DocTypeId, indexid)
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Agrega un Indice a un Entidad. Modifica la tabla DOC_I agregandole a la misma una columna
    ''' </summary>
    ''' <param name="doctype"></param>
    ''' <param name="IndexIdArray"></param>
    ''' <param name="IndexTypeArray"></param>
    ''' <param name="IndexLenArray"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history> 
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub AddColumn(ByVal doctype As DocType, ByVal IndexIdArray As ArrayList, ByVal IndexTypeArray As ArrayList, ByVal IndexLenArray As ArrayList)
        DTF.AddColumn(doctype, IndexIdArray, IndexTypeArray, IndexLenArray)
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Establece la relación entre el DOC_TYPE y los Indices
    ''' </summary>
    ''' <param name="indexid">ID del indice que se desea asignar</param>
    ''' <param name="doctypeid">Entidad al que se le desea asignar un Indice</param>
    ''' <param name="order">Orden en el que apareceran listados los indices.</param>
    ''' <param name="Required">Establece si el indice es obligatorio</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub adddoctyperelationindex(ByVal indexid As Int32, ByVal doctypeid As Int64, ByVal order As Int32, ByVal Required As Boolean, ByVal ISREFERENCED As Boolean)
        DTF.adddoctyperelationindex(indexid, doctypeid, order, Required, ISREFERENCED)
    End Sub


    Public Sub AddColumnTextindex(ByVal DocTypeId As Int64, ByVal IndexId As Int32)
        DTF.AddColumnTextindex(DocTypeId, IndexId)
    End Sub
    Public Function GetDocTypesbyUserRights(ByVal UserId As Int64, ByVal RightType As Zamba.Core.RightsType) As List(Of DocType)
        If (Cache.DocTypes.DocTypesByUserIdAndRightType.ContainsKey(UserId & "-" & RightType.ToString) = False) Then
            SyncLock Cache.DocTypes.DocTypesByUserIdAndRightType
                If (Cache.DocTypes.DocTypesByUserIdAndRightType.ContainsKey(UserId & "-" & RightType.ToString) = False) Then
                    Cache.DocTypes.DocTypesByUserIdAndRightType.Add(UserId & "-" & RightType.ToString, DTF.GetDocTypesbyUserRights(UserId, RightType))
                End If
            End SyncLock
        End If
        Return Cache.DocTypes.DocTypesByUserIdAndRightType(UserId & "-" & RightType.ToString)
    End Function




#Region "Permanent Collections"
#End Region

    Public Function GetDocTypesIdsAndNames() As DataSet
        Return DTF.GetDocTypesIdsAndNames
    End Function

    Public Function GetDocTypesIdsAndNames(DTIds As String) As DataSet
        Return DTF.GetDocTypesIdsAndNames(DTIds)
    End Function


    Public Function GetDocTypesIdsAndNamesSorted() As DataSet
        Dim dstemp As DataSet = DTF.GetDocTypesIdsAndNames
        Dim dv As DataView = dstemp.Tables(0).DefaultView
        dv.Sort = "Doc_Type_Name"

        Dim ds As DataSet = New DataSet()
        ds.Tables.Add(dv.ToTable)
        Return ds

    End Function

    Public Function GetAllDocTypes() As DataSet
        Return DTF.GetAllDocTypes
    End Function


    Public Function GetAllDocType() As DataTable
        Return DTF.GetAllDocType()
    End Function

    Public Sub Fill(ByRef instance As IDocType)
        If IsNothing(instance.Indexs) Then
        End If

    End Sub

    ''' <summary>
    ''' Obtiene las propiedades de atributos asociados de la tabla index_r_doc_type 
    ''' </summary>
    ''' <param name="doctypeid">Id de entidad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' Diego 18/7/2008 Created
    ''' [Ezequiel] 23/09/09 - Modified. Se aplico cache al metodo.
    ''' </history>
    Public Function GetIndexsProperties(ByVal doctypeid As Int64) As DataSet


        If Not Cache.DocTypesAndIndexs.hsIndexsProperties.ContainsKey(doctypeid) Then
            SyncLock (Cache.DocTypesAndIndexs.hsIndexsProperties)
                If Not Cache.DocTypesAndIndexs.hsIndexsProperties.ContainsKey(doctypeid) Then
                    Cache.DocTypesAndIndexs.hsIndexsProperties.Add(doctypeid, DTF.GetIndexsProperties(doctypeid))
                End If
            End SyncLock
        End If
        Return DirectCast(Cache.DocTypesAndIndexs.hsIndexsProperties.Item(doctypeid), DataSet)
    End Function


    Public Function getAsignedIndexAndNonAsignedByDocTypeId(ByVal DocTypeId As Int64) As DataSet
        Return DTF.getAsignedIndexAndNonAsignedByDocTypeId(DocTypeId)
    End Function


    ''' <summary>
    ''' Obtiene las propiedades de atributos asociados de la tabla index_r_doc_type 
    ''' </summary>
    ''' <param name="doctypeid">Id de entidad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>Diego 18/7/2008 Created</history>
    Public Function GetIndexsProperties(ByVal doctypeid As Int64, ByVal indexid As Int64) As DataSet
        Try
            Return DTF.GetIndexsProperties(doctypeid, indexid)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Obtiene las propiedades de atributos asociados de la tabla index_r_doc_type 
    ''' </summary>
    ''' <param name="doctypeid">Id de entidad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>Sebastián 24/9/2008 Created</history>
    'Public   Function GetIndexsPropertiesWithIndexType(ByVal doctypeid As Int64) As DataSet
    '    Try
    '        Return DTF.GetIndexsPropertiesWithIndexType(doctypeid)
    '    Catch ex As Exception
    '        ZClass.raiseerror(ex)
    '        Return Nothing
    '    End Try
    'End Function
    ''' <summary>
    ''' Obtiene las propiedades de atributos asociados de la tabla index_r_doc_type 
    ''' </summary>
    ''' <param name="doctypeid"></param>
    ''' <param name="indexid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>Diego 18/7/2008 Created</history>
    Public Function GetIndexRestrictions(ByVal doctypeid As Int64, ByVal indexid As Int64) As DataSet
        Try
            Return DTF.GetIndexRestrictions(doctypeid, indexid)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function

    Public Shared Sub ClearHashTables()
        SyncLock (Cache.DocTypesAndIndexs.hsDocTypesByName)
            Cache.DocTypesAndIndexs.hsDocTypesByName = New SynchronizedHashtable
        End SyncLock
        SyncLock (Cache.DocTypesAndIndexs.hsWFDocTypes)
            Cache.DocTypesAndIndexs.hsWFDocTypes = New SynchronizedHashtable
        End SyncLock
        SyncLock (Cache.DocTypesAndIndexs.hsIndexsProperties)
            Cache.DocTypesAndIndexs.hsIndexsProperties = New SynchronizedHashtable
        End SyncLock

        DocTypesBusiness.ClearHashTables()
    End Sub

    Public Class IndexReferences

        ''' <summary>
        ''' Método que llama al método que permite guardar en la base de datos los correspondientes valores asociados a un índice referenciado
        ''' </summary>
        ''' <param name="docTypeId">Id de un entidad</param>
        ''' <param name="indexId">Id de un índice</param>
        ''' <param name="server">Nombre del servidor</param>
        ''' <param name="dataBase">Nombre de la base de datos</param>
        ''' <param name="user">Nombre de usuario</param>
        ''' <param name="table">Nombre de la tabla</param>
        ''' <param name="column">Nombre de la columna</param>
        ''' <param name="ZIndexReference">Colección que contiene una o más relaciones</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' 	[Gaston]	17/12/2008	Modified    
        ''' </history>
        Public Function SaveIndexReference(ByRef docTypeId As Int64, ByRef indexId As Int64, ByRef server As String, ByRef dataBase As String,
                                          ByRef user As String, ByRef table As String, ByRef column As String,
                                          ByRef ZIndexReference As Generic.List(Of ZIndexReference)) As Boolean
            Try

                ZIndexReferenceDAC.SaveIndexReference(docTypeId, indexId, server, dataBase, user, table, column, ZIndexReference)

                'Actualizo la vista
                updateView(docTypeId)
                Return True
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return False
            End Try
        End Function
        Public Function IsIndexReferencedWithDocType(ByVal IndexId As Int64, ByVal DocTypeId As Int64) As Boolean
            Return ZIndexReferenceDAC.IsIndexReferencedWithDocType(IndexId, DocTypeId)
        End Function
        ''' <summary>
        ''' Actualiza la vista
        ''' </summary>
        ''' <param name="lstIndexReference">Listado con las referencias a ser agregadas</param>
        ''' <remarks></remarks>
        Public Sub updateView(ByVal DocTypeId As Int64)
            Dim lstColKeys As Dictionary(Of String(), String) = New Dictionary(Of String(), String)
            Dim lstColSelects As Dictionary(Of String, String()) = New Dictionary(Of String, String())
            Dim lstColIndexs As Dictionary(Of String, String) = New Dictionary(Of String, String)
            Dim dsIndex As DataSet = Nothing

            Try
                dsIndex = ZIndexReferenceDAC.GetIndexReference(DocTypeId)

                If Not IsNothing(dsIndex) AndAlso dsIndex.Tables.Count > 0 Then
                    For Each drSelectIndex As DataRow In dsIndex.Tables(0).Rows
                        If lstColIndexs.ContainsKey(drSelectIndex("ReferenceId")) = False Then
                            Dim strCol As StringBuilder = New StringBuilder()
                            If Not IsDBNull(drSelectIndex("IServer")) AndAlso String.IsNullOrEmpty(drSelectIndex("IServer")) = False Then
                                strCol.Append(drSelectIndex("IServer"))
                                strCol.Append(".")
                            End If
                            If Not IsDBNull(drSelectIndex("IDataBase")) AndAlso String.IsNullOrEmpty(drSelectIndex("IDataBase")) = False Then
                                strCol.Append(drSelectIndex("IDataBase"))
                                strCol.Append(".")
                            End If
                            If Not IsDBNull(drSelectIndex("IUser")) AndAlso String.IsNullOrEmpty(drSelectIndex("IUser")) = False Then
                                strCol.Append(drSelectIndex("IUser"))
                                strCol.Append(".")
                            End If
                            If Not IsDBNull(drSelectIndex("ITable")) AndAlso String.IsNullOrEmpty(drSelectIndex("ITable")) = False Then
                                strCol.Append(drSelectIndex("ITable"))
                            End If


                            lstColIndexs.Add(drSelectIndex("ReferenceId"), strCol.ToString().ToUpper())
                        End If

                        Dim item As String() = {drSelectIndex("IId"), drSelectIndex("IColumn")}
                        lstColSelects.Add(drSelectIndex("ReferenceId"), item)
                    Next

                    dsIndex = ZIndexReferenceDAC.GetIndexKeyReference(DocTypeId)
                    If Not IsNothing(dsIndex) AndAlso dsIndex.Tables.Count > 0 Then
                        For Each drIndex As DataRow In dsIndex.Tables(0).Rows
                            Dim item As String() = {drIndex("IndexId").ToString().ToUpper(), drIndex("IColumn").ToString().ToUpper()}
                            lstColKeys.Add(item, drIndex("ReferenceId"))
                        Next
                    End If

                    ZIndexReferenceDAC.UpdateView(DocTypeId, lstColKeys, lstColIndexs, lstColSelects)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                lstColKeys.Clear()
                lstColKeys = Nothing
                lstColSelects.Clear()
                lstColSelects = Nothing
                If Not IsNothing(dsIndex) Then
                    dsIndex.Dispose()
                    dsIndex = Nothing
                End If
            End Try
        End Sub



    End Class
End Class
