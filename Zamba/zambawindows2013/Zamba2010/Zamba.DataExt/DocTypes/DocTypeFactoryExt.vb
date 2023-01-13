Imports Zamba.Servers
Imports System.Text

Public Class DocTypeFactoryExt

    ''' <summary>
    ''' Devuelve todos los DocTypes (Entidades)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAllDocTypes() As DataTable
        Dim StrSelect As String = "select * from doc_type"
        Return Server.Con.ExecuteDataset(CommandType.Text, StrSelect).Tables(0)
    End Function

    Public Shared Function GetDoc_IByDocTypeID(ByVal docTypeId As Int64) As DataTable
        Try
            Dim StrSelect As String = "select * from doc_I" + docTypeId.ToString
            Return Server.Con.ExecuteDataset(CommandType.Text, StrSelect).Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Shared Function GetDoc_TByDocTypeID(ByVal docTypeId As Int64) As DataTable
        Try
            Dim StrSelect As String = "select * from doc_T" + docTypeId.ToString
            Return Server.Con.ExecuteDataset(CommandType.Text, StrSelect).Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Shared Function GetDoc_BByDocTypeID(ByVal docTypeId As Int64) As DataTable
        Try
            Dim StrSelect As String = "select * from doc_B" + docTypeId.ToString
            Return Server.Con.ExecuteDataset(CommandType.Text, StrSelect).Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Shared Function GetDoc_DByDocTypeID(ByVal docTypeId As Int64) As DataTable
        Try
            Dim StrSelect As String = "select * from doc_D" + docTypeId.ToString
            Return Server.Con.ExecuteDataset(CommandType.Text, StrSelect).Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try


    End Function

    Public Shared Function GetDocTypeByID(ByVal docTypeId As Int64) As DataTable
        Try
            Dim StrSelect As String = "select * from doc_type where doc_type_id = " + docTypeId.ToString
            Return Server.Con.ExecuteDataset(CommandType.Text, StrSelect).Tables(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    'scheme
    Public Shared Function GetSchemeFromDoc_IByDocTypeID(ByVal docTypeId As Int64) As DataTable
        Try
            Dim StrSelect As String = "EXEC sp_help 'Doc_I" + docTypeId.ToString + "'"

            Return Server.Con.ExecuteDataset(CommandType.Text, StrSelect).Tables(1)
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Shared Function GetSchemeFromDoc_TByDocTypeID(ByVal docTypeId As Int64) As DataTable
        Try
            Dim StrSelect As String = "EXEC sp_help 'Doc_T" + docTypeId.ToString + "'"
            Return Server.Con.ExecuteDataset(CommandType.Text, StrSelect).Tables(1)
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Shared Function GetSchemeFromDoc_BByDocTypeID(ByVal docTypeId As Int64) As DataTable
        Try
            Dim StrSelect As String = "EXEC sp_help 'Doc_B" + docTypeId.ToString + "'"
            Return Server.Con.ExecuteDataset(CommandType.Text, StrSelect).Tables(1)
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Shared Function GetSchemeFromDoc_DByDocTypeID(ByVal docTypeId As Int64) As DataTable
        Try
            Dim StrSelect As String = "EXEC sp_help 'Doc_D" + docTypeId.ToString + "'"
            Return Server.Con.ExecuteDataset(CommandType.Text, StrSelect).Tables(1)
        Catch ex As Exception
            Return Nothing
        End Try


    End Function

    ''' <summary>
    ''' Obtiene las propiedades de atributos asociados de la tabla index_r_doc_type 
    ''' </summary>
    ''' <param name="doctypeid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' </history>
    Public Shared Function GetIndexsProperties(ByVal doctypeid As Int64) As DataSet
        Try
            Dim query As New StringBuilder
            query.Append("SELECT DOC_INDEX.INDEX_ID, DOC_INDEX.INDEX_NAME, ORDEN, MUSTCOMPLETE, ")
            query.Append(" LoadLotus, ShowLotus, Complete, IndexSearch, DefaultValue, IsDataUnique, AutoComplete as Autoincremental, ISREFERENCED, minValue, maxValue ")
            query.Append(" FROM index_r_doc_type ")
            query.Append(" inner join DOC_INDEX ON DOC_INDEX.INDEX_ID = index_r_doc_type.index_id ")
            query.Append(" where doc_type_id = ")
            query.Append(doctypeid)
            query.Append(" ORDER BY index_r_doc_type.ORDEN")
            Return Server.Con.ExecuteDataset(CommandType.Text, query.ToString)

        Catch ex As Exception
            Dim query As New StringBuilder
            query.Append("SELECT DOC_INDEX.INDEX_ID, DOC_INDEX.INDEX_NAME, ORDEN, MUSTCOMPLETE, ")
            query.Append(" LoadLotus, ShowLotus, Complete, IndexSearch, DefaultValue, IsDataUnique, AutoComplete as Autoincremental, ISREFERENCED ")
            query.Append(" FROM index_r_doc_type ")
            query.Append(" inner join DOC_INDEX ON DOC_INDEX.INDEX_ID = index_r_doc_type.index_id ")
            query.Append(" where doc_type_id = ")
            query.Append(doctypeid)
            query.Append(" ORDER BY index_r_doc_type.ORDEN")
            Return Server.Con.ExecuteDataset(CommandType.Text, query.ToString)

        End Try
    End Function

    ''' <summary>
    ''' Verifica la existencia de un tipo de documento
    ''' </summary>
    ''' <param name="docTypeId">Id del tipo de documento</param>
    ''' <returns>True en caso de existir</returns>
    ''' <remarks></remarks>
    Public Function CheckDocTypeExistance(ByVal docTypeId As Int64) As Boolean
        Dim rows As Int32 = Server.Con.ExecuteScalar("ZSP_DOCTYPE_100_CheckDocTypeExistance", New Object() {docTypeId})
        If rows = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Shared Function CheckColumn(docTypeId As Long, indexId As Long, indexType As Integer, indexLen As Integer) As Boolean
        Try
            Server.Con.ExecuteScalar(CommandType.Text, "select i" & indexId & " from doc_i" & docTypeId & " where 1=2")
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
