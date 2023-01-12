Namespace Analysis
    ''' <summary>
    ''' Representa la interfaz del analisis de una etapa con sus hijos y padre.
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IWfStep(Of t)
        Inherits IHistory(Of IWFStep)

        ''' <summary>
        ''' Representa el porcentaje de tareas que tiene esta etapa en relacion al conjunto de tareas seleccionados.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property TasksPercent() As Double
        ''' <summary>
        ''' Representa el tiempo promedio que tardan las tareas en pasar del la etapa padre a esta.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property TasksTimeSpent() As TimeSpan
        ''' <summary>
        ''' Representa la cantidad de tareas en esta etapa
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property TasksCount() As Int64
    End Interface
End Namespace