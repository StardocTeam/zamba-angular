Imports Microsoft.Office.Interop
Imports System.IO

Public Class frm2007

    'tiene que ser global para el uso de la funcion que resuelve direcciones exchange
    Dim ol As New Outlook.Application()

    Private Sub btnLibreta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLibreta.Click

        Try

            ShowContactsInDialog()

        Catch ex As Exception

            Dim sw As StreamWriter
            sw = File.AppendText(AppDomain.CurrentDomain.BaseDirectory & "errores.txt")

            sw.WriteLine(DateTime.Now.ToString)
            sw.WriteLine("")
            sw.WriteLine("InnerException")
            sw.WriteLine("")
            sw.WriteLine(ex.InnerException)
            sw.WriteLine("")
            sw.WriteLine("Mensaje")
            sw.WriteLine("")
            sw.WriteLine(ex.Message)
            sw.WriteLine("")
            sw.WriteLine("Stack")
            sw.WriteLine("")
            sw.WriteLine(ex.StackTrace)
            sw.WriteLine("--------------------------------------------------------")

            sw.Close()
            sw = Nothing

            MessageBox.Show("Se produjo un error al ejecutar la aplicacion")

        End Try

    End Sub

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        End
    End Sub

    Sub ShowContactsInDialog()

        Dim oDialog As Outlook.SelectNamesDialog
        Dim Address As String = ""

        oDialog = ol.Session.GetSelectNamesDialog

        txtPara.Text = ""
        txtCC.Text = ""
        txtCCO.Text = ""

        With oDialog

            .ForceResolution = True

            Me.Hide()

            If .Display Then
                'Recipients Resolved
                'Access Recipients using oDialog.Recipients
                For Each recip As Outlook.Recipient In oDialog.Recipients

                    'si es nothing es una lista
                    If recip.Address Is Nothing Then

                        'recorrer los miembros de la lista
                        For Each recipLista As Outlook.AddressEntry In recip.AddressEntry.Members
                            Address = GetContactAddress(recipLista)
                            AdressToForm(Address, recip.Type)
                        Next

                    Else
                        Address = GetContactAddress(recip.AddressEntry)
                        AdressToForm(Address, recip.Type)
                    End If

                Next

                'guardar los datos
                GuardarDatos()

            End If

            Me.Show()
            Me.BringToFront()

        End With

    End Sub

    Sub GuardarDatos()
        Dim sw As StreamWriter
        Dim Address As String

        sw = File.AppendText(AppDomain.CurrentDomain.BaseDirectory & "datos.txt")

        sw.WriteLine("Libretas encontradas:")

        'Recorre todas las libretas
        For Each oAL As Outlook.AddressList In ol.Session.AddressLists

            sw.WriteLine(vbTab & oAL.Name.ToString)

            'Recorre los miembros de la libreta
            For Each Entry As Outlook.AddressEntry In oAL.AddressEntries

                Address = GetContactAddress(Entry)

                'Si no tiene dirección es una lista de distribución
                If Address Is Nothing Then

                    'nombre de la lista
                    sw.WriteLine(vbTab & vbTab & Entry.Name & " (lista de outlook):")

                    'miembros de la lista
                    For Each recipLista As Outlook.AddressEntry In Entry.Members
                        Address = GetContactAddress(recipLista)
                        sw.WriteLine(vbTab & vbTab & vbTab & recipLista.Name & ": " & Address)
                    Next

                Else
                    'usuario normal
                    sw.WriteLine(vbTab & vbTab & Entry.Name & ": " & Address)
                End If

            Next
        Next

        sw.WriteLine("")
        sw.WriteLine("Destinatarios seleccionados:")
        sw.WriteLine(vbTab & "Para: " & txtPara.Text)
        sw.WriteLine(vbTab & "CC: " & txtCC.Text)
        sw.WriteLine(vbTab & "CCO: " & txtCCO.Text)
        sw.WriteLine("--------------------------------------------------------------------------")

        sw.Close()

        sw = Nothing
    End Sub

    'Agrega el email al campo que corresponda
    Sub AdressToForm(ByVal Address As String, ByVal Type As String)
        Select Case Type
            Case 1 : txtPara.Text = txtPara.Text & Address.ToString & "; "
            Case 2 : txtCC.Text = txtCC.Text & Address.ToString & "; "
            Case 3 : txtCCO.Text = txtCCO.Text & Address.ToString & "; "
        End Select
    End Sub

    'Devuelve la dirección de email del contacto
    Function GetContactAddress(ByVal Entry As Outlook.AddressEntry)

        Dim Address As String = Entry.Address

        If (Entry.Type.ToString = "EX") Then
            Address = ResolveDisplayNameToSMTP(Entry)
        End If

        GetContactAddress = Address
    End Function

    'Obtiene la dirección SMTP a partir del nombre de usuario en Exchange
    Function ResolveDisplayNameToSMTP(ByVal Entry As Outlook.AddressEntry)
        Dim oEU As Outlook.ExchangeUser
        Dim oEDL As Outlook.ExchangeDistributionList
        Dim oRecip As Outlook.Recipient
        Dim EmailAddress As String = Entry.Address

        oRecip = ol.Session.CreateRecipient(Entry.Name)
        oRecip.Resolve()

        If oRecip.Resolved Then
            Select Case oRecip.AddressEntry.AddressEntryUserType
                Case Outlook.OlAddressEntryUserType.olExchangeUserAddressEntry
                    oEU = oRecip.AddressEntry.GetExchangeUser
                    If Not (oEU Is Nothing) Then
                        EmailAddress = oEU.PrimarySmtpAddress
                    End If
                Case Outlook.OlAddressEntryUserType.olExchangeDistributionListAddressEntry
                    oEDL = oRecip.AddressEntry.GetExchangeDistributionList
                    If Not (oEDL Is Nothing) Then
                        EmailAddress = oEDL.PrimarySmtpAddress
                    End If
            End Select
        End If

        ResolveDisplayNameToSMTP = EmailAddress
    End Function

End Class