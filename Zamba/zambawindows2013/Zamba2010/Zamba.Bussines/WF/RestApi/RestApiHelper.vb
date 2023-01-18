Imports System.Net
Imports System.Text
Imports System.IO
Public Class RestApiHelper
    Public Function test(url As String, Method As String, JsonMessage As String) As String
        Try
            Return ExecuteService(url, Method, JsonMessage)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
    Public Function ExecuteService(url As String, Method As String, JsonMessage As String) As String
        Dim WebRequest As HttpWebRequest
        WebRequest = CType(WebRequest.Create(New Uri(url)), HttpWebRequest)
        WebRequest.Timeout = 700000
        Dim response As String
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Llamada a rest api:" & vbCrLf & url & vbCrLf & Method & vbCrLf & JsonMessage)
        WebRequest.Accept = "application/json"
            WebRequest.ContentType = "application/json"
            WebRequest.Method = Method
            If JsonMessage <> "" Then
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





