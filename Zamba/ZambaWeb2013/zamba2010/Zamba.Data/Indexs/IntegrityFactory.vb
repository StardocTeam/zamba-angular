Imports Zamba.Servers
Imports zamba.core

Public Class IntegrityFactory
    Public Shared Function getAllDrI() As Hashtable
        Dim dsTemp As DataSet

        If Server.IsOracle Then
            Dim ParValues() As Object = {2}
            'Dim ParNames() As Object = {"io_cursor"}
            ' Dim parTypes() As Object = {5}
            'dsTemp = Server.Con.ExecuteDataset("ZGetIntegridadIndices_pkg.ZGetAllDrI", parValues)
            dsTemp = Server.Con.ExecuteDataset("zsp_index_100.GetAllIndexRDocType", ParValues)
        Else
            Dim ParValues() As Object = {}
            'dsTemp = Server.Con.ExecuteDataset("ZGetAllDrI", ParValues)
            dsTemp = Server.Con.ExecuteDataset("zsp_index_100_GetAllIndexRDocType", ParValues)
        End If


        Dim fila As DataRow
        Dim DtId As Int32 = 0
        Dim Indices As New Hashtable
        Dim DrI As DrI
        Dim ListaDrI As New Hashtable

        DtId = Integer.Parse(dsTemp.Tables(0).Rows(0).Item("doc_type_id"))
        'Empiezo a crear los DrI y ponerlos el la lista
        For Each fila In dsTemp.Tables(0).Rows
            Try
                If DtId = CType(fila.Item("doc_type_id"), Int32) Then
                    Try
                        Indices.Add(Integer.Parse(fila.Item("index_id")), Integer.Parse(fila.Item("index_id")))
                    Catch
                    End Try
                Else
                    'Creo el DrI
                    DrI = New DrI
                    DrI.DtId = DtId
                    DrI.Indices = Indices
                    'Agrego el DrI a la lista
                    ListaDrI.Add(DtId, DrI)
                    'Creo una nueva lista de atributos
                    Indices = New Hashtable
                    'Cargo el Indice que quedo colgado
                    Indices.Add(Integer.Parse(fila.Item("index_id")), Integer.Parse(fila.Item("index_id")))
                    'Cambio el DtId para el corte de control
                    DtId = fila.Item("doc_type_id")
                End If
            Catch ex As Exception
            End Try
        Next

        'Para la ultima fila de DS
        If IsNothing(ListaDrI.Item(DtId)) Then
            Try
                'Creo el DrI
                DrI = New DrI
                DrI.DtId = DtId
                'Agrego el ultimo indice
                'Indices.Add(Integer.Parse(fila.Item("index_id")), Integer.Parse(fila.Item("index_id")))
                DrI.Indices = Indices
                'Agrego el DrI a la lista
                ListaDrI.Add(DtId, DrI)
                'Creo una nueva lista de atributos
            Catch ex As Exception
            End Try
        End If

        Return ListaDrI
    End Function

    'Public Shared Function getAllDocI() As Hashtable
    '    Dim dsTemp As DataSet

    '        if Server.IsOracle then
    '            Dim ParValues() As Object = {2}
    '            'Dim ParNames() As Object = {"io_cursor"}
    '            ' Dim parTypes() As Object = {5}
    '            'dsTemp = Server.Con.ExecuteDataset("ZGetIntegridadIndices_pkg.ZGetColumnsDoc_I", parValues)
    '            dsTemp = Server.Con.ExecuteDataset("zsp_index_100.GetDoc_iColumns", parValues)
    '        Else
    '            Dim ParValues() As Object = {}
    '            'dsTemp = Server.Con.ExecuteDataset("ZGetColumnsDoc_I", ParValues)
    '            dsTemp = Server.Con.ExecuteDataset("zsp_index_100_GetDoc_iColumns", ParValues)
    '        End If


    '    Return crearListaDoc(dsTemp, "DOC_I")

    'End Function

    Public Shared Function getAllDocD() As Hashtable
        Dim dsTemp As DataSet

        If Server.isOracle Then
            Dim ParValues() As Object = {2}
            'Dim ParNames() As Object = {"io_cursor"}
            ' Dim parTypes() As Object = {5}
            'dsTemp = Server.Con.ExecuteDataset("ZGetIntegridadIndices_pkg.ZGetColumnsDoc_D", parValues)
            dsTemp = Server.Con.ExecuteDataset("zsp_index_100.GetDoc_dColumns", ParValues)
        Else
            Dim ParValues() As Object = {}
            'dsTemp = Server.Con.ExecuteDataset("ZGetColumnsDoc_D", ParValues)
            dsTemp = Server.Con.ExecuteDataset("zsp_index_100_GetDoc_dColumns", ParValues)
        End If


        Return crearListaDoc(dsTemp, "DOC_D")

    End Function
    Private Shared Function crearListaDoc(ByVal dsTemp As DataSet, ByVal sColumnaDoc As String) As Hashtable
        Dim fila As DataRow
        Dim dtId As Int32
        Dim DocI As DocI
        Dim Indices As New Hashtable
        Dim listaDocI As New Hashtable
        Dim iIndice, iDocI As Int32

        'inicializo el dtId para el corte de control
        Try
            dtId = Integer.Parse(dsTemp.Tables(0).Rows(0).Item(sColumnaDoc)) '"DOC_I"
        Catch ex As Exception
            dtId = 0
        End Try

        'empiezo a recorrer el dataset para instanciar los objetos DocI
        For Each fila In dsTemp.Tables(0).Rows
            Try
                iDocI = Integer.Parse(Integer.Parse(fila.Item(sColumnaDoc)))
            Catch ex As Exception
                iDocI = 0
            End Try

            Try
                If iDocI > 0 Then
                    If dtId = iDocI Then
                        Try
                            iIndice = Integer.Parse(fila.Item("Columna"))
                        Catch
                            iIndice = 0
                        End Try

                        If iIndice > 0 Then
                            Indices.Add(iIndice, iIndice)
                        End If
                    Else
                        'Creo el doci
                        DocI = New DocI
                        DocI.DtId = dtId
                        DocI.Indices = Indices
                        'Agrego el DocI a la lista
                        listaDocI.Add(dtId, DocI)
                        'Creo la nueva lista de atributos
                        Indices = New Hashtable
                        'Guardo el indice de la fila actual
                        Try
                            iIndice = Integer.Parse(fila.Item("Columna"))
                            Indices.Add(iIndice, iIndice)
                        Catch
                        End Try
                        'cambio el dtid para el corte de control
                        dtId = iDocI
                    End If 'If iDocI > 0 Then
                Else
                End If 'If iDocI > 0 Then
            Catch
            End Try
        Next 'For Each fila In dsTemp.Tables(0).Rows

        'Agrego el ultimo DOCI que quedo colgado
        If IsNothing(listaDocI.Item(dtId)) Then
            'Creo el doci
            DocI = New DocI
            DocI.DtId = dtId
            DocI.Indices = Indices
            'Agrego el DocI a la lista
            listaDocI.Add(dtId, DocI)
            'Creo la nueva lista de atributos
        End If

        Return listaDocI

    End Function
