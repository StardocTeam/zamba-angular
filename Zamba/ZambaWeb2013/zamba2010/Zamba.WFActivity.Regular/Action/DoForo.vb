Imports System
Imports System.Collections.Generic
Imports System.Net.Mail
Imports System.Reflection
Imports Zamba.Data
Imports Zamba.Core
Imports System.Xml.Serialization



<RuleCategory("Mensajes"), RuleDescription("Crear Mensaje en Foro"), RuleHelp("Crea un nuevo mensaje en el foro de la tarea."), RuleFeatures(True)> <Serializable()> _
Public Class DoForo
    Inherits WFRuleParent
    Implements IDoForo


    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _body As String
    Private _subject As String
    Private _idMensaje As String
    Private _participantes As String
    Private _automatic As Boolean



    Public Sub New(ByVal ruleID As Int64, ByVal ruleName As String, ByVal wfstepid As Int64, ByVal _subject As String, ByVal _body As String, ByVal _idMensaje As String, ByVal _participantes As String, ByVal _automatic As Boolean)
        MyBase.New(ruleID, ruleName, wfstepid)
        Me.Subject = _subject
        Me.Body = _body
        Me.IdMensaje = _idMensaje
        Me.Participantes = _participantes
        Me._automatic = _automatic
    End Sub


    Public Property Body() As String Implements Core.IDoForo.Body
        Get
            Return Me._body
        End Get
        Set(ByVal value As String)
            Me._body = value
        End Set
    End Property

    Public Property IdMensaje() As String Implements Core.IDoForo.IdMensaje
        Get
            Return Me._idMensaje
        End Get
        Set(ByVal value As String)
            Me._idMensaje = value
        End Set
    End Property
    Public Property Subject() As String Implements Core.IDoForo.Subject
        Get
            Return Me._subject
        End Get
        Set(ByVal value As String)
            Me._subject = value
        End Set
    End Property


    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overrides Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoForo()
        Return playRule.Play(results, Me)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoForo()
        Return playRule.Play(results, Me)
    End Function
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

    Public Property Automatic() As Boolean Implements Core.IDoForo.Automatic
        Get
            Return _automatic
        End Get
        Set(ByVal value As Boolean)
            _automatic = value
        End Set
    End Property

    Public Property Participantes() As String Implements Core.IDoForo.Participantes
        Get
            Return _participantes
        End Get
        Set(ByVal value As String)
            _participantes = value
        End Set
    End Property
End Class
