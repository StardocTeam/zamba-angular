Public Class Pic
    Inherits PictureBox

    Public OriginalImage As Image
    '    Public Page As Int32
    '   Public Dimension As System.Drawing.Imaging.FrameDimension

    Public Sub New()
        MyBase.New()
        SizeMode = PictureBoxSizeMode.StretchImage
        BorderStyle = BorderStyle.None
    End Sub
End Class
