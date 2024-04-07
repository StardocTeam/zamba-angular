Imports Zamba.Servers
Imports Zamba.Core
Imports Zamba.Data
Imports Zamba.Servers.Server
Imports System.Text
Imports System.Collections.Generic
Imports System.Data.SqlClient

Public Class Results_Factory

#Region "Tables & Names"

    Public Enum TableType
        Document
        Indexs
        'X
        'XD
        'Full
        Blob
    End Enum

    Public Shared Function MakeTable(ByVal docTypeId As Integer, ByVal tableType As TableType) As String
        Dim TableName As String = String.Empty

        Select Case tableType
            Case Core.TableType.Full
                TableName = "Doc" & docTypeId.ToString
            Case Core.TableType.Document
                TableName = "Doc_T" & docTypeId.ToString
            Case Core.TableType.Indexs
                TableName = "Doc_I" & docTypeId.ToString
            Case Core.TableType.Blob
                TableName = "Doc_B" & docTypeId.ToString
        End Select

        Return TableName
    End Function

    Public Shared Function LoadFileFromDB(ByVal DocId As Long, ByVal DocTypeId As Long) As Byte()
        'todo Implementar blob en oracle
        If Server.isOracle Then
            Return Nothing
        Else
            Dim sql As String = "SELECT DOCFILE FROM DOC_B" & DocTypeId.ToString & " WHERE DOC_ID = " & DocId.ToString
            Dim File As Byte() = DirectCast(Server.Con.ExecuteScalar(CommandType.Text, sql, 3000), Byte())
            Return File
        End If
    End Function

    'Ezequiel: Se comenta funcionalidad ZIP
    'Public Shared Function GetIfFileIsZipped(ByVal DocId As Long, ByVal DocTypeId As Long) As Boolean

    '    Try
    '        Dim sql As String = "SELECT ZIPPED FROM DOC_B" & DocTypeId.ToString & " WHERE DOC_ID = " & DocId.ToString

    '        If Int32.Parse(Server.Con.ExecuteScalar(CommandType.Text, sql)) = 1 Then
    '            Return True
    '        End If

    '        Return False
    '    Catch ex As Exception
    '        ZClass.raiseerror(ex)
    '    End Try

    '    Return False

    'End Function

    Public Shared Function MakefileName() As String
        'TODO Falta hacer que sea alphanumerico con datos de docid,doctype, etc
        Try
            Dim Strselect As String = "SELECT LASTNAME FROM LSTFNAME"
            Dim Ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, Strselect)
            Dim LastName As String

            If Ds.Tables(0).Rows.Count > 0 Then
                LastName = DirectCast(Ds.Tables(0).Rows(0).Item("LastName"), String).Trim
            Else
                LastName = "1"
            End If

            Dim a As Integer
            If IsNumeric(LastName) Then
                a = LastName + 1
            Else
                a = 0
            End If

            Dim Name As String = (a)
            '  Dim LenName As Integer = Name.Length

            Dim i As Integer
            For i = 1 To Name.Length - 8
                Name = "0" & Name
            Next

            If Ds.Tables(0).Rows.Count > 0 Then
                'UPDATE_LSTFNAME_PKG AS
                'PROCEDURE Update_LSTFNAME
                Dim StrUpdate As String = "UPDATE LSTFNAME SET LASTNAME = '" & Name & "' WHERE LASTNAME = '" & LastName & "'"
                Server.Con.ExecuteNonQuery(CommandType.Text, StrUpdate)
            Else
                Dim Strinsert As String = "INSERT INTO LSTFNAME (LASTNAME) VALUES ('" & Name & "')"
                Server.Con.ExecuteNonQuery(CommandType.Text, Strinsert)
            End If

            Return Name
        Catch ex As Exception
            Throw New Exception("Ocurrio un Error al intentar generar un nombre de documento" & " " & ex.ToString)
        End Try

    End Function
    Public Shared Function MakefileName(ByRef t As Transaction) As String
        'TODO Falta hacer que sea alphanumerico con datos de docid,doctype, etc
        Try
            Dim Strselect As String = "SELECT LASTNAME FROM LSTFNAME"
            Dim Ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, Strselect)
            Dim LastName As String

            If Ds.Tables(0).Rows.Count > 0 Then
                LastName = DirectCast(Ds.Tables(0).Rows(0).Item("LastName"), String).Trim
            Else
                LastName = "1"
            End If

            Dim a As Integer
            If IsNumeric(LastName) Then
                a = LastName + 1
            Else
                a = 0
            End If

            Dim Name As String = (a)
            '  Dim LenName As Integer = Name.Length

            Dim i As Integer
            For i = 1 To Name.Length - 8
                Name = "0" & Name
            Next

            If Ds.Tables(0).Rows.Count > 0 Then
                'UPDATE_LSTFNAME_PKG AS
                'PROCEDURE Update_LSTFNAME
                Dim StrUpdate As String = "UPDATE LSTFNAME SET LASTNAME = '" & Name & "' WHERE LASTNAME = '" & LastName & "'"
                'Server.Con.ExecuteNonQuery(CommandType.Text, StrUpdate)
                t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, StrUpdate)
            Else
                Dim Strinsert As String = "INSERT INTO LSTFNAME (LASTNAME) VALUES ('" & Name & "')"
                'Server.Con.ExecuteNonQuery(CommandType.Text, Strinsert)
                t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, Strinsert)
            End If

            Return Name
        Catch ex As Exception
            Throw New Exception("Ocurrio un Error al intentar generar un nombre de documento" & " " & ex.ToString)
        End Try

    End Function
    Public Shared Function MakefileName(ByVal File As String) As String
        'TODO Falta hacer que sea alphanumerico con datos de docid,doctype, etc
        Try
            Dim Strselect As String = "SELECT LASTNAME FROM LSTFNAME"
            Dim Ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, Strselect)
            Dim LastName As String
            If Ds.Tables(0).Rows.Count > 0 Then
                LastName = DirectCast(Ds.Tables(0).Rows(0).Item("LastName"), String).Trim
            Else
                LastName = "1"
            End If
            Dim a As Integer
            If IsNumeric(LastName) Then
                a = CInt(LastName) + 1
            Else
                a = 0
            End If
            Dim Name As String = (a)
            '  Dim LenName As Integer = Name.Length
            Dim i As Integer
            For i = 1 To Name.Length - 8
                Name = "0" & Name
            Next
            If Ds.Tables(0).Rows.Count > 0 Then
                'UPDATE_LSTFNAME_PKG AS
                'PROCEDURE Update_LSTFNAME
                Dim StrUpdate As String = "UPDATE LSTFNAME SET LASTNAME = '" & Name & "' WHERE LASTNAME = '" & LastName & "'"
                Server.Con.ExecuteNonQuery(CommandType.Text, StrUpdate)
            Else
                Dim Strinsert As String = "INSERT INTO LSTFNAME (LASTNAME) VALUES ('" & Name & "')"
                Server.Con.ExecuteNonQuery(CommandType.Text, Strinsert)
            End If
            Return Name & New IO.FileInfo(File).Extension
        Catch ex As Exception
            Throw New Exception("Ocurrio un Error al intentar generar un nombre de documento" & " " & ex.ToString)
        End Try
    End Function

#End Region

