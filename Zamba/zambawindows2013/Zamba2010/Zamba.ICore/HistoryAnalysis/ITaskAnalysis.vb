
Namespace Analysis
    ''' <summary>
    ''' Representa la interfaz del analisis de una tarea con sus hijos y padre.
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface ITask
        Inherits IHistory(Of ITaskResult)

        ''' <summary>
        ''' El estado actual de la tarea
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ReadOnly Property State() As String
        ''' <summary>
        ''' El historial de la tarea
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ReadOnly Property History() As List(Of IRuleNode)
        ''' <summary>
        ''' La etapa en la que se encuentra la tarea
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ReadOnly Property CurrentStep() As IWfStep(Of IWfStep)
        ''' <summary>
        ''' Inserta un nodo al historial de una regla DO
        ''' </summary>
        ''' <param name="node"></param>
        ''' <remarks></remarks>
        Sub AddHistoryNode(ByVal node As IDoRuleNodeHistory)
        ''' <summary>
        ''' Inserta un nodo al historial de una regla IF
        ''' </summary>
        ''' <param name="node"></param>
        ''' <remarks></remarks>
        Sub AddHistoryNode(ByVal node As IIfRuleNodeHistory)
    End Interface
End Namespace