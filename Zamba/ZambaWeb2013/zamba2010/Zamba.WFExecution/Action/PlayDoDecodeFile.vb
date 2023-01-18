Imports Zamba.Core
Imports System.IO

Public Class PlayDoDecodeFile

    Private _myRule As IDoDecodeFile

    Sub New(ByVal rule As IDoDecodeFile)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        For Each r As ITaskResult In results
            Dim strbinary As String = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.binary, r)
            Dim VarInterReglas As New VariablesInterReglas()
            Dim binary As Object = VarInterReglas.ReconocerVariablesAsObject(strbinary)

            'Se obtiene la ruta
            Dim path As String = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.path, r)
            path = VarInterReglas.ReconocerVariables(path).Trim

            If path.Length = 0 Then
                path = Membership.MembershipHelper.AppTempPath & "\temp"
            End If
             
            If Not IO.Directory.Exists(path) Then
                IO.Directory.CreateDirectory(path)
            End If
            Dim filePathnew As String = path + "\" + Me._myRule.fname + " " + DateTime.Now.ToString("dd-MM-yy HH-mm-ss") + Me._myRule.extfile
            filePathnew = FileBusiness.GetUniqueFileName(filePathnew)

            If TypeOf (binary) Is Byte() Then
                'Decodificacion del archivo
                Decode(filePathnew, DirectCast(binary, Byte()))

            Else
                strbinary = binary.ToString.TrimEnd

                'Trunca el comienzo no necesario
                Dim textstart As String = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.textstart, r)
                textstart = VarInterReglas.ReconocerVariables(textstart).TrimEnd
                Dim separator As String() = {textstart}
                If Not String.IsNullOrEmpty(textstart) Then
                    strbinary = strbinary.Split(separator, StringSplitOptions.None)(1)
                End If

                'Trunca el final no necesario
                Dim textend As String = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.textend, r)
                textend = VarInterReglas.ReconocerVariables(textend).TrimEnd
                Dim separator2 As String() = {textend}
                If Not String.IsNullOrEmpty(textend) Then
                    strbinary = strbinary.Split(separator2, StringSplitOptions.None)(0)
                End If

                'Decodificacion del archivo
                Decode(filePathnew, strbinary)
            End If

            VarInterReglas = Nothing

            If VariablesInterReglas.ContainsKey(Me._myRule.varpath) = Nothing Then
                VariablesInterReglas.Add(Me._myRule.varpath, filePathnew)
            Else
                VariablesInterReglas.Item(Me._myRule.varpath) = filePathnew
            End If

            dim file As new FileInfo(filePathnew)

            If VariablesInterReglas.ContainsKey("DecodedFileName") = Nothing Then
                VariablesInterReglas.Add("DecodedFileName", file.Name)
            Else
                VariablesInterReglas.Item("DecodedFileName") = file.Name
            End If


            If VariablesInterReglas.ContainsKey("DecodedFullPath") = Nothing Then
                VariablesInterReglas.Add("DecodedFullPath", file.DirectoryName)
            Else
                VariablesInterReglas.Item("DecodedFullPath") = file.DirectoryName
            End If

            dim WebUrl = Membership.MembershipHelper.AppUrl 

            If VariablesInterReglas.ContainsKey("WebUrl") = Nothing Then
                VariablesInterReglas.Add("WebUrl", WebUrl)
            Else
                VariablesInterReglas.Item("WebUrl") = WebUrl
            End If
        Next
        Return results
    End Function

    ''' <summary>
    ''' Decodifica una cadena de texto a un binario y luego crea el archivo
    ''' </summary>
    ''' <param name="txtOutFile">Ruta del archivo a crear</param>
    ''' <param name="txtEncoded">Cadena de texto que contiene el binario en base 64</param>
    ''' <remarks></remarks>
    Private Sub Decode(ByVal txtOutFile As String, ByVal txtEncoded As String)
        If Not String.IsNullOrEmpty(txtOutFile) Then
            Using fs As New FileStream(txtOutFile, FileMode.Create, FileAccess.Write, FileShare.None)
                Try
                    Dim filebytes As Byte() = Convert.FromBase64String(txtEncoded)
                    fs.Write(filebytes, 0, filebytes.Length)
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                Finally
                    If fs IsNot Nothing Then
                        fs.Close()
                    End If
                End Try
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Decodifica un binario y crea el archivo
    ''' </summary>
    ''' <param name="txtOutFile">Ruta del archivo a crear</param>
    ''' <param name="binary">Binario a generar el archivo</param>
    ''' <remarks></remarks>
    Private Sub Decode(ByVal txtOutFile As String, ByVal binary As Byte())
        If Not String.IsNullOrEmpty(txtOutFile) Then
            Using fs As New FileStream(txtOutFile, FileMode.Create, FileAccess.Write, FileShare.None)
                Try
                    fs.Write(binary, 0, binary.Length)
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                Finally
                    If fs IsNot Nothing Then
                        fs.Close()
                    End If
                End Try
            End Using
        End If
    End Sub

End Class
