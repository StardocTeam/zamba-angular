Public Interface IMensajeForo
    Inherits IZambaCore

    Property UserName() As String
    Property DocId() As Integer
    Property ParentId() As Integer
    Property Mensaje() As String
    Property Fecha() As Date
    Property UserId() As Integer
    Property StateId() As Integer
    Property GroupId() As Int64
    Property DiasVto() As Int32
End Interface