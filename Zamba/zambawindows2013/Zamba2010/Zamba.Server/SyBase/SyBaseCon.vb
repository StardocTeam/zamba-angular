Imports System.Data.OleDb
Imports System.Collections.Generic
Imports System.Text
Imports Zamba
Imports Zamba.Servers

Public Class SyBaseCon
    Implements IConnection




    Public Overridable Sub dispose() Implements IConnection.dispose
        Try
            '_dbOwner = Nothing
            CN.Close()
        Catch
        End Try
        Try
            CN.Dispose()
            CN = Nothing
        Catch
        End Try
    End Sub
    Private _CloseFlag As Boolean = False

    'Private _dbOwner As String
    Private _CN As OleDb.OleDbConnection

    Public ReadOnly Property ConString() As String Implements IConnection.ConString
        Get
            Return _CN.ConnectionString
        End Get
    End Property

    Public Property CN() As IDbConnection Implements IConnection.CN
        Get
            Return _CN
        End Get
        Set(ByVal Value As IDbConnection)
            _CN = Value
        End Set
    End Property
    Private _state As IConnection.ConnectionStates
    Public Property State() As IConnection.ConnectionStates Implements IConnection.State
        Get
            Return _state
        End Get
        Set(ByVal Value As IConnection.ConnectionStates)
            _state = Value
        End Set
    End Property

    Private _command As OleDbCommand
    ReadOnly Property Command() As IDbCommand Implements IConnection.Command
        Get
            Return _command
        End Get
    End Property

    Public Sub Open() Implements IConnection.Open

        If CN.State = ConnectionState.Closed Then CN.Open()

    End Sub
    Public Function Close() As Boolean Implements IConnection.Close
        Try
            If CN.State = ConnectionState.Open Then CN.Close()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    'FALTA HACER LA CONVERSION
    Public Function ConvertDate(ByVal datetime As String, Optional ByVal ResolveDate As Boolean = True) As String Implements IConnection.ConvertDate
        Return "'" & datetime & "'"
    End Function
    Public Function ConvertDatetime(ByVal datetime As String) As String Implements IConnection.ConvertDateTime
        Return "'" & datetime & "'"
    End Function
    Public Function ConvertDatetime(ByVal datetime As DateTime) As String Implements IConnection.ConvertDateTime
        Return "'" & datetime & "'"
    End Function

    Public Sub New(ByVal OleconnectionString As String, ByVal DbOwner As String, Optional ByVal Flagclose As Boolean = True)
        _CN = New OleDb.OleDbConnection(OleconnectionString)
        '_dbOwner = DbOwner
        CloseFlag = Flagclose
        Me.dbOwner = DbOwner
    End Sub
    Public Property dbOwner As String Implements IConnection.dbOwner

    Public Overloads Function ExecuteNonQuery(ByVal spName As String,
   ByVal ParametersNames() As Object, ByVal parameterstypes As Object, ByVal parametersValues() As Object) As Integer Implements IConnection.ExecuteNonQuery
        If CN.State = ConnectionState.Closed Then CN.Open()
        Dim cmd As New OleDbCommand
        _command = cmd
        cmd.Connection = CN
        cmd.CommandText = spName
        cmd.CommandType = CommandType.StoredProcedure
        Dim i As Int32
        For i = 0 To parametersValues.Length - 1
            cmd.Parameters.Add(New OleDbParameter(CStr(ParametersNames(i)), parameterstypes(i))).Value = parametersValues(i)
        Next

        cmd.ExecuteNonQuery()

    End Function
#Region "private utility methods & constructors"

    'Since this class provides only static methods, make the default constructor private to prevent 
    'instances from being created with "new SqlHelper()".
    Public Sub New(ByVal co As IDbConnection, Optional ByVal Flagclose As Boolean = True)
        _CN = co
        CloseFlag = Flagclose
    End Sub 'New

    ' This method is used to attach array of SqlParameters to a OleDBCommand.
    ' This method will assign a value of DbNull to any parameter with a direction of
    ' InputOutput and a value of null.  
    ' This behavior will prevent default values from being used, but
    ' this will be the less common case than an intended pure output parameter (derived as InputOutput)
    ' where the user provided no input value.
    ' Parameters:
    ' -command - The command to which the parameters will be added
    ' -commandParameters - an array of SqlParameters tho be added to command
    Sub AttachParameters(ByVal command As IDbCommand, ByVal commandParameters() As IDataParameter) Implements IConnection.AttachParameters
        Dim p As IDbDataParameter
        For Each p In commandParameters
            'check for derived output value with no value assigned
            If p.Direction = ParameterDirection.InputOutput AndAlso p.Value Is Nothing Then
                p.Value = Nothing
            End If
            command.Parameters.Add(p)
        Next p
    End Sub 'AttachParameters

    ' This method assigns an array of values to an array of SqlParameters.
    ' Parameters:
    ' -commandParameters - array of SqlParameters to be assigned values
    ' -array of objects holding the values to be assigned
    Private Sub AssignParameterValues(ByVal commandParameters() As IDbDataParameter, ByVal parameterValues() As Object) Implements IConnection.AssignParameterValues

        Dim i As Short
        Dim j As Short

        If (commandParameters Is Nothing) And (parameterValues Is Nothing) Then
            'do nothing if we get no data
            Return
        End If

        ' we must have the same number of values as we pave parameters to put them in
        If commandParameters.Length <> parameterValues.Length Then
            Throw New ArgumentException("Parameter count does not match Parameter Value count.")
        End If

        'value array
        j = commandParameters.Length - 1
        For i = 0 To j
            commandParameters(i).Value = parameterValues(i)
        Next

    End Sub 'AssignParameterValues

    ' This method opens (if necessary) and assigns a connection, transaction, command type and parameters 
    ' to the provided command.
    ' Parameters:
    ' -command - the OleDBCommand to be prepared
    ' -connection - a valid OleDBConnection, on which to execute this command
    ' -transaction - a valid OleDBTransaction, or 'null'
    ' -commandType - the CommandType (stored procedure, text, etc.)
    ' -commandText - the stored procedure name or T-SQL command
    ' -commandParameters - an array of SqlParameters to be associated with the command or 'null' if no parameters are required
    Private Sub PrepareCommand(ByVal command As IDbCommand,
    ByVal transaction As IDbTransaction,
    ByVal commandType As CommandType,
    ByVal commandText As String,
    ByVal commandParameters() As IDbDataParameter) Implements IConnection.PrepareCommand

        'if the provided connection is not open, we will open it
        If CN.State <> ConnectionState.Open Then
            CN.Open()
        End If

        'associate the connection with the command
        command.Connection = CN

        'set the command text (stored procedure name or SQL statement)
        command.CommandText = commandText

        'if we were provided a transaction, assign it.
        If Not (transaction Is Nothing) Then
            command.Transaction = transaction
        End If

        'set the command type
        command.CommandType = commandType

        'attach the command parameters if they are provided
        If Not (commandParameters Is Nothing) Then
            AttachParameters(command, commandParameters)
        End If

        Return
    End Sub 'PrepareCommand

#End Region

