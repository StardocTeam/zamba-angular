Imports Zamba.Data

Public Class ZOptBusiness

    Public Shared Sub Insert(ByVal key As String, ByVal value As String)
        ZOptFactory.Insert(key, value)
    End Sub

    Public Shared Sub Update(ByVal key As String, ByVal value As String)
        ZOptFactory.Update(key, value)
    End Sub

    Public Function GetValue(ByVal key As String) As String
        If Cache.UsersAndGroups.hsOptions.ContainsKey(key) Then
            Return Cache.UsersAndGroups.hsOptions(key)
        End If

        Dim zoptf As New ZOptFactory
        Dim Value As String = zoptf.GetValue(key)
        zoptf = Nothing

        '  If Value Is Nothing Then
        'ZTrace.WriteLineIf(ZTrace.IsError, "El valor de la clave " & key & _
        '                 " no se encuentra configurada en ZOPT. Esto puede ocasionar errores o comportamientos no esperados en la aplicación.")
        ' End If

        SyncLock Cache.UsersAndGroups.hsOptions.SyncRoot
            If Not Cache.UsersAndGroups.hsOptions.Contains(key) Then
                Cache.UsersAndGroups.hsOptions.Add(key, Value)
            End If
        End SyncLock

        Return Value
    End Function

    Public Shared Function GetValueOrDefault(ByVal key As String, DefaultValue As String) As String
        If Cache.UsersAndGroups.hsOptions.ContainsKey(key) Then
            Return Cache.UsersAndGroups.hsOptions(key)
        End If

        Dim zoptf As New ZOptFactory
        Dim Value As String = zoptf.GetValue(key)
        zoptf = Nothing

        If Value Is Nothing Then
            InsertUpdateValue(key, DefaultValue)
            Value = DefaultValue
        End If
        If Cache.UsersAndGroups.hsOptions.ContainsKey(key) = False Then Cache.UsersAndGroups.hsOptions.Add(key, Value)
        Return Value
    End Function

    Public Function GetValueOrDefaultNonShared(ByVal key As String, DefaultValue As String) As String
        If Cache.UsersAndGroups.hsOptions.ContainsKey(key) Then
            Return Cache.UsersAndGroups.hsOptions(key)
        End If

        Dim zoptf As New ZOptFactory
        Dim Value As String = zoptf.GetValue(key)
        zoptf = Nothing

        If Value Is Nothing AndAlso DefaultValue <> String.Empty Then
            InsertUpdateValue(key, DefaultValue)
            Value = DefaultValue
        End If
        If Cache.UsersAndGroups.hsOptions.ContainsKey(key) = False Then Cache.UsersAndGroups.hsOptions.Add(key, Value)
        Return Value
    End Function

    Public Sub LoadAllOptions()
        If Cache.UsersAndGroups.hsOptions.Count = 0 Then
            SyncLock Cache.UsersAndGroups.hsOptions
                Dim values As DataTable = Nothing
                Try
                    values = ZOptFactory.getAllOptionsValues()
                    For Each Value As DataRow In values.Rows
                        If Cache.UsersAndGroups.hsOptions.ContainsKey(Value(0)) = False Then Cache.UsersAndGroups.hsOptions.Add(Value(0), Value(1))
                    Next
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(New Exception("Error al cargar las preferencias de maquina. " & ex.ToString))
                Finally
                    If values IsNot Nothing Then
                        values.Dispose()
                        values = Nothing
                    End If
                End Try
            End SyncLock
        End If
    End Sub

    Public Shared Sub InsertUpdateValue(ByVal key As String, ByVal value As String)
        If Cache.UsersAndGroups.hsOptions.ContainsKey(key) = False Then
            Cache.UsersAndGroups.hsOptions.Add(key, value)
        Else
            Cache.UsersAndGroups.hsOptions(key) = value
        End If

        Dim zoptf As New ZOptFactory
        zoptf.InsertUpdateValue(key, value)
        zoptf = Nothing
    End Sub

    Public Shared Sub ClearCache()
        Cache.UsersAndGroups.hsOptions = New SynchronizedHashtable
    End Sub
End Class
