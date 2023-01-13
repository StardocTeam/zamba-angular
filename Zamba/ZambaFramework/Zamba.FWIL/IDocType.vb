Public Interface IDocType
    Inherits IZambaCore

    Property AutoNameCode() As String
    Property AutoNameText() As String
    Property DiskGroupId() As Integer
    Property DocCount() As Int32
    Property DocTypeGroupId() As Int32
    Property DocumentalId() As Int32
    Property FileFormatId() As Integer
    Property TemplateId() As Int32
    Property typeid() As Int32
    ''' <----------------------------------------------------------------------------->
    ''' <summary>
    ''' ArrayList de Objetos indexs
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    ''' </----------------------------------------------------------------------------->
    Property Indexs() As List(Of IIndex)
    ''' <summary>
    ''' coleccion que contiene con ids de atributos y sus valores por defecto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>[Diego]  04-07-2008 created</history>
    Property IndexsDefaultValues() As Dictionary(Of Int64, String)
    Property IsReadOnly() As Boolean
    Property IsReindex() As Boolean
    Property IsShared() As Boolean
    Property RightsLoaded() As Boolean
    Property Thumbnails() As Integer
    Property WorkFlowId() As Int32

    Property SearchTermGroup As Int32
    Property IsSearchTermGroupParent As Boolean
End Interface