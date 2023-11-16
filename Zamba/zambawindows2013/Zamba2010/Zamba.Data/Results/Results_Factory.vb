Imports Zamba.Core
Imports Zamba.Servers.Server
Imports System.Text
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports Zamba.Searchs
Imports Zamba.Core.Searchs

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
            'Case Results_Factory.TableType.Full
            '    TableName = "Doc" & docTypeId.ToString
            Case Results_Factory.TableType.Document
                TableName = "Doc_T" & docTypeId.ToString
            Case Results_Factory.TableType.Indexs
                TableName = "Doc_I" & docTypeId.ToString
            Case Results_Factory.TableType.Blob
                TableName = "DOC_B" & docTypeId.ToString
        End Select

        Return TableName
    End Function

    Public Shared Function MakefileName(Optional ByRef t As Transaction = Nothing) As String
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

            Dim query As String
            If Ds.Tables(0).Rows.Count > 0 Then
                query = "UPDATE LSTFNAME SET LASTNAME = '" & Name & "' WHERE LASTNAME = '" & LastName & "'"
            Else
                query = "INSERT INTO LSTFNAME (LASTNAME) VALUES ('" & Name & "')"
            End If

            If t Is Nothing Then
                Server.Con.ExecuteNonQuery(CommandType.Text, query)
            Else
                t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, query)
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

    Public Shared Function GetDocuments(ByVal DocTypeId As Integer) As DsResults
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

    Public Shared Function GetPublishDate(docid As Long) As Object
        Dim query As String = String.Format("select publishdate from Z_Publish where docid = {0}", docid)
        Return Server.Con.ExecuteScalar(CommandType.Text, query)
    End Function

    Public Shared Sub UpdateFileIcon(resultId As Long, docTypeId As Long, iconID As Integer)
        Dim query As String = String.Format("update DOC_T{0} set ICON_ID = {1} where DOC_ID = {2}", docTypeId, iconID, resultId)
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

    ''' <summary>
    ''' Devuelve el valor 
    ''' </summary>
    ''' <param name="docid"></param>
    ''' <returns></returns>
    Public Shared Function GetVersionValue(docTypeId As Long, docid As Long) As Integer
        Dim query As String = String.Format("select VERSION from doc_t{0} where DOC_ID = {1}", docTypeId, docid)
        Return Server.Con.ExecuteScalar(CommandType.Text, query)
    End Function

    Public Shared Function GetUserPublisher(docid As Long) As Object
        Dim query As String = String.Format("select u.NAME from Z_Publish p join USRTABLE u on p.USERID = u.ID where DOCID = {0}", docid)
        Return Server.Con.ExecuteScalar(CommandType.Text, query)
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
    Public Shared Function getResultsData(ByVal docTypeId As Int64, ByVal indexId As Int32, ByVal genIndex As List(Of ArrayList), Optional ByVal comparateValue As String = "", Optional ByVal searchValue As Boolean = True, Optional ByVal strRestricc As String = "") As DataSet
        Dim strSql As StringBuilder = New StringBuilder()
        strSql.Append("select name as ")
        strSql.Append(Chr(34))
        strSql.Append("Nombre del Documento")
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
        'strSql.Append("Fecha Creaci�n del Documento")
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

    'Martin: Se comenta, no se encontro quien lo usa, si alguien lo usaba se debe modificar el metodo para que tome de la coleccion de atributos el dropdown, no que lo busque de la base
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un Count con la cantidad de results, Utilizado para Fijar el limite del paginado 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Diego]	19/02/2008	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetResults(ByVal DocTypeId As Integer) As DataTable
        Return Server.Con.ExecuteDataset(CommandType.Text, "SELECT * FROM DOC_T" & DocTypeId.ToString).Tables(0)
    End Function

    ''' <summary>
    ''' Obtiene los ids de los documentos a actualizar
    ''' </summary>
    ''' <param name="DocTypeId"></param>
    ''' <param name="days"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAutoNameResults(ByVal DocTypeId As Integer, ByVal days As Int32) As DataTable
        If days > 0 Then
            If Server.isOracle Then
                Return Server.Con.ExecuteDataset(CommandType.Text, "SELECT doc_id FROM DOC_I" & DocTypeId.ToString & " where lupdate > sysdate - " & days).Tables(0)
            Else
                Return Server.Con.ExecuteDataset(CommandType.Text, "SELECT doc_id FROM DOC_I" & DocTypeId.ToString & " where lupdate > getdate() - " & days).Tables(0)
            End If
        Else
            Return Server.Con.ExecuteDataset(CommandType.Text, "SELECT doc_id FROM DOC_T" & DocTypeId.ToString).Tables(0)
        End If
    End Function

    Public Shared Function ValidateNewResult(ByVal DocTypeId As Integer, ByVal Doc_ID As Integer) As Boolean
        Dim StrQuery As String = "SELECT * FROM DOC_T" & DocTypeId.ToString & " WHERE Doc_ID = " & Doc_ID
        Dim ds As DataTable = Server.Con.ExecuteDataset(CommandType.Text, StrQuery).Tables(0)

        If ds.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function GetResultsFromDoc_I(ByVal DocTypeId As Integer) As DataTable
        Return Server.Con.ExecuteDataset(CommandType.Text, "SELECT * FROM DOC_I" & DocTypeId.ToString).Tables(0)
    End Function


    Public Shared Function LoadFileFromDB(ByVal DocId As Long, ByVal DocTypeId As Long) As Byte()
        Dim sql As String = "SELECT DOCFILE FROM DOC_B" & DocTypeId.ToString & " WHERE DOC_ID = " & DocId.ToString
        Dim File As Byte()
        Try
            File = DirectCast(Server.Con.ExecuteScalar(CommandType.Text, sql), Byte())
        Catch ex As SqlException
            'Si tira timeout la consulta, utiliza otra preparada para estos casos.
            If ex.Message.Contains("Valor de tiempo de espera caducado") Then
                If DialogResult.Yes = MessageBox.Show("El archivo que va a abrir puede tardar varios minutos en abrirse por ser muy pesado. �Desea continuar?", "Atenci�n",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) Then
                    File = DirectCast(Server.Con.ExecuteScalarForMigrator(CommandType.Text, sql), Byte())
                End If
            End If
        Catch ex As Exception
            If ex.Message.ToLower.Contains("la tabla o vista no existe") Then
                Return Nothing
            End If
            Throw ex
        End Try

        Return File
    End Function

    Public Shared Function GetIfFileIsZipped(ByVal DocId As Long, ByVal DocTypeId As Long) As Boolean
        Try
            Dim sql As String = "SELECT ZIPPED FROM DOC_B" & DocTypeId.ToString & " WHERE DOC_ID = " & DocId.ToString

            If Int32.Parse(Server.Con.ExecuteScalar(CommandType.Text, sql)) = 1 Then
                Return True
            End If

            Return False
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return False
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

    Public Shared Function getResultsAndPageQueryResults(ByVal PageId As Int16, ByVal PageSize As Int16, ByVal docTypeId As Int64, ByVal indexId As Int64, ByVal genIndex As List(Of ArrayList), Optional ByVal RestrictionAndSortExpression As String = "", Optional ByVal SymbolToReplace As String = "", Optional ByVal BySimbolReplace As String = "", Optional ByRef resultCount As Integer = 0) As DataTable

        Dim TableJoins As List(Of String)
        Dim strSql As StringBuilder = New StringBuilder()
        Dim Joinstr As StringBuilder

        strSql.Append("select name as ")
        strSql.Append(Chr(34))
        strSql.Append("Nombre del Documento")
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
                Dim indxType As Long = Indexs_Factory.GetIndexDropDownType(Int64.Parse(index(0).ToString))
                If indxType = IndexAdditionalType.AutoSustituci�n Or indxType = IndexAdditionalType.AutoSustituci�nJerarquico Then
                    strSql.Append(", SLST_S")
                    strSql.Append(index(0).ToString())
                    strSql.Append(".DESCRIPCION")
                    If IsNothing(TableJoins) Then
                        Joinstr = New StringBuilder()
                        TableJoins = New List(Of String)
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
        Dim con As IConnection = Nothing
        Dim dt As New DataTable
        Dim dc As DataColumn
        Dim count As Int32 = 0

        Try
            con = Server.Con(False)
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
                reader.Close()
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
    Public Shared Sub AddDocTypeToWF(ByVal docTypeId As Int64, ByVal WfID As Int32)
        ' Dim sql As String
        'MAXI 14/11/05
        ' Dim initialstep As Int32 = GetInitialStep(WfID)
        'sql = "Update doc_type set Life_Cycle=" & WfID & " where doc_type_id=" & DocTypeID
        'Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        If Server.isOracle Then
            Dim parValues() As Object = {WfID, docTypeId}
            Server.Con.ExecuteNonQuery("ZWfUpdDt_Pkg.ZWfUpdDtLCByDtId", parValues)
        Else
            Dim parValues() As Object = {WfID, docTypeId}
            Try
                Server.Con.ExecuteNonQuery("ZWfUpdDtLCByDtId", parValues)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End If
    End Sub
    Public Shared Sub RemoveDocTypeWF(ByVal docTypeId As Int64)
        'MAXI 14/11/05
        'Dim sql As String = "Update doc_type set Life_Cycle=0 where doc_type_id=" & DocTypeID
        'Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        If Server.isOracle Then
            ''Dim parNames() As String = {"WfId", "DocTypeID"}
            'Dim parTypes() As Object = {13, 13}
            Dim parValues() As Object = {0, docTypeId}
            Server.Con.ExecuteNonQuery("ZWfUpdDt_Pkg.ZWfUpdDtLCByDtId", parValues)
        Else
            Dim parValues() As Object = {0, docTypeId}
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
    Public Shared Function IsDocTypeInWF(ByVal docTypeId As Int64) As Boolean
        If IsDBNull(Server.Con.ExecuteScalar(CommandType.Text, "Select Life_Cycle from doc_type where Doc_type_id=" & docTypeId)) OrElse Server.Con.ExecuteScalar(CommandType.Text, "Select Life_Cycle from doc_type where Doc_type_id=" & docTypeId) = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

#End Region

