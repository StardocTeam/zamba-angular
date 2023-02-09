Imports System.Data.Odbc
Imports System.Collections.Generic
Imports System.Text
Imports Zamba.Core
Imports Zamba

Public Class ODBCCon
    Implements IConnection, IDisposable



    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve la fecha y hora del servidor odbc. GETDATE()
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public ReadOnly Property SysDate() As String Implements IConnection.SysDate
        Get
            Return " getdate()"
        End Get
    End Property

    Private disposedValue As Boolean = False        ' To detect redundant calls

#Region " IDisposable Support "

    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                If Not IsNothing(_command) Then

                    _command.Dispose()
                    _command = Nothing
                End If
                If Not IsNothing(State) Then
                    State = Nothing
                End If
                If Not IsNothing(CN) Then
                    CN.Close()
                    CN.Dispose()
                    CN = Nothing
                End If
            End If
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose, IConnection.dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

#Region "Properties"
    Private _CN As IDbConnection
    Public ReadOnly Property ConString() As String Implements IConnection.ConString
        Get
            Return _CN.ConnectionString
        End Get
    End Property
    Private _CloseFlag As Boolean = True

    Public Property CN() As IDbConnection Implements IConnection.CN
        Get
            Return _CN
        End Get
        Set(ByVal Value As IDbConnection)
            _CN = Value
        End Set
    End Property

    Private _state As IConnection.ConnectionStates = IConnection.ConnectionStates.Ready
    Public Property State() As IConnection.ConnectionStates Implements IConnection.State
        Get
            Return _state
        End Get
        Set(ByVal Value As IConnection.ConnectionStates)
            _state = Value
        End Set
    End Property

    Private _command As OdbcCommand
    ReadOnly Property Command() As IDbCommand Implements IConnection.Command
        Get
            Return _command
        End Get
    End Property

    ''' <summary>
    ''' Abre la conexion
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Open() Implements IConnection.Open
        If CN.State = ConnectionState.Closed Then CN.Open()
    End Sub

    Public Property CloseFlag() As Boolean Implements IConnection.CloseFlag
        Get
            Return Me._CloseFlag
        End Get
        Set(ByVal Value As Boolean)
            Me._CloseFlag = Value
        End Set
    End Property

    Public ReadOnly Property isOracle As Boolean Implements IConnection.isOracle
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property isODBC As Boolean Implements IConnection.isODBC
        Get
            Return True
        End Get
    End Property

    Private duration As System.TimeSpan
    Private startTime As Date
#End Region

#Region "private utility methods & constructors"
    Public Sub New(ByVal OleconnectionString As String, ByVal DbOwner As String, Optional ByVal Flagclose As Boolean = True)
        _CN = New OdbcConnection(OleconnectionString)
        Me.CloseFlag = Flagclose
        Me.dbOwner = DbOwner
    End Sub
    Public Property dbOwner As String Implements IConnection.dbOwner


    'Since this class provides only static methods, make the default constructor private to prevent 
    'instances from being created with "new odbcHelper()".
    Public Sub New(ByVal co As IDbConnection, Optional ByVal Flagclose As Boolean = True)
        _CN = co
        Me.CloseFlag = Flagclose
    End Sub 'New

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
    ' -command - the odbcCommand to be prepared
    ' -connection - a valid odbcConnection, on which to execute this command
    ' -transaction - a valid odbcTransaction, or 'null'
    ' -commandType - the CommandType (stored procedure, text, etc.)
    ' -commandText - the stored procedure name or T-odbc command
    ' -commandParameters - an array of odbcParameters to be associated with the command or 'null' if no parameters are required
    Private Sub PrepareCommand(ByVal command As IDbCommand, _
 _
    ByVal transaction As IDbTransaction, _
    ByVal commandType As CommandType, _
    ByVal commandText As String, _
    ByVal commandParameters() As IDbDataParameter) Implements IConnection.PrepareCommand

        'if the provided connection is not open, we will open it
        If Me.CN.State <> ConnectionState.Open Then
            CN.Open()
        End If

        'associate the connection with the command
        command.Connection = CN

        'set the command text (stored procedure name or odbc statement)
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

    ''' <summary>
    ''' Cierra la conexion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Close() As Boolean Implements IConnection.Close
        Try
            CN.Close()
            Return True
        Catch ex As Exception
            Return False
        Finally
            Me.Dispose()
        End Try
    End Function

    Public Function ConvertDate(ByVal datetime As String,Optional byval ResolveDate As Boolean = true) As String Implements IConnection.ConvertDate
        Return datetime
    End Function

    Public Function ConvertDatetime(ByVal datetime As String) As String Implements IConnection.ConvertDateTime
        Return datetime
    End Function

    Public Function ConvertDatetime(ByVal datetime As DateTime) As String Implements IConnection.ConvertDateTime
        Return datetime
    End Function
#End Region

