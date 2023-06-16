Imports Zamba.Data
Imports Zamba.Core
Imports System.Xml.Serialization

<RuleCategory("Mail"), RuleDescription("Enviar Mail"), RuleHelp("Permite completar y enviar un mail"), RuleFeatures(False)> <Serializable()> _
Public Class DOMail

    Inherits WFRuleParent
    Implements IDOMail

    Public Overrides Sub Dispose()
    End Sub

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDOMail

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
    Private _attachFile As Boolean
    Private _attachFilePath As String
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
            Me._saveMailPath = value
        End Set
    End Property
    Public Property MailPath() As String Implements IDOMail.MailPath
        Get
            Return _mailPath
        End Get
        Set(ByVal value As String)
            Me._mailPath = value
        End Set
    End Property

    Public Property DisableHistory() As Boolean Implements IDOMail.DisableHistory
        Get
            Return Me._disableHistory
        End Get
        Set(ByVal value As Boolean)
            Me._disableHistory = value
        End Set
    End Property

    Public Property FilterDocID() As String Implements IDOMail.FilterDocID
        Get
            Return _filterDocID
        End Get
        Set(ByVal value As String)
            Me._filterDocID = value
        End Set
    End Property
    Public Property RuleID() As Int64 Implements IDOMail.RuleID
        Get
            Return _ruleID
        End Get
        Set(ByVal value As Int64)
            Me._ruleID = value
        End Set
    End Property
    Public Property BtnName() As String Implements IDOMail.BtnName
        Get
            Return _btnName
        End Get
        Set(ByVal value As String)
            Me._btnName = value
        End Set
    End Property
    Public Property VarAttachs() As String Implements IDOMail.VarAttachs
        Get
            Return _varAttachs
        End Get
        Set(ByVal value As String)
            Me._varAttachs = value
        End Set
    End Property
    Public Property ColumnNameNumber() As String Implements IDOMail.ColumnName
        Get
            Return _columnName
        End Get
        Set(ByVal value As String)
            Me._columnName = value
        End Set
    End Property
    Public Property ColumnRouteNumber() As String Implements IDOMail.ColumnRoute
        Get
            Return _columnRoute
        End Get
        Set(ByVal value As String)
            Me._columnRoute = value
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
            Me._additionalRuleColumnName = value
        End Set
    End Property
    Public Property AdditionalRuleColumnRouteNumber() As String Implements IDOMail.AdditionalRuleColumnRoute
        Get
            Return _additionalRuleColumnRoute
        End Get
        Set(ByVal value As String)
            Me._additionalRuleColumnRoute = value
        End Set
    End Property

    Public Property AttachTableVar() As String Implements IDOMail.AttachTableVar
    Public Property AttachTableColDocTypeId() As String Implements IDOMail.AttachTableColDocTypeId
    Public Property AttachTableColDocId() As String Implements IDOMail.AttachTableColDocId
    Public Property AttachTableColDocName() As String Implements IDOMail.AttachTableColDocName

    'Public Property SmtpEnableSsl() As Boolean Implements IDOMail.SmtpEnableSsl
    Private _SmtpEnableSsl As Boolean
    Public Property SmtpEnableSsl() As Boolean Implements IDOMail.SmtpEnableSsl
        Get
            Return _SmtpEnableSsl
        End Get
        Set(ByVal value As Boolean)
            _SmtpEnableSsl = value
        End Set
    End Property

    Public Property AttachFile As Boolean Implements IDOMail.AttachFile
        Get
            Return _attachFile
        End Get
        Set(value As Boolean)
            _attachFile = value
        End Set
    End Property
    Public Property AttachFilePath As String Implements IDOMail.AttachFilePath
        Get
            Return _attachFilePath
        End Get
        Set(value As String)
            _attachFilePath = value
        End Set
    End Property



    ''' <summary>
    ''' Constructor de la regla DoMail
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' </history>
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64,
                   ByVal Para As String, ByVal CC As String, ByVal CCO As String, ByVal Asunto As String,
                   ByVal Senddocument As Boolean, ByVal Body As String, ByVal pAttachAssociatedDocument As Boolean,
                   ByVal imagNames As String, ByVal pathImag As String, ByVal groupMailTo As Boolean,
                   ByVal attachLink As Boolean, ByVal DTType As IMailConfigDocAsoc.DTTypes,
                   ByVal Selection As IMailConfigDocAsoc.Selections, ByVal DocTypes As String, ByVal Index As Int32,
                   ByVal Oper As Comparadores, ByVal IndexValue As String, ByVal Automatic As Boolean,
                   ByVal usesmtpconfig As Boolean, ByVal smtpserver As String, ByVal smtpport As String,
                   ByVal smtpuser As String, ByVal smtppass As String, ByVal smtpmail As String,
                   ByVal keepAssociatedDocName As Boolean, ByVal EmbedImages As Boolean, ByVal savemailpath As Boolean,
                   ByVal mailpath As String, ByVal disablehistory As Boolean, ByVal filterDocID As String,
                   ByVal RuleID As Int64, ByVal BtnName As String, ByVal VarAttachs As String, ByVal ColumnName As String,
                   ByVal ColumnRoute As String, ByVal ExecuteAdditionalRuleID As Int64, ByVal BtnAdditionalRuleName As String,
                   ByVal ViewOriginalDocument As Boolean, ByVal ViewAssociateDocument As Boolean, ByVal AdditionalRuleColumnName As String,
                   ByVal AdditionalRuleColumnRoute As String,
                   ByVal attachTableVar As String,
                   ByVal attachTableColDocTypeId As String,
                   ByVal attachTableColDocId As String,
                   ByVal attachTableColDocName As String,
                   ByVal smtpEnableSsl As Boolean,
                   ByVal Answer As Boolean,
                   ByVal AttachFile As Boolean,
                   ByVal _attachFilePath As String)

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
        Me._SmtpEnableSsl = smtpEnableSsl
        Me._useSMTPConfig = usesmtpconfig
        Me._keepAssociatedDocName = keepAssociatedDocName
        Me._embedImages = EmbedImages
        Me._saveMailPath = savemailpath
        Me._mailPath = mailpath
        Me._disableHistory = disablehistory
        Me._filterDocID = filterDocID
        Me._ruleID = RuleID
        Me.BtnName = BtnName
        Me._varAttachs = VarAttachs
        Me.ColumnNameNumber = ColumnName
        Me.ColumnRouteNumber = ColumnRoute
        Me._executeAdditionalRuleId = ExecuteAdditionalRuleID
        Me._btnAdditionalRuleName = BtnAdditionalRuleName
        Me._viewOriginalDocument = ViewOriginalDocument
        Me._viewAssociateDocument = ViewAssociateDocument
        Me.AdditionalRuleColumnNameNumber = AdditionalRuleColumnName
        Me.AdditionalRuleColumnRouteNumber = AdditionalRuleColumnRoute
        Me.AttachTableVar = attachTableVar
        Me.AttachTableColDocTypeId = attachTableColDocTypeId
        Me.AttachTableColDocId = attachTableColDocId
        Me.AttachTableColDocName = attachTableColDocName
        Me.playRule = New Zamba.WFExecution.PlayDOMail(Me)
        Me.AttachFile = AttachFile
        Me.AttachFilePath = AttachFilePath
    End Sub

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        If IsNothing(Params) Then
            Params = New Hashtable
        End If
        If IsNothing(Params) OrElse Params.Count = 0 Then
            'Si el envio no es automatico quiere decir que se debe mostrar el control
            'Por ende seteo que tiene un evento pendiente
            If Not Me.mAutomatic Then
                ExecutionResult = RuleExecutionResult.PendingEventExecution
                RulePendingEvent = RulePendingEvents.ShowMail
            End If

            'Ejecuto la regla
            Dim returnResults As List(Of ITaskResult) = playRule.PlayWeb(results, Params)

            'Si el evnio es automatico y se ejecuto correctamente
            'Seteo los eventos de manera correcta
            If Me.mAutomatic AndAlso Not returnResults Is Nothing Then

                Params.Clear()
                ExecutionResult = RuleExecutionResult.CorrectExecution
                RulePendingEvent = RulePendingEvents.NoPendingEvent
            End If

            Return returnResults
        Else
            Params.Clear()
            ExecutionResult = RuleExecutionResult.CorrectExecution
            RulePendingEvent = RulePendingEvents.NoPendingEvent
            Return results
        End If
    End Function
End Class