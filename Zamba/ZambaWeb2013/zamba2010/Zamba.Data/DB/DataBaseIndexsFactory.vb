Imports ZAMBA.Servers
Imports Zamba.Data
Imports Zamba.Core
Imports system.Text

Public Class DataBaseIndexsFactory
    'Inherits ZClass
    '<Obsolete("Las entidades no pertencen capa de datos" >_
    '    Public Shared Function Create_AllDocDIndex(ByVal doc_id As Integer, ByVal indices As DataTable) As ArrayList
    '        Dim alDocDAll As New ArrayList
    '        Dim dsDocDIndex As DataSet

    '        Dim strselect As New StringBuilder
    '        strselect.Append("SELECT * FROM DOC_D")
    '        strselect.Append(doc_id.ToString)
    '        dsDocDIndex = Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString)

    '        If dsDocDIndex.Tables(0).Rows.Count > 0 Then
    '            Dim i As Integer
    '            For i = 0 To dsDocDIndex.Tables(0).Rows.Count - 1
    '                Dim newdocd As New DocD
    '                newdocd.Index_Id = dsDocDIndex.Tables(0).Rows(i).Item("Index_Id")
    '                newdocd.Index_Name = dsDocDIndex.Tables(0).Rows(i).Item("Index_Name")
    '                newdocd.Index_Created = dsDocDIndex.Tables(0).Rows(i).Item("Index_Created")
    '                newdocd.Index_type = dsDocDIndex.Tables(0).Rows(i).Item("Index_Type")
    '                newdocd.Index_Modified = False

    '                Dim j As Integer
    '                For j = 0 To indices.Rows.Count - 1
    '                    'ME FIJO SI EL CAMPO PERTENECE AL INDICE Y LO CARGO A LA COLECCION CORRESPONDIENTE
    '                    If dsDocDIndex.Tables(0).Rows(i).Item("D" & indices.Rows(j).Item("Index_Id")) Then
    '                        newdocd.Asigned_Columns.Add(New DocDIndex(indices.Rows(j).Item("Index_Id"), indices.Rows(j).Item("Index_Name")))
    '                    Else
    '                        newdocd.No_Asigned_Columns.Add(New DocDIndex(indices.Rows(j).Item("Index_Id"), indices.Rows(j).Item("Index_Name")))
    '                    End If
    '                Next
    '                alDocDAll.Add(newdocd)
    '            Next
    '        End If
    '        Return alDocDAll
    '    End Function

    Public Shared Function GetData(ByVal taskId As Int64) As DataSet
        Dim strselect As New StringBuilder
        strselect.Append("SELECT * FROM DOC_D")
        strselect.Append(taskId.ToString)
        Return Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString)
    End Function

    Public Shared Function Create_AllDocDIndex(ByVal doc_id As Integer, ByVal indices As DataTable) As ArrayList
        'ARMO STRING DE SELECT
        Dim alDocDAll As New ArrayList
        Dim dsDocDIndex As DataSet
        Dim strselect As New System.Text.StringBuilder
        strselect.Append("SELECT * FROM DOC_D")
        strselect.Append(doc_id)
        dsDocDIndex = Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString)
        'CHEQUEO QUE DEVUELVA ALGUNA FILA
        If dsDocDIndex.Tables(0).Rows.Count > 0 Then
            Dim i As Integer
            'ARMO UNA COLECCION CON TODOS LOS OBJETOS DOCD QUE CONTIENEN LOS INDICES DE BUSQUEDA
            For i = 0 To dsDocDIndex.Tables(0).Rows.Count - 1
                Dim newdocd As New DocD
                newdocd.Index_Id = dsDocDIndex.Tables(0).Rows(i).Item("Index_Id")
                newdocd.Index_Name = dsDocDIndex.Tables(0).Rows(i).Item("Index_Name")
                newdocd.Index_Created = dsDocDIndex.Tables(0).Rows(i).Item("Index_Created")
                newdocd.Index_type = dsDocDIndex.Tables(0).Rows(i).Item("Index_Type")
                newdocd.Index_Modified = False

                Dim j As Integer
                For j = 0 To indices.Rows.Count - 1
                    'ME FIJO SI EL CAMPO PERTENECE AL INDICE Y LO CARGO A LA COLECCION CORRESPONDIENTE
                    If dsDocDIndex.Tables(0).Rows(i).Item("D" & indices.Rows(j).Item("Index_Id").ToString) Then
                        newdocd.Asigned_Columns.Add(New DocDIndex(indices.Rows(j).Item("Index_Id"), indices.Rows(j).Item("Index_Name")))
                    Else
                        newdocd.No_Asigned_Columns.Add(New DocDIndex(indices.Rows(j).Item("Index_Id"), indices.Rows(j).Item("Index_Name")))
                    End If
                Next
                alDocDAll.Add(newdocd)
            Next
        End If
        Return alDocDAll
    End Function

    Public Shared Function Create_New_Index(ByVal docId As Integer, ByVal Name As String, ByVal indices As DataTable) As DocD

        'CREO UNA NUEVA INSTANCIA DE LA CLASE DOCD Y LE ASIGNO ID Y NOMBRE INGRESADO POR EL USUARIO
        Dim newdocd As New DocD
        newdocd.Index_Id = CoreData.GetNewID(Zamba.Core.IdTypes.DBINDEX)
        newdocd.Index_Name = Name
        newdocd.Index_Created = False
        newdocd.Index_Modified = False

        'ARMO STRING DE INSERT
        '       Dim cols(indices.Rows.Count + 2) As String
        Dim Columns As New StringBuilder
        Columns.Append("INDEX_ID, INDEX_NAME, INDEX_CREATED")
        Dim Values As New StringBuilder
        Values.Append(newdocd.Index_Id)
        Values.Append(",'")
        Values.Append(Name.Replace("'", ""))
        Values.Append("',0")
        Dim i As Integer = 0
        For i = 0 To indices.Rows.Count - 1
            Columns.Append(",D")
            Columns.Append(indices.Rows(i).Item("Index_Id"))
            Values.Append(",0")
            'COMO EL OBJETO DOCD ES NUEVO NO TIENE CREADO EL INDICE
            newdocd.No_Asigned_Columns.Add(New DocDIndex(indices.Rows(i).Item("Index_Id"), indices.Rows(i).Item("Index_Name")))
        Next



        Dim StrInsert As New StringBuilder
        StrInsert.Append("INSERT INTO DOC_D")
        StrInsert.Append(docId)
        StrInsert.Append(" (")
        StrInsert.Append(Columns)
        StrInsert.Append(") VALUES (")
        StrInsert.Append(Values)
        StrInsert.Append(")")
        'INSERTO EN LA TABLA UNA FILA CON LA INFORMACION DEL NUEVO INDICE

        Server.Con.ExecuteNonQuery(CommandType.Text, StrInsert.ToString)

        StrInsert.Remove(0, StrInsert.Length)
        StrInsert = Nothing

        Return newdocd
    End Function

    Public Shared Function Update_DocD(ByVal docd_obj As DocD, ByVal doc_dindex As Integer, ByVal indices As DataTable) As Integer
        'CREO EL ARRAY CON LOS NOMBRES DE LAS COLUMNAS
        Dim StrUpdate As New StringBuilder

        StrUpdate.Append("UPDATE DOC_D")
        StrUpdate.Append(doc_dindex)
        StrUpdate.Append(" SET")
        'Dim cols(indices.Rows.Count + 1) As String

        StrUpdate.Append(" INDEX_ID = ")
        StrUpdate.Append(docd_obj.Index_Id)
        StrUpdate.Append(",INDEX_CREATED =")
        'cols.SetValue("Index_Id", 0)
        'cols.SetValue("Index_Created", 1)

        If docd_obj.Index_Created Then
            StrUpdate.Append("1")
            'vals.SetValue(1, 1)
        Else
            StrUpdate.Append("0")
            'vals.SetValue(0, 1)
        End If

        Dim j As Integer
        Dim i As Integer = 0
        For i = 0 To indices.Rows.Count - 1
            Dim Col As New StringBuilder
            Col.Append("D")
            Col.Append(indices.Rows(i).Item("Index_Id"))
            StrUpdate.Append(",")
            StrUpdate.Append(Col)
            StrUpdate.Append(" = ")

            'cols.SetValue("I" & indices.Rows(i).Item("Index_Id"), i + 2)

            For j = 0 To docd_obj.Asigned_Columns.Count - 1
                Dim str As New StringBuilder
                str.Append(docd_obj.Asigned_Columns(j).Id)
                If "D" & str.ToString = Col.ToString Then
                    StrUpdate.Append("1")
                    '                   vals.SetValue(1, i)
                End If
            Next

            For j = 0 To docd_obj.No_Asigned_Columns.Count - 1
                Dim str As New StringBuilder
                str.Append(docd_obj.No_Asigned_Columns(j).Id)
                If "D" & str.ToString = Col.ToString Then
                    StrUpdate.Append("0")
                    'vals.SetValue(0, i)
                End If
            Next
        Next

        If docd_obj.Index_type = Zamba.Core.IndexsType.unique Then
            StrUpdate.Append(" ,INDEX_TYPE=1 ")
        Else
            StrUpdate.Append(" ,INDEX_TYPE=0 ")
        End If

        'CREO EL ARRAY CON LOS VALORES DE LAS COLUMNAS
        '      Dim vals(cols.Length - 1)
        '       vals.SetValue(docd_obj.Index_Id, 0)


        StrUpdate.Append(StrUpdate)
        StrUpdate.Append(" WHERE INDEX_NAME = '")
        StrUpdate.Append(docd_obj.Index_Name)
        StrUpdate.Append("'")


        Server.Con.ExecuteNonQuery(CommandType.Text, StrUpdate.ToString)
    End Function

    Public Shared Sub Delete_DocD(ByVal docd_name As String, ByVal doc_dindex As Integer)
        Dim strDelete As New StringBuilder
        strDelete.Append("DELETE FROM DOC_D")
        strDelete.Append(doc_dindex)
        strDelete.Append(" WHERE Index_Name = '")
        strDelete.Append(docd_name)
        strDelete.Append("'")
        'EJECUTO EL QUERY EN LA BASE DE DATOS
        Server.Con.ExecuteNonQuery(CommandType.Text, strDelete.ToString)

        strDelete.Remove(0, strDelete.Length)
        strDelete = Nothing
    End Sub

    Public Shared Sub Create_Database_Index(ByVal docd_obj As DocD, ByVal doc_dindex As Integer, ByVal Cluster As Boolean, ByVal Unico As Boolean)

        Dim strCreateIndex As New StringBuilder
        Dim strIndices As New StringBuilder

        Dim i As Integer
        If Server.isOracle Then
            If Unico = True Then
                strCreateIndex.Append("CREATE UNIQUE INDEX ")
            Else
                strCreateIndex.Append("CREATE INDEX ")
            End If

            ' Tengo que averiguar si existen en Oracle los indices Clusters     
            'If Cluster = True Then
            '    strCreateIndex &= "CLUSTERED INDEX "
            'Else
            '    strCreateIndex &= "NONCLUSTERED INDEX "
            'End If

            strCreateIndex.Append(docd_obj.Index_Name)
            strCreateIndex.Append(" ON DOC_I")
            strCreateIndex.Append(doc_dindex)
            strCreateIndex.Append("(")

            'RECORRO LA COLECCION DE CAMPOS ASIGNADOS AL INDICE
            For i = 0 To docd_obj.Asigned_Columns.Count - 1
                If i = 0 Then
                    strIndices.Append("I")
                    strIndices.Append(docd_obj.Asigned_Columns.Item(i).Id)
                Else
                    strIndices.Append(", I")
                    strIndices.Append(docd_obj.Asigned_Columns.Item(i).Id)
                End If
            Next

        Else
            If Unico = True Then
                strCreateIndex.Append("CREATE UNIQUE ")
            Else
                strCreateIndex.Append("CREATE ")
            End If
            If Cluster = True Then
                strCreateIndex.Append("CLUSTERED INDEX ")
            Else
                strCreateIndex.Append("NONCLUSTERED INDEX ")
            End If
            strCreateIndex.Append(docd_obj.Index_Name)
            strCreateIndex.Append(" ON DOC_I")
            strCreateIndex.Append(doc_dindex)
            strCreateIndex.Append("(")

            'RECORRO LA COLECCION DE CAMPOS ASIGNADOS AL INDICE
            For i = 0 To docd_obj.Asigned_Columns.Count - 1
                If i = 0 Then
                    strIndices.Append("I")
                    strIndices.Append(docd_obj.Asigned_Columns.Item(i).Id)
                Else
                    strIndices.Append(strIndices)
                    strIndices.Append(", I")
                    strIndices.Append(docd_obj.Asigned_Columns.Item(i).Id)
                End If
            Next
        End If


        'EJECUTO EL STRING DE CREACION DEL INDICE EN LA BASE DE DATOS
        Server.Con.ExecuteNonQuery(CommandType.Text, strCreateIndex.ToString & strIndices.ToString & ")")
    End Sub

    Public Shared Sub Drop_Database_Index(ByVal index_name As String, ByVal docd_index As Integer)
        Dim strDelIndex As String = "DROP INDEX " & index_name
        Server.Con.ExecuteNonQuery(CommandType.Text, strDelIndex)
        strDelIndex = Nothing
    End Sub

    Public Shared Sub Delete_Index_Column(ByVal doc_type_id As Int64, ByVal indexes As ArrayList)
        Dim Strselect As String = "SELECT INDEX_NAME FROM DOC_D" & doc_type_id.ToString & " WHERE D" & indexes(0).ToString & " = 1 AND INDEX_CREATED = 1"
        Dim DS As DataSet = Server.Con.ExecuteDataset(CommandType.Text, Strselect)

        Dim I As Integer
        For I = 0 To DS.Tables(0).Rows.Count - 1
            Dim Strdelete As String = "DELETE FROM DOC_D" & doc_type_id.ToString & " WHERE INDEX_NAME = '" & DS.Tables(0).Rows(I).Item("INDEX_NAME").ToString & "'"
            Server.Con.ExecuteNonQuery(CommandType.Text, Strdelete)
            Strdelete = "DROP INDEX DOC_I" & doc_type_id & "." & DS.Tables(0).Rows(I).Item("INDEX_NAME").ToString
            Server.Con.ExecuteNonQuery(CommandType.Text, Strdelete)
        Next


    End Sub

    Public Shared Sub Delete_Index_Table(ByVal doc_type_id As Integer)
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Dim strselect As String = "SELECT INDEX_NAME FROM DOC_D" & doc_type_id & " WHERE INDEX_CREATED = 1"
        Dim DS As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strselect)

        Dim I As Integer
        Dim strdelete As String
        For I = 0 To DS.Tables(0).Rows.Count - 1
            strdelete = "DROP INDEX DOC_I" & doc_type_id & "." & DS.Tables(0).Rows(I).Item("INDEX_NAME")
            Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
        Next

        Dim strDelTable As String = "DROP TABLE DOC_D" & doc_type_id.ToString()

        Server.Con.ExecuteNonQuery(CommandType.Text, strDelTable)
    End Sub

    Public Shared Function VerificarServer() As Boolean
        'ORACLE no permite crear indices Agrupados, entonces lo deshabilito
        If Server.isOracle Then
            'OpAgrupado.Enabled = False
            'OPNoagrupado.Checked = True
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function FillIndexDocType(ByVal docTypeId As Int64) As DataSet
        Dim dstemp As New DataSet()
        If Server.isOracle Then
            Dim parNames() As String = {"IPJOBDocTypeId", "io_cursor"}
            ' Dim parTypes() As Object = {OracleType.Number, OracleType.Cursor}
            Dim parValues() As Object = {docTypeId, 2}
            dstemp = Server.Con.ExecuteDataset("zsp_index_100.FillIndex", parValues)
        Else
            Dim parameters() As Object = {docTypeId}
            dstemp = Server.Con.ExecuteDataset("zsp_index_100_FillIndex", parameters)
        End If
        Return dstemp
    End Function
End Class

