Imports ZAMBA.Servers
Imports Zamba.AppBlock
Imports System.Windows.Forms

Public Class ClsEstadisticas
    Public Shared Function InactiveUsers() As DataSet
        'Devuelve los usuarios que llevan mas de un mes sin usar Zamba
        'Me.GetTotalUsers()
        Dim ds As DataSet
        Dim dsinactive As New DsInactiveUsers
        Try
            'Dim sql As String = "Select * from InactiveUsers"
            Dim sql As String = "Select * from Zvw_INACTIVEUSERS_100"
            ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
            ds.Tables(0).TableName = dsinactive.DsInactiveUsers.TableName
            dsinactive.Merge(ds)
        Catch
        End Try
        Return dsinactive
    End Function
    Public Shared Function usuariosMasActivos() As DataSet
        Dim ds As DataSet
        Try
            Dim sql As String = "Select Name from UsrTable where ID in (Select User_Id from User_Hst) order by Name"
            ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
        Catch
        End Try
        Return ds
    End Function
    Public Shared Function DocumentosMasConsultados(ByVal ranking As Int16) As DataSet
        Dim ds As New dsBestDocuments
        Try
            Dim sql As String
            Dim dstemp As New DataSet
            If Server.ServerType = Server.DBTYPES.OracleClient Or Server.ServerType = Server.DBTYPES.Oracle9 Or Server.ServerType = Server.DBTYPES.Oracle Then
                'sql = "Select * from BestDocuments where rownum < " & ranking + 1
                sql = "Select * from Zvw_BESTDOCUMENTS_100 where rownum < " & ranking + 1
            Else
                sql = "Select top " & ranking & " * from Zvw_BESTDOCUMENTS_100"
            End If
            dstemp = Server.Con.ExecuteDataset(CommandType.Text, sql)
            dstemp.Tables(0).TableName = ds.dsBestDocuments.TableName
            dstemp.Tables(0).Columns(0).ColumnName = ds.Tables(0).Columns(0).ColumnName
            dstemp.Tables(0).Columns(1).ColumnName = ds.Tables(0).Columns(1).ColumnName
            ds.Merge(dstemp)
        Catch
        End Try
        Return ds
    End Function
    Public Shared Function UsersPrinting() As DataSet
        'Devuelve los documentos impresos por el usuario
        Dim ds As New DsUsersPrint
        Try
            Dim sql As String = "Select * from UsersPrint order by Usuario"
            Dim dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
            dstemp.Tables(0).TableName = ds.Tables(0).TableName
            ds.Merge(dstemp)
        Catch ex As Exception
            ZException.Log(ex)
        End Try
        Return ds
    End Function
    Public Shared Function UsersPrinting(ByVal desde As Date, ByVal hasta As Date) As DataSet
        Dim ds As New DsUsersPrint
        Dim sql As String
        Try
            If Server.ServerType = Server.DBTYPES.Oracle Or Server.ServerType = Server.DBTYPES.Oracle9 Or Server.ServerType = Server.DBTYPES.OracleClient Then
                'sql = "Select * from UsersPrint where Fecha between '" & desde.ToShortDateString & "' and '" & hasta.ToShortDateString & "' order by Usuario"
                sql = "Select * from Zvw_UsersPrint_100 where Fecha between '" & desde.ToShortDateString & "' and '" & hasta.ToShortDateString & "' order by Usuario"
            Else
                'sql = "Select * from UsersPrint where Fecha between #" & desde.ToShortDateString & "# and #" & hasta.ToShortDateString & "# order by Usuario"
                sql = "Select * from Zvw_UsersPrint_100 where Fecha between #" & desde.ToShortDateString & "# and #" & hasta.ToShortDateString & "# order by Usuario"
            End If
            Dim dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
            dstemp.Tables(0).TableName = ds.Tables(0).TableName
            ds.Merge(dstemp)
        Catch
        End Try
        Return ds
    End Function
    Public Shared Function DocumentosEliminados() As DataSet
        Dim ds As New DsDocDeleted
        Try
            'Dim sql As String = "Select * from DocDeleted order by USUARIO,FECHA"
            Dim sql As String = "Select * from Zvw_DOCDELETED_100 order by USUARIO,FECHA"
            Dim dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
            dstemp.Tables(0).TableName = ds.Tables(0).TableName
            ds.Merge(dstemp)
        Catch
        End Try
        Return ds
    End Function
    Public Shared Function DocEnviados() As DataSet
        ' uso el mismo dataset que docdeleted porque la tabla tiene la misma estructura
        Dim ds As New DsDocDeleted
        Try
            'Dim sql As String = "Select * from DocSend order by USUARIO,FECHA"
            Dim sql As String = "Select * from Zvw_DOCSEND_100 order by USUARIO,FECHA"
            Dim dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
            dstemp.Tables(0).TableName = ds.Tables(0).TableName
            ds.Merge(dstemp)
        Catch
        End Try
        Return ds
    End Function
    Public Shared Function Instalaciones() As DataSet
        Dim ds As New DsInstalled
        Dim dstemp As New DataSet
        Try
            'Dim sql As String = "Select * from instalaciones"
            Dim sql As String = "Select * from Zvw_INSTALACIONES_100"
            dstemp = Server.Con.ExecuteDataset(CommandType.Text, sql)
            dstemp.Tables(0).TableName = ds.Tables(0).TableName
            ds.Merge(dstemp)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return ds
    End Function
    Public Shared Function LoginFailed() As DataSet
        Dim ds As New DSLogins
        Dim dstemp As DataSet
        Try
            Dim sql As String = "Select * from loginsfailed"
            'Dim sql As String = "Select * from Zvw_LoginsFailed_100"
            dstemp = Server.Con.ExecuteDataset(CommandType.Text, sql)
            dstemp.Tables(0).TableName = ds.Tables(0).TableName
            ds.Merge(dstemp)
        Catch ex As Exception
            ZException.Log(ex, False)
            Return Nothing
        End Try
        Return ds
    End Function
    Private Shared Function DocTypes() As DataSet
        Dim ds As DataSet
        Try
            Dim sql As String = "Select doc_type_id, doc_type_name from doc_type order by doc_type_name"
            ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
        Catch
        End Try
        Return ds
    End Function

    Public Shared Function VolDocs(ByVal fecha1 As Date, ByVal fecha2 As Date) As DataSet
        Dim ds As New dsdocdiarios
        Dim dsdoct As New DataSet
        Dim sql As String
        Dim i, J As Int16
        Dim espacio As Decimal
        Dim cantidad As Int32
        For i = 0 To DocTypes.Tables(0).Rows.Count - 1
            espacio = 0
            cantidad = 0
            ' sql = "Select '" & DocTypes.Tables(0).Rows(i).Item(1).Trim & "' as [Tipo de Documento],CONVERT(CHAR(11), crdate, 100) as Fecha,count(crdate) as Cantidad, Sum(Doc_T" & DocTypes.Tables(0).Rows(i).Item(0) & ".Filesize) as Espacio from doc_t" & DocTypes.Tables(0).Rows(i).Item(0) & ",doc" & DocTypes.Tables(0).Rows(i).Item(0) & " Group by CONVERT(CHAR(11), crdate, 100)"
            Try
                sql = "Select Fecha,count(crdate) as Cantidad, Sum(Doc_T" & DocTypes.Tables(0).Rows(i).Item(0) & ".Filesize) as Espacio from doc_t" & DocTypes.Tables(0).Rows(i).Item(0) & " Inner Join doc_I" & DocTypes.Tables(0).Rows(i).Item(0) & " On(" & _
                "Doc_T" & DocTypes.Tables(0).Rows(i).Item(0) & ".Doc_Id=" & "Doc_I" & DocTypes.Tables(0).Rows(i).Item(0) & ".Doc_Id)" & " where fecha between '" & fecha1 & "' and '" & fecha2 & "' Group by Fecha"
                'sql = "Select Fecha,count(crdate) as Cantidad, Sum(Doc_T" & DocTypes.Tables(0).Rows(i).Item(0) & ".Filesize) as Espacio from doc_t" & DocTypes.Tables(0).Rows(i).Item(0) & ",doc_I" & DocTypes.Tables(0).Rows(i).Item(0) & " where fecha between '" & fecha1 & "' and '" & fecha2 & "' Group by Fecha"
                'sql = "Select Fecha,count(crdate) as Cantidad, Sum(Doc_T" & DocTypes.Tables(0).Rows(i).Item(0) & ".Filesize) as Espacio from doc_t" & DocTypes.Tables(0).Rows(i).Item(0) & ",doc_I" & DocTypes.Tables(0).Rows(i).Item(0) & " where fecha between convert(char(10),'" & fecha1.ToString & "') and convert(char(10),'" & fecha2.ToString & "') Group by Fecha"
                dsdoct = Server.Con.ExecuteDataset(CommandType.Text, sql)
            Catch ex As Exception
            End Try
            For J = 0 To dsdoct.Tables(0).Rows.Count - 1
                Dim row As dsdocdiarios.dsdocsRow = ds.dsdocs.NewdsdocsRow
                row.doctype = DocTypes.Tables(0).Rows(i).Item(1).Trim ' DocTypes.Tables(0).Rows(i).Item(1)
                row.Fecha = dsdoct.Tables(0).Rows(J).Item(0) 'dsdoct.Tables(0).Rows(J).Item(1)
                row.DocInserted = dsdoct.Tables(0).Rows(J).Item(1) 'dsdoct.Tables(0).Rows(J).Item(2)
                cantidad += dsdoct.Tables(0).Rows(J).Item(1) 'dsdoct.Tables(0).Rows(J).Item(2)
                row.espacio = dsdoct.Tables(0).Rows(J).Item(2) 'dsdoct.Tables(0).Rows(J).Item(3)
                espacio += dsdoct.Tables(0).Rows(J).Item(2) 'dsdoct.Tables(0).Rows(J).Item(3)
                row.espAcum = espacio
                row.docacum = cantidad
                ds.dsdocs.Rows.Add(row)
            Next
            dsdoct.Dispose()
        Next
        ds.AcceptChanges()
        Return ds
    End Function


End Class
