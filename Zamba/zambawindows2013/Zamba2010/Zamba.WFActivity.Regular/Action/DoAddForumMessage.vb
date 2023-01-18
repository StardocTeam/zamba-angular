Imports System
Imports System.Reflection
Imports Zamba.Data
Imports Zamba.Core
Imports Zamba.Core.MessagesBusiness
Imports System.Xml.Serialization

<RuleMainCategory("Mensajes, Mails y Foro"), RuleCategory("Foro"), RuleSubCategory(""), RuleDescription("Enviar mensaje al foro"), RuleHelp("Permite enviar o visualizar mensajes del foro de un documento asociado."), RuleFeatures(True)> <Serializable()> _
Public Class DoAddForumMessage
    Inherits WFRuleParent
    Implements IDoAddForumMessage, IRuleValidate

    Private playRule As Zamba.WFExecution.PlayDoAddForumMessage
    Public Overrides Sub Dispose()

    End Sub
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _isValid As Boolean

    Public Property IsValid As Boolean Implements IRuleValidate.isValid
        Get
            Return _isValid
        End Get
        Set(ByVal value As Boolean)
            _isValid = value
        End Set
    End Property

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
#Region "Constructores"
    Public Sub New(ByVal ruleID As Int64, ByVal ruleName As String, ByVal wfstepid As Int64)
        MyBase.New(ruleID, ruleName, wfstepid)
        Me.playRule = New WFExecution.PlayDoAddForumMessage(Me)
    End Sub
#End Region


    'Public Overrides Function Play(ByVal results As SortedList) As SortedList
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
            Return "Agrega una nueva conversacion al foro"
        End Get
    End Property
End Class
