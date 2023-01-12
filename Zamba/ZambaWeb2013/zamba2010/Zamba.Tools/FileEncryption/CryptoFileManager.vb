Imports System
Imports System.Security.Cryptography
Imports System.IO

Public Enum CryptoEventType
    Message
    FileProgress
End Enum

Public Class CryptoEventArgs
    Inherits EventArgs
    Private m_type As CryptoEventType
    Private m_message As String
    Private m_fileLength As Integer
    Private m_filePosition As Integer

    Public Sub New(ByVal message As String)
        m_type = CryptoEventType.Message
        Me.m_message = message
    End Sub

    Public Sub New(ByVal fileName As String, ByVal fileLength As Integer, ByVal filePosition As Integer)
        m_type = CryptoEventType.FileProgress
        Me.m_fileLength = fileLength
        Me.m_filePosition = filePosition
        m_message = fileName
    End Sub

    Public ReadOnly Property Type() As CryptoEventType
        Get
            Return m_type
        End Get
    End Property

    Public ReadOnly Property Message() As String
        Get
            Return m_message
        End Get
    End Property

    Public ReadOnly Property FileName() As String
        Get
            Return m_message
        End Get
    End Property

    Public ReadOnly Property FileLength() As Integer
        Get
            Return m_fileLength
        End Get
    End Property

    Public ReadOnly Property FilePosition() As Integer
        Get
            Return m_filePosition
        End Get
    End Property
End Class

Public Delegate Sub cryptoEventHandler(ByVal sender As Object, ByVal e As CryptoEventArgs)

Public Class CryptoFileManager
    Private testHeader As Byte() = Nothing
    'used to verify if decryption succeeded 
    Private testHeaderString As String = Nothing

    Public Sub New()
        testHeader = System.Text.Encoding.ASCII.GetBytes("testing header")
        testHeaderString = BitConverter.ToString(testHeader)
    End Sub

#Region "RijnDael_key encrypt"

    Public Sub getKeysFromPassword(ByVal pass As String, ByRef rijnKey As Byte(), ByRef rijnIV As Byte())
        Dim salt As Byte() = System.Text.Encoding.ASCII.GetBytes("System.Text.Encoding.ASCII.GetBytes")
        Dim pb As New PasswordDeriveBytes(pass, salt)
        rijnKey = pb.GetBytes(32)
        rijnIV = pb.GetBytes(16)
    End Sub

#End Region

#Region "RijnDael encrypt"
    Const bufLen As Integer = 4096

    Public Sub EncryptData(ByVal inName As [String], ByVal outName As [String], ByVal rijnKey As Byte(), ByVal rijnIV As Byte())
        Dim fin As FileStream = Nothing
        Dim fout As FileStream = Nothing
        Dim encStream As CryptoStream = Nothing
        Try
            'Create the file streams to handle the input and output files.
            fin = New FileStream(inName, FileMode.Open, FileAccess.Read)
            fout = New FileStream(outName, FileMode.Create, FileAccess.Write)
            'Create variables to help with read and write.
            Dim bin As Byte() = New Byte(bufLen - 1) {}
            'This is intermediate storage for the encryption.
            Dim rdlen As Long = 0
            'This is the total number of bytes written.
            Dim totlen As Long = fin.Length
            'This is the total length of the input file.
            Dim len As Integer
            'This is the number of bytes to be written at a time.
            Dim rijn As New RijndaelManaged()

            encStream = New CryptoStream(fout, rijn.CreateEncryptor(rijnKey, rijnIV), CryptoStreamMode.Write)

            'zakoduj testowy fragment
            encStream.Write(testHeader, 0, testHeader.Length)

            'Read from the input file, then encrypt and write to the output file.
            While True
                len = fin.Read(bin, 0, bufLen)
                If len = 0 Then
                    Exit While
                End If
                encStream.Write(bin, 0, len)
                rdlen += len
            End While
        Finally
            If encStream IsNot Nothing Then
                encStream.Close()
            End If
            If fout IsNot Nothing Then
                fout.Close()
            End If
            If fin IsNot Nothing Then
                fin.Close()
            End If
        End Try
    End Sub

#End Region

#Region "RijnDael decrypt"

    Public Function DecryptData(ByVal inName As [String], ByVal outName As [String], ByVal rijnKey As Byte(), ByVal rijnIV As Byte()) As Boolean
        'Create the file streams to handle the input and output files.
        Dim fin As FileStream = Nothing
        Dim fout As FileStream = Nothing
        Dim decStream As CryptoStream = Nothing
        Try
            fin = New FileStream(inName, FileMode.Open, FileAccess.Read)
            'Create variables to help with read and write.
            Dim bin As Byte() = New Byte(bufLen - 1) {}
            'This is intermediate storage for the encryption.
            Dim rdlen As Long = 0
            'This is the total number of bytes written.
            Dim totlen As Long = fin.Length
            'This is the total length of the input file.
            Dim len As Integer
            'This is the number of bytes to be written at a time.
            Dim rijn As New RijndaelManaged()
            'DES ds = new DESCryptoServiceProvider();
            decStream = New CryptoStream(fin, rijn.CreateDecryptor(rijnKey, rijnIV), CryptoStreamMode.Read)
            'odkoduj testowy fragment
            Dim test As Byte() = New Byte(testHeader.Length - 1) {}
            decStream.Read(test, 0, testHeader.Length)
            If BitConverter.ToString(test) <> testHeaderString Then
                decStream.Clear()
                decStream = Nothing
                Return False
            End If

            'create output file
            fout = New FileStream(outName, FileMode.Create, FileAccess.Write)

            'Read from the encrypted file and write dercypted data
            While True
                len = decStream.Read(bin, 0, bufLen)

                If len = 0 Then
                    Exit While
                End If

                fout.Write(bin, 0, len)
                rdlen += len
            End While
            Return True
        Catch ex As Exception
            Dim s = ex.ToString()
        Finally

            If decStream IsNot Nothing Then
                decStream.Close()
            End If

            If fout IsNot Nothing Then
                fout.Close()
            End If

            If fin IsNot Nothing Then
                fin.Close()
            End If
        End Try
    End Function

#End Region
End Class