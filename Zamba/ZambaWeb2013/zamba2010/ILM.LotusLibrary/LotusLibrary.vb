Imports System.Text
Imports Zamba
Public Class LotusLibrary

#Region "CommonFunctions"
    Public Shared Function InitializeSession() As Object
        '**Inicializa el Lotus
        Dim s As Object
        Try

            s = CreateObject("Notes.NotesSession")  'Acá dispara mensaje "no se encuentra el archivo"
            Trace.WriteLine("Inicializa el objeto S")
            Return s
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Throw New ArgumentException(ex.Message)
            'Finally
            '    s = Nothing
        End Try

    End Function

    Public Shared Function EnviromentServer(ByVal s As Object) As String
        '**Obtiene el Server del Notes.ini que usa esta sesion.
        Try
            Dim Server As String
            Server = s.GetEnvironmentString("MailServer", True)
            Trace.WriteLine("Obteniendo Server: " & Server)
            Return Server
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Throw New ArgumentException(ex.Message)
        End Try

    End Function

    Public Shared Function EnviromentLocation(ByVal s) As String
        '**Obtiene la Location del Notes.ini que usa esta sesion.
        Try
            Dim Location As String = s.GetEnvironmentString("Location", True).ToString
            Trace.WriteLine("Obteniendo la Location del Usuario: " & Location)
            Return Location
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Throw New ArgumentException(ex.Message)
        End Try
        Return String.Empty
    End Function

    Public Shared Function EnviromentNames(ByVal s As Object) As String
        '**Obtiene el nombre de la base names desde el Notes.ini que usa esta sesion
        Try
            Dim NAMES As String = s.GetEnvironmentString("NAMES", True)
            '[Ezequiel] 14/04/09 - Names es nullo o esta vacio se forza a devolver un valor por defecto
            '                      ya que en aysa por algun motivo no lo traia.
            If Not String.IsNullOrEmpty(NAMES) Then Return NAMES
            Return "names.nsf"
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Throw New ArgumentException(ex.Message)
        End Try
        Return String.Empty
    End Function

    Public Shared Function EnviromentServerNames(ByVal s As Object, ByVal vecNames As Object) As Object
        '**Obtiene la base de Names del server que figura en el notes.ini
        Dim dbnames As Object
        Try
            'Dim NAMES As String = EnviromentNames(s)
            Dim NAMES As String = vecNames(0).ToString()
            Dim server As String = EnviromentServer(s)
            dbnames = s.getdatabase(server, NAMES)
            Trace.WriteLine("Obteniendo La Base de datos NAMES")
            Return dbnames
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Throw New ArgumentException(ex.Message)
        End Try
        Return Nothing
    End Function

    Public Shared Function EnviromentDirectory(ByVal s As Object) As String
        Try
            Dim directory As String
            directory = s.GetEnvironmentString("Directory", True)
            Return directory
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Throw New ArgumentException(ex.Message)
        End Try
        Return String.Empty
    End Function

    Public Shared Function RunAgent(ByVal DataBAse As Object, ByVal AgentName As String, ByVal NoteId As String) As Boolean
        Dim nAgent As Object
        Try
            nAgent = DataBAse.GetAgent(AgentName)

            If Not nAgent Is Nothing Then
                Try
                    If nAgent.run(NoteId) = 0 Then
                        Trace.WriteLine("Agent run")
                    Else
                        Trace.WriteLine("Agent did not run")
                    End If
                    Return True
                Catch ex As Exception
                    Return False
                End Try
            End If
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Throw New ArgumentException(ex.Message)
            Return False
        Finally
            nAgent = Nothing
        End Try
    End Function

    Public Shared Function MailName(ByVal docnames As Object) As Object
        MailName = docnames.GetItemValue("MailFile")
        Trace.WriteLine("Obteniendo el nombre de la Base de Mail")
        Return MailName
    End Function
#End Region

    Private Const EMBED_ATTACHMENT As Short = 1454

    Private Const ENC_BASE64 = 1727 'Content-Transfer-Encoding is "base64"
    Private Const ENC_EXTENSION = 1731 ' Content-Transfer-Encoding is user-defined
    Private Const ENC_IDENTITY_7BIT = 1728 'Content-Transfer-Encoding is "7bit"
    Private Const ENC_IDENTITY_8BIT = 1729 ' Content-Transfer-Encoding is "8bit"
    Private Const ENC_IDENTITY_BINARY = 1730 ' Content-Transfer-Encoding is "binary"
    Private Const ENC_NONE = 1725 'no Content-Transfer-Encoding header
    Private Const ENC_QUOTED_PRINTABLE = 1726 'Content-Transfer-Encoding is "quoted-printable"