#Region "ExecuteNonQuery"
    Overloads Function ExecuteNonQuery(ByVal transaction As IDbTransaction, ByVal spName As String, ByVal ParametersNames As Object(), ByVal parameterstypes As Object, ByVal parametersValues As Object()) As Integer Implements IConnection.ExecuteNonQuery
        'TODO: implementar
    End Function

    Public Overloads Function ExecuteNonQuery(ByVal spName As String, ByVal ParametersNames() As Object, ByVal parameterstypes As Object, ByVal parametersValues() As Object) As Integer Implements IConnection.ExecuteNonQuery
        Dim cmd As OdbcCommand
        Dim log As String

        Try
            Me.State = IConnection.ConnectionStates.Executing
            Me.Open()

            cmd = New OdbcCommand
            Me._command = cmd
            cmd.Connection = CN
            cmd.CommandText = spName
            cmd.CommandType = CommandType.StoredProcedure

            Dim i As Int32
            For i = 0 To parametersValues.Length - 1
                cmd.Parameters.Add(New OdbcParameter(CStr(ParametersNames(i)), parameterstypes(i))).Value = parametersValues(i)
                log = log & " " & parametersValues(i)
            Next
            startTime = Now
            cmd.ExecuteNonQuery()
            duration = Now - startTime
            utilities.LogCommands("ODBC: " & "ODBC: " & cmd.CommandText, cmd.Parameters, duration)
            cmd.Connection.Close()
            'Debug.WriteLine(cmd.CommandText)
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(New Exception(spName & ":" & ex.ToString))
            Throw ex
        Finally
            log = Nothing
            If Me.CloseFlag Then
                If cmd IsNot Nothing Then
                    If cmd.Parameters IsNot Nothing Then
                        cmd.Parameters.Clear()
                    End If
                    cmd.Dispose()
                    cmd = Nothing
                End If

                Me.Close()
            Else
                Me.State = IConnection.ConnectionStates.Ready
            End If
        End Try
    End Function
    Public Overloads Function ExecuteNonQuery(ByVal commandType As CommandType, ByVal commandText As String) As Integer Implements IConnection.ExecuteNonQuery
        Return ExecuteNonQuery(commandType, commandText, CType(Nothing, OdbcParameter()))
    End Function
    Public Overloads Function ExecuteNonQuery(ByVal commandType As CommandType, ByVal commandText As String, _
        ByVal ParamArray commandParameters() As IDbDataParameter) As Integer Implements IConnection.ExecuteNonQuery
        Dim cmd As OdbcCommand
        Try
            Me.State = IConnection.ConnectionStates.Executing
            Me.Open()
            cmd = New OdbcCommand
            Me._command = cmd
            Dim retval As Integer
            PrepareCommand(cmd, CType(Nothing, OdbcTransaction), commandType, commandText, commandParameters)

            startTime = Now
            retval = cmd.ExecuteNonQuery()
            duration = Now - startTime
            utilities.LogCommands("ODBC: " & "ODBC: " & cmd.CommandText, cmd.Parameters, duration)
            'Debug.WriteLine(cmd.CommandText)
            cmd.Connection.Close()

            Return retval
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(New Exception(commandText & ":" & ex.ToString))
            Throw ex
        Finally
            If Me.CloseFlag Then
                If cmd IsNot Nothing Then
                    If cmd.Parameters IsNot Nothing Then
                        cmd.Parameters.Clear()
                    End If
                    cmd.Dispose()
                    cmd = Nothing
                End If


                Me.Close()
            Else
                Me.State = IConnection.ConnectionStates.Ready
            End If
        End Try
    End Function
    Public Overloads Function ExecuteNoTimeOutNonQuery(ByVal commandType As CommandType, ByVal commandText As String, _
        ByVal ParamArray commandParameters() As IDbDataParameter) As Integer Implements IConnection.ExecuteNoTimeOutNonQuery
        Dim cmd As OdbcCommand
        Try
            Me.State = IConnection.ConnectionStates.Executing
            Me.Open()
            cmd = New OdbcCommand
            cmd.CommandTimeout = 0
            Me._command = cmd
            Dim retval As Integer
            PrepareCommand(cmd, CType(Nothing, OdbcTransaction), commandType, commandText, commandParameters)

            startTime = Now
            retval = cmd.ExecuteNonQuery()
            duration = Now - startTime
            utilities.LogCommands("ODBC: " & "ODBC: " & cmd.CommandText, cmd.Parameters, duration)
            'Debug.WriteLine(cmd.CommandText)
            cmd.Connection.Close()

            Return retval
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(New Exception(commandText & ":" & ex.ToString))
            Throw ex
        Finally
            If Me.CloseFlag Then
                If cmd IsNot Nothing Then
                    If cmd.Parameters IsNot Nothing Then
                        cmd.Parameters.Clear()
                    End If
                    cmd.Dispose()
                    cmd = Nothing
                End If


                Me.Close()
            Else
                Me.State = IConnection.ConnectionStates.Ready
            End If
        End Try
    End Function
    Public Overloads Function ExecuteNonQuery(ByVal spName As String, ByVal ParamArray parameterValues() As Object) As Integer Implements IConnection.ExecuteNonQuery
        Dim commandParameters As IDbDataParameter()
        Try
            If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
                AssignParameterValues(commandParameters, parameterValues)
                Return ExecuteNonQuery(CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteNonQuery(CommandType.StoredProcedure, spName)
            End If
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(New Exception(spName & ":" & ex.ToString))
            Throw ex
        Finally
            If commandParameters IsNot Nothing Then
                For Each cmp As OdbcParameter In commandParameters
                    cmp = Nothing
                Next
                commandParameters = Nothing
            End If
        End Try
    End Function
    Public Overloads Function ExecuteNonQuery(ByVal transaction As IDbTransaction, ByVal commandType As CommandType, ByVal commandText As String) As Integer Implements IConnection.ExecuteNonQuery
        Return ExecuteNonQuery(transaction, commandType, commandText, CType(Nothing, OdbcParameter()))
    End Function
    Public Overloads Function ExecuteNonQuery(ByVal transaction As IDbTransaction, ByVal commandType As CommandType, ByVal commandText As String, _
   ByVal ParamArray commandParameters() As IDbDataParameter) As Integer Implements IConnection.ExecuteNonQuery
        Dim cmd As OdbcCommand
        Try
            Me.State = IConnection.ConnectionStates.Executing
            Me.Open()
            cmd = New OdbcCommand
            Me._command = cmd
            Dim retval As Integer
            PrepareCommand(cmd, transaction, commandType, commandText, commandParameters)

            startTime = Now
            retval = cmd.ExecuteNonQuery()
            duration = Now - startTime
            utilities.LogCommands("ODBC: " & "ODBC: " & cmd.CommandText, cmd.Parameters, duration)
            'Debug.WriteLine(cmd.CommandText.ToString)
            cmd.Connection.Close()

            Return retval
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(New Exception(commandText & ":" & ex.ToString))
            Throw ex
        Finally
            If Me.CloseFlag Then
                If cmd IsNot Nothing Then
                    If cmd.Parameters IsNot Nothing Then
                        cmd.Parameters.Clear()
                    End If
                    cmd.Dispose()
                    cmd = Nothing
                End If

                Me.Close()
            Else
                Me.State = IConnection.ConnectionStates.Ready
            End If
        End Try
    End Function
    Public Overloads Function ExecuteNonQuery(ByVal transaction As IDbTransaction, ByVal spName As String, ByVal ParamArray parameterValues() As Object) As Integer Implements IConnection.ExecuteNonQuery
        Dim commandParameters As IDbDataParameter()
        If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
            AssignParameterValues(commandParameters, parameterValues)
            Return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, commandParameters)
        Else
            Return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName)
        End If
    End Function
#End Region

