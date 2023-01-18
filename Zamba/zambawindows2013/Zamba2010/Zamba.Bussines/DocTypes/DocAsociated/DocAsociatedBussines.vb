Imports Zamba.Core.Search
Imports Zamba.Data
Imports System.Collections.Generic

Namespace DocTypes.DocAsociated
    Public Class DocAsociatedBusiness
        Inherits ZClass


#Region "Metodos Privados"
        ''' <summary>
        ''' Método que sirve para obtener los tipos de documento asociados a un entidad en particular
        ''' </summary>
        ''' <param name="docType">Entidad</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Gaston]	29/12/2008	Created
        '''     [Tomas]     04/08/2010  Modified    Se agrega el uso de hashtable
        ''' </history>
        Public Shared Function getDocTypesIdsAsociatedToDocType(ByVal docType As IDocType) As Generic.List(Of Int64)
            'Verifica si existe la asociacion de alguno de las 2 formas posibles. 
            If Not Cache.DocTypesAndIndexs.hsDocAsociationsIds.ContainsKey(docType.ID) Then
                Cache.DocTypesAndIndexs.hsDocAsociationsIds.Add(docType.ID, ResultsAsociatedFactory.getDocTypesIdsAsociatedToDocType(docType))
            End If
            Return Cache.DocTypesAndIndexs.hsDocAsociationsIds.Item(docType.ID)
        End Function

        ''' <summary>
        ''' Método que sirve para obtener los atributos asociados
        ''' </summary>
        ''' <param name="docType1">Entidad primario</param>
        ''' <param name="docType2">Entidad asociado</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Gaston]	29/12/2008	Created
        '''     [Tomas]     04/08/2010  Modified    Se agrega el uso de hashtable
        '''     [Tomas]     18/04/2011  Modified    Se comenta la busqueda por clave "dtid2 - dtid1".
        '''                                         Siempre debe ser "dtid1 - dtid2".
        ''' </history>
        Public Shared Function GetAsociations(ByVal docType1 As IDocType, ByVal docType2 As IDocType) As Generic.List(Of Asociados)
            'Verifica si existe la asociacion de alguno de las 2 formas posibles. 
            If Cache.DocTypesAndIndexs.hsDocAsociations.ContainsKey(docType1.ID & "-" & docType2.ID) Then
                Return Cache.DocTypesAndIndexs.hsDocAsociations.Item(docType1.ID & "-" & docType2.ID)
            Else
                Cache.DocTypesAndIndexs.hsDocAsociations.Add(docType1.ID & "-" & docType2.ID, ResultsAsociatedFactory.getAsociations(docType1, docType2))
                Return Cache.DocTypesAndIndexs.hsDocAsociations.Item(docType1.ID & "-" & docType2.ID)
            End If
        End Function

        ''' <summary>
        ''' Método que sirve para obtener los documentos asociados a la búsqueda
        ''' </summary>
        ''' <param name="result">Documento original</param>
        ''' <param name="IndexsAsociated">Atributos asociados</param>
        ''' <param name="docTypeAsociatedId">Entidad asociado</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Gaston]	29/12/2008	Created     Código original tomado de otro método
        '''     [Gaston]	30/12/2008	Modified    Se agrego el parámetro docTypeAsociated que indica la entidad asociado y se retorna 
        '''                                         nothing en caso de que indexs sea cero
        ''' </history>
        Private Shared Function getSearchAsociatedResultsAsDT(ByVal result As IResult, ByRef IndexsAsociated As Generic.List(Of Asociados), ByRef docTypeAsociatedId As Int64, ByVal PageSize As Int32) As DataTable
            Dim indexs As New Generic.List(Of IIndex)

            For Each associate As Asociados In IndexsAsociated
                For Each i As Index In result.Indexs
                    If i.ID = associate.Index1.ID AndAlso Not String.IsNullOrEmpty(i.Data) Then
                        associate.Index2.Data = i.Data
                        If i.DropDown = IndexAdditionalType.AutoSustitución Then
                            If String.IsNullOrEmpty(i.dataDescription) AndAlso String.IsNullOrEmpty(i.Data) = False Then
                                associate.Index2.dataDescription = AutoSubstitutionBusiness.getDescription(i.Data, i.ID, False)
                            Else
                                associate.Index2.dataDescription = i.dataDescription
                            End If
                        End If
                        associate.Index2.DataTemp = i.Data
                        indexs.Add(associate.Index2)
                        Exit For
                    End If
                Next
            Next

            Dim IndexsAsociatedaux As New Generic.List(Of Asociados)
            For Each associate As Asociados In IndexsAsociated
                If Not String.IsNullOrEmpty(associate.Index1.Data) Or Not String.IsNullOrEmpty(associate.Index2.Data) Then
                    IndexsAsociatedaux.Add(associate)
                End If
            Next
            IndexsAsociated = IndexsAsociatedaux
            If (indexs.Count > 0) Then

                '        'se obtienen los permisos de visualizacion de atributos para la grilla de asociados
                Dim strTable As String = Results_Factory.MakeTable(docTypeAsociatedId, Results_Factory.TableType.Full)
                Dim NonvisibleIndexs As List(Of Int64) = IndexsBusiness.GetAssociateDocumentIndexsRightsNonvisibleIndexs(docTypeAsociatedId, strTable, Membership.MembershipHelper.CurrentUser.ID)

                Dim docTypeAsociated As IDocType = DocTypesBusiness.GetDocType(docTypeAsociatedId, True)
                'Si es el mismo entidad, salteo este documento para que no lo muestre
                If result.DocTypeId = docTypeAsociatedId Then
                    Return ModDocuments.SearchRows(docTypeAsociated, indexs, UserBusiness.Rights.CurrentUser.ID, New Filters.FiltersComponent, 0, PageSize, False, FilterTypes.Asoc, Nothing, String.Empty, String.Empty, String.Empty, result.ID, NonvisibleIndexs)
                Else
                    Return ModDocuments.SearchRows(docTypeAsociated, indexs, UserBusiness.Rights.CurrentUser.ID, New Filters.FiltersComponent, 0, PageSize, False, FilterTypes.Asoc, Nothing, String.Empty, String.Empty, String.Empty, 0, NonvisibleIndexs)
                End If
            Else
                Return New DataTable
            End If
        End Function



        ''' <summary>
        ''' Método que sirve para obtener los documentos asociados a la búsqueda
        ''' </summary>
        ''' <param name="result">Documento original</param>
        ''' <param name="IndexsAsociated">Atributos asociados</param>
        ''' <param name="docTypeAsociated">Entidad asociado</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Gaston]	29/12/2008	Created     Código original tomado de otro método
        '''     [Gaston]	30/12/2008	Modified    Se agrego el parámetro docTypeAsociated que indica la entidad asociado y se retorna 
        '''                                         nothing en caso de que indexs sea cero
        ''' </history>
        Private Shared Function getSearchAsociatedResults(ByVal result As IResult, ByRef IndexsAsociated As Generic.List(Of Asociados), ByRef docTypeAsociated As Int64) As ArrayList
            Dim indexs As New Generic.List(Of IIndex)

            For Each associate As Asociados In IndexsAsociated
                For Each i As Index In result.Indexs
                    If i.ID = associate.Index1.ID AndAlso Not String.IsNullOrEmpty(i.Data) Then
                        associate.Index2.Data = i.Data
                        If i.DropDown = IndexAdditionalType.AutoSustitución Then
                            If String.IsNullOrEmpty(i.dataDescription) AndAlso String.IsNullOrEmpty(i.Data) = False Then
                                associate.Index2.dataDescription = AutoSubstitutionBusiness.getDescription(i.Data, i.ID, False)
                            Else
                                associate.Index2.dataDescription = i.dataDescription
                            End If
                        End If
                        associate.Index2.DataTemp = i.Data
                        indexs.Add(associate.Index2)
                        Exit For
                    End If
                Next
            Next

            Dim IndexsAsociatedaux As New Generic.List(Of Asociados)
            For Each associate As Asociados In IndexsAsociated
                If Not String.IsNullOrEmpty(associate.Index1.Data) Or Not String.IsNullOrEmpty(associate.Index2.Data) Then
                    IndexsAsociatedaux.Add(associate)
                End If
            Next
            IndexsAsociated = IndexsAsociatedaux
            If (indexs.Count > 0) Then
                Dim doctypes = New List(Of IDocType)
                Dim itemsdoctypes As IDocType = DocTypesFactory.GetDocType(docTypeAsociated)

                doctypes.Add(itemsdoctypes)
                Dim search As New Searchs.Search(indexs, String.Empty, doctypes, False, String.Empty)
                Dim PageSize As Integer = Int32.Parse(UserPreferences.getValue("CantidadFilas", Sections.UserPreferences, 100))
                Dim dt As DataTable = ModDocuments.DoSearch(search, UserBusiness.Rights.CurrentUser.ID, New Filters.FiltersComponent, 0, PageSize, False, True, FilterTypes.Asoc, False)
                Dim results As New ArrayList()
                If Not IsNothing(dt) Then
                    For Each row As DataRow In dt.Rows
                        Dim doctypeid As Int64 = CInt(row("doc_type_id"))

                        'Por ahora se implementa que vaya a la base a buscar el doc_type, hasta que se implemente la opcion de clonado
                        Dim r As Result = New Result(CInt(row("doc_id")), DocTypesBusiness.GetDocType(doctypeid, True), row(GridColumns.ORIGINAL_FILENAME_COLUMNNAME).ToString(), 0)
                        Results_Business.CompleteDocument(r, row)
                        results.Add(r)
                    Next
                End If
                Return results
            Else
                Return (Nothing)
            End If
        End Function

        ''' <summary>
        ''' Método que sirve para obtener los documentos asociados a la búsqueda
        ''' </summary>
        ''' <param name="result">Documento original</param>
        ''' <param name="IndexsAsociated">Atributos asociados</param>
        ''' <param name="docTypeAsociated">Entidad asociado</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Gaston]	29/12/2008	Created     Código original tomado de otro método
        '''     [Gaston]	30/12/2008	Modified    Se agrego el parámetro docTypeAsociated que indica la entidad asociado y se retorna 
        '''                                         nothing en caso de que indexs sea cero
        '''     [Marcelo]   26/08/2009  Modified    Devuelve un datatable en lugar de un arraylist
        '''     [Javier]    22/10/2010  Modified    Se agrega datadecription al indice asociado para usar al armar la query para traer asociados
        ''' </history>
        Private Shared Function getSearchAsociatedResultsDT(ByVal result As IResult,
                                                            ByRef IndexsAsociated As Generic.List(Of Asociados),
                                                            ByVal docTypeAsociatedId As Int64,
                                                            ByVal lastPage As Int64,
                                                            ByVal blnOpen As Boolean,
                                                            ByRef FC As IFiltersComponent) As DataTable

            Dim indexs As New Generic.List(Of IIndex)
            Dim PageSize As Integer = Integer.Parse(UserPreferences.getValue("CantidadFilas", Sections.UserPreferences, 100))

            For Each associate As Asociados In IndexsAsociated
                For Each i As Index In result.Indexs
                    If i.ID = associate.Index1.ID AndAlso Not String.IsNullOrEmpty(i.Data) Then
                        associate.Index2.Data = i.Data
                        associate.Index2.DataTemp = i.Data
                        associate.Index2.dataDescription = i.dataDescription
                        associate.Index2.dataDescriptionTemp = i.dataDescriptionTemp
                        indexs.Add(associate.Index2)
                        Exit For
                    End If
                Next
            Next

            Dim IndexsAsociatedaux As New Generic.List(Of Asociados)
            For Each associate As Asociados In IndexsAsociated
                If Not String.IsNullOrEmpty(associate.Index1.Data) Or Not String.IsNullOrEmpty(associate.Index2.Data) Then
                    IndexsAsociatedaux.Add(associate)
                End If
            Next
            IndexsAsociated = IndexsAsociatedaux
            If (indexs.Count > 0) Then
                Dim doctypes = New List(Of IDocType)
                Dim docTypeAsociated As IDocType = DocTypesBusiness.GetDocType(docTypeAsociatedId, True)
                doctypes.Add(docTypeAsociated)
                Dim search As New Searchs.Search(indexs, String.Empty, doctypes, False, String.Empty)
                Dim visibleIndexs As List(Of Int64) = New List(Of Int64)

                Dim AIR As Hashtable = RightsBusiness.GetAssociatedIndexsRightsCombined(result.DocTypeId, docTypeAsociatedId, Membership.MembershipHelper.CurrentUser.ID, True)


                For Each CurrentIndexID As Int64 In IndexsBusiness.GetIndexsIdsByDocTypeId(docTypeAsociatedId)
                    Dim IR As AssociatedIndexsRightsInfo = DirectCast(AIR(CurrentIndexID), AssociatedIndexsRightsInfo)

                    If IR.GetIndexRightValue(RightsType.AssociateIndexView) Then
                        visibleIndexs.Add(CurrentIndexID)
                    End If
                Next

                If Not IsNothing(FC) Then
                    Return ModDocuments.DoSearch(search, UserBusiness.Rights.CurrentUser.ID, FC, lastPage, PageSize, Not blnOpen, True, FilterTypes.Asoc, True, visibleIndexs)
                Else
                    Return ModDocuments.DoSearch(search, UserBusiness.Rights.CurrentUser.ID, New Filters.FiltersComponent, lastPage, PageSize, Not blnOpen, True, FilterTypes.Asoc, False, visibleIndexs)
                End If
            Else
                Return Nothing
            End If
        End Function
