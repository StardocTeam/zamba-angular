Imports System.Collections.Generic
Imports System.Text.RegularExpressions
Imports Zamba.Data
Imports Zamba.Servers


Public Class ServersBusiness

    Public Shared Function GetServerTypes() As List(Of String)
        Return ServersFactory.GetServerTypes()
    End Function

    Public Shared Function ConvertDate(ByVal pDate As String) As String
        Return ServersFactory.ConvertDate(pDate)
    End Function

    Public Shared Function GetServerType() As DBTYPES
        Return DirectCast(ServersFactory.GetServerType, DBTYPES)
    End Function

    Public Shared Function IsConnectionValid(ByVal serverType As DBTYPES, ByVal serverName As String, ByVal dataBase As String, ByVal conUser As String, ByVal conPass As String) As Boolean
        Dim intServerType As Int32 = DirectCast(serverType, Int32)
        Try
            Dim tempInt As Int32 = ServersFactory.IsConnectionValid(serverType, serverName, dataBase, conUser, conPass)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return False
        End Try
        Return True
    End Function

    Public Shared Function IsConnectionValid(ByVal serverType As DBTYPES, ByVal serverName As String, ByVal dataBase As String, ByVal conUser As String, ByVal conPass As String, ByRef errorMsg As String) As Boolean
        Dim intServerType As Int32 = DirectCast(serverType, Int32)
        Try
            Dim tempInt As Int32 = ServersFactory.IsConnectionValid(serverType, serverName, dataBase, conUser, conPass)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            errorMsg = ex.Message
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Devuelve una conexion a la base de datos
    ''' </summary>
    ''' <param name="servertype"></param>
    ''' <param name="dbname"></param>
    ''' <param name="dbpassword"></param>
    ''' <param name="dbuser"></param>
    ''' <param name="servidor"></param>
    ''' <param name="_commandType"></param>
    ''' <param name="commandText"></param>
    ''' <history>Marcelo Created 09/12/08</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetNewConnection(ByVal servertype As DBTYPES, ByVal dbname As String, ByVal dbpassword As String, ByVal dbuser As String, ByVal servidor As String) As IConnection
        Return ServersFactory.GetNewConnection(servertype, dbname, dbpassword, dbuser, servidor)
    End Function

    Public Shared Function BuildExecuteScalar(ByVal _commandType As CommandType, ByVal commandText As String) As Object
        Dim con As IConnection = Nothing
        Try
            con = Server.Con(True)
            Return con.ExecuteScalar(_commandType, commandText)
        Finally
            If Not IsNothing(con) Then
                'con.Close()
                con.dispose()
                con = Nothing
                'GC.Collect()
            End If
        End Try
        'Return ServersFactory.BuildExecuteScalar(_commandType, commandText)
    End Function


    ''' <summary>
    ''' Sobrecarga para ejecutar store de oracle.
    ''' </summary>
    ''' <param name="servertype"></param>
    ''' <param name="dbname"></param>
    ''' <param name="dbpassword"></param>
    ''' <param name="dbuser"></param>
    ''' <param name="servidor"></param>
    ''' <param name="spName"></param>
    ''' <param name="ParametersNames"></param>
    ''' <param name="parameterstypes"></param>
    ''' <param name="parameterValues"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Ezequiel] 29/10/09 - created
    ''' </history>
    Public Shared Function BuildExecuteScalar(ByVal spName As String, ByVal ParametersNames() As Object, ByVal parameterstypes As Object, ByVal parameterValues() As Object) As Object
        Dim con As IConnection = Nothing
        Try
            con = Server.Con(True)
            If ParametersNames Is Nothing Then
                Return con.ExecuteScalar(spName, parameterValues)
            Else
                Return con.ExecuteScalar(spName, ParametersNames, parameterstypes, parameterValues)
            End If
        Finally
            If Not IsNothing(con) Then
                con.dispose()
                con = Nothing
            End If
        End Try
    End Function

    ''' <summary>
    ''' Sobrecarga para ejecutar store de oracle.
    ''' </summary>
    ''' <param name="servertype"></param>
    ''' <param name="dbname"></param>
    ''' <param name="dbpassword"></param>
    ''' <param name="dbuser"></param>
    ''' <param name="servidor"></param>
    ''' <param name="spName"></param>
    ''' <param name="ParametersNames"></param>
    ''' <param name="parameterstypes"></param>
    ''' <param name="parameterValues"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Ezequiel] 29/10/09 - created
    ''' </history>
    Public Shared Function BuildExecuteScalar(ByVal servertype As DBTYPES, ByVal dbname As String, ByVal dbpassword As String, ByVal dbuser As String, ByVal servidor As String, ByVal spName As String, ByVal ParametersNames() As Object, ByVal parameterstypes As Object, ByVal parameterValues() As Object) As Object
        Dim con As IConnection = Nothing
        Try
            con = ServersBusiness.GetNewConnection(servertype, servidor, dbname, dbuser, dbpassword)
            Return con.ExecuteScalar(spName, ParametersNames, parameterstypes, parameterValues)
        Finally
            If Not IsNothing(con) Then
                con.dispose()
                con = Nothing
            End If
        End Try
    End Function


    Public Shared Function BuildExecuteScalar(ByVal servertype As DBTYPES, ByVal dbname As String, ByVal dbpassword As String, ByVal dbuser As String, ByVal servidor As String, ByVal _commandType As CommandType, ByVal commandText As String) As Object
        Dim con As IConnection = Nothing
        Try
            con = ServersBusiness.GetNewConnection(servertype, dbname, dbpassword, dbuser, servidor)
            Return con.ExecuteScalar(_commandType, commandText)
        Finally
            If Not IsNothing(con) Then
                '  con.Close()
                con.dispose()
                con = Nothing
                ' GC.Collect()
            End If
        End Try
        'Return ServersFactory.BuildExecuteScalar(servertype, dbname, dbpassword, dbuser, servidor, _commandType, commandText)
    End Function



    Public Shared Function BuildExecuteDataSet(ByVal _commandType As CommandType, ByVal commandText As String) As DataSet
        Dim con As IConnection = Nothing
        Try
            con = Server.Con(True)
            Return con.ExecuteDataset(_commandType, commandText)
        Finally
            If Not IsNothing(con) Then
                'con.Close()
                con.dispose()
                con = Nothing
                'GC.Collect()
            End If
        End Try
        'Return ServersFactory.BuildExecuteDataSet(_commandType, commandText)
    End Function

    ''' <summary>
    ''' Sobrecarga para ejecutar store de oracle.
    ''' </summary>
    ''' <param name="servertype"></param>
    ''' <param name="dbname"></param>
    ''' <param name="dbpassword"></param>
    ''' <param name="dbuser"></param>
    ''' <param name="servidor"></param>
    ''' <param name="spName"></param>
    ''' <param name="ParametersNames"></param>
    ''' <param name="parameterstypes"></param>
    ''' <param name="parameterValues"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Ezequiel] 29/10/09 - created
    ''' </history>
    Public Shared Function BuildExecuteDataSet(ByVal spName As String, ByVal ParametersNames() As Object, ByVal parameterstypes As Object, ByVal parameterValues() As Object) As DataSet
        Dim con As IConnection = Nothing
        Try
            con = Server.Con(True)
            Dim ds As New DataSet
            If ParametersNames Is Nothing Then
                ds = con.ExecuteDataset(spName, parameterValues)
            Else
                ds = con.ExecuteDataset(spName, ParametersNames, parameterstypes, parameterValues)
            End If

            Return ds

        Finally
            If Not IsNothing(con) Then
                con.dispose()
                con = Nothing
            End If
        End Try
    End Function

    ''' <summary>
    ''' Sobrecarga para ejecutar store de oracle.
    ''' </summary>
    ''' <param name="servertype"></param>
    ''' <param name="dbname"></param>
    ''' <param name="dbpassword"></param>
    ''' <param name="dbuser"></param>
    ''' <param name="servidor"></param>
    ''' <param name="spName"></param>
    ''' <param name="ParametersNames"></param>
    ''' <param name="parameterstypes"></param>
    ''' <param name="parameterValues"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Ezequiel] 29/10/09 - created
    ''' </history>
    Public Shared Function BuildExecuteDataSet(ByVal servertype As DBTYPES, ByVal dbname As String, ByVal dbpassword As String, ByVal dbuser As String, ByVal servidor As String, ByVal spName As String, ByVal ParametersNames() As Object, ByVal parameterstypes As Object, ByVal parameterValues() As Object) As DataSet
        Dim con As IConnection = Nothing
        Try
            con = ServersBusiness.GetNewConnection(servertype, servidor, dbname, dbuser, dbpassword)
            Return con.ExecuteDataset(spName, ParametersNames, parameterstypes, parameterValues)
        Finally
            If Not IsNothing(con) Then
                '  con.Close()
                con.dispose()
                con = Nothing
                ' GC.Collect()
            End If
        End Try
        'Return ServersFactory.BuildExecuteDataSet(servertype, dbname, dbpassword, dbuser, servidor, _commandType, commandText)
    End Function


    Public Shared Function BuildExecuteDataSet(ByVal servertype As DBTYPES, ByVal dbname As String, ByVal dbpassword As String, ByVal dbuser As String, ByVal servidor As String, ByVal _commandType As CommandType, ByVal commandText As String) As DataSet
        Dim con As IConnection = Nothing
        Try
            con = ServersBusiness.GetNewConnection(servertype, dbname, dbpassword, dbuser, servidor)
            Return con.ExecuteDataset(_commandType, commandText)
        Finally
            If Not IsNothing(con) Then
                '  con.Close()
                con.dispose()
                con = Nothing
                ' GC.Collect()
            End If
        End Try
        'Return ServersFactory.BuildExecuteDataSet(servertype, dbname, dbpassword, dbuser, servidor, _commandType, commandText)
    End Function

    Public Shared Sub BuildExecuteNonQuery(ByVal _commandType As CommandType, ByVal commandText As String)
        Dim con As IConnection = Nothing
        Try
            con = Server.Con(True)
            con.ExecuteNonQuery(_commandType, commandText)
        Finally
            If Not IsNothing(con) Then
                ' con.Close()
                con.dispose()
                con = Nothing
                'GC.Collect()
            End If
        End Try
        'ServersFactory.BuildExecuteNonQuery(_commandType, commandText)
    End Sub


    ''' <summary>
    ''' Sobrecarga para ejecutar store de oracle.
    ''' </summary>
    ''' <param name="servertype"></param>
    ''' <param name="dbname"></param>
    ''' <param name="dbpassword"></param>
    ''' <param name="dbuser"></param>
    ''' <param name="servidor"></param>
    ''' <param name="spName"></param>
    ''' <param name="ParametersNames"></param>
    ''' <param name="parameterstypes"></param>
    ''' <param name="parameterValues"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Ezequiel] 29/10/09 - created
    ''' </history>
    Public Shared Sub BuildExecuteNonQuery(ByVal spName As String, ByVal ParametersNames() As Object, ByVal parameterstypes As Object, ByVal parameterValues() As Object)
        Dim con As IConnection = Nothing
        Try
            con = Server.Con(True)
            If ParametersNames Is Nothing Then
                con.ExecuteNonQuery(spName, parameterValues)
            Else
                con.ExecuteNonQuery(spName, ParametersNames, parameterstypes, parameterValues)
            End If
        Finally
            If Not IsNothing(con) Then
                con.dispose()
                con = Nothing
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Sobrecarga para ejecutar store de oracle.
    ''' </summary>
    ''' <param name="servertype"></param>
    ''' <param name="dbname"></param>
    ''' <param name="dbpassword"></param>
    ''' <param name="dbuser"></param>
    ''' <param name="servidor"></param>
    ''' <param name="spName"></param>
    ''' <param name="ParametersNames"></param>
    ''' <param name="parameterstypes"></param>
    ''' <param name="parameterValues"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Ezequiel] 29/10/09 - created
    ''' </history>
    Public Shared Sub BuildExecuteNonQuery(ByVal servertype As DBTYPES, ByVal dbname As String, ByVal dbpassword As String, ByVal dbuser As String, ByVal servidor As String, ByVal spName As String, ByVal ParametersNames() As Object, ByVal parameterstypes As Object, ByVal parameterValues() As Object)
        Dim con As IConnection = Nothing
        Try
            con = ServersBusiness.GetNewConnection(servertype, dbname, dbpassword, dbuser, servidor)
            con.ExecuteNonQuery(spName, ParametersNames, parameterstypes, parameterValues)
        Finally
            If Not IsNothing(con) Then
                con.dispose()
                con = Nothing
            End If
        End Try
    End Sub

    Public Shared Sub BuildExecuteNonQuery(ByVal servertype As DBTYPES, ByVal dbname As String, ByVal dbpassword As String, ByVal dbuser As String, ByVal servidor As String, ByVal _commandType As CommandType, ByVal commandText As String)
        Dim con As IConnection = Nothing
        Try
            con = ServersBusiness.GetNewConnection(servertype, dbname, dbpassword, dbuser, servidor)
            con.ExecuteNonQuery(_commandType, commandText)
        Finally
            If Not IsNothing(con) Then
                'con.Close()
                con.dispose()
                con = Nothing
                ' GC.Collect()
            End If
        End Try
        'ServersFactory.BuildExecuteNonQuery(servertype, dbname, dbpassword, dbuser, servidor, _commandType, commandText)
    End Sub


    ''' <summary>
    ''' Metodo el cual genera los parametros para invocar un store en oracle.
    ''' </summary>
    ''' <param name="sql">Consulta sql</param>
    ''' <param name="hsparvalues">Hash table en el cual se le pasan valores en el caso de que se deseen completar con algun valor por defecto o filtro</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''        
    ''' </history>
    Public Shared Function GetStoreParams(ByVal sql As String, Optional ByVal hsparvalues As Hashtable = Nothing) As List(Of Object)
        Dim storeparams As New List(Of Object)
        '[Ezequiel] - Guardo el nombre del store
        Dim spname As String = sql.Split(New String() {" "}, StringSplitOptions.RemoveEmptyEntries)(1)
        '[Ezequiel] - Guardo los parametros.
        Dim param As String = sql.Replace(sql.Split(New String() {" "}, StringSplitOptions.RemoveEmptyEntries)(0) + " " + sql.Split(New String() {" "}, StringSplitOptions.RemoveEmptyEntries)(1), "")
        '[Ezequiel] - Creo los arrays donde se van a guardar los parametros y valores
        Dim parNames As System.Collections.ArrayList = New System.Collections.ArrayList()
        Dim parValues As System.Collections.ArrayList = New System.Collections.ArrayList()
        Dim split As Boolean
        Dim pos As Int32
        Dim par As String
        '[Ezequiel] - Recorro la lista de parametros para cargar los datos en los arraylist
        While (param <> String.Empty)
            split = False
            pos = 0
            par = String.Empty
            '[Ezequiel] - Bucle que separa el nombre del parametro del valor
            While Not split
                '[Ezequiel] - Obtengo la posicion de la primera coma y la guardo en pos,
                ' en el caso de que pos sea diferente de 0 es porque el valor tenia dentro una coma
                ' entonces en la proxima iteracion a esa posicion le sumo 1 asi toma la segunda coma
                pos = param.IndexOf(",", IIf(pos = 0, pos, pos + 1))
                '[Ezequiel] - Si la posicion es -1 es porque era el ultimo parametro.
                If pos = -1 Then
                    par = param
                Else
                    par = param.Substring(0, pos)
                End If
                '[Ezequiel] - Si el parametro termina con comilla simple es porque esta completo
                ' caso contrario habia una coma dentro del valor y sigue la iteracion
                If par.Trim().EndsWith("'") Then
                    '[Ezequiel] - Guardo el nombre del parametro
                    Dim parname As String = par.Split(New String() {" "}, StringSplitOptions.RemoveEmptyEntries)(0)
                    parNames.Add(parname.Trim())
                    '[Ezequiel] - Guardo el valor del parametro

                    If Not hsparvalues Is Nothing AndAlso hsparvalues.ContainsKey(parname.Trim()) Then
                        parValues.Add(hsparvalues.Item(parname.Trim).Replace("'", ""))
                    Else
                        parValues.Add(par.Substring(par.IndexOf(parname) + parname.Length).Trim().Replace("'", ""))
                    End If


                    '[Ezequiel] - Si la longitud del parametro es igual a la lista de parametros
                    ' es porque ya no hay mas nada por recorrer
                    If param.Length = par.Length Then
                        param = ""
                    Else
                        param = param.Substring(par.Length + 1)
                    End If
                    '[Ezequiel] - Pongo la bandera en true para que recorra el siguiente parametro.
                    split = True
                End If
            End While

        End While

        '[Ezequiel] - Genero el array para los tipos de parametros
        Dim parTypes(parNames.ToArray().Length - 1) As String
        Dim count As Int32 = parNames.ToArray().Length - 1
        For i As Integer = 0 To count
            parNames(i) = parNames(i).ToString().Trim()
            parValues(i) = parValues(i).ToString().Trim()
            If parNames(i).ToString().ToLower().CompareTo("io_cursor") = 0 Then
                parTypes(i) = "2"
            Else
                parTypes(i) = "13"
            End If
        Next
        storeparams.Add(spname)
        storeparams.Add(parNames.ToArray())
        storeparams.Add(parTypes)
        storeparams.Add(parValues.ToArray())
        Return storeparams
    End Function

    ' Vendria solo nombre del store y los valores
    Public Shared Function GetStoreParamsValues(ByVal sql As String) As List(Of Object)

        Dim withStoreNames As Boolean = storeHasParamsNames(sql)
        If withStoreNames Then
            Return GetStoreParams(sql, Nothing)
        End If

        Dim storeparams As New List(Of Object)
        '[Ezequiel] - Guardo el nombre del store
        Dim spname As String = sql.Split(New String() {" "}, StringSplitOptions.RemoveEmptyEntries)(1)
        '[Ezequiel] - Guardo los parametros.
        Dim param As String = sql.Replace(sql.Split(New String() {" "}, StringSplitOptions.RemoveEmptyEntries)(0) + " " + sql.Split(New String() {" "}, StringSplitOptions.RemoveEmptyEntries)(1), "").Trim()

        If param.StartsWith("(") AndAlso param.EndsWith(")") Then
            param = param.Remove(0, 1)
            param = param.Remove(param.Length - 1)
        End If

        '[Ezequiel] - Creo los arrays donde se van a guardar los parametros y valores
        Dim parValues As ArrayList = New ArrayList()

        Dim params As String() = SplitCSV(param)


        For Each value As String In params
            If String.IsNullOrEmpty(value) OrElse value.ToLower().Equals("null") Then
                parValues.Add(DBNull.Value)
            Else
                parValues.Add(value.Trim())
            End If
        Next

        storeparams.Add(spname) 'Nombre del store
        storeparams.Add(Nothing) 'Nombre de los parametros
        storeparams.Add(Nothing) 'Tipo de los parametros
        storeparams.Add(parValues.ToArray()) 'Valores

        Return storeparams

    End Function
    Public Shared Function SplitCSV(ByVal input As String) As String()
        Dim csvSplit As Regex = New Regex("(?:^|,)(""(?:[^""]+|"""")*""|[^,]*)", RegexOptions.Compiled)
        Dim list As List(Of String) = New List(Of String)()
        Dim curr As String = Nothing

        For Each match As Match In csvSplit.Matches(input)
            curr = match.Value

            If 0 = curr.Length Then
                list.Add("")
            End If

            list.Add(curr.TrimStart(","))
        Next

        Return list.ToArray()
    End Function

    Private Shared Function storeHasParamsNames(sql As String) As Boolean
        Dim params As String = sql.Replace(sql.Split(New String() {" "}, StringSplitOptions.RemoveEmptyEntries)(0) + " " + sql.Split(New String() {" "}, StringSplitOptions.RemoveEmptyEntries)(1), "").Trim()

        For Each param As String In params.Split(",")
            If param.Trim.Split(" ").Count > 1 AndAlso param.Split(" ")(1).StartsWith("'") AndAlso param.Split(" ")(1).EndsWith("'") Then
                Return True
        End If
        Next
        Return False
    End Function

End Class
