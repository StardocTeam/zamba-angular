Imports zamba.Core
Imports System.IO

Public Class PlayIfFileExists
    Private fileName As String

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myrule As IIfFileExists) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim WFRB As New WFRulesBusiness
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando la existencia de Reglas hijas")
        If (myrule.ChildRulesIds Is Nothing OrElse myrule.ChildRulesIds.Count = 0) Then
            myrule.ChildRulesIds = WFRB.GetChildRulesIds(myrule.ID, myrule.RuleClass, results)
        End If

        If myrule.ChildRulesIds.Count = 2 Then
            For Each childruleId As Int64 In myrule.ChildRulesIds
                Dim R As WFRuleParent = WFRB.GetInstanceRuleById(childruleId)
                R.ParentRule = myrule
                R.IsAsync = myrule.IsAsync
                If R.GetType().ToString().Contains("IfBranch") Then
                    Return results
                End If
            Next
        End If
        'Reconocimiento de ZVAR y texto inteligente
        fileName = myrule.TextoInteligente
        If fileName.Contains("zvar") Then
            Dim VarInterReglas As New VariablesInterReglas()
            fileName = VarInterReglas.ReconocerVariables(fileName)
            VarInterReglas = Nothing
        End If
        If Not IsNothing(results(0)) AndAlso fileName.Contains("<<") AndAlso fileName.Contains(">>") Then
            fileName = Zamba.Core.TextoInteligente.ReconocerCodigo(fileName, results(0))
        End If

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Buscando archivo...")
        If SearchFile(myrule) Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Archivo encontrado.")
            Return results
        Else
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Archivo no encontrado.")
            Return New System.Collections.Generic.List(Of Core.ITaskResult)()
        End If
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Archivo no encontrado.")
        Return New System.Collections.Generic.List(Of Core.ITaskResult)()
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
        Catch
            System.Windows.Forms.MessageBox.Show("Nombre de archivo incorrecto")
        End Try
    End Function
    'DONE: Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myrule As IIfExpireDate, ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
    'SE LE CAMBIO EL TIPO AL ARGUMENTO PORQUE LE ESTABA PASANDO myrule AL PLAY DE UN TIPO QUE NO CORRESPONDIA .{SEBASTIAN}
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myrule As IIfFileExists, ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
        'Reconocimiento de ZVAR y texto inteligente
        fileName = myrule.TextoInteligente
        If fileName.Contains("zvar") Then
            Dim VarInterReglas As New VariablesInterReglas()
            fileName = VarInterReglas.ReconocerVariables(fileName)
            VarInterReglas = Nothing
        End If
        If Not IsNothing(results(0)) AndAlso fileName.Contains("<<") AndAlso fileName.Contains(">>") Then
            fileName = Zamba.Core.TextoInteligente.ReconocerCodigo(fileName, results(0))
        End If

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Buscando archivo...")
        'Verificacion de archivo
        If SearchFile(myrule) = ifType Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Archivo encontrado.")
            Return results
        Else
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Archivo no encontrado.")
            Return New System.Collections.Generic.List(Of Core.ITaskResult)()
        End If
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Archivo no encontrado.")
        Return New System.Collections.Generic.List(Of Core.ITaskResult)()
    End Function
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
