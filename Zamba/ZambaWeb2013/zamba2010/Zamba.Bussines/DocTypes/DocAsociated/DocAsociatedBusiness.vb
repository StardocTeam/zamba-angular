Imports Zamba.Core.Search
Imports Zamba.Data
Imports System.Collections.Generic
Imports Zamba.Membership

Namespace DocTypes.DocAsociated

    Public Class DocAsociatedBusiness
        Inherits ZClass




#Region "Metodos Privados"

        Public Shared Function getDocTypesAsociated(ByVal docTypeId As Int64) As Generic.List(Of Int64)
            'Verifica si existe la asociacion de alguno de las 2 formas posibles. 
            Dim Asociados As New List(Of Int64)
            If Not Cache.DocTypesAndIndexs.hsDocTypeAsociations.ContainsKey(docTypeId) Then
                Asociados = ResultsAsociatedFactory.getDocTypesAsociated(docTypeId)
                SyncLock Cache.DocTypesAndIndexs.hsDocTypeAsociations.SyncRoot
                    If Not Cache.DocTypesAndIndexs.hsDocTypeAsociations.ContainsKey(docTypeId) Then
                        Cache.DocTypesAndIndexs.hsDocTypeAsociations.Add(docTypeId, Asociados)
                    End If
                End SyncLock
            Else
                Asociados = Cache.DocTypesAndIndexs.hsDocTypeAsociations.Item(docTypeId)
            End If
            Return Asociados
        End Function


        ''' <summary>
        ''' Método que sirve para obtener los atributos asociados
        ''' </summary>
        ''' <param name="docType1">Tipo de documento primario</param>
        ''' <param name="docType2">Tipo de documento asociado</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Gaston]	29/12/2008	Created
        '''     [Tomas]     04/08/2010  Modified    Se agrega el uso de hashtable
        '''     [Tomas]     18/04/2011  Modified    Se comenta la busqueda por clave "dtid2 - dtid1".
        '''                                         Siempre debe ser "dtid1 - dtid2".
        ''' </history>
        Private Shared Function getAsociations(ByVal docTypeId1 As Int64, ByVal docTypeId2 As Int64) As Generic.List(Of Asociados)
            'Verifica si existe la asociacion de alguno de las 2 formas posibles. 
            If Cache.DocTypesAndIndexs.hsDocAsociations.ContainsKey(docTypeId1 & "-" & docTypeId2) Then
                Return Cache.DocTypesAndIndexs.hsDocAsociations(docTypeId1 & "-" & docTypeId2)
                'ElseIf Cache.DocTypesAndIndexs.hsDocAsociations.ContainsKey(docType2.ID & "-" & docType1.ID) Then
                '    Return Cache.DocTypesAndIndexs.hsDocAsociations.Item(docType2.ID & "-" & docType1.ID)
            Else
                SyncLock Cache.DocTypesAndIndexs.hsDocAsociations
                    Dim Asociated As List(Of Asociados) = ResultsAsociatedFactory.getAsociations(docTypeId1, docTypeId2)
                    If Cache.DocTypesAndIndexs.hsDocAsociations.ContainsKey(docTypeId1 & "-" & docTypeId2) = False Then
                        Cache.DocTypesAndIndexs.hsDocAsociations.Add(docTypeId1 & "-" & docTypeId2, Asociated)
                        Return Asociated
                    End If
                End SyncLock
            End If
        End Function

        ''' <summary>
        ''' Método que sirve para obtener los documentos asociados a la búsqueda
        ''' </summary>
        ''' <param name="result">Documento original</param>
        ''' <param name="IndexsAsociated">Atributos asociados</param>
        ''' <param name="docTypeAsociated">Tipo de documento asociado</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Gaston]	29/12/2008	Created     Código original tomado de otro método
        '''     [Gaston]	30/12/2008	Modified    Se agrego el parámetro docTypeAsociated que indica el entidad asociado y se retorna 
        '''                                         nothing en caso de que indexs sea cero
        ''' </history>
        Private Shared Function getSearchAsociatedResultsAsDT(ByVal result As IResult, ByRef IndexsAsociated As Generic.List(Of Asociados), ByRef docTypeAsociated As Core.DocType, ByVal PageSize As Int32) As DataTable
            Dim indexs As New Generic.List(Of IIndex)
            Dim ASB As New AutoSubstitutionBusiness
            Dim allIndexsAsocComplete As Boolean = True
            For Each associate As Asociados In IndexsAsociated
                For Each i As Index In result.Indexs
                    If i.ID = associate.Index1.ID Then
                        If i.Data <> String.Empty Then
                            associate.Index2.Data = i.Data
                            If i.DropDown = IndexAdditionalType.AutoSustitución OrElse i.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                If String.IsNullOrEmpty(i.dataDescription) AndAlso String.IsNullOrEmpty(i.Data) = False Then
                                    associate.Index2.dataDescription = ASB.getDescription(i.Data, i.ID)
                                Else
                                    associate.Index2.dataDescription = i.dataDescription
                                End If
                            End If
                            associate.Index2.DataTemp = i.Data
                            indexs.Add(associate.Index2)
                            Exit For
                        Else
                            allIndexsAsocComplete = False
                            Exit For
                        End If
                    End If
                Next
            Next
            ASB = Nothing
            Dim IndexsAsociatedaux As New Generic.List(Of Asociados)
            For Each associate As Asociados In IndexsAsociated
                IndexsAsociatedaux.Add(associate)
            Next
            IndexsAsociated = IndexsAsociatedaux
            If (indexs.Count > 0 AndAlso allIndexsAsocComplete) Then
                Dim MD As New Zamba.Core.Search.ModDocuments
                Return MD.DoSearchAsocWithWF(docTypeAsociated, indexs, Zamba.Membership.MembershipHelper.CurrentUser.ID, 0, PageSize, result, Zamba.Membership.MembershipHelper.CurrentUser.Name)
                MD = Nothing
            Else
                Return New DataTable
            End If
        End Function

        ''' <summary>
        ''' Método que sirve para obtener los documentos asociados a la búsqueda
        ''' </summary>
        ''' <param name="result">Documento original</param>
        ''' <param name="IndexsAsociated">Atributos asociados</param>
        ''' <param name="docTypeAsociated">Tipo de documento asociado</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Gaston]	29/12/2008	Created     Código original tomado de otro método
        '''     [Gaston]	30/12/2008	Modified    Se agrego el parámetro docTypeAsociated que indica el entidad asociado y se retorna 
        '''                                         nothing en caso de que indexs sea cero
        ''' </history>
        Private Function getSearchAsociatedResults(ByVal result As IResult, ByRef IndexsAsociated As Generic.List(Of Asociados), ByRef docTypeIdAsociated As Int64, ByVal PageSize As Int32, ByVal UserId As Int64) As ArrayList
            Dim indexs As New Generic.List(Of IIndex)

            Dim DTB As New DocTypesBusiness
            Dim ASB As New AutoSubstitutionBusiness
            Dim allIndexsAsocComplete As Boolean = True


            For Each associate As Asociados In IndexsAsociated


                For Each i As Index In result.Indexs
                    If i.ID = associate.Index1.ID Then
                        If i.Data <> String.Empty Then
                            associate.Index2.Data = i.Data
                            If i.DropDown = IndexAdditionalType.AutoSustitución OrElse i.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                If String.IsNullOrEmpty(i.dataDescription) AndAlso String.IsNullOrEmpty(i.Data) = False Then
                                    associate.Index2.dataDescription = ASB.getDescription(i.Data, i.ID)
                                Else
                                    associate.Index2.dataDescription = i.dataDescription
                                End If
                            End If
                            associate.Index2.DataTemp = i.Data
                            indexs.Add(associate.Index2)
                            Exit For
                        Else
                            allIndexsAsocComplete = False
                            Exit For
                        End If
                    End If
                Next
            Next
            ASB = Nothing
            Dim IndexsAsociatedaux As New Generic.List(Of Asociados)
            For Each associate As Asociados In IndexsAsociated
                IndexsAsociatedaux.Add(associate)

            Next
            IndexsAsociated = IndexsAsociatedaux
            If (indexs.Count > 0 AndAlso allIndexsAsocComplete) Then
                Dim DocTypesAssociated As New List(Of IDocType)
                Dim Doctype As IDocType = DTB.GetDocType(docTypeIdAsociated)
                DocTypesAssociated.Add(Doctype)
                Dim search As New Searchs.Search(indexs, String.Empty, True, DocTypesAssociated, False, String.Empty, UserId)
                Dim MD As New Zamba.Core.Search.ModDocuments
                Dim TotalCount As Int64 = 0
                Dim dt As DataTable = MD.DoSearch(search, UserId, 0, PageSize, True, False, False, TotalCount, False)
                MD = Nothing
                Dim results As New ArrayList()
                If IsNothing(dt) = False Then
                    Dim Rb As New Results_Business

                    For Each row As DataRow In dt.Rows
                        Dim doctypeid As Int64 = CInt(row("doc_type_id"))
                        Dim r As IResult
                        If (dt.Columns.Contains("Nombre")) Then
                            'Por ahora se implementa que vaya a la base a buscar el doc_type, hasta que se implemente la opcion de clonado
                            r = New Result(CInt(row("doc_id")), DTB.GetDocType(doctypeid), row("Nombre").ToString(), 0)
                        Else
                            r = New Result(CInt(row("doc_id")), DTB.GetDocType(doctypeid), row("Name").ToString(), 0)

                        End If
                        Rb.CompleteDocument(r, row)
                        results.Add(r)
                    Next
                    Rb = Nothing
                End If
                Return results
            Else
                Return (Nothing)
            End If
            DTB = Nothing
        End Function

        ''' <summary>
        ''' Método que sirve para obtener los documentos asociados a la búsqueda
        ''' </summary>
        ''' <param name="result">Documento original</param>
        ''' <param name="IndexsAsociated">Atributos asociados</param>
        ''' <param name="docTypeAsociated">Tipo de documento asociado</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Gaston]	29/12/2008	Created     Código original tomado de otro método
        '''     [Gaston]	30/12/2008	Modified    Se agrego el parámetro docTypeAsociated que indica el entidad asociado y se retorna 
        '''                                         nothing en caso de que indexs sea cero
        ''' </history>
        Private Shared Function getSearchAsociatedResultsAsList(ByVal result As IResult, ByVal IndexsAsociated As Generic.List(Of Asociados), ByVal docTypeAsociated As Core.DocType, ByVal PageSize As Int32, ByVal UserId As Int64, onlyImportants As Boolean) As DataTable ' As List(Of IResult)
            Dim indexs As New Generic.List(Of IIndex)
            Dim ASB As New AutoSubstitutionBusiness

            Dim allIndexsAsocComplete As Boolean = True
            For Each associate As Asociados In IndexsAsociated
                If allIndexsAsocComplete = False Then Exit For

                For Each i As Index In result.Indexs
                    If i.ID = associate.Index1.ID Then
                        If i.Data <> String.Empty Then
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, $"Buscando en Entidad Asociada Atributo  {i.Name} con Valor {i.Data}")
                            associate.Index2.Data = i.Data
                            If i.DropDown = IndexAdditionalType.AutoSustitución OrElse i.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                If String.IsNullOrEmpty(i.dataDescription) AndAlso String.IsNullOrEmpty(i.Data) = False Then
                                    associate.Index2.dataDescription = ASB.getDescription(i.Data, i.ID)
                                Else
                                    associate.Index2.dataDescription = i.dataDescription
                                End If
                            End If
                            associate.Index2.DataTemp = i.Data
                            associate.Index2.Operator = "="
                            indexs.Add(associate.Index2)
                            Exit For
                        Else
                            'allIndexsAsocComplete = False
                            associate.Index2.Data = ""
                            associate.Index2.DataTemp = ""
                            associate.Index2.Operator = "es nulo"
                            indexs.Add(associate.Index2)
                            'Exit For

                        End If
                    End If
                Next
            Next
            ASB = Nothing
            'Dim IndexsAsociatedaux As New Generic.List(Of Asociados)
            'For Each associate As Asociados In IndexsAsociated
            '    IndexsAsociatedaux.Add(associate)
            'Next

            'IndexsAsociated = IndexsAsociatedaux

            If (indexs.Count > 0 AndAlso allIndexsAsocComplete) Then
                Dim DocTypesAsociates As New List(Of IDocType)
                DocTypesAsociates.Add(docTypeAsociated)
                Dim search As New Searchs.Search(indexs, String.Empty, True, DocTypesAsociates, False, String.Empty, UserId)
                search.SearchType = SearchTypes.AsociatedSearch
                search.ParentEntity = result
                Dim MD As New Zamba.Core.Search.ModDocuments
                Dim TotalCount As Int64 = 0
                Dim dt As DataTable = MD.DoSearch(search, UserId, 0, PageSize, True, False, False, TotalCount, onlyImportants)
                MD = Nothing
                'Dim results As New List(Of IResult)
                'If IsNothing(dt) = False Then
                '    Dim Rb As New Results_Business
                '    Dim DTB As New DocTypesBusiness
                '    For Each row As DataRow In dt.Rows
                '        Dim doctypeid As Int64 = CInt(row("doc_type_id"))

                '        'Por ahora se implementa que vaya a la base a buscar el doc_type, hasta que se implemente la opcion de clonado
                '        Dim r As Result = New Result(CInt(row("doc_id")), DTB.GetDocType(DocTypeId), row("Name").ToString(), 0)
                '        Rb.CompleteDocument(r, row)
                '        results.Add(r)
                '    Next
                '    DTB = Nothing
                '    Rb = Nothing
                'End If
                'Return results
                Return dt
            Else
                Return New DataTable ' List(Of IResult)
            End If
        End Function

        ''' <summary>
        ''' Método que sirve para obtener los documentos asociados a la búsqueda
        ''' </summary>
        ''' <param name="result">Documento original</param>
        ''' <param name="IndexsAsociated">Atributos asociados</param>
        ''' <param name="docTypeAsociated">Tipo de documento asociado</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Gaston]	29/12/2008	Created     Código original tomado de otro método
        '''     [Gaston]	30/12/2008	Modified    Se agrego el parámetro docTypeAsociated que indica el entidad asociado y se retorna 
        '''                                         nothing en caso de que indexs sea cero
        '''     [Marcelo]   26/08/2009  Modified    Devuelve un datatable en lugar de un arraylist
        '''     [Javier]    22/10/2010  Modified    Se agrega datadecription al indice asociado para usar al armar la query para traer asociados
        ''' </history>
        Private Function getSearchAsociatedResultsDT(ByVal result As IResult, ByRef IndexsAsociated As Generic.List(Of Asociados), ByRef docTypeAsociated As Core.DocType, ByVal LastDocId As Int64, ByVal blnOpen As Boolean, ByVal GetTaskId As Boolean, ByVal UserId As Int64, ByVal orderby As String) As DataTable
            Dim indexs As New Generic.List(Of IIndex)

            Dim allIndexsAsocComplete As Boolean = True
            For Each associate As Asociados In IndexsAsociated
                For Each i As Index In result.Indexs
                    If i.ID = associate.Index1.ID Then
                        If i.Data <> String.Empty Then
                            associate.Index2.Data = i.Data
                            associate.Index2.DataTemp = i.Data
                            associate.Index2.dataDescription = i.dataDescription
                            associate.Index2.dataDescriptionTemp = i.dataDescriptionTemp
                            indexs.Add(associate.Index2)
                            Exit For
                        Else
                            allIndexsAsocComplete = False
                            Exit For
                        End If
                    End If
                Next
            Next

            Dim IndexsAsociatedaux As New Generic.List(Of Asociados)
            For Each associate As Asociados In IndexsAsociated
                IndexsAsociatedaux.Add(associate)
            Next
            IndexsAsociated = IndexsAsociatedaux
            If (indexs.Count > 0 AndAlso allIndexsAsocComplete) Then
                Dim DocTypesAsociates As New List(Of IDocType)
                DocTypesAsociates.Add(docTypeAsociated)

                Dim search As New Searchs.Search(indexs, String.Empty, True, DocTypesAsociates, False, String.Empty, UserId)
                If String.IsNullOrEmpty(orderby) Then
                    orderby = "doc_id desc"
                End If
                search.OrderBy = orderby
                Dim MD As New Zamba.Core.Search.ModDocuments
                Dim TotalCount As Int64 = 0
                Return MD.DoSearch(search, UserId, LastDocId, 100, True, False, False, TotalCount, False)
                MD = Nothing
            Else
                Return (Nothing)
            End If
        End Function
#End Region

#Region "Metodos Publicos"

#Region "Get"

        Public Shared Function AreResultsAsociated(ByVal Result1 As Result, ByVal Result2 As Result) As Boolean
            Dim Asociated As Boolean
            If (IsNothing(Result1) = False AndAlso IsNothing(Result2) = False) Then

                ' Se obtienen los atributos asociados
                Dim IndexsAsociated As Generic.List(Of Asociados) = getAsociations(Result1.DocType.ID, Result2.DocType.ID)
                If IsNothing(IndexsAsociated) = False Then

                    For Each associate As Asociados In IndexsAsociated
                        For Each i1 As Index In Result1.Indexs
                            If (i1.ID = associate.Index1.ID) Then
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


        ''' <summary>
        ''' Método que sirve para recuperar un FormId en base a un docTypeId
        ''' </summary>
        ''' <param name="docTypeId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' 	[Gaston]	07/07/2008	Created
        ''' </history>
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
        Public Shared Function getAsociatedFormsId(ByVal docTypeId As Int64) As DataSet
            If Not Cache.DocTypesAndIndexs.hsForms.ContainsKey(docTypeId) Then
                Cache.DocTypesAndIndexs.hsForms.Add(docTypeId, DocAsociatedFactory.getAsociatedFormsId(docTypeId))
            End If
            Return Cache.DocTypesAndIndexs.hsForms.Item(docTypeId)


        End Function

        ''' <summary>
        ''' Busca por asociados y devuelve un datatable
        ''' </summary>
        ''' <param name="Result"></param>
        ''' <param name="PageSize"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function getAsociatedResultsFromResultAsDT(ByVal Result As IResult, ByVal PageSize As Int32, ByVal docTypesIDs As List(Of String), ByVal UserId As Int64) As DataTable
            If (IsNothing(Result) = False) Then
                Dim _results As New DataTable

                ' Se obtienen los tipos de documento asociados 
                Dim DocTypesAsociated As Generic.List(Of Int64) = getDocTypesAsociated(Result.DocTypeId)
                Dim indexsAsociated As Generic.List(Of Asociados)

                Dim DTB As New DocTypesBusiness
                ' Por cada entidad asociado
                For Each DT2 As Int64 In DocTypesAsociated
                    Try
                        If docTypesIDs.Count = 0 OrElse docTypesIDs.Contains(DT2) Then
                            If New RightsBusiness().GetUserRights(UserId, ObjectTypes.DocTypes, RightsType.View, DT2) Then
                                ' Se obtienen los atributos asociados
                                indexsAsociated = getAsociations(Result.DocTypeId, DT2)

                                If Not IsNothing(indexsAsociated) Then
                                    ' Se obtienen los documentos asociados a la búsqueda
                                    Dim DocType2 As IDocType = DTB.GetDocType(DT2)
                                    _results.Merge(getSearchAsociatedResultsAsDT(Result, indexsAsociated, DocType2, PageSize))
                                    _results.PrimaryKey = New DataColumn() {_results.Columns("DOC_ID")}
                                End If
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
                DTB = Nothing
                Return _results
            End If
        End Function

        '''[Tomas] 09/12/2009   Modified      Se aplica performance
        Public Function getAsociatedResultsFromResult(ByVal Result As IResult, ByVal PageSize As Int32, ByVal UserId As Int64) As ArrayList
            If (IsNothing(Result) = False) Then

                Dim _results As New Hashtable
                'Dim i As Int16 = DocAsociatedBusiness.GetDocAsociatedCount(Result.DocType.ID, True)


                ' Se obtienen los tipos de documento asociados 
                Dim DocTypesAsociated As Generic.List(Of Int64) = getDocTypesAsociated(Result.DocType.ID)
                Dim indexsAsociated As Generic.List(Of Asociados)
                Dim tempresults As ArrayList
                Dim asociatedDocuments As ArrayList
                ' Por cada entidad asociado
                For Each DT2 As Int64 In DocTypesAsociated
                    Try
                        If New RightsBusiness().GetUserRights(UserId, ObjectTypes.DocTypes, RightsType.View, DT2) Then

                            ' Se obtienen los atributos asociados
                            indexsAsociated = getAsociations(Result.DocType.ID, DT2)

                            If Not IsNothing(indexsAsociated) Then
                                ' Se obtienen los documentos asociados a la búsqueda
                                asociatedDocuments = getSearchAsociatedResults(Result, indexsAsociated, DT2, PageSize, UserId)

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

        ''' <summary>
        ''' Se modifico el metodo para que utilize un datatable
        ''' </summary>
        ''' <param name="Result">Result del que se quieren los asociados</param>
        ''' <returns></returns>
        ''' <history>   Marcelo    Modified     20/08/09</history>
        ''' <remarks></remarks>
        Public Function getAsociatedDTResultsFromResult(ByVal Result As IResult, ByVal LastDocId As Int64, ByVal blnOpen As Boolean, ByVal GetTaskId As Boolean, ByVal UserId As Int64) As DataTable
            If (IsNothing(Result) = False) Then

                Dim dt As New DataTable
                dt.MinimumCapacity = 0
                Dim dtAsociated As List(Of Long)
                dtAsociated = GetUniqueDocTypeIdsAsociation(Result.DocTypeId)
                Dim DTB As New DocTypesBusiness()
                Dim Doctypes As List(Of IDocType) = DTB.GetDocTypesbyUserRights(UserId, RightsType.View)

                Dim UP As New UserPreferences

                Dim customUserAssociatedOrderByAll As String = UP.getValue("customUserAssociatedOrderByAll", UPSections.UserPreferences, "doc_id desc", MembershipHelper.CurrentUser.ID)

                For Each DT2 As Int64 In dtAsociated
                    For Each IDT As IDocType In Doctypes
                        If (DT2 = IDT.ID) Then
                            Try
                                Dim IndexsAsociated As List(Of Asociados) = getAsociations(Result.DocTypeId, DT2)
                                If IndexsAsociated IsNot Nothing Then
                                    ' Se obtienen los documentos asociados a la búsqueda
                                    Dim Doctype2 As IDocType = DTB.GetDocType(DT2)
                                    If Doctype2 IsNot Nothing Then

                                        Dim customUserAssociatedOrderBy As String
                                        If String.IsNullOrEmpty(customUserAssociatedOrderByAll) Then
                                            customUserAssociatedOrderBy = UP.getValue("customUserAssociatedOrderBy-" & Result.DocTypeId.ToString() & "-" & DT2.ToString(), UPSections.UserPreferences, "", MembershipHelper.CurrentUser.ID)
                                        Else
                                            customUserAssociatedOrderBy = customUserAssociatedOrderByAll
                                        End If

                                        Dim asociatedDocuments As DataTable = getSearchAsociatedResultsDT(Result, IndexsAsociated, Doctype2, LastDocId, blnOpen, GetTaskId, UserId, customUserAssociatedOrderBy)

                                            If asociatedDocuments IsNot Nothing AndAlso asociatedDocuments.Rows.Count > 0 Then
                                                dt.Merge(asociatedDocuments)
                                                dt.MinimumCapacity += asociatedDocuments.MinimumCapacity
                                                asociatedDocuments = Nothing
                                            End If
                                            IndexsAsociated = Nothing
                                        End If
                                    End If
                            Catch ex As Exception
                                ZClass.raiseerror(ex)
                            End Try
                        End If
                    Next
                Next

                If String.IsNullOrEmpty(customUserAssociatedOrderByAll) = False Then
                    dt.DefaultView.Sort = customUserAssociatedOrderByAll
                    dt.AcceptChanges()
                End If

                DTB = Nothing
                Return dt
                End If
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
        Public Shared Function getAsociatedResultsFromResultAsList(ByVal AssociatedDocTypeId As Int64, ByVal Result As IResult, ByVal PageSize As Int32, ByVal UserId As Int64, onlyImportants As Boolean) As DataTable ' List(Of IResult)
            Dim DocTypesAsociated As Generic.List(Of Int64)
            Dim IndexsAsociated As Generic.List(Of Asociados)

            Try
                If (IsNothing(Result) = False) Then

                    Dim Results As New Hashtable

                    ZTrace.WriteLineIf(ZTrace.IsVerbose, $"Obteniendo Asociados de la Entidad {Result.DocType.ID}")
                    ' Se obtienen los tipos de documento asociados 
                    DocTypesAsociated = getDocTypesAsociated(Result.DocType.ID)

                    If (DocTypesAsociated IsNot Nothing) Then
                        Dim DTB As New DocTypesBusiness
                        ' Por cada entidad asociado
                        For Each DT2 As Int64 In DocTypesAsociated
                            Try
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, $"Buscando en Entidad asociada {AssociatedDocTypeId}")
                                If DT2 = AssociatedDocTypeId Then
                                    Dim tempresults As New List(Of IResult)
                                    ' Se obtienen los atributos asociados
                                    IndexsAsociated = getAsociations(Result.DocType.ID, DT2)

                                    If Not (IsNothing(IndexsAsociated)) Then
                                        Dim DocType2 As IDocType = DTB.GetDocType(DT2)
                                        ' Se obtienen los documentos asociados a la búsqueda
                                        Return getSearchAsociatedResultsAsList(Result, IndexsAsociated, DocType2, PageSize, UserId, onlyImportants)
                                    End If
                                End If
                            Catch ex As Exception
                                ZClass.raiseerror(ex)
                            End Try
                        Next
                        DTB = Nothing
                    End If
                End If
                Return New DataTable ' List(Of IResult)
            Finally
                If Not IsNothing(DocTypesAsociated) Then
                    DocTypesAsociated = Nothing
                End If
                If Not IsNothing(IndexsAsociated) Then
                    IndexsAsociated = Nothing
                End If
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
        Public Function getAsociatedResultsFromResult(ByVal DocTypeId As Int64, ByVal Result As IResult, ByVal PageSize As Int32, ByVal UserId As Int64) As ArrayList
            If (IsNothing(Result) = False) Then


                Dim Results As New Hashtable


                ' Se obtienen los tipos de documento asociados 
                Dim DocTypesAsociated As Generic.List(Of Int64) = getDocTypesAsociated(Result.DocType.ID)

                ' Por cada entidad asociado
                For Each DT2 As Int64 In DocTypesAsociated
                    Try
                        If DT2 = DocTypeId Then
                            ' Se obtienen los atributos asociados
                            Dim IndexsAsociated As Generic.List(Of Asociados) = getAsociations(Result.DocType.ID, DT2)
                            If Not (IsNothing(IndexsAsociated)) Then
                                Dim tempresults As New ArrayList
                                ' Se obtienen los documentos asociados a la búsqueda
                                Dim asociatedDocuments As ArrayList = getSearchAsociatedResults(Result, IndexsAsociated, DT2, PageSize, UserId)

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




        ''' <summary>
        '''     Obtiene Nombre del entidad asociado
        ''' </summary>
        ''' <param name="DocTypeId"></param>
        ''' <returns></returns>
        ''' <history>   Javier 18/10/2010   Created</history>
        Public Shared Function GetUniqueDocTypeIdsAsociation(ByVal DocTypeId As Int64) As List(Of Int64)
            If Not Cache.DocAsociations.GetInstance().hsDocAsociationsIds.ContainsKey(DocTypeId) Then
                Cache.DocAsociations.GetInstance().hsDocAsociationsIds.Add(DocTypeId, DocAsociatedFactory.GetUniqueDocTypeIdsAsociation(DocTypeId))
            End If
            Return Cache.DocAsociations.GetInstance().hsDocAsociationsIds.Item(DocTypeId)
        End Function
        Public Shared Function IfDocAssociationExist(ByVal DocType1 As Core.DocType, ByVal DocType2 As Core.DocType, ByVal Indice1 As Index, ByVal Indice2 As Index) As Boolean
            Return DocAsociatedFactory.AssocitedAlredyExist(DocType1, DocType2, Indice1, Indice2)
        End Function
      
        Public Overrides Sub Dispose()
        End Sub

#End Region
#End Region

    End Class
End Namespace