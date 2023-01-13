Imports HtmlAgilityPack

Public Class VarsBusiness

    Public Function PersistVariable(VarName As String, TaskId As Int64, VarValue As String) As Boolean
        Zamba.Servers.Server.Con().ExecuteNonQuery(CommandType.Text, String.Format("INSERT INTO Zvars ([VarName],[TaskId],[Value]) VALUES('{0}',{1},'{2}')", VarName, TaskId, VarValue))
    End Function


    Public Function GetVariableValue(VarName, TaskId) As String
        Return Zamba.Servers.Server.Con().ExecuteScalar(CommandType.Text, String.Format("SELECT Value FROM zvars where VarName = '{0}' and TaskId = {1}", VarName, TaskId))
    End Function

    Public Function AsignVarsValues(strhtml As String) As String
        Dim html As New HtmlDocument
        Dim htmlElement As New HtmlDocument
        'strhtml = strhtml.Replace(vbCr, "").Replace(vbLf, "").Replace(vbCrLf, "").Replace(vbTab, "")
        html.LoadHtml(strhtml)
        strhtml = html.DocumentNode.OuterHtml
        For Each varName As String In VariablesInterReglas.Keys()
            Dim e As HtmlNode = html.GetElementbyId(String.Format("zvar_{0}", varName))
            If e Is Nothing Then
                e = html.GetElementbyId(String.Format("zvar({0})", varName))
            End If

            If e IsNot Nothing Then

                If (TypeOf VariablesInterReglas.Item(varName) Is DataSet) Then
                    Dim tableNode As HtmlNode = e.SelectSingleNode("//table")
                    Dim tablebodyNode As HtmlNode = e.SelectSingleNode("//tbody")

                    '   e.InnerHtml = FormControlsController.LoadZVarTableHeader(e.InnerHtml, DirectCast(VariablesInterReglas.Item(s), DataSet).Table(0).Columns, tbody)
                    '   e.InnerHtml = FormControlsController.LoadZVarTableBody(e.InnerHtml, DirectCast(VariablesInterReglas.Item(s), DataSet).Table(0).Rows, tbody, String.Empty)


                Else


                    Try
                        Dim TagName As String = e.Name 'e.GetAttributeValue("type", "text")
                        Dim VarValue As String = VariablesInterReglas.Item(varName)
                        Dim oldTag As String = e.OuterHtml

                        'Filtra por tipo de control...
                        Select Case TagName
                            Case "input" ', "SELECT"

                                Dim TypeAttibute As String = e.GetAttributeValue("type", "text")

                                Select Case TypeAttibute
                                    Case "text", "hidden"

                                        e.SetAttributeValue("value", VarValue)

                                    Case "checkbox"
                                        If IsNothing(VarValue) OrElse VarValue = "0" OrElse VarValue = String.Empty Then
                                            'value = value.Insert(value.Length - 1, " checked= " & Chr(34) & "0" & Chr(34))
                                        Else
                                            e.SetAttributeValue("checked", 1)
                                        End If
                                    Case "radio"
                                        Dim id As String = String.Empty
                                        If IsNothing(VarValue) Then
                                            If id.ToUpper().EndsWith("S") Then
                                                e.SetAttributeValue("checked", Chr(34) & "False" & Chr(34))
                                            ElseIf id.ToUpper().EndsWith("N") = False Then
                                                e.SetAttributeValue("checked", Chr(34) & "False" & Chr(34))
                                            End If
                                        ElseIf VarValue = "0" Then
                                            If id.ToUpper().EndsWith("N") Then
                                                e.SetAttributeValue("checked", Chr(34) & "True" & Chr(34))
                                            ElseIf id.ToUpper().EndsWith("S") = True Then
                                                e.SetAttributeValue("checked", Chr(34) & "False" & Chr(34))
                                            End If
                                        ElseIf VarValue = "1" Then
                                            If id.ToUpper().EndsWith("S") Then
                                                e.SetAttributeValue("checked", Chr(34) & "True" & Chr(34))
                                            ElseIf id.ToUpper().EndsWith("N") = False Then
                                                e.SetAttributeValue("checked", Chr(34) & "False" & Chr(34))
                                            End If
                                        Else
                                            If id.ToUpper().EndsWith("S") Then
                                                e.SetAttributeValue("checked", Chr(34) & "False" & Chr(34))

                                            ElseIf id.ToUpper().EndsWith("N") = False Then
                                                e.SetAttributeValue("checked", Chr(34) & "False" & Chr(34))
                                            End If
                                        End If
                                    Case "select-one"
                                        If VarValue = Nothing Then
                                            e.SetAttributeValue("value", "")
                                        Else
                                            e.SetAttributeValue("value", VarValue)

                                        End If
                                End Select
                            Case "select"
                                e.SetAttributeValue("value", VarValue + "<option value=" & Chr(34) & VarValue & Chr(34) & "selected=" & Chr(34) & "selected" & Chr(34) & ">" & VarValue & "</option>")

                            Case "textarea"
                                If VarValue = Nothing Then
                                    e.SetAttributeValue("value", "")
                                Else
                                    e.SetAttributeValue("value", VarValue)
                                End If
                        End Select
                        htmlElement.LoadHtml(e.OuterHtml)

                        strhtml = strhtml.Replace(oldTag, e.OuterHtml)


                    Catch ex As System.Runtime.InteropServices.COMException
                        Zamba.Core.ZClass.raiseerror(ex)
                    Catch ex As Exception
                        Zamba.Core.ZClass.raiseerror(ex)

                    End Try


                End If

            End If


        Next

        Return strhtml
    End Function

End Class
