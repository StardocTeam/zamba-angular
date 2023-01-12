Imports Zamba.Core
Imports Zamba.Servers
Imports System.Data.SqlClient
Imports System.Text

Public Class FormsAdditionalFilesFactory
    ''' <summary>
    ''' Obtiene los datos necesarios para mostrar en la grilla
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAditionalFiles() As DataTable
        Return Server.Con.ExecuteDataset("zsp_forms_getAdditionalFiles").Tables(0)
    End Function

    ''' <summary>
    ''' Agrega los archivos adicionales insertandolos en blob en la base
    ''' </summary>
    ''' <param name="fileExt"></param>
    ''' <param name="path"></param>
    ''' <param name="fileblob"></param>
    ''' <remarks></remarks>
    Sub AddAditionalFile(ByVal fileExt As String, ByVal path As String, ByVal fileblob As Byte())
        Dim param As Object() = {fileExt, path, fileblob}
        Server.Con.ExecuteNonQuery("zsp_forms_AddAditionalFile", param)
    End Sub
    ''' <summary>
    ''' Quita un archivo adicional de la base
    ''' </summary>
    ''' <param name="fileExt"></param>
    ''' <param name="path"></param>
    ''' <remarks></remarks>
    Sub RemoveAditionalFile(ByVal fileExt As String, ByVal path As String)
        Dim param As Object() = {fileExt, path}
        Server.Con.ExecuteNonQuery("zsp_forms_DropAditionalFile", param)
    End Sub

    ''' <summary>
    ''' Obtiene los archivos adicionales que estan en la base 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAditionalFilesAndBlobDocuments() As DataTable
        Return Server.Con.ExecuteDataset(CommandType.Text, "select * from zfrms_AdditionalFiles ").Tables(0)
    End Function
End Class
