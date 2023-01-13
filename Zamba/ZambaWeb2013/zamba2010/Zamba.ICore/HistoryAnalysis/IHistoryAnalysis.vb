Imports System.Collections.Generic

Namespace Analysis
    ''' <summary>
    ''' Interfaz generica desarrollada para el analisis del historial de etapas , tareas , workflow, etc.
    ''' </summary>
    ''' <typeparam name="T">Clase a la que va a aplicar el analisis</typeparam>
    ''' <remarks></remarks>
    Public Interface IHistory(Of T)
        Inherits IDisposable

        ''' <summary>
        ''' Los items hijos de una instancia de analisis. 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks>Un ejemplo son las etapas hijas de otra etapa</remarks>
        ReadOnly Property Childs() As List(Of T)
        ''' <summary>
        ''' Valida si la instancia tiene items hijos
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ReadOnly Property HasChilds() As Boolean
        ''' <summary>
        ''' El item padre de una instance de analisis.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks>Si una instancia no tiene padre , esta es el padre.</remarks>
        ReadOnly Property Parent() As T
        ''' <summary>
        ''' Valida si la instancia tiene un item padre
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ReadOnly Property HasParent() As Boolean

    End Interface
End Namespace