End Class
'STORED PROCEDURES

'create proc ZGetColumnsDoc_D as
'select replace(c.name,'D','') 'Columna', replace(t.name,'DOC_D','') 'DOC_D' 
'from syscolumns c inner join sysobjects t on c.id=t.id 
'where t.type='u' and t.name like 'doc_d%'
'order by t.name ,c.name
'go

'create proc ZGetColumnsDoc_I as
'select replace(c.name,'I','') 'Columna', replace(t.name,'DOC_I','') 'DOC_I' 
'from syscolumns c inner join sysobjects t on c.id=t.id 
'where t.type='u' and t.name like 'doc_i%'
'order by t.name ,c.name
'go

'create proc ZGetAllDrI as
'select t.index_id, t.doc_type_id from INDEX_R_DOC_TYPE t 
'order by doc_type_id, index_id
'GO

'SP ORACLE
'create or replace package ZGetIntegridadIndices_pkg as
'  TYPE t_cursor IS REF CURSOR;
'  procedure ZGetColumnsDoc_D(io_cursor out t_cursor);
'  procedure ZGetColumnsDoc_I (io_cursor out t_cursor);
'  procedure ZGetAllDrI (io_cursor out t_cursor);
'end;
'/

'create or replace package body ZGetIntegridadIndices_pkg as

