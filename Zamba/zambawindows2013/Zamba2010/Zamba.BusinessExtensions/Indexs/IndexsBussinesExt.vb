Imports Zamba.Data

Public Class IndexsBussinesExt
    Private Shared HIERARCHICALTABLENAME As String = "ZHierarchy_I{0}_I{1}"

    ''' <summary>
    ''' Obtiene la tabla de las relaciones creadas entre padre e hijo
    ''' </summary>
    ''' <param name="IndexId"></param>
    ''' <param name="IndexParentId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetHierarchicalTable(ByVal IndexId As Int32, ByVal IndexParentId As Int32) As DataTable
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

                Return IndexsFactoryExt.GetHierarchicalTable(DataTableName, ParentTableName, ChildTableName)
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

    Public Shared Function getAllIndexesByDocTypeID(ByVal DocTypeID As Long) As List(Of IIndex)
        Dim IndexsFactory As IndexsFactoryExt = New IndexsFactoryExt()
        Dim indexs As DataTable = IndexsFactory.getAllindexByDocTypeID(DocTypeID)
        Dim listOfIndexs As New List(Of IIndex)
        For Each r As DataRow In indexs.Rows
            Dim Index As Index = New Index()
            Index.ID = r("Index_ID")
            Index.Name = r("Index_Name")
            Index.Type = r("Index_Type")
            Index.Len = r("Index_len")
            Index.AutoFill = r("AutoFill")
            Index.NoIndex = r("Noindex")
            Index.DropDown = r("Dropdown")
            Index.AutoDisplay = r("AutoDisplay")
            Index.Invisible = r("Invisible")
            Index.Object_Type_Id = r("Object_type_id")
            listOfIndexs.Add(Index)
        Next

        Return listOfIndexs
    End Function


    Public Shared Function GetHierarchicalTableByValue(ByVal IndexId As Long,
                                                       ByVal IndexParentId As Long, ByVal ParentValue As String, ByVal UseCache As Boolean) As DataTable
        Dim DataTableName As String = String.Format(HIERARCHICALTABLENAME, IndexParentId, IndexId)

        Dim HierarchyTable As DataTable = IndexsFactoryExt.GetHierarchicalTableByValue(DataTableName, ParentValue)
        Dim index As IIndex = IndexsBussinesExt.getIndex(IndexId, UseCache)

        If index.DropDown = IndexAdditionalType.DropDown OrElse index.DropDown = IndexAdditionalType.DropDownJerarquico Then
            HierarchyTable.Rows.Add(String.Empty)
            Return HierarchyTable
        End If

        Dim max As Integer = HierarchyTable.Rows.Count - 1

        HierarchyTable.Columns.Add("Description", String.Empty.GetType)

        For i As Integer = 0 To max
            HierarchyTable.Rows(i).Item("Description") = AutoSubstitutionBusiness.getDescription(HierarchyTable.Rows(i).Item("Value"),
                                                                                                 IndexId, False, index.Type)
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
        Return IndexsFactoryExt.ValidateHierarchyValue(ValueToValidate, DataTableName, ParentValue)
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un HashTable de Objetos Atributos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Javier]	18/10/2012	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function getAllIndexes() As Hashtable
        Dim DsIndex As DataSet
        Dim Ht As New Hashtable
        Dim Ind As Index
        Dim i As Integer

        DsIndex = IndexsFactoryExt.GetIndex()
        For i = 0 To DsIndex.Tables(0).Rows.Count - 1
            If Not IsDBNull(DsIndex.Tables(0).Rows(i)("DataTableName")) Then
                Ind = New Index(DsIndex.Tables(0)(i)("INDEX_ID"), DsIndex.Tables(0)(i)("INDEX_NAME"), DsIndex.Tables(0)(i)("INDEX_TYPE"), DsIndex.Tables(0)(i)("INDEX_LEN"),
                                False, False, DsIndex.Tables(0)(i)("DROPDOWN"), False, False, 0, String.Empty, DsIndex.Tables(0).Rows(i)("IndicePadre"), Nothing, DsIndex.Tables(0).Rows(i)("DataTableName"))
            Else
                Ind = New Index(DsIndex.Tables(0)(i)("INDEX_ID"), DsIndex.Tables(0)(i)("INDEX_NAME"), DsIndex.Tables(0)(i)("INDEX_TYPE"), DsIndex.Tables(0)(i)("INDEX_LEN"),
                                False, False, DsIndex.Tables(0)(i)("DROPDOWN"), False, False, 0, String.Empty, DsIndex.Tables(0).Rows(i)("IndicePadre"), Nothing, String.Empty)
            End If

            Ind.HierarchicalChildID = GetIndexChilds(Ind.ID)

            Ht.Add(Ind.ID, Ind)
        Next

        Return Ht
    End Function

    Public Shared Function GetIndexChilds(ByVal indexID As Long) As List(Of Long)

        Dim dt As DataTable = IndexsFactoryExt.GetIndexChilds(indexID)

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
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Dataset tipeado con todos los atributos de Doc_index
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Javier]	18/10/2012	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetIndexForAdministrator() As DataSet
        Return IndexsFactoryExt.GetIndex()
    End Function


    Public Shared Function getIndex(ByVal indexID As Int64, usecache As Boolean) As Index
        Dim DsIndex As DataSet

        If Cache.DocTypesAndIndexs.hsIndexs.ContainsKey(indexID) AndAlso usecache = True Then
            DsIndex = Cache.DocTypesAndIndexs.hsIndexs(indexID)
        Else
            DsIndex = Indexs_Factory.GetIndexById(indexID)
            If Cache.DocTypesAndIndexs.hsIndexs.ContainsKey(indexID) = False Then
                Cache.DocTypesAndIndexs.hsIndexs.Add(indexID, DsIndex)
            Else
                Cache.DocTypesAndIndexs.hsIndexs(indexID) = DsIndex
            End If
        End If

        Dim Ind As Index
        If DsIndex.Tables(0).Rows.Count > 0 Then
            If Not IsDBNull(DsIndex.Tables(0).Rows(0)("DataTableName")) Then
                Ind = New Index(DsIndex.Tables(0)(0)("INDEX_ID"), DsIndex.Tables(0)(0)("INDEX_NAME"), DsIndex.Tables(0)(0)("INDEX_TYPE"),
                                    DsIndex.Tables(0)(0)("INDEX_LEN"), False, False, DsIndex.Tables(0)(0)("DROPDOWN"), False, False, 0,
                                    String.Empty, DsIndex.Tables(0).Rows(0)("IndicePadre").ToString(), Nothing,
                                    DsIndex.Tables(0).Rows(0)("DataTableName").ToString())
            Else
                Ind = New Index(DsIndex.Tables(0)(0)("INDEX_ID"), DsIndex.Tables(0)(0)("INDEX_NAME"), DsIndex.Tables(0)(0)("INDEX_TYPE"),
                    DsIndex.Tables(0)(0)("INDEX_LEN"), False, False, DsIndex.Tables(0)(0)("DROPDOWN"), False, False, 0,
                    String.Empty, DsIndex.Tables(0).Rows(0)("IndicePadre").ToString(), Nothing,
                    String.Empty)
            End If
            Ind.HierarchicalChildID = GetIndexChilds(Ind.ID)
        End If
        Return Ind
    End Function

    ''' <summary>
    ''' Inserta en la tabla de jerarquia un padre y un hijo.
    ''' La relación siempre será uno a uno.
    ''' </summary>
    ''' <param name="IndexId">Id del indice(futuro hijo)</param>
    ''' <param name="IndexParentId">Id del indice(futuro padre)</param>
    ''' <returns>Retorna True si se pudo insertar, si la relación no es uno a uno retorna false</returns>
    Public Shared Function InsertIndexListJerarquico(ByVal IndexId As Int32, ByVal IndexParentId As Int32, ByVal UseCache As Boolean) As Boolean
        Dim DataTableName As String = String.Format(HIERARCHICALTABLENAME, IndexParentId, IndexId)
        Dim tempIndex As IIndex = IndexsBussinesExt.getIndex(IndexId, UseCache)
        Dim tempParentIndex As IIndex = IndexsBussinesExt.getIndex(IndexParentId, UseCache)


        Dim IndexTableName As String
        Dim ParentIndexTableName As String

        If tempIndex.DropDown = IndexAdditionalType.AutoSustitución OrElse
               tempIndex.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
            IndexTableName = "SLST_S" & IndexId
        Else
            IndexTableName = "ILST_I" & IndexId
        End If

        If tempParentIndex.DropDown = IndexAdditionalType.AutoSustitución OrElse
               tempParentIndex.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
            ParentIndexTableName = "SLST_S" & IndexParentId
        Else
            ParentIndexTableName = "ILST_I" & IndexParentId
        End If


        'Como la relacion es 1 padre un hijo:
        'Si el futuro hijo tiene un padre o si el futuro padre ya tenia un hijo
        'retorna false.
        If tempIndex.HierarchicalParentID > 0 AndAlso Indexs_Factory.ValidateHierarchicalValueTable(DataTableName) Then
            Return False
        ElseIf tempIndex.HierarchicalParentID = -1 Then
            Indexs_Factory.InsertIndexListJerarquico(IndexId, IndexParentId, DataTableName)
        End If

        Indexs_Factory.CreateHierarchicalValueTable(IndexId, IndexParentId, IndexTableName, ParentIndexTableName, DataTableName)
        Return True
    End Function
    Public Shared Function GetSchemeIlst(ByVal indexID As Long) As DataTable
        Return IndexsFactoryExt.GetSchemeIlst(indexID)
    End Function

    Public Shared Function GetSchemeSlst(ByVal indexID As Long) As DataTable
        Return IndexsFactoryExt.GetSchemeSlst(indexID)
    End Function

    Public Shared Function GetIlst(ByVal indexID As Long) As DataTable
        Return IndexsFactoryExt.GetIlst(indexID)
    End Function

    Public Shared Function GetSlst(ByVal indexID As Long) As DataTable
        Return IndexsFactoryExt.GetSlst(indexID)
    End Function


    ''' <summary>
    ''' Obtiene el tipo del atributo segun su id.
    ''' </summary>
    ''' <param name="indexid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetIndexDropDownType(ByVal indexid As Int64) As IndexAdditionalType
        Return WFTasksFactory.GetIndexDropDownType(indexid)
    End Function

    Public Shared Function GetColumnToWhere(ByVal columnType As String, ByVal columnValue As String) As String
        Return IndexsFactoryExt.GetColumnToWhere(columnType, columnValue)
    End Function

    Sub UpdateMinValue(ByVal doctypeid As Long, ByVal indexid As Long, ByVal minValue As String)
        IndexsFactoryExt.UpdateMinValue(doctypeid, indexid, minValue)
    End Sub

    Sub UpdateMaxValue(ByVal doctypeid As Long, ByVal indexid As Long, ByVal maxValue As String)
        IndexsFactoryExt.UpdateMaxValue(doctypeid, indexid, maxValue)
    End Sub

    ''' <summary>
    ''' Obtiene una lista de atributos con el id y nombre cargados
    ''' </summary>
    ''' <param name="docTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIndexIdAndName(ByVal docTypeId As Int64) As List(Of IIndex)
        Dim dt As DataTable = IndexsFactoryExt.GetIndexIdAndName(docTypeId)
        Dim list As New List(Of IIndex)

        For Each row As DataRow In dt.Rows
            list.Add(New Index(row(0), row(1).ToString))
        Next

        Return list
    End Function



    Public Function GetAsignedEntities(ByVal indexId As Int64) As DataTable
        Dim ife As New IndexsFactoryExt
        Dim dt As DataTable = ife.GetAsignedEntities(indexId)
        ife = Nothing
        Return dt
    End Function
End Class
