Imports Zamba.Core
Imports System.Text

''' Project	 : Zamba.Business
''' Class	 : Core.DoFillIndex
''' <summary>
''' Regla para completar Indices
''' </summary>
''' <remarks>
''' Hereda WFRuleParent
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
<RuleCategory("Datos"), RuleDescription("Completar Indices"), RuleHelp("Permite completar indices con valores sin interaccion con el usuario"), RuleFeatures(False)> <Serializable()> _
Public Class DoFillIndex
    Inherits WFRuleParent
    Implements IDoFillIndex
    Private _index As Index
    Private _indexId As String
    Private _primaryValue As String
    Private _secondaryValue As String
    Private _OverWriteIndex As String
    Private playRule As Zamba.WFExecution.PlayDoFillIndex

    Public Overrides Sub Dispose()

    End Sub

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
    Public Property OverWriteIndex() As String Implements IDoFillIndex.OverWriteIndex
        Get
            Return _OverWriteIndex
        End Get
        Set(ByVal value As String)
            _OverWriteIndex = value
        End Set
    End Property
    Public Property Index() As IIndex Implements IDoFillIndex.Index
        Get
            Return _index
        End Get
        Set(ByVal value As IIndex)
            _index = value
        End Set
    End Property

    Public Property IndexId() As String Implements IDoFillIndex.IndexId
        Get
            Return _indexId
        End Get
        Set(ByVal value As String)
            _indexId = value
        End Set
    End Property

    Public Property PrimaryValue() As String Implements IDoFillIndex.PrimaryValue
        Get
            Return _primaryValue
        End Get
        Set(ByVal value As String)
            _primaryValue = value
        End Set
    End Property

    Public Property SecondaryValue() As String Implements IDoFillIndex.SecondaryValue
        Get
            Return _secondaryValue
        End Get
        Set(ByVal value As String)
            _secondaryValue = value
        End Set
    End Property

    Public Sub ClearRule() Implements IDoFillIndex.ClearRule
        Me.IndexId = String.Empty
        Me.PrimaryValue = String.Empty
        Me.SecondaryValue = String.Empty
        Me.OverWriteIndex = String.Empty
    End Sub

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal Index_Id As String, ByVal primaryValue As String, ByVal secondaryValue As String, ByVal OverWrite As String)
        MyBase.New(Id, Name, wfstepid)

        Me.Index = New Index()
        Me.IndexId = Index_Id
        Me.PrimaryValue = primaryValue
        Me.SecondaryValue = secondaryValue
        Me.OverWriteIndex = OverWrite

        Me.playRule = New Zamba.WFExecution.PlayDoFillIndex(Me)
    End Sub

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    'Public Property Value() As String Implements Core.IDoFillIndex.Value
    '    Get

    '    End Get
    '    Set(ByVal value As String)

    '    End Set
    'End Property

    'Public Property Value() As String Implements Core.IDoFillIndex.SecondaryValue
    '    Get

    '    End Get
    '    Set(ByVal value As String)

    '    End Set
    'End Property

End Class