Imports zamba.Core
Imports System.IO

Public Class PlayIfFileExists
    Private fileName As String
    Private myRule As IIfFileExists


    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return results
    End Function

    'Busca el nombre ingresado en el textbox en el/los directorio(s) indicados(s).
    Private Function SearchFile(ByVal myrule As IIfFileExists) As Boolean
        Dim MainDirectory As New DirectoryInfo(myrule.SearchPath)
        Try
            If MainDirectory.GetFiles(fileName, myrule.SearchOption).Length > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Trace.WriteLineIf(ZTrace.IsInfo, "Nombre de archivo incorrecto. " & ex.Message)
        End Try
    End Function
    'DONE: Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myrule As IIfExpireDate, ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
    'SE LE CAMBIO EL TIPO AL ARGUMENTO PORQUE LE ESTABA PASANDO myrule AL PLAY DE UN TIPO QUE NO CORRESPONDIA .{SEBASTIAN}
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
        'Reconocimiento de ZVAR y texto inteligente
        fileName = myrule.TextoInteligente
        If fileName.Contains("zvar") Then
            fileName = WFRuleParent.ReconocerVariables(fileName)
        End If
        If Not IsNothing(results(0)) AndAlso fileName.Contains("<<") AndAlso fileName.Contains(">>") Then
            fileName = Zamba.Core.TextoInteligente.ReconocerCodigo(fileName, results(0))
        End If

        'Verificacion de archivo
        If SearchFile(myrule) = ifType Then
            Return results
        Else
            Return New System.Collections.Generic.List(Of Core.ITaskResult)()
        End If
        Return Nothing
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub New(ByVal rule As IIfFileExists)
        Me.myRule = rule
    End Sub
End Class


'Public Class PlayIfFileExists
'    Private _fileName As String
'    Private _extensionSearch As String = "*.*"

'    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myrule As IIfFileExists) As System.Collections.Generic.List(Of Core.ITaskResult)
'        Try
'            For Each CurrentResult As TaskResult In results
'                _fileName = myrule.SearchPath
'                SearchFile(myrule)
'            Next

'            Return results
'        Catch ex As Exception
'            Zamba.Core.ZClass.RaiseError(ex)
'            Return Nothing
'        End Try
'    End Function
'    Private Function GetFileName(ByVal fileFullPath As String) As String
'        Dim SlashIndex As Integer = fileFullPath.LastIndexOf("\") + 1
'        Dim FileNameLenght As Integer = fileFullPath.Length - SlashIndex

'        Return fileFullPath.Substring(SlashIndex, FileNameLenght)
'    End Function
'    'Busca el archivo por su nombre de archivo , en el path seleccionado en UCIfFileExists. 
'    Private Function SearchFile(ByVal myrule As IIfFileExists) As Boolean
'        LoadExtension()

'        Dim MainDirectory As New DirectoryInfo(myrule.SearchPath)
'        Dim CurrentDirectoryFiles As FileInfo()
'        Dim CurrentFile As FileInfo
'        Dim AllDirectories As DirectoryInfo() = MainDirectory.GetDirectories("*.*", myrule.SearchOption)
'        Dim CurrentDirectory As DirectoryInfo

'        For i As Integer = 0 To AllDirectories.Length - 1
'            CurrentDirectory = AllDirectories(i)
'            CurrentDirectoryFiles = CurrentDirectory.GetFiles(_extensionSearch, myrule.SearchOption)

'            For j As Integer = 0 To CurrentDirectoryFiles.Length - 1
'                CurrentFile = CurrentDirectoryFiles(j)
'                If String.Compare(_fileName, CurrentFile.Name) = 0 Then
'                    Return True
'                End If
'            Next
'        Next

'        Return False
'    End Function

'    'Carga la extension de filtro para agilizar la busqueda . Cualquier cosa usa *.* 
'    Private Sub LoadExtension()
'        If _fileName.Length > 4 Then
'            Dim TemporaryExtension As String = _fileName.Substring(_fileName.Length - 4, 4)

'            If TemporaryExtension.IndexOf(".") <> -1 Then
'                _extensionSearch = TemporaryExtension
'            Else
'                _extensionSearch = "*.*"
'            End If
'        Else
'            _extensionSearch = "*.*"
'        End If
'    End Sub
'End Class
