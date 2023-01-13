Imports Zamba.Core
Imports zamba.Data

Imports Zamba.Servers
Public Class RestrictionsFactory




    Public Shared Function LoadIndex(ByVal doctype As DocType) As DataSet
        '  Dim table As String = "Doc_Index"
        Dim Ds As New DataSet
        Ds = Indexs_Factory.GetIndexSchemaAsDataSet(doctype.ID)
        If Server.IsOracle Then
            Dim dstemp As DataSet
            ''Dim parNames() As String = {"DocTypeId", "io_cursor"}
            ''' Dim parTypes() As Object = {OracleType.Number, OracleType.Cursor}
            Dim parValues() As Object = {doctype.ID, 2}
            dstemp = Server.Con.ExecuteDataset("zsp_index_100.GetDocTypeIndexs", parValues)
            dstemp.Tables(0).TableName = "AsignedIndex"
            Ds.Merge(dstemp)
        Else
            'Dim parameters() As Integer = {doctype.Id}
            'Ds = Server.Con.ExecuteDataset("FrmDocType_LoadIndex", parameters)
            'Se modifico porque el paquete pedia un integer en vez de un array - MC
            Ds = Server.Con.ExecuteDataset("zsp_docindex_100_LoadIndex", doctype.ID)
            Ds.Tables(0).TableName = "AsignedIndex"
        End If
        Dim qRows As Integer = Ds.Tables("AsignedIndex").Rows.Count - 1
        Dim i As Integer
        Dim CondNegation As String
        For i = 0 To qRows
            If i = 0 Then
                CondNegation = " where (Index_Id <> " & Ds.Tables("AsignedIndex").Rows(i).Item("Index_Id") & ")"
            Else
                CondNegation = CondNegation & " and (Index_Id <> " & Ds.Tables("AsignedIndex").Rows(i).Item("Index_Id") & ")"
            End If
        Next
        Dim strselect As String = "SELECT Index_Id, Index_Name, Index_Type, Index_Len, Object_Type_Id FROM Doc_Index" & CondNegation & " Order By Index_Name"
        Dim DSTEMP1 As DataSet
        DSTEMP1 = Server.Con.ExecuteDataset(CommandType.Text, strselect)
        DSTEMP1.Tables(0).TableName = "Doc_Index"
        Ds.Merge(DSTEMP1)
        Return Ds
    End Function

End Class
