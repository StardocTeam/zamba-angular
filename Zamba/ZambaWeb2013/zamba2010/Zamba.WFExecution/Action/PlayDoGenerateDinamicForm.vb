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
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myRule As IDoGenerateDinamicForm) As System.Collections.Generic.List(Of Core.ITaskResult)

        Dim formDinamico As New Core.DynamicFormGenerator()
        Dim objValue As New Object()

        objValue = WFRuleParent.ObtenerValorVariableObjeto(myRule.Variable)

        If Not objValue Is Nothing AndAlso String.IsNullOrEmpty(objValue.ToString) = False Then

            Dim ObjectType As Type = objValue.GetType

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Generando formulario...")

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
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al generar dinamicamente el formulario. Contáctese con el personal de soporte.")
                    'sebastian [10-09-09]
                    'esta parte falta desarrollar

            End Select

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Formulario generado exitosamente.")

        End If

        Return results

    End Function

    Private Function updateFormTable(ByVal dsFormTable As DataSet, ByVal frmName As String, ByVal formId As Integer, Optional ByVal OverrideFile As Boolean = False, Optional ByVal labelName As String = "")

        Dim formDinamico As New Core.DynamicFormGenerator()
        Dim FB As New FormBusiness
        Dim Form As ZwebForm = FB.GetForm(formId)
        FB = Nothing

        Dim frmPath As String
        Dim UP As New UserPreferences
        Try

            ' Se crea una copia del formulario del servidor y se pasa a la máquina local
            If Boolean.Parse(UP.getValue("MakeLocalFormCopy", UPSections.FormPreferences, "False", Zamba.Membership.MembershipHelper.CurrentUser.ID)) = True Then
                frmPath = MakeLocalCopy(Form)
            Else
                Dim Dir As IO.DirectoryInfo = Zamba.Membership.MembershipHelper.AppTempDir("\temp")

                Dim ServerFile As New IO.FileInfo(Form.Path)
                Dim rutaTemp As String = Zamba.Membership.MembershipHelper.AppTempDir("\temp").FullName & "\" & ServerFile.Name

                If (IO.File.Exists(rutaTemp) = False) Then
                    rutaTemp = Membership.MembershipHelper.AppTempPath & "\temp\" & ServerFile.Name
                    If (IO.File.Exists(rutaTemp) = False) Then
                        frmPath = MakeLocalCopy(Form)
                    End If
                Else
                    frmPath = rutaTemp
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
        Finally
            UP = Nothing
        End Try

    End Function

    Private Function ValidateIndex(ByVal indexs As DataSet, ByVal docTypeId As Int64) As DataSet
        'Obtiene indices del entidad
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
            'Si el indice esta en el entidad
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

    Private Function MakeLocalCopy(ByVal Form As ZwebForm) As String

        Dim LocalFile As IO.FileInfo
        Dim Dir As IO.DirectoryInfo = Zamba.Membership.MembershipHelper.AppTempDir("\temp")
        Dim ServerFile As New IO.FileInfo(Form.Path)

        If Dir.Exists = False Then Dir.Create()

        Try

            LocalFile = New IO.FileInfo(Dir.FullName & "\" & ServerFile.Name)

            ServerFile.CopyTo(LocalFile.FullName, True)
            Form.Path = LocalFile.FullName

            Dim RutaArchJs As String = ServerFile.FullName.Remove(ServerFile.FullName.Length - ServerFile.Extension.Length, ServerFile.Extension.Length) + ".js"
            Dim RutaLocalJs As String = LocalFile.FullName.Remove(LocalFile.FullName.Length - LocalFile.Extension.Length, LocalFile.Extension.Length) + ".js"

            If File.Exists(RutaArchJs) Then
                'Sebastián: se borra el archivo local y luego se copia del servidor para que este siempre actualizado
                File.Delete(RutaLocalJs)
                File.Copy(RutaArchJs, RutaLocalJs)
            End If

            Dim RutaArchCss As String = ServerFile.FullName.Remove(ServerFile.FullName.Length - ServerFile.Extension.Length, ServerFile.Extension.Length) + ".css"
            Dim RutaLocalCss As String = LocalFile.FullName.Remove(LocalFile.FullName.Length - LocalFile.Extension.Length, LocalFile.Extension.Length) + ".css"

            If File.Exists(RutaArchCss) Then
                'Sebastián: se borra el archivo local y luego se copia del servidor para que este siempre actualizado
                File.Delete(RutaLocalCss)
                File.Copy(RutaArchCss, RutaLocalCss)
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return String.Empty
        End Try

        Return LocalFile.FullName

    End Function

End Class
