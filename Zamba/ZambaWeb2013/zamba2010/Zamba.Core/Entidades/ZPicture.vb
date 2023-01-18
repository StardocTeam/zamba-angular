Imports System.Drawing
Imports System.IO

<Serializable()> Public Class ZPicture
    Implements IZPicture

#Region " Atributos "
    Private _resV As Decimal 'L
    Private _image As Image
    Private _size As New Size 'L
    Private _resolution As Decimal 'L
    Private _resH As Decimal 'L
    'Dim _ratio As Decimal 'L
#End Region

#Region " Propiedades "
    Public Property Size() As Size Implements IZPicture.Size
        Get
            Return _size
        End Get
        Set(ByVal value As Size)
            _size = value
        End Set
    End Property
    Public Property Image() As Image Implements IZPicture.Image
        Get
            Return _image
        End Get
        Set(ByVal value As Image)
            _image = value
        End Set
    End Property
    Property SizeWidth() As Integer Implements IZPicture.SizeWidth
        Get
            Return _size.Width
        End Get
        Set(ByVal value As Integer)
            _size.Width = value
        End Set
    End Property
    Property SizeHeight() As Integer Implements IZPicture.SizeHeight
        Get
            Return _size.Height
        End Get
        Set(ByVal value As Integer)
            _size.Height = value
        End Set
    End Property
    Public Property Resolution() As Decimal Implements IZPicture.Resolution
        Get
            Return _resolution
        End Get
        Set(ByVal value As Decimal)
            _resolution = value
        End Set
    End Property
    Public Property ResH() As Decimal Implements IZPicture.ResH
        Get
            Return _resH
        End Get
        Set(ByVal value As Decimal)
            _resH = value
        End Set
    End Property
    Public Property ResV() As Decimal Implements IZPicture.ResV
        Get
            Return _resV
        End Get
        Set(ByVal value As Decimal)
            _resV = value
        End Set
    End Property
#End Region

#Region " Constructores "
    Private Sub New()
    End Sub
    Public Sub New(ByVal File As String)
        Me.New()
        Try
            If (File.Length > 0 AndAlso File.Contains(".") AndAlso IO.File.Exists(File)) Then
                Image = Drawing.Image.FromFile(File)
            End If
        Catch ex As Exception
            Try
                Image = New Bitmap(File)
            Catch
                Dim fs As FileStream = IO.File.OpenRead(File)
                Image = Drawing.Image.FromStream(fs)
            End Try
        End Try

        If Image IsNot Nothing Then
            _size.Width = Me.Image.Width
            _size.Height = Me.Image.Height
            ResH = Convert.ToDecimal(Me.Image.HorizontalResolution)
            ResV = Convert.ToDecimal(Me.Image.VerticalResolution)
            Resolution = 100
            Dim Size As Size = adjustImage(Resolution, Me.Size.Height, ResV, Me.Size.Width, ResH)
            Me.Size = Size
            ResV = 100
            ResH = 100
        End If
    End Sub
#End Region

    Public Sub AdjustImageRes() Implements IZPicture.AdjustImageRes
        Me._size.Width = Me.Image.Width
        Me._size.Height = Me.Image.Height
        ResH = Convert.ToDecimal(Me.Image.HorizontalResolution)
        ResV = Convert.ToDecimal(Me.Image.VerticalResolution)
        Resolution = 100
        Dim Size As Size = adjustImage(Resolution, Me.Size.Height, ResV, Me.Size.Width, ResH)
        Me.Size = Size
        ResV = 100
        ResH = 100
    End Sub

#Region "AdjustImage"
    'Funciones para ajustar el tamaño de las imagenes
    Public Function adjustImage(ByVal Resolution As Decimal, ByVal Height As Decimal, ByVal resV As Decimal, ByVal Width As Decimal, ByVal resH As Decimal) As Size Implements IZPicture.adjustImage
        Dim H As Decimal
        Dim w As Decimal
        If resV = 0 Then
            H = Resolution * Height
        Else
            H = (Resolution * Height) / resV
        End If
        If resH = 0 Then
            w = Resolution * Width
        Else
            w = (Resolution * Width) / resH
        End If
        Return New Size(Convert.ToInt32(w), Convert.ToInt32(H))
    End Function

    Public Function AdjustImageToScreenWidth(ByVal ScreenWidth As Decimal, ByVal Resolution As Decimal, ByVal Width As Decimal) As Decimal Implements IZPicture.AdjustImageToScreenWidth
        Return (ScreenWidth * Resolution) / Width
    End Function

    Public Function AdjustImageToScreenHeight(ByVal ScreenHeight As Decimal, ByVal Resolution As Decimal, ByVal Height As Decimal) As Decimal Implements IZPicture.AdjustImageToScreenHeight
        Return (ScreenHeight * Resolution) / Height
    End Function
    '    Public Shared Function AdjustImageToScreenWidth(ByVal ScreenWidth As Decimal, ByVal Resolution As Decimal, ByVal Width As Decimal) As Decimal Implements IZPicture.AdjustImageToScreenWidth
    '        Return (ScreenWidth * Resolution) / Width
    '    End Function

    '    Public Shared Function AdjustImageToScreenHeight(ByVal ScreenHeight As Decimal, ByVal Resolution As Decimal, ByVal Height As Decimal) As Decimal Implements IZPicture.AdjustImageToScreenHeight
    '        Return (ScreenHeight * Resolution) / Height
    '    End Function
#End Region
End Class
