Imports Zamba.Core

Imports Zamba.Servers
Public Class CreateTablesFactory
    'Implements Zamba.Servers.CreateTables

    Public Shared Sub AddDocsTables(ByVal DocTypeId As Integer) 'Implements Servers.CreateTables.AddDocsTables
        Server.CreateTables.AddDocsTables(DocTypeId)
    End Sub
    Public Shared Sub AddIndexColumn(ByVal DocTypeId As Integer, ByVal IndexIdArray As System.Collections.ArrayList, ByVal IndexTypeArray As System.Collections.ArrayList, ByVal IndexLenArray As System.Collections.ArrayList) ' Implements Servers.CreateTables.AddIndexColumn
        Server.CreateTables.AddIndexColumn(DocTypeId, IndexIdArray, IndexTypeArray, IndexLenArray)
    End Sub
    Public Shared Sub AddIndexList(ByVal IndexId As Integer, ByVal IndexLen As Integer) 'Implements Servers.CreateTables.AddIndexList
        Server.CreateTables.AddIndexList(IndexId, IndexLen)
    End Sub
    Public Shared Sub BorrarSustitucionTable(ByVal IndexId As Integer) 'Implements Servers.CreateTables.BorrarSustitucionTable
        Server.CreateTables.BorrarSustitucionTable(IndexId)
    End Sub
    Public Shared Sub BulkInsertSustitucionTable(ByVal FileName As String, ByVal separador As String, ByVal IndexId As Integer) 'Implements Servers.CreateTables.BulkInsertSustitucionTable
        Server.CreateTables.BulkInsertSustitucionTable(FileName, separador, IndexId)
    End Sub
    Public Shared Sub CreateSustitucionTable(ByVal Index As Integer, ByVal IsAlphanumeric As Boolean, ByVal IndexLen As Int32) 'Implements Servers.CreateTables.CreateSustitucionTable
        Server.CreateTables.CreateSustitucionTable(Index, IsAlphanumeric, IndexLen)
    End Sub
    Public Shared Sub CreateTextIndex(ByVal DocTypeId As Integer, ByVal IndexId As Integer) 'Implements Servers.CreateTables.CreateTextIndex
        Server.CreateTables.CreateTextIndex(DocTypeId, IndexId)
    End Sub
    'Public Shared Sub CreateView(ByVal DocTypeId As Integer) 'Implements Servers.CreateTables.CreateView
    '    Server.CreateTables.CreateView(DocTypeId)
    'End Sub
    Public Shared Sub DeleteFromSustitucion(ByVal Tabla As String, ByVal Codigo As Integer, ByVal Descripcion As String) 'Implements Servers.CreateTables.DeleteFromSustitucion
        Server.CreateTables.DeleteFromSustitucion(Tabla, Codigo, Descripcion)
    End Sub
    Public Shared Sub DeleteTable(ByVal Table As String) 'Implements Servers.CreateTables.DeleteTable
        Server.CreateTables.DeleteTable(Table)
    End Sub
    Public Shared Sub DelIndexColumn(ByVal DocTypeId As Integer, ByVal IndexIdArray As System.Collections.ArrayList) 'Implements Servers.CreateTables.DelIndexColumn
        Server.CreateTables.DelIndexColumn(DocTypeId, IndexIdArray)
    End Sub
    Public Shared Sub DelIndexItems(ByVal IndexId As Integer, ByVal IndexList As System.Collections.ArrayList) 'Implements Servers.CreateTables.DelIndexItems
        Server.CreateTables.DelIndexItems(IndexId, IndexList)
    End Sub
    Public Shared Sub DelIndexList(ByVal IndexId As Integer) 'Implements Servers.CreateTables.DelIndexList
        Server.CreateTables.DelIndexList(IndexId)
    End Sub
    Public Shared Sub DelTempTables() 'Implements Servers.CreateTables.DelTempTables
        Server.CreateTables.DelTempTables()
    End Sub
    Public Shared Sub Dispose() 'Implements Servers.CreateTables.Dispose
        Server.CreateTables.Dispose()
    End Sub
    Public Shared Sub DropSustitucionTable(ByVal IndexId As Integer) 'Implements Servers.CreateTables.DropSustitucionTable
        Server.CreateTables.DropSustitucionTable(IndexId)
    End Sub
    Public Shared Sub DropView(ByVal DocTypeId As Integer) 'Implements Servers.CreateTables.DropView
        Server.CreateTables.DropView(DocTypeId)
    End Sub
    Public Shared Sub ExportSustitucionTable(ByVal file As String, ByVal separador As String, ByVal IndexId As Integer) 'Implements Servers.CreateTables.ExportSustitucionTable
        Server.CreateTables.ExportSustitucionTable(file, separador, IndexId)
    End Sub
    Public Shared Sub InsertIndexList(ByVal IndexId As Integer, ByVal IndexList As System.Collections.ArrayList) 'Implements Servers.CreateTables.InsertIndexList
        Server.CreateTables.InsertIndexList(IndexId, IndexList)
    End Sub
    Public Shared Sub InsertIntoSustitucion(ByVal Tabla As String, ByVal Codigo As Integer, ByVal Descripcion As String) 'Implements Servers.CreateTables.InsertIntoSustitucion
        Server.CreateTables.InsertIntoSustitucion(Tabla, Codigo, Descripcion)
    End Sub
    Public Shared Sub UpdateIntoSustitucion(ByVal Tabla As String, ByVal Codigo As Integer, ByVal Descripcion As String) 'Implements Servers.CreateTables.UpdateIntoSustitucion
        Server.CreateTables.UpdateIntoSustitucion(Tabla, Codigo, Descripcion)
    End Sub


    Public Shared Sub CreateFriendlyView(ByVal DocType As DocType)
        Try
            Dim Indexs As New DataSet
            Indexs = Zamba.Data.Indexs_Factory.GetIndexSchema(DocType.ID)

            Dim Str As String
            Try
                'Str = "DROP View DOC" & DocTypeId
                Str = "if exists(select * from sysobjects where xtype='V' and name ='" & DocType.Name & "') begin drop view [" & DocType.Name & "] end"
                'TODO ERROR: REFERENCIAR DE Business A SERVER NO SE PUEDE< DEBE SER A TRAVEZ DE DATA
                Server.Con.ExecuteNonQuery(CommandType.Text, Str)
            Catch ex As SqlClient.SqlException
            Catch ex As Exception
            End Try

            Threading.Thread.Sleep(1000)

            Dim strbuilder As New System.Text.StringBuilder

            strbuilder.Append("CREATE VIEW [" & DocType.Name & "] AS SELECT ")

            '            strbuilder.Append("DOC_T" & DocType.ID & ".NAME AS [], ")
            For Each I As DataRow In Indexs.Tables(0).Rows
                strbuilder.Append("DOC_I" & DocType.ID & ".I" & I.Item("Index_Id").ToString & " AS [" & I.Item("Index_Name").ToString.Trim & "],")
            Next
            strbuilder.Append("DISK_VOLUME.DISK_VOL_PATH + '\' + CONVERT(nvarchar, ")
            strbuilder.Append(" DOC_T" & DocType.ID & ".DOC_TYPE_ID) + '\' + ")
            strbuilder.Append("CONVERT(nvarchar, DOC_T" & DocType.ID & ".OFFSET) + '\' + ")
            strbuilder.Append("CONVERT(nvarchar, DOC_T" & DocType.ID & ".DOC_FILE) AS PATH, ")
            strbuilder.Append("WFDocument.Do_State_ID AS IdEstado, WFStep.Name AS Etapa, WFStepStates.Name AS Estado, WFWorkflow.Name AS Workflow, WFStep.step_Id AS IdEtapa")
            strbuilder.Append(" FROM ")
            strbuilder.Append(" WFStep INNER JOIN WFDocument ON WFStep.step_Id = WFDocument.step_Id INNER JOIN WFStepStates ON WFDocument.do_state_id = WFStepStates.doc_state_id INNER JOIN  WFWorkflow ON WFStep.work_id = WFWorkflow.work_id RIGHT OUTER JOIN ")
            strbuilder.Append("DOC_T" & DocType.ID & " INNER JOIN ")
            strbuilder.Append("DOC_I" & DocType.ID & " ON DOC_T" & DocType.ID & ".DOC_ID = DOC_I" & DocType.ID & ".DOC_ID INNER JOIN ")
            strbuilder.Append("DISK_VOLUME ON DOC_T" & DocType.ID & ".VOL_ID = DISK_VOLUME.DISK_VOL_ID ")
            strbuilder.Append(" ON WFDocument.Doc_ID = DOC_T" & DocType.ID & ".DOC_ID")

            'TODO ERROR: REFERENCIAR DE Business A SERVER NO SE PUEDE< DEBE SER A TRAVEZ DE DATA
            Server.Con.ExecuteNonQuery(CommandType.Text, strbuilder.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
