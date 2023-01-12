Imports Zamba.Core
Public Class SectionFactory
    Public Shared Function GetDocGroups() As DataSet
        Dim strselect As String = "SELECT DOC_TYPE_GROUP_ID, DOC_TYPE_GROUP_NAME, ICON, PARENT_ID, OBJECT_TYPE_ID FROM DOC_TYPE_GROUP"

        Dim DSTEMP As New DataSet
        DSTEMP = Server.Con.ExecuteDataset(CommandType.Text, strselect)
        DSTEMP.Tables(0).TableName = ("DOC_TYPE_GROUP")
        Return DSTEMP
    End Function
    Public Shared Function GetDocGroups(ByVal UserId As Int32) As DataSet
        Dim DsDocGroup As New DataSet
        If Server.isOracle Then
            Dim dstemp As DataSet
            Dim parNames() As String = {"UserId", "io_cursor"}
            ' Dim parTypes() As Object = {13, 5}
            Dim parValues() As Object = {UserId, 2}
            'dstemp = Server.Con.ExecuteDataset("SEARCH_FILLMYTREEVIEW_PKG.fillMyTreeView", parValues)
            dstemp = Server.Con.ExecuteDataset("zsp_doctypes_100.FillMeTreeView", parValues)
            dstemp.Tables(0).TableName = "Doc_Type_Group"
            DsDocGroup.Merge(dstemp)
        Else
            Dim dstemp As DataSet
            Dim parameters() As Object = {UserId}
            'dstemp = Server.Con.ExecuteDataset("FillMyTreeView", parameters)
            dstemp = Server.Con.ExecuteDataset("zsp_doctypes_100_FillMeTreeView", parameters)
            dstemp.Tables(0).TableName = "Doc_Type_Group"
            DsDocGroup.Merge(dstemp)
        End If
        Return DsDocGroup
    End Function
    Public Shared Function GetDocGroupChilds(ByVal UserId As Int32) As DataSet ' DsDocGroup

        Dim DsTemp As DataSet
        Dim StrSelect As String = "SELECT * FROM DOC_TYPE_R_DOC_TYPE_GROUP ORDER BY DOC_TYPE_GROUP,DOC_ORDER"
        DsTemp = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        Return DsTemp
    End Function

    Public Shared Function LoadDocTypes(ByVal DocGroupId As Int64) As DataSet
        'TODO Falta cambiar por Store Procedure
        Dim Ds As New DataSet
        Dim DSTEMP As DataSet
        If Server.isOracle Then
            Dim parNames() As String = {"DocGroupId", "io_cursor"}
            ' Dim parTypes() As Object = {OracleType.Number, OracleType.Cursor}
            Dim parValues() As Object = {DocGroupId, 2}
            DSTEMP = Server.Con.ExecuteDataset("zsp_doctypes_100.GetDocTypesByGroupId", parValues)
            DSTEMP.Tables(0).TableName = "AsignedDocType"
            Ds.Merge(DSTEMP)
        Else
            Dim parameters() As Object = {DocGroupId}
            Ds = Server.Con.ExecuteDataset("zsp_doctypes_100_LoadDocTypes", parameters)
            Ds.Tables(0).TableName = "AsignedDocType"
        End If
        Dim dt1 As DataTable = Ds.Tables("AsignedDocType")
        Dim qRows As Integer = dt1.Rows.Count - 1
        Dim i As Integer
        Dim CondNegation As String = ""
        For i = 0 To qRows
            If i = 0 Then
                CondNegation = " where (Doc_Type_Id <> " & dt1.Rows(i).Item("Doc_Type_ID") & ")"
            Else
                CondNegation = CondNegation & " and (Doc_Type_Id <> " & dt1.Rows(i).Item("Doc_Type_Id") & ")"
            End If
        Next
        Dim strselect As String = ("SELECT Doc_Type_Id, Doc_Type_Name, Object_Type_Id FROM Doc_Type" & CondNegation & " Order By Doc_Type_Name")
        Dim DSTEMP1 As DataSet
        DSTEMP1 = Server.Con.ExecuteDataset(CommandType.Text, strselect)
        DSTEMP1.Tables(0).TableName = "Doc_Type"
        Ds.Merge(DSTEMP1)
        Return Ds
    End Function

    Public Shared Function AddDocGroup(ByVal DocGroupName As String, ByVal DocGroupParentId As Int64, ByVal DocGroupIcon As Integer) As Int32

        Dim NewId As Int32 = CoreData.GetNewID(IdTypes.DOCTYPEID)
        Dim StrInsert As String = "INSERT INTO Doc_Type_Group (DOC_TYPE_GROUP_ID,Doc_Type_Group_Name,Icon,Parent_Id,Object_Type_Id) Values (" _
        & NewId & ",'" & DocGroupName & "', " & DocGroupIcon & ", " & DocGroupParentId & ", 3)"
        Server.Con.ExecuteNonQuery(CommandType.Text, StrInsert)
        Return NewId
    End Function

    Public Shared Function UpdateDocGroup(ByVal DocGroupId As Int64, ByVal DocGroupName As String, ByVal DocGroupParentId As Integer, ByVal DocGroupIcon As Integer) As DataSet
        'PACKAGE UPDATE_DOC_TYPE_GROUP_pkg AS
        'PROCEDURE Update_DocTypeGroup
        Dim StrUpdate As String = "UPDATE Doc_Type_Group Set Doc_Type_Group_Name = '" & DocGroupName & "', Icon = " & DocGroupIcon & ", " _
        & "Parent_Id = " & DocGroupParentId & ", Object_Type_Id = 3 where (Doc_Type_Group_Id = " & DocGroupId & ")"
        Server.Con.ExecuteNonQuery(CommandType.Text, StrUpdate)

        Dim StrSelect As String = "SELECT Doc_Type_Group_Id, Doc_Type_Group_Name, Icon, Parent_Id, Object_Type_Id From Doc_Type_Group ORDER BY Doc_Type_Group_Name"
        Dim DSTEMP As DataSet
        DSTEMP = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        DSTEMP.Tables(0).TableName = "DOC_TYPE_GROUP"
        Return DSTEMP
    End Function

    Public Shared Function DelDocGroup(ByVal DocGroupId As Int64) As DataSet
        Dim StrDelete As String = "DELETE from Doc_Type_Group where (Doc_Type_Group_Id = " & DocGroupId & ")"
        Server.Con.ExecuteNonQuery(CommandType.Text, StrDelete)

        Dim StrSelect As String = "SELECT Doc_Type_Group_Id, Doc_Type_Group_Name, Icon, PARENT_ID, Object_Type_Id From Doc_Type_Group ORDER BY Doc_Type_Group_Name"
        Dim DSTEMP As DataSet
        DSTEMP = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        DSTEMP.Tables(0).TableName = "Doc_Type_Group"
        Return DSTEMP
    End Function

    Public Shared Function DocGroupHasAsigned(ByVal DocGroupId As Int64) As Boolean
        Try
            Dim strSelect As String = "SELECT COUNT(1) from Doc_Type_R_Doc_Type_Group where (Doc_Type_Group = " & DocGroupId & ")"
            Dim Qrows As Int32 = Server.Con.ExecuteScalar(CommandType.Text, strSelect)
            If Qrows <> 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception("Ocurrio un error al consultar la asignacion de este Archivo" & " " & ex.ToString)
        End Try
    End Function

    Public Shared Function DocGroupHasSubGroups(ByVal DocGroupId As Int64) As Boolean
        Try
            Dim strSelect As String = "SELECT COUNT(1) from Doc_Type_Group where (Parent_id = " & DocGroupId & ")"
            Dim Qrows As Int32 = Server.Con.ExecuteScalar(CommandType.Text, strSelect)
            If Qrows <> 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception("Ocurrio un error al consultar los Sub Archivos de este Archivo" & " " & ex.ToString)
        End Try
    End Function

    Public Shared Function DocGroupIsDuplicated(ByVal DocGroupName As String) As Boolean


        Try
            Dim strSelect As String = "SELECT COUNT(1) from Doc_Type_Group where (Doc_Type_Group_Name = '" & Trim(DocGroupName) & "')"
            Dim Qrows As Int32 = Server.Con.ExecuteScalar(CommandType.Text, strSelect)
            If Qrows > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception("Ocurrio un error al consultar la duplicidad del archivo" & " " & ex.ToString)
        End Try
    End Function
    ''' <summary>
    ''' Asigna un Entidad a un sector
    ''' </summary>
    ''' <param name="Orden">Posicion, de base cero, que indicará el orden para mostrar el o los Entidades</param>
    ''' <param name="DocTypeId">ID del Tipo de documento que se desea asignar</param>
    ''' <param name="DocGroupId">ID del Archivo(Sector) al que se le asignará el Entidad</param>
    ''' <remarks></remarks>
    Public Shared Sub AsignDocType(ByVal Orden As Integer, ByVal DocTypeId As Integer, ByVal DocGroupId As Int64)
        Dim STRINSERT As String = "INSERT INTO Doc_Type_R_Doc_Type_Group (Doc_Type_Id, Doc_Type_Group, DOC_ORDER) VALUES (" & DocTypeId & "," & DocGroupId & "," & Orden & ")"
        Server.Con.ExecuteNonQuery(CommandType.Text, STRINSERT)
    End Sub

    ''' <summary>
    ''' Quita un documento de un Archivo(Sector).
    ''' </summary>
    ''' <param name="DocTypeId">Id del entidad que se desea quitar</param>
    ''' <param name="DocGroupId">ID del Archivo(Sector) del cual se desea remover el Tipo de documento</param>
    ''' <remarks> No elimina el documento, solo lo desvincula al archivo</remarks>
    Public Shared Sub RemoveDocType(ByVal DocTypeId As Int32, ByVal DocGroupId As Int64)
        Dim StrDelete As String = "DELETE from Doc_Type_R_Doc_Type_Group where (Doc_Type_Id = " & DocTypeId & ") and (Doc_Type_Group = " & DocGroupId & ")"
        Server.Con.ExecuteNonQuery(CommandType.Text, StrDelete)
    End Sub

End Class
