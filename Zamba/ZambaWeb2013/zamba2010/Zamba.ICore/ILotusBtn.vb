Public Interface ILotusBtn
    Inherits IDisposable
    Event Log()
    Function CreateNewButton(ByVal s As Object, ByVal this_db As Object, ByVal sLink As String) As Object
End Interface