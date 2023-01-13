Namespace Cache
    Public Class Volumes
        Public Shared Property hsVolumeTypes As New Hashtable
        Public Shared Property hsVolumesbyVolumeListId As New Hashtable
        Public Shared Property hsVolumesbyVolumeId As New Hashtable

        Public Shared Property hsVolumesListIdbyEntityId As New Hashtable
        Friend Shared Sub RemoveCurrentInstance()
            ClearAll()
        End Sub

        Public Shared Sub ClearAll()
            Try
                hsVolumeTypes.Clear()
                hsVolumesbyVolumeListId.Clear()
                hsVolumesListIdbyEntityId.Clear()
                hsVolumesbyVolumeId.Clear()
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
    End Class
End Namespace

