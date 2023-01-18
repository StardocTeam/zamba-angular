Friend MustInherit Class AbstractState
    Inherits AbstractImage

    Friend parameter As Imaging.EncoderParameter

    Friend m_contex As AbstractPersistImage

    Friend Shared m_strategy As AbstractStrategy

    Public Sub New(ByVal contex As AbstractPersistImage)
        m_contex = contex
    End Sub

    Friend MustOverride Overloads Overrides Sub Save(ByVal img As System.Drawing.Image)
    Friend Overridable Overloads Sub Save()
    End Sub
End Class
