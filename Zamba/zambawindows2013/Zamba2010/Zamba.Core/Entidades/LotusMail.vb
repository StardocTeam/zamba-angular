Option Strict Off

Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Text

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.MessagesControls
''' Class	 : Controls.LotusMail
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase para crear objetos Mails que seran enviados mediante Lotus Notes
''' Los mails enviados por este medio, quedan en la bandeja de salida de Lotus Notes
''' </summary>
''' <remarks>
''' Implementa IMessage
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>

<ProgId("SendMail_NET.SendMail")> Public Class LotusMail
    Inherits ZClass
    Implements ILotusMail

    'Private _dest As ArrayList

    'Public Shared Function SendMail(ByVal para As String, Byval CC As String, Byval CCO As String, ByVal body As String, ByVal asunto As String, ByVal sLink As String, ByVal sReplyTo As String, ByVal sReturnReceipt As String, Optional ByVal At As ArrayList = Nothing, Optional ByVal Savesend As Boolean = False) As Boolean
    '    Dim msg As LotusMail
    '    msg = New LotusMail
    '    Dim i As Int16
    '    msg.Body = body
    '    msg.Subject = asunto
    '    msg.slink = sLink
    '    msg.ReplyTo = sReplyTo
    '    msg.ReturnReceipt = sReturnReceipt
    '    msg.strTo.AddRange(para.Split(";"))
    '    If CC.Length > 0 Then
    '        msg.strCC.AddRange(CC.Split(";"))
    '    End If
    '    If CCO.Length > 0 Then
    '        msg.strCCO.AddRange(CCO.Split(";"))
    '    End If
    '    msg.SaveOnSend = Savesend
    '    If msg.slink = "" Then
    '        If Not IsNothing(At) Then
    '            msg.Attachs = At
    '        End If
    '    Else
    '        If Not IsNothing(At) Then
    '            msg.Attachs = Nothing 'elimino los adjunto
    '        End If
    '    End If

    '    Try
    '        msg.SEND()
    '    Catch ex As Exception
    '        zamba.core.zclass.raiseerror(ex)
    '        Return False
    '    End Try
    '    Return True
    'End Function
    'Public Shared Function SendMail(ByVal para As String, ByVal body As String, ByVal asunto As String, Optional ByVal At As ArrayList = Nothing, Optional ByVal Savesend As Boolean = False) As Boolean
    '    Dim msg As New LotusMail
    '    Dim i As Int16
    '    ZTrace.WriteLineIf(ZTrace.IsInfo,"body " & body)
    '    msg.Body = body
    '    ZTrace.WriteLineIf(ZTrace.IsInfo,"asunto " & asunto)
    '    msg.Subject = asunto
    '    ZTrace.WriteLineIf(ZTrace.IsInfo,"para " & para)
    '    msg.strTo.Add(para.Split(";"))
    '    ZTrace.WriteLineIf(ZTrace.IsInfo,"savesend " & Savesend)
    '    msg.SaveOnSend = Savesend
    '    If IsNothing(At) Then
    '        ZTrace.WriteLineIf(ZTrace.IsInfo,"at " & At.Count)
    '        msg.Attachs = At
    '        For i = 0 To At.Count - 1
    '            ZTrace.WriteLineIf(ZTrace.IsInfo,"at " & i)
    '            RightFactory.SaveAction(6, IUser.ObjectTypes.ModuleMail, iuser.RightsType.EnviarPorMail, At(i))
    '        Next
    '    End If
    '    Try
    '        ZTrace.WriteLineIf(ZTrace.IsInfo,"send ")
    '        msg.SEND()
    '    Catch ex As Exception
    '        ZTrace.WriteLineIf(ZTrace.IsInfo,ex.ToString)
    '        Return False
    '    End Try
    '    Return True
    'End Function
    Private Const EMBED_ATTACHMENT As Short = 1454

#Region "Miembros Privados"
    Private strTo As New ArrayList
    Private strde As Object
    Private strCC As New ArrayList
    Private strCCO As New ArrayList
    Private _slink As String
    Private _ReplyTo As String


    'Private _to As String = ""
    'Private _cc As String = ""
    'Private _cco As String = ""


    Private strSubject As String = ""
    Private strBody As String = ""

    Private strImportance As String = "2"
    Private strDeliveryReport As String = "B"

    Private strReturnReceipt As Boolean = True

    Private AttachArray As New ArrayList

    Private bSaveOnSend As Boolean = True

#End Region

#Region "Public Property"
    Public Property MailTo() As ArrayList
        Get
            Return strTo
        End Get
        Set(ByVal Value As ArrayList)
            strTo = Value
        End Set
    End Property
    Public Property CC() As ArrayList
        Get
            Return strCC
        End Get
        Set(ByVal Value As ArrayList)
            strCC = Value
        End Set
    End Property
    Public Property CCO() As ArrayList
        Get
            Return strCCO
        End Get
        Set(ByVal Value As ArrayList)
            strCCO = Value
        End Set
    End Property
    Public Property Subject() As Object
        Get
            Return strSubject
        End Get
        Set(ByVal Value As Object)
            strSubject = Value
        End Set
    End Property
    Public Property Body() As Object
        Get
            Return strBody
        End Get
        Set(ByVal Value As Object)
            strBody = Value
        End Set
    End Property
    Public Property Importance() As String Implements ILotusMail.Importance
        Get
            Return strImportance
        End Get
        Set(ByVal Value As String)
            strImportance = Value
        End Set
    End Property
    Public Property DeliveryReport() As String Implements ILotusMail.DeliveryReport
        Get
            Return strDeliveryReport
        End Get
        Set(ByVal Value As String)
            strDeliveryReport = Value
        End Set
    End Property
    Public Property Attachments() As ArrayList Implements ILotusMail.Attachments
        Get
            Return AttachArray
        End Get
        Set(ByVal Value As ArrayList)
            AttachArray = Value
        End Set
    End Property
    Public Property SaveOnSend() As Boolean Implements ILotusMail.SaveOnSend
        Get
            Return bSaveOnSend
        End Get
        Set(ByVal Value As Boolean)
            bSaveOnSend = Value
        End Set
    End Property
    Public Property ReturnReceipt() As Boolean Implements ILotusMail.ReturnReceipt
        Get
            Return strReturnReceipt
        End Get
        Set(ByVal Value As Boolean)
            strReturnReceipt = Value
        End Set
    End Property
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Propiedad para armar el boton "VER EN ZAMBA"
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	14/06/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property slink() As String Implements ILotusMail.slink
        Get
            Return _slink
        End Get
        Set(ByVal Value As String)
            _slink = Value
        End Set
    End Property
    Public Property ReplyTo() As String Implements ILotusMail.ReplyTo
        Get
            Return _ReplyTo
        End Get
        Set(ByVal Value As String)
            _ReplyTo = Value
        End Set
    End Property

#End Region

    Public Enum MailType As Integer
        MailTo = 1
        MailCC = 2
        MailCCO = 3
        Sended = 4
    End Enum

#Region "Implementacion de la interfase"

    Private Shared Function ArmaDestinoLocal(ByVal s As Object, ByVal path As String, ByRef remoto As Boolean) As String
        Dim HOME As String
        Dim pos As Integer
        Dim filename As String
        Dim j As Short = 1
        Dim Dest As String

        '**Chr(92) = "\"
        '**Se obtiene el server path para sacarle el filename.
        filename = path
        '**Si el path es remoto, el 1º pos es 1 pues path=\\Server\...
        pos = InStr(filename, Chr(92))
        If pos <= 1 Then
            '**Obtengo el nombre del archivo sin el path.
            Do While pos <> 0
                filename = Mid(filename, pos + 1)
                pos = InStr(filename, Chr(92))
            Loop
            HOME = s.GetEnvironmentString("Directory", True) + Chr(92)
            Dest = HOME + filename
            '**Si el archivo local temporal llega a existir, se renombra.
            Do While File.Exists(Dest)
                Dest = HOME + (j) + filename
                j = j + 1
            Loop
            '**Copio pero no reescribo si existe el archivo.
            File.Copy(path, Dest, False)
            '**Para que se sepa que el path es remoto
            remoto = True
        Else
            remoto = False
            Dest = path
        End If
        '**Finalmente, le doy valor a la funcion.
        ArmaDestinoLocal = Dest
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo para enviar un mail con LotusNotes.
    ''' </summary>
    ''' <remarks>
    ''' Utiliza los objetos de Lotus, sino esta instalado Lotus, no funciona.
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	15/06/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    'Public Sub SEND()

    '    ''Dim s As Object
    '    'Dim db As Object
    '    'Dim dbnames As Object
    '    'Dim viewnames As Object
    '    'Dim viewSent As Object
    '    'Dim docnames As Object
    '    'Dim doc As Object
    '    'Dim rtItem As Object
    '    'Dim mailname As Object
    '    'Dim Folder As Boolean = False
    '    'Dim Remoto As Boolean
    '    'Dim newdoc As Object
    '    'Dim nitem As Object
    '    'Dim rtItem2 As Object
    '    'Const RICHTEXT As Int16 = 1


    '    'ZTrace.WriteLineIf(ZTrace.IsInfo,"Comenzando el envio del mensaje|")
    '    'ZTrace.WriteLineIf(ZTrace.IsInfo,"Asunto: " & Me.Subject)
    '    'ZTrace.WriteLineIf(ZTrace.IsInfo,"Body: " & Me.Body)
    '    'ZTrace.WriteLineIf(ZTrace.IsInfo,"Importancia: " & Me.Importance)
    '    'ZTrace.WriteLineIf(ZTrace.IsInfo,"DeliveryReport: " & Me.DeliveryReport)

    '    'Try
    '    '    Dim Server, User, NAMES, Location, Destination As String  ',database

    '    '    '**Inicializa Lotus
    '    '    s = CreateObject("Notes.NotesSession")
    '    '    ZTrace.WriteLineIf(ZTrace.IsInfo,"Sesion Creada")

    '    '    '**Obtiene el Server del Notes.ini que usa esta sesion
    '    '    Server = s.GetEnvironmentString("MailServer", True)
    '    '    ZTrace.WriteLineIf(ZTrace.IsInfo,"Obteniendo Server: " & Server)

    '    '    '**Obtiene la Location del Notes.ini que usa esta sesion.
    '    '    '**Esto es para poder sacar el usuario actual debido a que
    '    '    '**con el s.UserName a veces viene en blanco.
    '    '    Location = s.GetEnvironmentString("Location", True)
    '    '    ZTrace.WriteLineIf(ZTrace.IsInfo,"Obteniendo Location: " & Location)

    '    '    Dim vecLocation As String() = Location.Split(",")

    '    '    User = vecLocation(2) '**el UserName esta en la 3º posicion.
    '    '    ZTrace.WriteLineIf(ZTrace.IsInfo,"Obteniendo User: " & User)

    '    '    '**Obtiene el nombre de la base names desde el Notes.ini que usa esta sesion
    '    '    NAMES = s.GetEnvironmentString("NAMES", True)
    '    '    ZTrace.WriteLineIf(ZTrace.IsInfo,"Obteniendo NAMES de la base: " & User)

    '    '    '**Obtiene la base de Names
    '    '    dbnames = s.getdatabase(Server, NAMES)
    '    '    ZTrace.WriteLineIf(ZTrace.IsInfo,"Obteniendo La Base de datos")

    '    '    If Not (dbnames.IsOpen) Then
    '    '        dbnames.Open(Server, NAMES)
    '    '        ZTrace.WriteLineIf(ZTrace.IsInfo,"Base Abierta")
    '    '    End If
    '    '    '**La vista de Users tiene a cada usuario con todas las entradas posibles que pueda tener un usuario
    '    '    viewnames = dbnames.GetView("($Users)")
    '    '    ZTrace.WriteLineIf(ZTrace.IsInfo,"Obteniendo view")

    '    '    docnames = viewnames.GetDocumentByKey(User, True)
    '    '    ZTrace.WriteLineIf(ZTrace.IsInfo,"GetDocumentByKey Ejecutado")

    '    '    '**Obtengo el nombre de la base de mail del usuario que inicio la sesion pero sin la extension
    '    '    mailname = docnames.GetItemValue("MailFile")
    '    '    ZTrace.WriteLineIf(ZTrace.IsInfo,"Obteniendo el nombre de la base  de mail")

    '    '    '**Obtengo la base de mail del usuario agregando la extension
    '    '    Dim BaseName As String = mailname(0)

    '    '    ZTrace.WriteLineIf(ZTrace.IsInfo,"Nombre de la base= " & BaseName)

    '    '    If BaseName.ToUpper.IndexOf(".NSF") = -1 Then
    '    '        BaseName = BaseName + ".nsf"
    '    '        ZTrace.WriteLineIf(ZTrace.IsInfo,"El nombre de la base no fue renombrado")
    '    '    End If

    '    '    ZTrace.WriteLineIf(ZTrace.IsInfo,"Obteniendo la base  de mail")
    '    '    db = s.GETDATABASE(Server, BaseName)

    '    '    ZTrace.WriteLineIf(ZTrace.IsInfo,"Creando Documento Para enviar")
    '    '    doc = db.CREATEDOCUMENT


    '    '    doc.Form = "Memo"
    '    '    Dim j As Int32 = 0
    '    '    'Agrego Destinatarios
    '    '    ZTrace.WriteLineIf(ZTrace.IsInfo,"Cargando usuarios PARA")
    '    '    Try
    '    '        Dim ar(Me.MailTo.Count - 1) As String
    '    '        For j = 0 To Me.MailTo.Count - 1
    '    '            ar(j) = Me.MailTo(j)
    '    '        Next
    '    '        doc.SendTo = ar
    '    '    Catch ex As Exception
    '    '        zamba.core.zclass.raiseerror(ex)
    '    '    End Try
    '    '    Try
    '    '        'Agrego Destinatarios Con Copia
    '    '        ZTrace.WriteLineIf(ZTrace.IsInfo,"Agrego Destinatarios Con Copia")
    '    '        If Me.CC.Count > 0 Then
    '    '            Dim ar(Me.CC.Count - 1) As String
    '    '            For j = 0 To Me.CC.Count - 1
    '    '                ar(j) = Me.CC(j)
    '    '            Next
    '    '            doc.CopyTo = ar
    '    '        End If
    '    '    Catch ex As Exception
    '    '        zamba.core.zclass.raiseerror(ex)
    '    '    End Try
    '    '    Try
    '    '        'Agrego Destinatarios Con Copia Oculta
    '    '        ZTrace.WriteLineIf(ZTrace.IsInfo,"Agrego Destinatarios Con Copia Oculta")
    '    '        If Me.CCO.Count > 0 Then
    '    '            Dim ar(Me.CCO.Count - 1) As String
    '    '            For j = 0 To Me.CCO.Count - 1
    '    '                ar(j) = Me.CCO(j)
    '    '            Next
    '    '            doc.BlindCopyTo = ar
    '    '        End If
    '    '    Catch ex As Exception
    '    '        zamba.core.zclass.raiseerror(ex)
    '    '    End Try
    '    '    doc.Subject = Me.Subject
    '    '    doc.Importance = Me.Importance
    '    '    doc.DeliveryReport = Me.DeliveryReport
    '    '    doc.ReturnReceipt = Me.ReturnReceipt
    '    '    Try
    '    '        doc.slink = Me.slink
    '    '    Catch ex As Exception
    '    '        zamba.core.zclass.raiseerror(ex)
    '    '    End Try

    '    '    Try
    '    '        doc.ReplyTo = "Administrator/Stardoc"
    '    '    Catch ex As Exception
    '    '        zamba.core.zclass.raiseerror(ex)
    '    '    End Try

    '    '    Try
    '    '        '**Esto construye la parte del texto del mensaje
    '    '        rtItem = doc.CREATERICHTEXTITEM("Body")
    '    '        'Set body = New NotesRichTextItem(docA, "Body")
    '    '    Catch ex As Exception
    '    '        zamba.core.zclass.raiseerror(ex)
    '    '    End Try
    '    '    Try
    '    '        Dim btn As LotusBtn = New LotusBtn
    '    '        newdoc = btn.CreateNewButton(s, s.currentdatabase, Me.slink)
    '    '        ' Get the Rich Text field
    '    '        nitem = newdoc.GetFirstItem("tmpButtonBody")
    '    '        Call rtItem.APPENDTEXT(Me.Body)
    '    '        Call rtItem.ADDNEWLINE(2)

    '    '    Catch ex As System.Runtime.InteropServices.COMException
    '    '    Catch ex As System.Runtime.InteropServices.ExternalException
    '    '    Catch ex As System.NullReferenceException
    '    '    Catch ex As Exception
    '    '    End Try
    '    '    Try
    '    '        If (nitem.Type = RICHTEXT) Then
    '    '            rtItem2 = nitem
    '    '            ' Append the newly created button
    '    '            Call rtItem.AppendRTItem(rtItem2)
    '    '        Else
    '    '            ' If there's a problem getting the Rich Text field, print to the memo
    '    '            Call rtItem.AppendText("Error creating button.")
    '    '        End If
    '    '        ' Delete the imported document
    '    '        Call newdoc.Remove(True)
    '    '    Catch ex As Exception
    '    '    End Try
    '    '    ' Update the Rich Text field
    '    '    Call rtItem.Update()
    '    '    'Call rtItem.APPENDTEXT()
    '    '    'Call rtItem.ADDNEWLINE(1)
    '    '    ZTrace.WriteLineIf(ZTrace.IsInfo,"Texto del mensaje construido")

    '    '    '**Se adjuntan los documentos
    '    '    Dim i As Short
    '    '    'For i = 0 To Me.Attachments.Count - 1
    '    '    '    If Me.Attachments(i) <> String.Empty Then
    '    '    '        Call rtItem.EMBEDOBJECT(EMBED_ATTACHMENT, "", Me.Attachments(i))
    '    '    '    End If
    '    '    'Next

    '    '    For i = 0 To Me.Attachments.Count - 1
    '    '        If Me.Attachments(i) <> String.Empty Then
    '    '            '**Si es Remoto, armo destino local para poder adjuntar
    '    '            Destination = ArmaDestinoLocal(s, Me.Attachments(i), Remoto)
    '    '            ZTrace.WriteLineIf(ZTrace.IsInfo,"Adjuntando Documento:" & Me.Attachments(i))
    '    '            Call rtItem.EMBEDOBJECT(EMBED_ATTACHMENT, "", Destination)
    '    '            ZTrace.WriteLineIf(ZTrace.IsInfo,"Documento:" & Me.Attachments(i) & " Adjuntado")
    '    '            '**Borro el archivo temporal luego de haberlo copiado si Remoto = True
    '    '            If Remoto Then
    '    '                System.IO.File.Delete(Destination)
    '    '            End If
    '    '        End If
    '    '    Next

    '    '    ZTrace.WriteLineIf(ZTrace.IsInfo,"Documentos adjuntados")
    '    '    doc.SAVEMESSAGEONSEND = Me.SaveOnSend


    '    '    If Me.SaveOnSend Then
    '    '        viewSent = db.GetView("($Sent)")
    '    '        If Not viewSent Is Nothing Then
    '    '            '**Para asegurar que queden en la carpeta SENT, seteo los
    '    '            '**campos que aparecen en la formula de seleccion de la vista.
    '    '            doc.RemoveItem("DeliveredDate")
    '    '            '**En mi maquina, al setear este campo, me quedo repetido en
    '    '            '**el resultado.
    '    '            doc.PostedDate = DateTime.Now
    '    '        Else
    '    '            Folder = True
    '    '        End If
    '    '    End If
    '    '    '**Este parametro debe estar en Falso para que no se
    '    '    '**guarde el form con el mensaje enviado
    '    '    ZTrace.WriteLineIf(ZTrace.IsInfo,"Enviando Mensaje.....")
    '    '    ' Call doc.save(True, False)
    '    '    Call doc.SEND(False)
    '    '    ZTrace.WriteLineIf(ZTrace.IsInfo,"Mensaje Enviado")
    '    '    'Lo pongo aca porque antes del Send, me tira el error 4005.
    '    '    If Folder Then
    '    '        'Si no existiese la carpeta, se crea automaticamente.
    '    '        doc.PutInFolder("Sent")
    '    '    End If

    '    'Catch ComEx As COMException
    '    '    '**Este tipo de excepciones son las que vienen desde Notes.
    '    '    Dim strError As String
    '    '    Select Case ComEx.ErrorCode
    '    '        Case 4063
    '    '            strError = "Una de las bases de datos no se pudo abrir. Revise la configuración del Notes"
    '    '        Case 4294
    '    '            strError = "Fallo en el envío de mails. Revise los datos del remitente."
    '    '        Case 4005
    '    '            strError = "Fallo en la copia del mail en la carpeta Sent. Revise la configuración de la base de mail del usuario."
    '    '        Case 4225
    '    '            strError = ComEx.ToString & " Numero de error: 4225"
    '    '        Case Else
    '    '            strError = ComEx.ToString
    '    '    End Select
    '    '    ZTrace.WriteLineIf(ZTrace.IsInfo,"EXCEPTION:" & strError & "|" & ComEx.ToString)
    '    '    Throw New Exception(strError, ComEx)
    '    'Catch NullEx As NullReferenceException
    '    '    ZTrace.WriteLineIf(ZTrace.IsInfo,"EXCEPTION: Referencia nula. Revise la configuración del usuario.4151")
    '    '    Throw New Exception("Referencia nula. Revise la configuración del usuario.4151", NullEx)

    '    'Catch GlobalEx As Exception
    '    '    If Err.Number = 429 Then
    '    '        Throw New Exception("No se pudo crear el objeto ActiveX. Revise la configuración local.429", GlobalEx)
    '    '    Else
    '    '        Throw New Exception(GlobalEx.ToString - 1, GlobalEx)
    '    '    End If
    '    'Finally
    '    '    '**Liberamos memoria
    '    '    doc = Nothing
    '    '    db = Nothing
    '    '    s = Nothing
    '    '    rtItem = Nothing
    '    '    dbnames = Nothing
    '    '    viewnames = Nothing
    '    '    viewSent = Nothing
    '    '    docnames = Nothing
    '    '    mailname = Nothing
    '    '    nitem = Nothing
    '    '    rtItem2 = Nothing
    '    'End Try
    'End Sub
    Public Sub addAttach(ByVal str As String) Implements ILotusMail.addAttach
        AttachArray.Add(str)
    End Sub
    Public Sub addAttachResult(ByVal Results As ArrayList) Implements ILotusMail.addAttachResult
        Dim r As Result
        For Each r In Results
            AttachArray.Add(r.FullPath)
        Next
    End Sub
    Public Sub addDest(ByVal dest As Object) Implements ILotusMail.addDest
        Dim des As Destinatario = dest
        Select Case des.Type
            Case MailType.MailCC
                CC.Add(des.Address)
            Case MailType.MailCCO
                CCO.Add(des.Address)
            Case MailType.MailTo
                MailTo.Add(des.Address)
        End Select
    End Sub
    Public Sub addDestRange(ByVal des As ArrayList) Implements ILotusMail.addDestRange
        Dim i As Int16
        For i = 0 To des.Count - 1
            addDest(des(i))
        Next
    End Sub
    Public Property Attachs() As ArrayList
        Get
            Return AttachArray
        End Get
        Set(ByVal Value As ArrayList)
            AttachArray = Value
        End Set
    End Property
    Public Property De() As Object
        Get
            Return strde
        End Get
        Set(ByVal Value As Object)
            strde = Value
        End Set
    End Property

