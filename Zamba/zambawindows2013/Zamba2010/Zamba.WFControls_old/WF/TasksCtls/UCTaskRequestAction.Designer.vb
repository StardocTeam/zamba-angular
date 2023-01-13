Imports Zamba.Grid.Grid
Imports Zamba.Filters
Imports Zamba.Filters.Interfaces
Imports Zamba.Core

Namespace WF.TasksCtls
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class UCTaskRequestAction
        Inherits Zamba.AppBlock.ZControl
        Implements IGrid


        'UserControl overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

#Region " Código generado por el Diseñador de Windows Forms "

        'Requerido por el Diseñador de Windows Forms
        Private components As System.ComponentModel.IContainer

        'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
        'Puede modificarse utilizando el Diseñador de Windows Forms. 
        'No lo modifique con el editor de código.
        Friend WithEvents grdGeneral As GroupGrid
        Friend WithEvents DOC_ID As System.Windows.Forms.DataGridTextBoxColumn
        Friend WithEvents CheckIn As System.Windows.Forms.DataGridTextBoxColumn
        Friend WithEvents CheckOut As System.Windows.Forms.DataGridTextBoxColumn
        Friend WithEvents Etapa As System.Windows.Forms.DataGridTextBoxColumn
        Friend WithEvents EstadoInicial As System.Windows.Forms.DataGridTextBoxColumn
        Friend WithEvents EstadoFinal As System.Windows.Forms.DataGridTextBoxColumn
        Friend WithEvents UCheckin As System.Windows.Forms.DataGridTextBoxColumn
        Friend WithEvents DOCTYPE As System.Windows.Forms.DataGridTextBoxColumn
        Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
        Friend WithEvents tsbActualizar As System.Windows.Forms.ToolStripButton
        Friend WithEvents UCheckOut As System.Windows.Forms.DataGridTextBoxColumn

        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

            Me.grdGeneral = New GroupGrid(True, CurrentUserId, Me, FilterTypes.Document)
            Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
            Me.tsbActualizar = New System.Windows.Forms.ToolStripButton
            Me.ToolStrip1.SuspendLayout()
            Me.SuspendLayout()
            '
            'grdGeneral

            Me.grdGeneral.BackColor = System.Drawing.Color.WhiteSmoke
            Me.grdGeneral.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.grdGeneral.Dock = System.Windows.Forms.DockStyle.Fill
            Me.grdGeneral.Font = New System.Drawing.Font("Tahoma", 8.0!)
            Me.grdGeneral.ForeColor = System.Drawing.Color.MidnightBlue
            Me.grdGeneral.ImeMode = System.Windows.Forms.ImeMode.Disable
            Me.grdGeneral.Location = New System.Drawing.Point(0, 25)
            Me.grdGeneral.Name = "grdGeneral"
            Me.grdGeneral.Size = New System.Drawing.Size(624, 479)
            Me.grdGeneral.TabIndex = 4


            '
            'ToolStrip1
            '
            Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbActualizar})
            Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
            Me.ToolStrip1.Name = "ToolStrip1"
            Me.ToolStrip1.Size = New System.Drawing.Size(624, 25)
            Me.ToolStrip1.TabIndex = 26
            Me.ToolStrip1.Text = "ToolStrip1"
            '
            'tsbActualizar
            '
            Me.tsbActualizar.Image = Global.Zamba.Controls.My.Resources.Resources.bullet_ball_glass_green
            Me.tsbActualizar.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.tsbActualizar.Name = "tsbActualizar"
            Me.tsbActualizar.Size = New System.Drawing.Size(74, 22)
            Me.tsbActualizar.Text = "Actualizar"
            Me.tsbActualizar.ToolTipText = "Actualizar Historial"
            '
            'UCTaskHistory
            '
            Me.Controls.Add(Me.grdGeneral)
            'Me.Controls.Add(Me.ToolStrip1)
            Me.Name = "UCTaskAprobements"
            Me.Size = New System.Drawing.Size(624, 504)
            'CType(Me.grdGeneral, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ToolStrip1.ResumeLayout(False)
            Me.ToolStrip1.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

#End Region
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
            Get
                Throw New NotImplementedException()
            End Get
            Set(value As Integer)
                Throw New NotImplementedException()
            End Set
        End Property

        Public Property Exporting As Boolean Implements IGrid.Exporting
            Get
                Throw New NotImplementedException()
            End Get
            Set(value As Boolean)
                Throw New NotImplementedException()
            End Set
        End Property

        Public Property ExportSize As Integer Implements IGrid.ExportSize
            Get
                Throw New NotImplementedException()
            End Get
            Set(value As Integer)
                Throw New NotImplementedException()
            End Set
        End Property

        Public Sub ShowTaskOfDT() Implements IGrid.ShowTaskOfDT

        End Sub

        Public Sub AddOrderComponent(newValue As String, propertyName As String) Implements IGrid.AddOrderComponent

        End Sub
    End Class
End Namespace