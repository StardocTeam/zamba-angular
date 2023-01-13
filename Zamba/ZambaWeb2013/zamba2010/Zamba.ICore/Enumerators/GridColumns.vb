Public Class GridColumns

#Region "Constantes"

    Public Const DISKGROUPIDCOLUMNNAME = "Disk_Group_Id"
    Public Const DOC_ID_COLUMNNAME = "Doc_Id"
    Public Const DOCID_COLUMNNAME = "DocId"
    Public Const DOC_FILE_COLUMNNAME = "Doc_File"
    Public Const DOC_TYPE_ID_COLUMNNAME = "Doc_Type_Id"
    Public Const DISK_VOL_ID_COLUMNNAME = "Disk_Vol_Id"
    Public Const DISK_VOL_PATH_COLUMNNAME = "Disk_Vol_Path"
    Public Const DO_STATE_ID_COLUMNNAME = "Do_State_Id"
    Public Const PLATTER_ID_COLUMNNAME = "Platter_Id"
    Public Const VOL_ID_COLUMNNAME = "Vol_Id"
    Public Const OFFSET_COLUMNNAME = "Offset"
    Public Const VER_PARENT_ID_COLUMNNAME = "Ver_Parent_Id"
    Public Const STEP_ID_COLUMNNAME = "Step_Id"
    Public Const TASK_ID_COLUMNNAME = "Task_Id"
    Public Const TASK_STATE_ID_COLUMNNAME = "Task_State_Id"
    Public Const WORK_ID_COLUMNNAME = "Work_Id"
    Public Const C_EXCLUSIVE = "C_Exclusive"
    Public Const RNUM = "Rnum"

    Public Const DOC_TYPE_NAME_COLUMNNAME = "Entidad"
    Public Const IMAGEN_COLUMNNAME = "Imagen"
    Public Const SHARED_COLUMNNAME = "Shared"
    Public Const VERSION_COLUMNNAME = "Version"
    Public Const ROOTID_COLUMNNAME = "RootId"
    Public Const ORIGINAL_FILENAME_COLUMNNAME = "Original"
    Public Const NUMERO_DE_VERSION_COLUMNNAME = "Numero de Version"
    Public Const CRDATE_COLUMNNAME = "Creado"
    Public Const LASTUPDATE_COLUMNNAME = "Modificado"
    Public Const NAME1_COLUMNNAME = "NAME1"
    Public Const ICONID_COLUMNNAME = "Icono"
    Public Const CHECKIN_COLUMNNAME = "Fecha de ingreso"
    Public Const WFSTEPID_COLUMNNAME = "WfStepId"
    Public Const ASIGNADO_COLUMNNAME = "Asignado"
    Public Const STATE_COLUMNNAME = "Estado"
    Public Const ESTADO_TAREA_COLUMNNAME = "Estado Tarea"
    Public Const SITUACION_COLUMNNAME = "Situacion"
    Public Const EXPIREDATE_COLUMNNAME = "Vencimiento Tarea"
    Public Const USER_ASIGNED_COLUMNNAME = "User_Asigned"
    Public Const USER_ASIGNEDNAME_COLUMNNAME = "Asignado"
    Public Const USER_ASIGNED_BY_ID_COLUMNNAME = "User_Asigned_By"
    Public Const USER_ASIGNED_BY_COLUMNNAME = "Ususario asignado por"
    Public Const DATE_ASIGNED_BY_COLUMNNAME = "Fecha asignada por"
    Public Const REMARK_COLUMNNAME = "Remark"
    Public Const TAG_COLUMNNAME = "Tag"
    Public Const DOCTYPEID_COLUMNNAME = "DoctypeId"
    Public Const TASKCOLOR_COLUMNNAME = "TaskColor"
    Public Const VER_COLUMNNAME = "Ver"
    Public Const READDATE_COLUMNNAME = "Readdate"
    Public Const NOMBRE_DOCUMENTO_COLUMNNAME = "Tarea"
    Public Const WORKFLOW_COLUMNAME = "Proceso"
    Public Const ETAPA_COLUMNAME = "Etapa"
    Public Const FULLPATH_COLUMNAME = "fullpath"
    Public Const NAME_DOCUMENTO_COLUMNNAME = "Tarea"
    Public Const ICON_ID_COLUMNNAME = "ICON_ID"
    Public Const EXECUTION_COLUMNNAME = "EXECUTION"
    Public Const RN_COLUMNNAME = "RN"
    Public Const NUMEROVERSION_COLUMNNAME = "NUMEROVERSION"
    Public Const THUMB_COLUMNNAME = "THUMB"






