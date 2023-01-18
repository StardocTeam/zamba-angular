Imports Zamba.Core
Imports System.Collections.Generic

Public Class ZIndexReferenceDAC

    ''' <summary>
    ''' Método que permite guardar en la base de datos los correspondientes valores asociados a un Atributo referenciado
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
    ''' 	[Gaston]	18/12/2008	Modified    
    ''' </history>
    Public Shared Function SaveIndexReference(ByRef docTypeId As Int64, ByRef indexId As Int64, ByRef server As String, ByRef dataBase As String, _
                                          ByRef user As String, ByRef table As String, ByRef column As String, _
                                          ByRef ZIndexReference As Generic.List(Of ZIndexReference)) As Boolean

        Dim indexReferenceId As Integer = CoreData.GetNewID(IdTypes.IndexReferences)

        ' Primera tabla: ZIndexReference
        Dim strSelect As String = "INSERT INTO ZIndexReference (DTid, IId, IServer, IDataBase, IUser, ITable, IColumn, ReferenceId) VALUES (" _
                                  & docTypeId & ", " & indexId & ", '" & server & "', '" & dataBase & "', '" & user & "', '" & table & "', '" _
                                  & column & "', " & indexReferenceId & ")"
        Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strSelect)

        ' Por cada relación se guarda en:
        ' Segunda tabla: ZIndexReferenceKeys
        For Each R As ZIndexReference In ZIndexReference

            strSelect = "INSERT INTO ZIndexReferenceKeys (ReferenceId, IndexId, IColumn) VALUES (" _
                                       & indexReferenceId & ", " & R.IndexId & ", '" & R.Column & "')"
            Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strSelect)

        Next

    End Function

    Public Shared Function SaveIndexReference(ByVal ZIndexReference As Core.ZIndexReference) As Boolean
        Dim strSelect As String = "Insertct * from ZIndexReference"
        Server.Con.ExecuteNonQuery(CommandType.Text, strSelect)
    End Function

    ''' <summary>
    ''' Crea la vista a partir de los valores
    ''' </summary>
    ''' <param name="docTypeId"></param>
    ''' <remarks></remarks>
    Public Shared Sub UpdateView(ByVal docTypeId As Int64, ByVal lstColKeys As Dictionary(Of String(), String), ByVal lstColIndexs As Dictionary(Of String, String), ByVal lstColSelects As Dictionary(Of String, String()))
        'SE COMENTO PARA PODER COMPILAR EL PROYECTO [SEBASTIAN 15/01/2009]
        'Martin: NUNCA Comentar lineas sin saber que hacen por compilacion, sonsultar antes.
        Server.CreateTables.CreateView(docTypeId, lstColKeys, lstColIndexs, lstColSelects)
    End Sub

    ''' <summary>
    ''' Obtiene todos los atributos a poner en el select por el id de la entidad
    ''' </summary>
    ''' <param name="ZIndexReference"></param>
    ''' <history>Marcelo 17/12/2008 Created</history>
    ''' <param name="docTypeId">Id de la entidad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetIndexReference(ByVal docTypeId As Int64) As DataSet
        Dim strSelect As String = "select * from zindexreference  where dtId=" & docTypeId
        Return Server.Con.ExecuteDataset(CommandType.Text, strSelect)
    End Function
    ''' <summary>
    ''' [Sebastian 21-04-09] Este metodo devuelve el doc type y el index id de la tabla zindexreference
    ''' </summary>
    ''' <param name="IndexId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAllIndexsReferencedWithDocType(ByVal IndexId As Int64) As DataSet
        Dim query As New System.Text.StringBuilder
        Dim ds As New DataSet
        ZTrace.WriteLineIf(ZTrace.IsInfo, "-------------------")
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Dentro de GetAllIndexsReferencedWithDocType")
        Try

            query.Append("SELECT dtid, iid FROM zindexreference WHERE iid=" & IndexId)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando " & query.ToString)
            ds = Server.Con.ExecuteDataset(CommandType.Text, query.ToString)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "La consulta se ejecuto con exito")

            If IsDBNull(ds) = False Then
                Return ds
            Else
                Return Nothing
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            ds.Dispose()
            ds = Nothing
        End Try
    End Function



    ''' <summary>
    ''' [Sebastian 21-04-09] Este metodo devuelve el doc type y el index id de la tabla zindexreference
    ''' </summary>
    ''' <param name="IndexId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IsIndexReferencedWithDocType(ByVal IndexId As Int64, ByVal DocTypeId As Int64) As Boolean

        Dim query As New System.Text.StringBuilder

        ZTrace.WriteLineIf(ZTrace.IsInfo, "-------------------")
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Dentro de IsIndexReferencedWithDocType")


        query.Append("SELECT count(1) FROM zindexreference WHERE iid=" & IndexId & " and dtid = " & DocTypeId)

        Dim count As Object

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando " & query.ToString)
        count = Server.Con.ExecuteScalar(CommandType.Text, query.ToString)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "La consulta se ejecuto con exito")

        If IsDBNull(count) = False AndAlso count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Obtiene todos los atributos a poner en el select por el id de la entidad
    ''' </summary>
    ''' <param name="ZIndexReference"></param>
    ''' <history>Marcelo 17/12/2008 Created</history>
    ''' <param name="docTypeId">Id de la entidad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetIndexKeyReference(ByVal docTypeId As Int64) As DataSet
        Dim strSelect As String = "select zIndexReferenceKeys.* from zIndexReferenceKeys inner join zindexreference on zindexreference.referenceid= zIndexReferenceKeys.referenceid where dtId=" & docTypeId
        Return Server.Con.ExecuteDataset(CommandType.Text, strSelect)
    End Function

End Class