#Region "GetResults"

    Public Function GetDocuments(ByVal DocTypeId As Integer) As DsResults
        Dim DsResults As New DsResults
        Dim TableDoc As String = MakeTable(DocTypeId, TableType.Document)
        Dim StrSelect As String
        StrSelect = "Select * from " & TableDoc
        Dim DSTEMP As DataSet
        DSTEMP = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        DSTEMP.Tables(0).TableName = TableDoc
        DsResults.Merge(DSTEMP)
        Return DsResults
    End Function
    Public Function GetDocumentsCount(ByVal DocTypeId As Integer) As Long
        Dim DsResults As New DsResults
        Dim TableDoc As String = MakeTable(DocTypeId, TableType.Document)
        Dim StrSelect As String
        StrSelect = "Select count(1) from " & TableDoc

        Dim DSTEMP = Server.Con.ExecuteScalar(CommandType.Text, StrSelect)
        Dim result As Long = Long.Parse(DSTEMP.ToString())
        Return result
    End Function

    Public Function GetDocumentsIndexByRowNum(ByVal DocTypeId As Long, ByVal RowNum As Long) As DataTable
        Dim TableDoc As String = MakeTable(DocTypeId, TableType.Indexs)
        Dim oracleQ As String = "Select * From(Select Row_Number() OVER (Order by DOC_ID) rno, e.* From " & TableDoc & " e)Where rno =" & RowNum

        Dim sqlQ As String = "With Base As (SELECT *, ROW_NUMBER() OVER (ORDER BY DOC_ID) RN FROM " & TableDoc & ") SELECT * FROM Base WHERE RN = " & RowNum

        Dim DSTEMP As DataSet
        DSTEMP = Server.Con.ExecuteDataset(CommandType.Text, IIf(Server.isOracle, oracleQ, sqlQ))
        Return DSTEMP.Tables(0)
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un Dataset con los datos de un result a travez de una llamada a la vista
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Marcelo]	28/09/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function getResultsData(ByVal docTypeId As Int32, ByVal indexId As Int32, ByVal genIndex As List(Of ArrayList), Optional ByVal comparateValue As String = "", Optional ByVal searchValue As Boolean = True, Optional ByVal strRestricc As String = "") As DataSet
        Dim strSql As StringBuilder = New StringBuilder()
        strSql.Append("select name as ")
        strSql.Append(Chr(34))
        strSql.Append("Nombre")
        strSql.Append(Chr(34))
        'Datatype 4 = date
        'Datatype 5 = datetime
        For Each index As ArrayList In genIndex
            If index(2).ToString = "4" Then
                strSql.Append(",dateadd(dd,0, datediff(dd,0,I")
                strSql.Append(index(0).ToString())
                strSql.Append("))")
            ElseIf index(2).ToString = "5" Then
                strSql.Append(",convert(varchar,convert(datetime,I")
                strSql.Append(index(0).ToString())
                strSql.Append("),120)")
            Else
                strSql.Append(", I")
                strSql.Append(index(0).ToString())
            End If
            strSql.Append(" as '")
            strSql.Append(index(1).ToString())
            strSql.Append("' ")
        Next
        'strSql.Append(",crdate as ")
        'strSql.Append(Chr(34))
        'strSql.Append("Fecha Creaci?n del Documento")
        'strSql.Append(Chr(34))
        'strSql.Append(",lupdate as ")
        'strSql.Append(Chr(34))
        'strSql.Append("Modificado")
        'strSql.Append(Chr(34))
        'strSql.Append(",NumeroVersion as ")
        'strSql.Append(Chr(34))
        'strSql.Append("Version")
        'strSql.Append(Chr(34))
        strSql.Append(", RTRIM(DISK_VOLUME.DISK_VOL_PATH + '\' + RTRIM(CONVERT(char, DOC")
        strSql.Append(docTypeId.ToString())
        strSql.Append(".DOC_TYPE_ID)) + '\' + RTRIM(CONVERT(char, DOC")
        strSql.Append(docTypeId.ToString())
        strSql.Append(".OFFSET)) + '\' + DOC")
        strSql.Append(docTypeId.ToString())
        strSql.Append(".DOC_FILE) AS fullpath from doc")
        strSql.Append(docTypeId.ToString())
        strSql.Append(" INNER JOIN DISK_VOLUME ON DOC")
        strSql.Append(docTypeId.ToString())
        strSql.Append(".VOL_ID = DISK_VOLUME.DISK_VOL_ID ")

        If (searchValue = True) Then
            strSql.Append("where I")
            strSql.Append(indexId.ToString())
            Dim resultDate As Date
            If (comparateValue.Contains("\") Or comparateValue.Contains("/") AndAlso Date.TryParse(comparateValue, resultDate)) Then
                strSql.Append(" = ")
                strSql.Append(Server.Con.ConvertDateTime(comparateValue))
            Else
                strSql.Append(" = '")
                strSql.Append(comparateValue)
                strSql.Append("'")
            End If
        End If
        If (String.IsNullOrEmpty(strRestricc) = False) Then
            If searchValue = False Then
                strSql.Append("where ")
            Else
                strSql.Append(" and ")
            End If
            strSql.Append(strRestricc)
        End If

        Dim ds As DataSet
        ds = Server.Con.ExecuteDataset(CommandType.Text, strSql.ToString())
        Return ds
    End Function

#End Region

#Region "WebView"


    Public Shared Function GetResults(ByVal DocTypeId As Integer) As DataTable
        Return Server.Con.ExecuteDataset(CommandType.Text, "SELECT * FROM DOC_T" & DocTypeId.ToString).Tables(0)
    End Function


    Public Shared Function getPermisosInsert() As DataTable
        Try
            Return Server.Con.ExecuteDataset(CommandType.Text, "Select GROUPID, OBJID, RType, ADITIONAL from usr_rights where OBJID=98 And RType=1 And ADITIONAL=-1").Tables(0)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un Dataset con los datos de un result a travez de una llamada a la vista
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Marcelo]	28/09/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------

    Public Shared Function getResultsAndPageQueryResults(ByVal PageId As Int16, ByVal PageSize As Int16, ByVal docTypeId As Int32, ByVal indexId As Int64, ByVal genIndex As List(Of ArrayList), Optional ByVal RestrictionAndSortExpression As String = "", Optional ByVal SymbolToReplace As String = "", Optional ByVal BySimbolReplace As String = "", Optional ByRef resultCount As Integer = 0) As DataTable

        Dim TableJoins As Generic.List(Of String)
        Dim strSql As StringBuilder = New StringBuilder()
        Dim Joinstr As StringBuilder

        strSql.Append("select name as ")
        strSql.Append(Chr(34))
        strSql.Append("Nombre")
        strSql.Append(Chr(34))
        'Datatype 4 = date
        'Datatype 5 = datetime
        For Each index As ArrayList In genIndex
            If index(2).ToString = "4" Then
                strSql.Append(",dateadd(dd,0, datediff(dd,0,I")
                strSql.Append(index(0).ToString())
                strSql.Append("))")
            ElseIf index(2).ToString = "5" Then
                strSql.Append(",convert(datetime,I")
                strSql.Append(index(0).ToString())
                strSql.Append(")")
            Else
                'Si es un indice de sustitucion se hace un join con la tabla correspondiente  para obtener el Nombre
                '0 = normal, 1= busqueda 2 = sustitucion
                If Indexs_Factory.GetIndexDropDownType(Int64.Parse(index(0).ToString)) = IndexAdditionalType.AutoSustituci�n Then
                    strSql.Append(", SLST_S")
                    strSql.Append(index(0).ToString())
                    strSql.Append(".DESCRIPCION")
                    If IsNothing(TableJoins) Then
                        Joinstr = New StringBuilder()
                        TableJoins = New Generic.List(Of String)
                        Joinstr.Append("LEFT JOIN SLST_S")
                        Joinstr.Append(index(0).ToString)
                        Joinstr.Append(" ON DOC")
                        Joinstr.Append(docTypeId.ToString)
                        Joinstr.Append(".I" & index(0).ToString & " = ")
                        Joinstr.Append("SLST_S" & index(0).ToString)
                        Joinstr.Append(".CODIGO")

                        TableJoins.Add(Joinstr.ToString)
                    Else
                        Joinstr.Remove(0, Joinstr.Length)
                        Joinstr.Append("LEFT JOIN SLST_S")
                        Joinstr.Append(index(0).ToString)
                        Joinstr.Append(" ON DOC")
                        Joinstr.Append(docTypeId.ToString)
                        Joinstr.Append(".I" & index(0).ToString & " = ")
                        Joinstr.Append("SLST_S" & index(0).ToString)
                        Joinstr.Append(".CODIGO")

                        TableJoins.Add(Joinstr.ToString)
                    End If
                Else
                    strSql.Append(", I")
                    strSql.Append(index(0).ToString())
                End If

            End If
            strSql.Append(" as '")
            strSql.Append(Trim(index(1).ToString))
            strSql.Append("' ")
        Next
        strSql.Append(", REPLACE(RTRIM(DISK_VOLUME.DISK_VOL_PATH + '\' + RTRIM(CONVERT(char, DOC")
        strSql.Append(docTypeId.ToString())
        strSql.Append(".DOC_TYPE_ID)) + '\' + RTRIM(CONVERT(char, DOC")
        strSql.Append(docTypeId.ToString())
        strSql.Append(".OFFSET)) + '\' + DOC")
        strSql.Append(docTypeId.ToString())
        strSql.Append(".DOC_FILE),'" + SymbolToReplace + "','" + BySimbolReplace + "') AS fullpath, doc_type_id, doc_id from doc")
        strSql.Append(docTypeId.ToString())
        strSql.Append(" LEFT JOIN DISK_VOLUME ON DOC")
        strSql.Append(docTypeId.ToString())
        strSql.Append(".VOL_ID = DISK_VOLUME.DISK_VOL_ID ")

        If Not IsNothing(TableJoins) Then
            For Each Item As String In TableJoins
                strSql.Append(Item)
                strSql.Append(" ")
            Next
        End If

        If Not String.IsNullOrEmpty(RestrictionAndSortExpression) Then
            strSql.Append(RestrictionAndSortExpression)
        End If

        Dim reader As IDataReader = Nothing
        Dim con As IConnection
        Dim dt As New DataTable
        Dim dc As DataColumn
        Dim count As Int32 = 0

        Try
            con = Server.Con
            reader = con.ExecuteReader(CommandType.Text, strSql.ToString)

            While (reader.Read())
                count += 1
                If PageId = 1 Then
                    If count <= PageSize Then
                        Dim values(reader.FieldCount - 1) As Object
                        reader.GetValues(values)
                        If dt.Columns.Count < 1 Then
                            For index As Integer = 0 To values.Length - 1
                                dc = New DataColumn(reader.GetSchemaTable.Rows(index).Item("ColumnName"), reader.GetSchemaTable.Rows(index).Item("DataType"))
                                dt.Columns.Add(dc)
                            Next
                        End If
                        dt.LoadDataRow(values, False)
                    End If
                ElseIf PageId >= 1 Then
                    If (count > PageSize * (PageId - 1)) AndAlso (count <= (PageSize * PageId)) Then
                        Dim values(reader.FieldCount - 1) As Object
                        reader.GetValues(values)
                        If dt.Columns.Count < 1 Then
                            For index As Integer = 0 To values.Length - 1
                                dc = New DataColumn(reader.GetSchemaTable.Rows(index).Item("ColumnName"), reader.GetSchemaTable.Rows(index).Item("DataType"))
                                dt.Columns.Add(dc)
                            Next
                        End If
                        dt.LoadDataRow(values, False)
                    End If
                End If
            End While
        Finally
            If Not IsNothing(reader) Then
                If reader.IsClosed = False Then
                    reader.Close()
                End If
                reader.Dispose()
                reader = Nothing
            End If
            If Not IsNothing(con) Then
                con.Close()
                con.dispose()
                con = Nothing
            End If
        End Try

        resultCount = count

        Return dt

    End Function

#End Region

#Region "Insert & Save Document"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="DocTypeID"></param>
    ''' <param name="WfID"></param>
    ''' <remarks></remarks>
    Public Shared Sub AddDocTypeToWF(ByVal DocTypeID As Int32, ByVal WfID As Int32)
        ' Dim sql As String
        'MAXI 14/11/05
        ' Dim initialstep As Int32 = GetInitialStep(WfID)
        'sql = "Update doc_type set Life_Cycle=" & WfID & " where doc_type_id=" & DocTypeID
        'Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        If Server.isOracle Then
            Dim parNames() As String = {"WfId", "DocTypeID"}
            ' Dim parTypes() As Object = {13, 13}
            Dim parValues() As Object = {WfID, DocTypeID}
            Server.Con.ExecuteNonQuery("ZWfUpdDt_Pkg.ZWfUpdDtLCByDtId", parValues)
        Else
            Dim parValues() As Object = {WfID, DocTypeID}
            Try
                Server.Con.ExecuteNonQuery("ZWfUpdDtLCByDtId", parValues)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End If
    End Sub
    Public Shared Sub RemoveDocTypeWF(ByVal DocTypeID As Int32)
        'MAXI 14/11/05
        'Dim sql As String = "Update doc_type set Life_Cycle=0 where doc_type_id=" & DocTypeID
        'Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        If Server.isOracle Then
            Dim parNames() As String = {"WfId", "DocTypeID"}
            ' Dim parTypes() As Object = {13, 13}
            Dim parValues() As Object = {0, DocTypeID}
            Server.Con.ExecuteNonQuery("ZWfUpdDt_Pkg.ZWfUpdDtLCByDtId", parValues)
        Else
            Dim parValues() As Object = {0, DocTypeID}
            Try
                Server.Con.ExecuteNonQuery("ZWfUpdDtLCByDtId", parValues)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End If
    End Sub
    Public Shared Function GetInitialStep(ByVal WFID As Int16) As Int32
        Return Server.Con.ExecuteScalar(CommandType.Text, "Select InitialStepId from WFWorkflow where work_id=" & WFID)
    End Function
    Public Shared Function IsDocTypeInWF(ByVal DocTypeid As Int32) As Boolean
        If IsDBNull(Server.Con.ExecuteScalar(CommandType.Text, "Select Life_Cycle from doc_type where Doc_type_id=" & DocTypeid)) OrElse Server.Con.ExecuteScalar(CommandType.Text, "Select Life_Cycle from doc_type where Doc_type_id=" & DocTypeid) = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

#End Region

#Region "Indexs"


    Public Function CompleteIndexData(ByVal _ResultId As Int64, ByVal DocTypeId As Int64, ByVal Indexs As List(Of IIndex), ByRef con As IConnection, Optional ByVal inThread As Boolean = False) As IDataReader
        Dim Dr As IDataReader = Nothing
        Dim strselect2 As New StringBuilder
        Dim TableIndex As String = MakeTable(DocTypeId, Core.TableType.Full)

        strselect2.Append("Select crdate")
        Dim f As Int16
        For f = 0 To Indexs.Count - 1
            strselect2.Append(", " & TableIndex & ".I" & DirectCast(Indexs(f), Index).ID)
        Next
        strselect2.Append(" from " & TableIndex & " where doc_Id = " & _ResultId)

        If con Is Nothing Then con = Server.Con
        Dr = con.ExecuteReader(CommandType.Text, strselect2.ToString)
        Return Dr
    End Function
    '<Obsolete("Metodo discontinuado", False)> _
    'Public Shared Sub SaveIndexData(ByRef Result As NewResult, ByVal ReindexFlag As Boolean)
    '    Dim Table As String = MakeTable(Result.DocType.ID, TableType.Index)
    '    Dim i As Integer
    '    If ReindexFlag = False Then
    '        ZTrace.WriteLineIf(ZTrace.IsInfo,"reIndexFlag es False")
    '        Dim Columns As String = "DOC_ID"
    '        Dim Values As String = Result.ID
    '        For i = 0 To Result.Indexs.Count - 1
    '            If Result.Indexs(i).ISREFERENCED = True Then
    '                ZTrace.WriteLineIf(ZTrace.IsInfo,"Entro en el Select Case")
    '                Select Case CInt(Result.Indexs(i).type)
    '                    Case 1, 2, 3, 6     ' Es Numerico y Decimal el 6
    '                        If DirectCast(Result.Indexs(i).datatemp, String).Trim <> "" Then
    '                            If IsNumeric(DirectCast(Result.Indexs(i).datatemp, String).Trim) Then
    '                                Columns = Columns & ",I" & DirectCast(Result.Indexs(i).Id, String)
    '                                Values = Values & "," & Replace(DirectCast(Result.Indexs(i).datatemp, String), ",", ".")
    '                            End If
    '                        End If
    '                    Case 4     'Es Fecha 
    '                        If DirectCast(Result.Indexs(i).datatemp, String).Trim <> "" Then
    '                            DirectCast(Result.Indexs(i).datatemp, String).Trim()
    '                            Columns = Columns & ",I" & DirectCast(Result.Indexs(i).Id, String)
    '                            Values = Values & "," & Server.Con.ConvertDate(Result.Indexs(i).datatemp)
    '                        End If
    '                    Case 5
    '                        If DirectCast(Result.Indexs(i).datatemp, String).Trim <> "" Then
    '                            Trim(Result.Indexs(i).datatemp)
    '                            Columns = Columns & ",I" & Result.Indexs(i).Id
    '                            Values = Values & "," & Server.Con.ConvertDateTime(Result.Indexs(i).datatemp)
    '                        End If
    '                    Case 7, 8     'Es Texto
    '                        If DirectCast(Result.Indexs(i).datatemp, String).Trim <> "" Then
    '                            Columns = Columns & ",I" & Result.Indexs(i).Id
    '                            Dim DataLen As Int32 = Len(Result.Indexs(i).datatemp.trim)
    '                            Dim indexLen As Int32 = Result.Indexs(i).len
    '                            If DataLen > indexLen Then
    '                                Result.Indexs(i).datatemp = Result.Indexs(i).datatemp.substring(0, indexLen)
    '                            End If
    '                            Values = Values & ",'" & Result.Indexs(i).datatemp & "'"
    '                        End If
    '                    Case Else
    '                        If DirectCast(Result.Indexs(i).datatemp, String).Trim <> "" Then
    '                            Columns = Columns & ",I" & Result.Indexs(i).Id
    '                            Dim DataLen As Int32 = Result.Indexs(i).datatemp.trim
    '                            Dim indexLen As Int32 = Result.Indexs(i).len
    '                            If DataLen > indexLen Then
    '                                Result.Indexs(i).datatemp = Result.Indexs(i).datatemp.substring(0, indexLen)
    '                            End If
    '                            Values = Values & ",'" & Result.Indexs(i).datatemp & "'"
    '                        End If
    '                End Select
    '                If Result.DocumentalId > 0 Then
    '                    If Result.Indexs(i).Id = Result.DocumentalId Then
    '                        SaveIndexText(Result, Result.Indexs(i).datatemp)
    '                    End If
    '                End If
    '            End If
    '        Next
    '        ZTrace.WriteLineIf(ZTrace.IsInfo,"Salgo del For y del Select Case")
    '        Dim StrInsert As String = "INSERT INTO " & Table & " (" & Columns & ") VALUES (" & Values & ")"
    '        ZTrace.WriteLineIf(ZTrace.IsInfo,StrInsert)
    '        Try
    '            Server.Con.ExecuteNonQuery(CommandType.Text, StrInsert)
    '        Catch ex As Exception
    '            'TODO agarrar este codigo con sqlServer
    '            If ex.ToString.Substring(0, 9) = "ORA-00001" Then
    '                Server.Con.dispose()
    '                Throw New Exception("Clave ?nica violada")
    '            End If
    '        End Try
    '        Columns = Nothing
    '        Values = Nothing
    '        StrInsert = Nothing
    '    Else
    '        Dim strset As New StringBuilder
    '        ZTrace.WriteLineIf(ZTrace.IsInfo,"2do FOR")
    '        For i = 0 To Result.Indexs.Count - 1
    '            If Result.Indexs(i).ISREFERENCED = True Then
    '                Select Case CInt(Result.Indexs(i).type)
    '                    Case 4
    '                        If DirectCast(Result.Indexs(i).datatemp, String).Trim <> "" Then
    '                            strset.Append(",I" & Result.Indexs(i).Id & " = " & Server.Con.ConvertDate(Result.Indexs(i).datatemp))
    '                        Else
    '                            strset.Append(",I" & Result.Indexs(i).Id & " = null")
    '                        End If
    '                    Case 5
    '                        If DirectCast(Result.Indexs(i).datatemp, String).Trim <> "" Then
    '                            strset.Append(",I" & Result.Indexs(i).Id & " = " & Server.Con.ConvertDateTime(Result.Indexs(i).datatemp))
    '                        Else
    '                            strset.Append(",I" & Result.Indexs(i).Id & " = null")
    '                        End If
    '                    Case 7, 8
    '                        If DirectCast(Result.Indexs(i).datatemp, String).Trim <> "" Then
    '                            strset.Append(",I" & Result.Indexs(i).Id & " = '" & Result.Indexs(i).datatemp & "'")
    '                        Else
    '                            strset.Append(",I" & Result.Indexs(i).Id & " = null")
    '                        End If
    '                    Case 1, 2, 3, 6
    '                        If DirectCast(Result.Indexs(i).datatemp, String).Trim <> "" Then
    '                            strset.Append(",I" & Result.Indexs(i).Id & " = " & Replace(Result.Indexs(i).datatemp, ",", "."))
    '                        Else
    '                            strset.Append(",I" & Result.Indexs(i).Id & " = null")
    '                        End If
    '                    Case Else
    '                        If DirectCast(Result.Indexs(i).datatemp, String).Trim <> "" Then
    '                            strset.Append(",I" & Result.Indexs(i).Id & " = " & Replace(Result.Indexs(i).datatemp, ",", "."))
    '                        Else
    '                            strset.Append(",I" & Result.Indexs(i).Id & " = " & Replace(Result.Indexs(i).datatemp, ",", "."))
    '                        End If
    '                End Select
    '                If Result.DocumentalId > 0 Then
    '                    ZTrace.WriteLineIf(ZTrace.IsInfo,"El Documental ID es: " & Result.DocumentalId)
    '                    If Result.Indexs(i).Id = Result.DocumentalId Then
    '                        ZTrace.WriteLineIf(ZTrace.IsInfo,"Llamo al SaveIndexTEXT")
    '                        SaveIndexText(Result, Result.Indexs(i).datatemp)
    '                    End If
    '                End If
    '            End If
    '        Next
    '        Dim StrUpdate As String = "UPDATE " & Table & " SET DOC_ID = " & Result.ID & strset.ToString & " where DOC_ID = " & Result.ID
    '        ZTrace.WriteLineIf(ZTrace.IsInfo,"UPDATE: " & StrUpdate)
    '        Server.Con.ExecuteNonQuery(CommandType.Text, StrUpdate)
    '        strset = Nothing
    '        StrUpdate = Nothing
    '    End If
    'End Sub




    ''''' -----------------------------------------------------------------------------
    ''''' <summary>
    '''''     Persiste los indices de un documento
    ''''' </summary>
    ''''' <param name="Result">Documento con indices a persistir</param>
    ''''' <param name="ReindexFlag">True para Actualizar,False para Agregar</param>
    ''''' <param name="changeEvent">True para disparar el evento</param>
    ''''' <remarks>
    ''''' </remarks>
    ''''' <history>
    ''''' 	[oscar]	07/06/2006	Created
    ''''' </history>
    ''''' -----------------------------------------------------------------------------
    ''<Obsolete("Metodo discontinuado", False)> _
    ''Public Shared Function SaveIndexData(ByRef Result As Result, ByVal ReindexFlag As Boolean, Optional ByVal changeEvent As Boolean = True, Optional ByVal OnlySpecifiedIndexsids As Generic.List(Of Int64) = Nothing) As Boolean
    ''    Dim Table As String = MakeTable(Result.DocType.ID, TableType.Indexs)
    ''    Dim i As Integer
    ''    'TODO:INDEX: Enumeracion de tipos de atributos
    ''    'Este Enum es solo de ejemplo y no pertenece al codigo
    ''    'Enum
    ''    '   Numerico 1,2,3,6,9
    ''    '   Fecha    4
    ''    '   Texto    7,8   
    ''    '            5
    ''    'End Enum
    ''    If ReindexFlag = False Then
    ''        Dim Columns As String = "DOC_ID"
    ''        Dim Values As String = Result.ID
    ''        For i = 0 To Result.Indexs.Count - 1
    ''            If Result.Indexs(0).ISREFERENCED = False Then
    ''                If Not IsNothing(OnlySpecifiedIndexsids) Then
    ''                    For Each SpecifiedIndex As Int32 In OnlySpecifiedIndexsids
    ''                        If SpecifiedIndex = Result.Indexs(i).Id Then
    ''                            SaveIndexDataNoReindex(Result, i, Columns, Values)
    ''                        End If
    ''                    Next
    ''                Else
    ''                    SaveIndexDataNoReindex(Result, i, Columns, Values)
    ''                End If
    ''            End If
    ''        Next
    ''        Dim StrInsert As String = "INSERT INTO " & Table & " (" & Columns & ") VALUES (" & Values & ")"
    ''        Try
    ''            ZTrace.WriteLineIf(ZTrace.IsInfo,StrInsert)
    ''            Server.Con.ExecuteNonQuery(CommandType.Text, StrInsert)
    ''        Catch ex As Exception
    ''            'TODO Falta agarrar este codigo con sqlServer
    ''            If ex.ToString.Substring(0, 9) = "ORA-00001" Then
    ''                Server.Con.dispose()
    ''                Throw New Exception("Unique Constraint Violated")
    ''            End If
    ''        End Try

    ''    Else ' Reindexa

    ''        Dim strset As New StringBuilder
    ''        For i = 0 To Result.Indexs.Count - 1
    ''            If Result.Indexs(0).ISREFERENCED = False Then
    ''                If Not IsNothing(OnlySpecifiedIndexsids) Then
    ''                    For Each SpecifiedIndex As Int32 In OnlySpecifiedIndexsids
    ''                        If SpecifiedIndex = Result.Indexs(i).Id Then
    ''                            SaveIndexDataAndReindex(Result, i, strset)
    ''                        End If
    ''                    Next
    ''                Else
    ''                    SaveIndexDataAndReindex(Result, i, strset)
    ''                End If
    ''            End If
    ''        Next
    ''        Dim StrUpdate As String = "UPDATE " & Table & " SET DOC_ID = " & Result.ID & strset.ToString & " where DOC_ID = " & Result.ID
    ''        ZTrace.WriteLineIf(ZTrace.IsInfo,StrUpdate)
    ''        Server.Con.ExecuteNonQuery(CommandType.Text, StrUpdate)
    ''        strset = Nothing
    ''        StrUpdate = Nothing

    ''    End If
    ''    Return changeEvent
    ''End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Persiste los indices de un documento
    ''' </summary>
    ''' <param name="Result">Documento con indices a persistir</param>
    ''' <param name="ReindexFlag">True para Actualizar,False para Agregar</param>
    ''' <param name="changeEvent">True para disparar el evento</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[oscar]	07/06/2006	Created
    ''' 	[Gaston]    18/05/2009	Modified    Se comento una validaci?n para hacer posible la inserci?n de atributos con datos vac?os
    ''' </history>
    ''' -----------------------------------------------------------------------------
    <Obsolete("Metodo discontinuado", False)>
    Public Sub SaveModifiedIndexData(ByRef _result As IResult, ByVal ReindexFlag As Boolean, Optional ByVal OnlySpecifiedIndexsids As Generic.List(Of Int64) = Nothing)
        Try

            Dim Table As String = MakeTable(_result.DocType.ID, Core.TableType.Indexs)
            Dim i As Integer

            If ReindexFlag = False Then

                Dim Columns As String = "DOC_ID"
                Dim Values As String = _result.ID

                For i = 0 To _result.Indexs.Count - 1

                    If Not IsNothing(OnlySpecifiedIndexsids) Then
                        For Each SpecifiedIndex As Int32 In OnlySpecifiedIndexsids
                            If SpecifiedIndex = DirectCast(_result.Indexs(i), Index).ID Then
                                SaveIndexDataNoReindex(_result, i, Columns, Values)
                                _result.Indexs(i).Data = _result.Indexs(i).DataTemp
                                _result.Indexs(i).dataDescription = _result.Indexs(i).dataDescriptionTemp
                            End If
                        Next
                    Else
                        SaveIndexDataNoReindex(_result, i, Columns, Values)
                        _result.Indexs(i).Data = _result.Indexs(i).DataTemp
                        _result.Indexs(i).dataDescription = _result.Indexs(i).dataDescriptionTemp
                    End If

                Next

                Dim StrInsert As String = "INSERT INTO " & Table & " (" & Columns & ") VALUES (" & Values & ")"

                Try
                    ZTrace.WriteLineIf(ZTrace.IsInfo, StrInsert)
                    Server.Con.ExecuteNonQuery(CommandType.Text, StrInsert)
                Catch ex As Exception
                    'TODO Falta agarrar este codigo con sqlServer
                    If ex.ToString.Substring(0, 9) = "ORA-00001" Then
                        Server.Con.dispose()
                        Throw New Exception("Unique Constraint Violated")
                    End If
                End Try

            Else ' Reindexa

                Dim strset As New StringBuilder

                For i = 0 To _result.Indexs.Count - 1

                    ' Se comento el c?digo porque sino es imposible insertar un ?ndice con valor vac?o
                    'If String.IsNullOrEmpty(DirectCast(_Result.Indexs(i), Zamba.Core.Index).DataTemp) = False Then

                    If Not IsNothing(OnlySpecifiedIndexsids) Then
                        If OnlySpecifiedIndexsids.Contains(DirectCast(_result.Indexs(i), Core.Index).ID) Then
                            SaveIndexDataAndReindex(_result, i, strset)
                            _result.Indexs(i).Data = _result.Indexs(i).DataTemp
                            _result.Indexs(i).dataDescription = _result.Indexs(i).dataDescriptionTemp
                        End If
                    Else
                        SaveIndexDataAndReindex(_result, i, strset)
                        _result.Indexs(i).Data = _result.Indexs(i).DataTemp
                        _result.Indexs(i).dataDescription = _result.Indexs(i).dataDescriptionTemp
                    End If
                    'End If
                Next

                If Not String.IsNullOrEmpty(strset.ToString()) Then
                    Dim StrUpdate As String = "UPDATE " & Table & " SET " & strset.ToString().Remove(0, 1) & " where DOC_ID = " & _result.ID
                    ZTrace.WriteLineIf(ZTrace.IsInfo, StrUpdate)
                    Server.Con.ExecuteNonQuery(CommandType.Text, StrUpdate)
                End If
                strset = Nothing
            End If
        Catch ex As Threading.SynchronizationLockException
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As Threading.ThreadAbortException
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As Threading.ThreadInterruptedException
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As Threading.ThreadStateException
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub




    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Update the specified records of ZSearchValues_DT table.
    ''' </summary>
    ''' <param name="ResultId">Int64</param>
    ''' <param name="i">Int32</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tom?s]     18/03/09    Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    '[Ezequiel] 23/09/09 - Se comecto el metodo ya que cambio el indexado.
    'Public Shared Sub UpdateSearchIndexData(ByVal res As Result, ByVal i As Integer)
    '    ' Elimina todos los registros de un ?ndice y result determinado. 
    '    DeleteSearchIndexData(res.ID, DirectCast(res.Indexs(i), Index).ID)
    '    ' Agrego las modificaciones hechas
    '    If Not String.IsNullOrEmpty(DirectCast(DirectCast(res.Indexs(i), Index).DataTemp, String).Trim) Then
    '        InsertSearchIndexData(DirectCast(DirectCast(res.Indexs(i), Index).DataTemp, String).Trim, _
    '                                res.DocType.ID, _
    '                                res.ID, _
    '                                DirectCast(res.Indexs(i), Index).ID)
    '    End If
    'End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Delete the specified records of ZSearchValues_DT table.
    ''' </summary>
    ''' <param name="ResultId">Result Id</param>
    ''' <param name="IndexId">Index Id</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tom?s]     18/03/09    Created
    '''     [Tomas]     19/03/09    Modified    Se agrega la opci?n para Oracle
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub DeleteSearchIndexData(ByVal ResultId As Int64)

        Try
            Dim parametersValues() As Object = {ResultId}

            If Server.isOracle Then
                Dim parNames() As String = {"varResultId"}
                ' Dim parTypes() As Object = {13}
                Dim parValues() As Object = {ResultId}

                Server.Con.ExecuteNonQuery("zsp_search_100.WordDelete", parValues)
            Else
                Con.ExecuteNonQuery("zsp_search_100_WordDelete", parametersValues)

            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Get date for token --  Zss table.
    ''' </summary>
    ''' <param name="UserId">User Id</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Felipe]     31/01/2022    Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function getUserSessionInfo(ByVal UserId As Int64) As DataTable

        Try
            Dim DSIndexDataLst As New DateTime
            DSIndexDataLst = DateTime.Now().AddDays(1)

            Dim ObjSlect As String = "SELECT * FROM ZSS  " & If(Zamba.Servers.Server.isSQLServer, " WITH(NOLOCK) ", "") & " WHERE USERID = " + UserId.ToString()

            Dim ds = Server.Con.ExecuteDataset(CommandType.Text, ObjSlect)
            Dim dt As DataTable
            If ds IsNot Nothing And ds.Tables.Count > 0 And ds.Tables(0).Rows.Count > 0 Then
                dt = ds.Tables(0)
            End If

            Return dt
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Throw
        End Try


    End Function




    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Insert a new word for search and create the relation to the related
    '''     index, result and doctype.
    ''' </summary>
    ''' <param name="WordId">Word that is going to be inserted to the table</param>
    ''' <param name="IndexId">Index Id</param>
    ''' <param name="ResultId">Result Id</param>
    ''' <param name="DocTypeId">DocType Id</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tom?s]     18/03/09    Created
    '''     [Ezequiel]  18/03/09    Modified - Changed parameters and Values
    '''     [Tom?s]     19/03/09    Modified    Se agrega la opci?n para Oracle
    '''     [Ezequiel]  23/09/09    Modified   Se modifico el indexado, ya no usa mas indexid.
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub InsertSearchIndexData(ByVal Word As String, ByVal DocTypeId As Int64, ByVal ResultId As Int64)
        If Not String.IsNullOrEmpty(Word) Then
            Dim parametersValues() As Object = {Word.ToLower, DocTypeId, ResultId}

            If Server.isOracle Then
                ' Dim parNames() As String = {"varWord", "varDTID", "varResultId"}
                ' Dim parTypes() As Object = {22, 13, 13}


                Server.Con.ExecuteNonQuery("zsp_search_300.WordInsert", parametersValues)
            Else
                Server.Con.ExecuteNonQuery("zsp_search_300_WordInsert", parametersValues)
            End If
        End If
    End Sub

    ''' <summary> Insert a new word for search and create the relation to the related
    '''index, result and doctype.
    ''' </summary>
    ''' <param name="WordId">Word that is going to be inserted to the table</param>
    ''' <param name="IndexId">Index Id</param>
    ''' <param name="ResultId">Result Id</param>
    ''' <param name="DocTypeId">DocType Id</param>
    Public Sub InsertSearchIndexDataService(ByVal TextToIndex As String, ByVal DocTypeId As Int64, ByVal ResultId As Int64, indexid As Int64, t As IDbTransaction)
        If Not String.IsNullOrEmpty(TextToIndex) Then

            Dim strWords As String() = Split(TextToIndex, "?")
            Dim listAux As New ArrayList()

            'Remuevo las palabras repetidas
            For Each palabra As String In strWords
                If palabra.Length > 0 AndAlso Not listAux.Contains(palabra) Then
                    listAux.Add(palabra)

                    Try
                        Dim stringOracle As String = String.Empty
                        Dim stringNull As String = " ISNULL(Count(1),0) "
                        If Servers.Server.isOracle = True Then
                            stringOracle = " FROM DUAL"
                            stringNull = " NVL(COunt(1),0) "
                        End If

                        Dim querySearchValues As String = String.Format(" SELECT  CASE WHEN  (SELECT {2} FROM ZSearchDictionary WHERE Word = '{0}') > 0 THEN -1 ELSE  (SELECT Id FROM ZSearchValues WHERE Word = '{0}') END {1}", palabra, stringOracle, stringNull)
                        Dim idZSearchVal As Object = Server.Con.ExecuteScalar(CommandType.Text, querySearchValues)
                        'Si no se encuentra en ZSearchValues
                        If idZSearchVal Is Nothing OrElse IsDBNull(idZSearchVal) Then

                            If Server.isOracle Then
                                Dim insertZsearch = String.Format("INSERT INTO ZSearchValues (Id,Word) VALUES( Seq_WordId.NEXTVAL,'{0}')", palabra)
                                Server.Con.ExecuteNonQuery(CommandType.Text, insertZsearch)
                            Else
                                Dim insertZsearch = String.Format("INSERT INTO ZSearchValues (Id,Word) VALUES((SELECT ISNULL(Max(Id)+1,1) FROM ZSearchValues),'{0}')", palabra)
                                Server.Con.ExecuteNonQuery(CommandType.Text, insertZsearch)
                            End If

                            Dim querySearchValuesWord As String = String.Format("SELECT Id FROM ZSearchValues WHERE Word = '{0}'", palabra)
                            Dim wordid As Object = Server.Con.ExecuteScalar(CommandType.Text, querySearchValuesWord)


                            Dim insertZsearchDT = "INSERT INTO ZSearchValues_DT (WordId,ResultId,DTID,IndexId) VALUES(" + wordid.ToString + "," + ResultId.ToString + "," + DocTypeId.ToString + "," + indexid.ToString + ")"
                            Server.Con.ExecuteNonQuery(CommandType.Text, insertZsearchDT)

                        ElseIf Int64.Parse(idZSearchVal) > 0 Then

                            Dim valuesCount As String = "SELECT COUNT(1) FROM ZSearchValues_DT  where wordid = " + idZSearchVal.ToString + " and ResultId =" + ResultId.ToString + " and DTID = " + DocTypeId.ToString + " and IndexId = " + indexid.ToString
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Validacion de Existencia: {0}", valuesCount))

                            Dim CountObject As Object = Server.Con.ExecuteScalar(CommandType.Text, valuesCount)

                            If CountObject Is Nothing OrElse IsDBNull(CountObject) OrElse Integer.Parse(CountObject.ToString()) = 0 Then
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("ObjectCount = {0} de : ", CountObject, valuesCount))
                                Dim insertZSearch As String = "INSERT INTO ZSearchValues_DT (WordId,ResultId,DTID,IndexId)  VALUES (" + idZSearchVal.ToString + "," + ResultId.ToString + "," + DocTypeId.ToString + "," + indexid.ToString + ")"
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Insercion: {0}", insertZSearch))
                                Server.Con.ExecuteNonQuery(CommandType.Text, insertZSearch)
                            End If
                        End If
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try

                End If
            Next

        End If
    End Sub
    Private Shared Sub SaveIndexDataNoReindex(ByRef Result As IResult, ByVal i As Integer, ByRef Columns As String, ByRef Values As String)
        Dim _index As Index = DirectCast(Result.Indexs(i), Index)

        'Si el indice es referencial no se utiliza
        If Not _index.isReference Then
            Select Case CInt(_index.Type)
                Case 1, 2, 3, 6, 9    ' Es Numerico y Decimal el 6
                    If Not String.IsNullOrEmpty(_index.DataTemp.Trim) Then
                        If IsNumeric(_index.DataTemp.Trim) Then
                            Columns = Columns & ",I" & _index.ID.ToString
                            Values = Values & "," & Replace(_index.DataTemp, ",", ".")
                        End If
                    End If
                Case 4     'Es Fecha
                    If Not String.IsNullOrEmpty(_index.DataTemp.Trim) Then
                        'CStr(Result.Indexs(i).datatemp).Trim
                        Columns = Columns & ",I" & _index.ID.ToString
                        Values = Values & "," & Server.Con.ConvertDate(_index.DataTemp)
                    End If
                Case 5
                    If Not String.IsNullOrEmpty(_index.DataTemp.Trim) Then
                        'Trim(Result.Indexs(i).datatemp)
                        Columns = Columns & ",I" & _index.ID.ToString
                        Values = Values & "," & Server.Con.ConvertDateTime(_index.DataTemp)
                    End If
                Case 7, 8     'Es Texto
                    If Not String.IsNullOrEmpty(_index.DataTemp.Trim) Then
                        Columns = Columns & ",I" & _index.ID
                        Dim DataLen As Int32 = Len(_index.DataTemp.Trim)
                        Dim indexLen As Int32 = _index.Len
                        If DataLen > indexLen Then
                            _index.DataTemp = _index.DataTemp.Substring(0, indexLen)
                        End If
                        Values = Values & ",'" & _index.DataTemp & "'"
                    End If
                Case Else
                    If Not String.IsNullOrEmpty(_index.DataTemp.Trim) Then
                        Columns = Columns & ",I" & _index.ID.ToString
                        Dim DataLen As Int32 = _index.DataTemp.Trim
                        Dim indexLen As Int32 = _index.Len
                        If DataLen > indexLen Then
                            _index.DataTemp = _index.DataTemp.Substring(0, indexLen)
                        End If
                        Values = Values & ",'" & _index.DataTemp & "'"

                    End If
            End Select
            'If Result.DocumentalId > 0 AndAlso _index.ID = Result.DocumentalId Then
            '    SaveIndexText(Result, _index.DataTemp)
            'End If
        End If
        _index = Nothing
    End Sub
    ''' <summary>
    ''' Arma la consulta para actualizar los indices
    ''' </summary>
    ''' <param name="Result"></param>
    ''' <param name="i"></param>
    ''' <param name="strset"></param>
    ''' <history>   Marcelo Modified 01/09/2009</history>
    ''' <remarks></remarks>
    Private Shared Sub SaveIndexDataAndReindex(ByRef _Result As Result, ByVal i As Integer, ByVal strset As StringBuilder)
        Dim _index As Index = DirectCast(_Result.Indexs(i), Index)

        'Si el indice es referencial no se utiliza
        If Not _index.isReference Then
            If Not IsNothing(_index.DataTemp) Then
                Select Case CInt(_index.Type)
                    Case 4
                        If Not String.IsNullOrEmpty(_index.DataTemp.Trim) Then
                            strset.Append(",I" & _index.ID & " = " & Server.Con.ConvertDate(_index.DataTemp))
                        Else
                            strset.Append(",I" & _index.ID & " = null")
                        End If
                    Case 5
                        If Not String.IsNullOrEmpty(_index.DataTemp.Trim) Then
                            strset.Append(",I" & _index.ID & " = " & Server.Con.ConvertDateTime(_index.DataTemp))
                        Else
                            strset.Append(",I" & _index.ID & " = null")
                        End If
                    Case 7, 8
                        If Not String.IsNullOrEmpty(_index.DataTemp.Trim) Then

                            strset.Append(",I" & _index.ID & " = '" & Results_Factory.EncodeQueryString(_index.DataTemp) & "'")
                        Else
                            strset.Append(",I" & _index.ID & " = null")
                        End If
                    Case 1, 2, 3, 6
                        If Not String.IsNullOrEmpty(_index.DataTemp.Trim) Then
                            strset.Append(",I" & _index.ID & " = " & Replace(_index.DataTemp, ",", "."))
                        Else
                            strset.Append(",I" & _index.ID & " = null")
                        End If
                    Case 9
                        If Not String.IsNullOrEmpty(_index.DataTemp.Trim) Then
                            strset.Append(",I" & _index.ID & " = " & Replace(_index.DataTemp, ",", "."))
                        Else
                            'strset.Append(",I" & _index.ID & " = null")
                        End If
                    Case Else
                        If Not String.IsNullOrEmpty(_index.DataTemp.Trim) Then
                            strset.Append(",I" & _index.ID & " = " & Replace(_index.DataTemp, ",", "."))
                        Else
                            strset.Append(",I" & _index.ID & " = " & Replace(_index.DataTemp, ",", "."))

                        End If
                End Select
                'If _Result.DocumentalId > 0 AndAlso _index.ID = _Result.DocumentalId Then
                '    SaveIndexText(_Result, _index.DataTemp)
                'End If
            End If
        End If
        _index = Nothing
    End Sub

    Public Sub InsertIndexerState(ByVal DocType As Long, ByVal Id As Long, ByVal state As Integer, Optional ByVal t As Transaction = Nothing)
        Dim query As String
        Dim update As Boolean

        If Server.isOracle Then
            query = "SELECT state from ZindexerState WHERE DocType=" & DocType & " AND DocId=" & Id
        Else
            query = "SELECT state from ZindexerState with (nolock) WHERE DocType=" & DocType & " AND DocId=" & Id
        End If
        Dim currentState = Server.Con.ExecuteScalar(CommandType.Text, query)

        If IsDBNull(currentState) OrElse String.IsNullOrEmpty(currentState) Then
            update = False
        Else
            update = True
        End If

        ' si la combinacion de Docid, doctype id y el state = 0 ya existen en esa tabla no hago el insert
        Dim InsertIndexerTable As String = String.Empty
        If isOracle Then
            If update Then
                InsertIndexerTable = "update ZindexerState Set ""DATE"" = sysdate, State = " & state & " WHERE DocType=" & DocType & " And DocId=" & Id
            Else
                InsertIndexerTable = "INSERT INTO ZindexerState(DocType, DocId, ""DATE"", State) VALUES ('" & DocType & "'," & Id & ", sysdate ," & state & ")"
            End If
        Else
            If update Then
                InsertIndexerTable = "update ZindexerState set Date = getdate(), State = " & state & " WHERE DocType=" & DocType & " AND DocId=" & Id
            Else
                InsertIndexerTable = "INSERT INTO ZindexerState(DocType, DocId, Date, State) VALUES ('" & DocType & "'," & Id & ", getdate() ," & state & ")"
            End If
        End If

        If t Is Nothing Then
            Con.ExecuteNonQuery(CommandType.Text, InsertIndexerTable)
        Else
            t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, InsertIndexerTable)
        End If

    End Sub


    <Obsolete("Metodo discontinuado", False)>
    Public Shared Sub SaveIndexText(ByRef Result As NewResult, ByVal Data As String)
        Dim SplitData() As String = Data.Split(" ")
        Dim i As Int32
        Dim Id As Int32
        Dim strinsert As String
        For i = 0 To SplitData.Length - 1
            'TODO comparar con el diccionario de palabras comunes
            '      Document.IndexTextList.Add(SplitData.GetValue(i))
            Try
                Id = CoreData.GetNewID(IdTypes.INDEXTEXT)
                strinsert = "INSERT INTO DOC_XD" & Result.DocType.ID.ToString & " (ID,WORD) VALUES (" & Id.ToString & ",'" & SplitData.GetValue(i).ToString.ToLower & "')"
                Try
                    Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
                Catch
                    'Excepcion por Indice Duplicado no tomo accion
                End Try
                strinsert = "INSERT INTO DOC_X" & Result.DocType.ID & " (ID,DOC_ID) VALUES (" & Id & "," & Result.ID & ")"
                Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
            Catch ex As Exception
                MessageBox.Show("No se pudo generar la indexaci?n de texto", "ZAMBA", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                strinsert = Nothing
            End Try
        Next
        SplitData = Nothing
    End Sub

    Public Sub RegisterDocumentWithAlfanumericIndex(ByRef Result As NewResult, ByVal reIndexFlag As Boolean, ByVal userid As Int64, ByVal isVirtual As Boolean, ByVal isShared As Boolean)
        Dim Con As IConnection = Server.Con(True, False, True)
        Con.Open()

        Dim TableName As String
        Dim InsertQuery As String
        Dim tableQuery As String

        Try
            TableName = MakeTable(Result.DocType.ID, TableType.Document)
            Dim FileLen As Decimal
            If isVirtual = False Then
                Try
                    FileLen = CDec(New IO.FileInfo(Result.NewFile).Length / 1024)
                Catch ex As Exception
                    FileLen = CDec(70 / 1024)
                    ZClass.raiseerror(ex)
                End Try
            End If

            InsertQuery = CreateInsertQuery(Result, TableName, isVirtual, FileLen, userid, isShared)
            Con.ExecuteNonQuery(CommandType.Text, InsertQuery)

            If isVirtual = False Then
                Try
                    'Si el volumen es de tipo base de datos, se guarda el archivo en la DOC_B
                    If Result.Volume.VolumeType = VolumeType.DataBase Then
                        'Ezequiel: Se comenta funcionalidad ZIP
                        InsertIntoDOCB(Result) ', IsZipped)
                    End If

                    Try
                        If Server.isOracle Then
                            Dim parNames() As String = {"VolumeId", "FileSize"}
                            ' Dim parTypes() As Object = {13, 13}
                            Dim parValues() As Object = {Result.Volume.ID, FileLen}
                            Con.ExecuteNonQuery("zsp_volume_100.UpdFilesAndSize", parValues)
                        Else
                            Dim parametersValues() As Object = {Result.Volume.ID, FileLen, Result.DocType.ID}
                            Con.ExecuteNonQuery("zsp_volume_100_UpdFilesSizeAndDocCount", parametersValues)
                        End If
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try

                    Result.Volume.sizelen += FileLen
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            Else
                Try
                    If Server.isOracle Then
                        Dim parNames() As String = {"DocID", "X"}
                        ' Dim parTypes() As Object = {13, 13}
                        Dim parValues() As Object = {Result.DocType.ID, 1}
                        Con.ExecuteNonQuery("zsp_doctypes_100.IncrementsDocType", parValues)
                    Else
                        Dim parametersValues() As Object = {Result.DocType.ID, 1}
                        Con.ExecuteNonQuery("zsp_doctypes_100_IncrementsDocType", parametersValues)
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End If

            TableName = MakeTable(Result.DocType.ID, TableType.Indexs)
            'Dim i As Integer
            'Save Index Values
            If reIndexFlag = False Then
                tableQuery = CreateInsertQueryAlfanumerico(Result, TableName)
            Else
                tableQuery = CreateUpdateQuery(Result, TableName)
            End If

            If Not String.IsNullOrEmpty(tableQuery) Then
                Con.ExecuteNonQuery(CommandType.Text, tableQuery)
            End If

        Catch ex As Exception
            If String.Compare(ex.Message.ToString.Substring(0, 9), "ORA-00001") = 0 OrElse ex.Message.IndexOf("clave duplicada") > 0 OrElse ex.Message.IndexOf("duplicate key") > 0 Then
                Throw New Exception("Clave unica violada")
            End If
            Throw
        Finally
            Con.Close()
            Con.dispose()
            TableName = Nothing
            InsertQuery = Nothing
            tableQuery = Nothing
        End Try
    End Sub

    'Ezequiel: Se comenta funcionalidad ZIP
    Public Sub RegisterDocumentWithAlfanumericIndex(ByRef newRes As NewResult, ByVal reIndexFlag As Boolean, ByVal isVirtual As Boolean, ByRef t As Transaction, ByVal userId As Int64, ByVal isShared As Boolean)
        Dim TableName As String
        Dim InsertQuery As String
        Dim tableQuery As String
        Try
            TableName = MakeTable(newRes.DocType.ID, TableType.Document)
            Dim FileLen As Decimal = newRes.FileLength

            InsertQuery = CreateInsertQuery(newRes, TableName, isVirtual, FileLen, userId, isShared)
            t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, InsertQuery)

            If isVirtual = False Then
                'Si el volumen es de tipo base de datos, se guarda el archivo en la DOC_B
                If newRes.EncodedFile IsNot Nothing Then
                    InsertIntoDOCB(newRes, t) ', isZipped)
                End If

                Try
                    'Actualiza la cantidad de archivos en el volumen
                    If Server.isOracle Then
                        Dim parValues() As Object = {newRes.Volume.ID, FileLen}
                        t.Con.ExecuteNonQuery(t.Transaction, "zsp_volume_100.UpdFilesAndSize", parValues)
                    Else
                        Dim parametersValues() As Object = {newRes.Volume.ID, FileLen, newRes.DocType.ID}
                        t.Con.ExecuteNonQuery(t.Transaction, "zsp_volume_100_UpdFilesSizeAndDocCount", parametersValues)
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try

                newRes.Volume.sizelen += FileLen
            Else
                Try
                    'Actualiza la cantidad en los tipos de documento
                    If Server.isOracle Then
                        Dim parValues() As Object = {newRes.DocType.ID, 1}
                        t.Con.ExecuteNonQuery(t.Transaction, "zsp_doctypes_100.IncrementsDocType", parValues)
                    Else
                        Dim parametersValues() As Object = {newRes.DocType.ID, 1}
                        t.Con.ExecuteNonQuery(t.Transaction, "zsp_doctypes_100_IncrementsDocType", parametersValues)
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End If

            TableName = MakeTable(newRes.DocType.ID, TableType.Indexs)
            'Dim i As Integer
            'Save Index Values
            If reIndexFlag = False Then
                tableQuery = CreateInsertQueryAlfanumerico(newRes, TableName)
            Else
                tableQuery = CreateUpdateQuery(newRes, TableName)
            End If

            t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, tableQuery)

        Catch ex As Exception
            If String.Compare(ex.Message.ToString.Substring(0, 9), "ORA-00001") = 0 OrElse ex.Message.IndexOf("clave duplicada") > 0 Then
                Throw New Exception("Clave unica violada", ex)
            End If
            ZClass.raiseerror(ex)
            Throw
        Finally
            TableName = Nothing
            InsertQuery = Nothing
            tableQuery = Nothing
        End Try
    End Sub

    <Obsolete("Metodo discontinuado", False)>
    Public Sub UpdateRegisterDocument(ByRef Result As NewResult, ByVal ReindexFlag As Boolean, Optional ByVal isvirtual As Boolean = False)

        Dim Con As IConnection = Server.Con(False, False, True)
        Con.Open()

        Try
            Dim Table As String = MakeTable(Result.DocType.ID, TableType.Document)

            Dim FileLen As Decimal
            If isvirtual = True Then
                FileLen = 0
            Else
                Dim Fi As IO.FileInfo
                Fi = New IO.FileInfo(Result.File)
                Try
                    FileLen = CDec(Fi.Length) / 1024
                Catch ex As Exception
                    FileLen = 70 / 1024
                    ZClass.raiseerror(ex)
                End Try
            End If


            Dim Strinsert As New StringBuilder
            Strinsert.Append("UPDATE ")
            Strinsert.Append(Table)
            Strinsert.Append(" SET Disk_Group_Id = " & Result.Volume.ID & ", Vol_Id = " & Result.Volume.ID & ", Doc_File = '" & New IO.FileInfo(Result.NewFile).Name & "', Offset = " & Result.Volume.offset & ", Name = '" & Result.Name & "',ICON_ID = " & Result.IconId & ",original_filename = '" & Result.File & "',Filesize = " & Decimal.Round(FileLen, 3).ToString.Replace(",", ".") & "")
            Strinsert.Append(" where Doc_id = " & Result.ID)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "SQL: " & Strinsert.ToString)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Update en la Doc_T")
            Con.ExecuteNonQuery(CommandType.Text, Strinsert.ToString)

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Actualizo el tama?o")
            Try
                If isvirtual = False Then
                    If Server.isOracle Then
                        Dim parNames() As String = {"VolumeId", "FileSize"}
                        ' Dim parTypes() As Object = {13, 13}
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Parametros del Store")
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "El Volumen es: " & Result.Volume.ID)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "El FileLen es: " & FileLen)
                        Dim parValues() As Object = {Result.Volume.ID, FileLen}
                        Con.ExecuteNonQuery("zsp_volume_100.UpdFilesAndSize", parValues)
                    Else
                        Dim parametersValues() As Object = {Result.Volume.ID, FileLen}
                        Con.ExecuteNonQuery("zsp_volume_100_UpdFilesAndSize", parametersValues)
                    End If
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

            If isvirtual = False Then
                Try
                    'Actualiza la columna MB en doctype
                    'MAXI 14/11/05
                    'Dim sql As String = "Update doc_type set MB=(MB + " & FileLen.Round(FileLen, 3).ToString.Replace(",", ".") & ") where Doc_type_Id=" & Result.DocTypeId
                    'Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                    If Server.isOracle Then
                        Dim parNames() As String = {"TamArch", "DocTypeId"}
                        ' Dim parTypes() As Object = {13, 13}
                        Dim parValues() As Object = {Decimal.Parse(Decimal.Round(FileLen, 3).ToString.Replace(",", ".")), Result.DocType.ID}
                        Con.ExecuteNonQuery("zsp_doctypes_100.UpdMbById", parValues)
                    Else
                        Dim parValues() As Object = {Decimal.Parse(Decimal.Round(FileLen, 3).ToString.Replace(",", ".")), Result.DocType.ID}
                        Con.ExecuteNonQuery("zsp_doctypes_100_UpdMbById", parValues)
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try

                Result.Volume.sizelen += FileLen
                ZTrace.WriteLineIf(ZTrace.IsInfo, "actualizo DocCount en Doc_type")
            End If

            Try
                If Server.isOracle Then
                    Dim parNames() As String = {"DocID", "X"}
                    ' Dim parTypes() As Object = {13, 13}
                    Dim parValues() As Object = {Result.DocType.ID, 1}
                    Con.ExecuteNonQuery("zsp_doctypes_100.IncrementsDocType", parValues)
                Else
                    Dim parametersValues() As Object = {Result.DocType.ID, 1}
                    Con.ExecuteNonQuery("zsp_doctypes_100_IncrementsDocType", parametersValues)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try


            ZTrace.WriteLineIf(ZTrace.IsInfo, "Salgo de RegisterDocumentData(ByRef Result As NewResult) sin excepciones")

        Catch ex As Exception
            'TODO agarrar este codigo con sqlServer
            ' T.Rollback()
            ' T.Dispose()
            If ex.Message.ToString.Substring(0, 9) = "ORA-00001" OrElse ex.Message.ToString.Substring(0, 9) = "clave duplicada" Then
                Con.dispose()
                Throw New Exception("Clave unica violada")
            End If
            Throw
        Finally
            '  T.Dispose()
            Con.Close()
            Con.dispose()
        End Try
    End Sub


    ''' <summary>
    ''' Inserta un registro en la DOC_B
    ''' </summary>
    ''' <param name="newRes"></param>
    ''' <param name="t"></param>
    ''' <remarks>Se utiliza para migrar documentos existentes en servidor a la base de datos</remarks>
    Public Shared Sub InsertResIntoDOCB(ByRef Res As IResult, Optional ByVal isZipped As Boolean = False)

        If Res.EncodedFile IsNot Nothing Then
            'Ezequiel: Se comenta funcionalidad ZIP
            Dim zipped As Integer = 0

            If isZipped Then zipped = 1

            Dim query As String = "INSERT INTO DOC_B" & Res.DocTypeId.ToString & " VALUES(" & Res.ID.ToString & ", @docFile , " & zipped & ")"

            If Server.isOracle Then
                Exit Sub
            Else
                Dim pDocFile As SqlParameter
                Dim sql_version As String = Server.Con.ExecuteScalar(CommandType.Text, "select @@version")

                If sql_version.StartsWith("Microsoft SQL Server  2000") Then
                    pDocFile = New SqlParameter("@docFile", SqlDbType.Image)
                Else
                    pDocFile = New SqlParameter("@docFile", SqlDbType.VarBinary)
                End If

                pDocFile.Value = Res.EncodedFile
                Dim params As IDbDataParameter() = {pDocFile}

                Server.Con.ExecuteNonQuery(CommandType.Text, query, params)
            End If
        End If
    End Sub


    ''' <summary>
    ''' Inserta un registro en la DOC_B
    ''' </summary>
    ''' <param name="newRes"></param>
    ''' <param name="t"></param>
    ''' <remarks>Se utiliza para migrar documentos existentes en servidor a la base de datos</remarks>
    Public Shared Sub InsertIntoDOCB(ByRef Res As IResult, Optional ByVal isZipped As Boolean = False)

        'Ezequiel: Se comenta funcionalidad ZIP
        Dim zipped As Integer = 0

        If isZipped Then zipped = 1

        Dim query As String = "INSERT INTO DOC_B" & Res.DocTypeId.ToString & " VALUES(" & Res.ID.ToString & ", @docFile , " & zipped & ")"

        If Server.isOracle Then
            Throw New NotImplementedException()
        Else
            Dim pDocFile As SqlParameter
            Dim sql_version As String = Server.Con.ExecuteScalar(CommandType.Text, "select @@version")

            If sql_version.StartsWith("Microsoft SQL Server  2000") Then
                pDocFile = New SqlParameter("@docFile", SqlDbType.Image)
            Else
                pDocFile = New SqlParameter("@docFile", SqlDbType.VarBinary)
            End If

            If TypeOf Res Is INewResult Then
                pDocFile.Value = DirectCast(Res, INewResult).EncodedFile
            Else
                pDocFile.Value = Res.EncodedFile
            End If

            Dim params As IDbDataParameter() = {pDocFile}

            Server.Con.ExecuteNonQuery(CommandType.Text, query, params)
        End If
    End Sub


    ''' <summary>
    ''' Inserta un registro en la DOC_B
    ''' </summary>
    ''' <param name="newRes"></param>
    ''' <param name="t"></param>
    ''' <remarks>Se utiliza en la insercion</remarks>
    Public Shared Sub InsertIntoDOCB(ByRef newRes As INewResult, ByRef t As Transaction, Optional ByVal isZipped As Boolean = False)

        'Ezequiel: Se comenta funcionalidad ZIP
        Dim zipped As Integer = 0

        If isZipped Then zipped = 1

        Dim query As String = "INSERT INTO DOC_B" & newRes.DocTypeId.ToString & " VALUES(" & newRes.ID.ToString & ", @docFile , " & zipped & ")"

        If Server.isOracle Then
            Throw New NotImplementedException()
        Else
            Dim pDocFile As SqlParameter
            Dim sql_version As String = Server.Con.ExecuteScalar(CommandType.Text, "select @@version")

            If sql_version.StartsWith("Microsoft SQL Server  2000") Then
                pDocFile = New SqlParameter("@docFile", SqlDbType.Image, newRes.EncodedFile.Length, ParameterDirection.Input, False, 0, 0, String.Empty, DataRowVersion.Current, newRes.EncodedFile)
            Else
                pDocFile = New SqlParameter("@docFile", SqlDbType.VarBinary, newRes.EncodedFile.Length, ParameterDirection.Input, False, 0, 0, String.Empty, DataRowVersion.Current, newRes.EncodedFile)
            End If

            Dim params As IDbDataParameter() = {pDocFile}

            t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, query, params)
        End If
    End Sub

    ''' <summary>
    ''' Updates the result encoded file
    ''' </summary>
    ''' <param name="res"></param>
    ''' <remarks></remarks>
    Public Shared Sub UpdateDOCB(ByVal result As IResult)
        Dim query As String = "UPDATE DOC_B" & result.DocTypeId.ToString & " SET DOCFILE = @docFile WHERE DOC_ID = " & result.ID.ToString

        If Server.isOracle Then
            Throw New NotImplementedException()
        Else
            Dim pDocFile As New SqlParameter("@docFile", SqlDbType.VarBinary)
            pDocFile.Value = result.EncodedFile
            Dim params As IDbDataParameter() = {pDocFile}

            Server.Con.ExecuteNonQuery(CommandType.Text, query, params)
        End If

    End Sub

    ''' <summary>
    ''' Verifica si existe el docid en la doc_b de su entidad
    ''' </summary>
    ''' <param name="docId"></param>
    ''' <param name="docTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ExistsInDOCB(ByVal docId As Long, ByVal docTypeId As Long) As Boolean
        Dim query As String = "select count(1) from doc_b" & docTypeId & " where doc_id = " & docId

        Return (Server.Con.ExecuteScalar(CommandType.Text, query) > 0)
    End Function

    Public Function GetIndexData(ByVal DocTypeId As Int32, ByVal DocId As Int32) As DataSet
        Dim DSIndexDataLst As New DataSet
        Dim TableIndex As String = MakeTable(DocTypeId, Core.TableType.Full)
        'TODO Falta cambiar por Store Procedure
        Dim StrSelect As String = "Select * from " & TableIndex & " where (Doc_Id = " & DocId & ")"
        DSIndexDataLst = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        DSIndexDataLst.Tables(0).TableName = TableIndex
        Return DSIndexDataLst
    End Function

    Public Function GetIndexDataAssociate(ByVal DocTypeId As Int32, ByVal DocId As Int32, ByVal tableType As String) As DataTable
        Dim DSIndexDataLst As New DataSet
        Dim DTIndexDataLst As New DataTable
        Dim TableIndex As String = tableType
        'TODO Falta cambiar por Store Procedure
        Dim StrSelect As String = "Select * from " & TableIndex & " where (Doc_Id = " & DocId & ")"
        DSIndexDataLst = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        DTIndexDataLst = DSIndexDataLst.Tables(0)
        Return DTIndexDataLst
    End Function

    Public Function GetResultsDoshhowtable() As DataTable
        Dim DSIndexDataLst As New DataSet
        Dim DTIndexDataLst As New DataTable
        'TODO Falta cambiar por Store Procedure
        Dim StrSelect As String = "select distinct a.i139600 as 'CuitDespachante',isnull(a.i139579,'Sin Nombre') as 'NombreDespachante', b.i139614 as 'NroGuia'
        ,concat (a.i139573 ,' ', a.i139637 ,' ', l.descripcion,' ', p.descripcion) as 'Direccion'
        from doc_i139074 a 
        inner join doc_i139072 b
        on a.i139600 = b.i139600  
        inner join slst_s26215 l on l.codigo = a.i26215
        inner join slst_s26283 p on p.codigo = a.i26283 
        where  b.I139638 =  'Pendiente Retiro'"
        DSIndexDataLst = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        If DSIndexDataLst IsNot Nothing AndAlso DSIndexDataLst.Tables.Count > 0 AndAlso DSIndexDataLst.Tables(0).Rows.Count > 0 Then
            DTIndexDataLst = DSIndexDataLst.Tables(0)
        End If
        Return DTIndexDataLst
    End Function

    Public Function GetIndexObservaciones(ByVal indexId As Int64, ByVal entityId As Int64, ByVal parentResultId As Int64, ByVal InputObservacion As String, ByVal Evaluation As String) As DataTable
        Try
            Dim DSIndexDataLst As New DataSet
            Dim DTIndexDataLst As New DataTable
            If Evaluation = "true" Then
                Dim StrUpdate As String = "update doc_i" & entityId & " set i" & indexId & " = '" & InputObservacion & "' where doc_id = " & parentResultId & ""
                Server.Con.ExecuteNonQuery(CommandType.Text, StrUpdate)
                Dim StrSelect As String = "Select i" & indexId & " from doc_i" & entityId & " where doc_id = " & parentResultId & ""
                DSIndexDataLst = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
                DTIndexDataLst = DSIndexDataLst.Tables(0)
                Return DTIndexDataLst
            ElseIf Evaluation = "false" Then
                Dim StrSelect As String = "Select i" & indexId & " from doc_i" & entityId & " where doc_id = " & parentResultId & ""
                DSIndexDataLst = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
                DTIndexDataLst = DSIndexDataLst.Tables(0)
                Return DTIndexDataLst
            End If



        Catch ex As Exception

        End Try
    End Function


    Public Function getEntidadObservaciones() As DataTable
        Try
            Dim DSIndexDataLst As New DataSet
            Dim DTIndexDataLst As New DataTable

            Dim StrSelect As String = "select doc_type_id from index_r_doc_Type where index_id = 84"
            DSIndexDataLst = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
            DTIndexDataLst = DSIndexDataLst.Tables(0)
            Return DTIndexDataLst
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Function

    Public Function GetObservaciones(ByVal entityId As Int64, ByVal parentResultId As Int64, ByVal AtributeId As Int64) As DataTable
        Try
            Dim DSIndexDataLst As New DataSet
            Dim DTIndexDataLst As New DataTable

            Dim StrSelect As String = "select * from ZOBS_" & entityId & "_" & AtributeId & " where doc_id = " & parentResultId & " ORDER BY id DESC"
            DSIndexDataLst = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
            DTIndexDataLst = DSIndexDataLst.Tables(0)
            Return DTIndexDataLst
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Function

    Public Function DeletMigracionObservaciones(ByVal entityId As Int64, ByVal AtributeId As Int64) As DataTable
        Try
            Dim DSIndexDataLst As New DataSet
            Dim DTIndexDataLst As New DataTable

            Dim StrSelect As String = "delete ZOBS_" & entityId & "_" & AtributeId & ""
            DSIndexDataLst = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
            DTIndexDataLst = DSIndexDataLst.Tables(0)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Function


    Public Function MigracionObservaciones(ByVal Entidad As Int64) As DataTable
        Try
            Dim DSIndexDataLst As New DataSet
            Dim DTIndexDataLst As New DataTable

            Dim StrSelect As String = "select i84, doc_id from doc_i" & Entidad & " where i84 is not null and i84 like '%-%' union
                                        select (i.crdate || ' - ' || u.name  || ' - ' || i84) i84, i.doc_id
                                        from doc_i" & Entidad & " i inner join doc_t" & Entidad & " t on t.doc_id = i.doc_id 
                                        inner join usrtable u on t.platter_id = u.id
                                        where i84 is not null and i84 not like '%-%'"
            DSIndexDataLst = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
            DTIndexDataLst = DSIndexDataLst.Tables(0)
            Return DTIndexDataLst
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Function

    Public Function GetIndexObservacionesV2(ByVal entityId As Int64, ByVal parentResultId As Int64, ByVal InputObservacion As String, ByVal AtributeId As Int64, ByVal IncrementID As Int64, ByVal User As Int64) As DataTable
        Try
            Dim DSIndexDataLst As New DataSet
            Dim DTIndexDataLst As New DataTable
            Dim Strinsert As String
            If Server.isOracle Then
                Strinsert = "INSERT INTO ZOBS_" & entityId & "_" & AtributeId & " (ID, DOC_ID,USER_ID,DATEOBS, VALUE) VALUES (" & IncrementID & "," & parentResultId & "," & User & ", sysdate,'" & InputObservacion & "')"
            Else
                Strinsert = "INSERT INTO ZOBS_" & entityId & "_" & AtributeId & " (ID, DOC_ID,USER_ID,DATEOBS, VALUE) VALUES (" & IncrementID & "," & parentResultId & "," & User & ", getdate(),'" & InputObservacion & "')"
            End If
            Server.Con.ExecuteNonQuery(CommandType.Text, Strinsert)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Function

    Public Function GetIndexObservacionesV2(ByVal entityId As Int64, ByVal parentResultId As Int64, ByVal InputObservacion As String, ByVal AtributeId As Int64, ByVal IncrementID As Int64, ByVal User As Int64, ByVal Fecha As String) As DataTable
        Try
            Dim DSIndexDataLst As New DataSet
            Dim DTIndexDataLst As New DataTable
            Dim Strinsert As String
            If Server.isOracle Then
                Strinsert = "INSERT INTO ZOBS_" & entityId & "_" & AtributeId & " (ID, DOC_ID,USER_ID,DATEOBS, VALUE) VALUES (" & IncrementID & "," & parentResultId & "," & User & ", " & Server.Con.ConvertDate(Fecha) & ",'" & InputObservacion & "')"
            Else
                Strinsert = "INSERT INTO ZOBS_" & entityId & "_" & AtributeId & " (ID, DOC_ID,USER_ID,DATEOBS, VALUE) VALUES (" & IncrementID & "," & parentResultId & "," & User & ",'" & Fecha & "'," & InputObservacion & "')"
            End If
            Server.Con.ExecuteNonQuery(CommandType.Text, Strinsert)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Function

    Public Function GetIndexObservacionesV2Date(ByVal entityId As Int64, ByVal parentResultId As Int64, ByVal InputObservacion As String, ByVal AtributeId As Int64, ByVal IncrementID As Int64, ByVal User As Int64, ByVal Fecha As String) As DataTable
        Try
            Dim DSIndexDataLst As New DataSet
            Dim DTIndexDataLst As New DataTable
            Dim Strinsert As String
            If Server.isOracle Then
                Strinsert = "INSERT INTO ZOBS_" & entityId & "_" & AtributeId & " (ID, DOC_ID,USER_ID,DATEOBS, VALUE) VALUES (" & IncrementID & "," & parentResultId & "," & User & ", " & Server.Con.ConvertDateTime(Fecha) & ",'" & InputObservacion & "')"
            Else
                Strinsert = "INSERT INTO ZOBS_" & entityId & "_" & AtributeId & " (ID, DOC_ID,USER_ID,DATEOBS, VALUE) VALUES (" & IncrementID & "," & parentResultId & "," & User & ",'" & Fecha & "'," & InputObservacion & "')"
            End If
            Server.Con.ExecuteNonQuery(CommandType.Text, Strinsert)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Function

    Public Function AllReportGeneral(ByVal userid As String) As DataSet
        Dim ds As New DataSet
        Try
            Dim strselect As StringBuilder = New StringBuilder()
            strselect.Append("select * from reporte_general inner join Zvw_USR_Rights_200 on Zvw_USR_Rights_200.Aditional = reporte_general.id where objectid = ")
            strselect.Append(ObjectTypes.ModuleReports)
            strselect.Append(" and USER_ID= ")
            strselect.Append(userid)
            strselect.Append(" and RIGHT_TYPE= ")
            strselect.Append(RightsType.View)
            strselect.Append(" ORDER BY NAME")
            ds = Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString().ToUpper())
            Return ds
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return ds
        End Try
    End Function

    Public Shared Function GetIndexSchema(ByVal DocTypeId As Int32) As DataSet
        Dim dstemp As DataSet
        If Server.isOracle Then
            Dim parNames() As String = {"DocTypeId", "io_cursor"}
            ' Dim parTypes() As Object = {13, 5}
            Dim parValues() As Object = {DocTypeId, 2}
            'dstemp = Server.Con.ExecuteDataset("CLSDOC_GENERACIONINDICES_PKG.GeneracionIndices", parValues)
            dstemp = Server.Con.ExecuteDataset("zsp_index_100.IndexGeneration", parValues)
            dstemp.Tables(0).TableName = "IndexLst"
        Else
            Dim parameters() As Object = {DocTypeId}
            'dstemp = Server.Con.ExecuteDataset("GeneracionIndices", parameters)
            dstemp = Server.Con.ExecuteDataset("zsp_index_100_IndexGeneration", parameters)

            dstemp.Tables(0).TableName = "IndexLst"
        End If
        Return dstemp
    End Function
    <Obsolete("Metodo discontinuado", False)>
    Private Function CreateInsertQuery(ByRef Result As NewResult, ByVal tableName As String, ByVal isVirtual As Boolean, ByVal fileLen As Decimal, ByVal userid As Int64, ByVal isShared As Boolean) As String
        Try
            Dim Query As New StringBuilder
            Query.Append("INSERT INTO ")
            Query.Append(tableName)
            Query.Append(" (Doc_Id, FOLDER_ID, Disk_Group_Id, Platter_Id, Vol_Id, Doc_File, Offset, Doc_type_ID, Name,ICON_ID, Shared, ver_parent_id, version, rootid, original_filename, numeroversion, Filesize) VALUES (")
            Query.Append(Result.ID)
            Query.Append(",0,")
            Query.Append(Result.Disk_Group_Id)
            Query.Append(",")
            Query.Append(userid)
            Query.Append(" ,")
            Query.Append(Result.Volume.ID)
            Query.Append(", '")

            If isVirtual = False Then
                Query.Append(New IO.FileInfo(Result.NewFile).Name)
            ElseIf Not IsNothing(Result.Doc_File) Then
                Query.Append(Result.Doc_File)
            Else
                Query.Append(" ")
            End If
            Query.Append("', ")
            Query.Append(Result.Volume.offset)
            Query.Append(", ")
            Query.Append(Result.DocType.ID)
            Query.Append(", '")
            Query.Append(Results_Factory.EncodeQueryString(Result.Name))
            Query.Append("', ")
            Query.Append(Result.IconId)
            Query.Append(", ")
            Query.Append(CInt(isShared))
            Query.Append(", ")
            Query.Append(Result.ParentVerId)
            Query.Append(", ")
            Query.Append(CInt(Result.HasVersion))
            Query.Append(", ")
            Query.Append(Result.RootDocumentId)
            Query.Append(",' ")
            'Tomas: Se agrega validaci?n en el caso de que el file este en nothing
            If Not IsNothing(Result.File) Then
                'ML: Se agrega replace de ' por error al insertar un documento cuyo Original tiene '
                Query.Append(Result.File.Replace("'", String.Empty))
            End If
            Query.Append("', ")
            Query.Append(Result.VersionNumber)
            Query.Append(", ")
            Query.Append(Decimal.Round(fileLen, 3).ToString.Replace(",", "."))
            Query.Append(") ")

            Return Query.ToString()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return Nothing
    End Function

    <Obsolete("Metodo discontinuado", False)>
    Private Shared Function CreateUpdateQuery(ByRef Result As NewResult, ByVal tableName As String) As String
        Try
            Dim Columns As New StringBuilder

            For Each CurrentIndex As Index In Result.Indexs
                If Not IsNothing(CurrentIndex) Then
                    If CurrentIndex.isReference = False Then
                        Select Case CInt(CurrentIndex.Type)
                            Case 4
                                Columns.Append(",I")
                                Columns.Append(CurrentIndex.ID)
                                If CStr(CurrentIndex.DataTemp).Trim = "" Then
                                    Columns.Append(" = null")
                                Else
                                    Columns.Append(" = ")
                                    Columns.Append(Server.Con.ConvertDate(CurrentIndex.DataTemp))
                                End If
                            Case 5
                                Columns.Append(",I")
                                Columns.Append(CurrentIndex.ID)
                                If CStr(CurrentIndex.DataTemp).Trim = "" Then
                                    Columns.Append(" = null")
                                Else
                                    Columns.Append(" = ")
                                    Columns.Append(Server.Con.ConvertDateTime(CurrentIndex.DataTemp))
                                End If
                            Case 7, 8
                                Columns.Append(",I")
                                Columns.Append(CurrentIndex.ID)
                                If CStr(CurrentIndex.DataTemp).Trim = "" Then
                                    Columns.Append(" = null")
                                Else
                                    Columns.Append(" = '")
                                    Columns.Append(CurrentIndex.DataTemp)
                                    Columns.Append("'")
                                End If
                            Case 1, 2, 3, 6
                                Columns.Append(",I")
                                Columns.Append(CurrentIndex.ID)
                                If CStr(CurrentIndex.DataTemp).Trim = "" Then
                                    Columns.Append(" = null")
                                Else
                                    Columns.Append(" = ")
                                    Columns.Append(Replace(CurrentIndex.DataTemp, ",", "."))
                                End If
                            Case Else
                                Columns.Append(",I")
                                Columns.Append(CurrentIndex.ID)
                                If CStr(CurrentIndex.DataTemp).Trim = "" Then
                                    Columns.Append(" = null")
                                Else
                                    Columns.Append(" = ")
                                    Columns.Append(Replace(CurrentIndex.DataTemp, ",", "."))
                                End If
                        End Select
                        'If Result.DocumentalId > 0 AndAlso CurrentIndex.ID = Result.DocumentalId Then
                        '    SaveIndexText(Result, CurrentIndex.DataTemp)
                        'End If
                        CurrentIndex.Data = CurrentIndex.DataTemp
                        CurrentIndex.dataDescription = CurrentIndex.dataDescriptionTemp

                    End If
                End If
            Next

            Dim UpdateQuery As New StringBuilder
            If Not String.IsNullOrEmpty(Columns.ToString()) Then
                UpdateQuery.Append("UPDATE ")
                UpdateQuery.Append(tableName)
                UpdateQuery.Append(" SET ")
                UpdateQuery.Append(Columns.ToString().Remove(0, 1))
                UpdateQuery.Append(" WHERE DOC_ID = ")
                UpdateQuery.Append(Result.ID)
            End If

            Return UpdateQuery.ToString()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return Nothing
    End Function
    <Obsolete("Metodo discontinuado", False)>
    Private Shared Function CreateInsertQuery(ByRef Result As NewResult, ByVal tableName As String) As String
        Try
            Dim Columns As String = "DOC_ID"
            Dim Values As String = Result.ID

            For Each CurrentIndex As Index In Result.Indexs
                If Not String.IsNullOrEmpty(CurrentIndex.DataTemp) Then

                    Select Case CInt(CurrentIndex.Type)

                        Case 1, 2, 3, 6 ' Es Numerico y Decimal el 6
                            If DirectCast(CurrentIndex.DataTemp, String).Trim <> "" Then
                                If IsNumeric(CStr(CurrentIndex.DataTemp).Trim) Then
                                    Columns = Columns & ",I" & CurrentIndex.ID
                                    Values = Values & "," & Replace(CStr(CurrentIndex.DataTemp), ",", ".")
                                End If
                            End If
                        Case 4 'Es Fecha 
                            If CStr(CurrentIndex.DataTemp).Trim <> "" Then
                                Columns = Columns & ", I" & CurrentIndex.ID
                                Values = Values & ", " & Server.Con.ConvertDate(CurrentIndex.DataTemp)
                            End If
                        Case 5
                            If CStr(CurrentIndex.DataTemp).Trim <> "" Then
                                Columns = Columns & ",I" & CurrentIndex.ID
                                Values = Values & "," & Server.Con.ConvertDateTime(CurrentIndex.DataTemp)
                            End If
                        Case 7, 8 'Es Texto
                            If CStr(CurrentIndex.DataTemp).Trim <> "" Then
                                Columns = Columns & ",I" & CurrentIndex.ID
                                Dim DataLen As Int32 = Len(CurrentIndex.DataTemp.Trim)
                                Dim indexLen As Int32 = CurrentIndex.Len
                                If DataLen > indexLen Then
                                    CurrentIndex.DataTemp = CurrentIndex.DataTemp.Substring(0, indexLen)
                                End If

                                Values = Values & ",'" & CurrentIndex.DataTemp & "'"
                            End If
                        Case Else
                            If CStr(CurrentIndex.DataTemp).Trim <> "" Then
                                Columns = Columns & ",I" & CurrentIndex.ID
                                Dim DataLen As Int32 = CurrentIndex.DataTemp.Trim
                                Dim indexLen As Int32 = CurrentIndex.Len
                                If DataLen > indexLen Then
                                    CurrentIndex.DataTemp = CurrentIndex.DataTemp.Substring(0, indexLen)
                                End If
                                Values = Values & ",'" & CurrentIndex.DataTemp & "'"
                            End If
                    End Select
                    CurrentIndex.Data = CurrentIndex.DataTemp
                    CurrentIndex.dataDescription = CurrentIndex.dataDescriptionTemp
                End If

                'If Result.DocumentalId > 0 AndAlso CurrentIndex.ID = Result.DocumentalId Then
                '    SaveIndexText(Result, CurrentIndex.DataTemp)
                'End If
            Next

            Dim InsertQuery As New StringBuilder
            InsertQuery.Append("INSERT INTO ")
            InsertQuery.Append(tableName)
            InsertQuery.Append(" (")
            InsertQuery.Append(Columns)
            InsertQuery.Append(") VALUES(")
            InsertQuery.Append(Values)
            InsertQuery.Append(")")

            Return InsertQuery.ToString()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return Nothing
    End Function


    '
    ''' <summary>
    ''' Se creo este overload porque cuando se intentaba realizar una insercion en la base de un tipo alfanum?rico,
    ''' blanqueaba datatemp porque realizaba en el case 7 un substring de 0 a 0
    ''' </summary>
    ''' <param name="Result"></param>
    ''' <param name="tableName"></param>
    ''' <returns></returns>
    ''' <remarks>SEBASTI?N</remarks>
    Private Shared Function CreateInsertQueryAlfanumerico(ByRef Result As NewResult, ByVal tableName As String) As String

        Dim columns As String = "DOC_ID"
        Dim idValues As String = Result.ID
        Const I As String = ", I"

        For Each CurrentIndex As Index In Result.Indexs
            If CurrentIndex.isReference = False Then
                If Not String.IsNullOrEmpty(CurrentIndex.DataTemp) Then

                    Select Case CInt(CurrentIndex.Type)

                        Case 1, 2, 3 ' Es Numerico
                            If Not String.IsNullOrEmpty(CurrentIndex.DataTemp.Trim) Then
                                If IsNumeric(CStr(CurrentIndex.DataTemp).Trim) Then
                                    columns = columns & I & CurrentIndex.ID
                                    idValues = idValues & "," & Replace(CStr(CurrentIndex.DataTemp), ",", ".")
                                End If
                            End If
                        Case 6 ' Es Decimal el 6
                            If Not String.IsNullOrEmpty(CurrentIndex.DataTemp.Trim) Then
                                If CurrentIndex.DataTemp.IndexOf(",") <> -1 AndAlso CurrentIndex.DataTemp.IndexOf(".") <> -1 Then
                                    CurrentIndex.DataTemp = CurrentIndex.DataTemp.Replace(".", "")
                                End If
                                If IsNumeric(CStr(CurrentIndex.DataTemp).Trim) Then
                                    columns = columns & I & CurrentIndex.ID
                                    idValues = idValues & "," & Replace(CStr(CurrentIndex.DataTemp), ",", ".")
                                End If
                            End If
                        Case 4 'Es Fecha 
                            If Not String.IsNullOrEmpty(CurrentIndex.DataTemp.Trim) Then
                                columns = columns & I & CurrentIndex.ID
                                idValues = idValues & ", " & Server.Con.ConvertDate(CurrentIndex.DataTemp)
                            End If
                        Case 5
                            If Not String.IsNullOrEmpty(CurrentIndex.DataTemp.Trim) Then
                                columns = columns & I & CurrentIndex.ID
                                idValues = idValues & "," & Server.Con.ConvertDateTime(CurrentIndex.DataTemp)
                            End If
                        Case 7, 8 'Es Texto
                            If Not String.IsNullOrEmpty(CurrentIndex.DataTemp.Trim) Then
                                columns = columns & I & CurrentIndex.ID
                                Dim DataLen As Int32 = Len(CurrentIndex.DataTemp.Trim)
                                Dim indexLen As Int32 = CurrentIndex.Len
                                '[Sebasti?n]solo se cabio mayor por menor para que saltee esta parte, porque hacia un substring
                                'de 0 a 0 y eso banquea datatemp
                                '{Martin} Se volvio a > ya que con menor no funcionaba y daba error, si la longitud de los datos es mayor 
                                'que la longitud maxima del indice, se debe recortar los datos.
                                'todo sebas: mostrar el error de 0 a 0
                                If DataLen > indexLen Then
                                    CurrentIndex.DataTemp = CurrentIndex.DataTemp.Substring(0, indexLen)
                                End If
                                If CurrentIndex.DataTemp.Contains("'") Then
                                    CurrentIndex.DataTemp = CurrentIndex.DataTemp.Replace("'", "?")
                                End If
                                idValues = idValues & ",'" & CurrentIndex.DataTemp & "'"
                            End If
                        Case Else
                            If Not String.IsNullOrEmpty(CurrentIndex.DataTemp.Trim) Then
                                columns = columns & I & CurrentIndex.ID
                                ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Completando Atributo {0} {1} {2} {3}", CurrentIndex.ID, CurrentIndex.Name, CurrentIndex.Type.ToString(), CurrentIndex.DataTemp))
                                Dim DataLen As Int32
                                If CurrentIndex.Type = Zamba.IndexDataType.Si_No Then
                                    If CurrentIndex.DataTemp.Trim = "False" OrElse CurrentIndex.DataTemp.Trim = "false" OrElse CurrentIndex.DataTemp.Trim = "0" OrElse CurrentIndex.DataTemp.Trim = "" OrElse CurrentIndex.DataTemp.Trim = "N" OrElse CurrentIndex.DataTemp.Trim = "NO" OrElse CurrentIndex.DataTemp.Trim = "No" OrElse CurrentIndex.DataTemp.Trim = "no" Then
                                        DataLen = 0
                                    ElseIf CurrentIndex.DataTemp.Trim = "True" OrElse CurrentIndex.DataTemp.Trim = "true" OrElse CurrentIndex.DataTemp.Trim = "1" OrElse CurrentIndex.DataTemp.Trim = "-1" OrElse CurrentIndex.DataTemp.Trim = "S" OrElse CurrentIndex.DataTemp.Trim = "SI" OrElse CurrentIndex.DataTemp.Trim = "Si" OrElse CurrentIndex.DataTemp.Trim = "si" Then
                                        DataLen = -1
                                    Else
                                        DataLen = CurrentIndex.DataTemp.Trim
                                    End If

                                Else
                                    DataLen = CurrentIndex.DataTemp.Trim
                                End If

                                Dim indexLen As Int32 = CurrentIndex.Len
                                If DataLen > indexLen Then
                                    CurrentIndex.DataTemp = CurrentIndex.DataTemp.Substring(0, indexLen)
                                End If
                                If CurrentIndex.Type = Zamba.IndexDataType.Si_No Then
                                    idValues = idValues & "," & DataLen & ""
                                Else
                                    idValues = idValues & ",'" & CurrentIndex.DataTemp & "'"
                                End If
                            End If
                    End Select
                    CurrentIndex.Data = CurrentIndex.DataTemp
                    CurrentIndex.dataDescription = CurrentIndex.dataDescriptionTemp
                End If
            End If

        Next

        Dim InsertQuery As New StringBuilder
        InsertQuery.Append("INSERT INTO ")
        InsertQuery.Append(tableName)
        InsertQuery.Append(" (")
        InsertQuery.Append(columns)
        InsertQuery.Append(") VALUES(")
        InsertQuery.Append(idValues)
        InsertQuery.Append(")")

        Return InsertQuery.ToString()

        Return Nothing
    End Function
#End Region

#Region "CompleteDocument"
    <Obsolete("Metodo discontinuado", False)>
    Public Shared Sub aCompleteDocument(ByRef Document As Zamba.Core.Result)
        Try
            '    Dim DocFile As String = Document.Doc_File
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Dim DocTable As String = "DOC_T" & Document.DocType.ID

        Dim StrSelect As String = "SELECT " & DocTable & ".ICON_ID AS ICONID," & DocTable & ".DISK_GROUP_ID," & DocTable & ".DOC_FILE," & DocTable & ".OFFSET,DISK_VOLUME.DISK_VOL_PATH FROM DISK_VOLUME, " & DocTable & " WHERE DISK_VOLUME.DISK_VOL_ID = " & DocTable & ".Disk_Group_Id AND DOC_ID = " & Document.ID

        Dim DsTemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)

        Document.IconId = DsTemp.Tables(0).Rows(0).Item("IconId")
        Document.Doc_File = DsTemp.Tables(0).Rows(0).Item("DOC_FILE")
        Document.Disk_Group_Id = DsTemp.Tables(0).Rows(0).Item("DISK_GROUP_ID")
        Document.OffSet = DsTemp.Tables(0).Rows(0).Item("OFFSET")
        Document.DISK_VOL_PATH = DsTemp.Tables(0).Rows(0).Item("DISK_VOL_PATH")
    End Sub
    ''' <summary>
    ''' Completa el result
    ''' </summary>
    ''' <param name="_Result">Result a ser completado</param>
    ''' <param name="inThread">Las tablas de sustitucion se cargan en thread?</param>
    ''' <History>Marcelo    Modified    07/08/09</History>
    ''' <history> Marcelo modified 20/08/2009 </history>
    ''' <remarks></remarks>
    Public Function CompleteDocument(ByVal ResultId As Int64, ByVal docTypeId As Int64, ByVal indexs As Index(), Optional ByVal inThread As Boolean = False) As DataRow


        Dim separator As String = If(Server.isOracle, "||", "+")

        Dim strTableI As String = MakeTable(docTypeId, TableType.Indexs)
        Dim strTableT As String = MakeTable(docTypeId, TableType.Document)
        Dim MainJoin As String = String.Format("{0} T inner join {1} I on T.doc_id = I.doc_id", strTableT, strTableI)


        Dim strselect As StringBuilder = New StringBuilder()

        strselect.Append("SELECT ")
        strselect.Append("T.DOC_ID, ")
        strselect.Append("T.DISK_GROUP_ID,PLATTER_ID,VOL_ID,DOC_FILE,OFFSET,")
        strselect.Append("T.DOC_TYPE_ID, ")
        strselect.Append("T.NAME as ")
        strselect.Append(Chr(34))
        strselect.Append("Nombre")
        strselect.Append(Chr(34))
        strselect.Append(",")
        strselect.Append("T.ICON_ID,SHARED,ver_Parent_id,RootId,original_Filename as ")
        strselect.Append(Chr(34))
        strselect.Append("Original")
        strselect.Append(Chr(34))
        strselect.Append(",Version, NumeroVersion as ")
        strselect.Append(Chr(34))
        strselect.Append("Numero de Version")
        strselect.Append(Chr(34))
        strselect.Append(",disk_Vol_id, DISK_VOL_PATH, doc_type_name as ")
        strselect.Append(Chr(34))
        strselect.Append("Entidad")
        strselect.Append(Chr(34))
        strselect.Append(", ")
        strselect.Append("I.crdate as ")
        strselect.Append(Chr(34))
        strselect.Append("Creado")
        strselect.Append(Chr(34))
        strselect.Append(", ")
        strselect.Append("I.lupdate as ")
        strselect.Append(Chr(34))
        strselect.Append("Modificado")
        strselect.Append(Chr(34))
        strselect.Append(", '' as THUMB")
        For Each _Index As Index In indexs
            strselect.Append(", ")
            strselect.Append("I.I")
            strselect.Append(_Index.ID)
            If _Index.DropDown <> IndexAdditionalType.AutoSustituci�n AndAlso _Index.DropDown <> IndexAdditionalType.AutoSustituci�nJerarquico Then
                strselect.Append(" as ")
                strselect.Append(Chr(34))
                strselect.Append(_Index.Name)
                strselect.Append(Chr(34))
            End If
        Next
        strselect.Append(String.Format(" ,(u.APELLIDO {0} ' ' {0} u.NOMBRES) AS Asignado ", separator))
        strselect.Append(" ,s.NAME AS Etapa ")
        'FROM
        strselect.Append(" FROM ")
        strselect.Append(MainJoin)
        strselect.Append(" inner join doc_type on doc_type.doc_type_id = ")
        strselect.Append("T.doc_type_id left outer join disk_Volume on disk_Vol_id=vol_id ")
        'THUMBS
        ' strselect.Append("left outer join ZThumb on ")
        '  strselect.Append("T.DOC_ID= ZThumb.DOC_ID and T.DOC_TYPE_ID=ZThumb.DOC_TYPE_ID ")

        strselect.Append("left join wfdocument w on w.doc_id = T.doc_id ")
        strselect.Append("left join usrtable u ON u.ID = w.User_Asigned ")
        strselect.Append("left join WFStep s ON s.step_Id = w.step_Id ")
        'WHERE
        strselect.Append("where T.doc_id=")
        strselect.Append(ResultId.ToString())


        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString)
        If Not IsNothing(ds) And ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                Return ds.Tables(0).Rows(0)
            End If
        End If

        Return Nothing
    End Function

    ''' <summary>
    ''' Completa el result
    ''' </summary>
    ''' <param name="_Result">Result a ser completado</param>
    ''' <param name="inThread">Las tablas de sustitucion se cargan en thread?</param>
    ''' <History>Marcelo    Modified    07/08/09</History>
    ''' <remarks></remarks>
    Public Function CompleteDocument(ByVal ResultId As Int64, ByVal docTypeId As Int64, ByVal indexs As List(Of IIndex), ByRef con As IConnection, refIndexs As List(Of ReferenceIndex), Optional ByVal inThread As Boolean = False) As IDataReader

        Dim refIndexsQueryHelper As ReferenceIndexQueryHelper
        If refIndexs IsNot Nothing AndAlso refIndexs.Count > 0 Then
            refIndexsQueryHelper = New ReferenceIndexQueryHelper
        End If

        Dim strTableI As String = MakeTable(docTypeId, TableType.Indexs)
        Dim strTableT As String = MakeTable(docTypeId, TableType.Document)
        Dim MainJoin As String = String.Format("{0} T inner join {1} I on T.doc_id = I.doc_id", strTableT, strTableI)

        Dim strselect As StringBuilder = New StringBuilder()

        strselect.Append("SELECT T.DOC_ID, T.DISK_GROUP_ID,T.PLATTER_ID,T.VOL_ID,T.DOC_FILE,T.OFFSET,T.DOC_TYPE_ID,")
        strselect.Append("T.NAME,T.ICON_ID,T.SHARED")
        strselect.Append(",T.ver_Parent_id,T.version,T.RootId,T.original_Filename, T.NumeroVersion,disk_Vol_id, DISK_VOL_PATH, I.crdate ")
        strselect.Append(", DL.importance IsImportant, DLS.favorite IsFavorite")


        Dim f As Int16
        For f = 0 To indexs.Count - 1
            If refIndexs IsNot Nothing AndAlso refIndexs.Any(Function(_i) _i.IndexId = DirectCast(indexs(f), Index).ID) Then
                strselect.Append($",{refIndexsQueryHelper.GetStringForDocIQuery(DirectCast(indexs(f), Index).ID, docTypeId, refIndexs)} AS I{DirectCast(indexs(f), Index).ID}")
            Else
                strselect.Append(", I.I" & DirectCast(indexs(f), Index).ID)
            End If
        Next

        strselect.Append(" FROM ")
        strselect.Append(MainJoin)
        strselect.Append($" left outer join disk_Volume on disk_Vol_id = T.vol_id ")

        strselect.Append(" left join DocumentLabels DL On DL.doctypeid = T.DOC_TYPE_ID And DL.docid=T.Doc_ID And DL.userid=" + Membership.MembershipHelper.CurrentUser.ID.ToString() + " ")
        strselect.Append(" left join (Select * from DocumentLabels  where userid=" + Membership.MembershipHelper.CurrentUser.ID.ToString() + ") DLS On DLS.doctypeid = T.DOC_TYPE_ID And DLS.docid = T.Doc_ID ")


        If refIndexs IsNot Nothing AndAlso refIndexs.Count > 0 Then
            Dim joinStr As String
            For Each refInd As ReferenceIndex In refIndexs
                joinStr = refIndexsQueryHelper.GetStringJoinQuery(refInd.IndexId, docTypeId, refIndexs)
                If Not strselect.ToString().ToLower().Contains(joinStr.ToLower.Trim) Then
                    strselect.Append($"{joinStr} ")
                End If
            Next
        End If

        strselect.Append($" where T.doc_id = {ResultId.ToString()} ")

        con = Server.Con

        Return con.ExecuteReader(CommandType.Text, strselect.ToString)
    End Function


    Public Shared Function GetName(ByVal ResultId As Int64, ByVal DocTypeId As Int64) As String
        Dim selstr As String
        selstr = "SELECT NAME FROM DOC_T" & DocTypeId & " where doc_id=" & ResultId
        Dim Name As String = Server.Con.ExecuteScalar(CommandType.Text, selstr)
        Return Name
    End Function

    Public Shared Function GetFullName(ByVal ResultId As Int64, ByVal DocTypeId As Int64) As String
        Dim selstr As String
        selstr = "SELECT Original_filename FROM DOC_T" & DocTypeId & " where doc_id=" & ResultId
        Dim FullName As String = Server.Con.ExecuteScalar(CommandType.Text, selstr)
        Return FullName
    End Function
