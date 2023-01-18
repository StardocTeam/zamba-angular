Imports Zamba.Core

<RuleCategory("Secciones"), RuleDescription("Administrar archivos"), RuleHelp("Permite recorrer "), RuleFeatures(False)> <Serializable()> _
Public Class DoAdminFiles
    Inherits WFRuleParent
    Implements IDoAdminFiles

#Region "Atributos y propiedades"
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _maskName As String
    Private _sourceVar As String
    Private _action As FileActions
    Private _targetPath As String
    Private _errorVar As String
    Private _output As FWDataTypes
    Private _deleteVarFiles As Boolean
    Private _overwrite As Boolean
    Private playRule As Zamba.WFExecution.PlayDoAdminFiles

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

    Public Property Action() As Core.FileActions Implements Core.IDoAdminFiles.Action
        Get
            Return _action
        End Get
        Set(ByVal value As Core.FileActions)
            _action = value
        End Set
    End Property
    Public Property ErrorVar() As String Implements Core.IDoAdminFiles.ErrorVar
        Get
            Return _errorVar
        End Get
        Set(ByVal value As String)
            _errorVar = value
        End Set
    End Property
    Public Property SourceVar() As String Implements Core.IDoAdminFiles.SourceVar
        Get
            Return _sourceVar
        End Get
        Set(ByVal value As String)
            _sourceVar = value
        End Set
    End Property
    Public Property TargetPath() As String Implements Core.IDoAdminFiles.TargetPath
        Get
            Return _targetPath
        End Get
        Set(ByVal value As String)
            _targetPath = value
        End Set
    End Property
    Public Property OutputDataType() As Core.FWDataTypes Implements Core.IDoAdminFiles.OutputDataType
        Get
            Return _output
        End Get
        Set(ByVal value As Core.FWDataTypes)
            _output = value
        End Set
    End Property
    Public Property DeleteVarFiles() As Boolean Implements Core.IDoAdminFiles.DeleteVarFiles
        Get
            Return _deleteVarFiles
        End Get
        Set(ByVal value As Boolean)
            _deleteVarFiles = value
        End Set
    End Property
    Public Property Overwrite() As Boolean Implements Core.IDoAdminFiles.Overwrite
        Get
            Return _overwrite
        End Get
        Set(ByVal value As Boolean)
            _overwrite = value
        End Set
    End Property
#End Region

#Region "Constructores"
    Private Sub New()
        MyBase.New(0, "", 0)
    End Sub

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfStepId As Int64, ByVal sourceVar As String, ByVal action As FileActions, ByVal targetPath As String, ByVal errorVar As String, ByVal output As FWDataTypes, ByVal deleteVarFiles As Boolean, ByVal overwrite As Boolean)
        MyBase.New(Id, Name, wfStepId)

        Me._sourceVar = sourceVar
        Me._action = action
        Me._targetPath = targetPath
        Me._errorVar = errorVar
        Me._output = output
        Me._deleteVarFiles = deleteVarFiles
        Me._overwrite = overwrite

        playRule = New Zamba.WFExecution.PlayDoAdminFiles(Me)
    End Sub
#End Region

#Region "Métodos"
    Public Overrides Sub Load()

    End Sub
    Public Overrides Sub Dispose()

    End Sub
    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return playRule.Play(results)
    End Function
    Public Overloads Overrides Function PlayWeb(ByVal results As List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
#End Region

End Class
