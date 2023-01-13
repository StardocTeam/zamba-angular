Imports Zamba.Servers

Public Class VolumesFactoryExt
    Public Function GetVolumenPathByVolId(ByVal volid As Int32) As String
        If Server.isOracle Then
            Dim strsql As String = "select Disk_Vol_Path from Disk_Volume where Disk_Vol_ID=" & volid
            Return Server.Con.ExecuteScalar(CommandType.Text, strsql)
        Else
            Return Server.Con.ExecuteScalar("ZSP_VOLUME_100_GetVolumenPathByVolId", New Object() {volid})
        End If
    End Function
End Class
