Imports Zamba.Core
Imports Zamba.Data
'Imports Zamba.Volumes
Imports Zamba.Servers
Imports System.IO
Imports System.Collections.Generic

'Imports Zamba.Volumes.Core

<Serializable()>
Public Class ZCore

    Private Shared _hsSingletonZCoreInstances As New SynchronizedHashtable


    Public Sub New()

    End Sub

    ''' <summary>
    ''' Obtiene la instancia actual de ZCore
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetInstance() As ZCore
        If Membership.MembershipHelper.isWeb Then
            Dim zCoreKey As String = Membership.MembershipHelper.CurrentUser.ID
            If Not _hsSingletonZCoreInstances.ContainsKey(zCoreKey) Then
                _hsSingletonZCoreInstances.Add(zCoreKey, New ZCore())
            End If
            Return _hsSingletonZCoreInstances.Item(zCoreKey)
        Else
            Return New ZCore()
        End If
    End Function

    ''' <summary>
    ''' Remueve la instancia actual de ZCore
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function RemoveCurrentInstance() As ZCore
        If Membership.MembershipHelper.isWeb AndAlso Membership.MembershipHelper.CurrentUser IsNot Nothing Then

            Dim zCoreKey As String = Membership.MembershipHelper.CurrentUser.ID
            If _hsSingletonZCoreInstances.ContainsKey(zCoreKey) Then
                _hsSingletonZCoreInstances.Remove(zCoreKey)
            End If
        End If
    End Function

    Public Shared Sub raiseerror(ByVal ex As Exception)
        Zamba.AppBlock.ZException.Log(ex)
    End Sub

    Dim FlagVolumesLoaded As Boolean = False

    Private DsCore As New DataSet
    Public FlagLoaded As Boolean

    Public Sections As New SynchronizedHashtable
    Public DocTypes As New Generic.Dictionary(Of Int64, DocType)
    Public Indexs As New SynchronizedHashtable
    Public Volumes As New SynchronizedHashtable
    Private _HtHierarchyRelation As New SynchronizedHashtable

    Public ReadOnly Property HtHierarchyRelation() As SynchronizedHashtable
        Get
            Return _HtHierarchyRelation
        End Get
    End Property


    Public Sub LoadCore()
        SyncLock DsCore
            If FlagLoaded = False Then
                FlagLoaded = True
                Try
                    FillDSDocTypes()
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                End Try
                Try
                    FillDocTypesChilds()
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                End Try
                Try
                    FillIndex()
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                End Try
                Try
                    FillVolumes()
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                End Try
            End If
        End SyncLock
    End Sub

    Public Sub Cleardata()
        DsCore = New DataSet
        Sections = New SynchronizedHashtable
        DocTypes = New Dictionary(Of Int64, DocType)
        Indexs = New SynchronizedHashtable
        Volumes = New SynchronizedHashtable
    End Sub

#Region "Sections"
    'Private Sub FillSections()
    '    DsCore.Merge(Section_Factory.GetDocGroups(RightFactory.CurrentUser.id))
    '    ZCore.FilterCurrentSections(RightFactory.GetAditional(Zamba.ObjectTypes.Archivos, Zamba.Core.RightsType.View))
    'End Sub
    'Private Sub FillSectionsChilds()
    '    Dim dstemp As DataSet = Section_Factory.GetDocGroupChilds(RightFactory.CurrentUser.id)
    '    dstemp.Tables(0).TableName = DsCore.Tables("DOC_TYPE")_R_DOC_TYPE_GROUP.TableName
    '    DsCore.Merge(dstemp)
    'End Sub
    'Public Sub FilterCurrentSections(ByVal SelectedSections As ArrayList)
    '    Dim DvSections As New DataView
    '    Dim First As Boolean = True
    '    Dim Where As New System.Text.StringBuilder
    '    Dim SectionsIds As New ArrayList
    '    Try
    '        DvUPSections.Table = DsCore.Tables("DOC_TYPE")_GROUP
    '        Dim x As Int32
    '        For x = 0 To SelectedUPSections.Count - 1
    '            If SectionsIds.IndexOf(SelectedSections(x)) = -1 Then
    '                SectionsIds.Add(SelectedSections)
    '                If First Then
    '                    Where.Append("DOC_TYPE_GROUP_ID = ")
    '                    Where.Append(SelectedSections(x))
    '                    First = False
    '                Else
    '                    Where.Append(" OR DOC_TYPE_GROUP_ID =")
    '                    Where.Append(SelectedSections(x))
    '                End If
    '            End If
    '        Next
    '        DvUPSections.RowFilter = Where.ToString
    '        Dim SectionsCount As Int32 = DvUPSections.Count - 1
    '        Dim i As Int32
    '        Dim Permitted As Boolean
    '        For i = 0 To DsCore.Tables("DOC_TYPE")_GROUP.Count - 1
    '            Dim z As Int32
    '            For z = 0 To SectionsCount
    '                If DsCore.Tables("DOC_TYPE")_GROUP(i).Tables("DOC_TYPE")_GROUP_ID = DvSections(z).Item("DOC_TYPE_GROUP_ID") Then
    '                    Permitted = True
    '                    Exit For
    '                End If
    '            Next
    '            If Permitted = False Then DsCore.Tables("DOC_TYPE")_GROUP(i).Delete()
    '            Permitted = False
    '        Next
    '        DsCore.AcceptChanges()
    '    Finally
    '        DvUPSections.Dispose()
    '        DvSections = Nothing
    '        Where = Nothing
    '        SectionsIds = Nothing
    '    End Try
    'End Sub
#End Region

