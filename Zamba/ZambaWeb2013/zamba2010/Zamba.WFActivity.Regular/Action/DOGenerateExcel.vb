Imports Zamba.Data
Imports Zamba.Core
Imports System.Xml.Serialization


<RuleCategory("Aplicaciones"), RuleDescription("Generar Excel"), RuleHelp("Genera un documento de excel a partir del entidad seleccionado"), RuleFeatures(False)> <Serializable()> _
Public Class DoGenerateExcel
    'toda regla de accion debe derivar de WFRuleParent
    Inherits WFRuleParent
    Implements IDoGenerateExcel
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
    '''' -----------------------------------------------------------------------------
    '''' <summary>
    '''' Constructor
    '''' </summary>
    '''' <param name="Id">Id de la regla</param>
    '''' <param name="Name">Nombre para mostrar la regla</param>
    ''''' <param name="WFStep">Etapa Inicial</param>
    '''' <param name="DocType">DocType id</param>
    '''' <param name="footer">Footer of the document</param>
    '''' <param name="Index">Indexs of the doctype divided by "//"</param>
    '''' <param name="Title">Title of the document</param>
    '''' <remarks>
    '''' </remarks>
    '''' <history>
    '''' 	[Marcelo]	05/03/2007	Created
    '''' </history>
    '''' -----------------------------------------------------------------------------
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal DocType As Int32, ByVal Title As String, ByVal Index As String, ByVal footer As String)
        MyBase.New(Id, Name, wfstepid)
        Me._DocTypeId = DocType
        Me._Footer = footer
        Me._Index = Index
        Me._Title = Title
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Write the trace and generate the fact.
    ''' </summary>
    ''' <param name="results"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Marcelo]	23/02/2007	Created
    '''     [Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDOGenerateExcel()
        Return playRule.Play(results, Me)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDOGenerateExcel()
        Return playRule.Play(results, Me)
    End Function

#Region "Local properties"
    Public Property DocTypeId() As Int32 Implements IDoGenerateExcel.DocTypeId
        Get
            Return _DocTypeId
        End Get
        Set(ByVal value As Int32)
            _DocTypeId = value
        End Set
    End Property
    Public Property Title() As String Implements IDoGenerateExcel.Title
        Get
            Return _Title
        End Get
        Set(ByVal value As String)
            _Title = value
        End Set
    End Property
    Public Property Index() As String Implements IDoGenerateExcel.Index
        Get
            Return _Index
        End Get
        Set(ByVal value As String)
            _Index = value
        End Set
    End Property
    Public Property Footer() As String Implements IDoGenerateExcel.Footer
        Get
            Return _Footer
        End Get
        Set(ByVal value As String)
            _Footer = value
        End Set
    End Property

    Private _DocTypeId As Int32
    Private _Title As String
    Private _Index As String
    Private _Footer As String
#End Region
End Class