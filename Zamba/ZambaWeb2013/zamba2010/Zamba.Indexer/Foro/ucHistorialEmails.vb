Imports Zamba.Grid.Grid
Imports Zamba.Filters
Imports Zamba.Core
Imports Zamba.AppBlock
Imports Zamba.Grid
Imports System.IO
Imports Zamba.Filters.Interfaces

Public Class ucHistorialEmails

    Inherits ZwhiteControl
    Implements IFilter

    Private _docId As Int32 = 0

#Region " Código generado por el Diseñador de Windows Forms "
    Dim CurrentUserId As Int64

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        grdGeneral = New GroupGrid(True, CurrentUserId, Me)
        Me.grdGeneral.BackColor = System.Drawing.Color.LightSteelBlue
        Me.grdGeneral.DataSource = Nothing
        Me.grdGeneral.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdGeneral.ForeColor = System.Drawing.Color.Black
        Me.grdGeneral.Location = New System.Drawing.Point(0, 0)
        Me.grdGeneral.Name = "GridView"
        Me.grdGeneral.Size = New System.Drawing.Size(912, 306)
        Me.grdGeneral.TabIndex = 0
        Me.grdGeneral.WithExcel = False
        '
        'UCTaskHistory
        '
        Me.Name = "UCTaskHistory"
        Me.Size = New System.Drawing.Size(624, 504)
        Me.Controls.Add(Me.grdGeneral)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Sub New(ByVal DocId As Long, ByVal CurrentUserId As Int64)

        MyBase.New()
        Me.CurrentUserId = CurrentUserId
        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()
        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
        Me._fc = New FiltersComponent
        'Se agrega el evento para abrir los mails
        AddHandler grdGeneral.OnDoubleClick, AddressOf ShowMail

        CargarHistorial(DocId)
    End Sub

    Private Sub ShowMail(ByVal sender As Object, ByVal e As EventArgs)

        Try
            Dim mail As DataGridViewRow = DirectCast(sender, GroupGrid).OutLookGrid.SelectedRows(0)
            Dim pathMsg As String = mail.Cells("PATH").Value.ToString
            Dim pathLocal As String

            pathLocal = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Zamba Software\OfficeTemp"
            pathLocal = pathLocal & pathMsg.Remove(0, pathMsg.LastIndexOf("\"))
            pathLocal = Path.Combine(Path.GetDirectoryName(pathLocal), Path.GetFileNameWithoutExtension(pathLocal) & "_" & DateTime.Now.ToString("HHmmss") & Path.GetExtension(pathLocal))

            Try
                If File.Exists(pathMsg) Then
                    File.Copy(pathMsg, pathLocal, True)
                Else
                    Trace.WriteLineIf(ZTrace.IsError, "No se pudo encontrar el mensaje guardado para verlo en el historial: " & pathMsg)
                    MessageBox.Show("No se pudo encontrar el mensaje guardado", "Historial", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            Catch ex As Exception
                raiseerror(ex)
            End Try

            If pathMsg.EndsWith(".msg") Then
                Trace.WriteLineIf(ZTrace.IsInfo, "Abriendo msg desde el historial: " & pathMsg)
                Dim ol As Zamba.Office.Outlook.OutlookInterop = Zamba.Office.Outlook.SharedOutlook.GetOutlook()
                ol.OpenMailItem(pathLocal, False, FormWindowState.Maximized)
                ol = Nothing
            Else
                Trace.WriteLineIf(ZTrace.IsInfo, "Abriendo HTML desde el historial: " & pathMsg)
                Dim psi As New ProcessStartInfo()
                psi.UseShellExecute = True
                psi.FileName = "file:///" & pathLocal
                System.Diagnostics.Process.Start(psi)
                psi = Nothing
            End If

        Catch ex As Exception
            ZClass.raiseerror(New Exception("Ha ocurrido un error al mostrar el mail.", ex))
        End Try
    End Sub

    Public Sub ShowInfo(ByVal DocId As Long)
        CargarHistorial(DocId)
    End Sub

    Public Sub RefreshGrid()
        CargarHistorial(_docId)
    End Sub

    Private Sub CargarHistorial(ByVal DocId As Long)
        _docId = DocId
        Dim dt As DataTable = EmailBusiness.getHistory(_docId).Tables(0)
        dt.MinimumCapacity = dt.Rows.Count
        Me.grdGeneral.DataSource = Zamba.Core.EmailBusiness.getHistory(_docId).Tables(0)
        Me.grdGeneral.AutoSize = True
        Me.grdGeneral.AutoSizeMode = Windows.Forms.AutoSizeMode.GrowOnly
        Me.grdGeneral.Dock = DockStyle.Fill
        Me.grdGeneral.FixColumns()
        Me.grdGeneral.AlwaysFit = True

        If Me.grdGeneral.OutLookGrid.Columns.Contains("PATH") Then
            Me.grdGeneral.OutLookGrid.Columns("PATH").Visible = False
        End If
    End Sub
    Private _fc As IFiltersComponent

    Public Property Fc() As IFiltersComponent Implements IFilter.Fc
        Get
            Return _fc
        End Get
        Set(ByVal value As IFiltersComponent)
            _fc = value
        End Set
    End Property
    Private _lastPage As Integer

    Public Property LastPage() As Integer Implements IFilter.LastPage
        Get
            Return _lastPage
        End Get
        Set(ByVal value As Integer)
            _lastPage = value
        End Set
    End Property
    Public Sub ShowTaskOfDT() Implements IFilter.ShowTaskOfDT

    End Sub
End Class

