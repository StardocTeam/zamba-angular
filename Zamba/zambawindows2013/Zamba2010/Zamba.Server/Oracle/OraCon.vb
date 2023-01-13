Imports System.Data.OleDb
Imports System.Collections.Generic
Imports System.Text
Imports Zamba
Imports Zamba.Servers

Public Class OraCon
    Implements IConnection



    Public Overridable Sub dispose() Implements IConnection.dispose
        Try
            '	_dbOwner = Nothing
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
    Private _CN As IDbConnection
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
            If _CN IsNot Nothing AndAlso CN.State = ConnectionState.Open Then
                _CN.Close()
                _CN.Dispose()
                _CN = Nothing
            End If
            Return True
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ConvertDate(ByVal data As String, Optional ByVal ResolveDate As Boolean = True) As String Implements IConnection.ConvertDate
        Dim d As Date
        d = data
        Return "TO_DATE('" & d.ToString("dd/MM/yyyy") & "','DD/MM/YYYY')"
    End Function
    Public Function ConvertDatetime(ByVal data As String) As String Implements IConnection.ConvertDateTime
        Dim d As DateTime = Date.Parse(data)
        Dim str As String = "TO_DATE('" & d.ToString("dd/MM/yyyy hh:mm") & "' ,'DD/MM/YYYY HH24:MI')"
        Return str
    End Function
    Public Function ConvertDatetime(ByVal data As DateTime) As String Implements IConnection.ConvertDateTime
        Dim str As String = "TO_DATE('" & data.ToString("yyyy-MM-dd HH:mm:ss") & "' ,'YYYY-MM-DD HH24:MI:SS')"
        Return str
    End Function
    Public Sub New(ByVal OleconnectionString As String, ByVal DbOwner As String, Optional ByVal flagclose As Boolean = True)
        _CN = New OleDb.OleDbConnection(OleconnectionString)
        ' _dbOwner = DbOwner
        CloseFlag = flagclose

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

        If (commandParameters Is Nothing) AndAlso (parameterValues Is Nothing) Then
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

    Overloads Function ExecuteNonQuery(ByVal transaction As IDbTransaction, ByVal spName As String, ByVal ParametersNames As Object(), ByVal parameterstypes As Object, ByVal parametersValues As Object()) As Integer Implements IConnection.ExecuteNonQuery
        'TODO: implementar
    End Function

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

    Public Overloads Function ExecuteNonQuery(ByVal transaction As IDbTransaction,
   ByVal commandType As CommandType,
   ByVal commandText As String) As Integer Implements IConnection.ExecuteNonQuery
        'pass through the call providing null for the set of SqlParameters
        Return ExecuteNonQuery(transaction, commandType, commandText, CType(Nothing, OleDbParameter()))
    End Function 'ExecuteNonQuery

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

    Public Overloads Function ExecuteDataset(ByVal spName As String,
  ByVal ParametersNames() As Object, ByVal parameterstypes As Object, ByVal parametersValues() As Object) As DataSet Implements IConnection.ExecuteDataset
        Dim Ds As New DataSet
        If CN.State = ConnectionState.Closed Then CN.Open()
        Dim cmd As New OleDbCommand
        _command = cmd
        cmd.Connection = CN
        cmd.CommandText = spName
        cmd.CommandType = CommandType.StoredProcedure
        Dim i As Int32
        For i = 0 To parametersValues.Length - 2
            cmd.Parameters.Add(New OleDbParameter(CStr(ParametersNames(i)), parameterstypes(i))).Value = parametersValues(i)
        Next
        'falta ver como se resuelve esto
        '''''        cmd.Parameters.Add(New OleDbParameter(CStr(ParametersNames(parametersValues.Length - 1)), OleDb.OleDbType.Cursor)).Direction = ParameterDirection.Output
        'parameterstypes(parametersValues.Length - 1))).Direction = parametersValues(parametersValues.Length - 1)
        Dim Da As New OleDbDataAdapter(cmd)

        Da.Fill(Ds)

        Return Ds
    End Function


    Public Overloads Function ExecuteDataset(
    ByVal commandType As CommandType,
    ByVal commandText As String) As DataSet Implements IConnection.ExecuteDataset

        'pass through the call providing null for the set of SqlParameters
        Return ExecuteDataset(commandType, commandText, CType(Nothing, OleDbParameter()))
    End Function 'ExecuteDataset

    ' Execute a OleDBCommand (that returns a resultset) against the specified OleDBConnection 
    ' using the provided parameters.
    ' e.g.:  
    ' Dim ds as Dataset = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders", new oleDBParameter("@prodid", 24))
    ' Parameters:
    ' -connection - a valid OleDBConnection
    ' -commandType - the CommandType (stored procedure, text, etc.)
    ' -commandText - the stored procedure name or T-SQL command
    ' -commandParameters - an array of SqlParamters used to execute the command
    ' Returns: a dataset containing the resultset generated by the command
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
        If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
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
        If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
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

    Overloads Function ExecuteDataset(ByVal transaction As IDbTransaction, ByVal spName As String,
                                  ByVal ParametersNames As Object(), ByVal parameterstypes As Object, ByVal parametersValues As Object()) As DataSet Implements IConnection.ExecuteDataset
        Return Nothing
        'TODO: Implementar
    End Function
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

    Public Overloads Function ExecuteReader(
   ByVal commandType As CommandType,
   ByVal commandText As String,
   ByVal ParamArray commandParameters() As IDbDataParameter) As IDataReader Implements IConnection.ExecuteReader
        'pass through the call to private overload using a null transaction value
        Return ExecuteReader(CType(Nothing, OleDbTransaction), commandType, commandText, commandParameters, SqlConnectionOwnership.External)

    End Function 'ExecuteReader

    Public Overloads Function ExecuteReader(
   ByVal spName As String,
   ByVal ParamArray parameterValues() As Object) As IDataReader Implements IConnection.ExecuteReader
        'pass through the call using a null transaction value
        'Return ExecuteReader(connection, CType(Nothing, OleDBTransaction), spName, parameterValues)

        Dim commandParameters As IDbDataParameter()

        'if we receive parameter values, we need to figure out where they go
        If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
            commandParameters = OleDBHelperParameterCache.GetSpParameterSet(CN, ConString, spName)

            AssignParameterValues(commandParameters, parameterValues)

            Return ExecuteReader(CommandType.StoredProcedure, spName, commandParameters)
            'otherwise we can just call the SP without params
        Else
            Return ExecuteReader(CommandType.StoredProcedure, spName)
        End If

    End Function 'ExecuteReader

    Public Overloads Function ExecuteReader(ByVal transaction As IDbTransaction,
   ByVal commandType As CommandType,
   ByVal commandText As String) As IDataReader Implements IConnection.ExecuteReader
        'pass through the call providing null for the set of SqlParameters
        Return ExecuteReader(transaction, commandType, commandText, CType(Nothing, OleDbParameter()))
    End Function 'ExecuteReader

    Public Overloads Function ExecuteReader(ByVal transaction As IDbTransaction,
   ByVal commandType As CommandType,
   ByVal commandText As String,
   ByVal ParamArray commandParameters() As IDbDataParameter) As IDataReader Implements IConnection.ExecuteReader
        'pass through to private overload, indicating that the connection is owned by the caller
        Return ExecuteReader(transaction, commandType, commandText, commandParameters, SqlConnectionOwnership.External)
    End Function 'ExecuteReader

    Public Overloads Function ExecuteReader(ByVal transaction As IDbTransaction,
   ByVal spName As String,
   ByVal ParamArray parameterValues() As Object) As IDataReader Implements IConnection.ExecuteReader
        Dim commandParameters As IDbDataParameter()

        'if we receive parameter values, we need to figure out where they go
        If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
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
        If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
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
        If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
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
            Return " sysdate "
        End Get
    End Property

    Public ReadOnly Property isOracle As Boolean Implements IConnection.isOracle
        Get
            Return True
        End Get
    End Property

    Public ReadOnly Property isODBC As Boolean Implements IConnection.isODBC
        Get
            Return False
        End Get
    End Property

    Public Function ExecuteScalarForMigrator(commandType As System.Data.CommandType, commandText As String, ParamArray commandParameters() As System.Data.IDbDataParameter) As Object Implements IConnection.ExecuteScalarForMigrator

    End Function
End Class

Public Class OraCreateTables
    Implements CreateTables

    Public Shadows Sub DropSustitucionTable(ByVal IndexId As Int64) Implements CreateTables.DropSustitucionTable
        Dim strdrop As String = "DROP TABLE SLST_S" & IndexId
        Try
            Server.Con.ExecuteNonQuery(CommandType.Text, strdrop)
        Catch ex As Exception
        End Try
    End Sub

    Public Shadows Sub AddDocsTables(ByVal DocTypeId As Int64) Implements CreateTables.AddDocsTables
        Try
            Dim str As String = "CREATE TABLE DOC_T" & DocTypeId & " (" & "DOC_ID NUMBER(10) primary key, FOLDER_ID NUMBER(10) NOT NULL, DISK_GROUP_ID" & " NUMBER(10), " & "PLATTER_ID" & " NUMBER(10), " & "VOL_ID" & " NUMBER(10), " & "DOC_FILE" & " char(200), " & "OFFSET" & " NUMBER(10), " & "DOC_TYPE_ID" & " NUMBER(10), " & "NAME" & " CHAR(255), ICON_ID NUMBER(2),SHARED NUMBER (2), Original_filename varchar2(255),ver_Parent_id number(10),version number(1),RootId number(10),NumeroVersion number(2), FileSize NUMBER(10,4))  TABLESPACE " & Server.dbOwner
            Server.Con.ExecuteNonQuery(CommandType.Text, str)
            str = "CREATE TABLE " & "DOC_I" & DocTypeId & " (" & "DOC_ID" & " NUMBER(10), CONSTRAINT fk" & DocTypeId & " FOREIGN KEY(doc_id) REFERENCES DOC_T" & DocTypeId & " ) TABLESPACE " & Server.dbOwner
            Server.Con.ExecuteNonQuery(CommandType.Text, str)
            'CREO LA TABLA DOC_D id QUE CONTIENE LA INFORMACION DE LOS ATRIBUTOS DE LA TABLA DOC_I id
            'str = "CREATE TABLE " & Server.dbOwner & "." & "DOC_D" & DocTypeId & " (" & "INDEX_ID" & " NUMBER(10) NOT NULL, " & "INDEX_NAME" & " CHAR(30) NOT NULL, " & "INDEX_CREATED" & " NUMBER(1) NOT NULL, INDEX_TYPE NUMBER(1) NOT NULL ,CONSTRAINT INDEX_" & DocTypeId & "_PK PRIMARY KEY (INDEX_NAME))"
            'Server.Con.ExecuteNonQuery(CommandType.Text, str)

            Dim strTrig As New StringBuilder

            strTrig.Append("CREATE OR REPLACE TRIGGER TU_" & DocTypeId & " before ")
            strTrig.Append("UPDATE ON DOC_I" & DocTypeId & "  FOR EACH ROW BEGIN ")
            strTrig.Append(":new.lupdate:=sysdate; ")
            strTrig.Append("END tu_" & DocTypeId & ";")
            Server.Con.ExecuteNonQuery(CommandType.Text, strTrig.ToString)

            strTrig.Remove(0, strTrig.ToString.Length)

            strTrig.Append("CREATE OR REPLACE TRIGGER TI_" & DocTypeId & " before INSERT ON DOC_I" & DocTypeId & " FOR EACH ROW BEGIN :new.crdate:=sysdate; :new.lupdate:=sysdate; END ti_" & DocTypeId & ";")
            Server.Con.ExecuteNonQuery(CommandType.Text, strTrig.ToString)

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
    '''<history>Marcelo modified 30/03/09</history>
    ''' <remarks></remarks>
    Public Sub CreateView(ByVal docTypeId As Int64, ByVal lstColKeys As Dictionary(Of String(), String), ByVal lstColIndexs As Dictionary(Of String, String), ByVal lstColSelects As Dictionary(Of String, String())) Implements CreateTables.CreateView
        Dim Str As String
        Dim strBuilder As StringBuilder = New StringBuilder()
        Try
            'Elimino la vista anterior
            Str = "if exists(select * from sysobjects where xtype='V' and name ='doc" & docTypeId & "') begin drop view doc" & docTypeId & " end"
            Server.Con.ExecuteNonQuery(CommandType.Text, Str)
        Catch ex As SqlClient.SqlException
        Catch ex As Exception
        End Try
        Try
            strBuilder.Append("CREATE VIEW DOC")
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
                        strBuilder.Append(" AND DOC_I")
                        strBuilder.Append(docTypeId)
                        strBuilder.Append(".I")
                    Else
                        strBuilder.Append(" LEFT OUTER JOIN ")
                        strBuilder.Append(lstColIndexs(key.Value))
                        strBuilder.Append(" table" & cont)
                        strBuilder.Append(" ON DOC_I")
                        strBuilder.Append(docTypeId)
                        strBuilder.Append(".I")
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



        Dim str As String = "CREATE TABLE DOC_X" & DocTypeId & " (DOC_ID int, ID int PRIMARY KEY)"
        Server.Con.ExecuteNonQuery(CommandType.Text, str)
        str = "CREATE TABLE DOC_XD" & DocTypeId & " (ID int, WORD nchar (50) PRIMARY KEY)"
        Server.Con.ExecuteNonQuery(CommandType.Text, str)
        Dim StrUpdate As String = "UPDATE DOC_TYPE SET DocumentalID = " & IndexId & " where DOC_TYPE_ID = " & DocTypeId
        Server.Con.ExecuteNonQuery(CommandType.Text, StrUpdate)

    End Sub
    Public Shadows Sub DelIndexColumn(ByVal DocTypeId As Int64, ByVal IndexIdArray As ArrayList) Implements CreateTables.DelIndexColumn

        Dim str As String = "ALTER TABLE " & Chr(34) & Server.dbOwner & Chr(34) & "." & Chr(34) & "DOC_I" & DocTypeId & Chr(34) & " SET UNUSED ("
        'Dim strd As String = "ALTER TABLE " & Chr(34) & Server.dbOwner & Chr(34) & "." & Chr(34) & "DOC_D" & DocTypeId & Chr(34) & " SET UNUSED ("
        Dim i As Integer
        Dim str1 As String
        For i = 0 To IndexIdArray.Count - 1
            str1 = Chr(34) & "I" & IndexIdArray(i) & Chr(34)
            If i = 0 Then
                str = str & str1
                'strd = strd & str1
            Else
                str = str & ", " & str1
                'strd = strd & ", " & str1
            End If
        Next
        'Si no existe la columna, que siga borrando el resto igual
        Try
            Server.Con.ExecuteNonQuery(CommandType.Text, str & ") CASCADE CONSTRAINTS")
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
        'Try
        '    Server.Con.ExecuteNonQuery(CommandType.Text, strd & ") CASCADE CONSTRAINTS")
        'Catch ex As Exception
        '    Zamba.AppBlock.ZException.Log(ex)
        'End Try
    End Sub
    Public Shadows Sub AddIndexList(ByVal IndexId As Int64, ByVal IndexLen As Integer) Implements CreateTables.AddIndexList

        Dim str As String = "CREATE TABLE " & Chr(34) & Server.dbOwner & Chr(34) & "." & Chr(34) & "ILST_I" & IndexId & Chr(34) & " (" & Chr(34) & "ITEMID" & Chr(34) & " NUMBER(10), " & Chr(34) & "ITEM" & Chr(34) & " CHAR(" & IndexLen & ")) TABLESPACE " & Chr(34) & Server.dbOwner & Chr(34)
        ' Create the tables.
        Server.Con.ExecuteNonQuery(CommandType.Text, str)
        Dim StrPrimarykey As String = "ALTER TABLE " & Chr(34) & Server.dbOwner & Chr(34) & "." & Chr(34) & "ILST_I" & IndexId & Chr(34) & " ADD CONSTRAINT PK_ILST_I" & IndexId & " PRIMARY KEY  (ITEMID)"
        Server.Con.ExecuteNonQuery(CommandType.Text, StrPrimarykey)

    End Sub
    Public Shadows Sub InsertIndexList(ByVal IndexId As Int64, ByVal IndexList As ArrayList) Implements CreateTables.InsertIndexList

        Dim strdelete As String = "DELETE FROM " & Chr(34) & Server.dbOwner & Chr(34) & "." & Chr(34) & "ILST_I" & IndexId & Chr(34)

        ' Create the tables.
        'Del the old list
        Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
        'add the current list
        Dim i As Integer
        Dim table As String = "ILst_I" & IndexId
        For i = 0 To IndexList.Count - 1
            Dim Valuestring As String = i + 1 & ", '" & IndexList.Item(i) & "'"
            Dim strInsert As String = "INSERT INTO " & Chr(34) & Server.dbOwner & Chr(34) & "." & Chr(34) & table & Chr(34) & " (ITEMID, ITEM) Values (" & Valuestring & ")"
            Server.Con.ExecuteNonQuery(CommandType.Text, strInsert)
        Next

    End Sub
    Public Shadows Sub DelIndexList(ByVal IndexId As Int64) Implements CreateTables.DelIndexList

        Dim str As String = "DROP TABLE " & Chr(34) & Server.dbOwner & Chr(34) & "." & Chr(34) & "ILST_I" & IndexId & Chr(34)
        Server.Con.ExecuteNonQuery(CommandType.Text, str)

    End Sub
    Public Shadows Sub DelIndexItems(ByVal IndexId As Int64, ByVal IndexList As ArrayList) Implements CreateTables.DelIndexItems

        Dim i As Integer
        Dim table As String = "ILST_I" & IndexId
        For i = 0 To IndexList.Count - 1
            Dim strdelete As String = "DELETE FROM " & Chr(34) & Server.dbOwner & Chr(34) & "." & Chr(34) & table & Chr(34) & " where(ITEM = '" & IndexList(i) & "')"
            Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
        Next

    End Sub
    Public Shadows Sub DeleteTable(ByVal Table As String) Implements CreateTables.DeleteTable
        Try
            Dim strDelete As String = "DROP TABLE " & Chr(34) & Server.dbOwner & Chr(34) & "." & Chr(34) & Table & Chr(34)
            Server.Con.ExecuteNonQuery(CommandType.Text, strDelete)
        Catch ex As Exception
            If Not ex.ToString.Contains("no existe") AndAlso Not ex.ToString.Contains("not exist") Then
                Throw ex
            End If
        End Try
    End Sub
    Public Sub AddIndexColumn(ByVal DocTypeId As Int64, ByVal IndexIdArray As System.Collections.ArrayList, ByVal IndexTypeArray As System.Collections.ArrayList, ByVal IndexLenArray As System.Collections.ArrayList) Implements CreateTables.AddIndexColumn
        'Falta implementar esta funcion
    End Sub
    'Public Shadows Sub ExecuteArchivo(ByVal path As String) Implements CreateTables.ExecuteArchivo
    '    Dim app As ApplicationConfig
    '    Try
    '        app = New ApplicationConfig
    '        Dim fi As IO.FileInfo = New IO.FileInfo(path)
    '        Dim d1 As New IO.DirectoryInfo("C:\temp")
    '        If d1.Exists() = False Then
    '            d1.Create()
    '        End If

    '        If Not fi.Exists Then
    '            Throw New IO.FileNotFoundException
    '        End If
    '        Dim f2 As IO.FileInfo = fi.CopyTo("c:\temp\" & fi.Name, True)
    '        Dim str As String = "sqlplus " & app.USER & "/" & app.PASSWORD & "@" & app.DB & " @" & f2.FullName

    '        Shell(str, AppWinStyle.NormalFocus, True)
    '        f2.Delete()


    '    Finally
    '        app.dispose()
    '        app = Nothing
    '    End Try

    'End Sub
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
            Case 10
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
                If (Codigo).ToString.Trim <> "" AndAlso Descripcion.Trim <> "" Then
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
        Dim strdelete As String
        Try
            If (Codigo).ToString.Trim <> "" Then
                strdelete = "Delete From " & Tabla & " Where Codigo= " & Codigo
                Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
            End If
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Throw ex
        End Try
    End Sub

    Public Shadows Sub BorrarSustitucionTable(ByVal IndexId As Int64) Implements CreateTables.BorrarSustitucionTable
        Dim strdelete As String = "Delete from SLST_S" & IndexId
        Try
            Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
        Catch
        End Try
    End Sub
    Private Sub DelTempTables() Implements CreateTables.DelTempTables
        Try
            Dim Strdrop As String
            Dim i As Int32
            For i = 1 To 50
                Try
                    Strdrop = "DROP TABLE DOC_T" & i
                    Server.Con.ExecuteNonQuery(CommandType.Text, Strdrop)
                Catch
                End Try
                Try
                    Strdrop = "DROP TABLE DOC_I" & i
                    Server.Con.ExecuteNonQuery(CommandType.Text, Strdrop)
                Catch
                End Try
                'Try
                '    Strdrop = "DROP TABLE DOC_D" & i
                '    Server.Con.ExecuteNonQuery(CommandType.Text, Strdrop)
                'Catch
                'End Try
                'Try
                '    Strdrop = "DROP TABLE DOC_X" & i
                '    Server.Con.ExecuteNonQuery(CommandType.Text, Strdrop)
                'Catch
                'End Try
                'Try
                '    Strdrop = "DROP TABLE DOC_XD" & i
                '    Server.Con.ExecuteNonQuery(CommandType.Text, Strdrop)
                'Catch
                'End Try
            Next
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

        Catch ex As Exception
        Finally
            ds.Dispose()
        End Try
    End Sub

    Public Sub AddObsTable(DocTypeId As Long, IndexIdArray As ArrayList, IndexTypeArray As ArrayList, IndexLenArray As ArrayList) Implements CreateTables.AddObsTable
        Try
            Dim str As String = "CREATE TABLE ZOBS_" & DocTypeId & "_" & IndexIdArray(0) & "(ID NUMBER NOT NULL,DOC_ID NUMBER,USER_ID varchar2(200),DateObs DATE,VALUE varchar2(200),PRIMARY KEY (ID))"
            Server.Con.ExecuteNonQuery(CommandType.Text, str)
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Throw
        End Try
    End Sub
End Class
