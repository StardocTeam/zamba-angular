Public Enum SqlConnectionOwnership
    'Connection is owned and managed by SqlHelper
    Internal
    'Connection is owned and managed by the caller
    [External]
End Enum 'SqlConnectionOwnership


Public Interface IConnection

    Function ExecuteScalarForMigrator(commandType As CommandType, commandText As String, ParamArray commandParameters As IDbDataParameter()) As Object

    Sub dispose()

    Property CN() As IDbConnection
    Property State() As ConnectionStates

    Enum ConnectionStates
        Ready
        Executing
    End Enum

    ReadOnly Property SysDate() As String


    Property CloseFlag() As Boolean
    Sub Open()
    Function Close() As Boolean
    ReadOnly Property ConString() As String
    ReadOnly Property Command() As IDbCommand
    ReadOnly Property isOracle As Boolean
    ReadOnly Property isODBC As Boolean
    Property dbOwner As String
    Overloads Function ExecuteDataset(ByVal spName As String,
    ByVal ParametersNames() As Object, ByVal parameterstypes As Object, ByVal parameterValues() As Object) As DataSet

    Overloads Function ExecuteNonQuery(ByVal spName As String, _
    ByVal ParametersNames() As Object, ByVal parameterstypes As Object, ByVal parameterValues() As Object) As Integer



    Sub AttachParameters(ByVal command As IDbCommand, ByVal commandParameters() As IDataParameter)
    Sub AssignParameterValues(ByVal commandParameters() As IDbDataParameter, ByVal parameterValues() As Object)
    Sub PrepareCommand(ByVal command As IDbCommand, _
    ByVal transaction As IDbTransaction, _
    ByVal commandType As CommandType, _
    ByVal commandText As String, _
    ByVal commandParameters() As IDbDataParameter)
    Overloads Function ExecuteNonQuery( _
    ByVal commandType As CommandType, _
    ByVal commandText As String) As Integer
    Overloads Function ExecuteNoTimeOutNonQuery( _
ByVal commandType As CommandType, _
ByVal commandText As String, _
ByVal ParamArray commandParameters() As IDbDataParameter) As Integer
    Overloads Function ExecuteNonQuery( _
    ByVal commandType As CommandType, _
    ByVal commandText As String, _
    ByVal ParamArray commandParameters() As IDbDataParameter) As Integer
    Overloads Function ExecuteNonQuery( _
    ByVal spName As String, _
    ByVal ParamArray parameterValues() As Object) As Integer
    Overloads Function ExecuteNonQuery(ByVal transaction As IDbTransaction, _
    ByVal commandType As CommandType, _
    ByVal commandText As String) As Integer
    Overloads Function ExecuteNonQuery(ByVal transaction As IDbTransaction, _
    ByVal commandType As CommandType, _
    ByVal commandText As String, _
    ByVal ParamArray commandParameters() As IDbDataParameter) As Integer
    Overloads Function ExecuteNonQuery(ByVal transaction As IDbTransaction, _
    ByVal spName As String, _
    ByVal ParamArray parameterValues() As Object) As Integer
    Overloads Function ExecuteNonQuery(ByVal transaction As IDbTransaction, ByVal spName As String, ByVal ParametersNames As Object(), ByVal parameterstypes As Object, ByVal parametersValues As Object()) As Integer
    Overloads Function ExecuteDataset( _
   ByVal commandType As CommandType, _
   ByVal commandText As String) As DataSet
    Overloads Function ExecuteDataset( _
   ByVal commandType As CommandType, _
   ByVal commandText As String, _
   ByVal ParamArray commandParameters() As IDbDataParameter) As DataSet
    Overloads Function ExecuteDataset( _
   ByVal spName As String, _
   ByVal ParamArray parameterValues() As Object) As DataSet
    Overloads Function ExecuteDataset(ByVal transaction As IDbTransaction, _
   ByVal commandType As CommandType, _
   ByVal commandText As String) As DataSet
    Overloads Function ExecuteDataset(ByVal transaction As IDbTransaction, _
   ByVal commandType As CommandType, _
   ByVal commandText As String, _
   ByVal ParamArray commandParameters() As IDbDataParameter) As DataSet
    Overloads Function ExecuteDataset(ByVal transaction As IDbTransaction, _
   ByVal spName As String, _
   ByVal ParamArray parameterValues() As Object) As DataSet

    Overloads Function ExecuteDataset(ByVal transaction As IDbTransaction, ByVal spName As String, _
                                      ByVal ParametersNames As Object(), ByVal parameterstypes As Object, ByVal parametersValues As Object()) As DataSet

    Overloads Function ExecuteReader( _
    ByVal transaction As IDbTransaction, _
    ByVal commandType As CommandType, _
    ByVal commandText As String, _
    ByVal commandParameters() As IDbDataParameter, _
    ByVal connectionOwnership As SqlConnectionOwnership) As IDataReader
    Overloads Function ExecuteReader( _
    ByVal commandType As CommandType, _
    ByVal commandText As String) As IDataReader
    Overloads Function ExecuteReader( _
    ByVal commandType As CommandType, _
    ByVal commandText As String, _
    ByVal ParamArray commandParameters() As IDbDataParameter) As IDataReader
    Overloads Function ExecuteReader( _
    ByVal spName As String, _
    ByVal ParamArray parameterValues() As Object) As IDataReader
    Overloads Function ExecuteReader(ByVal transaction As IDbTransaction, _
    ByVal commandType As CommandType, _
    ByVal commandText As String) As IDataReader
    Overloads Function ExecuteReader(ByVal transaction As IDbTransaction, _
    ByVal commandType As CommandType, _
    ByVal commandText As String, _
    ByVal ParamArray commandParameters() As IDbDataParameter) As IDataReader
    Overloads Function ExecuteReader(ByVal transaction As IDbTransaction, _
    ByVal spName As String, _
    ByVal ParamArray parameterValues() As Object) As IDataReader
    Overloads Function ExecuteScalar( _
    ByVal commandType As CommandType, _
    ByVal commandText As String, _
    Optional ByVal timeout As Int32 = 0) As Object
    Overloads Function ExecuteScalar( _
    ByVal commandType As CommandType, _
    ByVal commandText As String, _
    ByVal ParamArray commandParameters() As IDbDataParameter) As Object
    Overloads Function ExecuteScalar( _
    ByVal commandType As CommandType, _
    ByVal commandText As String, _
    ByVal timeout As Int32, _
    ByVal ParamArray commandParameters() As IDbDataParameter) As Object
    Overloads Function ExecuteScalar( _
    ByVal spName As String, _
    ByVal ParamArray parameterValues() As Object) As Object
    Overloads Function ExecuteScalar(ByVal transaction As IDbTransaction, _
    ByVal commandType As CommandType, _
    ByVal commandText As String) As Object
    Overloads Function ExecuteScalar(ByVal transaction As IDbTransaction, _
    ByVal commandType As CommandType, _
    ByVal commandText As String, _
    ByVal ParamArray commandParameters() As IDbDataParameter) As Object
    Overloads Function ExecuteScalar(ByVal transaction As IDbTransaction, _
    ByVal spName As String, _
    ByVal ParamArray parameterValues() As Object) As Object

    Overloads Function ExecuteScalar(ByVal spName As String, _
    ByVal ParametersNames() As Object, ByVal parameterstypes As Object, ByVal parameterValues() As Object) As String

    Function ConvertDate(ByVal datetime As String, Optional byval ResolveDate As Boolean = true) As String
    Function ConvertDateTime(ByVal datetime As String) As String
    Function ConvertDateTime(ByVal datetime As DateTime) As String


End Interface