Imports System.Collections.Generic

Public Class GlobalVariables
    Private Shared _zGlobals As New Hashtable

    Public Shared Sub Add(ByVal key As Object, ByVal value As Object)
        _zGlobals.Add(key.ToString().ToLower(), value)
    End Sub

    Public Shared Sub Remove(ByVal key As Object)
        _zGlobals.Remove(key.ToString().ToLower())
    End Sub

    Public Shared Function ContainsKey(ByVal key As Object) As Boolean
        Return _zGlobals.ContainsKey(key.ToString().ToLower())
    End Function

    Public Shared Function clone() As Hashtable
        Return _zGlobals.Clone()
    End Function

    Public Shared Function Keys() As System.Collections.ICollection
        Return _zGlobals.Keys
    End Function

    Public Shared Property Item(ByVal key As Object) As Object
        Get
            Return _zGlobals.Item(key.ToString().ToLower())
        End Get
        Set(ByVal value As Object)
            _zGlobals.Item(key.ToString().ToLower()) = value
        End Set
    End Property

    Public Shared Sub Clear()
        Dim keys As New List(Of String)
        Try
            For Each key As String In VariablesInterReglas.Keys
                keys.Add(key)
            Next

            For Each key As String In keys
                If TypeOf _zGlobals(key) Is IDisposable Then
                    _zGlobals(key).dispose()
                End If
                _zGlobals(key) = Nothing
            Next

            _zGlobals.Clear()
            _zGlobals = Nothing
            _zGlobals = New Hashtable()
        Finally
            keys.Clear()
            keys = Nothing
        End Try
    End Sub
End Class
