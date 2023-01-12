Imports Zamba.Data
Imports Zamba.Core
''Imports Zamba.WFBusiness
Imports System.Xml.Serialization
''' <summary>
'''  Genera un Id y completa un indice determinado.
''' </summary>
''' <remarks></remarks>
<RuleCategory("Datos"), RuleDescription("Generar Id"), RuleHelp("Genera un nuevo id e ingresa el nuevo valor del indice en el entidad seleccionado"), RuleFeatures(False)> <Serializable()> _
Public Class DoGetNewId
    Inherits WFRuleParent
    Implements IDoGetNewId
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
    Private m_iDocTypeId As Int32
    Private m_iIndexId As Int32
    Public Overrides Sub Dispose()

    End Sub
#Region "Constructor"
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal p_iDocTypeId As Int32, ByVal p_iIndexId As Int32)
        MyBase.New(Id, Name, wfstepid)
        m_iDocTypeId = p_iDocTypeId
        m_iIndexId = p_iIndexId
    End Sub
#End Region

    Public Property DocTypeId() As Int32 Implements IDoGetNewId.DocTypeId
        Get
            Return m_iDocTypeId
        End Get
        Set(ByVal value As Int32)
            m_iDocTypeId = value
        End Set
    End Property

    Public Property IndexId() As Int32 Implements IDoGetNewId.IndexId
        Get
            Return m_iIndexId
        End Get
        Set(ByVal value As Int32)
            m_iIndexId = value
        End Set
    End Property

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoGetNewId()
        Return playRule.Play(results, Me)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoGetNewId()
        Return playRule.Play(results, Me)
    End Function
End Class
