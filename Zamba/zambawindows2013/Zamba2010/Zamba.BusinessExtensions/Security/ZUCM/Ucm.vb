Imports Zamba.Servers
Imports Zamba.Data

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Business
''' Class	 : Ucm
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase que gestiona la entrada de una pc a Zamba o su denegación si sobrepasa la licencia permitida. Entre otras cosas, como su eliminación
''' de las pc que están actualmente conectadas a Zamba si la pc se desconecto de Zamba o expiro su time_out
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
'''     [Gaston]	25/04/2008	Modified
''' </history>
''' -----------------------------------------------------------------------------

Public Class Ucm

    Public Shared ConectionTime As DateTime

#Region "Propiedades"

    ''' <summary>
    ''' Devuelve la cantidad de pc's que están actualmente conectadas a Zamba (licencia del WF)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetActiveWorkFlowConnections() As Int32
        Try
            Return UcmFactory.ActiveWorkflowConnections()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    ''' <summary>
    ''' Devuelve la cantidad de pc's que están actualmente conectadas a Zamba (licencia documental)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetActiveConections() As Int32
        Try
            Return UcmFactory.ActiveConections()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    ''' <summary>
    ''' Propiedad que devuelve la licencia documental o workflow desencriptada (la cantidad máximas de pc's que pueden estar conectadas) 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	09/06/2009	Modified    Parámetro type para identificación de licencia
    ''' </history>
    Public Shared ReadOnly Property PermitedConections(ByVal type As Int32) As Int32
        Get
            Return (ClsLic.PermitedConections(type))
        End Get
    End Property
#End Region

#Region "Métodos"

