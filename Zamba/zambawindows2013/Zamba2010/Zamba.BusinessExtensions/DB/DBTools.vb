Imports Zamba.Data
''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Business
''' Class	 : Core.DBTools
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase para trabajar con bases de datos
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	30/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class DBTools
    Inherits ZClass
    Implements IDisposable

    'Public Shared Sub DelDocumentsByVolId()
    '    Try
    '        If InputBox("Ingrese el Password", "Zamba Software").Trim = "Z007" Then
    '            Dim docTypeId as Int64 = InputBox("Ingrese el Id de la entidad", "Zamba Software")
    '            Dim initialVolId As Int32 = InputBox("Ingrese el Id del Volumen Inicial", "Zamba Software")
    '            Dim finalVolId As Int32 = InputBox("Ingrese el Id del Volumen Final", "Zamba Software")

    '            Dim StrSelect As String = "Select Doc_Id from Doc_T" & Doctypeid & " where Disk_GROUP_id >= " & initialVolId & " and Disk_GROUP_id <= " & finalVolId
    '            Dim Ds As New DataSet
    '            Ds = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
    '            Dim i As Int32
    '            Dim Count As Int32
    '            For i = 0 To Ds.Tables(0).Rows.Count - 1
    '                Try
    '                    Dim strdelete As String
    '                    strdelete = "DELETE FROM DOC_T" & Doctypeid & " where doc_id =" & Ds.Tables(0).Rows(i).Item("Doc_ID")
    '                    Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
    '                    strdelete = "DELETE FROM DOC_I" & Doctypeid & " where doc_id =" & Ds.Tables(0).Rows(i).Item("Doc_ID")
    '                    Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
    '                    Count += +1
    '                    MessageBox.Show("Documentos Eliminados: " & Count, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                Catch ex As Exception
    '                    Zamba.Core.ZClass.raiseerror(ex)
    '                End Try
    '            Next
    '            MessageBox.Show("Borrado completado, documentos eliminados: " & Count, "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        End If
    '    Catch ex As Exception
    '        Zamba.Core.ZClass.raiseerror(ex)
    '    End Try
    'End Sub
    'Public Shared Sub DelDocumentsByVolIdandDocId()
    '    Try
    '        If InputBox("Ingrese el Password", "Zamba Software").Trim = "Z007" Then
    '            Dim docTypeId as Int64 = InputBox("Ingrese el Id de la entidad", "Zamba Software")
    '            Dim initialVolId As Int32 = InputBox("Ingrese el Id del Volumen Inicial", "Zamba Software")
    '            Dim finalVolId As Int32 = InputBox("Ingrese el Id del Volumen Final", "Zamba Software")
    '            Dim initialDocId As Int32 = InputBox("Ingrese el Id del Documento Inicial", "Zamba Software")
    '            Dim finalDocId As Int32 = InputBox("Ingrese el Id del Documento Final", "Zamba Software")

    '            Dim StrSelect As String = "Select Doc_Id from Doc_T" & Doctypeid & " where Disk_GROUP_id >= " & initialVolId & " and Disk_GROUP_id <= " & finalVolId & " and doc_id >= " & initialDocId & " and doc_id <= " & finalDocId
    '            Dim Ds As New DataSet
    '            Ds = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
    '            Dim i As Int32
    '            Dim Count As Int32
    '            For i = 0 To Ds.Tables(0).Rows.Count - 1
    '                Try
    '                    Dim strdelete As String
    '                    strdelete = "DELETE FROM DOC_T" & Doctypeid & " where doc_id =" & Ds.Tables(0).Rows(i).Item("Doc_ID")
    '                    Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
    '                    strdelete = "DELETE FROM DOC_I" & Doctypeid & " where doc_id =" & Ds.Tables(0).Rows(i).Item("Doc_ID")
    '                    Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
    '                    Count += +1
    '                    MessageBox.Show("Documentos Eliminados: " & Count, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                Catch ex As Exception
    '                    zamba.core.zclass.raiseerror(ex)
    '                End Try
    '            Next
    '            MessageBox.Show("Borrado completado, documentos eliminados: " & Count, "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        End If
    '    Catch ex As Exception
    '        zamba.core.zclass.raiseerror(ex)
    '    End Try
    'End Sub
    'Public Shared Function CheckIndexByDocType(ByVal DocID As Int32) As ArrayList
    '    Try
    '        Dim Ds As DataSet
    '        Dim Result As New ArrayList
    '        Dim I As Int32

    '        Ds = DBToolsFactory.GetAllDocIByResultid(DocID)
    '        For I = 1 To Ds.Tables(0).Columns.Count - 1
    '            Result.Add(Ds.Tables(0).Columns(I).ColumnName.Substring(1))   'Rows(I).Item(0)))
    '        Next
    '        Return Result
    '    Catch ex As Exception
    '        zamba.core.zclass.raiseerror(ex)
    '    End Try
    '    Return Nothing
    'End Function
    'Public Shared Function VerifyIndex(ByVal DocID As Int32) As ArrayList
    '    Try
    '        Dim Faltantes As New ArrayList
    '        Dim Vector As New ArrayList
    '        Dim i As Int32
    '        if Server.IsOracle then
    '            ''Dim parNames() As String = {"DocId", "io_cursor"}
    '            'Dim parTypes() As Object = {13, 5}
    '            Dim parValues() As Object = {DocID, 2}
    '            Dim Ds As DataSet
    '            Ds = Server.Con.ExecuteDataset("zsp_index_100.GetIndexRDocType",  parValues)
    '            If (Ds.Tables(0).Rows.Count) <> (CheckIndexByDocType(DocID).Count) Then
    '                Dim strIndice As String = "Select * from Doc_I" & DocID
    '                Dim DsIndexName As DataSet
    '                DsIndexName = Server.Con.ExecuteDataset(CommandType.Text, strIndice)
    '                Dim Cant As Int32 = DsIndexName.Tables(0).Columns.Count - 1
    '                For i = 1 To Cant
    '                    Vector.Add(DirectCast(DsIndexName.Tables(0).Columns(i).ColumnName, String))
    '                Next
    '                CheckIndexByDocType(DocID).Sort()
    '                Dim dato As String
    '                For i = 1 To Ds.Tables(0).Rows.Count - 1
    '                    If CheckIndexByDocType(DocID).Count <= i Then
    '                        Faltantes.Add(DirectCast(Ds.Tables(0).Rows(i).Item(0), String))
    '                    Else
    '                        dato = DirectCast(Ds.Tables(0).Rows(i).Item(0), String)
    '                        If CheckIndexByDocType(DocID).BinarySearch(dato) < 0 Then
    '                            Faltantes.Add(dato)
    '                        End If
    '                    End If
    '                Next
    '            Else
    '                'ok
    '            End If
    '        Else   'SQL SERVER
    '            Dim parValues() As Object = {DocID}
    '            Dim Ds As DataSet
    '            Ds = Server.Con.ExecuteDataset("zsp_index_100_GetIndexRDocType", parValues)
    '            If (Ds.Tables(0).Rows.Count) <> (CheckIndexByDocType(DocID).Count) Then
    '                Dim strIndice As String = "Select * from Doc_I" & DocID
    '                Dim DsIndexName As DataSet
    '                Dim DataS As New DataSet
    '                Dim StrIndex As String
    '                DsIndexName = Server.Con.ExecuteDataset(CommandType.Text, strIndice)
    '                Dim Cant As Int32 = DsIndexName.Tables(0).Columns.Count - 1
    '                For i = 1 To Cant
    '                    StrIndex = "Select Index_Name from Doc_Index Where Index_ID=" & DsIndexName.Tables(0).Columns(i).ColumnName.Substring(1)
    '                    DataS = Server.Con.ExecuteDataset(CommandType.Text, StrIndex)
    '                    Vector.Add(DirectCast(DataS.Tables(0).Rows(0).Item(0), String))
    '                Next
    '                'Else
    '                '    'Esta ok
    '            End If
    '            Vector.Sort()
    '            CheckIndexByDocType(DocID).Sort()
    '            Dim dato As String = ""
    '            For i = 1 To Ds.Tables(0).Rows.Count - 1
    '                If CheckIndexByDocType(DocID).Count <= i Then
    '                    Faltantes.Add(DirectCast(Ds.Tables(0).Rows(i).Item(0), String))
    '                Else
    '                    dato = DirectCast(Vector(i), String).Substring(1)
    '                End If
    '                If CheckIndexByDocType(DocID).BinarySearch(dato) < 0 Then
    '                    Faltantes.Add(dato)
    '                End If
    '            Next
    '        End If
    '        Return Faltantes
    '    Catch ex As Exception
    '        Zamba.Core.ZClass.raiseerror(ex)
    '        MessageBox.Show("Ocurrio un error, verifique la entidad seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    End Try
    '    Return Nothing
    'End Function




    Public Shared Sub ReEnumerarColumna(ByVal Tabla As String, ByVal Columna As String)
        DBToolsFactory.reenumerarcolumna(Tabla, Columna)
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Actualiza la cantidad de documentos insertados para cada Entidad
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	30/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub ContarDocTypes()
        DBToolsFactory.contardoctypes()
    End Sub
    Public Shared Function GetDocCount(ByVal ID As Int32) As Int32
        Return DBToolsFactory.getdoccount(ID)
    End Function
    Public Shared Function GetDocCount() As DataSet
        Return DBToolsFactory.getdoccount
    End Function

    Public Shared Function GetActiveDatabase() As String
        Return DBToolsFactory.GetActiveDatabase
    End Function

    Public Shared Function GetServerType() As String
        Return DBToolsFactory.GetServerType
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Lee el archivo App.ini y completa los valores de la ultima conexion configurada
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetActualConfig() As ArrayList

        Dim array As ArrayList = New ArrayList()
        array.Add(Servers.Server.AppConfig.SERVER)
        array.Add(Zamba.Servers.Server.AppConfig.DB)
        array.Add(Zamba.Servers.Server.AppConfig.USER)
        array.Add(Zamba.Servers.Server.AppConfig.PASSWORD)
        array.Add(Zamba.Servers.Server.AppConfig.WIN_AUTHENTICATION)
        Return array
    End Function


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Establece la propiedad del archivo Readonly segun el valor recibido por parametro
    ''' </summary>
    ''' <param name="Valor">True,False</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Shared Sub SetReadOnlyAPP(ByVal Valor As Boolean)
        Dim f As IO.FileInfo
        Try
            If IO.File.Exists(".\app.ini") Then
                f = New IO.FileInfo(".\app.ini")

                If Valor = True Then
                    f.Attributes = IO.FileAttributes.ReadOnly
                Else
                    f.Attributes = IO.FileAttributes.Normal
                End If
                f = Nothing
            End If
        Finally
            f = Nothing
        End Try
    End Sub

    Public Overrides Sub Dispose()

    End Sub
End Class
