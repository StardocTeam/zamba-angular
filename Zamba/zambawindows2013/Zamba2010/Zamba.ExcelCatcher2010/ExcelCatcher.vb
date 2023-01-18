Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Runtime.InteropServices.Marshal
Imports System.Globalization
Imports System.Threading.Thread
Imports Zamba.Core

Public Class ExcelCatcher
    'Workbook que se esta mostrando
    Private xlBook As Excel.Workbook = Nothing
    ''' <summary>
    ''' Evento q se dispara cuando se cierra el excel desde fuera
    ''' </summary>
    ''' <remarks></remarks>
    Public Event ExcelClosed()
    'Timer de cerrado en caso de que no se haya podido cerrar el excel por tema de tiempo
    Private WithEvents TimerClose As Threading.Timer
    Private TCBClose As New Threading.TimerCallback(AddressOf closing)
    'Se cerro desde zamba
    Private closedmanual As Boolean
    'El closing ya esta siendo ejecutado en otro thread
    Private blnClosingInUse As Boolean
    'Si el usuario cerro el excel x fuera y con la cruz
    Private blnClosedByUser As Boolean
    'Cantidad de intentos de cerrar el excel (maximo 3)
    Private intCloseAttempted As Int16 = 0
    'Nombre del archivo que se esta visualizando
    Private _filename As String
    'Si se genera un nuevo trace o no
    Private _withTrace As Boolean
    'Temporal para mostrar los excel. Se utiliza por problemas de liberacion de memoria.
    'Para mayor información visitar el siguiente link: 
    'http://www.velocityreviews.com/forums/showpost.php?s=f87f0674feda4442dcbd40019cbca65b&p=528575&postcount=2
    Dim tmpBooks As Excel.Workbooks = Nothing
    'Aplicacion
    Dim xlApp As Excel.Application = Nothing


    ''' <summary>
    ''' Muestra el excel por fuera
    ''' </summary>
    ''' <param name="filename"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal filename As String, ByVal withTrace As Boolean)
        _filename = filename
        _withTrace = withTrace
        If withTrace = True Then
            Trace.Listeners.Add(New TextWriterTraceListener(System.Windows.Forms.Application.StartupPath & "\Exceptions\Trace Excel por Fuera" & Now.ToString("dd-MM-yyyy HH-mm-ss") & ".txt", "TraceExcel"))
            Trace.AutoFlush = True
            ZTrace.WriteLineIf(ZTrace.IsError, "ExcelCatcher Instanciado")
            ZTrace.WriteLineIf(ZTrace.IsVerbose,_filename)
        End If
    End Sub

    ''' <summary>
    ''' Path del excel q se esta mostrando
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FileName() As String
        Get
            Return _filename
        End Get
    End Property

    ''' <summary>
    ''' Abre el workbook
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub openExcel()
        'Dim xlApp As Excel.Application = Nothing
        Try
            Try
                ZTrace.WriteLineIf(ZTrace.IsError, "Adjuntando a Excel")
                xlApp = CType(GetObject(, "Excel.Application"), Excel.Application)
                If Not IsNothing(xlApp) Then
                    ZTrace.WriteLineIf(ZTrace.IsError, "Excel Adjuntado")
                Else
                    ZTrace.WriteLineIf(ZTrace.IsError, "Instanciando Excel")
                    xlApp = New Excel.Application
                    ZTrace.WriteLineIf(ZTrace.IsError, "Excel Instanciado")
                End If
            Catch
                ZTrace.WriteLineIf(ZTrace.IsError, "Instanciando Excel")
                xlApp = New Excel.Application
                ZTrace.WriteLineIf(ZTrace.IsError, "Excel Instanciado")
            End Try

            If IsNothing(xlBook) Then
                tmpBooks = xlApp.Workbooks
                Try
                    xlBook = tmpBooks.Open(FileName)
                Catch ex As Exception
                    CurrentThread.CurrentCulture = New CultureInfo("en-US")
                    xlBook = tmpBooks.Open(FileName)
                End Try
                ZTrace.WriteLineIf(ZTrace.IsError, "Workbook Abierto")
            End If

            xlApp.Visible = True
            xlApp.UserControl = True

            AddHandler xlBook.BeforeClose, AddressOf closeDoc
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsError, ex.ToString())
        End Try
    End Sub

    ''' <summary>
    ''' Cierra el excel a pedido de Zamba
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub closeExcel()
        'Si el metodo closing ya se esta ejecutando o ya se cerro el excel, no ejecuto este close
        If blnClosingInUse = False And blnClosedByUser = False Then
            'Marco que se esta cerrando desde Zamba
            closedmanual = True
            Try
                ZTrace.WriteLineIf(ZTrace.IsError, "Cerrando Workbook manualmente")
                If Not IsNothing(xlBook) Then
                    Dim closeApp As Boolean = True
                    If xlBook.Saved = False Then
                        If System.Windows.Forms.MessageBox.Show("Desea guardar los cambios?", "Microsoft Excel", System.Windows.Forms.MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                            xlBook.Save()
                        End If
                        xlBook.Saved = True
                    End If

                    'Se liberan los documentos
                    If tmpBooks IsNot Nothing Then
                        Try
                            tmpBooks.Close()
                        Catch
                        End Try
                        ReleaseComObject(tmpBooks)
                        tmpBooks = Nothing
                    End If
                    Try
                        xlBook.Close()
                    Catch
                    End Try
                    ReleaseComObject(xlBook)
                    xlBook = Nothing
                    ZTrace.WriteLineIf(ZTrace.IsError, "Workbook Cerrado")

                    'Si no queda ningun workbook abierto cierro tambien el excel
                    'Dim xlApp As Excel.Application = Nothing
                    Try
                        xlApp = CType(GetObject(, "Excel.Application"), Excel.Application)
                    Catch
                    End Try
                    If Not IsNothing(xlApp) Then
                        If xlApp.Workbooks.Count = 0 Then
                            xlApp.Quit()
                            ReleaseComObject(xlApp)
                            xlApp = Nothing
                            ZTrace.WriteLineIf(ZTrace.IsError, "Excel Cerrado")
                        End If
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsError, "Aplicacion excel no encontrada")
                    End If

                    Try
                        If _withTrace = True Then
                            Trace.Listeners.Remove("TraceExcel")
                        End If
                    Catch
                    End Try
                End If

            Catch ex As Exception
                'Si el objeto ya fue cerrado tira error
                ZTrace.WriteLineIf(ZTrace.IsError, ex.ToString())
            End Try
        End If
        GC.Collect()
    End Sub

    ''' <summary>
    ''' Cuando se esta cerrando el xls desde fuera dispara el timer
    ''' </summary>
    ''' <param name="cancel"></param>
    ''' <remarks></remarks>
    Private Sub closeDoc(ByRef cancel As Boolean)
        ZTrace.WriteLineIf(ZTrace.IsError, "Evento cerrar")
        TimerClose = New Threading.Timer(TCBClose, Nothing, 1000, 5000)
    End Sub

    ''' <summary>
    ''' Si se cerro el xls disparo el evento
    ''' </summary>
    ''' <param name="state"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Tomas] 28/10/2009  Modified    Se agrega ztimers por problemas entre los hilos.
    ''' </history>
    Private Sub closing(ByVal state As Object)
        'Cierra el timer
        If Not IsNothing(TimerClose) Then
            ZTrace.WriteLineIf(ZTrace.IsError, "Cerrando timer")
            If Not TimerClose Is Nothing Then
                TimerClose.Dispose()
                TimerClose = Nothing
            End If
        End If

        Try
            'Si el closing no se esta ejecutando y todavia el excel no se cerro
            If blnClosingInUse = False And blnClosedByUser = False Then
                ZTrace.WriteLineIf(ZTrace.IsError, "Validando cerrado de excel")
                blnClosingInUse = True
                'GC.Collect()
                'Si el workbook todavia esta vivo
                If Not IsNothing(xlBook) Then
                    Try
                        'Si tira error es porque se cerro por fuera el excel, sino es xq todavia no se cerro
                        If xlBook.Saved = True Then
                        End If

                        'En caso de que el usuario deje el mensaje abierto de guardar los cambios
                        TimerClose = New Threading.Timer(TCBClose, Nothing, 2000, 5000)
                        'Exit Sub
                    Catch ex As Exception
                        ZTrace.WriteLineIf(ZTrace.IsError, ex.ToString())
                        cerrarExcel()
                    End Try
                Else
                    cerrarExcel()
                End If

                blnClosingInUse = False
            End If
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsError, ex.ToString())
            blnClosingInUse = False

            'Guardo el nro de intento
            ZTrace.WriteLineIf(ZTrace.IsError, "Nro de intento fallido: " & intCloseAttempted)
            intCloseAttempted += 1

            'Intento cerrar el excel 3 veces
            If intCloseAttempted < 3 Then
                'En caso de error, disparo otra vez el timer
                ZTrace.WriteLineIf(ZTrace.IsError, "Pidiendo nuevo intento de cerrado")
                TimerClose = New Threading.Timer(TCBClose, state, 1000, 5000)
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Setea las variables del excel cerrado y avisa a Zamba que cierre la solapa
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub cerrarExcel()
        ZTrace.WriteLineIf(ZTrace.IsError, "Workbook cerrado")
        blnClosedByUser = True
        'Evento cerrar solapa de zamba
        RaiseEvent ExcelClosed()

        Try
            'Cierro el listener
            If _withTrace = True Then
                Trace.Listeners.Remove("TraceExcel")
            End If
        Catch
        End Try
    End Sub
End Class