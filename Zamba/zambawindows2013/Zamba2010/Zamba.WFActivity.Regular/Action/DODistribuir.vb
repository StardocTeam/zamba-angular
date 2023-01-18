Imports Zamba.Core

<RuleMainCategory("Tareas"), RuleCategory(""), RuleSubCategory(""), RuleDescription("Distribuir Tarea"), RuleHelp("Permite enviar una tarea a la siguiente etapa u otra cualquiera del Work Flow actual"), RuleFeatures(False)> <Serializable()>
Public Class DoDistribuir
    'toda regla de accion debe derivar de WFRuleParent
    Inherits WFRuleParent
    Implements IDoDistribuir, IRuleValidate

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDODistribuir
    Private _isValid As Boolean
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

    Public Property IsValid As Boolean Implements IRuleValidate.isValid
        Get
            Return _isValid
        End Get
        Set(ByVal value As Boolean)
            _isValid = value
        End Set
    End Property

    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub

    Public Overrides Sub Dispose()
        'Se comenta porque en algun lado esta pasado por referencia
        'Me.playRule = Nothing
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="Id">Id de la regla</param>
    ''' <param name="Name">Nombre para mostrar la regla</param>
    ''' <param name="WFStepid">Etapa Inicial</param>
    ''' <param name="NewWFStepId">Etapa a la cual se quiere distribuir</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    '''
    '''  </history>
    ''' -----------------------------------------------------------------------------
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal NewWFStepId As Long, ByVal SelecCarp As Boolean)
        MyBase.New(Id, Name, wfstepId)
        _NewWFStepID = NewWFStepId
        playRule = New Zamba.WFExecution.PlayDODistribuir(Me)
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo generico que se invoca para ejecutar la regla, este es el punto de entrada
    ''' en la ejecucion de la misma.
    ''' </summary>
    ''' <param name="results"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Martin]	30/05/2006	Created
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
    Public Property NewWFStepId() As Int64 Implements IDoDistribuir.NewWFStepId
        Get
            Return _NewWFStepID
        End Get
        Set(ByVal value As Int64)
            _NewWFStepID = value
        End Set
    End Property
    Private _NewWFStepID As Int64
#End Region

    Public Overrides ReadOnly Property MaskName As String
        Get
            Try
                If NewWFStepId > 0 Then
                    Dim StepName As String = Zamba.Core.WFStepBusiness.GetStepNameById(NewWFStepId)
                    Return "Envio a " & StepName
                Else
                    Return "Distribuir SIN CONFIGURAR"
                End If
            Catch ex As Exception
            End Try
        End Get
    End Property
End Class