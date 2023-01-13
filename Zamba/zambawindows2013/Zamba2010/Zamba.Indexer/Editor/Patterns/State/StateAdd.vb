Friend Class StateAdd
    Inherits AbstractState

    Public Sub New(ByVal contex As AbstractPersistImage)
        MyBase.New(contex)
        parameter = New Imaging.EncoderParameter(Imaging.Encoder.SaveFlag, Imaging.EncoderValue.FrameDimensionPage)
    End Sub

    Friend Overrides Sub Save(ByVal img As System.Drawing.Image)
        m_contex.m_ActualCount = m_contex.m_ActualCount + 1

        'Save
        Try
            Try
                m_strategy = New StrategyCompress(Me)
                m_contex.m_image.SaveAdd(img, DirectCast(m_strategy.getParameters(), Imaging.EncoderParameters))
            Catch
                Try
                    m_strategy = New StrategyNormal(Me)
                    m_contex.m_image.SaveAdd(img, DirectCast(m_strategy.getParameters(), Imaging.EncoderParameters))
                Catch ex As Exception
                    Throw New ArgumentException(ex.Message)
                End Try
            End Try
            If m_contex.m_ActualCount = m_contex.m_CountImg Then
                m_contex.m_State = New StateClose(m_contex)
            End If
        Catch ex As Exception
            Throw New ArgumentException(ex.Message)
        End Try
        '----
    End Sub
End Class
