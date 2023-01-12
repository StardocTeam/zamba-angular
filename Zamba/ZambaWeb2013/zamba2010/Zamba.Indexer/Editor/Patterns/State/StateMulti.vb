Friend Class StateMulti
    Inherits AbstractState

    Public Sub New(ByVal contex As AbstractPersistImage)
        MyBase.New(contex)
        parameter = New Imaging.EncoderParameter(Imaging.Encoder.SaveFlag, Imaging.EncoderValue.MultiFrame)
    End Sub

    Friend Overrides Sub Save(ByVal img As System.Drawing.Image)
        m_contex.m_ActualCount = m_contex.m_ActualCount + 1
        '-----------
        'Save
        m_contex.m_image = DirectCast(img.Clone, Image)

        Try
            m_strategy = New StrategyCompress(Me)
            m_contex.m_image.Save(m_contex.PathDestino, m_contex.Codec, DirectCast(m_strategy.getParameters(), Imaging.EncoderParameters))
        Catch
            Try
                m_strategy = New StrategyNormal(Me)
                m_contex.m_image.Save(m_contex.PathDestino, m_contex.Codec, DirectCast(m_strategy.getParameters(), Imaging.EncoderParameters))
            Catch ex As Exception
                Throw New ArgumentException(ex.Message)
            End Try
        End Try
        '----
        m_contex.m_State = New StateAdd(m_contex)
    End Sub
End Class