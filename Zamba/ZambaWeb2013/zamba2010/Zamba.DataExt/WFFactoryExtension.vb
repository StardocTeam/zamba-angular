Imports Zamba.Servers
Imports System.Text

Public Class WFFactoryExtension
    Public Const STRINGSQL_FORMAT As String = "'{0}'"
    Private Const INSERTLIMS_FORMAT As String = "INSERT INTO ZLWS_Recived(Muestra,Concepto,Reactivada,FechaReactivacion,MotivoReactivacion,Reactivador,Punto,FHExtraccion,Componente,ValorLectura,FechaLectura,ValorRegulado,FueraDeNorma,Estado,Nivel,InformeDeEnsayo,UltimaVersionIE,NdePrecinto,IdExtractor,NombreExtractor) VALUES({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19})"

    Shared Function GetLastExecutionDate() As String
        Dim sbQuery As String = "SELECT max(FechaHasta) FROM ZLWS_PrcessedHistory"

        Dim returnValue As Object = Server.Con.ExecuteScalar(CommandType.Text, sbQuery)

        If IsDBNull(returnValue) Then
            Return String.Empty
        Else
            Return returnValue.ToString()
        End If
    End Function

    Shared Sub InsertIncomingRows(ByRef tableToInsert As DataTable, ByVal transactionID As Long)
        Dim sbQuery As New StringBuilder

        For Each row As DataRow In tableToInsert.Rows
            sbQuery.AppendFormat(INSERTLIMS_FORMAT, row(0), String.Format(STRINGSQL_FORMAT, row(1)), String.Format(STRINGSQL_FORMAT, row(2)), _
                                 String.Format(STRINGSQL_FORMAT, row(3)), String.Format(STRINGSQL_FORMAT, row(4)), String.Format(STRINGSQL_FORMAT, row(5)), _
                                 String.Format(STRINGSQL_FORMAT, row(6)), String.Format(STRINGSQL_FORMAT, row(7)), String.Format(STRINGSQL_FORMAT, row(8)), _
                                 String.Format(STRINGSQL_FORMAT, row(9)), String.Format(STRINGSQL_FORMAT, row(10)), String.Format(STRINGSQL_FORMAT, row(11)),
                                 String.Format(STRINGSQL_FORMAT, row(12)), String.Format(STRINGSQL_FORMAT, row(13)), String.Format(STRINGSQL_FORMAT, row(14)), _
                                 String.Format(STRINGSQL_FORMAT, row(15)), String.Format(STRINGSQL_FORMAT, row(16)), String.Format(STRINGSQL_FORMAT, row(17)), _
                                 String.Format(STRINGSQL_FORMAT, row(18)), String.Format(STRINGSQL_FORMAT, row(19)), transactionID)
            Server.Con.ExecuteNonQuery(CommandType.Text, sbQuery.ToString())
            sbQuery.Length = 0
            sbQuery.Capacity = 0
        Next
    End Sub

    Shared Sub InsertExecutionDates(ByVal fechaDesde As String, ByVal fechaHasta As String)
        Dim sbQuery As New StringBuilder

        sbQuery.Append("INSERT INTO ZLWS_PrcessedHistory(FechaDesde,FechaHasta) VALUES(")
        sbQuery.AppendFormat(STRINGSQL_FORMAT, fechaDesde)
        sbQuery.Append(",")
        sbQuery.AppendFormat(STRINGSQL_FORMAT, fechaHasta)
        sbQuery.Append(")")

        Server.Con.ExecuteNonQuery(CommandType.Text, sbQuery.ToString())
    End Sub


    Shared Function GetLastTransaction() As Long
        Dim sbQuery As String = "SELECT max(IDTransaction) FROM ZLWS_PrcessedHistory"

        Return Server.Con.ExecuteScalar(CommandType.Text, sbQuery)
    End Function
End Class