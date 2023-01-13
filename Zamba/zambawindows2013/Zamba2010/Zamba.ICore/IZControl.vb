Public Interface IZControl
    Inherits IDisposable
    Event OnChangeControl(ByRef Item As IZBaseCore)
    Sub ChangeControl(ByRef Item As IZBaseCore)
End Interface
