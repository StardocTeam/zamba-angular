Imports System
Imports System.Reflection
Imports Zamba.Data
Imports Zamba.Core
Imports Zamba.Core.MessagesBusiness
Imports System.Xml.Serialization

<RuleCategory("Documentos Asociados"), RuleDescription("Asociar nuevos formularios"), RuleHelp("Permite agregar un nuevo formulario asociado a un documento"), RuleFeatures(True)> <Serializable()> _
Public Class DoAddAsociatedForm
    Inherits WFRuleParent
    Implements IDoAddAsociatedForm

#Region "Members"
    Private _formID As Long
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _continueWithCurrentTasks As Boolean
    Private _dontOpenTaskAfterInsert As Boolean
    Private _fillCommonAttributes As Boolean
    Private _haveSpecificAttributes As Boolean
    Private _specificAttrubutes As String
#End Region

#Region "Properties"
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

    Public Property FillCommonAttributes As Boolean Implements Core.IDoAddAsociatedForm.FillCommonAttributes
        Get
            Return _fillCommonAttributes
        End Get
        Set(ByVal value As Boolean)
            _fillCommonAttributes = value
        End Set
    End Property

    Public Property ContinueWithCurrentTasks As Boolean Implements Core.IDoAddAsociatedForm.ContinueWithCurrentTasks
        Get
            Return _continueWithCurrentTasks
        End Get
        Set(ByVal value As Boolean)
            _continueWithCurrentTasks = value
        End Set
    End Property

    Public Property DontOpenTaskAfterInsert As Boolean Implements Core.IDoAddAsociatedForm.DontOpenTaskAfterInsert
        Get
            Return _dontOpenTaskAfterInsert
        End Get
        Set(ByVal value As Boolean)
            _dontOpenTaskAfterInsert = value
        End Set
    End Property

    Public Property FormID() As Long Implements IDoAddAsociatedForm.FormID
        Get
            Return _formID
        End Get
        Set(ByVal value As Long)
            _formID = value
        End Set
    End Property
    ''' <summary>
    ''' Marca si la regla utilizara la configuracion para atributos especificos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HaveSpecificAttributes As Boolean Implements Core.IDoAddAsociatedForm.HaveSpecificAttributes
        Get
            Return _haveSpecificAttributes
        End Get
        Set(ByVal value As Boolean)
            _haveSpecificAttributes = value
        End Set
    End Property
    ''' <summary>
    ''' Contiene todos la configuracion de los atributos especificos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SpecificAttrubutes As String Implements Core.IDoAddAsociatedForm.SpecificAttrubutes
        Get
            Return _specificAttrubutes
        End Get
        Set(ByVal value As String)
            _specificAttrubutes = value
        End Set
    End Property
#End Region

#Region "Constructor"
    Public Sub New(ByVal ruleID As Int64, ByVal ruleName As String, ByVal wfstepId As Int64, ByVal FormID As Long, ByVal ContinueWithCurrentTasks As Boolean, ByVal DontOpenTaskAfterInsert As Boolean, _
               ByVal FillCommonAttributes As Boolean, ByVal HaveSpecificAttributes As Boolean, ByVal SpecificAttributes As String)
        MyBase.New(ruleID, ruleName, wfstepId)

        Me._formID = FormID
        Me._continueWithCurrentTasks = ContinueWithCurrentTasks
        Me._dontOpenTaskAfterInsert = DontOpenTaskAfterInsert
        Me._fillCommonAttributes = FillCommonAttributes
        Me._haveSpecificAttributes = HaveSpecificAttributes
        Me._specificAttrubutes = SpecificAttributes
    End Sub
#End Region

#Region "Rule Methods"

    Public Overrides Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoAddAsociatedForm(Me)
        Return playRule.Play(results)
    End Function

    Public Overrides Function PlayWeb(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByRef RulePendingEvent As Core.RulePendingEvents, ByRef ExecutionResult As Core.RuleExecutionResult, ByRef Params As System.Collections.Hashtable) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoAddAsociatedForm(Me)
        If IsNothing(Params) Then
            Params = New Hashtable
        End If
        If IsNothing(Params) OrElse Params.Count = 0 Then
            ExecutionResult = RuleExecutionResult.PendingEventExecution
            RulePendingEvent = RulePendingEvents.ShowDoAsociatedForm
            Return playRule.PlayWeb(results, Me, Params)
        Else
            Params.Clear()
            ExecutionResult = RuleExecutionResult.CorrectExecution
            RulePendingEvent = RulePendingEvents.NoPendingEvent
            Return results
        End If
    End Function


    Public Overrides Sub Dispose()

    End Sub

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides Sub Load()

    End Sub

#End Region
End Class
