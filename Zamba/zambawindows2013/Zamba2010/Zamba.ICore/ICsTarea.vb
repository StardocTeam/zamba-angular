Public Interface IcsTarea
    Event alarmaTarea()
    Sub comenzarTarea()
    Sub pararTarea()
    Sub retomarTarea()
    Function reprogramarMe() As Boolean
    Function estoyAtrasado() As Boolean
    Function programadoParaHoy() As Boolean
    Function soyRepetitiva() As Boolean
    Function getId() As Int32
    Function getFechaEejecucion() As DateTime
    Function getFrecuencia() As Int32
    Function getFechaFin() As DateTime
    Function getEstado() As Boolean
    Function getReporteId() As Int32
    Function getCarpeta() As String
    Function getFormato() As Int32
    Function getMailCco() As String
    Function getMailCc() As String
    Function getMailPara() As String
    Function getImprimir() As Int32
    Function getRepetirCadaMin() As Int32
    Function getDescripcion() As String
End Interface