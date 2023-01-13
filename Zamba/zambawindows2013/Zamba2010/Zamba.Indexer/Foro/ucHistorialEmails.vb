Imports Zamba.Grid.Grid
Imports Zamba.Filters
Imports Zamba.Core
Imports System.IO
Imports Telerik.WinControls.UI


Public Class ucHistorialEmails
    Inherits ZControl
    Implements IGrid
    Implements IDisposable

    Private _docId As Int32 = 0

#Region " Código generado por el Diseñador de Windows Forms "
    Dim CurrentUserId As Int64

    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If Not isDisposed Then
            If disposing Then
                If Not (components Is Nothing) Then components.Dispose()
                If CheckIn IsNot Nothing Then CheckIn.Dispose()
                If CheckOut IsNot Nothing Then CheckOut.Dispose()
                If dgHistorial IsNot Nothing Then dgHistorial.Dispose()
                If DOC_ID IsNot Nothing Then DOC_ID.Dispose()
                If DOCTYPE IsNot Nothing Then DOCTYPE.Dispose()
                If EstadoFinal IsNot Nothing Then EstadoFinal.Dispose()
                If EstadoInicial IsNot Nothing Then EstadoInicial.Dispose()
                If Etapa IsNot Nothing Then Etapa.Dispose()
                If UCheckin IsNot Nothing Then UCheckin.Dispose()
                If UCheckOut IsNot Nothing Then UCheckOut.Dispose()
                If grdGeneral IsNot Nothing Then grdGeneral.Dispose()
            End If
            MyBase.Dispose(disposing)
            isDisposed = True
        End If
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer
    Private isDisposed As Boolean
    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents grdGeneral As GroupGrid
    'Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DOC_ID As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents CheckIn As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents CheckOut As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents Etapa As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents EstadoInicial As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents EstadoFinal As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents UCheckin As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DOCTYPE As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents UCheckOut As System.Windows.Forms.DataGridTextBoxColumn
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        SuspendLayout()
        '
        grdGeneral = New GroupGrid(True, CurrentUserId, Me, FilterTypes.History)
        grdGeneral.BackColor = System.Drawing.Color.White
        grdGeneral.DataSource = Nothing
        grdGeneral.Dock = System.Windows.Forms.DockStyle.Fill
        grdGeneral.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        grdGeneral.Location = New System.Drawing.Point(0, 0)
        grdGeneral.Name = "GridView"
        grdGeneral.Size = New System.Drawing.Size(912, 306)
        grdGeneral.TabIndex = 0
        grdGeneral.WithExcel = False
        grdGeneral.showRefreshButton = True
        grdGeneral.AllowTelerikGridFilter = True
        grdGeneral.ShowFiltersPanel = False
        '
        'UCTaskHistory
        '
        Name = "UCTaskHistory"
        Size = New System.Drawing.Size(624, 504)
        Controls.Add(grdGeneral)
        ResumeLayout(False)

    End Sub

