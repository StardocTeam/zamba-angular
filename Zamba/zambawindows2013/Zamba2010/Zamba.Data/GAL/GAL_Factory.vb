Imports System.Collections.Generic

Public Class GAL_Factory

    ' Retorna todos los contactos guardados en la base
    Public Shared Function GetAllContacts() As DataSet
        Return Server.Con.ExecuteDataset(CommandType.Text, "SELECT * FROM GAL_CONTACTOS ORDER BY FULLNAME ASC")
    End Function

    ' Retorna todos los contactos guardados en la base de la libreta especificada
    Public Shared Function GetAddressBookContacts(ByVal AddressBook As String) As DataSet
        Return Server.Con.ExecuteDataset(CommandType.Text, String.Format("SELECT * FROM GAL_CONTACTOS WHERE ADDRESSBOOK = '{0}' ORDER BY FULLNAME ASC", AddressBook))
    End Function

    ' Retorna una lista de todas las libretas guardadas en la base
    Public Shared Function GetAddressBooks() As List(Of String)
        Dim ds As DataSet
        Dim AddressBooks As New List(Of String)

        ds = Server.Con.ExecuteDataset(CommandType.Text, "SELECT DISTINCT ADDRESSBOOK FROM GAL_CONTACTOS ORDER BY ADDRESSBOOK ASC")

        For Each row As DataRow In ds.Tables(0).Rows
            AddressBooks.Add(row("ADDRESSBOOK"))
        Next

        GetAddressBooks = AddressBooks
    End Function

    ' Retorna una lista de todas las libretas de Outlook que deben ser mostradas
    Public Shared Function GetOutlookAddressBooksToShow() As List(Of String)
        Dim ds As DataSet
        Dim valores() As String
        Dim AddressBooks As New List(Of String)

        AddressBooks = GetAddressBooks()

        ds = Server.Con.ExecuteDataset(CommandType.Text, "SELECT VALUE FROM ZOPT WHERE ITEM = 'OutlookAddressBook'")

        If ds.Tables(0).Rows.Count > 0 Then
            valores = ds.Tables(0).Rows(0)("VALUE").ToString.Split("|")

            For Each valor As String In valores
                AddressBooks.Add(valor)
            Next
        End If

        GetOutlookAddressBooksToShow = AddressBooks
    End Function

End Class
