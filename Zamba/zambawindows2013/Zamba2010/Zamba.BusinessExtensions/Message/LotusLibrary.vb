''' -----------------------------------------------------------------------------
''' Project	 : Zamba.MessagesControls
''' Class	 : Controls.LotusLibrary
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase para trabajar con la libreta de direcciones de Lotus Notes
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class MailLibrary

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un Dataset con los contactos del usuario guardados en Lotus Notes
    ''' </summary>
    ''' <param name="path">direccion del archivo XML que contiene los datos de la libreta de direcciones</param>
    ''' <returns>Dataset</returns>
    ''' <remarks>
    ''' Se accede a un XML generado en el módulo de exportación de mails
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function getAddressBook(ByVal path As String) As DataSet
        If IO.File.Exists(path) Then
            Dim ds As New contacts 'Data.DataSet("contacts")
            Try
                ArreglarXML(path)
                ds.ReadXml(path, XmlReadMode.Fragment)

                ZTrace.WriteLineIf(ZTrace.IsInfo, "AddressBook, Tablas: " & ds.Tables.Count.ToString)
                'No lee las filas, genera una tabla sin filas.

            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
                ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
            End Try
            Return ds
        End If
        Return Nothing
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Funcion que soluciona inconvenientes en el archivo XML
    ''' Cambia caracteres invalidos con el estandar XML
    ''' </summary>
    ''' <param name="path">Ruta del archivo XML que se desea estandarizar</param>
    ''' <remarks>
    ''' En desuso
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Shared Sub ArreglarXML(ByVal path As String)
        Dim file As New IO.StreamReader(path)
        Dim filetemp As New IO.StreamWriter(System.Windows.Forms.Application.StartupPath & "\Temp.xml", False)
        filetemp.WriteLine("<?xml version='1.0' encoding='iso-8859-1' standalone=" & Chr(34) & "yes" & Chr(34) & " ?>")
        Dim str As String
        While file.Peek <> -1
            str = file.ReadLine
            str = str.Replace("å", "")
            str = str.Replace("ç", "")
            str = str.Replace("Ç", "")
            str = str.Replace("º", "")
            str = str.Replace("´", "")
            str = str.Replace("`", "")
            str = str.Replace("Ã", "")
            str = str.Replace("Á", "A")
            str = str.Replace("à", "a")
            str = str.Replace("á", "a")
            str = str.Replace("å", "a")
            str = str.Replace("æ", "")
            str = str.Replace("ñ", "n")
            str = str.Replace("Ñ", "N")
            str = str.Replace("€", "euros")
            str = str.Replace("É", "E")
            str = str.Replace("Ê", "E")
            str = str.Replace("È", "E")
            str = str.Replace("è", "e")
            str = str.Replace("é", "e")
            str = str.Replace("ë", "e")
            str = str.Replace("ê", "e")
            str = str.Replace("Í", "I")
            str = str.Replace("ì", "i")
            str = str.Replace("í", "i")
            str = str.Replace("î", "i")
            str = str.Replace("ï", "i")
            str = str.Replace("ÿ", "y")
            str = str.Replace("Ó", "O")
            str = str.Replace("Ò", "O")
            str = str.Replace("ò", "o")
            str = str.Replace("ó", "o")
            str = str.Replace("ö", "o")
            str = str.Replace("Ü", "U")
            str = str.Replace("Ú", "U")
            str = str.Replace("û", "u")
            str = str.Replace("ù", "u")
            str = str.Replace("ü", "u")
            str = str.Replace("ú", "u")
            str = str.Replace("Ù", "U")
            If str <> ("<?xml version='1.0' encoding='iso-8859-1' standalone=" & Chr(34) & "yes" & Chr(34) & " ?>") Then
                filetemp.WriteLine(str)
            End If
        End While
        file.Close()
        filetemp.Close()
        IO.File.Copy(System.Windows.Forms.Application.StartupPath & "\temp.xml", path, True)
        IO.File.Delete(System.Windows.Forms.Application.StartupPath & "\temp.xml")
    End Sub
End Class
Public Class DestArrayList
    Inherits ArrayList

    Public ReadOnly Property DestTo() As DestArrayList
        Get
            Dim arr As New DestArrayList
            Dim d As Destinatario
            For Each d In Me
                If d.Type = MessageType.MailTo Then
                    arr.Add(d)
                End If
            Next
            Return arr
        End Get
    End Property

    Public ReadOnly Property DestCC() As DestArrayList
        Get
            Dim arr As New DestArrayList
            Dim d As Destinatario
            For Each d In Me
                If d.Type = MessageType.MailCC Then
                    arr.Add(d)
                End If
            Next
            Return arr
        End Get
    End Property
    Public ReadOnly Property DestCCO() As DestArrayList
        Get
            Dim arr As New DestArrayList
            Dim d As Destinatario
            For Each d In Me
                If d.Type = MessageType.MailCCO Then
                    arr.Add(d)
                End If
            Next
            Return arr
        End Get
    End Property


End Class
Public Class LotusDestArrayList
    Inherits DestArrayList

    Public Overloads Sub Add(ByVal des As Destinatario)
        If des.Address = String.Empty Then
            Exit Sub
        End If
        Dim d As Destinatario
        For Each d In Me
            If d.Address = des.Address Then
                Exit Sub
            End If
        Next
        MyBase.Add(des)
    End Sub
    Public Overloads Sub Add(ByVal Direction As String, ByVal mt As MessageType)
        Add(New Destinatario(Direction, mt, String.Empty))
    End Sub
    Public Overloads Sub AddRange(ByVal Destinatarios As LotusDestArrayList)
        Dim d As Destinatario
        For Each d In Destinatarios
            Add(d)
        Next
    End Sub

    Public ReadOnly Property ToStr() As String
        Get
            Try
                'Dim i As Integer
                'Dim arr As ArrayList = Me.DestTo
                Dim AdressBuilder As New System.Text.StringBuilder

                For Each CurrentDestinatario As Destinatario In DestTo
                    AdressBuilder.Append(CurrentDestinatario.Address)
                    AdressBuilder.Append(",")
                Next

                If AdressBuilder.Length > 1 Then
                    AdressBuilder.Remove(AdressBuilder.Length - 1, 1)
                End If


                'For i = 0 To arr.Count - 1
                '    Dim d As Destinatario = arr(i)
                '    strb.Append(d.Address)

                '    If i < arr.Count - 1 Then
                '        strb.Append(";")
                '    End If
                'Next
                Return AdressBuilder.ToString
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
                Return String.Empty
            End Try
        End Get
    End Property
    Public ReadOnly Property CCStr() As String
        Get
            Try
                Dim CCBuilder As New System.Text.StringBuilder

                For Each CurrentDestinatario As Destinatario In DestCC
                    CCBuilder.Append(CurrentDestinatario.Address)
                    CCBuilder.Append(",")
                Next

                If CCBuilder.Length > 1 Then
                    CCBuilder.Remove(CCBuilder.Length - 1, 1)
                End If

                'Dim i As Integer
                'Dim strb As New System.Text.StringBuilder
                'Dim arr As ArrayList = Me.DestCC

                'For i = 0 To arr.Count - 1

                '    Dim d As Destinatario = arr(i)
                '    strb.Append(d.Address)

                '    If i < arr.Count - 1 Then
                '        strb.Append(";")
                '    End If
                'Next
                Return CCBuilder.ToString
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
                Return String.Empty
            End Try
        End Get
    End Property
    Public ReadOnly Property CCOStr() As String
        Get
            Try

                Dim CCOBuilder As New System.Text.StringBuilder

                For Each CurrentDestinatario As Destinatario In DestCCO
                    CCOBuilder.Append(CurrentDestinatario.Address)
                    CCOBuilder.Append(",")
                Next

                If CCOBuilder.Length > 1 Then
                    CCOBuilder.Remove(CCOBuilder.Length - 1, 1)
                End If

                'Dim i As Integer
                'Dim strb As New System.Text.StringBuilder
                'Dim arr As ArrayList = Me.DestCCO

                'For i = 0 To arr.Count - 1

                '    Dim d As Destinatario = arr(i)
                '    strb.Append(d.Address)

                '    If i < arr.Count - 1 Then
                '        strb.Append(";")
                '    End If
                'Next
                Return CCOBuilder.ToString
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
                Return String.Empty
            End Try
        End Get
    End Property
End Class
Public Class LotusTools

    Public Shared Function GetAddressBookLocal() As ArrayList
        Dim AddressBook As New ArrayList
        Try
            'cargo la libreta local
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Voy a traer la libreta local")
            AddressBook.AddRange(ILM.LotusLibrary.LotusLibrary.GetAddressBook(ILM.LotusLibrary.LotusLibrary.TAdressBook.local))
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Listo libreta local")
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al traer la libreta local " & ex.ToString)
        End Try
        Return AddressBook
    End Function
    Public Shared Function GetAddressBookStaff() As ArrayList
        Dim AddressBook As New ArrayList
        Try
            'cargo la libreta de argentina
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Voy a traer la libreta Staff")
            AddressBook.AddRange(ILM.LotusLibrary.LotusLibrary.GetAddressBookStaff)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Listo la libreta Staff")
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al traer la libreta Staff " & ex.ToString)
        End Try
        Return AddressBook
    End Function
    Public Shared Function GetAddressBookGlobal() As ArrayList
        Dim AddressBook As New ArrayList
        Try
            'cargo la libreta de argentina
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Voy a traer la libreta Global")
            AddressBook.AddRange(ILM.LotusLibrary.LotusLibrary.GetAddressBookARSearchGlobal)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Listo la libreta Global")
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al traer la libreta Global " & ex.ToString)
        End Try
        Return AddressBook
    End Function

    ''' <summary>
    ''' Envia mail por medio de Lotus Notes
    ''' </summary>
    ''' <param name="sBody"></param>
    ''' <param name="sTo"></param>
    ''' <param name="sCC"></param>
    ''' <param name="sCCo"></param>
    ''' <param name="sSubject"></param>
    ''' <param name="Attachs"></param>
    ''' <param name="SaveOnSend"></param>
    ''' <param name="ReplyTo"></param>
    ''' <param name="ReturnReceipt"></param>
    ''' <param name="basemail"></param>
    ''' <param name="ArrayLinks"></param>
    ''' <history>
    '''     Javier  Modified    17/11/2010  Se agrega parámetro basemail para 
    ''' </history>
    Public Shared Sub SendMail(ByVal sBody As String, ByVal sTo As String, ByVal sCC As String, ByVal sCCo As String, ByVal sSubject As String, ByVal Attachs As ArrayList, ByVal SaveOnSend As Boolean, ByVal ReplyTo As String, ByVal ReturnReceipt As Boolean, ByVal basemail As String, Optional ByVal ArrayLinks As ArrayList = Nothing)
        ILM.LotusLibrary.LotusLibrary.EnviarMail(sBody, sTo, sCC, sCCo, sSubject, Attachs, SaveOnSend, ReplyTo, ReturnReceipt, ArrayLinks, basemail)
    End Sub
End Class