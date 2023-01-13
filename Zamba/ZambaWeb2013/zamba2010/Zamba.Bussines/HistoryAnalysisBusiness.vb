Imports Zamba.Core.Analysis
Imports System.Collections.Generic

''' <summary>
''' Esta clase se encarga de manejar la logica de carga de las clases de analisis del historial
''' </summary>
''' <remarks></remarks>
Public Class HistoryAnalysisBusiness

    ''' <summary>
    ''' Carga el analisis de la etapa y TODAS sus etapas hijas.
    ''' </summary>
    ''' <param name="stepId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' Puse q fuera un list porque tiraba error sino - MC
    '''TODO andres ver este tema
    Public Function LoadStep(ByVal stepId As Int64) As list(Of IWFStep)
        Return Nothing
    End Function

    '''Comentado esto porque no compila
    '''NO existia ITask en Icore
    '''TODO andres ver este tema
    ''' <summary>
    ''' Carga el analisis de la tarea y TODAS sus tareas hijas.
    ''' </summary>
    ''' <param name="taskId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadTask(ByVal taskId As Int64) As ITask
        Return Nothing
    End Function
End Class