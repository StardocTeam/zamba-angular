Imports Zamba.Core
Imports Zamba.Data
Public Class ActionsBusiness
    Public Shared Function GetDocumentActions(ByVal DocumentId As Int64) As DataSet 'DSActions
        Return ActionsFactory.GetDocumentActions(DocumentId)
    End Function

    ''' <summary>
    ''' Devuelve si la tarea fue modificada en el servidor o no desde la fecha pasada como parametro
    ''' </summary>
    ''' <param name="TaskId">Id de la tarea</param>
    ''' <param name="modifiedDate">Fecha de la anterior modificacion</param>
    ''' <history>Marcelo created 26/02/2009</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetIFModifiedDocumentHistoryById(ByVal DocumentId As Int64, ByVal modifiedDate As DateTime) As Boolean
        Dim lastModified As DateTime = ActionsFactory.GetLastModifiedDocumentHistoryById(DocumentId)

        If DateTime.Compare(lastModified, modifiedDate) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetUserActions(ByVal UserId As Int64) As DataSet
        Dim dsHistory As DataSet = ActionsFactory.GetUserActions(UserId)
        Dim accion As String = String.Empty
        'Dado que los nombres de los permisos no se pueden modificar y estos
        'no son los más descriptivos a la hora de mostrar un historial al usuario
        'se modifican directamente los campos realizando los replaces necesarios
        'para facilitar la comprensión de este.
        For Each row As DataRow In dsHistory.Tables(0).Rows
            accion = row.Item("Accion").ToString

            If String.Compare(accion, "insert") = 0 Then
                row.Item("Accion") = "Inserción"
            ElseIf String.Compare(accion, "View") = 0 Then
                row.Item("Accion") = "Visualización"
            ElseIf String.Compare(accion, "InitializeTask") = 0 Then
                row.Item("Accion") = "Inicio de tarea"
            ElseIf String.Compare(accion, "DerivateTask") = 0 Then
                row.Item("Accion") = "Derivar tarea"
            ElseIf String.Compare(accion, "ExecuteRule") = 0 Then
                row.Item("Accion") = "Ejecución de tarea"
            ElseIf String.Compare(accion, "Create") = 0 Then
                row.Item("Accion") = "Creación"
            ElseIf String.Compare(accion, "Delete") = 0 Then
                row.Item("Accion") = "Eliminación"
            ElseIf String.Compare(accion, "Buscar") = 0 Then
                row.Item("Accion") = "Búsqueda"
            ElseIf String.Compare(accion, "AgregarDocumento") = 0 Then
                row.Item("Accion") = "Agregar documento"
            ElseIf String.Compare(accion, "EnviarPorMail") = 0 Then
                row.Item("Accion") = "Envío de mail"
            ElseIf String.Compare(accion, "VerDocumentosAsociados") = 0 Then
                row.Item("Accion") = "Visualización de asociados"
            ElseIf String.Compare(accion, "Usar") = 0 _
            OrElse String.Compare(accion, "Use") = 0 _
            OrElse String.Compare(accion, "Edit") = 0 _
            OrElse String.Compare(accion, "Re Indexar") = 0 _
            OrElse String.Compare(accion, "ReIndex") = 0 Then
                row.Item("Accion") = "Edición"
            End If

            If row.Item("En").ToString.StartsWith("- ") Then
                row.Item("En") = row.Item("En").ToString.Remove(0, 2)
            End If
        Next

        accion = Nothing
        Return dsHistory
    End Function
    Public Shared Sub SaveActioninDB(ByVal ObjectId As Int64, ByVal ObjectType As ObjectTypes, ByVal ActionType As Zamba.Core.RightsType, ByVal S_Object_ID As String, ByVal _userid As Int32, ByVal ConnectionId As Int32)
        ActionsFactory.SaveActioninDB(ObjectId, ObjectType, ActionType, S_Object_ID, _userid, ConnectionId)
    End Sub
    Public Shared Sub CleanExceptions()
        ActionsFactory.CleanExceptions()
    End Sub
    Public Event LogError(ByVal ex As Exception)

End Class
