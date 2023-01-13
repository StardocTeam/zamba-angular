Imports System.Data.SqlClient
Imports Zamba.Servers
Imports Zamba
Public Class HistorialDeAcciones

    Implements IDisposable
    Private Const TRAER_TABLA_USER_HST As String = "SELECT * FROM USER_HST"
    Private Const TRAER_TABLA_USR_GROUP As String = "select USRTABLE.ID USER_ID, USRTABLE.NAME USER_NAME,USRGROUP.ID GROUP_ID, USRGROUP.NAME GROUP_NAME FROM USRTABLE INNER JOIN ( USR_R_GROUP INNER JOIN USRGROUP ON USR_R_GROUP.GROUPID=USRGROUP.ID)ON(USRTABLE.ID=USR_R_GROUP.USRID)"
    Private Const TRAER_TABLAObjectTypes As String = "SELECT * FROM OBJECTTYPES"
    Private Const TRAER_TABLA_OBJECTID As String = "SELECT * FROM OBJECTID"
    Private Const TRAER_TABLARightsType As String = "SELECT * FROM RIGHTSTYPE"
    ' Dim WithEvents frmDates As frmDates ''Revisar, hay que eliminar el uso de formularios desde clases
    Dim FechaInicial, FechaFinal As Date

    Public Sub Dispose() Implements System.IDisposable.Dispose

    End Sub
    Public Function CargarDataSet() As DsHistorialAcciones
        Dim dsOrg(5) As DataSet
        Dim dst As New DsHistorialAcciones
        '  Dim sNombreTabla,
        Dim sql As String
        Dim FechaI, FechaF As String
        'Dim i1, i2 As Int16

        'Revisar, hay que eliminar el uso de formularios desde clases
        'Try
        '    frmDates = New frmDates
        '    frmDates.StartPosition = FormStartPosition.CenterScreen
        '    frmDates.ShowDialog()
        'Catch ex As Exception
        'End Try


        If Server.ServerType = Server.DBTYPES.OracleClient OrElse Server.ServerType = Server.DBTYPES.Oracle9 OrElse Server.ServerType = Server.DBTYPES.Oracle Then
            FechaI = Server.Con.ConvertDate(FechaInicial.ToString)
            FechaF = Server.Con.ConvertDate(FechaFinal.ToString)
            Try
                sql = TRAER_TABLA_USER_HST & " WHERE ACTION_DATE BETWEEN " & FechaI & " AND " & FechaF
                dsOrg(0) = Server.Con.ExecuteDataset(CommandType.Text, sql)

                sql = TRAER_TABLA_OBJECTID
                dsOrg(1) = Server.Con.ExecuteDataset(CommandType.Text, sql)


                sql = TRAER_TABLAObjectTypes
                dsOrg(2) = Server.Con.ExecuteDataset(CommandType.Text, sql)


                sql = TRAER_TABLA_USR_GROUP
                dsOrg(3) = Server.Con.ExecuteDataset(CommandType.Text, sql)


                sql = TRAER_TABLARightsType
                dsOrg(4) = Server.Con.ExecuteDataset(CommandType.Text, sql)

            Catch ex As Exception
                Zamba.AppBlock.ZException.Log(ex, False)

            End Try
        Else
            Try
                'sql = TRAER_TABLA_USER_HST & " WHERE ACTION_DATE BETWEEN '" & Zamba.Servers.Server.Con.ConvertDateTime(FechaInicial.ToString) & "' AND '" & Zamba.Servers.Server.Con.ConvertDateTime(FechaFinal.ToString) & "'"
                sql = TRAER_TABLA_USER_HST
                dsOrg(0) = Server.Con.ExecuteDataset(CommandType.Text, sql)
                sql = TRAER_TABLA_OBJECTID
                dsOrg(1) = Server.Con.ExecuteDataset(CommandType.Text, sql)
                sql = TRAER_TABLAObjectTypes
                dsOrg(2) = Server.Con.ExecuteDataset(CommandType.Text, sql)
                sql = TRAER_TABLA_USR_GROUP
                dsOrg(3) = Server.Con.ExecuteDataset(CommandType.Text, sql)
                sql = TRAER_TABLARightsType
                dsOrg(4) = Server.Con.ExecuteDataset(CommandType.Text, sql)
            Catch ex As Exception
                Zamba.AppBlock.ZException.Log(ex, False)
            End Try
        End If


        Try
            dst.Tables("USR_HST").TableName = dsOrg(0).Tables(0).TableName
            dst.Merge(dsOrg(0))
            dst.Tables("Table").TableName = "USR_HST"

            dst.Tables("OBJECTID").TableName = dsOrg(1).Tables(0).TableName
            dst.Merge(dsOrg(1))
            dst.Tables("Table").TableName = "OBJECTID"

            dst.Tables("OBJECTTYPES").TableName = dsOrg(2).Tables(0).TableName
            dst.Merge(dsOrg(2))
            dst.Tables("Table").TableName = "OBJECTTYPES"

            dst.Tables("USR_GROUP").TableName = dsOrg(3).Tables(0).TableName
            dst.Merge(dsOrg(3))
            dst.Tables("Table").TableName = "USR_GROUP"

            dst.Tables("RIGHTSTYPE").TableName = dsOrg(4).Tables(0).TableName
            dst.Merge(dsOrg(4))
            dst.Tables("Table").TableName = "RIGHTSTYPE"

        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex, False)
        End Try
        CargarDataSet = dst
    End Function
    'Revisar, hay que eliminar el uso de formularios desde clases
    'Private Sub frmdates_fechas(ByVal Desde As Date, ByVal Hasta As Date) Handles frmDates.Fechas
    '    MyClass.FechaInicial = Desde
    '    MyClass.FechaFinal = Hasta
    'End Sub
End Class