#Region "Indexs"

    Public Shared Sub CompleteIndexData(ByVal docTypeId As Int64, ByVal docId As Int64, ByRef indexs As ArrayList)
        Dim Dr As IDataReader = Nothing
        Dim con As IConnection = Nothing
        Dim TableIndex As String = Results_Factory.MakeTable(docTypeId, Results_Factory.TableType.Indexs)
        Dim StrBuilder As New StringBuilder()

        Try
            StrBuilder.Append("Select Doc_id ")

            Dim f As Int16
            For f = 0 To indexs.Count - 1
                StrBuilder.Append(", ")
                StrBuilder.Append(TableIndex.ToString())
                StrBuilder.Append(".I")
                StrBuilder.Append(DirectCast(indexs(f), Index).ID)
            Next


            StrBuilder.Append(" from ")
            StrBuilder.Append(TableIndex.ToString())
            StrBuilder.Append(" where doc_Id = ")
            StrBuilder.Append(docId.ToString())

            con = Server.Con(False)
            Dr = con.ExecuteReader(CommandType.Text, StrBuilder.ToString)

            If Dr.Read() Then
                Dim i As Int32
                For i = 0 To indexs.Count - 1
                    Try
                        If Not IsDBNull(Dr.GetValue(i + 1)) Then
                            DirectCast(indexs(f), Index).Data = Dr.GetValue(i + 1)
                        End If
                    Catch
                        DirectCast(indexs(f), Index).Data = String.Empty
                    End Try
                Next
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            If Not IsNothing(Dr) Then
                Dr.Close()
                Dr.Dispose()
                Dr = Nothing
            End If
            If Not IsNothing(con) Then
                con.Close()
                con.dispose()
                con = Nothing
            End If
            TableIndex = Nothing
            StrBuilder.Remove(0, StrBuilder.Length)
            StrBuilder = Nothing
        End Try
    End Sub
    ''' <summary>
    ''' carga la informacion de los atributos del result 
    ''' </summary>
    ''' <param name="result"></param>
    ''' <remarks></remarks>
    <Obsolete("Metodo discontinuado", False)>
    Public Shared Function CompleteIndexDataDr(ByVal _ResultId As Int64, ByVal DocTypeId As Int64, ByVal Indexs As List(Of IIndex), ByVal con As IConnection, Optional ByVal inThread As Boolean = False) As IDataReader
        Dim Dr As IDataReader = Nothing
        Dim strselect2 As New StringBuilder
        Dim TableIndex As String = Results_Factory.MakeTable(DocTypeId, Results_Factory.TableType.Indexs)

        strselect2.Append("Select crdate")
        Dim f As Int16
        For f = 0 To Indexs.Count - 1
            strselect2.Append(", " & TableIndex & ".I" & DirectCast(Indexs(f), Index).ID)
        Next
        strselect2.Append(" from " & TableIndex & " where doc_Id = " & _ResultId)

        Dr = con.ExecuteReader(CommandType.Text, strselect2.ToString)
        Return Dr
    End Function
    '-------------------------------------------


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Persiste los atributos de un documento
    ''' </summary>
    ''' <param name="Result">Documento con atributos a persistir</param>
    ''' <param name="ReindexFlag">True para Actualizar,False para Agregar</param>
    ''' <param name="changeEvent">True para disparar el evento</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[oscar]	07/06/2006	Created
    ''' 	[Gaston]    18/05/2009	Modified    Se comento una validaci�n para hacer posible la inserci�n de atributos con datos vac�os
    ''' </history>
    ''' -----------------------------------------------------------------------------
    <Obsolete("Metodo discontinuado", False)>
    Public Shared Sub SaveModifiedIndexData(ByRef _result As IResult, ByVal ReindexFlag As Boolean, Optional ByVal OnlySpecifiedIndexsids As Generic.List(Of Int64) = Nothing)
        Try

            Dim Table As String = MakeTable(_result.DocType.ID, Results_Factory.TableType.Indexs)
            Dim i As Integer

            If ReindexFlag = False Then

                Dim Columns As String = "DOC_ID"
                Dim Values As String = _result.ID

                For i = 0 To _result.Indexs.Count - 1

                    If Not IsNothing(OnlySpecifiedIndexsids) Then
                        For Each SpecifiedIndex As Int32 In OnlySpecifiedIndexsids
                            If SpecifiedIndex = DirectCast(_result.Indexs(i), Index).ID Then
                                SaveIndexDataNoReindex(_result, i, Columns, Values)
                            End If
                        Next
                    Else
                        SaveIndexDataNoReindex(_result, i, Columns, Values)
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

                    ' Se comento el c�digo porque sino es imposible insertar un Atributo con valor vac�o
                    'If String.IsNullOrEmpty(DirectCast(_Result.Indexs(i), Zamba.Core.Index).DataTemp) = False Then

                    If Not IsNothing(OnlySpecifiedIndexsids) Then
                        If OnlySpecifiedIndexsids.Contains(DirectCast(_result.Indexs(i), Index).ID) Then
                            SaveIndexDataAndReindex(_result, i, strset)
                        End If
                    Else
                        SaveIndexDataAndReindex(_result, i, strset)
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

    ''' <summary>
    ''' Se indexan los atributos asincronicamente
    ''' </summary>
    ''' <param name="o"></param>
    ''' <remarks></remarks>


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Update the specified records of ZSearchValues_DT table.
    ''' </summary>
    ''' <param name="ResultId">Int64</param>
    ''' <param name="i">Int32</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tom�s]     18/03/09    Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    '[Ezequiel] 23/09/09 - Se comecto el metodo ya que cambio el indexado.
    'Public Shared Sub UpdateSearchIndexData(ByVal res As Result, ByVal i As Integer)
    '    ' Elimina todos los registros de un Atributo y result determinado. 
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
    ''' 	[Tom�s]     18/03/09    Created
    '''     [Tomas]     19/03/09    Modified    Se agrega la opci�n para Oracle
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub DeleteSearchIndexData(ByVal ResultId As Int64)

        Try
            Dim parametersValues() As Object = {ResultId}

            If Server.isOracle Then
                ''Dim parNames() As String = {"varResultId"}
                'Dim parTypes() As Object = {13}
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
    ''' 	[Tom�s]     18/03/09    Created
    '''     [Ezequiel]  18/03/09    Modified - Changed parameters and Values
    '''     [Tom�s]     19/03/09    Modified    Se agrega la opci�n para Oracle
    '''     [Ezequiel]  23/09/09    Modified   Se modifico el indexado, ya no usa mas indexid.
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub InsertSearchIndexData(ByVal Word As String, ByVal DocTypeId As Int64, ByVal ResultId As Int64, Optional ByRef t As Transaction = Nothing)
        If Not String.IsNullOrEmpty(Word) Then
            Dim parametersValues() As Object = {Word.ToLower, DocTypeId, ResultId}

            If Server.isOracle Then

                If t Is Nothing Then
                    Server.Con.ExecuteNonQuery("zsp_search_300.WordInsert", parametersValues)
                Else
                    t.Con.ExecuteNonQuery(t.Transaction, "zsp_search_300.WordInsert", parametersValues)
                End If

            Else
                If t Is Nothing Then
                    Server.Con.ExecuteNonQuery("zsp_search_300_WordInsert", parametersValues)
                Else
                    t.Con.ExecuteNonQuery(t.Transaction, "zsp_search_300_WordInsert", parametersValues)
                End If
            End If
        End If
    End Sub

    Private Shared Sub SaveIndexDataNoReindex(ByRef Result As IResult, ByVal i As Integer, ByRef Columns As String, ByRef Values As String)
        Dim _index As Index = DirectCast(Result.Indexs(i), Index)

        'Si el atributo es referencial no se utiliza
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
        End If
        _index = Nothing
    End Sub
    ''' <summary>
    ''' Arma la consulta para actualizar los atributos
    ''' </summary>
    ''' <param name="Result"></param>
    ''' <param name="i"></param>
    ''' <param name="strset"></param>
    ''' <history>   Marcelo Modified 01/09/2009</history>
    ''' <remarks></remarks>
    Private Shared Sub SaveIndexDataAndReindex(ByRef _Result As Result, ByVal i As Integer, ByVal strset As StringBuilder)
        Dim _index As Index = DirectCast(_Result.Indexs(i), Index)

        'Si el atributo es referencial no se utiliza
        If Not _index.isReference Then
            If Not IsNothing(_index.DataTemp) Then
                Select Case CInt(_index.Type)
                    Case IndexDataType.Fecha
                        If Not String.IsNullOrEmpty(_index.DataTemp.Trim) Then
                            strset.Append(",I" & _index.ID & " = " & Server.Con.ConvertDate(_index.DataTemp))
                        Else
                            strset.Append(",I" & _index.ID & " = null")
                        End If
                    Case IndexDataType.Fecha_Hora
                        If Not String.IsNullOrEmpty(_index.DataTemp.Trim) Then
                            strset.Append(",I" & _index.ID & " = " & Server.Con.ConvertDateTime(_index.DataTemp))
                        Else
                            strset.Append(",I" & _index.ID & " = null")
                        End If
                    Case IndexDataType.Alfanumerico, IndexDataType.Alfanumerico_Largo
                        If Not String.IsNullOrEmpty(_index.DataTemp.Trim) Then
                            If _index.DataTemp.Contains("'") Then
                                _index.DataTemp = _index.DataTemp.Replace("'", String.Empty)
                            End If
                            strset.Append(",I" & _index.ID & " = '" & _index.DataTemp & "'")
                        Else
                            strset.Append(",I" & _index.ID & " = null")
                        End If
                    Case IndexDataType.Numerico, IndexDataType.Numerico_Largo, IndexDataType.Numerico_Decimales, IndexDataType.Moneda, IndexDataType.Si_No
                        If Not String.IsNullOrEmpty(_index.DataTemp.Trim) Then
                            strset.Append(",I" & _index.ID & " = " & Replace(_index.DataTemp, ",", "."))
                        Else
                            strset.Append(",I" & _index.ID & " = null")
                        End If
                    Case Else
                        If Not String.IsNullOrEmpty(_index.DataTemp.Trim) Then
                            strset.Append(",I" & _index.ID & " = " & Replace(_index.DataTemp, ",", "."))
                        Else
                            strset.Append(",I" & _index.ID & " = " & Replace(_index.DataTemp, ",", "."))

                        End If
                End Select
            End If
        End If
        _index = Nothing
    End Sub

    Public Shared Sub InsertIndexerState(ByVal DocType As Long, ByVal Id As Long, ByVal state As Integer, Optional ByVal t As Transaction = Nothing)
        Dim query As String
        If Server.isOracle Then
            query = "SELECT COUNT(1) FROM ZINDEXERSTATE WHERE DOCTYPE=" & DocType & " AND DOCID=" & Id
            Dim count As Int32 = Server.Con.ExecuteScalar(CommandType.Text, query)

            ' si la combinacion de Docid, doctype id y el state = 0 ya existen en esa tabla no hago el insert
            If count <= 0 Then

                Dim InsertIndexerTable As String = "INSERT INTO ZINDEXERSTATE VALUES (" & DocType & "," & Id & ", sysdate ," & state & ")"

                If t Is Nothing Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, InsertIndexerTable)
                Else
                    t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, InsertIndexerTable)
                End If
            Else
                Dim InsertIndexerTable As String = "UPDATE ZINDEXERSTATE SET STATE = 0 WHERE DOCTYPE = " & DocType & " AND DOCID = " & Id

                If t Is Nothing Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, InsertIndexerTable)
                Else
                    t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, InsertIndexerTable)
                End If

            End If
        Else
            query = "SELECT COUNT(1) FROM ZINDEXERSTATE WITH (NOLOCK) WHERE DOCTYPE=" & DocType & " AND DOCID=" & Id
            Dim count As Int32 = Server.Con.ExecuteScalar(CommandType.Text, query)

            ' si la combinacion de Docid, doctype id y el state = 0 ya existen en esa tabla no hago el insert
            If count <= 0 Then

                Dim InsertIndexerTable As String = "INSERT INTO ZINDEXERSTATE VALUES (" & DocType & "," & Id & ", getdate() ," & state & ")"

                If t Is Nothing Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, InsertIndexerTable)
                Else
                    t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, InsertIndexerTable)
                End If
            Else
                Dim InsertIndexerTable As String = "UPDATE ZINDEXERSTATE SET STATE = 0 WHERE DOCTYPE = " & DocType & " AND DOCID = " & Id

                If t Is Nothing Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, InsertIndexerTable)
                Else
                    t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, InsertIndexerTable)
                End If

            End If
        End If


    End Sub



    Public Sub RegisterDocumentWithAlfanumericIndex(ByRef Result As NewResult, ByVal reIndexFlag As Boolean, ByVal userid As Int64, Optional ByVal isVirtual As Boolean = False)
        Dim Con As IConnection = Server.Con(False)

        Dim TableName As String
        Dim InsertQuery As String
        Dim tableQuery As String

        Try
            TableName = MakeTable(Result.DocTypeId, TableType.Document)
            Dim FileLen As Decimal
            If isVirtual = False AndAlso Result.Volume.Type <> VolumeTypes.DataBase Then
                Dim ResultFileInfo As New IO.FileInfo(Result.NewFile)
                Try
                    FileLen = CDec(New IO.FileInfo(Result.NewFile).Length / 1024)
                Catch ex As Exception
                    FileLen = CDec(70 / 1024)
                    ZClass.raiseerror(ex)
                End Try
            End If

            InsertQuery = CreateInsertQuery(Result, TableName, isVirtual, FileLen, userid)
            Con.ExecuteNonQuery(CommandType.Text, InsertQuery)

            If isVirtual = False Then
                'Si el volumen es de tipo base de datos, se guarda el archivo en la DOC_B
                If Result.Volume.Type = VolumeTypes.DataBase Then
                    InsertIntoDOCB(Result, False)
                End If

                Try
                    If Server.isOracle Then
                        ''Dim parNames() As String = {"VolumeId", "FileSize"}
                        'Dim parTypes() As Object = {13, 13}
                        Dim parValues() As Object = {Result.Volume.ID, FileLen}
                        Con.ExecuteNonQuery("zsp_volume_100.UpdFilesAndSize", parValues)
                    Else
                        Dim parametersValues() As Object = {Result.Volume.ID, FileLen, Result.DocTypeId}
                        Con.ExecuteNonQuery("zsp_volume_100_UpdFilesSizeAndDocCount", parametersValues)
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try

                Result.Volume.sizelen += FileLen

            Else
                Try
                    If Server.isOracle Then
                        ''Dim parNames() As String = {"DocID", "X"}
                        'Dim parTypes() As Object = {13, 13}
                        Dim parValues() As Object = {Result.DocTypeId, 1}
                        Con.ExecuteNonQuery("zsp_doctypes_100.IncrementsDocType", parValues)
                    Else
                        Dim parametersValues() As Object = {Result.DocTypeId, 1}
                        Con.ExecuteNonQuery("zsp_doctypes_100_IncrementsDocType", parametersValues)
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End If

            TableName = MakeTable(Result.DocTypeId, TableType.Indexs)
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
            If String.Compare(ex.Message.ToString.Substring(0, 9), "ORA-00001") = 0 OrElse ex.Message.IndexOf("clave duplicada", StringComparison.CurrentCultureIgnoreCase) > 0 OrElse ex.Message.IndexOf("duplicate key", StringComparison.CurrentCultureIgnoreCase) > 0 Then
                Throw New Exception("Clave unica violada")
            End If
            Throw ex
        Finally
            Con.Close()
            Con.dispose()
            TableName = Nothing
            InsertQuery = Nothing
            tableQuery = Nothing
        End Try
    End Sub
    Public Sub RegisterDocumentWithAlfanumericIndex(ByRef Result As NewResult, ByVal reIndexFlag As Boolean, ByVal isVirtual As Boolean, ByRef t As Transaction, ByVal userId As Int64)
        Dim TableName As String
        Dim InsertQuery As String
        Dim tableQuery As String
        Dim FileLen As Decimal

        Try
            TableName = MakeTable(Result.DocTypeId, TableType.Document)

            If isVirtual = False And Result.Volume.Type <> VolumeTypes.DataBase Then
                Dim ResultFileInfo As New IO.FileInfo(Result.NewFile)
                Try
                    If ResultFileInfo.Exists Then FileLen = Math.Round(CDec(ResultFileInfo.Length / 1024))
                Catch ex As Exception
                    FileLen = CDec(70 / 1024)
                    ZClass.raiseerror(ex)
                End Try
            End If

            InsertQuery = CreateInsertQuery(Result, TableName, isVirtual, FileLen, userId)
            t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, InsertQuery)

            If isVirtual = False Then
                'Si el volumen es de tipo base de datos, se guarda el archivo en la DOC_B
                If Result.Volume.Type = VolumeTypes.DataBase Then
                    InsertIntoDOCB(Result, t, False)
                End If

                Try
                    ' If (Boolean.Parse(UserPreferences.getValueForMachine("IsBulkInsertSaveStats", Sections.Indexer, True, True)) = False) Then
                    'Actualiza la cantidad de archivos en el volumen
                    If Server.isOracle Then

                        'Dim query1 As String = "UPDATE DISK_VOLUME SET DISK_VOL_FILES = DISK_VOL_FILES + 1, DISK_VOL_SIZE_LEN = DISK_VOL_SIZE_LEN + " & FileLen & " WHERE DISK_VOL_ID = " & Result.Volume.ID
                        'Dim query2 As String = "Update Doc_Type Set DocCount = DocCount + 1 where Doc_Type_Id = " & Result.DocTypeId

                        't.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, query1)
                        't.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, query2)
                    Else
                        Dim parametersValues() As Object = {Result.Volume.ID, FileLen, Result.DocTypeId}
                        t.Con.ExecuteNonQuery(t.Transaction, "zsp_volume_100_UpdFilesSizeAndDocCount", parametersValues)
                    End If
                    ' End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try

                Result.Volume.sizelen += FileLen
            Else
                Try
                    'Actualiza la cantidad en los tipos de documento
                    If Server.isOracle Then
                        t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, String.Format("Update Doc_Type Set DocCount=DocCount + {0} where Doc_Type_Id= {1} ", Result.DocTypeId, 1))
                    Else
                        Dim parametersValues() As Object = {Result.DocTypeId, 1}
                        t.Con.ExecuteNonQuery(t.Transaction, "zsp_doctypes_100_IncrementsDocType", parametersValues)
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End If

            TableName = MakeTable(Result.DocTypeId, TableType.Indexs)
            'Dim i As Integer
            'Save Index Values
            If reIndexFlag = False Then
                tableQuery = CreateInsertQueryAlfanumerico(Result, TableName)
            Else
                tableQuery = CreateUpdateQuery(Result, TableName)
            End If

            If Not String.IsNullOrEmpty(tableQuery) Then
                t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, tableQuery)
            End If
        Catch ex As Exception
            If String.Compare(ex.Message.ToString.Substring(0, 9), "ORA-00001") = 0 OrElse ex.Message.IndexOf("clave duplicada", StringComparison.CurrentCultureIgnoreCase) > 0 Then
                Throw New Exception("Clave unica violada")
            End If
            ZClass.raiseerror(ex)
            Throw ex
        Finally
            TableName = Nothing
            InsertQuery = Nothing
            tableQuery = Nothing
        End Try
    End Sub



    <Obsolete("Metodo discontinuado", False)>
    Public Shared Sub UpdateRegisterDocument(ByRef Result As NewResult, ByVal ReindexFlag As Boolean, Optional ByVal isvirtual As Boolean = False)
        Try
            Dim Table As String = MakeTable(Result.DocTypeId, TableType.Document)

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
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "SQL: " & Strinsert.ToString)
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Update en la Doc_T")
            Con.ExecuteNonQuery(CommandType.Text, Strinsert.ToString)

            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Actualizo el tama�o")
            Try
                If isvirtual = False Then
                    If Server.isOracle Then
                        ''Dim parNames() As String = {"VolumeId", "FileSize"}
                        'Dim parTypes() As Object = {13, 13}
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Parametros del Store")
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "El Volumen es: " & Result.Volume.ID)
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "El FileLen es: " & FileLen)
                        Dim parValues() As Object = {Result.Volume.ID, FileLen}
                        Server.Con.ExecuteNonQuery("zsp_volume_100.UpdFilesAndSize", parValues)
                    Else
                        Dim parametersValues() As Object = {Result.Volume.ID, FileLen}
                        Server.Con.ExecuteNonQuery("zsp_volume_100_UpdFilesAndSize", parametersValues)
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
                        ''Dim parNames() As String = {"TamArch", "DocTypeId"}
                        'Dim parTypes() As Object = {13, 13}
                        Dim parValues() As Object = {Decimal.Parse(Decimal.Round(FileLen, 3).ToString.Replace(",", ".")), Result.DocTypeId}
                        Server.Con.ExecuteNonQuery("zsp_doctypes_100.UpdMbById", parValues)
                    Else
                        Dim parValues() As Object = {Decimal.Parse(Decimal.Round(FileLen, 3).ToString.Replace(",", ".")), Result.DocTypeId}
                        Server.Con.ExecuteNonQuery("zsp_doctypes_100_UpdMbById", parValues)
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try

                Result.Volume.sizelen += FileLen
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "actualizo DocCount en Doc_type")
            End If

            Try
                If Server.isOracle Then
                    ''Dim parNames() As String = {"DocID", "X"}
                    'Dim parTypes() As Object = {13, 13}
                    Dim parValues() As Object = {Result.DocTypeId, 1}
                    Server.Con.ExecuteNonQuery("zsp_doctypes_100.IncrementsDocType", parValues)
                Else
                    Dim parametersValues() As Object = {Result.DocTypeId, 1}
                    Server.Con.ExecuteNonQuery("zsp_doctypes_100_IncrementsDocType", parametersValues)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try


            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Salgo de RegisterDocumentData(ByRef Result As NewResult) sin excepciones")

        Catch ex As Exception
            'TODO agarrar este codigo con sqlServer
            ' T.Rollback()
            ' T.Dispose()
            If ex.Message.ToString.Substring(0, 9) = "ORA-00001" OrElse ex.Message.ToString.Substring(0, 9) = "clave duplicada" Then
                Server.Con.dispose()
                Throw New Exception("Clave unica violada")
            End If
            Throw ex
        End Try
    End Sub
    Public Shared Function GetIndexData(ByVal docTypeId As Int64, ByVal DocId As Int32) As DataSet
        Dim DSIndexDataLst As New DataSet
        Dim TableIndex As String = Results_Factory.MakeTable(docTypeId, Results_Factory.TableType.Indexs)
        'TODO Falta cambiar por Store Procedure
        Dim StrSelect As String = "Select * from " & TableIndex & " where (Doc_Id = " & DocId & ")"
        DSIndexDataLst = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        DSIndexDataLst.Tables(0).TableName = TableIndex
        Return DSIndexDataLst
    End Function
    Public Shared Function GetIndexSchema(ByVal docTypeId As Int64) As DataSet
        Dim dstemp As DataSet
        If Server.isOracle Then
            ''Dim parNames() As String = {"DocTypeId", "io_cursor"}
            'Dim parTypes() As Object = {13, 5}
            Dim parValues() As Object = {docTypeId, 2}
            'dstemp = Server.Con.ExecuteDataset("CLSDOC_GENERACIONINDICES_PKG.GeneracionIndices",  parValues)
            dstemp = Server.Con.ExecuteDataset("zsp_index_100.IndexGeneration", parValues)
            dstemp.Tables(0).TableName = "IndexLst"
        Else
            Dim parameters() As Object = {docTypeId}
            'dstemp = Server.Con.ExecuteDataset("GeneracionIndices", parameters)
            dstemp = Server.Con.ExecuteDataset("zsp_index_100_IndexGeneration", parameters)

            dstemp.Tables(0).TableName = "IndexLst"
        End If
        Return dstemp
    End Function
    <Obsolete("Metodo discontinuado", False)>
    Private Function CreateInsertQuery(ByRef Result As NewResult, ByVal tableName As String, ByVal isVirtual As Boolean, ByVal fileLen As Decimal, ByVal userid As Int64) As String
        Try
            Dim Query As New StringBuilder
            Query.Append("INSERT INTO ")
            Query.Append(tableName)
            Query.Append(" (Doc_Id, FOLDER_ID, Disk_Group_Id, Platter_Id, Vol_Id, Doc_File, Offset, Doc_type_ID, Name,ICON_ID, Shared, ver_parent_id, version, rootid, original_filename, numeroversion, Filesize) VALUES (")
            Query.Append(Result.ID)
            Query.Append(", ")
            Query.Append(0)
            Query.Append(", ")
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
            Query.Append(Result.DocTypeId)
            Query.Append(", '")
            Query.Append(Result.Name)
            Query.Append("', ")
            Query.Append(Result.IconId)
            Query.Append(", ")
            Query.Append(CInt(Result.DocType.IsShared))
            Query.Append(", ")
            Query.Append(Result.ParentVerId)
            Query.Append(", ")
            Query.Append(CInt(Result.HasVersion))
            Query.Append(", ")
            Query.Append(Result.RootDocumentId)
            Query.Append(",' ")
            'Tomas: Se agrega validaci�n en el caso de que el file este en nothing
            If Not IsNothing(Result.File) Then
                'ML: Se agrega replace de ' por error al insertar un documento cuyo nombre original tiene '
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
    Private Function CreateUpdateQuery(ByRef Result As NewResult, ByVal tableName As String) As String
        Try
            Dim Columns As New StringBuilder

            For Each CurrentIndex As Index In Result.Indexs
                If Not IsNothing(CurrentIndex) Then
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
                    CurrentIndex.Data = CurrentIndex.DataTemp
                    CurrentIndex.dataDescription = CurrentIndex.dataDescriptionTemp
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

    Public Shared Function CreateInsertQueryAlfanumericoMigrador(ByVal _Result As Result, ByVal tableName As String) As String
        Dim columns As String = "DOC_ID"
        Dim idValues As String = _Result.ID
        Const I As String = ", I"

        For Each CurrentIndex As Index In _Result.Indexs
            If Not String.IsNullOrEmpty(CurrentIndex.DataTemp) Then

                Select Case CInt(CurrentIndex.Type)

                    Case 1, 2, 3, 6 ' Es Numerico y Decimal el 6
                        If Not String.IsNullOrEmpty(CurrentIndex.DataTemp.Trim) Then
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

                            If DataLen > indexLen Then
                                CurrentIndex.DataTemp = CurrentIndex.DataTemp.Substring(0, indexLen)
                            End If

                            idValues = idValues & ",'" & CurrentIndex.DataTemp.Replace("'", "''") & "'"
                        End If
                    Case Else
                        If Not String.IsNullOrEmpty(CurrentIndex.DataTemp.Trim) Then
                            columns = columns & I & CurrentIndex.ID
                            Dim DataLen As Int32 = CurrentIndex.DataTemp.Trim
                            Dim indexLen As Int32 = CurrentIndex.Len
                            If DataLen > indexLen Then
                                CurrentIndex.DataTemp = CurrentIndex.DataTemp.Substring(0, indexLen)
                            End If
                            idValues = idValues & ",'" & CurrentIndex.DataTemp & "'"
                        End If
                End Select
                CurrentIndex.Data = CurrentIndex.DataTemp
                CurrentIndex.dataDescription = CurrentIndex.dataDescriptionTemp
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
    End Function
    '
    ''' <summary>
    ''' Se creo este overload porque cuando se intentaba realizar una insercion en la base de un tipo alfanum�rico,
    ''' blanqueaba datatemp porque realizaba en el case 7 un substring de 0 a 0
    ''' </summary>
    ''' <param name="Result"></param>
    ''' <param name="tableName"></param>
    ''' <returns></returns>
    ''' <remarks>SEBASTI�N</remarks>
    Private Function CreateInsertQueryAlfanumerico(ByRef Result As NewResult, ByVal tableName As String) As String
        Dim columns As String = "DOC_ID"
        Dim idValues As String = Result.ID
        Const I As String = ", I"

        For Each CurrentIndex As Index In Result.Indexs
            If CurrentIndex.isReference = False Then
                If Not String.IsNullOrEmpty(CurrentIndex.DataTemp) Then

                    Select Case CInt(CurrentIndex.Type)

                        Case 1, 2, 3, 6 ' Es Numerico y Decimal el 6
                            If Not String.IsNullOrEmpty(CurrentIndex.DataTemp.Trim) Then
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

                                If DataLen > indexLen Then
                                    CurrentIndex.DataTemp = CurrentIndex.DataTemp.Substring(0, indexLen)
                                End If

                                idValues = idValues & ",'" & CurrentIndex.DataTemp.Replace("'", "''") & "'"
                            End If
                        Case Else
                            If Not String.IsNullOrEmpty(CurrentIndex.DataTemp.Trim) Then
                                columns = columns & I & CurrentIndex.ID
                                Dim DataLen As Int32 = CurrentIndex.DataTemp.Trim
                                Dim indexLen As Int32 = CurrentIndex.Len
                                If DataLen > indexLen Then
                                    CurrentIndex.DataTemp = CurrentIndex.DataTemp.Substring(0, indexLen)
                                End If
                                idValues = idValues & ",'" & CurrentIndex.DataTemp & "'"
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
        Dim DocTable As String = "DOC_T" & Document.DocTypeId

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
    Public Shared Function CompleteDocument(ByVal ResultId As Int64, ByVal docTypeId As Int64, ByVal indexs As Index(), ByVal inThread As Boolean, ByVal restrictions As String) As DataRow
        Dim strTableI As String = Results_Factory.MakeTable(docTypeId, Results_Factory.TableType.Indexs)
        Dim strTableT As String = Results_Factory.MakeTable(docTypeId, Results_Factory.TableType.Document)

        Dim MainJoin As String = String.Format("{0} T inner join {1} I on T.doc_id = I.doc_id", strTableT, strTableI)

        Dim strselect As StringBuilder = New StringBuilder()
        Dim auIndex As New List(Of Int64)

        strselect.Append("SELECT T.DOC_ID, ")
        strselect.Append("T.DISK_GROUP_ID,PLATTER_ID,VOL_ID,DOC_FILE,OFFSET,")
        strselect.Append("T.DOC_TYPE_ID, ")
        strselect.Append("T.NAME as ")
        strselect.Append(Chr(34))
        strselect.Append(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME)
        strselect.Append(Chr(34))
        strselect.Append(",")
        strselect.Append("T.ICON_ID,SHARED,ver_Parent_id,RootId,original_Filename as ")
        strselect.Append(Chr(34))
        strselect.Append("Nombre Original")
        strselect.Append(Chr(34))
        strselect.Append(",Version, NumeroVersion as ")
        strselect.Append(Chr(34))
        strselect.Append(GridColumns.NUMERO_DE_VERSION_COLUMNNAME)
        strselect.Append(Chr(34))
        strselect.Append(",disk_Vol_id, DISK_VOL_PATH, doc_type_name as ")
        strselect.Append(Chr(34))
        strselect.Append("Entidad")
        strselect.Append(Chr(34))
        strselect.Append(", crdate as ")
        strselect.Append(Chr(34))
        strselect.Append("Fecha Creacion")
        strselect.Append(Chr(34))
        strselect.Append(", lupdate as ")
        strselect.Append(Chr(34))
        strselect.Append("Fecha Modificacion")
        strselect.Append(Chr(34))
        For Each _Index As Index In indexs
            'If IsNothing(VisibleIndexs) OrElse VisibleIndexs.Contains(_Index.ID) Then
            strselect.Append(",")
            strselect.Append("I.I")
            strselect.Append(_Index.ID)
            If _Index.DropDown = IndexAdditionalType.AutoSustituci�n _
                Or _Index.DropDown = IndexAdditionalType.AutoSustituci�nJerarquico Then
                strselect.Append(", slst_s" & _Index.ID & ".descripcion")
                auIndex.Add(_Index.ID)
            End If
            strselect.Append(" as ")
            strselect.Append(Chr(34))
            strselect.Append(_Index.Name)
            strselect.Append(Chr(34))

        Next
        strselect.Append(" FROM ")
        strselect.Append(MainJoin)
        strselect.Append(" inner join doc_type on doc_type.doc_type_id = ")
        strselect.Append("T.doc_type_id left outer join disk_Volume on disk_Vol_id=vol_id ")

        If auIndex.Count > 0 Then
            For Each indiceID As Int64 In auIndex
                strselect.Append(" left join slst_s" & indiceID & " on I.i" & indiceID & " = slst_s" & indiceID & ".codigo ")
            Next
        End If
        strselect.Append(" where t.doc_id=")
        strselect.Append(ResultId.ToString())

        If Not String.IsNullOrEmpty(restrictions) Then
            strselect.Append(" AND ")
            strselect.Append(restrictions)
        End If

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
    Public Shared Function CompleteDocument(ByVal ResultId As Int64, ByVal docTypeId As Int64, ByVal indexs As List(Of IIndex), ByVal con As IConnection, Optional ByVal inThread As Boolean = False) As IDataReader
        Dim strTableI As String = Results_Factory.MakeTable(docTypeId, Results_Factory.TableType.Indexs)
        Dim strTableT As String = Results_Factory.MakeTable(docTypeId, Results_Factory.TableType.Document)

        Dim MainJoin As String = String.Format("{0} T inner join {1} I on T.doc_id = I.doc_id", strTableT, strTableI)
        Dim strselect As StringBuilder = New StringBuilder()

        strselect.Append("SELECT T.DOC_ID, T.DISK_GROUP_ID,T.PLATTER_ID,T.VOL_ID,T.DOC_FILE,T.OFFSET,T.DOC_TYPE_ID,")
        strselect.Append("T.NAME,T.ICON_ID,T.SHARED")
        strselect.Append(",T.ver_Parent_id,T.version,T.RootId,T.original_Filename, T.NumeroVersion,disk_Vol_id, DISK_VOL_PATH, I.crdate ")
        Dim f As Int16
        For f = 0 To indexs.Count - 1
            If indexs(f).isReference = False Then
                strselect.Append(", I.I" & DirectCast(indexs(f), Index).ID)
            End If
        Next
        strselect.Append(" FROM ")
        strselect.Append(MainJoin)
        strselect.Append(" left outer join disk_Volume on disk_Vol_id = T.vol_id where T.doc_id = ")
        strselect.Append(ResultId.ToString())

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

    Public Shared Function SearchInAllIndexs(query As String) As DataSet
        Return Server.Con.ExecuteDataset(CommandType.Text, query)
    End Function

    ''' <summary>
    ''' Realiza la busqueda en el indexado de la palabra especificada
    ''' </summary>
    ''' <param name="Search"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function SearchInAllIndexs(ByVal Search As Search, ByRef searchSql As String) As DataSet
        Dim strSelect As String
        Dim strFrom As String
        Dim strInDocType As String
        Dim strWhere As String
        Dim strLike As String
        Dim sqlBuilder As New StringBuilder()

        strSelect = "SELECT distinct ZSearchValues_DT.ResultId, ZSearchValues_DT.DTID "
        strFrom = "FROM ZSearchValues INNER JOIN ZSearchValues_DT ON ZSearchValues_DT.WordId = ZSearchValues.Id "
        strWhere = "WHERE ("
        strLike = "(ZSearchValues_DT.ResultId in (SELECT distinct ZSearchValues_DT.ResultId FROM ZSearchValues INNER JOIN ZSearchValues_DT ON ZSearchValues_DT.WordId = ZSearchValues.Id WHERE (word like '%"
        strInDocType = "ZSearchValues_DT.DTID = "

        sqlBuilder.Append(strSelect)
        sqlBuilder.Append(strFrom)
        sqlBuilder.Append(strWhere)

        Dim strSplitedWord() As String = Search.Textsearch.Trim.Split(" ")

        For Each _strword As String In strSplitedWord
            sqlBuilder.Append(strLike)
            sqlBuilder.Append(_strword.ToLower)
            sqlBuilder.Append("%')))")
            sqlBuilder.Append(" OR ")
        Next

        sqlBuilder.Remove(sqlBuilder.ToString().Length - 4, 4)

        sqlBuilder.Append(") ")

        sqlBuilder.Append("AND (")

        For Each _docType As IDocType In Search.Doctypes
            If Search.Doctypes.Count > 1 And _docType.ID <> Search.Doctypes(0).ID Then
                sqlBuilder.Append(" OR ")
            End If
            sqlBuilder.Append(strInDocType)
            sqlBuilder.Append(_docType.ID.ToString())
        Next
        sqlBuilder.Append(")")
        searchSql = sqlBuilder.ToString()
        Return Server.Con.ExecuteDataset(CommandType.Text, searchSql)
    End Function


    Public Shared Function GetSearchInAllIndexsJoins(ByVal TextSearch As String, ByVal EntityId As Int64, ByVal EQ As IEntityEnabledForQuickSearch) As String

        Dim strSelect As String
        Dim strFrom As String
        Dim strInDocType As String
        Dim strWhere As String
        Dim strLike As String
        Dim sqlBuilder As New StringBuilder()

        strSelect = "SELECT DISTINCT ZSEARCHVALUES_DT1.ResultId, ZSEARCHVALUES_DT1.DTID "
        strFrom = " FROM ZSearchValues   ZSEARCHVALUES1, ZSearchValues_DT ZSEARCHVALUES_DT1  "
        strWhere = " WHERE "
        strLike = " (word like '%"
        strInDocType = " AND ZSEARCHVALUES_DT1.DTID = "

        sqlBuilder.Append(strSelect)
        sqlBuilder.Append(strFrom)
        sqlBuilder.Append(strWhere)

        Dim strSplitedWord() As String = TextSearch.Trim.Split(" ")

        For Each _strword As String In strSplitedWord
            sqlBuilder.Append(strLike)
            sqlBuilder.Append(_strword.ToLower)
            sqlBuilder.Append("%'")
            sqlBuilder.Append(" OR ")
        Next
        sqlBuilder.Remove(sqlBuilder.ToString().Length - 4, 4)
        sqlBuilder.Append(") ")

        sqlBuilder.Append(" AND ZSEARCHVALUES_DT1.WordId =
                                                         ZSEARCHVALUES1.Id ")

        sqlBuilder.Append(strInDocType)
        sqlBuilder.Append(EntityId.ToString())

        If EQ IsNot Nothing AndAlso EQ.IndexsIds.Count > 0 Then
            For Each IndexId As Int64 In EQ.IndexsIds
                sqlBuilder.Append(" and ZSEARCHVALUES_DT1.IndexId in (")
                sqlBuilder.Append(IndexId)
                sqlBuilder.Append(",")
            Next
            sqlBuilder.Remove(sqlBuilder.Length - 1, 1)
            sqlBuilder.Append(")")
        End If

        Return sqlBuilder.ToString()
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
        Dim sql As New StringBuilder

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
            sql.Append(ServersFactory.ConvertDate(IndexData, True))
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
        query.Append("SELECT count(1) FROM ZDOCRELATIONS WHERE DOC_ID =")
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
        query.Append("SELECT count(1) FROM ZDOCRELATIONS WHERE DOC_ID =")
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
    Public Shared Sub Delete(ByVal Result As IResult, Optional ByVal delfile As Boolean = True)

        Try
            Dim TableT As String = MakeTable(Result.DocTypeId, TableType.Document)
            Dim TableI As String = MakeTable(Result.DocTypeId, TableType.Indexs)
            Dim TableB As String = MakeTable(Result.DocTypeId, TableType.Blob)
            Dim TableNotes As String = "Doc_Notes"
            Dim StrDelete As String

            Try
                StrDelete = "DELETE FROM " & TableI & " Where (Doc_ID = " & Result.ID & ")"
                Server.Con.ExecuteNonQuery(CommandType.Text, StrDelete)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Try
                If TableB IsNot Nothing Then
                    StrDelete = "DELETE FROM " & TableB & " Where (Doc_ID = " & Result.ID & ")"
                    Server.Con.ExecuteNonQuery(CommandType.Text, StrDelete)
                End If
            Catch ex As Exception
                ' ZClass.raiseerror(ex)
            End Try
            Try
                StrDelete = "DELETE FROM " & TableT & " Where (Doc_ID = " & Result.ID & ")"
                Server.Con.ExecuteNonQuery(CommandType.Text, StrDelete)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Try
                StrDelete = "DELETE FROM " & TableNotes & " Where (Doc_ID = " & Result.ID & ")"
                Server.Con.ExecuteNonQuery(CommandType.Text, StrDelete)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Try
                StrDelete = "DELETE FROM ZBARCODE Where (Doc_ID = " & Result.ID & ")"
                Server.Con.ExecuteNonQuery(CommandType.Text, StrDelete)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

            'Andres 4/9/28 - Si es documento virtual no tiene path , no se borra
            If Result.ISVIRTUAL = False Then
                Dim file As IO.FileInfo = Nothing
                Try
                    file = New IO.FileInfo(Result.FullPath)
                    Dim FileLen As Decimal
                    If file.Exists = False Then
                        FileLen = 0
                    Else
                        FileLen = CDec(file.Length) / 1000
                    End If

                    If Server.isOracle Then
                        ''Dim parNames() As String = {"VolumeId", "FileSize"}
                        'Dim parTypes() As Object = {13, 13}
                        Dim parValues() As Object = {Result.Disk_Group_Id, FileLen}
                        'Server.Con.ExecuteNonQuery("UPDATEVOLDELFILE_PKG.UPDATEVOLDELFILE",  parValues)
                        Server.Con.ExecuteNonQuery("zsp_volume_100.UpdDeletedFiles", parValues)
                    Else
                        Dim parametersValues() As Object = {Result.Disk_Group_Id, FileLen}
                        'Server.Con.ExecuteDataset("UPDATEVOLDELFILE", parametersValues)
                        Server.Con.ExecuteDataset("zsp_volume_100_UpdDeletedFiles", parametersValues)
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try

                Try
                    Try
                        'If delfile = True Then file.Delete()
                        If delfile = True Then IO.File.Delete(Result.FullPath)
                    Catch ex As Exception
                        Results_Factory.FilesForDelete.Add(file)
                    End Try
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End If


            Try
                If Server.isOracle Then
                    ''Dim parNames() As String = {"DocID", "X"}
                    'Dim parTypes() As Object = {13, 13}
                    Dim parValues() As Object = {Result.DocTypeId, -1}
                    'Server.Con.ExecuteNonQuery("IncrementarDocType_pkg.IncrementarDocType",  parValues)
                    Server.Con.ExecuteNonQuery("zsp_doctypes_100.IncrementsDocType", parValues)
                Else
                    Dim parametersValues() As Object = {Result.DocTypeId, -1}
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

    Public Shared Sub Delete(ByVal taskId As Int64, ByVal DocTypeId As Int64, ByVal fullPath As String, Optional ByVal deleteFile As Boolean = True)

        Dim TableT As String = MakeTable(DocTypeId, TableType.Document)
        Dim TableI As String = MakeTable(DocTypeId, TableType.Indexs)
        Dim TableB As String = MakeTable(DocTypeId, TableType.Blob)

        'Dim StrDelete As String
        Dim QueryBuilder As New StringBuilder()

        QueryBuilder.Append("DELETE FROM ")
        QueryBuilder.Append(TableI)
        QueryBuilder.Append(" Where (Doc_ID = ")
        QueryBuilder.Append(taskId.ToString())
        QueryBuilder.Append(")")
        Server.Con.ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString())

        QueryBuilder.Remove(0, QueryBuilder.Length)
        QueryBuilder.Append("DELETE FROM ")
        QueryBuilder.Append(TableB)
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


        If deleteFile AndAlso IO.File.Exists(fullPath) Then
            IO.File.Delete(fullPath)
        End If

        If Server.isOracle Then
            ''Dim parNames() As String = {"DocID", "X"}
            'Dim parTypes() As Object = {13, 13}
            Dim parValues() As Object = {DocTypeId, -1}
            'Server.Con.ExecuteNonQuery("IncrementarDocType_pkg.IncrementarDocType",  parValues)
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

    ''' <summary>
    ''' Updates the result encoded file
    ''' </summary>
    ''' <param name="res"></param>
    ''' <remarks></remarks>
    Public Shared Sub UpdateDOCB(ByVal res As Result)
        Dim query As String = "UPDATE DOC_B" & res.DocTypeId.ToString & " SET DOCFILE = @docFile WHERE DOC_ID = " & res.ID.ToString

        If Server.isOracle Then

        Else
            Dim pDocFile As SqlParameter
            Dim sql_version As String = Server.Con.ExecuteScalar(CommandType.Text, "select @@version")
            If sql_version.StartsWith("Microsoft SQL Server  2000") Then
                pDocFile = New SqlParameter("@docFile", SqlDbType.Image)
            Else
                pDocFile = New SqlParameter("@docFile", SqlDbType.VarBinary)
            End If

            pDocFile.Value = res.EncodedFile
            Dim params As IDbDataParameter() = {pDocFile}

            Server.Con.ExecuteNonQuery(CommandType.Text, query, params)
            pDocFile.Value = Nothing
            pDocFile = Nothing
            params = Nothing
        End If

    End Sub

    ''' <summary>
    ''' Inserta un registro en la DOC_B
    ''' </summary>
    ''' <param name="newRes"></param>
    ''' <param name="t"></param>
    ''' <remarks>Se utiliza para migrar documentos existentes en servidor a la base de datos</remarks>
    Public Shared Sub InsertIntoDOCB(ByRef res As IResult, ByVal IsZipped As Boolean)

        Dim zipped As Integer = 0

        If IsZipped Then zipped = 1

        Dim query As String = "INSERT INTO DOC_B" & res.DocTypeId.ToString & " VALUES(" & res.ID.ToString & ", @docFile, " & zipped & ")"

        If Server.isOracle Then

        Else
            Dim pDocFile As SqlParameter
            Dim sql_version As String = Server.Con.ExecuteScalar(CommandType.Text, "select @@version")

            If sql_version.StartsWith("Microsoft SQL Server  2000") Then
                pDocFile = New SqlParameter("@docFile", SqlDbType.Image)
            Else
                pDocFile = New SqlParameter("@docFile", SqlDbType.VarBinary)
            End If

            pDocFile.Value = res.EncodedFile
            Dim params As IDbDataParameter() = {pDocFile}

            Server.Con.ExecuteNonQuery(CommandType.Text, query, params)
            pDocFile.Value = Nothing
            pDocFile = Nothing
            params = Nothing
        End If
    End Sub


    ''' <summary>
    ''' Inserta un registro en la DOC_B
    ''' </summary>
    ''' <param name="newRes"></param>
    ''' <param name="t"></param>
    ''' <remarks>Se utiliza para migrar documentos existentes en servidor a la base de datos sin l�mite de timeout</remarks>
    Public Shared Sub InsertIntoDOCBForMigrator(ByRef res As IResult, ByVal IsZipped As Boolean)

        Dim zipped As Integer = 0

        If IsZipped Then zipped = 1

        Dim query As String = "INSERT INTO DOC_B" & res.DocTypeId.ToString & " VALUES(" & res.ID.ToString & ", @docFile, " & zipped & ")"

        If Server.isOracle Then

        Else
            Dim pDocFile As SqlParameter
            Dim sql_version As String = Server.Con.ExecuteScalar(CommandType.Text, "select @@version")

            If sql_version.StartsWith("Microsoft SQL Server  2000") Then
                pDocFile = New SqlParameter("@docFile", SqlDbType.Image)
            Else
                pDocFile = New SqlParameter("@docFile", SqlDbType.VarBinary)
            End If

            pDocFile.Value = res.EncodedFile
            Dim params As IDbDataParameter() = {pDocFile}

            'Server.Con.ExecuteNonQuery(CommandType.Text, query, params)
            Server.Con.ExecuteNoTimeOutNonQuery(CommandType.Text, query, params)

            pDocFile.Value = Nothing
            pDocFile = Nothing
            params = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Inserta un registro en la DOC_B
    ''' </summary>
    ''' <param name="newRes"></param>
    ''' <param name="t"></param>
    ''' <remarks>Se utiliza en la insercion</remarks>
    Public Shared Sub InsertIntoDOCB(ByRef newRes As INewResult, ByRef t As Transaction, ByVal isZipped As Boolean)

        Dim zipped As Integer = 0

        If isZipped Then zipped = 1

        Dim query As String = "INSERT INTO DOC_B" & newRes.DocTypeId.ToString & " VALUES(" & newRes.ID.ToString & ", @docFile, " & zipped & ")"

        If Not IsNothing(newRes.EncodedFile) Then
            If Server.isOracle Then

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
                pDocFile.Value = Nothing
                pDocFile = Nothing
                params = Nothing
            End If
        Else
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Archivo de Blob vacio")
        End If
    End Sub

    <Obsolete("Metodo discontinuado", False)>
    Public Shared Sub Delete(ByRef Result As NewResult, Optional ByVal delfile As Boolean = True)
        Try
            Dim TableT As String = MakeTable(Result.DocTypeId, TableType.Document)
            Dim TableI As String = MakeTable(Result.DocTypeId, TableType.Indexs)
            Dim TableB As String = MakeTable(Result.DocTypeId, TableType.Blob)
            Dim TableNotes As String = "Doc_Notes"
            Dim StrDelete As String
            '            
            StrDelete = "DELETE FROM " & TableT & " Where (Doc_ID = " & Result.ID & ")"
            Server.Con.ExecuteNonQuery(CommandType.Text, StrDelete)
            StrDelete = "DELETE FROM " & TableI & " Where (Doc_ID = " & Result.ID & ")"
            Server.Con.ExecuteNonQuery(CommandType.Text, StrDelete)
            StrDelete = "DELETE FROM " & TableB & " Where (Doc_ID = " & Result.ID & ")"
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
                ''Dim parNames() As String = {"VolumeId", "FileSize"}
                'Dim parTypes() As Object = {13, 13}
                Dim parValues() As Object = {Result.Volume.ID, FileLen}
                'Server.Con.ExecuteNonQuery("UPDATEVOLDELFILE_PKG.UPDATEVOLDELFILE",  parValues)
                Server.Con.ExecuteNonQuery("zsp_volume_100.UpdDeletedFiles", parValues)
            Else
                Dim parametersValues() As Object = {Result.Volume.ID, FileLen}
                'Server.Con.ExecuteDataset("UPDATEVOLDELFILE", parametersValues)
                Server.Con.ExecuteDataset("zsp_volume_100_UpdDeletedFiles", parametersValues)
            End If

            Try
                If delfile = True Then file.Delete()
            Catch ex As Exception
                Results_Factory.FilesForDelete.Add(file)
            End Try
        Catch ex As Exception
            Throw New Exception("Ocurrio un error al intentar eliminar el documento" & " " & ex.ToString)
        End Try
    End Sub
    <Obsolete("Metodo discontinuado", False)>
    Public Shared Sub Delete(ByRef Result As NewResult, ByVal delfile As Boolean, ByRef t As Transaction)
        Try
            Dim TableT As String = MakeTable(Result.DocTypeId, TableType.Document)
            Dim TableI As String = MakeTable(Result.DocTypeId, TableType.Indexs)
            Dim TableB As String = MakeTable(Result.DocTypeId, TableType.Blob)
            Dim TableNotes As String = "Doc_Notes"
            Dim StrDelete As String
            '            
            StrDelete = "DELETE FROM " & TableT & " Where (Doc_ID = " & Result.ID & ")"
            t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, StrDelete)
            StrDelete = "DELETE FROM " & TableB & " Where (Doc_ID = " & Result.ID & ")"
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
                '''Dim parNames() As String = {"VolumeId", "FileSize"}
                ''Dim parTypes() As Object = {13, 13}
                Dim parValues() As Object = {Result.Volume.ID, FileLen}
                'Server.Con.ExecuteNonQuery("UPDATEVOLDELFILE_PKG.UPDATEVOLDELFILE",  parValues)
                t.Con.ExecuteNonQuery(t.Transaction, "zsp_volume_100.UpdDeletedFiles", parValues)
            Else
                Dim parametersValues() As Object = {Result.Volume.ID, FileLen}
                'Server.Con.ExecuteDataset("UPDATEVOLDELFILE", parametersValues)
                t.Con.ExecuteNonQuery(t.Transaction, "zsp_volume_100_UpdDeletedFiles", parametersValues)
            End If

            Try
                If delfile = True Then file.Delete()
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

