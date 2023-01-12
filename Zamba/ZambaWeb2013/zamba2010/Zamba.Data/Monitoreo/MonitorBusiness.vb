Imports Zamba.Core
Namespace MonitorFactory

    Public Class MonitorProcessFactory

        Public Shared Function GetFolders(ByVal UserConfigMachineName As String) As DataSet
            Dim dsTemp As DataSet
            Dim Strselect As String


            If UserConfigMachineName = String.Empty Then
                Strselect = "SELECT * FROM IP_FOLDER WHERE Upper(NOMBREMAQUINA) = '" & Environment.MachineName.ToUpper & "'"
            Else
                Strselect = "SELECT * FROM IP_FOLDER WHERE Upper(NOMBREMAQUINA) = '" & UserConfigMachineName.ToUpper & "'"
            End If
            ZTrace.WriteLineIf(ZTrace.IsInfo, Strselect)
            dsTemp = Server.Con.ExecuteDataset(CommandType.Text, Strselect)

            '[Tomas - 18/09/2009] Si el valor del timer no se encuentra en la carpeta ip_folder lo busca en ip_folderconf
            If TypeOf (dsTemp.Tables(0).Rows(0)("TIMER")) Is DBNull Then
                Dim query As String = "SELECT TIMER FROM IP_FOLDERCONF WHERE ID_CARPETA = " & dsTemp.Tables(0).Rows(0)("ID").ToString
                Dim timer As Object = Server.Con.ExecuteScalar(CommandType.Text, query)
                dsTemp.Tables(0).Rows(0)("TIMER") = timer
                query = Nothing
                timer = Nothing
            End If

            Return dsTemp
        End Function

    End Class

End Namespace
