Imports Zamba.Data
Public Class Section_Factory
    Public Shared Function GetDocGroups() As DataSet
        Return SectionFactory.GetDocGroups
    End Function
    Public Shared Function GetDocGroups(ByVal UserId As Int64) As DataSet
        Return SectionFactory.GetDocGroups(UserId)
    End Function
    Public Shared Function GetDocGroupChilds(ByVal UserId As Int64) As DataSet ' DsDocGroup
        Return SectionFactory.GetDocGroupChilds(UserId)
    End Function

    Public Shared Function LoadDocTypes(ByVal DocGroupId As Int64) As DataSet
        Return SectionFactory.LoadDocTypes(DocGroupId)
    End Function

    Public Shared Function AddDocGroup(ByVal DocGroupName As String, ByVal DocGroupParentId As Int64, ByVal DocGroupIcon As Integer) As Int32
        Return SectionFactory.AddDocGroup(DocGroupName, DocGroupParentId, DocGroupIcon)
    End Function

    Public Shared Function UpdateDocGroup(ByVal DocGroupId As Int64, ByVal DocGroupName As String, ByVal DocGroupParentId As Integer, ByVal DocGroupIcon As Integer) As DataSet
        Return SectionFactory.UpdateDocGroup(DocGroupId, DocGroupName, DocGroupParentId, DocGroupIcon)
    End Function

    Public Shared Function DelDocGroup(ByVal DocGroupId As Int64) As DataSet
        Return SectionFactory.DelDocGroup(DocGroupId)
    End Function

    Public Shared Function DocGroupHasAsigned(ByVal DocGroupId As Int64) As Boolean
        Return SectionFactory.DocGroupHasAsigned(DocGroupId)
    End Function

    Public Shared Function DocGroupHasSubGroups(ByVal DocGroupId As Int64) As Boolean
        Return SectionFactory.DocGroupHasSubGroups(DocGroupId)
    End Function

    Public Shared Function DocGroupIsDuplicated(ByVal DocGroupName As String) As Boolean
        Return SectionFactory.DocGroupIsDuplicated(DocGroupName)
    End Function
    ''' <summary>
    ''' Asigna un Entidad a un sector
    ''' </summary>
    ''' <param name="Orden">Posicion, de base cero, que indicará el orden para mostrar el o los Entidades</param>
    ''' <param name="DocTypeId">ID de la entidad que se desea asignar</param>
    ''' <param name="DocGroupId">ID del Archivo(Sector) al que se le asignará la entidad</param>
    ''' <remarks></remarks>
    Public Shared Sub AsignDocType(ByVal Orden As Integer, ByVal DocTypeId As Integer, ByVal DocGroupId As Int64)
        SectionFactory.AsignDocType(Orden, DocTypeId, DocGroupId)
    End Sub

    ''' <summary>
    ''' Quita un documento de un Archivo(Sector).
    ''' </summary>
    ''' <param name="DocTypeId">Id de la entidad que se desea quitar</param>
    ''' <param name="DocGroupId">ID del Archivo(Sector) del cual se desea remover la entidad</param>
    ''' <remarks> No elimina el documento, solo lo desvincula al archivo</remarks>
    Public Shared Sub RemoveDocType(ByVal docTypeId As Int64, ByVal DocGroupId As Int64)
        SectionFactory.RemoveDocType(DocTypeId, DocGroupId)
    End Sub
    Public Shared Sub Fill(ByRef docGroup As IDocGroup)

    End Sub

End Class
