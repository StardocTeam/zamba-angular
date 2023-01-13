Friend Class StrategyNormal
    Inherits AbstractStrategy

    Public Sub New(ByVal state As AbstractState)
        m_parameters = New Imaging.EncoderParameters(1)
        m_parameters.Param(0) = state.parameter
        m_state = state
    End Sub
End Class