#End Region

    Sub New(ByVal DocId As Long, ByVal CurrentUserId As Int64)

        MyBase.New()
        Me.CurrentUserId = CurrentUserId
        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()
        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
        _fc = New FiltersComponent
        'Se agrega el evento para abrir los mails
        AddHandler grdGeneral.OnDoubleClick, AddressOf ShowMail

        CargarHistorial(DocId)
    End Sub

    Private Sub ShowMail(ByVal sender As Object, ByVal e As EventArgs)
        Dim mail As GridViewRowInfo = Nothing
        Dim id As Long
        Dim originalPathMsg As String
        Dim pathMsg As String
        Dim pathLocal As String
        Dim useBlobMails As Boolean
        Dim useWinWebService As Boolean
        Dim copyMailFileToBlob As Boolean

        Try
            mail = DirectCast(sender, GridDataCellElement).RowInfo
            If IsDBNull(mail.Cells("ID").Value) OrElse String.IsNullOrEmpty(mail.Cells("ID").Value) Then
                id = -1
                ZTrace.WriteLineIf(ZTrace.IsInfo, "# El mail no contiene ID. Es muy probable que se haya enviado con una versión de Zamba antigua. Se procederá a la apertura vía volumen físico.")
            Else
                id = CLng(mail.Cells("ID").Value)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "ID de mail seleccionado para su apertura: " & id.ToString)
            End If

            originalPathMsg = mail.Cells("PATH").Value.ToString
            pathMsg = originalPathMsg
            pathLocal = GetLocalPath(originalPathMsg, id)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "PathLocal: " & pathLocal)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "PathMsg: " & pathMsg)

            Boolean.TryParse(ZOptBusiness.GetValue("UseBlobMails"), useBlobMails)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Opcion UseBlobMails = " & useBlobMails)

            Try
                'Si debe utilizar blob para cargar el mail y si el mail contiene un id válido (los mails viejos no contienen id)
                If useBlobMails And id <> -1 Then
                    If Not File.Exists(pathLocal) Then
                        Dim fileB As Byte() = MessagesBusiness.GetMessageFile(id)

                        If fileB Is Nothing OrElse fileB.Length = 0 Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "El archivo binario del mail no se ha encontrado.")

                            Boolean.TryParse(ZOptBusiness.GetValue("CopyMailFileToBlob"), copyMailFileToBlob)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Opcion CopyMailFileToBlob = " & copyMailFileToBlob)

                            If copyMailFileToBlob Then
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se procede a insertar el archivo físico en la tabla de historial de mail.")
                                If Not File.Exists(originalPathMsg) Then
                                    MessageBox.Show("El mensaje no tiene cuerpo para ser mostrado", "Historial", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    fileB = Nothing
                                    Exit Sub
                                End If

                                Boolean.TryParse(ZOptBusiness.GetValue("UseWinWebService"), useWinWebService)
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Opcion UseWinWebService = " & useWinWebService)

                                If useWinWebService Then
                                    Dim WinWebServiceBusiness As WinWebServiceBusiness = New WinWebServiceBusiness()
                                    fileB = WinWebServiceBusiness.GetMessageFile(originalPathMsg, CurrentUserId)
                                    If fileB Is Nothing OrElse fileB.Length = 0 Then
                                        If File.Exists(originalPathMsg) Then
                                            'Busco en fisico , lo inserto en blob y lo muestro.
                                            Using fs As New FileStream(originalPathMsg, FileMode.Open, FileAccess.Read)
                                                fileB = New Byte(fs.Length - 1) {}
                                                fs.Read(fileB, 0, Convert.ToInt32(fs.Length))
                                                fs.Close()
                                            End Using
                                            'Lo guardo en Blob
                                            WinWebServiceBusiness.SaveMessageFile(originalPathMsg, fileB)
                                            fileB = Nothing
                                        Else
                                            MessageBox.Show("No se pudo encontrar el mensaje guardado", "Historial", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            WinWebServiceBusiness = Nothing
                                            fileB = Nothing
                                            Exit Sub
                                        End If
                                    End If
                                    WinWebServiceBusiness = Nothing
                                Else
                                    If File.Exists(originalPathMsg) Then
                                        'Busco en fisico , lo inserto en blob y lo muestro.
                                        Using fs As New FileStream(originalPathMsg, FileMode.Open, FileAccess.Read)
                                            fileB = New Byte(fs.Length - 1) {}
                                            fs.Read(fileB, 0, Convert.ToInt32(fs.Length))
                                            fs.Close()
                                        End Using
                                        'Lo guardo en Blob
                                        Dim WinWebServiceBusiness As WinWebServiceBusiness = New WinWebServiceBusiness()
                                        If Not WinWebServiceBusiness.SaveMessageFile(originalPathMsg, fileB) Then
                                            MessageBox.Show("No se pudo encontrar el mensaje guardado", "Historial", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            WinWebServiceBusiness = Nothing
                                            fileB = Nothing
                                            Exit Sub
                                        End If
                                        WinWebServiceBusiness = Nothing
                                        fileB = Nothing
                                    Else
                                        MessageBox.Show("No se pudo encontrar el mensaje guardado", "Historial", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        fileB = Nothing
                                        Exit Sub
                                    End If
                                End If

                                SaveToDiskByteArray(fileB, pathLocal)
                            Else
                                CopyMail(pathMsg, pathLocal)
                            End If
                        Else
                            SaveToDiskByteArray(fileB, pathLocal)
                        End If
                    End If
                Else
                    CopyMail(pathMsg, pathLocal)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

            If pathMsg.EndsWith(".html") Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Decodificando html: " & pathLocal)
                DecodeHTML(pathLocal)
            End If

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Abriendo mail: " & pathMsg)
            System.Diagnostics.Process.Start(pathLocal)

        Catch ex As Exception
            ZClass.raiseerror(New Exception("Ha ocurrido un error al mostrar el mail.", ex))
        Finally
            mail = Nothing
        End Try
    End Sub

    Private Function GetLocalPath(ByVal pathMsg As String, ByVal id As Int64) As String
        Dim fileName As String = id.ToString
        Dim pathLocal As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Zamba Software\OfficeTemp"
        pathLocal = Path.Combine(pathLocal, fileName & Path.GetExtension(pathMsg))

        Return pathLocal
    End Function

    ''' <summary>
    ''' Guarda en disco el array de bytes de un mail
    ''' </summary>
    ''' <param name="fileB"></param>
    ''' <param name="pathLocal"></param>
    ''' <remarks></remarks>
    Private Sub SaveToDiskByteArray(ByRef fileB As Byte(), ByVal pathLocal As String)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Creando archivo físico desde binario en la ruta '" & pathLocal & "'")
        Using fs As New FileStream(pathLocal, FileMode.Create, FileAccess.Write)
            fs.Write(fileB, 0, Convert.ToInt32(fileB.Length))
            fs.Close()
        End Using
        fileB = Nothing
    End Sub
    ''' <summary>
    ''' Copia un mail a la carpeta temporal
    ''' </summary>
    ''' <param name="pathMsg">Ruta de origen</param>
    ''' <param name="pathLocal">Ruta de destino</param>
    ''' <remarks></remarks>
    Private Sub CopyMail(ByVal pathMsg As String, ByVal pathLocal As String)
        If File.Exists(pathMsg) Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Copiando '" & pathMsg & "' a '" & pathLocal & "'")
            File.Copy(pathMsg, pathLocal, True)
        Else
            MessageBox.Show("No se pudo encontrar el mensaje guardado", "Historial", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw New FileNotFoundException("No se ha encontrado el archivo de historial del mail seleccionado", pathMsg)
        End If
    End Sub


    ''' <summary>
    ''' Codifica el html para su correcta visualizacion
    ''' </summary>
    ''' <param name="path"></param>
    ''' <remarks></remarks>
    Private Sub DecodeHTML(ByVal path As String)
        Dim strReader As StreamReader
        Dim strWriter As StreamWriter

        If Boolean.Parse(UserPreferences.getValue("UseMsgHTMLOpenDecode", UPSections.UserPreferences, "False")) Then
            Try
                strReader = New StreamReader(path)
                Dim str As String = strReader.ReadToEnd()
                strReader.Close()
                strReader.Dispose()
                strReader = Nothing

                'codigo jere
                str = str.Replace(" & ", " &amp; ")
                str = str.Replace("´", "&acute;")
                str = str.Replace("°", "&deg;")
                str = str.Replace("º", "&ordm;")

                str = str.Replace("¥", "&yen;")

                str = str.Replace("ñ", "&ntilde;")
                str = str.Replace("Ñ", "&Ntilde;")

                str = str.Replace("á", "&aacute;")
                str = str.Replace("é", "&eacute;")
                str = str.Replace("í", "&iacute;")
                str = str.Replace("ó", "&oacute;")
                str = str.Replace("ú", "&uacute;")

                str = str.Replace("Á", "&Aacute;")
                str = str.Replace("É", "&Eacute;")
                str = str.Replace("Í", "&Iacute;")
                str = str.Replace("Ó", "&Oacute;")
                str = str.Replace("Ú", "&Uacute;")

                strWriter = New StreamWriter(path)
                strWriter.Write(str)
                strWriter.Close()
                strWriter.Dispose()
                strWriter = Nothing

            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                If Not IsNothing(strReader) Then
                    strReader.Close()
                    strReader.Dispose()
                    strReader = Nothing
                End If
            End Try
        End If
    End Sub

    Public Sub ShowInfo(ByVal DocId As Long)
        CargarHistorial(DocId)
    End Sub

    Public Sub RefreshGrid()
        CargarHistorial(_docId)
    End Sub

    Private Sub CargarHistorial(ByVal DocId As Long)
        Try

            _docId = DocId
            Dim dt As DataTable
            Dim ds As DataSet = EmailBusiness.getHistory(_docId)

            If Not IsNothing(ds) Then
                dt = ds.Tables(0)
            End If

            If Not IsNothing(dt) Then
                dt.MinimumCapacity = dt.Rows.Count
                grdGeneral.DataSource = dt
                grdGeneral.AutoSize = True
                grdGeneral.AutoSizeMode = AutoSizeMode.GrowOnly
                grdGeneral.Dock = DockStyle.Fill
                grdGeneral.FixColumns()
                grdGeneral.AlwaysFit = True
            End If

            If grdGeneral.NewGrid.Columns.Contains("PATH") Then
                grdGeneral.NewGrid.Columns("PATH").IsVisible = False
            End If

            If grdGeneral.NewGrid.Columns.Contains("ID") Then
                grdGeneral.NewGrid.Columns("ID").IsVisible = False
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Zamba.Core.ZClass.RaiseNotifyError("Ocurrio un error al mostrar el historial, contactese con el Administrador del Sistema")
        End Try

    End Sub
    Private _fc As IFiltersComponent

    Public Property Fc() As IFiltersComponent Implements IGrid.Fc
        Get
            Return _fc
        End Get
        Set(ByVal value As IFiltersComponent)
            _fc = value
        End Set
    End Property
    Private _lastPage As Integer

    Public Property LastPage() As Integer Implements IGrid.LastPage
        Get
            Return _lastPage
        End Get
        Set(ByVal value As Integer)
            _lastPage = value
        End Set
    End Property

    Public Property PageSize As Integer Implements IGrid.PageSize

    Public Property Exporting As Boolean Implements IGrid.Exporting

    Public Property ExportSize As Integer Implements IGrid.ExportSize

    Public Property SaveSearch As Boolean Implements IGrid.SaveSearch
        Get
        End Get
        Set(value As Boolean)
        End Set
    End Property

    Public Property SortChanged As Boolean Implements IOrder.SortChanged

    Public Property FiltersChanged As Boolean Implements IFilter.FiltersChanged
        Get

        End Get
        Set(value As Boolean)

        End Set
    End Property

    Public Property FontSizeChanged As Boolean Implements IGrid.FontSizeChanged

    Public Sub ShowTaskOfDT() Implements IGrid.ShowTaskOfDT
        Try
            'Buscar mails enviados
            'Cargar grilla
            CargarHistorial(_docId)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Sub AddOrderComponent(orderString As String) Implements IOrder.AddOrderComponent

    End Sub
    Public Sub AddGroupByComponent(v As String) Implements IGrid.AddGroupByComponent

    End Sub
End Class

