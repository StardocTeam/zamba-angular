Public Interface iLog

    Property AVISO() As Int32
    Property ABORTO() As Int32
    Property ERRONEO() As Int32
    Function logMensaje(ByVal iTipo As Int32, ByVal sMensaje As String) As Boolean

End Interface
