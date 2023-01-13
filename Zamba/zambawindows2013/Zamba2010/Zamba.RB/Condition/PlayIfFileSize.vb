Public Class PlayIfFileSize
    Dim _rule As IIfFileSize
    Private myRule As IIfFileSize

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return results
    End Function
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
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

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub New(ByVal rule As IIfFileSize)
        Me.myRule = rule
    End Sub
End Class
