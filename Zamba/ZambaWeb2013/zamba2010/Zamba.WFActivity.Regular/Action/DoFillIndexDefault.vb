Imports Zamba.Core
Imports System.Text
Imports System.Collections
Imports System.Xml.Serialization

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
<RuleCategory("Datos"), RuleDescription("Completar Indices con Valores Predefinidos"), RuleHelp("Permite ingresar un valor predefinido al indice de forma automatica"), RuleFeatures(False)> <Serializable()> _
Public Class DoFillIndexDefault
    Inherits WFRuleParent
    Implements IDoFillIndexDefault
    Private _index As Index
    Public Overrides Sub Dispose()

    End Sub
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDoFillIndexDefault
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
    Public Property Index() As IIndex Implements IDoFillIndexDefault.Index
        Get
            Return _index
        End Get
        Set(ByVal value As IIndex)
            _index = value
        End Set
    End Property
    Public Property TEXTODEFAULT() As String Implements IDoFillIndexDefault.TEXTODEFAULT
        Get
            Return _TEXTODEFAULT
        End Get
        Set(ByVal value As String)
            _TEXTODEFAULT = value
        End Set
    End Property

    Private _TEXTODEFAULT As String

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal Index_Id As Int32, ByVal TEXTODEFAULT As String)
        MyBase.New(Id, Name, wfstepid)

        Index = ZCore.GetInstance().GetIndex(Index_Id)

        Me._TEXTODEFAULT = TEXTODEFAULT

        Me.playRule = New Zamba.WFExecution.PlayDoFillIndexDefault(Me)
    End Sub

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
End Class