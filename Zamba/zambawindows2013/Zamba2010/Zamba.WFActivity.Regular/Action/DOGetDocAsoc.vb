Imports zamba.Core

'Public Interface IDOGetDocAsoc
'    Property tiposDeDocumento() As String
'    Property Variable() As String
'End Interface
''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Business
''' Class	 : Core.DOGetDocAsoc
''' -----------------------------------------------------------------------------
''' <summary>
''' Regla de Zamba WorkFlow
''' </summary>
''' <remarks>
''' Hereda de WFRuleParent
''' </remarks>
''' <history>
''' 	[Patricio]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
<RuleMainCategory("Documentos y Asociados"), RuleCategory("Documentos Asociados"), RuleSubCategory("Obtener Asociados"), RuleDescription("Documentos Asociados"), RuleHelp("Obtiene los documentos asociados a un documento."), RuleFeatures(False)> <Serializable()> _
Public Class DOGetDocAsoc
    'Toda regla de Accion debe derivar de WFRuleParent
    Inherits WFRuleParent
    Implements IDOGetDocAsoc
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDOGetDocAsoc


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

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal TiposDeDocumento As String, ByVal Variable As String, ByVal ContinuarConResultadoObtenido As Boolean)
        MyBase.New(Id, Name, wfstepId) ', ListofRules.DoGetDocAsoc)
        Me.tiposDeDocumento = TiposDeDocumento
        Me.Variable = Variable
        Me.ContinuarConResultadoObtenido = ContinuarConResultadoObtenido
        playRule = New WFExecution.PlayDOGetDocAsoc(Me)
    End Sub

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="results"></param>
    ''' <returns>Sorted list con objetos results</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Pocket]	29/05/2006	Created
    '''     [Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    ''' </history>
    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)

        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function

#Region "Local variables"
    Public Property tiposDeDocumento() As String Implements IDOGetDocAsoc.tiposDeDocumento
        Get
            Return _tiposDeDocumento
        End Get
        Set(ByVal value As String)
            _tiposDeDocumento = value
        End Set
    End Property
    Private _tiposDeDocumento As String = ""
    Public Property Variable() As String Implements IDOGetDocAsoc.Variable
        Get
            Return _variable
        End Get
        Set(ByVal value As String)
            _variable = value
        End Set
    End Property
    Private _variable As String = ""

    Public Property ContinuarConResultadoObtenido() As Boolean Implements IDOGetDocAsoc.ContinuarConResultadoObtenido
        Get
            Return _continuarConResultado
        End Get
        Set(ByVal value As Boolean)
            _continuarConResultado = value
        End Set
    End Property

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return String.Empty
        End Get
    End Property

    Private _continuarConResultado As Boolean
#End Region
End Class