#Region "Verificación de la Licencia"

    ''' <summary>
    ''' Método que verifica si las pc conectadas a Zamba ya llegan o no a la cantidad de pc que permite la licencia. Si no llegan, entonces se
    ''' agrega una nueva pc, de lo contrario se lanzara un mensaje de error
    ''' </summary>
    ''' <param name="userId">Id del usuario</param>
    ''' <param name="winUser">Usuario de Windows</param>
    ''' <param name="winPC">PC del usuario</param>
    ''' <param name="timeout">Tiempo de expiración</param>
    ''' <param name="type">Identificación de la licencia</param>
    ''' <param name="forceConnection">Indica si se debe o no aceptar la conexion aunque no queden licencias disponibles</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	03/06/2009	Modified    Llamada al método verifyLicence
    '''     [Alejandro]	27/11/2009	Modified    Agregado parametro para forzar una licencia
    ''' </history>
    Public Shared Function NewConnection(ByVal userId As Int32, ByVal winUser As String, ByVal winPC As String, ByVal timeout As Int16, ByVal type As Int32, ByVal forceConnection As Boolean) As Int32

        Dim connectionId As Int64 = verifyLicense(userId, winUser, winPC, timeout, type, forceConnection)

        If (connectionId > 0) Then

            UserBusiness.Rights.CurrentUser.ConnectionId = connectionId
            Ucm.ConectionTime = Now.ToString("hh:mm:ss")

        ElseIf (connectionId = -1) Then      
            
            'Realiza el switch del tipo de licencias, pasa a WorkFlow
            connectionId = verifyLicense(userId, winUser, winPC, timeout, 1, forceConnection)
            If (connectionId > 0) Then
                UserBusiness.Rights.CurrentUser.ConnectionId = connectionId
                Ucm.ConectionTime = Now.ToString("hh:mm:ss")
                UserBusiness.Rights.CurrentUser.WFLic = True               
            End If
        End If
      
        Return connectionId
    End Function

    ''' <summary>
    ''' Método que sirve para verificar la licencia (documental o workflow)
    ''' </summary>
    ''' <param name="userId">Id del usuario</param>
    ''' <param name="winUser">Usuario de Windows</param>
    ''' <param name="winPC">PC del usuario</param>
    ''' <param name="timeout">Tiempo de expiración</param>
    ''' <param name="type">Identificación de la licencia</param>
    ''' <param name="forceConnection">Indica si se debe o no aceptar la conexion aunque no queden licencias disponibles</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	25/04/2008	Modified    Parte de este código estaba dentro del método NewConnection, lo que se hizo fue colocarlo en un 
    '''                                         nuevo método, agregar comentarios y modificar lo restante
    '''     [Gaston]	01/06/2009	Modified    Verificación y eliminación de usuarios repetidos
    '''     [Gaston]    03/06/2009  Modified    Completa modificacion del método para adaptación a licencia documental y de workflow (también se 
    '''                                         modifico el nombre del método)
    '''     [Gaston]    09/06/2009  Modified    Agregado del parámetro type a la propiedad "PermitedConections" y diferenciación de la licencia
    '''                                         al aparecer el mensaje de máximo de licencias conectadas
    '''     [Gaston]    11/06/2009  Modified    Si el usuario no puede entrar con licencia documental entonces se verifica si puede entrar con WF
    '''     [Gaston]    16/06/2009  Modified    Si el usuario es un administrador entonces no se verifica la posibilidad de ingreso con licencia de Workflow 
    '''                                         (opción ya no válida. Se verifica la posibilidad de ingreso con Workflow aún para el administrador)
    '''     [Gaston]    19/06/2009  Modified    Si el administrador no puede ingresar con licencia documental entonces se verifica si puede ingresar con WF
    '''     [Alejandro]	27/11/2009	Modified    Agregado parametro para forzar una licencia 
    ''' </history>
    Private Shared Function verifyLicense(ByRef userId As Int32, ByRef winUser As String, ByRef winPC As String, ByRef timeout As Int16,
                                          ByVal type As Int32, Optional ByVal forceConnection As Boolean = False) As Int32
        'Retorno positivo => consume una licencia
        'Retorno 0 => no posee licencias (puede significar un mensaje de error o forzar la conexion en el caso del log por Services)
        'Retorno -1 => switch de tipo de licencias

        'Verifica si el usuario ya esta logueado
        Dim conID As Int64 = GetConIDUser(userId, winUser, winPC, type)
        If conID > 0 Then
            Return conID
        End If

        Dim actCon As Int32 = Nothing

        actCon = getCurrentActiveConnections(type)


        ' Si la cantidad de usuarios (PCs) que están actualmente conectados es mayor o igual a lo que permite la licencia
       
        If PermitedConections(type) > 0 Then

             'Si forceConnection = true, acepta la conexion aunque no queden licencias 
            If(forceConnection) Then
                updateLic(1)
                Return returnNewConId(userId, winUser, winPC, timeout, 1)
            End If

            If (actCon >= PermitedConections(type)) Then
                ' Si no se elimino un usuario repetido entonces se verifica si hay un usuario cuyo tiempo de duración expiro
                RemoveExpiredConnection(timeout, type)

                actCon = getCurrentActiveConnections(type)

                ' Se vuelve a verificar si la cantidad de usuarios que están actualmente conectados es mayor o igual a lo que permite la licencia
                ' Si forceConnection = true se acepta la conexion aunque no queden licencias libres
                If (actCon >= PermitedConections(type)) And (forceConnection = False) Then

                    If (type = 0) And (UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.ModuleWorkFlow, RightsType.Use)) Then
                        Return -1
                    End If
                   
                    SaveMaxLicencesHistory(userId, type)
                    Return 0
                    
                Else
                    updateLic(type)
                    Return returnNewConId(userId, winUser, winPC, timeout, type)
                End If

                
            Else
                updateLic(type)
                Return returnNewConId(userId, winUser, winPC, timeout, type)

            End If
            ElseIf (type = 0) And (UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.ModuleWorkFlow, RightsType.Use)) Then
                Return -1
            Else
                Return 0
        End If
    End Function

    ''' <summary>
    ''' Método que sirve para actualizar la cantidad de usuarios conectados
    ''' </summary>
    ''' <param name="actCon">Cantidad antigua de usuarios conectados actualmente a Zamba</param>
    ''' <param name="type">Identificación de la licencia</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	03/06/2009	Created
    '''     [Gaston]	18/06/2009	Modified    Inserción del type 3 que identifica a un administrador que ingreso con licencia de Workflow
    ''' </history>
    Private Shared Function getCurrentActiveConnections(ByVal type As Int32) As Int32
        If type = 1 Then
            Return GetActiveWorkFlowConnections()
        Else
            Return GetActiveConections()
        End If
    End Function

    ''' <summary>
    ''' Método que sirve para verificar y eliminar de la tabla UCM al usuario que quiere entrar a Zamba (en caso de que haya quedado un registro
    ''' de ese usuario en UCM si Zamba se cerro de forma incorrecta)
    ''' </summary>
    ''' <param name="userId">Id del usuario</param>
    ''' <param name="winUser">Cuenta de Windows</param>
    ''' <param name="winPC">PC del usuario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	01/06/2009	Created
    ''' </history>
    Private Shared Function GetConIDUser(ByRef userId As Int32, ByRef winUser As String, ByRef winPC As String, ByVal type As Int32) As Int64
        Return UcmFactory.GetConIDUser(userId, winUser, winPC, type)
    End Function

    ''' <summary>
    ''' Método que remueve una conexión expirada de la UCM como para que un usuario pueda tomar su lugar
    ''' </summary>
    ''' <param name="timeout">Tiempo de expiración</param>
    ''' <param name="type">Identificación de la licencia</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	02/06/2009	Modified    Se agrego el parámetro type
    ''' </history>
    Public Shared Sub RemoveExpiredConnection(ByVal timeout As Int16, ByVal type As Int32)

        Try
            UcmFactory.RemoveExpiredConnection(timeout, type)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub



    ''' <summary>
    ''' Método que se utiliza para generar una exception de Máximo de licencias conectadas
    ''' </summary>
    ''' <param name="userId">Id del usuario</param>
    ''' <param name="type">Identificación de la licencia</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	11/06/2009	Created
    '''     [Gaston]	18/06/2009	Modified    Inserción del type 3 que identifica a un administrador que ingreso con licencia de Workflow
    ''' </history>
    Private Shared Sub SaveMaxLicencesHistory(ByRef userId As Int32, ByVal type As String)

        Dim lic As String = Nothing

        If ((type = 1) Or (type = 3)) Then
            lic = "de Worflow"
        Else
            lic = "documentales"
        End If

         RightFactory.SaveAction(userId, ObjectTypes.Licencias, Environment.MachineName, RightsType.InicioFallidoDeSesion, UserBusiness.Rights.CurrentUser,  "Máximo número de licencias " & lic)
         RightFactory.SaveAction(userId, ObjectTypes.Licencias, Environment.MachineName, RightsType.InicioFallidoDeSesion, UserBusiness.Rights.CurrentUser,  "Máximo número de licencias " & lic)

      
     
    End Sub

#End Region

#Region "Actualización de la tabla LIC"

    ''' <summary>
    ''' Método que sirve para consumir una licencia
    ''' </summary>
    ''' <param name="type">Identificación de la licencia</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	02/06/2009	Created
    '''     [Gaston]	18/06/2009	Modified    Inserción del type 3 que identifica a un administrador que ingreso con licencia de Workflow
    ''' </history>
    Private Shared Sub updateLic(ByVal type As Int32)

        Try

            If ((type = 1) Or (type = 3)) Then
                ' Se consume una licencia de workflow
                UpdateLicWF()
            Else
                ' Se consume una licencia documental
                UpdateLicDoc()
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Método que actualiza el atributo Used que indica la cantidad de pc's que están actualmente conectadas s Zamba (licencia documental)
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Sub UpdateLicDoc()
        Try
            UcmFactory.UpdateLicDoc()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Método que actualiza el atributo Used que indica la cantidad de pc's que están actualmente conectadas a Zamba (licencia del WF)
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Sub UpdateLicWF()
        Try
            UcmFactory.UpdateLicWF()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

#End Region

#Region "Generación de la conexión"

    ''' <summary>
    ''' Método que sirve para retornar el nuevo id de conexión para el usuario que quiere entrar a Zamba
    ''' </summary>
    ''' <param name="UserId">Id del usuario</param>
    ''' <param name="WinUser">Usuario de Windows</param>
    ''' <param name="WinPC">PC del usuario</param>
    ''' <param name="timeout">Tiempo de expiración</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	02/06/2009	Created     Código original del método de verificación de licencias
    ''' </history>
    Private Shared Function returnNewConId(ByRef userId As Int32, ByRef winUser As String, ByRef winPC As String, ByRef timeout As Int16, ByVal type As Int32) As Int32
        Try
            Return makeNewConnection(userId, winUser, winPC, type, timeout)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Método que sirve para agregar una nueva pc a la tabla UCM
    ''' </summary>
    ''' <param name="userId">Id del usuario</param>
    ''' <param name="winUser">Usuario de Windows</param>
    ''' <param name="winPC">PC del usuario</param>
    ''' <param name="actCon">Cantidad de usuarios conectados actualmente a Zamba</param>
    ''' <param name="type">Identificación de la licencia</param>
    ''' <param name="TimeOut">Tiempo de expiración</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	03/06/2009	Modified    
    ''' </history>
    Private Shared Function makeNewConnection(ByVal userId As Integer, ByVal winUser As String, ByVal winPC As String, ByVal type As Int32, ByVal TimeOut As Int16) As Int32
        Try
            Return UcmFactory.MakeNewConnection(userId, winUser, winPC, type, TimeOut)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return 0
        End Try
    End Function

#End Region

#Region "Eliminación de la conexión"

    ''' <summary>
    ''' Método que quita una pc de la tabla UCM. También actualiza el used de la tabla LIC en base a la licencia
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	26/05/2008	Modified    Se agrego el nombre de la pc
    '''     [Gaston]	08/06/2009	Modified    Modificación en la identificación de la licencia al eliminar al usuario
    '''     [Gaston]    18/06/2009  Modified    Validación del usuario actual
    ''' </history>
    Public Shared Sub RemoveConnection(Optional ByVal serviceType As ServiceTypes = ServiceTypes.None)
        If (Not IsNothing(UserBusiness.Rights.CurrentUser)) Then

            'Lo saco del UCM
            'La nueva version
            Dim ConnectionId As Int32 = Membership.MembershipHelper.CurrentUser.ConnectionId
            Dim pcName As String = Environment.MachineName

            If serviceType <> ServiceTypes.None Then
                pcName = pcName & " - " & serviceType.ToString()
            End If

            Try
                '  Server.closeConnections()
                If (UserBusiness.Rights.CurrentUser.WFLic = True) Then
                    UcmFactory.RemoveConnection(ConnectionId, pcName, 1)
                Else
                    UcmFactory.RemoveConnection(ConnectionId, pcName, 0)
                End If

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Método que quita una pc que se logueo a Zamba desde web de la tabla UCM (si la licencia es documental) 
    ''' </summary>
    ''' <param name="ConnectionId">Id de la conexión del usuario</param>
    ''' <param name="pcName">Nombre de la computadora del usuario que se logueo desde web</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	14/01/2009	Created
    '''     [Gaston]	08/06/2009	Modified    Se agrego el valor 0 como tercer parámetro del método
    ''' </history>
    Public Shared Sub RemoveConnectionFromWeb(ByVal ConnectionId As Int32, ByVal pcName As String)

        Try
            UcmFactory.RemoveConnection(ConnectionId, pcName, 0)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

#End Region

#Region "Otros"

    ''' <summary>
    ''' Método que verifica si la pc todavía sigue o no en la tabla UCM
    ''' </summary>
    ''' <param name="con_id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	16/05/2008	Created
    ''' </history>
    Public Shared Function verifyIfUserStillExistsInUCM(ByVal con_id As Integer, ByVal pcName As String) As Boolean

        If (UcmFactory.verifyIfUserInUCM(con_id, pcName)) Then
            Return (True)
        Else
            Return (False)
        End If

    End Function

    ''' <summary>
    ''' Método que verifica el timeout de la pc
    ''' </summary>
    ''' <param name="timeout"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FinishTimeOut(ByVal timeout As Int16) As Boolean
        'Si el TimeOut es 0 se considera que la sesion no expira
        Try
            If DateDiff(DateInterval.Minute, Server.U_Time, Now) > timeout AndAlso timeout <> 0 Then
                Return True
            Else
                Return False
            End If
        Catch
            Return True
        End Try
    End Function

    ''' <summary>
    ''' Método que actualiza el type del cliente de 0 (licencia documental) a workflow (licencia de workflow) y actualiza los LIC
    ''' </summary>
    ''' <param name="con_id">Id de conexión</param>
    ''' <param name="userId">Id de usuario</param>
    ''' <param name="winUser">Usuario de Windows</param>
    ''' <param name="winPC">PC del usuario</param>
    ''' <param name="timeout">Tiempo de expiración</param>
    ''' <param name="type">Identificación de la licencia</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	08/06/2009	Created
    '''     [Gaston]	09/06/2009	Modified    Nuevos parámetros y mejoras en el código
    '''     [Gaston]	19/06/2009	Modified    Verificación y eliminación de usuarios repetidos aunque no se hayan consumido todas las licencias
    ''' </history>
    Public Shared Sub changeLicDocToLicWF(ByRef con_id As Integer, ByRef userId As Int32, ByRef winUser As String, ByRef winPC As String, ByRef timeout As Int16, ByVal type As Int32)
        'Si el usuario ya esta logueado con WF, sale del metodo
        Dim conID As Int64 = GetConIDUser(userId, winUser, winPC, type)
        If conID > 0 Then
            Exit Sub
        End If

        Dim actCon As Int32 = Nothing

        actCon = getCurrentActiveConnections(1)

        ' Si la cantidad de usuarios (PCs) que están actualmente conectados es mayor o igual a lo que permite la licencia de workflow
        If (actCon >= PermitedConections(1)) Then
            ' Si no se elimino un usuario repetido entonces se verifica si hay un usuario cuyo tiempo de duración expiro
            RemoveExpiredConnection(timeout, 1)

            actCon = getCurrentActiveConnections(1)

            If (actCon >= PermitedConections(1)) Then
                UserBusiness.Rights.SaveAction(userId, ObjectTypes.Licencias, RightsType.InicioFallidoDeSesion, "Máximo número de licencias " & actCon.ToString)
                UserBusiness.Rights.SaveAction(userId, ObjectTypes.ErrorLog, RightsType.InicioFallidoDeSesion, "Máximo número de licencias " & actCon.ToString)
                Throw New Exception("Máximo de licencias de Workflow conectadas, por favor contáctese con su administrador del sistema")
            Else
                UcmFactory.changeLicDocToLicWF(con_id, winPC)
            End If

        Else
            UcmFactory.changeLicDocToLicWF(con_id, winPC)
        End If

        UserBusiness.Rights.CurrentUser.WFLic = True

    End Sub

    ''' <summary>
    ''' Inserta o actualiza la conexion en la UCM
    ''' </summary>
    ''' <param name="con_id">ID de la conexion</param>
    ''' <param name="userId"></param>
    ''' <param name="winUser"></param>
    ''' <param name="winPC"></param>
    ''' <param name="type"></param>
    ''' <param name="timeout"></param>
    ''' <remarks></remarks>
    Private Shared Sub UpdateOrInsertActionTime(ByVal userId As Int64, ByVal winUser As String, ByVal winPC As String, ByVal con_id As Int64, ByVal timeout As Int32, ByVal type As Int32)
        UcmFactory.UpdateOrInsertActionTime(userId, winUser, winPC, con_id, timeout, type)
    End Sub


    ''' <summary>
    ''' Inserta o actualiza la conexion en la UCM
    ''' </summary>
    Public Shared Function UpdateOrInsertActionTime(ByVal TimeOut As Integer, ByVal serviceType As ServiceTypes) As Integer
        ' Obtiene los datos del usuario actual de windows
        Dim winusername As String = UserGroupBusiness.GetUserorGroupNamebyId(Membership.MembershipHelper.CurrentUser.ID)
        Dim machinename As String = Environment.MachineName

        If serviceType <> ServiceTypes.None Then
            machinename = machinename & " - " & serviceType.ToString() & " " & Zamba.AppBlock.ZException.ModuleName
        End If

        If Membership.MembershipHelper.CurrentUser.WFLic = True Then
            UpdateOrInsertActionTime(Membership.MembershipHelper.CurrentUser.ID, winusername, machinename, Membership.MembershipHelper.CurrentUser.ConnectionId, TimeOut, serviceType)
        Else
            UpdateOrInsertActionTime(Membership.MembershipHelper.CurrentUser.ID, winusername, machinename, Membership.MembershipHelper.CurrentUser.ConnectionId, TimeOut, serviceType)
        End If
    End Function
#End Region

#End Region

End Class
