Public Interface IDiagramFiltereable
    Function ApplyActorFilter(ByVal actorName As String, ByVal diagramType As DiagramType, ByVal parameters As Object()) As IDiagram
End Interface
