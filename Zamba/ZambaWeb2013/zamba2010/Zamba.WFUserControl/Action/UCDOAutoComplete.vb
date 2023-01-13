'Imports Zamba.WFBusiness
Imports zamba.Controls
'''' -----------------------------------------------------------------------------
'''' Project	 : Zamba.Controls
'''' Class	 : Controls.UCDOAutoComplete
'''' 
'''' -----------------------------------------------------------------------------
'''' <summary>
''''    Control de configuracion para Rule Action DOAutocomplete     
'''' </summary>
'''' <remarks>
'''' </remarks>
'''' <history>
'''' 	[oscar]	06/06/2006	Created
'''' </history>
'''' -----------------------------------------------------------------------------
'TODO:ACTION:AutoComplete
Public Class UCDOAutoComplete
    Inherits ZRuleControl


#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New(ByRef p_autoComplete As IDOAutoComplete, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(p_autoComplete, _wfPanelCircuit)
        InitializeComponent()
    End Sub

    'Public Sub New()
    '    MyBase.New()
    '    'El Diseñador de Windows Forms requiere esta llamada.
    '    InitializeComponent()
    '    'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    'End Sub

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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
    Friend WithEvents UcComplete As UCComplete
    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        UcComplete = New Zamba.Controls.UCComplete()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(UcComplete)
        tbRule.Size = New System.Drawing.Size(661, 432)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(669, 461)
        '
        'UcComplete
        '
        UcComplete.BackColor = System.Drawing.Color.White
        UcComplete.Dock = System.Windows.Forms.DockStyle.Fill
        UcComplete.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        UcComplete.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        UcComplete.HelpId = Nothing
        UcComplete.IndexKey = False
        UcComplete.Location = New System.Drawing.Point(3, 3)
        UcComplete.Name = "UcComplete"
        UcComplete.Size = New System.Drawing.Size(655, 426)
        UcComplete.TabIndex = 0
        '
        'UCDOAutoComplete
        '
        Name = "UCDOAutoComplete"
        Size = New System.Drawing.Size(669, 461)
        tbRule.ResumeLayout(False)
        tbctrMain.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

    Public Shadows ReadOnly Property MyRule() As IDOAutoComplete
        Get
            Return DirectCast(Rule, IDOAutoComplete)
        End Get
    End Property

    Private Sub UcComplete_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles UcComplete.Load

    End Sub
End Class
