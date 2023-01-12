Public Enum DiagramType As Integer
    SiteMap = 0
    Workflows = 1
    Entities = 2
    StepActions = 3
    Actors = 4
    Forms = 5
    Environment = 6
    WorkflowSteps = 7
    WorkflowEntitiesRelations = 8
    Search = 9
    Insert = 10
    Tasks = 11
    Home = 12
    DiagramType = 13
    WorkFlowRules = 14
    Reports = 16
    Interfaces = 15
    DocType = 17
    DER = 18
    EntityForms = 19
End Enum

Public Class DiagramTypeName
    Public Shared Function GetTypeName(ByVal tipo As DiagramType) As String
        Select Case tipo
            Case DiagramType.SiteMap
                Return "Mapa de Sitio"
            Case DiagramType.Workflows
                Return "Flujos de Trabajo"
            Case DiagramType.Entities, DiagramType.DocType
                Return "Entidades"
            Case DiagramType.StepActions
                Return "Acciones de Etapa: "
            Case DiagramType.Actors
                Return "Actores"
            Case DiagramType.Forms
                Return "Formulario: "
            Case DiagramType.Environment
                Return "Ambiente"
            Case DiagramType.WorkflowSteps
                Return "Etapa: "
            Case DiagramType.WorkflowEntitiesRelations
                Return "Relacion Entidad Flujo de Trabajo"
            Case DiagramType.Search
                Return "Busqueda"
            Case DiagramType.Insert
                Return "Insercion"
            Case DiagramType.DiagramType
                Return "Tipo de Diagrama"
            Case DiagramType.WorkFlowRules
                Return "Regla: "
            Case DiagramType.Home
                Return "Inicio"
            Case DiagramType.Reports
                Return "Reportes"
            Case DiagramType.Interfaces
                Return "Interfaces"
            Case DiagramType.DER
                Return "Diagrama Entidad Relación: "
            Case DiagramType.EntityForms
                Return "Formularios asociados con: "
            Case Else
                Return "Nuevo Tab"
        End Select
    End Function
End Class