#Region "ExecuteDataset"
    Public Overloads Function ExecuteDataset(ByVal commandType As CommandType, _
    ByVal commandText As String) As DataSet Implements IConnection.ExecuteDataset
        Return ExecuteDataset(commandType, commandText, CType(Nothing, OdbcParameter()))
    End Function
    Public Overloads Function ExecuteDataset(ByVal commandType As CommandType, _
    ByVal commandText As String, ByVal ParamArray commandParameters() As IDbDataParameter) As DataSet Implements IConnection.ExecuteDataset

        Dim cmd As OdbcCommand
        Dim da As OdbcDataAdapter
        Try
            Me.State = IConnection.ConnectionStates.Executing
            Me.Open()
            cmd = New OdbcCommand
            Me._command = cmd
            Dim ds As New DataSet
            PrepareCommand(cmd, CType(Nothing, OdbcTransaction), commandType, commandText, commandParameters)
            da = New OdbcDataAdapter(cmd)
            'ZTrace.WriteLineIf(ZTrace.IsInfo,cmd.CommandText)
            startTime = Now
            da.Fill(ds)
            duration = Now - startTime
            utilities.LogCommands("ODBC: " & cmd.CommandText, cmd.Parameters, duration)
            cmd.Connection.Close()

            Return ds
        Catch ex As System.InvalidOperationException
            Zamba.AppBlock.ZException.Log(New Exception(commandText & ":" & ex.ToString))
            Me.State = IConnection.ConnectionStates.Ready
            Me.Close()
            Me.Open()
            Return Me.ExecuteDataset(commandType, commandText, commandParameters)
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(New Exception(commandText & ":" & ex.ToString))
            Throw ex
        Finally
            If Me.CloseFlag Then
                If da IsNot Nothing Then
                    da.Dispose()
                    da = Nothing
                End If

                If cmd IsNot Nothing Then
                    If cmd.Parameters IsNot Nothing Then
                        cmd.Parameters.Clear()
                    End If
                    cmd.Dispose()
                    cmd = Nothing
                End If


                Me.Close()
            Else
                Me.State = IConnection.ConnectionStates.Ready
            End If
        End Try
    End Function
    Public Overloads Function ExecuteDataset(ByVal spName As String, _
    ByVal ParamArray parameterValues() As Object) As DataSet Implements IConnection.ExecuteDataset
        Dim commandParameters As IDbDataParameter()
        Try
            If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
                AssignParameterValues(commandParameters, parameterValues)
                Return ExecuteDataset(CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteDataset(CommandType.StoredProcedure, spName)
            End If
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(New Exception(spName & ":" & ex.ToString))
            Throw ex
        Finally
            If commandParameters IsNot Nothing Then
                For Each cmp As OdbcParameter In commandParameters
                    cmp = Nothing
                Next
                commandParameters = Nothing
            End If
        End Try
    End Function
    Public Overloads Function ExecuteDataset(ByVal transaction As IDbTransaction, _
    ByVal commandType As CommandType, _
    ByVal commandText As String) As DataSet Implements IConnection.ExecuteDataset
        Return ExecuteDataset(transaction, commandType, commandText, CType(Nothing, OdbcParameter()))
    End Function
    Public Overloads Function ExecuteDataset(ByVal transaction As IDbTransaction, _
    ByVal commandType As CommandType, _
    ByVal commandText As String, _
    ByVal ParamArray commandParameters() As IDbDataParameter) As DataSet Implements IConnection.ExecuteDataset
        Dim cmd As OdbcCommand
        Dim da As OdbcDataAdapter
        Try
            Me.State = IConnection.ConnectionStates.Executing
            Me.Open()
            cmd = New OdbcCommand
            Me._command = cmd
            Dim ds As New DataSet
            PrepareCommand(cmd, transaction, commandType, commandText, commandParameters)
            da = New OdbcDataAdapter(cmd)
            da.Fill(ds)

            Return ds
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(New Exception(commandText & ":" & ex.ToString))
            Throw ex
        Finally
            If Me.CloseFlag Then
                If da IsNot Nothing Then
                    da.Dispose()
                    da = Nothing
                End If

                If cmd IsNot Nothing Then
                    If cmd.Parameters IsNot Nothing Then
                        cmd.Parameters.Clear()
                    End If
                    cmd.Dispose()
                    cmd = Nothing
                End If

                Me.Close()
            Else
                Me.State = IConnection.ConnectionStates.Ready
            End If
        End Try

    End Function
    Public Overloads Function ExecuteDataset(ByVal transaction As IDbTransaction, _
    ByVal spName As String, _
    ByVal ParamArray parameterValues() As Object) As DataSet Implements IConnection.ExecuteDataset
        Dim commandParameters As IDbDataParameter()
        If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
            AssignParameterValues(commandParameters, parameterValues)
            Return ExecuteDataset(transaction, CommandType.StoredProcedure, spName, commandParameters)
        Else
            Return ExecuteDataset(transaction, CommandType.StoredProcedure, spName)
        End If
    End Function
    Public Overloads Function ExecuteDataset(ByVal spName As String, _
ByVal ParametersNames() As Object, ByVal parameterstypes As Object, ByVal parameterValues() As Object) As DataSet Implements IConnection.ExecuteDataset
        Me.State = IConnection.ConnectionStates.Ready
        'TODO:Implementar
        Return Nothing
    End Function

    Overloads Function ExecuteDataset(ByVal transaction As IDbTransaction, ByVal spName As String, _
                                  ByVal ParametersNames As Object(), ByVal parameterstypes As Object, ByVal parametersValues As Object()) As DataSet Implements IConnection.ExecuteDataset
        Return Nothing
        'TODO: Implementar
    End Function
#End Region

#Region "ExecuteReader"
    Private Overloads Function ExecuteReader( _
    ByVal transaction As IDbTransaction, _
    ByVal commandType As CommandType, _
    ByVal commandText As String, _
    ByVal commandParameters() As IDbDataParameter, _
    ByVal connectionOwnership As SqlConnectionOwnership) As IDataReader Implements IConnection.ExecuteReader
        Dim cmd As OdbcCommand
        Dim dr As IDataReader

        Try
            Me.State = IConnection.ConnectionStates.Executing
            cmd = New OdbcCommand
            Me._command = cmd

            PrepareCommand(cmd, transaction, commandType, commandText, commandParameters)
            startTime = Now
            If connectionOwnership = SqlConnectionOwnership.External Then
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                'Debug.WriteLine(cmd.CommandText)
                cmd.Connection.Close()
            Else
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                'Debug.WriteLine(cmd.CommandText)
                cmd.Connection.Close()
            End If
            duration = Now - startTime
            utilities.LogCommands("ODBC: " & cmd.CommandText, cmd.Parameters, duration)

            Me.State = IConnection.ConnectionStates.Ready
            Return dr
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(New Exception(commandText & ":" & ex.ToString))
            Me.State = IConnection.ConnectionStates.Ready
            If Me.CloseFlag Then
                If cmd IsNot Nothing Then
                    If cmd.Parameters IsNot Nothing Then
                        cmd.Parameters.Clear()
                    End If
                    cmd.Dispose()
                    cmd = Nothing
                End If


                Me.Close()
            End If
            Throw ex
        End Try
    End Function 'ExecuteReader
    Public Overloads Function ExecuteReader(ByVal commandType As CommandType, ByVal commandText As String) As IDataReader Implements IConnection.ExecuteReader
        Return ExecuteReader(commandType, commandText, CType(Nothing, OdbcParameter()))
    End Function 'ExecuteReader
    Public Overloads Function ExecuteReader(ByVal commandType As CommandType, _
   ByVal commandText As String, ByVal ParamArray commandParameters() As IDbDataParameter) As IDataReader Implements IConnection.ExecuteReader
        Return ExecuteReader(CType(Nothing, OdbcTransaction), commandType, commandText, commandParameters, SqlConnectionOwnership.External)
    End Function 'ExecuteReader
    Public Overloads Function ExecuteReader( _
   ByVal spName As String, _
   ByVal ParamArray parameterValues() As Object) As IDataReader Implements IConnection.ExecuteReader
        Me.State = IConnection.ConnectionStates.Executing
        Dim commandParameters As IDbDataParameter()
        If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
            'commandParameters = odbcHelperParameterCache.GetSpParameterSet(Me.CN, CN.ConnectionString, spName)
            AssignParameterValues(commandParameters, parameterValues)
            Me.State = IConnection.ConnectionStates.Ready
            Return ExecuteReader(CommandType.StoredProcedure, spName, commandParameters)
        Else
            Me.State = IConnection.ConnectionStates.Ready
            Return ExecuteReader(CommandType.StoredProcedure, spName)
        End If
    End Function 'ExecuteReader
    Public Overloads Function ExecuteReader(ByVal transaction As IDbTransaction, _
   ByVal commandType As CommandType, _
   ByVal commandText As String) As IDataReader Implements IConnection.ExecuteReader
        Return ExecuteReader(transaction, commandType, commandText, CType(Nothing, OdbcParameter()))
    End Function 'ExecuteReader
    Public Overloads Function ExecuteReader(ByVal transaction As IDbTransaction, _
   ByVal commandType As CommandType, _
   ByVal commandText As String, _
   ByVal ParamArray commandParameters() As IDbDataParameter) As IDataReader Implements IConnection.ExecuteReader
        Return ExecuteReader(transaction, commandType, commandText, commandParameters, SqlConnectionOwnership.External)
    End Function 'ExecuteReader
    Public Overloads Function ExecuteReader(ByVal transaction As IDbTransaction, _
   ByVal spName As String, _
   ByVal ParamArray parameterValues() As Object) As IDataReader Implements IConnection.ExecuteReader
        Me.State = IConnection.ConnectionStates.Executing
        Dim commandParameters As IDbDataParameter()
        If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
            'commandParameters = odbcHelperParameterCache.GetSpParameterSet(Me.CN, transaction, transaction.Connection.ConnectionString, spName)
            AssignParameterValues(commandParameters, parameterValues)
            Me.State = IConnection.ConnectionStates.Ready
            Return ExecuteReader(transaction, CommandType.StoredProcedure, spName, commandParameters)
        Else
            Me.State = IConnection.ConnectionStates.Ready
            Return ExecuteReader(transaction, CommandType.StoredProcedure, spName)
        End If
    End Function 'ExecuteReader
#End Region

#Region "ExecuteScalar"
    Public Overloads Function ExecuteScalar( _
   ByVal commandType As CommandType, _
   ByVal commandText As String, _
   Optional ByVal timeout As Int32 = 0) As Object Implements IConnection.ExecuteScalar
        Return ExecuteScalar(commandType, commandText, timeout, CType(Nothing, OdbcParameter()))
    End Function

    Public Overloads Function ExecuteScalar( _
ByVal commandType As CommandType, _
ByVal commandText As String, _
ByVal ParamArray commandParameters() As IDbDataParameter) As Object Implements IConnection.ExecuteScalar

        Return ExecuteScalar(commandType, commandText, 0, commandParameters)

    End Function

    Public Overloads Function ExecuteScalar( _
   ByVal commandType As CommandType, _
   ByVal commandText As String, _
   ByVal timeout As Int32, _
   ByVal ParamArray commandParameters() As IDbDataParameter) As Object Implements IConnection.ExecuteScalar
        Dim cmd As OdbcCommand

        Try
            Me.State = IConnection.ConnectionStates.Executing
            Me.Open()
            cmd = New OdbcCommand
            Me._command = cmd
            Dim retval As Object
            PrepareCommand(cmd, CType(Nothing, OdbcTransaction), commandType, commandText, commandParameters)
            If timeout <> 0 Then cmd.CommandTimeout = timeout
            startTime = Now
            retval = cmd.ExecuteScalar()
            duration = Now - startTime
            utilities.LogCommands("ODBC: " & cmd.CommandText, cmd.Parameters, duration)
            'Debug.WriteLine(cmd.CommandText)
            cmd.Connection.Close()

            Return retval
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(New Exception(commandText & ":" & ex.ToString))
            Throw ex
        Finally
            If Me.CloseFlag Then
                If cmd IsNot Nothing Then
                    If cmd.Parameters IsNot Nothing Then
                        cmd.Parameters.Clear()
                    End If
                    cmd.Dispose()
                    cmd = Nothing
                End If

                Me.Close()
            Else
                Me.State = IConnection.ConnectionStates.Ready
            End If
        End Try
    End Function
    Public Overloads Function ExecuteScalar( _
   ByVal spName As String, _
   ByVal ParamArray parameterValues() As Object) As Object Implements IConnection.ExecuteScalar
        Dim commandParameters As IDbDataParameter()
        Try
            Me.Open()

            If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
                AssignParameterValues(commandParameters, parameterValues)
                Return ExecuteScalar(CommandType.StoredProcedure, spName, commandParameters)
            Else
                Return ExecuteScalar(CommandType.StoredProcedure, spName)
            End If
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(New Exception(spName & ":" & ex.ToString))
            Throw ex
        Finally
            If commandParameters IsNot Nothing Then
                For Each cmp As OdbcParameter In commandParameters
                    cmp = Nothing
                Next
                commandParameters = Nothing
            End If
        End Try
    End Function
    Public Overloads Function ExecuteScalar(ByVal transaction As IDbTransaction, _
   ByVal commandType As CommandType, _
   ByVal commandText As String) As Object Implements IConnection.ExecuteScalar
        Return ExecuteScalar(transaction, commandType, commandText, CType(Nothing, OdbcParameter()))
    End Function
    Public Overloads Function ExecuteScalar(ByVal transaction As IDbTransaction, _
   ByVal commandType As CommandType, _
   ByVal commandText As String, _
   ByVal ParamArray commandParameters() As IDbDataParameter) As Object Implements IConnection.ExecuteScalar
        Dim cmd As OdbcCommand

        Try
            Me.State = IConnection.ConnectionStates.Executing
            Me.Open()
            cmd = New OdbcCommand
            Me._command = cmd
            Dim retval As Object
            PrepareCommand(cmd, transaction, commandType, commandText, commandParameters)

            startTime = Now
            retval = cmd.ExecuteScalar()
            duration = Now - startTime
            utilities.LogCommands("ODBC: " & cmd.CommandText, cmd.Parameters, duration)
            'Debug.WriteLine(cmd.CommandText)
            cmd.Connection.Close()

            Return retval
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(New Exception(commandText & ":" & ex.ToString))
            Throw ex
        Finally
            If Me.CloseFlag Then
                If cmd IsNot Nothing Then
                    If cmd.Parameters IsNot Nothing Then
                        cmd.Parameters.Clear()
                    End If
                    cmd.Dispose()
                    cmd = Nothing
                End If


                Me.Close()
            Else
                Me.State = IConnection.ConnectionStates.Ready
            End If
        End Try
    End Function
    Public Overloads Function ExecuteScalar(ByVal transaction As IDbTransaction, _
   ByVal spName As String, _
   ByVal ParamArray parameterValues() As Object) As Object Implements IConnection.ExecuteScalar
        Dim commandParameters As IDbDataParameter()
        If Not (parameterValues Is Nothing) And parameterValues.Length > 0 Then
            'pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            'commandParameters = odbcHelperParameterCache.GetSpParameterSet(Me.CN, transaction, transaction.Connection.ConnectionString, spName)
            AssignParameterValues(commandParameters, parameterValues)
            Return ExecuteScalar(transaction, CommandType.StoredProcedure, spName, commandParameters)
        Else
            Return ExecuteScalar(transaction, CommandType.StoredProcedure, spName)
        End If
    End Function
    Public Overloads Function ExecuteScalar(ByVal spName As String, _
  ByVal ParametersNames() As Object, ByVal parameterstypes As Object, ByVal parametersValues() As Object) As String Implements IConnection.ExecuteScalar
        'TODO:falta implementar similar
        Return Nothing
    End Function
#End Region

    Public Function ExecuteScalarForMigrator(commandType As System.Data.CommandType, commandText As String, ParamArray commandParameters() As System.Data.IDbDataParameter) As Object Implements IConnection.ExecuteScalarForMigrator

    End Function
End Class

Public Class odbc7CreateTables
    Implements CreateTables

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Crea las tablas del DocType, DOC_I, DOC_T. La Vista DOC y los triggers respectivos para el DocType especifico
    ''' </summary>
    ''' <param name="DocTypeId"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shadows Sub AddDocsTables(ByVal DocTypeId As Int64) Implements CreateTables.AddDocsTables
        Try
            Dim str As String = "CREATE TABLE [DOC_T" & DocTypeId & "] ([DOC_ID] [numeric] PRIMARY KEY, [FOLDER_ID] [numeric],[DISK_GROUP_ID] [int] NULL, [PLATTER_ID] [int] NULL, [VOL_ID] [int] NULL, [DOC_FILE] [nchar] (200) NULL, [OFFSET] [int] NULL, [DOC_TYPE_ID] [int] NULL, [NAME] [nvarchar] (255) NULL, [ICON_ID] [INT], [SHARED] [INT] , [Original_filename] [nvarchar] (255),[ver_Parent_id] [numeric],[version] int,[RootId] NUMERIC,[NumeroVersion] int, [FileSize] decimal) ON [PRIMARY]"
            Server.Con.ExecuteNonQuery(CommandType.Text, str)

            str = "CREATE TABLE [DOC_I" & DocTypeId & "] ([lupdate] [datetime],[crdate] [datetime] ,[Fecha] smalldatetime default(GetDate()),[DOC_ID] [numeric] foreign key(doc_id) primary key (doc_id) references doc_t" & DocTypeId & "(doc_id)ON DELETE CASCADE) ON [PRIMARY];"
            Server.Con.ExecuteNonQuery(CommandType.Text, str)

            str = "CREATE TRIGGER 	tu_i" & DocTypeId & "	ON	doc_i" & DocTypeId & "	FOR UPDATE AS BEGIN update	doc_i" & DocTypeId & " set lupdate=getdate() where doc_id in (select doc_id from inserted) END"
            Server.Con.ExecuteNonQuery(CommandType.Text, str)

            str = "CREATE TRIGGER 	ti_" & DocTypeId & " ON doc_i" & DocTypeId & "	FOR insert AS BEGIN update doc_i" & DocTypeId & " set lupdate=getdate(),crdate=getdate() where doc_id in (select doc_id from inserted) END"
            Server.Con.ExecuteNonQuery(CommandType.Text, str)

            'str = "CREATE TABLE [DOC_D" & DocTypeId & "] ([INDEX_ID] [numeric] NOT NULL, [INDEX_NAME] [nvarchar] (30) PRIMARY KEY, [INDEX_CREATED] [bit] NOT NULL) ON [PRIMARY]"
            'Server.Con.ExecuteNonQuery(CommandType.Text, str)

            CreateView(DocTypeId)
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Throw ex
        End Try

    End Sub

    Private Sub CreateView(ByVal docTypeId As Int64) 'Implements CreateTables.CreateView
        Dim Str As String
        Try
            'Str = "DROP View DOC" & DocTypeId
            Str = "if exists(select * from sysobjects where xtype='V' and name ='doc" & DocTypeId & "') begin drop view doc" & DocTypeId & " end"
            Server.Con.ExecuteNonQuery(CommandType.Text, Str)
        Catch ex As Exception
        End Try
        Try
            Str = "CREATE VIEW DOC" & DocTypeId & " AS SELECT DOC_I" & DocTypeId & ".*, DOC_T" & DocTypeId & ".FOLDER_ID As FOLDER_ID, DOC_T" & DocTypeId & ".DISK_GROUP_ID AS DISK_GROUP_ID, DOC_T" & DocTypeId & ".PLATTER_ID AS PLATTER_ID, DOC_T" & DocTypeId & ".VOL_ID AS VOL_ID, DOC_T" & DocTypeId & ".DOC_FILE AS DOC_FILE, " _
               & "DOC_T" & DocTypeId & ".OFFSET AS OFFSET, DOC_T" & DocTypeId & ".DOC_TYPE_ID AS DOC_TYPE_ID, DOC_T" & DocTypeId & ".NAME AS NAME, DOC_T" & DocTypeId & ".ICON_ID AS ICON_ID, DOC_T" & DocTypeId & ".SHARED AS SHARED, " _
               & "DOC_T" & DocTypeId & ".Original_filename AS Original_filename, DOC_T" & DocTypeId & ".ver_Parent_id AS ver_Parent_id, DOC_T" & DocTypeId & ".version AS version, DOC_T" & DocTypeId & ".RootId AS RootId, DOC_T" & DocTypeId & ".NumeroVersion AS NumeroVersion " _
               & " FROM DOC_I" & DocTypeId & " INNER JOIN DOC_T" & DocTypeId & " ON DOC_I" & DocTypeId & ".DOC_ID = DOC_T" & DocTypeId & ".DOC_ID"
            Server.Con.ExecuteNonQuery(CommandType.Text, Str)
        Catch ex As Exception
        End Try
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
    Private Sub CreateView(ByVal docTypeId As Int64, ByVal lstColKeys As Dictionary(Of String(), String), ByVal lstColIndexs As Dictionary(Of String, String), ByVal lstColSelects As Dictionary(Of String, String())) Implements CreateTables.CreateView
        Dim Str As String
        Dim strBuilder As StringBuilder = New StringBuilder()
        Try
            'Elimino la vista anterior
            Str = "if exists(select * from sysobjects where xtype='V' and name ='doc" & docTypeId & "') begin drop view doc" & docTypeId & " end"
            Server.Con.ExecuteNonQuery(CommandType.Text, Str)
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
            Dim strodbc As String = strBuilder.ToString()
            For Each tabla As KeyValuePair(Of String, String) In tables
                strodbc = strodbc.Replace(tabla.Key & ".", tabla.Value & ".")
            Next

            Server.Con.ExecuteNonQuery(CommandType.Text, strodbc)
        Finally
            strBuilder = Nothing
        End Try
    End Sub
    Private Sub DropView(ByVal DocTypeId As Int64) Implements CreateTables.DropView
        'Dim Str As String = "DROP VIEW DOC" & DocTypeId
        Dim Str As String = "if exists(select * from sysobjects where xtype='V' and name ='doc" & DocTypeId & "') begin drop view doc" & DocTypeId & " end"
        Server.Con.ExecuteNonQuery(CommandType.Text, Str)
    End Sub
    Public Shadows Sub CreateTextIndex(ByVal DocTypeId As Int64, ByVal IndexId As Int64) Implements CreateTables.CreateTextIndex

        Dim str As String = "CREATE TABLE [DOC_X" & DocTypeId & "] ([DOC_ID] [int], [ID] [int]) ON [PRIMARY];"
        Server.Con.ExecuteNonQuery(CommandType.Text, str)
        str = "CREATE INDEX [IX_DOC_X" & DocTypeId & "] ON [DOC_X" & DocTypeId & "]([ID]) ON [PRIMARY]"
        Server.Con.ExecuteNonQuery(CommandType.Text, str)
        str = "CREATE TABLE [DOC_XD" & DocTypeId & "] ([ID] [int], [WORD] [nvarchar] (50) PRIMARY KEY) ON [PRIMARY];"
        Server.Con.ExecuteNonQuery(CommandType.Text, str)
        Dim StrUpdate As String = "UPDATE DOC_TYPE SET DocumentalID = " & IndexId & " where DOC_TYPE_ID = " & DocTypeId
        Server.Con.ExecuteNonQuery(CommandType.Text, StrUpdate)

    End Sub
    Public Shadows Sub CreateSustitucionTable(ByVal Index As Int64, ByVal IndexLen As Int32, ByVal IndexType As Int32) Implements CreateTables.CreateSustitucionTable
        Dim strType As String

        Select Case IndexType
            Case 1
                strType = "NUMERIC(9)"
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
        Dim strupdate As String = ""
        Dim DsSelect As DataSet
        Dim strselect As String = "Select count(1) from " & Tabla & " Where Codigo=" & Codigo
        Try
            DsSelect = Server.Con.ExecuteDataset(CommandType.Text, strselect)
            If DsSelect.Tables(0).Rows(0).Item(0) > 0 Then
                If (Codigo).ToString.Trim <> "" AndAlso Descripcion.Trim <> "" Then
                    strupdate = "Update " & Tabla & " Set descripcion='" & Replace(Descripcion, "'", " ") & "' where Codigo= " & Codigo
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
                If Codigo.ToString.Trim <> "" AndAlso Descripcion.Trim <> "" Then
                    strinsert = "Insert into " & Tabla & " Values(" & Codigo & ",'" & Replace(Descripcion, "'", " ") & "')"
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

        Server.Con.ExecuteNonQuery(CommandType.Text, strdrop)

    End Sub
    Public Shadows Sub BorrarSustitucionTable(ByVal IndexId As Int64) Implements CreateTables.BorrarSustitucionTable
        Dim strdelete As String = "Delete from [SLST_S" & IndexId & "]"

        Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)

    End Sub
