Imports Zamba.Core
Imports Zamba.Data
Imports System.IO
Imports Zamba.Tools
Imports Zamba.Core.WF.WF

Public Class PlayDoSaveFile

    Private _myrule As IDOSaveFile
    'Private _barcodeId As Int64
    Private _res As NewResult
    Private indices As SortedList
    Private indexvalue As String
    Private ruleindicesaux As String
    Private strItem As String
    Private value As String
    Private id As Int32
    Private top As String
    Private left As String


    Public Sub New(ByVal rule As IDOSaveFile)
        Me._myrule = rule

    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        Dim newResults As New System.Collections.Generic.List(Of Core.ITaskResult)
        Dim wi As New Zamba.Office.WordInterop()
        Me.indices = New SortedList()
        Me.indices.Clear()
        Me.indexvalue = String.Empty
        Dim rutaTemp As String
        Dim stream As StreamWriter
        Dim st As FileStream

 

        For Each r As ITaskResult In results
            _res = New NewResult()


            '_barcodeId = Zamba.Core.ToolsBussines.GetNewID(Zamba.Core.IdTypes.Caratulas)
            '_res.Parent = DocTypesBusiness.GetDocType(_myrule.docTypeId, True)
            _res.DocumentalId = 0
            '_res.Indexs = ZCore.FilterIndex(_myrule.docTypeId, False)
            _res.ID = Zamba.Core.ToolsBussines.GetNewID(Zamba.Core.IdTypes.DOCID)
            _res.FolderId = Zamba.Core.ToolsBussines.GetNewID(Zamba.Core.IdTypes.FOLDERID)

            'Dim filepath As String = Zamba.Core.TextoInteligente.ReconocerCodigo(_myrule.FilePath, r)

            'filepath = WFRuleParent.ReconocerVariables(filepath)

            Dim TextoToSave As String = Zamba.Core.TextoInteligente.ReconocerCodigo(_myrule.TextToSave, r)
            TextoToSave = WFRuleParent.ReconocerVariables(TextoToSave)

            'Dim file As New FileInfo(filepath)
            rutaTemp = Me._myrule.FilePath 'EnvironmentUtil.GetTempDir("\\temp").ToString & "\\" & DateTime.Now.ToString("dd-MM-yy HH-mm-ss") & file.Name & Me._myrule.FileExtension

            Dim pathtemp As Object = rutaTemp

            Dim filePathnew As String = String.Empty


       

            filePathnew = rutaTemp.Trim() + "\" + Me._myrule.FileName + " " + DateTime.Now.ToString("dd-MM-yy HH-mm-ss") + Me._myrule.FileExtension 'rutaTemp"

            Using sw As StreamWriter = New StreamWriter(filePathnew)
                sw.Write(TextoToSave)
            End Using

            'My.Computer.FileSystem.WriteAllText(filePathnew, TextoToSave, False)



            'Seteamos una variable que guarda el path del documento local
            If WFRuleParent.VariablesInterReglas.ContainsKey(Me._myrule.VarFilePath) = Nothing Then
                WFRuleParent.VariablesInterReglas.Add(Me._myrule.VarFilePath, filePathnew)
            Else
                WFRuleParent.VariablesInterReglas(Me._myrule.VarFilePath) = filePathnew
            End If


            Try
                'Dim name As String
                'name = DateTime.Now.ToString("dd-MM-yy HH-mm-ss")
                'Dim filetosave As New IO.FileInfo(rutaTemp)
                'Dim saveFileDialog As New Windows.Forms.SaveFileDialog

                'With saveFileDialog
                '    .DefaultExt = "txt"



                '    .FileName = rutaTemp
                '    .Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
                '    .FilterIndex = 1
                '    .OverwritePrompt = True
                '    .Title = "Guardar Archivo"
                'End With

                'System.IO.Path.Combine( _
                '          Me._myrule.FilePath, _
                '    rutaTemp)
                'Me.txtFilePath.Text = saveFileDialog.FileName



            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try


            'stream = New System.IO.StreamWriter(rutaTemp)
            'stream.Write(TextoToSave)
            'stream.Close()








        Next

        Return results

    End Function
End Class
