Imports Oracle.ManagedDataAccess.Client
Imports System.Collections.Generic
Imports System.Text
Imports Zamba.Core
Imports System.Threading
Imports Zamba

Public Class OraManagedCon
    Implements IConnection, IDisposable


    'Private _dbOwner As String
    Private _CN As OracleConnection
    Private duration As System.TimeSpan
    Private startTime As Date
    Private _CloseFlag As Boolean = True
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
    Private _state As IConnection.ConnectionStates = IConnection.ConnectionStates.Ready
    Public Property State() As IConnection.ConnectionStates Implements IConnection.State
        Get
            Return _state
        End Get
        Set(ByVal Value As IConnection.ConnectionStates)
            _state = Value
        End Set
    End Property

    Private _command As OracleCommand
    ReadOnly Property Command() As IDbCommand Implements IConnection.Command
        Get
            Return _command
        End Get
    End Property

    Public Sub New(ByVal OleconnectionString As String, ByVal DbOwner As String, Optional ByVal Flagclose As Boolean = True)
        _CN = New OracleConnection(OleconnectionString)
        Me.CloseFlag = Flagclose
        Me.dbOwner = DbOwner
    End Sub
    Public Property dbOwner As String Implements IConnection.dbOwner


    Public Sub Open() Implements IConnection.Open
        If CN Is Nothing Then
            Dim _Server As New Server(Server.currentfile)
            _Server.MakeConnection()
            _Server.dispose()
            CN = Server.Con.CN
        End If

        Dim FirstTime As Boolean

        Try
TryAgain:
            If CN.State = ConnectionState.Closed Then
                CN.Open()
            End If
        Catch ex As Exception
            If ex.Message = "Connection request timed out" And FirstTime Then
                FirstTime = False
                GoTo TryAgain
            End If
            Throw ex
        End Try
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
            Zamba.AppBlock.ZException.Log(ex)
            Return False
        Finally
            Me.Dispose()
        End Try
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve la funcion de conversion de Fecha de Oracle. TO_DATE()
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function ConvertDate(ByVal data As String, Optional ByVal ResolveDate As Boolean = True) As String Implements IConnection.ConvertDate
        If data.Trim = String.Empty Then Return "null"
        If ResolveDate Then
            Dim d As Date
            d = Date.Parse(data, New System.Globalization.CultureInfo("es-AR"))
            Return "TO_DATE('" & d.ToString("dd/MM/yyyy") & "','DD/MM/YYYY')"
        Else

            Return data
        End If
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve TO_DATE(fecha) para conversion a Fecha y Hora en Oracle 
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' 	[Martin]	12/02/2007	Modificado Cambie el hh por HH para 24hs
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function ConvertDatetime(ByVal data As String) As String Implements IConnection.ConvertDateTime
        If data.Trim = String.Empty Then Return "null"
        Dim d As DateTime = Date.Parse(data, New System.Globalization.CultureInfo("es-AR"))
        Dim str As String = "TO_DATE('" & d.ToString("dd/MM/yyyy HH:mm:ss") & "' ,'DD/MM/YYYY HH24:MI:SS')"
        Return str
    End Function

    Public Function ConvertDatetime(ByVal data As DateTime) As String Implements IConnection.ConvertDateTime
        Dim str As String = "TO_DATE('" & data.ToString("yyyy-MM-dd HH:mm:ss") & "' ,'YYYY-MM-DD HH24:MI:SS')"
        Return str
    End Function
    'Private Function Convert_Datetime(ByVal str As String) As String
    '    Dim pos As Integer = str.IndexOf("CONVERT")
    '    Dim flag As Boolean = False
    '    Dim strFinal As String = str
    '    If pos > 0 Then
    '        flag = True
    '    Else
    '        flag = False
    '    End If
    '    'SACO LOS STRINGS ANTES Y DESPUES DEL CONVERT
    '    While flag
    '        Dim str1 As String = strFinal.Substring(0, pos)
    '        strFinal = strFinal.Remove(0, pos)
    '        Dim pos2 As Integer = strFinal.IndexOf(")")
    '        Dim str2 As String = strFinal.Substring(pos2, strFinal.Length - pos2)

    '        'SACO LA FECHA Y HORA
    '        Dim pos3 As Integer = strFinal.IndexOf("'", 0)
    '        Dim pos4 As Integer = strFinal.IndexOf("'", pos3 + 1)
    '        Dim date_hour As String = strFinal.Substring(pos3, pos4 - pos3)

    '        'CONVIERTO LA FECHA
    '        Dim anio As String = date_hour.Substring(1, 4)
    '        Dim mes As String = date_hour.Substring(6, 2)
    '        Dim dia As String = date_hour.Substring(9, 2)
    '        Select Case mes
    '            Case "01"
    '                mes = "Jan"
    '            Case "02"
    '                mes = "Feb"
    '            Case "03"
    '                mes = "Mar"
    '            Case "04"
    '                mes = "Apr"
    '            Case "05"
    '                mes = "May"
    '            Case "06"
    '                mes = "Jun"
    '            Case "07"
    '                mes = "Jul"
    '            Case "08"
    '                mes = "Aug"
    '            Case "09"
    '                mes = "Sep"
    '            Case "10"
    '                mes = "Oct"
    '            Case "11"
    '                mes = "Nov"
    '            Case "12"
    '                mes = "Dic"
    '        End Select
    '        Dim fecha As String = dia & "-" & mes & "-" & anio

    '        'ARMO EL STRING FINAL
    '        strFinal = ""

    '        ''SACO LAS COMILLAS SIMPLES QUE QUEDARON EN EL STRING
    '        'str1 = str1.Substring(0, str1.Length)
    '        'str2.Remove(1, 1)
    '        strFinal = str1 & "TO_DATE ('" & fecha & "', 'dd-Mon-yyyy HH:MI:SS AM'" & str2
    '        pos = strFinal.IndexOf("CONVERT")
    '        If pos > 0 Then
    '            flag = True
    '        Else
    '            flag = False
    '        End If
    '    End While
    '    Return strFinal

    'End Function

    'Public Event ConectionBroken(ByVal ex As Exception) Implements IConnection.ConectionBroken


#Region "private utility methods & constructors"

    'Since this class provides only static methods, make the default constructor private to prevent 
    'instances from being created with "new SqlHelper()".
    Public Sub New(ByVal co As IDbConnection, Optional ByVal Flagclose As Boolean = True)
        Me._CN = co
        Me.CloseFlag = Flagclose
    End Sub 'New

    ' This method is used to attach array of SqlParameters to a OracleCommand.
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
            If ((p.Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(p.Value.ToString)) AndAlso (p.DbType = DbType.Date)) Then
                Try
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Valor Parametro " & p.ParameterName & " Date" & p.Value)
                    p.Value = Date.ParseExact(p.Value, "dd/MM/yyyy", Nothing)
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                    Try
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Fallo parseo exacto Date")
                        p.Value = Date.Parse(p.Value)
                    Catch ex1 As Exception
                        ZClass.raiseerror(ex1)
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Fallo parseo Date")
                        p.Value = Date.Parse(p.Value, "yyyyMMdd")
                    End Try
                End Try
            End If
            If ((p.Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(p.Value.ToString)) AndAlso (p.DbType = DbType.DateTime)) Then
                Try
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Valor Parametro " & p.ParameterName & " DateTime" & p.Value)
                    p.Value = Date.ParseExact(p.Value, "dd/MM/yyyy HH:mm:ss", Nothing)
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                    Try
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Fallo parseo exacto DateTime")
                        p.Value = Date.Parse(p.Value)
                    Catch ex1 As Exception
                        ZClass.raiseerror(ex1)
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Fallo parseo DateTime")
                        p.Value = Date.Parse(p.Value, "yyyyMMdd")
                    End Try
                End Try
            End If
            If ((p.Value IsNot Nothing AndAlso String.IsNullOrEmpty(p.Value.ToString))) Then
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
        ' En este metodo es donde va a machear los parametros values que nosotros le pasamos, con lo que obtuvo de la base de datos.
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
            'aca si el parametro es un cursor le asigna el dbtype de cursor, en este metodo no veo cambios a hacer.
            If commandParameters(i).DbType = OracleDbType.RefCursor Then
                commandParameters(i).Direction = ParameterDirection.Output
                commandParameters(i).Value = Nothing
            Else
                commandParameters(i).Value = parameterValues(i)
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "param " & i & "  " & parameterValues(i))
            End If
        Next

    End Sub 'AssignParameterValues

    ' This method opens (if necessary) and assigns a connection, transaction, command type and parameters 
    ' to the provided command.
    ' Parameters:
    ' -command - the OracleCommand to be prepared
    ' -connection - a valid OracleConnection, on which to execute this command
    ' -transaction - a valid OracleTransaction, or 'null'
    ' -commandType - the CommandType (stored procedure, text, etc.)
    ' -commandText - the stored procedure name or T-SQL command
    ' -commandParameters - an array of SqlParameters to be associated with the command or 'null' if no parameters are required
    Private Sub PrepareCommand(ByVal command As IDbCommand,
    ByVal transaction As IDbTransaction,
    ByVal commandType As CommandType,
    ByVal commandText As String,
    ByVal commandParameters() As IDbDataParameter) Implements IConnection.PrepareCommand
       
       If CN Is Nothing Then
            Dim _Server As New Server(Server.currentfile)
            _Server.MakeConnection()
            _Server.dispose()
            CN = Server.Con().CN
        End If
        Dim FirstTime As Boolean
        Try
