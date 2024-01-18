Imports Zamba.Servers

Public Class ResultsAsociatedFactory
    Inherits ZClass

    'Public Shared Sub AsociarDocumento(ByVal doctypeID1 As Int32, ByVal indexID1 As Int32, ByVal doctypeId2 As Int32, ByVal indexid2 As Int32)
    '    Dim sql As String = "Select count(1) from DOC_TYPE_R_DOC_TYPE where DoctypeId1=" & doctypeID1 & " and index1=" & indexID1 & " and doctypeid2=" & doctypeId2 & " and index2=" & indexid2
    '    Dim i As Int16 = Server.Con.ExecuteScalar(CommandType.Text, sql)
    '    If i = 0 Then
    '        sql = "Insert into DOC_TYPE_R_DOC_TYPE(doctypeid1,doctypeid2,index1,Index2) values(" & doctypeID1 & "," & doctypeId2 & "," & indexID1 & "," & indexid2 & ")"
    '        Server.Con.ExecuteNonQuery(CommandType.Text, sql)
    '    End If
    'End Sub

    ''' <summary>
    ''' Guarda en la base los documentos asociados
    ''' </summary>
    ''' <param name="Asoc"></param>
    ''' <history>
    ''' 	[Maxi]	    10/11/2005	Modified
    ''' 	[Marcelo]	22/05/2008	Modified
    ''' </history>
    ''' <remarks></remarks>
    Public Shared Sub AsociarDocumentos(ByVal Asoc As Asociados)
        Dim sql As String

        'TODO pasar a factory
        sql = "Insert into DOC_TYPE_R_DOC_TYPE(doctypeid1,doctypeid2,index1,Index2) values(" & Asoc.DocType1.ID & "," & Asoc.DocType2.ID & "," & Asoc.Index1.ID & "," & Asoc.Index2.ID & ")"
        Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        sql = "Insert into DOC_TYPE_R_DOC_TYPE(doctypeid1,doctypeid2,index1,Index2) values(" & Asoc.DocType2.ID & "," & Asoc.DocType1.ID & "," & Asoc.Index2.ID & "," & Asoc.Index1.ID & ")"
        Server.Con.ExecuteNonQuery(CommandType.Text, sql)

        'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
        UserBusiness.Rights.SaveAction(Asoc.DocType1.ID, ObjectTypes.DocTypes, RightsType.VerDocumentosAsociados, "Se agrego la asociacion: " & Asoc.DocTypeAsociatedName & " que asocia el documento " & Asoc.DocType1.Name & " por el atributo " & Asoc.Index1.Name & " al documento " & Asoc.DocType2.Name & " por el atributo " & Asoc.Index2.Name)
    End Sub

    Public Shared Function IsDocsAssociated(ByVal docTypeId As Int64) As Boolean
        'Dim sql As String = "Select count(1) from DOC_TYPE_R_DOC_TYPE where DoctypeId1=" & DocTypeId & " or doctypeid2=" & DocTypeId MAXI 10/11/05
        'Dim i As Int16 = Server.Con.ExecuteScalar(CommandType.Text, sql)
        Dim i As Int16

        If Server.isOracle Then
            Dim parNames() As String = {"pDoctypeId1"}
            Dim parTypes() As Object = {13}
            Dim parValues() As Object = {docTypeId}
            i = Server.Con.ExecuteScalar("ZDtGetDoctypes_pkg.ZDtDOC_TYPE_R_DOC_TYPEById", parValues)
        Else
            Dim parValues() As Object = {docTypeId}
            i = Server.Con.ExecuteScalar("ZDtGetDOC_TYPE_R_DOC_TYPEById", parValues)
        End If


        Return i <> 0

        'If i = 0 Then
        '    Return False
        'Else
        '    Return True
        'End If
    End Function

    ''' <summary>
    ''' M�todo que sirve para obtener los tipos de documento asociados a un entidad en particular
    ''' </summary>
    ''' <param name="DocType">Entidad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	29/12/2008	Modified    Se cambio ArrayList por Generic.List(Of Asociados)
    ''' </history>
    Public Shared Function getDocTypesIdsAsociatedToDocType(ByVal DocType As DocType) As Generic.List(Of Int64)

        'Devuelve los nombres de los entidades asociados
        Dim vec As New Generic.List(Of Int64)
        Dim i As Int16

        'MAXI 10/11/2005
        Dim ds As DataSet = Nothing

        If Server.isOracle Then
            Dim parNames() As String = {"DocTypeId", "UserId", "io_cursor"}
            Dim parTypes() As Object = {13, 13, 5}
            Dim parValues() As Object = {DocType.ID, Membership.MembershipHelper.CurrentUser.ID, 2}
            ds = Server.Con.ExecuteDataset("zsp_docassociated_100.getDocTypesAsociated", parValues)

        Else
            Dim parameters() As Object = {DocType.ID, Membership.MembershipHelper.CurrentUser.ID}
            ds = Server.Con.ExecuteDataset("zsp_docassociated_200_getDocTypesAsociated", parameters)
        End If

        Dim doctypeid As Int64 = 0
        For i = 0 To ds.Tables(0).Rows.Count - 1
            doctypeid = ds.Tables(0).Rows(i).Item("DocTypeId2")
            If vec.Contains(doctypeid) = False Then vec.Add(doctypeid)
        Next

        Return (vec)

    End Function

    ''' <summary>
    ''' M�todo que sirve para obtener los atributos asociados
    ''' </summary>
    ''' <param name="DocType1">Entidad primario</param>
    ''' <param name="DocType2">Entidad asociado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	29/12/2008	Modified    Se cambio ArrayList por Generic.List(Of Asociados)
    ''' </history>
    Public Shared Function getAsociations(ByVal DocType1 As DocType, ByVal DocType2 As DocType) As Generic.List(Of Asociados)

        Dim ds As DataSet = Nothing

        If Server.isOracle Then
            Dim parNames() As String = {"DocTypeId11", "DocTypeId21", "io_cursor"}
            Dim parTypes() As Object = {13, 13, 5}
            Dim parValues() As Object = {DocType1.ID, DocType2.ID, 2}
            ds = Server.Con.ExecuteDataset("zsp_doctypes_100.GetAssociatedIndex", parValues)
        Else
            Dim parValues() As Object = {DocType1.ID, DocType2.ID}
            ds = Server.Con.ExecuteDataset("zsp_doctype_100_GetAssociatedIndex", parValues)
        End If
        Dim Asoc As New Generic.List(Of Asociados)
        Dim i As Int32
        For i = 0 To ds.Tables(0).Rows.Count - 1
            Asoc.Add(New Asociados(DocType1, DocType2, ZCore.GetIndex(ds.Tables(0).Rows(i).Item("Index1")), ZCore.GetIndex(ds.Tables(0).Rows(i).Item("Index2"))))
        Next

        Return (Asoc)

    End Function


    Public Overrides Sub Dispose()

    End Sub

End Class