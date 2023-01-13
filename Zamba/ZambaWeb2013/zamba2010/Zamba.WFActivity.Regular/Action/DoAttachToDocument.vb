Imports Zamba.Core
Imports Zamba.WFExecution

<RuleCategory("Documentos Asociados"), RuleDescription("Agregar archivo a Documento"), RuleHelp("Agrega un archivo a un documento con sus indices en blanco."), RuleFeatures(True)> <Serializable()> _
Public Class DoAttachToDocument
    Inherits WFRuleParent
    Implements IDoAttachToDocument


#Region "Atributos"

    Private _docTypeId As Int64
    Private _limitKB As String
    Private _withLimith As Boolean
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _playrule As PlayDoAttachToDocument
    Private _currentSize As String

#End Region

#Region "Constructor"

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal WFStepid As Int64, ByVal doctypeid As Int64, ByVal limitkb As String, ByVal withlimith As Boolean, ByVal currentsize As String)
        MyBase.New(Id, Name, WFStepid)
        Me._docTypeId = doctypeid
        Me._limitKB = limitkb
        Me._withLimith = withlimith
        Me._currentSize = currentsize
        Me._playrule = New PlayDoAttachToDocument(Me)
    End Sub

#End Region

#Region "Metodos Publicos"

    Public Overrides Sub Dispose()

    End Sub

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides Sub Load()

    End Sub

    Public Overrides Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return Me._playrule.Play(results)
    End Function
    Public Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return Me._playrule.Play(results)
    End Function
#End Region


#Region "Propiedades"

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

    Public Property DocTypeId() As Long Implements Core.IDoAttachToDocument.DocTypeId
        Get
            Return Me._docTypeId
        End Get
        Set(ByVal value As Long)
            Me._docTypeId = value
        End Set
    End Property

    Public Property LimitKB() As String Implements Core.IDoAttachToDocument.LimitKB
        Get
            Return Me._limitKB
        End Get
        Set(ByVal value As String)
            Me._limitKB = value
        End Set
    End Property

    Public Property CurrentSize() As String Implements IDoAttachToDocument.CurrentSize
        Get
            Return Me._currentSize
        End Get
        Set(ByVal value As String)
            Me._currentSize = value
        End Set
    End Property

    Public Property WithLimit() As Boolean Implements Core.IDoAttachToDocument.WithLimit
        Get
            Return Me._withLimith
        End Get
        Set(ByVal value As Boolean)
            Me._withLimith = value
        End Set
    End Property

#End Region

End Class
