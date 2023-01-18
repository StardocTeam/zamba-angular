Imports Zamba.Core
Imports Zamba.Data
Imports Zamba.coreDs

Public Class DocTypeBusinessExt
    ''' <summary>
    ''' Obtiene el ID de las entidades que utilizan el volumen
    ''' </summary>
    ''' <param name="DiskGroupID"></param>
    ''' <history>Marcelo Created 14/12/12</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetDocTypesIdsByVolumeID(ByVal VolumeID As Int64) As List(Of Int64)
        Dim docTypesIds As New List(Of Int64)
        Dim EF As New EntityFactory
        Dim ds As DataSet = EF.GetDocTypesIdsByDiskGroupID(VolumeID)

        EF = Nothing

        For Each dr As DataRow In ds.Tables(0).Rows
            docTypesIds.Add(dr(0))
        Next

        Return docTypesIds
    End Function
End Class