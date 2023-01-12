Imports System.Security.Cryptography
Imports System.IO
Imports Zamba

Public Class EncryptTransformer

    Private byteDESKey As Byte()
    Private byteDESIV() As Byte
    Private _key As String
    Private _IVKey As String

    Public Property Key() As String
        Get
            Return _key
        End Get
        Set(ByVal Value As String)
            _key = Value
        End Set
    End Property

    Public Property IVKey() As String
        Get
            Return _IVKey
        End Get
        Set(ByVal Value As String)
            _IVKey = Value
        End Set
    End Property

    Public Enum CryptoAction
        actionEncrypt = 1
        actionDecrypt = 2
    End Enum

    Public Sub InitializeKey()
        byteDESKey = GetKeyByteArray(_key)
        byteDESIV = GetKeyByteArray(_IVKey)
    End Sub

    Private Shared Function GetKeyByteArray(ByVal sPassword As String) As Byte()
        Dim byteTemp(7) As Byte
        sPassword = sPassword.PadRight(8)   ' make sure we have 8 chars

        Dim iCharIndex As Integer
        For iCharIndex = 0 To 7
            'byteTemp(iCharIndex) = Asc(Mid$(sPassword, iCharIndex + 1, 1))
            byteTemp(iCharIndex) = Byte.Parse(Asc(Mid$(sPassword, iCharIndex + 1, 1)).ToString())
        Next

        Return byteTemp
    End Function

    Public Sub EncryptOrDecryptFile(ByVal sInputFile As String, ByVal sOutputFile As String, ByVal Direction As CryptoAction)

        'Create the file streams to handle the input and output files.
        Dim fsInput As New FileStream(sInputFile, FileMode.Open, FileAccess.Read)
        Dim fsOutput As New FileStream(sOutputFile, FileMode.OpenOrCreate, FileAccess.Write)
        fsOutput.SetLength(0)

        'Variables needed during encrypt/decrypt process
        Dim byteBuffer(4096) As Byte 'holds a block of bytes for processing
        Dim nBytesProcessed As Long = 0 'running count of bytes encrypted
        Dim nFileLength As Long = fsInput.Length
        Dim iBytesInCurrentBlock As Integer
        Dim desProvider As New DESCryptoServiceProvider
        Dim csMyCryptoStream As CryptoStream = Nothing

        ' Set up for encryption or decryption
        Select Case Direction
            Case CryptoAction.actionEncrypt
                csMyCryptoStream = New CryptoStream(fsOutput, _
                   desProvider.CreateEncryptor(byteDESKey, byteDESIV), _
                   CryptoStreamMode.Write)
            Case CryptoAction.actionDecrypt
                csMyCryptoStream = New CryptoStream(fsOutput, _
                   desProvider.CreateDecryptor(byteDESKey, byteDESIV), _
                   CryptoStreamMode.Write)
        End Select

        'Read from the input file, then encrypt or decrypt
        'and write to the output file.
        While nBytesProcessed < nFileLength
            iBytesInCurrentBlock = fsInput.Read(byteBuffer, 0, 4096)
            csMyCryptoStream.Write(byteBuffer, 0, iBytesInCurrentBlock)
            nBytesProcessed = nBytesProcessed + CLng(iBytesInCurrentBlock)
        End While

        csMyCryptoStream.Close()
        fsInput.Close()
        fsOutput.Close()
    End Sub

End Class
