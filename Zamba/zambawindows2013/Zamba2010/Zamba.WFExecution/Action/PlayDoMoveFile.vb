Imports System.Windows.Forms
Imports System.IO
Imports System.Text
Public Class PlayDoMoveFile
    Private myRule As IDoMoveFile

    Sub New(ByVal rule As IDoMoveFile)
        Me.myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim inputRoute, outputRoute, fileName As String
        Dim inputPath, OutputPath As String
        Try

            If myRule.InputRoute.ToLower.Contains("zvar") = True Then
                inputRoute = Trim(WFRuleParent.ObtenerValorVariableObjeto(myRule.InputRoute))
            Else
                inputRoute = myRule.InputRoute
            End If
            Trace.WriteLine("Ruta Origen: " & inputRoute)

            If myRule.FileName.ToLower.Contains("zvar") = True Then
                fileName = Trim(WFRuleParent.ObtenerValorVariableObjeto(myRule.FileName))
            Else
                fileName = myRule.FileName
            End If
            Trace.WriteLine("Archivo a Mover: " & fileName)

            If myRule.OutputRoute.ToLower.Contains("zvar") = True Then
                outputRoute = Trim(WFRuleParent.ObtenerValorVariableObjeto(myRule.OutputRoute))
            Else
                outputRoute = myRule.OutputRoute
            End If
            Trace.WriteLine("Ruta Destino: " & outputRoute)

            inputPath = IO.Path.Combine(inputRoute, fileName)
            OutputPath = IO.Path.Combine(outputRoute, fileName)

            If Not IO.Directory.Exists(outputRoute) Then
                IO.Directory.CreateDirectory(outputRoute)
            End If

            'si existe lo elimino
            If IO.File.Exists(OutputPath) Then
                IO.File.Delete(OutputPath)
            End If

            IO.File.Copy(inputPath, OutputPath, True)
            IO.File.Delete(inputPath)

            If File.Exists(inputPath) Then
                Trace.WriteLine("Ocurrió un error al eliminar el archivo")
            Else
                Trace.WriteLine("El archivo se movió correctamente")
            End If

        Catch ex As Exception
            raiseerror(ex)
        Finally
            inputRoute = Nothing
            outputRoute = Nothing
            fileName = Nothing
            inputPath = Nothing
        End Try
        Return results
    End Function
End Class