#End Region

#Region "GetResults"



    Public Shared Function RunWebTextSearch(ByVal query As String) As DataTable
        Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)
    End Function



    Public Shared Function SearchIndex(ByVal lngIndexID As Int64, ByVal enmIndexType As IndexDataType, ByVal strComparador As String, ByVal strValue As String, ByVal lngDocTypeID As Int64) As DataSet

        Dim sqlBuilder As New StringBuilder()
        Dim ds As New DataSet()

        sqlBuilder.Append("SELECT DOC_ID FROM DOC")
        sqlBuilder.Append(lngDocTypeID.ToString())
        sqlBuilder.Append(" WHERE ")

        Select Case enmIndexType

            Case IndexDataType.Fecha, IndexDataType.Fecha_Hora
                sqlBuilder.Append("I")
                sqlBuilder.Append(lngIndexID.ToString())
                sqlBuilder.Append(" ")
                sqlBuilder.Append(strComparador)
                sqlBuilder.Append(" '")
                sqlBuilder.Append(strValue)
                sqlBuilder.Append("'")

            Case IndexDataType.None
                sqlBuilder.Append("lower I")
                sqlBuilder.Append(lngIndexID.ToString())
                sqlBuilder.Append(") like '%")
                sqlBuilder.Append(strValue.ToLower())
                sqlBuilder.Append("%')")

            Case Else
                'IndexDataType.Si_No, IndexDataType.Alfanumerico, IndexDataType.Alfanumerico_Largo,
                ' IndexDataType.Numerico, IndexDataType.Numerico_Decimales, 
                'IndexDataType.Numerico_Largo, IndexDataType.Moneda
                sqlBuilder.Append("I")
                sqlBuilder.Append(lngIndexID.ToString())
                sqlBuilder.Append(" ")
                sqlBuilder.Append(strComparador)
                sqlBuilder.Append(" '")
                sqlBuilder.Append(strValue)
                sqlBuilder.Append("'")

        End Select

        Return Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())

    End Function

    Public Shared Function SearchIndex(ByVal lngIndexID As Int64, ByVal enmIndexType As IndexDataType, ByVal strComparador As String, ByVal strValue As String, ByVal lngDocTypeID As Int64, ByVal restriction As String) As DataSet

        Dim sqlBuilder As New StringBuilder()
        Dim ds As New DataSet()

        sqlBuilder.Append("SELECT DOC_ID FROM DOC")
        sqlBuilder.Append(lngDocTypeID.ToString())
        sqlBuilder.Append(" WHERE ")

        Select Case enmIndexType

            Case IndexDataType.Fecha, IndexDataType.Fecha_Hora
                sqlBuilder.Append("I")
                sqlBuilder.Append(lngIndexID.ToString())
                sqlBuilder.Append(" ")
                sqlBuilder.Append(strComparador)
                sqlBuilder.Append(" '")
                sqlBuilder.Append(strValue)
                sqlBuilder.Append("'")

            Case IndexDataType.None
                sqlBuilder.Append("lower I")
                sqlBuilder.Append(lngIndexID.ToString())
                sqlBuilder.Append(") like '%")
                sqlBuilder.Append(strValue.ToLower())
                sqlBuilder.Append("%')")

            Case Else
                'IndexDataType.Si_No, IndexDataType.Alfanumerico, IndexDataType.Alfanumerico_Largo,
                ' IndexDataType.Numerico, IndexDataType.Numerico_Decimales, 
                'IndexDataType.Numerico_Largo, IndexDataType.Moneda
                sqlBuilder.Append("I")
                sqlBuilder.Append(lngIndexID.ToString())
                sqlBuilder.Append(" ")
                sqlBuilder.Append(strComparador)
                sqlBuilder.Append(" '")
                sqlBuilder.Append(strValue)
                sqlBuilder.Append("'")

        End Select

        sqlBuilder.Append(restriction)

        Return Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())

    End Function
    Public Shared Function SearchIndexForWebServices(ByVal lngIndexID As Int64, ByVal enmIndexType As IndexDataType, ByVal strComparador As String, ByVal strValue As String, ByVal lngDocTypeID As Int64, ByVal restriction As String) As DataSet

        Dim sqlBuilder As New StringBuilder()
        Dim ds As New DataSet()

        sqlBuilder.Append("SELECT DOC_ID FROM DOC")
        sqlBuilder.Append(lngDocTypeID.ToString())


        If String.IsNullOrEmpty(strValue) Then

            'capaz tiene restriccion
            If Not String.IsNullOrEmpty(restriction) Then
                sqlBuilder.Append(" WHERE ")
                sqlBuilder.Append(restriction)
            End If

        Else
            sqlBuilder.Append(" WHERE ")

            Select Case enmIndexType

                Case IndexDataType.Fecha, IndexDataType.Fecha_Hora
                    sqlBuilder.Append("I")
                    sqlBuilder.Append(lngIndexID.ToString())
                    sqlBuilder.Append(" ")
                    sqlBuilder.Append(strComparador)
                    sqlBuilder.Append(" '")
                    sqlBuilder.Append(strValue)
                    sqlBuilder.Append("'")

                Case IndexDataType.None
                    sqlBuilder.Append("lower I")
                    sqlBuilder.Append(lngIndexID.ToString())
                    sqlBuilder.Append(") like '%")
                    sqlBuilder.Append(strValue.ToLower())
                    sqlBuilder.Append("%')")

                Case Else
                    'IndexDataType.Si_No, IndexDataType.Alfanumerico, IndexDataType.Alfanumerico_Largo,
                    ' IndexDataType.Numerico, IndexDataType.Numerico_Decimales, 
                    'IndexDataType.Numerico_Largo, IndexDataType.Moneda
                    sqlBuilder.Append("I")
                    sqlBuilder.Append(lngIndexID.ToString())
                    sqlBuilder.Append(" ")
                    sqlBuilder.Append(strComparador)

                    If String.Compare(strComparador, "LIKE", True) = 0 Then
                        sqlBuilder.Append(" '%")
                        sqlBuilder.Append(strValue)
                        sqlBuilder.Append("%'")

                    Else
                        sqlBuilder.Append(" '")
                        sqlBuilder.Append(strValue)
                        sqlBuilder.Append("'")
                    End If
            End Select

            If Not String.IsNullOrEmpty(restriction) Then
                sqlBuilder.Append(" and ")
                sqlBuilder.Append(restriction)
            End If
        End If

        Return Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())

    End Function

    Public Shared Function SearchbyIndexs(ByVal indexId As Int32, ByVal indexType As Int32, ByVal dt As DocType, ByVal IndexData As String) As DataSet
        Dim sql As New System.Text.StringBuilder

        If indexType <> 7 AndAlso indexType <> 8 AndAlso indexType <> 4 Then
            sql.Append("select doc_id from doc")
            sql.Append(dt.ID)
            sql.Append(" where i")
            sql.Append(indexId)
            sql.Append("=")
            sql.Append(IndexData)
        ElseIf indexType = 4 Then
            sql.Append("select doc_id from doc")
            sql.Append(dt.ID)
            sql.Append(" where i")
            sql.Append(indexId)
            sql.Append("=")
            sql.Append(ServersFactory.ConvertDate(IndexData))
        Else
            sql.Append("select doc_id from doc")
            sql.Append(dt.ID)
            sql.Append(" where ")
            sql.Append(" (lower(i" & indexId & ") Like '%" & IndexData.ToLower & "%')")
        End If
        Return Servers.Server.Con.ExecuteDataset(CommandType.Text, sql.ToString)
    End Function

    Public Shared Function GetDocumentData(ByVal ds As DataSet, ByVal dt As DocType, ByVal i As Int32) As DataSet
        Return Servers.Server.Con.ExecuteDataset(CommandType.Text, "Select name,DOC_FILE,OFFSET,VOL_ID,DISK_GROUP_ID from doc_t" & dt.ID & " where doc_id=" & ds.Tables(0).Rows(i).Item(0))
    End Function

    ''' <summary>
    ''' Obtiene las relaciones de tabla zrelations
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>Diego 31-07-2008 Created</history>
    Public Shared Function GetDocRelations() As DataSet
        Dim query As New StringBuilder
        query.Append("select relationId, name from zrelations")
        Return Server.Con.ExecuteDataset(CommandType.Text, query.ToString)
    End Function
    '[Ezequiel] 11/09/09 - Se comento el metodo ya que el mismo no se usa.
    'Public Shared Function GetIndexsRelations(ByVal id As Integer) As DataSet
    '    Return (Server.Con.ExecuteDataset(CommandType.Text, "SELECT IndexIdParent, IndexIdChild, DTIdParent, ZRelationsIndexs.RelationId FROM ZRelationsIndexs INNER JOIN ZRelations ON ZRelationsIndexs.RelationId = ZRelations.RelationId WHERE (ZRelationsIndexs.DTIdChild = " & id & ")"))
    'End Function

    ''' <summary>
    ''' Inserta una relacion entre dos results
    ''' </summary>
    ''' <param name="parentResultId">id del result padre</param>
    ''' <param name="relationatedResultId">id del result relacionado</param>
    ''' <param name="relationId">id de la relacion</param>
    ''' <remarks></remarks>
    ''' <history>Diego 31-07-2008 Created</history>
    Shared Sub InsertDocumentRelation(ByVal parentResultId As Int64, ByVal relationatedResultId As Int64, ByVal relationId As Int32)
        Dim query As New StringBuilder
        query.Append("INSERT INTO ZDocRelations(Doc_Id, ParentId, RelationId) VALUES(")
        query.Append(relationatedResultId)
        query.Append(",")
        query.Append(parentResultId)
        query.Append(",")
        query.Append(relationId)
        query.Append(")")
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
        query.Remove(0, query.Length)
        query.Append("SELECT COUNT(1) FROM ZDOCRELATIONS WHERE DOC_ID =")
        query.Append(parentResultId)
        If Server.Con.ExecuteScalar(CommandType.Text, query.ToString) = 0 Then
            query.Remove(0, query.Length)
            query.Append("INSERT INTO ZDocRelations(Doc_Id, ParentId, RelationId) VALUES(")
            query.Append(parentResultId)
            query.Append(",0,")
            query.Append(relationId)
            query.Append(")")
            Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
        End If
    End Sub



    ''' <summary>
    ''' Inserta una relacion entre dos results
    ''' </summary>
    ''' <param name="parentResultId">id del result padre</param>
    ''' <param name="relationatedResultId">id del result relacionado</param>
    ''' <param name="relationId">id de la relacion</param>
    ''' <remarks></remarks>
    ''' <history>Diego 31-07-2008 Created</history>
    Shared Sub InsertDocumentRelation(ByVal parentResultId As Int64, ByVal relationatedResultId As Int64, ByVal relationId As Int32, ByRef t As Transaction)
        Dim query As New StringBuilder
        query.Append("INSERT INTO ZDocRelations(Doc_Id, ParentId, RelationId) VALUES(")
        query.Append(relationatedResultId)
        query.Append(",")
        query.Append(parentResultId)
        query.Append(",")
        query.Append(relationId)
        query.Append(")")
        t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, query.ToString)
        query.Remove(0, query.Length)
        query.Append("SELECT COUNT(1) FROM ZDOCRELATIONS WHERE DOC_ID =")
        query.Append(parentResultId)
        If Server.Con.ExecuteScalar(CommandType.Text, query.ToString) = 0 Then
            query.Remove(0, query.Length)
            query.Append("INSERT INTO ZDocRelations(Doc_Id, ParentId, RelationId) VALUES(")
            query.Append(parentResultId)
            query.Append(",0,")
            query.Append(relationId)
            query.Append(")")
            t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, query.ToString)
        End If
    End Sub

