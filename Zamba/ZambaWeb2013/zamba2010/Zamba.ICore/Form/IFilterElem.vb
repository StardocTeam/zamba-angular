Public Interface IFilterElem
    Property Id() As Int64
    ReadOnly Property Filter() As String
    ReadOnly Property Description() As String
    ReadOnly Property DataType() As IndexDataType
    ReadOnly Property Value() As String
    ReadOnly Property NullValue() As Boolean
    ReadOnly Property Comparator() As String
    ReadOnly Property Type() As String
    ReadOnly Property Text() As String
    Property Enabled() As [Boolean]
    Overloads Function ToString() As String
    Property DocTypeId() As Int64
    Property UserId() As Int64
    Property DataDescription() As String


    ''' <summary>
    ''' Formatea el valor a filtrar para que se realize el filtrado de manera correcta.
    ''' </summary>
    ''' <remarks>
    ''' La razón de que primero se remueven los espacios entre las comas y luego las comillas simples
    ''' es porque si el usuario desea filtrar algo como " Nombre de Usuario  " no podría.
    ''' Por eso primero se remueven los espacios, luego las posibles comillas (ya en este momento el
    ''' valor a filtrar queda definido) y luego se agregan las comillas simples entre los valores.
    ''' </remarks>
    ''' <param name="column"></param>
    ''' <param name="val">Valores múltiples a filtrar sin formatear</param>
    ''' <param name="indexType" />
    ''' <param name="compareComparator"></param>
    ''' <returns>Valores múltiples formateados</returns>
    Function FormatMultipleValues(ByVal column As [String], ByVal val As String, ByVal indexType As IndexDataType, ByVal compareComparator As [String]) As String
End Interface
