Imports ZAMBA.AppBlock
Imports ZAMBA.Servers

Public Class DocCount
    Dim CantidadDoc As Int32

    Dim dsDocuments As New DsDocumentsType
    'Public Function Count(ByVal DocID) As Int32
    '    Dim strselect As String = "select Count(*) from Doc_T" & DocID
    '    Dim cantidad As Int32
    '    Try
    '        cantidad = Server.Con.ExecuteScalar(Server.Con.ConString, CommandType.Text, strselect)
    '    Catch ex As Exception
    '    End Try
    '    Return cantidad
    'End Function

    Public Function SoloMails() As DsDocumentsType
        Dim I As Int32
        Dim DocID As Int32
        Dim sql As String
        Dim Ds2 As New DataSet
        Try
            For I = 0 To CantidadDoc - 1
                Dim row As DsDocumentsType.DsDocTypesRow = dsDocuments.DsDocTypes.NewDsDocTypesRow
                DocID = CInt(dsDocuments.Documents.Rows(I).Item(2))
                sql = "Select Doc_type_name,(Select count(*) from doc_t" & DocID & ")as Cantidad from doc_type where Doc_type_ID=" & DocID & " and doc_type_name like 'mail%' or doc_type_name like 'Mail%' order by Cantidad"
                Ds2 = Server.Con.ExecuteDataset(CommandType.Text, sql)
                If CInt(Ds2.Tables(0).Rows(0).Item(1)) > 0 Then
                    row.Tipo_de_Documento = CStr(Ds2.Tables(0).Rows(0).Item(0))
                    row.Cantidad = CInt(Ds2.Tables(0).Rows(0).Item(1))
                    dsDocuments.DsDocTypes.Rows.Add(row)
                End If
                dsDocuments.AcceptChanges()
            Next
            dsDocuments.AcceptChanges()
        Catch
        End Try
        Return dsDocuments
    End Function
    Public Function DatosSinMails() As DsDocumentsType
        Dim I As Int32
        Dim DocID As Int32
        Dim sql As String
        Dim Ds2 As New DataSet
        Try
            For I = 0 To CantidadDoc - 1
                Dim row As DsDocumentsType.DsDocTypesRow = dsDocuments.DsDocTypes.NewDsDocTypesRow
                If dsDocuments.Documents.Rows(I).Item(3).ToString.ToUpper.StartsWith("MAIL") = False Then
                    DocID = CInt(dsDocuments.Documents.Rows(I).Item(2))
                    sql = "Select Doc_type_name,(Select count(*) from doc_t" & DocID & ")as Cantidad from doc_type where Doc_type_ID=" & DocID & "  and (Doc_type_name not like 'Mail%' or Doc_type_name not like 'mail%') order by Cantidad"
                    Ds2 = Server.Con.ExecuteDataset(CommandType.Text, sql)
                    If Integer.Parse(Ds2.Tables(0).Rows.Count.ToString()) > 0 Then
                        If CInt(Ds2.Tables(0).Rows(0).Item(1)) > 0 Then
                            row.Tipo_de_Documento = CStr(Ds2.Tables(0).Rows(0).Item(0))
                            row.Cantidad = CInt(Ds2.Tables(0).Rows(0).Item(1))
                            dsDocuments.DsDocTypes.Rows.Add(row)
                        End If
                        dsDocuments.AcceptChanges()
                    End If
                End If
            Next
            dsDocuments.AcceptChanges()
        Catch
        End Try
        Return dsDocuments
    End Function
    Public Function DocumentosPorFechas(ByVal fdesde As Date, ByVal fhasta As Date) As DsDocumentsByDate
        Dim dsdocs As New DsDocumentsByDate
        Try
            Dim I As Int32
            Dim DocID As Int32
            Dim sql As String
            Dim Ds2 As New DataSet
            Try
                For I = 0 To CantidadDoc - 1
                    Dim row As DsDocumentsByDate.DsDocumentsByDateRow = dsdocs.DsDocumentsByDate.NewDsDocumentsByDateRow
                    DocID = CInt(dsDocuments.Documents.Rows(I).Item(2))
                    sql = "Select Doc_type_name as Documento,(Select count(*) from doc_i" & DocID & " where Crdate between " & Server.Con.ConvertDate(fdesde.ToShortDateString) & " and " & Server.Con.ConvertDate(fhasta.ToShortDateString) & ")as Cantidad from doc_type where Doc_type_ID=" & DocID
                    Ds2 = Server.Con.ExecuteDataset(CommandType.Text, sql)
                    If CInt(Ds2.Tables(0).Rows(0).Item(1)) > 0 Then
                        row.Documento = CStr(Ds2.Tables(0).Rows(0).Item(0))
                        row.Cantidad = CInt(Ds2.Tables(0).Rows(0).Item(1))
                        dsdocs.DsDocumentsByDate.Rows.Add(row)
                    End If
                Next
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
        Return dsdocs
    End Function
    Public Function DocumentosPorFechasSinMails(ByVal fdesde As Date, ByVal fhasta As Date) As DsDocumentsByDate
        Try
            Dim I As Int32
            Dim DocID As Int32
            Dim sql As String
            Dim dsdocs As New DsDocumentsByDate
            Dim Ds2 As New DataSet

            Try
                For I = 0 To CantidadDoc - 1
                    Dim row As DsDocumentsByDate.DsDocumentsByDateRow = dsdocs.DsDocumentsByDate.NewDsDocumentsByDateRow
                    DocID = CInt(dsDocuments.Documents.Rows(I).Item(2))
                    sql = "Select Doc_type_name as Documento,(Select count(*) from doc_i" & DocID & " where Crdate between " & Server.Con.ConvertDate(fdesde.ToShortDateString) & " and " & Server.Con.ConvertDate(fhasta.ToShortDateString) & ")as Cantidad from doc_type where Doc_type_ID=" & DocID & " and (Doc_type_name not like 'Mail%' or Doc_type_name not like 'mail%')"
                    Ds2 = Server.Con.ExecuteDataset(CommandType.Text, sql)
                    If CInt(Ds2.Tables(0).Rows(0).Item(1)) > 0 Then
                        row.Documento = CStr(Ds2.Tables(0).Rows(0).Item(0))
                        row.Cantidad = CInt(Ds2.Tables(0).Rows(0).Item(1))
                        dsdocs.DsDocumentsByDate.Rows.Add(row)
                    End If

                Next
            Catch ex As Exception
            End Try
            Return dsdocs

        Catch ex As Exception
        End Try
    End Function
    Public Function Datos(ByVal SinMails As Boolean) As DsDocumentsType
        Dim I As Int32
        Dim DocID As Int32
        Dim sql As String
        Dim Ds2 As New DataSet
        Try
            For I = 0 To CantidadDoc - 1
                Dim row As DsDocumentsType.DsDocTypesRow = dsDocuments.DsDocTypes.NewDsDocTypesRow
                DocID = CInt(dsDocuments.Documents.Rows(I).Item(2))
                sql = "Select Doc_type_name,(Select count(*) from doc_t" & DocID & ")as Cantidad from doc_type where Doc_type_ID=" & DocID & "  and Doc_type_name like 'Mail%' or Doc_type_name like 'mail%' order by Cantidad"
                Ds2 = Server.Con.ExecuteDataset(CommandType.Text, sql)
                If Integer.Parse(Ds2.Tables(0).Rows.Count.ToString()) > 0 Then
                    If CInt(Ds2.Tables(0).Rows(0).Item(1)) > 0 Then
                        row.Tipo_de_Documento = CStr(Ds2.Tables(0).Rows(0).Item(0))
                        row.Cantidad = CInt(Ds2.Tables(0).Rows(0).Item(1))
                        dsDocuments.DsDocTypes.Rows.Add(row)
                    End If
                    dsDocuments.AcceptChanges()
                End If
            Next
            dsDocuments.AcceptChanges()
        Catch
        End Try
        Return dsDocuments
    End Function

    Public Function Datos() As DsDocumentsType
        '  Dim Ds As New DsDocumentstype
        Dim I As Int32
        Dim DocID As Int32
        Dim sql As String
        Dim Ds2 As New DataSet
        Try
            For I = 0 To CantidadDoc - 1
                Dim row As DsDocumentsType.DsDocTypesRow = dsDocuments.DsDocTypes.NewDsDocTypesRow
                DocID = CInt(dsDocuments.Documents.Rows(I).Item(2))
                sql = "Select Doc_type_name,(Select count(*) from doc_t" & DocID & ")as Cantidad from doc_type where Doc_type_ID=" & DocID & " order by Cantidad"
                Ds2 = Server.Con.ExecuteDataset(CommandType.Text, sql)
                If CInt(Ds2.Tables(0).Rows(0).Item(1)) > 0 Then
                    row.Tipo_de_Documento = CStr(Ds2.Tables(0).Rows(0).Item(0))
                    row.Cantidad = CInt(Ds2.Tables(0).Rows(0).Item(1))
                    dsDocuments.DsDocTypes.Rows.Add(row)
                End If
                dsDocuments.AcceptChanges()
            Next
            dsDocuments.AcceptChanges()
        Catch
        End Try
        Return dsDocuments
    End Function
    Public Function DocSinExtension() As DataSet
        '  Dim Ds As New DsDocumentstype
        Dim I As Int32
        Dim DocID As Int32
        Dim sql As String
        Dim Ds2 As New DataSet

        For I = 0 To CantidadDoc - 1
            Dim row As DsDocumentsType.DsDocTypesRow = dsDocuments.DsDocTypes.NewDsDocTypesRow
            DocID = CInt(dsDocuments.Documents.Rows(I).Item(2))
            sql = "Select Doc_type_name,(Select count(*) from doc_t" & DocID & " where doc_file not like '%.%')as Cantidad from doc_type where Doc_type_ID=" & DocID & " order by Doc_type_name"
            Ds2 = Server.Con.ExecuteDataset(CommandType.Text, sql)
            row.Tipo_de_Documento = CStr(Ds2.Tables(0).Rows(0).Item(0))
            row.Cantidad = CInt(Ds2.Tables(0).Rows(0).Item(1))
            dsDocuments.DsDocTypes.Rows.Add(row)
            dsDocuments.AcceptChanges()
        Next
        Return dsDocuments
    End Function
    Private Function GetDocTypesCount() As Int32
        Dim strselect As String = "Select count(*) from Doc_Type"
        Dim count As Int32
        Dim Dstemp As New DataSet
        Try
            count = Server.Con.ExecuteScalar(CommandType.Text, strselect)
            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, "Select doc_type_ID, doc_type_name from doc_type order by doc_type_name")
            Dstemp.Tables(0).TableName = dsDocuments.Documents.TableName
            dsDocuments.Merge(Dstemp)
        Catch
        Finally
            Dstemp.Dispose()
        End Try
        Return count
    End Function
    Public Sub New()
        CantidadDoc = GetDocTypesCount()
    End Sub