#Region "ExecuteNonQuery"

    Public Overloads Function ExecuteNonQuery(
   ByVal commandType As CommandType,
   ByVal commandText As String) As Integer Implements IConnection.ExecuteNonQuery
        'pass through the call providing null for the set of SqlParameters
        Return ExecuteNonQuery(commandType, commandText, CType(Nothing, OleDbParameter()))

    End Function 'ExecuteNonQuery

    Public Overloads Function ExecuteNonQuery(
   ByVal commandType As CommandType,
   ByVal commandText As String,
   ByVal ParamArray commandParameters() As IDbDataParameter) As Integer Implements IConnection.ExecuteNonQuery

        'create a command and prepare it for execution
        Dim cmd As New OleDbCommand
        _command = cmd
        Dim retval As Integer

        PrepareCommand(cmd, CType(Nothing, OleDbTransaction), commandType, commandText, commandParameters)

        'finally, execute the command.
        retval = cmd.ExecuteNonQuery()

        'detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear()

        Return retval

    End Function 'ExecuteNonQuery

    Overloads Function ExecuteNonQuery(ByVal transaction As IDbTransaction, ByVal spName As String, ByVal ParametersNames As Object(), ByVal parameterstypes As Object, ByVal parametersValues As Object()) As Integer Implements IConnection.ExecuteNonQuery
        'TODO: implementar
    End Function

    Public Overloads Function ExecuteNoTimeOutNonQuery(
 ByVal commandType As CommandType,
 ByVal commandText As String,
 ByVal ParamArray commandParameters() As IDbDataParameter) As Integer Implements IConnection.ExecuteNoTimeOutNonQuery

        'create a command and prepare it for execution
        Dim cmd As New OleDbCommand
        cmd.CommandTimeout = 0
        _command = cmd
        Dim retval As Integer

        PrepareCommand(cmd, CType(Nothing, OleDbTransaction), commandType, commandText, commandParameters)

        'finally, execute the command.
        retval = cmd.ExecuteNonQuery()

        'detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear()

        Return retval

    End Function

    ' Execute a stored procedure via a OleDBCommand (that returns no resultset) against the specified OleDBConnection 
    ' using the provided parameter values.  This method will discover the parameters for the 
    ' stored procedure, and assign the values based on parameter order.
    ' This method provides no access to output parameters or the stored procedure's return value parameter.
    ' e.g.:  
    '  Dim result as integer = ExecuteNonQuery(conn, "PublishOrders", 24, 36)
    ' Parameters:
    ' -connection - a valid OleDBConnection
    ' -spName - the name of the stored procedure 
    ' -parameterValues - an array of objects to be assigned as the input values of the stored procedure 
    ' Returns: an int representing the number of rows affected by the command 
    Public Overloads Function ExecuteNonQuery(
   ByVal spName As String,
   ByVal ParamArray parameterValues() As Object) As Integer Implements IConnection.ExecuteNonQuery
        Dim commandParameters As IDbDataParameter()

        'if we receive parameter values, we need to figure out where they go
        If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
            'pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            commandParameters = OleDBHelperParameterCache.GetSpParameterSet(CN, ConString, spName)

            'assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues)

            'call the overload that takes an array of SqlParameters
            Return ExecuteNonQuery(CommandType.StoredProcedure, spName, commandParameters)
            'otherwise we can just call the SP without params
        Else
            Return ExecuteNonQuery(CommandType.StoredProcedure, spName)
        End If

    End Function 'ExecuteNonQuery

    ' Execute a OleDBCommand (that returns no resultset and takes no parameters) against the provided OleDBTransaction.
    ' e.g.:  
    '  Dim result as Integer = ExecuteNonQuery(trans, CommandType.StoredProcedure, "PublishOrders")
    ' Parameters:
    ' -transaction - a valid OleDBTransaction associated with the connection 
    ' -commandType - the CommandType (stored procedure, text, etc.) 
    ' -commandText - the stored procedure name or T-SQL command 
    ' Returns: an int representing the number of rows affected by the command 
    Public Overloads Function ExecuteNonQuery(ByVal transaction As IDbTransaction,
   ByVal commandType As CommandType,
   ByVal commandText As String) As Integer Implements IConnection.ExecuteNonQuery
        'pass through the call providing null for the set of SqlParameters
        Return ExecuteNonQuery(transaction, commandType, commandText, CType(Nothing, OleDbParameter()))
    End Function 'ExecuteNonQuery

    ' Execute a OleDBCommand (that returns no resultset) against the specified OleDBTransaction
    ' using the provided parameters.
    ' e.g.:  
    ' Dim result as Integer = ExecuteNonQuery(trans, CommandType.StoredProcedure, "GetOrders", new oleDBParameter("@prodid", 24))
    ' Parameters:
    ' -transaction - a valid OleDBTransaction 
    ' -commandType - the CommandType (stored procedure, text, etc.) 
    ' -commandText - the stored procedure name or T-SQL command 
    ' -commandParameters - an array of SqlParamters used to execute the command 
    ' Returns: an int representing the number of rows affected by the command 
    Public Overloads Function ExecuteNonQuery(ByVal transaction As IDbTransaction,
   ByVal commandType As CommandType,
   ByVal commandText As String,
   ByVal ParamArray commandParameters() As IDbDataParameter) As Integer Implements IConnection.ExecuteNonQuery
        'create a command and prepare it for execution
        Dim cmd As New OleDbCommand
        _command = cmd
        Dim retval As Integer

        PrepareCommand(cmd, transaction, commandType, commandText, commandParameters)

        'finally, execute the command.
        retval = cmd.ExecuteNonQuery()

        'detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear()

        Return retval

    End Function 'ExecuteNonQuery

    ' Execute a stored procedure via a OleDBCommand (that returns no resultset) against the specified OleDBTransaction 
    ' using the provided parameter values.  This method will discover the parameters for the 
    ' stored procedure, and assign the values based on parameter order.
    ' This method provides no access to output parameters or the stored procedure's return value parameter.
    ' e.g.:  
    ' Dim result As Integer = SqlHelper.ExecuteNonQuery(trans, "PublishOrders", 24, 36)
    ' Parameters:
    ' -transaction - a valid OleDBTransaction 
    ' -spName - the name of the stored procedure 
    ' -parameterValues - an array of objects to be assigned as the input values of the stored procedure 
    ' Returns: an int representing the number of rows affected by the command 
    Public Overloads Function ExecuteNonQuery(ByVal transaction As IDbTransaction,
   ByVal spName As String,
   ByVal ParamArray parameterValues() As Object) As Integer Implements IConnection.ExecuteNonQuery
        Dim commandParameters As IDbDataParameter()

        'if we receive parameter values, we need to figure out where they go
        If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
            'pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            commandParameters = OleDBHelperParameterCache.GetSpParameterSet(CN, transaction.Connection.ConnectionString, spName)

            'assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues)

            'call the overload that takes an array of SqlParameters
            Return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, commandParameters)
            'otherwise we can just call the SP without params
        Else
            Return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName)
        End If
    End Function 'ExecuteNonQuery

#End Region