#End Region

    ''' <summary>
    ''' Diccionario que guarda el nombre visible como key y el nombre en la BD como valor
    ''' </summary>
    Public Shared ZambaColumns As New Dictionary(Of String, String)(StringComparer.InvariantCultureIgnoreCase)
    ''' <summary>
    ''' Diccionario que guarda las columnas de Zamba como key y si son visibles o no como valor
    ''' </summary>
    Public Shared ColumnsVisibility As New Dictionary(Of String, Boolean)(StringComparer.InvariantCultureIgnoreCase)

    Public Shared ColumnsOwnForZamba As New Dictionary(Of String, Int32)(StringComparer.InvariantCultureIgnoreCase)
    ''' <summary>
    ''' Diccionario que almacena las columnas visibles con indices, para poder cargar el combo de filtros en grilla de tareas web
    ''' </summary>
    Public Shared VisibleColumns As New Dictionary(Of String, Int32)(StringComparer.InvariantCultureIgnoreCase)
    ''' <summary>
    ''' Diccionario que almacena las columnas visibles y el tipo de dato, para poder realizar el filterString
    ''' </summary>
    Public Shared ZambaColumnsType As New Dictionary(Of String, IndexDataType)(StringComparer.InvariantCultureIgnoreCase)

    Shared Sub New()

#Region "Diccionario alias, nombre"
        '        ZambaColumns.Add(NOMBRE_DOCUMENTO_COLUMNNAME, "wd.NAME")
        ZambaColumns.Add(NAME_DOCUMENTO_COLUMNNAME, "NAME")
        ZambaColumns.Add(DOC_TYPE_NAME_COLUMNNAME, "DOC_TYPE_NAME")
        ZambaColumns.Add(ORIGINAL_FILENAME_COLUMNNAME, "ORIGINAL")
        ZambaColumns.Add(NUMERO_DE_VERSION_COLUMNNAME, "NUMEROVERSION")
        ZambaColumns.Add(ICONID_COLUMNNAME, "T.Icon_Id")
        ZambaColumns.Add(CHECKIN_COLUMNNAME, "wd.CHECKIN")
        ZambaColumns.Add(EXPIREDATE_COLUMNNAME, "wd.EXPIREDATE")
        ZambaColumns.Add(USER_ASIGNED_BY_COLUMNNAME, "wd.USER_ASIGNED_BY")
        '       ZambaColumns.Add(WORKFLOW_NAME_COLUMNAME, "WFWORKFLOW.NAME")
        ZambaColumns.Add(WORKFLOW_COLUMNAME, "WORKFLOW")
        '      ZambaColumns.Add(ASIGNADO_COLUMNNAME, "u.NAME")
        ZambaColumns.Add(ASIGNADO_COLUMNNAME, "ASSIGNEDTO")
        '        ZambaColumns.Add(STATE_COLUMNNAME, "wss.NAME")
        ZambaColumns.Add(STATE_COLUMNNAME, "STATE")
        ZambaColumns.Add(ETAPA_COLUMNAME, "STEP")
        ZambaColumns.Add(CRDATE_COLUMNNAME, "I.crdate")
        ZambaColumns.Add(LASTUPDATE_COLUMNNAME, "I.lupdate")
        ZambaColumns.Add(ESTADO_TAREA_COLUMNNAME, "wss.NAME")
        ZambaColumns.Add(SITUACION_COLUMNNAME, "WFTASK_STATES.TASK_STATE_NAME")

        'ZambaColumns.Add("ASSIGNEDTO", "User_Asigned")
        'ZambaColumns.Add("STEP", "Step_Id")
        'ZambaColumns.Add("INGRESO", "checkin")
        'ZambaColumns.Add("NAME", "name")
        'ZambaColumns.Add("Leido", "name")

