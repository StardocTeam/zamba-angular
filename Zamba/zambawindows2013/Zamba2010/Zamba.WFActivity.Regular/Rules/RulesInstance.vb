Imports Zamba.Core
Imports System.Reflection
Imports System.Windows.Forms
Imports System.IO

Public Class RulesInstance

    Private RulesAssembly As Assembly
    Public Function GetWFActivityRegularAssembly() As Assembly
        Try
            If RulesAssembly Is Nothing Then
                ZTrace.WriteLineIf(ZTrace.IsError, "Directorio de Aplicacion: " & Application.StartupPath)
                Dim RulesDirectory As String = New FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName
                Try
                    ZTrace.WriteLineIf(ZTrace.IsError, "Rule de Assembly de Reglas: " & RulesDirectory & "\Zamba.WFActivity.Regular.dll")
                    RulesAssembly = Assembly.LoadFile(RulesDirectory & "\Zamba.WFActivity.Regular.dll")
                Catch ex As Exception
                    'Get the assembly information
                    Dim assemblyInfo As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
                    'Location is where the assembly is run from 
                    'Dim assemblyLocation As String = New IO.FileInfo(assemblyInfo.Location).Directory.FullName

                    'CodeBase is the location of the ClickOnce deployment files
                    Dim uriCodeBase As Uri = New Uri(assemblyInfo.CodeBase)
                    Dim ClickOnceLocation As String = Path.GetDirectoryName(uriCodeBase.LocalPath.ToString())

                    ZTrace.WriteLineIf(ZTrace.IsError, "Directorio de Aplicacion ClickOnce: " & ClickOnceLocation & "\Zamba.WFActivity.Regular.dll")
                    RulesAssembly = Assembly.LoadFile(ClickOnceLocation & "\Zamba.WFActivity.Regular.dll")
                End Try
            End If
        Catch ex As Exception
            Return Nothing
        End Try
        Return RulesAssembly
    End Function

End Class
