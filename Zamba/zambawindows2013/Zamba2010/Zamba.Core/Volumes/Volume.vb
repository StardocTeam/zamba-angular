
'Imports Zamba.Volumes.Core
<Serializable()> Public Class Volume
    Inherits ZBaseCore
    Implements IVolume

#Region " Atributos "
    Private _Id As Int32
    Private _name As String = String.Empty
    Private _size As Integer
    Private _type As VolumeTypes
    Private _files As Long
    Private _copy As Integer
    Private _path As String = String.Empty
    Private _sizelen As Decimal
    Private _state As Integer
    Private _offset As Integer
    Private _volumestate As VolumeStates = VolumeStates.VolumenEnPreparacion
    Private _isFull As Boolean
    Private _isLoaded As Boolean
#End Region

#Region " Propiedades "
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Representa el tamaño total del volumen
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property Size() As Integer Implements IVolume.Size
        Get
            Return _size
        End Get
        Set(ByVal Value As Integer)
            _size = Value
        End Set
    End Property
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Representa los tipos de medios donde se asienta el volumen
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' Disco Rigido, Cinta Magnetica, Disco Optico, etc.
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property Type() As VolumeTypes Implements IVolume.Type
        Get
            Return _type
        End Get
        Set(ByVal Value As VolumeTypes)
            _type = Value
        End Set
    End Property
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Representa la cantidad de archivos que tiene el Volumen
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property Files() As Long Implements IVolume.Files
        Get
            Return _files
        End Get
        Set(ByVal Value As Long)
            _files = Value
        End Set
    End Property
    Public Property copy() As Integer Implements IVolume.copy
        Get
            Return _copy
        End Get
        Set(ByVal Value As Integer)
            _copy = Value
        End Set
    End Property
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Representa la ruta donde se encuentra el Volumen
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property path() As String Implements IVolume.path
        Get
            Return _path
        End Get
        Set(ByVal Value As String)
            _path = Value
        End Set
    End Property
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Representa el tamaño en Megabytes que ocupan los archivos en el Volumen
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property sizelen() As Decimal Implements IVolume.sizelen
        Get
            Return _sizelen
        End Get
        Set(ByVal Value As Decimal)
            _sizelen = Value
        End Set
    End Property
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Representa si el estado del volumen es Disponible o No Disponible
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property state() As Integer Implements IVolume.state
        Get
            Return _state
        End Get
        Set(ByVal Value As Integer)
            _state = Value
        End Set
    End Property
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Representa una Subcarpeta dentro del path de Volumen
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' Estas subcarpetas son 20
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property offset() As Integer Implements IVolume.offset
        Get
            Return _offset
        End Get
        Set(ByVal Value As Integer)
            _offset = Value
        End Set
    End Property
    Public Property VolumeState() As VolumeStates Implements IVolume.VolumeState
        Get
            Return _volumestate
        End Get
        Set(ByVal value As VolumeStates)
            _volumestate = value
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

#Region " Constructores "
    Public Sub New()
        MyBase.New()
    End Sub
#End Region
  
    Public Overrides Sub FullLoad()
        CallForceLoad(Me)
    End Sub
    Public Overrides Sub Load()
        CallForceLoad(Me)
    End Sub
    Public Overrides Sub Dispose()

    End Sub
End Class