Imports Zamba.Tools
Imports Zamba.Data
Imports Zamba.Membership
Imports Zamba.Servers
Imports System

Partial Public Class UserPreferences


    Public Function getValue(ByVal name As String, ByVal section As UPSections, ByVal DefaultValue As Object, Optional ByVal actualUserId As Int64 = 0) As String
        Try
            Dim userId As Int64 = GetUserId()

            If actualUserId > 0 Then
                userId = actualUserId
            End If

            If Not (Cache.UsersAndGroups.hUserPreferencesByUserIdAndName.ContainsKey(userId & "-" & name & "-" & section)) Then
                LoadAllUserConfigValues(userId)

                If Not (Cache.UsersAndGroups.hUserPreferencesByUserIdAndName.ContainsKey(userId & "-" & name & "-" & section)) OrElse (Cache.UsersAndGroups.hUserPreferencesByUserIdAndName.Item(userId & "-" & name & "-" & section) Is Nothing OrElse
               IsDBNull(Cache.UsersAndGroups.hUserPreferencesByUserIdAndName.Item(userId & "-" & name & "-" & section)) OrElse
                String.IsNullOrEmpty(Cache.UsersAndGroups.hUserPreferencesByUserIdAndName.Item(userId & "-" & name & "-" & section))) Then

                    Dim valor As String = Nothing

                    'Si no hay un usuario logueado, busco en la tabla de PC's
                    If userId <> 0 Then
                            valor = UserPreferencesFactory.getValueDB(name, section, 0)
                        End If

                        'Si no esta en la BD
                        If valor Is Nothing OrElse IsDBNull(valor) OrElse String.IsNullOrEmpty(valor) Then
                            valor = getValueForMachine(name, section, DefaultValue)

                            'Si no esta en el userconfig
                            If valor Is Nothing OrElse IsDBNull(valor) OrElse String.IsNullOrEmpty(valor) Then
                                If DefaultValue IsNot Nothing AndAlso DefaultValue.ToString.Length > 0 Then
                                    UserPreferencesFactory.setValueDB(name, DefaultValue, section, 0)
                                    UserPreferencesFactory.setValueDB(name, DefaultValue, section, userId)
                                End If
                                valor = DefaultValue
                            End If
                        End If

                    SyncLock Cache.UsersAndGroups.hUserPreferencesByUserIdAndName.SyncRoot
                        If Not (Cache.UsersAndGroups.hUserPreferencesByUserIdAndName.ContainsKey(userId & "-" & name & "-" & section)) Then
                            Cache.UsersAndGroups.hUserPreferencesByUserIdAndName.Add(userId & "-" & name & "-" & section, valor)
                        Else
                            Cache.UsersAndGroups.hUserPreferencesByUserIdAndName(userId & "-" & name & "-" & section) = valor
                        End If
                    End SyncLock
                    Return valor

                End If
            End If
            If (Cache.UsersAndGroups.hUserPreferencesByUserIdAndName.Item(userId & "-" & name & "-" & section) Is Nothing OrElse
                IsDBNull(Cache.UsersAndGroups.hUserPreferencesByUserIdAndName.Item(userId & "-" & name & "-" & section)) OrElse
                String.IsNullOrEmpty(Cache.UsersAndGroups.hUserPreferencesByUserIdAndName.Item(userId & "-" & name & "-" & section))) Then
                Return String.Empty
            End If
            Return Cache.UsersAndGroups.hUserPreferencesByUserIdAndName.Item(userId & "-" & name & "-" & section)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return String.Empty
        End Try
    End Function

    Public Function getEspecificUserValue(ByVal name As String, ByVal section As UPSections, ByVal DefaultValue As Object, userId As Int64) As String
        Try
            Dim valor As String

            If Not Cache.UsersAndGroups.hUserPreferencesByUserIdAndName.ContainsKey(userId & "-" & name & "-" & section) Then


                valor = UserPreferencesFactory.getValueDB(name, section, userId)

                'Si no esta en la BD
                If valor Is Nothing OrElse IsDBNull(valor) OrElse String.IsNullOrEmpty(valor) Then
                    valor = UserPreferencesFactory.GetDefaultValueDB(name, section)

                    'Si no esta en el userconfig
                    If valor Is Nothing OrElse IsDBNull(valor) OrElse String.IsNullOrEmpty(valor) Then
                        If DefaultValue IsNot Nothing AndAlso DefaultValue.ToString.Length > 0 Then

                            UserPreferencesFactory.setValueDB(name, DefaultValue, section, 0)
                        End If
                        valor = DefaultValue
                    End If
                End If

                SyncLock Cache.UsersAndGroups.hUserPreferencesByUserIdAndName.SyncRoot
                    If Not Cache.UsersAndGroups.hUserPreferencesByUserIdAndName.ContainsKey(userId & "-" & name & "-" & section) Then
                        Cache.UsersAndGroups.hUserPreferencesByUserIdAndName.Add(userId & "-" & name & "-" & section, valor)
                    Else
                        Cache.UsersAndGroups.hUserPreferencesByUserIdAndName(userId & "-" & name & "-" & section) = valor
                    End If
                End SyncLock
            End If
            valor = Cache.UsersAndGroups.hUserPreferencesByUserIdAndName.Item(userId & "-" & name & "-" & section)
            If IsDBNull(valor) Then
                Return String.Empty
            Else
                Return valor
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return String.Empty
        End Try
    End Function


    Public Function getValueForMachine(ByVal name As String, ByVal Section As UPSections, ByVal DefaultValue As Object) As String
        Try


            If Not Cache.UsersAndGroups.hsAllMachineConfigValues.ContainsKey(name) Then
                LoadAllMachineConfigValues()
                If Not Cache.UsersAndGroups.hsAllMachineConfigValues.ContainsKey(name) Then

                    Dim valor As String ' = UserPreferencesFactory.getValueDBByMachine(name, Section, Environment.MachineName)

                        'si no hay valor especifico de la PC busco el valor por default para las PC
                        valor = UserPreferencesFactory.getValueDBByMachine(name, Section, "default")

                        'Si no esta en la zmachineconfig el valor por defecto de las PC
                        If valor Is Nothing Then
                            If DefaultValue IsNot Nothing AndAlso DefaultValue.ToString.Length > 0 Then
                                'Guardo el valor por defecto del codigo en el valor por defecto de las PC
                                UserPreferencesFactory.setValueDBByMachine(name, DefaultValue, Section, "default")
                                UserPreferencesFactory.setValueDBByMachine(name, DefaultValue, Section, Environment.MachineName)
                            End If
                            valor = DefaultValue
                        Else
                            UserPreferencesFactory.setValueDBByMachine(name, DefaultValue, Section, Environment.MachineName)
                        End If

                    SyncLock Cache.UsersAndGroups.hsAllMachineConfigValues.SyncRoot
                        If Not Cache.UsersAndGroups.hsAllMachineConfigValues.ContainsKey(name) Then
                            Cache.UsersAndGroups.hsAllMachineConfigValues.Add(name, valor)
                        End If
                    End SyncLock

                End If
                Return Cache.UsersAndGroups.hsAllMachineConfigValues.Item(name)
            End If
            Return Cache.UsersAndGroups.hsAllMachineConfigValues.Item(name)


        Catch ex As System.Threading.ThreadAbortException
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(New Exception("Error al buscar el valor de " & name & " " & ex.ToString))
            Return String.Empty
        End Try
    End Function

    <Obsolete("Esta funcion es obsoleta por que obtiene el CurrentUser del MembershipHelper.")>
    Public Shared Sub setValue(ByVal name As String, ByVal valor As String, ByVal Section As UPSections)
        Try
            'Si no hay un usuario logueado, busco en la tabla de PC's
            If Zamba.Membership.MembershipHelper.CurrentUser Is Nothing Then
                If UserPreferencesFactory.getValueDBByMachine(name, Section, Environment.MachineName) Is Nothing Then
                    UserPreferencesFactory.setValueDBByMachine(name, valor, Section, Environment.MachineName)
                Else
                    UserPreferencesFactory.updateValueDBByMachine(name, valor, Section, Environment.MachineName)
                End If

            Else
                UserPreferencesFactory.setValueDB(name, valor, Section, Membership.MembershipHelper.CurrentUser.ID)
                Dim userId As Int64 = Zamba.Membership.MembershipHelper.CurrentUser.ID

                SyncLock Cache.UsersAndGroups.hUserPreferencesByUserIdAndName.SyncRoot
                    If Cache.UsersAndGroups.hUserPreferencesByUserIdAndName.ContainsKey(userId & "-" & name & "-" & Section) Then
                        Cache.UsersAndGroups.hUserPreferencesByUserIdAndName(userId & "-" & name & "-" & Section) = valor
                    Else
                        Cache.UsersAndGroups.hUserPreferencesByUserIdAndName.Add(userId & "-" & name & "-" & Section, valor)
                    End If
                End SyncLock
            End If


        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shared Sub setValue(ByVal name As String, ByVal valor As String, ByVal Section As UPSections, ByVal userId As Long)
        Try
            'Si no hay un usuario logueado, busco en la tabla de PC's
            If Zamba.Membership.MembershipHelper.CurrentUser Is Nothing Then
                If UserPreferencesFactory.getValueDBByMachine(name, Section, Environment.MachineName) Is Nothing Then
                    UserPreferencesFactory.setValueDBByMachine(name, valor, Section, Environment.MachineName)
                Else
                    UserPreferencesFactory.updateValueDBByMachine(name, valor, Section, Environment.MachineName)
                End If

            Else
                UserPreferencesFactory.setValueDB(name, valor, Section, userId)


                SyncLock Cache.UsersAndGroups.hUserPreferencesByUserIdAndName.SyncRoot
                    If Cache.UsersAndGroups.hUserPreferencesByUserIdAndName.ContainsKey(userId & "-" & name & "-" & Section) Then
                        Cache.UsersAndGroups.hUserPreferencesByUserIdAndName(userId & "-" & name & "-" & Section) = valor
                    Else
                        Cache.UsersAndGroups.hUserPreferencesByUserIdAndName.Add(userId & "-" & name & "-" & Section, valor)
                    End If
                End SyncLock
            End If


        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shared Sub setValueLastUser(ByVal name As String, ByVal valor As String, ByVal Section As UPSections, ByVal userId As Long)
        Try

            UserPreferencesFactory.setValueDB(name, valor, Section, userId)


            SyncLock Cache.UsersAndGroups.hUserPreferencesByUserIdAndName.SyncRoot
                If Cache.UsersAndGroups.hUserPreferencesByUserIdAndName.ContainsKey(userId & "-" & name & "-" & Section) Then
                    Cache.UsersAndGroups.hUserPreferencesByUserIdAndName(userId & "-" & name & "-" & Section) = valor
                Else
                    Cache.UsersAndGroups.hUserPreferencesByUserIdAndName.Add(userId & "-" & name & "-" & Section, valor)
                End If
            End SyncLock



        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Sub setEspecificUserValue(ByVal name As String, ByVal valor As String, ByVal Section As UPSections, UserId As Int64)
        Try
            UserPreferencesFactory.setValueDB(name, valor, Section, UserId)
            SyncLock Cache.UsersAndGroups.hUserPreferencesByUserIdAndName.SyncRoot
                If Cache.UsersAndGroups.hUserPreferencesByUserIdAndName.ContainsKey(UserId & "-" & name & "-" & Section) Then
                    Cache.UsersAndGroups.hUserPreferencesByUserIdAndName(UserId & "-" & name & "-" & Section) = valor
                Else
                    Cache.UsersAndGroups.hUserPreferencesByUserIdAndName.Add(UserId & "-" & name & "-" & Section, valor)
                End If
            End SyncLock
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Enum InitialSizes
        Normal
        Height
        Width
    End Enum

    Public Sub LoadAllMachineConfigValues()
        If Cache.UsersAndGroups.hsAllMachineConfigValues.Count = 0 Then
            Dim values As DataTable = Nothing
            Try
                values = UserPreferencesFactory.getAllValuesDBByMachine(Environment.MachineName)
                SyncLock Cache.UsersAndGroups.hsAllMachineConfigValues.SyncRoot
                    For Each Value As DataRow In values.Rows
                        If Cache.UsersAndGroups.hsAllMachineConfigValues.ContainsKey(Value(1)) = False Then Cache.UsersAndGroups.hsAllMachineConfigValues.Add(Value(1), Value(0))
                    Next
                End SyncLock
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(New Exception("Error al cargar las preferencias de maquina. " & ex.ToString))
            End Try
        End If

    End Sub

    Public Function LoadAllUserAndGroupConfigValuesForModule(ByVal UserId As Int64) As DataTable
        Dim values As DataTable = Nothing
        values = UserPreferencesFactory.getAllValuesDB(UserId)
        Return values
    End Function

    Public Sub LoadAllUserConfigValues(ByVal UserId As Int64)
        Dim values As DataTable = Nothing
        Try
            values = UserPreferencesFactory.getAllValuesDB(UserId)
            SyncLock Cache.UsersAndGroups.hUserPreferencesByUserIdAndName.SyncRoot
                For Each Value As DataRow In values.Rows
                    If (IsDBNull(Value("Value")) = False) Then
                        If Cache.UsersAndGroups.hUserPreferencesByUserIdAndName.ContainsKey(UserId.ToString() & "-" & Value("Name") & "-" & Value("Section")) = False Then
                            Cache.UsersAndGroups.hUserPreferencesByUserIdAndName.Add(UserId & "-" & Value("Name") & "-" & Value("Section"), Value("Value"))
                        Else
                            Cache.UsersAndGroups.hUserPreferencesByUserIdAndName(UserId & "-" & Value("Name") & "-" & Value("Section")) = Value("Value")
                        End If
                    End If
                Next
            End SyncLock
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(New Exception("Error al cargar las preferencias del usuario. " & ex.ToString))
        End Try

    End Sub


    Private Shared Function GetUserId() As Int64
        If Zamba.Membership.MembershipHelper.CurrentUser Is Nothing Then
            Return 0
        Else
            Return Zamba.Membership.MembershipHelper.CurrentUser.ID
        End If
    End Function
End Class