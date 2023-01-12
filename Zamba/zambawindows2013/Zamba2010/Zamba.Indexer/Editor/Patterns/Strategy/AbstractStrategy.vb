Friend MustInherit Class AbstractStrategy
    'Patron Strategy
    Friend m_parameters As Imaging.EncoderParameters
    Friend m_state As AbstractState

    Public Function getParameters() As Imaging.EncoderParameters
        Return m_parameters
    End Function
End Class