#End Region

#Region "Delete"

    Public Shared FilesForDelete As New ArrayList

    <Obsolete("Metodo discontinuado", False)>
    Public Sub Delete(ByVal Result As IResult)

        Try
            Dim TableT As String = MakeTable(Result.DocType.ID, TableType.Document)
            Dim TableI As String = MakeTable(Result.DocType.ID, TableType.Indexs)
            Dim TableNotes As String = "Doc_Notes"
            Dim StrDelete As String


            StrDelete = "DELETE FROM " & TableI & " Where (Doc_ID = " & Result.ID & ")"
            Server.Con.ExecuteNonQuery(CommandType.Text, StrDelete)
            StrDelete = "DELETE FROM " & TableT & " Where (Doc_ID = " & Result.ID & ")"
            Server.Con.ExecuteNonQuery(CommandType.Text, StrDelete)
            StrDelete = "DELETE FROM " & TableNotes & " Where (Doc_ID = " & Result.ID & ")"
            Server.Con.ExecuteNonQuery(CommandType.Text, StrDelete)
            StrDelete = "DELETE FROM ZBARCODE Where (Doc_ID = " & Result.ID & ")"
            Server.Con.ExecuteNonQuery(CommandType.Text, StrDelete)

            'Andres 4/9/28 - Si es documento virtual no tiene path , no se borra
            If Result.ISVIRTUAL = False Then

                Dim file As IO.FileInfo = Nothing
                Dim FileLen As Long = 0

                Try

                    If Result.Disk_Group_Id <> 0 AndAlso VolumesFactory.GetVolumeType(Result.Disk_Group_Id) = VolumeType.DataBase Then

                        If IsNothing(Result.EncodedFile) Then

                            Result.EncodedFile = Results_Factory.LoadFileFromDB(Result.ID, Result.DocTypeId)

                            If Not IsNothing(Result.EncodedFile) AndAlso Result.EncodedFile.Length > 0 Then
                                FileLen = Math.Ceiling(Result.EncodedFile.Length / 1024)
                            End If

                        End If

                    Else

                        file = New IO.FileInfo(Result.FullPath)

                        If file.Exists AndAlso file.Length > 0 Then
                            FileLen = Math.Ceiling(file.Length / 1024)
                        End If

                        Try
                            Try
                                file.MoveTo(file.FullName & "_DELETED")
                                'file.Delete()
                            Catch ex As Exception
                                Results_Factory.FilesForDelete.Add(file)
                            End Try
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try

                    End If

                    If Server.isOracle Then
                        Dim parNames() As String = {"VolumeId", "FileSize"}
                        ' Dim parTypes() As Object = {13, 13}
                        Dim parValues() As Object = {Result.Disk_Group_Id, FileLen}
                        'Server.Con.ExecuteNonQuery("UPDATEVOLDELFILE_PKG.UPDATEVOLDELFILE", parValues)
                        Server.Con.ExecuteNonQuery("zsp_volume_100.UpdDeletedFiles", parValues)
                    Else
                        Dim parametersValues() As Object = {Result.Disk_Group_Id, FileLen}
                        'Server.Con.ExecuteDataset("UPDATEVOLDELFILE", parametersValues)
                        Server.Con.ExecuteDataset("zsp_volume_100_UpdDeletedFiles", parametersValues)
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try

            End If

            Try
                If Server.isOracle Then
                    Dim parNames() As String = {"DocID", "X"}
                    ' Dim parTypes() As Object = {13, 13}
                    Dim parValues() As Object = {Result.DocType.ID, -1}
                    'Server.Con.ExecuteNonQuery("IncrementarDocType_pkg.IncrementarDocType", parValues)
                    Server.Con.ExecuteNonQuery("zsp_doctypes_100.IncrementsDocType", parValues)
                Else
                    Dim parametersValues() As Object = {Result.DocType.ID, -1}
                    'Server.Con.ExecuteNonQuery("incrementarDocType", parametersValues)
                    Server.Con.ExecuteNonQuery("zsp_doctypes_100_IncrementsDocType", parametersValues)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        Catch ex As Exception
            Throw New Exception("Ocurrio un error al intentar eliminar el documento" & " " & ex.ToString)
        End Try
    End Sub

    Public Sub Delete(ByVal taskId As Int64, ByVal DocTypeId As Int64, ByVal fullPath As String)

        Dim TableT As String = MakeTable(DocTypeId, TableType.Document)
        Dim TableI As String = MakeTable(DocTypeId, TableType.Indexs)

        'Dim StrDelete As String
        Dim QueryBuilder As New StringBuilder()

        QueryBuilder.Append("DELETE FROM ")
        QueryBuilder.Append(TableI)
        QueryBuilder.Append(" Where (Doc_ID = ")
        QueryBuilder.Append(taskId.ToString())
        QueryBuilder.Append(")")
        Server.Con.ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString())

        'StrDelete = "DELETE FROM " & TableI & " Where (Doc_ID = " & taskId & ")"
        'Server.Con.ExecuteNonQuery(CommandType.Text, StrDelete)

        QueryBuilder.Remove(0, QueryBuilder.Length)
        QueryBuilder.Append("DELETE FROM ")
        QueryBuilder.Append(TableT)
        QueryBuilder.Append(" Where (Doc_ID = ")
        QueryBuilder.Append(taskId.ToString())
        QueryBuilder.Append(")")
        Server.Con.ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString())

        'StrDelete = "DELETE FROM " & TableT & " Where (Doc_ID = " & taskId & ")"
        'Server.Con.ExecuteNonQuery(CommandType.Text, StrDelete)

        Dim TableNotes As String = "Doc_Notes"
        QueryBuilder.Remove(0, QueryBuilder.Length)
        QueryBuilder.Append("DELETE FROM ")
        QueryBuilder.Append(TableNotes)
        QueryBuilder.Append(" Where (Doc_ID = ")
        QueryBuilder.Append(taskId.ToString())
        QueryBuilder.Append(")")
        Server.Con.ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString())

        'StrDelete = "DELETE FROM " & TableNotes & " Where (Doc_ID = " & taskId & ")"
        'Server.Con.ExecuteNonQuery(CommandType.Text, StrDelete)

        QueryBuilder.Remove(0, QueryBuilder.Length)
        QueryBuilder.Append("DELETE FROM ZBARCODE Where (Doc_ID = ")
        QueryBuilder.Append(taskId.ToString())
        QueryBuilder.Append(")")
        Server.Con.ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString())

        'StrDelete = "DELETE FROM ZBARCODE Where (Doc_ID = " & taskId & ")"
        'Server.Con.ExecuteNonQuery(CommandType.Text, StrDelete)

        QueryBuilder.Remove(0, QueryBuilder.Length)
        QueryBuilder = Nothing


        If IO.File.Exists(fullPath) Then
            IO.File.Move(fullPath, fullPath & "_DELETED")
        End If

        If Server.isOracle Then
            ' Dim parNames() As String = {"DocID", "X"}
            ' Dim parTypes() As Object = {13, 13}
            Dim parValues() As Object = {DocTypeId, -1}
            'Server.Con.ExecuteNonQuery("IncrementarDocType_pkg.IncrementarDocType", parValues)
            Server.Con.ExecuteNonQuery("zsp_doctypes_100.IncrementsDocType", parValues)

            Array.Clear(parValues, 0, parValues.Length)
            parValues = Nothing

        Else
            Dim parametersValues() As Object = {DocTypeId, -1}
            'Server.Con.ExecuteNonQuery("incrementarDocType", parametersValues)
            Server.Con.ExecuteNonQuery("zsp_doctypes_100_IncrementsDocType", parametersValues)

            Array.Clear(parametersValues, 0, parametersValues.Length)
            parametersValues = Nothing
        End If

    End Sub
    <Obsolete("Metodo discontinuado", False)>
    Public Sub Delete(ByRef Result As NewResult)
        Try
            Dim TableT As String = MakeTable(Result.DocType.ID, TableType.Document)
            Dim TableI As String = MakeTable(Result.DocType.ID, TableType.Indexs)
            Dim TableNotes As String = "Doc_Notes"
            Dim StrDelete As String
            '            
            StrDelete = "DELETE FROM " & TableT & " Where (Doc_ID = " & Result.ID & ")"
            Server.Con.ExecuteNonQuery(CommandType.Text, StrDelete)
            StrDelete = "DELETE FROM " & TableI & " Where (Doc_ID = " & Result.ID & ")"
            Server.Con.ExecuteNonQuery(CommandType.Text, StrDelete)
            StrDelete = "DELETE FROM " & TableNotes & " Where (Doc_ID = " & Result.ID & ")"
            Server.Con.ExecuteNonQuery(CommandType.Text, StrDelete)
            Dim file As IO.FileInfo
            Try
                file = New IO.FileInfo(Result.FullPath)
                If file.Exists = False Then file = New IO.FileInfo(Result.NewFile)
                If file.Exists = False Then file = New IO.FileInfo(Result.File)
            Catch ex As Exception
                file = New IO.FileInfo(Result.NewFile)
            End Try


            Dim FileLen As Decimal = CDec(file.Length) / 1000
            If Server.isOracle Then
                Dim parNames() As String = {"VolumeId", "FileSize"}
                ' Dim parTypes() As Object = {13, 13}
                Dim parValues() As Object = {Result.Volume.ID, FileLen}
                'Server.Con.ExecuteNonQuery("UPDATEVOLDELFILE_PKG.UPDATEVOLDELFILE", parValues)
                Server.Con.ExecuteNonQuery("zsp_volume_100.UpdDeletedFiles", parValues)
            Else
                Dim parametersValues() As Object = {Result.Volume.ID, FileLen}
                'Server.Con.ExecuteDataset("UPDATEVOLDELFILE", parametersValues)
                Server.Con.ExecuteDataset("zsp_volume_100_UpdDeletedFiles", parametersValues)
            End If

            Try
                file.MoveTo(file.FullName & "_DELETED")
            Catch ex As Exception
                Results_Factory.FilesForDelete.Add(file)
            End Try
        Catch ex As Exception
                Throw New Exception("Ocurrio un error al intentar eliminar el documento" & " " & ex.ToString)
        End Try
    End Sub
    <Obsolete("Metodo discontinuado", False)>
    Public Sub Delete(ByRef Result As NewResult, ByRef t As Transaction)
        Try
            Dim TableT As String = MakeTable(Result.DocType.ID, TableType.Document)
            Dim TableI As String = MakeTable(Result.DocType.ID, TableType.Indexs)
            Dim TableNotes As String = "Doc_Notes"
            Dim StrDelete As String
            '            
            StrDelete = "DELETE FROM " & TableT & " Where (Doc_ID = " & Result.ID & ")"
            t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, StrDelete)
            StrDelete = "DELETE FROM " & TableI & " Where (Doc_ID = " & Result.ID & ")"
            t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, StrDelete)
            StrDelete = "DELETE FROM " & TableNotes & " Where (Doc_ID = " & Result.ID & ")"
            t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, StrDelete)
            Dim file As IO.FileInfo
            Try
                file = New IO.FileInfo(Result.FullPath)
                If file.Exists = False Then file = New IO.FileInfo(Result.NewFile)
                If file.Exists = False Then file = New IO.FileInfo(Result.File)
            Catch ex As Exception
                file = New IO.FileInfo(Result.NewFile)
            End Try


            Dim FileLen As Decimal = CDec(file.Length) / 1000
            If Server.isOracle Then
                'Dim parNames() As String = {"VolumeId", "FileSize"}
                '' Dim parTypes() As Object = {13, 13}
                Dim parValues() As Object = {Result.Volume.ID, FileLen}
                'Server.Con.ExecuteNonQuery("UPDATEVOLDELFILE_PKG.UPDATEVOLDELFILE", parValues)
                t.Con.ExecuteNonQuery(t.Transaction, "zsp_volume_100.UpdDeletedFiles", parValues)
            Else
                Dim parametersValues() As Object = {Result.Volume.ID, FileLen}
                'Server.Con.ExecuteDataset("UPDATEVOLDELFILE", parametersValues)
                t.Con.ExecuteNonQuery(t.Transaction, "zsp_volume_100_UpdDeletedFiles", parametersValues)
            End If

            Try
                file.MoveTo(file.FullName & "_DELETED")
            Catch ex As Exception
                Results_Factory.FilesForDelete.Add(file)
            End Try
        Catch ex As Exception
                Throw New Exception("Ocurrio un error al intentar eliminar el documento" & " " & ex.ToString)
        End Try
    End Sub

    Public Shared Sub DeleteResultFromWorkflows(ByVal docid As Int64)
        Dim query As New StringBuilder
        query.Append("DELETE FROM WfDocument where Doc_Id = ")
        query.Append(docid.ToString)
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
    End Sub
    Public Shared Sub RemoveDocument(ByVal docid As Int64, ByVal DocTypeId As Int64)
        Dim query As New StringBuilder
        For Each PrefixTable As String In {"T", "B", "I"}
            Dim TableName As String = "DOC_" + PrefixTable + DocTypeId.ToString
            If Server.isSQLServer Then
                query.AppendLine("if OBJECT_ID('" + TableName + "') is not null")
                query.AppendLine("begin")
                query.AppendLine("delete from " + TableName + " where doc_id=" + docid.ToString)
                query.AppendLine("end")
            Else
                query.AppendLine("delete from " + TableName + " where doc_id=" + docid.ToString)
            End If
        Next
        query.AppendLine("DELETE FROM WfDocument where Doc_Id = " + docid.ToString + " and doc_type_id=" + DocTypeId.ToString)
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
    End Sub
