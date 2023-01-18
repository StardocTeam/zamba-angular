Imports Zamba
Imports Zamba.Data
Imports Zamba.Core
Imports System.Collections
Imports System.Collections.Generic
Imports System.Text
Imports Zamba.Services.RemoteInterfaces

Public Class DocTypesBusiness
    Private Shared indexs As Hashtable = New Hashtable
    Private Shared IgnoreIsReIndex As Boolean = False
    Public Shared Sub GetEditRights(ByVal DocType As DocType)
        Try
            If DocType.RightsLoaded = False Then
                DocType.IsReadOnly = Not UserBusiness.Rights.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, Zamba.Core.RightsType.Edit, DocType.ID)
                '[Sebastian 11-08-2009] se agrego el flag para obviar el permiso de re index para que 
                'permita insertar sin necesidad de tener ese permiso.
                If IgnoreIsReIndex = False Then
                    DocType.IsReindex = UserBusiness.Rights.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, Zamba.Core.RightsType.ReIndex, DocType.ID)
                Else
                    DocType.IsReindex = True
                End If
                DocType.IsShared = UserBusiness.Rights.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, Zamba.Core.RightsType.Share, DocType.ID)
                DocType.RightsLoaded = True
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Verifica que todos los documentos tengan su DOCB
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CreateDOCBTables()
        Dim docTypeId As Int64

        For Each dr As DataRow In GetDocTypeIds().Rows
            Try
                ZClass.RaiseInfo("Creando Tabla DOC_B" & docTypeId, "Creacion de Tablas BLOB")
                docTypeId = Int64.Parse(dr.Item(0).ToString)
                DocTypesFactory.CreateDOCBTable(docTypeId)
                updateView(docTypeId)
            Catch ex As Exception
                ZClass.raiseerror(ex)
                ZClass.RaiseNotifyError(ex.Message)
            End Try
        Next
    End Sub

    Public Function GetDocTypeIds() As DataTable
        Return DocTypesFactory.GetDocTypeIds()
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

    ''' <summary>
    ''' Obtiene los índices a mappear para outlook
    ''' </summary>
    ''' <returns>Tabla que contiene 3 columnas: DOC_TYPE_ID, INDEX_ID y MAIL_ATTRIB.</returns>
    ''' <remarks></remarks>
    Public Function GetDocTypeIndexMaps() As DataTable
        Return DocTypesFactory.GetDocTypeIndexMaps()
    End Function

    Public Function GetDocTypeIndexMaps(EntityId As Int64) As DataTable
        Return DocTypesFactory.GetDocTypeIndexMaps(EntityId)
    End Function

    ''' <summary>
    ''' Obtiene los índices a mappear para outlook
    ''' </summary>
    ''' <returns>Devuelve un conjunto de datos de la siguiente manera: EntryId, FolderName, DocTypeId, DOC_TYPE_NAME.</returns>
    ''' <remarks></remarks>
    Public Function GetFolderDocTypeMaps() As DataTable
        Return DocTypesFactory.GetFolderDocTypeMaps()
    End Function



    ''' <summary>
    ''' [Sebastian 11-08-2009] setea el flag para ignorar o no el permiso is re index
    ''' </summary>
    ''' <param name="IgnoreRIsReIndex"></param>
    ''' <remarks></remarks>
    Public Shared Sub InsertingDoc(ByVal IgnoreRIsReIndex As Boolean)
        IgnoreIsReIndex = IgnoreRIsReIndex
    End Sub
    Public Shared Function GetDocCountFromADocType(ByVal Doctype As Int64) As Int32
        Return DocTypesFactory.GetDocCountFromADocType(Doctype)
    End Function
    Public Shared Function GetDocTypeIdByIndexId(ByVal indexID As Int64) As Generic.List(Of Int64)
        Dim tmpDocTypes As New Generic.List(Of Int64)
        Dim tmpDS As DataSet
        Try
            tmpDS = DocTypesFactory.GetDocTypeIdByIndexId(indexID)

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
    Public Shared Function GetDocTypeIdByIndexId(ByVal indexID As Int64, ByVal userId As Int64) As Generic.List(Of Int64)

        Dim tmpDocTypes As New Generic.List(Of Int64)
        Dim tmpDS As DataSet

        Try
            'Comente lo siguiente para qeu me compile, Diego
            tmpDS = DocTypesFactory.GetDocTypeIdByIndexId(indexID, userId)

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
    ''' Define Permisos para doctypes
    ''' </summary>
    ''' <history>
    ''' 	[Diego]	17/03/2008	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    'Public Shared Sub SetDocTypeRight(ByVal DoctypeId As Int64, ByVal RightType As DocTypeRights, ByVal Value As Int32)
    '    DocTypesFactory.SetDocTypeRight(DoctypeId, RightType, Value)
    'End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene Permisos para Entidades
    ''' </summary>
    ''' <history>
    ''' 	[Diego]	17/03/2008	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    'Public Shared Function LoadDocTypeRightValue(ByVal DoctypeId As Int64, ByVal RightType As DocTypeRights) As Int32
    '    Return DocTypesFactory.LoadDocTypeRightValue(DoctypeId, RightType)
    'End Function

    Public Shared Function GetIndexSubstitutionTable(ByVal indexID As Int64) As DataTable
        Return AutoSubstitutionBusiness.GetIndexData(indexID, False)
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
    Public Shared Sub BorrarDocumentos(ByVal Id As Int64)
        DocTypesFactory.BorrarDocumentos(Id)
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
    Public Shared Sub Remove_DocType_FromAll_DocTypesGroup(ByVal doctypeid As Int64)
        DocTypesFactory.Remove_DocType_FromAll_DocTypesGroup(doctypeid)
    End Sub

    Public Shared Function CheckTempFiles(ByVal Id As Int64) As Int32
        Return DocTypesFactory.CheckTempFiles(Id)
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve True en caso que el doc_type este asignado a algun archivo
    ''' </summary>
    ''' <param name="DocTypeId">ID de la entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function DocTypeIsAsigned(ByVal DocTypeId As Int64) As Boolean
        Return DocTypesFactory.DocTypeIsAsigned(DocTypeId)
    End Function
    'Public Shared Function GetDocTypes(ByVal UserId As Int32) As DSDocType
    '    Dim DsDocType1 As New DSDocType
    '    Dim DSTEMP As DataSet
    '    if Server.IsOracle then
    '        Dim parnames() As String = {"UserId", "io_cursor"}
    '        Dim parval() As Object = {UserId, 2}
    '        Dim partypes() As Object = {13, 5}
    '        DSTEMP = Server.Con.ExecuteDataset("GetDocTypeRights_PKG.GetDocTypeRights",  parval)
    '    Else
    '        Dim parval() As String = {UserId}

    '        DSTEMP = Server.Con.ExecuteDataset("GetDocTypeRights", parval)
    '    End If
    '    DSTEMP.Tables(0).TableName = "Doc_Type"
    '    DsDocType1.Merge(DSTEMP)
    '    Return DsDocType1
    'End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un DataSet con todos los datos de los entidades existentes en Zamba
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetDocTypesDsDocType() As DSDOCTYPE
        Return DocTypesFactory.GetDocTypesDsDocType()
    End Function


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un DataSet con todos los datos de los entidades existentes en Zamba
    ''' vale aclarar que este no es tipado como el que hizo Hernan.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Alejandro]	14/08/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetDocTypesDataSet() As DataSet
        Return DocTypesFactory.GetDocTypesDataSet
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
    Public Shared Function GetDocTypesArrayList(Optional ByVal bolAddBlank As Boolean = False) As ArrayList
        Dim dsDocType1 As DSDOCTYPE = DocTypesFactory.GetDocTypesArrayList

        Dim DocTypes As New ArrayList
        Dim i As Int32

        If bolAddBlank = True Then
            DocTypes.Add(New DocType(0, "Ninguno", 0))
        End If

        For i = 0 To dsDocType1.DOC_TYPE.Count - 1
            Dim DocType As New DocType(Convert.ToInt32(dsDocType1.DOC_TYPE(i).DOC_TYPE_ID()), dsDocType1.DOC_TYPE(i).DOC_TYPE_NAME(), Convert.ToInt32(dsDocType1.DOC_TYPE(i).FILE_FORMAT_ID()), Convert.ToInt32(dsDocType1.DOC_TYPE(i).DISK_GROUP_ID()), Convert.ToInt32(dsDocType1.DOC_TYPE(i).THUMBNAILS()), Convert.ToInt32(dsDocType1.DOC_TYPE(i).ICON_ID()), Convert.ToInt32(dsDocType1.DOC_TYPE(i).OBJECT_TYPE_ID()), dsDocType1.DOC_TYPE(i).AUTONAME(), dsDocType1.DOC_TYPE(i).AUTONAME(), Convert.ToInt32(dsDocType1.DOC_TYPE(i).DOCCOUNT()), 0, Convert.ToInt32(dsDocType1.DOC_TYPE(i).DOCUMENTALID()))
            DocTypes.Add(DocType)
        Next

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
    'Public Shared Function GetDocType(ByVal DocTypeId As Int64, ByVal useCache As Boolean = True) As DocType
    '    If useCache = False Then
    '        Dim docType As IDocType = DocTypesFactory.GetDocType(DocTypeId)
    '        'analizar el uso de ambos cache de indices y mejorar dejando uno solo.
    '        docType.Indexs = ZCore.FilterIndex(DocTypeId)
    '        IndexsBusiness.GetIndexsSchemaAsListOf(DocTypeId, True)
    '        GetEditRights(docType)
    '        FormBusiness.GetAllForms(DocTypeId, True)
    '        RestrictionsMapper_Factory.getRestrictionIndexs(Membership.MembershipHelper.CurrentUser.ID, DocTypeId)
    '        Return docType
    '    ElseIf Cache.DocTypesAndIndexs.hsDocTypes.Contains(DocTypeId) = False Then
    '        Dim docType As IDocType = DocTypesFactory.GetDocType(DocTypeId)
    '        docType.Indexs = ZCore.FilterIndex(DocTypeId)
    '        IndexsBusiness.GetIndexsSchemaAsListOf(DocTypeId, True)
    '        GetEditRights(docType)
    '        FormBusiness.GetAllForms(DocTypeId, True)
    '        RestrictionsMapper_Factory.getRestrictionIndexs(Membership.MembershipHelper.CurrentUser.ID, DocTypeId)

    '        If Cache.DocTypesAndIndexs.hsDocTypes.Contains(DocTypeId) Then
    '            Cache.DocTypesAndIndexs.hsDocTypes(DocTypeId) = docType
    '        Else
    '            'Este try catch se pone asi ya que el problema es por el shared, cuando se utilize el otro branch no hace falta
    '            Try
    '                Cache.DocTypesAndIndexs.hsDocTypes.Add(DocTypeId, docType)
    '            Catch
    '            End Try
    '        End If
    '    End If
    '    Return DirectCast(Cache.DocTypesAndIndexs.hsDocTypes(DocTypeId).Clone, DocType)
    'End Function
    Public Shared Function GetDocType(ByVal DocTypeId As Int64, Optional ByVal useCache As Boolean = True) As DocType
        If Cache.DocTypesAndIndexs.hsDocTypes.ContainsKey(DocTypeId) = False Or useCache = False Then
            Dim docType As IDocType = DocTypesFactory.GetDocType(DocTypeId)
            docType.Indexs = ZCore.FilterIndex(DocTypeId)
            GetEditRights(docType)
            If useCache = True Then
                Try
                    Cache.DocTypesAndIndexs.hsDocTypes.Add(DocTypeId, docType)
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            Else
                Return docType
            End If
        End If
        Return DirectCast(DirectCast(Cache.DocTypesAndIndexs.hsDocTypes.Item(DocTypeId), DocType).Clone, DocType)
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
    Public Shared Function GetDocType(ByVal DocTypeName As String, ByVal UseCache As Boolean) As DocType
        If UseCache = False Then Return DocTypesFactory.GetDocType(DocTypeName)
        If Cache.DocTypesAndIndexs.hsDocTypesByName.ContainsKey(DocTypeName) = False Then
            Cache.DocTypesAndIndexs.hsDocTypesByName(DocTypeName) = DocTypesFactory.GetDocType(DocTypeName)
        End If
        Return DirectCast(Cache.DocTypesAndIndexs.hsDocTypesByName(DocTypeName).Clone, DocType)
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
    Public Shared Function GetDocTypes() As DSDOCTYPE
        Return DocTypesFactory.GetDocTypes
    End Function




    'Public Shared Function GetDocTypes(ByVal UserId As Int32) As DSDocType
    'Dim DsDocType As New DsDocType

    'if Server.IsOracle then
    'Dim dstemp As DataSet
    'Dim parNames() As String = {"GlobalUserId", "io_cursor"}
    'Dim parTypes() = {13, 13, 5}
    'Dim parValues() = {UserId, 2}
    'dstemp = Server.Con.ExecuteDataset("Z_USERDOCTYPES_PKG.Z_USERDOCTYPES",  parValues)
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

    'Public Shared Function GetDocTypesChilds() As DataSet
    '    Return DocTypesFactory.GetDocTypesChilds
    'End Function
    Public Shared Function GetIndexText(ByVal docTypeId As Int64) As Int32
        Return DocTypesFactory.GetIndexText(DocTypeId)
    End Function

    Private Shared AccesingObjectDTNames As New Object

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
    '''     [Ezequiel]  10/09/09    Modified - Se agrego la opcion de cache la cual llama al autonombre del hash de la entidad.
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetDocTypeName(ByVal docTypeId As Int64, ByVal WithCache As Boolean) As String
        SyncLock (AccesingObjectDTNames)
            If Not WithCache Then Return DocTypesFactory.GetDocTypeName(DocTypeId)
            Return GetDocType(DocTypeId, WithCache).Name
        End SyncLock
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
    Public Shared Function GetDocTypeNames() As ArrayList
        Return DocTypesFactory.GetDocTypeNames
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
    Public Shared Function GetDocTypeNamesAndIds() As DataTable
        Return DocTypesFactory.GetDocTypeNamesAndIds()
    End Function

