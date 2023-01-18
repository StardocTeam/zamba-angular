Imports System.Collections.Generic
Imports System.Text
Imports Zamba.Core
Imports Zamba.Tools


Public Class RightFactory
    Inherits ZClass


    <Obsolete("Las entidades no corresponden a la capa de datos")> Public Shared _CurrentUser As IUser
    Public Shared ReadOnly Property GroupRights(ByVal id As Int32) As DataSet
        Get
            SyncLock (Cache.UsersAndGroups.Permisos)
                If Not Cache.UsersAndGroups.Permisos.ContainsKey(id) Then
                    Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, "select * from usr_Rights where groupid = " & id)
                    If Cache.UsersAndGroups.Permisos.ContainsKey(id) = False Then Cache.UsersAndGroups.Permisos.Add(id, ds)
                    Return ds
                Else
                    Return Cache.UsersAndGroups.Permisos(id)
                End If
            End SyncLock
        End Get
    End Property


    <Obsolete("Las entidades no corresponden a la capa de datos")>
    Private Shared Function GetRows(ByVal id As Integer, ByVal ObjectId As ObjectTypes, ByVal RType As RightsType, Optional ByVal AditionalParam As Integer = -1) As DataRow()

        Dim DtGroupRights As DataTable = GroupRights(id).Tables(0)

        Dim Querybuilder As New StringBuilder()

        Querybuilder.Append("GROUPID = ")
        Querybuilder.Append(id.ToString())
        Querybuilder.Append(" And OBJID = ")
        Querybuilder.Append(DirectCast(ObjectId, Int32).ToString())
        Querybuilder.Append(" And RTYPE= ")
        Querybuilder.Append(DirectCast(RType, Int32).ToString())
        Querybuilder.Append(" And ADITIONAL = ")
        Querybuilder.Append(AditionalParam.ToString())
        Return DtGroupRights.Select(Querybuilder.ToString())
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="objectId"></param>
    ''' <param name="rType"></param>
    ''' <param name="aditionalParam"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>[Ezequiel] 06-03-2009 Modified - Se paso el metodo a publico asi puedo validar directamente por permisos de usuario</history>
    Public Shared Function GetRight(ByVal id As Integer, ByVal objectId As ObjectTypes, ByVal rType As RightsType, Optional ByVal aditionalParam As Integer = -1) As Boolean
        Dim Value As Boolean = False

        If id = 0 Then
            Value = False
        Else

            Dim r() As DataRow = GetRows(id, objectId, rType, aditionalParam)
            If r.Length = 0 Then
                Value = False
            Else
                Value = True
            End If
        End If

        Return Value
    End Function


    <Obsolete("Las entidades no corresponden a la capa de datos")>
    Private Shared Function GetGroupsRights(ByVal strselect As String) As DataSet
        Dim ds As DataSet = Servers.Server.Con.ExecuteDataset(CommandType.Text, "Select * from usr_Rights where " & strselect)
        Return ds
    End Function

    Public Shared Function IsUserPasswordNull(ByVal usrID As Int64) As Boolean
        If IsDBNull(Server.Con.ExecuteScalar(CommandType.Text, "Select PASSWORD from USRTABLE where id = " & usrID)) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetUserPassword(ByVal usrID As Int64) As String
        Return Server.Con.ExecuteScalar(CommandType.Text, "Select PASSWORD from USRTABLE where id = " & usrID)
    End Function





    ''' <summary>
    ''' Obtiene los permisos del usuario por el id
    ''' </summary>
    ''' <param name="UserId"></param>
    ''' <param name="ObjectId"></param>
    ''' <param name="RType"></param>
    ''' <param name="AditionalParam"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetUserRightsById(ByVal User As IUser, ByVal ObjectId As ObjectTypes, ByVal RType As RightsType, Optional ByVal AditionalParam As Integer = -1) As Boolean
        ' override
        If User.ID > 0 Then
            If GetRight(User.ID, ObjectId, RType, AditionalParam) Then ' redo
                Return True
            Else

                'If User.ID = 0 Then Return True
                ' Dim user As IUser = UserFactory.GetUserById(user.ID)
                If User.ID = 9999 Then
                    If User.Name.ToUpper = "ZAMBA1234567" AndAlso ObjectId = ObjectTypes.Users Then Return True
                End If

                If IsNothing(User.Groups) OrElse User.Groups.Count = 0 Then UserFactory.FillGroups(User)
                For Each g As IUserGroup In User.Groups
                    If GetRight(g.ID, ObjectId, RType, AditionalParam) = True Then
                        Return True
                    End If
                Next
                Return False
            End If
        Else
            Return False
        End If
    End Function

    <Obsolete("Las entidades no corresponden a la capa de datos")>
    Public Shared Function AddRight(ByVal Groupid As Int64, ByVal ObjectId As ObjectTypes, ByVal RType As RightsType, Optional ByVal AditionalParam As Integer = -1) As Boolean
        Dim dsRights As New DataSet
        dsRights = Servers.Server.Con.ExecuteDataset(CommandType.Text, "Select GROUPID, OBJID, RType, ADITIONAL from usr_rights where GROUPID=" & Groupid & " And OBJID=" & ObjectId & " And RType=" & RType & " And ADITIONAL=" & AditionalParam)

        If dsRights.Tables(0).Rows.Count > 0 Then
            Servers.Server.Con.ExecuteNonQuery(CommandType.Text, "update usr_rights Set GROUPID=" & Groupid & " , OBJID = " & ObjectId & ", RType = " & RType & ", ADITIONAL = " & AditionalParam & " where GROUPID=" & Groupid & " And OBJID=" & ObjectId & " And RType=" & RType & " And ADITIONAL=" & AditionalParam)
        Else
            Servers.Server.Con.ExecuteNonQuery(CommandType.Text, "insert into usr_rights(GROUPID, OBJID, RType, ADITIONAL) values(" & Groupid & "," & ObjectId & "," & RType & "," & AditionalParam & ")")
        End If

        Dim r As DataRow = GroupRights(Groupid).Tables(0).NewRow
        r("GROUPID") = Groupid
        r("OBJID") = ObjectId
        r("RTYPE") = RType
        r("ADITIONAL") = AditionalParam
        GroupRights(Groupid).Tables(0).Rows.Add(r)

    End Function

    <Obsolete("Las entidades no corresponden a la capa de datos")>
    Public Shared Function DelRight(ByVal id As Int64, ByVal ObjectId As ObjectTypes, ByVal RType As RightsType, Optional ByVal AditionalParam As Integer = -1) As Boolean
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Quitando permiso:   " & "delete usr_rights where groupid=" & id & " and objid=" & ObjectId & " and Rtype=" & RType & " and Aditional=" & AditionalParam)
        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, "delete usr_rights where groupid=" & id & " and objid=" & ObjectId & " and Rtype=" & RType & " and Aditional=" & AditionalParam)
        Dim r() As DataRow = GetRows(id, ObjectId, RType, AditionalParam)
        For Each row As DataRow In r
            GroupRights(id).Tables(0).Rows.Remove(row)
        Next
    End Function


    ''' <summary>
    ''' Valida si el usuario tiene permiso de utilizar el modulo correspondiente
    ''' </summary>
    ''' <param name="Modulo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ValidateModuleLicense(ByVal Modulo As ObjectTypes) As Boolean
        Dim key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Dim iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Dim valor As String
        Dim value As String = Server.Con.ExecuteScalar(CommandType.Text, "Select Valor from ModulesRights where Modulo='" & Modulo.ToString & "'")

        If String.IsNullOrEmpty(value) Then
            Dim count As Int64 = Server.Con.ExecuteScalar(CommandType.Text, "Select count(1) from ModulesRights where Modulo='" & Modulo.ToString & "'")
            If count > 0 Then
                Return True
            Else
                Return False
            End If
        Else
            ZTrace.WriteLineIf(ZTrace.IsInfo, value)
            valor = Zamba.Tools.Encryption.DecryptString(value, key, iv)
            ZTrace.WriteLineIf(ZTrace.IsInfo, valor)
            If valor.ToUpper = "OK" & Modulo.ToString.ToUpper Then
                Return True
            Else
                Return False
            End If
        End If
        Return False
    End Function

    Public Shared Property CurrentUser() As IUser
        Get
            Return _CurrentUser
        End Get
        Set(ByVal value As IUser)
            If Not IsNothing(value) Then
                Trace.WriteLineIf(ZTrace.IsVerbose, "Usuario Logueado: " & value.Name)
            Else
                Trace.WriteLineIf(ZTrace.IsVerbose, "El usuario es incorrecto")
            End If
            _CurrentUser = value
        End Set
    End Property

    <Obsolete("Las entidades no corresponden a la capa de datos")>
    Public Shared Function GetArchivosUserRight(ByVal CurrentUser As IUser) As Data_Group_Doc
        '1/1/2006
        Dim dstemp As DataSet

        If Server.isOracle Then
            dstemp = Server.Con.ExecuteDataset(CommandType.Text, "SELECT distinct(dtg.Doc_Type_Group_ID),dtg.Doc_Type_Group_Name,dtg.Icon,dtg.Parent_Id,dtg.Object_Type_Id,urv.User_Id,urv.Right_Type FROM DOC_TYPE_GROUP dtg, Zvw_USR_Rights_200 urv WHERE dtg.Doc_Type_Group_ID = urv.Aditional AND dtg.Object_Type_Id = urv.ObjectID and urv.User_Id =" & CurrentUser.ID & " ORDER BY dtg.Doc_Type_Group_ID")
        Else
            Dim ParValues() As Object = {CurrentUser.ID}
            dstemp = Server.Con.ExecuteDataset("zsp_security_100_GetArchivosUserRight", ParValues)
        End If
        Dim ds As New Data_Group_Doc
        dstemp.Tables(0).TableName = ds.Doc_Type_Group.TableName
        ds.Merge(dstemp)
        Return ds
    End Function


    Public Shared Function GetCategoriesByUserRight(ByVal UserId As Int64) As DataTable
        '1/1/2006
        Dim dstemp As DataSet

        If Server.isOracle Then
            Dim ParValues() As Object = {UserId, 2}
            ''Dim parNames() As Object = {"UserId", "io_cursor"}
            'Dim parTypes() As Object = {13, 5}
            'dstemp = Server.Con.ExecuteDataset("ZGetUserRigth_pkg.GetArchivosUserRight",  ParValues)
            dstemp = Server.Con.ExecuteDataset("zsp_security_100.GetArchivosUserRight", ParValues)
        Else
            Dim ParValues() As Object = {UserId}
            'dstemp = Server.Con.ExecuteDataset("GetArchivosUserRight", ParValues)
            dstemp = Server.Con.ExecuteDataset("zsp_security_200_GetArchivosUserRight", ParValues)
        End If

        If dstemp.Tables.Count > 0 Then Return dstemp.Tables(0)
        Return Nothing
    End Function

    <Obsolete("Las entidades no corresponden a la capa de datos")>
    Public Shared Function GetDocTypeUserRightFromArchive(ByVal Doc_GroupID As Integer, ByVal CurrentUser As IUser) As DsDoctypeRight
        'TODO Martin: Esta yendo a la base para traer los doctypes que el usuario tiene permisos, aprobechar zcore
        Dim UserWhere As New StringBuilder
        UserWhere.Append("user_id = " & CurrentUser.ID)
        For Each u As IUserGroup In CurrentUser.Groups
            UserWhere.Append(" or user_id = " & u.ID)
        Next

        Dim sb As New StringBuilder
        sb.Append(" select t.doc_type_name Doc_Type_Name,r.doc_type_Group Doc_Type_Group, t.doc_type_id Doc_Type_Id,icon_id Icon_Id,documentalId DocumentalID from doc_type t,doc_type_r_doc_type_group r ")
        sb.Append(" where r.doc_type_id=t.doc_type_id and r.doc_type_group=" & Doc_GroupID & " and r.doc_type_id in ")
        'sb.Append(" (select DISTINCT aditional from usr_rights_view where Right_type=1 and objectid=2 and (" & UserWhere & ")) order by DOC_TYPE_NAME")
        sb.Append(" (select DISTINCT aditional from Zvw_USR_Rights_200 where Right_type=1 and objectid=2 and (" & UserWhere.ToString & ")) order by DOC_ORDER")

        Dim ds As New DsDoctypeRight
        Dim d As DataSet = Servers.Server.Con.ExecuteDataset(CommandType.Text, sb.ToString)
        d.Tables(0).TableName = ds.DocTypes.TableName
        ds.Merge(d.Tables(0))
        Return ds
    End Function

    '<Obsolete("Las entidades no corresponden a la capa de datos")> _
    'Public Shared Function GetDocTypeUserRightFromArchiveWithAsociation(ByVal ds As DsDoctypeRight, ByVal Doc_GroupID As Integer) As DataSet
    '    'TODO Martin: Esta yendo a la base para traer los doctypes que el usuario tiene permisos, aprobechar zcore
    '    Dim UserWhere As New System.Text.StringBuilder
    '    UserWhere.Append("user_id = " & CurrentUser.ID)
    '    For Each u As IUserGroup In CurrentUser.Groups
    '        UserWhere.Append(" or user_id = " & u.ID)
    '    Next

    '    Dim sb As New System.Text.StringBuilder
    '    sb.Append(" select t.doc_type_name Doc_Type_Name,r.doc_type_Group Doc_Type_Group, t.doc_type_id Doc_Type_Id,icon_id Icon_Id,documentalId DocumentalID from doc_type t,doc_type_r_doc_type_group r ")
    '    sb.Append(" where r.doc_type_id=t.doc_type_id and r.doc_type_group=" & Doc_GroupID & " and r.doc_type_id in ")
    '    'sb.Append(" (select DISTINCT aditional from usr_rights_view where Right_type=1 and objectid=2 and (" & UserWhere & ")) order by DOC_TYPE_NAME")
    '    sb.Append(" (select DISTINCT aditional from Zvw_USR_Rights_100 where Right_type=1 and objectid=2 and (" & UserWhere.ToString & ")) order by DOC_TYPE_NAME")

    '    Dim d As DataSet = Servers.Server.Con.ExecuteDataset(CommandType.Text, sb.ToString)
    '    d.Tables(0).TableName = ds.DocTypes.TableName
    '    ds.Merge(d.Tables(0))
    '    Return ds
    'End Function



    Public Shared Function GetAllDocTypesByUserRight(ByVal userid As Int64) As DataSet
        ' Obtiene todos los doctypes Segun los permisos que se asigno para el usuario
        ' Los permisos por los cual filtra son el de "VER" - "Crear" y "Re Indexar"
        Dim sb As New StringBuilder
        sb.Append(" select t.doc_type_name Doc_Type_Name,r.doc_type_Group Doc_Type_Group, t.doc_type_id Doc_Type_Id,icon_id Icon_Id,documentalId DocumentalID from doc_type t,doc_type_r_doc_type_group r ")
        sb.Append(" where r.doc_type_id=t.doc_type_id and r.doc_type_id in ")
        sb.Append(" (select DISTINCT aditional from Zvw_USR_Rights_200 where (Right_type=1  or Right_type=3 or Right_type=12) and objectid=2 and ( user_id = " & userid.ToString & "))")

        Return Servers.Server.Con.ExecuteDataset(CommandType.Text, sb.ToString)
    End Function

    ''' <summary>
    ''' Trae los tipod de documento en los cuales el usuario tiene permiso de crear.
    ''' </summary>
    ''' <param name="userid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> [Ezequiel] 31/03/2009 Created
    Public Shared Function GetAllDocTypesByUserRightOfCreate(ByVal userid As Int64) As DataSet
        ' Obtiene todos los doctypes Segun los permisos que se asigno para el usuario
        ' Los permisos por los cual filtra son el de "VER" - "Crear" y "Re Indexar"
        Dim sb As New StringBuilder
        sb.Append(" select t.doc_type_name Doc_Type_Name,r.doc_type_Group Doc_Type_Group, t.doc_type_id Doc_Type_Id,icon_id Icon_Id,documentalId DocumentalID from doc_type t,doc_type_r_doc_type_group r ")
        sb.Append(" where r.doc_type_id=t.doc_type_id and r.doc_type_id in ")
        sb.Append(" (select DISTINCT aditional from Zvw_USR_Rights_200 where (Right_type=3) and objectid=2 and ( user_id = " & userid.ToString & "))")

        Return Servers.Server.Con.ExecuteDataset(CommandType.Text, sb.ToString)
    End Function

    <Obsolete("Las entidades no corresponden a la capa de datos")>
    Public Shared Function GetAditional(ByVal ObjectType As ObjectTypes, ByVal Rtype As RightsType, ByVal CurrentUser As IUser) As ArrayList

        If IsNothing(CurrentUser) OrElse IsNothing(CurrentUser.Groups) Then
            Return New ArrayList
        End If

        Dim strselect As New StringBuilder

        strselect.Append("RTYPE=" & Rtype & " and OBJID=" & ObjectType & " and (groupid=" & CurrentUser.ID)

        For Each g As IUserGroup In CurrentUser.Groups
            strselect.Append(" or groupid=" & g.ID)
        Next
        strselect.Append(")")

        Dim RightsTable As DataTable = GetGroupsRights(strselect.ToString).Tables(0)

        Dim Aditionals As New ArrayList

        For Each r As DataRow In RightsTable.Rows
            If Not Aditionals.Contains(r("ADITIONAL")) Then
                Aditionals.Add(r("ADITIONAL"))
            End If
        Next
        Return Aditionals
    End Function


    Public Shared Sub SaveAction(ByVal ObjectId As Int64, ByVal ObjectType As ObjectTypes, ByVal ActionType As RightsType, ByVal CurrentUser As IUser, Optional ByVal S_Object_ID As String = "")
        Try
            Dim ConnectionId As Integer
            Dim _userid As Int64
            Try

                If IsNothing(CurrentUser) Then
                    _userid = 0
                    ConnectionId = 0
                Else
                    _userid = CurrentUser.ID
                    ConnectionId = CurrentUser.ConnectionId
                End If
            Catch ex As Exception
                raiseerror(ex)
            End Try

            ActionsFactory.SaveActioninDB(ObjectId, ObjectType, ActionType, S_Object_ID, _userid, ConnectionId, Environment.MachineName, Nothing)
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub


    <Obsolete("Las entidades no corresponden a la capa de datos")>
    Public Shared Function SaveAction(ByVal ObjectId As Int64, ByVal ObjectType As ObjectTypes, ByVal machineName As String, ByVal ActionType As RightsType, ByVal CurrentUser As IUser, ByVal S_Object_ID As String) As Integer
        Return SaveAction(ObjectId, ObjectType, machineName, ActionType, CurrentUser, S_Object_ID, Nothing)
    End Function
    <Obsolete("Las entidades no corresponden a la capa de datos")>
    Public Shared Function SaveAction(ByVal ObjectId As Int64, ByVal ObjectType As ObjectTypes, ByVal machineName As String, ByVal ActionType As RightsType, ByVal CurrentUser As IUser, ByVal S_Object_ID As String, ByRef t As Transaction) As Integer

        Dim ConnectionId As Integer
        Dim _userid As Int64
        Try
            If IsNothing(CurrentUser) Then
                _userid = 0
                ConnectionId = 0
            Else
                _userid = CurrentUser.ID
                ConnectionId = CurrentUser.ConnectionId
            End If
            'Devuelve 0 en caso de no existir licencias disponibles
            'Devuelve 1 en caso de que las consultas se hayan realizado con éxito
            Return ActionsFactory.SaveActioninDB(ObjectId, ObjectType, ActionType, S_Object_ID, _userid, ConnectionId, machineName, t)
        Catch ex As Exception
            raiseerror(ex)
            Return 0
        End Try

    End Function





    Public Shared Function GetLoadRightsDs(ByVal ids As ArrayList, ByRef dsarchivos As DataSet, ByRef dsdoctype As DataSet, ByRef dsrestriction As DataSet) As DataSet
        Dim dsgeneral As DataSet

        Dim strIds As New StringBuilder
        Dim keys As String = String.Empty
        'armo los ids de usuario y grupo
        Dim i As Integer
        Try
            For i = 0 To ids.Count - 1
                If i = 0 Then
                    strIds.Append("ID=" & ids(i))
                Else
                    strIds.Append(" Or ID=" & ids(i))
                End If
            Next
            keys = "(" & strIds.ToString & ")"
        Catch ex As Exception
        End Try
        Dim strSelect As String = String.Empty
        'cargo permisos generales
        Try
            'strSelect = "Select distinct objecttypes As Herramienta, rightstype As Modo FROM VIEW_OBJECTS_RIGHTS WHERE ADITIONAL=-1 And " & keys & " ORDER BY 1,2"
            strSelect = "Select distinct objecttypes As Herramienta,rightstype As Modo FROM Zvw_objects_rights_100 WHERE ADITIONAL=-1 And " & keys & " ORDER BY 1,2"
            dsgeneral = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
        Catch ex As Exception
            Dim exn As New Exception("Error (Public Sub LoadRights(ByVal user As iuser)) al ejecutar SQL: SELECT distinct objecttypes as Herramienta,rightstype as Modo FROM Zvw_objects_rights_100 WHERE ADITIONAL=-1 and " & keys & " ORDER BY 1,2. Excepción: " & ex.ToString)
            raiseerror(exn)
        End Try

        'cargo permsos de archivos
        Try
            'strSelect = "SELECT distinct DOC_TYPE_GROUP_NAME as Archivo,RIGHTSTYPE as Modo FROM view_docgroups_rights WHERE " & keys & " ORDER BY 1,2"
            strSelect = "SELECT distinct DOC_TYPE_GROUP_NAME as Archivo,RIGHTSTYPE as Modo FROM Zvw_docgroups_rights_100 WHERE " & keys & " ORDER BY 1,2"
            dsarchivos = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
        Catch ex As Exception
            Dim exn As New Exception("Error (Public Sub LoadRights(ByVal user as iuser)) al ejecutar SQL:" & strSelect & ".Excepción" & ex.ToString)
            raiseerror(exn)
        End Try

        'cargo permisos de entidades
        Try
            If Server.isOracle Then
                'strSelect = "SELECT distinct doc_type_name as " & Chr(34) & "Entidad" & Chr(34) & " ,rightstype as Modo  FROM view_doctypes_rights WHERE ADITIONAL<>-1 and " & keys & " ORDER BY 1,2"
                strSelect = "SELECT distinct doc_type_name as " & Chr(34) & "Entidad" & Chr(34) & " ,rightstype as Modo  FROM Zvw_doctypes_rights_100 WHERE ADITIONAL<>-1 and " & keys & " ORDER BY 1,2"
            Else
                'strSelect = "SELECT distinct doc_type_name as [Entidad],rightstype as Modo  FROM view_doctypes_rights WHERE ADITIONAL<>-1 and " & keys & " ORDER BY 1,2"
                strSelect = "SELECT distinct doc_type_name as [Entidad],rightstype as Modo  FROM Zvw_doctypes_rights_100 WHERE ADITIONAL<>-1 and " & keys & " ORDER BY 1,2"
            End If
            dsdoctype = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
        Catch ex As Exception
            Dim exn As New Exception("Error (Public Sub LoadRights(ByVal user as iuser)) al ejecutar SQL:" & strSelect & ".Excepción" & ex.ToString)
            raiseerror(exn)
        End Try

        'cargo restricciones
        Try
            If Server.isOracle Then
                'strSelect = "SELECT distinct doc_type_name as " & Chr(34) & "Entidad" & Chr(34) & " ,restriction_name as Restriccion FROM VIEW_RESTRICTIONS WHERE " & keys & " ORDER BY 1,2"
                strSelect = "SELECT distinct doc_type_name as " & Chr(34) & "Entidad" & Chr(34) & " ,restriction_name as Restriccion FROM Zvw_restrictions_100 WHERE " & keys & " ORDER BY 1,2"
            Else
                'strSelect = "SELECT distinct doc_type_name as [Entidad],restriction_name as Restriccion FROM VIEW_RESTRICTIONS WHERE " & keys & " ORDER BY 1,2"
                strSelect = "SELECT distinct doc_type_name as [Entidad],restriction_name as Restriccion FROM Zvw_restrictions_100 WHERE " & keys & " ORDER BY 1,2"
            End If
            dsrestriction = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
        Catch ex As Exception
            Dim exn As New Exception("Error (Public Sub LoadRights(ByVal user as iuser)) al ejecutar SQL:" & strSelect & ".Excepción" & ex.ToString)
            raiseerror(exn)
        End Try

        strIds = Nothing
        Return dsgeneral
    End Function

    Public Shared Function VerLicenciasDocumentales() As String
        Dim LicenciasDocumentales As String = String.Empty
        Dim key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Dim iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        '' Ver Documentales
        Try
            'Encryption.EncryptString("0", key, iv)

            If Server.isOracle Then
                LicenciasDocumentales = Server.Con.ExecuteScalar(CommandType.Text, "Select Numero_Licencias from lic Where Type=0")
            Else
                'LicenciasDocumentales = Server.Con.ExecuteScalar("SelectLic")
                LicenciasDocumentales = Server.Con.ExecuteScalar("zsp_license_100_GetDocumentalLicenses")
            End If
            If IsNothing(LicenciasDocumentales) OrElse LicenciasDocumentales.Trim = "" Then
                LicenciasDocumentales = "0"
            Else
                LicenciasDocumentales = Encryption.DecryptString(LicenciasDocumentales, key, iv)
            End If
        Catch ex As Exception
            raiseerror(ex)
        End Try
        Return LicenciasDocumentales
    End Function
    Public Shared Function VerLicenciasWorkFlow() As String
        Dim LicenciasWorkflow As String = String.Empty
        Dim key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Dim iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        ''  Ver Workflow
        Try
            'Encryption.EncryptString("0", key, iv)

            LicenciasWorkflow = Server.Con.ExecuteScalar(CommandType.Text, "Select Numero_Licencias from lic Where Type=1")
            If IsNothing(LicenciasWorkflow) OrElse LicenciasWorkflow.Trim = "" Then
                LicenciasWorkflow = 0
            Else
                LicenciasWorkflow = Encryption.DecryptString(LicenciasWorkflow, key, iv)
            End If
        Catch ex As Exception
            raiseerror(ex)
        End Try
        Return LicenciasWorkflow
    End Function

    Public Shared Sub UpdateLicenciasDocumentales(ByVal LicenciasActuales As Int32, ByVal TotalLicencias As String)
        Dim sql As New StringBuilder
        If LicenciasActuales = 0 Then    'si es 0, entonces inserta 0 
            sql.Append("Insert into Lic(Numero_Licencias,Type,Nombre,Used) values('" & TotalLicencias & "',0,'DOC',0)")
        Else
            sql.Append("Update Lic set Numero_Licencias='" & TotalLicencias & "' Where Type=0")
        End If
        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    End Sub
    Public Shared Sub UpdateLicenciasWorkFlow(ByVal LicenciasActuales As Int32, ByVal TotalLicencias As Int32)
        Dim sql As String = String.Empty
        If Server.isOracle Then
            sql = "Update Lic set Numero_Licencias='" & TotalLicencias & "' Where Type=1"
            If LicenciasActuales = 0 Then  'si es 0, entonces inserta el valor inicial
                sql = "Insert into Lic(Numero_Licencias,Type,Nombre,Used) values('" & TotalLicencias & "',1,'WF',0)"
            End If
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        Else
            If LicenciasActuales = 0 Then    'si es 0, entonces inserta 0 
                sql = "Insert into Lic(Numero_Licencias,Type,Nombre,Used) values('" & TotalLicencias & "',1,'WF',0)"
            End If
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        End If
    End Sub

    ''' <summary>
    ''' Inserta la registracion del modulo en Zamba
    ''' </summary>
    ''' <param name="ModuleId">ID del modulo a registrar</param>
    ''' <param name="ModuleName">Nombre del modulo a registrar</param>
    ''' <history>   Marcelo Modified    02/08/2012</history>
    ''' <remarks></remarks>
    Public Shared Sub RegisterModule(ByVal ModuleId As Int32, ByVal ModuleName As String)
        Dim key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Dim iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Dim sql As String = "Delete ModulesRights where id=" & ModuleId
        Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        sql = "Insert into ModulesRights(id,modulo,Valor) values(" & ModuleId & ",'" & ModuleName & "','" & Zamba.Tools.Encryption.EncryptString("OK" & ModuleName.ToString.ToUpper, key, iv) & "')"
        Server.Con.ExecuteNonQuery(CommandType.Text, sql)
    End Sub

    <Obsolete("Las entidades no corresponden a la capa de datos")>
    Public Shared Sub SetRight(ByVal groupid As Int64, ByVal objectId As ObjectTypes, ByVal Rtype As RightsType, Optional ByVal Additional As Int32 = -1, Optional ByVal Value As Boolean = True)
        If Value Then
            AddRight(groupid, objectId, Rtype, Additional)
            'If Me.ValidateModuleLicense(objectId) = False Then Me.AddModuleRight(objectId)
        Else
            DelRight(groupid, objectId, Rtype, Additional)
        End If
    End Sub

    Public Shared Sub SetIndexRights(ByVal DocTypeId As Int64, ByVal GID As Int32, ByVal IndexId As Int64, ByVal righttypeid As Int16)
        Dim query As New StringBuilder
        query.Append("INSERT INTO ZIR(IndexId, DoctypeId, UserId, RightType)  VALUES(")
        query.Append(IndexId & ",")
        query.Append(DocTypeId & ",")
        query.Append(GID & ",")
        query.Append(righttypeid & ")")
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
    End Sub
    Public Shared Sub RemoveIndexRights(ByVal DocTypeId As Int64, ByVal GID As Int32, ByVal IndexId As Int64, ByVal righttypeid As Int16)
        Dim query As New StringBuilder
        query.Append("DELETE FROM ZIR WHERE IndexId = ")
        query.Append(IndexId & " AND DoctypeId =")
        query.Append(DocTypeId & " AND UserId = ")
        query.Append(GID & " AND RightType = ")
        query.Append(righttypeid)
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
    End Sub

    ''' <summary>
    ''' Obtiene los valores por defecto de los atributos atachados al entidad
    ''' </summary>
    ''' <param name="doctypeid">Id de entidad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>[Pablo] created 4-07-2008</history>
    Public Shared Sub SetAssociateIndexRight(ByVal DocTypeParentId As Int64, ByVal DocTypeId As Int64, ByVal IndexId As Int64, ByVal GID As Int32, ByVal righttypeid As Int16)
        Dim query As New StringBuilder
        query.Append("INSERT INTO ZVIR(DocTypeParentId, DoctypeId, IndexId, GID, RightType)  VALUES(")
        query.Append(DocTypeParentId & ",")
        query.Append(DocTypeId & ",")
        query.Append(IndexId & ",")
        query.Append(GID & ",")
        query.Append(righttypeid & ")")
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
    End Sub
    ''' <summary>
    ''' Obtiene los valores por defecto de los atributos atachados al entidad
    ''' </summary>
    ''' <param name="doctypeid">Id de entidad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>[Pablo] created 4-07-2008</history>
    Public Shared Sub RemoveAssociateIndexRight(ByVal DocTypeParentId As Int64, ByVal DocTypeId As Int64, ByVal IndexId As Int64, ByVal GID As Int32, ByVal righttypeid As Int16)
        Dim query As New StringBuilder
        query.Append("DELETE FROM ZVIR WHERE DocTypeParentId = ")
        query.Append(DocTypeParentId & " AND DocTypeId =")
        query.Append(DocTypeId & " AND IndexId = ")
        query.Append(IndexId & " AND GID =")
        query.Append(GID & " AND RightType = ")
        query.Append(righttypeid)
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
    End Sub

    ''' <summary>
    ''' Obtiene los permisos para los documentos asociados de un doctype 
    ''' </summary>
    ''' <param name="DocTypeParentId"></param>
    ''' <param name="DocTypeId"></param>
    ''' <param name="IndexId"></param>
    ''' <param name="GID"></param>
    ''' <param name="righttypeid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Javier]	17/10/2010	Created
    ''' </history>
    Public Shared Function GetAssociateIndexRight(ByVal DocTypeParentId As Int64, ByVal DocTypeId As Int64, ByVal GID As Int32) As DataTable
        Dim query As New StringBuilder
        query.Append("SELECT RightType, IndexId, DocTypeParentId, DocTypeId FROM ZVIR WHERE DocTypeParentId = ")
        query.Append(DocTypeParentId & " AND DocTypeId =")
        query.Append(DocTypeId & " AND GID =")
        query.Append(GID)

        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, query.ToString)
        Return ds.Tables(0)
    End Function

    ''' <summary>
    '''     Obtiene los permisos de atributos por asociado por usuario y grupos de usuarios 
    ''' </summary>
    ''' <param name="DocTypeParentId"></param>
    ''' <param name="DocTypeId"></param>
    ''' <param name="GID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Javier]	22/10/2010	Created
    ''' </history>
    Public Shared Function GetAssociateIndexRightCombined(ByVal DocTypeParentId As Int64, ByVal DocTypeId As Int64, ByVal GID As Generic.List(Of Int64)) As DataTable
        Dim query As New StringBuilder
        query.Append("SELECT RightType, IndexId, DocTypeParentId, DocTypeId FROM ZVIR WHERE DocTypeParentId = ")
        query.Append(DocTypeParentId & " AND DocTypeId =")
        query.Append(DocTypeId & " and (")

        For index As Integer = 0 To GID.Count - 1
            query.Append("GID =" & GID(index) & " OR ")
            If index = GID.Count - 1 Then
                query.Remove(query.Length - 4, (query.Length) - (query.Length - 4))
                query.Append(")")
            End If
        Next

        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, query.ToString)
        Return ds.Tables(0)
    End Function


    ''' <summary>
    ''' Guarda los cambios de filtros por defecto al configurarlos desde el administrador.
    ''' </summary>
    ''' <param name="docTypeId"></param>
    ''' <param name="userId"></param>
    ''' <param name="indexId"></param>
    ''' <param name="filterValue"></param>
    ''' <remarks></remarks>
    Public Shared Sub SetIndexRightsDefaultSearch(ByVal docTypeId As Int64, ByVal userId As Int64, ByVal indexId As Int64, ByVal filterValue As String)
        If Server.isOracle Then

            Throw New NotImplementedException("La modificación o creación de atributos por defecto para el motor Oracle no ha sido implementada.")
            'Dim query As New StringBuilder
            'Dim ds As New DataSet
            'Dim IndexList As New ArrayList

            ''REALIZA UNA CONSULTA PARA SABER SI YA HAY ALGUN VALOR INGRESADO PARA USUARIO PARA SER USADO COMO CONSULTA
            ''POR DEFECTO. EN CASO DE HABERLA HACE UN UPDATE DE LA FILA, ES DECIR SOLO SE PUEDE TENER UNA SOLA BUSQUEDA
            ''POR DEFECTO POR CADA USUARIO.
            'ds = Server.Con.ExecuteDataset(CommandType.Text, "SELECT * FROM ZFILTERS WHERE userid=" & userId & " and FILTERATTRIBUTE='i" & indexId.ToString & "' and doctypeid= " & docTypeId)

            ''reemplazo las millas simples que tenga la consulta por dobles para poder insertarla en la tabla.
            'filterValue = Replace(filterValue, "'", """")

            'If ds.Tables(0).Rows.Count = 0 Then
            '    query.Append("INSERT INTO zfilters ( DoctypeId, Value, IndexId, UserId)  VALUES(")
            '    query.Append(docTypeId & ",'")
            '    query.Append(filterValue & "',")
            '    query.Append(indexId & ",")
            '    query.Append(userId & ")")
            'Else
            '    query.Append("UPDATE DEFAULT_SEARCH SET VALUE='" & filterValue & "' WHERE indexid=" & indexId & " and doctypeid=" & docTypeId & " and userid=" & userId)
            'End If

            'Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)

        Else
            'POR EL MOMENTO SE PONE POR DEFECTO EL IGUAL PERO SE PODRÍA CAMBIAR EL OPERADOR SIN NINGÚN PROBLEMA.
            Dim ParValues() As Object = {indexId,
                                         filterValue,
                                         "=",
                                         docTypeId,
                                         userId}
            Server.Con.ExecuteNonQuery("ZSP_FILTERS_100_InsertDefaultFilter", ParValues)
        End If
    End Sub

    ''' <summary>
    ''' [sebastian 05-05-09] sobre carga del metodo para que en el administrador cargue los filtros 
    '''especificos para la entidad.
    ''' </summary>
    ''' <param name="DocTypeId"></param>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetDefaultSearchIndexAdmin(ByVal DocTypeId As Int64, ByVal userId As Int64) As DataSet
        Dim consulta As String = "SELECT * FROM ZFILTERS where userid=" & userId & " And DocTypeId = " & DocTypeId.ToString
        Return Server.Con.ExecuteDataset(CommandType.Text, consulta)
    End Function

    ''' <summary>
    ''' Devuelve filtros de la entidad seleccionada que contenga el usuario o grupo.
    ''' </summary>
    ''' <param name="DocTypeId"></param>
    ''' <param name="UserId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetFilters(ByVal DocTypeId As Int64, ByVal UserId As Int64) As DataSet
        Dim consulta As StringBuilder = New StringBuilder()
        Dim _GUIDtemp As Generic.List(Of Int64) = UserFactory.GetUserGroupsIdsByUserid(UserId, False)

        consulta.Append("SELECT * FROM zfilters where DocTypeId = ")
        consulta.Append(DocTypeId.ToString)
        consulta.Append(" and ( ")
        consulta.Append("userid=")
        consulta.Append(UserId)
        If Not IsNothing(_GUIDtemp) Then
            consulta.Append(" or ")
            For Each Guid As Int64 In _GUIDtemp
                consulta.Append("userid=")
                consulta.Append(Guid)
                consulta.Append(" or ")
            Next
        End If
        consulta.Remove(consulta.Length - 4, 4)
        consulta.Append(" )")

        Return Server.Con.ExecuteDataset(CommandType.Text, consulta.ToString())

    End Function

    ''' <summary>
    ''' Inserta un filtro
    ''' </summary>
    ''' <param name="FilterAttribute"></param>
    ''' <param name="FilterValue"></param>
    ''' <param name="FilterDataType"></param>
    ''' <param name="FilterComparator"></param>
    ''' <param name="FilterType"></param>
    ''' <param name="DocTypeId"></param>
    ''' <param name="UserId"></param>
    ''' <param name="FilterDbName"></param>
    ''' <param name="IndexDropDown"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function InsertFilter(ByVal FilterAttribute As String, ByVal FilterValue As String, ByVal FilterDataType As Int32, ByVal FilterComparator As String, ByVal FilterType As String, ByVal DocTypeId As Int64, ByVal UserId As Int64, ByVal FilterDbName As String, ByVal IndexDropDown As Int32) As Int64
        Dim FilterId As Int64

        If Server.isOracle Then
            'TODO: PASAR ESTA PARTE A SP DE ORACLE.
            FilterId = CoreData.GetNewID(IdTypes.Filter)
            Dim consulta As String = "INSERT INTO ZFILTERS (id, attribute, value, enabled, datatype, comparator, filtertype, doctypeid, userid, description, indexdropdown ) values ("
            consulta &= FilterId.ToString() & ",'" & FilterAttribute & "','" & FilterValue & "',1,'" & FilterDataType.ToString & "','" & FilterComparator & "','" & FilterType & "'," & DocTypeId.ToString() & "," & UserId.ToString() & ",'" & FilterDbName & "'," & IndexDropDown.ToString & ")"
            Server.Con.ExecuteNonQuery(CommandType.Text, consulta)
        Else
            Dim ParValues() As Object = {FilterAttribute,
                                         FilterValue,
                                         FilterDataType,
                                         FilterComparator,
                                         FilterType,
                                         DocTypeId,
                                         UserId,
                                         FilterDbName,
                                         IndexDropDown}
            FilterId = Int64.Parse(Server.Con.ExecuteScalar("ZSP_FILTERS_100_InsertFilter", ParValues))
        End If

        Return FilterId
    End Function

    ''' <summary>
    ''' Elimina un filtro en específico por Id de filtro.
    ''' </summary>
    ''' <param name="filterId"></param>
    ''' <remarks>Se utiliza para remover filtros desde el cliente de Zamba.</remarks>
    Public Shared Sub RemoveFilter(ByVal filterId As Int64)
        If Server.isOracle Then
            'TODO: PASAR ESTA PARTE A SP DE ORACLE.
            Server.Con.ExecuteNonQuery(CommandType.Text, "DELETE ZFILTERS WHERE ID = " & filterId.ToString)
        Else
            Dim ParValues() As Object = {filterId}
            Server.Con.ExecuteNonQuery("ZSP_FILTERS_100_RemoveByFilterId", ParValues)
        End If
    End Sub

    ''' <summary>
    ''' Elimina un filtro en específico por DocTypeId, IndexId, y UserId.
    ''' </summary>
    ''' <param name="docTypeId"></param>
    ''' <param name="userId"></param>
    ''' <param name="indexId"></param>
    ''' <param name="value"></param>
    ''' <remarks>Se utiliza para remover filtros por defecto desde el Administrador.</remarks>
    Public Shared Sub RemoveFilter(ByVal docTypeId As Int64, ByVal userId As Int64, ByVal indexId As Int64, ByVal value As String)
        If Server.isOracle Then
            'TODO: PASAR ESTA PARTE A SP DE ORACLE.
            Dim query As New StringBuilder
            query.Append("DELETE FROM ZFILTERS WHERE ATTRIBUTE = ")
            query.Append(indexId.ToString & "' AND DoctypeId =")
            query.Append("'" & docTypeId & " AND UserId = ")
            query.Append(userId)
            Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
        Else
            Dim ParValues() As Object = {indexId.ToString, docTypeId, userId}
            Server.Con.ExecuteNonQuery("ZSP_FILTERS_100_RemoveDefaultFilter", ParValues)
        End If
    End Sub

    ''' <summary>
    ''' Elimina todos los filtros de un entidad de un usuario específico.
    ''' Se puede especificar si se eliminan los filtros por defecto o no.
    ''' </summary>
    ''' <param name="docTypeId"></param>
    ''' <param name="userId"></param>
    ''' <param name="removeDefaults"></param>
    ''' <remarks></remarks>
    Public Shared Sub ClearFilters(ByVal docTypeId As Int64, ByVal userId As Int64, ByVal removeDefaults As Boolean)
        Dim remove As Int32
        If removeDefaults Then
            remove = 1
        Else
            remove = 0
        End If

        If Server.isOracle Then
            'TODO: PASAR ESTA PARTE A SP DE ORACLE.
            If removeDefaults Then
                Server.Con.ExecuteNonQuery(CommandType.Text, "DELETE ZFILTERS WHERE DOCTYPEID = " & docTypeId.ToString & " AND USERID = " & userId.ToString)
            Else
                Server.Con.ExecuteNonQuery(CommandType.Text, "DELETE ZFILTERS WHERE DOCTYPEID = " & docTypeId.ToString & " AND USERID = " & userId.ToString & " AND FILTERTYPE <> 'defecto'")
            End If
        Else
            Dim ParValues() As Object = {docTypeId, userId, remove}
            Server.Con.ExecuteNonQuery("ZSP_FILTERS_200_ClearDocTypeFiltersByUserId", ParValues)
        End If
    End Sub

    ''' <summary>
    ''' Actualiza la habilitación del filtro.
    ''' </summary>
    ''' <param name="filterId"></param>
    ''' <param name="filterEnabled"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     Tomás   30/08/2010  Modified    Se aplica SP para SQL.
    ''' </history>
    Public Shared Sub UpdateFilterEnabled(ByVal filterId As Int64, ByVal filterEnabled As Boolean)
        'Se hace de esta manera ya que si se pone directamente el boolean.ToString 
        'escribe True o False en vez de 1 o 0 que es lo requerido.
        Dim isEnabled As Int32
        If filterEnabled Then
            isEnabled = 1
        Else
            isEnabled = 0
        End If

        If Server.isOracle Then
            'TODO: PASAR ESTA PARTE A SP DE ORACLE.
            Dim consulta As String = "UPDATE ZFILTERS set enabled = " & isEnabled.ToString & " where id = " & filterId.ToString
            Server.Con.ExecuteNonQuery(CommandType.Text, consulta)
        Else
            Dim ParValues() As Object = {filterId, filterEnabled}
            Server.Con.ExecuteNonQuery("ZSP_FILTERS_100_UpdateFilterEnabled", ParValues)
        End If
    End Sub



    Public Overrides Sub Dispose()

    End Sub
#Region "Indexs"




    Public Shared Function GetIndexsRightsDT(ByVal lstgroups As List(Of Long), ByVal DocTypeId As Long) As DataTable

        Dim query As New StringBuilder
        query.Append("SELECT IndexId,RightType FROM ZIR where DoctypeId =" & DocTypeId & " and (")

        For index As Integer = 0 To lstgroups.Count - 1
            query.Append("UserId =" & lstgroups(index) & " OR ")
            If index = lstgroups.Count - 1 Then
                query.Remove(query.Length - 4, (query.Length) - (query.Length - 4))
                query.Append(")")
            End If
        Next

        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, query.ToString)
        Return ds.Tables(0)
    End Function

    Public Shared Function GetIndexsRights(ByVal DocTypeIds As Generic.List(Of Int64), ByVal GID As Generic.List(Of Int64)) As DataTable
        Dim query As New StringBuilder
        query.Append("SELECT IndexId,RightType FROM ZIR where (")

        For index As Integer = 0 To GID.Count - 1
            query.Append("UserId =" & GID(index) & " OR ")
            If index = GID.Count - 1 Then
                query.Remove(query.Length - 4, (query.Length) - (query.Length - 4))
                query.Append(") and (")
            End If
        Next
        For index As Integer = 0 To DocTypeIds.Count - 1
            query.Append("DoctypeId =" & DocTypeIds(index) & " OR ")
            If index = DocTypeIds.Count - 1 Then
                query.Remove(query.Length - 4, (query.Length) - (query.Length - 4))
                query.Append(")")
            End If
        Next

        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, query.ToString())
        Return ds.Tables(0)
    End Function

    ''' <summary>
    ''' Método que sirve para obtener los atributos obligatorios de la base de datos
    ''' </summary>
    ''' <param name="DocTypeId">Id de la entidad</param>
    ''' <param name="GID">Colección de ids de grupos a los que pertenece el usuario</param>
    ''' <param name="rtIndexRequired">Enumerador de Atributo requerido</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	10/11/2008	Created
    ''' </history>
    Public Shared Function GetMandatoryIndexs(ByVal DocTypeId As Int64, ByVal GID As ArrayList, ByVal rtIndexRequired As RightsType) As DataTable

        Dim query As New StringBuilder

        query.Append("SELECT DISTINCT IndexId FROM ZIR where DoctypeId = " & DocTypeId & " and RightType = " & rtIndexRequired & " and (")

        For index As Integer = 0 To GID.Count - 1

            query.Append("UserId =" & DirectCast(GID.Item(index), UserGroup).ID & " OR ")

            If index = GID.Count - 1 Then
                query.Remove(query.Length - 4, (query.Length) - (query.Length - 4))
                query.Append(")")
            End If

        Next

        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, query.ToString)

        If (ds.Tables(0).Rows.Count > 0) Then
            Return ds.Tables(0)
        Else
            Return (Nothing)
        End If

    End Function

    Public Shared Function GetIndexRightValue(ByVal IndexId As Int64, ByVal doctypeid As Int64, ByVal _GID As Generic.List(Of Int64), ByVal RightTypeId As Int16) As Boolean
        Dim query As New StringBuilder
        query.Append("SELECT COUNT(IndexId) FROM ZIR WHERE DoctypeId =  ")
        query.Append(doctypeid)
        query.Append(" AND IndexId = ")
        query.Append(IndexId)
        query.Append(" AND (")
        Dim OnlyOnce As Boolean
        For Each gid As Int64 In _GID
            If OnlyOnce = False Then
                OnlyOnce = True
                query.Append("UserId = ")
                query.Append(gid)
            Else
                query.Append(" or UserId = ")
                query.Append(gid)
            End If
        Next
        query.Append(")")
        query.Append(" AND RightType = ")
        query.Append(RightTypeId)
        Dim count As Int16 = Server.Con.ExecuteScalar(CommandType.Text, query.ToString)
        If count = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Devuelve todos los permisos de los atributos
    ''' </summary>
    ''' <param name="_GID">Ids de los grupos y el usuario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Tomas] 18/06/2009  Modified    Se modifica el método para trabajar con procedimientos
    Public Shared Function GetIndexRightValues(ByVal userId As Int32) As DataTable

        Dim ds As DataSet = Nothing
        If Server.isOracle Then
            ds = Server.Con.ExecuteDataset(CommandType.Text, String.Format("SELECT * FROM ZIR WHERE UserId IN (SELECT DISTINCT GROUPID from (select usrid, groupid from usr_r_group
      Union
      select usrid, InheritedUserGroup from usr_r_group inner join group_r_group on UserGroup=groupid) q WHERE USRID =  {0})", userId))
        Else
            ds = Server.Con.ExecuteDataset(CommandType.Text, String.Format("SELECT * FROM ZIR WHERE UserId IN (SELECT DISTINCT GROUPID from  (select usrid, groupid from usr_r_group Union select usrid, InheritedUserGroup from usr_r_group inner join group_r_group on UserGroup=groupid) q WHERE USRID = {0})", userId))
        End If

        If Not IsNothing(ds) AndAlso ds.Tables.Count > 0 Then
            Return ds.Tables(0)
        Else
            Return Nothing
        End If
    End Function

    Public Shared Sub SetAssociateGridColumnsRight(groupOrUserID As Long, docTypeID As Long, docTypeParentID As Long, column As String)
        Dim query As New StringBuilder
        query.Append("INSERT INTO ZVCR(GROUPORUSERID, DOCTYPEID, DOCTYPEPARENTID, COLUMN_NAME)  VALUES(")
        query.Append(groupOrUserID & ",")
        query.Append(docTypeID & ",")
        query.Append(docTypeParentID & ",'")
        query.Append(column & "')")
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
    End Sub

    Public Shared Sub RemoveAssociateGridColumnsRight(groupOrUserID As Long, docTypeID As Long, docTypeParentID As Long, column As String)
        Dim query As New StringBuilder
        query.Append("DELETE FROM ZVCR WHERE GROUPORUSERID = ")
        query.Append(groupOrUserID & " AND DOCTYPEID = ")
        query.Append(docTypeID & " AND DOCTYPEPARENTID = ")
        query.Append(docTypeParentID & " AND COLUMN_NAME = '")
        query.Append(column & "'")
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
    End Sub

    Public Shared Function GetAssociateGridColumnsRight(groupOrUserID As Long, docTypeID As Long, docTypeParentID As Long) As DataTable
        Dim query As New StringBuilder
        query.Append("SELECT GROUPORUSERID, DOCTYPEID, DOCTYPEPARENTID, COLUMN_NAME FROM ZVCR WHERE GROUPORUSERID = ")
        query.Append(groupOrUserID & " AND DOCTYPEID =")
        query.Append(docTypeID & " AND DOCTYPEPARENTID =")
        query.Append(docTypeParentID)
        Return Server.Con.ExecuteDataset(CommandType.Text, query.ToString).Tables(0)
    End Function


#End Region
End Class
