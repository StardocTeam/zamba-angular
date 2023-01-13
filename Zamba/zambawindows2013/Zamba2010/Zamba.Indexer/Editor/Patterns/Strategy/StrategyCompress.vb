Friend Class StrategyCompress
    Inherits AbstractStrategy

    Public Sub New(ByVal state As AbstractState)
        m_state = state
        If Not IsNothing(state.parameter) Then
            m_parameters = New Imaging.EncoderParameters(2)
            m_parameters.Param(1) = state.parameter
        Else
            m_parameters = New Imaging.EncoderParameters(1)
        End If
        m_parameters.Param(0) = New Imaging.EncoderParameter(Imaging.Encoder.Compression, Imaging.EncoderValue.CompressionCCITT4)
    End Sub
End Class
