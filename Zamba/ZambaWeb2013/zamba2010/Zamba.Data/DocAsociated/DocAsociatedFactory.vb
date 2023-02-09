Imports System.Collections.Generic
Imports Zamba.Core
Imports Zamba.Servers
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

    Public Shared Function GetUniqueDocTypeIdsAsociation(ByVal DocTypeId As Int32) As List(Of Int64)
        Try
            Dim ds As DataSet = Servers.Server.Con.ExecuteDataset(CommandType.Text, "select  distinct doctypeID2 from DOC_TYPE_R_DOC_TYPE where doctypeid1=" & DocTypeId)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                Dim Ids As New List(Of Int64)
                For Each r As DataRow In ds.Tables(0).Rows
                    Ids.Add(Int64.Parse(r(0).ToString()))
                Next
                Return Ids
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return New List(Of Int64)
        End Try
    End Function

    Public Shared Function AssocitedAlredyExist(ByVal DocType1 As DocType, ByVal DocType2 As DocType, ByVal Indice1 As Index, ByVal Indice2 As Index) As Boolean
        Dim c1 As Int16, c2 As Int16, c As Int16
        Try
            c1 = Server.Con.ExecuteScalar(CommandType.Text, "select count(1) from DOC_TYPE_R_DOC_TYPE where doctypeid1=" & DocType1.ID & " and doctypeid2=" & DocType2.ID & " and index1=" & Indice1.ID & " and index2=" & Indice2.ID)
            c2 = Server.Con.ExecuteScalar(CommandType.Text, "select count(1) from DOC_TYPE_R_DOC_TYPE where doctypeid1=" & DocType2.ID & " and doctypeid2=" & DocType1.ID & " and index1=" & Indice2.ID & " and index2=" & Indice1.ID)
            c = c1 + c2
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            Return True
        End Try
        If c = 0 Then Return False Else Return True
    End Function


    Public Shared Function AsociatedExists(ByRef R As TaskResult) As Boolean
        Dim StrSelect As String = "Select docTypeID2 from doc_type_r_doc_type where DocTypeID1=" & R.DocType.ID
        Dim D As DataSet = Zamba.Servers.Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        Dim i As Int16 = 0

        If D.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function



    Public Overrides Sub Dispose()

    End Sub


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
        Return (Servers.Server.Con.ExecuteDataset(CommandType.Text, "select Form_Id from Ztype_Zfrms where DocType_Id = " & docTypeId))
    End Function

End Class