'  procedure ZGetColumnsDoc_D(io_cursor out t_cursor) IS
'  v_cursor t_cursor;
'  begin
'    open v_cursor for select replace(COLUMN_NAME,'D',''),replace(TABLE_NAME,'DOC_D','') 
'    from user_tab_columns 
'    where TABLE_NAME like'DOC_D%' and COLUMN_NAME like 'D%' order by TABLE_NAME,COLUMN_NAME;
'    io_cursor:=v_cursor;
'  end ZGetColumnsDoc_D;

'  procedure ZGetColumnsDoc_I (io_cursor out t_cursor) IS
'  v_cursor t_cursor;
'  begin
'    open v_cursor for select replace(COLUMN_NAME,'I',''),replace(TABLE_NAME,'DOC_I','') 
'    from user_tab_columns 
'    where TABLE_NAME like'DOC_I%' and COLUMN_NAME like 'I%' order by TABLE_NAME,COLUMN_NAME;
'    io_cursor:=v_cursor;
'  end ZGetColumnsDoc_I;

'  procedure ZGetAllDrI (io_cursor out t_cursor) IS
'  v_cursor t_cursor;
'  begin
'    open v_cursor for select index_id, doc_type_id from INDEX_R_DOC_TYPE  
'    order by doc_type_id, index_id;
'    io_cursor:=v_cursor;
'  end ZGetAllDrI;

'end ZGetIntegridadIndices_pkg;
'/


Public Class DocI
    Public DtId As Int32
    Public Indices As New Hashtable
    Public Sobran As New Hashtable
    Public Faltan As New Hashtable
    Public Function comprobarIndices(ByVal Indices1 As Hashtable) As Boolean
        Dim i As Object
        Dim bFaltan As Boolean = False
        Dim bSobran As Boolean = False

        'Busco los que faltan
        For Each i In Indices1.Values
            Try
                If IsNothing(Indices.Item(i)) Then
                    Try
                        Faltan.Add(i, i)
                    Catch
                    End Try
                    bFaltan = True
                End If
            Catch
            End Try
        Next

        'Busco los que sobran
        For Each i In Indices.Values
            Try
                If IsNothing(Indices1.Item(i)) Then
                    Try
                        Sobran.Add(i, i)
                    Catch
                    End Try
                    bSobran = True
                End If
            Catch
            End Try
        Next

        If bSobran OrElse bFaltan Then
            Return False
        Else
            Return True
        End If
    End Function


End Class

Public Class DrI
    Public DtId As Int32
    Public Indices As New Hashtable
    Public EsIntegroDocI As Boolean
    Public EsIntegroDocD As Boolean

    Public Function comprobarIntegridad(ByVal DocI As DocI, ByVal DocD As DocI) As Boolean
        If IsNothing(DocI) Then
            EsIntegroDocI = False
        Else
            EsIntegroDocI = DocI.comprobarIndices(Indices)
        End If

        If IsNothing(DocD) Then
            EsIntegroDocD = False
        Else
            EsIntegroDocD = DocD.comprobarIndices(Indices)
        End If

        Return (EsIntegroDocI And EsIntegroDocD)
    End Function
End Class

