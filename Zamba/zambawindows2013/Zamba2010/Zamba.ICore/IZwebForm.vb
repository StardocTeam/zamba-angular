Public Interface IZwebForm
    Inherits IZClass
    Inherits ICore
    Property useRuleRights() As Boolean
    Property DocTypeId() As Int32
    Property ObjectTypeId() As Zamba.Core.IdTypes
    Property ParentId() As Int32
    Property Path() As String
    Property Step_id() As Int32
    Property Type() As FormTypes
    Property Description() As String
    Property EncodedFile() As Byte()
    Property LastUpdate() As DateTime
    Property UseBlob() As Boolean
    ReadOnly Property TempFullPath() As String
    ReadOnly Property TempPathName() As String
End Interface