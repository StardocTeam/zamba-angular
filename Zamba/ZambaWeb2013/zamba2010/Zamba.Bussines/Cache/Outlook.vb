Imports System.Collections.Generic

Namespace Cache

    Public Class Outlook
        Public Shared hsAdressBooks As Hashtable = New Hashtable
        Public Shared hsAdressBooksContacts As Hashtable = New Hashtable

        Friend Shared Sub RemoveCurrentInstance()
          hsAdressBooks.Clear
            hsAdressBooksContacts.Clear
        End Sub
    End Class

End Namespace

