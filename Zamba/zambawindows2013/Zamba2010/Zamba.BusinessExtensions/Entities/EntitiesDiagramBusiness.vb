Imports Zamba.Data

Public Class EntitiesDiagramBusiness



    Public Shared Function GetEntitiesByRightType() As ArrayList

        Dim EF As New EntityFactory

        Dim arrEntityList As ArrayList = EF.GetEntitiesByRightType(RightsType.Create)

        Dim arrDiagramList As New ArrayList

        For Each Entity As IDocType In arrEntityList
            Dim ed As New EntityDiagram(Entity)
            arrDiagramList.Add(ed)
        Next
        Return arrDiagramList
    End Function



End Class
