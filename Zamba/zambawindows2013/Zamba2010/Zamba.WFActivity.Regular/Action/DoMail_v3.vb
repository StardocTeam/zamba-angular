Imports Zamba.Data
Imports Zamba.Core
Imports System.Xml.Serialization

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Bussines
''' Class	 : Core.DOMail
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Regla que permite enviar un Mail
''' </summary>
''' <remarks>
''' Hereda WFRuleParent
''' </remarks>
''' <history>
''' 	[Alejandro]	31/05/2010	Created
''' </history>
''' -----------------------------------------------------------------------------
<RuleCategory("Mensajes"), RuleDescription("Enviar Mail"), RuleApproved("True"), RuleHelp("Permite completar y enviar un mail"), RuleFeatures(True)> <Serializable()> _
Public Class DOMail_v3

    Inherits WFRuleParent
    Implements IDOMail_v3

    Public Overrides Sub Dispose()
    End Sub

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDOMail_v3

    Private mDTType As IMailConfigDocAsoc.DTTypes = IMailConfigDocAsoc.DTTypes.AllDT
    Private mSelection As IMailConfigDocAsoc.Selections = IMailConfigDocAsoc.Selections.First
    Private mDocTypes As String
    Private mIndex As Int32
    Private mOper As String
    Private mIndexValue As String
    Private mAutomatic As Boolean
    Private _smtpMail As String
    Private _smtpPass As String
    Private _smtpPort As String
    Private _smtpServer As String
    Private _smtpUser As String
    Private _useSMTPConfig As Boolean
    Private _keepAssociatedDocName As Boolean
    Private _Para As String
    Private _CC As String
    Private _CCO As String
    Private _Asunto As String
    Private _Body As String
    Private _AttachLink As Boolean
    Private _AttachAssociatedDocuments As Boolean
    Private _Senddocument As Boolean
    Private _groupMailTo As Boolean
    Private _imagesNames As String
    Private _pathImages As String
    Private _embedImages As Boolean
    Private _saveMailPath As Boolean
    Private _mailPath As String
    Private _disableHistory As Boolean
    Private _filterDocID As String

    Public Overrides ReadOnly Property IsFull() As Boolean
        Get
            Return _isFull
        End Get
    End Property
    Public Overrides ReadOnly Property IsLoaded() As Boolean
        Get
            Return _isLoaded
        End Get
    End Property
    Public Overrides ReadOnly Property MaskName() As String
        Get
            If _Para Is Nothing Then
                Return "Envio Mail"
            Else
                Return "Envio Mail a " & _Para
            End If
        End Get
    End Property
    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
    Public Property DTType() As IMailConfigDocAsoc.DTTypes Implements IDOMail_v3.DTType
        Get
            Return mDTType
        End Get
        Set(ByVal value As IMailConfigDocAsoc.DTTypes)
            mDTType = value
        End Set
    End Property
    Public Property Selection() As IMailConfigDocAsoc.Selections Implements IDOMail_v3.Selection
        Get
            Return mSelection
        End Get
        Set(ByVal value As IMailConfigDocAsoc.Selections)
            mSelection = value
        End Set
    End Property
    Public Property DocTypes() As String Implements IDOMail_v3.DocTypes
        Get
            Return mDocTypes
        End Get
        Set(ByVal value As String)
            mDocTypes = value
        End Set
    End Property
    Public Property Index() As Int32 Implements IDOMail_v3.Index
        Get
            Return mIndex
        End Get
        Set(ByVal value As Int32)
            mIndex = value
        End Set
    End Property
    Public Property Oper() As Comparadores Implements IDOMail_v3.Oper
        Get
            Return mOper
        End Get
        Set(ByVal value As Comparadores)
            mOper = value
        End Set
    End Property
    Public Property IndexValue() As String Implements IDOMail_v3.IndexValue
        Get
            Return mIndexValue
        End Get
        Set(ByVal value As String)
            mIndexValue = value
        End Set
    End Property
    Public Property Automatic() As Boolean Implements IDOMail_v3.Automatic
        Get
            Return mAutomatic
        End Get
        Set(ByVal value As Boolean)
            mAutomatic = value
        End Set
    End Property
    Public Property Para() As String Implements IDOMail_v3.Para
        Get
            Return _Para
        End Get
        Set(ByVal value As String)
            _Para = value
        End Set
    End Property
    Public Property CC() As String Implements IDOMail_v3.CC
        Get
            Return _CC
        End Get
        Set(ByVal value As String)
            _CC = value
        End Set
    End Property
    Public Property CCO() As String Implements IDOMail_v3.CCO
        Get
            Return _CCO
        End Get
        Set(ByVal value As String)
            _CCO = value
        End Set
    End Property
    Public Property Asunto() As String Implements IDOMail_v3.Asunto
        Get
            Return _Asunto
        End Get
        Set(ByVal value As String)
            _Asunto = value
        End Set
    End Property
    Public Property Body() As String Implements IDOMail_v3.Body
        Get
            Return _Body
        End Get
        Set(ByVal value As String)
            _Body = value
        End Set
    End Property
    Public Property SmtpMail() As String Implements IDOMail_v3.SmtpMail
        Get
            Return _smtpMail
        End Get
        Set(ByVal value As String)
            _smtpMail = value
        End Set
    End Property
    Public Property SmtpPass() As String Implements IDOMail_v3.SmtpPass
        Get
            Return _smtpPass
        End Get
        Set(ByVal value As String)
            _smtpPass = value
        End Set
    End Property
    Public Property SmtpPort() As String Implements IDOMail_v3.SmtpPort
        Get
            Return _smtpPort
        End Get
        Set(ByVal value As String)
            _smtpPort = value
        End Set
    End Property
    Public Property SmtpServer() As String Implements IDOMail_v3.SmtpServer
        Get
            Return _smtpServer
        End Get
        Set(ByVal value As String)
            _smtpServer = value
        End Set
    End Property
    Public Property SmtpUser() As String Implements IDOMail_v3.SmtpUser
        Get
            Return _smtpUser
        End Get
        Set(ByVal value As String)
            _smtpUser = value
        End Set
    End Property
    Public Property UseSMTPCOnfig() As Boolean Implements IDOMail_v3.UseSMTPConfig
        Get
            Return _useSMTPConfig
        End Get
        Set(ByVal value As Boolean)
            _useSMTPConfig = value
        End Set
    End Property
    Public Property AttachLink() As Boolean Implements IDOMail_v3.AttachLink
        Get
            Return _AttachLink
        End Get
        Set(ByVal value As Boolean)
            _AttachLink = value
        End Set
    End Property
    Public Property AttachAssociatedDocuments() As Boolean Implements IDOMail_v3.AttachAssociatedDocuments
        Get
            Return _AttachAssociatedDocuments
        End Get
        Set(ByVal value As Boolean)
            _AttachAssociatedDocuments = value
        End Set
    End Property
    Public Property SendDocument() As Boolean Implements IDOMail_v3.SendDocument
        Get
            Return _Senddocument
        End Get
        Set(ByVal value As Boolean)
            _Senddocument = value
        End Set
    End Property
    Public Property groupMailTo() As Boolean Implements IDOMail_v3.groupMailTo
        Get
            Return _groupMailTo
        End Get
        Set(ByVal value As Boolean)
            _groupMailTo = value
        End Set
    End Property
    Public Property ImagesNames() As String Implements IDOMail_v3.ImagesNames
        Get
            Return (_imagesNames)
        End Get
        Set(ByVal value As String)
            _imagesNames = value
        End Set
    End Property
    Public Property PathImages() As String Implements IDOMail_v3.PathImages
        Get
            Return (_pathImages)
        End Get
        Set(ByVal value As String)
            _pathImages = value
        End Set
    End Property
    Public Property KeepAssociatedDocsName() As Boolean Implements IDOMail_v3.KeepAssociatedDocsName
        Get
            Return _keepAssociatedDocName
        End Get
        Set(ByVal value As Boolean)
            _keepAssociatedDocName = value
        End Set
    End Property
    Public Property EmbedImages() As Boolean Implements IDOMail_v2.EmbedImages
        Get
            Return _embedImages
        End Get
        Set(ByVal value As Boolean)
            _embedImages = value
        End Set
    End Property

    Public Property SaveMailPath() As Boolean Implements IDOMail_v3.SaveMailPath
        Get
            Return _saveMailPath
        End Get
        Set(ByVal value As Boolean)
            Me._saveMailPath = value
        End Set
    End Property
    Public Property MailPath() As String Implements IDOMail_v3.MailPath
        Get
            Return _mailPath
        End Get
        Set(ByVal value As String)
            Me._mailPath = value
        End Set
    End Property

    Public Property DisableHistory() As Boolean Implements IDOMail_v3.DisableHistory
        Get
            Return Me._disableHistory
        End Get
        Set (ByVal value As Boolean)
            Me._disableHistory = value
        End Set
    End Property

    Public Property FilterDocID() As String Implements IDOMail_v3.FilterDocID
        Get
            Return _filterDocID
        End Get
        Set (ByVal value As String)
            Me._filterDocID = value
        End Set
    End Property


    ''' <summary>
    ''' Constructor de la regla DoMail
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' </history>
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal Para As String, ByVal CC As String, ByVal CCO As String, ByVal Asunto As String, ByVal Senddocument As Boolean, ByVal Body As String, ByVal pAttachAssociatedDocument As Boolean, ByVal imagNames As String, ByVal pathImag As String, ByVal groupMailTo As Boolean, ByVal attachLink As Boolean, ByVal DTType As IMailConfigDocAsoc.DTTypes, ByVal Selection As IMailConfigDocAsoc.Selections, ByVal DocTypes As String, ByVal Index As Int32, ByVal Oper As Comparadores, ByVal IndexValue As String, ByVal Automatic As Boolean, ByVal usesmtpconfig As Boolean, ByVal smtpserver As String, ByVal smtpport As String, ByVal smtpuser As String, ByVal smtppass As String, ByVal smtpmail As String, ByVal keepAssociatedDocName As Boolean, ByVal EmbedImages As Boolean, ByVal savemailpath As Boolean, ByVal mailpath As String, ByVal disablehistory As Boolean, ByVal filterDocID As String)
        MyBase.New(Id, Name, wfstepId)
        Me._Para = Para
        Me._CC = CC
        Me._CCO = CCO
        Me._Asunto = Asunto
        Me._Senddocument = Senddocument
        Me._Body = Body
        Me._AttachAssociatedDocuments = pAttachAssociatedDocument
        Me._imagesNames = imagNames
        Me._pathImages = pathImag
        Me._groupMailTo = groupMailTo
        Me._AttachLink = attachLink
        Me.DTType = DTType
        Me.Selection = Selection
        Me.DocTypes = DocTypes
        Me.Index = Index
        Me.Oper = Oper
        Me.IndexValue = IndexValue
        Me.mAutomatic = Automatic
        Me._smtpServer = smtpserver
        Me._smtpUser = smtpuser
        Me._smtpPort = smtpport
        Me._smtpPass = smtppass
        Me._smtpMail = smtpmail
        Me._useSMTPConfig = usesmtpconfig
        Me._keepAssociatedDocName = keepAssociatedDocName
        Me._embedImages = EmbedImages
        Me._saveMailPath = savemailpath
        Me._mailPath = mailpath
        Me._disableHistory = disablehistory
        Me._filterDocID = filterDocID
        Me.playRule = New Zamba.WFExecution.PlayDOMail_v3(Me)
    End Sub

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

End Class