#Region "Como estaba antes AddIndexColumn"
    'Try
    '    Dim str As String = "ALTER TABLE DOC_I" & DocTypeId & " ADD ("
    '    Dim i As Integer
    '    Dim str1 As String
    '    For i = 0 To IndexIdArray.Count - 1
    '        Dim type As Integer = IndexTypeArray(i)

    '        Select Case type
    '            Case 1
    '                str1 = "I" & IndexIdArray(i) & " int NULL"
    '            Case 2
    '                str1 = "I" & IndexIdArray(i) & " numeric NULL"
    '            Case 3
    '                str1 = "I" & IndexIdArray(i) & " decimal(12,3) NULL"
    '                '     str1 = "I" & IndexIdArray(i) & " float NULL"
    '            Case 4
    '                str1 = "I" & IndexIdArray(i) & " smalldatetime NULL"
    '            Case 5
    '                str1 = "I" & IndexIdArray(i) & " smalldatetime NULL"
    '            Case 6
    '                str1 = "I" & IndexIdArray(i) & " money NULL"
    '            Case 7
    '                str1 = "I" & IndexIdArray(i) & " varchar (" & IndexLenArray(i) & ") NULL"
    '            Case 8
    '                str1 = "I" & IndexIdArray(i) & " varchar (" & IndexLenArray(i) & ") NULL"
    '        End Select
    '        If i = 0 Then
    '            str = str & str1
    '        Else
    '            str = str & ", " & str1
    '        End If
    '    Next
    '    str &= ")"
    '    Server.Con.ExecuteNonQuery( CommandType.Text, str)
    '    For i = 0 To IndexIdArray.Count - 1
    '        Dim StrIndex As String = "CREATE INDEX [" & "I" & IndexIdArray(i) & "] ON [DOC_I" & DocTypeId & "] ([I" & IndexIdArray(i) & "]) ON [PRIMARY]"
    '        Server.Con.ExecuteNonQuery( CommandType.Text, StrIndex)
    '    Next
    'Catch ex As Exception
    '    Throw New Exception("In exception handler: " & ex.Message)
    'Finally
    '    'con.close()
    '    'con.dispose()
    '    'con = nothing
    'End Try
