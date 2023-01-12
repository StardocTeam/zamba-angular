

Public Class Schedule
    Implements ISchedule

#Region " Atributos "
    Private _scheduleId As Int32
    Private _objectTypeId As Int32
    Private _objectId As Int64
    Private _scheduleDate As String = String.Empty
    Private _scheduleTime As String = String.Empty
    Private _dayType As DayTypes
#End Region

#Region " Propiedades "
    Public Property ScheduleId() As Int32 Implements ISchedule.ScheduleId
        Get
            Return _scheduleId
        End Get
        Set(ByVal value As Int32)
            _scheduleId = value
        End Set
    End Property
    Public Property ObjectTypeId() As Int32 Implements ISchedule.ObjectTypeId
        Get
            Return _objectTypeId
        End Get
        Set(ByVal value As Int32)
            _objectTypeId = value
        End Set
    End Property
    Public Property ObjectId() As Int64 Implements ISchedule.ObjectId
        Get
            Return _objectId
        End Get
        Set(ByVal value As Int64)
            _objectId = value
        End Set
    End Property
    Public Property ScheduleTime() As String Implements ISchedule.ScheduleTime
        Get
            Return _scheduleTime
        End Get
        Set(ByVal value As String)
            _scheduleTime = value
        End Set
    End Property
    Public Property ScheduleDate() As String Implements ISchedule.ScheduleDate
        Get
            Return _scheduleDate
        End Get
        Set(ByVal value As String)
            _scheduleDate = value
        End Set
    End Property
    Public Property DayType() As DayTypes Implements ISchedule.DayType
        Get
            Return _dayType
        End Get
        Set(ByVal value As DayTypes)
            _dayType = value
        End Set
    End Property
#End Region

#Region " Constructores "
    Private Sub New()
        MyBase.New()
    End Sub
    Public Sub New(ByVal scheduleId As Int32, ByVal objectTypeId As Int32, ByVal objectId As Int32, ByVal scheduleDate As Date, ByVal scheduleTime As Date)
        Me.New()
        Me.ScheduleId = scheduleId
        Me.ObjectTypeId = objectTypeId
        Me.ObjectId = objectId
        scheduleDate = scheduleDate
        scheduleTime = scheduleTime
    End Sub
#End Region

End Class