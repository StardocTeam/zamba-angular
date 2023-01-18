Public Interface IDoAddAsociatedDocument

    Inherits IRule
    Property AsociatedDocType() As IDocType
    Property TemplateId() As Int32
    Property Typeid() As OfficeTypes
    Property SelectionId() As Selection
    Property OpenDefaultScreen() As Boolean
    Property DefaultScreenId() As DefaultScreenSelection
    Property DontOpenTaskIfIsAsociatedToWF() As Boolean
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

    Enum Selection As Int32
        Ninguno = 0
        Template = 1
        Documento = 2
    End Enum

    Enum DefaultScreenSelection As Int32
        InsertDocument = 0
        InsertFolder = 1
        ScanDocuments = 2
        InsertForms = 3
    End Enum

End Interface
