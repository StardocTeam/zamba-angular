Imports System.ServiceProcess
Imports System.Reflection
Imports System.Data.OleDb
Imports System.Diagnostics
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Windows.Forms
Imports System.Timers.Timer
Imports Zamba.Core
Imports Zamba.Data
Imports Zamba.ZTimers
Imports Zamba.FileTools.SpireTools

Public Class SearchService
    Inherits System.ServiceProcess.ServiceBase

    Private Sub LoadTrace()
        Try
            If Boolean.Parse(UserPreferences.getValue("WithTrace", Sections.UserPreferences, True)) Then
                Dim level As Int32 = CType(UserPreferences.getValue("TraceLevel", Sections.UserPreferences, "1"), Int32)
                ZTrace.SetLevel(level, "Zamba Search Service")
            End If
        Catch ex As Exception
        End Try
    End Sub
    'punto de entrada principal para el proceso
    <MTAThread()> Shared Sub Main()
        Dim ServiceToRun() As System.ServiceProcess.ServiceBase

        ServiceToRun = New System.ServiceProcess.ServiceBase() {New SearchService}
        System.ServiceProcess.ServiceBase.Run(ServiceToRun)
    End Sub

#Region "Eventos"

    Dim T1 As Threading.Timer
    Dim CB As New System.Threading.TimerCallback(AddressOf threadStartService)
    Dim state As Object
    Dim processing As Boolean

    'Agente adjunto
    Dim agentAttachedService As AgentServiceBusiness = Nothing
    Dim agentAttachedTimer As Threading.TimerCallback = Nothing
    Dim T2 As Threading.Timer

    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Add code here to start your service. This method should set things
        ' in motion so your service can do its work.
        Try
            System.Diagnostics.Debugger.Break()
            Me.LoadTrace()

            Trace.WriteLineIf(ZTrace.IsInfo, "Se inicia el Servicio de Indexacion.")
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Trace.WriteLineIf(ZTrace.IsInfo, "Error" & ex.Message)
        End Try

        Try
            'Se lee el intervalo de repetición de ejecución del Servicio.
            Dim intervaloServicio As Integer = CInt(Data.ZOptFactory.GetValue("IntervaloServicioIndexadorMinutos"))

            If (T1 Is Nothing) Then
                T1 = New Threading.Timer(CB, state, 0, intervaloServicio * 60000)
            End If

            agentAttachedService = New AgentServiceBusiness(7)
            agentAttachedTimer = New Threading.TimerCallback(AddressOf ExecuteAttachedAgent)
            If (T2 Is Nothing) Then
                T2 = New Threading.Timer(agentAttachedTimer, Nothing, 0, 60 * 60000)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Trace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
        End Try
    End Sub

    Private Sub ExecuteAttachedAgent()
        agentAttachedService.StartService()
    End Sub

    Protected Overrides Sub OnStop()
        Try
            Dim strng() As String = Nothing
            Dim t1 As New Threading.Thread(AddressOf FinalizeService)
            t1.Start()
        Catch ex As Threading.AbandonedMutexException
        Catch ex As Threading.SemaphoreFullException
        Catch ex As Threading.SynchronizationLockException
        Catch ex As Threading.ThreadAbortException
        Catch ex As Threading.ThreadInterruptedException
        Catch ex As Threading.ThreadStartException
        Catch ex As Threading.ThreadStateException
        Catch ex As Threading.WaitHandleCannotBeOpenedException
        End Try
    End Sub
#End Region

