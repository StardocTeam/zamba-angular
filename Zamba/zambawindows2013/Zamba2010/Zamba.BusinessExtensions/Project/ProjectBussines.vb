Imports Zamba.Data
Imports System.Text

Public Class ProjectBussines

    Delegate Function QuestionDelegate(ByVal question As String) As Boolean

    Shared Property QuestionEvent As QuestionDelegate

    Public Shared Function GetProjectByID(ByVal id As Long) As IProject
        Dim dr As DataRow = ProjectFactory.GetProjectByID(id)

        If dr Is Nothing Then
            Return Nothing
        End If

        Return New Project(dr(0), dr(1), dr(2))
    End Function

    Public Shared Function GetAllProjects() As List(Of IProject)
        Dim dt As DataTable = ProjectFactory.GetAllProjects()

        Return ParseTableToProjects(dt)
    End Function

    Public Shared Function CreateProject(ByVal name As String, ByVal desc As String) As IProject
        If String.IsNullOrEmpty(name) Then
            Throw New InvalidOperationException("El nombre de proyecto no puede ser vacio.")
        End If

        Dim id As Long = Zamba.Core.CoreBusiness.GetNewID(IdTypes.Project)

        ProjectFactory.CreateProject(id, name, desc)

        Return New Project(id, name, desc)
    End Function

    Public Shared Function EditProject(ByVal project As IProject) As IProject
        If String.IsNullOrEmpty(project.Name) Then
            Throw New InvalidOperationException("El nombre de proyecto no puede ser vacio.")
        End If

        ProjectFactory.EditProject(project.ID, project.Name, project.Description)

        Return GetProjectByID(project.ID)
    End Function

    Public Shared Function DeleteProject(ByVal id As Long) As Boolean
        Dim count As Integer = GetProjectAsociationsCount(id)
        Dim confirmation As Boolean = False

        If count > 0 Then
            If QuestionEvent("El proyecto posee " & count & " objetos de Zamba asociados ¿Desea eliminarlo de todas formas?") Then
                confirmation = True
                ProjectFactory.ReasignProjectAsociations(id)
            End If
        Else
            confirmation = True
        End If

        If confirmation Then
            ProjectFactory.DeleteProject(id)
            Return True
        End If

        Return False
    End Function

    Public Shared Function GetProjectAsociations(ByVal id As Long)
        Return ProjectFactory.GetProjectAsociations(id)
    End Function

    Public Shared Function CreateProjectAsociation(ByVal projectID As Long, ByVal objType As Long, ByVal objID As Long) As Boolean
        If ProjectFactory.CreateProjectAsociation(projectID, objType, objID) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function DeleteProjectAsociation(ByVal projectID As Long, ByVal objType As Long, ByVal objID As Long) As Boolean
        If ProjectFactory.DeleteProjectAsociation(projectID, objType, objID) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetProjectAsociationsCount(ByVal projectID As Long) As Long
        Return ProjectFactory.GetProjectAsociationsCount(projectID)
    End Function

    Shared Function GetProjectGroupAsociations(ByVal groupID As Long) As List(Of IProject)
        Dim dt As DataTable = ProjectFactory.GetProjectGroupAsociations(groupID)

        Return ParseTableToProjects(dt)
    End Function

    Shared Function GetProjectGroupAvailable(ByVal groupID As Long) As List(Of IProject)
        Dim dt As DataTable = ProjectFactory.GetProjectGroupAvailable(groupID)
        Return ParseTableToProjects(dt)
    End Function

    Shared Function GetProjectsByUser(ByVal user As IUser) As List(Of IProject)
        Dim sbGrups As New StringBuilder

        For Each group As UserGroup In user.Groups
            sbGrups.Append(group.ID)
            sbGrups.Append("|")
        Next

        Dim dt As DataTable = ProjectFactory.GetProjectsByUser(sbGrups.ToString())
        Return ParseTableToProjects(dt)
    End Function

    Shared Function ParseTableToProjects(ByRef dt As DataTable) As List(Of IProject)
        If dt Is Nothing OrElse dt.Rows.Count < 1 Then
            Return Nothing
        End If

        Dim max As Integer = dt.Rows.Count
        Dim returnList As New List(Of IProject)
        For i As Integer = 0 To max - 1
            returnList.Add(New Project(dt.Rows(i)(0), dt.Rows(i)(1), dt.Rows(i)(2)))
        Next

        Return returnList
    End Function

End Class
