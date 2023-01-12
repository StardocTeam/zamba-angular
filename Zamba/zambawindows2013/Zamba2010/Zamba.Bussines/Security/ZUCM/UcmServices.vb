Imports Zamba.ZTimers

Public Class UcmServices

    Private Shared _CTCB As New Threading.TimerCallback(AddressOf TimeOut)

    Private Shared _ConnectionTimer As ZTimer
    Private Shared _State As Object
  
    ' Datos para conexion
    Private Shared _user_id As Integer
    Private Shared _username As String
    Private Shared _timeout As Integer
    Private Shared _servicename As String
    Private Shared state As State



    'Metodo utilizado para el timer que almacena informacion cada un minuto para que el servicio no se dé de baja por timeout por otro usuario
    Private Shared Sub TimeOut(ByVal state As State)
        UserBusiness.Rights.SaveAction(state.ConnId, ObjectTypes.LogIn, state.RightsType, "Servicio: " & state.ServiceName)
    End Sub


    Public Shared Function Login(ByVal TimeOut As Integer, ByVal ServiceName As String, ByVal ZmbUserId As Int64, ByVal WinUserName As String, ByVal machineName As String, ByVal type As Int32, serviceType As ServiceTypes) As Boolean

        Dim conn_id As Integer = 0

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Iniciando sesión en Zamba")

        ' Guardar datos para el relogin
        _user_id = ZmbUserId
        _timeout = TimeOut
        _servicename = ServiceName
        _username = WinUserName

        If serviceType <> ServiceTypes.None Then
            machineName = machineName & " - " & serviceType.ToString()
        End If

        ' Intentar conectar sin forzar licencias
        conn_id = doLogin(_timeout, _servicename, _user_id, _username, False, machineName, type)
 

        If conn_id < 0 Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al hacer login")
            Return False
        Else
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Login correcto")
            Return True
        End If

    End Function

    ' Metodo que se desloguea de Zamba liberando la licencia
    Public Shared Sub Logout(Optional ByVal serviceType As ServiceTypes = ServiceTypes.None)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Haciendo logout")
        Ucm.RemoveConnection(serviceType)
    End Sub

    Private Shared Function doLogin(ByVal TimeOut As Integer, ByVal ServiceName As String, _
                                           ByVal ZmbUserId As Integer, ByVal WinUserName As String, _
                                           ByVal ForceConection As Boolean, ByVal machineName As String, ByVal type As Int32) As Integer
        Dim conn_id As Integer = 0

        Try

            conn_id = Ucm.NewConnection(ZmbUserId, WinUserName, machineName, Short.Parse(TimeOut), type, False)

            'Se realiza el login forzado
            If conn_id = 0 Then
                ' Si falla por maximo de licencias usadas dejarlo que pase igual               
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Intentando forzar login")
                ' Forzar conexion
                conn_id = Ucm.NewConnection(ZmbUserId, WinUserName, machineName, Short.Parse(TimeOut), type, True)
                   
            End If                         
            If conn_id > 0 Then
               state = New State()
               state.ConnId = conn_id
               state.ServiceName = ServiceName

                ' Registrar el inicio forzado
                If ForceConection Then                    
                    state.RightsType = RightsType.InicioForzadoDeSesion                         
                Else
                    state.RightsType = RightsType.LogIn
                End If
                InitializeConnectionTimer()
            End If
        Catch ex As Exception
            Throw ex
        End Try

        Return conn_id
    End Function


    Private Shared Sub InitializeConnectionTimer()

        If (_ConnectionTimer Is Nothing) Then

            Dim duetime As Int32 = 60000
            Dim period As Int32 = 60000
            _ConnectionTimer = New ZTimer(_CTCB, state, duetime, period, Int32.Parse(UserPreferences.getValue("TimeStartT", Sections.UserPreferences, "0")), Int32.Parse(UserPreferences.getValue("TimeEndT", Sections.UserPreferences, "23")))

        End If

    End Sub

End Class

Public Class State

    Public _connId As Integer
    Public Property ConnId() As Integer
        Get
            Return _connId
        End Get
        Set(ByVal value As Integer)
            _connId = value
        End Set
    End Property

    Public _serviceName As String
    Public Property ServiceName() As String
        Get
            Return _serviceName
        End Get
        Set(ByVal value As String)
            _serviceName = value
        End Set
    End Property

    Public _rightsType As String
    Public Property RightsType() As RightsType
        Get
            Return _rightsType
        End Get
        Set(ByVal value As RightsType)
            _rightsType = value
        End Set
    End Property

End Class