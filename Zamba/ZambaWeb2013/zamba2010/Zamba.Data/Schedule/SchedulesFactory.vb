Imports ZAMBA.Servers
Imports Zamba.Data
Imports Zamba.Core
''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Data
''' Class	 : Data.SchedulesFactory
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Factory para programar envios de reportes
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class SchedulesFactory




    Public Shared Function GetSchedules(ByVal ObjectTypeId As ObjectTypes, ByVal ObjectId As Int64) As ArrayList
        Dim StrSelect As String = "SELECT * FROM ZSCHED WHERE OBJECT_TYPE_ID = " & ObjectTypeId & " AND OBJECTID = " & ObjectId
        Dim Dstemp As DataSet
        Dim DsSched As New Zamba.Core.DsSchedule
        Dstemp = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        Dstemp.Tables(0).TableName = DsSched.Schedule.TableName
        DsSched.Merge(Dstemp)

        Dim Schedules As New ArrayList
        Dim i As Int32
        For i = 0 To DsSched.Schedule.Count - 1
            Schedules.Add(New Schedule(DsSched.Schedule(i).SCHED_ID, ObjectTypeId, ObjectId, DsSched.Schedule(i).SCHED_DATE, DsSched.Schedule(i).SCHED_TIME))
        Next
        Return Schedules
    End Function





    Public Shared Function CheckSchedule(ByVal Schedule As Schedule) As Boolean
        Select Case Schedule.ScheduleDate
            Case 1 To 7
                Schedule.DayType = DayTypes.DayofWeek
                If Now.DayOfWeek = Schedule.ScheduleDate Then
                    If Now.ToString("HH:mm") = CDate(Schedule.ScheduleTime).ToString("HH:mm") Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Case Else
                Schedule.DayType = DayTypes.EspecificDate
                If Now = CDate(Schedule.ScheduleDate & " " & Schedule.ScheduleTime) Then
                    Return True
                Else
                    Return False
                End If
        End Select
	End Function





    Private Sub New()
    End Sub





    Public Shared Function GetMaxId() As Int32
        Return Servers.Server.Con.ExecuteScalar(CommandType.Text, "SELECT MAX(TASK_ID) FROM schedule")
    End Function





    Public Shared Function GetReportesIds() As DataSet
        Return Servers.Server.Con.ExecuteDataset(CommandType.Text, "select * from REPORTE_ID where AUTOMATICO=1")
    End Function




    Public Shared Function GetScheduleByTaskId(ByVal strid As Int32) As DataSet
        Dim ds As DataSet
        Dim sql As String = "select * from schedule where TASK_ID=" & strid
        ds = Servers.Server.Con.ExecuteDataset(CommandType.Text, sql)
        Return ds
	End Function



    Public Shared Function GetDESByReporteId(ByVal strRepId As Int32) As String
        Return Servers.Server.Con.ExecuteScalar(CommandType.Text, "select DES from REPORTE_ID where ID=" & strRepId)
	End Function




    Public Shared Sub DeleteSchedule(ByVal TaskId As Int32)
        Dim sql As String = "delete from schedule where TASK_ID=" & TaskId
        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, sql)
    End Sub




    Shared fechaini As Date, fechafin As Date, activo As Int32, repeticiones As Int32
	Shared id_reporte As Int32, mail_to As String = "0", mail_cc As String = "0", mail_cco As String = "0" 'id_task As Int32,



#Region "Seteo de datos"
    Public Shared Sub setFechaInicial(ByVal fecha_ini As Date)
        fechaini = fecha_ini
    End Sub
    Public Shared Sub setFechaFinal(ByVal fecha_fin As Date)
        fechafin = fecha_fin
    End Sub
    Public Shared Sub setActivo(ByVal act As Int32)
        activo = act
    End Sub
    Public Shared Sub setRepeticiones(ByVal repet As Int32)
        repeticiones = repet
    End Sub
    Public Shared Sub setIdReporte(ByVal reporte As Int32)
        id_reporte = reporte
    End Sub
    Public Shared Sub addMails(ByVal mto As String, ByVal mcc As String, ByVal mcco As String)
        mail_to = mto
        mail_cc = mcc
        mail_cco = mcco
    End Sub
#End Region

#Region "Obencion de datos"
    Public Shared Function getFechaInicial() As Date
        Return fechaini
    End Function
    Public Shared Function getFechaFinal() As Date
        Return fechafin
    End Function
    Public Shared Function getActivo() As Int32
        Return activo
    End Function
    Public Shared Function getRepeticiones() As Int32
        Return repeticiones
    End Function
    Public Shared Function getIdReporte() As Int32
        Return id_reporte
    End Function
    Public Shared Function getMailTo() As String
        Return mail_to
    End Function
    Public Shared Function getMailCC() As String
        Return mail_cc
    End Function
    Public Shared Function getMailCCO() As String
        Return mail_cco
    End Function
#End Region



    Public Shared Sub addTask(ByVal _id As Int64, ByVal _fechaini As Date, ByVal _frecuencia As Int32, ByVal _fechafin As Date, ByVal _ultima As Date, ByVal _activo As Int32, ByVal _id_reporte As Int32, ByVal _formato As Int32, ByVal _carpeta As String, ByVal _imprimir As String, ByVal _min As Int32, ByVal _desc As String, ByVal origen As Int32, ByVal repet As Int32)
        '  fechafin = _fechafin
        '  fechaini = _fechaini
        '   If _activo Then activo = 1 Else activo = 0
        '  repeticiones = _frecuencia
        ' id_reporte = _id_reporte
        Dim col As String = "(TASK_ID,FECHA,FRECUENCIA,FECHA_FIN,ACTIVO,REPORTE,ULTIMA,FORMATO,CARPETA,MAIL_TO,MAIL_CC,MAIL_CCO,IMPRIMIR,MINUTOS,DESCRIP,ORIGEN,REPET)"
        Dim values As New System.Text.StringBuilder
        values.Append("(")
        values.Append(_id)
        values.Append(",")
        values.Append(Server.Con.ConvertDateTime(_fechaini.ToString.Trim))
        values.Append(",")
        values.Append(_frecuencia)
        values.Append(",")
        values.Append(Server.Con.ConvertDateTime(_fechafin.ToString.Trim))
        values.Append(",")
        values.Append(_activo)
        values.Append(",")
        values.Append(_id_reporte)
        values.Append(",")
        values.Append(Server.Con.ConvertDateTime(_ultima.ToString.Trim))
        values.Append(",")
        values.Append(_formato)
        values.Append(",'")
        values.Append(_carpeta)
        values.Append("','")
        values.Append(mail_to)
        values.Append("','")
        values.Append(mail_cc)
        values.Append("','")
        values.Append(mail_cco)
        values.Append("',")
        values.Append(_imprimir)
        values.Append(",")
        values.Append(_min)
        values.Append(",'")
        values.Append(_desc)
        values.Append("',")
        values.Append(origen)
        values.Append(",")
        values.Append(repet)
        values.Append(")")
        Dim sql As New System.Text.StringBuilder
        sql.Append("INSERT INTO Schedule ")
        sql.Append(col)
        sql.Append(" values")
        sql.Append(values.ToString)
        _activo = 1
        Try
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql = Nothing
            values = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
	End Sub




    Public Shared Sub updateTask(ByVal _id As Int64, ByVal _fechaini As Date, ByVal _frecuencia As Int32, ByVal _fechafin As Date, ByVal _ultima As Date, ByVal _activo As Int32, ByVal _id_reporte As Int32, ByVal _formato As Int32, ByVal _carpeta As String, ByVal _imprimir As String, ByVal _min As Int32, ByVal _desc As String, ByVal origen As Int32, ByVal repet As Int32)
        '  fechafin = _fechafin
        ' fechaini = _fechaini
        '  If _activo Then activo = 1 Else activo = 0
        '  repeticiones = _frecuencia
        ' id_reporte = _id_reporte
        Dim sql As String = "update schedule set FECHA=" & Server.Con.ConvertDateTime(_fechaini.ToString.Trim) & ",FRECUENCIA=" & _frecuencia & ",FECHA_FIN=" & Server.Con.ConvertDateTime(_fechafin.ToString.Trim) & ",ACTIVO=" & _activo & ",REPORTE=" & _id_reporte & ",ULTIMA=" & Server.Con.ConvertDateTime(_ultima.ToString.Trim) & ",FORMATO=" & _formato & ",CARPETA='" & _carpeta & "',MAIL_TO='" & mail_to & "',MAIL_CC='" & mail_cc & "',MAIL_CCO='" & mail_cco & "',IMPRIMIR=" & _imprimir & ",MINUTOS=" & _min & ",DESCRIP='" & _desc & "',ORIGEN=" & origen & ",REPET=" & repet & "where TASK_ID=" & _id
        _activo = 1
        Try
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
	End Sub




    Public Shared Function GetSchedules() As DataSet
        Dim ds As DataSet
        ds = Servers.Server.Con.ExecuteDataset(CommandType.Text, "select * from schedule order by TASK_ID")
        Return ds
	End Function



    Public Shared Function cargarTareasHoy(ByVal Prog As iProgramador) As System.Collections.Hashtable
        Dim ListaTareas As New Hashtable
        Dim ds As New DataSet
        Dim fila As DataRow
        Dim Tarea As csTarea
        Try
            If Server.IsOracle Then
                Dim parNames() As String = {}
                ' Dim parTypes() As Object = {}
                Dim parValues() As Object = {}
                'ds = Server.Con.ExecuteDataset("ZScdGetSchedule_PKG.ZScdGetTareasHoy", parValues)
                ds = Server.Con.ExecuteDataset("zsp_schedule_100.GetTasks", parValues)
            Else
                Dim parValues() As Object = {}
                'ds = Server.Con.ExecuteDataset("ZScdGetTareasHoy", parvalues)
                ds = Server.Con.ExecuteDataset("zsp_schedule_100_GetTasks", parValues)
            End If
        Catch ex As Exception
            Return Nothing
        End Try

        fila = ds.Tables(0).NewRow
        For Each fila In ds.Tables(0).Rows
            If CType(fila("ACTIVO"), Boolean) = True Or CType(fila("ACTIVO"), Int32) = 1 Then
                'Tarea = Tarea.NuevaTarea(fila("TASK_ID"), fila("FECHA"), fila("FRECUENCIA"), fila("FECHA_FIN"), fila("ACTIVO"), fila("REPORTE"), Prog)
                Tarea = csTarea.NuevaTarea(fila, Prog)
                If Tarea.estoyAtrasado Or Tarea.programadoParaHoy Then
                    ListaTareas.Add(Tarea.getId, Tarea)
                    'Tarea.comenzarTarea()
                Else
                    Tarea = Nothing
                End If
            End If
        Next
        ds.Dispose()
        Return ListaTareas
    End Function



    Public Shared Function reprogramarTarea(ByVal Tarea As csTarea) As Boolean
        If Tarea.reprogramarMe() Then
            'ejecutar store proceducere ZSchUpdRepTarea Tarea.TareaId,Tarea.FechaEjecucion
            If Server.isOracle Then
                Dim parNames() As String = {"Id", "Fecha"}
                ' Dim parTypes() As Object = {13, 13} '¿¿¿¿Que nro es TIMESTAMP???
                Dim parValues() As Object = {Tarea.getId, Tarea.getFechaEejecucion}
                Try
                    'Server.Con.ExecuteNonQuery("ZSchUpdSchedule_pkg.ZSchUpdRepTarea", parValues)
                    Server.Con.ExecuteNonQuery("zsp_schedule_100.UpdLastTaskExecution", parValues)
                Catch
                End Try
            Else
                Dim parValues() As Object = {Tarea.getId, Tarea.getFechaEejecucion}
                Try
                    'Server.Con.ExecuteNonQuery("ZSchUpdRepTarea", parvalues)
                    Server.Con.ExecuteNonQuery("zsp_schedule_100_UpdLastTaskExecution", parValues)
                Catch
                End Try
            End If
            Return True
        Else
            'ejecutar store proceducere ZSchDelTarea Tarea.TareaId
            If Server.isOracle Then
                Dim parNames() As String = {"Id"}
                ' Dim parTypes() As Object = {13}
                Dim parValues() As Object = {Tarea.getId}
                Try
                    'Server.Con.ExecuteNonQuery("ZSchDelSchedule_pkg.ZSchDelTarea", parValues)
                    Server.Con.ExecuteNonQuery("zsp_schedule_100.DeleteTask", parValues)
                Catch
                End Try
            Else
                Dim parValues() As Object = {Tarea.getId}
                Try
                    'Server.Con.ExecuteNonQuery("ZSchDelTarea", parvalues)
                    Server.Con.ExecuteNonQuery("zsp_schedule_100_DeleteTask", parvalues)
                Catch
                End Try
            End If
            Return False
        End If
    End Function



End Class
