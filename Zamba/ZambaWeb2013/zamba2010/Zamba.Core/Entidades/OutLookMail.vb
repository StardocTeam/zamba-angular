Option Strict Off

Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Text

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.MessagesControls
''' Class	 : Controls.OutLookMail
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase para crear objetos Mails que seran enviados mediante OutLook Notes
''' Los mails enviados por este medio, quedan en la bandeja de salida de OutLook Notes
''' </summary>
''' <remarks>
''' Implementa IMessage
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>

<ProgId("SendMail_NET.SendMail")> Public Class OutLookMail
    Inherits ZClass
    Implements IOutLookMail

    'Private _dest As ArrayList

    'Public Shared Function SendMail(ByVal para As String, Byval CC As String, Byval CCO As String, ByVal body As String, ByVal asunto As String, ByVal sLink As String, ByVal sReplyTo As String, ByVal sReturnReceipt As String, Optional ByVal At As ArrayList = Nothing, Optional ByVal Savesend As Boolean = False) As Boolean
    '    Dim msg As OutLookMail
    '    msg = New OutLookMail
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
    '    Dim msg As New OutLookMail
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
    Private strTo As New Generic.List(Of String)
    Private strde As Object
    Private strCC As New generic.List(Of String)
    Private strCCO As New Generic.List(Of String)
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

    Private AttachArray As New Generic.List(Of String)

    Private bSaveOnSend As Boolean = True

#End Region

#Region "Public Property"
    Public Property MailTo() As Generic.List(Of String)
        Get
            Return strTo
        End Get
        Set(ByVal Value As Generic.List(Of String))
            strTo = Value
        End Set
    End Property
    Public Property CC() As Generic.List(Of String)
        Get
            Return strCC
        End Get
        Set(ByVal Value As Generic.List(Of String))
            strCC = Value
        End Set
    End Property
    Public Property CCO() As Generic.List(Of String)
        Get
            Return strCCO
        End Get
        Set(ByVal Value As Generic.List(Of String))
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
    Public Property Importance() As String Implements IOutLookMail.Importance
        Get
            Return strImportance
        End Get
        Set(ByVal Value As String)
            strImportance = Value
        End Set
    End Property
    Public Property DeliveryReport() As String Implements IOutLookMail.DeliveryReport
        Get
            Return strDeliveryReport
        End Get
        Set(ByVal Value As String)
            strDeliveryReport = Value
        End Set
    End Property
    Public Property Attachments() As Generic.List(Of String) Implements IOutLookMail.Attachments
        Get
            Return AttachArray
        End Get
        Set(ByVal Value As Generic.List(Of String))
            AttachArray = Value
        End Set
    End Property
    Public Property SaveOnSend() As Boolean Implements IOutLookMail.SaveOnSend
        Get
            Return bSaveOnSend
        End Get
        Set(ByVal Value As Boolean)
            bSaveOnSend = Value
        End Set
    End Property
    Public Property ReturnReceipt() As Boolean Implements IOutLookMail.ReturnReceipt
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
    Public Property slink() As String Implements IOutLookMail.slink
        Get
            Return _slink
        End Get
        Set(ByVal Value As String)
            _slink = Value
        End Set
    End Property
    Public Property ReplyTo() As String Implements IOutLookMail.ReplyTo
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

    Public Sub addAttach(ByVal str As String) Implements IOutLookMail.addAttach
        Me.AttachArray.Add(str)
    End Sub
    Public Sub addAttachResult(ByVal Results As ArrayList) Implements IOutLookMail.addAttachResult
        Dim r As Result
        For Each r In Results
            'falta ver si el volumen en -2 copiar en temporal para enviar.

            Me.AttachArray.Add(r.FullPath)
        Next
    End Sub
    Public Sub addDest(ByVal dest As Object) Implements IOutLookMail.addDest
        Dim des As Destinatario = dest
        Select Case des.Type
            Case MailType.MailCC
                Me.CC.Add(des.Address)
            Case MailType.MailCCO
                Me.CCO.Add(des.Address)
            Case MailType.MailTo
                Me.MailTo.Add(des.Address)
        End Select
    End Sub
    Public Sub addDestRange(ByVal des As ArrayList) Implements IOutLookMail.addDestRange
        Dim i As Int16
        For i = 0 To des.Count - 1
            Me.addDest(des(i))
        Next
    End Sub
    Public Property Attachs() As Generic.List(Of String)
        Get
            Return Me.AttachArray
        End Get
        Set(ByVal Value As Generic.List(Of String))
            Me.AttachArray = Value
        End Set
    End Property
    Public Property De() As Object
        Get
            Return Me.strde
        End Get
        Set(ByVal Value As Object)
            Me.strde = Value
        End Set
    End Property

#End Region


    Public Property Attachs1() As Object Implements IMailMessage.Attachs
        Get
            Return Me.AttachArray
        End Get
        Set(ByVal Value As Object)
            Me.AttachArray = Value
        End Set
    End Property
    Public Property Body1() As Object Implements IMailMessage.Body
        Get
            Return Me.Body
        End Get
        Set(ByVal Value As Object)
            Me.Body = Value
        End Set
    End Property
    Public Property Confirmation1() As Boolean Implements IMailMessage.Confirmation
        Get
            If Me.ReturnReceipt = "0" Then
                Return False
            Else
                Return True
            End If
        End Get
        Set(ByVal Value As Boolean)
            If Value = True Then
                Me.ReturnReceipt = "1"
            Else
                Me.ReturnReceipt = "0"
            End If
        End Set
    End Property

    Private Sub send() Implements IMailMessage.send
        'Me.SEND()
    End Sub
    Public Property CC1() As Object Implements IMailMessage.CC
        Get
            Return GetString(Me.CC, ";")
        End Get
        Set(ByVal Value As Object)
            Dim s As String

            If Value Is Nothing Then
                s = ""
            Else
                s = Value
            End If

            Me.CC = New Generic.List(Of String)
            Me.CC.AddRange(s.Split(";"))

        End Set
    End Property
    Public Property CCO2() As Object Implements IMailMessage.CCO
        Get
            Return GetString(Me.CCO, ";")
        End Get
        Set(ByVal Value As Object)
            Dim s As String
            If Value Is Nothing Then
                s = ""
            Else
                s = Value
            End If
            Me.CCO = New Generic.List(Of String)
            Me.CCO.AddRange(s.Split(";"))

        End Set
    End Property
    Public Property MailTo2() As Object Implements IMailMessage.MailTo
        Get
            Return GetString(Me.CC, ";")
        End Get
        Set(ByVal Value As Object)
            Dim s As String

            If Value Is Nothing Then
                s = ""
            Else
                s = Value
            End If

            Me.MailTo = New Generic.List(Of String)
            Me.MailTo.AddRange(s.Split(";"))
        End Set
    End Property
    Public Property De2() As Object Implements IMailMessage.De
        Get
            Return Me.De
        End Get
        Set(ByVal Value As Object)
            Me.De = Value
        End Set
    End Property
    Public Property Subject2() As Object Implements IMailMessage.Subject
        Get
            Return Me.Subject
        End Get
        Set(ByVal Value As Object)
            Me.Subject = Value
        End Set
    End Property
    Private Shared Function GetString(ByVal data As Generic.List(Of String), ByVal Separator As String) As String
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