#End Region
    Public Shadows Sub AddIndexColumn(ByVal DocTypeId As Int64, ByVal IndexIdArray As ArrayList, ByVal IndexTypeArray As ArrayList, ByVal IndexLenArray As ArrayList) Implements CreateTables.AddIndexColumn

        Dim str As String = "ALTER TABLE DOC_I" & DocTypeId & " ADD "
        'Dim strd As String = "ALTER TABLE DOC_D" & DocTypeId & " ADD "
        Dim i As Integer = 0
        Dim str1 As String = ""
        Dim strd1 As String = ""
        For i = 0 To IndexIdArray.Count - 1
            Dim type As Integer = IndexTypeArray(i)

            Select Case type
                Case 1
                    str1 = "I" & IndexIdArray(i) & " int NULL"
                Case 2
                    str1 = "I" & IndexIdArray(i) & " NUMERIC NULL"
                Case 3
                    'str1 = "I" & IndexIdArray(i) & " decimal(12,3) NULL"
                    '[Sebastian 02-09-09] se cambio la precision del decimal y la escala
                    str1 = "I" & IndexIdArray(i) & " decimal(18,2) NULL"
                Case 4
                    'Cambio realizado: El tipo de datos antes era CHAR y paso a date
                    str1 = "I" & IndexIdArray(i) & " datetime "
                Case 5
                    'Cambio realizado: El tipo de datos antes era CHAR y paso a datetime
                    str1 = "I" & IndexIdArray(i) & " datetime "
                Case 6
                    '[Sebastian 02-09-09] se cambio la precision del tipo de dato moneda y la escala
                    str1 = "I" & IndexIdArray(i) & " decimal(18,2) NULL"
                Case 7
                    str1 = "I" & IndexIdArray(i) & " VARCHAR(" & IndexLenArray(i) & ") NULL"
                Case 8
                    str1 = "I" & IndexIdArray(i) & " VARCHAR(" & IndexLenArray(i) & ") NULL"
                Case 9
                    str1 = "I" & IndexIdArray(i) & " NUMERIC NOT NULL DEFAULT 0"
            End Select
            strd1 = "D" & IndexIdArray(i) & " Numeric"
            If i = 0 Then
                str = str & str1
                'strd = strd & strd1
            Else
                str = str & ", " & str1
                'strd = strd & ", " & strd1
            End If
        Next
        'str &= ")"
        'strd &= ")"

        'Server.Con.ExecuteNonQuery(CommandType.Text, str)
        'Server.Con.ExecuteNonQuery(CommandType.Text, strd)
        'Metemos las dos consultas dentro de una transaccion
        'Dim odbc As New System.Text.StringBuilder
        'odbc.Append("BEGIN TRAN ")
        'odbc.Append(str)
        'odbc.Append(" IF @@Erro r= 0 ")
        'odbc.Append(strd)
        'odbc.Append(" if @@error <> 0 begin print 'Transacción AddIndexColumn Terminada' rollback tran end else commit tran")
        'Server.Con.ExecuteNonQuery(odbc.ToString)

        Dim c As IConnection
        Dim t As IDbTransaction = Nothing
        Try
            c = Server.Con
            c.Open()
            c.CloseFlag = False
            t = c.CN.BeginTransaction
            c.ExecuteNonQuery(t, CommandType.Text, str)
            'c.ExecuteNonQuery(t, CommandType.Text, strd)
            t.Commit()
            c.CN.Close()

            'Si el atributo es slst y el objeto slst es una tabla generá el Atributo de base de datos
            Dim indexId, query As String
            For i = 0 To IndexIdArray.Count - 1
                indexId = IndexIdArray(i).ToString
                query = "select count(1) from sysobjects where xtype='u' and name='slst_s" & indexId & "'"
                If c.ExecuteScalar(CommandType.Text, query) = 1 Then
                    Try
                        query = "ALTER TABLE DOC_I" & DocTypeId & " ADD CONSTRAINT FK_DOC_T" & DocTypeId & "_SLST_S" & indexId & " FOREIGN KEY(I" & indexId & ") REFERENCES SLST_S" & indexId & "(CODIGO) ON UPDATE NO ACTION ON DELETE NO ACTION"
                        c.ExecuteNonQuery(CommandType.Text, query)
                    Catch
                    End Try
                End If
            Next

        Catch ex As Exception
            t.Rollback()
            Dim exn As New Exception("No se pudo realizar la transacción AddIndexColumn, error: " & ex.Message)
            Throw exn
        End Try
    End Sub
    Public Shadows Sub DelIndexColumn(ByVal DocTypeId As Int64, ByVal IndexIdArray As ArrayList) Implements CreateTables.DelIndexColumn

        Dim i As Integer
        Dim ColDefault As String()
        Dim ds As New DataSet
        ds = Server.Con.ExecuteDataset(CommandType.Text, "EXEC SP_HELPCONSTRAINT DOC_I" & DocTypeId)
        For Each dtr As DataRow In ds.Tables(1).Rows
            Dim a As String
            a = dtr.Item(0)
            ColDefault = a.Split(" ")
            For i = 0 To IndexIdArray.Count - 1
                Dim arrayindex As String
                arrayindex = "I" & IndexIdArray(i)

                For Each dsindex As String In ColDefault
                    If arrayindex = dsindex Then
                        Dim constraint As String = dtr.Item(1)
                        Server.Con.ExecuteNonQuery(CommandType.Text, "ALTER TABLE DOC_I" & DocTypeId & " DROP CONSTRAINT " & constraint)
                    End If
                Next
            Next
        Next

        'For Each dtr As DataRow In ds.Tables(1).Rows
        '    Dim a As String
        '    a = dtr.Item(0)
        '    ColDefault = a.Split(" ")
        '    For i = 0 To IndexIdArray.Count - 1
        '        Dim arrayindex As String
        '        arrayindex = "D" & IndexIdArray(i)

        '        For Each dsindex As String In ColDefault
        '            If arrayindex = dsindex Then
        '                Dim constraint As String = dtr.Item(1)
        '                Server.Con.ExecuteNonQuery(CommandType.Text, "ALTER TABLE DOC_D" & DocTypeId & " DROP CONSTRAINT " & constraint)
        '            End If
        '        Next
        '    Next
        'Next

        Dim str As String = "ALTER TABLE DOC_I" & DocTypeId & " DROP COLUMN "
        Dim str1 As String
        'Dim strd As String = "ALTER TABLE DOC_D" & DocTypeId & " DROP COLUMN "
        'Dim strd1 As String

        For i = 0 To IndexIdArray.Count - 1
            str1 = "I" & IndexIdArray(i)
            'strd1 = "D" & IndexIdArray(i)
            If i = 0 Then
                str = str & str1
                'strd = strd & strd1
            Else
                str = str & ", " & str1
                'strd = strd & ", " & strd1
            End If
        Next
        'Si no existe la columna, que siga borrando el resto igual
        Try
            Server.Con.ExecuteNonQuery(CommandType.Text, str)
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
        'Try
        '    Server.Con.ExecuteNonQuery(CommandType.Text, strd)
        'Catch ex As Exception
        '    Zamba.AppBlock.ZException.Log(ex)
        'End Try
    End Sub

    Public Shadows Sub AddIndexList(ByVal IndexId As Int64, ByVal IndexLen As Integer) Implements CreateTables.AddIndexList
        Try
            Dim str As String = "CREATE TABLE [ILST_I" & IndexId & "] ([ITEMID] [int] NOT NULL, [ITEM] [nvarchar] (" & IndexLen & ") NOT NULL) ON [PRIMARY];"
            ' Create the tables.
            Server.Con.ExecuteNonQuery(CommandType.Text, str)
            Dim StrPrimarykey As String = "ALTER TABLE [ILST_I" & IndexId & "] WITH NOCHECK ADD CONSTRAINT [PK_ILST_I" & IndexId & "] PRIMARY KEY  CLUSTERED ([ITEMID]) ON [PRIMARY]"
            Server.Con.ExecuteNonQuery(CommandType.Text, StrPrimarykey)
        Catch ex As Exception
            ' Reporta erroresd y sale
            Throw New Exception("Error: " & ex.Message)
        End Try
    End Sub
    Public Shadows Sub InsertIndexList(ByVal IndexId As Int64, ByVal IndexList As ArrayList) Implements CreateTables.InsertIndexList
        Try
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
        Catch ex As Exception
            ' Report any errors and abort.
            Throw New Exception("Error: " & ex.Message)
        End Try
    End Sub
    Public Shadows Sub DelIndexList(ByVal IndexId As Int64) Implements CreateTables.DelIndexList

        Dim str As String = "DROP TABLE ILST_I" & IndexId & ";"
        Server.Con.ExecuteNonQuery(CommandType.Text, str)

    End Sub
    Public Shadows Sub DelIndexItems(ByVal IndexId As Int64, ByVal IndexList As ArrayList) Implements CreateTables.DelIndexItems
        Try
            Dim i As Integer
            Dim table As String = "ILST_I" & IndexId
            For i = 0 To IndexList.Count - 1
                Dim strdelete As String = "DELETE FROM " & table & " where(ITEM = '" & IndexList(i) & "')"
                Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
            Next
        Catch ex As Exception
            Throw New Exception("Error: " & ex.Message)
        End Try
    End Sub
    Public Shadows Sub DeleteTable(ByVal Table As String) Implements CreateTables.DeleteTable
        Try
            Dim strDelete As String = "DROP TABLE " & Table
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

    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Inserta el contenido de un archivo de texto dentro del tabla de sustitucion
    ''' </summary>
    ''' <param name="FileName"></param>
    ''' <param name="separador"></param>
    ''' <param name="IndexId"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub BulkInsertSustitucionTable(ByVal FileName As String, ByVal separador As String, ByVal IndexId As Int64) Implements CreateTables.BulkInsertSustitucionTable
        Dim tabla As String = "SLST_S" & IndexId
        Dim sr As New IO.StreamReader(FileName)
        Try
            'La existencia del archivo debe ser verificada en la capa anterior
            Dim campos() As String
            Dim count As Int32
            Dim odbc As String
            While sr.Peek <> -1
                campos = sr.ReadLine.Split(separador)
                odbc = "Select count(1) from " & tabla & " Where Codigo=" & campos(0)
                count = Server.Con.ExecuteScalar(CommandType.Text, odbc)
                If count = 0 Then
                    odbc = "Insert into " & tabla & "(codigo,Descripcion) values(" & campos(0) & ",'" & Replace(campos(1), "'", " ") & "')"
                Else
                    odbc = "Update " & tabla & " set descripcion='" & Replace(campos(1), "'", " ") & "' Where codigo=" & campos(0)
                End If
                Server.Con.ExecuteNonQuery(CommandType.Text, odbc)
            End While
        Catch
        Finally
            sr.Close()
        End Try
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Exporta el contenido de la Tabla de sustitucion a un archivo de texto
    ''' </summary>
    ''' <param name="file"></param>
    ''' <param name="separador"></param>
    ''' <param name="IndexId"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub ExportSustitucionTable(ByVal file As String, ByVal separador As String, ByVal IndexId As Int64) Implements CreateTables.ExportSustitucionTable
        Dim odbc As String = "Select Codigo,replace(Descripcion, '''', ' ') from SLST_S" & IndexId & " order by Codigo"
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, odbc)
        Try
            Dim sw As New IO.StreamWriter(file, False)
            sw.AutoFlush = True
            Dim i As Int64
            For i = 0 To ds.Tables(0).Rows.Count - 1
                Dim strb As New System.Text.StringBuilder
                strb.Append(ds.Tables(0).Rows(i).Item(0) & separador & ds.Tables(0).Rows(i).Item(1))
                sw.WriteLine(strb.ToString)
            Next
            sw.Close()

        Catch ex As Exception
        Finally
            ds.Dispose()
        End Try
    End Sub

    Public Overridable Sub Dispose() Implements CreateTables.Dispose

    End Sub
End Class
