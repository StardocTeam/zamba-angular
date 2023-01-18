Public Interface IVolume
    Inherits ICore

    Property Size() As Integer
    Property Type() As VolumeTypes
    Property Files() As Long
    Property copy() As Integer
    Property path() As String
    Property sizelen() As Decimal
    Property state() As Integer
    Property offset() As Integer
    Property VolumeState() As VolumeStates


End Interface

