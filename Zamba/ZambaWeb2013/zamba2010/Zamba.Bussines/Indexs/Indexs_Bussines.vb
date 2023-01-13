Imports Zamba.Data
Imports System.Collections.Generic
Imports Zamba.Core.Cache

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Business
''' Class	 : Core.Indexs_Factory
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Lógica de Atributos
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Marcelo]	26/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Partial Public Class IndexsBusiness
    Private Shared _LoadedControlsTable As New Hashtable

#Region "Constantes"
    'Todo pasarlo a constantes
    'Nombres de las columnas
    Private Shared INDEX_ID As String = "INDEX_ID"
    Private Shared INDEX_NAME As String = "INDEX_NAME"
    Private Shared INDEX_TYPE As String = "INDEX_TYPE"
    Private Shared INDEX_LENGTH As String = "INDEX_LEN"
    Private Shared INDEX_AUTOFILL As String = "AUTOFILL"
    Private Shared INDEX_NO_INDEX As String = "NOINDEX"
    Private Shared INDEX_DROPDOWN As String = "DROPDOWN"
    Private Shared INDEX_AUTO_DISPLAY As String = "AUTODISPLAY"
    Private Shared INDEX_INVISIBLE As String = "INVISIBLE"
    Private Shared INDEX_OBJECT_TYPE_ID As String = "OBJECT_TYPE_ID"
    Private Shared INDEX_MUST_COMPLETE As String = "MUSTCOMPLETE"
    Private Shared HIERARCHICALTABLENAME As String = "ZHierarchy_I{0}_I{1}"
    'IndexId, ParentIndexId, ParentValue
    Private Shared HIERARCHICALTABLE_CACHEOPTIONS As String = "{0}_{1}_{2}"
#End Region
    Public Shared Event saveIndex(ByVal noQuestion As Boolean)
    Public Shared Event CancelIndex(ByVal CancelSave As Boolean)
    Public Shared Event CloseSaveIndex()

    Private Shared _useIndexCache As String


    Public Shared Sub SaveIndexValue(Optional ByVal noQuestion As Boolean = True)
        RaiseEvent saveIndex(noQuestion)
        RaiseEvent CloseSaveIndex()
    End Sub
    Public Shared Sub CancelIndexValue(Optional ByVal CancelSave As Boolean = True)
        RaiseEvent CancelIndex(CancelSave)
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo que chequea si un indice de sustitucion o de busqueda permite insertar datos que no se encuentren en la lista de valores
    ''' </summary>
    ''' <param name="IndexId">ID del indice a comprobar</param>
    ''' <history>
    ''' 	[AlejandroR]    14/03/2011  Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Shared hsAllowDataOutOfList As Hashtable = New Hashtable

    Public Shared Function CheckIfAllowDataOutOfList(ByVal IndexID As Long) As Boolean

        If Not hsAllowDataOutOfList.ContainsKey(IndexID) Then
            hsAllowDataOutOfList.Add(IndexID, Indexs_Factory.CheckIfAllowDataOutOfList(IndexID))
        End If

        Return hsAllowDataOutOfList(IndexID)

    End Function

    Public Shared Sub Fill(ByRef instance As IIndex)
        If IsNothing(instance.DropDownList) Then
            instance.DropDownList = GetDropDownList(instance.ID)
        End If
        If IsNothing(instance.IndexTypes) Then

        End If
    End Sub

    Private Shared Function BuildIndex(ByVal dr As DataRow) As IIndex
        Dim CurrentIndex As IIndex = Nothing

        If Not IsNothing(dr) Then
            CurrentIndex = New Index()

            If Not IsNothing(dr(INDEX_ID)) Then
                Dim TryIndexId As Int64

                If Int64.TryParse(dr(INDEX_ID).ToString(), TryIndexId) Then
                    CurrentIndex.ID = TryIndexId
                End If
            End If

            If Not IsNothing(dr(INDEX_NAME)) Then
                CurrentIndex.Name = dr(INDEX_NAME).ToString()
            End If

            If Not IsNothing(dr(INDEX_TYPE)) Then
                Dim TryIndexType As Int32

                If Int32.TryParse(dr(INDEX_TYPE).ToString(), TryIndexType) Then
                    CurrentIndex.Type = DirectCast(TryIndexType, IndexDataType)
                End If
            End If

            If Not IsNothing(dr(INDEX_LENGTH)) Then
                Dim TryIndexLenght As Int32

                If Int32.TryParse(dr(INDEX_LENGTH).ToString(), TryIndexLenght) Then
                    CurrentIndex.Len = TryIndexLenght
                End If
            End If

            If Not IsNothing(dr(INDEX_AUTOFILL)) Then
                Dim TryIndexAutoFill As Int32


                If Int32.TryParse(dr(INDEX_AUTOFILL).ToString(), TryIndexAutoFill) Then
                    If TryIndexAutoFill = 0 Then
                        CurrentIndex.AutoFill = False
                    Else
                        CurrentIndex.AutoFill = True
                    End If

                End If
            End If

            If Not IsNothing(dr(INDEX_NO_INDEX)) Then
                Dim TryIndexNoIndex As Int32

                If Int32.TryParse(dr(INDEX_NO_INDEX).ToString(), TryIndexNoIndex) Then
                    CurrentIndex.NoIndex = TryIndexNoIndex

                    If TryIndexNoIndex = 0 Then
                        CurrentIndex.NoIndex = False
                    Else
                        CurrentIndex.NoIndex = True
                    End If
                End If
            End If

            If Not IsNothing(dr(INDEX_DROPDOWN)) Then
                Dim TryIndexDropDown As Int32

                If Int32.TryParse(dr(INDEX_DROPDOWN).ToString(), TryIndexDropDown) Then
                    CurrentIndex.DropDown = DirectCast(TryIndexDropDown, IndexAdditionalType)
                End If
            End If

            If Not IsNothing(dr(INDEX_AUTO_DISPLAY)) Then
                Dim TryIndexAutoDisplay As Int32

                If Int32.TryParse(dr(INDEX_AUTO_DISPLAY).ToString(), TryIndexAutoDisplay) Then
                    CurrentIndex.AutoDisplay = TryIndexAutoDisplay

                    If TryIndexAutoDisplay = 0 Then
                        CurrentIndex.AutoDisplay = False
                    Else
                        CurrentIndex.AutoDisplay = True
                    End If
                End If
            End If

            If Not IsNothing(dr(INDEX_INVISIBLE)) Then
                Dim TryIndexInvisible As Int32

                If Int32.TryParse(dr(INDEX_INVISIBLE).ToString(), TryIndexInvisible) Then
                    CurrentIndex.Invisible = TryIndexInvisible

                    If TryIndexInvisible = 0 Then
                        CurrentIndex.Invisible = False
                    Else
                        CurrentIndex.Invisible = True
                    End If
                End If
            End If

            If Not IsNothing(dr(INDEX_OBJECT_TYPE_ID)) Then
                Dim TryIndexObjectId As Int32

                If Int32.TryParse(dr(INDEX_OBJECT_TYPE_ID).ToString(), TryIndexObjectId) Then
                    CurrentIndex.Object_Type_Id = TryIndexObjectId
                End If
            End If

            If Not IsNothing(dr(INDEX_MUST_COMPLETE)) Then
                Dim TryIndexRequired As Int32

                If Int32.TryParse(dr(INDEX_MUST_COMPLETE).ToString(), TryIndexRequired) Then
                    CurrentIndex.Required = TryIndexRequired

                    If TryIndexRequired = 0 Then
                        CurrentIndex.Required = False
                    Else
                        CurrentIndex.Required = True
                    End If
                End If
            End If

        End If

        Return CurrentIndex
    End Function

#Region "Comparación"

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Compara un índice con un valor y comparador especificado.
    ''' </summary>
    ''' <history>
    ''' [Alejandro]
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function CompareIndex(ByVal indexValue As String,
    ByVal indexType As IndexDataType, ByVal comparator As Comparators,
     ByVal valueToCompare As String, Optional ByVal casesensitive As Boolean = True
     ) As Boolean
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Comparando Atributo: " & indexValue & " (" & indexType.ToString() & ") " & comparator.ToString() & " " & valueToCompare)
        Select Case comparator
            Case Comparators.Equal
                If String.Compare(indexValue, valueToCompare, Not casesensitive) = 0 Then Return True
            Case Comparators.Different
                If String.Compare(indexValue, valueToCompare, Not casesensitive) <> 0 Then Return True
            Case Comparators.Contents
                If casesensitive Then
                    If indexValue.IndexOf(valueToCompare) > -1 Then Return True
                Else
                    If indexValue.ToLower().IndexOf(valueToCompare.ToLower()) > -1 Then Return True
                End If
            Case Comparators.Starts
                If casesensitive Then
                    If indexValue.StartsWith(valueToCompare) Then Return True
                Else
                    If indexValue.ToLower().StartsWith(valueToCompare.ToLower()) Then Return True
                End If
            Case Comparators.Ends
                If casesensitive Then
                    If indexValue.EndsWith(valueToCompare) Then Return True
                Else
                    If indexValue.ToLower().EndsWith(valueToCompare.ToLower()) Then Return True
                End If
            Case Comparators.Upper
                Select Case indexType
                    Case IndexDataType.Numerico, IndexDataType.Numerico_Largo,
                          IndexDataType.Numerico_Decimales, IndexDataType.Moneda,
                          IndexDataType.Si_No
                        Try
                            If Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = "," Then
                                indexValue = indexValue.Replace(".", "").Replace(",", ".").Replace(".", ",")
                            End If

                            If Decimal.Parse(indexValue) > Decimal.Parse(valueToCompare) Then
                                Return True
                            Else
                                Return False
                            End If
                        Catch
                            Return False
                        End Try
                    Case IndexDataType.Fecha, IndexDataType.Fecha_Hora
                        Try
                            If DateTime.Parse(indexValue) > DateTime.Parse(valueToCompare) Then
                                Return True
                            Else
                                Return False
                            End If
                        Catch
                            Return False
                        End Try
                    Case Else
                        If indexValue.CompareTo(valueToCompare) > 0 Then
                            Return True
                        Else
                            Return False
                        End If
                End Select
            Case Comparators.Lower
                Select Case indexType
                    Case IndexDataType.Numerico, IndexDataType.Numerico_Largo,
                          IndexDataType.Numerico_Decimales, IndexDataType.Moneda,
                          IndexDataType.Si_No
                        Try
                            If Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = "," Then
                                indexValue = indexValue.Replace(".", "").Replace(",", ".").Replace(".", ",")
                            End If

                            If Decimal.Parse(indexValue) < Decimal.Parse(valueToCompare) Then
                                Return True
                            Else
                                Return False
                            End If
                        Catch
                            Return False
                        End Try
                    Case IndexDataType.Fecha, IndexDataType.Fecha_Hora
                        Try
                            If DateTime.Parse(indexValue) < DateTime.Parse(valueToCompare) Then
                                Return True
                            Else
                                Return False
                            End If
                        Catch
                            Return False
                        End Try
                    Case Else
                        If indexValue.CompareTo(valueToCompare) < 0 Then
                            Return True
                        Else
                            Return False
                        End If
                End Select
            Case Comparators.EqualLower
                Select Case indexType
                    Case IndexDataType.Numerico, IndexDataType.Numerico_Largo,
                          IndexDataType.Numerico_Decimales, IndexDataType.Moneda,
                          IndexDataType.Si_No
                        Try
                            If Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = "," Then
                                indexValue = indexValue.Replace(".", "").Replace(",", ".").Replace(".", ",")
                            End If

                            If Decimal.Parse(indexValue) <= Decimal.Parse(valueToCompare) Then
                                Return True
                            Else
                                Return False
                            End If
                        Catch
                            Return False
                        End Try
                    Case IndexDataType.Fecha, IndexDataType.Fecha_Hora
                        Try
                            If DateTime.Parse(indexValue) <= DateTime.Parse(valueToCompare) Then
                                Return True
                            Else
                                Return False
                            End If
                        Catch
                            Return False
                        End Try
                    Case Else
                        If indexValue.CompareTo(valueToCompare) <= 0 Then
                            Return True
                        Else
                            Return False
                        End If
                End Select
            Case Comparators.EqualUpper
                Select Case indexType
                    Case IndexDataType.Numerico, IndexDataType.Numerico_Largo,
                          IndexDataType.Numerico_Decimales, IndexDataType.Moneda,
                          IndexDataType.Si_No
                        Try
                            If Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = "," Then
                                indexValue = indexValue.Replace(".", "").Replace(",", ".").Replace(".", ",")
                            End If

                            If Decimal.Parse(indexValue) >= Decimal.Parse(valueToCompare) Then
                                Return True
                            Else
                                Return False
                            End If
                        Catch
                            Return False
                        End Try
                    Case IndexDataType.Fecha, IndexDataType.Fecha_Hora
                        Try
                            If DateTime.Parse(indexValue) >= DateTime.Parse(valueToCompare) Then
                                Return True
                            Else
                                Return False
                            End If
                        Catch
                            Return False
                        End Try
                    Case Else
                        If indexValue.CompareTo(valueToCompare) >= 0 Then
                            Return True
                        Else
                            Return False
                        End If
                End Select
            Case Else
                Return False
        End Select
        Return False
    End Function