#End Region


    Public Property Attachs1() As Object Implements IMailMessage.Attachs
        Get
            Return AttachArray
        End Get
        Set(ByVal Value As Object)
            AttachArray = Value
        End Set
    End Property
    Public Property Body1() As Object Implements IMailMessage.Body
        Get
            Return Body
        End Get
        Set(ByVal Value As Object)
            Body = Value
        End Set
    End Property
    Public Property Confirmation1() As Boolean Implements IMailMessage.Confirmation
        Get
            If ReturnReceipt = "0" Then
                Return False
            Else
                Return True
            End If
        End Get
        Set(ByVal Value As Boolean)
            If Value = True Then
                ReturnReceipt = "1"
            Else
                ReturnReceipt = "0"
            End If
        End Set
    End Property

    Private Sub send() Implements IMailMessage.send
        'Me.SEND()
    End Sub
    Public Property CC1() As Object Implements IMailMessage.CC
        Get
            Return GetString(CC, ";")
        End Get
        Set(ByVal Value As Object)
            Dim s As String

            If Value Is Nothing Then
                s = ""
            Else
                s = Value
            End If

            CC = New ArrayList(s.Split(";"))
        End Set
    End Property
    Public Property CCO2() As Object Implements IMailMessage.CCO
        Get
            Return GetString(CCO, ";")
        End Get
        Set(ByVal Value As Object)
            Dim s As String
            If Value Is Nothing Then
                s = ""
            Else
                s = Value
            End If
            CCO = New ArrayList(s.Split(";"))
        End Set
    End Property
    Public Property MailTo2() As Object Implements IMailMessage.MailTo
        Get
            Return GetString(MailTo, ";")
        End Get
        Set(ByVal Value As Object)
            Dim s As String

            If Value Is Nothing Then
                s = ""
            Else
                s = Value
            End If

            MailTo = New ArrayList(s.Split(";"))
        End Set
    End Property
    Public Property De2() As Object Implements IMailMessage.De
        Get
            Return De
        End Get
        Set(ByVal Value As Object)
            De = Value
        End Set
    End Property
    Public Property Subject2() As Object Implements IMailMessage.Subject
        Get
            Return Subject
        End Get
        Set(ByVal Value As Object)
            Subject = Value
        End Set
    End Property
    Private Shared Function GetString(ByVal data As ArrayList, ByVal Separator As String) As String
        Dim r As New StringBuilder
        For Each s As String In data
            r.Append(s)
            r.Append(Separator)
        Next
        Return r.ToString.Substring(0, r.Length - Separator.Length)
    End Function

    Public Sub New()

    End Sub
    Public Overrides Sub Dispose()
    End Sub
End Class



