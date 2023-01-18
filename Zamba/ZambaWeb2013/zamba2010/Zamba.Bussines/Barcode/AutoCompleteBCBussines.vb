Imports zamba.core
Imports Zamba.Data
Imports System.Collections.Generic
Imports System.Text

'Imports Zamba.Barcode.Factory
<Serializable()> Public Class AutocompleteBCBusiness
    Inherits ZClass

    Private _Index As Zamba.Core.Index
    ' Protected _Index As Zamba.Core.Index
    Private fila As Int16
    Protected DocTypeId As Int32
    Public ReadOnly Property Index() As Zamba.Core.Index
        Get
            Return _Index
        End Get
    End Property

#Region "Eventos"
    Public Event Creado()
#End Region

    Public Overrides Sub Dispose()
        _Index.Childs.Clear()
        _Index = Nothing
    End Sub



    'Public Shared Function ExecuteAutoComplete(ByRef docresult As IResult, ByRef ind As IIndex, ByRef frmGrilla As Form) As Hashtable


    '    ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo atributos para autocompletar")
    '    ZTrace.WriteLineIf(ZTrace.IsInfo, " del documento: " & docresult.Name & " (Id: " & docresult.ID.ToString & ")")
    '    'Obtiene el campo IndexKey relacionado con AutoComplete del primer
    '    'documento.
    '    Dim indexTemp As ArrayList
    '    indexTemp = AutoCompleteBarcode_FactoryBusiness.getIndexKeys(docresult.DocType.ID)
    '    'Obtiene una instancia del Objeto AutoComplete
    '    Dim AC As AutocompleteBC
    '    AC = AutoCompleteBarcode_Factory.GetComplete(docresult.DocType.ID, ind.ID)
    '    If Not IsNothing(AC) Then
    '        Dim RB As New Results_Business
    '        Try

    '            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo los datos para autocompletar en los atributos...")
    '            For Each intmp As Index In indexTemp
    '                intmp.DataTemp = RB.findIn(docresult.Indexs, intmp).DataTemp
    '            Next
    '            Return AC.Complete(docresult, indexTemp, frmGrilla)
    '        Finally
    '            AC.Dispose()
    '            AC = Nothing
    '            RB = Nothing
    '        End Try
    '    Else
    '        Return Nothing
    '    End If
    'End Function


    Public Shared Function ExecuteAutoComplete(ByVal entityId As Int64, ByVal attributeId As Int64, ByVal attributeValue As String) As Hashtable

        Dim indexTemp As ArrayList
        indexTemp = AutoCompleteBarcode_FactoryBusiness.getIndexKeys(entityId)

        Dim AC As AutocompleteBC = AutoCompleteBarcode_Factory.GetComplete(entityId, attributeId)
        If Not IsNothing(AC) Then
            Dim RB As New Results_Business
            Try

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo los datos para autocompletar en los atributos...")
                For Each intmp As Index In indexTemp
                    If intmp.ID = attributeId Then
                        intmp.DataTemp = attributeValue
                    End If
                Next
                Return AC.Complete(indexTemp)
            Finally
                AC.Dispose()
                AC = Nothing
                RB = Nothing
            End Try
        Else
            Return Nothing
        End If
    End Function


    '' -----------------------------------------------------------------------------
    '' <summary>
    ''     Recupera un conjunto de Atributos con sus respectivos valores de la 
    ''     base de datos.
    '' </summary>
    '' <param name="Result1">Documento actual</param>
    '' <param name="index">IndexKey para AutoComplete, la propiedad DataTemp
    ''  debe contener el valor del indice' </param>
    '' <returns>Documento actual con sus indices autocompletados</returns>
    '' <remarks>
    '' </remarks>
    '' <history>
    '' 	[oscar]	07/06/2006	Created
    '' </history>
    '' -----------------------------------------------------------------------------
    Public Function Complete(ByRef Result1 As Result, ByRef index As Index) As Result
        Dim ds As DataSet
        Dim col As Int16
        Dim ASB As New AutoSubstitutionBusiness

        ds = Data.BarcodeFactory.getsentencia(DocTypeId, index.DataTemp)
      

        'Disparo excepcion para que n0 refresque indices
        If ds.Tables(0).Rows.Count = 0 Then
            Return Nothing
        End If

        Dim i, j As Int16
        Dim dsindexs As DataSet

        'Recupera todos los indices del documento con sus respectivos valores
        dsindexs = Data.BarcodeFactory.GetDsIndexs(DocTypeId, True)

        'ds.WriteXml(Membership.MembershipHelper.StartUpPath & "\ds.xml")

        If ds.Tables(0).Rows.Count > 1 Then Me.GetData(ds)
        If ds.Tables(0).Rows.Count <> 0 Then  'ultimo agregado
            'Recorre todos los registros del conjunto de atributos obtenidos
            For i = 0 To dsindexs.Tables(0).Rows.Count - 1
                'Recorre todos los indices del conjunto de registros de documentos
                For j = 0 To Result1.Indexs.Count - 1
                    'Si el campo no es el IndexKey del AutoComplete 
                    If Not IsDBNull(dsindexs.Tables(0).Rows(i)("Clave")) AndAlso dsindexs.Tables(0).Rows(i)("Clave") <> 1 Then
                        'Verifica que el ID del indice del documento sea igual al id indice 
                        'del conjunto de atributos recuperados. De no coincidir los recorre uno a uno.
                        If Result1.Indexs(j).Id = dsindexs.Tables(0).Rows(i).Item(0) Then
                            ' Result1.Indexs(j).Data = ds.Tables(0).Rows(fila).Item(i)
                            ' Result1.Indexs(j).Datatemp = ds.Tables(0).Rows(fila).Item(i)
                            Try
                                If Not IsDBNull(dsindexs.Tables(0).Rows(i).Item(1)) Then col = dsindexs.Tables(0).Rows(i).Item(1)
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Result1.indexs(j).Data=" & ds.Tables(0).Rows(fila)(col))
                                'Completa el valor del Indice
                                If Not IsDBNull(ds.Tables(0).Rows(fila)(col)) Then
                                    Result1.Indexs(j).Data = ds.Tables(0).Rows(fila)(col)
                                    If DirectCast(Result1.Indexs(j), Index).DropDown = IndexAdditionalType.AutoSustitución OrElse DirectCast(Result1.Indexs(j), Index).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                        Result1.Indexs(j).dataDescription = ASB.getDescription(ds.Tables(0).Rows(fila)(col), DirectCast(Result1.Indexs(j), Index).ID)
                                    End If
                                End If
                            Catch ex As Exception
                                Zamba.Core.ZClass.raiseerror(ex)
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "ERROR al cargar el indice: " & Result1.Indexs(j).name & ", de la columna nro: " & col)
                            End Try
                            Try
                                If Not IsDBNull(ds.Tables(0).Rows(fila)(col)) Then
                                    Result1.Indexs(j).Datatemp = ds.Tables(0).Rows(fila)(col)
                                End If
                            Catch ex As Exception
                                Zamba.Core.ZClass.raiseerror(ex)
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "ERROR: Al cargar el indice (datatemp): " & Result1.Indexs(j).name & " ,de la columna nro: " & col)
                            End Try
                        End If
                        'Si el id del indice actual es igual al id del IndexKey Complete,
                        'Completa el valor del indice actual.
                        If Result1.Indexs(j).ID = index.ID Then Result1.Indexs(j).Data = index.DataTemp
                    End If
                Next
            Next
        End If
        ASB = Nothing
        Return Result1
    End Function



    ''' <summary>
    ''' Método que sirve para autocompletar los atributos
    ''' </summary>
    ''' <param name="Result1"></param>
    ''' 
    ''' <param name="indexs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Gaston]    05/11/2008  Created    Código original de complete, pero con segundo parámetro para aceptar una colección de atributos        
    '''     [Ezequiel]    29/04/2009  Modified    Se arreglo el codigo ya que no funcionaba
    ''' </history>
    Public Function Complete(ByRef Result1 As Result, ByRef indexs As ArrayList, ByRef frmGrilla As Form) As Hashtable
        Dim modifiedIndex As Hashtable = New Hashtable()
        Dim ds As DataSet
        Dim ASB As New AutoSubstitutionBusiness

        ds = Data.BarcodeFactory.getsentencia(DocTypeId, indexs)
        'Disparo excepcion para que n0 refresque indices
        If ds.Tables(0).Rows.Count = 0 Then
            Return Nothing
        End If

        Dim dsindexs As DataSet

        'Recupera todos los indices del documento con sus respectivos valores
        dsindexs = Data.BarcodeFactory.GetDsIndexs(DocTypeId, True)

        If ds.Tables(0).Rows.Count > 1 AndAlso Not frmGrilla Is Nothing Then
            DirectCast(frmGrilla, IfrmGrilla).DS = ds
            frmGrilla.ShowDialog()
            Me.fila = DirectCast(frmGrilla, IfrmGrilla).Id
        Else
            Me.fila = 0
        End If

        If ds.Tables(0).Rows.Count <> 0 Then  'ultimo agregado
            'Recorre todos los registros del conjunto de atributos obtenidos
            For Each r As DataRow In dsindexs.Tables(0).Rows
                'Recorre todos los indices del conjunto de registros de documentos
                If Not IsDBNull(r("Clave")) AndAlso r("Clave") <> 1 Then
                    'For j = 0 To Result1.Indexs.Count - 1
                    For Each ind As Index In Result1.Indexs
                        'Si el campo no es el IndexKey del AutoComplete 
                        'If Not IsDBNull(dsindexs.Tables(0).Rows(i)("Clave")) AndAlso dsindexs.Tables(0).Rows(i)("Clave") <> 1 Then
                        'Verifica que el ID del indice del documento sea igual al id indice 
                        'del conjunto de atributos recuperados.
                        'De no coincidir los recorre uno a uno.
                        If ind.ID = r.Item(0) Then
                          
                            Dim col As String = String.Empty
                            Try
                                'Se aplica performance de Strings
                                'ind.Data = ds.Tables(0).Rows(Me.fila)(r(3).ToString().Substring(1).Substring(0, r(3).ToString().Substring(1).Length - 1))
                                'ind.DataTemp = ds.Tables(0).Rows(Me.fila)(r(3).ToString().Substring(1).Substring(0, r(3).ToString().Substring(1).Length - 1))
                                If Not IsDBNull(ds.Tables(0).Rows(Me.fila)(r(3).substring(1, r(3).ToString.Length - 2))) Then
                                    col = r(3).substring(1, r(3).ToString.Length - 2)
                                    ind.Data = ds.Tables(0).Rows(Me.fila)(col)
                                    ind.DataTemp = ds.Tables(0).Rows(Me.fila)(col)
                                    If ind.Type = IndexDataType.Numerico OrElse ind.Type = IndexDataType.Numerico_Largo Then
                                        ind.Data = Int64.Parse(ind.Data)
                                        ind.DataTemp = Int64.Parse(ind.DataTemp)
                                    End If

                                    If ind.DropDown = IndexAdditionalType.AutoSustitución OrElse ind.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                        ind.dataDescription = ASB.getDescription(ind.Data, ind.ID)
                                        ind.dataDescriptionTemp = ASB.getDescription(ind.DataTemp, ind.ID)
                                    End If
                                Else
                                    ind.Data = String.Empty
                                    ind.DataTemp = String.Empty
                                End If
                                modifiedIndex.Add(ind.ID, ind)
                            Catch ex As Exception
                                Zamba.Core.ZClass.raiseerror(ex)
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "ERROR: Al cargar el indice (datatemp): " & ind.Name & " ,de la columna nro: " & col)
                            Finally
                                col = Nothing
                            End Try
                        Else
                            ind.SetData(ind.DataTemp)
                        End If
                    Next
                End If
            Next
        End If
        ASB = Nothing

        If modifiedIndex.Count > 0 Then
            Return modifiedIndex
        Else
            Return Nothing
        End If
    End Function

    Public Function Complete(ByVal indexs As ArrayList) As Hashtable
        Dim modifiedIndex As Hashtable = New Hashtable()
        Dim ds As DataSet
        Dim ASB As New AutoSubstitutionBusiness

        ds = Data.BarcodeFactory.GetSentencia(DocTypeId, indexs)

        If ds.Tables(0).Rows.Count = 0 Then
            Return Nothing
        End If

        Dim dsindexs As DataSet

        'Recupera todos los indices del documento con sus respectivos valores
        dsindexs = Data.BarcodeFactory.GetDsIndexs(DocTypeId, True)

        If ds.Tables(0).Rows.Count <> 0 Then  'ultimo agregado
            'Recorre todos los registros del conjunto de atributos obtenidos
            For Each r As DataRow In dsindexs.Tables(0).Rows
                'Recorre todos los indices del conjunto de registros de documentos
                If Not IsDBNull(r("Clave")) AndAlso r("Clave") <> 1 Then
                    'For j = 0 To Result1.Indexs.Count - 1
                    For Each ind As Index In indexs
                        'Si el campo no es el IndexKey del AutoComplete 
                        'If Not IsDBNull(dsindexs.Tables(0).Rows(i)("Clave")) AndAlso dsindexs.Tables(0).Rows(i)("Clave") <> 1 Then
                        'Verifica que el ID del indice del documento sea igual al id indice 
                        'del conjunto de atributos recuperados.
                        'De no coincidir los recorre uno a uno.
                        If ind.ID = r.Item(0) Then

                            Dim col As String = String.Empty
                            Try
                                'Se aplica performance de Strings
                                'ind.Data = ds.Tables(0).Rows(Me.fila)(r(3).ToString().Substring(1).Substring(0, r(3).ToString().Substring(1).Length - 1))
                                'ind.DataTemp = ds.Tables(0).Rows(Me.fila)(r(3).ToString().Substring(1).Substring(0, r(3).ToString().Substring(1).Length - 1))
                                If Not IsDBNull(ds.Tables(0).Rows(Me.fila)(r(3).substring(1, r(3).ToString.Length - 2))) Then
                                    col = r(3).substring(1, r(3).ToString.Length - 2)
                                    ind.Data = ds.Tables(0).Rows(Me.fila)(col)
                                    ind.DataTemp = ds.Tables(0).Rows(Me.fila)(col)
                                    If ind.DropDown = IndexAdditionalType.AutoSustitución OrElse ind.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                        ind.dataDescription = ASB.getDescription(ind.Data, ind.ID)
                                        ind.dataDescriptionTemp = ASB.getDescription(ind.DataTemp, ind.ID)
                                    End If
                                Else
                                    ind.Data = String.Empty
                                    ind.DataTemp = String.Empty
                                End If
                                modifiedIndex.Add(ind.ID, ind)
                            Catch ex As Exception
                                Zamba.Core.ZClass.raiseerror(ex)
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "ERROR: Al cargar el indice (datatemp): " & ind.Name & " ,de la columna nro: " & col)
                            Finally
                                col = Nothing
                            End Try
                        Else
                            ind.SetData(ind.DataTemp)
                        End If
                    Next
                End If
            Next
        End If
        ASB = Nothing

        If modifiedIndex.Count > 0 Then
            Return modifiedIndex
        Else
            Return Nothing
        End If
    End Function


    Protected Overridable Function GetData(ByVal ds As DataSet) As Int16
        'Dim grilla As New frmGrilla(ds)
        'RemoveHandler grilla.IdFila, AddressOf Me.getrow
        'AddHandler grilla.IdFila, AddressOf Me.getrow
        'grilla.ShowDialog()
    End Function


    Protected Sub getrow(ByVal id As Int16)
        Try
            Me.fila = id
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    '''' -----------------------------------------------------------------------------
    '''' <summary>
    '''' Construye y devuelve una consulta SQL.
    '''' </summary>
    '''' <param name="doctypeid"></param>
    '''' <param name="indexvalue"></param>
    '''' <returns></returns>
    '''' <remarks>
    '''' </remarks>
    '''' <history>
    '''' 	[Hernan]	26/05/2006	Created
    '''' </history>
    '''' -----------------------------------------------------------------------------



    Public Sub New()
        'Se instancia para el insertar, en el módulo de administrador
    End Sub
    Public Sub New(ByVal Dtid As Int32)
        Me.DocTypeId = Dtid
        Me._Index = New Zamba.Core.Index
        Me._Index = Data.BarcodeFactory.getIndexKey(Dtid)
    End Sub

End Class

Public Class AutoCompleteBarcode_FactoryBusiness
    Private Shared AC As AutocompleteBCBusiness




    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un Objeto AutoCompleteBC si hay configurado autocompletar en base al DocTypeID y al Indice seleccionado
    ''' </summary>
    ''' <param name="DocTypeID"></param>
    ''' <param name="IndexId"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Se debe evaluar si el resultado de esto es NOTHING
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetComplete(ByVal DocTypeID As Int32, ByVal IndexId As Int32) As AutocompleteBCBusiness
        If IsNothing(Data.BarcodeFactory.GetAutoIndexs(DocTypeID, IndexId)) Then
            Return Nothing
        Else
            AC = New AutocompleteBCBusiness(DocTypeID)
            Return AC
        End If
    End Function

    Public Shared Function getIndexKey(ByVal id As Int32) As Zamba.Core.Index
        Return Data.BarcodeFactory.getIndexKey(id)
    End Function

    ''' <summary>
    ''' Método que llama a un método que sirve para obtener los atributos clave
    ''' </summary>
    ''' <param name="id">Id del entidad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Gaston]    05/11/2008  Created     
    '''     Marcelo     22/06/2009  Modified    Se agrego un hash
    ''' </history>
    Public Shared Function getIndexKeys(ByVal id As Int64) As ArrayList

        If Cache.DocTypesAndIndexs.hsAutocompleteKeys.Contains(id) = False Then
                Try
                    Cache.DocTypesAndIndexs.hsAutocompleteKeys.Add(id, Data.BarcodeFactory.getIndexKeys(id))
                Catch ex As Exception
                    ZCore.raiseerror(ex)
                End Try
            End If
            Return Cache.DocTypesAndIndexs.hsAutocompleteKeys(id)

    End Function

    ''' <summary>
    '''  Ejecutar el autocompletar sobre todos los documentos que pertenecen a ese entidad
    ''' </summary>
    ''' <param name="doctypeid">Id del tipo de Documento</param>
    ''' <history>
    '''     Marcelo modified 16/12/2008
    '''     [Gaston]    22/12/2008  Modified
    '''     [Gaston]    23/12/2008  Modified
    '''<history>
    ''' <remarks></remarks>
    Public Shared Sub ExecuteAutocomplete(ByVal doctypeid As String, Optional ByVal FilterIndex As Dictionary(Of String, String) = Nothing)
        Dim BB As New BarcodesBusiness
        Dim dt As DataTable = BB.GetAutoCompleteIndexs(doctypeid)
        Dim dsValues As DataSet
        Dim sql As New StringBuilder
        Dim strInner As New StringBuilder
        Dim strWhere As New StringBuilder
        Dim strFilter As New StringBuilder
        Dim bolColum As Boolean = True
        Dim bolClave As Boolean = True
        Dim bolWhere As Boolean = True
        Dim lstColumnas As Dictionary(Of String, String) = New Dictionary(Of String, String)
        Dim lstGroupIndexs As Dictionary(Of String, Boolean) = New Dictionary(Of String, Boolean)
        Dim lstClaves As Dictionary(Of String, String) = New Dictionary(Of String, String)

        Try
            If Not (IsNothing(dt)) Then


                For Each row As DataRow In dt.Rows
                    'Armo el select
                    If Not row.Item("COLUMNA").ToString().Trim.Contains(" ") Then
                        row.Item("COLUMNA") = row.Item("COLUMNA").ToString.Replace(Chr(34), "")
                    End If
                    If (bolColum = True) Then
                        sql.Append("SELECT " & row.Item("TABLA") & "." & row.Item("COLUMNA"))
                        bolColum = False
                    Else
                        sql.Append(", " & row.Item("TABLA") & "." & row.Item("COLUMNA"))
                    End If

                    'Armo el join
                    If row.Item("Clave") = 1 Then
                        'Guardo los campos clave para utilizarlos mas adelante
                        lstClaves.Add(row.Item("INDEXID"), row.Item("COLUMNA"))
                        If bolClave = True Then
                            strInner.Append(" Inner Join Doc_I")
                            strInner.Append(doctypeid)
                            strInner.Append(" on ")
                            bolClave = False
                        Else
                            strInner.Append(" AND ")
                        End If
                        strInner.Append(row.Item("TABLA"))
                        strInner.Append(".")
                        strInner.Append(row.Item("COLUMNA"))
                        strInner.Append("= Doc_I")
                        strInner.Append(doctypeid)
                        strInner.Append(".I")
                        strInner.Append(row.Item("INDEXID"))
                    Else
                        'Guardo las columnas a actualizar para usarlas mas adelante
                        lstColumnas.Add(row.Item("INDEXID"), row.Item("COLUMNA"))
                        ' Se guarda el id del índice y si es o no un índice agrupado
                        If Not IsDBNull(row.Item("INDEXGROUP")) AndAlso Not String.IsNullOrEmpty(row.Item("INDEXGROUP")) Then
                            lstGroupIndexs.Add(row.Item("INDEXID"), row.Item("INDEXGROUP"))
                        Else
                            lstGroupIndexs.Add(row.Item("INDEXID"), 0)
                        End If
                    End If

                    If Not IsDBNull(row.Item("wherecondition")) AndAlso String.IsNullOrEmpty(row.Item("wherecondition").ToString().Trim) = False Then
                        If bolWhere = True Then
                            bolWhere = False
                            strWhere.Append(" where ")
                        Else
                            strWhere.Append(" and ")
                        End If
                        strWhere.Append(row.Item("wherecondition"))
                    End If
                Next

                sql.Append(" FROM " & dt.Rows(0).Item("TABLA"))
                sql.Append(strInner.ToString())

                '[AlejandroR] (WI 4093) - Created - 18/01/10
                'Filtro por indice
                If Not FilterIndex Is Nothing Then
                    Dim i As Integer
                    If FilterIndex.Count > 0 Then
                        For Each indice As KeyValuePair(Of String, String) In FilterIndex
                            strFilter.Append("Doc_I" & doctypeid & ".I" & indice.Key & " = '" & indice.Value & "' ")
                            If i < FilterIndex.Count - 1 Then
                                strFilter.Append(" AND ")
                            End If
                            i = i + 1
                        Next
                    End If

                    If Not String.IsNullOrEmpty(strFilter.ToString()) Then
                        If String.IsNullOrEmpty(strWhere.ToString()) Then
                            strWhere.Append(" where ")
                            strWhere.Append(strFilter.ToString())
                        Else
                            strWhere.Append(" AND ( ")
                            strWhere.Append(strFilter.ToString())
                            strWhere.Append(" ) ")
                        End If
                    End If

                End If

                sql.Append(strWhere.ToString())

                sql.Append(" ORDER BY ")
                bolClave = True
                For Each kvClave As KeyValuePair(Of String, String) In lstClaves
                    If bolClave = True Then
                        bolClave = False
                    Else
                        sql.Append(", ")
                    End If
                    sql.Append(kvClave.Value)
                Next
            End If

            'Obtengo todos los valores a actualizar
            dsValues = Zamba.Servers.Server.Con.ExecuteDataset(CommandType.Text, sql.ToString())

            If Not (IsNothing(dsValues)) Then

                If (dsValues.Tables.Count > 0) Then

                    ' Colección que se utiliza para almacenar los valores de un id o más ids y que servira en el For Each row as DataRow ...
                    Dim keysValues As New List(Of String)

                    ' Ejemplo: Si en dsValues.Tables(0) se tiene lo siguiente:

                    ' IdAuto	Modelo
                    ' 100		1234
                    ' 100		1370
                    ' 200       5555

                    ' Se recorre fila por fila. La primera fila tiene IdAuto = 100. Se realiza todo un procesamiento para esta fila. Después la
                    ' segunda fila también tiene IdAuto = 100, por lo tanto es necesario evitar el procesamiento que se realizo para la primera
                    ' fila, ya que no tiene sentido volver a hacer lo mismo. Pero el procesamiento si se realiza para la tercera fila porque 
                    ' tiene otro id. De esta forma todo el procesamiento sólo se realiza para un id y se evita hacerlo en una fila con igual id

                    Dim blnContinue As Boolean = True

                    Dim filter As String = ""

                    For Each row As DataRow In dsValues.Tables(0).Rows

                        If (keysValues.Count > 0) Then
                            verifyIfRowValuesKeyConstainsInkeysValues(row, blnContinue, keysValues, lstClaves)
                        End If

                        If (blnContinue = True) Then

                            filter = Nothing
                            Dim table As DataTable = filterTable(row, dsValues.Tables(0), lstClaves, filter)

                            ' Si la tabla tiene una fila, entonces el id es único. Sino si tiene más de una fila entonces el id o ids se repiten
                            If (table.Rows.Count = 1) Then
                                updateIndexsWithSingleKeys(row, doctypeid, lstColumnas, lstClaves)
                            ElseIf (table.Rows.Count > 1) Then
                                updateIndexsWithSameKeys(row, doctypeid, lstColumnas, lstGroupIndexs, lstClaves, table, keysValues, filter)
                            End If

                        End If

                        blnContinue = True

                    Next

                End If

            End If


        Finally
            'Limpio las variables utilizadas
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            If Not IsNothing(dsValues) Then
                dsValues.Dispose()
                dsValues = Nothing
            End If
            sql = Nothing
            strInner = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Método que sirve para verificar si el valor o valores de id se encuentran en la colección de valores de ids. Si están no se actualiza
    ''' la tabla porque no tiene sentido, ya que se realizo la primera vez
    ''' </summary>
    ''' <param name="row">Fila que contiene los atributos con los nuevos valores</param>
    ''' <param name="blnContinue">Bandera que indica si hay que actualizar o no la base de datos</param>
    ''' <param name="keysValues">Colección que contiene los valores de ids</param>
    ''' <param name="lstClaves">Colección que contiene las columnas claves</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	22/12/2008  Created    
    ''' </history>
    Private Shared Sub verifyIfRowValuesKeyConstainsInkeysValues(ByRef row As DataRow, ByRef blnContinue As Boolean, ByRef keysValues As List(Of String), _
                                                                 ByRef lstClaves As Dictionary(Of String, String))

        Dim test As String = Nothing

        Try

            For Each kvClave As KeyValuePair(Of String, String) In lstClaves
                test = test + " " & row(kvClave.Value.Replace("[", "").Replace("]", "")).ToString()
            Next

            test = test.Trim()

            If (keysValues.Contains(test)) Then
                blnContinue = False
            End If

        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
        Finally
            test = Nothing
        End Try

    End Sub

    ''' <summary>
    ''' Método que sirve para filtrar las filas que contienen uno o más ids, en base al id o ids de row
    ''' </summary>
    ''' <param name="row">Fila que contiene los atributos con los nuevos valores</param>
    ''' <param name="data">Tabla que contiene todos los atributos que se deben actualizar</param>
    ''' <param name="lstClaves">Colección que contiene las columnas claves</param>
    ''' <param name="filter">String que contendrá los valores de los ids. Ejemplo: Si IdAuto es clave y su valor es 100, entonces se guarda 100</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	22/12/2008  Created    
    ''' </history>
    Private Shared Function filterTable(ByRef row As DataRow, ByRef data As DataTable, ByRef lstClaves As Dictionary(Of String, String), ByRef filter As String) As DataTable

        Dim query As New StringBuilder
        Dim counter As Short = 0

        Try

            For Each kvClave As KeyValuePair(Of String, String) In lstClaves

                If (counter > 0) Then
                    query.Append(" AND ")
                End If

                ' Nombre de columna
                query.Append(kvClave.Value.Replace("[", "").Replace("]", ""))
                query.Append(" = ")
                If IsNumeric(row(kvClave.Value.Replace("[", "").Replace("]", ""))) Then
                    query.Append(row(kvClave.Value.Replace("[", "").Replace("]", "")))
                Else
                    query.Append("'" & row(kvClave.Value.Replace("[", "").Replace("]", "")) & "'")
                End If
                counter = counter + 1

                filter = filter + " " & row(kvClave.Value.Replace("[", "").Replace("]", "")).ToString()

            Next

            filter = filter.Trim()
            Dim view As New DataView(data)
            view.RowFilter = query.ToString()
            Return (view.ToTable())

        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
        Finally
            query = Nothing
            counter = Nothing
        End Try

    End Function

    ''' <summary>
    ''' Método que sirve para actualizar los atributos que tengan uno o varios ids únicos, es decir, que sólo se encuentran en una fila
    ''' </summary>
    ''' <param name="row">Fila que contiene los atributos con los nuevos valores</param>
    ''' <param name="doctypeid">Id de un entidad</param>
    ''' <param name="lstColumnas">Colección que contiene las columnas no claves</param>
    ''' <param name="lstClaves">Colección que contiene las columnas claves</param>
    ''' <remarks>String que contendrá los valores de ids que se repiten</remarks>
    ''' <history>
    ''' 	[Gaston]	22/12/2008  Created       El código original estaba en el método "ExecuteAutocomplete"
    ''' </history>
    Private Shared Sub updateIndexsWithSingleKeys(ByRef row As DataRow, ByRef doctypeid As String, _
                                                  ByRef lstColumnas As Dictionary(Of String, String), ByRef lstClaves As Dictionary(Of String, String))

        Dim querybuilder As StringBuilder = New StringBuilder()

        Try

            querybuilder.Append("Update Doc_I")
            querybuilder.Append(doctypeid)
            querybuilder.Append(" Set ")

            Dim bolColum As Boolean = True
            Dim IB As New IndexsBusiness
            'Armo el update
            For Each kvColumna As KeyValuePair(Of String, String) In lstColumnas

                If bolColum = True Then
                    bolColum = False
                Else
                    querybuilder.Append(", ")
                End If

                querybuilder.Append("I")
                querybuilder.Append(kvColumna.Key)
                querybuilder.Append(" = ")

                If CInt(IB.GetIndexById(kvColumna.Key).Tables(0).Rows(0)("index_type").ToString().Trim()) = IndexDataType.Fecha Then
                    querybuilder.Append(Servers.Server.Con.ConvertDate(row(kvColumna.Value.Replace("[", "").Replace("]", ""))))
                ElseIf CInt(IB.GetIndexById(kvColumna.Key).Tables(0).Rows(0)("index_type").ToString().Trim()) = IndexDataType.Fecha_Hora Then
                    querybuilder.Append(Servers.Server.Con.ConvertDateTime(row(kvColumna.Value.Replace("[", "").Replace("]", "")).ToString))
                Else
                    querybuilder.Append("'" & row(kvColumna.Value.Replace("[", "").Replace("]", "")))
                    querybuilder.Append("'")
                End If

            Next
            IB = Nothing
            querybuilder.Append(" where ")

            Dim bolClave As Boolean = True

            'armo el where
            For Each kvClave As KeyValuePair(Of String, String) In lstClaves

                If bolClave = True Then
                    bolClave = False
                Else
                    querybuilder.Append(" and ")
                End If

                querybuilder.Append("I")
                querybuilder.Append(kvClave.Key)
                querybuilder.Append(" = ")
                querybuilder.Append("'" & row(kvClave.Value.Replace("[", "").Replace("]", "")) & "'")

            Next

            ZTrace.WriteLineIf(ZTrace.IsInfo, querybuilder.ToString())
            'Actualizo los indices del documento
            Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, querybuilder.ToString())

        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
        Finally
            querybuilder = Nothing
        End Try

    End Sub

    ''' <summary>
    ''' Método que sirve para actualizar los atributos que tengan uno o varios ids repetidos, es decir, cuyos ids se encuentran en más de una fila
    ''' </summary>
    ''' <param name="row">Fila que contiene los atributos con los nuevos valores</param>
    ''' <param name="doctypeid">Id de un entidad</param>
    ''' <param name="lstColumnas">Colección que contiene las columnas no claves</param>
    ''' <param name="lstGroupIndexs">Colección que contiene los id de las columnas no claves y si son o no atributos agrupados</param>
    ''' <param name="lstClaves">Colección que contiene las columnas claves</param>
    ''' <param name="table">Tabla que contiene todas las filas que tienen ese o esos ids</param>
    ''' <param name="keysValues">Colección que contiene los valores de los ids que se repiten. Ejemplo: 100 (cuya clave sería IdAuto)</param>
    ''' <param name="filter">Valores de ids que se repiten</param>
    ''' <param name=""></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	22/12/2008  Created    
    '''                 23/12/2008  Modified
    ''' </history>
    Private Shared Sub updateIndexsWithSameKeys(ByRef row As DataRow, ByRef doctypeid As String, ByRef lstColumnas As Dictionary(Of String, String), _
                                                ByRef lstGroupIndexs As Dictionary(Of String, Boolean), ByRef lstClaves As Dictionary(Of String, String), _
                                                ByRef table As DataTable, ByRef keysValues As List(Of String), ByRef filter As String)

        Dim querybuilder As New StringBuilder()
        Dim blnBan As Boolean = True

        Try

            ' Se agrega a la colección el valor del id, o los valores de cada id
            keysValues.Add(filter)

            querybuilder.Append("Update Doc_I")
            querybuilder.Append(doctypeid)
            querybuilder.Append(" Set ")
            Dim IB As New IndexsBusiness
            For Each kvColumna As KeyValuePair(Of String, String) In lstColumnas

                Dim indexValues As New StringBuilder

                ' Si el índice es agrupado entonces se concatenan los valores que haya en cada fila de la columna. Sino se selecciona el primer valor
                If (lstGroupIndexs.Item(kvColumna.Key) = True) Then

                    ' Ejemplo: Si tengo en table:

                    ' IdAuto    Modelo
                    ' 100		1234
                    ' 100		1370

                    ' Se debería colocar en la columna 'Modelo' de la base de datos el siguiente valor: '1234 1370'

                    For Each row2 As DataRow In table.Rows
                        indexValues.Append(row2(kvColumna.Value.Replace("[", "").Replace("]", "")))
                        indexValues.Append(" ")
                    Next

                Else
                    indexValues.Append(table.Rows(0).Item(kvColumna.Value.Replace("[", "").Replace("]", "")))
                End If

                If (blnBan = False) Then
                    querybuilder.Append(", ")
                End If

                querybuilder.Append("I")
                querybuilder.Append(kvColumna.Key)
                querybuilder.Append(" = ")
                If CInt(IB.GetIndexById(kvColumna.Key).Tables(0).Rows(0)("index_type").ToString().Trim()) = IndexDataType.Fecha Then
                    querybuilder.Append(Servers.Server.Con.ConvertDate(indexValues.ToString().Trim()))
                ElseIf CInt(IB.GetIndexById(kvColumna.Key).Tables(0).Rows(0)("index_type").ToString().Trim()) = IndexDataType.Fecha_Hora Then
                    querybuilder.Append(Servers.Server.Con.ConvertDateTime(indexValues.ToString().Trim()))
                Else
                    querybuilder.Append("'" & indexValues.ToString().Trim())
                    querybuilder.Append("' ")
                End If

                blnBan = False
                indexValues = Nothing

            Next
            IB = Nothing
            blnBan = True

            ' Se arma el where
            querybuilder.Append(" Where ")

            For Each kvClave As KeyValuePair(Of String, String) In lstClaves

                If (blnBan = False) Then
                    querybuilder.Append(" AND ")
                End If

                querybuilder.Append("I")
                querybuilder.Append(kvClave.Key)
                querybuilder.Append(" = ")
                querybuilder.Append("'" & row(kvClave.Value.Replace("[", "").Replace("]", "")) & "'")

                blnBan = False

            Next

            ZTrace.WriteLineIf(ZTrace.IsInfo, querybuilder.ToString())
            'Se actualizan los indices del documento
            Zamba.Servers.Server.Con.ExecuteNonQuery(CommandType.Text, querybuilder.ToString())

        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
        Finally
            querybuilder = Nothing
        End Try

    End Sub

End Class
