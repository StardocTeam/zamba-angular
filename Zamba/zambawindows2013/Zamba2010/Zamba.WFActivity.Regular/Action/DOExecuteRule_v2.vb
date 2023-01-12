Imports Zamba.Core
'Imports Zamba.WFBusiness
Imports System.Xml.Serialization

<RuleCategory("Reglas"), RuleDescription("Ejecutar Regla"), RuleApproved("True"), RuleHelp("Permite seleccionar y ejecutar una regla en el Work Flow actual al momento de realizar la tarea"), RuleFeatures(False)> _
<Serializable()> Public Class DOExecuteRule_v2
    Inherits WFRuleParent
    Implements IDOExecuteRule_v2
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDOExecuteRule_v2
    Private _isRemote As Boolean
    Private _useIdRule As Boolean
    Private _idRule As String
    Public Property IsRemote() As Boolean Implements IDOExecuteRule_v2.IsRemote
        Get
            Return _isRemote
        End Get
        Set(ByVal value As Boolean)
            _isRemote = value
        End Set
    End Property

    Public Property USeIDRule() As Boolean Implements IDOExecuteRule_v2.USeIDRule
        Get
            Return _useIdRule
        End Get
        Set(ByVal value As Boolean)
            Me._useIdRule = value
        End Set
    End Property

    Public Property IdRuleString() As String Implements IDOExecuteRule_v2.IdRuleString
        Get
            Return _idRule
        End Get
        Set(ByVal value As String)
            Me._idRule = value
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
    Public Overrides Sub Dispose()

    End Sub

#Region "campos privados"
    Private _RuleId As Int32
#End Region
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal RuleID As Int32, ByVal IsRemote As Boolean, ByVal useruleid As Boolean, ByVal ruleidstring As String)
        MyBase.New(Id, Name, wfstepid)
        Me._RuleId = RuleID
        Me._isRemote = IsRemote
        Me._useIdRule = useruleid
        Me._idRule = ruleidstring
        Me.playRule = New Zamba.WFExecution.PlayDOExecuteRule_v2(Me)
    End Sub
    Public Property RuleID() As Int32 Implements IDOExecuteRule_v2.RuleID
        Get
            Return _RuleId
        End Get
        Set(ByVal value As Int32)
            _RuleId = value
        End Set
    End Property

    Public Overrides ReadOnly Property MaskName() As String
        Get
            If Me.RuleID <> 0 Then
                'Dim workid As Int64 = WFBusiness.GetWorkflowIdByStepId(WFStepId)
                Return "Ejecutar Regla " & WFRulesBussines.GetRuleNameById(Me.RuleID)
                'Return "Ejecutar Regla " & WFTaskBussines.GetTaskByTaskId(RuleID, workid).Name.Trim 'ver el work_id
            Else
                Return "Ejecutar Regla"
            End If
        End Get
    End Property

    'Public Overrides Function Play(ByVal Results As System.Collections.SortedList) As System.Collections.SortedList
    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
End Class
