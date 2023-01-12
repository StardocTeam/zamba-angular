Imports Zamba.Core
Imports Zamba.Servers

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Data
''' Class	 : TemplatesFactory
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase que maneja todo lo relativo a la tabla ZTempl, la tabla en donde están los datos de los templates
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
'''     [Gaston]	22/04/2008 29/04/2008   Modified
''' </history>
''' -----------------------------------------------------------------------------

Public Class TemplatesFactory

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene todos los Templates guardados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetTemplates() As DataSet

        Dim Strselect As String = "Select * from ztempl"
        'Dim dstemp As DataSet
        ' Dim i_dsTemplate As DsTemplates = New DsTemplates()

        Using dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, Strselect)
            dstemp.Tables(0).TableName = "Templates"
            '   i_dsTemplate.Merge(dstemp)
            Return dstemp
        End Using

        Return Nothing

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' almacena el template en la base.
    ''' </summary>
    ''' <param name="DsTemplates"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    '''     [Gaston]    29/05/2008  Modified    Se elimino la parte del delete
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub SaveTemplates(ByVal DsTemplates As DataSet)

        Dim i As Int32

        Dim DtTemplates As DataTable = DsTemplates.Tables(0).GetChanges(DataRowState.Added)

        If IsNothing(DtTemplates) = False Then
            For i = 0 To DtTemplates.Rows.Count - 1
                'guardo las reglas agregadas
                Server.Con.ExecuteNonQuery(CommandType.Text, "INSERT INTO ztempl (Id,Name,Description,type,path) VALUES (" & DtTemplates.Rows(i).Item("Id") & ",'" & DtTemplates.Rows(i).Item("Name") & "','" & DtTemplates.Rows(i).Item("Description") & "'," & DtTemplates.Rows(i).Item("Type") & ",'" & DtTemplates.Rows(i).Item("Path") & "')")
            Next
        End If

        DtTemplates = DsTemplates.Tables(0).GetChanges(DataRowState.Modified)

        If IsNothing(DtTemplates) = False Then
            For i = 0 To DtTemplates.Rows.Count - 1
                'guardo las reglas modificadas
                Server.Con.ExecuteNonQuery(CommandType.Text, "UPDATE ztempl SET Id = " & DtTemplates.Rows(i).Item("Id") & " , Name = '" & DtTemplates.Rows(i).Item("Name") & "' , Description = '" & DtTemplates.Rows(i).Item("Description") & "' , Type = " & DtTemplates.Rows(i).Item("Type") & " , Path = '" & DtTemplates.Rows(i).Item("Path") & "' where Id = " & DtTemplates.Rows(i).Item("Id"))
            Next
        End If

    End Sub

    ''' <summary>
    ''' Elimina de la base de datos un template según su id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <remarks></remarks>
    '''' <history>
    '''' 	[Gaston]	29/04/2008	Created
    '''' </history>
    Public Shared Sub DeleteTemplate(ByVal id As Integer)

        Dim query As String = "DELETE FROM ZTempl Where Id = " & id
        Server.Con.ExecuteNonQuery(CommandType.Text, query)

    End Sub

    '''' -----------------------------------------------------------------------------
    '''' <summary>
    '''' Elimina de la base de datos un Template
    '''' </summary>
    '''' <param name="FullName"></param>
    '''' <remarks>
    '''' No elimina el template original
    '''' </remarks>
    '''' <history>
    '''' 	[Hernan]	26/05/2006	Created
    '''' </history>
    '''' -----------------------------------------------------------------------------
    'Public Shared Sub DeleteTemplate(ByVal FullName As String)
    '    Dim StrDelete As String = "Delete * from ztempl Where (Path = '" & FullName & "')"
    '    Server.Con.ExecuteNonQuery(CommandType.Text, StrDelete)
    'End Sub

    ''' <summary>
    ''' Obtiene el el path para llegar al template
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
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

End Class
