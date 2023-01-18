
Imports System.IO

Public Class OfficeDocumentInfo
    Inherits ZClass
    Implements IOfficeDocumentInfo

#Region " Atributos "
    Private _docTypeId As Int64
    Private _docId As Int64
    Private _doc_ver As Integer
    Private _parentver As Int64
    Private _revise As Int16
    Private _isInZamba As String = String.Empty
    Private _fullPath As String = String.Empty
#End Region

#Region " Propiedades "
    Public Property DoctypeId() As Int32 Implements IOfficeDocumentInfo.DoctypeId
        Get
            Return _doctypeId
        End Get
        Set(ByVal value As Int32)
            _doctypeId = value
        End Set
    End Property
    Public Property Doc_ver() As Integer Implements IOfficeDocumentInfo.Doc_ver
        Get
            Return _doc_ver
        End Get
        Set(ByVal value As Integer)
            _doc_ver = value
        End Set
    End Property
    Public Property DocId() As Int64 Implements IOfficeDocumentInfo.DocId
        Get
            Return _docId
        End Get
        Set(ByVal value As Int64)
            _docId = value
        End Set
    End Property
    Public Property Parentver() As Int64 Implements IOfficeDocumentInfo.Parentver
        Get
            Return _parentver
        End Get
        Set(ByVal value As Int64)
            _parentver = value
        End Set
    End Property
    Public Property Isinzamba() As String Implements IOfficeDocumentInfo.Isinzamba
        Get
            Return _isInZamba
        End Get
        Set(ByVal value As String)
            _isInZamba = value
        End Set
    End Property
    Public Property Revise() As Int16 Implements IOfficeDocumentInfo.Revise
        Get
            Return _revise
        End Get
        Set(ByVal value As Int16)
            _revise = value
        End Set
    End Property
    Public Property FullPath() As String Implements IOfficeDocumentInfo.FullPath
        Get
            Return _fullPath
        End Get
        Set(ByVal value As String)
            _fullPath = value
        End Set
    End Property
#End Region

#Region "Constructores"
    Public Sub New(ByVal Res As Result)
        DoctypeId = Convert.ToInt32(Res.Parent.ID)
        _docId = Res.ID
        _doc_ver = Res.VersionNumber
        Parentver = Res.ParentVerId
        Revise = 0
        _isInZamba = "1"
        FullPath = Res.FullPath
        Dim Dir As IO.DirectoryInfo
        Try
            Dir = New IO.DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Zamba Software\temp")
            If Dir.Exists = False Then
                Dir.Create()
            End If
        Catch
            Dir = New IO.DirectoryInfo(System.Windows.Forms.Application.StartupPath & "\temp")
            If Dir.Exists = False Then
                Dir.Create()
            End If
        End Try
        Write(Dir.FullName & "\" & Res.Doc_File & ".ini")
    End Sub

    Public Sub New(ByVal filename As String)
        Try
            Dim fi As New StreamReader(filename)

            _doctypeId = CInt(fi.ReadLine())
            _docId = CInt(fi.ReadLine())
            _doc_ver = CInt(fi.ReadLine())
            Parentver = CInt(fi.ReadLine())
            Revise = Convert.ToInt16((fi.ReadLine()))
            _isInZamba = fi.ReadLine
            fullPath = fi.ReadLine

            fi.Close()
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub
#End Region

    Private Sub Write(ByVal filename As String)
        Dim w As New StreamWriter(filename, False)
        w.WriteLine(DoctypeId)
        w.WriteLine(_docId)
        w.WriteLine(_doc_ver)
        w.WriteLine(Parentver)
        w.WriteLine(Revise)
        w.WriteLine(_isInZamba)
        w.WriteLine(fullPath)
        w.Close()
    End Sub
    Public Overrides Sub Dispose()

    End Sub
End Class