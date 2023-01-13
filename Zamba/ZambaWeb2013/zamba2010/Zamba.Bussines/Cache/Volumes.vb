Namespace Cache
    Public Class Volumes
        Public Shared hsVolumeTypes As New SynchronizedHashtable
        Public Shared Property hsVolumesbyVolumeListId As New SynchronizedHashtable
        Public Shared Property hsVolumesListIdbyEntityId As New SynchronizedHashtable
        Public Shared Property hsVolumesbyVolumeId As New SynchronizedHashtable
        Friend Shared Sub RemoveCurrentInstance()
            hsVolumeTypes.Clear()
            hsVolumesbyVolumeListId.Clear()
            hsVolumesListIdbyEntityId.Clear()
            hsVolumesbyVolumeId.Clear()
        End Sub
    End Class
End Namespace

