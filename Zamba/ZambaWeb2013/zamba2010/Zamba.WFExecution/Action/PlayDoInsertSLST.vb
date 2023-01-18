Public Class PlayDoInsertSLST

    Private _myRule As IDoInsertSLST
    Private value As String
    Private compareValue As String
    Private ifsql As String
    Private sqlaux As String
    Private sqlaux2 As String
    Private strselect As String
    Private strline As String
    Private Ds As DataSet
    Private valueexec As Object

    Public Shared Event AddedTask(ByVal Results As Generic.List(Of ITaskResult), ByVal OpenTaskAfterInsert As Boolean)


    Public Sub New(ByVal rule As IDoInsertSLST)
        _myRule = rule
        Ds = New DataSet
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim VarInterReglas As New VariablesInterReglas()
        Dim code, description, indexID As String
        code = _myRule.Code.ToString
        description = _myRule.Description.ToString
        indexID = _myRule.IDSLST.ToString
        Try
            Dim ASB As New AutoSubstitutionBusiness
            For Each t As Core.TaskResult In results
                code = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.Code, t).Trim
                description = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.Description, t).Trim
                indexID = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.IDSLST, t).Trim

                If code.Contains("zvar") = True Then
                    code = VarInterReglas.ReconocerVariablesValuesSoloTexto(code)
                End If
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Codigo = " + code)

                If description.Contains("zvar") = True Then
                    description = VarInterReglas.ReconocerVariablesValuesSoloTexto(description)
                End If

                If description.Contains("zvar") = True Then
                    indexID = VarInterReglas.ReconocerVariablesValuesSoloTexto(indexID)
                End If

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Descripcion = " + description)
                ASB.InsertIndexSust(indexID, code.ToString, description.ToString)

            Next
            ASB = Nothing

        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            VarInterReglas = Nothing
        End Try

        Return results
    End Function
End Class
