Friend Class PersistImageImpl
    Inherits AbstractPersistImage

    Public Sub New(ByVal helper As ImageHelper)
        m_Helper = helper

        m_CountImg = m_Helper.PicArray.Count

        m_State = New StateSingle(Me)
    End Sub

    Friend Overrides Sub Save(ByVal img As Image)
        m_State.Save(img)
        m_Helper.m_img = m_image
    End Sub

End Class