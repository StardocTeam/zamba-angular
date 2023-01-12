Imports System
Imports System.IO
Imports System.IO.Compression
Imports System.Text

''' <summary>
''' Clase encargada de realizar la codificación y decodificación en base 64 de archivos.
''' También permite la compresión y decompresión.
''' </summary>
Public Class FileEncode


    ''' <summary>
    ''' Codifica un archivo en base 64
    ''' </summary>
    ''' <param name="txtINFilePath">Ruta del archivo físico a codificar</param>
    ''' <returns>Código en formato Byte()</returns>
    ''' <remarks></remarks>
    Public Shared Function Encode(ByVal txtINFilePath As String) As Byte()
        Try
            Dim filebytes As Byte()

            Using fs As New FileStream(txtINFilePath, FileMode.Open, FileAccess.Read)
                filebytes = New Byte(fs.Length - 1) {}

                fs.Read(filebytes, 0, Convert.ToInt32(fs.Length))
                fs.Close()
            End Using

            Return filebytes

        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsWarning, ex.Message)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Decodifica un archivo en base 64
    ''' </summary>
    ''' <param name="txtOutFile">Ruta del archivo a generar</param>
    ''' <param name="txtEncoded">Código en base 64 en formato Byte()</param>
    ''' <remarks></remarks>
    Public Shared Sub Decode(ByVal txtOutFile As String, ByVal filebytes As Byte())
        Try
            If Not String.IsNullOrEmpty(txtOutFile) Then
                Using fs As FileStream = New FileStream(txtOutFile, FileMode.Create, FileAccess.Write, FileShare.None)
                    fs.Write(filebytes, 0, filebytes.Length)
                    fs.Close()
                End Using
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Decodifica un archivo en base 64 - se crea el metodo para la carga de archivos en el dropzone
    ''' </summary>
    ''' <param name="txtOutFile">Ruta del archivo a generar</param>
    ''' <param name="txtEncoded">Código en base 64 en formato Byte()</param>
    ''' <remarks></remarks>
    Public Shared Sub DecodeN(ByVal txtOutFile As String, ByVal filebytes As Byte())
        Try
            'If Not String.IsNullOrEmpty(txtOutFile) Then
            Using fs As FileStream = New FileStream(txtOutFile, FileMode.Create, FileAccess.Write, FileShare.None)
                fs.Write(filebytes, 0, filebytes.Length)
                fs.Close()
            End Using
            'End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    'Ezequiel: Se comenta codigo de ZIP
    'Public Shared Function Zip(ByVal file As Byte()) As Byte()

    '    Dim buffer As Byte() = file

    '    Dim ms As New MemoryStream()

    '    Using zipFile As GZipStream = New System.IO.Compression.GZipStream(ms, CompressionMode.Compress, True)
    '        zipFile.Write(buffer, 0, buffer.Length)
    '    End Using

    '    ms.Position = 0
    '    Dim outStream As New MemoryStream()

    '    Dim compressed As Byte() = New Byte(ms.Length - 1) {}
    '    ms.Read(compressed, 0, compressed.Length)

    '    Dim gzBuffer As Byte() = New Byte(compressed.Length + 3) {}

    '    System.Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length)
    '    System.Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4)

    '    Return gzBuffer

    'End Function

    'Ezequiel: Se comenta codigo de ZIP
    'Public Shared Function UnZip(ByVal file As Byte()) As Byte()

    '    Dim gzBuffer As Byte() = file

    '    Using ms As New MemoryStream()
    '        Dim msgLength As Integer = BitConverter.ToInt32(gzBuffer, 0)
    '        ms.Write(gzBuffer, 4, gzBuffer.Length - 4)

    '        Dim buffer As Byte() = New Byte(msgLength - 1) {}
    '        ms.Position = 0

    '        Using zip As New GZipStream(ms, CompressionMode.Decompress)
    '            zip.Read(buffer, 0, buffer.Length)
    '        End Using

    '        Return buffer
    '    End Using

    'End Function


    'Public Shared Function Zip(ByVal text As String) As String

    '    Dim buffer As Byte() = Encoding.UTF8.GetBytes(text)
    '    Dim ms As New MemoryStream()
    '    Dim Zips As New GZipStream(ms, CompressionMode.Compress, True)
    '    Zips.Write(buffer, 0, buffer.Length)
    '    ms.Position = 0
    '    Dim outStream As New MemoryStream()
    '    Dim compressed As Byte() = New Byte(ms.Length - 1) {}
    '    ms.Read(compressed, 0, compressed.Length)
    '    Dim gzBuffer As Byte() = New Byte(compressed.Length + 3) {}
    '    System.Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length)
    '    System.Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4)
    '    Return Convert.ToBase64String(gzBuffer)
    'End Function

    'Public Shared Function UnZip(ByVal compressedText As String) As String
    '    Dim gzBuffer As Byte() = Convert.FromBase64String(compressedText)
    '    Using ms As New MemoryStream()
    '        Dim msgLength As Integer = BitConverter.ToInt32(gzBuffer, 0)
    '        ms.Write(gzBuffer, 4, gzBuffer.Length - 4)
    '        Dim buffer As Byte() = New Byte(msgLength - 1) {}
    '        ms.Position = 0

    '        Using zip As New GZipStream(ms, CompressionMode.Decompress)
    '            zip.Read(buffer, 0, buffer.Length)
    '        End Using
    '        Return Encoding.UTF8.GetString(buffer)
    '    End Using
    'End Function

End Class

