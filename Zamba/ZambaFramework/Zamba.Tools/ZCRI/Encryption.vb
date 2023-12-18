Imports System
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography
Imports Zamba
''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Tools
''' Class	 : Tools.Encryption
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' SEGURIDAD: Clase para encriptar datos
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	26/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class Encryption

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Recibe una cadena y la devuelve encriptada
    ''' </summary>
    ''' <param name="sIn">Cadena a encriptar</param>
    ''' <param name="sKey">Conjunto de claves</param>
    ''' <param name="iv">conjunto de valores</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function EncryptString(ByVal sIn As String, ByVal sKey As Byte(), ByVal iv As Byte()) As String
        Dim result As String
        Dim DES As New System.Security.Cryptography.RijndaelManaged

        If sKey.GetUpperBound(0) <> 15 Or iv.GetUpperBound(0) <> 15 Then
            Throw New Exception("Error en el vector o en la clave")
        End If

        Try

            ' Set the cipher mode.
            DES.Mode = CipherMode.ECB
            Dim DESEncrypt As ICryptoTransform

            ' Create the encryptor.
            DESEncrypt = DES.CreateEncryptor(sKey, iv)

            ' Get a byte array of the string.
            Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(sIn)

            ' Transform and return the string.
            result = Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length))

        Catch ex As Exception
            Throw (New Exception("Error en la Encryptacion: " & ex.ToString))
        End Try

        Return result
    End Function

    ''' <summary>
    ''' Recibe una cadena y la devuelve encriptada
    ''' </summary>
    ''' <param name="sIn">Cadena a encriptar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function EncryptString(ByVal sIn As String) As String
        Dim key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Dim vector As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Return EncryptString(sIn, key, vector)
    End Function

    ''' <summary>
    ''' Recibe una cadena y la devuelve encriptada
    ''' </summary>
    ''' <param name="sIn">Cadena a encriptar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function EncryptNewString(ByVal sIn As String) As String
        Dim key As Byte() = {0, 2, 3, 6, 5, 4, 7, 8, 9, 1, 1, 2, 3, 6, 5, 4}
        Dim vector As Byte() = {0, 2, 3, 6, 5, 4, 7, 8, 9, 1, 1, 2, 3, 6, 5, 4}
        Return EncryptString(sIn, key, vector)
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Recibe una cadena encriptada y la desencripta
    ''' </summary>
    ''' <param name="sOut">Cadena a desencriptar</param>
    ''' <param name="sKey">Conjunto de claves</param>
    ''' <param name="iv">conjunto de valores</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function DecryptString(ByVal sOut As String, ByVal sKey As Byte(), ByVal IV As Byte()) As String
        'Servicio de encriptaci�n
        Dim DES As New System.Security.Cryptography.RijndaelManaged
        'Verifico la clave y el vector

        If sKey.GetUpperBound(0) <> 15 Or IV.GetUpperBound(0) <> 15 Then
            Throw New ArgumentOutOfRangeException("Iv o Key")
        End If

        'Variable a donde se pone el resultado
        Dim Result As String

        DES.Mode = CipherMode.ECB
        Dim DESDecrypt As ICryptoTransform = DES.CreateDecryptor(sKey, IV)
        Try
            Dim Buffer As Byte() = Convert.FromBase64String(sOut)
            If Buffer.Length > 0 Then
                Result = (System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length)))
            Else
                Result = String.Empty
            End If
        Catch ex As Exception
            Result = String.Empty
        End Try

        Return Result
    End Function

    ''' <summary>
    ''' Recibe una cadena encriptada y la desencripta
    ''' </summary>
    ''' <param name="sOut">Cadena a desencriptar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DecryptString(ByVal sOut As String) As String
        Dim key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Dim vector As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Return DecryptString(sOut, key, vector)
    End Function

    ''' <summary>
    ''' Recibe una cadena encriptada y la desencripta
    ''' </summary>
    ''' <param name="sOut">Cadena a desencriptar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DecryptNewString(ByVal sOut As String) As String
        Dim key As Byte() = {0, 2, 3, 6, 5, 4, 7, 8, 9, 1, 1, 2, 3, 6, 5, 4}
        Dim vector As Byte() = {0, 2, 3, 6, 5, 4, 7, 8, 9, 1, 1, 2, 3, 6, 5, 4}
        Return DecryptString(sOut, key, vector)
    End Function
End Class