Public Class NegociosIntegridad
    Public lstDrI As Hashtable
    Public lstDocI As Hashtable
    Public lstDocD As Hashtable
    Public ColumnasErroneas As New dsColumnasErroneas
    Public RelacionesNoUsadas As New dsRelacionesNoUsadas
    Public Log As iLog

    'Public Function comprobarIntegridad() As Boolean
    '    Dim bResultado As Boolean = True

    '    lstDrI = IntegrityFactory.getAllDrI
    '    lstDocI = IntegrityFactory.getAllDocI
    '    lstDocD = IntegrityFactory.getAllDocD

    '    Dim DrI As DrI
    '    Dim DocD, DocI As DocI
    '    ' Dim sLog As String

    '    For Each DrI In lstDrI.Values
    '        Try

    '            DocI = lstDocI.Item(DrI.DtId)
    '            DocD = lstDocD.Item(DrI.DtId)

    '            If Not (IsNothing(DocI) And IsNothing(DocD)) Then
    '                If Not DrI.comprobarIntegridad(DocI, DocD) Then
    '                    insertarIndicesErroneos(DrI)
    '                    bResultado = False
    '                End If
    '            Else
    '                LogMsg(Log.AVISO, "El DTID: " & DrI.DtId & " está en la tabla INDEX_R_DOC_TYPE, pero no existe la tabla DOC_I/D")
    '                insertarRelacionesNoUsadas(DrI.DtId)
    '            End If
    '        Catch ex As Exception
    '            '              zamba.core.zclass.raiseerror(ex)
    '        End Try
    '    Next

    '    Return bResultado
    'End Function

    'Private Function insertarIndicesErroneos(ByVal DrI As DrI) As Int32
    '    If Not DrI.EsIntegroDocD Then
    '        Dim DocD As DocI
    '        DocD = lstDocD.Item(DrI.DtId)
    '        If Not IsNothing(DocD) Then
    '            Dim fila As DataRow
    '            Dim iInd As Int32
    '            For Each iInd In DocD.Sobran.Values
    '                fila = ColumnasErroneas.Tables(0).NewRow
    '                fila.Item("Tabla") = "DOC_D" & DrI.DtId.ToString
    '                fila.Item("ColumnaSobrante") = "D" & iInd.ToString
    '                ColumnasErroneas.Tables(0).Rows.Add(fila)
    '                LogMsg(Log.AVISO, "En la tabla DOC_D" & DrI.DtId & " sobra la columna D" & iind.ToString)
    '            Next

    '            For Each iInd In DocD.Faltan.Values
    '                fila = ColumnasErroneas.Tables(0).NewRow
    '                fila.Item("Tabla") = "DOC_D" & DrI.DtId.ToString
    '                fila.Item("ColumnaFaltante") = "D" & iInd.ToString
    '                ColumnasErroneas.Tables(0).Rows.Add(fila)
    '                LogMsg(Log.AVISO, "En la tabla DOC_D" & DrI.DtId & " falta la columna D" & iind.ToString)
    '            Next
    '        End If
    '    End If

    '    If Not DrI.EsIntegroDocI Then
    '        Dim DocI As DocI
    '        DocI = lstDocI.Item(DrI.DtId)
    '        If Not IsNothing(DocI) Then
    '            Dim fila As DataRow
    '            Dim iInd As Int32
    '            For Each iInd In DocI.Sobran.Values
    '                fila = ColumnasErroneas.Tables(0).NewRow
    '                fila.Item("Tabla") = "DOC_I" & DrI.DtId.ToString
    '                fila.Item("ColumnaSobrante") = "I" & iInd.ToString
    '                ColumnasErroneas.Tables(0).Rows.Add(fila)
    '                LogMsg(Log.AVISO, "En la tabla DOC_I" & DrI.DtId & " sobra la columna I" & iind.ToString)
    '            Next

    '            For Each iInd In DocI.Faltan.Values
    '                fila = ColumnasErroneas.Tables(0).NewRow
    '                fila.Item("Tabla") = "DOC_I" & DrI.DtId.ToString
    '                fila.Item("ColumnaFaltante") = "I" & iInd.ToString
    '                ColumnasErroneas.Tables(0).Rows.Add(fila)
    '                LogMsg(Log.AVISO, "En la tabla DOC_I" & DrI.DtId & " falta la columna I" & iind.ToString)
    '            Next
    '        End If
    '    End If

    'End Function

    Private Function insertarRelacionesNoUsadas(ByVal DtId As Int32) As Int32
        Dim fila As DataRow
        fila = RelacionesNoUsadas.Tables(0).NewRow
        fila.Item("Doc_type_id") = DtId
        RelacionesNoUsadas.Tables(0).Rows.Add(fila)
    End Function

    Private Function LogMsg(ByVal iTipo As Int32, ByVal sMensaje As String) As Boolean
        Try
            Return Log.logMensaje(iTipo, sMensaje)
        Catch
            Return False
        End Try
    End Function

End Class