#Region "Volumes"
    Private Function FillVolumes() As Boolean
        If FlagVolumesLoaded = False Then
            DsCore.Merge(VolumesBusiness.GetAllVolumes())
            FlagVolumesLoaded = True
        End If
    End Function
    Dim lastusedVolume As IVolume
    Public Function filterVolumes(ByVal VolumeId As Int32) As Volume
        LoadCore()
        If IsNothing(lastusedVolume) = False AndAlso lastusedVolume.ID = VolumeId Then
            Return lastusedVolume
        Else
            If Volumes.ContainsKey(VolumeId) Then
                Return Volumes(VolumeId)
            Else
                Dim DvVolumes As New DataView(DsCore.Tables("Disk_Volume"))
                DvVolumes.RowFilter = "Disk_Vol_Id = " & VolumeId

                Dim Vol As New Volume
                Vol.Name = DvVolumes(0).Item("DISK_VOL_NAME")
                Vol.Size = DvVolumes(0).Item("Disk_Vol_Size")
                Vol.VolumeType = DvVolumes(0).Item("Disk_Vol_Type")
                Vol.copy = DvVolumes(0).Item("Disk_Vol_Copy")
                Vol.path = DvVolumes(0).Item("Disk_Vol_Path")
                Vol.sizelen = DvVolumes(0).Item("Disk_Vol_Size_Len")
                Vol.state = DvVolumes(0).Item("Disk_Vol_State")
                Vol.offset = DvVolumes(0).Item("Disk_Vol_LstOffset")
                Vol.Files = DvVolumes(0).Item("Disk_Vol_Files")
                Vol.ID = DvVolumes(0).Item("Disk_Vol_Id")
                lastusedVolume = Vol
                Volumes.Add(VolumeId, Vol)
                Return Vol
            End If
        End If
    End Function
#End Region

