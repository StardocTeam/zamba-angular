
Public Class WFUtils

    Public Shared Property IconImage(ByVal IconId As Int16) As Object
        Get
            Return Nothing
        End Get
        Set(ByVal Value As Object)

        End Set
    End Property

    'TODO ver la mantencion de esto, seria optimo tomarlo con reflexion de una DLL que agrupe las interfaces
    Public Shared Function ZambaObjects() As ArrayList
        Dim ZObjects As ArrayList = New ArrayList
        ZObjects.Add("Document")
        ZObjects.Add("Folder")
        ZObjects.Add("Section")
        ZObjects.Add("DocType")
        ZObjects.Add("Index")
        ZObjects.Add("Volume")
        ZObjects.Add("VolumeList")
        ZObjects.Add("User")
        ZObjects.Add("UserGroup")
        ZObjects.Add("Process")
        ZObjects.Add("WF")
        ZObjects.Add("WFStep")
        Return ZObjects
    End Function


End Class
