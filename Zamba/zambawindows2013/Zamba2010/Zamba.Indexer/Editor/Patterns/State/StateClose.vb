Friend Class StateClose
    Inherits AbstractState

    Public Sub New(ByVal contex As AbstractPersistImage)
        MyBase.New(contex)
    End Sub

    Friend Overrides Sub Save(ByVal img As System.Drawing.Image)
        'Save Normal
        Try
            Try
                m_contex.m_image.SaveAdd(img, DirectCast(m_strategy.getParameters(), Imaging.EncoderParameters))
            Catch
                Try
                    m_strategy = New StrategyNormal(Me)
                    m_contex.m_image.SaveAdd(img, DirectCast(m_strategy.getParameters(), Imaging.EncoderParameters))
                Catch ex As Exception
                    Throw New ArgumentException(ex.Message)
                End Try
            End Try
            'Save Flush
            parameter = New Imaging.EncoderParameter(Imaging.Encoder.SaveFlag, Imaging.EncoderValue.Flush)
            m_strategy = New StrategyNormal(Me)
            m_contex.m_image.SaveAdd(DirectCast(m_strategy.getParameters(), Imaging.EncoderParameters))
            Dim i As Image = DirectCast(m_contex.m_image.Clone, Image)
            m_contex.m_image.Dispose()
            m_contex.m_image = DirectCast(i.Clone, Image)
            i.Dispose()
        Catch
            Try
                Dim i As Image = DirectCast(m_contex.m_image.Clone, Image)
                m_contex.m_image.Dispose()
                m_contex.m_image = DirectCast(i.Clone, Image)
                i.Dispose()
            Catch
            End Try
        End Try
    End Sub
End Class