Public Interface IDiagramCategoryFilter
    Function ApplyCategoryRuleFilter(ByVal category As Int32, ByVal diagramType As DiagramType, ByVal parameters As Object()) As IDiagram
End Interface
