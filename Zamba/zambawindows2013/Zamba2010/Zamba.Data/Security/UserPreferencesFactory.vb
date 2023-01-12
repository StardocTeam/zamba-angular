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
    Public Shared Sub setValueDB(ByVal name As String, ByVal value As String, ByVal section As Sections, ByVal userId As Int64)
        If Server.isOracle Then

            Dim query As New System.Text.StringBuilder()

            query.Append("SELECT c_value FROM ZUserConfig WHERE C_NAME = ")
            query.Append("'")
            query.Append(name)
            query.Append("'")
            query.Append(" AND C_SECTION = ")
            query.Append(section)
            query.Append(" AND C_USERID = ")
            query.Append(userId)

            Dim currentvalue As Object = Server.Con.ExecuteScalar(CommandType.Text, query.ToString())
            query.Clear()

            If currentvalue IsNot Nothing AndAlso Not IsDBNull(currentvalue) AndAlso currentvalue.ToString() <> value Then
                query.Append("UPDATE ZUserConfig set c_value = '")
                query.Append(value)
                query.Append("' where c_name = ")
                query.Append("'")
                query.Append(name)
                query.Append("'")
                query.Append(" and c_section = '")
                query.Append(section)
                query.Append("' and c_userId = '")
                query.Append(userId)
                query.Append("'")
                Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString())
            ElseIf currentvalue Is Nothing Then
                query.Append("INSERT INTO ZUserConfig (c_name, c_value, c_section, c_userid) values (")
                query.Append("'")
                query.Append(name)
                query.Append("'")
                query.Append(", '")
                query.Append(value)
                query.Append("', '")
                query.Append(section)
                query.Append("', '")
                query.Append(userId)
                query.Append("')")
                    Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString())
            End If         

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
    Public Shared Function GetDefaultValueDB(ByVal name As String, ByVal section As Sections) As String
        If Server.isOracle Then
            Dim ReturnValue As Object = Server.Con.ExecuteScalar(CommandType.Text, "Select c_value from ZUserConfig where c_name='" & name & "' and c_section='" & CType(section, Integer) & "' and c_userId=0")
            If IsDBNull(ReturnValue) Then
                Return Nothing
            Else
                Return ReturnValue
            End If
        Else
            Dim parValues() As Object = {name, section, 0}
            Dim ReturnValue As Object = Server.Con.ExecuteScalar("zsp_userpreferences_100_getValueDB", parValues)
            If IsDBNull(ReturnValue) Then
                Return Nothing
            Else
                Return ReturnValue
            End If
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
    Public Shared Function getValueDB(ByVal name As String, ByVal Section As Sections, ByVal userId As Int64) As String
        If Server.isOracle Then
            Dim result = Server.Con.ExecuteScalar(CommandType.Text, "SELECT c_value from ZUserConfig where c_name='" & name & "' and c_section='" & CType(Section, Integer) & "' and (c_userId= " & userId & " or c_userId = 0) order by c_userid desc")
            If IsDBNull(result) Then Return Nothing
            Return result
        Else
            Return Server.Con.ExecuteScalar(CommandType.Text, "SELECT value from ZUserConfig where name='" & name & "' and section='" & CType(Section, Integer) & "' and (userId= " & userId & " or userId = 0) order by userid desc")
        End If
    End Function

    Public Shared Function getAllValuesDB(ByVal UserId As Int64) As DataTable
        If Server.isOracle Then
            Return Server.Con.ExecuteDataset(CommandType.Text, "SELECT c_name name, c_section section, c_value value from ZUserConfig where c_userid= " & UserId).Tables(0)
        Else
            Return Server.Con.ExecuteDataset(CommandType.Text, "SELECT name, section, value from ZUserConfig where userid= " & UserId).Tables(0)
        End If
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
    Public Shared Sub setValueDBByMachine(ByVal name As String, ByVal value As String, ByVal section As Sections, ByVal machineName As String)
        If Server.isOracle Then
            If Not IsDBNull(value) AndAlso String.IsNullOrEmpty(value) = False Then
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
    Public Shared Sub updateValueDBByMachine(ByVal name As String, ByVal value As String, ByVal section As Sections, ByVal machineName As String)
        If Server.isOracle Then
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
    Public Shared Function getValueDBByMachine(ByVal name As String, ByVal Section As Sections, ByVal machineName As String) As String
        If Server.isOracle Then
            Dim parNames() As String = {"m_name", "m_section", "m_userId"}
            Dim parTypes() As Object = {13, 13, 13}
            Dim parValues() As Object = {name, CType(Section, Integer), machineName}

            Dim ReturnValue As Object = Server.Con.ExecuteScalar(CommandType.Text, "SELECT c_value from ZMachineConfig where c_name='" & name & "' and c_section='" & CType(Section, Integer) & "' and c_machinename='" & machineName & "'")
            If IsDBNull(ReturnValue) Then
                Return Nothing
            Else
                Return ReturnValue
            End If
        Else
            Dim parValues() As Object = {name, Section, machineName}
            Dim ReturnValue As Object = Server.Con.ExecuteScalar("zsp_machinepreferences_100_getValueDB", parValues)
            If IsDBNull(ReturnValue) Then
                Return Nothing
            Else
                Return ReturnValue
            End If
        End If
    End Function

    Public Shared Function getAllValuesDBByMachine(ByVal machineName As String) As DataTable
        If Server.isOracle Then
            Return Server.Con.ExecuteDataset(CommandType.Text, "SELECT c_value,c_name from ZMachineConfig where c_machinename= '" & machineName & "'").Tables(0)
        Else
            Return Server.Con.ExecuteDataset(CommandType.Text, "SELECT value,name from ZMachineConfig where machinename= '" & machineName & "'").Tables(0)
        End If
    End Function

End Class