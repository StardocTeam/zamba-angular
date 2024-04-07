﻿Imports System.Collections.Generic
Imports System.Text

Public Class GridColumns
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
    Public Const ICON_ID_COLUMNNAME = "Icon_Id"
    Public Const VER_PARENT_ID_COLUMNNAME = "Ver_Parent_Id"
    Public Const STEP_ID_COLUMNNAME = "Step_Id"
    Public Const TASK_ID_COLUMNNAME = "Task_Id"
    Public Const TASK_STATE_ID_COLUMNNAME = "Task_State_Id"
    Public Const WORK_ID_COLUMNNAME = "Work_Id"
    Public Const C_EXCLUSIVE = "C_Exclusive"
    Public Const RNUM = "Rnum"

    Public Const DOC_TYPE_NAME_COLUMNNAME = "Entidad"
    Public Const IMAGEN_COLUMNNAME = "I"
    Public Const SHARED_COLUMNNAME = "Shared"
    Public Const VERSION_COLUMNNAME = "Version"
    Public Const ROOTID_COLUMNNAME = "RootId"
    Public Const ORIGINAL_FILENAME_COLUMNNAME = "Nombre Original"
    Public Const NUMERO_DE_VERSION_COLUMNNAME = "Numero de Version"
    Public Const CRDATE_COLUMNNAME = "Fecha Creacion"
    Public Const LASTUPDATE_COLUMNNAME = "Fecha Modificacion"
    Public Const NAME1_COLUMNNAME = "NAME1"
    Public Const ICONID_COLUMNNAME = "IconId"
    Public Const CHECKIN_COLUMNNAME = "Fecha de ingreso"
    Public Const WFSTEPID_COLUMNNAME = "WfStepId"
    Public Const ASIGNADO_COLUMNNAME = "Asignado"
    Public Const STATE_COLUMNNAME = "Estado"
    Public Const ESTADO_TAREA_COLUMNNAME = "Estado Tarea"
    Public Const SITUACION_COLUMNNAME = "Situacion"
    Public Const SITUACIONICON_COLUMNNAME = "Ejecucion"
    Public Const EXPIREDATE_COLUMNNAME = "Vencimiento Tarea"
    Public Const USER_ASIGNED_COLUMNNAME = "User_Asigned"
    Public Const USER_ASIGNEDNAME_COLUMNNAME = "Asignado"
    Public Const USER_ASIGNED_BY_ID_COLUMNNAME = "User_Asigned_By"
    Public Const USER_ASIGNED_BY_COLUMNNAME = "Usuario asignado por"
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

    Public Shared ZambaColumns As New Dictionary(Of String, String) 'Clave: alias, Valor: nombre de la columna en la tabla.
    Public Shared ColumnsVisibility As New Dictionary(Of String, Boolean) 'Columnas con su respectiva visibilidad. 
    Public Shared _ColumnsType As New Dictionary(Of String, String) 'Columnas con su respectivo tipo. 

    Shared Sub New()

        'Tipo de las columnas (columna, tipo)
        _ColumnsType.Add(GridColumns.USER_ASIGNED_BY_COLUMNNAME.ToLower(), "A")
        '_ColumnsType.Add(GridColumns.REMARK_COLUMNNAME.ToLower(), "A")
        _ColumnsType.Add(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME.ToLower(), "A")
        _ColumnsType.Add(GridColumns.STATE_COLUMNNAME.ToLower(), "A")
        _ColumnsType.Add(GridColumns.ASIGNADO_COLUMNNAME.ToLower(), "A")
        _ColumnsType.Add(GridColumns.WORKFLOW_COLUMNAME.ToLower(), "A")
        _ColumnsType.Add(GridColumns.SITUACION_COLUMNNAME.ToLower(), "A")
        _ColumnsType.Add(GridColumns.DATE_ASIGNED_BY_COLUMNNAME.ToLower(), "D")
        _ColumnsType.Add(GridColumns.EXPIREDATE_COLUMNNAME.ToLower(), "D")
        _ColumnsType.Add(GridColumns.CRDATE_COLUMNNAME.ToLower(), "D")
        _ColumnsType.Add(GridColumns.CHECKIN_COLUMNNAME.ToLower(), "D")


        'Columna en la DB a la que pertenece cada columna de la grilla
        ZambaColumns.Add(NOMBRE_DOCUMENTO_COLUMNNAME, "strTable.NAME")
        ZambaColumns.Add(DOC_TYPE_NAME_COLUMNNAME, "DOC_TYPE.DOC_TYPE_NAME")
        ZambaColumns.Add(ORIGINAL_FILENAME_COLUMNNAME, "strTable.ORIGINAL_FILENAME")
        ZambaColumns.Add(NUMERO_DE_VERSION_COLUMNNAME, "strTable.NUMEROVERSION")
        ZambaColumns.Add(CRDATE_COLUMNNAME, "strTable.CRDATE")
        ZambaColumns.Add(LASTUPDATE_COLUMNNAME, "strTable.LASTUPDATE")
        ZambaColumns.Add(ICONID_COLUMNNAME, "WFDOCUMENT.IconId")
        ZambaColumns.Add(CHECKIN_COLUMNNAME, "WFDOCUMENT.CHECKIN")
        ZambaColumns.Add(ETAPA_COLUMNAME, "S.NAME")
        ZambaColumns.Add(EXPIREDATE_COLUMNNAME, "WFDOCUMENT.EXPIREDATE")
        ZambaColumns.Add(USER_ASIGNED_BY_COLUMNNAME, "WFDOCUMENT.USER_AIGNED_BY")
        ZambaColumns.Add(WORKFLOW_COLUMNAME, "WFWORKFLOW.NAME")
        ZambaColumns.Add(ASIGNADO_COLUMNNAME, "UAG.NAME")
        ZambaColumns.Add(STATE_COLUMNNAME, "SS.NAME")
        ZambaColumns.Add(ESTADO_TAREA_COLUMNNAME, "SS.NAME")
        ZambaColumns.Add(SITUACION_COLUMNNAME, "WFTASK_STATES.TASK_STATE_NAME")

        'visibilidad de las columnas de zamba
        ColumnsVisibility.Add(GridColumns.C_EXCLUSIVE.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.RNUM.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.DOC_ID_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.DOC_TYPE_ID_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.VER_PARENT_ID_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.STEP_ID_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.DISKGROUPIDCOLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.PLATTER_ID_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.VOL_ID_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.OFFSET_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.ICON_ID_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.SHARED_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.ROOTID_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.DISK_VOL_ID_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.DISK_VOL_PATH_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.DOC_FILE_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.READDATE_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.TASK_ID_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.DO_STATE_ID_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.ICONID_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.USER_ASIGNED_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.USER_ASIGNED_BY_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.DATE_ASIGNED_BY_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.TASK_STATE_ID_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.REMARK_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.TAG_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.WORK_ID_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.STATE_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.WFSTEPID_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.DOCID_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.DOCTYPEID_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.TASKCOLOR_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add(GridColumns.VERSION_COLUMNNAME.ToLower(), False)
        ColumnsVisibility.Add("exclusive", False)

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

    End Sub


    ''' <summary>
    ''' A partir de el nombre de la columna en la grilla, obtengo el nombre en la tabla de la DB
    ''' Si no esta esa columna devuelve el mismo nombre
    ''' </summary>
    ''' <param name="gridColumnName"></param>
    ''' <returns></returns>
    Public Shared Function GetTableColumnName(gridColumnName As String) As String

        If ZambaColumns.ContainsKey(gridColumnName) Then
            gridColumnName = ZambaColumns(gridColumnName)
        End If

        Return gridColumnName
    End Function
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

    Public Shared Function AddOrderColumnsQuote(orderString As String) As String
        Return ControlChars.Quote & GetColumnByOrderString(orderString) & ControlChars.Quote & " " & GetOrderByOrderString(orderString)
    End Function

    ''' <summary>
    ''' A traves del string de ordenamiento obtengo el type de la/s columna/s por las que ordenar. 
    ''' Si se ordena por una sola columna: orden = "Tarea ASC" Tipo = "A"
    ''' Si se ordena por varias: orden = "Tarea ASC, Fecha de ingreso DESC, Numero de siniestro ASC" Tipo: "A,D,A".
    ''' </summary>
    ''' <param name="orderString">Es el string de ordenamiento</param>
    ''' <returns></returns>
    Public Shared Function GetColumnsTypeByOrderString(ByVal orderString As String) As String

        Dim columnsOrder As IList = orderString.Split(",")
        Dim columnsType As New StringBuilder
        Dim orderColumn As String = String.Empty

        For Each column As String In columnsOrder
            orderColumn = GetColumnByOrderString(orderString).ToLower()
            If _ColumnsType.ContainsKey(orderColumn) Then
                columnsType.Append(_ColumnsType(orderColumn))
            Else
                columnsType.Append("")
            End If
            If column <> columnsOrder(columnsOrder.Count - 1) Then columnsType.Append(",")
        Next

        Return columnsType.ToString()

    End Function

    ''' <summary>
    ''' Si el string de ordenamiento esta compuesto por mas de una palabra se le agrega un simbolo entre medio.
    ''' </summary>
    ''' <param name="OrderString">Es el string de ordenamiento</param>
    ''' <returns></returns>
    Public Shared Function AddSimbol(ByRef OrderString As String)

        Dim tempOrderSt As New StringBuilder
        Dim columnsOrderStrings As IList = OrderString.Split(",")
        Dim column As String = String.Empty

        For Each orderStr As String In columnsOrderStrings
            column = GridColumns.GetColumnByOrderString(orderStr)
            If column.Split(" ").Length > 1 Then
                Dim order As String = GetOrderByOrderString(orderStr)
                tempOrderSt.Append("[" & column.Replace(" ", "$") & "]" & " " & order)
            Else
                tempOrderSt.Append(orderStr)
            End If

            If orderStr <> columnsOrderStrings(columnsOrderStrings.Count - 1) Then tempOrderSt.Append(", ")
        Next
        OrderString = tempOrderSt.ToString()
    End Function

    ''' <summary>
    ''' A traves del string de ordenamiento obtengo solo el nombre de la columna.
    ''' </summary>
    ''' <param name="orderString">Es el string de ordenamiento completo</param>
    ''' <returns></returns>
    Public Shared Function GetColumnByOrderString(ByVal orderString As String) As String
        Dim orderIndex As Int32 = If((orderString.IndexOf("asc", StringComparison.OrdinalIgnoreCase) <> -1),
                                    orderString.IndexOf("asc", StringComparison.OrdinalIgnoreCase),
                                    orderString.IndexOf("desc", StringComparison.OrdinalIgnoreCase))
        Return orderString.Substring(0, orderIndex).Trim()
    End Function

    ''' <summary>
    ''' A traves del string de ordenamiento obtengo solo la direccion del ordenamiento (ASC, DESC).
    ''' </summary>
    ''' <param name="orderString">Es el string de ordenamiento completo</param>
    ''' <returns></returns>
    Private Shared Function GetOrderByOrderString(orderString As String) As String
        Dim column As String = GetColumnByOrderString(orderString)
        Dim order As String = orderString.Trim().Substring(column.Length).Trim()
        Return order
    End Function

End Class