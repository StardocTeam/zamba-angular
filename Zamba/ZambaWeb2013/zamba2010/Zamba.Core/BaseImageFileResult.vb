Imports System.IO
Imports System.Web

<Serializable()> Public Class BaseImageFileResult
    Inherits ZambaCore
    Implements IBaseImageFileResult

#Region "Atributos"
    Private _originalName As String
    Private _createdate As Date
    Private _Disk_Group_Id As Int32
    Private _Doc_File As String
    Private _file As String
    Private _OffSet As Int32
    Private _DISK_VOL_PATH As String
    Private _editdate As Date
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private Const _BARRA As String = "\"
    Private _encodedFile As Byte()
    Public _mimeType As String

    Public Sub New()

    End Sub

    Public Sub New(ByVal Id As Int64, ByVal Name As String)
        Me.New()
        Me.ID = Id
        Me.Name = Name
    End Sub


#End Region

#Region "Propiedades"
    Public Property Disk_Group_Id() As Int32 Implements IBaseImageFileResult.Disk_Group_Id
        Get
            Return _Disk_Group_Id
        End Get
        Set(ByVal Value As Int32)
            _Disk_Group_Id = Value
        End Set
    End Property
    Public Property Doc_File() As String Implements IBaseImageFileResult.Doc_File
        Get
            If IsNothing(_Doc_File) Then Return Nothing
            Return _Doc_File.Trim
        End Get
        Set(ByVal Value As String)
            _Doc_File = Value
        End Set
    End Property
    Public Property OffSet() As Int32 Implements IBaseImageFileResult.OffSet
        Get
            Return _OffSet
        End Get
        Set(ByVal Value As Int32)
            _OffSet = Value
        End Set
    End Property
    Public Property DISK_VOL_PATH() As String Implements IBaseImageFileResult.DISK_VOL_PATH
        Get
            Return _DISK_VOL_PATH
        End Get
        Set(ByVal Value As String)
            _DISK_VOL_PATH = Value
        End Set
    End Property
    Public Property File() As String Implements IBaseImageFileResult.File
        Get
            Return _file
        End Get
        Set(ByVal Value As String)
            _file = Value.Trim
        End Set
    End Property
    Public Function RealFullPath() As String Implements IFileResult.RealFullPath
        If Disk_Group_Id = -1 Then
            Return Doc_File
        ElseIf Disk_Group_Id = 0 Then
            Return File
        ElseIf Disk_Group_Id = -2 Then
            Return Doc_File
        ElseIf Disk_Group_Id = -3 Then
            Return OriginalName
        ElseIf Doc_File Is Nothing Then
            Return File
        Else
            Return DISK_VOL_PATH & _BARRA & Parent.ID & _BARRA & OffSet & _BARRA & Doc_File
        End If
    End Function

    <PropiedadesType(Propiedades.PropiedadPublica)>
    Public ReadOnly Property FullPath() As String Implements IFileResult.FullPath
        Get
            If Disk_Group_Id = -1 Then
                Return Doc_File
            ElseIf Disk_Group_Id = 0 Then
                Return File
            ElseIf Disk_Group_Id = -2 Then
                Return Doc_File.Replace(Path.GetExtension(Doc_File), Path.GetExtension(OriginalName))
            ElseIf Disk_Group_Id = -3 Then
                Return OriginalName
            ElseIf Doc_File Is Nothing Then
                Return File
            Else
                Return DISK_VOL_PATH & _BARRA & Parent.ID & _BARRA & OffSet & _BARRA & Doc_File
            End If
        End Get
    End Property

    Public Property CreateDate() As DateTime Implements IFileResult.CreateDate
        Get
            Return _createdate
        End Get
        Set(ByVal value As DateTime)
            _createdate = value
        End Set
    End Property
    Public Property EditDate() As Date Implements IFileResult.EditDate
        Get
            Return _editdate
        End Get
        Set(ByVal value As Date)
            _editdate = value
        End Set
    End Property
    Public Property OriginalName() As String Implements IFileResult.OriginalName
        Get
            Return _originalName
        End Get
        Set(ByVal value As String)
            _originalName = value
        End Set
    End Property
    Public Overrides ReadOnly Property IsFull() As Boolean
        Get
            Return _isFull
        End Get
    End Property
    Public Overrides ReadOnly Property IsLoaded() As Boolean
        Get
            Return _isLoaded
        End Get
    End Property
    Public Property EncodedFile() As Byte() Implements IBaseImageFileResult.EncodedFile
        Get
            Return _encodedFile
        End Get
        Set(ByVal value As Byte())
            _encodedFile = value
        End Set
    End Property
    Public ReadOnly Property MimeType() As String Implements IBaseImageFileResult.MimeType
        Get
            If String.IsNullOrEmpty(_mimeType) Then
                _mimeType = GetMimeType()
            End If
            Return _mimeType
        End Get
    End Property
#End Region

    Private Function GetMimeType() As String

        Dim mimeType As String = "application/unknown"

        If String.IsNullOrEmpty(Me.FullPath) OrElse IO.File.Exists(Me.FullPath) = False Then
            If (Me.Doc_File IsNot Nothing AndAlso Me.Doc_File.Length > 0 AndAlso Me.Doc_File.Contains(".") AndAlso IO.File.Exists(Me.Doc_File)) Then
                mimeType = MimeMapping.GetMimeMapping(Me.Doc_File)
            End If
        Else
            If (Me.FullPath.Length > 0 AndAlso Me.FullPath.Contains(".") AndAlso IO.File.Exists(Me.FullPath)) Then
                mimeType = MimeMapping.GetMimeMapping(Me.FullPath)
            End If
        End If

        'If fI IsNot Nothing Then
        '    Dim regKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(fI.Extension.ToLower())

        '    If regKey IsNot Nothing Then
        '        Dim contentType As Object = regKey.GetValue("Content Type")

        '        If contentType IsNot Nothing Then
        '            mimeType = contentType.ToString()
        '        End If
        '    End If
        'End If

        Return mimeType
    End Function

    Public Overrides Sub Dispose()

    End Sub

    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub


End Class
