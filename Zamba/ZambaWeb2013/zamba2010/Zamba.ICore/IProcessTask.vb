Public Interface IProcessTask
    Property Id() As Integer
    Property ProcessGroup() As String
    Property Hora() As String
    Property Dia() As DayOfWeek
    Property Maquina() As String
    Property UserId() As Integer
    Function CompareTimes() As Boolean
End Interface