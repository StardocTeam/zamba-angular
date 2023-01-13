Imports Zamba.Data
Imports Zamba.Core

Public Class VolumeListsBusiness


    Public Event LogError(ByVal ex As Exception)

    Public Shared Function GetDiskGroupsList() As ArrayList
        Return VolumeListsFactory.GetDiskGroupsList()
    End Function

    Public Shared Function getAllLists() As DataSet
        Return VolumeListsFactory.getAllLists()
    End Function

    Public Shared Function GetDiskGroupVolumes(ByVal DiskGroupId As Int32) As DataSet
        Return VolumeListsFactory.GetDiskGroupVolumes(DiskGroupId)
    End Function

    ''' <summary>
    ''' Obtiene un listado de volumenes asociados al id de grupo de volumenes.
    ''' Los datos obtenidos son los siguientes: DISK_VOL_STATE, DISK_VOL_TYPE, DISK_VOL_PATH.
    ''' </summary>
    ''' <param name="DiskGroupId"></param>
    ''' <returns>Los datos obtenidos son los siguientes: DISK_VOL_STATE, DISK_VOL_TYPE, DISK_VOL_PATH.</returns>
    ''' <remarks></remarks>
    Public Shared Function GetDiskGroupData(ByVal DiskGroupId As Int32) As DataTable
        Return VolumeListsFactory.GetDiskGroupData(DiskGroupId)
    End Function

    Public Shared Function IsDuplicate(ByVal name As String) As Boolean
        Return VolumeListsFactory.IsDuplicate(name)
    End Function

    Public Shared Function GetActiveDiskGroupVolumes(ByVal VolumeListId As Int32) As DataSet
        If Not Cache.Volumes.hsVolumesbyVolumeListId.Contains(VolumeListId) Then
            Dim dsList As DataSet = VolumeListsFactory.GetActiveDiskGroupVolumes(VolumeListId)
            SyncLock (Cache.Volumes.hsVolumesbyVolumeListId)
                If Not Cache.Volumes.hsVolumesbyVolumeListId.Contains(VolumeListId) Then
                    Cache.Volumes.hsVolumesbyVolumeListId.Add(VolumeListId, dsList)
                    Return dsList
                End If
            End SyncLock
            Return Cache.Volumes.hsVolumesbyVolumeListId.Item(VolumeListId)
        End If
        Return Cache.Volumes.hsVolumesbyVolumeListId.Item(VolumeListId)
    End Function

    Public Shared Function AddDiskGroup(ByVal DiskGroupName As String) As Int32
        Return VolumeListsFactory.AddDiskGroup(DiskGroupName)
    End Function
    Public Shared Sub DELDiskGroup(ByVal DiskGroupid As Int32)
        VolumeListsFactory.DELDiskGroup(DiskGroupid)
    End Sub

    Public Shared Sub AddtoDiskGroup(ByVal Vols As ArrayList, ByVal DiskGroupId As Int32)
        VolumeListsFactory.AddtoDiskGroup(Vols, DiskGroupId)
    End Sub
    Public Shared Sub DelFromDiskGroup(ByVal VolsId As ArrayList, ByVal DiskGroupId As Int32)
        VolumeListsFactory.DelFromDiskGroup(VolsId, DiskGroupId)
    End Sub

    Public Shared Sub Fill(ByRef volumeList As IVolumeList)

    End Sub
End Class
