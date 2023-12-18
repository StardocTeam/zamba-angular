Public Interface ITaskResult
    Inherits IResult

    Property Exclusive() As Int32
    Property m_AsignedToId() As Int64
    Property AsignedById() As Int64
    Property AsignedDate() As Date
    Property TaskId() As Int64
    Property IndiceXnombre(ByVal IndexName As String) As String
    Property IndiceDescripcion(ByVal IndexName As String) As String
    <Obsolete("Evaluar si vale la pena usar el objeto completo en lugar del id", False)> _
    Property WfStep() As IWFStep
    Property WorkId() As Int64
    Property StepId() As Int64
    Property State() As IWFStepState
    Property CheckIn() As Date
    Property ExpireDate() As Date
    Property TaskState() As Zamba.Core.TaskStates
    Property UserRules() As Hashtable
    ReadOnly Property IsExpired() As Boolean
    Property AsignedToId() As Int64
    Overloads ReadOnly Property Fecha_Fin() As Date
    Overloads ReadOnly Property Fecha_Inicio() As Date
    Function IndexByName(ByVal IndexName As String) As IIndex

    Property Variables(ByVal Nombre As String) As Object
    Property StateId As Long
End Interface