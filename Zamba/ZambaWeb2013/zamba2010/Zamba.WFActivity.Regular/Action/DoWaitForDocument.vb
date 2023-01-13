Imports Zamba.Data
Imports Zamba.Core
Imports System.Xml.Serialization

<RuleCategory("Tareas"), RuleDescription("Esperar por Documento"), RuleHelp("Espera por un documento entrante"), RuleFeatures(False)> <Serializable()> _
Public Class DoWaitForDocument
    Inherits WFRuleParent
    Implements IDoWaitForDocument



    Public Overrides Sub Dispose()
    End Sub

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal _docType As Int32, ByVal _indexIDs As String, ByVal _iValue As String)
        MyBase.New(Id, Name, wfstepId)

        Me.RuleID = Id
        Me.DocType = _docType
        Me.IndiceID = _indexIDs
        Me.IValue = _iValue

    End Sub
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _ruleID As Int32
    Private _DocType As Int32
    Private _IndiceID As String
    Private _IValue As String

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

#End Region

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overrides Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoWaitForDocument()
        Return playRule.Play(results, Me)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoWaitForDocument()
        Return playRule.Play(results, Me)
    End Function
End Class