#Region "SendMail"

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
            Do While System.IO.File.Exists(Dest)
                Dest = HOME + (j) + filename
                j = j + 1
            Loop
            '**Copio pero no reescribo si existe el archivo.
            System.IO.File.Copy(path, Dest, False)
            '**Para que se sepa que el path es remoto
            remoto = True
        Else
            remoto = False
            Dest = path
        End If
        '**Finalmente, le doy valor a la funcion.
        ArmaDestinoLocal = Dest
    End Function

    Public Shared Sub EnviarCalendario(ByVal sBody As String, ByVal sRequired As String, ByVal sOptional As String, ByVal sFYI As String, ByVal sSubject As String)
        '**Cuando se ejecuta esta acción se envia un calendario
        Dim Server, User, NAMES, Location, Destination As String
        Dim s As Object
        Dim rtitem, newdoc, nitem As Object
        Dim btn As LotusBtn = New LotusBtn
        Dim dbnames, viewnames, docnames, mailname As Object
        Dim Remoto As Boolean

        Trace.WriteLine("Acá comienza la ejecución")

        '**Inicializa el Lotus obteniendo la sesion
        Trace.WriteLine("Obteniendo la sesion")
        s = ILM.LotusLibrary.LotusLibrary.InitializeSession()

        Try
            If s Is Nothing Then
                Trace.WriteLine("No encontró la sesión y se debe esperar")
            Else
                Trace.WriteLine("Sesion Creada")
            End If
        Catch ex As System.Threading.AbandonedMutexException
        Catch ex As System.Threading.ThreadAbortException
        Catch ex As Threading.ThreadInterruptedException
        Catch ex As Exception
            Trace.WriteLine(ex.ToString())
            Zamba.AppBlock.ZException.Log(ex)
        End Try

        '**Obtiene el Server del Notes.ini que usa esta sesion
        Trace.WriteLine("Obtienendo el Server del Notes.ini que usa esta sesion")
        Server = ILM.LotusLibrary.LotusLibrary.EnviromentServer(s)

        '**Obtiene la Location del Notes.ini que usa esta sesion.
        '**Esto es para poder sacar el usuario actual debido a que
        '**con el s.UserName a veces viene en blanco.

        Trace.WriteLine("Obtienendo la Location del Notes.ini que usa esta sesion.")
        Location = ILM.LotusLibrary.LotusLibrary.EnviromentLocation(s)
        Trace.WriteLine("Location: " & Location)
        Dim vecLocation As String() = Location.Split(Char.Parse(","))

        User = vecLocation(2) '**el UserName esta en la 3º posicion.
        Trace.WriteLine("Obteniendo el User: " & User)

        '**Obtiene el nombre de la base names desde el Notes.ini que usa esta sesion
        NAMES = ILM.LotusLibrary.LotusLibrary.EnviromentNames(s)
        Trace.WriteLine("Obteniendo el NAMES de la base: " & User)
        Dim vecNames As String() = NAMES.Split(Char.Parse(","))

        '**Obtiene la base de Names del server
        Trace.WriteLine("Obteniendo la base de Names del server")
        dbnames = ILM.LotusLibrary.LotusLibrary.EnviromentServerNames(s, vecNames)


        If Not (dbnames.IsOpen) Then
            dbnames.Open(Server, NAMES)
            Trace.WriteLine("La Base fué Abierta")
        Else
            Trace.WriteLine("La Base no fué Abierta")
        End If

        '**La vista de Users tiene a cada usuario con todas las entradas posibles que pueda tener un usuario
        viewnames = dbnames.GetView("($Users)")
        Trace.WriteLine("Obteniendo la Vista")

        docnames = viewnames.GetDocumentByKey(User, True)
        Trace.WriteLine("GetDocumentByKey Ejecutado")

        '**Obtengo el nombre de la base de mail del usuario que inicio la sesion pero sin la extension
        Trace.WriteLine("Obteniendo el nombre de la base de mail del usuario que inicio la sesion pero sin la extension")
        mailname = ILM.LotusLibrary.LotusLibrary.MailName(docnames)

        '**Obtengo la base de mail del usuario agregando la extension
        Trace.WriteLine("Obteniendo la base de mail del usuario agregando la extensión")
        Dim BaseName As String = mailname(0)

        Trace.WriteLine("Nombre de la Base de datos = " & BaseName)

        If BaseName.ToUpper.IndexOf(".NSF") = -1 Then
            BaseName = BaseName + ".nsf"
            Trace.WriteLine("El nombre de la base no fue renombrado")
        End If

        Trace.WriteLine("Obteniendo la base de Mail")
        Trace.WriteLine("Server:" & Server & " Name: " & BaseName)

        Dim db As Object
        db = s.GETDATABASE(Server, BaseName)
        Dim doc As Object

        Trace.WriteLine("Creando el Documento Appointment para enviar")
        doc = db.CREATEDOCUMENT

        doc.Form = "Appointment"

        Dim j As Int32 = 0

        '**Agrego Destinatarios en el tmprequired, tmpoptional, tmpFYI

        Dim arrayRequired() As String = sRequired.Split(Char.Parse(","))
        Dim arrayOptional() As String = sOptional.Split(Char.Parse(","))
        Dim arrayFYI() As String = sFYI.Split(Char.Parse(","))

        doc.AppendItemValue("tmprequired", arrayRequired)
        Trace.WriteLine("tmprequired: " & arrayRequired.Length.ToString())
        doc.AppendItemValue("tmpoptional", arrayOptional)
        Trace.WriteLine("tmpoptional: " & arrayOptional.Length.ToString())
        doc.AppendItemValue("tmpFYI", arrayFYI)
        Trace.WriteLine("tmpFYI: " & arrayFYI.Length.ToString())

        doc.Subject = sSubject
        Trace.WriteLine("Subject: " & sSubject)

        '**Esto construye la parte del texto del mensaje
        rtitem = doc.CREATERICHTEXTITEM("Body")
        Call rtitem.APPENDTEXT(sBody)
        Call rtitem.ADDNEWLINE(2)
        Call rtitem.Update()

        Trace.WriteLine("Enviando Calendario")

        Try
            Call doc.SEND(False)
            Trace.WriteLine("El Calendario ha sido Enviado")
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Trace.WriteLine(ex.ToString())
        End Try


        s = Nothing
        rtitem = Nothing
        newdoc = Nothing
        nitem = Nothing
        btn = Nothing
        dbnames = Nothing
        viewnames = Nothing
        docnames = Nothing
        mailname = Nothing
        db = Nothing
        doc = Nothing

    End Sub

    Public Shared Sub CreateAppoinment()


        '**Cuando se ejecuta esta acción se envia un calendario
        Dim Server, User, NAMES, Location, Destination As String
        Dim s As Object
        Dim rtitem, newdoc, nitem As Object
        Dim btn As LotusBtn = New LotusBtn
        Dim dbnames, viewnames, docnames, mailname As Object
        Dim Remoto As Boolean

        Trace.WriteLine("Acá comienza la ejecución")

        '**Inicializa el Lotus obteniendo la sesion
        Trace.WriteLine("Obteniendo la sesion")
        s = ILM.LotusLibrary.LotusLibrary.InitializeSession()

        Try
            If s Is Nothing Then
                Trace.WriteLine("No encontró la sesión y se debe esperar")
            Else
                Trace.WriteLine("Sesion Creada")
            End If
        Catch ex As System.Threading.AbandonedMutexException
        Catch ex As System.Threading.ThreadAbortException
        Catch ex As Threading.ThreadInterruptedException
        Catch ex As Exception
            Trace.WriteLine(ex.ToString())
            Zamba.AppBlock.ZException.Log(ex)
        End Try
        Try

            '**Obtiene el Server del Notes.ini que usa esta sesion
            Trace.WriteLine("Obtienendo el Server del Notes.ini que usa esta sesion")
            Server = ILM.LotusLibrary.LotusLibrary.EnviromentServer(s)

            '**Obtiene la Location del Notes.ini que usa esta sesion.
            '**Esto es para poder sacar el usuario actual debido a que
            '**con el s.UserName a veces viene en blanco.

            Trace.WriteLine("Obtienendo la Location del Notes.ini que usa esta sesion.")
            Location = ILM.LotusLibrary.LotusLibrary.EnviromentLocation(s)
            Trace.WriteLine("Location: " & Location)
            Dim vecLocation As String() = Location.Split(Char.Parse(","))

            User = vecLocation(2) '**el UserName esta en la 3º posicion.
            Trace.WriteLine("Obteniendo el User: " & User)

            '**Obtiene el nombre de la base names desde el Notes.ini que usa esta sesion
            NAMES = ILM.LotusLibrary.LotusLibrary.EnviromentNames(s)
            Trace.WriteLine("Obteniendo el NAMES de la base: " & User)
            Dim vecNames As String() = NAMES.Split(Char.Parse(","))

            '**Obtiene la base de Names del server
            Trace.WriteLine("Obteniendo la base de Names del server")
            dbnames = ILM.LotusLibrary.LotusLibrary.EnviromentServerNames(s, vecNames)


            If Not (dbnames.IsOpen) Then
                dbnames.Open(Server, NAMES)
                Trace.WriteLine("La Base fué Abierta")
            Else
                Trace.WriteLine("La Base no fué Abierta")
            End If

            '**La vista de Users tiene a cada usuario con todas las entradas posibles que pueda tener un usuario
            viewnames = dbnames.GetView("($Users)")
            Trace.WriteLine("Obteniendo la Vista")

            docnames = viewnames.GetDocumentByKey(User, True)
            Trace.WriteLine("GetDocumentByKey Ejecutado")

            '**Obtengo el nombre de la base de mail del usuario que inicio la sesion pero sin la extension
            Trace.WriteLine("Obteniendo el nombre de la base de mail del usuario que inicio la sesion pero sin la extension")
            mailname = ILM.LotusLibrary.LotusLibrary.MailName(docnames)

            '**Obtengo la base de mail del usuario agregando la extension
            Trace.WriteLine("Obteniendo la base de mail del usuario agregando la extensión")
            Dim BaseName As String = mailname(0)

            Trace.WriteLine("Nombre de la Base de datos = " & BaseName)

            If BaseName.ToUpper.IndexOf(".NSF") = -1 Then
                BaseName = BaseName + ".nsf"
                Trace.WriteLine("El nombre de la base no fue renombrado")
            End If

            Trace.WriteLine("Obteniendo la base de Mail")
            Trace.WriteLine("Server:" & Server & " Name: " & BaseName)

            Trace.WriteLine("Obteniendo base de datos")
            Dim db As Object
            db = s.GETDATABASE(Server, BaseName)
            Dim doc As Object
            Trace.WriteLine("Creando calendario")
            doc = db.CREATEDOCUMENT
            doc.Form = "Appointment"
            Dim SD, ED As Object

            SD = s.CreateDateTime("02/24/2009 09:30:00 PM")
            ED = s.CreateDateTime("02/24/2009 05:30:00 PM")

            'CaladerDoc = Maildb.GetProfileDocument("CalendarProfile",Session.CommonUserName)

            Dim WatchedItems(5) As String
            WatchedItems(0) = "$S"
            WatchedItems(1) = "$L"
            WatchedItems(2) = "$B"
            WatchedItems(3) = "$R"
            WatchedItems(4) = "$E"

            Dim CSWISLArray(5) As String
            CSWISLArray(0) = "$S:1"
            CSWISLArray(1) = "$L:1"
            CSWISLArray(2) = "$B:1"
            CSWISLArray(3) = "$R:1"
            CSWISLArray(4) = "$E:1"

            Dim excludeArray(2) As String
            excludeArray(0) = "D"
            excludeArray(1) = "S"

            Dim CaladerDoc As Object
            CaladerDoc = db.CreateDocument()
            '/*AppointmentType
            'Appointment = 0
            'Anniversary = 1
            'All Day Event = 2
            'Meeting = 3
            'Reminder = 4
            Trace.WriteLine("Cargando datos")
            CaladerDoc.ReplaceItemValue("$AltPrincipal", "CN=MMM001/OU=SVR/O=Abcd/C=AdE")
            CaladerDoc.ReplaceItemValue("$BorderColor", "D2DCDC")
            CaladerDoc.ReplaceItemValue("$BusyName", User)
            CaladerDoc.ReplaceItemValue("$BusyPriority", "1")
            CaladerDoc.ReplaceItemValue("$CSVersion", "2")
            CaladerDoc.ReplaceItemValue("$CSWISL", CSWISLArray)
            CaladerDoc.ReplaceItemValue("$ExpandGroups", "3")
            CaladerDoc.ReplaceItemValue("$FromPreferredLanguage", "en-US")
            CaladerDoc.ReplaceItemValue("$HFFlags", "1")
            CaladerDoc.ReplaceItemValue("$Mailer", "Lotus Notes Release 6.5.1 January 21, 2004")
            CaladerDoc.ReplaceItemValue("$NoPurge", SD)
            CaladerDoc.ReplaceItemValue("$PublicAccess", "1")
            CaladerDoc.ReplaceItemValue("$SMTPKeepNotesItems", "1")
            CaladerDoc.ReplaceItemValue("$WatchedItems", WatchedItems)
            CaladerDoc.ReplaceItemValue("Alarms", "")
            CaladerDoc.ReplaceItemValue("AltChair", "CN=MMM001/OU=SVR/O=Abcd/C=AdE")
            CaladerDoc.ReplaceItemValue("AltRequiredNames", User)
            CaladerDoc.ReplaceItemValue("AppointmentType", "3")
            CaladerDoc.ReplaceItemValue("Encrypt", "")
            CaladerDoc.ReplaceItemValue("ExcludeFromView", excludeArray)
            CaladerDoc.ReplaceItemValue("Form", "Appointment")
            CaladerDoc.ReplaceItemValue("From", User)
            CaladerDoc.ReplaceItemValue("Logo", "stdNotesLtr0")
            CaladerDoc.ReplaceItemValue("MailOptions", "")
            CaladerDoc.ReplaceItemValue("MeetingType", "1")
            CaladerDoc.ReplaceItemValue("Principal", User)
            CaladerDoc.ReplaceItemValue("Recipients", User)
            CaladerDoc.ReplaceItemValue("RequiredAttendees", User)
            CaladerDoc.ReplaceItemValue("SchedulerSwitcher", "3")
            CaladerDoc.ReplaceItemValue("Sign", "0")
            CaladerDoc.ReplaceItemValue("StorageRequiredNames", "1")
            CaladerDoc.ReplaceItemValue("Subject", "Client Meeting")
            CaladerDoc.ReplaceItemValue("STARTDATETIME", SD)
            CaladerDoc.ReplaceItemValue("EndDateTime", ED)
            CaladerDoc.ReplaceItemValue("CalendarDateTime", SD.LocalTime)
            CaladerDoc.ReplaceItemValue("Chair", User)
            CaladerDoc.ReplaceItemValue("Location", "Work - Location")
            CaladerDoc.ReplaceItemValue("Categories", "Calander Setup")
            CaladerDoc.ReplaceItemValue("Body", "Some points to be discussed.. ")

            CaladerDoc.ReplaceItemValue("_viewIcon", 158)
            CaladerDoc.ReplaceItemValue("OrgTable", "C0")
            CaladerDoc.ReplaceItemValue("WebDateTimeInit", "1")
            CaladerDoc.Save(True, True, False)
            CaladerDoc.ReplaceItemValue("ApptUNID", CaladerDoc.UniversalID)
            Trace.WriteLine("Creacion finalizada")
            CaladerDoc.ComputeWithForm(True, False)
            CaladerDoc.Save(True, True, False)
            s = Nothing
            db = Nothing
            CaladerDoc = Nothing
        Catch ex As Exception
            Trace.WriteLine(ex.ToString())
            Zamba.AppBlock.ZException.Log(ex)
        End Try
    End Sub

    'Public Sub SendNotesAppointment(Username as string, Subject as string,
    'Body as string, AppDate as Date, StartTime as Date, MinsDuration as
    'integer)
    'This public sub will write an appointment to a persons diary
    'You must have write privleges to the calendar of the user you are going to add an appointment for
    'Username is the name of the user's mail database, used to get database
    'Also change the servername to reflect the notes server name

    Public Shared Sub SendNotesAppointment(ByVal Subject As String, ByVal Body As String, ByVal AppDate As Date, ByVal StartTime As Date, ByVal MinsDuration As Integer)
        Try
            Dim Server, User, NAMES, Location, Destination As String
            Dim s As Object
            Dim rtitem, newdoc, nitem As Object
            Dim btn As LotusBtn = New LotusBtn
            Dim dbnames, viewnames, docnames, mailname As Object
            Dim Remoto As Boolean

            Trace.WriteLine("Acá comienza la ejecución")

            '**Inicializa el Lotus obteniendo la sesion
            Trace.WriteLine("Obteniendo la sesion")
            s = ILM.LotusLibrary.LotusLibrary.InitializeSession()

            Try
                If s Is Nothing Then
                    Trace.WriteLine("No encontró la sesión y se debe esperar")
                Else
                    Trace.WriteLine("Sesion Creada")
                End If
            Catch ex As System.Threading.AbandonedMutexException
            Catch ex As System.Threading.ThreadAbortException
            Catch ex As Threading.ThreadInterruptedException
            Catch ex As Exception
                Trace.WriteLine(ex.ToString())
                Zamba.AppBlock.ZException.Log(ex)
            End Try

            '**Obtiene el Server del Notes.ini que usa esta sesion
            Trace.WriteLine("Obtienendo el Server del Notes.ini que usa esta sesion")
            Server = ILM.LotusLibrary.LotusLibrary.EnviromentServer(s)

            '**Obtiene la Location del Notes.ini que usa esta sesion.
            '**Esto es para poder sacar el usuario actual debido a que
            '**con el s.UserName a veces viene en blanco.

            Trace.WriteLine("Obtienendo la Location del Notes.ini que usa esta sesion.")
            Location = ILM.LotusLibrary.LotusLibrary.EnviromentLocation(s)
            Trace.WriteLine("Location: " & Location)
            Dim vecLocation As String() = Location.Split(Char.Parse(","))

            User = vecLocation(2) '**el UserName esta en la 3º posicion.
            Trace.WriteLine("Obteniendo el User: " & User)

            '**Obtiene el nombre de la base names desde el Notes.ini que usa esta sesion
            NAMES = ILM.LotusLibrary.LotusLibrary.EnviromentNames(s)
            Trace.WriteLine("Obteniendo el NAMES de la base: " & User)
            Dim vecNames As String() = NAMES.Split(Char.Parse(","))

            '**Obtiene la base de Names del server
            Trace.WriteLine("Obteniendo la base de Names del server")
            dbnames = ILM.LotusLibrary.LotusLibrary.EnviromentServerNames(s, vecNames)


            If Not (dbnames.IsOpen) Then
                dbnames.Open(Server, NAMES)
                Trace.WriteLine("La Base fué Abierta")
            Else
                Trace.WriteLine("La Base no fué Abierta")
            End If

            '**La vista de Users tiene a cada usuario con todas las entradas posibles que pueda tener un usuario
            viewnames = dbnames.GetView("($Users)")
            Trace.WriteLine("Obteniendo la Vista")

            docnames = viewnames.GetDocumentByKey(User, True)
            Trace.WriteLine("GetDocumentByKey Ejecutado")

            '**Obtengo el nombre de la base de mail del usuario que inicio la sesion pero sin la extension
            Trace.WriteLine("Obteniendo el nombre de la base de mail del usuario que inicio la sesion pero sin la extension")
            mailname = ILM.LotusLibrary.LotusLibrary.MailName(docnames)

            '**Obtengo la base de mail del usuario agregando la extension
            Trace.WriteLine("Obteniendo la base de mail del usuario agregando la extensión")
            Dim BaseName As String = mailname(0)

            Trace.WriteLine("Nombre de la Base de datos = " & BaseName)

            If BaseName.ToUpper.IndexOf(".NSF") = -1 Then
                BaseName = BaseName + ".nsf"
                Trace.WriteLine("El nombre de la base no fue renombrado")
            End If

            Trace.WriteLine("Obteniendo la base de Mail")
            Trace.WriteLine("Server:" & Server & " Name: " & BaseName)

            Trace.WriteLine("Obteniendo base de datos")
            Dim db As Object
            db = s.GETDATABASE(Server, BaseName)

            If db Is Nothing Then
                Trace.WriteLine("No se obtuvo la base de datos")
            Else
                Trace.WriteLine("Base de datos obtenida")
            End If

            'Set up the objects required for Automation into lotus notes
            ' Dim MailDbName As String 'The persons notes mail database name
            Dim strSTime As String
            Dim strETime As String
            Dim CalenDoc As Object 'The calendar entry itself
            Dim WorkSpace As Object
            Dim ErrCnt As Integer
            Trace.WriteLine("Creando calendario")
            WorkSpace = CreateObject("Notes.NOTESUIWORKSPACE")

            strSTime = CStr(FormatDateTime(StartTime, vbShortTime))
            strETime = CStr(FormatDateTime(DateAdd("n", MinsDuration, StartTime), vbShortTime))

            'MAKE SURE TO SET SERVER NAME BELOW
            CalenDoc = WorkSpace.COMPOSEDOCUMENT(Server, db, "Appointment")

            CalenDoc.FIELDSETTEXT("AppointmentType", "0")
            CalenDoc.Refresh()

            'Each loop is used to write the value to the field until the field is changed to that value
            Trace.WriteLine("Cargando datos en el calendario")
            Do Until (CDate(Right(CalenDoc.fieldgettext("StartDate"), 10)) = CDate(AppDate)) Or ErrCnt = 1000
                CalenDoc.FIELDSETTEXT("StartDate", CStr(FormatDateTime(AppDate, vbShortDate)))
                CalenDoc.Refresh()
                'ErrCnt is used to prevent an endless loop
                ErrCnt = ErrCnt + 1
            Loop
            ErrCnt = 0
            Do Until (CDate(CalenDoc.fieldgettext("StartTime")) = CDate(strSTime)) Or ErrCnt = 1000
                CalenDoc.FIELDSETTEXT("StartTime", strSTime)
                CalenDoc.Refresh()
                ErrCnt = ErrCnt + 1
            Loop
            ErrCnt = 0
            Do Until (CDate(Right(CalenDoc.fieldgettext("EndDate"), 10)) = CDate(AppDate)) Or ErrCnt = 1000
                CalenDoc.FIELDSETTEXT("EndDate", CStr(FormatDateTime(AppDate, vbShortDate)))
                CalenDoc.Refresh()
                ErrCnt = ErrCnt + 1
            Loop
            ErrCnt = 0
            Do Until (CDate(CalenDoc.fieldgettext("EndTime")) = CDate(strETime)) Or ErrCnt = 1000
                CalenDoc.FIELDSETTEXT("EndTime", strETime)
                CalenDoc.Refresh()
                ErrCnt = ErrCnt + 1
            Loop
            CalenDoc.FIELDSETTEXT("Subject", Subject)
            CalenDoc.FIELDSETTEXT("Body", Body)
            CalenDoc.Refresh()
            Trace.WriteLine("Creacion finalizada")
            CalenDoc.Save()
            CalenDoc.Close()
            CalenDoc = Nothing
            WorkSpace = Nothing
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
    End Sub


    'Public Shared Sub EnviarMail(ByVal sBody As String, ByVal sTo As String, ByVal sCC As String, ByVal sCCo As String, ByVal sSubject As String, ByVal Attachs As ArrayList, ByVal SaveOnSend As Boolean, ByVal ReplyTo As String, ByVal ReturnReceipt As Boolean, ByVal ArrayLinks As ArrayList, ByVal basemail As String)

    '    '**Cuando se ejecuta esta acción se exporta el mail y queda en la carpeta SENT
    '    Dim Server, User, NAMES, Location, Destination As String  ',database
    '    Dim s As Object
    '    Dim rtitem, newdoc, nitem As Object
    '    Dim btn As LotusBtn = New LotusBtn
    '    Dim dbnames, viewnames, docnames, mailname As Object
    '    Dim Remoto As Boolean

    '    Trace.WriteLine("Acá comienza la ejecución")

    '    '**Inicializa el Lotus obteniendo la sesion
    '    Trace.WriteLine("Obteniendo la sesion")
    '    s = ILM.LotusLibrary.LotusLibrary.InitializeSession()

    '    Try
    '        If s Is Nothing Then
    '            Trace.WriteLine("No encontró la sesión y se debe esperar")
    '        Else
    '            Trace.WriteLine("Sesion Creada")
    '        End If
    '    Catch ex As System.Threading.AbandonedMutexException
    '    Catch ex As System.Threading.ThreadAbortException
    '    Catch ex As Threading.ThreadInterruptedException
    '    Catch ex As Exception
    '        Trace.WriteLine(ex.ToString())
    '        Zamba.AppBlock.ZException.Log(ex)
    '    End Try

    '    '**Obtiene el Server del Notes.ini que usa esta sesion
    '    Trace.WriteLine("Obtienendo el Server del Notes.ini que usa esta sesion")
    '    Server = ILM.LotusLibrary.LotusLibrary.EnviromentServer(s)

    '    '**Obtiene la Location del Notes.ini que usa esta sesion.
    '    '**Esto es para poder sacar el usuario actual debido a que
    '    '**con el s.UserName a veces viene en blanco.

    '    Trace.WriteLine("Obtienendo la Location del Notes.ini que usa esta sesion.")
    '    Location = ILM.LotusLibrary.LotusLibrary.EnviromentLocation(s)
    '    Trace.WriteLine("Location: " & Location)
    '    Dim vecLocation As String() = Location.Split(Char.Parse(","))

    '    User = vecLocation(2) '**el UserName esta en la 3º posicion.
    '    Trace.WriteLine("Obteniendo el User: " & User)

    '    '**Obtiene el nombre de la base names desde el Notes.ini que usa esta sesion
    '    NAMES = ILM.LotusLibrary.LotusLibrary.EnviromentNames(s)
    '    Trace.WriteLine("Obteniendo el NAMES de la base: " & User)
    '    Dim vecNames As String() = NAMES.Split(Char.Parse(","))

    '    '**Obtiene la base de Names del server
    '    Trace.WriteLine("Obteniendo la base de Names del server")
    '    dbnames = ILM.LotusLibrary.LotusLibrary.EnviromentServerNames(s, vecNames)


    '    If Not (dbnames.IsOpen) Then
    '        dbnames.Open(Server, NAMES)
    '        Trace.WriteLine("La Base fué Abierta")
    '    Else
    '        Trace.WriteLine("La Base no fué Abierta")
    '    End If

    '    '**La vista de Users tiene a cada usuario con todas las entradas posibles que pueda tener un usuario
    '    viewnames = dbnames.GetView("($Users)")
    '    Trace.WriteLine("Obteniendo la Vista")

    '    docnames = viewnames.GetDocumentByKey(User, True)
    '    Trace.WriteLine("GetDocumentByKey Ejecutado")

    '    Dim BaseName As String

    '    If Not String.IsNullOrEmpty(Server) Then
    '        '**Obtengo el nombre de la base de mail del usuario que inicio la sesion pero sin la extension
    '        Trace.WriteLine("Obteniendo el nombre de la base de mail del usuario que inicio la sesion pero sin la extension")
    '        mailname = ILM.LotusLibrary.LotusLibrary.MailName(docnames)

    '        '**Obtengo la base de mail del usuario agregando la extension
    '        Trace.WriteLine("Obteniendo la base de mail del usuario agregando la extension")
    '        BaseName = mailname(0)

    '        Trace.WriteLine("Nombre de la Base de datos = " & BaseName)

    '        If BaseName.ToUpper.IndexOf(".NSF") = -1 Then
    '            BaseName = BaseName + ".nsf"
    '            Trace.WriteLine("El nombre de la base no fue renombrado")
    '        End If

    '        Trace.WriteLine("Obteniendo la base de Mail")
    '        Trace.WriteLine("Server:" & Server & " Name: " & BaseName)
    '    Else
    '        BaseName = basemail
    '    End If

    '    'Dim aasdd As Domino.NotesDbDirectory
    '    'aasdd.GetFirstDatabase(Domino.DB_TYPES.DATABASE).FilePath
    '    's.GETDBDIRECTORY(Nothing).GETFIRSTDATABASE(1247)
    '    'Dim BaseName As String = "mail\mauromau.nsf"
    '    'BaseName = s.GETDBDIRECTORY(Server).GETFIRSTDATABASE(DbDirectory.DATABASE).

    '    Dim db As Object
    '    db = s.GETDATABASE(Server, BaseName)
    '    Dim doc As Object

    '    Trace.WriteLine("Creando el Documento para enviar")
    '    doc = db.CREATEDOCUMENT

    '    doc.Form = "Memo"
    '    Dim j As Int32 = 0
    '    'Agrego Destinatarios en el SendTo
    '    Trace.WriteLine("Cargando usuarios para el SendTo")

    '    Dim arraySendto() As String = sTo.Split(Char.Parse(","))
    '    Dim arrayCC() As String = sCC.Split(Char.Parse(","))
    '    Dim arrayCCO() As String = sCCo.Split(Char.Parse(","))

    '    doc.AppendItemValue("sendto", arraySendto)
    '    Trace.WriteLine("SendTo: " & arraySendto.Length.ToString())
    '    doc.AppendItemValue("CopyTo", arrayCC)
    '    Trace.WriteLine("CopyTo: " & arrayCC.Length.ToString())
    '    doc.AppendItemValue("BlindCopyTo", arrayCCO)
    '    Trace.WriteLine("BlindCopyTo: " & arrayCCO.Length.ToString())

    '    'doc.CopyTo = sCC
    '    'doc.BlindCopyTo = sCCo
    '    doc.Subject = sSubject
    '    Trace.WriteLine("Subject: " & sSubject)

    '    doc.Importance = 1
    '    doc.DeliveryReport = 1
    '    doc.ReturnReceipt = 1

    '    '**Esto construye la parte del texto del mensaje
    '    Trace.WriteLine("Construyendo la parte del texto del mensaje")
    '    rtitem = doc.CREATERICHTEXTITEM("Body")
    '    Call rtitem.APPENDTEXT(sBody)
    '    Call rtitem.ADDNEWLINE(2)

    '    '**Acá genero el botón con el Link
    '    If Not IsNothing(ArrayLinks) AndAlso ArrayLinks.Count > 0 Then
    '        Trace.WriteLine("Generando el botón con el Link")
    '        Trace.WriteLine("Cantidad de Links: " & ArrayLinks.Count)
    '        newdoc = btn.CreateNewButton(s, db, ArrayLinks)
    '        '**Tomo el campo RichText
    '        nitem = newdoc.GetFirstItem("tmpButtonBody")

    '        rtitem.AppendRTItem(nitem)
    '        Call newdoc.Remove(True)
    '    End If

    '    'Dim rtitem2 As Object
    '    'Try
    '    '    If IncludeButton Then
    '    '        ' Agregado por MNP 4/01/07
    '    '        Trace.WriteLine("Agregando por MNP")
    '    '        newdoc = btn.CreateNewButton(s, db, doc.getitemvalue("slink")(0))
    '    '        nitem = newdoc.GetFirstItem("tmpButtonBody")
    '    '        If (nitem.Type = 1) Then
    '    '            rtitem2 = nitem
    '    '            '**Agrego el botón al campo
    '    '            Call rtitem.AppendRTItem(rtitem2)
    '    '        Else
    '    '            '**Si hubo un problema lo dejo asentado en el body del mail
    '    '            Call rtitem.AppendText("Error creating button.")
    '    '        End If
    '    '        ' Borro el documento importado
    '    '        Call newdoc.Remove(True)
    '    '    End If
    '    'Catch ex As Exception
    '    '    Zamba.AppBlock.ZException.Log(ex)
    '    '    Trace.WriteLine(ex.ToString())
    '    'End Try

    '    ' Update the Rich Text field
    '    Call rtitem.Update()

    '    Trace.WriteLine("Texto del mensaje construido")

    '    '**Se adjuntan los documentos si es que hay attachs.
    '    If Not IsNothing(Attachs) Then
    '        Dim i As Short
    '        'For i = 0 To Attachs.Count - 1
    '        '    If Attachs(i) <> String.Empty Then
    '        '        Call rtitem.EMBEDOBJECT(EMBED_ATTACHMENT, "", Attachs(i))
    '        '    End If
    '        'Next

    '        Trace.WriteLine("Attachs: " & Attachs.Count.ToString())
    '        For i = 0 To Attachs.Count - 1
    '            If Attachs(i) <> String.Empty Then
    '                '**Si es Remoto, armo destino local para poder adjuntar
    '                Destination = ArmaDestinoLocal(s, Attachs(i), Remoto)
    '                Trace.WriteLine("Adjuntando Documento:" & Attachs(i))
    '                Call rtitem.EMBEDOBJECT(EMBED_ATTACHMENT, "", Destination)
    '                Trace.WriteLine("Documento:" & Attachs(i) & " Adjuntado")
    '                '**Borro el archivo temporal luego de haberlo copiado si Remoto = True
    '                If Remoto Then
    '                    System.IO.File.Delete(Destination)
    '                End If
    '            End If
    '        Next
    '    End If

    '    Trace.WriteLine("Documentos adjuntados")
    '    doc.SAVEMESSAGEONSEND = True

    '    'Dim viewsent As Object

    '    'viewsent = db.GetView("($Sent)")
    '    'If Not viewsent Is Nothing Then
    '    '    '**Para asegurar que queden en la carpeta SENT, seteo los
    '    '    '**campos que aparecen en la formula de seleccion de la vista.
    '    '    doc.RemoveItem("DeliveredDate")
    '    '    '**En mi maquina, al setear este campo, me quedo repetido en
    '    '    '**el resultado.
    '    '    doc.PostedDate = DateTime.Now
    '    'End If
    '    ''**Este parametro debe estar en Falso para que no se
    '    ''**guarde el form con el mensaje enviado
    '    Trace.WriteLine("Enviando Mensaje.....")
    '    'Call doc.save(True, False)
    '    Try
    '        Call doc.SEND(False)
    '        Trace.WriteLine("Mensaje Enviado")
    '    Catch ex As Exception
    '        Zamba.AppBlock.ZException.Log(ex)
    '        Trace.WriteLine(ex.ToString())
    '    End Try


    '    s = Nothing
    '    rtitem = Nothing
    '    newdoc = Nothing
    '    nitem = Nothing
    '    btn = Nothing
    '    dbnames = Nothing
    '    viewnames = Nothing
    '    docnames = Nothing
    '    mailname = Nothing
    '    db = Nothing
    '    doc = Nothing
    '    'rtitem2 = Nothing
    'End Sub


    ''' <summary>
    '''     Envia un mail a través de Lotus Mail
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
    ''' <param name="ArrayLinks"></param>
    ''' <param name="basemail"></param>
    ''' <history>
    '''         Created
    '''     Javier  Modified    17/11/2010  Se modifica para permitir el manejo local de una cuenta en lotus notes (ej: pop3), maneja
    '''                                     el mail como HTML
    '''</history>
    Public Shared Sub EnviarMail(ByVal sBody As String, ByVal sTo As String, ByVal sCC As String, ByVal sCCo As String, ByVal sSubject As String, ByVal Attachs As ArrayList, ByVal SaveOnSend As Boolean, ByVal ReplyTo As String, ByVal ReturnReceipt As Boolean, ByVal ArrayLinks As ArrayList, ByVal basemail As String)

        '**Cuando se ejecuta esta acción se exporta el mail y queda en la carpeta SENT
        Dim Server, User, NAMES, Location, Destination As String  ',database
        Dim s As Object
        Dim rtitem, newdoc, nitem As Object
        Dim btn As LotusBtn = New LotusBtn
        Dim dbnames, viewnames, docnames, mailname As Object
        Dim Remoto As Boolean

        Trace.WriteLine("LOTUS NOTES Enviar Mail - Acá comienza la ejecución")

        '**Inicializa el Lotus obteniendo la sesion
        Trace.WriteLine("LOTUS NOTES Enviar Mail - Obteniendo la sesion")
        s = ILM.LotusLibrary.LotusLibrary.InitializeSession()

        Try
            If s Is Nothing Then
                Trace.WriteLine("LOTUS NOTES Enviar Mail - No encontró la sesión y se debe esperar")
            Else
                Trace.WriteLine("LOTUS NOTES Enviar Mail - Sesion Creada")
            End If
        Catch ex As System.Threading.AbandonedMutexException
        Catch ex As System.Threading.ThreadAbortException
        Catch ex As Threading.ThreadInterruptedException
        Catch ex As Exception
            Trace.WriteLine(ex.ToString())
            Zamba.AppBlock.ZException.Log(ex)
        End Try

        '**Obtiene el Server del Notes.ini que usa esta sesion
        Trace.WriteLine("LOTUS NOTES Enviar Mail - Obtienendo el Server del Notes.ini que usa esta sesion")
        Server = ILM.LotusLibrary.LotusLibrary.EnviromentServer(s)

        '**Obtiene la Location del Notes.ini que usa esta sesion.
        '**Esto es para poder sacar el usuario actual debido a que
        '**con el s.UserName a veces viene en blanco.

        Trace.WriteLine("LOTUS NOTES Enviar Mail - Obtienendo la Location del Notes.ini que usa esta sesion.")
        Location = ILM.LotusLibrary.LotusLibrary.EnviromentLocation(s)
        Trace.WriteLine("LOTUS NOTES Enviar Mail - Location: " & Location)
        Dim vecLocation As String() = Location.Split(Char.Parse(","))

        User = vecLocation(2) '**el UserName esta en la 3º posicion.
        Trace.WriteLine("LOTUS NOTES Enviar Mail - Obteniendo el User: " & User)

        '**Obtiene el nombre de la base names desde el Notes.ini que usa esta sesion
        NAMES = ILM.LotusLibrary.LotusLibrary.EnviromentNames(s)
        Trace.WriteLine("LOTUS NOTES Enviar Mail - Obteniendo el NAMES de la base: " & User)
        Dim vecNames As String() = NAMES.Split(Char.Parse(","))

        '**Obtiene la base de Names del server
        Trace.WriteLine("LOTUS NOTES Enviar Mail - Obteniendo la base de Names del server")
        dbnames = ILM.LotusLibrary.LotusLibrary.EnviromentServerNames(s, vecNames)


        If Not (dbnames.IsOpen) Then
            dbnames.Open(Server, NAMES)
            Trace.WriteLine("LOTUS NOTES Enviar Mail - La Base fué Abierta")
        Else
            Trace.WriteLine("LOTUS NOTES Enviar Mail - La Base no fué Abierta")
        End If

        '**La vista de Users tiene a cada usuario con todas las entradas posibles que pueda tener un usuario
        viewnames = dbnames.GetView("($Users)")
        Trace.WriteLine("LOTUS NOTES Enviar Mail - Obteniendo la Vista")

        docnames = viewnames.GetDocumentByKey(User, True)
        Trace.WriteLine("LOTUS NOTES Enviar Mail - GetDocumentByKey Ejecutado")

        Dim BaseName As String

        If Not String.IsNullOrEmpty(Server) Then
            '**Obtengo el nombre de la base de mail del usuario que inicio la sesion pero sin la extension
            Trace.WriteLine("LOTUS NOTES Enviar Mail - Obteniendo el nombre de la base de mail del usuario que inicio la sesion pero sin la extension")
            mailname = ILM.LotusLibrary.LotusLibrary.MailName(docnames)

            '**Obtengo la base de mail del usuario agregando la extension
            Trace.WriteLine("LOTUS NOTES Enviar Mail - Obteniendo la base de mail del usuario agregando la extension")
            BaseName = mailname(0)

            Trace.WriteLine("LOTUS NOTES Enviar Mail - Nombre de la Base de datos = " & BaseName)

            If BaseName.ToUpper.IndexOf(".NSF") = -1 Then
                BaseName = BaseName + ".nsf"
                Trace.WriteLine("LOTUS NOTES Enviar Mail - El nombre de la base no fue renombrado")
            End If

            Trace.WriteLine("LOTUS NOTES Enviar Mail - Obteniendo la base de Mail")
            Trace.WriteLine("LOTUS NOTES Enviar Mail - Server:" & Server & " Name: " & BaseName)
        Else
            Trace.WriteLine("LOTUS NOTES Enviar Mail - Server Nulo - Trabajo local")
            BaseName = basemail
        End If


        Dim db As Object
        db = s.GETDATABASE(Server, BaseName)
        Dim doc As Object

        s.ConvertMIME = False

        Trace.WriteLine("LOTUS NOTES Enviar Mail - Creando el Documento para enviar")
        doc = db.CREATEDOCUMENT

        doc.Form = "Memo"
        Dim j As Int32 = 0
        'Agrego Destinatarios en el SendTo
        Trace.WriteLine("LOTUS NOTES Enviar Mail - Cargando usuarios para el SendTo")

        Dim arraySendto() As String = sTo.Split(Char.Parse(","))
        Dim arrayCC() As String = sCC.Split(Char.Parse(","))
        Dim arrayCCO() As String = sCCo.Split(Char.Parse(","))

        doc.AppendItemValue("sendto", arraySendto)
        Trace.WriteLine("SendTo: " & arraySendto.Length.ToString())
        doc.AppendItemValue("CopyTo", arrayCC)
        Trace.WriteLine("CopyTo: " & arrayCC.Length.ToString())
        doc.AppendItemValue("BlindCopyTo", arrayCCO)
        Trace.WriteLine("BlindCopyTo: " & arrayCCO.Length.ToString())

        'doc.CopyTo = sCC
        'doc.BlindCopyTo = sCCo
        doc.Subject = sSubject
        Trace.WriteLine("Subject: " & sSubject)

        doc.Importance = 1
        doc.DeliveryReport = 1
        doc.ReturnReceipt = 1

        '**Esto construye la parte del texto del mensaje
        Trace.WriteLine("LOTUS NOTES Enviar Mail - Construyendo la parte del texto del mensaje")

        Dim body As Object
        Dim bodyChild As Object
        Dim stream As Object

        stream = s.CreateStream
        body = doc.CreateMIMEEntity
        bodyChild = body.CreateChildEntity()

        'Agregar el body del mail
        Call stream.WriteText(sBody + "<BR>")
        Trace.WriteLine("LOTUS NOTES Enviar Mail - Se agrego body")

        'Agregar los links
        Call stream.WriteText(ObtenerLinksHtml(ArrayLinks))
        Trace.WriteLine("LOTUS NOTES Enviar Mail - Se agrega/n links")

        Call bodyChild.SetContentFromText(stream, "text/html;charset=iso-8859-1", ENC_NONE)
        Call stream.Close()
        Call stream.Truncate()

        'Agregar los attachs
        If Not IsNothing(Attachs) Then
            Dim i As Short
            Trace.WriteLine("Attachs: " & Attachs.Count.ToString())
            For i = 0 To Attachs.Count - 1
                If Attachs(i) <> String.Empty Then
                    '**Si es Remoto, armo destino local para poder adjuntar
                    Destination = ArmaDestinoLocal(s, Attachs(i), Remoto)
                    Dim pos As Int32
                    pos = InStrRev(Attachs(i), "\")
                    Dim Filename As String = Right(Attachs(i), Len(Attachs(i)) - pos)

                    Trace.WriteLine("LOTUS NOTES Enviar Mail - Adjuntando Documento:" & Filename)
                    Dim header As Object
                    bodyChild = body.CreateChildEntity()
                    header = bodyChild.CreateHeader("Content-Type")
                    Call header.SetHeaderVal("multipart/mixed")



                    header = bodyChild.CreateHeader("Content-Disposition")
                    'Call header.SetHeaderVal("attachment; filename=" & Filename)
                    '(pablo) 22-03-2011
                    header.SetHeaderVal("attachment; filename=" & Chr(34) & Filename & Chr(34))


                    header = bodyChild.CreateHeader("Content-ID")
                    Call header.SetHeaderVal(Attachs(i))

                    stream = s.CreateStream()
                    If Not stream.Open(Destination, "binary") Then
                        Trace.WriteLine("LOTUS NOTES Enviar Mail - Error al abrir el adjunto")
                    End If
                    If stream.Bytes = 0 Then
                        Trace.WriteLine("LOTUS NOTES Enviar Mail - Error, el documento a adjuntar no tiene contenido")
                    End If

                    Call bodyChild.SetContentFromBytes(stream, getMIMEType(Filename.Trim().Substring(Filename.Trim().Length() - 4)), ENC_IDENTITY_BINARY) ' All my attachments are excel this would need changing depensding on your attachments.

                    Call stream.Close()
                    Call stream.Truncate()

                    If Remoto Then
                        System.IO.File.Delete(Destination)
                    End If
                End If
            Next
        End If

        s.ConvertMIME = True
        Trace.WriteLine("LOTUS NOTES Enviar Mail - Documentos Adjuntados")
        doc.SAVEMESSAGEONSEND = True


        s.ConvertMIME = True ' Restore conversion


        Trace.WriteLine("Enviando Mensaje.....")

        Try
            Call doc.SEND(False)
            Trace.WriteLine("Mensaje Enviado")
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Trace.WriteLine(ex.ToString())
        End Try


        s = Nothing
        rtitem = Nothing
        newdoc = Nothing
        nitem = Nothing
        btn = Nothing
        dbnames = Nothing
        viewnames = Nothing
        docnames = Nothing
        mailname = Nothing
        db = Nothing
        doc = Nothing
        'rtitem2 = Nothing
    End Sub

    ''' <summary>
    ''' Genera el HTML de los links a partir del array de links
    ''' </summary>
    ''' <param name="arrayLinks"></param>
    ''' <returns></returns>
    ''' <history>
    '''     Javier  Created    17/11/2010  
    '''     Javier  Modified   30/11/2010   Se corrige el link generado a Zamba
    '''</history>
    Private Shared Function ObtenerLinksHtml(ByVal arrayLinks As ArrayList) As String

        Dim newdoc As Object
        Dim stream As Object
        Dim dmp As Object
        Dim btn As StringBuilder = New StringBuilder()
        Try
            For Each link As String In arrayLinks
                If link.ToLower.Contains("zamba") = True Then
                    Trace.WriteLine("Link: " & link)
                    'If link.ToLower.StartsWith("Zamba:\\\\") = False Then
                    '    link = link.Replace("\", "\\")
                    '    Trace.WriteLine("Link Modificado: " & link)
                    'End If
                    link = link.Replace("&", "&amp;")

                    btn.Append("<table width='150px'>")
                    btn.Append("<tr>")
                    btn.Append("<td style='background-color: #DDDDEE' align='CENTER'>")

                    btn.Append("<a href='" + link + "' >")

                    If link.ToLower.Contains("task") Then
                        btn.Append("Ver en Workflow")
                    Else
                        btn.Append("Ver en Zamba")
                    End If

                    btn.Append("</a>")
                    btn.Append("</td>")
                    btn.Append("</tr>")
                    btn.Append("</table><BR>")

                End If
            Next
            btn.Append("<BR/>")
            Return btn.ToString()
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
    End Function

    ''' <summary>
    ''' Retorno el tipo MIME de acuerdo a la extension del archivo
    ''' </summary>
    ''' <param name="extension"></param>
    ''' <returns></returns>
    ''' <history>
    '''     Javier Created  17/11/2010
    '''</history>
    Private Shared Function getMIMEType(ByVal extension As String) As String
        extension = extension.ToLower
        Select Case extension
            Case ".gif"
                Return "image/gif"
            Case ".jpg"
                Return "image/jpeg"
            Case "jpeg"
                Return "image/jpeg"
            Case ".bmp"
                Return "image/bmp"
            Case ".xls"
                Return "application/excel"
            Case ".doc"
                Return "application/msword"
            Case ".dot"
                Return "application/msword"
            Case ".ppt"
                Return "application/mspowerpoint"
            Case ".pps"
                Return "application/mspowerpoint"
            Case ".txt"
                Return "text/plain"
            Case "html"
                Return "text/html"
            Case ".htm"
                Return "text/html"
            Case "htmls"
                Return "text/html"
            Case ".pdf"
                Return "application/pdf"
            Case ".zip"
                Return "application/zip"
            Case Else
                Return ""
        End Select
    End Function


