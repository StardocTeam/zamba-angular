Imports Zamba.Data
Imports ZAMBA.Servers
Imports Zamba.Core

Public Class VolumeListsFactory

    Public Event LogError(ByVal ex As Exception)

    Public Overridable Sub Dispose()
        Server.Con.dispose()
    End Sub
    Public Shared Function GetDiskGroupsList() As ArrayList
        Dim DsDiskGroup As New DataSet
        Dim Strselect As String
        Strselect = "select * from Disk_GROUP  order by DISK_GROUP_NAME"
        DsDiskGroup = Server.Con.ExecuteDataset(CommandType.Text, Strselect)
        Dim Diskgroups As New ArrayList
        Dim i As Int32
        For i = 0 To DsDiskGroup.Tables(0).Rows.Count - 1
            Dim VolumeList As New VolumeList
            VolumeList.Id = DsDiskGroup.Tables(0).Rows(i).Item("DISK_GROUP_ID")
            VolumeList.Name = DsDiskGroup.Tables(0).Rows(i).Item("DISK_GROUP_NAME")
            Diskgroups.Add(VolumeList)
        Next
        Return Diskgroups
    End Function
    Public Shared Function getAllLists() As DataSet
        Dim Strselect As String
        Strselect = "select * from Disk_GROUP order by disk_group_crdate"
        Return Server.Con.ExecuteDataset(CommandType.Text, Strselect)
    End Function

    Public Shared Function GetDiskGroupVolumes(ByVal DiskGroupId As Int32) As DataSet
        Dim Strselect As String
        Strselect = "select * from Disk_GROUP_R_DISK_VOLUME WHERE DISK_GROUP_ID = " & DiskGroupId
        Dim dstemp As New DataSet
        dstemp = Server.Con.ExecuteDataset(CommandType.Text, Strselect)
        Return dstemp
    End Function

    ''' <summary>
    ''' Obtiene un listado de volumenes asociados al id de grupo de volumenes.
    ''' Los datos obtenidos son los siguientes: DISK_VOL_STATE, DISK_VOL_TYPE, DISK_VOL_PATH.
    ''' </summary>
    ''' <param name="DiskGroupId"></param>
    ''' <returns>Los datos obtenidos son los siguientes: DISK_VOL_STATE, DISK_VOL_TYPE, DISK_VOL_PATH.</returns>
    ''' <remarks></remarks>
    Public Shared Function GetDiskGroupData(ByVal DiskGroupId As Int32) As DataTable
        If Server.isOracle Then
            Throw New NotImplementedException()
        Else
            Dim sqlparams() As Object = {DiskGroupId}
            Return Server.Con.ExecuteDataset("zsp_volumes_100_GetVolumesByDgId", sqlparams).Tables(0)
        End If
    End Function

    Public Shared Function IsDuplicate(ByVal name As String) As Boolean
        Dim sql As String = "Select * from disk_group where UPPER(disk_group_name)='" & name.Trim & "'"
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetActiveDiskGroupVolumes(ByVal DiskGroupId As Int32) As DataSet
        Dim Strselect As String
        Strselect = "select Disk_GROUP_R_DISK_VOLUME.DISK_VOLUME_ID from Disk_GROUP_R_DISK_VOLUME,DISK_VOLUME WHERE Disk_GROUP_R_DISK_VOLUME.DISK_VOLUME_ID = DISK_VOLUME.DISK_VOL_ID AND DISK_VOLUME.DISK_VOL_STATE = 0 AND DISK_GROUP_R_DISK_VOLUME.DISK_GROUP_ID = " & DiskGroupId
        Dim dstemp As New DataSet
        dstemp = Server.Con.ExecuteDataset(CommandType.Text, Strselect)
        Return dstemp
    End Function

    Public Shared Function AddDiskGroup(ByVal DiskGroupName As String) As Int32

        Dim strinsert As String = "INSERT INTO DISK_GROUP (DISK_GROUP_ID,DISK_GROUP_NAME,DISK_GROUP_CRDATE) VALUES (" & CoreData.GetNewID(IdTypes.DISKGROUPID) & ", '" & DiskGroupName & "'," & Server.Con.ConvertDate(Now.ToShortDateString) & ")"
        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)

    End Function
    Public Shared Sub DELDiskGroup(ByVal DiskGroupid As Int32)
        Try
            Dim strdelete As String = "DELETE FROM DISK_GROUP WHERE DISK_GROUP_ID = " & DiskGroupid
            Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
        Catch
        End Try
    End Sub

    Public Shared Sub AddtoDiskGroup(ByVal Vols As ArrayList, ByVal DiskGroupId As Int32)
        Dim i As Int32
        Try
            Dim strinsert As String
            Dim strselect As String
            Dim scalar As Int32
            For i = 0 To Vols.Count - 1
                strselect = " SELECT count(1) FROM DISK_GROUP_R_DISK_VOLUME where DISK_GROUP_ID=" & DiskGroupId & " AND DISK_VOLUME_ID=" & CType(Vols(i).Id, String)
                scalar = Server.Con.ExecuteScalar(CommandType.Text, strselect)
                If Not scalar > 0 Then
                    strinsert = "INSERT INTO DISK_GROUP_R_DISK_VOLUME (DISK_GROUP_ID,DISK_VOLUME_ID) VALUES (" & CType(DiskGroupId, String) & ", " & CType(Vols(i).Id, String) & ")"
                    Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
                End If
            Next
        Catch
        End Try
    End Sub
    Public Shared Sub DelFromDiskGroup(ByVal VolsId As ArrayList, ByVal DiskGroupId As Int32)
        Try
            Dim i As Int32
            Dim strdelete As String
            For i = 0 To VolsId.Count - 1
                strdelete = "DELETE FROM DISK_GROUP_R_DISK_VOLUME WHERE DISK_GROUP_ID = " & DiskGroupId & " and DISK_VOLUME_ID = " & CType(VolsId(i).id, String)
                Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
            Next
        Catch
        End Try
    End Sub
End Class
