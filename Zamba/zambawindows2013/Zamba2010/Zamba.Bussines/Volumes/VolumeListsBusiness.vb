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

    Public Shared Function IsDuplicate(ByVal name As String) As Boolean
        Return VolumeListsFactory.IsDuplicate(name)
    End Function

    'Public Shared Function GetActiveDiskGroupVolumes(ByVal DiskGroupId As Int32, Optional ByRef t As Transaction = Nothing) As DataSet
    '    Return VolumeListsFactory.GetActiveDiskGroupVolumes(DiskGroupId, t)
    'End Function
    Public Shared Function GetActiveDiskGroupVolumes(ByVal DiskGroupId As Int32) As DataSet
        Return VolumeListsFactory.GetActiveDiskGroupVolumes(DiskGroupId)
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
