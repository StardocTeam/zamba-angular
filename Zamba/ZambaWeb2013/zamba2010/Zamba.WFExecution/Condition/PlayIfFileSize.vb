Public Class PlayIfFileSize
    Dim _rule As IIfFileSize

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myrule As IIfFileSize) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim WFRB As New WFRulesBusiness
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando la existencia de Reglas hijas")
        If (Me._rule.ChildRulesIds Is Nothing OrElse Me._rule.ChildRulesIds.Count = 0) Then
            Me._rule.ChildRulesIds = WFRB.GetChildRulesIds(Me._rule.ID)
        End If

        If myrule.ChildRulesIds.Count = 2 Then
            For Each childruleId As Int64 In Me._rule.ChildRulesIds
                Dim R As WFRuleParent = WFRB.GetInstanceRuleById(childruleId)
                R.ParentRule = myrule
                R.IsAsync = myrule.IsAsync
                If R.GetType().ToString().Contains("IfBranch") Then
                    Return results
                End If
            Next
        End If

        Me._rule = myrule
        If ValidarArchivo(myrule) Then
            Return results
        Else
            Return Nothing
        End If
        Return Nothing
    End Function
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myrule As IIfFileSize, ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
        Me._rule = myrule
        If ValidarArchivo(myrule) = ifType Then
            Return results
        Else
            Return Nothing
        End If
        Return Nothing
    End Function
    Private Function ValidarArchivo(ByRef myrule As IIfFileSize) As Boolean
        Dim Path As New System.IO.FileInfo(Me._rule.path)
        'Try
        Select Case Me._rule.Comparador
            Case Comparacion.Igual
                If Path.Length = CLng(Me._rule.num1) Then
                    Return True
                Else
                    Return False
                End If
            Case Comparacion.Distinto
                If Path.Length <> CLng(Me._rule.num1) Then
                    Return True
                Else
                    Return False
                End If
            Case Comparacion.Mayor
                If Path.Length > CLng(Me._rule.num1) Then
                    Return True
                Else
                    Return False
                End If
            Case Comparacion.Menor
                If Path.Length < CLng(Me._rule.num1) Then
                    Return True
                Else
                    Return False
                End If
            Case Comparacion.IgualMayor
                If Path.Length >= CLng(Me._rule.num1) Then
                    Return True
                Else
                    Return False
                End If
            Case Comparacion.IgualMenor
                If Path.Length <= CLng(Me._rule.num1) Then
                    Return True
                Else
                    Return False
                End If
            Case Comparacion.Entre
                If Path.Length > CLng(Me._rule.num1) And Path.Length < CLng(Me._rule.num2) Then
                    Return True
                Else
                    Return False
                End If
            Case Else
                Return False
        End Select
        'Catch ex As Exception
        '    Zamba.Core.RaiseError(ex)
        'End Try
    End Function
End Class