TryAgain:
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
        Catch ex As Exception
            If ex.Message = "Connection request timed out" And FirstTime Then
                FirstTime = False
                GoTo TryAgain
            End If
            Throw ex
        End Try
    End Sub 'PrepareCommand

#End Region

#Region "ExecuteNonQuery"

    Public Overloads Function ExecuteNonQuery(ByVal spName As String,
    ByVal ParametersNames() As Object, ByVal parameterstypes As Object, ByVal parameterValues() As Object) As Integer Implements IConnection.ExecuteNonQuery
        Dim cmd As OracleCommand

        Try
            Me.State = IConnection.ConnectionStates.Executing
            Me.Open()

            cmd = New OracleCommand()
            Me._command = cmd
            cmd.Connection = CN
            cmd.CommandText = spName
            cmd.CommandType = CommandType.StoredProcedure

            Dim param As OracleParameter
            Dim i As Int32
            For i = 0 To parameterValues.Length - 1
                param = New OracleParameter()
                param.ParameterName = ParametersNames(i)
                param.OracleDbType = parameterstypes(i)
                param.Value = parameterValues(i)

                cmd.Parameters.Add(param)
            Next
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "SQL: " & cmd.CommandText)

            startTime = Now
            Dim rows As Integer = cmd.ExecuteNonQuery()
            duration = Now - startTime
            utilities.LogCommands(cmd.CommandText, cmd.Parameters, duration)
        Catch ex As Threading.ThreadAbortException
        Catch ex As System.Data.OracleClient.OracleException
            Zamba.AppBlock.ZException.Log(New Exception(spName & ":" & ex.ToString))
            If ex.Code = 3114 OrElse ex.Code = 3113 OrElse ex.Code = 12571 OrElse ex.Code = 1089 OrElse ex.Code = 27101 OrElse ex.Code.ToString = "01034" OrElse ex.Code = 12500 OrElse ex.Code = 12541 OrElse ex.Code.ToString = "01033" OrElse ex.Code.ToString = "03113" OrElse ex.Code.ToString = "03114" OrElse ex.Code.ToString = "01090" OrElse ex.Code = 1017 Then
                Me.CN = Server.ConnectionIsBroken(ex).CN
                Return Me.ExecuteNonQuery(spName, ParametersNames, parameterstypes, parameterValues)
            Else
                Throw ex
            End If
        Finally
            Me.State = IConnection.ConnectionStates.Ready
            If Me.CloseFlag Then
                Me.Close()
            End If
            If cmd IsNot Nothing Then
                If cmd.Parameters IsNot Nothing Then
                    cmd.Parameters.Clear()
                End If
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function

    Public Overloads Function ExecuteNonQuery(ByVal commandType As CommandType, ByVal commandText As String) As Integer Implements IConnection.ExecuteNonQuery
        'pass through the call providing null for the set of SqlParameters
        Return ExecuteNonQuery(commandType, commandText, Nothing)
    End Function

    Public Overloads Function ExecuteNonQuery(ByVal commandType As CommandType, ByVal commandText As String, ByVal ParamArray commandParameters() As IDbDataParameter) As Integer Implements IConnection.ExecuteNonQuery
        Dim cmd As OracleCommand
        Try
            Me.State = IConnection.ConnectionStates.Executing
            cmd = New OracleCommand()
            Me._command = cmd
            Dim retval As Integer
            PrepareCommand(cmd, Nothing, commandType, commandText, commandParameters)
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "SQL: " & cmd.CommandText)

            startTime = Now
            retval = cmd.ExecuteNonQuery()
            duration = Now - startTime
            utilities.LogCommands(cmd.CommandText, cmd.Parameters, duration)

            Return retval
        Catch ex As Threading.ThreadAbortException
        Catch ex As System.Data.OracleClient.OracleException
            If ex.Code <> 1 Then
                Zamba.AppBlock.ZException.Log(New Exception(commandText & ":" & ex.ToString))
            End If
            If ex.Code = 3114 OrElse ex.Code = 3113 OrElse ex.Code = 12571 OrElse ex.Code = 1089 OrElse ex.Code = 27101 OrElse ex.Code.ToString = "01034" OrElse ex.Code = 12500 OrElse ex.Code = 12541 OrElse ex.Code.ToString = "01033" OrElse ex.Code.ToString = "03113" OrElse ex.Code.ToString = "03114" OrElse ex.Code.ToString = "01090" OrElse ex.Code = 1017 Then
                CN = Server.ConnectionIsBroken(ex).CN
                Return ExecuteNonQuery(commandType, commandText, commandParameters)
            Else
                Throw ex
            End If
        Finally
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
        End Try
    End Function

    Public Overloads Function ExecuteNoTimeOutNonQuery(ByVal commandType As CommandType, ByVal commandText As String, ByVal ParamArray commandParameters() As IDbDataParameter) As Integer Implements IConnection.ExecuteNoTimeOutNonQuery
        Dim cmd As OracleCommand

        Try
            Me.State = IConnection.ConnectionStates.Executing
            cmd = New OracleCommand
            Me._command = cmd
            Dim retval As Integer
            PrepareCommand(cmd, Nothing, commandType, commandText, commandParameters)
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "SQL: " & cmd.CommandText)

            startTime = Now
            retval = cmd.ExecuteNonQuery()
            duration = Now - startTime
            utilities.LogCommands(cmd.CommandText, cmd.Parameters, duration)

            Return retval
        Catch ex As Threading.ThreadAbortException
        Catch ex As System.Data.OracleClient.OracleException
            If ex.Code <> 1 Then
                Zamba.AppBlock.ZException.Log(New Exception(commandText & ":" & ex.ToString))
            End If
            If ex.Code = 3114 OrElse ex.Code = 3113 OrElse ex.Code = 12571 OrElse ex.Code = 1089 OrElse ex.Code = 27101 OrElse ex.Code.ToString = "01034" OrElse ex.Code = 12500 OrElse ex.Code = 12541 OrElse ex.Code.ToString = "01033" OrElse ex.Code.ToString = "03113" OrElse ex.Code.ToString = "03114" OrElse ex.Code.ToString = "01090" OrElse ex.Code = 1017 Then
                Me.CN = Server.ConnectionIsBroken(ex).CN
                Return Me.ExecuteNonQuery(commandType, commandText, commandParameters)
            Else
                Throw ex
            End If
        Finally
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
        End Try
    End Function

    Public Overloads Function ExecuteNonQuery(ByVal spName As String,
        ByVal ParamArray parameterValues() As Object) As Integer Implements IConnection.ExecuteNonQuery
        Me.State = IConnection.ConnectionStates.Executing
        Dim commandParameters As IDbDataParameter()
        If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
            Try
                commandParameters = OracleHelperParameterCacheManaged.GetSpParameterSet(CN, CN.ConnectionString, spName)
                AssignParameterValues(commandParameters, parameterValues)
                Me.State = IConnection.ConnectionStates.Ready
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.Close()
                Throw ex
            End Try
            Return ExecuteNonQuery(CommandType.StoredProcedure, spName, commandParameters)
        Else
            Me.State = IConnection.ConnectionStates.Ready
            Return ExecuteNonQuery(CommandType.StoredProcedure, spName)
        End If
    End Function 'ExecuteNonQuery

    Public Overloads Function ExecuteNonQuery(ByVal transaction As IDbTransaction, ByVal commandType As CommandType, ByVal commandText As String) As Integer Implements IConnection.ExecuteNonQuery
        Return ExecuteNonQuery(transaction, commandType, commandText, Nothing)
    End Function 'ExecuteNonQuery

    Public Overloads Function ExecuteNonQuery(ByVal transaction As IDbTransaction, ByVal commandType As CommandType,
        ByVal commandText As String, ByVal ParamArray commandParameters() As IDbDataParameter) As Integer Implements IConnection.ExecuteNonQuery

        Dim cmd As OracleCommand

        Try
            Me.State = IConnection.ConnectionStates.Executing
            cmd = New OracleCommand()
            Me._command = cmd
            Dim retval As Integer
            PrepareCommand(cmd, transaction, commandType, commandText, commandParameters)
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "SQL: " & cmd.CommandText)

            startTime = Now
            retval = cmd.ExecuteNonQuery()
            duration = Now - startTime
            utilities.LogCommands(cmd.CommandText, cmd.Parameters, duration)

            Return retval
        Catch ex As Threading.ThreadAbortException
        Catch ex As System.Data.OracleClient.OracleException
            duration = Now - startTime
            utilities.LogCommands("Error: " & cmd.CommandText, cmd.Parameters, duration)
            Zamba.AppBlock.ZException.Log(New Exception(commandText & ":" & ex.ToString))
            If ex.Code = 3114 OrElse ex.Code = 3113 OrElse ex.Code = 12571 OrElse ex.Code = 1089 OrElse ex.Code = 27101 OrElse ex.Code.ToString = "01034" OrElse ex.Code = 12500 OrElse ex.Code = 12541 OrElse ex.Code.ToString = "01033" OrElse ex.Code.ToString = "03113" OrElse ex.Code.ToString = "03114" OrElse ex.Code.ToString = "01090" OrElse ex.Code = 1017 Then
                Me.CN = Server.ConnectionIsBroken(ex).CN
                Return Me.ExecuteNonQuery(transaction, commandType, commandText, commandParameters)
            Else
                Throw ex
            End If
        Finally
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
        End Try
    End Function 'ExecuteNonQuery

    Public Overloads Function ExecuteNonQuery(ByVal transaction As IDbTransaction, ByVal spName As String,
        ByVal ParamArray parameterValues() As Object) As Integer Implements IConnection.ExecuteNonQuery

        Me.State = IConnection.ConnectionStates.Executing
        Dim commandParameters As IDbDataParameter()
        If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
            commandParameters = OracleHelperParameterCacheManaged.GetSpParameterSet(transaction, transaction.Connection.ConnectionString, spName)
            AssignParameterValues(commandParameters, parameterValues)
            Me.State = IConnection.ConnectionStates.Ready
            Return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, commandParameters)
        Else
            Me.State = IConnection.ConnectionStates.Ready
            Return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName)
        End If
    End Function 'ExecuteNonQuery

    Public Overloads Function ExecuteNonQuery(ByVal transaction As IDbTransaction, ByVal spName As String,
        ByVal ParametersNames() As Object, ByVal parameterstypes As Object, ByVal parametersValues() As Object) As Integer Implements IConnection.ExecuteNonQuery

        Dim parameters As New List(Of IDbDataParameter)

        Me.State = IConnection.ConnectionStates.Executing
        Dim Ds As New DataSet

        Me.Open()
        Dim i As Int32

        Dim param As OracleParameter

        For i = 0 To parametersValues.Length - 1
            param = New OracleParameter()
            param.ParameterName = ParametersNames(i)
            param.OracleDbType = parameterstypes(i)
            param.Value = parametersValues(i)

            parameters.Add(param)

            If parameterstypes(i) = OracleDbType.RefCursor Then
                parameters(i).Direction = ParameterDirection.Output
            End If
        Next

        Return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, parameters.ToArray())
    End Function 'ExecuteNonQuery
