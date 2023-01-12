Imports FormulariosDinamicos
Imports Zamba.Core
Imports System.IO
Imports System.Windows.Forms

Public Class PlayDoGenerateDinamicForm


    ''' <summary>
    ''' Crea un formulario virtual a partir de un dataset
    ''' </summary>
    ''' <param name="results"></param>
    ''' <param name="myRule"></param>
    ''' <history>   Sebastian   Created 15/09/2009
    '''             Marcelo     Modified    16/09/2009
    '''</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 

    Private myRule As IDoGenerateDinamicForm
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        Dim formDinamico As New FormulariosDinamicos.DynamicFormsGenerate()
        Dim objValue As New Object()

        objValue = WFRuleParent.ObtenerValorVariableObjeto(myRule.Variable)

        If Not objValue Is Nothing AndAlso String.IsNullOrEmpty(objValue.ToString) = False Then

            Dim ObjectType As Type = objValue.GetType

            Trace.WriteLineIf(ZTrace.IsInfo, "Generando formulario...")

            Select Case ObjectType.Name.ToLower()

                Case "dataset"

                    Dim dsValues As DataSet = DirectCast(objValue, DataSet)

                    ValidateIndex(dsValues, myRule.DocType)

                    If myRule.FormId = 0 Then
                        formDinamico.CreateTable(dsValues, myRule.Name, True, myRule.ColumnName)
                    Else
                        updateFormTable(dsValues, myRule.Name, myRule.FormId, True, myRule.ColumnName)
                    End If

                Case "datatable"

                    Dim dsValues As New DataSet

                    dsValues.Tables(0).Merge(DirectCast(objValue, DataTable))

                    ValidateIndex(dsValues, myRule.DocType)

                    If myRule.FormId = 0 Then
                        formDinamico.CreateTable(dsValues, myRule.Name, True, myRule.ColumnName)
                    Else
                        updateFormTable(dsValues, myRule.Name, myRule.FormId, True, myRule.ColumnName)
                    End If

                Case "arraylist"
                    '[Tomas] Se creo el trace solamente para que pueda ser encontrado facilmente
                    '        y que se sepa que esta parte faltaba desarrollar.
                    Trace.WriteLineIf(ZTrace.IsInfo, "Error al generar dinamicamente el formulario. Contáctese con el personal de soporte.")
                    'sebastian [10-09-09]
                    'esta parte falta desarrollar

            End Select

            Trace.WriteLineIf(ZTrace.IsInfo, "Formulario generado exitosamente.")

        End If

        Return results

    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Private Function updateFormTable(ByVal dsFormTable As DataSet, ByVal frmName As String, ByVal formId As Integer, Optional ByVal OverrideFile As Boolean = False, Optional ByVal labelName As String = "")

        Dim formDinamico As New FormulariosDinamicos.DynamicFormsGenerate()
        Dim form As ZwebForm = FormBusiness.GetForm(formId)
        Dim frmPath As String

        Try
            ' Se crea una copia del formulario del servidor y se pasa a la máquina local
            If Boolean.Parse(UserPreferences.getValue("MakeLocalFormCopy", Sections.FormPreferences, "False")) = True Then
                frmPath = MakeLocalCopy(form)
            Else
                frmPath = form.TempFullPath

                If Not IO.File.Exists(frmPath) Then
                    frmPath = Application.StartupPath & "\temp\" & form.TempPathName
                    If Not IO.File.Exists(frmPath) Then
                        frmPath = MakeLocalCopy(form)
                    End If
                End If
            End If

            'si tengo un form ...
            If frmPath <> String.Empty Then
                Dim Str As New StreamReader(frmPath)
                Dim a As New StreamReader(Str.BaseStream)
                Dim strHtml As String = a.ReadToEnd()
                a.Close()

                formDinamico.UpdateTable(dsFormTable, strHtml, frmName, OverrideFile, labelName)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Function

    Private Function ValidateIndex(ByVal indexs As DataSet, ByVal docTypeId As Int64) As DataSet
        'Obtiene atributos de la entidad
        Dim dsDocTypeIndexs As DataSet = IndexsBusiness.GetIndexSchemaAsDataSet(docTypeId)
        Dim dt As DataTable = New DataTable
        dt = indexs.Tables(0).Clone()
        'Dim count As Int64 = indexs.Tables(0).Rows.Count
        'Dim Idx As Int64 = 0
        'Dim RemoveRow As Boolean = True

        'Para filtrar por id de indice
        Dim dv As New DataView(dsDocTypeIndexs.Tables(0))

        'Por cada fila en el dataset de la variable
        For Each dr As DataRow In indexs.Tables(0).Rows
            'Si el atributo esta en la entidad
            dv.RowFilter = "index_id=" & dr("index_id")
            If dv.ToTable().Rows.Count > 0 Then
                'Se agrega al datatable temporal
                dt.Rows.Add(dr.ItemArray())
            End If
        Next

        'Se guardan solamente los que estan
        indexs = New DataSet()
        indexs.Tables.Add(dt)

        'While count >= 0
        '    For Each DocTypeRow As DataRow In dsDocTypeIndexs.Tables(0).Rows
        '        If DocTypeRow("index_id").Equals(indexs.Tables(0).Rows(Idx)("index_id")) Then
        '            RemoveRow = False
        '            Exit For
        '        End If
        '    Next

        '    If RemoveRow = True Then
        '        indexs.Tables(0).Rows.Remove(indexs.Tables(0).Rows(Idx))
        '        count -= 1
        '    Else
        '        Idx += 1
        '    End If

        '    RemoveRow = True

        '    If indexs.Tables(0).Rows.Count = dsDocTypeIndexs.Tables(0).Rows.Count Then
        '        Exit While
        '    End If
        'End While

        Return indexs
    End Function

    Private Function MakeLocalCopy(ByVal form As ZwebForm) As String
        Try
            Dim serverFile As New IO.FileInfo(form.Path)
            Dim localFile As New IO.FileInfo(form.TempFullPath)

            Dim tempDir As IO.DirectoryInfo = Tools.EnvironmentUtil.GetTempDir("\temp")
            If tempDir.Exists = False Then tempDir.Create()
            If File.Exists(localFile.FullName) Then
                If File.GetAttributes(localFile.FullName).ToString.Contains("ReadOnly") Then
                    File.SetAttributes(localFile.FullName, FileAttributes.Normal)
                End If
            End If

            If form.UseBlob Then
                Dim frmBusinessExt As New FormBusinessExt
                form.Path = frmBusinessExt.CopyBlobToTemp(form, False)
                frmBusinessExt = Nothing
            Else
                form.Path = localFile.FullName
                Try
                    If serverFile.Exists AndAlso ((localFile.Exists AndAlso serverFile.LastWriteTime > localFile.LastWriteTime) OrElse localFile.Exists = False) Then

                        serverFile.CopyTo(localFile.FullName, True)
                    End If
                Catch ex As Exception
                End Try
            End If

            CopyFormAssociatedFiles(serverFile, localFile, ".js")
            CopyFormAssociatedFiles(serverFile, localFile, ".css")

            Return localFile.FullName

        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return String.Empty
        End Try
    End Function

    ''' <summary>
    ''' Utilizado para copiar archivos asociados al formulario como los css o js
    ''' </summary>
    ''' <param name="server">Archivo de origen</param>
    ''' <param name="local">Archivo de destino</param>
    ''' <param name="extension">Extensión del archivo</param>
    ''' <remarks></remarks>
    Private Sub CopyFormAssociatedFiles(ByVal server As FileInfo, ByVal local As FileInfo, ByVal extension As String)
        Dim serverPath As String = server.FullName.Remove(server.FullName.Length - server.Extension.Length, server.Extension.Length) + extension
        Dim localPath As String = local.FullName.Remove(local.FullName.Length - local.Extension.Length, local.Extension.Length) + extension
        Dim fi As New IO.FileInfo(localPath)
        Dim fo As New IO.FileInfo(serverPath)

        If fo.Exists AndAlso ((fi.Exists AndAlso fo.LastWriteTime > fi.LastWriteTime) OrElse fi.Exists = False) Then
            fo.CopyTo(localPath, True)
        End If
    End Sub

    Public Sub New(ByVal rule As IDoGenerateDinamicForm)
        Me.myRule = rule
    End Sub
End Class
