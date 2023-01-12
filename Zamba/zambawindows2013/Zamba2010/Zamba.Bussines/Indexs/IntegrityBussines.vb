Imports Zamba.Servers
Imports zamba.core

Public Class IntegrityBusiness
    Public Shared Function getAllDrI() As Hashtable
        Return IntegrityBusiness.getAllDrI
    End Function
    Public Shared Function getAllDocI() As Hashtable
        Return IntegrityBusiness.getAllDocI
    End Function
    Public Shared Function getAllDocD() As Hashtable
        Return IntegrityBusiness.getAllDocD
    End Function
    Private Shared Function crearListaDoc(ByVal dsTemp As DataSet, ByVal sColumnaDoc As String) As Hashtable
        Return IntegrityBusiness.crearListaDoc(dsTemp, sColumnaDoc)
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
    Public Atributos As New Hashtable
    Public Sobran As New Hashtable
    Public Faltan As New Hashtable
    Public Function comprobarIndices(ByVal Indices1 As Hashtable) As Boolean
        Dim i As Object
        Dim bFaltan As Boolean = False
        Dim bSobran As Boolean = False

        'Busco los que faltan
        For Each i In Indices1.Values
            Try
                If IsNothing(Atributos.Item(i)) Then
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
        For Each i In Atributos.Values
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
    Public Atributos As New Hashtable
    Public EsIntegroDocI As Boolean
    Public EsIntegroDocD As Boolean

    Public Function comprobarIntegridad(ByVal DocI As DocI, ByVal DocD As DocI) As Boolean
        If IsNothing(DocI) Then
            EsIntegroDocI = False
        Else
            EsIntegroDocI = DocI.comprobarIndices(Atributos)
        End If

        If IsNothing(DocD) Then
            EsIntegroDocD = False
        Else
            EsIntegroDocD = DocD.comprobarIndices(Atributos)
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

    '    lstDrI = IntegrityBusiness.getAllDrI
    '    lstDocI = IntegrityBusiness.getAllDocI
    '    lstDocD = IntegrityBusiness.getAllDocD

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

