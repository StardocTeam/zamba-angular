Imports Zamba.Data
Public Class CreateTablesBusiness

    'TODO ERROR: REFERENCIAR DE Business A SERVER NO SE PUEDE< DEBE SER A TRAVEZ DE DATA
    ' Implements Zamba.Servers.CreateTables

    'TODO ERROR: REFERENCIAR DE Business A SERVER NO SE PUEDE< DEBE SER A TRAVEZ DE DATA
    Public Sub AddDocsTables(ByVal DocTypeId As Integer) 'Implements Servers.CreateTables.AddDocsTables
        CreateTablesFactory.AddDocsTables(DocTypeId)
    End Sub
    Public Sub AddIndexColumn(ByVal DocTypeId As Integer, ByVal IndexIdArray As System.Collections.ArrayList, ByVal IndexTypeArray As System.Collections.ArrayList, ByVal IndexLenArray As System.Collections.ArrayList) 'Implements Servers.CreateTables.AddIndexColumn
        CreateTablesFactory.AddIndexColumn(DocTypeId, IndexIdArray, IndexTypeArray, IndexLenArray)
    End Sub
    'TODO ERROR: REFERENCIAR DE Business A SERVER NO SE PUEDE< DEBE SER A TRAVEZ DE DATA
    Public Sub AddIndexList(ByVal IndexId As Integer, ByVal IndexLen As Integer) 'Implements Servers.CreateTables.AddIndexList
        CreateTablesFactory.AddIndexList(IndexId, IndexLen)
    End Sub
    'TODO ERROR: REFERENCIAR DE Business A SERVER NO SE PUEDE< DEBE SER A TRAVEZ DE DATA
    Public Sub BorrarSustitucionTable(ByVal IndexId As Integer) ' Implements Servers.CreateTables.BorrarSustitucionTable
        CreateTablesFactory.BorrarSustitucionTable(IndexId)
    End Sub
    Public Sub BulkInsertSustitucionTable(ByVal FileName As String, ByVal separador As String, ByVal IndexId As Integer) 'Implements Servers.CreateTables.BulkInsertSustitucionTable
        CreateTablesFactory.BulkInsertSustitucionTable(FileName, separador, IndexId)
    End Sub
    'TODO ERROR: REFERENCIAR DE Business A SERVER NO SE PUEDE< DEBE SER A TRAVEZ DE DATA
    Public Sub CreateSustitucionTable(ByVal Index As Integer, ByVal IndexLen As Int32, ByVal IndexType As Int32) 'Implements Servers.CreateTables.CreateSustitucionTable
        CreateTablesFactory.CreateSustitucionTable(Index, IndexLen, IndexType)
    End Sub
    'TODO ERROR: REFERENCIAR DE Business A SERVER NO SE PUEDE< DEBE SER A TRAVEZ DE DATA
    Public Sub CreateTextIndex(ByVal DocTypeId As Integer, ByVal IndexId As Integer) 'Implements Servers.CreateTables.CreateTextIndex
        CreateTablesFactory.CreateTextIndex(DocTypeId, IndexId)
    End Sub
    'TODO ERROR: REFERENCIAR DE Business A SERVER NO SE PUEDE< DEBE SER A TRAVEZ DE DATA
    Public Sub CreateView(ByVal DocTypeId As Integer) 'Implements Servers.CreateTables.CreateView
        DocTypesBusiness.CreateView(DocTypeId)
    End Sub
    Public Sub DeleteFromSustitucion(ByVal Tabla As String, ByVal Codigo As Integer, ByVal Descripcion As String) 'Implements Servers.CreateTables.DeleteFromSustitucion
        CreateTablesFactory.DeleteFromSustitucion(Tabla, Codigo, Descripcion)
    End Sub
    'TODO ERROR: REFERENCIAR DE Business A SERVER NO SE PUEDE< DEBE SER A TRAVEZ DE DATA
    Public Sub DeleteTable(ByVal Table As String) 'Implements Servers.CreateTables.DeleteTable
        CreateTablesFactory.DeleteTable(Table)
    End Sub
    Public Sub DelIndexColumn(ByVal DocTypeId As Integer, ByVal IndexIdArray As System.Collections.ArrayList) 'Implements Servers.CreateTables.DelIndexColumn
        CreateTablesFactory.DelIndexColumn(DocTypeId, IndexIdArray)
    End Sub
    'TODO ERROR: REFERENCIAR DE Business A SERVER NO SE PUEDE< DEBE SER A TRAVEZ DE DATA
    Public Sub DelIndexItems(ByVal IndexId As Integer, ByVal IndexList As System.Collections.ArrayList) 'Implements Servers.CreateTables.DelIndexItems
        CreateTablesFactory.DelIndexItems(IndexId, IndexList)
    End Sub
    'TODO ERROR: REFERENCIAR DE Business A SERVER NO SE PUEDE< DEBE SER A TRAVEZ DE DATA
    Public Sub DelIndexList(ByVal IndexId As Integer) 'Implements Servers.CreateTables.DelIndexList
        CreateTablesFactory.DelIndexList(IndexId)
    End Sub
    'TODO ERROR: REFERENCIAR DE Business A SERVER NO SE PUEDE< DEBE SER A TRAVEZ DE DATA
    Public Sub DelTempTables() 'Implements Servers.CreateTables.DelTempTables
        CreateTablesFactory.DelTempTables()
    End Sub
    'TODO ERROR: REFERENCIAR DE Business A SERVER NO SE PUEDE< DEBE SER A TRAVEZ DE DATA
    Public Sub Dispose() 'Implements Servers.CreateTables.Dispose
        CreateTablesFactory.Dispose()
    End Sub
    'TODO ERROR: REFERENCIAR DE Business A SERVER NO SE PUEDE< DEBE SER A TRAVEZ DE DATA
    Public Sub DropSustitucionTable(ByVal IndexId As Integer) ' Implements Servers.CreateTables.DropSustitucionTable
        CreateTablesFactory.DropSustitucionTable(IndexId)
    End Sub
    'TODO ERROR: REFERENCIAR DE Business A SERVER NO SE PUEDE< DEBE SER A TRAVEZ DE DATA
    Public Sub DropView(ByVal DocTypeId As Integer) ' Implements Servers.CreateTables.DropView
        CreateTablesFactory.DropView(DocTypeId)
    End Sub
    Public Sub ExportSustitucionTable(ByVal file As String, ByVal separador As String, ByVal IndexId As Integer) 'Implements Servers.CreateTables.ExportSustitucionTable
        CreateTablesFactory.ExportSustitucionTable(file, separador, IndexId)
    End Sub
    'TODO ERROR: REFERENCIAR DE Business A SERVER NO SE PUEDE< DEBE SER A TRAVEZ DE DATA
    Public Sub InsertIndexList(ByVal IndexId As Integer, ByVal IndexList As System.Collections.ArrayList) ' Implements Servers.CreateTables.InsertIndexList
        CreateTablesFactory.InsertIndexList(IndexId, IndexList)
    End Sub
    'TODO ERROR: REFERENCIAR DE Business A SERVER NO SE PUEDE< DEBE SER A TRAVEZ DE DATA
    Public Sub InsertIntoSustitucion(ByVal Tabla As String, ByVal Codigo As Integer, ByVal Descripcion As String) 'Implements Servers.CreateTables.InsertIntoSustitucion
        CreateTablesFactory.InsertIntoSustitucion(Tabla, Codigo, Descripcion)
    End Sub
    'TODO ERROR: REFERENCIAR DE Business A SERVER NO SE PUEDE< DEBE SER A TRAVEZ DE DATA
    Public Sub UpdateIntoSustitucion(ByVal Tabla As String, ByVal Codigo As Integer, ByVal Descripcion As String) ' Implements Servers.CreateTables.UpdateIntoSustitucion
        CreateTablesFactory.UpdateIntoSustitucion(Tabla, Codigo, Descripcion)
    End Sub

    Public Sub New()

    End Sub
    Public Sub CreateFriendlyView(ByVal DocType As DocType)
        CreateTablesFactory.CreateFriendlyView(DocType)
    End Sub


End Class
