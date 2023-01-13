Imports Zamba.Core
Imports Zamba.WFExecution

<RuleMainCategory("Documentos y Asociados"), RuleCategory("Documentos"), RuleSubCategory(""), RuleDescription("Insertar documento en blob"), RuleHelp("Permite insertar en blob un documento, pasandole el id y el id de la entidad"), RuleFeatures(True)> <Serializable()> _
Public Class DoInsertDocToBlob
    Inherits WFRuleParent
    Implements IDoInsertDocToBlob
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As PlayDoInsertDocToBlob

#Region "WFRuleParent properties"
    Dim _docId As String
    Dim _docTypeId As String

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
    Public Overrides Sub Dispose()

    End Sub
#End Region

    Public Property DocID As String Implements Core.IDoInsertDocToBlob.DocID
        Get
            Return _docId
        End Get
        Set(ByVal value As String)
            _docId = value
        End Set
    End Property

    Public Property DocTypeID As String Implements Core.IDoInsertDocToBlob.DocTypeID
        Get
            Return _docTypeId
        End Get
        Set(ByVal value As String)
            _docTypeId = value
        End Set
    End Property

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return String.Empty
        End Get
    End Property

    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfStepId As Int64, ByVal docId As String, ByVal docTypeId As String)
        MyBase.New(Id, Name, wfStepId)
        Me.DocID = docId
        Me.DocTypeID = docTypeId

        playRule = New Zamba.WFExecution.PlayDoInsertDocToBlob(Me)
    End Sub
End Class
