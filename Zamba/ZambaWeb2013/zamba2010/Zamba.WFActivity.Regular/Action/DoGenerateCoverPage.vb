Imports System
Imports Zamba.Data
Imports Zamba.Core
Imports Zamba.Core.MessagesBusiness
Imports System.Xml.Serialization

<RuleCategory("importacion"), RuleDescription("Generar Caratula"), RuleHelp("Genera una caratula a partir del documento seleccionado en la tarea"), RuleFeatures(True)> <Serializable()> _
Public Class DoGenerateCoverPage
    Inherits WFRuleParent
    Implements IDoGenerateCoverPage

    Public Overrides Sub Dispose()

    End Sub
    Private _isLoaded As Boolean
    Private _isFull As Boolean
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

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult)) As List(Of ITaskResult)
        Dim playRule As New WFExecution.PlayDoGenerateCoverPage(Me)
        Return playRule.Play(results, Me)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As List(Of ITaskResult),
                                                ByRef RulePendingEvent As RulePendingEvents,
                                                ByRef ExecutionResult As RuleExecutionResult,
                                                ByRef Params As Hashtable) As List(Of ITaskResult)
        Dim playRule As New WFExecution.PlayDoGenerateCoverPage(Me)
        Return playRule.PlayWeb(Me, results, RulePendingEvent, ExecutionResult, Params)
    End Function


    Public Sub New(ByVal ruleID As Int64,
                   ByVal ruleName As String,
                   ByVal wfstepid As Int64,
                   ByVal pDocTypeId As Int64,
                   ByVal pPrintIndexs As Boolean,
                   ByVal pNote As String,
                   ByVal setprinter As Boolean,
                   ByVal numcopies As String,
                   ByVal continueWithGeneratedDocument As Boolean,
                   ByVal DontOpenTaskAfterInsert As Boolean,
                   ByVal UseTemplate As Boolean,
                   ByVal TemplatePath As String,
                   ByVal UseCurrentTask As Boolean,
                   ByVal CopiesCount As String,
                   ByVal templateWidth As Single,
                   ByVal templateHeight As Single)

        MyBase.New(ruleID, ruleName, wfstepid)
        DocTypeId = pDocTypeId
        PrintIndexs = pPrintIndexs
        Me.continueWithGeneratedDocument = continueWithGeneratedDocument
        Note = pNote
        _setPrinter = setprinter
        _copies = numcopies
        _dontOpenTaskAfterInsert = DontOpenTaskAfterInsert
        Me.UseTemplate = UseTemplate
        Me.TemplatePath = TemplatePath
        Me.UseCurrentTask = UseCurrentTask
        Me.CopiesCount = CopiesCount
        Me.templateWidth = templateWidth
        Me.templateHeight = templateHeight

    End Sub

    Dim mDocTypeId As Int64

    Public Property DocTypeId() As Int64 Implements IDoGenerateCoverPage.DocTypeId
        Get
            Return mDocTypeId
        End Get
        Set(ByVal value As Int64)
            mDocTypeId = value
        End Set
    End Property


    Private mNote As String
    Public Property Note() As String Implements Core.IDoGenerateCoverPage.Note
        Get
            Return mNote
        End Get
        Set(ByVal value As String)
            mNote = value
        End Set
    End Property

    Private mPrintIndexs As Boolean
    Public Property PrintIndexs() As Boolean Implements Core.IDoGenerateCoverPage.PrintIndexs
        Get
            Return mPrintIndexs
        End Get
        Set(ByVal value As Boolean)
            mPrintIndexs = value
        End Set
    End Property

    Private mContinueWithGeneratedDocument As Boolean
    Public Property continueWithGeneratedDocument() As Boolean Implements IDoGenerateCoverPage.continueWithGeneratedDocument
        Get
            Return mContinueWithGeneratedDocument
        End Get
        Set(ByVal value As Boolean)
            mContinueWithGeneratedDocument = value
        End Set
    End Property

    Private _copies As Int16
    'Public Property Copies() As Int16 Implements IDoGenerateCoverPage.Copies
    '    Get
    '        Return _copies
    '    End Get
    '    Set(ByVal value As Int16)
    '        _copies = value
    '    End Set
    'End Property

    Private _setPrinter As Boolean
    Public Property SetPrinter() As Boolean Implements IDoGenerateCoverPage.SetPrinter
        Get
            Return _setPrinter
        End Get
        Set(ByVal value As Boolean)
            _setPrinter = value
        End Set
    End Property
    Private _dontOpenTaskAfterInsert As Boolean
    Public Property DontOpenTaskAfterInsert() As Boolean Implements IDoGenerateCoverPage.DontOpenTaskAfterInsert
        Get
            Return _dontOpenTaskAfterInsert
        End Get
        Set(ByVal value As Boolean)
            _dontOpenTaskAfterInsert = value
        End Set
    End Property

    Public Property Copies() As String Implements IDoGenerateCoverPage.Copies
        Get
            Return _copies
        End Get
        Set(ByVal value As String)
            _copies = value
        End Set
    End Property

    Private _useTemplate As Boolean
    Public Property UseTemplate As Boolean Implements IDoGenerateCoverPage.UseTemplate
        Get
            Return _useTemplate
        End Get
        Set(value As Boolean)
            _useTemplate = value
        End Set
    End Property

    Private _templatePath
    Public Property TemplatePath As String Implements IDoGenerateCoverPage.TemplatePath
        Get
            Return _templatePath
        End Get
        Set(value As String)
            _templatePath = value
        End Set
    End Property

    Private _useCurrentTask
    Public Property UseCurrentTask As Boolean Implements IDoGenerateCoverPage.UseCurrentTask
        Get
            Return _useCurrentTask
        End Get
        Set(value As Boolean)
            _useCurrentTask = value
        End Set
    End Property

    Private _copiesCount

    ''' <summary>
    ''' Numero de impresiones
    ''' </summary>
    ''' <returns></returns>
    Public Property CopiesCount As String Implements IDoGenerateCoverPage.CopiesCount
        Get
            Return _copiesCount
        End Get
        Set(value As String)
            _copiesCount = value
        End Set
    End Property

    Public Property templateWidth As Single Implements IDoGenerateCoverPage.templateWidth
    Public Property templateHeight As Single Implements IDoGenerateCoverPage.templateHeight
End Class
