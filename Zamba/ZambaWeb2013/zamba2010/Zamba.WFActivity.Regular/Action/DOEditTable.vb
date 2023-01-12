Imports Zamba.Core

<RuleCategory("Base de Datos"), RuleDescription("Editar Tabla de Datos"), RuleHelp("Permite editar los datos obtenidos de una variable"), RuleFeatures(False)> <Serializable()> _
Public Class DOEditTable
    Inherits WFRuleParent
    Implements IDOEditTable

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

    Private m_VarSource As String
    Private m_VarDestiny As String
    Private m_KeyColumn As String
    Private m_EditColumn As String
    Private m_EditType As Int64


    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal WFStepid As Int64, ByVal p_VarSource As String, ByVal p_KeyColumn As String, ByVal p_EditColumn As String, ByVal p_EditType As Int64, ByVal p_VarDestiny As String)
        MyBase.New(Id, Name, WFStepid)
        Me.m_VarSource = p_VarSource
        Me.m_KeyColumn = p_KeyColumn
        Me.m_EditColumn = p_EditColumn
        Me.m_EditType = p_EditType
        Me.m_VarDestiny = p_VarDestiny
    End Sub

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDOEDITTABLE()
        Return playRule.Play(results, Me)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDOEDITTABLE()
        Return playRule.Play(results, Me)
    End Function

    Public Property EditColumn() As String Implements IDOEditTable.EditColumn
        Get
            Return Me.m_EditColumn
        End Get
        Set(ByVal value As String)
            Me.m_EditColumn = value
        End Set
    End Property

    Public Property VarDestiny() As String Implements IDOEditTable.VarDestiny
        Get
            Return Me.m_VarDestiny
        End Get
        Set(ByVal value As String)
            Me.m_VarDestiny = value
        End Set
    End Property

    Public Property VarSource() As String Implements IDOEditTable.VarSource
        Get
            Return Me.m_VarSource
        End Get
        Set(ByVal value As String)
            Me.m_VarSource = value
        End Set
    End Property

    Public Property KeyColumn() As String Implements IDOEditTable.KeyColumn
        Get
            Return m_KeyColumn
        End Get
        Set(ByVal value As String)
            m_KeyColumn = value
        End Set
    End Property

    Public Property EditType() As Int64 Implements IDOEditTable.EditType
        Get
            Return m_EditType
        End Get
        Set(ByVal value As Int64)
            m_EditType = value
        End Set
    End Property
End Class