#End Region

#Region "DOCFILE"

    <Obsolete("Metodo discontinuado", False)>
    Public Shared Sub updatedocfile(ByRef Result As Result, ByVal docfile As String)
        Try
            Dim strupdate As String = "UPDATE DOC_T" & Result.DocTypeId & " set DOC_FILE = '" & docfile & "' where doc_id = " & Result.ID
            Server.Con.ExecuteNonQuery(CommandType.Text, strupdate)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Shared Sub ReplaceFile(ByRef Result As Result, ByVal NewDocumentFile As String)
        'GC.Collect()

        Try
            Dim Fi As New IO.FileInfo(Result.FullPath)
            Dim NewFi As New IO.FileInfo(NewDocumentFile)

            Dim NewName As String = Results_Factory.MakefileName()
            Dim NewFullFileName As String = Fi.DirectoryName & "\" & NewName & NewFi.Extension
            NewFi.CopyTo(NewFullFileName, True)
            Result.Doc_File = NewName & NewFi.Extension
            Dim table As String = Results_Factory.MakeTable(Result.DocTypeId, TableType.Document)
            Dim Strupdate As String = "UPDATE " & table & " SET Doc_File = '" & NewName & NewFi.Extension & "' WHERE DOC_ID = " & Result.ID
            Server.Con.ExecuteNonQuery(CommandType.Text, Strupdate)

            '            Fi.Delete()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    <Obsolete("Metodo discontinuado", False)>
    Public Shared Sub ReplaceDocument(ByRef result As Result, ByVal NewName As String, ByVal ComeFromWF As Boolean, ByVal DsVols As DataSet)
        Dim table As String
        Dim volume As IVolume = Nothing

        Try
            table = Results_Factory.MakeTable(result.DocTypeId, TableType.Document)
            volume = VolumesFactory.LoadVolume(result.DocTypeId, DsVols)

            Dim QueryBuilder As New StringBuilder

            QueryBuilder.Append("UPDATE " & table & " SET Doc_File = '" & NewName & "', icon_id = " & result.IconId & ", offset= " & volume.offset & ",vol_id=" & volume.ID & ", Disk_Group_Id = " & volume.ID)
            QueryBuilder.Append(" WHERE DOC_ID = " & result.ID)

            Server.Con.ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString)

            If ComeFromWF Then
                Dim QueryBuilder1 As New StringBuilder

                QueryBuilder1.Append("UPDATE wfdocument SET iconId=" & result.IconId)

                If Server.isOracle Then
                    QueryBuilder1.Append(",LastUpdateDate=sysdate")
                Else
                    QueryBuilder1.Append(",LastUpdateDate=getdate()")
                End If

                QueryBuilder1.Append(" WHERE DOC_ID = " & result.ID)

                Server.Con.ExecuteNonQuery(CommandType.Text, QueryBuilder1.ToString)
            End If

            If volume.Type = VolumeTypes.DataBase Then
                ReplaceDigitalDocument(result)
            End If
        Finally
            If DsVols IsNot Nothing Then
                DsVols.Dispose()
                DsVols = Nothing
            End If
            If volume IsNot Nothing Then
                volume.Dispose()
                volume = Nothing
            End If
        End Try
    End Sub
    <Obsolete("Metodo discontinuado", False)>
    Public Shared Sub ReplaceDocument(ByRef Result As NewResult)
        Dim strup As New StringBuilder
        Dim StrSelect As String = "Select t.doc_id, t.doc_file , disk_vol_path,concat('\',concat(offset,concat('\',doc_file))) FILENAME from doc_t" & Result.DocTypeId & " t , disk_volume where vol_id=disk_vol_id "
        Dim ds2 As DataSet = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)

        If ds2.Tables(0).Rows.Count > 0 Then
            Result.ID = ds2.Tables(0).Rows(0).Item(0)
            Dim oldFile As String = ds2.Tables(0).Rows(0).Item(2) & "\" & Result.DocTypeId & ds2.Tables(0).Rows(0).Item(3)
            Dim olfi As New IO.FileInfo(oldFile.Trim)

            Dim ifinf As New IO.FileInfo(Result.File)
            If strup.ToString <> "" Then
                Server.Con.ExecuteNonQuery(CommandType.Text, "update doc_i" & Result.DocTypeId & " set " & strup.ToString.Substring(0, strup.ToString.LastIndexOf(",")) & " where doc_id=" & Result.ID)
                Server.Con.ExecuteNonQuery(CommandType.Text, "update doc_t" & Result.DocTypeId & " set doc_file=" & "'" & New IO.FileInfo(Result.NewFile).Name & "'" & ", offset= " & Result.Volume.offset & ",vol_id=" & Result.Volume.ID & ",name=" & "'" & Result.Name & "'" & ",icon_id=" & Result.IconId & " where doc_id=" & Result.ID)
            End If
            'copyfile(Result)
            If olfi.Exists = True Then olfi.Delete()
        End If
    End Sub
    <Obsolete("Metodo discontinuado", False)>
    Public Shared Sub ReplaceDocument(ByRef Result As NewResult, ByRef t As Transaction)
        Dim strup As New StringBuilder
        Dim StrSelect As String = "Select t.doc_id, t.doc_file , disk_vol_path,concat('\',concat(offset,concat('\',doc_file))) FILENAME from doc_t" & Result.DocTypeId & " t , disk_volume where vol_id=disk_vol_id "
        Dim ds2 As DataSet = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)

        If ds2.Tables(0).Rows.Count > 0 Then
            Result.ID = ds2.Tables(0).Rows(0).Item(0)
            Dim oldFile As String = ds2.Tables(0).Rows(0).Item(2) & "\" & Result.DocTypeId & ds2.Tables(0).Rows(0).Item(3)
            Dim olfi As New IO.FileInfo(oldFile.Trim)

            Dim ifinf As New IO.FileInfo(Result.File)
            If strup.ToString <> "" Then
                t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, "update doc_i" & Result.DocTypeId & " set " & strup.ToString.Substring(0, strup.ToString.LastIndexOf(",")) & " where doc_id=" & Result.ID)
                t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, "update doc_t" & Result.DocTypeId & " set doc_file=" & "'" & New IO.FileInfo(Result.NewFile).Name & "'" & ", offset= " & Result.Volume.offset & ",vol_id=" & Result.Volume.ID & ",name=" & "'" & Result.Name & "'" & ",icon_id=" & Result.IconId & " where doc_id=" & Result.ID)
            End If
            'copyfile(Result)
            If olfi.Exists = True Then olfi.Delete()
        End If
    End Sub

    <Obsolete("Metodo discontinuado", False)>
    Public Sub ReplaceDocument(ByRef Result As Result, ByVal NewFi As IO.FileInfo, ByVal NewName As String, ByVal ComeFromWF As Boolean)


        Dim RF As New Results_Factory

        Try
            Dim QueryBuilder As New StringBuilder
            Dim table As String = RF.MakeTable(Result.DocType.ID, TableType.Document)

            QueryBuilder.Append("UPDATE " & table & " SET Doc_File = '" & NewName & "', icon_id = " & Result.IconId & ", offset= " & Result.OffSet & ",vol_id=" & Result.Disk_Group_Id & ", Disk_Group_Id = " & Result.Disk_Group_Id)
            QueryBuilder.Append(" WHERE DOC_ID = " & Result.ID)

            Server.Con.ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString)

            If ComeFromWF Then
                Dim QueryBuilder1 As New StringBuilder

                QueryBuilder1.Append("UPDATE wfdocument SET iconId=" & Result.IconId)

                If Server.isOracle Then
                    QueryBuilder1.Append(",LastUpdateDate=sysdate")
                Else
                    QueryBuilder1.Append(",LastUpdateDate=getdate()")
                End If

                QueryBuilder1.Append(" WHERE DOC_ID = " & Result.ID)

                Server.Con.ExecuteNonQuery(CommandType.Text, QueryBuilder1.ToString)
            End If

            'If Volume.Type = VolumeTypes.DataBase Then
            '    ReplaceDigitalDocument(Result)
            'End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            RF = Nothing
        End Try
    End Sub

    Public Shared Sub ReplaceDigitalDocument(ByVal result As Result)
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

    Public Shared Function GetNewVersionID(ByVal RootId As Long, ByVal doctype As Int64, ByVal originalDocId As Int64) As Int32
        Dim Query As New StringBuilder

        Query.Append("SELECT max(NumeroVersion) FROM doc_t")
        Query.Append(doctype.ToString())
        Query.Append(" WHERE ROOTID = ")
        Query.Append(RootId.ToString())
        Dim maxVersion As Integer = Server.Con.ExecuteScalar(CommandType.Text, Query.ToString)
        If Not IsDBNull(maxVersion) Then
            Return 1 + maxVersion
        Else
            Return 1
        End If
    End Function

    Public Shared Sub setParentVersion(ByVal docTypeId As Int64, ByVal docId As Int64)
        Dim Query As New StringBuilder()
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
                ''Dim parNames() As Object = {"Par_docId", "Par_comment"}
                'Dim parTypes() As Object = {13, 7}
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
                ''Dim parNames() As Object = {"Par_docId", "Par_comment"}
                'Dim parTypes() As Object = {13, 7}
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
            ''Dim parNames() As Object = {"Parm_publishid", "Parm_docid", "Parm_userid", "Par_publishdate"}
            'Dim parTypes() As Object = {13, 13, 13, 13} ' encontrar el tipo date
            Dim parValues() As Object = {publishid, docid, userid, publishdate}
            Server.Con.ExecuteNonQuery("ZSP_VERSION_300.INSERTPUBLISH", parValues)
        Else
            Dim parameters() As Object = {publishid, docid, userid, publishdate}
            Server.Con.ExecuteNonQuery("ZSP_VERSION_Insert_Publish", parameters)
        End If
    End Sub


    Public Shared Function GetPublishEvents() As DataSet
        Return Server.Con.ExecuteDataset(CommandType.Text, "SELECT * FROM ZVerEvents")
    End Function


    Public Shared Function GetPublishEventsIds(ByVal EventName As String) As Int32
        Return Server.Con.ExecuteScalar(CommandType.Text, "SELECT EventId FROM ZVerEvents WHERE Event = '" + EventName + "'")
    End Function

    Public Shared Function GetPublishableIndexsStates(ByVal idDocType As Int64) As DataSet
        Dim dataset As New DataSet
        If Server.isSQLServer Then
            Dim query As String = "SELECT DOC_TYPE.DOC_TYPE_NAME as 'Entidad', DOC_INDEX.INDEX_NAME as 'Atributo', Zverevents.Event as 'Evento', ZVerEv.EvValue as 'Valor' FROM ZVerConfig INNER Join DOC_INDEX ON ZVerConfig.IndexId = DOC_INDEX.INDEX_ID INNER Join DOC_TYPE ON ZVerConfig.DtId = DOC_TYPE.DOC_TYPE_ID INNER Join ZVerEv ON ZVerConfig.dataid = ZVerEv.dataid INNER Join Zverevents ON ZVerEv.eventid= Zverevents.eventid WHERE DtId = " + idDocType.ToString
            dataset = Server.Con.ExecuteDataset(CommandType.Text, query)
        Else
            dataset = Server.Con.ExecuteDataset(CommandType.Text, "SELECT DOC_TYPE.DOC_TYPE_NAME as " + Chr(34) + "Entidad" + Chr(34) + " , DOC_INDEX.INDEX_NAME as " + Chr(34) + "Atributo" + Chr(34) + " , Zverevents.Event as " + Chr(34) + "Evento" + Chr(34) + " , ZVerEv.EvValue as " + Chr(34) + "Valor" + Chr(34) + " FROM ZVerConfig INNER Join DOC_INDEX ON ZVerConfig.IndexId = DOC_INDEX.INDEX_ID INNER Join DOC_TYPE ON ZVerConfig.DtId = DOC_TYPE.DOC_TYPE_ID INNER Join ZVerEv ON ZVerConfig.dataid = ZVerEv.dataid INNER Join Zverevents ON ZVerEv.eventid= Zverevents.eventid WHERE DtId = " + idDocType.ToString)
        End If
        Return dataset
    End Function

    Public Shared Sub SavePublishableIndexsState(ByVal dataid As Int64, ByVal DocTypeid As Int64, ByVal Indexid As Int64, ByVal eventId As Int32, ByVal DefValue As String)
        Server.Con.ExecuteNonQuery(CommandType.Text, "INSERT INTO ZVerConfig(DataId, DtId, IndexId) VALUES( " + dataid.ToString + ", " + DocTypeid.ToString + ", " + Indexid.ToString + ")")
        Server.Con.ExecuteNonQuery(CommandType.Text, "INSERT INTO ZVerEv(DataId, EventId, EvValue) VALUES( " + dataid.ToString + ", " + eventId.ToString + ", '" + DefValue + "')")
    End Sub
    Public Shared Function ValidatePublishableIndexsStateExistance(ByVal DocTypeid As Int64, ByVal Indexid As Int64, ByVal eventId As Int32) As Int32
        Dim query As String = "SELECT COUNT(DtId) FROM ZVerConfig INNER JOIN ZVerEv ON ZVerConfig.DataId = ZVerEv.DataId WHERE DtId =" + DocTypeid.ToString + " AND IndexId = " + Indexid.ToString + " AND EventId = " + eventId.ToString
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

    Public Shared Function GetAutoNameCode(ByVal docTypeId As Int64) As String
        Dim strselect As String = "Select AutoName from Doc_Type where(Doc_Type_Id = " & docTypeId & ")"
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
    Public Shared Sub SaveName(ByRef Result As IResult)

        Try

            Dim Table As String = MakeTable(Result.DocTypeId, TableType.Document)
            Dim StrUpDate As String = "UPDATE " & Table & " SET Name = '" & Result.Name & "' where Doc_id =" & Result.ID
            Server.Con.ExecuteNonQuery(CommandType.Text, StrUpDate)
            Dim QueryBuilder As New StringBuilder

            QueryBuilder.Append("UPDATE wfdocument SET Name = '" & Result.Name)

            If Server.isOracle Then
                QueryBuilder.Append("',LastUpdateDate=sysdate")
            Else
                QueryBuilder.Append("',LastUpdateDate=getdate()")
            End If

            QueryBuilder.Append(" where Doc_id =" & Result.ID)
            Server.Con.ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString)
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Autonombre de Id " & Result.ID & " modificado a " & Result.Name)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

