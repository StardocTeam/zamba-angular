Imports Zamba.Servers
Imports System.Data.SqlClient
Imports Zamba.Core

Public Class ResultFactoryExt
    ''' <summary>
    ''' Inserta un registro en la DOC_B
    ''' </summary>
    ''' <param name="newRes"></param>
    ''' <param name="t"></param>
    ''' <remarks>Se utiliza para migrar documentos existentes en servidor a la base de datos</remarks>
    Public Sub InsertResIntoDOCB(ByRef Res As IResult, Optional ByVal isZipped As Boolean = False)

        'Ezequiel: Se comenta funcionalidad ZIP
        Dim zipped As Integer = 0

        If isZipped Then zipped = 1

        Dim query As String = "INSERT INTO DOC_B" & Res.DocTypeId.ToString & " VALUES(" & Res.ID.ToString & ", @docFile , " & zipped & ")"

        If Server.isOracle Then
            Throw New NotImplementedException()
        Else
            Dim pDocFile As SqlParameter
            Dim sql_version As String = Server.Con.ExecuteScalar(CommandType.Text, "select @@version")

            If sql_version.StartsWith("Microsoft SQL Server  2000") Then
                pDocFile = New SqlParameter("@docFile", SqlDbType.Image)
            Else
                pDocFile = New SqlParameter("@docFile", SqlDbType.VarBinary)
            End If

            pDocFile.Value = Res.EncodedFile
            Dim params As IDbDataParameter() = {pDocFile}

            Server.Con.ExecuteNonQuery(CommandType.Text, query, params)
        End If
    End Sub

    ''' <summary>
    ''' Obtiene todos los docID del documento que no se encuentran en la Doc_b
    ''' </summary>
    ''' <param name="DocTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetNotInBlobDocuments(ByVal DocTypeId As Integer) As DataSet
        Dim TableDoc As String = "Doc_t" & DocTypeId

        Dim StrSelect As String
        StrSelect = "Select doc_id from Doc_t" & DocTypeId & " where doc_id not in (select doc_id from doc_b" & DocTypeId & ")"

        Return Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
    End Function

    ''' <summary>
    ''' Obtiene el count de la tabla ZSer por doc id
    ''' </summary>
    ''' <param name="docId"></param>
    ''' <param name="docTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetEnccryptionCount(ByVal docId As Long, ByVal docTypeId As Long) As Long
        Dim query As String = "select count(1) from ZSer where docId = " & docId & " and doctypeid = " & docTypeId

        Return Server.Con.ExecuteScalar(CommandType.Text, query)
    End Function

    ''' <summary>
    ''' Obtiene la password de encriptacion/descriptacion guardada en la tabla ZSer
    ''' </summary>
    ''' <param name="docId"></param>
    ''' <param name="docTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFilePassword(ByVal docId As Long, ByVal docTypeId As Long) As String
        Dim query As String = "select pss from ZSer where docId = " & docId & " and doctypeid = " & docTypeId

        Return Server.Con.ExecuteScalar(CommandType.Text, query)
    End Function

    Public Shared Function GetBinaryDoc(ByVal docTypeId As Long, ByVal docId As Long) As Byte()
        Dim sql As String = "SELECT DOCFILE FROM DOC_B" & docTypeId.ToString & " WHERE DOC_ID = " & docId.ToString
        Return DirectCast(Server.Con.ExecuteScalarForMigrator(CommandType.Text, sql), Byte())
    End Function
End Class
