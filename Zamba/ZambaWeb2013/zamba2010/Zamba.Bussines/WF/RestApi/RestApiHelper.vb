Imports System.Net
Imports System.Text
Imports System.IO
Imports System.Collections.Generic
Imports Newtonsoft.Json
Imports Zamba.Framework

Public Class RestApiHelper
    Public Function test(url As String, Method As String, JsonMessage As String) As String
        Return ExecuteService(url, Method, JsonMessage)
    End Function
    Public Function ExecuteService(url As String, Method As String, JsonMessage As String) As String



        If JsonMessage.ToLower().Contains("zamba.genericrequest.params") Then

            Dim jsonMessageParams As String() = JsonMessage.Split(";")
            Dim GR As New genericRequest
            GR.UserId = Membership.MembershipHelper.CurrentUser.ID

            For Each j As String In jsonMessageParams

                If (j.Trim <> String.Empty) Then
                    j = j.Replace("zamba.genericrequest.params(", "").Trim()
                    j = j.Substring(0, j.Length - 1).Trim()

                    Dim key As String = j.Split(",")(0)
                    Dim value As String = j.Split(",")(1)

                    If value.ToLower().Contains("zamba.table") Then
                        Dim VarInterReglas As New VariablesInterReglas()
                        Dim DataTableJson As String = JsonConvert.SerializeObject(VarInterReglas.ReconocerVariablesAsObject(value.Replace("zamba.table", "zvar")), Formatting.Indented)
                        value = DataTableJson
                        VarInterReglas = Nothing
                    End If

                    If value.Contains("zvar") Then
                        Dim VarInterReglas As New VariablesInterReglas()
                        value = VarInterReglas.ReconocerVariables(value)
                        VarInterReglas = Nothing
                    End If

                    GR.Params.Add(key, value)
                End If
            Next

            JsonMessage = JsonConvert.SerializeObject(GR, Formatting.Indented)

        Else
            If JsonMessage.Contains("zamba.table") Then
                Dim VarInterReglas As New VariablesInterReglas()

                Dim DataTableJson As String = JsonConvert.SerializeObject(VarInterReglas.ReconocerVariablesAsObject(JsonMessage.Replace("zamba.table", "zvar")), Formatting.Indented)

                JsonMessage = DataTableJson
                VarInterReglas = Nothing
            End If

            If JsonMessage.Contains("zvar") Then
                Dim VarInterReglas As New VariablesInterReglas()
                JsonMessage = VarInterReglas.ReconocerVariables(JsonMessage)
                VarInterReglas = Nothing
            End If

            JsonMessage = JsonConvert.SerializeObject(JsonMessage, Formatting.Indented)
        End If
        Dim WebRequest As HttpWebRequest
        WebRequest = CType(WebRequest.Create(New Uri(url)), HttpWebRequest)
        WebRequest.Timeout = 700000
        Dim response As String
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Llamada a rest api:" & vbCrLf & url & vbCrLf & Method & vbCrLf & JsonMessage)
        WebRequest.Accept = "application/json"
        WebRequest.ContentType = "application/json"
        WebRequest.Method = Method
        If Method = "POST" Then
            Dim encoding As New ASCIIEncoding()
            Dim bytes() As Byte = encoding.GetBytes(JsonMessage)
            Dim NewStream As Stream = WebRequest.GetRequestStream()
            NewStream.Write(bytes, 0, bytes.Length)
            NewStream.Close()
        End If
        Dim HttpResponse As WebResponse
        Dim ObjStream As Stream
        HttpResponse = WebRequest.GetResponse()
        ObjStream = HttpResponse.GetResponseStream()
        Dim ObjStreamReader As StreamReader = New StreamReader(ObjStream)
        Dim ContenidoRespuesta As String = ObjStreamReader.ReadToEnd
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Respuesta rest api:" & ContenidoRespuesta)
        Return ContenidoRespuesta
    End Function
End Class


'Public Class genericRequest
'    Public Property UserId As Long
'    Public Property Params As Dictionary(Of String, String) = New Dictionary(Of String, String)
'End Class


