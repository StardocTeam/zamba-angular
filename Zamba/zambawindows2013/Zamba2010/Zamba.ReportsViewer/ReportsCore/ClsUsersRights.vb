Imports ZAMBA.AppBlock
Imports ZAMBA.Servers
Public Class UsersRights
    Public Shared Function VerPermisos() As DataSet
        Try
            'Dim sql As String = "Select * from ViewPermisos"
            Dim sql As String = "Select * from Zvw_permisos_100"
            Dim Ds As New DsPermisos
            Dim dstemp As DataSet
            dstemp = Server.Con(True).ExecuteDataset(CommandType.Text, sql)
            dstemp.Tables(0).TableName = "DsPermisos"
            Ds.Tables(0).TableName = "DsPermisos"
            Ds.Merge(dstemp)
            Return Ds
        Catch ex As Exception
        End Try
    End Function
    Public Shared Function PermisosPorUsuario() As DataSet
        Try
            'Dim sql As String = "Select distinct * from viewpermisos"
            Dim sql As String = "Select distinct * from Zvw_permisos_100"
            Dim Ds As New DsPermisos
            Dim dstemp As DataSet
            dstemp = Server.Con(True).ExecuteDataset(CommandType.Text, sql)
            dstemp.Tables(0).TableName = "DsPermisos"
            Ds.Tables(0).TableName = "DsPermisos"
            Ds.Merge(dstemp)
            Return Ds
        Catch ex As Exception
        End Try
    End Function

End Class
