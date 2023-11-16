Imports Zamba.Core


<RuleMainCategory("Archivos y Aplicaciones"), RuleCategory("Aplicaciones"), RuleSubCategory("Office"), RuleDescription("Generar Excel"), RuleHelp("Genera un documento de excel a partir de la entidad seleccionado"), RuleFeatures(False)> <Serializable()> _
Public Class DoGenerateExcel
    'toda regla de accion debe derivar de WFRuleParent
    Inherits WFRuleParent
    Implements IDoGenerateExcel
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDOGenerateExcel
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
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal DocType As Int64, ByVal Title As String, ByVal Index As String, ByVal footer As String)
        MyBase.New(Id, Name, wfstepid)
        _DocTypeId = DocType
        _Footer = footer
        _Index = Index
        _Title = Title

        playRule = New WFExecution.PlayDOGenerateExcel(Me)
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
    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)

        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
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

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return String.Empty
        End Get
    End Property

    Private _DocTypeId As Int64
    Private _Title As String
    Private _Index As String
    Private _Footer As String
#End Region
End Class