#End Region


        ColumnsVisibility.Add(GridColumns.C_EXCLUSIVE.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.RNUM.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.DOC_ID_COLUMNNAME.ToLower(), True)
        ColumnsVisibility.Add(GridColumns.DOC_TYPE_ID_COLUMNNAME.ToLower(), True)
        ColumnsVisibility.Add(GridColumns.VER_PARENT_ID_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.STEP_ID_COLUMNNAME.ToLower(), True)
        ColumnsVisibility.Add(GridColumns.DISKGROUPIDCOLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.PLATTER_ID_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.VOL_ID_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.OFFSET_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.SHARED_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.ROOTID_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.FULLPATH_COLUMNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.DISK_VOL_ID_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.DISK_VOL_PATH_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.DOC_FILE_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.READDATE_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.TASK_ID_COLUMNNAME.ToLower(), True)
        ColumnsVisibility.Add(GridColumns.DO_STATE_ID_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.ICONID_COLUMNNAME.ToLower(), True)
        ColumnsVisibility.Add(GridColumns.USER_ASIGNED_COLUMNNAME.ToLower(), True)
        ColumnsVisibility.Add(GridColumns.USER_ASIGNED_BY_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.DATE_ASIGNED_BY_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.TASK_STATE_ID_COLUMNNAME.ToLower(), True)
        ColumnsVisibility.Add(GridColumns.REMARK_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.TAG_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.WORK_ID_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.WFSTEPID_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.DOCID_COLUMNNAME.ToLower(), True)
        ColumnsVisibility.Add(GridColumns.DOCTYPEID_COLUMNNAME.ToLower(), True)
        ColumnsVisibility.Add(GridColumns.TASKCOLOR_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.VERSION_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.ETAPA_COLUMNAME.ToLower(), True)
        ColumnsVisibility.Add(GridColumns.NUMERO_DE_VERSION_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.ORIGINAL_FILENAME_COLUMNNAME.ToLower(), True)
        ColumnsVisibility.Add(GridColumns.ICON_ID_COLUMNNAME.ToLower(), True)
        ColumnsVisibility.Add(GridColumns.EXECUTION_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.RN_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.NUMEROVERSION_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.THUMB_COLUMNNAME.ToLower(), False)

        ColumnsVisibility.Add("exclusive", False)
        ColumnsVisibility.Add("step", True)

        ' Columnas siempre visibles
        ColumnsVisibility.Add(GridColumns.EXPIREDATE_COLUMNNAME.ToLower(), True)
        ColumnsVisibility.Add(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME.ToLower(), True)
        ColumnsVisibility.Add(GridColumns.IMAGEN_COLUMNNAME.ToLower(), True)
        ColumnsVisibility.Add(GridColumns.ESTADO_TAREA_COLUMNNAME.ToLower(), True)
        ColumnsVisibility.Add(GridColumns.ASIGNADO_COLUMNNAME.ToLower(), True)
        ColumnsVisibility.Add(GridColumns.SITUACION_COLUMNNAME.ToLower(), True)
        ColumnsVisibility.Add(GridColumns.VER_COLUMNNAME.ToLower(), True)
        ColumnsVisibility.Add(GridColumns.CRDATE_COLUMNNAME.ToLower(), True)
        ColumnsVisibility.Add(GridColumns.CHECKIN_COLUMNNAME.ToLower(), True)

        VisibleColumns.Add(GridColumns.EXPIREDATE_COLUMNNAME.ToLower(), -1)
        VisibleColumns.Add(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME.ToLower(), -2)
        VisibleColumns.Add(GridColumns.IMAGEN_COLUMNNAME.ToLower(), -3)
        VisibleColumns.Add(GridColumns.ESTADO_TAREA_COLUMNNAME.ToLower(), -4)
        VisibleColumns.Add(GridColumns.ASIGNADO_COLUMNNAME.ToLower(), -5)
        VisibleColumns.Add(GridColumns.SITUACION_COLUMNNAME.ToLower(), -6)
        VisibleColumns.Add(GridColumns.VER_COLUMNNAME.ToLower(), -7)
        VisibleColumns.Add(GridColumns.CRDATE_COLUMNNAME.ToLower(), -8)
        VisibleColumns.Add(GridColumns.CHECKIN_COLUMNNAME.ToLower(), -9)

        ZambaColumnsType.Add(GridColumns.EXPIREDATE_COLUMNNAME.ToLower(), IndexDataType.Fecha)
        ZambaColumnsType.Add(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME.ToLower(), IndexDataType.Alfanumerico_Largo)
        ZambaColumnsType.Add(GridColumns.IMAGEN_COLUMNNAME.ToLower(), IndexDataType.Alfanumerico)
        ZambaColumnsType.Add(GridColumns.ESTADO_TAREA_COLUMNNAME.ToLower(), IndexDataType.Alfanumerico)
        ZambaColumnsType.Add(GridColumns.ASIGNADO_COLUMNNAME.ToLower(), IndexDataType.Alfanumerico)
        ZambaColumnsType.Add(GridColumns.SITUACION_COLUMNNAME.ToLower(), IndexDataType.Alfanumerico)
        ZambaColumnsType.Add(GridColumns.VER_COLUMNNAME.ToLower(), IndexDataType.Alfanumerico)
        ZambaColumnsType.Add(GridColumns.CRDATE_COLUMNNAME.ToLower(), IndexDataType.Fecha)
        ZambaColumnsType.Add(GridColumns.CHECKIN_COLUMNNAME.ToLower(), IndexDataType.Fecha)

        ColumnsOwnForZamba.Add(GridColumns.USER_ASIGNED_COLUMNNAME.ToLower(), -10)
        ColumnsOwnForZamba.Add(GridColumns.ETAPA_COLUMNAME.ToLower(), -11)
        ColumnsOwnForZamba.Add(GridColumns.ORIGINAL_FILENAME_COLUMNNAME.ToLower(), -12)
        ColumnsOwnForZamba.Add(GridColumns.EXECUTION_COLUMNNAME.ToLower(), -13)
        ColumnsOwnForZamba.Add("step", True)
        ColumnsOwnForZamba.Add(GridColumns.EXPIREDATE_COLUMNNAME.ToLower(), -1)
        ColumnsOwnForZamba.Add(GridColumns.ESTADO_TAREA_COLUMNNAME.ToLower(), -4)
        ColumnsOwnForZamba.Add(GridColumns.ASIGNADO_COLUMNNAME.ToLower(), -5)
        ColumnsOwnForZamba.Add(GridColumns.SITUACION_COLUMNNAME.ToLower(), -6)
        ColumnsOwnForZamba.Add(GridColumns.CRDATE_COLUMNNAME.ToLower(), -8)
        ColumnsOwnForZamba.Add(GridColumns.CHECKIN_COLUMNNAME.ToLower(), -9)
        'ColumnsOwnForZamba.Add(GridColumns.EXPIREDATE_COLUMNNAME.ToLower(), -1)



    End Sub

    ''' <summary>
    ''' A partir de el nombre de la columna en la grilla, obtengo el nombre en la tabla de la DB
    ''' Si no esta esa columna devuelve el mismo nombre
    ''' </summary>
    ''' <param name="gridColumnName"></param>
    ''' <returns></returns>
    'Public Shared Function GetTableColumnName(gridColumnName As String) As String

    '    If ZambaColumns.ContainsKey(gridColumnName) Then
    '        gridColumnName = ZambaColumns(gridColumnName)
    '    End If

    '    Return gridColumnName
    'End Function


    ''' <summary>
    ''' A partir de el nombre de la columna en la grilla (el alias), obtengo el nombre de la columna 
    ''' en la DB. 
    ''' Si la columna no se encuentra es porque no se trata de una columna con alias, se devuelve el mismo nombre.
    ''' </summary>
    ''' <param name="ColumnAlias">Nombre de la columna como aparece en la grilla</param>
    ''' <returns></returns>
    Public Shared Function GetColumnNameByAliasName(ColumnAlias As String) As String

        If ZambaColumns.ContainsKey(ColumnAlias) Then
            ColumnAlias = ZambaColumns(ColumnAlias)
        End If

        Return ColumnAlias
    End Function

    ''' <summary>
    ''' A traves del nombre de una columna en la BD obtengo el alias de la misma (el nombre en la grilla)
    ''' </summary>
    ''' <param name="ColumnName">Nombre de la columna en la BD</param>
    ''' <returns></returns>
    Public Shared Function GetAliasNameByColumnName(ColumnName As String) As String

        If ZambaColumns.ContainsValue(ColumnName) Then
            For Each pair As KeyValuePair(Of String, String) In ZambaColumns
                If pair.Value.ToLower().Equals(ColumnName.ToLower()) Then
                    ColumnName = pair.Key
                End If
            Next
        End If

        Return ColumnName
    End Function

End Class
