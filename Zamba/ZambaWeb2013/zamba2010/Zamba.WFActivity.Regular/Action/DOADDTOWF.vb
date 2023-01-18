Imports Zamba.Core
Imports System.Xml.Serialization
Imports Zamba.Data

<RuleCategory("Workflow"), RuleDescription("Agregar a Work Flow"), RuleHelp("Permite agregar un documento al workflow."), RuleFeatures(False)> <Serializable()> _
Public Class DOADDTOWF
    Inherits WFRuleParent
    Implements IDOADDTOWF
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
    Public Overrides Sub Dispose()

    End Sub
    Private Sub New()
        MyBase.New(0, "", 0)
    End Sub

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfStepID As Int64, ByVal p_WorkId As Int32)
        ' La variable wfStepId no es la etapa inicial del workflow (p_WorkId) al que se quiere agregar la tarea
        ' Corresponde al workflow en donde está contenido actualmente el documento y la regla
        MyBase.New(Id, Name, wfStepID)
        m_iWorkId = p_WorkId
    End Sub

    'Properties
    Private m_iWorkId As Int32

    Public Function WorkFlowName() As String Implements IDOADDTOWF.WorkFlowName
        Return New WFFactory().GetWfNameById(Me.m_iWorkId)
    End Function

    Public Property WorkId() As Int32 Implements IDOADDTOWF.WorkId
        Get
            Return m_iWorkId
        End Get
        Set(ByVal value As Int32)
            m_iWorkId = value
        End Set
    End Property

    'Public ReadOnly Property RuleWFStep() As IWFStep Implements IDOADDTOWF.WFStep
    '    Get
    '        Return Me.WFStep
    '    End Get
    'End Property

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        'Dim o As New ArrayList(0)
        Dim playRule As New Zamba.WFExecution.PlayDOADDTOWF()
        Return playRule.Play(results, Me)
    End Function

    Public Overloads Overrides Function PlayWeb(ByVal results As List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDOADDTOWF()
        Return playRule.Play(results, Me)
    End Function
End Class
