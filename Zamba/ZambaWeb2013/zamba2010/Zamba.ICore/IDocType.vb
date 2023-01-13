Public Interface IDocType
    Inherits IZambaCore

    Property AutoNameCode() As String
    Property AutoNameText() As String
    Property DiskGroupId() As Integer
    Property DocTypeGroupId() As Int32
    Property FileFormatId() As Integer
    Property TemplateId() As Int32
    Property typeid() As Int32
    Property Indexs() As ArrayList
    Property IndexsDefaultValues() As Dictionary(Of Int64, String)
    Property IsReadOnly() As Boolean
    Property Thumbnails() As Integer
    Property WorkFlowId() As Int32

    Property SearchTermGroup As Int32
    Property IsSearchTermGroupParent As Boolean
End Interface