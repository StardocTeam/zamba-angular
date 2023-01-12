Imports System.IO
Imports System.Runtime.Serialization.Formatters.Soap

''' <summary>
''' MODELO PARA PERSISTIR UN OBJETO WORKFLOW
''' 13/SEPTIEMBRE/2006
''' </summary>
''' <remarks></remarks>
Public Class WFPersistModel
    ''' <summary>
    ''' Permite serializar un Workflow en una ruta especificada
    ''' </summary>
    ''' <param name="Wf">Objeto Workflow que se desea persistir</param>
    ''' <param name="Path">Opcional. Ruta donde se desea guardar el objeto. Si no se especifica, se guardará en la carpeta WF</param>
    ''' <remarks>Se recomienda que la ruta no sea una ruta local</remarks>
    Public Shared Sub PersistWF(ByVal Wf As Zamba.Core.WorkFlow, Optional ByVal Path As String = "")
        If String.IsNullOrEmpty(Path) Then
            Path = ".\WF\data.xml"
            If Directory.Exists(".\WF") = False Then Directory.CreateDirectory(".\WF")
        End If
        Dim stream As Stream = File.Open(Path, FileMode.Create)
        Dim formatter As New SoapFormatter()
        formatter.Serialize(stream, Wf)
        stream.Close()
        Wf = Nothing
    End Sub
    ''' <summary>
    ''' Devuelve un objeto WF en base a un objeto serializado
    ''' </summary>
    ''' <param name="Path">Ruta de donde se tomara el objeto a desserializar</param>
    ''' <returns>Objeto Workflow</returns>
    ''' <remarks></remarks>
    Public Shared Function GetWorkflow(ByVal Path As String) As Zamba.Core.WorkFlow
        If File.Exists(Path) Then Return Nothing
        Dim stream As Stream = File.Open(Path, FileMode.Open)
        Dim formatter As SoapFormatter = New SoapFormatter()
        Dim WF As Zamba.Core.WorkFlow = CType(formatter.Deserialize(stream), Zamba.Core.WorkFlow)
        stream.Close()
        Return WF
    End Function
    ''' <summary>
    ''' Devuelve la fecha y hora en la que fue persistido el objeto Workflow
    ''' </summary>
    ''' <param name="path">Ruta donde se encuentra el objeto serializado</param>
    ''' <returns>Fecha y Hora de creación</returns>
    ''' <remarks></remarks>
    Public Shared Function GetCreationDate(ByVal path As String) As Date
        If File.Exists(path) Then
            Dim f As New FileInfo(path)
            Return f.CreationTime
        End If
    End Function
    ''' <summary>
    ''' Recibe dos objetos serializados y devuelve la instancia del objeto mas nuevo
    ''' </summary>
    ''' <param name="ObjectPath1">Ruta a un objeto serializado</param>
    ''' <param name="ObjectPath2">Ruta a otro objeto serializado</param>
    ''' <returns>Objeto Zamba.Core.Workflow</returns>
    ''' <remarks></remarks>
    Public Shared Function GetNewerWorkflow(ByVal ObjectPath1 As String, ByVal ObjectPath2 As String) As Zamba.Core.WorkFlow
        If GetCreationDate(ObjectPath1) > GetCreationDate(ObjectPath2) Then
            Return GetWorkflow(ObjectPath1)
        Else
            Return GetWorkflow(ObjectPath2)
        End If
    End Function
    ''' <summary>
    ''' Genera un Arraylist de objetos WorkFlows
    ''' </summary>
    ''' <param name="Paths">Arraylist con rutas a archivos xml con objetos workflows persistidos</param>
    ''' <returns>Arraylist con objetos Workflows</returns>
    ''' <remarks></remarks>
    Public Shared Function GetAllWorkflows(ByVal Paths As ArrayList) As ArrayList
        Dim Array As ArrayList = New ArrayList
        For Each path As String In Paths
            Array.Add(GetWorkflow(path))
        Next
        Return Array
    End Function
    ''' <summary>
    ''' Elimina un workflow serializado
    ''' </summary>
    ''' <param name="path">Ruta del archivo que contiene el objeto workflow y se desea eliminar</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteWorkflow(ByVal path As String)
        If File.Exists(path) Then File.Delete(path)
    End Sub

End Class
