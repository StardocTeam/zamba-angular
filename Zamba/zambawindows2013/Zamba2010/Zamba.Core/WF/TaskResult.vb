Imports System.Collections.Generic

<Serializable()> _
Public Class TaskResult
    Inherits Result
    Implements ITaskResult, IDisposable

#Region " Atributos "
    Private _taskID As Int64
    'Private _wfStep As IWFStep = Nothing
    Private _state As IWFStepState = Nothing
    Private _checkIn As Date = Nothing
    Private _expiredDate As Date = Nothing
    Private _taskState As TaskStates
    Private _workId As Int64
    Private _stepId As Int64
    Private m_userrules As Hashtable = Nothing
    Private _exclusive As Boolean
    Private _asignedById As Int64 = 0
    Private _asignedDate As Date = Nothing
    Private _Variables As Hashtable = New Hashtable()
#End Region

#Region " Propiedades "
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {"Nombre Atributo"})>
    Public ReadOnly Property Atributo(ByVal Nombre As String) As String
        Get
            For Each I As IIndex In Indexs
                If String.Compare(I.Name, Nombre, True) = 0 Then
                    If I.DropDown = IndexAdditionalType.LineText Then
                        Return I.Data
                        'Else
                        'Return I.Data & " - " & I.dataDescription
                    Else
                        'Agregado Diego porque no Obtenia valores para atributos de Sustitucion
                        Return I.Data
                    End If
                End If
            Next
            Return String.Empty
        End Get
    End Property

    Public Property Exclusive() As Boolean Implements ITaskResult.Exclusive
        Get
            Return _exclusive
        End Get
        Set(ByVal value As Boolean)
            _exclusive = value
        End Set
    End Property
    Public Property AsignedById() As Int64 Implements ITaskResult.AsignedById
        Get
            Return _asignedById
        End Get
        Set(ByVal value As Int64)
            _asignedById = value
        End Set
    End Property
    Public Property AsignedDate() As Date Implements ITaskResult.AsignedDate
        Get
            Return _asignedDate
        End Get
        Set(ByVal value As Date)
            _asignedDate = value
        End Set
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public Property TaskId() As Int64 Implements ITaskResult.TaskId
        Get
            Return _taskID
        End Get
        Set(ByVal Value As Int64)
            _taskID = Value
        End Set
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public Property StateId() As Int32
        Get
            Return CType(State.ID, Int32)
        End Get
        Set(ByVal value As Int32)
            State.ID = value
        End Set
    End Property
    ' <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})> _
    'Public Property TareaId() As Int64
    '     Get
    '         Return _taskID
    '     End Get
    '     Set(ByVal Value As Int64)
    '         _taskID = Value
    '     End Set
    ' End Property
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public Property TareaDocumentoId() As Int64
        Get
            Return ID
        End Get
        Set(ByVal Value As Int64)
            ID = Value
        End Set
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public Property Nombre() As String
        Get
            Return Name
        End Get
        Set(ByVal Value As String)
            Name = Value
        End Set
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public Property WorkId() As Int64 Implements ITaskResult.WorkId
        Get
            Return _workId
        End Get
        Set(ByVal value As Int64)
            _workId = value
        End Set
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public Property StepId() As Int64 Implements ITaskResult.StepId
        Get
            Return _stepId
        End Get
        Set(ByVal value As Int64)
            _stepId = value
        End Set
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public Property UsuarioAsignadoId() As Int64
        Get
            Return AsignedToId
        End Get
        Set(ByVal Value As Int64)
            AsignedToId = Value
        End Set
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {"Nombre"})>
    Public Property Variables(ByVal Nombre As String) As Object Implements ITaskResult.Variables
        Get
            If _Variables.Contains(Nombre) Then
                Return _Variables(Nombre)
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal Value As Object)
            If _Variables.Contains(Nombre) Then
                _Variables(Nombre) = Value
            Else
                _Variables.Add(Nombre, Value)
            End If

        End Set
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {"Nombre Atributo"})>
    Public Property IndiceXnombre(ByVal IndexName As String) As String Implements ITaskResult.IndiceXnombre
        Get
            Dim I As IIndex = IndexByName(IndexName)
            If Not IsNothing(I) Then
                Return I.Data
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal Value As String)
            Dim I As IIndex = IndexByName(IndexName)
            If Not IsNothing(I) Then
                I.Data = Value
            End If
        End Set
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {"Nombre Atributo"})>
    Public Property IndiceDescripcion(ByVal IndexName As String) As String Implements ITaskResult.IndiceDescripcion
        Get
            Dim I As IIndex = IndexByName(IndexName)

            Select Case I.DropDown
                Case IndexAdditionalType.AutoSustitución, IndexAdditionalType.AutoSustituciónJerarquico
                    Return I.dataDescription
                Case IndexAdditionalType.DropDown, IndexAdditionalType.AutoSustituciónJerarquico
                    Return I.Data
                Case IndexAdditionalType.LineText
                    Return I.Data
            End Select

            If Not IsNothing(I) Then
                Return I.Data
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal Value As String)
            Dim I As IIndex = IndexByName(IndexName)
            If Not IsNothing(I) Then
                I.Data = Value
            End If
        End Set
    End Property


    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public Property State() As IWFStepState Implements ITaskResult.State
        Get
            If IsNothing(_state) Then CallForceLoad(Me)

            Return _state
        End Get
        Set(ByVal Value As IWFStepState)
            _state = Value
        End Set
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public ReadOnly Property EstadoNombre() As String
        Get
            If IsNothing(_state) Then CallForceLoad(Me)

            Return _state.Name
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public ReadOnly Property EstadoId() As Int32
        Get
            If IsNothing(_state) Then CallForceLoad(Me)

            Return CType(_state.ID, Int32)
        End Get
    End Property

    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public Property CheckIn() As Date Implements ITaskResult.CheckIn
        Get
            Return _checkIn
        End Get
        Set(ByVal Value As Date)
            _checkIn = Value
        End Set
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public Property ExpireDate() As Date Implements ITaskResult.ExpireDate
        Get
            Dim D As Date
            'TODO wf: ver que pasa si se extiende el plazo
            If _expiredDate = Nothing Then
                'D = Me.CheckIn.AddHours(Me.WfStep.MaxHours)
                Return Date.Now()
            Else
                Return _expiredDate
            End If
        End Get
        Set(ByVal Value As Date)
            _expiredDate = Value
        End Set
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public Property TaskState() As TaskStates Implements ITaskResult.TaskState
        Get
            Return _taskState
        End Get
        Set(ByVal Value As TaskStates)
            _taskState = Value
        End Set
    End Property
    Public Property UserRules() As Hashtable Implements ITaskResult.UserRules
        Get
            If IsNothing(m_userrules) Then CallForceLoad(Me)
            If IsNothing(m_userrules) Then m_userrules = New Hashtable()

            Return m_userrules
        End Get
        Set(ByVal value As Hashtable)
            m_userrules = value
        End Set
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public ReadOnly Property IsExpired() As Boolean Implements ITaskResult.IsExpired
        Get
            Try
                If Date.Now > ExpireDate Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                raiseerror(ex)
            End Try
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public Property AsignedToId() As Int64 Implements ITaskResult.AsignedToId


    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public Overloads ReadOnly Property Fecha_Fin() As Date Implements ITaskResult.Fecha_Fin
        Get
            Return ExpireDate
        End Get
    End Property
    <PropiedadesType(Propiedades.PropiedadPublica), PropiedadesReturnType(New String() {""}), PropiedadesParam(New String() {""})>
    Public Overloads ReadOnly Property Fecha_Inicio() As Date Implements ITaskResult.Fecha_Inicio
        Get
            Return CheckIn
        End Get
    End Property

    'Public Overloads ReadOnly Property UserId1() As Integer
    '    Get
    '        Return Me.UserId
    '    End Get
    'End Property
