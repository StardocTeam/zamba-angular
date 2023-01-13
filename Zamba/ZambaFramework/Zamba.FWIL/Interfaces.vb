Imports System.Data.OleDb
Imports System.Collections.Generic

Namespace Servers
    Public Enum SqlConnectionOwnership
        'Connection is owned and managed by SqlHelper
        Internal
        'Connection is owned and managed by the caller
        [External]
    End Enum 'SqlConnectionOwnership

    'Public Interface IConnection

    '    Function ExecuteScalarForMigrator(commandType As CommandType, commandText As String, ParamArray commandParameters As IDbDataParameter()) As Object

    '    Sub dispose()

    '    Property CN() As IDbConnection
    '    Property State() As ConnectionStates

    '    Enum ConnectionStates
    '        Ready
    '        Executing
    '    End Enum

    '    ReadOnly Property SysDate() As String


    '    Property CloseFlag() As Boolean
    '    Sub Open()
    '    Function Close() As Boolean
    '    ReadOnly Property ConString() As String
    '    ReadOnly Property Command() As IDbCommand

    '    Overloads Function ExecuteDataset(ByVal spName As String, _
    '    ByVal ParametersNames() As Object, ByVal parameterstypes As Object, ByVal parameterValues() As Object) As DataSet

    '    Overloads Function ExecuteNonQuery(ByVal spName As String, _
    '    ByVal ParametersNames() As Object, ByVal parameterstypes As Object, ByVal parameterValues() As Object) As Integer



    '    Sub AttachParameters(ByVal command As IDbCommand, ByVal commandParameters() As IDataParameter)
    '    Sub AssignParameterValues(ByVal commandParameters() As IDbDataParameter, ByVal parameterValues() As Object)
    '    Sub PrepareCommand(ByVal command As IDbCommand, _
    '    ByVal transaction As IDbTransaction, _
    '    ByVal commandType As CommandType, _
    '    ByVal commandText As String, _
    '    ByVal commandParameters() As IDbDataParameter)
    '    Overloads Function ExecuteNonQuery( _
    '    ByVal commandType As CommandType, _
    '    ByVal commandText As String) As Integer
    '    Overloads Function ExecuteNoTimeOutNonQuery( _
    'ByVal commandType As CommandType, _
    'ByVal commandText As String, _
    'ByVal ParamArray commandParameters() As IDbDataParameter) As Integer
    '    Overloads Function ExecuteNonQuery( _
    '    ByVal commandType As CommandType, _
    '    ByVal commandText As String, _
    '    ByVal ParamArray commandParameters() As IDbDataParameter) As Integer
    '    Overloads Function ExecuteNonQuery( _
    '    ByVal spName As String, _
    '    ByVal ParamArray parameterValues() As Object) As Integer
    '    Overloads Function ExecuteNonQuery(ByVal transaction As IDbTransaction, _
    '    ByVal commandType As CommandType, _
    '    ByVal commandText As String) As Integer
    '    Overloads Function ExecuteNonQuery(ByVal transaction As IDbTransaction, _
    '    ByVal commandType As CommandType, _
    '    ByVal commandText As String, _
    '    ByVal ParamArray commandParameters() As IDbDataParameter) As Integer
    '    Overloads Function ExecuteNonQuery(ByVal transaction As IDbTransaction, _
    '    ByVal spName As String, _
    '    ByVal ParamArray parameterValues() As Object) As Integer
    '    Overloads Function ExecuteNonQuery(ByVal transaction As IDbTransaction, ByVal spName As String, ByVal ParametersNames As Object(), ByVal parameterstypes As Object, ByVal parametersValues As Object()) As Integer
    '    Overloads Function ExecuteDataset( _
    '   ByVal commandType As CommandType, _
    '   ByVal commandText As String) As DataSet
    '    Overloads Function ExecuteDataset( _
    '   ByVal commandType As CommandType, _
    '   ByVal commandText As String, _
    '   ByVal ParamArray commandParameters() As IDbDataParameter) As DataSet
    '    Overloads Function ExecuteDataset( _
    '   ByVal spName As String, _
    '   ByVal ParamArray parameterValues() As Object) As DataSet
    '    Overloads Function ExecuteDataset(ByVal transaction As IDbTransaction, _
    '   ByVal commandType As CommandType, _
    '   ByVal commandText As String) As DataSet
    '    Overloads Function ExecuteDataset(ByVal transaction As IDbTransaction, _
    '   ByVal commandType As CommandType, _
    '   ByVal commandText As String, _
    '   ByVal ParamArray commandParameters() As IDbDataParameter) As DataSet
    '    Overloads Function ExecuteDataset(ByVal transaction As IDbTransaction, _
    '   ByVal spName As String, _
    '   ByVal ParamArray parameterValues() As Object) As DataSet

    '    Overloads Function ExecuteDataset(ByVal transaction As IDbTransaction, ByVal spName As String, _
    '                                      ByVal ParametersNames As Object(), ByVal parameterstypes As Object, ByVal parametersValues As Object()) As DataSet

    '    Overloads Function ExecuteReader( _
    '    ByVal transaction As IDbTransaction, _
    '    ByVal commandType As CommandType, _
    '    ByVal commandText As String, _
    '    ByVal commandParameters() As IDbDataParameter, _
    '    ByVal connectionOwnership As SqlConnectionOwnership) As IDataReader
    '    Overloads Function ExecuteReader( _
    '    ByVal commandType As CommandType, _
    '    ByVal commandText As String) As IDataReader
    '    Overloads Function ExecuteReader( _
    '    ByVal commandType As CommandType, _
    '    ByVal commandText As String, _
    '    ByVal ParamArray commandParameters() As IDbDataParameter) As IDataReader
    '    Overloads Function ExecuteReader( _
    '    ByVal spName As String, _
    '    ByVal ParamArray parameterValues() As Object) As IDataReader
    '    Overloads Function ExecuteReader(ByVal transaction As IDbTransaction, _
    '    ByVal commandType As CommandType, _
    '    ByVal commandText As String) As IDataReader
    '    Overloads Function ExecuteReader(ByVal transaction As IDbTransaction, _
    '    ByVal commandType As CommandType, _
    '    ByVal commandText As String, _
    '    ByVal ParamArray commandParameters() As IDbDataParameter) As IDataReader
    '    Overloads Function ExecuteReader(ByVal transaction As IDbTransaction, _
    '    ByVal spName As String, _
    '    ByVal ParamArray parameterValues() As Object) As IDataReader
    '    Overloads Function ExecuteScalar( _
    '    ByVal commandType As CommandType, _
    '    ByVal commandText As String) As Object
    '    Overloads Function ExecuteScalar( _
    '    ByVal commandType As CommandType, _
    '    ByVal commandText As String, _
    '    ByVal ParamArray commandParameters() As IDbDataParameter) As Object
    '    Overloads Function ExecuteScalar( _
    '    ByVal spName As String, _
    '    ByVal ParamArray parameterValues() As Object) As Object
    '    Overloads Function ExecuteScalar(ByVal transaction As IDbTransaction, _
    '    ByVal commandType As CommandType, _
    '    ByVal commandText As String) As Object
    '    Overloads Function ExecuteScalar(ByVal transaction As IDbTransaction, _
    '    ByVal commandType As CommandType, _
    '    ByVal commandText As String, _
    '    ByVal ParamArray commandParameters() As IDbDataParameter) As Object
    '    Overloads Function ExecuteScalar(ByVal transaction As IDbTransaction, _
    '    ByVal spName As String, _
    '    ByVal ParamArray parameterValues() As Object) As Object

    '    Overloads Function ExecuteScalar(ByVal spName As String, _
    '    ByVal ParametersNames() As Object, ByVal parameterstypes As Object, ByVal parameterValues() As Object) As String

    '    Function ConvertDate(ByVal datetime As String) As String
    '    Function ConvertDateTime(ByVal datetime As String) As String


    'End Interface


    'Public Interface CreateTables
    '    Sub AddDocsTables(ByVal DocTypeId As Int64)
    '    Sub AddIndexColumn(ByVal DocTypeId As Int64, ByVal IndexIdArray As ArrayList, ByVal IndexTypeArray As ArrayList, ByVal IndexLenArray As ArrayList)
    '    Sub AddIndexList(ByVal IndexId As Int64, ByVal IndexLen As Integer)
    '    Sub DelIndexColumn(ByVal DocTypeId As Int64, ByVal IndexIdArray As ArrayList)
    '    Sub DelIndexList(ByVal IndexId As Int64)
    '    Sub DelIndexItems(ByVal IndexId As Int64, ByVal IndexList As ArrayList)
    '    Sub DeleteTable(ByVal Table As String)
    '    Sub DelTempTables()
    '    '    Sub ExecuteArchivo(ByVal path As String)
    '    Sub CreateTextIndex(ByVal DocTypeId As Int64, ByVal IndexId As Int64)
    '    Sub CreateSustitucionTable(ByVal Index As Int64, ByVal IndexLen As Int32, ByVal IndexType As Int32)
    '    Sub BulkInsertSustitucionTable(ByVal FileName As String, ByVal separador As String, ByVal IndexId As Int64)
    '    Sub ExportSustitucionTable(ByVal file As String, ByVal separador As String, ByVal IndexId As Int64)
    '    Sub InsertIntoSustitucion(ByVal Tabla As String, ByVal Codigo As Int32, ByVal Descripcion As String)
    '    Sub InsertIndexList(ByVal IndexId As Int64, ByVal IndexList As ArrayList)
    '    Sub UpdateIntoSustitucion(ByVal Tabla As String, ByVal Codigo As Int32, ByVal Descripcion As String)
    '    Sub DeleteFromSustitucion(ByVal Tabla As String, ByVal Codigo As Int32, ByVal Descripcion As String)
    '    Sub DropSustitucionTable(ByVal IndexId As Int64)
    '    Sub BorrarSustitucionTable(ByVal IndexId As Int64)
    '    Sub Dispose()
    '    'Sub CreateView(ByVal docTypeId as Int64)
    '    Sub CreateView(ByVal docTypeId As Int64, ByVal lstColKeys As Dictionary(Of String(), String), ByVal lstColIndexs As Dictionary(Of String, String), ByVal lstColSelects As Dictionary(Of String, String()))
    '    Sub DropView(ByVal DocTypeId As Int64)
    'End Interface
End Namespace

Namespace Core


End Namespace