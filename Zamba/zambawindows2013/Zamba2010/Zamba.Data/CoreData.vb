Imports Zamba.Core
''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Data
''' Class	 : Data.CoreData
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Funciones comunes para Zamba
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public NotInheritable Class CoreData
    Inherits ZClass
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un nuevo ID
    ''' </summary>
    ''' <param name="IdType">Tipo de objeto para el cual se requiere un nuevo ID</param>
    ''' <returns>Integer</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetNewID(ByVal IdType As Zamba.Core.IdTypes) As Integer
        Dim Id As Int32
        Dim DSTEMP As New DataSet
        Dim con As IConnection = Server.Con(False)
        Try

TryAgain:

            Dim result As Object = con.ExecuteScalar(CommandType.Text, "SELECT OBJECTID FROM OBJLASTID WHERE OBJECT_TYPE_ID = " & Int64.Parse(IdType))
            If result Is Nothing OrElse IsDBNull(result) OrElse Int64.TryParse(result, Id) = False Then
                con.ExecuteNonQuery(CommandType.Text, "Insert into Objlastid(Object_type_id,objectid) values (" & Int64.Parse(IdType) & ", 1) ")
                Id = 1
            Else
                Dim affectedrows As Integer = con.ExecuteNonQuery(CommandType.Text, "UPDATE OBJLASTID SET OBJECTID = OBJECTID + 1 WHERE OBJECT_TYPE_ID = " & Int64.Parse(IdType) & " and OBJECTID = " & Id)
                If affectedrows = 1 Then
                    Id = Id + 1
                Else
                    GoTo TryAgain
                End If
            End If


            Dim MaxId As Int64
            Select Case IdType
                Case IdTypes.RulePreference
                    MaxId = Int64.Parse(con.ExecuteScalar(CommandType.Text, "SELECT MAX(ID) + 1 FROM ZRULEOPTBASE").ToString())
            End Select

            If Id < MaxId Then
                con.ExecuteNonQuery(CommandType.Text, String.Format("UPDATE OBJLASTID SET OBJECTID = {0} WHERE OBJECT_TYPE_ID = " & IdType, MaxId))
                Id = MaxId
            End If

            Return Id

        Catch ex As IndexOutOfRangeException
            raiseerror(ex)
            Try
                con.ExecuteNonQuery(CommandType.Text, "insert into objlastid(OBJECT_TYPE_ID,OBJECTID) values(" & (IdType) & ",2)")
            Catch exe As Exception
                raiseerror(exe)
                Throw exe
            End Try
            Return 1
        Catch ex As Exception
            raiseerror(ex)
            Throw ex
        Finally
            con.Close()
            con.dispose()
            con = Nothing
        End Try
    End Function

    Public Shared Sub SetNewID(ByVal IdType As Zamba.Core.IdTypes, NewId As Int64)
        Dim con As IConnection = Server.Con(False)
        Try
            Dim Id As Int64
            Dim result As Object = con.ExecuteScalar(CommandType.Text, "SELECT OBJECTID FROM OBJLASTID WHERE OBJECT_TYPE_ID = " & Int64.Parse(IdType))
            If result Is Nothing OrElse IsDBNull(result) OrElse Int64.TryParse(result, Id) = False Then
                con.ExecuteNonQuery(CommandType.Text, "Insert into Objlastid(Object_type_id,objectid) values (" & Int64.Parse(IdType) & "," & NewId & ") ")
            Else
                Dim affectedrows As Integer = con.ExecuteNonQuery(CommandType.Text, "UPDATE OBJLASTID SET OBJECTID = " & NewId & " WHERE OBJECT_TYPE_ID = " & Int64.Parse(IdType))
            End If

        Catch ex As Exception
            raiseerror(ex)
            Throw ex
        Finally
            con.Close()
            con.dispose()
            con = Nothing
        End Try
    End Sub
    Public Shared dIdsCache As New Stack()
    Public Shared hIdsCache As New Stack()

    Public Shared Function GetNewID(ByVal IdType As Zamba.Core.IdTypes, CacheSize As Int64) As Integer
        SyncLock dIdsCache
            Dim Id As Int32
            Dim DSTEMP As New DataSet

            Try

                If IdType = IdTypes.DOCID Then
                    If dIdsCache.Count = 0 Then

                        Id = GetNewIdFromDataBase(IdType, CacheSize, Id)
                        Dim i As Int64
                        For i = Id + CacheSize To Id Step -1
                            dIdsCache.Push(i)
                        Next
                    End If

                    Return dIdsCache.Pop

                ElseIf IdType = IdTypes.USERHSTID Then

                    SyncLock hIdsCache
                        If hIdsCache.Count = 0 Then

                            Id = GetNewIdFromDataBase(IdType, CacheSize, Id)
                            Dim i As Int64
                            For i = Id + CacheSize To Id Step -1
                                hIdsCache.Push(i)
                            Next
                        End If

                        Return hIdsCache.Pop
                    End SyncLock
                End If


            Catch ex As IndexOutOfRangeException
                raiseerror(ex)
                Try
                    Server.Con.ExecuteNonQuery(CommandType.Text, "insert into objlastid(OBJECT_TYPE_ID,OBJECTID) values(" & (IdType) & ",2)")
                Catch exe As Exception
                    raiseerror(exe)
                    Throw exe
                End Try
                Return 1
            Catch ex As Exception
                raiseerror(ex)
                Throw ex
            End Try
        End SyncLock
    End Function

    Private Shared Function GetNewIdFromDataBase(IdType As IdTypes, CacheSize As Long, Id As Integer) As Integer
        If CacheSize = 0 Then CacheSize = 1
