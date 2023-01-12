Imports Zamba.Core.Enumerators
Public Class UCRuleType
    Inherits zcontrol


#Region " Código generado por el Diseñador de Windows Forms "

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
    Friend WithEvents PanelTop As ZLabel
    Friend WithEvents PanelFill As ZPanel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        PanelTop = New ZLabel
        PanelFill = New ZPanel
        SuspendLayout()
        '
        'PanelTop
        '
        PanelTop.BackColor = System.Drawing.Color.White
        PanelTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        PanelTop.Font = New Font("Verdana", 12.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        PanelTop.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        PanelTop.Location = New System.Drawing.Point(0, 0)
        PanelTop.Name = "PanelTop"
        PanelTop.Size = New System.Drawing.Size(596, 32)
        PanelTop.TabIndex = 99
        PanelTop.Text = "  Tipo de Regla"
        PanelTop.TextAlign = ContentAlignment.MiddleLeft
        '
        'PanelFill
        '
        PanelFill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        PanelFill.Dock = System.Windows.Forms.DockStyle.Fill
        PanelFill.Location = New System.Drawing.Point(0, 32)
        PanelFill.Name = "PanelFill"
        PanelFill.Size = New System.Drawing.Size(596, 488)
        PanelFill.TabIndex = 100
        '
        'UCRuleType
        '
        Controls.Add(PanelFill)
        Controls.Add(PanelTop)
        Name = "UCRuleType"
        Size = New System.Drawing.Size(596, 520)
        ResumeLayout(False)

    End Sub

#End Region


    Public Sub New(ByVal stepid As Int64, ByVal RuleParentType As TypesofRules, ByVal ruleIds As Generic.List(Of Int64))
        MyBase.New()
        InitializeComponent()

        Dim message As String = String.Empty

        Select Case RuleParentType

            Case TypesofRules.Entrada
                PanelTop.Text = " Entrada"
                message = "Las reglas de entrada se ejecutaran automáticamente cuando un documento entre en una etapa de distribución."

            Case TypesofRules.Salida
                PanelTop.Text = " Salida"
                message = "Las reglas de salida se ejecutan cuando un documento es distribuido a otra etapa. Este no puede poseer reglas de Distribución."

            Case TypesofRules.Actualizacion
                PanelTop.Text = " Actualizacion"
                message = "El servidor de WorkFlow se encarga de iniciar, mantener y finalizar un servicio de sesión por el cual ejecutara acciones en su tiempo de refresco determinado."

            Case TypesofRules.AccionUsuario
                PanelTop.Text = " Accion de Usuario"
                message = "Acciones ejecutadas por el usuario desde su respectiva interfaz."


            Case TypesofRules.Planificada
                PanelTop.Text = " Planifica"
                message = "Se pueden configurar reglas para que entren en acción en una fecha y hora planificada."

            Case TypesofRules.Eventos
                PanelTop.Text = " Eventos"
                message = "Acciones ejecutadas en el momento en que se dispare el evento seleccionado."
        End Select

        Dim UCHelp As New UCUsrHelp(stepid, ruleIds, message)
        UCHelp.Dock = DockStyle.Fill
        PanelFill.Controls.Add(UCHelp)

    End Sub
End Class