#End Region

#Region "ProgramedAgent"
    Public Shared Sub ExecuteProgramedAgent()

        Dim s As Object
        Dim btn As LotusBtn = New LotusBtn
        Dim nAgent As Object
        Dim db As Object
        Try

            '**Inicializa Lotus  
            s = ILM.LotusLibrary.LotusLibrary.InitializeSession()

            Trace.WriteLine("Obteniendo la base exporta")

            db = s.getdatabase("", "Exporta.nsf")
            nAgent = db.GetAgent("Exporta Mails 5")
            If Not nAgent Is Nothing Then
                Try
                    If nAgent.runOnServer() = 0 Then
                        Trace.WriteLine("Agent ran")
                    Else
                        Trace.WriteLine("Agent did not run")
                    End If
                Catch ex As Exception
                    Zamba.AppBlock.ZException.Log(ex)
                End Try
            End If

        Catch

        Finally
            s = Nothing
            btn = Nothing
            nAgent = Nothing
            db = Nothing
        End Try
    End Sub

#End Region
    Public Enum TAdressBook
        Server = 0
        local = 1
        ServerG = 2
    End Enum
#Region "AddressBook"
    Public Overloads Shared Function GetAddressBook() As ArrayList
        Dim AddressBook As New ArrayList

        Dim s As Object
        Dim viewnames, docnames As Object
        Dim server As String

        '**Inicializa Lotus
        Trace.WriteLine("Inicializando Lotus")
        s = ILM.LotusLibrary.LotusLibrary.InitializeSession()

        Trace.WriteLine("Obteniendo User: " & s.CommonUserName)

        'Agregado por mi para probar tema servidor y libreta de direcciones del servidor
        Trace.WriteLine("Obteniendo Server")
        server = ILM.LotusLibrary.LotusLibrary.EnviromentServer(s)
        Trace.WriteLine("Obteniendo Server: " & server)

        Trace.WriteLine("Obteniendo la base de mail")
        Dim dbNames As Object
        dbNames = s.GETDATABASE(server, "names.nsf")  'Acá estoy levantando el reg de nombres del server
        If Not dbNames Is Nothing Then
            viewnames = dbNames.GetView("($VIMPeople)")
            If Not viewnames Is Nothing Then
                'Recorro la vista y cargo en el vector los valores de las columnas
                Trace.WriteLine("Recorriendo la vista y cargo en el vector los valores de las columnas")
                docnames = viewnames.GetFirstDocument()
                While Not docnames Is Nothing
                    Dim vecReg() As Object
                    vecReg = docnames.columnvalues
                    Trace.WriteLine("Vector: " & vecReg(0).ToString)
                    AddressBook.Add(vecReg(0).ToString)
                    Trace.WriteLine(vecReg(0).ToString)
                    docnames = viewnames.GetNextDocument(docnames)
                End While
            Else
                Trace.WriteLine("No se ha podido obtener la vista")
            End If
        Else
            Trace.WriteLine("No se ha podido obtener la base")
        End If

        Trace.WriteLine("Creando Documento Para enviar")
        Trace.WriteLine("Mensaje Enviado")

        Return AddressBook

    End Function
    ''' <summary>
    ''' Devuelve un listado con los nombres de las libretas de direcciones personalizadas
    ''' </summary>
    ''' <history>(pablo) - Created</history>
    ''' <returns>Dataset</returns>
    ''' <remarks></remarks>
    Public Shared Function GetCustomAddressBookList() As DataSet
        Return ILM.LotusLibrary.DATA.GetCustomAddressBookList()
    End Function

    ''' <summary>
    ''' Devuelve la libreta de direcciones local o del servidor
    ''' </summary>
    ''' <param name="tipo">Libreta local o Libreta del Servidor</param>
    ''' <returns>Arraylist de strings (nombres)</returns>
    ''' <remarks></remarks>
    Public Overloads Shared Function GetAddressBook(ByVal tipo As TAdressBook) As ArrayList
        Dim AddressBook As New ArrayList
        Dim s As Object
        Dim viewnames, docnames As Object
        Dim server As String

        '**Inicializa Lotus
        Trace.WriteLine("Inicializando Lotus")
        s = ILM.LotusLibrary.LotusLibrary.InitializeSession()

        Trace.WriteLine("Obteniendo User: " & s.CommonUserName)

        Trace.WriteLine("Obteniendo Server")
        server = ILM.LotusLibrary.LotusLibrary.EnviromentServer(s)
        Trace.WriteLine("Obteniendo Server: " & server)

        Trace.WriteLine("Obteniendo la base de mail")
        Dim dbNames As Object

        If tipo = TAdressBook.Server Then
            dbNames = s.GETDATABASE(server, "names.nsf")
        Else
            dbNames = s.GETDATABASE("", "names.nsf")
        End If

        If Not dbNames Is Nothing Then
            viewnames = dbNames.GetView("($VIMPeople)")
            If Not viewnames Is Nothing Then
                'Recorro la vista y cargo en el vector los valores de las colummnas
                Trace.WriteLine("Recorriendo la vista y cargo en el vector los valores de las columnas")
                docnames = viewnames.GetFirstDocument()
                While Not docnames Is Nothing
                    Dim vecReg As Object
                    vecReg = docnames.columnvalues
                    Trace.WriteLine("Vector 0: " & vecReg(0).ToString)
                    If tipo = TAdressBook.Server Then
                        If String.Compare(vecReg(0).ToString, String.Empty) <> 0 Then
                            AddressBook.Add(vecReg(0).ToString)
                            Trace.WriteLine(vecReg(0).ToString)
                        Else
                            ' si el nombre viene en blanco se carga la direccion de mail para mostrar
                            Trace.WriteLine("Vector 4: " & vecReg(4).ToString)
                            AddressBook.Add(vecReg(4).ToString)
                            Trace.WriteLine(vecReg(4).ToString)
                        End If
                    Else
                        If String.Compare(vecReg(0).ToString, String.Empty) <> 0 Then
                            AddressBook.Add(vecReg(0).ToString)
                            Trace.WriteLine(vecReg(0).ToString)
                        Else
                            ' si el nombre viene en blanco se carga la direccion de mail para mostrar
                            Trace.WriteLine("Vector 4: " & vecReg(4).ToString)
                            AddressBook.Add(vecReg(4).ToString)
                            Trace.WriteLine(vecReg(4).ToString)
                        End If
                    End If
                    docnames = viewnames.GetNextDocument(docnames)
                End While
            Else
                Trace.WriteLine("No se ha podido obtener la vista")
            End If
        Else
            Trace.WriteLine("No se ha podido obtener la base")
        End If

        Trace.WriteLine("Creando Documento Para enviar")
        Trace.WriteLine("Mensaje Enviado")


        Return AddressBook
    End Function

    'Public Overloads Shared Function GetAddressBookAR() As ArrayList
    ' NO SE USA MAS
    '    Dim AddressBook As New ArrayList
    '    Dim s As Object
    '    Dim viewnames, docnames As Object
    '    Dim server As String

    '    '**Inicializa Lotus
    '    s = ILM.LotusLibrary.LotusLibrary.InitializeSession()

    '    Trace.WriteLine("Obteniendo User: " & s.CommonUserName)
    '    'Agregado por mi para probar tema servidor y libreta de direcciones del servidor
    '    server = ILM.LotusLibrary.LotusLibrary.EnviromentServer(s)
    '    Trace.WriteLine("Obteniendo Server: " & server)

    '    Trace.WriteLine("Obteniendo la base de mail del usuario.")
    '    Dim dbNames As Object

    '    dbNames = s.GETDATABASE(server, "Staff.nsf") 'Acá estoy levantando la Base donde están los contactos AR
    '    viewnames = dbNames.GetView("2. All by Location")

    '    docnames = viewnames.GetFirstDocument()

    '    While Not docnames Is Nothing
    '        Trace.WriteLine("Entro en el while")
    '        Dim vecReg As Object
    '        vecReg = docnames.columnvalues
    '        Trace.WriteLine(vecReg(1).ToString.ToLower)
    '        If vecReg(1).ToString.ToLower.IndexOf("argentina") <> -1 Then
    '            Trace.WriteLine("La direccion es de argentina")
    '            AddressBook.Add(vecReg(5).ToString)
    '            Trace.WriteLine(vecReg(5).ToString)
    '            docnames = viewnames.GetNextDocument(docnames)
    '        Else
    '            Exit While
    '        End If
    '    End While

    '    Trace.WriteLine("Creando Documento Para enviar")
    '    Trace.WriteLine("Mensaje Enviado")

    '    Return AddressBook

    'End Function

    Public Overloads Shared Function GetAddressBookGlobal() As ArrayList
        Dim AddressBook As New ArrayList
        Dim s As Object
        Dim viewnames, docnames As Object
        Dim server As String

        '**Inicializa Lotus
        Trace.WriteLine("Inicializando Lotus")
        s = ILM.LotusLibrary.LotusLibrary.InitializeSession()

        Trace.WriteLine("Obteniendo User: " & s.CommonUserName)

        Trace.WriteLine("Obteniendo Server")
        server = ILM.LotusLibrary.LotusLibrary.EnviromentServer(s)

        Trace.WriteLine("Obteniendo Server: " & server)
        Trace.WriteLine("Obteniendo la base de mail")

        Dim dbNames As Object

        dbNames = s.GETDATABASE(server, "names.nsf")

        If Not dbNames Is Nothing Then
            viewnames = dbNames.GetView("($VIMPeople)")
            If Not viewnames Is Nothing Then
                'Recorro la vista y cargo en el vector los valores de las colummnas
                Trace.WriteLine("Recorriendo la vista y cargo en el vector los valores de las columnas")
                docnames = viewnames.GetFirstDocument()
                While Not docnames Is Nothing
                    Dim vecReg As Object
                    vecReg = docnames.columnvalues
                    Trace.WriteLine("Vector 0: " & vecReg(0).ToString)
                    AddressBook.Add(vecReg(0).ToString)
                    Trace.WriteLine(vecReg(0).ToString)
                    docnames = viewnames.GetNextDocument(docnames)
                End While
            Else
                Trace.WriteLine("No se ha podido obtener la vista")
            End If
        Else
            Trace.WriteLine("No se ha podido obtener la base")
        End If


        Trace.WriteLine("Creando Documento Para enviar")
        Trace.WriteLine("Mensaje Enviado")

        Return AddressBook
    End Function
    Public Overloads Shared Function GetAddressBookCustom(ByVal CustomName As String) As ArrayList
        Dim AddressBook As New ArrayList
        Dim s As Object
        Dim viewnames, docnames As Object
        Dim server As String

        '**Inicializa Lotus
        Trace.WriteLine("Inicializando Lotus")
        s = ILM.LotusLibrary.LotusLibrary.InitializeSession()

        Trace.WriteLine("Obteniendo User: " & s.CommonUserName)

        Trace.WriteLine("Obteniendo Server")
        server = ILM.LotusLibrary.LotusLibrary.EnviromentServer(s)

        Trace.WriteLine("Obteniendo Server: " & server)
        Trace.WriteLine("Obteniendo la base de mail")

        Dim dbNames As Object

        dbNames = s.GETDATABASE(server, CustomName)

        If Not dbNames Is Nothing Then
            viewnames = dbNames.GetView("($VIMPeople)")
            If Not viewnames Is Nothing Then
                'Recorro la vista y cargo en el vector los valores de las colummnas
                Trace.WriteLine("Recorriendo la vista y cargo en el vector los valores de las columnas")
                docnames = viewnames.GetFirstDocument()
                While Not docnames Is Nothing
                    Dim vecReg As Object
                    vecReg = docnames.columnvalues
                    Trace.WriteLine("Vector 0: " & vecReg(0).ToString)
                    AddressBook.Add(vecReg(0).ToString)
                    Trace.WriteLine(vecReg(0).ToString)
                    docnames = viewnames.GetNextDocument(docnames)
                End While
            Else
                Trace.WriteLine("No se ha podido obtener la vista")
            End If
        Else
            Trace.WriteLine("No se ha podido obtener la base")
        End If


        Trace.WriteLine("Creando Documento Para enviar")
        Trace.WriteLine("Mensaje Enviado")

        Return AddressBook
    End Function

    Public Overloads Shared Function GetAddressBookStaff() As ArrayList

        Trace.Listeners.Add(New TextWriterTraceListener(Membership.MembershipHelper.AppTempPath & "\Exceptions\trace ilm" & Now.ToString("dd-MM-yyyy HH-mm") & ".txt"))
        Trace.AutoFlush = True

        Dim AddressList As New ArrayList
        Dim s As Object
        Dim docnames As Object
        Dim server As String

        '**Inicializa Lotus
        Trace.WriteLine("Iniciando Lotus")
        s = ILM.LotusLibrary.LotusLibrary.InitializeSession()

        'Agregado por mi para probar tema servidor y libreta de direcciones del servidor
        Trace.WriteLine("Obteniendo Server")
        server = ILM.LotusLibrary.LotusLibrary.EnviromentServer(s)
        Trace.WriteLine("Obtenido Server: " & server)

        Trace.WriteLine("Obteniendo la base de mail staff")
        Dim dbNames As Object
        dbNames = s.GETDATABASE(server, "Staff.nsf") 'Acá estoy levantando la Base donde están los contactos AR

        Dim selection As String = "Form = " & Chr(34) & "Person" & Chr(34) & " & Address3 = " & Chr(34) & "ARGENTINA" & Chr(34)
        Trace.WriteLine("sELECTION = " & selection)

        Dim ncoll As Object
        Dim dt As New Object
        dt = New Date(1981, 1, 1).ToString("yyyy,MM,dd")
        Dim notesDateTime As Object
        ' La fecha no tiene importancia.
        notesDateTime = s.CreateDateTime("08/18/95 01:36:22 PM")
        ' El parámetro 0 indica que debe devolver todos los documentos que se 
        ' encuentren con la búsqueda
        ncoll = dbNames.Search(selection, notesDateTime, 0)

        Dim x As Integer
        Dim ar1() As Object
        Dim ar2() As Object

        If IsNothing(ncoll) = False AndAlso ncoll.count > 0 Then
            Trace.WriteLine("COUNT DE NCOLL " & ncoll.COUNT)

            For x = 1 To ncoll.count
                Trace.WriteLine("VOY A PEDIR EL DOCNAMES")
                docnames = ncoll.GetNthDocument(x)
                Trace.WriteLine("PEDI EL DOCNAMES")

                Trace.WriteLine("PIDO LA POSICION 1 DEL VECREG")



                Try
                    ar1 = docnames.WebSearchName
                    ar2 = docnames.InternetAddress
                Catch ex As Exception
                    Zamba.AppBlock.ZException.Log(ex)
                    Trace.WriteLine(ex.ToString)
                End Try

                Try
                    Dim Name As String = ar1(0)
                    Dim Mail As String = ar2(0)

                    Trace.WriteLine(Name)
                    Trace.WriteLine("AGREGO LA POSICION 5 DEL VECREG A LA COLECCION")
                    Dim MailWParentesis As String
                    MailWParentesis = "(" & Name & ") " & Mail
                    AddressList.Add(MailWParentesis)
                Catch ex As Exception
                    Zamba.AppBlock.ZException.Log(ex)
                    Trace.WriteLine(ex.ToString)
                End Try
            Next
        Else
            Trace.WriteLine("No se han encntrado datos")
        End If

        Trace.WriteLine("VOY A DEVOLVER ADDRESSBOOK CON " & AddressList.Count)

        Return AddressList
    End Function


    Public Overloads Shared Function GetAddressBookARSearchGlobal() As ArrayList

        Trace.Listeners.Add(New TextWriterTraceListener(Membership.MembershipHelper.AppTempPath & "\Exceptions\trace ilm" & Now.ToString("dd-MM-yyyy HH-mm") & ".txt"))
        Trace.AutoFlush = True

        Dim AddressBook As New ArrayList
        Dim s As Object
        Dim docnames As Object
        Dim server As String

        '**Inicializa Lotus
        Trace.WriteLine("Iniciando Lotus")
        s = ILM.LotusLibrary.LotusLibrary.InitializeSession()

        'Agregado por mi para probar tema servidor y libreta de direcciones del servidor
        Trace.WriteLine("Obteniendo Server")
        server = ILM.LotusLibrary.LotusLibrary.EnviromentServer(s)
        Trace.WriteLine("Obtenido Server: " & server)

        Trace.WriteLine("Obteniendo la base de mail names del Server")
        Dim dbNames As Object
        dbNames = s.GETDATABASE(server, "names.nsf") 'Acá estoy levantando la Base donde están los contactos AR

        'Dim selection As String = "Form = " & Chr(34) & "Person" & Chr(34) & " & Location = " & Chr(34) & "Argentina" & Chr(34)
        'Dim selection As String = "Form = " & Chr(34) & "Person" & Chr(34) & " & @Contains(FullName;" & Chr(34) & "AR" & Chr(34) & ")"
        Dim selection As String = "Form = " & Chr(34) & "Person" & Chr(34)
        Trace.WriteLine("SELECTION = " & selection)

        Dim ncoll As Object
        Dim dt As New Object
        dt = New Date(1981, 1, 1).ToString("yyyy,MM,dd")
        Dim notesDateTime As Object
        ' La fecha no tiene importancia
        notesDateTime = s.CreateDateTime("08/18/95 01:36:22 PM")
        ' El parámetro 0 indica que debe devolver todos los documentos que se 
        ' encuentren con la búsqueda
        ncoll = dbNames.Search(selection, notesDateTime, 0)
        'ncoll = dbNames.AllDocuments

        Dim x As Integer

        If IsNothing(ncoll) = False AndAlso ncoll.count > 0 Then
            Trace.WriteLine("COUNT DE NCOLL  " & ncoll.COUNT)
            docnames = ncoll.GetNthDocument(1)
            Addtoaddressbook(docnames, AddressBook)
            For x = 2 To ncoll.count
                Trace.WriteLine("VOY A PEDIR EL DOCNAMES")
                docnames = ncoll.GetNextDocument(docnames)
                Trace.WriteLine("PEDI EL DOCNAMES")
                Addtoaddressbook(docnames, AddressBook)
            Next
        Else
            Trace.WriteLine("No se han encntrado datos")
        End If
        Trace.WriteLine("VOY A DEVOLVER ADDRESSBOOK CON " & AddressBook.Count)

        Return AddressBook
    End Function

    Private Shared Sub Addtoaddressbook(ByVal DocNames As Object, ByVal AddressBook As ArrayList)
        Dim ar1() As Object
        Try

            ar1 = DocNames.FullName
            Dim Name As String = ar1(1)
            Trace.WriteLine(Name)
            Trace.WriteLine("AGREGO LA POSICION 5 DEL VECREG A LA COLECCION")
            AddressBook.Add(Name)
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Trace.WriteLine(ex.ToString)
        Finally
            ar1 = Nothing
        End Try

    End Sub