TryAgain:
        Dim result As Object = Server.Con.ExecuteScalar(CommandType.Text, "SELECT OBJECTID FROM OBJLASTID WHERE OBJECT_TYPE_ID = " & Int64.Parse(IdType))
        If result Is Nothing OrElse IsDBNull(result) OrElse Int64.TryParse(result, Id) = False Then
            Server.Con.ExecuteNonQuery(CommandType.Text, "Insert into Objlastid(Object_type_id,objectid) values (" & Int64.Parse(IdType) & ", " & CacheSize & ") ")
            Id = 1
        Else
            Dim affectedrows As Integer = Server.Con.ExecuteNonQuery(CommandType.Text, "UPDATE OBJLASTID SET OBJECTID = OBJECTID + " & CacheSize & " WHERE OBJECT_TYPE_ID = " & Int64.Parse(IdType) & " and OBJECTID = " & Id)
            If affectedrows = 1 Then
                Id = Id + 1
            Else
                GoTo TryAgain
            End If
        End If


        Dim MaxId As Int64
        Select Case IdType
            Case IdTypes.RulePreference
                MaxId = Int64.Parse(Server.Con.ExecuteScalar(CommandType.Text, "SELECT MAX(ID) + 1 FROM ZRULEOPTBASE").ToString())
        End Select

        If Id < MaxId Then
            Server.Con.ExecuteNonQuery(CommandType.Text, String.Format("UPDATE OBJLASTID SET OBJECTID = {0} WHERE OBJECT_TYPE_ID = " & IdType, MaxId))
            Id = MaxId
        End If

        Return Id
    End Function

    Public Overrides Sub Dispose()

    End Sub

#Region "Globals Factory"
    ''' <summary>
    ''' Obtiene todas las variables globales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetGlobalVariables() As DataSet
        If Server.isOracle Then
            Dim strquery As String = "SELECT id,name as Nombre,value as Valor,environment as Entorno FROM ZVariables"
            Return Server.Con.ExecuteDataset(CommandType.Text, strquery)
        Else
            Dim parameters() As Object = {}
            Return Server.Con.ExecuteDataset("zsp_100_var_GetVariables", parameters)
        End If
    End Function


    ''' <summary>
    ''' Obtiene todas las variables del entorno
    ''' </summary>
    ''' <param name="environment"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetGlobalVariablesByEnvironment(ByVal environment As String) As DataSet
        Dim strquery As String = "SELECT id,name,value FROM ZVariables where environment='" & environment & "'"
        Return Server.Con.ExecuteDataset(CommandType.Text, strquery)
    End Function


    ''' <summary>
    ''' Actualiza el valor de una variable global
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="name"></param>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Public Shared Sub UpdateGlobalVariable(ByVal id As String, ByVal name As String, ByVal value As String)
        Dim strquery As String = "UPDATE ZVariables SET " + name + " = '" + value + "' WHERE ID = " + id
        Server.Con.ExecuteScalar(CommandType.Text, strquery)
    End Sub

    ''' <summary>
    ''' Elimina una variable global
    ''' </summary>
    ''' <param name="id"></param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteGlobalVariable(ByVal id As String)
        If Server.isOracle Then
            Dim strquery As String = "DELETE FROM ZVariables WHERE id = " + id
            Server.Con.ExecuteScalar(CommandType.Text, strquery)
        Else
            Dim parameters() As Object = {id}
            Server.Con.ExecuteDataset("zsp_100_var_DeleteVariable", parameters)
        End If
    End Sub

    ''' <summary>
    ''' Inserta una nueva variable global
    ''' </summary>
    ''' <param name="name"></param>
    ''' <param name="value"></param>
    ''' <param name="environment"></param>
    ''' <param name="id"></param>
    ''' <remarks></remarks>
    Public Shared Sub InsertGlobalVariable(ByVal name As String, ByVal value As String, ByVal environment As String, ByVal id As Int64)
        If Server.isOracle Then
            Dim strquery As String = "INSERT INTO ZVariables (id,name,value,environment) VALUES (" & id & ",'" & name & "','" & value + "','" & environment & "')"
            Server.Con.ExecuteScalar(CommandType.Text, strquery)
        Else
            Dim parameters() As Object = {id, name, value, environment}
            Server.Con.ExecuteDataset("zsp_100_var_InsertVariable", parameters)
        End If
    End Sub
#End Region
End Class
