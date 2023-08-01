Imports Zamba.Servers
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
    Public Shared Function GetNewID(ByVal IdType As Zamba.Core.IdTypes) As Int64
        Dim Id As Int64
        Dim DSTEMP As New DataSet
        Dim con As IConnection = Server.Con()
        Try

            If Server.isOracle Then
                Dim parValues() As Object = {DirectCast(IdType, Int32), 2}
                DSTEMP = con.ExecuteDataset("zsp_objects_100.GetAndSetLastId", parValues)
                Id = Int64.Parse(DSTEMP.Tables(0).Rows(0).Item(0).ToString)
            Else
                Dim parameters() As Object = {(IdType)}
                Id = Int64.Parse(con.ExecuteScalar("zsp_objects_100_GetAndSetLastId", parameters).ToString)
            End If
            ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Nuevo Id {0} para IDType: {1}", Id, IdType.ToString()))
            Return Id

        Catch ex As IndexOutOfRangeException
            'Zamba.Core.ZClass.raiseerror(ex)
            ZTrace.WriteLineIf(ZTrace.IsError, "Error al obtener Id por Store, se intenta por consulta: " & ex.Message)
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
                        MaxId = Int64.Parse(con.ExecuteScalar(CommandType.Text, "SELECT MAX(ID) + 1 FROM ZRULEOPTBASE " & If(Zamba.Servers.Server.isSQLServer, " WITH(NOLOCK) ", "")).ToString())
                End Select

                If Id < MaxId Then
                    con.ExecuteNonQuery(CommandType.Text, String.Format("UPDATE OBJLASTID SET OBJECTID = {0} WHERE OBJECT_TYPE_ID = " & IdType, MaxId))
                    Id = MaxId
                End If

                Return Id

            Catch ex2 As Exception
                Zamba.Core.ZClass.raiseerror(ex)
                Try
                    con.ExecuteNonQuery(CommandType.Text, "insert into objlastid(OBJECT_TYPE_ID,OBJECTID) values(" & (IdType) & ",2)")
                Catch exe As Exception
                    Zamba.Core.ZClass.raiseerror(exe)
                    Return 1
                End Try
                Return 1
            End Try

        Finally
            con.Close()
            con.dispose()
            con = Nothing
        End Try
    End Function
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
    Public Shared Function GetNewID(ByVal IdType As Zamba.Core.IdTypes, ByRef t As Transaction) As Int64
        Dim Id As Int64
        Dim DSTEMP As New DataSet

        Try
            If Server.isOracle Then

                Dim parValues() As Object = {DirectCast(IdType, Integer), 2}
                Try
                    DSTEMP = t.Con.ExecuteDataset(t.Transaction, "zsp_objects_100.GetAndSetLastId", parValues)
                    Id = Int64.Parse(DSTEMP.Tables(0).Rows(0).Item(0).ToString())
                Catch ex As Exception
                    System.Threading.Thread.Sleep(500)
                    DSTEMP = t.Con.ExecuteDataset(t.Transaction, "zsp_objects_100.GetAndSetLastId", parValues)
                    If DSTEMP.Tables(0).Rows.Count > 0 Then
                        Id = Int64.Parse(DSTEMP.Tables(0).Rows(0).Item(0).ToString())
                    Else
                        Id = 1
                    End If
                End Try
                'End If
            Else
                Dim parameters() As Object = {(IdType)}
                'result = CInt(Server.Con.ExecuteScalar("GetandSetLastID", parameters)) 
                Try
                    Id = CInt(t.Con.ExecuteScalar(t.Transaction, "zsp_objects_100_GetAndSetLastId", parameters))
                Catch ex As Exception
                    System.Threading.Thread.Sleep(5000)
                    Id = CInt(t.Con.ExecuteScalar(t.Transaction, "zsp_objects_100_GetAndSetLastId", parameters))
                End Try
            End If
            Return Id




        Catch ex As Exception
            'Zamba.Core.ZClass.raiseerror(ex)
            ZTrace.WriteLineIf(ZTrace.IsError, "Error al obtener Id por Store, se intenta por consulta: " & ex.Message)
            Try

TryAgain:

                Dim result As Object = t.Con.ExecuteScalar(CommandType.Text, "SELECT OBJECTID FROM OBJLASTID WHERE OBJECT_TYPE_ID = " & Int64.Parse(IdType))
                If result Is Nothing OrElse IsDBNull(result) OrElse Int64.TryParse(result, Id) = False Then
                    t.Con.ExecuteNonQuery(CommandType.Text, "Insert into Objlastid(Object_type_id,objectid) values (" & Int64.Parse(IdType) & ", 1) ")
                    Id = 1
                Else
                    Dim affectedrows As Integer = t.Con.ExecuteNonQuery(CommandType.Text, "UPDATE OBJLASTID SET OBJECTID = OBJECTID + 1 WHERE OBJECT_TYPE_ID = " & Int64.Parse(IdType) & " and OBJECTID = " & Id)
                    If affectedrows = 1 Then
                        Id = Id + 1
                    Else
                        GoTo TryAgain
                    End If
                End If


                Dim MaxId As Int64
                Select Case IdType
                    Case IdTypes.RulePreference
                        MaxId = Int64.Parse(t.Con.ExecuteScalar(CommandType.Text, "SELECT MAX(ID) + 1 FROM ZRULEOPTBASE " & If(Zamba.Servers.Server.isSQLServer, " WITH(NOLOCK) ", "")).ToString())
                End Select

                If Id < MaxId Then
                    t.Con.ExecuteNonQuery(CommandType.Text, String.Format("UPDATE OBJLASTID SET OBJECTID = {0} WHERE OBJECT_TYPE_ID = " & IdType, MaxId))
                    Id = MaxId
                End If

                Return Id
            Catch ex2 As Exception
                Zamba.Core.ZClass.raiseerror(ex)
                Try
                    t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, "insert into objlastid(OBJECT_TYPE_ID,OBJECTID) values(" & (IdType) & ",2)")
                Catch exe As Exception
                    Zamba.Core.ZClass.raiseerror(exe)
                    Return 1
                End Try
                Return 1
            End Try




        End Try
    End Function
    Public Shared Function GetZsiteMap() As DataSet
        Dim queryString As String = "SELECT [ID], [Title], [URL], [ParentID] FROM [ZSiteMap]"
        Return Zamba.Servers.Server.Con.ExecuteDataset(CommandType.Text, queryString)
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
            Return Zamba.Servers.Server.Con.ExecuteDataset("zsp_100_var_GetVariables", parameters)
        End If
    End Function


    ''' <summary>
    ''' Obtiene todas las variables del entorno
    ''' </summary>
    ''' <param name="environment"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetGlobalVariablesByEnvironment(ByVal environment As String) As DataSet
        If Server.isOracle Then
            Dim strquery As String = "SELECT id,name,value FROM ZVariables where environment='" & environment & "'"
            Return Server.Con.ExecuteDataset(CommandType.Text, strquery)
        Else
            Dim parameters() As Object = {environment}
            Return Zamba.Servers.Server.Con.ExecuteDataset("zsp_100_var_GetEnvVariables", parameters)
        End If
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
            Zamba.Servers.Server.Con.ExecuteDataset("zsp_100_var_DeleteVariable", parameters)
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
            Zamba.Servers.Server.Con.ExecuteDataset("zsp_100_var_InsertVariable", parameters)
        End If
    End Sub
#End Region


End Class
