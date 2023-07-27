Imports System.Collections.Generic
Imports Zamba.Membership
Imports System.Web
Imports System.IO

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
    Private Shared _hsSingletonZCoreInstances As New Dictionary(Of String, TraceListener)
    Public Shared Property hsSingletonZCoreModules As New Dictionary(Of String, String)
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
    Public Shared ReadOnly Property IsOff() As TraceLevel
        Get
            Return ValidateTrace(TraceLevel.Off)
        End Get
    End Property
    ''' <summary>
    ''' Verifica si el log debe escribir errores
    ''' Equivale al nivel 1
    ''' </summary>
    Public Shared ReadOnly Property IsError() As TraceLevel
        Get
            Return ValidateTrace(TraceLevel.Error)
        End Get
    End Property
    ''' <summary>
    ''' Verifica si el log debe escribir warnings
    ''' Equivale al nivel 2
    ''' </summary>
    Public Shared ReadOnly Property IsWarning() As TraceLevel
        Get
            Return ValidateTrace(TraceLevel.Warning)
        End Get
    End Property

    Public Shared ReadOnly Property IsInfo() As TraceLevel
        Get
            Return ValidateTrace(TraceLevel.Info)
        End Get
    End Property


    ''' <summary>
    ''' Verifica si el log debe escribir verbose
    ''' Equivale al nivel 4
    ''' </summary>
    Public Shared ReadOnly Property IsVerbose() As TraceLevel
        Get
            Return ValidateTrace(TraceLevel.Verbose)
        End Get
    End Property

    Private Shared Property Enabled() As Boolean
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
                zTraceSw.Level = TraceLevel.Info
            End If
        End Set
    End Property

    Private Shared Property LastTraceDBTime As Date
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
        'If level <> 0 Then
        'AddListener(zModuleName)
        Zamba.AppBlock.ZException.ModuleName = zModuleName
        'End If
        'Asigna el nivel de trace
        zTraceSw.Level = DirectCast(level, TraceLevel)
    End Sub



    ''' <summary>
    ''' Create a new listener and in the log name it adds the module name.
    ''' </summary>
    ''' <param name="zModule">Module name</param>
    ''' <history>
    '''     [Tomas] - 04/06/2009 - Created 
    ''' </history>
    Private Shared Sub AddListener(ByVal zModule As String)
        Try
            For Each Chr As Char In IO.Path.GetInvalidFileNameChars
                zModule = zModule.Replace(Chr, String.Empty)
            Next
            Dim zCoreKey As String = GetKey()

            SyncLock (hsSingletonZCoreModules)
                If (zCoreKey <> zModule AndAlso Not hsSingletonZCoreModules.ContainsKey(zCoreKey)) OrElse Not hsSingletonZCoreModules.ContainsKey(zCoreKey) Then
                    hsSingletonZCoreModules.Add(zCoreKey, zModule)
                Else
                    zModule = hsSingletonZCoreModules(zCoreKey)
                End If
            End SyncLock

            If (Membership.MembershipHelper.CurrentUser) IsNot Nothing Then
                zModule = zModule & "-" & Membership.MembershipHelper.CurrentUser.Name
            End If

            SyncLock (_hsSingletonZCoreInstances)
                If Not _hsSingletonZCoreInstances.ContainsKey(zCoreKey) Then
                    Dim dir As New DirectoryInfo(Membership.MembershipHelper.AppTempPath & "\" & DateTime.Now.ToString("yyyy-MM-dd") & "\Trace\")
                    If dir.Exists = False Then
                        dir.Create()
                    End If
                    Dim Listener As New TextWriterTraceListener(dir.FullName & "\Trace " & zModule & " - " & Now.ToString("dd-MM-yyyy HH-mm-ss") & ".html", zModule)
                    Listener.Attributes.Add("AutoFlush", "true")

                    Dim sw As StreamWriter = Listener.Writer
                    If (sw IsNot Nothing) Then sw.AutoFlush = True
                    Trace.AutoFlush = True
                    Trace.IndentSize = 1
                    _hsSingletonZCoreInstances.Add(zCoreKey, Listener)

                    Listener.WriteLine("<html lang='es'><head>    <meta charset='utf-8'>    <meta name='viewport' content='width=device-width, initial-scale=1, shrink-to-fit=no'>    <title></title> <link rel='stylesheet' href='https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css' integrity='sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T' crossorigin='anonymous'><style>.row{height:15px;} .data{margin-left: .5rem;}</style></head>  <body style='font-size: .7rem'><div class='container-fluid'>")
                End If
            End SyncLock

            If traceDate = 0 Then
                traceDate = Date.Today.Day
            End If


        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
    End Sub


    ''' <summary>
    ''' Delete an existing listener.
    ''' </summary>
    ''' <param name="zModuleName">Module name</param>
    ''' <history>
    '''     [Tomas] - 04/06/2009 - Created 
    ''' </history>
    Private Shared Sub RemoveListener(ByVal zModuleName As String)
        Try
            'Busca el listener viejo y lo cierra para apuntar a otro log
            If Not IsNothing(Trace.Listeners(zModuleName)) Then
                Trace.Listeners(zModuleName).WriteLine("</div></body></html>")
                Trace.Flush()
                Trace.Listeners(zModuleName).Close()
                Trace.Listeners.Remove(zModuleName)
            End If
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Delete an existing listener.
    ''' </summary>
    ''' <param name="zModuleIndex">Module index</param>
    ''' <history>
    '''     [Tomas] - 04/06/2009 - Created 
    ''' </history>
    Private Shared Sub RemoveListener(ByVal zModuleIndex As Int32)
        Try
            'Busca el listener viejo y lo cierra para apuntar a otro log
            If Not IsNothing(Trace.Listeners(zModuleIndex)) Then
                Trace.Listeners(zModuleIndex).WriteLine("</div></body></html>")
                Trace.Flush()
                Trace.Listeners(zModuleIndex).Close()
                Trace.Listeners.RemoveAt(zModuleIndex)
            End If
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
    End Sub

    Private Shared Function GetTraceTimeandMemory() As String
        Dim SB As New System.Text.StringBuilder

        Try
            Dim Duration As System.TimeSpan = DateTime.Now - LastTraceTime
            LastTraceTime = DateTime.Now
            SB.Append("<div ><span ")
            If (Int(Duration.TotalMilliseconds) > 1000) Then
                SB.Append("class='text-error' >")
            Else
                SB.Append("class='text-primary' >")
            End If
            SB.Append(CompleteSpaces(DateTime.Now.ToString("HH:mm:ss:") & DateTime.Now.Millisecond.ToString, 12))
            SB.Append(CompleteSpaces(Int(Duration.TotalMilliseconds).ToString(), 7))
            SB.Append("</span>")
            Return SB.ToString()
        Catch ex As Exception
            Return String.Empty
        Finally
            SB.Remove(0, SB.Length)
            SB = Nothing
        End Try
    End Function

    Public Shared Function CompleteSpaces(ByVal value As String, ByVal spaces As Int32) As String
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
    Private Shared Function ValidateTrace(ByVal level As TraceLevel) As TraceLevel
        'Pregunta si se debe loguear el mensaje
        If Int32.Parse(zTraceSw.Level) >= level Then
            'Compara si la fecha de hoy es la almacenada
            If traceDate <> Date.Now.Day Then
                'Si la fecha es diferente resetea los listeners cerrando los 
                'viejos logs y creando nuevos para continuar la escritura en ellos
                traceDate = Date.Now.Day
                'ResetListeners()
            End If
            Return level
        Else
            'Como no se debe loguear devuelve false
            Return TraceLevel.Info
        End If
    End Function


    Public Shared Sub ResetListeners()
        'En caso de haber cambiado la fecha se cierra el log del día anterior y se crea uno nuevo
        Dim names As New List(Of String)

        'Remuevo los listeners viejos
        While Trace.Listeners.Count > 1
            If Not IsNothing(Trace.Listeners(1)) Then
                names.Add(Trace.Listeners(1).Name)
                Trace.Listeners(1).WriteLine("</div></body></html>")
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
    Private Shared Function StopListeners() As List(Of String)
        'En caso de haber cambiado la fecha se cierra el log del día anterior y se crea uno nuevo
        Dim listeners As New List(Of String)

        'Remuevo los listeners viejos
        While Trace.Listeners.Count > 1
            If Not IsNothing(Trace.Listeners(1)) Then
                listeners.Add(Trace.Listeners(1).Name)
                Trace.Listeners(1).WriteLine("</div></body></html>")
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
    Private Shared Sub StartListeners(ByVal listeners As List(Of String))
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
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
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
    Private Shared Function GetCallStack() As String
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

    ''' <summary>
    ''' Escribe trace
    ''' </summary>
    ''' <param name="line"></param>
    ''' <remarks></remarks>
    Private Shared Sub WriteLine(ByVal line As String)
        Dim I As TraceListener = GetInstance()
        If Not IsNothing(I) Then
            I.WriteLine(GetTraceTimeandMemory() & line)
            I.Flush()
            Trace.Flush()
        End If
    End Sub




    ''' <summary>
    ''' Escribe trace si se cumple la condicion
    ''' </summary>
    ''' <param name="line"></param>
    ''' <remarks></remarks>
    Public Shared Sub WriteLineIf(ByVal level As TraceLevel, ByVal line As String)
        If level <> TraceLevel.Off Then
            Dim instance As TraceListener = GetInstance()
            If Not IsNothing(instance) Then
                If line.Trim <> String.Empty Then
                    Dim timestring As String = GetTraceTimeandMemory()

                    Dim nline As String = timestring & "<span class=' data "

                    Select Case level
                        Case TraceLevel.Error
                            nline &= "text-error"
                        Case TraceLevel.Info
                            nline &= "text-primary"
                        Case TraceLevel.Verbose
                            nline &= "text-secondary"
                        Case TraceLevel.Warning
                            nline &= "text-warning"
                    End Select

                    nline &= " ' >" & line & "</span></div>"

                    instance.WriteLine(nline)
                    instance.Flush()
                    Trace.Flush()
                End If
            End If
        End If
    End Sub

    Private Shared Sub Write(ByVal line As String)
        Dim TraceInstance As TraceListener = GetInstance()
        If Not IsNothing(TraceInstance) Then
            TraceInstance.Write(line)
            TraceInstance.Flush()
            Trace.Flush()
        End If
    End Sub


#End Region

    Public Sub New()

    End Sub


    ''' <summary>
    ''' Obtiene la instancia actual de ZCore
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetInstance() As TraceListener
        Dim zCoreKey As String = GetKey()
        SetLevel()
        If Not _hsSingletonZCoreInstances.ContainsKey(zCoreKey) Then
            AddListener(zCoreKey)
        End If
        If _hsSingletonZCoreInstances.ContainsKey(zCoreKey) Then
            Return _hsSingletonZCoreInstances.Item(zCoreKey)
        Else
            Return Nothing
        End If

    End Function

    Public Shared Function GetKey() As String
        If Membership.MembershipHelper.isWeb Then
            Dim SessionName As String = String.Empty
            If Membership.MembershipHelper.CurrentUser IsNot Nothing Then
                SessionName = Membership.MembershipHelper.CurrentUser.Name & " - "
            End If
            SessionName = SessionName & Membership.MembershipHelper.CurrentSession.SessionID
            Return SessionName
        Else
            Return "CommonWebServiceTrace"
        End If
    End Function

    Private Shared Function SetLevel()
        If Membership.MembershipHelper.isWeb Then
            Dim TraceLevel As Int32 = 4
            If Membership.MembershipHelper.CurrentUser IsNot Nothing Then
                TraceLevel = Membership.MembershipHelper.CurrentUser.TraceLevel
                If (TraceLevel > 0) Then
                    zTraceSw.Level = TraceLevel
                End If
            End If
        End If
    End Function


    Public Shared Sub RemoveCurrentInstance()
        Dim zCoreKey As String = GetKey()
        If _hsSingletonZCoreInstances.ContainsKey(zCoreKey) Then
            _hsSingletonZCoreInstances(zCoreKey).WriteLine("</div></body></html>")
            _hsSingletonZCoreInstances(zCoreKey).Flush()
            _hsSingletonZCoreInstances(zCoreKey).Close()
            _hsSingletonZCoreInstances.Remove(zCoreKey)
        End If
    End Sub


End Class
