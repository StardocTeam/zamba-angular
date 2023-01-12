Imports Zamba.Membership
Imports System.IO
Imports System.Collections.Generic

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Core
''' Class	 : Core.ZTrace
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase que contiene objetos y métodos estáticos para manipular la clase 
''' Trace y poder filtrar los distintos tipos de errores y mensajes.
''' </summary>
''' <history>
''' 	[Tomas]	03/06/2009	Created
''' </history>
''' -----------------------------------------------------------------------------
Public NotInheritable Class ZTrace

#Region "Atributos"
    Private Shared zTraceSw As New TraceSwitch("ZTrace", "ZTrace")
    Private Shared traceDate As Int32
    Private Shared levelTemp As TraceLevel = TraceLevel.Off
    Private Shared LastTraceTime As DateTime = DateTime.Now
#End Region

#Region "Propiedades"
    ''' <summary>
    ''' Verifica si el log se encuentra apagado
    ''' Equivale al nivel 0
    ''' </summary>
    Public Shared ReadOnly Property IsOff() As Boolean
        Get
            Return ValidateTrace(TraceLevel.Off)
        End Get
    End Property
    ''' <summary>
    ''' Verifica si el log debe escribir errores
    ''' Equivale al nivel 1
    ''' </summary>
    Public Shared ReadOnly Property IsError() As Boolean
        Get
            Return ValidateTrace(TraceLevel.Error)
        End Get
    End Property
    ''' <summary>
    ''' Verifica si el log debe escribir warnings
    ''' Equivale al nivel 2
    ''' </summary>
    Public Shared ReadOnly Property IsWarning() As Boolean
        Get
            Return ValidateTrace(TraceLevel.Warning)
        End Get
    End Property
    ''' <summary>
    ''' Verifica si el log debe escribir info
    ''' Equivale al nivel 3
    ''' </summary>
    Public Shared ReadOnly Property IsInfo() As Boolean
        Get
            Return ValidateTrace(TraceLevel.Info)
        End Get
    End Property
    ''' <summary>
    ''' Verifica si el log debe escribir verbose
    ''' Equivale al nivel 4
    ''' </summary>
    Public Shared ReadOnly Property IsVerbose() As Boolean
        Get
            Return ValidateTrace(TraceLevel.Verbose)
        End Get
    End Property

    Public Shared ReadOnly Property Level() As TraceLevel
        Get
            Return zTraceSw.Level
        End Get
    End Property

    Public Shared Property Enabled() As Boolean
        Get
            If zTraceSw.Level = TraceLevel.Off Then
                Return False
            Else
                Return True
            End If
        End Get
        Set(ByVal value As Boolean)
            If value Then
                If zTraceSw.Level = TraceLevel.Off Then
                    zTraceSw.Level = levelTemp
                End If
            Else
                levelTemp = zTraceSw.Level
                zTraceSw.Level = TraceLevel.Off
            End If
        End Set
    End Property
#End Region

#Region "Métodos"
    ''' <summary>
    ''' Create a new listener and assign the tracing level.
    ''' </summary>
    ''' <param name="level">Trace level</param>
    ''' <param name="zModuleName">Módule name</param>
    ''' <history>
    '''     [Tomas] - 04/06/2009 - Created 
    ''' </history>
    Public Shared Sub SetLevel(ByVal level As Int32, ByVal zModuleName As String)
        'Crea el listener
        If level <> 0 Then
            AddListener(zModuleName)
        End If
        'Asigna el nivel de trace
        zTraceSw.Level = DirectCast(level, TraceLevel)
    End Sub
    ''' <summary>
    ''' Create a new listener and assign the tracing level.
    ''' </summary>
    ''' <param name="level">Trace level</param>
    ''' <param name="zModuleName">Módule name</param>
    ''' <history>
    '''     [Tomas] - 04/06/2009 - Created 
    ''' </history>
    Public Shared Sub SetLevel(ByVal level As TraceLevel, ByVal zModuleName As String)
        'Crea el listener
        If level <> 0 Then
            AddListener(zModuleName)
        End If
        'Asigna el nivel de trace
        zTraceSw.Level = level
    End Sub

    ''' <summary>
    ''' Create a new listener and in the log name it adds the module name.
    ''' </summary>
    ''' <param name="zModule">Module name</param>
    ''' <history>
    '''     [Tomas] - 04/06/2009 - Created 
    ''' </history>
    Public Shared Sub AddListener(ByVal zModule As String)
        Try
            For Each Chr As Char In IO.Path.GetInvalidFileNameChars
                zModule = zModule.Replace(Chr, String.Empty)
            Next
            Trace.Listeners.Add(New TextWriterTraceListener(GetTempDir("\Exceptions").FullName & "\Trace " & zModule & " - " & Now.ToString("dd-MM-yyyy HH-mm-ss") & ".txt", zModule))
            Trace.AutoFlush = True
            If traceDate = 0 Then
                traceDate = Date.Today.Day
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Delete an existing listener.
    ''' </summary>
    ''' <param name="zModuleName">Module name</param>
    ''' <history>
    '''     [Tomas] - 04/06/2009 - Created 
    ''' </history>
    Public Shared Sub RemoveListener(ByVal zModuleName As String)
        Try
            'Busca el listener viejo y lo cierra para apuntar a otro log
            If Not IsNothing(Trace.Listeners(zModuleName)) Then
                Trace.Flush()
                Trace.Listeners(zModuleName).Close()
                Trace.Listeners.Remove(zModuleName)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete an existing listener.
    ''' </summary>
    ''' <param name="zModuleIndex">Module index</param>
    ''' <history>
    '''     [Tomas] - 04/06/2009 - Created 
    ''' </history>
    Public Shared Sub RemoveListener(ByVal zModuleIndex As Int32)
        Try
            'Busca el listener viejo y lo cierra para apuntar a otro log
            If Not IsNothing(Trace.Listeners(zModuleIndex)) Then
                Trace.Flush()
                Trace.Listeners(zModuleIndex).Close()
                Trace.Listeners.RemoveAt(zModuleIndex)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shared Function GetTraceTimeandMemory(ByVal TraceLevel As Int32) As String
        Dim SB As New System.Text.StringBuilder

        Try
            Dim Duration As System.TimeSpan = DateTime.Now - LastTraceTime
            LastTraceTime = DateTime.Now

            SB.Append(CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") & DateTime.Now.Millisecond.ToString, 12))
            SB.Append(vbTab)
            SB.Append(CompleteSpaces(Int(Duration.TotalMilliseconds).ToString(), 6))
            SB.Append(vbTab)
            Return SB.ToString()
        Catch ex As Exception
            Return String.Empty
        Finally
            SB.Remove(0, SB.Length)
            SB = Nothing
        End Try
    End Function

    Private Shared Function CompleteSpaces(ByVal value As String, ByVal spaces As Int32) As String
        If value.Length < spaces Then
            For i As Int32 = 0 To spaces - value.Length - 1
                value = " " & value
            Next
        End If
        Return value
    End Function

    ''' <summary>
    ''' Validates the current trace. It checks if the current trace level is the one which is asked.
    ''' If the current date is different than the traceDate it close the current listener and creates
    ''' a new one with the same name but different date.
    ''' </summary>
    ''' <param name="level">Trace level to validate</param>
    ''' <history>
    '''     [Tomas] - 04/06/2009 - Created 
    ''' </history>
    Private Shared Function ValidateTrace(ByVal level As TraceLevel) As Boolean
        'Pregunta si se debe loguear el mensaje
        If Int32.Parse(zTraceSw.Level) >= level Then
            'Compara si la fecha de hoy es la almacenada
            If traceDate = Date.Now.Day Then
                'Si es la misma se continua escribiendo sobre el mismo log
                Trace.Write(GetTraceTimeandMemory(level))
                Return True
            Else
                'Si la fecha es diferente resetea los listeners cerrando los 
                'viejos logs y creando nuevos para continuar la escritura en ellos
                traceDate = Date.Now.Day
                ResetListeners()
                Trace.WriteLineIf(ZTrace.IsInfo, GetTraceTimeandMemory(level) & "Iniciando Trace por un nuevo dia")
                Return True
            End If
        Else
            'Como no se debe loguear devuelve false
            Return False
        End If
    End Function

    ''' <summary>
    ''' Reset all Zamba listeners 
    ''' </summary>
    ''' <remarks>
    ''' This is done to generate a log file for day
    ''' <history>
    '''     [Tomas] - 05/06/2009 - Created 
    ''' </history>
    Public Shared Sub ResetListeners()
        'En caso de haber cambiado la fecha se cierra el log del día anterior y se crea uno nuevo
        Dim names As New List(Of String)

        'Remuevo los listeners viejos
        While Trace.Listeners.Count > 1
            If Not IsNothing(Trace.Listeners(1)) Then
                names.Add(Trace.Listeners(1).Name)
                Trace.Flush()
                Trace.Listeners(1).Close()
                Trace.Listeners.RemoveAt(1)
            End If
        End While
        'Agrego los listeners nuevos apuntando a otro log
        For Each listenerName As String In names
            AddListener(listenerName)
        Next

        names.Clear()
    End Sub

    ''' <summary>
    ''' Detiene los listeners creados y los devuelve en una lista para reiniciarlos luego
    ''' </summary>
    ''' <returns>Lista de listeners detenidos para reiniciarlos luego</returns>
    ''' <remarks>Se puede utilizar en conjunto con StartListeners</remarks>
    Public Shared Function StopListeners() As List(Of String)
        'En caso de haber cambiado la fecha se cierra el log del día anterior y se crea uno nuevo
        Dim listeners As New List(Of String)

        'Remuevo los listeners viejos
        While Trace.Listeners.Count > 1
            If Not IsNothing(Trace.Listeners(1)) Then
                listeners.Add(Trace.Listeners(1).Name)
                Trace.Flush()
                Trace.Listeners(1).Close()
                Trace.Listeners.RemoveAt(1)
            End If
        End While

        Return listeners
    End Function

    ''' <summary>
    ''' Inicia una lista de listeners
    ''' </summary>
    ''' <param name="listeners">Listeners a iniciar</param>
    ''' <remarks>Se puede utilizar en conjunto con StopListeners</remarks>
    Public Shared Sub StartListeners(ByVal listeners As List(Of String))
        'Agrego los listeners nuevos apuntando a otro log
        For Each listenerName As String In listeners
            AddListener(listenerName)
        Next
    End Sub

    ''' <summary>
    ''' Gets directory to save data
    ''' </summary>
    ''' <param name="dire"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Ezequiel] 15/05/09 Created.
    '''     [Tomas] - 04/06/2009 - Se copia este método del proyecto Zamba.Cliente
    ''' </history>
    Public Shared Function GetTempDir(ByVal dire As String) As IO.DirectoryInfo
        Dim Dir As IO.DirectoryInfo
        Try
            Dir = New IO.DirectoryInfo(MembershipHelper.AppTempPath & dire)
            If Dir.Exists = False Then
                Dir.Create()
            End If
        Catch
            Dir = New IO.DirectoryInfo(System.Windows.Forms.Application.StartupPath & dire)
            If Dir.Exists = False Then
                Dir.Create()
            End If
        End Try
        Return Dir
    End Function

    ''' <summary>
    ''' Devuelve el callstack
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetCallStack() As String
        Dim stackTrace As New StackTrace()
        Dim stackFrames As StackFrame()
        Dim sb As New Text.StringBuilder()
        Try
            stackFrames = stackTrace.GetFrames()
            For Each stackFrame As StackFrame In stackFrames
                sb.AppendLine(stackFrame.GetMethod().ToString)
            Next

            Return sb.ToString
        Finally
            stackTrace = Nothing
            stackFrames = Nothing
            sb = Nothing
        End Try
    End Function

#End Region
End Class