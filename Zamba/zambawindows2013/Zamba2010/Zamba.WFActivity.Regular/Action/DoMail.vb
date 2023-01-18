Imports Zamba.Core

<RuleMainCategory("Mensajes, Mails y Foro"), RuleCategory("Mail"), RuleSubCategory(""), RuleDescription("Enviar Mail"), RuleHelp("Permite completar y enviar un mail"), RuleFeatures(False)> <Serializable()> _
Public Class DOMail

    Inherits WFRuleParent
    Implements IDOMail, IRuleValidate

    Public Overrides Sub Dispose()
    End Sub

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDOMail



    '''' <summary>
    '''' Constructor de la regla DoMail
    '''' </summary>
    '''' <param name="Id"></param>
    '''' <param name="Name"></param>
    '''' <param name="wfstepId"></param>
    '''' <param name="Para"></param>
    '''' <param name="CC"></param>
    '''' <param name="CCO"></param>
    '''' <param name="Asunto"></param>
    '''' <param name="Senddocument"></param>
    '''' <param name="Body"></param>
    '''' <param name="pAttachAssociatedDocument"></param>
    '''' <param name="imagNames"></param>
    '''' <param name="pathImag"></param>
    '''' <remarks></remarks>
    '''' <history>
    '''' [Gaston]    07/08/2008	Modified   Se agrego imagesNames y pathImages
    '''' </history>
    'Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal Para As String, ByVal CC As String, ByVal CCO As String, ByVal Asunto As String, ByVal Senddocument As Boolean, ByVal Body As String, ByVal pAttachAssociatedDocument As Boolean, ByVal imagNames As String, ByVal pathImag As String, ByVal groupMailTo As Boolean, ByVal attachLink As Boolean, ByVal DTType As IMailConfigDocAsoc.DTTypes, ByVal Selection As IMailConfigDocAsoc.Selections, ByVal DocTypes As String, ByVal Index As Int32, ByVal Oper As Comparadores, ByVal IndexValue As String, ByVal Automatic As Boolean, ByVal usesmtpconfig As Boolean, ByVal smtpserver As String, ByVal smtpport As String, ByVal smtpuser As String, ByVal smtppass As String, ByVal smtpmail As String, ByVal keepAssociatedDocName As Boolean)
    '    MyBase.New(Id, Name, wfstepId)
    '    _Para = Para
    '    _CC = CC
    '    _CCO = CCO
    '    _Asunto = Asunto
    '    _Senddocument = Senddocument
    '    _Body = Body
    '    _AttachAssociatedDocuments = pAttachAssociatedDocument
    '    _imagesNames = imagNames
    '    _pathImages = pathImag
    '    _groupMailTo = groupMailTo
    '    _AttachLink = attachLink
    '    Me.DTType = DTType
    '    Me.Selection = Selection
    '    Me.DocTypes = DocTypes
    '    Me.Index = Index
    '    Me.Oper = Oper
    '    Me.IndexValue = IndexValue
    '    mAutomatic = Automatic
    '    _smtpServer = smtpserver
    '    _smtpUser = smtpuser
    '    _smtpPort = smtpport
    '    _smtpPass = smtppass
    '    _smtpMail = smtpmail
    '    _useSMTPConfig = usesmtpconfig
    '    _keepAssociatedDocName = keepAssociatedDocName
    '    playRule = New Zamba.WFExecution.PlayDOMail(Me)
    'End Sub

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
    Private _ruleID As Int64
    Private _btnName As String
    Private _varAttachs As String
    Private _columnName As String
    Private _columnRoute As String
    Private _isValid As Boolean

    'funcionalidad ejecucion segunda regla
    Private _executeAdditionalRuleId As Int64
    Private _btnAdditionalRuleName As String
    'ver documento original en tab
    Private _viewOriginalDocument As Boolean
    Private _viewAssociateDocument As Boolean
    'ver documento original en tab
    Private _additionalRuleColumnRoute As String
    Private _additionalRuleColumnName As String

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
    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
    Public Property DTType() As IMailConfigDocAsoc.DTTypes Implements IDOMail.DTType
        Get
            Return mDTType
        End Get
        Set(ByVal value As IMailConfigDocAsoc.DTTypes)
            mDTType = value
        End Set
    End Property
    Public Property Selection() As IMailConfigDocAsoc.Selections Implements IDOMail.Selection
        Get
            Return mSelection
        End Get
        Set(ByVal value As IMailConfigDocAsoc.Selections)
            mSelection = value
        End Set
    End Property
    Public Property DocTypes() As String Implements IDOMail.DocTypes
        Get
            Return mDocTypes
        End Get
        Set(ByVal value As String)
            mDocTypes = value
        End Set
    End Property
    Public Property Index() As Int32 Implements IDOMail.Index
        Get
            Return mIndex
        End Get
        Set(ByVal value As Int32)
            mIndex = value
        End Set
    End Property
    Public Property Oper() As Comparadores Implements IDOMail.Oper
        Get
            Return mOper
        End Get
        Set(ByVal value As Comparadores)
            mOper = value
        End Set
    End Property
    Public Property IndexValue() As String Implements IDOMail.IndexValue
        Get
            Return mIndexValue
        End Get
        Set(ByVal value As String)
            mIndexValue = value
        End Set
    End Property
    Public Property Automatic() As Boolean Implements IDOMail.Automatic
        Get
            Return mAutomatic
        End Get
        Set(ByVal value As Boolean)
            mAutomatic = value
        End Set
    End Property
    Public Property Para() As String Implements IDOMail.Para
        Get
            Return _Para
        End Get
        Set(ByVal value As String)
            _Para = value
        End Set
    End Property
    Public Property CC() As String Implements IDOMail.CC
        Get
            Return _CC
        End Get
        Set(ByVal value As String)
            _CC = value
        End Set
    End Property
    Public Property CCO() As String Implements IDOMail.CCO
        Get
            Return _CCO
        End Get
        Set(ByVal value As String)
            _CCO = value
        End Set
    End Property
    Public Property Asunto() As String Implements IDOMail.Asunto
        Get
            Return _Asunto
        End Get
        Set(ByVal value As String)
            _Asunto = value
        End Set
    End Property
    Public Property Body() As String Implements IDOMail.Body
        Get
            Return _Body
        End Get
        Set(ByVal value As String)
            _Body = value
        End Set
    End Property
    Public Property SmtpMail() As String Implements IDOMail.SmtpMail
        Get
            Return _smtpMail
        End Get
        Set(ByVal value As String)
            _smtpMail = value
        End Set
    End Property
    Public Property SmtpPass() As String Implements IDOMail.SmtpPass
        Get
            Return _smtpPass
        End Get
        Set(ByVal value As String)
            _smtpPass = value
        End Set
    End Property
    Public Property SmtpPort() As String Implements IDOMail.SmtpPort
        Get
            Return _smtpPort
        End Get
        Set(ByVal value As String)
            _smtpPort = value
        End Set
    End Property
    Public Property SmtpServer() As String Implements IDOMail.SmtpServer
        Get
            Return _smtpServer
        End Get
        Set(ByVal value As String)
            _smtpServer = value
        End Set
    End Property
    Public Property SmtpUser() As String Implements IDOMail.SmtpUser
        Get
            Return _smtpUser
        End Get
        Set(ByVal value As String)
            _smtpUser = value
        End Set
    End Property
    Public Property UseSMTPCOnfig() As Boolean Implements IDOMail.UseSMTPConfig
        Get
            Return _useSMTPConfig
        End Get
        Set(ByVal value As Boolean)
            _useSMTPConfig = value
        End Set
    End Property
    Public Property AttachLink() As Boolean Implements IDOMail.AttachLink
        Get
            Return _AttachLink
        End Get
        Set(ByVal value As Boolean)
            _AttachLink = value
        End Set
    End Property
    Public Property AttachAssociatedDocuments() As Boolean Implements IDOMail.AttachAssociatedDocuments
        Get
            Return _AttachAssociatedDocuments
        End Get
        Set(ByVal value As Boolean)
            _AttachAssociatedDocuments = value
        End Set
    End Property
    Public Property SendDocument() As Boolean Implements IDOMail.SendDocument
        Get
            Return _Senddocument
        End Get
        Set(ByVal value As Boolean)
            _Senddocument = value
        End Set
    End Property
    Public Property groupMailTo() As Boolean Implements IDOMail.groupMailTo
        Get
            Return _groupMailTo
        End Get
        Set(ByVal value As Boolean)
            _groupMailTo = value
        End Set
    End Property
    Public Property ImagesNames() As String Implements IDOMail.ImagesNames
        Get
            Return (_imagesNames)
        End Get
        Set(ByVal value As String)
            _imagesNames = value
        End Set
    End Property
    Public Property PathImages() As String Implements IDOMail.PathImages
        Get
            Return (_pathImages)
        End Get
        Set(ByVal value As String)
            _pathImages = value
        End Set
    End Property
    Public Property KeepAssociatedDocsName() As Boolean Implements IDOMail.KeepAssociatedDocsName
        Get
            Return _keepAssociatedDocName
        End Get
        Set(ByVal value As Boolean)
            _keepAssociatedDocName = value
        End Set
    End Property
    Public Property EmbedImages() As Boolean Implements IDOMail.EmbedImages
        Get
            Return _embedImages
        End Get
        Set(ByVal value As Boolean)
            _embedImages = value
        End Set
    End Property

    Public Property SaveMailPath() As Boolean Implements IDOMail.SaveMailPath
        Get
            Return _saveMailPath
        End Get
        Set(ByVal value As Boolean)
            _saveMailPath = value
        End Set
    End Property
    Public Property MailPath() As String Implements IDOMail.MailPath
        Get
            Return _mailPath
        End Get
        Set(ByVal value As String)
            _mailPath = value
        End Set
    End Property

    Public Property DisableHistory() As Boolean Implements IDOMail.DisableHistory
        Get
            Return _disableHistory
        End Get
        Set(ByVal value As Boolean)
            _disableHistory = value
        End Set
    End Property

    Public Property FilterDocID() As String Implements IDOMail.FilterDocID
        Get
            Return _filterDocID
        End Get
        Set(ByVal value As String)
            _filterDocID = value
        End Set
    End Property
    Public Property RuleID() As Int64 Implements IDOMail.RuleID
        Get
            Return _ruleID
        End Get
        Set(ByVal value As Int64)
            _ruleID = value
        End Set
    End Property
    Public Property BtnName() As String Implements IDOMail.BtnName
        Get
            Return _btnName
        End Get
        Set(ByVal value As String)
            _btnName = value
        End Set
    End Property
    Public Property VarAttachs() As String Implements IDOMail.VarAttachs
        Get
            Return _varAttachs
        End Get
        Set(ByVal value As String)
            _varAttachs = value
        End Set
    End Property
    Public Property ColumnNameNumber() As String Implements IDOMail.ColumnName
        Get
            Return _columnName
        End Get
        Set(ByVal value As String)
            _columnName = value
        End Set
    End Property
    Public Property ColumnRouteNumber() As String Implements IDOMail.ColumnRoute
        Get
            Return _columnRoute
        End Get
        Set(ByVal value As String)
            _columnRoute = value
        End Set
    End Property

    Private Property ExecuteAdditionalRuleID() As Int64 Implements IDOMail.ExecuteAdditionalRuleID
        Get
            Return _executeAdditionalRuleId
        End Get
        Set(ByVal value As Int64)
            _executeAdditionalRuleId = value
        End Set
    End Property

    Private Property BtnAdditionalRuleName() As String Implements IDOMail.BtnAdditionalRuleName
        Get
            Return _btnAdditionalRuleName
        End Get
        Set(ByVal value As String)
            _btnAdditionalRuleName = value
        End Set
    End Property
    Private Property ViewOriginalDocument() As Boolean Implements IDOMail.ViewOriginal
        Get
            Return _viewOriginalDocument
        End Get
        Set(ByVal value As Boolean)
            _viewOriginalDocument = value
        End Set
    End Property
    Private Property ViewAssociateDocument() As Boolean Implements IDOMail.ViewAssociateDocuments
        Get
            Return _viewAssociateDocument
        End Get
        Set(ByVal value As Boolean)
            _viewAssociateDocument = value
        End Set
    End Property
    Public Property AdditionalRuleColumnNameNumber() As String Implements IDOMail.AdditionalRuleColumnName
        Get
            Return _additionalRuleColumnName
        End Get
        Set(ByVal value As String)
            _additionalRuleColumnName = value
        End Set
    End Property
    Public Property AdditionalRuleColumnRouteNumber() As String Implements IDOMail.AdditionalRuleColumnRoute
        Get
            Return _additionalRuleColumnRoute
        End Get
        Set(ByVal value As String)
            _additionalRuleColumnRoute = value
        End Set
    End Property

    Public Property IsValid As Boolean Implements IRuleValidate.isValid
        Get
            Return _isValid
        End Get
        Set(ByVal value As Boolean)
            _isValid = value
        End Set
    End Property

    Public Property AttachTableVar() As String Implements IDOMail.AttachTableVar
    Public Property AttachTableColDocTypeId() As String Implements IDOMail.AttachTableColDocTypeId
    Public Property AttachTableColDocId() As String Implements IDOMail.AttachTableColDocId
    Public Property AttachTableColDocName() As String Implements IDOMail.AttachTableColDocName
    Public Property SmtpEnableSsl() As Boolean Implements IDOMail.SmtpEnableSsl

    ''' <summary>
    ''' Constructor de la regla DoMail
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' </history>
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, _
                   ByVal Para As String, ByVal CC As String, ByVal CCO As String, ByVal Asunto As String, _
                   ByVal Senddocument As Boolean, ByVal Body As String, ByVal pAttachAssociatedDocument As Boolean, _
                   ByVal imagNames As String, ByVal pathImag As String, ByVal groupMailTo As Boolean, _
                   ByVal attachLink As Boolean, ByVal DTType As IMailConfigDocAsoc.DTTypes, _
                   ByVal Selection As IMailConfigDocAsoc.Selections, ByVal DocTypes As String, ByVal Index As Int32, _
                   ByVal Oper As Comparadores, ByVal IndexValue As String, ByVal Automatic As Boolean, _
                   ByVal usesmtpconfig As Boolean, ByVal smtpserver As String, ByVal smtpport As String, _
                   ByVal smtpuser As String, ByVal smtppass As String, ByVal smtpmail As String, _
                   ByVal keepAssociatedDocName As Boolean, ByVal EmbedImages As Boolean, ByVal savemailpath As Boolean, _
                   ByVal mailpath As String, ByVal disablehistory As Boolean, ByVal filterDocID As String, _
                   ByVal RuleID As Int64, ByVal BtnName As String, ByVal VarAttachs As String, ByVal ColumnName As String, _
                   ByVal ColumnRoute As String, ByVal ExecuteAdditionalRuleID As Int64, ByVal BtnAdditionalRuleName As String, _
                   ByVal ViewOriginalDocument As Boolean, ByVal ViewAssociateDocument As Boolean, ByVal AdditionalRuleColumnName As String, _
                   ByVal AdditionalRuleColumnRoute As String, _
                   ByVal attachTableVar As String, _
                   ByVal attachTableColDocTypeId As String, _
                   ByVal attachTableColDocId As String, _
                   ByVal attachTableColDocName As String, _
                   ByVal smtpEnableSsl As Boolean)

        MyBase.New(Id, Name, wfstepId)
        _Para = Para
        _CC = CC
        _CCO = CCO
        _Asunto = Asunto
        _Senddocument = Senddocument
        _Body = Body
        _AttachAssociatedDocuments = pAttachAssociatedDocument
        _imagesNames = imagNames
        _pathImages = pathImag
        _groupMailTo = groupMailTo
        _AttachLink = attachLink
        Me.DTType = DTType
        Me.Selection = Selection
        Me.DocTypes = DocTypes
        Me.Index = Index
        Me.Oper = Oper
        Me.IndexValue = IndexValue
        mAutomatic = Automatic
        _smtpServer = smtpserver
        _smtpUser = smtpuser
        _smtpPort = smtpport
        _smtpPass = smtppass
        _smtpMail = smtpmail
        Me.SmtpEnableSsl = smtpEnableSsl
        _useSMTPConfig = usesmtpconfig
        _keepAssociatedDocName = keepAssociatedDocName
        _embedImages = EmbedImages
        _saveMailPath = savemailpath
        _mailPath = mailpath
        _disableHistory = disablehistory
        _filterDocID = filterDocID
        _ruleID = RuleID
        Me.BtnName = BtnName
        _varAttachs = VarAttachs
        playRule = New Zamba.WFExecution.PlayDOMail(Me)
        ColumnNameNumber = ColumnName
        ColumnRouteNumber = ColumnRoute

        _executeAdditionalRuleId = ExecuteAdditionalRuleID
        _btnAdditionalRuleName = BtnAdditionalRuleName

        _viewOriginalDocument = ViewOriginalDocument
        _viewAssociateDocument = ViewAssociateDocument

        AdditionalRuleColumnNameNumber = AdditionalRuleColumnName
        AdditionalRuleColumnRouteNumber = AdditionalRuleColumnRoute

        Me.AttachTableVar = attachTableVar
        Me.AttachTableColDocTypeId = attachTableColDocTypeId
        Me.AttachTableColDocId = attachTableColDocId
        Me.AttachTableColDocName = attachTableColDocName
    End Sub

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function

    Public Overrides ReadOnly Property MaskName() As String
        Get
            If _Para Is Nothing Then
                Return "Envio Mail"
            Else
                Return "Envio Mail a " & _Para
            End If
        End Get
    End Property

End Class