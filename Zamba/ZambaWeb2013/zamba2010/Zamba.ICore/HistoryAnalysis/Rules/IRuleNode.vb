Namespace Analysis
    ''' <summary>
    ''' Representa un nodo de una regla en la ejecucion del workflow
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IRuleNode
        ''' <summary>
        ''' El nomnbre de la regla
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ReadOnly Property Name() As String
        ''' <summary>
        ''' El tipo de la regla
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ReadOnly Property Type() As String
        ''' <summary>
        ''' Las tareas que
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>

    End Interface
End Namespace