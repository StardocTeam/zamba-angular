Imports System.IO
Public Class PlayDoUnZip
    Private _myRule As IDoUnZip
    Sub New(ByVal rule As IDoUnZip)
        _myRule = rule
    End Sub
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        Try

            _myRule.files = WFRuleParent.ReconocerVariablesValuesSoloTexto(_myRule.files)
            _myRule.files = TextoInteligente.ReconocerCodigo(_myRule.files, results(0))

            _myRule.nameNewFile = WFRuleParent.ReconocerVariablesValuesSoloTexto(_myRule.nameNewFile)
            _myRule.nameNewFile = TextoInteligente.ReconocerCodigo(_myRule.nameNewFile, results(0))

            _myRule.nameVar = WFRuleParent.ReconocerVariablesValuesSoloTexto(_myRule.nameVar)
            _myRule.nameVar = TextoInteligente.ReconocerCodigo(_myRule.nameVar, results(0))

            Dim myFile As DirectoryInfo = New DirectoryInfo(_myRule.files)
            Dim zipFile As Ionic.Zip.ZipFile

            If myFile.Exists Then

                Dim nameZip As String = Membership.MembershipHelper.AppTempPath & "\temp\" & _myRule.nameNewFile


                Dim myFiles As FileInfo() = myFile.GetFiles

                For Each archivo As FileInfo In myFiles
                    If archivo.Exists Then
                        zipFile.AddFile(archivo.FullName)
                    End If
                Next

                If Not VariablesInterReglas.ContainsKey(_myRule.nameVar) Then
                    VariablesInterReglas.Add(_myRule.nameVar, zipFile, False)
                Else
                    VariablesInterReglas.Item(_myRule.nameVar) = zipFile
                End If
            End If

        Catch ex As Exception
            Throw New ZambaEx("Ocurrió un error al insertar el usuario a la base de datos.", ex)
        End Try
        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function

    Function DiscoverParams() As List(Of String)

    End Function
End Class
