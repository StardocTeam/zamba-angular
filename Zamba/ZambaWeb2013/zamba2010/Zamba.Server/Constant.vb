Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.IO

Public Class Constant
    ' Nested Types
    Public Class SQLCon
        ' Methods
     
        Private Shared Function ReadWebConfig(ByVal filename As String) As String
            '    Dim ConString As String = DirectCast(System.Web.Configuration.WebConfigurationManager.ConnectionStrings.Item("Zamba").ConnectionString, String)
            Dim WebFile As String = System.Web.Configuration.WebConfigurationManager.ConnectionStrings.Item("Zamba").ElementInformation.Source
            Dim fi As New IO.FileInfo(WebFile)
            Dim File As String = fi.Directory.FullName & "\" & filename
            Return File
        End Function

        Public Shared _DB_CONVERT_DATE_FORMAT As String
        Public Shared Function DB_CONVERT_DATE_FORMAT() As String
            Dim DB_CONVERT_DATE_FORMAT_ As String
            Try
                If _DB_CONVERT_DATE_FORMAT = String.Empty Then
                    Return "CONVERT(DATETIME, '{0}-{1}-{2} 00:00:00', 102)"
                End If
                DB_CONVERT_DATE_FORMAT_ = _DB_CONVERT_DATE_FORMAT
            Catch exception1 As Exception
                Dim ex As Exception = exception1
                DB_CONVERT_DATE_FORMAT_ = "CONVERT(DATETIME, '{0}-{1}-{2} 00:00:00', 102)"
                Return DB_CONVERT_DATE_FORMAT_
            End Try
            Return DB_CONVERT_DATE_FORMAT_
        End Function


        Public Shared _DB_CONVERT_DATE_TIME_FORMAT As String
        Public Shared Function DB_CONVERT_DATE_TIME_FORMAT() As String
            Try
                If _DB_CONVERT_DATE_TIME_FORMAT = String.Empty Then
                    Return "CONVERT(DATETIME, '{0}-{1}-{2} {3}:{4}:{5}', 102)"
                End If
                Return _DB_CONVERT_DATE_TIME_FORMAT
            Catch exception1 As Exception
                Dim ex As Exception = exception1
                Return "CONVERT(DATETIME, '{0}-{1}-{2} {3}:{4}:{5}', 102)"
            End Try
        End Function
    End Class
End Class