#End Region

#Region "Icon"
    ''' <summary>
    ''' Actualiza el IconID del documento
    ''' </summary>
    ''' <param name="docTypeId"></param>
    ''' <param name="docId"></param>
    ''' <param name="iconID"></param>
    ''' <remarks></remarks>
    Public Shared Sub setIconId(ByVal docTypeId As Int64, ByVal docId As Int64, ByVal iconID As Int64)
        Dim Query As New StringBuilder()
        Query.Append("UPDATE doc_t")
        Query.Append(docTypeId.ToString())
        Query.Append(" SET icon_ID = ")
        Query.Append(iconID)
        Query.Append(" WHERE doc_id = ")
        Query.Append(docId)

        Server.Con.ExecuteNonQuery(CommandType.Text, Query.ToString())

        Query.Remove(0, Query.Length)

        Query.Append("UPDATE wfdocument")
        Query.Append(" SET iconID = ")
        Query.Append(iconID)

        If Server.isOracle Then
            Query.Append(",LastUpdateDate=sysdate")
        Else
            Query.Append(",LastUpdateDate=getdate()")
        End If

        Query.Append(" WHERE doc_id = ")
        Query.Append(docId)

        Server.Con.ExecuteNonQuery(CommandType.Text, Query.ToString())
    End Sub
#End Region

    ''' <summary>
    ''' Devuelve el path de un documento
    ''' por su id de doc. y id de tipo de doc.
    ''' </summary>
    ''' <returns>path</returns>
    Public Shared Function getPathForIdTypeIdDoc(ByVal doc_id As Int32,
    ByVal doc_type_id As Int32) As String

        Dim resultado As DataSet

            If Server.isOracle Then

                'Dim parValues() As Object = {doc_id, doc_type_id, 2}
                '''Dim parNames() As Object = {"doc_id", "doc_type_id", "io_cursor"}
                ''Dim parTypes() As Object = {13, 13, 5}

                Dim res As Object = Server.Con.ExecuteScalar(CommandType.Text, "select vol_id from doc_t" + doc_type_id.ToString + " where doc_id = " + doc_id.ToString())
                Dim volId As Int16
                If res IsNot Nothing Then
                    volId = Int16.Parse(res.ToString)
                Else
                    Throw New Exception("Documento inexistente")
                End If

                If volId > 0 Then
                        Dim consulta As String = "select dv.disk_vol_path || '\' ||" +
                                         " dt.doc_type_id || '\' ||" +
                                         " dt.offset || '\' ||" +
                                         " dt.doc_file RutaArchivo" +
                                         " from disk_volume dv inner join doc_t" +
                                          doc_type_id.ToString + " dt" +
                                         " on dt.vol_id=dv.disk_vol_id" +
                                         " where dt.doc_id=" +
                                         doc_id.ToString

                        ZTrace.WriteLineIf(ZTrace.IsVerbose, consulta)
                        resultado = Server.Con.ExecuteDataset(CommandType.Text, consulta)

                    ElseIf volId = -1 Then
                        Dim consulta As String = "select doc_file from doc_t" + doc_type_id.ToString + " where doc_id=" + doc_id.ToString
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, consulta)
                        resultado = Server.Con.ExecuteDataset(CommandType.Text, consulta)
                    ElseIf volId = -2 Then
                        Dim consulta As String = "select doc_file from doc_t" + doc_type_id.ToString + " where doc_id=" + doc_id.ToString
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, consulta)
                        resultado = Server.Con.ExecuteDataset(CommandType.Text, consulta)
                    End If

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

            Return Microsoft.VisualBasic.Strings.Trim(resultado.Tables(0).Rows(0).Item(0).ToString())
            'DirectCast(resultado.Tables(0).Rows(0).Item(0), String)


            Return Nothing
    End Function

#Region "WorkFlow"



    'Devuelve un DataTable con todos los WAIT ID esperando a por ese DTID (en la Tabla ZWFI). [Alejandro].
    Public Shared Function SelectWaitingDocTypeInZWFI(ByVal _DTID As Int64) As DataTable

        Dim sqlBuilder As New StringBuilder()
        Dim ds As New DataSet
        Dim dt As New DataTable

        Try

            sqlBuilder.Append("SELECT WI FROM ZWFI WHERE DTID = '")
            sqlBuilder.Append(_DTID.ToString())
            sqlBuilder.Append("'")

            'Oracle inserta el semiColon autom�ticamente, asi que lo ponemos en caso SQL
            If Server.isSQLServer Then
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

    'Por cada WAIT ID llama a la funci�n ZWFIIIndexsValidation, si todos los WI devuelven
    'verdadero en esa funci�n, entonces esta funci�n devuelve verdadero. Si alguno de low WI
    'devuelven False o hay alg�n error. Esta funci�n devuelve false.[Alejandro].
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

    'Verifica si hay documentos en ZWFI esperando por el DocType pasado por par�metro.[Alejandro].
    Public Shared Function VerifyIfWaitingDocuments(ByVal ruleId As Int64) As Int16

        Dim sqlBuilder As New StringBuilder
        Dim count As Int16

        Try
            sqlBuilder.Append("SELECT COUNT(WI) FROM ZWFI WHERE RuleID = '")
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
            sqlBuilder.Append("SELECT COUNT(WI) FROM ZWFII WHERE WI = '")
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

    'En cada row del DataTable updatea el InsertID especificado por par�metro.
    'llamando a la funci�n ZWFIUpdateInsertID. [Alejandro].
    Public Shared Sub ZWFIUpdateInsertID(ByVal _waitIDs As Int64(), ByVal _InsertID As Int64)

        Try
            For i As Int16 = 0 To i >= _waitIDs.Length
                ZWFIUpdateInsertID(_waitIDs(i), _InsertID)
            Next

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    'Updatea el InsertID pasado por par�metro en el WAIT ID pasado por par�metro
    'en la Tabla ZWFI. [Alejandro].
    Private Shared Sub ZWFIUpdateInsertID(ByVal _WI As Int32, ByVal _InsertID As Int32)

        Try
            Dim sqlbuilder As New StringBuilder()

            sqlbuilder.Append("UPDATE ZWFI SET InsertID = '")
            sqlbuilder.Append(_InsertID.ToString())
            sqlbuilder.Append("' WHERE WI = '")
            sqlbuilder.Append(_WI.ToString())
            sqlbuilder.Append("'")

            'Oracle inserta el semiColon autom�ticamente, asi que lo ponemos en caso SQL
            If Server.isSQLServer Then
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

        sqlBuilder.Append("SELECT COUNT(DTID) FROM ZI WHERE DTID = '")
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
    ''' M�todo que devuelve un dataset con el contenido de la tabla ZDocRelations
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="relationId"></param>
    ''' <param name="idRoot"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	01/08/2008	Created
    ''' </history>
    Public Shared Function GetZDocRelations()
        Return Server.Con.ExecuteDataset(CommandType.Text, "SELECT * FROM ZDocRelations")
    End Function

    Public Shared Sub DeleteValuesZsearchValue_DT(resultId As Integer)
        'Se elimina las palabras indexadas para volver a indexar
        Dim deleteSentence As String = "DELETE FROM ZSearchValues_DT WHERE ResultId=" + resultId.ToString
        Server.Con.ExecuteNonQuery(CommandType.Text, deleteSentence)
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

            Dim strWords As String() = Split(TextToIndex, "�")
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

End Class