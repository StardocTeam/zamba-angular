
Public Interface IZPicture
    Property Image() As System.Drawing.Image
    Property Size() As System.Drawing.Size
    Property SizeWidth() As Integer
    Property SizeHeight() As Integer
    Property Resolution() As Decimal
    Property ResH() As Decimal
    Property ResV() As Decimal
    Sub AdjustImageRes()

#Region "AdjustImage"
    'Funciones para ajustar el tamaño de las imagenes
    Function adjustImage(ByVal Resolution As Decimal, ByVal Height As Decimal, ByVal resV As Decimal, ByVal Width As Decimal, ByVal resH As Decimal) As System.Drawing.Size
    Function AdjustImageToScreenWidth(ByVal ScreenWidth As Decimal, ByVal Resolution As Decimal, ByVal Width As Decimal) As Decimal
    Function AdjustImageToScreenHeight(ByVal ScreenHeight As Decimal, ByVal Resolution As Decimal, ByVal Height As Decimal) As Decimal
#End Region
End Interface
