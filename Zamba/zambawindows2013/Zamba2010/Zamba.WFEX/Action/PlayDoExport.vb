Imports Zamba.Core
Imports System.IO
Public Class PlayDoExport

    Private myRule As IDoExport
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        Try
            Dim Line As System.Text.StringBuilder
            Dim sw As StreamWriter = Nothing
            Dim count As Int32 = 1
            For Each r As TaskResult In results
                 'Segun parametro, Si son varios results se permite crear un versionado del documento asignandole un numero correlativo al nombre
                ' o sino se crea un unico archivo con los atributos de todos los results

                If myrule.VersionsExportedDocuments Then
                    sw = New StreamWriter(myrule.documentPath & "\" & myrule.documentName & "(" & count & ")" & ".txt", False)
                Else
                    If count = 1 Then
                        sw = New StreamWriter(myrule.documentPath & "\" & myrule.documentName & ".txt", True)
                    End If
                End If

                Line = New System.Text.StringBuilder

                For Each Item As String In myrule.resultLine.Split(myrule.separator.ToCharArray)
                    If String.IsNullOrEmpty(Item) = False Then

                        If Trim(Item).StartsWith("<<") Then
                            'es texto inteligente
                            Line.Append(Zamba.Core.TextoInteligente.ReconocerCodigo(Item, r))
                        Else
                            'Buscar el valor del indice
                            For Each _index As Index In r.Indexs
                                If Trim(_index.Name) = Trim(Item) Then
                                    If String.IsNullOrEmpty(_index.Data) = False Then
                                        Line.Append(_index.Data)
                                    Else
                                        Line.Append(_index.DataTemp)
                                    End If
                                    Exit For
                                End If
                            Next
                        End If

                        Line.Append(myrule.separator)
                    End If
                    count += 1
                Next
                Trace.WriteLineIf(ZTrace.IsInfo, "Exportando documento...")
                sw.WriteLine(Line.ToString)
                If myrule.VersionsExportedDocuments Then
                    sw.Close()
                End If
                Trace.WriteLineIf(ZTrace.IsInfo, "Exportación realizada con éxito!")
            Next

            If myrule.VersionsExportedDocuments = False Then
                sw.Close()
            End If
        Finally

        End Try
        Return results
    End Function
    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub New(ByVal rule As IDoExport)
        Me.myRule = rule
    End Sub
End Class