#Region "ExecuteDataset"

    Public Overloads Function ExecuteDataset(
    ByVal commandType As CommandType,
    ByVal commandText As String) As DataSet Implements IConnection.ExecuteDataset

        'pass through the call providing null for the set of SqlParameters
        Return ExecuteDataset(commandType, commandText, CType(Nothing, OleDbParameter()))
    End Function 'ExecuteDataset

    Public Overloads Function ExecuteDataset(
    ByVal commandType As CommandType,
    ByVal commandText As String,
    ByVal ParamArray commandParameters() As IDbDataParameter) As DataSet Implements IConnection.ExecuteDataset

        'create a command and prepare it for execution
        Dim cmd As New OleDbCommand
        _command = cmd
        Dim ds As New DataSet
        Dim da As OleDbDataAdapter

        PrepareCommand(cmd, CType(Nothing, OleDbTransaction), commandType, commandText, commandParameters)

        'create the DataAdapter & DataSet
        da = New OleDbDataAdapter(cmd)

        'fill the DataSet using default values for DataTable names, etc.
        da.Fill(ds)

        'detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear()

        'return the dataset
        Return ds

    End Function 'ExecuteDataset

    ' Execute a stored procedure via a OleDBCommand (that returns a resultset) against the specified OleDBConnection 
    ' using the provided parameter values.  This method will discover the parameters for the 
    ' stored procedure, and assign the values based on parameter order.
    ' This method provides no access to output parameters or the stored procedure's return value parameter.
    ' e.g.:  
    ' Dim ds As Dataset = ExecuteDataset(conn, "GetOrders", 24, 36)
    ' Parameters:
    ' -connection - a valid OleDBConnection
    ' -spName - the name of the stored procedure
    ' -parameterValues - an array of objects to be assigned as the input values of the stored procedure
    ' Returns: a dataset containing the resultset generated by the command
    Public Overloads Function ExecuteDataset(
    ByVal spName As String,
    ByVal ParamArray parameterValues() As Object) As DataSet Implements IConnection.ExecuteDataset
        'Return ExecuteDataset(connection, spName, parameterValues)
        Dim commandParameters As IDbDataParameter()

        'if we receive parameter values, we need to figure out where they go
        If Not (parameterValues Is Nothing) And parameterValues.Length > 0 Then
            'pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            commandParameters = OleDBHelperParameterCache.GetSpParameterSet(CN, ConString, spName)

            'assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues)

            'call the overload that takes an array of SqlParameters
            Return ExecuteDataset(CommandType.StoredProcedure, spName, commandParameters)
            'otherwise we can just call the SP without params
        Else
            Return ExecuteDataset(CommandType.StoredProcedure, spName)
        End If

    End Function 'ExecuteDataset


    ' Execute a OleDBCommand (that returns a resultset and takes no parameters) against the provided OleDBTransaction. 
    ' e.g.:  
    ' Dim ds As Dataset = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders")
    ' Parameters
    ' -transaction - a valid OleDBTransaction
    ' -commandType - the CommandType (stored procedure, text, etc.)
    ' -commandText - the stored procedure name or T-SQL command
    ' Returns: a dataset containing the resultset generated by the command
    Public Overloads Function ExecuteDataset(ByVal transaction As IDbTransaction,
    ByVal commandType As CommandType,
    ByVal commandText As String) As DataSet Implements IConnection.ExecuteDataset
        'pass through the call providing null for the set of SqlParameters
        Return ExecuteDataset(transaction, commandType, commandText, CType(Nothing, OleDbParameter()))
    End Function 'ExecuteDataset

    ' Execute a OleDBCommand (that returns a resultset) against the specified OleDBTransaction
    ' using the provided parameters.
    ' e.g.:  
    ' Dim ds As Dataset = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders", new oleDBParameter("@prodid", 24))
    ' Parameters
    ' -transaction - a valid OleDBTransaction 
    ' -commandType - the CommandType (stored procedure, text, etc.)
    ' -commandText - the stored procedure name or T-SQL command
    ' -commandParameters - an array of SqlParamters used to execute the command
    ' Returns: a dataset containing the resultset generated by the command
    Public Overloads Function ExecuteDataset(ByVal transaction As IDbTransaction,
    ByVal commandType As CommandType,
    ByVal commandText As String,
    ByVal ParamArray commandParameters() As IDbDataParameter) As DataSet Implements IConnection.ExecuteDataset
        'create a command and prepare it for execution
        Dim cmd As New OleDbCommand
        _command = cmd
        Dim ds As New DataSet
        Dim da As OleDbDataAdapter

        PrepareCommand(cmd, transaction, commandType, commandText, commandParameters)

        'create the DataAdapter & DataSet
        da = New OleDbDataAdapter(cmd)

        'fill the DataSet using default values for DataTable names, etc.
        da.Fill(ds)

        'detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear()

        'return the dataset
        Return ds
    End Function 'ExecuteDataset

    ' Execute a stored procedure via a OleDBCommand (that returns a resultset) against the specified
    ' OleDBTransaction using the provided parameter values.  This method will discover the parameters for the 
    ' stored procedure, and assign the values based on parameter order.
    ' This method provides no access to output parameters or the stored procedure's return value parameter.
    ' e.g.:  
    ' Dim ds As Dataset = ExecuteDataset(trans, "GetOrders", 24, 36)
    ' Parameters:
    ' -transaction - a valid OleDBTransaction 
    ' -spName - the name of the stored procedure
    ' -parameterValues - an array of objects to be assigned as the input values of the stored procedure
    ' Returns: a dataset containing the resultset generated by the command
    Public Overloads Function ExecuteDataset(ByVal transaction As IDbTransaction,
    ByVal spName As String,
    ByVal ParamArray parameterValues() As Object) As DataSet Implements IConnection.ExecuteDataset
        Dim commandParameters As IDbDataParameter()

        'if we receive parameter values, we need to figure out where they go
        If Not (parameterValues Is Nothing) And parameterValues.Length > 0 Then
            'pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            commandParameters = OleDBHelperParameterCache.GetSpParameterSet(CN, transaction.Connection.ConnectionString, spName)

            'assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues)

            'call the overload that takes an array of SqlParameters
            Return ExecuteDataset(transaction, CommandType.StoredProcedure, spName, commandParameters)
            'otherwise we can just call the SP without params
        Else
            Return ExecuteDataset(transaction, CommandType.StoredProcedure, spName)
        End If
    End Function 'ExecuteDataset

#End Region

