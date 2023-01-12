Imports System.Data.OleDb
' OleDBHelperParameterCache provides functions to leverage a static cache of procedure parameters, and the
' ability to discover parameters for stored procedures at run-time.
Public NotInheritable Class OleDBHelperParameterCache

#Region "private methods, variables, and constructors"


    'Since this class provides only static methods, make the default constructor private to prevent 
    'instances from being created with "new OleDBHelperParameterCache()".
    Private Sub New()
    End Sub 'New 

    Private Shared paramCache As Hashtable = Hashtable.Synchronized(New Hashtable())

    ' resolve at run time the appropriate set of SqlParameters for a stored procedure
    ' Parameters:
    ' - connectionString - a valid connection string for a OleDBConnection
    ' - spName - the name of the stored procedure
    ' - includeReturnValueParameter - whether or not to include their return value parameter>
    ' Returns: oleDBParameter()
    Private Shared Function DiscoverSpParameterSet(ByVal co As IDbConnection, ByVal connectionString As String, _
                                                   ByVal spName As String, _
                                                   ByVal includeReturnValueParameter As Boolean, _
                                                   ByVal ParamArray parameterValues() As Object) As OleDbParameter()

        '   Dim cn As New OleDbConnection(connectionString)
        Dim cmd As OleDbCommand = New OleDbCommand(spName, co)
        Dim discoveredParameters() As OleDbParameter

        Try
            '      If cn.State = ConnectionState.Closed Then cn.Open()
            cmd.CommandType = CommandType.StoredProcedure
            OleDbCommandBuilder.DeriveParameters(cmd)
            If Not includeReturnValueParameter Then
                cmd.Parameters.RemoveAt(0)
            End If

            discoveredParameters = New OleDbParameter(cmd.Parameters.Count - 1) {}
            cmd.Parameters.CopyTo(discoveredParameters, 0)
        Finally
            cmd.Dispose()
            'cn.Dispose()

        End Try

        Return discoveredParameters

    End Function 'DiscoverSpParameterSet

    'deep copy of cached oleDBParameter array
    Private Shared Function CloneParameters(ByVal originalParameters() As OleDbParameter) As OleDbParameter()

        Dim i As Short
        Dim j As Short = originalParameters.Length - 1
        Dim clonedParameters(j) As OleDbParameter

        For i = 0 To j
            clonedParameters(i) = CType(CType(originalParameters(i), ICloneable).Clone, OleDbParameter)
        Next

        Return clonedParameters
    End Function 'CloneParameters



#End Region

#Region "caching functions"

    ' add parameter array to the cache
    ' Parameters
    ' -connectionString - a valid connection string for a OleDBConnection 
    ' -commandText - the stored procedure name or T-SQL command 
    ' -commandParameters - an array of SqlParamters to be cached 
    Public Shared Sub CacheParameterSet(ByVal connectionString As String, _
                                        ByVal commandText As String, _
                                        ByVal ParamArray commandParameters() As OleDbParameter)
        Dim hashKey As String = connectionString + ":" + commandText

        paramCache(hashKey) = commandParameters
    End Sub 'CacheParameterSet

    ' retrieve a parameter array from the cache
    ' Parameters:
    ' -connectionString - a valid connection string for a OleDBConnection 
    ' -commandText - the stored procedure name or T-SQL command 
    ' Returns: an array of SqlParamters 
    Public Shared Function GetCachedParameterSet(ByVal connectionString As String, ByVal commandText As String) As OleDbParameter()
        Dim hashKey As String = connectionString + ":" + commandText
        Dim cachedParameters As OleDbParameter() = CType(paramCache(hashKey), OleDbParameter())

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
    ' -connectionString - a valid connection string for a OleDBConnection 
    ' -spName - the name of the stored procedure 
    ' Returns: an array of SqlParameters
    Public Overloads Shared Function GetSpParameterSet(ByVal co As IDbConnection, ByVal connectionString As String, ByVal spName As String) As OleDbParameter()
        Return GetSpParameterSet(co, connectionString, spName, False)
    End Function 'GetSpParameterSet 

    ' Retrieves the set of SqlParameters appropriate for the stored procedure
    ' 
    ' This method will query the database for this information, and then store it in a cache for future requests.
    ' Parameters:
    ' -connectionString - a valid connection string for a OleDBConnection
    ' -spName - the name of the stored procedure 
    ' -includeReturnValueParameter - a bool value indicating whether the return value parameter should be included in the results 
    ' Returns: an array of SqlParameters 
    Public Overloads Shared Function GetSpParameterSet(ByVal co As IDbConnection, ByVal connectionString As String, _
                                                       ByVal spName As String, _
                                                       ByVal includeReturnValueParameter As Boolean) As OleDbParameter()

        Dim cachedParameters() As OleDbParameter
        Dim hashKey As String

        hashKey = connectionString + ":" + spName + IIf(includeReturnValueParameter = True, ":include ReturnValue Parameter", "")

        cachedParameters = CType(paramCache(hashKey), OleDbParameter())

        If (cachedParameters Is Nothing) Then
            paramCache(hashKey) = DiscoverSpParameterSet(co, connectionString, spName, includeReturnValueParameter)
            cachedParameters = CType(paramCache(hashKey), OleDbParameter())

        End If

        Return CloneParameters(cachedParameters)

    End Function 'GetSpParameterSet
#End Region

End Class 'OleDBHelperParameterCache  

