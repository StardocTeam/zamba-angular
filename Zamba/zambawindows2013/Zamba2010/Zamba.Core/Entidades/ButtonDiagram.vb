Public Class ButtonDiagram
    Inherits ZambaCore
    Implements IButtonDiagram
#Region " Atributos "
    Private _iconId As Integer
    Private _placeId As Integer
    Private _wfId As Integer
    Private _buttonID As String
    Private _isFull As Boolean
    Private _isLoaded As Boolean
#End Region
#Region " Propiedades "
    Public Property ButtonID As String Implements IButtonDiagram.ButtonID
        Get
            Return _buttonID
        End Get
        Set(value As String)
            _buttonID = value
        End Set
    End Property
    Public Property IconId As Integer Implements IButtonDiagram.IconId
        Get
            Return _iconId
        End Get
        Set(value As Integer)
            _iconId = value
        End Set
    End Property

    Public Property PlaceId As Integer Implements IButtonDiagram.PlaceId
        Get
            Return _placeId
        End Get
        Set(value As Integer)
            _placeId = value
        End Set
    End Property

    Public Property WFID As Integer Implements IButtonDiagram.WFID
        Get
            Return _wfId
        End Get
        Set(value As Integer)
            _wfId = value
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
    End Sub
#End Region

    Public Overrides Sub Dispose()

    End Sub

    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
End Class