#Region "ExecuteReader"
    ' this enum is used to indicate whether the connection was provided by the caller, or created by SqlHelper, so that
    ' we can set the appropriate CommandBehavior when calling ExecuteReader()

    ' Create and prepare a OleDBCommand, and call ExecuteReader with the appropriate CommandBehavior.
    ' If we created and opened the connection, we want the connection to be closed when the DataReader is closed.
    ' If the caller provided the connection, we want to leave it to them to manage.
    ' Parameters:
    ' -connection - a valid OleDBConnection, on which to execute this command 
    ' -transaction - a valid OleDBTransaction, or 'null' 
    ' -commandType - the CommandType (stored procedure, text, etc.) 
    ' -commandText - the stored procedure name or T-SQL command 
    ' -commandParameters - an array of SqlParameters to be associated with the command or 'null' if no parameters are required 
    ' -connectionOwnership - indicates whether the connection parameter was provided by the caller, or created by SqlHelper 
    ' Returns: OleDBDataReader containing the results of the command 
    Private Overloads Function ExecuteReader(
    ByVal transaction As IDbTransaction,
    ByVal commandType As CommandType,
    ByVal commandText As String,
    ByVal commandParameters() As IDbDataParameter,
    ByVal connectionOwnership As SqlConnectionOwnership) As IDataReader Implements IConnection.ExecuteReader
        'create a command and prepare it for execution
        Dim cmd As New OleDbCommand
        _command = cmd
        'create a reader
        Dim dr As IDataReader

        PrepareCommand(cmd, transaction, commandType, commandText, commandParameters)

        ' call ExecuteReader with the appropriate CommandBehavior
        If connectionOwnership = SqlConnectionOwnership.External Then
            dr = cmd.ExecuteReader()
        Else
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        End If

        'detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear()

        Return dr
    End Function 'ExecuteReader

    Public Overloads Function ExecuteReader(
   ByVal commandType As CommandType,
   ByVal commandText As String) As IDataReader Implements IConnection.ExecuteReader

        Return ExecuteReader(commandType, commandText, CType(Nothing, OleDbParameter()))

    End Function 'ExecuteReader

    ' Execute a OleDBCommand (that returns a resultset) against the specified OleDBConnection 
    ' using the provided parameters.
    ' e.g.:  
    ' Dim dr As IDataReader = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders", new oleDBParameter("@prodid", 24))
    ' Parameters:
    ' -connection - a valid OleDBConnection 
    ' -commandType - the CommandType (stored procedure, text, etc.) 
    ' -commandText - the stored procedure name or T-SQL command 
    ' -commandParameters - an array of SqlParamters used to execute the command 
    ' Returns: a OleDBDataReader containing the resultset generated by the command 
    Public Overloads Function ExecuteReader(
   ByVal commandType As CommandType,
   ByVal commandText As String,
   ByVal ParamArray commandParameters() As IDbDataParameter) As IDataReader Implements IConnection.ExecuteReader
        'pass through the call to private overload using a null transaction value
        Return ExecuteReader(CType(Nothing, OleDbTransaction), commandType, commandText, commandParameters, SqlConnectionOwnership.External)

    End Function 'ExecuteReader

    ' Execute a stored procedure via a OleDBCommand (that returns a resultset) against the specified OleDBConnection 
    ' using the provided parameter values.  This method will discover the parameters for the 
    ' stored procedure, and assign the values based on parameter order.
    ' This method provides no access to output parameters or the stored procedure's return value parameter.
    ' e.g.:  
    ' Dim dr As IDataReader = ExecuteReader(conn, "GetOrders", 24, 36)
    ' Parameters:
    ' -connection - a valid OleDBConnection 
    ' -spName - the name of the stored procedure 
    ' -parameterValues - an array of objects to be assigned as the input values of the stored procedure 
    ' Returns: a OleDBDataReader containing the resultset generated by the command 
    Public Overloads Function ExecuteReader(
   ByVal spName As String,
   ByVal ParamArray parameterValues() As Object) As IDataReader Implements IConnection.ExecuteReader
        'pass through the call using a null transaction value
        'Return ExecuteReader(connection, CType(Nothing, OleDBTransaction), spName, parameterValues)

        Dim commandParameters As IDbDataParameter()

        'if we receive parameter values, we need to figure out where they go
        If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
            commandParameters = OleDBHelperParameterCache.GetSpParameterSet(CN, CN.ConnectionString, spName)

            AssignParameterValues(commandParameters, parameterValues)

            Return ExecuteReader(CommandType.StoredProcedure, spName, commandParameters)
            'otherwise we can just call the SP without params
        Else
            Return ExecuteReader(CommandType.StoredProcedure, spName)
        End If

    End Function 'ExecuteReader

    ' Execute a OleDBCommand (that returns a resultset and takes no parameters) against the provided OleDBTransaction.
    ' e.g.:  
    ' Dim dr As IDataReader = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders")
    ' Parameters:
    ' -transaction - a valid OleDBTransaction  
    ' -commandType - the CommandType (stored procedure, text, etc.) 
    ' -commandText - the stored procedure name or T-SQL command 
    ' Returns: a OleDBDataReader containing the resultset generated by the command 
    Public Overloads Function ExecuteReader(ByVal transaction As IDbTransaction,
   ByVal commandType As CommandType,
   ByVal commandText As String) As IDataReader Implements IConnection.ExecuteReader
        'pass through the call providing null for the set of SqlParameters
        Return ExecuteReader(transaction, commandType, commandText, CType(Nothing, OleDbParameter()))
    End Function 'ExecuteReader

    ' Execute a OleDBCommand (that returns a resultset) against the specified OleDBTransaction
    ' using the provided parameters.
    ' e.g.:  
    ' Dim dr As IDataReader = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders", new oleDBParameter("@prodid", 24))
    ' Parameters:
    ' -transaction - a valid OleDBTransaction 
    ' -commandType - the CommandType (stored procedure, text, etc.)
    ' -commandText - the stored procedure name or T-SQL command 
    ' -commandParameters - an array of SqlParamters used to execute the command 
    ' Returns: a OleDBDataReader containing the resultset generated by the command 
    Public Overloads Function ExecuteReader(ByVal transaction As IDbTransaction,
   ByVal commandType As CommandType,
   ByVal commandText As String,
   ByVal ParamArray commandParameters() As IDbDataParameter) As IDataReader Implements IConnection.ExecuteReader
        'pass through to private overload, indicating that the connection is owned by the caller
        Return ExecuteReader(transaction, commandType, commandText, commandParameters, SqlConnectionOwnership.External)
    End Function 'ExecuteReader

    ' Execute a stored procedure via a OleDBCommand (that returns a resultset) against the specified OleDBTransaction 
    ' using the provided parameter values.  This method will discover the parameters for the 
    ' stored procedure, and assign the values based on parameter order.
    ' This method provides no access to output parameters or the stored procedure's return value parameter.
    ' e.g.:  
    ' Dim dr As IDataReader = ExecuteReader(trans, "GetOrders", 24, 36)
    ' Parameters:
    ' -transaction - a valid OleDBTransaction 
    ' -spName - the name of the stored procedure 
    ' -parameterValues - an array of objects to be assigned as the input values of the stored procedure
    ' Returns: a OleDBDataReader containing the resultset generated by the command
    Public Overloads Function ExecuteReader(ByVal transaction As IDbTransaction,
   ByVal spName As String,
   ByVal ParamArray parameterValues() As Object) As IDataReader Implements IConnection.ExecuteReader
        Dim commandParameters As IDbDataParameter()

        'if we receive parameter values, we need to figure out where they go
        If Not (parameterValues Is Nothing) And parameterValues.Length > 0 Then
            commandParameters = OleDBHelperParameterCache.GetSpParameterSet(CN, transaction.Connection.ConnectionString, spName)

            AssignParameterValues(commandParameters, parameterValues)

            Return ExecuteReader(transaction, CommandType.StoredProcedure, spName, commandParameters)
            'otherwise we can just call the SP without params
        Else
            Return ExecuteReader(transaction, CommandType.StoredProcedure, spName)
        End If
    End Function 'ExecuteReader

#End Region

