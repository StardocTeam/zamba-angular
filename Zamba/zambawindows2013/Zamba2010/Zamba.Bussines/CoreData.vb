Imports System.IO
''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Data
''' Class	 : Data.CoreData
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Funciones comunes para Zamba
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class CoreBusiness
    Inherits ZClass
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un nuevo ID
    ''' </summary>
    ''' <param name="IdType">Tipo de objeto para el cual se requiere un nuevo ID</param>
    ''' <returns>Integer</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetNewID(ByVal IdType As Zamba.Core.IdTypes) As Integer
        Return Zamba.Data.CoreData.GetNewID(IdType)
    End Function
    Public Shared Sub SetNewID(ByVal IdType As Zamba.Core.IdTypes, NewId As Int64)
        Zamba.Data.CoreData.SetNewID(IdType, NewId)
    End Sub
    Public Shared Sub EliminarException()
        Dim File As FileInfo = Nothing
        If Directory.Exists(Tools.EnvironmentUtil.GetTempDir("\Exceptions").FullName) = True Then
            For Each Path As String In Directory.GetFiles(Tools.EnvironmentUtil.GetTempDir("\Exceptions").FullName)
                File = New FileInfo(Path)
                Try
                    If File.LastWriteTime.Month > Date.Now.Month Then
                        File.Attributes = FileAttributes.Normal
                        File.Delete()
                    End If
                Catch ex As Exception
                    ZTrace.WriteLineIf(ZTrace.IsInfo, ex.Message)
                Finally
                    File = Nothing
                End Try
            Next
        End If
        GC.Collect()
    End Sub

    Public Overrides Sub Dispose()

    End Sub
End Class
