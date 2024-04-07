Imports System.Data.OracleClient
' OracleHelperParameterCache provides functions to leverage a static cache of procedure parameters, and the
' ability to discover parameters for stored procedures at run-time.
Public NotInheritable Class OracleHelperParameterCache

#Region "private methods, variables, and constructors"


    'Since this class provides only static methods, make the default constructor private to prevent 
    'instances from being created with "new OracleHelperParameterCache()".
    Private Sub New()
    End Sub 'New 

    Private Shared paramCache As Hashtable = Hashtable.Synchronized(New Hashtable())

    ' resolve at run time the appropriate set of SqlParameters for a stored procedure
    ' Parameters:
    ' - connectionString - a valid connection string for a OracleConnection
    ' - spName - the name of the stored procedure
    ' - includeReturnValueParameter - whether or not to include their return value parameter>
    ' Returns: OracleParameter()
    Private Shared Function DiscoverSpParameterSet(ByVal co As IDbConnection, ByVal connectionString As String, _
                                                   ByVal spName As String, _
                                                   ByVal ParamArray parameterValues() As Object) As OracleParameter()

        Dim cmd As OracleCommand = New OracleCommand(spName, co)
        Dim discoveredParameters() As OracleParameter


        If co.State = ConnectionState.Closed Then co.Open()
        cmd.CommandType = CommandType.StoredProcedure
        OracleCommandBuilder.DeriveParameters(cmd)

        discoveredParameters = New OracleParameter(cmd.Parameters.Count - 1) {}
        cmd.Parameters.CopyTo(discoveredParameters, 0)


        Return discoveredParameters

    End Function

    ' resolve at run time the appropriate set of SqlParameters for a stored procedure
    ' Parameters:
    ' - connectionString - a valid connection string for a OracleConnection
    ' - spName - the name of the stored procedure
    ' - includeReturnValueParameter - whether or not to include their return value parameter>
    ' Returns: OracleParameter()
    Private Shared Function DiscoverSpParameterSet(ByRef t As IDbTransaction, ByVal connectionString As String, _
                                                   ByVal spName As String, _
                                                   ByVal ParamArray parameterValues() As Object) As OracleParameter()

        '   Dim cn As New OracleConnection(connectionString)
        Dim cmd As OracleCommand = New OracleCommand(spName, t.Connection, t)
        Dim discoveredParameters() As OracleParameter

        Try
            cmd.CommandType = CommandType.StoredProcedure
            OracleCommandBuilder.DeriveParameters(cmd)
            'If Not includeReturnValueParameter Then
            '    cmd.Parameters.RemoveAt(0)
            'End If

            discoveredParameters = New OracleParameter(cmd.Parameters.Count - 1) {}
            cmd.Parameters.CopyTo(discoveredParameters, 0)
        Finally
            cmd.Dispose()
            cmd = Nothing
        End Try

        Return discoveredParameters

    End Function 'DiscoverSpParameterSet

    'deep copy of cached OracleParameter array
    Private Shared Function CloneParameters(ByVal originalParameters() As OracleParameter) As OracleParameter()

        Dim i As Short
        Dim j As Short = originalParameters.Length - 1
        Dim clonedParameters(j) As OracleParameter

        For i = 0 To j
            clonedParameters(i) = CType(CType(originalParameters(i), ICloneable).Clone, OracleParameter)
        Next

        Return clonedParameters
    End Function 'CloneParameters



#End Region

#Region "caching functions"

    ' add parameter array to the cache
    ' Parameters
    ' -connectionString - a valid connection string for a OracleConnection 
    ' -commandText - the stored procedure name or T-SQL command 
    ' -commandParameters - an array of SqlParamters to be cached 
    Public Shared Sub CacheParameterSet(ByVal connectionString As String, _
                                        ByVal commandText As String, _
                                        ByVal ParamArray commandParameters() As OracleParameter)
        Dim hashKey As String = connectionString + ":" + commandText

        paramCache(hashKey) = commandParameters
    End Sub 'CacheParameterSet

    ' retrieve a parameter array from the cache
    ' Parameters:
    ' -connectionString - a valid connection string for a OracleConnection 
    ' -commandText - the stored procedure name or T-SQL command 
    ' Returns: an array of SqlParamters 
    Public Shared Function GetCachedParameterSet(ByVal connectionString As String, ByVal commandText As String) As OracleParameter()
        Dim hashKey As String = connectionString + ":" + commandText
        Dim cachedParameters As OracleParameter() = CType(paramCache(hashKey), OracleParameter())

        If cachedParameters Is Nothing Then
            Return Nothing
        Else
            Return CloneParameters(cachedParameters)
        End If
    End Function 'GetCachedParameterSet

#End Region

#Region "Parameter Discovery Functions"

    ' Retrieves the set of SqlParameters appropriate for the stored procedure
    ' 
    ' This method will query the database for this information, and then store it in a cache for future requests.
    ' Parameters:
    ' -connectionString - a valid connection string for a OracleConnection
    ' -spName - the name of the stored procedure 
    ' -includeReturnValueParameter - a bool value indicating whether the return value parameter should be included in the results 
    ' Returns: an array of SqlParameters 
    Public Overloads Shared Function GetSpParameterSet(ByVal co As IDbConnection, ByVal connectionString As String, _
                                                       ByVal spName As String) As OracleParameter()

        Dim cachedParameters() As OracleParameter
        Dim hashKey As String

        hashKey = connectionString + ":" + spName

        cachedParameters = CType(paramCache(hashKey), OracleParameter())

        If (cachedParameters Is Nothing) Then
            cachedParameters = DiscoverSpParameterSet(co, connectionString, spName)
            paramCache(hashKey) = cachedParameters
        End If

        Return CloneParameters(cachedParameters)

    End Function 'GetSpParameterSet

    ' Retrieves the set of SqlParameters appropriate for the stored procedure
    ' 
    ' This method will query the database for this information, and then store it in a cache for future requests.
    ' Parameters:
    ' -connectionString - a valid connection string for a OracleConnection
    ' -spName - the name of the stored procedure 
    ' -includeReturnValueParameter - a bool value indicating whether the return value parameter should be included in the results 
    ' Returns: an array of SqlParameters 
    Public Overloads Shared Function GetSpParameterSet(ByRef t As IDbTransaction, ByVal connectionString As String, _
                                                       ByVal spName As String) As OracleParameter()

        Dim cachedParameters() As OracleParameter
        Dim hashKey As String

        hashKey = connectionString + ":" + spName

        cachedParameters = CType(paramCache(hashKey), OracleParameter())

        If (cachedParameters Is Nothing) Then
            paramCache(hashKey) = DiscoverSpParameterSet(t, connectionString, spName)
            cachedParameters = CType(paramCache(hashKey), OracleParameter())

        End If

        Return CloneParameters(cachedParameters)

    End Function 'GetSpParameterSet
#End Region

End Class 'OracleHelperParameterCache 