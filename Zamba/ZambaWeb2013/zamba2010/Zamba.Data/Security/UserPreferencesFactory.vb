Imports Zamba.Core

''' <summary>
''' Llamadas a la base de datos del UserConfig
''' </summary>
''' <history>Marcelo Created 29/10/2010</history>
''' <remarks></remarks>
Public Class UserPreferencesFactory

    ''' <summary>
    ''' Guarda en la base el valor de la configuracion
    ''' </summary>
    ''' <param name="name">Nombre de la configuracion a obtener</param>
    ''' <param name="value">Valor de la configuracion</param>
    ''' <param name="section">Seccion a la que pertenece la configuracion</param>
    ''' <param name="userId">ID del usuario a quien pertenece la configuracion</param>
    ''' <history>Marcelo Created 29/10/2010</history>
    ''' <remarks></remarks>
    Public Shared Sub setValueDB(ByVal name As String, ByVal value As String, ByVal section As UPSections, ByVal userId As Int64)
        If Server.isOracle Then
            Dim parNames() As String = {"m_name", "m_value", "m_section", "m_userId"}
            ' Dim parTypes() As Object = {OracleType.VarChar, OracleType.VarChar, OracleType.Number, OracleType.VarChar}
            Dim parValues() As Object = {name, value, CType(section, Integer), userId}

            Server.Con.ExecuteNonQuery("zsp_userpreferences_100.setValueDB", parValues)
        Else
            Dim parValues() As Object = {name, value, section, userId}
            Server.Con.ExecuteNonQuery("zsp_userpreferences_100_setValueDB", parValues)
        End If
    End Sub

    ''' <summary>
    ''' Obtiene el valor por defecto de la configuracion
    ''' </summary>
    ''' <param name="name">Nombre de la configuracion a obtener</param>
    ''' <param name="section">Seccion donde se encuentra la configuracion</param>
    ''' <param name="userId">Id del usuario, en principio no se utiliza, pero se pide por si a futuro se hacen configuracion por defecto por grupo</param>
    ''' <history>Marcelo Created 29/10/2010</history>
    ''' <remarks></remarks>
    Public Shared Function GetDefaultValueDB(ByVal name As String, ByVal section As UPSections) As String
        Dim value As Object
        If Server.isOracle Then
            value = Server.Con.ExecuteScalar(CommandType.Text, "SELECT c_value from ZUserConfig where c_name='" & name & "' and c_section='" & CType(section, Integer) & "' and c_userId=0")
        Else
            Dim parValues() As Object = {name, section, 0}
            value = Server.Con.ExecuteScalar("zsp_userpreferences_100_getValueDB", parValues)
        End If
        If IsDBNull(value) Then
            Return String.Empty
        ElseIf value Is Nothing Then
            Return String.Empty
        Else
            Return value.ToString()
        End If
    End Function

    ''' <summary>
    ''' Obtiene el valor por defecto de la configuracion
    ''' </summary>
    ''' <param name="name">Nombre de la configuracion a obtener</param>
    ''' <param name="section">Seccion donde se encuentra la configuracion</param>
    ''' <param name="userId">Id del usuario</param>
    ''' <history>Marcelo Created 29/10/2010</history>
    ''' <remarks></remarks>
    Public Shared Function getValueDB(ByVal name As String, ByVal Section As UPSections, ByVal userId As Int64) As String
        If Server.isOracle Then
            Dim value As Object = Server.Con.ExecuteScalar(CommandType.Text, "SELECT c_value from ZUserConfig where c_name='" & name & "' and c_section='" & CType(Section, Integer) & "' and c_userId= " & userId)
            If IsDBNull(value) Then
                Return Nothing
            Else
                Return value
            End If
        Else
            Dim parValues() As Object = {name, Section, userId}
            Return Server.Con.ExecuteScalar("zsp_userpreferences_100_getValueDB", parValues)
        End If
    End Function

    'Public Shared Function getAllValuesDB(ByVal UserId As Int64) As DataTable
    '    If Server.isOracle Then
    '        Return Server.Con.ExecuteDataset(CommandType.Text, "SELECT c_name, c_section, c_value from ZUserConfig where c_userid= " & UserId).Tables(0)
    '    Else
    '        Return Server.Con.ExecuteDataset("Zsp_USERRIGHT_100_GetValues", New Object() {UserId}).Tables(0)
    '    End If
    'End Function

    Public Shared Function getAllValuesDB(ByVal UserId As Int64) As DataTable
        Try

            If Server.isOracle Then
                Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, String.Format("SELECT c_userid userid, c_name name, c_section section, c_value value from ZUserConfig where c_userid = {0} union ALL  SELECT c_userid userid, c_name name, c_section section, c_value value from ZUserConfig d  where not exists( select 1 from ZUserConfig l where d.c_name = l.c_name and d.c_section = l.c_section and l.c_userid = {0} ) and (d.c_userid in ( select inheritedusergroup from group_r_group where usergroup = {0})  or d.c_userid in (  Select groupid from usr_r_group where usrid= {0})  or d.c_userid in ( select inheritedusergroup from group_r_group where usergroup in ( select groupid from usr_r_group where usrid = {0})) ) union ALL  SELECT c_userid userid, c_name name, c_section section, c_value value from ZUserConfig d  where not exists(  select 1 from ZUserConfig l where d.c_name = l.c_name and d.c_section = l.c_section and (l.c_userid = {0}  or (l.c_userid in ( select inheritedusergroup from group_r_group where usergroup = {0})  or l.c_userid in (  Select groupid from usr_r_group where usrid= {0})  or l.c_userid in ( select inheritedusergroup from group_r_group where usergroup in ( select groupid from usr_r_group where usrid = {0})) ) )) and d.c_userid = 0", UserId))

                If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                    Return ds.Tables(0)
                Else
                    Return Nothing
                End If
            Else
                Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, String.Format("SELECT userid userid, name name, section section, value value from ZUserConfig where userid = {0} union ALL  SELECT userid userid, name name, section section, value value from ZUserConfig d  where not exists( select 1 from ZUserConfig l where d.name = l.name and d.section = l.section and l.userid = {0} ) and (d.userid in ( select inheritedusergroup from group_r_group where usergroup = {0})  or d.userid in (  Select groupid from usr_r_group where usrid= {0})  or d.userid in ( select inheritedusergroup from group_r_group where usergroup in ( select groupid from usr_r_group where usrid = {0})) ) union ALL  SELECT userid userid, name name, section section, value value from ZUserConfig d  where not exists(  select 1 from ZUserConfig l where d.name = l.name and d.section = l.section and (l.userid = {0}  or (l.userid in ( select inheritedusergroup from group_r_group where usergroup = {0})  or l.userid in (  Select groupid from usr_r_group where usrid= {0})  or l.userid in ( select inheritedusergroup from group_r_group where usergroup in ( select groupid from usr_r_group where usrid = {0})) ) )) and d.userid = 0", UserId))

                If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                    Return ds.Tables(0)
                Else
                    Return Nothing
                End If
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function



    ''' <summary>
    ''' Guarda en la base el valor de la configuracion
    ''' </summary>
    ''' <param name="name">Nombre de la configuracion a obtener</param>
    ''' <param name="value">Valor de la configuracion</param>
    ''' <param name="section">Seccion a la que pertenece la configuracion</param>
    ''' <param name="userId">ID del usuario a quien pertenece la configuracion</param>
    ''' <history>Marcelo Created 29/10/2010</history>
    ''' <remarks></remarks>
    Public Shared Sub setValueDBByMachine(ByVal name As String, ByVal value As String, ByVal section As UPSections, ByVal machineName As String)
        If Server.isOracle Then
            If Not IsDBNull(value) AndAlso String.IsNullOrEmpty(value) = False Then
                Dim parNames() As String = {"m_name", "m_value", "m_section", "m_machineName"}
                ' Dim parTypes() As Object = {OracleType.VarChar, OracleType.VarChar, OracleType.Number, OracleType.VarChar}
                Dim parValues() As Object = {name, value, CType(section, Integer), machineName}

                Server.Con.ExecuteNonQuery("zsp_machinepreferences_100.setValueDB", parValues)
            End If
        Else
            Dim parValues() As Object = {name, value, section, machineName}
            Server.Con.ExecuteNonQuery("zsp_machinepreferences_100_setValueDB", parValues)
        End If
    End Sub

    ''' <summary>
    ''' Actualiza en la base el valor de la configuracion si existe
    ''' </summary>
    ''' <param name="name">Nombre de la configuracion a obtener</param>
    ''' <param name="value">Valor de la configuracion</param>
    ''' <param name="section">Seccion a la que pertenece la configuracion</param>
    ''' <param name="userId">ID del usuario a quien pertenece la configuracion</param>
    ''' <history>Marcelo Created 29/10/2010</history>
    ''' <remarks></remarks>
    Public Shared Sub updateValueDBByMachine(ByVal name As String, ByVal value As String, ByVal section As UPSections, ByVal machineName As String)
        If Server.isOracle Then
            Dim parNames() As String = {"m_name", "m_value", "m_section", "m_machineName"}
            ' Dim parTypes() As Object = {OracleType.VarChar, OracleType.VarChar, OracleType.Number, OracleType.VarChar}
            Dim parValues() As Object = {name, value, CType(section, Integer), machineName}

            Server.Con.ExecuteNonQuery("zsp_machinepreferences_100.updateValueDB", parValues)
        Else
            Dim parValues() As Object = {name, value, section, machineName}
            Server.Con.ExecuteNonQuery("zsp_machinepreferences_100_updateValueDB", parValues)
        End If
    End Sub

    ''' <summary>
    ''' Obtiene el valor por defecto de la configuracion
    ''' </summary>
    ''' <param name="name">Nombre de la configuracion a obtener</param>
    ''' <param name="section">Seccion donde se encuentra la configuracion</param>
    ''' <param name="userId">Id del usuario</param>
    ''' <history>Marcelo Created 29/10/2010</history>
    ''' <remarks></remarks>
    Public Shared Function getValueDBByMachine(ByVal name As String, ByVal Section As UPSections, ByVal machineName As String) As String
        If Server.isOracle Then
            Dim Result = Server.Con.ExecuteScalar(CommandType.Text, "SELECT c_value from ZMachineConfig where c_name='" & name & "' and c_section='" & CType(Section, Integer) & "' and c_machinename='" & machineName & "'")
            If Result Is Nothing Then
                Return String.Empty
            Else
                Return Result.ToString
            End If
        Else
            Dim result = Server.Con.ExecuteScalar(CommandType.Text, "SELECT value from ZMachineConfig where name='" & name & "' and section='" & CType(Section, Integer) & "' and machinename='" & machineName & "'")
            If result Is Nothing Then
                Return String.Empty
            Else
                Return result.ToString
            End If

        End If
    End Function

    Public Shared Function getAllValuesDBByMachine(ByVal machineName As String) As DataTable
        Dim ReturnValue As DataSet
        If Server.isOracle Then
            ReturnValue = Server.Con.ExecuteDataset(CommandType.Text, "SELECT c_value,c_name from ZMachineConfig where c_machinename= '" & machineName & "'")
        Else
            ReturnValue = Server.Con.ExecuteDataset(CommandType.Text, "SELECT value,name from ZMachineConfig where machinename= '" & machineName & "'")
        End If

        If Not IsNothing(ReturnValue) AndAlso ReturnValue.Tables.Count > 0 Then
            Return ReturnValue.Tables(0)
        End If
        Return Nothing
    End Function

End Class