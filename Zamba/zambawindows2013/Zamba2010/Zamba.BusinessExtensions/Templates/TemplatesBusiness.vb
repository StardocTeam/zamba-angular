Imports Zamba.Data

Public Class TemplatesBusiness

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Método que obtiene un nuevo ID para guardar el Template
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Gaston]	23/01/2009	Created 
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetNewTemplateId() As Int32
        Return (CoreData.GetNewID(IdTypes.TEMPLATE))
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene todos los Templates guardados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [Hernan]	26/05/2006	Created
    ''' [Gaston]    18/04/2008  Modified
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetTemplates() As DataSet
        Return (TemplatesFactory.GetTemplates())
    End Function

    ''' <summary>
    ''' Obtiene todos los Templates guardados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [Diego]	12/09/2008	Created
    ''' </history>
    Public Shared Function GetTemplatesWithIcon() As DataSet
        Dim ds As DataSet = TemplatesFactory.GetTemplates()
        ds.Tables(0).Columns.Add("Icono")
        Dim RB As New Results_Business

        For Each r As DataRow In ds.Tables(0).Rows
            r.Item("Icono") = RB.GetFileIcon(r.Item("Path"))
        Next

        Return ds
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' actualiza el template en la base de datos (Agregar - Modificar o Eliminar)
    ''' </summary>
    ''' <param name="DsTemplates"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    '''     [Gaston]    18/04/2008  Modified
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub SaveTemplates(ByRef DsTemplates As DataSet)
        TemplatesFactory.SaveTemplates(DsTemplates)
        DsTemplates.AcceptChanges()
    End Sub
    ''' <summary>
    ''' Elimina un template
    ''' </summary>
    ''' <param name="id"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    29/04/2008  Created
    ''' </history>
    Public Shared Sub DeleteTemplate(ByVal id As Integer)
        TemplatesFactory.DeleteTemplate(id)
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Elimina de la base de datos un Template
    ''' </summary>
    ''' <param name="FullName"></param>
    ''' <remarks>
    ''' No elimina el template original
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    'Public Shared Sub DeleteTemplate(ByVal FullName As String)
    '    TemplatesFactory.SaveTemplates(D

    '    Dim StrDelete As String = "Delete * from ztempl Where (Path = '" & FullName & "')"
    '    Server.Con.ExecuteNonQuery(CommandType.Text, StrDelete)
    'End Sub

    Public Function ObtainTemplatePath(ByVal id As Int32) As String
        Dim TemplatePath As String
        Dim ds As New DataSet
        ds = Zamba.Servers.Server.Con.ExecuteDataset(CommandType.Text, "SELECT PAth from ZTempl WHERE Id = " + id.ToString())
        If ds.Tables(0).Rows.Count > 0 Then
            TemplatePath = ds.Tables(0).Rows(0).Item(0).ToString()
        Else
            TemplatePath = ""
        End If
        Return TemplatePath
    End Function

    Public Shared Function GetIdByName(ByVal nameTemplate As String) As Int32

        Return TemplatesFactory.GetIdByName(nameTemplate)

    End Function
End Class

