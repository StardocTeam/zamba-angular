Public Class Project
    Implements IProject

    Public Property Description As String Implements IProject.Description
    Public Property ID As Long Implements IProject.ID
    Public Property Name As String Implements IProject.Name

    Public Sub New(ByVal id As Long, ByVal name As String, ByVal desc As String)
        Me.ID = id
        Me.Name = name
        Description = desc
    End Sub

    Public Sub New()
    End Sub
End Class
