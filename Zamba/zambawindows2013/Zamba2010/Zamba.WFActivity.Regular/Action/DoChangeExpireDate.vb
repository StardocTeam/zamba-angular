Imports Zamba.Core

<RuleMainCategory("Tareas"), RuleCategory(""), RuleSubCategory(""), RuleDescription("Modificar Vencimiento"), RuleHelp("Permite modificar el vencimiento de la tarea"), RuleFeatures(False)> <Serializable()> _
Public Class DoChangeExpireDate
    Inherits WFRuleParent
    Implements IDoChangeExpireDate, IRuleValidate

    Private playRule As Zamba.WFExecution.PlayDoChangeExpireDate
    Private _isValid As Boolean
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
    'TODO Martin, completar los parametros

    '''' -----------------------------------------------------------------------------
    '''' <summary>
    '''' 
    '''' </summary>
    '''' <param name="Id"></param>
    '''' <param name="Name"></param>
    '''' <param name="WFStep"></param>
    '''' <param name="Direccion1"></param>
    '''' <param name="Direccion2"></param>
    '''' <param name="Direccion3"></param>
    '''' <param name="Direccion4"></param>
    '''' <param name="Direccion5"></param>
    '''' <param name="Value1"></param>
    '''' <param name="Value2"></param>
    '''' <param name="Value3"></param>
    '''' <param name="Value4"></param>
    '''' <param name="Value5"></param>
    '''' <remarks>
    '''' </remarks>
    '''' <history>
    '''' 	[Hernan]	29/05/2006	Created
    '''' </history>
    '''' -----------------------------------------------------------------------------
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal Direccion1 As Int32, ByVal Direccion2 As Int32, ByVal Direccion3 As Int32, ByVal Direccion4 As Int32, ByVal Direccion5 As Int32, ByVal Value1 As Int32, ByVal Value2 As Int32, ByVal Value3 As Int32, ByVal Value4 As Int32, ByVal Value5 As Int32)
        MyBase.New(Id, Name, wfstepId) ', ListofRules.DoChangeExpireDate)
        _Direccion1 = Direccion1
        _Direccion2 = Direccion2
        _Direccion3 = Direccion3
        _Direccion4 = Direccion4
        _Direccion5 = Direccion5
        _Value1 = Value1
        _Value2 = Value2
        _Value3 = Value3
        _Value4 = Value4
        _Value5 = Value5
        playRule = New WFExecution.PlayDoChangeExpireDate(Me)
    End Sub
    '--ITEMS--
    'StateId=0

    'Properties
    Public Property Direccion1() As Int32 Implements IDoChangeExpireDate.Direccion1
        Get
            Return _Direccion1
        End Get
        Set(ByVal value As Int32)
            _Direccion1 = value
        End Set
    End Property
    Private _Direccion1 As Int32
    Public Property Direccion2() As Int32 Implements IDoChangeExpireDate.Direccion2
        Get
            Return _Direccion2
        End Get
        Set(ByVal value As Int32)
            _Direccion2 = value
        End Set
    End Property
    Private _Direccion2 As Int32
    Public Property Direccion3() As Int32 Implements IDoChangeExpireDate.Direccion3
        Get
            Return _Direccion3
        End Get
        Set(ByVal value As Int32)
            _Direccion3 = value
        End Set
    End Property
    Private _Direccion3 As Int32
    Public Property Direccion4() As Int32 Implements IDoChangeExpireDate.Direccion4
        Get
            Return _Direccion4
        End Get
        Set(ByVal value As Int32)
            _Direccion4 = value
        End Set
    End Property
    Private _Direccion4 As Int32
    Public Property Direccion5() As Int32 Implements IDoChangeExpireDate.Direccion5
        Get
            Return _Direccion5
        End Get
        Set(ByVal value As Int32)
            _Direccion5 = value
        End Set
    End Property
    Private _Direccion5 As Int32

    Public Property Value1() As Int32 Implements IDoChangeExpireDate.Value1
        Get
            Return _Value1
        End Get
        Set(ByVal value As Int32)
            _Value1 = value
        End Set
    End Property
    Private _Value1 As Int32
    Public Property Value2() As Int32 Implements IDoChangeExpireDate.Value2
        Get
            Return _Value2
        End Get
        Set(ByVal value As Int32)
            _Value2 = value
        End Set
    End Property
    Private _Value2 As Int32
    Public Property Value3() As Int32 Implements IDoChangeExpireDate.Value3
        Get
            Return _Value3
        End Get
        Set(ByVal value As Int32)
            _Value3 = value
        End Set
    End Property
    Private _Value3 As Int32
    Public Property Value4() As Int32 Implements IDoChangeExpireDate.Value4
        Get
            Return _Value4
        End Get
        Set(ByVal value As Int32)
            _Value4 = value
        End Set
    End Property
    Private _Value4 As Int32
    Public Property Value5() As Int32 Implements IDoChangeExpireDate.Value5
        Get
            Return _Value5
        End Get
        Set(ByVal value As Int32)
            _Value5 = value
        End Set
    End Property

    Private _Value5 As Int32

    Public Property IsValid As Boolean Implements IRuleValidate.isValid
        Get
            Return _isValid
        End Get
        Set(ByVal value As Boolean)
            _isValid = value
        End Set
    End Property

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)

        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function
    Public Overrides ReadOnly Property MaskName As String
        Get
            Return "Cambio Fecha de Vencimiento"
        End Get
    End Property

End Class