End Class
Public Class Document
    Implements IDisposable
    'Public Overloads Function GetDocumentsIndex() As String
    '    Dim path As String = "C:\Informes\DocIndexs.pdf"
    '    If IO.File.Exists("C:\Informes\DocIndexs.pdf") = True Then
    '        IO.File.Delete("C:\Informes\DocIndexs.pdf")
    '    End If
    '    Try
    '        'Dim sql As String = "Select Doc_type.Doc_type_name Documento,Doc_Index.Index_name Indices from Index_R_Doc_Type inner join Doc_type on Index_R_Doc_Type.Doc_type_id=Doc_type.Doc_Type_id inner join doc_index on doc_index.index_id=Index_R_Doc_type.index_id Where Doc_Type.Doc_Type_name not like 'Mail%' order by Doc_type.Doc_type_id"
    '        If IO.Directory.Exists("C:\Informes") = False Then
    '            IO.Directory.CreateDirectory("C:\Informes")
    '        End If
    '        'TODO MAXI: USAR LA DLL QUE COMPRAMOS
    '        '            Dim pdf As New PDFCreate.NewPDF(path)
    '        '           pdf.Create()
    '        '          Dim con As New PDFCreate.NewPDF.DataProvider
    '        Dim config As New ZAMBA.Tools.ApplicationConfig
    '        'Dim constr As String
    '        'If Server.ServerType = Server.DBTYPES.MSSQLServer Or Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
    '        '    ' constr = "Provider=SQLOLEDB.1;Password=" & config.PASSWORD & ";Persist Security Info=True;User ID=" & config.USER & ";Initial Catalog=" & config.DB & ";Data Source=" & config.SERVER & ";Use Procedure for Prepare=1;Auto Translate=" & _
    '        '    ' "True;Packet Size=4096;Workstation ID=" & Server.StationId & ";Use Encryption for Data=False;Tag with column collation when possible=False"
    '        'Else
    '        '    ' constr = "Provider=MSDAORA.1;Data Source=zambaprd;UserID=" & config.USER & ";password=" & config.PASSWORD & ";"
    '        'End If
    '        ' Dim cols() As String = {"Documento", "Indices"}
    '        '    Dim anchors() As Single = {20, 20}
    '        '            pdf.CreateReport("Informe de documentos", constr, sql, PDFCreate.NewPDF.DataProvider.OleDb, cols, anchors)
    '        '           pdf.Dispose()
    '        Return path
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    '    Return path
    'End Function
    Public Overloads Shared Function GetDocumentsIndex() As DataSet
        'Byval Crystal As Boolean
        Dim ds As New DsDocIndex
        Try
            Dim dstemp As DataSet
            dstemp = Server.Con.ExecuteDataset(CommandType.Text, "Select Doc_type.Doc_type_name Documento,Doc_Index.Index_name Indices from Index_R_Doc_Type inner join Doc_type on Index_R_Doc_Type.Doc_type_id=Doc_type.Doc_Type_id inner join doc_index on doc_index.index_id=Index_R_Doc_type.index_id Where Doc_Type.Doc_Type_name not like 'Mail%' order by Doc_type.Doc_type_id")
            dstemp.Tables(0).TableName = ds.Tables(0).TableName
            ds.Merge(dstemp)
            dstemp.Dispose()
        Catch
        End Try
        Return ds
    End Function
    Public Shared Function DocumentsIndexs() As DataSet
        Dim ds As New DsDocIndex
        Try
            Dim dstemp As DataSet
            Dim sql As String = "Select Doc_type.Doc_type_name Documento,Doc_Index.Index_name Indices , Index_R_Doc_Type.MustComplete Obligatorio,Index_R_Doc_Type.ShowLotus ShowLotus from Index_R_Doc_Type inner join Doc_type on Index_R_Doc_Type.Doc_type_id=Doc_type.Doc_Type_id inner join doc_index on doc_index.index_id=Index_R_Doc_type.index_id Where Doc_Type.Doc_Type_name not like 'Mail%' order by Doc_type.Doc_type_id"
            dstemp = Server.Con.ExecuteDataset(CommandType.Text, sql)
            Dim i As Int32
            For i = 0 To dstemp.Tables(0).Rows.Count - 1
                Dim row As DsDocIndex.DsDocIndexRow = ds.DsDocIndex.NewDsDocIndexRow
                row.Documento = dstemp.Tables(0).Rows(i).Item(0).ToString()
                row.Indices = dstemp.Tables(0).Rows(i).Item(1).ToString()
                If Not IsDBNull(dstemp.Tables(0).Rows(i).Item(2)) Then
                    If CInt(dstemp.Tables(0).Rows(i).Item(2)) = 1 Then
                        row.Obligatorio = "Obligatorio"
                    End If
                End If
                If Not IsDBNull(dstemp.Tables(0).Rows(i).Item(3)) AndAlso CInt(dstemp.Tables(0).Rows(i).Item(3)) = 1 Then
                    row.ShowLotus = "Ver en Lotus"
                End If
                ds.Tables(0).Rows.Add(row)
                ds.AcceptChanges()
            Next
            Return ds
        Catch
        End Try
    End Function
    Public Sub Dispose() Implements System.IDisposable.Dispose

    End Sub
End Class
