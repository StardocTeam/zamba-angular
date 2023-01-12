Imports Zamba.Data
Imports System.Collections.Generic
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
Public Class IndexsBusiness

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
#End Region
    Public Event saveIndex(ByVal noQuestion As Boolean)
    Public Shared Event CancelIndex(ByVal CancelSave As Boolean)
    Public Shared Event CloseSaveIndex()
    Public Shared Sub SaveIndexValue(Optional ByVal noQuestion As Boolean = True)
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
    ''' Compara un Atributo con un valor y comparador especificado.
    ''' </summary>
    ''' <history>
    ''' [Alejandro]
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function CompareIndex(ByVal indexValue As String,
    ByVal indexType As IndexDataType, ByVal comparator As Comparators,
     ByVal valueToCompare As String, ByVal casesensitive As Boolean
     ) As Boolean
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
    Public Shared Function GetIndex() As DSIndex
        Return Indexs_Factory.GetIndex()
    End Function

    Public Shared Function GetIndexByDropDownValue(ByVal type As IndexAdditionalType) As DSIndex
        Return Indexs_Factory.GetIndexByDropDownValue(type)
    End Function

    Public Shared Function GetIndexOfAnyDropDownType() As DSIndex
        Return Indexs_Factory.GetIndexOfAnyDropDownType()
    End Function

    Public Shared Function GetIndexById(ByVal id As Int32) As DSIndex
        Return Indexs_Factory.GetIndexById(id)
    End Function

    Public Shared Sub GetIndexIdAndTypeByName(ByVal _IndexName As String, ByRef _IndexID As Int64, ByRef _IndexType As IndexDataType)
        Try
            Dim dsTemp As DataSet
            dsTemp = Indexs_Factory.GetDocIndexByIndexName(_IndexName)
            If Not IsNothing(dsTemp.Tables(0)) Then
                For Each r As DataRow In dsTemp.Tables(0).Rows
                    _IndexID = Convert.ToInt64(r("INDEX_ID"))
                    _IndexType = Convert.ToInt16(r("INDEX_TYPE"))
                Next
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shared Function GetIndexDropDownType(ByVal indexid As Int64) As Int64
        Dim ret As Int16 = 0

        If Not String.IsNullOrEmpty(IndexsBusiness.GetIndexName(indexid, True)) Then
            ret = WFTasksFactory.GetIndexDropDownType(indexid)
        End If

        Return ret
    End Function

    Public Shared Function GetIndexById(ByVal indexID As Int64, ByVal value As String) As IIndex

        If indexID = 0 Then Return Nothing
        Dim CurrentIndex As IIndex = Nothing
        Dim CurrentIndexaux As IIndex = Nothing


        Dim ds As DataSet = Indexs_Factory.GetIndexById(indexID)
        Dim dr As DataRow

        If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then

            CurrentIndexaux = New Index

            dr = ds.Tables(0).Rows(0)

            CurrentIndexaux.ID = CInt(dr("INDEX_ID"))
            CurrentIndexaux.Name = dr("INDEX_NAME")
            CurrentIndexaux.Type = CInt(dr("INDEX_TYPE"))
            CurrentIndexaux.Len = CInt(dr("INDEX_LEN"))

            CurrentIndexaux.NoIndex = CBool(dr("NOINDEX"))
            If CurrentIndexaux.NoIndex = True Then CurrentIndexaux.NoIndex1 = 1 Else CurrentIndexaux.NoIndex1 = 0

            CurrentIndexaux.AutoDisplay = CShort(dr("AUTODISPLAY"))
            If CurrentIndexaux.AutoDisplay = True Then CurrentIndexaux.AutoDisplay1 = 1 Else CurrentIndexaux.AutoDisplay1 = 0
            CurrentIndexaux.Invisible = CBool(dr("INVISIBLE"))

            If CurrentIndexaux.Invisible = True Then CurrentIndexaux.Invisible1 = 1 Else CurrentIndexaux.Invisible1 = 0

            If CShort(dr("OBJECT_TYPE_ID")) = 0 Then
                CurrentIndexaux.Required = False
            Else
                CurrentIndexaux.Required = True
            End If

            CurrentIndexaux.DropDown = CShort(dr("DROPDOWN"))

            CurrentIndexaux.AutoFill = CBool(dr("INVISIBLE"))
            If CurrentIndexaux.AutoFill = True Then CurrentIndexaux.AutoFill1 = 1


        End If

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

    ''' <summary>
    ''' Obtiene los atributos a partir del id del id del documento
    ''' </summary>
    ''' <param name="docId">Id del documento</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetIndexs(ByVal docId As Int64, ByVal DocTypeId As Int64) As List(Of IIndex)
        Dim DtIndexs As List(Of IIndex) = IndexsBusiness.GetIndexsSchemaAsListOfDT(DocTypeId, True)

        Dim IndexIds As Object = Indexs_Factory.GetIndexsIdsByDocTypeId(DocTypeId)
        Dim IndexsValues As Dictionary(Of Int64, String) = Indexs_Factory.GetIndexValues(docId, DocTypeId, IndexIds)


        Dim Indexs As List(Of IIndex) = Nothing


        If Not IsNothing(DtIndexs) Then
            Indexs = New List(Of IIndex)

            For Each CurrentIndex As IIndex In DtIndexs

                If IndexsValues.ContainsKey(CurrentIndex.ID) Then
                    CurrentIndex.Data = IndexsValues.Item(CurrentIndex.ID)
                End If

                Indexs.Add(CurrentIndex)
            Next
        End If

        Return Indexs
    End Function

    ''' <summary>
    ''' Obtiene los atributos a partir del id del id del documento
    ''' </summary>
    ''' <param name="docId">Id del documento</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetResultIndexs(ByVal docId As Int64, ByVal DocTypeId As Int64, ByVal indexs As List(Of IIndex)) As List(Of IIndex)
        Dim IndexsValues As Dictionary(Of Int64, String) = Indexs_Factory.GetIndexValues(docId, DocTypeId, indexs)
        Dim i As Int16 = 0
        Dim indexsList As List(Of IIndex) = indexs

        For Each CurrentIndex As IIndex In indexs
            If IndexsValues.ContainsKey(CurrentIndex.ID) Then
                indexsList(i).Data = IndexsValues.Item(CurrentIndex.ID)
            End If
            i += 1
        Next

        Return indexsList
    End Function

    Public Shared Function GetAllIndexsIdsAndNames() As Dictionary(Of Int64, String)
        Dim Indexs As New Dictionary(Of Int64, String)
        Dim ds As DataSet = Indexs_Factory.GetIndex

        If Not IsNothing(ds) AndAlso ds.Tables.Count = 1 Then
            For Each CurrentRow As DataRow In ds.Tables(0).Rows
                Indexs.Add(CurrentRow("Index_Id"), CurrentRow("Index_Name"))
            Next

            ds.Dispose()
            ds = Nothing
        End If

        Return Indexs
    End Function

    Public Shared Function GetIndexNameById(ByVal Indexid As Int64) As String
        Return Indexs_Factory.GetIndexNameById(Indexid)
    End Function

    Public Shared Function GetIndexIdByName(ByVal IndexName As String) As Int64
        If Not Cache.DocTypesAndIndexs.hsIndexsName.ContainsKey(IndexName) Then
            Cache.DocTypesAndIndexs.hsIndexsName.Add(IndexName, Indexs_Factory.GetIndexIdByName(IndexName))
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
    Public Shared Function GetDistinctIndexValues(ByVal docTypeId As Int64, ByVal indexId As Int64, ByVal UserId As Int64, ByVal indexType As Int32) As DataSet
        'Traigo las restricciones y armo un string con ellas
        Dim restricc As String = RestrictionsMapper_Factory.GetRestrictionWebStrings(UserId, docTypeId)
        Return Indexs_Factory.GetDistinctIndexValues(docTypeId, indexId, restricc, indexType)
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un HashTable de Objetos Atributos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    '''     [Javier]    28/09/2011  Agregado el nombre de la tabla a levantar los datos
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function getAllIndexes() As Hashtable
        Dim DsIndex As DSIndex
        Dim Ht As New Hashtable
        Dim Ind As Index
        Dim i As Integer

        'Dim WhereString As String = "Index_R_Doc_Type.Doc_Type_Id = " & DocTypeId
        'Dim strSelect As New System.Text.StringBuilder
        'strSelect.Append("SELECT distinct (Doc_Index.Index_Id), Doc_Index.Index_Name, Doc_Index.Index_Type, Doc_Index.Index_Len, Doc_Index.DropDown,mustcomplete FROM Doc_Index, Index_R_Doc_Type")
        'strSelect.Append(WhereString)
        'strSelect.Append(" ORDER BY Index_R_Doc_Type.Orden")
        'Dim Dstemp As DataSet
        'Dstemp = Server.Con.ExecuteDataset(CommandType.Text, strSelect.ToString)
        'Dstemp.Tables(0).TableName = DsIndex.DOC_INDEX.TableName
        'DsIndex.Merge(Dstemp)
        'Dim Indexs(DsIndex.DOC_INDEX.Count - 1) As Index
        DsIndex = Indexs_Factory.GetIndex()
        For i = 0 To DsIndex.DOC_INDEX.Count - 1
            'Indexs.SetValue(New Index(DsIndex.DOC_INDEX(i).INDEX_ID, DsIndex.DOC_INDEX(i).INDEX_NAME, DsIndex.DOC_INDEX(i).INDEX_TYPE, DsIndex.DOC_INDEX(i).INDEX_LEN, False, False, DsIndex.DOC_INDEX(i).DROPDOWN, False, False, DsIndex.DOC_INDEX(i).MUSTCOMPLETE), i)
            If Not IsDBNull(DsIndex.Tables(0).Rows(i)("DataTableName")) Then
                Ind = New Index(DsIndex.DOC_INDEX(i).INDEX_ID, DsIndex.DOC_INDEX(i).INDEX_NAME, DsIndex.DOC_INDEX(i).INDEX_TYPE,
                                DsIndex.DOC_INDEX(i).INDEX_LEN, False, False, DsIndex.DOC_INDEX(i).DROPDOWN, False, False, 0,
                                String.Empty, DsIndex.Tables(0).Rows(i)("IndicePadre"), Nothing, DsIndex.Tables(0).Rows(i)("DataTableName"))
            Else
                Ind = New Index(DsIndex.DOC_INDEX(i).INDEX_ID, DsIndex.DOC_INDEX(i).INDEX_NAME, DsIndex.DOC_INDEX(i).INDEX_TYPE,
                                DsIndex.DOC_INDEX(i).INDEX_LEN, False, False, DsIndex.DOC_INDEX(i).DROPDOWN, False, False, 0,
                                String.Empty, DsIndex.Tables(0).Rows(i)("IndicePadre"), Nothing, String.Empty)
            End If
            Ht.Add(Ind.ID, Ind)
        Next

        Return Ht
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un HashTable de Objetos Atributos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------


    Public Shared Function GetIndexIdAndName() As DataTable
        Return Indexs_Factory.GetIndexIdAndName()
    End Function


    Public Shared Function GetIndexTypes(ByVal ValuesToModify As Boolean) As ArrayList
        Dim indexTypes As New ArrayList()
        indexTypes.Add("Numerico")
        indexTypes.Add("Numerico_Largo")
        indexTypes.Add("Numerico_Decimales")
        indexTypes.Add("Fecha")
        indexTypes.Add("Fecha_Hora")
        indexTypes.Add("Moneda")
        indexTypes.Add("Alfanumerico")
        indexTypes.Add("Alfanumerico_Largo")
        If Not ValuesToModify Then
            indexTypes.Add("Si_No")
        End If


        Return indexTypes
    End Function


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Dataset tipeado con los datos de Index_R_Doc_type para un Doc_Type especifico
    ''' </summary>
    ''' <param name="DocTypeId">Id de la entidad</param>
    ''' <returns>DsIndex</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetIndexSchema(ByVal docTypeId As Int64) As DSIndex
        Return Indexs_Factory.GetIndexSchema(docTypeId)
    End Function
    Public Shared Function GetIndexSchemaAsDataSet(ByVal docTypeId As Int64) As DataSet
        Return Indexs_Factory.GetIndexSchemaAsDataSet(docTypeId)
    End Function

    Public Shared Function GetIndexsIdsByDocTypeId(ByVal docTypeId As Int64) As List(Of Int64)
        Return Indexs_Factory.GetIndexsIdsByDocTypeId(docTypeId)
    End Function

    ''' <summary>
    '''  Obtiene los atributos de un entidad segun permisos
    ''' </summary>
    ''' <param name="DocTypeId">Id de entidad</param>
    ''' <param name="GUID">Id usuario/grupo</param>
    ''' <param name="_RightsType">Tupo de permiso a filtrar</param>
    ''' <returns>Dataset</returns>
    ''' <remarks></remarks>
    Public Shared Function getIndexByDocTypeId(ByVal docTypeId As Int64, ByVal GUID As Int64, ByVal _RightsType As RightsType) As DataTable
        Dim dttemp As DataTable = IndexsBusiness.GetIndexSchemaAsDataSet(docTypeId).Tables(0)
        Dim dt As DataTable = dttemp.Copy()


        Dim indexsids As New Generic.List(Of Int64)

        Dim iri As Hashtable = UserBusiness.Rights.GetIndexsRights(docTypeId, GUID, True, True)

        For Each ir As IndexsRightsInfo In iri.Values

            If ir.GetIndexRightValue(_RightsType) = True Then
                indexsids.Add(ir.Indexid)
            End If
        Next

        For drid As Integer = 0 To dttemp.Rows.Count - 1
            If indexsids.Contains(Int64.Parse(dttemp.Rows(drid).Item("Index_Id").ToString)) = False Then
                dt.Rows(drid).Delete()
            End If
        Next
        dt.AcceptChanges()
        Return dt

    End Function
    ''' <summary>
    ''' Obtiene los permisos de visualizacion de atributos para los asociados de la tarea
    ''' </summary>
    ''' <param name="DocTypeParentId">Entidad a la cual esta asociada la entidad</param>
    ''' <param name="DoctypeId">doctypeid asociado a la entidad</param>
    ''' <param name="userID">current user</param>
    ''' <returns>visibleIndexs</returns>
    ''' <history>   Pablo    Created     11/12/13</history>
    ''' <remarks></remarks>
    Public Shared Function GetAssociateDocumentIndexsRightsNonvisibleIndexs(ByVal DocTypeParentId As Int64, ByVal DoctypeId As String, ByVal userID As Int64) As List(Of Int64)
        Dim groupIds As List(Of Int64)
        Dim NonvisibleIndexs As New List(Of Int64)
        Dim Indexs As DataSet
        Dim Doc_type_Id As String = String.Empty
        Dim AssociateRight As Boolean

        groupIds = UserBusiness.GetUserGroupsIdsByUserid(userID)
        'si el doctypeid es de tipo string se lo formatea
        If Not IsNumeric(DoctypeId) Then
            For Each digit As Char In DoctypeId
                If Char.IsDigit(digit) Then
                    Doc_type_Id += digit
                End If
            Next
        End If
        'se valida el permiso de habilitacion de atributos asociados
        AssociateRight = RightsBusiness.GetUserRights(2, 133, Doc_type_Id)
        If AssociateRight Then
            Indexs = Indexs_Factory.GetAssociateDocumentIndexsRights(DocTypeParentId, Int64.Parse(Doc_type_Id), groupIds)
            For Each row As DataRow In Indexs.Tables(0).Rows
                NonvisibleIndexs.Add(row.Item(0))
            Next
        Else
            NonvisibleIndexs = Nothing
        End If

        Return NonvisibleIndexs
    End Function

  

    ''' <summary>
    ''' Obtiene una lista de objetos Index en base a un Doc_type_id
    ''' </summary>
    ''' <param name="DcoTypeID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Javier]    28/09/2011  Agregado el nombre de la tabla a levantar los datos
    ''' </history>
    public Shared Function GetIndexsSchemaAsListOfDT(ByVal DocTypeId As Int64, ByVal UseCache As Boolean) As Generic.List(Of IIndex)
        Dim DsIndex As New DSIndex
        Dim i As Integer

        If (Cache.DocTypesAndIndexs.hsIndexsSchemaOfDT.ContainsKey(DocTypeId) = True AndAlso UseCache = True) Then
            Return Cache.DocTypesAndIndexs.hsIndexsSchemaOfDT(DocTypeId)
        Else
            Dim dsTemp As DataSet = Indexs_Factory.GetIndexsSchema(DocTypeId)

            If Not IsNothing(dsTemp) Then
                dsTemp.Tables(0).TableName = DsIndex.DOC_INDEX.TableName
                DsIndex.Merge(dsTemp)

                Dim IndexsList As New Generic.List(Of IIndex)

                For i = 0 To DsIndex.DOC_INDEX.Count - 1
                    If Not IsDBNull(DsIndex.Tables(0).Rows(i)("DataTableName")) Then
                        IndexsList.Add(New Index(DsIndex.DOC_INDEX(i).INDEX_ID, DsIndex.DOC_INDEX(i).INDEX_NAME, DsIndex.DOC_INDEX(i).INDEX_TYPE,
                                                 DsIndex.DOC_INDEX(i).INDEX_LEN, False, False, DsIndex.DOC_INDEX(i).DROPDOWN, False, False, DsIndex.DOC_INDEX(i).MUSTCOMPLETE,
                                                 String.Empty, DsIndex.Tables(0).Rows(i)("IndicePadre"), Nothing, DsIndex.Tables(0).Rows(i)("DataTableName")))
                    Else
                        IndexsList.Add(New Index(DsIndex.DOC_INDEX(i).INDEX_ID, DsIndex.DOC_INDEX(i).INDEX_NAME, DsIndex.DOC_INDEX(i).INDEX_TYPE,
                                                 DsIndex.DOC_INDEX(i).INDEX_LEN, False, False, DsIndex.DOC_INDEX(i).DROPDOWN, False, False, DsIndex.DOC_INDEX(i).MUSTCOMPLETE,
                                                 String.Empty, DsIndex.Tables(0).Rows(i)("IndicePadre"), Nothing, String.Empty))
                    End If
                Next

                If (Cache.DocTypesAndIndexs.hsIndexsSchemaOfDT.ContainsKey(DocTypeId) = True) Then

                    Cache.DocTypesAndIndexs.hsIndexsSchemaOfDT(DocTypeId) = IndexsList
                Else
                    Cache.DocTypesAndIndexs.hsIndexsSchemaOfDT.Add(DocTypeId, IndexsList)
                End If
                Return IndexsList
            Else
                Return Nothing
            End If

        End If


    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un Dataset tipeado con los atributos en comun entre varios DOC_TYPEs
    ''' </summary>
    ''' <param name="DocTypeIds"></param>
    ''' <returns>DsIndex</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	28/06/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetIndexSchema(ByVal DocTypeIds As ArrayList) As DSIndex
        Return Indexs_Factory.GetIndexSchema(DocTypeIds)
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


            'If Index.DropDown = True Then Index.DropDown1 = 1 Else Index.DropDown1 = 0

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
    ''' Elimina un Atributo de Zamba
    ''' </summary>
    ''' <param name="Index">Objeto Atributo que se desea eliminar</param>
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
#Region "Attribute updates: functionality"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Actualiza longitud de un Atributo de Zamba
    ''' </summary>
    ''' <param name="Index">Indice</param>
    ''' <param name="IndexLength">Longitud a modificar</param>
    ''' <param name="OldIndexLength">Longitud previa a la modificacion</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Pablo]	26/03/2012	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub UpdateIndexLength(ByVal Index As Index, ByVal IndexLength As String, ByVal OldIndexLength As String)
        'obtengo las tablas en donde se encuentra el Atributo
        Dim dsDT As DataSet = Data.Indexs_Factory.GetDocTypesFromIndexRDocType(Index.ID)
        For Each Row As DataRow In dsDT.Tables(0).Rows

            If IndexLength > OldIndexLength Then
                'obtengo los registros a actualizar
                Dim dsDTID As DataTable = Results_Business.GetResults(Row.Item(0))
                For Each docID As DataRow In dsDTID.Rows
                    If Index.Type = IndexDataType.Alfanumerico Or Index.Type = IndexDataType.Alfanumerico_Largo Then
                        'trunco los datos de cada registro de tabla para actualizar la columna
                        Data.Indexs_Factory.TruncateIndexsLengthByDocTypeID(docID.Item(0), Row.Item(0), Index.ID, IndexLength, Index.Type)
                    End If
                Next
            End If

            'Modifico la columna
            Data.Indexs_Factory.UpdateIndexLengthByDocTypeID(Row.Item(0), Index, IndexLength)

        Next
        'actualizo la Doc_Index
        Data.Indexs_Factory.UpdateIndex(Index.ID, IndexLength, Nothing)
    End Sub

    ''' <summary>
    ''' Actualiza longitud de un Atributo de Zamba
    ''' </summary>
    ''' <param name="indexId">Indice</param>
    ''' <param name="IndexType">Longitud a modificar</param>
    ''' <param name="IndexLength">Longitud previa a la modificacion</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Pablo]	26/03/2012	Created
    ''' </history>
    Public Shared Sub AlterIndexSlst(ByVal indexId As String, ByVal type As String, ByVal len As String)
        Indexs_Factory.ExecIndexAlterSlst(indexId, type, len)
    End Sub


    Public Shared Function UpdateIndexType(ByVal IndexID As String, ByVal IndexType As String, ByVal IndexLength As String) As DataTable
        Dim VUnableToCast As Hashtable
        Dim DataUnableToCast As New DataTable

        'obtengo las tablas en donde se encuentra el indice
        Dim dsDT As DataSet = Data.Indexs_Factory.GetDocTypesFromIndexRDocType(IndexID)

        DataUnableToCast.Columns.Add("Entidad")
        DataUnableToCast.Columns.Add("ID Documento")
        DataUnableToCast.Columns.Add("Valor del Atributo")

        For Each Row As DataRow In dsDT.Tables(0).Rows
            'convierto los datos
            VUnableToCast = Data.Indexs_Factory.UpdateIndexType(Row.Item(0), IndexID, IndexType, IndexLength)

            If Not IsNothing(VUnableToCast) Then
                'obtengo los registros que no se pudieron castear
                For Each item As DictionaryEntry In VUnableToCast
                    DataUnableToCast.Rows.Add(DocTypesBusiness.GetDocTypeName(Row.Item(0), True), item.Key, item.Value)
                Next
            Else
                Return Nothing
            End If

        Next
        VUnableToCast = Nothing

        Return DataUnableToCast
    End Function

    ''' <summary>
    ''' Aplica los cambios de conversion de tipo de dato a un atributo
    ''' </summary>
    ''' <param name="IndexID">Indice</param>
    ''' <param name="IndexLength">Longitud a modificar</param>
    ''' <param name="IndexType">Tipo de indice</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Pablo]	26/03/2012	Created
    ''' </history>
    Public Shared Sub ConfirmUpdateIndexType(ByVal IndexID As String, ByVal IndexLength As String, ByVal IndexType As String)
        Dim dsDT As DataSet = Data.Indexs_Factory.GetDocTypesFromIndexRDocType(IndexID)
        For Each Row As DataRow In dsDT.Tables(0).Rows
            Data.Indexs_Factory.ConfirmUpdateIndexType(Row.Item(0), IndexID)
            'Vuelvo a Crear la vista
            DocTypesBusiness.CreateView(Row.Item(0))
        Next

        Select Case IndexType
            Case "Numerico"
                IndexType = "1"
            Case "Numerico_Largo"
                IndexType = "2"
            Case "Numerico_Decimales"
                IndexType = "3"
            Case "Fecha"
                IndexType = "4"
            Case "Fecha_Hora"
                IndexType = "5"
            Case "Moneda"
                IndexType = "6"
            Case "Alfanumerico"
                IndexType = "7"
            Case "Alfanumerico_Largo"
                IndexType = "8"
            Case "Si_No"
                IndexType = "9"
        End Select

        'actualizo la Doc_Index
        Data.Indexs_Factory.UpdateIndex(IndexID, IndexLength, IndexType)
    End Sub

    ''' <summary>
    ''' Descarta los cambios de conversion de tipo de dato a un atributo
    ''' </summary>
    ''' <param name="IndexID">Indice</param>
    ''' <param name="IndexLength">Longitud a modificar</param>
    ''' <param name="IndexType">Tipo de indice</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Pablo]	26/03/2012	Created
    ''' </history>
    Public Shared Sub DiscardUpdateIndexTypeChanges(ByVal IndexID As String)
        Dim dsDT As DataSet = Data.Indexs_Factory.GetDocTypesFromIndexRDocType(IndexID)
        For Each Row As DataRow In dsDT.Tables(0).Rows
            Data.Indexs_Factory.DiscardUpdateIndexTypeChanges(Row.Item(0))
        Next
    End Sub
#End Region
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Actualiza un indice en Zamba
    ''' </summary>
    ''' <param name="Index">Atributo que se desea guardar</param>
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
    ''' <param name="IndexId">ID del Atributo que se desea modificar</param>
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
    ''' Obtiene el Nombre del Atributo en base a su ID
    ''' </summary>
    ''' <param name="IndexId">Id del indice que se desea conocer el nombre</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetIndexName(ByVal IndexId As Integer, ByVal WithCache As Boolean) As String
        If Not WithCache Then Return Indexs_Factory.GetIndexName(IndexId)
        Return ZCore.GetIndex(IndexId).Name
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Arraylist con los nombre de todos los atributos existentes en Zamba
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetIndexNames() As ArrayList
        Return Indexs_Factory.GetIndexNames
        'Static arr As ArrayList = arr

        'If arr Is Nothing Then
        '    arr = New ArrayList
        '    arr.Add(DirectCast("", String))
        '    Dim strselect As String = "Select Index_Name from Doc_Index order by 1"
        '    Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strselect)
        '    For i As Int32 = 0 To ds.Tables(0).Rows.Count - 1
        '        arr.Add(DirectCast(ds.Tables(0).Rows(i).Item(0), String).Trim)
        '    Next
        'End If

        'Return arr
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Datatable con los ids y nombres de todos los atributos existentes en Zamba
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
    ''' Obtiene un Datatable con los nombres de todos los atributos existentes en Zamba
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
    ''' Obtiene el ID de un indice en base a su nombre
    ''' </summary>
    ''' <param name="IndexName">Nombre del indice del cual se desea conocer su ID</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetIndexId(ByVal IndexName As String) As Int32
        Return Indexs_Factory.GetIndexId(IndexName)
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Dataset con todos los valores que aparecen en el atributo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetIndexValues() As DSIndex
        Return Indexs_Factory.GetIndexValues
    End Function
    ''' <summary>
    ''' Obtiene el valor por defecto de un indice
    ''' </summary>
    ''' <param name="doctypeid">Id de entidad</param>
    ''' <param name="IndexId">Id de Atributo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>[Diego] created 4-07-2008</history>
    Public Shared Function GetIndexDefaultValues(ByVal doctype As DocType) As Dictionary(Of Int64, String)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "GetIndexDefaultValues")
        Dim IndexsId As New List(Of Int64)
        Try
            If doctype.Indexs Is Nothing Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "doctype.Indexs is Nothing")
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo los Indexs para doctype")
                doctype.Indexs = ZCore.FilterIndex(doctype.ID)
            End If
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Llenando la lista de IDs de indices")
            For Each _index As Index In doctype.Indexs
                IndexsId.Add(_index.ID)
            Next
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Indexs_Factory.GetIndexDefaultValues")
            Return Indexs_Factory.GetIndexDefaultValues(doctype.ID, IndexsId)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return Indexs_Factory.GetIndexDefaultValues(doctype.ID, IndexsId)
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Establece un indice como obligatorio para un Entidad
    ''' </summary>
    ''' <param name="DocTypeId">Id de la entidad que tendrá el atributo obligatorio</param>
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
    ''' Quita la propiedad REQUERIDO de un indice para un Entidad
    ''' </summary>
    ''' <param name="DocTypeId">Id de la entidad</param>
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
    ''' <param name="DocTypeId">Id de la entidad que tendrá el atributo unico</param>
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
    ''' <param name="DocTypeId">Id de la entidad que tendrá el atributo unico</param>
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
    ''' <param name="DocTypeId">Id de la entidad que tendrá el atributo unico</param>
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
    ''' Metodo estatico que permite la visualización de un Atributo, en un entidad para su exportacion
    ''' desde Lotus Notes
    ''' </summary>
    ''' <param name="Doctypeid">Id del DOC_TYPE que se desea mostrar</param>
    ''' <param name="index">Objeto Index que se va a mostrar en Lotus Notes</param>
    ''' <remarks>
    ''' El objeto Atributo debe existir previamente en Zamba.
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
    ''' Quita la propiedad Mostrar en lotus Notes de un indice para un Entidad
    ''' </summary>
    ''' <param name="DocTypeId">Id de la entidad</param>
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
    ''' Obtiene un Arraylist con la lista de seleccion asociada a un Atributo
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
    Public Shared Function retrieveArraylist(ByVal IndexID As Int64) As List(Of String)
        SyncLock Cache.DocTypesAndIndexs.hsIndexsArray
            If Cache.DocTypesAndIndexs.hsIndexsArray.ContainsKey(IndexID) Then
                Return Cache.DocTypesAndIndexs.hsIndexsArray(IndexID)
            Else
                Dim ds As List(Of String)
                ds = Indexs_Factory.retrieveArraylist(IndexID)
                Cache.DocTypesAndIndexs.hsIndexsArray.Add(IndexID, ds)
                Return ds
            End If
        End SyncLock
    End Function
    'Javier: Agregado el nombre de la tabla a levantar los datos
    Public Shared Function retrieveArraylistHierachical(ByVal DataTableName As String, ByVal IndexID As Integer, ByVal ParentIndexs As Hashtable) As ArrayList
        Dim ds As New ArrayList
        If Not String.IsNullOrEmpty(DataTableName) AndAlso IndexID > 0 Then
            ds = Indexs_Factory.retrieveArraylistHierachical(DataTableName, IndexID, ParentIndexs)
        End If
        Return ds
    End Function



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
    ''' Obtiene un DataTable con la lista de seleccion asociada a un Atributo
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
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve la lista de ITEMS que tiene asociado un indice.
    ''' </summary>
    ''' <param name="IndexId"></param>
    ''' <returns>Arraylist</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	28/06/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetDropDownList(ByVal IndexId As Integer) As List(Of String)
        Return retrieveArraylist(IndexId)
    End Function
    'Javier: Agregado el nombre de la tabla a levantar los datos
    Public Shared Function GetDropDownListHierachical(ByVal DataTableName As String, ByVal IndexID As Integer, ByVal ParentIndexs As Hashtable) As ArrayList
        Return retrieveArraylistHierachical(DataTableName, IndexID, ParentIndexs)
    End Function

    Public Shared Function GetDropDownListSerarchCode(ByVal IndexId As Int32, ByVal Value As String) As Int32
        Return Indexs_Factory.GetDropDownListSerarchCode(IndexId, Value)
    End Function

    Public Shared Sub LoadAdditionalData(ByVal Index As Index)
        Select Case Index.DropDown
            Case IndexAdditionalType.DropDown, IndexAdditionalType.DropDownJerarquico
                Index.DropDownList = retrievearraytablelist(Index.ID)

            Case IndexAdditionalType.AutoSustitución, IndexAdditionalType.AutoSustituciónJerarquico

        End Select
    End Sub

    Public Shared Function getTableListHierachical(ByVal IndexId As Int32) As DataSet ' DSTableList
        Return Indexs_Factory.getTableListHierachical(IndexId)
    End Function

    Public Shared Function getTableListParentName(ByVal IndexId As Int32) As String
        Return Indexs_Factory.getTableListParentName(IndexId)
    End Function

    Public Shared Function getTableListParentID(ByVal IndexId As Int32) As Int32
        Return Indexs_Factory.getTableListParentID(IndexId)
    End Function

    Public Shared Function getTableListHierachicalValueByIDs(ByVal IndexId As Int32, ByVal Codigo As Int32) As String
        Return Indexs_Factory.getTableListHierachicalValueByIDs(IndexId, Codigo)
    End Function

    Public Shared Function getTableList(ByVal IndexId As Int32) As DataSet ' DSTableList
        Return Indexs_Factory.getTableList(IndexId)
    End Function
    Public Shared Function GetTable(ByVal indexid As Int32) As DataSet
        Return Indexs_Factory.GetTable(indexid)
    End Function



    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Destruye la lista de busqueda asociada a un indice
    ''' </summary>
    ''' <param name="IndexId">Atributo del cual se eliminara la lista</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub delindexlist(ByVal IndexId As Int32)
        Indexs_Factory.delindexlist(IndexId)
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Crea una lista de busqueda para un indice
    ''' </summary>
    ''' <param name="IndexId">ID del Atributo para el cual se creara la lista</param>
    ''' <param name="IndexLen">Longitud maxima que representa los caracteres que podra contener como maximo cada palabra</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub addindexlist(ByVal IndexId As Int32, ByVal IndexLen As Int32)
        Indexs_Factory.addindexlist(IndexId, IndexLen)
    End Sub
    ''' <summary>
    ''' Crea una vista para una lista de sustitucion de un indice
    ''' </summary>
    ''' <param name="IndexId">Id del indice</param>
    ''' <param name="tableName">Nombre de la tabla a la q apuntara la vista</param>
    ''' <param name="columnCodName">Nombre de la columna q sera el codigo</param>
    ''' <param name="columnDescName">Nombre de la columna q sera la descripcion</param>
    ''' <history>Marcelo 07/10/08 Created</history>
    ''' <remarks></remarks>
    Public Shared Sub addindexlist(ByVal IndexId As Int32, ByVal tableName As String, ByVal columnCodName As String, ByVal columnDescName As String)
        Indexs_Factory.addindexlist(IndexId, tableName, columnCodName, columnDescName)
    End Sub

    ''' <summary>
    ''' Crea una vista para una tabla de busqueda de un indice
    ''' </summary>
    ''' <param name="IndexId">Id del indice</param>
    ''' <param name="tableName">Nombre de la tabla a la q apuntara la vista</param>
    ''' <param name="columnCodName">Nombre de la columna q sera el codigo</param>
    ''' <param name="columnDescName">Nombre de la columna q sera la descripcion</param>
    ''' <history>Marcelo 07/10/08 Created</history>
    ''' <remarks></remarks>
    Public Shared Sub addindexsust(ByVal IndexId As Int32, ByVal tableName As String, ByVal columnCodName As String, ByVal columnDescName As String)
        Indexs_Factory.addindexSust(IndexId, tableName, columnCodName, columnDescName)
    End Sub

    Public Shared Sub InsertIndexList(ByVal indexid As Int32, ByVal IndexList As ArrayList)
        Indexs_Factory.InsertIndexList(indexid, IndexList)
    End Sub

    'Public Shared Sub InsertIndexListJerarquico(ByVal IndexId As Int32, ByVal IndexParentId As Int32, ByVal Codigo As Int32, ByVal CodigoParent As Int32)
    '    Indexs_Factory.InsertIndexListJerarquico(IndexId, IndexParentId, Codigo, CodigoParent)
    'End Sub
    'Javier: Agregado el nombre de la tabla a levantar los datos

    ''' <summary>
    ''' Inserta en la tabla de jerarquia un padre y un hijo.
    ''' La relación siempre será uno a uno.
    ''' </summary>
    ''' <param name="IndexId">Id del indice(futuro hijo)</param>
    ''' <param name="IndexParentId">Id del indice(futuro padre)</param>
    ''' <returns>Retorna True si se pudo insertar, si la relación no es uno a uno retorna false</returns>
    'Public Shared Function InsertIndexListJerarquico(ByVal IndexId As Int32, ByVal IndexParentId As Int32) As Boolean
    'Dim DataTableName As String = String.Format(HIERARCHICALTABLENAME, IndexParentId, IndexId)
    'Dim tempIndex As IIndex = IndexsBusiness.GetIndex(IndexId)
    'Dim tempParentIndex As IIndex = IndexsBusiness.GetIndex(IndexParentId)

    ''Como la relacion es 1 padre un hijo:
    ''Si el futuro hijo tiene un padre o si el futuro padre ya tenia un hijo
    ''retorna false.
    'If tempIndex.HierarchicalParentID > 0 OrElse _
    '    tempParentIndex.HierarchicalChildID > 0 Then
    '    Return False
    'End If

    'Indexs_Factory.InsertIndexListJerarquico(IndexId, IndexParentId, DataTableName)

    'Dim IndexTableName As String
    'Dim ParentIndexTableName As String

    'If tempIndex.DropDown = IndexAdditionalType.AutoSustitución OrElse _
    '   tempIndex.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
    '    IndexTableName = "SLST_S" & IndexId
    'Else
    '    IndexTableName = "ILST_I" & IndexId
    'End If

    'If tempParentIndex.DropDown = IndexAdditionalType.AutoSustitución OrElse _
    '   tempParentIndex.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
    '    ParentIndexTableName = "SLST_S" & IndexParentId
    'Else
    '    ParentIndexTableName = "ILST_I" & IndexParentId
    'End If

    'Indexs_Factory.CreateHierarchicalValueTable(IndexId, IndexParentId, IndexTableName, ParentIndexTableName, DataTableName)

    'Return True
    ' End Function

    Public Shared Sub AddHierarchyItem(ByVal IndexId As Int32, ByVal IndexParentId As Int32,
                                       ByVal ParentValue As String, ByVal ChildValue As String)
        Dim DataTableName As String = String.Format(HIERARCHICALTABLENAME, IndexParentId, IndexId)
        Indexs_Factory.AddHierarchyItem(DataTableName, ParentValue, ChildValue)
    End Sub

    ''' <summary>
    ''' Borro la jerarquia del atributo
    ''' </summary>
    ''' <param name="IndexId"></param>
    ''' <param name="IndexParentId"></param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteIndexListJerarquico(ByVal IndexId As Int32, ByVal IndexParentId As Int32)
        Indexs_Factory.DeleteIndexListJerarquico(IndexId)
        Dim DataTableName As String = String.Format(HIERARCHICALTABLENAME, IndexParentId, IndexId)
        Indexs_Factory.DeleteHierarchicalValueTable(DataTableName)
    End Sub

    <Obsolete("Se da de baja el metodo, el nuevo se encuentra en BussinesExt")>
    Public Shared Function GetHierarchicalTable(ByVal IndexId As Int32, ByVal IndexParentId As Int32)
        If IndexId > 0 AndAlso IndexParentId > 0 Then
            Dim DataTableName As String = String.Format(HIERARCHICALTABLENAME, IndexParentId, IndexId)
            Return Indexs_Factory.GetHierarchicalTable(DataTableName)
        Else
            Return Nothing
        End If
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
    Public Shared Sub InsertIndexSust(ByVal IndexId As String, ByVal Code As String, ByVal ColumnDescName As String)

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando si se debe incluir la tabla SLST_S" + IndexId.ToString + " en memoria")
        If Cache.DocTypesAndIndexs.hsSustIndex.Contains(CLng(IndexId)) = False Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Incluyendo la tabla en memoria")
            Dim dt As DataTable = New DataTable
            Dim ds As DataSet
            Dim f As Int16
            dt.Columns.Add("Codigo")
            dt.Columns.Add("Descripcion")
            Dim columns() As DataColumn = New DataColumn() {dt.Columns("Codigo")}
            dt.PrimaryKey = columns
            dt.TableName = IndexId

            ds = Indexs_Factory.GetSustTable(IndexId, 0)
            If ds.Tables(0).Rows.Count > 0 Then
                For f = 0 To ds.Tables(0).Rows.Count - 1
                    dt.Rows.Add(ds.Tables(0).Rows(f).Item(0).ToString(), ds.Tables(0).Rows(f).Item(1).ToString())
                Next
            End If
            Cache.DocTypesAndIndexs.hsSustIndex.Add(IndexId, dt)
        Else
            ZTrace.WriteLineIf(ZTrace.IsInfo, "La tabla SLST_S" + IndexId.ToString + " ya se encuentra en memoria")
        End If

        Dim dsSust As DataSet = Indexs_Factory.GetSustTable(IndexId, Code)

        If dsSust.Tables(0).Rows.Count > 0 Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Existe un registro con los datos ingresados. Actualizando.")
            Try
                Indexs_Factory.UpdateindexSust(IndexId, Code, ColumnDescName)
            Catch ex As Exception
                ZClass.raiseerror(ex)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error. La actualizacion no ha podido completarse")
            End Try
        Else
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Insertando nuevo registro")
            Try
                Indexs_Factory.InsertSustIndex(IndexId, Code, ColumnDescName)
            Catch ex As Exception
                ZClass.raiseerror(ex)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error. La insercion no ha podido completarse")
            End Try
        End If

        Try
            'elimino la tabla del cache
            Cache.DocTypesAndIndexs.hsSustIndex.Remove(CLng(IndexId))

            'vuelvo a agragar la tabla actualizada
            Dim UpdatedDt As DataTable = New DataTable
            Dim UpdatedDs As DataSet
            Dim f As Int16
            UpdatedDt.Columns.Add("Codigo")
            UpdatedDt.Columns.Add("Descripcion")
            Dim columns() As DataColumn = New DataColumn() {UpdatedDt.Columns("Codigo")}
            UpdatedDt.PrimaryKey = columns
            UpdatedDt.TableName = IndexId

            UpdatedDs = Indexs_Factory.GetSustTable(IndexId, 0)
            If UpdatedDs.Tables(0).Rows.Count > 0 Then
                For f = 0 To UpdatedDs.Tables(0).Rows.Count - 1
                    UpdatedDt.Rows.Add(UpdatedDs.Tables(0).Rows(f).Item(0).ToString(), UpdatedDs.Tables(0).Rows(f).Item(1).ToString())
                Next
            End If
            Cache.DocTypesAndIndexs.hsSustIndex.Add(CLng(IndexId), UpdatedDt)


        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Crea una tabla de sustitucion asociada a un Atributo
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
    ''' <param name="IndexId">Id del Atributo</param>
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
    ''' Obtiene si un indice esta declarado como referencial o no
    ''' </summary>
    ''' <param name="docTypeID"></param>
    ''' <param name="indexID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getReferenceStatus(ByVal docTypeID As Int64, ByVal indexID As Int64) As Boolean
        Return Indexs_Factory.getReferenceStatus(docTypeID, indexID)
    End Function

    ''' <summary>
    ''' Método que llama a un método que sirve para verificar si el Atributo es un Atributo referenciado o no
    ''' </summary>
    ''' <param name="indexId">Id de un Atributo</param>
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

    ''' <summary>
    ''' Metodo para obtener en un hashtable(por id) los hijos de cada atrubuto
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetHierarchicalRelations() As Hashtable
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
        Dim htToReturn As New Hashtable

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