#Region "Atributos"
    Dim FlagIndexLoaded As Boolean
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Funcion para obtener los Indices que comparten un conjunto de Doc_types
    ''' </summary>
    ''' <param name="SelectedDocTypes">Arraylist de Objetos Doc_Types</param>
    ''' <returns>Coleccion de objetos Indices</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	30/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function FilterIndex(ByVal SelectedDocTypes As ArrayList) As Zamba.Core.Index()

        Dim DvIndex As New DataView
        Dim DvIndexR As New DataView
        Dim i As Int32
        Dim Filter As New System.Text.StringBuilder
        Dim IndexData As New Hashtable
        Dim Where As New System.Text.StringBuilder
        Dim cAux As New SortedList

        Try
            LoadCore()
            DvIndexR.Table = DsCore.Tables("INDEX_R_DOC_TYPE")
            For i = 0 To SelectedDocTypes.Count - 1
                If i = 0 Then
                    Filter.Append("DOC_TYPE_ID = ")
                    'todo: sacar el boxing del arraylist de enteros
                    Filter.Append(SelectedDocTypes(i))
                Else
                    Filter.Append(" Or DOC_TYPE_ID = ")
                    Filter.Append(SelectedDocTypes(i))
                End If
            Next
            '        Filter &= " GROUP BY Index_Id HAVING (COUNT(Index_Id)) = " & SelectedDocTypes.Count

            'Select INDEX_ID FROM(INDEX_R_DOC_TYPE)
            'WHERE (DOC_TYPE_ID = 18) OR (DOC_TYPE_ID = 137)
            'GROUP BY INDEX_ID HAVING(COUNT(INDEX_ID) = 2)


            DvIndex.Table = DsCore.Tables("DOC_INDEX")
            DvIndexR.RowFilter = Filter.ToString
            DvIndexR.Sort = "INDEX_ID"
            Dim x As Int32
            Dim FlagFirst As Boolean = True
            ' Dim LastId As Int32
            If SelectedDocTypes.Count > 1 Then
                For x = 0 To DvIndexR.Count - SelectedDocTypes.Count
                    Try
                        If FlagFirst Then
                            If DvIndexR(x).Item("INDEX_ID") = DvIndexR(x + SelectedDocTypes.Count - 1).Item("INDEX_ID") Then
                                Where.Append("INDEX_ID = ")
                                Where.Append(DvIndexR(x).Item("INDEX_ID"))
                                IndexData.Add(CInt(DvIndexR(x).Item("INDEX_ID")), CBool(DvIndexR(x).Item("MUSTCOMPLETE")))
                                FlagFirst = False
                                ' LastId = DvIndexR(x).Item("INDEX_ID")
                            End If
                        Else
                            If DvIndexR(x).Item("INDEX_ID") = DvIndexR(x + SelectedDocTypes.Count - 1).Item("INDEX_ID") Then
                                Where.Append(" Or INDEX_ID =")
                                Where.Append(DvIndexR(x).Item("INDEX_ID"))
                                IndexData.Add(CInt(DvIndexR(x).Item("INDEX_ID")), CBool(DvIndexR(x).Item("MUSTCOMPLETE")))
                                '  LastId = DvIndexR(x).Item("INDEX_ID")
                            End If
                        End If
                    Catch ex As Exception
                        Zamba.Core.ZClass.raiseerror(ex)
                    End Try
                Next
            Else
                For x = 0 To DvIndexR.Count - 1
                    If FlagFirst Then
                        Where.Append("INDEX_ID = ")
                        Where.Append(DvIndexR(x).Item("INDEX_ID"))
                        IndexData.Add(CInt(DvIndexR(x).Item("INDEX_ID")), CBool(DvIndexR(x).Item("MUSTCOMPLETE")))
                        FlagFirst = False
                    Else
                        Where.Append(" Or INDEX_ID =")
                        Where.Append(DvIndexR(x).Item("INDEX_ID"))
                        IndexData.Add(CInt(DvIndexR(x).Item("INDEX_ID")), CBool(DvIndexR(x).Item("MUSTCOMPLETE")))
                    End If
                Next
            End If
            If FlagFirst = True Then Where.Append("INDEX_ID = 0")
            DvIndex.RowFilter = Where.ToString

            DvIndexR.Sort = "ORDEN"

            Dim IndexCount As Int32 = DvIndex.Count - 1

            Dim tempcindex(IndexCount) As Zamba.Core.Index

            'Obtiene todos los indices desde los DataView.
            'Agrega cada indice a una lista ordenada, utilizando como clave el Index_id de la tabla Doc_Index
            For i = 0 To IndexCount
                Dim mustcomplete As Boolean = CBool(IndexData(CInt(DvIndex(i).Item("Index_Id"))))
                Dim ind As Index
                If Not IsDBNull(DvIndex(i).Item("DataTableName")) Then
                    ind = New Index(DvIndex(i).Item("Index_Id"), DvIndex(i).Item("Index_Name"), DvIndex(i).Item("Index_Type"), DvIndex(i).Item("Index_Len"),
                                    False, False, DvIndex(i).Item("DropDown"), False, False, mustcomplete, String.Empty, DvIndex(i).Item("IndicePadre"), Nothing, DvIndex(i).Item("DataTableName"))
                Else
                    ind = New Index(DvIndex(i).Item("Index_Id"), DvIndex(i).Item("Index_Name"), DvIndex(i).Item("Index_Type"), DvIndex(i).Item("Index_Len"),
                                    False, False, DvIndex(i).Item("DropDown"), False, False, mustcomplete, String.Empty, DvIndex(i).Item("IndicePadre"), Nothing, String.Empty)
                End If

                If Not _HtHierarchyRelation(ind.ID) Is Nothing Then
                    ind.HierarchicalChildID = _HtHierarchyRelation(ind.ID)
                End If

                'Si el atributo no está repetido se agrega a la lista
                If Not cAux.ContainsKey(DvIndex(i).Item("Index_Id").ToString) Then
                    cAux.Add(DvIndex(i).Item("Index_Id").ToString, ind)
                End If
            Next
            'recorre todos los registros del dataView que contiene la relacion entre indices y documentos 
            'esta informacion se encuentra en la tabla Index_R_Doc_Type.
            'Si el indice se encuentra en la lista ordena cAux lo agrega a un array de atributos temporal tempcindex.
            'El dataView dvIndexR se encuentra oredena por la columna orden de la tabla Index_R_Doc_Type.
            Dim d As Int16
            Dim arrayagregados As New ArrayList
            For i = 0 To DvIndexR.Count - 1
                If cAux.ContainsKey(DvIndexR(i).Item("Index_Id").ToString) AndAlso Not arrayagregados.Contains(DvIndexR(i).Item("Index_Id").ToString) Then
                    tempcindex.SetValue(cAux.Item(DvIndexR(i).Item("Index_Id").ToString), d)
                    arrayagregados.Add(DvIndexR(i).Item("Index_Id").ToString)
                    d += +1
                End If
            Next
            Return tempcindex
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            Filter = Nothing
            DvIndex.Dispose()
            DvIndex = Nothing
            DvIndexR.Dispose()
            DvIndexR = Nothing
            Where = Nothing
            IndexData = Nothing
            cAux.Clear()
            cAux = Nothing
        End Try
        Return Nothing
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Funcion para obtener los Indices que comparten un conjunto de Doc_types
    ''' </summary>
    ''' <param name="SelectedDocTypes">Arraylist de Objetos Doc_Types</param>
    ''' <returns>Coleccion de objetos Indices</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	30/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function FilterSearchIndex(ByVal SelectedDocTypes As ArrayList) As List(Of IIndex)

        Dim DvIndex As New DataView
        Dim DvIndexR As New DataView
        Dim i As Int32
        Dim Filter As New System.Text.StringBuilder
        Dim IndexData As New Hashtable
        Dim Where As New System.Text.StringBuilder
        Dim cAux As New SortedList

        Try
            LoadCore()
            DvIndexR.Table = DsCore.Tables("INDEX_R_DOC_TYPE")

            Filter.Append("DOC_TYPE_ID In (")
            For i = 0 To SelectedDocTypes.Count - 1
                If i > 0 Then
                    Filter.Append(" ,")
                End If

                'todo: sacar el boxing del arraylist de enteros
                Filter.Append(SelectedDocTypes(i))
            Next
            Filter.Append(")")

            DvIndexR.Sort = "INDEX_ID"
            DvIndexR.RowFilter = Filter.ToString & " And INDEXSEARCH <> '1'"

            DvIndex.Table = DsCore.Tables("DOC_INDEX")
            Dim x As Integer

            If SelectedDocTypes.Count = 0 Then
                Where.Append("INDEX_ID = 0")
            Else
                Dim IndexIdsList As New List(Of Int64)
                Where.Append("INDEX_ID in (")

                For Each DocTypeId As Int64 In SelectedDocTypes
                    DvIndexR.RowFilter = "doc_type_id = " & DocTypeId
                    For Each indexrow As DataRow In DvIndexR.ToTable().Rows
                        If Not IndexIdsList.Contains(indexrow("Index_Id")) Then
                            IndexIdsList.Add(indexrow("Index_Id"))
                            Where.Append(indexrow("INDEX_ID"))
                            Where.Append(",")
                            IndexData.Add(CInt(indexrow("INDEX_ID")), CBool(indexrow("MUSTCOMPLETE")))
                        End If
                    Next
                Next
                Where.Append(")")
            End If

            DvIndex.RowFilter = Where.ToString

            DvIndexR.Sort = "ORDEN"

            Dim IndexCount As Int32 = DvIndex.Count - 1

            Dim tempcindex As New List(Of IIndex)

            'Obtiene todos los indices desde los DataView.
            'Agrega cada indice a una lista ordenada, utilizando como clave el Index_id de la tabla Doc_Index
            For i = 0 To IndexCount
                Dim mustcomplete As Boolean = CBool(IndexData(CInt(DvIndex(i).Item("Index_Id"))))

                'Si el indice no está repetido se agrega a la lista
                If Not cAux.ContainsKey(DvIndex(i).Item("Index_Id").ToString) Then
                    Dim ind As Index
                    If Not IsDBNull(DvIndex(i).Item("DataTableName")) Then
                        ind = New Index(DvIndex(i).Item("Index_Id"), DvIndex(i).Item("Index_Name"), DvIndex(i).Item("Index_Type"), DvIndex(i).Item("Index_Len"),
                                        False, False, DvIndex(i).Item("DropDown"), False, False, mustcomplete, String.Empty, DvIndex(i).Item("IndicePadre"), Nothing, DvIndex(i).Item("DataTableName"))
                    Else
                        ind = New Index(DvIndex(i).Item("Index_Id"), DvIndex(i).Item("Index_Name"), DvIndex(i).Item("Index_Type"), DvIndex(i).Item("Index_Len"),
                                        False, False, DvIndex(i).Item("DropDown"), False, False, mustcomplete, String.Empty, DvIndex(i).Item("IndicePadre"), Nothing, String.Empty)
                    End If

                    If Not _HtHierarchyRelation(ind.ID) Is Nothing Then
                        ind.HierarchicalChildID = _HtHierarchyRelation(ind.ID)
                    End If
                    cAux.Add(ind.ID.ToString, ind)
                End If
            Next
            'recorre todos los registros del dataView que contiene la relacion entre indices y documentos 
            'esta informacion se encuentra en la tabla Index_R_Doc_Type.
            'Si el indice se encuentra en la lista ordena cAux lo agrega a un array de atributos temporal tempcindex.
            'El dataView dvIndexR se encuentra oredena por la columna orden de la tabla Index_R_Doc_Type.
            Dim d As Int16
            Dim arrayagregados As New ArrayList
            For i = 0 To DvIndexR.Count - 1
                If cAux.ContainsKey(DvIndexR(i).Item("Index_Id").ToString) AndAlso Not arrayagregados.Contains(DvIndexR(i).Item("Index_Id").ToString) Then
                    tempcindex.Add(cAux.Item(DvIndexR(i).Item("Index_Id").ToString))
                    arrayagregados.Add(DvIndexR(i).Item("Index_Id").ToString)
                    d += +1
                End If
            Next
            Return tempcindex
        Catch ex As Exception
            Me.FilterIndex(SelectedDocTypes)
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            Filter = Nothing
            DvIndex.Dispose()
            DvIndex = Nothing
            DvIndexR.Dispose()
            DvIndexR = Nothing
            Where = Nothing
            IndexData = Nothing
            cAux.Clear()
            cAux = Nothing
        End Try
        Return Nothing
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Funcion para obtener los Indices que comparten un conjunto de Doc_types
    ''' </summary>
    ''' <param name="SelectedDocTypes">Coleccion de Objetos Doc_Types</param>
    ''' <returns>Coleccion de objetos Indices</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	30/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function FilterIndex(ByVal SelectedDocTypes() As DocType) As Zamba.Core.Index()

        Dim DvIndex As New DataView
        Dim DvIndexR As New DataView
        Dim i As Int32
        Dim Filter As New System.Text.StringBuilder
        Dim IndexData As New Hashtable
        Dim Where As New System.Text.StringBuilder
        Dim cAux As New SortedList

        Try
            LoadCore()
            DvIndexR.Table = DsCore.Tables("INDEX_R_DOC_TYPE")
            For i = 0 To SelectedDocTypes.Length - 1
                If i = 0 Then
                    Filter.Append("DOC_TYPE_ID = ")
                    Filter.Append(SelectedDocTypes(i).ID)
                Else
                    Filter.Append(" or DOC_TYPE_ID = ")
                    Filter.Append(SelectedDocTypes(i).ID)
                End If
            Next

            DvIndexR.RowFilter = Filter.ToString
            DvIndex.Table = DsCore.Tables("DOC_INDEX")
            Dim x As Int32
            Dim FlagFirst As Boolean = True
            '  Dim LastId As Int32
            If SelectedDocTypes.Length > 1 Then
                For x = 0 To DvIndexR.Count - 1
                    Try
                        If FlagFirst Then
                            If DvIndexR(x).Item("INDEX_ID") = DvIndexR(x + SelectedDocTypes.Length - 1).Item("INDEX_ID") Then
                                Where.Append("INDEX_ID = ")
                                Where.Append(DvIndexR(x).Item("INDEX_ID"))
                                IndexData.Add(DvIndexR(x).Item("INDEX_ID"), CBool(DvIndexR(x).Item("MUSTCOMPLETE")))
                                FlagFirst = False
                                '   LastId = DvIndexR(x).Item("INDEX_ID")
                            End If
                        Else
                            If DvIndexR(x).Item("INDEX_ID") = DvIndexR(x + SelectedDocTypes.Length - 1).Item("INDEX_ID") Then
                                Where.Append(" OR INDEX_ID =")
                                Where.Append(DvIndexR(x).Item("INDEX_ID"))
                                IndexData.Add(DvIndexR(x).Item("INDEX_ID"), CBool(DvIndexR(x).Item("MUSTCOMPLETE")))
                                '    LastId = DvIndexR(x).Item("INDEX_ID")
                            End If
                        End If
                    Catch ex As Exception
                        Zamba.Core.ZClass.raiseerror(ex)
                    End Try
                Next
            Else
                For x = 0 To DvIndexR.Count - 1
                    Try
                        If FlagFirst Then
                            Where.Append("INDEX_ID = ")
                            Where.Append(DvIndexR(x).Item("INDEX_ID"))
                            IndexData.Add(DvIndexR(x).Item("INDEX_ID"), CBool(DvIndexR(x).Item("MUSTCOMPLETE")))
                            FlagFirst = False
                        Else
                            Where.Append(" OR INDEX_ID =")
                            Where.Append(DvIndexR(x).Item("INDEX_ID"))
                            IndexData.Add(DvIndexR(x).Item("INDEX_ID"), CBool(DvIndexR(x).Item("MUSTCOMPLETE")))
                        End If
                    Catch ex As Exception
                        Zamba.Core.ZClass.raiseerror(ex)
                    End Try
                Next
            End If
            If FlagFirst = True Then Where.Append("INDEX_ID = 0")
            DvIndex.RowFilter = Where.ToString
            DvIndexR.Sort = "ORDEN"

            Dim IndexCount As Int32 = DvIndex.Count - 1
            Dim tempcindex(IndexCount) As Zamba.Core.Index
            '        ZCore.cindex = tempcindex

            'Obtiene todos los indices desde los DataView.
            'Agrega cada indice a una lista ordenada, utilizando como clave el Index_id de la tabla Doc_Index
            For i = 0 To IndexCount
                Dim mustcomplete As Boolean = CBool(IndexData(CInt(DvIndex(i).Item("Index_Id"))))

                'Si el indice no está repetido se agrega a la lista
                If Not cAux.ContainsKey(DvIndex(i).Item("Index_Id").ToString) Then
                    Dim ind As Index
                    If Not IsDBNull(DvIndex(i).Item("DataTableName")) Then
                        ind = New Index(DvIndex(i).Item("Index_Id"), DvIndex(i).Item("Index_Name"), DvIndex(i).Item("Index_Type"), DvIndex(i).Item("Index_Len"),
                                        False, False, DvIndex(i).Item("DropDown"), False, False, mustcomplete, String.Empty, DvIndex(i).Item("IndicePadre"), Nothing, DvIndex(i).Item("DataTableName"))
                    Else
                        ind = New Index(DvIndex(i).Item("Index_Id"), DvIndex(i).Item("Index_Name"), DvIndex(i).Item("Index_Type"), DvIndex(i).Item("Index_Len"),
                                        False, False, DvIndex(i).Item("DropDown"), False, False, mustcomplete, String.Empty, DvIndex(i).Item("IndicePadre"), Nothing, String.Empty)
                    End If

                    If Not _HtHierarchyRelation(ind.ID) Is Nothing Then
                        ind.HierarchicalChildID = _HtHierarchyRelation(ind.ID)
                    End If

                    cAux.Add(ind.ID.ToString, ind)
                End If
            Next
            'recorre todos los registros del dataView que contiene la relacion entre indices y documentos 
            'esta informacion se encuentra en la tabla Index_R_Doc_Type.
            'Si el indice se encuentra en la lista ordena cAux lo agrega a un array de atributos temporal tempcindex.
            'El dataView dvIndexR se encuentra oredena por la columna orden de la tabla Index_R_Doc_Type.
            For i = 0 To DvIndexR.Count - 1
                If cAux.ContainsKey(DvIndexR(i).Item("Index_Id").ToString) Then
                    tempcindex.SetValue(cAux.Item(DvIndexR(i).Item("Index_Id").ToString), i)
                End If
            Next

            'For i = 0 To IndexCount
            '    Dim mustcomplete As Boolean = CBool(IndexData(DvIndex(i).Item("Index_Id")))
            '    Dim ind As New Index(DvIndex(i).Item("Index_Id"), DvIndex(i).Item("Index_Name"), DvIndex(i).Item("Index_Type"), DvIndex(i).Item("Index_Len"), False, False, DvIndex(i).Item("DropDown"), False, False, mustcomplete)
            '    tempcindex.SetValue(ind, i)
            'Next

            Return tempcindex
        Catch ex As ExecutionEngineException
            Zamba.Core.ZClass.raiseerror(ex)
            Return Nothing
        Finally
            Filter = Nothing
            DvIndex.Dispose()
            DvIndex = Nothing
            DvIndexR.Dispose()
            DvIndexR = Nothing
            Where = Nothing
            IndexData = Nothing
            cAux.Clear()
            cAux = Nothing
        End Try
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un Arraylist con los Objetos Indices asignados al Doc_Type
    ''' </summary>
    ''' <param name="DocTypeId">ID del entidad que se desea conocer los indices asignados</param>
    ''' <returns>Arraylist de objetos Indices</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	30/05/2006	Created
    '''     [Tomas]     09/10/2009  Modified    Se comenta el hash IndexData ya que se cargaban  
    '''                                         los datos pero nunca era utilizado.
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function FilterIndex(ByVal DocTypeId As Int64, Optional ByVal loadDefaultValues As Boolean = False,
                                Optional ByVal loadMinMaxValues As Boolean = True) As List(Of IIndex)

        Dim DT As DocType

        Try
            LoadCore()
            Dim FilterR As String = "DOC_TYPE_ID = " & DocTypeId
            Dim DvIndexR As DataRow() = DsCore.Tables("INDEX_R_DOC_TYPE").Select(FilterR)
            Dim Indexs As New List(Of IIndex)

            If loadDefaultValues OrElse loadMinMaxValues Then
                DT = FilterDocTypes(DocTypeId)
                If DT IsNot Nothing Then
                    Dim IB As New IndexsBusiness
                    DT.Indexs.AddRange(IB.GetIndexsSchemaAsListOfDT(DT.ID))
                    IB = Nothing
                End If

                'CREA EL INDICE CON VALOR POR DEFECTO, UTILIZADO PARA INSERTAR NUEVOS DOCUMENTOS                            
                If DT.IndexsDefaultValues Is Nothing OrElse DT.IndexsDefaultValues.Count < 1 Then
                    DT.IndexsDefaultValues = IndexsBusiness.GetIndexDefaultValues(DT)
                End If

            End If


            For Each R As DataRow In DvIndexR

                Dim Rows As System.Data.DataRow() = DsCore.Tables("DOC_INDEX").Select("INDEX_ID = " & R("INDEX_ID"))

                If (Rows.Count > 0) Then
                    Dim DvIndex As DataRow = DsCore.Tables("DOC_INDEX").Select("INDEX_ID = " & R("INDEX_ID"))(0)

                    Dim ind As Index
                    Dim mustcomplete As Boolean = R("MustComplete")
                    Dim isreference As Boolean = R("ISREFERENCED")
                    Dim isAutoincremental As Boolean = False
                    If Not IsDBNull(R("Autocomplete")) Then
                        isAutoincremental = R("Autocomplete")
                    End If

                    If loadDefaultValues Then

                        Dim defaultValue As String = String.Empty
                        If DT.IndexsDefaultValues.ContainsKey(DvIndex("Index_Id")) Then
                            defaultValue = DT.IndexsDefaultValues.Item(DvIndex("Index_Id"))
                            If DvIndex("Index_Type") = 9 And defaultValue.Contains("(") And Server.isSQLServer = True Then
                                defaultValue = 0
                            End If
                        End If

                        If Not IsDBNull(DvIndex("DataTableName")) Then
                            ind = New Index(DvIndex("Index_Id"), DvIndex("Index_Name"), DvIndex("Index_Type"), DvIndex("Index_Len"),
                                                False, False, DvIndex("DropDown"), False, False, mustcomplete, defaultValue, DvIndex("IndicePadre"), Nothing, DvIndex("DataTableName"))
                        Else
                            ind = New Index(DvIndex("Index_Id"), DvIndex("Index_Name"), DvIndex("Index_Type"), DvIndex("Index_Len"),
                                                False, False, DvIndex("DropDown"), False, False, mustcomplete, defaultValue, DvIndex("IndicePadre"), Nothing, String.Empty)
                        End If
                    Else
                        'CREA EL INDICE SIN DEFAULT VALUE
                        If Not IsDBNull(DvIndex("DataTableName")) Then
                            ind = New Index(DvIndex("Index_Id"), DvIndex("Index_Name"), DvIndex("Index_Type"), DvIndex("Index_Len"),
                                                False, False, DvIndex("DropDown"), False, False, mustcomplete, String.Empty, DvIndex("IndicePadre"), Nothing, DvIndex("DataTableName"))
                        Else
                            ind = New Index(DvIndex("Index_Id"), DvIndex("Index_Name"), DvIndex("Index_Type"), DvIndex("Index_Len"),
                                                False, False, DvIndex("DropDown"), False, False, mustcomplete, String.Empty, -1, Nothing, String.Empty)
                        End If
                    End If

                    'Se cargan los valores minimos y maximos para el indice
                    If loadMinMaxValues Then
                        Try
                            ind.MinValue = (From currInd As IIndex In DT.Indexs
                                            Where currInd.ID = ind.ID
                                            Select currInd.MinValue).SingleOrDefault()
                            ind.MaxValue = (From currInd As IIndex In DT.Indexs
                                            Where currInd.ID = ind.ID
                                            Select currInd.MaxValue).SingleOrDefault()
                        Catch ex As Exception

                        End Try
                    End If

                    ind.isReference = isreference
                    ind.AutoIncremental = isAutoincremental

                    'Si existen los hijos del atributo, los agrego en la propiedad
                    If Not _HtHierarchyRelation(ind.ID) Is Nothing Then
                        ind.HierarchicalChildID = _HtHierarchyRelation(ind.ID)
                    End If

                    'Si el indice no está repetido se agrega a la lista
                    If Not Indexs.Contains(ind) Then
                        Indexs.Add(ind)
                    End If
                Else
                    ZTrace.WriteLineIf(ZTrace.IsError, String.Format("ERROR: EL Indice {0} no existe o no tiene permiso de verlo y esta asignado a la entidad {1}", R("INDEX_ID").ToString(), DocTypeId))
                End If
            Next

            Return Indexs
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            DT = Nothing
        End Try
        Return Nothing
    End Function
    Public Function FilterCIndex(ByVal DocTypeId As Int32) As Index()
        LoadCore()
        Dim DvIndex As New DataView
        Dim DvIndexR As New DataView
        Dim i As Int32
        Dim x As Int32
        Dim Where As New System.Text.StringBuilder
        Dim IndexData As New Hashtable
        Try
            DvIndexR.Table = DsCore.Tables("INDEX_R_DOC_TYPE")
            DvIndexR.RowFilter = "DOC_TYPE_ID = " & DocTypeId
            DvIndex.Table = DsCore.Tables("DOC_INDEX")
            For x = 0 To DvIndexR.Count - 1
                Try
                    If x = 0 Then
                        Where.Append("INDEX_ID in (")
                        Where.Append(DvIndexR(x).Item("INDEX_ID"))
                        If IndexData.ContainsKey(DvIndexR(x).Item("INDEX_ID")) = False Then IndexData.Add(DvIndexR(x).Item("INDEX_ID"), CBool(DvIndexR(x).Item("COMPLETE")))
                    Else
                        Where.Append(",")
                        Where.Append(DvIndexR(x).Item("INDEX_ID"))
                        If IndexData.ContainsKey(DvIndexR(x).Item("INDEX_ID")) = False Then IndexData.Add(DvIndexR(x).Item("INDEX_ID"), CBool(DvIndexR(x).Item("COMPLETE")))
                    End If
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                End Try
            Next
            Where.Append(")")
            DvIndex.RowFilter = Where.ToString

            Dim IndexCount As Int32 = DvIndex.Count - 1
            Dim CIndexs(IndexCount) As Index
            For i = 0 To IndexCount
                Dim mustcomplete As Boolean = CBool(IndexData(DvIndex(i).Item("Index_Id")))
                CIndexs.SetValue(New Index(DvIndex(i).Item("Index_Id"), DvIndex(i).Item("Index_Name"), DvIndex(i).Item("Index_Type"), DvIndex(i).Item("Index_Len"), False, False, DvIndex(i).Item("DropDown"), False, False, mustcomplete), i)
            Next
            Return CIndexs
        Catch ex As StackOverflowException
            Zamba.Core.ZClass.raiseerror(ex)
            Return Nothing
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return Nothing
        Finally
            DvIndex.Dispose()
            DvIndex = Nothing
            DvIndexR.Dispose()
            DvIndexR = Nothing
            Where = Nothing
            IndexData = Nothing
        End Try
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Funcion que devuelve un Objeto Indice en base a su ID
    ''' </summary>
    ''' <param name="IndexId">Id del indice que se desea obtener</param>
    ''' <returns>Objeto Index</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	30/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetIndex(ByVal IndexId As Int64) As Index
        Try
            If Indexs.ContainsKey(IndexId) Then
                Return New Index(Indexs(IndexId))
            Else
                LoadCore()
                Dim DvIndex As New DataView
                DvIndex.Table = DsCore.Tables("DOC_INDEX")
                Dim Where As New System.Text.StringBuilder
                Try
                    Where.Append("INDEX_ID = ")
                    Where.Append(IndexId)
                    DvIndex.RowFilter = Where.ToString
                    If DvIndex.Count > 0 Then
                        Dim index As New Index(DvIndex(0).Item("Index_Id"), DvIndex(0).Item("Index_Name"), DvIndex(0).Item("Index_Type"), DvIndex(0).Item("Index_Len"), False, False, DvIndex(0).Item("DropDown"), False, False, 0)
                        Indexs.Add(IndexId, index)
                        Return index
                    Else
                        Return Nothing
                    End If
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                    Return Nothing
                Finally
                    DvIndex.Dispose()
                    DvIndex = Nothing
                    Where = Nothing
                End Try
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function
    Private Function FillIndex() As Boolean
        If FlagIndexLoaded = False Then
            DsCore.Merge(Zamba.Core.IndexsBusiness.GetAllIndexs().Tables("DOC_INDEX"))

            'Obtenemos las relaciones y las almacenamos en chache
            _HtHierarchyRelation = IndexsBusiness.GetHierarchicalRelations()
            If _HtHierarchyRelation Is Nothing Then
                _HtHierarchyRelation = New SynchronizedHashtable()
            End If

            If DsCore.Tables("DOC_INDEX").Rows.Count > 0 Then
                FlagIndexLoaded = True
                Return True
            Else
                Return False
            End If
        End If
        Return True
    End Function

#End Region

#Region "DocTypes"
    Public Function FilterDocTypes(ByVal SelectedIndexs() As Zamba.Core.Index) As Zamba.Core.DocType()
        LoadCore()
        Dim DvDocType As New DataView
        Dim DvIndexR As New DataView
        Dim First As Boolean = True
        Dim Where As New System.Text.StringBuilder
        Dim DocTypesIds As New ArrayList
        Dim i As Int32
        Try
            DvIndexR.Table = DsCore.Tables("INDEX_R_DOC_TYPE")
            DvDocType.Table = DsCore.Tables("DOC_TYPE")
            For i = 0 To SelectedIndexs.Length - 1
                DvIndexR.RowFilter = "Index_ID = " & SelectedIndexs(i).ID
                Dim x As Int32
                For x = 0 To DvIndexR.Count - 1
                    If DocTypesIds.IndexOf(DvIndexR(x).Item("DOC_TYPE_ID")) = -1 Then
                        DocTypesIds.Add(DvIndexR(x).Item("DOC_TYPE_ID"))
                        If First Then
                            Where.Append("DOC_TYPE_ID = ")
                            Where.Append(DvIndexR(x).Item("DOC_TYPE_ID"))
                            First = False
                        Else
                            Where.Append(" OR DOC_TYPE_ID =")
                            Where.Append(DvIndexR(x).Item("DOC_TYPE_ID"))
                        End If
                    End If
                Next
                DvDocType.RowFilter = Where.ToString
            Next
            Dim DocTypeCount As Int32 = DvDocType.Count - 1
            Dim tempcdoctype(DocTypeCount) As Zamba.Core.DocType
            Dim b As Int32
            For b = 0 To DocTypeCount
                tempcdoctype.SetValue(New DocType(DvDocType(b).Item("Doc_Type_Id"), DvDocType(b).Item("Doc_Type_Name"), DvDocType(b).Item("File_Format_id"), DvDocType(b).Item("Disk_Group_Id"), DvDocType(b).Item("Thumbnails"), DvDocType(b).Item("Icon_Id"), DvDocType(b).Item("Object_Type_Id"), DvDocType(b).Item("AutoName"), DvDocType(b).Item("AutoName"), 0), b)
            Next
            Return tempcdoctype
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            DvDocType.Dispose()
            DvIndexR.Dispose()
            DvIndexR = Nothing
            Where = Nothing
            DocTypesIds = Nothing
        End Try
        Return Nothing
    End Function
    'Public Function FilterDocTypes(ByVal SelectedDocTypes As ArrayList) As Zamba.Core.DocType()
    '    LoadCore()
    '    Dim DvDocType As New DataView
    '    Dim First As Boolean = True
    '    Dim Where As New System.Text.StringBuilder
    '    Dim DocTypesIds As New ArrayList
    '    Try
    '        DvDocType.Table = DsCore.Tables("DOC_TYPE")
    '        Dim x As Int32
    '        For x = 0 To SelectedDocTypes.Count - 1
    '            If DocTypesIds.IndexOf(SelectedDocTypes(x)) = -1 Then
    '                DocTypesIds.Add(SelectedDocTypes)
    '                If First Then
    '                    Where.Append("DOC_TYPE_ID = ")
    '                    Where.Append(SelectedDocTypes(x))
    '                    First = False
    '                Else
    '                    Where.Append(" OR DOC_TYPE_ID =")
    '                    Where.Append(SelectedDocTypes(x))
    '                End If
    '            End If
    '        Next
    '        DvDocType.RowFilter = Where.ToString

    '        Dim DocTypeCount As Int32 = DvDocType.Count - 1
    '        Dim tempcdoctype(DocTypeCount) As Zamba.Core.DocType
    '        Dim b As Int32
    '        For b = 0 To DocTypeCount
    '            tempcdoctype.SetValue(New DocType(DvDocType(b).Item("DOC_TYPE_ID"), DvDocType(b).Item("DOC_TYPE_NAME"), DvDocType(b).Item("FILE_FORMAT_ID"), DvDocType(b).Item("DISK_GROUP_ID"), DvDocType(b).Item("THUMBNAILS"), DvDocType(b).Item("ICON_ID"), DvDocType(b).Item("OBJECT_TYPE_ID"), DvDocType(b).Item("AUTONAME"), DvDocType(b).Item("AUTONAME"), DvDocType(b).Item("DOCCOUNT"), 0, DvDocType(b).Item("DOCUMENTALID")), b)
    '        Next
    '        Return tempcdoctype
    '    Finally
    '        DvDocType.Dispose()
    '        Where = Nothing
    '        DocTypesIds = Nothing
    '    End Try
    'End Function
    Public Function FilterDocTypes(ByVal DocTypeId As Int32) As Zamba.Core.DocType
        If DocTypes.ContainsKey(DocTypeId) Then
            Return DocTypes(DocTypeId).Clone()
        Else
            LoadCore()
            Dim DvDocType As New DataView
            Dim Where As String

            DvDocType.Table = DsCore.Tables("DOC_TYPE")
            Where = "DOC_TYPE_ID = " & DocTypeId
            DvDocType.RowFilter = Where
            If DvDocType.Count > 0 Then
                Dim b As Int32
                Dim Doctype As New DocType(DvDocType(b).Item("DOC_TYPE_ID"), DvDocType(b).Item("DOC_TYPE_NAME"), DvDocType(b).Item("FILE_FORMAT_ID"), DvDocType(b).Item("DISK_GROUP_ID"), DvDocType(b).Item("THUMBNAILS"), DvDocType(b).Item("ICON_ID"), DvDocType(b).Item("OBJECT_TYPE_ID"), DvDocType(b).Item("AUTONAME"), DvDocType(b).Item("AUTONAME"), 0)
                DvDocType.Dispose()
                DvDocType = Nothing

                SyncLock DocTypes
                    If Not DocTypes.ContainsKey(DocTypeId) Then
                        DocTypes.Add(DocTypeId, Doctype)
                    End If
                End SyncLock
                'DocTypesBusiness.GetEditRights(Doctype) ESTO EN WEB ESTA DESCOMENTADO.
                Return Doctype.Clone()
            Else
                DvDocType.Dispose()
                DvDocType = Nothing
            End If
        End If
        Return Nothing
    End Function


    Public Function GetDocTypes() As List(Of IDocType)
        LoadCore()
        Dim DocTypes As New List(Of IDocType)
        Dim Where As String

        For Each r As DataRow In DsCore.Tables("DOC_TYPE").Rows
            Dim Doctype As New DocType(r.Item("DOC_TYPE_ID"), r.Item("DOC_TYPE_NAME"), r.Item("FILE_FORMAT_ID"), r.Item("DISK_GROUP_ID"), r.Item("THUMBNAILS"), r.Item("ICON_ID"), r.Item("OBJECT_TYPE_ID"), r.Item("AUTONAME"), r.Item("AUTONAME"), 0)
            DocTypes.Add(Doctype)
        Next
        Return DocTypes

    End Function



    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene el Autonombre asignado a un entidad
    ''' </summary>
    ''' <param name="DocTypeId">Id del Entidad para el cual se desea conocer el Autonombre</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	30/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetDocTypeAutoName(ByVal DocTypeId As Int32) As String
        If DocTypes.ContainsKey(DocTypeId) Then
            Return DirectCast(DocTypes(DocTypeId), DocType).AutoNameText
        Else
            LoadCore()
            Dim DvDocType As New DataView
            DvDocType.Table = DsCore.Tables("DOC_TYPE")
            DvDocType.RowFilter = "DOC_TYPE_ID = " & DocTypeId
            If DvDocType.Count > 0 Then
                Return DvDocType(0).Item("AUTONAME")
            Else
                Return String.Empty
            End If
        End If
    End Function
    Public Sub FilterCurrentDocTypes(ByVal SelectedDocTypes As ArrayList)
        LoadCore()
        Dim DvDocType As New DataView
        Dim First As Boolean = True
        Dim Where As New System.Text.StringBuilder
        Dim DocTypesIds As New ArrayList
        Try
            DvDocType.Table = DsCore.Tables("DOC_TYPE")
            Dim x As Int32
            For x = 0 To SelectedDocTypes.Count - 1
                If DocTypesIds.IndexOf(SelectedDocTypes(x)) = -1 Then
                    DocTypesIds.Add(SelectedDocTypes)
                    If First Then
                        Where.Append("DOC_TYPE_ID = ")
                        Where.Append(SelectedDocTypes(x))
                        First = False
                    Else
                        Where.Append(" OR DOC_TYPE_ID =")
                        Where.Append(SelectedDocTypes(x))
                    End If
                End If
            Next
            DvDocType.RowFilter = Where.ToString

            Dim DocTypeCount As Int32 = DvDocType.Count - 1
            Dim i As Int32
            Dim Permitted As Boolean
            For i = 0 To DsCore.Tables("DOC_TYPE").Rows.Count - 1
                Dim z As Int32
                For z = 0 To DocTypeCount
                    If DsCore.Tables("DOC_TYPE")(i)("DOC_TYPE_ID") = DvDocType(z).Item("DOC_TYPE_ID") Then
                        Permitted = True
                        Exit For
                    End If
                Next
                If Permitted = False Then DsCore.Tables("DOC_TYPE")(i).Delete()
                Permitted = False
            Next
            DsCore.AcceptChanges()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            DvDocType.Dispose()
            DvDocType = Nothing
            Where = Nothing
            DocTypesIds = Nothing
        End Try
    End Sub
    Private Sub FillDSDocTypes()
        DsCore.Merge(DocTypesFactory.GetDocTypesDsDocType())
    End Sub
    Private Sub FillDocTypesChilds()
        Dim DTF As New DocTypesFactory
        Dim dt As DataTable = DTF.GetDocTypesChilds()
        dt.TableName = "INDEX_R_DOC_TYPE"
        DsCore.Merge(dt)
    End Sub

#End Region


#Region "System"
    Private errorRB As ErrorReportBusiness

    Public Sub InitializeSystem(ByVal moduleName As String)

        Dim UP As New UserPreferences
        Dim ZOPTB As New ZOptBusiness
        Dim TB As New ToolsBusiness
        Dim DBB As New DBBusiness

        'If VariablesInterReglas.ZVarsRulesRepo Is Nothing Then
        '    VariablesInterReglas.ZVarsRulesRepo = New ZVarsRulesRepo()
        'End If

        DBB.InitializeDB()
        StartTrace(moduleName)
        ZOPTB.LoadAllOptions()
        UP.LoadAllMachineConfigValues()
        TB.loadGlobalVariables()

        ZOPTB = Nothing
        UP = Nothing
        TB = Nothing

    End Sub

    Public Sub VerifyFileServer()

        'Dim zoptb As New ZOptBusiness
        'Dim serverPath As String = If(zoptb.GetValue("DomainServerShare"), String.Empty)

        'If Not String.IsNullOrEmpty(serverPath) Then
        '    If Not New DirectoryInfo(serverPath).Exists Then
        '        Dim ex As New Exception("No existe o no esta disponible el servidor de archivos. Configurar y verificar ruta en variable DomainServerShare de zopt.")
        '        raiseerror(ex)
        '        '  Throw ex
        '    End If
        'End If
        'If Not String.IsNullOrEmpty(serverPath) Then
        '    If Not New DirectoryInfo(serverPath).Exists Then
        '        Dim ex As New Exception("No existe o no esta disponible el servidor de archivos. Configurar y verificar ruta en variable DomainServerShare de zopt.")
        '        raiseerror(ex)
        '        Throw ex
        '    End If
        'End If

    End Sub

    Public Sub StartTrace(ByVal moduleName As String)

        Dim UP As New UserPreferences
        Dim level As Int32
        Try
            level = Int32.Parse(UP.getValueForMachine("TraceLevel", Zamba.UPSections.UserPreferences, "4"))
        Catch
            level = 4
        End Try
        ZTrace.SetLevel(level, moduleName)
        '        If level > 0 Then ZTrace.WriteLine("Nivel de trace: " & level)
        UP = Nothing
    End Sub


#End Region


    Public Function InitWebPage(Optional optionalModule As String = "") As String

        If (Zamba.Servers.Server.ConInitialized = False) Then

            Dim UP As New UserPreferences
            Dim ZC As New Zamba.Core.ZCore()
            If optionalModule.Length > 0 Then
                ZC.InitializeSystem(optionalModule)
            Else
                ZC.InitializeSystem("Zamba.Web")
            End If
            Zamba.Membership.MembershipHelper.OptionalAppTempPath = UP.getValueForMachine("AppTempPath", Zamba.UPSections.UserPreferences, String.Empty)
            UP = Nothing

        End If
        Dim zoptb As New ZOptBusiness()
        Dim CurrentTheme As String = zoptb.GetValue("CurrentTheme")
        zoptb = Nothing
        Return CurrentTheme

    End Function


End Class