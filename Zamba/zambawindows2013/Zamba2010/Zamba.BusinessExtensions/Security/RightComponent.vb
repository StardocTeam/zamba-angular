Imports Zamba.Data
Public Class RightComponent

#Region "Metodos Privados"
    Private ReadOnly Property GroupRights() As DataSet
        Get
            If _DsGroupRights Is Nothing Then
                Dim ds As DataSet = Servers.Server.Con.ExecuteDataset(CommandType.Text, "select * from usr_Rights")
                _DsGroupRights = New DataSet
                _DsGroupRights.Merge(ds)
            End If
            Return _DsGroupRights
        End Get
    End Property
    Private Function GetRows(ByVal id As Integer, ByVal ObjectId As ObjectTypes, ByVal RType As Zamba.Core.RightsType, Optional ByVal AditionalParam As Integer = -1) As DataRow()
        Return GroupRights.Tables("usr_rights").Select("GROUPID=" & id & " and OBJID=" & ObjectId & " and RTYPE=" & RType & " and ADITIONAL=" & AditionalParam)
    End Function
    Private _DsGroupRights As DataSet
    'Private _UserArchivosRightView As Hashtable
    'Private _UserArchivosRightSearch As Hashtable
    'Private _UserArchivosRightMail As Hashtable
    'Private _UserArchivosRightdelete As Hashtable
    'Private Sub AddModuleRight(ByVal modulo As ObjectTypes)
    '    Dim key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
    '    Dim iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
    '    Dim sql As String = "Insert into ModulesRights(id,modulo,valor) values (" & modulo & ",'" & modulo.ToString & "','" & Zamba.Tools.Encryption.EncryptString("OK", key, iv) & "')"
    '    Servers.Server.Con.ExecuteNonQuery(CommandType.Text, sql)
    'End Sub
#End Region

    Public Shared CurrentUser As iuser
    Public Function GetUserRights(ByVal ObjectId As ObjectTypes, ByVal RType As Zamba.Core.RightsType, Optional ByVal AditionalParam As Integer = -1) As Boolean
        Return RightsBusiness.GetUserRights(CurrentUser, ObjectId, RType, AditionalParam)
    End Function
    Public Function GetUserRights(ByVal User As iuser, ByVal ObjectId As ObjectTypes, ByVal RType As Zamba.Core.RightsType, Optional ByVal AditionalParam As Integer = -1) As Boolean
        Return RightsBusiness.GetUserRights(User, ObjectId, RType, AditionalParam)
    End Function


    Public Function DelRight(ByVal id As Integer, ByVal ObjectId As ObjectTypes, ByVal RType As Zamba.Core.RightsType, Optional ByVal AditionalParam As Integer = -1) As Boolean
        Return RightFactory.DelRight(id, ObjectId, RType, AditionalParam)
    End Function


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

    Public Function GetAditional(ByVal ObjectType As ObjectTypes, ByVal Rtype As Zamba.Core.RightsType) As ArrayList
        Return RightFactory.GetAditional(ObjectType, Rtype, Membership.MembershipHelper.CurrentUser)
    End Function

End Class

