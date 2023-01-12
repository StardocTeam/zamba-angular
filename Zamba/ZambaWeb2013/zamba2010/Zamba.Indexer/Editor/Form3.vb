Friend Class Form3
    Inherits Zamba.AppBlock.ZForm

    Public Property Image() As Image
        Get
            Return Me.PictureBox1.Image
        End Get
        Set(ByVal value As Image)
            Me.PictureBox1.Image = value
        End Set
    End Property
End Class