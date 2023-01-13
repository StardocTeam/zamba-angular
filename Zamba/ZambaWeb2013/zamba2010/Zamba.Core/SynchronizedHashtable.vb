Public Class SynchronizedHashtable
    'Inherits Hashtable

    Public _syncronizedHT As Hashtable

    Public Sub New()
        _syncronizedHT = Hashtable.Synchronized(New Hashtable)
    End Sub

    Public Sub Add(ByVal key As Object, ByVal value As Object)
        _syncronizedHT.Add(key, value)
    End Sub

    Default Public Property Item(ByVal key As Object) As Object
        Get
            Return _syncronizedHT.Item(key)
        End Get
        Set(ByVal value As Object)
            _syncronizedHT.Item(key) = value
        End Set
    End Property

    Public Sub Clear()
        _syncronizedHT.Clear()
    End Sub

    Public Function Contains(ByVal key As Object) As Boolean
        Return _syncronizedHT.Contains(key)
    End Function

    Public Function ContainsKey(ByVal key As Object) As Boolean
        Return _syncronizedHT.ContainsKey(key)
    End Function

    Public Function Clone() As Object
        Return _syncronizedHT.Clone()
    End Function

    Public Function ContainsValue(ByVal value As Object) As Boolean
        Return _syncronizedHT.ContainsValue(value)
    End Function

    Public ReadOnly Property Count As Integer
        Get
            Return _syncronizedHT.Count
        End Get
    End Property

    Public Sub Remove(ByVal key As Object)
        _syncronizedHT.Remove(key)
    End Sub

    Public Sub CopyTo(ByVal array As System.Array, ByVal arrayIndex As Integer)
        _syncronizedHT.CopyTo(array, arrayIndex)
    End Sub

    Public Function Equals(ByVal obj As Object) As Boolean
        Return _syncronizedHT.Equals(obj)
    End Function

    Public Function GetEnumerator() As System.Collections.IDictionaryEnumerator
        Return _syncronizedHT.GetEnumerator()
    End Function

    Public ReadOnly Property SyncRoot As Object
        Get
            Return _syncronizedHT.SyncRoot
        End Get
    End Property

    Public ReadOnly Property Values As ICollection
        Get
            Return _syncronizedHT.Values
        End Get
    End Property

    Public ReadOnly Property Keys As ICollection
        Get
            Return _syncronizedHT.Keys
        End Get
    End Property

End Class