#End Region

#Region "DOCFILE"

    <Obsolete("Metodo discontinuado", False)>
    Public Shared Sub updatedocfile(ByRef Result As Result, ByVal docfile As String)
        Try
            Dim strupdate As String = "UPDATE DOC_T" & Result.DocType.ID & " set DOC_FILE = '" & docfile & "' where doc_id = " & Result.ID
            Server.Con.ExecuteNonQuery(CommandType.Text, strupdate)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub


    Public Sub ReplaceDocument(ByRef Result As Result, ByVal NewFi As IO.FileInfo, ByVal NewName As String, ByVal ComeFromWF As Boolean, ByRef t As Transaction)


        Dim RF As New Results_Factory

        Try
            Dim QueryBuilder As New StringBuilder
            Dim table As String = RF.MakeTable(Result.DocType.ID, TableType.Document)

            QueryBuilder.Append("UPDATE " & table & " SET Doc_File = '" & NewName & "', icon_id = " & Result.IconId & ", offset= " & Result.OffSet & ",vol_id=" & Result.Disk_Group_Id & ", Disk_Group_Id = " & Result.Disk_Group_Id)
            QueryBuilder.Append(" WHERE DOC_ID = " & Result.ID)

            If t IsNot Nothing Then
                t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, QueryBuilder.ToString)
            Else
                Server.Con.ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString)
            End If

            If ComeFromWF Then
                Dim QueryBuilder1 As New StringBuilder

                QueryBuilder1.Append("UPDATE wfdocument SET iconId=" & Result.IconId)

                If Server.isOracle Then
                    QueryBuilder1.Append(",LastUpdateDate=sysdate")
                Else
                    QueryBuilder1.Append(",LastUpdateDate=getdate()")
                End If

                QueryBuilder1.Append(" WHERE DOC_ID = " & Result.ID)
                If t IsNot Nothing Then
                    t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, QueryBuilder1.ToString)
                Else
                    Server.Con.ExecuteNonQuery(CommandType.Text, QueryBuilder1.ToString)
                End If
            End If



        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            RF = Nothing
        End Try
    End Sub

    Public Shared Sub ReplaceDigitalDocument(ByVal result As IResult)
        Dim count As Int32 = Server.Con.ExecuteScalar(CommandType.Text, "SELECT COUNT(1) FROM DOC_B" & result.DocTypeId & " WITH(NOLOCK) WHERE DOC_ID = " & result.ID)
        If Server.isOracle Then
            count = Server.Con.ExecuteScalar(CommandType.Text, "SELECT COUNT(1) FROM DOC_B" & result.DocTypeId & " WHERE DOC_ID = " & result.ID)
        Else
            count = Server.Con.ExecuteScalar(CommandType.Text, "SELECT COUNT(1) FROM DOC_B" & result.DocTypeId & " WITH(NOLOCK) WHERE DOC_ID = " & result.ID)
        End If

        If count = 0 Then
            InsertIntoDOCB(result, False)
        Else
            UpdateDOCB(result)
        End If
    End Sub


    Public Sub LogDropzoneHistory(ByVal taskID As Int64, ByVal taskName As String, ByVal docTypeId As Int32, ByVal docTypeName As String, ByVal stepId As Int32, ByVal workflowId As Int32, ByVal State As String, ByVal stepname As String, WFName As String)

        'ByVal statename As String, ByVal assignedTo As String, ByVal workflowId As Int32)

        Dim RF As New Results_Factory

        Try
            Dim QueryBuilder As New StringBuilder
            Dim table As String = RF.MakeTable(docTypeId, TableType.Document)

            If IsDBNull(stepname) AndAlso stepname Is Nothing Then
                stepname = String.Empty
            End If

            If IsDBNull(WFName) AndAlso WFName Is Nothing Then
                WFName = String.Empty
            End If

            QueryBuilder.Append("INSERT INTO WFStepHst(Doc_Id, Doc_Name, DocTypeId, Doc_Type_Name, FOLDER_Id, StepId, Step_Name, UserName, Accion,  Fecha, WorkflowId, WorkflowName,State) VALUES (" &
                                  taskID & ",'" & taskName & "'," & docTypeId & ",'" & docTypeName & "',0," & stepId & ",'" & stepname & "','" & RightFactory.CurrentUser.Name & "','Subio un archivo',sysdate," & workflowId & ",'" & WFName & "','" & State & "')")

            'QueryBuilder.Append(" WHERE DOC_ID = " & Result.ID)
            If Servers.Server.isSQLServer Then
                QueryBuilder.Replace("sysdate", "getdate()")
            End If
            Server.Con.ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString)

        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            RF = Nothing
        End Try
    End Sub

    '<Obsolete("Metodo discontinuado", False)>
    'Public Shared Sub ReplaceDocument(ByRef Result As NewResult)
    '    ' Dim s As String = "select * from doc_d" & Result.DocType.Id & " where index_type=" & indexType.unique
    '    '  ZTrace.WriteLineIf(ZTrace.IsInfo,"Consulta ejecutada=" & s)
    '    '  Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, s)
    '    '  ds.Tables(0).TableName = "dsDOCD"

    '    '  ZTrace.WriteLineIf(ZTrace.IsInfo,"creo nuevo Result")
    '    ' Dim dsdocds As New dsDOCD
    '    ' dsdocds.Merge(ds)

    '    Dim strb As New System.Text.StringBuilder
    '    Dim strup As New System.Text.StringBuilder

    '    'Dim i As Integer
    '    '' Dim j As Integer
    '    'ZTrace.WriteLineIf(ZTrace.IsInfo,"Comienza la construccion del insert")

    '    'For i = 0 To Result.Indexs.Count - 1
    '    '    If ds.Tables(0).Rows.Count > 0 Then
    '    '        If dsdocds.dsDOCD(0).Item("D" & Result.Indexs(i).id) = 1 Then
    '    '            Select Case Result.Indexs(i).type
    '    '                Case 1, 2, 3, 6
    '    '                    If Result.Indexs(i).datatemp.trim = "" Then
    '    '                        strb.Append(" and i" & Result.Indexs(i).id & "= null")
    '    '                    Else
    '    '                        strb.Append(" and i" & Result.Indexs(i).id & "=" & Result.Indexs(i).datatemp)
    '    '                    End If
    '    '                    ' strb.Append(" and i" & Result.indexs(i).id & "=" & Result.indexs(i).datatemp)
    '    '                    '                            strb.Append(" and i" & Result.indexs(i).id & "=" & Result.indexs(i).datatemp)
    '    '                Case 4
    '    '                    strb.Append(" and i" & Result.Indexs(i).id & "=" & Result.Indexs(i).datatemp)
    '    '                Case 5
    '    '                    strb.Append(" and i" & Result.Indexs(i).id & "=" & Result.Indexs(i).datatemp)
    '    '                Case 7, 8
    '    '                    strb.Append(" and i" & Result.Indexs(i).id & "=" & "'" & Result.Indexs(i).datatemp & "'")
    '    '                Case Else
    '    '            End Select
    '    '        End If
    '    '        'OJO Ver si fuciona sino descomentar
    '    '        'If ds.Tables(0).Rows(0).Item(i + 3) = 1 Then
    '    '        '    strb.Append(" and i" & Result.indexs(i).id & "=" & Result.indexs(i).datatemp)
    '    '        'End If

    '    '        Select Case Result.Indexs(i).type
    '    '            Case 1, 2, 3, 6   ' Es Numerico y Decimal el 6
    '    '                If DirectCast(Result.Indexs(i).datatemp, String).Trim <> "" Then
    '    '                    If IsNumeric(DirectCast(Result.Indexs(i).datatemp, String).Trim) Then
    '    '                        strup.Append("I" & DirectCast(Result.Indexs(i).id, String) & "=" & Replace(CStr(Result.Indexs(i).datatemp), ",", ".") & ",")
    '    '                    End If
    '    '                Else
    '    '                    strup.Append("I" & DirectCast(Result.Indexs(i).id, String) & "=" & "null" & ",")
    '    '                End If
    '    '            Case 4   'Es Fecha 
    '    '                If CStr(CStr(Result.Indexs(i).datatemp).Trim) <> "" Then
    '    '                    strup.Append("I" & DirectCast(Result.Indexs(i).id, String) & "=" & Server.Con.ConvertDate(Result.Indexs(i).datatemp) & ",")
    '    '                Else
    '    '                    strup.Append("I" & DirectCast(Result.Indexs(i).id, String) & "=" & "null" & ",")
    '    '                End If
    '    '            Case 5
    '    '                If DirectCast(Result.Indexs(i).datatemp, String).Trim <> "" Then
    '    '                    strup.Append("I" & DirectCast(Result.Indexs(i).id, String) & "=" & Server.Con.ConvertDateTime(Result.Indexs(i).datatemp) & ",")
    '    '                Else
    '    '                    strup.Append("I" & DirectCast(Result.Indexs(i).id, String) & "=" & "null" & ",")
    '    '                End If
    '    '            Case 7, 8   'Es Texto

    '    '                If DirectCast(Result.Indexs(i).datatemp, String).Trim <> "" Then
    '    '                    Result.Indexs(i).datatemp = Result.Indexs(i).datatemp.replace("'", "''")
    '    '                    Dim DataLen As Int32 = Len(Result.Indexs(i).datatemp.trim)
    '    '                    Dim indexLen As Int32 = Result.Indexs(i).len
    '    '                    If DataLen > indexLen Then
    '    '                        Result.Indexs(i).datatemp = Result.Indexs(i).datatemp.substring(0, indexLen)
    '    '                    End If
    '    '                    strup.Append("I" & DirectCast(Result.Indexs(i).id, String) & "=" & "'" & CStr(Result.Indexs(i).datatemp).Replace("'", "''") & "'" & ",")
    '    '                Else
    '    '                    strup.Append("I" & DirectCast(Result.Indexs(i).id, String) & "=" & "null" & ",")
    '    '                End If

    '    '            Case Else
    '    '                If DirectCast(Result.Indexs(i).datatemp, String).Trim <> "" Then
    '    '                    Dim DataLen As Int32 = Result.Indexs(i).datatemp.trim
    '    '                    Dim indexLen As Int32 = Result.Indexs(i).len
    '    '                    If DataLen > indexLen Then
    '    '                        Result.Indexs(i).datatemp = Result.Indexs(i).datatemp.substring(0, indexLen)
    '    '                    End If
    '    '                    strup.Append("I" & DirectCast(Result.Indexs(i).id, String) & "=" & Server.Con.ConvertDateTime(Result.Indexs(i).datatemp) & ",")
    '    '                Else
    '    '                    strup.Append("I" & DirectCast(Result.Indexs(i).id, String) & "=" & "null" & ",")
    '    '                End If
    '    '        End Select

    '    '    End If
    '    'Next

    '    Dim StrSelect As String = "Select t.doc_id, t.doc_file , disk_vol_path,concat('\',concat(offset,concat('\',doc_file))) FILENAME from doc_t" & Result.DocType.ID & " t , disk_volume where vol_id=disk_vol_id " & strb.ToString
    '    ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando Sentencia " & StrSelect)
    '    Dim ds2 As DataSet = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)

    '    If ds2.Tables(0).Rows.Count > 0 Then
    '        Result.ID = ds2.Tables(0).Rows(0).Item(0)
    '        Dim oldFile As String = ds2.Tables(0).Rows(0).Item(2) & "\" & Result.DocType.ID & ds2.Tables(0).Rows(0).Item(3)
    '        Dim olfi As New IO.FileInfo(oldFile.Trim)

    '        Dim ifinf As New IO.FileInfo(Result.File)
    '        If strup.ToString <> "" Then
    '            Server.Con.ExecuteNonQuery(CommandType.Text, "update doc_i" & Result.DocType.ID & " set " & strup.ToString.Substring(0, strup.ToString.LastIndexOf(",")) & " where doc_id=" & Result.ID)
    '        End If
    '        Server.Con.ExecuteNonQuery(CommandType.Text, "update doc_t" & Result.DocType.ID & " set doc_file=" & "'" & New IO.FileInfo(Result.NewFile).Name & "'" & ", offset= " & Result.Volume.offset & ",vol_id=" & Result.Volume.ID & ",name=" & "'" & Result.Name & "'" & ",icon_id=" & Result.IconId & " where doc_id=" & Result.ID)
    '        'copyfile(Result)
    '        If olfi.Exists = True Then olfi.Delete()
    '    End If
    'End Sub
    '<Obsolete("Metodo discontinuado", False)>
    'Public Sub ReplaceDocument(ByRef Result As NewResult, ByRef t As Transaction)
    '    ' Dim s As String = "select * from doc_d" & Result.DocType.Id & " where index_type=" & indexType.unique
    '    '  ZTrace.WriteLineIf(ZTrace.IsInfo,"Consulta ejecutada=" & s)
    '    '  Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, s)
    '    '  ds.Tables(0).TableName = "dsDOCD"

    '    '  ZTrace.WriteLineIf(ZTrace.IsInfo,"creo nuevo Result")
    '    ' Dim dsdocds As New dsDOCD
    '    ' dsdocds.Merge(ds)

    '    Dim strb As New System.Text.StringBuilder
    '    Dim strup As New System.Text.StringBuilder

    '    'Dim i As Integer
    '    '' Dim j As Integer
    '    'ZTrace.WriteLineIf(ZTrace.IsInfo,"Comienza la construccion del insert")

    '    'For i = 0 To Result.Indexs.Count - 1
    '    '    If ds.Tables(0).Rows.Count > 0 Then
    '    '        If dsdocds.dsDOCD(0).Item("D" & Result.Indexs(i).id) = 1 Then
    '    '            Select Case Result.Indexs(i).type
    '    '                Case 1, 2, 3, 6
    '    '                    If Result.Indexs(i).datatemp.trim = "" Then
    '    '                        strb.Append(" and i" & Result.Indexs(i).id & "= null")
    '    '                    Else
    '    '                        strb.Append(" and i" & Result.Indexs(i).id & "=" & Result.Indexs(i).datatemp)
    '    '                    End If
    '    '                    ' strb.Append(" and i" & Result.indexs(i).id & "=" & Result.indexs(i).datatemp)
    '    '                    '                            strb.Append(" and i" & Result.indexs(i).id & "=" & Result.indexs(i).datatemp)
    '    '                Case 4
    '    '                    strb.Append(" and i" & Result.Indexs(i).id & "=" & Result.Indexs(i).datatemp)
    '    '                Case 5
    '    '                    strb.Append(" and i" & Result.Indexs(i).id & "=" & Result.Indexs(i).datatemp)
    '    '                Case 7, 8
    '    '                    strb.Append(" and i" & Result.Indexs(i).id & "=" & "'" & Result.Indexs(i).datatemp & "'")
    '    '                Case Else
    '    '            End Select
    '    '        End If
    '    '        'OJO Ver si fuciona sino descomentar
    '    '        'If ds.Tables(0).Rows(0).Item(i + 3) = 1 Then
    '    '        '    strb.Append(" and i" & Result.indexs(i).id & "=" & Result.indexs(i).datatemp)
    '    '        'End If

    '    '        Select Case Result.Indexs(i).type
    '    '            Case 1, 2, 3, 6   ' Es Numerico y Decimal el 6
    '    '                If DirectCast(Result.Indexs(i).datatemp, String).Trim <> "" Then
    '    '                    If IsNumeric(DirectCast(Result.Indexs(i).datatemp, String).Trim) Then
    '    '                        strup.Append("I" & DirectCast(Result.Indexs(i).id, String) & "=" & Replace(CStr(Result.Indexs(i).datatemp), ",", ".") & ",")
    '    '                    End If
    '    '                Else
    '    '                    strup.Append("I" & DirectCast(Result.Indexs(i).id, String) & "=" & "null" & ",")
    '    '                End If
    '    '            Case 4   'Es Fecha 
    '    '                If CStr(CStr(Result.Indexs(i).datatemp).Trim) <> "" Then
    '    '                    strup.Append("I" & DirectCast(Result.Indexs(i).id, String) & "=" & Server.Con.ConvertDate(Result.Indexs(i).datatemp) & ",")
    '    '                Else
    '    '                    strup.Append("I" & DirectCast(Result.Indexs(i).id, String) & "=" & "null" & ",")
    '    '                End If
    '    '            Case 5
    '    '                If DirectCast(Result.Indexs(i).datatemp, String).Trim <> "" Then
    '    '                    strup.Append("I" & DirectCast(Result.Indexs(i).id, String) & "=" & Server.Con.ConvertDateTime(Result.Indexs(i).datatemp) & ",")
    '    '                Else
    '    '                    strup.Append("I" & DirectCast(Result.Indexs(i).id, String) & "=" & "null" & ",")
    '    '                End If
    '    '            Case 7, 8   'Es Texto

    '    '                If DirectCast(Result.Indexs(i).datatemp, String).Trim <> "" Then
    '    '                    Result.Indexs(i).datatemp = Result.Indexs(i).datatemp.replace("'", "''")
    '    '                    Dim DataLen As Int32 = Len(Result.Indexs(i).datatemp.trim)
    '    '                    Dim indexLen As Int32 = Result.Indexs(i).len
    '    '                    If DataLen > indexLen Then
    '    '                        Result.Indexs(i).datatemp = Result.Indexs(i).datatemp.substring(0, indexLen)
    '    '                    End If
    '    '                    strup.Append("I" & DirectCast(Result.Indexs(i).id, String) & "=" & "'" & CStr(Result.Indexs(i).datatemp).Replace("'", "''") & "'" & ",")
    '    '                Else
    '    '                    strup.Append("I" & DirectCast(Result.Indexs(i).id, String) & "=" & "null" & ",")
    '    '                End If

    '    '            Case Else
    '    '                If DirectCast(Result.Indexs(i).datatemp, String).Trim <> "" Then
    '    '                    Dim DataLen As Int32 = Result.Indexs(i).datatemp.trim
    '    '                    Dim indexLen As Int32 = Result.Indexs(i).len
    '    '                    If DataLen > indexLen Then
    '    '                        Result.Indexs(i).datatemp = Result.Indexs(i).datatemp.substring(0, indexLen)
    '    '                    End If
    '    '                    strup.Append("I" & DirectCast(Result.Indexs(i).id, String) & "=" & Server.Con.ConvertDateTime(Result.Indexs(i).datatemp) & ",")
    '    '                Else
    '    '                    strup.Append("I" & DirectCast(Result.Indexs(i).id, String) & "=" & "null" & ",")
    '    '                End If
    '    '        End Select

    '    '    End If
    '    'Next

    '    Dim StrSelect As String = "Select t.doc_id, t.doc_file , disk_vol_path,concat('\',concat(offset,concat('\',doc_file))) FILENAME from doc_t" & Result.DocType.ID & " t , disk_volume where vol_id=disk_vol_id " & strb.ToString
    '    ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando Sentencia " & StrSelect)
    '    Dim ds2 As DataSet = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)

    '    If ds2.Tables(0).Rows.Count > 0 Then
    '        Result.ID = ds2.Tables(0).Rows(0).Item(0)
    '        Dim oldFile As String = ds2.Tables(0).Rows(0).Item(2) & "\" & Result.DocType.ID & ds2.Tables(0).Rows(0).Item(3)
    '        Dim olfi As New IO.FileInfo(oldFile.Trim)

    '        Dim ifinf As New IO.FileInfo(Result.File)
    '        If strup.ToString <> "" Then
    '            t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, "update doc_i" & Result.DocType.ID & " set " & strup.ToString.Substring(0, strup.ToString.LastIndexOf(",")) & " where doc_id=" & Result.ID)
    '            t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, "update doc_t" & Result.DocType.ID & " set doc_file=" & "'" & New IO.FileInfo(Result.NewFile).Name & "'" & ", offset= " & Result.Volume.offset & ",vol_id=" & Result.Volume.ID & ",name=" & "'" & Result.Name & "'" & ",icon_id=" & Result.IconId & " where doc_id=" & Result.ID)
    '        End If
    '        'copyfile(Result)
    '        If olfi.Exists = True Then olfi.Delete()
    '    End If
    'End Sub

    ''' <summary>
    ''' Actualiza la informacion del documento en la doc_t
    ''' </summary>
    ''' <param name="Result"></param>
    ''' <param name="volume"></param>
    ''' <remarks></remarks>
    Public Shared Sub ReplaceResultFileInfo(ByVal Result As IResult, ByVal volume As IVolume)
        Server.Con.ExecuteNonQuery(CommandType.Text, "update doc_t" & Result.DocType.ID & " set doc_file=" & "'" & Result.OriginalName & "'" &
        ", offset= " & volume.offset & ",vol_id=" & volume.ID & ",name=" & "'" & Result.Name & "'" & ",icon_id=" & Result.IconId &
        ", original_filename = '" & Result.OriginalName & "' where doc_id=" & Result.ID)
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo para mover un documento a otro entidad
    ''' </summary>
    ''' <param name="Result">NewResult Original que se va a mover o copiar</param>
    ''' <param name="TempVolId">Volumen Temporal</param>
    ''' <param name="TempVolOffSet"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub MoveDocument(ByRef Result As NewResult, ByVal TempVolId As Int32, ByVal TempVolOffSet As Int32, ByVal TempVolPath As String)

        Dim Table As String = MakeTable(Result.DocType.ID, Core.TableType.Document)
        Dim Fi As IO.FileInfo = New IO.FileInfo(TempVolPath & "\" & Result.DocType.ID & "\" & TempVolOffSet & "\" & Result.FileName.Trim)

        If Fi.Exists Then

            Fi.CopyTo(Result.Volume.path.Trim & "\" & Result.DocType.ID & "\" & Result.Volume.offset & "\" & Result.FileName.Trim, True)

            Dim FileLen As Decimal = CDec(Fi.Length) / 1000
            If Server.isOracle Then
                Dim parNames() As String = {"VolumeId", "FileSize"}
                ' Dim parTypes() As Object = {13, 13}
                Dim parValues() As Object = {TempVolId, FileLen}
                Server.Con.ExecuteNonQuery("UPDATEVOLDELFILE_PKG.UPDATEVOLDELFILE", parValues)
            Else
                Dim parametersValues() As Object = {Result.Volume.ID, FileLen}
                Server.Con.ExecuteDataset("UPDATEVOLDELFILE", parametersValues)
            End If

            If Result.FlagCopyVerify = True Then
                Dim NewFi As New IO.FileInfo(Result.FullPath) 'NewFullFileName)
                If NewFi.Exists Then
                    Result.FlagCopyVerify = True
                    GC.Collect()
                    Try
                        Fi.Delete()
                    Catch
                    End Try
                Else
                    Result.FlagCopyVerify = False
                    'TODO Falta ver que hago si no la copio por algo
                End If
            Else
                GC.Collect()
                Try
                    Fi.Delete()
                Catch ex As Exception
                    'Documents.FilesForDelete.Add(Fi)
                End Try
            End If
        Else
            Throw New Exception("El Archivo origen no existe o no se puede acceder a el, verifique la existencia del mismo: " & Fi.FullName)
        End If

        Dim Strupdate As String = "UPDATE " & Table & " SET Disk_Group_Id = " & Result.Volume.ID & ", Vol_Id = " & Result.Volume.ID & ", Offset = " & Result.Volume.offset & " where Doc_ID = " & Result.ID
        Server.Con.ExecuteNonQuery(CommandType.Text, Strupdate)
        Try
            Dim FileLen As Decimal = CDec(Fi.Length) / 1000
            If Server.isOracle Then
                Dim parNames() As String = {"VolumeId", "FileSize"}
                ' Dim parTypes() As Object = {13, 13}
                Dim parValues() As Object = {Result.Volume.ID, FileLen}
                Server.Con.ExecuteNonQuery("UpdateData_pkg.UpdateData", parValues)
            Else
                Dim parametersValues() As Object = {Result.Volume.ID, FileLen}
                Server.Con.ExecuteDataset("UpdateData_pkg.UpdateData", parametersValues)
            End If
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try

    End Sub

#End Region

#Region "Versioning"

    Public Shared Function GetParentVersionId(ByVal DocTypeid As Int64, ByVal docid As Int64) As Int64
        Dim SelectParent As New StringBuilder
        SelectParent.Append("SELECT ver_parent_id FROM DOC_T")
        SelectParent.Append(DocTypeid.ToString)
        SelectParent.Append(" WHERE doc_id = ")
        SelectParent.Append(docid.ToString)

        Return Server.Con.ExecuteScalar(CommandType.Text, SelectParent.ToString)
    End Function

    Public Shared Function CountChildsVersions(ByVal DocTypeid As Int64, ByVal parentid As Int64) As Int32
        Dim Childs As String = "SELECT count(1) FROM DOC_T" + DocTypeid.ToString + " where ver_parent_id = " & parentid.ToString
        Return Server.Con.ExecuteScalar(CommandType.Text, Childs)
    End Function

    Public Shared Function GetNewVersionID(ByVal RootId As Long, ByVal doctype As Int32, ByVal originalDocId As Int64) As Int32
        Dim Query As New System.Text.StringBuilder

        Query.Append("SELECT max(NumeroVersion) FROM doc_t")
        Query.Append(doctype.ToString())
        Query.Append(" WHERE ver_Parent_id= ")
        Query.Append(originalDocId.ToString())
        If Not IsDBNull(Server.Con.ExecuteScalar(CommandType.Text, Query.ToString)) Then
            Return 1 + Server.Con.ExecuteScalar(CommandType.Text, Query.ToString)
        Else
            Return 1
        End If


    End Function

    Public Shared Sub setParentVersion(ByVal docTypeId As Int32, ByVal docId As Int64)
        Dim Query As New System.Text.StringBuilder()
        Query.Append("UPDATE doc_t")
        Query.Append(docTypeId.ToString())
        Query.Append(" SET version = 1 WHERE doc_id = ")
        Query.Append(docId)

        Server.Con.ExecuteNonQuery(CommandType.Text, Query.ToString())
    End Sub

    Public Shared Sub SaveVersionComment(ByRef Result As NewResult)
        Try
            'todo: marcos hacer tabla y store para guardar el comment
            If Server.isOracle Then
                'Dim ParNames() As Object = {"Par_docId", "Par_comment"}
                ' Dim parTypes() As Object = {13, 7}
                Dim parValues() As Object = {Result.ID, Result.Comment}
                Server.Con.ExecuteNonQuery("ZSP_VERSION_300.INSERTVERSIONCOMMENT", parValues)
            Else
                Dim parameters() As Object = {Result.ID, Result.Comment}
                Server.Con.ExecuteNonQuery("ZSP_VERSION_300_INSERTVERSIONCOMMENT", parameters)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shared Sub SaveVersionComment(ByVal ResultID As Int64, ByVal ResultComment As String)
        Try
            'todo: marcos hacer tabla y store para guardar el comment
            If Server.isOracle Then
                'Dim ParNames() As Object = {"Par_docId", "Par_comment"}
                ' Dim parTypes() As Object = {13, 7}
                Dim parValues() As Object = {ResultID, ResultComment}
                Server.Con.ExecuteNonQuery("ZSP_VERSION_300.INSERTVERSIONCOMMENT", parValues)
            Else
                Dim parameters() As Object = {ResultID, ResultComment}
                Server.Con.ExecuteNonQuery("ZSP_VERSION_300_INSERTVERSIONCOMMENT", parameters)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shared Sub SetVersionComment(ByVal rID As Int64, ByVal rComment As String)
        Server.Con.ExecuteNonQuery(CommandType.Text, "UPDATE ZComment SET COMMENTS = '" & rComment & "' WHERE DOCID = '" & rID & "'")
    End Sub

    Public Shared Function GetVersionComment(ByVal ResultId As Int64) As String
        Return Zamba.Servers.Server.Con.ExecuteScalar(CommandType.Text, "SELECT COMMENTS FROM ZComment WHERE DOCID = " & ResultId)
    End Function

    Public Shared Function GetVersionCommentDate(ByVal ResultId As Int64) As String
        Return Zamba.Servers.Server.Con.ExecuteScalar(CommandType.Text, "SELECT CreateDate FROM ZComment WHERE DOCID = " & ResultId)
    End Function

    Public Shared Sub SavePublish(ByVal publishid As Int32, ByVal docid As Int64, ByVal userid As Int32, ByVal publishdate As Date)
        If Server.isOracle Then
            'Dim ParNames() As Object = {"Parm_publishid", "Parm_docid", "Parm_userid", "Par_publishdate"}
            ' Dim parTypes() As Object = {13, 13, 13, 13} ' encontrar el tipo date
            Dim parValues() As Object = {publishid, docid, userid, publishdate}
            Server.Con.ExecuteNonQuery("ZSP_VERSION_300.INSERTPUBLISH", parValues)
        Else
            Dim parameters() As Object = {publishid, docid, userid, publishdate}
            Server.Con.ExecuteNonQuery("ZSP_VERSION_Insert_Publish", parameters)
        End If
    End Sub

    ''ABM Alta - Baja - Modificacion
    'Public Shared Sub SaveState(ByVal state As IPublishState)
    '    'TODO Diego
    '    Try
    '        Server.Con.ExecuteNonQuery("VersioningCreateState", state.StateId, state.StateName)
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '    End Try
    'End Sub
    'Public Shared Sub UpdateState(ByVal state As IPublishState)
    '    'TODO Diego
    '    Try
    '        Server.Con.ExecuteNonQuery("VersioningUpdateState", state.StateId, state.StateName)
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '    End Try
    'End Sub
    'Public Shared Sub DeleteState(ByVal state As IPublishState)
    '    'TODO Diego
    '    Try
    '        Server.Con.ExecuteNonQuery("VersioningDeleteState", state.StateId, state.StateName)
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '    End Try
    'End Sub


    'Public Shared Function GetStates(ByVal docId As Int32) As Generic.List(Of IPublishState)
    '    'TODO Diego
    '    'dataset -> datatble ->
    '    Try
    '        'todo DIEGO: recuperar por ID el comment Y LA FECHA DE ZCOMMENT
    '        Dim ds As New DataSet
    '        ds = Zamba.Servers.Server.Con.ExecuteDataset(CommandType.Text, "SELECT * FROM z_publish INNER JOIN	z_states ON z_publish.stateid = z_states.stateid")
    '        Dim List As New Generic.List(Of IPublishState)
    '        If ds.Tables(0).Rows.Count > 0 Then
    '            List.Add(ds.Tables(0).Rows(0).Item(3).ToString())
    '            List.Add(ds.Tables(0).Rows(0).Item(4).ToString())
    '        End If
    '        Return List
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '    End Try

    '    Return Nothing ' incluido en codigo

    'End Function

    'Public Shared Sub Publish(ByVal asd As Ipublishable)

    'End Sub

    Public Shared Function GetPublishEvents() As DataSet
        Return Server.Con.ExecuteDataset(CommandType.Text, "SELECT * FROM ZVerEvents")
    End Function


    Public Shared Function GetPublishEventsIds(ByVal EventName As String) As Int32
        Return Server.Con.ExecuteScalar(CommandType.Text, "SELECT EventId FROM ZVerEvents WHERE Event = '" + EventName + "'")
    End Function

    Public Shared Function GetPublishableIndexsStates(ByVal idDocType As Int64) As DataSet
        Dim dataset As New DataSet
        If Server.ServerType = DBTYPES.MSSQLServer OrElse Server.ServerType = DBTYPES.MSSQLServer7Up Then
            Dim query As String = "SELECT DOC_TYPE.DOC_TYPE_NAME as 'Entidad', DOC_INDEX.INDEX_NAME as 'Indice', Zverevents.Event as 'Evento', ZVerEv.EvValue as 'Valor' FROM ZVerConfig INNER Join DOC_INDEX ON ZVerConfig.IndexId = DOC_INDEX.INDEX_ID INNER Join DOC_TYPE ON ZVerConfig.DtId = DOC_TYPE.DOC_TYPE_ID INNER Join ZVerEv ON ZVerConfig.dataid = ZVerEv.dataid INNER Join Zverevents ON ZVerEv.eventid= Zverevents.eventid WHERE DtId = " + idDocType.ToString
            dataset = Server.Con.ExecuteDataset(CommandType.Text, query)
        Else
            dataset = Server.Con.ExecuteDataset(CommandType.Text, "SELECT DOC_TYPE.DOC_TYPE_NAME as " + Chr(34) + "Entidad" + Chr(34) + " , DOC_INDEX.INDEX_NAME as " + Chr(34) + "Indice" + Chr(34) + " , Zverevents.Event as " + Chr(34) + "Evento" + Chr(34) + " , ZVerEv.EvValue as " + Chr(34) + "Valor" + Chr(34) + " FROM ZVerConfig INNER Join DOC_INDEX ON ZVerConfig.IndexId = DOC_INDEX.INDEX_ID INNER Join DOC_TYPE ON ZVerConfig.DtId = DOC_TYPE.DOC_TYPE_ID INNER Join ZVerEv ON ZVerConfig.dataid = ZVerEv.dataid INNER Join Zverevents ON ZVerEv.eventid= Zverevents.eventid WHERE DtId = " + idDocType.ToString)
        End If
        Return dataset
    End Function

    Public Shared Sub SavePublishableIndexsState(ByVal dataid As Int64, ByVal DocTypeid As Int64, ByVal Indexid As Int64, ByVal eventId As Int32, ByVal DefValue As String)
        Server.Con.ExecuteNonQuery(CommandType.Text, "INSERT INTO ZVerConfig(DataId, DtId, IndexId) VALUES( " + dataid.ToString + ", " + DocTypeid.ToString + ", " + Indexid.ToString + ")")
        Server.Con.ExecuteNonQuery(CommandType.Text, "INSERT INTO ZVerEv(DataId, EventId, EvValue) VALUES( " + dataid.ToString + ", " + eventId.ToString + ", '" + DefValue + "')")
    End Sub
    Public Shared Function ValidatePublishableIndexsStateExistance(ByVal DocTypeid As Int64, ByVal Indexid As Int64, ByVal eventId As Int32) As Int32
        Dim query As String = "SELECT COUNT(1) FROM ZVerConfig INNER JOIN ZVerEv ON ZVerConfig.DataId = ZVerEv.DataId WHERE DtId =" + DocTypeid.ToString + " AND IndexId = " + Indexid.ToString + " AND EventId = " + eventId.ToString
        Dim count As Int32 = Server.Con.ExecuteScalar(CommandType.Text, query)
        Return count
    End Function

    Public Shared Sub DeletePublishableIndexsState(ByVal DocTypeid As Int64, ByVal Indexid As Int64, ByVal DefValue As String)
        Dim query As String = "SELECT ZVerConfig.DataId FROM ZVerConfig INNER JOIN ZVerEv ON ZVerConfig.DataId = ZVerEv.DataId WHERE ZverConfig.DtId = " + DocTypeid.ToString + "  AND ZverConfig.IndexId = " + Indexid.ToString + " AND ZVerEv.EvValue = '" + DefValue + "'"
        Dim dataid As Int64 = Server.Con.ExecuteScalar(CommandType.Text, query)
        Server.Con.ExecuteNonQuery(CommandType.Text, "DELETE FROM ZVerEv WHERE DataId = " + dataid.ToString)
        Server.Con.ExecuteNonQuery(CommandType.Text, "DELETE FROM ZVerConfig WHERE DataId = " + dataid.ToString)
    End Sub

    Public Shared Function GetParentsCountForVersions(ByVal DocId As Int64, ByVal doctypeId As Int64) As Int32
        Dim query As New StringBuilder()
        Dim parentid As Int64
        Dim parentsCount As Int32
        parentid = DocId
        query.Append("SELECT ver_parent_id FROM DOC_T")
        query.Append(doctypeId.ToString)
        query.Append(" WHERE Doc_Id =")
        query.Append(DocId.ToString)
        ' hay un documento que no tiene padre (root)

        Dim haveParentDoc As Int64 = Con.ExecuteScalar(CommandType.Text, query.ToString)

        If haveParentDoc <> 0 Then

            Do
                Dim query2 As New StringBuilder()
                query2.Append("SELECT ver_parent_id FROM DOC_T")
                query2.Append(doctypeId.ToString)
                query2.Append(" WHERE Doc_Id =")
                query2.Append(parentid.ToString)
                parentid = Con.ExecuteScalar(CommandType.Text, query2.ToString)
                parentsCount += 1
            Loop While parentid <> 0

        End If

        Return parentsCount
    End Function

#End Region

#Region "AutoName"

    Public Shared Function GetAutoNameCode(ByVal DocTypeId As Int32) As String
        Dim strselect As String = "Select AutoName from Doc_Type where(Doc_Type_Id = " & DocTypeId & ")"
        Dim DsDocType As DataSet
        DsDocType = Server.Con.ExecuteDataset(CommandType.Text, strselect)
        Dim AutoNameCode As String
        If DsDocType.Tables(0).Rows.Count > 0 Then
            AutoNameCode = DirectCast(DsDocType.Tables(0).Rows(0).Item("AutoName"), String).Trim
        Else
            AutoNameCode = "@DT@ - @FC@ - @FM@"
        End If
        Return AutoNameCode
    End Function
    Public Sub SaveName(ByRef Result As Result)

        If Server.isOracle Then
            'Dim ParNames() As Object = {"resultname", "resultid", "dtid"}
            ' Dim parTypes() As Object = {OracleType.VarChar, OracleType.Number, OracleType.Number}
            Dim parValues() As Object = {Result.Name, Result.ID, Result.DocTypeId}
            Server.Con.ExecuteNonQuery("ZSP_AUTONAME_100.SaveName", parValues)
        Else
            Dim Table As String = MakeTable(Result.DocType.ID, TableType.Document)
            Dim StrUpDate As String = "UPDATE " & Table & " SET Name = '" & Results_Factory.EncodeQueryString(Result.Name) & "' where Doc_id =" & Result.ID
            Server.Con.ExecuteNonQuery(CommandType.Text, StrUpDate)
            Try
                StrUpDate = "UPDATE wfdocument SET Name = '" & Results_Factory.EncodeQueryString(Result.Name) & "' where Doc_id =" & Result.ID
                Server.Con.ExecuteNonQuery(CommandType.Text, StrUpDate)
            Catch ex As Exception
                ZClass.raiseerror(ex)

            End Try
        End If

    End Sub

#End Region


    ''' <summary>
    ''' Devuelve el path de un documento
    ''' por su id de doc. y id de tipo de doc.
    ''' </summary>
    ''' <returns>path</returns>
    Public Shared Function getPathForIdTypeIdDoc(ByVal doc_id As Int32,
    ByVal doc_type_id As Int32) As String
        Try
            Dim resultado As DataSet

            If Server.isOracle Then

                'Server.Con.ExecuteNonQuery(CommandType.Text, "set serveroutput on")

                Dim parValues() As Object = {doc_id, doc_type_id, 2}
                'Dim ParNames() As Object = {"doc_id", "doc_type_id", "io_cursor"}
                ' Dim parTypes() As Object = {13, 13, 5}


                resultado =
                Server.Con.ExecuteDataset("ZSP_ZBARCODE_200_PKG.getPathForIdTypeIdDoc", parValues)

                'Server.Con.ExecuteNonQuery(CommandType.Text, "set serveroutput off")
            Else
                Dim parValues() As Object = {doc_id, doc_type_id}
                resultado =
                Server.Con.ExecuteDataset("zsp_zBarcode_200_getPathForIdTypeIdDoc",
                parValues)
            End If

            ' Si no hay resultados se devuelve una tabla vacia...
            If IsNothing(resultado) OrElse
             resultado.Tables.Count = 0 OrElse
             resultado.Tables(0).Rows.Count = 0 Then
                Return String.Empty
            End If

            Return DirectCast(resultado.Tables(0).Rows(0).Item(0), String)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return Nothing
    End Function

#Region "WorkFlow"

    'Inserta un nuevo registro en la Tabla ZI, lo que indica 
    'que hay un nuevo documento entrante. [Alejandro].
    Public Shared Sub InsertZI(ByVal _InsertID As Int64, ByVal _DTID As Int64, ByVal _DocID As Int64, ByVal _IDate As Date, ByVal _name As String, ByVal _UserId As Int64)

        Dim sqlBuilder As New System.Text.StringBuilder

        Try

            sqlBuilder.Append("INSERT INTO ZI (InsertID, DTID, DocID, FolderID, IDate, name, UserID) VALUES ('")
            sqlBuilder.Append(_InsertID.ToString())
            sqlBuilder.Append("', '")
            sqlBuilder.Append(_DTID.ToString())
            sqlBuilder.Append("', '")
            sqlBuilder.Append(_DocID.ToString())
            sqlBuilder.Append("', '")
            sqlBuilder.Append(0)
            sqlBuilder.Append("', ")
            sqlBuilder.Append(Server.Con.ConvertDate(_IDate))
            sqlBuilder.Append(",'")
            sqlBuilder.Append(_name)
            sqlBuilder.Append("', '")
            sqlBuilder.Append(_UserId.ToString)
            sqlBuilder.Append("')")

            ''Oracle inserta el semiColon autom?ticamente, asi que lo ponemos en caso SQL
            'If ServerType = DBTYPES.MSSQLServer7Up Or ServerType = DBTYPES.MSSQLServer Then
            '    sqlBuilder.Append(";")
            'End If

            Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    'Devuelve un DataTable con todos los WAIT ID esperando a por ese DTID (en la Tabla ZWFI). [Alejandro].
    Public Shared Function SelectWaitingDocTypeInZWFI(ByVal _DTID As Int64) As DataTable

        Dim sqlBuilder As New System.Text.StringBuilder()
        Dim ds As New DataSet
        Dim dt As New DataTable

        Try

            sqlBuilder.Append("SELECT WI FROM ZWFI WHERE DTID = '")
            sqlBuilder.Append(_DTID.ToString())
            sqlBuilder.Append("'")

            'Oracle inserta el semiColon autom?ticamente, asi que lo ponemos en caso SQL
            If ServerType = DBTYPES.MSSQLServer7Up Or ServerType = DBTYPES.MSSQLServer Then
                sqlBuilder.Append(";")
            End If

            ds = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())
            dt = ds.Tables(0)

            Return dt

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Return dt
    End Function

    'Por cada WAIT ID llama a la funci?n ZWFIIIndexsValidation, si todos los WI devuelven
    'verdadero en esa funci?n, entonces esta funci?n devuelve verdadero. Si alguno de low WI
    'devuelven False o hay alg?n error. Esta funci?n devuelve false.[Alejandro].
    Public Shared Function ZWFIIValidation(ByRef _dt As DataTable, ByVal _IID As Int64(), ByVal _IValue As String()) As Int64()
        Dim _waitIDs As Int64() = {}
        Dim i As Int16 = 0

        Try
            For Each _dr As DataRow In _dt.Rows

                If ZWFIIIndexsValidation(_dr("WI"), _IID, _IValue) Then
                    _waitIDs(i) = _dr("WI")
                    i = i + 1
                End If
            Next

            Return _waitIDs

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return _waitIDs

    End Function

    'Verifica si hay documentos en ZWFI esperando por el DocType pasado por par?metro.[Alejandro].
    Public Shared Function VerifyIfWaitingDocuments(ByVal ruleId As Int64) As Int16

        Dim sqlBuilder As New StringBuilder
        Dim count As Int16

        Try
            sqlBuilder.Append("SELECT COUNT(1) FROM ZWFI WHERE RuleID = '")
            sqlBuilder.Append(ruleId.ToString())
            sqlBuilder.Append("'")

            count = Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString())

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return count

    End Function

    'Valida en la tabla ZWFII que ese WI (WaitID) tenga asignado todos los _IID e _IValue. [Alejandro].
    Public Shared Function ZWFIIIndexsValidation(ByVal _WI As Int32, ByVal _IID As Int64(), ByVal _IValue As String()) As Boolean

        Try
            For i As Int32 = 0 To i >= _IID.Length - 1

                If Not ZWFIIIndexValidation(_WI, _IID(i), _IValue(i)) Then
                    Return False
                End If

            Next

            Return True

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Function

    'Cuenta la cantidad de WI que hay en la tabla ZWFII donde los valores que se pasan en los
    'parametros. [Alejandro].
    Public Shared Function ZWFIIIndexValidation(ByVal _WI As Int32, ByVal _IID As Int64, ByVal _IValue As String) As Boolean

        Dim sqlBuilder As New StringBuilder
        Dim _count As Int16

        Try
            sqlBuilder.Append("SELECT COUNT(1) FROM ZWFII WHERE WI = '")
            sqlBuilder.Append(_WI.ToString())
            sqlBuilder.Append("' AND IID = '")
            sqlBuilder.Append(_IID.ToString())
            sqlBuilder.Append("' AND IValue = '")
            sqlBuilder.Append(_IValue)
            sqlBuilder.Append("'")

            _count = CInt(Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString()))

            If _count > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return False

    End Function

    'En cada row del DataTable updatea el InsertID especificado por par?metro.
    'llamando a la funci?n ZWFIUpdateInsertID. [Alejandro].
    Public Shared Sub ZWFIUpdateInsertID(ByVal _waitIDs As Int64(), ByVal _InsertID As Int64)

        Try
            For i As Int16 = 0 To i >= _waitIDs.Length
                ZWFIUpdateInsertID(_waitIDs(i), _InsertID)
            Next

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    'Updatea el InsertID pasado por par?metro en el WAIT ID pasado por par?metro
    'en la Tabla ZWFI. [Alejandro].
    Private Shared Sub ZWFIUpdateInsertID(ByVal _WI As Int32, ByVal _InsertID As Int32)

        Try
            Dim sqlbuilder As New System.Text.StringBuilder()

            sqlbuilder.Append("UPDATE ZWFI SET InsertID = '")
            sqlbuilder.Append(_InsertID.ToString())
            sqlbuilder.Append("' WHERE WI = '")
            sqlbuilder.Append(_WI.ToString())
            sqlbuilder.Append("'")

            'Oracle inserta el semiColon autom?ticamente, asi que lo ponemos en caso SQL
            If ServerType = DBTYPES.MSSQLServer7Up Or ServerType = DBTYPES.MSSQLServer Then
                sqlbuilder.Append(";")
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shared Function GetWIFromZIWhereRuleID(ByVal lngRuleID As Int64) As List(Of Int64)

        Dim WIs As New List(Of Int64)
        Dim ds As New DataSet

        ds = GetZIWhereRuleID(lngRuleID)

        If Not IsDBNull(ds) AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
            For Each r As DataRow In ds.Tables(0).Rows
                WIs.Add(Convert.ToInt64(r("WI")))
            Next
        End If

        Return WIs

    End Function

    Public Shared Function GetZIWhereRuleID(ByVal lngRuleID As Int64) As DataSet
        Dim sqlBuilder As New StringBuilder()
        Dim ds As New DataSet

        sqlBuilder.Append("SELECT * FROM ZI WHERE RuleID LIKE '%")
        sqlBuilder.Append(lngRuleID.ToString())
        sqlBuilder.Append("%'")

        ds = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())

        Return ds

    End Function

    Public Shared Function GetZIWhereDocID(ByVal lngDocID As Int64) As DataSet
        Dim sqlBuilder As New StringBuilder()
        Dim ds As New DataSet

        sqlBuilder.Append("SELECT * FROM ZI WHERE DocID = '")
        sqlBuilder.Append(lngDocID.ToString())
        sqlBuilder.Append("'")

        ds = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())

        Return ds

    End Function



    Public Shared Function GetZIWhereRuleIDAndDocID(ByVal lngRuleID As Int64, ByVal docID As Int64) As DataSet
        Dim sqlBuilder As New StringBuilder()
        Dim ds As New DataSet

        sqlBuilder.Append("SELECT * FROM ZI WHERE DocID = '")
        sqlBuilder.Append(docID.ToString())
        sqlBuilder.Append("' AND RuleID LIKE '%")
        sqlBuilder.Append(lngRuleID.ToString())
        sqlBuilder.Append("%'")

        ds = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())

        Return ds

    End Function

    Public Shared Sub SetRuleIDNotWaiting(ByVal lngRuleID As Int64, ByVal lngInsertID As Int64)

        Dim sqlBuilder As New StringBuilder()
        Dim valueToInsert As New StringBuilder()
        Dim ruleIDs As New List(Of Int64)

        Try
            ruleIDs = GetRuleIDsFromZIWhereInsertID(lngInsertID)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Dim i As Int16 = 0
        For Each ruleID As Int64 In ruleIDs
            If i = 0 Then
                valueToInsert.Append(ruleID)
            Else
                valueToInsert.Append(",")
                valueToInsert.Append(ruleID)
            End If
            i = i + 1
        Next

        If ruleIDs.Count = 0 Then
            sqlBuilder.Append("UPDATE ZI SET RuleID = '")
            sqlBuilder.Append(valueToInsert.ToString())
            sqlBuilder.Append("' WHERE InsertID = ")
            sqlBuilder.Append(lngInsertID.ToString())
        End If


    End Sub

    Public Shared Function GetRuleIDsFromZIWhereInsertID(ByVal lngInsertID As Int64) As List(Of Int64)

        Dim sqlBuilder As New StringBuilder()
        Dim ds As New DataSet()
        Dim ruleIDs As New List(Of Int64)

        sqlBuilder.Append("SELECT RuleID FROM ZI WHERE InsertID = '")
        sqlBuilder.Append(lngInsertID.ToString())
        sqlBuilder.Append("'")

        If Not IsDBNull(ds) AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
            For Each r As DataRow In ds.Tables(0).Rows
                For Each ruleID As String In r("RuleID").ToString().Split(Char.Parse("|"))
                    ruleIDs.Add(Int64.Parse(ruleID))
                Next
            Next
        End If

        Return ruleIDs

    End Function

    Public Shared Function GetDocTypesZWFI(ByVal ruleID As Int64) As DataSet

        Dim sqlBuilder As New StringBuilder

        Dim ds As New DataSet

        sqlBuilder.Append("SELECT DTID FROM ZWFI WHERE RuleID = '")
        sqlBuilder.Append(ruleID.ToString())
        sqlBuilder.Append("'")

        ds = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())

        Return ds

    End Function

    Public Shared Function GetWIFromZWFI(ByVal ruleID As Int64) As DataSet

        Dim ds As New DataSet

        Dim sqlBuilder As New StringBuilder

        sqlBuilder.Append("SELECT * FROM ZWFI WHERE RuleID = '")
        sqlBuilder.Append(ruleID.ToString())
        sqlBuilder.Append("'")

        ds = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())

        Return ds

    End Function

    Public Shared Function GetZIbyDocType(ByVal docType As Int64) As DataSet

        Dim ds As New DataSet()
        Dim sqlBuilder As New StringBuilder()

        sqlBuilder.Append("SELECT * FROM ZI WHERE DTID = '")
        sqlBuilder.Append(docType.ToString())
        sqlBuilder.Append("'")

        ds = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())

        Return ds

    End Function

    Public Shared Function GetZIbyWI(ByVal wI As Int64) As DataSet

        Dim ds As New DataSet()
        Dim sqlBuilder As New StringBuilder()

        sqlBuilder.Append("SELECT * FROM ZI WHERE WI = '")
        sqlBuilder.Append(wI.ToString())
        sqlBuilder.Append("'")

        ds = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())

        Return ds

    End Function

    Public Shared Function GetFullNameWhereDocID(ByVal lngDocType As Int64, ByVal lngDocID As Int64) As String
        Dim FullName As String = String.Empty
        Dim sqlBuilder As New StringBuilder()

        sqlBuilder.Append("SELECT Original_filename FROM DOC")
        sqlBuilder.Append(lngDocType.ToString())
        sqlBuilder.Append(" WHERE DOC_ID = '")
        sqlBuilder.Append(lngDocID.ToString())
        sqlBuilder.Append("'")

        FullName = Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString()).ToString()

        Return FullName

    End Function

    Public Shared Function GetZWFIIbyWI(ByVal wI As Int64) As DataSet

        Dim ds As New DataSet
        Dim sqlBuilder As New StringBuilder()

        sqlBuilder.Append("SELECT * FROM ZWFII WHERE WI = '")
        sqlBuilder.Append(wI.ToString())
        sqlBuilder.Append("'")

        ds = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())

        Return ds

    End Function

    Public Shared Function GetZWFIbyWI(ByVal wI As Int64) As DataSet

        Dim ds As New DataSet
        Dim sqlBuilder As New StringBuilder()

        sqlBuilder.Append("SELECT * FROM ZWFI WHERE WI = '")
        sqlBuilder.Append(wI.ToString())
        sqlBuilder.Append("'")

        ds = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())

        Return ds

    End Function

    Public Shared Function GetZWFIbyRuleID(ByVal lngRuleID As Int64) As DataSet

        Dim ds As New DataSet
        Dim sqlBuilder As New StringBuilder()

        sqlBuilder.Append("SELECT * FROM ZWFI WHERE RuleID = '")
        sqlBuilder.Append(lngRuleID.ToString())
        sqlBuilder.Append("'")

        ds = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())

        Return ds

    End Function

    Public Shared Function ValidateIsDocTypeInZI(ByVal docType As Int64) As Boolean

        Dim ds As New DataSet
        Dim count As Int16
        Dim sqlBuilder As New StringBuilder()

        sqlBuilder.Append("SELECT COUNT(1) FROM ZI WHERE DTID = '")
        sqlBuilder.Append(docType.ToString())
        sqlBuilder.Append("'")

        count = Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString())

        If count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Shared Function GetIndexValueFromDocI(ByVal docType As Int64, ByVal iID As Int64, ByVal docID As Int64, ByRef iValue As String) As Boolean

        Dim ds As New DataSet

        Dim sqlBuilder As New StringBuilder()

        sqlBuilder.Append("SELECT I")
        sqlBuilder.Append(iID.ToString())
        sqlBuilder.Append(" FROM DOC")
        sqlBuilder.Append(docType.ToString())
        sqlBuilder.Append("WHERE DOC_ID = '")
        sqlBuilder.Append(docID.ToString())
        sqlBuilder.Append("'")

        ds = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())

        If Not IsDBNull(ds) AndAlso Not IsNothing(ds.Tables(0)) Then
            If ds.Tables(0).Rows.Count > 0 Then

                For Each row As DataRow In ds.Tables(0).Rows
                    iValue = Convert.ToString(row("IValue"))
                    Return True
                Next

            Else
                Return False
            End If

        Else
            Return False
        End If

    End Function

    Public Shared Function GetDocIDsFromZI(ByVal docType As Int64) As DataSet

        Dim ds As New DataSet
        Dim sqlBuilder As New StringBuilder()

        sqlBuilder.Append("SELECT DocID FROM ZI WHERE DTID = '")
        sqlBuilder.Append(docType.ToString())
        sqlBuilder.Append("'")

        ds = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())

        Return ds

    End Function

    Public Shared Function GetIndexValueFromDoc_I(ByVal docType As Int64, ByVal docID As Int64, ByVal indexID As Int64) As String

        Dim ds As New DataSet()
        Dim sqlBuilder As New StringBuilder()
        Dim iValue As String = String.Empty


        sqlBuilder.Append("SELECT I")
        sqlBuilder.Append(indexID.ToString())
        sqlBuilder.Append(" FROM DOC")
        sqlBuilder.Append(docType.ToString())
        sqlBuilder.Append(" WHERE DOC_ID = '")
        sqlBuilder.Append(docID.ToString())
        sqlBuilder.Append("'")

        If IsDBNull(Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString())) = True Then
            iValue = String.Empty
        Else
            iValue = Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString())
        End If


        Return iValue

    End Function

    Public Shared Function GetIndexValueFromZWFII(ByVal wI As Int64, ByVal indexID As Int64) As String

        Dim iValue As String = String.Empty
        Dim sqlBuilder As New StringBuilder()

        sqlBuilder.Append("SELECT IValue FROM ZWFII WHERE WI = '")
        sqlBuilder.Append(wI.ToString())
        sqlBuilder.Append("' AND IID = '")
        sqlBuilder.Append(indexID.ToString())
        sqlBuilder.Append("'")

        iValue = Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString())

        Return iValue

    End Function

    Public Shared Sub DeleteFromZWFI(ByVal wI As Int64)

        Dim sqlBuilder As New StringBuilder()

        sqlBuilder.Append("DELETE FROM ZWFI WHERE WI = '")
        sqlBuilder.Append(wI.ToString())
        sqlBuilder.Append("'")

        Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())

    End Sub

    Public Shared Sub DeleteFromZWFII(ByVal wI As Int64)

        Dim sqlBuilder As New StringBuilder()

        sqlBuilder.Append("DELETE FROM ZWFII WHERE WI = '")
        sqlBuilder.Append(wI.ToString())
        sqlBuilder.Append("'")

        Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())

    End Sub

    Public Shared Sub DeleteFromZI(ByVal lngDocID As Int64)

        Dim sqlBuilder As New StringBuilder()

        sqlBuilder.Append("DELETE FROM ZI WHERE DocID = '")
        sqlBuilder.Append(lngDocID.ToString())
        sqlBuilder.Append("'")

        Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())

    End Sub

    Public Shared Function GetIndexFromZWFII(ByVal wI As Int64) As DataSet

        Dim sqlBuilder As New StringBuilder()
        Dim ds As New DataSet()

        sqlBuilder.Append("SELECT IID, IValue FROM ZWFII WHERE WI = '")
        sqlBuilder.Append(wI.ToString())
        sqlBuilder.Append("'")

        ds = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())

        Return ds

    End Function

    Public Shared Function GetWatingDocumentMails(ByVal lngRuleID As Int64) As DataSet

        Dim ds As New DataSet()
        Dim sqlBuilder As New StringBuilder()

        sqlBuilder.Append("SELECT * FROM Z_GroupToNotify WHERE DocId = '")
        sqlBuilder.Append(lngRuleID.ToString())
        sqlBuilder.Append("'")

        ds = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())

        Return ds

    End Function

    Public Shared Function GetZWFIbyDocType(ByVal lngDocType As Int64) As DataSet

        Dim ds As New DataSet()
        Dim sqlBuilder As New StringBuilder()

        sqlBuilder.Append("SELECT * FROM ZWFI WHERE DTID = '")
        sqlBuilder.Append(lngDocType.ToString())
        sqlBuilder.Append("'")

        ds = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())

        Return ds

    End Function

#End Region

#Region "Creation Info"

    Public Shared Function GetCreatorUser(ByVal docid As Int64) As String
        Dim query As New StringBuilder
        query.Append("SELECT USRTABLE.NAME ")
        query.Append("FROM USER_HST INNER JOIN ")
        query.Append("USRTABLE ON USER_HST.USER_ID = USRTABLE.ID ")
        query.Append("WHERE USER_HST.ACTION_TYPE = 3 AND USER_HST.OBJECT_ID =")
        query.Append(docid.ToString)

        Return Server.Con.ExecuteScalar(CommandType.Text, query.ToString)
    End Function

#End Region

#Region "Update Result"

    Public Shared Sub UpdateLastResultVersioned(ByVal DocTypeid As Int64, ByVal parentid As Int64)
        Dim query As New StringBuilder
        query.Append("UPDATE DOC_T")
        query.Append(DocTypeid.ToString)
        query.Append(" SET version = 0")
        query.Append(" WHERE Doc_Id = ")
        query.Append(parentid.ToString)
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
    End Sub

    Public Shared Sub UpdateResultsVersionedDataWhenDelete(ByVal DocTypeid As Int64, ByVal parentid As Int64, ByVal docid As Int64, ByVal RootDocumentId As Int64)

        Dim max As String
        max = "SELECT MAX(NumeroVersion) FROM DOC_T" & DocTypeid.ToString & " WHERE Ver_parent_Id  = " & parentid.ToString
        Dim MaxNumeroVersion As Int32
        If Not IsDBNull(Server.Con.ExecuteScalar(CommandType.Text, max)) Then
            MaxNumeroVersion = Server.Con.ExecuteScalar(CommandType.Text, max)
        End If
        Dim selectchilds As String = "SELECT DOC_ID FROM DOC_T" & DocTypeid.ToString & " where ver_parent_id = " & docid.ToString
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, selectchilds)
        For Each row As DataRow In ds.Tables(0).Rows
            MaxNumeroVersion += 1
            Dim query As New StringBuilder
            query.Append("UPDATE DOC_T")
            query.Append(DocTypeid.ToString)
            query.Append(" SET ver_Parent_id = ")
            query.Append(parentid.ToString)
            query.Append(" , NumeroVersion = ")
            query.Append(MaxNumeroVersion.ToString)
            query.Append(" WHERE Doc_Id = ")
            query.Append(row.Item(0).ToString)
            Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
        Next

    End Sub

    Public Shared Sub UpdateOriginalName(ByVal DocTypeId As Int64, ByVal DocId As Int64, ByVal strOriginalName As String)
        Dim query As New StringBuilder
        query.Append("UPDATE DOC_T")
        query.Append(DocTypeId.ToString)
        query.Append(" SET Original_FileName = '")
        query.Append(strOriginalName + "' ")
        query.Append(" WHERE Doc_Id = ")
        query.Append(DocId.ToString)
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
    End Sub

#End Region

    ''' <summary>
    ''' M?todo que devuelve un dataset con el contenido de la tabla ZDocRelations
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="relationId"></param>
    ''' <param name="idRoot"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	01/08/2008	Created
    ''' </history>
    Public Shared Sub getZDocRelations(ByRef ds As DataSet)
        ds = Server.Con.ExecuteDataset(CommandType.Text, "SELECT * FROM ZDocRelations")
    End Sub

    ''' <summary>
    ''' Codifica el string para que sea apto para insertarlo en una query dinamica
    ''' </summary>
    ''' <param name="p1"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function EncodeQueryString(ByVal p1 As String) As String
        Dim tempString As String = Results_Factory.EncodeQuotedString(p1)
        Return tempString
    End Function

    ''' <summary>
    ''' Encodea un string de forma que las comillas simples que cortan un string literal en SQl sean reemplazadas por ''(doble comilla simple)
    ''' </summary>
    ''' <param name="p1"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function EncodeQuotedString(ByVal p1 As String) As String
        Dim quotedIndex As Integer = p1.IndexOf("'")
        Dim prevIndex As Integer = 0
        Dim sb As New StringBuilder

        If quotedIndex > -1 Then
            Return p1.Replace("'", "''")
        Else
            Return p1
        End If
    End Function

    Public Function ValidateNewResult(ByVal DocTypeId As Integer, ByVal Doc_ID As Integer) As Boolean
        Dim StrQuery As String = "SELECT * FROM DOC_T" & DocTypeId.ToString & " WHERE Doc_ID = " & Doc_ID
        Dim ds As DataTable = Server.Con.ExecuteDataset(CommandType.Text, StrQuery).Tables(0)

        If ds.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub UpdateFileIcon(resultId As Long, docTypeId As Long, iconID As Integer)
        Dim query As String = String.Format("update DOC_T{0} set ICON_ID = {1} where DOC_ID = {2}", docTypeId, iconID, resultId)
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

    Public Function GetCountOfBaremoIndex(ByVal baremoMuerteId As Long, ByVal indexName As String) As Boolean
        Dim StrQuery As String = String.Format("select Count(*) from doc_i10131 inner join doc_i10114 on doc_i10131.{0} = doc_i10114.{0} and doc_i10131.{0} = {1}", indexName, baremoMuerteId)
        Dim numberOfRowsAffected As Long = Server.Con.ExecuteScalar(CommandType.Text, StrQuery)
        If (numberOfRowsAffected > 0) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function GetBaremoMuerteReclamantes(ByVal idReclamo As Int64, ByVal idBaremoMuerte As Int64) As Object
        Dim reclamante As String = MakeTable(10131, Core.TableType.Indexs)
        Dim baremoMuerte As String = MakeTable(10114, Core.TableType.Indexs)
        Dim stringBuilder As StringBuilder = New StringBuilder()
        stringBuilder.Append("SELECT reclamante.I2706 AS Nombre,")
        stringBuilder.Append("reclamante.I10317 AS Tipo_Reclamante,")
        stringBuilder.Append(" reclamante.Doc_Id AS ResultId FROM ")
        stringBuilder.Append(reclamante)
        stringBuilder.Append(" reclamante INNER JOIN ")
        stringBuilder.Append("(SELECT baremoMuerte.doc_ID,baremoMuerte.I2677")
        stringBuilder.Append(",baremoMuerte.I1020121 FROM ")
        stringBuilder.Append(baremoMuerte)
        stringBuilder.Append(" baremoMuerte INNER JOIN ")
        stringBuilder.Append("wfdocument wf  ON baremoMuerte.doc_id = wf.doc_id)")
        stringBuilder.Append("datosBaremosMuerte ON reclamante.I2677 = datosBaremosMuerte.I2677 ")
        stringBuilder.Append("AND reclamante.I1020121 = datosBaremosMuerte.I1020121 ")
        stringBuilder.Append("AND reclamante.I2677 =")
        stringBuilder.Append(idReclamo)
        stringBuilder.Append(" AND reclamante.I1020121 =")
        stringBuilder.Append(idBaremoMuerte)



        Dim result As DataTable

        Try
            Dim query As String = stringBuilder.ToString()

            result = Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)


            '' result = Server.Con.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString())

        Catch ex As Exception
            ex.Message.ToString()
        End Try

        Return result
    End Function


    Public Function GetAsociatedToEditTable(ByVal idReclamo As Int64,
                                            ByVal idParent As Int64,
                                            ByVal idParentColumnName As Int64,
                                            ByVal docTypeId As Int64,
                                            ByVal associatedId As Int64,
                                            ByVal childAlias As String,
                                            ByVal parentAlias As String,
                                            ByVal indexs As Dictionary(Of Int64, String)) As Object
        Dim childTable As String = MakeTable(docTypeId, Core.TableType.Indexs)
        Dim parentTable As String = MakeTable(associatedId, Core.TableType.Indexs)
        Dim stringBuilder As StringBuilder = New StringBuilder()
        parentAlias = parentAlias.Trim.Replace(" ", "_")
        childAlias = parentAlias.Trim.Replace(" ", "_")
        'stringBuilder.Append("SELECT " & childAlias & ".I2706 AS Nombre,")
        stringBuilder.Append("SELECT ")

        For Each id As Int64 In indexs.Keys
            stringBuilder.Append(" " & childAlias & ".I" & id & " AS ")
            stringBuilder.Append(indexs(id).Trim.Replace(" ", "_") & ",")
        Next
        ' stringBuilder.Append(" " & childAlias & ".I10317 AS Tipo_Reclamante,")
        stringBuilder.Append(" " & childAlias & ".Doc_Id AS ResultId FROM ")
        stringBuilder.Append(childTable)
        stringBuilder.Append(" " & childAlias & " INNER JOIN ")
        stringBuilder.Append("(SELECT " & parentAlias & ".doc_ID," & parentAlias & ".I2677")
        stringBuilder.Append("," & parentAlias & ".I" & idParentColumnName & " FROM ")
        stringBuilder.Append(parentTable)
        stringBuilder.Append(" " & parentAlias & " INNER JOIN ")
        stringBuilder.Append("wfdocument wf  ON " & parentAlias & ".doc_id = wf.doc_id)")
        stringBuilder.Append("datosBaremosMuerte ON " & childAlias & ".I2677 = datosBaremosMuerte.I2677 ")
        stringBuilder.Append("AND " & childAlias & ".I" & idParentColumnName)
        stringBuilder.Append(" = datosBaremosMuerte.I" & idParentColumnName & " ")
        stringBuilder.Append("AND " & childAlias & ".I2677 =")
        stringBuilder.Append(idReclamo)
        stringBuilder.Append(" AND " & childAlias & ".I" & idParentColumnName & " =")
        stringBuilder.Append(idParent)



        Dim result As DataTable

        Try
            Dim query As String = stringBuilder.ToString()

            result = Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)


            '' result = Server.Con.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString())

        Catch ex As Exception
            ex.Message.ToString()
        End Try

        Return result
    End Function




    Public Function GetIdsFromABaremoMuerte(ByVal baremoDocId As Int64) As DataTable
        Dim baremoMuerte As String = MakeTable(10114, Core.TableType.Indexs)

        Dim strQuery As StringBuilder = New StringBuilder()
        strQuery.Append("SELECT baremoMuerte.I2677, baremoMuerte.I1020121 FROM ")
        strQuery.Append(baremoMuerte)
        strQuery.Append(" baremoMuerte INNER JOIN wfdocument wf ON baremoMuerte.doc_id = wf.doc_id AND baremoMuerte.doc_id = ")
        strQuery.Append(baremoDocId)

        Dim ds As DataSet = New DataSet()
        ds = Server.Con.ExecuteDataset(CommandType.Text, strQuery.ToString())
        Return ds.Tables(0)
    End Function

    Public Function GetIdsToAsociatedParents(ByVal docID As Int64, ByVal asociatedID As Int64, ByVal formID As Int64, ByVal tableAlias As String) As DataTable

        Dim associatedTable As String = MakeTable(asociatedID, Core.TableType.Indexs)
        tableAlias = tableAlias.Replace(" ", "_")
        Dim strQuery As StringBuilder = New StringBuilder()
        strQuery.Append("SELECT " & tableAlias & ".I2677, " & tableAlias & ".I" & formID & " FROM ")
        strQuery.Append(associatedTable)
        strQuery.Append("  " & tableAlias & "  INNER JOIN wfdocument wf ON  " & tableAlias & ".doc_id = wf.doc_id AND  " & tableAlias & " .doc_id = ")
        strQuery.Append(docID)

        Dim ds As DataSet = New DataSet()
        ds = Server.Con.ExecuteDataset(CommandType.Text, strQuery.ToString())
        Return ds.Tables(0)
    End Function


    Public Function GetRequestAssociatedResult(ByVal requestNumber As Int64, ByVal tableId As Int64, ByVal indexId As Int64,
                                               ByVal indexs As List(Of Int64)) As DataTable
        Dim strQuery As StringBuilder = New StringBuilder()
        Dim table As String = MakeTable(tableId, Core.TableType.Indexs)
        strQuery.Append("select")
        For Each index As Int64 In indexs
            strQuery.Append(" I" & index & " AS ")
            strQuery.Append(Indexs_Factory.GetIndexNameById(index).Trim.Replace(" ", "_").ToUpper() & ",")
        Next
        strQuery.Append("DOC_ID AS RESULTID")
        strQuery.Append(" from " & table & " where i" & indexId & "=")
        strQuery.Append(requestNumber)
        Return Server.Con.ExecuteDataset(CommandType.Text, strQuery.ToString()).Tables(0)
    End Function

    Public Function GetRequestNumber(ByVal tableId As Int64, ByVal indexId As Int64, ByVal docId As Int64) As Int64
        Dim strQuery As StringBuilder = New StringBuilder()
        Dim table As String = MakeTable(tableId, Core.TableType.Indexs)
        strQuery.Append("Select i" & indexId & " from " & table & " where doc_id =")
        strQuery.Append(docId)
        Dim dt As DataSet = Servers.Server.Con.ExecuteDataset(CommandType.Text, strQuery.ToString())
        If dt IsNot Nothing Then
            Return Int64.Parse(dt.Tables(0).Rows(0)(0).ToString())
        End If
    End Function



    Public Function GetFilesFromEntityByAttributeValue(ByVal idEntidad As String, ByVal indiceBusqueda As String, ByVal valor As String) As DataTable
        Dim strQuery As StringBuilder = New StringBuilder()

        strQuery.Append(String.Format("Select (dv.disk_vol_path + '\{0}\' + dt.offset + '\' + dt.doc_file from doc_t{0}  dt inner join disk_volume dv on dv.disk_vol_id = dt.vol_id  inner join doc_i{0}  di on di.doc_id = dt.doc_id and di.i{1} = '{2}'", idEntidad, indiceBusqueda, valor))

        Dim dt As DataSet = Con.ExecuteDataset(CommandType.Text, strQuery.ToString())

        Dim ListOfFiles As New List(Of String)

        If dt IsNot Nothing Then
            Return dt.Tables(0)
        End If
    End Function

    Public Function setIndexData(ByVal indexId As Int64, ByVal entityId As Int64, ByVal parentResultId As Int64,
                                 ByVal indexValue As String) As Boolean
        Try
            Dim StrUpdate As String = "update doc_i" & entityId & " set i" & indexId & " = '" & indexValue & "' where doc_id = " & parentResultId & ""
            Server.Con.ExecuteNonQuery(CommandType.Text, StrUpdate)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    'Public Function GetFamiliasCantidadBynroDespacho(ByVal nroDespacho As String) As DataTable
    '    Dim strQuery As StringBuilder = New StringBuilder()

    '    strQuery.Append("select i139603 as codigo, i139609 as cantidad from DOC_I139084 where i139548 = ")
    '    strQuery.Append(nroDespacho)

    '    Dim dt As DataSet = Servers.Server.Con.ExecuteDataset(CommandType.Text, strQuery.ToString())
    '    strQuery.Clear()
    '    strQuery = Nothing

    '    If dt IsNot Nothing Then
    '        Return dt.Tables(0)
    '    End If
    'End Function


    Public Function GetCalendar(ByVal entityId As String, titleAttriute As String, startAttribute As String, endAttribute As String, filterColumn As String, filterValue As String) As DataTable
        Try
            If entityId > 0 Then
                Dim DSIndexDataLst As New DataSet

                Dim StrSelect As New StringBuilder
                StrSelect.Append("select I" & titleAttriute)
                If Servers.Server.isOracle Then
                    StrSelect.Append(" title, I")
                Else
                    StrSelect.Append(" ""title"", I")
                End If
                StrSelect.Append(startAttribute)
                If Servers.Server.isOracle Then
                    StrSelect.Append(" [start], I")
                Else
                    StrSelect.Append(" ""start"", I")
                End If

                StrSelect.Append(endAttribute)
                If Servers.Server.isOracle Then
                    StrSelect.Append(" end from Doc_I")
                Else
                    StrSelect.Append(" ""end"" from Doc_I")
                End If
                StrSelect.Append(entityId)
                StrSelect.Append(" where I")
                StrSelect.Append(titleAttriute)
                StrSelect.Append(" is not null and I")
                StrSelect.Append(startAttribute)
                StrSelect.Append(" is not null and I")
                StrSelect.Append(endAttribute)
                StrSelect.Append(" is not null ")

                If (filterColumn <> String.Empty AndAlso filterValue <> String.Empty) Then
                    StrSelect.Append(" and ")
                    StrSelect.Append(filterColumn)
                    StrSelect.Append(" = '")
                    StrSelect.Append(filterValue)
                    StrSelect.Append("'")
                End If
                DSIndexDataLst = Server.Con.ExecuteDataset(CommandType.Text, StrSelect.ToString())
                Return DSIndexDataLst.Tables(0)

            End If
            Return New DataTable
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    Public Function GetAllEntities() As DataTable
        Dim strQuery As StringBuilder = New StringBuilder()

        strQuery.Append("SELECT distinct e.DOC_TYPE_ID id, RTRIM(e.DOC_TYPE_NAME) text FROM doc_type e")

        Dim dt As DataSet = Con.ExecuteDataset(CommandType.Text, strQuery.ToString())
        If dt IsNot Nothing Then
            Return dt.Tables(0)
        End If
    End Function



    Public Function GetIndexForEntities(indexId As Int64) As DataTable
        Dim strQuery As StringBuilder = New StringBuilder()

        strQuery.Append("select doc_index.index_id,doc_index.index_name from Index_R_Doc_Type inner join doc_index on index_r_doc_type.index_id = doc_index.index_id inner join doc_type on doc_type.doc_type_id = index_r_doc_type.doc_type_id and doc_type.doc_type_id=" & indexId)

        Dim dt As DataSet = Con.ExecuteDataset(CommandType.Text, strQuery.ToString())
        If dt IsNot Nothing Then
            Return dt.Tables(0)
        End If
    End Function



    Public Function UpdateGuiaDespacho(ByVal guia As String, ByVal codigoAfip As Int32, descripcionAfip As String) As DataTable
        Dim strQuery As StringBuilder = New StringBuilder()

        strQuery.Append("update DOC_I139081 set I139610 = " & codigoAfip & ", I139611 = " & descripcionAfip & "where I139557 =" & guia)

        Dim dt As DataSet = Con.ExecuteDataset(CommandType.Text, strQuery.ToString())
        If dt IsNot Nothing Then
            Return dt.Tables(0)
        End If
    End Function

    Public Function getDataFromHerarchicalParentData(parentTagValue As Int64) As DataTable
        Dim strQuery As StringBuilder = New StringBuilder()
        strQuery.Append("select CONCAT(convert(nvarchar,i26296),' - ',convert(nvarchar,i139562)) As Items  from doc_i139073 where i139600 =" & parentTagValue)
        Dim dt As DataSet = Con.ExecuteDataset(CommandType.Text, strQuery.ToString())
        If dt IsNot Nothing Then
            Return dt.Tables(0)
        Else
            Return New DataTable()
        End If
    End Function

    Public Function getInsertAdInfoInZamba(TagValue As String, UserId As Int64, PropertyId As Int64, eId As Int64) As Boolean
        Dim strQuery As New System.Text.StringBuilder
        Dim sqlBuilder As New System.Text.StringBuilder
        Try
            strQuery.Append("select * from zud where userid = " + UserId.ToString() + " and datatypeid = " + PropertyId.ToString() + " ")

            Dim dt As DataSet = Con.ExecuteDataset(CommandType.Text, strQuery.ToString())
            If dt.Tables(0).Rows.Count > 0 Then
                sqlBuilder.Append("UPDATE zud ")
                sqlBuilder.Append("SET VALUE= '" + TagValue + "' ")
                sqlBuilder.Append("WHERE ")
                sqlBuilder.Append("USERID = " + UserId.ToString() + " AND DATATYPEID = " + PropertyId.ToString() + "")
            Else

                sqlBuilder.Append("INSERT INTO zud ")
                sqlBuilder.Append("(ID,VALUE,USERID,DATATYPEID) ")
                sqlBuilder.Append("VALUES ")
                sqlBuilder.Append("(" + eId.ToString() + ",'" + TagValue + "'," + UserId.ToString() + "," + PropertyId.ToString() + ")")

            End If

            Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())

            Return True
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return False
        End Try

    End Function


    Public Function getZudt() As DataTable
        Dim strQuery As StringBuilder = New StringBuilder()
        Try
            strQuery.Append("select * from zudt")
            Dim dt As DataSet = Con.ExecuteDataset(CommandType.Text, strQuery.ToString())
            If dt IsNot Nothing Then
                Return dt.Tables(0)
            Else
                Return New DataTable()
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    Public Function getHeralchicalTagData(parentTagValue As Int64, indexs As List(Of String), tableId As String, isView As Boolean) As DataTable
        Dim strQuery As StringBuilder = New StringBuilder()
        Dim selectStatement As String
        Dim table As String
        If isView = False Then
            table = MakeTable(Integer.Parse(tableId), Core.TableType.Indexs)
        Else
            table = tableId
        End If

        selectStatement = "select "

        If Server.isOracle Then
            For Each index As String In indexs
                If isView = True Then
                    selectStatement += "CAST(" + index + " AS VARCHAR(25))|| '-' ||"
                Else
                    selectStatement += "CAST(i" + index + " AS VARCHAR(25))|| '-' ||"
                End If
            Next
        End If

        If Server.isSQLServer Then
            selectStatement = "CONCAT("
            For Each index As String In indexs
                If isView = True Then
                    selectStatement += " convert(nvarchar," & index & "),'-',"
                Else
                    selectStatement += " convert(nvarchar,i" & index & "),'-',"
                End If
            Next
        End If


        strQuery.Append(selectStatement.Substring(0, selectStatement.LastIndexOf(")") + 1))

        If Server.isSQLServer Then
            strQuery.Append(") ")
        End If

        strQuery.Append(" As Item from ")
        strQuery.Append(table)
        If isView Then
            strQuery.Append(" where ")
        Else
            strQuery.Append(" where i")
        End If
        strQuery.Append(indexs(0))
        strQuery.Append("=")
        strQuery.Append(parentTagValue)

        Dim dt As DataSet = Con.ExecuteDataset(CommandType.Text, strQuery.ToString())
        If dt IsNot Nothing Then
            Return dt.Tables(0)
        Else
            Return New DataTable()
        End If
    End Function


    Public Shared Sub DeleteValuesZsearchValue_DT(resultId As Integer)
        'Se elimina las palabras indexadas para volver a indexar
        Dim deleteSentence As String = "DELETE FROM ZSearchValues_DT WHERE ResultId=" + resultId.ToString
        Server.Con.ExecuteNonQuery(CommandType.Text, deleteSentence)
    End Sub

    ''' <summary>
    ''' Inserta los ultimos resultados obtenidos de la dosearch.
    ''' </summary>
    ''' <returns></returns>
    Public Function InsertDoSearchResults(ByVal data As String, ByVal UserId As Int64) As Object
        Dim sqlBuilder As New System.Text.StringBuilder
        Try
            'sqlBuilder.Append("DELETE FROM ZDoSearchResults ")
            'sqlBuilder.Append("WHERE UserId = " + UserId.ToString())

            sqlBuilder.Append("INSERT INTO ZDoSearchResults ")
            sqlBuilder.Append("([UserId], [ExpirationDate], [ResultsData]) ")
            sqlBuilder.Append("VALUES ")
            sqlBuilder.Append("(" + UserId.ToString() + ", DATEADD(hour, 24, GETDATE()) , " + data + ")")

            Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    ''' <summary>
    ''' Inserta los ultimos filtros aplicados.
    ''' </summary>
    ''' <returns></returns>
    Public Function InsertFilterSettings(ByVal ResultsFilters As String, ByVal SearchObject As String, ByVal mode As String, ByVal UserId As Int64) As Object
        Dim sqlBuilder As New System.Text.StringBuilder

        Try
            sqlBuilder.Append("INSERT INTO ZDoSearchResults ")
            sqlBuilder.Append("([UserId], [Mode], [ExpirationDate], [ResultsFilters], [SearchObject]) ")
            sqlBuilder.Append("VALUES ")
            sqlBuilder.Append("(" + UserId.ToString() + ", " + mode + ", DATEADD(hour, 24, GETDATE()), " + ResultsFilters + "," + SearchObject + ")")

            Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    ''' <summary>
    ''' Inserta los ultimos resultados obtenidos de la dosearch.
    ''' </summary>
    ''' <returns></returns>
    Public Function UpdateDoSearchResults(ByVal data As String, ByVal UserId As Int64) As Object
        Dim sqlBuilder As New System.Text.StringBuilder
        Try
            sqlBuilder.Append("UPDATE ZDoSearchResults ")

            sqlBuilder.Append("SET")
            sqlBuilder.Append("[ExpirationDate] = DATEADD(hour, 24, GETDATE())" + ", ")
            sqlBuilder.Append("[ResultsData] = " + data)

            sqlBuilder.Append("WHERE UserId = " + UserId.ToString())

            Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    ''' <summary>
    ''' Inserta o actualiza el ultimo objeto search resultado del DoSearch.
    ''' </summary>
    ''' <returns></returns>
    Public Function InsertOrUpdateDoSearchResults(ByVal SearchObject As String, ByVal Mode As String, ByVal UserId As Int64, ByVal ExpirationDate As DateTime) As Object
        Dim sqlBuilder As New System.Text.StringBuilder
        Dim count As Int64
        Try
            If Server.isSQLServer Then
                sqlBuilder.Append("if exists (SELECT UserId FROM [ZDoSearchResults] where UserId = " + UserId.ToString() + " AND Mode = '" + Mode + "') ")
                sqlBuilder.Append("UPDATE [ZDoSearchResults] ")
                sqlBuilder.Append("Set ")
                sqlBuilder.Append("[ExpirationDate] = CAST('" + ExpirationDate.ToString("yyyyMMdd HH:mm:ss") + "' AS DATETIME), ")
                sqlBuilder.Append("[SearchObject] = " + SearchObject + ", ")
                sqlBuilder.Append("[Mode] = '" + Mode + "' ")
                sqlBuilder.Append("WHERE UserId = " + UserId.ToString() + " AND Mode = '" + Mode + "'")
                sqlBuilder.Append(" else ")
                sqlBuilder.Append("INSERT INTO [ZDoSearchResults] ")
                sqlBuilder.Append("([UserId], [ExpirationDate], [SearchObject], [Mode]) ")
                sqlBuilder.Append("VALUES ")
                sqlBuilder.Append("(" + UserId.ToString() + ", CAST('" + ExpirationDate.ToString("yyyyMMdd HH:mm:ss") + "' AS DATETIME), " + SearchObject + ", '" + Mode + "')")
                Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
            Else
                SearchObject = Replace(SearchObject, "0x", "")
                Dim SplitFile() As String = SplitFileClob(SearchObject)
                sqlBuilder.Append("DECLARE ")
                sqlBuilder.Append("FileClob CLOB;")
                sqlBuilder.AppendLine("BEGIN ")
                For Each PartialFile As String In SplitFile
                    sqlBuilder.AppendLine("FileClob := FileClob || '" & PartialFile & "';")
                Next
                count = Zamba.Servers.Server.Con().ExecuteScalar(CommandType.Text, $"SELECT count(1)  FROM ZDoSearchResults where UserId = {UserId.ToString()} AND MODESEARCH = '{Mode}'")
                If count = 0 Then
                    sqlBuilder.AppendLine("INSERT INTO ZDoSearchResults")
                    sqlBuilder.AppendLine("(USERID,ExpirationDate,SearchObject,MODESEARCH)")
                    sqlBuilder.AppendLine("values(")
                    sqlBuilder.Append(UserId.ToString())
                    sqlBuilder.Append(",TO_DATE('" & ExpirationDate.ToString("yyyy/MM/dd HH:mm:ss") & "', 'yyyy/mm/dd hh24:mi:ss')")
                    sqlBuilder.Append(",FileClob")
                    sqlBuilder.Append(",'" & Mode & "')")
                Else
                    sqlBuilder.AppendLine("update ZDoSearchResults set ")
                    sqlBuilder.AppendLine("MODESEARCH='" & Mode & "',")
                    sqlBuilder.AppendLine("SEARCHOBJECT=FileClob")
                    sqlBuilder.AppendLine("WHERE USERID=" & UserId.ToString())
                End If
                sqlBuilder.AppendLine(";")
                sqlBuilder.AppendLine("END;")
                Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
            End If
        Catch ex As Exception

            ZClass.raiseerror(ex)
            Throw
        End Try
    End Function

    Public Function InsertOrUpdateLastSearchResults(ByVal SearchObject As String, ByVal Mode As String, ByVal UserId As Int64, ByVal ExpirationDate As DateTime, name As String) As Object
        Dim count As Int64
        If Server.isSQLServer Then
            count = Zamba.Servers.Server.Con().ExecuteScalar(CommandType.Text, $"SELECT count(1)  FROM ZLSR  WITH(NOLOCK)  where UserId = {UserId.ToString()} AND Mode = '{Mode}' and name = '{name}' ")
            If count = 0 Then
                Dim sqlBuilder As New System.Text.StringBuilder
                sqlBuilder.Append("INSERT INTO [ZLSR] ")
                sqlBuilder.Append("([UserId], [SearchDate], [SearchObject], [Mode], Name) ")
                sqlBuilder.Append("VALUES ")
                sqlBuilder.Append("(" & UserId.ToString() & ", CAST('" & ExpirationDate.ToString("yyyyMMdd HH:mm:ss") & "' AS DATETIME), " & SearchObject & ", '" & Mode & "', '" & name & "')")
                Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
            Else
                Zamba.Servers.Server.Con().ExecuteScalar(CommandType.Text, $"update ZLSR set SearchDate = '{ExpirationDate.ToString("yyyyMMdd HH:mm:ss")}' where UserId = {UserId.ToString()} AND Mode = '{Mode}'  and name = '{name}' ")
            End If
        Else
            count = Zamba.Servers.Server.Con().ExecuteScalar(CommandType.Text, $"SELECT count(1)  FROM ZLSR where UserId = {UserId.ToString()} AND MODESEARCH = '{Mode}' and name = '{name}' ")
            Dim sqlBuilder As New System.Text.StringBuilder
            Dim MaxLength As Int64 = 4000
            SearchObject = Replace(SearchObject, "0x", "")
            Dim SplitFile() As String = SplitFileClob(SearchObject)
            sqlBuilder.Append("DECLARE ")
            sqlBuilder.Append("FileClob CLOB;")
            sqlBuilder.AppendLine("BEGIN ")
            For Each PartialFile As String In SplitFile
                sqlBuilder.AppendLine("FileClob := FileClob || '" & PartialFile & "';")
            Next
            If count = 0 Then
                sqlBuilder.AppendLine("INSERT INTO ZLSR")
                sqlBuilder.AppendLine("(USERID,SEARCHDATE,SEARCHOBJECT,MODESEARCH,NAME)")
                sqlBuilder.AppendLine("values(")
                sqlBuilder.AppendLine(UserId.ToString())
                sqlBuilder.AppendLine(",TO_DATE('" & ExpirationDate.ToString("yyyy/MM/dd HH:mm:ss") & "', 'yyyy/mm/dd hh24:mi:ss')")
                sqlBuilder.AppendLine(",FileClob")
                sqlBuilder.AppendLine(",'" & Mode & "'")
                sqlBuilder.AppendLine(",'" & name & "')")
            Else
                sqlBuilder.AppendLine("update ZLSR set ")
                sqlBuilder.AppendLine("MODESEARCH='" & Mode & "',")
                sqlBuilder.AppendLine("SEARCHDATE=TO_DATE('" & ExpirationDate.ToString("yyyy/MM/dd HH:mm:ss") & "', 'yyyy/mm/dd hh24:mi:ss'),")
                sqlBuilder.AppendLine("SEARCHOBJECT=FileClob")
                sqlBuilder.AppendLine("NAME='" & name & "'")
                sqlBuilder.AppendLine("WHERE USERID=" & UserId.ToString())
            End If
            sqlBuilder.AppendLine(";")
            sqlBuilder.AppendLine("END;")
            Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
        End If
    End Function
    Private Function SplitFileClob(FileContent As String) As Array
        Dim MaxLength As Int64 = 4000
        Dim SplitFile() As String = 'Separa el archivo en partes iguales de 4000 caracteres teniendo en cuenta si el ultimo mide menos inclusive
                New String("*", FileContent.Length / MaxLength - 1).
                Select(Function(n, index) FileContent.
                Substring(index * MaxLength,
                          IIf(FileContent.Length - index * MaxLength < MaxLength,
                              FileContent.Length - index * MaxLength,
                              MaxLength)
                          )).ToArray
        Return SplitFile
    End Function

    ''' <summary>
    ''' Obtiene el ultimo objeto search resultado del DoSearch.
    ''' </summary>
    ''' <returns></returns>
    Public Function SelectDoSearchResults(ByVal UserId As Int64, ByVal Mode As String) As DataTable
        Dim sqlBuilder As New System.Text.StringBuilder
        Dim DT_Result As DataTable

        Try
            If Server.isSQLServer Then
                sqlBuilder.Append("SELECT * FROM ZDoSearchResults ")
                sqlBuilder.Append("WHERE UserId = " & UserId.ToString() & " And Mode = '" & Mode & "'")
            Else
                sqlBuilder.Append("SELECT ")
                sqlBuilder.Append("USERID")
                sqlBuilder.Append(",MODESEARCH AS ""MODE""")
                sqlBuilder.Append(",SEARCHOBJECT")
                sqlBuilder.Append(",EXPIRATIONDATE")
                sqlBuilder.Append(" FROM ZDoSearchResults")
                sqlBuilder.Append(" WHERE UserId = " & UserId.ToString() & " And ModeSearch = '" & Mode & "'")
            End If
            DT_Result = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString()).Tables(0)
            Return DT_Result
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Throw
        End Try
    End Function

    Public Function SelectLastSearchResults(ByVal UserId As Int64) As DataTable
        Dim sqlBuilder As New System.Text.StringBuilder
        Dim DT_Result As DataTable

        Try
            If Server.isSQLServer Then
                sqlBuilder.Append("SELECT top(20) * FROM ZLSR  WITH(NOLOCK) ")
            Else
                sqlBuilder.Append("SELECT * FROM ZLSR ")
            End If
            sqlBuilder.Append("WHERE UserId = " & UserId.ToString())
            If Server.isOracle Then
                sqlBuilder.Append(" and rownum <= 20 ")
            End If
            sqlBuilder.Append("  order by searchdate desc ")

            DT_Result = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString()).Tables(0)
            Return DT_Result
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Throw
        End Try
    End Function
    Public Function DeleteDoSearchResults(userId As Long, Mode As String) As Object
        Dim sqlBuilder As New System.Text.StringBuilder
        Dim DT_Result As DataTable

        Try
            If Server.isSQLServer Then
                sqlBuilder.Append("DELETE FROM ZDoSearchResults ")
                sqlBuilder.Append("WHERE UserId = " & userId.ToString() & " AND Mode = '" & Mode & "'")
            Else
                sqlBuilder.Append("DELETE FROM ZDoSearchResults ")
                sqlBuilder.Append("WHERE UserId = " & userId.ToString() & " AND MODESEARCH = '" & Mode & "'")
            End If

            DT_Result = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString()).Tables(0)
            Return DT_Result
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Throw
        End Try
    End Function
End Class