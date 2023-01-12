Imports Zamba.Core
Imports System.Text

Public Class ModDocumentsFactory

    ''' <summary>
    ''' Construye la consulta para obtener los asociados marcados como importantes 
    ''' </summary>
    ''' <param name="strTable">Consulta original</param>
    ''' <param name="Indexs">Atributos donde se encontrarán los filtros</param>
    ''' <param name="ColumnNameAsignado">Alias de la columna Asignado</param>
    ''' <param name="ColumnNameSituacion">Alias de la columna Situacion</param>
    ''' <param name="ColumnNameEstadoTarea">Alias de la columna EstadoTarea</param>
    ''' <returns>StringBuilder con el cuerpo de la consulta casi terminado</returns>
    ''' <remarks></remarks>
    Public Function CreateSelectImportantAsocForm(ByVal strTable As String, _
                                                         ByVal Indexs As ArrayList, _
                                                         ByVal ColumnNameAsignado As String, _
                                                         ByVal ColumnNameSituacion As String, _
                                                         ByVal ColumnNameEstadoTarea As String) As StringBuilder
        Dim strselect As New System.Text.StringBuilder
        Dim auIndex As New List(Of Int64)
        strselect.Append("SELECT ")
        strselect.Append(strTable)
        strselect.Append(".DOC_ID, ")
        strselect.Append(strTable)
        strselect.Append(".DISK_GROUP_ID,PLATTER_ID,VOL_ID,DOC_FILE,OFFSET,")
        strselect.Append(strTable)
        strselect.Append(".DOC_TYPE_ID, ")
        strselect.Append(strTable)
        strselect.Append(".NAME as ""Nombre"", ")
        strselect.Append(strTable)
        strselect.Append(".ICON_ID,SHARED,ver_Parent_id,RootId,")
        If DBToolsFactory.IsOracle Then
            strselect.Append("get_filename(original_Filename)")
        Else
            strselect.Append("REVERSE(SUBSTRING(REVERSE(original_Filename), 0, CHARINDEX('\', REVERSE(original_Filename))))")
        End If
        strselect.Append(" as ""Original"",Version, NumeroVersion as ""Numero de Version"",disk_Vol_id, DISK_VOL_PATH, doc_type_name as ""Entidad"",")
        strselect.Append(strTable)
        strselect.Append(".crdate as ""Creado"", lupdate as ""Modificado"", User_Asigned as """)
        strselect.Append(ColumnNameAsignado)
        strselect.Append(""", Task_State_ID as """)
        strselect.Append(ColumnNameSituacion)
        strselect.Append(""", Do_State_ID as """)
        strselect.Append(ColumnNameEstadoTarea)
        strselect.Append(Chr(34))

        For Each _Index As Index In Indexs
            strselect.Append(",")
            strselect.Append(strTable)
            strselect.Append(".I")
            strselect.Append(_Index.ID)
            If _Index.DropDown = IndexAdditionalType.AutoSustitución Or _Index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                strselect.Append(", slst_s" & _Index.ID & ".descripcion")
                auIndex.Add(_Index.ID)
            End If
            strselect.Append(" as """)
            strselect.Append(_Index.Name)
            strselect.Append(Chr(34))
        Next

        strselect.Append(" FROM ")
        strselect.Append(strTable)
        strselect.Append(" inner join DocumentLabels dl on dl.docid = ")
        strselect.Append(strTable)
        strselect.Append(".doc_id and dl.importance=1 inner join doc_type on doc_type.doc_type_id = ")
        strselect.Append(strTable)
        strselect.Append(".doc_type_id left outer join disk_Volume on disk_Vol_id=vol_id ")

        If auIndex.Count > 0 Then
            For Each indiceID As Int64 In auIndex
                strselect.Append(" left join slst_s" & indiceID & " on " & strTable & ".i" & indiceID & " = slst_s" & indiceID & ".codigo ")
            Next
        End If

        'realizo un inner join con la WFdocument para agregar las
        'columnas faltantes al realizar busquedas en tareas
        strselect.Append("left join wfdocument wd ON ")
        strselect.Append(strTable)
        strselect.Append(".DOC_ID = wd.DOC_ID")

        auIndex = Nothing
        Return strselect
    End Function

End Class
