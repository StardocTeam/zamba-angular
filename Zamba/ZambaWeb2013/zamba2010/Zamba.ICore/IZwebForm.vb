Public Interface IZwebForm
    Inherits IZClass
    Property useRuleRights() As Boolean
    Property DocTypeId() As Int64
    Property ObjectTypeId() As Zamba.Core.IdTypes
    Property ParentId() As Int32
    Property Path() As String
    Property Step_id() As Int32
    Property Type() As FormTypes
    Property Description() As String
    Property Name() As String
    Property ID() As Int32
    Property ModifiedTime() As DateTime

    Property Rebuild() As Boolean

    Property EncodedFile() As Byte()

    ReadOnly Property TempFullPath() As String
    ReadOnly Property TempPathName() As String
End Interface