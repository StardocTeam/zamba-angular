Imports ZAMBA.AppBlock
Imports Zamba.Core
Imports Zamba.Data


Public Class UCTaskGridResume
    Inherits ZControl

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

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
    Friend WithEvents Panel2 As ZBluePanel
    Friend WithEvents DataGrid1 As System.Windows.Forms.DataGrid
    Friend WithEvents StepStyle As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents ColNAME As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents ColDocCounts As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents ColID As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents ColAutoName As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents ColState As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents ColUser As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents Documents As System.Windows.Forms.DataGridTableStyle
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel2 = New Zamba.AppBlock.ZBluePanel
        Me.DataGrid1 = New System.Windows.Forms.DataGrid
        Me.StepStyle = New System.Windows.Forms.DataGridTableStyle
        Me.ColNAME = New System.Windows.Forms.DataGridTextBoxColumn
        Me.ColDocCounts = New System.Windows.Forms.DataGridTextBoxColumn
        Me.Documents = New System.Windows.Forms.DataGridTableStyle
        Me.ColID = New System.Windows.Forms.DataGridTextBoxColumn
        Me.ColAutoName = New System.Windows.Forms.DataGridTextBoxColumn
        Me.ColState = New System.Windows.Forms.DataGridTextBoxColumn
        Me.ColUser = New System.Windows.Forms.DataGridTextBoxColumn
        Me.Panel2.SuspendLayout()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.DataGrid1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(752, 424)
        Me.Panel2.TabIndex = 1
        '
        'DataGrid1
        '
        Me.DataGrid1.AlternatingBackColor = System.Drawing.Color.GhostWhite
        Me.DataGrid1.BackColor = System.Drawing.Color.GhostWhite
        Me.DataGrid1.BackgroundColor = System.Drawing.Color.White
        Me.DataGrid1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGrid1.CaptionBackColor = System.Drawing.Color.SteelBlue
        Me.DataGrid1.CaptionFont = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.DataGrid1.CaptionForeColor = System.Drawing.Color.White
        Me.DataGrid1.CaptionText = "Tareas Pendientes"
        Me.DataGrid1.CaptionVisible = False
        Me.DataGrid1.DataMember = ""
        Me.DataGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGrid1.FlatMode = True
        Me.DataGrid1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGrid1.ForeColor = System.Drawing.Color.MidnightBlue
        Me.DataGrid1.GridLineColor = System.Drawing.Color.LightSteelBlue
        Me.DataGrid1.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None
        Me.DataGrid1.HeaderBackColor = System.Drawing.Color.MidnightBlue
        Me.DataGrid1.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.DataGrid1.HeaderForeColor = System.Drawing.Color.Lavender
        Me.DataGrid1.LinkColor = System.Drawing.Color.Teal
        Me.DataGrid1.Location = New System.Drawing.Point(0, 0)
        Me.DataGrid1.Name = "DataGrid1"
        Me.DataGrid1.ParentRowsBackColor = System.Drawing.Color.Lavender
        Me.DataGrid1.ParentRowsForeColor = System.Drawing.Color.MidnightBlue
        Me.DataGrid1.ReadOnly = True
        Me.DataGrid1.RowHeaderWidth = 10
        Me.DataGrid1.SelectionBackColor = System.Drawing.Color.Teal
        Me.DataGrid1.SelectionForeColor = System.Drawing.Color.PaleGreen
        Me.DataGrid1.Size = New System.Drawing.Size(752, 424)
        Me.DataGrid1.TabIndex = 0
        Me.DataGrid1.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.StepStyle, Me.Documents})
        '
        'StepStyle
        '
        Me.StepStyle.DataGrid = Me.DataGrid1
        Me.StepStyle.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.ColNAME, Me.ColDocCounts})
        Me.StepStyle.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.StepStyle.MappingName = "WFSteps"
        '
        'ColNAME
        '
        Me.ColNAME.Format = ""
        Me.ColNAME.FormatInfo = Nothing
        Me.ColNAME.HeaderText = "Etapa"
        Me.ColNAME.MappingName = "Step_Name"
        Me.ColNAME.Width = 150
        '
        'ColDocCounts
        '
        Me.ColDocCounts.Format = ""
        Me.ColDocCounts.FormatInfo = Nothing
        Me.ColDocCounts.HeaderText = "Tareas Pendientes"
        Me.ColDocCounts.MappingName = "Step_DocCounts"
        Me.ColDocCounts.NullText = "Sin tareas"
        Me.ColDocCounts.Width = 30
        '
        'Documents
        '
        Documents.DataGrid = Me.DataGrid1
        Documents.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.ColID, Me.ColAutoName, Me.ColState, Me.ColUser})
        Documents.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Documents.MappingName = "TAREAS"
        '
        'ColID
        '
        Me.ColID.Format = ""
        Me.ColID.FormatInfo = Nothing
        Me.ColID.HeaderText = "Id"
        Me.ColID.MappingName = "Doc_Id"
        Me.ColID.NullText = ""
        Me.ColID.Width = 15
        '
        'ColAutoName
        '
        Me.ColAutoName.Format = ""
        Me.ColAutoName.FormatInfo = Nothing
        Me.ColAutoName.HeaderText = "Nombre"
        Me.ColAutoName.MappingName = "Name"
        Me.ColAutoName.NullText = ""
        Me.ColAutoName.Width = 150
        '
        'ColState
        '
        Me.ColState.Format = ""
        Me.ColState.FormatInfo = Nothing
        Me.ColState.HeaderText = "Estado"
        Me.ColState.MappingName = "State"
        Me.ColState.NullText = "Sin Revisar"
        Me.ColState.Width = 75
        '
        'ColUser
        '
        Me.ColUser.Format = ""
        Me.ColUser.FormatInfo = Nothing
        Me.ColUser.HeaderText = "Usuario"
        Me.ColUser.MappingName = "User"
        Me.ColUser.NullText = "Sistema"
        Me.ColUser.Width = 75
        '
        'UCTaskGridResume
        '
        Me.Controls.Add(Me.Panel2)
        Me.Name = "UCTaskGridResume"
        Me.Size = New System.Drawing.Size(752, 424)
        Me.Panel2.ResumeLayout(False)
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim DsStepsStates As New DsStepsTask
    Dim DsStepsInfo As New DsDisplayTasks
    Dim DsStepTask As New DsDisplayTask

    'Private FlagResumeShowing As Boolean


    Public Sub ShowStepsTask(ByRef wfstep As WFStep)
        'Me.FlagResumeShowing = False
        Try
            For Each Result As Zamba.Core.TaskResult In WfStep.Tasks
                Dim row As DsDisplayTask.TareasRow = DsStepTask.Tareas.NewTareasRow
                row.Id = CInt(Result.ID)
                row.Tarea = Result.Name
                row.Creado = Result.CreateDate
                'If IsNothing(Result.CheckOut) Then row.Egreso = Nothing Else row.Egreso = Result.CheckOut
                row.Estado = WFStepStatesFactory.GetStateName(Result.State.Id, Me.DsStepsStates)
                row.Etapa = WfStep.Name
                'todo: ver de donde saco el nombre del wf
                If IsNothing(WfStep.Parent) Then
                    row.Flujo = ""
                    ' Else
                    'row.Flujo = WFBusiness.GetWFName(WfStep.Parent.Id)
                End If
                row.Ingreso = Result.CheckIn
                'If IsNothing(Result.Start) Then row.Inicio = Nothing Else row.Inicio = Result.Start
                If IsNothing(Result.EditDate) Then row.Ultima_Revision = Nothing Else row.Ultima_Revision = Result.EditDate
                If IsNothing(Result.UserID) Then row.Usuario = Nothing Else row.Usuario = Result.UserID.ToString()
                If IsNothing(Result.ExpireDate) Then row.Vencimiento = Nothing Else row.Vencimiento = Result.ExpireDate
                DsStepTask.Tareas.AddTareasRow(row)
            Next
            DsStepTask.AcceptChanges()
            Me.DataGrid1.DataSource = DsStepTask
            Me.DataGrid1.DataMember = DsStepTask.Tareas.TableName
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Public Sub ShowStepsInfo(ByVal WfSteps As ArrayList)
        'Me.FlagResumeShowing = True
        Try
            Dim i As Int32
            For i = 0 To WfSteps.Count - 1
                Dim row As DsDisplayTasks.WFStepsRow = DsStepsInfo.WFSteps.NewWFStepsRow
                row.Step_Id = WfSteps(i).id
                row.Step_Name = WfSteps(i).Name.ToString
                row.Step_DocCounts = WfSteps(i).DocumentsCount
                DsStepsInfo.WFSteps.AddWFStepsRow(row)

            Next
            DsStepsInfo.AcceptChanges()
            Me.DataGrid1.DataSource = DsStepsInfo
            Me.DataGrid1.DataMember = DsStepsInfo.WFSteps.TableName
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub UCTaskGrid_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.DsStepsStates = WFStepStatesFactory.GetStepsStates
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Private Sub DataGrid1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.CurrentCellChanged
        If Me.DataGrid1.CurrentRowIndex <> -1 Then
            Dim cell As DataGridCell = Me.DataGrid1.CurrentCell()
            MessageBox.Show(Me.DataGrid1.Item(cell).ToString, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

End Class