#End Region

#Region " Constructores "
    Public Sub New()
    End Sub
    Public Sub New(ByRef stepId As Int64, ByVal TaskId As Int64, ByVal docID As Int64, ByVal DOCTYPE As DocType, ByVal Name As String, ByVal IconId As Int32, ByVal Exclusive As Int32, ByVal Task_State_ID As TaskStates, ByVal Indexs As List(Of IIndex), ByVal TaskStepState As IWFStepState, Optional ByVal UserAsignedToId As Int64 = 0, Optional ByVal UserAsignedToName As String = "")
        MyBase.New(docID, DOCTYPE, Name, IconId)
        _taskID = TaskId
        ID = docID
        Me.Indexs = Indexs
        Me.StepId = stepId
        Me.Exclusive = Convert.ToBoolean(Exclusive)
        TaskState = Task_State_ID
        AsignedToId = UserAsignedToId
        State = TaskStepState
    End Sub

    Public Sub New(ByRef stepId As Int64, ByVal TaskId As Int64, ByVal docID As Int64, ByVal DOCTYPE As DocType,
                    ByVal Name As String, ByVal IconId As Int32, ByVal Exclusive As Int32,
                   ByVal Task_State_ID As TaskStates, ByVal Indexs As List(Of IIndex), ByVal DiskVolPath As String,
                   ByVal ParentId As String, ByVal Offset As String, ByVal DocFile As String, ByVal DiskGroupId As Int32, ByVal TaskStepState As IWFStepState,
                   Optional ByVal UserAsignedToId As Int64 = 0, Optional ByVal UserAsignedToName As String = "")
        MyBase.New(docID, DOCTYPE, Name, IconId)

        _taskID = TaskId
        ID = docID
        Me.Indexs = Indexs
        Me.StepId = stepId
        Me.Exclusive = Convert.ToBoolean(Exclusive)
        TaskState = Task_State_ID
        AsignedToId = UserAsignedToId
        DISK_VOL_PATH = DiskVolPath
        Parent.ID = ParentId
        Me.OffSet = Offset
        Doc_File = DocFile
        Disk_Group_Id = DiskGroupId

        State = TaskStepState
    End Sub