#End Region

#Region "InitialData"
    Public Shared Sub ExecuteInitialData()
        ' Me conecto a la base de un usuario y ejecuto el initial data,
        ' considero que de alguna forma Exporta.nsf ya existe
        Dim Server, User, Location As String  ',database
        Dim s, mailname As Object
        Dim Directorio As String
        Dim viewnames, docuser, dbnames, NAMES, docnames As Object

        '**Inicializa Lotus
        s = ILM.LotusLibrary.LotusLibrary.InitializeSession()

        '**Obtiene el Server del Notes.ini que usa esta sesion
        Server = ILM.LotusLibrary.LotusLibrary.EnviromentServer(s)
        Dim vecServer As String() = Server.Split(",")
        Dim sServer As String = vecServer(0)
        Trace.WriteLine("Obteniendo Server: " & vecServer(0))

        '**Obtiene la Location del Notes.ini que usa esta sesion.
        '**Esto es para poder sacar el usuario actual debido a que
        '**con el s.UserName a veces viene en blanco.
        Location = ILM.LotusLibrary.LotusLibrary.EnviromentLocation(s)
        Dim vecLocation As String() = Location.Split(",")

        User = vecLocation(2) '**el UserName esta en la 3º posicion.
        Trace.WriteLine("Obteniendo User: " & User)
        '**Obtiene el nombre de la base names desde el Notes.ini que usa esta sesion
        NAMES = ILM.LotusLibrary.LotusLibrary.EnviromentNames(s)
        Trace.WriteLine("Obteniendo el NAMES de la base: " & User)

        Dim vecNames As String() = NAMES.Split(",")

        '**Obtiene la base de Names
        dbnames = ILM.LotusLibrary.LotusLibrary.EnviromentServerNames(s, vecNames)

        '**La vista de Users tiene a cada usuario con todas las entradas posibles que pueda tener un usuario
        viewnames = dbnames.GetView("($Users)")
        Trace.WriteLine("Obteniendo view")

        docnames = viewnames.GetDocumentByKey(User, True)
        If Not (dbnames.IsOpen) Then
            dbnames.Open(Server, NAMES)
            Trace.WriteLine("Base Abierta")
        End If

        '**La vista de Users tiene a cada usuario con todas las entradas posibles que pueda tener un usuario
        viewnames = dbnames.GetView("($Users)")
        Trace.WriteLine("Obteniendo view")

        docnames = viewnames.GetDocumentByKey(User, True)
        Trace.WriteLine("GetDocumentByKey Ejecutado")

        '**Obtengo el nombre de la base de mail del usuario que inicio la sesion pero sin la extension
        mailname = ILM.LotusLibrary.LotusLibrary.MailName(docnames)

        '**Obtengo la base de mail del usuario agregando la extension
        Dim BaseName As String = mailname(0)
        If BaseName.ToUpper.IndexOf(".NSF") = -1 Then
            BaseName = BaseName + ".nsf"
            Trace.WriteLine("El nombre de la base no fue renombrado")
        End If

        Trace.WriteLine("Nombre de la base= " & BaseName)

        Trace.WriteLine("GetDocumentByKey Ejecutado")

        Directorio = ILM.LotusLibrary.LotusLibrary.EnviromentDirectory(s)

        Trace.WriteLine("Obteniendo User: " & s.CommonUserName)

        Trace.WriteLine("Obteniendo la base de mail")
        Dim dbExporta As Object, nAgent As Object
        dbExporta = s.GETDATABASE("", "Exporta.nsf")
        If Not dbExporta Is Nothing Then
            'viewnames = dbExporta.GetView("(Registro)")
            'If Not viewnames Is Nothing Then
            'docuser = s.currentdatabase.createdocument
            docuser = dbExporta.createdocument
            docuser.replaceitemvalue("form", "ConfigFrm")
            docuser.replaceitemvalue("Conf_NomUserNotes", s.UserName)
            docuser.replaceitemvalue("Conf_BaseMail", BaseName)
            docuser.replaceitemvalue("Conf_MailServer", sServer)
            docuser.save(True, False)

            'docnames = viewnames.GetDocumentByKey(s.commonusername, True)
            nAgent = ILM.LotusLibrary.LotusLibrary.RunAgent(dbExporta, "Initial Data Auto", docuser.noteid)
        End If

        Trace.WriteLine("Creando Documento Para enviar")
        Trace.WriteLine("Mensaje Enviado")


        s = Nothing
        mailname = Nothing
        viewnames = Nothing
        docuser = Nothing
        dbnames = Nothing
        NAMES = Nothing
        docnames = Nothing
        dbExporta = Nothing
        nAgent = Nothing

        Server = String.Empty
        User = String.Empty
        Location = String.Empty
        Directorio = String.Empty

    End Sub

#End Region


End Class