#End Region

#Region "Metodos Publicos"

#Region "Get"

        Public Shared Function AreResultsAsociated(ByVal Result1 As Result, ByVal Result2 As Result) As Boolean
            Dim Asociated As Boolean
            If (IsNothing(Result1) = False AndAlso IsNothing(Result2) = False) Then

                ' Se obtienen los atributos asociados
                Dim IndexsAsociated As Generic.List(Of Asociados) = GetAsociations(Result1.DocType, Result2.DocType)
                If IsNothing(IndexsAsociated) = False Then

                    For Each associate As Asociados In IndexsAsociated
                        For Each i1 As Index In Result1.Indexs
                            If (i1.ID = associate.Index1.ID AndAlso Not String.IsNullOrEmpty(i1.Data)) Then
                                For Each i2 As Index In Result2.Indexs
                                    If (i2.ID = associate.Index2.ID AndAlso Not String.IsNullOrEmpty(i2.Data)) Then
                                        If i2.Data <> i1.Data Then
                                            Return False
                                        Else
                                            Asociated = True
                                            Exit For
                                        End If
                                    End If
                                Next
                                Exit For
                            End If
                        Next
                    Next

                End If
            End If
            Return Asociated
        End Function
        Public Shared Function GetDocAsoc(ByVal FolderId As Int32, ByVal ArrayDocTypes As ArrayList) As ArrayList
            Return DocAsociatedFactory.GetDocAsoc(FolderId, ArrayDocTypes)
        End Function
        Public Shared Function GetDocAsociatedCount(ByVal DocTypeId As Int64) As Int16
            Return DocAsociatedFactory.GetDocAsociatedCount(DocTypeId)
        End Function
        Public Shared Function getAsociatedFormId(ByVal docTypeId As Integer) As Integer
            Return (DocAsociatedFactory.getAsociatedFormId(docTypeId))
        End Function
        ''' <summary>
        ''' Método que sirve para recuperar varios FormsId (si es que hay, si hay uno se retornara uno, sino cero rows)
        ''' en base a un docTypeId
        ''' </summary>
        ''' <param name="docTypeId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' 	[Gaston]	08/07/2008	Created
        ''' </history>
        Public Shared Function getAsociatedFormsId(ByVal docTypeId As Integer) As DataSet
            Return (DocAsociatedFactory.getAsociatedFormsId(docTypeId))
        End Function
        ''' <summary>
        ''' Busca por asociados y devuelve un datatable
        ''' </summary>
        ''' <param name="Result"></param>
        ''' <param name="PageSize"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function getAsociatedResultsFromResultAsDT(ByVal Result As IResult, ByVal PageSize As Int32, ByVal docTypesIDs As List(Of String)) As DataTable
            If (IsNothing(Result) = False) Then
                Dim _results As New DataTable

                ' Se obtienen los tipos de documento asociados 
                Dim DocTypesAsociated As Generic.List(Of Int64) = getDocTypesIdsAsociatedToDocType(Result.DocType)
                Dim indexsAsociated As Generic.List(Of Asociados)

                ' Por cada entidad asociado
                For Each DT2 As Int64 In DocTypesAsociated
                    Try
                        If docTypesIDs.Count = 0 Or docTypesIDs.Contains(DT2) Then
                            ' Se obtienen los atributos asociados
                            Dim docTypeAsociated As IDocType = DocTypesBusiness.GetDocType(DT2, True)

                            indexsAsociated = GetAsociations(Result.DocType, docTypeAsociated)
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("Se obtiene atributos asociados{0} en la coleccion", Result))
                            If Not IsNothing(indexsAsociated) Then
                                ' Se obtienen los documentos asociados a la búsqueda
                                _results.Merge(getSearchAsociatedResultsAsDT(Result, indexsAsociated, DT2, PageSize))
                                _results.PrimaryKey = New DataColumn() {_results.Columns("DOC_ID")}
                            End If
                        End If
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    Finally
                        If Not IsNothing(indexsAsociated) Then
                            indexsAsociated = Nothing
                        End If
                    End Try
                Next

                Return _results
            End If
        End Function

        '[Tomas] 09/12/2009   Modified      Se aplica performance
        Public Shared Function getAsociatedResultsFromResult(ByVal Result As IResult) As ArrayList
            If (IsNothing(Result) = False) Then

                Dim _results As New Hashtable


                ' Se obtienen los tipos de documento asociados 
                Dim DocTypesAsociated As Generic.List(Of Int64) = getDocTypesIdsAsociatedToDocType(Result.DocType)
                Dim indexsAsociated As Generic.List(Of Asociados)
                Dim tempresults As ArrayList
                Dim asociatedDocuments As ArrayList
                ' Por cada entidad asociado
                For Each DT2 As Int64 In DocTypesAsociated
                    Try
                        ' Se obtienen los atributos asociados
                        Dim docTypeAsociated As IDocType = DocTypesBusiness.GetDocType(DT2, True)

                        indexsAsociated = GetAsociations(Result.DocType, docTypeAsociated)

                        If Not IsNothing(indexsAsociated) Then
                            ' Se obtienen los documentos asociados a la búsqueda
                            asociatedDocuments = getSearchAsociatedResults(Result, indexsAsociated, DT2)

                            If Not (IsNothing(asociatedDocuments)) AndAlso asociatedDocuments.Count > 0 Then
                                tempresults = New ArrayList
                                tempresults.AddRange(asociatedDocuments)
                                For Each re As Result In tempresults
                                    If re.ID <> Result.ID Then
                                        If (_results.ContainsKey(re.ID) = False) Then _results.Add(re.ID, re)
                                    End If
                                Next
                            End If
                        End If

                    Catch ex As Exception
                        ZClass.raiseerror(ex)

                    Finally
                        If Not IsNothing(indexsAsociated) Then
                            indexsAsociated = Nothing
                        End If
                        If Not IsNothing(tempresults) Then
                            tempresults = Nothing
                        End If
                        If Not IsNothing(asociatedDocuments) Then
                            asociatedDocuments = Nothing
                        End If
                    End Try
                Next

                If Not IsNothing(indexsAsociated) Then
                    indexsAsociated = Nothing
                End If
                If Not IsNothing(tempresults) Then
                    tempresults = Nothing
                End If
                If Not IsNothing(asociatedDocuments) Then
                    asociatedDocuments = Nothing
                End If

                Dim RS As New ArrayList
                RS.AddRange(_results.Values)
                Return (RS)
            End If
        End Function
        'Public Shared Function getAsociatedResultsFromResult(ByVal Result As IResult) As ArrayList
        '    If (IsNothing(Result) = False) Then

        '        Dim _results As New Hashtable
        '        Dim i As Int16 = GetDocAsociatedCount(Result.DocType.ID)

        '        If i > 0 Then

        '            ' Se obtienen los tipos de documento asociados 
        '            Dim DocTypesAsociated As Generic.List(Of Asociados) = getDocTypesAsociated(Result.DocType)
        '            Dim indexsAsociated As Generic.List(Of Asociados)
        '            Dim tempresults As ArrayList
        '            Dim asociatedDocuments As ArrayList
        '            ' Por cada tipo de documento asociado
        '            For Each DT2 As Asociados In DocTypesAsociated
        '                Try
        '                    ' Se obtienen los índices asociados
        '                    indexsAsociated = GetAsociations(Result.DocType, DT2.DocType2)

        '                    If Not IsNothing(indexsAsociated) Then
        '                        ' Se obtienen los documentos asociados a la búsqueda
        '                        asociatedDocuments = getSearchAsociatedResults(Result, indexsAsociated, DT2.DocType2)

        '                        If Not (IsNothing(asociatedDocuments)) AndAlso asociatedDocuments.Count > 0 Then
        '                            tempresults = New ArrayList
        '                            tempresults.AddRange(asociatedDocuments)
        '                            For Each re As Result In tempresults
        '                                If (_results.ContainsKey(re.ID) = False) Then _results.Add(re.ID, re)
        '                            Next
        '                        End If
        '                    End If

        '                Catch ex As Exception
        '                    ZClass.raiseerror(ex)

        '                Finally
        '                    If Not IsNothing(indexsAsociated) Then
        '                        indexsAsociated = Nothing
        '                    End If
        '                    If Not IsNothing(tempresults) Then
        '                        tempresults = Nothing
        '                    End If
        '                    If Not IsNothing(asociatedDocuments) Then
        '                        asociatedDocuments = Nothing
        '                    End If
        '                End Try
        '            Next

        '            If Not IsNothing(indexsAsociated) Then
        '                indexsAsociated = Nothing
        '            End If
        '            If Not IsNothing(tempresults) Then
        '                tempresults = Nothing
        '            End If
        '            If Not IsNothing(asociatedDocuments) Then
        '                asociatedDocuments = Nothing
        '            End If

        '        End If

        '        If UserPreferences.getValue("LoadDocumentAsociatedByFolderId", Sections.UserPreferences, "False") = True Then
        '            Dim DocTypes As ArrayList = DocTypesBusiness.GetDocTypesbyUserRightsOfView(UserBusiness.CurrentUser.ID, RightsType.View)

        '            For Each Asociado As Zamba.Core.DocType In DocTypes
        '                Dim DocTypeAsoc As Zamba.Core.DocType = DocTypesBusiness.GetDocType(Asociado.ID, False)
        '                Dim R As ArrayList = ModDocuments.SearchByFolderId(Result.ID, DocTypeAsoc, Result.FolderId, UserBusiness.CurrentUser)
        '                If Not IsNothing(R) AndAlso R.Count > 0 Then
        '                    For Each re As Result In R
        '                        If re.ID <> Result.ID Then
        '                            re.Name = "(Carpeta) " & re.Name.Trim
        '                            If _results.ContainsKey(re.ID) = False Then _results.Add(re.ID, re)
        '                        End If
        '                    Next
        '                End If
        '            Next
        '        End If

        '        Dim RS As New ArrayList
        '        RS.AddRange(_results.Values)
        '        Return (RS)
        '    End If
        'End Function

        ''' <summary>
        ''' Busca por asociados y devuelve un datatable
        ''' </summary>
        ''' <param name="treenode">Nodo a completar</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function getAsocResultsCountByDTIDAndIndexsAsoc(ByVal treenode As IAdvanceDocTypeNode) As Int64
            Try
                Return DocAsociatedFactory.getAsocResultsCountByDTIDAndIndexsAsoc(treenode)
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return 0
            End Try
        End Function
        ''' <summary>
        ''' Busca por asociados y devuelve un datatable
        ''' </summary>
        ''' <param name="treenode">Nodo a completar</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function UpdateResultCounts(ByVal QueryString As String) As Int64
            Try
                Return DocAsociatedFactory.UpdateResultsCount(QueryString)
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return 0
            End Try
        End Function

        ''' <summary>
        ''' Se modifico el metodo para que utilize un datatable
        ''' </summary>
        ''' <param name="Result">Result del que se quieren los asociados</param>
        ''' <returns></returns>
        ''' <history>   Marcelo    Modified     20/08/09</history>
        ''' <remarks></remarks>
        Public Shared Function getAsociatedDTResultsFromResult(ByVal result As IResult,
                                                               ByVal lastPage As Int64,
                                                               ByVal blnOpen As Boolean,
                                                               ByRef FC As IFiltersComponent) As DataTable
            Dim dt As DataTable
            Dim dtAsociated As List(Of Long)

            Try
                If result IsNot Nothing Then

                    dt = New DataTable()
                    dt.MinimumCapacity = 0
                    dtAsociated = getDocTypesIdsAsociatedToDocType(result.DocType)

                    For Each DT2 As Int64 In dtAsociated

                        Dim docTypeAsociated As IDocType = DocTypesBusiness.GetDocType(DT2, True)
                        Dim IndexsAsociated As List(Of Asociados) = GetAsociations(result.DocType, docTypeAsociated)

                        If IndexsAsociated IsNot Nothing Then
                            ' Se obtienen los documentos asociados a la búsqueda
                            Dim asociatedDocuments As DataTable = getSearchAsociatedResultsDT(result, IndexsAsociated, DT2, lastPage, blnOpen, FC)

                            If asociatedDocuments IsNot Nothing AndAlso asociatedDocuments.Rows.Count > 0 Then
                                dt.Merge(asociatedDocuments)
                                dt.MinimumCapacity += asociatedDocuments.MinimumCapacity
                                asociatedDocuments = Nothing
                            End If

                            IndexsAsociated = Nothing

                        End If
                    Next

                    If Boolean.Parse(UserPreferences.getValue("ShowNewFirstAsoc", Sections.Search, "False")) AndAlso dt.Columns.Contains(GridColumns.CRDATE_COLUMNNAME) Then
                        Dim dv As New DataView(dt)
                        dv.Sort = GridColumns.CRDATE_COLUMNNAME & " desc"
                        dt = dv.ToTable()
                        dv.Dispose()
                        dv = Nothing
                    End If

                    Return dt
                End If

            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                dtAsociated = Nothing
            End Try

        End Function

        ''' <summary>
        ''' Método que sirve para obtener los documentos asociados
        ''' </summary>
        ''' <param name="Result"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Gaston]	29/12/2008	Modified
        '''     [Gaston]	30/12/2008	Modified    Se agregaron validaciones y nothings en variables que dejaron de utilizarse
        ''' </history>
        Public Shared Function getAsociatedResultsFromResult(ByVal DocTypeId As Int64, ByVal Result As IResult, ByVal PageSize As Int32) As ArrayList
            If (IsNothing(Result) = False) Then


                Dim Results As New Hashtable


                ' Se obtienen los tipos de documento asociados 
                Dim DocTypesAsociated As Generic.List(Of Int64) = getDocTypesIdsAsociatedToDocType(Result.DocType)

                ' Por cada entidad asociado
                For Each DT2 As Int64 In DocTypesAsociated
                    Try
                        If DT2 = DocTypeId Then
                            ' Se obtienen los atributos asociados
                            Dim docTypeAsociated As IDocType = DocTypesBusiness.GetDocType(DT2, True)

                            Dim IndexsAsociated As Generic.List(Of Asociados) = GetAsociations(Result.DocType, docTypeAsociated)
                            If Not (IsNothing(IndexsAsociated)) Then
                                Dim tempresults As New ArrayList
                                ' Se obtienen los documentos asociados a la búsqueda
                                Dim asociatedDocuments As ArrayList = getSearchAsociatedResults(Result, IndexsAsociated, DT2)

                                If Not (IsNothing(asociatedDocuments)) Then
                                    tempresults.AddRange(asociatedDocuments)
                                    For Each re As Result In tempresults
                                        If (Results.ContainsKey(re.ID) = False) Then Results.Add(re.ID, re)
                                    Next
                                    tempresults = Nothing
                                    asociatedDocuments = Nothing
                                End If

                                IndexsAsociated = Nothing
                            End If
                        End If
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                Next
                Dim RS As New ArrayList
                RS.AddRange(Results.Values)
                Return (RS)
            End If
        End Function
