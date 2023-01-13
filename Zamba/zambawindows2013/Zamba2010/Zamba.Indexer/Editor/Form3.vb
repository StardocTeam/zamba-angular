Friend Class Form3
    Inherits Zamba.AppBlock.ZForm

    Public Property Image() As Image
        Get
            Return PictureBox1.Image
        End Get
        Set(ByVal value As Image)
            PictureBox1.Image = value
        End Set
    End Property
End Class