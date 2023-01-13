Imports Zamba.Core

Public Class PlayDoShowBinary

    Private _myRule As IDoShowBinary

    Sub New(ByVal rule As IDoShowBinary)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByVal myRule As IDoShowBinary) As System.Collections.Generic.List(Of ITaskResult)
        Return PlayWeb(results, Nothing)
    End Function

    Public Function PlayWeb(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal Params As Hashtable) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim VarInterReglas As New VariablesInterReglas()
        Dim binaryFile As Object = Nothing
        Dim binaryText As String

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo el binario")

        Try
            'Se obtiene el binario
            binaryText = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.binaryFile, results(0))
            binaryFile = VarInterReglas.ReconocerVariablesAsObject(binaryText)

            If TypeOf (binaryFile) Is Byte() Then
                'Se obtiene el mime
                binaryText = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.mimeType, results(0))
                binaryText = VarInterReglas.ReconocerVariables(binaryText).Trim

                'Se guardan los parametros
                Params.Add("BinaryFile", binaryFile)
                Params.Add("BinaryMime", binaryText)

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Binario guardado en memoria correctamente")
            Else
                ZTrace.WriteLineIf(ZTrace.IsError, "El objeto obtenido no es del formato correcto.")
                Throw New ArgumentException("El valor obtenido de " & _myRule.binaryFile & " es incorrecto.")
            End If

        Finally
            VarInterReglas = Nothing
            binaryFile = Nothing
            binaryText = Nothing
        End Try

        Return results
    End Function
End Class