#End Region


#Region "ABM"



        Public Shared Sub DeleteAsociaton(ByVal DT1Id As Int32, ByVal DT2Id As Int32, ByVal Ind1 As Int32, ByVal Ind2 As Int32, ByVal name As String)
            DocAsociatedFactory.DeleteAsociaton(DT1Id, DT2Id, Ind1, Ind2)
            'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
            UserBusiness.Rights.SaveAction(DT1Id, ObjectTypes.DocTypes, RightsType.VerDocumentosAsociados, "Se eliminó la asociación '" & name & "' que relacionaba los documentos " & DT1Id & " y " & DT2Id & " por " & Ind1 & " y " & Ind2)
        End Sub
        Public Shared Function GetDocTypeAsociation(ByVal docTypeId As Int64) As DataSet
            Return DocAsociatedFactory.GetDocTypeAsociation(docTypeId)
        End Function
        ''' <summary>
        '''     Obtiene Nombre de la entidad asociado
        ''' </summary>
        ''' <param name="DocTypeId"></param>
        ''' <returns></returns>
        ''' <history>   Javier 18/10/2010   Created</history>
        Public Shared Function GetUniqueDocTypeNameAsociation(ByVal DocTypeParentId As Int64) As DataSet
            Return DocAsociatedFactory.GetUniqueDocTypeNameAsociation(DocTypeParentId)
        End Function
        Public Shared Function IfDocAssociationExist(ByVal DocType1 As Core.DocType, ByVal DocType2 As Core.DocType, ByVal Indice1 As Index, ByVal Indice2 As Index) As Boolean
            Return DocAsociatedFactory.AssocitedAlredyExist(DocType1, DocType2, Indice1, Indice2)
        End Function
        Public Shared Function IfDocAsocExists(ByVal DocId As Int32, ByVal FolderId As Int32) As Boolean
            Return DocAsociatedFactory.IfDocAsocExists(DocId, FolderId)
        End Function

        Public Overrides Sub Dispose()
        End Sub

#End Region
#End Region



    End Class
End Namespace
