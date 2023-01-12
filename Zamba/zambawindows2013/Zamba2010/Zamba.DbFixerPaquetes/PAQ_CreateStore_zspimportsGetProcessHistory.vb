Imports Zamba.Servers
Imports Zamba.AppBlock
Public Class PAQ_CreateStore_zspimportsGetProcessHistory
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("12/05/06")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Procedimiento de consulta tablas P_HST,USRTABLE,IP_RESULTS"
        End Get
    End Property



    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "zsp_imports_100_GetProcessHistory"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_CreateStore_zspimportsGetProcessHistory
        End Get
    End Property

    Dim _instaled As Boolean = False
    Public Property installed() As Boolean Implements IPAQ.installed
        Get
            Return _instaled
        End Get
        Set(ByVal value As Boolean)
            _instaled = value
        End Set
    End Property
    Public ReadOnly Property orden() As Int64 Implements IPAQ.orden
        Get
            Return 82
        End Get
    End Property

#End Region

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        Dim sql As New System.Text.StringBuilder
        Dim var As Boolean

        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            'SQL
            var = False
            Try
                If ZPaq.IfExists("zspimportsGetProcessHistory", ZPaq.Tipo.StoredProcedure, False) = False Then
                    If MessageBox.Show("El procedimiento zspimportsGetProcessHistory ya existe desea borrarlo?", "Creación de Procedimientos", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly) = DialogResult.Yes Then
                        sql.Append("drop procedure zspimportsGetProcessHistory")
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                        var = True
                    End If
                Else
                    var = True
                End If
                If var = True Then
                    sql.Append("CREATE PROCEDURE zspimportsGetProcessHistory(@ProcessID NUMERIC) AS SELECT [P_HST].[ID],[P_HST].[Process_Date],[P_HST].[User_Id],[P_HST].[TotalFiles],[P_HST].[ProcessedFiles],[P_HST].[Result_Id],[P_HST].[SkipedFiles],[P_HST].[ErrorFiles],[P_HST].[Path],[USRTABLE].[Name],[P_HST].[Process_id],[ip_results].[Result],[P_HST].[Hash], [P_HST].[LOGFILE],[P_HST].[ERRORFILE],[P_HST].[TEMPFILE] FROM P_HST,USRTABLE,IP_RESULTS WHERE P_HST.[User_Id] = [USRTABLE].[Id] AND [P_HST].[process_ID] = @ProcessID AND [IP_RESULTS].[RESULT_ID] = [P_HST].[RESULT_ID] ORDER BY [P_HST].[ID] DESC")
                    If GenerateScripts = False Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql.ToString)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If

                End If
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        Else
            'ORACLE
            sql.Append("")

        End If




        sql = Nothing
        Return True
    End Function
#End Region

End Class