#Region "Procesos"
    ''' <summary>
    ''' Ejecuta el servicio
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub threadStartService()
        Try
            If processing = False Then
                processing = True
                ProcessDocTypes()
            End If
        Catch ex As Threading.SynchronizationLockException
        Catch ex As Threading.ThreadAbortException
        Catch ex As Threading.ThreadInterruptedException
        Catch ex As Threading.ThreadStateException
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            processing = False
        End Try
    End Sub

    Public Sub ProcessDocTypes()
        Trace.WriteLineIf(ZTrace.IsVerbose, Date.Now.ToString + "Se ha iniciado la indexacion.")

        Dim body As String = String.Empty
        Dim AuxDs3 As New DataTable
        Dim dsDocs As New DataSet
        Dim DocsCount As Integer

        'Datatable Auxiliar para filtrar NOTAS
        AuxDs3.Columns.Add("FechaCreacion")
        AuxDs3.Columns.Add("Ruta")
        AuxDs3.Columns.Add("DocID")
        AuxDs3.Columns.Add("DocTypeID")
        AuxDs3.Columns.Add("Codigo")

        Try
            'Obtengo el cantidad de documentos que se quieren indexar por cada iteración del servicio.
            Dim numberOfDocsToProcess As Integer = CInt(Data.ZOptFactory.GetValue("CantDeDocsAProcesarServicioIndexador"))
            'Si no existe la propiedad en ZOPT la creo. 100000 es el valor por defecto. 
            If numberOfDocsToProcess = Nothing Then
                Data.ZOptFactory.Insert("CantDeDocsAProcesarServicioIndexador", "100000")
                numberOfDocsToProcess = 100000
            End If

            'Se obtiene los tipos de documentos que se van a Indexar. Se setea desde ZOPT.
            Dim docTypesIndexar As String() = Split(Data.ZOptFactory.GetValue("DocTypesIndexacion"), ",")
            Trace.WriteLineIf(ZTrace.IsVerbose, "DocTypes a indexar:" & Data.ZOptFactory.GetValue("DocTypesIndexacion"))

            Dim lastExecution As String = Data.ZOptFactory.GetValue("UltimaEjecucionServicioIndexador")
            Dim ultimaEjecucion As Date
            'Obtengo el último día en que se ejecuto el servicio.
            If lastExecution <> Nothing Then
                ultimaEjecucion = CDate(lastExecution)
                If Now.Date > ultimaEjecucion Then
                    'Borra los archivos que se encuentran en edición de la tabla ZSearchService para que sean reindexados
                    'Esto se realiza 1 vez al día.
                    DocTypesBusiness.DeletefromZsearchService(docTypesIndexar)
                End If
            End If

            Dim Ds As DataSet = DocTypesBusiness.GetDocTypes(docTypesIndexar)

            For Each row As DataRow In Ds.Tables(0).Rows

                If row.Item(9) = 4 Then
                    Trace.WriteLineIf(ZTrace.IsVerbose, "DocType a indexar: MANUALES")
                    'Obtengo Manuales que aun no fueron indexados
                    dsDocs = DocTypesBusiness.getVersionedDocsByDocTypeId(row.Item(9))
                Else
                    'DEMAS DOCTYPES
                    Select Case row.Item(9)
                        Case 6
                            Trace.WriteLineIf(ZTrace.IsVerbose, "DocType a indexar: CIRCULARES")
                            'Obtengo Circulares que no fueron indexadas.
                            dsDocs = DocTypesBusiness.getActiveDocsByDocTypeId(row.Item(9))
                        Case 8
                            Trace.WriteLineIf(ZTrace.IsVerbose, "DocType a indexar: NOTAS")
                            'Obtengo las notas que no fueron indexadas.
                            dsDocs = DocTypesBusiness.getActiveDocsByDocTypeId(row.Item(9))
                        Case 9
                            Trace.WriteLineIf(ZTrace.IsVerbose, "DocType a indexar: DOCUMENTOS ANEXOS")
                            'Obtengo los Documentos anexos que no fueron indexados
                            dsDocs = DocTypesBusiness.getActiveDocsByDocTypeId(row.Item(9))
                    End Select
                End If

                DocsCount = numberOfDocsToProcess

                If dsDocs.Tables.Count > 0 Then
                    Trace.WriteLineIf(ZTrace.IsVerbose, "Cantidad de documentos a indexar: " & dsDocs.Tables(0).Rows.Count)

                    If dsDocs.Tables(0).Rows.Count > 0 Then
                        'Abro cada archivo y realizo la indexacion
                        For Each r As DataRow In dsDocs.Tables(0).Rows
                            'Abro el archivo
                            Trace.WriteLineIf(ZTrace.IsVerbose, "Se procede a abrir el documento para obtener su contenido.")
                            body = OpenFile(r.Item(1))

                            If body.Length > 0 Then
                                'Inserto su contenido
                                Trace.WriteLineIf(ZTrace.IsVerbose, "Se insertará el contenido del documento en la base de datos.")
                                Select Case r.Item(2)
                                    Case 4
                                        'MANUALES
                                        Results_Business.InsertSearchData(body.Trim, r.Item(3), r.Item(2))
                                    Case 6
                                        'CIRCULARES
                                        Results_Business.InsertSearchData(body.Trim, r.Item(3), r.Item(2))
                                    Case 8
                                        'NOTAS
                                        Results_Business.InsertSearchData(body.Trim, r.Item(3), r.Item(2))
                                    Case 9
                                        'DOCUMENTOS ANEXOS
                                        Results_Business.InsertSearchData(body.Trim, r.Item(3), r.Item(2))
                                End Select

                                Trace.WriteLineIf(ZTrace.IsVerbose, "Se insertó el contenido en la base de datos.")

                                'Guardo el docid procesado del documento
                                Trace.WriteLineIf(ZTrace.IsVerbose, "Se logueará la accion en base de datos.")
                                DocTypesBusiness.LogDocIDIntoZsearchService(r.Item(2), r.Item(3), r.Item(0))
                                Trace.WriteLineIf(ZTrace.IsVerbose, "Documento indexado con éxito. Doc_id: " & r.Item(3).ToString() & " Doc_type_id:  " & r.Item(2).ToString())
                                Trace.WriteLineIf(ZTrace.IsVerbose, "=======================================================================================================================")
                            End If

                            DocsCount = DocsCount - 1
                            If DocsCount = 0 Then Exit For
                        Next
                        dsDocs.Reset()
                    End If
                End If
            Next

            'Guardo la fecha de ejecución del servicio. Si no existe en Zopt la propiedad, se crea.
            If Data.ZOptFactory.GetValue("UltimaEjecucionServicioIndexador") = Nothing Then
                Data.ZOptFactory.Insert("UltimaEjecucionServicioIndexador", Now.Date.ToString)
            Else
                Data.ZOptFactory.Update("UltimaEjecucionServicioIndexador", Now.Date.ToString)
            End If

            Trace.WriteLineIf(ZTrace.IsVerbose, Date.Now.ToString & "Se ha completado exitosamente el proceso de indexación de los documentos existentes en el servidor.")
            Trace.WriteLineIf(ZTrace.IsVerbose, "=======================================================================================================================")
            Trace.WriteLineIf(ZTrace.IsVerbose, "=======================================================================================================================")

            OnStop()
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Trace.WriteLineIf(ZTrace.IsVerbose, "Se produjo un error durante el proceso de indexación. | " + ex.ToString())
        End Try
    End Sub

    Private Function OpenFile(ByVal File As String) As String
        Dim extension As String = File.Remove(0, File.LastIndexOf("."))
        Dim body As String = String.Empty

        If extension.Contains(".doc") Or extension.Contains(".docx") Or extension.Contains(".dot") Then
            Trace.WriteLineIf(ZTrace.IsVerbose, "Procesando archivo Word")
            Try
                If IO.File.Exists(File) Then

                    Dim st As New Zamba.FileTools.SpireTools()

                    Trace.WriteLineIf(ZTrace.IsVerbose, Date.Now.ToString + " - Se abre el archivo Word. Buscando documento en: " + File)
                    Trace.WriteLineIf(ZTrace.IsVerbose, Date.Now.ToString + " - Identificando el contenido del Word.")
                    body = st.GetTextFromDoc(File)
                Else
                    Trace.WriteLineIf(ZTrace.IsVerbose, "No se encuentra la ruta del archivo: " + File)
                    Trace.WriteLineIf(ZTrace.IsVerbose, "=======================================================================================================================")
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Trace.WriteLineIf(ZTrace.IsVerbose, Date.Now.ToString + " - Ocurrio un error al abrir el word. " _
                                  + ex.ToString())
                Trace.WriteLineIf(ZTrace.IsVerbose, "=======================================================================================================================")
            End Try
        ElseIf extension.Contains(".xls") Or extension.Contains(".xlsx") Then
            Trace.WriteLineIf(ZTrace.IsVerbose, "Procesando archivo Excel")
            Try
                If IO.File.Exists(File) Then

                    Dim st As New Zamba.FileTools.SpireTools()

                    Trace.WriteLineIf(ZTrace.IsVerbose, Date.Now.ToString + " - Se abre el archivo Excel. Buscando documento en: " + File)
                    Trace.WriteLineIf(ZTrace.IsVerbose, Date.Now.ToString + " - Identificando el contenido del archivo Excel.")
                    body = st.GetTextFromExcel(File)
                Else
                    Trace.WriteLineIf(ZTrace.IsVerbose, "No se encuentra la ruta del archivo: " & File)
                    Trace.WriteLineIf(ZTrace.IsVerbose, "=======================================================================================================================")
                End If

            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsVerbose, Date.Now.ToString + " - Ocurrio un error al abrir el Excel. " _
                                  + ex.ToString())
                Trace.WriteLineIf(ZTrace.IsVerbose, Date.Now.ToString + " - Haciendo Dispose del objeto. ")
                Trace.WriteLineIf(ZTrace.IsVerbose, "=======================================================================================================================")
            End Try
        ElseIf extension.Contains(".pdf") Then
            Trace.WriteLineIf(ZTrace.IsVerbose, "Procesando archivo PDF")
            Try
                If IO.File.Exists(File) Then
                    Trace.WriteLineIf(ZTrace.IsVerbose, Date.Now.ToString + " - Se abre el archivo PDF. Buscando documento en: " + File)
                    Trace.WriteLineIf(ZTrace.IsVerbose, Date.Now.ToString + " - Se Obtiene el texto del PDF.")
                    Dim st As New Zamba.FileTools.SpireTools()

                    body = st.GetTextFromPDF(File)
                Else
                    Trace.WriteLineIf(ZTrace.IsVerbose, "No se encuentra la ruta del archivo: " & File)
                    Trace.WriteLineIf(ZTrace.IsVerbose, "=======================================================================================================================")
                End If
            Catch ex As Exception
                Trace.WriteLineIf(ZTrace.IsVerbose, Date.Now.ToString + " - Ocurrio un error al obtener el texto del PDF. " _
                                  + ex.ToString())
                Trace.WriteLineIf(ZTrace.IsVerbose, "=======================================================================================================================")
            End Try
        End If
        Return body
    End Function

    Private Sub FinalizeService()
        Try
            Try
                'If Not IsNothing(WSS) Then
                '    WSS.Abort()
                '    WSS = Nothing
                'End If
                Trace.WriteLineIf(ZTrace.IsInfo, "Servicio de busqueda finalizado.")
                Trace.WriteLineIf(ZTrace.IsVerbose, "=======================================================================================================================")
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Application.Exit()
            Me.Dispose()

        Catch ex As Threading.AbandonedMutexException
        Catch ex As Threading.SemaphoreFullException
        Catch ex As Threading.SynchronizationLockException
        Catch ex As Threading.ThreadAbortException
        Catch ex As Threading.ThreadInterruptedException
        Catch ex As Threading.ThreadStartException
        Catch ex As Threading.ThreadStateException
        Catch ex As Threading.WaitHandleCannotBeOpenedException
        Catch ex As Exception
        End Try
    End Sub
#End Region

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        CanPauseAndContinue = True
    End Sub
End Class
