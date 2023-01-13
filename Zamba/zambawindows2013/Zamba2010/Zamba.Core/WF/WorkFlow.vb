Imports System.Collections.Generic

''' <summary>
''' PERMITE CREAR OBJETOS WORKFLOWS LOS CUALES REPRESENTAN UN CIRCUITO DE TRABAJO CON SUS ETAPAS Y ESTADOS
''' </summary>
''' <remarks></remarks>
<Serializable()>
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
    Private _steps As Dictionary(Of Int64, IWFStep) = Nothing
    Private _initialStepIdTemp As Int64
    Private _isFull As Boolean
    Private _isLoaded As Boolean
    Private _tasksCount As Int32
    Private _expiredTasksCount As Int32
#End Region

#Region " Propiedades "
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public Property Description() As String Implements IWorkFlow.Description
        Get
            Return _description
        End Get
        Set(ByVal Value As String)
            _description = Value
        End Set
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public Property Help() As String Implements IWorkFlow.Help
        Get
            Return _help
        End Get
        Set(ByVal Value As String)
            _help = Value
        End Set
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public Property WFStat() As WFStats Implements IWorkFlow.WFStat
        Get
            Return _wfStat
        End Get
        Set(ByVal Value As WFStats)
            _wfStat = Value
        End Set
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public Property CreateDate() As Date Implements IWorkFlow.CreateDate
        Get
            Return _createDate
        End Get
        Set(ByVal Value As Date)
            _createDate = Value
        End Set
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public Property EditDate() As Date Implements IWorkFlow.EditDate
        Get
            Return _editDate
        End Get
        Set(ByVal Value As Date)
            _editDate = Value
        End Set
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public Property RefreshRate() As Int32 Implements IWorkFlow.RefreshRate
        Get
            Return _refreshRate
        End Get
        Set(ByVal Value As Int32)
            _refreshRate = Value
        End Set
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
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

    ''' <summary>
    ''' Guarda las etapas de un workflow
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Steps() As Dictionary(Of Int64, IWFStep) Implements IWorkFlow.Steps
        Get
            If IsNothing(_steps) Then CallForceLoad(Me)
            If IsNothing(_steps) Then _steps = New Dictionary(Of Int64, IWFStep)()
            Return _steps
        End Get
        Set(ByVal Value As Dictionary(Of Int64, IWFStep))
            _steps = Value
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
        InitialStepIdTEMP = initialStepId
    End Sub

#End Region

    Public Sub SetInitialStep() Implements IWorkFlow.SetInitialStep
        If Not InitialStepIdTEMP = 0 AndAlso Steps.ContainsKey(InitialStepIdTEMP) Then
            InitialStep = CType(Steps.Item(InitialStepIdTEMP), WFStep)
        End If
    End Sub

    Public Overrides Sub Dispose()
        Dim i As Int16

        If Not IsNothing(_steps) Then
            For i = 0 To _steps.Count - 1
                DirectCast(_steps(i), WFStep).Dispose()
                _steps(i) = Nothing
            Next
            _steps.Clear()
            _steps = Nothing
        End If

        If Not IsNothing(InitialStep) Then
            InitialStep.Dispose()
            InitialStep = Nothing
        End If
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



Public Class WorkflowAdminDto
    Public Property Work_ID As Integer
    Public Property Name As String
    Public Property Right As RightsType
    Public Property Description As String
    Public Property Help As String
    Public Property WStat_Id As Integer
    Public Property CreateDate As DateTime
    Public Property EditDate As DateTime
    Public Property RefreshRate As Integer
    Public Property InitialStepId

    Public Sub New()

    End Sub
    Public Sub New(Work_ID As Integer, Name As String, Right As RightsType)
        Me.Work_ID = Work_ID
        Me.Name = Name
        Me.Right = Right
        Description = Description
        Help = Help
        WStat_Id = WStat_Id
        CreateDate = CreateDate
        EditDate = EditDate
        RefreshRate = RefreshRate
        InitialStepId = InitialStepId
    End Sub

    Public Sub New(Work_ID As Integer, Name As String, Right As RightsType, Description As String, Help As String, WStat_Id As Integer, CreateDate As DateTime, EditDate As DateTime, RefreshRate As Integer, InitialStepId As Integer)
        Me.Work_ID = Work_ID
        Me.Name = Name
        Me.Right = Right
        Me.Description = Description
        Me.Help = Help
        Me.WStat_Id = WStat_Id
        Me.CreateDate = CreateDate
        Me.EditDate = EditDate
        Me.RefreshRate = RefreshRate
        Me.InitialStepId = InitialStepId
    End Sub
End Class