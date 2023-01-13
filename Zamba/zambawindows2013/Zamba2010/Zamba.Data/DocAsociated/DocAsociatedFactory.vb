Imports Zamba.Core
Public NotInheritable Class DocAsociatedFactory
    Inherits ZClass
    Public Shared Sub DeleteAsociaton(ByVal DT1Id As Int32, ByVal DT2Id As Int32, ByVal Ind1 As Int32, ByVal Ind2 As Int32)
        Dim where As String = "doctypeid1=" & DT1Id.ToString & " and doctypeid2=" & DT2Id.ToString & " and index1=" & Ind1.ToString & " and index2=" & Ind2.ToString
        Dim strDel As String = "delete from DOC_TYPE_R_DOC_TYPE where " & where
        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strDel)
        where = "doctypeid1=" & DT2Id.ToString & " and doctypeid2=" & DT1Id.ToString & " and index1=" & Ind2.ToString & " and index2=" & Ind1.ToString
        strDel = "delete from DOC_TYPE_R_DOC_TYPE where " & where
        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strDel)
    End Sub
    Public Shared Function GetDocTypeAsociation(ByVal docTypeId As Int64) As DataSet
        Try
            Return Servers.Server.Con.ExecuteDataset(CommandType.Text, "select * from DOC_TYPE_R_DOC_TYPE where doctypeid1=" & docTypeId)
        Catch ex As Exception
            raiseerror(ex)
            Return New DataSet
        End Try
    End Function
    Public Shared Function GetUniqueDocTypeNameAsociation(ByVal DocTypeParentId As Int64) As DataSet
        Try
            Return Servers.Server.Con.ExecuteDataset(CommandType.Text, "select  distinct doctypeID2 from DOC_TYPE_R_DOC_TYPE where doctypeid1=" & DocTypeParentId)
        Catch ex As Exception
            raiseerror(ex)
            Return New DataSet
        End Try
    End Function

    Public Shared Function AssocitedAlredyExist(ByVal DocType1 As DocType, ByVal DocType2 As DocType, ByVal Indice1 As Index, ByVal Indice2 As Index) As Boolean
        Dim c1 As Int16, c2 As Int16, c As Int16
        Try
            c1 = Server.Con.ExecuteScalar(CommandType.Text, "select count(doctypeid1) from DOC_TYPE_R_DOC_TYPE where doctypeid1=" & DocType1.ID & " and doctypeid2=" & DocType2.ID & " and index1=" & Indice1.ID & " and index2=" & Indice2.ID)
            c2 = Server.Con.ExecuteScalar(CommandType.Text, "select count(doctypeid1) from DOC_TYPE_R_DOC_TYPE where doctypeid1=" & DocType2.ID & " and doctypeid2=" & DocType1.ID & " and index1=" & Indice2.ID & " and index2=" & Indice1.ID)
            c = c1 + c2
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            Return True
        End Try
        If c = 0 Then Return False Else Return True
    End Function


    Public Shared Function AsociatedExists(ByRef R As TaskResult) As Boolean
        Dim StrSelect As String = "Select docTypeID2 from doc_type_r_doc_type where DocTypeID1=" & R.DocType.ID
        Dim D As DataSet = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        Dim i As Int16 = 0

        If D.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetDocAsoc(ByVal FolderId As Int32, ByVal ArrayDocTypes As ArrayList) As ArrayList
        Dim Array As New ArrayList
        Try
            Dim StrSelect As String
            Dim D As DataSet

            For Each DocT As Int32 In ArrayDocTypes
                StrSelect = "Select Doc_Id, Doc_Type_Id From Doc_T" & DocT & " Where Folder_Id = " & FolderId
                D = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
                For Each R As DataRow In D.Tables(0).Rows
                    Array.Add(R.Item(0) & "*" & R.Item(1))
                Next
            Next
            Return Array
        Catch ex As Exception
            raiseerror(ex)
            Return Array
        End Try
    End Function

    Public Overrides Sub Dispose()

    End Sub
    Public Shared Function GetDocAsociatedCount(ByVal DocTypeId As Int64) As Int16
        Return Server.Con.ExecuteScalar(CommandType.Text, "select count(doctypeid1) from DOC_TYPE_R_DOC_TYPE where doctypeid1=" & DocTypeId)
    End Function
    ''' <summary>
    ''' Método que sirve para recuperar un FormId en base a un docTypeId
    ''' </summary>
    ''' <param name="docTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	07/07/2008	Created
    ''' </history>
    Public Shared Function getAsociatedFormId(ByVal docTypeId As Integer) As Integer
        Return (Servers.Server.Con.ExecuteScalar(CommandType.Text, "select Form_Id from Ztype_Zfrms where DocType_Id = " & docTypeId))
    End Function
    ''' <summary>
    ''' Método que sirve para recuperar varios FormsId (si hay uno, se retornara uno) en base a un docTypeId
    ''' </summary>
    ''' <param name="docTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	08/07/2008	Created
    ''' </history>
    Public Shared Function getAsociatedFormsId(ByVal docTypeId As Integer) As DataSet
        Return Servers.Server.Con.ExecuteDataset(CommandType.Text, "select Form_Id from Ztype_Zfrms where DocType_Id = " & docTypeId)
    End Function
    Public Shared Function IfDocAsocExists(ByVal DocId As Int32, ByVal FolderId As Int32) As Boolean
        Try
            Dim StrSelect As String = "Select Doc_Id, Doc_Type_Id From Doc_T" & DocId & " Where Folder_Id = " & FolderId
            Dim D As DataSet = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)

            If D.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Function
    Public Shared Function getAsocResultsCountByDTIDAndIndexsAsoc(ByVal treenode As IAdvanceDocTypeNode) As Int64
        Dim strselect As New System.Text.StringBuilder

        If String.IsNullOrEmpty(treenode.IndexID) OrElse IsDBNull(treenode.IndexID) Then
            strselect.Append("select count(1) from (select (I")
        Else
            strselect.Append("select count(1) from (select distinct(I")
        End If

        If String.IsNullOrEmpty(treenode.IndexID) = False AndAlso (treenode.IndexID <> "0") Then
            strselect.Append(treenode.IndexID)
        Else
            strselect.Append(treenode.RelationIndexID2)
        End If

        strselect.Append(") from doc_i")
        strselect.Append(treenode.docTypeID)
        strselect.Append(" where I")
        strselect.Append(treenode.RelationIndexID2)
        strselect.Append(" is not null")

        completeParentsQuery(treenode, strselect)

        If String.IsNullOrEmpty(treenode.IndexID) = False AndAlso (treenode.IndexID <> "0") Then
            strselect.Append(" group by I" & treenode.IndexID)
        End If
        strselect.Append(")")

        If Server.isSQLServer Then strselect.Append(" as q")

        Return Server.Con.ExecuteScalar(CommandType.Text, strselect.ToString)
    End Function

    ''' <summary>
    ''' Agrega a la query los nodos padres
    ''' </summary>
    ''' <param name="treenode">Nodo a buscar los padres</param>
    ''' <param name="strselect">StringBuilder donde se agregara la consulta</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function completeParentsQuery(ByVal treenode As IAdvanceDocTypeNode, ByRef strselect As System.Text.StringBuilder)
        If Not IsNothing(treenode.ParentNode) Then
            strselect.Append(" and I")
            strselect.Append(treenode.RelationIndexID2)

            If String.IsNullOrEmpty(treenode.IndexID) OrElse IsDBNull(treenode.IndexID) Then
                strselect.Append(" in (select I")
            Else
                strselect.Append(" in (select distinct I")
            End If

            strselect.Append(treenode.RelationIndexID1)
            strselect.Append(" from doc_I")
            strselect.Append(treenode.ParentNode.docTypeID)
            strselect.Append(" where I")
            strselect.Append(treenode.ParentNode.RelationIndexID2)
            strselect.Append(" is not null")

            completeParentsQuery(treenode.ParentNode, strselect)

            strselect.Append(")")
        End If
    End Function
    Public Shared Function UpdateResultsCount(ByVal QueryString As String) As Int64
        Return Server.Con.ExecuteScalar(CommandType.Text, QueryString)
    End Function
End Class