#End Region

    Public Function IndexByName(ByVal IndexName As String) As IIndex Implements ITaskResult.IndexByName
        For Each I As IIndex In Indexs
            If String.Compare(I.Name.ToLower(), IndexName.ToLower()) = 0 Then
                Return I
            End If
        Next

        Return Nothing
    End Function

    Private _disposed As Boolean
    Public Overrides Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)

        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        'Para evitar que se haga dispose 2 veces
        If Not _disposed Then
            If disposing Then
                Dim i As Int16

                If Not IsNothing(_Variables) Then
                    For i = 0 To _Variables.Count - 1
                        _Variables(i) = Nothing
                    Next
                    _Variables.Clear()
                End If

                'Se comenta porque en algun lado esta pasado por referencia
                'If Not IsNothing(_state) Then
                '    _state.Dispose()
                'End If

                If Not IsNothing(m_userrules) Then
                    For i = 0 To m_userrules.Count - 1
                        m_userrules(i) = Nothing
                    Next
                    m_userrules.Clear()
                End If
            End If

            ' Indicates that the instance has been disposed.
            _disposed = True
            'Se comenta porque en algun lado esta pasado por referencia
            '_state = Nothing
            _Variables = Nothing
            m_userrules = Nothing

            MyBase.Dispose()
        End If
    End Sub
End Class

Public Class GridTaskResult
    Private TaskResult As ITaskResult

    Public Sub New(ByRef wfstepid As Int64, ByVal TaskId As Int64, ByVal docID As Int64, ByVal DOCTYPEId As Int64, ByVal Name As String, ByVal Task_State_ID As TaskStates, ByVal UserAsignedToId As Int64, ByVal StateId As Int64)
        TaskResult = New TaskResult
        TaskResult.ID = docID
        TaskResult.DocTypeId = DOCTYPEId
        TaskResult.Name = Name
        TaskResult.TaskId = TaskId
        TaskResult.StepId = wfstepid
        TaskResult.TaskState = Task_State_ID
        TaskResult.AsignedToId = UserAsignedToId
        _stateId = StateId
    End Sub

    Public Property UserRules() As Hashtable
        Get
            Return TaskResult.UserRules
        End Get
        Set(ByVal value As Hashtable)
            TaskResult.UserRules = value
        End Set
    End Property

    Public Property ID() As Int64
        Get
            Return TaskResult.ID
        End Get
        Set(ByVal value As Int64)
            TaskResult.ID = value
        End Set
    End Property

    Public Property TaskId() As Int64
        Get
            Return TaskResult.TaskId
        End Get
        Set(ByVal value As Int64)
            TaskResult.TaskId = value
        End Set
    End Property

    Public Property doctypeid() As Int64
        Get
            Return TaskResult.DocTypeId
        End Get
        Set(ByVal value As Int64)
            TaskResult.DocTypeId = value
        End Set
    End Property

    Public Property Name() As String
        Get
            Return TaskResult.Name
        End Get
        Set(ByVal value As String)
            TaskResult.Name = value
        End Set
    End Property

    Public Property StepId() As Int64
        Get
            Return TaskResult.StepId
        End Get
        Set(ByVal value As Int64)
            TaskResult.StepId = value
        End Set
    End Property

    Public Property TaskState() As Zamba.Core.TaskStates
        Get
            Return TaskResult.TaskState
        End Get
        Set(ByVal value As Zamba.Core.TaskStates)
            TaskResult.TaskState = value
        End Set
    End Property

    Public Property AsignedToId() As Int64
        Get
            Return TaskResult.AsignedToId
        End Get
        Set(ByVal value As Int64)
            TaskResult.AsignedToId = value
        End Set
    End Property

    Private _stateId As Int64
    Public Property StateId() As Int64
        Get
            Return _stateId
        End Get
        Set(ByVal value As Int64)
            _stateId = value
        End Set
    End Property
End Class