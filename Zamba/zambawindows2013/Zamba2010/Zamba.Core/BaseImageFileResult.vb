Imports System.IO

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

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve la extensi�n del archivo
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' 	[Diego]	13/11/2007	Modified - Se agrego un finally para liberar el archivo
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public ReadOnly Property FileName() As String Implements IBaseImageFileResult.FileName
        Get
            Dim fileData As FileInfo = Nothing
            Try
                If Not IsNothing(File) Then
                    fileData = New FileInfo(File)
                    Return fileData.Name
                End If
            Catch
                Return String.Empty
            Finally
                fileData = Nothing
            End Try
        End Get
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
#End Region

    Private _disposed As Boolean
    Public Overrides Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)

        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        'Para evitar que se haga dispose 2 veces
        If Not _disposed Then
            If disposing Then

            End If

            ' Indicates that the instance has been disposed.
            _disposed = True
        End If
    End Sub

    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub


End Class