#Region "ExecuteScalar"

    Public Overloads Function ExecuteScalar(
   ByVal commandType As CommandType,
   ByVal commandText As String,
   Optional ByVal timeout As Int32 = 0) As Object Implements IConnection.ExecuteScalar
        'pass through the call providing null for the set of SqlParameters
        Return ExecuteScalar(commandType, commandText, CType(Nothing, OleDbParameter()))
    End Function 'ExecuteScalar

    Public Overloads Function ExecuteScalar(
    ByVal commandType As CommandType,
    ByVal commandText As String,
    ByVal ParamArray commandParameters() As IDbDataParameter) As Object Implements IConnection.ExecuteScalar

        Return ExecuteScalar(commandType, commandText, 0, commandParameters)

    End Function

    Public Overloads Function ExecuteScalar(
   ByVal commandType As CommandType,
   ByVal commandText As String,
      ByVal timeout As Int32,
   ByVal ParamArray commandParameters() As IDbDataParameter) As Object Implements IConnection.ExecuteScalar
        'create a command and prepare it for execution
        Dim cmd As New OleDbCommand
        _command = cmd
        Dim retval As Object

        PrepareCommand(cmd, CType(Nothing, OleDbTransaction), commandType, commandText, commandParameters)

        'execute the command & return the results
        retval = cmd.ExecuteScalar()

        'detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear()

        Return retval

    End Function 'ExecuteScalar

    Public Overloads Function ExecuteScalar(
   ByVal spName As String,
   ByVal ParamArray parameterValues() As Object) As Object Implements IConnection.ExecuteScalar

        Dim commandParameters As IDbDataParameter()

        'if we receive parameter values, we need to figure out where they go
        If Not (parameterValues Is Nothing) And parameterValues.Length > 0 Then
            'pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            commandParameters = OleDBHelperParameterCache.GetSpParameterSet(CN, ConString, spName)

            'assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues)

            'call the overload that takes an array of SqlParameters
            Return ExecuteScalar(CommandType.StoredProcedure, spName, commandParameters)
            'otherwise we can just call the SP without params
        Else
            Return ExecuteScalar(CommandType.StoredProcedure, spName)
        End If

    End Function 'ExecuteScalar

    Public Overloads Function ExecuteScalar(ByVal transaction As IDbTransaction,
   ByVal commandType As CommandType,
   ByVal commandText As String) As Object Implements IConnection.ExecuteScalar
        'pass through the call providing null for the set of SqlParameters
        Return ExecuteScalar(transaction, commandType, commandText, CType(Nothing, OleDbParameter()))
    End Function 'ExecuteScalar

    Public Overloads Function ExecuteScalar(ByVal transaction As IDbTransaction,
   ByVal commandType As CommandType,
   ByVal commandText As String,
   ByVal ParamArray commandParameters() As IDbDataParameter) As Object Implements IConnection.ExecuteScalar
        'create a command and prepare it for execution
        Dim cmd As New OleDbCommand
        _command = cmd
        Dim retval As Object

        PrepareCommand(cmd, transaction, commandType, commandText, commandParameters)

        'execute the command & return the results
        retval = cmd.ExecuteScalar()

        'detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear()

        Return retval

    End Function 'ExecuteScalar

    Public Overloads Function ExecuteScalar(ByVal transaction As IDbTransaction,
   ByVal spName As String,
   ByVal ParamArray parameterValues() As Object) As Object Implements IConnection.ExecuteScalar
        Dim commandParameters As IDbDataParameter()

        'if we receive parameter values, we need to figure out where they go
        If Not (parameterValues Is Nothing) And parameterValues.Length > 0 Then
            'pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            commandParameters = OleDBHelperParameterCache.GetSpParameterSet(CN, transaction.Connection.ConnectionString, spName)

            'assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues)

            'call the overload that takes an array of SqlParameters
            Return ExecuteScalar(transaction, CommandType.StoredProcedure, spName, commandParameters)
            'otherwise we can just call the SP without params
        Else
            Return ExecuteScalar(transaction, CommandType.StoredProcedure, spName)
        End If
    End Function 'ExecuteScalar

    Public Overloads Function ExecuteScalar(ByVal spName As String,
  ByVal ParametersNames() As Object, ByVal parameterstypes As Object, ByVal parametersValues() As Object) As String Implements IConnection.ExecuteScalar

        'TODO:falta implementar similar
        Return Nothing
    End Function
#End Region

    Public Overloads Function ExecuteDataset(ByVal spName As String,
  ByVal ParametersNames() As Object, ByVal parameterstypes As Object, ByVal parameterValues() As Object) As DataSet Implements IConnection.ExecuteDataset
        'TODO:Implementar
        Return Nothing
    End Function
    Public Property CloseFlag() As Boolean Implements IConnection.CloseFlag
        Get
            Return _CloseFlag
        End Get
        Set(ByVal Value As Boolean)
            _CloseFlag = Value
        End Set
    End Property

    Public ReadOnly Property SysDate() As String Implements IConnection.SysDate
        Get
            Return " getdate()"
        End Get
    End Property

    Public ReadOnly Property isOracle As Boolean Implements IConnection.isOracle
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property isODBC As Boolean Implements IConnection.isODBC
        Get
            Return False
        End Get
    End Property

    Overloads Function ExecuteDataset(ByVal transaction As IDbTransaction, ByVal spName As String,
                                      ByVal ParametersNames As Object(), ByVal parameterstypes As Object, ByVal parametersValues As Object()) As DataSet Implements IConnection.ExecuteDataset
        Return Nothing
    End Function

    Public Function ExecuteScalarForMigrator(commandType As System.Data.CommandType, commandText As String, ParamArray commandParameters() As System.Data.IDbDataParameter) As Object Implements IConnection.ExecuteScalarForMigrator

    End Function
