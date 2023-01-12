Public Class UCDoQuitarWF
    Inherits ZRuleControl

    'Private P As Int32

#Region " Código generado por el Diseñador de Windows Forms "


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
    Dim WithEvents btnaceptar As ZButton
    Shadows WithEvents Label1 As ZLabel

    <DebuggerStepThrough()> Private Overloads Sub InitializeComponent()
        btnaceptar = New ZButton()
        Label1 = New ZLabel()
        tbRule.SuspendLayout()
        tbctrMain.SuspendLayout()
        SuspendLayout()
        '
        'tbRule
        '
        tbRule.Controls.Add(Label1)
        tbRule.Controls.Add(btnaceptar)
        tbRule.Size = New System.Drawing.Size(344, 350)
        '
        'tbctrMain
        '
        tbctrMain.Size = New System.Drawing.Size(352, 376)
        '
        'btnaceptar
        '
        btnaceptar.Location = New System.Drawing.Point(104, 320)
        btnaceptar.Name = "btnaceptar"
        btnaceptar.Size = New System.Drawing.Size(112, 23)
        btnaceptar.TabIndex = 31
        btnaceptar.Text = "Aceptar"
        '
        'Label1
        '
        Label1.BackColor = System.Drawing.Color.Transparent
        Label1.Font = New Font("Tahoma", 12.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Label1.Location = New System.Drawing.Point(32, 128)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(296, 96)
        Label1.TabIndex = 32
        Label1.Text = "Elimina de WorkFlow el documento, pero no la documentación."
        Label1.TextAlign = ContentAlignment.MiddleCenter
        '
        'UCDoQuitarWF
        '
        Name = "UCDoQuitarWF"
        Size = New System.Drawing.Size(352, 376)
        tbRule.ResumeLayout(False)
        tbctrMain.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

    Public Shadows ReadOnly Property MyRule() As Core.WFRuleParent
        Get
            Return Nothing
            'TFalta la regla doquitarWF
        End Get
    End Property

    Public Sub New(ByVal rule As IRule, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(rule, _wfPanelCircuit)
    End Sub

End Class
