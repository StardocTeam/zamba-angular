Public Interface ISchedule
    Property ScheduleId() As Int32
    Property ObjectTypeId() As Int32
    Property ObjectId() As Int64
    Property ScheduleTime() As String
    Property ScheduleDate() As String
    Property DayType() As DayTypes
End Interface