End Class
Public Class SyBaseCreateTables
    Implements CreateTables


    Public Shadows Sub AddDocsTables(ByVal DocTypeId As Int64) Implements CreateTables.AddDocsTables
        Try
            Dim str As String = "CREATE TABLE [DOC_T" & DocTypeId & "] ([DOC_ID] [int] PRIMARY KEY IDENTITY,[FOLDER_ID] [int], [DISK_GROUP_ID] [int] NULL, [PLATTER_ID] [int] NULL, [VOL_ID] [int] NULL, [DOC_FILE] [nchar] (200) NULL, [OFFSET] [int] NULL, [DOC_TYPE_ID] [int] NULL, [NAME] [nvarchar] (255) NULL, [ICON_ID] [INT], [SHARED] [INT],ver_Parent_id number(10),version number(1),RootId number(10),NumeroVersion number(2)) ;"
            Server.Con.ExecuteNonQuery(CommandType.Text, str)
            str = "CREATE TABLE [DOC_I" & DocTypeId & "] ([DOC_ID] [int] PRIMARY KEY IDENTITY) ;"
            Server.Con.ExecuteNonQuery(CommandType.Text, str)
            'str = "CREATE TABLE [DOC_D" & DocTypeId & "] ([INDEX_ID] [int] NOT NULL, [INDEX_NAME] [nvarchar] (30) PRIMARY KEY, [INDEX_CREATED] [bit] NOT NULL) ;"
            'Server.Con.ExecuteNonQuery(CommandType.Text, str)
            CreateView(DocTypeId)
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Throw ex
        End Try

    End Sub
    Public Sub CreateView(ByVal docTypeId As Int64) Implements CreateTables.CreateView
        Dim Str As String = "CREATE OR REPLACE VIEW DOC" & docTypeId & " AS SELECT DOC_I" & docTypeId & ".*, DOC_T" & docTypeId & ".FOLDER_ID As FOLDER_ID, DOC_T" & docTypeId & ".DISK_GROUP_ID AS DISK_GROUP_ID, DOC_T" & docTypeId & ".PLATTER_ID AS PLATTER_ID, DOC_T" & docTypeId & ".VOL_ID AS VOL_ID, DOC_T" & docTypeId & ".DOC_FILE AS DOC_FILE, " _
  & "DOC_T" & docTypeId & ".OFFSET AS OFFSET, DOC_T" & docTypeId & ".DOC_TYPE_ID AS DOC_TYPE_ID, DOC_T" & docTypeId & ".NAME AS NAME, DOC_T" & docTypeId & ".ICON_ID AS ICON_ID, DOC_T" & docTypeId & ".SHARED AS SHARED, " _
  & "DOC_T" & docTypeId & ".Original_filename AS Original_filename, DOC_T" & docTypeId & ".ver_Parent_id AS ver_Parent_id, DOC_T" & docTypeId & ".version AS version, DOC_T" & docTypeId & ".RootId AS RootId, DOC_T" & docTypeId & ".NumeroVersion AS NumeroVersion " _
  & " FROM DOC_I" & docTypeId & " INNER JOIN DOC_T" & docTypeId & " ON DOC_I" & docTypeId & ".DOC_ID = DOC_T" & docTypeId & ".DOC_ID"
        Server.Con.ExecuteNonQuery(CommandType.Text, Str)
    End Sub

    ''' <summary>
    ''' Metodo que crea una vista con atributos referenciales
    ''' </summary>
    ''' <param name="docTypeId">Id de la entidad</param>
    ''' <param name="lstColKeys">Listado de las claves y los ids referenciales</param>
    ''' <param name="lstColIndexs">Listado de las columnas y los ids referenciales</param>
    ''' <param name="lstColSelects">Listado de los atributos y los ids referenciales</param>
    ''' <remarks></remarks>
    Public Sub CreateView(ByVal docTypeId As Int64, ByVal lstColKeys As Dictionary(Of String(), String), ByVal lstColIndexs As Dictionary(Of String, String), ByVal lstColSelects As Dictionary(Of String, String())) Implements CreateTables.CreateView
        Dim strBuilder As StringBuilder = New StringBuilder()
        Try
            strBuilder.Append("CREATE OR REPLACE VIEW DOC")
            strBuilder.Append(docTypeId)
            strBuilder.Append(" AS SELECT DOC_I")
            strBuilder.Append(docTypeId)
            strBuilder.Append(".*")

            'Cargo todos los atributos referenciales que se van a visualizar
            For Each key As KeyValuePair(Of String, String()) In lstColSelects
                strBuilder.Append(", ")
                strBuilder.Append(lstColIndexs(key.Key))
                strBuilder.Append(".")
                strBuilder.Append(key.Value(1))
                strBuilder.Append(" AS I")
                strBuilder.Append(key.Value(0))
            Next

            strBuilder.Append(", DOC_T")
            strBuilder.Append(docTypeId)
            strBuilder.Append(".FOLDER_ID As FOLDER_ID, DOC_T")
            strBuilder.Append(docTypeId)
            strBuilder.Append(".DISK_GROUP_ID AS DISK_GROUP_ID, DOC_T")
            strBuilder.Append(docTypeId)
            strBuilder.Append(".PLATTER_ID AS PLATTER_ID, DOC_T")
            strBuilder.Append(docTypeId)
            strBuilder.Append(".VOL_ID AS VOL_ID, DOC_T")
            strBuilder.Append(docTypeId)
            strBuilder.Append(".DOC_FILE AS DOC_FILE, ")
            strBuilder.Append("DOC_T")
            strBuilder.Append(docTypeId)
            strBuilder.Append(".OFFSET AS OFFSET, DOC_T")
            strBuilder.Append(docTypeId)
            strBuilder.Append(".DOC_TYPE_ID AS DOC_TYPE_ID, DOC_T")
            strBuilder.Append(docTypeId)
            strBuilder.Append(".NAME AS NAME, DOC_T")
            strBuilder.Append(docTypeId)
            strBuilder.Append(".ICON_ID AS ICON_ID, DOC_T")
            strBuilder.Append(docTypeId)
            strBuilder.Append(".SHARED AS SHARED, ")
            strBuilder.Append("DOC_T")
            strBuilder.Append(docTypeId)
            strBuilder.Append(".Original_filename AS Original_filename, DOC_T")
            strBuilder.Append(docTypeId)
            strBuilder.Append(".ver_Parent_id AS ver_Parent_id, DOC_T")
            strBuilder.Append(docTypeId)
            strBuilder.Append(".version AS version, DOC_T")
            strBuilder.Append(docTypeId)
            strBuilder.Append(".RootId AS RootId, DOC_T")
            strBuilder.Append(docTypeId)
            strBuilder.Append(".NumeroVersion AS NumeroVersion ")
            strBuilder.Append(" FROM DOC_I")
            strBuilder.Append(docTypeId)
            strBuilder.Append(" INNER JOIN DOC_T")
            strBuilder.Append(docTypeId)
            strBuilder.Append(" ON DOC_I")
            strBuilder.Append(docTypeId)
            strBuilder.Append(".DOC_ID = DOC_T")
            strBuilder.Append(docTypeId)
            strBuilder.Append(".DOC_ID")

            'Agrego los join
            'Contador para los alias de las tablas
            Dim cont As Int16 = 1
            'Guarda el alias de la tabla
            Dim tables As Dictionary(Of String, String) = New Dictionary(Of String, String)
            'Guarda las comparaciones ya realizadas
            Dim tablesCol As List(Of String) = New List(Of String)

            'Agrego los join
            For Each key As KeyValuePair(Of String(), String) In lstColKeys
                If tablesCol.Contains(lstColIndexs(key.Value) & key.Key(1)) = False Then
                    If tables.ContainsKey(lstColIndexs(key.Value)) Then
                        strBuilder.Append(" AND I")
                    Else
                        strBuilder.Append(" INNER JOIN ")
                        strBuilder.Append(lstColIndexs(key.Value))
                        strBuilder.Append(" table" & cont)
                        strBuilder.Append(" ON I")
                        tables.Add(lstColIndexs(key.Value), "table" & cont)
                        cont = cont + 1
                    End If

                    strBuilder.Append(key.Key(0))
                    strBuilder.Append(" = ")
                    strBuilder.Append(lstColIndexs(key.Value))
                    strBuilder.Append(".")
                    strBuilder.Append(key.Key(1))

                    tablesCol.Add(lstColIndexs(key.Value) & key.Key(1))
                End If
            Next

            'Reemplazo donde se utiliza la tabla por el alias que se le creo
            Dim strSql As String = strBuilder.ToString()
            For Each tabla As KeyValuePair(Of String, String) In tables
                strSql = strSql.Replace(tabla.Key & ".", tabla.Value & ".")
            Next

            Server.Con.ExecuteNonQuery(CommandType.Text, strSql)
        Finally
            strBuilder = Nothing
        End Try
    End Sub
    Private Sub DropView(ByVal DocTypeId As Int64) Implements CreateTables.DropView
        Dim Str As String = "DROP VIEW DOC" & DocTypeId
        Server.Con.ExecuteNonQuery(CommandType.Text, Str)
    End Sub
    Public Shadows Sub CreateTextIndex(ByVal DocTypeId As Int64, ByVal IndexId As Int64) Implements CreateTables.CreateTextIndex



        Dim str As String = "CREATE TABLE [DOC_X" & DocTypeId & "] ([DOC_ID] [int], [ID] [int] PRIMARY KEY) ;"
        Server.Con.ExecuteNonQuery(CommandType.Text, str)
        str = "CREATE TABLE [DOC_XD" & DocTypeId & "] ([ID] [int], [WORD] [nvarchar] (50) PRIMARY KEY ) ;"
        Server.Con.ExecuteNonQuery(CommandType.Text, str)
        Dim StrUpdate As String = "UPDATE DOC_TYPE SET DocumentalID = " & IndexId & " where DOC_TYPE_ID = " & DocTypeId
        Server.Con.ExecuteNonQuery(CommandType.Text, StrUpdate)

    End Sub
    Public Shadows Sub AddIndexColumn(ByVal DocTypeId As Int64, ByVal IndexIdArray As ArrayList, ByVal IndexTypeArray As ArrayList, ByVal IndexLenArray As ArrayList) Implements CreateTables.AddIndexColumn
        'Dim str As String = "ALTER TABLE DOC_I" & DocTypeId & " ADD ("
        Dim str As New StringBuilder
        Dim i As Integer
        Dim str1 As New StringBuilder

        str.Append("ALTER TABLE DOC_I")
        str.Append(DocTypeId.ToString)
        str.Append(" ADD (")

        For i = 0 To IndexIdArray.Count - 1
            Dim type As Integer = IndexTypeArray(i)
            If str1.Length > 0 Then
                str1.Remove(0, str.Length)
            End If
            Select Case type
                Case 1
                    str1.Append("I")
                    str1.Append(IndexIdArray(i))
                    str1.Append(" int NULL")
                Case 2
                    str1.Append("I")
                    str1.Append(IndexIdArray(i))
                    str1.Append(" numeric NULL")
                Case 3
                    str1.Append("I")
                    str1.Append(IndexIdArray(i))
                    str1.Append(" float NULL")
                Case 4
                    str1.Append("I")
                    str1.Append(IndexIdArray(i))
                    str1.Append(" varchar (")
                    str1.Append(IndexLenArray(i))
                    str1.Append(") NULL")
                Case 5
                    str1.Append("I")
                    str1.Append(IndexIdArray(i))
                    str1.Append(" varchar (")
                    str1.Append(IndexLenArray(i))
                    str1.Append(") NULL")
                Case 6
                    str1.Append("I")
                    str1.Append(IndexIdArray(i))
                    str1.Append(" money NULL")
                Case 7
                    str1.Append("I")
                    str1.Append(IndexIdArray(i))
                    str1.Append(" varchar (")
                    str1.Append(IndexLenArray(i))
                    str1.Append(") NULL")
                Case 8
                    str1.Append("I")
                    str1.Append(IndexIdArray(i))
                    str1.Append(" varchar (")
                    str1.Append(IndexLenArray(i))
                    str1.Append(") NULL")
            End Select
            If i = 0 Then
                str.Append(str1.ToString)
            Else
                str.Append(", ")
                str.Append(str1.ToString)
            End If
        Next
        str.Append(")")
        Server.Con.ExecuteNonQuery(CommandType.Text, str.ToString)
        For i = 0 To IndexIdArray.Count - 1
            'Dim StrIndex As String = "CREATE INDEX [" & "I" & IndexIdArray(i) & "] ON [DOC_I" & DocTypeId & "] ([I" & IndexIdArray(i) & "]) "
            Dim StrIndex As New StringBuilder
            StrIndex.Append("CREATE INDEX [")
            StrIndex.Append("I")
            StrIndex.Append(IndexIdArray(i))
            StrIndex.Append("] ON [DOC_I")
            StrIndex.Append(DocTypeId.ToString)
            StrIndex.Append("] ([I")
            StrIndex.Append(IndexIdArray(i))
            StrIndex.Append("]) ")

            Server.Con.ExecuteNonQuery(CommandType.Text, StrIndex.ToString)
        Next

    End Sub
    Public Shadows Sub DelIndexColumn(ByVal DocTypeId As Int64, ByVal IndexIdArray As ArrayList) Implements CreateTables.DelIndexColumn


        Dim str As String = "ALTER TABLE DOC_I" & DocTypeId & " DROP COLUMN "
        Dim i As Integer
        Dim str1 As String
        For i = 0 To IndexIdArray.Count - 1
            str1 = "I" & IndexIdArray(i)
            If i = 0 Then
                str = str & str1
            Else
                str = str & ", " & str1
            End If
        Next
        'Si no existe la columna, que siga borrando el resto igual
        Try
            Server.Con.ExecuteNonQuery(CommandType.Text, str)
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
    End Sub
    Public Shadows Sub AddIndexList(ByVal IndexId As Int64, ByVal IndexLen As Integer) Implements CreateTables.AddIndexList



        Dim str As String = "CREATE TABLE [ILST_I" & IndexId & "] ([ITEMID] [int] NOT NULL, [ITEM] [nvarchar] (" & IndexLen & ") NOT NULL) ;"
        ' Create the tables.
        Server.Con.ExecuteNonQuery(CommandType.Text, str)
        Dim StrPrimarykey As String = "ALTER TABLE [ILST_I" & IndexId & "] WITH NOCHECK ADD CONSTRAINT [PK_ILST_I" & IndexId & "] PRIMARY KEY  CLUSTERED ([ITEMID]) "
        Server.Con.ExecuteNonQuery(CommandType.Text, StrPrimarykey)

    End Sub
    Public Shadows Sub InsertIndexList(ByVal IndexId As Int64, ByVal IndexList As ArrayList) Implements CreateTables.InsertIndexList



        Dim strdelete As String = "DELETE FROM ILST_I" & IndexId

        ' Create the tables.
        'Del the old list
        Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
        'add the current list
        Dim i As Integer
        Dim table As String = "ILst_I" & IndexId
        For i = 0 To IndexList.Count - 1
            Dim Valuestring As String = i + 1 & ", '" & IndexList.Item(i) & "'"
            Dim strInsert As String = "INSERT INTO " & table & " (ITEMID, ITEM) Values (" & Valuestring & ")"
            Server.Con.ExecuteNonQuery(CommandType.Text, strInsert)
        Next

    End Sub
    Public Shadows Sub DelIndexList(ByVal IndexId As Int64) Implements CreateTables.DelIndexList



        Dim str As String = "DROP TABLE ILST_I" & IndexId & ";"
        Server.Con.ExecuteNonQuery(CommandType.Text, str)

    End Sub
    Public Shadows Sub DelIndexItems(ByVal IndexId As Int64, ByVal IndexList As ArrayList) Implements CreateTables.DelIndexItems



        Dim i As Integer
        Dim table As String = "ILST_I" & IndexId
        For i = 0 To IndexList.Count - 1
            Dim strdelete As String = "DELETE FROM " & table & " where(ITEM = '" & IndexList(i) & "')"
            Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
        Next

    End Sub
    Public Shadows Sub DeleteTable(ByVal Table As String) Implements CreateTables.DeleteTable
        Try
            Dim strDelete As String = "DROP TABLE " & Table & ";"
            Server.Con.ExecuteNonQuery(CommandType.Text, strDelete)
        Catch ex As Exception
            If Not ex.ToString.Contains("no existe") AndAlso Not ex.ToString.Contains("not exist") Then
                Throw ex
            End If
        End Try
    End Sub
    'Public Shadows Sub ExecuteArchivo(ByVal path As String) Implements CreateTables.ExecuteArchivo
    '    System.Console.WriteLine()
    'End Sub
    Private Sub DelTempTables() Implements CreateTables.DelTempTables

        Try

            Dim Strdrop As String
            Dim i As Int32
            For i = 1 To 100
                Strdrop = "DROP TABLE DOC_T" & i
                Server.Con.ExecuteNonQuery(CommandType.Text, Strdrop)
                Strdrop = "DROP TABLE DOC_I" & i
                Server.Con.ExecuteNonQuery(CommandType.Text, Strdrop)
                'Strdrop = "DROP TABLE DOC_D" & i
                'Server.Con.ExecuteNonQuery(CommandType.Text, Strdrop)
                'Strdrop = "DROP TABLE DOC_X" & i
                'Server.Con.ExecuteNonQuery(CommandType.Text, Strdrop)
                'Strdrop = "DROP TABLE DOC_XD" & i
                'Server.Con.ExecuteNonQuery(CommandType.Text, Strdrop)
            Next
        Catch ex As Exception
        Finally
            'con.close()
            'con.dispose()
            'con = nothing
        End Try
    End Sub
    Public Shadows Sub CreateSustitucionTable(ByVal Index As Int64, ByVal IndexLen As Int32, ByVal IndexType As Int32) Implements CreateTables.CreateSustitucionTable
        Dim strType As String

        Select Case IndexType
            Case 1
                strType = "NUMERIC"
            Case 2, 9
                strType = "NUMERIC"
            Case 3, 6
                strType = "decimal(18,2)"
            Case 4, 5
                strType = "datetime"
            Case 7, 8
                strType = "VARCHAR(" & IndexLen.ToString & ")"
            Case Else
                strType = "NUMERIC"
        End Select
        Try
            Dim strcreate As String = "Create Table SLST_S" & Index & " (Codigo " & strType & " NOT NULL, Descripcion varchar(60), CONSTRAINT PK_Codigo" & Index & " PRIMARY KEY(Codigo))"
            Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
    End Sub
    Public Shadows Sub UpdateIntoSustitucion(ByVal Tabla As String, ByVal Codigo As Int32, ByVal Descripcion As String) Implements CreateTables.UpdateIntoSustitucion

        'actualiza la tabla de sustitucion
        'Dim Tabla As String = "SLST_S" & TableIndex

        Dim DsSelect As DataSet
        Dim strupdate As String
        Dim strselect As String = "Select count(1) from " & Tabla & " Where Codigo=" & Codigo
        Try
            DsSelect = Server.Con.ExecuteDataset(CommandType.Text, strselect)
            If DsSelect.Tables(0).Rows(0).Item(0) > 0 Then
                If Codigo.ToString.Trim <> "" AndAlso Descripcion.Trim <> "" Then
                    strupdate = "Update " & Tabla & " Set descripcion='" & Descripcion & "' where Codigo= " & Codigo
                    Server.Con.ExecuteNonQuery(CommandType.Text, strupdate)
                End If
            End If
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Throw ex
        End Try
    End Sub
    Public Shadows Sub InsertIntoSustitucion(ByVal Tabla As String, ByVal Codigo As Int32, ByVal Descripcion As String) Implements CreateTables.InsertIntoSustitucion

        'Inserta en la tabla de sustitucion
        'Dim Tabla As String = "SLST_S" & TableIndex

        Dim DsSelect As DataSet
        Dim strinsert As String
        Dim strselect As String = "Select count(1) from " & Tabla & " Where Codigo=" & Codigo
        Try
            DsSelect = Server.Con.ExecuteDataset(CommandType.Text, strselect)
            If DsSelect.Tables(0).Rows(0).Item(0) = 0 Then
                If (Codigo).ToString.Trim <> "" AndAlso Descripcion.Trim <> "" Then
                    strinsert = "Insert into " & Tabla & " Values(" & Codigo & ",'" & Descripcion & "')"
                    Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
                End If
            End If
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Throw ex
        End Try
    End Sub
    Public Shadows Sub DeleteFromSustitucion(ByVal Tabla As String, ByVal Codigo As Int32, ByVal Descripcion As String) Implements CreateTables.DeleteFromSustitucion
        'Elimina las filas modificadas de la tabla de sustitucion
        'Dim Tabla As String = "SLST_S" & TableIndex
        Dim strdelete As String = ""
        Try
            If Codigo.ToString.Trim <> "" Then
                strdelete = "Delete From " & Tabla & " Where Codigo= " & Codigo
                Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
            End If
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Throw ex
        End Try

    End Sub
    Public Shadows Sub DropSustitucionTable(ByVal IndexId As Int64) Implements CreateTables.DropSustitucionTable
        Dim strdrop As String = "DROP TABLE [SLST_S" & IndexId & "]"
        Try
            Server.Con.ExecuteNonQuery(CommandType.Text, strdrop)
        Catch
        End Try
    End Sub
    Public Shadows Sub BorrarSustitucionTable(ByVal IndexId As Int64) Implements CreateTables.BorrarSustitucionTable
        Dim strdelete As String = "Delete from [SLST_S" & IndexId & "]"
        Try
            Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
        Catch
        End Try
    End Sub
    Public Overridable Sub Dispose() Implements CreateTables.Dispose

    End Sub

    Public Sub BulkInsertSustitucionTable(ByVal FileName As String, ByVal separador As String, ByVal IndexId As Int64) Implements CreateTables.BulkInsertSustitucionTable
        Dim tabla As String = "SLST_S" & IndexId
        Dim sr As New IO.StreamReader(FileName)
        Try
            'La existencia del archivo debe ser verificada en la capa anterior
            Dim campos() As String
            Dim count As Int32
            Dim sql As String
            While sr.Peek <> -1
                campos = sr.ReadLine.Split(separador)
                sql = "Select count(1) from " & tabla & " Where Codigo=" & campos(0)
                count = Server.Con.ExecuteScalar(CommandType.Text, sql)
                If count = 0 Then
                    sql = "Insert into " & tabla & "(codigo,Descripcion) values(" & campos(0) & ",'" & campos(1) & "')"
                Else
                    sql = "Update " & tabla & " set descripcion='" & campos(1) & "' Where codigo=" & campos(0)
                End If
                Server.Con.ExecuteNonQuery(CommandType.Text, sql)
            End While
        Catch
        Finally
            sr.Close()
        End Try
    End Sub
    Public Sub ExportSustitucionTable(ByVal file As String, ByVal separador As String, ByVal IndexId As Int64) Implements CreateTables.ExportSustitucionTable
        Dim sql As String = "Select Codigo,Descripcion from SLST_S" & IndexId & " order by Codigo"
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
        Try
            Dim sw As New IO.StreamWriter(file, False)
            sw.AutoFlush = True
            Dim i As Int64
            For i = 0 To ds.Tables(0).Rows.Count - 1
                Dim strb As New StringBuilder
                strb.Append(ds.Tables(0).Rows(i).Item(0) & separador & ds.Tables(0).Rows(i).Item(1))
                sw.WriteLine(strb.ToString)
            Next
            sw.Close()
        Catch
        Finally
            ds.Dispose()
        End Try
    End Sub

    Public Sub AddObsTable(DocTypeId As Long, IndexIdArray As ArrayList, IndexTypeArray As ArrayList, IndexLenArray As ArrayList) Implements CreateTables.AddObsTable
        Throw New NotImplementedException()
    End Sub
End Class
