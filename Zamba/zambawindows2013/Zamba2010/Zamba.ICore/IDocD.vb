Public Interface IDocD
    Property Index_Id() As Integer
    ''' <----------------------------------------------------------------------------->
    ''' <summary>
    ''' Propiedad. Devuelve o Establece el nombre para un Atributo de Base de Datos
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    ''' </----------------------------------------------------------------------------->
    Property Index_Name() As String
    ''' <----------------------------------------------------------------------------->
    ''' <summary>
    ''' Propiedad. Devuelve un ArrayList con las columnas que conforman el Atributo
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    ''' </----------------------------------------------------------------------------->
    Property Asigned_Columns() As ArrayList
    ''' <----------------------------------------------------------------------------->
    ''' <summary>
    ''' Propiedad. Devuelve las columnas que no conforman el Atributo
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    ''' </----------------------------------------------------------------------------->
    Property No_Asigned_Columns() As ArrayList
    ''' <----------------------------------------------------------------------------->
    ''' <summary>
    ''' Propiedad. Devuelve o Establece si el atributo fue creado
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    ''' </----------------------------------------------------------------------------->
    Property Index_Created() As Boolean
    ''' <----------------------------------------------------------------------------->
    ''' <summary>
    ''' Propiedad. Devuelve o Establece si el atributo fue modificado
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    ''' </----------------------------------------------------------------------------->
    Property Index_Modified() As Boolean
    ''' <----------------------------------------------------------------------------->
    ''' <summary>
    ''' Propiedad. Devuelve o establece el tipo de Atributo, "Unique, Not Null, etc"
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    ''' </----------------------------------------------------------------------------->
    Property Index_type() As IndexsType
End Interface
