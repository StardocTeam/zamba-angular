Imports ZAMBA.Servers
Imports Zamba.Data
Imports Zamba.Core
Public Class SchedulesComponent
    Public Shared Function GetSchedules(ByVal ObjectTypeId As ObjectTypes, ByVal ObjectId As Int64) As ArrayList
        Return SchedulesBusiness.GetSchedules(ObjectTypeId, ObjectId)
    End Function
    Public Shared Function CheckSchedule(ByVal Schedule As Schedule) As Boolean
        Return SchedulesBusiness.CheckSchedule(Schedule)
    End Function
End Class
