Public Interface ISectionDiagram
    Inherits ICore
    Property IconId() As Int32
    Property ParentSectionId() As Int32
    Property ObjectTypeId() As Int32
    Property ChildSection() As List(Of ISectionDiagram)
    Property EntitiesSection() As List(Of IEntityDiagram)

End Interface
