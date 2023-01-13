Imports System.Collections.Generic

Namespace Cache

    Public Class Outlook
        Public Shared Sub clearAll()
            Try
                hsAdressBooks.Clear()
                hsAdressBooksContacts.Clear()
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Public Shared hsAdressBooks As Hashtable = New Hashtable
        Public Shared hsAdressBooksContacts As Hashtable = New Hashtable
    End Class

End Namespace

