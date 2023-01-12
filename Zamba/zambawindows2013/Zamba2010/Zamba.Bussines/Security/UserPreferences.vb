Imports Zamba.Tools
Imports Zamba.Data

Public Class UserPreferences
   
    '''Devuelve el valor de un item por el nombre
    Public Shared Function getValue(ByVal name As String, ByVal Section As Sections, ByVal DefaultValue As Object) As String
        Try
            If Cache.UsersAndGroups.hsUserPreferences.Contains(name) Then
                Return Cache.UsersAndGroups.hsUserPreferences(name)
            Else
                Dim valor As String
                Dim userId As Int64

                'Si no hay un usuario logueado, busco en la tabla de PC's zmachineconfig
                If Membership.MembershipHelper.CurrentUser Is Nothing Or name.ToLower() = "userid" Then
                    'busco el valor especifico de la PC
                    valor = UserPreferencesFactory.getValueDBByMachine(name, Section, Environment.MachineName)

                    If valor Is Nothing Then
                        'si no hay valor especifico de la PC busco el valor por default para las PC
                        valor = UserPreferencesFactory.getValueDBByMachine(name, Section, "default")

                        'Si no esta en la zmachineconfig el valor por defecto de las PC
                        If valor Is Nothing Then
                            'Guardo el valor por defecto del codigo en el valor por defecto de las PC
                            UserPreferencesFactory.setValueDBByMachine(name, DefaultValue, Section, "default")
                            valor = DefaultValue
                        End If
                    End If
                Else
                    'Hay usuario logueado, entonces vamos a usar la zuserconfig
                    userId = Membership.MembershipHelper.CurrentUser.ID
                    'busco el valor especifico del usuario
                    valor = UserPreferencesFactory.getValueDB(name, Section, userId)

                    If valor Is Nothing Then
                        'Si no hay valor especifico del usuario  busco el valor por defecto Usuario 0
                        valor = UserPreferencesFactory.GetDefaultValueDB(name, Section)

                        'Si no tiene un valor por defecto de usuario 0 en la base
                        If valor Is Nothing Then
                            'guardo el valor por defecto del codigo en el usuario por defecto 0
                            UserPreferencesFactory.setValueDB(name, DefaultValue, Section, 0)
                            valor = DefaultValue
                        End If
                    End If
                End If

                If Not Cache.UsersAndGroups.hsUserPreferences.ContainsKey(name) Then
                    Cache.UsersAndGroups.hsUserPreferences.Add(name, valor)
                End If

                Return valor
            End If

        Catch ex As System.Threading.ThreadAbortException
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(New Exception("Error al buscar el valor de " & name & " " & ex.ToString))
            Return DefaultValue
        End Try
    End Function

    '''Devuelve el valor de un item por el nombre
    Public Shared Function getValueForMachine(ByVal name As String, ByVal Section As Sections, ByVal DefaultValue As Object) As String
        Try
            If Cache.UsersAndGroups.hsUserPreferences.Contains(name) Then
                Return Cache.UsersAndGroups.hsUserPreferences(name)
            Else
                Dim valor As String = UserPreferencesFactory.getValueDBByMachine(name, Section, Environment.MachineName)

                If valor Is Nothing Then
                    'si no hay valor especifico de la PC busco el valor por default para las PC
                    valor = UserPreferencesFactory.getValueDBByMachine(name, Section, "default")

                    'Si no esta en la zmachineconfig el valor por defecto de las PC
                    If valor Is Nothing Then
                        'Guardo el valor por defecto del codigo en el valor por defecto de las PC
                        UserPreferencesFactory.setValueDBByMachine(name, DefaultValue, Section, "default")
                        valor = DefaultValue
                    End If
                End If

                If Not Cache.UsersAndGroups.hsUserPreferences.ContainsKey(name) Then
                    Cache.UsersAndGroups.hsUserPreferences.Add(name, valor)
                End If

                Return valor
            End If
        Catch ex As System.Threading.ThreadAbortException
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(New Exception("Error al buscar el valor de " & name & " " & ex.ToString))
            Return DefaultValue
        End Try
    End Function

    '''agrega un item por el nombre y le asigna el valor
    Public Shared Sub setValue(ByVal name As String, ByVal valor As String, ByVal Section As Sections)
        Try
            'Si no hay un usuario logueado, busco en la tabla de PC's
            If Membership.MembershipHelper.CurrentUser Is Nothing Then
                UserPreferencesFactory.setValueDBByMachine(name, valor, Section, Environment.MachineName)
            Else
                UserPreferencesFactory.setValueDB(name, valor, Section, Membership.MembershipHelper.CurrentUser.ID)
            End If
            If (Cache.UsersAndGroups.hsUserPreferences.ContainsKey(name)) Then
                Cache.UsersAndGroups.hsUserPreferences(name) = valor
            Else
                Cache.UsersAndGroups.hsUserPreferences.Add(name, valor)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    '''agrega un item por el nombre y le asigna el valor
    Public Shared Sub setValueForMachine(ByVal name As String, ByVal valor As String, ByVal Section As Sections)
        Try
            UserPreferencesFactory.setValueDBByMachine(name, valor, Section, Environment.MachineName)
            If (Cache.UsersAndGroups.hsUserPreferences.ContainsKey(name)) Then
                Cache.UsersAndGroups.hsUserPreferences(name) = valor
            Else
                Cache.UsersAndGroups.hsUserPreferences.Add(name, valor)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Enum InitialSizes
        Normal
        Height
        Width
    End Enum

    Public Shared Sub LoadAllMachineConfigValues()
        Dim values As DataTable = Nothing
        Try
            values = UserPreferencesFactory.getAllValuesDBByMachine(Environment.MachineName)
            SyncLock Cache.UsersAndGroups.hsUserPreferences
            For Each Value As DataRow In values.Rows
                If Cache.UsersAndGroups.hsUserPreferences.ContainsKey(Value(1)) = False Then Cache.UsersAndGroups.hsUserPreferences.Add(Value(1), Value(0))
            Next
            End SyncLock
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(New Exception("Error al cargar las preferencias de maquina" & ex.ToString))
        Finally
            If values IsNot Nothing Then
                values.Dispose()
                values = Nothing
            End If
        End Try
    End Sub

    Public Shared Sub LoadAllUserConfigValues()
        If Membership.MembershipHelper.CurrentUser IsNot Nothing Then
            Dim values As DataTable = Nothing
            Try
                values = UserPreferencesFactory.getAllValuesDB(Membership.MembershipHelper.CurrentUser.ID)
                SyncLock Cache.UsersAndGroups.hsUserPreferences
                For Each Value As DataRow In values.Rows
                    If Cache.UsersAndGroups.hsUserPreferences.ContainsKey(Value("name")) = False Then Cache.UsersAndGroups.hsUserPreferences.Add(Value("name"), Value("value"))
                Next
                    End SyncLock
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(New Exception("Error al cargar las preferencias del usuario" & ex.ToString))
            Finally
                If values IsNot Nothing Then
                    values.Dispose()
                    values = Nothing
                End If
            End Try
        End If
    End Sub

End Class