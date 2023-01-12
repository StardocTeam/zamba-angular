Imports System.Diagnostics

' copyright 2004 - Adiall (e-mail: vb.net@adiall.cjb.net)
''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Tools
''' Class	 : Tools.INIClass
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase para trabajar con archivos INI
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	26/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class INIClass
    Private Declare Function GetPrivateProfileSection Lib "kernel32" Alias "GetPrivateProfileSectionA" (ByVal lpAppName As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Private Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Private Declare Function WritePrivateProfileSection Lib "kernel32" Alias "WritePrivateProfileSectionA" (ByVal lpAppName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
    Private Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
    'reads ini string
    Public Shared Function ReadIni(ByVal Filename As String, ByVal Section As String, ByVal Key As String, Optional ByVal optValue As String = "") As String
        Dim RetVal As String, v As Integer

        Try
            If Not IO.File.Exists(Filename) Then
                Trace.WriteLine("No se encuentra el archivo: " & Filename)
                Return String.Empty

            End If

            RetVal = " ".PadRight(255, Char.Parse(" "))
            v = GetPrivateProfileString(Section, Key, String.Empty, RetVal, RetVal.Length, Filename)
            Dim Result As String = Left(RetVal, v)
            If Result = "" Then
                Return String.Empty
            Else
                Return Result
            End If
        Catch
            Return String.Empty
        End Try
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Lee la seccion especifica de un archivo INI
    ''' </summary>
    ''' <param name="Filename">Nombre del archivo INI</param>
    ''' <param name="Section">Nombre de la seccion que se desea leer</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function ReadIniSection(ByVal Filename As String, ByVal Section As String) As String
        Dim RetVal As String = String.Empty
        Dim v As Integer
        Try
            RetVal = " ".PadRight(255, Char.Parse(" "))
            v = GetPrivateProfileSection(Section, RetVal, RetVal.Length, Filename)
            ReadIniSection = Left(RetVal, v)
        Catch ex As Exception
            Return String.Empty
        End Try
        Return String.Empty
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Escribe en un archivo INI, el valor para una seccion especifica
    ''' </summary>
    ''' <param name="Filename">Nombre del archivo INI que se desea escribir, si no existe lo crea</param>
    ''' <param name="Section">Nombre de la seccion del archivo que se desea escribir, si no existe, la crea</param>
    ''' <param name="Key">Clave dentro de la seccion que se desea escribir</param>
    ''' <param name="Value">Valor que se desea guardar</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub WriteIni(ByVal Filename As String, ByVal Section As String, ByVal Key As String, ByVal Value As String)
        WritePrivateProfileString(Section, Key, Value, Filename)
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Escribe una seccion en un archivo INI
    ''' </summary>
    ''' <param name="Filename">Nombre del archivo INI que se desea escribir, si no existe lo crea</param>
    ''' <param name="Section">Nombre de la seccion del archivo que se desea escribir, si no existe, la crea</param>
    ''' <param name="Value">Valor que se desea guardar</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub WriteIniSection(ByVal Filename As String, ByVal Section As String, ByVal Value As String)
        WritePrivateProfileSection(Section, Value, Filename)
    End Sub

End Class