Imports System.Text
Imports Zamba.Core
Imports Zamba.Data
Imports Zamba.Membership

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Data
''' Class	 : UcmFactory
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase que maneja todo lo relativo a la tabla UCM, la tabla en donde están los datos de todas las pc's que están actualmente conectadas a Zamba
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
'''     [Gaston]	25/04/2008	Created
'''     Gran parte de lo que aparece en esta clase esta tomado de la clase Ucm de Zamba.Business. Lo que se hizo fue pasar ese código a esta clase.
'''     Más algunas correcciones que hice en ciertos métodos del código (que aparecen como Gaston Modified) y agregados (Gaston Created) 
''' </history>
''' -----------------------------------------------------------------------------

Public Class UcmFactory

#Region "Métodos"

    ''' <summary>
    ''' Método utilizado para devolver la cantidad de pc's actualmente conectadas a Zamba (licencia del WF)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	10/06/2009  Modified      Modificación completa del método. Ahora se toma en cuenta la cantidad de conexiones que se
    '''                                           encuentren en la tabla UCM, y no el used de la tabla LIC
    '''     [Gaston]    19/06/2009  Modified      Inserción del type 3 que identifica a un administrador que ingreso con licencia de Workflow
    ''' </history>
    Public Function ActiveWorkflowConnections() As Integer
        Dim usados As Integer = Server.Con.ExecuteScalar(CommandType.Text, "Select count(1) from UCM Where Type = 1")
        Return (usados)
    End Function

    ''' <summary>
    ''' Método utilizado para devolver la cantidad de pc's actualmente conectadas a Zamba (licencia documental)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	04/06/2009  Modified    El type pasa a ser <> de 1 (es decir, de la licencia de Workflow). La razón del cambio es que
    '''                                         para un cliente el type es 0, y para un administrador el type es 2 (en licencia documental)
    '''     [Gaston]    19/06/2009  Modified    Inserción del type 3 que identifica a un administrador que ingreso con licencia de Workflow
    ''' </history>
    Public Function ActiveConections() As Int32
        Dim usados As Integer = Server.Con.ExecuteScalar(CommandType.Text, "Select count(1) from UCM Where Type <> 1")
        Return (usados)
    End Function

    ''' <summary>
    ''' Método que actualiza el atributo Used (el atributo que dice las pc's que están actualmente conectadas a Zamba) si la licencia es documental
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub UpdateLicDoc()

        Dim key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Dim iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Dim sql As String = "Update Lic set Used='" & Tools.Encryption.EncryptString((ActiveConections() + 1).ToString, key, iv) & "' Where Type = 0"
        Server.Con.ExecuteNonQuery(CommandType.Text, sql)

    End Sub

    ''' <summary>
    ''' Método que actualiza el atributo Used (el atributo que dice las pc's que están actualmente conectadas a Zamba) si la licencia es WF
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub UpdateLicWF()

        Dim key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Dim iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Dim sql As String = "Update Lic set Used='" & Tools.Encryption.EncryptString((ActiveWorkflowConnections() + 1).ToString, key, iv) & "' Where Type = 1"
        Server.Con.ExecuteNonQuery(CommandType.Text, sql)

    End Sub

    ''' <summary>
    ''' Método que agrega una nueva pc a la tabla UCM
    ''' </summary>
    ''' <param name="UserId">Id del usuario</param>
    ''' <param name="WinUser">Usuario de Windows</param>
    ''' <param name="WinPC">PC del usuario</param>
    ''' <param name="ActCon">Cantidad actual de conexiones</param>
    ''' <param name="WF">Identificación de la licencia</param>
    ''' <param name="TimeOut">Tiempo de expiración</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	25/04/2008  07/05/2008	Modified
    ''' </history>
    Public Function MakeNewConnection(ByVal UserId As Integer, ByVal WinUser As String, ByVal WinPC As String, ByVal WF As Int32, ByVal TimeOut As Int32) As Int32

        Dim conDS As DataSet = Server.Con.ExecuteDataset(CommandType.Text, String.Format("SELECT CON_ID,type FROM UCM Where USER_ID = {0} AND WINUSER = '{1}' AND WINPC = '{2}'", UserId, WinUser, WinPC))
        If (conDS IsNot Nothing AndAlso conDS.Tables.Count > 0 AndAlso conDS.Tables(0).Rows.Count > 0) Then

            Dim actualconid As Int64 = Int64.Parse(conDS.Tables(0).Rows(0).Item("CON_ID"))

            If IsDBNull(actualconid) OrElse actualconid < 1 Then
                Dim lastConID As Int32 = GetNewConnection_Id()

                Dim query As String = String.Format("INSERT INTO UCM(USER_ID, C_TIME, U_TIME, WinUser, WinPC, CON_ID, Time_out, Type) VALUES({0},GetDate(),GetDate() ,'{1}','{2}',{3},{4},{5})", UserId.ToString(), WinUser, WinPC, lastConID, TimeOut, WF)
                If Server.isOracle Then query = query.Replace("GetDate()", "SYSDATE")
                Server.Con.ExecuteNonQuery(CommandType.Text, query)
                Return lastConID
            Else
                Dim actualType As Int32 = Int32.Parse(conDS.Tables(0).Rows(0).Item("Type"))
                If actualType <> WF Then
                    changeLic(actualconid, WinPC, actualType, WF)
                End If
                Return actualconid
            End If
        Else
            Dim lastConID As Int32 = GetNewConnection_Id()

            Dim query As String = String.Format("INSERT INTO UCM(USER_ID, C_TIME, U_TIME, WinUser, WinPC, CON_ID, Time_out, Type) VALUES({0},GetDate(),GetDate() ,'{1}','{2}',{3},{4},{5})", UserId.ToString(), WinUser, WinPC, lastConID, TimeOut, WF)
            If Server.isOracle Then query = query.Replace("GetDate()", "SYSDATE")
            Server.Con.ExecuteNonQuery(CommandType.Text, query)
            Return lastConID
        End If


    End Function

    ''' <summary>
    ''' Método que genera un nuevo connection_id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	25/04/2008	Created
    '''                 20/05/2008  Modified    Para la generación de un número aleatorio al estar vacía la tabla UCM
    ''' </history>
    Public Function GetNewConnection_Id() As Integer

        ' Si el servidor es SQL
        If Server.isSQLServer Then

            ' Si la tabla UCM no está vacía entonces se devuelve el máximo con_id + 1, de lo contrario, si la tabla UCM está vacía entonces se 
            ' devuelve como con_id un número aleatorio que puede ir desde 1 hasta 1000
            Dim query As New System.Text.StringBuilder
            query.Append("IF (SELECT COUNT(1) FROM UCM) > 0 ")
            query.Append("BEGIN ")
            query.Append("SELECT MAX(CON_ID) + 1 AS LastId  FROM UCM END ")
            query.Append("ELSE ")
            query.Append("BEGIN ")
            query.Append("SELECT { fn TRUNCATE (RAND() * 1000, 0) } AS Expr1 ")
            query.Append("END")
            Return (Server.Con.ExecuteScalar(CommandType.Text, query.ToString()))

            'Sino, si es Oracle
        Else

            Dim value As Integer = Server.Con.ExecuteScalar(CommandType.Text, "SELECT count(1) FROM UCM")

            ' Si la tabla UCM no tiene nada se retorna un número aleatorio (que puede ser del 1 hasta el 1000)
            If (value = 0) Then

                Randomize()
                Return (Convert.ToInt16(1 - 1000 * Rnd() + 1000))

                ' de lo contrario, se retorna como número máximo el con_id + 1
            Else
                Return (Server.Con.ExecuteScalar(CommandType.Text, "SELECT MAX(CON_ID) + 1 AS LastId FROM UCM"))
            End If

        End If

    End Function


    ''' <summary>
    ''' Método que quita una pc de la tabla UCM mediante su id de conexión y el nombre de la pc. También actualiza el used de la tabla LIC 
    ''' en base a la licencia
    ''' </summary>
    ''' <param name="ConnectionID">Id de conexión</param>
    ''' <param name="pcName">PC del usuario</param>
    ''' <param name="type">Identificación de la licencia</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	26/05/2008	Modified     Se agrego el pcName
    '''     [Gaston]	08/06/2009	Modified     Nuevo parámetro: type 
    '''     [Gaston]	10/06/2009	Modified     Antes de remover la conexión se debe comprobar si el usuario todavía sigue en la tabla UCM
    '''     [Gaston]    19/06/2009  Modified     Inserción del type 3 que identifica a un administrador que ingreso con licencia de Workflow 
    ''' </history>
    Public Sub RemoveConnection(ByVal ConnectionID As Int32)

        If verifyIfUserStillExistsInUCM(ConnectionID) Then

            Dim key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
            Dim iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
            Dim Usados As Int32
            Dim type As Int32 = GetConnectionType(ConnectionID)
            ' Si la licencia es de Workflow 1: Cliente - 3: Administrador
            If ((type = 1) Or (type = 3)) Then
                Usados = (ActiveWorkflowConnections() - 1)
                type = 1
            Else
                Usados = (ActiveConections() - 1)
                type = 0
            End If

            If (Usados < 0) Then Usados = 0

            Dim sql As String = "Update LIC Set Used='" & Tools.Encryption.EncryptString(Usados, key, iv) & "' Where Type = " & type

            Server.Con.ExecuteNonQuery(CommandType.Text, sql)

            Server.Con.ExecuteNonQuery(CommandType.Text, "DELETE FROM ZSS WHERE ConnectionId =" & ConnectionID)
            Server.Con.ExecuteNonQuery(CommandType.Text, "DELETE FROM UCM WHERE Con_ID =" & ConnectionID)

        End If

    End Sub


    Public Function UserUniqueConnection(ByVal ConnectionId As Int64) As Boolean
        Dim value As Integer = Server.Con.ExecuteScalar(CommandType.Text, "SELECT count(*) FROM UCM Where USER_ID in (SELECT USER_ID FROM UCM Where  CON_ID = " & ConnectionId & ")")

        If (value > 1) Then
            Return (False)
        Else
            Return (True)
        End If

    End Function

    Public Function GetFirstExpiredConnection() As Int32
        Dim ConnectionID As Int32 = 0
        If Server.isOracle Then
            ConnectionID = Server.Con.ExecuteScalar(CommandType.Text, "SELECT CON_ID FROM UCM Where(TIME_OUT < TO_NUMBER(SYSDATE - U_TIME) * (24 * 60)) AND rownum = 1")
        Else
            ConnectionID = Server.Con.ExecuteScalar(CommandType.Text, "SELECT TOP 1 CON_ID FROM UCM Where DATEDIFF(mi,U_TIME,GetDate())> [Time_Out]")
        End If

        Return ConnectionID
    End Function

    Public Function GetUserIdByConId(ConnectionId) As Int64
        Dim UserId As Int64 = 0
        UserId = Server.Con.ExecuteScalar(CommandType.Text, String.Format("SELECT USER_ID FROM UCM Where CON_ID = {0}", ConnectionId))
        Return UserId
    End Function
    Public Function GetConnectionType(conid As Int32) As Int32
        Dim type As Int32
        type = Server.Con.ExecuteScalar(CommandType.Text, "SELECT TYPE FROM UCM Where CON_ID =" & conid)
        Return type
    End Function

    ''' <summary>
    ''' Método que remueve una conexión expirada de la UCM como para que un usuario pueda tomar su lugar
    ''' </summary>
    ''' <param name="timeout">Tiempo de expiración</param>
    ''' <param name="type">Identificación de la licencia</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	02/06/2009	Modified    Adaptación del código según el type
    '''     [Gaston]	04/06/2009	Modified    Si type no es uno entonces la consulta será type  distinto de 1
    '''     [Gaston]	09/06/2009	Modified    Si hay un usuario para eliminar por tiempo de expiración entonces se elimina dicho usuario y 
    '''                                         se actualiza la tabla LIC
    '''     [Gaston]    10/06/2009  Modified    Sólo se realiza una vez la resta de usados. No dos veces como ocurría antes
    '''     [Gaston]    19/06/2009  Modified    Inserción del type 3 que identifica a un administrador que ingreso con licencia de Workflow
    ''' </history>
    Public Sub RemoveExpiredConnection()

        Dim conid As Int32

        'SELECCIONA EL PRIMER REGISTRO EXPIRADO
        conid = GetFirstExpiredConnection()
        If (conid) Then ' Si el conid es distinto a cero entonces hay un usuario para eliminar por tiempo de expiración
            RemoveConnection(conid)
        End If

    End Sub



    ''' <summary>
    ''' Método que verifica si la pc todavía sigue o no en la tabla UCM
    ''' </summary>
    ''' <param name="con_id">Id de conexión</param>
    ''' <param name="pcName">PC del usuario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	16/05/2008	Created
    ''' </history>
    Public Function verifyIfUserStillExistsInUCM(ByVal con_id As Integer) As Boolean
        Dim value As Integer = Server.Con.ExecuteScalar(CommandType.Text, "SELECT count(1) FROM UCM Where CON_ID = " & con_id)

        If (value >= 1) Then
            Return (True)
        Else
            Return (False)
        End If

    End Function

    ''' <summary>
    ''' Método que sirve para verificar si ya existen uno o más registros en la tabla UCM para el usuario que quiere entrar a Zamba. Si existe, 
    ''' entonces se eliminan los registros repetidos. Teóricamente no debería haber ningun registro para dicho usuario ya que todavia no entro a 
    ''' Zamba. Pero es posible que quede el registro si Zamba se cierra en forma incorrecta
    ''' </summary>
    ''' <param name="userId">Id del usuario</param>
    ''' <param name="winUser">Usuario de Windows</param>
    ''' <param name="winPC">PC del usuario</param>
    ''' <param name="type">Identificación de la licencia</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	01/06/2009	Created
    '''     [Gaston]	02/06/2009	Modified    Se agrego el parámetro type
    ''' </history>
    Public Function verifyAndDeleteRepeatedUser(ByRef userId As Int32, ByRef winUser As String, ByRef winPC As String, ByVal type As Int32) As Boolean

        'Se obtiene el id del proceso para diferenciar las instancias de Zamba.
        'If String.IsNullOrEmpty(processIdStr) Then
        '    processIdStr = "_" & Process.GetCurrentProcess().Id.ToString()
        'End If

        Dim query As New StringBuilder()

        query.Append("SELECT count(1) FROM UCM Where ")
        query.Append("USER_ID = " & userId & " AND ")
        query.Append("WINUSER = '" & winUser & "' AND ")
        'query.Append("WINPC = '" & winPC & processIdStr & "' AND ")
        query.Append("WINPC = '" & winPC & "' AND ")
        query.Append("TYPE = " & type)

        Dim value As Integer = Server.Con.ExecuteScalar(CommandType.Text, query.ToString())

        query = Nothing

        ' Si value es mayor a cero entonces se debe eliminar al usuario que quiere entrar de la tabla UCM, ya que no debería existir en dicha tabla,
        ' puesto que todavía no accedio. Este caso se produce cuando por alguna razón el programa Zamba se cierra de forma incorrecta y el usuario
        ' todavía sigue en UCM, cuando en realidad ya no debería estar
        If (value > 1) Then
            deleteRepeatedUser(userId, winUser, winPC, type)
            Return (True)
        Else
            Return (False)
        End If

    End Function

    ''' <summary>
    ''' Método que sirve para eliminar un usuario repetido en la tabla UCM. Sólo para el usuario que quiere entrar a Zamba
    ''' </summary>
    ''' <param name="userId">Id del usuario</param>
    ''' <param name="winUser">Usuario de Windows</param>
    ''' <param name="winPC">PC del usuario</param>
    ''' <param name="type">Identificación de la licencia</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	01/06/2009	Created
    '''     [Gaston]	02/06/2009	Modified    Se agrego el parámetro type
    ''' </history>
    Private Sub deleteRepeatedUser(ByRef userId As Int32, ByRef winUser As String, ByRef winPC As String, ByVal type As Int32)


        Dim queryzss As New StringBuilder

        queryzss.Append("DELETE FROM ZSS where CONNECTIONID IN ( select CON_ID from UCM Where ")
        queryzss.Append("USER_ID = " & userId & " AND ")
        queryzss.Append("WINUSER = '" & winUser & "' AND ")
        ' queryzss.Append("WINPC = '" & winPC & processIdStr & "' AND ")
        queryzss.Append("WINPC = '" & winPC & "' AND ")
        queryzss.Append("TYPE = " & type)
        queryzss.Append(")")

        Server.Con.ExecuteNonQuery(CommandType.Text, queryzss.ToString())

        Dim query As New StringBuilder

        query.Append("DELETE FROM UCM Where ")
        query.Append("USER_ID = " & userId & " AND ")
        query.Append("WINUSER = '" & winUser & "' AND ")
        ' query.Append("WINPC = '" & winPC & processIdStr & "' AND ")
        query.Append("WINPC = '" & winPC & "' AND ")
        query.Append("TYPE = " & type)

        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString())

        query = Nothing

        Dim key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Dim iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Dim usados As Int32

        ' Actualización del LIC 
        Dim count As String = Zamba.Tools.Encryption.DecryptString(Server.Con.ExecuteScalar(CommandType.Text, "Select Used from LIC Where TYPE = " & type), key, iv)

        If (String.IsNullOrEmpty(count)) Then
            usados = 1
        Else
            usados = Int32.Parse(count) - 1
        End If

        'ACTUALIZA LA TABLA LIC PARA LICENCIAS DOCUMENTALES
        If (usados < 0) Then usados = 0

        Dim SQL As String = "Update LIC set Used='" & Zamba.Tools.Encryption.EncryptString(usados - 1, key, iv) & "' Where TYPE = " & type
        Server.Con.ExecuteNonQuery(CommandType.Text, SQL)

    End Sub

    ''' <summary>
    ''' Método que actualiza el type del cliente de 0 (licencia documental) a Workflow (licencia de Workflow) y actualiza los LIC
    ''' </summary>
    ''' <param name="con_id">Id de conexión</param>
    ''' <param name="pcName">PC del usuario</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	08/06/2009	Created
    ''' </history>
    Public Sub changeLic(ByVal con_id As Int64, ByVal pcName As String, fromtype As Int32, totype As Int32)

        ' Verificación del type del usuario
        Dim type As Short = Server.Con.ExecuteScalar(CommandType.Text, "SELECT TYPE FROM UCM Where CON_ID = " & con_id & " AND WINPC = '" & pcName & "'")

        ' Si el type es en verdad 0
        If ((Not IsNothing(type)) AndAlso (totype = 1) And type = 0) Then

            ' Se baja una licencia documental
            subtractLicDoc()
            ' Se sube una licencia de Workflow
            UpdateLicWF()
            ' Se actualiza el type del cliente de 0 a 1
            Dim query As New StringBuilder

            query.Append("UPDATE UCM SET TYPE = 1 Where ")
            query.Append("CON_ID = " & con_id & " AND ")
            query.Append("WINPC = '" & pcName & "' AND ")
            query.Append("TYPE = 0")

            Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString())
            query = Nothing
        ElseIf ((Not IsNothing(type)) AndAlso (totype = 0) And type = 1) Then
            ' Se baja una licencia documental
            subtractLicWF()
            ' Se sube una licencia de Workflow
            UpdateLicDoc()
            ' Se actualiza el type del cliente de 0 a 1
            Dim query As New StringBuilder

            query.Append("UPDATE UCM SET TYPE = 0 Where ")
            query.Append("CON_ID = " & con_id & " AND ")
            query.Append("WINPC = '" & pcName & "' AND ")
            query.Append("TYPE = 0")

            Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString())
            query = Nothing

        End If

    End Sub

    ''' <summary>
    ''' Método que actualiza el atributo Used si la licencia es WF restando un uno
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	08/06/2009  Created     
    ''' </history>
    Private Sub subtractLicDoc()

        Dim key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Dim iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}

        Dim actConnections As Int32 = ActiveConections() - 1

        If (actConnections < 0) Then
            actConnections = 0
        End If

        Dim sql As String = "Update Lic set Used='" & Tools.Encryption.EncryptString((actConnections).ToString, key, iv) & "' Where Type = 0"

        Server.Con.ExecuteNonQuery(CommandType.Text, sql)

    End Sub

    Private Sub subtractLicWF()

        Dim key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Dim iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}

        Dim actConnections As Int32 = ActiveWorkflowConnections() - 1

        If (actConnections < 0) Then
            actConnections = 0
        End If

        Dim sql As String = "Update Lic set Used='" & Tools.Encryption.EncryptString((actConnections).ToString, key, iv) & "' Where Type = 1"

        Server.Con.ExecuteNonQuery(CommandType.Text, sql)

    End Sub
    ''' <summary>
    ''' Inserta o actualiza la conexion en la UCM
    ''' </summary>
    ''' <param name="con_id"></param>
    ''' <param name="userId"></param>
    ''' <param name="winUser"></param>
    ''' <param name="winPC"></param>
    ''' <param name="type"></param>
    ''' <param name="timeout"></param>
    ''' <remarks></remarks>
    Public Sub UpdateOrInsertActionTime(ByVal userId As Int64,
                                               ByVal winUser As String,
                                               ByVal winPC As String,
                                               ByVal con_id As Int64,
                                               ByVal timeout As Int32,
                                               ByVal type As Int32)
        If Server.isOracle Then
            'Dim rCount As Integer = Server.Con.ExecuteScalar(CommandType.Text, "SELECT count(1) from UCM where con_id=" & con_id & " AND WINUSER = '" & winUser & "' ")

            Dim q As String = "SELECT count(1) from UCM where con_id=" & con_id & " AND WINUSER='" & winUser & "' "
            Dim rcount As Int32 = Server.Con.ExecuteScalar(CommandType.Text, q)

            If rcount > 0 Then
                Server.Con.ExecuteNonQuery(CommandType.Text, "UPDATE UCM SET U_TIME = sysdate WHERE con_id = " & con_id & " AND WINUSER = '" & winUser & "' ")
            Else
                Server.Con.ExecuteNonQuery(CommandType.Text, "INSERT INTO UCM(USER_ID,C_TIME,U_TIME,WINUSER,WINPC,CON_ID,Time_out,Type) VALUES (" & userId & ", sysdate, sysdate, '" &
                         winUser & "', '" & winPC & "', " & con_id & ", " & timeout & ", " & type & ")  ")
            End If
        Else
            Dim rCount As Integer = Server.Con.ExecuteScalar(CommandType.Text, "SELECT count(1) from UCM where con_id=" & con_id & " AND WINUSER = '" & winUser & "' ")
            If rCount > 0 Then
                Server.Con.ExecuteNonQuery(CommandType.Text, "UPDATE UCM SET U_TIME = getdate() WHERE con_id = " & con_id & " AND WINUSER = '" & winUser & "' ")
            Else
                Server.Con.ExecuteNonQuery(CommandType.Text, "INSERT INTO UCM(USER_ID,C_TIME,U_TIME,WINUSER,WINPC,CON_ID,Time_out,Type) VALUES (" & userId & ", getdate(), getdate(), '" &
                         winUser & "', '" & winPC & "', " & con_id & ", " & timeout & ", " & type & ")  ")
            End If
        End If
    End Sub
#End Region
    Public Sub UpdateUserTime(ByVal TimeOut As Int32)
        Try
            If (Not IsNothing(Zamba.Membership.MembershipHelper.CurrentUser) AndAlso Zamba.Membership.MembershipHelper.CurrentUser.ConnectionId > 0) Then
                Dim userpreferences As IUserPreferences
                Dim Type As Int32
                Type = 0
                If Zamba.Membership.MembershipHelper.CurrentUser.WFLic Then
                    Type = 1
                End If
                Dim WinPc As String

                UpdateOrInsertActionTime(Zamba.Membership.MembershipHelper.CurrentUser.ID, Zamba.Membership.MembershipHelper.CurrentUser.Name, WinPc, Zamba.Membership.MembershipHelper.CurrentUser.ConnectionId, TimeOut, Type)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

End Class