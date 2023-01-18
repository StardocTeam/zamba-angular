Imports Zamba.Data

Public Class DocTypeBusinessExt
    ''' <summary>
    ''' Obtiene el ID de las entidades que utilizan el volumen
    ''' </summary>
    ''' <param name="DiskGroupID"></param>
    ''' <history>Marcelo Created 14/12/12</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAllDocTypes() As List(Of IDocType)

        Dim docTypes As DataTable = DocTypeFactoryExt.GetAllDocTypes()
        Dim listOfDocTypes As List(Of IDocType) = New List(Of IDocType)
        For Each r As DataRow In docTypes.Rows
            Dim docType As DocType = New DocType
            docType.ID = r("Doc_Type_ID")
            docType.Name = r("Doc_type_Name")
            docType.FileFormatId = r("File_format_id")
            docType.DiskGroupId = r("Disk_group_id")
            docType.Thumbnails = r("Thumbnails")
            docType.IconId = r("ICON_ID")
            docType.ObjecttypeId = r("OBJECT_TYPE_ID")
            docType.AutoNameText = r("AUTONAME")
            docType.DocCount = r("DOCCOUNT")
            docType.DocumentalId = r("DOCUMENTALID")

            listOfDocTypes.Add(docType)
        Next

        Return listOfDocTypes
    End Function

    Public Shared Function GetDocTypeByID(ByVal docTypeId As Int64) As DocType
        Dim dt As DataTable = DocTypeFactoryExt.GetDocTypeByID(docTypeId)
        If Not dt Is Nothing Then
            Dim r As DataRow = DocTypeFactoryExt.GetDocTypeByID(docTypeId).Rows(0)



            Dim docType As DocType = New DocType
            docType.ID = r("Doc_Type_ID")
            docType.Name = r("Doc_type_Name")
            docType.FileFormatId = r("File_format_id")
            docType.DiskGroupId = r("Disk_group_id")
            docType.Thumbnails = r("Thumbnails")
            docType.IconId = r("ICON_ID")
            docType.ObjecttypeId = r("OBJECT_TYPE_ID")
            docType.AutoNameText = r("AUTONAME")
            docType.DocCount = r("DOCCOUNT")
            docType.DocumentalId = r("DOCUMENTALID")

            Return docType
        End If
        Return Nothing
    End Function

    Public Shared Function GetDocTypesIdsByVolumeID(ByVal VolumeID As Int64) As List(Of Int64)
        Dim docTypesIds As New List(Of Int64)
        Dim EF As New EntityFactory
        Dim ds As DataSet = EF.GetDocTypesIdsByDiskGroupID(VolumeID)

        EF = Nothing

        For Each dr As DataRow In ds.Tables(0).Rows
            docTypesIds.Add(dr(0))
        Next

        Return docTypesIds
    End Function

    Public Shared Function GetDoc_IByDocTypeID(ByVal docTypeId As Int64) As DataTable

        Return DocTypeFactoryExt.GetDoc_IByDocTypeID(docTypeId)

    End Function

    Public Shared Function GetDoc_BByDocTypeID(ByVal docTypeId As Int64) As DataTable

        Return DocTypeFactoryExt.GetDoc_BByDocTypeID(docTypeId)

    End Function

    Public Shared Function GetDoc_DByDocTypeID(ByVal docTypeId As Int64) As DataTable

        Return DocTypeFactoryExt.GetDoc_DByDocTypeID(docTypeId)

    End Function

    Public Shared Function GetDoc_TByDocTypeID(ByVal docTypeId As Int64) As DataTable

        Return DocTypeFactoryExt.GetDoc_TByDocTypeID(docTypeId)

    End Function

    Public Shared Function GetSchemeFromDoc_IByDocTypeID(ByVal docTypeId As Int64) As DataTable

        Return DocTypeFactoryExt.GetSchemeFromDoc_IByDocTypeID(docTypeId)

    End Function

    Public Shared Function GetSchemeFromDoc_BByDocTypeID(ByVal docTypeId As Int64) As DataTable

        Return DocTypeFactoryExt.GetSchemeFromDoc_BByDocTypeID(docTypeId)

    End Function

    Public Shared Function GetSchemeFromDoc_DByDocTypeID(ByVal docTypeId As Int64) As DataTable

        Return DocTypeFactoryExt.GetSchemeFromDoc_DByDocTypeID(docTypeId)

    End Function

    Public Shared Function GetSchemeFromDoc_TByDocTypeID(ByVal docTypeId As Int64) As DataTable

        Return DocTypeFactoryExt.GetSchemeFromDoc_TByDocTypeID(docTypeId)

    End Function

    ''' <summary>
    ''' Obtiene las propiedades de atributos asociados de la tabla index_r_doc_type 
    ''' </summary>
    ''' <param name="doctypeid">Id de entidad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' </history>
    Public Shared Function GetIndexsProperties(ByVal doctypeid As Int64, ByVal withcache As Boolean) As DataSet
        SyncLock (Cache.DocTypesAndIndexs.hsIndexsProperties)
            If Not withcache Then Return DocTypeFactoryExt.GetIndexsProperties(doctypeid)
            If Not Cache.DocTypesAndIndexs.hsIndexsProperties.ContainsKey(doctypeid) Then
                Cache.DocTypesAndIndexs.hsIndexsProperties.Add(doctypeid, DocTypeFactoryExt.GetIndexsProperties(doctypeid))
            End If
            Return DirectCast(Cache.DocTypesAndIndexs.hsIndexsProperties.Item(doctypeid), DataSet)
        End SyncLock
    End Function

    ''' <summary>
    ''' Verifica la existencia de un tipo de documento
    ''' </summary>
    ''' <param name="docTypeId">Id del tipo de documento</param>
    ''' <returns>True en caso de existir</returns>
    ''' <remarks></remarks>
    Public Function CheckDocTypeExistance(ByVal docTypeId As Int64) As Boolean
        Dim docTypeFactoryExt As New DocTypeFactoryExt
        Dim result As Boolean = docTypeFactoryExt.CheckDocTypeExistance(docTypeId)
        docTypeFactoryExt = Nothing
        Return result
    End Function
End Class