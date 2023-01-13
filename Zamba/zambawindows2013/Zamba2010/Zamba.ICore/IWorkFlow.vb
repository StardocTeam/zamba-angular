
Public Interface IWorkFlow
    Inherits IZambaCore

    Property Description() As String
    Property Help() As String
    Property WFStat() As WFStats
    Property CreateDate() As Date
    Property EditDate() As Date
    Property RefreshRate() As Int32
    Property InitialStep() As IWFStep
    Property Steps() As Dictionary(Of Int64, IWFStep)
    Property InitialStepIdTEMP() As Int64
    Property TasksCount() As Int32
    Property ExpiredTasksCount() As Int32
    Sub SetInitialStep()
End Interface

