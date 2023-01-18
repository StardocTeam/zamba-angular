
Imports Zamba.Data
Imports Zamba.Servers

<Serializable()>
Public Class ZCore


    Private Shared FlagVolumesLoaded As Boolean = False
    Private Shared DsCore As New DataSet
    Private Shared _HtHierarchyRelation As New Hashtable
    Public Shared FlagLoaded As Boolean
    Public Shared Sections As New Hashtable
    Public Shared DocTypes As New Generic.Dictionary(Of Int64, DocType)
    Public Shared Indexs As New Hashtable
    Public Shared Volumes As New Hashtable

    Public Shared Sub LoadCore()
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
    End Sub

    Public Shared Sub Cleardata()
        DsCore = New DataSet
        Sections = New Hashtable
        DocTypes = New Dictionary(Of Int64, DocType)
        Indexs = New Hashtable
        Volumes = New Hashtable
        Cache.DocTypesAndIndexs.arIndexs.Clear()
        FlagLoaded = False
        FlagIndexLoaded = False
        FlagVolumesLoaded = False

    End Sub

#Region "Volumes"
    Private Shared Function FillVolumes() As Boolean
        If FlagVolumesLoaded = False Then
            DsCore.Merge(VolumesFactory.GetAllVolumes())
            FlagVolumesLoaded = True
        End If
    End Function
    Shared lastusedVolume As IVolume
    Public Shared Function filterVolumes(ByVal VolumeId As Int32) As Volume
        LoadCore()
        If Not IsNothing(lastusedVolume) AndAlso lastusedVolume.ID = VolumeId Then
            Return lastusedVolume
        Else
            If Volumes.ContainsKey(VolumeId) Then
                Return Volumes(VolumeId)
            Else
                Dim DvVolumes As New DataView(DsCore.Tables("Disk_Volume"))
                DvVolumes.RowFilter = "Disk_Vol_Id = " & VolumeId
                If DvVolumes.Count > 0 Then
                    Dim Vol As New Volume
                    Vol.Name = DvVolumes(0).Item("DISK_VOL_NAME")
                    Vol.Size = DvVolumes(0).Item("Disk_Vol_Size")
                    Vol.Type = DvVolumes(0).Item("Disk_Vol_Type")
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
                Else
                    ZTrace.WriteLineIf(ZTrace.IsError, String.Format("El Volumen con ID {0} no fue encontrado", VolumeId))
                    Return Nothing
                End If
            End If
        End If
    End Function
#End Region

