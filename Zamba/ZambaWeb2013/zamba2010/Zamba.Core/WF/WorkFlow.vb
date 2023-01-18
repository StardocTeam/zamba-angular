''' <summary>
''' PERMITE CREAR OBJETOS WORKFLOWS LOS CUALES REPRESENTAN UN CIRCUITO DE TRABAJO CON SUS ETAPAS Y ESTADOS
''' </summary>
''' <remarks></remarks>
<Serializable()> _
Public Class WorkFlow
    Inherits ZambaCore
    Implements IWorkFlow

#Region " Atributos "
    Private _description As String = String.Empty
    Private _help As String = String.Empty
    Private _wfStat As WFStats = Nothing
    Private _createDate As Date = Nothing
    Private _editDate As Date = Nothing
    Private _refreshRate As Int32 = 5
    Private _initialStep As IWFStep = Nothing
    Private _steps As SortedList = Nothing
    Private _initialStepIdTemp As Int64
    Private _isFull As Boolean
    Private _isLoaded As Boolean
    Private _tasksCount As Int32
    Private _expiredTasksCount As Int32
#End Region

#Region " Propiedades "
    <PropiedadesType(Propiedades.PropiedadPublica)> _
    Public Property Description() As String Implements IWorkFlow.Description
        Get
            Return Me._description
        End Get
        Set(ByVal Value As String)
            Me._description = Value
        End Set
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)> _
    Public Property Help() As String Implements IWorkFlow.Help
        Get
            Return _help
        End Get
        Set(ByVal Value As String)
            Me._help = Value
        End Set
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)> _
    Public Property WFStat() As WFStats Implements IWorkFlow.WFStat
        Get
            Return _wfStat
        End Get
        Set(ByVal Value As WFStats)
            _wfStat = Value
        End Set
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)> _
    Public Property CreateDate() As Date Implements IWorkFlow.CreateDate
        Get
            Return Me._createDate
        End Get
        Set(ByVal Value As Date)
            Me._createDate = Value
        End Set
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)> _
    Public Property EditDate() As Date Implements IWorkFlow.EditDate
        Get
            Return Me._editDate
        End Get
        Set(ByVal Value As Date)
            Me._editDate = Value
        End Set
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)> _
    Public Property RefreshRate() As Int32 Implements IWorkFlow.RefreshRate
        Get
            Return _refreshRate
        End Get
        Set(ByVal Value As Int32)
            _refreshRate = Value
        End Set
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica)> _
    Public Property InitialStep() As IWFStep Implements IWorkFlow.InitialStep
        Get
            If IsNothing(_initialStep) Then CallForceLoad(Me)
            If IsNothing(_initialStep) Then _initialStep = New WFStep()

            Return _initialStep
        End Get
        Set(ByVal Value As IWFStep)
            _initialStep = Value
        End Set
    End Property
    Public Property Steps() As SortedList Implements IWorkFlow.Steps
        Get
            If IsNothing(_steps) Then CallForceLoad(Me)
            If IsNothing(_steps) Then _steps = New SortedList()
            Return _steps
        End Get
        Set(ByVal Value As SortedList)
            Me._steps = Value
        End Set
    End Property
    Public Property InitialStepIdTEMP() As Int64 Implements IWorkFlow.InitialStepIdTEMP
        Get
            Return _initialStepIdTemp
        End Get
        Set(ByVal value As Int64)
            _initialStepIdTemp = value
        End Set
    End Property
    Public Property TasksCount() As Int32 Implements IWorkFlow.TasksCount
        Get
            Return _tasksCount
        End Get
        Set(ByVal value As Int32)
            _tasksCount = value
        End Set
    End Property
    Public Property ExpiredTasksCount() As Int32 Implements IWorkFlow.ExpiredTasksCount
        Get
            Return _expiredTasksCount
        End Get
        Set(ByVal value As Int32)
            _expiredTasksCount = value
        End Set
    End Property
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
#End Region

#Region " Constructores "
    Public Sub New()

    End Sub

    Public Sub New(ByVal id As Int64)
        Me.New()
        Me.ID = id
    End Sub

    Public Sub New(ByVal id As Int32, ByVal name As String, ByVal description As String, ByVal help As String)
        Me.New(id)
        Me.Name = name
        Me.Description = description
        Me.Help = help
    End Sub

    Public Sub New(ByVal id As Int32, ByVal name As String, ByVal description As String, ByVal help As String, ByVal wfStat As WFStats, ByVal createDate As Date, ByVal editDate As Date, ByVal refreshRate As Int32, ByVal initialStepId As Int32)
        Me.New(id, name, description, help)
        Me.WFStat = wfStat
        Me.CreateDate = createDate
        Me.EditDate = editDate
        Me.RefreshRate = refreshRate
        Me.InitialStepIdTEMP = initialStepId
    End Sub

#End Region

    Public Sub SetInitialStep() Implements IWorkFlow.SetInitialStep
        If Not InitialStepIdTEMP = 0 Then
            Me.InitialStep = CType(Steps.Item(InitialStepIdTEMP), WFStep)
        End If
    End Sub

    Public Overrides Sub Dispose()

    End Sub
    Public Overrides Sub FullLoad()
        If Not _isFull Then
            CallForceLoad(Me)
        End If

    End Sub
    Public Overrides Sub Load()
        If Not _isLoaded Then
            CallForceLoad(Me)
        End If
    End Sub

End Class