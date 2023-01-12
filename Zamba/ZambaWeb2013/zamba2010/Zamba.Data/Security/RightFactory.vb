Imports System.Collections.Generic
Imports System.Text
Imports Zamba.Membership
Imports Zamba.Core
Imports Zamba.Servers
Imports Zamba.Tools


Public Class RightFactory
    Inherits ZClass

    Public Shared Permisos As New SynchronizedHashtable

    Public Shared ReadOnly Property CurrentUser() As IUser
        Get
            Return Zamba.Membership.MembershipHelper.CurrentUser
        End Get
    End Property

    Public Shared ReadOnly Property GetAllUserOrGroupsRights(ByVal id As Int64) As DataSet
        Get
            If Permisos.ContainsKey(id) = False Then

                Dim ds As DataSet
                ds = Server.Con.ExecuteDataset(CommandType.Text, "select * from usr_Rights where groupid = " & id & " or groupid in (select inheritedusergroup from group_r_group where usergroup = " & id & ") or groupid in ( Select groupid from usr_r_group where usrid=" & id & ") or groupid in (select inheritedusergroup from group_r_group where usergroup in ( Select groupid from usr_r_group where usrid=" & id & "))")

                ds.Tables(0).TableName = "usr_rights"
                SyncLock Permisos.SyncRoot
                    If Not Permisos.Contains(id) Then
                        Permisos.Add(id, ds)
                    End If
                End SyncLock
                Return ds

            Else
                Dim ds As DataSet = DirectCast(Permisos.Item(id), DataSet)
                Return ds
            End If
        End Get
    End Property

    'Public Shared Function GetUserRightsGroup(ByVal id As Integer, ByVal ObjectId As ObjectTypes, ByVal RType As RightsType, Optional ByVal AditionalParam As Integer = -1) As Boolean

    '    Dim Value As Boolean = False
    '    If id = 0 Then
    '        Value = False
    '    Else
    '        Dim query As New StringBuilder
    '        Dim Querybuilder As New StringBuilder()
    '        Querybuilder.Append("  Select * from usr_rights where")
    '        Querybuilder.Append(" OBJID = ")
    '        Querybuilder.Append(DirectCast(ObjectId, Int32).ToString())
    '        Querybuilder.Append(" and RTYPE= ")
    '        Querybuilder.Append(DirectCast(RType, Int32).ToString())
    '        Querybuilder.Append(" and ADITIONAL = ")
    '        Querybuilder.Append(AditionalParam.ToString())

    '        Dim strselect As String = "Select * from usr_r_group where usrid=" & id
    '        Dim ds As DataSet = Servers.Server.Con.ExecuteDataset(CommandType.Text, strselect)

    '        Dim count As Int32 = 1
    '        Dim VarRow As Integer = ds.Tables(0).Rows.Count
    '        For Each r As DataRow In ds.Tables(0).Rows
    '            'TODO: Ver si no existe el grupo  If GroupTable.ContainsKey(CInt(r("GROUPID"))) Then
    '            If VarRow = 1 Then
    '                Querybuilder.Append(" and GROUPID = " & r("GROUPID"))
    '            End If
    '            If VarRow > 1 Then

    '                Dim varQuery As String = Querybuilder.ToString()

    '                If varQuery.Contains("and GROUPID ") Then
    '                    Querybuilder.Append("," & r("GROUPID"))
    '                    If VarRow = count Then
    '                        Querybuilder.Append(")")
    '                    End If
    '                Else
    '                    Querybuilder.Append(" and GROUPID in (" & r("GROUPID"))
    '                End If
    '                count = count + 1
    '            End If
    '        Next

    '        Dim exist As Boolean = Server.Con.ExecuteScalar(CommandType.Text, Querybuilder.ToString())

    '        If exist = True Then
    '            Value = True
    '        Else
    '            Value = False
    '        End If
    '    End If

    '    Return Value
    'End Function

    Public Shared Function GetUserRights(ByVal id As Integer, ByVal ObjectId As ObjectTypes, ByVal RType As RightsType, Optional ByVal AditionalParam As Integer = -1) As Boolean

        Dim Value As Boolean = False
        If id = 0 Then
            Value = False
        Else
            Dim DtGroupRights As DataTable = GetAllUserOrGroupsRights(id).Tables(0)

            Dim Querybuilder As New StringBuilder()
            Querybuilder.Append("  OBJID = ")
            Querybuilder.Append(DirectCast(ObjectId, Int32).ToString())
            Querybuilder.Append(" and RTYPE= ")
            Querybuilder.Append(DirectCast(RType, Int32).ToString())
            Querybuilder.Append(" and ADITIONAL = ")
            Querybuilder.Append(AditionalParam.ToString())
            Dim r() As DataRow = DtGroupRights.Select(Querybuilder.ToString())

            If r.Length = 0 Then
                Value = False
            Else
                Value = True
            End If
        End If

        Return Value
    End Function

    Public Function GetUserRights(ByVal id As Integer, ByVal objectId As ObjectTypes, ByVal rType As RightsType, ByVal aditionalParam As List(Of Int64)) As Boolean
        Dim Value As Boolean = False
        If id = 0 Then
            Value = False
        Else

            Dim DtGroupRights As DataTable = GetAllUserOrGroupsRights(id).Tables(0)

            Dim Querybuilder As New StringBuilder()
            Querybuilder.Append("  OBJID = ")
            Querybuilder.Append(DirectCast(objectId, Int32).ToString())
            Querybuilder.Append(" and RTYPE= ")
            Querybuilder.Append(DirectCast(rType, Int32).ToString())
            Querybuilder.Append(" and ADITIONAL in (")
            For Each aid As Int64 In aditionalParam
                Querybuilder.Append(aid.ToString())
                Querybuilder.Append(",")
            Next
            Querybuilder.Remove(Querybuilder.Length - 1, 1)
            Querybuilder.Append(" ) ")
            Dim r() As DataRow = DtGroupRights.Select(Querybuilder.ToString())

            If r.Length = 0 Then
                Value = False
            Else
                Value = True
            End If
        End If

        Return Value
    End Function




    Public Shared Function IsUserPasswordNull(ByVal usrID As Int64) As Boolean
        If IsDBNull(Server.Con.ExecuteScalar(CommandType.Text, "select PASSWORD from USRTABLE where id = " & usrID)) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetUserPassword(ByVal usrID As Int64) As String
        Return Server.Con.ExecuteScalar(CommandType.Text, "select PASSWORD from USRTABLE where id = " & usrID)
    End Function





    Public Shared Function ValidateModuleLicense(ByVal Modulo As ObjectTypes) As Boolean
        Dim key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Dim iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Dim valor As String
        Dim value As String = Zamba.Servers.Server.Con.ExecuteScalar(CommandType.Text, "Select Valor from ModulesRights where Modulo='" & Modulo.ToString & "'")
        If Not IsNothing(value) Then
            valor = Zamba.Tools.Encryption.DecryptString(value, key, iv)
            If valor.ToUpper = "OK" & Modulo.ToString.ToUpper Then
                Return True
            Else
                Return False
            End If
        End If
        Return False
    End Function

    Public Shared Function ValidateModuleLicense(ByVal Modulo As ObjectTypes, ByVal t As Transaction) As Boolean
        Dim key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Dim iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Dim valor As String
        Dim value As String = t.Con.ExecuteScalar(t.Transaction, CommandType.Text, "Select Valor from ModulesRights where Modulo='" & Modulo.ToString & "'")
        If Not IsNothing(value) Then
            valor = Zamba.Tools.Encryption.DecryptString(value, key, iv)
            If valor.ToUpper = "OK" & Modulo.ToString.ToUpper Then
                Return True
            Else
                Return False
            End If
        End If
        Return False
    End Function

    <Obsolete("Las entidades no corresponden a la capa de datos")>
    Public Shared Function GetArchivosUserRight(ByVal ds As Data_Group_Doc) As DataSet
        '1/1/2006
        Dim dstemp As DataSet

        If Server.isOracle Then
            Dim ParValues() As Object = {CurrentUser.ID, 2}
            'Dim ParNames() As Object = {"UserId", "io_cursor"}
            ' Dim parTypes() As Object = {OracleType.Number, OracleType.Cursor}
            dstemp = Server.Con.ExecuteDataset("zsp_security_100.GetArchivosUserRight", ParValues)
        Else
            Dim ParValues() As Object = {CurrentUser.ID}
            'dstemp = Server.Con.ExecuteDataset("GetArchivosUserRight", ParValues)
            dstemp = Server.Con.ExecuteDataset("zsp_security_100_GetArchivosUserRight", ParValues)
        End If

        dstemp.Tables(0).TableName = ds.Doc_Type_Group.TableName
        ds.Merge(dstemp)
        Return ds
    End Function


    Public Shared Function GetCategoriesByUserRight(ByVal UserId As Int64) As DataTable
        '1/1/2006
        Dim dstemp As DataSet

        If Server.isOracle Then
            Dim ParValues() As Object = {UserId, 2}
            'Dim ParNames() As Object = {"UserId", "io_cursor"}
            ' Dim parTypes() As Object = {13, 5}
            'dstemp = Server.Con.ExecuteDataset("ZGetUserRigth_pkg.GetArchivosUserRight", parValues)
            dstemp = Server.Con.ExecuteDataset("zsp_security_100.GetArchivosUserRight", ParValues)
        Else
            Dim ParValues() As Object = {UserId}
            'dstemp = Server.Con.ExecuteDataset("GetArchivosUserRight", ParValues)
            dstemp = Server.Con.ExecuteDataset("zsp_security_100_GetArchivosUserRight", ParValues)
        End If

        If dstemp.Tables.Count > 0 Then Return dstemp.Tables(0)
        Return Nothing
    End Function

    <Obsolete("Las entidades no corresponden a la capa de datos")>
    Public Shared Function GetDocTypeUserRightFromArchive(ByVal ds As DsDoctypeRight, ByVal Doc_GroupID As Integer) As DataSet
        'TODO Martin: Esta yendo a la base para traer los doctypes que el usuario tiene permisos, aprobechar zcore
        Dim usrs As New System.Text.StringBuilder
        usrs.Append(CurrentUser.ID)
        For Each u As IUserGroup In CurrentUser.Groups
            usrs.Append("," + u.ID)
        Next
        Dim rights As String = "1"
        Dim objectid As String = "2"

        Dim d As DataSet
        If Server.isOracle Then
            Dim strBuilder As New StringBuilder()
            strBuilder.Append("select t.doc_type_name Doc_Type_Name,r.doc_type_Group Doc_Type_Group, t.doc_type_id Doc_Type_Id,icon_id Icon_Id,documentalId DocumentalID from doc_type t,doc_type_r_doc_type_group r ")
            strBuilder.Append("where r.doc_type_id=t.doc_type_id and r.doc_type_id in ")
            strBuilder.Append("(select DISTINCT aditional from Zvw_USR_Rights_200 where Right_type  in (")
            strBuilder.Append(rights)
            strBuilder.Append(") and objectid in (")
            strBuilder.Append(objectid)
            strBuilder.Append(") and user_id in (")
            strBuilder.Append(usrs)
            strBuilder.Append("))")
            d = Servers.Server.Con.ExecuteDataset(CommandType.Text, strBuilder.ToString())
        Else
            d = Servers.Server.Con.ExecuteDataset("zsp_300_GetAllDocTypesByUserRight", New Object() {usrs.ToString(), rights, objectid})
        End If
        d.Tables(0).TableName = ds.DocTypes.TableName
        ds.Merge(d.Tables(0))
        Return ds
    End Function

    Public Shared Function GetAllDocTypesByUserRight(ByVal userid As Int64) As DataSet
        ' Obtiene todos los doctypes Segun los permisos que se asigno para el usuario
        ' Los permisos por los cual filtra son el de "VER" - "Crear" y "Re Indexar"
        Dim rights As String = "1,3,12"
        Dim objectid As String = "2"

        If Server.isOracle Then
            Dim strBuilder As New StringBuilder()
            strBuilder.Append("select t.doc_type_name Doc_Type_Name,r.doc_type_Group Doc_Type_Group, t.doc_type_id Doc_Type_Id,icon_id Icon_Id,documentalId DocumentalID from doc_type t,doc_type_r_doc_type_group r ")
            strBuilder.Append("where r.doc_type_id=t.doc_type_id and r.doc_type_id in ")
            strBuilder.Append("(select DISTINCT aditional from Zvw_USR_Rights_200 where Right_type  in (")
            strBuilder.Append(rights)
            strBuilder.Append(") and objectid in (")
            strBuilder.Append(objectid)
            strBuilder.Append(") and user_id in (")
            strBuilder.Append(userid)
            strBuilder.Append("))")

            Return Servers.Server.Con.ExecuteDataset(CommandType.Text, strBuilder.ToString())
        Else
            Return Servers.Server.Con.ExecuteDataset("zsp_300_GetAllDocTypesByUserRight", New Object() {userid.ToString(), rights, objectid})
        End If
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
        ' Los permisos por los cual filtra son el de "Crear" 
        Dim rights As String = "3"
        Dim objectid As String = "2"

        If Server.isOracle Then
            Dim strBuilder As New StringBuilder()
            strBuilder.Append("select t.doc_type_name Doc_Type_Name,r.doc_type_Group Doc_Type_Group, t.doc_type_id Doc_Type_Id,icon_id Icon_Id,documentalId DocumentalID from doc_type t,doc_type_r_doc_type_group r ")
            strBuilder.Append("where r.doc_type_id=t.doc_type_id and r.doc_type_id in ")
            strBuilder.Append("(select DISTINCT aditional from Zvw_USR_Rights_200 where Right_type  in (")
            strBuilder.Append(rights)
            strBuilder.Append(") and objectid in (")
            strBuilder.Append(objectid)
            strBuilder.Append(") and user_id in (")
            strBuilder.Append(userid)
            strBuilder.Append("))")

            Return Servers.Server.Con.ExecuteDataset(CommandType.Text, strBuilder.ToString())
        Else
            Return Servers.Server.Con.ExecuteDataset("zsp_300_GetAllDocTypesByUserRight", New Object() {userid.ToString, rights, objectid})
        End If
    End Function




    <Obsolete("Las entidades no corresponden a la capa de datos")>
    Public Shared Sub SaveAction(ByVal ObjectId As Int64, ByVal ObjectType As ObjectTypes, ByVal ActionType As RightsType, Optional ByVal S_Object_ID As String = "", Optional ByVal _userid As Int64 = 0)
        Try
            Dim ConnectionId As Integer
            Try
                If _userid = 0 Then
                    If IsNothing(RightFactory.CurrentUser) Then
                        _userid = 0
                    Else
                        _userid = RightFactory.CurrentUser.ID
                    End If
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Try
                If IsNothing(RightFactory.CurrentUser) Then
                    ConnectionId = 0
                Else
                    ConnectionId = RightFactory.CurrentUser.ConnectionId
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            ActionsFactory.SaveActioninDB(ObjectId, ObjectType, ActionType, S_Object_ID, _userid, ConnectionId)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Trae los tipod de documento en los cuales el usuario tiene permiso de crear.
    ''' </summary>
    ''' <returns>
    '''     Devuelve 0 en caso de no existir licencias disponibles
    '''     Devuelve 1 en caso de que las consultas se hayan realizado con éxito
    '''     Devuelve 2 en caso de tener máximo de licencias conectadas
    ''' </returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Tomas] 29/06/2009  Created    Se adapta el método para un nuevo manejo de logueo de acciones del usuario  
    '''</history>
    <Obsolete("Las entidades no corresponden a la capa de datos")>
    Public Shared Function SaveAction(ByVal ObjectId As Int64, ByVal ObjectType As ObjectTypes, ByVal machineName As String, ByVal ActionType As RightsType, Optional ByVal S_Object_ID As String = "", Optional ByVal _userid As Int32 = 0) As Integer
        Try
            Dim ConnectionId As Integer
            Try
                If _userid = 0 Then
                    If IsNothing(RightFactory.CurrentUser) Then
                        _userid = 0
                    Else
                        _userid = RightFactory.CurrentUser.ID
                    End If
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Try
                If IsNothing(RightFactory.CurrentUser) Then
                    ConnectionId = 0
                Else
                    ConnectionId = RightFactory.CurrentUser.ConnectionId
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            'Devuelve 0 en caso de no existir licencias disponibles
            'Devuelve 1 en caso de que las consultas se hayan realizado con éxito
            Return ActionsFactory.SaveActioninDB(ObjectId, ObjectType, ActionType, S_Object_ID, _userid, ConnectionId, machineName)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return -1
        End Try
    End Function

    ''' <summary>
    ''' Trae los tipod de documento en los cuales el usuario tiene permiso de crear.
    ''' </summary>
    ''' <returns>
    '''     Devuelve 0 en caso de no existir licencias disponibles
    '''     Devuelve 1 en caso de que las consultas se hayan realizado con éxito
    '''     Devuelve 2 en caso de tener máximo de licencias conectadas
    ''' </returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Tomas] 29/06/2009  Created    Se adapta el método para un nuevo manejo de logueo de acciones del usuario  
    '''</history>
    <Obsolete("Las entidades no corresponden a la capa de datos")>
    Public Shared Function SaveAction(ByVal ObjectId As Int64, ByVal ObjectType As ObjectTypes, ByVal machineName As String, ByVal ActionType As RightsType, ByVal S_Object_ID As String, ByVal _userid As Int32, ByRef t As Transaction) As Integer
        Dim ConnectionId As Integer
        If _userid = 0 Then
            If IsNothing(RightFactory.CurrentUser) Then
                _userid = 0
            Else
                _userid = RightFactory.CurrentUser.ID
            End If
        End If
        If IsNothing(RightFactory.CurrentUser) Then
            ConnectionId = 0
        Else
            ConnectionId = RightFactory.CurrentUser.ConnectionId
        End If
        'Devuelve 0 en caso de no existir licencias disponibles
        'Devuelve 1 en caso de que las consultas se hayan realizado con éxito
        Return ActionsFactory.SaveActioninDB(ObjectId, ObjectType, ActionType, S_Object_ID, _userid, ConnectionId, machineName, t)
    End Function


    <Obsolete("Las entidades no corresponden a la capa de datos")>
    Public Shared Sub LoadRights(ByVal user As IUser, ByRef dsgeneral As DataSet, ByRef dsarchivos As DataSet, ByRef dsdoctype As DataSet, ByRef dsrestriction As DataSet, ByVal ids As ArrayList)

        Dim strIds As New System.Text.StringBuilder
        Dim keys As String = String.Empty

        'armo los ids de usuario y grupo
        Dim i As Integer
        Try
            For i = 0 To ids.Count - 1
                If i = 0 Then
                    strIds.Append("ID=" & ids(i))
                Else
                    strIds.Append(" OR ID=" & ids(i))
                End If
            Next
            keys = "(" & strIds.ToString & ")"
        Catch ex As Exception
        End Try
        Dim strSelect As String = String.Empty
        'cargo permisos generales
        Try
            'strSelect = "SELECT distinct objecttypes as Herramienta,rightstype as Modo FROM VIEW_OBJECTS_RIGHTS WHERE ADITIONAL=-1 and " & keys & " ORDER BY 1,2"
            strSelect = "SELECT distinct objecttypes as Herramienta,rightstype as Modo FROM Zvw_objects_rights_100 WHERE ADITIONAL=-1 and " & keys & " ORDER BY 1,2"
            dsgeneral = Zamba.Servers.Server.Con.ExecuteDataset(CommandType.Text, strSelect)
        Catch ex As Exception
            Dim exn As New Exception("Error (Public Sub LoadRights(ByVal user as iuser)) al ejecutar SQL: SELECT distinct objecttypes as Herramienta,rightstype as Modo FROM Zvw_objects_rights_100 WHERE ADITIONAL=-1 and " & keys & " ORDER BY 1,2. Excepción: " & ex.ToString)
            ZClass.raiseerror(exn)
        End Try

        'cargo permsos de archivos
        Try
            'strSelect = "SELECT distinct DOC_TYPE_GROUP_NAME as Archivo,RIGHTSTYPE as Modo FROM view_docgroups_rights WHERE " & keys & " ORDER BY 1,2"
            strSelect = "SELECT distinct DOC_TYPE_GROUP_NAME as Archivo,RIGHTSTYPE as Modo FROM Zvw_docgroups_rights_100 WHERE " & keys & " ORDER BY 1,2"
            dsarchivos = Zamba.Servers.Server.Con.ExecuteDataset(CommandType.Text, strSelect)
        Catch ex As Exception
            Dim exn As New Exception("Error (Public Sub LoadRights(ByVal user as iuser)) al ejecutar SQL:" & strSelect & ".Excepción" & ex.ToString)
            ZClass.raiseerror(exn)
        End Try

        'cargo permisos de tipos de documentos
        Try
            If Server.isOracle Then
                'strSelect = "SELECT distinct doc_type_name as " & Chr(34) & "Entidad" & Chr(34) & " ,rightstype as Modo  FROM view_doctypes_rights WHERE ADITIONAL<>-1 and " & keys & " ORDER BY 1,2"
                strSelect = "SELECT distinct doc_type_name as " & Chr(34) & "Entidad" & Chr(34) & " ,rightstype as Modo  FROM Zvw_doctypes_rights_100 WHERE ADITIONAL<>-1 and " & keys & " ORDER BY 1,2"
            Else
                'strSelect = "SELECT distinct doc_type_name as [Entidad],rightstype as Modo  FROM view_doctypes_rights WHERE ADITIONAL<>-1 and " & keys & " ORDER BY 1,2"
                strSelect = "SELECT distinct doc_type_name as [Entidad],rightstype as Modo  FROM Zvw_doctypes_rights_100 WHERE ADITIONAL<>-1 and " & keys & " ORDER BY 1,2"
            End If
            dsdoctype = Zamba.Servers.Server.Con.ExecuteDataset(CommandType.Text, strSelect)
        Catch ex As Exception
            Dim exn As New Exception("Error (Public Sub LoadRights(ByVal user as iuser)) al ejecutar SQL:" & strSelect & ".Excepción" & ex.ToString)
            ZClass.raiseerror(exn)
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
            dsrestriction = Zamba.Servers.Server.Con.ExecuteDataset(CommandType.Text, strSelect)
        Catch ex As Exception
            Dim exn As New Exception("Error (Public Sub LoadRights(ByVal user as iuser)) al ejecutar SQL:" & strSelect & ".Excepción" & ex.ToString)
            ZClass.raiseerror(exn)
        End Try

        strIds = Nothing
    End Sub

    Public Shared Function VerLicenciasDocumentales() As String
        Dim LicenciasDocumentales As String = String.Empty
        Dim key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Dim iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        '' Ver Documentales
        Try
            If Server.isOracle Then
                LicenciasDocumentales = Server.Con.ExecuteScalar(CommandType.Text, "Select Numero_Licencias from lic Where Type=0")
            Else
                'LicenciasDocumentales = Server.Con.ExecuteScalar("SelectLic")
                LicenciasDocumentales = Server.Con.ExecuteScalar("zsp_license_100_GetDocumentalLicenses")
            End If
            If IsNothing(LicenciasDocumentales) OrElse LicenciasDocumentales.Trim = "" Then
                LicenciasDocumentales = "0"
            Else
                LicenciasDocumentales = Zamba.Tools.Encryption.DecryptString(LicenciasDocumentales, key, iv)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Return LicenciasDocumentales
    End Function
    Public Shared Function VerLicenciasWorkFlow() As String
        Dim LicenciasWorkflow As String = String.Empty
        Dim key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Dim iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        ''  Ver Workflow
        Try
            LicenciasWorkflow = Server.Con.ExecuteScalar(CommandType.Text, "Select Numero_Licencias from lic Where Type=1")
            If IsNothing(LicenciasWorkflow) OrElse LicenciasWorkflow.Trim = String.Empty Then
                LicenciasWorkflow = 0
            Else
                LicenciasWorkflow = Encryption.DecryptString(LicenciasWorkflow, key, iv)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
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

    Public Shared Sub RegisterModule(ByVal ModuleId As Int32, ByVal ModuleName As String)
        Dim key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Dim iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Dim sql As String = "Insert into ModulesRights(id,modulo,Valor) values(" & ModuleId & ",'" & ModuleName & "','" & Zamba.Tools.Encryption.EncryptString("OK" & ModuleName.ToString.ToUpper, key, iv) & "')"
        Server.Con.ExecuteNonQuery(CommandType.Text, sql)
    End Sub

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
    ''' Obtiene los valores por defecto de los indices atachados al entidad
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
    '''especificos para el entidad.
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
    ''' Devuelve filtros del entidad seleccionado que contenga el usuario o grupo.
    ''' </summary>
    ''' <param name="DocTypeId"></param>
    ''' <param name="UserId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetFilters(ByVal DocTypeId As Int64, ByVal UserId As Int64) As DataSet
        If Server.isOracle Then
            'TODO: PASAR ESTA PARTE A SP DE ORACLE.
            Dim consulta As StringBuilder = New StringBuilder()
            Dim _GUIDtemp As Generic.List(Of Int64) = UserFactory.GetUserGroupsIdsByUserid(UserId, False)

            consulta.Append("SELECT * FROM zfilters where DocTypeId = ")
            consulta.Append(DocTypeId.ToString)
            consulta.Append(" and ( ")
            If Not IsNothing(_GUIDtemp) Then
                For Each Guid As Int64 In _GUIDtemp
                    consulta.Append("userid=")
                    consulta.Append(Guid)
                    consulta.Append(" or ")
                Next
            End If
            consulta.Remove(consulta.Length - 4, 4)
            consulta.Append(" )")

            Return Server.Con.ExecuteDataset(CommandType.Text, consulta.ToString())
            'Dim ParValues() As Object = {DocTypeId, UserId}
            'Return Server.Con(True).ExecuteDataset("ZSP_FILTERS_100_GetFilters", ParValues)
        Else
            Dim ParValues() As Object = {DocTypeId, UserId}
            Return Server.Con.ExecuteDataset("ZSP_FILTERS_100_GetFilters", ParValues)
        End If
    End Function

    Public Shared Function GetFiltersWeb(ByVal DocTypeId As Int64, ByVal UserId As Int64) As DataSet
        If Server.isOracle Then
            Dim consulta As StringBuilder = New StringBuilder()
            Dim _GUIDtemp As Generic.List(Of Int64) = UserFactory.GetUserGroupsIdsByUserid(UserId, False)

            consulta.Append("SELECT * FROM zfiltersweb where DocTypeId = ")
            consulta.Append(DocTypeId.ToString)
            consulta.Append(" and ( ")
            If Not IsNothing(_GUIDtemp) Then
                For Each Guid As Int64 In _GUIDtemp
                    consulta.Append("userid=")
                    consulta.Append(Guid)
                    consulta.Append(" or ")
                Next
            End If
            consulta.Remove(consulta.Length - 4, 4)
            consulta.Append(" )")

            Return Server.Con.ExecuteDataset(CommandType.Text, consulta.ToString())
        Else
            Dim ParValues() As Object = {DocTypeId, UserId}
            Return Server.Con.ExecuteDataset("ZSP_FILTERS_100_GetFiltersWeb", ParValues)
        End If
    End Function

    Public Shared Function GetFiltersWebByView(ByVal DocTypeId As Int64, ByVal UserId As Int64, ByVal filterType As String) As DataSet
        'para oracle agregar la parte del view en el where
        If Server.isOracle Then
            Dim consulta As StringBuilder = New StringBuilder()
            Dim _GUIDtemp As Generic.List(Of Int64) = UserFactory.GetUserGroupsIdsByUserid(UserId, False)
            consulta.AppendLine("SELECT * FROM ZFILTERS")
            consulta.AppendLine("WHERE DOCTYPEID = " & DocTypeId)
            consulta.AppendLine("And FilterType = '" & filterType & "'")
            consulta.AppendLine("And (USERID = " & UserId)
            consulta.AppendLine("Or USERID IN")
            consulta.AppendLine("(SELECT GROUPID FROM USR_R_GROUP WHERE usrid = " & UserId & "))")

            Return Server.Con.ExecuteDataset(CommandType.Text, consulta.ToString())
        Else
            Dim ParValues() As Object = {DocTypeId, UserId, filterType}
            Return Server.Con.ExecuteDataset("ZSP_FILTERS_200_GetFiltersWebByView", ParValues)
        End If
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

    Public Shared Function InsertFilterWeb(ByVal FilterAttribute As String, ByVal FilterValue As String, ByVal FilterDataType As Int32, ByVal FilterComparator As String, ByVal FilterType As String, ByVal DocTypeId As Int64, ByVal UserId As Int64, ByVal FilterDbName As String, ByVal IndexDropDown As Int32) As Int64
        Dim FilterId As Int64
        'TODO: La parte de Oracle le falta el DataDescription y las palabras reservadas no se si tiran exception, como por ej VALUE
        If Server.isOracle Then
            FilterId = CoreData.GetNewID(IdTypes.FilterWeb)
            Dim consulta As String = "INSERT INTO ZFILTERS (id, attribute, ""VALUE"", enabled, datatype, comparator, filtertype, doctypeid, userid, description, indexdropdown ) values ("
            consulta &= FilterId.ToString() & ",'" & FilterAttribute & "','" & FilterValue & "',1,'" & FilterDataType.ToString & "','" & FilterComparator & "','" & FilterType & "'," & DocTypeId.ToString() & "," & UserId.ToString() & ",'" & FilterDbName & "'," & IndexDropDown.ToString & "')"
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
            FilterId = Int64.Parse(Server.Con.ExecuteScalar("ZSP_FILTERS_200_InsertFilterWeb", ParValues))
        End If

        Return FilterId
    End Function


    Public Shared Function DeleteUserAssignedFilter(ByVal UserId As Int64, ByVal DocTypeId As Int64, ByVal FilterType As String)
        Dim query As String = "DELETE ZFILTERS WHERE USERID = " & UserId & " AND DoctypeId = " & DocTypeId & " AND FILTERTYPE = '" & FilterType & "' AND Attribute = 'uag.NAME'"
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Function

    Public Shared Function DeleteStepFilter(ByVal UserId As Int64, ByVal DocTypeId As Int64, ByVal FilterType As String)
        Dim query As String = "DELETE ZFILTERS WHERE USERID = " & UserId & " AND DoctypeId = " & DocTypeId & " AND FILTERTYPE = '" & FilterType & "' AND Attribute = 'STEPID'"
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
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
    ''' Elimina un filtro en específico por DocTypeId, IndexId, y UserId.
    ''' </summary>
    ''' <param name="docTypeId"></param>
    ''' <param name="userId"></param>
    ''' <param name="indexId"></param>
    ''' <param name="value"></param>
    ''' <remarks>Se utiliza para remover filtros por defecto desde el Administrador.</remarks>
    Public Shared Sub RemoveFilterWeb(ByVal docTypeId As Int64, ByVal userId As Int64, ByVal attribute As String, ByVal comparator As String, ByVal value As String)
        Dim query As New StringBuilder
        query.Append("DELETE FROM ZFILTERSWEB WHERE ATTRIBUTE = '")
        query.Append(attribute & "' AND DoctypeId = ")
        query.Append(docTypeId & " AND UserId = ")
        query.Append(userId & " AND [Value] =  '(" & value & ")'") 'En Oracle las palabras reservadas se escapan tambien con corchetes?
        query.Append(" AND Comparator = '" & comparator & "'")
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
    End Sub

    Public Shared Sub RemoveFilterWebById(ByVal zfilterWebId As Int64)
        Dim query As New StringBuilder
        query.Append("DELETE FROM ZFILTERS WHERE id = " & zfilterWebId)
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
    End Sub

    ''' <summary>
    ''' Quita los filtros para las columnas fijas
    ''' </summary>
    ''' <param name="docTypeId"></param>
    ''' <param name="userId"></param>
    ''' <param name="attribute"></param>
    ''' <param name="comparator"></param>
    ''' <param name="value"></param>
    Public Shared Sub RemoveZambaColumnsFilterWeb(ByVal docTypeId As Int64, ByVal userId As Int64, ByVal filterType As String)
        Dim query As New StringBuilder
        query.Append("DELETE zfw FROM ZFILTERS as zfw where zfw.Description in ('Tarea','Nombre Original','Fecha Creacion','Modificación')")
        query.Append(" AND zfw.DoctypeId = " & docTypeId)
        query.Append(" AND zfw.UserId = " & userId)
        query.Append(" AND zfw.FilterType = '" & filterType & "'")

        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
    End Sub

    Public Shared Sub UpdateFilterValue(ByVal zfilterId As Int64, ByVal filterValue As String)
        Dim query As New StringBuilder
        query.Append("UPDATE ZFILTERSWEB SET")
        query.Append(" [Value] = '(" & filterValue & ")', ")
        query.Append(" [Enabled] = 1 ")
        query.Append(" WHERE Id = " & zfilterId)
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
    End Sub

    Public Shared Sub SetDisabledAllFiltersByUser(ByVal userId As Int64, ByVal FilterType As String)
        Dim query As New StringBuilder
        query.Append("UPDATE ZFILTERS SET")

        If Server.isOracle Then
            query.Append(" Enabled = 0 ")
        Else
            query.Append(" [Enabled] = 0 ")
        End If

        query.Append(" WHERE UserId = " & userId & " And FilterType = '" & FilterType & "'")
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
    End Sub


    Public Shared Sub SetDisabledAllFiltersByUserViewDoctype(ByVal userId As Int64, ByVal FilterType As String, ByVal DocTypeId As Int64)
        Dim query As New StringBuilder
        query.Append("UPDATE ZFILTERS SET")
        query.Append(" [Enabled] = 0 ")
        query.Append(" WHERE UserId = " & userId & " And FilterType = '" & FilterType & "' AND DoctypeId = " & DocTypeId)
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
    End Sub

    Public Shared Sub SetEnabledAllFiltersByUserViewDoctype(ByVal userId As Int64, ByVal FilterType As String, ByVal DocTypeId As Int64)
        Dim query As New StringBuilder
        query.Append("UPDATE ZFILTERS SET")
        query.Append(" [Enabled] = 1 ")
        query.Append(" WHERE UserId = " & userId & " And FilterType = '" & FilterType & "' AND DoctypeId = " & DocTypeId)
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
    End Sub

    Public Shared Sub RemoveAllZambaColumnsFilter(ByVal filterIdsString As String)
        Dim query As New StringBuilder
        query.Append("DELETE FROM ZFILTERS where Id in " & filterIdsString)
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
    End Sub

    Public Shared Sub RemoveAllFilters(ByVal userId As Int64, ByVal FilterType As String, ByVal DocTypeId As Int64)
        Dim query As New StringBuilder
        query.Append("DELETE FROM ZFILTERS where userId = " & userId & " and FilterType = '" & FilterType & "' and DocTypeId = " & DocTypeId)
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
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
                Server.Con.ExecuteNonQuery(CommandType.Text, "DELETE ZFILTERS WHERE DOCTYPEID = " & docTypeId.ToString & " And USERID = " & userId.ToString)
            Else
                Server.Con.ExecuteNonQuery(CommandType.Text, "DELETE ZFILTERS WHERE DOCTYPEID = " & docTypeId.ToString & " And USERID = " & userId.ToString & " And FILTERTYPE <> 'defecto'")
        End If
        Else
        Dim ParValues() As Object = {DocTypeId, userId, remove}
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

    Public Shared Sub UpdateFilterWebEnabled(ByVal docTypeId As Int64, ByVal userId As Int64, ByVal attribute As String, ByVal comparator As String, ByVal value As String, ByVal enabled As Int64)
        Dim query As New StringBuilder
        query.Append("UPDATE ZFILTERSWEB set Enabled = " & enabled)
        query.Append(" WHERE ATTRIBUTE = '")
        query.Append(attribute & "' AND DoctypeId = ")
        query.Append(docTypeId & " AND UserId = ")
        query.Append(userId & " AND [Value] =  '(" & value & ")'") 'En Oracle las palabras reservadas se escapan tambien con corchetes?
        query.Append(" AND Comparator = '" & comparator & "'")
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
    End Sub
    Public Shared Sub UpdateFilterWebEnabledById(ByVal id As Int64, ByVal enabled As Int64)
        Dim query As New StringBuilder
        query.Append("UPDATE ZFILTERS set Enabled = " & enabled)
        query.Append(" WHERE id = " & id)
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
    End Sub



    Public Shared Function GetDocTypeUserRightFromArchive(ByVal UserId As Long, ByVal Doc_GroupID As Integer) As DsDoctypeRight
        'TODO Martin: Esta yendo a la base para traer los doctypes que el usuario tiene permisos, aprobechar zcore
        Dim ds As New DsDoctypeRight
        Dim UserWhere As New System.Text.StringBuilder
        UserWhere.Append("user_id = " & CurrentUser.ID)
        For Each u As IUserGroup In CurrentUser.Groups
            UserWhere.Append(" or user_id = " & u.ID)
        Next

        Dim sb As New System.Text.StringBuilder
        sb.Append(" select t.doc_type_name Doc_Type_Name,r.doc_type_Group Doc_Type_Group, t.doc_type_id Doc_Type_Id,icon_id Icon_Id,documentalId DocumentalID from doc_type t,doc_type_r_doc_type_group r ")
        sb.Append(" where r.doc_type_id=t.doc_type_id and r.doc_type_group=" & Doc_GroupID & " and r.doc_type_id in ")
        sb.Append(" (select DISTINCT aditional from Zvw_USR_Rights_200 where Right_type=1 and objectid=2 and (" & UserWhere.ToString & ")) order by DOC_TYPE_NAME")

        Dim d As DataSet = Servers.Server.Con.ExecuteDataset(CommandType.Text, sb.ToString)
        d.Tables(0).TableName = ds.DocTypes.TableName
        ds.Merge(d.Tables(0))
        Return ds
    End Function

    Public Shared Function GetArchivosUserRight(ByVal UserId As Long) As DataSet
        '1/1/2006
        Dim ds As New Data_Group_Doc
        Dim dstemp As DataSet

        If Server.isOracle Then
            Dim ParValues() As Object = {UserId, 2}
            'Dim ParNames() As Object = {"UserId", "io_cursor"}
            ' Dim parTypes() As Object = {OracleType.Number, OracleType.Cursor}
            dstemp = Server.Con.ExecuteDataset("zsp_security_100.GetArchivosUserRight", ParValues)
        Else
            Dim ParValues() As Object = {UserId}
            dstemp = Server.Con.ExecuteDataset("zsp_security_200_GetArchivosUserRight", ParValues)
        End If

        dstemp.Tables(0).TableName = ds.Doc_Type_Group.TableName
        ds.Merge(dstemp)
        Return ds
    End Function

    ''' <summary>
    ''' Registra una accion de usuario en su historial desde WebView, no realiza comprobacion de licencias
    ''' </summary>
    ''' <returns>
    ''' </returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [AlejandroR] 18/06/2010  Created
    '''</history>
    Public Shared Sub SaveActionWebView(ByVal ObjectId As Int64, ByVal ObjectType As ObjectTypes, ByVal machineName As String, ByVal ActionType As RightsType, Optional ByVal S_Object_ID As String = "", Optional ByVal _currUserid As Int64 = 0)
        Try
            'Devuelve 0 en caso de no existir licencias disponibles
            'Devuelve 1 en caso de que las consultas se hayan realizado con éxito
            ActionsFactory.SaveActionWebViewInDB(ObjectId, ObjectType, ActionType, S_Object_ID, _currUserid)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub


    Public Overrides Sub Dispose()

    End Sub
#Region "Indexs"


    Public Shared Function GetIndexsRights(ByVal DocTypeId As Int64, ByVal UserId As Int64) As DataTable
        Dim query As New StringBuilder
        query.Append("SELECT IndexId,RightType FROM ZIR where DoctypeId =" & DocTypeId & " and (")
        query.Append("UserId =" & UserId & " OR UserId in")
        query.Append("  (SELECT DISTINCT GROUPID from USR_R_GROUP WHERE USRID = " & UserId & "  )")
        query.Append(" Or UserId in ( select inheritedusergroup from group_r_group where usergroup in ")
        query.Append(" ( Select groupid from usr_r_group where usrid= " & UserId & " )))")
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, query.ToString)
        Return ds.Tables(0)
    End Function

    Public Shared Function GetIndexsRights(ByVal DocTypeIds As Generic.List(Of Int64), ByVal UserId As Int64) As DataTable
        Dim query As New StringBuilder
        query.Append("SELECT IndexId,RightType FROM ZIR where (")

        For index As Integer = 0 To DocTypeIds.Count - 1
            query.Append("DoctypeId =" & DocTypeIds(index) & " OR ")
            If index = DocTypeIds.Count - 1 Then
                query.Remove(query.Length - 4, (query.Length) - (query.Length - 4))
                query.Append(")")
            End If
        Next

        query.Append(" and (")
        query.Append("UserId =" & UserId & " OR UserId in")
        query.Append("  (SELECT DISTINCT GROUPID from USR_R_GROUP WHERE USRID = " & UserId & "  )")
        query.Append(" Or UserId in ( select inheritedusergroup from group_r_group where usergroup in ")
        query.Append(" ( Select groupid from usr_r_group where usrid= " & UserId & " )))")
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, query.ToString)

        Return ds.Tables(0)
    End Function



    Public Shared Function GetIndexRightValue(ByVal IndexId As Int64, ByVal doctypeid As Int64, ByVal _GID As Generic.List(Of Int64), ByVal RightTypeId As Int16) As Boolean
        Dim gids As New StringBuilder

        Dim OnlyOnce As Boolean
        For Each gid As Int64 In _GID
            If OnlyOnce = False Then
                OnlyOnce = True
                gids.Append(gid.ToString())
            Else
                gids.Append("," + gid.ToString())
            End If
        Next
        Dim count As Int16 = Server.Con.ExecuteScalar("zsp_200_GetAllDocTypesByUserRight", New Object() {doctypeid, IndexId, gids.ToString(), RightTypeId})
        If count = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Devuelve todos los permisos de los indices
    ''' </summary>
    ''' <param name="_GID">Ids de los grupos y el usuario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Tomas] 18/06/2009  Modified    Se modifica el método para trabajar con procedimientos
    Public Shared Function GetIndexRightValues(ByVal userId As Int32) As DataTable

        Dim ds As DataSet = Nothing
        ds = Server.Con.ExecuteDataset(CommandType.Text, String.Format(" SELECT * FROM ZIR WHERE UserId IN (SELECT DISTINCT GROUPID from USR_R_GROUP WHERE USRID = {0}  ) or UserId in ( select inheritedusergroup from group_r_group where usergroup in ( Select groupid from usr_r_group where usrid= {0} ))", userId))
        If Not IsNothing(ds) AndAlso ds.Tables.Count > 0 Then
            Return ds.Tables(0)
        Else
            Return Nothing
        End If
    End Function
#End Region

    Public Shared Sub ClearHashTables()
        'If Not IsNothing(Permisos) Then
        '    Permisos.Clear()
        '    Permisos = Nothing
        '    Permisos = New SynchronizedHashtable()
        'End If
    End Sub

    ''' <summary>
    ''' Obtiene si el usuario y/o algun grupo no tiene el check de atributos especificos
    ''' </summary>
    ''' <param name="User"></param>
    ''' <param name="docTypeID"></param>
    ''' <param name="userID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetSpecificAttributeRight(ByVal User As IUser, ByVal docTypeID As Long, ByVal UserGroups As ArrayList) As Boolean


        If Not IsNothing(User) Then
            If Not GetUserRights(User.ID, ObjectTypes.DocTypes, RightsType.ViewRightsByIndex, docTypeID) AndAlso
                        (GetUserRights(User.ID, ObjectTypes.DocTypes, RightsType.View, docTypeID) OrElse GetUserRights(User.ID, ObjectTypes.DocTypes, RightsType.ReIndex, docTypeID)) Then ' redo
                Return False
            Else
                'If IsNothing(User.Groups) OrElse User.Groups.Count = 0 Then
                '    User.Groups = UserGroups
                'End If

                'For Each g As IUserGroup In User.Groups
                '    'Si el grupo no tiene el check y si tiene permisos de ver el documento
                '    If Not GetUserRights(g.ID, ObjectTypes.DocTypes, RightsType.ViewRightsByIndex, docTypeID) AndAlso
                '        (GetUserRights(g.ID, ObjectTypes.DocTypes, RightsType.View, docTypeID) OrElse GetUserRights(g.ID, ObjectTypes.DocTypes, RightsType.ReIndex, docTypeID)) Then
                '        Return False
                '    Else
                '        Return True
                '    End If
                'Next
                Return True
            End If
        Else
            Return True
        End If
    End Function

    Public Function GetAssociateGridColumnsRight(groupOrUserID As Long, docTypeID As Long, docTypeParentID As Long) As DataTable
        Dim query As New StringBuilder
        query.Append("SELECT GROUPORUSERID, DOCTYPEID, DOCTYPEPARENTID, COLUMN_NAME FROM ZVCR WHERE GROUPORUSERID = ")
        query.Append(groupOrUserID & " AND DOCTYPEID =")
        query.Append(docTypeID & " AND DOCTYPEPARENTID =")
        query.Append(docTypeParentID)
        Return Server.Con.ExecuteDataset(CommandType.Text, query.ToString).Tables(0)
    End Function

End Class
