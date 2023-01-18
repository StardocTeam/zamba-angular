Public Interface IDoAddAsociatedForm
    Inherits IRule

    Property FormID() As Long
    Property ContinueWithCurrentTasks() As Boolean
    Property DontOpenTaskAfterInsert() As Boolean
    Property FillCommonAttributes() As Boolean
    ''' <summary>
    ''' Marca si la regla utilizara la configuracion para atributos especificos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property HaveSpecificAttributes() As Boolean
    ''' <summary>
    ''' Contiene todos la configuracion de los atributos especificos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SpecificAttrubutes() As String

End Interface
