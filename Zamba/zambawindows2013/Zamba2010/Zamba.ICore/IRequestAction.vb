Public Interface IRequestAction
    Inherits IRule

    Property Id() As Nullable(Of Int64)
    Property FinishDate() As Nullable(Of DateTime)
    Property RequestDate() As DateTime
    Property RequestUserId() As Int64
    Property IsFinished() As Boolean

    ReadOnly Property RulesIds() As List(Of Int64)
    ReadOnly Property TasksAndStepsIds() As Dictionary(Of Int64, Int64)
    ReadOnly Property TaskIds() As List(Of Int64)
    ReadOnly Property StepIds() As List(Of Int64)
    ReadOnly Property UsersIds() As List(Of Int64)

End Interface