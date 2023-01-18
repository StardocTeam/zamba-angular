Imports Zamba.Servers
Imports Zamba.Membership

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

        Dim DTB As New DocTypesBusiness
        'TODO pasar a factory
        sql = "Insert into DOC_TYPE_R_DOC_TYPE(doctypeid1,doctypeid2,index1,Index2) values(" & Asoc.DocTypeid1 & "," & Asoc.DocTypeId2 & "," & Asoc.Index1.ID & "," & Asoc.Index2.ID & ")"
        Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        sql = "Insert into DOC_TYPE_R_DOC_TYPE(doctypeid1,doctypeid2,index1,Index2) values(" & Asoc.DocTypeId2 & "," & Asoc.DocTypeid1 & "," & Asoc.Index2.ID & "," & Asoc.Index1.ID & ")"
        Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        Dim UB As New UserBusiness()
        'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
        UB.SaveAction(Asoc.DocTypeId1, ObjectTypes.DocTypes, RightsType.VerDocumentosAsociados, "Se agrego la asociacion: que asocia el documento " & DTB.GetDocTypeName(Asoc.DocTypeId1) & " por el indice " & Asoc.Index1.Name & " al documento " & DTB.GetDocTypeName(Asoc.DocTypeId2) & " por el indice " & Asoc.Index2.Name)
        DTB = Nothing
        UB = Nothing

    End Sub

    Public Shared Function IsDocsAssociated(ByVal DocTypeId As Int32) As Boolean
        'Dim sql As String = "Select count(1) from DOC_TYPE_R_DOC_TYPE where DoctypeId1=" & DocTypeId & " or doctypeid2=" & DocTypeId MAXI 10/11/05
        'Dim i As Int16 = Server.Con.ExecuteScalar(CommandType.Text, sql)
        Dim i As Int16

        If Server.IsOracle Then
            ''Dim parNames() As String = {"pDoctypeId1"}
            ''' Dim parTypes() As Object = {13}
            Dim parValues() As Object = {DocTypeId}
            i = Server.Con.ExecuteScalar("ZDtGetDoctypes_pkg.ZDtDOC_TYPE_R_DOC_TYPEById", parValues)
        Else
            Dim parValues() As Object = {DocTypeId}
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
    ''' Método que sirve para obtener los tipos de documento asociados a un entidad en particular
    ''' </summary>
    ''' <param name="DocType">Tipo de documento</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	29/12/2008	Modified    Se cambio ArrayList por Generic.List(Of Asociados)
    ''' </history>
    Public Shared Function getDocTypesAsociated(ByVal DocTypeId1 As Int64) As Generic.List(Of Int64)
        'Devuelve los nombres de los tipos de documentos asociados
        Dim vec As New Generic.List(Of Int64)
        Dim i As Int16
        Dim ds As DataSet = Nothing
        Dim core As ZCore

        Try
            ds = Server.Con.ExecuteDataset(CommandType.Text, "SELECT distinct DocTypeId2  From DOC_TYPE_R_DOC_TYPE  Where DocTypeId1 = " & DocTypeId1)
            core = ZCore.GetInstance()

            For i = 0 To ds.Tables(0).Rows.Count - 1
                Dim doctypeid2 As Int64 = ds.Tables(0).Rows(i).Item("DocTypeId2")
                'se reemplaza la utilizacion de zcore, para optimizar la carga en web, se podria optimizar verificando si este metodo se usa recursivamente por varios results, entonces guardarlo en memoria. MARTIN 27/08/08
                vec.Add(doctypeid2)
            Next
        Finally
            ds.Dispose()
            ds = Nothing
            core = Nothing
        End Try

        Return (vec)
    End Function



    ''' <summary>
    ''' Método que sirve para obtener los atributos asociados
    ''' </summary>
    ''' <param name="DocType1">Tipo de documento primario</param>
    ''' <param name="DocType2">Tipo de documento asociado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	29/12/2008	Modified    Se cambio ArrayList por Generic.List(Of Asociados)
    ''' </history>
    Public Shared Function getAsociations(ByVal DocTypeID1 As Int64, ByVal DocTypeId2 As Int64) As Generic.List(Of Asociados)

        Dim ds As DataSet = Nothing
        'MAXI 10/11/05
        'Dim sql As String = "Select Index1,index2 from DOC_TYPE_R_DOC_TYPE where doctypeid1=" & DocType1.Id & " and doctypeId2=" & DocType2.Id
        'ds = Server.Con.ExecuteDataset(CommandType.Text, sql)

        'create proc ZDtGetDtAssInd12ById12 @doctypeId1 int, @doctypeId2 int as
        'Select Index1,index2 from DOC_TYPE_R_DOC_TYPE where doctypeid1= @doctypeid1 and doctypeId2=@doctypeId2

        If Server.isOracle Then
            'Dim parNames() As String = {"DocTypeId11", "DocTypeId21", "io_cursor"}
            '' Dim parTypes() As Object = {13, 13, 5}
            Dim parValues() As Object = {DocTypeID1, DocTypeId2, 2}
            'ds = Server.Con.ExecuteDataset("ZDtGetDoctypes_pkg.ZDtGetInd12ById12", parValues)
            ds = Server.Con.ExecuteDataset("zsp_doctypes_100.GetAssociatedIndex", parValues)
        Else
            Dim parValues() As Object = {DocTypeID1, DocTypeId2}
            'ds = Server.Con.ExecuteDataset("zdtgetind12byid12", parvalues)
            ds = Server.Con.ExecuteDataset("zsp_doctype_100_GetAssociatedIndex", parValues)
        End If
        Dim Asoc As New Generic.List(Of Asociados)
        '    Dim Asoc As New Asociados(DocType1, DocType2, ZCore.GetIndex(ds.Tables(0).Rows(0).Item(0)), ZCore.GetIndex(ds.Tables(0).Rows(0).Item(1)))
        Dim i As Int32

        Dim core As ZCore = ZCore.GetInstance()

        For i = 0 To ds.Tables(0).Rows.Count - 1
            Asoc.Add(New Asociados(DocTypeID1, DocTypeId2, core.GetIndex(ds.Tables(0).Rows(i).Item("Index1")), core.GetIndex(ds.Tables(0).Rows(i).Item("Index2"))))
        Next

        Return (Asoc)

    End Function


    Public Overrides Sub Dispose()

    End Sub

End Class
