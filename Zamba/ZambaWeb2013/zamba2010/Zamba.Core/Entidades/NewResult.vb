Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

'Imports Zamba.Volumes

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Core
''' Class	 : Core.NewResult
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase para crear objetos NewResult. Hereda de objetos Result
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	26/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------

<Serializable()> Public Class NewResult
    Inherits Result
    Implements INewResult
#Region "Constructores"
    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(ByVal File As String)
        MyBase.New(File)
    End Sub
#End Region

#Region "Atributos"
    Private _state As States = States.Indexar
    Private _ready As Boolean
    Private _newFile As String
    Private _volume As IVolume = Nothing
    Private _dsVols As DataSet = Nothing
    Private _volumeListId As Int32
    Private _flagCopyVerify As Boolean
    Private _comment As String
    Private _encodedFile As Byte()
    Private _fileLength As Decimal
#End Region

#Region "Propiedades"
    Public Property DsVols() As DataSet Implements INewResult.DsVols
        Get
            If IsNothing(_dsVols) Then CallForceLoad(Me)
            If IsNothing(_dsVols) Then _dsVols = New DataSet()

            Return _dsVols
        End Get
        Set(ByVal value As DataSet)
            _dsVols = value
        End Set
    End Property
    Public Property State() As States Implements INewResult.State
        Get
            Return _state
        End Get
        Set(ByVal value As States)
            _state = value
        End Set
    End Property
    Public Property FlagCopyVerify() As Boolean Implements INewResult.FlagCopyVerify
        Get
            Return _flagCopyVerify
        End Get
        Set(ByVal value As Boolean)
            _flagCopyVerify = value
        End Set
    End Property
    Public Property Ready() As Boolean Implements INewResult.Ready
        Get
            Return _ready
        End Get
        Set(ByVal value As Boolean)
            _ready = value
        End Set
    End Property
    Public Property NewFile() As String Implements INewResult.NewFile
        Get
            Return _newFile
        End Get
        Set(ByVal value As String)
            _newFile = value
        End Set
    End Property
    Public Property Volume() As IVolume Implements INewResult.Volume
        Get
            If IsNothing(_volume) Then CallForceLoad(Me)
            If IsNothing(_volume) Then _volume = New Volume()
            Return _volume
        End Get
        Set(ByVal value As IVolume)
            _volume = value
        End Set
    End Property
    Public Property VolumeListId() As Int32 Implements INewResult.VolumeListId
        Get
            Return _volumeListId
        End Get
        Set(ByVal value As Int32)
            _volumeListId = value
        End Set
    End Property
    Public Property Comment() As String Implements INewResult.Comment
        Get
            Return _comment
        End Get
        Set(ByVal value As String)
            _comment = value
        End Set
    End Property
    Public Property EncodedFile() As Byte() Implements INewResult.EncodedFile
        Get
            Return _encodedFile
        End Get
        Set(ByVal value As Byte())
            _encodedFile = value
        End Set
    End Property
    Public Property FileLength() As Decimal Implements INewResult.FileLength
        Get
            If ISVIRTUAL = False Then
                If _fileLength = 0 Then
                    Try
                        _fileLength = CDec(New IO.FileInfo(NewFile).Length / 1024)
                    Catch ex As Exception
                        _fileLength = CDec(70 / 1024)
                        ZClass.raiseerror(ex)
                    End Try
                End If
            End If

            Return _fileLength
        End Get
        Set(ByVal value As Decimal)
            _fileLength = value
        End Set
    End Property
#End Region

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene la cantidad de atributos de un tipo determidado que contiene
    ''' </summary>
    ''' <param name="indextypeId">Tipo de indice enumerado</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetIndex(ByVal indextypeId As Integer) As Integer Implements INewResult.GetIndex
        Dim count As Integer = 0
        If Not Me.Indexs Is Nothing Then
            For Each i As Index In Me.Indexs
                If i.ID = indextypeId Then
                    Return count
                Else
                    count += 1
                End If
            Next
        End If
        Return -1
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve la extensión del archivo
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' 	[Diego]	13/11/2007	Modified - Se agrego un finally para liberar el archivo
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public ReadOnly Property Extension() As String Implements INewResult.Extension
        Get
            Dim fileData As FileInfo = Nothing
            Try
                fileData = New FileInfo(Me.File)
                Return fileData.Extension
            Catch
                Return String.Empty
            Finally
                fileData = Nothing
            End Try
        End Get
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve la extensión del archivo
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' 	[Diego]	13/11/2007	Modified - Se agrego un finally para liberar el archivo
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public ReadOnly Property FileName() As String Implements INewResult.FileName
        Get
            Dim fileData As FileInfo = Nothing
            Try
                fileData = New FileInfo(Me.File)
                Return fileData.Name
            Catch
                Return String.Empty
            Finally
                fileData = Nothing
            End Try
        End Get
    End Property
    'options

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Serializa el objeto NewResult en un Archivo
    ''' </summary>
    ''' <param name="filename">Nombre del archivo donde se va a serializar el objeto NewResult</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub FileSerialize(ByVal filename As String) Implements INewResult.FileSerialize
        Dim stream As Stream = IO.File.Open(filename, FileMode.Create)
        Dim bf As New BinaryFormatter
        Try
            bf.Serialize(stream, Me)
            stream.Close()
        Catch ex As Exception
            stream.Close()
            IO.File.Delete(filename)
        End Try
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un NewResult en base al archivo donde fue serializado el mismo
    ''' </summary>
    ''' <param name="filename">Nombre del archivo que contiene un Newresult serializado</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetResultFromFile(ByVal filename As String) As NewResult
        Dim res As NewResult
        Dim file As Stream = IO.File.Open(filename, FileMode.Open)
        Dim bf As New BinaryFormatter
        res = CType(bf.Deserialize(file), NewResult)
        file.Close()
        Return res
    End Function

    
End Class