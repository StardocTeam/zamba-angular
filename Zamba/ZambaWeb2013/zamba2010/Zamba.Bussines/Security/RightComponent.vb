Imports Zamba.Core
Imports Zamba.Data
Imports Zamba.Servers
Imports Zamba.Users.Factory
Public Class RightComponent

#Region "Metodos Privados"
    Private ReadOnly Property GroupRights() As DataSet
        Get
            If _DsGroupRights Is Nothing Then
                Dim ds As DataSet = Servers.Server.Con.ExecuteDataset(CommandType.Text, "select * from usr_Rights")

                ds.Tables(0).TableName = "usr_rights"
                _DsGroupRights.Merge(ds)
            End If
            Return _DsGroupRights
        End Get
    End Property

    Private _DsGroupRights As DataSet

#End Region
    Public Shared Function GetAllDocTypesByUserRight(ByVal userid As Int64) As Generic.List(Of Int64)
        ' Obtiene todos los doctypes Segun los permisos que se asigno para el usuario
        ' Los permisos por los cual filtra son el de "VER" - "Crear" y "Re Indexar"
        Dim ds As DataSet = RightFactory.GetAllDocTypesByUserRight(userid)
        Dim doctypes As New Generic.List(Of Int64)
        If Not IsNothing(ds) AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In ds.Tables(0).Rows
                If Not IsDBNull(row.Item(2)) AndAlso doctypes.Contains(Int64.Parse(row.Item(2))) = False Then
                    doctypes.Add(Int64.Parse(row.Item(2).ToString))
                End If
            Next
        End If
        Return doctypes
    End Function


    ''' <summary>
    ''' Metodo que trae los documentos por los cual el usuario tiene permisos de crear.
    ''' </summary>
    ''' <param name="Formtype"></param>
    ''' <param name="userid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> [Ezequiel] 31/03/2009 Created
    Public Shared Function GetAllDocTypesByUserRightOfCreate(ByVal userid As Int64) As Generic.List(Of Int64)
        ' Obtiene todos los doctypes Segun los permisos que se asigno para el usuario
        ' Los permisos por los cual filtra es "Crear"
        Dim ds As DataSet = RightFactory.GetAllDocTypesByUserRightOfCreate(userid)
        Dim doctypes As New Generic.List(Of Int64)
        If Not IsNothing(ds) AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In ds.Tables(0).Rows
                If Not IsDBNull(row.Item(2)) AndAlso doctypes.Contains(Int64.Parse(row.Item(2))) = False Then
                    doctypes.Add(Int64.Parse(row.Item(2).ToString))
                End If
            Next
        End If
        Return doctypes
    End Function


End Class

