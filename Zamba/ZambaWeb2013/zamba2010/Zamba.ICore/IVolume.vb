Public Interface IVolume
    Inherits ICore

    Property Size() As Integer
    Property VolumeType() As VolumeType
    Property Files() As Long
    Property copy() As Integer
    Property path() As String
    Property sizelen() As Decimal
    Property state() As Integer
    Property offset() As Integer
    Property VolumeState() As VolumeStates

    Enum VolumeStates
        VolumenListo
        VolumenLleno
        VolumenError
        VolumenNoDisponible
        VolumenEnPreparacion
    End Enum
End Interface

