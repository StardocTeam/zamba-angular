Public Class Pic
    Inherits PictureBox

    Public OriginalImage As Image
    '    Public Page As Int32
    '   Public Dimension As System.Drawing.Imaging.FrameDimension

    Public Sub New()
        MyBase.New()
        Me.SizeMode = PictureBoxSizeMode.StretchImage
        Me.BorderStyle = Windows.Forms.BorderStyle.None
    End Sub
End Class