#End Region

#Region "ExecuteDataset"
    Public Overloads Function ExecuteDataset(ByVal spName As String,
        ByVal ParametersNames() As Object, ByVal parameterstypes As Object, ByVal parametersValues() As Object) As DataSet Implements IConnection.ExecuteDataset
        Dim cmd As OracleCommand
        Dim Da As OracleDataAdapter
        Dim log As String

        Try
            Me.State = IConnection.ConnectionStates.Executing
            Dim Ds As New DataSet

            Me.Open()

            cmd = New OracleCommand
            Me._command = cmd
            cmd.Connection = CN
            cmd.CommandText = spName
            cmd.CommandType = CommandType.StoredProcedure
            Dim i As Int32

            Dim param As OracleParameter

            For i = 0 To parametersValues.Length - 1
                param = New OracleParameter()
                param.ParameterName = ParametersNames(i)
                param.OracleDbType = parameterstypes(i)
                param.Value = parametersValues(i)

                cmd.Parameters.Add(param)

                If parameterstypes(i) = OracleDbType.RefCursor Then
                    cmd.Parameters(i).Direction = ParameterDirection.Output
                End If

                log = log & " " & parametersValues(i)
            Next

            Da = New OracleDataAdapter(cmd)
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "SQL: " & cmd.CommandText)
            startTime = Now
            Da.Fill(Ds)
            duration = Now - startTime
            utilities.LogCommands(cmd.CommandText, cmd.Parameters, duration)

            Return Ds
        Catch ex As Threading.ThreadAbortException
        Catch ex As System.Data.OracleClient.OracleException
            Zamba.AppBlock.ZException.Log(New Exception(spName & ":" & ex.ToString))
            If ex.Code = 3114 OrElse ex.Code = 3113 OrElse ex.Code = 12571 OrElse ex.Code = 1089 OrElse ex.Code = 27101 OrElse ex.Code.ToString = "01034" OrElse ex.Code = 12500 OrElse ex.Code = 12541 OrElse ex.Code.ToString = "01033" OrElse ex.Code.ToString = "03113" OrElse ex.Code.ToString = "03114" OrElse ex.Code.ToString = "01090" OrElse ex.Code = 1017 Then
                Me.CN = Server.ConnectionIsBroken(ex).CN
                Return Me.ExecuteDataset(spName, ParametersNames, parameterstypes, parametersValues)
            Else
                Throw ex
            End If
        Finally
            log = Nothing
            Me.State = IConnection.ConnectionStates.Ready
            If Me.CloseFlag Then
                If Da IsNot Nothing Then
                    Da.Dispose()
                    Da = Nothing
                End If

                If cmd IsNot Nothing Then
                    If cmd.Parameters IsNot Nothing Then
                        cmd.Parameters.Clear()
                    End If
                    cmd.Dispose()
                    cmd = Nothing
                End If

                Me.Close()
            End If
        End Try
    End Function

    Public Overloads Function ExecuteDataset(ByVal commandType As CommandType,
        ByVal commandText As String) As DataSet Implements IConnection.ExecuteDataset

        Return ExecuteDataset(commandType, commandText, Nothing)
    End Function

    Public Overloads Function ExecuteDataset(ByVal commandType As CommandType, ByVal commandText As String,
        ByVal ParamArray commandParameters() As IDbDataParameter) As DataSet Implements IConnection.ExecuteDataset

        Dim da As OracleDataAdapter
        Dim cmd As OracleCommand

        Try
            Me.State = IConnection.ConnectionStates.Executing
            cmd = New OracleCommand()
            Me._command = cmd
            Dim ds As New DataSet()

            PrepareCommand(cmd, Nothing, commandType, commandText, commandParameters)
            da = New OracleDataAdapter(cmd)

            ZTrace.WriteLineIf(ZTrace.IsVerbose, "SQL: " & commandText)
            startTime = Now
            da.Fill(ds)
            duration = Now - startTime

            utilities.LogCommands(cmd.CommandText, cmd.Parameters, duration)

            Return ds
        Catch ex As Threading.ThreadAbortException
        Catch ex As System.Data.OracleClient.OracleException
            Zamba.AppBlock.ZException.Log(New Exception(commandText & ":" & ex.ToString))
            If ex.Code = 3114 OrElse ex.Code = 3113 OrElse ex.Code = 12571 OrElse ex.Code = 1089 OrElse ex.Code = 27101 OrElse ex.Code.ToString = "01034" OrElse ex.Code = 12500 OrElse ex.Code = 12541 OrElse ex.Code.ToString = "01033" OrElse ex.Code.ToString = "03113" OrElse ex.Code.ToString = "03114" OrElse ex.Code.ToString = "01090" OrElse ex.Code = 1017 Then
                Me.CN = Server.ConnectionIsBroken(ex).CN
                Return Me.ExecuteDataset(commandType, commandText, commandParameters)
            Else
                Throw ex
            End If
        Finally
            Me.State = IConnection.ConnectionStates.Ready

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
            End If
        End Try
    End Function 'ExecuteDataset

    Public Overloads Function ExecuteDataset(ByVal spName As String,
        ByVal ParamArray parameterValues() As Object) As DataSet Implements IConnection.ExecuteDataset

        Me.State = IConnection.ConnectionStates.Executing
        Dim commandParameters As IDbDataParameter()
        If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
            commandParameters = OracleHelperParameterCacheManaged.GetSpParameterSet(CN, CN.ConnectionString, spName)
            AssignParameterValues(commandParameters, parameterValues)
            Me.State = IConnection.ConnectionStates.Ready
            Dim ds As DataSet = ExecuteDataset(CommandType.StoredProcedure, spName, commandParameters)
            Dim p As IDbDataParameter
            Dim i As Int32 = 0
            For Each p In commandParameters
                'check for derived output value with no value assigned
                If (p.Direction = ParameterDirection.Output) AndAlso p.DbType = DbType.String Then
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Parametro de Salida: " & p.ParameterName & " > " & p.Value)
                    parameterValues(i) = p.Value
                End If
                i = i + 1
            Next p
            Return ds
        Else
            Me.State = IConnection.ConnectionStates.Ready
            Return ExecuteDataset(CommandType.StoredProcedure, spName)
        End If
    End Function

    Public Overloads Function ExecuteDataset(ByVal transaction As IDbTransaction, ByVal commandType As CommandType,
        ByVal commandText As String) As DataSet Implements IConnection.ExecuteDataset

        Return ExecuteDataset(transaction, commandType, commandText, Nothing)
    End Function

    Public Overloads Function ExecuteDataset(ByVal transaction As IDbTransaction, ByVal commandType As CommandType,
        ByVal commandText As String, ByVal ParamArray commandParameters() As IDbDataParameter) As DataSet Implements IConnection.ExecuteDataset

        Dim cmd As OracleCommand
        Dim da As OracleDataAdapter

        Try
            Me.State = IConnection.ConnectionStates.Executing
            cmd = New OracleCommand
            Me._command = cmd
            Dim ds As New DataSet

            PrepareCommand(cmd, transaction, commandType, commandText, commandParameters)
            da = New OracleDataAdapter(cmd)

            ZTrace.WriteLineIf(ZTrace.IsVerbose, "SQL: " & commandText)
            startTime = Now
            da.Fill(ds)
            duration = Now - startTime
            utilities.LogCommands(cmd.CommandText, cmd.Parameters, duration)

            Return ds
        Catch ex As Threading.ThreadAbortException
        Catch ex As System.Data.OracleClient.OracleException
            Zamba.AppBlock.ZException.Log(New Exception(commandText & ":" & ex.ToString))
            If ex.Code = 3114 OrElse ex.Code = 3113 OrElse ex.Code = 12571 OrElse ex.Code = 1089 OrElse ex.Code = 27101 OrElse ex.Code.ToString = "01034" OrElse ex.Code = 12500 OrElse ex.Code = 12541 OrElse ex.Code.ToString = "01033" OrElse ex.Code.ToString = "03113" OrElse ex.Code.ToString = "03114" OrElse ex.Code.ToString = "01090" OrElse ex.Code = 1017 Then
                Me.CN = Server.ConnectionIsBroken(ex).CN
                Return Me.ExecuteDataset(transaction, commandType, commandText, commandParameters)
            Else
                Throw ex
            End If
        Finally
            Me.State = IConnection.ConnectionStates.Ready

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
            End If
        End Try
    End Function 'ExecuteDataset

    Public Overloads Function ExecuteDataset(ByVal transaction As IDbTransaction, ByVal spName As String,
        ByVal ParametersNames() As Object, ByVal parameterstypes As Object, ByVal parametersValues() As Object) As DataSet Implements IConnection.ExecuteDataset
        Dim cmd As OracleCommand
        Dim Da As OracleDataAdapter
        Dim log As String

        Try
            Me.State = IConnection.ConnectionStates.Executing
            Dim Ds As New DataSet

            Me.Open()

            cmd = New OracleCommand
            Me._command = cmd
            cmd.Connection = CN
            cmd.Transaction = transaction
            cmd.CommandText = spName
            cmd.CommandType = CommandType.StoredProcedure
            Dim i As Int32

            Dim param As OracleParameter

            For i = 0 To parametersValues.Length - 1
                param = New OracleParameter()
                param.ParameterName = ParametersNames(i)
                param.OracleDbType = parameterstypes(i)
                param.Value = parametersValues(i)

                cmd.Parameters.Add(param)

                If parameterstypes(i) = OracleDbType.RefCursor Then
                    cmd.Parameters(i).Direction = ParameterDirection.Output
                End If

                log = log & " " & parametersValues(i)
            Next

            Da = New OracleDataAdapter(cmd)
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "SQL: " & cmd.CommandText)
            startTime = Now
            Da.Fill(Ds)
            duration = Now - startTime
            utilities.LogCommands(cmd.CommandText, cmd.Parameters, duration)

            Return Ds
        Catch ex As Threading.ThreadAbortException
        Catch ex As System.Data.OracleClient.OracleException
            Zamba.AppBlock.ZException.Log(New Exception(spName & ":" & ex.ToString))
            If ex.Code = 3114 OrElse ex.Code = 3113 OrElse ex.Code = 12571 OrElse ex.Code = 1089 OrElse ex.Code = 27101 OrElse ex.Code.ToString = "01034" OrElse ex.Code = 12500 OrElse ex.Code = 12541 OrElse ex.Code.ToString = "01033" OrElse ex.Code.ToString = "03113" OrElse ex.Code.ToString = "03114" OrElse ex.Code.ToString = "01090" OrElse ex.Code = 1017 Then
                Me.CN = Server.ConnectionIsBroken(ex).CN
                Return Me.ExecuteDataset(spName, ParametersNames, parameterstypes, parametersValues)
            Else
                Throw ex
            End If
        Finally
            log = Nothing
            Me.State = IConnection.ConnectionStates.Ready
            If Me.CloseFlag Then
                If Da IsNot Nothing Then
                    Da.Dispose()
                    Da = Nothing
                End If

                If cmd IsNot Nothing Then
                    If cmd.Parameters IsNot Nothing Then
                        cmd.Parameters.Clear()
                    End If
                    cmd.Dispose()
                    cmd = Nothing
                End If

                Me.Close()
            End If
        End Try
    End Function 'ExecuteDataset

    Public Overloads Function ExecuteDataset(ByVal transaction As IDbTransaction, ByVal spName As String,
        ByVal ParamArray parameterValues() As Object) As DataSet Implements IConnection.ExecuteDataset

        Me.State = IConnection.ConnectionStates.Executing
        Dim commandParameters As IDbDataParameter()
        If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
            commandParameters = OracleHelperParameterCacheManaged.GetSpParameterSet(transaction, transaction.Connection.ConnectionString, spName)
            AssignParameterValues(commandParameters, parameterValues)
            Me.State = IConnection.ConnectionStates.Ready
            Return ExecuteDataset(transaction, CommandType.StoredProcedure, spName, commandParameters)
        Else
            Me.State = IConnection.ConnectionStates.Ready
            Return ExecuteDataset(transaction, CommandType.StoredProcedure, spName)
        End If
    End Function
