Imports System.IO
Imports Zamba.Core.Caching
Imports Zamba.Servers
Imports System.Data
Imports System.Collections
Imports Zamba.AppBlock
Imports Zamba.Core
Imports Zamba.Data
Imports System.Text
Imports System.Linq
Imports System.Collections.Generic
Imports Zamba.Framework.Search.Data

Partial Public Class WebSearch

    Const _COMILLA_SIMPLE As String = "'"
    Const _PUNTO As String = "."
    Const _COMA As String = ","
    Const _PUNTO_COMA As String = ";"
    Const _PARENTESIS_CERRADO As String = ")"
    Const _ESP_PARENT_ABIERTO As String = " ("
    Const _ESPACIO As String = " "
    Const _OR As String = " OR "
    Const _AND As String = " AND "
    Const _AS As String = " AS "
    Const _COMILLA_COMA As String = ""","

    Dim RB As New Results_Business
    Dim UB As New UserBusiness

    ''' <summary>
    ''' Genera un Array de string con todas las consultas SQL para una determinada búsqueda de Webview
    ''' </summary>
    ''' <param name="dtypes">Array de los tipos de documentos incluidos en la búsqueda</param>
    ''' <param name="Indexs">Atributos de la búsqueda</param>
    ''' <param name="userId">Id de usuario</param>
    ''' <returns>Array con Querys de SQL</returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Javier]    17/05/2010  Created
    ''' </history>
    Public Overloads Function WebMakeSearch(ByVal dtypes As List(Of IDocType), ByVal indexs As Generic.List(Of IIndex), ByVal currentUser As IUser) As String()
        Dim queries() As String
        Dim s As New Zamba.Core.Searchs.Search()

        s.Doctypes = dtypes
        s.Indexs = indexs
        s.UserId = currentUser.ID
        s = SetSearchSettings(s)

        queries = WebMakeQuerySearch(s)
        s = Nothing

        Return queries
    End Function

    Public Function Search(ByVal s As Zamba.Core.Searchs.Search) As DataTable
        Dim dt As DataTable
        s = SetSearchSettings(s)

        If Not String.IsNullOrEmpty(s.Textsearch) Then
            Dim docTypeIds As New List(Of Int64)
            For i As Int16 = 0 To s.Doctypes.Count - 1
                docTypeIds.Add(s.Doctypes(i).ID)
            Next
            dt = RunWebTextSearch(s, docTypeIds)
            docTypeIds.Clear()
            docTypeIds = Nothing
        Else
            Dim queries() As String = WebMakeQuerySearch(s)
            dt = WebRunSearch(queries)
            queries = Nothing
        End If

        Return dt
    End Function

    ''' <summary>
    ''' Recibe un array de querys SQL de búsqueda de documentos, las ejecuta y une los resultados en un solo datatable
    ''' </summary>
    ''' <param name="SQL">Array de Querys SQL</param>
    ''' <returns>Tabla en memoria con el resultado de la búsqueda de documentos</returns>
    ''' <remarks></remarks>
    Public Overloads Function WebRunSearch(ByVal SQL() As String) As DataTable
        Dim key As String = String.Join(_COMA, SQL)
        Dim dt As New DataTable()

        For i As Integer = 0 To SQL.Length - 1
            dt.Merge(Results_Factory.RunWebTextSearch(SQL(i)))
        Next

        key = Nothing
        Return dt
    End Function

    Public Function RunWebTextSearch(ByVal Search As ISearch, ByVal DocTypesSelected As List(Of Int64)) As DataTable
        Dim dsResultsId As DataSet
        Dim dtResults As New DataTable

        Try
            If Not String.IsNullOrEmpty(Search.Textsearch) Then
                Dim ZFWST As New SearchToolsData(Zamba.Servers.Server.Con)
                dsResultsId = ZFWST.SearchInAllIndexs(Search)
                ZFWST.Dispose()
                ZFWST = Nothing

                If dsResultsId.Tables.Count > 0 Then
                    If dtResults.Rows.Count > 0 Then
                        'Ya hay resultados por indices, debo combinar solo dejando los resultados en comun
                        For Each R As DataRow In dtResults.Rows
                            If dsResultsId.Tables(0).Select("RESULTID = " & CType(R("DOC_ID"), Int64)).Length = 0 Then
                                R.Delete()
                            End If
                        Next
                        dtResults.AcceptChanges()
                    Else
                        For Each r As DataRow In dsResultsId.Tables(0).Rows
                            Dim drResult As DataRow = RB.GetResultRow(CType(r(0), Int64), CType(r(1), Int64))
                            If Not IsNothing(drResult) Then
                                dtResults.Merge(drResult.Table)
                            End If
                        Next
                    End If
                End If
            End If

            If dsResultsId.Tables.Count <> 0 Then
                For Each r As DataRow In dsResultsId.Tables(0).Rows
                    Dim drResult As DataRow = RB.GetResultRow(CType(r(0), Int64), CType(r(1), Int64))
                    If Not IsNothing(drResult) Then
                        dtResults.Merge(drResult.Table)
                    End If
                Next
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            dsResultsId = Nothing
        End Try
        UB.SaveAction(2, ObjectTypes.Documents, RightsType.Buscar, "Se realizó una búsqueda por la palabra: " & Search.Textsearch)

        Return dtResults
    End Function

    Public Function SetSearchSettings(ByVal search As Zamba.Core.Searchs.Search) As Zamba.Core.Searchs.Search

        Dim UP As New UserPreferences
        With search
            Dim ZOPT As New ZOptBusiness
            .CaseSensitive = Boolean.Parse(ZOPT.GetValue("CaseSensitive"))
            .UseVersion = Boolean.Parse(UP.getValue("UseVersion", UPSections.UserPreferences, False, search.UserId))
            .MaxResults = Int64.Parse(UP.getValue("MaxResults", UPSections.UserPreferences, 1000, search.UserId))
        End With
        UP = Nothing
        Return search
    End Function

    ''' <summary>
    ''' Genera el query SQL de una búsqueda para un determinado entidad
    ''' </summary>
    ''' <param name="docType">Entidad donde se realizará la consulta de búsqueda</param>
    ''' <param name="indexs">Atributos a buscar</param>
    ''' <param name="currentUser">Usuario actual</param>
    ''' <param name="flagCase">Opción FlagCase de Userconfig</param>
    ''' <param name="useVersion">Opción UseVersion de Userconfig</param>
    ''' <param name="countTop">Opción CountTop de Userconfig</param>
    ''' <returns>Query SQL de búsqueda para ese entidad</returns>
    ''' <remarks></remarks>
    Private Function WebMakeQuerySearch(ByVal s As ISearch) As String()
        Dim i As Integer
        Dim first As Boolean
        Dim table As String
        Dim sbValues As System.Text.StringBuilder
        Dim sbColCondition As System.Text.StringBuilder
        Dim sbOrder As System.Text.StringBuilder
        Dim sbRestrictions As System.Text.StringBuilder
        Dim sbQuery As System.Text.StringBuilder
        Dim hideColumns As Dictionary(Of String, Boolean) = Nothing
        Dim querys(s.Doctypes.Count - 1) As String
        Dim entityCount As Int16 = 0

        Dim RF As New Results_Factory
        Try
            Dim currentUser As IUser = UB.GetUserById(s.UserId)
            hideColumns = GetHideColumns(s.UserId)

            For Each entity As IDocType In s.Doctypes
                i = 0
                first = True
                table = RF.MakeTable(entity.ID, Core.TableType.Full)
                sbValues = New System.Text.StringBuilder
                sbColCondition = New System.Text.StringBuilder
                sbOrder = New System.Text.StringBuilder
                sbRestrictions = New System.Text.StringBuilder
                sbQuery = New System.Text.StringBuilder

                For i = 0 To s.Indexs.Count - 1
                    webCreateWhereSearch(sbValues, s.Indexs, i, s.CaseSensitive, sbColCondition, sbOrder, first)
                Next

                sbQuery = WebCreateSelectSearch(table, entity.Indexs, False, s.MaxResults, currentUser, entity.ID, hideColumns)

                If sbOrder.Length = 0 Then
                    sbOrder.Append("ORDER BY ")
                Else
                    sbOrder.Append(_COMA)
                End If
                sbOrder.Append(table)
                sbOrder.Append(".DOC_ID DESC")

                If s.UseVersion AndAlso hideColumns("Version") Then
                    If sbColCondition.Length > 0 Then
                        sbColCondition.Append(_AND)
                    Else
                        sbColCondition.Append(_ESPACIO)
                    End If
                    sbQuery.Append(table)
                    sbQuery.Append(".Version = 0")
                End If

                If s.UserId > 0 Then
                    'Obtenemos los indices de las restricciones
                    Dim RmF As New RestrictionsMapper_Factory
                    Dim restrictionIndexs As Generic.List(Of IIndex) = RmF.GetRestrictionIndexs(s.UserId, entity.ID)
                    RmF = Nothing

                    For i = 0 To restrictionIndexs.Count - 1
                        webCreateWhereSearch(sbValues, restrictionIndexs, i, s.CaseSensitive, sbRestrictions, sbOrder, first)
                    Next

                    If sbColCondition.Length = 0 AndAlso sbRestrictions.Length > 0 Then
                        sbQuery.Append(" WHERE " & sbRestrictions.ToString())
                    ElseIf sbColCondition.Length <> 0 AndAlso sbRestrictions.Length > 0 Then
                        sbQuery.Append(" WHERE " & sbRestrictions.ToString() & _AND & sbColCondition.ToString)
                    ElseIf sbColCondition.Length <> 0 AndAlso sbRestrictions.Length = 0 Then
                        sbQuery.Append(" WHERE " & sbColCondition.ToString)
                    End If

                    restrictionIndexs = Nothing
                End If

                If Server.isOracle Then
                    'Se inserta un comienzo especial para consultas oracle
                    sbQuery.Insert(0, "select * FROM (select a.*, rownum rnum from (")
                    'Se agrega el final de la consulta
                    sbQuery.Append(_ESPACIO)
                    sbQuery.Append(sbOrder.ToString)
                    sbQuery.Append(") a where rownum <= ")
                    sbQuery.Append(s.MaxResults)
                    sbQuery.Append(") where rnum >= 0")
                Else
                    sbQuery.Append(_ESPACIO)
                    sbQuery.Append(sbOrder.ToString)
                End If

                querys(entityCount) = sbQuery.ToString
                entityCount += 1
            Next

        Catch ex As Exception
            ZClass.raiseerror(ex)
            querys = Nothing

        Finally
            sbValues = Nothing
            sbColCondition = Nothing
            sbOrder = Nothing
            sbRestrictions = Nothing
            sbQuery = Nothing
            If hideColumns IsNot Nothing Then
                hideColumns.Clear()
                hideColumns = Nothing
            End If
            RF = Nothing
        End Try

        Return querys
    End Function

    ''' <summary>
    ''' Armado del select que se utiliza para las busquedas
    ''' </summary>
    ''' <param name="strTable">Nombre de la tabla. Ej: doc58</param>
    ''' <param name="Indexs">Indices que se van a traer</param>
    ''' <returns>Stringbuilder con la consulta</returns>
    ''' <history>   Marcelo    Created     20/08/09</history>
    ''' <remarks></remarks>
    Private Function WebCreateSelectSearch(ByVal strTable As String,
                                           ByVal Indexs As List(Of IIndex),
                                           ByVal IsFolDerSearch As Boolean,
                                           ByVal countTop As Int32,
                                           ByVal currentUser As IUser,
                                           ByVal docTypeId As Long,
                                           ByVal hideColumns As Dictionary(Of String, Boolean)) As StringBuilder
        Dim strselect As New StringBuilder
        Dim strinnerjoin As New StringBuilder
        Dim UP As New UserPreferences

        If Server.isOracle Then
            'El top en oracle se realiza mediante rownum en el where.
            strselect.Append("SELECT ")
        Else
            strselect.Append("SELECT TOP ")
            strselect.Append(countTop)
            strselect.Append(_ESPACIO)
        End If

        strselect.Append(strTable)
        strselect.Append(".DOC_ID,")
        strselect.Append(strTable)
        strselect.Append(".DOC_TYPE_ID,")

        If hideColumns("ResultName") Then
            strselect.Append(strTable)
            strselect.Append(".NAME as ")
            strselect.Append(Chr(34))
            strselect.Append("Nombre")
            strselect.Append(_COMILLA_COMA)
        End If

        strselect.Append(strTable)
        strselect.Append(".ICON_ID,")

        If hideColumns("OriginalName") Then
            If Server.isOracle Then
                strselect.Append("get_filename(")
                strselect.Append(strTable)
                strselect.Append(".original_Filename)")
            Else
                strselect.Append("REVERSE(SUBSTRING(REVERSE(")
                strselect.Append(strTable)
                strselect.Append(".original_Filename), 0, CHARINDEX('\', REVERSE(")
                strselect.Append(strTable)
                strselect.Append(".original_Filename))))")
            End If
            strselect.Append(_AS)
            strselect.Append(Chr(34))
            strselect.Append("Original")
            strselect.Append(_COMILLA_COMA)
        End If

        If hideColumns("Version") Then
            strselect.Append(strTable)
            strselect.Append(".Version,")
        End If

        If hideColumns("VersionNumber") Then
            strselect.Append(strTable)
            strselect.Append(".NumeroVersion as ")
            strselect.Append(Chr(34))
            strselect.Append("Numero de Version")
            strselect.Append(_COMILLA_COMA)
        End If

        If hideColumns("EntityName") Then
            strselect.Append("doc_type.doc_type_name as ")
            strselect.Append(Chr(34))
            strselect.Append("Entidad")
            strselect.Append(Chr(34))
            strselect.Append(",")
        End If

        If hideColumns("CreateDate") Then
            strselect.Append(strTable)
            strselect.Append(".crdate as ")
            strselect.Append(Chr(34))
            strselect.Append("Creado")
            strselect.Append(Chr(34))
            strselect.Append(",")
        End If

        If hideColumns("LastEdit") Then
            strselect.Append(strTable)
            strselect.Append(".lupdate as ")
            strselect.Append(Chr(34))
            strselect.Append("Modificado")
            strselect.Append(Chr(34))
            strselect.Append(",")
        End If
        Dim RiB As New RightsBusiness
        Dim showIndexsOnGrid As Boolean = Boolean.Parse(UP.getValue("ShowIndexsOnGrid", UPSections.UserPreferences, "True", currentUser.ID))
        Dim specificAttributes As Boolean = RiB.GetSpecificAttributeRight(currentUser, docTypeId)
        RiB = Nothing
        If showIndexsOnGrid Then
            For Each _Index As Index In Indexs
                If (Not specificAttributes) OrElse (UB.GetIndexRightValue(docTypeId, _Index.ID, currentUser.ID, RightsType.IndexSearch) OrElse
                                             UB.GetIndexRightValue(docTypeId, _Index.ID, currentUser.ID, RightsType.IndexView)) Then

                    If strselect.ToString.LastIndexOf(_COMA) <> strselect.Length - 1 Then
                        strselect.Append(_COMA)
                    End If
                    'En caso de que sea un tipo de indice SI_No mostramos la descripcion del mismo
                    If _Index.Type = IndexDataType.Si_No Then
                        If Server.isOracle Then
                            'strselect.Append(strTable)
                            strselect.Append(".I")
                            strselect.Append(_Index.ID)
                            strselect.Append(" , Case ")
                        Else
                            strselect.Append(" Case ")
                            strselect.Append(strTable)
                            strselect.Append(".I")
                            strselect.Append(_Index.ID)
                        End If
                        strselect.Append(" when 1 then 'Si' else 'No' end ")
                    Else
                        If _Index.DropDown = IndexAdditionalType.AutoSustitución OrElse _Index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                            'strselect.Append(_COMA)
                            strselect.Append("slst_s")
                            strselect.Append(_Index.ID)
                            strselect.Append(".DESCRIPCION")
                            With strinnerjoin
                                .Append(" LEFT JOIN slst_s")
                                .Append(_Index.ID)
                                .Append(" ON ")
                                .Append(strTable)
                                .Append(".I")
                                .Append(_Index.ID)
                                .Append(" = slst_s")
                                .Append(_Index.ID)
                                .Append(".CODIGO ")
                            End With
                        Else
                            strselect.Append(strTable)
                            strselect.Append(".I")
                            strselect.Append(_Index.ID)
                        End If
                    End If
                    strselect.Append(_AS)
                    strselect.Append(Chr(34))
                    strselect.Append(_Index.Name)
                    strselect.Append(Chr(34))
                End If
            Next
        End If

        strselect.Append(" FROM ")
        strselect.Append(strTable)
        strselect.Append(" inner join doc_type on doc_type.doc_type_id=")
        strselect.Append(strTable)
        strselect.Append(".doc_type_id ")
        strselect.Append(strinnerjoin.ToString)

        If strselect.ToString.LastIndexOf(_COMA) = strselect.Length - 1 Then
            strselect.Remove(strselect.Length - 1, 1)
        End If
        UP = Nothing

        Return strselect
    End Function

    ''' <summary>
    ''' Armado del where que se utiliza para las busquedas
    ''' </summary>
    ''' <param name="valueString"></param>
    ''' <param name="Indexs"></param>
    ''' <param name="i"></param>
    ''' <param name="FlagCase"></param>
    ''' <param name="ColumCondstring"></param>
    ''' <param name="Orderstring"></param>
    ''' <param name="First">Bandera que indica si es la 1era vez</param>
    ''' <history>   Marcelo    Created     20/08/09</history>
    ''' <remarks></remarks>
    Private Sub webCreateWhereSearch(ByRef sbValue As StringBuilder,
                                     ByVal Indexs As Generic.List(Of IIndex),
                                     ByRef i As Int64,
                                     ByRef FlagCase As Boolean,
                                     ByRef ColumCondstring As StringBuilder,
                                     ByRef Orderstring As StringBuilder,
                                     ByRef First As Boolean)
        Dim mainVal As String = String.Empty
        Dim tempVal As Object = Nothing

        Dim _NULLFUNCTION As String
        If Server.isOracle() Then
            _NULLFUNCTION = " nvl("
        Else
            _NULLFUNCTION = " IsNull("
        End If

        sbValue.Remove(0, sbValue.Length)
        sbValue.Append(Indexs(i).Data)

        'TODO: ¿DEBERIA EXISTIR ESTA LOGICA?
        'If String.IsNullOrEmpty(tablePrefix) Then
        '    indexColName = "[" & Indexs(i).Name & "]"
        'Else
        '    Select Case Indexs(i).DropDown
        '        Case IndexAdditionalType.AutoSustitución
        '            indexColName = "SLST_S" & Indexs(i).ID.ToString & ".CODIGO"
        '        Case Else
        '            indexColName = "DOC" & tablePrefix & ".I" & Indexs(i).ID.ToString
        '    End Select
        'End If

        If sbValue.Length <> 0 OrElse Indexs(i).[Operator].ToLower = "es nulo" Then
            If Indexs(i).[Operator] <> "SQL" AndAlso Not sbValue.ToString.StartsWith("select ", True, Nothing) Then
                If sbValue.ToString.Split(_PUNTO_COMA).Length > 1 AndAlso
                Indexs(i).[Operator].ToLower = "Dentro" Then
                    If Indexs(i).Type = IndexDataType.Alfanumerico OrElse
                        Indexs(i).Type = IndexDataType.Alfanumerico_Largo Then
                        FlagCase = True
                        mainVal = _COMILLA_SIMPLE & LCase(sbValue.Replace(_PUNTO_COMA, "';'").ToString) & _COMILLA_SIMPLE
                    End If
                Else
                    Select Case Indexs(i).Type
                        Case IndexDataType.Numerico, IndexDataType.Numerico_Largo, IndexDataType.Si_No

                            If sbValue.Length <> 0 Then
                                If Indexs(i).DropDown = IndexAdditionalType.AutoSustitución OrElse Indexs(i).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                    mainVal = _COMILLA_SIMPLE & sbValue.ToString & _COMILLA_SIMPLE
                                Else
                                    mainVal = sbValue.ToString()
                                End If
                            End If
                        Case IndexDataType.Numerico_Decimales, IndexDataType.Moneda
                            If sbValue.Length <> 0 Then
                                If System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator = _COMA Then
                                    sbValue = sbValue.Replace(_PUNTO, _COMA)
                                End If
                                mainVal = CDec(sbValue.ToString)
                                mainVal = mainVal.Replace(_COMA, _PUNTO)
                            End If
                        Case IndexDataType.Fecha, IndexDataType.Fecha_Hora
                            If Not String.IsNullOrEmpty(sbValue.ToString.Trim) Then
                                If Indexs(i).Type = IndexDataType.Fecha Then
                                    mainVal = Server.Con.ConvertDate(sbValue.ToString)
                                Else
                                    mainVal = Server.Con.ConvertDateTime(sbValue.ToString)
                                End If
                            End If
                        Case IndexDataType.Alfanumerico, IndexDataType.Alfanumerico_Largo
                            If FlagCase Then
                                mainVal = _COMILLA_SIMPLE & LCase(sbValue.ToString) & _COMILLA_SIMPLE
                            Else
                                mainVal = _COMILLA_SIMPLE & sbValue.ToString & _COMILLA_SIMPLE
                            End If
                    End Select
                End If
            Else
                mainVal = Indexs(i).Data
            End If

            Dim Op As String = mainVal
            If Op.Contains("''") Then Op = Op.Replace("''", _COMILLA_SIMPLE)
            mainVal = Op
            Op = String.Empty

            Dim separator As String = " AND "
            If ColumCondstring.Length > 0 Then
                ColumCondstring.Append(separator)
            End If

            Select Case Indexs(i).[Operator]
                Case "="
                    Op = "="
                    If FlagCase AndAlso (Indexs(i).Type = IndexDataType.Alfanumerico OrElse Indexs(i).Type = IndexDataType.Alfanumerico_Largo) Then
                        ColumCondstring.Append(" (lower(" & DirectCast(Indexs(i).Column, String) & ")=(" & mainVal & "))")
                    Else
                        ColumCondstring.Append(_ESP_PARENT_ABIERTO & DirectCast(Indexs(i).Column, String) & "=(" & mainVal & "))")
                    End If

                Case ">", "<", ">=", "<="
                    Op = ">"
                    ColumCondstring.Append(_ESP_PARENT_ABIERTO & Indexs(i).Column & Op & _ESP_PARENT_ABIERTO & mainVal & "))")

                Case "Es nulo"
                    Op = "is null"
                    ColumCondstring.Append(_ESP_PARENT_ABIERTO & Indexs(i).Column & " is null)")

                Case "<>"
                    Op = "<>"
                    ColumCondstring.Append(_ESP_PARENT_ABIERTO & Indexs(i).Column & " <> (" & mainVal & ") OR " & Indexs(i).Column & " is null)")

                Case "Entre"
                    Dim Data2Added As Boolean
                    Try
                        'cambio las a como indice a I ya que todos los indices vienen con dato algunos vacio otros no.
                        Select Case Indexs(i).Type
                            Case 1
                                If Not String.IsNullOrEmpty(Indexs(i).Data2) Then
                                    tempVal = Int64.Parse(Indexs(i).Data2)
                                    Data2Added = True
                                End If
                            Case 2
                                If Not String.IsNullOrEmpty(Indexs(i).Data2) Then
                                    tempVal = Int64.Parse(Indexs(i).Data2)
                                    Data2Added = True
                                End If
                            Case 3
                                If Not String.IsNullOrEmpty(Indexs(i).Data2) Then
                                    tempVal = Decimal.Parse(Indexs(i).Data2)
                                    Data2Added = True
                                End If
                            Case 4
                                If Not String.IsNullOrEmpty(Indexs(i).Data2) Then
                                    tempVal = Server.Con.ConvertDate(Indexs(i).Data2)
                                    Data2Added = True
                                End If
                            Case 5
                                If Not String.IsNullOrEmpty(Indexs(i).Data2) Then
                                    tempVal = Server.Con.ConvertDateTime(Indexs(i).Data2)
                                    Data2Added = True
                                End If
                            Case 6

                                If Not String.IsNullOrEmpty(Indexs(i).Data2) Then
                                    tempVal = CDec(Indexs(i).Data2)
                                    Data2Added = True
                                End If
                            Case 7
                                If Not String.IsNullOrEmpty(Indexs(i).Data2) Then
                                    FlagCase = True
                                    tempVal = _COMILLA_SIMPLE & LCase(DirectCast(Indexs(i).Data2, String)) & _COMILLA_SIMPLE
                                    Data2Added = True
                                End If
                            Case 8
                                If Not String.IsNullOrEmpty(Indexs(i).Data2) Then
                                    FlagCase = True
                                    tempVal = _COMILLA_SIMPLE & LCase(DirectCast(Indexs(i).Data2, String)) & _COMILLA_SIMPLE
                                    Data2Added = True
                                End If
                        End Select
                    Catch ex As Exception
                        Throw New Exception("Ocurrio un error al convertir al tipo de Dato: Dato: " & sbValue.ToString & ", Tipo Dato: " & Indexs(i).Type & _ESPACIO & ex.ToString)
                        Exit Sub
                    End Try

                    If Data2Added = True Then
                        ColumCondstring.Append(_ESP_PARENT_ABIERTO & Indexs(i).Column & " >= " & mainVal & ") AND " & Indexs(i).Column & " <= (" & tempVal & "))")
                        Data2Added = False
                    End If

                Case "Contiene"
                    While mainVal.Contains("  ")
                        mainVal = mainVal.Replace("  ", " ")
                    End While
                    mainVal = mainVal.Trim.Replace(" ", "%")

                    If FlagCase Then
                        ColumCondstring.Append(" (lower(" & Indexs(i).Column & ") Like '%" & Replace(Trim(mainVal), _COMILLA_SIMPLE, String.Empty) & "%')")
                    Else
                        ColumCondstring.Append(_ESP_PARENT_ABIERTO & Indexs(i).Column & " Like '%" & Replace(Trim(mainVal), _COMILLA_SIMPLE, String.Empty) & "%')")
                    End If

                Case "Empieza"
                    If FlagCase Then
                        ColumCondstring.Append(" (lower(" & Indexs(i).Column & ") Like '" & Replace(Trim(mainVal), _COMILLA_SIMPLE, String.Empty) & "%')")
                    Else
                        ColumCondstring.Append(_ESP_PARENT_ABIERTO & Indexs(i).Column & " Like '" & Replace(Trim(mainVal), _COMILLA_SIMPLE, String.Empty) & "%')")
                    End If

                Case "Termina"
                    If FlagCase Then
                        ColumCondstring.Append(" (lower(" & Indexs(i).Column & ") Like '%" & Replace(Trim(mainVal), _COMILLA_SIMPLE, String.Empty) & "')")
                    Else
                        ColumCondstring.Append(_ESP_PARENT_ABIERTO & Indexs(i).Column & " Like '%" & Replace(Trim(mainVal), _COMILLA_SIMPLE, String.Empty) & "')")
                    End If

                Case "Alguno"
                    Op = "LIKE"

                    If Indexs(i).DropDown = IndexAdditionalType.LineText Then
                        mainVal = mainVal.Replace(_PUNTO_COMA, _COMA)
                        mainVal = mainVal.Replace("  ", _ESPACIO)
                        mainVal = mainVal.Replace(_ESPACIO, _COMA)
                    End If

                    Dim SomeValues As Array = DirectCast(mainVal, String).Split(_COMA)
                    Dim x As Int32
                    Dim somestring As String = String.Empty
                    For x = 0 To SomeValues.Length - 1
                        Dim Val As String = SomeValues(x)
                        If IsNothing(Val) = False AndAlso Not String.IsNullOrEmpty(Val.Trim) Then
                            If ColumCondstring.Length = 0 AndAlso x = 0 Then
                                If FlagCase Then
                                    somestring = " (lower(" & Indexs(i).Column & ") " & Op & " ('%" & Val.Replace(_COMILLA_SIMPLE, String.Empty) & "%')"
                                Else
                                    somestring = _ESP_PARENT_ABIERTO & Indexs(i).Column & _ESPACIO & Op & " ('%" & Val.Replace(_COMILLA_SIMPLE, String.Empty) & "%')"
                                End If
                            ElseIf x = 0 Then
                                If FlagCase Then
                                    somestring &= separator & " (lower(" & Indexs(i).Column & ") " & Op & " ('%" & Val.Replace(_COMILLA_SIMPLE, String.Empty) & "%')"
                                Else
                                    somestring &= separator & _ESP_PARENT_ABIERTO & Indexs(i).Column & _ESPACIO & Op & " ('%" & Val.Replace(_COMILLA_SIMPLE, String.Empty) & "%')"
                                End If
                            ElseIf x > 0 Then
                                If String.IsNullOrEmpty(somestring) Then
                                    If FlagCase Then
                                        somestring &= separator & " lower(" & DirectCast(Indexs(i).Column, String) & ") " & Op & " ('%" & Replace(Val, _COMILLA_SIMPLE, String.Empty).Trim & "%')"
                                    Else
                                        somestring &= separator & _ESPACIO & DirectCast(Indexs(i).Column, String) & _ESPACIO & Op & " ('%" & Replace(Val, _COMILLA_SIMPLE, String.Empty).Trim & "%')"
                                    End If
                                Else
                                    separator = _OR
                                    If FlagCase Then
                                        somestring &= separator & " lower(" & Indexs(i).Column & ") " & Op & " ('%" & Replace(Val, _COMILLA_SIMPLE, String.Empty).Trim & "%')"
                                    Else
                                        somestring &= separator & _ESPACIO & Indexs(i).Column & _ESPACIO & Op & " ('%" & Replace(Val, _COMILLA_SIMPLE, String.Empty).Trim & "%')"
                                    End If
                                End If
                            End If
                        End If
                    Next
                    If Not String.IsNullOrEmpty(somestring) Then ColumCondstring.Append(somestring & _PARENTESIS_CERRADO)
                    SomeValues = Nothing
                    somestring = Nothing

                Case "Distinto"
                    Op = "<>"
                    Dim Val As String = DirectCast(mainVal, String)
                    If IsNothing(Val) = False AndAlso Not String.IsNullOrEmpty(Val.Trim) Then
                        If FlagCase Then
                            ColumCondstring.Append(" (lower(" & _NULLFUNCTION & Indexs(i).Column & ",'')) " & Op & " ('" & Val.Replace(_COMILLA_SIMPLE, String.Empty) & "'))")
                        Else
                            ColumCondstring.Append(_ESP_PARENT_ABIERTO & _NULLFUNCTION & Indexs(i).Column & ",'') " & Op & " ('" & Val.Replace(_COMILLA_SIMPLE, String.Empty) & "'))")
                        End If
                    End If

                Case "Dentro"
                    If FlagCase Then
                        ColumCondstring.Append(" (lower(" & Indexs(i).Column & ") in (" & mainVal & "'))")
                    Else
                        ColumCondstring.Append(_ESP_PARENT_ABIERTO & Indexs(i).Column & " in (" & mainVal & "))")
                    End If

                Case "SQL Sin Atributo"
                    ColumCondstring.Append(" (" & mainVal & ")")

                Case "SQL"
                    ColumCondstring.Append(_ESP_PARENT_ABIERTO & Indexs(i).Column & " in (" & mainVal & "))")

            End Select
            Op = Nothing
            separator = Nothing
        End If

        If Indexs(i).OrderSort <> OrderSorts.Ninguno Then
            Dim Sort As String = String.Empty
            If Indexs(i).OrderSort = OrderSorts.ASC Then Sort = " ASC "
            If Indexs(i).OrderSort = OrderSorts.DESC Then Sort = " DESC "
            If First Then
                Orderstring.Append("ORDER BY " & Indexs(i).Column & Sort)
                First = False
            Else
                Orderstring.Append(", " & Indexs(i).Column & Sort)
            End If
            Sort = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Verifica que columnas(que no sean atributos) debe ocultar.
    ''' </summary>
    ''' <param name="currentUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetHideColumns(ByVal UserId As Int64) As Dictionary(Of String, Boolean)
        Dim RiB As New RightsBusiness
        Dim dictionaryOfColumns As New Dictionary(Of String, Boolean)
        dictionaryOfColumns.Add("Version", RiB.GetUserRights(UserId, Zamba.ObjectTypes.Grids, Zamba.Core.RightsType.WebResultGridShowVersionColumn))
        dictionaryOfColumns.Add("VersionNumber", RiB.GetUserRights(UserId, Zamba.ObjectTypes.Grids, Zamba.Core.RightsType.WebResultGridShowVersionNumberColumn))
        dictionaryOfColumns.Add("CreateDate", RiB.GetUserRights(UserId, Zamba.ObjectTypes.Grids, Zamba.Core.RightsType.WebResultGridShowCreatedDateColumn))
        dictionaryOfColumns.Add("EntityName", RiB.GetUserRights(UserId, Zamba.ObjectTypes.Grids, Zamba.Core.RightsType.WebResultGridShowEntityNameColumn))
        dictionaryOfColumns.Add("IconName", RiB.GetUserRights(UserId, Zamba.ObjectTypes.Grids, Zamba.Core.RightsType.WebResultGridShowIconNameColumn))
        dictionaryOfColumns.Add("LastEdit", RiB.GetUserRights(UserId, Zamba.ObjectTypes.Grids, Zamba.Core.RightsType.WebResultGridShowLastEditDateColumn))
        dictionaryOfColumns.Add("OriginalName", RiB.GetUserRights(UserId, Zamba.ObjectTypes.Grids, Zamba.Core.RightsType.WebResultGridShowOriginalName))
        dictionaryOfColumns.Add("ResultName", RiB.GetUserRights(UserId, Zamba.ObjectTypes.Grids, Zamba.Core.RightsType.WebResultGridShowResultNameColumn))
        Return dictionaryOfColumns
    End Function

End Class
