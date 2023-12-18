Imports Zamba.Core

<RuleMainCategory("Documentos y Asociados"), RuleCategory("Documentos"), RuleSubCategory(""), RuleDescription("Esperar por Documento"), RuleHelp("Espera por un documento entrante"), RuleFeatures(False)> <Serializable()> _
Public Class DoWaitForDocument
    Inherits WFRuleParent
    Implements IDoWaitForDocument, IRuleValidate

    Private playRule As Zamba.WFExecution.PlayDoWaitForDocument



    Public Overrides Sub Dispose()
    End Sub

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal _docType As Int64, ByVal _indexIDs As String, ByVal _iValue As String)
        MyBase.New(Id, Name, wfstepId)

        RuleID = Id
        DocType = _docType
        IndiceID = _indexIDs
        IValue = _iValue
        playRule = New WFExecution.PlayDoWaitForDocument(Me)
    End Sub
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _ruleID As Int64
    Private _DocType As Int64
    Private _IndiceID As String
    Private _IValue As String
    Private _isValid As Boolean

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

#Region "Propiedades"

    Public Property RuleID() As Integer Implements Core.IDoWaitForDocument.RuleID
        Get
            Return _ruleID
        End Get
        Set(ByVal value As Integer)
            _ruleID = value
        End Set
    End Property

    Public Property DocType() As Integer Implements Core.IDoWaitForDocument.DocType
        Get
            Return _DocType
        End Get
        Set(ByVal value As Integer)
            _DocType = value
        End Set
    End Property

    Public Property IndiceID() As String Implements Core.IDoWaitForDocument.IndiceID
        Get
            Return _IndiceID
        End Get
        Set(ByVal value As String)
            _IndiceID = value
        End Set
    End Property

    Public Property IValue() As String Implements Core.IDoWaitForDocument.IValue
        Get
            Return _IValue
        End Get
        Set(ByVal value As String)
            _IValue = value
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

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return String.Empty
        End Get
    End Property

#End Region

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

End Class