#End Region

#Region "ExecuteReader"
    Private Overloads Function ExecuteReader(
    ByVal transaction As IDbTransaction,
    ByVal commandType As CommandType,
    ByVal commandText As String,
    ByVal commandParameters() As IDbDataParameter,
    ByVal connectionOwnership As SqlConnectionOwnership) As IDataReader Implements IConnection.ExecuteReader
        Dim cmd As OracleCommand
        Dim dr As IDataReader

        Try
            Me.State = IConnection.ConnectionStates.Executing
            cmd = New OracleCommand()
            Me._command = cmd
            PrepareCommand(cmd, transaction, commandType, commandText, commandParameters)
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "SQL: " & commandText)

            startTime = Now
            If connectionOwnership = SqlConnectionOwnership.External Then
                dr = cmd.ExecuteReader()
            Else
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            End If
            duration = Now - startTime
            utilities.LogCommands(cmd.CommandText, cmd.Parameters, duration)

            Me.State = IConnection.ConnectionStates.Ready
            Return dr
        Catch ex As Threading.ThreadAbortException
        Catch ex As System.Data.OracleClient.OracleException
            Zamba.AppBlock.ZException.Log(New Exception(commandText & ":" & ex.ToString))
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
        End Try
    End Function 'ExecuteReader
    Public Overloads Function ExecuteReader(
   ByVal commandType As CommandType,
   ByVal commandText As String) As IDataReader Implements IConnection.ExecuteReader
        Return ExecuteReader(commandType, commandText, CType(Nothing, OracleParameter()))
    End Function 'ExecuteReader
    Public Overloads Function ExecuteReader(
   ByVal commandType As CommandType,
   ByVal commandText As String,
   ByVal ParamArray commandParameters() As IDbDataParameter) As IDataReader Implements IConnection.ExecuteReader
        Return ExecuteReader(CType(Nothing, OracleTransaction), commandType, commandText, commandParameters, SqlConnectionOwnership.External)
    End Function 'ExecuteReader
    Public Overloads Function ExecuteReader(
   ByVal spName As String,
   ByVal ParamArray parameterValues() As Object) As IDataReader Implements IConnection.ExecuteReader
        Me.State = IConnection.ConnectionStates.Executing
        Dim commandParameters As IDbDataParameter()
        If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
            commandParameters = OracleHelperParameterCacheManaged.GetSpParameterSet(CN, CN.ConnectionString, spName)
            AssignParameterValues(commandParameters, parameterValues)
            Me.State = IConnection.ConnectionStates.Ready
            Return ExecuteReader(CommandType.StoredProcedure, spName, commandParameters)
        Else
            Me.State = IConnection.ConnectionStates.Ready
            Return ExecuteReader(CommandType.StoredProcedure, spName)
        End If
    End Function 'ExecuteReader
    Public Overloads Function ExecuteReader(ByVal transaction As IDbTransaction,
   ByVal commandType As CommandType,
   ByVal commandText As String) As IDataReader Implements IConnection.ExecuteReader
        Return ExecuteReader(transaction, commandType, commandText, CType(Nothing, OracleParameter()))
    End Function 'ExecuteReader
    Public Overloads Function ExecuteReader(ByVal transaction As IDbTransaction,
   ByVal commandType As CommandType,
   ByVal commandText As String,
   ByVal ParamArray commandParameters() As IDbDataParameter) As IDataReader Implements IConnection.ExecuteReader
        Return ExecuteReader(transaction, commandType, commandText, commandParameters, SqlConnectionOwnership.External)
    End Function 'ExecuteReader
    Public Overloads Function ExecuteReader(ByVal transaction As IDbTransaction,
   ByVal spName As String,
   ByVal ParamArray parameterValues() As Object) As IDataReader Implements IConnection.ExecuteReader
        Me.State = IConnection.ConnectionStates.Executing
        Dim commandParameters As IDbDataParameter()
        If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
            commandParameters = OracleHelperParameterCacheManaged.GetSpParameterSet(transaction, transaction.Connection.ConnectionString, spName)
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
    Public Overloads Function ExecuteScalar(
   ByVal commandType As CommandType,
   ByVal commandText As String,
   Optional ByVal timeout As Int32 = 0) As Object Implements IConnection.ExecuteScalar
        Return ExecuteScalar(commandType, commandText, CType(Nothing, OracleParameter()))
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
        Dim cmd As OracleCommand

        Try
            Me.State = IConnection.ConnectionStates.Executing
            cmd = New OracleCommand()
            Dim retval As Object

            Me._command = cmd
            PrepareCommand(cmd, CType(Nothing, OracleTransaction), commandType, commandText, commandParameters)
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "SQL: " & commandText)

            startTime = Now
            retval = cmd.ExecuteScalar()
            duration = Now - startTime
            utilities.LogCommands(cmd.CommandText, cmd.Parameters, duration)

            Return retval
        Catch ex As Threading.ThreadAbortException
        Catch ex As System.Data.OracleClient.OracleException
            Zamba.AppBlock.ZException.Log(New Exception(commandText & ":" & ex.ToString))
            If ex.Code = 3114 OrElse ex.Code = 3113 OrElse ex.Code = 12571 OrElse ex.Code = 1089 OrElse ex.Code = 27101 OrElse ex.Code.ToString = "01034" OrElse ex.Code = 12500 OrElse ex.Code = 12541 OrElse ex.Code.ToString = "01033" OrElse ex.Code.ToString = "03113" OrElse ex.Code.ToString = "03114" OrElse ex.Code.ToString = "01090" OrElse ex.Code = 1017 Then
                Me.CN = Server.ConnectionIsBroken(ex).CN
                Return Me.ExecuteScalar(commandType, commandText, commandParameters)
            Else
                Throw ex
            End If
        Finally
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
        End Try
    End Function 'ExecuteScalar
    Public Overloads Function ExecuteScalar(
   ByVal spName As String,
   ByVal ParamArray parameterValues() As Object) As Object Implements IConnection.ExecuteScalar
        Me.State = IConnection.ConnectionStates.Executing
        Dim commandParameters As IDbDataParameter()
        If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
            commandParameters = OracleHelperParameterCacheManaged.GetSpParameterSet(CN, CN.ConnectionString, spName)
            AssignParameterValues(commandParameters, parameterValues)
            Me.State = IConnection.ConnectionStates.Ready
            Return ExecuteScalar(CommandType.StoredProcedure, spName, commandParameters)
        Else
            Me.State = IConnection.ConnectionStates.Ready
            Return ExecuteScalar(CommandType.StoredProcedure, spName)
        End If
    End Function 'ExecuteScalar
    Public Overloads Function ExecuteScalar(ByVal transaction As IDbTransaction,
   ByVal commandType As CommandType,
   ByVal commandText As String) As Object Implements IConnection.ExecuteScalar
        Return ExecuteScalar(transaction, commandType, commandText, CType(Nothing, OracleParameter()))
    End Function 'ExecuteScalar
    Public Overloads Function ExecuteScalar(ByVal transaction As IDbTransaction,
   ByVal commandType As CommandType,
   ByVal commandText As String,
   ByVal ParamArray commandParameters() As IDbDataParameter) As Object Implements IConnection.ExecuteScalar
        Dim cmd As OracleCommand
        Try
            Me.State = IConnection.ConnectionStates.Executing
            cmd = New OracleCommand
            Me._command = cmd
            Dim retval As Object
            PrepareCommand(cmd, transaction, commandType, commandText, commandParameters)

            ZTrace.WriteLineIf(ZTrace.IsVerbose, "SQL: " & commandText)
            startTime = Now

            retval = cmd.ExecuteScalar()
            duration = Now - startTime
            utilities.LogCommands(cmd.CommandText, cmd.Parameters, duration)

            Return retval
        Catch ex As Threading.ThreadAbortException
        Catch ex As System.Data.OracleClient.OracleException
            Zamba.AppBlock.ZException.Log(New Exception(commandText & ":" & ex.ToString))
            If ex.Code = 3114 OrElse ex.Code = 3113 OrElse ex.Code = 12571 OrElse ex.Code = 1089 OrElse ex.Code = 27101 OrElse ex.Code.ToString = "01034" OrElse ex.Code = 12500 OrElse ex.Code = 12541 OrElse ex.Code.ToString = "01033" OrElse ex.Code.ToString = "03113" OrElse ex.Code.ToString = "03114" OrElse ex.Code.ToString = "01090" OrElse ex.Code = 1017 Then
                Me.CN = Server.ConnectionIsBroken(ex).CN
                Return Me.ExecuteScalar(transaction, commandType, commandText, commandParameters)
            Else
                Throw ex
            End If
        Finally
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
        End Try
    End Function 'ExecuteScalar
    Public Overloads Function ExecuteScalar(ByVal transaction As IDbTransaction,
   ByVal spName As String,
   ByVal ParamArray parameterValues() As Object) As Object Implements IConnection.ExecuteScalar
        Me.State = IConnection.ConnectionStates.Executing
        Dim commandParameters As IDbDataParameter()
        If Not (parameterValues Is Nothing) AndAlso parameterValues.Length > 0 Then
            commandParameters = OracleHelperParameterCacheManaged.GetSpParameterSet(transaction, transaction.Connection.ConnectionString, spName)
            AssignParameterValues(commandParameters, parameterValues)
            Me.State = IConnection.ConnectionStates.Ready
            Return ExecuteScalar(transaction, CommandType.StoredProcedure, spName, commandParameters)
        Else
            Me.State = IConnection.ConnectionStates.Ready
            Return ExecuteScalar(transaction, CommandType.StoredProcedure, spName)
        End If
    End Function 'ExecuteScalar
    Public Overloads Function ExecuteScalar(ByVal spName As String,
  ByVal ParametersNames() As Object, ByVal parameterstypes As Object, ByVal parametersValues() As Object) As String Implements IConnection.ExecuteScalar
        Dim cmd As New OracleCommand
        Dim log As String

        Try
            Me.State = IConnection.ConnectionStates.Executing
            Me.Open()

            cmd = New OracleCommand()
            Me._command = cmd
            cmd.Connection = CN
            cmd.CommandText = spName
            cmd.CommandType = CommandType.StoredProcedure
            Dim i As Int32
            For i = 0 To parametersValues.Length - 1
                cmd.Parameters.Add(New OracleParameter(CStr(ParametersNames(i)), parameterstypes(i))).Value = parametersValues(i)
                log = log & " " & parametersValues(i)
            Next
            cmd.Parameters.Add(New OracleParameter(CStr(ParametersNames(parametersValues.Length - 1)), OracleDbType.RefCursor)).Direction = ParameterDirection.Output
            'parameterstypes(parametersValues.Length - 1))).Direction = parametersValues(parametersValues.Length - 1)
            Dim Da As New OracleDataAdapter(cmd)
            Dim Result As Int32
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "SQL: " & cmd.CommandText)
            startTime = Now
            Result = cmd.ExecuteScalar()
            duration = Now - startTime
            utilities.LogCommands(cmd.CommandText, cmd.Parameters, duration)

            Return CStr(Result)
        Catch ex As Threading.ThreadAbortException
        Catch ex As System.Data.OracleClient.OracleException
            Zamba.AppBlock.ZException.Log(New Exception(spName & ":" & ex.ToString))
            If ex.Code = 3114 OrElse ex.Code = 3113 OrElse ex.Code = 12571 OrElse ex.Code = 1089 OrElse ex.Code = 27101 OrElse ex.Code.ToString = "01034" OrElse ex.Code = 12500 OrElse ex.Code = 12541 OrElse ex.Code.ToString = "01033" OrElse ex.Code.ToString = "03113" OrElse ex.Code.ToString = "03114" OrElse ex.Code.ToString = "01090" OrElse ex.Code = 1017 Then
                Me.CN = Server.ConnectionIsBroken(ex).CN
                Return Me.ExecuteScalar(spName, ParametersNames, parameterstypes, parametersValues)
            Else
                Throw ex
            End If
        Finally
            log = Nothing
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
        End Try
    End Function