#End Region
    Public Shared Property LoadedControlsTable() As Hashtable
        Get
            Return _LoadedControlsTable
        End Get
        Set(ByVal Value As Hashtable)
            _LoadedControlsTable = Value
        End Set
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Dataset tipeado con todos los datos de Doc_index,
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetAllIndexs() As DataSet
        Dim allIndexs As DataSet = Indexs_Factory.GetAllIndexs()


        Return allIndexs

    End Function

    Public Shared Function GetIndexByDropDownValue(ByVal type As IndexAdditionalType) As DataSet
        Return Indexs_Factory.GetIndexByDropDownValue(type)
    End Function

    Public Shared Function GetIndexOfAnyDropDownType() As DataSet
        Return Indexs_Factory.GetIndexOfAnyDropDownType()
    End Function

    Public Function GetIndexById(ByVal id As Int64) As DataSet
        If Cache.DocTypesAndIndexs.hsIndexs.ContainsKey(id) = False Then
            SyncLock Cache.DocTypesAndIndexs.hsIndexs
                Dim INF As New Indexs_Factory
                Dim i As DataSet = INF.GetIndexById(id)
                INF = Nothing
                If Cache.DocTypesAndIndexs.hsIndexs.ContainsKey(id) = False Then
                    Cache.DocTypesAndIndexs.hsIndexs.Add(id, i)
                End If
            End SyncLock
        End If
        Return Cache.DocTypesAndIndexs.hsIndexs(id)

    End Function

    Public Function GetIndexChilds(ByVal indexID As Long) As List(Of Long)
        Dim dt As DataTable
        If Cache.DocTypesAndIndexs.hsIndexsChilds.ContainsKey(indexID) = False Then
            Dim INF As New Indexs_Factory
            dt = Indexs_Factory.GetIndexChilds(indexID)
            INF = Nothing
            SyncLock Cache.DocTypesAndIndexs.hsIndexsChilds
                If Cache.DocTypesAndIndexs.hsIndexsChilds.ContainsKey(indexID) = False Then
                    Cache.DocTypesAndIndexs.hsIndexsChilds.Add(indexID, dt)
                End If
            End SyncLock
        Else
            dt = Cache.DocTypesAndIndexs.hsIndexsChilds(indexID)
        End If

        If dt Is Nothing OrElse dt.Rows.Count = 0 Then
            Return Nothing
        Else
            Dim retList As New List(Of Long)
            Dim max As Integer = dt.Rows.Count - 1

            For i As Integer = 0 To max
                retList.Add(dt.Rows(i)(0))
            Next

            Return retList
        End If
    End Function




    Public Function GenerateDummyIndex(id As Int64, name As String, type As IndexDataType) As Object
        Dim Index As IIndex = New Index()
        Index.ID = id
        Index.Name = name
        Index.Type = type
        Return Index
    End Function

    Public Shared Function GetIndexDropDownType(ByVal indexid As Int64) As Int64
        Dim ret As Int16 = 0

        If Not String.IsNullOrEmpty(IndexsBusiness.GetIndexName(indexid)) Then

            Dim WTF As New WFTasksFactory
            ret = WTF.GetIndexDropDownType(indexid, 30)
            WTF = Nothing
        End If

        Return ret
    End Function


    ''' <summary>
    ''' Obtiene los indices a partir del id del id del documento
    ''' </summary>
    ''' <param name="docId">Id del documento</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIndexsData(ByVal docId As Int64, ByVal docTypeId As Int64) As List(Of IIndex)
        Dim IB As New IndexsBusiness
        Dim dtIndexs As List(Of IIndex) = IB.GetIndexsSchemaAsListOfDT(docTypeId)
        IB = Nothing

        If dtIndexs IsNot Nothing Then
            Dim indexIds As New List(Of Int64)
            For Each I As IIndex In dtIndexs
                indexIds.Add(I.ID)
            Next
            Dim ISF As New Indexs_Factory
            Dim refIndexs As List(Of ReferenceIndex) = (New ReferenceIndexBusiness).GetReferenceIndexesByDoctypeId(docTypeId)
            Dim indexsValues As Dictionary(Of Int64, String) = ISF.GetIndexValues(docId, docTypeId, indexIds, refIndexs)
            ISF = Nothing

            Dim indexs As List(Of IIndex) = New List(Of IIndex)

            For Each currentIndex As IIndex In dtIndexs
                If indexsValues.ContainsKey(currentIndex.ID) Then
                    currentIndex.Data = indexsValues.Item(currentIndex.ID)
                    If currentIndex.DropDown = IndexAdditionalType.AutoSustitución OrElse currentIndex.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                        'Se carga la descripcion de Indice
                        Dim ASB As New AutoSubstitutionBusiness
                        currentIndex.dataDescription = ASB.getDescription(currentIndex.Data, currentIndex.ID)
                        ASB = Nothing

                        currentIndex.dataDescriptionTemp = currentIndex.dataDescription
                    End If
                End If

                indexs.Add(currentIndex)
            Next

            dtIndexs = Nothing
            indexIds = Nothing
            indexsValues = Nothing

            Return indexs
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene los indices a partir del id del id del documento
    ''' </summary>
    ''' <param name="docId">Id del documento</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetResultIndexs(ByVal docId As Int64, ByVal DocTypeId As Int64, ByVal indexs As ArrayList) As ArrayList
        Dim IndexsValues As Dictionary(Of Int64, String) = Indexs_Factory.GetIndexValues(docId, DocTypeId, indexs)
        Dim i As Int16 = 0
        Dim indexsList As ArrayList = indexs.Clone()

        For Each CurrentIndex As IIndex In indexs
            If IndexsValues.ContainsKey(CurrentIndex.ID) Then
                indexsList(i).Data = IndexsValues.Item(CurrentIndex.ID)
            End If
            i += 1
        Next

        Return indexsList
    End Function



    Public Shared Function GetIndexNameById(ByVal Indexid As Int64) As String
        Return Indexs_Factory.GetIndexNameById(Indexid)
    End Function

    Public Shared Function GetIndexIdByName(ByVal IndexName As String) As Int64
        If Cache.DocTypesAndIndexs.hsIndexsName.ContainsKey(IndexName) = False Then
            Dim IndexId As Int64 = Indexs_Factory.GetIndexIdByName(IndexName)
            SyncLock Cache.DocTypesAndIndexs.hsIndexsName
                If Cache.DocTypesAndIndexs.hsIndexsName.ContainsKey(IndexName) = False Then
                    Cache.DocTypesAndIndexs.hsIndexsName.Add(IndexName, IndexId)
                End If
            End SyncLock
        End If
        Return Cache.DocTypesAndIndexs.hsIndexsName(IndexName)
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un Dataset con una lista de los valores que aparecen en un item filtrando con restricciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Marcelo]	28/09/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetDistinctIndexValues(ByVal docTypeId As Int32, ByVal indexId As Int32, ByVal UserId As Int32, ByVal indexType As Int32) As DataSet
        'Traigo las restricciones y armo un string con ellas
        Dim UB As New UserBusiness
        Dim RMF As New RestrictionsMapper_Factory
        Dim restricc As String = RMF.GetRestrictionWebStrings(UserId, docTypeId, UB.GetUserNamebyId(UserId))
        UB = Nothing
        RMF = Nothing
        Return Indexs_Factory.GetDistinctIndexValues(docTypeId, indexId, restricc, indexType)
    End Function





    Public Shared Function GetIndexTypes() As ArrayList
        Dim indexTypes As New ArrayList()
        indexTypes.Add("Numerico")
        indexTypes.Add("Numerico_Largo")
        indexTypes.Add("Numerico_Decimales")
        indexTypes.Add("Fecha")
        indexTypes.Add("Fecha_Hora")
        indexTypes.Add("Moneda")
        indexTypes.Add("Alfanumerico")
        indexTypes.Add("Alfanumerico_Largo")
        indexTypes.Add("Si_No")

        Return indexTypes
    End Function


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Dataset tipeado con los datos de Index_R_Doc_type para un Doc_Type especifico
    ''' </summary>
    ''' <param name="DocTypeId">Id del Entidad</param>
    ''' <returns>DsIndex</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetIndexSchema(ByVal DocTypeId As Int32) As DataSet
        Return Indexs_Factory.GetIndexSchema(DocTypeId)
    End Function

    Public Shared Function GetIndexSchemaAsDataSet(ByVal DocTypeId As Int64) As DataSet
        Return Indexs_Factory.GetIndexSchemaAsDataSet(DocTypeId)
    End Function

    Public Shared Function GetWordsByDocTypeAsDS(Optional ByVal DocTypeId As Int32 = 0, Optional ByVal IndexId As Int32 = 0) As DataSet
        Return Indexs_Factory.GetWordsByDocTypeAsDS(DocTypeId, IndexId)
    End Function


    ''' <summary>
    '''  Obtiene los indices de un entidad segun permisos
    ''' </summary>
    ''' <param name="DocTypeId">Id de entidad</param>
    ''' <param name="GUID">Id usuario/grupo</param>
    ''' <param name="_RightsType">Tupo de permiso a filtrar</param>
    ''' <returns>Dataset</returns>
    ''' <remarks></remarks>
    Public Function getIndexsByDocTypeIdAndRightTypeAsIIndex(ByVal DocType As IDocType, ByVal GUID As Int64, ByVal _RightsType As RightsType) As List(Of IIndex)
        Dim Indexs As New List(Of IIndex)
        Dim UB As New UserBusiness
        DocType.Indexs = GetIndexsSchemaAsListOfDT(DocType.ID)
        If New RightsBusiness().GetUserRights(GUID, Zamba.ObjectTypes.DocTypes, Zamba.Core.RightsType.ViewRightsByIndex, DocType.ID) Then
            Dim iri As Hashtable = UB.GetIndexsRights(DocType.ID, GUID)
            For Each ir As IndexsRightsInfo In iri.Values
                If ir.GetIndexRightValue(_RightsType) = True Then
                    Dim index As IIndex = DocType.Indexs.FirstOrDefault(Function(I) I.ID = ir.Indexid)
                    If index IsNot Nothing Then
                        Indexs.Add(index)
                    End If
                End If
            Next
        Else
            UB = Nothing
            Return DocType.Indexs
        End If
        Return Indexs
    End Function

    Public Shared Function getWordsByDocType(ByVal GUID As Int64, ByVal _RightsType As RightsType, Optional ByVal DocTypeId As Int32 = 0, Optional ByVal IndexId As Int32 = 0) As DataTable
        Dim dt As DataTable = IndexsBusiness.GetWordsByDocTypeAsDS(DocTypeId, IndexId).Tables(0)
        Return dt
    End Function


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Vector de objetos Index en base a un Doc_type_id
    ''' </summary>
    ''' <param name="DocTypeId">Id del Entidad</param>
    ''' <returns>Coleccion de objetos Index</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' 	[Marcelo]	16/10/2007	Modified
    ''' </history>
    ''' -----------------------------------------------------------------------------
    'Public Function GetIndexsSchema(ByVal DocTypeId As Int64) As List(Of IIndex)
    '    Dim i As Integer
    '    Dim ISF As New Indexs_Factory
    '    Dim dsTemp As DataSet = ISF.GetIndexsSchema(New List(Of Long) From {DocTypeId})
    '    ISF = Nothing

    '    If Not IsNothing(dsTemp) Then
    '        dsTemp.Tables(0).TableName = "DOC_INDEX"

    '        Dim Indexs As New List(Of IIndex)

    '        For i = 0 To dsTemp.Tables("DOC_INDEX").Rows.Count - 1
    '            Indexs.Add(New Index(dsTemp.Tables("DOC_INDEX")(i)("INDEX_ID"), dsTemp.Tables("DOC_INDEX")(i)("INDEX_NAME"), dsTemp.Tables("DOC_INDEX")(i)("INDEX_TYPE"), dsTemp.Tables("DOC_INDEX")(i)("INDEX_LEN"), False, False, dsTemp.Tables("DOC_INDEX")(i)("DROPDOWN"), False, False, dsTemp.Tables("DOC_INDEX")(i)("MUSTCOMPLETE"), String.Empty, dsTemp.Tables(0).Rows(i)("IndicePadre"), dsTemp.Tables(0).Rows(i)("IndiceHijo"),,, dsTemp.Tables(0).Rows(i)("IsReferenced")))
    '        Next

    '        Return Indexs
    '    Else
    '        Return Nothing
    '    End If
    'End Function



    ''' <summary>
    ''' Obtiene una lista de objetos Index en base a un Doc_type_id
    ''' </summary>
    ''' <param name="EntityID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIndexsSchemaAsListOfDT(ByVal EntityID As Int64) As Generic.List(Of IIndex)
        Dim i As Integer
        Dim dsTemp As DataSet


        If DocTypesAndIndexs.hsDocTypeIndexsDS.ContainsKey(EntityID) Then
            dsTemp = DocTypesAndIndexs.hsDocTypeIndexsDS(EntityID)
        Else
            Dim ISF As New Indexs_Factory
            dsTemp = ISF.GetIndexsSchema(New List(Of Long) From {EntityID})
            SyncLock (DocTypesAndIndexs.hsDocTypeIndexsDS)
                If DocTypesAndIndexs.hsDocTypeIndexsDS.ContainsKey(EntityID) = False Then
                    DocTypesAndIndexs.hsDocTypeIndexsDS.Add(EntityID, dsTemp)
                End If
            End SyncLock
            ISF = Nothing
        End If

        If Not IsNothing(dsTemp) Then
            dsTemp.Tables(0).TableName = "DOC_INDEX"

            Dim IndexsList As New Generic.List(Of IIndex)
            Dim index As IIndex
            Dim core As ZCore = ZCore.GetInstance()
            For i = 0 To dsTemp.Tables("DOC_INDEX").Rows.Count - 1
                index = New Index(dsTemp.Tables("DOC_INDEX")(i)("INDEX_ID"), dsTemp.Tables("DOC_INDEX")(i)("INDEX_NAME"), dsTemp.Tables("DOC_INDEX")(i)("INDEX_TYPE"),
                                         dsTemp.Tables("DOC_INDEX")(i)("INDEX_LEN"), False, False, dsTemp.Tables("DOC_INDEX")(i)("DROPDOWN"), False, False, If(IsDBNull(dsTemp.Tables("DOC_INDEX")(i)("MUSTCOMPLETE")), String.Empty, dsTemp.Tables("DOC_INDEX")(i)("MUSTCOMPLETE")),
                                         String.Empty, If(IsDBNull(dsTemp.Tables(0).Rows(i)("IndicePadre")), String.Empty, dsTemp.Tables(0).Rows(i)("IndicePadre")), If(IsDBNull(dsTemp.Tables(0).Rows(i)("IndiceHijo")), String.Empty, dsTemp.Tables(0).Rows(i)("IndiceHijo")), String.Empty, If(IsDBNull(dsTemp.Tables("DOC_INDEX")(i)("MINVALUE")), String.Empty, dsTemp.Tables("DOC_INDEX")(i)("MINVALUE")), If(IsDBNull(dsTemp.Tables("DOC_INDEX")(i)("MAXVALUE")), String.Empty, dsTemp.Tables("DOC_INDEX")(i)("MAXVALUE")), dsTemp.Tables(0).Rows(i)("IsReferenced"))

                If core.HtHierarchyRelation IsNot Nothing Then
                    index.HierarchicalChildID = core.HtHierarchyRelation(index.ID)
                End If

                IndexsList.Add(index)
            Next

            Return IndexsList
        Else
            Return Nothing
        End If
    End Function




    Public Function GetIndexsIdsAndNamesByEntityIdAsDictionary(ByVal EntityID As Int64) As Dictionary(Of Int64, String)
        Dim i As Integer
        Dim dsTemp As DataSet
        Dim core As ZCore = ZCore.GetInstance()
        Dim ISF As New Indexs_Factory
        If DocTypesAndIndexs.hsDocTypeIndexsDS.ContainsKey(EntityID) Then
            dsTemp = DocTypesAndIndexs.hsDocTypeIndexsDS(EntityID)
        Else
            dsTemp = ISF.GetIndexsSchema(New List(Of Long) From {EntityID})
            SyncLock (DocTypesAndIndexs.hsDocTypeIndexsDS)
                DocTypesAndIndexs.hsDocTypeIndexsDS.Add(EntityID, dsTemp)
            End SyncLock
        End If
        ISF = Nothing

        If Not IsNothing(dsTemp) Then
            dsTemp.Tables(0).TableName = "DOC_INDEX"

            Dim IndexsList As New Dictionary(Of Int64, String)
            For i = 0 To dsTemp.Tables("DOC_INDEX").Rows.Count - 1
                IndexsList.Add(dsTemp.Tables("DOC_INDEX")(i)("INDEX_ID"), dsTemp.Tables("DOC_INDEX")(i)("INDEX_NAME"))
            Next

            Return IndexsList
        Else
            Return Nothing
        End If
    End Function


    Public Function GetIndexsSchema(ByVal currentUserId As Int64, ByVal DocTypeIds As List(Of Int64)) As List(Of IIndex)
        Dim IndexsList As New Generic.List(Of IIndex)

        If Cache.DocTypesAndIndexs.hsSearchIndexs.ContainsKey(currentUserId & "-" & String.Join("-", DocTypeIds)) = False Then
            Dim EntitiesIds As New ArrayList
            EntitiesIds.AddRange(DocTypeIds)
            Dim core As ZCore = ZCore.GetInstance()
            IndexsList = core.FilterSearchIndex(EntitiesIds)
            For Each DTId As Int64 In DocTypeIds
                IndexsList = FilterIndexsByUserRights(DTId, Membership.MembershipHelper.CurrentUser.ID, IndexsList, RightsType.IndexSearch)
            Next
            SyncLock Cache.DocTypesAndIndexs.hsSearchIndexs
                If Cache.DocTypesAndIndexs.hsSearchIndexs.ContainsKey(currentUserId & "-" & String.Join("-", DocTypeIds)) = False Then
                    Cache.DocTypesAndIndexs.hsSearchIndexs.Add(currentUserId & "-" & String.Join("-", DocTypeIds), IndexsList)
                    Return IndexsList
                End If
            End SyncLock
        End If

        IndexsList = Cache.DocTypesAndIndexs.hsSearchIndexs.Item(currentUserId & "-" & String.Join("-", DocTypeIds))

        Return IndexsList
    End Function

    Public Function FilterIndexsByUserRights(doctypeId As Long, currentUserId As Long, indexs As List(Of IIndex), RightsType As RightsType) As List(Of IIndex)
        Dim UB As New UserBusiness
        Try
            Dim tempIndexs As New List(Of IIndex)

            'Si el usuario no tiene permisos sobre los indices marcado
            Dim RB As New RightsBusiness
            If RB.GetUserRights(currentUserId, ObjectTypes.DocTypes, RightsType.ViewRightsByIndex, doctypeId) = False Then
                tempIndexs.AddRange(indexs)
            Else
                For Each index As IIndex In indexs
                    If UB.GetIndexRightValue(doctypeId, index.ID, currentUserId, RightsType) Then
                        tempIndexs.Add(index)
                    End If
                Next
            End If


            Return tempIndexs
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return indexs
        Finally
            UB = Nothing
        End Try
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Crea un indice.
    ''' </summary>
    ''' <param name="Index">Objeto indice que se va a guardar</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' 	[Marcelo]	16/10/2007	Modified
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function AddIndex(ByVal Index As Index) As Integer
        Try
            If Index.AutoDisplay Then
                Index.AutoDisplay1 = 1
            Else
                Index.AutoDisplay1 = 0
            End If


            If Index.AutoFill Then
                Index.AutoFill1 = 1
            Else
                Index.AutoFill1 = 0
            End If

            If Index.Invisible Then
                Index.Invisible1 = 1
            Else
                Index.Invisible1 = 0
            End If

            If Index.NoIndex Then
                Index.NoIndex1 = 1
            Else
                Index.NoIndex1 = 0
            End If

            Return Indexs_Factory.AddIndex(CoreData.GetNewID(Zamba.Core.IdTypes.DOCINDEXID), Index.Name, Index.Type,
            Index.Len, Index.AutoFill1, Index.NoIndex1, Index.DropDown, Index.AutoDisplay1,
            Index.Invisible1, Index.Object_Type_Id)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Elimina un Indice de Zamba
    ''' </summary>
    ''' <param name="Index">Objeto Indice que se desea eliminar</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub DelIndex(ByVal Index As Index)
        Data.Indexs_Factory.DelIndex(Index.ID)
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Actualiza un indice en Zamba
    ''' </summary>
    ''' <param name="Index">Indice que se desea guardar</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub UpdateIndex(ByVal Index As Index)
        If Index.AutoDisplay Then
            Index.AutoDisplay1 = 1
        Else
            Index.AutoDisplay1 = 0
        End If

        If Index.AutoFill Then
            Index.AutoFill1 = 1
        Else
            Index.AutoFill1 = 0
        End If

        If Index.Invisible Then
            Index.Invisible1 = 1
        Else
            Index.Invisible1 = 0
        End If

        If Index.NoIndex Then
            Index.NoIndex1 = 1
        Else
            Index.NoIndex1 = 0
        End If

        'PACKAGE UPDATE_DOC_INDEX_pkg AS
        'PROCEDURE Update_DocIndex
        Try
            Dim cant As Int16
            cant = Indexs_Factory.UpdateIndex(Index.ID, Index.Name, Index.AutoDisplay1, Index.NoIndex1, Index.DropDown, Index.Invisible1)

            If cant > 0 Then
                MessageBox.Show("Ya existe un indice con ese nombre.", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Verifica si existe un indice con el mismo nombre que se le pasa por parametro
    ''' </summary>
    ''' <param name="IndexName">Nombre del indice que se desea verificar</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function IndexIsDuplicated(ByVal IndexName As String) As Boolean
        Return Indexs_Factory.IndexIsDuplicated(IndexName)
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Actualiza el valor de un indice de la lista de seleccion
    ''' </summary>
    ''' <param name="IndexId">ID del Indice que se desea modificar</param>
    ''' <param name="Item">Valor que se desea modificar</param>
    ''' <param name="NewItem">Nuevo valor que se desea guardar</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub UpdateListItem(ByVal IndexId As Integer, ByVal Item As String, ByVal NewItem As String)
        Indexs_Factory.UpdateListItem(IndexId, Item, NewItem)
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene el Nombre del Indice en base a su ID
    ''' </summary>
    ''' <param name="IndexId">Id del indice que se desea conocer el nombre</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetIndexName(ByVal IndexId As Integer) As String

        Return ZCore.GetInstance().GetIndex(IndexId).Name
    End Function



    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Datatable con los ids y nombres de todos los indices existentes en Zamba
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Marcelo]	04/05/2011	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetIndexsDsIdsAndNames() As DataSet
        Return Indexs_Factory.GetIndexsDsIdsAndNames
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Datatable con los nombres de todos los indices existentes en Zamba
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Marcelo]	09/10/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetIndexDsNames() As DataSet
        Return Indexs_Factory.GetIndexDsNames
    End Function



    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Dataset con todos los valores que aparecen en el indice
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetIndexValues() As DataSet
        Return Indexs_Factory.GetIndexValues
    End Function
    ''' <summary>
    ''' Obtiene el valor por defecto de un indice
    ''' </summary>
    ''' <param name="doctypeid">Id de entidad</param>
    ''' <param name="IndexId">Id de Indice</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>[Diego] created 4-07-2008</history>
    Public Shared Function GetIndexDefaultValuesByDoctypeId(ByVal doctypeiD As Long) As List(Of Indexs_Factory.IndexDefaultDTO)
        Try
            Return Indexs_Factory.GetIndexDefaultValuesByDoctypeId(doctypeiD)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Return New List(Of Indexs_Factory.IndexDefaultDTO)
    End Function

    ''' <summary>
    ''' Obtiene el valor por defecto de un indice
    ''' </summary>
    ''' <param name="doctypeid">Id de entidad</param>
    ''' <param name="IndexId">Id de Indice</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>[Diego] created 4-07-2008</history>
    Public Shared Function GetIndexDefaultValues(ByVal doctype As DocType) As Dictionary(Of Int64, String)
        Try
            Return Indexs_Factory.GetIndexDefaultValues(doctype.ID)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Return New Dictionary(Of Int64, String)
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Establece un indice como obligatorio para un Tipo de documento
    ''' </summary>
    ''' <param name="DocTypeId">Id del entidad que tendrá el indice obligatorio</param>
    ''' <param name="IndexId">Id del indice que sera obligatorio</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub SetIndexRequired(ByVal DocTypeId As Int64, ByVal IndexId As Int32)
        Indexs_Factory.SetIndexRequired(DocTypeId, IndexId)
    End Sub
    ''' <summary>
    ''' Quita la propiedad REQUERIDO de un indice para un Tipo de documento
    ''' </summary>
    ''' <param name="DocTypeId">Id del entidad</param>
    ''' <param name="IndexId">Id del indice</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Diego]	21/07/2008	Created
    ''' </history>
    Public Shared Sub DeleteIndexRequired(ByVal DocTypeId As Int64, ByVal IndexId As Int32)
        Indexs_Factory.DeleteIndexRequired(DocTypeId, IndexId)
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Establece un indice como Unico ( no permite repetir un valor)
    ''' </summary>
    ''' <param name="DocTypeId">Id del entidad que tendrá el indice unico</param>
    ''' <param name="IndexId">Id del indice que sera unico</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Diego]	13/03/2008	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub SetIndexUnique(ByVal DocTypeId As Int64, ByVal IndexId As Int32)
        Indexs_Factory.SetIndexUnique(DocTypeId, IndexId)
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Establece a un indice si el dropdown es de una vista o una tabla
    ''' </summary>
    ''' <param name="IndexId">Id del indice</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Marcelo]	07/10/2008	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub SetIndexDropDownType(ByVal IndexId As Int32, ByVal dropValue As Int32)
        Indexs_Factory.SetIndexDropDownType(IndexId, dropValue)
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Establece el dropdown de un indice
    ''' </summary>
    ''' <param name="IndexId">Id del indice</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------

    Public Shared Sub SetIndexDropDown(ByVal IndexId As Int32, ByVal dropValue As IndexAdditionalType)
        Indexs_Factory.SetIndexDropDown(IndexId, dropValue)
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Establece a un indice si el dropdown es de una vista o una tabla
    ''' </summary>
    ''' <param name="IndexId">Id del indice</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Marcelo]	07/10/2008	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    'Public Shared Function GetIndexDropDownType(ByVal IndexId As Int32) As Int64
    '    Return Indexs_Factory.GetIndexDropDownType(IndexId)
    'End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' quita la propiedad unico del indice
    ''' </summary>
    ''' <param name="DocTypeId">Id del entidad que tendrá el indice unico</param>
    ''' <param name="IndexId">Id del indice que sera unico</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Diego]	22/07/2008	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub RemoveIndexUnique(ByVal DocTypeId As Int64, ByVal IndexId As Int32)
        Indexs_Factory.RemoveIndexUnique(DocTypeId, IndexId)
    End Sub
    ''' <summary>
    ''' Establece un valor por defecto para un indice
    ''' </summary>
    ''' <param name="DocTypeId">Id del entidad que tendrá el indice unico</param>
    ''' <param name="IndexId">Id del indice que sera unico</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Diego]	02/07/2008	Created
    ''' </history>
    Public Shared Sub SetIndexDefaultValue(ByVal DocTypeId As Int64, ByVal IndexId As Int32, ByVal value As String, ByVal _IndexDatatype As IndexDataType)
        Indexs_Factory.SetIndexDefaultValue(DocTypeId, IndexId, value, _IndexDatatype)
    End Sub
    ''' <summary>
    ''' Borra el valor por defecto de un indice
    ''' </summary>
    ''' <param name="DocTypeId"></param>
    ''' <param name="IndexId"></param>
    ''' <remarks></remarks>
    '''<history>[Diego]	21/07/2008	[Created]</history>
    Public Shared Sub DeleteIndexDefaultValue(ByVal DocTypeId As Int64, ByVal IndexId As Int32)
        Indexs_Factory.DeleteIndexDefaultValue(DocTypeId, IndexId)
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo estatico que permite la visualización de un Indice, en un entidad para su exportacion
    ''' desde Lotus Notes
    ''' </summary>
    ''' <param name="Doctypeid">Id del DOC_TYPE que se desea mostrar</param>
    ''' <param name="index">Objeto Index que se va a mostrar en Lotus Notes</param>
    ''' <remarks>
    ''' El objeto Indice debe existir previamente en Zamba.
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub ShowInLotusNotes(ByVal Doctypeid As Int64, ByVal index As Index)
        Indexs_Factory.ShowInLotusNotes(Doctypeid, index.ID, index.DropDown)
    End Sub
    ''' <summary>
    ''' [sebastian 20-03-2009]Change value in autocomplete column into  index_r_doc_type_Businesss
    ''' </summary>
    ''' <history>   Marcelo Modified 22/09/2009
    ''' </history>
    ''' <param name="Doctypeid"></param>
    ''' <param name="indexId"></param>
    ''' <param name="dropDown"></param>
    ''' <remarks></remarks>
    Public Shared Sub BuildAutoincremental(ByVal Doctypeid As Int64, ByVal index As Index)
        Indexs_Factory.BuildAutoincremental(Doctypeid, index.ID, index.DropDown)
    End Sub
    ''' <summary>
    ''' Quita la propiedad Mostrar en lotus Notes de un indice para un Tipo de documento
    ''' </summary>
    ''' <param name="DocTypeId">Id del entidad</param>
    ''' <param name="IndexId">Id del indice</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Diego]	21/07/2008	Created
    ''' </history>
    Public Shared Sub DeleteShowInLotusNotes(ByVal DocTypeId As Int64, ByVal IndexId As Int32)
        Indexs_Factory.DeleteShowInLotusNotes(DocTypeId, IndexId)
    End Sub

    ''' <summary>
    ''' Delete the autoincremental value of the index
    ''' </summary>
    ''' <param name="DocTypeId"></param>
    ''' <param name="IndexId"></param>
    ''' <history> Marcelo Modified 22/09/2009 </history>
    ''' <remarks></remarks>
    Public Shared Sub DeleteAutoincremental(ByVal DocTypeId As Int64, ByVal IndexId As Int32)
        Indexs_Factory.DeleteAutoincremental(DocTypeId, IndexId)
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Arraylist con la lista de seleccion asociada a un Indice
    ''' </summary>
    ''' <param name="IndexID">ID del indice que se desea obtener la lista de selección</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    '''     [Marcelo}]  19/06/2009  Modified
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetDropDownList(ByVal IndexID As Int64) As List(Of String)

        If Cache.DocTypesAndIndexs.hsIndexsArray.ContainsKey(IndexID) Then
            Return Cache.DocTypesAndIndexs.hsIndexsArray(IndexID)
        Else
            Dim ds As List(Of String)
            ds = Indexs_Factory.retrieveArraylist(IndexID)
            Cache.DocTypesAndIndexs.hsIndexsArray.Add(IndexID, ds)
            Return ds
        End If

    End Function


    'Public Shared Function retrieveArraylistHierachical(ByVal DocTypeID As Integer, ByVal IndexID As Integer, ByVal ParentIndexs As Hashtable) As ArrayList
    '    Dim ds As New ArrayList
    '    If DocTypeID > 0 AndAlso IndexID > 0 Then
    '        ds = Indexs_Factory.retrieveArraylistHierachical(DocTypeID, IndexID, ParentIndexs)
    '    End If
    '    Return ds
    'End Function

#Region "Versiones"

    Public Shared Function GetIndexByPublishStates(ByVal result As PublishableResult) As Hashtable

        Dim hash As Hashtable = Nothing
        Try
            Dim ds As DataSet
            ds = Indexs_Factory.GetIndexByPublishStates(result.DocType.ID)

            hash = New Hashtable()
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For Each row As DataRow In ds.Tables(0).Rows
                        hash.Add(Int32.Parse(row.Item(0).ToString()), row.Item(1))
                    Next
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Return hash
    End Function
#End Region

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un DataTable con la lista de seleccion asociada a un Indice
    ''' </summary>
    ''' <param name="IndexID">ID del indice que se desea obtener la lista de selección</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function retrievetablelist(ByVal IndexID As Integer) As DataTable
        Return Indexs_Factory.retrievetablelist(IndexID)
    End Function

    Private Shared Function retrievearraytablelist(ByVal IndexID As Integer) As List(Of String)
        Return Indexs_Factory.retrievearraytablelist(IndexID)
    End Function

    Public Function GetIndexList(ByVal IndexID As Int64, ByVal DropDown As IndexAdditionalType, ByVal Value As String, ByVal LimitTo As Int64) As List(Of IIndexList)
        If (DropDown = IndexAdditionalType.DropDown OrElse DropDown = IndexAdditionalType.DropDownJerarquico) Then
            Return Indexs_Factory.GetIndexSimpleList(IndexID, Value, LimitTo)
        Else
            Return Indexs_Factory.GetIndexComplexList(IndexID, Value, LimitTo)
        End If
    End Function


    Public Shared Function GetDropDownListSearchCode(ByVal IndexId As Int64, ByVal Value As String) As Int32
        Return Indexs_Factory.GetDropDownListSerarchCode(IndexId, Value)
    End Function

    Public Shared Sub LoadAdditionalData(ByVal Index As Index)
        Select Case Index.DropDown
            Case IndexAdditionalType.DropDown, IndexAdditionalType.DropDownJerarquico
                Index.DropDownList = retrievearraytablelist(Index.ID)

            Case IndexAdditionalType.AutoSustitución, IndexAdditionalType.AutoSustituciónJerarquico

        End Select
    End Sub


    Public Shared Function getTableListParentName(ByVal IndexId As Int32) As String
        Return Indexs_Factory.getTableListParentName(IndexId)
    End Function

    Public Shared Function getTableListParentID(ByVal IndexId As Int32) As Int32
        Return Indexs_Factory.getTableListParentID(IndexId)
    End Function


    Public Shared Function getTableList(ByVal IndexId As Int32) As DataSet ' DSTableList
        Return Indexs_Factory.getTableList(IndexId)
    End Function





    ''' <summary>
    ''' Obtiene un atributo por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Javier]	18/10/2012	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetIndex(ByVal indexID As Int64) As IIndex

        If indexID = 0 Then Return Nothing

        Dim Ind As IIndex
        Dim DsIndex As DataSet
        DsIndex = GetIndexById(indexID)

        If DsIndex.Tables(0).Rows.Count > 0 Then
            Dim dr As DataRow = DsIndex.Tables("DOC_INDEX")(0)
            If Not IsDBNull(dr("DataTableName")) Then
                Ind = New Index(dr("INDEX_ID"), dr("INDEX_NAME"), dr("INDEX_TYPE"),
                                dr("INDEX_LEN"), False, False, dr("DROPDOWN"), False, False, 0,
                                String.Empty, dr("IndicePadre").ToString(), Nothing,
                                dr("DataTableName").ToString())
            Else
                Ind = New Index(dr("INDEX_ID"), dr("INDEX_NAME"), dr("INDEX_TYPE"),
                dr("INDEX_LEN"), False, False, dr("DROPDOWN"), False, False, 0,
                String.Empty, dr("IndicePadre").ToString(), Nothing,
                String.Empty)
            End If
            Ind.HierarchicalChildID = GetIndexChilds(Ind.ID)


            Ind.NoIndex = CBool(dr("NOINDEX"))
            If Ind.NoIndex = True Then Ind.NoIndex1 = 1 Else Ind.NoIndex1 = 0

            Ind.AutoDisplay = CShort(dr("AUTODISPLAY"))
            If Ind.AutoDisplay = True Then Ind.AutoDisplay1 = 1 Else Ind.AutoDisplay1 = 0
            Ind.Invisible = CBool(dr("INVISIBLE"))

            If Ind.Invisible = True Then Ind.Invisible1 = 1 Else Ind.Invisible1 = 0

            If CShort(dr("OBJECT_TYPE_ID")) = 0 Then
                Ind.Required = False
            Else
                Ind.Required = True
            End If

            Ind.DropDown = CShort(dr("DROPDOWN"))

            Ind.AutoFill = CBool(dr("INVISIBLE"))
            If Ind.AutoFill = True Then Ind.AutoFill1 = 1



        Else
            Return Nothing

        End If
        Return Ind
    End Function

    Public Function GetIndexById(ByVal indexID As Int64, ByVal value As String) As IIndex

        If indexID = 0 Then Return Nothing
        Dim CurrentIndex As IIndex = GetIndex(indexID)

        If CurrentIndex Is Nothing Then Return Nothing

        CurrentIndex.Data = value
        CurrentIndex.DataTemp = value
        CurrentIndex.dataDescription = value
        CurrentIndex.dataDescriptionTemp = value
        CurrentIndex.Data2 = value
        CurrentIndex.DataTemp2 = value
        CurrentIndex.dataDescription2 = value
        CurrentIndex.dataDescriptionTemp2 = value

        Return CurrentIndex


    End Function

    Public Shared Sub AddHierarchyItem(ByVal IndexId As Int32, ByVal IndexParentId As Int32,
                                       ByVal ParentValue As String, ByVal ChildValue As String)
        Dim DataTableName As String = String.Format(HIERARCHICALTABLENAME, IndexParentId, IndexId)
        Indexs_Factory.AddHierarchyItem(DataTableName, ParentValue, ChildValue)
    End Sub

    Public Shared Sub DeleteIndexListJerarquico(ByVal IndexId As Int32, ByVal IndexParentId As Int32)
        Indexs_Factory.DeleteIndexListJerarquico(IndexId)
        Dim DataTableName As String = String.Format(HIERARCHICALTABLENAME, IndexParentId, IndexId)
        Indexs_Factory.DeleteHierarchicalValueTable(DataTableName)
    End Sub

    Public Shared Function GetHierarchicalTable(ByVal IndexId As Long, ByVal IndexParentId As Long) As DataTable
        If IndexId > 0 AndAlso IndexParentId > 0 Then
            Dim DataTableName As String = String.Format(HIERARCHICALTABLENAME, IndexParentId, IndexId)
            Dim ParentTableName As String
            Dim ChildTableName As String
            Dim parentTypeDropDown As IndexAdditionalType = GetIndexDropDownType(IndexParentId)
            Dim childTypeDropDown As IndexAdditionalType = GetIndexDropDownType(IndexId)

            Try
                If parentTypeDropDown = IndexAdditionalType.DropDown Or parentTypeDropDown = IndexAdditionalType.DropDownJerarquico Then
                    ParentTableName = "ilst_i" & IndexParentId
                ElseIf parentTypeDropDown = IndexAdditionalType.AutoSustitución Or parentTypeDropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                    ParentTableName = "slst_s" & IndexParentId
                Else
                    Throw New Exception("El indice seleccionado no tiene una tabla asociada")
                End If

                If childTypeDropDown = IndexAdditionalType.DropDown Or childTypeDropDown = IndexAdditionalType.DropDownJerarquico Then
                    ChildTableName = "ilst_i" & IndexId
                ElseIf childTypeDropDown = IndexAdditionalType.AutoSustitución Or childTypeDropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                    ChildTableName = "slst_s" & IndexId
                Else
                    Throw New Exception("El indice seleccionado no tiene una tabla asociada")
                End If

                Return Indexs_Factory.GetHierarchicalTable(DataTableName, ParentTableName, ChildTableName)
            Finally
                DataTableName = Nothing
                ParentTableName = Nothing
                ChildTableName = Nothing
                parentTypeDropDown = Nothing
                childTypeDropDown = Nothing
            End Try
        Else
            Return Nothing
        End If
    End Function

    Public Function GetHierarchicalTableByValue(ByVal IndexId As Long,
                                                        ByVal ParentIndex As IIndex) As DataTable
        If (ParentIndex IsNot Nothing) Then
            Dim cacheKey As String = String.Format(HIERARCHICALTABLE_CACHEOPTIONS, IndexId, ParentIndex.ID, ParentIndex.Data)

            If Cache.DocTypesAndIndexs.hsHierarchicalTableByParentValue.Contains(cacheKey) Then

                Return Cache.DocTypesAndIndexs.hsHierarchicalTableByParentValue.Item(cacheKey)
            Else
                Dim DataTableName As String = String.Format(HIERARCHICALTABLENAME, ParentIndex.ID, IndexId)
                Dim HierarchyTable As DataTable = Indexs_Factory.GetHierarchicalTableByValue(DataTableName, ParentIndex)
                Dim index As IIndex = GetIndex(IndexId)

                If HierarchyTable IsNot Nothing Then
                    If index.DropDown = IndexAdditionalType.DropDown OrElse index.DropDown = IndexAdditionalType.DropDownJerarquico Then
                        HierarchyTable.Rows.Add(String.Empty)
                        Return HierarchyTable
                    End If

                    Dim max As Integer = HierarchyTable.Rows.Count - 1

                    HierarchyTable.Columns.Add("Description", String.Empty.GetType)

                    Dim desc As Object
                    Dim hierarchyValue As Object
                    Dim ASB As New AutoSubstitutionBusiness
                    For i As Integer = 0 To max
                        hierarchyValue = HierarchyTable.Rows(i).Item("Value")

                        If IsDBNull(hierarchyValue) Then
                            hierarchyValue = String.Empty
                        End If

                        desc = ASB.getDescription(hierarchyValue, IndexId)
                        If IsDBNull(desc) Then
                            HierarchyTable.Rows(i).Item("Description") = String.Empty
                        Else
                            HierarchyTable.Rows(i).Item("Description") = desc
                        End If

                    Next
                    ASB = Nothing
                    If HierarchyTable.Columns(0).DataType Is GetType(Decimal) = False AndAlso (index.Type = IndexDataType.Alfanumerico OrElse index.Type = IndexDataType.Alfanumerico_Largo) Then
                        HierarchyTable.Columns(0).DataType = GetType(String)
                        HierarchyTable.Rows.Add(String.Empty, "A Definir")
                    Else
                        HierarchyTable.Rows.Add(0, "A definir")
                    End If

                    HierarchyTable.DefaultView.Sort = "Description asc"

                    If Not Cache.DocTypesAndIndexs.hsHierarchicalTableByParentValue.Contains(cacheKey) Then
                        Cache.DocTypesAndIndexs.hsHierarchicalTableByParentValue.Add(cacheKey, HierarchyTable.DefaultView.ToTable())
                    End If

                    Return HierarchyTable.DefaultView.ToTable()
                End If
            End If
        End If
    End Function
    Public Function GetHierarchicalTableByValue(ByVal IndexId As Long,
                                                       ByVal IndexParentId As Long,
                                                       ByVal ParentValue As String
                                                      ) As DataTable

        Dim DataTableName As String = String.Format(HIERARCHICALTABLENAME, IndexParentId, IndexId)
        Dim HierarchyTable As DataTable = IndexsFactoryExt.GetHierarchicalTableByValue(DataTableName, ParentValue)
        Dim index As IIndex = GetIndex(IndexId)
        Dim ASB As New AutoSubstitutionBusiness

        If index.DropDown = IndexAdditionalType.DropDown OrElse index.DropDown = IndexAdditionalType.DropDownJerarquico Then
            HierarchyTable.Rows.Add(String.Empty)
            Return HierarchyTable
        End If

        Dim max As Integer = HierarchyTable.Rows.Count - 1

        HierarchyTable.Columns.Add("Description", String.Empty.GetType)

        For i As Integer = 0 To max
            HierarchyTable.Rows(i).Item("Description") = ASB.getDescription(HierarchyTable.Rows(i).Item("Value"),
                                                                                                 IndexId)
        Next

        If HierarchyTable.Columns(0).DataType Is GetType(Decimal) = False AndAlso (index.Type = IndexDataType.Alfanumerico OrElse index.Type = IndexDataType.Alfanumerico_Largo) Then
            HierarchyTable.Columns(0).DataType = GetType(String)
            HierarchyTable.Rows.Add(String.Empty, "A Definir")
        Else
            HierarchyTable.Rows.Add(0, "A definir")
        End If

        Return HierarchyTable
    End Function





    Public Shared Function ValidateHierarchyValue(ByVal ValueToValidate As String, ByVal IndexId As Long,
                                                  ByVal ParentIndexId As Long, ByVal ParentValue As String) As Boolean
        Dim DataTableName As String = String.Format(HIERARCHICALTABLENAME, ParentIndexId, IndexId)
        Return Indexs_Factory.ValidateHierarchyValue(ValueToValidate, DataTableName, ParentValue)
    End Function

    Public Shared Sub DeleteHierarchyValues(ByVal ListOfValues As List(Of String),
                                     ByVal IndexId As Int32, ByVal IndexParentId As Int32)
        Dim DataTableName As String = String.Format(HIERARCHICALTABLENAME, IndexParentId, IndexId)
        Indexs_Factory.DeleteHierarchyValues(ListOfValues, DataTableName)
    End Sub

    Public Shared Sub ModifyHierarchyValue(ByVal IndexID As Integer, ByVal ParentIndexID As Integer,
                                           ByVal ParentOldValue As String, ByVal ChildOldValue As String,
                                           ByVal ParentNewValue As String, ByVal ChildNewValue As String)
        Dim DataTableName As String = String.Format(HIERARCHICALTABLENAME, ParentIndexID, IndexID)
        Indexs_Factory.ModifyHierarchyValue(DataTableName, ParentOldValue,
                                            ChildOldValue, ParentNewValue, ChildNewValue)
    End Sub

    Public Shared Function IndexIsAsigned(ByVal index As Index) As Boolean
        Return Indexs_Factory.IndexIsAsigned(index)
    End Function

    Public Shared Sub delindexitems(ByVal IndexId As Int32, ByVal IndexList As ArrayList)
        Indexs_Factory.delindexitems(IndexId, IndexList)
    End Sub

    ''' <summary>
    ''' Actualiza o Inserta un registro en una tabla de tipo SLST
    ''' </summary>
    ''' <param name="IndexID">ID de la tabla SLST</param>
    ''' <param name="Code">Codigo del registro seleccionado</param>
    ''' <param name="ColumnDescName">Descripcion del registro seleccionado</param>
    ''' <history>
    ''' 	(pablo)	19/02/2011	Created
    ''' </history>

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Crea una tabla de sustitucion asociada a un Indice
    ''' </summary>
    ''' <param name="IndexId">ID del indice al que se le asignará la tabla de sustitucion</param>
    ''' <remarks>
    ''' Tabla de Codigo y descripcion
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub createsustituciontable(ByVal IndexId As Long, ByVal IndexLen As Int32, ByVal IndexType As IndexDataType)
        Indexs_Factory.createsustituciontable(IndexId, IndexLen, IndexType)
    End Sub



    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Destruye la tabla de sustitución asociada a un indice
    ''' </summary>
    ''' <param name="IndexId">Id del Indice</param>
    ''' <remarks>
    ''' Se elimina la tabla y su contenido
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub DeleteSustituciontable(ByVal IndexId As Int32)
        Indexs_Factory.DeleteSustituciontable(IndexId)
    End Sub
    Public Shared Sub DropTable(ByVal tabla As String)
        Indexs_Factory.DropTable(tabla)
    End Sub

    '<summary>
    'Gets a List of values from Indexs of Find Type
    '</summary>
    '<param name="taskId"></param>
    '<returns></returns>
    Public Shared Function LoadIndexFindTypeValues(ByVal IndexId As Int64) As DataTable
        Return Indexs_Factory.LoadIndexFindTypeValues(IndexId)
    End Function

    ''' <summary>
    ''' Método que llama a un método que sirve para verificar si el índice es un índice referenciado o no
    ''' </summary>
    ''' <param name="indexId">Id de un índice</param>
    ''' <param name="docTypeId">Id de un entidad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	18/12/2008	Created
    ''' </history>
    Public Shared Function verifyIfAIndexReferenced(ByVal indexId As Int64, ByVal docTypeId As Long) As Boolean
        Return (Indexs_Factory.verifyIfAIndexReferenced(indexId, docTypeId))
    End Function


    Public Shared Function SelectMaxIndexValue(ByVal IndexId As Int64, ByVal DocTypeId As Int64) As Int64
        Return Indexs_Factory.SelectMaxIndexValue(IndexId, DocTypeId)
    End Function

    Public Shared Function GetReferencedIndexsByDocTypeID(ByVal docTypeIdFrom As Int64, ByVal docTypeIdTo As Int64) As DataTable
        Return Indexs_Factory.GetReferencedIndexsByDocTypeID(docTypeIdFrom, docTypeIdTo)
    End Function

    Public Shared Sub ClearHashTables()
        If Not IsNothing(Cache.DocTypesAndIndexs.hsIndexsName) Then
            Cache.DocTypesAndIndexs.hsIndexsName.Clear()
            Cache.DocTypesAndIndexs.hsIndexsName = Nothing
            Cache.DocTypesAndIndexs.hsIndexsName = New SynchronizedHashtable()
        End If
        If Not IsNothing(Cache.DocTypesAndIndexs.hsIndexsArray) Then
            Cache.DocTypesAndIndexs.hsIndexsArray.Clear()
            Cache.DocTypesAndIndexs.hsIndexsArray = Nothing
            Cache.DocTypesAndIndexs.hsIndexsArray = New SynchronizedHashtable()
        End If
        If Not IsNothing(Cache.DocTypesAndIndexs.hsSustIndex) Then
            Cache.DocTypesAndIndexs.hsSustIndex.Clear()
            Cache.DocTypesAndIndexs.hsSustIndex = Nothing
            Cache.DocTypesAndIndexs.hsSustIndex = New SynchronizedHashtable()
        End If
        If Not IsNothing(Cache.DocTypesAndIndexs.hsDocTypeIndexsDS) Then
            Cache.DocTypesAndIndexs.hsDocTypeIndexsDS.Clear()
            Cache.DocTypesAndIndexs.hsDocTypeIndexsDS = Nothing
            Cache.DocTypesAndIndexs.hsDocTypeIndexsDS = New SynchronizedHashtable()
        End If
        _useIndexCache = String.Empty
    End Sub

    Shared Function GetHierarchicalRelations() As SynchronizedHashtable
        'Obtenemos la tabla con las relaciones, deben venir ordenadas por atributo padre
        'A su vez deben venir en el orden atributo padre|atributo hijo
        Dim dtHirarchy As DataTable = Indexs_Factory.GetHierarchicalRelations()

        'Si la tabla esta vacia o no tiene registros devuelvo vacio
        If dtHirarchy Is Nothing OrElse dtHirarchy.Rows.Count = 0 Then
            Return Nothing
        End If

        Dim max As Integer = dtHirarchy.Rows.Count
        Dim i As Integer = 0
        Dim currRow As DataRow
        Dim currID As Long = 0
        Dim htToReturn As New SynchronizedHashtable

        'Mientras haya filas para recorrer
        While i < max
            'Guardo la fila actual
            currRow = dtHirarchy(i)
            'Si el id actual cambia
            If currID <> currRow(0) Then
                'Asignamos el nuevo id
                currID = currRow(0)
                'Creamos la nueva lista
                htToReturn(currID) = New List(Of Long)
            End If
            'Guardo en la lista el nuevo valor
            DirectCast(htToReturn(currID), List(Of Long)).Add(currRow(1))

            i += 1
        End While

        Return htToReturn
    End Function

End Class