#Region "Add"
    Public Shared Function AddDocType(ByVal DocType As DocType) As Integer
        Return DocTypesFactory.AddDocType(DocType)
    End Function
#End Region

#Region "Update"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Actualiza en la BD un Objeto Doc_type
    ''' </summary>
    ''' <param name="DocType"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	22/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub UpdateDocType(ByVal DocType As DocType)
        DocTypesFactory.UpdateDocType(DocType)
        'Grabo el Log
        UserBusiness.Rights.SaveAction(DocType.ID, ObjectTypes.DocTypes, Zamba.Core.RightsType.Edit, DocType.Name)
    End Sub
#End Region

    Public Shared Function DocTypeIsDuplicated(ByVal DocTypeName As String) As Boolean
        Return DocTypesFactory.DocTypeIsDuplicated(DocTypeName)
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve la cantidad de valores que tiene cargado un indice  
    ''' </summary>
    ''' <param name="Doctypeid">ID de la entidad</param>
    ''' <param name="Indexid">ID del Index</param>
    ''' <returns>Cantidad</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/06/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function IndexCountInDocuments(ByVal Doctypeid As Int64, ByVal Indexid As Int64) As Int64
        Try
            Return DocTypesFactory.IndexCountInDocuments(Doctypeid, Indexid)
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
    Public Shared Function DocumentsCount(ByVal DocTypeId As Int64) As Int32
        Return DocTypesFactory.DocumentsCount(DocTypeId)
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
    Public Shared Function ConvertDatasettoArraylist(ByVal Ds As DataSet, ByVal ColumnId As Int32) As ArrayList
        Return DocTypesFactory.ConvertDatasettoArraylist(Ds, ColumnId)
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Crea un Entidad en base a otro ya existente.
    ''' </summary>
    ''' <param name="DocIDOrigen">ID de la entidad original</param>
    ''' <param name="DocNameDestino">Nombre del nuevo entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Lo genera en el mismo volumen que el original
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function CopyDoc(ByVal DocIDOrigen As Int64, ByVal DocNameDestino As String) As Integer
        Return DocTypesFactory.CopyDoc(DocIDOrigen, DocNameDestino)
    End Function

    'Public Shared Function GetIndexSchema(ByVal docTypeId as Int64) As DataSet
    '    Return DocTypesFactory.GetIndexSchema(docTypeId)
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
    Public Shared Function GetDocTypeIdByName(ByVal DocTypeName As String) As Int32
        Return DocTypesFactory.GetDocTypeIdByName(DocTypeName)
    End Function

#Region "LinkIndexs"
    Public Shared Function IndexIsLinked(ByVal docTypeId As Int64, ByVal indexid As Int32) As Boolean
        Return DocTypesFactory.IndexIsLinked(doctypeid, indexid)
    End Function

  
    Public Shared Sub UpdateSomeRowsCascade(ByVal doctypeIdParent As Int32, ByVal indexParentId As Int32, ByVal Value As Object, ByVal whereindexId As Int32, ByVal wherevalue As Object)
        DocTypesFactory.UpdateSomeRowsCascade(doctypeIdParent, indexParentId, Value, whereindexId, wherevalue)
    End Sub

#End Region

#Region "WF"



    Public Shared Function GetAllWFDocTypesByWFIdOnlyForAdmin(ByVal WfId As Int64) As DataSet
        '    SyncLock (Cache.DocTypesAndIndexs.hsDocTypesWF)
        '        If Not WithCache Then Return DocTypesFactory.GetAllWFDocTypes(WfId)
        '        If Not Cache.DocTypesAndIndexs.hsDocTypesWF.ContainsKey(WfId) Then
        '            Cache.DocTypesAndIndexs.hsDocTypesWF.Add(WfId, DocTypesFactory.GetAllWFDocTypes(WfId))
        '        End If
        '        Return DirectCast(Cache.DocTypesAndIndexs.hsDocTypesWF.Item(WfId), DataSet)
        '    End SyncLock
        Return DocTypesFactory.GetAllWFDocTypes(WfId)
    End Function

    Public Shared Function GetDocTypeWfIds(ByVal docTypeId As Int64, Optional ByVal WithCache As Boolean = True) As ArrayList
        SyncLock (Cache.DocTypesAndIndexs.hsWFDocTypes)
            If Not WithCache Then Return DocTypesFactory.GetDocTypeWfIds(DocTypeId)
            If Not Cache.DocTypesAndIndexs.hsWFDocTypes.ContainsKey(DocTypeId) Then
                Cache.DocTypesAndIndexs.hsWFDocTypes.Add(DocTypeId, DocTypesFactory.GetDocTypeWfIds(DocTypeId))
            End If
            Return DirectCast(Cache.DocTypesAndIndexs.hsWFDocTypes.Item(DocTypeId), ArrayList)
        End SyncLock
    End Function

    Public Shared Function GetDocTypeWorkFlowByWfId(ByVal WfId As Int32) As DataSet
        Return DocTypesFactory.GetDocTypeWorkFlowByWfId(WfId)
    End Function

    Public Shared Sub AsignDocType2Wf(ByVal WFID As Int64, ByVal docTypeId As Int64)
        DocTypesFactory.AsignDocType2Wf(WFID, DocTypeId)
    End Sub
    Public Shared Sub RemoveDocTypefromWf(ByVal WFID As Int32, ByVal docTypeId As Int64)
        DocTypesFactory.RemoveDocTypefromWf(WFID, DocTypeId)
    End Sub
    Public Shared Sub RemoveAllAsociationsByDT(ByVal docTypeId As Int64)
        DocTypesFactory.RemoveAllAsociationsByDT(DocTypeId)
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
    Public Shared Sub CheckDockTypeTables()
        DocTypesFactory.CheckDockTypeTables()
    End Sub

#Region "Eventos"
    Public Shared Event CheckTable(ByVal str As String)
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
    Public Overloads Shared Function GetAutoName(ByVal AutoNameCode As String, ByVal DocTypeName As String, ByVal CreateDate As Date, ByVal EditDate As Date, ByVal Indexs As List(Of IIndex)) As String
        Try
            Dim PreAutoNameText As String = String.Empty
            'Dim str As String = String.Empty
            PreAutoNameText &= AutoNameCode.Trim()
            PreAutoNameText = PreAutoNameText.Replace("@DT@", DocTypeName)
            PreAutoNameText = PreAutoNameText.Replace("@CD@", CreateDate.ToString())
            PreAutoNameText = PreAutoNameText.Replace("@ED@", EditDate.ToString())

            Dim i As Integer
            Dim IndexIdLstCount As Int32 = Indexs.Count
            For i = 0 To IndexIdLstCount - 1
                Dim strData As String = String.Empty
                Dim CurrentIndex As Index = Nothing

                CurrentIndex = DirectCast(Indexs(i), Index)
                If CurrentIndex.DropDown = IndexAdditionalType.LineText Then
                    If Not IsNothing(CurrentIndex.DataTemp) AndAlso Not IsDBNull(CurrentIndex.DataTemp) AndAlso Not String.IsNullOrEmpty(CurrentIndex.DataTemp.Trim()) Then
                        strData = Trim(CurrentIndex.DataTemp)
                    Else
                        strData = Trim(CurrentIndex.Data)
                    End If
                Else
                    If String.IsNullOrEmpty(CurrentIndex.dataDescriptionTemp) Then
                        If String.IsNullOrEmpty(CurrentIndex.dataDescription) Then
                            If CurrentIndex.DropDown = IndexAdditionalType.AutoSustitución Then
                                strData = AutoSubstitutionBusiness.getDescription(CurrentIndex.Data, CurrentIndex.ID, False)
                                If String.IsNullOrEmpty(strData) Then
                                    strData = CurrentIndex.Data.Trim()
                                End If
                            Else
                                strData = CurrentIndex.Data.Trim()
                            End If
                        Else
                            strData = CurrentIndex.dataDescription.Trim()
                        End If
                    Else
                        strData = CurrentIndex.dataDescriptionTemp.Trim()
                    End If
                End If
                PreAutoNameText = PreAutoNameText.Replace("@I" & CurrentIndex.ID.ToString() & "@", strData)
            Next
            If PreAutoNameText.Trim = String.Empty Then Return DocTypeName
            Return PreAutoNameText.Trim()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(New Exception("Error al aplicar el autonombre", ex))
            Return DocTypeName
        End Try
    End Function
    Public Shared Function GetAutoNameCode(ByVal AutoNameText As String, ByVal IndexTable As DataTable) As String
        Try
            Return DocTypesFactory.GetAutoNameCode(AutoNameText, IndexTable)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return AutoNameText
        End Try
    End Function
    Public Shared Function GetAutoNameText(ByVal AutoNameCode As String, ByVal IndexTable As DataTable) As String
        Try
            Return DocTypesFactory.GetAutoNameText(AutoNameCode, IndexTable)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return AutoNameCode
        End Try
    End Function

#End Region
    ''' <summary>
    '''  Obtiene todos los atributos de los documentos
    ''' </summary>
    ''' <returns>Dataset</returns>
    ''' <remarks></remarks>
    'Public Shared Function GetAllIndexs() As DataSet
    '    Return DocTypesFactory.GetAllIndexs
    'End Function
    ''' <summary>
    ''' Cambia el orden de un indice asociado en la base de datos
    ''' </summary>
    ''' <param name="docTypeID">El ID de la entidad al que pertenecen los atributos</param>
    ''' <param name="selectedIndexID">El id del indice seleccionado </param>
    ''' <param name="selectedIndexOrder">El numero de orden del indice seleccionado</param>
    ''' <param name="modifiedIndexId">El id del indice que se va a modificar</param>
    ''' <param name="modifiedIndexOrder">El orden del indice que se va a modificar</param>
    ''' <remarks></remarks>
    Public Shared Sub ChangeIndexOrder(ByVal docTypeID As Integer, ByVal selectedIndexID As Integer, ByVal selectedIndexOrder As Integer, ByVal modifiedIndexId As Integer, ByVal modifiedIndexOrder As Integer)
        DocTypesFactory.ChangeIndexOrder(docTypeID, selectedIndexID, selectedIndexOrder, modifiedIndexId, modifiedIndexOrder)
    End Sub

    'Public Shared Function GetIndex(ByVal docTypeID As Integer, ByVal indexID As Integer) As DataTable
    '    Return DocTypesFactory.GetIndex(docTypeID, indexID)
    'End Function
    'Public Shared Function GetIndexDropDownType(ByVal indexID As Integer) As Integer
    '    Return Indexs_Factory.GetIndexDropDownType(indexID)
    'End Function

    'Public Shared Function GetIndexs(ByVal DocType As DocType) As DataSet
    '    Return DocTypesFactory.GetIndexs(DocType)
    'End Function




    'Public Shared Function GetIndexsByDocTypeIdAsDataset(ByVal DocTypeId As Int64) As DataSet
    '    Return DocTypesFactory.GetIndexsByDocTypeIdAsDataset(DocTypeId)
    'End Function
    ''' <summary>
    '''  Obtiene los atributos de un entidad
    ''' </summary>
    ''' <param name="DocTypeId">Id de entidad</param>
    ''' <returns>Dataset</returns>
    ''' <remarks></remarks>
    'Public Shared Function getIndexByDocTypeId(ByVal docTypeId as Int64) As DataSet
    '    Return DocTypesFactory.getIndexByDocTypeId(DocTypeId)
    'End Function



    'Public Shared Function getIndexsByDocTypeId(ByVal doctypeid As Long) As ArrayList
    '    Return DocTypesFactory.getIndexsByDocTypeId(doctypeid)
    'End Function

    'Dado un ID de DocType, devuelve los atributos asociados, pero como
    'una Lista Genérica de Index. [Alejandro]
    'Lo Comente por que El nombre de la funcion no corresponde con la funcionalidad
    ' creo una igual pero con la funcionalidad correjida [Diego]
    'Public Shared Function GetIndexsByDocTypeIdAsIndexList(ByVal _doctypeid As Long) As Generic.List(Of Index)

    '    'Obtengo la mayoría de los datos de los atributos asociados a ese doctype
    '    '(la mayoría de los datos menos el DropDown
    '    Dim ds As DataSet = DocTypesFactory.getIndexByDocTypeId(_doctypeid)

    '    'Creo una lista Genérica de Index
    '    Dim _index As New Generic.List(Of Index)

    '    'Creo las variables necesarias para inicializar
    '    'un objeto de tipo indice
    '    Dim _Idropdown As IndexAdditionalType
    '    Dim _Iid As Int32
    '    Dim _Itype As IndexDataType
    '    Dim _Iname As String
    '    Dim _Ilen As Int32


    '    'Por cada r que simboliza un Index:
    '    For Each r As DataRow In ds.Tables(0).Rows

    '        'Inicializo los valores base de los que se
    '        'compone el Atributo:
    '        _Iid = CInt(r("Index_Id"))
    '        _Iname = r("Index_Name")
    '        _Itype = DirectCast(CInt(r("Index_Type")), IndexDataType)
    '        _Ilen = CInt(r("Index_Len"))
    '        _Idropdown = DirectCast(Core.DocTypesBusiness.GetIndexDropDownType(_Iid), IndexAdditionalType)

    '        'Inicializo un nuevo Index:
    '        Dim _i As New Index(_Iid, _Iname, _Itype, _Ilen, _Idropdown)

    '        'Y lo sumo a la colección que se retorna
    '        _index.Add(_i)

    '        'Cuestiones de memoria
    '        _i = Nothing

    '    Next

    '    'Retorno la coleccion de Atributos.
    '    Return _index

    'End Function


    ''' <summary>
    ''' Si bien la Lista Genérica de Index es más cómodo de usar, había 
    ''' métodos ya realizados que pedían un ArrayList. [Alejandro]
    ''' </summary>
    ''' <param name="_doctypeid">Id de la entidad</param>
    ''' <history>Marcelo Modified 03/02/2009</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    'Public Shared Function GetIndexsListByDocTypeId(ByVal _doctypeid As Long) As Generic.List(Of Index)
    '    Try
    '        'Obtengo la mayoría de los datos de los atributos asociados a ese doctype
    '        '(la mayoría de los datos menos el DropDown
    '        Dim ds As DataSet = DocTypesFactory.getIndexByDocTypeId(_doctypeid)

    '        'Creo una lista Genérica de Index
    '        Dim _index As New Generic.List(Of Index)

    '        'Creo las variables necesarias para inicializar
    '        'un objeto de tipo indice
    '        Dim IndexTemp As Index

    '        'Por cada r que simboliza un Index:
    '        For Each r As DataRow In ds.Tables(0).Rows
    '            IndexTemp = New Index
    '            'Inicializo los valores base de los que se
    '            'compone el Atributo:
    '            IndexTemp.ID = CInt(r("Index_Id"))
    '            IndexTemp.Name = r("Index_Name").ToString
    '            IndexTemp.Type = DirectCast(CInt(r("Index_Type")), IndexDataType)
    '            IndexTemp.Len = CInt(r("Index_Len"))
    '            IndexTemp.DropDown = DirectCast(CInt(r("DropDown")), IndexAdditionalType)
    '            'Y lo sumo a la colección que se retorna
    '            _index.Add(IndexTemp)
    '        Next

    '        'Retorno la coleccion de Atributos.
    '        Return _index
    '    Catch ex As Exception
    '        ZClass.raiseerror(ex)
    '        Return Nothing
    '    End Try
    'End Function

    ''' <summary>
    ''' Devuelve los atributos como un arraylist
    ''' </summary>
    ''' <param name="_doctypeid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    'Public Shared Function GetIndexsByDocTypeIdAsArrayList(ByVal _doctypeid As Long) As ArrayList

    '    'Obtengo la mayoría de los datos de los atributos asociados a ese DocType(la mayoría salvo el DropDown)
    '    Dim ds As DataSet = DocTypesFactory.getIndexByDocTypeId(_doctypeid)

    '    'Creo un ArrayList (que será el que retorne el método)
    '    Dim _index As New ArrayList(ds.Tables(0).Rows.Count)

    '    'Creo las variables que conforman el tipo Index
    '    Dim _Idropdown As IndexAdditionalType
    '    Dim _Iid As Int32
    '    Dim _Itype As IndexDataType
    '    Dim _Iname As String
    '    Dim _Ilen As Int32


    '    'Por cada r que simboliza un Index en ds:
    '    For Each r As DataRow In ds.Tables(0).Rows
    '        Dim _i As Index
    '        _Iid = CInt(r("Index_Id"))
    '        If indexs.Contains(_Iid) Then
    '            _i = indexs(_Iid)
    '        Else
    '            _Iname = r("Index_Name")
    '            _Itype = DirectCast(CInt(r("Index_Type")), IndexDataType)
    '            _Ilen = CInt(r("Index_Len"))
    '            _Idropdown = CInt(r("DropDown")) 'DirectCast(Core.DocTypesBusiness.GetIndexDropDownType(_Iid), IndexAdditionalType)

    '            'Creo un nuevo Atributo
    '            _i = New Index(_Iname, _Iid, _Itype, _Ilen, _Idropdown)

    '            'Guardo el atributo en el hashtable que contiene todos
    '            indexs.Add(_Iid, _i)
    '        End If
    '        'y lo cargo en el Array que va a ser devuelto
    '        _index.Add(_i)
    '    Next

    '    Return _index
    'End Function

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
    Public Shared Sub DeleteTables(ByVal DocTypeID As Int64)
        DocTypesFactory.DeleteTables(DocTypeID)
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
    Public Shared Sub DeleteRights(ByVal doctypeID As Int64)
        DocTypesFactory.DeleteRights(doctypeID)
    End Sub
    Public Shared Sub RemoveIndex(ByVal DoctypeID As Int64, ByVal IndexId As Int64)
        DocTypesFactory.RemoveIndex(DoctypeID, IndexId)
    End Sub
    Public Shared Sub Removecolumn(ByVal doctypeID As Int64, ByVal indexidarray As ArrayList)
        DocTypesFactory.Removecolumn(doctypeID, indexidarray)
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Establece un indice como obligatorio para un doc_type
    ''' </summary>
    ''' <param name="DocTypeId">Id de la entidad</param>
    ''' <param name="indexid">Atributo que se desea establecer como obligatorio</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub SetRequiredIndex(ByVal docTypeId As Int64, ByVal indexid As Int32)
        DocTypesFactory.SetRequiredIndex(DocTypeId, indexid)
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Agrega un Atributo a un Entidad. Modifica la tabla DOC_I agregandole a la misma una columna
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
    Public Shared Sub AddColumn(ByVal doctype As DocType, ByVal IndexIdArray As ArrayList, ByVal IndexTypeArray As ArrayList, ByVal IndexLenArray As ArrayList)
        DocTypesFactory.AddColumn(doctype, IndexIdArray, IndexTypeArray, IndexLenArray)
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Establece la relación entre el DOC_TYPE y los Atributos
    ''' </summary>
    ''' <param name="indexid">ID del indice que se desea asignar</param>
    ''' <param name="doctypeid">Entidad al que se le desea asignar un Atributo</param>
    ''' <param name="order">Orden en el que apareceran listados los atributos.</param>
    ''' <param name="Required">Establece si el atributo es obligatorio</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub adddoctyperelationindex(ByVal indexid As Int32, ByVal doctypeid As Int64, ByVal order As Int32, ByVal Required As Boolean, ByVal ISREFERENCED As Boolean)
        DocTypesFactory.adddoctyperelationindex(indexid, doctypeid, order, Required, ISREFERENCED)
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Crea la vista DOCX
    ''' </summary>
    ''' <param name="DocType"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Marcelo]	17/12/2008	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub CreateView(ByVal DocTypeId As Int64)
        IndexReferences.updateView(DocTypeId)
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Crea la vista DOCX
    ''' </summary>
    ''' <param name="DocType"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub CreateView(ByVal DocType As DocType)
        IndexReferences.updateView(DocType.ID)
    End Sub
    Public Shared Sub AddColumnTextindex(ByVal DocTypeId As Int64, ByVal IndexId As Int32)
        DocTypesFactory.AddColumnTextindex(DocTypeId, IndexId)
    End Sub
    Public Shared Function GetDocTypesbyUserRightsOfView(ByVal UserId As Int64, ByVal RightType As Zamba.Core.RightsType) As ArrayList
        SyncLock (Cache.DocTypesAndIndexs.dicDocTypeByRight)
            If Not Cache.DocTypesAndIndexs.dicDocTypeByRight.ContainsKey(RightType) Then
                Dim DocTypes As ArrayList = DocTypesFactory.GetDocTypesbyUserRightsOfView(UserId, RightType)
                Cache.DocTypesAndIndexs.dicDocTypeByRight.Add(RightType, DocTypes)
                Return DocTypes
            End If
            Return Cache.DocTypesAndIndexs.dicDocTypeByRight.Item(RightType)
        End SyncLock
    End Function


#Region "Permanent Collections"
#End Region

    Public Shared Function GetDocTypesIdsAndNames() As DataSet
        Return DocTypesFactory.GetDocTypesIdsAndNames
    End Function

    Public Shared Function GetDocTypesIdsAndNamesSorted() As DataSet
        Dim dstemp As DataSet = DocTypesFactory.GetDocTypesIdsAndNames
        Dim dv As DataView = dstemp.Tables(0).DefaultView
        dv.Sort = "Doc_Type_Name"

        Dim ds As DataSet = New DataSet()
        ds.Tables.Add(dv.ToTable)
        Return ds

    End Function

    Public Shared Function GetAllDocTypes() As DataSet
        Return DocTypesFactory.GetAllDocTypes
    End Function

    Public Shared Function GetAllDocType() As DataTable
        Return DocTypesFactory.GetAllDocType()
    End Function

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
    Public Shared Function GetIndexsProperties(ByVal doctypeid As Int64, ByVal withcache As Boolean) As DataSet
        SyncLock (Cache.DocTypesAndIndexs.hsIndexsProperties)
            If Not withcache Then Return DocTypesFactory.GetIndexsProperties(doctypeid)
            If Not Cache.DocTypesAndIndexs.hsIndexsProperties.ContainsKey(doctypeid) Then
                Cache.DocTypesAndIndexs.hsIndexsProperties.Add(doctypeid, DocTypesFactory.GetIndexsProperties(doctypeid))
            End If
            Return DirectCast(Cache.DocTypesAndIndexs.hsIndexsProperties.Item(doctypeid), DataSet)
        End SyncLock
    End Function

    ''' <summary>
    '''  Obtiene los atributos de un entidad
    ''' </summary>
    ''' <param name="DocTypeId">Id de entidad</param>
    ''' <history> Marcelo Modified 03/02/2009
    '''           Marcelo Modified 05/02/2009</history>
    ''' <returns>Dataset</returns>
    ''' <remarks></remarks>
    Public Shared Function getAsignedIndexAndNonAsignedByDocTypeId(ByVal DocTypeId As Int64) As DataSet
        Return DocTypesFactory.getAsignedIndexAndNonAsignedByDocTypeId(DocTypeId)
    End Function


    ''' <summary>
    ''' Obtiene las propiedades de atributos asociados de la tabla index_r_doc_type 
    ''' </summary>
    ''' <param name="doctypeid">Id de entidad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>Diego 18/7/2008 Created</history>
    Public Shared Function GetIndexsProperties(ByVal doctypeid As Int64, ByVal indexid As Int64) As DataSet
        Try
            Return DocTypesFactory.GetIndexsProperties(doctypeid, indexid)
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
    'Public Shared Function GetIndexsPropertiesWithIndexType(ByVal doctypeid As Int64) As DataSet
    '    Try
    '        Return DocTypesFactory.GetIndexsPropertiesWithIndexType(doctypeid)
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
    Public Shared Function GetIndexRestrictions(ByVal doctypeid As Int64, ByVal indexid As Int64) As DataSet
        Try
            Return DocTypesFactory.GetIndexRestrictions(doctypeid, indexid)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function

#Region "AutoSetupHelper"

    ''' <summary>
    ''' Agrego un nuevo entidad
    ''' </summary>
    '''<history>
    ''' 	[Marcelo]	22/05/2008	Modified
    '''     [Gaston]    04/05/2009  Modified    Cada vez que se crea un nuevo entidad entonces se crea una nueva instancia de Core.DocType
    '''</history>
    ''' <remarks></remarks>
    Public Function AddVirtualDocType(ByVal NewDocTypeName As String) As Int64

        Dim newDocType As New Core.DocType

        If (NewDocTypeName.Trim = "") Then
            Exit Function
        End If

        If (NewDocTypeName.Trim.Length > 70) Then
            Exit Function
        Else

            Try
                If (DocTypesBusiness.DocTypeIsDuplicated(NewDocTypeName) = True) Then
                    Exit Function
                End If
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
                Exit Function
            End Try

            newDocType.Name = NewDocTypeName.Trim()

        End If

        newDocType.AutoNameCode = "@DT@"
        newDocType.ObjecttypeId = 2
        newDocType.IconId = 0
        newDocType.DiskGroupId = 0

        Try
            newDocType.ID = Int64.Parse(Trim(DocTypesBusiness.AddDocType(newDocType).ToString()))
            Return newDocType.ID
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return 0
        Finally
            If (Not IsNothing(newDocType)) Then
                newDocType.Dispose()
                newDocType = Nothing
            End If
        End Try
    End Function

    Public Function AsignIndex(ByVal DocTypeName As String, ByVal IndexId As Int64) As Int64

        Try
            Dim DocType As IDocType = DocTypesBusiness.GetDocType(DocTypeName, False)
            DocTypesBusiness.adddoctyperelationindex(IndexId, DocType.ID, 1, False, False)
            '--
            'agrego las columnas a la tabla de documentos
            Dim IndexIdArray As New ArrayList
            Dim IndexTypeArray As New ArrayList
            Dim IndexLenArray As New ArrayList

            Dim Index As IIndex

            IndexIdArray.Add(IndexId)
            IndexTypeArray.Add(Index.Type)
            IndexLenArray.Add(Index.Len)

            DocTypesBusiness.AddColumn(DocType, IndexIdArray, IndexTypeArray, IndexLenArray)

            DocTypesBusiness.CreateView(DocType)

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Function

#End Region


    Public Class IndexReferences

        ''' <summary>
        ''' Método que llama al método que permite guardar en la base de datos los correspondientes valores asociados a un Atributo referenciado
        ''' </summary>
        ''' <param name="docTypeId">Id de un entidad</param>
        ''' <param name="indexId">Id de un Atributo</param>
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
        Public Shared Function SaveIndexReference(ByRef docTypeId As Int64, ByRef indexId As Int64, ByRef server As String, ByRef dataBase As String, _
                                          ByRef user As String, ByRef table As String, ByRef column As String, _
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
        Public Shared Function IsIndexReferencedWithDocType(ByVal IndexId As Int64, ByVal DocTypeId As Int64) As Boolean
            Return ZIndexReferenceDAC.IsIndexReferencedWithDocType(IndexId, DocTypeId)
        End Function
        ''' <summary>
        ''' Actualiza la vista
        ''' </summary>
        ''' <param name="lstIndexReference">Listado con las referencias a ser agregadas</param>
        ''' <remarks></remarks>
        Public Shared Sub updateView(ByVal DocTypeId As Int64)
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
