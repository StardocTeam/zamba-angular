Public Class TaskDTO

    Public Sub New(Tarea As String, Task_id As Int64, doc_id As Int64, DOC_TYPE_ID As Int64, Fecha As DateTime, Etapa As String, Asignado As String, Ingreso As DateTime, Vencimiento As DateTime)
        Me.Tarea = Tarea
        Me.Task_id = Task_id
        Me.doc_id = doc_id
        Me.DOC_TYPE_ID = DOC_TYPE_ID
        Me.Fecha = Fecha
        Me.Etapa = Etapa
        Me.Asignado = Asignado
        Me.Ingreso = Ingreso
        Me.Vencimiento = Vencimiento
    End Sub

    Public Property Tarea As String
    Public Property Task_id As Long
    Public Property doc_id As Long
    Public Property DOC_TYPE_ID As Long
    Public Property Fecha As Date
    Public Property Etapa As String
    Public Property Asignado As String
    Public Property Ingreso As Date
    Public Property Vencimiento As Date
End Class
