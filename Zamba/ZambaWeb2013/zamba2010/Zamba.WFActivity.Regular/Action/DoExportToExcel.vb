Imports Zamba.Core
Imports Zamba.Data
Imports System.IO
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>

<RuleCategory("Importacion"), RuleDescription("Exportar a Excel"), RuleHelp("Genera un archivo de Excel con la tarea actual"), RuleFeatures(False)> <Serializable()> _
Public Class DoExportToExcel
    Inherits WFRuleParent
    Implements IDoExportToExcel
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
    Private m_sRuta As String
    Private m_sStepName As String
    Public Overrides Sub Dispose()

    End Sub
    Public Property Ruta() As String Implements IDoExportToExcel.Ruta
        Get
            Return m_sRuta
        End Get
        Set(ByVal value As String)
            m_sRuta = value
        End Set
    End Property


    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal p_sRuta As String)
        MyBase.New(Id, Name, wfstepid)
        m_sRuta = p_sRuta
        m_sStepName = WFStepBusiness.GetStepNameById(wfstepid)
    End Sub

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoExportToExcel()
        Return playRule.Play(results, Me)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoExportToExcel()
        Return playRule.Play(results, Me)
    End Function
End Class