#End Region

    Public Property CloseFlag() As Boolean Implements IConnection.CloseFlag
        Get
            Return Me._CloseFlag
        End Get
        Set(ByVal Value As Boolean)
            Me._CloseFlag = Value
        End Set
    End Property

    Public ReadOnly Property SysDate() As String Implements IConnection.SysDate
        Get
            Return " Sysdate "
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

Public Class OraManagedCreateTables
    Implements CreateTables


    Public Shadows Sub AddDocsTables(ByVal DocTypeId As Int64) Implements CreateTables.AddDocsTables
        Try
            Dim strCheckT = "select count(1) from DOC_T" & DocTypeId
            Try
                Server.Con.ExecuteNonQuery(CommandType.Text, strCheckT)
            Catch ex As Exception
                Dim str As String = "CREATE TABLE DOC_T" & DocTypeId & " (DOC_ID NUMBER(10) primary key, FOLDER_ID NUMBER(10) NOT NULL, DISK_GROUP_ID NUMBER(10), PLATTER_ID NUMBER(10), VOL_ID NUMBER(10), DOC_FILE VARCHAR2(200), OFFSET NUMBER(10), DOC_TYPE_ID NUMBER(10), NAME VARCHAR2(255), ICON_ID NUMBER(2),SHARED NUMBER (2), Original_filename varchar2(255),ver_Parent_id number(10),version number(1),RootId number(10),NumeroVersion number(2), FileSize NUMBER(10,4)) STORAGE(INITIAL 64K)"
                Server.Con.ExecuteNonQuery(CommandType.Text, str)
            End Try
            Dim strCheckI = "select count(1) from DOC_I" & DocTypeId
            Try
                Server.Con.ExecuteNonQuery(CommandType.Text, strCheckI)
            Catch ex As Exception
                Dim str As String = "CREATE TABLE " & "DOC_I" & DocTypeId & " (DOC_ID NUMBER(10),CRDATE DATE,LUPDATE DATE ,FECHA DATE, CONSTRAINT fk" & DocTypeId & " FOREIGN KEY(doc_id) REFERENCES DOC_T" & DocTypeId & " (DOC_ID))STORAGE(INITIAL 64K)"
                Server.Con.ExecuteNonQuery(CommandType.Text, str)
            End Try
            'str = "CREATE TABLE DOC_D" & DocTypeId & " (INDEX_ID NUMBER(10) NOT NULL, INDEX_NAME VARCHAR2(30) NOT NULL, INDEX_CREATED NUMBER(1) NOT NULL,INDEX_TYPE NUMBER(1) DEFAULT 0 NOT NULL , CONSTRAINT INDEX_" & DocTypeId & "_PK PRIMARY KEY (INDEX_NAME)) STORAGE(INITIAL 64K)"
            'Server.Con.ExecuteNonQuery(CommandType.Text, str)

            'Creacion de triggers para Las fechas de creacion y modificación
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
        'Dim Str As String
        Dim strBuilder As StringBuilder = New StringBuilder()
        'Try
        '    'Elimino la vista anterior
        '    Str = "if exists(select * from sysobjects where xtype='V' and name ='doc" & docTypeId & "') begin drop view doc" & docTypeId & " end"
        '    Server.Con.ExecuteNonQuery(CommandType.Text, Str)
        'Catch ex As SqlClient.SqlException
        'Catch ex As Exception
        'End Try
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

    Public Sub CreateView(ByVal DocTypeId As Int64) Implements CreateTables.CreateView
        Dim Str As String = "CREATE OR REPLACE VIEW DOC" & DocTypeId & " AS SELECT DOC_I" & DocTypeId & ".*, DOC_T" & DocTypeId & ".FOLDER_ID As FOLDER_ID, DOC_T" & DocTypeId & ".DISK_GROUP_ID AS DISK_GROUP_ID, DOC_T" & DocTypeId & ".PLATTER_ID AS PLATTER_ID, DOC_T" & DocTypeId & ".VOL_ID AS VOL_ID, DOC_T" & DocTypeId & ".DOC_FILE AS DOC_FILE, " _
   & "DOC_T" & DocTypeId & ".OFFSET AS OFFSET, DOC_T" & DocTypeId & ".DOC_TYPE_ID AS DOC_TYPE_ID, DOC_T" & DocTypeId & ".NAME AS NAME, DOC_T" & DocTypeId & ".ICON_ID AS ICON_ID, DOC_T" & DocTypeId & ".SHARED AS SHARED, " _
   & "DOC_T" & DocTypeId & ".Original_filename AS Original_filename, DOC_T" & DocTypeId & ".ver_Parent_id AS ver_Parent_id, DOC_T" & DocTypeId & ".version AS version, DOC_T" & DocTypeId & ".RootId AS RootId, DOC_T" & DocTypeId & ".NumeroVersion AS NumeroVersion " _
   & " FROM DOC_I" & DocTypeId & " INNER JOIN DOC_T" & DocTypeId & " ON DOC_I" & DocTypeId & ".DOC_ID = DOC_T" & DocTypeId & ".DOC_ID"
        Server.Con.ExecuteNonQuery(CommandType.Text, Str)
    End Sub
    Private Sub DropView(ByVal DocTypeId As Int64) Implements CreateTables.DropView
        Dim Str As String = "DROP VIEW DOC" & DocTypeId
        Server.Con.ExecuteNonQuery(CommandType.Text, Str)
    End Sub
    Public Shadows Sub CreateTextIndex(ByVal DocTypeId As Int64, ByVal IndexId As Int64) Implements CreateTables.CreateTextIndex
        Try
            Dim strDrop As String = "DROP TABLE DOC_X" & DocTypeId
            Try
                Server.Con.ExecuteNonQuery(CommandType.Text, strDrop)
            Catch
            End Try
            Dim str As String = "CREATE TABLE DOC_X" & DocTypeId & " (DOC_ID int, ID int PRIMARY KEY) STORAGE(INITIAL 64K)"
            Server.Con.ExecuteNonQuery(CommandType.Text, str)
            strDrop = "DROP TABLE DOC_XD" & DocTypeId
            Try
                Server.Con.ExecuteNonQuery(CommandType.Text, strDrop)
            Catch ex As Exception
                Zamba.AppBlock.ZException.Log(ex)
            End Try
            str = "CREATE TABLE DOC_XD" & DocTypeId & " (ID int, WORD VARCHAR2(50) PRIMARY KEY)  STORAGE(INITIAL 64K)"
            Server.Con.ExecuteNonQuery(CommandType.Text, str)
            Dim StrUpdate As String = "UPDATE DOC_TYPE SET DocumentalID = " & IndexId & " where DOC_TYPE_ID = " & DocTypeId
            Server.Con.ExecuteNonQuery(CommandType.Text, StrUpdate)
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Throw
        End Try
    End Sub
    ''' <summary>
    ''' Crea la columna de atributos
    ''' </summary>
    ''' <param name="DocTypeId">Id de la entidad</param>
    ''' <param name="IndexIdArray"></param>
    ''' <param name="IndexTypeArray"></param>
    ''' <param name="IndexLenArray"></param>
    ''' <remarks></remarks>
    Public Shadows Sub AddIndexColumn(ByVal DocTypeId As Int64, ByVal IndexIdArray As ArrayList, ByVal IndexTypeArray As ArrayList, ByVal IndexLenArray As ArrayList) Implements CreateTables.AddIndexColumn
        Try
            Dim i As Int16
            For i = 0 To IndexIdArray.Count - 1
                Dim t As IDbTransaction = Nothing
                Try
                    Dim str As New StringBuilder
                    Dim str1 As New StringBuilder

                    Dim NewTable As New StringBuilder
                    Dim NewTable1 As New StringBuilder

                    str.Append("ALTER TABLE DOC_I" & DocTypeId & " ADD (")

                    Dim type As Integer = IndexTypeArray(i)

                    Select Case type
                        Case 1
                            str1.Append("I")
                            str1.Append(IndexIdArray(i))
                            str1.Append(" NUMERIC NULL")
                        Case 2
                            str1.Append("I")
                            str1.Append(IndexIdArray(i))
                            str1.Append(" NUMERIC NULL")
                        Case 3
                            str1.Append("I")
                            str1.Append(IndexIdArray(i))
                            str1.Append(" decimal(18,2) NULL")
                        Case 4
                            str1.Append("I")
                            str1.Append(IndexIdArray(i))
                            str1.Append(" date NULL")
                        Case 5
                            str1.Append("I")
                            str1.Append(IndexIdArray(i))
                            str1.Append(" date NULL")
                        Case 6
                            str1.Append("I")
                            str1.Append(IndexIdArray(i))
                            str1.Append(" decimal(18,2) NULL")
                        Case 7
                            str1.Append("I")
                            str1.Append(IndexIdArray(i))
                            str1.Append(" VARCHAR2(")
                            str1.Append(IndexLenArray(i))
                            str1.Append(") NULL")
                        Case 8
                            str1.Append("I")
                            str1.Append(IndexIdArray(i))
                            str1.Append(" VARCHAR2(")
                            str1.Append(IndexLenArray(i))
                            str1.Append(") NULL")
                        Case 9
                            str1.Append("I")
                            str1.Append(IndexIdArray(i))
                            str1.Append(" NUMBER(1) DEFAULT 0 NOT NULL ")
                        Case 10
                            str1.Append("I")
                            str1.Append(IndexIdArray(i))
                            str1.Append(" VARCHAR2(")
                            str1.Append(IndexLenArray(i))
                            str1.Append(") NULL")

                    End Select

                    str.Append(str1.ToString)
                    str.Append(")")

                    str1.Clear()

                    Dim c As IConnection

                    c = Server.Con
                    c.Open()
                    c.CloseFlag = False
                    t = c.CN.BeginTransaction
                    c.ExecuteNonQuery(t, CommandType.Text, str.ToString)
                    t.Commit()
                    c.CN.Close()
                Catch ex As Exception
                    t.Rollback()
                    Dim exn As New Exception("No se pudo realizar la transacción AddIndexColumn, error: " & ex.Message)
                    ZClass.raiseerror(ex)
                End Try
            Next
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Crea la columna de atributos
    ''' </summary>
    ''' <param name="DocTypeId">Id de la entidad</param>
    ''' <param name="IndexIdArray"></param>
    ''' <param name="IndexTypeArray"></param>
    ''' <param name="IndexLenArray"></param>
    ''' <remarks></remarks>
    Public Shadows Sub AddObsTable(ByVal DocTypeId As Int64, ByVal IndexIdArray As ArrayList, ByVal IndexTypeArray As ArrayList, ByVal IndexLenArray As ArrayList) Implements CreateTables.AddObsTable
        Try
            Dim str As String = "CREATE TABLE ZOBS_" & DocTypeId & "_" & IndexIdArray(0) & "(ID NUMBER NOT NULL,DOC_ID NUMBER,USER_ID varchar2(200),DateObs DATE,VALUE varchar2(200),PRIMARY KEY (ID))"
            Server.Con.ExecuteNonQuery(CommandType.Text, str)
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Throw
        End Try
    End Sub

    Public Shadows Sub DelIndexColumn(ByVal DocTypeId As Int64, ByVal IndexIdArray As ArrayList) Implements CreateTables.DelIndexColumn
        Try
            Dim str As String = "ALTER TABLE DOC_I" & DocTypeId & " DROP COLUMN "
            'Dim strd As String = "ALTER TABLE DOC_D" & DocTypeId & " DROP COLUMN "
            Dim i As Integer
            Dim str1 As String
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
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Throw
        End Try
    End Sub
    Public Shadows Sub AddIndexList(ByVal IndexId As Int64, ByVal IndexLen As Integer) Implements CreateTables.AddIndexList


        Try
            Dim strDrop As String = "DROP TABLE ILST_I" & IndexId
            Try
                Server.Con.ExecuteNonQuery(CommandType.Text, strDrop)
            Catch ex As Exception
            End Try
            Dim str As String = "CREATE TABLE ILST_I" & IndexId & " (ITEMID NUMBER(10), ITEM VARCHAR2(" & IndexLen & ")) STORAGE(INITIAL 64K)"
            ' Create the tables.
            Server.Con.ExecuteNonQuery(CommandType.Text, str)
            Dim StrPrimarykey As String = "ALTER TABLE ILST_I" & IndexId & " ADD CONSTRAINT PK_ILST_I" & IndexId & " PRIMARY KEY  (ITEMID)"
            Server.Con.ExecuteNonQuery(CommandType.Text, StrPrimarykey)
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            ' Report any errors and abort.
            Throw
        End Try

    End Sub
    Public Shadows Sub InsertIndexList(ByVal IndexId As Int64, ByVal IndexList As ArrayList) Implements CreateTables.InsertIndexList
        Try
            'Package=ILSTTodo_IPKG
            'SP=borrartodoilsti (Tabla)
            Dim strdelete As String = "DELETE FROM ILST_I" & IndexId
            ' Create the tables.
            'Del the old list
            Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
            'add the current list
            Dim i As Integer
            Dim table As String = "ILst_I" & IndexId
            For i = 0 To IndexList.Count - 1
                'PACKAGE= ILSTI__PKG
                'SP= ins_ilstitem(Tabla,ITemId,Item)
                Dim Valuestring As String = i + 1 & ", '" & IndexList.Item(i) & "'"
                Dim strInsert As String = "INSERT INTO " & table & " (ITEMID, ITEM) Values (" & Valuestring & ")"
                Server.Con.ExecuteNonQuery(CommandType.Text, strInsert)
            Next
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            ' Report any errors and abort.
            Throw
        End Try
    End Sub
    Public Shadows Sub DelIndexList(ByVal IndexId As Int64) Implements CreateTables.DelIndexList
        Try
            Dim str As String = "DROP TABLE ILST_I" & IndexId
            Server.Con.ExecuteNonQuery(CommandType.Text, str)
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Throw
        End Try
    End Sub
    Public Shadows Sub DelIndexItems(ByVal IndexId As Int64, ByVal IndexList As ArrayList) Implements CreateTables.DelIndexItems
        Try
            Dim i As Integer
            Dim table As String = "ILST_I" & IndexId
            For i = 0 To IndexList.Count - 1
                'Package= ILST_IPKG
                'SP borraritemilsti(item, tabla)
                Dim strdelete As String = "DELETE FROM " & table & " where(ITEM = '" & IndexList(i) & "')"
                Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
            Next
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Throw
        End Try
    End Sub
    Public Shadows Sub DeleteTable(ByVal Table As String) Implements CreateTables.DeleteTable
        Try
            Try
                Dim strDelete As String = "DROP TABLE " & Table
                Server.Con.ExecuteNonQuery(CommandType.Text, strDelete)
            Catch ex As Exception
                If Not ex.ToString.Contains("no existe") AndAlso Not ex.ToString.Contains("not exist") Then
                    Throw
                End If
            End Try
        Catch ex As Exception
            'Se pone el if ya que en oracle POR AHORA no existe la doc_b
            If Table.ToLower().StartsWith("doc_b") = False Then
                Zamba.AppBlock.ZException.Log(ex)
                Throw
            End If
        End Try
    End Sub
    'Public Shadows Sub ExecuteArchivo(ByVal path As String) Implements CreateTables.ExecuteArchivo
    '    Dim app As ApplicationConfig
    '    Try
    '        app = New ApplicationConfig
    '        Dim fi As IO.FileInfo = New IO.FileInfo(path)
    'Dim d1 As New IO.DirectoryInfo("C:\temp")
    '        If Not d1.Exists() Then
    '            d1.Create()
    '        End If
    '        Dim f2 As IO.FileInfo = fi.CopyTo("c:\temp\" & fi.Name, True)
    '        Dim str As String = "sqlplus " & app.USER & "/" & app.PASSWORD & "@" & app.DB & " @" & f2.FullName
    '        Try
    '            Dim i As Integer

    '            i = Shell(str, AppWinStyle.Hide, True)
    '            f2.Delete()

    '        Catch ex As Exception
    '            Zamba.AppBlock.ZException.Log(ex)
    '            Throw
    '        End Try
    '    Catch ex As Exception
    '        Zamba.AppBlock.ZException.Log(ex)
    '        Throw
    '    Finally
    '        app.dispose()
    '        app = Nothing
    '    End Try
    'End Sub
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
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
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
                strType = "Varchar2(" & IndexLen.ToString & ")"
            Case Else
                strType = "NUMERIC"
        End Select

        Try
            Dim strcreate As String = "Create Table SLST_S" & Index & " (Codigo " & strType & " NOT NULL, Descripcion Varchar2(60), CONSTRAINT PK_Codigo" & Index & " PRIMARY KEY(Codigo))"
            Server.Con.ExecuteNonQuery(CommandType.Text, strcreate)
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
    End Sub

    Public Shadows Sub UpdateIntoSustitucion(ByVal Tabla As String, ByVal Codigo As Int32, ByVal Descripcion As String) Implements CreateTables.UpdateIntoSustitucion

        'actualiza la tabla de sustitucion
        'Dim Tabla As String = "SLST_S" & TableIndex

        Dim DsSelect As DataSet
        'Dim strinsert As String
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
            Throw
        End Try
    End Sub
    Public Shadows Sub InsertIntoSustitucion(ByVal Tabla As String, ByVal Codigo As Int32, ByVal Descripcion As String) Implements CreateTables.InsertIntoSustitucion

        'Inserta en la tabla de sustitucion
        'Dim Tabla As String = "SLST_S" & TableIndex

        Dim DsSelect As DataSet
        Dim strinsert As New StringBuilder
        Dim strselect As New StringBuilder
        strselect.Append("Select count(1) from ")
        strselect.Append(Tabla)
        strselect.Append(" Where Codigo=")
        strselect.Append(Codigo)
        Try
            DsSelect = Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString)
            If DsSelect.Tables(0).Rows(0).Item(0) = 0 Then
                If (Codigo).ToString.Trim <> "" AndAlso Descripcion.Trim <> "" Then
                    strinsert.Append("Insert into ")
                    strinsert.Append(Tabla)
                    strinsert.Append(" Values(")
                    strinsert.Append(Codigo)
                    strinsert.Append(",'")
                    strinsert.Append(Descripcion)
                    strinsert.Append("')")
                    Server.Con.ExecuteNonQuery(CommandType.Text, strinsert.ToString)
                End If
            End If
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Throw
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
            Throw
        End Try
    End Sub

    Public Shadows Sub DropSustitucionTable(ByVal IndexId As Int64) Implements CreateTables.DropSustitucionTable
        Dim strdrop As String = "DROP TABLE SLST_S" & IndexId
        Try
            Server.Con.ExecuteNonQuery(CommandType.Text, strdrop)
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
    End Sub
    Public Shadows Sub BorrarSustitucionTable(ByVal IndexId As Int64) Implements CreateTables.BorrarSustitucionTable
        Dim strdelete As String = "Delete from SLST_S" & IndexId
        Try

            Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
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
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Exporta la tabla de sustitucion a un archivo de texto.
    ''' </summary>
    ''' <param name="file"></param>
    ''' <param name="separador"></param>
    ''' <param name="IndexId"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	22/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
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
End Class