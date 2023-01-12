Imports System.Text

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
    Public Shared Function ActiveWorkflowConnections() As Integer
        If Server.isOracle Then
            Return Server.Con.ExecuteScalar(CommandType.Text, "Select count(1) from UCM  Where Type = 1")
        Else
            Return Server.Con.ExecuteScalar(CommandType.Text, "Select count(1) from UCM with(nolock) Where Type = 1")
        End If
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
    Public Shared Function ActiveConections() As Int32
        If Server.isOracle Then
            Return Server.Con.ExecuteScalar(CommandType.Text, "Select count(1) from UCM Where Type <> 1")
        Else
            Return Server.Con.ExecuteScalar(CommandType.Text, "Select count(1) from UCM with(nolock) Where Type <> 1")
        End If
    End Function

    ''' <summary>
    ''' Método que actualiza el atributo Used (el atributo que dice las pc's que están actualmente conectadas a Zamba) si la licencia es documental
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub UpdateLicDoc()
        Dim key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Dim iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Dim sql As String = "Update Lic set Used='" & Tools.Encryption.EncryptString((ActiveConections() + 1).ToString, key, iv) & "' Where Type = 0"
        Server.Con.ExecuteNonQuery(CommandType.Text, sql)
    End Sub

    ''' <summary>
    ''' Método que actualiza el atributo Used (el atributo que dice las pc's que están actualmente conectadas a Zamba) si la licencia es WF
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub UpdateLicWF()

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
    Public Shared Function MakeNewConnection(ByVal UserId As Integer, ByVal WinUser As String, ByVal WinPC As String, ByVal WF As Int32, ByVal TimeOut As Int16) As Int32

        Dim actualconid As Object = Server.Con.ExecuteScalar(CommandType.Text, String.Format("SELECT CON_ID FROM UCM Where USER_ID = {0} And WINPC = '{1}'", UserId, WinPC))
        If IsDBNull(actualconid) OrElse actualconid < 1 Then
            Dim lastConID As Int32 = UcmFactory.GetNewConnection_Id()

            Dim query As String = String.Format("INSERT INTO UCM(USER_ID, C_TIME, U_TIME, WinUser, WinPC, CON_ID, Time_out, Type) VALUES({0},GetDate(),GetDate() ,'{1}','{2}',{3},{4},{5})", UserId.ToString(), WinUser, WinPC, lastConID, TimeOut, WF)
            If Server.isOracle Then query = query.Replace("GetDate()", "SYSDATE")
            Server.Con.ExecuteNonQuery(CommandType.Text, query)
            Return lastConID
        Else
            Return actualconid
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
    Public Shared Function GetNewConnection_Id() As Integer

        ' Si el servidor es SQL
        If Server.isSQLServer Then

            ' Si la tabla UCM no está vacía entonces se devuelve el máximo con_id + 1, de lo contrario, si la tabla UCM está vacía entonces se 
            ' devuelve como con_id un número aleatorio que puede ir desde 1 hasta 1000
            Dim query As New StringBuilder
            query.Append("If (Select COUNT(con_id) FROM UCM With(nolock)) > 0 ")
            query.Append("BEGIN ")
            query.Append("Select MAX(CON_ID) + 1 As LastId  FROM UCM With(nolock) End ")
            query.Append("Else ")
            query.Append("BEGIN ")
            query.Append("Select { fn TRUNCATE (RAND() * 1000, 0) } As Expr1 ")
            query.Append("End")
            Return (Server.Con.ExecuteScalar(CommandType.Text, query.ToString()))

            'Sino, si es Oracle
        Else

            Dim value As Integer = Server.Con.ExecuteScalar(CommandType.Text, "Select count(1) FROM UCM")

            ' Si la tabla UCM no tiene nada se retorna un número aleatorio (que puede ser del 1 hasta el 1000)
            If (value = 0) Then

                Randomize()
                Return (Convert.ToInt16(1 - 1000 * Rnd() + 1000))

                ' de lo contrario, se retorna como número máximo el con_id + 1
            Else
                Return (Server.Con.ExecuteScalar(CommandType.Text, "Select MAX(CON_ID) + 1 As LastId FROM UCM"))
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
    Public Shared Sub RemoveConnection(ByVal ConnectionID As Int64, ByVal pcName As String, ByVal type As Int32)

        If (verifyIfUserInUCM(ConnectionID, pcName) = True) Then
            Dim key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
            Dim iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
            Dim Usados As Int32
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
            Server.Con.ExecuteNonQuery(CommandType.Text, $"DELETE FROM UCM WHERE con_id = {ConnectionID} And winpc = '{pcName}'")
        End If
    End Sub

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
    Public Shared Sub RemoveExpiredConnection(ByVal timeout As Int16, ByVal type As Int32)

        'The new version
        Dim key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Dim iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Dim conid As Int32
        Dim usados As Int32

        'SELECCIONA EL PRIMER REGISTRO EXPIRADO
        If Server.isOracle Then

            ' Si la licencia es de Workflow, 1: Cliente - 3: Administrador
            'If ((type = 1) Or (type = 3)) Then
            If type = 1 Then
                'conid = Server.Con.ExecuteScalar(CommandType.Text, "Select CON_ID FROM UCM Where(TIME_OUT < TO_NUMBER(SYSDATE - U_TIME) * (24 * 60)) And type <> 0 And type <> 2 And rownum = 1")
                conid = Server.Con.ExecuteScalar(CommandType.Text, "Select CON_ID FROM UCM Where(TIME_OUT < TO_NUMBER(SYSDATE - U_TIME) * (24 * 60)) And Type <> 0  And rownum = 1")
                ' de lo contrario, si la licencia es documental
            Else
                'conid = Server.Con.ExecuteScalar(CommandType.Text, "Select CON_ID FROM UCM Where(TIME_OUT < TO_NUMBER(SYSDATE - U_TIME) * (24 * 60)) And Type <> 1 And Type <> 3 And rownum = 1")
                conid = Server.Con.ExecuteScalar(CommandType.Text, "Select CON_ID FROM UCM Where(TIME_OUT < TO_NUMBER(SYSDATE - U_TIME) * (24 * 60)) And Type <> 1 And rownum = 1")
            End If

        Else

            'If ((type = 1) Or (type = 3)) Then
            If type = 1 Then
                If Server.isOracle Then
                    conid = Server.Con.ExecuteScalar(CommandType.Text, "Select TOP 1 CON_ID FROM UCM Where Type <> 0 And  DATEDIFF(mi,U_TIME,SysDate())> [Time_Out]")
                Else
                    conid = Server.Con.ExecuteScalar(CommandType.Text, "Select TOP 1 CON_ID FROM UCM With(nolock) Where Type <> 0 And  DATEDIFF(mi,U_TIME,GetDate())> [Time_Out]")
                End If
            Else
                If Server.isOracle Then
                    conid = Server.Con.ExecuteScalar(CommandType.Text, "Select TOP 1 CON_ID FROM UCM Where Type <> 1  And DATEDIFF(mi,U_TIME,SysDate())> [Time_Out]")
                Else
                    conid = Server.Con.ExecuteScalar(CommandType.Text, "Select TOP 1 CON_ID FROM UCM With(nolock) Where Type <> 1  And DATEDIFF(mi,U_TIME,GetDate())> [Time_Out]")
                End If
            End If

        End If

        ' Si el conid es distinto a cero entonces hay un usuario para eliminar por tiempo de expiración
        If (conid <> 0) Then

            'BORRA EL PRIMER REGISTRO SELECCIONADO DE TIPO DE LICENCIA DOCUMENTAL o Workflow
            'If ((type = 1) Or (type = 3)) Then
            If type = 1 Then
                Server.Con.ExecuteNonQuery(CommandType.Text, "DELETE FROM UCM Where CON_ID = " & conid & " And Type <> 0 ")
            Else
                Server.Con.ExecuteNonQuery(CommandType.Text, "DELETE FROM UCM Where CON_ID = " & conid & " And Type <> 1 ")
            End If

            ' CUENTA DE LA TABLA LIC LAS LICENCIAS USADAS DE TIPO DOCUMENTAL o Workflow
            Dim count As String

            If (type = 3) Then
                type = 1
            ElseIf (type = 2) Then
                type = 0
            End If

            If (type = 1) Then
                If Server.isOracle Then
                    count = Zamba.Tools.Encryption.DecryptString(Server.Con.ExecuteScalar(CommandType.Text, "Select Used from LIC Where TYPE = " & type), key, iv)
                Else
                    count = Zamba.Tools.Encryption.DecryptString(Server.Con.ExecuteScalar(CommandType.Text, "Select Used from LIC With(nolock) Where TYPE = " & type), key, iv)
                End If
            Else
                If Server.isOracle Then
                    count = Zamba.Tools.Encryption.DecryptString(Server.Con.ExecuteScalar(CommandType.Text, "Select Used from LIC  Where TYPE <> 1"), key, iv)
                Else
                    count = Zamba.Tools.Encryption.DecryptString(Server.Con.ExecuteScalar(CommandType.Text, "Select Used from LIC With(nolock) Where TYPE <> 1"), key, iv)
                End If
            End If

            If (String.IsNullOrEmpty(count)) Then
                usados = 1
            Else
                usados = Int32.Parse(count) - 1
            End If

            ' SE ACTUALIZA LA TABLA LIC PARA LICENCIAS DOCUMENTALES o Workflow
            If usados < 0 Then usados = 0

            Dim SQL As String

            If (type = 1) Then
                SQL = "Update LIC Set Used='" & Zamba.Tools.Encryption.EncryptString(usados, key, iv) & "' Where TYPE = " & type
            Else
                SQL = "Update LIC set Used='" & Zamba.Tools.Encryption.EncryptString(usados, key, iv) & "' Where TYPE <> 1"
            End If

            Server.Con.ExecuteNonQuery(CommandType.Text, SQL)

        End If

    End Sub

    Public Shared Function verifyIfUserStillExistsInUCM(ByVal con_id As Integer) As Boolean
        Dim value As Integer = Server.Con.ExecuteScalar(CommandType.Text, "SELECT count(1) FROM UCM Where CON_ID = " & con_id)

        If (value >= 1) Then
            Return (True)
        Else
            Return (False)
        End If

    End Function

    Public Shared Sub RemoveConnection(ByVal ConnectionID As Int64)

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
    Public Shared Function GetConnectionType(conid As Int64) As Int32
        Dim type As Int32
        type = Server.Con.ExecuteScalar(CommandType.Text, "SELECT TYPE FROM UCM Where CON_ID =" & conid)
        Return type
    End Function
    Public Shared Function UserUniqueConnection(ByVal ConnectionId As Int64) As Boolean
        Dim value As Integer = Server.Con.ExecuteScalar(CommandType.Text, "SELECT count(1) FROM UCM Where USER_ID in (SELECT USER_ID FROM UCM Where  CON_ID = " & ConnectionId & ")")

        If (value > 1) Then
            Return (False)
        Else
            Return (True)
        End If

    End Function
    Public Shared Function GetUserIdByConId(ConnectionId) As Int64
        Dim UserId As Int64 = 0
        UserId = Server.Con.ExecuteScalar(CommandType.Text, String.Format("SELECT USER_ID FROM UCM Where CON_ID = {0}", ConnectionId))
        Return UserId
    End Function
    Public Shared Function GetFirstExpiredConnection() As Int32
        Dim ConnectionID As Int32 = 0
        If Server.isOracle Then
            ConnectionID = Server.Con.ExecuteScalar(CommandType.Text, "SELECT CON_ID FROM UCM Where(TIME_OUT < TO_NUMBER(SYSDATE - U_TIME) * (24 * 60)) AND rownum = 1")
        Else
            ConnectionID = Server.Con.ExecuteScalar(CommandType.Text, "SELECT TOP 1 CON_ID FROM UCM Where DATEDIFF(mi,U_TIME,GetDate())> [Time_Out]")
        End If

        Return ConnectionID
    End Function

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
    Public Shared Function verifyIfUserInUCM(ByVal con_id As Integer, ByVal pcName As String) As Boolean
        Dim value As Integer
        If Server.isOracle Then
            value = Server.Con.ExecuteScalar(CommandType.Text, "SELECT count(1) FROM UCM  Where CON_ID = " & con_id & " AND WINPC = '" & pcName & "'")
        Else
            value = Server.Con.ExecuteScalar(CommandType.Text, "SELECT count(1) FROM UCM with(nolock) Where CON_ID = " & con_id & " AND WINPC = '" & pcName & "'")
        End If

        If (value = 1) Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Si el usuario ya esta en la tabla, se trae el con_id
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
    Public Shared Function GetConIDUser(ByRef userId As Int32, ByRef winUser As String, ByRef winPC As String, ByVal type As Int32) As Int64
        Dim query As New StringBuilder()

        Try

            If Server.isOracle Then
                query.Append("SELECT con_ID FROM UCM Where ")
            Else
                query.Append("SELECT con_ID FROM UCM  with(nolock)  Where ")
            End If
            query.Append("USER_ID = " & userId & " AND ")
            query.Append("WINUSER = '" & winUser & "' AND ")
            query.Append("WINPC = '" & winPC & "' AND ")
            query.Append("TYPE = " & type)

            Return Server.Con.ExecuteScalar(CommandType.Text, query.ToString())
        Finally
            query = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Método que actualiza el type del cliente de 0 (licencia documental) a Workflow (licencia de Workflow) y actualiza los LIC
    ''' </summary>
    ''' <param name="con_id">Id de conexión</param>
    ''' <param name="pcName">PC del usuario</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	08/06/2009	Created
    ''' </history>
    Public Shared Sub changeLicDocToLicWF(ByVal con_id As Integer, ByVal pcName As String)

        Dim type As Short
        ' Verificación del type del usuario
        If Server.isOracle Then
            type = Server.Con.ExecuteScalar(CommandType.Text, "SELECT TYPE FROM UCM Where CON_ID = " & con_id & " AND WINPC = '" & pcName & "'")
        Else
            type = Server.Con.ExecuteScalar(CommandType.Text, "SELECT TYPE FROM UCM  with(nolock) Where CON_ID = " & con_id & " AND WINPC = '" & pcName & "'")
        End If

        ' Si el type es en verdad 0
        If ((Not IsNothing(type)) AndAlso (type = 0)) Then

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

        End If

    End Sub

    ''' <summary>
    ''' Método que actualiza el atributo Used si la licencia es WF restando un uno
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	08/06/2009  Created     
    ''' </history>
    Private Shared Sub subtractLicDoc()

        Dim key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Dim iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}

        Dim actConnections As Int32 = ActiveConections() - 1

        If (actConnections < 0) Then
            actConnections = 0
        End If

        Dim sql As String = "Update Lic set Used='" & Tools.Encryption.EncryptString((actConnections).ToString, key, iv) & "' Where Type = 0"

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
    Public Shared Sub UpdateOrInsertActionTime(ByVal userId As Int64,
                                               ByVal winUser As String,
                                               ByVal winPC As String,
                                               ByVal conId As Int64,
                                               ByVal timeout As Int32,
                                               ByVal type As Int32)
        If Server.isOracle Then
            Dim q As String = "SELECT count(1) from UCM where USER_ID=" & userId & " AND WINUSER='" & winUser & "' AND TYPE=" & type
            Dim count As Int32 = Server.Con.ExecuteScalar(CommandType.Text, q)

            If count > 0 Then
                q = "UPDATE UCM SET U_TIME=sysdate WHERE con_id=" & conId & " AND WINUSER='" & winUser & "'"
            Else
                q = "INSERT INTO UCM(USER_ID,C_TIME,U_TIME,WINUSER,WINPC,CON_ID,Time_out,Type) " &
                    "VALUES (" & userId & ",sysdate,sysdate,'" & winUser & "','" & winPC & "'," & conId & "," & timeout & "," & type & ")"
            End If
            If Server.isSQLServer Then
                q = q.Replace("sysdate", "getdate()")
            End If
            Server.Con.ExecuteNonQuery(CommandType.Text, q)
        Else
            Dim parvalues() As Object = {userId, winUser, winPC, conId, timeout, type}
            Server.Con.ExecuteNonQuery("zsp_license_100_UpdateOrInsertActionTime", parvalues)
        End If
    End Sub
#End Region

End Class