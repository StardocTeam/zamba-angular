
Friend MustInherit Class AbstractPersistImage
    Inherits AbstractImage

    Friend m_image As Image
    Friend m_CountImg As Int32
    Friend m_ActualCount As Int32

    'Contexto
    Friend Shared m_Helper As ImageHelper
    'Estado
    Friend m_State As AbstractState

    Friend MustOverride Overrides Sub Save(ByVal img As System.Drawing.Image)

    Friend ReadOnly Property PathDestino() As String
        Get
            Return m_Helper.PathDestino
        End Get
    End Property

    Friend ReadOnly Property Codec() As Imaging.ImageCodecInfo
        Get
            Return m_Helper.Codec
        End Get
    End Property

End Class






