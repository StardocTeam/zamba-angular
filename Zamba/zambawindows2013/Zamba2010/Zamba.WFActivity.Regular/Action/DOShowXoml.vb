Imports System
Imports System.Collections.Generic
Imports System.Net.Mail
Imports System.Reflection
Imports Zamba.Data
Imports Zamba.Core
Imports Zamba.Core.MessagesBusiness
Imports System.Xml.Serialization


<RuleMainCategory("Workflow"), RuleCategory(""), RuleSubCategory(""), RuleDescription("Dise�ar Archivo Xoml"), RuleHelp("Permite dise�ar un archivo Xoml."), RuleFeatures(True)> <Serializable()> _
Public Class DOShowXoml
    Inherits WFRuleParent
    Implements IDOShowXoml, IRuleValidate
    Private _isLoaded As Boolean
    Private _isFull As Boolean
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

    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
    Public Overrides Sub Dispose()

    End Sub

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal Wfstepid As Int64, ByVal IdAutoMail As Int32, ByVal AddDocument As Boolean, ByVal AddLink As Boolean, ByVal AddIndexs As Boolean, ByVal user As String, ByVal password As String, ByVal port As Integer, ByVal server As String, ByVal indexsNames As String, ByVal groupByMailto As Boolean)
        MyBase.New(Id, Name, Wfstepid)
    End Sub

    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)
        Return results
    End Function
    Public Overloads Overrides Function PlayTest() As Boolean
        Return True
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return New List(Of String)
    End Function
End Class