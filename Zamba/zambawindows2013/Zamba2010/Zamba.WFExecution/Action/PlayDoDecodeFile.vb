﻿Imports System.IO

Public Class PlayDoDecodeFile

    Private _myRule As IDoDecodeFile

    Sub New(ByVal rule As IDoDecodeFile)
        _myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        For Each r As ITaskResult In results
            Dim strbinary As String = TextoInteligente.ReconocerCodigo(_myRule.binary, r)
            Dim VarInterReglas As New VariablesInterReglas()
            Dim binary As Object = WFRuleParent.ReconocerVariablesAsObject(strbinary)

            'Se obtiene la ruta
            Dim path As String = TextoInteligente.ReconocerCodigo(_myRule.path, r)
            path = WFRuleParent.ReconocerVariables(path).Trim
            VarInterReglas = Nothing
            If Not Directory.Exists(path) Then
                Directory.CreateDirectory(path)
            End If
            Dim filePathnew As String = path + "\" + _myRule.fname + " " + DateTime.Now.ToString("dd-MM-yy HH-mm-ss") + _myRule.extfile
            filePathnew = FileBusiness.GetUniqueFileName(filePathnew)

            If TypeOf (binary) Is Byte() Then
                'Decodificacion del archivo
                Decode(filePathnew, DirectCast(binary, Byte()))

            Else
                strbinary = binary.ToString.TrimEnd

                'Trunca el comienzo no necesario
                Dim textstart As String = TextoInteligente.ReconocerCodigo(_myRule.textstart, r)
                textstart = WFRuleParent.ReconocerVariables(textstart).TrimEnd
                Dim separator As String() = {textstart}
                If Not String.IsNullOrEmpty(textstart) Then
                    strbinary = strbinary.Split(separator, StringSplitOptions.None)(1)
                End If

                'Trunca el final no necesario
                Dim textend As String = TextoInteligente.ReconocerCodigo(_myRule.textend, r)
                textend = WFRuleParent.ReconocerVariables(textend).TrimEnd
                Dim separator2 As String() = {textend}
                If Not String.IsNullOrEmpty(textend) Then
                    strbinary = strbinary.Split(separator2, StringSplitOptions.None)(0)
                End If

                'Decodificacion del archivo
                Decode(filePathnew, strbinary)
            End If

            If VariablesInterReglas.ContainsKey(_myRule.varpath) = Nothing Then
                VariablesInterReglas.Add(_myRule.varpath, filePathnew, False)
            Else
                VariablesInterReglas.Item(_myRule.varpath) = filePathnew
            End If
        Next
        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

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