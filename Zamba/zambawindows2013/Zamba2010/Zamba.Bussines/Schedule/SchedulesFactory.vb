Imports Zamba.Data
''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Data
''' Class	 : Data.SchedulesFactory
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Business para programar envios de reportes
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class SchedulesBusiness


    Public Shared Function GetMaxId() As Int32
        Return SchedulesFactory.GetMaxId
    End Function


    ' Devuelve los id de todos los reporte existentes
    Public Shared Function GetReportesIds() As DataSet
        Return SchedulesFactory.GetReportesIds
    End Function

    ' Devuelve la descripcion de un reporte
    Public Shared Function GetDESByReporteId(ByVal strRepId As Int32) As String
        Return SchedulesFactory.GetDESByReporteId(strRepId)
    End Function



#Region "Seteo de datos"
    Public Shared Sub setFechaInicial(ByVal fecha_ini As Date)
        SchedulesFactory.setFechaInicial(fecha_ini)
    End Sub
    Public Shared Sub setFechaFinal(ByVal fecha_fin As Date)
        SchedulesFactory.setFechaFinal(fecha_fin)
    End Sub
    Public Shared Sub setActivo(ByVal act As Int32)
        SchedulesFactory.setActivo(act)
    End Sub
    Public Shared Sub setRepeticiones(ByVal repet As Int32)
        SchedulesFactory.setRepeticiones(repet)
    End Sub
    Public Shared Sub setIdReporte(ByVal reporte As Int32)
        SchedulesFactory.setIdReporte(reporte)
    End Sub
    Public Shared Sub addMails(ByVal mto As String, ByVal mcc As String, ByVal mcco As String)
        SchedulesFactory.addMails(mto, mcc, mcco)
    End Sub
#End Region

#Region "Obencion de datos"
    Public Shared Function getFechaInicial() As Date
        Return SchedulesFactory.getFechaInicial
    End Function
    Public Shared Function getFechaFinal() As Date
        Return SchedulesFactory.getFechaFinal
    End Function
    Public Shared Function getActivo() As Int32
        Return SchedulesFactory.getActivo
    End Function
    Public Shared Function getRepeticiones() As Int32
        Return SchedulesFactory.getRepeticiones
    End Function
    Public Shared Function getIdReporte() As Int32
        Return SchedulesFactory.getIdReporte
    End Function
    Public Shared Function getMailTo() As String
        Return SchedulesFactory.getMailTo
    End Function
    Public Shared Function getMailCC() As String
        Return SchedulesFactory.getMailCC
    End Function
    Public Shared Function getMailCCO() As String
        Return SchedulesFactory.getMailCCO
    End Function
#End Region


    Public Shared Sub addTask(ByVal _id As Int64, _
    ByVal _fechaini As Date, _
    ByVal _frecuencia As Int32, _
    ByVal _fechafin As Date, _
    ByVal _ultima As Date, _
    ByVal _activo As Int32, _
    ByVal _id_reporte As Int32, _
    ByVal _formato As Int32, _
    ByVal _carpeta As String, _
    ByVal _imprimir As String, _
    ByVal _min As Int32, _
    ByVal _desc As String, _
    ByVal origen As Int32, _
    ByVal repet As Int32)
        SchedulesFactory.addTask(_id, _
        _fechaini, _
        _frecuencia, _
        _fechafin, _
        _ultima, _
        _activo, _
        _id_reporte, _
        _formato, _
        _carpeta, _
        _imprimir, _
        _min, _
        _desc, _
        origen, _
        repet)
    End Sub


    Public Shared Sub updateTask(ByVal _id As Int64, _
    ByVal _fechaini As Date, _
    ByVal _frecuencia As Int32, _
    ByVal _fechafin As Date, _
    ByVal _ultima As Date, _
    ByVal _activo As Int32, _
    ByVal _id_reporte As Int32, _
    ByVal _formato As Int32, _
    ByVal _carpeta As String, _
    ByVal _imprimir As String, _
    ByVal _min As Int32, _
    ByVal _desc As String, _
    ByVal origen As Int32, _
    ByVal repet As Int32)
        SchedulesFactory.updateTask(_id, _
        _fechaini, _
        _frecuencia, _
        _fechafin, _
        _ultima, _
        _activo, _
        _id_reporte, _
        _formato, _
        _carpeta, _
        _imprimir, _
        _min, _
        _desc, _
        origen, _
        repet)
    End Sub


    Public Shared Function GetSchedules(ByVal ObjectTypeId As ObjectTypes, _
 ByVal ObjectId As Int64) As ArrayList

        Return SchedulesFactory.GetSchedules(ObjectTypeId, ObjectId)
    End Function


    Public Shared Function GetSchedules() As DataSet
        Return SchedulesFactory.GetSchedules
    End Function


    Public Shared Function CheckSchedule(ByVal Schedule As Schedule) As Boolean
        Return SchedulesFactory.CheckSchedule(Schedule)
    End Function


    Public Shared Function GetScheduleByTaskId(ByVal strid As Int32) As DataSet
        Return SchedulesFactory.GetScheduleByTaskId(strid)
    End Function


    Public Shared Sub DeleteSchedule(ByVal TaskId As Int32)
        SchedulesFactory.DeleteSchedule(TaskId)
    End Sub


    Public Shared Function cargarTareasHoy(ByVal Prog As iProgramador) _
    As System.Collections.Hashtable
        Return SchedulesFactory.cargarTareasHoy(Prog)
    End Function


    Public Shared Function reprogramarTarea(ByVal Tarea As csTarea) As Boolean
        Return SchedulesFactory.reprogramarTarea(Tarea)
    End Function


End Class