#Region "Atributos"
    Shared FlagIndexLoaded As Boolean
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Funcion para obtener los Atributos que comparten un conjunto de Doc_types
    ''' </summary>
    ''' <param name="SelectedDocTypes">Arraylist de Objetos Doc_Types</param>
    ''' <returns>Coleccion de objetos Atributos</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	30/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function FilterIndex(ByVal SelectedDocTypes As ArrayList) As Zamba.Core.Index()

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


            DvIndexR.RowFilter = Filter.ToString
            DvIndexR.Sort = "INDEX_ID"
            DvIndex.Table = DsCore.Tables("DOC_INDEX")
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

            'Obtiene todos los atributos desde los DataView.
            'Agrega cada indice a una lista ordenada, utilizando como clave el Index_id de la tabla Doc_Index
            For i = 0 To IndexCount
                Dim mustcomplete As Boolean = IndexData(CInt(DvIndex(i).Item("Index_Id")))
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
            'recorre todos los registros del dataView que contiene la relacion entre atributos y documentos 
            'esta informacion se encuentra en la tabla Index_R_Doc_Type.
            'Si el atributo se encuentra en la lista ordena cAux lo agrega a un array de atributos temporal tempcindex.
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
    ''' Funcion para obtener los Atributos que comparten un conjunto de Doc_types
    ''' </summary>
    ''' <param name="SelectedDocTypes">Arraylist de Objetos Doc_Types</param>
    ''' <returns>Coleccion de objetos Atributos</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	30/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function FilterSearchIndex(ByVal SelectedDocTypes As ArrayList) As Zamba.Core.Index()

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
            DvIndexR.Sort = "ORDEN ASC"
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

            DvIndexR.Sort = "INDEX_ID"
            DvIndexR.RowFilter = Filter.ToString & " And INDEXSEARCH <> '1'"

            DvIndex.Table = DsCore.Tables("DOC_INDEX")
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

            'Obtiene todos los atributos desde los DataView.
            'Agrega cada indice a una lista ordenada, utilizando como clave el Index_id de la tabla Doc_Index
            For i = 0 To IndexCount
                Dim mustcomplete As Boolean = IndexData(CInt(DvIndex(i).Item("Index_Id")))

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
            'recorre todos los registros del dataView que contiene la relacion entre atributos y documentos 
            'esta informacion se encuentra en la tabla Index_R_Doc_Type.
            'Si el atributo se encuentra en la lista ordena cAux lo agrega a un array de atributos temporal tempcindex.
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
            ZCore.FilterIndex(SelectedDocTypes)
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
    ''' Funcion para obtener los Atributos que comparten un conjunto de Doc_types
    ''' </summary>
    ''' <param name="SelectedDocTypes">Coleccion de Objetos Doc_Types</param>
    ''' <returns>Coleccion de objetos Atributos</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	30/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function FilterIndex(ByVal SelectedDocTypes() As DocType) As Zamba.Core.Index()

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
                    Filter.Append(" Or DOC_TYPE_ID = ")
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
                                Where.Append(" Or INDEX_ID =")
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
                            Where.Append(" Or INDEX_ID =")
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

            'Obtiene todos los atributos desde los DataView.
            'Agrega cada indice a una lista ordenada, utilizando como clave el Index_id de la tabla Doc_Index
            For i = 0 To IndexCount
                Dim mustcomplete As Boolean = IndexData(CInt(DvIndex(i).Item("Index_Id")))
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
            'recorre todos los registros del dataView que contiene la relacion entre atributos y documentos 
            'esta informacion se encuentra en la tabla Index_R_Doc_Type.
            'Si el atributo se encuentra en la lista ordena cAux lo agrega a un array de atributos temporal tempcindex.
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
    ''' Devuelve un Arraylist con los Objetos Atributos asignados al Doc_Type
    ''' </summary>
    ''' <param name="DocTypeId">ID de la entidad que se desea conocer los atributos asignados</param>
    ''' <returns>Arraylist de objetos Atributos</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	30/05/2006	Created
    '''     [Tomas]     09/10/2009  Modified    Se comenta el hash IndexData ya que se cargaban  
    '''                                         los datos pero nunca era utilizado.
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function FilterIndex(ByVal DocTypeId As Int64, Optional ByVal loadDefaultValues As Boolean = False) As List(Of IIndex)

        If Cache.DocTypesAndIndexs.arIndexs.ContainsKey(DocTypeId) AndAlso Cache.DocTypesAndIndexs.arIndexs(DocTypeId).Count > 0 Then
            Return Cache.DocTypesAndIndexs.arIndexs(DocTypeId)
        Else
            Dim DvIndex As New DataView
            Dim DvIndexR As New DataView
            Dim i As Int32
            Dim x As Int32
            Dim Where As New System.Text.StringBuilder
            'Dim IndexData As New Hashtable
            Dim cAux As New SortedList

            Try
                LoadCore()
                DvIndexR.Table = DsCore.Tables("INDEX_R_DOC_TYPE")
                DvIndexR.RowFilter = "DOC_TYPE_ID = " & DocTypeId
                DvIndexR.Sort = "ORDEN ASC"
                DvIndex.Table = DsCore.Tables("DOC_INDEX")
                For x = 0 To DvIndexR.Count - 1
                    If x = 0 Then
                        Where.Append("INDEX_ID = ")
                        Where.Append(DvIndexR(x).Item("INDEX_ID"))
                        'If IndexData.ContainsKey(DvIndexR(x).Item("INDEX_ID")) = False Then
                        '    IndexData.Add(DvIndexR(x).Item("INDEX_ID"), CBool(DvIndexR(x).Item("COMPLETE")))
                        'End If
                    Else
                        Where.Append(" Or INDEX_ID =")
                        Where.Append(DvIndexR(x).Item("INDEX_ID"))
                        'If IndexData.ContainsKey(DvIndexR(x).Item("INDEX_ID")) = False Then
                        '    IndexData.Add(DvIndexR(x).Item("INDEX_ID"), CBool(DvIndexR(x).Item("COMPLETE")))
                        'End If
                    End If
                Next

                Dim Indexs As New List(Of IIndex)
                If IsNothing(Where) = False Then
                    DvIndex.RowFilter = Where.ToString
                    DvIndexR.Sort = "ORDEN"
                    Dim IndexCount As Int32 = DvIndex.Count - 1

                    'Se carga los valores por defecto de las entidades
                    Dim DT As DocType = Nothing
                    If IndexCount >= 0 AndAlso loadDefaultValues Then
                        DT = DocTypesBusiness.GetDocType(DocTypeId, True)

                        If IsNothing(DT.IndexsDefaultValues) OrElse DT.IndexsDefaultValues.Count < 1 Then
                            'Se obtienen los valores por defecto
                            Dim defaultValues As Dictionary(Of Int64, String) = IndexsBusiness.GetIndexDefaultValues(DT)

                            'Se guardan en la instancia y en cache
                            DT.IndexsDefaultValues = defaultValues
                            DirectCast(Cache.DocTypesAndIndexs.hsDocTypes(DocTypeId), DocType).IndexsDefaultValues = defaultValues
                        End If
                    End If

                    'Obtiene todos los atributos desde los DataView.
                    Dim filter As String = DvIndexR.RowFilter
                    'Agrega cada indice a una lista ordenada, utilizando como clave el Index_id de la tabla Doc_Index
                    For i = 0 To IndexCount
                        DvIndexR.RowFilter = "Index_id=" & DvIndex(i).Item("Index_Id").ToString() & " And Doc_type_id=" & DocTypeId

                        If DvIndexR.Count > 0 Then
                            Dim mustcomplete As Boolean = DvIndexR(0).Item("MustComplete")
                            Dim isreference As Boolean = DvIndexR(0).Item("ISREFERENCED")
                            Dim isAutoincremental As Boolean = False
                            If Not IsDBNull(DvIndexR(0).Item("Autocomplete")) Then
                                isAutoincremental = DvIndexR(0).Item("Autocomplete")
                            End If

                            Dim ind As Index
                            If loadDefaultValues Then
                                'CREA el atributo CON VALOR POR DEFECTO, UTILIZADO PARA INSERTAR NUEVOS DOCUMENTOS
                                Dim defaultValue As String = String.Empty
                                If (DT.IndexsDefaultValues.Count > 0 AndAlso DT.IndexsDefaultValues.ContainsKey(DvIndex(i).Item("Index_Id"))) Then
                                    defaultValue = DT.IndexsDefaultValues.Item(DvIndex(i).Item("Index_Id"))
                                End If

                                If DvIndex(i).Item("Index_Type") = 9 And defaultValue.Contains("(") And Server.isSQLServer = True Then
                                    defaultValue = 0
                                End If
                                If Not IsDBNull(DvIndex(i).Item("DataTableName")) Then
                                    ind = New Index(DvIndex(i).Item("Index_Id"), DvIndex(i).Item("Index_Name"), DvIndex(i).Item("Index_Type"), DvIndex(i).Item("Index_Len"),
                                                    False, False, DvIndex(i).Item("DropDown"), False, False, mustcomplete, defaultValue, DvIndex(i).Item("IndicePadre"), Nothing, DvIndex(i).Item("DataTableName"))
                                Else
                                    ind = New Index(DvIndex(i).Item("Index_Id"), DvIndex(i).Item("Index_Name"), DvIndex(i).Item("Index_Type"), DvIndex(i).Item("Index_Len"),
                                                    False, False, DvIndex(i).Item("DropDown"), False, False, mustcomplete, defaultValue, DvIndex(i).Item("IndicePadre"), Nothing, String.Empty)
                                End If
                            Else
                                'CREA el atributo SIN DEFAULT VALUE
                                If Not IsDBNull(DvIndex(i).Item("DataTableName")) Then
                                    ind = New Index(DvIndex(i).Item("Index_Id"), DvIndex(i).Item("Index_Name"), DvIndex(i).Item("Index_Type"), DvIndex(i).Item("Index_Len"),
                                                    False, False, DvIndex(i).Item("DropDown"), False, False, mustcomplete, String.Empty, DvIndex(i).Item("IndicePadre"), Nothing, DvIndex(i).Item("DataTableName"))
                                Else
                                    ind = New Index(DvIndex(i).Item("Index_Id"), DvIndex(i).Item("Index_Name"), DvIndex(i).Item("Index_Type"), DvIndex(i).Item("Index_Len"),
                                                    False, False, DvIndex(i).Item("DropDown"), False, False, mustcomplete, String.Empty, -1, Nothing, String.Empty)
                                End If
                            End If
                            ind.isReference = isreference
                            ind.AutoIncremental = isAutoincremental

                            'Si existen los hijos del atributo, los agrego en la propiedad
                            If Not _HtHierarchyRelation(ind.ID) Is Nothing Then
                                ind.HierarchicalChildID = _HtHierarchyRelation(ind.ID)
                            End If

                            'Si el atributo no está repetido se agrega a la lista
                            If Not cAux.ContainsKey(DvIndex(i).Item("Index_Id").ToString) Then
                                cAux.Add(DvIndex(i).Item("Index_Id").ToString, ind)

                                SyncLock ZCore.Indexs
                                    If ZCore.Indexs.ContainsKey(ind.ID) = False Then
                                        ZCore.Indexs.Add(ind.ID, ind)
                                    End If
                                End SyncLock
                            End If
                        End If
                    Next

                    DvIndexR.RowFilter = filter

                    'recorre todos los registros del dataView que contiene la relacion entre atributos y documentos 
                    'esta informacion se encuentra en la tabla Index_R_Doc_Type.
                    'Si el atributo se encuentra en la lista ordena cAux lo agrega a un array de atributos temporal Indexs.
                    'El dataView dvIndexR se encuentra oredena por la columna orden de la tabla Index_R_Doc_Type.
                    For i = 0 To DvIndexR.Count - 1
                        If cAux.ContainsKey(DvIndexR(i).Item("Index_Id").ToString) Then
                            Indexs.Add(cAux.Item(DvIndexR(i).Item("Index_Id").ToString))
                        End If
                    Next
                End If

                If Cache.DocTypesAndIndexs.arIndexs.ContainsKey(DocTypeId) Then
                    Cache.DocTypesAndIndexs.arIndexs(DocTypeId) = Indexs
                Else
                    Cache.DocTypesAndIndexs.arIndexs.Add(DocTypeId, Indexs)
                End If
                Return Indexs
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            Finally
                DvIndex.Dispose()
                DvIndex = Nothing
                DvIndexR.Dispose()
                DvIndexR = Nothing
                Where = Nothing
                'IndexData = Nothing
                cAux.Clear()
                cAux = Nothing
            End Try
            Return Nothing

        End If
    End Function
    Public Shared Function FilterCIndex(ByVal docTypeId As Int64) As Index()
        LoadCore()
        Dim DvIndex As New DataView
        Dim DvIndexR As New DataView
        Dim i As Int32
        Dim x As Int32
        Dim Where As New System.Text.StringBuilder
        Dim IndexData As New Hashtable
        Try
            DvIndexR.Table = DsCore.Tables("INDEX_R_DOC_TYPE")
            DvIndexR.RowFilter = "DOC_TYPE_ID = " & docTypeId
            DvIndex.Table = DsCore.Tables("DOC_INDEX")
            For x = 0 To DvIndexR.Count - 1
                Try
                    If x = 0 Then
                        Where.Append("INDEX_ID = ")
                        Where.Append(DvIndexR(x).Item("INDEX_ID"))
                        If IndexData.ContainsKey(DvIndexR(x).Item("INDEX_ID")) = False Then IndexData.Add(DvIndexR(x).Item("INDEX_ID"), CBool(DvIndexR(x).Item("COMPLETE")))
                    Else
                        Where.Append(" Or INDEX_ID =")
                        Where.Append(DvIndexR(x).Item("INDEX_ID"))
                        If IndexData.ContainsKey(DvIndexR(x).Item("INDEX_ID")) = False Then IndexData.Add(DvIndexR(x).Item("INDEX_ID"), CBool(DvIndexR(x).Item("COMPLETE")))
                    End If
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                End Try
            Next
            DvIndex.RowFilter = Where.ToString
            '  Dim b As Int32

            Dim IndexCount As Int32 = DvIndex.Count - 1
            Dim CIndexs(IndexCount) As Index
            For i = 0 To IndexCount
                Dim mustcomplete As Boolean = IndexData(DvIndex(i).Item("Index_Id"))
                CIndexs.SetValue(New Index(DvIndex(i).Item("Index_Id"), DvIndex(i).Item("Index_Name"), DvIndex(i).Item("Index_Type"), DvIndex(i).Item("Index_Len"), False, False, DvIndex(i).Item("DropDown"), False, False, mustcomplete), i)
            Next
            Return CIndexs
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
    ''' Funcion que devuelve un Objeto Atributo en base a su ID
    ''' </summary>
    ''' <param name="IndexId">Id del indice que se desea obtener</param>
    ''' <returns>Objeto Index</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	30/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetIndex(ByVal IndexId As Int64) As Index
        Try
            SyncLock ZCore.Indexs

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
                            Dim index As New Index(DvIndex(0).Item("Index_Id"), Trim(DvIndex(0).Item("Index_Name")), DvIndex(0).Item("Index_Type"), DvIndex(0).Item("Index_Len"), False, False, DvIndex(0).Item("DropDown"), False, False, 0)
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
            End SyncLock
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function
    Private Shared Function FillIndex() As Boolean
        If FlagIndexLoaded = False Then
            DsCore.Merge(Zamba.Core.IndexsBusiness.GetIndex().Tables("DOC_INDEX"))

            'Obtenemos las reglasciones y las almacenamos en chache

            _HtHierarchyRelation = IndexsBusiness.GetHierarchicalRelations()
            If _HtHierarchyRelation Is Nothing Then
                _HtHierarchyRelation = New Hashtable()
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
    Public Shared Function FilterDocTypes(ByVal SelectedIndexs() As Zamba.Core.Index) As Zamba.Core.DocType()
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
                            Where.Append(" Or DOC_TYPE_ID =")
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
                tempcdoctype.SetValue(New DocType(DvDocType(b).Item("Doc_Type_Id"), DvDocType(b).Item("Doc_Type_Name"), DvDocType(b).Item("File_Format_id"), DvDocType(b).Item("Disk_Group_Id"), DvDocType(b).Item("Thumbnails"), DvDocType(b).Item("Icon_Id"), DvDocType(b).Item("Object_Type_Id"), DvDocType(b).Item("AutoName"), DvDocType(b).Item("AutoName"), DvDocType(b).Item("DocCount"), 0, DvDocType(b).Item("DocumentalId")), b)
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
    Public Shared Function FilterDocTypes(ByVal docTypeId As Int64) As Zamba.Core.DocType
        If DocTypes.ContainsKey(docTypeId) Then
            Return DocTypes(docTypeId).Clone()
        Else
            LoadCore()
            Dim DvDocType As New DataView
            ' Dim First As Boolean = True
            Dim Where As String
            '  Dim DocTypesIds As New ArrayList
            DvDocType.Table = DsCore.Tables("DOC_TYPE")
            Where = "DOC_TYPE_ID = " & docTypeId
            DvDocType.RowFilter = Where
            If DvDocType.Count > 0 Then
                Dim b As Int32
                Dim Doctype As New DocType(DvDocType(b).Item("DOC_TYPE_ID"), DvDocType(b).Item("DOC_TYPE_NAME"), DvDocType(b).Item("FILE_FORMAT_ID"), DvDocType(b).Item("DISK_GROUP_ID"), DvDocType(b).Item("THUMBNAILS"), DvDocType(b).Item("ICON_ID"), DvDocType(b).Item("OBJECT_TYPE_ID"), DvDocType(b).Item("AUTONAME"), DvDocType(b).Item("AUTONAME"), DvDocType(b).Item("DOCCOUNT"), 0, DvDocType(b).Item("DOCUMENTALID"))
                DocTypes.Add(docTypeId, Doctype)
                'DocTypesBusiness.GetEditRights(Doctype) ESTO EN WEB ESTA DESCOMENTADO.
                Return Doctype.Clone()
            End If
        End If
        Return Nothing
    End Function
    Public Shared Function GetDocTypeAutoName(ByVal docTypeId As Int64) As String
        If DocTypes.ContainsKey(docTypeId) Then
            Return DocTypes(docTypeId).AutoNameText
        Else
            LoadCore()
            Dim DvDocType As New DataView
            DvDocType.Table = DsCore.Tables("DOC_TYPE")
            DvDocType.RowFilter = "DOC_TYPE_ID = " & docTypeId
            If DvDocType.Count > 0 Then
                Return DvDocType(0).Item("AUTONAME")
            Else
                Return String.Empty
            End If
        End If
    End Function
    Public Shared Sub FilterCurrentDocTypes(ByVal SelectedDocTypes As ArrayList)
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
                    If DsCore.Tables("DOC_TYPE").Rows(i)("DOC_TYPE_ID") = DvDocType(z).Item("DOC_TYPE_ID") Then
                        Permitted = True
                        Exit For
                    End If
                Next
                If Permitted = False Then DsCore.Tables("DOC_TYPE").Rows(i).Delete()
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
    Private Shared Sub FillDSDocTypes()
        DsCore.Merge(DocTypesFactory.GetDocTypesDsDocType())
        ZCore.FilterCurrentDocTypes(UserBusiness.Rights.GetAditional(ObjectTypes.DocTypes, RightsType.View))
    End Sub
    Private Shared Sub FillDocTypesChilds()
        'if there are initial arguments currentUser maybe Nothing
        If Membership.MembershipHelper.CurrentUser IsNot Nothing Then
            Dim dstemp As DataSet = DocTypesFactory.GetDocTypesChilds(Membership.MembershipHelper.CurrentUser)
            dstemp.Tables(0).TableName = "INDEX_R_DOC_TYPE"
            DsCore.Merge(dstemp)
        End If
    End Sub
    Private Shared Sub FillDocTypes()
        Dim ArrDocTypes As List(Of IDocType) = DocTypesFactory.GetDocTypesbyUserRightsOfView(Membership.MembershipHelper.CurrentUser.ID, RightsType.View)
        For Each Doctype As DocType In ArrDocTypes
            DocTypes.Add(Doctype.ID, Doctype)
        Next
    End Sub

#End Region

#Region "System"
    Private Shared errorRB As ErrorReportBusiness



    Public Shared Sub StartTrace(ByVal moduleName As String)
        Try

            Dim level As Int32
            Try
                level = Int32.Parse(UserPreferences.getValueForMachine("TraceLevel", Zamba.UPSections.UserPreferences, "4"))
            Catch
                level = 4
            End Try
            ZTrace.SetLevel(level, moduleName)
            If level > 0 Then ZTrace.WriteLineIf(ZTrace.IsError, "Nivel de trace: " & level)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub


#End Region

End Class